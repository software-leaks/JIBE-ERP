using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.DMS;
using System.Data;
using System.IO;



public partial class Crew_CrewVoyageDocuments : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_DMS_Admin objDMS = new BLL_DMS_Admin();
    public string CurrentDateFormatMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (getQueryString("CrewID") == null)
            Response.Redirect("CrewList.aspx");

        UserAccessValidation();
        CurrentDateFormatMessage = UDFLib.DateFormatMessage();
        if (!IsPostBack)
        {
            try
            {
                int CrewID = int.Parse(getQueryString("CrewID"));
                BindData();

                DataTable dt = objCrew.Get_CrewPersonalDetailsByID(CrewID);
                if (dt.Rows.Count > 0)
                {
                    lblCrewName.Text = dt.Rows[0]["staff_fullname"].ToString();
                    lblCurrentRank.Text = dt.Rows[0]["CurrentRank"].ToString();
                    lblStaffCode.Text = dt.Rows[0]["Staff_Code"].ToString();
                    lblNationality.Text = dt.Rows[0]["Country_Name"].ToString();
                    lblVessel.Text = dt.Rows[0]["Vessel_Short_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

        }
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
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();
            lblMessage.Text = "";

            DropDownList ddlYesNo = ((DropDownList)(GridView1.Rows[e.NewEditIndex].FindControl("ddlYN")));
            if (ddlYesNo != null)
            {
                ddlYesNo.SelectedValue = "1";
            }
            DropDownList ddlCountry = ((DropDownList)(GridView1.Rows[e.NewEditIndex].FindControl("ddlCountry")));
            BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
            DataTable dtt = objBLLCountry.Get_CountryList();
            ddlCountry.DataSource = dtt;
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
            HiddenField hdnId = new HiddenField();
            hdnId = (HiddenField)GridView1.Rows[GridView1.EditIndex].FindControl("hdnId");
            HiddenField hdnCountryId = new HiddenField();
            hdnCountryId = (HiddenField)GridView1.Rows[GridView1.EditIndex].FindControl("hdnCountryId");

            HiddenField hdnScannedMand = new HiddenField();
            hdnScannedMand = (HiddenField)GridView1.Rows[GridView1.EditIndex].FindControl("hdnScannedMand");
            Session["isScannedMandatory"] = Convert.ToString(hdnScannedMand.Value);

            if (!string.IsNullOrEmpty(hdnId.Value))
            {
                ddlCountry.SelectedItem.Text = hdnId.Value;
                ddlCountry.SelectedValue = hdnCountryId.Value;
            }
            else
                ddlCountry.SelectedIndex = 0;
        }
        catch
        {
        }


    }

    protected void GridView1_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            BindData();
            lblMessage.Text = "";

        }
        catch
        {
        }


    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];


            int CrewID = int.Parse(getQueryString("CrewID"));
            int DocID = 0;
            int DocTypeID;
            int AnswarYN = 1, RankID = 0;
            string DocName = "", Remark = "", DocNo = "", IssueDate = "", ExpiryDate = "", IssuePalce = "";
            string FileName = "";
            string FileExt = "";
            int IssueCountry = 0;

            DocTypeID = UDFLib.ConvertToInteger(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
            FileUpload file = ((FileUpload)(GridView1.Rows[e.RowIndex].FindControl("docUploader")));
            if (e.NewValues["AnswerYN"] != null)
                AnswarYN = UDFLib.ConvertToInteger(e.NewValues["AnswerYN"].ToString());
            if (e.NewValues["RankID"] != null)
                RankID = UDFLib.ConvertToInteger(e.NewValues["RankID"].ToString());
            if (e.NewValues["Remark"] != null)
                Remark = Convert.ToString(e.NewValues["Remark"]);
            if (e.NewValues["DocNo"] != null)
                DocNo = Convert.ToString(e.NewValues["DocNo"]);
           
            TextBox txtIssueDate = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("txtIssueDate"));
            if (txtIssueDate != null)
            {
                if (txtIssueDate.Text != "")
                {
                    if (!UDFLib.DateCheck(txtIssueDate.Text))
                    {
                        string js = "alert('Enter valid Issue Date"+CurrentDateFormatMessage+"');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                        return;
                    }
                    else
                    {
                        IssueDate = txtIssueDate.Text;
                    }
                }
                else
                {
                    string js = "alert('Enter Issue Date');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                    return;
                
                }

            }

            TextBox txtExpDate = (TextBox)(GridView1.Rows[e.RowIndex].FindControl("txtExpDate"));
            if (txtExpDate != null)
            {
                if (txtExpDate.Text != "")
                {
                    if (!UDFLib.DateCheck(txtExpDate.Text))
                    {
                        string js = "alert('Enter valid Expiry Date" + CurrentDateFormatMessage + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                        return;
                    }
                    else
                    {
                        ExpiryDate = txtExpDate.Text;
                    }
                }
                else
                {
                    string js = "alert('Enter Expiry Date');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                    return;
                }
                               
            }
            if (e.NewValues["IssuePlace"] != null)
                IssuePalce = Convert.ToString(e.NewValues["IssuePlace"]);
            int VoyageID = UDFLib.ConvertToInteger(getQueryString("VoyID"));
            int VoyageSpecific = UDFLib.ConvertToInteger(e.NewValues["VoyageSpecific"].ToString());
            
            IssueCountry = UDFLib.ConvertToInteger((GridView1.Rows[e.RowIndex].FindControl("ddlCountry") as DropDownList).SelectedItem.Value);
            DataTable dt = new DataTable();
            dt = objUploadFilesize.Get_Module_FileUpload("CWF_");

            string datasize = dt.Rows[0]["Size_KB"].ToString();

            if (AnswarYN == 1)


            {
                if (ValidateEntry(IssueDate, ExpiryDate) == true)
                {
                    if (file.FileName.Length > 0)//checking if user uploaded any file

                        if (file.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            lblMessage.Text = "";
                            Guid GUID = Guid.NewGuid();
                            FileName = file.FileName;
                            FileExt = Path.GetExtension(FileName).ToLower();
                            DocName = FileName.Replace(FileExt, "");
                            FileName = GUID.ToString() + FileExt;

                            int ContractDocTypeId = objDMS.Get_DocumentTypeId("CONTRACT");
                            if (ContractDocTypeId == DocTypeID)
                            {
                                if (FileExt == ".pdf")
                                {
                                    file.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));

                                    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
                                    System.Data.DataTable dtAgreement = objCrewBLL.Get_CrewAgreementRecords(CrewID, VoyageID, 0, GetSessionUserID());
                                    DataRow[] dr = dtAgreement.Select("agreement_stage = 1");
                                    if (dr.Length > 0)
                                    {
                                        ViewState["pagecount"] = dr[0]["PageCounts"].ToString();
                                    }
                                    else
                                    {
                                        ViewState["pagecount"] = 0;
                                    }
                                    int PageCount = getNumberOfPdfPages(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));
                                    if (PageCount >= Convert.ToInt32(ViewState["pagecount"]))
                                    {
                                        int expiryMandatory = objDMS.Check_Document_Expiry(DocTypeID);
                                        if (expiryMandatory == 1 && (ExpiryDate == "" || ExpiryDate == "1900/01/01"))
                                        {
                                            string js = "alert('ExpiryDate date is mandatory.');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                                            return;
                                        }

                                        DocID = objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), DocNo, IssueDate, IssuePalce, ExpiryDate, IssueCountry);
                                        if (VoyageSpecific == 1)
                                        {
                                            objCrew.UPDATE_DocumentChecklist(CrewID, DocID, DocTypeID, DocName, AnswarYN, RankID, Remark, DocNo, IssueDate, IssuePalce, ExpiryDate, GetSessionUserID(), FileName, VoyageID);
                                            objCrew.Insert_CrewAgreementRecord(CrewID, VoyageID, 3, 0, "Crew Agreement - Signed by Staff", FileName, FileName, GetSessionUserID(), PageCount);
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));
                                        string js = "alert('The PDF document you are trying to upload is invalid.Page count is not matching downloaded document.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                                    }
                                    GridView1.EditIndex = -1;
                                    BindData();
                                    Response.Redirect("~/Crew/CrewVoyageDocuments.aspx?CrewID=" + CrewID + "&VoyID=" + VoyageID, false);

                                }
                                else
                                {
                                    string js2 = "Only PDF documents are allowed to be uploaded as Crew Agreement.";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js2 + "');", true);
                                }
                            }
                            else
                            {
                                int expiryMandatory = objDMS.Check_Document_Expiry(DocTypeID);
                                if (expiryMandatory == 1 && (ExpiryDate == "" || ExpiryDate == "1900/01/01"))
                                {
                                    string js = "alert('ExpiryDate date is mandatory.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                                    return;
                                }
                                file.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));
                                DocID = objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), DocNo, UDFLib.ConvertToDefaultDt(IssueDate), IssuePalce, UDFLib.ConvertToDefaultDt(ExpiryDate), IssueCountry);
                                if (VoyageSpecific == 1)
                                {
                                    objCrew.UPDATE_DocumentChecklist(CrewID, DocID, DocTypeID, DocName, AnswarYN, RankID, Remark, DocNo, UDFLib.ConvertToDefaultDt(IssueDate), IssuePalce, UDFLib.ConvertToDefaultDt(ExpiryDate), GetSessionUserID(), FileName, VoyageID);
                                }
                                GridView1.EditIndex = -1;
                                BindData();
                                Response.Redirect("~/Crew/CrewVoyageDocuments.aspx?CrewID=" + CrewID + "&VoyID=" + VoyageID, false);
                            }
                        }
                        else
                        {
                            lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                        }


                    else
                    {
                        if (Convert.ToString(Session["isScannedMandatory"]) != "1")
                        {
                             DocID = objCrew.INS_CrewDocuments(CrewID, DocName, "", "", DocTypeID, GetSessionUserID(), DocNo, UDFLib.ConvertToDefaultDt(IssueDate), IssuePalce, UDFLib.ConvertToDefaultDt(ExpiryDate), IssueCountry);
                             if (VoyageSpecific == 1)
                             {
                                 objCrew.UPDATE_DocumentChecklist(CrewID, DocID, DocTypeID, DocName, AnswarYN, RankID, Remark, DocNo, UDFLib.ConvertToDefaultDt(IssueDate), IssuePalce, UDFLib.ConvertToDefaultDt(ExpiryDate), GetSessionUserID(), FileName, VoyageID);
                             }
                            GridView1.EditIndex = -1;
                            BindData();
                            Response.Redirect("~/Crew/CrewVoyageDocuments.aspx?CrewID=" + CrewID + "&VoyID=" + VoyageID, false);
                        }
                        else
                        {
                            string js = "Browse document to upload .";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
                        }
                    }
                }
            }
            else
            {
                if (ValidateEntry(IssueDate, ExpiryDate) == true)
                {
                    objCrew.UPDATE_DocumentChecklist(CrewID, DocID, DocTypeID, DocName, AnswarYN, RankID, Remark, DocNo, UDFLib.ConvertToDefaultDt(IssueDate), UDFLib.ConvertToDefaultDt(IssuePalce), ExpiryDate, GetSessionUserID(), FileName, VoyageID);
                    GridView1.EditIndex = -1;
                    BindData();
                    Response.Redirect("~/Crew/CrewVoyageDocuments.aspx?CrewID=" + CrewID + "&VoyID=" + VoyageID, false);
                }
            }



        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
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


                    if (File.Exists(Server.MapPath("~/Uploads/CrewDocuments/") + DocFileName))
                    {
                        img.NavigateUrl = "~/Uploads/CrewDocuments/" + DocFileName;
                    }
                    else
                    {
                        img.NavigateUrl = "~/FileNotFound.aspx";

                    }



                    img.Target = "_blank";
                }
                else
                    img.Visible = false;
            }
            int ContractDocTypeId = objDMS.Get_DocumentTypeId("CONTRACT");
            if (DocTypeID.Equals(ContractDocTypeId.ToString()))
            {
                int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
                int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

                System.Data.DataTable dt = objCrew.Get_CrewAgreementStatus(iVoyID, GetSessionUserID());

                DataRow[] dr = dt.Select("StepText like '%Contract Signed by Office%'");
                if (dr.Length == 0)
                {
                    ImageButton LinkButton2 = (ImageButton)e.Row.FindControl("LinkButton2");
                    if (LinkButton2 != null)
                    {
                        LinkButton2.Visible = false;
                        Image imgInfo = (Image)e.Row.FindControl("imgInfo");
                        if (imgInfo != null)
                        {
                            imgInfo.Visible = true;
                            imgInfo.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Contract not signed by office] body=[Contract is not yet signed by office. Please wait for the office to sign the contract.]");
                        }
                    }
                }
            }

            //DropDownList ddlCountry = (e.Row.FindControl("ddlCountry") as DropDownList);
            //BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
            //DataTable dtt = objBLLCountry.Get_CountryList();
            //ddlCountry.DataSource = dtt;
            //ddlCountry.DataTextField = "Country";
            //ddlCountry.DataValueField = "ID";
            //ddlCountry.DataBind();
            //ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
            //string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //ddlCountry.Items.FindByValue(country).Selected = true;
        }
    }

    protected void BindData()
    {
        int CrewID = int.Parse(getQueryString("CrewID"));
        int VoyageID = int.Parse(getQueryString("VoyID"));
        DataTable dtDocs = objCrew.Get_Crew_VoyageDocuments(CrewID, VoyageID);
        GridView1.DataSource = dtDocs;
        GridView1.DataBind();
    }

    protected Boolean ValidateEntry(string IssueDate, string ExpiryDate)
    {
        Boolean ret = true;
        string msg = "";

        if (IssueDate == "")
        {
            ret = false;
            msg = "Issue Date is mandatory ";
        }
        if (IssueDate != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(IssueDate));
            }
            catch
            {
                ret = false;
                msg = "Enter valid ISSUE DATE" + CurrentDateFormatMessage;
            }
        }
        if (ExpiryDate != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(ExpiryDate));
            }
            catch
            {
                ret = false;
                msg = "Enter valid EXPIRY DATE" + CurrentDateFormatMessage;
            }
        }
        if (IssueDate != "" && ExpiryDate != "")
        {
            try
            {
                DateTime dtExpiryDate = DateTime.Parse(UDFLib.ConvertToDefaultDt(ExpiryDate));
                DateTime dtIssueDate = DateTime.Parse(UDFLib.ConvertToDefaultDt(IssueDate));
                if (dtIssueDate > dtExpiryDate)
                {
                    ret = false;
                    msg = "Issue date cannot be greater than Expiry date";
                }
            }
            catch
            {
                ret = false;
                msg = "Invalid entry in DATE field.";
            }
        }
        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgValidation", js, true);
        }
        return ret;
    }

    public void SendMail(string To, string CC, string From, string Sub, string MailBody, string AttachmentPath)
    {
    }

    protected int getNumberOfPdfPages(string fileName)
    {
        int RetVal = 0;
        try
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
                System.Text.RegularExpressions.MatchCollection matches = regex.Matches(sr.ReadToEnd());

                RetVal = matches.Count;
            }
        }
        catch { }

        return RetVal;
    }

  

}