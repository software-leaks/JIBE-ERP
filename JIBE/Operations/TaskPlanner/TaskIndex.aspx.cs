using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SMS.Business.Infrastructure;
using SMS.Business.Operation;
using SMS.Business.Crew;
using SMS.Business.Technical;

using SMS.Properties;
using System.Configuration;
using System.Text;
using System.Data;
using AjaxControlToolkit4;
using System.IO;


public partial class Operations_TaskPlanner_TaskIndex : System.Web.UI.Page
{

    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Tec_Worklist objBLLTech = new BLL_Tec_Worklist();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnLoadFiles.Attributes.Add("style", "visibility:hidden");
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("OPS_");
        AjaxFileUpload1.MaximumSizeOfFile = Int32.Parse(dt.Rows[0]["Size_KB"].ToString());
        //AjaxFileUpload1.MaximumSizeOfFile = 5;
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            string toDate = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
            string fromDate = "01/01/2010"; //Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));

            if (Session["USERID"] == null)
                Response.Redirect("~/account/login.aspx");

            if (!IsPostBack == true)
            {

                if (Convert.ToString(Request.QueryString["ViewPendingVerification"]) == "1")
                {
                    rdoJobStaus.ClearSelection();
                    rdoJobStaus.Items.FindByValue("3").Selected = true;
                }

                txtFromDate.Text = fromDate;
                txtToDate.Text = toDate;

                //BindPIC();
                Load_FleetList();
                Load_VesselList();
                UserAccessValidation();
                Load_Tasks();

                string UserAgent = Request.UserAgent;
                string Browser = "";

                if (UserAgent.IndexOf("MSIE") != -1) Browser = "MSIE";
                if (UserAgent.IndexOf("Chrome") != -1) Browser = "Chrome";
                if (UserAgent.IndexOf("Firefox") != -1) Browser = "Firefox";
                if (UserAgent.IndexOf("Safari") != -1) Browser = "Safari";

                //if (Browser == "Firefox" || Browser == "Chrome")
                //{
                //    dvUploader_Fx.Visible = true;
                //    dvUploader_MSIE.Visible = false;
                //}
                //else
                //{
                //    dvUploader_Fx.Visible = false;
                //    dvUploader_MSIE.Visible = true;
                //}
            }

