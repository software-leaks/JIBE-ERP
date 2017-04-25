using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Data;
using System.Configuration;

public partial class Purchase_LOG_Raise_LogisticPO : System.Web.UI.Page
{
    string TypeOpen = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            lblheader.Text = "Raise PO for Logistic ID : " + Request.QueryString["LOG_ID"];
            DataTable dtPoList = BLL_PURC_LOG.Get_Log_POList_Raise(Convert.ToInt32(Request.QueryString["LOG_ID"]));
            gvLPOList.DataSource = dtPoList;
            gvLPOList.DataBind();

            btnSendOrder.Visible = true;

            dvRaiselPo.Visible = false;
            BLL_PURC_Purchase objport = new BLL_PURC_Purchase();
            DDLPort.DataSource = objport.getDeliveryPort();
            DDLPort.DataBind();
            BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
            //DataSource = objCrew.Get_PortCall_List(Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()));
            //gvPortCalls.DataBind();
            DataTable dt = objport.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='A'";

            ddlSentFrom.DataTextField = "SUPPLIER_NAME";
            ddlSentFrom.DataValueField = "SUPPLIER";
            ddlSentFrom.DataSource = dt.DefaultView.ToTable();
            ddlSentFrom.DataBind();
            ddlSentFrom.Items.Insert(0, new ListItem("SELECT", "0"));

            ListItem liDDl = new ListItem();
            liDDl = DDLPort.Items.FindByValue(dtPoList.Rows[0]["port"].ToString());
            if (liDDl != null)
                liDDl.Selected = true;

