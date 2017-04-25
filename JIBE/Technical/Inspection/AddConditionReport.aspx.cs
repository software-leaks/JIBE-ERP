using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.Technical;
using System.Data;
using AjaxControlToolkit4;
using System.IO;
using SMS.Business.Inspection;
public partial class Technical_Worklist_AddConditionReport : System.Web.UI.Page
{

    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    DataTable dtattachment = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {
            if (Session["Attachment"] == null)
            {
                dtattachment.Columns.Add("FileName");
                dtattachment.Columns.Add("Flag_Attach");
                dtattachment.Columns.Add("FileSize");
                Session["Attachment"] = dtattachment;
            }
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["InspID"] != null && Request.QueryString["LocationID"] != null)
                {

                }

                if (Session["Attachment"] == null)
                {
                    dtattachment.Columns.Add("FileName");
                    dtattachment.Columns.Add("Flag_Attach");
                    dtattachment.Columns.Add("FileSize");
                    Session["Attachment"] = dtattachment;


                }

                Session["Mode"] = "ADD";
                hdnOPMode.Value = "ADD";
                //BtnAttach.Enabled = false;
                BindDeptInOffice();
                BindDeptOnShip();
                BindWLType();
                Search_Worklist();
                sortWLJobs("WORKLIST_ID");
                ClearPage();

            }
        }
    }




    protected void BindDeptOnShip()
    {
        try
        {

            ddlDeptShip.DataSource = objInsp.Get_Dept_OnShip();
            ddlDeptShip.DataTextField = "value";
            ddlDeptShip.DataValueField = "id";
            ddlDeptShip.DataBind();
            ddlDeptShip.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindWLType()
    {
        try
        {

            ddlType.DataSource = objInsp.GetAllWorklistType();
            ddlType.DataTextField = "Worklist_Type_Display";
            ddlType.DataValueField = "Worklist_Type";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindDeptInOffice()
    {
        try
        {

            ddlDeptOff.DataSource = objInsp.Get_Dept_InOffice();
            ddlDeptOff.DataTextField = "value";
            ddlDeptOff.DataValueField = "id";
            ddlDeptOff.DataBind();
            ddlDeptOff.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }


    /// <summary>
    /// Modified By Anjali DT:14-10-2016
    /// To save attached files to DB.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="file"></param>
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            dtattachment = (DataTable)Session["Attachment"];


            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Technical");
            string sPath1 = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Inspection");
            Guid GUID = Guid.NewGuid();
            string _fileName = "";

            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);


            int Vessel_ID = Convert.ToInt32(Request.QueryString["VesselID"].ToString());/* Here it should be received after inserting data TEC_WORKLIST_MAIN*/
            int Worklist_ID = Convert.ToInt32(Session["WID"]); /* Here it should be received after inserting data TEC_WORKLIST_MAIN*/
            int Office_ID = Convert.ToInt32(Session["OID"]); /* Here it should be received after inserting data TEC_WORKLIST_MAIN*/

            int FileID = objInsp.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, Office_ID, UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), Flag_Attach,
                                                            file.FileSize, UDFLib.ConvertToInteger(Session["USERID"]));

            _fileName = "TEC_" + Vessel_ID + "_" + Worklist_ID + "_" + Office_ID + "_" + "O" + "_" + FileID.ToString() + "_" + Flag_Attach;

            if (FileID > 0)
            {

                int Ret = objInsp.Insert_ActivityObject(Vessel_ID, Worklist_ID, Office_ID, UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)),/* filattachPath, */_fileName,
                                                        UDFLib.ConvertToInteger(Session["USERID"]));

                string FullFilename = Path.Combine(sPath, _fileName);

                //Save attached file to folder
                FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(fileBytes, 0, fileBytes.Length);
                fileStream.Close();


                File.Copy(FullFilename, Path.Combine(sPath1, _fileName));

                Load_Attachments(Vessel_ID, Worklist_ID, Office_ID, UDFLib.ConvertToInteger(Session["USERID"]));

                string jsPopClose = " hideModal('dvPopupAddAttachment');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPopClose", jsPopClose, true);
            }
            else
            {
                string jsPopAlert = " alert('File Already exists.');";
                ScriptManager.RegisterStartupScript(AjaxFileUpload1, AjaxFileUpload1.GetType(), "jsPopAlert", jsPopAlert, true);
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void Search_Worklist()
    {
        try
        {

            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(Request.QueryString["VesselID"].ToString()) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", ucCustomPagerctp.CurrentPageIndex });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", ucCustomPagerctp.PageSize });
            dtFilter.Rows.Add(new object[] { "@LOCATION_ID", UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()) });
            dtFilter.Rows.Add(new object[] { "@NODE_ID", UDFLib.ConvertIntegerToNull(Request.QueryString["LocationNodeID"].ToString() == "-1" ? null : Request.QueryString["LocationNodeID"].ToString()) });
            dtFilter.Rows.Add(new object[] { "@InspectionID", UDFLib.ConvertIntegerToNull(Request.QueryString["InspID"]) });

            int Record_Count = 0;


            DataSet taskTable = objInsp.INSP_Get_Worklist(dtFilter, ref Record_Count);
            if (taskTable.Tables.Count > 0)
            {
                Session["TaskTable"] = taskTable.Tables[0];
                grdWLJobs.DataSource = taskTable.Tables[0];
                grdWLJobs.DataBind();

                ucCustomPagerctp.CountTotalRec = Record_Count.ToString();
                ucCustomPagerctp.BuildPager();
            }
        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int VESSEL_ID = Convert.ToInt32(Request.QueryString["VesselID"].ToString());
            DataSet dsInsp = objInsp.GetVesselInspectionDetails(Convert.ToInt32(Request.QueryString["InspID"].ToString()));
            string WL_TYPE;
            if (ddlType.SelectedItem.Text == "-Select-")
            {
                WL_TYPE = "";
            }
            else
            {
                WL_TYPE = ddlType.SelectedValue;
            }
            string JOB_DESCRIPTION = txtDesc.Text;

            string DATE_RAISED = DateTime.Today.ToString("dd/MM/yyyy");


            string DATE_ESTMTD_CMPLTN = txtExpectedComp.Text;
            if (txtExpectedComp.Text.Trim() != "")
            {
                DATE_ESTMTD_CMPLTN = txtExpectedComp.Text;
            }
            else
            {
                DATE_ESTMTD_CMPLTN = "1900/01/01";
            }

            string DATE_COMPLETED = "1900/01/01";


            int PRIORITY = 1;//int.Parse(ddlPriority.SelectedValue);
            int ASSIGNOR = 0;// int.Parse(ddlAssignedBy.SelectedValue);
            int NCR_YN = 0;//int.Parse(rdoNCR.SelectedValue);
            int DEPT_SHIP = int.Parse(ddlDeptShip.SelectedValue);
            int DEPT_OFFICE = int.Parse(ddlDeptOff.SelectedValue);
            string REQSN_MSG_REF = "";// uc_ReqRef.SelectedValue;

            int DEFER_TO_DD = 0;// int.Parse(rdoDeferToDrydock.SelectedValue);

            int CATEGORY_NATURE = 0;//int.Parse(ddlNature.SelectedValue);
            int CATEGORY_PRIMARY = 0;// int.Parse(ddlPrimary.SelectedValue);
            int CATEGORY_SECONDARY = 0;// int.Parse(ddlSecondary.SelectedValue);
            int CATEGORY_MINOR = 0;// int.Parse(ddlMinorCat.SelectedValue);

            int PIC = 0;
            //if (ddlPIC.SelectedIndex > 0)
            //    PIC = int.Parse(ddlPIC.SelectedValue.ToString());

            int CREATED_BY = int.Parse(Session["USERID"].ToString());
            int Inspector = 0;//UDFLib.ConvertToInteger(ddlInspector.SelectedValue);
            string InspectionDate = dsInsp.Tables[0].Rows[0]["InspectionDate"].ToString();


            //if (InspectionDate.Trim() != "")
            //{
            //    DATE_COMPLETED = InspectionDate;
            //    txtCompletedOn.Text = InspectionDate;
            //}
            //else
            //{
            //    DATE_COMPLETED = "1900/01/01";
            //    InspectionDate = "1900/01/01";
            //}


            int TOSYNC = 0;
            string Causes = "";
            string CorrectiveAction = "";
            string PreventiveAction = "";
            if (ddlType.SelectedValue.ToString() == "NCR") //This if block is added by Hadish on 24-Nov-16 JIT:11076 ,Guided by Pranav 
            {
                NCR_YN = -1;
            }
            else
            {
                NCR_YN = 0;
            }
            if (NCR_YN == -1)
            {
                Causes = "";
                CorrectiveAction = "";
                PreventiveAction = "";
            }

            if (objInsp.IsVessel(VESSEL_ID) == 1)
                TOSYNC = 1;
            DataTable dtNewRecord = new DataTable();

            if (Session["Mode"].ToString() == "ADD")
            {
                //dtNewRecord = objInsp.Insert_NewJob(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE);
                string typeStr = "0";
                //if (WL_TYPE == "DEFECT")
                //{
                //    typeStr = "1";
                //}
                //else
                //{
                //    typeStr = "2";
                //}

                //string Activity_ID= objInsp.TEC_WL_INS_Acivity(VESSEL_ID, UDFLib.ConvertDateToNull(DATE_RAISED), UDFLib.ConvertDateToNull(DATE_ESTMTD_CMPLTN), 0, UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), DEPT_SHIP, DEPT_OFFICE, JOB_DESCRIPTION, typeStr, CREATED_BY);

                dtNewRecord = objInsp.Insert_NewJob(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE, "", typeStr, UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), null, 1);



                if (dtNewRecord.Rows.Count > 0)
                {

                    //if (rdoNCR.SelectedValue == "-1")
                    //{
                    //    txtNCRNo.Text = dtNewRecord.Rows[0]["NCR_NUM"].ToString();
                    //    txtNCRNo_Year.Text = dtNewRecord.Rows[0]["NCR_YEAR"].ToString();
                    //}

                    //txtOfficeID.Value = dtNewRecord.Rows[0]["OFFICE_ID"].ToString();
                    //txtJobCode.Text = "0";

                    if (Request.QueryString["InspID"] != null && Request.QueryString["LocationID"] != null)
                    {
                        int inspID = Convert.ToInt32(Request.QueryString["InspID"]);
                        int WorlistID = Convert.ToInt32(dtNewRecord.Rows[0]["WORKLIST_ID"].ToString());
                        int OfficeID = Convert.ToInt32(dtNewRecord.Rows[0]["OFFICE_ID"].ToString());
                        //int LocationID = UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString());
                        int LocationNodeID = Convert.ToInt32(Request.QueryString["LocationNodeID"].ToString());
                        //objInsp.TEC_WL_INS_InspectionWorklist_Location(Convert.ToInt32(Request.QueryString["InspID"]), WorlistID, VESSEL_ID, OfficeID, LocationID, CREATED_BY);
                        objInsp.TEC_WL_INS_InspectionWorklist_Location(Convert.ToInt32(Request.QueryString["InspID"]), WorlistID, VESSEL_ID, OfficeID, UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), LocationNodeID, CREATED_BY);

                        // 
                        if (ddlType.SelectedItem.Text == "Defect")
                        {
                            //objInsp.INSP_Update_SubSystemRating(Convert.ToInt32(Request.QueryString["InspID"]), LocationID, "", "", CREATED_BY, DateTime.Now);
                            objInsp.INSP_Update_SubSystemRating(Convert.ToInt32(Request.QueryString["InspID"]), UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), "", "", CREATED_BY, DateTime.Now);
                        }

                        Session["Attachment"] = null;
                    }

                }

                Search_Worklist();

                string js1 = "alert('Job added successfully.'); hideModal('dvJob');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);
                string js2 = "parent.UpdatePage();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);

                ClearPage();
                // BtnSave.Enabled = false;
                //BtnAttach.Enabled = false;
                //BtnClear.Enabled = true;
                Session["Mode"] = "ADD";
            }

            else if (Session["Mode"].ToString() == "EDIT")
            {

                int res = objInsp.Update_Job(VESSEL_ID, UDFLib.ConvertToInteger(Session["WID"].ToString()), UDFLib.ConvertToInteger(Session["OID"].ToString()), DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, UDFLib.ConvertStringToNull(PIC), CREATED_BY, TOSYNC, Inspector, InspectionDate, WL_TYPE, null);

                //DataTable dtAttach = (DataTable)Session["Attachment"];
                //if (dtAttach.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtAttach.Rows.Count; i++)
                //    {
                //        int FileID = objInsp.Insert_Worklist_Attachment(VESSEL_ID, UDFLib.ConvertToInteger(Session["WID"].ToString()), UDFLib.ConvertToInteger(Session["OID"].ToString()), dtAttach.Rows[i]["FileName"].ToString(), dtAttach.Rows[i]["Flag_Attach"].ToString(), Convert.ToInt32(dtAttach.Rows[i]["FileSize"].ToString()), UDFLib.ConvertToInteger(Session["USERID"]));
                //    }
                //}

                string js1 = "alert('Job updated successfully.'); ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);

                //string js2 = "parent.UpdatePage();";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
                Search_Worklist();
                ClearPage();
                //BtnSave.Enabled = false;
                //BtnAttach.Enabled = false;
                //BtnClear.Enabled = true;
                Session["Mode"] = "ADD";


            }


        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BtnSave1_Click(object sender, EventArgs e)
    {

        int VESSEL_ID = Convert.ToInt32(Request.QueryString["VesselID"].ToString());
        DataSet dsInsp = objInsp.GetVesselInspectionDetails(Convert.ToInt32(Request.QueryString["InspID"].ToString()));
        string WL_TYPE;
        if (ddlType.SelectedItem.Text == "-Select-")
        {
            WL_TYPE = "";
        }
        else
        {
            WL_TYPE = ddlType.SelectedValue;
        }
        string JOB_DESCRIPTION = txtDesc.Text;

        string DATE_RAISED = DateTime.Today.ToString("dd/MM/yyyy");


        string DATE_ESTMTD_CMPLTN = txtExpectedComp.Text;
        if (txtExpectedComp.Text.Trim() != "")
        {
            DATE_ESTMTD_CMPLTN = txtExpectedComp.Text;
        }
        else
        {
            DATE_ESTMTD_CMPLTN = "1900/01/01";
        }

        string DATE_COMPLETED = "1900/01/01";


        int PRIORITY = 1;//int.Parse(ddlPriority.SelectedValue);
        int ASSIGNOR = 0;// int.Parse(ddlAssignedBy.SelectedValue);
        int NCR_YN = 0;//int.Parse(rdoNCR.SelectedValue);
        int DEPT_SHIP = int.Parse(ddlDeptShip.SelectedValue);
        int DEPT_OFFICE = int.Parse(ddlDeptOff.SelectedValue);
        string REQSN_MSG_REF = "";// uc_ReqRef.SelectedValue;

        int DEFER_TO_DD = 0;// int.Parse(rdoDeferToDrydock.SelectedValue);

        int CATEGORY_NATURE = 0;//int.Parse(ddlNature.SelectedValue);
        int CATEGORY_PRIMARY = 0;// int.Parse(ddlPrimary.SelectedValue);
        int CATEGORY_SECONDARY = 0;// int.Parse(ddlSecondary.SelectedValue);
        int CATEGORY_MINOR = 0;// int.Parse(ddlMinorCat.SelectedValue);

        int PIC = 0;
        //if (ddlPIC.SelectedIndex > 0)
        //    PIC = int.Parse(ddlPIC.SelectedValue.ToString());

        int CREATED_BY = int.Parse(Session["USERID"].ToString());
        int Inspector = 0;//UDFLib.ConvertToInteger(ddlInspector.SelectedValue);
        string InspectionDate = dsInsp.Tables[0].Rows[0]["InspectionDate"].ToString();


        //if (InspectionDate.Trim() != "")
        //{
        //    DATE_COMPLETED = InspectionDate;
        //    txtCompletedOn.Text = InspectionDate;
        //}
        //else
        //{
        //    DATE_COMPLETED = "1900/01/01";
        //    InspectionDate = "1900/01/01";
        //}


        int TOSYNC = 0;
        string Causes = "";
        string CorrectiveAction = "";
        string PreventiveAction = "";

        if (NCR_YN == -1)
        {
            Causes = "";
            CorrectiveAction = "";
            PreventiveAction = "";
        }

        if (objInsp.IsVessel(VESSEL_ID) == 1)
            TOSYNC = 1;
        DataTable dtNewRecord = new DataTable();

        if (Session["Mode"].ToString() == "ADD")
        {

            string typeStr = "";
            if (WL_TYPE == "DEFECT")
            {
                typeStr = "1";
            }
            else
            {
                typeStr = "2";
            }

            dtNewRecord = objInsp.Insert_NewJob(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE, "", typeStr, UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), null, 1);

            //dtNewRecord = objInsp.Insert_NewJob(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE);

            if (dtNewRecord.Rows.Count > 0)
            {
                Session["WID"] = dtNewRecord.Rows[0]["WORKLIST_ID"];
                Session["VID"] = dtNewRecord.Rows[0]["VESSEL_ID"];
                Session["OID"] = dtNewRecord.Rows[0]["OFFICE_ID"];

                //if (rdoNCR.SelectedValue == "-1")
                //{
                //    txtNCRNo.Text = dtNewRecord.Rows[0]["NCR_NUM"].ToString();
                //    txtNCRNo_Year.Text = dtNewRecord.Rows[0]["NCR_YEAR"].ToString();
                //}

                //txtOfficeID.Value = dtNewRecord.Rows[0]["OFFICE_ID"].ToString();
                //txtJobCode.Text = "0";

                if (Request.QueryString["InspID"] != null && Request.QueryString["LocationID"] != null)
                {
                    int inspID = Convert.ToInt32(Request.QueryString["InspID"]);
                    int WorlistID = Convert.ToInt32(dtNewRecord.Rows[0]["WORKLIST_ID"].ToString());
                    int OfficeID = Convert.ToInt32(dtNewRecord.Rows[0]["OFFICE_ID"].ToString());
                    //int LocationID = UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString());
                    int LocationNodeID = Convert.ToInt32(Request.QueryString["LocationNodeID"].ToString());
                    //objInsp.TEC_WL_INS_InspectionWorklist_Location(Convert.ToInt32(Request.QueryString["InspID"]), WorlistID, VESSEL_ID, OfficeID, LocationID, CREATED_BY);
                    objInsp.TEC_WL_INS_InspectionWorklist_Location(Convert.ToInt32(Request.QueryString["InspID"]), WorlistID, VESSEL_ID, OfficeID, UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), LocationNodeID, CREATED_BY);

                    // 
                    if (ddlType.SelectedItem.Text == "Defect")
                    {
                        //objInsp.INSP_Update_SubSystemRating(Convert.ToInt32(Request.QueryString["InspID"]), LocationID, "", "", CREATED_BY, DateTime.Now);
                        objInsp.INSP_Update_SubSystemRating(Convert.ToInt32(Request.QueryString["InspID"]), UDFLib.ConvertIntegerToNull(Request.QueryString["LocationID"].ToString()), "", "", CREATED_BY, DateTime.Now);
                    }

                    Session["Attachment"] = null;
                }

            }

            Search_Worklist();
            string js1 = " showModal('dvPopupAddAttachment',true,Rebind)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);
            //string js1 = "alert('Job added successfully'); hideModal('dvJob');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);
            //string js2 = "parent.UpdatePage();";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
            txtDesc.Enabled = false;
            //ClearPage();
            // BtnSave.Enabled = false;
            //BtnAttach.Enabled = false;
            //BtnClear.Enabled = true;
            Session["Mode"] = "EDIT";
            //Session["Mode"] = "ADD";
        }

        else if (Session["Mode"].ToString() == "EDIT")
        {

            string js1 = " showModal('dvPopupAddAttachment',true,Rebind)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);

            //int res = objInsp.Update_Job(VESSEL_ID, UDFLib.ConvertToInteger(Session["WID"].ToString()), UDFLib.ConvertToInteger(Session["OID"].ToString()), DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, UDFLib.ConvertStringToNull(PIC), CREATED_BY, TOSYNC, Inspector, InspectionDate, WL_TYPE);

            ////DataTable dtAttach = (DataTable)Session["Attachment"];
            ////if (dtAttach.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < dtAttach.Rows.Count; i++)
            ////    {
            ////        int FileID = objInsp.Insert_Worklist_Attachment(VESSEL_ID, UDFLib.ConvertToInteger(Session["WID"].ToString()), UDFLib.ConvertToInteger(Session["OID"].ToString()), dtAttach.Rows[i]["FileName"].ToString(), dtAttach.Rows[i]["Flag_Attach"].ToString(), Convert.ToInt32(dtAttach.Rows[i]["FileSize"].ToString()), UDFLib.ConvertToInteger(Session["USERID"]));
            ////    }
            ////}

            //string js1 = "alert('Job Updated successfully'); ";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);

            ////string js2 = "parent.UpdatePage();";
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
            //Search_Worklist();
            //ClearPage();
            //BtnSave.Enabled = false;
            ////BtnAttach.Enabled = false;
            ////BtnClear.Enabled = true;
            //Session["Mode"] = "ADD";


        }


    }

    public void ClearPage()
    {
        ddlType.SelectedIndex = 0;
        ddlDeptOff.SelectedIndex = 0;
        ddlDeptShip.SelectedIndex = 0;
        txtDesc.Text = "";
        txtExpectedComp.Text = "";
        gvWLJobAttachment.DataSource = null;
        gvWLJobAttachment.DataBind();
        txtDesc.Enabled = true;
        BtnVerify.Enabled = false;


        ddlType.Enabled = true;
        txtDesc.Enabled = true;
        ddlDeptShip.Enabled = true;
        ddlDeptOff.Enabled = true;
        txtExpectedComp.Enabled = true;
        BtnSave.Enabled = true;



    }
    protected void ImgEditJob_Click(object sender, CommandEventArgs e)
    {

        int WorklistID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[0]);
        int VesselID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[1]);
        int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[2]);

        Session["WID"] = WorklistID;
        Session["VID"] = VesselID;
        Session["OID"] = OfficeID;

        DataSet dtsJobDetails = objInsp.Get_JobDetails_ByID(OfficeID, WorklistID, VesselID);

        if (dtsJobDetails.Tables[0].Rows.Count > 0)
        {
            ListItem item = new ListItem(dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString());

            ddlType.SelectedValue = item.Text;
            txtDesc.Text = dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();
            ddlDeptShip.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["DEPT_SHIP"].ToString();
            ddlDeptOff.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["DEPT_OFFICE"].ToString();

            if (dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString() != "")
            {
                txtExpectedComp.Text = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString()).ToShortDateString();
            }

        }

        Load_Attachments(VesselID, WorklistID, OfficeID, int.Parse(Session["USERID"].ToString()));

        Session["Mode"] = "EDIT";
        //BtnAttach.Enabled = true;
        // BtnSave.Enabled = true;
        //  BtnClear.Enabled = false;
        txtDesc.Enabled = false;
        hdnOPMode.Value = "EDIT";
        BtnVerify.Enabled = true;
        BtnSave.Enabled = true;
        ddlType.Enabled = true;
        ddlDeptOff.Enabled = true;
        ddlDeptShip.Enabled = true;
        txtExpectedComp.Enabled = true;


    }

    protected void ImgDeleteJob_Click(object sender, CommandEventArgs e)
    {

        int WorklistID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[0]);
        int VesselID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[1]);
        int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[2]);

        objInsp.INSP_Delete_WorklistJob(VesselID, WorklistID, OfficeID);
        objInsp.Upd_Worklist_Status(Convert.ToInt32(e.CommandArgument.ToString().Split(';')[1]), Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]), Convert.ToInt32(e.CommandArgument.ToString().Split(';')[2]), Convert.ToInt32(Session["USERID"]), "DELETED", "REMOVED");

        objInsp.INSP_Delete_Activity(VesselID, WorklistID, OfficeID);
        Search_Worklist();
        sortWLJobs("WORKLIST_ID");

    }
    private void Load_Attachments(int VESSEL_ID, int WORKLIST_ID, int WL_OFFICE_ID, int UserID)
    {


        DataTable dt = objInsp.Get_Worklist_Attachments(VESSEL_ID, WORKLIST_ID, WL_OFFICE_ID, UserID);
        DataView dvImage = dt.DefaultView;
        // dvImage.RowFilter = "Is_Image='1' ";



        gvWLJobAttachment.DataSource = dt.DefaultView;
        gvWLJobAttachment.DataBind();


        //Bind Popup


    }
    protected void btnRefreshPMSJobAttachment_Click(object sender, EventArgs e)
    {

    }
    protected void grdWLJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdWLJobs_Sorting(object sender, GridViewSortEventArgs e)
    {
        sortWLJobs(e.SortExpression);
    }
    public void sortWLJobs(string SortExpression)
    {
        try
        {
            //hdnFlagCheck.Value = "false";
            //Retrieve the table from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = SortExpression + " " + GetSortDirection(SortExpression);
                grdWLJobs.DataSource = dt;
                grdWLJobs.DataBind();
            }
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
    protected void imgbtnDeleteAssembly_Click(object sender, CommandEventArgs e)
    {
        int AttachID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[1]);
        int WorklistID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[2]);
        int VesselID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[3]);
        int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[4]);
        string AttachPATH = UDFLib.ConvertStringToNull(e.CommandArgument.ToString().Trim().Split(';')[0]);
        int state = objInsp.INSP_Delete_WorklistAttachment(VesselID, WorklistID, OfficeID, AttachID, int.Parse(Session["USERID"].ToString()));
        int ret = objInsp.INSP_Delete_ActivityObject(VesselID, WorklistID, OfficeID, AttachID, int.Parse(Session["USERID"].ToString()));
        Load_Attachments(VesselID, WorklistID, OfficeID, UDFLib.ConvertToInteger(Session["USERID"]));

    }
    protected void lnkRating_Click(object sender, CommandEventArgs e)
    {
        int WorklistID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[0]);
        int VesselID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[1]);
        int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Trim().Split(';')[2]);

        Session["WID"] = WorklistID;
        Session["VID"] = VesselID;
        Session["OID"] = OfficeID;
        //BindWLType();
        DataSet dtsJobDetails = objInsp.Get_JobDetails_ByID(OfficeID, WorklistID, VesselID);

        if (dtsJobDetails.Tables[0].Rows.Count > 0)
        {
            ListItem item = new ListItem(dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString());

            //ddlType.SelectedIndex = ddlType.Items.IndexOf(item);
            ddlType.SelectedValue = item.Text.ToString();
            txtDesc.Text = dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();
            ddlDeptShip.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["DEPT_SHIP"].ToString();
            ddlDeptOff.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["DEPT_OFFICE"].ToString();
            if (dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString() != "")
            {
                txtExpectedComp.Text = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString()).ToShortDateString();
            }
        }

        Load_Attachments(VesselID, WorklistID, OfficeID, int.Parse(Session["USERID"].ToString()));

        ddlDeptOff.Enabled = false;
        txtDesc.Enabled = false;
        ddlDeptShip.Enabled = false;
        ddlType.Enabled = false;
        txtExpectedComp.Enabled = false;
        BtnSave.Enabled = false;
        //// BtnVerify.Enabled = true;

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearPage();
        //  BtnClear.Enabled = false;
        //  BtnSave.Enabled = true;
        Session["Mode"] = "ADD";
        txtDesc.Enabled = true;
        hdnOPMode.Value = "ADD";
        //BtnAttach.Enabled = false;
        BtnVerify.Enabled = false;
    }
    protected void BtnVerify_Click(object sender, EventArgs e)
    {

        ViewState["WORKLIST_STATUS"] = "CLOSED";
        lblWorklistTitle.Text = "Verify and close job";
        txtWorklistStatusRemark.Text = "";

        UpdatePanel2.Update();

        string js = "showModal('dvReworkClose');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);
        //ClearPage();
    }
    protected void btnSaveStatus_Click(object s, EventArgs e)
    {


        objInsp.Upd_Worklist_Status(Convert.ToInt32(Request.QueryString["VesselID"].ToString()), Convert.ToInt32(Session["WID"].ToString()), Convert.ToInt32(Session["OID"].ToString()), Convert.ToInt32(Session["USERID"]), txtWorklistStatusRemark.Text, Convert.ToString(ViewState["WORKLIST_STATUS"]));
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VesselID"].ToString());
        int Worklist_ID = UDFLib.ConvertToInteger(Session["WID"].ToString());
        int Office_ID = UDFLib.ConvertToInteger(Session["OID"].ToString());


        Search_Worklist();
        ClearPage();
        Session["Mode"] = "ADD";
        BtnVerify.Enabled = false;
        hdnOPMode.Value = "ADD";
        //BtnAttach.Enabled = false;
        txtDesc.Enabled = true;
        //Load_Attachments(Vessel_ID, Worklist_ID, Office_ID, GetSessionUserID());
        // DataTable dtpkid = (DataTable)Session["WORKLIST_PKID_NAV"];
        //fillvalue(dtpkid.Rows.Find(new object[] { Worklist_ID, Vessel_ID, Office_ID }));
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Load_Attachments(Convert.ToInt32(Session["VID"]), Convert.ToInt32(Session["WID"]), Convert.ToInt32(Session["OID"]), int.Parse(Session["USERID"].ToString()));
    }

}