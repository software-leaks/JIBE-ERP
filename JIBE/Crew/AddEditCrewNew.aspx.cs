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
using System.Web.Script.Services;
using System.Web.Services;
using System.Text;
using System.IO;

public partial class Crew_AddEditCrewNew : System.Web.UI.Page
{
    public int CrewID = 0;
    public string DateFormat = "";
    BLL_Crew_Admin objAdmin = new BLL_Crew_Admin();
    int UserCompanyID = 0;
    public string HOST = "";
    public string JSONCountry = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            UserAccessValidation();
            DateFormat = UDFLib.GetDateFormat();//Get User date format

            if (Request.QueryString["crewid"] != null && Request.QueryString["crewid"] != "")
            {
                CrewID = Convert.ToInt32(Request.QueryString["crewid"]);
                HiddenField_CrewID.Value = CrewID.ToString();
            }

            if (Session["USERCOMPANY"] != null)
                hdnClientName.Value = Convert.ToString(Session["USERCOMPANY"]);

            if (HiddenField_CrewID.Value != "")
                CrewID = Convert.ToInt32(HiddenField_CrewID.Value);

            if (CrewID == 0)
                HiddenField_CrewID.Value = "0";

            HOST = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/crew/addeditcrewnew"));
            if (!Page.IsPostBack)
            {
                UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
                hdnLoggedInUserId.Value = GetSessionUserID().ToString();
                #region Date format
                txtHireDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.ToShortDateString());
                CalendarExtenderDOB.Format = CalendarExtenderAvailDate.Format = CalendarExtenderHireDate.Format = DateFormat;
                CEMMCExpiryDate.Format = CEMMCIssueDate.Format = CENaturaliztionDate.Format = CEPassportExpiryDate.Format = CEPassportIssueDate.Format = DateFormat;
                CESeamanExpiryDate.Format = CESeamanExpiryDate.Format = CESeamanIssueDate.Format = CETWICExpiryDate.Format = CETWICIssueDate.Format = DateFormat;
                CEDependentDOB.Format = CENOKDOB.Format = DateFormat;
                CEUSVisaIssueDate.Format = CEUSVisaExpiryDate.Format = DateFormat;

                CEDependentDOB.EndDate = CENOKDOB.EndDate = CalendarExtenderDOB.EndDate = DateTime.Now;
                //CalendarExtenderHireDate.StartDate = CalendarExtenderAvailDate.StartDate = DateTime.Now;
                CEUSVisaIssueDate.EndDate = CETWICIssueDate.EndDate = CEMMCIssueDate.EndDate = CESeamanIssueDate.EndDate = CEPassportIssueDate.EndDate = DateTime.Now;

                #endregion
                #region US/Internation Address
                DataTable dtAddress = objAdmin.CRW_GetCDConfiguration("Addressformat").Tables[0];
                if (dtAddress.Rows[0]["Value"].ToString() == "True")
                    hdnAddressFromat.Value = "1";///International
                else
                    hdnAddressFromat.Value = "0";///US
                if (hdnAddressFromat.Value == "0")
                {
                    trUSAddress.Visible = trUSCityState.Visible = trUSCountryZip.Visible = true;
                    trNOKUSAddress.Visible = trNOKUSCityState.Visible = trNOKUSCountryZip.Visible = true;
                    trDependentUSAddress.Visible = trDependentUSCityState.Visible = trDependentUSCountryZip.Visible = true;
                }
                else
                {
                    trInternational.Visible = true;
                    trNOKInternational.Visible = true;
                    trDependentInternational.Visible = true;

                }
                #endregion

