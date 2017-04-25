using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Business.PortageBill;
using System.Data;
using SMS.Properties;

public partial class Crew_CrewDetails_SalaryInstruction : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
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
        if (GetSessionUserID() == 0)
        {
            Response.Write("Session expired!! Please log out and login again");
            Response.End();
        }
        if (!IsPostBack)
        {
            UserAccessValidation();
            int CurrentUserID = GetSessionUserID();
            hdnUserID.Value = CurrentUserID.ToString();

            Load_VesselList();
            Get_LastPBDate();
            if (Request.QueryString["Vessel_ID"] != null)
            {
                ddlVessel.SelectedValue = Request.QueryString["Vessel_ID"].ToString();
                ddlVessel.Enabled = false;
            }
            string Mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString();

            if (Mode == "ADD")
            {
                if (objUA.Add == 1)
                {
                    pnlAddNew.Visible = true;
                }
            }
            else
            {
                if (objUA.View == 1)
                {
                    pnlViewInstructions.Visible = true;
                    Load_SalInstructions();                    
                }
            }
            
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            lblMessage.Text = "You don't have sufficient privilege to access the requested information.";

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
    public int GetVoyageID()
    {
        try
        {
            if (Request.QueryString["VoyID"] != null)
            {
                return int.Parse(Request.QueryString["VoyID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    public void Load_VesselList()
    {
        int Fleet_ID = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = 1;

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int Res = BLL_PB_PortageBill.INS_Crew_Salary_Instruction(GetCrewID(), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), GetVoyageID(), UDFLib.ConvertToInteger(rdoEarndeduction.SelectedValue), txtPBDate.Text, UDFLib.ConvertToInteger(ddlType.SelectedValue), UDFLib.ConvertToDecimal(txtAmount.Text), txtRemarks.Text, GetSessionUserID());
        
        if (Res == 1)
        {
            string js1 = "alert('Salary instruction added for the staff');parent.hideModal('dvPopupFrame');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg11", js1, true);

        }
        
    }

    protected void Get_LastPBDate()
    {
        string dt = BLL_PB_PortageBill.Get_LastPortageBillDate(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));

        txtPBDate.Text = dt;

        if (dt == "")
        {
            DateTime lastDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
            txtPBDate.Text = lastDayOfThisMonth.ToString(Convert.ToString(Session["User_DateFormat"]));
        }
        else
        {
            txtPBDate.Enabled = false;
        }
    }

    protected void rdoEarndeduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlType.Items.Clear();

        if (rdoEarndeduction.SelectedValue == "18")
        {
            ddlType.Items.Add(new ListItem("Office Instructions", "40"));
        }
        else
        {
            ddlType.Items.Add(new ListItem("Office Instructions", "42"));
        }
    }

    protected void Load_SalInstructions()
    {
        int CrewID = GetCrewID();
        int VoyageID = GetVoyageID();

        DataTable dtVoy = objBLLCrew.Get_CrewVoyages(CrewID, VoyageID);
        if (dtVoy.Rows.Count > 0)
        {
            //For Joining Rank = TECH = 33, SUPERNUMERY=34, CMDR= 44, or GRD =45 then dont show the WAGES update icon 
            if (dtVoy.Rows[0]["joining_rank"].ToString() == "33" || dtVoy.Rows[0]["joining_rank"].ToString() == "34" || dtVoy.Rows[0]["joining_rank"].ToString() == "44" || dtVoy.Rows[0]["joining_rank"].ToString() == "45")
            {
                Response.Write("You are not authorised to view this page.");
                Response.End();

            }
            else
            {
                DataTable dt = BLL_PB_PortageBill.Get_Crew_Salary_Instructions(CrewID, VoyageID, GetSessionUserID());

                GridView_SalaryInstructions.DataSource = dt;
                GridView_SalaryInstructions.DataBind();
            }
        }
        
    }
}