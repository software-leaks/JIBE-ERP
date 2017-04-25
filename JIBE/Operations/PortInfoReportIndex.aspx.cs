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
using SMS.Business.Operation;
using SMS.Business.Infrastructure;
using CrystalDecisions.Shared;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SMS.Properties;
using System.Text;
using SMS.Business.Crew;

public partial class Operations_PortInfoReportIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_PortList();
            Load_CountryList();
            BindFleetDLL();
            BindVesselDDL();

            ViewState["iVesselID"] = ddlvessel.SelectedValue;
            ViewState["iPortID"] = "0";
            ViewState["sFromDT"] = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            ViewState["sToDT"] = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["iFleetID"] = DDLFleet.SelectedValue;

            BindItems();    
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }

    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Write("<center><h2>You do not have sufficient privilege to access to this page.</h2><br><br>Please contact " + Session["Company_Name_GL"] + " </center>");
            Response.End();
        }
    }

    public void Load_PortList()
    {
        BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
        DataTable dt = objBLLPort.Get_PortList_Mini();

        DDLPortFilter.DataSource = dt;
        DDLPortFilter.DataTextField = "Port_Name";
        DDLPortFilter.DataValueField = "Port_ID";
        DDLPortFilter.DataBind();
        DDLPortFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlvessel.Items.Clear();
            ddlvessel.DataSource = dtVessel;
            ddlvessel.DataTextField = "Vessel_name";
            ddlvessel.DataValueField = "Vessel_id";
            ddlvessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlvessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
        }
    }


    protected void BindItems()
    {
        int Record_count = 0;
        ViewState["Sort_Column"] = "";
        ViewState["Sort_Direction"] = "";

        gvPortInfoReportIndex.DataSource = BLL_OPS_VoyageReports.Get_PortInfoReportIndex(
            UDFLib.ConvertToInteger(ddlvessel.SelectedValue),
            UDFLib.ConvertToInteger(DDLPortFilter.SelectedValue), 
            UDFLib.ConvertToInteger(ddlCountry.SelectedValue),
            UDFLib.ConvertStringToNull(txtTerminal.Text),
            UDFLib.ConvertStringToNull(txtSearch.Text),
            UDFLib.ConvertStringToNull(ViewState["Sort_Column"]),
            UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize), ref Record_count);
        gvPortInfoReportIndex.DataBind();

        ucCustomPagerItems.CountTotalRec = Record_count.ToString();
        ucCustomPagerItems.BuildPager();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iPortID"] = int.Parse(DDLPortFilter.SelectedValue);
        BindItems();
    }
    protected void btnclearall_Click(object sender, EventArgs e)
    {
        ViewState["iVesselID"] = 0;
        ViewState["iPortID"] = Convert.ToInt32(DDLPortFilter.SelectedValue);
        ViewState["iFleetID"] = 0;
        ViewState["Sort_Column"] = "";
        ViewState["Sort_Direction"] = "";

        DDLFleet.SelectedIndex = 0;
        DDLPortFilter.SelectedIndex = 0;
        BindVesselDDL();
        BindItems();
    }
    public void Load_CountryList()
    {
        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
        ddlCountry.DataSource = objCrew.Get_CrewNationality(GetSessionUserID());
        ddlCountry.DataTextField = "COUNTRY";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCountry.SelectedIndex = 0;
    }
    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

        ddlvessel.Items.Clear();
        DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        ddlvessel.DataSource = dtVessel;
        ddlvessel.DataTextField = "Vessel_name";
        ddlvessel.DataValueField = "Vessel_ID";
        ddlvessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        ddlvessel.Items.Insert(0, li);
    }
    protected void ExptoExcel_Click(object sender, ImageClickEventArgs e)
    {
        int Record_count = 1;
        DataTable dt = BLL_OPS_VoyageReports.Get_PortInfoReportIndex(
            UDFLib.ConvertToInteger(ddlvessel.SelectedValue),
            UDFLib.ConvertToInteger(DDLPortFilter.SelectedValue),
            UDFLib.ConvertToInteger(ddlCountry.SelectedValue),
            UDFLib.ConvertStringToNull(txtTerminal.Text),
            UDFLib.ConvertStringToNull(txtSearch.Text),
            UDFLib.ConvertStringToNull(ViewState["Sort_Column"]),
            UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize), ref Record_count);
        string[] HeaderCaptions = { "Port", "Country", "Terminal", "Berth", "Latitude", "Longitude", "Vessel" ,"Departure Date"};
        string[] DataColumnsNames = { "Port_Name", "Country_Name", "TerminalName", "Berth", "Latitude", "Longitude", "VesselName" ,"Departure_Date"};

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsNames, "PortInfoReport", "PortInfoReport", "");
    }
   
 
    protected void gvPortInfoReportIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string strRowId = DataBinder.Eval(e.Row.DataItem, "PKID").ToString();
                string strtype = DataBinder.Eval(e.Row.DataItem, "Vessel_Id").ToString();
                string strOffice = DataBinder.Eval(e.Row.DataItem, "Office_Id").ToString();

                for (int ix = 1; ix < e.Row.Cells.Count - 1; ix++)
                {
                    e.Row.Cells[ix].Attributes.Add("onclick", "showdetails('" + strRowId + "," + strtype + "," + strOffice + "')");
                    e.Row.Cells[ix].Attributes.Add("style", "cursor:pointer");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}