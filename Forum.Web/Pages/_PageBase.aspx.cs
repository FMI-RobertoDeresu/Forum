using Autofac.Integration.Web.Forms;
using Forum.Domain.Contracts;
using Forum.Domain.Exceptions;
using Forum.Service.Contracts.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace Forum.Web.Pages
{
    [InjectProperties]
    public abstract partial class PageBase : Page
    {
        public ISecurityContext SecurityContext { get; set; }

        public IImageService ImageService { get; set; }

        public string GenericErrorMessage { get { return "A aparut o eroare in timpul procesarii!!"; } }

        public string JpegToBase64(string address)
        {
            if (!string.IsNullOrEmpty(address) && File.Exists(address))
                return ImageService.JpegToBase64(Image.FromFile(address));
            else
                return string.Empty;
        }

        protected void Authorize()
        {
            Authorize(string.Empty);
        }

        protected void Authorize(string roles)
        {
            if (!SecurityContext.IsAuthenticated)
                throw new NotAuthorizedException();

            if (!string.IsNullOrEmpty(roles))
            {
                var rolesList = roles.Split(new char[] { ',' })
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x.Trim())
                    .ToList();

                if (rolesList.Any() && !rolesList.Any(x => SecurityContext.HasRole(x)))
                    throw new NotAuthorizedException();
            }
        }
    }
}