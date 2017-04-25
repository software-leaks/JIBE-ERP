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

public partial class Operations_BunkerReportIndex : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();

            Load_PortList();
            BindFleetDLL();
            BindVesselDDL();

            ViewState["iVesselID"] = ddlvessel.SelectedValue;
            ViewState["iPortID"] = "0";
            ViewState["sFromDT"] = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            ViewState["sToDT"] = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["iFleetID"] = DDLFleet.SelectedValue;

            txtfrom.Text = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            txtto.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
        string sFromDate = "";
        string sToDate = "";
        if (ViewState["sFromDT"].ToString() == "")
        {
            sFromDate = "1900/01/01";
        }
        else
        {
            sFromDate = ViewState["sFromDT"].ToString();
        }
        if (ViewState["sToDT"].ToString() == "")
        {
            sToDate = "1900/01/01";
        }
        else
        {
            sToDate = ViewState["sToDT"].ToString();
        }

        int Record_count = 0;
      
        gvBunkerReport.DataSource = BLL_OPS_VoyageReports.Get_BunkerReportIndex(Convert.ToInt32(ViewState["iVesselID"]), Convert.ToInt32(ViewState["iPortID"]), sFromDate, sToDate, Convert.ToInt32(ViewState["iFleetID"]),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize), ref Record_count, UDFLib.ConvertStringToNull(ViewState["Sort_Column"]), UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]));

        gvBunkerReport.DataBind();

        ucCustomPagerItems.CountTotalRec = Record_count.ToString();
        ucCustomPagerItems.BuildPager();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iPortID"] = int.Parse(DDLPortFilter.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void btnclearall_Click(object sender, EventArgs e)
    {
        ViewState["iVesselID"] = 0;
        ViewState["iPortID"] = Convert.ToInt32(DDLPortFilter.SelectedValue);
        ViewState["iFleetID"] = 0;
        ViewState["sFromDT"] = "";
        ViewState["sToDT"] = "";
        ViewState["Sort_Column"] = "";
        ViewState["Sort_Direction"] = "";

        txtfrom.Text = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
        txtto.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;

        DDLFleet.SelectedIndex = 0;
        DDLPortFilter.SelectedIndex = 0;
        BindVesselDDL();
        BindItems();
    }
 
    protected void ObjectDataSourcereport_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["vesselid"] = Convert.ToInt32(ViewState["iVesselID"].ToString());
        e.InputParameters["fleetid"] = Convert.ToInt32(ViewState["iFleetID"].ToString());
        e.InputParameters["CurrentPageIndex"] = UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex);
        e.InputParameters["Page_Size"] = UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize);
        e.InputParameters["portId"] = Convert.ToInt32(ViewState["iPortID"].ToString());

        if (ViewState["sFromDT"].ToString() == "")
        {
            e.InputParameters["fromdate"] = "1900/01/01";
        }
        else
        {
            e.InputParameters["fromdate"] = ViewState["sFromDT"].ToString();
        }
        if (ViewState["sToDT"].ToString() == "")
        {
            e.InputParameters["todate"] = "1900/01/01";
        }
        else
        {
            e.InputParameters["todate"] = ViewState["sToDT"].ToString();
        }

    }

    protected void ObjectDataSourcereport_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
        }
    }
    protected void gvBunkerReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["Sort_Column"] = e.SortExpression.ToString();

        if (string.IsNullOrWhiteSpace(Convert.ToString(ViewState["Sort_Direction"])))
            ViewState["Sort_Direction"] = "ASC";
        else
        {
            ViewState["Sort_Direction"] = ViewState["Sort_Direction"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        BindItems();
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()); 
        ddlvessel.Items.Clear();
        //DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        DataTable dtVessel = objVsl.Get_VesselList(int.Parse(DDLFleet.SelectedValue.ToString()), 0, Vessel_Manager, "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));
        ddlvessel.DataSource = dtVessel;
        ddlvessel.DataTextField = "Vessel_name";
        ddlvessel.DataValueField = "Vessel_ID";
        ddlvessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        ddlvessel.Items.Insert(0, li);

     }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["sFromDT"].ToString() == "")
        {
            ViewState["sFromDT"] = "1900/01/01";
        }

        if (ViewState["sToDT"].ToString() == "")
        {
            ViewState["sToDT"] = "1900/01/01";
        }

        DataTable dt = BLL_OPS_VoyageReports.Get_BunkerReportIndex(Convert.ToInt32(ViewState["iVesselID"]), Convert.ToInt32(ViewState["iPortID"]), ViewState["sFromDT"].ToString(), ViewState["sToDT"].ToString(), Convert.ToInt32(ViewState["iFleetID"]));
        string[] HeaderCaptions = { "Date", "Voyage", "Port", "Commenced Bunkering", "Completed Bunkering", "Quantity Bunkered", "Grade" };
        string[] DataColumnsNames = { "REPORT_DATE", "VOYAGE", "PORT_NAME", "COMMENCED_BUNKERING", "COMPLETED_BUNKERING", "QUANTITY_BUNKERED_Fuel_Oil", "QUANTITY_BUNKERED_Fuel_Oil_Type" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsNames, "BunkerReport", "BunkerReport","");
    }

    protected void gvBunkerReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        Label lblVesselID = (Label)gvBunkerReport.Rows[se.NewSelectedIndex].FindControl("lblVesselID");
        Label lblBunkerReportId = (Label)gvBunkerReport.Rows[se.NewSelectedIndex].FindControl("lblBunkerReportId");
        string filters = Convert.ToString(ViewState["iVesselID"]) + "~" + Convert.ToString(ViewState["iPortID"]) + "~" + Convert.ToString(ViewState["sFromDT"]) + "~" + Convert.ToString(ViewState["sToDT"]) + "~" + Convert.ToString(ViewState["iFleetID"]);
      
        ResponseHelper.Redirect("../Operations/BunkerReport.aspx?BunkerReportId=" + lblBunkerReportId.Text.Trim() + "&VesselID=" + lblVesselID.Text + "&filters=" + filters, "Blank", "");
    }
}



