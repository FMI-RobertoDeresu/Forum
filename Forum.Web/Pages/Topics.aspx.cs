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
    public partial class Topics : PageBase
    {
        public ISubjectService SubjectService { get; set; }

        public ITopicService TopicService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var navigation = new List<Tuple<string, string>>();
                int subjectId = CustomConvert.ToInt32(RouteData.Values["subjectId"]);
                var subject = SubjectService.Get(subjectId);

                if (subject == null)
                    Response.RedirectToRoute("NotFound", null);
                else
                {
                    navigation.Add(Tuple.Create(GetRouteUrl("Default", null), "Acasa"));
                    navigation.Add(Tuple.Create(GetRouteUrl("SubjectTopics", new { subjectId = subjectId }), subject.Name));
                    Session["Navigation"] = navigation;

                    this.subjectId.Value = subject.Id.ToString();
                    DataBind();
                }
            }
        }

        protected void topicsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (this.topicsListView.EditIndex == e.Item.DataItemIndex)
            {
                var idHidden = (HiddenField)e.Item.FindControl("id");
                var nameTextBox = (TextBox)e.Item.FindControl("name");
                var subjectDropDownList = (DropDownList)e.Item.FindControl("subject");
                var topic = TopicService.Get(int.Parse(idHidden.Value), x => x.CreatedBy);

                nameTextBox.Visible = topic.CreatedBy.Id == SecurityContext.User.Id;

                if (SecurityContext.IsManager)
                {
                    var subjects = SubjectService.GetMany(x => x.Category.Id == topic.Subject.Category.Id)
                        .Select(x => new
                        {
                            Value = x.Id,
                            Text = x.Name
                        })
                        .ToList();

                    subjectDropDownList.DataSource = subjects.ToList<object>();
                    subjectDropDownList.DataValueField = "Value";
                    subjectDropDownList.DataTextField = "Text";
                    subjectDropDownList.SelectedIndex = subjects.IndexOf(subjects.First(x => x.Value == topic.Subject.Id));
                    subjectDropDownList.DataBind();
                    subjectDropDownList.Visible = true;
                }
            }
        }

        protected void topicsListView_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            var topicNameTextBox = this.topicsListView.InsertItem.FindControl("name") as TextBox;

            if (!SecurityContext.IsAuthenticated)
                throw new NotAuthorizedException();

            try
            {
                var subject = SubjectService.Get(CustomConvert.ToInt32(this.subjectId.Value));
                var topic = new Topic(topicNameTextBox.Text, subject, SecurityContext.User);

                TopicService.Create(topic);
                TopicService.CommitChanges();
                Response.RedirectToRoute("SubjectTopics", new { subjectId = this.subjectId.Value });
            }
            catch
            {
                //handle exception
                this.message.InnerText = GenericErrorMessage;
                this.message.Visible = true;
            }
        }

        protected void topicsListView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.topicsListView.EditIndex = e.NewEditIndex;
            this.topicsListView.DataSource = GetTopics();
            this.topicsListView.DataBind();
        }

        protected void topicsListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var topicIdHiddenField = (HiddenField)this.topicsListView.EditItem.FindControl("id");
            var topicNameTextBox = (TextBox)this.topicsListView.EditItem.FindControl("name");
            var subjectDropDownList = (DropDownList)this.topicsListView.EditItem.FindControl("subject");
            var topic = TopicService.Get(CustomConvert.ToInt32(topicIdHiddenField.Value));

            if (topic.CreatedBy != SecurityContext.User && !SecurityContext.IsManager)
                throw new NotAuthorizedException();

            try
            {
                if (topic.CreatedBy == SecurityContext.User)
                    topic.Edit(topicNameTextBox.Text);

                if (SecurityContext.IsManager)
                {
                    var newSubject = SubjectService.Get(int.Parse(subjectDropDownList.SelectedValue));
                    topic.ChangeSubject(newSubject);
                }

                TopicService.Update(topic);
                TopicService.CommitChanges();
                Response.RedirectToRoute("SubjectTopics", new { subjectId = this.subjectId.Value });
            }
            catch
            {
                //handle exception
                this.message.InnerText = GenericErrorMessage;
                this.message.Visible = true;
            }
        }

        protected void topicsListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            Response.RedirectToRoute("SubjectTopics", new { subjectId = this.subjectId.Value });
        }

        protected void topicsListView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var topicIdHiddenField = this.topicsListView.Items[e.ItemIndex].FindControl("id") as HiddenField;
            var topicId = CustomConvert.ToInt32(topicIdHiddenField.Value);
            var topic = TopicService.Get(topicId);

            if (!SecurityContext.IsAdmin && !SecurityContext.IsManager)
                throw new NotAuthorizedException();

            try
            {
                TopicService.Delete(topic);
                TopicService.CommitChanges();
                Response.RedirectToRoute("SubjectTopics", new { subjectId = this.subjectId.Value });
            }
            catch
            {
                //handle exception
                this.message.InnerText = GenericErrorMessage;
                this.message.Visible = true;
            }
        }

        protected object GetTopics()
        {
            try
            {
                var result = SubjectService.Get(CustomConvert.ToInt32(this.subjectId.Value), x => x.Topics.Select(y => y.CreatedBy))?.Topics
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => (object)new
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy,
                        Name = x.Name,
                        NumberOfPosts = x.Posts.Count,
                        PostsUrl = GetRouteUrl("TopicPosts", new { topicId = x.Id }),
                        CanEdit = SecurityContext.IsAuthenticated && (x.CreatedBy == SecurityContext.User || SecurityContext.IsManager),
                        CanDelete = SecurityContext.IsManager || SecurityContext.IsAdmin
                    }).ToList();

                return result;
            }
            catch (Exception exception)
            {
                //handle exception
                return null;
            }
        }
    }
}