                BindDefaults();
                btnNextScreen1.Visible = false;
                if (Request.QueryString.Count > 0)
                {
                    if (Convert.ToInt32(Request.QueryString["crewid"]) > 0)
                    {
                        BindCrewDetails();
                        BtnCreateNewCrew.Text = "Save and Continue";
                    }
                    else
                    {
                        if (File.Exists(Server.MapPath("~/Uploads/CrewImages/" + Convert.ToString(Session["ADDEDITNEWFILENAME"]))))
                        {
                            imgCrewPicScreen2.ImageUrl = imgCrewPic.ImageUrl = "~/Uploads/CrewImages/" + Convert.ToString(Session["ADDEDITNEWFILENAME"]);
                            imgCrewPicScreen2.Visible = imgCrewPic.Visible = true;
                            imgNoPicScreen2.Visible = imgNoPic.Visible = false;
                            hdnCrewPhotoFileName.Value = Convert.ToString(Session["ADDEDITNEWFILENAME"]);
                        }
                        else
                        {
                            imgCrewPicScreen2.Visible = imgCrewPic.Visible = false;
                            imgNoPicScreen2.Visible = imgNoPic.Visible = true;
                        }
                    }
                }
            }
            else
            {
                if (Session["ADDEDITNEWFILENAME"] != null)
                {
                    imgCrewPic.ImageUrl = "~/Uploads/CrewImages/" + Convert.ToString(Session["ADDEDITNEWFILENAME"]);
                    imgCrewPicScreen2.Attributes.Add("style", "display:block");
                    imgCrewPic.Attributes.Add("style", "display:block");

                    imgNoPicScreen2.Attributes.Add("style", "display:none");
                    imgNoPic.Attributes.Add("style", "display:none");
                    Session["ADDEDITNEWFILENAME"] = null;
                }
                if (hdnUnionBranchId.Value != "0" || hdnUnionBranchId.Value != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "BindUnionBranch", "BindUnionBranch('" + Convert.ToInt32(hdnUnionBranchId.Value) + "');", true);
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
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();
        objUA = objUser.Get_UserAccessForPage(GetSessionUserID(), PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 1 || objUA.Edit == 1)
            BtnCreateNewCrew.Enabled = true;
        else
            BtnCreateNewCrew.Enabled = false;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// To bind the default value like country and all library 
    /// </summary>
    private void BindDefaults()
    {
        try
        {
            BLL_Crew_CrewDetails objCrewDetails = new BLL_Crew_CrewDetails();
            DataSet dsDefault = objCrewDetails.CRW_GET_DefaultValuesCrewAddEdit(UserCompanyID);
            if (dsDefault != null)
            {
                //Bind Country
                if (dsDefault.Tables.Count > 0)
                {
                    BindDropDown(drpUSCountry, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(ddlPD_Nationality, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(drpCountryPassport, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(drpCountrySeaman, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(drpCountryMMC, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(drpCountryTWIC, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(drpNOKUSCountry, dsDefault.Tables[0], "Country_Name");
                    BindDropDown(drpDependentUSCountry, dsDefault.Tables[0], "Country_Name");

                    dsDefault.Tables[0].DefaultView.RowFilter = "";
                    dsDefault.Tables[0].DefaultView.RowFilter = "ISO_Code='US'";
                    if (dsDefault.Tables[0].DefaultView.Count > 0)
                    {
                        hdnUSCountryID.Value = Convert.ToString(dsDefault.Tables[0].DefaultView[0]["ID"]);
                        if (hdnUSCountryID.Value != "")
                            drpUSCountry.SelectedValue = hdnUSCountryID.Value;
                    }

                    ///Bind Ranks
                    BindDropDown(drpAppliedRank, dsDefault.Tables[1], "Rank_Short_Name");

                    ///Bind union
                    BindDropDown(drpUnion, dsDefault.Tables[2], "UnionName");

                    ///Bind Veteran Status
                    BindDropDown(drpVeteranStatus, dsDefault.Tables[3], "VeteranStatus");

                    ///Bind Race
                    BindDropDown(ddlPD_Race, dsDefault.Tables[4], "Race");

                    ///Bind Union Book
                    BindDropDown(drpUnionBook, dsDefault.Tables[5], "UnionBook");

                    ///Bind School
                    BindDropDown(drpSchool, dsDefault.Tables[6], "School");

                    ///Bind Manning office
                    BindDropDown(drpManningOffice, dsDefault.Tables[7], "Company_Name");

                    ///Bind School Year Graduated
                    for (int i = DateTime.Now.Year; i >= 1950; i--)
                        drpSchoolYearGraduated.Items.Add(new ListItem() { Value = i.ToString(), Text = i.ToString() });
                    drpSchoolYearGraduated.Items.Insert(0, new ListItem() { Text = "-Select-", Value = "0" });

                    drpManningOffice.Text = UserCompanyID.ToString();
                    drpNaturaliztion.SelectedValue = "1";

                    ///Show hide fields according to settings settings
                    if (dsDefault.Tables[8].Rows.Count > 0)
                    {
                        #region US visa
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='USVisa'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trValidUSVisa.Visible = true;
                        }
                        #endregion
                        #region SSN
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='SSN'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                            {
                                tdtxtSSN.Visible = tdlblSSN.Visible = true;
                                tdlblSSNNOK.Visible = tdtxtSSNNOK.Visible = true;
                                tdlblDependentSSN.Visible = tdtxtDependentSSN.Visible = true;
                                txtDependentDOB.CssClass = txtNOKDOB.CssClass = "required";
                            }
                            else
                            {
                                spnDependentDOB.Visible = spnNOKDOB.Visible = false;
                                txtNOKDOB.Attributes.Remove("class");
                                txtDependentDOB.Attributes.Remove("class");
                            }
                        }
                        #endregion
                        #region Union
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='UUB'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                            {
                                tdlblUnionBook.Visible = tdtxtUnionBook.Visible = trUnion.Visible = true;
                                tdlblUnionBook.Visible = tdtxtUnionBook.Visible = true;
                            }
                        }
                        #endregion
                        #region Veteran
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='Veteran'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                            {
                                tdEmptyVeteran.Visible = tdlblVeteran.Visible = tdtxtVeteran.Visible = true;
                            }
                        }
                        #endregion
                        #region MMC Number
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='MMC'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trMMCNumber.Visible = true;
                        #endregion
                        #region TWIC Number
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='TWIC'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trTWICNumber.Visible = true;
                        #endregion
                        #region Seaman
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='Seaman'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trSeaman.Visible = true;
                        #endregion
                        #region School
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='School'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trSchool.Visible = true;
                        #endregion
                        #region Naturaliztion
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='Naturalization'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trNaturalization.Visible = true;
                        #endregion
                        #region Race
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='Race'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                tdEmptyRace.Visible = tdlblRace.Visible = tdtxtRace.Visible = true;
                        #endregion
                        #region Hire Date
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='HireDate'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                tdlblHireDate.Visible = tdtxtHireDate.Visible = true;
                        #endregion
                        #region Configured Fields
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='CF1'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                            {
                                lblCF1.Text = Convert.ToString(dsDefault.Tables[8].DefaultView[0]["DisplayName"]);
                                fieldsetConfigured.Visible = trCF1.Visible = true;
                            }
                        }

                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='CF2'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                            {
                                lblCF2.Text = Convert.ToString(dsDefault.Tables[8].DefaultView[0]["DisplayName"]);
                                fieldsetConfigured.Visible = trCF2.Visible = true;
                            }
                        }

                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='CF3'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                        {
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                            {
                                lblCF3.Text = Convert.ToString(dsDefault.Tables[8].DefaultView[0]["DisplayName"]);
                                fieldsetConfigured.Visible = trCF3.Visible = true;
                            }
                        }
                        #endregion
                        #region Uniform
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='Uniform'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                trUniform1.Visible = trUniform2.Visible = true;
                        #endregion
                        #region BodyDimensions
                        dsDefault.Tables[8].DefaultView.RowFilter = "";
                        dsDefault.Tables[8].DefaultView.RowFilter = "Key='BodyDimensions'";
                        if (dsDefault.Tables[8].DefaultView.Count > 0)
                            if (Convert.ToBoolean(dsDefault.Tables[8].DefaultView[0]["Display"]))
                                tdlblHeight.Visible = tdtxtHeight.Visible = trHeight.Visible = true;
                        #endregion
                    }

                    /// Bind Document type ID
                    if (dsDefault.Tables[9].Rows.Count > 0)
                    {
                        dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='Passport'";
                        if (dsDefault.Tables[9].DefaultView.Count > 0)
                            hdnPassportDocTypeID.Value = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);

                        dsDefault.Tables[9].DefaultView.RowFilter = "";
                        dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='Seaman'";
                        if (dsDefault.Tables[9].DefaultView.Count > 0)
                            hdnSeamanDocTypeID.Value = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);

                        dsDefault.Tables[9].DefaultView.RowFilter = "";
                        dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='MMCNumber'";
                        if (dsDefault.Tables[9].DefaultView.Count > 0)
                            hdnMMCDocTypeID.Value = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);

                        dsDefault.Tables[9].DefaultView.RowFilter = "";
                        dsDefault.Tables[9].DefaultView.RowFilter = "Document_Type='TWICNumber'";
                        if (dsDefault.Tables[9].DefaultView.Count > 0)
                            hdnTWICDocTypeID.Value = Convert.ToString(dsDefault.Tables[9].DefaultView[0]["DocTypeID"]);
                    }

                    ///Check whether NOK is mandatory or not
                    if (dsDefault.Tables[10].Rows.Count > 0)
                        hdnIsNOKMandatory.Value = Convert.ToString(dsDefault.Tables[10].Rows[0]["Value"]);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Genaric method to bind the dropdown
    /// </summary>
    /// <param name="dropdown"></param>
    /// <param name="dt"></param>
    /// <param name="TextField"></param>
    private void BindDropDown(DropDownList dropdown, DataTable dt, string TextField)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                dropdown.DataSource = dt;
                dropdown.DataTextField = TextField;
                dropdown.DataValueField = "ID";
                dropdown.DataBind();
            }
            else
            {
                dropdown.DataSource = null;
                dropdown.DataBind();
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            dropdown.DataSource = null;
            dropdown.DataBind();
        }
        finally { dropdown.Items.Insert(0, new ListItem() { Text = "-Select-", Value = "0" }); }
    }

    /// <summary>
    /// Save Screen 1 details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCreateNewCrew_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CrewExists", "alert('Please fill all the mandatory fields');", true);
                return;
            }

            string AddressType = "I";
            int CountryID = 0;
            if (hdnAddressFromat.Value == "0")
            {
                AddressType = "US";
                CountryID = Convert.ToInt32(drpUSCountry.SelectedValue);
            }

            string Result = "";
            string SSN = txtSSN.Text == "___-__-____" ? "" : txtSSN.Text;

            DateTime dt = DateTime.Parse("1900/01/01");

            BLL_Crew_CrewDetails objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
            if (objBLL_Crew_CrewDetails.CRW_INS_CrewDetailsScreen1(txtSurname.Text.Trim().ToUpper(), txtName.Text.Trim().ToUpper(), txtMiddleName.Text.Trim(), txtAlias.Text.Trim(), txtPlaceOfBirth.Text.Trim(), UDFLib.ConvertToDate(txtDOB.Text), ddlPD_MaritalStatus.SelectedValue == "0" ? "" : ddlPD_MaritalStatus.SelectedValue,
                Convert.ToInt32(ddlPD_Nationality.SelectedValue), Convert.ToInt32(ddlPD_Race.SelectedValue), SSN, txtAddress.Value, txtAddressline1.Text.Trim(), txtAddressline2.Text.Trim(),
                txtCity.Text.Trim(), txtState.Text.Trim(), txtZipCode.Text.Trim(), CountryID, txtPD_Airport.SelectedText.Trim(), txtPD_Airport.Text == "" ? 0 : Convert.ToInt32(txtPD_Airport.SelectedValue),
                txtPhoneNumber.Text, txtEmailAddress.Text, txtMobileNumber.Text, txtFax.Text, Convert.ToInt32(drpAppliedRank.SelectedValue), Convert.ToInt32(drpManningOffice.SelectedValue),
                UDFLib.ConvertToDate(txtAvailabilityDate.Text), txtHireDate.Text.Trim() == "" ? dt : UDFLib.ConvertToDate(txtHireDate.Text), Convert.ToInt32(drpUnion.SelectedValue), Convert.ToInt32(hdnUnionBranchId.Value), Convert.ToInt32(drpVeteranStatus.SelectedValue),
                Convert.ToInt32(drpUnionBook.SelectedValue), Convert.ToInt32(HiddenField_CrewID.Value), GetSessionUserID(), AddressType, Convert.ToString(hdnCrewPhotoFileName.Value), ref  Result) > 0)
            {
                lblName.Text = txtName.Text;
                lblSurname.Text = txtSurname.Text;
                lblAppliedRank.Text = drpAppliedRank.SelectedItem.Text;
                lblAvilabilityDate.Text = txtAvailabilityDate.Text;

                HiddenField_CrewID.Value = Result.ToString();

                if (hdnCrewPhotoFileName.Value != "")
                {
                    imgCrewPicScreen2.ImageUrl = "~/Uploads/CrewImages/" + hdnCrewPhotoFileName.Value;
                    imgCrewPicScreen2.Attributes.Add("style", "display:block;");
                    imgNoPicScreen2.Attributes.Add("style", "display:none;");
                }
                else
                {
                    imgCrewPicScreen2.Attributes.Add("style", "display:none;");
                    imgNoPicScreen2.Attributes.Add("style", "display:block;");
                }
                if (CrewID == 0)
                    drpNaturaliztion.SelectedValue = "0";

                BtnCreateNewCrew.Text = "Save and Continue";
                pnlView_Screen1.Visible = false;
                pnlView_Screen2.Visible = true;
                pnlView_CommonScreen23.Visible = true;

                #region Check whether passport and seaman book is mandatory form applied rank and nationality
                BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
                if (objCrewAdmin.Check_Document_Mandatory(int.Parse(drpAppliedRank.SelectedValue), int.Parse(ddlPD_Nationality.SelectedValue), "PASSPORT") == 0)
                    hdnIsPassportMandatory.Value = "0";
                else
                    hdnIsPassportMandatory.Value = "1";

                objCrewAdmin = new BLL_Crew_Admin();
                if (objCrewAdmin.Check_Document_Mandatory(int.Parse(drpAppliedRank.SelectedValue), int.Parse(ddlPD_Nationality.SelectedValue), "SEAMAN") == 0)
                    hdnIsSeamanMandatory.Value = "0";
                else
                    hdnIsSeamanMandatory.Value = "1";

                if (hdnIsSeamanMandatory.Value == "0" || hdnIsPassportMandatory.Value == "0")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RemoveMandatory", "RemoveMandatory();", true);
                #endregion
                BindCrewDetails();
            }

            if (Result == "-1")
            {
                if (txtSSN.Text != "")
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CrewExists", "alert('Crew with same SSN exists.');", true);
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CrewExists", "alert('Staff with Same details already exists.');", true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Converts month(alphabets) to numric values
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
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

    protected void BtnCancel_OnClick(object sender, EventArgs e)
    {
        Response.RedirectPermanent("CrewList.aspx");
    }

    private void BindDropdownValue(DropDownList drp, string value)
    {
        drp.ClearSelection();
        if (value != "" && value != "0")
        {
            foreach (var item in drp.Items)
            {
                if (((System.Web.UI.WebControls.ListItem)(item)).Value == value)
                    ((System.Web.UI.WebControls.ListItem)(item)).Selected = true;
            }
        }
    }

    /// <summary>
    /// Bind Crew all details
    /// </summary>
    protected void BindCrewDetails()
    {
        try
        {
            BLL_Crew_CrewDetails objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
            DataSet dsCrewDetails = objBLL_Crew_CrewDetails.CRW_LIB_CD_GetCrewDetails(CrewID);
            if (dsCrewDetails != null)
            {
                if (dsCrewDetails.Tables[0].Rows.Count > 0)
                {
                    #region Personal Details
                    string PageTitle = "Staff: ";

                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Code"]) != "")
                        PageTitle += Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Code"]) + " - ";

                    lblName.Text = txtName.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Staff_Name"]);
                    PageTitle += Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Name"]) + " " + Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Surname"]);

                    Page.Title = PageTitle;

                    HiddenField_CrewStaffCode.Value = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Code"]);

                    if (File.Exists(Server.MapPath("~/Uploads/CrewImages/" + Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["PhotoURL"]))))
                    {
                        hdnCrewPhotoFileName.Value = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["PhotoURL"]);
                        imgCrewPicScreen2.ImageUrl = imgCrewPic.ImageUrl = "~/Uploads/CrewImages/" + Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["PhotoURL"]);
                        imgCrewPicScreen2.Attributes.Add("style", "display:block");
                        imgCrewPic.Attributes.Add("style", "display:block");
                        imgNoPicScreen2.Attributes.Add("style", "display:none");
                        imgNoPic.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        imgCrewPicScreen2.Attributes.Add("style", "display:none");
                        imgCrewPic.Attributes.Add("style", "display:none");
                        imgNoPicScreen2.Attributes.Add("style", "display:block");
                        imgNoPic.Attributes.Add("style", "display:block");
                    }

                    txtMiddleName.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Staff_Midname"]);
                    lblSurname.Text = txtSurname.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Staff_Surname"]);
                    txtAlias.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Alias"]);
                    txtPlaceOfBirth.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Staff_Born_Place"]);
                    txtDOB.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Birth_Date"]));

                    ddlPD_MaritalStatus.ClearSelection();
                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["MaritalStatus"]) != "")
                        ddlPD_MaritalStatus.Text = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["MaritalStatus"]);

                    ddlPD_Nationality.ClearSelection();
                    ddlPD_Nationality.Text = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Nationality"]);

                    ddlPD_Race.ClearSelection();
                    ddlPD_Race.Text = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Race"]);

                    txtSSN.Text = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Staff_Ssn"]);
                    txtPD_Airport.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["NearestAirport"]);
                    if (txtPD_Airport.Text.Trim() == "")
                        txtPD_Airport.SelectedValue = "0";
                    else
                        txtPD_Airport.SelectedValue = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["NearestAirportID"]);
                    #endregion
                    #region Contact Information
                    if (hdnAddressFromat.Value == "0")//US Client
                    {
                        txtAddressline1.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["AddressLine1"]);
                        txtAddressline2.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["AddressLine2"]);
                        txtCity.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["City"]);
                        txtState.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["State"]);
                        if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["CountryId"]) != "")
                        {
                            drpUSCountry.ClearSelection();
                            drpUSCountry.Items.FindByValue(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["CountryId"])).Selected = true;
                        }
                        txtZipCode.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["ZipCode"]);
                    }
                    else
                    {
                        txtAddress.Value = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Address"]);
                    }

                    txtPhoneNumber.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Telephone"]);
                    txtEmailAddress.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["EMail"]);
                    txtMobileNumber.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Mobile"]);
                    txtFax.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Fax"]);
                    #endregion
                    #region Hiring Details
                    drpAppliedRank.ClearSelection();
                    drpAppliedRank.Items.FindByValue(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Rank_Applied"])).Selected = true;

                    drpManningOffice.ClearSelection();
                    drpManningOffice.Items.FindByValue(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["ManningOfficeID"])).Selected = true;

                    lblAppliedRank.Text = drpAppliedRank.SelectedItem.Text;
                    lblAvilabilityDate.Text = txtAvailabilityDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Available_From_Date"]));
                    txtHireDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["HireDate"]));

                    drpUnion.ClearSelection();
                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionId"]) != "")
                        BindDropdownValue(drpUnion, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionId"]));

                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionId"]) != "")
                    {
                        drpUnionBranch.DataSource = null;
                        drpUnionBranch.DataBind();

                        BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                        int rowcount = 0, Result = 0;
                        DataSet dt = objBLL.CRUD_UnionBranch("", 0, Convert.ToInt32(dsCrewDetails.Tables[0].Rows[0]["UnionId"]), "", "", "", "", 0, "", "", "", "R", GetSessionUserID(), "", "UnionBranch", null, 1, 100000, ref  rowcount, ref Result);
                        BindDropDown(drpUnionBranch, dt.Tables[0], "UnionBranch");

                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            drpUnionBranch.ClearSelection();
                            hdnUnionBranchId.Value = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionBranch"]);
                            BindDropdownValue(drpUnionBranch, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionBranch"]));
                            drpUnionBranch.Enabled = true;
                        }
                    }

                    drpVeteranStatus.ClearSelection();
                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["VeteranStatus"]) != "")
                        BindDropdownValue(drpVeteranStatus, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["VeteranStatus"]));

                    drpUnionBook.ClearSelection();
                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionBook"]) != "")
                        BindDropdownValue(drpUnionBook, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["UnionBook"]));

                    drpSchool.ClearSelection();
                    drpSchoolYearGraduated.ClearSelection();
                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["School"]) != "" && Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["School"]) != "0" && Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["School"]).ToLower() != "-select-")
                    {
                        BindDropdownValue(drpSchool, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["School"]));
                        BindDropdownValue(drpSchoolYearGraduated, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["SchoolYearGraduated"]));
                        drpSchoolYearGraduated.Enabled = true;
                    }

                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Naturaliztion"]) != "")
                    {
                        drpNaturaliztion.ClearSelection();
                        if (Convert.ToBoolean(dsCrewDetails.Tables[0].Rows[0]["Naturaliztion"]))
                        {
                            drpNaturaliztion.Items.FindByValue("1").Selected = true;
                            txtNaturaliztionDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["NaturaliztionDate"]));
                            txtNaturaliztionDate.Enabled = true;
                            txtNaturaliztionDate.CssClass = "required";
                            spnNaturaliztionDate.Attributes.Add("style", "display:block");
                        }
                        else
                        {
                            txtNaturaliztionDate.Text = string.Empty;
                            drpNaturaliztion.Items.FindByValue("0").Selected = true;
                        }
                    }
                    else
                        drpNaturaliztion.SelectedValue = "0";

                    #endregion
                    #region Configured Fields
                    txtCF1.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["CustomField1"]);
                    txtCF2.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["CustomField2"]);
                    txtCF3.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["CustomField3"]);
                    #endregion
                    #region Documents
                    txtPassport.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Passport_Number"]);
                    txtPassportPlaceofIssue.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Passport_PlaceOf_Issue"]);
                    txtPassportIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Passport_Issue_Date"]));
                    txtPassportExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Passport_Expiry_Date"]));

                    drpCountryPassport.ClearSelection();
                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Passport_CountryId"]) != "")
                        BindDropdownValue(drpCountryPassport, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Passport_CountryId"]));

                    drpCountrySeaman.ClearSelection();
                    txtSeaman.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Seaman_Book_Number"]);
                    txtSeamanPlaceofIssue.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[0].Rows[0]["Seaman_Book_PlaceOf_Issue"]);
                    txtSeamanIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Seaman_Book_Issue_date"]));
                    txtSeamanExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Seaman_Book_Expiry_Date"]));

                    if (txtSeaman.Text != "")
                        if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Seaman_CountryId"]) != "")
                            BindDropdownValue(drpCountrySeaman, Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Seaman_CountryId"]));

                    if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Us_Visa_Flag"]) != "")
                    {
                        if (Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Us_Visa_Flag"]) == "1")
                        {
                            rblstValidUSVisa.ClearSelection();
                            rblstValidUSVisa.Items.FindByValue("1").Selected = true;

                            txtUSVisaNumber.Enabled = true;
                            spnUSVisaNumber.Attributes.Remove("style");
                            spnUSVisaNumber.Attributes.Add("style", "display:block");
                            txtUSVisaNumber.CssClass = "required";
                            txtUSVisaNumber.Text = Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Us_Visa_Number"]);

                            txtUSVisaIssueDate.Enabled = true;
                            spnUSVisaIssueDate.Attributes.Remove("style");
                            spnUSVisaIssueDate.Attributes.Add("style", "display:block");
                            txtUSVisaIssueDate.CssClass = "required";
                            txtUSVisaIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Us_Visa_IssueDate"]));

                            txtUSVisaExpiryDate.Enabled = true;
                            spnUSVisaExpiryDate.Attributes.Remove("style");
                            spnUSVisaExpiryDate.Attributes.Add("style", "display:block");
                            txtUSVisaExpiryDate.CssClass = "required";
                            txtUSVisaExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[0].Rows[0]["Us_Visa_Expiry"]));
                        }
                        else
                        {
                            rblstValidUSVisa.ClearSelection();
                            rblstValidUSVisa.Items.FindByValue("0").Selected = true;

                            txtUSVisaNumber.Enabled = false;
                            spnUSVisaNumber.Attributes.Remove("style");
                            spnUSVisaNumber.Attributes.Add("style", "display:none");
                            txtUSVisaNumber.CssClass = "";
                            txtUSVisaNumber.Text = "";

                            txtUSVisaIssueDate.Enabled = false;
                            spnUSVisaIssueDate.Attributes.Remove("style");
                            spnUSVisaIssueDate.Attributes.Add("style", "display:none");
                            txtUSVisaIssueDate.CssClass = "";
                            txtUSVisaIssueDate.Text = "";

                            txtUSVisaExpiryDate.Enabled = false;
                            spnUSVisaExpiryDate.Attributes.Remove("style");
                            spnUSVisaExpiryDate.Attributes.Add("style", "display:none");
                            txtUSVisaExpiryDate.CssClass = "";
                            txtUSVisaExpiryDate.Text = "";
                        }
                    }
                    #endregion
                }

                ///NOK and dependents
                rptDependents.DataSource = null;
                rptDependents.DataBind();
                if (dsCrewDetails.Tables[1].Rows.Count > 0)
                {
                    dsCrewDetails.Tables[1].DefaultView.RowFilter = "IsNOK=1 AND IsBeneficiary=1";
                    if (dsCrewDetails.Tables[1].DefaultView.Count > 0)
                    {
                        dsCrewDetails.Tables[1].DefaultView.RowFilter = "";
                        rptDependents.DataSource = dsCrewDetails.Tables[1];
                    }
                    else
                    {
                        dsCrewDetails.Tables[1].DefaultView.RowFilter = "";
                        dsCrewDetails.Tables[1].DefaultView.RowFilter = "IsNOK=0";
                        rptDependents.DataSource = dsCrewDetails.Tables[1];
                    }
                    rptDependents.DataBind();
                    if (rptDependents.Items.Count > 0)
                        rptDependents.Visible = true;
                    else
                        rptDependents.Visible = false;

                    dsCrewDetails.Tables[1].DefaultView.RowFilter = "";
                    dsCrewDetails.Tables[1].DefaultView.RowFilter = "IsNOK=1";
                    if (dsCrewDetails.Tables[1].DefaultView.Count > 0)
                    {
                        hdnNOKID.Value = Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["ID"]);
                        txtNOKFirstName.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["FullName"]);
                        txtNOKSurname.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["Surname"]);

                        drpNOKRelationship.ClearSelection();
                        drpNOKRelationship.Text = Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["Relationship"]);
                        txtNOKPhoneNumber.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["Phone"]);
                        if (!Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["DOB"]).Contains("1900"))
                            txtNOKDOB.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["DOB"]));
                        txtNOKSSN.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["SSN"]);

                        ///For US Client
                        txtNOKAddressline1.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["Address1"]);
                        txtNOKAddressline2.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["Address2"]);
                        txtNOKCity.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["City"]);
                        txtNOKState.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["State"]);
                        txtNOKZipCode.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["ZipCode"]);

                        if (Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["Country"]) != "")
                        {
                            drpNOKUSCountry.ClearSelection();
                            BindDropdownValue(drpNOKUSCountry, Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["Country"]).ToString());
                        }

                        //For International
                        txtNOKAddress.Value = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[1].DefaultView[0]["Address"]);

                        if (Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["IsBeneficiary"]) != "")
                        {
                            rblstMarkasbenficiary.ClearSelection();
                            rblstMarkasbenficiary.Items.FindByValue(Convert.ToString(dsCrewDetails.Tables[1].DefaultView[0]["IsBeneficiary"])).Selected = true;
                        }
                    }
                }

                if (dsCrewDetails.Tables[2].Rows.Count > 0)
                {
                    txtHeight.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[2].Rows[0]["HEIGHT"]);
                    txtWeight.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[2].Rows[0]["WEIGHT"]);
                    txtWaist.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[2].Rows[0]["WAIST"]);
                }



                if (dsCrewDetails.Tables[3].Rows.Count > 0)
                {
                    txtCargoPantSize.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[3].Rows[0]["PANT_SIZE"]);
                    txtOverallSize.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[3].Rows[0]["OVERALL_SIZE"]);
                    txtShirtSize.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[3].Rows[0]["TSHIRT_SIZE"]);
                    txtShoeSize.Text = UDFLib.ConvertStringToNull(dsCrewDetails.Tables[3].Rows[0]["SHOE_SIZE"]);
                }

                if (dsCrewDetails.Tables[4].Rows.Count > 0)
                {
                    drpEnglishProficiency.ClearSelection();
                    BindDropdownValue(drpEnglishProficiency, Convert.ToString(dsCrewDetails.Tables[4].Rows[0]["English_Proficiency"]));
                }

                if (dsCrewDetails.Tables[5].Rows.Count > 0)
                {
                    ///Bind Passport details
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + Convert.ToInt32(hdnPassportDocTypeID.Value);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        hdnPassportDocID.Value = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                        txtPassport.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocNo"]);
                        txtPassportPlaceofIssue.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["PlaceOfIssue"]);
                        txtPassportIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfIssue"]));
                        txtPassportExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfExpiry"]));

                        drpCountryPassport.ClearSelection();
                        if (Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]) != "")
                            BindDropdownValue(drpCountryPassport, Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]));
                    }

                    ///Bind Seaman details
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "";
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + Convert.ToInt32(hdnSeamanDocTypeID.Value);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        hdnSeamanDocID.Value = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                        txtSeaman.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocNo"]);
                        txtSeamanPlaceofIssue.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["PlaceOfIssue"]);
                        txtSeamanIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfIssue"]));
                        txtSeamanExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfExpiry"]));

                        drpCountrySeaman.ClearSelection();
                        if (Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]) != "")
                            BindDropdownValue(drpCountrySeaman, Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]));
                    }

                    ///Bind Crew MMC Number details
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "";
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + Convert.ToInt32(hdnMMCDocTypeID.Value);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        hdnMMCDocID.Value = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                        txtMMCNumber.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocNo"]);
                        txtMMCPlaceofIssue.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["PlaceOfIssue"]);
                        txtMMCIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfIssue"]));
                        txtMMCExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfExpiry"]));

                        drpCountryMMC.ClearSelection();
                        if (Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]) != "")
                            BindDropdownValue(drpCountryMMC, Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]));
                    }

                    /// Bind crew TWIC number details
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "";
                    dsCrewDetails.Tables[5].DefaultView.RowFilter = "DocTypeID=" + Convert.ToInt32(hdnTWICDocTypeID.Value);
                    if (dsCrewDetails.Tables[5].DefaultView.Count > 0)
                    {
                        hdnTWICDocID.Value = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocID"]);
                        txtTWICNumber.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DocNo"]);
                        txtTWICPlaceofIssue.Text = Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["PlaceOfIssue"]);
                        txtTWICIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfIssue"]));
                        txtTWICExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["DateOfExpiry"]));

                        drpCountryTWIC.ClearSelection();
                        if (Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]) != "")
                            BindDropdownValue(drpCountryTWIC, Convert.ToString(dsCrewDetails.Tables[5].DefaultView[0]["CountryOfIssue"]));
                    }
                }
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnNext_OnClick(object sender, EventArgs e)
    {

        BindCrewDetails();
        pnlView_Screen1.Visible = false;
        pnlView_Screen2.Visible = false;
        pnlView_CommonScreen23.Visible = true;
        pnlView_Screen3.Visible = true;

        FramePrejoining.Attributes.Add("src", "CrewDetails_Prejoining.aspx?ID=" + CrewID + "&Page='AddEdit'");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen2", "Check_NOK_Mandatory();", true);
    }

    private void ClearScreenTwoControls()
    {
        txtPassport.Text = string.Empty;
        txtPassportPlaceofIssue.Text = string.Empty;
        txtPassportIssueDate.Text = string.Empty;
        txtPassportExpiryDate.Text = string.Empty;
        drpCountryPassport.ClearSelection();

        txtSeaman.Text = string.Empty;
        txtSeamanExpiryDate.Text = string.Empty;
        txtSeamanIssueDate.Text = string.Empty;
        txtSeamanPlaceofIssue.Text = string.Empty;
        drpCountrySeaman.ClearSelection();

        txtMMCExpiryDate.Text = string.Empty;
        txtMMCIssueDate.Text = string.Empty;
        txtMMCNumber.Text = string.Empty;
        txtMMCPlaceofIssue.Text = string.Empty;
        drpCountryMMC.ClearSelection();

        txtTWICExpiryDate.Text = string.Empty;
        txtTWICIssueDate.Text = string.Empty;
        txtTWICNumber.Text = string.Empty;
        txtTWICPlaceofIssue.Text = string.Empty;
        drpCountryTWIC.ClearSelection();

        drpNaturaliztion.SelectedValue = "0";
        txtNaturaliztionDate.Text = string.Empty;

        drpEnglishProficiency.ClearSelection();

        txtHeight.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtWaist.Text = string.Empty;
        txtShoeSize.Text = string.Empty;
        txtShirtSize.Text = string.Empty;
        txtCargoPantSize.Text = string.Empty;
        txtOverallSize.Text = string.Empty;

        txtCF1.Text = string.Empty;
        txtCF2.Text = string.Empty;
        txtCF3.Text = string.Empty;

        rblstValidUSVisa.SelectedValue = "0";
        txtUSVisaNumber.Text = string.Empty;
        txtUSVisaIssueDate.Text = string.Empty;
        txtUSVisaExpiryDate.Text = string.Empty;
    }
    private void ClearScreenThreeControls()
    {
        txtNOKFirstName.Text = string.Empty;
        txtNOKSurname.Text = string.Empty;
        drpNOKRelationship.ClearSelection();
        txtNOKPhoneNumber.Text = string.Empty;
        txtNOKDOB.Text = string.Empty;
        txtNOKSSN.Text = string.Empty;
        txtNOKAddress.InnerText = string.Empty;
        txtNOKAddressline1.Text = string.Empty;
        txtNOKAddressline2.Text = string.Empty;
        txtNOKCity.Text = string.Empty;
        txtNOKState.Text = string.Empty;
        drpNOKUSCountry.ClearSelection();
        txtNOKZipCode.Text = string.Empty;
    }

    protected void btnBackScreen2_OnClick(object sender, EventArgs e)
    {
        BindCrewDetails();
        ClearScreenTwoControls();
        pnlView_Screen1.Visible = true;
        pnlView_Screen2.Visible = false;
        pnlView_CommonScreen23.Visible = false;
        pnlView_Screen3.Visible = false;
    }

    protected void btnSaveScreen2_OnClick(object sender, EventArgs e)
    {
        try
        {
            bool IsDocTypeExists = false;
            string DocTypeMsg = "";
            string Result = "";

            if (Convert.ToInt32(hdnPassportDocTypeID.Value) == 0 && txtPassport.Text.Trim() != "")
            {
                IsDocTypeExists = true;
                DocTypeMsg += "Passport document type missing";
            }

            if (Convert.ToInt32(hdnSeamanDocTypeID.Value) == 0 && txtSeaman.Text.Trim() != "" && IsDocTypeExists == false)
            {
                IsDocTypeExists = true;
                DocTypeMsg += "Seaman document type missing";
            }

            if (Convert.ToInt32(hdnMMCDocTypeID.Value) == 0 && txtMMCNumber.Text.Trim() != "" && IsDocTypeExists == false)
            {
                IsDocTypeExists = true;
                DocTypeMsg += "MMC Number document type missing";
            }

            if (Convert.ToInt32(hdnTWICDocTypeID.Value) == 0 && txtTWICNumber.Text.Trim() != "" && IsDocTypeExists == false)
            {
                IsDocTypeExists = true;
                DocTypeMsg += "TWIC Number document type missing";
            }



            CrewProperties objCrewProperties = new CrewProperties();
            objCrewProperties.CrewID = Convert.ToInt32(HiddenField_CrewID.Value);

            if (hdnIsPassportMandatory.Value == "1")
            {
                if (txtPassport.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen2", "alert('Please fill all the mandatory fields');", true);
                    return;
                }
            }

            if (hdnIsSeamanMandatory.Value == "1")
            {
                if (txtSeaman.Visible)
                {
                    if (txtSeaman.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen2", "alert('Please fill all the mandatory fields');", true);
                        return;
                    }
                }
            }

            if (!IsDocTypeExists)
            {

                #region Passport
                DateTime dt = DateTime.Parse("1900/01/01");

                objCrewProperties.Passport_Number = txtPassport.Text;
                objCrewProperties.Passport_PlaceOf_Issue = txtPassportPlaceofIssue.Text;
                objCrewProperties.Passport_Issue_Date = txtPassportIssueDate.Text.Trim() != "" ? UDFLib.ConvertToDate(txtPassportIssueDate.Text) : dt;
                objCrewProperties.Passport_Expiry_Date = txtPassportExpiryDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtPassportExpiryDate.Text)) : dt;
                objCrewProperties.Passport_Country = Convert.ToInt32(drpCountryPassport.SelectedValue);
                #endregion
                #region Seaman
                if (txtSeaman.Visible)
                {
                    objCrewProperties.Seaman_Book_Number = txtSeaman.Text;
                    objCrewProperties.Seaman_Book_PlaceOf_Issue = txtSeamanPlaceofIssue.Text;
                    objCrewProperties.Seaman_Book_Issue_Date = txtSeamanIssueDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtSeamanIssueDate.Text)) : dt;
                    objCrewProperties.Seaman_Book_Expiry_Date = txtSeamanExpiryDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtSeamanExpiryDate.Text)) : dt;
                    objCrewProperties.Seaman_Book_Country = Convert.ToInt32(drpCountrySeaman.SelectedValue);
                }
                #endregion
                #region MMC
                if (txtMMCNumber.Visible)
                {
                    objCrewProperties.MMC_Number = txtMMCNumber.Text;
                    objCrewProperties.MMC_PlaceOf_Issue = txtMMCPlaceofIssue.Text;
                    objCrewProperties.MMC_Issue_Date = txtMMCIssueDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtMMCIssueDate.Text)) : dt;
                    objCrewProperties.MMC_Expiry_Date = txtMMCExpiryDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtMMCExpiryDate.Text)) : dt;
                    objCrewProperties.MMC_Country = Convert.ToInt32(drpCountrySeaman.SelectedValue);
                }
                #endregion
                #region TWIC
                if (txtTWICNumber.Visible)
                {
                    objCrewProperties.TWIC_Number = txtTWICNumber.Text;
                    objCrewProperties.TWIC_PlaceOf_Issue = txtTWICPlaceofIssue.Text;
                    objCrewProperties.TWIC_Issue_Date = txtTWICIssueDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtTWICIssueDate.Text)) : dt;
                    objCrewProperties.TWIC_Expiry_Date = txtTWICExpiryDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtTWICExpiryDate.Text)) : dt;
                    objCrewProperties.TWIC_Country = Convert.ToInt32(drpCountryTWIC.SelectedValue);
                }
                #endregion
                #region US Visa
                if (rblstValidUSVisa.Visible)
                {
                    objCrewProperties.USVisa_Flag = Convert.ToInt32(rblstValidUSVisa.SelectedValue);
                    if (rblstValidUSVisa.SelectedValue == "1")
                    {
                        objCrewProperties.USVisa_Number = txtUSVisaNumber.Text;
                        objCrewProperties.USVisa_Issue_Date = txtUSVisaIssueDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtUSVisaIssueDate.Text)) : dt;
                        objCrewProperties.USVisa_Expiry = txtUSVisaExpiryDate.Text.Trim() != "" ? UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtUSVisaExpiryDate.Text)) : dt;
                    }
                }
                #endregion

                objCrewProperties.School = Convert.ToInt32(drpSchool.SelectedValue);
                if (drpSchool.SelectedIndex > 0)
                    objCrewProperties.SchoolYearGraduated = drpSchoolYearGraduated.SelectedValue;

                objCrewProperties.Naturaliztion = (drpNaturaliztion.SelectedValue == "1") ? true : false;
                if (drpNaturaliztion.SelectedValue == "1")
                    objCrewProperties.NaturaliztionDate = UDFLib.ConvertToDate(txtNaturaliztionDate.Text);

                if (drpEnglishProficiency.SelectedIndex != 0)
                    objCrewProperties.EnglishProficiency = drpEnglishProficiency.SelectedValue;

                if (txtHeight.Visible)
                {
                    objCrewProperties.Height = UDFLib.ConvertDecimalToNull(txtHeight.Text);
                    objCrewProperties.Weight = UDFLib.ConvertDecimalToNull(txtWeight.Text);
                    objCrewProperties.Waist = UDFLib.ConvertDecimalToNull(txtWaist.Text);
                }
                if (txtShoeSize.Visible)
                {
                    objCrewProperties.ShoeSize = txtShoeSize.Text;
                    objCrewProperties.TShirtSize = txtShirtSize.Text;
                    objCrewProperties.CargoPantSize = txtCargoPantSize.Text;
                    objCrewProperties.OverallSize = txtOverallSize.Text;
                }

                objCrewProperties.CF1 = txtCF1.Text;
                objCrewProperties.CF2 = txtCF2.Text;
                objCrewProperties.CF3 = txtCF3.Text;

                BLL_Crew_CrewDetails objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                objBLL_Crew_CrewDetails.CRW_INS_CrewDetailsScreen2(objCrewProperties, GetSessionUserID(), ref Result);


                if (Convert.ToInt32(hdnPassportDocID.Value) > 0)
                {
                    objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                    objBLL_Crew_CrewDetails.UPDATE_CrewDocument(CrewID, Convert.ToInt32(hdnPassportDocID.Value), Convert.ToInt32(hdnPassportDocTypeID.Value), "", txtPassport.Text.Trim(), objCrewProperties.Passport_Issue_Date.ToString(), txtPassportPlaceofIssue.Text.Trim(), objCrewProperties.Passport_Expiry_Date.ToString(), GetSessionUserID(), Convert.ToInt32(drpCountryPassport.SelectedValue));
                }
                else if (Convert.ToInt32(hdnPassportDocTypeID.Value) > 0)
                {
                    objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                    objBLL_Crew_CrewDetails.INS_CrewDocuments(CrewID, "", "", "", Convert.ToInt32(hdnPassportDocTypeID.Value), GetSessionUserID(), txtPassport.Text.Trim(), objCrewProperties.Passport_Issue_Date.ToString(), txtPassportPlaceofIssue.Text.Trim(), objCrewProperties.Passport_Expiry_Date.ToString(), UDFLib.ConvertToInteger(drpCountryPassport.SelectedValue));
                }

                if (trSeaman.Visible && Convert.ToInt32(hdnSeamanDocID.Value) > 0)
                {
                    objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                    objBLL_Crew_CrewDetails.UPDATE_CrewDocument(CrewID, Convert.ToInt32(hdnSeamanDocID.Value), Convert.ToInt32(hdnSeamanDocTypeID.Value), "", txtSeaman.Text.Trim(), objCrewProperties.Seaman_Book_Issue_Date.ToString(), txtSeamanPlaceofIssue.Text.Trim(), objCrewProperties.Seaman_Book_Expiry_Date.ToString(), GetSessionUserID(), Convert.ToInt32(drpCountrySeaman.SelectedValue));
                }
                else if (Convert.ToInt32(hdnSeamanDocTypeID.Value) > 0)
                {
                    objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                    objBLL_Crew_CrewDetails.INS_CrewDocuments(CrewID, "", "", "", Convert.ToInt32(hdnSeamanDocTypeID.Value), GetSessionUserID(), txtSeaman.Text.Trim(), objCrewProperties.Seaman_Book_Issue_Date.ToString(), txtSeamanPlaceofIssue.Text.Trim(), objCrewProperties.Seaman_Book_Expiry_Date.ToString(), UDFLib.ConvertToInteger(drpCountrySeaman.SelectedValue));
                }

                if (Convert.ToInt32(hdnMMCDocTypeID.Value) > 0 && txtMMCNumber.Text.Trim() != "")
                {
                    if (Convert.ToInt32(hdnMMCDocID.Value) > 0)
                    {
                        objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                        objBLL_Crew_CrewDetails.UPDATE_CrewDocument(CrewID, Convert.ToInt32(hdnMMCDocID.Value), Convert.ToInt32(hdnMMCDocTypeID.Value), "", txtMMCNumber.Text.Trim(), objCrewProperties.MMC_Issue_Date.ToString(), txtMMCPlaceofIssue.Text.Trim(), objCrewProperties.MMC_Expiry_Date.ToString(), GetSessionUserID(), Convert.ToInt32(drpCountryMMC.SelectedValue));
                    }
                    else if (Convert.ToInt32(hdnMMCDocTypeID.Value) > 0)
                    {
                        objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                        objBLL_Crew_CrewDetails.INS_CrewDocuments(CrewID, "", "", "", Convert.ToInt32(hdnMMCDocTypeID.Value), GetSessionUserID(), txtMMCNumber.Text.Trim(), objCrewProperties.MMC_Issue_Date.ToString(), txtMMCPlaceofIssue.Text.Trim(), objCrewProperties.MMC_Expiry_Date.ToString(), UDFLib.ConvertToInteger(drpCountryMMC.SelectedValue));
                    }
                }

                if (Convert.ToInt32(hdnTWICDocTypeID.Value) > 0 && txtTWICNumber.Text.Trim() != "")
                {
                    if (Convert.ToInt32(hdnTWICDocID.Value) > 0)
                    {
                        objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                        objBLL_Crew_CrewDetails.UPDATE_CrewDocument(CrewID, Convert.ToInt32(hdnTWICDocID.Value), Convert.ToInt32(hdnTWICDocTypeID.Value), "", txtTWICNumber.Text.Trim(), objCrewProperties.TWIC_Issue_Date.ToString(), txtTWICPlaceofIssue.Text.Trim(), objCrewProperties.TWIC_Expiry_Date.ToString(), GetSessionUserID(), Convert.ToInt32(drpCountryTWIC.SelectedValue));
                    }
                    else if (Convert.ToInt32(hdnTWICDocTypeID.Value) > 0)
                    {
                        objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
                        objBLL_Crew_CrewDetails.INS_CrewDocuments(CrewID, "", "", "", Convert.ToInt32(hdnTWICDocTypeID.Value), GetSessionUserID(), txtTWICNumber.Text.Trim(), objCrewProperties.TWIC_Issue_Date.ToString(), txtTWICPlaceofIssue.Text.Trim(), objCrewProperties.TWIC_Expiry_Date.ToString(), UDFLib.ConvertToInteger(drpCountryTWIC.SelectedValue));
                    }
                }
            }

            if (IsDocTypeExists)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MissingDocumentType", "RemoveMandatory();alert('" + DocTypeMsg + "');", true);
            else
            {
                if (Result.ToString() != "0")
                {
                    pnlView_Screen1.Visible = false;
                    pnlView_Screen2.Visible = false;
                    pnlView_CommonScreen23.Visible = true;
                    pnlView_Screen3.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen2", "Check_NOK_Mandatory();", true);
                    FramePrejoining.Attributes.Add("src", "CrewDetails_Prejoining.aspx?ID=" + CrewID + "&Page='AddEdit'");
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen2", "alert('" + UDFLib.GetException("SystemError/GeneralMessage") + "');", true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnBackScreen3_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (drpNaturaliztion.SelectedValue == "1")
            {
                txtNaturaliztionDate.CssClass = "required";
                txtNaturaliztionDate.Enabled = true;
            }
            else
            {
                txtNaturaliztionDate.Attributes.Remove("style");
                txtNaturaliztionDate.Enabled = false;
            }
            if (rblstValidUSVisa.SelectedValue == "1")
            {
                txtUSVisaExpiryDate.Enabled = txtUSVisaIssueDate.Enabled = txtUSVisaNumber.Enabled = true;
                txtUSVisaNumber.CssClass = txtUSVisaIssueDate.CssClass = txtUSVisaExpiryDate.CssClass = "required";
                spnUSVisaNumber.Attributes.Add("style", "display:block");
                spnUSVisaIssueDate.Attributes.Add("style", "display:block");
                spnUSVisaExpiryDate.Attributes.Add("style", "display:block");
            }
            
            BindCrewDetails();
            ClearScreenThreeControls();
            pnlView_Screen1.Visible = false;
            pnlView_Screen2.Visible = true;
            pnlView_CommonScreen23.Visible = true;
            pnlView_Screen3.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RemoveMandatory", "RemoveMandatory();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnNextScreen1_OnClick(object sender, EventArgs e)
    {
        BindCrewDetails();
        pnlView_Screen1.Visible = false;
        pnlView_Screen2.Visible = true;
        pnlView_CommonScreen23.Visible = true;
        pnlView_Screen3.Visible = false;
    }

    /// <summary>
    /// Save 3 screen data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveScreen3_OnClick(object sender, EventArgs e)
    {
        try
        {
            int Result = 0;
            string AddressType = "I";
            int CountryID = 0;
            if (hdnAddressFromat.Value == "0")
            {
                AddressType = "US";
                CountryID = Convert.ToInt32(drpNOKUSCountry.SelectedValue);
            }

            DateTime dt = DateTime.Parse("1900/01/01");

            BLL_Crew_CrewDetails objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
            objBLL_Crew_CrewDetails.CRW_INS_CrewDetailsScreen3(Convert.ToInt32(HiddenField_CrewID.Value), txtNOKFirstName.Text, txtNOKSurname.Text, drpNOKRelationship.SelectedValue, txtNOKPhoneNumber.Text, txtNOKDOB.Text.Trim() == "" ? dt : UDFLib.ConvertToDate(txtNOKDOB.Text), txtNOKSSN.Text == "___-__-____" ? "" : txtNOKSSN.Text, txtNOKAddress.Value, txtNOKAddressline1.Text, txtNOKAddressline2.Text, txtNOKCity.Text, txtNOKState.Text, CountryID, txtNOKZipCode.Text, Convert.ToInt32(rblstMarkasbenficiary.SelectedValue), GetSessionUserID(), Convert.ToInt32(hdnNOKID.Value), AddressType, ref Result);

            if (Result > 0)
            {
                BindCrewDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen3", "Check_NOK_Mandatory();alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen3", "Check_NOK_Mandatory();alert('" + UDFLib.GetException("SystemError/GeneralMessage") + "');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Screen3", "Check_NOK_Mandatory();alert('" + UDFLib.GetException("SystemError/GeneralMessage") + "');", true);
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnSaveExitScreen3_OnClick(object sender, EventArgs e)
    {
        btnSaveScreen3_OnClick(null, null);
        Response.RedirectPermanent("CrewList.aspx");
    }

    protected void rptDependents_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Image ImgbtnEdit = (Image)e.Item.FindControl("Edit");
                Image ImgbtnImgDelete = (Image)e.Item.FindControl("ImgDelete");
                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "IsNOK")))
                    ImgbtnEdit.Visible = ImgbtnImgDelete.Visible = false;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnQuikApproval_OnClick(object sender, EventArgs e)
    {
        Response.RedirectPermanent("CrewApproval.aspx?ID=" + CrewID);
    }

    protected void btnSaveDependents_OnClick(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
            DateTime dt = DateTime.Parse("1900/01/01");

            if (Convert.ToInt32(hdnDependentID.Value) > 0)
            {
                if (hdnAddressFromat.Value == "0")///US
                    result = objBLLCrew.UPDATE_Crew_DependentDetails(0, int.Parse(hdnDependentID.Value), txtDependentFristName.Text.Trim(), txtDependentSurname.Text.Trim(), drpDependentRelationship.SelectedValue, txtDependentAddressline1.Text.Trim(), txtDependentAddressline2.Text.Trim(), "", txtDependentSSN.Text, txtDependentDOB.Text.Trim() == "" ? dt : UDFLib.ConvertToDate(txtDependentDOB.Text), txtDependentCity.Text.Trim(), txtDependentState.Text.Trim(), drpDependentUSCountry.SelectedValue, txtDependentZipCode.Text.Trim(), txtDependentPhoneNumber.Text.Trim(), Convert.ToInt32(rblstDependentBeneficiary.SelectedValue), 0, GetSessionUserID());
                else
                    result = objBLLCrew.UPDATE_Crew_DependentDetails(1, int.Parse(hdnDependentID.Value), txtDependentFristName.Text.Trim(), txtDependentSurname.Text.Trim(), drpDependentRelationship.SelectedValue, "", "", txtDependentAddress.Value, txtDependentSSN.Text, txtDependentDOB.Text.Trim() == "" ? dt : UDFLib.ConvertToDate(txtDependentDOB.Text), "", "", "0", "", txtDependentPhoneNumber.Text.Trim(), Convert.ToInt32(rblstDependentBeneficiary.SelectedValue), 0, GetSessionUserID());
            }
            else
            {
                if (hdnAddressFromat.Value == "0")///US
                    result = objBLLCrew.INS_Crew_DependentDetails(0, CrewID, txtDependentFristName.Text.Trim(), txtDependentSurname.Text.Trim(), drpDependentRelationship.SelectedValue, txtDependentAddressline1.Text.Trim(), txtDependentAddressline2.Text.Trim(), "", txtDependentSSN.Text.Trim(), txtDependentDOB.Text.Trim() == "" ? dt : UDFLib.ConvertToDate(txtDependentDOB.Text), txtDependentCity.Text.Trim(), txtDependentState.Text.Trim(), drpDependentUSCountry.SelectedValue, txtDependentZipCode.Text.Trim(), txtDependentPhoneNumber.Text.Trim(), Convert.ToInt32(rblstDependentBeneficiary.SelectedValue), 0, GetSessionUserID());
                else
                    result = objBLLCrew.INS_Crew_DependentDetails(1, CrewID, txtDependentFristName.Text.Trim(), txtDependentSurname.Text.Trim(), drpDependentRelationship.SelectedValue, "", "", txtDependentAddress.Value.Trim(), "", txtDependentDOB.Text.Trim() == "" ? dt : UDFLib.ConvertToDate(txtDependentDOB.Text), "", "", "0", "", txtDependentPhoneNumber.Text.Trim(), Convert.ToInt32(rblstDependentBeneficiary.SelectedValue), 0, GetSessionUserID());
            }

            if (result > 0)
            {
                BindCrewDetails();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "Check_NOK_Mandatory();alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divAddNewDependent');", true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// To Get union branches
    /// </summary>
    /// <param name="UnionID"></param>
    /// <param name="UserID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetUnionBranch(int UnionID, int UserID)
    {
        StringBuilder rtnStre = new StringBuilder();
        try
        {
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            int rowcount = 0, Result = 0;
            DataSet dt = objBLL.CRUD_UnionBranch("", 0, UnionID, "", "", "", "", 0, "", "", "", "R", UserID, "", "UnionBranch", null, 1, 100000, ref  rowcount, ref Result);

            if (dt.Tables[0].Rows.Count > 0)
            {
                rtnStre.Append("<option value='0'>-Select-</option>");
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    rtnStre.Append("<option value='" + Convert.ToString(dt.Tables[0].Rows[i]["ID"]) + "'>" + Convert.ToString(dt.Tables[0].Rows[i]["UnionBranch"]) + "</option>");
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            rtnStre.Clear();
        }
        return rtnStre.ToString();
    }

    [WebMethod]
    public static string GetDependentDetails(int DependentID, int CrewID, string DateFormat)
    {
        DataTable dt = new DataTable();
        BLL_Crew_CrewDetails objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
        dt = objBLL_Crew_CrewDetails.Get_Crew_DependentsByCrewID(CrewID, 0);
        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.RowFilter = "ID=" + DependentID + " AND CrewID=" + CrewID;
            dt.Columns.Add("DateOfBirth", typeof(string));
            if (DateFormat != "")
                dt.DefaultView[0]["DateOfBirth"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.DefaultView[0]["DOB"]));
            else
                dt.DefaultView[0]["DateOfBirth"] = Convert.ToString(dt.DefaultView[0]["DOB"]);

            return DataTableToJsonObj(dt.DefaultView.ToTable());
        }
        return dt.ToString();
    }

    /// <summary>
    /// Get JSON from datatable
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Delete dependent by dependentID
    /// </summary>
    /// <param name="DependentID"></param>
    /// <param name="UserID"></param>
    /// <returns></returns>
    [WebMethod]
    public static int DeleteDependent(int DependentID, int UserID)
    {
        BLL_Crew_CrewDetails objBLL_Crew_CrewDetails = new BLL_Crew_CrewDetails();
        return objBLL_Crew_CrewDetails.DEL_Crew_DependentDetails(DependentID, UserID);
    }
}
