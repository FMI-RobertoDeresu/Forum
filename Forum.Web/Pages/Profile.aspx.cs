using Autofac.Integration.Web.Forms;
using Forum.Domain.Security;
using Forum.Service.Contracts.Security;
using Forum.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public partial class Profile : PageBase
    {
        public IUserService UserService { get; set; }

        protected new User User { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var navigation = new List<Tuple<string, string>>();

                User = UserService.Get(CustomConvert.ToInt32(RouteData.Values["userId"]));

                if (User == null)
                    Response.RedirectToRoute("Default", null);
                else
                {
                    navigation.Add(Tuple.Create(GetRouteUrl("Default", null), "Acasa"));
                    navigation.Add(Tuple.Create(GetRouteUrl("Profile", new { userId = User.Id }), User.FullName));
                    Session["Navigation"] = navigation;
                    DataBind();
                }
            }
        }

        protected void updateProfile_Click(object sender, EventArgs e)
        {
            User = UserService.Get(CustomConvert.ToInt32(RouteData.Values["userId"]));

            if (SecurityContext.User.Id == User.Id)
            {
                try
                {
                    if (this.profileImageUpload.HasFile)
                    {
                        var profileImage = Image.FromStream(this.profileImageUpload.FileContent);
                        UserService.SetProfilePicture(User, profileImage);
                    }

                    User.UpdateProfile(this.firstName.Text, this.lastName.Text);

                    UserService.Update(User);
                    UserService.CommitChanges();
                    Response.RedirectToRoute("Profile", new { userId = User.Id });
                }
                catch (Exception exception)
                {
                    //handle exception
                    this.message.InnerText = GenericErrorMessage;
                    this.message.Visible = true;
                }
            }
            else
            {
                Response.RedirectToRoute("NotFound", null);
            }
        }
    }
}