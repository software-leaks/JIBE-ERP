using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.Technical;
using SMS.Business.Crew;
using System.IO;
using AjaxControlToolkit4;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Technical_Worklist_ConvertAudioToText : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    // BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UserAccessValidation();

            BindgvAttachments();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
           
        }
        if (objUA.Edit == 0)
        {
           
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }

    }
    public void BindgvAttachments()
    {

        DataTable dt = objBLL.Get_Worklist_Audio_Files();

        gvAttachments.DataSource = dt;
        gvAttachments.DataBind();
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        BindgvAttachments();
    }
}