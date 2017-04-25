using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;

public partial class CrewQuery_CrewQuery_Index : System.Web.UI.Page
{

    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        if (!IsPostBack)
        {            
            Load_FleetList();
            Load_VesselList();
            Load_CrewQuery_Types();
            Load_Crew_Queries();
        }
        string js = "initScripts();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript_", js, true);
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
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

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

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_Crew_Queries();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Crew_Queries();
    }

    protected void ddlQueryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Crew_Queries();
    }

    protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Crew_Queries();
    }

    protected void txtFilterText_OnTextChanged(object sender, EventArgs e)
    {
        Load_Crew_Queries();
    }

    protected void Load_Crew_Queries()
    {
        int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int Query_Type = UDFLib.ConvertToInteger(ddlQueryType.SelectedValue);
        int Status = UDFLib.ConvertToInteger(rdoStatus.SelectedValue);
        int PAGE_SIZE = ucCustomPager_CrewQueries.PageSize;
        int PAGE_INDEX = ucCustomPager_CrewQueries.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager_CrewQueries.isCountRecord;
        string SortBy = (ViewState["SORTBY"] == null) ? null : (ViewState["SORTBY"].ToString());
        int? SortDirection = null;
        if (ViewState["SORTDIRECTION"] != null) SortDirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Crew_Queries.Get_CrewQueries(FleetCode, Vessel_ID, 0, Query_Type, Status, txtFilterText.Text, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, SortBy, SortDirection);
        ddlQueryType.DataSource = dt;

        GridView_CrewQueries.DataSource = dt;
        GridView_CrewQueries.DataBind();

        if (ucCustomPager_CrewQueries.isCountRecord == 1)
        {
            ucCustomPager_CrewQueries.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewQueries.BuildPager();
        }
    }

    protected void Load_CrewQuery_Types()
    {
        DataTable dt = BLL_Crew_Queries.Get_CrewQuery_Types(GetSessionUserID());
        ddlQueryType.DataSource = dt;
        ddlQueryType.DataTextField = "Query_Type";
        ddlQueryType.DataValueField = "ID";
        ddlQueryType.DataBind();

        ddlQueryType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlQueryType.SelectedIndex = 0;

    }

    protected void GridView_CrewQueries_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView_CrewQueries_Sorting(object sender, EventArgs e)
    {

    }

    protected void btnSaveFollowUp_Click(object sender, EventArgs e)
    {
        int QueryID = UDFLib.ConvertToInteger(HiddenField_QueryID.Value);
        int Vessel_ID = UDFLib.ConvertToInteger(HiddenField_Vessel_ID.Value);
        //int Send_To_Ship = chkSendToShip.Checked == true?1:0;

        int Res = BLL_Crew_Queries.INSERT_CrewQuery_FollowUp(QueryID, Vessel_ID, txtFollowUp.Text, 0, GetSessionUserID());
        if (Res > 0)
        {
            string js = "alert('FollowUp added!!)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFollowup", js, true);

            txtFollowUp.Text = "";
            Load_Crew_Queries();
        }
    }
}