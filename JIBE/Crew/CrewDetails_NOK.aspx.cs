using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_NOK : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    BLL_Crew_Admin objAdmin = new BLL_Crew_Admin();
    BLL_Infra_Country objCountry = new BLL_Infra_Country();
    public string DFormat = "";
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
            DFormat = UDFLib.GetDateFormat();
            CalendarExtender1.Format = DFormat;
            CalendarExtender2.Format = DFormat;
            CalendarExtender3.Format = DFormat;
            CalendarExtender4.Format = DFormat;

            CalendarExtender1.EndDate = CalendarExtender3.EndDate = CalendarExtender2.EndDate = CalendarExtender4.EndDate = DateTime.Now;
            DataTable dManadatory = objAdmin.GetMandatorySettings();
            if (dManadatory.Rows.Count > 0)
            {
                DataRow[] dNOK = dManadatory.Select("Key_Name='NOK'");
                if (dNOK.Length > 0)
                {
                    if (dNOK[0].ItemArray[0].ToString() == "0")
                    {
                        btnSaveNOKDetails.CausesValidation = false;
                        Button3.CausesValidation = false;
                        UnCheckMandatory();
                    }
                }
                else
                {
                    CheckMandatory();
                }
            }

            if (Session["USERID"] == null)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
            }
            else
            {
                if (!IsPostBack)
                {
                    UserAccessValidation();
                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                    int DependentID = UDFLib.ConvertToInteger(Request.QueryString["DependentID"]);
                    string Mode = Request.QueryString["Mode"];
                    hdnNOKID.Value = DependentID.ToString();
                    DataTable dtAddress = null;
                    if (objAdmin.CRW_GetCDConfiguration("Addressformat").Tables.Count > 0)
                    {
                        dtAddress = objAdmin.CRW_GetCDConfiguration("Addressformat").Tables[0];
                    }
                    DataTable dtSSN = null;
                    if (objAdmin.CRW_GetCDConfiguration("SSN").Tables.Count > 0)
                    {
                        dtSSN = objAdmin.CRW_GetCDConfiguration("SSN").Tables[0];
                    }
                    bindCountry();
                    if (dtAddress.Rows.Count > 0)
                    {

                        //International Address
                        if (dtAddress.Rows[0]["Value"].ToString() == "True")
                        {

                            tblInternational.Visible = true;
                            tblUS.Visible = false;
                            tdI.Visible = true;
                            tdU.Visible = false;
                            tblDepInternational.Visible = true;
                            tblDepUS.Visible = false;

                        }
                        else
                        {
                            tblInternational.Visible = false;
                            tblUS.Visible = true;
                            tblDepInternational.Visible = false;
                            tblDepUS.Visible = true;
                            tdI.Visible = false;
                            tdU.Visible = true;
                        }

                    }
                    if (dtSSN.Rows.Count > 0)
                    {
                        if (dtSSN.Rows[0]["Display"].ToString() != "True")
                        {
                            txtSSN.Visible = false;
                            lblSSN.Visible = false;
                            lblSSNUS.Visible = false;
                            txtDepSSN.Visible = false;
                            lblUSSSN.Visible = false;
                            lblSSNV.Visible = false;
                        }
                    }
                    if (Mode == "EDIT_DEP")
                    {
                        pnlAdd_Dependents.Visible = true;
                        DataTable dt = objBLLCrew.Get_Crew_DependentsByCrewID(CrewID, 0);
                        DataRow[] dr = dt.Select("ID=" + DependentID.ToString());
                        if (dr.Length > 0)
                        {
                            txtDepFirstName.Text = dr[0]["FirstName"].ToString();
                            txtDepSurname.Text = dr[0]["Surname"].ToString();
                            ddlDepRelationship.SelectedValue = dr[0]["Relationship"].ToString();
                            txtDepPhone.Text = dr[0]["Phone"].ToString();
                            txtDepSSN.Text = dr[0]["SSN"].ToString();
                            txtDepSSNI.Text = dr[0]["SSN"].ToString();
                            txtDepDOB.Text = string.IsNullOrEmpty(dr[0]["DOB"].ToString()) ? "" : DateTime.Parse(dr[0]["DOB"].ToString()).ToString(UDFLib.GetDateFormat());
                            txtDepCity.Text = dr[0]["City"].ToString();
                            ddlDepCountry.SelectedValue = dr[0]["Country"].ToString();
                            txtDepZip.Text = dr[0]["ZipCode"].ToString();
                            txtDepAdd1.Text = dr[0]["Address1"].ToString();
                            txtDepAdd2.Text = dr[0]["Address2"].ToString();
                            txtDepState.Text = dr[0]["State"].ToString();
                            txtDepFirstName1.Text = dr[0]["FirstName"].ToString();
                            txtDepSurname1.Text = dr[0]["Surname"].ToString();
                            ddlDepRelationShip1.SelectedValue = dr[0]["Relationship"].ToString();
                            txtDepPhone1.Text = dr[0]["Phone"].ToString();
                            txtDepDoB1.Text = string.IsNullOrEmpty(dr[0]["DOB"].ToString()) ? "" : DateTime.Parse(dr[0]["DOB"].ToString()).ToString(UDFLib.GetDateFormat());
                            txtDepInternational.Text = dr[0]["Address"].ToString();
                            if (dr[0]["IsBeneficiary"].ToString() == "1")
                            {
                                rdbDepBeneficiary.SelectedIndex = 0;
                                rdbDepBenefeciary1.SelectedIndex = 0;
                            }
                            else
                            {
                                rdbDepBeneficiary.SelectedIndex = 1;
                                rdbDepBenefeciary1.SelectedIndex = 1;
                            }
                        }

                    }
                    else if (Mode == "EDIT_NOK")
                    {
                        pnlAddEdit_NextOfKin.Visible = true;
                        Load_Next_Of_Kin(CrewID);

                    }
                    else if (Mode == "INSERT")
                    {
                        pnlAdd_Dependents.Visible = true;
                        pnlView_NextOfKin.Visible = false;

                    }
                    else if (Mode == "ADD_NOK")
                    {
                        pnlAddEdit_NextOfKin.Visible = true;
                        pnlView_NextOfKin.Visible = false;
                    }
                    else
                    {
                        if (objUA.View == 1)
                        {
                            pnlView_NextOfKin.Visible = true;
                            Load_Next_Of_Kin(CrewID);
                            Load_Dependents(CrewID);
                        }
                    }

                }
            }
            ConfidentialityCheck();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path);

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
            lnkEditNOK.Visible = false;
            GridView_Dependents.Columns[GridView_Dependents.Columns.Count - 1].Visible = false;
        }

        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_Next_Of_Kin(int CrewID)
    {
        try
        {
            int add = 0;
            DataTable dtNOK = objBLLCrew.Get_Crew_DependentsByCrewID(CrewID, 1);
            DataTable dtAddress = null;
            if (objAdmin.CRW_GetCDConfiguration("Addressformat").Tables.Count > 0)
            {
                dtAddress = objAdmin.CRW_GetCDConfiguration("Addressformat").Tables[0];
            }
            if (dtAddress.Rows.Count > 0)
            {
                if (dtAddress.Rows[0]["Value"].ToString() == "True")
                {
                    // txtAddress2.Text = "";
                    add = 1;
                }
            }
            if (dtNOK.Rows.Count > 0)
            {
                try
                {
                    txtFirstName.Text = dtNOK.Rows[0]["FirstName"].ToString();
                    txtSurname.Text = dtNOK.Rows[0]["Surname"].ToString();
                    ddlNOKRelationship.SelectedValue = dtNOK.Rows[0]["RelationShip"].ToString();
                    txtPhone.Text = dtNOK.Rows[0]["Phone"].ToString();
                    txtSSN.Text = dtNOK.Rows[0]["SSN"].ToString();
                    txtSSNI.Text = dtNOK.Rows[0]["SSN"].ToString();
                    txtDOB.Text = string.IsNullOrEmpty(dtNOK.Rows[0]["DOB"].ToString()) ? "" : Convert.ToDateTime(dtNOK.Rows[0]["DOB"].ToString()).ToString(UDFLib.GetDateFormat());
                    txtAddress1.Text = dtNOK.Rows[0]["Address1"].ToString();
                    txtAddress2.Text = dtNOK.Rows[0]["Address2"].ToString();
                    txtCity.Text = dtNOK.Rows[0]["City"].ToString();
                    txtState.Text = dtNOK.Rows[0]["State"].ToString();
                    ddlCountry.SelectedValue = dtNOK.Rows[0]["Country"].ToString();
                    txtZip.Text = dtNOK.Rows[0]["ZipCode"].ToString();
                    if (dtNOK.Rows[0]["FirstName"].ToString().Length > 35)
                    {
                        lblNOKName.Text = dtNOK.Rows[0]["FirstName"].ToString().Substring(0, 32) + "...";
                        lblNOKName.Attributes.Add("rel", dtNOK.Rows[0]["FirstName"].ToString());
                    }
                    else
                        lblNOKName.Text = dtNOK.Rows[0]["FirstName"].ToString();
                    if (dtNOK.Rows[0]["Surname"].ToString().Length > 35)
                    {
                        lblSurname.Text = dtNOK.Rows[0]["Surname"].ToString().Substring(0, 32) + "...";
                        lblSurname.Attributes.Add("rel", dtNOK.Rows[0]["Surname"].ToString());
                    }
                    else
                        lblSurname.Text = dtNOK.Rows[0]["Surname"].ToString();

                    if (dtNOK.Rows[0]["RelationShip"].ToString() == "0")
                    {
                        lblNOKrelationship.Text = "";
                    }
                    else
                        lblNOKrelationship.Text = dtNOK.Rows[0]["RelationShip"].ToString();
                    lblNOKPhone.Text = dtNOK.Rows[0]["Phone"].ToString();
                    lblSSN.Text = dtNOK.Rows[0]["SSN"].ToString();
                    lblSSNI.Text = dtNOK.Rows[0]["SSN"].ToString();
                    lblDOB.Text = string.IsNullOrEmpty(dtNOK.Rows[0]["DOB"].ToString()) ? "" : Convert.ToDateTime(dtNOK.Rows[0]["DOB"].ToString()).ToString(UDFLib.GetDateFormat());
                    lblNOKName1.Text = dtNOK.Rows[0]["FirstName"].ToString();
                    lblSurname1.Text = dtNOK.Rows[0]["Surname"].ToString();
                    if (dtNOK.Rows[0]["RelationShip"].ToString() == "0")
                        lblRelationship1.Text = "";
                    else
                        lblRelationship1.Text = dtNOK.Rows[0]["RelationShip"].ToString();
                    lblPhone1.Text = dtNOK.Rows[0]["Phone"].ToString();
                    if (!string.IsNullOrEmpty(dtNOK.Rows[0]["Country"].ToString()))
                    {
                        if (Convert.ToString(dtNOK.Rows[0]["Country"]) != "0")
                            lblCountry.Text = ddlCountry.Items.FindByValue(dtNOK.Rows[0]["Country"].ToString()).Text;
                    }
                    else
                    {

                        lblCountry.Text = "";
                    }
                    lblDoB1.Text = string.IsNullOrEmpty(dtNOK.Rows[0]["DOB"].ToString()) ? "" : Convert.ToDateTime(dtNOK.Rows[0]["DOB"].ToString()).ToString(UDFLib.GetDateFormat());

                    if (dtNOK.Rows[0]["Address"].ToString().Length > 35)
                    {
                        lblAddress.Text = dtNOK.Rows[0]["Address"].ToString().Substring(0, 32) + "...";
                        lblAddress.Attributes.Add("rel", dtNOK.Rows[0]["Address"].ToString());
                    }
                    else
                        lblAddress.Text = dtNOK.Rows[0]["Address"].ToString();

                    if (dtNOK.Rows[0]["Address1"].ToString().Length > 35)
                    {
                        lblNOKAddress1.Text = dtNOK.Rows[0]["Address1"].ToString().Substring(0, 32) + "...";
                        lblNOKAddress1.Attributes.Add("rel", dtNOK.Rows[0]["Address1"].ToString());
                    }
                    else
                        lblNOKAddress1.Text = dtNOK.Rows[0]["Address1"].ToString();

                    if (dtNOK.Rows[0]["Address2"].ToString().Length > 35)
                    {
                        lblNOKAddress2.Text = dtNOK.Rows[0]["Address2"].ToString().Substring(0, 32) + "...";
                        lblNOKAddress2.Attributes.Add("rel", dtNOK.Rows[0]["Address2"].ToString());
                    }
                    else
                        lblNOKAddress2.Text = dtNOK.Rows[0]["Address2"].ToString();

                    if (dtNOK.Rows[0]["City"].ToString().Length > 35)
                    {
                        lblCity.Text = dtNOK.Rows[0]["City"].ToString().Substring(0, 32) + "...";
                        lblCity.Attributes.Add("rel", dtNOK.Rows[0]["City"].ToString());
                    }
                    else
                        lblCity.Text = dtNOK.Rows[0]["City"].ToString();

                    if (dtNOK.Rows[0]["State"].ToString().Length > 35)
                    {
                        lblState.Text = dtNOK.Rows[0]["State"].ToString().Substring(0, 32) + "...";
                        lblState.Attributes.Add("rel", dtNOK.Rows[0]["State"].ToString());
                    }
                    else
                        lblState.Text = dtNOK.Rows[0]["State"].ToString();


                    //lblAddress.Text = dtNOK.Rows[0]["Address"].ToString();
                    //lblNOKAddress1.Text = dtNOK.Rows[0]["Address1"].ToString();
                    //lblNOKAddress2.Text = dtNOK.Rows[0]["Address2"].ToString();
                    //lblCity.Text = dtNOK.Rows[0]["City"].ToString();
                    //lblState.Text = dtNOK.Rows[0]["State"].ToString();

                    if (dtNOK.Rows[0]["ZipCode"].ToString().Length > 35)
                    {
                        lblZipCode.Text = dtNOK.Rows[0]["ZipCode"].ToString().Substring(0, 32) + "...";
                        lblZipCode.Attributes.Add("rel", dtNOK.Rows[0]["ZipCode"].ToString());
                    }
                    else
                        lblZipCode.Text = dtNOK.Rows[0]["ZipCode"].ToString();

                    if (dtNOK.Rows[0]["IsBeneficiary"].ToString() == "1")
                        rdbBeneficiary.SelectedIndex = 0;
                    else
                        rdbBeneficiary.SelectedIndex = 1;

                    txtFirstName1.Text = dtNOK.Rows[0]["FirstName"].ToString();
                    txtSurname1.Text = dtNOK.Rows[0]["Surname"].ToString();
                    ddlRelationShip1.SelectedValue = dtNOK.Rows[0]["RelationShip"].ToString();
                    txtPhone1.Text = dtNOK.Rows[0]["Phone"].ToString();
                    txtDOB1.Text = string.IsNullOrEmpty(dtNOK.Rows[0]["DOB"].ToString()) ? "" : Convert.ToDateTime(dtNOK.Rows[0]["DOB"].ToString()).ToString(UDFLib.GetDateFormat());
                    txtAddressInternational.Text = dtNOK.Rows[0]["Address"].ToString();
                    lblAddress.Text = txtAddressInternational.Text.Replace("\n", "<br/>");

                    if (dtNOK.Rows[0]["IsBeneficiary"].ToString() == "1")
                    {
                        rdbBeneficiary1.SelectedIndex = 0;
                    }
                    else
                    {
                        rdbBeneficiary1.SelectedIndex = 1;
                    }
                }
                catch
                {

                }

                lnkEditNOK.OnClientClick = "EditNOK(" + CrewID.ToString() + "," + dtNOK.Rows[0]["ID"].ToString() + "); return false;";
            }
            else
            {
                lnkEditNOK.OnClientClick = "AddNOK(" + CrewID.ToString() + "); return false;";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveNOKDetails_Click(object sender, EventArgs e)
    {
        try
        {

            int add = 0;
            int IsBeneficiary = 0;
            int IsBeneficiary1 = 0;
            if (rdbBeneficiary.SelectedIndex == 0)
            {
                IsBeneficiary = 1;
            }
            if (rdbBeneficiary1.SelectedIndex == 0)
            {
                IsBeneficiary1 = 1;
            }
            DataTable dtAddress = null;
            if (objAdmin.CRW_GetCDConfiguration("Addressformat").Tables.Count > 0)
            {
                dtAddress = objAdmin.CRW_GetCDConfiguration("Addressformat").Tables[0];
            }
            if (dtAddress.Rows.Count > 0)
            {
                if (dtAddress.Rows[0]["Value"].ToString() == "True")
                {
                    add = 1;

                }
                else
                {
                    add = 0;

                }
            }
            if (txtDOB.Text != "" && txtDOB.Visible==true)
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDOB.Text));
                    if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "sc", "alert('Date of Birth cannot be future date');", true);
                        return;
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }
            if (txtSSN.Visible == true)
            {
                lblDOBstar.Visible = true;

                if (txtSSN.Text != "")
                {
                    if (txtDOB.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter Date of Birth');", true);
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDOB.Text));
                            if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Date of Birth cannot be future date');", true);
                                return;
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                            return;
                        }
                    }
                    if (!ValidateSSN(txtSSN.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid SSN No.');", true);
                        return;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter SSN No.');", true);
                    return;
                }

            }
            else
            {
                lblDOBstar.Visible = false;

            }
            if (txtDOB1.Text != "" && txtDOB1.Visible==true)
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDOB1.Text));
                    if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Date of Birth cannot be future date');", true);
                        return;
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }
            if (txtSSNI.Visible == true)
            {
                lblDobStar2.Visible = true;
                if (txtSSNI.Text != "")
                {
                    if (txtDOB1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter Date of Birth');", true);
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDOB1.Text));
                            if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Date of Birth cannot be future date');", true);
                                return;
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                            return;
                        }
                    }
                    if (!ValidateSSN(txtSSNI.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn2", "alert('Enter valid SSN No.');", true);
                        return;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter SSN No.');", true);
                    return;
                }

            }
            else
            {
                lblDobStar2.Visible = false;
            }
            string js = "";
            int result = 0;
            try
            {
                if (UDFLib.ConvertToInteger(hdnNOKID.Value) > 0)
                {

                    if (add == 0)
                        result = objBLLCrew.UPDATE_Crew_DependentDetails(add, int.Parse(hdnNOKID.Value), txtFirstName.Text.Trim(), txtSurname.Text.Trim(), ddlNOKRelationship.SelectedValue, txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), txtAddress1.Text.Trim(), txtSSN.Text.Trim(), ConvertToDate(txtDOB.Text), txtCity.Text.Trim(), txtState.Text.Trim(), ddlCountry.SelectedValue, txtZip.Text.Trim(), txtPhone.Text.Trim(), IsBeneficiary, 1, GetSessionUserID());
                    else
                        result = objBLLCrew.UPDATE_Crew_DependentDetails(add, int.Parse(hdnNOKID.Value), txtFirstName1.Text.Trim(), txtSurname1.Text.Trim(), ddlRelationShip1.SelectedValue, txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), txtAddressInternational.Text.Trim(), txtSSNI.Text.Trim(), ConvertToDate(txtDOB1.Text), txtCity.Text.Trim(), txtState.Text.Trim(), "", txtZip.Text.Trim(), txtPhone1.Text.Trim(), IsBeneficiary1, 1, GetSessionUserID());

                    if (result > 0)
                        lblMsg.Text = "NOK details updated.";
                    else
                        lblMsg.Text = "Unable to update NOK details.";

                    js = "parent.GetCrewNOKAndDependents(" + Request.QueryString["CrewID"].ToString() + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsGetCrewNOKAndDependents", js, true);

                }
                else
                {
                    if (add == 0)
                        result = objBLLCrew.INS_Crew_DependentDetails(add, UDFLib.ConvertToInteger(Request.QueryString["CREWID"]), txtFirstName.Text.Trim(), txtSurname.Text.Trim(), ddlNOKRelationship.SelectedValue, txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), "", txtSSN.Text.Trim(), ConvertToDate(txtDOB.Text), txtCity.Text.Trim(), txtState.Text.Trim(), ddlCountry.SelectedValue.Trim(), txtZip.Text.Trim(), txtPhone.Text.Trim(), IsBeneficiary, 1, GetSessionUserID());
                    else
                        result = objBLLCrew.INS_Crew_DependentDetails(add, UDFLib.ConvertToInteger(Request.QueryString["CREWID"]), txtFirstName1.Text.Trim(), txtSurname1.Text.Trim(), ddlRelationShip1.SelectedValue, "", "", txtAddressInternational.Text.Trim(), txtSSNI.Text.Trim(), ConvertToDate(txtDOB.Text), "", "", "", "", txtPhone1.Text.Trim(), IsBeneficiary, 1, GetSessionUserID());

                    if (result > 0)
                        lblMsg.Text = "NOK details added.";
                    else
                        lblMsg.Text = "Unable to add NOK details.";

                    js = "parent.GetCrewNOKAndDependents(" + Request.QueryString["CrewID"].ToString() + ");parent.hideModal('dvPopupFrame');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsGetCrewNOKAndDependents", js, true);
                }
            }

            catch (Exception ex)
            {
                lblMsg.Text = "Unable to save NOK details !! " + ex.Message;
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        string jss = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jss, true);
    }
    protected void btnSaveAndCloseNOKDetails_Click(object sender, EventArgs e)
    {
        string jss = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jss, true);
    }

    protected void Load_Dependents(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_Crew_DependentsByCrewID(CrewID, 0);
        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = "IsNOK desc";
            GridView_Dependents.DataSource = dv.ToTable();
            GridView_Dependents.DataBind();
        }
    }

    protected void btnSaveDependents_Click(object sender, EventArgs e)
    {
        try
        {
            int add = 0;
            int IsBeneficiary = 0;
            int IsBeneficiary1 = 0;
            if (rdbDepBeneficiary.SelectedIndex == 0)
            {
                IsBeneficiary = 1;
            }
            if (rdbDepBenefeciary1.SelectedIndex == 0)
            {
                IsBeneficiary1 = 1;
            }

            DataTable dtAddress = null;
            if (objAdmin.CRW_GetCDConfiguration("Addressformat").Tables.Count > 0)
            {
                dtAddress = objAdmin.CRW_GetCDConfiguration("Addressformat").Tables[0];
            }
            if (dtAddress.Rows.Count > 0)
            {
                if (dtAddress.Rows[0]["Value"].ToString() == "True")
                {
                    // txtAddress2.Text = "";
                    add = 1;
                }
                else
                {
                    add = 0;
                }
            }

            if (txtDepDOB.Text != "" && txtDepDOB.Visible == true)
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDepDOB.Text));
                    if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn1", "alert('Date of Birth cannot be future date');", true);
                        return;
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn2", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }

            if (txtDepSSN.Visible == true)
            {
                lblDOBStar3.Visible = true;
                if (txtDepSSN.Text != "")
                {
                    if (txtDepDOB.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter Date of Birth');", true);
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDepDOB.Text));
                            if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Date of Birth cannot be future date');", true);
                                return;
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                            return;
                        }
                    }
                    if (!ValidateSSN(txtDepSSN.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid SSN No.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter SSN No.');", true);
                    return;
                }


            }
            if (txtDepDoB1.Text != "" && txtDepDoB1.Visible == true)
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDepDoB1.Text));
                    if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn1", "alert('Date of Birth cannot be future date');", true);
                        return;
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn2", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                    return;
                }
            }
            if (txtDepSSNI.Visible == true)
            {

                if (txtDepSSNI.Text != "")
                {
                    if (txtDepDoB1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter Date of Birth');", true);
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDepDoB1.Text));
                            if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Date of Birth cannot be future date');", true);
                                return;
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter valid Date of Birth" + UDFLib.DateFormatMessage() + "');", true);
                            return;
                        }
                    }
                    if (!ValidateSSN(txtDepSSNI.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn2", "alert('Enter valid SSN No.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ssn", "alert('Enter SSN No.');", true);
                    return;
                }

            }



            string js = "";
            int result = 0;
            try
            {

                if (UDFLib.ConvertToInteger(hdnNOKID.Value) > 0)
                {
                    if (add == 0)
                        result = objBLLCrew.UPDATE_Crew_DependentDetails(add, int.Parse(hdnNOKID.Value), txtDepFirstName.Text.Trim(), txtDepSurname.Text.Trim(), ddlDepRelationship.SelectedValue, txtDepAdd1.Text.Trim(), txtDepAdd2.Text.Trim(), txtDepAdd1.Text.Trim(), txtDepSSN.Text.Trim(), ConvertToDate(txtDepDOB.Text), txtDepCity.Text.Trim(), txtDepState.Text.Trim(), ddlDepCountry.SelectedValue, txtDepZip.Text.Trim(), txtDepPhone.Text.Trim(), IsBeneficiary, 0, GetSessionUserID());
                    else
                        result = objBLLCrew.UPDATE_Crew_DependentDetails(add, int.Parse(hdnNOKID.Value), txtDepFirstName1.Text.Trim(), txtDepSurname1.Text.Trim(), ddlDepRelationShip1.SelectedValue, txtDepAdd1.Text.Trim(), txtDepAdd2.Text.Trim(), txtDepInternational.Text.Trim(), txtDepSSNI.Text.Trim(), ConvertToDate(txtDepDoB1.Text), txtDepCity.Text.Trim(), txtDepState.Text.Trim(), "", txtDepZip.Text.Trim(), txtDepPhone1.Text.Trim(), IsBeneficiary1, 0, GetSessionUserID());

                    if (result > 0)
                        lblMsg.Text = "Dependent details updated.";
                    else
                        lblMsg.Text = "Unable to update dependent details.";

                    js = "parent.GetCrewNOKAndDependents(" + Request.QueryString["CrewID"].ToString() + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsGetCrewNOKAndDependents", js, true);

                }
                else
                {
                    if (add == 0)
                        result = objBLLCrew.INS_Crew_DependentDetails(add, UDFLib.ConvertToInteger(Request.QueryString["CREWID"]), txtDepFirstName.Text.Trim(), txtDepSurname.Text.Trim(), ddlDepRelationship.SelectedValue, txtDepAdd1.Text.Trim(), txtDepAdd2.Text.Trim(), "", txtDepSSN.Text.Trim(), ConvertToDate(txtDepDOB.Text), txtDepCity.Text.Trim(), txtDepState.Text.Trim(), ddlDepCountry.SelectedValue, txtDepZip.Text.Trim(), txtDepPhone.Text.Trim(), IsBeneficiary, 0, GetSessionUserID());
                    else
                        result = objBLLCrew.INS_Crew_DependentDetails(add, UDFLib.ConvertToInteger(Request.QueryString["CREWID"]), txtDepFirstName1.Text.Trim(), txtDepSurname1.Text.Trim(), ddlDepRelationShip1.SelectedValue, "", "", txtDepInternational.Text.Trim(), txtDepSSNI.Text.Trim(), ConvertToDate(txtDepDoB1.Text), "", "", "", txtDepZip.Text.Trim(), txtDepPhone1.Text.Trim(), IsBeneficiary1, 0, GetSessionUserID());

                    if (result > 0)
                        lblMsg.Text = "Dependent details added.";
                    else
                        lblMsg.Text = "Unable to add dependent details.";

                    js = "parent.GetCrewNOKAndDependents(" + Request.QueryString["CrewID"].ToString() + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsGetCrewNOKAndDependents", js, true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Unable to save dependent details !! " + ex.Message;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        string jss = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jss, true);
    }
    protected void btnSaveAndCloseDependents_Click(object sender, EventArgs e)
    {
        string jas = "parent.hideModal('dvPopupFrame');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jas, true);

    }

    public void bindCountry()
    {
        try
        {
            DataTable dt = objCountry.Get_CountryList();
            ddlCountry.DataSource = dt;
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataTextField = "COUNTRY";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("-SELECT-", "0"));

            ddlDepCountry.DataSource = dt;
            ddlDepCountry.DataValueField = "ID";
            ddlDepCountry.DataTextField = "COUNTRY";
            ddlDepCountry.DataBind();
            ddlDepCountry.Items.Insert(0, new ListItem("-SELECT-", "0"));

            dt.DefaultView.RowFilter = "";
            dt.DefaultView.RowFilter = "ISO_Code='US'";
            if (dt.DefaultView.Count > 0)
                hdnUSCountryID.Value = Convert.ToString(dt.DefaultView[0]["ID"]);
        }
        catch (Exception ex)
        {
            ddlDepCountry.DataSource = ddlCountry.DataSource = null;
            ddlDepCountry.DataBind();
            ddlCountry.DataBind();
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_Dependents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label IsNOK = (Label)e.Row.FindControl("lblIsNOK");
                if (IsNOK.Text == "1")
                {
                    ImageButton img1 = (ImageButton)e.Row.FindControl("LinkButton1del");
                    ImageButton img2 = (ImageButton)e.Row.FindControl("LinkButton2");
                    img1.Visible = false;
                    img2.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void CheckMandatory()
    {
        lbl1.Visible = true;
        lbl2.Visible = true;
        lbl3.Visible = true;
        lbl4.Visible = true;
        lbl6.Visible = true;
        lbl7.Visible = true;
        lbl8.Visible = true;
        lbl11.Visible = true;
        lbl12.Visible = true;
        lbl13.Visible = true;
        lbl14.Visible = true;
        lbl17.Visible = true;
        lbl18.Visible = true;
        lbl19.Visible = true;
    }

    protected void UnCheckMandatory()
    {
        lbl1.Visible = false;
        lbl2.Visible = false;
        lbl3.Visible = false;
        lbl4.Visible = false;
        lbl6.Visible = false;
        lbl7.Visible = false;
        lbl8.Visible = false;
        lbl11.Visible = false;
        lbl12.Visible = false;
        lbl13.Visible = false;
        lbl14.Visible = false;
        lbl17.Visible = false;
        lbl18.Visible = false;
        lbl19.Visible = false;
        txtFirstName1.BackColor = System.Drawing.Color.White;
        txtSurname1.BackColor = System.Drawing.Color.White;
        ddlRelationShip1.BackColor = System.Drawing.Color.White;
        txtPhone1.BackColor = System.Drawing.Color.White;
        txtAddressInternational.BackColor = System.Drawing.Color.White;

        txtFirstName.BackColor = System.Drawing.Color.White;
        txtSurname.BackColor = System.Drawing.Color.White;
        ddlNOKRelationship.BackColor = System.Drawing.Color.White;
        txtFirstName1.BackColor = System.Drawing.Color.White;
        txtAddress1.BackColor = System.Drawing.Color.White;
        txtAddress2.BackColor = System.Drawing.Color.White;
        txtCity.BackColor = System.Drawing.Color.White;
        txtState.BackColor = System.Drawing.Color.White;
        ddlCountry.BackColor = System.Drawing.Color.White;
        txtZip.BackColor = System.Drawing.Color.White;
        txtPhone.BackColor = System.Drawing.Color.White;
    }

    protected void ConfidentialityCheck()
    {
        if (objAdmin.CRW_GetCDConfiguration("SSN").Tables.Count > 0)
        {
            DataTable dtSSN = objAdmin.CRW_GetCDConfiguration("SSN").Tables[0];

            if (dtSSN.Rows.Count > 0)
            {
                if (dtSSN.Rows[0]["Display"].ToString() == "True")
                {
                    tdlblSSN1.Visible = true;
                    tdlblSSN2.Visible = true;
                    tdtxtSSN1.Visible = true;
                    tdtxtSSN2.Visible = true;
                    tdblank.Visible = true;
                    tdSSNI1.Visible = true;
                    tdSSNI2.Visible = true;
                    tdlblSSNI1.Visible = true;
                    tdlblSSNI2.Visible = true;
                    tdblank2.Visible = true;
                    tdDepSSN1.Visible = true;
                    tdDepSSN2.Visible = true;
                    tdDepSSN3.Visible = true;
                    tdDepSSN4.Visible = true;
                    lblDOBstar.Visible = true;
                    lblDobStar2.Visible = true;
                    lblDOBStar3.Visible = true;
                    lblDOBStar4.Visible = true;
                    txtDepDOB.CssClass = "required";
                    txtDepDoB1.CssClass = "required";
                    txtDOB.CssClass = "required";
                    txtDOB1.CssClass = "required";
                }
                else
                {
                    tdlblSSN1.Visible = false;
                    tdlblSSN2.Visible = false;
                    tdtxtSSN1.Visible = false;
                    tdblank.Visible = false;
                    tdtxtSSN2.Visible = false;
                    tdSSNI1.Visible = false;
                    tdSSNI2.Visible = false;
                    tdlblSSNI1.Visible = false;
                    tdlblSSNI2.Visible = false;
                    tdblank2.Visible = false;
                    tdDepSSN1.Visible = false;
                    tdDepSSN2.Visible = false;
                    tdDepSSN3.Visible = false;
                    tdDepSSN4.Visible = false;
                    lblDOBstar.Visible = false;
                    lblDobStar2.Visible = false;
                    lblDOBStar3.Visible = false;
                    lblDOBStar4.Visible = false;
                }

            }
        }
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
                if (ssn.Split('-')[0] == "000" || ssn.Split('-')[0] == "900" || ssn.Split('-')[0] == "999" || ssn.Split('-')[0] == "666")
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
                if (Convert.ToInt32(ssn.Split('-')[0]) >= 900 && Convert.ToInt32(ssn.Split('-')[0]) <= 999)
                {
                    return false;
                }
                return true;
            }
        }

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
}