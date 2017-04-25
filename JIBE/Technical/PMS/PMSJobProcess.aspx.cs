using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;


public partial class Technical_PMS_PMSJobProcess : System.Web.UI.Page
{
    BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase();
    BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();
    BLL_PMS_Job_Status objjob = new BLL_PMS_Job_Status();
    MergeGridviewHeader_Info objPMSMerge = new MergeGridviewHeader_Info();

    DataTable dtSubCatelogue = new DataTable();
    int RHDone = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {


            String msgretv = String.Format("setTimeout(getOperatingSystem,500);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

            if (!IsPostBack)
            {

                // FillDDL();

                BindFleetDLL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                BindVesselDDL();

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                if (Request.QueryString["OverDueSearchFlage"] != null)
                {
                    btnOverDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#D8F6CE");
                    ViewState["OverDueSearchFlage"] = Request.QueryString["OverDueSearchFlage"].ToString();

                }
                else
                {
                    ViewState["OverDueSearchFlage"] = null;
                }


                ViewState["ucfJobIDFilterType"] = null;

                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["JOB_ID"])))
                {

                    ViewState["Jobid"] = Request.QueryString["JOB_ID"].ToString();

                    txtSearchJobTitle.Text = Request.QueryString["JOB_ID"].ToString();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["VESSEL_ID"])))
                {
                    DDLVessel.SelectedValue = Request.QueryString["VESSEL_ID"].ToString();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Fleet_ID"])))
                {
                    DDLFleet.SelectedValue = Request.QueryString["Fleet_ID"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["Qflag"]))
                    ViewState["Qflag"] = Request.QueryString["Qflag"].ToString();
                else
                    ViewState["Qflag"] = "S";

                if (ViewState["Qflag"].ToString().ToUpper() == "S")
                {
                    lblPageTitle.Text = "Job Status";
                    this.Title = "Job Status";
                    rbtnJobTypes.SelectedValue = "PMS";
                }
                else
                {
                    lblPageTitle.Text = "Job History";
                    if (string.IsNullOrWhiteSpace(Convert.ToString(ViewState["Jobid"])))
                    {
                        this.Title = "Job History";
                        //rbtnJobTypes.SelectedValue = "NONPMS";
                    }
                    else
                    {
                        this.Title = "Job Code :: " + Convert.ToString(ViewState["Jobid"]);
                    }

                }


                Bind_Custom_Filters();

                Bindfunction();
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Function_ID"])))
                {
                    ddlFunction.SelectedValue = Request.QueryString["Function_ID"].ToString();
                }
                BindSystem_Location();
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["System_ID"])))
                {
                    ddlSystem_location.SelectedValue = Request.QueryString["System_ID"].ToString();
                }
                BindSubSystem_Location();


                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["SubSystem_ID"])))
                {
                    ddlSubSystem_location.SelectedValue = Request.QueryString["SubSystem_ID"].ToString();
                }
                BindJobStatus();
              
            }

            objPMSMerge.AddMergedColumns(new int[] { 7, 8 }, "Frequency", "HeaderStyle-css");
            objPMSMerge.AddMergedColumns(new int[] { 9, 10 }, "Dates", "HeaderStyle-css");
            objPMSMerge.AddMergedColumns(new int[] { 11, 12 }, "Hours", "HeaderStyle-css");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    public void Bindfunction()
    {
        try
        {
            DataTable dt = objBLLPURC.LibraryGetSystemParameterList("115", "");
            ddlFunction.Items.Clear();
            ddlFunction.DataSource = dt;
            ddlFunction.DataValueField = "CODE";
            ddlFunction.DataTextField = "DESCRIPTION";
            ddlFunction.DataBind();
            ddlFunction.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlFunction.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindSystem_Location()
    {

        try
        {
            DataTable dt = objBLLPURC.GET_SYSTEM_LOCATION(UDFLib.ConvertToInteger(ddlFunction.SelectedValue), UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

            ddlSystem_location.Items.Clear();
            ddlSystem_location.DataSource = dt;
            ddlSystem_location.DataValueField = "AssginLocationID";
            ddlSystem_location.DataTextField = "LocationName";
            ddlSystem_location.DataBind();
            ddlSystem_location.Items.Insert(0, new ListItem("- ALL-", "0,0"));
            if (ddlSystem_location.SelectedIndex == -1)
                ddlSystem_location.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void BindSubSystem_Location()
    {
        try
        {

            if (ddlSystem_location.SelectedValue != "0")
            {
                DataTable dt = objBLLPURC.GET_SUBSYTEMSYSTEM_LOCATION(ddlSystem_location.SelectedValue.ToString().Split(',')[1], null, UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

                ddlSubSystem_location.Items.Clear();
                ddlSubSystem_location.DataSource = dt;
                ddlSubSystem_location.DataValueField = "AssginLocationID";
                ddlSubSystem_location.DataTextField = "LocationName";
                ddlSubSystem_location.DataBind();
                ddlSubSystem_location.Items.Insert(0, new ListItem("- ALL-", "0,0"));
                if (ddlSubSystem_location.SelectedIndex == -1)
                    ddlSubSystem_location.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Added on:27-07-2016|| To reset drop down to default values.
    /// </summary>
    private void reset_ddl()
    {
        try
        {
            DataTable dtemt = new DataTable();

            DataColumn workCol = dtemt.Columns.Add("AssginLocationID", typeof(Int32));

            dtemt.Columns.Add("LocationName", typeof(String));
            DataRow drsys = dtemt.NewRow();
            drsys["AssginLocationID"] = 0;
            drsys["LocationName"] = "--ALL --";
            dtemt.Rows.InsertAt(drsys, 0);
            ddlSystem_location.DataSource = dtemt;
            ddlSystem_location.DataValueField = "AssginLocationID";
            ddlSystem_location.DataTextField = "LocationName";
            ddlSystem_location.DataBind();

            DataTable dts = new DataTable();
            DataColumn workCols = dts.Columns.Add("AssginLocationID", typeof(String));

            dts.Columns.Add("LocationName", typeof(String));
            DataRow drsyss = dts.NewRow();
            drsyss["AssginLocationID"] = "0,0";
            drsyss["LocationName"] = "--ALL --";
            dts.Rows.InsertAt(drsyss, 0);
            ddlSubSystem_location.DataSource = dts;
            ddlSubSystem_location.DataValueField = "AssginLocationID";
            ddlSubSystem_location.DataTextField = "LocationName";
            ddlSubSystem_location.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Bind_Custom_Filters()
    {
        try
        {
            if (!IsPostBack)
            {
                // Job Rank - Filter
                DataTable dtJobRank = objjob.TecJobsGetRanks();
                ucf_DDLRank.DataValueField = "ID";
                ucf_DDLRank.DataTextField = "Rank_Short_Name";
                ucf_DDLRank.DataSource = dtJobRank;


                // Job Critical - Filter
                //DataTable dtJobCritical = new DataTable();
                //dtJobCritical.Columns.Add("ID", typeof(int));
                //dtJobCritical.Columns.Add("Critical_Name", typeof(string));

                //dtJobCritical.Rows.Add(1, "Critical");
                //dtJobCritical.Rows.Add(0, "Non Critical");

                //ucf_optCritical.DataValueField = "ID";
                //ucf_optCritical.DataTextField = "Critical_Name";
                //ucf_optCritical.DataSource = dtJobCritical;

                //// Job CMS - Filter
                //DataTable dtJobCMS = new DataTable();
                //dtJobCMS.Columns.Add("ID", typeof(int));
                //dtJobCMS.Columns.Add("CMS_Name", typeof(string));

                //dtJobCMS.Rows.Add(1, "CMS");
                //dtJobCMS.Rows.Add(0, "Non CMS");

                //ucf_optCMS.DataValueField = "ID";
                //ucf_optCMS.DataTextField = "CMS_Name";
                //ucf_optCMS.DataSource = dtJobCMS;

            }
        }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;
        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
            DDLVessel.SelectedIndex = 0;


            if (Request.QueryString["Vessel_ID"] != null)
            {
                DDLVessel.SelectedValue = Request.QueryString["Vessel_ID"].ToString();

            }



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void EnableButton()
    {
        string QueryFlage = ViewState["Qflag"].ToString();



        if (QueryFlage == "H")
        {
            btn7days.Visible = false;
            btnOverDue.Visible = false;
            btnCurrentMonth.Visible = false;


            //gvStatus.Columns[0].Visible = false;
        }
        else
        {
            btn7days.Visible = true;
            btnOverDue.Visible = true;
            btnCurrentMonth.Visible = true;


            // gvStatus.Columns[0].Visible = true;
        }

    }

    /// <summary>
    /// Bind jobs status
    /// </summary>
    /// <param name="VesselId">Vessel Id</param>
    /// <param name="LocationId">location Id</param>
    /// <param name="SystemLocation">system location of system</param>
    /// <param name="SubSystemLocation">sub system location of system</param>
    /// <param name="DepartmentID">Department Id for system</param>
    /// <param name="RankID">Rank Id</param>
    /// <param name="SearchText">Search text like job code or title</param>
    /// <param name="critical">Check for critical jobs</param>
    /// <param name="cms">Check for CMS jobs</param>
    /// <param name="fromdate">from date</param>
    /// <param name="todate">To date</param>
    /// <param name="DueDateFlageSearch">For filter like "This Month","Overdue"</param>
    /// <param name="VerifyFlag">Check for job is verifie or not</param>
    /// <param name="JobFreq">Set frequency for job</param>
    /// <param name="IsSafetyAlarm">check IsSafetyAlarm for job</param>
    /// <param name="IsCalibration">check IsCalibration for job</param>
    /// <param name="sortby">for sorting </param>
    /// <param name="sortdirection">for sortdirection</param>
    /// <param name="pagenumber">used for page number</param>
    /// <param name="pagesize">used for page size</param>
    /// <param name="RHDone">for search RunHours done</param>
    /// <param name="isfetchcount"></param>
    /// <returns></returns>
    public void BindJobStatus()
    {

        try
        {

            BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
            DataSet ds = new DataSet();

            int rowcount = ucCustomPagerItems.isCountRecord;
            string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            string duedateflagesearch = (ViewState["OverDueSearchFlage"] == null) ? null : (ViewState["OverDueSearchFlage"].ToString());
            int IsPendingOfcVerify = 0, PostponeJob = 0, FollowupAdded = 0, JobWithMandateRAssess = 0, JobWithSubRAssess = 0, JobDiffToDDock = 0;
            string QueryFlage = ViewState["Qflag"].ToString();
            int isCritical = 0;
            int isCMS = 0;
            EnableButton();
            int? jobid = null;
            int? isHistory = 0;

            if (QueryFlage != "S")
            {
                isHistory = 1;

                if (QueryFlage == "H" && rbtnJobTypes.SelectedValue == "NONPMS")
                {
                    isHistory = 3;
                }

                jobid = null; if (ViewState["Jobid"] != null) jobid = Convert.ToInt32(ViewState["Jobid"].ToString());

            }
            else if (QueryFlage == "S" && rbtnJobTypes.SelectedValue == "NONPMS")
            {
                isHistory = 2;

            }

            if (chkCritical.Checked == true)/* For Filtering Critical jobs */
                isCritical = 1;


            if (chkCMS.Checked == true)/* For Filtering CMS jobs */
                isCMS = 1;

            if (chkOfcVerify.Checked == true) /* For Filtering jobs pending for office verification */
                IsPendingOfcVerify = 1;


            int? SafetyAlarm = null;
            int? Calibration = null;

            if (chkAdvSftyAlarm.Checked == true) /* For Filtering Safety alarm jobs*/
                SafetyAlarm = 1;
            if (chkAdvCalibration.Checked == true)/* For Filtering Calibration jobs */
                Calibration = 1;

            int? Is_RAMandatory = null;    //Added by reshma for RA:  For Filtering RA mandatory jobs
            int? Is_RASubmitted = null;      //Added by reshma for RA : For Filtering RA form submitted jobs

            if (rbtnMRA.SelectedItem.Value == "YES")
            {
                Is_RAMandatory = 1;
            }
            else if (rbtnMRA.SelectedItem.Value == "NO")
            {
                Is_RAMandatory = 2;
              
            }

            if (rbtnRASubmitted.SelectedItem.Value == "YES")
            {
                Is_RASubmitted = 1;
            }
            else if (rbtnRASubmitted.SelectedItem.Value == "NO")
            {
                Is_RASubmitted = 2;
            }
                       
            
            /* Below Filters will be added in next phase of development*/
            //if (chkAdvPPJobs.Checked == true)
            //    PostponeJob = 1;

            //if (chkAdvJWMRA.Checked == true)
            //    JobWithMandateRAssess = 1;
            //if (chkAdvJWSRA.Checked == true)
            //    JobWithSubRAssess = 1;
            //if (chkAdvJDTDD.Checked == true)
            //    JobDiffToDDock = 1;
            /* upto this Filters will be added in next phase of development*/

            ds = objJobStatus.TecJobStatusIndex(jobid, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0])
                 , ucf_DDLRank.SelectedValues, null, txtSearchJobTitle.Text != "" ? txtSearchJobTitle.Text.Trim() : null
                  , isCritical, isCMS, UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), UDFLib.ConvertDateToNull(txtActFrmDate.Text), UDFLib.ConvertDateToNull(txtActToDate.Text), isHistory, duedateflagesearch,
                  IsPendingOfcVerify, SafetyAlarm, Calibration, PostponeJob, FollowupAdded, JobWithMandateRAssess, JobWithSubRAssess, JobDiffToDDock, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, RHDone, Is_RAMandatory, Is_RASubmitted, ref  rowcount);


            DataTable dt = ds.Tables[0];

            DataColumnCollection columns = dt.Columns;
            if (columns.Contains("RemRhrs"))
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (dt.Rows[k]["FREQUENCY_TYPE"] == "2484" || dt.Rows[k]["FREQUENCY_TYPE"] == "2485")
                    {
                        dt.Rows[k]["RemRhrs"] = System.DBNull.Value;
                    }
                }
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
                    gvStatus.DataSource = dt;
                    gvStatus.DataBind();
                }
                else
                {
                    gvStatus.DataSource = dt;
                    gvStatus.DataBind();
                }
            }

            UpdPnlGrid.Update();
            UpdPnlFilter.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }

    protected void gvStatus_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }

    protected void gvStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowJobHistory")
        {

            ViewState["Qflag"] = "H";
            ViewState["Jobid"] = e.CommandArgument.ToString();

            ViewState["ucfJobIDFilterType"] = "EqualTo";
           

            BindJobStatus();


        }

    }

    protected void gvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblVesselCode = (Label)e.Row.FindControl("lblVesselCode");
                Label lblCMS = (Label)e.Row.FindControl("lblCMS");
                Label lblCritical = (Label)e.Row.FindControl("lblCritical");
                Label lblJobTitle = (Label)e.Row.FindControl("lblJobTitle");
                Label lblLocationID = (Label)e.Row.FindControl("lblLocationID");
                Label lblLocation = (Label)e.Row.FindControl("lblLocation");
                Label lblOverDueFlage = (Label)e.Row.FindControl("lblOverDueFlage");
                Label lblNext30dayFlage = (Label)e.Row.FindControl("lblNext30dayFlage");
                Label lblJobDescription = (Label)e.Row.FindControl("lblJobDescription");
                Label lblJobHistoryRemaks = (Label)e.Row.FindControl("lblJobHistoryRemaks");
                Label lblFullJobHistoryRemaks = (Label)e.Row.FindControl("lblFullJobHistoryRemaks");
                Label lblJobHistoryID = (Label)e.Row.FindControl("lblJobHistoryID");
                Label lblFreqType = (Label)e.Row.FindControl("lblFrequencyType");
                Label lblJobCode = (Label)e.Row.FindControl("lblJobCode");


                HyperLink ImgHistory = (HyperLink)e.Row.FindControl("ImgHistory");
                if (ViewState["Qflag"].ToString() == "H")
                {
                    ImgHistory.Visible = false;
                }
                else
                {

                    ImgHistory.NavigateUrl = "PMSJobHistory.aspx?Qflag=H&JOB_ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JOB_ID") + "&VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Function_ID=" + DataBinder.Eval(e.Row.DataItem, "FunctionID").ToString() + "&System_ID=" + DataBinder.Eval(e.Row.DataItem, "SysID").ToString() + "&SubSystem_ID=" + DataBinder.Eval(e.Row.DataItem, "SubSysID").ToString() + "&Fleet_ID=" + DataBinder.Eval(e.Row.DataItem, "FleetID").ToString() + "&JobCode=" + DataBinder.Eval(e.Row.DataItem, "Job_Code").ToString());
                }

                if (lblJobHistoryID.Text == "")
                    lblJobHistoryID.Text = "0";

                HyperLink hlnkJobCode = (HyperLink)e.Row.FindControl("hlnkJobCode");
                if (UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "JobHistoryID")) > 0)
                    hlnkJobCode.NavigateUrl = "PMSAddAdhocJob.aspx?JOB_HISTORY_ID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&OFFICE_ID=" + DataBinder.Eval(e.Row.DataItem, "OFFICE_ID").ToString() + "&JOB_ID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString() + "&IsRAMandatory=" + DataBinder.Eval(e.Row.DataItem, "IsRAMandatory").ToString();  //Added by reshma for RA JIT : 11705
                else
                    hlnkJobCode.NavigateUrl = "PMSJobIndividualDetails.aspx?JobHistoryID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&VID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Qflag=S&JobID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString() + "&System_ID=" + DataBinder.Eval(e.Row.DataItem, "SysID").ToString() + "&SubSystem_ID=" + DataBinder.Eval(e.Row.DataItem, "SubSysID").ToString(); // for binding SystemLocation and SubSystemLocation 

                ImageButton ImgSpareUsed = (ImageButton)e.Row.FindControl("ImgSpareUsed");
                ImgSpareUsed.Attributes.Add("onclick", "document.getElementById('iFrmJobsDetails').src ='../PMS/PMS_SparesUsed.aspx?JobID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString() + "&JobHistoryID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&Vessel_ID=" + lblVesselCode.Text.Trim() + "';showModal('dvJobsDetails');return false;");

                ImageButton ImgRHours = (ImageButton)e.Row.FindControl("ImgRHours");
                
                if (lblFreqType.Text.ToString() == "2486")
                {
                    ImgRHours.Visible = true;
                    ImgRHours.Attributes.Add("onClick", "javascript:window.open('../PMS/PMSRunningHours.aspx?VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Function_ID=" + DataBinder.Eval(e.Row.DataItem, "FunctionID").ToString() + "&System_ID=" + DataBinder.Eval(e.Row.DataItem, "SysID").ToString() + "&SubSystem_ID=" + DataBinder.Eval(e.Row.DataItem, "SubSysID").ToString() + "&Fleet_ID=" + DataBinder.Eval(e.Row.DataItem, "FleetID").ToString() + "&DisplayInheritingCounters=1','_blank');return false");// Added one paramater to display inheriting counters also.
                }


                if (lblFullJobHistoryRemaks.Text.Length > 15)
                    lblJobHistoryRemaks.Text = lblJobHistoryRemaks.Text + "..";


                if (lblJobDescription.Text != "")
                    lblJobTitle.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Job Description] body=[" + lblJobDescription.Text + "]");


                if (lblFullJobHistoryRemaks.Text != "")
                    lblJobHistoryRemaks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + lblFullJobHistoryRemaks.Text + "]");

                if (lblOverDueFlage.Text == "Y")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[10].Font.Bold = true;
                }
                if (lblNext30dayFlage.Text == "Y")
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[10].Font.Bold = true;
                }
                if (lblCMS.Text == "Y")
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[13].Font.Bold = true;
                }
                if (lblCritical.Text == "Y")
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[14].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[14].Font.Bold = true;
                }


            }

            if (e.Row.RowType == DataControlRowType.DataRow)   //Reshma - Remaining Running Hours - when the remaining running hours are negative (meaning the job is overdue), this field (Rem. RHRS) should be painted red.
            {
                Label lblRemHrs = (Label)e.Row.FindControl("lblRemRHrsdone");
                if (lblRemHrs.Text != "")
                {
                    if (Convert.ToInt32(lblRemHrs.Text.ToString()) < 0)
                    {
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[12].Font.Bold = true;
                    }
                }

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFilePathIfSingle = (Label)e.Row.FindControl("lblFilePathIfSingle");
                ImageButton ImgJobDoneAtt = (ImageButton)e.Row.FindControl("ImgJobDoneAtt");

                if (lblFilePathIfSingle.Text != "")
                {
                    ImgJobDoneAtt.Attributes.Add("onclick", "DocOpen('" + lblFilePathIfSingle.Text + "'); return false;");
                }
                else
                {
                    ImgJobDoneAtt.Attributes.Add("onclick", "javascript:window.open('../PMS/PMSJobDone_Attachments.aspx?JobHistoryID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&Vessel_ID=" + DataBinder.Eval(e.Row.DataItem, "VESSEL_ID").ToString() + "&OFFICE_ID=" + DataBinder.Eval(e.Row.DataItem, "OFFICE_ID").ToString() + "'); return false;");
                }
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader.SetProperty(objPMSMerge);

                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                ViewState["DynamicHeaderCSS"] = "HeaderStyle-css";

            }


            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SORTBYCOLOUMN"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                    if (img != null)
                    {
                        if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                            img.Src = "../../purchase/Image/arrowUp.png";

                        else
                            img.Src = "../../purchase/Image/arrowDown.png";

                        img.Visible = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void imgCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        BindJobStatus();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string duedateflagesearch = (ViewState["OverDueSearchFlage"] == null) ? null : (ViewState["OverDueSearchFlage"].ToString());
            string QueryFlage = ViewState["Qflag"].ToString();
            int IsPendingOfcVerify = 0, PostponeJob = 0, FollowupAdded = 0, JobWithMandateRAssess = 0, JobWithSubRAssess = 0, JobDiffToDDock = 0;
            int? SafetyAlarm = null;
            int? Calibration = null;
            int? isCritical = 0;
            int? isCMS = 0;
            if (chkAdvSftyAlarm.Checked == true)
                SafetyAlarm = 1;
            if (chkAdvCalibration.Checked == true)
                Calibration = 1;

            if (chkCritical.Checked == true)/* For Filtering Critical jobs */
                isCritical = 1;


            if (chkCMS.Checked == true)/* For Filtering CMS jobs */
                isCMS = 1;

            if (chkOfcVerify.Checked == true) /* For Filtering jobs pending for office verification */
                IsPendingOfcVerify = 1;




            if (chkAdvSftyAlarm.Checked == true) /* For Filtering Safety alarm jobs*/
                SafetyAlarm = 1;
            if (chkAdvCalibration.Checked == true)/* For Filtering Calibration jobs */
                Calibration = 1;

            int? Is_RAMandatory = null;    //Added by reshma for RA: For Filtering RA mandatory jobs
            int? Is_RASubmitted = null;      //Added by reshma for RA :For Filtering RA form submitted jobs

            if (rbtnMRA.SelectedItem.Value == "YES")
            {
                Is_RAMandatory = 1;
            }

            if (rbtnMRA.SelectedItem.Value == "NO")
            {
                Is_RAMandatory = 2;
            }
            if (rbtnRASubmitted.SelectedItem.Value == "YES")
            {
                Is_RASubmitted = 1;
            }
            if (rbtnRASubmitted.SelectedItem.Value == "NO")
            {
                Is_RASubmitted = 2;
            }
           

            /* Below Filters will be added in next phase of development*/
            //if (chkAdvPPJobs.Checked == true)
            //    PostponeJob = 1;

            //if (chkAdvJWMRA.Checked == true)
            //    JobWithMandateRAssess = 1;
            //if (chkAdvJWSRA.Checked == true)
            //    JobWithSubRAssess = 1;
            //if (chkAdvJDTDD.Checked == true)
            //    JobDiffToDDock = 1;
            /* upto this Filters will be added in next phase of development*/

            if (QueryFlage == "S")
            {


                DataSet ds = objJobStatus.TecJobStatusIndex(null, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0])
               , ucf_DDLRank.SelectedValues, null, txtSearchJobTitle.Text != "" ? txtSearchJobTitle.Text.Trim() : null
                , isCritical, isCMS, UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), UDFLib.ConvertDateToNull(txtActFrmDate.Text), UDFLib.ConvertDateToNull(txtActToDate.Text), 0, duedateflagesearch,
                IsPendingOfcVerify, SafetyAlarm, Calibration, PostponeJob, FollowupAdded, JobWithMandateRAssess, JobWithSubRAssess, JobDiffToDDock, sortbycoloumn, sortdirection, null, null, RHDone, Is_RAMandatory, Is_RASubmitted, ref  rowcount);

               
                string[] HeaderCaptions = { "Vessel", "System Location", "Sub-System Location", "Job Code", "Job Title", "Job Description", "Frequency", "Frequency Name", "Done", "Next Due", "Rhrs", "Rem. Rhrs", "CMS", "Critical", "Mandatory Risk Assessment", "Department", "Rank", "Remarks" };
                string[] DataColumnsName = { "Vessel_Name", "Location", "SUBLocation", "Job_Code", "JOB_TITLE", "Job_Description", "FREQUENCY", "Frequency_Name", "LAST_DONE", "DATE_NEXT_DUE", "RHRS_DONE", "RemRhrs", "CMS", "Critical", "IsRAMandatory", "Department", "RankName", "FullRemarks" };

                string FileName = "PMSStatus" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".xls";
                string FilePath = Server.MapPath(@"~/Uploads\\Temp\\"); string ExportTempFilePath = Server.MapPath(@"~/Uploads\\ExportTemplete\\");

                if ((sender as ImageButton).CommandArgument == "ExportFrom_IE")
                {
                    GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Status", "PMS Status", "");
                }
                else
                {
                    GridViewExportUtil.Export_To_Excel_Interop(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Status", FilePath + FileName, ExportTempFilePath, @"~\\Uploads\\Temp\\" + FileName);
                    //GridViewExportUtil.Show_CSV_TO_Excel(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Status", "PMS Status", "");
                }

            }
            else
            {


                if (chkAdvSftyAlarm.Checked == true)
                    SafetyAlarm = 1;
                if (chkAdvCalibration.Checked == true)
                    Calibration = 1;
                int? jobid = null; if (ViewState["Jobid"] != null) jobid = Convert.ToInt32(ViewState["Jobid"].ToString());


                DataSet ds = objJobStatus.TecJobStatusIndex(jobid, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0])
             , ucf_DDLRank.SelectedValues, null, txtSearchJobTitle.Text != "" ? txtSearchJobTitle.Text.Trim() : null
              , isCritical, isCMS, UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), UDFLib.ConvertDateToNull(txtActFrmDate.Text), UDFLib.ConvertDateToNull(txtActToDate.Text), 1, duedateflagesearch,
              IsPendingOfcVerify, SafetyAlarm, Calibration, PostponeJob, FollowupAdded, JobWithMandateRAssess, JobWithSubRAssess, JobDiffToDDock, sortbycoloumn, sortdirection, null, null, RHDone, Is_RAMandatory, Is_RASubmitted, ref  rowcount);

               
                string[] HeaderCaptions = { "Job History ID", "Vessel", "Location", "SubSystem", "Job Code", "Job Title", "Job Description", "Frequency", "Frequency Name", "Last Done", "Rhrs", "Rem. Rhrs", "Next Due", "CMS", "Critical", "Mandatory Risk Assessment", "Department", "Rank", "Remarks" };
                string[] DataColumnsName = { "JobHistoryID", "Vessel_Name", "Location", "SubSystem", "Job_Code", "JOB_TITLE", "Job_Description", "FREQUENCY", "Frequency_Name", "LAST_DONE", "RHRS_DONE", "RemRhrs", "DATE_NEXT_DUE", "CMS", "Critical", "IsRAMandatory", "Department", "RankName", "FullRemarks" };


                string FileName = "PMSJobHistory" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".xls";
                string FilePath = Server.MapPath(@"~/Uploads\\Temp\\"); string ExportTempFilePath = Server.MapPath(@"~/Uploads\\ExportTemplete\\");

                if ((sender as ImageButton).CommandArgument == "ExportFrom_IE")
                {
                    GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Job History", "PMS Job History", "");
                }
                else
                {
                    GridViewExportUtil.Export_To_Excel_Interop(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Job History", FilePath + FileName, ExportTempFilePath, @"~\\Uploads\\Temp\\" + FileName);
                    //GridViewExportUtil.Show_CSV_TO_Excel(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Job History", "PMS Job History", "");
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;
            string duedateflagesearch = (ViewState["OverDueSearchFlage"] == null) ? null : (ViewState["OverDueSearchFlage"].ToString());
            if (duedateflagesearch == "W")
            {
                RHDone = 0;
            }
            if (duedateflagesearch == "M")
            {
                RHDone = 0;
            }

            BindJobStatus();
            UpdPnlGrid.Update();

            if (hfAdv.Value == "o")
            {
                String tgladvsearch = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch", tgladvsearch, true);
            }
            else
            {
                String tgladvsearch1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch1", tgladvsearch1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void btnOverDue_Click(object sender, EventArgs e)
    {
        try
        {
            btnOverDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#D8F6CE");
            btn7days.BackColor = System.Drawing.ColorTranslator.FromHtml("");
            btnCurrentMonth.BackColor = System.Drawing.ColorTranslator.FromHtml("");


            ViewState["OverDueSearchFlage"] = "O";

            BindJobStatus();
            UpdPnlGrid.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btn7days_Click(object sender, EventArgs e)
    {
        try
        {
            btnOverDue.BackColor = System.Drawing.ColorTranslator.FromHtml("");
            btn7days.BackColor = System.Drawing.ColorTranslator.FromHtml("#D8F6CE");
            btnCurrentMonth.BackColor = System.Drawing.ColorTranslator.FromHtml("");

            ViewState["OverDueSearchFlage"] = "W";
            RHDone = 0;
            BindJobStatus();
            UpdPnlGrid.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnCurrentMonth_Click(object sender, EventArgs e)
    {
        try
        {
            btnOverDue.BackColor = System.Drawing.ColorTranslator.FromHtml("");
            btn7days.BackColor = System.Drawing.ColorTranslator.FromHtml("");
            btnCurrentMonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#D8F6CE");

            ViewState["OverDueSearchFlage"] = "M";
            RHDone = 0;
            BindJobStatus();
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
            btnOverDue.BackColor = System.Drawing.ColorTranslator.FromHtml("");
            btn7days.BackColor = System.Drawing.ColorTranslator.FromHtml("");
            btnCurrentMonth.BackColor = System.Drawing.ColorTranslator.FromHtml("");



            ViewState["OverDueSearchFlage"] = null;

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ViewState["ucfJobIDFilterType"] = null;
            ViewState["Jobid"] = null;

            DDLFleet.SelectedValue = "0";
            BindVesselDDL();

            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtActFrmDate.Text = "";
            txtActToDate.Text = "";

            DDLVessel.ClearSelection();

            ddlFunction.ClearSelection();
            
            chkCMS.Checked = false;
            chkCritical.Checked = false;
            ddlSystem_location.ClearSelection();
            ucf_DDLRank.ClearSelection();
            ddlSubSystem_location.ClearSelection();

            chkAdvCalibration.Checked = false;
            chkAdvSftyAlarm.Checked = false;
            rbtnJobTypes.SelectedValue = "PMS";
            txtSearchJobTitle.Text = "";
            rbtnMRA.SelectedValue = "ALL";  
            rbtnRASubmitted.SelectedValue = "ALL";            

            BindJobStatus();
            UpdPnlGrid.Update();
            UpdAdvFltr.Update();

            
            chkOfcVerify.Checked = false;

            if (hfAdv.Value == "o")
            {
                String tgladvsearchClr = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr", tgladvsearchClr, true);
            }
            else
            {
                String tgladvsearchClr1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr1", tgladvsearchClr1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void gvStatus_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindJobStatus();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    private void SetCatalogueRowSelection()
    {
        gvStatus.SelectedIndex = -1;
        for (int i = 0; i < gvStatus.Rows.Count; i++)
        {
            if (gvStatus.DataKeys[i].Value.ToString().Equals(ViewState["SystemId"].ToString()))
            {
                gvStatus.SelectedIndex = i;
            }
        }
    }


    protected void btnJobsHistory_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Qflag"] = "H";

            BindJobStatus();
            UpdPnlGrid.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {


        reset_ddl(); // To reset system and subsystem dropdown to default values.
        if (ddlFunction.SelectedIndex != 0)
        {
            BindSystem_Location();
        }
        ddlSubSystem_location.SelectedIndex = 0;

    }

    protected void ddlSystem_location_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindSubSystem_Location();
    }

    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSystem_Location();
        BindSubSystem_Location();
    }

    
}