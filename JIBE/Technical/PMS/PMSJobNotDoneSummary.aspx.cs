using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PMS;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using System.Text;
using SMS.Business.Crew;
using System.Runtime.InteropServices;
using System.Diagnostics;

public partial class PMSJobNotDoneSummary : System.Web.UI.Page
{
    BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // FillDDL();

                BindFleetDLL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                BindVesselDDL();

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                //ucCustomPagerItems.PageSize = 20;

                Bind_Custom_Filters();

                BindJobDoneSummary();


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// For custom filter
    /// </summary>
    public void Bind_Custom_Filters()
    {
        try
        {
            if (!IsPostBack)
            {
                DataTable dtJobRank = objJobStatus.TecJobsGetRanks();

                ucf_DDLRank.DataValueField = "ID";
                ucf_DDLRank.DataTextField = "Rank_Short_Name";
                ucf_DDLRank.DataSource = dtJobRank;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// for binding job done summary
    /// </summary>
    /// <param name="fleetcode">for Fleet</param>
    /// <param name="vesselid">For vessel</param>
    /// <param name="DTCF_RANKID">for rank</param>
    /// <param name="StaffCode"> staff code</param>
    /// <param name="TYCF_StaffCode"></param>
    /// <param name="StaffName">staff name</param>
    /// <param name="TYCF_StaffName"></param>
    /// <param name="sortby">sort by </param>
    /// <param name="sortdirection">sort direction</param>
    /// <param name="pagenumber">page number</param>
    /// <param name="pagesize">page size</param>
    /// <param name="isfetchcount">for fetch count</param>
    /// <returns></returns>
    public void BindJobDoneSummary()
    {
        try
        {

            int rowcount = ucCustomPagerItems.isCountRecord;
            DataSet ds = new DataSet();


            string sortby = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            if (optRptType.SelectedValue == "0") //Rank - wise
            {
                gvJobDoneSummary.Columns[1].Visible = true;
                gvJobDoneSummary.Columns[2].Visible = true;
                gvJobDoneSummary.Columns[3].Visible = true;
                gvJobDoneSummary.Columns[4].Visible = true;

                ds = objJobStatus.TecJobDoneNotDoneSummarySearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                        , ucf_DDLRank.SelectedValues, null, null, null, null
                        , sortby, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            }
            else // Vessel - wise
            {
                gvJobDoneSummary.Columns[1].Visible = false;
                gvJobDoneSummary.Columns[2].Visible = false;
                gvJobDoneSummary.Columns[3].Visible = false;
                gvJobDoneSummary.Columns[4].Visible = false;

                ds = objJobStatus.TecJobDoneVesselWiseSummarySearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                        , sortby, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            }


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvJobDoneSummary.DataSource = ds.Tables[0];
                    gvJobDoneSummary.DataBind();
                }

                else
                {
                    gvJobDoneSummary.DataSource = ds.Tables[0];
                    gvJobDoneSummary.DataBind();
                }
            }

            UpdPnlGrid.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// for bind fleet & vessel 
    /// </summary>
    public void FillDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();



            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    /// <summary>
    /// for binding fleet
    /// </summary>

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
            throw ex;
        }
    }

    /// <summary>
    /// binding vessel depends on fleet value
    /// </summary>
    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    //private void FillDDLRank()
    //{
    //    try
    //    {
    //        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();

    //        DataTable dtRank = new DataTable();
    //        dtRank = objCrewAdmin.Get_RankList();
    //        DDLRankCategory.DataTextField = "Rank_Short_Name";
    //        DDLRankCategory.DataValueField = "ID";
    //        DDLRankCategory.DataSource = dtRank;
    //        DDLRankCategory.DataBind();
    //        ListItem li = new ListItem("--SELECT ALL--", "0");
    //        DDLRankCategory.Items.Insert(0, li);
    //    }
    //    catch
    //    {
    //    }

    //}

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();


    }



    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;

            BindJobDoneSummary();

            UpdPnlGrid.Update();
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            DDLFleet.SelectedValue = "0";
            DDLVessel.SelectedValue = "0";

            ucf_DDLRank.ClearSelection();

            optRptType.SelectedValue = "0";


            BindJobDoneSummary();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            int rowcount = ucCustomPagerItems.isCountRecord;
            DataSet ds = new DataSet();

            string sortby = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            if (optRptType.SelectedValue == "0") // Rank - wise
            {

                gvJobDoneSummary.Columns[2].Visible = true;
                gvJobDoneSummary.Columns[3].Visible = true;
                gvJobDoneSummary.Columns[4].Visible = true;

                ds = objJobStatus.TecJobDoneNotDoneSummarySearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                        , ucf_DDLRank.SelectedValues, null, null, null, null
                        , sortby, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

                string[] HeaderCaptions = { "Vessel", "Staff Code", "Staff Name", "Rank", "Total Jobs", "Jobs Updated", "Jobs Not Updated" };
                string[] DataColumnsName = { "Vessel_Name", "Staff_Code", "Staff_Name", "Rank_Short_Name", "JobCount", "JOB_DONE", "JobNotDone" };

                if (ds != null)
                {
                    GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Job Summary", "Jobs Updating Status");
                }
            }
            else // Vessel - wise
            {

                gvJobDoneSummary.Columns[2].Visible = false;
                gvJobDoneSummary.Columns[3].Visible = false;
                gvJobDoneSummary.Columns[4].Visible = false;

                ds = objJobStatus.TecJobDoneVesselWiseSummarySearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                        , sortby, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

                string[] HeaderCaptions = { "Vessel", "Total Jobs", "Jobs Updated", "Jobs Not Updated" };
                string[] DataColumnsName = { "Vessel_Name", "JobCount", "JOB_DONE", "JobNotDone" };


                GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Job Summary", "Jobs Updating Status");


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void BtnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            DataSet ds = objJobStatus.TecJobDoneNotDoneSummarySearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), null, null,
             null, null, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            ExcelDataPMSJob objExpJob = new ExcelDataPMSJob();
            StringBuilder sbEmailbody = new StringBuilder();

