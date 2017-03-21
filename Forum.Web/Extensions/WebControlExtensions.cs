using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Forum.Web.Extensions
{
    public static class WebControlExtensions
    {
        public static WebControl AddCssClass(this WebControl control, string cssClass)
        {
            control.CssClass = string.Join(" ", control.CssClass
                .Split(' ')
                .Except(new string[] { cssClass, "", " " })
                .Concat(new string[] { cssClass })
                .ToArray());

            return control;
        }

        public static WebControl RemoveClass(this WebControl control, string cssClass)
        {
            control.CssClass = string.Join(" ", control.CssClass
                .Split(' ')
                .Except(new string[] { cssClass, "", " " })
                .ToArray());

            return control;
        }
    }
}