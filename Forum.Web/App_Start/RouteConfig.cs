using System.Web.Routing;

namespace Forum.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "", "~/Pages/Categories.aspx");

            routes.MapPageRoute("Category", "{categoryId}", "~/Pages/Categories.aspx", false, null,
                new RouteValueDictionary { { "categoryId", "[0-9]+" } });

            routes.MapPageRoute("TopicIndex", "subject/{subjectId}/topics", "~/Pages/Topics.aspx", false, null,
                new RouteValueDictionary { { "subjectId", "[0-9]+" } });

            routes.MapPageRoute("PostIndex", "topic/{topicId}/posts", "~/Pages/Posts.aspx", false, null,
                new RouteValueDictionary { { "topicId", "[0-9]+" } });

            routes.MapPageRoute("Auth", "authenticate/{action}/{userName}", "~/Pages/Auth.aspx", false,
                new RouteValueDictionary() { { "userName", "" } },
                new RouteValueDictionary() { { "action", "signin|register|signout" } });

            routes.MapPageRoute("Profile", "profile/{userId}", "~/Pages/Profile.aspx", false, null,
                new RouteValueDictionary { { "userId", "[0-9]+" } });

            routes.MapPageRoute("ManageUsers", "manageusers", "~/Pages/ManageUsers.aspx");

            routes.MapPageRoute("Search", "search/{query}", "~/Pages/Search.aspx", false, null,
                new RouteValueDictionary { { "query", "^[A-Za-z0-9, ]+$" } });

            routes.MapPageRoute("Error", "error", "~/Pages/_Error.aspx");

            routes.MapPageRoute("NotAuthorized", "notauthorized", "~/Pages/_NotAuthorized.aspx");

            routes.MapPageRoute("NotFound", "{*value}", "~/Pages/_NotFound.aspx");
        }
    }
}