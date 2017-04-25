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
using BLLQuotation;
using Telerik.Web.UI;
using System.Text;
using System.Drawing;
using SMS.Business.Infrastructure;
using System.Reflection;
using System.IO;
using Exel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using System.Diagnostics;

public partial class WebQuotation_QoutationEntry : System.Web.UI.Page
{
    public string PageKeyValue = "QUOTENRY";
    private decimal Exchange_rate = 1;
    public int iCountRows = 0;
    public string strPath = ConfigurationManager.AppSettings["INVFolderPath"].ToString();
    public static int RemarkRowIndex = 0;
    public Control Cursor = new Control();
    public DataTable dtItemsTypes = new DataTable();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    clsQuotationBLL objQuoBLL = new clsQuotationBLL();

    protected void Page_Init(object source, System.EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dvitemtype.Visible = false;

        if (Convert.ToString(Session["SuppCode"]) == "")
            FormsAuthentication.RedirectToLoginPage();

        ctlPortList1.Web_Method_URL = "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/JibeWebService.asmx/asyncGet_Port_List";

        if (!Page.IsPostBack)
        {
            txtTotalDiscount.Attributes.Add("onKeydown", "return MaskMoney(event)");

            txtVat.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtExchangeRate.Attributes.Add("onKeydown", "return MaskMoney(event)");

            BLL_PURC_Purchase obj = new BLL_PURC_Purchase();
            obj.Update_Progress(Request.QueryString["QUOTATION_CODE"]);

            BindCurrency();

            BindQuotationItems();
            BindRequisitionSummary();

            BindAttachmentList(); // Bind the Attachment List for particular requisition for a Supplier
            divLeadTime.Visible = false;
            txtReqCode.Text = Request.QueryString["Requisitioncode"].ToString();
            txtVessselCode.Text = Request.QueryString["Vessel_Code"].ToString();


            clsQuotationBLL objqtn = new clsQuotationBLL();
            lblLegal.Text = objqtn.Get_LegalTerm(282);
  
        }
        // UserAccessValidation();
        String msg = String.Format("CalculateTotal();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgcal", msg, true);

    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {
            btnSave.Visible = false;
            btnSubmit.Visible = false;
            btnRFQUpload.Visible = false;
            btnDeclinQuote.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }
    }


