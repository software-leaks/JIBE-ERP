using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.VET;
using System.Data;
using EO.Pdf;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Technical_Vetting_Vetting_VettingDetails : System.Web.UI.Page
{      
    public string strDateFormat = "";
    public string strCheckVetStatus = "";   
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
            String msgretv = String.Format("setTimeout(getOperatingSystem,500);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
            UserAccessValidation();

            if (!IsPostBack)
            {
                if (Request.QueryString["Vetting_ID"].ToString() != "" && Request.QueryString["Vetting_ID"] != null)
                {
                                  
                    ViewState["ValidUntil_Date"] = "";
                    ViewState["Vetting_Status"] = "";
                    int VettingID = UDFLib.ConvertToInteger(Request.QueryString["Vetting_ID"].ToString());
                    ViewState["VettingID"] = VettingID;
                    BindFilter(VettingID);
                    BindObservation(VettingID);
                    ImgAddNewObs.Attributes.Add("onClick", "CreateNewObs(" + VettingID + "," + ViewState["FleetCode"].ToString() + "," + ViewState["Vessel_ID"].ToString() + ")");
                    ImgVetAttchement.Attributes.Add("onClick", "AddAttachment(" + VettingID + ")");
                    ImgRemark.Attributes.Add("onClick", "AddRemarks(" + VettingID + ")");
                    btnPerVetting.Attributes.Add("onClick", "onPerformVetting(" + VettingID + ")");
                    hdfVettingID.Value = Request.QueryString["Vetting_ID"].ToString();
                    ImgImportObs.Attributes.Add("onClick", "OnImportObservation(" + VettingID + ")");                  


                }

                if (ViewState["VettingID"] != null)
                {
                    BLL_VET_Index objIndex = new BLL_VET_Index();
                    DataTable dt = objIndex.VET_Get_ImportObs_ErrorLog(Convert.ToInt32(ViewState["VettingID"]));
                    if (dt.Rows.Count > 0)
                    {
                        btnForce_Download.Visible = true;
                    }
                    BindObservation(Convert.ToInt32(ViewState["VettingID"]));
                }
            }

           
            if (ddlVettingStatus.SelectedValue != "Planned")
            {
                String tglsearchpostback = String.Format("toggleSerachPostBack(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglsearchpostback", tglsearchpostback, true);
            
            }
            setDateFormat();
            MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
            objVetMerge.AddMergedColumns(new int[] { 9, 10, 11, 12, 13, 14 }, "Jobs", "GroupHeaderStyle-css HeaderStyle-css");
          
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to check wheather user have access to this page or not.
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            UserAccess objUA = new UserAccess();
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {


            }

            ViewState["del"] = objUA.Delete;
            ViewState["edit"] = objUA.Edit;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Bind entire page data
    /// </summary>
    /// <param name="VettingID">Vetting ID for which data to displayed</param>
    public void BindObservation(int VettingID)
    {
        try
        {
            string dateFormat = UDFLib.GetDateFormat();
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();
            DataSet ds = new DataSet();

            DataTable dtSection = new DataTable();
            DataTable dtQuestion = new DataTable();
            DataTable dtObsType = new DataTable();
            DataTable dtObsState = new DataTable();
            DataTable dtJState = new DataTable();
            DataTable dtCategory = new DataTable();
            DataTable dtRiskLevel = new DataTable();

            dtSection.Columns.Add("VID");
            dtQuestion.Columns.Add("VID");
            dtObsType.Columns.Add("VTID");
            dtObsState.Columns.Add("Value");
            dtJState.Columns.Add("Value");
            dtCategory.Columns.Add("VID");
            dtRiskLevel.Columns.Add("VID");

            dtSection = DDLSection.SelectedValues;
            dtQuestion = DDLQuestion.SelectedValues;
            dtObsType = DDLObsType.SelectedValues;
            dtObsState = DDLObsStatus.SelectedValues;
            dtJState = DDLJobStatus.SelectedValues;
            dtCategory = DDLCategory.SelectedValues;
            dtRiskLevel = DDLRiskLevel.SelectedValues;

            ds = objBlPlan.VET_Get_VettingDetails(VettingID, dtSection, dtQuestion, dtObsType, dtObsState, dtJState, dtCategory, dtRiskLevel, sortbycoloumn, sortdirection);
            if (ds.Tables.Count > 0)
            {
               
                
                DataTable dtVetDetails = ds.Tables[0];
                DataTable dtObs=new DataTable();
                DataTable dtJobsCount=new DataTable();
                DataTable dtResCount=new DataTable();
                DataTable dtOpenObsCount=new DataTable();

                if(ds.Tables.Count>0)
                 dtObs = ds.Tables[1];

                if(ds.Tables.Count>1)
                dtJobsCount = ds.Tables[2];

                if (ds.Tables.Count > 2)
                dtResCount = ds.Tables[3];

                if (ds.Tables.Count > 3)
                dtOpenObsCount = ds.Tables[4];

                if (dtVetDetails.Rows.Count > 0)
                {
                    ddlVettingStatus.SelectedValue = dtVetDetails.Rows[0]["Vetting_Status"].ToString();
                    ViewState["Vetting_Status"] = dtVetDetails.Rows[0]["Vetting_Status"].ToString();
                    DateTime Vetting_Date = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(dtVetDetails.Rows[0]["Vetting_Date"].ToString()));
                    DateTime ValidUntil_Date;
                    ViewState["Vetting_Status"] = dtVetDetails.Rows[0]["Vetting_Status"].ToString();
                    hdnStatus.Value = dtVetDetails.Rows[0]["Vetting_Status"].ToString();
                    if (ViewState["Vetting_Status"].ToString() == "Completed")
                    {
                        ImgRework.Visible = true;
                        ValidUntil_Date = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(dtVetDetails.Rows[0]["Valid_Until_Date"].ToString()));
                    }
                    else
                    {
                        ValidUntil_Date = Vetting_Date.AddDays(UDFLib.ConvertToInteger(dtVetDetails.Rows[0]["Expires_In_Days"].ToString()));
                    }
                    ViewState["Vessel_ID"] = dtVetDetails.Rows[0]["Vessel_ID"].ToString();
                    ViewState["FleetCode"] = dtVetDetails.Rows[0]["FleetCode"].ToString();
                    ViewState["Vessel_Name"] = dtVetDetails.Rows[0]["Vessel_Name"].ToString();
                    ViewState["ValidUntil_Date"] = UDFLib.ConvertUserDateFormat(ValidUntil_Date.ToString());
                    if (dtVetDetails.Rows[0]["Response_Next_Due_Date"].ToString() != "")
                    {
                        ViewState["Response_Next_Due_Date"] = UDFLib.ConvertUserDateFormat(dtVetDetails.Rows[0]["Response_Next_Due_Date"].ToString());
                    }
                    else
                    {
                        ViewState["Response_Next_Due_Date"] = "";
                    }

                    ViewState["Vetting_Type_ID"] = dtVetDetails.Rows[0]["Vetting_Type_ID"].ToString();
                    ViewState["Vetting_Type_Name"] = dtVetDetails.Rows[0]["Vetting_Type_Name"].ToString();

                    lblVettingName.Text = dtVetDetails.Rows[0]["Vetting_Name"].ToString();
                    lblSVetName.Text = dtVetDetails.Rows[0]["Vetting_Name"].ToString();
                    lblVesName.Text = dtVetDetails.Rows[0]["Vessel_Name"].ToString();
                    lblSVesName.Text = dtVetDetails.Rows[0]["Vessel_Name"].ToString();
                    lblVetType.Text = dtVetDetails.Rows[0]["Vetting_Type_Name"].ToString();
                    if (lblVetType.Text == "SIRE")
                    {
                        ImgImportObs.Visible = true;
                    }
                    lblSVetType.Text = dtVetDetails.Rows[0]["Vetting_Type_Name"].ToString();
                    lblQuesnrName.Text = dtVetDetails.Rows[0]["Questionnaire_Name"].ToString();
                    hdnQuestionnaireID.Value = dtVetDetails.Rows[0]["Questionnaire_ID"].ToString();
                    lblInspName.Text = dtVetDetails.Rows[0]["Inspector_Name"].ToString();
                    hdnInsptrID.Value = dtVetDetails.Rows[0]["Inspector_ID"].ToString();
                    lblOM.Text = dtVetDetails.Rows[0]["Oil_Major_Name"].ToString();
                    hdnOMID.Value = dtVetDetails.Rows[0]["OMID"].ToString();
                    lblPort.Text = dtVetDetails.Rows[0]["PORT_NAME"].ToString();
                    hdnPortID.Value = dtVetDetails.Rows[0]["PORT_ID"].ToString();
                    lblVetDate.Text = UDFLib.ConvertUserDateFormat(dtVetDetails.Rows[0]["Vetting_Date"].ToString());

                    txtVetDate.Text = UDFLib.ConvertUserDateFormat(dtVetDetails.Rows[0]["Vetting_Date"].ToString());
                    txtVettingDate.Text = UDFLib.ConvertUserDateFormat(dtVetDetails.Rows[0]["Vetting_Date"].ToString());                               
                    if (dtVetDetails.Rows[0]["No_Of_Days"].ToString() != "")
                    {
                        txtNoOfDays.Text = dtVetDetails.Rows[0]["No_Of_Days"].ToString();
                        lblNoOdDays.Text = dtVetDetails.Rows[0]["No_Of_Days"].ToString();     
                    }
                    else
                    {
                        txtNoOfDays.Text = "1";
                        lblNoOdDays.Text = "1";
                    }
                    EnableDisableVisibleControls(dtVetDetails.Rows[0]["Vetting_Status"].ToString());
                }

                ViewState["dtJobsCount"] = dtJobsCount;
                ViewState["dtResCount"] = dtResCount;
                ViewState["dtOpenObsCount"] = dtOpenObsCount;
                ViewState["dtObs"] = dtObs;
                gvObs.DataSource = dtObs;
                gvObs.DataBind();
                gvObs.SelectedIndex = -1;
                hdfObsCount.Value = dtObs.Rows.Count.ToString();
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
    /// Added on DT:02-07-2016.To force download all attchment.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public void btnForce_Download_Click(object s, EventArgs e)
    {
        try
        {
            DownloadAllAttchment(Convert.ToInt32(ViewState["VettingID"]));
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }
    /// <summary>
    ///  To download error log file.
    /// </summary>
    private void DownloadAllAttchment(int VettingID)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
            DataTable dt = objIndex.VET_Get_ImportObs_ErrorLog(VettingID);
            if (dt.Rows.Count > 0)
            {
                string _filePath = dt.Rows[0]["File_Path"].ToString();


                if (File.Exists(Server.MapPath(_filePath)) == true)
                {
                    string FileName = new FileInfo(_filePath).Name;
                    string FileExtension = new FileInfo(_filePath).Extension;
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AppendHeader("content-disposition", "attachment;filename=" + FileName);
                    Response.TransmitFile(Server.MapPath(_filePath));
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsNoFileerror", "alert('File not found.');", true);


                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Enable or disable and Visible or Hide the Control based on Status
    /// </summary>
    /// <param name="Status">Status of Vetting</param>
    public void EnableDisableVisibleControls(string Status)
    {
        try
        {
            string dateFormat = UDFLib.GetDateFormat();

            if (Status == "Completed")
            {
                PnlObsJobs.Visible = true;
                ddlVettingStatus.Enabled = false;
                BtnEdit.Enabled = false;
                ImgAddNewObs.Enabled = false;
                ImgVetAttchement.Enabled = false;
                ImgRemark.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#00DA63");
                lblValidUntil.Visible = true;
                txtValidUntil.Visible = true;
                lblRespondBy.Visible = false;
                txtRespondBy.Visible = false;

                pnlPerformVet.Visible = false;
                txtFValidUntil.Visible = false;
                lblVettingDate.Visible = false;
                txtVettingDate.Visible = false;
                ImgRework.Visible = true;

                if (ViewState["ValidUntil_Date"].ToString() != "")
                {
                    txtValidUntil.Text = ViewState["ValidUntil_Date"].ToString();
                }
                else
                {
                    txtValidUntil.Text = "";
                }
            }
            else if (Status == "Failed")
            {
                ddlVettingStatus.Enabled = false;
                PnlObsJobs.Visible = true;
                BtnEdit.Enabled = false;
                ImgAddNewObs.Enabled = false;
                ImgVetAttchement.Enabled = false;
                ImgRemark.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.Color.Red;
                lblValidUntil.Visible = true;
                txtValidUntil.Visible = false;
                txtFValidUntil.Visible = true;
                lblRespondBy.Visible = false;
                txtRespondBy.Visible = false;
                pnlPerformVet.Visible = false;
                lblVettingDate.Visible = false;
                txtVettingDate.Visible = false;
                ImgRework.Visible = true;

            }
            else if (Status == "In-Progress")
            {
                PnlObsJobs.Visible = true;
                ImgRework.Visible = false;
                BtnEdit.Enabled = true;
                ImgAddNewObs.Enabled = true;
                ImgVetAttchement.Enabled = true;
                ImgRemark.Enabled = true;

                ddlVettingStatus.Enabled = true;
                ddlVettingStatus.BackColor = System.Drawing.Color.Yellow;
                lblRespondBy.Visible = true;
                txtRespondBy.Visible = true;
                lblValidUntil.Visible = false;
                txtValidUntil.Visible = false;
                txtFValidUntil.Visible = false;
                pnlPerformVet.Visible = false;
                lblVettingDate.Visible = false;
                txtVettingDate.Visible = false;


                if (ViewState["Response_Next_Due_Date"].ToString() != "")
                {
                    txtRespondBy.Text = ViewState["Response_Next_Due_Date"].ToString();
                }
                else
                {
                    txtRespondBy.Text = "";
                }
            }
            else if (Status == "Planned")
            {
                ddlVettingStatus.Enabled = false;
                BtnEdit.Enabled = true;
                ImgRework.Visible = false;
                PnlObsJobs.Visible = false;
                ddlVettingStatus.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#9BC2E6");
                lblVettingDate.Visible = true;
                txtVettingDate.Visible = true;
                txtVettingDate.Enabled = false;
                lblRespondBy.Visible = false;
                txtRespondBy.Visible = false;
                lblValidUntil.Visible = false;
                txtValidUntil.Visible = false;
                txtFValidUntil.Visible = false;
                pnlPerformVet.Visible = true;


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// This is use to change status of control on changing status in dropdown
    /// </summary>
    /// <param name="Status"></param>
    public void EnableDisableVisibleControlsOnStatusChange(string Status)
    {
        try
        {
            string dateFormat = UDFLib.GetDateFormat();

            if (Status == "Completed")
            {
                PnlObsJobs.Visible = true;

                BtnEdit.Enabled = false;
                ImgAddNewObs.Enabled = false;
                ImgVetAttchement.Enabled = false;
                ImgRemark.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#00DA63");
                lblValidUntil.Visible = true;
                txtValidUntil.Visible = true;
                lblRespondBy.Visible = false;
                txtRespondBy.Visible = false;

                pnlPerformVet.Visible = false;
                txtFValidUntil.Visible = false;
                lblVettingDate.Visible = false;
                txtVettingDate.Visible = false;


                if (ViewState["ValidUntil_Date"].ToString() != "")
                {
                    txtValidUntil.Text = ViewState["ValidUntil_Date"].ToString();
                }
                else
                {
                    txtValidUntil.Text = "";
                }
            }
            else if (Status == "Failed")
            {

                PnlObsJobs.Visible = true;
                BtnEdit.Enabled = false;
                ImgAddNewObs.Enabled = false;
                ImgVetAttchement.Enabled = false;
                ImgRemark.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.Color.Red;
                lblValidUntil.Visible = true;
                txtValidUntil.Visible = false;
                txtFValidUntil.Visible = true;
                lblRespondBy.Visible = false;
                txtRespondBy.Visible = false;
                pnlPerformVet.Visible = false;
                lblVettingDate.Visible = false;
                txtVettingDate.Visible = false;


            }
            else if (Status == "In-Progress")
            {
                PnlObsJobs.Visible = true;
                ImgRework.Visible = false;
                BtnEdit.Enabled = true;
                ImgAddNewObs.Enabled = true;
                ImgVetAttchement.Enabled = true;
                ImgRemark.Enabled = true;

                ddlVettingStatus.Enabled = true;
                ddlVettingStatus.BackColor = System.Drawing.Color.Yellow;
                lblRespondBy.Visible = true;
                txtRespondBy.Visible = true;
                lblValidUntil.Visible = false;
                txtValidUntil.Visible = false;
                txtFValidUntil.Visible = false;
                pnlPerformVet.Visible = false;
                lblVettingDate.Visible = false;
                txtVettingDate.Visible = false;


                if (ViewState["Response_Next_Due_Date"].ToString() != "")
                {
                    txtRespondBy.Text = ViewState["Response_Next_Due_Date"].ToString();
                }
                else
                {
                    txtRespondBy.Text = "";
                }
            }
            else if (Status == "Planned")
            {

                BtnEdit.Enabled = true;
                ImgRework.Visible = false;
                PnlObsJobs.Visible = false;
                ddlVettingStatus.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#9BC2E6");
                lblVettingDate.Visible = true;
                txtVettingDate.Visible = true;
                txtVettingDate.Enabled = false;
                lblRespondBy.Visible = false;
                txtRespondBy.Visible = false;
                lblValidUntil.Visible = false;
                txtValidUntil.Visible = false;
                txtFValidUntil.Visible = false;
                pnlPerformVet.Visible = true;


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Set Date for to date pickers in page as per Date format set in User Setting
    /// </summary>
    public void setDateFormat()
    {
        try
        {
            txtVetDate_CalendarExtender.Format = UDFLib.GetDateFormat();
            CalValidUntil.Format = UDFLib.GetDateFormat();
            CalVetDate.Format = UDFLib.GetDateFormat();
            calResBy.Format = UDFLib.GetDateFormat();
            CalFValidUntil.Format = UDFLib.GetDateFormat();
            strDateFormat = UDFLib.GetDateFormat();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Bind All Filters as per vetting ID
    /// </summary>
    /// <param name="VettingID">Vetting ID</param>
    public void BindFilter(int VettingID)
    {
        try
        {

            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();
            DataSet ds = new DataSet();

            ds = objBlPlan.VET_GET_FiltersForVetting(VettingID);
            if (ds.Tables.Count > 0)
            {

                DDLSection.DataSource = ds.Tables[0];
                DDLSection.DataTextField = "Section_No";
                DDLSection.DataValueField = "Section_No";
                DDLSection.DataBind();

                DDLQuestion.DataSource = ds.Tables[1];
                DDLQuestion.DataTextField = "Question_No";
                DDLQuestion.DataValueField = "Question_ID";
                DDLQuestion.DataBind();

                DDLObsType.DataSource = ds.Tables[2];
                DDLObsType.DataTextField = "Observation_Type_Name";
                DDLObsType.DataValueField = "Observation_Type_ID";
                DDLObsType.DataBind();


                DDLCategory.DataSource = ds.Tables[3];
                DDLCategory.DataTextField = "OBSCategory_Name";
                DDLCategory.DataValueField = "OBSCategory_ID";
                DDLCategory.DataBind();

                DataTable dtObsStatus = new DataTable();
                dtObsStatus.Columns.Add("Status");
                DataRow dr = dtObsStatus.NewRow();
                dr[0] = "Open";
                dtObsStatus.Rows.Add(dr);
                dr = dtObsStatus.NewRow();
                dr[0] = "Closed";
                dtObsStatus.Rows.Add(dr);

                DataTable dtJStatus = new DataTable();
                dtJStatus.Columns.Add("Status");
                DataRow drj = dtJStatus.NewRow();
                drj[0] = "PENDING";
                dtJStatus.Rows.Add(drj);
                drj = dtJStatus.NewRow();
                drj[0] = "COMPLETED";
                dtJStatus.Rows.Add(drj);
                drj = dtJStatus.NewRow();
                drj[0] = "VERIFY";
                dtJStatus.Rows.Add(drj);
                drj = dtJStatus.NewRow();
                drj[0] = "DEFERRED";
                dtJStatus.Rows.Add(drj);

                DataTable dtRiskLevel = new DataTable();
                DataRow drl;
                dtRiskLevel.Columns.Add("Level");
                for (int i = 1; i <= 5; i++)
                {
                    drl = dtRiskLevel.NewRow();
                    drl[0] = i;
                    dtRiskLevel.Rows.Add(drl);
                }
                DDLRiskLevel.DataSource = dtRiskLevel;
                DDLRiskLevel.DataTextField = "Level";
                DDLRiskLevel.DataValueField = "Level";
                DDLRiskLevel.DataBind();

                DDLObsStatus.DataSource = dtObsStatus;
                DDLObsStatus.DataTextField = "Status";
                DDLObsStatus.DataValueField = "Status";
                DDLObsStatus.DataBind();

                DDLJobStatus.DataSource = dtJStatus;
                DDLJobStatus.DataTextField = "Status";
                DDLJobStatus.DataValueField = "Status";
                DDLJobStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Questionnaire Dropdown
    /// </summary>
    public void BindQuestionnaire(int Vessel_ID, int Vetting_Type_ID)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
            DataTable dt = objIndex.VET_GET_Published_QuestionnireList(Vessel_ID, Vetting_Type_ID);
            DDLQuestionnaire.DataSource = dt;
            DDLQuestionnaire.DataValueField = "Questionnaire_ID";
            DDLQuestionnaire.DataTextField = "Name";
            DDLQuestionnaire.DataBind();
            DDLQuestionnaire.Items.Insert(0, new ListItem("-SELECT-", "0"));
            DDLQuestionnaire.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    /// <summary>
    /// Bind Oil Major Dropdown
    /// </summary>
    public void BindOilMajors()
    {
        try
        {
            BLL_VET_VettingLib objLib = new BLL_VET_VettingLib();
            DataTable dt = objLib.VET_Get_OilMajorList();

            DDLOilMajor.DataSource = dt;
            DDLOilMajor.DataValueField = "ID";
            DDLOilMajor.DataTextField = "Display_Name";
            DDLOilMajor.DataBind();
            DDLOilMajor.Items.Insert(0, new ListItem("-SELECT-", "0"));
            DDLOilMajor.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Port List
    /// </summary>
    public void BindPortList()
    {
        try
        {

            BLL_VET_Index objIndex = new BLL_VET_Index();
            DataTable dt = objIndex.VET_Get_PortList();

            DDLPort.DataSource = dt;
            DDLPort.DataValueField = "PORT_ID";
            DDLPort.DataTextField = "PORT_NAME";
            DDLPort.DataBind();
            DDLPort.Items.Insert(0, new ListItem("-SELECT-", "0"));
            DDLPort.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Inspector Dropdown
    /// </summary>
    public void BindInspector()
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
            DataTable dt = objIndex.VET_Get_InspectorListByVettingType(UDFLib.ConvertToInteger(ViewState["Vetting_Type_ID"].ToString()));

            DDLInspector.DataSource = dt;
            DDLInspector.DataValueField = "UserID";
            DDLInspector.DataTextField = "NAME";
            DDLInspector.DataBind();
            DDLInspector.Items.Insert(0, new ListItem("-SELECT-", "0"));
            DDLInspector.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void DDLSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int VettingID = UDFLib.ConvertToInteger(ViewState["VettingID"].ToString());
            BindObservation(VettingID);

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
            BLL_VET_Index objIndex = new BLL_VET_Index();
            DataTable dt = objIndex.VET_Get_ImportObs_ErrorLog(Convert.ToInt32(ViewState["VettingID"]));
            if (dt.Rows.Count > 0)
            {
                btnForce_Download.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));

            ExportGridviewToExcel(gvObs, "Vetting Details");

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DDLSection.ClearSelection();
            DDLQuestion.ClearSelection();
            DDLObsType.ClearSelection();
            DDLJobStatus.ClearSelection();
            DDLObsStatus.ClearSelection();
            DDLCategory.ClearSelection();
            DDLRiskLevel.ClearSelection();
            updCategory.Update();
            updRiskLevel.Update();
            BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
            UpdPnlGrid.Update();

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
        }

    }
    protected void gvObs_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
                MergeGridviewHeader.SetProperty(objVetMerge);
                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ImgDelete_Click(object sender, CommandEventArgs e)
    {
        try
        {
            BLL_VET_Planner objPlan = new BLL_VET_Planner();
            string inputString = e.CommandArgument.ToString();
            int Observation_ID = UDFLib.ConvertToInteger(inputString.Split(',')[0]);
            int Question_ID = UDFLib.ConvertToInteger(inputString.Split(',')[1]);
            int Vetting_ID = UDFLib.ConvertToInteger(inputString.Split(',')[2]);
            int res = objPlan.VET_Del_Observation(Observation_ID, Question_ID, Vetting_ID, GetSessionUserID());
            BindObservation(Vetting_ID);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvObs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
                MergeGridviewHeader.SetProperty(objVetMerge);

                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                ViewState["DynamicHeaderCSS"] = "GroupHeaderStyle-css HeaderStyle-css";

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Height = 36;
                DataTable dtJobsCount = (DataTable)ViewState["dtJobsCount"];

                Label lblQID = (Label)e.Row.FindControl("lblQuestionID");
                Label lblOBID = (Label)e.Row.FindControl("lblOBSID");
                HyperLink ImgNewJob = (HyperLink)e.Row.FindControl("ImgNewJob");
                HyperLink lnkRelatedObs = (HyperLink)e.Row.FindControl("lnkRelatedObs");
                Label lblQuestionID = (Label)e.Row.FindControl("lblQuestionID");
                Label lblOBSID = (Label)e.Row.FindControl("lblOBSID");
                HiddenField hdnFleetCode = (HiddenField)e.Row.FindControl("hdnFleetCode");
                ImageButton ImgAssignJob = (ImageButton)e.Row.FindControl("ImgAssignJob");
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEdit");
                ImageButton ImgDelete = (ImageButton)e.Row.FindControl("ImgDelete");
                LinkButton lnkOBSStatus = (LinkButton)e.Row.FindControl("lnkOBSStatus");
                if (ViewState["Vetting_Status"].ToString() == "In-Progress")
                {
                    ImgNewJob.Enabled = true;
                    lnkRelatedObs.Enabled = true;
                    ImgAssignJob.Enabled = true;
                    ImgEdit.Enabled = true;
                    ImgDelete.Enabled = true;
                    lnkOBSStatus.Enabled = true;
                }
                else
                {
                    ImgNewJob.Enabled = false;
                    lnkRelatedObs.Enabled = false;
                    ImgAssignJob.Enabled = false;
                    //ImgEdit.Enabled = false;
                    ImgDelete.Enabled = false;
                    lnkOBSStatus.Enabled = false;
                }

                for (int k = 0; k < gvObs.HeaderRow.Cells.Count; k++)
                {
                    string header = gvObs.HeaderRow.Cells[k].Text.Trim();
                    if (header.Equals("Section") || header.Equals("Type") || header.Equals("Content") || header.Equals("Category") || header.Equals("Risk Level") || header.Equals("Question") || header.Equals("Response"))
                    {
                        e.Row.Cells[k].Attributes.Add("onclick", "window.open('Vetting_ObservationIndex.aspx?Question_ID=" + lblQuestionID.Text + "&FleetCode=" + UDFLib.ConvertToInteger(hdnFleetCode.Value) + "&Vetting_Type_ID=" + ViewState["Vetting_Type_ID"] + "&Parent=VD" + "&Status=Open','_blank');");
                        e.Row.Cells[k].Attributes.Add("style", "cursor:pointer;");
                        e.Row.Cells[k].Attributes["onmousedown"] = ClientScript.GetPostBackClientHyperlink(this.gvObs, "Select$" + e.Row.RowIndex);
                    }

                }
             
           

                ImgNewJob.NavigateUrl = "../Worklist/AddNewJob.aspx?VID=" + ViewState["Vessel_ID"].ToString() + "&Vetting_ID=" + ViewState["VettingID"].ToString() + "&Question_ID=" + lblQuestionID.Text + "&Observation_ID=" + lblOBSID.Text + "&WLID=0&OFFID=0";

                lnkRelatedObs.NavigateUrl = "Vetting_ObservationIndex.aspx?Question_ID=" + lblQuestionID.Text + "&FleetCode=" + UDFLib.ConvertToInteger(hdnFleetCode.Value) + "&Vetting_Type_ID=" + ViewState["Vetting_Type_ID"] + "&Parent=VD" + "&Status=Open";

                ImgAssignJob.Attributes.Add("onclick", "document.getElementById('iFrmAssignJobs').src ='Vetting_AssignJobs.aspx?Observation_ID=" + lblOBSID.Text + "&Vessel_ID=" + ViewState["Vessel_ID"].ToString() + "'; $('#divAssignJobs').prop('title', 'Assign Jobs');showModal('divAssignJobs');return false;");

                //e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Display, "none");
                //e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;");
                //e.Row.Cells[2].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlin k(this.gvObs, "Select$" + e.Row.RowIndex);


                string filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(lblQID.Text) + " and Observation_ID=" + UDFLib.ConvertToInteger(lblOBID.Text) + " and Job_Status='PENDING'";
                DataRow[] drPenJobsCount = dtJobsCount.Select(filterpenexpression);


                LinkButton lnkPJobs = (LinkButton)e.Row.FindControl("lnkPenJobsCnt");
                if (drPenJobsCount.Length <= 0)
                {
                    lnkPJobs.Text = "0";
                }
                else
                {
                    lnkPJobs.Text = drPenJobsCount[0][0].ToString();
                }



                filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(lblQID.Text) + " and Observation_ID=" + UDFLib.ConvertToInteger(lblOBID.Text) + " and Job_Status='COMPLETED'";
                DataRow[] drComJobsCount = dtJobsCount.Select(filterpenexpression);
                LinkButton lblCJobs = (LinkButton)e.Row.FindControl("lnkCompletedJobsCnt");
                if (drComJobsCount.Length <= 0)
                {
                    lblCJobs.Text = "0";
                }
                else
                {
                    lblCJobs.Text = drComJobsCount[0][0].ToString();
                }


                filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(lblQID.Text) + " and Observation_ID=" + UDFLib.ConvertToInteger(lblOBID.Text) + " and Job_Status='VERIFY'";
                DataRow[] drVerJobsCount = dtJobsCount.Select(filterpenexpression);
                LinkButton lnkVJobs = (LinkButton)e.Row.FindControl("lnkVerJobsCnt");
                if (drVerJobsCount.Length <= 0)
                {
                    lnkVJobs.Text = "0";
                }
                else
                {
                    lnkVJobs.Text = drVerJobsCount[0][0].ToString();
                }

                filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(lblQID.Text) + " and Observation_ID=" + UDFLib.ConvertToInteger(lblOBID.Text) + " and Job_Status='DEFERRED'";
                DataRow[] drDefJobsCount = dtJobsCount.Select(filterpenexpression);
                LinkButton lblDJobs = (LinkButton)e.Row.FindControl("lnkDefJobsCnt");
                if (drDefJobsCount.Length <= 0)
                {
                    lblDJobs.Text = "0";
                }
                else
                {
                    lblDJobs.Text = drDefJobsCount[0][0].ToString();
                }




                DataTable dtResCount = (DataTable)ViewState["dtResCount"];
                filterpenexpression = filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(lblQID.Text) + " and Observation_ID=" + UDFLib.ConvertToInteger(lblOBID.Text);
                DataRow[] drResCount = dtResCount.Select(filterpenexpression);
                Label lblResCnt = (Label)e.Row.FindControl("lblResponseCnt");
                if (drResCount.Length <= 0)
                {
                    lblResCnt.Text = "0";
                }
                else
                {
                    lblResCnt.Text = drResCount[0][0].ToString();
                }

                DataTable dtOpenObsCount = (DataTable)ViewState["dtOpenObsCount"];
                filterpenexpression = filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(lblQID.Text) + " and FleetCode=" + UDFLib.ConvertToInteger(hdnFleetCode.Value);
                DataRow[] drOpnObsCount = dtOpenObsCount.Select(filterpenexpression);
                if (drOpnObsCount.Length <= 0)
                {
                    lnkRelatedObs.Text = "0";
                    lnkRelatedObs.Enabled = false;
                }
                else
                {
                    lnkRelatedObs.Text = drOpnObsCount[0][0].ToString();
                    lnkRelatedObs.Enabled = true;
                }

                ImgEdit.Attributes.Add("onclick", "document.getElementById('iFrmNewObs').src ='Vetting_AddObservationNotes.aspx?FleetCode=" + UDFLib.ConvertToInteger(hdnFleetCode.Value) + "&Vessel_ID=" + ViewState["Vessel_ID"].ToString() + "&Vessel_Name=" + ViewState["Vessel_Name"].ToString() + "&Observation_ID=" + lblOBSID.Text + "&Question_ID=" + lblQuestionID.Text + "&Vetting_ID=" + ViewState["VettingID"].ToString() + "&Vetting_Type_ID=" + ViewState["Vetting_Type_ID"].ToString() + "&Vetting_Type_Name=" + ViewState["Vetting_Type_Name"].ToString() + "&Vetting_Status=" + ViewState["Vetting_Status"].ToString() + "&Opn_Obs_Count=" + lnkRelatedObs.Text + "&Mode=Edit'; $('#dvNewObs').prop('title', 'Add Note/Observation');showModal('dvNewObs',true,CloseNote_Observation);return false;");

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void ddlVettingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Vetting_Status"].ToString() != ddlVettingStatus.SelectedValue)
            {
                if (ddlVettingStatus.SelectedValue == "Planned")
                {
                    ddlVettingStatus.SelectedValue = ViewState["Vetting_Status"].ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "statualert", "alert('Status cannot be changed to Planned.')", true);
                }

                ImgSaveStatus.Visible = true;
                ImgCancelSave.Visible = true;
            }
            else
            {
                ImgSaveStatus.Visible = false;
                ImgCancelSave.Visible = false;
            }
            hdnStatus.Value = ddlVettingStatus.SelectedValue;
            EnableDisableVisibleControlsOnStatusChange(ddlVettingStatus.SelectedValue);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    protected void ddlOBSStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            BLL_VET_Planner objPlan = new BLL_VET_Planner();
            DropDownList ddl_status = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl_status.Parent.Parent;
            int idx = row.RowIndex;




            string hdnObsID = ((HiddenField)row.Cells[0].FindControl("hdnObsID")).Value;
            LinkButton lnkOBSStatus = (LinkButton)row.FindControl("lnkOBSStatus");
            objPlan.VET_Upd_ObservationStatus(UDFLib.ConvertToInteger(hdnObsID), ddl_status.SelectedValue, GetSessionUserID());

            ddl_status.Visible = false;
            lnkOBSStatus.Visible = true;

            btnRetrieve_Click(null, null);



        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void btnRework_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
            int res = objIndex.VET_Upd_VettingReworkStatus(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), GetSessionUserID());

            BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// This will export data to Excel
    /// </summary>
    /// <param name="GridViewexp"> Object Of Gridview to be exported </param>
    /// <param name="ExportHeader"> Header to be set to exported excel</param>
    public void ExportGridviewToExcel(GridView GridViewexp, string ExportHeader)
    {
        try
        {
            // Reference your own GridView here
            string filename = String.Format("Vetting_Details_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(),
                DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.Charset = "";

            // SetCacheability doesn't seem to make a difference (see update)
            HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            HttpContext.Current.Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            // Replace all gridview controls with literals     
            GridViewRow grh = GridViewexp.HeaderRow;


            grh.ForeColor = System.Drawing.Color.Black;
            grh.HorizontalAlign = HorizontalAlign.Left;
            GridViewexp.GridLines = GridLines.Both;


            grh.Cells[16].Visible = false;

            foreach (TableCell cl in grh.Cells)
            {


                cl.HorizontalAlign = HorizontalAlign.Left;

                cl.Attributes.Add("class", "text");
                PrepareControlForExport_GridView(cl);


            }


            foreach (GridViewRow gr in GridViewexp.Rows)
            {

                gr.Cells[16].Visible = false;
                foreach (TableCell cl in gr.Cells)
                {
                    cl.HorizontalAlign = HorizontalAlign.Left;
                    cl.Attributes.Add("class", "text");
                    PrepareControlForExport_GridView(cl);

                }
            }


            System.Web.UI.HtmlControls.HtmlForm form
                = new System.Web.UI.HtmlControls.HtmlForm();
            Controls.Add(form);

            Label lbl = new Label();
            lbl.Text = ExportHeader;
            lbl.Font.Size = 14;
            lbl.Font.Bold = true;
            form.Controls.Add(lbl);
            form.Controls.Add(GridViewexp);
            form.RenderControl(htmlWriter);
            string style = @"<style> .text { mso-number-format:\@; } </style> ";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Write(stringWriter.ToString());
            HttpContext.Current.Response.End();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// This will prepare gridview before export( for e.g This will convert link button to a normal text )
    /// </summary>
    /// <param name="control"> Object of the control to be converted to normal text+   </param>
    public static void PrepareControlForExport_GridView(Control control)
    {
        try
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];

                if (current is LinkButton)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as LinkButton).Text;

                }
                else if (current is ImageButton)
                {

                    TableCell cl = control as TableCell;
                    cl.Text = ""; //(current as ImageButton).ToolTip;
                }
                else if (current is HyperLink)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = ""; //(current as HyperLink).Text;
                }
                else if (current is DropDownList)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as DropDownList).Items.Count > 0 ? (current as DropDownList).SelectedItem.Text : ""; ;
                }
                else if (current is CheckBox)
                {

                    current.Visible = false;
                }
                else if (current is TextBox)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as TextBox).Text;

                }
                else if (current is System.Web.UI.WebControls.Image)
                {
                    TableCell cl = control as TableCell;

                    cl.Text = (current as System.Web.UI.WebControls.Image).AlternateText;

                }
                else if (current is Label)
                {
                    TableCell cl = control as TableCell;
                    cl.Text = (current as Label).Text;



                }



            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    private string GridViewToHtml(GridView gv)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
        return sb.ToString();
    }

    private void On_AfterRenderPage(object sender, EO.Pdf.PdfPageEventArgs e)
    {



    }

    protected void btnPdfExport_Click(object sender, ImageClickEventArgs e)
    {
        string jsOpenReport = "window.open('Vetting_Reports.aspx?Vetting_ID=" + ViewState["VettingID"].ToString() + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsOpenReport", jsOpenReport, true);
    }
    protected void btnPerVetting_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            pnlEdit.Visible = false;
            BindQuestionnaire(UDFLib.ConvertToInteger(ViewState["Vessel_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["Vetting_Type_ID"].ToString()));
            BindOilMajors();
            BindPortList();
            BindInspector();
            if (DDLQuestionnaire.Items.Count > 1)
            {
                DDLQuestionnaire.SelectedValue = hdnQuestionnaireID.Value == "" ? "0" : hdnQuestionnaireID.Value;
            }

            DDLInspector.SelectedValue = hdnInsptrID.Value == "" ? "0" : hdnInsptrID.Value;
            DDLPort.SelectedValue = hdnPortID.Value == "" ? "0" : hdnPortID.Value;
            DDLOilMajor.SelectedValue = hdnOMID.Value == "" ? "0" : hdnOMID.Value;
            pnlSave.Visible = true;
            if (ddlVettingStatus.SelectedValue == "Planned")
            {
                lblNOD.Visible = false;
                lblInspMandate.Visible = false;

            }
            else
            {
                lblNOD.Visible = true;
                lblInspMandate.Visible = true;
            }
        
            UpdatePanel3.Update();
            UpdatePanel4.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {
            pnlSave.Visible = true;

        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();

            DataTable dtVetting = objIndex.VET_Get_ExistVetting(UDFLib.ConvertToInteger(ViewState["Vessel_ID"].ToString()), UDFLib.ConvertToDate(txtVetDate.Text), UDFLib.ConvertToInteger(ViewState["Vetting_Type_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
            if (dtVetting.Rows.Count > 0)
            {
                string jsSqlError3 = "alert('Vetting already exists with same vessel,same type and same date.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
            }
            else
            {
                int res = objIndex.VET_Upd_VettingDetails(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), lblSVetName.Text, UDFLib.ConvertToInteger(ViewState["Vetting_Type_ID"].ToString()), lblVetType.Text, UDFLib.ConvertToDate(txtVetDate.Text), UDFLib.ConvertToInteger(DDLQuestionnaire.SelectedValue), UDFLib.ConvertToInteger(DDLInspector.SelectedValue), UDFLib.ConvertToInteger(DDLOilMajor.SelectedValue), UDFLib.ConvertToInteger(DDLPort.SelectedValue), UDFLib.ConvertToInteger(txtNoOfDays.Text), GetSessionUserID());

                pnlSave.Visible = false;
                BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
                pnlEdit.Visible = true;

                string jsFoot1 = " Get_RecordInfo();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFoot1", jsFoot1, true);


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvObs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Status")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                LinkButton lnkStatus = (LinkButton)e.CommandSource;
                DropDownList ddlStatus = (DropDownList)gvr.FindControl("ddlOBSStatus");
                ddlStatus.Visible = true;
                lnkStatus.Visible = false;
                ddlStatus.SelectedValue = lnkStatus.Text;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void ImgCancelSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImgSaveStatus.Visible = false;
            ImgCancelSave.Visible = false;
            ddlVettingStatus.SelectedValue = ViewState["Vetting_Status"].ToString();
            EnableDisableVisibleControls(ddlVettingStatus.SelectedValue);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ImgSaveStatus_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLL_VET_Planner objPlan = new BLL_VET_Planner();
            if (ddlVettingStatus.SelectedValue == "Completed")
            {
                DataTable dtObs = (DataTable)ViewState["dtObs"];
                DataRow[] dr = dtObs.Select("Status='Open'");
                if (dr.Length > 0)
                {
                    string jsStatusAlerst = "alert('Cannot Complete the Vetting.\\nThere are Open Observation Under Vetting');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsStatusAlerst", jsStatusAlerst, true);

                    ddlVettingStatus.SelectedValue = ViewState["Vetting_Status"].ToString();
                    ddlVettingStatus.Enabled = true;

                }
                else
                {

                    DateTime? ValidUntilDate = txtValidUntil.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtValidUntil.Text));
                    int res = objPlan.VET_Upd_VettingStatus(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), ValidUntilDate, ddlVettingStatus.SelectedValue, GetSessionUserID());
                    if (res >= 1)
                    {
                        BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));

                        ImgRework.Visible = true;
                        ImgSaveStatus.Visible = false;
                        ImgCancelSave.Visible = false;

                        string jsSaveMsg = " alert('Vetting Details Updated')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSaveMsg", jsSaveMsg, true);

                    }
                }
            }
            else if (ddlVettingStatus.SelectedValue == "In-Progress")
            {
                DateTime? RespondBy = txtRespondBy.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtRespondBy.Text));
                int res = objPlan.VET_Upd_VettingStatus(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), RespondBy, ddlVettingStatus.SelectedValue, GetSessionUserID());
                if (res >= 1)
                {
                    BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
                    ImgSaveStatus.Visible = false;
                    ImgCancelSave.Visible = false;
                    string jsSaveMsg1 = " alert('Vetting Details Updated')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSaveMsg1", jsSaveMsg1, true);



                }
            }
            else
            {
                int res = objPlan.VET_Upd_VettingStatus(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), UDFLib.ConvertDateToNull(txtValidUntil.Text), ddlVettingStatus.SelectedValue, GetSessionUserID());
                if (res >= 1)
                {
                    BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));

                    ImgRework.Visible = true;
                    ImgSaveStatus.Visible = false;
                    ImgCancelSave.Visible = false;
                    string jsSaveMsg2 = " alert('Vetting Details Updated')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSaveMsg2", jsSaveMsg2, true);

                }
            }
            EnableDisableVisibleControls(ddlVettingStatus.SelectedValue);
            if (ddlVettingStatus.SelectedValue == "Completed")
            {
                ddlVettingStatus.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#00DA63");
            }
            else if (ddlVettingStatus.SelectedValue == "Failed")
            {
                ddlVettingStatus.Enabled = false;
                ddlVettingStatus.BackColor = System.Drawing.Color.Red;

            }
            else if (ddlVettingStatus.SelectedValue == "In-Progress")
            {
                ddlVettingStatus.BackColor = System.Drawing.Color.Yellow;

            }
            else if (ddlVettingStatus.SelectedValue == "Planned")
            {
                ddlVettingStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#9BC2E6");
            }

            string jsFoot2 = " Get_RecordInfo();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFoot2", jsFoot2, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void txtValidUntil_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["ValidUntil_Date"].ToString() != txtRespondBy.Text)
        {
            ImgSaveStatus.Visible = true;
            ImgCancelSave.Visible = true;
        }
        else
        {
            ImgSaveStatus.Visible = false;
            ImgCancelSave.Visible = false;
        }

        hdnStatus.Value = ddlVettingStatus.SelectedValue;
    }
    protected void btnCreateVetting_Click(object sender, EventArgs e)
    {
        try
        {

            BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
            string jsFoot3 = " Get_RecordInfo();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFoot3", jsFoot3, true);
            UpdPnlGrid.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void txtRespondBy_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Response_Next_Due_Date"].ToString() != txtRespondBy.Text)
            {
                ImgSaveStatus.Visible = true;
                ImgCancelSave.Visible = true;
            }
            else
            {
                ImgSaveStatus.Visible = false;
                ImgCancelSave.Visible = false;
            }

            hdnStatus.Value = ddlVettingStatus.SelectedValue;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        pnlSave.Visible = false;
        pnlEdit.Visible = true;
    }

   
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();         
            int Vetting_ID = Convert.ToInt32(ViewState["VettingID"]);
            int IsDataInValid = 0;
            int IsObsExists = 0;
            if (FileUpXMLReport.HasFile)
            {
                var FileExtension = "." + Path.GetExtension(FileUpXMLReport.PostedFile.FileName).Substring(1);
                if (FileExtension != ".xml")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file type.');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowModal", "showModal('divImportObs', false);", true);
                    return;
                }
                else
                {
                    DataSet ds = new DataSet();
                    string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\ImportedObservation\\");

                    if (!Directory.Exists(Server.MapPath("~/uploads/Vetting/ImportedObservation")))
                        Directory.CreateDirectory(Server.MapPath("~/uploads/Vetting/ImportedObservation"));

                    Guid GUID = Guid.NewGuid();

                    string AttachPath = "VET_ImportObs" + GUID.ToString() + FileExtension;

                  
                    ViewState["AttachPath"] = AttachPath;
                    ViewState["FileName"] = Path.GetFileName(FileUpXMLReport.PostedFile.FileName);
                    FileUpXMLReport.SaveAs(sPath + AttachPath);
                    ds.ReadXml(sPath + AttachPath);
                    Session["VET_ImportDataset" + GUIDSession.Value.ToString()] = ds;
                    if (ds.Tables.Count > 0)
                    {
                        ImportObservation objImportObs = new ImportObservation();
                        if (string.IsNullOrWhiteSpace(objImportObs.ImportValidation(ds,Vetting_ID)))
                        {
                            Dictionary<string, string> objDicResult = objImportObs.ImportValueValidation(ds, hdnQuestionnaireID.Value, lblVesName.Text.Trim(), lblPort.Text, txtVetDate.Text, Vetting_ID, ref IsDataInValid, ref IsObsExists);
                            if (objDicResult.Keys.Contains("QUESTIONNAIRE"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + objDicResult["QUESTIONNAIRE"] + "');", true);
                            }                          
                           else if (string.IsNullOrWhiteSpace(objDicResult["ERRORMESSAGE"]))
                           {
                               if (IsDataInValid == 0 && IsObsExists == 0)
                               {
                                   if (string.IsNullOrWhiteSpace(objImportObs.SaveImportObservation(ds, hdnQuestionnaireID.Value,Vetting_ID)))
                                   {
                                       objIndex.VET_Ins_AttatmentImport(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), Path.GetFileName(FileUpXMLReport.PostedFile.FileName), AttachPath, GetSessionUserID());
                                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Observations/Notes saved successfully');", true);
                                       BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
                                       Session["VET_ImportDataset" + GUIDSession.Value.ToString()] = null;
                                       DataTable dt = objIndex.VET_Get_ImportObs_ErrorLog(UDFLib.ConvertToInteger(ViewState["VettingID"]));
                                       if (dt.Rows.Count > 0)
                                       {
                                           btnForce_Download.Visible = true;
                                       }

                                   }
                                   else
                                   {
                                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Error while importing.');", true);
                                       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowModal", "showModal('divImportObs', false);", true);
                                   }
                               }
                               else
                               {
                                   ScriptManager.RegisterStartupScript(this, typeof(Page), "Confirm", "ImportConfirm(" + IsDataInValid + "," + IsObsExists + ",'" + lblVesName.Text.Trim() + "','" + lblPort.Text+ "','" + txtVetDate.Text + "');", true);
                               }
                           }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file.');", true);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowModal", "showModal('divImportObs', false);", true);
                            }                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file.');", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowModal", "showModal('divImportObs', false);", true);
                        }
                    }                  

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('File does not exists.');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowModal", "showModal('divImportObs', false);", true);
                return;
            }


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnconfirm_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
                string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\ImportedObservation\\");

                DataSet ds = new DataSet();
           
                ds.ReadXml(sPath + ViewState["AttachPath"].ToString());
          
                ImportObservation objImportObs = new ImportObservation();
                if (string.IsNullOrWhiteSpace(objImportObs.SaveImportObservation(ds, hdnQuestionnaireID.Value,UDFLib.ConvertToInteger(ViewState["VettingID"]))))
                {

                    objIndex.VET_Ins_AttatmentImport(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()), ViewState["FileName"].ToString(), ViewState["AttachPath"].ToString(), GetSessionUserID());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Observations/Notes saved successfully');", true);
                    BindObservation(UDFLib.ConvertToInteger(ViewState["VettingID"].ToString()));
                    Session["VET_ImportDataset" + GUIDSession.Value.ToString()] = null;                  
                    DataTable dt = objIndex.VET_Get_ImportObs_ErrorLog(Convert.ToInt32(ViewState["VettingID"]));
                    if (dt.Rows.Count > 0)
                    {
                        btnForce_Download.Visible = true;
                    }
                  
                 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file.');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowModal", "showModal('divImportObs', false);", true);
                }
            


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}