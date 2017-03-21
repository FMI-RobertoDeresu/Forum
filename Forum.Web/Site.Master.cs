using Autofac.Integration.Web.Forms;
using Forum.Domain.Contracts;
using Forum.Service.Contracts.Common;
using System;
using System.Drawing;
using System.IO;
using System.Web.UI;

namespace Forum.Web
{
    [InjectProperties]
    public partial class SiteMaster : MasterPage
    {
        public ISecurityContext SecurityContext { get; set; }

        public IImageService ImageService { get; set; }

        protected void Page_Load(object sender, EventArgs e) { }

        protected string JpegToBase64(string address)
        {
            if (!string.IsNullOrEmpty(address) && File.Exists(address))
                return ImageService.JpegToBase64(Image.FromFile(address));
            else
                return string.Empty;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.searchInput.Text))
                Response.RedirectToRoute("Search", new { query = this.searchInput.Text });
        }
    }
}