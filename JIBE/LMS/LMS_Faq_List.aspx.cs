using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.LMS;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class LMS_LMS_Faq_List : System.Web.UI.Page
{
    public int UserAccess_Edit = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (UDFLib.ConvertToInteger(Session["USERID"]) == 0)
            Response.Redirect("../Account/Login.aspx");

        if (!IsPostBack)
        {
            hdf_User_ID.Value = Session["USERID"].ToString();
            hdnEdit.Value = UserAccess_Edit.ToString();
            ucAsyncPager1.BindMethodName = "asyncBindFAQListFaq";
            string URL = "asyncBindFAQListFaq(" + UserAccess_Edit + ");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyLoad", URL, true);
        }
    }
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        int CurrentUserID = UDFLib.ConvertToInteger(Session["UserID"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");

        }
        if (objUA.Add == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "disableAdd", "$('#btnAddNewFAQ').hide();", true);
        }
        if (objUA.Edit == 0)
        {
            UserAccess_Edit = 0;
        }

    }
    public void ExportToExcel(ref string html, string fileName)
    {
        html = html.Replace("&gt;", ">");
        html = html.Replace("&lt;", "<");
       //string ab = Request.Url.Host;

        string webpath = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString();
        //string webpath = Request.Url.GetLeftPart(UriPartial.Authority)+"/SMSLog";
        string localpath   = Server.MapPath("~/");
        string correctlocalpath = localpath.Replace(@"\", "/");
        html.Replace("&lt;", "<");
        html = html.Replace("src=\"/SMSLog", "src=\"" + webpath);
        html = html.Replace("src=\"..\\", "src=\"" + webpath);

        //html = html.Replace("<img src=\"..\\Images\\edit.gif\;", "<");
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        //
       // string imgpath = Server.MapPath("~/Uploads/FAQ/FAQ_4177bf6a-c2f3-4607-a919-ad6bdfc32b32.jpg");
        //String imagepath = "<img src='" + imgpath + "' width='70' height='80'/>";
        //
        //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(filePath);
        //string imagepath = string.Format("<img src='{0}' width='{1}' height='{2}'/>", filePath, imgPhoto.Width, imgPhoto.Height);
        HttpContext.Current.Response.Write(html);
       // HttpContext.Current.Response.Output.Write("<table width='800' align='center' style='text-align:center'");
     //   HttpContext.Current.Response.Output.Write("<tr><td colspan='10' align='center'><div align='center' style='text-align:center'>" + imagepath + "</div></td></tr>");
        HttpContext.Current.Response.End();
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        string html = HdnValue.Value;
        ExportToExcel(ref html, "FaqList");
              
    }
}