            liDDl = ddlSentFrom.Items.FindByValue(dtPoList.Rows[0]["Agent"].ToString());
            if (liDDl != null)
            {
                liDDl.Selected = true;
                if (Convert.ToString(dtPoList.Rows[0]["Agent"]) != "0")
                {
                    DataTable dtAgentsDtls = BLL_PURC_Common.Get_SupplierDetails_ByCode(dtPoList.Rows[0]["Agent"].ToString());
                    if (dtAgentsDtls.Rows.Count > 0)
                    {
                        txtagent.Text = dtAgentsDtls.Rows[0]["fullname"].ToString();
                        txtagent.Text += "\r\n" + dtAgentsDtls.Rows[0]["address"].ToString();
                        txtagent.Text += "\r\n" + dtAgentsDtls.Rows[0]["phone"].ToString();
                        txtagent.Text += "\r\n" + dtAgentsDtls.Rows[0]["email"].ToString();
                    }
                }
            }


        }

    }

    protected void FormateEmail(DataSet dsEmailInfo, out string sEmailAddress, out string strSubject, out string strBody, bool IsPendingApprovalPO, string OrderCode)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();

        strBody = @"Dear " + dsEmailInfo.Tables[0].DefaultView[0]["SHORT_NAME"].ToString() + @"<br><br>
                     We are pleased to attach our Purchase Order for delivery of the goods quoted per your quotation number:" + dsEmailInfo.Tables[0].DefaultView[0]["Supplier_Quotation_Reference"].ToString() + @"  for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + @".<br><br>
                    The purchase order can be downloaded from the following link   <br>
           
                   <b> KINDLY ACKNOWLEDGE OUR PO WITH DELIVERY DAYS / READINESS DATE VIA THE ABOVE LINK WITHIN SAME WORKING DAY.</b><br><br>

                    Please ensure accurate and complete delivery of this order to the agent, Forwarder or vessel. <br><br>
                    If there are partial deliveries, the sender of the Purchase Order we must be informed immediately via email.  <br><br>
                    Following packing and documentation procedures to adhere strictly; <br>
                    1 To pack in airworthy packing (strictly in carton box) <br>
                    2. Markings ON ALL PACKAGES: Address to vessel name :" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + @" and our purchase order number:" + dsEmailInfo.Tables[0].DefaultView[0]["ORDER_CODE"].ToString() + @" and vessel department____________ (eg. Engine Stores, Cabin Stores, Electrical stores, etc)<br>
                    3. Pls indicate the dim (LXBXH in CM), cargo weight, no.of pieces, value of goods on the manifest invoice and packing list or delivery note 
                    4 . Kindly enclose 02 copies of delivery note into the carton/crate for ship's checking <br>
                    5. Original Invoice and Original Delivery Note (with original company stamp and signature of receiving staff of our forwarder) to be send to " + Session["Company_Name_GL"] + @" by email in PDF format.<br><br>

                    Any inaccuracies, missing or damaged items will result in a delayed payment <br>
                    of your invoice. The PO number must be indicated in the invoice for <br>
                    prompt processing and  payment. <br><br>

                    Thank you & best regards,<br>
                    " + Session["USERFULLNAME"].ToString() + @"<br>
                    " + dtUser.Rows[0]["Designation"].ToString() + @"<br>
                    "+Convert.ToString(Session["Company_Address_GL"]) +@"
                    Mobile:+65 " + dtUser.Rows[0]["Mobile_Number"].ToString() + @"<br>
                    Email: " + dtUser.Rows[0]["MailID"].ToString() + @"<br>
                                 
                    <br><br>
                    ";


        strSubject = "Purchase Order No. " + OrderCode + " from " + Session["Company_Name_GL"] + "  for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + " ,  Date :" + DateTime.Now.ToString();
        sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();
      


    }

    protected void GeneratePOAsPDF(DataTable dt, string strPath, string FileName)
    {

        string repFilePath = Server.MapPath("LOG_POReport.rpt");
        //using (ReportDocument repDoc = new ReportDocument())
        //{

        ReportDocument repDoc = new ReportDocument();
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
        //}

    }

    protected void SendPurchaseOrder()
    {

        // string[] Attchment = new string[10];
        lblError.Text = "";
        string strPath = Server.MapPath(".") + "\\SendPO\\";
        DataTable DsPO = new DataTable();
        DataSet dsSendMailInfo = new DataSet();

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        string emailIDcc = dtUser.Rows[0]["MailID"].ToString();
        bool IsPO = false;



        int i = 0;

        string sDlvIns = txtdlvins.Text != "" ? txtdlvins.Text : DBNull.Value.ToString();
        string strDeliveryPort = DDLPort.SelectedItem.ToString() != "--Select--" ? DDLPort.SelectedValue.ToString() : DBNull.Value.ToString();
        string sEta = txteta.Text != "" ? txteta.Text + " " + txtETAAPPM.Text.ToString().Trim() + ":00" : Convert.ToString("01/01/1900");
        string sRemark = txtremark.Text != "" ? txtremark.Text : DBNull.Value.ToString();
        string sAgent = txtagent.Text != "" ? txtagent.Text : DBNull.Value.ToString();
        string sEtd = txtetd.Text != "" ? txtetd.Text + " " + txtETDAMPM.Text.ToString().Trim() + ":00" : Convert.ToString("01/01/1900");

        foreach (GridViewRow gvRow in gvLPOList.Rows)
        {
            CheckBox chk = (CheckBox)(gvRow.FindControl("chkSelectLPO") as CheckBox);
            string order_code = gvLPOList.DataKeys[gvRow.RowIndex].Value.ToString();

            if ((chk.Checked))
            {

                string OrderCode = gvLPOList.DataKeys[gvRow.RowIndex].Value.ToString();

                int sts = BLL_PURC_LOG.Upd_Log_Order_Details(sDlvIns, strDeliveryPort, DateTime.Parse(sEta), sRemark, DateTime.Parse(sEtd), sAgent, Request.QueryString["LOG_ID"], order_code, Convert.ToInt32(Session["USERID"]));

                DsPO = BLL_PURC_LOG.Get_Log_Raise_PO(order_code, Request.QueryString["LOG_ID"]);

                dsSendMailInfo = BLL_PURC_LOG.Get_RaisePO_EmailInfo(order_code, Request.QueryString["LOG_ID"], Convert.ToInt32(Session["USERID"]));
                string FileName = "PO_" + OrderCode + "_" + ReplaceSpecialCharacterinFileName(DsPO.Rows[0]["SHORT_NAME"].ToString()) + DateTime.Now.ToString("yyMMddss") + ".pdf";
                string sToEmailAddress = "", strSubject = "", strEmailBody = "";
                //Generate the PDF file and check the include amount status


                DataTable dtPO = DsPO;

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


                FormateEmail(dsSendMailInfo, out  sToEmailAddress, out  strSubject, out strEmailBody, false, OrderCode);

                BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
                int MailID = 0;
                MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, emailIDcc, "", strSubject, strEmailBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");

                string UploadFilePath = ConfigurationManager.AppSettings["PURC_UPLOAD_PATH"];

                //string uploadpath = @"\\server01\uploads\Purchase";
                string uploadpath = @"uploads\Purchase";
                BLL_Infra_Common.Insert_EmailAttachedFile(MailID, FileName, uploadpath + @"\" + FileName);


                string URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);

                //   ResponseHelper.Redirect("../crew/EmailEditor.aspx?ID=" + MailID.ToString(), "blank", "");

            }



        }


        String msg1 = String.Format("window.open('','_self') ;window.close() ;");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);


        dvRaiselPo.Visible = false;
        btnSendOrder.Enabled = true;




    }

    public string ReplaceSpecialCharacterinFileName(string strFileName)
    {
        return strFileName.Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace("?", "_").Replace("*", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace(":", "_").Replace("&", "_").Trim();
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
            dvRaiselPo.Visible = false;
            btnSendOrder.Enabled = true;
        }


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
        catch (Exception ex)
        {
            string js = "alert(" + ex.Message + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);

        }

    }

    protected void btnSendOrder_Click(object s, EventArgs e)
    {
        bool selected = false;

        foreach (GridViewRow gr in gvLPOList.Rows)
        {
            if ((gr.FindControl("chkSelectLPO") as CheckBox).Checked == true)
            {
                selected = true;
            }
        }
        if (!selected)
        {
            lblError.Text = "Please select PO !";
            return;
        }
        dvRaiselPo.Visible = true;
    }

    protected void btncancelpo_Click(object s, EventArgs e)
    {
        dvRaiselPo.Visible = false;
    }

}