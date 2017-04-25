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

public partial class Crew_CrewDetails_EditStatus : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            UserAccessValidation();
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

            if (objUA.View == 1)
            {
                pnlCrewStatus.Visible = true;
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
            pnlCrewStatus.Visible = false;
        }

        if (objUA.Add == 0)
        {
            btnSaveStatus.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            btnSaveStatus.Enabled = false;
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
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["CrewID"] != null)
            {
                return int.Parse(Request.QueryString["CrewID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    protected void btnSaveStatus_Click(object sender, EventArgs e)
    {
        try
        {
            bool iValidate = true;
            int CrewId = GetCrewID();
            int ServiceCount = 0;
            string js = "";
            if (iValidate == true)
            {
                int CrewStatus = objBLLCrew.Get_CrewStatus(CrewId);
                if ( CrewStatus == -1)
                {
                    js = "parent.ShowNotification('Alert','Status can not be changed for NTBR Crew',false);";
                }
                else if (CrewStatus == 0 && ddlCrewStatus.SelectedValue.Equals("INACTIVE"))
                {
                    js = "parent.ShowNotification('Alert','Crew status is already Inactive',false);";
                }
                else if (CrewStatus == 1 && ddlCrewStatus.SelectedValue.Equals("ACTIVE"))
                {
                    js = "parent.ShowNotification('Alert','Crew status is already Active',false);";
                }
                else
                {
                    if (ddlCrewStatus.SelectedValue.Equals("INACTIVE"))
                    {
                        ServiceCount = objBLLCrew.GetCrewServiceStatus(CrewId);
                    }
                    if (ServiceCount == 0)
                    {
                        int ret = objBLLCrew.UPDATE_CrewStatus(GetCrewID(), ddlCrewStatus.SelectedValue, txtCrewStatusChangeRemark.Text, GetSessionUserID());
                        js = "parent.hideModal('dvPopupFrame');parent.ShowNotification('Alert','Status updated for the Crew',true);parent.document.getElementById('ctl00_MainContent_btnRefreshCrewStatus').click();";
                    }
                    else
                    {
                        js = "parent.ShowNotification('Alert','Crew is assigned to Voyage/Service.Signoff the Crew from current Voyage/Service to update status',false);";
                        
                    }
                }
                if (js != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                }
            }
        }
        catch { }
    }
}