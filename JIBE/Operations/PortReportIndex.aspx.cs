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

public partial class Operations_PortReportIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();



            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
            


       
            ViewState["iVesselID"] = ddlvessel.SelectedValue;
            ViewState["iLocationID"] = "0";
            ViewState["sFromDT"] = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            ViewState["sToDT"] = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["iFleetID"] = DDLFleet.SelectedValue;


            txtfrom.Text = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
            txtto.Text = DateTime.Now.ToString("dd/MM/yyyy");

        

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
        //UserAccess objUA = new UserAccess();
        //BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        //int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        //objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        //if (objUA.View == 0)
        //{
        //    Response.Write("<center><h2>You do not have sufficient privilege to access to this page.</h2><br><br>Please contact " + Session["Company_Name_GL"] + " </center>");
        //    Response.End();
        //}
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

        DateTime? sFromDate ;
        DateTime? sToDate ;
        if (ViewState["sFromDT"].ToString() == "")
        {
            sFromDate = null;
        }
        else
        {
            sFromDate =   UDFLib.ConvertDateToNull(ViewState["sFromDT"]);
        }
        if (ViewState["sToDT"].ToString() == "")
        {
            sToDate = null;
        }
        else
        {
            sToDate =UDFLib.ConvertDateToNull(ViewState["sToDT"]);
        }

        int Record_count = 0;

        gvPortReport.DataSource = BLL_OPS_VoyageReports .Get_PortReportIndex(Convert.ToInt32(ViewState["iVesselID"]),sFromDate, sToDate,UDFLib.ConvertStringToNull(ViewState["Sort_Column"]), UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]), 
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CurrentPageIndex),
            UDFLib.ConvertIntegerToNull(ucCustomPagerItems.PageSize), ref Record_count);

        gvPortReport.DataBind();

        ucCustomPagerItems.CountTotalRec = Record_count.ToString();
        ucCustomPagerItems.BuildPager();

    }
     
    protected void btnclearall_Click(object sender, EventArgs e)
    {
        // Bind("NDA", 0, 0);
        ViewState["sReportType"] = "NDA";
        ViewState["iVesselID"] = 0;
        ViewState["iLocationID"] = 0;
        ViewState["iFleetID"] = 0;
        ViewState["sFromDT"] = "";
        ViewState["sToDT"] = "";
        txtto.Text = "";
        txtfrom.Text = "";
        ViewState["Sort_Column"] = "";
        ViewState["Sort_Direction"] = "";
       


        BindFleetDLL();
        BindVesselDDL();

        BindItems();



    }
    protected void gvPortReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {


                string strVesselID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();

                string strRowId = DataBinder.Eval(e.Row.DataItem, "PortReportID").ToString();

                
                for (int ix = 1; ix < e.Row.Cells.Count - 1; ix++)
                {

                     string filters = Convert.ToString("~" + Convert.ToString(strVesselID));

                     e.Row.Cells[ix].Attributes.Add("onclick", "showdetails('" + strRowId.Split(new char[] { '.' })[0]  + filters + "')");

                     e.Row.Cells[ix].Attributes.Add("style", "cursor:pointer");
                }

                #region check for low ROB

             

         
                string strVSLflt = "Vessel_ID=" + strVesselID + " AND PortReportID=" + strRowId;

 

            
                #endregion

            }
            catch (Exception ex)
            {
            }
        }
    }


    //protected void gvPortReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvPortReport.PageIndex = e.NewPageIndex;
    //    gvPortReport.DataBind();
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

        ViewState["sReportType"] = "NDA";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
       
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["sReportType"] = "NDA";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        ViewState["sReportType"] = "NDA";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void txtto_TextChanged(object sender, EventArgs e)
    {
        ViewState["sReportType"] = "NDA";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
    
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();

    }
    protected void gvPortReport_Sorted(object sender, EventArgs e)
    {

    }
    protected void gvPortReport_Sorting(object sender, GridViewSortEventArgs e)
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
        //DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        ddlvessel.DataSource = dtVessel;
        ddlvessel.DataTextField = "Vessel_name";
        ddlvessel.DataValueField = "Vessel_ID";
        ddlvessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        ddlvessel.Items.Insert(0, li);


        ViewState["sReportType"] = "NDA";
        ViewState["iVesselID"] = int.Parse(ddlvessel.SelectedValue);
        ViewState["iFleetID"] = DDLFleet.SelectedValue;
        
        ViewState["sFromDT"] = txtfrom.Text;
        ViewState["sToDT"] = txtto.Text;
        BindItems();
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        //MemoryStream oStream;

        if (ViewState["sFromDT"].ToString() == "")
        {
            ViewState["sFromDT"] = "1900/01/01";
        }

        if (ViewState["sToDT"].ToString() == "")
        {
            ViewState["sToDT"] = "1900/01/01";
        }

        //ReportDocument reportDocumentObj = new ReportDocument();
        //reportDocumentObj.Load(Server.MapPath("~/operations/CR_VoyageReport.rpt"));

        DataTable dtreport = BLL_OPS_VoyageReports.Get_VoyageReport(ViewState["sReportType"].ToString(), Convert.ToInt32(ViewState["iVesselID"].ToString()), Convert.ToInt32(ViewState["iLocationID"].ToString()), ViewState["sFromDT"].ToString(), ViewState["sToDT"].ToString(), Convert.ToInt32(ViewState["iFleetID"].ToString()));
        ExcelDataExportPR objexp = new ExcelDataExportPR();
        objexp.WriteExcell(dtreport);

        //reportDocumentObj.SetDataSource(dtreport);

        //Response.Clear();

        //Response.Buffer = true;

        //oStream = (MemoryStream)reportDocumentObj.ExportToStream(ExportFormatType.Excel);

        //Response.ContentType = "application/vnd.ms-excel";

        //Response.BinaryWrite(oStream.ToArray());

        //Response.End();



        //ResponseHelper.Redirect("CR_VoyageReportView.aspx?REPORTTYPE=" + ViewState["sReportType"].ToString() + "&VESSELID=" + ViewState["iVesselID"].ToString() + "&LOCATIONCODE=" + ViewState["iLocationID"].ToString() + "&FROMDT=" + ViewState["sFromDT"].ToString() + "&TODT=" + ViewState["sToDT"].ToString() + "&FLEETID=" + ViewState["iFleetID"].ToString(), "blank", "");
        // GridViewExportUtil.Export("NADReport"+DateTime.Now.ToShortDateString()+".xls", gvPortReport);
    }
}

