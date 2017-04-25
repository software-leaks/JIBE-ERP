using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.Technical;
using SMS.Properties;
using System.Data;


public partial class Crew_CrewCardIndex : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
            else
                UserAccessValidation();

            if (!IsPostBack)
            {
                int CurrentUserID = GetSessionUserID();

                Load_CountryList();
                Load_FleetList();
                Load_VesselList();

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;


                BindCrewCardIndex();
            }
            string js = "$('.vesselinfo').InfoBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        int CurrentUserID = GetSessionUserID();

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Write("You don't have sufficient previledge to access this page.");
            Response.End();
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

    public void Load_CountryList()
    {
        ddlCountry.DataSource = objCrew.Get_CrewNationality(GetSessionUserID());
        ddlCountry.DataTextField = "COUNTRY";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCountry.SelectedIndex = 0;
    }

    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_VesselList();
            BindCrewCardIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void BindCrewCardIndex()
    {  
        int rowcount = ucCustomPagerItems.isCountRecord;
        int? status = null;
        status = UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objCrew.Get_CrewCardIndex(txtSearchText.Text != "" ? txtSearchText.Text : null
              ,UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue) ,UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue)
              , status, GetSessionUserID(), UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }

    protected void Load_CrewCards(object sender, EventArgs e)
    {
        int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int Nationality = UDFLib.ConvertToInteger(ddlCountry.SelectedValue);
        int Status = UDFLib.ConvertToInteger(ddlStatus.SelectedValue);

        //DataTable dt = objCrew.Get_CrewCardIndex(FleetCode, VesselID, Nationality, Status, txtSearchText.Text, GetSessionUserID());

        //GridView1.DataSource = dt;
        //GridView1.DataBind();  

        BindCrewCardIndex();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            Load_CrewCards(null, null);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        ddlVessel.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        txtSearchText.Text = "";
        BindCrewCardIndex();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_VesselList();
            BindCrewCardIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            int? status = null;
            if (ddlStatus.SelectedValue != "2")
                status = UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue);

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objCrew.Get_CrewCardIndex(txtSearchText.Text != "" ? txtSearchText.Text : null
                  , UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue)
                  , status, GetSessionUserID(), UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()), sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);

            foreach (DataRow item in dt.Rows)
            {

                item["CardType"] = item["CardType"].ToString() + " " + item["ApprovalStatus"].ToString();
                item["Date_Of_Creation"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["Date_Of_Creation"]));

            }
            string[] HeaderCaptions = { "Vessel", "Staff Code", "Name", "Rank", "Nation", "Proposed By", "Propose Date", "Proposed Remarks", "Status" };
            string[] DataColumnsName = { "Vessel_Short_Name", "STAFF_CODE", "Staff_FullName", "Rank_short_Name", "ISO_Code", "ProposedBy", "Date_Of_Creation", "ProposedRemarks", "CardType" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Crew CardIndex", "Crew CardIndex", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
   
}