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
using SMS.Business.OPS;

public partial class Operations_DailyReportIndex_Chem : System.Web.UI.Page
{
    //public static DataTable dtVesselROB;
    //public static DataTable dtCPROB;

    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExport);
        if (!IsPostBack)
        {
            UserAccessValidation();



            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
            ddllocation.DataSource = objVessel.GetVesselLocations();
            ddllocation.DataTextField = "location_name";
            ddllocation.DataValueField = "location_code";
            ddllocation.DataBind();
            ListItem allloc = new ListItem("SELECT ALL", "0");
            ddllocation.Items.Insert(0, allloc);
            if (Session["Vessel_ID"] != null)
            {
                ddlvessel.SelectedValue = Session["Vessel_ID"].ToString();
                ViewState["iVesselID"] = ddlvessel.SelectedValue;
                Session["Vessel_ID"] = null;
            }
            else
            {
                ViewState["iVesselID"] = ddlvessel.SelectedValue;
            }

            ViewState["sReportType"] = "NDAX";

            ViewState["iLocationID"] = "0";
            ViewState["sFromDT"] = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            ViewState["sToDT"] = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["iFleetID"] = DDLFleet.SelectedValue;


            txtfrom.Text = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            txtto.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //dtCPROB = BLL_OPS_VoyageReports.OPS_Get_RecentCPROB();
            //dtVesselROB = BLL_OPS_VoyageReports.Get_VoyageReportIndex("NDA", 0, 0, "1800/01/01", "1900/01/01", 0);

            BindItems();
        }

        // Bind("NDA",0,0);

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

        int Record_count = 1;

        gvVoyageReport.DataSource = BLL_AXSG_OPS_VoyageReport_Chem.Get_DailyVoyageReportIndex_Chem(ViewState["sReportType"].ToString(), Convert.ToInt32(ViewState["iVesselID"]), Convert.ToInt32(ViewState["iLocationID"]), sFromDate, sToDate, Convert.ToInt32(ViewState["iFleetID"]),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize), ref Record_count, UDFLib.ConvertStringToNull(ViewState["Sort_Column"]), UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]));

        gvVoyageReport.DataBind();

        ucCustomPagerItems.CountTotalRec = Record_count.ToString();
        ucCustomPagerItems.BuildPager();

    }
    protected void reportType_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        // Bind(btn.CommandArgument,Convert.ToInt32( ddlvessel.SelectedValue),Convert.ToInt32( ddllocation.SelectedValue));
        ViewState["sReportType"] = btn.CommandArgument;
        ViewState["iVesselID"] = Convert.ToInt32(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iLocationID"] = Convert.ToInt32(ddllocation.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;

        BindItems();
    }
    protected void btnclearall_Click(object sender, EventArgs e)
    {
        // Bind("NDA", 0, 0);
        ViewState["sReportType"] = "NDAX";
        ViewState["iVesselID"] = 0;
        ViewState["iLocationID"] = 0;
        ViewState["iFleetID"] = 0;
        ViewState["sFromDT"] = "";
        ViewState["sToDT"] = "";
        //txtto.Text = "";
        //txtfrom.Text = "";
        ViewState["Sort_Column"] = "";
        ViewState["Sort_Direction"] = "";
        ddllocation.SelectedIndex = 0;

        txtfrom.Text = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
        txtto.Text = DateTime.Now.ToString("dd/MM/yyyy");

        BindFleetDLL();
        BindVesselDDL();

        BindItems();



    }
    protected void gvVoyageReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string TelegramType = DataBinder.Eval(e.Row.DataItem, "TELEGRAM_TYPE").ToString();
                string wind = DataBinder.Eval(e.Row.DataItem, "WIND_FORCE").ToString();
                string instructedspeed = DataBinder.Eval(e.Row.DataItem, "INSTRUCTED_SPEED").ToString();
                string averagespeed = DataBinder.Eval(e.Row.DataItem, "AVERAGE_SPEED").ToString();
                string stractualHO = DataBinder.Eval(e.Row.DataItem, "ACTUAL_HO_CONSMP").ToString();
                string stractualDO = DataBinder.Eval(e.Row.DataItem, "ACTUAL_DO_CONSMP").ToString();
                string strcpHO = DataBinder.Eval(e.Row.DataItem, "CP_HOCONS").ToString();
                string strcpDO = DataBinder.Eval(e.Row.DataItem, "CP_DOCONS").ToString();
                string strVesselID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();

                #region Format cells
                if (TelegramType.ToUpper() == "N")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                }
                else if (TelegramType.ToUpper() == "D")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Orange;
                }
                else if (TelegramType.ToUpper() == "A")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.YellowGreen;
                }
                else if (TelegramType.ToUpper() == "X")
                {
                    //e.Row.Cells[3].CssClass = "PurpleFinder-css";
                    //e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.SkyBlue;
                }

                double WindForce = 0.0;
                if (double.TryParse(wind, out WindForce))
                {
                    if (WindForce >= 5)
                    {
                        e.Row.Cells[8].CssClass = "RedCell";

                    }
                }

                double AvgSpeed = 0.0;
                double InsSpeed = 0.0;

                if (double.TryParse(averagespeed, out AvgSpeed) && double.TryParse(instructedspeed, out InsSpeed))
                {

                    if (AvgSpeed < InsSpeed)
                    {
                        e.Row.Cells[10].CssClass = "RedCell";
                    }

                }

                double ActualHO = 0.0;
                double CPHO = 0.0;

                if (double.TryParse(stractualHO, out ActualHO) && double.TryParse(strcpHO, out CPHO))
                {

                    if (ActualHO > CPHO)
                    {
                        e.Row.Cells[12].CssClass = "RedCell";
                    }

                }


                double ActualDO = 0.0;
                double CPDO = 0.0;

                if (double.TryParse(stractualDO, out ActualDO) && double.TryParse(strcpDO, out CPDO))
                {

                    if (ActualDO > CPDO)
                    {
                        e.Row.Cells[14].CssClass = "RedCell";
                    }

                }

                #endregion

                string strTelegramID = DataBinder.Eval(e.Row.DataItem, "TELEGRAM_ID").ToString();

                string strtype = DataBinder.Eval(e.Row.DataItem, "Telegram_Type").ToString();

                for (int ix = 1; ix < e.Row.Cells.Count - 1; ix++)
                {

                    string filters = Convert.ToString(ViewState["sReportType"]) + "~" + Convert.ToString(ViewState["iVesselID"]) + "~" + Convert.ToString(ViewState["iLocationID"]) + "~" + Convert.ToString(ViewState["sFromDT"]) + "~" + Convert.ToString(ViewState["sToDT"]) + "~" + Convert.ToString(ViewState["iFleetID"]);

                    e.Row.Cells[ix].Attributes.Add("onclick", "showdetails('" + strTelegramID + "," + strVesselID + "," + strtype + "," + filters + "')");

                    e.Row.Cells[ix].Attributes.Add("style", "cursor:pointer");
                }

                #region check for low ROB

                bool stsLow = false;

                string strCPFlt = "Vessel_ID=" + strVesselID + " AND datatype like '%ROB%'";
                string strVSLflt = "Vessel_ID=" + strVesselID + " AND TELEGRAM_ID=" + strTelegramID;


                // DataRow[] drVSLROB = dtVesselROB.Select(strVSLflt);
                // DataRow[] drCPROB = dtCPROB.Select(strCPFlt);

                //foreach (DataRow dr in drCPROB)
                //{
                //    string DataCode = dr["Data_Code"].ToString().ToUpper().Trim();
                //    decimal datavalue = decimal.Parse(dr["data_value"].ToString());

                //    if (DataCode == "HO_ROB")
                //    {
                //        if (decimal.Parse(drVSLROB[0]["HO_ROB"].ToString()) < datavalue)
                //        {
                //            stsLow = true;
                //            break;
                //        }
                //    }
                //    else if (DataCode == "DO_ROB")
                //    {
                //        if (decimal.Parse(drVSLROB[0]["DO_ROB"].ToString()) < datavalue)
                //        {
                //            stsLow = true;
                //            break;
                //        }

                //    }

                //    else if (DataCode == "AECC_ROB")
                //    {
                //        if (decimal.Parse(drVSLROB[0]["AECC_ROB"].ToString()) < datavalue)
                //        {
                //            stsLow = true;
                //            break;
                //        }

                //    }
                //    else if (DataCode == "MECC_ROB")
                //    {
                //        if (decimal.Parse(drVSLROB[0]["MECC_ROB"].ToString()) < datavalue)
                //        {
                //            stsLow = true;
                //            break;
                //        }

                //    }

                //    else if (DataCode == "MECYL_ROB")
                //    {
                //        if (decimal.Parse(drVSLROB[0]["MECYL_ROB"].ToString()) < datavalue)
                //        {
                //            stsLow = true;
                //            break;
                //        }

                //    }
                //    else if (DataCode == "FW_ROB")
                //    {
                //        if (decimal.Parse(drVSLROB[0]["FW_ROB"].ToString()) < datavalue)
                //        {
                //            stsLow = true;
                //            break;
                //        }

                //    }




                //}

                if (stsLow == true)
                {
                    ((Label)e.Row.Cells[16].FindControl("lblLowrob")).Text = "YES";
                    //((Label)e.Row.Cells[14].FindControl("lblLowrob")).CssClass = "RedCell";
                    e.Row.Cells[16].CssClass = "RedCell";
                }

                #endregion

                #region check for FW Cons

                if (TelegramType.ToUpper() == "N")
                {
                    decimal FWCons = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "FW_CONS").ToString());

                    decimal FWCPCons = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "CP_FWCONS").ToString());

                    if (FWCPCons < FWCons)
                    {
                        e.Row.Cells[15].CssClass = "RedCell";

                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
            }
        }
    }


    //protected void gvVoyageReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvVoyageReport.PageIndex = e.NewPageIndex;
    //    gvVoyageReport.DataBind();
    //}
    protected void ObjectDataSourcereport_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["reporttype"] = ViewState["sReportType"].ToString(); ;
        e.InputParameters["vesselid"] = Convert.ToInt32(ViewState["iVesselID"].ToString());
        e.InputParameters["fleetid"] = Convert.ToInt32(ViewState["iFleetID"].ToString());
        e.InputParameters["Page_Size"] = UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex);
        e.InputParameters["Page_Size"] = UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize);

        e.InputParameters["locationcode"] = Convert.ToInt32(ViewState["iLocationID"].ToString());
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
    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["sReportType"] = "NDAX";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iLocationID"] = int.Parse(ddllocation.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["sReportType"] = "NDAX";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iLocationID"] = int.Parse(ddllocation.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        ViewState["sReportType"] = "NDAX";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iLocationID"] = int.Parse(ddllocation.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void txtto_TextChanged(object sender, EventArgs e)
    {
        ViewState["sReportType"] = "NDAX";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iLocationID"] = int.Parse(ddllocation.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();

    }
    protected void gvVoyageReport_Sorted(object sender, EventArgs e)
    {

    }
    protected void gvVoyageReport_Sorting(object sender, GridViewSortEventArgs e)
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

        ddlvessel.Items.Clear();
        DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        ddlvessel.DataSource = dtVessel;
        ddlvessel.DataTextField = "Vessel_name";
        ddlvessel.DataValueField = "Vessel_ID";
        ddlvessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        ddlvessel.Items.Insert(0, li);


        ViewState["sReportType"] = "NDAX";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        ViewState["iLocationID"] = int.Parse(ddllocation.SelectedValue);
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
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


        int Record_count = ucCustomPagerItems.isCountRecord;
        DataTable dtReport = BLL_AXSG_OPS_VoyageReport_Chem.Get_DailyVoyageReportIndex_Chem(ViewState["sReportType"].ToString(), Convert.ToInt32(ViewState["iVesselID"]), Convert.ToInt32(ViewState["iLocationID"]), sFromDate, sToDate, Convert.ToInt32(ViewState["iFleetID"]),
            null,null, ref Record_count, UDFLib.ConvertStringToNull(ViewState["Sort_Column"]), UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]));

        string[] HeaderCaptions = { "Vessel", "Report Date", "Report Type", "Voyage", "Location", "Next Port", "UTC HR", "Average Speed", "HSFO %S ROB", "LSFO %S ROB", "DO %S ROB" };
        string[] DataColumnsName = { "VESSEL_NAME", "TELEGRAM_DATE", "TELEGRAM_TYPE_TEXT", "VOYAGE", "LOCATION_NAME", "NEXT_PORT", "UTC_HR", "AVERAGE_SPEED", "HO_ROB", "LSFO_ROB", "DO_ROB" };

        GridViewExportUtil.ShowExcel(dtReport, HeaderCaptions, DataColumnsName, "VoyageReportList", "Voyage Report List", "");
        
    }
}

