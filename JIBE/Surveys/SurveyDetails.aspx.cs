using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Survey;
using System.Data;
using System.IO;
using System.Configuration;
using SMS.Business.Infrastructure;
using SMS.Business.Inspection;
using System.Drawing;
public partial class Surveys_SurveyDetails : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();
    BLL_Infra_UploadFileSize objBLL_Infra_UploadFileSize = new BLL_Infra_UploadFileSize();
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    BLL_Infra_Port objInfra = new BLL_Infra_Port();
    SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    decimal SURVEY_MAX_ATTACHMENT_SIZE = 0;
    long SIZE_BYTES = 0;
    public string OperationMode = "";
    public string DateFormat = "";
    public int AdminAccessRights = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateFormat = UDFLib.GetDateFormat();
            Calendar1.Format = Calendar2.Format = CalendarExtender2.Format = CalendarExtender3.Format = CalendarExtenderIssueDate.Format = DateFormat;
            DataTable dtSYNC = new DataTable();
            dtSYNC = objBLL_Infra_UploadFileSize.Get_Module_FileUpload("SURV_");
            SURVEY_MAX_ATTACHMENT_SIZE = UDFLib.ConvertToInteger(dtSYNC.Rows[0]["Size_KB"].ToString());

            SURVEY_MAX_ATTACHMENT_SIZE = SURVEY_MAX_ATTACHMENT_SIZE * 1024;
            UserAccessValidation();

            if (Request.QueryString.Count != 0)
            {
                int Surv_Details_ID1 = 0;
                int Surv_Vessel_ID1 = 0;
                int Vessel_ID1 = 0;
                int OfficeID1 = 0;
                int QueryStringOfficeId = 0;
                if (Request.QueryString["Surv_Details_ID"] != null && Request.QueryString["Surv_Details_ID"] != "")
                    Surv_Details_ID1 = Convert.ToInt16(Request.QueryString["Surv_Details_ID"].ToString());
                if (Request.QueryString["Surv_Vessel_ID"] != null && Request.QueryString["Surv_Vessel_ID"] != "")
                    Surv_Vessel_ID1 = Convert.ToInt16(Request.QueryString["Surv_Vessel_ID"].ToString());
                if (Request.QueryString["Vessel_ID"] != null && Request.QueryString["Vessel_ID"] != "")
                    Vessel_ID1 = Convert.ToInt16(Request.QueryString["Vessel_ID"].ToString());
                if (Request.QueryString["OfficeID"] != null && Request.QueryString["OfficeID"] != "")
                    OfficeID1 = Convert.ToInt16(Request.QueryString["OfficeID"].ToString());
                if (Request.QueryString["QueryStringOfficeId"] != null && Request.QueryString["QueryStringOfficeId"] != "")
                    QueryStringOfficeId = Convert.ToInt16(Request.QueryString["QueryStringOfficeId"].ToString());

                if (Request.QueryString["Method"] == "DeleteCertificate")
                {
                    DeleteCertificate(Surv_Details_ID1, Surv_Vessel_ID1, Vessel_ID1, OfficeID1, QueryStringOfficeId);
                    return;
                }
            }

            if (!IsPostBack)
            {
                Session["SaveFile"] = null;
                Load_IssuingAuthority();
                string Vessel_ID = GetQueryString("vid");
                string Surv_Vessel_ID = GetQueryString("s_v_id");
                string Surv_Details_ID = GetQueryString("s_d_id");
                string OfficeID = GetQueryString("off_id");
                if (Vessel_ID == "" && Surv_Vessel_ID == "")
                {
                    Vessel_ID = GetQueryString("VESSEL_ID");
                    Surv_Vessel_ID = GetQueryString("SURV_VESSEL_ID");
                    OfficeID = GetQueryString("OFFICEID");
                    Surv_Details_ID = GetQueryString("SURV_DETAILS_ID");

                }
                if (GetQueryString("s_d_id") != null && Convert.ToInt16(GetQueryString("s_d_id")) > 0)
                    ImgDeleteCertificate.Visible = true;
                else
                    ImgDeleteCertificate.Visible = false;
                hdnV_ID.Value = Vessel_ID;
                hdnO_ID.Value = OfficeID;
                hdnS_V_ID.Value = Surv_Vessel_ID;
                hdnS_D_ID.Value = Surv_Details_ID;

                lblDate.Text = DateTime.Today.ToString(DateFormat);

                btnAdd.Enabled = false;
                if (Vessel_ID != "" && Surv_Vessel_ID != "" && Surv_Details_ID != "0")
                {
                    BindSurvayDetailList(int.Parse(Vessel_ID), int.Parse(Surv_Vessel_ID), int.Parse(Surv_Details_ID), 0);
                }
                else if (Vessel_ID != "" && Surv_Vessel_ID != "")
                {
                    BindSurveyDetails(int.Parse(Vessel_ID), int.Parse(Surv_Vessel_ID));

                    ctlRecordNavigation1.Visible = false;
                    ImgBtnAddFollowup.Enabled = false;
                    pnlVerify.Visible = false;
                }
                Load_DocTypeList();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private void BindSurvayDetailList(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int CurrentIndex)
    {
        try
        {
            btnAdd.Enabled = true;
            ImgBtnAddFollowup.Enabled = true;
            int RecordNo = 0;
            DataTable dt = objBLL.Get_SurvayDetailList(Vessel_ID, Surv_Vessel_ID);
            string page = GetQueryString("page");
            if (page == "calendar" || CurrentIndex > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "Surv_Details_ID=" + Surv_Details_ID;
                RecordNo = UDFLib.ConvertToInteger(dv[0]["RowNo"]);
            }
            if (RecordNo > 0)
            {
                ctlRecordNavigation1.InitRecords(dt);
                ctlRecordNavigation1.MoveToIndex(RecordNo - 1);
            }
            else
            {
                ctlRecordNavigation1.InitRecords(dt);
                ctlRecordNavigation1.MoveLast();
            }
            hdnTotalRecords.Value = ctlRecordNavigation1.RecordCount.ToString();
            hdnCurrentRecordNumber.Value = ctlRecordNavigation1.CurrentIndex.ToString();
            pnlFollowup.Visible = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private void BindSurveyDetails(int Vessel_ID, int Surv_Vessel_ID)
    {
        try
        {
            DataSet ds = objBLL.Get_NewSurvayDetails(Vessel_ID, Surv_Vessel_ID);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString();
                    txtCategoryName.Text = dt.Rows[0]["Survey_Category"].ToString();
                    txtCertificateName.Text = dt.Rows[0]["Survey_Cert_Name"].ToString();
                    txtCertificateRemarks.Text = dt.Rows[0]["Survey_Cert_remarks"].ToString();
                    chkInspectionRequired.Checked = Convert.ToBoolean(dt.Rows[0]["InspectionRequired"]);
                    ImgBtnEditRemarks.Visible = true;
                    ImgBtnUpdateRemarks.Visible = false;
                    ImgBtnCancelRemarks.Visible = false;

                    txtTerm.Text = dt.Rows[0]["Term"].ToString();
                    txtMakeModel.Text = dt.Rows[0]["EquipmentType"].ToString();
                    txtIssuingAuthority.Text = dt.Rows[0]["IssuingAuth"].ToString();
                    txtGraceRange.Text = dt.Rows[0]["GraceRange"].ToString() == "0" ? "" : dt.Rows[0]["GraceRange"].ToString();
                    CalculatedExpiryDate();

                    if (ds.Tables.Count > 1)
                    {
                        DataTable dt1 = ds.Tables[1];
                        if (dt1.Rows.Count > 0)
                        {
                            txtCertInspection.Text = dt1.Rows[0]["Schedule_date"].ToString() != "" ? UDFLib.ConvertUserDateFormat(dt1.Rows[0]["Schedule_date"].ToString()).ToString() : "";
                            hdnCertInspection.Value = dt1.Rows[0]["RenewalInspection_ID"].ToString();
                        }
                    }
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
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["userid"]), UDFLib.GetPageURL(Request.Path.ToUpper()));

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnMakeAsNA.Visible = false;
            btnSave.Visible = false;
            btnAdd.Visible = false;
            ImgBtnEditRemarks.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            btnMakeAsNA.Visible = false;
            btnSave.Visible = false;
            btnAdd.Visible = false;
            ImgBtnEditRemarks.Visible = false;
        }

        if (objUA.Approve == 0)
            tdVerify.Visible = false;
        else
            tdVerify.Visible = true;

        if (objUA.Delete == 0)
            grdAttachments.Columns[7].Visible = false;

        if (objUA.Admin == 0)
        {
            AdminAccessRights = 0;
            pnlEditRemarks.Visible = false;
            ImgDeleteCertificate.Visible = false;
        }
        else
        {
            AdminAccessRights = 1;
            pnlEditRemarks.Visible = true;
            ImgDeleteCertificate.Visible = true;
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void ctlRecordNavigation1_NavigateRow(DataRow dr)
    {
        try
        {
            Show_SurveyDetails(dr);
            hdnTotalRecords.Value = ctlRecordNavigation1.RecordCount.ToString();
            hdnCurrentRecordNumber.Value = ctlRecordNavigation1.CurrentIndex.ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void Show_SurveyDetails(DataRow drIDList)
    {
        try
        {
            if (drIDList != null)
            {
                hdnV_ID.Value = drIDList["Vessel_ID"].ToString();
                hdnO_ID.Value = drIDList["OfficeID"].ToString();
                hdnS_V_ID.Value = drIDList["Surv_Vessel_ID"].ToString();
                hdnS_D_ID.Value = drIDList["Surv_Details_ID"].ToString();
                DataTable dt = objBLL.Get_SurvayDetails(UDFLib.ConvertToInteger(drIDList["Vessel_ID"].ToString()), UDFLib.ConvertToInteger(drIDList["Surv_Vessel_ID"].ToString()), UDFLib.ConvertToInteger(drIDList["Surv_Details_ID"].ToString()), UDFLib.ConvertToInteger(drIDList["OfficeID"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtVesselName.Text = dr["Vessel_Name"].ToString();
                    txtCategoryName.Text = dr["Survey_Category"].ToString();
                    txtCertificateName.Text = dr["Survey_Cert_Name"].ToString();
                    txtCertificateRemarks.Text = dr["Survey_Cert_remarks"].ToString();
                    txtGraceRange.Text = dr["GraceRange"].ToString();
                    chkInspectionRequired.Checked = Convert.ToBoolean(dr["InspectionRequired"]);
                    ImgBtnEditRemarks.Visible = true;
                    ImgBtnUpdateRemarks.Visible = false;
                    ImgBtnCancelRemarks.Visible = false;
                    hdnCertInspection.Value = dr["CertificateInspection_ID"].ToString();
                    hdnRenewwalInspection.Value = dr["RenewalInspection_ID"].ToString();
                    txtCertInspection.Text = UDFLib.ConvertUserDateFormat(dr["CertificateInspectionDate"].ToString());
                    txtRenewwalInspection.Text = UDFLib.ConvertUserDateFormat(dr["RenewalInspectionDate"].ToString());

                    hdnRenewwalInspectionStatus.Value = dr["RenewalInspectionStatus"].ToString();
                    hdnScheduleInspectionStatus.Value = dr["CertificateInspectionStatus"].ToString();
                    HighlightInspectionColor();
                    txtTerm.Text = dr["Term"].ToString();
                    txtMakeModel.Text = dr["EquipmentType"].ToString();
                    txtIssuingAuthority.Text = dr["IssuingAuth"].ToString();

                    string DateOfExpiry = dr["DateOfExpiry"].ToString();
                    string FollowupReminderDt = dr["FollowupReminderDt"].ToString();
                    string DateOfIssue = dr["DateOfIssue"].ToString();
                    string NAExpiryDate = dr["NAExpiryDate"].ToString();
                    string ExtensionDate = dr["ExtensionDate"].ToString();

                    if (ExtensionDate != "")
                        txtExtensionDate.Text = UDFLib.ConvertUserDateFormat(ExtensionDate, DateFormat);
                    else
                        txtExtensionDate.Text = "";

                    if (DateOfIssue != "")
                        txtDateOfIssue.Text = UDFLib.ConvertUserDateFormat(DateOfIssue, DateFormat);
                    else
                        txtDateOfIssue.Text = "";

                    if (DateOfExpiry != "")
                        txtDateOfExpiry.Text = UDFLib.ConvertUserDateFormat(DateOfExpiry, DateFormat);
                    else
                        txtDateOfExpiry.Text = "";

                    if (NAExpiryDate == "-1")
                    {
                        txtDateOfExpiry.Enabled = false;
                        chkNoExpiry.Checked = true;
                    }
                    else
                    {
                        txtDateOfExpiry.Enabled = true;
                        chkNoExpiry.Checked = false;
                    }

                    if (FollowupReminderDt != "")
                        txtReminderDate.Text = UDFLib.ConvertUserDateFormat(FollowupReminderDt, DateFormat);
                    else
                        txtReminderDate.Text = "";

                    txtFollowupDetails.Text = dr["FollowupReminder"].ToString();
                    txtRemarks.Text = dr["Remarks"].ToString();
                    txtVerifiedBy.Text = dr["Verified_By"].ToString();

                    if (dr["Verified_Date"].ToString() != "")
                        txtDateVerified.Text = UDFLib.ConvertUserDateFormat(dr["Verified_Date"].ToString(), DateFormat);
                    else
                        txtDateVerified.Text = "";


                    if (objUA.Delete == 0)
                        grdAttachments.Columns[7].Visible = false;
                    else
                        grdAttachments.Columns[7].Visible = true;


                    if (dr["Verified_By"].ToString() != "")
                    {
                        btnConfirmVerify.Visible = false;
                        btnConfirmUnVerify.Visible = true;
                        grdAttachments.Columns[7].Visible = false;
                    }
                    else
                    {
                        btnConfirmVerify.Visible = true;
                        btnConfirmUnVerify.Visible = false;
                    }


                    ListItem liAuth = ddlAuthority.Items.FindByValue(Convert.ToString(dr["IssuingAthority_ID"]));
                    if (liAuth != null)
                    {
                        ddlAuthority.ClearSelection();
                        liAuth.Selected = true;
                    }

                    txtCertificateNo.Text = dr["Certificate_No"].ToString();

                    if (dr["Surv_Details_ID"].ToString() != "0" && dr["Surv_Details_ID"].ToString() != "")
                    {
                        btnSave.Enabled = true;
                        UploadAttachments.Enabled = true;
                        btnAdd.Enabled = true;
                        pnlMessage.Visible = false;

                    }
                    else
                    {
                        btnSave.Enabled = false;
                        UploadAttachments.Enabled = false;
                        btnAdd.Enabled = false;
                        pnlMessage.Visible = true;

                    }

                    int Vessel_ID = int.Parse(dr["Vessel_ID"].ToString());
                    int Surv_Details_ID = int.Parse(dr["Surv_Details_ID"].ToString());
                    int OfficeID = int.Parse(dr["OfficeID"].ToString());

                    Show_Attachments(Vessel_ID, Surv_Details_ID, SIZE_BYTES);
                    Show_FollowUps(Vessel_ID, Surv_Details_ID, OfficeID);

                    CalculatedExpiryDate();
                    HighlightColor();
                    HighlightInspectionColor();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void txtReminderDate_TextChanged(object sender, EventArgs e)
    {
        HighlightColor();
    }

    protected void chkNoExpiry_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNoExpiry.Checked)
        {
            hdnExpiryDate.Value = txtDateOfExpiry.Text;
            txtDateOfExpiry.Text = "";
            txtDateOfExpiry.Enabled = false;
            txtDateOfExpiry.CssClass = "disabled";

        }
        else
        {
            txtDateOfExpiry.Enabled = true;
            txtDateOfExpiry.Text = hdnExpiryDate.Value;
            txtDateOfExpiry.CssClass = "required";
            HighlightColor();
        }
    }

    public string GetQueryString(string Query)
    {
        try
        {
            if (Request.QueryString[Query] != null)
            {
                return Request.QueryString[Query].ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }

    protected void grdAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AttachmentPath = DataBinder.Eval(e.Row.DataItem, "Attach_Path").ToString();
            string attachmentName = DataBinder.Eval(e.Row.DataItem, "Attach_Name").ToString();

            string AttachmentName = System.IO.Path.GetFileName(AttachmentPath);
            GridViewRow item = (GridViewRow)e.Row;
            if (((System.Web.UI.WebControls.Image)item.FindControl("imgRemarks")).AlternateText != string.Empty)
            {
                ((System.Web.UI.WebControls.Image)item.FindControl("imgRemarks")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + ((System.Web.UI.WebControls.Image)item.FindControl("imgRemarks")).AlternateText + "]");
            }
            else
            {
                ((System.Web.UI.WebControls.Image)item.FindControl("imgRemarks")).Visible = false;
            }


            HyperLink lnk = (HyperLink)(e.Row.FindControl("lblAttach_Name"));
            if (lnk != null)
            {
                lnk.Text = attachmentName;
                lnk.NavigateUrl = "~/Uploads/Survey/" + AttachmentPath;
            }
        }
    }

    protected void Show_FollowUps(int Vessel_ID, int Surv_Details_ID, int OfficeID)
    {
        DataTable dt = objBLL.Get_SurvayFollowups(Vessel_ID, Surv_Details_ID, OfficeID);
        grdFollowUp.DataSource = dt;
        grdFollowUp.DataBind();

    }

    protected void Show_Attachments(int Vessel_ID, int Surv_Details_ID, long SIZE_BYTES)
    {
        long Uploaded_Size = SIZE_BYTES;
        int refValue = 0;
        DataTable dt = objBLL.Get_SurvayAttachments(Vessel_ID, Surv_Details_ID, ref refValue);
        grdAttachments.DataSource = dt;
        grdAttachments.DataBind();

        decimal size_kb_uploaded = (Uploaded_Size / Convert.ToDecimal(1024));
        decimal size_kb_allowed = (SURVEY_MAX_ATTACHMENT_SIZE / Convert.ToDecimal(1024));
        hdnSizeAllowed.Value = (size_kb_allowed).ToString();
        lblSizeAllowed.Text = "&nbsp;&nbsp; For each upload max file size : " + size_kb_allowed.ToString("0") + " KB";
    }
    protected void btnCloseFollowup_OnClick(object sender, EventArgs e)
    {
        txtFollowUp.Text = "";
        rfv_txtFollowUp.Text = "";
        string divAddFollowUp = String.Format("hideModal('dvAddFollowUp');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divAddFollowUp", divAddFollowUp, true);
    }
    protected void btnSaveFollowUpAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {
            string Vessel_ID = hdnV_ID.Value;
            string OfficeID = hdnO_ID.Value;
            string Surv_Vessel_ID = hdnS_V_ID.Value;
            string Surv_Details_ID = hdnS_D_ID.Value;

            string FollowUpText = txtFollowUp.Text;

            if (Vessel_ID != "" && Surv_Details_ID != "" && OfficeID != "")
            {
                int iVessel_ID = int.Parse(Vessel_ID);
                int iSurv_Details_ID = int.Parse(Surv_Details_ID);
                int iOfficeID = int.Parse(OfficeID);

                objBLL.INSERT_New_Followup(iVessel_ID, iSurv_Details_ID, iOfficeID, FollowUpText, GetSessionUserID());
                txtFollowUp.Text = "";
                rfv_txtFollowUp.Text = "";
                string divAddFollowUp = String.Format("hideModal('dvAddFollowUp');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divAddFollowUp", divAddFollowUp, true);
                Show_FollowUps(iVessel_ID, iSurv_Details_ID, iOfficeID);
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    protected void btnUploadAttachments_Click(object sender, EventArgs e)
    {
        try
        {
            string Vessel_ID = hdnV_ID.Value;
            string OfficeID = hdnO_ID.Value;
            string Surv_Vessel_ID = hdnS_V_ID.Value;
            string Surv_Details_ID = hdnS_D_ID.Value;
            int iVessel_ID = int.Parse(Vessel_ID);
            int iSurv_Details_ID = int.Parse(Surv_Details_ID);

            if (Session["SaveFile"] == null)
            {
                if (UploadAttachments.FileName.Length > 0)
                {
                    string ParentFolderMapPath = Server.MapPath("~/Uploads/Survey/");
                    string FollowUpText = txtFollowUp.Text;

                    if (Vessel_ID != "" && Surv_Details_ID != "" && OfficeID != "")
                    {
                        Show_Attachments(iVessel_ID, iSurv_Details_ID, UploadAttachments.PostedFile.ContentLength);
                        Guid GUID = Guid.NewGuid();

                        string GUID_File = "SURV_" + "_" + GUID.ToString() + Path.GetExtension(UploadAttachments.FileName);


                        SIZE_BYTES = UploadAttachments.PostedFile.ContentLength;

                        if (UDFLib.ConvertToDecimal(SIZE_BYTES / Convert.ToDecimal(1024)) < UDFLib.ConvertToDecimal(hdnSizeAllowed.Value))
                        {
                            int FileID = objBLL.INSERT_New_Attachment(iVessel_ID, iSurv_Details_ID, UploadAttachments.FileName.Replace("&", "_"), GUID_File, SIZE_BYTES, GetSessionUserID(), Convert.ToInt32(SURVEY_MAX_ATTACHMENT_SIZE), Convert.ToInt32(ddlDocType.SelectedValue), txtIssueDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtIssueDate.Text).ToString(), txtAtchRemarks.Text);
                            string AttachmentName = "SURV_" + Vessel_ID + "_" + Surv_Details_ID + "_" + "O" + FileID.ToString() + "_" + UploadAttachments.FileName.Replace("&", "_").ToUpper();

                            if (FileID > 0)
                            {
                                if (!Directory.Exists(Server.MapPath("~/Uploads/Survey/")))
                                    Directory.CreateDirectory(Server.MapPath("~/Uploads/Survey/"));

                                UploadAttachments.PostedFile.SaveAs(Server.MapPath("~/Uploads/Survey/" + GUID_File));
                                Session["SaveFile"] = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorupload", "alert('Error in uploading the attachment');", true);
                            }
                            ClearField();
                        }
                        else
                        {
                            string js = "alert('Unable to upload attachment as the selected file size (" + (SIZE_BYTES / 1024).ToString("00.00") + " KB) exceeds the max file size !!');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorupload", js, true);
                        }

                        Show_Attachments(iVessel_ID, iSurv_Details_ID, SIZE_BYTES);
                    }
                }
                else
                {
                    OperationMode = "Add Attachment";
                    string js = String.Format("alert('Select file for attachment');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
                }
            }
            else
            {
                Show_Attachments(iVessel_ID, iSurv_Details_ID, SIZE_BYTES);
            }
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
        //long SIZE_KB = SIZE_BYTES / 1000;
        return SIZE_BYTES;
    }
    /// <summary>
    /// Save the survey details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CalculatedExpiryDate();
            string Vessel_ID = hdnV_ID.Value;
            string OfficeID = hdnO_ID.Value;
            string Surv_Vessel_ID = hdnS_V_ID.Value;
            string Surv_Details_ID = hdnS_D_ID.Value;

            if (Vessel_ID != "" && Surv_Vessel_ID != "" && Surv_Details_ID != "" && OfficeID != "" && Surv_Details_ID != "0" && OfficeID != "0")
            {
                int NoExpiry = 0;
                if (chkNoExpiry.Checked)
                    NoExpiry = -1;

                if (ValidateDetails())
                {
                    DataTable dt = objBLL.UPDATE_SurveyDetails(int.Parse(Vessel_ID), int.Parse(Surv_Vessel_ID), int.Parse(Surv_Details_ID), int.Parse(OfficeID), UDFLib.ConvertToDate(txtDateOfIssue.Text).ToString(), UDFLib.ConvertToDate(txtDateOfExpiry.Text).ToString(), txtRemarks.Text, txtReminderDate.Text == "" ? txtReminderDate.Text : UDFLib.ConvertToDate(txtReminderDate.Text).ToString(),
                        txtFollowupDetails.Text, GetSessionUserID(), NoExpiry, UDFLib.ConvertToInteger(txtGraceRange.Text), txtExtensionDate.Text == "" ? txtExtensionDate.Text : UDFLib.ConvertToDate(txtExtensionDate.Text.ToString()).ToString(), txtCertificateNo.Text.Trim(), Convert.ToInt32(ddlAuthority.SelectedValue), hdnCertInspection.Value != "" ? Convert.ToInt16(hdnCertInspection.Value) : 0, hdnRenewwalInspection.Value != "" ? Convert.ToInt16(hdnRenewwalInspection.Value) : 0);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hdnO_ID.Value = dt.Rows[0]["Office_ID"].ToString();
                        hdnS_D_ID.Value = dt.Rows[0]["Surv_Details_ID"].ToString();
                        hdnScheduleInspectionStatus.Value = dt.Rows[0]["CertificateInspectionStatus"].ToString();
                        hdnRenewwalInspectionStatus.Value = dt.Rows[0]["RenewalInspectionStatus"].ToString();
                    }
                    txtCertInspection.Text = hdnScheduleInspectionDate.Value;
                    txtRenewwalInspection.Text = hdnRenewwalInspectionDate.Value;
                    HighlightColor();
                    HighlightInspectionColor();
                    string js = "Footer();alert('Survey details updated');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "datasaved", js, true);
                    if (AdminAccessRights == 1)
                        ImgDeleteCertificate.Visible = true;
                    BindSurvayDetailList(int.Parse(Vessel_ID), int.Parse(Surv_Vessel_ID), int.Parse(Surv_Details_ID), 1);
                }
            }
            else if (Vessel_ID != "" && Surv_Vessel_ID != "")
            {
                int NoExpiry = 0;
                if (chkNoExpiry.Checked)
                    NoExpiry = -1;
                if (ValidateDetails())
                {
                    DataTable dt = objBLL.INSERT_SurveyDetails(int.Parse(Vessel_ID), int.Parse(Surv_Vessel_ID), UDFLib.ConvertToDate(txtDateOfIssue.Text).ToString(), UDFLib.ConvertToDate(txtDateOfExpiry.Text).ToString(), txtRemarks.Text, txtReminderDate.Text == "" ? txtReminderDate.Text : UDFLib.ConvertToDate(txtReminderDate.Text).ToString(), txtFollowupDetails.Text, GetSessionUserID(), NoExpiry,
                        UDFLib.ConvertToInteger(txtGraceRange.Text), txtExtensionDate.Text == "" ? txtExtensionDate.Text : UDFLib.ConvertToDate(txtExtensionDate.Text).ToString(), txtCertificateNo.Text.Trim(), Convert.ToInt32(ddlAuthority.SelectedValue), hdnCertInspection.Value != "" ? Convert.ToInt16(hdnCertInspection.Value) : 0, hdnRenewwalInspection.Value != "" ? Convert.ToInt16(hdnRenewwalInspection.Value) : 0);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hdnO_ID.Value = dt.Rows[0]["Office_ID"].ToString();
                        hdnS_D_ID.Value = dt.Rows[0]["Surv_Details_ID"].ToString();
                        hdnScheduleInspectionStatus.Value = dt.Rows[0]["CertificateInspectionStatus"].ToString();
                        hdnRenewwalInspectionStatus.Value = dt.Rows[0]["RenewalInspectionStatus"].ToString();
                        txtRenewwalInspection.Text = UDFLib.ConvertUserDateFormat(dt.Rows[0]["RenewalInspectionDate"].ToString());
                        txtCertInspection.Text = UDFLib.ConvertUserDateFormat(dt.Rows[0]["CertificateInspectionDate"].ToString());
                        hdnRenewwalInspectionDate.Value = txtRenewwalInspection.Text;
                        hdnScheduleInspectionDate.Value = txtCertInspection.Text;
                    }
                    btnAdd.Enabled = true;
                    ImgBtnAddFollowup.Enabled = true;
                    HighlightColor();
                    HighlightInspectionColor();
                    if (AdminAccessRights == 1)
                        ImgDeleteCertificate.Visible = true;
                    string js = "Footer();alert('Survey details saved');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "datasaved", js, true);
                    pnlVerify.Visible = true;
                    btnConfirmUnVerify.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "footer", "Footer();", true);
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Depending upon Verify Status , update the survey certificate status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfirmVerify_Click(object sender, EventArgs e)
    {
        try
        {
            string Vessel_ID = hdnV_ID.Value;
            string OfficeID = hdnO_ID.Value;
            string Surv_Vessel_ID = hdnS_V_ID.Value;
            string Surv_Details_ID = hdnS_D_ID.Value;

            if (Vessel_ID != "" && Surv_Details_ID != "" && OfficeID != "")
            {
                int iVessel_ID = int.Parse(Vessel_ID);
                int iSurv_Vessel_ID = int.Parse(Surv_Vessel_ID);
                int iSurv_Details_ID = int.Parse(Surv_Details_ID);
                int iOfficeID = int.Parse(OfficeID);
                int VerifyStatus = 0;

                if (((Button)sender).CommandArgument == "1")
                    VerifyStatus = 1;

                int result = objBLL.Verify_Survey(iVessel_ID, iSurv_Vessel_ID, iSurv_Details_ID, iOfficeID, VerifyStatus, GetSessionUserID());
                if (result == 1)
                {
                    DataTable dt = objBLL.Get_SurvayDetailList(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(Surv_Vessel_ID));

                    int curr = ctlRecordNavigation1.CurrentIndex;
                    ctlRecordNavigation1.InitRecords(dt);
                    if (GetQueryString("s_d_id") == "0" || GetQueryString("s_d_id") == "")
                        ctlRecordNavigation1.MoveToIndex(dt.Rows.Count - 1); //New certificate is added
                    else
                        ctlRecordNavigation1.MoveToIndex(curr);

                    hdnTotalRecords.Value = ctlRecordNavigation1.RecordCount.ToString();
                    hdnCurrentRecordNumber.Value = ctlRecordNavigation1.CurrentIndex.ToString();

                    string js = "";
                    if (VerifyStatus == 1)
                    {
                        js = "alert('Survey/Certificate is Verified');";
                        btnConfirmVerify.Visible = false;
                        btnConfirmUnVerify.Visible = true;
                    }
                    else
                    {
                        btnConfirmVerify.Visible = true;
                        btnConfirmUnVerify.Visible = false;
                        js = "alert('Survey/Certificate is Unverified');";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
                }
                else
                {
                    if (VerifyStatus == 1)
                    {
                        btnConfirmVerify.Visible = false;
                        btnConfirmUnVerify.Visible = true;
                    }
                    else
                    {
                        btnConfirmVerify.Visible = true;
                        btnConfirmUnVerify.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected Boolean ValidateDetails()
    {
        Boolean ret = true;
        string msg = "";


        if (txtDateOfIssue.Text == "")
        {
            ret = false;
            msg = "Please select DATE OF ISSUE";
        }
        else if (chkNoExpiry.Checked == false && txtDateOfExpiry.Text == "")
        {
            ret = false;
            msg = "Please enter DATE OF EXPIRY";
        }

        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        return ret;
    }
    /// <summary>
    /// Make the certificate as Not Active
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMakeAsNA_Click(object sender, EventArgs e)
    {
        try
        {
            string Vessel_ID = hdnV_ID.Value;
            string Surv_Vessel_ID = hdnS_V_ID.Value;
            int iVessel_ID = 0;
            int iSurv_Vessel_ID = 0;

            if (Vessel_ID != "")
                iVessel_ID = int.Parse(Vessel_ID);

            if (Surv_Vessel_ID != "")
                iSurv_Vessel_ID = int.Parse(Surv_Vessel_ID);

            objBLL.UPDATE_SurveyStatus(iVessel_ID, iSurv_Vessel_ID, 0, GetSessionUserID());

            btnSave.Enabled = false;
            btnMakeAsNA.Enabled = false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void HighlightInspectionColor()
    {
        if (hdnRenewwalInspectionStatus.Value != "" && hdnRenewwalInspectionStatus.Value == "Completed")
        {
            txtRenewwalInspection.BackColor = System.Drawing.Color.Green;
            txtRenewwalInspection.ForeColor = System.Drawing.Color.White;
        }
        else if (hdnRenewwalInspectionStatus.Value != "" && hdnRenewwalInspectionStatus.Value == "Pending" && txtRenewwalInspection.Text != "" && UDFLib.ConvertToDate(txtRenewwalInspection.Text) < DateTime.Today.AddDays(1))
        {
            txtRenewwalInspection.BackColor = System.Drawing.Color.Red;
            txtRenewwalInspection.ForeColor = System.Drawing.Color.Yellow;
        }
        else
        {
            txtRenewwalInspection.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            txtRenewwalInspection.ForeColor = System.Drawing.Color.Black;
        }
        if (hdnScheduleInspectionStatus.Value != "" && hdnScheduleInspectionStatus.Value == "Completed")
        {
            txtCertInspection.BackColor = System.Drawing.Color.Green;
            txtCertInspection.ForeColor = System.Drawing.Color.White;
        }
        else if (hdnScheduleInspectionStatus.Value != "" && hdnScheduleInspectionStatus.Value == "Pending" && txtCertInspection.Text != "" && UDFLib.ConvertToDate(txtCertInspection.Text) < DateTime.Today.AddDays(1))
        {
            txtCertInspection.BackColor = System.Drawing.Color.Red;
            txtCertInspection.ForeColor = System.Drawing.Color.Yellow;
        }
        else
        {
            txtCertInspection.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            txtCertInspection.ForeColor = System.Drawing.Color.Black;
        }
    }
    protected void HighlightColor()
    {
        if (txtDateOfIssue.Text != "")
        {
            DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDate(txtDateOfIssue.Text).ToString());
            if (dt1 > DateTime.Today.AddDays(-30))
            {
                txtDateOfIssue.CssClass = "Done30";
            }
            else
            {
                txtDateOfIssue.CssClass = "required";
            }
        }
        else
        {
            txtDateOfIssue.Text = "";
            txtDateOfIssue.CssClass = "required";
        }

        if (txtCalculatedExpiryDate.Text != "")
        {
            DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDate(txtCalculatedExpiryDate.Text).ToString());
            if (dt1 < DateTime.Today.AddDays(1))
            {
                txtCalculatedExpiryDate.CssClass = "Overdue";
            }
            else if (dt1 < DateTime.Today.AddDays(90) && dt1 > DateTime.Today.AddDays(30))
            {
                txtCalculatedExpiryDate.CssClass = "Due30-90";
            }
            else if (dt1 <= DateTime.Today.AddDays(30))
            {
                txtCalculatedExpiryDate.CssClass = "Due0-30";
            }
            else
            {
                txtCalculatedExpiryDate.CssClass = "";
            }
        }
        else
        {
            txtCalculatedExpiryDate.CssClass = "";
        }
        if (chkNoExpiry.Checked == true)
        {
            txtDateOfExpiry.Text = "";
            txtDateOfExpiry.CssClass = "disabled";
        }
        if (txtReminderDate.Text != "")
        {
            DateTime dtReminderDate = DateTime.Parse(UDFLib.ConvertToDate(txtReminderDate.Text).ToString());

            //Reminder Date Color
            if (dtReminderDate < DateTime.Today.AddDays(1))
                txtReminderDate.CssClass = "Overdue";
            else if (dtReminderDate < DateTime.Today.AddDays(90) && dtReminderDate > DateTime.Today.AddDays(30))
                txtReminderDate.CssClass = "Due30-90";
            else if (dtReminderDate < DateTime.Today.AddDays(30))
                txtReminderDate.CssClass = "Due0-30";
        }
        else
        {
            txtReminderDate.CssClass = "";
        }
    }
    protected void ImgBtnEditRemarks_Click(object sender, EventArgs e)
    {
        ImgBtnEditRemarks.Visible = false;
        ImgBtnUpdateRemarks.Visible = true;
        ImgBtnCancelRemarks.Visible = true;

        txtCertificateRemarks.Enabled = true;
        txtTerm.Enabled = true;
        txtMakeModel.Enabled = true;
        txtIssuingAuthority.Enabled = true;

        ViewState["SurveyRemarks"] = txtCertificateRemarks.Text.Trim();
        ViewState["Term"] = txtTerm.Text.Trim();
        ViewState["MakeModel"] = txtMakeModel.Text.Trim();
        ViewState["IssuingAuthority"] = txtIssuingAuthority.Text.Trim();
    }
    protected void ImgBtnUpdateRemarks_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnV_ID.Value);
        int S_V_ID = UDFLib.ConvertToInteger(hdnS_V_ID.Value);
        int S_D_ID = UDFLib.ConvertToInteger(hdnS_D_ID.Value);

        string Survey_Cert_remarks = txtCertificateRemarks.Text.Trim();

        string MakeModel = txtMakeModel.Text;
        string IssuingAuthority = txtIssuingAuthority.Text;

        int Term = 0;

        if (txtTerm.Text != "" && int.TryParse(txtTerm.Text, out Term) == false)
        {
            string js = "alert('Enter numeric value in Term field');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorupdate", js, true);
        }
        else
        {
            Term = UDFLib.ConvertToInteger(txtTerm.Text);
            objBLL.UPDATE_Survey_CertificateRemarks(Vessel_ID, S_V_ID, S_D_ID, GetSessionUserID(), Survey_Cert_remarks, UDFLib.ConvertIntegerToNull(Term), MakeModel, IssuingAuthority);
            ImgBtnEditRemarks.Visible = true;
            ImgBtnUpdateRemarks.Visible = false;
            ImgBtnCancelRemarks.Visible = false;

            txtCertificateRemarks.Enabled = false;
            txtTerm.Enabled = false;
            txtMakeModel.Enabled = false;
            txtIssuingAuthority.Enabled = false;

        }
    }
    protected void ImgBtnCancelRemarks_Click(object sender, EventArgs e)
    {
        txtCertificateRemarks.Enabled = false;
        txtTerm.Enabled = false;
        txtMakeModel.Enabled = false;
        txtIssuingAuthority.Enabled = false;

        txtCertificateRemarks.Text = ViewState["SurveyRemarks"].ToString();
        txtTerm.Text = ViewState["Term"].ToString();
        txtMakeModel.Text = ViewState["MakeModel"].ToString();
        txtIssuingAuthority.Text = ViewState["IssuingAuthority"].ToString();

        ImgBtnEditRemarks.Visible = true;
        ImgBtnUpdateRemarks.Visible = false;
        ImgBtnCancelRemarks.Visible = false;
    }
    protected void CalculatedExpiryDate(object sender, EventArgs e)
    {
        CalculatedExpiryDate();
    }
    protected void CalculatedExpiryDate()
    {
        try
        {
            if (txtDateOfExpiry.Text != "")
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDateOfExpiry.Text));
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "date1", "alert('Enter valid Date of Expiry" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }

            if (txtReminderDate.Text != "")
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtReminderDate.Text));
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "date5", "alert('Enter valid Followup Date" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }
            if (txtExtensionDate.Text != "")
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtExtensionDate.Text));
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "date2", "alert('Enter valid Extension Date" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }
            string DateOfExpiry = txtDateOfExpiry.Text.Trim();
            string ExtensionDate = txtExtensionDate.Text.Trim();
            int GraceRange = UDFLib.ConvertToInteger(txtGraceRange.Text);
            string RangeMax = "", MaxDate = "";
            if (DateOfExpiry != "")
            {
                MaxDate = DateOfExpiry;
                RangeMax = UDFLib.ConvertToDate(DateOfExpiry).AddDays((GraceRange * 30 / 2)).ToString(DateFormat);
                if (GraceRange > 0 && RangeMax != "")
                {
                    MaxDate = UDFLib.ConvertToDate(MaxDate) > UDFLib.ConvertToDate(RangeMax) ? MaxDate : RangeMax;
                }
                if (ExtensionDate != "")
                {
                    MaxDate = UDFLib.ConvertToDate(MaxDate) > UDFLib.ConvertToDate(ExtensionDate) ? MaxDate : ExtensionDate;
                }
            }
            txtCalculatedExpiryDate.Text = MaxDate;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void Load_DocTypeList()
    {
        ddlDocType.Items.Clear();
        ddlDocType.DataSource = objBLL.Get_Survey_Document_Type_List();
        ddlDocType.DataTextField = "DocumentType";
        ddlDocType.DataValueField = "ID";
        ddlDocType.DataBind();
        ddlDocType.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlDocType.SelectedIndex = 0;
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Session["SaveFile"] = null;
        this.SetFocus("ctl00_MainContent_ddlDocType");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Attachment";
        ViewState["OperationMode"] = "Add Attachment";
        ClearField();
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    public void Load_IssuingAuthority()
    {
        ddlAuthority.DataSource = objBLL.Get_Authorit();
        ddlAuthority.DataTextField = "Authority";
        ddlAuthority.DataValueField = "ID";
        ddlAuthority.DataBind();
        ddlAuthority.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlAuthority.SelectedIndex = 0;
    }

    /// <summary>
    /// Delete the attachment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgDeleteFile_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            int Vessel_ID = Convert.ToInt32(arg[0]);
            int Surv_Details_ID = Convert.ToInt32(arg[1]);
            int SurvVessel_Att_ID = Convert.ToInt32(arg[2]);
            objBLL.DELETE_Survey_CertificateAttachment(Vessel_ID, Surv_Details_ID, SurvVessel_Att_ID, UDFLib.ConvertToInteger(Session["userid"].ToString()));
            Show_Attachments(Convert.ToInt32(Vessel_ID), Convert.ToInt32(Surv_Details_ID), SIZE_BYTES);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ClearField()
    {
        ddlDocType.SelectedIndex = 0;
        txtIssueDate.Text = "";
        txtAtchRemarks.Text = "";
    }

    /// <summary>
    /// Delete the unverified certificate
    /// </summary>
    /// <param name="Surv_Details_ID1"></param>
    /// <param name="Surv_Vessel_ID1"></param>
    /// <param name="Vessel_ID1"></param>
    /// <param name="OfficeID1"></param>
    protected void DeleteCertificate(int Surv_Details_ID1, int Surv_Vessel_ID1, int Vessel_ID1, int OfficeID1, int QueryStringOfficeId)
    {
        try
        {
            int Res = objBLL.DELETE_Survey_Certificate(Surv_Details_ID1, Surv_Vessel_ID1, Vessel_ID1, OfficeID1, UDFLib.ConvertToInteger(Session["userid"].ToString()));
            int Surv_Details_ID = 0;
            int Office_ID = 0;
            if (QueryStringOfficeId > 0)
            {
                DataTable dt = objBLL.Get_SurvayDetailList(Vessel_ID1, Surv_Vessel_ID1);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    Surv_Details_ID = int.Parse(dr["Surv_Details_ID"].ToString());
                    Office_ID = int.Parse(dr["OfficeID"].ToString());
                }
            }
            Response.Clear();
            Response.Write(Surv_Details_ID.ToString() + '|' + Office_ID.ToString());
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}