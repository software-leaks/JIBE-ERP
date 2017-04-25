using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Configuration;
using System.IO;
using SMS.Business.LMS;
using AjaxControlToolkit4;
using System.Web;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SMS.Business.FAQ;

public partial class LMS_LMS_FAQ_Builder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModule();

            if (Request.QueryString["FAQ_ID"] != null)
            {
                DataTable dt = BLL_LMS_FAQ.Get_FAQ_Details(Convert.ToInt16(Request.QueryString["FAQ_ID"]));
                txtquestion.Text = Convert.ToString(dt.Rows[0]["Question"]);

                if (dt.Rows[0]["Sync_To_Vessel"].ToString() == "1")
                {
                    chkvessel.Checked = true;
                }

                if (ddlModule.Items.FindByValue(dt.DefaultView[0]["Module_ID"].ToString()) != null)
                    ddlModule.SelectedValue = dt.DefaultView[0]["Module_ID"].ToString() != "" ? dt.DefaultView[0]["Module_ID"].ToString() : "0";
                else
                    ddlModule.SelectedValue = "0";

                BindTopic();

                if (ddlTopic.Items.FindByValue(dt.DefaultView[0]["Topic_ID"].ToString()) != null)
                    ddlTopic.SelectedValue = dt.DefaultView[0]["Topic_ID"].ToString() != "" ? dt.DefaultView[0]["Topic_ID"].ToString() : "0";
                else
                    ddlTopic.SelectedValue = "0";

              

                hdnFaq_Id.Value = UDFLib.ConvertToInteger(Request.QueryString["FAQ_ID"]).ToString();
                txtProcedureSectionDetails.Content = "<html><body>" + Convert.ToString(dt.Rows[0]["Answer"]) + "</body></html>";
                btnSave_FAQ.Visible = false;
                btn_Update_FAQ.Visible = true;
                btnPublishToCrew.Enabled = true;
                btnAttach.Visible = true;
            }
            else
            {
                btnSave_FAQ.Visible = true;
                btn_Update_FAQ.Visible = false;
                btnPublishToCrew.Enabled = false;
                ListItem liselect = new ListItem("--Select--", "0", true);
                ddlTopic.Items.Clear();
                ddlTopic.Items.Insert(0, liselect);
                ddlModule.SelectedValue = "0";
            }
            Session["UploadsFiles_Name"] = "";

        }
        string js = "initScripts();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
    }
    protected void BindModule()
    {
        ddlModule.Items.Clear();
        ddlModule.DataTextField = "Module_Description";
        ddlModule.DataValueField = "Module_ID";
        ddlModule.DataSource = BLL_FAQ_Item.Get_FAQModule_List().Tables[0];
        ddlModule.DataBind();
        ListItem li = new ListItem("--Select--", "0");
        ddlModule.Items.Insert(0, li);
       
    }
    protected void BindTopic()
    {
        ListItem liselect = new ListItem("--Select--", "0", true);
        ddlTopic.Items.Clear();
        if (ddlModule.SelectedValue != "0")
        {
            DataTable dtproj = BLL_FAQ_Item.Get_FAQTopic_List(Convert.ToInt32(ddlModule.SelectedValue)).Tables[0];

            ddlTopic.DataSource = dtproj;

            ddlTopic.DataValueField = "Topic_ID";
            ddlTopic.DataTextField = "Description";
            ddlTopic.DataBind();
        }
        ddlTopic.Items.Insert(0, liselect);
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTopic();
    }
    protected void btnInsertPic_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Files.Count > 0 && txtProcedureSectionDetails.ActiveMode == AjaxControlToolkit.HTMLEditor.ActiveModeType.Design)
            {
                //string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/");


                try
                {
                    int Max_Height = 400;
                    //int Max_Width = 400;

                    System.Text.StringBuilder size = new System.Text.StringBuilder();

                    //string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["FAQAttachmentPath"] + "/");
                    string sPath = Server.MapPath("~/") + "Uploads\\FAQ\\";
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile postedFile = Request.Files[i];
                        string FileName = "FAQ_" + Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
                        string FullFilename = Path.Combine(sPath, FileName);
                        hdnAttacment_Id.Value = Convert.ToString(FileName);
                        postedFile.SaveAs(FullFilename);

                        ////BLL_LMS_FAQ.Faq_List_Attachment (Convert.ToInt32(Session["AttLoadId"]), FileName, Convert.ToInt32(Session["userid"]));
                        //DataTable dt_FAQ_Attachments = (DataTable)Session["dt_FAQ_Attachments"];
                        //DataRow dr = dt_FAQ_Attachments.NewRow();
                        //dr["Attachment_Name"] = FileName;
                        //dt_FAQ_Attachments.Rows.Add(dr);
                        //Session["dt_FAQ_Attachments"] = dt_FAQ_Attachments;

                        if (postedFile.ContentLength > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(postedFile.FileName))
                            {
                                System.Drawing.Image objUpdImg = System.Drawing.Image.FromFile(Path.Combine(sPath, FileName));

                                if (objUpdImg.Height > Max_Height)
                                    size.Append("height:" + Max_Height.ToString() + "px;");

                                //if (objUpdImg.Width > Max_Width)
                                //    size.Append("width:" + Max_Width.ToString() + "px;");
                                //txtProcedureSectionDetails.Content += "<img style='" + size.ToString() + "'  src='/" + ConfigurationManager.AppSettings["FAQAttachmentPath"] + "/" + FileName + "'>";
                                //txtProcedureSectionDetails.Content += "<P><img style='" + size.ToString() + "'  src='" + "" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "Uploads/FAQ" + "/" + FileName + "'></P>";
                                txtProcedureSectionDetails.Content += "<P><img style='" + size.ToString() + "'  src='" + "../Uploads/FAQ" + "/" + FileName + "'></P>";
                                size.Clear();
                                //Session.Clear();
                            }
                        }
                    }

                }
                catch { }
                finally
                {
                    //updInserImage.Update();
                    Session["UploadedFiles_Name"] = "";
                }

            }
            else
            {
                //ucError.Text = Request.Files.Count > 0 ? "You are not in design mode" : "No Picture selected.";
                //ucError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //ucError.ErrorMessage = ex.Message;
            //ucError.Visible = true;
        }
    }
    public void CleartextBoxes1()
    {
        foreach (Control Cleartext in this.Controls)
        {

            if (Cleartext is TextBox)
            {

                ((TextBox)Cleartext).Text = string.Empty;

            }

        }
    }
    protected void btnSave_FAQ_Click(object sender, EventArgs e)
    {
        //if (!check(txtquestion.Text.ToString(), (txtProcedureSectionDetails.Content)))
        //{
        //    string js = "alert('Faq Question AllReady Exist!');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
        //    txtquestion.Focus();
        //    return;
        //}
        if (ddlModule.SelectedValue == "0")
        {
            string js2 = "alert('Please select Module to save FAQ!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert11", js2, true);
            ddlTopic.Focus();
            return;
        }
        if (ddlTopic.SelectedValue == "0")
        {
            string js1 = "alert('Please select Topic to save FAQ!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert10", js1, true);
            ddlTopic.Focus();
            return;
        }
        int FAQ_ID = Get_FAQ_ID();
        string successmsg = "";
        if (FAQ_ID == 0)
            successmsg = "FAQ created successfully!";
        else
            successmsg = "FAQ updated successfully!";
        int Sync_To_Vessel = 0;
        if (chkvessel.Checked)
        {
            Sync_To_Vessel = 1;
        }

        try
        {
            // Validate your data, if any
            //FAQ_ID = BLL_LMS_FAQ.Upd_Faq_Details(FAQ_ID, txtquestion.Text.Trim(), txtProcedureSectionDetails.Content, UDFLib.ConvertToInteger(Session["userid"]));
            FAQ_ID = BLL_LMS_FAQ.Upd_Faq_Details(FAQ_ID, txtquestion.Text.Trim(), txtProcedureSectionDetails.Content, UDFLib.ConvertToInteger(Session["userid"]), UDFLib.ConvertToInteger(ddlTopic.SelectedValue));
            if (FAQ_ID > 0)
            {
                DataTable dt_FAQ_Attachments = new DataTable();
                dt_FAQ_Attachments.Columns.Add("ID", typeof(Int32));
                dt_FAQ_Attachments.Columns.Add("ATTACHMENT_NAME", typeof(String));

                string[] FAQ_Attachs = txtProcedureSectionDetails.Content.Split(new string[] { "Uploads/FAQ/" }, StringSplitOptions.None);

                foreach (string Attach in FAQ_Attachs)
                {
                    if (Attach.StartsWith("FAQ_", StringComparison.OrdinalIgnoreCase))
                    {
                        DataRow dr = dt_FAQ_Attachments.NewRow();
                        dr["ID"] = FAQ_ID.ToString();
                        dr["ATTACHMENT_NAME"] = Attach.Substring(0, 44);
                        dt_FAQ_Attachments.Rows.Add(dr);
                    }
                }

                DataTable dtAttachToDelete = BLL_LMS_FAQ.Ins_Attachment(FAQ_ID, Convert.ToInt32(Session["userid"]), dt_FAQ_Attachments, Sync_To_Vessel);


                // delete file not in use
                foreach (DataRow drAttach in dtAttachToDelete.Rows)
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/FAQ/" + drAttach["ATTACHMENT_NAME"]));
                    BLL_LMS_FAQ.Del_Faq_Attachment(Convert.ToString(drAttach["ATTACHMENT_NAME"]), Sync_To_Vessel);
                }
                btnPublishToCrew.Enabled = true;
                btnSave_FAQ.Visible = false;
                btn_Update_FAQ.Visible = true;
                string js = "alert('" + successmsg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js, true);
                btnSave_FAQ.Attributes.Add("OnClick", "self.close()");
            }
            else
            {
                btnPublishToCrew.Enabled = false;
                string js = "alert('Question and Answer is mandatory field...!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            }

            hdnFaq_Id.Value = UDFLib.ConvertToInteger(FAQ_ID).ToString();
        }
        catch (Exception ee)
        {
            //"Error occured. " + ee.Message;
        }
    }
    //This function is called after every page is created
    private void On_AfterRenderPage(object sender, EO.Pdf.PdfPageEventArgs e)
    {
        EO.Pdf.PdfPage page = e.Page;
        EO.Pdf.Acm.AcmRender render = new EO.Pdf.Acm.AcmRender(page, 0, new EO.Pdf.Acm.AcmPageLayout(new EO.Pdf.Acm.AcmPadding(0, 0, 0, 0)));
        //render.SetDefPageSize(new SizeF(EO.Pdf.PdfPageSizes.A4.Width, EO.Pdf.PdfPageSizes.A4.Height));
        EO.Pdf.Acm.AcmBlock footer = new EO.Pdf.Acm.AcmBlock(new EO.Pdf.Acm.AcmText("."));
        footer.Style.Top = 10.4f;
        //footer.Style.BackgroundColor = Color.Blue;
        //footer.Style.ForegroundColor = Color.Blue;
        render.Render(footer);
    }
    protected void btnPublishToCrew_Click(object sender, EventArgs e)
    {
        int ItemId = 0;
        string sFileName = "CTRN_" + Guid.NewGuid().ToString() + ".pdf";
        string htmlStr = "";
        string Question = "";
        string Answer = "";
        if (Get_FAQ_ID() > 0)
        {

            DataTable dt = BLL_LMS_FAQ.Get_FAQ_Details(Get_FAQ_ID());
            Question = Convert.ToString(dt.Rows[0]["Question"]);
            Answer = Convert.ToString(dt.Rows[0]["Answer"]);
            htmlStr += "<table style='width:100%' ><tr><td style='font-size:14;color:navy;font-family:verdana;border-bottom:1px solid black' ><b> " + Question + "</b></td> </tr> <tr> <td  style='font-size:11;color:black;font-family:verdana' >" + Answer + "</td> </tr></table> ";
            string filePath = Server.MapPath("~/") + "Uploads\\TrainingItems\\" + sFileName;
            EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
            // This use For IIS
            EO.Pdf.HtmlToPdf.Options.BaseUrl = "http://" + HttpContext.Current.Request.Url.Host + "/";
            //This use For Local machine
            EO.Pdf.HtmlToPdf.ConvertHtml(@"<html><head></head><body><div>" + htmlStr + "</div></body></html> ", filePath);
            if (Question.Length > 250)
            {
                Question = Question.Substring(0, 250);
            }
            int Result = BLL_LMS_Training.Ins_Training_Items(Question, null, "ARTICLE", "0", sFileName, sFileName, Convert.ToInt32(Session["USERID"]), 1, ref ItemId);

            string js = "alert(' FAQ published successfully! !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            txtquestion.Text = string.Empty;
            txtProcedureSectionDetails.Content = string.Empty;

        }
        else
        {
            string js = "alert('Please save the FAQ to proceed!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert3", js, true);
        }
    }
    public bool check(string s, string a)
    {
        //DataTable dt = BLL_LMS_FAQ.Check_Faq_List(s,a);
        DataTable dt = BLL_LMS_FAQ.Check_Faq_List(s, a, UDFLib.ConvertToInteger(ddlTopic.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private int Get_FAQ_ID()
    {
        if (UDFLib.ConvertToInteger(Request.QueryString["FAQ_ID"]) == 0)
            return UDFLib.ConvertToInteger(hdnFaq_Id.Value);
        else
            return UDFLib.ConvertToInteger(Request.QueryString["FAQ_ID"]);
    }
    protected void btnAttach_Click(object sender, EventArgs e)
    {
        String msgretv = String.Format("OpenPopupWindowBtnID('POP__ChapterDetails', 'Chapter Details','LMS_AttachItem_Details.aspx?FAQ_ID=" + hdnFaq_Id.Value + "', 'popup', 800, 1000, null, null, false, false, true, false,'" + null + "')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

    }
}