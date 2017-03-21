using Autofac.Integration.Web.Forms;
using Forum.Domain.Security;
using Forum.Service.Contracts.Security;
using Forum.Service.Services.Security;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public partial class Auth : PageBase
    {
        public IHashPasswordService HashPasswordService { get; set; }

        public IUserService UserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var action = RouteData.Values["action"].ToString();
                var userName = RouteData.Values["userName"].ToString();

                if (SecurityContext.IsAuthenticated && (action == "signin" || action == "register"))
                {
                    Response.RedirectToRoute("Default", null);
                    return;
                }

                switch (action)
                {
                    case "signin":
                        this.pageActionType.Value = "signin";
                        this.signInButtons.Visible = true;
                        this.username.Text = userName;
                        this.rememberMeWrapper.Visible = true;
                        break;
                    case "register":
                        this.pageActionType.Value = "register";
                        this.registerButtons.Visible = true;
                        this.username.Attributes.Add("autocomplete", "off");
                        this.passwordReWrapper.Visible = true;
                        break;
                    case "signout":
                        using (var formsAuthService = new FormsAuthenticationService(Context))
                        {
                            formsAuthService.SignOut();
                        }
                        Response.RedirectToRoute("Default", null);
                        break;
                    default:
                        Response.RedirectToRoute("NotFound", null);
                        break;
                }

                DataBind();
            }
        }

        protected void signInButton_Click(object sender, EventArgs e)
        {
            if (pageActionType.Value == "signin")
            {
                try
                {
                    var user = UserService.Get(x => x.UserName == this.username.Text);
                    if (user != null && HashPasswordService.ArePasswordsMatching(this.password.Text, user.Password))
                    {
                        using (var formsAuthService = new FormsAuthenticationService(Context))
                        {
                            string returnUrl;

                            formsAuthService.SignIn(user.UserName, rememberMe.Checked, user.Id.ToString(), out returnUrl);

                            if (!File.Exists(user.ProfileImage64Url) || !File.Exists(user.ProfileImage64Url) || !File.Exists(user.ProfileImage128Url))
                            {
                                UserService.SetProfilePicture(user, Image.FromFile(Server.MapPath("/Content/images/default_profile.jpg")));
                                UserService.CommitChanges();
                            }

                            Response.Redirect(returnUrl);
                        }
                    }
                }
                catch (Exception exception)
                {
                    //handle exception
                    this.message.InnerText = GenericErrorMessage;
                    this.message.Visible = true;
                }

                if (!SecurityContext.IsAuthenticated)
                {
                    this.message.InnerText = "Utilizatorul sau parola sunt gresite!";
                    this.message.Visible = true;
                    this.password.Text = string.Empty;
                }
            }
            else
            {
                Response.RedirectToRoute("Auth", new { action = "signin" });
            }
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            if (pageActionType.Value == "register")
            {
                try
                {
                    if (string.IsNullOrEmpty(this.username.Text) || string.IsNullOrEmpty(this.password.Text))
                    {
                        this.message.InnerText = "Introduceti utilizatorul si parola!";
                        this.message.Visible = true;
                    }
                    else if (this.password.Text != this.passwordRe.Text)
                    {
                        this.message.InnerText = "Parolele introduse sunt diferite!";
                        this.message.Visible = true;
                    }
                    else if (UserService.Get(x => x.UserName == this.username.Text) != null)
                    {
                        this.message.InnerText = "Utilizator existent, alegeti alt utilizator!";
                        this.message.Visible = true;
                    }
                    else
                    {
                        var password = HashPasswordService.GeneratePasswordHash(this.password.Text);
                        var user = new User(this.username.Text, password);

                        UserService.Create(user);
                        UserService.SetProfilePicture(user, Image.FromFile(Server.MapPath("/Content/images/default_profile.jpg")));
                        UserService.CommitChanges();
                        Response.RedirectToRoute("Auth", new { action = "signin", userName = user.UserName });
                    }

                    this.password.Text = string.Empty;
                    this.passwordRe.Text = string.Empty;
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
                Response.RedirectToRoute("Auth", new { action = "register" });
            }
        }
    }
}