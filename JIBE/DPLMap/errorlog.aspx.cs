using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

public partial class errorlog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        litError.Text = Session["errorinfo"].ToString();
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    StringBuilder strError = new StringBuilder();
    //    //strError.Append("09%2704N ");

    //    string ss = "09'04N";

    //    string req = ss.Replace("'", "%27");

    //   // string perf_str=HttpUtility.HtmlEncode(strError.ToString());
    //    //string obj_err = strError.ToString();

    //    //int indx_garbage = obj_err.IndexOf("'");

    //    //Response.Write(indx_garbage.ToString());
    //    this.Page.RegisterStartupScript("onclick", "<script language='javascript' type='text/javascript'>createPopUpwindow_err('" + req + "');</script>");
    //}
}
