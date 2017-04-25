using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.IO;

public partial class Crew_CrewDetails_MaintenanceFeedback : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
        {
            lblMsg.Text = "Session Expired!! Log-out and log-in again.";
        }
        else
        {
            if (!IsPostBack)
            {
                UserAccessValidation();
                int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                string Mode = Request.QueryString["Mode"];
                if (Mode == "ADD")
                {
                    if (objUA.Add == 1)
                    {
                        pnlAddFeedback.Visible = true;
                        //Load_CrewVoyages(CrewID);
                    }
                }
                else
                {
                    if (objUA.View == 1)
                    {
                        pnlViewFeedbacks.Visible = true;
                        Load_CrewMaintenanceFeedbacks(CrewID);
                    }
                }
            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            
        }
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
        //-- MANNING OFFICE LOGIN --
       
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    //protected void Load_CrewVoyages(int CrewID)
    //{
    //    DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID);
    //    ddlVoyages.DataSource = dt;
    //    ddlVoyages.DataBind();
    //}
    protected void Load_CrewMaintenanceFeedbacks(int CrewID)
    {
        
        DataTable dt = objBLLCrew.Get_CrewMaintenanceFeedback(CrewID, int.Parse(Session["USERID"].ToString()));

        GridView_CrewRemarks.DataSource = dt;
        GridView_CrewRemarks.DataBind();
    }
    protected void btnSaveRemarks_Click(object sender, EventArgs e)
    {
        try
        {
            int VID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);
            int WLID = UDFLib.ConvertToInteger(Request.QueryString["WLID"]);
            int OFFID = UDFLib.ConvertToInteger(Request.QueryString["OFFID"]);
            int JID = UDFLib.ConvertToInteger(Request.QueryString["JID"]);
            int VoyageID = UDFLib.ConvertToInteger(Request.QueryString["voygeid"]);
            int JHID = UDFLib.ConvertToInteger(Request.QueryString["JHID"]);

            int Job_Type = (WLID >0)?1:2;


            if (txtCrewRemarks.Text != "")
            {
                string FileName = "";
                string Guid_File_Attach = "";
                if (CrewRemarks_FileUploader.HasFile)
                {
                    FileName = Path.GetFileName(CrewRemarks_FileUploader.FileName);
                    Guid GUID = Guid.NewGuid();

                    Guid_File_Attach =  GUID.ToString() + Path.GetExtension(CrewRemarks_FileUploader.FileName);

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + Guid_File_Attach;// FileName;
                    CrewRemarks_FileUploader.SaveAs(strPath);
                }
                lblMsg.Text = "";


                objBLLCrew.INS_Crew_Maintenance_Feedback(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]), txtCrewRemarks.Text, Guid_File_Attach, GetSessionUserID(), VoyageID, Job_Type, VID, WLID, OFFID, JID, JHID);
                
                lblMsg.Text = "Feedback Saved.";
                
                txtCrewRemarks.Text = "";

                string js = "alert('Maintenance feedback saved for the staff.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "saveMessage", js, true);


                //string js = "parent.GetCrewMaintenanceFeedback(" + Request.QueryString["CrewID"].ToString() + ");";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
                lblMsg.Text = "Please enter your Feedback and Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void btnSaveAndCloseRemarks_Click(object sender, EventArgs e)
    {
        btnSaveRemarks_Click(null, null);
     
        //string js = "parent.GetCrewFeedback(" + Request.QueryString["CrewID"].ToString() + ");parent.hideModal('dvPopupFrame');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);

        string js = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);

    }
}