using System;
using System.Web.UI;

namespace Forum.Web.Pages
{
    public partial class _Error : Page
    {
        protected string errorMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                errorMessage = RouteData.Values["error"].ToString();
            }
            catch
            {
                errorMessage = string.Empty;
            }
        }
    }
}