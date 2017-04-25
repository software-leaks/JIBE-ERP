using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;




public partial class Technical_Worklist_EmailJob : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    //ActiveDirectoryHelper objAD = new ActiveDirectoryHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int OFFICE_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
            int WORKLIST_ID = Convert.ToInt32(Request.QueryString["WLID"]);
            int VESSEL_ID = Convert.ToInt32(Request.QueryString["VID"]);

            getJobDetails(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    protected void getJobDetails(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    {
        try
        {
            DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

            if (dtsJobDetails.Tables[0].Rows.Count > 0)
            {
                if (dtsJobDetails.Tables[0].Rows[0]["IsVessel"].ToString() == "1")
                    txtSubject.Text = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString();
                else
                    txtSubject.Text = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString() + "/PIC:" + dtsJobDetails.Tables[0].Rows[0]["PIC_Name"].ToString();

                string msgBody = "";

                msgBody += "\n\nVessel: " + dtsJobDetails.Tables[0].Rows[0]["vessel_name"].ToString();
                msgBody += "\nJob Code: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString();
                msgBody += "\nDescription: " + dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();

                msgBody += "\n\n<a href='www.google.com'>View Job</a>";
                txtMailBody.Text = msgBody;

            }

        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);

        }
    }

    public void SendMail(string To, string CC, string Subject, string MsgBody, Boolean IsHtml)
    {
        try
        {
            System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
            Message.From = new System.Net.Mail.MailAddress("");

            string[] ToEmails = To.Split(';');
            for (int i = 0; i < ToEmails.Length; i++)
            {
                Message.To.Add(ToEmails[i]);
            }

            if (CC != "")
            {
                string[] CCEmails = CC.Split(';');
                for (int i = 0; i < CCEmails.Length; i++)
                {
                    Message.CC.Add(CCEmails[i]);
                }
            }

            Message.Subject = Subject;
            Message.IsBodyHtml = IsHtml;
            Message.Body = MsgBody;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificatesCallback;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("", "1234");

            smtp.Port = 587;
            smtp.Host = "66.46.182.50";
            smtp.EnableSsl = true;

            //smtp.Port = 587;
            //smtp.Host = "smtp.gmail.com";
            //smtp.EnableSsl = true;

            smtp.Send(Message);

            string js2 = "alert('Mail sent successfully');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailalert", js2, true);
        }
        catch (SmtpException ex)
        {

            string js = "alert('Email sending failed!! Error:" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMail", js, true);
        }
    }

    public static bool TrustAllCertificatesCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }
    protected void ImgBtnSend_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtTo.Text == "")
            {
                string js = "alert('Enter mail id of the receiver');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMail", js, true);
                return;
            }
            SendMail(txtTo.Text, txtCC.Text, txtSubject.Text, txtMailBody.Text, false);

        }
        catch (Exception ex)
        {
            string js = "alert('Error sending mail. Error: " + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);

        }

    }






































    protected void btnAddTo_Click(object sender, EventArgs e)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        txtSelectedIDsTo.Text = "";
        string To = "";

        foreach (ListItem li in lstUsers.Items)
        {
            if (li.Selected)
            {
                DataTable dt = objUser.Get_UserDetails(int.Parse(li.Value));
                if (dt.Rows.Count > 0)
                {
                    if (To.Length > 0 && dt.Rows[0]["MailID"].ToString() != "")
                        To += ";";
                    To += dt.Rows[0]["MailID"].ToString();
                }
            }
        }
        txtSelectedIDsTo.Text = To;
        objUser = null;
    }

    protected void btnCC_Click(object sender, EventArgs e)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        txtSelectedIDsCC.Text = "";
        string CC = "";

        foreach (ListItem li in lstUsers.Items)
        {
            if (li.Selected)
            {
                DataTable dt = objUser.Get_UserDetails(int.Parse(li.Value));
                if (dt.Rows.Count > 0)
                {
                    if (CC.Length > 0 && dt.Rows[0]["MailID"].ToString() != "")
                        CC += ";";
                    CC += dt.Rows[0]["MailID"].ToString();
                }
            }
        }
        txtSelectedIDsCC.Text = CC;
        objUser = null;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        txtTo.Text = txtSelectedIDsTo.Text;
        txtCC.Text = txtSelectedIDsCC.Text;

        string js = "$('#dvSelectAddress').hide('slow');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePop", js, true);

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string js = "$('#dvSelectAddress').hide('slow');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePop", js, true);
    }

}