public class ExcelDataExportPR : Page
{
    Hashtable myHashtable;
    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    //private Microsoft.Office.Interop.Excel.Range range;
    public ExcelDataExportPR()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void WriteExcell(DataTable dtReport)
    {
        CheckExcellProcesses();

        string path = "";
        path = Server.MapPath("~/operations/excelfile/NAD_Export.xls");


        ExlApp = new Microsoft.Office.Interop.Excel.Application();
        try
        {
            ExlWrkBook = ExlApp.Workbooks.Open(path, 0,
                                                      true,
                                                      5,
                                                      "",
                                                      "",
                                                      true,
                                                      Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                      "\t",
                                                      false,
                                                      false,
                                                      0,
                                                      true,
                                                      1,
                                                      0);
            ExlWrkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExlWrkBook.ActiveSheet;

            int i = 3;
            foreach (DataRow dr in dtReport.Rows)
            {
                ExlWrkSheet.Cells[i, 1] = Convert.ToString(dr["VESSEL_NAME"]);
                ExlWrkSheet.Cells[i, 2] = Convert.ToString(dr["STRTELEGRAM_DATE"]);
                ExlWrkSheet.Cells[i, 3] = Convert.ToString(dr["TELEGRAM_TYPE"]);
                ExlWrkSheet.Cells[i, 4] = Convert.ToString(dr["VOYAGE"]);
                ExlWrkSheet.Cells[i, 5] = Convert.ToString(dr["LOCATION_NAME"]);
                ExlWrkSheet.Cells[i, 6] = Convert.ToString(dr["PORT_NAME"]);
                ExlWrkSheet.Cells[i, 7] = Convert.ToString(dr["UTC_TYPE"]);
                ExlWrkSheet.Cells[i, 8] = Convert.ToString(dr["UTC_HR"]);
                ExlWrkSheet.Cells[i, 9] = Convert.ToString(dr["Clocks_type"]);
                ExlWrkSheet.Cells[i, 10] = Convert.ToString(dr["Clocks_Hr"]);
                ExlWrkSheet.Cells[i, 11] = Convert.ToString(dr["Latitude_Degrees"]);
                ExlWrkSheet.Cells[i, 12] = Convert.ToString(dr["Latitude_Minutes"]);
                ExlWrkSheet.Cells[i, 13] = Convert.ToString(dr["Latitude_Seconds"]);
                ExlWrkSheet.Cells[i, 14] = Convert.ToString(dr["LATITUDE_N_S"]);
                ExlWrkSheet.Cells[i, 15] = Convert.ToString(dr["Longitude_Degrees"]);
                ExlWrkSheet.Cells[i, 16] = Convert.ToString(dr["Longitude_Minutes"]);
                ExlWrkSheet.Cells[i, 17] = Convert.ToString(dr["Longitude_Seconds"]);
                ExlWrkSheet.Cells[i, 18] = Convert.ToString(dr["Longitude_E_W"]);
                ExlWrkSheet.Cells[i, 19] = Convert.ToString(dr["Vessel_Course"]);
                ExlWrkSheet.Cells[i, 20] = Convert.ToString(dr["Wind_Direction"]);
                ExlWrkSheet.Cells[i, 21] = Convert.ToString(dr["Wind_Force"]);
                ExlWrkSheet.Cells[i, 22] = Convert.ToString(dr["Sea_Direction"]);
                ExlWrkSheet.Cells[i, 23] = Convert.ToString(dr["Sea_Force"]);
                ExlWrkSheet.Cells[i, 24] = Convert.ToString(dr["Current_Direction"]);
                ExlWrkSheet.Cells[i, 25] = Convert.ToString(dr["Current_Speed"]);
                ExlWrkSheet.Cells[i, 26] = Convert.ToString(dr["Swell_Direction"]);
                ExlWrkSheet.Cells[i, 27] = Convert.ToString(dr["Swell_Height"]);
                ExlWrkSheet.Cells[i, 28] = Convert.ToString(dr["AirTemp"]);
                ExlWrkSheet.Cells[i, 29] = Convert.ToString(dr["AirPress"]);
                ExlWrkSheet.Cells[i, 30] = Convert.ToString(dr["AVERAGE_SPEED"]);
                ExlWrkSheet.Cells[i, 31] = Convert.ToString(dr["PassTime"]);
                ExlWrkSheet.Cells[i, 32] = Convert.ToString(dr["PassToGo"]);
                ExlWrkSheet.Cells[i, 33] = Convert.ToString(dr["DistSinceLastRep"]);
                ExlWrkSheet.Cells[i, 34] = Convert.ToString(dr["PassDist"]);
                ExlWrkSheet.Cells[i, 35] = Convert.ToString(dr["PassDistToGo"]);
                ExlWrkSheet.Cells[i, 36] = Convert.ToString(dr["CDFrmStormCenter"]);
                ExlWrkSheet.Cells[i, 37] = Convert.ToString(dr["TimeOfCPA"]);
                ExlWrkSheet.Cells[i, 38] = Convert.ToString(dr["NEXTPORT"]);
                ExlWrkSheet.Cells[i, 39] = Convert.ToString(dr["ETA_Next_Port"]);
                ExlWrkSheet.Cells[i, 40] = Convert.ToString(dr["FwdDraft"]);
                ExlWrkSheet.Cells[i, 41] = Convert.ToString(dr["MIDDRAFT"]);
                ExlWrkSheet.Cells[i, 42] = Convert.ToString(dr["AftDraft"]);
                ExlWrkSheet.Cells[i, 43] = Convert.ToString(dr["Trim"]);
                ExlWrkSheet.Cells[i, 44] = Convert.ToString(dr["GM"]);
                ExlWrkSheet.Cells[i, 45] = Convert.ToString(dr["Displacement"]);
                ExlWrkSheet.Cells[i, 46] = Convert.ToString(dr["SeaTemp"]);
                ExlWrkSheet.Cells[i, 47] = Convert.ToString(dr["EngRPM"]);
                ExlWrkSheet.Cells[i, 48] = Convert.ToString(dr["Slip"]);
                ExlWrkSheet.Cells[i, 49] = Convert.ToString(dr["ME_HOcons"]);
                ExlWrkSheet.Cells[i, 50] = Convert.ToString(dr["AE_HOcons"]);
                ExlWrkSheet.Cells[i, 51] = Convert.ToString(dr["Blr_HOcons"]);
                ExlWrkSheet.Cells[i, 52] = Convert.ToString(dr["ME_DOcons"]);
                ExlWrkSheet.Cells[i, 53] = Convert.ToString(dr["AE_DOcons"]);
                ExlWrkSheet.Cells[i, 54] = Convert.ToString(dr["Blr_DOcons"]);
                ExlWrkSheet.Cells[i, 55] = Convert.ToString(dr["HO_ROB"]);
                ExlWrkSheet.Cells[i, 56] = Convert.ToString(dr["DO_ROB"]);
                ExlWrkSheet.Cells[i, 57] = Convert.ToString(dr["MECC_ROB"]);
                ExlWrkSheet.Cells[i, 58] = Convert.ToString(dr["MECYL_ROB"]);
                ExlWrkSheet.Cells[i, 59] = Convert.ToString(dr["AECC_ROB"]);
                ExlWrkSheet.Cells[i, 60] = Convert.ToString(dr["FW_CONS"]);
                ExlWrkSheet.Cells[i, 61] = Convert.ToString(dr["FW_PROD"]);
                ExlWrkSheet.Cells[i, 62] = Convert.ToString(dr["FW_ROB"]);
                ExlWrkSheet.Cells[i, 63] = Convert.ToString(dr["RHRS_ME"]);
                ExlWrkSheet.Cells[i, 64] = Convert.ToString(dr["RHRS_AE1"]);
                ExlWrkSheet.Cells[i, 65] = Convert.ToString(dr["RHRS_AE2"]);
                ExlWrkSheet.Cells[i, 66] = Convert.ToString(dr["RHRS_AE3"]);
                ExlWrkSheet.Cells[i, 67] = Convert.ToString(dr["RHRS_AE4"]);
                ExlWrkSheet.Cells[i, 68] = Convert.ToString(dr["RHRS_SG"]);
                ExlWrkSheet.Cells[i, 69] = Convert.ToString(dr["RHRS_BLR"]);
                ExlWrkSheet.Cells[i, 70] = Convert.ToString(dr["ESP"]);
                ExlWrkSheet.Cells[i, 71] = Convert.ToString(dr["NOR"]);
                ExlWrkSheet.Cells[i, 72] = Convert.ToString(dr["ETB"]);
                ExlWrkSheet.Cells[i, 73] = Convert.ToString(dr["ETD"]);
                ExlWrkSheet.Cells[i, 74] = Convert.ToString(dr["Anchrd"]);
                ExlWrkSheet.Cells[i, 75] = Convert.ToString(dr["AnchrgNo"]);
                ExlWrkSheet.Cells[i, 76] = Convert.ToString(dr["ReasonsAnchg"]);
                ExlWrkSheet.Cells[i, 81] = Convert.ToString(dr["CntainrOnbd"]);
                ExlWrkSheet.Cells[i, 82] = Convert.ToString(dr["Reefer_Num"]);
                ExlWrkSheet.Cells[i, 83] = Convert.ToString(dr["CntainrDmgdSinceDep"]);
                ExlWrkSheet.Cells[i, 84] = Convert.ToString(dr["CntainrDmgdSinceDep_Remarks"]);
                ExlWrkSheet.Cells[i, 85] = Convert.ToString(dr["CntainrDmgdInPort"]);
                ExlWrkSheet.Cells[i, 86] = Convert.ToString(dr["CntainrDmgdInPort_Remarks"]);
                ExlWrkSheet.Cells[i, 87] = Convert.ToString(dr["Cntainr_Loaded"]);
                ExlWrkSheet.Cells[i, 88] = Convert.ToString(dr["Cntainr_Discharged"]);
                ExlWrkSheet.Cells[i, 89] = Convert.ToString(dr["remarks"]);



                i++;

            }

            string FileNameToSave = "NADReport_" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + " " + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + ".xlsx";

            ExlWrkBook.SaveAs(Server.MapPath("~/uploads/NADReports/") + FileNameToSave, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, true);
            ResponseHelper.Redirect("~/uploads/NADReports/" + FileNameToSave, "blank", "");
            // File.Delete(Server.MapPath("~/uploads/Purchase/") + "NADReport.xlsx");
        }
        catch
        {

        }
        finally
        {
            ExlWrkBook.Close(null, null, null);
            ExlApp.Workbooks.Close();
            ExlApp.Quit();
            KillExcel();



        }



    }

    private void CheckExcellProcesses()
    {
        Process[] AllProcesses = Process.GetProcessesByName("excel");
        myHashtable = new Hashtable();
        int iCount = 0;

        foreach (Process ExcelProcess in AllProcesses)
        {
            myHashtable.Add(ExcelProcess.Id, iCount);
            iCount = iCount + 1;
        }
    }

    private void KillExcel()
    {
        Process[] AllProcesses = Process.GetProcessesByName("excel");

        // check to kill the right process
        foreach (Process ExcelProcess in AllProcesses)
        {
            if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
                ExcelProcess.Kill();
        }

        AllProcesses = null;
    }


}