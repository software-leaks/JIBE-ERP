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
//using System.Xml.Linq;
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using PMSReports;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.ServiceModel;
using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class Technical_INV_ApprovedPurchaseOrder : System.Web.UI.Page
{
    string TypeOpen = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            btnSendPO.Attributes.Add("onclick", "Async_Get_Reqsn_Validity('" + Request.QueryString["Requisitioncode"] + "');return Validation();");
            //Session["VESSELCODE"] = Request.QueryString["Vessel_Code"].ToString();
            //Session["DOCCODE"] = Request.QueryString["Document_Code"].ToString();
            TypeOpen = Request.QueryString["Type"].ToString();

            BindGrid();
            BindRequisitionInfo();

            divApprove.Visible = false;
            divpurcomment.Visible = false;
            BLL_PURC_Purchase objport = new BLL_PURC_Purchase();
            DDLPort.DataSource = objport.getDeliveryPort();
            DDLPort.DataBind();
            DDLPort.Items.Insert(0, new ListItem("SELECT", "0"));
            BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
            gvPortCalls.DataSource = objCrew.Get_PortCall_List(Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()));
            gvPortCalls.DataBind();
            DataTable dt = objport.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='A'";

            ddlSentFrom.DataTextField = "SUPPLIER_NAME";
            ddlSentFrom.DataValueField = "SUPPLIER";
            ddlSentFrom.DataSource = dt.DefaultView.ToTable();
            ddlSentFrom.DataBind();
            ddlSentFrom.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        //hlinkViewEval.NavigateUrl = "Quotation_Evaluation_Report.aspx?Requisitioncode=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"];
        hlinkViewEval.NavigateUrl = "QuotationEvalRpt.aspx?Requisitioncode=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"];

    }

    //DataTable RFQDt;

    private void BindGrid()
    {
        try
        {
            DataTable dtReqInfo = new DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                rgdSuppliers.DataSource = objTechService.SelectPOToSendSupplier(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString());

                rgdSuppliers.DataBind();

                rgdSuppliers.Columns[10].Visible = true;
                //  rgdSuppliers.Columns[14].Visible = false;
                btnSendOrder.Visible = true;

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

    protected void btnSendOrder_Click(object sender, EventArgs e)
    {
        try
        {
            bool IsPO = false;
            string DeliveryInstructions = "";
            string DeliveryPort = "";
            if (rgdSuppliers.MasterTableView.Items.Count > 0)
            {
                foreach (GridDataItem dataItem in rgdSuppliers.MasterTableView.Items)
                {
                    CheckBox chk = (CheckBox)(dataItem.FindControl("chkSendOrder") as CheckBox);
                    if ((chk.Checked))
                    {
                        DeliveryInstructions += dataItem.Cells[rgdSuppliers.MasterTableView.Items[0].Cells.Count - 2].Text + "        ";
                        if (DeliveryPort.ToString() == "")
                        {
                            DeliveryPort = dataItem.Cells[rgdSuppliers.MasterTableView.Items[0].Cells.Count - 1].Text;
                        }
                        IsPO = true;
                    }
                }
            }

            if (rgdSuppliers.MasterTableView.Items.Count > 0)
            {
                if (IsPO)
                {
                    divpurcomment.Visible = true;
                    txtdlvins.Text = "";//DeliveryInstructions;
                    txteta.Text = "";
                    txtremark.Text = "";
                    txtagent.Text = "";
                    txtetd.Text = "";
                    btnSendOrder.Enabled = false;
                    int selectedindex = DDLPort.SelectedIndex;
                    DDLPort.Items[selectedindex].Selected = false;
                    DDLPort.Items.FindByValue(DeliveryPort).Selected = true;
                }
                else
                {
                    String msg2 = String.Format("alert('Select alteast one quotation to raise the PO.')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                    //lblError.Text = "Select alteast one check box to raise the PO.";
                }
            }
            else
            {

                String msg3 = String.Format("alert('There is no quotation selected to Send the PO.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg3, true);
                //lblError.Text = "There is No data to Send the Purchase Order";
            }

        }
        catch { }
    }
    /// <summary>
    /// JIT-11845
    /// Add vessel owner name and vessel owner address as per JIT
    /// Change signature of email template (Add Company Name) 
    /// </summary>
    /// <param name="dsEmailInfo"></param>
    /// <param name="sEmailAddress"></param>
    /// <param name="strSubject"></param>
    /// <param name="strBody"></param>
    /// <param name="IsPendingApprovalPO"></param>
    /// <param name="OrderCode"></param>
    protected void FormateEmail(DataSet dsEmailInfo, out string sEmailAddress, out string strSubject, out string strBody, bool IsPendingApprovalPO, string OrderCode)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();

        strBody = @"Dear " + dsEmailInfo.Tables[0].DefaultView[0]["SHORT_NAME"].ToString() + @"<br><br>
                     We are pleased to attach our Purchase Order for delivery of the goods quoted per your Supplier reference:" + dsEmailInfo.Tables[0].DefaultView[0]["Supplier_Quotation_Reference"].ToString() + @"  for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + @".<br><br>
                    The purchase order can be downloaded from the following link   <br>
             <a href='" + ServerIPAdd.Trim() + "'>" + ServerIPAdd.Trim() + @"</a> <br>
                    User Name&nbsp;:  " + dsEmailInfo.Tables[0].Rows[0]["User_name"].ToString() + @"<br> 
                    Password&nbsp;&nbsp; :  " + dsEmailInfo.Tables[0].Rows[0]["Password"].ToString() + @" <br><br>
                   <span style='color:Red'> “ANNOUNCEMENT OF NEW POLICY” </span><br>   
                    With effect from 12th Feb 2013, It is mandate to obtain only Master/ Chief Engineer’s signature and  vessel stamp on any “DELIVERY NOTE” for orders delivered on board. <br>
                    Stores Suppliers - delivering to third party/forwarder must obtain the vessel signed Delivery Note from the c/o parties.<br>
                    Supplier confirms that " + Session["Company_Name_GL"] + @" will not pay for any delivers that are not confirmed in accordance with the above condition.

                   <br><br/> <b> KINDLY ACKNOWLEDGE OUR PO WITH DELIVERY DAYS / READINESS DATE VIA THE ABOVE LINK WITHIN SAME WORKING DAY.</b><br><br>

                    Please ensure accurate and complete delivery of this order to the agent, Forwarder or vessel. <br><br>
                    If there are partial deliveries, the sender of the Purchase Order we must be informed immediately via email.  <br><br>
                    Following packing and documentation procedures to adhere strictly; <br>
                    1 To pack in airworthy packing (strictly in carton box) <br>
                    2. Markings ON ALL PACKAGES: Address to vessel name :" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + @" and our purchase order number:" + dsEmailInfo.Tables[0].DefaultView[0]["ORDER_CODE"].ToString() + @" and vessel department____________ (eg. Engine Stores, Cabin Stores, Electrical stores, etc)<br>
                    3. Pls indicate the dim (LXBXH in CM), cargo weight, no.of pieces, value of goods on the manifest invoice and packing list or delivery note 
                    4 . Kindly enclose 02 copies of delivery note into the carton/crate for ship's checking <br>
                    5. Original Invoice and Original Delivery Note (with original company stamp and " + Session["Company_Name_GL"] + @" of receiving staff of our forwarder) to be send to " + Session["Company_Name_GL"] + @" by email in PDF format.<br><br>

                    Any inaccuracies, missing or damaged items will result in a delayed payment <br>
                    of your invoice. The PO number must be indicated in the invoice for <br>
                    prompt processing and  payment. <br><br>

                    IMPORTANT REMARK: All invoices must be issued as follows:<br>
                    M/V..<br>
                    " + dsEmailInfo.Tables[0].Rows[0]["Owner_Name"].ToString() + @"<br>
                    " + dsEmailInfo.Tables[0].Rows[0]["Owner_Address"].ToString().Replace("\n", "<br>") + @"<br><br>
                    Thank you for your co-operation.<br><br> 
                     " + Session["Company_Name_GL"].ToString() + @"<br>
                     PURCHASING DEPARTMENT
                                 
                    <br><br>
                    ";


        strSubject = "Purchase Order No. " + OrderCode + " from " + Session["Company_Name_GL"]  + " for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + " ,  Date :" + DateTime.Now.ToString();
        sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();
       

    }

    protected void GeneratePOAsPDF(DataTable dt, string strPath, string FileName)
    {

        string repFilePath = Server.MapPath("POReport.rpt");
        //using (ReportDocument repDoc = new ReportDocument())
        //{

        using (ReportDocument repDoc = new ReportDocument())
        {
            repDoc.Load(repFilePath);

            repDoc.SetDataSource(dt);

            if (rbtnIncludeAmount.Checked == true)
            {
                decimal Total_Price = 0;
                decimal Exchane_rate = 1;
                decimal Vat = 0;
                decimal sercharge = 0;
                decimal discount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    Exchane_rate = Convert.ToDecimal(dr["EXCHANGE_RATE"].ToString());
                    discount = Convert.ToDecimal(dr["DISCOUNT"].ToString());
                    Vat = Convert.ToDecimal(dr["VAT"].ToString());
                    sercharge = Convert.ToDecimal(dr["SURCHARGES"].ToString());
                    Total_Price = Total_Price + (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) - (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) * Convert.ToDecimal(dr["QUOTED_DISCOUNT"].ToString()) / 100));
                }
                repDoc.DataDefinition.FormulaFields[1].Text = (Total_Price / Exchane_rate).ToString();
                repDoc.DataDefinition.FormulaFields[2].Text = (Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) * discount / 100).ToString();
                repDoc.DataDefinition.FormulaFields[3].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - (Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) * discount / 100)) * sercharge / 100).ToString();
                repDoc.DataDefinition.FormulaFields[4].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[3].Text)) * Vat / 100).ToString();
                repDoc.DataDefinition.FormulaFields[0].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[3].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[4].Text)).ToString());
            }
            else
            {
                repDoc.DataDefinition.FormulaFields[1].Text = "";
                repDoc.DataDefinition.FormulaFields[2].Text = "";
                repDoc.DataDefinition.FormulaFields[3].Text = "";
                repDoc.DataDefinition.FormulaFields[4].Text = "";
                repDoc.DataDefinition.FormulaFields[0].Text = "";

            }
            ExportOptions exp = new ExportOptions();
            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

            string sFile = strPath + FileName;
            dk.DiskFileName = strPath + FileName;

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.PortableDocFormat;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);

            //for email attachment

            string destFile = Server.MapPath("../Uploads/Purchase") + "\\" + FileName; ;
            File.Copy(sFile, destFile, true);

            repDoc.Close();
            repDoc.Dispose();
        }
        //}

    }

    private DataTable BindItemsInHirarchy(string VesselCode, string strRequistionCode, string REQ_Supplier)
    {
        DataTable dtItems = new DataTable();
        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                dtItems = objTechService.SelectItemsHierarchyForPO(VesselCode, strRequistionCode, REQ_Supplier);


            }
            return dtItems;
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            return dtItems = null;
        }
        finally
        {

        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        //int i = 0;
        try
        {

            //foreach (GridDataItem dataItem in rgdSuppliers.MasterTableView.ItemsHierarchy)
            //{
            //    TextBox txtQty = (TextBox)(dataItem.FindControl("txtQty") as TextBox);
            //    TextBox txtsd = (TextBox)(dataItem.FindControl("txtQty") as TextBox);
            //    if (txtQty !=null)
            //    {
            //        txtQty.Enabled = true;
            //    }




            //} 

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {




        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

        }


    }


    protected void rgdSuppliers_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        divpurcomment.Visible = false;
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "Items":
                {
                    string REQUISITION_CODE = Request.QueryString["Requisitioncode"].ToString();
                    string REQ_Supplier = dataItem.GetDataKeyValue("SUPPLIER").ToString();
                    string VesselCode = Request.QueryString["Vessel_Code"].ToString();
                    // e.DetailTableView.DataSource = GetDataTable("SELECT DISTINCT itv.[ID],itv.[Part_Number],itv.[Short_Description],itv.[Unit_and_Packings], SITEM.[REQUESTED_QTY],SITEM.[ITEM_COMMENT], SITEM.REQUISITION_CODE,slib.SYSTEM_Description FROM dbo.PURC_Dtl_Supply_Items SITEM   INNER jOIN dbo.PMS_INV_Lib_Systems_Library Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE INNER jOIN  dbo.PMS_INV_Lib_Items itv on itv.[ID]=SITEM.[ITEM_REF_CODE] where SITEM.REQUISITION_CODE ='" + REQUISITION_CODE + "'");
                    if (e.DetailTableView.DataSourceID == "")
                    {
                        e.DetailTableView.DataSource = BindItemsInHirarchy(VesselCode, REQUISITION_CODE, REQ_Supplier);

                    }
                    break;
                }
            //rgdSuppliers.MasterTableView.DetailTables[0]  

        }
    }

    protected void onSelectPOPreview(object source, CommandEventArgs e)
    {

        string value = e.CommandArgument.ToString();
        string[] arInfo = new string[4];
        char[] splitter = { ',' };
        arInfo = value.Split(splitter);
        Session["RFQCODE"] = lblReqNo.Text;
        Session["QTCODE"] = arInfo[0].ToString();
        Session["SUPLCODE"] = arInfo[1].ToString();
        // Session["VESSELCODE"] = lblVessel.tex;

        ResponseHelper.Redirect("POPreview.aspx?RFQCODE=" + lblReqNo.Text + "&Quotation_code=" + arInfo[0].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString(), "_Blank", "");

    }

    protected void onSelectApprovePO(object source, CommandEventArgs e)
    {
        divApprove.Visible = true;
        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split(',');
        string QuotationCode = strArgs[0];
        string SupplierCode = strArgs[1];
        string ApproveStatus = strArgs[2];

        if (ApproveStatus == "YES")
        {
            String msg = String.Format("alert('PO has already been approved.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            divApprove.Visible = false;
        }


    }


    protected void RaisePromtTOManager(DataSet dsPoApprovalData)
    {
        string sToEmailAddress = "", strSubject = "", strEmailBody = "";
        string[] Attchment = new string[10];
        FormateEmail(dsPoApprovalData, out  sToEmailAddress, out  strSubject, out strEmailBody, true, "");
        //Send Email to selected supplier.
        // Email.SendEMailForPO("teamlead@sms.com", sToEmailAddress, "", "", strSubject, strEmailBody, Attchment);
        //Send Promt(Email) to LoginUser Manager(superior) IDs To Approve PO. 

    }

    protected void btnApprv_Click(object sender, EventArgs e)
    {

    }


    protected void SendPurchaseOrder()
    {

        // string[] Attchment = new string[10];
        lblError.Text = "";
        string strPath = Server.MapPath(".") + "\\SendPO\\";
        DataSet DsPO = new DataSet();
        DataSet dsSendMailInfo = new DataSet();

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        string emailIDcc = dtUser.Rows[0]["MailID"].ToString();
        bool IsPO = false;

        if (rgdSuppliers.MasterTableView.Items.Count > 0)
        {
            foreach (GridDataItem dataItem in rgdSuppliers.MasterTableView.Items)
            {
                CheckBox chk = (CheckBox)(dataItem.FindControl("chkSendOrder") as CheckBox);
                if ((chk.Checked))
                {
                    IsPO = true;
                }
            }
        }

        if (rgdSuppliers.MasterTableView.Items.Count > 0)
        {
            int i = 0;

            if (IsPO)
            {
                DataTable dtQuotationList = new DataTable();
                dtQuotationList.Columns.Add("Qtncode");
                dtQuotationList.Columns.Add("amount");

                string sDlvIns = txtdlvins.Text != "" ? txtdlvins.Text : DBNull.Value.ToString();
                string strDeliveryPort = DDLPort.SelectedItem.ToString() != "--Select--" ? DDLPort.SelectedValue.ToString() : DBNull.Value.ToString();
                string sEta = txteta.Text != "" ? txteta.Text + " " + txtETAAPPM.Text.ToString().Trim() + ":00" : Convert.ToString("01/01/1900");
                string sRemark = txtremark.Text != "" ? txtremark.Text : DBNull.Value.ToString();
                string sAgent = txtagent.Text != "" ? txtagent.Text : DBNull.Value.ToString();
                string sEtd = txtetd.Text != "" ? txtetd.Text + " " + txtETDAMPM.Text.ToString().Trim() + ":00" : Convert.ToString("01/01/1900");

                foreach (GridDataItem dataItem in rgdSuppliers.MasterTableView.Items)
                {
                    CheckBox chk = (CheckBox)(dataItem.FindControl("chkSendOrder") as CheckBox);

                    string chkAppStatus = dataItem["APPROVED_Status"].Text;



                    if ((chk.Checked) && dataItem["APPROVED_Status"].Text == "YES")
                    {
                        i++;
                        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                        {
                            DataRow drQTN = dtQuotationList.NewRow();
                            drQTN["Qtncode"] = dataItem["QUOTATION_CODE"].Text.ToString();
                            drQTN["amount"] = "0";
                            dtQuotationList.Rows.Add(drQTN);

                            string OrderCode = dataItem["ORDER_CODE"].Text;

                            int sts = objTechService.PMSUpdOtherPODetails(sDlvIns, strDeliveryPort, sEta, sRemark, sEtd, sAgent, Request.QueryString["Document_Code"].ToString(), Request.QueryString["Requisitioncode"].ToString(), Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()), dataItem["QUOTATION_CODE"].Text.ToString(), dataItem["SUPPLIER"].Text.ToString(), Convert.ToInt32(Session["USERID"].ToString()));

                            DsPO = objTechService.GetDataToGeneratPO(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), dataItem["SUPPLIER"].Text.ToString(), dataItem["QUOTATION_CODE"].Text, Request.QueryString["Document_Code"].ToString());

                            dsSendMailInfo = objTechService.GetRFQsuppInfoSendEmail(dataItem["SUPPLIER"].Text.ToString(), dataItem["QUOTATION_CODE"].Text, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["userid"].ToString());
                            string FileName = "PO_" + Request.QueryString["Vessel_Code"].ToString() + "_" + OrderCode + "_" + dataItem["SUPPLIER"].Text.ToString() + ReplaceSpecialCharacterinFileName(dataItem["SHORT_NAME"].Text) + DateTime.Now.ToString("yyMMddss") + ".pdf";
                            string sToEmailAddress = "", strSubject = "", strEmailBody = "";
                            //Generate the PDF file and check the include amount status


                            DataTable dtPO = DsPO.Tables[0];

                            if (rbtnIncludeAmount.Checked == false)
                            {
                                int ipo = 0;
                                foreach (DataRow dr in dtPO.Rows)
                                {
                                    dtPO.Rows[ipo]["currency"] = "";
                                    dtPO.Rows[ipo]["exchange_rate"] = 0;
                                    dtPO.Rows[ipo]["quoted_rate"] = 0;

                                    dtPO.AcceptChanges();
                                    ipo++;
                                }

                            }



                            GeneratePOAsPDF(dtPO, strPath, FileName);
                            //Attchment.SetValue(strPath + FileName, 0);
                            // objTechService.ChangeSupplierPOStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString());
                            //Format Email body.

                            FormateEmail(dsSendMailInfo, out  sToEmailAddress, out  strSubject, out strEmailBody, false, OrderCode);
                            //Send Email to selected supplier.
                            // // Email.SendEMailForPO("teamlead@sms.com", sToEmailAddress, "", "", strSubject, strEmailBody, Attchment);

                            BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
                            int MailID = 0;
                            MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, emailIDcc, "", strSubject, strEmailBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");


                            string UploadFilePath = ConfigurationManager.AppSettings["PURC_UPLOAD_PATH"];
                            //string uploadpath = @"\\server01\uploads\Purchase";
                            string uploadpath = @"uploads\Purchase";
                            BLL_Infra_Common.Insert_EmailAttachedFile(MailID, FileName, uploadpath + @"\" + FileName);



                            string URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH="+UploadFilePath+"');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);

                            //   ResponseHelper.Redirect("../crew/EmailEditor.aspx?ID=" + MailID.ToString(), "blank", "");

                        }

                    }
                    else if ((chk.Checked) && dataItem["APPROVED_Status"].Text == "NO")
                    {
                        String msg = String.Format("alert('Purchase order can not be raise because PO has not been approved.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    }
                }

                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    //Update the requistion Stage Status
                    objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "SCN", sRemark, Convert.ToInt32(Session["userid"]),dtQuotationList);

                }

                if (i == rgdSuppliers.MasterTableView.Items.Count)
                {
                    String msg1 = String.Format("window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                }
                else
                {
                    String msg1 = String.Format("alert('Purchase order has been raised successfully.'); ");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                }




                BindRequisitionInfo();

                //AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Supply_Items");
                //AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Reqsn");
                //AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Reqsn_Status");
                BindGrid();
                divpurcomment.Visible = false;
                btnSendOrder.Enabled = true;
            }
            else
            {
                String msg2 = String.Format("alert('Select alteast one quotation to raise the PO.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                //lblError.Text = "Select alteast one check box to raise the PO.";
            }
        }
        else
        {

            String msg3 = String.Format("alert('There is no quotation selected to Send the PO.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg3, true);
            //lblError.Text = "There is No data to Send the Purchase Order";
        }


    }


    public string ReplaceSpecialCharacterinFileName(string strFileName)
    {
        return strFileName.Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace("?", "_").Replace("*", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace(":", "_").Replace("&", "_").Trim();
    }

    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");

    }

    protected void btnSendPO_click(object s, EventArgs e)
    {
        try
        {
            SendPurchaseOrder();
        }
        catch (Exception ex)
        {
            lblmsgt.Text = ex.Message;
        }
        finally
        {
            divpurcomment.Visible = false;
            btnSendOrder.Enabled = true;
        }


    }


    protected void btnSendPOContinue_click(object s, EventArgs e)
    {
        try
        {
            SendPurchaseOrder();
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {
            divpurcomment.Visible = false;
            btnSendOrder.Enabled = true;
        }

    }


    protected void rgdSuppliers_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

    }

    protected void lnkSelect_Click(object s, EventArgs e)
    {
        try
        {
            lblOnerCharts.Text = "";
            GridViewRow gr = (GridViewRow)((ImageButton)s).Parent.Parent;
            txteta.Text = ((Label)gr.FindControl("lblArrival")).Text;
            txtetd.Text = ((Label)gr.FindControl("lblDeparture")).Text;
            DDLPort.ClearSelection();
            DDLPort.Items.FindByValue(((HiddenField)gr.FindControl("hdnPortID")).Value).Selected = true;
            string suppCode = ((Label)gr.FindControl("lblOwners_Agent")).ToolTip;

            // if owner agent is null then select charts agent 
            if (suppCode == "")
            {
                suppCode = ((Label)gr.FindControl("lblCharterers_Agent")).ToolTip;
                if (suppCode != "")
                    lblOnerCharts.Text = "Charterer's Agent";
            }

            if (suppCode.Trim() != "")
            {
                DataTable dtSuppDtl = BLL_PURC_Common.Get_SupplierDetails_ByCode(suppCode);
                txtagent.Text = dtSuppDtl.Rows[0]["fullname"].ToString() + "\r\n";
                txtagent.Text += dtSuppDtl.Rows[0]["address"].ToString() + "\r\n";
                txtagent.Text += dtSuppDtl.Rows[0]["phone"].ToString() + "\r\n";
                txtagent.Text += dtSuppDtl.Rows[0]["email"].ToString() + "\r\n";

            }
        }
        catch(Exception ex)
        {
            string js = "alert(" + ex.Message + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);

        }

    }


}
