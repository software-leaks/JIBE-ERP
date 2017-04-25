using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.LMS;
using System.Data;
using AjaxControlToolkit4;
using System.IO;
using SMS.Business.Infrastructure;
using SMS.Business.FAQ;
public partial class LMS_ItemDetails : System.Web.UI.Page
{
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    string msgmodal;
   
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtSize = objUploadFilesize.Get_Module_FileUpload("LMS_");
        if (dtSize.Rows.Count > 0)
        {
            AjaxFileUpload1.MaximumSizeOfFile = UDFLib.ConvertToInteger(dtSize.Rows[0]["size_kb"].ToString());
            hiddenSize.Value = AjaxFileUpload1.MaximumSizeOfFile.ToString();
        }
        
        UserAccessValidation();
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            if (!IsPostBack)
            {
                Session["vsAttachmentData"] = null;
                Session["vsAttachmentFileName"] = null;

                FillItemType();

            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
        //if (objUA.View == 0)
        //    Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSaveandAddNew.Visible = false;
            btnUploadclick.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            btnSaveandAddNew.Visible = false;
            btnUploadclick.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {

        }
    }


    protected void btnSaveItemDetails_Click(object sender, EventArgs e)
    {


        if (Session["vsAttachmentData"] == null && (ddlItemType.SelectedValue == "1"))
        {
            msgmodal = String.Format("alert('Please upload the file to save !');showDiv('dvAddItem');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
            return;
        }

        if (Session["vsAttachmentFileName"] != null)
        {
            string ITEM_PATH = "";
            int IsItemExist = 0;
            if (Check_Duplicate_AttachmentFile(Path.GetFileName(UDFLib.ConvertStringToNull(Session["vsAttachmentFileName"])), txtItemName.Text, ref ITEM_PATH, ref IsItemExist))
            {

                if (IsItemExist == 1)
                {
                    msgmodal = String.Format("alert('Item Name already exists.');showDiv('dvAddItem');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
                    return;
                }

                else
                {
                    string sPath = "../uploads/TrainingItems/" + ITEM_PATH;
                    msgmodal = String.Format("Check_Duplicate_AttachmentFile('" + sPath + "');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);

                }

            }

            else
            {
                AddItem(sender);

            }
        }

        else
        {
            AddItem(sender);


        }

    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            Session["vsAttachmentData"] = file.GetContents();
            Session["vsAttachmentFileName"] = file.FileName;


        }
        catch (Exception ex)
        {

        }

    }


    protected void AddItem(object sender)
    {
        string fileName;
        string Attachment_Name;
        Byte[] fileBytes = (Byte[])Session["vsAttachmentData"];

        if (Session["vsAttachmentFileName"] != null)
        {
            fileName = Session["vsAttachmentFileName"].ToString();

        }

        else
        {
            fileName = string.Empty;
        }

        string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\TrainingItems");
        Guid GUID = Guid.NewGuid();
        Attachment_Name = Path.GetFileName(fileName);
        string Flag_Attach = "LMS_" + GUID.ToString() + Path.GetExtension(fileName);
        int ItemId = 0;

        if (ddlItemType.SelectedValue == "2")
        {
            Flag_Attach = GetPathforFBMNumber();
        }
        int Result = BLL_LMS_Training.Ins_Training_Items(txtItemName.Text.Trim(), txtDescription.Text.Trim(), UDFLib.ConvertStringToNull(ddlItemType.SelectedItem),
            txtDuration.Text, Flag_Attach, ddlItemType.SelectedValue == "1" ? Attachment_Name : null, Convert.ToInt32(Session["USERID"]), 1, ref ItemId);

        if (ddlItemType.SelectedValue == "1")
        {
            string FullFilename = Path.Combine(sPath, "LMS_" + GUID.ToString() + Path.GetExtension(fileName));
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
        }

        Session["vsAttachmentData"] = null;
        Session["vsAttachmentFileName"] = null;
        txtItemName.Text = "";
        txtDuration.Text = "";
        txtDescription.Text = "";

        if (ddlItemType.SelectedValue == "2")
        {
            txtItemName.Text = "";
            ddlFBMNumber.Visible = true;
            txtItemName.Enabled = false;
            if (ViewState["dt_FBM_Number"] != null)
            {
                ddlFBMNumber.DataSource = (ViewState["dt_FBM_Number"]);
                ddlFBMNumber.DataBind();
            }
            else
            {
                FillFBMNumber();
                txtItemName.Text = ddlFBMNumber.SelectedValue;
                txtItemName.Enabled = false;
            }
            if (txtItemName.Text == "")
            {
                txtItemName.Text = ddlFBMNumber.SelectedValue;
                txtItemName.Enabled = false;
            }
        }

        if (Result >= 0)
        {
            if ((sender as Button).ID.ToUpper() == "BTNSAVEANDCLOSE")
            {
                string js = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);
            }
        }

    }

    private string GetPathforFBMNumber()
    {
        string strPath = String.Empty;
        DataTable dt_FBM_Number;
        if (ViewState["dt_FBM_Number"] != null)
        {
            dt_FBM_Number = (DataTable)ViewState["dt_FBM_Number"];

            var query = from dr in dt_FBM_Number.AsEnumerable()
                        where dr.Field<string>("FBM_Number").Contains(ddlFBMNumber.SelectedValue)
                        select dr;

            DataTable dt = query.CopyToDataTable();
            DataRow drow = dt.Rows[0];
            strPath = "FBM_Main_Report_Details.aspx?FBMID=" + drow.Field<int>("ID") + "&UserID=" + drow.Field<int>("CREATED_BY");
        }

        return strPath;
    }


    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["vsAttachmentData"] = null;
        Session["vsAttachmentFileName"] = null;

        if (ViewState["previtem"] != null)
            if (ViewState["previtem"].ToString() == "2")
                txtItemName.Text = "";

        lblAttachment.Visible = false;
        AjaxFileUpload1.Visible = false;
        ddlFBMNumber.Visible = false;
        txtItemName.Enabled = true;
        
        
        if (ddlItemType.SelectedValue == "3" || ddlItemType.SelectedValue == "4")
        {

        }

        else if (ddlItemType.SelectedValue == "2")
        {
            txtItemName.Text = "";
            ddlFBMNumber.Visible = true;
            txtItemName.Enabled = false;
            if (ViewState["dt_FBM_Number"] != null)
            {
                ddlFBMNumber.DataSource = (ViewState["dt_FBM_Number"]);
                ddlFBMNumber.DataBind();
            }
            else
            {
                FillFBMNumber();
                txtItemName.Text = ddlFBMNumber.SelectedValue;
                txtItemName.Enabled = false;
            }
            if (txtItemName.Text == "")
            {
                txtItemName.Text = ddlFBMNumber.SelectedValue;
                txtItemName.Enabled = false;
            }
        }

        else
        {
            lblAttachment.Visible = true;
            AjaxFileUpload1.Visible = true;
        }

        msgmodal = String.Format("showDiv('dvAddItem');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
        ViewState["previtem"] = ddlItemType.SelectedValue;
    }

    protected void ddlFBMNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFBMNumber.SelectedIndex != -1)
        {
            txtItemName.Text = ddlFBMNumber.SelectedValue;
            txtItemName.Enabled = false;
        }

