using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.FAQ;
using System.Configuration;
using System.Data;

public partial class SiteMaster : System.Web.UI.MasterPage
{

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind(); //Used to resolve the javascript file references in master page <Head>.
        if (!IsPostBack)
        {
            this.imgReportIssue.ImageUrl = "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/images/close.png";
            this.ImageButton1.ImageUrl = "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/images/Cancel.png";
            SetUserDateFormat();
        }

        if (Session["User_DateFormat"]!=null)
            hdnDateFromatMasterPage.Value = Convert.ToString(Session["User_DateFormat"]);

       
        Label lblUserSetting = HeadLoginView.FindControl("userSetting") as Label;
        if (lblUserSetting != null)
        {
            if (Session["USERID"] != null)
                lblUserSetting.Visible = true;
            else
                lblUserSetting.Visible = false;
        }
       

        string APP_NAME = ConfigurationManager.AppSettings["APP_NAME"].ToString();
        DynamicLink.Href = "/" + APP_NAME + "/Styles/" + Convert.ToString(Session["USERSTYLE"]);


        string JsModalpopup = "/" + APP_NAME + "/Scripts/ReportIssuePopup.js";
        Literal scriptModalpopup = new Literal();
        scriptModalpopup.Text = string.Format(@"<script src=""{0}"" type=""text/javascript""></script>", JsModalpopup);
        Page.Header.Controls.Add(scriptModalpopup);

        var scriptManager = ScriptManager.GetCurrent(Page);
        if (scriptManager == null) return;
        scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/html2canvas.js" });

        var scriptManager1 = ScriptManager.GetCurrent(Page);
        if (scriptManager1 == null) return;
        scriptManager1.Scripts.Add(new ScriptReference { Path = "~/Scripts/HelpFile.js" });

        hdfuserDefaultTheme.Value = Convert.ToString(Session["USERSTYLE"]);

        UserAccessValidation();

        AssignSPMModuleID();

        var scriptManager2 = ScriptManager.GetCurrent(Page);
        if (scriptManager2 == null) return;
        scriptManager2.Scripts.Add(new ScriptReference { Path = "~/Scripts/StaffInfo.js" });

        //string msg1 = String.Format("StaffInfo();");
        string msg1 = String.Format("try{{StaffInfo();}}catch(exp){{}}");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
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
        if (CurrentUserID > 0)
        {
            HeadLoginView.Visible = true;

            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
            if (objUA.View == 0 && objUA.Menu_Code > 0)
                Response.Redirect("~/default.aspx?msgid=1");
        }
        else
            HeadLoginView.Visible = false;
    }

    protected void AssignSPMModuleID()
    {

        // Get the Absolute path of the url
        System.IO.FileInfo oInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);

        //if (uc_Report_Issue!=null)
        //{

        //    // Get the Page Name
        //    SqlDataReader dr = BLL_Infra_Common.Get_SPM_Module_ID(oInfo.Name);

        //    uc_Report_Issue.ModuleID = "13";
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        string ModuleID = dr["SPM_Module_ID"].ToString();
        //        //Assign the module ID into the Feeb back button.

        //        /* if there is ModuleID is Empty it means there is no  value of  SPM_Module_ID  coloumn in Lib_Menu table
        //            In this case Feedback would be record under 'COMMON' module ID = 13  under SPM bug traker. 
        //         */
        //        if (ModuleID != "")
        //            uc_Report_Issue.ModuleID = ModuleID;
        //        else
        //            uc_Report_Issue.ModuleID = "13";
        //    }
        //}

    }

    protected void LogoutMe(object sender, EventArgs e)
    {
        if (Session["USERID"] != null)
        {
            BLL_Infra_UserCredentials objBLL = new BLL_Infra_UserCredentials();
            try
            {
                objBLL.End_Session(int.Parse(Session["USERID"].ToString()));
            }
            catch { }
            finally { objBLL = null; }
        }
        FormsAuthentication.SignOut();
        Session.RemoveAll();
        Session.Abandon();
    }

    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        //string js = "alert('" + e.Exception.Message + "');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "masterpageexpmsg", js, true);
    }

    /// <summary>
    /// Description: This method is used to set the preselected date format in User settings pop-up.
    /// <summary> 
    protected void SetUserDateFormat()
    {
        try
        {
            if (Session["User_DateFormat"] != null)
            {
               RadioButtonList Rbl =  HeadLoginView.FindControl("rbtnDateFormat") as RadioButtonList;
               if (Rbl != null)
               {
                   if (Rbl.Items.FindByValue(Session["User_DateFormat"].ToString()) != null)
                   {
                       Rbl.SelectedValue = Session["User_DateFormat"].ToString() != "" ? Session["User_DateFormat"].ToString() : "0";
                   }
               }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}
