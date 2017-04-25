using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.DMS;
using SMS.Business.Infrastructure;
using System.Data;
using System.IO;

public partial class Crew_CrewPrepDocuments : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_DMS_Admin objDMS = new BLL_DMS_Admin();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (getQueryString("CrewID") == null)
            Response.Redirect("CrewList.aspx");

        DateFormat = UDFLib.GetDateFormat();//Get User date format
        calFrom.Format = DateFormat;
        calExpDate.Format = DateFormat;

        if (!IsPostBack)
        {
            try
            {
                Load_DocumentGroupList();
                BindData();
                BindCountry();
                DataTable dt = objCrew.Get_CrewPersonalDetailsByID(int.Parse(getQueryString("CrewID")));
                if (dt.Rows.Count > 0)
                {
                    lblCrewName.Text = dt.Rows[0]["staff_fullname"].ToString();
                    lblRank.Text = dt.Rows[0]["rank_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }
    }
    public void Load_DocumentGroupList()
    {
        DataTable dt = objDMS.Get_GroupList();

        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "GroupID";
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
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

        //-- MANNING OFFICE LOGIN --
        if (Session["UTYPE"].ToString() == "MANNING AGENT")
        {

        }
        //-- VESSEL MANAGER -- //
        else if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
        {

        }
        else//--- CREW TEAM LOGIN--
        {

        }
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

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        trEdit.Visible = true;
        int CrewID = int.Parse(getQueryString("CrewID"));
        int DocTypeID = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        DataTable dtDocs = objCrew.Get_Crew_PerpetualDocument(CrewID, DocTypeID);
        if (dtDocs.Rows.Count > 0)
        {
            lblDocName.Text = dtDocs.Rows[0]["DocTypeName"].ToString();
            txtDocGroupName.Text = dtDocs.Rows[0]["GroupName"].ToString();
            txtIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtDocs.Rows[0]["DateOfIssue"]));
            txtExpDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtDocs.Rows[0]["DateOfExpiry"]));
            txtIssuePlace.Text = dtDocs.Rows[0]["PlaceOfIssue"].ToString();
            txtDocNo.Text = dtDocs.Rows[0]["DocNo"].ToString();
            lblDocTypeId.Text = dtDocs.Rows[0]["DocTypeID"].ToString();
            Session["isScannedMandatory"] = dtDocs.Rows[0]["isScannedDocMandatory"].ToString();

            if (!string.IsNullOrEmpty(dtDocs.Rows[0]["CountryOfIssue"].ToString()))
            {
                ddlCountry.SelectedValue = dtDocs.Rows[0]["CountryOfIssue"].ToString();
            }
            else
            {
                ddlCountry.SelectedIndex = 0;

            }
            try
            {
                GridView1.EditIndex = e.NewEditIndex;
                BindData();
                DropDownList ddlYesNo = ((DropDownList)(GridView1.Rows[e.NewEditIndex].FindControl("ddlYN")));
                if (ddlYesNo != null)
                    ddlYesNo.SelectedValue = "1";

                 ScriptManager.RegisterStartupScript(this, this.GetType(), "BindHeight", "BindHeight();", true);
            }
            catch(Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string DocTypeID = DataBinder.Eval(e.Row.DataItem, "DocTypeID").ToString();
            string DocFileName = DataBinder.Eval(e.Row.DataItem, "DocFileName").ToString();

            HyperLink img = (HyperLink)e.Row.FindControl("ImgAttachment");
            if (img != null)
            {
                if (DocFileName != "")
                {
                    img.NavigateUrl = "~/Uploads/CrewDocuments/" + DocFileName;
                    img.Target = "_blank";
                }
                else
                    img.Visible = false;
            }
        }
    }

    protected void BindData()
    {
        try
        {


            int CrewID = int.Parse(getQueryString("CrewID"));
            int DocumentGroupId = int.Parse(ddlGroup.SelectedValue.ToString());
            string SearchText = txtSearchText.Text;

            DataTable dtDocs = objCrew.Get_Crew_PerpetualDocuments(CrewID, SearchText, DocumentGroupId);
            GridView1.DataSource = dtDocs;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void LoadDocumentList(object sender, EventArgs e)
    {
        try
        {
            BindData();
            trEdit.Visible = false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected Boolean ValidateEntry(string IssueDate, string ExpiryDate)
    {
        return true;
    }

    public void SendMail(string To, string CC, string From, string Sub, string MailBody, string AttachmentPath)
    {
    }

    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        BindData();
        trEdit.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (Page.IsValid)
        {
            try
            {
                int CrewID = int.Parse(getQueryString("CrewID"));
                int DocID = 0;
                int DocTypeID;

                string DocName = "";
                string FileName = "";
                string FileExt = "";

                DocTypeID = int.Parse(lblDocTypeId.Text.ToString());
                DataTable dt = new DataTable();
                dt = objUploadFilesize.Get_Module_FileUpload("CDOC_");
                if (dt.Rows.Count > 0)
                {
                    string DocNo = txtDocNo.Text.ToString();
                    string IssueDate = txtIssueDate.Text.ToString();
                    string ExpiryDate = txtExpDate.Text.ToString();
                    string IssuePalce = txtIssuePlace.Text.ToString();


                    if (docUploader != null && docUploader.FileName.Trim() != "")
                    {
                        string datasize = dt.Rows[0]["Size_KB"].ToString();
                        if (ValidateEntry(IssueDate, ExpiryDate) == true)
                        {

                            if (FileExt == ".exe" || FileExt == ".bat" || FileExt == ".dll" || FileExt == ".com")
                            {
                                string js = "Please note .exe,.bat,.dll and .com can not be upload";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile1", "alert('" + js + "');", true);
                                return;
                            }

                            if (docUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                            {
                                Guid GUID = Guid.NewGuid();
                                FileName = docUploader.FileName;
                                FileExt = Path.GetExtension(FileName).ToLower();
                                DocName = FileName.Replace(FileExt, "");
                                FileName = GUID.ToString() + FileExt;
                            }
                            else
                            {
                                lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                                return;
                            }
                            docUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));


                            int expiryMandatory = objDMS.Check_Document_Expiry(DocTypeID);
                            if (expiryMandatory == 1 && (ExpiryDate == "" || ExpiryDate == "1900/01/01"))
                            {
                                lblMessage.Text = "ExpiryDate date is mandatory";
                                return;
                            }
                            DocID = objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), DocNo, UDFLib.ConvertToDefaultDt(IssueDate), IssuePalce, UDFLib.ConvertToDefaultDt(ExpiryDate), UDFLib.ConvertToInteger(ddlCountry.SelectedValue.ToString()));

                            string js1 = "Document has successfully uploaded !!.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile1", "BindHeight();alert('" + js1 + "');", true);
                            trEdit.Visible = false;
                            GridView1.EditIndex = -1;
                            BindData();
                        }
                    }
                    else
                    {
                        if (Convert.ToString(Session["isScannedMandatory"]) != "1")
                        {
                            if (ValidateEntry(Convert.ToString(txtIssueDate.Text), Convert.ToString(txtExpDate.Text)) == true)
                            {
                                int expiryMandatory = objDMS.Check_Document_Expiry(DocTypeID);
                                if (expiryMandatory == 1 && (ExpiryDate == "" || ExpiryDate == "1900/01/01"))
                                {
                                    lblMessage.Text = "ExpiryDate date is mandatory";
                                    return;
                                }
                                DocID = objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), DocNo, UDFLib.ConvertToDefaultDt(IssueDate), IssuePalce, UDFLib.ConvertToDefaultDt(ExpiryDate), UDFLib.ConvertToInteger(ddlCountry.SelectedValue.ToString()));
                                string js1 = "Document has successfully uploaded !!.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile1", "BindHeight();alert('" + js1 + "');", true);
                                trEdit.Visible = false;
                                GridView1.EditIndex = -1;
                                BindData();
                            }


                        }

                        else
                        {
                            string js = "Select Document";
                            trEdit.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    trEdit.Visible = true;
                    string js2 = "alert('Upload size not set!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }
            }

            catch
            {
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trEdit.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "BindHeight", "BindHeight();", true);
    }

    #region bind Country
    protected void BindCountry()
    {
        BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
        DataTable dt = objBLLCountry.Get_CountryList();
        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";

        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    #endregion
}