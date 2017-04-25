using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;
using System.Configuration;
using SMS.Business.Operation;
using System.IO;
using SMS.Business.Infrastructure;

public partial class Operations_BunkerAttachments : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();

            string Sample_ID = GetQueryString("id");
            string TypeID = GetQueryString("Type");

            hdnSample_ID.Value = Sample_ID;
            hdnTypeID.Value = TypeID;

            if (TypeID == "1")
            {
                ddlType1.Items.Add(new ListItem("Lab Report", "1"));
                ddlType2.Items.Add(new ListItem("Lab Report", "1"));
                ddlType3.Items.Add(new ListItem("Lab Report", "1"));
            }
            else
            {
                ddlType1.Items.Add(new ListItem("BDN", "2"));
                ddlType1.Items.Add(new ListItem("Others", "3"));
                ddlType2.Items.Add(new ListItem("BDN", "2"));
                ddlType2.Items.Add(new ListItem("Others", "3"));
                ddlType3.Items.Add(new ListItem("BDN", "2"));
                ddlType3.Items.Add(new ListItem("Others", "3"));

            }
            if (Sample_ID != "")
            {
                DataTable dt = BLL_OPS_BunkerAnalysis.Get_BunkerSampleAttachments(UDFLib.ConvertToInteger(Sample_ID), UDFLib.ConvertToInteger(TypeID), GetSessionUserID());
                string AttachmentName = "";
                string FileName = "";
                string UploadPath = ConfigurationManager.AppSettings["UploadPath"].ToString();

                if (dt.Rows.Count == 1)
                {
                    FileName = System.IO.Path.GetFileName(dt.Rows[0]["File_Name"].ToString());
                    AttachmentName = dt.Rows[0]["Attachment_Name"].ToString();
                    FileName = System.IO.Path.GetFileName(dt.Rows[0]["File_Name"].ToString());

                    HyperLink lnk = new HyperLink();
                    lnk.Text = AttachmentName + "<br>";
                    lnk.NavigateUrl = "../../" + UploadPath + "/BunkerAnalysis/" + FileName;
                    lnk.Target = "_blank";
                    Panel1.Controls.Add(lnk);

                }
                else if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AttachmentName = dr["Attachment_Name"].ToString();
                        FileName = System.IO.Path.GetFileName(dr["File_Name"].ToString());

                        HyperLink lnk = new HyperLink();
                        lnk.Text = AttachmentName + "<br>";
                        lnk.NavigateUrl = "../../" + UploadPath + "/BunkerAnalysis/" + FileName;
                        lnk.Target = "_blank";
                        Panel1.Controls.Add(lnk);
                    }
                }
            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.IsAdmin == 0)
        {
            if (objUA.View == 0)
            {
                Response.Write("You don't have sufficient previlege to access the requested page.");
                Response.End();
            }
            if (objUA.Add == 0)
            {
                lnkAddAttachment.Visible = false;
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
    }
    protected int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetQueryString(string Query)
    {
        try
        {
            if (Request.QueryString[Query] != null)
            {
                return Request.QueryString[Query].ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }

    protected void lnkAddAttachment_Click(object sender, EventArgs e)
    {
        lblmsg1.Text = "";
        lblmsg2.Text = "";
        lblmsg3.Text = "";
        string AttachmentName = "";
        string FileName = "";
        int AttachmentType;
        int RetVal = 0;
        int uploadedSuccessfully = 1;
        lblmsg1.Text = "";
        int Sample_ID = UDFLib.ConvertToInteger(hdnSample_ID.Value);
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("BNK_");
        if (dt.Rows.Count > 0)
        {
            string datasize = dt.Rows[0]["Size_KB"].ToString();
            if (FileUpload_1.HasFile)
            {
                if (FileUpload_1.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    AttachmentName = Path.GetFileName(FileUpload_1.FileName);
                    AttachmentType = UDFLib.ConvertToInteger(ddlType1.SelectedValue);
                    FileName = "BNK_" + Guid.NewGuid().ToString() + Path.GetExtension(AttachmentName);

                    FileUpload_1.SaveAs(MapPath("~/Uploads/BunkerAnalysis/") + FileName);
                    RetVal = BLL_OPS_BunkerAnalysis.Insert_BunkerSampleAttachment(Sample_ID, AttachmentType, AttachmentName.Replace(Path.GetExtension(AttachmentName), ""), FileName, GetSessionUserID());
                    lblmsg1.Text = "File Uploaded";
                    
                }
                else
                {
                    lblmsg1.Text = datasize + " KB File size exceeds max limit";
                    uploadedSuccessfully = 0;
                   
                }
            }
            if (FileUpload_2.HasFile)
            {
                if (FileUpload_2.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    AttachmentName = Path.GetFileName(FileUpload_2.FileName);
                    AttachmentType = UDFLib.ConvertToInteger(ddlType2.SelectedValue);
                    FileName = "BNK_" + Guid.NewGuid().ToString() + Path.GetExtension(AttachmentName);

                    FileUpload_2.SaveAs(MapPath("~/Uploads/BunkerAnalysis/") + FileName);
                    RetVal = BLL_OPS_BunkerAnalysis.Insert_BunkerSampleAttachment(Sample_ID, AttachmentType, AttachmentName.Replace(Path.GetExtension(AttachmentName), ""), FileName, GetSessionUserID());
                    lblmsg2.Text = "File Uploaded";
                }
                else
                {
                    lblmsg2.Text = datasize + " KB File size exceeds max limit";
                    uploadedSuccessfully = 0;    
                }
            }
            if (FileUpload_3.HasFile)
            {
                if (FileUpload_3.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    AttachmentName = Path.GetFileName(FileUpload_3.FileName);
                    AttachmentType = UDFLib.ConvertToInteger(ddlType3.SelectedValue);
                    FileName = "BNK_" + Guid.NewGuid().ToString() + Path.GetExtension(AttachmentName);

                    FileUpload_3.SaveAs(MapPath("~/Uploads/BunkerAnalysis/") + FileName);
                    RetVal = BLL_OPS_BunkerAnalysis.Insert_BunkerSampleAttachment(Sample_ID, AttachmentType, AttachmentName.Replace(Path.GetExtension(AttachmentName), ""), FileName, GetSessionUserID());
                    lblmsg3.Text = "File Uploaded";
                }
                else
                {
                    lblmsg3.Text = datasize + " KB File size exceeds max limit";
                    uploadedSuccessfully = 0;
                   // return;
                }              
            }
        }
        else
        {
            string js2 = "alert('Upload size not set!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
        if (uploadedSuccessfully == 1)
        {
            string js = "";
            if (RetVal > 0)
            {
                js = "alert('Attachment Uploaded!!');";
            }
            js += "window.parent.closeDiv('dvAttachments');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }
}