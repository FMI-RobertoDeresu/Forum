using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Autofac.Integration.Web.Forms;
using Forum.Domain.Exceptions;
using Forum.Domain.Models;
using Forum.Service.Contracts.Entity;
using Forum.Web.Helpers;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public partial class Categories : PageBase
    {
        public ICategoryService CategoryService { get; set; }

        public ISubjectService SubjectService { get; set; }

        protected int PageSize => 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var navigation = new List<Tuple<string, string>>();
                navigation.Add(Tuple.Create(GetRouteUrl("Default", null), "Acasa"));
                Session["Navigation"] = navigation;

                categoryIdHiddenField.Value = (string) RouteData.Values["categoryId"];

                DataBind();
            }
        }

        //categories list
        protected void categoriesListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var categoryId = CustomConvert.ToInt32(categoriesListView.DataKeys[e.Item.DataItemIndex].Value);
                var subjectsGrid = e.Item.FindControl("subjectsGridView") as GridView;

                if (subjectsGrid != null)
                    SetSubjectsGridData(subjectsGrid, categoryId, 0, PageSize);
            }
        }

        protected void categoriesListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!SecurityContext.IsAdmin)
                throw new NotAuthorizedException();
        }

        protected void categoriesListView_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            var categpryNameTextbox = e.Item.FindControl("name") as TextBox;

            try
            {
                CategoryService.Create(new Category(categpryNameTextbox.Text, SecurityContext.User));
                CategoryService.CommitChanges();
                Response.RedirectToRoute("Default");
            }
            catch (Exception exception)
            {
                //handle exception
                message.InnerText = GenericErrorMessage;
                message.Visible = true;
            }
        }

        protected void categoriesListView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            categoriesListView.EditIndex = e.NewEditIndex;
            categoriesListView.DataSource = GetCategories();
            categoriesListView.DataBind();
        }

        protected void categoriesListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var categoryIdHiddenField = categoriesListView.EditItem.FindControl("id") as HiddenField;
            var categoryNameTextBox = categoriesListView.EditItem.FindControl("name") as TextBox;
            var category = CategoryService.Get(CustomConvert.ToInt32(categoryIdHiddenField.Value));

            if (category != null)
                try
                {
                    category.Edit(categoryNameTextBox.Text);
                    CategoryService.Update(category);
                    CategoryService.CommitChanges();
                    Response.RedirectToRoute("Default", null);
                }
                catch
                {
                    //handle exception
                    message.InnerText = GenericErrorMessage;
                    message.Visible = true;
                }
        }

        protected void categoriesListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            Response.RedirectToRoute("Default", null);
        }

        protected void categoriesListView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var categoryIdHiddenField = categoriesListView.Items[e.ItemIndex].FindControl("id") as HiddenField;
            var categoryId = CustomConvert.ToInt32(categoryIdHiddenField.Value);

            try
            {
                var category = CategoryService.Get(categoryId);

                CategoryService.Delete(category);
                CategoryService.CommitChanges();
                Response.RedirectToRoute("Default", null);
            }
            catch
            {
                //handle exception
                message.InnerText = GenericErrorMessage;
                message.Visible = true;
            }
        }

        //subjects grid
        protected void subjectsGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                foreach (var cell in e.Row.Cells.Cast<TableCell>())
                    cell.Visible = false;

                e.Row.Cells[1].Visible = true;
                e.Row.Cells[1].ColumnSpan = 5;
            }
        }

        protected void subjectsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var subjectsGridView = sender as GridView;
            var categoryIndex = categoriesListView.Items.IndexOf(subjectsGridView.Parent as ListViewDataItem);
            var categoryId = CustomConvert.ToInt32(categoriesListView.DataKeys[categoryIndex].Value);

            try
            {
                subjectsGridView.PageIndex = e.NewPageIndex;
                SetSubjectsGridData(subjectsGridView, categoryId, e.NewPageIndex, PageSize);
            }
            catch
            {
                // handle exception
            }
        }

        protected void showInsertSubjectButton_Click(object sender, EventArgs e)
        {
            var btnShowInsert = sender as LinkButton;
            var categoryIdHiddenField = btnShowInsert.Parent.FindControl("id") as HiddenField;
            var subjectsGridView = btnShowInsert.Parent.FindControl("subjectsGridView") as GridView;
            var categoryId = CustomConvert.ToInt32(categoryIdHiddenField.Value);

            try
            {
                subjectsGridView.ShowFooter = true;
                SetSubjectsGridData(subjectsGridView, categoryId, subjectsGridView.PageIndex, PageSize);
            }
            catch
            {
                // handle exception
            }
        }

        protected void subjectsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower().Equals("page"))
                return;

            if (!SecurityContext.IsAdmin)
                throw new NotAuthorizedException();

            if (e.CommandName == "InsertRow")
            {
                var senderBtn = e.CommandSource as LinkButton;
                var subjectNameTextBox = senderBtn.Parent.FindControl("subjectName") as TextBox;
                var subjectsGridView = senderBtn.Parent.Parent.Parent.Parent.Parent as GridView;
                var categoryIdHiddenField = subjectsGridView.Parent.FindControl("id") as HiddenField;

                try
                {
                    var category = CategoryService.Get(CustomConvert.ToInt32(categoryIdHiddenField.Value));
                    var subject = new Subject(subjectNameTextBox.Text, category, SecurityContext.User);

                    SubjectService.Create(subject);
                    SubjectService.CommitChanges();
                    Response.RedirectToRoute("Default", null);
                }
                catch
                {
                    // handle exception
                }
            }
            else if (e.CommandName == "CancelInsertRow")
            {
                Response.RedirectToRoute("Default", null);
            }
        }

        protected void subjectsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var subjectsGridView = sender as GridView;
            var categoryIdHiddenField = subjectsGridView.Parent.FindControl("id") as HiddenField;
            var categoryId = CustomConvert.ToInt32(categoryIdHiddenField.Value);

            try
            {
                subjectsGridView.EditIndex = e.NewEditIndex;
                SetSubjectsGridData(subjectsGridView, categoryId, subjectsGridView.PageIndex, PageSize);
            }
            catch
            {
                // handle exception
            }
        }

        protected void subjectsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var subjectsGridView = sender as GridView;
            var subjectNameTextBox = subjectsGridView.Rows[e.RowIndex].FindControl("subjectName") as TextBox;
            var subject = SubjectService.Get(CustomConvert.ToInt32(subjectsGridView.DataKeys[e.RowIndex].Value));

            if (subject != null)
                try
                {
                    subject.Edit(subjectNameTextBox.Text);
                    SubjectService.Update(subject);
                    SubjectService.CommitChanges();
                    Response.RedirectToRoute("Default", null);
                }
                catch
                {
                    //handle exception
                    message.InnerText = GenericErrorMessage;
                    message.Visible = true;
                }
        }

        protected void subjectsGridView_RowCanceling(object sender, GridViewCancelEditEventArgs e)
        {
            var subjectsGridView = sender as GridView;
            var categoryIdHiddenField = subjectsGridView.Parent.FindControl("id") as HiddenField;
            var categoryId = CustomConvert.ToInt32(categoryIdHiddenField.Value);


            try
            {
                subjectsGridView.EditIndex = -1;
                SetSubjectsGridData(subjectsGridView, categoryId, subjectsGridView.PageIndex, PageSize);
            }
            catch
            {
                // handle exception
            }
        }

        protected void subjectsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var subjectsGridView = sender as GridView;
            var subjectId = CustomConvert.ToInt32(subjectsGridView.DataKeys[e.RowIndex].Value);

            try
            {
                var subject = SubjectService.Get(subjectId);

                SubjectService.Delete(subject);
                SubjectService.CommitChanges();
                Response.RedirectToRoute("Default", null);
            }
            catch
            {
                //handle exception
                message.InnerText = GenericErrorMessage;
                message.Visible = true;
            }
        }

        //
        protected object GetCategories()
        {
            try
            {
                IEnumerable<Category> result;

                if (!string.IsNullOrEmpty(categoryIdHiddenField.Value))
                {
                    var categoryId = int.Parse(categoryIdHiddenField.Value);
                    result = CategoryService.GetMany(x => x.Id == categoryId, x => x.CreatedBy);
                }
                else
                {
                    result = CategoryService.GetAll(x => x.CreatedBy);
                }

                return result
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(category => (object) new
                    {
                        Id = category.Id,
                        Name = category.Name,
                        CreatedAt = category.CreatedAt,
                        CreatedById = category.CreatedBy?.Id,
                        CreatedByName = category.CreatedBy?.FullName
                    }).ToList();
            }
            catch (Exception exception)
            {
                // handle exception
                return null;
            }
        }

        private void SetSubjectsGridData(GridView subjectsGridView, int categoryId, int pageIndex, int pageSize)
        {
            var category = CategoryService.Get(categoryId,
                x => x.Subjects,
                x => x.Subjects.Select(y => y.CreatedBy),
                x => x.Subjects.Select(y => y.Topics.Select(p => p.Posts.Select(t => t.CreatedBy))));

            subjectsGridView.VirtualItemCount = category.Subjects?.Count ?? 0;
            subjectsGridView.DataSource = category.Subjects?
                .OrderByDescending(x => x.CreatedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(subject => (object) new
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    CreatedAt = subject.CreatedAt,
                    CreatedById = subject.CreatedBy?.Id,
                    CreatedByName = subject.CreatedBy?.FullName,
                    NumberOfTopics = subject.Topics?.Count,
                    NumberOfPosts = subject.Topics?.SelectMany(x => x.Posts).Count(),
                    LastPostCreatedById =
                    subject.Topics?.SelectMany(x => x.Posts).OrderBy(x => x.CreatedAt).LastOrDefault()?.CreatedBy?.Id,
                    LastPostCreatedByName =
                    subject.Topics?.SelectMany(x => x.Posts).OrderBy(x => x.CreatedAt).LastOrDefault()?.CreatedBy?.FullName,
                    LastPostCreatedAt =
                    subject.Topics?.SelectMany(x => x.Posts).OrderBy(x => x.CreatedAt).LastOrDefault()?.CreatedAt.ToString(),
                    LastPostCreatedAtSortFormat =
                    subject.Topics?.SelectMany(x => x.Posts)
                        .OrderBy(x => x.CreatedAt)
                        .LastOrDefault()?.CreatedAt.ToString("yyyy.MM.dd hh:mm:ss")
                })
                .ToList();
            subjectsGridView.DataBind();
        }
    }
}