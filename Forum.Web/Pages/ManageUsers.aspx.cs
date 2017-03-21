using Autofac.Integration.Web.Forms;
using Forum.Domain.Exceptions;
using Forum.Domain.Security;
using Forum.Service.Contracts.Security;
using Forum.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public partial class ManageUsers : PageBase
    {
        public IUserService UserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SecurityContext.IsAdmin)
                throw new NotAuthorizedException();

            if (!IsPostBack)
            {
                var navigation = new List<Tuple<string, string>>();

                navigation.Add(Tuple.Create(GetRouteUrl("Default", null), "Acasa"));
                navigation.Add(Tuple.Create(GetRouteUrl("ManageUsers", null), "Gestionare utilizatori"));
                Session["Navigation"] = navigation;
                DataBind();
            }
        }

        protected void usersGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;

            if (e.Row.RowType == DataControlRowType.DataRow &&
                (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                var userId = this.usersGridView.DataKeys[e.Row.RowIndex].Value;
                var user = UserService.Get(CustomConvert.ToInt32(userId));
                var roleDropDrown = e.Row.FindControl("roleDropDown") as DropDownList;

                roleDropDrown.DataSource = new List<object>
                {
                    new { Value = (int)Role.Normal, Text = Role.Normal.ToString() },
                    new { Value = (int)Role.Manager, Text = Role.Manager.ToString() },
                    new { Value = (int)Role.Admin, Text = Role.Admin.ToString() }
                };

                roleDropDrown.DataValueField = "Value";
                roleDropDrown.DataTextField = "Text";
                roleDropDrown.DataBind();
                roleDropDrown.SelectedIndex = (int)user.Role;
            }
        }

        protected void usersGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.usersGridView.EditIndex = e.NewEditIndex;
            this.usersGridView.DataSource = GetUsers();
            this.usersGridView.DataBind();
        }

        protected void usersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var userId = this.usersGridView.DataKeys[e.RowIndex].Value;
            var user = UserService.Get(CustomConvert.ToInt32(userId));
            var roleDropDown = this.usersGridView.Rows[e.RowIndex].FindControl("roleDropDown") as DropDownList;

            if (!SecurityContext.IsAdmin)
                throw new NotAuthorizedException();

            if (user != null)
            {
                try
                {
                    this.usersGridView.EditIndex = -1;
                    user.SetRole((Role)int.Parse(roleDropDown.SelectedValue));
                    UserService.Update(user);
                    UserService.CommitChanges();
                    DataBind();
                }
                catch (Exception execption)
                {
                    //handle exception
                    this.message.InnerText = GenericErrorMessage;
                    this.message.Visible = true;
                }
            }
        }

        protected void usersGridView_RowCanceling(object sender, GridViewCancelEditEventArgs e)
        {
            this.usersGridView.EditIndex = -1;
            this.usersGridView.DataSource = GetUsers();
            this.usersGridView.DataBind();
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.usersGridView.DataSource = GetUsers();
            this.usersGridView.DataBind();
        }

        protected object GetUsers()
        {
            IList<User> users;

            if (string.IsNullOrEmpty(searchQuery.Text))
                users = UserService.GetAll().ToList();
            else
                users = UserService.GetMany(x => x.UserName.Contains(searchQuery.Text.Trim())).ToList();

            return users?.Select(x => new
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Role = x.Role.ToString(),
                ProfileImageUrl = x.ProfileImage32Url
            }).ToList();
        }
    }
}