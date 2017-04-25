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
using System.Text;
using System.IO;
//using Excel;
using System.Runtime.InteropServices;

using System.Diagnostics;
using SMS.Business.Crew;
using System.Collections.Generic;
using SMS.Business.Infrastructure;





public partial class Technical_INV_SelectSuppliers : System.Web.UI.Page
{

    static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    ControlParameter pr = new ControlParameter();
    System.Data.DataTable mytempSuppTable = new System.Data.DataTable();
    public string Supplier = null;
    static string RetValQtnCode = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        dvreqsnItems.Visible = false;
        lblrfqmessage.Text = "";

        btnSendToSupplier.Attributes.Add("onclick", "Async_Get_Reqsn_Validity('" + Request.QueryString["Requisitioncode"] + "')" + ";return Validation()");
        if (!IsPostBack)
        {
            Session["Supplier"] = null;
            ViewState["Supplier"] = "0";
            txtReqCode.Text = Request.QueryString["Requisitioncode"].ToString();
            txtVessselCode.Text = Request.QueryString["Vessel_Code"].ToString();
            gvReqsnItems.DataSource = BLL_PURC_Common.Get_ReqsnItems(Request.QueryString["Requisitioncode"]);
            gvReqsnItems.DataBind();

            BindRequisitionInfo();
            Session["lstId"] = "";

            BindSupplist();

            Session["lstId"] = "";
            lblErrorMsg.Text = "";
            BindCountry();

            DataTable myTable = new DataTable();

            Session["Attachment"] = "";

            string[] value = new string[10];
            Session["Attachmentvalue"] = value;
            filterlistbox();
        }
        lblDataErr.Text = "";
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