            string Attachfilename = "";

            string CaptainName = "", ChiefEnggName = "";

            objExpJob.WriteExcell(ds, ref Attachfilename);

            string sToEmailAddress = ds.Tables[2].Rows[0]["Vessel_email"].ToString(), strEmailAddCc = ds.Tables[2].Rows[0]["Fleet_email"].ToString() + ";" + ds.Tables[2].Rows[0]["Fleet_Suprintendent"].ToString(), strFormatSubject = "PMS Jobs updating status";

            DataTable dtRequesterDetails = ojbInfra.Get_UserDetails(Convert.ToInt32(Session["userid"].ToString()));


            if (ds.Tables[1].Rows.Count > 0)
                CaptainName = ds.Tables[1].Rows[0]["Staff_Name"].ToString();
            else
                CaptainName = "";

            if (ds.Tables[3].Rows.Count > 0)
                ChiefEnggName = ds.Tables[3].Rows[0]["Staff_Name"].ToString();
            else
                ChiefEnggName = "";

            sbEmailbody.Append("Dear Capt.  " + CaptainName + " & C/E  " + ChiefEnggName + ",");
            sbEmailbody.AppendLine("<br><br>");
            sbEmailbody.AppendLine("<br><br>");
            sbEmailbody.AppendLine("<br><br>");
            sbEmailbody.AppendLine("<br><br>");
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine("Best Regards,");
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine(dtRequesterDetails.Rows[0]["User_name"].ToString().ToUpper() + " " + dtRequesterDetails.Rows[0]["Last_Name"].ToString().ToUpper());
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine(dtRequesterDetails.Rows[0]["Designation"].ToString());
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine(Convert.ToString(Session["Company_Address_GL"]));
            sbEmailbody.AppendLine("<br>");

            /* Make Entry for email nofification   */

            int MailID = objBLLCrew.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, sbEmailbody.ToString(), "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");
            //string uploadpath = @"\\server01\uploads\PmsJobs";
            //string uploadpath = @"\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "" + @"\uploads\PmsJobs";
            string uploadpath = @"uploads\PmsJobs";
            /* Make Entry for email attachment   */

            BLL_Infra_Common.Insert_EmailAttachedFile(MailID, Attachfilename, uploadpath + @"\" + Attachfilename);

            string URL = String.Format("window.open('/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/crew/EmailEditor.aspx?ID=+" + MailID.ToString() + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }



    protected void optRptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindJobDoneSummary();

        UpdPnlGrid.Update();
    }
}


public class ExcelDataPMSJob : Page
{
    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    //private Microsoft.Office.Interop.Excel.Range range;
    public ExcelDataPMSJob()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void WriteExcell(DataSet dsJobSummary, ref string AttachFileName)
    {


        string path = "";
        path = Server.MapPath("~/Technical/PMS/ExcelFormat/JobsPerform_Format.xls");


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

            ExlWrkSheet.Cells[3, 2] = dsJobSummary.Tables[2].Rows[0]["Vessel_Name"].ToString();
            ExlWrkSheet.Cells[3, 5] = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

            int i = 6;
            foreach (DataRow dr in dsJobSummary.Tables[0].Rows)
            {
                ExlWrkSheet.Cells[i, 1] = dr["Staff_Code"].ToString();

                ExlWrkSheet.Cells[i, 2] = dr["Staff_Name"].ToString();
                ExlWrkSheet.Cells[i, 3] = dr["Rank_Short_Name"].ToString();

                ExlWrkSheet.Cells[i, 4] = dr["JobCount"].ToString();
                ExlWrkSheet.Cells[i, 5] = dr["JOB_DONE"].ToString();
                ExlWrkSheet.Cells[i, 6] = dr["JobNotDone"].ToString();

                i++;

            }


            string FileNameToSave = dsJobSummary.Tables[2].Rows[0]["Vessel_Short_Name"].ToString() + "_PMS Jobs updating status_" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + " " + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + ".xlsx";


            AttachFileName = FileNameToSave;

            ExlWrkBook.SaveAs(Server.MapPath("~/uploads/PmsJobs/") + FileNameToSave, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, true);


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
        finally
        {
            ExlWrkBook.Close(null, null, null);
            ExlApp.Workbooks.Close();
            ExlApp.Quit();
            Marshal.ReleaseComObject(ExlApp);
            Marshal.ReleaseComObject(ExlWrkSheet);
            Marshal.ReleaseComObject(ExlWrkBook);

        }



    }
}