using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;
using System.Globalization;

public partial class Crew_CrewDetails_Confidential : System.Web.UI.Page
{
    BLL_Crew_Admin obj = new BLL_Crew_Admin();
    int i = 1;
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
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
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TodayDateFormat = UDFLib.DateFormatMessage();
            DFormat = UDFLib.GetDateFormat();
            CalendarExtender3.Format = CalendarExtender2.Format = CalendarExtender1.Format = UDFLib.GetDateFormat();
            CalendarExtender4.Format = CalendarExtender5.Format = CalendarExtender6.Format = UDFLib.GetDateFormat();
            CalendarExtender7.Format = CalendarExtender8.Format = CalendarExtender9.Format = UDFLib.GetDateFormat();
            CalendarExtender10.Format = UDFLib.GetDateFormat();


            CalendarExtender2.EndDate = CalendarExtender4.EndDate = CalendarExtender6.EndDate = CalendarExtender7.EndDate = DateTime.Now;

            if (Session["USERID"] == null)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
                tblLabel.Visible = false;
                tblText.Visible = false;
                return;
            }
            else
            {
                if (!IsPostBack)
                {
                    UserAccessValidation();
                    if (lblMsg.Text != "")
                    {
                        tblLabel.Visible = false;
                        tblText.Visible = false;
                        return;
                    }
                    CustomFieldValidation();
                    tblLabel.Visible = true;
                    tblText.Visible = false;
                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
                    DataSet dss = obj.CRW_CD_GetConfidentialDetails(CrewID);
                    if (dss != null)
                    {
                        DataTable ds = dss.Tables[0];
                        DataTable dsDocTWIC = dss.Tables[1];
                        DataTable dsDocMMC = dss.Tables[2];
                        DataTable dsDocSeaman = dss.Tables[3];
                        DataTable dsHt = dss.Tables[5];
                        DataTable dsDress = dss.Tables[6];
                        BindUnion();
                        BindUnionBook();
                        BindSchool();
                        BindRace();
                        BindVeteran();
                        getConfigurationDetails();
                        BindSchoolYear();
                        BindPermanentStatus();
                        Load_CountryList();
                        LoadSeamanTWICMMCDetails(CrewID);
                        if (dsHt.Rows.Count > 0)
                        {
                            txtHeight.Text = dsHt.Rows[0]["Height"].ToString();
                            lblHeight.Text = dsHt.Rows[0]["Height"].ToString();
                            txtWaist.Text = dsHt.Rows[0]["Waist"].ToString();
                            lblWaist.Text = dsHt.Rows[0]["Waist"].ToString();
                            txtWeight.Text = dsHt.Rows[0]["Weight"].ToString();
                            lblWeight.Text = dsHt.Rows[0]["Weight"].ToString();
                        }
                        if (dsDress.Rows.Count > 0)
                        {
                            txtShoeSize.Text = dsDress.Rows[0]["SHOE_SIZE"].ToString();
                            lblShoe.Text = dsDress.Rows[0]["SHOE_SIZE"].ToString();
                            txtTshirt.Text = dsDress.Rows[0]["TSHIRT_SIZE"].ToString();
                            lblTshirt.Text = dsDress.Rows[0]["TSHIRT_SIZE"].ToString();
                            txtCargo.Text = dsDress.Rows[0]["PANT_SIZE"].ToString();
                            lblCargoPant.Text = dsDress.Rows[0]["PANT_SIZE"].ToString();
                            txtOverall.Text = dsDress.Rows[0]["OVERALL_SIZE"].ToString();
                            lblOverall.Text = dsDress.Rows[0]["OVERALL_SIZE"].ToString();
                        }
                        if (ds.Rows.Count > 0)
                        {

                            //txtAge.Text = ds.Rows[0]["Age"].ToString();
                            //lblAge.Text = ds.Rows[0]["Age"].ToString();
                            txtSSN.Text = ds.Rows[0]["Staff_Ssn"].ToString();
                            lblSSN.Text = ds.Rows[0]["Staff_Ssn"].ToString();
                            if (ds.Rows[0]["Us_Visa_Flag"].ToString() == "1")
                            {
                                rdbUS.SelectedValue = "1";
                                lblUsVisa.Text = "Yes";
                            }
                            else
                            {
                                rdbUS.SelectedValue = "0";
                                lblUsVisa.Text = "No";
                            }
                            txtUSVisaNumber.Text = ds.Rows[0]["Us_Visa_Number"].ToString();
                            lblUSVisaNumber.Text = ds.Rows[0]["Us_Visa_Number"].ToString();
                            if (!string.IsNullOrEmpty(ds.Rows[0]["Us_Visa_IssueDate"].ToString()))
                                txtUSIssue.Text = Convert.ToDateTime(ds.Rows[0]["Us_Visa_IssueDate"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtUSIssue.Text = ds.Rows[0]["Us_Visa_IssueDate"].ToString();
                            lblUSIssueDAte.Text = txtUSIssue.Text;
                            if (!string.IsNullOrEmpty(ds.Rows[0]["Us_Visa_Expiry"].ToString()))
                                txtUSExpiry.Text = Convert.ToDateTime(ds.Rows[0]["Us_Visa_Expiry"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtUSExpiry.Text = ds.Rows[0]["Us_Visa_Expiry"].ToString();
                            lblUSExpiryDAte.Text = txtUSExpiry.Text;

                            ddlPermanent.SelectedValue = ds.Rows[0]["PermanentStatus"].ToString();
                            lblPermanentStatus.Text = ddlPermanent.SelectedItem.Text;
                            if (ddlPermanent.SelectedIndex == 0)
                            {
                                lblPermanentStatus.Text = "";
                            }
                            ddlUnion.SelectedValue = ds.Rows[0]["UnionID"].ToString();
                            lblUnion.Text = ddlUnion.SelectedItem.Text;
                            if (ddlUnion.SelectedIndex == 0)
                                lblUnion.Text = "";
                            ddlUnion_SelectedIndexChanged(sender, e);
                            ddlUnionBranch.SelectedValue = ds.Rows[0]["UnionBranch"].ToString();
                            lnlUnionBranch.Text = ddlUnionBranch.SelectedItem.Text;
                            if (ddlUnionBranch.SelectedIndex == 0)
                                lnlUnionBranch.Text = "";
                            ddlUnionBook.SelectedValue = ds.Rows[0]["UnionBook"].ToString();
                            lblUnionBook.Text = ddlUnionBook.SelectedItem.Text;
                            if (ddlUnionBook.SelectedIndex == 0)
                                lblUnionBook.Text = "";
                            ddlSchool.SelectedValue = ds.Rows[0]["School"].ToString();
                            if (ddlSchool.SelectedIndex != 0)
                                lblSchool.Text = ddlSchool.SelectedItem.Text;
                            ddlSchool_SelectedIndexChanged(sender, e);
                            ddlSchoolGraduated.SelectedValue = ds.Rows[0]["SchoolYearGraduated"].ToString();
                            if (ddlSchoolGraduated.SelectedIndex != 0)
                                lblSchoolGraduated.Text = ddlSchoolGraduated.SelectedItem.Text;

                            if (!string.IsNullOrEmpty(ds.Rows[0]["HireDate"].ToString()))
                                txtHireDate.Text = Convert.ToDateTime(ds.Rows[0]["HireDate"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtHireDate.Text = ds.Rows[0]["HireDate"].ToString();
                            lblHiredate.Text = txtHireDate.Text;
                            ddlRace.SelectedValue = ds.Rows[0]["Race"].ToString();
                            if (ddlRace.SelectedIndex != 0)
                            {
                                lblRace.Text = ddlRace.SelectedItem.Text;
                            }
                            else
                                lblRace.Text = "";
                            txtID.Text = ds.Rows[0]["IDNumber"].ToString();
                            lblIDNumber.Text = txtID.Text;
                            ddlVeteran.SelectedValue = ds.Rows[0]["VeteranStatus"].ToString();
                            if (ddlVeteran.SelectedIndex != 0)
                                lblVeteranStatus.Text = ddlVeteran.SelectedItem.Text;
                            else
                                lblVeteranStatus.Text = "";
                            if (ds.Rows[0]["Naturaliztion"].ToString() == "True")
                            {
                                ddlNature.SelectedValue = "1";
                                lblNaturalization.Text = "Yes";
                            }
                            else if (ds.Rows[0]["Naturaliztion"].ToString() == "False" || string.IsNullOrEmpty(Convert.ToString(ds.Rows[0]["Naturaliztion"])))
                            {
                                ddlNature.SelectedValue = "0";
                                lblNaturalization.Text = "No";
                            }

                            if (!string.IsNullOrEmpty(ds.Rows[0]["NaturaliztionDate"].ToString()))
                                txtNatureDate.Text = Convert.ToDateTime(ds.Rows[0]["NaturaliztionDate"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtNatureDate.Text = "";
                            lblNaturalizationDate.Text = txtNatureDate.Text;
                            txtCF1.Text = ds.Rows[0]["CustomField1"].ToString();
                            txtCF2.Text = ds.Rows[0]["CustomField2"].ToString();
                            txtCF3.Text = ds.Rows[0]["CustomField3"].ToString();
                            lblCF11.Text = ds.Rows[0]["CustomField1"].ToString();
                            lblCF21.Text = ds.Rows[0]["CustomField2"].ToString();
                            lblCF31.Text = ds.Rows[0]["CustomField3"].ToString();
                        }


                        //DocType

                        if (dsDocTWIC.Rows.Count > 0)
                        {
                            txtTWIC.Text = dsDocTWIC.Rows[0]["DocNo"].ToString();
                            lblTwicNo.Text = txtTWIC.Text;
                            if (!string.IsNullOrEmpty(dsDocTWIC.Rows[0]["DateOfIssue"].ToString()))
                                txtTWICIssueDate.Text = Convert.ToDateTime(dsDocTWIC.Rows[0]["DateOfIssue"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtTWICIssueDate.Text = "";
                            lblTIssueDate.Text = txtTWICIssueDate.Text;
                            txtTWICIssuePlace.Text = dsDocTWIC.Rows[0]["PlaceOfIssue"].ToString();
                            lblTIssuePlace.Text = txtTWICIssuePlace.Text;
                            if (!string.IsNullOrEmpty(dsDocTWIC.Rows[0]["DateOfExpiry"].ToString()))
                                txtTWICExpiry.Text = Convert.ToDateTime(dsDocTWIC.Rows[0]["DateOfExpiry"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtTWICExpiry.Text = "";
                            lblTExpiryDate.Text = txtTWICExpiry.Text;

                            if (dsDocTWIC.Rows[0]["CountryOfIssue"].ToString() != "")
                            {
                                drpTWICCountry.ClearSelection();
                                drpTWICCountry.Items.FindByValue(Convert.ToString(dsDocTWIC.Rows[0]["CountryOfIssue"])).Selected = true;
                                if (drpTWICCountry.SelectedIndex != 0)
                                    lblTWICCountry.Text = drpTWICCountry.SelectedItem.Text;
                                else
                                    lblTWICCountry.Text = "";
                            }

                        }

                        if (dsDocMMC.Rows.Count > 0)
                        {
                            txtMMC.Text = dsDocMMC.Rows[0]["DocNo"].ToString();
                            lblMMCno.Text = txtMMC.Text;
                            if (!string.IsNullOrEmpty(dsDocMMC.Rows[0]["DateOfIssue"].ToString()))
                                txtMMCISSueDate.Text = Convert.ToDateTime(dsDocMMC.Rows[0]["DateOfIssue"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtMMCISSueDate.Text = "";
                            lblMMCIssueDate.Text = txtMMCISSueDate.Text;
                            txtMMCIssuePlace.Text = dsDocMMC.Rows[0]["PlaceOfIssue"].ToString();
                            lblMMCIssuePlace.Text = txtMMCIssuePlace.Text;
                            if (!string.IsNullOrEmpty(dsDocMMC.Rows[0]["DateOfExpiry"].ToString()))
                                txtMMCExpiryDate.Text = Convert.ToDateTime(dsDocMMC.Rows[0]["DateOfExpiry"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtMMCExpiryDate.Text = "";
                            lblMMCExpiryDate.Text = txtMMCExpiryDate.Text;

                            if (dsDocMMC.Rows[0]["CountryOfIssue"].ToString() != "")
                            {
                                drpMMCCountry.ClearSelection();
                                drpMMCCountry.Items.FindByValue(Convert.ToString(dsDocMMC.Rows[0]["CountryOfIssue"])).Selected = true;
                                if (drpMMCCountry.SelectedIndex != 0)
                                    lblMMCCountry.Text = drpMMCCountry.SelectedItem.Text;
                                else
                                    lblMMCCountry.Text = "";
                            }
                        }

                        if (dsDocSeaman.Rows.Count > 0)
                        {
                            txtSeaman.Text = dsDocSeaman.Rows[0]["DocNo"].ToString();
                            lblSeamanbook.Text = txtSeaman.Text;
                            if (!string.IsNullOrEmpty(dsDocSeaman.Rows[0]["DateOfIssue"].ToString()))
                                txtSeamanIssueDate.Text = Convert.ToDateTime(dsDocSeaman.Rows[0]["DateOfIssue"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtSeamanIssueDate.Text = "";
                            lblSeamanIssueDate.Text = txtSeamanIssueDate.Text;
                            txtSeamanIssuePlace.Text = dsDocSeaman.Rows[0]["PlaceOfIssue"].ToString();
                            lblSeamanIssuePlace.Text = txtSeamanIssuePlace.Text;
                            if (!string.IsNullOrEmpty(dsDocSeaman.Rows[0]["DateOfExpiry"].ToString()))
                                txtSeamanExpiryDate.Text = Convert.ToDateTime(dsDocSeaman.Rows[0]["DateOfExpiry"].ToString()).ToString(UDFLib.GetDateFormat());
                            else
                                txtSeamanExpiryDate.Text = "";
                            lblSeamanExpiryDate.Text = txtSeamanExpiryDate.Text;

                            if (dsDocSeaman.Rows[0]["CountryOfIssue"].ToString() != "")
                            {
                                drpSeamanCountry.ClearSelection();
                                drpSeamanCountry.Items.FindByValue(Convert.ToString(dsDocSeaman.Rows[0]["CountryOfIssue"])).Selected = true;
                                if (drpSeamanCountry.SelectedIndex != 0)
                                    lblSeamanCountry.Text = drpSeamanCountry.SelectedItem.Text;
                                else
                                    lblSeamanCountry.Text = "";
                            }
                        }



                        //

                        disableControls();
                        rdbUS_SelectedIndexChanged(sender, e);
                        lnkEditNOKConf.OnClientClick = "EditConfidential(" + CrewID.ToString() + "); return false;";
                        if (Request.QueryString["Mode"] == "EDIT")
                        {
                            EnableControls();
                            tblText.Visible = true;
                            tblLabel.Visible = false;
                            lnkEditNOKConf.Visible = false;
                            btnSave.Visible = true;
                            btnClose.Visible = true;
                            txtSSN.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtHireDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            ddlRace.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            ddlVeteran.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            ddlUnion.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9"); ;
                            ddlUnionBook.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            ddlUnionBranch.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtTWIC.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtTWICExpiry.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtTWICIssueDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtTWICIssuePlace.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtMMC.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtMMCExpiryDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtMMCISSueDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                            txtMMCIssuePlace.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");

                            ViewState["RankApplied"] = dss.Tables[0].Rows[0]["Rank_Applied"].ToString();
                            ViewState["Nationality"] = dss.Tables[0].Rows[0]["Staff_Nationality"].ToString();
                            if (!string.IsNullOrEmpty(ViewState["RankApplied"].ToString()))
                            {
                                if (obj.Check_Document_Mandatory(int.Parse(ViewState["RankApplied"].ToString()), int.Parse(ViewState["Nationality"].ToString()), "SEAMAN") != 0)
                                {
                                    lblS1.Visible = true;
                                    lblS2.Visible = true;
                                    lblS3.Visible = true;
                                    lblS4.Visible = true;
                                    txtSeaman.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                    txtSeamanExpiryDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                    txtSeamanIssueDate.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                    txtSeamanIssuePlace.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
                                }
                                else
                                {

                                    txtSeaman.BackColor = System.Drawing.Color.Transparent;
                                    txtSeamanExpiryDate.BackColor = System.Drawing.Color.Transparent;
                                    txtSeamanIssueDate.BackColor = System.Drawing.Color.Transparent;
                                    txtSeamanIssuePlace.BackColor = System.Drawing.Color.Transparent;
                                }
                            }
                            else
                            {
                                txtSeaman.BackColor = System.Drawing.Color.Transparent;
                                txtSeamanExpiryDate.BackColor = System.Drawing.Color.Transparent;
                                txtSeamanIssueDate.BackColor = System.Drawing.Color.Transparent;
                                txtSeamanIssuePlace.BackColor = System.Drawing.Color.Transparent;
                            }

                            ddlNature_SelectedIndexChanged(sender, e);
                        }

                    }
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
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        }

        if (objUA.Edit == 0)
        {
            lnkEditNOKConf.Visible = false;
        }

        if (objUA.Approve == 0)
        {
        }

    }
    public void disableControls()
    {
    }

    public void EnableControls()
    {
    }

    protected void BindUnion()
    {

        ddlUnion.DataSource = obj.CRUD_Union("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlUnion.DataValueField = "ID";
        ddlUnion.DataTextField = "UnionName";
        ddlUnion.DataBind();
        ddlUnion.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindUnionBook()
    {

        ddlUnionBook.DataSource = obj.CRUD_UnionBook("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlUnionBook.DataValueField = "ID";
        ddlUnionBook.DataTextField = "UnionBook";
        ddlUnionBook.DataBind();
        ddlUnionBook.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void BindSchool()
    {

        ddlSchool.DataSource = obj.CRUD_School("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlSchool.DataValueField = "ID";
        ddlSchool.DataTextField = "School";
        ddlSchool.DataBind();
        ddlSchool.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void BindRace()
    {

        ddlRace.DataSource = obj.CRUD_Race("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlRace.DataValueField = "ID";
        ddlRace.DataTextField = "Race";
        ddlRace.DataBind();
        ddlRace.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void BindVeteran()
    {

        ddlVeteran.DataSource = obj.CRUD_VeteranStatus("", "R", UDFLib.ConvertToInteger(GetSessionUserID()), 0, "", "VeteranStatus", null, 1, 10000, ref i, ref i);
        ddlVeteran.DataValueField = "ID";
        ddlVeteran.DataTextField = "VeteranStatus";
        ddlVeteran.DataBind();
        ddlVeteran.Items.Insert(0, new ListItem("-SELECT-", "0"));
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

    protected void BindPermanentStatus()
    {

        ddlPermanent.DataSource = obj.CRUD_PermanentStatus("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlPermanent.DataValueField = "ID";
        ddlPermanent.DataTextField = "Status";
        ddlPermanent.DataBind();
        ddlPermanent.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSSN.Visible == true)
            {
                if (txtSSN.Text == "")
                {
                    lblMsg.Text = "SSN is mandatory.";
                    return;
                }
                if (!ValidateSSN(txtSSN.Text))
                {
                    lblMsg.Text = "Enter valid SSN ";
                    return;
                }
                if (!IsExistSSN(txtSSN.Text.Trim()))
                {
                    lblMsg.Text = "SSN number is already assigned to another Crew ";
                    return;
                }
            }


            if (ddlUnion.Visible == true)
            {
                if (ddlUnion.SelectedIndex == 0)
                {
                    lblMsg.Text = "Union is mandatory.";
                    return;
                }
            }

            if (ddlUnionBranch.Visible == true)
            {
                if (ddlUnionBranch.SelectedIndex == 0)
                {
                    lblMsg.Text = "Union branch is mandatory.";
                    return;
                }
            }


            if (ddlUnionBook.Visible == true)
            {
                if (ddlUnionBook.SelectedIndex == 0)
                {
                    lblMsg.Text = "Union book is mandatory.";
                    return;
                }
            }

            if (rdbUS.SelectedValue == "1")
            {
                if (txtUSVisaNumber.Text.Trim() == "" || txtUSIssue.Text.Trim() == "" || txtUSExpiry.Text.Trim() == "")
                {
                    lblMsg.Text = "Enter US visa Details";
                    return;
                }
                if (txtUSVisaNumber.Text != "")
                {
                    if (txtUSIssue.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtUSIssue.Text));
                        }
                        catch
                        {
                            lblMsg.Text = "Enter valid US Visa Issue date" + TodayDateFormat;
                            return;
                        }
                    }
                    if (txtUSExpiry.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtUSExpiry.Text));
                        }
                        catch
                        {
                            lblMsg.Text = "Enter valid US Visa expiry date" + TodayDateFormat;
                            return;
                        }
                    }

                }
            }

            if (txtHireDate.Visible == true)
            {
                if (txtHireDate.Text == "")
                {
                    lblMsg.Text = "Hire Date is mandatory.";
                    return;
                }
            }

            if (ddlRace.Visible == true)
            {
                if (ddlRace.SelectedIndex == 0)
                {
                    lblMsg.Text = "Race is mandatory.";
                    return;
                }
            }

            if (ddlVeteran.Visible == true)
            {
                if (ddlVeteran.SelectedIndex == 0)
                {
                    lblMsg.Text = "Veteran status is mandatory.";
                    return;
                }
            }
            if (ddlSchool.Visible == true)
            {
                if (ddlSchool.SelectedIndex != 0)
                {
                    if (ddlSchoolGraduated.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Year Graduated is mandatory.";
                        return;
                    }
                }
            }

            if (ddlNature.Visible == true)
            {
                if (ddlNature.SelectedValue == "1")
                {
                    if (txtNatureDate.Text == "")
                    {
                        lblMsg.Text = "Naturalization date is mandatory.";
                        return;
                    }
                    try
                    {
                        DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtNatureDate.Text));
                    }
                    catch
                    {
                        lblMsg.Text = "Enter valid Naturalization date" + TodayDateFormat;
                        return;
                    }
                }
            }
            if (Convert.ToInt32(ViewState["MMCDocTypeID"]) == 0 && Convert.ToInt32(ViewState["TWICDocTypeID"]) == 0 && txtMMC.Text.Trim() != "" && txtTWIC.Text.Trim() != "")
            {
                lblMsg.Text = "MMC Number and TWIC Number document type missing";
                return;
            }
            if (txtSeaman.Visible == true)
            {
                if (Convert.ToInt32(ViewState["SeamanDocTypeID"]) == 0 && txtSeaman.Text.Trim() != "")
                {
                    lblMsg.Text = "Seaman document type missing";
                    return;
                }
                if (obj.Check_Document_Mandatory(int.Parse(ViewState["RankApplied"].ToString()), int.Parse(ViewState["Nationality"].ToString()), "SEAMAN") != 0)
                {
                    if (txtSeaman.Text != "")
                    {

                        if (txtSeamanIssuePlace.Text == "")
                        {
                            lblMsg.Text = "Seaman Issue place is mandatory.";
                            return;
                        }
                        else
                            if (txtSeamanIssueDate.Text == "")
                            {
                                lblMsg.Text = "Seaman Issue date is mandatory.";
                                return;
                            }

                            else if (txtSeamanExpiryDate.Text == "")
                            {
                                lblMsg.Text = "Seaman Expiry date is mandatory.";
                                return;
                            }
                        if (txtSeamanIssueDate.Text != "")
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanIssueDate.Text));
                            }
                            catch
                            {
                                lblMsg.Text = "Enter valid Seaman Issue date" + TodayDateFormat;
                                return;
                            }
                        }
                        if (txtSeamanExpiryDate.Text != "")
                        {
                            try
                            {
                                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSeamanExpiryDate.Text));
                            }
                            catch
                            {
                                lblMsg.Text = "Enter valid Seaman Expiry date" + TodayDateFormat;
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Seaman(Book) Number is mandatory.";
                        return;
                    }
                }

            }
            if (txtMMC.Visible == true)
            {
                if (Convert.ToInt32(ViewState["MMCDocTypeID"]) == 0 && txtMMC.Text.Trim() != "")
                {
                    lblMsg.Text = "MMC Number document type missing";
                    return;
                }
                if (txtMMC.Text != "")
                {

                    if (txtMMCIssuePlace.Text == "")
                    {
                        lblMsg.Text = "MMC Issue place is mandatory.";
                        return;
                    }
                    else
                        if (txtMMCISSueDate.Text == "")
                        {
                            lblMsg.Text = "MMC Issue date is mandatory.";
                            return;
                        }

                        else if (txtMMCExpiryDate.Text == "")
                        {
                            lblMsg.Text = "MMC Expiry date is mandatory.";
                            return;
                        }
                    if (txtMMCISSueDate.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtMMCISSueDate.Text));
                        }
                        catch
                        {
                            lblMsg.Text = "Enter valid MMC issue date" + TodayDateFormat;
                            return;
                        }
                    }
                    if (txtMMCExpiryDate.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtMMCExpiryDate.Text));
                        }
                        catch
                        {
                            lblMsg.Text = "Enter valid MMC expiry date" + TodayDateFormat;
                            return;
                        }
                    }

                }
                else
                {
                    lblMsg.Text = "MMC Number is mandatory.";
                    return;
                }
            }

            if (txtTWIC.Visible == true)
            {
                if (Convert.ToInt32(ViewState["TWICDocTypeID"]) == 0 && txtTWIC.Text.Trim() != "")
                {
                    lblMsg.Text = "TWIC Number document type missing";
                    return;
                }
                if (txtTWIC.Text != "")
                {


                    if (txtTWICIssuePlace.Text == "")
                    {
                        lblMsg.Text = "TWIC Issue place is mandatory.";
                        return;
                    }
                    else

                        if (txtTWICIssueDate.Text == "")
                        {
                            lblMsg.Text = "TWIC Issue date is mandatory.";
                            return;
                        }

                        else if (txtTWICExpiry.Text == "")
                        {
                            lblMsg.Text = "TWIC Expiry date is mandatory.";
                            return;
                        }

                    if (txtTWICIssueDate.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTWICIssueDate.Text));
                        }
                        catch
                        {
                            lblMsg.Text = "Enter valid TWIC issue date" + TodayDateFormat;
                            return;
                        }
                    }

                    if (txtTWICExpiry.Text != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTWICExpiry.Text));
                        }
                        catch
                        {
                            lblMsg.Text = "Enter valid TWIC expiry date" + TodayDateFormat;
                            return;
                        }
                    }


                }
                else
                {
                    lblMsg.Text = "TWIC Number is mandatory.";
                    return;
                }
            }
            decimal d;
            if (!string.IsNullOrEmpty(txtHeight.Text))
            {
                if (!decimal.TryParse(txtHeight.Text, out d))
                {
                    lblMsg.Text = "Height must be a decimal/integer number";
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtWaist.Text))
            {
                if (!decimal.TryParse(txtWaist.Text, out d))
                {
                    lblMsg.Text = "Waist must be a decimal/integer number";
                    return;

                }
            }
            if (!string.IsNullOrEmpty(txtWeight.Text))
            {
                if (!decimal.TryParse(txtWeight.Text, out d))
                {
                    lblMsg.Text = "Weight must be a decimal/integer number";
                    return;
                }
            }


            obj.CRW_CD_UPDConfidentialDetails(UDFLib.ConvertDecimalToNull(txtHeight.Text.Trim()), UDFLib.ConvertDecimalToNull(txtWeight.Text.Trim()), UDFLib.ConvertDecimalToNull(txtWaist.Text.Trim()),
                 UDFLib.ConvertIntegerToNull(ddlPermanent.SelectedValue), txtSSN.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlUnion.SelectedValue)
                , UDFLib.ConvertIntegerToNull(ddlUnionBranch.SelectedValue), UDFLib.ConvertIntegerToNull(ddlUnionBook.SelectedValue),
                 UDFLib.ConvertIntegerToNull(ddlSchool.SelectedValue), ddlSchoolGraduated.SelectedValue, ConvertToDate(txtHireDate.Text.Trim()), UDFLib.ConvertIntegerToNull(ddlRace.SelectedValue),
                 txtID.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlVeteran.SelectedValue), UDFLib.ConvertIntegerToNull(ddlNature.SelectedValue), ConvertToDate(txtNatureDate.Text.Trim()), txtCF1.Text.Trim(), txtCF2.Text.Trim(), txtCF3.Text.Trim(),
                UDFLib.ConvertToInteger(Request.QueryString["ID"]), txtMMCIssuePlace.Text.Trim(), txtTWICIssuePlace.Text.Trim(), txtMMC.Text.Trim(),
                 ConvertToDate(txtMMCISSueDate.Text.Trim()), ConvertToDate(txtTWICIssueDate.Text.Trim()), ConvertToDate(txtMMCExpiryDate.Text.Trim()),
                 txtTWIC.Text.Trim(), ConvertToDate(txtTWICExpiry.Text.Trim()), txtSeamanIssuePlace.Text.Trim(), ConvertToDate(txtSeamanIssueDate.Text.Trim()), txtSeaman.Text.Trim(),

              ConvertToDate(txtSeamanExpiryDate.Text.Trim()), txtTshirt.Text.Trim(), txtCargo.Text.Trim(), txtOverall.Text.Trim(),
               txtShoeSize.Text.Trim(), rdbUS.SelectedValue, ConvertToDate(txtUSIssue.Text.Trim()), txtUSVisaNumber.Text.Trim(), ConvertToDate(txtUSExpiry.Text.Trim()), GetSessionUserID()
                );
            if (Convert.ToInt32(ViewState["SeamanDocID"]) <= 0)
            {
                objBLLCrew.INS_CrewDocuments(UDFLib.ConvertToInteger(Request.QueryString["ID"]), "", "", "", Convert.ToInt32(ViewState["SeamanDocTypeID"]), GetSessionUserID(), txtSeaman.Text.Trim(), ConvertToDate(txtSeamanIssueDate.Text).ToString(), txtSeamanIssuePlace.Text.Trim(), ConvertToDate(txtSeamanExpiryDate.Text).ToString(), Convert.ToInt32(drpSeamanCountry.SelectedValue));

            }
            else
                objBLLCrew.UPDATE_CrewDocument(UDFLib.ConvertToInteger(Request.QueryString["ID"]), Convert.ToInt32(ViewState["SeamanDocID"]), Convert.ToInt32(ViewState["SeamanDocTypeID"]), "", txtSeaman.Text.Trim(), ConvertToDate(txtSeamanIssueDate.Text).ToString(), txtSeamanIssuePlace.Text.Trim(), ConvertToDate(txtSeamanExpiryDate.Text).ToString(), GetSessionUserID(), Convert.ToInt32(drpSeamanCountry.SelectedValue));

            if (Convert.ToInt32(ViewState["TWICDocID"]) <= 0)
            {
                objBLLCrew.INS_CrewDocuments(UDFLib.ConvertToInteger(Request.QueryString["ID"]), "", "", "", Convert.ToInt32(ViewState["TWICDocTypeID"]), GetSessionUserID(), txtTWIC.Text.Trim(), ConvertToDate(txtTWICIssueDate.Text).ToString(), txtTWICIssuePlace.Text.Trim(), ConvertToDate(txtTWICExpiry.Text).ToString(), Convert.ToInt32(drpTWICCountry.SelectedValue));

            }
            else
                objBLLCrew.UPDATE_CrewDocument(UDFLib.ConvertToInteger(Request.QueryString["ID"]), Convert.ToInt32(ViewState["TWICDocID"]), Convert.ToInt32(ViewState["TWICDocTypeID"]), "", txtTWIC.Text.Trim(), ConvertToDate(txtTWICIssueDate.Text).ToString(), txtTWICIssuePlace.Text.Trim(), ConvertToDate(txtTWICExpiry.Text).ToString(), GetSessionUserID(), Convert.ToInt32(drpTWICCountry.SelectedValue));

            if (Convert.ToInt32(ViewState["MMCDocID"]) <= 0)
            {
                objBLLCrew.INS_CrewDocuments(UDFLib.ConvertToInteger(Request.QueryString["ID"]), "", "", "", Convert.ToInt32(ViewState["MMCDocTypeID"]), GetSessionUserID(), txtMMC.Text.Trim(), ConvertToDate(txtMMCISSueDate.Text).ToString(), txtMMCIssuePlace.Text.Trim(), ConvertToDate(txtMMCExpiryDate.Text).ToString(), Convert.ToInt32(drpMMCCountry.SelectedValue));

            }
            else
                objBLLCrew.UPDATE_CrewDocument(UDFLib.ConvertToInteger(Request.QueryString["ID"]), Convert.ToInt32(ViewState["MMCDocID"]), Convert.ToInt32(ViewState["MMCDocTypeID"]), "", txtMMC.Text.Trim(), ConvertToDate(txtMMCISSueDate.Text).ToString(), txtMMCIssuePlace.Text.Trim(), ConvertToDate(txtMMCExpiryDate.Text).ToString(), GetSessionUserID(), Convert.ToInt32(drpMMCCountry.SelectedValue));

            string js = "parent.GetConfidentialResult(" + Request.QueryString["ID"].ToString() + ");parent.hideModal('dvPopupFrame');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void CustomFieldValidation()
    {
        DataSet ds1 = obj.CRW_GetCDConfiguration("CF1");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lblCF1.Text = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
            lblCF4.Text = ds1.Tables[0].Rows[0]["DisplayName"].ToString();
        }

        DataSet ds2 = obj.CRW_GetCDConfiguration("CF2");
        if (ds2.Tables[0].Rows.Count > 0)
        {
            lblCF2.Text = ds2.Tables[0].Rows[0]["DisplayName"].ToString();
            lblCF5.Text = ds2.Tables[0].Rows[0]["DisplayName"].ToString();
        }

        DataSet ds3 = obj.CRW_GetCDConfiguration("CF3");
        if (ds3.Tables[0].Rows.Count > 0)
        {
            lblCF3.Text = ds3.Tables[0].Rows[0]["DisplayName"].ToString();
            lblCF6.Text = ds3.Tables[0].Rows[0]["DisplayName"].ToString();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    protected void ddlUnion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlUnionBranch.DataSource = obj.CRUD_UnionBranch("", 0, Convert.ToInt32(ddlUnion.SelectedValue), "", "", "", "", 0, "", "", "", "R", GetSessionUserID(), null, "UnionBranch", null, 1, 100000, ref  i, ref i);
        ddlUnionBranch.DataValueField = "ID";
        ddlUnionBranch.DataTextField = "UnionBranch";
        ddlUnionBranch.DataBind();
        ddlUnionBranch.Items.Insert(0, "-Select-");
    }
    protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNature.SelectedValue == "1")
        {
            txtNatureDate.Enabled = true;
            txtNatureDate.CssClass = "required";
            lblnaturem.Visible = true;
        }
        else
        {
            txtNatureDate.Text = string.Empty;
            txtNatureDate.Enabled = false;
            txtNatureDate.CssClass = "";
            lblnaturem.Visible = false;
        }
    }

    public void getConfigurationDetails()
    {
        string str = "";
        DataRow[] dr;
        DataTable dt = obj.CRW_GetCDConfiguration(null).Tables[0];
        if (dt.Rows.Count > 0)
        {
            // string confSSN=dt.row
            str = "BodyDimensions";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trBodyDimensions1.Visible = true;
                    trBodyDimensions4.Visible = true;
                }
                else
                {
                    trBodyDimensions1.Visible = false;
                    trBodyDimensions4.Visible = false;
                }
            }

            str = "Uniform";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trUniform1.Visible = true;
                    trUniform5.Visible = true;

                }
                else
                {
                    trUniform1.Visible = false;
                    trUniform5.Visible = false;
                }
            }


            str = "USVisa";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trUS1.Visible = true;
                    trUS5.Visible = true;
                    tblDocuments.Visible = tblDocuments1.Visible = true;

                }
                else
                {
                    trUS1.Visible = false;
                    trUS5.Visible = false;
                    tblDocuments.Visible = tblDocuments1.Visible = false;

                }
            }

            str = "SSN";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    lblSSN.Visible = LabelSSN.Visible = true;
                    txtSSN.Visible = lblSSNEdit.Visible = spnSSNEdit.Visible = true;
                    tdlblSSN.Attributes.Add("class", "data");
                }
                else
                {
                    lblSSN.Visible = LabelSSN.Visible = false;
                    txtSSN.Visible = lblSSNEdit.Visible = spnSSNEdit.Visible = false;
                    tdlblSSN.Attributes.Add("class", "");
                }
            }
            str = "UUB";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {

                    trUnion1.Visible = true;
                    trUnion4.Visible = true;
                    tdlblUnionBook.Visible = lblUnionBook.Visible = LabelUnionBook.Visible = true;
                    tdEditlblUnionBook.Visible = tdEdittxtUnionBook.Visible = true;
                }
                else
                {
                    trUnion1.Visible = false;
                    trUnion4.Visible = false;
                    tdlblUnionBook.Visible = lblUnionBook.Visible = LabelUnionBook.Visible = false;
                    tdEditlblUnionBook.Visible = tdEdittxtUnionBook.Visible = false;
                }
            }

            str = "TWIC";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trTWIC1.Visible = true;
                    trTWIC5.Visible = true;
                    tblDocuments.Visible = tblDocuments1.Visible = true;
                }
                else
                {
                    trTWIC1.Visible = false;
                    trTWIC5.Visible = false;
                }
            }

            str = "MMC";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trMMC1.Visible = true;
                    trMMC8.Visible = true;
                    tblDocuments.Visible = tblDocuments1.Visible = true;
                }
                else
                {
                    trMMC1.Visible = false;
                    trMMC8.Visible = false;
                }
            }

            str = "Seaman";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trSeaman1.Visible = true;
                    trSeaman5.Visible = true;
                    tblDocuments.Visible = tblDocuments1.Visible = true;
                }
                else
                {
                    trSeaman1.Visible = false;
                    trSeaman5.Visible = false;
                }
            }
            str = "School";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trSchool1.Visible = true;
                    trSchool3.Visible = true;
                }
                else
                {
                    trSchool1.Visible = false;
                    trSchool3.Visible = false;
                }
            }
            str = "HireDate";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trHire.Visible = true;
                    trHire1.Visible = true;

                }
                else
                {
                    trHire.Visible = false;
                    trHire1.Visible = false;
                }
            }

            str = "Race";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trRace.Visible = true;
                    trRace1.Visible = true;
                }
                else
                {
                    trRace.Visible = false;
                    trRace1.Visible = false;
                }
            }

            str = "PermanentStatus";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trPermanent.Visible = true;
                    trPermanent1.Visible = true;
                }
                else
                {
                    trPermanent.Visible = false;
                    trPermanent1.Visible = false;
                }
            }

            str = "IDNumber";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trID.Visible = true;
                    trID1.Visible = true;
                }
                else
                {
                    trID.Visible = false;
                    trID1.Visible = false;
                }
            }

            str = "Veteran";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trVeteran.Visible = true;
                    trVeteran1.Visible = true;
                }
                else
                {
                    trVeteran.Visible = false;
                    trVeteran1.Visible = false;
                }
            }
            str = "Naturalization";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trNature1.Visible = true;
                    trNature3.Visible = true;
                }
                else
                {
                    trNature1.Visible = false;
                    trNature3.Visible = false;
                }
            }

            str = "CF1";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trCF1.Visible = true;
                    trCF4.Visible = true;
                }
                else
                {
                    trCF1.Visible = false;
                    trCF4.Visible = false;
                }
            }
            str = "CF2";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trCF2.Visible = true;
                    trCF5.Visible = true;
                }
                else
                {
                    trCF2.Visible = false;
                    trCF5.Visible = false;
                }
            }

            str = "CF3";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "True")
                {
                    trCF3.Visible = true;
                    trCF6.Visible = true;
                }
                else
                {
                    trCF3.Visible = false;
                    trCF6.Visible = false;
                }
            }

            if (trSchool3.Visible || trUnion4.Visible || trNature3.Visible || trVeteran1.Visible || trRace1.Visible || trPermanent1.Visible || trHire1.Visible || trID1.Visible || LabelSSN.Visible)
                tblGeneralInformation1.Visible = tblGeneralInformation.Visible = true;
            else
                tblGeneralInformation1.Visible = tblGeneralInformation.Visible = false;

            if (trSeaman5.Visible || trMMC8.Visible || trTWIC5.Visible || trUS5.Visible)
                tblDocuments1.Visible = tblDocuments.Visible = true;
            else
                tblDocuments1.Visible = tblDocuments.Visible = false;

            if (trCF4.Visible || trCF5.Visible || trCF6.Visible)
                tblAdditionalFields1.Visible = tblAdditionalFields.Visible = true;
            else
                tblAdditionalFields1.Visible = tblAdditionalFields.Visible = false;

            if (tblGeneralInformation1.Visible || tblDocuments1.Visible || tblAdditionalFields1.Visible || trUniform5.Visible || trBodyDimensions4.Visible)
                lnkEditNOKConf.Visible = true;
            else
                lnkEditNOKConf.Visible = false;
        }
    }
    protected void rdbUS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbUS.SelectedValue == "0")
        {
            txtUSExpiry.Enabled = false;
            txtUSIssue.Enabled = false;
            txtUSVisaNumber.Enabled = false;
            txtUSExpiry.Text = "";
            txtUSVisaNumber.Text = "";
            txtUSIssue.Text = "";
            txtUSIssue.CssClass = txtUSVisaNumber.CssClass = txtUSExpiry.CssClass = "";
            spnMandtoryUSVisa.Visible = spnMandtoryUSVisaExpiry.Visible = spnMandtoryUSVisaIssue.Visible = false;
        }
        else
        {
            txtUSExpiry.Enabled = true;
            txtUSIssue.Enabled = true;
            txtUSVisaNumber.Enabled = true;
            txtUSIssue.CssClass = txtUSVisaNumber.CssClass = txtUSExpiry.CssClass = "required";
            spnMandtoryUSVisa.Visible = spnMandtoryUSVisaExpiry.Visible = spnMandtoryUSVisaIssue.Visible = true;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string jss = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jss, true);
    }

    protected bool ValidateSSN(string ssn)
    {
        if (ssn == "078-05-1120" || ssn == "219-09-9999")
        {
            return false;
        }
        if (ssn.Contains('-'))
        {
            if (ssn.Length == 11)
            {
                if (ssn.Split('-')[0].Length != 3)
                {
                    return false;
                }
                if (ssn.Split('-')[0] == "000" || ssn.Split('-')[0] == "666")
                {
                    return false;
                }
                if (UDFLib.ConvertToInteger(ssn.Split('-')[0]) >= 900 && UDFLib.ConvertToInteger(ssn.Split('-')[0]) <= 999)
                {
                    return false;
                }
                if (ssn.Split('-')[1].Length != 2)
                {
                    return false;
                }
                if (ssn.Split('-')[1] == "00")
                {
                    return false;
                }
                if (ssn.Split('-')[2].Length != 4)
                {
                    return false;
                }
                if (ssn.Split('-')[2] == "0000")
                {
                    return false;
                }
                return true;
            }
        }

        return false;
    }

    protected bool IsExistSSN(string ssn)
    {
        int i = obj.CRW_CD_Validate_SSN(ssn, UDFLib.ConvertToInteger(Request.QueryString["ID"]));
        if (i == 1)
        {
            return true;
        }
        else
            return false;

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

    protected DateTime? ConvertToDate(string Date)
    {
        DateTime date = new DateTime();
        if (Date != "")
        {
            if (DFormat.ToLower() == "dd-mm-yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[2]), Convert.ToInt32(Date.Split('-')[1]), Convert.ToInt32(Date.Split('-')[0]));
            if (DFormat.ToLower() == "mm-dd-yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[2]), Convert.ToInt32(Date.Split('-')[0]), Convert.ToInt32(Date.Split('-')[1]));
            else if (DFormat.ToLower() == "dd-mmm-yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[2]), ConvertMonthToDigit(Date.Split('-')[1].ToString()), Convert.ToInt32(Date.Split('-')[0]));
            else if (DFormat.ToLower() == "yyyy-mm-dd")
                date = new DateTime(Convert.ToInt32(Date.Split('-')[0]), Convert.ToInt32(Date.Split('-')[1]), Convert.ToInt32(Date.Split('-')[2]));
            else if (DFormat.ToLower() == "mm/dd/yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('/')[2]), Convert.ToInt32(Date.Split('/')[0]), Convert.ToInt32(Date.Split('/')[1]));
            else if (DFormat.ToLower() == "dd/mm/yyyy")
                date = new DateTime(Convert.ToInt32(Date.Split('/')[2]), Convert.ToInt32(Date.Split('/')[1]), Convert.ToInt32(Date.Split('/')[0]));
        }
        else
        {
            return null;
        }
        return date;
    }

    protected void Load_CountryList()
    {
        try
        {
            BLL_Infra_Country objCountry = new BLL_Infra_Country();
            DataTable dt = objCountry.Get_CountryList();

            drpSeamanCountry.DataSource = dt;
            drpSeamanCountry.DataTextField = "COUNTRY";
            drpSeamanCountry.DataValueField = "ID";
            drpSeamanCountry.DataBind();
            drpSeamanCountry.Items.Insert(0, new ListItem("-Select-", "0"));

            drpMMCCountry.DataSource = dt;
            drpMMCCountry.DataTextField = "COUNTRY";
            drpMMCCountry.DataValueField = "ID";
            drpMMCCountry.DataBind();
            drpMMCCountry.Items.Insert(0, new ListItem("-Select-", "0"));

            drpTWICCountry.DataSource = dt;
            drpTWICCountry.DataTextField = "COUNTRY";
            drpTWICCountry.DataValueField = "ID";
            drpTWICCountry.DataBind();
            drpTWICCountry.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void LoadSeamanTWICMMCDetails(int CrewID)
    {
        DataSet dsDefault = objBLLCrew.CRW_GET_DefaultValuesCrewAddEdit(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        DataSet dsCrewDetails = objBLLCrew.CRW_LIB_CD_GetCrewDetails(CrewID);
        if (dsDefault != null)
        {
            DataTable dtDocument = dsDefault.Tables[9];
            if (dtDocument.Rows.Count > 0)
            {


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
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSchoolGraduated.Items.Clear();
        BindSchoolYear();
        if (ddlSchool.SelectedIndex == 0)
        {
            lblSY.Visible = false;
            ddlSchoolGraduated.BackColor = System.Drawing.Color.Transparent;
            ddlSchoolGraduated.Enabled = false;
        }
        else
        {
            lblSY.Visible = true;
            ddlSchoolGraduated.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F5A9");
            ddlSchoolGraduated.Enabled = true;
        }
    }
}