        msgmodal = String.Format("showDiv('dvAddItem');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
    }


    protected void FillItemType()
    {

        ListItem li = new ListItem("RESOURCE", "1");
        ddlItemType.Items.Insert(0, li);
        ListItem li1 = new ListItem("FBM", "2");
        ddlItemType.Items.Insert(1, li1);
        ListItem li2 = new ListItem("ARTICLE", "3");
        ddlItemType.Items.Insert(2, li2);
        ListItem li3 = new ListItem("VIDEO MATERIALS", "4");
        ddlItemType.Items.Insert(2, li3);

    
    }

    protected void FillFBMNumber()
    {
        DataTable dt_FBM_Number = BLL_LMS_Training.GET_FBM_Number();
        ddlFBMNumber.DataSource = dt_FBM_Number;
        ddlFBMNumber.DataBind();
        ViewState["dt_FBM_Number"] = dt_FBM_Number;
    }

    private Boolean Check_Duplicate_AttachmentFile(string AttachmentFile, string ITEM_NAME, ref string ITEM_PATH, ref int IsItemExist)
    {
        Boolean Result = false;
        if (BLL_LMS_Training.Check_Duplicate_AttachmentFile(AttachmentFile, ITEM_NAME, ref ITEM_PATH, ref IsItemExist) == 1)
        {
            Result = true;
        }
        return Result;
    }

    protected void saveitem_Click(object sender, EventArgs e)
    {
        AddItem(sender);

    }

}