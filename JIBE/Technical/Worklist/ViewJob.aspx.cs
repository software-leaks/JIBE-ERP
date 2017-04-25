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
using SMS.Business.Technical;
using SMS.Business.Crew;
using System.IO;
using AjaxControlToolkit4;
using SMS.Business.Infrastructure;
using SMS.Business.VET;

public partial class Technical_Job_List_ViewJob : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_VET_Index ObjBLLIndx = new BLL_VET_Index();
    public string OperationMode = "";

    public int OFFID = 0;
    public int WLID = 0;
    public int VID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnLoadFiles.Attributes.Add("style", "visibility:hidden");

            CheckSettingforFunctions();

            if (AjaxFileUpload1.IsInFileUploadPostBack)
            {

            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {

                        OFFID = UDFLib.ConvertToInteger(Request.QueryString["OFFID"]);
                        WLID = UDFLib.ConvertToInteger(Request.QueryString["WLID"]);
                        VID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);


                        DataTable dtpkid = (DataTable)Session["WORKLIST_PKID_NAV"];

                        int indexofCurrentpk = dtpkid != null ? dtpkid.Rows.IndexOf(dtpkid.Rows.Find(new object[] { WLID, VID, OFFID })) : -1;

                        if (indexofCurrentpk < 0)
                        {
                            dtpkid = new DataTable();
                            dtpkid.Columns.Add("WORKLIST_ID");
                            dtpkid.Columns.Add("VESSEL_ID");
                            dtpkid.Columns.Add("OFFICE_ID");
                            dtpkid.PrimaryKey = new DataColumn[] { dtpkid.Columns["WORKLIST_ID"], dtpkid.Columns["VESSEL_ID"], dtpkid.Columns["OFFICE_ID"] };
                            dtpkid.Rows.Add(new object[] { WLID, VID, OFFID });
                            indexofCurrentpk = 0;
                        }

                        ctlRecordNavigationDetails.InitRecords(dtpkid);

                        ctlRecordNavigationDetails.MoveToIndex(indexofCurrentpk);



                    }
                    catch (Exception ex)
                    {
                        UDFLib.WriteExceptionLog(ex);
                    }
                    UserAccessValidation();
                }
                else
                {
                    string js = "$('#myGallery').galleryView();$('#myGallery2').galleryView();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "_galleryView", js, true);
                }
                if (Convert.ToString(Request["Mode"]) == "View")
                {
                    EnableDisableControls(false);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    protected void UserAccessValidation()
    {
        try
        {
                  
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgBtnAddFollowup.Visible = false;
            pnlAddAttachment.Visible = false;
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
       
    }

    private int GetSessionUserID()

    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return 0;
        }
    }
    /// <summary>  
    /// Load all data to respective controls ,when user View job.
    /// </summary>
    /// <param name="drWorklistDetails"> Data row of unique key combination of Vessel ID, Office Id and Worklist ID.</param>
    protected void fillvalue(DataRow drWorklistDetails)
    {
        try
        {

            if (drWorklistDetails != null) 
            {
                int _OfficeID = Convert.ToInt32(drWorklistDetails["office_id"]);
                int WorklistID = Convert.ToInt32(drWorklistDetails["worklist_id"]);
                int VesselID = Convert.ToInt32(drWorklistDetails["vessel_id"]);
                txtFollowupDate.Text = DateTime.Today.ToString("dd/MM/yyyy");


                DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(_OfficeID, WorklistID, VesselID);

                if (dtsJobDetails != null)
                {
                    hdnOfficeID.Value = _OfficeID.ToString();
                    hdnVesselID.Value = VesselID.ToString();
                    hdnWorklistlID.Value = WorklistID.ToString();
                    Session["hdnOfficeID"] = _OfficeID.ToString();
                    Session["hdnVesselIDl"] = VesselID.ToString();
                    Session["hdnWorklistlID"] = WorklistID.ToString();

                    string temp = "";

                    hplCloseThisJob.Visible = false;
                    hplRework.Visible = false;
                    lblWorklistTitle.Text = "";
                    txtWorklistStatusRemark.Text = "";

                    lblFunction.Text = "";
                    lblFunction.Text = dtsJobDetails.Tables[0].Rows[0]["Function_Text"].ToString();
                    lblLocation.Text = "";
                    lblLocation.Text = dtsJobDetails.Tables[0].Rows[0]["Location_Text"].ToString();
                    lblSubLocation.Text = "";
                    lblSubLocation.Text = dtsJobDetails.Tables[0].Rows[0]["Sub_Location_Text"].ToString();

                    hplCloseThisJob.Visible = false;

                    if (dtsJobDetails.Tables[0].Rows[0]["VERIFIED_BY"].ToString() == "")
                    {
                        if (dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() == "NCR" || (dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() != "NCR" && dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "") || (dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() != "NCR" && dtsJobDetails.Tables[0].Rows[0]["Vessel_ID"].ToString() == "11"))
                        {
                            if (objBLL.Get_Worklist_Access_ByUser(Convert.ToInt32(Session["USERID"]), "CLOSE", dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString()))
                            {
                                hplCloseThisJob.Visible = true;
                            }

                        }

                    }


                    hplRework.Visible = false;
                    if (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "")
                    {

                        if (objBLL.Get_Worklist_Access_ByUser(Convert.ToInt32(Session["USERID"]), "REWORK", dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString()))
                        {
                            hplRework.Visible = true;
                        }

                    }


                    string CREATED_DATE = dtsJobDetails.Tables[0].Rows[0]["CREATED_DATE"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["CREATED_DATE"].ToString();
                    string MODIFIED_DATE = dtsJobDetails.Tables[0].Rows[0]["MODIFIED_DATE"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["MODIFIED_DATE"].ToString();
                    string VERIFIED_DATE = dtsJobDetails.Tables[0].Rows[0]["VERIFIED_DATE"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["VERIFIED_DATE"].ToString();
                    string COMPLETED_DATE = dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString();


                    if (dtsJobDetails.Tables[0].Rows.Count > 0)
                    {
                        this.Title = "Job: " + dtsJobDetails.Tables[0].Rows[0]["WLID_DISPLAY"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString();

                        lblVesselName.Text = dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString();
                        lblVesselName.NavigateUrl = "../../Crew/CrewListHistory.aspx?VesselID=" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_ID"].ToString();


                        lblVesselCode.Text = dtsJobDetails.Tables[0].Rows[0]["Vessel_Code"].ToString();
                        lblVesselCode.NavigateUrl = "../../Crew/CrewListHistory.aspx?VesselID=" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_ID"].ToString();

                        lbljobCode.Text = dtsJobDetails.Tables[0].Rows[0]["WLID_DISPLAY"].ToString();
                        lblJobStatus.Text = dtsJobDetails.Tables[0].Rows[0]["WL_STATUS_DISPLAY"].ToString();

                        lblReqsnStatus.Text = " - " + dtsJobDetails.Tables[0].Rows[0]["REQSN_STATUS"].ToString();


                        if (dtsJobDetails.Tables[0].Rows[0]["VESSEL_ID"].ToString().ToUpper() != "11" && dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() == "0")
                        {
                            pnlViewCrewInvolve.Visible = false;
                        }


                        lblCompletedOn.Text = "";

                        if (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "")
                        {
                            temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString()).ToString("dd-MMM-yyyy").ToString();
                            lblCompletedOn.Text = temp == "01-Jan-1900" ? "" : temp;
                        }


                        if (dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() == "NCR")
                        {
                            lblNCRNo.Text = dtsJobDetails.Tables[0].Rows[0]["NCR_NUM"].ToString() + " / " + dtsJobDetails.Tables[0].Rows[0]["NCR_YEAR"].ToString();
                            lblNCRNoCap.Visible = true;
                            pnlNCRRelated.Visible = true;
                            pnlRootCause.Visible = false;
                            lblCompletedOnCaption.Text = "Closed On/By";

                        }
                        else
                        {
                            lblNCRNo.Text = "";
                            lblNCRNoCap.Visible = false;
                            pnlNCRRelated.Visible = false;
                            pnlRootCause.Visible = false;
                        }


                        try
                        {
                            if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_Name"].ToString() != "")
                            {
                                pnlRootCause.Visible = true;


                                lknCompletedBy.Text = dtsJobDetails.Tables[0].Rows[0]["Verified_By_Name"].ToString() + VERIFIED_DATE;
                                if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString() != "")
                                    lknCompletedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString();
                            }
                            else
                            {
                                lknCompletedBy.Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            UDFLib.WriteExceptionLog(ex);
                            lknCompletedBy.Visible = false;
                        }


                        if (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "")
                        {

                            lblCompletedOn.Text = dtsJobDetails.Tables[0].Rows[0]["Modified_By_Name"].ToString() + COMPLETED_DATE;
                            if (dtsJobDetails.Tables[0].Rows[0]["Modified_By_Name"].ToString() != "")
                                lblCompletedOn.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Modified_By_CrewID"].ToString();

                        }
                        else
                        {
                            lblCompletedOn.Visible = false;
                        }

                        if (dtsJobDetails.Tables[0].Rows[0]["DATE_RAISED"].ToString() != "")
                        {
                            temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_RAISED"].ToString()).ToString("dd/MM/yyyy");
                            lblRaisedOn.Text = temp == "01-Jan-1900" ? "" : temp;
                        }
                        if (dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString() != "")
                        {
                            temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString()).ToString("dd/MM/yyyy");
                            lblExpectedCompletion.Text = temp == "01-Jan-1900" ? "" : temp;
                        }


                        lblPIC.Text = dtsJobDetails.Tables[0].Rows[0]["PIC_Name"].ToString();
                        lblDeptonShip.Text = dtsJobDetails.Tables[0].Rows[0]["onship_DEPT"].ToString();
                        lblDeptinOffice.Text = dtsJobDetails.Tables[0].Rows[0]["INOFFICE_DEPT"].ToString();
                        lbljobType.Text = dtsJobDetails.Tables[0].Rows[0]["Worklist_Type"].ToString();//dtsJobDetails.Tables[0].Rows[0]["NCR"].ToString() == "0" ? "JOB" : "NCR";
                        lblAssignedBy.Text = dtsJobDetails.Tables[0].Rows[0]["AssignorName"].ToString();
                       // Added for Vetting
                        if (lblAssignedBy.Text == "Vetting")
                        {
                            pnlVetting.Visible = true;
                           
                        }
                        else
                        {
                            pnlVetting.Visible = false;
                        }
                        lnkRequisitionNumber.Text = Convert.ToString(dtsJobDetails.Tables[0].Rows[0]["REQSN_MSG_REF"].ToString()) == "0" ? "" : dtsJobDetails.Tables[0].Rows[0]["REQSN_MSG_REF"].ToString();

                        if (dtsJobDetails.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString() != "")
                        {
                            lnkRequisitionNumber.NavigateUrl = "~/Purchase/RequisitionSummary.aspx?REQUISITION_CODE=" + dtsJobDetails.Tables[0].Rows[0]["REQSN_MSG_REF"].ToString() + "&Document_Code=" + dtsJobDetails.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString() + "&Vessel_Code=" + VesselID.ToString();
                        }
                        else
                        {
                            lnkRequisitionNumber.NavigateUrl = "";
                        }

                        lblNature.Text = dtsJobDetails.Tables[0].Rows[0]["NATURENAME"].ToString();
                        lblPrimary.Text = dtsJobDetails.Tables[0].Rows[0]["PRIMARYNAME"].ToString();
                        lblSecondary.Text = dtsJobDetails.Tables[0].Rows[0]["SECONDARYNAME"].ToString();
                        lblMinorCategory.Text = Convert.ToString(dtsJobDetails.Tables[0].Rows[0]["CATEGORY_MINOR"]) == "-1" ? "" : Convert.ToString(dtsJobDetails.Tables[0].Rows[0]["MINORNAME"]);
                        lbldescription.Text = dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString().Replace("\n", "<br>");
                        lblCauses.Text = dtsJobDetails.Tables[0].Rows[0]["Causes"].ToString().Replace("\n", "<br>");
                        lblCorrectiveAction.Text = dtsJobDetails.Tables[0].Rows[0]["Correc_Action"].ToString().Replace("\n", "<br>");
                        lblPreventiveAction.Text = dtsJobDetails.Tables[0].Rows[0]["Preven_Action"].ToString().Replace("\n", "<br>");
                        lblDeferToDD.Text = (dtsJobDetails.Tables[0].Rows[0]["DEFER_TO_DD"].ToString() == "0") ? "NO" : "YES";
                        lblPriority.Text = dtsJobDetails.Tables[0].Rows[0]["Priority_Name"].ToString();
                        lblInspector.Text = dtsJobDetails.Tables[0].Rows[0]["Inspector_Name"].ToString();
                        lblPSCSIRE.Text = dtsJobDetails.Tables[0].Rows[0]["PSC_SIRE_Name"].ToString();

                        if (dtsJobDetails.Tables[0].Rows[0]["INSPECTION_DATE"].ToString() != "")
                        {
                            lblInspectionDate.Text = DateTime.Parse(dtsJobDetails.Tables[0].Rows[0]["INSPECTION_DATE"].ToString()).ToString("dd/MM/yyyy");
                        }
                        else
                            lblInspectionDate.Text = "";
                        


                        if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString() != "")
                        {
                            lnkVerifiedBy.Text = dtsJobDetails.Tables[0].Rows[0]["Verified_By_Name"].ToString() + VERIFIED_DATE;
                            lnkVerifiedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString();
                        }
                        else
                        {
                            lnkVerifiedBy.Visible = false;
                        }

                        //------------- Followup list ------------------
                        LoadFollowUps(_OfficeID, VesselID, WorklistID);

                        //--------------Attachments --------------------                
                        Load_Attachments(VesselID, WorklistID, _OfficeID, GetSessionUserID());

                        if (dtsJobDetails.Tables[0].Rows[0]["AssignorName"].ToString().ToUpper() == "CREW COMPLAINT")
                        {
                            Load_CrewComplaintsLog();
                            pnlCrewComplaint.Visible = true;

                        }
                        else
                        {
                            pnlCrewComplaint.Visible = false;
                        }

                        if (WorklistID != 0 && (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() == "01/01/1900" || dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() == ""))
                            btnEditJob.Visible = true;
                        else
                            btnEditJob.Visible = false;


                        //--- Enable/Disable Follow-up and Attachments
                        if (dtsJobDetails.Tables[0].Rows[0]["IsVessel"].ToString() == "1")
                        {
                            if (UDFLib.ConvertToInteger(dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString()) == 0)
                            {
                                ImgBtnAddFollowup.Visible = false;
                                pnlAddAttachment.Visible = false;
                            }
                            else
                            {
                                ImgBtnAddFollowup.Visible = (string.IsNullOrWhiteSpace(lblCompletedOn.Text) != true) ? false : true;
                                pnlAddAttachment.Visible = (string.IsNullOrWhiteSpace(lblCompletedOn.Text) != true) ? false : true;
                            }
                        }
                        else
                        {
                            ImgBtnAddFollowup.Visible = (string.IsNullOrWhiteSpace(lblCompletedOn.Text) != true) ? false : true;
                            pnlAddAttachment.Visible = (string.IsNullOrWhiteSpace(lblCompletedOn.Text) != true) ? false : true;
                        }

                    }

                    // Bind vetting and observation details
                    if (dtsJobDetails.Tables[1].Rows.Count > 0)
                    {
                        lblVetting.Text = dtsJobDetails.Tables[1].Rows[0]["VettingName"].ToString();
                        lblSelectedObs.Text = dtsJobDetails.Tables[1].Rows[0]["Description"].ToString();
                        txtObersvation.Text = dtsJobDetails.Tables[1].Rows[0]["Description"].ToString();
                        BindObservationJobs(int.Parse(dtsJobDetails.Tables[1].Rows[0]["Observation_ID"].ToString()),"Observation");
                        BindVettingJobs(int.Parse(dtsJobDetails.Tables[1].Rows[0]["Vetting_ID"].ToString()),"Vetting");

                        
                    }
                    // Bind Observation Response
                    if (dtsJobDetails.Tables[2].Rows.Count > 0)
                    {
                        GvObservation.DataSource = dtsJobDetails.Tables[2];
                        GvObservation.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }

        UpdatePanel2.Update();
    }
    /// <summary>
    /// Display the jobs that related to this specific observation
    /// </summary>
    /// <param name="FilterJobsBy">Passed observation ID</param>
    /// <param name="Mode">Mode is Observation</param>
    protected void BindObservationJobs( int FilterJobsBy,string Mode)
    {
        try
        {
            DataTable dt = ObjBLLIndx.VET_Get_Vetting_Jobs_Details(FilterJobsBy, Mode);
            GvObservationJobs.DataSource = dt;
            GvObservationJobs.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Display all the jobs related to this specific vetting
    /// </summary>
    /// <param name="FilterJobBy">Passed Vetting ID</param>
    /// <param name="Mode">Mode is Vetting</param>
    protected void BindVettingJobs(int FilterJobBy, string Mode)
    {
        try
        {
            DataTable dt = ObjBLLIndx.VET_Get_Vetting_Jobs_Details(FilterJobBy, Mode);
            GvVettingJobs.DataSource = dt;
            GvVettingJobs.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void Load_CrewComplaintsLog()
    {
        try
        {
        int Worklist_ID = UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString());
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());
        int USERID = UDFLib.ConvertToInteger(Session["USERID"].ToString());

        DataSet ds = objBLLCrew.Get_CrewComplaintLog(Worklist_ID, Vessel_ID, USERID);

        rptComplaintsToDPA.DataSource = ds.Tables[0];
        rptComplaintsToDPA.DataBind();

        DataRow[] dr = ds.Tables[0].Select("escalation_level>=3");


        if (dr.Length == 1)
        {
            btnReleaseToFlag.Enabled = true;
            pnlReleaseToFlag.Visible = true;
        }
        else if (dr.Length == 2)
        {
            btnReleaseToFlag.Enabled = false;
            pnlReleaseToFlag.Visible = false;
        }
        else
        {
            pnlReleaseToFlag.Visible = false;
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }


    private void CheckSettingforFunctions()
    {

        DataTable _settingTable;
        DataRow[] foundRows;

        try
        {
            _settingTable = objBLL.GetSettingforFunctions();


            if (_settingTable.Rows.Count > 0)
            {
                foundRows = _settingTable.Select("Settings_Key = 'View Functions To Jobs'");

                if (foundRows.Length > 0)
                {
                    if (_settingTable.Rows[0]["Settings_Value"].ToString() == "0")                  //Convert datatype bit to varchar  
                    //if ((bool)foundRows[0]["Settings_Value"] == false)
                    {
                        tblFunction.Visible = false;
                    }
                    else
                    {
                        tblFunction.Visible = true;
                    }
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
        }

    }

    protected void btnSaveFollowUpAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtMessage.Text.Trim().Length == 0)
            {
                string OpenFollowupDiv = "alert('Message description is mandatory field.');OpenFollowupDiv();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenFollowupDiv", OpenFollowupDiv, true);
                return;
            }

            int iJob_OfficeID = int.Parse(hdnOfficeID.Value);
            int Worklist_ID = int.Parse(hdnWorklistlID.Value);
            int VESSEL_ID = int.Parse(hdnVesselID.Value);

            string FOLLOWUP = txtMessage.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());
            int TOSYNC = 1;

            int newFollowupID = objBLL.Insert_Followup(iJob_OfficeID, Worklist_ID, VESSEL_ID, FOLLOWUP, CREATED_BY, TOSYNC);

            DataTable dtpkid = (DataTable)Session["WORKLIST_PKID_NAV"];

            if (dtpkid != null)
            {
                fillvalue(dtpkid.Rows.Find(new object[] { Worklist_ID, VESSEL_ID, iJob_OfficeID }));
            }
            else
            {
                dtpkid = new DataTable();
                dtpkid.Columns.Add("WORKLIST_ID", typeof(string));
                dtpkid.Columns.Add("VESSEL_ID", typeof(string));
                dtpkid.Columns.Add("OFFICE_ID", typeof(string));

                dtpkid.Rows.Add(new object[] { Worklist_ID, VESSEL_ID, iJob_OfficeID });
                dtpkid.PrimaryKey = new DataColumn[] { dtpkid.Columns["WORKLIST_ID"], dtpkid.Columns["VESSEL_ID"], dtpkid.Columns["OFFICE_ID"] };

                fillvalue(dtpkid.Rows.Find(new object[] { Worklist_ID, VESSEL_ID, iJob_OfficeID }));
            }
            txtMessage.Text = ""; /// Added by Anjali DT:17-May-2016 JIT:9604 || To clear fields after adding follow up.
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    private void LoadFollowUps(int OFFICE_ID, int VESSEL_ID, int WORKLIST_ID)
    {
        try
        {
        DataTable dtFollowUps = objBLL.Get_FollowupList(OFFICE_ID, VESSEL_ID, WORKLIST_ID);

        grdFollowUps.DataSource = dtFollowUps;
        grdFollowUps.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnReleaseToFlag_Click(object sender, EventArgs e)
    {
        try
        {
        int OFFICE_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
        int WORKLIST_ID = Convert.ToInt32(Request.QueryString["WLID"]);
        int VESSEL_ID = Convert.ToInt32(Request.QueryString["VID"]);
        int USERID = int.Parse(Session["USERID"].ToString());
        if (txtDPARemark.Text.Trim() == "")
        {
            string js = "alert('Please enter remarks.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDPA", js, true);
        }
        else
        {
            objBLL.ReleaseComplaint_ToFlag(VESSEL_ID, WORKLIST_ID, OFFICE_ID, USERID, txtDPARemark.Text);

            SendMail_ReleaseToFlag(VESSEL_ID, WORKLIST_ID, OFFICE_ID, USERID);
            Load_CrewComplaintsLog();
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void SendMail_ReleaseToFlag(int VESSEL_ID, int WORKLIST_ID, int OFFICE_ID, int USERID)
    {
   try
   {
        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";

        string msgBody = "";
        string msgSubject = "";

        DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

        DataSet ds = objBLLCrew.Get_CrewComplaintLog(WORKLIST_ID, VESSEL_ID, USERID);

        DataTable dtMain = dtsJobDetails.Tables[0];
        DataTable dtDetail = ds.Tables[0];

        DataRow[] dr1 = ds.Tables[0].Select("escalation_level = 1");


        foreach (DataRow dr in dtMain.Rows)
        {
            msgSubject = "Crew complaint  escalated to Flag: " + dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString() + " - " + dr1[0]["escalated_by_staff_code"].ToString() + " - " + dr1[0]["escalated_by_name"].ToString();

            msgBody = "Dear Sir/Madam,";
            msgBody += "<br><br>";

            msgBody += "Crew complaint from vessel " + dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString() + " has been escalated for your attention.";
            msgBody += "<br><br>";

            msgBody += "Escalated By: " + dr1[0]["escalated_by_name"].ToString() + "<br>";
            msgBody += "Escalated On: " + dr1[0]["escalated_on"].ToString() + "<br>";

            msgBody += "Complaint: " + dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString().Replace("\n", "<br>");


            int MsgID = objBLLCrew.Send_CrewNotification(0, 0, 0, 0, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
            if (MsgID > 0)
            {
                string js = "window.open('../../Crew/EmailEditor.aspx?discard=1&ID=" + MsgID + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NotifyFlag", js, true);
            }

        }
   }
   catch (Exception ex)
   {
       UDFLib.WriteExceptionLog(ex);
   }

    }

    private void Load_Attachments(int VESSEL_ID, int WORKLIST_ID, int WL_OFFICE_ID, int UserID)
    {   try
    {

        DataTable dt = objBLL.Get_Worklist_Attachments(VESSEL_ID, WORKLIST_ID, WL_OFFICE_ID, UserID);
        DataView dvImage = dt.DefaultView;
        dvImage.RowFilter = "Is_Image='1' ";


        ListView1.DataSource = dvImage;
        ListView1.DataBind();
        ListView2.DataSource = dvImage;
        ListView2.DataBind();

        hidenTotalrecords.Value = dvImage.Count.ToString();
        HCurrentIndex.Value = "0";
        if (dvImage.Count == 0)
        {
            tdg.Visible = false;
        }
        else
        {
            tdg.Visible = true;
        }

        dt.DefaultView.RowFilter = "Is_Image='0'  ";//AND Is_Audio='1'
        gvAttachments.DataSource = dt.DefaultView;
        gvAttachments.DataBind();

        //Bind Popup
    }
    catch (Exception ex)
    {
        UDFLib.WriteExceptionLog(ex);
    }

    }

    public string GetFileName(string sFilePath)
    {
        return sFilePath;
    }

    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Attachment_ID = DataBinder.Eval(e.Row.DataItem, "Attachment_ID").ToString();
            string VESSEL_ID = DataBinder.Eval(e.Row.DataItem, "VESSEL_ID").ToString();
            string WORKLIST_ID = DataBinder.Eval(e.Row.DataItem, "WORKLIST_ID").ToString();
            string WL_OFFICE_ID = DataBinder.Eval(e.Row.DataItem, "WL_OFFICE_ID").ToString();


            HyperLink lnk = (HyperLink)(e.Row.FindControl("lblAttach_Name"));
            if (lnk != null)
            {
                lnk.NavigateUrl = GetAttachmentURL(VESSEL_ID, WORKLIST_ID, WL_OFFICE_ID, Attachment_ID);
            }
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {       
        try
        {
        /*Change by pranali_03072015 for JIT_3283 iMAGES ARE SAVING UNDER DIFFRENT WORKLIST ID 
         *Due to above variables into session updated values not getting so that these values replaced with querystring values. 
         */
        int Office_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
        int Worklist_ID = Convert.ToInt32(Request.QueryString["WLID"]);
        int Vessel_ID = Convert.ToInt32(Request.QueryString["VID"]);


        DataTable dtpkid = (DataTable)Session["WORKLIST_PKID_NAV"];

        fillvalue(dtpkid.Rows.Find(new object[] { Worklist_ID, Vessel_ID, Office_ID }));
        string js = "loadImageRotate();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ldimg", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        }
 

    public static long GetFileSize(string file)
    {
        FileInfo info = new FileInfo(file);
        long SIZE_BYTES = info.Length;    
        return SIZE_BYTES;
    }

    public string GetAttachmentURL(string VESSEL_ID, string WORKLIST_ID, string WL_OFFICE_ID, string AttachID)
    {
        return "Attachments.aspx?vid=" + VESSEL_ID + "&wlid=" + WORKLIST_ID + "&wl_off_id=" + WL_OFFICE_ID + "&AttId=" + AttachID;
    }

    protected void btnEditJob_Click(object sender, EventArgs e)
    {
        try
        {
        string OFFICE_ID = hdnOfficeID.Value;
        string WORKLIST_ID = hdnWorklistlID.Value;
        string VESSEL_ID = hdnVesselID.Value;

        Response.Redirect("addnewjob.aspx?OFFID=" + OFFICE_ID + "&WLID=" + WORKLIST_ID + "&VID=" + VESSEL_ID);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>  
    /// To verify pending NCR job.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void btnSaveStatus_Click(object s, EventArgs e)
    {
        try
        {

            objBLL.Upd_Worklist_Status(Convert.ToInt32(hdnVesselID.Value), Convert.ToInt32(hdnWorklistlID.Value), Convert.ToInt32(hdnOfficeID.Value), Convert.ToInt32(Session["USERID"]), txtWorklistStatusRemark.Text, Convert.ToString(ViewState["WORKLIST_STATUS"]));
            int Vessel_ID = UDFLib.ConvertToInteger(hdnVesselID.Value);
            int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklistlID.Value);
            int Office_ID = UDFLib.ConvertToInteger(hdnOfficeID.Value);

            DataTable dtpkid = (DataTable)Session["WORKLIST_PKID_NAV"];
                       
            // Same dataTable row pass to function 'fillvalue' that loads all job details.
            if (dtpkid == null)
            {
                dtpkid = new DataTable();

                dtpkid.Columns.Add("WORKLIST_ID", typeof(string));
                dtpkid.Columns.Add("VESSEL_ID", typeof(string));
                dtpkid.Columns.Add("OFFICE_ID", typeof(string));

                dtpkid.Rows.Add(new object[] { Worklist_ID, Vessel_ID, Office_ID });

                dtpkid.PrimaryKey = new DataColumn[] { dtpkid.Columns["WORKLIST_ID"], dtpkid.Columns["VESSEL_ID"], dtpkid.Columns["OFFICE_ID"] };
            }

            if (dtpkid.Rows.Find(new object[] { Worklist_ID, Vessel_ID, Office_ID }) == null)
            {
                dtpkid.Rows.Add(new object[] { Worklist_ID, Vessel_ID, Office_ID });
            }
            fillvalue(dtpkid.Rows.Find(new object[] { Worklist_ID, Vessel_ID, Office_ID }));

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void imgEmail_Click(object s, EventArgs e)
    {
        try
        {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVesselID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklistlID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOfficeID.Value);

        Send_Mail_Job_Details(Office_ID, Worklist_ID, Vessel_ID);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void Send_Mail_Job_Details(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    {
        try
        {
            int MsgID = objBLL.Create_Mail_Job_Details(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
            if (MsgID > 0)
            {
                string js = "window.open('../../Crew/EmailEditor.aspx?ID=" + MsgID + "&Discard=1');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SendMailJs" + MsgID, js, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }

    protected void hplRework_Click(object sender, EventArgs e)
    {
       try
       {
        ViewState["WORKLIST_STATUS"] = "REWORKED";
        lblWorklistTitle.Text = "Rework job";
        txtWorklistStatusRemark.Text = "";
        UpdatePanel2.Update();

        string js = "showModal('dvReworkClose');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }
    }

    protected void hplCloseThisJob_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["WORKLIST_STATUS"] = "CLOSED";
            lblWorklistTitle.Text = "Verify and close job";
            txtWorklistStatusRemark.Text = "";

            UpdatePanel2.Update();

            string js = "showModal('dvReworkClose');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);
        }
          
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>  
    /// To save attached files to DB.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="file"></param>
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Technical");
            string sPath1 = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Inspection");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int Office_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
            int Worklist_ID = Convert.ToInt32(Request.QueryString["WLID"]);
            int Vessel_ID = Convert.ToInt32(Request.QueryString["VID"]);
            string _fileName = "";
            
            int FileID = objBLL.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, Office_ID, UDFLib.Remove_Special_Characters(file.FileName), Flag_Attach, file.FileSize, 
                                                            UDFLib.ConvertToInteger(Session["USERID"]));

            _fileName = "TEC_" + Vessel_ID + "_" + Worklist_ID + "_" + Office_ID + "_" + "O" + "_" + FileID.ToString() + "_" + Flag_Attach;

            // This method insert attachments in object table and it will sync to mobile by Pranav Sakpal on 2016-03-23
            int Ret = objBLL.Insert_ActivityObject(Vessel_ID, Worklist_ID, Office_ID, _fileName, _fileName, UDFLib.ConvertToInteger(Session["USERID"]));


            string FullFilename = Path.Combine(sPath, _fileName);

            //Save attached file to folder
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

            // This will copy attachments in inspection folder it will sync to mobile by Pranav Sakpal on 2016-03-23
            string filecopyname = _fileName;
            File.Copy(FullFilename, Path.Combine(sPath1, filecopyname), true);


            Load_Attachments(Vessel_ID, Worklist_ID, Office_ID, GetSessionUserID());
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }



    private void EnableDisableControls(bool Mode)
    {

        imgEmail.Enabled = Mode;

        lnkRequisitionNumber.Enabled = Mode;

        hplRework.Enabled = Mode;

        hplCloseThisJob.Enabled = Mode;

        lnkVerifiedBy.Enabled = Mode;

        pnlReleaseToFlag.Enabled = Mode;

        txtDPARemark.Enabled = Mode;

        btnReleaseToFlag.Enabled = Mode;

        ImgBtnAddFollowup.Enabled = Mode;

        grdFollowUps.Enabled = Mode;

        pnlAddAttachment.Enabled = Mode;

        ListView1.Enabled = Mode;

        gvAttachments.Enabled = Mode;

        pnlViewCrewInvolve.Enabled = Mode;    

        btnEditJob.Enabled = Mode;

        btnClose.Enabled = Mode;

        txtFollowupDate.Enabled = Mode;

        txtMessage.Enabled = Mode;

        btnSaveFollowUpAndClose.Enabled = Mode;

        txtWorklistStatusRemark.Enabled = Mode;

        btnSaveStatus.Enabled = Mode;
        AjaxFileUpload1.Enabled = Mode;
    }

    private bool CANPLAY(string s)
    {

        if (s == "1")
        {
            return true;
        }
        else
            return false;
    }

    protected void GvObservationJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDATE_ESTMTD_CMPLTN = (Label)e.Row.FindControl("lblDATE_ESTMTD_CMPLTN");
                Label lblCompletedOn = (Label)e.Row.FindControl("lblCompletedOn");
                if (lblDATE_ESTMTD_CMPLTN.Text != "")
                {
                    lblDATE_ESTMTD_CMPLTN.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblDATE_ESTMTD_CMPLTN.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
                if (lblCompletedOn.Text != "")
                {
                    lblCompletedOn.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblCompletedOn.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
               
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GvVettingJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDATE_ESTMTD_CMPLTN = (Label)e.Row.FindControl("lblDATE_ESTMTD_CMPLTN");
                Label lblgrdCompletedOn = (Label)e.Row.FindControl("lblgrdCompletedOn");
                if (lblDATE_ESTMTD_CMPLTN.Text != "")
                {
                    lblDATE_ESTMTD_CMPLTN.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblDATE_ESTMTD_CMPLTN.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
                if (lblgrdCompletedOn.Text != "")
                {
                    lblgrdCompletedOn.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblgrdCompletedOn.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}