            //ucJQueryUpload1.Init_InputFileUpload("Uploads\\Travel", "JQueryAttachmentUploader.ashx") ;
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAddNewTask.Enabled = false;
            btnSaveFollowUpAndClose.Enabled = false;
            btnAddNewFollowUpAndClose.Enabled = false;
            btnSavePortCall.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            //grdJoblist.Columns[grdJoblist.Columns.Count - 3].Visible = false;
            btnEditTask.Enabled = false;
            btnUpdateTaskStatus.Enabled = false;
            btnSavePortCall.Enabled = false;
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {
            btnSaveVerify.Enabled = false;
        }
    }

    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objBLLVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = 1;

        ddlVessels.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        ddlVessels.DataTextField = "VESSEL_NAME";
        ddlVessels.DataValueField = "VESSEL_ID";
        ddlVessels.DataBind();
        ddlVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessels.SelectedIndex = 0;
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_Tasks();
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        Load_Tasks();
    }

    protected void Load_Tasks()
    {
        //try
        //{

        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager.isCountRecord;
        string SortColumn = "";

        if (ViewState["SortExpression"] != null)
            SortColumn = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();

        int iSortDirection = 0;

        int isPrivate = chkPrivateFilter.Checked == true ? 1 : 0;

        DataTable dtSearchResult = BLL_OPS_TaskPlanner.Get_TaskList(UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue),
                                                                    UDFLib.ConvertIntegerToNull(ddlVessels.SelectedValue),
                                                                    UDFLib.ConvertIntegerToNull(ddlCatFilter.SelectedValue),
                                                                    UDFLib.ConvertIntegerToNull(lstPICFilter.SelectedValue),
                                                                    UDFLib.ConvertIntegerToNull(rdoJobStaus.SelectedValue),
                                                                    UDFLib.ConvertDateToNull(txtFromDate.Text),
                                                                    UDFLib.ConvertDateToNull(txtToDate.Text),
                                                                    UDFLib.ConvertDateToNull(txtExpectedCompFrom.Text),
                                                                    UDFLib.ConvertDateToNull(txtExpectedCompTo.Text),
                                                                    GetSessionUserID(),
                                                                    UDFLib.ConvertStringToNull(txtDescription.Text),


                                                                    PAGE_SIZE, PAGE_INDEX,
                                                                    ref SelectRecordCount,
                                                                    SortColumn,
                                                                    iSortDirection,
                                                                    isPrivate,
                                                                     UDFLib.ConvertIntegerToNull(txtModifiedInDays.Text)
                                                                    );

        if (ucCustomPager.isCountRecord == 1)
        {
            //lblRecordCount.Text = SelectRecordCount.ToString();
            ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager.BuildPager();
        }

        grdJoblist.DataSource = dtSearchResult;
        grdJoblist.DataBind();
        UpdatePanel_Main.Update();
        ctlRecordNavigationTask.InitRecords(dtSearchResult);
        //}
        //catch (Exception ex)
        //{
        //    ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        //    string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        //}
    }

    public void Load_TaskDetails(int Worklist_ID, int Vessel_ID, int Office_ID)
    {
        try
        {
            DataSet dsTask = BLL_OPS_TaskPlanner.Get_TaskDetails(Worklist_ID, Vessel_ID, Office_ID, GetSessionUserID());

            frmTaskDetails.DataSource = dsTask.Tables[0];
            frmTaskDetails.DataBind();
            if (((GridView)frmTaskDetails.FindControl("grdFollowUps")) != null)
            {
                ((GridView)frmTaskDetails.FindControl("grdFollowUps")).DataSource = dsTask.Tables[1];
                ((GridView)frmTaskDetails.FindControl("grdFollowUps")).DataBind();
            }

            //if (((GridView)frmTaskDetails.FindControl("gvAttachments")) != null)
            //{
            //    ((GridView)frmTaskDetails.FindControl("gvAttachments")).DataSource = dsTask.Tables[2];
            //    ((GridView)frmTaskDetails.FindControl("gvAttachments")).DataBind();
            //}

            gvAttachments.DataSource = dsTask.Tables[2];
            gvAttachments.DataBind();

            if ((Session["userid"].ToString() == "15" || Session["userid"].ToString() == "42" || Session["userid"].ToString() == "53" || Session["userid"].ToString() == "30") && (dsTask.Tables[0].Rows[0]["IsVerified"].ToString() == "0" && dsTask.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != ""))
                btnVerifyFromDetails.Visible = true;
            else
                btnVerifyFromDetails.Visible = false;

            //ctlUploadAttachment1.Worklist_ID = Worklist_ID;
            //ctlUploadAttachment1.Vessel_ID = Vessel_ID;
            //ctlUploadAttachment1.WL_Office_ID = Office_ID;
            //ctlUploadAttachment1.UserID = GetSessionUserID();
            //ctlUploadAttachment1.InitControl();

            //DnDUploader1.Worklist_ID = Worklist_ID;
            //DnDUploader1.Vessel_ID = Vessel_ID;
            //DnDUploader1.WL_Office_ID = Office_ID;
            //DnDUploader1.UserID = GetSessionUserID();
            //DnDUploader1.InitControl();

            UpdatePanel_Details.Update();

            //ucJQueryUpload1.Init_InputFileUpload("Uploads\\Technical", "JQueryAttachmentUploader.ashx?Vessel_ID=" + Vessel_ID.ToString() + "&Worklist_ID=" + Worklist_ID.ToString() + "&WL_Office_ID=" + Office_ID.ToString());

        }
        catch
        { }
    }

    public void Load_TaskDetails_Navigate(DataRow drTask)
    {
        hdnVessel_ID.Value = drTask["Vessel_ID"].ToString();
        hdnWorklist_ID.Value = drTask["Worklist_ID"].ToString();
        hdnOffice_ID.Value = drTask["Office_ID"].ToString();

        Session["hdnOfficeID"] = drTask["Office_ID"].ToString();
        Session["hdnVesselID"] = drTask["Vessel_ID"].ToString();
        Session["hdnWorklistlID"] = drTask["Worklist_ID"].ToString();

        Load_TaskDetails(UDFLib.ConvertToInteger(drTask["Worklist_ID"]), UDFLib.ConvertToInteger(drTask["Vessel_ID"]), UDFLib.ConvertToInteger(drTask["Office_ID"]));


    }

    protected void ImgBtnClearFilter_Click(object sender, EventArgs e)
    {
        string toDate = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        string fromDate = Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));

        ddlFleet.SelectedIndex = 0;
        ddlVessels.SelectedIndex = 0;
        ddlCatFilter.SelectedIndex = 0;

        txtFromDate.Text = fromDate;
        txtToDate.Text = toDate;
        lstPICFilter.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        rdoJobStaus.SelectedIndex = 2;
        txtExpectedCompTo.Text = "";
        txtExpectedCompFrom.Text = "";

        Load_Tasks();
    }

    protected void grdJoblist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdJoblist.PageIndex = e.NewPageIndex;
        Load_Tasks();
    }

    protected void grdJoblist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName.ToUpper() == "SELECT_PORTCALL")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');

                int Vessel_ID = UDFLib.ConvertToInteger(arg[0]);
                int Worklist_ID = UDFLib.ConvertToInteger(arg[1]);
                int Office_ID = UDFLib.ConvertToInteger(arg[2]);
                string Status = arg[3];
                string P_C_ID = arg[4];

                if (Vessel_ID > 0)
                {
                    gvPortCalls.DataSource = objCrew.Get_PortCall_List(Vessel_ID);
                    gvPortCalls.DataBind();

                    hdnVessel_ID.Value = Vessel_ID.ToString();
                    hdnWorklist_ID.Value = Worklist_ID.ToString();
                    hdnOffice_ID.Value = Office_ID.ToString();

                    if (Status == "1")
                    {
                        btnSavePortCall.Visible = false;
                    }
                    else
                    {
                        btnSavePortCall.Visible = true;
                    }

                    UpdatePanel_PortCalls.Update();
                    string js = "showModal('dvVesselMovement')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "portcall", js, true);
                }

            }

            if (e.CommandName.ToUpper() == "UPDATE_STATUS")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');

                int Vessel_ID = UDFLib.ConvertToInteger(arg[0]);
                int Worklist_ID = UDFLib.ConvertToInteger(arg[1]);
                int Office_ID = UDFLib.ConvertToInteger(arg[2]);
                string Status = arg[3];
                string Date_Completed = arg[4];
                string IsVerified = arg[5];

                hdnVessel_ID.Value = Vessel_ID.ToString();
                hdnWorklist_ID.Value = Worklist_ID.ToString();
                hdnOffice_ID.Value = Office_ID.ToString();

                //rdoTaskStatusUpdate.Text = Status;

                txtCompletionDate.Text = Date_Completed;

                if (Status == "1")
                {
                    lblTaskStatus.Text = "Complete";
                    //rdoTaskStatusUpdate.Enabled = false;
                    txtCompletionDate.Enabled = false;
                    txtCompletionRemark.Enabled = false;
                    btnUpdateTaskStatus.Visible = false;
                }
                else
                {
                    lblTaskStatus.Text = "Pending";
                    //rdoTaskStatusUpdate.Enabled = true;
                    txtCompletionDate.Enabled = true;
                    txtCompletionRemark.Enabled = true;
                    btnUpdateTaskStatus.Visible = true;
                }
                UpdatePanel_UpdateStatus.Update();

                if (Vessel_ID > 0)
                {
                    string js = "showModal('dvUpdateStatus')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update", js, true);
                }


                if ((Session["userid"].ToString() == "15" || Session["userid"].ToString() == "42" || Session["userid"].ToString() == "53" || Session["userid"].ToString() == "30") && IsVerified == "0" && Date_Completed != "")
                {
                    btnSaveVerify.Visible = true;
                }
                else
                {
                    btnSaveVerify.Visible = false;
                }
            }

            if (e.CommandName.ToUpper() == "VIEW_TASK")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');

                int Vessel_ID = UDFLib.ConvertToInteger(arg[0]);
                int Worklist_ID = UDFLib.ConvertToInteger(arg[1]);
                int Office_ID = UDFLib.ConvertToInteger(arg[2]);
                string Status = arg[3];

                btnEditTask.Visible = true;
                hdnVessel_ID.Value = Vessel_ID.ToString();
                hdnWorklist_ID.Value = Worklist_ID.ToString();
                hdnOffice_ID.Value = Office_ID.ToString();


                Session["hdnOfficeID"] = Office_ID.ToString();
                Session["hdnVesselID"] = Vessel_ID.ToString();
                Session["hdnWorklistlID"] = Worklist_ID.ToString();


                if (Status == "1" && arg[4] == "0")
                {
                    btnEditTask.Visible = false;
                    if (Session["userid"].ToString() == "15" || Session["userid"].ToString() == "42" || Session["userid"].ToString() == "53" || Session["userid"].ToString() == "30")
                        btnVerifyFromDetails.Visible = true;
                }
                else if (Status == "1" && arg[4] == "1")
                {
                    btnEditTask.Visible = false;
                    btnVerifyFromDetails.Visible = false;
                }
                else
                {
                    btnEditTask.Visible = true;
                    btnVerifyFromDetails.Visible = false;
                }

                if (Vessel_ID > 0)
                {

                    frmTaskDetails.ChangeMode(FormViewMode.ReadOnly);

                    frmTaskDetails.DataSource = null;
                    frmTaskDetails.DataBind();

                    Load_TaskDetails(Worklist_ID, Vessel_ID, Office_ID);

                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);//
                    int index = gvRow.RowIndex;

                    ctlRecordNavigationTask.CurrentIndex = index;
                    ctlRecordNavigationTask.Visible = true;

                    //Added by Anjali. to display heat map information on tooltip
                    string wh = "OFFICE_ID= " + Office_ID.ToString() + " and WORKLIST_ID=" + Worklist_ID.ToString() + " and VESSEL_ID=" + Vessel_ID.ToString() + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Get_Record_Information_Details('TEC_WORKLIST_MAIN','" + wh.ToString() + "');", true);

                    string js = "showModal('dvTaskDetails',false)";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js, true);

                }

            }

            if (e.CommandName.ToUpper() == "ADD_FOLLOWUP")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');

                int Vessel_ID = UDFLib.ConvertToInteger(arg[0]);
                int Worklist_ID = UDFLib.ConvertToInteger(arg[1]);
                int Office_ID = UDFLib.ConvertToInteger(arg[2]);

                hdnVessel_ID.Value = Vessel_ID.ToString();
                hdnWorklist_ID.Value = Worklist_ID.ToString();
                hdnOffice_ID.Value = Office_ID.ToString();

                if (Vessel_ID > 0)
                {
                    string js = "showModal('dvAddNewFollowup');$('#dialog').hide();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js, true);
                }

            }
            if (e.CommandName.ToUpper() == "RE_ACTIVATE")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');
                int sts = BLL_OPS_TaskPlanner.ReActivate_Task(UDFLib.ConvertToInteger(arg[1]), UDFLib.ConvertToInteger(arg[0]), UDFLib.ConvertToInteger(arg[2]), Convert.ToInt32(Session["userid"].ToString()));
                if (sts > 0)
                {
                    string js = "alert('Re Activated successfully.')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ReActivated ", js, true);
                    Load_Tasks();
                }
            }
            if (e.CommandName.ToUpper() == "EMAILJOB")
            {
                // OFFICE_ID , WLID , VESSEL_ID 
                string[] arg = e.CommandArgument.ToString().Split(',');
                Send_Mail_Job_Details(UDFLib.ConvertToInteger(arg[0]), UDFLib.ConvertToInteger(arg[1]), UDFLib.ConvertToInteger(arg[2]));
            }
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
            string msgSubject = "";
            string msgTo = "";
            string msgCC = "";
            string msgBCC = "";

            DataSet dtsJobDetails = BLL_OPS_TaskPlanner.Get_TaskDetails(WORKLIST_ID, VESSEL_ID, OFFICE_ID, GetSessionUserID());

            if (dtsJobDetails.Tables[0].Rows.Count > 0)
            {
                if (dtsJobDetails.Tables[0].Rows[0]["IsVessel"].ToString() == "1")
                    msgSubject = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString();
                else
                    msgSubject = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString() + "/PIC:" + dtsJobDetails.Tables[0].Rows[0]["PIC_Name"].ToString();

                string msgBody = "";
                msgBody += "Vessel: " + dtsJobDetails.Tables[0].Rows[0]["vessel_name"].ToString();
                msgBody += "<br>Job Code: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString();
                msgBody += "<br>Category: " + dtsJobDetails.Tables[0].Rows[0]["CATEGORY_PRIMARY_NAME"].ToString();
                msgBody += "<br>Description: " + dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();

                msgBody += "<br><br><a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/Operations/TaskPlanner/TaskIndex.aspx?OFFID=" + OFFICE_ID.ToString() + "&WLID=" + WORKLIST_ID.ToString() + "&VID=" + VESSEL_ID.ToString() + "'>View Job Details</a>";

                msgBody += "<br><br>Regards,<br>" + Session["USERFULLNAME"].ToString();

                int MsgID = objCrew.Send_CrewNotification(0, 0, 0, 5, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
                if (MsgID > 0)
                {
                    string js = "window.open('../../Crew/EmailEditor.aspx?ID=" + MsgID + "&Discard=1');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SendMail" + MsgID, js, true);
                }
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);

        }
    }

    protected void grdJoblist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string V = DataBinder.Eval(e.Row.DataItem, "VESSEL_ID").ToString();
            string W = DataBinder.Eval(e.Row.DataItem, "WORKLIST_ID").ToString();
            string O = DataBinder.Eval(e.Row.DataItem, "OFFICE_ID").ToString();
            string AttachmentCount = DataBinder.Eval(e.Row.DataItem, "AttachmentCount").ToString();

            ImageButton imgRemarks = (ImageButton)e.Row.FindControl("imgRemarks");
            if (imgRemarks != null)
            {
                imgRemarks.Attributes.Add("onmouseover", "showFollowups(" + V + "," + W + "," + O + ")");
                imgRemarks.Attributes.Add("onmouseout", "closeDiv('dialog')");
            }

            Image ImgAttachment = (Image)(e.Row.FindControl("ImgAttachment"));
            if (ImgAttachment != null)
            {
                if (AttachmentCount == "0")
                    ImgAttachment.Visible = false;
                else
                    ImgAttachment.Attributes.Add("onclick", "showDialog('../../Technical/Worklist/Attachments.aspx?vid=" + V + "&wlid=" + W + "&wl_off_id=" + O + "');");

            }

            Label lblJD = (Label)e.Row.FindControl("lblJD");
            if (lblJD != null)
            {
                string JD = DataBinder.Eval(e.Row.DataItem, "JOB_DESCRIPTION").ToString();
                JD = JD.Replace("\n", "<br>").Replace("'", "");
                e.Row.Cells[3].Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Description] body=[" + JD + "]");

            }

            string DATE_ESTMTD_CMPLTN = DataBinder.Eval(e.Row.DataItem, "DATE_ESTMTD_CMPLTN").ToString();
            string IsVerified = DataBinder.Eval(e.Row.DataItem, "IsVerified").ToString();
            string DATE_COMPLETED = DataBinder.Eval(e.Row.DataItem, "DATE_COMPLETED").ToString();
            Label lblDATE_ESTMTD_CMPLTN = (Label)e.Row.FindControl("lblDATE_ESTMTD_CMPLTN");

            if (lblDATE_ESTMTD_CMPLTN != null && DATE_ESTMTD_CMPLTN != "" && DATE_COMPLETED == "")
            {
                if (DateTime.Parse(DATE_ESTMTD_CMPLTN) < DateTime.Today)
                {
                    lblDATE_ESTMTD_CMPLTN.BackColor = System.Drawing.Color.Red;
                    lblDATE_ESTMTD_CMPLTN.ForeColor = System.Drawing.Color.White;

                }
            }

            if (DATE_COMPLETED != "" && IsVerified == "0")
            {
                ((ImageButton)e.Row.FindControl("imgStatus")).ImageUrl = "../../Images/Round-org-c.png";
            }
        }
    }

    protected void grdJoblist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["SortDirection"] = GetSortDirection(e.SortExpression);
            ViewState["SortExpression"] = e.SortExpression;

            Load_Tasks();
            //DataTable dt = Session["TaskTable"] as DataTable;
            //if (dt != null)
            //{
            //    //Sort the data.
            //    dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            //    grdJoblist.DataSource = Session["TaskTable"];
            //    grdJoblist.DataBind();
            //}
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    private string GetSortDirection(string column)
    {
        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;

        return sortDirection;
    }

    protected void btnSavePortCall_Click(object sender, EventArgs e)
    {

        string Port_Call_ID = "";

        if (gvPortCalls.SelectedValue != null)
            Port_Call_ID = gvPortCalls.SelectedValue.ToString();

        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);

        BLL_OPS_TaskPlanner.UPDATE_Task_PortCall(Worklist_ID, Vessel_ID, Office_ID, Port_Call_ID, GetSessionUserID());

        hdnVessel_ID.Value = "";
        hdnWorklist_ID.Value = "";
        hdnOffice_ID.Value = "";

        Load_Tasks();
        UpdatePanel_Main.Update();
    }

    protected void btnUpdateTaskStatus_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);
        int Status = 1;

        string CompletionDate = txtCompletionDate.Text;
        string CompletionRemark = txtCompletionRemark.Text;

        if (CompletionRemark != "" && CompletionDate != "")
        {
            BLL_OPS_TaskPlanner.UPDATE_Task_Status(Worklist_ID, Vessel_ID, Office_ID, Status, UDFLib.ConvertDateToNull(CompletionDate), UDFLib.ConvertStringToNull(CompletionRemark), GetSessionUserID());

            txtCompletionDate.Text = "";
            txtCompletionRemark.Text = "";
            UpdatePanel_UpdateStatus.Update();

            hdnVessel_ID.Value = "";
            hdnWorklist_ID.Value = "";
            hdnOffice_ID.Value = "";

            Load_Tasks();
            UpdatePanel_Main.Update();

            string js = "hideModal('dvUpdateStatus');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "StatusJs", js, true);
        }

    }

    protected void btnSaveVerify_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);
        int Status = 2;



        string CompletionDate = txtCompletionDate.Text;
        string CompletionRemark = txtCompletionRemark.Text;

        if (CompletionDate != "")
        {
            BLL_OPS_TaskPlanner.UPDATE_Task_Status(Worklist_ID, Vessel_ID, Office_ID, Status, UDFLib.ConvertDateToNull(CompletionDate), UDFLib.ConvertStringToNull(CompletionRemark), GetSessionUserID());

            txtCompletionDate.Text = "";
            txtCompletionRemark.Text = "";
            UpdatePanel_UpdateStatus.Update();

            hdnVessel_ID.Value = "";
            hdnWorklist_ID.Value = "";
            hdnOffice_ID.Value = "";

            Load_Tasks();
            UpdatePanel_Main.Update();

            string js = "hideModal('dvUpdateStatus');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "StatusJsVerify", js, true);
        }

    }

    protected void btnVerifyFromDetails_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);
        int Status = 2;



        string CompletionDate = "";
        string CompletionRemark = "";


        BLL_OPS_TaskPlanner.UPDATE_Task_Status(Worklist_ID, Vessel_ID, Office_ID, Status, UDFLib.ConvertDateToNull(CompletionDate), UDFLib.ConvertStringToNull(CompletionRemark), GetSessionUserID());

        Load_TaskDetails(Worklist_ID, Vessel_ID, Office_ID);

        hdnVessel_ID.Value = "";
        hdnWorklist_ID.Value = "";
        hdnOffice_ID.Value = "";



    }

    protected void btnSaveFollowUpAndClose_Click(object sender, EventArgs e)
    {
        try
        {
            int Office_ID = int.Parse(hdnOffice_ID.Value);
            int Worklist_ID = int.Parse(hdnWorklist_ID.Value);
            int VESSEL_ID = int.Parse(hdnVessel_ID.Value);

            string FOLLOWUP = txtFollowupMessage.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());

            int newFollowupID = BLL_OPS_TaskPlanner.Insert_Followup(Worklist_ID, VESSEL_ID, Office_ID, FOLLOWUP, CREATED_BY);

            txtFollowupMessage.Text = "";

            Load_TaskDetails(Worklist_ID, VESSEL_ID, Office_ID);
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    protected void btnAddNewFollowUpAndClose_Click(object sender, EventArgs e)
    {
        try
        {
            int Office_ID = int.Parse(hdnOffice_ID.Value);
            int Worklist_ID = int.Parse(hdnWorklist_ID.Value);
            int VESSEL_ID = int.Parse(hdnVessel_ID.Value);

            string FOLLOWUP = txtAddNewFollowup.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());

            int newFollowupID = BLL_OPS_TaskPlanner.Insert_Followup(Worklist_ID, VESSEL_ID, Office_ID, FOLLOWUP, CREATED_BY);

            txtAddNewFollowup.Text = "";

            //Load_TaskDetails(Worklist_ID, VESSEL_ID,Office_ID);
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    protected void btnAddNewTask_Click(object sender, EventArgs e)
    {
        try
        {
            btnEditTask.Visible = false;
            btnVerifyFromDetails.Visible = false;

            DataSet dsTask = BLL_OPS_TaskPlanner.Get_TaskDetails(0, 0, 0, GetSessionUserID());

            frmTaskDetails.ChangeMode(FormViewMode.Insert);
            frmTaskDetails.DataSource = dsTask.Tables[0];
            frmTaskDetails.DataBind();

            int Vessel_ID = UDFLib.ConvertToInteger(ddlVessels.SelectedValue);

            if (Vessel_ID > 0)
            {
                DropDownList ddlVessel = (DropDownList)(frmTaskDetails.FindControl("ddlVesselInsert"));
                if (ddlVessel != null)
                    ddlVessel.SelectedValue = ddlVessels.SelectedValue;

                DropDownList ddlPortCallInsert = (DropDownList)(frmTaskDetails.FindControl("ddlPortCallInsert"));
                if (ddlPortCallInsert != null)
                {
                    DataTable dt = BLL_OPS_TaskPlanner.Get_Vessel_PortCallList(Vessel_ID, DateTime.Today, GetSessionUserID());
                    ddlPortCallInsert.DataSource = dt;
                    ddlPortCallInsert.DataBind();
                    ddlPortCallInsert.Items.Insert(0, new ListItem("- SELECT -", "0"));
                }
            }

            ((TextBox)frmTaskDetails.FindControl("txtCompletion")).Enabled = false;
            UpdatePanel_Details.Update();

            string js = "showModal('dvTaskDetails',false);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js, true);
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    protected void btnEditTask_Click(object sender, EventArgs e)
    {
        if (hdnOffice_ID.Value != "")
        {
            int Office_ID = int.Parse(hdnOffice_ID.Value);
            int Worklist_ID = int.Parse(hdnWorklist_ID.Value);
            int Vessel_ID = int.Parse(hdnVessel_ID.Value);

            frmTaskDetails.ChangeMode(FormViewMode.Edit);
            DataSet dsTask = BLL_OPS_TaskPlanner.Get_TaskDetails(Worklist_ID, Vessel_ID, Office_ID, GetSessionUserID());

            frmTaskDetails.DataSource = dsTask.Tables[0];
            frmTaskDetails.DataBind();
            if (((GridView)frmTaskDetails.FindControl("grdFollowUps")) != null)
            {
                ((GridView)frmTaskDetails.FindControl("grdFollowUps")).DataSource = dsTask.Tables[1];
                ((GridView)frmTaskDetails.FindControl("grdFollowUps")).DataBind();
            }

            gvAttachments.DataSource = dsTask.Tables[2];
            gvAttachments.DataBind();


            DateTime PortCallStartDate = (dsTask.Tables[0].Rows[0]["PORT_CALL_ID"].ToString() == "" || dsTask.Tables[0].Rows[0]["PORT_CALL_ID"].ToString() == "0") ? DateTime.Today : DateTime.Parse("1900/01/01");


            DropDownList ddlPortCallUpdate = (DropDownList)(frmTaskDetails.FindControl("ddlPortCallUpdate"));
            if (ddlPortCallUpdate != null)
            {
                ddlPortCallUpdate.AppendDataBoundItems = false;
                DataTable dt = BLL_OPS_TaskPlanner.Get_Vessel_PortCallList(Vessel_ID, PortCallStartDate, GetSessionUserID());
                ddlPortCallUpdate.DataSource = dt;
                ddlPortCallUpdate.DataBind();
                ddlPortCallUpdate.Items.Insert(0, new ListItem("- SELECT -", "0"));
                try
                {
                    ddlPortCallUpdate.Text = dsTask.Tables[0].Rows[0]["PORT_CALL_ID"].ToString();
                }
                catch { }

            }
            //ctlUploadAttachment1.Worklist_ID = Worklist_ID;
            //ctlUploadAttachment1.Vessel_ID = Vessel_ID;
            //ctlUploadAttachment1.WL_Office_ID = Office_ID;
            //ctlUploadAttachment1.UserID = GetSessionUserID();
            //ctlUploadAttachment1.InitControl();

            //DnDUploader1.Worklist_ID = Worklist_ID;
            //DnDUploader1.Vessel_ID = Vessel_ID;
            //DnDUploader1.WL_Office_ID = Office_ID;
            //DnDUploader1.UserID = GetSessionUserID();
            //DnDUploader1.InitControl();

            ctlRecordNavigationTask.Visible = false;
            UpdatePanel_Details.Update();
        }
    }

    protected void frmTaskDetails_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        if (e.NewMode == FormViewMode.ReadOnly)
        {
            ctlRecordNavigationTask.Visible = true;
        }
    }

    protected void frmTaskDetails_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        string Vessel_ID = "";
        string PortCall_ID = "";

        string CATEGORY_PRIMARY = e.Values["CATEGORY_PRIMARY"].ToString();
        string JOB_DESCRIPTION = e.Values["JOB_DESCRIPTION"].ToString();
        string DATE_ESTMTD_CMPLTN = e.Values["DATE_ESTMTD_CMPLTN"].ToString();
        string DATE_COMPLETED = e.Values["DATE_COMPLETED"].ToString();
        string PIC = "";


        DropDownList ddlPortCallInsert = (DropDownList)(frmTaskDetails.FindControl("ddlPortCallInsert"));
        if (ddlPortCallInsert != null)
            PortCall_ID = ddlPortCallInsert.SelectedValue;

        DropDownList ddlVessel = (DropDownList)(frmTaskDetails.FindControl("ddlVesselInsert"));
        if (ddlVessel != null)
            Vessel_ID = ddlVessel.SelectedValue;

        ListBox ddlPICInsert = (ListBox)(frmTaskDetails.FindControl("ddlPICInsert"));
        if (ddlPICInsert != null)
            PIC = ddlPICInsert.SelectedValue;

        int isPrivate = 0;
        CheckBox chkPrivate = (CheckBox)(frmTaskDetails.FindControl("chkPrivate"));
        if (chkPrivate != null)
            isPrivate = chkPrivate.Checked == true ? 1 : 0;

        DataTable dt = BLL_OPS_TaskPlanner.Create_New_Task(UDFLib.ConvertToInteger(Vessel_ID), JOB_DESCRIPTION, DateTime.Today, UDFLib.ConvertDateToNull(DATE_ESTMTD_CMPLTN), UDFLib.ConvertDateToNull(DATE_COMPLETED), UDFLib.ConvertToInteger(CATEGORY_PRIMARY), UDFLib.ConvertToInteger(PIC), PortCall_ID, GetSessionUserID(), isPrivate);
        if (dt.Rows.Count > 0)
        {
            frmTaskDetails.ChangeMode(FormViewMode.ReadOnly);
            Load_Tasks();
            string js = "alert('New task created');hideModal('dvTaskDetails');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNew", js, true);

        }

    }

    protected void frmTaskDetails_Updating(object sender, FormViewUpdateEventArgs e)
    {
        string PortCall_ID = "";

        string CATEGORY_PRIMARY = e.NewValues["CATEGORY_PRIMARY"].ToString();
        string JOB_DESCRIPTION = e.NewValues["JOB_DESCRIPTION"].ToString();
        string DATE_ESTMTD_CMPLTN = e.NewValues["DATE_ESTMTD_CMPLTN"].ToString();
        string DATE_COMPLETED = e.NewValues["DATE_COMPLETED"].ToString();
        string PIC = "";

        int Office_ID = int.Parse(hdnOffice_ID.Value);
        int Worklist_ID = int.Parse(hdnWorklist_ID.Value);
        int VESSEL_ID = int.Parse(hdnVessel_ID.Value);


        DropDownList ddlPortCallUpdate = (DropDownList)(frmTaskDetails.FindControl("ddlPortCallUpdate"));
        if (ddlPortCallUpdate != null)
            PortCall_ID = ddlPortCallUpdate.SelectedValue;

        int isPrivate = 0;
        CheckBox chkPrivate = (CheckBox)(frmTaskDetails.FindControl("chkPrivate"));
        if (chkPrivate != null)
            isPrivate = chkPrivate.Checked == true ? 1 : 0;

        DropDownList ddlPICUpdate = (DropDownList)(frmTaskDetails.FindControl("ddlPICUpdate"));
        if (ddlPICUpdate != null)
            PIC = ddlPICUpdate.SelectedValue;


        int RetVal = BLL_OPS_TaskPlanner.UPDATE_Task(Worklist_ID, VESSEL_ID, Office_ID, JOB_DESCRIPTION, null, UDFLib.ConvertDateToNull(DATE_ESTMTD_CMPLTN), UDFLib.ConvertDateToNull(DATE_COMPLETED), UDFLib.ConvertToInteger(CATEGORY_PRIMARY), UDFLib.ConvertToInteger(PIC), PortCall_ID, GetSessionUserID(), isPrivate);

        if (RetVal > 0)
        {
            Load_Tasks();
            UpdatePanel_Main.Update();

            //string js = "alert('Task Updated');hideModal('dvTaskDetails');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNew", js, true);
            frmTaskDetails.ChangeMode(FormViewMode.ReadOnly);
            Load_TaskDetails(Worklist_ID, VESSEL_ID, Office_ID);
            UpdatePanel_Details.Update();
        }

    }

    protected void frmTaskDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            int Office_ID = int.Parse(hdnOffice_ID.Value);
            int Worklist_ID = int.Parse(hdnWorklist_ID.Value);
            int VESSEL_ID = int.Parse(hdnVessel_ID.Value);

            frmTaskDetails.ChangeMode(FormViewMode.ReadOnly);
            Load_TaskDetails(Worklist_ID, VESSEL_ID, Office_ID);
            UpdatePanel_Details.Update();
        }



    }

    protected void frmTaskDetails_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (frmTaskDetails.Row.RowState != DataControlRowState.Edit && frmTaskDetails.Row.RowState != DataControlRowState.Insert)
            {
                DataRowView dataRow = ((DataRowView)frmTaskDetails.DataItem);
                if (dataRow != null)
                {
                    string CREATED_DATE = dataRow["CREATED_DATE"].ToString() == "" ? "" : "<br>" + dataRow["CREATED_DATE"].ToString();
                    string MODIFIED_DATE = dataRow["MODIFIED_DATE"].ToString() == "" ? "" : "<br>" + dataRow["MODIFIED_DATE"].ToString();

                    if (dataRow["Created_By_CrewID"].ToString() != "")
                    {
                        ((HyperLink)frmTaskDetails.FindControl("lnkCreatedBy")).Text = "Created By: " + dataRow["Created_By_Name"].ToString() + CREATED_DATE;
                        ((HyperLink)frmTaskDetails.FindControl("lnkCreatedBy")).NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dataRow["Created_By_CrewID"].ToString();
                        ((HyperLink)frmTaskDetails.FindControl("lnkCreatedBy")).Target = "_blank";
                        ((HyperLink)frmTaskDetails.FindControl("lnkCreatedBy")).Visible = true;

                        // ((Image)frmTaskDetails.FindControl("imgCreatedBy")).ImageUrl = "" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/uploads/CrewImages/" + dataRow["Photo_CREATED_BY"].ToString();

                        ((Image)frmTaskDetails.FindControl("imgCreatedBy")).ImageUrl = "http://" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dataRow["Photo_CREATED_BY"].ToString();



                        ((Image)frmTaskDetails.FindControl("imgCreatedBy")).Visible = true;
                    }
                    else
                    {
                        ((HyperLink)frmTaskDetails.FindControl("lnkCreatedBy")).Visible = false;
                        ((Image)frmTaskDetails.FindControl("imgCreatedBy")).Visible = false;
                    }
                    if (dataRow["Modified_By_CrewID"].ToString() != "")
                    {
                        ((HyperLink)frmTaskDetails.FindControl("lnkModifiedBy")).Text = "Last Modified By: " + dataRow["Modified_By_Name"].ToString() + MODIFIED_DATE;
                        ((HyperLink)frmTaskDetails.FindControl("lnkModifiedBy")).NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dataRow["Modified_By_CrewID"].ToString();
                        ((HyperLink)frmTaskDetails.FindControl("lnkModifiedBy")).Target = "_blank";
                        ((HyperLink)frmTaskDetails.FindControl("lnkModifiedBy")).Visible = true;

                        //((Image)frmTaskDetails.FindControl("imgModifiedBy")).ImageUrl = "" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/uploads/CrewImages/" + dataRow["Photo_MODIFIED_BY"].ToString();
                        ((Image)frmTaskDetails.FindControl("imgModifiedBy")).ImageUrl = "http://" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dataRow["Photo_MODIFIED_BY"].ToString();
                        ((Image)frmTaskDetails.FindControl("imgModifiedBy")).Visible = true;

                    }
                    else
                    {
                        ((Image)frmTaskDetails.FindControl("imgModifiedBy")).Visible = false;
                        ((Image)frmTaskDetails.FindControl("imgModifiedBy")).Visible = false;
                    }
                }
            }
        }
        catch { }

    }


    protected void ddlVesselInsert_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(((DropDownList)sender).SelectedValue);

        DropDownList ddlPortCallInsert = (DropDownList)(frmTaskDetails.FindControl("ddlPortCallInsert"));
        if (ddlPortCallInsert != null)
        {
            DataTable dt = BLL_OPS_TaskPlanner.Get_Vessel_PortCallList(Vessel_ID, DateTime.Today, GetSessionUserID());
            ddlPortCallInsert.DataSource = dt;
            ddlPortCallInsert.DataBind();
            ddlPortCallInsert.Items.Insert(0, new ListItem("- SELECT -", "0"));
        }
    }

    protected void ddlVesselUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlPortCallUpdate = (DropDownList)(frmTaskDetails.FindControl("ddlPortCallUpdate"));
        if (ddlPortCallUpdate != null)
        {
            int Vessel_ID = UDFLib.ConvertToInteger(((DropDownList)sender).SelectedValue);
            ddlPortCallUpdate.AppendDataBoundItems = false;

            DataTable dt = BLL_OPS_TaskPlanner.Get_Vessel_PortCallList(Vessel_ID, DateTime.Parse("1900/01/01"), GetSessionUserID());
            ddlPortCallUpdate.DataSource = dt;
            ddlPortCallUpdate.DataBind();
            ddlPortCallUpdate.Items.Insert(0, new ListItem("- SELECT -", "0"));
        }
    }

    //public void ctlUploadAttachment1_UploadCompleted()
    //{
    //    int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
    //    int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
    //    int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);
    //    Load_TaskDetails(Worklist_ID, Vessel_ID, Office_ID);
    //    ctlUploadAttachment1.InitControl();
    //}

    protected void DnDUploader1_UploadCompleted(string Message, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);
        Load_TaskDetails(Worklist_ID, Vessel_ID, Office_ID);

        // DnDUploader1.InitControl();
    }
    protected void DnDUploader1_UploadFailed(string Message, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);
        int Worklist_ID = UDFLib.ConvertToInteger(hdnWorklist_ID.Value);
        int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value);
        Load_TaskDetails(Worklist_ID, Vessel_ID, Office_ID);

        // DnDUploader1.InitControl();
    }
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {

        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Technical");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int Vessel_ID = UDFLib.ConvertToInteger(Session["hdnVesselID"].ToString());
            int Worklist_ID = UDFLib.ConvertToInteger(Session["hdnWorklistlID"].ToString());
            int Office_ID = UDFLib.ConvertToInteger(Session["hdnOfficeID"].ToString());

            int FileID = objBLL.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, Office_ID, UDFLib.Remove_Special_Characters(file.FileName), Flag_Attach, file.FileSize, UDFLib.ConvertToInteger(Session["USERID"]));

            string FullFilename = Path.Combine(sPath, Vessel_ID + "_" + Worklist_ID + "_" + Office_ID + "_" + "O" + "_" + FileID.ToString() + "_" + GUID.ToString() + Path.GetExtension(file.FileName));

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {

        }

    }
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        if (Session["hdnVesselID"] != null)
        {
            int Vessel_ID = UDFLib.ConvertToInteger(Session["hdnVesselID"].ToString());
            int Worklist_ID = UDFLib.ConvertToInteger(Session["hdnWorklistlID"].ToString());
            int Office_ID = UDFLib.ConvertToInteger(Session["hdnOfficeID"].ToString());
            Load_TaskDetails(Worklist_ID, Vessel_ID, Office_ID);
            //DataTable dtpkid = (DataTable)Session["WORKLIST_PKID_NAV"];

            //fillvalue(dtpkid.Rows.Find(new object[] { Worklist_ID, Vessel_ID, Office_ID }));
        }
        UpdatePanel_Main.Update();
    }
}