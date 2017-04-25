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

public partial class Technical_PMS_PMSJobHistory : System.Web.UI.Page
{
    BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase();
    BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();
    BLL_PMS_Job_Status objjob = new BLL_PMS_Job_Status();
    MergeGridviewHeader_Info objPMSMerge = new MergeGridviewHeader_Info();
    BLL_PMS_Job_Status objJobStat = new BLL_PMS_Job_Status();
    DataTable dtSubCatelogue = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        String msgretv = String.Format("setTimeout(getOperatingSystem,500);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

        if (!IsPostBack)
        {
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            BindFleetDLL();
            if (Session["USERFLEETID"] == null)
            {
                DDLFleet.SelectedValue = "0";
            }
            else
            {
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            }
            BindVesselDDL();

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            if (Request.QueryString["OverDueSearchFlage"] != null)
            {
                //  btnOverDue.BackColor = System.Drawing.ColorTranslator.FromHtml("#D8F6CE");
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
            else
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["JobCode"])))
                {
                    txtSearchJobTitle.Text = Request.QueryString["JobCode"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["VESSEL_ID"])))
            {
                DDLVessel.Select(Request.QueryString["VESSEL_ID"].ToString());
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Fleet_ID"])))
            {
                DDLFleet.SelectedValue = Request.QueryString["Fleet_ID"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Qflag"]))
                ViewState["Qflag"] = Request.QueryString["Qflag"].ToString();
            else
                ViewState["Qflag"] = "S";


            lblPageTitle.Text = "Job History";
            if (string.IsNullOrWhiteSpace(Convert.ToString(ViewState["Jobid"])))
            {
                this.Title = "Job History";
                // rbtnJobTypes.SelectedValue = "NONPMS";
                rbtDueType.SelectedValue = "0";
            }
            else
            {
                this.Title = "Job Code :: " + Convert.ToString(ViewState["Jobid"]);
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
        objPMSMerge.AddMergedColumns(new int[] { 9, 10, 11, 12}, "Dates", "HeaderStyle-css");
        //objPMSMerge.AddMergedColumns(new int[] { 13, 14 }, "Hours", "HeaderStyle-css");
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
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("VID");

            foreach (DataRow dr in DDLVessel.SelectedValues.Rows)
            {
                dtVessel.Rows.Add(dr[0]);
            }

            DataTable dt = objJobStat.PMS_Get_SystemLocation(UDFLib.ConvertToInteger(ddlFunction.SelectedValue), dtVessel);

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
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("VID");

            foreach (DataRow dr in DDLVessel.SelectedValues.Rows)
            {
                dtVessel.Rows.Add(dr[0]);
            }

            if (ddlSystem_location.SelectedValue != "0")
            {
                DataTable dt = objJobStat.PMS_Get_SubSystemLocation(ddlSystem_location.SelectedValue.ToString().Split(',')[1], null, dtVessel);

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

                // Job CMS - Filter
                DataTable dtJobCMS = new DataTable();
                dtJobCMS.Columns.Add("ID", typeof(int));
                dtJobCMS.Columns.Add("CMS_Name", typeof(string));

                dtJobCMS.Rows.Add(1, "CMS");
                dtJobCMS.Rows.Add(0, "Non CMS");

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
           // DDLFleet.Items.Clear();
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


  /// <summary>
  /// Function to Bind Job Status to grid
  /// </summary>
    public void BindJobStatus()
    {
        try
        {
            BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
            DataSet ds = new DataSet();
            
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            int rowcount = ucCustomPagerItems.isCountRecord;
            int odjobcount = 1;
            int critodjobcount = 1;
            int totaljobcount = 1;
            string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int isCritical = 0;
            int isCMS = 0;
            int JobStatus = 0;
            string duedateflagesearch = (ViewState["OverDueSearchFlage"] == null) ? null : (ViewState["OverDueSearchFlage"].ToString());
            string QueryFlage = ViewState["Qflag"].ToString();
            DataTable dtVessel = new DataTable();
            int IsPendingOfcVerify = 0, PostponeJob = 0, FollowupAdded = 0, JobWithMandateRAssess = 0, JobWithSubRAssess = 0, JobDiffToDDock = 0;
            dtVessel.Columns.Add("VID");

            if (rbtDueType.Items[0].Selected && rbtDueType.Items[1].Selected) // This will filter job history with all due and overdue job based on the date specified. If date between is not specified the default date is taken as current date
            {
                JobStatus = 3;

                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
            }
            else if (rbtDueType.Items[0].Selected || rbtDueType.Items[1].Selected)// This will filter job history with either due or overdue job based on the date specified. If date between is not specified the default date is taken as current date
            {
                JobStatus = UDFLib.ConvertToInteger(rbtDueType.SelectedValue);
            }
            else if (rbtDueType.Items[0].Selected == false && rbtDueType.Items[1].Selected == false)/* This will display all the job  */
            {
                JobStatus = 0;
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }

            if (chkOfcVerify.Checked == true)
                IsPendingOfcVerify = 1;

          
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
            int? SafetyAlarm = null;
            int? Calibration = null;

            if (chkAdvSafetyAlarm.Checked == true)
                SafetyAlarm = 1;

            if (chkAdvCalibration.Checked == true)
                Calibration = 1;

            if (chkCritical.Checked == true)
            {
                isCritical = 1;
            }
            else
            {
                isCritical = 0;
            }
            if (chkCMS.Checked == true)
            {
                isCMS = 1;
            }
            else
            {
                isCMS = 0;
            }

            int? Is_RAMandatory = null;       //Added by reshma for RA :For Filtering RA form Mandatory jobs
            int? Is_RASubmitted = null;      //Added by reshma for RA :For Filtering RA form submitted jobs

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

            /* Call Webservice to get JobsCount  */
            List<string> wVessel = new List<string>();
            List<string> wRank = new List<string>();

            foreach (DataRow dr in DDLVessel.SelectedValues.Rows)
            {
                wVessel.Add(dr[0].ToString());
            }

            foreach (DataRow dr in ucf_DDLRank.SelectedValues.Rows)
            {
                wRank.Add(dr[0].ToString());
            }


            string searchjobtitle = txtSearchJobTitle.Text != "" ? txtSearchJobTitle.Text.Trim() : "";

            String GetJobCount = "PMs_Get_OverdueJobsCount('" + jobid + "','" + UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue) + "'," + getArrayString(wVessel.ToArray()) + ",'" + UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue) + "','" + UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[1]) + "','" + UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]) + "','" + UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]) + "','" + UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0]) + "'," + getArrayString(wRank.ToArray()) + ",'" + null + "','" + UDFLib.ConvertStringToNull(searchjobtitle) + "','" + isCritical + "','" + isCMS + "','" + UDFLib.ConvertDateToNull(txtFromDate.Text) + "','" + UDFLib.ConvertDateToNull(txtToDate.Text) + "','" + isHistory + "','" + JobStatus + "','" + duedateflagesearch + "','" + IsPendingOfcVerify + "','" + SafetyAlarm + "','" + Calibration + "','" + UDFLib.ConvertDateToNull(txtActFrmDate.Text) + "','" + UDFLib.ConvertDateToNull(txtActToDate.Text) + "','" + PostponeJob + "','" + FollowupAdded + "','" + JobWithMandateRAssess + "','" + JobWithSubRAssess + "','" + JobDiffToDDock + "','" + Is_RAMandatory + "','" + Is_RASubmitted + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GetJobCount", GetJobCount, true);

            /* Call Webservice to get JobsCount */

            ds = objJobStatus.PMS_Get_OverdueJobs(jobid, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), DDLVessel.SelectedValues, UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue),
                                                  UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]),
                                                  UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0]),
                                                  ucf_DDLRank.SelectedValues, null, txtSearchJobTitle.Text != "" ? txtSearchJobTitle.Text.Trim() : null, isCritical, isCMS,
                                                  UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), isHistory, JobStatus, duedateflagesearch, IsPendingOfcVerify, SafetyAlarm, Calibration,
                                                   UDFLib.ConvertDateToNull(txtActFrmDate.Text), UDFLib.ConvertDateToNull(txtActToDate.Text), PostponeJob, FollowupAdded,
                                                  JobWithMandateRAssess, JobWithSubRAssess, JobDiffToDDock, Is_RAMandatory, Is_RASubmitted, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize,
                                                  ref  rowcount, ref totaljobcount, ref odjobcount, ref critodjobcount);


            DataTable dt = ds.Tables[0];


            if (ucCustomPagerItems.isCountRecord == 1)
            {

                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
         

            }



            gvStatus.DataSource = dt;
            gvStatus.DataBind();

     
            UpdPnlGrid.Update();
            UpdPnlFilter.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string getArrayString(string[] array)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            sb.Append(array[i] + ",");
        }
        string arrayStr = string.Format("[{0}]", sb.ToString().TrimEnd(','));
        return arrayStr;
    }
    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }


    protected void gvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
                ImgHistory.NavigateUrl = "PMSJOBProcess.aspx?Qflag=H&JOB_ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JOB_ID") + "&VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Function_ID=" + DataBinder.Eval(e.Row.DataItem, "FunctionID").ToString() + "&System_ID=" + DataBinder.Eval(e.Row.DataItem, "SysID").ToString() + "&SubSystem_ID=" + DataBinder.Eval(e.Row.DataItem, "SubSysID").ToString() + "&Fleet_ID=" + DataBinder.Eval(e.Row.DataItem, "FleetID").ToString());
            }

            if (lblJobHistoryID.Text == "")
                lblJobHistoryID.Text = "0";

            HyperLink hlnkJobCode = (HyperLink)e.Row.FindControl("hlnkJobCode");
            HyperLink hlnkNextDone = (HyperLink)e.Row.FindControl("hlnkNextDone");
            if (UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "JobHistoryID")) > 0)
            {
                hlnkJobCode.NavigateUrl = "PMSAddAdhocJob.aspx?JOB_HISTORY_ID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&OFFICE_ID=" + DataBinder.Eval(e.Row.DataItem, "OFFICE_ID").ToString() + "&JOB_ID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString() + "&OD_DAYS=" + DataBinder.Eval(e.Row.DataItem, "OD_DAYS").ToString() + "&IsRAMandatory=" + DataBinder.Eval(e.Row.DataItem, "IsRAMandatory").ToString();
                hlnkNextDone.NavigateUrl = "PMSAddAdhocJob.aspx?JOB_HISTORY_ID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&OFFICE_ID=" + DataBinder.Eval(e.Row.DataItem, "OFFICE_ID").ToString() + "&JOB_ID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString() + "&OD_DAYS=" + DataBinder.Eval(e.Row.DataItem, "OD_DAYS").ToString() + "&IsRAMandatory=" + DataBinder.Eval(e.Row.DataItem, "IsRAMandatory").ToString();
            }
            else
            {
                hlnkJobCode.NavigateUrl = "PMSJobIndividualDetails.aspx?JobHistoryID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&VID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Qflag=S&JobID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString();
                hlnkNextDone.NavigateUrl = "PMSJobIndividualDetails.aspx?JobHistoryID=" + DataBinder.Eval(e.Row.DataItem, "JobHistoryID").ToString() + "&VID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Qflag=S&JobID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString();
            }


            ImageButton ImgSpareUsed = (ImageButton)e.Row.FindControl("ImgSpareUsed");
            ImgSpareUsed.Attributes.Add("onclick", "document.getElementById('iFrmJobsDetails').src ='../PMS/PMS_SparesUsed.aspx?JobID=" + DataBinder.Eval(e.Row.DataItem, "JOB_ID").ToString() + "&Vessel_ID=" + lblVesselCode.Text.Trim() + "';showModal('dvJobsDetails');return false;");

            ImageButton ImgRHours = (ImageButton)e.Row.FindControl("ImgRHours");
            if (lblFreqType.Text.ToString() == "2486")
            {
                ImgRHours.Visible = true;
                ImgRHours.Attributes.Add("onClick", "javascript:window.open('../PMS/PMSRunningHours.aspx?VESSEL_ID=" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + "&Function_ID=" + DataBinder.Eval(e.Row.DataItem, "FunctionID").ToString() + "&System_ID=" + DataBinder.Eval(e.Row.DataItem, "SysID").ToString() + "&SubSystem_ID=" + DataBinder.Eval(e.Row.DataItem, "SubSysID").ToString() + "&Fleet_ID=" + DataBinder.Eval(e.Row.DataItem, "FleetID").ToString() + "','_blank');return false");
            }


            if (lblFullJobHistoryRemaks.Text.Length > 15)
                lblJobHistoryRemaks.Text = lblJobHistoryRemaks.Text + "..";


            if (lblJobDescription.Text != "")
                lblJobTitle.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Job Description] body=[" + lblJobDescription.Text + "]");


            if (lblFullJobHistoryRemaks.Text != "")
                lblJobHistoryRemaks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + lblFullJobHistoryRemaks.Text + "]");

            if (lblOverDueFlage.Text == "Y")
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[12].Font.Bold = true;
            }
            if (lblNext30dayFlage.Text == "Y")
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Orange;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[12].Font.Bold = true;
            }
            if (lblCMS.Text == "Y")
            {
                e.Row.Cells[15].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[15].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[15].Font.Bold = true;
            }
            if (lblCritical.Text == "Y")
            {
                e.Row.Cells[16].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[16].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[16].Font.Bold = true;
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)   //Reshma - Remaining Running Hours - when the remaining running hours are negative (meaning the job is overdue), this field (Rem. RHRS) should be painted red.
        {
            Label lblRemHrs = (Label)e.Row.FindControl("lblRemRHrsdone");
            if (lblRemHrs.Text != "")
            {
                if (Convert.ToInt32(lblRemHrs.Text.ToString()) < 0)
                {
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[14].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[14].Font.Bold = true;
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


    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
            int rowcount = ucCustomPagerItems.isCountRecord;
            int odjobcount = 1;
            int critodjobcount = 1;
            int totaljobcount = 1;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string duedateflagesearch = (ViewState["OverDueSearchFlage"] == null) ? null : (ViewState["OverDueSearchFlage"].ToString());
            string QueryFlage = ViewState["Qflag"].ToString();
            int? SafetyAlarm = null;
            int? Calibration = null;
            int JobStatus = 0;
            int IsPendingOfcVerify = 0, PostponeJob = 0, FollowupAdded = 0, JobWithMandateRAssess = 0, JobWithSubRAssess = 0, JobDiffToDDock = 0;
            JobStatus = UDFLib.ConvertToInteger(rbtDueType.SelectedValue);
            if (chkAdvSafetyAlarm.Checked == true)
                SafetyAlarm = 1;
            if (chkAdvCalibration.Checked == true)
                Calibration = 1;

            int isCritical = 0;
            int isCMS = 0;

            int? Is_RAMandatory = null;     //Added by reshma for RA: For Filtering RA mandatory jobs
            int? Is_RASubmitted = null;      //Added by reshma for RA :For Filtering RA form submitted jobs

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

            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("VID");
            if (rbtDueType.Items[0].Selected && rbtDueType.Items[1].Selected) // This will filter job history with all due and overdue job based on the date specified. If date between is not specified the default date is taken as current date
            {
                JobStatus = 3;

                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
            }
            else if (rbtDueType.Items[0].Selected || rbtDueType.Items[1].Selected)// This will filter job history with either due or overdue job based on the date specified. If date between is not specified the default date is taken as current date
            {
                JobStatus = UDFLib.ConvertToInteger(rbtDueType.SelectedValue);
            }
            else if (rbtDueType.Items[0].Selected == false && rbtDueType.Items[1].Selected == false)/* This will display all the job  */
            {
                JobStatus = 0;
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
            foreach (DataRow dr in DDLVessel.SelectedValues.Rows)
            {
                dtVessel.Rows.Add(dr[0]);
            }


            if (chkOfcVerify.Checked == true)
                IsPendingOfcVerify = 1;


            if (chkAdvSafetyAlarm.Checked == true)
                SafetyAlarm = 1;
            if (chkAdvCalibration.Checked == true)
                Calibration = 1;
            int? jobid = null; if (ViewState["Jobid"] != null) jobid = Convert.ToInt32(ViewState["Jobid"].ToString());


            DataSet ds = objJobStatus.PMS_Get_OverdueJobs(jobid, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), dtVessel, UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0]),
                                                ucf_DDLRank.SelectedValues, null, txtSearchJobTitle.Text != "" ? txtSearchJobTitle.Text.Trim() : null, isCritical, isCMS,
                                                UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), 1, JobStatus, duedateflagesearch, IsPendingOfcVerify, SafetyAlarm, Calibration,
                                                 UDFLib.ConvertDateToNull(txtActFrmDate.Text), UDFLib.ConvertDateToNull(txtActToDate.Text), PostponeJob, FollowupAdded,
                                                JobWithMandateRAssess, JobWithSubRAssess, JobDiffToDDock, Is_RAMandatory, Is_RASubmitted, sortbycoloumn, sortdirection, null, null,
                                                ref  rowcount, ref totaljobcount, ref odjobcount, ref critodjobcount);



            string[] HeaderCaptions = { "Vessel", "System Location", "Sub-System Location", "Job Code", "Job Title", "Job Description", "Frequency", "Frequency Name", "Due Date", "Done", "OD.Days", "Next Due", "Rhrs", "Rem Rhrs", "CMS", "Critical","Mandatory Risk Assessment", "Department", "Rank", "Remarks" };
            string[] DataColumnsName = { "Vessel_Name", "Location", "SUBLocation", "Job_Code", "JOB_TITLE", "Job_Description", "FREQUENCY", "Frequency_Name", "DUE_DATE", "NEXT_DONE", "OD_DAYS", "DATE_NEXT_DUE", "RHRS_DONE", "RemRhrs", "CMS", "Critical", "IsRAMandatory", "Department", "RankName", "FullRemarks" };


            string FileName = "PMSJobHistory" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".xls";
            string FilePath = Server.MapPath(@"~/Uploads\\Temp\\");
            string ExportTempFilePath = Server.MapPath(@"~/Uploads\\ExportTemplete\\");

            if ((sender as ImageButton).CommandArgument == "ExportFrom_IE")
            {
                GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Job History", "PMS Job History", "");
            }
            else
            {
                GridViewExportUtil.Export_To_Excel_Interop(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Job History", FilePath + FileName, ExportTempFilePath, @"~\\Uploads\\Temp\\" + FileName);

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


    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
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
        chkAdvSafetyAlarm.Checked = false;
        chkAdvCalibration.Checked = false;
        ddlSystem_location.ClearSelection();
        ucf_DDLRank.ClearSelection();
        ddlSubSystem_location.ClearSelection();

       // rbtDueType.SelectedValue = "0";
        rbtDueType.Items[0].Selected = true;
        rbtDueType.Items[1].Selected = true;
        rbtnJobTypes.SelectedValue = "PMS";
        txtSearchJobTitle.Text = "";
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;

        rbtnMRA.SelectedValue = "ALL";  
        rbtnRASubmitted.SelectedValue = "ALL";

        BindJobStatus();
        UpdPnlFilter.Update();
        UpdPnlGrid.Update();
        UpdAdvFltr.Update();

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

    protected void gvStatus_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindJobStatus();

    }



    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindSystem_Location();

    }

    protected void ddlSystem_location_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindSubSystem_Location();
    }


    protected void DDLVesselApplySearch()
    {
        BindSystem_Location();
        BindSubSystem_Location();
    }

    protected void rbtDueType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            if (rbtDueType.Items[0].Selected == false && rbtDueType.Items[1].Selected == false)
            {
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
            }
            else
            {
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
            }
        
    }



  
}