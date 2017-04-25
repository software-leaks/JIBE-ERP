using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;
using EO.Pdf;
using System.Drawing;
using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Text;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Web.Caching;

public partial class PO_LOG_PO_Log_Notice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["ID"] != null)
            //{

                BindSupplierDetails();
            //}
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
    protected void BindSupplierDetails()
    {
        try
        {
            string supply_Id = "15003680";

            //DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Supplier_Deatils(UDFLib.ConvertIntegerToNull(Request.QueryString["ID"].ToString()), UDFLib.ConvertIntegerToNull(GetSessionUserID()));
            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Supplier_Deatils(UDFLib.ConvertIntegerToNull(supply_Id), UDFLib.ConvertIntegerToNull(GetSessionUserID()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                if(!string.IsNullOrEmpty( ds.Tables[0].Rows[0]["Email"].ToString()))
                {
                ViewState["TO"] = ds.Tables[0].Rows[0]["Email"].ToString()+";"+ds.Tables[0].Rows[0]["Contact_1_Email"].ToString();
                }
                else{
                    ViewState["TO"] = ds.Tables[0].Rows[0]["Contact_1_Email"].ToString();
                }
                ViewState["PassCode"] = ds.Tables[0].Rows[0]["PassCode"].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {

            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                ViewState["PO"] = ds.Tables[2].Rows[0]["Office_Ref_Code"].ToString();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                
                    ViewState["CC"] = ds.Tables[3].Rows[0]["MailId"].ToString();
            }
        }
        catch { }
        {

        }
    }
    protected void btnMail1_Click(object sender, EventArgs e)
    {
       
        pnlEmail.Visible = true;
        StringBuilder str = new StringBuilder();
        String Str= null;
        string PassString = ViewState["PassCode"].ToString();
        string Office_Ref_Code = ViewState["PO"].ToString();
       string Subject = "Online Upload of Invoice for Purchase Order " + Office_Ref_Code + "";

        str.Append("***This is an automated email, please do not reply.***");
        str.Append("<Br><Br>");
        str.Append("Dear Supplier,");
        str.Append("<Br><Br>");
        str.Append("In our ongoing efforts to reduce our carbon foot print and improve invoice processing efficiency,");
        str.Append("we require all our Suppliers to prepare and upload E-invoice (Electronic Invoices, Or Scanned Invoices) against the PO that has been issue to them.");
        str.Append("This will ensure that all invoices are submitted to us in a timely manner without the unneccessary processing delays and resources that is required to handle paper invoices.");
        str.Append("<Br>");
        str.Append("<Font Color=red Size=5>To ensure compliance, hard copy invoices and Email invoices will not be accepted.</Font>");
        str.Append("<Br>");
        str.Append("Please use the below link to access the Online Invoice Status Page to upload the invoice for Purchase Order Reference " + Str + " into our system.");
        str.Append("<Br>");
        str.Append("Http://seachange.dyndns.info/demoasp/po_log/Supplier_Online_Invoice_Status.asp?P=" + PassString + "");
        str.Append("<a href='http://seachange.dyndns.info/demoasp/po_log/Supplier_Online_Invoice_Status.asp?P=" + PassString + "' Target='new'> CLICK HERE </a>");
        str.Append("<Br><Br>");
        str.Append("Guidelines/Instructions are provided during the invoice upload process.");
        str.Append("You can also contact the person in charge of the PO (Cc in this email) to assist you if you face any problems.");
        str.Append("<Br><Br>");
        str.Append("Thank you for your co-operation.");
        str.Append("System Administrator");
        str.Append("***This is an automated email, please do not reply.***");

        lblSubject.Text = Subject;
        lblCC.Text = ViewState["CC"].ToString();
        lblTo.Text = ViewState["TO"].ToString();
        lblBody.Text = str.ToString();
        ViewState["Message_ID"] = "1";
    }
    protected void btnMail2_Click(object sender, EventArgs e)
    {
        
        pnlEmail.Visible = true;
          StringBuilder str = new StringBuilder();
        String Str= null;
        string PassString = ViewState["PassCode"].ToString();
        string Office_Ref_Code = ViewState["PO"].ToString();
        string Subject = "Submission of Invoice for Purchase Order " + Office_Ref_Code + "";

        str.Append("***This is an automated email, please do not reply.***");
        str.Append("<Br><Br>");
        str.Append("Dear Supplier,");
        str.Append("<Br><Br>");
        str.Append("We noticed that the following Purchase Order has been issued to you and till date, NO invoice has been received.");
        str.Append("<Br>" + Office_Ref_Code + "<Br>");
        str.Append("<Br>");
        str.Append("To ensure that payment is processed on a timely basis, we would require you to upload the invoice ");
        str.Append("(together with delivery order/service report/work done report) ");
        str.Append("for the above Purchase Order using our online Invoice upload link.");
        str.Append("<Br><Br>");
        str.Append("Http://seachange.dyndns.info/demoasp/po_log/Supplier_Online_Invoice_Status.asp?P=" + PassString + "");
        str.Append("<a href='http://seachange.dyndns.info/demoasp/po_log/Supplier_Online_Invoice_Status.asp?P=" + PassString + "' Target='new'> CLICK HERE </a> ");
        str.Append("<Br><Br>");
        str.Append("Guidelines/Instructions are provided during the invoice upload process.");
        str.Append("You can also contact the person in charge of the PO (Cc in this email) to assist you if you face any problems.");
        str.Append("<Br><Br>");
        str.Append("Thank you for your co-operation.");
        str.Append("System Administrator");
        str.Append("***This is an automated email, please do not reply.***");
        lblSubject.Text = Subject;
       lblCC.Text = ViewState["CC"].ToString();
        lblTo.Text = ViewState["TO"].ToString();
        lblBody.Text = str.ToString();

        ViewState["Message_ID"] = "2";
    }
    
    protected void btnMail3_Click(object sender, EventArgs e)
    {
        pnlEmail.Visible = true;
       StringBuilder str = new StringBuilder();
        String Str= null;
        string PassString = ViewState["PassCode"].ToString();
        string Office_Ref_Code = null;
        string Subject = "Update of Company registered information";

        str.Append("***This is an automated email, please do not reply.***");
        str.Append("<Br><Br>");
        str.Append("Dear Supplier,");
        str.Append("<Br><Br>");
        str.Append("We note that there is a change in your company details / Banking information.");
        str.Append("<Br>");
        str.Append("To ensure that payment is processed on a timely basis, we would require you to update/re-verify your company registered information using our online form. ");
        str.Append("<Br><Br>");
        str.Append("Http://seachange.dyndns.info/demoasp/asl/Supplier_data.asp?P=" + PassString + "<Br>");
        str.Append("Or " + "<Br>" + " <a href='http://seachange.dyndns.info/demoasp/asl/Supplier_data.asp?P=" + PassString + "' Target='new'> CLICK HERE </a> ");
        str.Append("<Br><Br>");
        str.Append("Once you have verified and finalized your company information, please also upload an scanned copy of the form that has been duly signed and endorsed with your company stamp.");
        str.Append("<Br><Br>");
        str.Append("We seek your cooperation in the above matter.");
        str.Append("<Br><Br>");

        str.Append("Thank you for your co-operation." + "<Br>");
      
        str.Append("System Administrator");
        str.Append("***This is an automated email, please do not reply.***");
        lblSubject.Text = Subject;
        lblCC.Text = ViewState["CC"].ToString();
        lblTo.Text = ViewState["TO"].ToString();
        lblBody.Text = str.ToString();
        ViewState["Message_ID"] = "3";
    
    }
    protected void btnMail4_Click(object sender, EventArgs e)
    {
        pnlEmail.Visible = true;
        StringBuilder str = new StringBuilder();
        String Str = null;
        string PassCode = ViewState["PassCode"].ToString();
        string SupplierName = null;
        string Office_Ref_Code = ViewState["PO"].ToString();
        string Subject = "Purchase Order " + Office_Ref_Code;

        str.Append("To " + SupplierName);
        str.Append("<Br><Br>");
        str.Append("Please take be advised that purchase order " + Office_Ref_Code + " has been issued to your good company. ");
        str.Append("<Br>");
        str.Append("click on the below hyperlink to view your Purchase Order.  ");
        str.Append("<Br><Br>");
        str.Append("Click here : <A HRef='http://103.24.4.60/demoasp/PO_LOG/Supplier_PO_View.asp?PassCode=" + PassCode + "'>" + Office_Ref_Code + "</A> ");
        str.Append("<Br><Br>");
        str.Append(" This is an automated mailer system.");
        lblSubject.Text = Subject;
        lblCC.Text = ViewState["CC"].ToString();
        lblTo.Text = ViewState["TO"].ToString();
        lblBody.Text = str.ToString();
        ViewState["Message_ID"] = "4";
    }
    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
       // string html = HtmlEncode(lblBody.Text);
            BLL_POLOG_Invoice.POLOG_Insert_Mail_Details(lblSubject.Text, lblTo.Text, lblCC.Text, lblBody.Text, Convert.ToInt32(GetSessionUserID()));
            string js = "alert('Mail has been sent successfully')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
    }
   
}