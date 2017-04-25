/*EDITED BY :Pranali_28072016_jit_10586: Send Copy to Vessel was not unchecked issue.*/

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
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Text;
using ClsBLLTechnical;
using System.IO;
using SMS.Business.Crew;
using AjaxControlToolkit4;
using SMS.Business.Infrastructure;

public partial class Technical_INV_RequisitionSummary : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    public int sizeupload;
    public string FormType = "";
    public string ReqsnCode = "";
    public string DocCode = "";
    public int VESSEL_ID;
    public int OFFICE_ID;
    TechnicalBAL objtechBAL = new TechnicalBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        AjaxFileUpload1.MaximumSizeOfFile = sizeupload;
        //AjaxFileUpload1.MaximumSizeOfFile = Int32.Parse(dt.Rows[0]["Size_KB"].ToString());
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            //UserAccessValidation();
            btnLoadFiles.Attributes.Add("style", "visibility:hidden");

            txtDocumentCode.Text = Request.QueryString["document_code"].ToString();
            txtReqCode.Text = Request.QueryString["REQUISITION_CODE"].ToString();
            txtVessselCode.Text = Request.QueryString["Vessel_Code"].ToString();
            if (BLL_PURC_Common.Get_Reqsn_IsValidToClose(Request.QueryString["REQUISITION_CODE"].ToString()) > 0)
                btnCloseRequisition.Enabled = true;

            if (IsPostBack)
            {

                GridViewHelper helper = new GridViewHelper(this.gvReqsnItems);
                helper.RegisterGroup("Subsystem_Description", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);

                //ucPurcAttachment1.Register_JS_Attach();
                Session["PURCATTACHEDFILES"] = ASP.global_asax.AttachedFile;


            }
            if (!IsPostBack)
            {
                lbtnAllremarks.Attributes.Add("onmouseover", "javascript:GetRemarkToolTip('" + Request.QueryString["Document_Code"] + "','1',event);return false;");
                lbtnAllremarks.Attributes.Add("onmouseout", "javascript:CloseRemarkToolTip();return false;");
                lbtnAllremarks.Attributes.Add("onclick", "javascript:GetRemarkAll('" + Request.QueryString["Document_Code"] + "','" + GetSessionUserID() + "','1',event);return false;");

                Session["PURCATTACHEDFILES"] = "";
                ASP.global_asax.AttachedFile = "";

                GridViewHelper helper = new GridViewHelper(this.gvReqsnItems);
                helper.RegisterGroup("Subsystem_Description", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);


                Session["REQSN_DETAILS_REQUISITION_CODE"] = Request.QueryString["REQUISITION_CODE"].ToString();
                Session["REQSN_DETAILS_VESSEL_CODE"] = Request.QueryString["Vessel_Code"].ToString();
                BindRequisitionSummary();
                TechnicalBAL objpurch = new TechnicalBAL();
                int sts = objpurch.Get_SendToVessel(Request.QueryString["document_code"].ToString(), Request.QueryString["REQUISITION_CODE"].ToString());
                if (sts == 1 || !Request.QueryString["REQUISITION_CODE"].Contains("-O"))
                {
                    chkSendTovessel.Enabled = false;
                    btnSendToVessel.Enabled = false;

                }

                ucPurcCancelReqsnNew.ReqsnNumber = Request.QueryString["REQUISITION_CODE"];
                ucPurcCancelReqsnNew.DocCode = Request.QueryString["document_code"];
                ucPurcCancelReqsnNew.VesselCode = Request.QueryString["Vessel_Code"];
                BindReqsTypeLog();
                //rptCrew.DataSource = BLL_PURC_Common.GET_REQSN_VMT(Request.QueryString["document_code"]);
                //rptCrew.DataBind();

                BindPurcQuestion();
                LoadWorkListInvolved();
            }

            if (Request.QueryString["hold"] != null) // this hold will come from new and sent rfq/status only ,means if it is null then it has been sent for approval
            {
                string holdSTS = Request.QueryString["hold"];
                if (holdSTS.ToLower() == "false")
                {
                    btnSendRFQ.Enabled = true;
                    btnCancel.Enabled = true;


                }
            }

            if (Convert.ToString(ViewState["OnHold"]) == "0")
            {
                btnHold.Text = "Hold Requisition";
            }
            else
            {
                btnHold.Text = "UnHold Requisition";
            }


            dvCancelReq.Visible = false;
            divOnHold.Visible = false;

            dlistPONumber.DataSource = BLL_PURC_Common.Get_PONumbers(Request.QueryString["REQUISITION_CODE"].ToString());
            dlistPONumber.DataBind();
        }
        DocCode = Convert.ToString(Request.QueryString["document_code"]);
        VESSEL_ID = Convert.ToInt32(Request.QueryString["Vessel_Code"]);
        OFFICE_ID = 0;
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        //if (objUA.Add == 0)
        //{
        //    ucPurcAttachment1.Visible = false;

        //}
        if (objUA.Edit == 0)
        {
            btnSendRFQ.Visible = false;
            btnHold.Visible = false;
            btnBulkPurchase.Visible = false;
            btnHold.Visible = false;
            btnAccountTypeSave.Visible = false;
            btnSendToVessel.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            btnCancel.Visible = false;

        }


    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Subsystem_Description")
        {
            row.BackColor = System.Drawing.Color.LightGray;

            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;

        }

    }
    protected void btnAccountTypeSave_Click(object s, EventArgs e)
    {
        try
        {
            TechnicalBAL objtechBAL = new TechnicalBAL();
            objtechBAL.Update_AccountType(Request.QueryString["REQUISITION_CODE"].ToString(), ddlAccountType.SelectedValue, txtTypeRemark.Text, UDFLib.ConvertToInteger(GetSessionUserID()));
            BindRequisitionSummary();
            //BindReqsTypeLog();
            txtTypeRemark.Text = string.Empty;
            string hidemodal = String.Format("hideModal('dvAccountType')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void BindRequisitionSummary()
    {
        TechnicalBAL objtechBAL = new TechnicalBAL();
        DataSet dsReqSumm = new DataSet();

        try
        {
            dsReqSumm = objtechBAL.GetRequisitionSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString());
            if (dsReqSumm.Tables[0].Rows.Count > 0)
            {
                lblReqsntype.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ReqsnType"]);
                txtCatalogueCode.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["System_Code"]);
                lblCatalog.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Catalog"]);
                lblReqNo.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["RequistionCode"]);
                //lblTotalItem.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["TotalItems"]);
                lblToDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ToDate"]);
                lblFunction.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["DeptName"]);
                lblReason.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ReqComents"]);
                lblDeliveryPort.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["DeliveryPort"]);
                lblDeliveryDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["DELIVERY_DATE"]);
                btnAccountType.Text = "Account Type :" + " " + Convert.ToString(dsReqSumm.Tables[0].Rows[0]["VARIABLE_NAME"]);
                ddlAccountType.SelectedValue = dsReqSumm.Tables[0].Rows[0]["Account_Type"].ToString();
                chkSendTovessel.Checked = Convert.ToBoolean(dsReqSumm.Tables[0].Rows[0]["Send_To_Vessel"]);

                ViewState["OnHold"] = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["OnHold"]);

                gvReqsnItems.DataSource = dsReqSumm.Tables[1];
                gvReqsnItems.DataBind();
                GridViewHelper helper = new GridViewHelper(this.gvReqsnItems);
                helper.RegisterGroup("Subsystem_Description", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                var Listselected = dsReqSumm.Tables[1].AsEnumerable().Select(r => r.Field<string>("ItemID")).ToArray();
                hdnSelectedCde.Value = string.Join(",", Listselected);


                rpAttachment.DataSource = dsReqSumm.Tables[2];
                rpAttachment.DataBind();
            }
            else
            {
                String msg = String.Format("alert('This Requisition is not valid !'); window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg156", msg, true);
            }
            if (UDFLib.ConvertToInteger(dsReqSumm.Tables[3].Rows[0][0].ToString()) > 0 && BLL_PURC_Common.Get_Bulk_Reqsn_Finalized(Request.QueryString["REQUISITION_CODE"]) > 0)
            {

                btnBulkPurchase.Enabled = true;
            }

            else
            {
                btnBulkPurchase.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            BindDataOnPostback();
            ResponseHelper.Redirect("SummaryReportsShow.aspx?REQUISITION_CODE=" + Request.QueryString["REQUISITION_CODE"].ToString() + "&document_code=" + Request.QueryString["document_code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&RptType=ReqSumry", "Blank", "");
            ////ResponseHelper.Redirect("SummaryReportsShow.aspx?REQUISITION_CODE=" + Request.QueryString["REQUISITION_CODE"].ToString() + "&document_code=" + Request.QueryString["document_code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&RptType=QtnSumry" + "&QUOTATION_CODE=" + Request.QueryString["QUOTATION_CODE"].ToString(), "Blank", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string GetSessionUserName()
    {
        if (Session["USERNAME"] != null)
            return Session["USERNAME"].ToString();
        else
            return "0";
    }
    protected void BindReqsTypeLog()
    {
        //gvReqsnTypeLog.DataSource = BLL_PURC_Common.Get_ReqsnType_Log(Request.QueryString["REQUISITION_CODE"].ToString());
        //gvReqsnTypeLog.DataBind();
        //DataTable dtAccountType = BLL_PURC_Common.PURC_Get_Sys_Variable(UDFLib.ConvertToInteger(GetSessionUserID()), "Account_Type");
        //ddlAccountType.DataSource = dtAccountType;
        //ddlAccountType.DataTextField = "VARIABLE_NAME";
        //ddlAccountType.DataValueField = "VARIABLE_CODE";
        //ddlAccountType.DataBind();
        //ddlAccountType.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
    protected void btnSendRFQ_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataOnPostback();
            ResponseHelper.Redirect("SelectSuppliers.aspx?Requisitioncode=" + Request.QueryString.ToString().Replace("REQUISITION_CODE=", ""), "blank", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvReqsnItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((HyperLink)(e.Row.FindControl("lblItemDesc"))).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + "Short Description: " + ((Label)e.Row.FindControl("lblshortDesc")).Text + "] body=[" + "Long Description: " + ((Label)e.Row.FindControl("lblLongDesc")).Text + "<hr>" + "Comment: " + ((Label)e.Row.FindControl("lblComments")).Text + "]");

            if (((Label)e.Row.FindControl("lblshortDesc")).ToolTip != "0")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }

            if (DataBinder.Eval(e.Row.DataItem, "Critical_Flag").ToString() == "1")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Wheat;
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataOnPostback();
            dvCancelReq.Visible = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #region Requisition Hold Button click
    /// <summary>
    /// Hold Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnHold_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataOnPostback();
            divOnHold.Visible = true;
            BLL_PURC_Purchase objhold = new BLL_PURC_Purchase();
            HoldUnHold.DTLog = objhold.GetRequisitionOnHoldLogHistory_ByReqsn(Request.QueryString["REQUISITION_CODE"]);
            HoldUnHold.BindLog();
            if (btnHold.Text == "UnHold")
            {
                btnSendRFQ.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                btnSendRFQ.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
    #region Hold Popup save button
    /// <summary>
    /// On Hold Requisition => Save Button Functionality.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btndivSave_Click(object sender, EventArgs e)
    {
        string sOnHold = ViewState["OnHold"].ToString() == "0" ? "false" : "true";
        if (HoldUnHold.Remarks != "")
        {
            string sRequisitionCode = Request.QueryString["REQUISITION_CODE"];
            string sDocumentCode = Request.QueryString["document_code"];
            string sVessel_Code = Request.QueryString["Vessel_Code"];
            string sLinkType = "";

            string sOnHoldName = "";
            string sRemarks = HoldUnHold.Remarks;
            string sSupplierID = "";
            if (sOnHold.ToString().ToLower() == "false")
            {
                sOnHold = "1";
            }
            else
            {
                sOnHold = "0";
            }
            if (sOnHold == "1")
                sOnHoldName = "hold";
            else
                sOnHoldName = "un hold";

            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    int iReturn = objTechService.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVessel_Code, sDocumentCode, sLinkType, sOnHold, sRemarks, sSupplierID, Convert.ToInt32(GetSessionUserID()));
                    if (iReturn == 1)
                    {
                        DataTable dtQuotationList = new DataTable();
                        dtQuotationList.Columns.Add("Qtncode");
                        dtQuotationList.Columns.Add("amount");

                        ViewState["OnHold"] = sOnHold;
                        objTechService.InsertRequisitionStageStatus(sRequisitionCode, sVessel_Code, sDocumentCode, sOnHold, sRemarks, Convert.ToInt32(Session["USERID"]), dtQuotationList);
                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(GetSessionUserID()), sRemarks, 306);
                        divOnHold.Visible = false;
                        if (ViewState["OnHold"].ToString() == "0")
                        {
                            btnHold.Text = "Hold Requisition";
                            btnSendRFQ.Enabled = true;
                            btnCancel.Enabled = true;
                        }
                        else
                        {
                            btnHold.Text = "UnHold Requisition";
                            btnSendRFQ.Enabled = false;
                            btnCancel.Enabled = false;
                        }

                        String msg = String.Format("alert('Requisition RFQ has been marked on " + sOnHoldName + "'); ");
                        //String msg = String.Format("alert('Requisition UPQ has been marked on " + sOnHoldName + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        HoldUnHold.Remarks = string.Empty;


                    }


                }
            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
        }
        else
        {
            String msg = String.Format("alert('Remark is mandatory field.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }
    #endregion
    #region Hold Popup Cancel Click
    /// <summary>
    ///  On Hold Requisition=>Cancel Button functionality.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        try
        {
            HoldUnHold.Remarks = string.Empty;
            if (btnHold.Text == "UnHold")
            {
                btnSendRFQ.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                btnSendRFQ.Enabled = true;
                btnCancel.Enabled = true;
            }
            divOnHold.Visible = false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        BindDataOnPostback();
        try
        {

            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();

            int ID = int.Parse(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[0]);
            int res = objAttch.Purc_Delete_Reqsn_Attachments(ID);
            if (res > 0)
            {
                File.Delete(Server.MapPath(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[1]));
            }

            LoadFiles(null, null);
            DataSet dsReqSumm = new DataSet();
            TechnicalBAL objtechBAL = new TechnicalBAL();
            dsReqSumm = objtechBAL.GetRequisitionSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString());
            rpAttachment.DataSource = dsReqSumm.Tables[2];
            rpAttachment.DataBind();
        }
        catch
        { }
    }

    public void LoadFiles(object s, EventArgs e)
    {
        BindDataOnPostback();
        try
        {
            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();
            DataTable dtAttachedFile = objAttch.Purc_Get_Reqsn_Attachments(Request.QueryString["REQUISITION_CODE"].ToString(), int.Parse(Request.QueryString["Vessel_Code"].ToString()));
            //dtAttachedFile.DefaultView.RowFilter = "id in (" + Session["PURCATTACHEDFILES"].ToString().Remove(Session["PURCATTACHEDFILES"].ToString().Length - 1, 1) + ")";
            rpAttachment.DataSource = dtAttachedFile;
            rpAttachment.DataBind();
        }
        catch { }

    }

    protected void btnBulkPurchase_Click(object s, EventArgs e)
    {
        BindDataOnPostback();
        ResponseHelper.Redirect("PURC_Split_Reqsn_BulkPurchase.aspx?ReqsnCode=" + Request.QueryString["REQUISITION_CODE"] + "&Document_Code=" + Request.QueryString["document_code"], "blank", "");

    }

    protected void imgbtnExportReqsnItems_Click(object s, EventArgs e)
    {
        BindDataOnPostback();
        TechnicalBAL objtechBAL = new TechnicalBAL();
        DataSet dsReqSumm = new DataSet();

        dsReqSumm = objtechBAL.GetRequisitionSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString());

        DataTable dtexportdata = dsReqSumm.Tables[1];

        string[] HeaderCaptions = new string[] { "Sr No", "Sub Catalogue", "Drawing Number", "Part Number", " Short Description", "Long Description", "Unit", "Requested Qty" };
        string[] DataColumnsName = new string[] { "SrNo", "Subsystem_Description", "Drawing_Number", "Part_Number", "ItemDesc", "Long_Description", "ItemUnit", "ReqestedQty" };
        string FileHeaderName = "Items of requisition number : " + Request.QueryString["REQUISITION_CODE"];
        string FileName = Request.QueryString["REQUISITION_CODE"];
        GridViewExportUtil.ShowExcel(dtexportdata, HeaderCaptions, DataColumnsName, FileName, FileHeaderName);
    }

    protected void btnCloseRequisition_Click(object s, EventArgs e)
    {
        BindDataOnPostback();
        BLL_PURC_Common.Upd_Close_Requisition(Request.QueryString["REQUISITION_CODE"].ToString(), Convert.ToInt32(Session["userid"]));

        String msg = String.Format("alert('Requisition has been closed.');RefreshPendingDetails();window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Purchase");
            Guid GUID = Guid.NewGuid();
            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int sts = objTechService.SaveAttachedFileInfo(Convert.ToString(Request.QueryString["Vessel_Code"]), Convert.ToString(Request.QueryString["REQUISITION_CODE"]), "0", Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), "../Uploads/Purchase/" + Flag_Attach, Convert.ToString(GetSessionUserID()), 0);

            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
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
        BindDataOnPostback();
        LoadFiles(null, null);
    }

    #region Purchase Question and Answers
    private void BindPurcQuestion()
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        try
        {
            string FormType = GetFormType();
            DataSet dt = objTechService.Get_Purc_Questions(Request.QueryString["document_code"], FormType);
            if (dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Columns.Contains("ID"))
                {
                    grdQuestion.DataSource = dt;
                    grdQuestion.DataBind();
                }
                else
                {
                    grdQuestion.DataSource = null;
                    grdQuestion.DataBind();
                }
            }

        }
        catch
        {
        }
    }
    private string GetFormType()
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet ds = objTechService.GET_PURC_DEP_ON_DOCCODE(Request.QueryString["document_code"]);
        string DeptType = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);//Convert.ToString(Request.QueryString["DocumentCode"]).Split('-')[1];
        DataTable dtDept = objTechService.SelectDepartment();
        var q = from a in dtDept.AsEnumerable()
                where a.Field<string>("code") == DeptType
                select new { Form_Type = a.Field<string>("Form_Type") };
        return q.Select(a => a.Form_Type).Single();

    }

    #endregion

    #region
    private void LoadWorkListInvolved()
    {
        BindDataOnPostback();
        int s = 0;
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataTable dtWorklstInvolved = objBLLPurc.Get_Purc_Worklist(Convert.ToInt32(Request.QueryString["OFFID"]), Convert.ToInt32(Request.QueryString["Vessel_Code"]), Convert.ToString(Request.QueryString["document_code"]));

        grdWorklistInvolved.DataSource = dtWorklstInvolved;
        grdWorklistInvolved.DataBind();


    }
    #endregion


    protected void btnSaveSupplierSetting_Click(object sender, EventArgs e)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        string vDocCode = Convert.ToString(Request.QueryString["document_code"]);
        string vReqCode = Convert.ToString(Request.QueryString["REQUISITION_CODE"]);
        string[] CheckArray = hdnSelectedCde.Value.Split(',');
        string[] UnCheckArray = hdnNotselect.Value.Split(',');
        string[] CheckArrayList = CheckArray.Distinct().ToArray();
        string[] UnCheckArrayList = UnCheckArray.Distinct().ToArray();

        foreach (string check in CheckArrayList)
        {
            if (check != "")
            {
                objBLLPurc.Insert_Supplier_remarks_settings(check, vReqCode, vDocCode, 1, Convert.ToInt32(Session["USERID"]));
            }
        }
        foreach (string Uncheck in UnCheckArrayList)
        {
            if (Uncheck != "")
            {
                objBLLPurc.Insert_Supplier_remarks_settings(Uncheck, vReqCode, vDocCode, 0, Convert.ToInt32(Session["USERID"]));
            }
        }
        //foreach (GridViewRow row in gvReqsnItems.Rows)
        //{
        //    int index = row.RowIndex;
        //    CheckBox chkb1 = (CheckBox)gvReqsnItems.Rows[index].FindControl("chkSupplier");
        //    string ItemRefCode = ((Label)gvReqsnItems.Rows[index].FindControl("hdnItemIDd")).Text;
        //    objBLLPurc.Insert_Supplier_remarks_settings(ItemRefCode, vReqCode, vDocCode, Convert.ToInt32(chkb1.Checked == true ? 1 : 0), Convert.ToInt32(Session["USERID"]));

        //}
        BindPurcQuestion();
        BindDataOnPostback();
        GridViewHelper helper = new GridViewHelper(this.gvReqsnItems);
        helper.RegisterGroup("Subsystem_Description", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        hdnSelectedCde.Value = "";
        hdnNotselect.Value = "";

    }
    private void BindDataOnPostback()
    {
        DataSet dsReqSumm = objtechBAL.GetRequisitionSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString());
        rpAttachment.DataSource = dsReqSumm.Tables[2];
        rpAttachment.DataBind();
        gvReqsnItems.DataSource = dsReqSumm.Tables[1];
        gvReqsnItems.DataBind();


    }
    protected void chkSendTovessel_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            SendTovessel();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSendToVessel_Click(object sender, EventArgs e)
    {
        try
        {
            SendTovessel();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void SendTovessel()
    {

        if (chkSendTovessel.Checked == true)
        {
            TechnicalBAL objpurch = new TechnicalBAL();

            objpurch.Insert_sendToVessel(Request.QueryString["document_code"].ToString(), Request.QueryString["REQUISITION_CODE"].ToString(), 1);
            btnSendToVessel.Enabled = false;
            chkSendTovessel.Enabled = false;

            int sts = objpurch.Get_SendToVessel(Request.QueryString["document_code"].ToString(), Request.QueryString["REQUISITION_CODE"].ToString());
            if (sts == 1 || !Request.QueryString["REQUISITION_CODE"].Contains("-O"))
            {
                chkSendTovessel.Enabled = false;
                btnSendToVessel.Enabled = false;

            }

            String msg = String.Format("alert('Requisition has been sent to vessel'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgsentreq", msg, true);
        }
    }
}
