using Autofac.Integration.Web.Forms;
using Forum.Service.Contracts.Entity;
using System;
using System.Linq;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public partial class Search : PageBase
    {
        public ICategoryService CategoryService { get; set; }

        public ISubjectService SubjectService { get; set; }

        public ITopicService TopicService { get; set; }

        public IPostService PostService { get; set; }

        private int MaxResultsPerType { get { return 10; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = (string)RouteData.Values["query"];

                if (!string.IsNullOrEmpty(query))
                {
                    var result = CategoryService.GetAll()
                    .Where(x => x.Name.ToUpper().Trim().Contains(query.ToUpper().Trim()))
                    .Take(MaxResultsPerType)
                    .Select(x => new
                    {
                        DsiplayName = "#Categorie " + x.Name,
                        IsHyperlinkResult = true,
                        NavigateUrl = GetRouteUrl("Category", new { categoryId = x.Id }),
                        IsContentResult = false,
                        ContentResult = string.Empty
                    }).Union(SubjectService.GetAll()
                    .Where(x => x.Name.ToUpper().Trim().Contains(query.ToUpper().Trim()))
                    .Take(MaxResultsPerType)
                    .Select(x => new
                    {
                        DsiplayName = "#Subiect " + x.Name,
                        IsHyperlinkResult = true,
                        NavigateUrl = GetRouteUrl("TopicIndex", new { subjectId = x.Id }),
                        IsContentResult = false,
                        ContentResult = string.Empty
                    })).Union(TopicService.GetAll()
                    .Where(x => x.Name.ToUpper().Trim().Contains(query.ToUpper().Trim()))
                    .Take(MaxResultsPerType)
                    .Select(x => new
                    {
                        DsiplayName = "#Topic " + x.Name,
                        IsHyperlinkResult = true,
                        NavigateUrl = GetRouteUrl("PostIndex", new { topicId = x.Id }),
                        IsContentResult = false,
                        ContentResult = string.Empty
                    })).Union(PostService.GetAll()
                    .Where(x => x.Text.ToUpper().Trim().Contains(query.ToUpper().Trim()))
                    .Take(MaxResultsPerType)
                    .Select(x => new
                    {
                        DsiplayName = "#Postare",
                        IsHyperlinkResult = true,
                        NavigateUrl = GetRouteUrl("PostIndex", new { topicId = x.Topic.Id }),
                        IsContentResult = true,
                        ContentResult = x.Text
                    }));

                    this.searchResultListView.DataSource = result;
                    DataBind();
                }
            }
        }
    }
}