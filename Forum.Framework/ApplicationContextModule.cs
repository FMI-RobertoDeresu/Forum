using Autofac;
using Autofac.Integration.Web;
using Forum.Domain.Contracts;
using Forum.Domain.Exceptions;
using Forum.Framework.Repositorires;
using System;
using System.Web;
using System.Web.Security;

namespace Forum.Framework
{
    public class ApplicationContextModule : IHttpModule
    {
        public void Init(HttpApplication httpApplication)
        {
            httpApplication.PostAuthenticateRequest += new EventHandler(OnPostAuthenticateRequest);
            httpApplication.Error += new EventHandler(OnError);
        }

        private void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;

            IApplicationContext applicationContext;
            ISecurityContext securityContext;
            IUserRepository userRepository;

            if (httpApplication.Request.IsAuthenticated)
            {
                var containerProviderAccessor = (IContainerProviderAccessor)httpApplication;
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
            var httpApplication = (HttpApplication)sender;

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

        public void Dispose() { }
    }
}