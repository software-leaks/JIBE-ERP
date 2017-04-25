using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_Personal : System.Web.UI.Page
{
    int i = 1;
    BLL_Crew_Admin obj = new BLL_Crew_Admin();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    public string DFormat = "";
    public string TodayDateFormat = "";

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["USERID"] == null)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
            }
            else
            {
                TodayDateFormat = UDFLib.DateFormatMessage();

                DFormat = UDFLib.GetDateFormat();
                CalendarExtender1.Format = DFormat;
                CalendarExtender10.Format = DFormat;
                CalendarExtender12.Format = DFormat;
                CalendarExtender13.Format = DFormat;
                CalendarExtender2.Format = DFormat;
                CalendarExtender3.Format = DFormat;
                CalendarExtender9.Format = DFormat;
                CalendarExtenderMMCE.Format = DFormat;
                CalendarExtenderMMCI.Format = DFormat;
                CalendarExtenderTWICE.Format = DFormat;
                CalendarExtenderTWICI.Format = DFormat;

                CalendarExtender12.EndDate = CalendarExtender13.EndDate = CalendarExtenderMMCI.EndDate = CalendarExtenderTWICI.EndDate = CalendarExtender3.EndDate = DateTime.Now;

                if (!IsPostBack)
                {

                    if (Session["USERCOMPANY"] != null)
                        hdnClientName.Value = Convert.ToString(Session["USERCOMPANY"]);

                    UserAccessValidation();
                    ConfidentialityCheck();
                    tbltxtContact.Visible = false;
                    tbltextGeneral.Visible = false;
                    tblDocText.Visible = false;
                    trHeight.Visible = false;
                    trUniform.Visible = false;
                    tblConText.Visible = false;

                    CustomFieldValidation();
                    Load_CountryList();
                    BindSchool();
                    BindSchoolYear();
                    BindVeteran();
                    BindEnglishProf();
                    Load_PassportAndSeamanDetails(GetCrewID());

                    #region DataBind

                    DataSet dss = obj.CRW_CD_GetConfidentialDetails(GetCrewID());
                    if (dss != null)
                    {
                        ViewState["RankApplied"] = dss.Tables[0].Rows[0]["Rank_Applied"].ToString();
                        ViewState["Nationality"] = dss.Tables[0].Rows[0]["Staff_Nationality"].ToString();
                        hdnCrewName.Value = Convert.ToString(dss.Tables[0].Rows[0]["Staff_Name"]) + " " + Convert.ToString(dss.Tables[0].Rows[0]["Staff_Surname"]);
                        #region Check whether passport and seaman book is mandatory form applied rank and nationality
                        if (!string.IsNullOrEmpty(ViewState["RankApplied"].ToString()))
                        {
                            if (obj.Check_Document_Mandatory(int.Parse(ViewState["RankApplied"].ToString()), int.Parse(ViewState["Nationality"].ToString()), "PASSPORT") != 0)
                            {
                                lblP1.Visible = true;
                                lblP2.Visible = true;
                                lblP3.Visible = true;
                                lblP4.Visible = true;
                                txtPassport_ExpDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                txtPassport_IssueDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                txtPassport_No.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                txtPassport_Place.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");

                            }
                            else
                            {
                                txtPassport_ExpDate.BackColor = System.Drawing.Color.Transparent;
                                txtPassport_IssueDate.BackColor = System.Drawing.Color.Transparent;
                                txtPassport_No.BackColor = System.Drawing.Color.Transparent;
                                txtPassport_Place.BackColor = System.Drawing.Color.Transparent;
                            }


                            if (obj.Check_Document_Mandatory(int.Parse(ViewState["RankApplied"].ToString()), int.Parse(ViewState["Nationality"].ToString()), "SEAMAN") != 0)
                            {
                                lblS1.Visible = true;
                                lblS2.Visible = true;
                                lblS3.Visible = true;
                                lblS4.Visible = true;
                                txtSeamanBk_ExpDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                txtSeamanBk_IssueDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                txtSeamanBk_No.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                txtSeamanBk_Place.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            }
                            else
                            {
                                txtSeamanBk_ExpDate.BackColor = System.Drawing.Color.Transparent;
                                txtSeamanBk_IssueDate.BackColor = System.Drawing.Color.Transparent;
                                txtSeamanBk_No.BackColor = System.Drawing.Color.Transparent;
                                txtSeamanBk_Place.BackColor = System.Drawing.Color.Transparent;
                            }
                        }
                        else
                        {
                            txtPassport_ExpDate.BackColor = System.Drawing.Color.Transparent;
                            txtPassport_IssueDate.BackColor = System.Drawing.Color.Transparent;
                            txtPassport_No.BackColor = System.Drawing.Color.Transparent;
                            txtPassport_Place.BackColor = System.Drawing.Color.Transparent;
                            txtSeamanBk_ExpDate.BackColor = System.Drawing.Color.Transparent;
                            txtSeamanBk_IssueDate.BackColor = System.Drawing.Color.Transparent;
                            txtSeamanBk_No.BackColor = System.Drawing.Color.Transparent;
                            txtSeamanBk_Place.BackColor = System.Drawing.Color.Transparent;
                        }
                        #endregion


                        DataTable dtTwic = dss.Tables[1];
                        DataTable dtMMC = dss.Tables[2];
                        if (dtTwic.Rows.Count > 0)
                        {
                            txtTWICNumber.Text = dtTwic.Rows[0]["DocNo"].ToString();
                            txtTWICPOI.Text = dtTwic.Rows[0]["PlaceOfIssue"].ToString();
                            if (!string.IsNullOrEmpty(dtTwic.Rows[0]["DateOfExpiry"].ToString()))
                                txtTWICDOE.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtTwic.Rows[0]["DateOfExpiry"].ToString()));
                            else
                                txtTWICDOE.Text = "";
                            if (!string.IsNullOrEmpty(dtTwic.Rows[0]["DateOfIssue"].ToString()))
                                txtTWICDOI.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtTwic.Rows[0]["DateOfIssue"].ToString()));
                            else
                                txtTWICDOI.Text = "";
                            lblTWIC.Text = txtTWICNumber.Text;
                            lblTWICPOI.Text = txtTWICPOI.Text;
                            lblTWICDOE.Text = txtTWICDOE.Text;
                            lblTWICDOI.Text = txtTWICDOI.Text;
                            if (Convert.ToString(dtTwic.Rows[0]["CountryOfIssue"]) != "")
                            {
                                drpTWICCountry.ClearSelection();
                                drpTWICCountry.Items.FindByValue(Convert.ToString(dtTwic.Rows[0]["CountryOfIssue"])).Selected = true;
                                if (drpTWICCountry.SelectedIndex != 0)
                                    lblTWICCountry.Text = drpTWICCountry.SelectedItem.Text;
                                else
                                    lblTWICCountry.Text = "";
                            }
                        }

                        if (dtMMC.Rows.Count > 0)
                        {
                            txtMMCNO.Text = dtMMC.Rows[0]["DocNo"].ToString();
                            txtMMCPOI.Text = dtMMC.Rows[0]["PlaceOfIssue"].ToString();
                            if (!string.IsNullOrEmpty(dtMMC.Rows[0]["DateOfExpiry"].ToString()))
                                txtMMCDOE.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtMMC.Rows[0]["DateOfExpiry"].ToString()));
                            else
                                txtMMCDOE.Text = "";
                            if (!string.IsNullOrEmpty(dtMMC.Rows[0]["DateOfIssue"].ToString()))
                                txtMMCDOI.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtMMC.Rows[0]["DateOfIssue"].ToString()));
                            else
                                txtMMCDOI.Text = "";
                            lblMMCNo.Text = txtMMCNO.Text;
                            lblMMCPOI.Text = txtMMCPOI.Text;
                            lblMMCDOE.Text = txtMMCDOE.Text;
                            lblMMCDOI.Text = txtMMCDOI.Text;
                            if (Convert.ToString(dtMMC.Rows[0]["CountryOfIssue"]) != "")
                            {
                                drpMMCCountry.ClearSelection();
                                drpMMCCountry.Items.FindByValue(Convert.ToString(dtMMC.Rows[0]["CountryOfIssue"])).Selected = true;
                                if (drpMMCCountry.SelectedIndex != 0)
                                    lblMMCCountry.Text = drpMMCCountry.SelectedItem.Text;
                                else
                                    lblMMCCountry.Text = "";
                            }
                        }
                        DataTable ds = dss.Tables[0];
                        DataTable dsM = dss.Tables[4];
                        DataTable dsP = dss.Tables[4];
                        if (ds.Rows.Count > 0)
                        {
                            lblNOKAddress1.Text = ds.Rows[0]["AddressLine1"].ToString();
                            lblNOKAddress2.Text = ds.Rows[0]["AddressLine2"].ToString();
                            lblAddress1.Text = lblNOKAddress1.Text;
                            lblAddress2.Text = lblNOKAddress2.Text;
                            txtCity.Text = ds.Rows[0]["City"].ToString();
                            lblCity.Text = txtCity.Text;
                            txtState.Text = ds.Rows[0]["State"].ToString();
                            lblState.Text = txtState.Text;
                            ddlCountry.SelectedValue = ds.Rows[0]["CountryId"].ToString();
                            if (ddlCountry.SelectedIndex != 0)
                                lblUSCountry.Text = ddlCountry.SelectedItem.Text;
                            else
                                lblUSCountry.Text = "";


                            lblZipCode.Text = ds.Rows[0]["Zipcode"].ToString();
                            lblZip.Text = lblZipCode.Text;
                            txtIAddress.Text = ds.Rows[0]["Address"].ToString();
                            lblAddress.Text = txtIAddress.Text.Replace("\n", "<br/>");
                            txtFax.Text = ds.Rows[0]["Fax"].ToString();
                            txtIFax.Text = ds.Rows[0]["Fax"].ToString();
                            lblFax.Text = txtFax.Text;
                            lblIFax.Text = txtIFax.Text;
                            txtPD_Airport.Text = ds.Rows[0]["NearestAirport"].ToString();
                            lblAirport.Text = txtPD_Airport.Text;
                            if (!string.IsNullOrEmpty(ds.Rows[0]["NearestAirportID"].ToString()))
                            {
                                txtPD_Airport.SelectedValue = ds.Rows[0]["NearestAirportID"].ToString();
                                ViewState["AirportID"] = ds.Rows[0]["NearestAirportID"].ToString();
                                ViewState["Airport"] = ds.Rows[0]["NearestAirport"].ToString();
                            }
                            
                            ddlVeteran.SelectedValue = ds.Rows[0]["VeteranStatus"].ToString();
                            if (ddlVeteran.SelectedIndex != 0)
                                lblVeteran.Text = ddlVeteran.SelectedItem.Text;
                            else
                                lblVeteran.Text = "";
                            if (!string.IsNullOrEmpty(ds.Rows[0]["School"].ToString()))
                                ddlSchool.SelectedValue = ds.Rows[0]["School"].ToString();
                            else
                                ddlSchool.SelectedIndex = 0;
                            if (ddlSchool.SelectedIndex != 0)
                                lblSchool.Text = ddlSchool.SelectedItem.Text;
                            else
                                lblSchool.Text = "";
                            if (!string.IsNullOrEmpty(ds.Rows[0]["SchoolYearGraduated"].ToString()))
                                ddlSchoolGraduated.SelectedValue = ds.Rows[0]["SchoolYearGraduated"].ToString();
                            else
                                ddlSchoolGraduated.SelectedIndex = 0;
                            if (ddlSchoolGraduated.SelectedIndex != 0)
                                lblSchoolYear.Text = ddlSchoolGraduated.SelectedItem.Text;
                            else
                                lblSchoolYear.Text = "";

                            if (ds.Rows[0]["Naturaliztion"].ToString() == "True")
                            {
                                ddlNaturalization.SelectedValue = "1";
                                lblNaturalization.Text = "Yes";
                            }
                            else if (ds.Rows[0]["Naturaliztion"].ToString() == "False" || string.IsNullOrEmpty(Convert.ToString(ds.Rows[0]["Naturaliztion"])))
                            {
                                ddlNaturalization.SelectedValue = "0";
                                lblNaturalization.Text = "No";
                            }

                            if (!string.IsNullOrEmpty(ds.Rows[0]["NaturaliztionDate"].ToString()))
                                txtNaturalizationYear.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Rows[0]["NaturaliztionDate"].ToString()));
                            else
                                txtNaturalizationYear.Text = ds.Rows[0]["NaturaliztionDate"].ToString();
                            lblNaturlizationDate.Text = txtNaturalizationYear.Text;

                            if (dsM.Rows.Count > 0)
                            {
                                if (!string.IsNullOrWhiteSpace(Convert.ToString(dsM.Rows[0]["English_Proficiency"])))
                                    ddlEnglish.SelectedValue = dsM.Rows[0]["English_Proficiency"].ToString();
                                else
                                    ddlEnglish.SelectedValue = "0";
                            }
                            else
                                ddlEnglish.SelectedValue = "0";

                            if (ddlEnglish.SelectedIndex != 0)
                                lblEnglish.Text = ddlEnglish.SelectedItem.Text;
                            else
                                lblEnglish.Text = "";

                            if (ds.Rows[0]["Us_Visa_Flag"].ToString() == "1")
                            {
                                rdbUS.SelectedIndex = 0;
                                lblUsVisa.Text = "Yes";
                                if (!string.IsNullOrEmpty(ds.Rows[0]["Us_Visa_Expiry"].ToString()))
                                    txtUSExpiry.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Rows[0]["Us_Visa_Expiry"].ToString()));
                                else
                                    txtUSExpiry.Text = "";
                                lblUsVisaExpiry.Text = txtUSExpiry.Text;
                                if (!string.IsNullOrEmpty(ds.Rows[0]["Us_Visa_IssueDate"].ToString()))
                                    txtUSIssue.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Rows[0]["Us_Visa_IssueDate"].ToString()));
                                else
                                    txtUSIssue.Text = "";
                                lblUSVisaIssue.Text = txtUSIssue.Text;
                                txtUSVisaNumber.Text = ds.Rows[0]["Us_Visa_Number"].ToString();
                                lblUsVisaNumber.Text = txtUSVisaNumber.Text;
                            }
                            else
                            {
                                rdbUS.SelectedIndex = 1;
                                txtUSExpiry.Text = "";
                                txtUSIssue.Text = "";
                                txtUSVisaNumber.Text = "";
                                lblUsVisa.Text = "No";
                            }


                            txtRemark.Text = ds.Rows[0]["Remarks_2"].ToString();
                            lblRemarks.Text = txtRemark.Text;

                            Load_CrewUniformSize(GetCrewID());
                            Load_CrewHeightWaiseWeight(GetCrewID());

                            txtCon1.Text = ds.Rows[0]["CustomField1"].ToString();
                            txtCon2.Text = ds.Rows[0]["CustomField2"].ToString();
                            txtCon3.Text = ds.Rows[0]["CustomField3"].ToString();
                            lblCF1.Text = txtCon1.Text;
                            lblCF2.Text = txtCon2.Text;
                            lblCF3.Text = txtCon3.Text;
                            txtEmail.Text = ds.Rows[0]["Email"].ToString();
                            txtMobile.Text = ds.Rows[0]["Mobile"].ToString();
                        }
                    }
                    #endregion

                    tdtxtAirport.Visible = true;
                    tdAirport.Visible = false;
                    txtNaturalizationYear.Enabled = false;
                    DataTable dtAddress = obj.CRW_GetCDConfiguration("Addressformat").Tables[0];
                    if (dtAddress.Rows.Count > 0)
                    {

                        //International Address
                        if (dtAddress.Rows[0]["Value"].ToString() == "True")
                        {
                            tdI.Visible = true;
                            tdU.Visible = false;
                            tdILabel.Visible = true;
                            tdULabel.Visible = false;
                        }
                        else
                        {
                            tdI.Visible = false;
                            tdU.Visible = true;
                            tdILabel.Visible = false;
                            tdULabel.Visible = true;
                        }
                    }
                    #region LinkClientClick

                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
                    lnkEditContact.OnClientClick = "EditContactDetails(" + CrewID.ToString() + "); return false;";
                    lnkEditGeneral.OnClientClick = "EditGeneralDetails(" + CrewID.ToString() + "); return false;";
                    lnkEdiTPassport.OnClientClick = "EditPassportDetails(" + CrewID.ToString() + "); return false;";
                    lnkEditDimension.OnClientClick = "EditDimensionDetails(" + CrewID.ToString() + "); return false;";
                    lnkEditConfig.OnClientClick = "EditConfiguredFieldDetails(" + CrewID.ToString() + "); return false;";
                    #endregion

                    #region QueryStringPopUP

                    if (Request.QueryString["Mode"] == "EDITCONTACT")
                    {
                        pnl_Config.Visible = false;
                        pnl_Dimension.Visible = false;
                        pnl_Documents.Visible = false;
                        pnl_general.Visible = false;
                        pnl_contactInformation.Visible = true;
                        lnkEditContact.Visible = false;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        EnableControls();
                        tblLblContact.Visible = false;
                        tbltxtContact.Visible = true;
                    }
                    if (Request.QueryString["Mode"] == "EDITGeneral")
                    {
                        pnl_Config.Visible = false;
                        pnl_Dimension.Visible = false;
                        pnl_Documents.Visible = false;
                        pnl_general.Visible = true;
                        pnl_contactInformation.Visible = false;
                        lnkEditGeneral.Visible = false;
                        tdtxtAirport.Visible = false;
                        tdAirport.Visible = true;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        EnableControls();
                        rdbUS_SelectedIndexChanged(sender, e);
                        tbltextGeneral.Visible = true;
                        tblLabelGeneral.Visible = false;
                        ddlNaturalization_SelectedIndexChanged(sender, e);
                        ddlSchool_SelectedIndexChanged(sender, e);
                    }
                    if (Request.QueryString["Mode"] == "EDITPassport")
                    {
                        pnl_Documents.Height = 180;
                        pnl_Config.Visible = false;
                        pnl_Dimension.Visible = false;
                        pnl_Documents.Visible = true;
                        pnl_general.Visible = false;
                        pnl_contactInformation.Visible = false;
                        lnkEdiTPassport.Visible = false;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        tblDocPanel.Visible = false;
                        tblDocText.Visible = true;

                        ddlVeteran.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");

                        EnableControls();
                        rdbUS_SelectedIndexChanged(sender, e);
                    }
                    if (Request.QueryString["Mode"] == "EDITDimension")
                    {

                        pnl_Config.Visible = false;
                        pnl_Dimension.Visible = true;
                        pnl_Documents.Visible = false;
                        pnl_general.Visible = false;
                        pnl_contactInformation.Visible = false;
                        lnkEditDimension.Visible = false;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        EnableControls();
                        if (trHeightLbl.Visible == true)
                        {
                            trHeight.Visible = true;
                            trHeightLbl.Visible = false;
                        }
                        if (trUniformlbl.Visible == true)
                        {
                            trUniform.Visible = true;
                            trUniformlbl.Visible = false;
                        }


                    }
                    if (Request.QueryString["Mode"] == "EDITConf")
                    {
                        pnl_Config.Visible = true;
                        pnl_Dimension.Visible = false;
                        pnl_Documents.Visible = false;
                        pnl_general.Visible = false;
                        pnl_contactInformation.Visible = false;
                        lnkEditConfig.Visible = false;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        EnableControls();
                        tblConLabel.Visible = false;
                        tblConText.Visible = true;
                    }
                    #endregion
                }

            }


        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            pnl_Config.Visible = false;
            pnl_contactInformation.Visible = false;
            pnl_Dimension.Visible = false;
            pnl_Documents.Visible = false;
            pnl_general.Visible = false;
        }

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
            lnkEditConfig.Visible = false;
            lnkEditContact.Visible = false;
            lnkEditDimension.Visible = false;
            lnkEditGeneral.Visible = false;
            lnkEdiTPassport.Visible = false;
        }

        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --

    }
    protected void Load_CountryList()
    {
        try
        {
            BLL_Infra_Country objCountry = new BLL_Infra_Country();
            DataTable dt = new DataTable();
            dt = objCountry.Get_CountryList();

            if (dt.Rows.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();

                drpPassportCountry.DataSource = dt;
                drpPassportCountry.DataTextField = "COUNTRY";
                drpPassportCountry.DataValueField = "ID";
                drpPassportCountry.DataBind();

                drpSeamanCountry.DataSource = dt;
                drpSeamanCountry.DataTextField = "COUNTRY";
                drpSeamanCountry.DataValueField = "ID";
                drpSeamanCountry.DataBind();

                drpMMCCountry.DataSource = dt;
                drpMMCCountry.DataTextField = "COUNTRY";
                drpMMCCountry.DataValueField = "ID";
                drpMMCCountry.DataBind();

                drpTWICCountry.DataSource = dt;
                drpTWICCountry.DataTextField = "COUNTRY";
                drpTWICCountry.DataValueField = "ID";
                drpTWICCountry.DataBind();

                dt.DefaultView.RowFilter = "";
                dt.DefaultView.RowFilter = "ISO_Code='US'";
                if (dt.DefaultView.Count > 0)
                    hdnUSCountryID.Value = Convert.ToString(dt.DefaultView[0]["ID"]);
            }
        }
        catch (Exception ex)
        {
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            drpPassportCountry.DataSource = null;
            drpPassportCountry.DataBind();
            drpSeamanCountry.DataSource = null;
            drpSeamanCountry.DataBind();
            drpMMCCountry.DataSource = null;
            drpMMCCountry.DataBind();
            drpTWICCountry.DataSource = null;
            drpTWICCountry.DataBind();
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {
            ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            drpPassportCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            drpSeamanCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            drpMMCCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            drpTWICCountry.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }

    protected void BindSchool()
    {

        ddlSchool.DataSource = obj.CRUD_School("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlSchool.DataValueField = "ID";
        ddlSchool.DataTextField = "School";
        ddlSchool.DataBind();
        ddlSchool.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void BindSchoolYear()
    {
        int startYear = 1950;
        int EndYear = DateTime.Now.Year;
        int yearDiff = EndYear - startYear;
        ddlSchoolGraduated.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-SELECT-", "0"));
        ddlSchoolGraduated.Items.Insert(1, new System.Web.UI.WebControls.ListItem(startYear.ToString(), startYear.ToString()));
        for (int i = 1; i <= yearDiff; i++)
        {
            int ins = startYear + i;
            ddlSchoolGraduated.Items.Insert(1, new System.Web.UI.WebControls.ListItem(ins.ToString(), ins.ToString()));
        }
        ddlSchoolGraduated.DataBind();
    }

    protected void BindVeteran()
    {

        ddlVeteran.DataSource = obj.CRUD_VeteranStatus("", "R", UDFLib.ConvertToInteger(GetSessionUserID()), 0, "", "VeteranStatus", null, 1, 10000, ref i, ref i);
        ddlVeteran.DataValueField = "ID";
        ddlVeteran.DataTextField = "VeteranStatus";
        ddlVeteran.DataBind();
        ddlVeteran.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void BindEnglishProf()
    {
        DataSet ds = new DataSet();
        ds = obj.getEnglishProficiency(null);

        ddlEnglish.DataSource = ds;
        ddlEnglish.DataTextField = "EnglishProficiency";
        ddlEnglish.DataValueField = "EnglishProficiency";
        ddlEnglish.DataBind();
        ddlEnglish.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void EnableControls()
    {
        lblNOKAddress2.Enabled = true;
        lblNOKAddress1.Enabled = true;
        txtCity.Enabled = true;
        txtState.Enabled = true;
        ddlCountry.Enabled = true;
        lblZipCode.Enabled = true;
        txtFax.Enabled = true;
        txtIFax.Enabled = true;
        txtAirport.Enabled = true;
        ddlVeteran.Enabled = true;
        ddlSchool.Enabled = true;
        ddlNaturalization.Enabled = true;
        ddlEnglish.Enabled = true;
        txtNaturalizationYear.Enabled = true;
        txtRemark.Enabled = true;
        txtIAddress.Enabled = true;
        txtShoeSize.Enabled = true;
        txtTShirtSize.Enabled = true;
        txtCargoPantSize.Enabled = true;
        txtOverallSize.Enabled = true;
        txtHeight.Enabled = true;
        txtWaist.Enabled = true;
        txtWeight.Enabled = true;
        txtCon1.Enabled = true;
        txtCon2.Enabled = true;
        txtCon3.Enabled = true;
        txtPassport_No.Enabled = true;
        txtPassport_Place.Enabled = true;
        txtPassport_IssueDate.Enabled = true;
        txtPassport_ExpDate.Enabled = true;
        txtSeamanBk_No.Enabled = true;
        txtSeamanBk_Place.Enabled = true;
        txtSeamanBk_IssueDate.Enabled = true;
        txtSeamanBk_ExpDate.Enabled = true;
        rdbUS.Enabled = true;
        txtMobile.Enabled = true;
        txtEmail.Visible = false;
        txtUSExpiry.Enabled = true;
        txtUSIssue.Enabled = true;
        txtUSVisaNumber.Enabled = true;
        txtMMCDOE.Enabled = true;
        txtMMCDOI.Enabled = true;
        txtMMCNO.Enabled = true;
        txtMMCPOI.Enabled = true;
        txtTWICDOE.Enabled = true;
        txtTWICDOI.Enabled = true;
        txtTWICNumber.Enabled = true;
        txtTWICPOI.Enabled = true;
        ddlSchoolGraduated.Enabled = true;
    }
    protected void btnSavePersonal_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdbUS.SelectedValue == "0")
            {
                txtUSExpiry.Text = "";
                txtUSIssue.Text = "";
                txtUSVisaNumber.Text = "";
            }

            if (ddlNaturalization.SelectedValue == "0")
                txtNaturalizationYear.Text = "";

            string updatePanel = "";
            if (Request.QueryString["Mode"] == "EDITCONTACT")
            {
                updatePanel = "CONTACT";
                string con = "";
                if (lblNOKAddress1.Visible == true)
                {
                    if (lblNOKAddress1.Text == "")
                    {
                        con += "Enter Address Line 1\\n";
                    }
                    if (txtCity.Text == "")
                    {
                        con += "Enter City\\n";
                    }
                    if (txtState.Text == "")
                    {
                        con += "Enter State/ Province/ Region\\n";
                    }
                    if (ddlCountry.SelectedIndex == 0)
                    {
                        con += "Select Country\\n";
                    }
                    if (lblZipCode.Text == "")
                    {
                        con += "Enter Zipcode\\n";
                    }

                    if (con != "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "conr", "alert('" + con + "');", true);
                        return;
                    }
                }
                con = "";
                if (txtIAddress.Visible == true)
                {
                    if (txtIAddress.Text == "")
                    {
                        con += "Enter Address \\n";
                    }
                    if (con != "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "conr", "alert('" + con + "');", true);
                        return;
                    }
                }

            }

            else if (Request.QueryString["Mode"] == "EDITGeneral")
            {
                updatePanel = "GENERAL";
                if (txtRemark.Text.Trim().Length > 250)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PassR", "alert('Remark should not be more than 250 characters')", true);
                    return;
                }
                if (ddlSchool.Visible == true)
                {
                    if (ddlSchool.SelectedIndex != 0)
                    {
                        if (ddlSchoolGraduated.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass", "alert('Select Year Graduated')", true);
                            return;
                        }
                    }
                }
                if (ddlVeteran.Visible == true)
                {
                    if (ddlVeteran.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Veteran", "alert('Select Veteran Status')", true);
                        return;
                    }
                }
                if (ddlNaturalization.Visible == true)
                {
                    if (ddlNaturalization.SelectedValue == "1")
                    {
                        if (txtNaturalizationYear.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('Enter Naturalization Date')", true);
                            return;
                        }
                        else
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtNaturalizationYear.Text));
                            }
                            catch
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('Enter valid Naturalization Date" + TodayDateFormat + "')", true);
                                return;
                            }
                        }
                    }
                }
            }
            else if (Request.QueryString["Mode"] == "EDITPassport")
            {
                updatePanel = "PASSPORT";
                string con = "";
                if (!string.IsNullOrEmpty(ViewState["RankApplied"].ToString()))
                {
                    if (obj.Check_Document_Mandatory(int.Parse(ViewState["RankApplied"].ToString()), int.Parse(ViewState["Nationality"].ToString()), "PASSPORT") != 0)
                    {
                        con = "";
                        if (txtPassport_No.Text == "" || txtPassport_Place.Text == "" || txtPassport_IssueDate.Text == "" || txtPassport_ExpDate.Text == "")
                        {
                            con += "Enter Passport Details\\n";
                        }
                        else
                        {
                            if (txtPassport_IssueDate.Text != "")
                            {
                                try
                                {
                                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPassport_IssueDate.Text));
                                    if (dt > DateTime.Now)
                                        con += "Passport Issue Date should not be future date\\n";
                                }
                                catch
                                {
                                    con += "Enter valid Passport Issue Date" + TodayDateFormat + "\\n";
                                }
                            }
                            if (txtPassport_ExpDate.Text != "")
                            {
                                try
                                {
                                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPassport_ExpDate.Text));
                                }
                                catch
                                {
                                    con += "Enter valid Passport Expiry Date" + TodayDateFormat + "\\n";
                                }
                            }
                        }
                        if (con != "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "passports", "alert('" + con + "');", true);
                            return;
                        }
                    }
                    else
                    {
                        con = "";
                        if (txtPassport_No.Text != "")
                        {
                            if (txtPassport_Place.Text == "" || txtPassport_IssueDate.Text == "" || txtPassport_ExpDate.Text == "")
                            {
                                con += "Enter Passport Details\\n";
                            }
                            else
                            {
                                if (txtPassport_IssueDate.Text != "")
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPassport_IssueDate.Text));
                                        if (dt > DateTime.Now)
                                            con += "Passport Issue Date should not be future date\\n";
                                    }
                                    catch
                                    {
                                        con += "Enter valid Passport Issue Date" + TodayDateFormat + "\\n";
                                    }
                                }
                                if (txtPassport_ExpDate.Text != "")
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPassport_ExpDate.Text));
                                    }
                                    catch
                                    {
                                        con += "Enter valid Passport Expiry Date" + TodayDateFormat + "\\n";
                                    }
                                }
                            }
                            if (con != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "passports1", "alert('" + con + "');", true);
                                return;
                            }
                        }
                    }
                    if (obj.Check_Document_Mandatory(int.Parse(ViewState["RankApplied"].ToString()), int.Parse(ViewState["Nationality"].ToString()), "SEAMAN") != 0)
                    {
                        con = "";
                        if (txtSeamanBk_No.Visible == true)
                        {
                            if (txtSeamanBk_No.Text == "" || txtSeamanBk_Place.Text == "" || txtSeamanBk_IssueDate.Text == "" || txtSeamanBk_ExpDate.Text == "")
                            {
                                con += "Enter Seaman Book Details\\n";
                            }
                            else
                            {
                                if (txtSeamanBk_IssueDate.Text != "")
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanBk_IssueDate.Text));
                                        if (dt > DateTime.Now)
                                            con += "Seaman Book Issue Date should not be future date\\n";
                                    }
                                    catch
                                    {
                                        con += "Enter valid Seaman Book Issue Date" + TodayDateFormat + "\\n";
                                    }
                                }
                                if (txtSeamanBk_ExpDate.Text != "")
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanBk_ExpDate.Text));
                                    }
                                    catch
                                    {
                                        con += "Enter valid Seaman Book Expiry Date" + TodayDateFormat + "\\n";
                                    }
                                }
                            }
                            if (con != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "SemanBook", "alert('" + con + "');", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (txtSeamanBk_No.Visible == true)
                        {
                            con = "";
                            if (txtSeamanBk_No.Text != "")
                            {
                                if (txtSeamanBk_Place.Text == "" || txtSeamanBk_IssueDate.Text == "" || txtSeamanBk_ExpDate.Text == "")
                                {
                                    con += "Enter Seaman Book Details\\n";
                                }
                                else
                                {
                                    if (txtSeamanBk_IssueDate.Text != "")
                                    {
                                        try
                                        {
                                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanBk_IssueDate.Text));
                                            if (dt > DateTime.Now)
                                                con += "Seaman Book Issue Date should not be future date\\n";
                                        }
                                        catch
                                        {
                                            con += "Enter valid Seaman Book Issue Date" + TodayDateFormat + "\\n";
                                        }
                                    }
                                    if (txtSeamanBk_ExpDate.Text == "")
                                    {
                                        try
                                        {
                                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanBk_ExpDate.Text));
                                        }
                                        catch
                                        {
                                            con += "Enter valid Seaman Book Expiry Date" + TodayDateFormat + "\\n";
                                        }
                                    }
                                }
                                if (con != "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SemanBook1", "alert('" + con + "');", true);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    con = "";
                    if (txtPassport_No.Text != "")
                    {
                        if (txtPassport_Place.Text == "" || txtPassport_IssueDate.Text == "" || txtPassport_ExpDate.Text == "")
                        {
                            con += "Enter Passport Details\\n";
                        }
                        else
                        {
                            if (txtPassport_IssueDate.Text != "")
                            {
                                try
                                {
                                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPassport_IssueDate.Text));
                                    if (dt > DateTime.Now)
                                        con += "Passport Issue Date should not be future date\\n";
                                }
                                catch
                                {
                                    con += "Enter valid Passport Issue Date" + TodayDateFormat + "\\n";
                                }
                            }
                            if (txtPassport_ExpDate.Text != "")
                            {
                                try
                                {
                                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPassport_ExpDate.Text));
                                }
                                catch
                                {
                                    con += "Enter valid Passport Expiry Date" + TodayDateFormat + "\\n";
                                }
                            }
                        }
                        if (con != "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "passports1", "alert('" + con + "');", true);
                            return;
                        }
                    }
                    if (txtSeamanBk_No.Visible == true)
                    {
                        con = "";
                        if (txtSeamanBk_No.Text != "")
                        {
                            if (txtSeamanBk_Place.Text == "" || txtSeamanBk_IssueDate.Text == "" || txtSeamanBk_ExpDate.Text == "")
                            {
                                con += "Enter Seaman Book Details\\n";
                            }
                            else
                            {
                                if (txtSeamanBk_IssueDate.Text != "")
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanBk_IssueDate.Text));
                                        if (dt > DateTime.Now)
                                            con += "Seaman Book Issue Date should not be future date\\n";
                                    }
                                    catch
                                    {
                                        con += "Enter valid Seaman Book Issue Date" + TodayDateFormat + "\\n";
                                    }
                                }
                                if (txtSeamanBk_ExpDate.Text == "")
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanBk_ExpDate.Text));
                                    }
                                    catch
                                    {
                                        con += "Enter valid Seaman Book Expiry Date" + TodayDateFormat + "\\n";
                                    }
                                }
                            }
                            if (con != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "SemanBook1", "alert('" + con + "');", true);
                                return;
                            }
                        }
                    }
                }

                if (txtMMCNO.Visible == true)
                {
                    if (txtMMCDOI.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtMMCDOI.Text));
                            if (dt > DateTime.Now)
                            {
                                string ss = "MMC Issue date can not be future date";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "MMC1", "alert('" + ss + "');", true);
                                return;
                            }
                        }
                        catch
                        {
                            string ss = "Enter valid MMC Issue Date" + TodayDateFormat + "\\n";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MMC2", "alert('" + ss + "');", true);
                            return;
                        }
                    }

                    if (txtMMCDOE.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtMMCDOE.Text));
                            
                        }
                        catch
                        {
                            string ss = "Enter valid MMC Expiry Date" + TodayDateFormat + "\\n";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "MMC3", "alert('" + ss + "');", true);
                            return;
                        }
                    }

                }
                if (txtTWICNumber.Visible == true)
                {
                    if (txtTWICDOI.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTWICDOI.Text));
                            if (dt > DateTime.Now)
                            {
                                string ss = "TWIC Issue date can not be future date";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "TW1", "alert('" + ss + "');", true);
                                return;
                            }
                        }
                        catch
                        {
                            string ss = "Enter valid TWIC Issue Date" + TodayDateFormat + "\\n";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "TW2", "alert('" + ss + "');", true);
                            return;
                        }
                    }
                    if (txtTWICDOE.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTWICDOE.Text));

                        }
                        catch
                        {
                            string ss = "Enter valid TWIC Expiry Date" + TodayDateFormat + "\\n";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "TW3", "alert('" + ss + "');", true);
                            return;
                        }
                    }
                }
                if (rdbUS.Visible == true)
                {
                    con = "";
                    if (rdbUS.SelectedIndex == 0)
                    {
                        if (txtUSVisaNumber.Text == "")
                        {
                            con += "Enter US Visa Number\\n";
                        }
                        if (txtUSIssue.Text == "")
                        {
                            con += "Enter US Visa Issue Date\\n";
                        }
                        else
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtUSIssue.Text));
                                if (dt > DateTime.Now)
                                    con += "US Visa Issue Date should not be future date\\n";
                            }
                            catch
                            {
                                con += "Enter valid US Visa Issue Date" + TodayDateFormat + "\\n";
                            }
                        }
                        if (txtUSExpiry.Text == "")
                        {
                            con += "Enter US Visa Expiry Date\\n";
                        }
                        else
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtUSExpiry.Text));
                            }
                            catch
                            {
                                con += "Enter valid US Visa Expiry Date" + TodayDateFormat + "\\n";
                            }
                        }
                        if (con != "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "USVisa", "alert('" + con + "');", true);
                            return;
                        }
                    }
                }
            }
            else if (Request.QueryString["Mode"] == "EDITDimension")
            {
                decimal d;
                updatePanel = "DIMENSION";
                if (!string.IsNullOrEmpty(txtHeight.Text))
                {
                    if (!decimal.TryParse(txtHeight.Text, out d))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ht", "alert('Height must be a decimal/integer number')", true);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtWaist.Text))
                {
                    if (!decimal.TryParse(txtWaist.Text, out d))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "wt", "alert('Waist must be a decimal/integer number')", true);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    if (!decimal.TryParse(txtWeight.Text, out d))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "wtt", "alert('Weight must be a decimal/integer number')", true);
                        return;
                    }
                }
                objBLLCrew.UPDATE_CrewUniformSize(GetCrewID(), txtShoeSize.Text, txtTShirtSize.Text, txtCargoPantSize.Text, txtOverallSize.Text, GetSessionUserID());
                objBLLCrew.UPDATE_HeightWaistWeight(GetCrewID(), UDFLib.ConvertToDecimal(txtHeight.Text).ToString(), UDFLib.ConvertToDecimal(txtWaist.Text).ToString(), UDFLib.ConvertToDecimal(txtWeight.Text).ToString(), GetSessionUserID());
            }
            else if (Request.QueryString["Mode"] == "EDITConf")
            {
                updatePanel = "CUSTOM";
            }


            if (updatePanel == "PASSPORT")
            {

                if (txtMMCDOI.Text != "")
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtMMCDOI.Text));
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass1", "alert('Enter valid MMC Issue Date" + TodayDateFormat + "')", true);
                        return;
                    }
                }
                if (txtMMCDOE.Text != "")
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtMMCDOE.Text));
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass2", "alert('Enter valid MMC Expiry Date" + TodayDateFormat + "')", true);
                        return;
                    }
                }
                if (txtTWICDOI.Text != "")
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTWICDOI.Text));

                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass3", "alert('Enter valid TWIC Issue Date" + TodayDateFormat + "')", true);
                        return;
                    }
                }
                if (txtTWICDOE.Text != "")
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTWICDOE.Text));
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass4", "alert('Enter valid TWIC Expiry Date" + TodayDateFormat + "')", true);
                        return;
                    }
                }
                if (txtPassport_IssueDate.Visible == true && txtPassport_ExpDate.Visible == true && txtPassport_IssueDate.Text != "" && txtPassport_ExpDate.Text != "")
                {
                    if (UDFLib.ConvertToDate(txtPassport_IssueDate.Text) >= UDFLib.ConvertToDate(txtPassport_ExpDate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass5", "alert('Passport Issue Date must be less than Expiry date')", true);
                        return;
                    }
                }
                if (txtSeamanBk_IssueDate.Visible == true && txtSeamanBk_ExpDate.Visible == true && txtSeamanBk_IssueDate.Text != "" && txtSeamanBk_ExpDate.Text != "")
                {
                    if (UDFLib.ConvertToDate(txtSeamanBk_IssueDate.Text) >= UDFLib.ConvertToDate(txtSeamanBk_ExpDate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass6", "alert('Seaman Issue Date must be less than Expiry date')", true);
                        return;
                    }
                }
                if (txtMMCDOI.Visible == true && txtMMCDOE.Visible == true && txtMMCDOI.Text != "" && txtMMCDOE.Text != "")
                {
                    if (UDFLib.ConvertToDate(txtMMCDOI.Text) >= UDFLib.ConvertToDate(txtMMCDOE.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass7", "alert('MMC Issue Date must be less than Expiry date')", true);
                        return;
                    }
                }
                if (txtTWICDOI.Visible == true && txtTWICDOE.Visible == true && txtTWICDOI.Text != "" && txtTWICDOE.Text != "")
                {
                    if (UDFLib.ConvertToDate(txtTWICDOI.Text) >= UDFLib.ConvertToDate(txtTWICDOE.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass8", "alert('TWIC Issue Date must be less than Expiry date')", true);
                        return;
                    }
                }
                if (Convert.ToInt32(ViewState["PassportDocID"]) <= 0)
                {
                    if (!string.IsNullOrEmpty(txtPassport_No.Text.Trim()))
                    {
                        if (Convert.ToInt32(ViewState["PassportDocTypeID"]) != 0)
                            objBLLCrew.INS_CrewDocuments(GetCrewID(), "", "", "", Convert.ToInt32(ViewState["PassportDocTypeID"]), GetSessionUserID(), txtPassport_No.Text.Trim(), txtPassport_IssueDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtPassport_IssueDate.Text).ToString(), txtPassport_Place.Text.Trim(), txtPassport_ExpDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtPassport_ExpDate.Text).ToString(), UDFLib.ConvertToInteger(drpPassportCountry.SelectedValue));
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass9", "alert('Passport document type missing')", true);
                            return;
                        }
                    }
                }
                else
                    objBLLCrew.UPDATE_CrewDocument(GetCrewID(), Convert.ToInt32(ViewState["PassportDocID"]), Convert.ToInt32(ViewState["PassportDocTypeID"]), "", txtPassport_No.Text.Trim(), txtPassport_IssueDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtPassport_IssueDate.Text).ToString(), txtPassport_Place.Text.Trim(), txtPassport_ExpDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtPassport_ExpDate.Text).ToString(), GetSessionUserID(), Convert.ToInt32(drpPassportCountry.SelectedValue));
                if (Convert.ToInt32(ViewState["SeamanDocID"]) <= 0)
                {
                    if (!string.IsNullOrEmpty(txtSeamanBk_No.Text.Trim()))
                    {
                        if (Convert.ToInt32(ViewState["SeamanDocTypeID"]) != 0)
                            objBLLCrew.INS_CrewDocuments(GetCrewID(), "", "", "", Convert.ToInt32(ViewState["SeamanDocTypeID"]), GetSessionUserID(), txtSeamanBk_No.Text.Trim(), txtSeamanBk_IssueDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtSeamanBk_IssueDate.Text).ToString(), txtSeamanBk_Place.Text.Trim(), txtSeamanBk_ExpDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtSeamanBk_ExpDate.Text).ToString(), UDFLib.ConvertToInteger(drpSeamanCountry.SelectedValue));
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pass10", "alert('Seaman document type missing')", true);
                            return;
                        }
                    }
                }
                else
                    objBLLCrew.UPDATE_CrewDocument(GetCrewID(), Convert.ToInt32(ViewState["SeamanDocID"]), Convert.ToInt32(ViewState["SeamanDocTypeID"]), "", txtSeamanBk_No.Text.Trim(), txtSeamanBk_IssueDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtSeamanBk_IssueDate.Text).ToString(), txtSeamanBk_Place.Text.Trim(), txtSeamanBk_ExpDate.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtSeamanBk_ExpDate.Text).ToString(), GetSessionUserID(), Convert.ToInt32(drpSeamanCountry.SelectedValue));


                obj.UPD_CrewDetails_Personal(GetCrewID(), updatePanel, lblNOKAddress1.Text.Trim(), lblNOKAddress2.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtFax.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), lblZipCode.Text.Trim(), txtIAddress.Text.Trim(), txtPD_Airport.Text.Trim(), txtPD_Airport.Text.Trim() == "" ? 0 : UDFLib.ConvertIntegerToNull(txtPD_Airport.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVeteran.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSchool.SelectedValue), ddlSchoolGraduated.SelectedValue.ToString(), UDFLib.ConvertIntegerToNull(ddlNaturalization.SelectedValue), ddlEnglish.SelectedValue == "0" ? "" : ddlEnglish.SelectedValue,
                 ConvertDate(txtNaturalizationYear.Text.Trim()), txtRemark.Text.Trim(), txtCon1.Text.Trim(), txtCon2.Text.Trim(), txtCon3.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), rdbUS.SelectedValue, ConvertDate(txtUSExpiry.Text.Trim()), txtUSVisaNumber.Text.Trim(), ConvertDate(txtUSIssue.Text.Trim()));

                DataTable dtMMC = obj.CRW_GetCDConfiguration("MMC").Tables[0];
                DataTable dtTWIC = obj.CRW_GetCDConfiguration("TWIC").Tables[0];
                if (Convert.ToInt32(ViewState["MMCDocTypeID"]) == 0 && txtMMCNO.Text.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MMCD", "alert('MMC Number document type missing')", true);
                    return;
                }

                if (Convert.ToInt32(ViewState["TWICDocTypeID"]) == 0 && txtTWICNumber.Text.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "TWICCD", "alert('TWIC Number document type missing')", true);
                    return;
                }

                if (Convert.ToInt32(ViewState["MMCDocTypeID"]) == 0 && Convert.ToInt32(ViewState["TWICDocTypeID"]) == 0 && txtMMCNO.Text.Trim() != "" && txtTWICNumber.Text.Trim() != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "TWICCD", "alert('MMC Number and TWIC Number document type missing')", true);
                    return;
                }
                if (dtMMC.Rows.Count > 0)
                {
                    if (dtMMC.Rows[0]["Display"].ToString() == "True" && dtMMC.Rows[0]["Confidential"].ToString() == "False")
                    {
                        if (Convert.ToInt32(ViewState["MMCDocID"]) <= 0)
                            objBLLCrew.INS_CrewDocuments(GetCrewID(), "", "", "", Convert.ToInt32(ViewState["MMCDocTypeID"]), GetSessionUserID(), txtMMCNO.Text.Trim(), txtMMCDOI.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtMMCDOI.Text).ToString(), txtMMCPOI.Text.Trim(), txtMMCDOE.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtMMCDOE.Text).ToString(), UDFLib.ConvertToInteger(drpMMCCountry.SelectedValue));
                        else
                            objBLLCrew.UPDATE_CrewDocument(GetCrewID(), Convert.ToInt32(ViewState["MMCDocID"]), Convert.ToInt32(ViewState["MMCDocTypeID"]), "", txtMMCNO.Text.Trim(), txtMMCDOI.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtMMCDOI.Text.Trim()).ToString(), txtMMCPOI.Text.Trim(), txtMMCDOE.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtMMCDOE.Text.Trim()).ToString(), GetSessionUserID(), Convert.ToInt32(drpMMCCountry.SelectedValue));

                    }
                    if (dtTWIC.Rows.Count > 0)
                    {
                        if (dtTWIC.Rows[0]["Display"].ToString() == "True" && dtTWIC.Rows[0]["Confidential"].ToString() == "False")
                        {
                            if (Convert.ToInt32(ViewState["TWICDocID"]) <= 0)
                                objBLLCrew.INS_CrewDocuments(GetCrewID(), "", "", "", Convert.ToInt32(ViewState["TWICDocTypeID"]), GetSessionUserID(), txtTWICNumber.Text.Trim(), txtTWICDOI.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtTWICDOI.Text).ToString(), txtTWICPOI.Text.Trim(), txtTWICDOE.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtTWICDOE.Text).ToString(), UDFLib.ConvertToInteger(drpTWICCountry.SelectedValue));
                            else
                                objBLLCrew.UPDATE_CrewDocument(GetCrewID(), Convert.ToInt32(ViewState["TWICDocID"]), Convert.ToInt32(ViewState["TWICDocTypeID"]), "", txtTWICNumber.Text.Trim(), txtTWICDOI.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtTWICDOI.Text.Trim()).ToString(), txtTWICPOI.Text.Trim(), txtTWICDOE.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtTWICDOE.Text.Trim()).ToString(), GetSessionUserID(), Convert.ToInt32(drpTWICCountry.SelectedValue));
                        }
                    }
                }



            }
            else if (updatePanel != "DIMENSION")
            {
                DataTable dtAddress = obj.CRW_GetCDConfiguration("Addressformat").Tables[0];
                if (dtAddress.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(txtPD_Airport.SelectedText.Trim()))
                    {
                        DataTable dt = objBLLCrew.Get_AirportList(0, "", "", "");
                        DataRow[] rows = dt.Select("AirportName='" + txtPD_Airport.SelectedText + "'");
                        if (rows.Length == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Air", "alert('Invalid Nearest Airport')", true);
                            return;
                        }
                        if (ViewState["Airport"] == txtPD_Airport.SelectedText.Trim())
                        {
                            txtPD_Airport.SelectedValue = ViewState["AirportID"].ToString();
                        }

                        if (UDFLib.ConvertIntegerToNull(txtPD_Airport.SelectedValue) != 0 && txtPD_Airport.SelectedValue != "")
                        {
                            //International Address
                            if (dtAddress.Rows[0]["Value"].ToString() == "True")
                            {
                                obj.UPD_CrewDetails_Personal(GetCrewID(), updatePanel, lblNOKAddress1.Text.Trim(), lblNOKAddress2.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtIFax.Text.Trim(), 0, lblZipCode.Text.Trim(), txtIAddress.Text.Trim(), txtPD_Airport.SelectedText.Trim(), txtPD_Airport.Text.Trim() == "" ? 0 : UDFLib.ConvertIntegerToNull(txtPD_Airport.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVeteran.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSchool.SelectedValue), ddlSchoolGraduated.SelectedValue.ToString(), UDFLib.ConvertIntegerToNull(ddlNaturalization.SelectedValue), ddlEnglish.SelectedValue == "0" ? "" : ddlEnglish.SelectedValue,
                                   ConvertDate(txtNaturalizationYear.Text.Trim()), txtRemark.Text.Trim(), txtCon1.Text.Trim(), txtCon2.Text.Trim(), txtCon3.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), rdbUS.SelectedIndex.ToString(), ConvertDate(txtUSExpiry.Text.Trim()), txtUSVisaNumber.Text.Trim(), ConvertDate(txtUSIssue.Text.Trim()));
                            }
                            else
                            {
                                obj.UPD_CrewDetails_Personal(GetCrewID(), updatePanel, lblNOKAddress1.Text.Trim(), lblNOKAddress2.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtFax.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), lblZipCode.Text.Trim(), txtIAddress.Text.Trim(), txtPD_Airport.SelectedText.Trim(), txtPD_Airport.Text.Trim() == "" ? 0 : UDFLib.ConvertIntegerToNull(txtPD_Airport.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVeteran.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSchool.SelectedValue), ddlSchoolGraduated.SelectedValue.ToString(), UDFLib.ConvertIntegerToNull(ddlNaturalization.SelectedValue), ddlEnglish.SelectedValue == "0" ? "" : ddlEnglish.SelectedValue,
                         ConvertDate(txtNaturalizationYear.Text.Trim()), txtRemark.Text.Trim(), txtCon1.Text.Trim(), txtCon2.Text.Trim(), txtCon3.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), rdbUS.SelectedIndex.ToString(), ConvertDate(txtUSExpiry.Text.Trim()), txtUSVisaNumber.Text.Trim(), ConvertDate(txtUSIssue.Text.Trim()));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Air", "alert('Invalid Nearest Airport')", true);
                            return;
                        }
                    }
                    else
                    {
                        if (dtAddress.Rows[0]["Value"].ToString() == "True")
                        {
                            obj.UPD_CrewDetails_Personal(GetCrewID(), updatePanel, lblNOKAddress1.Text.Trim(), lblNOKAddress2.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtIFax.Text.Trim(), 0, lblZipCode.Text.Trim(), txtIAddress.Text.Trim(), txtPD_Airport.SelectedText.Trim(), txtPD_Airport.Text.Trim() == "" ? 0 : UDFLib.ConvertIntegerToNull(txtPD_Airport.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVeteran.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSchool.SelectedValue), ddlSchoolGraduated.SelectedValue.ToString(), UDFLib.ConvertIntegerToNull(ddlNaturalization.SelectedValue), ddlEnglish.SelectedValue == "0" ? "" : ddlEnglish.SelectedValue,
                                ConvertDate(txtNaturalizationYear.Text.Trim()), txtRemark.Text.Trim(), txtCon1.Text.Trim(), txtCon2.Text.Trim(), txtCon3.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), rdbUS.SelectedIndex.ToString(), ConvertDate(txtUSExpiry.Text.Trim()), txtUSVisaNumber.Text.Trim(), ConvertDate(txtUSIssue.Text.Trim()));
                        }
                        else
                        {
                            obj.UPD_CrewDetails_Personal(GetCrewID(), updatePanel, lblNOKAddress1.Text.Trim(), lblNOKAddress2.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtFax.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), lblZipCode.Text.Trim(), txtIAddress.Text.Trim(), txtPD_Airport.SelectedText.Trim(), txtPD_Airport.Text.Trim() == "" ? 0 : UDFLib.ConvertIntegerToNull(txtPD_Airport.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVeteran.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSchool.SelectedValue), ddlSchoolGraduated.SelectedValue.ToString(), UDFLib.ConvertIntegerToNull(ddlNaturalization.SelectedValue), ddlEnglish.SelectedValue == "0" ? "" : ddlEnglish.SelectedValue,
                     ConvertDate(txtNaturalizationYear.Text.Trim()), txtRemark.Text.Trim(), txtCon1.Text.Trim(), txtCon2.Text.Trim(), txtCon3.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), rdbUS.SelectedIndex.ToString(), ConvertDate(txtUSExpiry.Text.Trim()), txtUSVisaNumber.Text.Trim(), ConvertDate(txtUSIssue.Text.Trim()));
                        }
                    }
                }
            }

            string js = "parent.GetPersonalDetails(" + Request.QueryString["ID"].ToString() + ");parent.hideModal('dvPopupFrame');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void btnClosePersonal_Click(object sender, EventArgs e)
    {
        string js = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }

    protected void CustomFieldValidation()
    {
        DataSet ds1 = obj.CRW_GetCDConfiguration("CF1");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lblCon1.Text = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
            lblCon4.Text = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
        }

        DataSet ds2 = obj.CRW_GetCDConfiguration("CF2");
        if (ds2.Tables[0].Rows.Count > 0)
        {
            lblCon2.Text = ds2.Tables[0].Rows[0]["DisplayName"].ToString();
            lblCon5.Text = ds2.Tables[0].Rows[0]["DisplayName"].ToString();
        }

        DataSet ds3 = obj.CRW_GetCDConfiguration("CF3");
        if (ds3.Tables[0].Rows.Count > 0)
        {
            lblCon3.Text = ds3.Tables[0].Rows[0]["DisplayName"].ToString();
            lblCon6.Text = ds3.Tables[0].Rows[0]["DisplayName"].ToString();
        }
    }
    protected void ddlNaturalization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNaturalization.SelectedValue == "1")
        {
            txtNaturalizationYear.Enabled = true;
            txtNaturalizationYear.CssClass = "required";
            lblN.Visible = true;
        }
        else
        {
            txtNaturalizationYear.Enabled = false;
            txtNaturalizationYear.Text = "";
            txtNaturalizationYear.CssClass = "";
            lblN.Visible = false;
        }
    }

    protected void Load_PassportAndSeamanDetails(int CrewID)
    {
        DataSet dsDefault = objBLLCrew.CRW_GET_DefaultValuesCrewAddEdit(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        DataSet dsCrewDetails = objBLLCrew.CRW_LIB_CD_GetCrewDetails(CrewID);
        if (dsDefault != null)
        {
            DataTable dtDocument = dsDefault.Tables[9];
            if (dtDocument.Rows.Count > 0)
            {
                dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='Passport'";
                if (dsDefault.Tables[9].DefaultView.Count > 0)
                {
                    ViewState["PassportDocTypeID"] = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + UDFLib.ConvertIntegerToNull(ViewState["PassportDocTypeID"]);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        ViewState["PassportDocID"] = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                    }
                }

                dsDefault.Tables[9].DefaultView.RowFilter = "";
                dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='Seaman'";
                if (dsDefault.Tables[9].DefaultView.Count > 0)
                {
                    ViewState["SeamanDocTypeID"] = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + UDFLib.ConvertIntegerToNull(ViewState["SeamanDocTypeID"]);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        ViewState["SeamanDocID"] = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                    }
                }

                dsDefault.Tables[9].DefaultView.RowFilter = "";
                dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='MMCNumber'";
                if (dsDefault.Tables[9].DefaultView.Count > 0)
                {
                    ViewState["MMCDocTypeID"] = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + UDFLib.ConvertIntegerToNull(ViewState["MMCDocTypeID"]);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        ViewState["MMCDocID"] = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                    }
                }

                dsDefault.Tables[9].DefaultView.RowFilter = "";
                dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='TWICNumber'";
                if (dsDefault.Tables[9].DefaultView.Count > 0)
                {
                    ViewState["TWICDocTypeID"] = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + UDFLib.ConvertIntegerToNull(ViewState["TWICDocTypeID"]);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        ViewState["TWICDocID"] = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                    }
                }
            }
        }

        DataTable dt = objBLLCrew.Get_CrewPassportAndSeamanDetails(CrewID);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtPassport_No.Text = dt.Rows[0]["Passport_Number"].ToString();
            lblPassportNo.Text = txtPassport_No.Text;
            txtPassport_Place.Text = dt.Rows[0]["Passport_PlaceOf_Issue"].ToString();
            lblPassportPOI.Text = txtPassport_Place.Text;

            if (dt.Rows[0]["Passport_Issue_Date"].ToString() != "")
            {
                txtPassport_IssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Passport_Issue_Date"].ToString()));
            }
            else
            {
                txtPassport_IssueDate.Text = "";
            }
            if (dt.Rows[0]["Passport_Issue_Date"].ToString() == "01/01/1900")
            {
                txtPassport_IssueDate.Text = "";
            }
            lblPassportIssue.Text = txtPassport_IssueDate.Text;

            if (dt.Rows[0]["Passport_Expiry_Date"].ToString() != "")
            {
                txtPassport_ExpDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Passport_Expiry_Date"].ToString()));
            }
            else
            {
                txtPassport_ExpDate.Text = "";
            }
            if (dt.Rows[0]["Passport_Expiry_Date"].ToString() == "01/01/1900")
            {
                txtPassport_ExpDate.Text = "";
            }
            lblPassportExpiry.Text = txtPassport_ExpDate.Text;

            if (dt.Rows[0]["Passport_CountryId"].ToString() != "")
            {
                drpPassportCountry.ClearSelection();
                drpPassportCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["Passport_CountryId"])).Selected = true;
                if (drpPassportCountry.SelectedIndex > 0)
                    lblPassportCountry.Text = drpPassportCountry.SelectedItem.Text;
                else
                    lblPassportCountry.Text = "";
            }

            //--Seaman--//

            txtSeamanBk_No.Text = dt.Rows[0]["Seaman_Book_Number"].ToString();
            txtSeamanBk_Place.Text = dt.Rows[0]["Seaman_Book_PlaceOf_Issue"].ToString();
            lblSeaman.Text = txtSeamanBk_No.Text;
            lblSeamanPOI.Text = txtSeamanBk_Place.Text;
            if (dt.Rows[0]["Seaman_Book_Issue_Date"].ToString() != "")
            {
                txtSeamanBk_IssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Seaman_Book_Issue_Date"].ToString()));
            }
            else
            {
                txtSeamanBk_IssueDate.Text = "";
            }
            if (dt.Rows[0]["Seaman_Book_Issue_Date"].ToString() == "01/01/1900")
            {
                txtSeamanBk_IssueDate.Text = "";
            }
            lblSeamanIssue.Text = txtSeamanBk_IssueDate.Text;
            if (dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString() != "")
            {
                txtSeamanBk_ExpDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()));

            }
            else
            {
                txtSeamanBk_ExpDate.Text = "";
            }
            if (dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString() == "01/01/1900")
            {
                txtSeamanBk_ExpDate.Text = "";
            }
            lblSemanExpiry.Text = txtSeamanBk_ExpDate.Text;
            if (dt.Rows[0]["Seaman_CountryId"].ToString() != "")
            {
                drpSeamanCountry.ClearSelection();
                drpSeamanCountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["Seaman_CountryId"])).Selected = true;
                if (drpSeamanCountry.SelectedIndex != 0)
                    lblSeamanCountry.Text = drpSeamanCountry.SelectedItem.Text;
                else
                    lblSeamanCountry.Text = "";
            }
        }

    }
    protected void rdbUS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbUS.SelectedIndex == 0)
        {
            txtUSExpiry.Enabled = true;
            txtUSVisaNumber.Enabled = true;
            txtUSIssue.Enabled = true;
            txtUSExpiry.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
            txtUSVisaNumber.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
            txtUSIssue.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
            lblU3.Visible = true;
            lblU2.Visible = true;
            lblU1.Visible = true;
            txtUSVisaNumber.Text = lblUsVisaNumber.Text;
            txtUSIssue.Text = lblUSVisaIssue.Text;
            txtUSExpiry.Text = lblUsVisaExpiry.Text;
        }
        else
        {
            txtUSExpiry.Enabled = false;
            txtUSVisaNumber.Enabled = false;
            txtUSIssue.Enabled = false;
            txtUSExpiry.Text = "";
            txtUSVisaNumber.Text = "";
            txtUSIssue.Text = "";
            txtUSExpiry.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4");
            txtUSVisaNumber.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4");
            txtUSIssue.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4");
            lblU3.Visible = false;
            lblU2.Visible = false;
            lblU1.Visible = false;
        }
    }

    protected void Load_CrewUniformSize(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewUniformSize(CrewID);

        if (dt.Rows.Count > 0)
        {


            txtShoeSize.Text = dt.Rows[0]["SHOE_SIZE"].ToString();
            txtTShirtSize.Text = dt.Rows[0]["TSHIRT_SIZE"].ToString();
            txtCargoPantSize.Text = dt.Rows[0]["PANT_SIZE"].ToString();
            txtOverallSize.Text = dt.Rows[0]["OVERALL_SIZE"].ToString();
            lblShoe.Text = txtShoeSize.Text;
            lblTShirt.Text = txtTShirtSize.Text;
            lblCargoPant.Text = txtCargoPantSize.Text;
            lblOverall.Text = txtOverallSize.Text;
        }
    }
    protected void Load_CrewHeightWaiseWeight(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewHeightWaistWeight(CrewID);

        if (dt.Rows.Count > 0)
        {

            txtHeight.Text = dt.Rows[0]["height"].ToString();
            txtWaist.Text = dt.Rows[0]["waist"].ToString();
            txtWeight.Text = dt.Rows[0]["weight"].ToString();
            if (UDFLib.ConvertToDecimal(txtHeight.Text).ToString() == "0.00")
            {
                lblHeight.Text = "";
                txtHeight.Text = "";
            }
            else
                lblHeight.Text = txtHeight.Text;
            if (UDFLib.ConvertToDecimal(txtWaist.Text).ToString() == "0.00")
            {
                lblWaist.Text = "";
                txtWaist.Text = "";
            }
            else
                lblWaist.Text = txtWaist.Text;
            if (UDFLib.ConvertToDecimal(txtWeight.Text).ToString() == "0.00")
            {
                txtWeight.Text = "";
                lblWeight.Text = "";
            }
            else
                lblWeight.Text = txtWeight.Text;
        }
    }

    protected void ConfidentialityCheck()
    {
        string str = "";
        DataRow[] dr;
        DataTable dt = obj.CRW_GetCDConfiguration(null).Tables[0];
        if (dt.Rows.Count > 0)
        {
            str = "Veteran";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    tdVeteran1.Visible = true;
                    tdVeteran2.Visible = true;
                    tdVeteran3.Visible = true;
                    tdVeteran4.Visible = true;
                }
                else
                {
                    tdVeteran1.Visible = false;
                    tdVeteran2.Visible = false;
                    tdVeteran3.Visible = false;
                    tdVeteran4.Visible = false;
                }
            }

            str = "School";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    tdSchool1.Visible = true;
                    tdSchool4.Visible = true;
                    tdSchool5.Visible = true;
                    tdSchool6.Visible = true;
                }
                else
                {
                    tdSchool1.Visible = false;
                    tdSchool4.Visible = false;
                    tdSchool5.Visible = false;
                    tdSchool6.Visible = false;
                }
            }

            str = "USVisa";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trUSVisa.Visible = true;
                    trUSVisa1.Visible = true;
                }
                else
                {
                    trUSVisa.Visible = false;
                    trUSVisa1.Visible = false;
                }
            }

            str = "Seaman";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trSeaman.Visible = true;
                    trSeaman1.Visible = true;
                }
                else
                {
                    trSeaman.Visible = false;
                    trSeaman1.Visible = false;
                }
            }

            str = "BodyDimensions";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trHeight.Visible = true;
                    trHeightLbl.Visible = true;
                }
                else
                {
                    trHeight.Visible = false;
                    trHeightLbl.Visible = false;
                }
            }

            str = "Uniform";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trUniform.Visible = true;
                    trUniformlbl.Visible = true;
                }
                else
                {
                    trUniform.Visible = false;
                    trUniformlbl.Visible = false;
                }
            }

            if (trHeightLbl.Visible == false && trUniformlbl.Visible == false)
                lnkEditDimension.Visible = false;


            str = "Naturalization";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    tdN1.Visible = true;
                    tdN5.Visible = true;
                }
                else
                {
                    tdN1.Visible = false;
                    tdN5.Visible = false;
                }
            }
            str = "CF1";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trCon1.Visible = true;
                    trCon4.Visible = true;
                }
                else
                {
                    trCon1.Visible = false;
                    trCon4.Visible = false;
                }
            }
            str = "CF2";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trCon2.Visible = true;
                    trCon5.Visible = true;
                }
                else
                {
                    trCon2.Visible = false;
                    trCon5.Visible = false;
                }
            }
            str = "CF3";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trCon3.Visible = true;
                    trCon6.Visible = true;
                }
                else
                {
                    trCon3.Visible = false;
                    trCon6.Visible = false;
                }
            }

            if (trCon4.Visible == false && trCon5.Visible == false && trCon6.Visible == false)
                lnkEditConfig.Visible = false;

            str = "TWIC";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trTWIC.Visible = true;
                    trTWIC1.Visible = true;
                }
                else
                {
                    trTWIC.Visible = false;
                    trTWIC1.Visible = false;
                }
            }
            str = "MMC";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                {
                    trMMC.Visible = true;
                    trMMC1.Visible = true;
                }
                else
                {
                    trMMC.Visible = false;
                    trMMC1.Visible = false;
                }
            }
            //if (trCon1.Visible == false && trCon2.Visible == false && trCon3.Visible == false)
            //{
            //    pnl_Config.Visible = false;
            //}

        }
    }


    private int ConvertMonthToDigit(string month)
    {
        int retVal = 0;
        switch (month.ToLower())
        {
            case "jan":
                retVal = 01;
                break;
            case "feb":
                retVal = 02;
                break;
            case "mar":
                retVal = 03;
                break;
            case "apr":
                retVal = 04;
                break;
            case "may":
                retVal = 05;
                break;
            case "jun":
                retVal = 06;
                break;
            case "jul":
                retVal = 07;
                break;
            case "aug":
                retVal = 08;
                break;
            case "sep":
                retVal = 09;
                break;
            case "oct":
                retVal = 10;
                break;
            case "nov":
                retVal = 11;
                break;
            case "dec":
                retVal = 12;
                break;
        }
        return retVal;
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex == 0)
        {
            ddlSchoolGraduated.SelectedIndex = 0;
            ddlSchoolGraduated.Enabled = false;
            lblS.Visible = false;
            ddlSchoolGraduated.BackColor = System.Drawing.Color.Transparent;
        }
        else
        {
            ddlSchoolGraduated.Enabled = true;
            ddlSchoolGraduated.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
            lblS.Visible = true;
        }

    }
    public DateTime? ConvertDate(string date)
    {
        if (date == "" || date == null)
        {
            return null;
        }
        else
        {
            return UDFLib.ConvertToDate(date);
        }
    }
}