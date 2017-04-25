//System libararies
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
//Custom libararies
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;
using System.Configuration;
using System.Web.UI.HtmlControls;
using SMS.Business.PURC;
using SMS.Business.Crew;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;


public partial class Evaluation : System.Web.UI.Page
{
    string LastTravelAgent = "";
    protected string currentstatus = "";
    protected int requestID, paxcount, userPreference = 0;
    int QuotationApproved = 0;
    public int IsApproving = 0;



    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (UDFLib.ConvertToInteger(Request.QueryString["IsPreference"]) == 1)
        {
            cmdApprove.Visible = false;
        }

        if (Request.QueryString["requestid"] != null)
        {
            try
            {
                requestID = Convert.ToInt32(Request.QueryString["requestid"].ToString());
                ltRequestid.Text = requestID.ToString();
                if (!IsPostBack)
                {
                    BLL_TRV_TravelRequest objT = new BLL_TRV_TravelRequest();
                    DataTable dtsts = objT.Get_Request_ApprovalStatus(requestID);
                    if (dtsts.Rows.Count > 0)
                    {
                        if (dtsts.Rows[0]["Sent_For_Approval"].ToString() == "1" && dtsts.Rows[0]["currentStatus"].ToString() == "QUOTE RECEIVED")
                        {
                            IsApproving = 1;
                        }
                    }

                    ViewState["ChepeastAmount"] = null;

                    GetTravelRequestDetails();

                    GetCheapestOptions();

                    GetQuotationForEvaluation();
                    BLL_TRV_QuoteRequest objAppHis = new BLL_TRV_QuoteRequest();

                    gvApprovals.DataSource = objAppHis.Get_Approval_History(requestID);
                    gvApprovals.DataBind();

                    BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();


                    DataTable dt = QR.Get_Qtn_Approver_DeptMgr(0, 0);

                    lstMngApprList.DataSource = dt;
                    lstMngApprList.DataBind();
                    lstMngApprList.Items.Insert(0, new ListItem("SELECT", "0"));
                    lstMngApprList.SelectedIndex = 0;
                    ListItem itemrmv = lstMngApprList.Items.FindByValue(Session["userid"].ToString());
                    lstMngApprList.Items.Remove(itemrmv);


                    ListBoxPOApprover.DataSource = QR.Get_Qtn_Approver();
                    ListBoxPOApprover.DataBind();
                    ListBoxPOApprover.Items.Insert(0, new ListItem("SELECT", "0"));
                    ListBoxPOApprover.SelectedIndex = 0;

                    lstMngApprList.Items.Remove(itemrmv);

                }
            }
            catch { }
        }
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0 )
        {
            Response.Redirect("~/default.aspx?msgid=1", false);
        }

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {
            cmdApprove.Visible = false;
        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void GetTravelRequestDetails()
    {
        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        try
        {
            DataSet ds = new DataSet();
            ds = treq.GetTravelRequestByID(requestID, 0);
            lblSeamanStatus.Text = ds.Tables[0].Rows[0]["isSeaman"].ToString() == "Y" ? "SEAMAN TICKET" : "NOT A SEAMAN TICKET";
            lblSeamanStatus.ForeColor = ds.Tables[0].Rows[0]["isSeaman"].ToString() == "Y" ? System.Drawing.Color.Blue : System.Drawing.Color.Red;

            rptParent.DataSource = ds;
            rptParent.DataBind();
        }
        catch { }
        finally { treq = null; }
    }

    protected void GetQuotationForEvaluation()
    {
        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();
        try
        {
            DataSet ds = new DataSet();
            ds = QR.GetQuotationForEvaluation(requestID);
            rptQuotes.DataSource = ds;
            rptQuotes.DataBind();

            hdf_No_of_Quotation.Value = ds.Tables[0].Rows.Count.ToString();

        }
        catch { }
        finally { QR = null; }
    }

    protected void GetCheapestOptions()
    {

        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();
        try
        {
            DataSet ds = new DataSet();
            ds = QR.GetCheapestOptions(requestID);
            ViewState["ChepeastAmount"] = ds.Tables[0].Rows[0]["USD_Total_Amount"].ToString();
            hdf_Cheapest_Totalamount.Value = ds.Tables[0].Rows[0]["USD_Total_Amount"].ToString();

        }
        catch { }
        finally { QR = null; }

    }



    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;
                paxcount = Convert.ToInt32(drv.Row["PaxCount"].ToString());
                currentstatus = drv.Row["CurrentStatus"].ToString().ToUpper();
                if (currentstatus == "APPROVED")
                {
                    cmdApprove.Text = "Quotation already approved";
                    cmdApprove.Enabled = false;
                    QuotationApproved = 1;
                }
                if (currentstatus == "TICKET ISSUED")
                {
                    cmdApprove.Text = "Ticket already issued";
                    cmdApprove.Enabled = false;
                    QuotationApproved = 1;
                }

                ((Image)e.Item.FindControl("imgRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["remarks"].ToString() + "]");
                if (drv.Row["User_Preferred_QuoteId"] != null)
                    userPreference = UDFLib.ConvertToInteger(drv.Row["User_Preferred_QuoteId"].ToString());
            }

        }
        catch { }
    }

    protected void rptChild_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int id;
            if (e.CommandName.ToUpper() == "REMOVEPAX")
            {
                BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
                id = Convert.ToInt32(e.CommandArgument);
                TRequest.RemovePaxFromTravelRequest(id, Convert.ToInt32(Session["USERID"].ToString()));
                TRequest = null;
                GetTravelRequestDetails();
            }
        }
        catch { }
    }


    protected void rptChild_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        BLL_TRV_TravelRequest objRequest = new BLL_TRV_TravelRequest();
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRow drv = (System.Data.DataRow)e.Item.DataItem;
                string message = "";
                message = objRequest.Get_Pax_Validation(Convert.ToInt32(drv["requestid"].ToString()), "DEL");

                if (message != "OK")
                    ((Image)e.Item.FindControl("btnDelete")).Attributes.Add("onclick", "alert('" + message + "'); return false;");
                else
                    ((Image)e.Item.FindControl("btnDelete")).Attributes.Add("onclick", "return confirm('This will DELETE the PAX from travel request. Do you want to proceed?');");
            }





        }
        catch { }
        finally { objRequest = null; }
    }


    protected void rptQuotes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "RESETQUOTE")
            {
                BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
                int MailID = TRequest.ResetQuotation(requestID, 0, Convert.ToInt32(e.CommandArgument));
                TRequest = null;
                string UploadFilePath = ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"];

                string URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath.Remove(UploadFilePath.Length - 1, 1).Replace(@"\", @"\\") + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);
                GetQuotationForEvaluation();
            }

            if (e.CommandName.ToUpper() == "PREFERENCE")
            {
                BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
                Qr.UpdateUserPreference(requestID, Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USERID"]));
                Qr = null;
                GetTravelRequestDetails();
                GetQuotationForEvaluation();
            }

        }
        catch { }
    }
    int rownumber = 2;
    protected void rptQuotes_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            decimal amount;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;


                if (rownumber % 2 == 0)
                    (e.Item.FindControl("datarow") as HtmlTableRow).Attributes.Add("class", "RowStyle-css");
                else
                    (e.Item.FindControl("datarow") as HtmlTableRow).Attributes.Add("class", "AlternatingRowStyle-css");

                rownumber++;

                amount = Convert.ToDecimal(drv.Row["totalamount"]);

                if (UDFLib.ConvertToInteger(drv["apprSts"]) == 1 || UDFLib.ConvertToInteger(drv["VerifSts"]) == 1)
                {
                    hdAgentID.Value = Convert.ToString(drv["AgentID"]);
                    hdf_Totalamount.Value = Convert.ToString(drv["USD_Total_Amount"]);
                }


                ((Label)e.Item.FindControl("lblGrandTotal")).Text = (paxcount * amount).ToString();
                ((Label)e.Item.FindControl("lblUSDRate")).Text = System.Math.Round(paxcount * UDFLib.ConvertToDecimal(drv["USDRate"].ToString()), 2).ToString();


                if (((Label)e.Item.FindControl("lblGrandTotal_usd")).Text == ViewState["ChepeastAmount"].ToString())
                {
                    ((Label)e.Item.FindControl("lblGrandTotal_usd")).BackColor = System.Drawing.Color.Yellow;

                }

                if (!String.IsNullOrEmpty(drv.Row["user_preference_By"].ToString()))
                {
                    ((Button)e.Item.FindControl("cmdUserPref")).Text = "Prefered By: " + drv["user_preference_By"].ToString();
                    ((Button)e.Item.FindControl("cmdUserPref")).Enabled = false;
                }

              

                if (QuotationApproved == 1)
                {
                   // ((Button)e.Item.FindControl("cmdUserPref")).Enabled = false;

                    ((Button)e.Item.FindControl("imgReset")).Enabled = false;
                }

              

            }
        }
        catch { }
    }

    protected void rptFlights_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRow drv = (System.Data.DataRow)e.Item.DataItem;
                ((Image)e.Item.FindControl("imgFlightRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["remarks"].ToString() + "]");
            }
        }
        catch { }
    }

    protected void cmdApprove_Click(object sender, EventArgs e)
    {

        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();



        int result;
        try
        {
            string qid = "";
            string QSupplierCode = "";
            try
            {
                  qid = Request.Form["rdQuote"].ToString().Split(',')[0];
                  QSupplierCode = Request.Form["rdQuote"].ToString().Split(',')[1];
            }
            catch (Exception)
            {

                string msgmodal = String.Format("alert('Approver not Selected!');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Approver", msgmodal, true);
                return;
            }
        

            if (!String.IsNullOrEmpty(qid))
            {
                DataTable dtSuppDate = BLL_PURC_Common.Get_Supplier_ValidDate("'" + QSupplierCode + "'");
                if (dtSuppDate.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(dtSuppDate.Rows[0]["ASL_Status_Valid_till"]) < DateTime.Now)
                    {
                        String msg = String.Format("alert('Selected agent has been expired/blacklisted');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg451", msg, true);

                    }
                    else
                    {
                        QR.Insert_Approval_Entry(Convert.ToInt32(Session["USERID"].ToString()),
                                                Convert.ToInt32(Session["USERID"].ToString()),
                                                decimal.Parse(hdf_Totalamount.Value.Trim()),
                                                Convert.ToInt32(Request.QueryString["requestid"].ToString()), "", txtApproverRemark.Text, 1, UDFLib.ConvertIntegerToNull(qid));

                        decimal Approval_limit = QR.Get_Approveal_Limit(Convert.ToInt32(Session["userid"]));
                        //decimal Approval_limit = 1000;
                        if (Approval_limit > 0 && (decimal.Parse(hdf_Totalamount.Value.Trim()) <= Approval_limit))
                        {    
                            int Mail_ID;
                            result = QR.ApproveQuotation(requestID, Convert.ToInt32(qid),
                                Convert.ToInt32(Session["USERID"].ToString()), txtApproverRemark.Text,out Mail_ID);
                            if (result > -1)
                            {
                                //add PO as attachment to mail
                              
                              DataTable dtPO= QR.Get_Generate_PO_PDF(Request.QueryString["requestid"]);
                              if (dtPO.Rows.Count > 0)
                              {
                                  string fileName = Convert.ToString(dtPO.Rows[0]["ORDER_CODE"]).Replace('-','_') + DateTime.Now.ToString("yyMMddss") + ".pdf";
                                  GeneratePOAsPDF(dtPO,fileName, Mail_ID);
                              }


                                if (result > 0) // SQ Flight has been approved
                                {
                                    UTF8Encoding utf8 = new UTF8Encoding();
                                    System.Net.WebClient myClient = new System.Net.WebClient();

                                    string currentPageUrl = ConfigurationManager.AppSettings["INVFolderPath"] + "/travel/Evaluation_view.aspx?requestid=" + Request.QueryString["requestid"];
                                    byte[] requestHTML; requestHTML = myClient.DownloadData(currentPageUrl);
                                    string ReportPageHTML = utf8.GetString(requestHTML);

                                    BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();                                 
                                }


                                string msgmodal = String.Format(" alert('Quotation has been approved successfully');self.close();window.opener.location.reload();");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalFinalApproved", msgmodal, true);

                            }

                        }
                        else
                        {

                            lstUserList.DataSource = QR.Get_Qtn_Approver();
                            lstUserList.DataBind();
                            lstUserList.Items.Insert(0, new ListItem("SELECT", "0"));
                            lstUserList.SelectedIndex = 0;
                            ListItem itemrmv = lstUserList.Items.FindByValue(Session["userid"].ToString());
                            lstUserList.Items.Remove(itemrmv);

                            string msgmodal = String.Format("showModal('divSendForApproval');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);
                        }
                    }
                }

            }
            else
            {
                string msgmodal = String.Format(" alert('Please select option to approve'); ");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "optiotoapprove", msgmodal, true);
            }
        }
        catch (Exception ex)
        {
            string msgmodal = "alert('"+ex.Message+"');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorex", msgmodal, true);
        }
        finally { QR = null; TRequest = null; }

    }

    protected void btnSendForApproval_Click(object s, EventArgs e)
    {
        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        BLL_Infra_UserCredentials objuser = new BLL_Infra_UserCredentials();
        DataTable dtuser = objuser.Get_UserDetails(Int32.Parse(lstUserList.SelectedValue));
        DataTable dtCurrentuser = objuser.Get_UserDetails(Convert.ToInt32(Session["userid"]));

        QR.Insert_Approval_Entry(Convert.ToInt32(Session["USERID"].ToString()),
                                               Int32.Parse(lstUserList.SelectedValue),
                                                 0,
                                                 Convert.ToInt32(Request.QueryString["requestid"].ToString()), txtRemark.Text, "");


        string msgmodal = String.Format("alert('Sent successfully to selected approver.');window.close();window.opener.location.reload();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);

    }

    protected void btnRefreshpage_Click(object s, EventArgs e)
    {
        GetTravelRequestDetails();

        GetCheapestOptions();

        GetQuotationForEvaluation();
    }

    protected void btnReworkPIC_Click(object s, EventArgs e)
    {
        BLL_TRV_TravelRequest objTR = new BLL_TRV_TravelRequest();
        int sts = objTR.UPD_Rework_TravelPIC(txtReworkRemark.Text, Convert.ToInt32(Request.QueryString["requestid"]), Convert.ToInt32(Session["userid"]));

        if (sts > 0)
        {
            string msgmodal = String.Format("alert('Reworked successfully.');window.close();window.opener.location.reload();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgrework", msgmodal, true);
        }
    }


    protected void btnSendForApprovalByPIC_Click(object s, EventArgs e)
    {
        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();

        int ApproverID = 0;
        int ForApproval;

        if (lstMngApprList.SelectedItem.Value == "0" && ListBoxPOApprover.SelectedItem.Value != "0") // sending for po approval
        {
            ApproverID = int.Parse(ListBoxPOApprover.SelectedItem.Value);
            ForApproval = 1;
        }
        else if (lstMngApprList.SelectedItem.Value != "0" && ListBoxPOApprover.SelectedItem.Value == "0")// sending for Manager approval
        {
            ApproverID = int.Parse(lstMngApprList.SelectedItem.Value);
            ForApproval = 2;
        }
        else if (lstMngApprList.SelectedItem.Value == "0" && ListBoxPOApprover.SelectedItem.Value == "0") // atleat  one should be selected
        {
            lblMessageOnSendApproval.Visible = true;
            lblMessageOnSendApproval.Text = "Please select Approver !";
            return;
        }
        else // both can not be selected at a time
        {
            lblMessageOnSendApproval.Visible = true;
            lblMessageOnSendApproval.Text = "PO approver and Managerial approver can not be selected simultaneously !";
            return;
        }

        if (ApproverID != 0)
        {
            QR.Insert_Approval_Entry(UDFLib.ConvertToInteger(Session["USERID"].ToString()),
                                    ApproverID,
                                     0,
                                     UDFLib.ConvertToInteger(Request.QueryString["requestid"]),
                                     txtSendForAppRemark.Text,
                                     "", ForApproval);


            string msgmodal = String.Format("alert('Request sent successfully');hideModal('divSendForApproval');window.close();window.opener.location.reload();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);
        }
    }

    protected void GeneratePOAsPDF(DataTable dt, string FileName,int  EmailID)
    {
       
        string repFilePath = Server.MapPath("TRVPOReport.rpt");
       
        ReportDocument repDoc = new ReportDocument();
        repDoc.Load(repFilePath);

        repDoc.SetDataSource(dt);

                       
        ExportOptions exp = new ExportOptions();
        DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
        PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

        string destFile = Server.MapPath("../Uploads/Travel") + "\\" + FileName;
        dk.DiskFileName = destFile;

        //string Network_Path = System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString()+@"Uploads\Travel\";
        string Network_Path =  @"Uploads\Travel\";
        exp.ExportDestinationType = ExportDestinationType.DiskFile;
        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
        exp.DestinationOptions = dk;
        exp.FormatOptions = pd;
        repDoc.Export(exp);

        BLL_Infra_Common.Insert_EmailAttachedFile(EmailID, FileName, Network_Path + FileName);
       
    }

}