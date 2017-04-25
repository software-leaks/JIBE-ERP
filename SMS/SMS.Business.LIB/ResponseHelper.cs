using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ResponseHelper
/// </summary>
public static class ResponseHelper
{

    public static void Redirect(string url, string target, string windowFeatures)
    {

        HttpContext context = HttpContext.Current;



        if ((String.IsNullOrEmpty(target) ||

            target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&

            String.IsNullOrEmpty(windowFeatures))
        {



            context.Response.Redirect(url);

        }

        else
        {

            Page page = (Page)context.Handler;

            if (page == null)
            {

                throw new InvalidOperationException(

                    "Cannot redirect to new window outside Page context.");

            }

            url = page.ResolveClientUrl(url);



            string script;

            if (!String.IsNullOrEmpty(windowFeatures))
            {

                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";

            }

            else
            {

                script = @"window.open(""{0}"", ""{1}"");";

            }



            script = String.Format(script, url, target, windowFeatures);

            ScriptManager.RegisterStartupScript(page, typeof(Page), "register", script, true);

        }

    }

   
}

