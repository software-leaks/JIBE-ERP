using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class Crew_EmailEditor : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    ActiveDirectoryHelper objAD = new ActiveDirectoryHelper();

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            lstVessel.DataSource = dtVessel;
            lstVessel.DataTextField = "Vessel_name";
            lstVessel.DataValueField = "Vessel_id";
            lstVessel.DataBind();


            if (getQueryString("Discard") == "1")
                ImgBtnDiscard.Visible = true;
            else
                ImgBtnDiscard.Visible = false;

            if (getQueryString("ID") != "")
            {
                DataTable dt = objBLLCrew.Get_CrewNotificationDetails(int.Parse(getQueryString("ID")));

                if (dt.Rows.Count > 0)
                {
                    txtTo.Text = dt.Rows[0]["MailTo"].ToString();
                    txtCC.Text = dt.Rows[0]["CC"].ToString();

                    txtSubject.Text = dt.Rows[0]["Subject"].ToString();

                    txtMailBody.Text = dt.Rows[0]["Body"].ToString();
                    ViewState["SENT_STATUS"] = dt.Rows[0]["sent_status"].ToString();
                    if (ViewState["SENT_STATUS"].ToString() == "1")
                    {
                        if (getQueryString("ReSend") != "1")
                        {
                            txtTo.Enabled = false;
                            txtCC.Enabled = false;
                            txtMailBody.Enabled = false;
                            ImgBtnSend.Enabled = false;
                            txtSubject.Enabled = false;
                        }
                    }
                    else
                    {
                        txtTo.Enabled = true;
                        txtCC.Enabled = true;
                        txtMailBody.Enabled = true;
                        ImgBtnSend.Enabled = true;
                        txtSubject.Enabled = true;
                    }
                }
            }

            txtMailBody.config.toolbar = new object[]
            {
                new object[] { "Preview"},
                new object[] { "Cut", "Copy", "Paste", "PasteText", "-", "Print", "SpellChecker", "Scayt" },
                new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                "/",
                new object[] { "HorizontalRule", "Smiley", "PageBreak"},
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" },
                new object[] { "Maximize", "ShowBlocks"}
            };

            txtMailBody.Height = 400;
            txtMailBody.ResizeEnabled = false;

            ucEmailAttachment1.EmailID = getQueryString("ID");
            string FilePath = getQueryString("FILEPATH");
            if (FilePath == "")
            {
                ucEmailAttachment1.FileUploadPath = @"Uploads\EmailAttachments";
            }
            else
            {
                ucEmailAttachment1.FileUploadPath = FilePath;
            }
            LoadFiles(null, null);

        }
    }

    protected void UserAccessValidation()
    {

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgBtnSend.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
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

    //protected void txtTo_TextChanged(object sender, EventArgs e)
    //{

    //    ucEmailAttachment1.Register_JS_Attach();

    //    try
    //    {
    //        string[] sNames = txtTo.Text.Split(';');
    //        if (txtTo.Text.IndexOf(',') > 0)
    //        {
    //            sNames = txtTo.Text.Split(',');
    //        }

    //        for (var i = 0; i < sNames.Length; i++)
    //        {
    //            if (sNames[i].IndexOf("@unimarships.com") < 0)
    //            {
    //                string[] ADUser = objAD.getActiveUserDetails(sNames[i]);

    //                if (ADUser[3] != "")
    //                {
    //                    txtTo.Text = txtTo.Text.ToLower().Replace(ADUser[1].ToString().ToLower(), ADUser[3].ToString().ToLower());
    //                }
    //            }
    //        }
    //    }
    //    catch { }
    //}
    //protected void txtCC_TextChanged(object sender, EventArgs e)
    //{

    //    ucEmailAttachment1.Register_JS_Attach();

    //    try
    //    {
    //        string[] sNames = txtCC.Text.Split(',');

    //        for (var i = 0; i < sNames.Length; i++)
    //        {
    //            if (sNames[i].IndexOf("@unimarships.com") < 0)
    //            {
    //                string[] ADUser = objAD.getActiveUserDetails(sNames[i]);

    //                if (ADUser[3] != "")
    //                {
    //                    txtCC.Text = txtCC.Text.ToLower().Replace(ADUser[1].ToString().ToLower(), ADUser[3].ToString().ToLower());
    //                }
    //            }
    //        }
    //    }
    //    catch { }
    //}

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
        txtTo.Text +=";" +To;
        if (IsPostBack)
        {
            ucEmailAttachment1.Register_JS_Attach();
        }
        UpdatePanel1.Update();
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
        txtCC.Text += ";"+CC;
        objUser = null;
        if (IsPostBack)
        {
            ucEmailAttachment1.Register_JS_Attach();
        }
        UpdatePanel1.Update();
    }


    protected void btnAddVslTo_Click(object sender, EventArgs e)
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        txtSelectedIDsTo.Text = "";
        string To = "";

        foreach (ListItem li in lstVessel.Items)
        {
            if (li.Selected)
            {
                DataTable dt = objVsl.GetVesselDetails_ByID(int.Parse(li.Value));
                if (dt.Rows.Count > 0)
                {
                    if (To.Length > 0 && dt.Rows[0]["Vessel_email"].ToString() != "")
                        To += ";";
                    To += dt.Rows[0]["Vessel_email"].ToString();
                }
            }
        }

        if (((Button)sender).CommandArgument == "TO")
        {
           
            txtTo.Text += ";" + To;
        }
        else
        {
            txtCC.Text += ";" + To;
        }
        if (IsPostBack)
        {
            ucEmailAttachment1.Register_JS_Attach();
        }
        UpdatePanel1.Update();
        objVsl = null;
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
            else
            {
                if (ViewState["SENT_STATUS"].ToString() == "1" && getQueryString("ReSend") == "1")
                {
                    objBLLCrew.Send_CrewNotification(0, 0, 0, 0, txtTo.Text, txtCC.Text, "", txtSubject.Text, txtMailBody.Text, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "READY");
                    string js = "isMailSent=1; alert('Email has been sent from your side. This will be delivered to the receipients within 10 minutes.');window.open('','_self','');window.close();";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                }
                else
                {

                    if (getQueryString("ID") != "")
                    {
                        string body = txtMailBody.Text;

                        //var regex = new Regex(Regex.Escape("<p>"));
                        //body = regex.Replace(body, "<html><head>");
                        //regex = new Regex(Regex.Escape("</p>"));
                        //body = regex.Replace(body, "</head><body>");

                        body = ReplaceFirst(body, "<p>", "<html><head>");
                        body = ReplaceFirst(body, "</p>", "</head><body>");

                        body = body + "</body></html>";
                        objBLLCrew.UPDATE_CrewNotification(int.Parse(getQueryString("ID")), txtTo.Text, txtCC.Text, "", txtSubject.Text, body, "", "", GetSessionUserID(), "READY");
                        string js = " isMailSent=1; alert('Email has been sent from your side. This will be delivered to the receipients within 10 minutes.');window.open('','_self','');window.close();";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error sending mail. Error: " + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
        }

    }

    string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    protected void ImgBtnDiscard_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int RES = objBLLCrew.Discard_MailMessage(int.Parse(getQueryString("ID")), GetSessionUserID());
            if (RES == 1)
            {
                string js = "alert('Mail message is discarded');window.close();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error sending mail. Error: " + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
        }

    }

    public void LoadFiles(object s, EventArgs e)
    {
        try
        {
            dlstAttachment.DataSource = BLL_Infra_Common.Get_EmailAttachedFile(Convert.ToInt32(getQueryString("ID")));
            dlstAttachment.DataBind();
            if (IsPostBack)
            {
                ucEmailAttachment1.Register_JS_Attach();
            }

        }
        catch { }

    }

    protected void imgbtnDelete_Click(object s, EventArgs e)
    {
        string ID = ((ImageButton)s).CommandArgument.Split(',')[0];
        int res = BLL_Infra_Common.Delete_EmailAttachedFile_DL(Convert.ToInt32(ID));
        if (res > 0)
        {
            string path = Server.MapPath("~") + @"\" + ((ImageButton)s).CommandArgument.Split(',')[1];
            //File.Delete(((ImageButton)s).CommandArgument.Split(',')[1]);
            File.Delete(path);
      

        }
        LoadFiles(null, null);
    }
}