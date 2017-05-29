using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Web;
using Forum.Domain.Contracts;
using Forum.Domain.Exceptions;
using Forum.Framework.Repositorires;

namespace Forum.Framework
{
    public class ApplicationContextModule : IHttpModule
    {
        public void Init(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += OnBeginRequest;
            httpApplication.PostAuthenticateRequest += OnPostAuthenticateRequest;
            httpApplication.Error += OnError;
        }

        public void Dispose() { }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication) sender;
            var url = httpApplication.Context.Request.Url;
            var requireHttps = bool.Parse(ConfigurationManager.AppSettings["requireHttps"]);

            if (url.Scheme == "http" && requireHttps)
            {
                httpApplication.Context.Response.RedirectPermanent(url.ToString().Replace("http", "https"));
                httpApplication.Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication) sender;

            IApplicationContext applicationContext;
            ISecurityContext securityContext;
            IUserRepository userRepository;

            if (httpApplication.Request.IsAuthenticated)
            {
                var containerProviderAccessor = (IContainerProviderAccessor) httpApplication;
                var containerProvider = containerProviderAccessor.ContainerProvider;

                userRepository = containerProvider.RequestLifetime.Resolve<IUserRepository>();

                var ticket = ((FormsIdentity) httpApplication.User.Identity).Ticket;
                var user = userRepository.Get(int.Parse(ticket.UserData));

                securityContext = new SecurityContext(user);
            }
            else
            {
                securityContext = new SecurityContext(null);
            }

            applicationContext = new ApplicationContext(securityContext);

            httpApplication.Context.Items.Add(Environment.SecurityContextKey, securityContext);
            httpApplication.Context.Items.Add(Environment.ApplicationContextKey, applicationContext);
        }

        private void OnError(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication) sender;

            //handle exception
            if (httpApplication.Server.GetLastError().InnerException?.GetType() == typeof(NotAuthorizedException))
            {
                httpApplication.Server.ClearError();
                httpApplication.Response.RedirectToRoute("NotAuthorized", null);
            }
            else
            {
                httpApplication.Server.ClearError();
                httpApplication.Response.RedirectToRoute("Error", null);
            }
        }
    }
}