    protected void BindAttachmentList()
    {
        string strPath = ConfigurationManager.AppSettings["INVFolderPath"].ToString();
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        DataSet ds = objQuoBLL.GetRFQAttachment(Request.QueryString["Requisitioncode"].ToString(), Session["SuppCode"].ToString(), Convert.ToInt32(Request.QueryString["Vessel_Code"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpAttachment.DataSource = ds.Tables[0];
            rpAttachment.DataBind();

        }
    }

    protected string GetQuotationStatus()
    {
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        return objQuoBLL.GetQuotStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["SuppCode"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());
    }

    protected void BindQuotationItems()
    {
        try
        {
            clsQuotationBLL objQuoBLL = new clsQuotationBLL();
            //System.Drawing.Color.PaleVioletRed

            //Get the Quotation status of the requistion.
            string strQuotStatus = objQuoBLL.GetQuotStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["SuppCode"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());

            DataSet dsQuot = new DataSet();
            dsQuot = objQuoBLL.GetDataToGenerateRFQ(Session["SuppCode"].ToString(), Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());

            //Get Mech_Particulars and Marker Details

            DataSet DsMech = new DataSet();
            DsMech = objQuoBLL.GetMechDetails(Request.QueryString["Requisitioncode"].ToString());

            DataTable dtVesselDtl = BLL_PURC_Purchase.Get_VID_VesselDetails(UDFLib.ConvertToInteger(Request.QueryString["Vessel_Code"].ToString()));

            DataTable dtContractSts = BLL_PURC_Purchase.Get_Contract_Status(Request.QueryString["QUOTATION_CODE"].ToString());
            if (dtContractSts.Rows.Count > 0)
            {
                if (dtContractSts.Rows[0]["CONTRACT_STS"].ToString() == "0")
                {
                    hylContractSts.Text = " Contract code : " + dtContractSts.Rows[0]["QTN_Contract_Code"].ToString() + " has expired !";
                    hylContractSts.NavigateUrl = "~/purchase/ctp_contract_details.aspx?Quotation_ID=" + dtContractSts.Rows[0]["Quotation_ID"].ToString() + "&supplier_code=" + Session["SuppCode"].ToString();
                    hylContractSts.ForeColor = System.Drawing.Color.Red;
                    hylContractSts.Font.Bold = true;
                }
                else
                {
                    hylContractSts.Text = "This Quotation is using Contract code :" + dtContractSts.Rows[0]["QTN_Contract_Code"].ToString();
                    hylContractSts.NavigateUrl = "~/purchase/ctp_contract_details.aspx?Quotation_ID=" + dtContractSts.Rows[0]["Quotation_ID"].ToString() + "&supplier_code=" + Session["SuppCode"].ToString();
                }
            }

            txtdelremark.Text = dsQuot.Tables[0].Rows[0]["Delivery_Instructions"].ToString();
            lblDelDT.Text = dsQuot.Tables[0].Rows[0]["DELIVERY_DATE"].ToString();
            lblDeLPort.Text = dsQuot.Tables[0].Rows[0]["PORT_NAME"].ToString();

            if (dsQuot.Tables[0].Rows.Count > 0 && dsQuot.Tables[1].Rows.Count > 0)
            {
                //this fields coming from table :[PMS_Dtl_Requisition]
                lblReqNo.Text = dsQuot.Tables[0].Rows[0]["Requisition_Code"].ToString();
                lblVessel.Text = dsQuot.Tables[2].Rows[0]["Vessel_Name"].ToString();
                lblCatalog.Text = dsQuot.Tables[0].Rows[0]["System_Description"].ToString();
                lblmachinaryname.Text = dsQuot.Tables[0].Rows[0]["System_Description"].ToString();
                //lblToDate.Text = dsQuot.Tables[0].Rows[0]["Date_Of_Creatation"].ToString();
                txtPurchaserRemarks.Text = dsQuot.Tables[0].Rows[0]["BUYER_COMMENTS"].ToString();
                lblToDate.Text = Request.QueryString["Quotation_Due_Date"].ToString();
                txtCatalogueCode.Text = dsQuot.Tables[0].Rows[0]["System_Code"].ToString();
                lblTotalItems.Text = dsQuot.Tables[0].Rows[0]["TOTAL_ITEMS"].ToString();
                lblDept.Text = dsQuot.Tables[0].Rows[0]["Name_Dept"].ToString();
                lblReqType.Text = dsQuot.Tables[0].Rows[0]["URGENCY_CODE"].ToString();
                txtreason.Text = dsQuot.Tables[0].Rows[0]["REASON_TRANS_PKG"].ToString();
                lblPurchaserName.Text = dsQuot.Tables[0].Rows[0]["purchaserName"].ToString();
                lblPurchaserNum.Text = dsQuot.Tables[0].Rows[0]["purchasernumber"].ToString();
                txtTruckCharge.Text = dsQuot.Tables[0].Rows[0]["Truck_Cost"].ToString();
                txtBarge.Text = dsQuot.Tables[0].Rows[0]["Barge_Workboat_Cost"].ToString();
                txtOtherChange.Text = dsQuot.Tables[0].Rows[0]["Other_Charges"].ToString();
                txtReasonOthers.Text = dsQuot.Tables[0].Rows[0]["Other_Charges_Reason"].ToString();
                ctlPortList1.SelectedValue = dsQuot.Tables[0].Rows[0]["Delivery_Origin"].ToString();
                #region Declined Quotation
                /// Supplier should not be able to upload any quotation for declined RFQ. 
                hdnLink.Value = Convert.ToString(dsQuot.Tables[0].Rows[0]["Link"]);
                if (hdnLink.Value == "3")//Link:3=> declined Quotation.
                {
                    FileRFQUpload.Enabled = false;
                    btnRFQUpload.Enabled = false;
                }
                ///End
                #endregion
                if (DsMech.Tables[0].Rows.Count > 0)
                {
                    lblMachinesrno.Text = DsMech.Tables[0].Rows[0]["System_Serial_Number"].ToString();
                    lblMakerCity.Text = DsMech.Tables[0].Rows[0]["MakerCity"].ToString();
                    lblMakerContact.Text = DsMech.Tables[0].Rows[0]["MakerCONTACT"].ToString();
                    lblMakerEmail.Text = DsMech.Tables[0].Rows[0]["MakerEmail"].ToString();
                    lblMakerName.Text = DsMech.Tables[0].Rows[0]["MakerName"].ToString();
                    lblMakerPh.Text = DsMech.Tables[0].Rows[0]["MakerPhone"].ToString();
                    lblModel.Text = DsMech.Tables[0].Rows[0]["Model_Type"].ToString();
                    txtAddress.Text = DsMech.Tables[0].Rows[0]["MakerAddress"].ToString();
                    lblParticulars.Text = DsMech.Tables[0].Rows[0]["MechInfo"].ToString();
                    lblSetInstalled.Text = DsMech.Tables[0].Rows[0]["Set_Instaled"].ToString();
                }

                if (dtVesselDtl.Rows.Count > 0)
                {
                    lblVesselExName1.Text = Convert.ToString(dtVesselDtl.Rows[0]["VesselExNames"]);
                    //lblVesselExName2.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Vessel_Ex_Name2"]);
                    lblVesselHullNo.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Hull_No"]);
                    lblVesselType.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Type"]);
                    lblVesselYard.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Yard"]);
                    lblVesselDelvDate.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Delvry_Date"]);
                    lblIMOno.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_IMO_No"]);
                }

                if (lblReqType.Text == "Normal")
                {
                    lblReqType.ForeColor = Color.Blue;
                }
                else
                {
                    lblReqType.ForeColor = Color.Red;
                }


                txtRequi.Text = dsQuot.Tables[0].Rows[0]["QUOTATION_COMMENTS"].ToString();
                txtTotalDiscount.Text = dsQuot.Tables[0].Rows[0]["DISCOUNT"].ToString();
                //txtRebate.Text = dsQuot.Tables[0].Rows[0]["REBATE"].ToString();

                if (dsQuot.Tables[0].Rows[0]["CURRENCY"].ToString() != "")
                {

                    DDLCurrency.Items.FindByText(dsQuot.Tables[0].Rows[0]["CURRENCY"].ToString().Trim()).Selected = true;
                    HiddenField2.Value = dsQuot.Tables[0].Rows[0]["PREVIOUS_EXCHANGE_RATE"].ToString();
                }
                txtExchangeRate.Text = dsQuot.Tables[0].Rows[0]["PREVIOUS_EXCHANGE_RATE"].ToString();
                // txtSurcharge.Text = Convert.ToDecimal(dsQuot.Tables[1].Rows[0]["SURCHARGES"]).ToString();

                //  txtAddCharge.Text = Convert.ToDecimal(dsQuot.Tables[1].Rows[0]["Additional_Charges"]).ToString();
                // DDlRbtType.SelectedValue = dsQuot.Tables[0].Rows[0]["Rebate_Type"].ToString();
                txtQuppQtnRef.Text = dsQuot.Tables[0].Rows[0]["Supplier_Quotation_Reference"].ToString();

                if (Convert.ToDecimal(dsQuot.Tables[0].Rows[0]["Packing_Handling_Charges"].ToString()) != -100)
                {
                    txtPkgHandling.Text = Convert.ToDecimal(dsQuot.Tables[0].Rows[0]["Packing_Handling_Charges"]).ToString();
                }
                else
                {
                    txtPkgHandling.Text = "";
                }

                if (Convert.ToDecimal(dsQuot.Tables[0].Rows[0]["Freight_Cost"].ToString()) != -100)
                {
                    txtTransCost.Text = Convert.ToDecimal(dsQuot.Tables[0].Rows[0]["Freight_Cost"]).ToString();
                }
                else
                {
                    txtTransCost.Text = "";
                }


                //Binding the Grid.
                rgdQuoEntry.DataSource = dsQuot.Tables[1];
                rgdQuoEntry.DataBind();

                iCountRows = dsQuot.Tables[1].Rows.Count; //Is being used in JavaScript

                HiddenField1.Value = rgdQuoEntry.Items.Count.ToString();
                // If the Quotation Status is 'Finalization' -->(Quotation Finalization) then Every fields should be bind

                if (strQuotStatus == "F" || strQuotStatus == "A" || strQuotStatus == "N" || strQuotStatus == "Q" || strQuotStatus == "C")
                {
                    EnableGridtext(false);
                }


                // If Quotatio Status is 'Saved', 'New' --> (Quotation Saved) then User are able to Update the Quotation Data.
                else
                {
                    EnableGridtext(true);
                }
                if (dsQuot.Tables[5].Rows.Count > 0)
                {
                    if (dsQuot.Tables[5].Rows[0]["Applicable_Flag"].ToString() == "No")
                    {
                        txtVat.Text = Convert.ToDecimal(dsQuot.Tables[1].Rows[0]["Vat"]).ToString();
                        txtVat.Enabled = false;
                    }
                    else
                    {
                        txtVat.Enabled = true;
                        txtVat.Text = Convert.ToDecimal(dsQuot.Tables[1].Rows[0]["Vat"]).ToString();
                    }
                }
                else
                {
                    txtVat.Text = Convert.ToDecimal(dsQuot.Tables[1].Rows[0]["Vat"]).ToString();
                    txtVat.Enabled = true;
                }
                HideDiscount();
            }

            DataTable dtDept = objQuoBLL.GetDeptCode(Request.QueryString["Dept_Code"].ToString());

            string strDeptCode = dtDept.Rows[0]["Form_type"].ToString();

            if (strDeptCode == "ST")
            {
                rgdQuoEntry.MasterTableView.Columns[2].Display = false;
                rgdQuoEntry.MasterTableView.Columns[3].Display = true;

            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    protected void HideDiscount()
    {
        SMS.Business.PURC.BLL_PURC_Common objComm = new SMS.Business.PURC.BLL_PURC_Common();
        string CompanyID = "0";
        DataTable dt = objComm.Get_LIB_Page_Config(UDFLib.ConvertToInteger(CompanyID), PageKeyValue);

        if (dt.Rows.Count > 0)
        {
            //rgdQuoEntry.MasterTableView.GetColumn("QUOTED_DISCOUNT").Visible = false; 
            rgdQuoEntry.MasterTableView.GetColumn("QUOTED_DISCOUNT").Display = false;            
        }
    }

    protected void SumTatalAmount()
    {
        decimal Total_Amount = 0;

        foreach (GridDataItem dataItem in rgdQuoEntry.MasterTableView.Items)
        {
            TextBox txtTotalRate = (TextBox)(dataItem.FindControl("txtTotalRate") as TextBox);
            Total_Amount = Convert.ToDecimal(txtTotalRate.Text);
            lblAmount.Text = Total_Amount + Total_Amount.ToString("#,###.00");
        }
    }

    protected void BindRequistiionInfo()
    {
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        DataTable dtReqInfo = new DataTable();
        dtReqInfo = objQuoBLL.GetRequisition_info(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString());
        lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();
        lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
        lblCatalog.Text = dtReqInfo.DefaultView[0]["SYSTEM_Description"].ToString();
        //lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
        lblTotalItems.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
    }

    protected void BindCurrency()
    {

        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        DataTable dt = new DataTable();
        dt = objQuoBLL.GetCurrency();

        DDLCurrency.DataSource = dt;
        DDLCurrency.DataTextField = "Currency_Code";
        DDLCurrency.DataValueField = "Currency_Code";
        DDLCurrency.DataBind();

        DDLCurrency.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (isValidAll())
        {
            SaveQuotation();

            clsQuotationBLL objQuoBLL = new clsQuotationBLL();
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append("update dbo.PURC_Dtl_Reqsn set Quotation_Status='F");
            strQuery.Append("',Quotation_Status_Date=getdate() where REQUISITION_CODE='");
            strQuery.Append(Request.QueryString["Requisitioncode"].ToString());
            strQuery.Append("' and QUOTATION_CODE='");
            strQuery.Append(Request.QueryString["QUOTATION_CODE"].ToString());
            strQuery.Append("' and QUOTATION_SUPPLIER='");
            strQuery.Append(Session["SuppCode"].ToString());
            strQuery.Append("' and DOCUMENT_CODE='");
            strQuery.Append(Request.QueryString["Document_Code"].ToString());
            strQuery.Append("' and Vessel_Code='");
            strQuery.Append(Request.QueryString["Vessel_Code"].ToString());
            strQuery.Append("' ");
            strQuery.Append(" update PURC_Dtl_Quoted_Prices set Quotation_Status='R',SYNC_FLAG='0',Date_Of_Modified=getdate() where [QUOTATION_CODE]='");
            strQuery.Append(Request.QueryString["QUOTATION_CODE"].ToString());
            strQuery.Append("' and  [SUPPLIER_CODE]='");
            strQuery.Append(Session["SuppCode"].ToString());
            strQuery.Append("' and DOCUMENT_CODE='");
            strQuery.Append(Request.QueryString["Document_Code"].ToString());
            strQuery.Append("' and Vessel_Code='");
            strQuery.Append(Request.QueryString["Vessel_Code"].ToString());
            strQuery.Append("' ");

            string FinalQuery = strQuery.ToString();
            int valRet = objQuoBLL.ExecuteQuery(FinalQuery);
            //lblErrorMsg.Text = "Quotation has been submited and sent to office.";
            String msg = String.Format("javascript:alert('Quotation has been submitted and sent to office.');window.close();");
            // String msg = String.Format("alert('Quotation has been finalized and sent to office.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

        }
    }

    protected void btnDeclinQuote_Click(object sender, EventArgs e)
    {

        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        StringBuilder strQuery = new StringBuilder();
        strQuery.Append("update dbo.PURC_Dtl_Reqsn set Quotation_Status='F");
        strQuery.Append("',Quotation_Status_Date=getdate() , link=3, QUOTATION_COMMENTS='" + txtDeclinetoQuoteRemark.Text.Replace("'", "") + "'  where REQUISITION_CODE='");
        strQuery.Append(Request.QueryString["Requisitioncode"].ToString());
        strQuery.Append("' and QUOTATION_CODE='");
        strQuery.Append(Request.QueryString["QUOTATION_CODE"].ToString());
        strQuery.Append("' and QUOTATION_SUPPLIER='");
        strQuery.Append(Session["SuppCode"].ToString());
        strQuery.Append("' and DOCUMENT_CODE='");
        strQuery.Append(Request.QueryString["Document_Code"].ToString());
        strQuery.Append("' and Vessel_Code='");
        strQuery.Append(Request.QueryString["Vessel_Code"].ToString());
        strQuery.Append("' ");


        string FinalQuery = strQuery.ToString();
        int valRet = objQuoBLL.ExecuteQuery(FinalQuery);
        //lblErrorMsg.Text = "Quotation has been submited and sent to office.";
        String msg = String.Format("javascript:alert('Quotation has been Declined .');window.close();");
        // String msg = String.Format("alert('Quotation has been finalized and sent to office.');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdec", msg, true);


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        string QuotStatus = objQuoBLL.getQuotation_Status(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString(), Session["SuppCode"].ToString().Trim(), Request.QueryString["Document_Code"].ToString(), Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()));
        if (QuotStatus.Trim().ToUpper() != "F")
        {
            SaveQuotation();

            //lblErrorMsg.Text = "Quotation has been saved as draft.";

            String msg = String.Format("alert('Quotation has been saved as draft.');window.close()");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
        else
        {
            String msg = String.Format("alert('Quotation has already been Finalized and Sent to Office.');window.close()");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        String msg = String.Format("window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

    }



    private void EnableGridtext(bool flag)
    {
        this.txtExchangeRate.Enabled = flag;
        this.txtTotalDiscount.Enabled = flag;

        this.txtVat.Enabled = flag;
        btnSubmit.Enabled = flag;
        btnSave.Enabled = flag;
        DDLCurrency.Enabled = flag;
        btnRFQUpload.Enabled = flag;
        Button2.Enabled = flag;
        btnDeclinQuote.Enabled = flag;

        foreach (GridDataItem dataItem in rgdQuoEntry.MasterTableView.Items)
        {
            TextBox txtRate = (dataItem.FindControl("txtRate") as TextBox);
            TextBox txtDiscount = (dataItem.FindControl("txtDiscount") as TextBox);
            //  TextBox txtComments = (TextBox)(dataItem.FindControl("txtComments") as TextBox);
            TextBox txtTotalRate = (dataItem.FindControl("txtTotalRate") as TextBox);

            (dataItem.FindControl("btnMoreItemType") as Button).Enabled = flag;
            txtRate.Enabled = flag;
            txtDiscount.Enabled = flag;
            //  txtComments.Enabled = flag;
            txtTotalRate.Enabled = false;
            txtRequi.Enabled = flag;

        }

    }


    protected void SaveQuotation()
    {
        if (isValidAll())
        {

            try
            {
                // clsQuotationBLL objQuoBLL = new clsQuotationBLL();
                //lblErrorMsg.Text = "";

                StringBuilder strQuery = new StringBuilder();

                strQuery.Append("begin try begin tran  DECLARE @ifExist int = 0 ,@QuotationCode varchar(30) ='' ,@ItemType int =0 ,@ItemRefCode varchar(30)='0'");


                Exchange_rate = 1;


                double TotalAmount = 0.0;
                if (rgdQuoEntry.MasterTableView.Items.Count != 0)
                {

                    foreach (GridDataItem dataItem in rgdQuoEntry.MasterTableView.Items)
                    {

                        TextBox txtRate = (TextBox)(dataItem.FindControl("txtRate") as TextBox);
                        TextBox txtDiscount = (TextBox)(dataItem.FindControl("txtDiscount") as TextBox);
                        // TextBox txtComments = (TextBox)(dataItem.FindControl("txtComments") as TextBox);
                        TextBox txtTotalRate = (TextBox)(dataItem.FindControl("txtTotalRate") as TextBox);
                        TotalAmount += Convert.ToDouble(txtTotalRate.Text);
                        TextBox txtLeadTime = (TextBox)(dataItem.FindControl("txtLeadTime") as TextBox);
                        TextBox txtOfferedQty = (TextBox)(dataItem.FindControl("txtofferedqty") as TextBox);
                        DropDownList DDLType = (DropDownList)(dataItem.FindControl("DDLItemType") as DropDownList);
                        if (txtRate.Text != "")
                        {
                            strQuery.Append(" Update dbo.PURC_Dtl_Quoted_Prices Set ");
                            strQuery.Append("QUOTED_RATE='");
                            if (txtRate.Text == "")
                            {
                                strQuery.Append("0");
                            }
                            else
                            {
                                strQuery.Append((UDFLib.ConvertToDecimal(txtRate.Text)).ToString());
                            }
                            strQuery.Append("', QUOTED_DISCOUNT='");
                            if (txtDiscount.Text == "")
                            {
                                strQuery.Append("0");
                            }
                            else
                            {
                                strQuery.Append(UDFLib.ConvertToDecimal(txtDiscount.Text));
                            }


                            strQuery.Append("', SURCHARGES='");
                            //if (txtSurcharge.Text.ToString() == "")
                            //{
                            strQuery.Append("0");
                            //}
                            //else
                            //{
                            //    strQuery.Append(UDFLib.ConvertToDecimal(txtSurcharge.Text));
                            //}
                            //strQuery.Append(dr["SURCHARGES%"].ToString());
                            strQuery.Append("', Vat='");
                            if (txtVat.Text.ToString() == "")
                            {
                                strQuery.Append("0");
                            }
                            else
                            {
                                strQuery.Append(UDFLib.ConvertToDecimal(txtVat.Text));
                            }
                            strQuery.Append("', QUOTED_Price='");
                            if (txtTotalRate.Text == "")
                            {
                                strQuery.Append("0");
                            }
                            else
                            {
                                strQuery.Append(UDFLib.ConvertToDecimal(txtTotalRate.Text));
                            }
                            strQuery.Append("',QUOTED_CURRENCY='");
                            strQuery.Append(DDLCurrency.SelectedValue.ToString());

                            strQuery.Append("',Additional_Charges='");
                            //if (txtAddCharge.Text == "")
                            //{
                            strQuery.Append("0");
                            //}
                            //else
                            //{
                            //    strQuery.Append(UDFLib.ConvertToDecimal(txtAddCharge.Text));
                            //}
                            strQuery.Append("',Lead_Time='");
                            if (txtLeadTime.Text == "")
                            {
                                strQuery.Append("0");
                            }
                            else
                            {
                                strQuery.Append(UDFLib.ConvertToDecimal(txtLeadTime.Text));
                            }
                            strQuery.Append("',Item_Type='");
                            if (DDLType.SelectedIndex == 0)
                            {
                                strQuery.Append(DDLType.SelectedValue);
                            }
                            else
                            {
                                strQuery.Append(DDLType.SelectedValue);
                            }
                            strQuery.Append("',OFFERED_QTY='");
                            if (txtOfferedQty.Text == "")
                            {
                                strQuery.Append("0");
                            }
                            else
                            {
                                strQuery.Append(UDFLib.ConvertToDecimal(txtOfferedQty.Text));
                            }




                            //  strQuery.Append(txtCurrency.Text);

                            strQuery.Append("',SYNC_FLAG='0' where [QUOTATION_CODE]='");
                            strQuery.Append(Request.QueryString["QUOTATION_CODE"].ToString());
                            strQuery.Append("' and  [SUPPLIER_CODE]='");
                            strQuery.Append(Session["SuppCode"].ToString());
                            strQuery.Append("' and [ITEM_REF_CODE]='");
                            strQuery.Append(dataItem["ITEM_REF_CODE"].Text);
                            strQuery.Append("' and DOCUMENT_CODE='");
                            strQuery.Append(Request.QueryString["Document_Code"].ToString());
                            strQuery.Append("' and Vessel_Code='");
                            strQuery.Append(Request.QueryString["Vessel_Code"].ToString());
                            strQuery.Append("' ");

                            strQuery.Append(@" 
                                         
                                          SELECT  @QuotationCode='" + Request.QueryString["QUOTATION_CODE"].ToString() + @"' , @ItemType = '" + DDLType.SelectedValue + @"' ,@ItemRefCode = '" + dataItem["ITEM_REF_CODE"].Text + @"'            
                                          SET @ifExist=(SELECT COUNT(0) from PURC_DTL_QuotedPrices_ItemType where Quotation_Code=@QuotationCode and Item_Ref_Code=@ItemRefCode  and Item_Type=@ItemType)
                                        IF(isnull(@ifExist,0)!=0)
	                                        BEGIN
	
		                                        UPDATE PURC_DTL_QuotedPrices_ItemType set Quoted_Rate= isnull(" + UDFLib.ConvertToDecimal(txtRate.Text.Trim()) + @",0)
		                                        WHERE Quotation_Code=@QuotationCode and Item_Ref_Code=@ItemRefCode  and Item_Type=@ItemType
	                                        END
                                        ELSE 
	                                        BEGIN
		                                        IF(isnull(" + UDFLib.ConvertToDecimal(txtRate.Text.Trim()) + @",0) > 0)
			                                        BEGIN
				                                        INSERT INTO PURC_DTL_QuotedPrices_ItemType(ID,Quotation_Code,Item_Ref_Code,Item_Type,Quoted_Rate,Date_Of_Creation)
				                                        SELECT isnull(MAX(id),0)+1,@QuotationCode,@ItemRefCode,@ItemType,isnull(" + UDFLib.ConvertToDecimal(txtRate.Text.Trim()) + @",0),GETDATE() FROM PURC_DTL_QuotedPrices_ItemType
			                                        END
	                                        END  
                                            	
                                        ");



                        }
                    }

                    strQuery.Append("update dbo.PURC_DTL_REQSN set Currency='");
                    strQuery.Append(DDLCurrency.SelectedItem.Text);
                    strQuery.Append("',PREVIOUS_EXCHANGE_RATE='");
                    strQuery.Append(Exchange_rate);
                    strQuery.Append("',QUOTATION_COMMENTS='");
                    strQuery.Append(txtRequi.Text.Replace("'", " ")).ToString();
                    strQuery.Append("',Other_Charges_Reason='");
                    strQuery.Append(txtReasonOthers.Text.Replace("'", " "));
                    strQuery.Append("',Supplier_DeliveryTerm='");
                    strQuery.Append(ddlDelvTerm.SelectedValue);
                    strQuery.Append("',Delivery_Origin='");
                    strQuery.Append(ctlPortList1.SelectedValue);
                    strQuery.Append("',DISCOUNT='");
                    strQuery.Append(txtTotalDiscount.Text != "" ? UDFLib.ConvertToDecimal(txtTotalDiscount.Text).ToString() : "0");
                    strQuery.Append("',Truck_Cost='");
                    strQuery.Append(txtTruckCharge.Text != "" ? UDFLib.ConvertToDecimal(txtTruckCharge.Text).ToString() : "0");
                    strQuery.Append("',Barge_Workboat_Cost='");
                    strQuery.Append(txtBarge.Text != "" ? UDFLib.ConvertToDecimal(txtBarge.Text).ToString() : "0");
                    strQuery.Append("',Other_Charges='");
                    strQuery.Append(txtOtherChange.Text != "" ? UDFLib.ConvertToDecimal(txtOtherChange.Text).ToString() : "0");
                    strQuery.Append("',REBATE='");
                    strQuery.Append("0");
                    strQuery.Append("',Rebate_Type='");
                    strQuery.Append("0");
                    strQuery.Append("',Quotation_Status='S");
                    strQuery.Append("',Quotation_Status_Date=getdate()");
                    strQuery.Append(",Supplier_Quotation_Reference='");
                    strQuery.Append(txtQuppQtnRef.Text != "" ? txtQuppQtnRef.Text.Replace("'", " ") : "0");
                    strQuery.Append("',Freight_Cost='");
                    strQuery.Append(txtTransCost.Text != "" ? UDFLib.ConvertToDecimal(txtTransCost.Text).ToString() : "0");
                    strQuery.Append("',Packing_Handling_Charges='");
                    strQuery.Append(txtPkgHandling.Text != "" ? UDFLib.ConvertToDecimal(txtPkgHandling.Text).ToString() : "0");
                    strQuery.Append("',REASON_TRANS_PKG='");
                    strQuery.Append(txtreason.Text.Replace("'", " "));
                    strQuery.Append("' where REQUISITION_CODE='");
                    strQuery.Append(Request.QueryString["Requisitioncode"].ToString());
                    strQuery.Append("' and QUOTATION_CODE='");
                    strQuery.Append(Request.QueryString["QUOTATION_CODE"].ToString());
                    strQuery.Append("' and QUOTATION_SUPPLIER='");
                    strQuery.Append(Session["SuppCode"].ToString().Trim());
                    strQuery.Append("' and DOCUMENT_CODE='");
                    strQuery.Append(Request.QueryString["Document_Code"].ToString());
                    strQuery.Append("' and Vessel_Code='");
                    strQuery.Append(Request.QueryString["Vessel_Code"].ToString());
                    strQuery.Append("' ");
                    strQuery.Append("commit tran end try begin catch  DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT , @ErrorState INT;SELECT  @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),  @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,@ErrorSeverity, @ErrorState ); rollback tran end catch");

                    string FinalQuery = strQuery.ToString();
                    int valRet = objQuoBLL.ExecuteQuery_String(FinalQuery);




                    lblAmount.Text = TotalAmount.ToString("##,###.00");
                    // double totalInUSD = TotalAmount / (Convert.ToDouble(txtExchangeRate.Text));
                    // lblTotalAmountUSD.Text = totalInUSD.ToString("##,###.00");


                }
                else
                {
                    //lblErrorMsg.Text = "There is no data to upload, Please check the uploaded file.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
            finally
            {
                // RefreshGrid();

            }
        }
    }

    protected void rpAttachment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        string strPath = ConfigurationManager.AppSettings["INVFolderPath"].ToString();
        if (((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl.ToUpper().Contains("SENDRFQ/"))
        {
            ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl = "../Purchase/" + ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl;
        }
        else if (!((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl.ToUpper().Contains("/"))
        {
            ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl = "../Upload/Purchase/" + ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl;
        }
        ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl = ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl.Replace("..", strPath);
    }

    protected void rgdQuoEntry_ItemDataBound(object sender, GridItemEventArgs e)
    {
        

    }





    protected void rgdQuoEntry_DataBound(object sender, EventArgs e)
    {
        foreach (GridDataItem dataItem in rgdQuoEntry.MasterTableView.Items)
        {
            TextBox txtLT = (TextBox)(dataItem.FindControl("txtLeadTime") as TextBox);
            txtLT.Attributes.Add("onKeydown", "return MaskMoney(event)");

            Label lblLDesc = (Label)(dataItem.FindControl("lblLongDesc11") as Label);
            Label lblitem = (Label)(dataItem.FindControl("lblitem") as Label);

            DropDownList DDLItem = (DropDownList)(dataItem.FindControl("DDLItemType") as DropDownList);
            DDLItem.Items.Clear();
            DDLItem.DataSource = objQuoBLL.GetItemType();
            DDLItem.DataTextField = "Description";
            DDLItem.DataValueField = "code";
            DDLItem.DataBind();

            DDLItem.SelectedValue = dataItem["Item_Type"].Text.Trim();

            if (!string.IsNullOrWhiteSpace(((Label)dataItem.FindControl("lbllongDesc")).ToolTip))
            {
                string long_desc = "";//= string.IsNullOrWhiteSpace(((Label)dataItem.FindControl("lbllongDesc")).Text) ? "Long Description not found !" : "<b>Long Description :</b> <br>" + ((Label)dataItem.FindControl("lbllongDesc")).Text;
                long_desc = long_desc + "<br><br>" + "<b>Item Comment:</b><br>" + ((Label)dataItem.FindControl("lbllongDesc")).ToolTip;
                (dataItem.FindControl("imgItemDetails") as System.Web.UI.WebControls.Image).Attributes.Add("onclick", "js_ShowToolTip_Fixed('<table><tr><td > " + long_desc + "</td></tr></table>',event,this)");
                ((Label)dataItem.FindControl("lblitem")).BackColor = System.Drawing.Color.Yellow;
            }
            else
                (dataItem.FindControl("imgItemDetails") as System.Web.UI.WebControls.Image).Visible = false;

            if (((Label)dataItem.FindControl("lblVesselID")).Text != "0")
            {
                dataItem.BackColor = System.Drawing.Color.Yellow;
            }
            #region JIT_10031 /*JIT:10031 Short/Long Description length limit upto 50 characters. as content goes out of page. */
            lblLDesc.ToolTip = lblLDesc.Text;
            if (lblLDesc.Text.Length > 30)
            {
                lblLDesc.Text = lblLDesc.Text.Substring(0, 30);
            }
            lblitem.ToolTip = lblitem.Text;
            if (lblitem.Text.Length > 30)
            {
                lblitem.Text = lblitem.Text.Substring(0, 30);
            }
            #endregion
        }




    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }

    public void BindRequisitionSummary()
    {
        try
        {
            clsQuotationBLL objQuoBLL = new clsQuotationBLL();
            DataSet dsReqSumm = new DataSet();
            dsReqSumm = objQuoBLL.GetRequisitionSummary(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString());

            lblCatalog.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Catalog"]);
            lblReqNo.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["RequistionCode"]);
            //lblToDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ToDate"]);
            lblToDate.Text = Request.QueryString["Quotation_Due_Date"];
            lblVessel.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["VesselName"]);
            // txtComments.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ReqComents"]);



        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = ex.Message;
        }


    }

    protected void txtQuppQtnRef_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnLeadTimeOk_Click(object sender, EventArgs e)
    {
        divLeadTime.Visible = false;
        foreach (GridDataItem dataItem in rgdQuoEntry.MasterTableView.Items)
        {
            (dataItem.FindControl("txtLeadTime") as TextBox).Text = txtLeadTimeEnt.Text.Trim();
        }
        //txtLeadTimeEnt.Text = "0";
    }

    protected void btnLeadTime_Click(object sender, EventArgs e)
    {
        divLeadTime.Visible = true;
        // DivMaker.VisibleOnLoad = true;
    }

    protected void ddlDelvTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtInfo = BLL_PURC_Purchase.GET_SystemParameters(int.Parse(ddlDelvTerm.SelectedValue));
        if (dtInfo.Rows.Count > 0)
        {
            lblMsgonddlDelvTerm.Text = dtInfo.Rows[0]["Short_Code"].ToString();
        }
        else
            lblMsgonddlDelvTerm.Text = "";


    }

    protected void btnSaveItem_Click(object s, EventArgs e)
    {
        try
        {


            string ItemRefCode = ViewState["ItemTypeItemRefCode"].ToString();
            if (dlItemType.Items.Count > 0)
            {
                foreach (DataListItem item in dlItemType.Items)
                {
                    TextBox Price = (TextBox)item.FindControl("txtQuotedprice");
                    Label lblItemType = (Label)item.FindControl("lblItemType");
                    BLL_PURC_Purchase.INSERT_ItemType(Request.QueryString["QUOTATION_CODE"].ToString(), ItemRefCode, UDFLib.ConvertToInteger(Price.ToolTip), UDFLib.ConvertToDecimal(Price.Text.Trim()), UDFLib.ConvertToInteger(lblItemType.ToolTip));
                }
            }
            dvitemtype.Visible = false;
            ViewState["ItemTypeItemRefCode"] = "";
        }
        catch { }


    }

    protected void DDLItemType_SelectedIndexChanged(object s, EventArgs e)
    {
        GridDataItem grItem = (GridDataItem)((DropDownList)s).Parent.Parent.Parent.Parent;
        string itemType = ((DropDownList)s).SelectedValue;
        DataTable dtprice = BLL_PURC_Purchase.GET_ItemType(Request.QueryString["QUOTATION_CODE"].ToString(), grItem["ITEM_REF_CODE"].Text);
        dtprice.DefaultView.RowFilter = "Code=" + itemType;
        ((TextBox)grItem.FindControl("txtRate")).Text = dtprice.DefaultView[0]["Quoted_Rate"].ToString();

        ((UpdatePanel)grItem.FindControl("updUnitPrice")).Update();

        string js = "Claculate('" + ((TextBox)grItem.FindControl("txtRate")).ClientID + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCalc", js, true);

    }

    protected bool isValidAll()
    {
        lblErrorMsg.Text = "";
        bool isvalid = true;

        decimal pkg = 0;
        if (decimal.TryParse(txtPkgHandling.Text.Trim(), out pkg))
        {
        }
        else
        {
            if (txtPkgHandling.Text.Trim() != "0")
            {
                if (txtPkgHandling.Text.Trim() == "" || UDFLib.ConvertToDecimal(txtPkgHandling.Text.Trim()) == 0)
                {
                    isvalid = false;

                    lblErrorMsg.Text = "Please enter Pkg & Handling , ";
                }
            }
        }

        decimal trans = 0;
        if (decimal.TryParse(txtTransCost.Text.Trim(), out trans))
        {
        }
        else
        {
            if (txtTransCost.Text.Trim() != "0")
            {
                if (txtTransCost.Text.Trim() == "" || UDFLib.ConvertToDecimal(txtTransCost.Text.Trim()) == 0)
                {
                    isvalid = false;
                    lblErrorMsg.Text += "Please provide Freight Cost, ";
                }
            }
        }

        if (txtQuppQtnRef.Text.Trim() == "")
        {
            isvalid = false;
            lblErrorMsg.Text += "Please enter Suppler Quot. ref. , ";
        }
        if (txtreason.Text.Trim() == "")
        {
            isvalid = false;

        }
        if (DDLCurrency.SelectedValue == "0")
        {
            isvalid = false;
            lblErrorMsg.Text += "Please select the currency , ";
        }

        //check the query string
        if (Request.QueryString["Document_Code"] == null || Request.QueryString["Vessel_Code"] == null || Request.QueryString["Requisitioncode"] == null || Request.QueryString["QUOTATION_CODE"] == null)
        {
            isvalid = false;
        }

        bool isValue = false;
        foreach (GridDataItem dataItem in rgdQuoEntry.MasterTableView.Items)
        {

            TextBox txtRate = (TextBox)(dataItem.FindControl("txtRate") as TextBox);
            TextBox txtLeadTime = (TextBox)(dataItem.FindControl("txtLeadTime") as TextBox);
            if (txtRate.Text != "0.0000" && txtRate.Text.Trim() != "")
                isValue = true;
        }

        if (!isValue)
        {
            lblErrorMsg.Text += "Please provide unit price of atleast one Item! ";
            isvalid = false;
        }
        if (!isvalid)
            lblErrorMsg.Visible = true;

        return isvalid;



    }

    protected void btnMoreItemType_Click(object s, EventArgs e)
    {
        Button btnMore = ((Button)s);
        lblItemName.Text = "Item Name: " + btnMore.ToolTip;
        dlItemType.DataSource = BLL_PURC_Purchase.GET_ItemType(Request.QueryString["QUOTATION_CODE"].ToString(), btnMore.CommandArgument.ToString());
        dlItemType.DataBind();
        ViewState["ItemTypeItemRefCode"] = btnMore.CommandArgument;
        dvitemtype.Visible = true;
        upditemtypeSave.Update();


    }

    protected void btnUpload_Click(object s, EventArgs e)
    {
        lblErrorMsg.Text = "";
        DataTable dta = new DataTable();
        dta = objUploadFilesize.Get_Module_FileUpload("TRV_");
        if (dta.Rows.Count > 0)
        {
            string datasize = dta.Rows[0]["Size_KB"].ToString();
            if (FileUploadAttch.HasFile)
            {

                if (FileUploadAttch.PostedFile.ContentLength < Int32.Parse(dta.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    string strLocalPath = FileUploadAttch.PostedFile.FileName;

                    string FileName = Path.GetFileName(strLocalPath);
                    string FileExt = Path.GetExtension(FileName);

                    string Attachpath = ConfigurationManager.AppSettings["AttachURL"];
                    FileUploadAttch.PostedFile.SaveAs(Attachpath + FileName);
                    BLL_PURC_Purchase.SaveAttachedFileInfo(Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Requisitioncode"].ToString(), Session["SuppCode"].ToString(), FileExt, FileName.Replace(FileExt, ""), "../Uploads/Purchase/" + FileName, Session["SuppCode"].ToString(), 0);
                    BindAttachmentList();
                }
                else
                {
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                }
            }
            else
            {
                String msg = String.Format("alert('There is no file to upload.Please Select File & Try again.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
            //{ 
            //     lblMessage.Text = datasize + " KB File size exceeds maximum limit";
            //}
           
        }
        else
        {
            string js2 = "alert('Upload size not set!');return false;";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
    }

    protected void btnExporttoPDF_Click(object s, EventArgs e)
    {
        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
                            "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
                            "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
                            "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
                            "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
                            "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
        ASPXToPDF1.RenderAsPDF("Qtn.pdf");

    }
    /// <summary>
    /// Modify Item reference code data Type Int to string in Page.
    /// Modified By- Alok
    /// Modified Date - 16/09/2016
    /// </summary>
    Hashtable myHashtable;
    protected void btnRFQUpload_Click(object s, EventArgs e)
    {
        try
        {
            CheckExcellProcesses();
            lblErrorMsg.Text = "";
            DataTable dta = new DataTable();
            dta = objUploadFilesize.Get_Module_FileUpload("TRV_");

            if (FileRFQUpload.HasFile)
            {

                if (dta.Rows.Count > 0)
                {
                    string datasize = dta.Rows[0]["Size_KB"].ToString();
                    if (FileRFQUpload.PostedFile.FileName.Contains(".xls") || FileRFQUpload.PostedFile.FileName.Contains(".xlsx"))
                    {
                        if (FileRFQUpload.PostedFile.ContentLength < Int32.Parse(dta.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            //FileRFQUpload.
                            string strLocalPath = FileRFQUpload.PostedFile.FileName;

                            string FileName = Path.GetFileName(strLocalPath);
                            FileRFQUpload.PostedFile.SaveAs(Server.MapPath("~\\WebQtn\\TempUpload\\" + FileName));
                            string strPath = Server.MapPath("~\\WebQtn\\TempUpload\\" + FileName).ToString();

                            ViewState["strPath"] = strPath;

                            ExlApp = new Microsoft.Office.Interop.Excel.Application();
                            ExlWrkBook = ExlApp.Workbooks.Open(strPath,
                                                                      0,
                                                                      true,
                                                                      5,
                                                                      "", "",
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

                            DataSet ds = new DataSet();
                            System.Data.DataTable dt = new System.Data.DataTable();
                            dt.Columns.Add("Quotation_code", typeof(string));
                            dt.Columns.Add("Quotation_date", typeof(string));
                            dt.Columns.Add("Requisition_No", typeof(string));
                            dt.Columns.Add("Vessel_Code", typeof(string));
                            dt.Columns.Add("System_code", typeof(string));
                            dt.Columns.Add("Department", typeof(string));
                            dt.Columns.Add("Quotated_Currency", typeof(string));
                            dt.Columns.Add("supplier_code", typeof(string));
                            dt.Columns.Add("Document_code", typeof(string));
                            dt.Columns.Add("Exchange_Rate", typeof(string));
                            dt.Columns.Add("Supplier_Remark", typeof(string));
                            dt.Columns.Add("Discount", typeof(string));
                            dt.Columns.Add("Vat", typeof(string));
                            dt.Columns.Add("Trans_Freight_Cost", typeof(string));
                            dt.Columns.Add("Pkg_handing_Charges", typeof(string));
                            dt.Columns.Add("OtherCharge", typeof(string));
                            dt.Columns.Add("TruckCharge", typeof(string));
                            dt.Columns.Add("BargeCharge", typeof(string));
                            dt.Columns.Add("ReasonForPkg", typeof(string));
                            dt.Columns.Add("ReasonForOther", typeof(string));
                            dt.AcceptChanges();
                            DataRow dr = dt.NewRow();

                            dr["Quotation_code"] = ((Exel.Range)ExlWrkSheet.Cells[6, 13]).Value2.ToString();
                            dr["Quotation_date"] = ((Exel.Range)ExlWrkSheet.Cells[5, 13]).Value2.ToString();
                            dr["Requisition_No"] = ((Exel.Range)ExlWrkSheet.Cells[2, 3]).Value2.ToString();
                            dr["Vessel_Code"] = ((Exel.Range)ExlWrkSheet.Cells[1, 13]).Value2.ToString();
                            dr["System_code"] = ((Exel.Range)ExlWrkSheet.Cells[3, 13]).Value2.ToString();
                            dr["Department"] = "";// ((Exel.Range)ExlWrkSheet.Cells[3, 13]).Value2.ToString();
                            dr["Quotated_Currency"] = ((Exel.Range)ExlWrkSheet.Cells[5, 3]).Value2.ToString();
                            dr["supplier_code"] = ((Exel.Range)ExlWrkSheet.Cells[7, 13]).Value2.ToString();
                            dr["Document_code"] = ((Exel.Range)ExlWrkSheet.Cells[2, 13]).Value2.ToString();
                            dr["Supplier_Remark"] = ((Exel.Range)ExlWrkSheet.Cells[7, 3]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[7, 3]).Value2.ToString();

                            dr["Discount"] = ((Exel.Range)ExlWrkSheet.Cells[8, 3]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[8, 3]).Value2.ToString();
                            dr["Vat"] = ((Exel.Range)ExlWrkSheet.Cells[9, 7]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[9, 7]).Value2.ToString();
                            dr["Trans_Freight_Cost"] = ((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2.ToString();
                            dr["Pkg_handing_Charges"] = ((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2.ToString();
                            dr["OtherCharge"] = ((Exel.Range)ExlWrkSheet.Cells[8, 9]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[8, 9]).Value2.ToString();
                            dr["ReasonForOther"] = ((Exel.Range)ExlWrkSheet.Cells[8, 12]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[8, 12]).Value2.ToString();
                            dr["ReasonForPkg"] = ((Exel.Range)ExlWrkSheet.Cells[7, 12]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[7, 12]).Value2.ToString();
                            dr["TruckCharge"] = ((Exel.Range)ExlWrkSheet.Cells[9, 3]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[9, 3]).Value2.ToString();
                            dr["BargeCharge"] = ((Exel.Range)ExlWrkSheet.Cells[9, 9]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[9, 9]).Value2.ToString();

                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                            string filte = "Quotation_code='" + Request.QueryString["QUOTATION_CODE"].ToString() + "' and Vessel_Code ='" + Request.QueryString["Vessel_Code"].ToString() + "' and Supplier_code='" + Session["SuppCode"].ToString() + "'";
                            dt.DefaultView.RowFilter = filte;
                            if (dt.DefaultView.Count > 0)
                            {
                                string strCurrency = dt.Rows[0]["Quotated_Currency"].ToString();
                                string strRequi = dt.Rows[0]["Supplier_Remark"].ToString();
                                string strDiscount = dt.Rows[0]["Discount"].ToString();
                                string strVat = dt.Rows[0]["Vat"].ToString();
                                string strTrsportCost = dt.Rows[0]["Trans_Freight_Cost"].ToString();
                                string strPkgCharges = dt.Rows[0]["Pkg_handing_Charges"].ToString();
                                string strAdditionlachrgs = dt.Rows[0]["OtherCharge"].ToString();
                                string strReasonPKG = dt.Rows[0]["ReasonForPkg"].ToString();
                                string strBarge = dt.Rows[0]["BargeCharge"].ToString(); ;
                                string strReasonOther = dt.Rows[0]["ReasonForOther"].ToString(); ;
                                string strTruck = dt.Rows[0]["TruckCharge"].ToString(); ;

                                System.Data.DataTable dtItems = new DataTable();
                                dtItems.Columns.Add("Item_ref_code", typeof(string));
                                dtItems.Columns.Add("Short_desc", typeof(string));
                                dtItems.Columns.Add("Long_desc", typeof(string));
                                dtItems.Columns.Add("Item_comments", typeof(string));
                                dtItems.Columns.Add("Unit", typeof(string));
                                dtItems.Columns.Add("Request_Qty", typeof(decimal));
                                dtItems.Columns.Add("Unit_Price", typeof(decimal));
                                dtItems.Columns.Add("Discount", typeof(decimal));
                                dtItems.Columns.Add("Total_Price", typeof(decimal));
                                dtItems.Columns.Add("Supplier_Remarks", typeof(string));
                                dtItems.Columns.Add("LeadTime", typeof(string));
                                dtItems.Columns.Add("Item_Type", typeof(string));
                                dtItems.AcceptChanges();
                                int i = 15;
                                while (((Exel.Range)ExlWrkSheet.Cells[i, 1]).Value2 != null)
                                {
                                    double value = 0;
                                    if (double.TryParse(((Exel.Range)ExlWrkSheet.Cells[i, 1]).Value2.ToString(), out value))
                                    {
                                        DataRow drNew = dtItems.NewRow();
                                        drNew["Item_ref_code"] = ((Exel.Range)ExlWrkSheet.Cells[i, 4]).Value2.ToString();
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 5]).Value2 != null)
                                        {
                                            drNew["Short_desc"] = ((Exel.Range)ExlWrkSheet.Cells[i, 5]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Short_desc"] = "";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i + 1, 5]).Value2 != null)
                                        {
                                            drNew["Long_desc"] = ((Exel.Range)ExlWrkSheet.Cells[i + 1, 5]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Long_desc"] = "";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i + 2, 5]).Value2 != null)
                                        {
                                            drNew["Item_comments"] = ((Exel.Range)ExlWrkSheet.Cells[i + 2, 5]).Value2.ToString().Trim().Replace("'", "");
                                        }
                                        else
                                        {
                                            drNew["Item_comments"] = "";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 6]).Value2 != null)
                                        {
                                            drNew["Unit"] = ((Exel.Range)ExlWrkSheet.Cells[i, 6]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Unit"] = "";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 7]).Value2 != null)
                                        {
                                            drNew["Request_Qty"] = ((Exel.Range)ExlWrkSheet.Cells[i, 7]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Request_Qty"] = "0";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 8]).Value2 != null)
                                        {
                                            drNew["Unit_Price"] = ((Exel.Range)ExlWrkSheet.Cells[i, 8]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Unit_Price"] = "0";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 9]).Value2 != null)
                                        {
                                            drNew["Discount"] = ((Exel.Range)ExlWrkSheet.Cells[i, 9]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Discount"] = "0";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 11]).Value2 != null)
                                        {
                                            drNew["Total_Price"] = ((Exel.Range)ExlWrkSheet.Cells[i, 11]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Total_Price"] = "0";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 12]).Value2 != null)
                                        {
                                            drNew["Supplier_Remarks"] = ((Exel.Range)ExlWrkSheet.Cells[i, 12]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["Supplier_Remarks"] = "";
                                        }
                                        if (((Exel.Range)ExlWrkSheet.Cells[i, 10]).Value2 != null)
                                        {
                                            drNew["LeadTime"] = ((Exel.Range)ExlWrkSheet.Cells[i, 10]).Value2.ToString();
                                        }
                                        else
                                        {
                                            drNew["LeadTime"] = "0";
                                        }

                                        dtItems.Rows.Add(drNew);

                                    }
                                    i = i + 3;
                                }
                                dtItems.AcceptChanges();

                                SaveUploadedQuotationData(dtItems, strCurrency, strRequi, strDiscount, strVat, strTrsportCost, strPkgCharges
                                    , strAdditionlachrgs, strReasonPKG, strBarge, strReasonOther, strTrsportCost);

                                btnSave.Enabled = true;
                            }
                            else
                            {
                                String msg = String.Format("alert('The uploaded excel file do not belong to the selected supplier');");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                                lblErrorMsg.Text = "The uploaded Excel file do not belong to the selected supplier";
                                btnSave.Enabled = false;
                            }
                        }
                        else
                        {

                            lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                        }
                    }
                    else
                    {
                        string msg = "alert('Invalid File Type,Please Upload Excel File for Selected Supplier !');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", msg, true);
                    } 
                }
                else
                {
                    String msg = String.Format("alert('There is no file to upload.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
                
            }
            else
            {
                String msg = String.Format("alert('There is no file to upload.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        catch (Exception ex) //Pranali_22042015/JIT_1285/UPDLOADING EXCEL FILES THAT DOEN'T BLEONG TO SUPPLIER.
        {
            UDFLib.WriteExceptionLog(ex);
                String msg1 = String.Format("alert('The uploaded file does not belong to the selected supplier!');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", msg1, true);
        }
        finally
        {
            //ExlWrkBook.Close(null, null, null);
            //ExlApp.Workbooks.Close();
            //ExlApp.Quit();
            KillExcel();
            //Marshal.ReleaseComObject(ExlApp);
            //Marshal.ReleaseComObject(ExlWrkSheet);
            //Marshal.ReleaseComObject(ExlWrkBook);
        }
    }
   

    protected void SaveUploadedQuotationData(DataTable dt, string strCurrency, string strRequi,
                                                string strDiscount, string strVat, string strTrspontCost, string strPkgCharges,
                                                string strAdditionlachrgs,
                                                string strReasonPKG, string strBarge, string strReasonOther, string strTruckCost)
    {

        try
        {

            lblErrorMsg.Text = "";

            clsQuotationBLL objBll = new clsQuotationBLL();

            StringBuilder strQuory = new StringBuilder();
            strQuory.Append(" begin try begin tran   DECLARE @ifExist int = 0 ,@QuotationCode varchar(30) ='' ,@ItemType int =0 ,@ItemRefCode varchar(30)='0' ");
            Exchange_rate = 1;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    strQuory.Append(" Update dbo.PURC_Dtl_Quoted_Prices Set ");
                    strQuory.Append("QUOTED_RATE='");
                    if (dr["Unit_Price"].ToString() == "")
                    {
                        strQuory.Append("0");
                    }
                    else
                    {
                        strQuory.Append((Convert.ToDecimal(dr["Unit_Price"].ToString())).ToString());
                    }
                    strQuory.Append("', QUOTED_DISCOUNT='");
                    if (dr["Discount"].ToString() == "")
                    {
                        strQuory.Append("0");
                    }
                    else
                    {
                        strQuory.Append(dr["Discount"].ToString());
                    }


                    strQuory.Append("', SURCHARGES='");
                    strQuory.Append("0");
                    strQuory.Append("', Vat='");
                    if (txtVat.Text.ToString() == "")
                    {
                        strQuory.Append("0");
                    }
                    else
                    {
                        strQuory.Append(txtVat.Text.ToString());
                    }
                    strQuory.Append("', QUOTED_Price='");
                    strQuory.Append(dr["Total_Price"].ToString());
                    strQuory.Append("',QUOTED_CURRENCY='");
                    strQuory.Append(strCurrency);
                    strQuory.Append("',QUOTATION_REMARKS='");
                    if (dr["Supplier_Remarks"].ToString() == "" || dr["Supplier_Remarks"].ToString() == "&nbsp;")
                    {
                        strQuory.Append("");
                    }
                    else
                    {
                        strQuory.Append(dr["Supplier_Remarks"].ToString().Trim().Replace("'", ""));
                    }
                    strQuory.Append("',Lead_Time='");
                    strQuory.Append(dr["LeadTime"].ToString());

                    strQuory.Append("',Item_Type=154");
                    strQuory.Append(",Additional_Charges='");
                    strQuory.Append(strAdditionlachrgs);
                    strQuory.Append("',Quotation_Status='R',SYNC_FLAG='0',Date_Of_Modified=getdate() where [QUOTATION_CODE]='");
                    strQuory.Append(Request.QueryString["QUOTATION_CODE"].ToString());
                    strQuory.Append("' and  [SUPPLIER_CODE]='");
                    strQuory.Append(Session["SuppCode"].ToString());
                    strQuory.Append("' and [ITEM_REF_CODE]='");
                    strQuory.Append(dr["ITEM_REF_CODE"].ToString());
                    strQuory.Append("' and DOCUMENT_CODE='");
                    strQuory.Append(Request.QueryString["Document_Code"].ToString());
                    strQuory.Append("' and Vessel_Code='");
                    strQuory.Append(Request.QueryString["Vessel_Code"].ToString());
                    strQuory.Append("' ");

                    strQuory.Append(@" 
                                         
                                          SELECT  @QuotationCode='" + Session["SuppCode"].ToString() + @"' , @ItemType =154  ,@ItemRefCode = '" + dr["ITEM_REF_CODE"].ToString() + @"'            
                                          SET @ifExist=(SELECT COUNT(0) from PURC_DTL_QuotedPrices_ItemType where Quotation_Code=@QuotationCode and Item_Ref_Code=@ItemRefCode  and Item_Type=@ItemType)
                                        IF(isnull(@ifExist,0)!=0)
	                                        BEGIN
	
		                                        UPDATE PURC_DTL_QuotedPrices_ItemType set Quoted_Rate= isnull(" + UDFLib.ConvertToDecimal(dr["Unit_Price"].ToString().Trim()) + @",0)
		                                        WHERE Quotation_Code=@QuotationCode and Item_Ref_Code=@ItemRefCode  and Item_Type=@ItemType
	                                        END
                                        ELSE 
	                                        BEGIN
		                                        IF(isnull(" + UDFLib.ConvertToDecimal(dr["Unit_Price"].ToString().Trim()) + @",0) > 0)
			                                        BEGIN
				                                        INSERT INTO PURC_DTL_QuotedPrices_ItemType(ID,Quotation_Code,Item_Ref_Code,Item_Type,Quoted_Rate,Date_Of_Creation)
				                                        SELECT isnull(MAX(id),0)+1,@QuotationCode,@ItemRefCode,@ItemType,isnull(" + UDFLib.ConvertToDecimal(dr["Unit_Price"].ToString().Trim()) + @",0),GETDATE() FROM PURC_DTL_QuotedPrices_ItemType
			                                        END
	                                        END  
                                            	
                                        ");

                }

                strQuory.Append("update dbo.PURC_Dtl_REQSN set Currency='");
                strQuory.Append(strCurrency);
                strQuory.Append("',PREVIOUS_EXCHANGE_RATE=1");//the actual value will be saved on po approval  
                strQuory.Append(",Supplier_Quotation_Reference='");
                strQuory.Append(((Exel.Range)ExlWrkSheet.Cells[7, 3]).Value2.ToString().Trim().Replace("'", ""));
                strQuory.Append("',Freight_Cost='");
                strQuory.Append(((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2.ToString() != "" ? ((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2.ToString() : "0");
                strQuory.Append("',Packing_Handling_Charges='");
                strQuory.Append(((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2.ToString() != "" ? ((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2.ToString() : "0");
                strQuory.Append("',REBATE='");
                strQuory.Append(((Exel.Range)ExlWrkSheet.Cells[9, 9]).Value2.ToString());
                strQuory.Append("',Truck_Cost='");
                strQuory.Append(strTruckCost != "" ? UDFLib.ConvertToDecimal(strTruckCost).ToString() : "0");
                strQuory.Append("',Barge_Workboat_Cost='");
                strQuory.Append(txtBarge.Text != "" ? UDFLib.ConvertToDecimal(txtBarge.Text).ToString() : "0");
                strQuory.Append("',Other_Charges='");
                strQuory.Append(strAdditionlachrgs != "" ? UDFLib.ConvertToDecimal(strAdditionlachrgs).ToString() : "0");
                strQuory.Append("',REASON_TRANS_PKG='");
                strQuory.Append(strReasonPKG.ToString().Trim().Replace("'", ""));
                strQuory.Append("',Other_Charges_Reason='");
                strQuory.Append(strReasonOther.ToString().Trim().Replace("'", ""));
                strQuory.Append("',QUOTATION_COMMENTS='");
                strQuory.Append(txtRequi.Text.Trim().Replace("'", ""));
                strQuory.Append("',DISCOUNT='");
                strQuory.Append(strDiscount != "" ? strDiscount : "0");
                strQuory.Append("' , Quotation_Status='S' ,Quotation_Status_Date=getdate() where REQUISITION_CODE='");
                strQuory.Append(Request.QueryString["Requisitioncode"].ToString());
                strQuory.Append("' and QUOTATION_CODE='");
                strQuory.Append(Request.QueryString["QUOTATION_CODE"].ToString());
                strQuory.Append("' and QUOTATION_SUPPLIER='");
                strQuory.Append(Session["SuppCode"].ToString());
                strQuory.Append("' and DOCUMENT_CODE='");
                strQuory.Append(Request.QueryString["Document_Code"].ToString());
                strQuory.Append("' and Vessel_Code='");
                strQuory.Append(Request.QueryString["Vessel_Code"].ToString());
                strQuory.Append("' ");
                strQuory.Append(" commit tran end try begin catch  DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT , @ErrorState INT;SELECT  @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),  @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,@ErrorSeverity, @ErrorState ); rollback tran end catch");

                string FinalQuery = strQuory.ToString();
                int valRet = objBll.ExecuteQuery(FinalQuery);

                string strPath = Path.GetDirectoryName((String)ViewState["strPath"].ToString());
                string FileName = Path.GetFileName((String)ViewState["strPath"].ToString());

                FileRFQUpload.PostedFile.SaveAs(Server.MapPath("~\\WebQtn\\TempUpload\\" + FileName));

                //Copy the uploaded file into the server.
                if (System.IO.File.Exists(Server.MapPath("~\\WebQtn\\UploadQuot\\" + FileName)) == false)
                {
                    File.Move(Server.MapPath("~\\WebQtn\\TempUpload\\" + FileName), Server.MapPath("~\\WebQtn\\UploadQuot\\" + FileName));
                }

                string strQuoUpdPath = Server.MapPath("~\\WebQtn\\UploadQuot\\").ToString();
                lblErrorMsg.Text = "Quotation has been uploaded sucessfully";
                String script = String.Format("alert('Quotation has been uploaded sucessfully.');window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
            }
            else
            {
                lblErrorMsg.Text = "There is no data to upload, Please check the uploaded file.";
            }
        }
        catch (Exception ex)
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = ex.Message;
        }

    }

    protected void btnExportQtnToExcel_Click(object s, EventArgs e)
    {

        /*--------Excel Base RFQ-------------------- */
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        string strPath1 = Server.MapPath(".") + "\\GenerateRFQ\\";
        string FilePath1 = Server.MapPath(".") + "\\ExcelFile\\";
        string FileName = "";
        ExceldataImport objExcelRFQ = new ExceldataImport();

        DataSet dsRFQ = objQuoBLL.GetDataToGenerateRFQ(Session["SuppCode"].ToString(), Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());

        string SuppName = dsRFQ.Tables[0].Rows[0]["SHORT_NAME"].ToString();
        string portname = dsRFQ.Tables[0].Rows[0]["PORT_NAME"].ToString();


        FileName = "RFQ_" + Request.QueryString["Vessel_Code"].ToString() + "_" + Request.QueryString["Requisitioncode"].ToString() + "_" + Session["SuppCode"].ToString() + "_" + SuppName.Replace(" ", "_").Replace(".", "_").Replace("&", "_")
                          + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "" + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss")
                          + portname + ".xls";

        objExcelRFQ.WriteExcell(dsRFQ, Request.QueryString["Requisitioncode"].ToString(), FileName, strPath1, FilePath1);

        ResponseHelper.Redirect("Uploads/Purchase/" + FileName, "blanck", "");

    }

    protected void btnUploadExcelQtn_Click(object s, EventArgs e)
    {

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