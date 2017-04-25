using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.PortageBill;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Xml;
using System.IO;
using System.Drawing;
using AjaxControlToolkit4;


public partial class Crew_CrewContract : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Contract objBLLCrewContract = new BLL_Crew_Contract();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    DataView dataview;
    protected decimal TotalAmount;
    string FooterText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            btnLoadFiles.Attributes.Add("style", "visibility:hidden");
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            if (!IsPostBack)
            {
                UserAccessValidation();

                int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
                int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

                Session["hdnCrewID"] = iCrewID.ToString();
                Session["hdnVoyID"] = iVoyID.ToString();
                int Wages = BLL_PortageBill.CRW_GET_Wage_Status(iCrewID, iVoyID);
                if (Wages == 0)
                {
                    Response.Write("<h1>Contract can not be printed at this moment as Salary is not entered for the Crew</h1>");
                    Response.End();
                }

                Load_Agreements();
                Load_Agreement_Status();

                System.Data.DataTable dt = objCrewBLL.Get_CrewAgreementData(iCrewID, iVoyID);

                frmCrewDetails.DataSource = dt;
                frmCrewDetails.DataBind();

                DataTable dtSL = objCrewBLL.Get_SideLetter_ForVoyage(iVoyID, iCrewID, GetSessionUserID());
                if (dtSL.Rows.Count > 0)
                {
                    pnlSideLetterExists.Visible = true;
                    lblSideLetter.Text = "There is a side letter of US$ " + dtSL.Rows[0]["Amount"].ToString() + " exists for this contract";
                }
                else
                {
                    pnlSideLetterExists.Visible = false;
                }
            }
        }
    }

    protected string GetBrowserName()
    {
        string Browser = "";
        string ClientBrowser = Request.UserAgent;

        if (ClientBrowser.IndexOf("MSIE") != -1) Browser = "MSIE";
        if (ClientBrowser.IndexOf("Chrome") != -1) Browser = "Chrome";
        if (ClientBrowser.IndexOf("Firefox") != -1) Browser = "Firefox";
        if (ClientBrowser.IndexOf("Safari") != -1) Browser = "Safari";
        
        return Browser;        
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

    protected void rpt2_ItemCreated(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalAmt = (Label)e.Item.FindControl("lblTotalAmt");
            if (lblTotalAmt != null)
            {
                TotalAmount = this.calculateTotals();
                lblTotalAmt.Text = TotalAmount.ToString("0.00");

                Label lblPerMonth = (Label)e.Item.FindControl("lblPerMonth");
                lblPerMonth.Text = "Per Month";

            }
        }

    }

    protected decimal calculateTotals()
    {
        decimal total = 0;
        if (dataview != null)
        {
            foreach (DataRowView drv in dataview)
            {
                if (drv.Row["PayableAt"].ToString() == "MOC" && drv.Row["EarningDeduction"].ToString() == "Earning")
                    total += System.Decimal.Parse(drv.Row["Amount"].ToString());
                else if (drv.Row["PayableAt"].ToString() == "MOC" && drv.Row["EarningDeduction"].ToString() == "Deduction")
                    total -= System.Decimal.Parse(drv.Row["Amount"].ToString());

            }
        }
        return total;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }

        if (objUA.Approve == 0)
        {
            btnRollbackDigitalSign.Enabled = false;
            btnRollbackStaffSign.Enabled = false;
            btnVerify.Enabled = false;            
            btnUnVerify.Enabled  = false;
            ucUploadCrewContract1.Visible = false;
            
        }
        else
        {
            btnRollbackDigitalSign.Enabled = true ;
            btnRollbackStaffSign.Enabled  = true;
            btnVerify.Enabled  = true;
            btnUnVerify.Enabled = true;
            ucUploadCrewContract1.Visible = true ;
        }

        if (objUA.Admin == 0)
        {
            lnkDownloadSideLetter.Visible = true;
        }
        else
        {
            lnkDownloadSideLetter.Visible = false;
        }
      }

    protected void btnGenerateContract_Click(object sender, EventArgs e)
    {
        Generate_Crew_Agreement();
    }

    protected void btnRollbackDigitalSign_Click(object sender, EventArgs e)
    {
        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        DataTable dtAgreement = objCrewBLL.Get_CrewAgreementRecords(iCrewID, iVoyID, 2, GetSessionUserID());
        if (dtAgreement.Rows.Count > 0)
        {
            string Download_Date = dtAgreement.Rows[0]["Download_Date"].ToString();
            if (Download_Date != "")
            {
                string js = "alert('Manning office has downloaded the contract on : " + Download_Date + ". Please inform MANNING OFFICE to discard the old copy and take the new copy from JiBE after you upload the signed copy again');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);

            }
        }
        objCrewBLL.Undo_DigiSign_CrewAgreement(iCrewID, iVoyID, GetSessionUserID());
        Load_Agreements();
        Load_Agreement_Status();
    }
    
    protected void btnRollbackStaffSign_Click(object sender, EventArgs e)
    {
        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        objCrewBLL.Undo_StaffsSign_CrewAgreement(iCrewID, iVoyID, GetSessionUserID());
        Load_Agreements();
        Load_Agreement_Status();
    }

    protected void btnUnVerify_Click(object sender, EventArgs e)
    {
        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        objCrewBLL.Undo_Verify_CrewAgreement(iCrewID, iVoyID, GetSessionUserID());
        Load_Agreements();
        Load_Agreement_Status();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string sFileName = "";
        int ID = UDFLib.ConvertToInteger(hdnCurrentDocID.Value);
        System.Data.DataTable dt = objCrewBLL.Get_CrewAgreementRecords(ID, GetSessionUserID());
        foreach (DataRow item in dt.Rows)
        {
            if (!string.IsNullOrEmpty(item["Date_Of_Creation"].ToString()))
            item["Date_Of_Creation"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Date_Of_Creation"]));
            if (!string.IsNullOrEmpty(item["Date_Of_Modification"].ToString()))
            item["Date_Of_Modification"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Date_Of_Modification"]));
            if (!string.IsNullOrEmpty(item["Date_Of_Deletion"].ToString()))
            item["Date_Of_Deletion"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Date_Of_Deletion"]));
            if (!string.IsNullOrEmpty(item["Download_Date"].ToString()))
            item["Download_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Download_Date"]));
            if (!string.IsNullOrEmpty(item["VerifiedDate"].ToString()))
            item["Verified_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["VerifiedDate"]));
        }
        if (dt.Rows.Count > 0)
        {
            sFileName = dt.Rows[0]["DocFileName"].ToString();
            ResponseHelper.Redirect("DownloadFile.aspx?url=" + "../Uploads/CrewDocuments/" + sFileName, "_blank", "");
        }
    }

    protected void ucUploadCrewContract1_UploadCompleted()
    {
        Load_Agreements();
        Load_Agreement_Status();

        string js = "alert('Document Uploaded Successfully.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);

    }

    protected void rpt1_ItemCommand(Object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ViewDocument")
        {
            int ID = UDFLib.ConvertToInteger(e.CommandArgument);
            System.Data.DataTable dt = objCrewBLL.Get_CrewAgreementRecords(ID, GetSessionUserID());
            if (dt.Rows.Count > 0)
            {
                string filePath = "../Uploads/CrewDocuments/" + dt.Rows[0]["DocFilePath"].ToString();
                hdnCurrentDocID.Value = ID.ToString();
                if (dt.Rows[0]["Agreement_Stage"].ToString() == "1")
                {
                    ViewState["pagecount"] = dt.Rows[0]["PageCounts"].ToString();
                }
                else
                {
                    ViewState["pagecount"] = 0;
                }
                if (System.IO.File.Exists(Server.MapPath(filePath)) == true)
                {
                   
                    Random r = new Random();
                    string ver = r.Next().ToString();
                    frmContract.Attributes.Add("src", filePath + "?ver=" + ver);
                }
            }
        }
    }

    protected void Load_Agreements()
    {
        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        string filePath = "";
        Random r = new Random();
        string ver = r.Next().ToString();

        System.Data.DataTable dtAgreement = objCrewBLL.Get_CrewAgreementRecords(iCrewID, iVoyID, 0, GetSessionUserID());
        rpt1.DataSource = dtAgreement;
        rpt1.DataBind();

        btnGenerateContract.Visible = false;
        ucUploadCrewContract1.Visible = false;
        btnRollbackDigitalSign.Visible = false;
        btnVerify.Visible = false;
        btnUnVerify.Visible = false;
        btnRollbackStaffSign.Visible = false;
        ViewState["pagecount"]  = 0;

        DataRow[] dr = dtAgreement.Select("agreement_stage = 1");
        if (dr.Length > 0)
        {
            hdnCurrentDocID.Value = dr[0]["ID"].ToString();
            ViewState["pagecount"] = dr[0]["PageCounts"].ToString();
            // -- if signed by Office --
            DataRow[] dr2 = dtAgreement.Select("agreement_stage = 2");
            if (dr2.Length > 0)
            {
                hdnCurrentDocID.Value = dr2[0]["ID"].ToString();
                //ViewState["pagecount"] = dr2[0]["PageCount"].ToString();
                // -- if signed by staff --
                DataRow[] dr3 = dtAgreement.Select("agreement_stage = 3");
                if (dr3.Length > 0)
                {                    
                    hdnCurrentDocID.Value = dr3[0]["ID"].ToString();

                    filePath = "../Uploads/CrewDocuments/" + dr3[0]["DocFilePath"].ToString();
                    if (System.IO.File.Exists(Server.MapPath(filePath)) == true)
                        frmContract.Attributes.Add("src", filePath + "?ver=" + ver);
                    //ViewState["pagecount"] = dr3[0]["PageCount"].ToString();
                    // -- if verified by office --
                    if (UDFLib.ConvertToInteger(dr3[0]["ApprovedBy"].ToString()) > 0)
                    {
                        lblMessage.Text = "<img src='../images/ok-icon.png' align='middle' height='18px'> Crew Agreement is verified by Office";
                        pnlVerified.Visible = true;
                        lblVerifiedBy.Text = dr3[0]["VerifiedBy"].ToString();
                        lblVerifiedDate.Text = dr3[0]["VerifiedDate"].ToString();
                        btnUnVerify.Visible = true;

                    }
                    else
                    {
                        lblMessage.Text = "<img src='../images/information.png' align='middle' height='18px'> Document is signed by Staff. Pending for Office to verify.";
                        btnRollbackStaffSign.Visible = true;
                        btnVerify.Visible = true;
     
                    }
                }
                else
                {
                    //-- DIGITALLY SIGNED
                    //-- Pending for Manning Office to upload the signed copy
                    btnRollbackDigitalSign.Visible = true;

                    filePath = "../Uploads/CrewDocuments/" + dr2[0]["DocFilePath"].ToString();
                    hdnCurrentDocID.Value = dr2[0]["ID"].ToString();

                    if (System.IO.File.Exists(Server.MapPath(filePath)) == true)
                        frmContract.Attributes.Add("src", filePath + "?ver=" + ver);
                    lblMessage.Text = "<img src='../images/information.png' align='middle' height='18px'> Pending for Manning Office to upload the signed copy.";

                }
            }
            else
            {
                //-- Pending for office to digitally sign the document --
                filePath = "../Uploads/CrewDocuments/" + dr[0]["DocFilePath"].ToString();
                hdnCurrentDocID.Value = dr[0]["ID"].ToString();
                ViewState["pagecount"] = dr[0]["PageCounts"].ToString();
                ucUploadCrewContract1.Visible = true;

                btnGenerateContract.Visible = true;

                if (System.IO.File.Exists(Server.MapPath(filePath)) == true)
                    frmContract.Attributes.Add("src", filePath + "?ver=" + ver);

                lblMessage.Text = "<img src='../images/information.png' align='middle' height='18px'> Pending for office to digitally sign the document";
            }

        }
        else
        {
            Generate_Crew_Agreement();
            ucUploadCrewContract1.Visible = true;

            btnGenerateContract.Visible = true;
        }        
    }

    protected void Load_Agreement_Status()
    {
        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        System.Data.DataTable dt = objCrewBLL.Get_CrewAgreementStatus(iVoyID, GetSessionUserID());
        rptSteps.DataSource = dt;
        rptSteps.DataBind();

        DataRow[] dr = dt.Select("StepText like '%Contract Downloaded by Manning Office%'");
        if (dr.Length > 0)
        {
            string msg = dr[0][1].ToString() + ". Do you still want to rollback the digital signature ?";


            btnRollbackDigitalSign.Attributes.Add("onclick", "return ContractDownloaded(' " + msg + " ')");
        }

    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        objCrewBLL.Verify_CrewAgreement(iCrewID, iVoyID, GetSessionUserID());

        Load_Agreements();
        Load_Agreement_Status();
    }

    protected void Generate_Crew_Agreement()
    {
        int iCrewID = int.Parse(getQueryString("CrewID"));
        int VoyID = int.Parse(getQueryString("VoyID"));
        int Contract_template_ID = 0;

        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();


        System.Data.DataTable dt = objCrewBLL.Get_CrewAgreementData(iCrewID, VoyID);
        System.Data.DataTable dtPersonal = objCrewBLL.Get_CrewPersonalDetailsByID1(iCrewID);
        System.Data.DataTable dtNOK = objCrewBLL.Get_Crew_DependentsByCrewID(iCrewID, 1);
        string Photo = "";
        int NOK_Mandatory = 0,Photo_Mandatory = 0;

        DataTable dtMandatorySettings = objCrewAdmin.GetMandatorySettings();
        if (dtMandatorySettings != null && dtMandatorySettings.Rows.Count > 0)
        {
            if (dtMandatorySettings.Rows[0]["Value"].ToString() == "1")
                NOK_Mandatory = 1;
            if (dtMandatorySettings.Rows[1]["Value"].ToString() == "1")
                Photo_Mandatory = 1;
        }

        if (dtPersonal.Rows.Count > 0)
        {
            Photo = dtPersonal.Rows[0]["PhotoURL"].ToString();
        }

        if (Photo_Mandatory == 1 && Photo == "")
        {
            Response.Write("<h1>Contract can not be printed at this moment as Photo is not yet uploaded for the Crew</h1>");
            Response.End();
        }
        else if (NOK_Mandatory == 1 && dtNOK.Rows.Count == 0)
        {
            Response.Write("<h1>Contract can not be printed at this moment as Next Of Kin details is not yet updated</h1>");
            Response.End();
        }
        else if (dt.Rows.Count == 0)
        {
            Response.Write("<h1>Contract can not be printed at this moment. Please enter the voyage details for the crew.</h1>");
            Response.End();
        }
        else
        {
            try
            {
                DataSet ds = BLL_PortageBill.Get_CrewWagesByVoyage_ForAgreement(iCrewID, VoyID);
                ds.Relations.Add(new DataRelation("Parent", ds.Tables[0].Columns["Effective_Date"], ds.Tables[1].Columns["Effective_Date"]));
                ds.Tables[1].TableName = "Child";

                ds.Tables[1].DefaultView.RowFilter = "amount > 0";
                dataview = ds.Tables[1].DefaultView;

                DataTable dtCheckSalaryComponent = ds.Tables[1].DefaultView.ToTable();
                dtCheckSalaryComponent.DefaultView.RowFilter = " ( Name like '%//%' OR Name like '%&%') ";

                if (dtCheckSalaryComponent.DefaultView.ToTable().Rows.Count > 0)
                {
                    string js = "alert('Contract cannot be generated as Salary Component contains special charaters. Update Salary Components to generate contract');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
                }
                else
                {

                    rpt2.DataSource = dataview;
                    rpt2.DataBind();

                    System.Data.DataTable dtVoy = objCrewBLL.Get_CrewVoyages(iCrewID, VoyID);
                    int Vessel_Flag = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Vessel_Flag"].ToString());
                    int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
                    int VesselID = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Vessel_ID"].ToString());
                    System.Data.DataSet dtCompany = objCrewBLL.Get_Crew_CompanyDetails(UDFLib.ConvertIntegerToNull(VesselID));
                    System.Data.DataTable dtTemplate;

                    int ContractId = UDFLib.ConvertToInteger(dtVoy.Rows[0]["ContractId"].ToString());
                    dtTemplate = objBLLCrewContract.Get_ContractTemplate(ContractId);

                    string TemplateText = "";
                    string FooterText1 = "";
                    string FooterText2 = "";
                    FooterText = ".";

                    if (dtTemplate.Rows.Count > 0)
                    {
                        Contract_template_ID = UDFLib.ConvertToInteger(dtTemplate.Rows[0]["ID"].ToString());
                        FooterText = dtTemplate.Rows[0]["FooterText"].ToString();

                        TemplateText = "<template><br></br><br></br>" + dtTemplate.Rows[0]["template_text"].ToString() + "</template>";

                        TemplateText = TemplateText.Replace("&nbsp;", "&#32;");
                        TemplateText = TemplateText.Replace(" &amp; ", " &#38; ");
                        TemplateText = TemplateText.Replace(" & ", " and ");
                        TemplateText = TemplateText.Replace("&rsquo;", "&#39;");
                        TemplateText = TemplateText.Replace("&ldquo;", "&#34;");
                        TemplateText = TemplateText.Replace("&rdquo;", "&#34;");
                        TemplateText = TemplateText.Replace("&hellip;", "&#46;");
                        TemplateText = TemplateText.Replace("&ndash;", "&#150;");
                        //TemplateText = TemplateText.Replace("<p></p>", "");

                        XmlDocument _doc = new XmlDocument();
                        _doc.LoadXml(TemplateText);
                        XmlNodeList wages = _doc.GetElementsByTagName("wages");

                        if (wages != null && wages.Count > 0)
                        {
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            rpt2.RenderControl(hw);

                            string tblWages = "<table style='width:100%'><tr style='background-color: #D8D8D8; color: Black; font-weight: bold;'><td>Earning/Deduction</td><td>Name</td><td>Currency</td><td>Amount</td><td>Salary Type</td></tr>";
                            tblWages += sw.ToString().Replace("\n\r", "").Replace("\r\n", "").Replace(" & ", " &#38; ");
                            tblWages += "</table>";

                            wages[0].InnerXml = string.Format(tblWages);
                        }
                        TemplateText = _doc.InnerXml;

                        string ST_NAME = "<input name=\"STAFF_NAME\" type=\"text\" value=\"NAME\" />";
                        string ST_ADD = "<input name=\"ADDRESS\" type=\"text\" value=\"ADDRESS\" />";
                        string ST_RANK = "<input name=\"STAFF_RANK\"  type=\"text\" value=\"RANK\" />";
                        string ST_RANK1 = "<input name=\"STAFF_RANK\" type=\"text\" value=\"RANK\" />";

                        string VSL = "<input name=\"VESSEL\"  type=\"text\" value=\"VESSEL\" />";
                        string VSL1 = "<input name=\"VESSEL\" type=\"text\" value=\"VESSEL\" />";
                        string DURATION = "<input name=\"DURATION\"  type=\"text\" value=\"0 Month(s) 0 Days \" />";
                        string DURATION1 = "<input name=\"DURATION\" type=\"text\" value=\"0 Month(s) 0 Days \" />";
                        string ST_DAY = "<input name=\"START_DAY\"  type=\"text\" value=\"0\" />";
                        string ST_MONTH = "<input name=\"START_MONTH\"  type=\"text\" value=\"MMM yyyy\" />";
                        string ST_DAY1 = "<input name=\"START_DAY\" type=\"text\" value=\"0\" />";
                        string ST_MONTH1 = "<input name=\"START_MONTH\" type=\"text\" value=\"MMM yyyy\" />";
                        string STAFF_CODE = "<input name=\"STAFF_CODE\" type=\"text\" value=\"STAFF_CODE\" />";
                        string PASSPORT_NO = "<input name=\"PASSPORT_NO\" type=\"text\" value=\"PASSPORT_NO\" />";
                        string CONTRACT_DATE = "<input name=\"CONTRACT_DATE\" type=\"text\" />";
                        string DOB = "<input name=\"DOB\" type=\"text\" />";

                        string BIRTH_PLACE = "<input name=\"BIRTH_PLACE\" type=\"text\" value=\"BIRTH_PLACE\" />";

                        string NATIONALITY = "<input name=\"NATIONALITY\" type=\"text\" value=\"NATIONALITY\" />";
                        string SEAMAN_NO = "<input name=\"SEAMAN_NO\" type=\"text\" value=\"SEAMAN_NO\" />";
                        //string MGR_Name = "<input name=\"Manager_Name\" type=\"text\" value=\"Manager_Name\" />";
                        string Comp_Name = "<input name=\"COMPANY_NAME\" type=\"text\" value=\"COMPANY_NAME\" />";
                        string Comp_Address = "<input name=\"COMPANY_ADDRESS\" type=\"text\" value=\"COMPANY_ADDRESS\" />";

                        TemplateText = TemplateText.Replace(ST_NAME, dt.Rows[0]["CrewName"].ToString());
                        TemplateText = TemplateText.Replace(ST_ADD, dt.Rows[0]["Address"].ToString());
                        TemplateText = TemplateText.Replace(ST_RANK, dt.Rows[0]["Rank_Name"].ToString());
                        TemplateText = TemplateText.Replace(ST_RANK1, dt.Rows[0]["Rank_Name"].ToString());
                        TemplateText = TemplateText.Replace(VSL, dt.Rows[0]["Vessel_Name"].ToString());
                        TemplateText = TemplateText.Replace(VSL1, dt.Rows[0]["Vessel_Name"].ToString());
                        TemplateText = TemplateText.Replace(DURATION, dt.Rows[0]["MonthDays"].ToString());
                        TemplateText = TemplateText.Replace(DURATION1, dt.Rows[0]["MonthDays"].ToString());
                        TemplateText = TemplateText.Replace(STAFF_CODE, dt.Rows[0]["STAFF_CODE"].ToString());
                        TemplateText = TemplateText.Replace(PASSPORT_NO, dt.Rows[0]["Passport_Number"].ToString());
                        TemplateText = TemplateText.Replace(CONTRACT_DATE, dt.Rows[0]["Joining_Date"].ToString() == "" ? "________________" : dt.Rows[0]["Joining_Date"].ToString());
                        TemplateText = TemplateText.Replace(DOB, dt.Rows[0]["DOB"].ToString());
                        TemplateText = TemplateText.Replace(BIRTH_PLACE, dt.Rows[0]["BIRTH_PLACE"].ToString());

                        TemplateText = TemplateText.Replace(NATIONALITY, dt.Rows[0]["NATIONALITY"].ToString());
                        TemplateText = TemplateText.Replace(SEAMAN_NO, dt.Rows[0]["Seaman_Book_Number"].ToString());

                        TemplateText = TemplateText.Replace(Comp_Name, dtCompany.Tables[0].Rows[0]["Company_Name"].ToString());
                        TemplateText = TemplateText.Replace(Comp_Address, dtCompany.Tables[0].Rows[0]["Address"].ToString());

                        //TemplateText = TemplateText.Replace(MGR_Name, dtCompany.Tables[1].Rows[0]["Manager_Name"].ToString());

                        string JoinDate = "";
                        if (dt.Rows[0]["Joining_Date"].ToString() != "")
                        {
                            JoinDate = dt.Rows[0]["Joining_Date"].ToString();
                            DateTime DtJoining = DateTime.Parse(JoinDate);

                            TemplateText = TemplateText.Replace(ST_DAY, DtJoining.ToString("dd"));
                            TemplateText = TemplateText.Replace(ST_MONTH, DtJoining.ToString("MMMM") + " " + DtJoining.ToString("yyyy"));
                            TemplateText = TemplateText.Replace(ST_DAY1, DtJoining.ToString("dd"));
                            TemplateText = TemplateText.Replace(ST_MONTH1, DtJoining.ToString("MMMM") + " " + DtJoining.ToString("yyyy"));
                        }
                        else
                        {
                            TemplateText = TemplateText.Replace(ST_DAY, "_______");
                            TemplateText = TemplateText.Replace(ST_MONTH, "________________");

                        }


                        if (TemplateText != "")
                        {
                            string sFileName = iCrewID.ToString() + "_" + VoyID.ToString() + "_1" + ".pdf";
                            string filePath = Server.MapPath("~/Uploads/CrewDocuments/") + sFileName;
                            //string filePath = Server.MapPath("~/Uploads/CrewDocuments/") + Flag_Attach;
                            //FileUpload1.SaveAs(filePath);
                            System.Data.DataTable dtAgreement = objCrewBLL.Get_CrewAgreementRecords(iCrewID, VoyID, 0, GetSessionUserID());
                            DataRow[] dr = dtAgreement.Select("agreement_stage = 1");
                            if (dr.Length > 0)
                            {
                                ViewState["pagecount"] = dr[0]["PageCounts"].ToString();
                            }
                            else
                            {
                                ViewState["pagecount"] = 0;
                            }
                            //-- write pdf file --
                            /* Added by pranali_070715 License Key EO.Pdf*/
                            EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
                              "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
                              "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
                              "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
                              "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
                              "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");


                            EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
                            EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = "<center><img alt='' src='" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/images/Company_logo.jpg' height='60px' /></center>";
                            EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='color:white;z-order:100'><table style='width:100%'><tr><td style='width:33%;text-align:left;height:40px;vertical-align:bottom;'>" + FooterText1 + "</td><td style='width:34%;text-align:center;height:40px;vertical-align:bottom;'>Page {page_number} of {total_pages}</td><td style='width:33%;text-align:right;height:40px;vertical-align:bottom;'>" + FooterText2 + "</td></tr></table></div>";
                            EO.Pdf.HtmlToPdf.ConvertHtml(TemplateText, filePath);


                            Random r1 = new Random();
                            string ver1 = r1.Next().ToString();

                            int Pagecount = getNumberOfPdfPages(filePath);
                            int DocID = objCrewBLL.Insert_CrewAgreementRecord(iCrewID, VoyID, 1, Contract_template_ID, "Crew Agreement", sFileName, sFileName, GetSessionUserID(), Pagecount);
                            hdnCurrentDocID.Value = DocID.ToString();

                            if (DocID > 0)
                            {
                                //--Delete the existing file
                                try
                                {
                                    System.IO.FileInfo fi = new FileInfo(filePath);
                                    fi.Delete();
                                }
                                catch { }

                                //-- write pdf file --
                                /* Added by pranali_070715 License Key EO.Pdf*/
                                EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
                                  "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
                                  "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
                                  "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
                                  "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
                                  "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
                                EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
                                EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = "<center><img alt='' src='" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/images/Company_logo.jpg'  height='60px' /></center>";
                                EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='color:white;z-order:100'><table style='width:100%'><tr><td style='width:33%;text-align:left;height:40px;vertical-align:bottom;'>" + FooterText1 + "</td><td style='width:34%;text-align:center;height:40px;vertical-align:bottom;'>Page {page_number} of {total_pages}</td><td style='width:33%;text-align:right;height:40px;vertical-align:bottom;'>" + FooterText2 + "</td></tr></table></div>";
                                EO.Pdf.HtmlToPdf.ConvertHtml(TemplateText, filePath);


                                Random r = new Random();
                                string ver = r.Next().ToString();

                                lblMessage.Text = "Crew Agreement Generated. The agreement is yet to be signed by the office.";

                            }

                            Load_Agreements();
                            Load_Agreement_Status();
                        }
                        else
                        {
                            lblMessage.Text = "Contract template do not have wages section";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Contract template not found for the vessel flag";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

            }


        }
    }
    
    private void On_AfterRenderPage(object sender, EO.Pdf.PdfPageEventArgs e)
    {       
        EO.Pdf.PdfPage page = e.Page;
        EO.Pdf.Acm.AcmRender render = new EO.Pdf.Acm.AcmRender(page, 0, new EO.Pdf.Acm.AcmPageLayout(new EO.Pdf.Acm.AcmPadding(0, 0, 0, 0)));
        render.SetDefPageSize(new SizeF(EO.Pdf.PdfPageSizes.A4.Width, EO.Pdf.PdfPageSizes.A4.Height));
        EO.Pdf.Acm.AcmBlock footer = new EO.Pdf.Acm.AcmBlock(new EO.Pdf.Acm.AcmText(FooterText));
        //footer.Style.Border.Top = new EO.Pdf.Acm.AcmLineInfo(EO.Pdf.Acm.AcmLineStyle.Solid, Color.LightGray, 0.01f);
        footer.Style.Top = 10.4f;
        //footer.Style.FontName = "Arial";        
        footer.Style.FontSize = 10f;
        footer.Style.HorizontalAlign = EO.Pdf.Acm.AcmHorizontalAlign.Right;
        //footer.Style.BackgroundColor = Color.Blue;
        //footer.Style.ForegroundColor = Color.White;
        render.Render(footer);

        
    }

    protected void btnUploadAttachments_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploader1.FileName.Length > 0)
            {
                int CrewID = int.Parse(getQueryString("CrewID"));
                int VoyID = int.Parse(getQueryString("VoyID"));

                string sFileName = CrewID.ToString() + "_" + VoyID.ToString() + "_2" + ".pdf";

                string Upload_Path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/CrewDocuments/");
                FileUploader1.SaveAs(Upload_Path + "\\" + sFileName);

                int DocID = objCrewBLL.Insert_CrewAgreementRecord(CrewID, VoyID, 2, 0, "Crew Agreement - Signed by Office", sFileName, sFileName, GetSessionUserID());

                Load_Agreements();
                Load_Agreement_Status();
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    public int getNumberOfPdfPages(string fileName)
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

    protected void lnkDownloadSideLetter_Click(object sender, EventArgs e)
    {
        string ST_NAME = "<input name=\"STAFF_NAME\" type=\"text\" value=\"NAME\" />";
        string ST_ADD = "<input name=\"ADDRESS\" size=\"50\" type=\"text\" value=\"ADDRESS\" />";
        string ST_RANK = "<input name=\"STAFF_RANK\" size=\"6\" type=\"text\" value=\"RANK\" />";
        string VSL = "<input name=\"VESSEL\" size=\"6\" type=\"text\" value=\"VESSEL\" />";
        string DURATION = "<input name=\"DURATION\" size=\"15\" type=\"text\" value=\"0 Month(s) 0 Days \" />";
        string ST_DAY = "<input name=\"START_DAY\" size=\"2\" type=\"text\" value=\"0\" />";
        string ST_MONTH = "<input name=\"START_MONTH\" size=\"8\" type=\"text\" value=\"MMM yyyy\" />";
        string STAFF_CODE = "<input name=\"STAFF_CODE\" type=\"text\" value=\"STAFF_CODE\" />";
        string PASSPORT_NO = "<input name=\"PASSPORT_NO\" type=\"text\" value=\"PASSPORT_NO\" />";
        string CONTRACT_DATE = "<input name=\"CONTRACT_DATE\" type=\"text\" />";
        string TOTAL_SALARY = "<input name=\"TOTAL_SALARY\" type=\"text\" value=\"TOTAL_SALARY\" />";
        string AMOUNT_MONTHLY = "<input name=\"AMOUNT_MONTHLY\" type=\"text\" value=\"AMOUNT_MONTHLY\" />";
        string AMOUNT_COC = "<input name=\"AMOUNT_COC\" type=\"text\" value=\"AMOUNT_COC\" />";

        //----------------------------------------------------------------------


        int iCrewID = UDFLib.ConvertToInteger(getQueryString("CrewID"));
        int iVoyID = UDFLib.ConvertToInteger(getQueryString("VoyID"));

        string sFileName = "SideLetter_" +  iCrewID.ToString() + "_" + iVoyID.ToString()  + ".pdf";
        string filePath = Server.MapPath("~/Uploads/CrewDocuments/") + sFileName;

        DataTable dt =  objCrewBLL.Get_SideLetter_ForVoyage(iVoyID, iCrewID, GetSessionUserID());

        if (dt.Rows.Count > 0)
        {
            string TemplateText = dt.Rows[0]["Template_Text"].ToString();

            TemplateText = TemplateText.Replace(ST_NAME, dt.Rows[0]["Staff_FullName"].ToString());
            TemplateText = TemplateText.Replace(ST_RANK, dt.Rows[0]["Rank_Short_Name"].ToString());
            TemplateText = TemplateText.Replace(DURATION, dt.Rows[0]["MonthDays"].ToString());
            TemplateText = TemplateText.Replace(STAFF_CODE, dt.Rows[0]["STAFF_CODE"].ToString());
            TemplateText = TemplateText.Replace(AMOUNT_MONTHLY, dt.Rows[0]["AMOUNT_MONTHLY"].ToString());
            TemplateText = TemplateText.Replace(AMOUNT_COC, dt.Rows[0]["AMOUNT"].ToString());
            TemplateText = TemplateText.Replace(TOTAL_SALARY, dt.Rows[0]["TOTAL_SALARY"].ToString());

            //-- write pdf file --
            /* Added by pranali_070715 License Key EO.Pdf*/
            EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
              "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
              "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
              "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
              "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
              "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
            EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage_SideLetter);
            EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = "<center><img alt='' src='" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/images/Company_logo.jpg' height='80px' /></center>";
            EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='color:white;z-order:100'><table style='width:100%'><tr><td style='width:33%;text-align:left;height:40px;vertical-align:bottom;'></td><td style='width:34%;text-align:center;height:40px;vertical-align:bottom;'>Page {page_number} of {total_pages}</td><td style='width:33%;text-align:right;height:40px;vertical-align:bottom;'></td></tr></table></div>";
            EO.Pdf.HtmlToPdf.ConvertHtml(TemplateText, filePath);


            if (System.IO.File.Exists(filePath) == true)
            {
                Random r = new Random();
                string ver = r.Next().ToString();
                frmContract.Attributes.Add("src", "../Uploads/CrewDocuments/" + sFileName + "?ver=" + ver);
            }

            ResponseHelper.Redirect("DownloadFile.aspx?url=" + "../Uploads/CrewDocuments/" + sFileName, "_blank", "");
        }        

    }

    private void On_AfterRenderPage_SideLetter(object sender, EO.Pdf.PdfPageEventArgs e)
    {
        EO.Pdf.PdfPage page = e.Page;
        EO.Pdf.Acm.AcmRender render = new EO.Pdf.Acm.AcmRender(page, 0, new EO.Pdf.Acm.AcmPageLayout(new EO.Pdf.Acm.AcmPadding(0, 0, 0, 0)));
        render.SetDefPageSize(new SizeF(EO.Pdf.PdfPageSizes.A4.Width, EO.Pdf.PdfPageSizes.A4.Height));
        EO.Pdf.Acm.AcmBlock footer = new EO.Pdf.Acm.AcmBlock(new EO.Pdf.Acm.AcmText(FooterText));
        //footer.Style.Border.Top = new EO.Pdf.Acm.AcmLineInfo(EO.Pdf.Acm.AcmLineStyle.Solid, Color.LightGray, 0.01f);
        footer.Style.Top = 10.4f;
        footer.Style.Height = 0.2f;        
        footer.Style.FontSize = 10f;
        footer.Style.HorizontalAlign = EO.Pdf.Acm.AcmHorizontalAlign.Right;
        //footer.Style.BackgroundColor = Color.Blue;
        //footer.Style.ForegroundColor = Color.White;
        render.Render(footer);

       
    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\CrewDocuments");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int CrewID = UDFLib.ConvertToInteger(Session["hdnCrewID"].ToString());
            int VoyID = UDFLib.ConvertToInteger(Session["hdnVoyID"].ToString());

            string FullFilename = Path.Combine(sPath, Flag_Attach);

            int FileID = objCrewBLL.Insert_CrewAgreementRecord(CrewID, VoyID, 2, 0, "Crew Agreement - Signed by Office", Flag_Attach, Flag_Attach, GetSessionUserID());// objBLL.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, Office_ID, UDFLib.Remove_Special_Characters(file.FileName), Flag_Attach, file.FileSize, UDFLib.ConvertToInteger(Session["USERID"]));

            

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {

        }

    }
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        Load_Agreements();
        Load_Agreement_Status();        
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            //Byte[] fileBytes = FileUpload1.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\CrewDocuments");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(FileUpload1.FileName);

            int CrewID = UDFLib.ConvertToInteger(Session["hdnCrewID"].ToString());
            int VoyID = UDFLib.ConvertToInteger(Session["hdnVoyID"].ToString());

            string FullFilename = Path.Combine(sPath, Flag_Attach);
            string filePath = Server.MapPath("~/Uploads/CrewDocuments/") + Flag_Attach;
            FileUpload1.SaveAs(filePath);
            int PageCount = getNumberOfPdfPages(filePath);

            string FileExt = Path.GetExtension(FullFilename).ToLower();
            if (FileExt == ".pdf")
            {
                if (PageCount != 0 && PageCount >= Convert.ToInt32(ViewState["pagecount"]))
                {
                    int FileID = objCrewBLL.Insert_CrewAgreementRecord(CrewID, VoyID, 2, 0, "Crew Agreement - Signed by Office", Flag_Attach, Flag_Attach, GetSessionUserID(), PageCount);// objBLL.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, Office_ID, UDFLib.Remove_Special_Characters(file.FileName), Flag_Attach, file.FileSize, UDFLib.ConvertToInteger(Session["USERID"]));
                    Load_Agreements();
                    Load_Agreement_Status();
                    string js = "closeDialog();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                }
                else
                {
                    File.Delete(filePath);
                    string js = "alert('The PDF document you are trying to upload is invalid.Page count is not matching downloaded document.');showDialog();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertMessage", js, true);
                    //lblMessage.Text = "The PDF document you are trying to upload is invalid. Please scan all the pages of the contract to 1 PDF Document and then upload.";
                }
            }
            else
            {
                string js2 = "Only PDF documents are allowed to be uploaded as Crew Agreement.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js2 + "');", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}