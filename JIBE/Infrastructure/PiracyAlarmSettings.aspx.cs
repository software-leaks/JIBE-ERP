using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class DPLMap_PiracyAlarmSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_Piracy_Alarms();
        }
        else
        {
            string js = "initStartupScripts();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsInit", js, true);
            
        }
    }
    private int GetSessionUserID()
    {
        return UDFLib.ConvertToInteger(Session["USERID"]);
    }
    protected void UserAccessValidation()
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }
        if (objUA.Add == 0)
        {
            btnSaveStatus.Enabled = false;
            lblNoAccess.Visible = true;
        }
        if (objUA.Edit == 0)
        {
            btnSaveStatus.Enabled = false;
            lblNoAccess.Visible = true;
        }
        if (objUA.Delete == 0)
        {
            
        }
        if (objUA.Approve == 0)
        {

        }
        


    }
    protected void Load_Piracy_Alarms()
    {
        DataTable dt = BLL_OPS_DPL.Get_Piracy_Alarms();

        GridView_PiracyAlarm.DataSource = dt;
        GridView_PiracyAlarm.DataBind();
    }


    protected void GridView_PiracyAlarm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Alarm_Status")
        {
            hdnVessel_ID.Value =  e.CommandArgument.ToString().Split(',')[0];
            string Current_Status = e.CommandArgument.ToString().Split(',')[1];
            imgNewStatus.ImageUrl = (Current_Status == "1") ? "../images/off.png" : "../images/on.png";

            string js = "showModal('dvRemarks');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);

        }
    }

    protected void btnSaveStatus_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        string Remarks = txtRemarks.Text;
        if (Remarks == "")
        {
            string js = "alert('Please enter remark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js2", js, true);
        }
        else
        {
            int Res = BLL_OPS_DPL.Toggle_Piracy_Alarm_Status(Vessel_ID, GetSessionUserID(), Remarks);
            Load_Piracy_Alarms();

            string js = "hideModal('dvRemarks');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js3", js, true);
        }
    }
}