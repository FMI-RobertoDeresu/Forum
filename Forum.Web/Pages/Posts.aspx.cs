using Autofac.Integration.Web.Forms;
using Forum.Domain.Exceptions;
using Forum.Domain.Models;
using Forum.Service.Contracts.Entity;
using Forum.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public partial class Posts : PageBase
    {
        public ITopicService TopicService { get; set; }

        public IPostService PostService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var navigation = new List<Tuple<string, string>>();
                int topicId = CustomConvert.ToInt32(RouteData.Values["topicId"]);
                var topic = TopicService.Get(topicId);

                if (topic == null)
                    Response.RedirectToRoute("NotFound", null);
                else
                {
                    navigation.Add(Tuple.Create(GetRouteUrl("Default", null), "Acasa"));
                    navigation.Add(Tuple.Create(GetRouteUrl("TopicIndex", new { subjectId = topic.Subject.Id }), topic.Subject.Name));
                    navigation.Add(Tuple.Create(GetRouteUrl("PostIndex", new { topicId = topic.Id }), topic.Name));
                    Session["Navigation"] = navigation;

                    this.topicId.Value = topicId.ToString();
                    this.sortByDropDownList.DataSource = new List<object>
                    {
                        new { Value = "0", Text = "Data descrescator" },
                        new { Value = "1", Text = "Data crescator" },
                        new { Value = "2", Text = "Autor descrescator" },
                        new { Value = "3", Text = "Autor crescator" }
                    };
                    this.sortByDropDownList.DataValueField = "Value";
                    this.sortByDropDownList.DataTextField = "Text";
                    this.sortByDropDownList.DataBind();
                    DataBind();
                }
            }
        }

        protected void sortByDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.postsListView.DataBind();
        }

        protected void insertPostButton_Click(object sender, EventArgs e)
        {
            var topic = TopicService.Get(CustomConvert.ToInt32(this.topicId.Value));

            if (!SecurityContext.IsAuthenticated)
                throw new NotAuthorizedException();

            try
            {
                var post = new Post(this.newPostText.Text, topic, SecurityContext.User);

                PostService.Create(post);
                PostService.CommitChanges();
                Response.RedirectToRoute("PostIndex", new { topicId = topic.Id });
            }
            catch
            {
                //handle exception
                this.message.InnerText = GenericErrorMessage;
                this.message.Visible = true;
            }
        }

        protected void postsView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.postsListView.EditIndex = e.NewEditIndex;
            this.postsListView.DataSource = GetPosts(CustomConvert.ToInt32(this.topicId.Value));
            this.postsListView.DataBind();
        }

        protected void postsView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var postTextBox = this.postsListView.EditItem.FindControl("Text") as TextBox;
            var postIdField = this.postsListView.EditItem.FindControl("Id") as HiddenField;
            var post = PostService.Get(CustomConvert.ToInt32(postIdField.Value));

            if (post.CreatedBy != SecurityContext.User)
                throw new NotAuthorizedException();

            try
            {
                post.Edit(postTextBox.Text);
                PostService.Update(post);
                PostService.CommitChanges();
                Response.RedirectToRoute("PostIndex", new { topicId = this.topicId.Value });
            }
            catch
            {
                //handle exception
                this.message.InnerText = GenericErrorMessage;
                this.message.Visible = true;
            }
        }

        protected void postsView_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            Response.RedirectToRoute("PostIndex", new { topicId = this.topicId.Value });
        }

        protected void postsView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var postIdField = this.postsListView.Items[e.ItemIndex].FindControl("Id") as HiddenField;
            var postId = CustomConvert.ToInt32(postIdField.Value);
            var post = PostService.Get(postId);

            if (!SecurityContext.IsAdmin && !SecurityContext.IsManager)
                throw new NotAuthorizedException();

            try
            {
                PostService.Delete(post);
                PostService.CommitChanges();
                Response.RedirectToRoute("PostIndex", new { topicId = this.topicId.Value });
            }
            catch
            {
                //handle exception
                this.message.InnerText = GenericErrorMessage;
                this.message.Visible = true;
            }
        }

        protected object GetPosts(int topicId)
        {
            try
            {
                var result = TopicService.Get(topicId, x => x.Posts.Select(y => y.CreatedBy))?.Posts
                    .Select(x => new
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        CreatedById = x.CreatedBy?.Id,
                        CreatedByName = x.CreatedBy?.FullName,
                        CreatedByProfileImage = x.CreatedBy?.ProfileImage64Url,
                        Text = x.Text,
                        CanEdit = SecurityContext.IsAuthenticated && x.CreatedBy == SecurityContext.User,
                        CanDelete = SecurityContext.IsManager || SecurityContext.IsAdmin
                    }).ToList();

                var order = CustomConvert.ToInt32(this.sortByDropDownList.SelectedValue);

                switch (order)
                {
                    case 1:
                        result = result.OrderBy(x => x.CreatedAt).ToList();
                        break;
                    case 2:
                        result = result.OrderByDescending(x => x.CreatedByName).ToList();
                        break;
                    case 3:
                        result = result.OrderBy(x => x.CreatedByName).ToList();
                        break;
                    default:
                        result = result.OrderByDescending(x => x.CreatedAt).ToList();
                        break;
                }

                return result.ToList<object>();
            }
            catch (Exception exception)
            {
                //handle exception
                return null;
            }
        }
    }
}