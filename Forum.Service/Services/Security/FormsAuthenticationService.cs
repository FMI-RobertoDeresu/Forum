using Forum.Framework;
using System;
using System.Web;
using System.Web.Security;

namespace Forum.Service.Services.Security
{
    public class FormsAuthenticationService : Disposable
    {
        private readonly HttpContext _httpContext;

        public FormsAuthenticationService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public void SignIn(string userName, bool isPersistent, string userData, out string returnUrl)
        {
            var ticket = new FormsAuthenticationTicket(2, userName, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent, userData);
            var cookie = FormsAuthentication.GetAuthCookie(userName, isPersistent);

            cookie.Value = FormsAuthentication.Encrypt(ticket);
            _httpContext.Response.SetCookie(cookie);

            returnUrl = FormsAuthentication.GetRedirectUrl(userName, isPersistent);

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = FormsAuthentication.DefaultUrl;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
