using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.DMS;
using System.Text.RegularExpressions;


public partial class Crew_AddEditCrew : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_DMS_Admin objDMSAdminBLL = new BLL_DMS_Admin();
    BLL_DMS_Document objDMSBLL = new BLL_DMS_Document();
    BLL_Infra_Country objInfraBLL = new BLL_Infra_Country();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

    UserAccess objUA = new UserAccess();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
                        
            int CrewID = GetCrewID();
            HiddenField_CrewID.Value = CrewID.ToString();
            HiddenField_UserID.Value = GetSessionUserID().ToString();
            HiddenField_PhotoUploadPath.Value = Server.MapPath("../Uploads/CrewImages");
            HiddenField_AccType.Value = "";
            Load_CountryList();
            Load_ManningAgentList();
            Load_RankList();
            getEnglishProficiency();
            if (CrewID > 0)
            {                
                Load_CrewPersonalDetails(CrewID);
                Load_PassportAndSeamanDetails(CrewID);
                Load_CrewHeightWaiseWeight();
                Load_Next_Of_Kin(CrewID);

                btnSaveDetails.Enabled = false;
                btnCrewDetails.Enabled = true;

                pnlView_PersonalDetails.Visible = true;
                pnlEdit_PersonalDetails.Visible = false;

                pnlView_Passport.Visible = true;
                pnlEdit_Passport.Visible = false;
                lnkAddDependents.Visible = true;
                if (hdnNOKID.Value != "" && hdnNOKID.Value != "0")
                {
                    pnlView_NextOfKin.Visible = true;
                    pnlEdit_NextOfKin.Visible = false;
                }
                else
                {
                    pnlView_NextOfKin.Visible = false;
                    pnlEdit_NextOfKin.Visible = true;
                }
                pnlDependents.Visible = true;
                dvPrejoining.Visible = true;
            }
            else
            {
                btnCrewDetails.Enabled = false;
               
                pnlView_PersonalDetails.Visible = false;
                pnlEdit_PersonalDetails.Visible = true;

                pnlView_Passport.Visible = false;
                pnlEdit_Passport.Visible = true;

                pnlView_NextOfKin.Visible = false;
                pnlEdit_NextOfKin.Visible = true;

                lnkAddDependents.Visible = false;

                pnlView_NextOfKin.Visible = false;
                pnlEdit_NextOfKin.Visible = false;

                btnSavePersonalDetails.Visible = false;
                btnCancelPersonalDetails.Visible = false;
                btnSavePassportDetails.Visible = false;
                btnCancelPassportDetails.Visible = false;

                pnlDependents.Visible = false;
                dvPrejoining.Visible = false;
           }

            if (Print() == 1)
            {
                string js = "window.print();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", js, true);

            }
        }
        if (Session["NewCrew"] == "NewCrew")
        {
            showAlert("This profile will be send to Manning Office for first level of interview");
            Session["NewCrew"] = "";
        } 
        UserAccessValidation();

        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            pnlAddDependents.Visible = false;
            btnSaveDetails.Enabled = false;
            lnkAddDependents.Visible = false;
        }

        if (objUA.Edit == 0)
        {
            lnkEditPersonalDetails.Visible = false;
            lnkEditPassport.Visible = false;
            lnkEditNOK.Visible = false;
            GridView_Dependents.Columns[GridView_Dependents.Columns.Count - 2].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Dependents.Columns[GridView_Dependents.Columns.Count - 1].Visible = false;
        }
        //Quick Approval button should not be visible to Manning Agent
        if (objUA.Approve == 0 || (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT")))
        {
            btnQuickApproval.Visible = false;
        }
        else
        {
            int CrewID = GetCrewID();
            if (CrewID > 0)
                btnQuickApproval.Visible = true;
            else
                btnQuickApproval.Visible = false;
        }
    }
    protected int Print()
    {
        try
        {
            if (Request.QueryString["P"] != null && Request.QueryString["P"].ToString() != "")
                return int.Parse(Request.QueryString["P"].ToString());
            else
                return 0;
        }
        catch
        {
            return 0;
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
    public int GetCrewID()
    {
        try
        {
            if (HiddenField_CrewID.Value != "0" && HiddenField_CrewID.Value != "")
            {
                return int.Parse(HiddenField_CrewID.Value);
            }
            else if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_CountryList()
    {
        BLL_Infra_Country objCountry = new BLL_Infra_Country();

        ddlPD_Nationality.DataSource = objCountry.Get_CountryList();
        ddlPD_Nationality.DataTextField = "COUNTRY";
        ddlPD_Nationality.DataValueField = "ID";
        ddlPD_Nationality.DataBind();
        ddlPD_Nationality.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    public void Load_ManningAgentList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlManningOffice.DataSource = objBLLCrew.Get_ManningAgentList(UserCompanyID);
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();
        ddlManningOffice.Items.Insert(0, new ListItem("-SELECT-", "0"));

        ddlManningOffice.Text = UserCompanyID.ToString();

    }

    public void Load_RankList()
    {
        ddlRankAppliedFor.DataSource = objCrewAdmin.Get_RankList();
        ddlRankAppliedFor.DataBind();
    }

    protected void Load_CrewPersonalDetails(int ID)
    {
        string js_notify = "";
        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
        if (dt.Rows.Count > 0)
        {
            HiddenField_AccType.Value = dt.Rows[0]["Allotment_AccType"].ToString();
            HiddenField_NationalityId.Value = dt.Rows[0]["Country_id"].ToString();
            lblSurname.Text = dt.Rows[0]["Staff_Surname"].ToString();
            lblGivenName.Text = dt.Rows[0]["Staff_Name"].ToString();
            lblAlias.Text = dt.Rows[0]["Alias"].ToString();

            if (dt.Rows[0]["Staff_Birth_Date"].ToString() != "")
                lblDateOfBirth.Text = DateTime.Parse(dt.Rows[0]["Staff_Birth_Date"].ToString()).ToString("dd/MM/yyyy");

            lblPlaceOfBirth.Text = dt.Rows[0]["Staff_Born_Place"].ToString();
            lblNationality.Text = dt.Rows[0]["Country_Name"].ToString();
            lblMaritalStatus.Text = dt.Rows[0]["MaritalStatus"].ToString();
            lblTelephone.Text = dt.Rows[0]["Telephone"].ToString();
            lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
            lblAddress.Text = dt.Rows[0]["Address"].ToString();
            lblFax.Text = dt.Rows[0]["Fax"].ToString();
            lblEMail.Text = dt.Rows[0]["EMail"].ToString();
            /*following link for Email added for jit 8704*/
            lblEMail.NavigateUrl = "mailto:" + dt.Rows[0]["EMail"].ToString();

            lblIntlAirport.Text = dt.Rows[0]["NearestAirport"].ToString();
           
            if (dt.Rows[0]["PhotoURL"].ToString() != "")
            {
                imgCrewPic.ImageUrl = "~/Uploads/CrewImages/" + dt.Rows[0]["PhotoURL"].ToString();
                imgCrewPic.Visible = true;
                imgNoPic.Visible = false;
            }
            else
            {
                imgCrewPic.Visible = false;
                imgNoPic.Visible = true;
            }

            lblUSVisaNo.Text = dt.Rows[0]["Us_Visa_Number"].ToString();
            lblUSVisaFlag.Text = dt.Rows[0]["Us_Visa_Flag"].ToString() == "1" ? "YES" : "NO";
            if (dt.Rows[0]["Us_Visa_Expiry"].ToString() != "")
                lblUSVisaExpiry.Text = DateTime.Parse(dt.Rows[0]["Us_Visa_Expiry"].ToString()).ToString("dd/MM/yyyy");


            if (dt.Rows[0]["CurrentRankID"].ToString() != "")
                ddlRankAppliedFor.SelectedValue = dt.Rows[0]["CurrentRankID"].ToString();

            if (dt.Rows[0]["Available_From_Date"].ToString() != "")
                txtDateAvailable.Text = DateTime.Parse(dt.Rows[0]["Available_From_Date"].ToString()).ToString("dd/MM/yyyy");

            if (dt.Rows[0]["ManningOfficeID"].ToString() != "")
            {
                ddlManningOffice.Text = dt.Rows[0]["ManningOfficeID"].ToString();
            }

            txtPD_Surname.Text = dt.Rows[0]["Staff_Surname"].ToString();
            txtPD_Givenname.Text = dt.Rows[0]["Staff_Name"].ToString();
            txtPD_Alias.Text = dt.Rows[0]["Alias"].ToString();

            txtPD_DOB.Text = DateTime.Parse(dt.Rows[0]["Staff_Birth_Date"].ToString()).ToString("dd/MM/yyyy");
            txtPD_PlaceOfBirth.Text = dt.Rows[0]["Staff_Born_Place"].ToString();

            if (dt.Rows[0]["Staff_Nationality"].ToString() != "")
                ddlPD_Nationality.Text = dt.Rows[0]["Staff_Nationality"].ToString();

            if (dt.Rows[0]["MaritalStatus"].ToString() != "")
                ddlPD_MaritalStatus.Text = dt.Rows[0]["MaritalStatus"].ToString();

            txtPD_Phone.Text = dt.Rows[0]["Telephone"].ToString();
            txtPD_Mobile.Text = dt.Rows[0]["Mobile"].ToString();
            txtPD_Address.Text = dt.Rows[0]["Address"].ToString();
            txtPD_Fax.Text = dt.Rows[0]["Fax"].ToString();
            txtPD_Email.Text = dt.Rows[0]["EMail"].ToString();
            txtPD_Airport.Text = dt.Rows[0]["NearestAirport"].ToString();
            txtPD_Airport.SelectedValue = dt.Rows[0]["NearestAirportID"].ToString();
            txtPD_USVisaNo.Text = dt.Rows[0]["Us_Visa_Number"].ToString();
            rdoUSVisaFlag.SelectedValue = dt.Rows[0]["Us_Visa_Flag"].ToString();
            txtPD_USVisaExpiry.Text = dt.Rows[0]["Us_Visa_Expiry"].ToString();

            this.Title = "Crew Details: " + dt.Rows[0]["Staff_Surname"].ToString() + " " + dt.Rows[0]["Staff_Name"].ToString();

        }
    }

    protected void Load_CrewHeightWaiseWeight()
    {
        int CrewID = GetCrewID();
        DataTable dt = objBLLCrew.Get_CrewHeightWaistWeight(CrewID);

        if (dt.Rows.Count > 0)
        {
            lblHeight.Text = dt.Rows[0]["height"].ToString();
            lblWaist.Text = dt.Rows[0]["waist"].ToString();
            lblWeight.Text = dt.Rows[0]["weight"].ToString();

            txtHeight.Text = dt.Rows[0]["height"].ToString();
            txtWaist.Text = dt.Rows[0]["waist"].ToString();
            txtWeight.Text = dt.Rows[0]["weight"].ToString();

        }
    }

    protected void lnkAddPreJoiningExp_Click(object sender, EventArgs e)
    {
       // pnlAdd_PreJoining.Visible = true;
        //pnlView_PreJoining.Visible = false;
    }

    protected void lnkAddDependents_Click(object sender, EventArgs e)
    {
        pnlAddDependents.Visible = true;
    }

    protected void Load_PassportAndSeamanDetails(int CrewID)
    {
        string js_notify = "";
        DataTable dt = objBLLCrew.Get_CrewPassportAndSeamanDetails(CrewID);

        lblPassport_No.Text = dt.Rows[0]["Passport_Number"].ToString();
        lblPassport_Place.Text = dt.Rows[0]["Passport_PlaceOf_Issue"].ToString();
        txtPassport_No.Text = dt.Rows[0]["Passport_Number"].ToString();
        txtPassport_Place.Text = dt.Rows[0]["Passport_PlaceOf_Issue"].ToString();

        if (dt.Rows[0]["Passport_Issue_Date"].ToString() != "")
        {
            lblPassport_IssueDt.Text = DateTime.Parse(dt.Rows[0]["Passport_Issue_Date"].ToString()).ToString("dd/MM/yyyy");
            txtPassport_IssueDate.Text = DateTime.Parse(dt.Rows[0]["Passport_Issue_Date"].ToString()).ToString("dd/MM/yyyy");
        }
        else
        {
            lblPassport_IssueDt.Text = "";
            txtPassport_IssueDate.Text = "";
        }

        if (dt.Rows[0]["Passport_Expiry_Date"].ToString() != "")
        {
            txtPassport_ExpDate.Text = DateTime.Parse(dt.Rows[0]["Passport_Expiry_Date"].ToString()).ToString("dd/MM/yyyy");
            lblPassport_ExpDt.Text = DateTime.Parse(dt.Rows[0]["Passport_Expiry_Date"].ToString()).ToString("dd/MM/yyyy");
            int PassportAlertDays = int.Parse(dt.Rows[0]["PassportAlertDays"].ToString());

            if (PassportAlertDays > 0 && DateTime.Parse(dt.Rows[0]["Passport_Expiry_Date"].ToString()) < DateTime.Today.AddDays(PassportAlertDays))
            {
                lblPassport_ExpDt.BackColor = System.Drawing.Color.Red;
                lblPassport_ExpDt.ForeColor = System.Drawing.Color.Yellow;

                js_notify = js_notify + "Passport is expiring on " + DateTime.Parse(dt.Rows[0]["Passport_Expiry_Date"].ToString()).ToString("dd/MM/yyyy") + "<br>";
            }
            else
            {
                lblPassport_ExpDt.BackColor = System.Drawing.Color.Transparent;
                lblPassport_ExpDt.ForeColor = System.Drawing.Color.Gray;
            }
        }
        else
        {
            txtPassport_ExpDate.Text = "";
            lblPassport_ExpDt.Text = "";
        }

        //--Seaman--//

        lblSeamanBk_No.Text = dt.Rows[0]["Seaman_Book_Number"].ToString();
        lblSeamanBk_Place.Text = dt.Rows[0]["Seaman_Book_PlaceOf_Issue"].ToString();
        txtSeamanBk_No.Text = dt.Rows[0]["Seaman_Book_Number"].ToString();
        txtSeamanBk_Place.Text = dt.Rows[0]["Seaman_Book_PlaceOf_Issue"].ToString();

        if (dt.Rows[0]["Seaman_Book_Issue_Date"].ToString() != "")
        {
            lblSeamanBk_IssueDt.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Issue_Date"].ToString()).ToString("dd/MM/yyyy");
            txtSeamanBk_IssueDate.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Issue_Date"].ToString()).ToString("dd/MM/yyyy");
        }
        else
        {
            lblSeamanBk_IssueDt.Text = "";
            txtSeamanBk_IssueDate.Text = "";
        }

        if (dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString() != "")
        {
            lblSeamanBk_ExpDt.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()).ToString("dd/MM/yyyy");
            txtSeamanBk_ExpDate.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()).ToString("dd/MM/yyyy");
            int SeamanAlertDays = int.Parse(dt.Rows[0]["SeamanAlertDays"].ToString());

            if (SeamanAlertDays > 0 && DateTime.Parse(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()) < DateTime.Today.AddDays(SeamanAlertDays))
            {
                lblSeamanBk_ExpDt.BackColor = System.Drawing.Color.Red;
                lblSeamanBk_ExpDt.ForeColor = System.Drawing.Color.Yellow;

                js_notify = js_notify + "Seaman book is expiring on " + DateTime.Parse(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                lblSeamanBk_ExpDt.BackColor = System.Drawing.Color.Transparent;
                lblSeamanBk_ExpDt.ForeColor = System.Drawing.Color.Gray;
            }
        }
        else
        {
            lblSeamanBk_ExpDt.Text = "";
            txtSeamanBk_ExpDate.Text = "";
        }
        if (js_notify != "")
        {
            js_notify = "ShowNotification('Alert','" + js_notify + "',true);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js_load", js_notify, true);
        }
    }
    protected void Load_Next_Of_Kin(int CrewID)
    {
        DataTable dtNOK = objBLLCrew.Get_Crew_DependentsByCrewID(CrewID, 1);


        if (dtNOK.Rows.Count > 0)
        {
            hdnNOKID.Value = dtNOK.Rows[0]["ID"].ToString();

            lblNOKName.Text = dtNOK.Rows[0]["FullName"].ToString().ToUpper();
            lblNOKrelationship.Text = dtNOK.Rows[0]["Relationship"].ToString();
            lblNOKAddress.Text = dtNOK.Rows[0]["Address"].ToString();
            //lblNOKDOB.Text = Convert.ToDateTime(dtNOK.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
            lblNOKPhone.Text = dtNOK.Rows[0]["Phone"].ToString();

            txtNOKName.Text = dtNOK.Rows[0]["FullName"].ToString().ToUpper();
            ddlNOKRelationship.Text = dtNOK.Rows[0]["Relationship"].ToString();
            txtNOKAddress.Text = dtNOK.Rows[0]["Address"].ToString();
            //txtNOKDOB.Text = Convert.ToDateTime(dtNOK.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
            txtNOKPhone.Text = dtNOK.Rows[0]["Phone"].ToString();

        }
        else
        {
            hdnNOKID.Value = "";

            lblNOKName.Text = "";
            lblNOKrelationship.Text = "";
            lblNOKAddress.Text = "";
            //lblNOKDOB.Text = "";
            lblNOKPhone.Text = "";

            txtNOKName.Text = "";
            ddlNOKRelationship.Text = "";
            txtNOKAddress.Text = "";
            //txtNOKDOB.Text = "";
            txtNOKPhone.Text = "";
        }

        GridView_Dependents.DataBind();
    }

    protected void lnkEditPersonalDetails_Click(object sender, EventArgs e)
    {
        pnlEdit_PersonalDetails.Visible = true;
        pnlView_PersonalDetails.Visible = false;
        lblMessage.Text = "";
        DataTable dt = objCrewAdmin.getEnglishProficiency(GetCrewID()).Tables[0];
        if (dt.Rows.Count > 0)
            ddlEngProficiency.SelectedValue = dt.Rows[0]["English_Proficiency"].ToString() == "" ? "0" : lblEngProficiency.Text.ToString();
     }

    protected void btnSavePersonalDetails_Click(object sender, EventArgs e)
    {
        CrewProperties objCrw = new CrewProperties();
        try
        {
            if (!ValidateDetails())
                return;

         
            objCrw.CrewID = GetCrewID();
            objCrw.RankID = int.Parse(ddlRankAppliedFor.SelectedValue);
            objCrw.Available_From_Date = DateTime.Parse(txtDateAvailable.Text, iFormatProvider);
            objCrw.Surname = txtPD_Surname.Text;
            objCrw.GivenName = txtPD_Givenname.Text;
            objCrw.Alias = txtPD_Alias.Text;
            objCrw.DateOfBirth = DateTime.Parse(txtPD_DOB.Text, iFormatProvider);
            objCrw.PlaceofBirth = txtPD_PlaceOfBirth.Text;
            objCrw.Nationality = int.Parse(ddlPD_Nationality.SelectedValue);
            objCrw.MaritalStatus = ddlPD_MaritalStatus.SelectedValue;
            objCrw.Telephone = txtPD_Phone.Text;
            objCrw.Mobile = txtPD_Mobile.Text;
            objCrw.Address = txtPD_Address.Text;
            objCrw.Fax = txtPD_Fax.Text;
            objCrw.EMail = txtPD_Email.Text;
            objCrw.NearestInternationalAirport = txtPD_Airport.SelectedText;
            objCrw.NearestInternationalAirportID = UDFLib.ConvertToInteger(txtPD_Airport.SelectedValue);
            objCrw.ManningOfficeID = int.Parse(ddlManningOffice.SelectedValue);

            objCrw.USVisa_Flag = UDFLib.ConvertToInteger(rdoUSVisaFlag.SelectedValue);
            objCrw.USVisa_Number = txtPD_USVisaNo.Text;
            if (txtPD_USVisaExpiry.Text != "")
                objCrw.USVisa_Expiry = DateTime.Parse(txtPD_USVisaExpiry.Text);
            else
                objCrw.USVisa_Expiry = DateTime.Parse("1900/01/01");


            objCrw.Modified_By = GetSessionUserID();

            int result = objBLLCrew.UPDATE_CrewPersonalDetails(objCrw);
            objBLLCrew.UPDATE_HeightWaistWeight(GetCrewID(), txtHeight.Text, txtWaist.Text, txtWeight.Text, GetSessionUserID());

            if (result == 1)
            {
                pnlEdit_PersonalDetails.Visible = false;
                pnlView_PersonalDetails.Visible = true;
                lblMessage.Text = "Personal details updated.";
                Load_CrewPersonalDetails(objCrw.CrewID);
                Load_CrewHeightWaiseWeight();
            }
            objCrewAdmin.InsertEnglishProficiency(GetCrewID(), ddlEngProficiency.SelectedValue.ToString() == "0" ? "" : ddlEngProficiency.SelectedValue , GetSessionUserID());

            DataTable dt = objCrewAdmin.getEnglishProficiency(GetCrewID()).Tables[0];
            if (dt.Rows.Count > 0)
                lblEngProficiency.Text = dt.Rows[0]["English_Proficiency"].ToString();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objCrw = null;
        }

    }

    protected void btnCancelPersonalDetails_Click(object sender, EventArgs e)
    {
        pnlEdit_PersonalDetails.Visible = false;
        pnlView_PersonalDetails.Visible = true;
        lblMessage.Text = "";
    }

    protected void lnkEditPassport_Click(object sender, EventArgs e)
    {
        //Load_PassportAndSeamanDetails(GetCrewID());
        pnlView_Passport.Visible = false;
        pnlEdit_Passport.Visible = true;

    }

    protected void btnSavePassportDetails_Click(object sender, EventArgs e)
    {
        string msg = "";

        try
        {   
            if (HiddenField_NationalityId.Value != null)
            {         
                if (txtPassport_No.Text == "" || txtPassport_Place.Text == "" || txtPassport_IssueDate.Text == "" || txtPassport_ExpDate.Text == "")
                {                
                    int Passport_Mandatory = objCrewAdmin.Check_Document_Mandatory(int.Parse(ddlRankAppliedFor.SelectedValue), int.Parse(HiddenField_NationalityId.Value), "PASSPORT");
                    if (Passport_Mandatory == 1)
                    {
                        msg = "Please enter PASSPORT details";
                    }
                }
                else if (txtSeamanBk_No.Text == "" || txtSeamanBk_Place.Text == "" || txtSeamanBk_IssueDate.Text == "" || txtSeamanBk_ExpDate.Text == "")
                {
                    int Seaman_Mandatory = objCrewAdmin.Check_Document_Mandatory(int.Parse(ddlRankAppliedFor.SelectedValue), int.Parse(HiddenField_NationalityId.Value), "SEAMAN");
                    if (Seaman_Mandatory == 1)
                    {
                        msg = "Please enter  SEAMAN BOOK details";
                    }
                }
            }
            if (msg == "")
            {
                int result = 0;
                if (!string.IsNullOrEmpty(txtPassport_No.Text))
                {
                    DataRow[] dtPassportRows = objBLLCrew.GetCrewIfCrewExists("", "", "", txtPassport_No.Text).Select("Passport_Number='" + txtPassport_No.Text + "'");
                    if (dtPassportRows.Count() == 0)
                    {
                        result = objBLLCrew.UPDATE_CrewPassportAndSeamanDetails(GetCrewID(), txtPassport_No.Text, txtPassport_IssueDate.Text, txtPassport_ExpDate.Text, txtPassport_Place.Text, txtSeamanBk_No.Text, txtSeamanBk_IssueDate.Text, txtSeamanBk_ExpDate.Text, txtSeamanBk_Place.Text, GetSessionUserID());
                    }
                    else
                    {
                        msg = "Passport number already exists for other staff.";
                    }

                    if (lblPassport_No.Text == txtPassport_No.Text)
                    {
                        result = 1;
                    }
                }
                else
                {
                    msg = "";
                     result = objBLLCrew.UPDATE_CrewPassportAndSeamanDetails(GetCrewID(), txtPassport_No.Text, txtPassport_IssueDate.Text, txtPassport_ExpDate.Text, txtPassport_Place.Text, txtSeamanBk_No.Text, txtSeamanBk_IssueDate.Text, txtSeamanBk_ExpDate.Text, txtSeamanBk_Place.Text, GetSessionUserID());
                }
                if (result == 1)
                {
                    pnlView_Passport.Visible = true;
                    pnlEdit_Passport.Visible = false;

                    msg = "Passport details updated.";
                    Load_PassportAndSeamanDetails(GetCrewID());
                }
                else
                {
                    msg += "Unable to update Passport details.";
                }
            }

            if (msg != "")
            {
                string js = "alert('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgPP", js, true);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void btnCancelPassportDetails_Click(object sender, EventArgs e)
    {
        pnlView_Passport.Visible = true;
        pnlEdit_Passport.Visible = false;
        lblMessage.Text = "";
    }

    protected void lnkEditNOK_Click(object sender, EventArgs e)
    {
        pnlView_NextOfKin.Visible = false;
        pnlEdit_NextOfKin.Visible = true;
        lblMessage.Text = "";
        //btnSaveNOKDetails.Visible = false;
    }

    protected void btnSaveNOKDetails_Click(object sender, EventArgs e)
    {

        try
        {
            int CrewID = GetCrewID();
            if (CrewID > 0)
            {

                if (hdnNOKID.Value != "" && hdnNOKID.Value != "0")
                {
                    objBLLCrew.UPDATE_Crew_DependentDetails(int.Parse(hdnNOKID.Value), txtNOKName.Text, ddlNOKRelationship.SelectedValue, txtNOKAddress.Text, txtNOKPhone.Text, 1, GetSessionUserID());
                }
                else
                {
                    objBLLCrew.INS_Crew_DependentDetails(CrewID, txtNOKName.Text, ddlNOKRelationship.SelectedValue, txtNOKAddress.Text, txtNOKPhone.Text, 1, GetSessionUserID());
                }
            }
            else
            {
                lblMessage.Text = "Unable to save Next of Kin details !! Crew information missing !!";
            }

            Load_Next_Of_Kin(GetCrewID());

            lblMessage.Text = "Next of Kin details Saved.";
            pnlView_NextOfKin.Visible = true;
            pnlEdit_NextOfKin.Visible = false;

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to save Next of Kin details !! " + ex.Message;
        }

    }
    protected void btnCancelNOKDetails_Click(object sender, EventArgs e)
    {
        pnlView_NextOfKin.Visible = true;
        pnlEdit_NextOfKin.Visible = false;
        lblMessage.Text = "";
    }

    protected void btnSaveDependents_Click(object sender, EventArgs e)
    {
        try
        {

            int CrewID = GetCrewID();
            if (CrewID > 0)
            {
                if (txtDEP_Name.Text == "" || ddlDEP_Relatipnship.SelectedValue == "0")
                {
                    lblMessage.Text = "Unable to save Dependent details !! Crew information missing !!";
                }
                else
                {

                    int result = objBLLCrew.INS_Crew_DependentDetails(CrewID, txtDEP_Name.Text, ddlDEP_Relatipnship.SelectedValue, "", "", 0, GetSessionUserID());

                    objDS_Dependents.SelectParameters["CrewID"].DefaultValue = CrewID.ToString();
                    objDS_Dependents.Select();
                    GridView_Dependents.DataBind();
                    GridView_Dependents.Visible = true;

                    if (result == 1)
                    {
                        lblMessage.Text = "Dependent added successfully !!";
                    }
                    else if (result == 1)
                    {
                        lblMessage.Text = "Unable to save Dependent details !! Depedent details already exists!!";
                    }
                }

            }
            else
            {
                lblMessage.Text = "Enter dependent details and select relationship!!";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to save Dependent details !! " + ex.Message;
        }

    }
    protected void btnCloseDependent_Click(object sender, EventArgs e)
    {
        pnlAddDependents.Visible = false;
        lblMessage.Text = "";
    }

    protected void lnkEditPreviousContacts_Click(object sender, EventArgs e)
    {
       // pnlView_PreviousContacts.Visible = false;
        //pnlEdit_PreviousContacts.Visible = true;
        lblMessage.Text = "";
    }
  
    //--- Save Details --
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";

        CrewProperties objCrw = new CrewProperties();

        if (!ValidateDetails())
            return;

       

        try
        {          

            objCrw.CrewID = GetCrewID();

            objCrw.RankID = int.Parse(ddlRankAppliedFor.SelectedValue);
            objCrw.Available_From_Date = DateTime.Parse(txtDateAvailable.Text, iFormatProvider);

            objCrw.Surname = txtPD_Surname.Text;
            objCrw.GivenName = txtPD_Givenname.Text;
            objCrw.Alias = txtPD_Alias.Text;
            objCrw.DateOfBirth = DateTime.Parse(txtPD_DOB.Text, iFormatProvider);
            objCrw.PlaceofBirth = txtPD_PlaceOfBirth.Text;
            objCrw.Nationality = int.Parse(ddlPD_Nationality.SelectedValue);
            objCrw.MaritalStatus = ddlPD_MaritalStatus.SelectedValue;
            objCrw.Telephone = txtPD_Phone.Text;
            objCrw.Mobile = txtPD_Mobile.Text;
            objCrw.Address = txtPD_Address.Text;
            objCrw.Fax = txtPD_Fax.Text;
            objCrw.EMail = txtPD_Email.Text;
            objCrw.NearestInternationalAirport = txtPD_Airport.Text;
            objCrw.NearestInternationalAirportID = UDFLib.ConvertToInteger(txtPD_Airport.SelectedValue);

            objCrw.Passport_Number = txtPassport_No.Text;

            DateTime dtCDCExpiry;
            DateTime dtCDCIssue;
            DateTime dtPassport_IssueDate;
            DateTime dtPassportExpDate;

            if (DateTime.TryParse(txtPassport_IssueDate.Text, out dtPassport_IssueDate))
                objCrw.Passport_Issue_Date = dtPassport_IssueDate;
            else
                objCrw.Passport_Issue_Date = DateTime.Parse("1900/01/01");

            if (DateTime.TryParse(txtPassport_ExpDate.Text, out dtPassportExpDate))
                objCrw.Passport_Expiry_Date = dtPassportExpDate;
            else
                objCrw.Passport_Expiry_Date = DateTime.Parse("1900/01/01");


            //objCrw.Passport_Issue_Date = DateTime.Parse(txtPassport_IssueDate.Text, iFormatProvider);
            //objCrw.Passport_Expiry_Date = DateTime.Parse(txtPassport_ExpDate.Text, iFormatProvider);
            objCrw.Passport_PlaceOf_Issue = txtPassport_Place.Text;

            objCrw.Seaman_Book_Number = txtSeamanBk_No.Text;
           
            if (DateTime.TryParse(txtSeamanBk_IssueDate.Text, out dtCDCIssue))
                objCrw.Seaman_Book_Issue_Date = dtCDCIssue;
            else
                objCrw.Seaman_Book_Issue_Date = DateTime.Parse("1900/01/01");

            if (DateTime.TryParse(txtSeamanBk_ExpDate.Text, out dtCDCExpiry))
                objCrw.Seaman_Book_Expiry_Date = dtCDCExpiry;
            else
                objCrw.Seaman_Book_Expiry_Date = DateTime.Parse("1900/01/01");

            objCrw.Seaman_Book_PlaceOf_Issue = txtSeamanBk_Place.Text;

            objCrw.Workedwith_Multinational_Crew = 0;
            objCrw.MultinationalCrew_Nationalities = "";
            objCrw.ManningOfficeID = int.Parse(ddlManningOffice.SelectedValue);

            objCrw.USVisa_Flag = UDFLib.ConvertToInteger(rdoUSVisaFlag.SelectedValue);
            objCrw.USVisa_Number = txtPD_USVisaNo.Text;
            if (txtPD_USVisaExpiry.Text != "")
                objCrw.USVisa_Expiry = DateTime.Parse(txtPD_USVisaExpiry.Text);
            else
                objCrw.USVisa_Expiry = DateTime.Parse("1900/01/01");

            objCrw.Created_By = GetSessionUserID();
            objCrw.Modified_By = GetSessionUserID();

            if (HiddenField_AccType.Value == "")
            {
                objCrw.Allotment_AccType = "BOTH";
            }
            else
            {
                objCrw.Allotment_AccType = HiddenField_AccType.Value.ToString();
            }

            int iNewCrewID = 0;
            if (GetCrewID() > 0)
            {
                objBLLCrew.UPDATE_CrewPersonalDetails(objCrw);
                objBLLCrew.UPDATE_CrewPassportAndSeamanDetails(objCrw.CrewID, objCrw.Passport_Number, txtPassport_IssueDate.Text, txtPassport_ExpDate.Text, objCrw.Passport_PlaceOf_Issue, objCrw.Seaman_Book_Number, txtSeamanBk_IssueDate.Text, txtSeamanBk_ExpDate.Text, objCrw.Seaman_Book_PlaceOf_Issue, GetSessionUserID());
                objBLLCrew.UPDATE_HeightWaistWeight(GetCrewID(), txtHeight.Text, txtWaist.Text, txtWeight.Text, GetSessionUserID());

                if (hdnNOKID.Value != "" && hdnNOKID.Value != "0")
                {
                    objBLLCrew.UPDATE_Crew_DependentDetails(int.Parse(hdnNOKID.Value), txtNOKName.Text, ddlNOKRelationship.SelectedValue, txtNOKAddress.Text, txtNOKPhone.Text, 1, GetSessionUserID());
                }
            }
            else
            {
                //-- NEW CREW ADDED --
                
                DataTable dtCrew = objBLLCrew.GetCrewIfCrewExists(objCrw.GivenName, objCrw.Surname, objCrw.DateOfBirth.ToString("dd/MM/yyyy"),objCrw.Passport_Number);
                if (dtCrew.Rows.Count == 0)
                {
                    iNewCrewID = objBLLCrew.INS_NewCrewDetails(objCrw);
                    HiddenField_CrewID.Value = iNewCrewID.ToString();
                    if (iNewCrewID > 0)
                    {
                          objBLLCrew.UPDATE_HeightWaistWeight(iNewCrewID, txtHeight.Text, txtWaist.Text, txtWeight.Text, GetSessionUserID());
                          //English Proficiency
                          if (ddlEngProficiency.SelectedIndex > 0)
                          {
                              objCrewAdmin.InsertEnglishProficiency(iNewCrewID, ddlEngProficiency.SelectedValue, GetSessionUserID());
                          }

                          DataTable dt = objCrewAdmin.getEnglishProficiency(iNewCrewID).Tables[0];
                          if (dt.Rows.Count > 0)
                              lblEngProficiency.Text = dt.Rows[0]["English_Proficiency"].ToString();

                    }
                }
                else
                {
                    grdExistingCrew.DataSource = dtCrew;
                    grdExistingCrew.DataBind();

                    string js = "alert('Staff with Same Name OR Same Passport number exists.');showModal('dvExistingCrew');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CrewExists", js, true);
                }
            }
       

            if (iNewCrewID > 0)
            {
                Session["NewCrew"] = "NewCrew";

                Load_CrewPersonalDetails(iNewCrewID);
                Load_Next_Of_Kin(iNewCrewID);
                Load_CrewHeightWaiseWeight();

                pnlView_PersonalDetails.Visible = true;
                pnlEdit_PersonalDetails.Visible = false;

                pnlView_Passport.Visible = true;
                pnlEdit_Passport.Visible = false;

                pnlEdit_NextOfKin.Visible = true;
                lnkAddDependents.Visible = true;

                btnSaveDetails.Enabled = false;


                lblMessage.Text = "Crew details Added !! Please add the Next of Kin and Dependent Details";
                Response.Redirect("AddEditCrew.aspx?ID=" + iNewCrewID.ToString());

            }
            else
            {
                //lblMessage.Text = "Crew details updated !!";
            }

           
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objCrw = null;
         }
     

    }
    protected void btnCloseDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewList.aspx");
    }
    protected void btnCrewDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDetails.aspx?ID=" + getQueryString("ID"));
    }

    protected bool checkSpecialCharacter(string Value_)
    {
        Regex reg = new Regex(@"^[A-Za-z\s]{2,200}$");

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected bool checkEmail(string Value_)
    {
        Regex reg = new Regex(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z][A-Z]{2})?)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected Boolean ValidateDetails()
    {
        if (ddlManningOffice.SelectedValue == "0")
        {
            showAlert("Please select the MANNING OFFICE for Employment.");
            return false;
        }
        else if (ddlRankAppliedFor.SelectedValue == "0")
        {
            showAlert("Please select the APPLICATION for Employment..");
            return false;
        }
        else if (txtDateAvailable.Text == "")
        {
            showAlert("Please select DATE AVAILABLE FOR JOINING.");
            return false;
        }
        else if (txtDateAvailable.Text != "" && Convert.ToDateTime(txtDateAvailable.Text).Date < DateTime.Now.Date)
        {
            showAlert("DATE AVAILABLE FOR JOINING should be equal or greater than from todays date.");
            return false;
        }
        else if (txtPD_Surname.Text != "" && !checkSpecialCharacter(txtPD_Surname.Text))
        {
            showAlert("Please enter a valid SURNAME.");
            return false;
        }
        else if (txtPD_Givenname.Text == "" || !checkSpecialCharacter(txtPD_Givenname.Text))
        {
            showAlert("Please enter a valid FIRST NAME.");
            return false;
        }
        else if (txtPD_DOB.Text == "")
        {
            showAlert("Please select DATE OF BIRTH.");
            return false;
        }
        else if (txtPD_PlaceOfBirth.Text.Trim() == "")
        {
            showAlert("Please enter PLACE OF BIRTH.");
            return false;
        }
        else if (ddlPD_Nationality.SelectedValue == "0")
        {
            showAlert("Please select NATIONALITY.");
            return false;
        }
        else if (txtPD_Address.Text == "")
        {
            showAlert("Please enter ADDRESS.");
            return false;
        }
        else if (txtPD_Mobile.Text == "")
        {
            showAlert("Please enter MOBILE NUMBER");
            return false;
        }
        else if (rdoUSVisaFlag.SelectedIndex == -1)
        {
            showAlert("Please select the appropriate option for US Visa Status.");
            return false;
        }
        else if (txtPD_Email.Text.Trim() == "" || !checkEmail(txtPD_Email.Text))
        {
            if (txtPD_Email.Text.Trim() == "" )
                showAlert("Please enter E-mail");
            else
                showAlert("Please enter Valid E-mail");
            return false;
        }

        if (txtHeight.Text.Trim() == "")
        {
            showAlert("Please enter Height.");
            return false;
        }
        else
        {
            try
            {
                decimal h = decimal.Parse(txtHeight.Text);
            }
            catch
            {
                showAlert("Please enter numeric value in Height field.");
                return false;
            }
        }

        if (txtWaist.Text.Trim() == "")
        {
            showAlert("Please enter Waist.");
            return false;
        }
        else
        {
            try
            {
                decimal w = decimal.Parse(txtWaist.Text);
            }
            catch
            {
                showAlert("Please enter numeric value in Waist field.");
                return false;
            }
        }

        if (txtWeight.Text.Trim() == "")
        {
            showAlert("Please enter Weight.");
            return false;
        }
        else
        {
            try
            {
                decimal wt = decimal.Parse(txtWeight.Text);
            }
            catch
            {
                showAlert("Please enter numeric value in Weight field.");
                return false;
            }
        }
        if (HiddenField_NationalityId.Value != null || ddlPD_Nationality.SelectedIndex > 0)
        {
            int NationalityId = 0;
            if (HiddenField_NationalityId.Value == null || HiddenField_NationalityId.Value == "")
                NationalityId = int.Parse(ddlPD_Nationality.SelectedValue.ToString());
            else
                NationalityId = int.Parse(HiddenField_NationalityId.Value);

            if (txtPassport_No.Text == "" || txtPassport_Place.Text == "" || txtPassport_IssueDate.Text == "" || txtPassport_ExpDate.Text == "")
            {
                int Passport_Mandatory = objCrewAdmin.Check_Document_Mandatory(int.Parse(ddlRankAppliedFor.SelectedValue), NationalityId, "PASSPORT");
                if (Passport_Mandatory == 1)
                {
                    showAlert("Please enter PASSPORT details");
                    return false;
                }
            }
            if (txtSeamanBk_No.Text == "" || txtSeamanBk_Place.Text == "" || txtSeamanBk_IssueDate.Text == "" || txtSeamanBk_ExpDate.Text == "")
            {
                int Seaman_Mandatory = objCrewAdmin.Check_Document_Mandatory(int.Parse(ddlRankAppliedFor.SelectedValue), NationalityId, "SEAMAN");
                if (Seaman_Mandatory == 1)
                {
                    showAlert("Please enter  SEAMAN BOOK details");
                    return false;
                }
            }
        }
        if (txtDateAvailable.Text != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(txtDateAvailable.Text);
                if (dt > DateTime.Today.AddMonths(6))
                {
                    showAlert("Date of available cannot be more than 6 months!!");
                    return false;
                }
            }
            catch
            {
                showAlert("Invalid entry in DATE AVAILABLE field. Please enter in DD/MM/YYYY format");
                return false;
            }
        }
        if (txtPD_DOB.Text != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(txtPD_DOB.Text);
            }
            catch
            {
                showAlert("Invalid entry in DATE OF BIRTH field. Please enter in DD/MM/YYYY format");
                return false;
            }
        }
        if (rdoUSVisaFlag.SelectedValue == "1")
        {
            DateTime dt;
            if (txtPD_USVisaNo.Text == "" || txtPD_USVisaExpiry.Text == "")
            {
                showAlert("Please enter US Visa number and expiry date.");
                return false;
            }
            else if (DateTime.TryParse(txtPD_USVisaExpiry.Text, out dt) == false)
            {
                showAlert("Invalid entry in US VISA EXPIRY DATE field. Please enter in DD/MM/YYYY format");
                return false;
            }

        }
        if (txtPassport_IssueDate.Text != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(txtPassport_IssueDate.Text);
            }
            catch
            {
                showAlert("Invalid entry in PASSPORT ISSUE DATE field. Please enter in DD/MM/YYYY format");
                return false;
            }
        }
        if (txtPassport_ExpDate.Text != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(txtPassport_ExpDate.Text);
            }
            catch
            {
                showAlert("Invalid entry in PASSPORT EXPIRY field. Please enter in DD/MM/YYYY format");
                return false;
            }
        }
        if (txtSeamanBk_IssueDate.Text != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(txtSeamanBk_IssueDate.Text);
            }
            catch
            {
                showAlert("Invalid entry in SEAMAN BOOK ISSUE DATE field. Please enter in DD/MM/YYYY format");
                return false;
            }
        }
        if (txtSeamanBk_ExpDate.Text != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(txtSeamanBk_ExpDate.Text);
            }
            catch
            {
                showAlert("Invalid entry in SEAMAN BOOK EXPIRY field. Please enter in DD/MM/YYYY format");
                return false;
            }
        }
        
        return true;
    }
    protected void showAlert(string msg)
    {
        string js = "alert('" + msg + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
    protected int GetRankCategoryID(int RankID)
    {
        int retVal = 0;
        DataTable dt = objCrewAdmin.Get_RankCategoryByRankID(RankID);
        if (dt.Rows.Count > 0)
        {
            retVal = UDFLib.ConvertToInteger(dt.Rows[0]["id"].ToString());
        }
        return retVal;
    }  

    public void getEnglishProficiency()
    {
       ddlEngProficiency.DataSource= objCrewAdmin.getEnglishProficiency(null);
       ddlEngProficiency.DataTextField = "EnglishProficiency";
       ddlEngProficiency.DataValueField = "EnglishProficiency";
       ddlEngProficiency.DataBind();
       ddlEngProficiency.Items.Insert(0, new ListItem("-Select-", "0"));

      DataTable dt= objCrewAdmin.getEnglishProficiency(GetCrewID()).Tables[0];
        if(dt.Rows.Count>0)
      lblEngProficiency.Text = dt.Rows[0]["English_Proficiency"].ToString();
    }
}