    private void BindSupplist()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {


                DataTable dt = objTechService.GetSendedRFQSuppliersList(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                grvSupplier.DataSource = dt;

                grvSupplier.DataBind();
                ViewState["SelectedSupplier"] = dt;

                CheckSendItem();
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void bindlistbox()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                string lCountry = "";
                if (ddlCountry.SelectedValue.ToString() != "0" && ddlCountry.SelectedIndex != -1)
                {
                    lCountry = ddlCountry.SelectedValue.ToString();
                }
                int rowcount = ucCustomPagerItems.isCountRecord;
                DataSet ds = objTechService.SelectSupplier_Filter(null, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, txtCity.Text.Trim(), txtSupname.Text.Trim(), lCountry);
                if (ucCustomPagerItems.isCountRecord == 1)
                {
                    ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                    //ucCustomPagerItems.CountTotalRec = ds.Tables[1].Rows[0]["RowCount"].ToString();
                    ucCustomPagerItems.BuildPager();
                }
                lstsupplier.DataSource = ds.Tables[0];
                lstsupplier.DataBind();

            }
        }
        catch (Exception ex)
        {

        }

    }

    //protected void DataPagerProducts_PreRender(object sender, EventArgs e)
    //{
    //    filterlistbox();

    //}

    protected void BindCountry()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dt = objTechService.GetAllCountries().Tables[0];
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataValueField = "COUNTRY";
                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));

                ddlSuppcategory.DataTextField = "Category_Name";
                ddlSuppcategory.DataValueField = "Category_Code";
                ddlSuppcategory.DataSource = BLL_PURC_Common.Get_Supplier_Category();
                ddlSuppcategory.DataBind();
                ddlSuppcategory.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    private void addTempDataToList(string SupplierCode, string supplierName, string strDate, System.Data.DataTable myTable)
    {
        //check whether supplierCode is already exist
        bool duplicateSuppCode = false;

        for (int i = 0; i <= myTable.Rows.Count - 1; i++)
        {

            if (SupplierCode == myTable.Rows[i][0].ToString())
            {
                duplicateSuppCode = true;
            }
        }

        if (!duplicateSuppCode)
        {

            DataRow row;
            row = myTable.NewRow();

            row["SUPPLIER"] = SupplierCode;
            row["SUPPLIER_NAME"] = supplierName;
            row["Date"] = strDate;

            myTable.Rows.Add(row);
            lstsupplier.DataSource = myTable;
            lstsupplier.DataBind();
            ViewState["SelectedSupplier"] = myTable;
            lblErrorMsg.Text = "";
        }
        else
        {
            lblErrorMsg.Text = "This supplier is already selected.";
        }

    }

    private void AddTempDataToGrid(string SupplierCode, string supplierName, string strDate, System.Data.DataTable myTable)
    {

        //check whether supplierCode is already exist
        bool duplicateSuppCode = false;

        for (int i = 0; i <= myTable.Rows.Count - 1; i++)
        {

            if (SupplierCode == myTable.Rows[i][0].ToString())
            {
                duplicateSuppCode = true;
            }
        }

        if (!duplicateSuppCode)
        {

            DataRow row;
            row = myTable.NewRow();

            //row["SUPPLIER"] = SupplierCode;
            row["SUPPLIER_NAME"] = supplierName;
            row["Date"] = strDate;

            myTable.Rows.Add(row);
            //grvSupplier.DataSource = myTable;
            //grvSupplier.DataBind();
            ViewState["SelectedSupplier"] = myTable;
            lblErrorMsg.Text = "";
        }
        else
        {
            lblErrorMsg.Text = "This supplier is already selected.";
        }

    }
    protected void grvSupplier_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in grvSupplier.MasterTableView.Items)
        {
            DropDownList ddlProperty = (DropDownList)(dataItem.FindControl("ddlProperty") as DropDownList);
            Label lblSupplierProperty = (Label)(dataItem.FindControl("lblSupplierProperty") as Label);
            Label lblApplicable_Flag = (Label)(dataItem.FindControl("lblApplicable") as Label);
            //string dataItem1 = grvSupplier.MasterTableView.Items.ToString();
            if (lblApplicable_Flag.Text.ToString() == "Yes")
            {
                ddlProperty.SelectedValue = "Yes";
            }
            else if (lblApplicable_Flag.Text.ToString() == "No")
            {
                ddlProperty.SelectedValue = "No";
            }
            else if (lblSupplierProperty.Text.ToString() == "Yes")
            {
                ddlProperty.SelectedValue = "Yes";
            }
            else
            {
                ddlProperty.SelectedValue = "No";
            }

        }
    }
    protected void grvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < grvSupplier.Items.Count - 1; i++)
        {
            DropDownList ddl = (DropDownList)grvSupplier.Items[i].FindControl("DDlSupplier");
            System.Web.UI.WebControls.CheckBox chkExportToExcel = (System.Web.UI.WebControls.CheckBox)grvSupplier.Items[i].FindControl("chkExportToExcel");
            DropDownList ddlProperty = (DropDownList)grvSupplier.Items[i].FindControl("ddlProperty");
            string dataItem = grvSupplier.MasterTableView.Items.ToString();
            //ddl.SelectedItem.Value= grvSupplier.Rows[i].Cells[0].Text.ToString();
            ddl.SelectedValue = grvSupplier.Items[i].Cells[0].Text.ToString();

            if (grvSupplier.Items[i].Cells[1].Text.ToString() != "0")
            {
                chkExportToExcel.Checked = true;
            }
            else
            {
                chkExportToExcel.Checked = false;
            }
            if (dataItem  == "Yes")
            {
                ddlProperty.SelectedValue = "Yes";
            }
            else
            {
                ddlProperty.SelectedValue = "No";
            }

        }




    }

    private void BindRequisitionInfo()
    {

        try
        {
            System.Data.DataTable dtReqInfo = new System.Data.DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtReqInfo = objTechService.SelectRequistionToSupplier(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString());
                lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();

                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();
                lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblCatalog.Text = dtReqInfo.DefaultView[0]["Name_Dept"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                ViewState["dlport"] = dtReqInfo.DefaultView[0]["DELIVERY_PORT"].ToString();
                ViewState["dldate"] = dtReqInfo.DefaultView[0]["DELIVERY_DATE"].ToString();
                //ViewState["Supplier_Property"] = dtReqInfo.DefaultView[0]["Supplier_Property"].ToString();
               
            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    protected void optRFQType_SelectedIndexChanged(object sender, EventArgs e)
    {




    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    string FileName;
    protected void btnSendToSupplier_Click(object sender, EventArgs e)
    {
        try
        {
            //check for port name if  same supplier come more than one
            int stsport = 0;
            Session["Supplier"] = null;
            foreach (GridItem gr in grvSupplier.Items)
            {
                string supplier = gr.Cells[2].Text;
                foreach (GridItem grinner in grvSupplier.Items)
                {
                    if (grinner.Cells[2].Text == supplier && gr.RowIndex != grinner.RowIndex)
                    {
                        if (((UserControl_ctlPortList)gr.FindControl("DDLPort")).SelectedValue == ((UserControl_ctlPortList)grinner.FindControl("DDLPort")).SelectedValue)
                        {
                            stsport = 1;
                            String msg = String.Format("alert('Please select the different port for same supplier !');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgport", msg, true);

                            return;
                        }
                    }
                }
            }

            string strPath = Server.MapPath(".") + "\\SendRFQ\\";
            string FilePath = Server.MapPath("~") + "\\Purchase\\ExcelFile\\";
            bool cheboxClick = false;
            string QuotDueDate = "";
            //string sBuyerRemaks = txtRFQRemarks.Text.Trim() != "" ? txtRFQRemarks.Text.Trim() : "Null";
            string sBuyerRemaks = txtRFQRemarks.Text.Trim();
            if (txtfrom.Text != "")
            {
                QuotDueDate = txtfrom.Text.Substring(6, 4) + "/" + txtfrom.Text.Substring(3, 2) + "/" + txtfrom.Text.Substring(0, 2);
            }
            else
            {
                QuotDueDate = "Null";
            }
            foreach (GridItem gr in grvSupplier.Items)
            {
                CheckBox chk1 = (CheckBox)(gr.FindControl("chkExportToExcel") as CheckBox);
                if ((chk1.Checked))
                {
                    cheboxClick = true;
                }
            }

            if (cheboxClick == true)
            {
                if (grvSupplier.Items.Count > 0)
                {
                    DataTable dtQuotationList = new DataTable();
                    dtQuotationList.Columns.Add("Qtncode");
                    dtQuotationList.Columns.Add("amount");

                    foreach (GridDataItem grv in grvSupplier.MasterTableView.Items)
                    {
                        DataSet dsRFQ = new DataSet();
                        DataSet dsSendMailInfo = new DataSet();
                        DataTable dtQtnSupplier = new DataTable();

                        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(grv.FindControl("chkExportToExcel") as System.Web.UI.WebControls.CheckBox);
                        System.Web.UI.WebControls.RadioButtonList optGRowRFQType = (System.Web.UI.WebControls.RadioButtonList)(grv.FindControl("optGRowRFQType") as System.Web.UI.WebControls.RadioButtonList);


                        if (chk.Checked)
                        {
                            string SuppName = grv["SUPPLIER_NAME"].Text.ToString();

                            string SuppCode = grv["SUPPLIER"].Text.ToString();

                            string RowIDSuppCode = grv["RowID"].Text.Trim() + ":" + SuppCode;

                            Dictionary<string, ArrayList> dicItemRefCode = (Dictionary<string, ArrayList>)ViewState["DicItemRefCode"];
                            StringBuilder strItemsForSupplier = new StringBuilder("");
                            if (dicItemRefCode != null)
                            {
                                if (dicItemRefCode.Keys.Contains(RowIDSuppCode))
                                {
                                    ArrayList arrItemRefCode = dicItemRefCode[RowIDSuppCode];
                                    foreach (string sitem in arrItemRefCode)
                                    {
                                        strItemsForSupplier.Append(" select '" + sitem + "' ");
                                    }
                                }
                            }
                            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                            {
                                RetValQtnCode = objTechService.InsertQuotedPriceForRFQ(SuppCode, Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["userid"].ToString(), QuotDueDate, sBuyerRemaks, ((UserControl_ctlPortList)grv.FindControl("DDLPort")).SelectedValue.ToString(), ((TextBox)grv.FindControl("txtDeliveryDate")).Text.Trim(), ((TextBox)grv.FindControl("txtDeliveryInstruction")).Text, strItemsForSupplier.ToString());
                                string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
                                dsRFQ = objTechService.InsertSupplierProperties(SuppCode, Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), ((DropDownList)grv.FindControl("ddlProperty")).SelectedValue.ToString(), RetValQtnCode, GetSessionUserID());
                                dsRFQ = objTechService.GetDataToGenerateRFQ(SuppCode, Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), RetValQtnCode);
                                //check the supplier details in lib_user and insert if not exists
                                dtQtnSupplier = objTechService.GetSupplierUserDetails(SuppCode, "S");
                                dsSendMailInfo = objTechService.GetRFQsuppInfoSendEmail(SuppCode, RetValQtnCode, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["userid"].ToString());
                                string RFQType = optGRowRFQType.SelectedValue.ToString();
                                int value = Int32.Parse(RFQType);

                                switch (value)
                                {
                                    // Excel Based RFQ
                                    case 1:
                                        PO_RFQ_Generate.ExceldataImport objExcelRFQ = new PO_RFQ_Generate.ExceldataImport();
                                        FileName = "RFQ_" + Request.QueryString["Vessel_Code"].ToString() + "_" + Request.QueryString["Requisitioncode"].ToString() + "_" + SuppCode + "_" + ReplaceSpecialCharacterinFileName(SuppName) + DateTime.Now.ToString("yyMMdd") + ((UserControl_ctlPortList)grv.FindControl("DDLPort")).SelectedText.ToString() + ".xls";
                                        objExcelRFQ.WriteExcell(dsRFQ, Request.QueryString["Requisitioncode"].ToString(), FileName, strPath, FilePath);
                                        objTechService.SaveAttachedFileInfo(Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Requisitioncode"].ToString(), SuppCode, ".xls", FileName.Replace(".xls", ""), "SendRFQ/" + FileName, Session["userid"].ToString(), UDFLib.ConvertToInteger(((UserControl_ctlPortList)grv.FindControl("DDLPort")).SelectedValue.ToString()));
                                        SendEmailToSupplier(dsSendMailInfo, SuppCode, ServerIPAdd, FileName, true, RFQType, true);
                                        break;
                                    // Web Based RFQ
                                    case 2:
                                        //No Excel File will be generated because it's Web based RFQ as per satvinder sir..


                                        SendEmailToSupplier(dsSendMailInfo, SuppCode, ServerIPAdd, "", true, RFQType, true);
                                        break;
                                }

                                //objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "RFQ", " ", Convert.ToInt32(Session["USERID"]));
                            }
                        }
                    }

                    if (RetValQtnCode.Length > 1)
                    {
                        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
                        objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "RFQ", " ", Convert.ToInt32(Session["USERID"]), dtQuotationList);
                    }
                    grvSupplier.Columns[1].Visible = false;
                    LinkFileLoc.Text = strPath.ToString();
                    String msg = String.Format("window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
                else
                {
                    lblErrorMsg.Text = "There is no data for supplier.";
                }
            }
            else
            {
                String msg = String.Format("alert('Please select atlest one supplier to send Quotation.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                lblErrorMsg.Text = "Select Atlest one supplier to send the Quatation";
            }
        }

        catch (Exception ex)
        {
            String msg = String.Format("alert('" + ex.Message + ex.Source + ex.StackTrace + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
        finally
        {
            // String msg = String.Format("alert('Please send mail for selected suppliers');window.close();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

        }
    }
   
    private void CheckSendItem()
    {


        foreach (GridItem item in grvSupplier.MasterTableView.Items)
        {
            CheckBox chkRFQSend = (CheckBox)(item.FindControl("chkExportToExcel") as CheckBox);
            if (item is GridDataItem)
            {
                GridDataItem current = item as GridDataItem;
                if (current["Requisition_Code"].Text.ToString() != "New")
                {
                    chkRFQSend.Checked = false;
                    chkRFQSend.Enabled = false;
                    //((RJS.Web.WebControl.PopCalendar)item.FindControl("calDeliveryDT")).Enabled = false;
                    ((TextBox)item.FindControl("txtDeliveryInstruction")).Enabled = false;
                    ((TextBox)item.FindControl("txtDeliveryDate")).Enabled = false;
                    ((ImageButton)item.FindControl("btnSelectItems")).Enabled = false;
                    ((ImageButton)item.FindControl("btnSelectItems")).ImageUrl = "~/Purchase/Image/spacer.png";
                    ((RadioButtonList)item.FindControl("optGRowRFQType")).Enabled = false;
                    UserControl_ctlPortList stl = (UserControl_ctlPortList)item.FindControl("DDLPort");
                    ListBox lst = (ListBox)stl.FindControl("lstPortList");
                    lst.Enabled = false;
                   
                }
            }
        }

    }

    protected void lmgSearchCode_Click(object sender, EventArgs e)
    {
        GridHeaderItem ghItem = (GridHeaderItem)(grvSupplier.MasterTableView.GetItems(GridItemType.Header)[0]);
        TextBox txtBox = (TextBox)(ghItem.FindControl("txtCodeNumber"));
        string str = txtBox.Text;
    }

   /// <summary>
    /// JIT- 12171 Checking whether email body exist or not.
   /// </summary>
   /// <param name="dsEmailInfo"></param>
   /// <param name="strSuppCode"></param>
   /// <param name="strServerIPAdd"></param>
   /// <param name="Attachment"></param>
   /// <param name="bIsListed"></param>
   /// <param name="RFQType"></param>
   /// <param name="IsInsert_Mail"></param>
    protected void SendEmailToSupplier(DataSet dsEmailInfo, string strSuppCode, string strServerIPAdd, string Attachment, bool bIsListed, string RFQType, bool IsInsert_Mail)
    {
        try
        {
            string UploadFilePath = ConfigurationManager.AppSettings["PURC_UPLOAD_PATH"];
            DataTable dtSuppDetails = new DataTable();
            string strFormatSubject = "", strFormatBody = "", sToEmailAddress = "";
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

            DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));
            string strEmailAddCc = dtUser.Rows[0]["MailID"].ToString();

            int value = Int32.Parse(RFQType);
            BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
            int MailID = 0;
            String URL = "";

            switch (value)
            {
                case 1:  // Excel Based RFQ

                    FormateEmail(dsEmailInfo, true, strServerIPAdd, out sToEmailAddress, out strFormatSubject, out strFormatBody, bIsListed, Attachment);

                    MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, strFormatBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");

                    //string uploadpath = @"\\server01\uploads\Purchase";
                    string uploadpath = @"uploads\Purchase";
                    //if (IsInsert_Mail)
                    //    BLL_PURC_Common.Insert_Mail(Int32.Parse(RetValID), Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()), MailID);

                    BLL_Infra_Common.Insert_EmailAttachedFile(MailID, FileName, uploadpath + @"\" + FileName);

                    if (dsEmailInfo.Tables[2].Rows.Count > 0)
                    {
                        URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);
                    }
                    else
                    {
                        String msg = String.Format("alert('Please configure Email Template.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                           
                    }


                    break;

                case 2:  // Web Based RFQ

                    FormateEmail(dsEmailInfo, false, strServerIPAdd, out sToEmailAddress, out strFormatSubject, out strFormatBody, bIsListed, "");

                    MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, strFormatBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");

                    //if (IsInsert_Mail)
                    //    BLL_PURC_Common.Insert_Mail(Int32.Parse(RetValID), Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()), MailID);

                    
                    if (dsEmailInfo.Tables[2].Rows.Count > 0)
                    {
                    URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);
                    }
                    else
                    {
                        String msg = String.Format("alert('Please configure Email Template.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    }

                    break;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            lblrfqmessage.Text = ex.Message;
            throw ex;
        }

    }
    /// <summary>
    /// This function is for formating mail for RFQ
    /// JIT-11845
    /// Add vessel owner name and vessel owner address as per JIT
    /// Change signature of email template (Add Company Name)
    /// Whole email body is configurable 
    /// JIT- 12171 Checking whether email body exist or not.
    /// </summary>
    /// <param name="dsEmailInfo"></param>
    /// <param name="IsExcelBaseRFQ"></param>
    /// <param name="strServerIPAdd"></param>
    /// <param name="sEmailAddress"></param>
    /// <param name="strSubject"></param>
    /// <param name="strBody"></param>
    /// <param name="bIsListed"></param>
    /// <param name="Attachment"></param>
    protected void FormateEmail(DataSet dsEmailInfo, bool IsExcelBaseRFQ, string strServerIPAdd, out string sEmailAddress, out string strSubject, out string strBody, bool bIsListed, string Attachment)
    {
        BLL_PURC_Purchase objpurch = new BLL_PURC_Purchase();
        string Legalterm = objpurch.Get_LegalTerm(282);
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        StringBuilder sbBody = new StringBuilder();
        sEmailAddress = "";
        strSubject = "";
        strBody = "";
        string webExl = "";
        string strfeature = "";
        string TemplateText = "";
        string TemplateSubject = "";
            try
            {
                if (dsEmailInfo.Tables[2].Rows.Count > 0)
                {
                    TemplateText = dsEmailInfo.Tables[2].Rows[0]["Email_Body"].ToString();
                    TemplateSubject = dsEmailInfo.Tables[2].Rows[0]["Email_Subject"].ToString();
                    if (TemplateText.ToString().Trim() == null || TemplateText.ToString().Trim() == "")
                    {
                        TemplateText = "Please configure Email template.";
                    }
                    if (TemplateSubject.ToString().Trim() == null || TemplateSubject.ToString().Trim() == "")
                    {
                        TemplateSubject = "Please configure Subject template.";
                    }
                }
                else
                {
                    TemplateText = "Please configure Email template.";
                    TemplateSubject = "Please configure Subject template.";
                }
        //Need filter Line type ='Q' because it's in RFQ stage.
        dsEmailInfo.Tables[0].DefaultView.RowFilter = "Line_type ='Q'";

        if (dsEmailInfo.Tables[0].Rows.Count > 0)
        {
            if (IsExcelBaseRFQ)  //  For Excel Based Quotation
            {

                webExl = " Kindly quote for the attached file : " + Attachment + "<br>";

            }
            else   //For Web Based Quotation.
            {

                webExl = @" Kindly quote by clicking on the below link :<br>
                  <a href=" + strServerIPAdd.Trim() + ">" + strServerIPAdd.Trim() + @"</a> <br>
                    User Name&nbsp;:  " + dsEmailInfo.Tables[0].Rows[0]["User_name"].ToString() + @"<br> 
                    Password&nbsp;&nbsp; :  " + dsEmailInfo.Tables[0].Rows[0]["Password"].ToString() + @"<br><br>  "
                          ;
                /* As Discussed with Maneesh/Ramesh 16062015:18:06 JIT_2717
                strfeature = @"<span style='color:Red'> “ANNOUNCEMENT OF NEW POLICY” </span> <br>
                                    With effect from 12th Feb 2013, It is mandate to obtain only Master/ Chief Engineer’s signature and  vessel stamp on any “DELIVERY NOTE” for orders delivered on board.  <br>
                                    Stores Suppliers - delivering to third party/forwarder must obtain the vessel signed Delivery Note from the c/o parties. <br>
                                    Supplier confirms that XT will not pay for any delivers that are not confirmed in accordance with the above condition. <br><br>
                                    <hr/>";
                */
                strfeature = "<hr/>";
            }
            

            TemplateText = TemplateText.Replace("&nbsp;", "&#32;");
            TemplateText = TemplateText.Replace(" &amp; ", " &#38; ");
            TemplateText = TemplateText.Replace(" & ", " and ");
            TemplateText = TemplateText.Replace("&rsquo;", "&#39;");
            TemplateText = TemplateText.Replace("&ldquo;", "&#34;");
            TemplateText = TemplateText.Replace("&rdquo;", "&#34;");
            TemplateText = TemplateText.Replace("&hellip;", "&#46;");
            TemplateText = TemplateText.Replace("&ndash;", "&#150;");
            
            TemplateText = TemplateText.Replace("(SHORT_NAME)", dsEmailInfo.Tables[0].DefaultView[0]["SHORT_NAME"].ToString());
            TemplateText = TemplateText.Replace("(WEBEXL)", webExl);
            TemplateText = TemplateText.Replace("(VESSEL_NAME)", dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString());
            TemplateText = TemplateText.Replace("(DELIVERY_PORT)", dsEmailInfo.Tables[0].DefaultView[0]["DELIVERY_PORT"].ToString());
            TemplateText = TemplateText.Replace("(BUYER_COMMENTS)", dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString());
            TemplateText = TemplateText.Replace("(QUOTATION_DUE_DATE)", txtfrom.Text.ToString());
            //TemplateText = TemplateText.Replace("strfeature", strfeature.ToString());
            TemplateText = TemplateText.Replace("(VESSEL_OWNER_NAME)", dsEmailInfo.Tables[0].DefaultView[0]["Owner_Name"].ToString());
            TemplateText = TemplateText.Replace("(VESSEL_OWNER_ADDRESS)", dsEmailInfo.Tables[0].DefaultView[0]["Owner_Address"].ToString());
            TemplateText = TemplateText.Replace("(LEGAL_TERMS)", Legalterm.Replace("\n", "<br>"));
            TemplateText = TemplateText.Replace("(COMPANY_NAME)", GetSessionCompanyName());

            TemplateText = TemplateText.Replace("(USER_NAME)", dsEmailInfo.Tables[1].Rows[0]["UserName"].ToString());
            TemplateText = TemplateText.Replace("(USER_EMAIL)", dsEmailInfo.Tables[1].Rows[0]["UserEmail"].ToString());
            TemplateText = TemplateText.Replace("(USER_DESIGNATION)", dsEmailInfo.Tables[1].Rows[0]["User_Designation"].ToString());
            TemplateText = TemplateText.Replace("(USER_PHONE)", dsEmailInfo.Tables[1].Rows[0]["User_MobileNo"].ToString());
            TemplateText = TemplateText.Replace("(USER_ADDRESS)", dsEmailInfo.Tables[1].Rows[0]["Address"].ToString());
            TemplateText = TemplateText.Replace("(COMPANY_ADDRESS)", dsEmailInfo.Tables[1].Rows[0]["Address"].ToString());

            TemplateSubject = TemplateSubject.Replace("&nbsp;", "&#32;");
            TemplateSubject = TemplateSubject.Replace(" &amp; ", " &#38; ");
            TemplateSubject = TemplateSubject.Replace(" & ", " and ");
            TemplateSubject = TemplateSubject.Replace("&rsquo;", "&#39;");
            TemplateSubject = TemplateSubject.Replace("&ldquo;", "&#34;");
            TemplateSubject = TemplateSubject.Replace("&rdquo;", "&#34;");
            TemplateSubject = TemplateSubject.Replace("&hellip;", "&#46;");
            TemplateSubject = TemplateSubject.Replace("&ndash;", "&#150;");
            TemplateSubject = TemplateSubject.Replace("(REQUISITION_CODE)", dsEmailInfo.Tables[0].DefaultView[0]["REQUISITION_CODE"].ToString());
            TemplateSubject = TemplateSubject.Replace("(COMPANY_NAME)", GetSessionCompanyName());
            TemplateSubject = TemplateSubject.Replace("(VESSEL_NAME)", dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString());
            TemplateSubject = TemplateSubject.Replace("(DATE)", txtfrom.Text.ToString());

            //strSubject = "RFQ No. " + dsEmailInfo.Tables[0].DefaultView[0]["REQUISITION_CODE"].ToString() + " from " + Session["Company_Name_GL"] + "  for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + ",  Date : " + txtfrom.Text;

            strSubject = TemplateSubject;
            strBody = TemplateText;

            //To Get Email Address of the supplier 
            sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();


        }
        //else
        //{
        //    String msg = String.Format("alert('Can't change port of already sent RFQ.');");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        //    lblErrorMsg.Text = "Can't change port of already sent RFQ";
   
        //}
            }
            catch (Exception ex)
            {
                //throw ex;
                UDFLib.WriteExceptionLog(ex);
                throw ex;
            }

    }
    private string GetSessionCompanyName()
    {
        if (Session["Company_Name_GL"] != null)
            return Session["Company_Name_GL"].ToString();
        else
            return null;
    }
    protected string iSuplierId()
    {
        string strID = "";
        //if (Session["lstId"].ToString() != "")
        //{
        //    strID = Session["lstId"].ToString() + ",";
        //}

        for (int i = 0; i < lstsupplier.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)lstsupplier.Items[i].FindControl("chk");
            if (chk.Checked == true)
            {
                strID += "'" + ((Label)lstsupplier.Items[i].FindControl("lblSupplierCode")).Text + "'" + ",";
            }
        }

        return strID;
    }

    protected void btnSelect_Click(object s, EventArgs e)
    {
        Button btnSupplier = (Button)s;

        // save the delivery port ,delivery date ,remark and rfq option for newly added supplier
       
        Session["Supplier"] = null;
        DataTable myTable = new DataTable();
        myTable = (DataTable)(ViewState["SelectedSupplier"]);

        int rowid = 0;
        foreach (GridDataItem gr in grvSupplier.Items)
        {
            if (gr["Requisition_Code"].Text == "New")
            {
                myTable.Rows[rowid]["DELIVERY_PORT"] = ((UserControl_ctlPortList)gr.FindControl("DDLPort")).SelectedValue;
                myTable.Rows[rowid]["DELIVERY_DATE"] = ((TextBox)gr.FindControl("txtDeliveryDate")).Text;
                myTable.Rows[rowid]["Delivery_Instructions"] = ((TextBox)gr.FindControl("txtDeliveryInstruction")).Text;
                myTable.Rows[rowid]["Supplier_Property"] = ((DropDownList)gr.FindControl("ddlProperty")).SelectedValue;
            }

            rowid++;
        }



        if (btnSupplier.CommandArgument != "")
        {

            try
            {
                string filter = "";



                int rowcount = myTable.Rows.Count;
                DataRow dr = myTable.NewRow();
                string[] arg = btnSupplier.CommandArgument.ToString().Split(',');
                dr["SUPPLIER"] = arg[0];
                dr["SUPPLIER_NAME"] = btnSupplier.ToolTip;
                dr["COUNTRY"] = btnSupplier.ValidationGroup;
                dr["Date"] = DateTime.Now.ToString("dd/MM/yyyy");
                dr["Requisition_Code"] = "New";
                dr["DELIVERY_DATE"] = ViewState["dldate"].ToString();
                dr["Supplier_Property"] = arg[1];
                //if (ViewState["Supplier_Property"].ToString() != "No")
                //{
                //  dr["Supplier_Property"] = ViewState["Supplier_Property"].ToString();
                //}

                if (ViewState["dlport"].ToString().Trim() != "")
                {
                    dr["DELIVERY_PORT"] = ViewState["dlport"].ToString();
                }
                dr["RowID"] = (rowcount + 1);

                myTable.Rows.Add(dr);
                //}
                if (ViewState["Supplier"].ToString() == "0")
                {
                    ViewState["Supplier"] = btnSupplier.CommandArgument;
                }
                else
                {
                    ViewState["Supplier"] = ViewState["Supplier"] + "," + btnSupplier.CommandArgument;
                }
                Session["Supplier"] = ViewState["Supplier"].ToString();
                //}
                myTable.AcceptChanges();
                myTable.DefaultView.RowFilter = "1=1";
                grvSupplier.DataSource = myTable;
                grvSupplier.DataBind();
               
               
                foreach (GridDataItem gr in grvSupplier.Items)
                {
                    DropDownList ddlProperty = (DropDownList)gr.FindControl("ddlProperty");
                    Label lblSupplierProperty = (Label)gr.FindControl("lblSupplierProperty");
                    Label lblApplicable_Flag = (Label)gr.FindControl("lblApplicable");
                    if (lblApplicable_Flag.Text.ToString() == "Yes")
                    {
                        ddlProperty.SelectedValue = "Yes";
                    }
                    else if (lblApplicable_Flag.Text.ToString() == "No")
                    {
                        ddlProperty.SelectedValue = "No";
                    }
                    else if (lblSupplierProperty.Text.ToString() == "Yes")
                        {
                            ddlProperty.SelectedValue = "Yes";
                        }
                        else
                        {
                            ddlProperty.SelectedValue = "No";
                        }
                   
          
                }
                txtdeldate.Text = ViewState["dldate"].ToString();
                updDelv.Update();
                //Session["lstId"] = supplierId;
                ViewState["SelectedSupplier"] = myTable;
                
                CheckSendItem();
                updgrvSupplier.Update();


            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        updgrvSupplier.Update();
    }

    protected void filterlistbox()
    {
        string filter = "";
        if ((txtCity.Text != string.Empty) || (txtcode.Text != string.Empty) || (txtSupname.Text != string.Empty) || (ddlCountry.SelectedIndex != 0) || (ddlSuppcategory.SelectedValue != "0"))
        {
            if (txtCity.Text != string.Empty)
            {
                if (filter.Length > 1)
                {
                    filter = filter + " and City like '" + "%" + txtCity.Text.Trim() + "%'";
                }
                else
                {
                    filter = filter + "  City like '" + "%" + txtCity.Text.Trim() + "%'";
                }
            }
            if (txtcode.Text != string.Empty)
            {
                if (filter.Length > 1)
                {
                    filter = filter + " and SUPPLIER like '" + "%" + txtcode.Text.Trim() + "%'";
                }
                else
                {
                    filter = filter + "  SUPPLIER like '" + "%" + txtcode.Text.Trim() + "%'";

                }
            }
            if (txtSupname.Text != string.Empty)
            {
                if (filter.Length > 1)
                {
                    filter = filter + " and SUPPLIER_NAME like '" + "%" + txtSupname.Text.Trim() + "%'";
                }
                else
                {
                    filter = filter + "  SUPPLIER_NAME like '" + "%" + txtSupname.Text.Trim() + "%'";
                }
            }
            if (ddlCountry.SelectedIndex != 0)
            {
                if (filter.Length > 1)
                {
                    filter = filter + " and COUNTRY like '" + ddlCountry.SelectedItem.Text + "%'";
                }
                else
                {
                    filter = filter + " COUNTRY like '" + ddlCountry.SelectedItem.Text + "%'";
                }
            }
            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    string lCountry = "";
                    if (ddlCountry.SelectedValue.ToString() != "0" && ddlCountry.SelectedIndex != -1)
                    {
                        lCountry = ddlCountry.SelectedValue.ToString();
                    }
                    DataSet ds = new DataSet();
                    int rowcount = ucCustomPagerItems.isCountRecord;
                    if (ddlSuppcategory.SelectedValue == "0")
                        ds = objTechService.SelectSupplier_Filter(null, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, txtCity.Text.Trim(), txtSupname.Text.Trim(), lCountry);
                    else
                        ds = objTechService.SelectSupplier_Filter(ddlSuppcategory.SelectedValue, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, txtCity.Text.Trim(), txtSupname.Text.Trim(), lCountry);
                    if (ucCustomPagerItems.isCountRecord == 1)
                    {
                        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                        // ucCustomPagerItems.CountTotalRec = ds.Tables[1].Rows[0]["RowCount"].ToString();
                        ucCustomPagerItems.BuildPager();
                    }
                    DataTable dt = ds.Tables[0];
                    dt.DefaultView.RowFilter = filter;

                    if (dt.DefaultView.Count > 0)
                    {
                        lstsupplier.DataSource = dt.DefaultView;
                    }
                    else
                    {
                        lstsupplier.DataSource = dt.DefaultView;
                        lblDataErr.Text = "No records found.";
                    }
                    lstsupplier.DataBind();

                }
            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
        }
        else
        {
            bindlistbox();
        }
    }

    protected void btnimg_Click(object sender, EventArgs e)
    {
        filterlistbox();
    }

    protected void btnDELSave_Click(object s, EventArgs e)
    {
        foreach (GridItem gr in grvSupplier.Items)
        {

            CheckBox chk1 = (CheckBox)(gr.FindControl("chkExportToExcel") as CheckBox);
            //Label lblsupplier = (Label)(gr.FindControl("DDlSupplier") as System.Web.UI.WebControls.DropDownList);

            if (chk1.Checked)
            {
                ((TextBox)gr.FindControl("txtDeliveryInstruction")).Text = txtdelivery.Text;

                ((TextBox)gr.FindControl("txtDeliveryDate")).Text = txtdeldate.Text;
                ((UserControl_ctlPortList)gr.FindControl("DDLPort")).SelectedValue = DDLPortAll.SelectedValue;

            }
        }
    }

  
    protected void imgbtnDelete_Click(object s, EventArgs e)
    {
        GridDataItem item = (GridDataItem)((ImageButton)s).Parent.Parent;
        DataTable dtsupp = (DataTable)ViewState["SelectedSupplier"];


        int rowid = 0;
        foreach (GridDataItem gr in grvSupplier.Items)
        {
            if (gr["Requisition_Code"].Text == "New")
            {
                dtsupp.Rows[rowid]["DELIVERY_PORT"] = ((UserControl_ctlPortList)gr.FindControl("DDLPort")).SelectedValue;
                dtsupp.Rows[rowid]["DELIVERY_DATE"] = ((TextBox)gr.FindControl("txtDeliveryDate")).Text;
                dtsupp.Rows[rowid]["Delivery_Instructions"] = ((TextBox)gr.FindControl("txtDeliveryInstruction")).Text;

            }

            rowid++;
        }



        dtsupp.Rows.RemoveAt(item.ItemIndex);
        dtsupp.AcceptChanges();
        grvSupplier.DataSource = dtsupp;
        grvSupplier.DataBind();



        //Session["lstId"] = supplierId;
        ViewState["SelectedSupplier"] = dtsupp;
        CheckSendItem();

    }

    protected void btnselectedItemsSave_Click(object sender, EventArgs e)
    {
        Dictionary<string, ArrayList> dicItemRefCode = new Dictionary<string, ArrayList>();
        dicItemRefCode = (Dictionary<string, ArrayList>)ViewState["DicItemRefCode"];

        if (dicItemRefCode == null)
            dicItemRefCode = new Dictionary<string, ArrayList>();

        ArrayList ArrListItemRefCode = new ArrayList();

        foreach (GridViewRow gr in gvReqsnItems.Rows)
        {
            string itemRefCode = ((Label)gr.FindControl("lblItemRefCode")).Text.Trim();

            if (itemRefCode != "" && ((CheckBox)gr.FindControl("chkitems")).Checked == true)
            {

                ArrListItemRefCode.Add(itemRefCode);

            }
            else // for  de selcted 
            {
                ((CheckBox)gr.FindControl("chkitems")).Checked = true; //set true because all items should be selected by default
            }
        }

        if (dicItemRefCode.Keys.Contains(hdfSuppRowID.Value.Trim()))
        {
            dicItemRefCode.Remove(hdfSuppRowID.Value.Trim());
        }
        dicItemRefCode.Add(hdfSuppRowID.Value.Trim(), ArrListItemRefCode);


        ViewState["DicItemRefCode"] = dicItemRefCode;

    }

    protected void btnselectedItemsClose_Click(object sender, EventArgs e)
    {

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
        }
    }

    protected void btnSelectItems_Click(object s, EventArgs e)
    {
        foreach (GridViewRow gr in gvReqsnItems.Rows)
        {
            ((CheckBox)gr.FindControl("chkitems")).Checked = true;
        }


        ImageButton btnitems = (ImageButton)s;

        hdfSuppRowID.Value = btnitems.CommandArgument;
        lblSupplierName.Text = btnitems.ToolTip;
        Dictionary<string, ArrayList> dicItems = (Dictionary<string, ArrayList>)ViewState["DicItemRefCode"];
        if (dicItems != null)
        {
            if (dicItems.Keys.Contains(btnitems.CommandArgument))
            {
                ArrayList arrItemRefCode = dicItems[btnitems.CommandArgument];

                foreach (GridViewRow gr in gvReqsnItems.Rows)
                {

                    if (!arrItemRefCode.Contains(((Label)gr.FindControl("lblItemRefCode")).Text))
                    {
                        ((CheckBox)gr.FindControl("chkitems")).Checked = false;
                    }
                }

            }
        }

        dvreqsnItems.Visible = true;
        updReqsnitems.Update();
    }

    protected void btnSendToNonApprovedSupp_Click(object s, EventArgs e)
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet dsRFQ = objTechService.GetDataToGenerateRFQ("0", Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());

        WriteExcell(dsRFQ, Request.QueryString["Requisitioncode"].ToString());



    }

    protected void btnSendNewRFQ_Click(object s, EventArgs e)
    {


    }


    protected void btnWebRFQ_Click(object s, EventArgs e)
    {
        /*--------Web Base RFQ-------------------- */
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            string RFQType = "2"; //Web Based RFQ
            ImageButton suppCode = (s as ImageButton);
            GridDataItem grv = (GridDataItem)suppCode.Parent.Parent;

            string SuppName = grv["SUPPLIER_NAME"].Text.ToString();
            string SuppCode = grv["SUPPLIER"].Text.ToString();
            string QUOTATION_CODE = grv["QUOTATION_CODE"].Text;
            string sBuyerRemaks = txtRFQRemarks.Text.Trim();
            string QuotDueDate = txtfrom.Text != "" ? txtfrom.Text.Substring(6, 4) + "/" + txtfrom.Text.Substring(3, 2) + "/" + txtfrom.Text.Substring(0, 2) : "Null";
            //string RowIDSuppCode = grv["RowID"].Text.Trim() + ":" + SuppCode;


            string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();

            DataSet dsSendMailInfo = objTechService.GetRFQsuppInfoSendEmail(SuppCode, QUOTATION_CODE, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["userid"].ToString());
            // objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "RFQ", " ", Convert.ToInt32(Session["USERID"]));

            SendEmailToSupplier(dsSendMailInfo, SuppCode, ServerIPAdd, "", true, RFQType, false);
        }
        catch (Exception ex)
        {
            lblrfqmessage.Text = ex.Message;
        }

    }

    protected void btnExcelRFQ_Click(object s, EventArgs e)
    {
        /*--------Excel Base RFQ-------------------- */

        try
        {
            string RFQType = "1"; //Excel Based RFQ
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            string strPath1 = Server.MapPath(".") + "\\SendRFQ\\";
            string FilePath1 = Server.MapPath("~") + "\\Purchase\\ExcelFile\\";


            ImageButton suppCode = (s as ImageButton);
            PO_RFQ_Generate.ExceldataImport objExcelRFQ = new PO_RFQ_Generate.ExceldataImport();

            GridDataItem grv = (GridDataItem)suppCode.Parent.Parent;


            string SuppName = grv["SUPPLIER_NAME"].Text.ToString();
            string SuppCode = grv["SUPPLIER"].Text.ToString();
            string QtnCode = grv["QUOTATION_CODE"].Text;
            //string RowIDSuppCode = grv["RowID"].Text.Trim() + ":" + SuppCode;
            string QuotDueDate = txtfrom.Text != "" ? txtfrom.Text.Substring(6, 4) + "/" + txtfrom.Text.Substring(3, 2) + "/" + txtfrom.Text.Substring(0, 2) : "Null";
            string sBuyerRemaks = txtRFQRemarks.Text.Trim();

            string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
            DataSet dsRFQ = objTechService.GetDataToGenerateRFQ(SuppCode, Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), QtnCode);
            DataSet dsSendMailInfo = objTechService.GetRFQsuppInfoSendEmail(SuppCode, QtnCode, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["userid"].ToString());

            // +DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "" + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") 

            FileName = "RFQ_" + Request.QueryString["Vessel_Code"].ToString() + "_" + Request.QueryString["Requisitioncode"].ToString() + "_" + SuppCode + "_" + ReplaceSpecialCharacterinFileName(SuppName) + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "" + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + ((UserControl_ctlPortList)grv.FindControl("DDLPort")).SelectedText.ToString() + ".xls";
            objExcelRFQ.WriteExcell(dsRFQ, Request.QueryString["Requisitioncode"].ToString(), FileName, strPath1, FilePath1);

            objTechService.SaveAttachedFileInfo(Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Requisitioncode"].ToString(), SuppCode, ".xls", FileName.Replace(".xls", ""), "SendRFQ/" + FileName, Session["userid"].ToString(), UDFLib.ConvertToInteger(((UserControl_ctlPortList)grv.FindControl("DDLPort")).SelectedValue.ToString()));
            // objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "RFQ", " ", Convert.ToInt32(Session["USERID"]));

            SendEmailToSupplier(dsSendMailInfo, SuppCode, ServerIPAdd, FileName, true, RFQType, false);
        }
        catch (Exception ex)
        {
            lblrfqmessage.Text = ex.Message;
        }

    }
    Hashtable myHashtable;
    public void WriteExcell(DataSet ds, string Requisition)
    {
        CheckExcellProcesses();

        object Opt = Type.Missing;
        Microsoft.Office.Interop.Excel.Application ExlApp;

        string FilePath = Server.MapPath("~") + "\\Purchase\\ExcelFile\\";
        string path = FilePath + @"RFQ_FormatFile.xls";


        ExlApp = new Microsoft.Office.Interop.Excel.Application();
        ExlApp.DisplayAlerts = false;
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



            ExlWrkSheet.Cells[10, 3] = ds.Tables[4].Rows[0]["Dept_Name"].ToString();
            ExlWrkSheet.Cells[1, 3] = ds.Tables[1].Rows[0]["Vessel_name"].ToString();

            if (ds.Tables[3].Rows.Count > 0)
            {
                ExlWrkSheet.Cells[10, 7] = ds.Tables[3].Rows[0]["MechInfo"].ToString();
                ExlWrkSheet.Cells[11, 7] = ds.Tables[3].Rows[0]["Model_Type"].ToString();

                ExlWrkSheet.Cells[12, 7] = ds.Tables[3].Rows[0]["MakerName"].ToString()

                                            + ' ' + ds.Tables[3].Rows[0]["MakerAddress"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["MakerCity"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["MakerEmail"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["MakerCONTACT"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["MakerPhone"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["MakerFax"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["MakerTELEX"].ToString()
                                            + ' ' + ds.Tables[3].Rows[0]["System_Serial_Number"].ToString();

            }

            int i = 15;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ExlWrkSheet.Cells[i, 1] = dr[0].ToString();

                ExlWrkSheet.Cells[i, 2] = dr[10].ToString();
                ExlWrkSheet.Cells[i, 3] = dr[9].ToString();

                ExlWrkSheet.Cells[i, 4] = dr[1].ToString();
                ExlWrkSheet.Cells[i, 5] = dr[4].ToString();
                ExlWrkSheet.Cells[i + 1, 5] = dr[5].ToString();
                ExlWrkSheet.Cells[i + 2, 5] = dr[8].ToString();
                ExlWrkSheet.Cells[i, 6] = dr[6].ToString();
                ExlWrkSheet.Cells[i, 7] = dr[7].ToString();

                i = i + 3;

            }


            ExlWrkSheet.get_Range("A" + (ds.Tables[0].Rows.Count * 3 + 15).ToString(), "N1639").Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);
            ExlWrkSheet.Cells[ds.Tables[0].Rows.Count * 3 + 15, 1] = ds.Tables[2].Rows[0]["LegalTerm"].ToString();

            ExlWrkSheet.get_Range("G9", "G9").NumberFormat = "#0.00";

            ExlWrkSheet.get_Range("M1", "M10").EntireColumn.Hidden = true;

            string RFQFileName = "RFQ_" + Requisition.Replace('-', '_') + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "" + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + ".xls";
            ExlWrkBook.SaveAs(Server.MapPath("~/uploads/purchase/") + RFQFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            ResponseHelper.Redirect("~/uploads/purchase/" + RFQFileName, "blanck", "");

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ExlWrkBook.Close(null, null, null);
            ExlApp.Workbooks.Close();
            ExlApp.Quit();
            KillExcel();


        }



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

    public string ReplaceSpecialCharacterinFileName(string strFileName)
    {
        return strFileName.Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace("?", "_").Replace("*", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace(":", "_").Trim();
    }

}

