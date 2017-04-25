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

public partial class Crew_CrewDetails_BankAcc : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Crew_Admin objCrew = new BLL_Crew_Admin();
    UserAccess objUA = new UserAccess();
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
                    int AccID = UDFLib.ConvertToInteger(Request.QueryString["AccID"]);
                    string Mode = Request.QueryString["Mode"];

                    HiddenField_CrewID.Value = CrewID.ToString();
                    HiddenField_AccID.Value = AccID.ToString();

                    DataSet dss = objCrew.CRW_CD_GetConfidentialDetails(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]));

                    if (dss != null)
                    {
                        DataTable dt = dss.Tables[0];
                        if (rdoOptAllotment.Items.FindByValue(dt.Rows[0]["Allotment_AccType"].ToString()) != null)
                        {
                            rdoOptAllotment.SelectedValue = dt.Rows[0]["Allotment_AccType"].ToString() != "" ? dt.Rows[0]["Allotment_AccType"].ToString() : "BOTH";
                        }
                        else
                        {
                            rdoOptAllotment.SelectedValue = "BOTH";
                        }
                    }
                    lblAllotment.Text = rdoOptAllotment.SelectedItem.Text;
                    if (Mode == "EDIT")
                    {
                        pnlEditAccount.Visible = true;
                        DetailsView_NewAcc.DefaultMode = DetailsViewMode.Edit;
                        Load_CrewBankAccDetails(AccID);
                        ((CheckBox)DetailsView_NewAcc.FindControl("chkMOAcc")).Enabled = false;
                        ((DropDownList)DetailsView_NewAcc.FindControl("ddlMO_Account")).Enabled = false;

                        if (((CheckBox)DetailsView_NewAcc.FindControl("chkMOAcc")).Checked == true)
                            ReadOnly_CrewBankAccDetails();
                    }
                    else if (Mode == "INSERT")
                    {
                        pnlEditAccount.Visible = true;
                        DetailsView_NewAcc.DefaultMode = DetailsViewMode.Insert;
                        Load_CrewBankAccDetails(AccID);
                    }
                    else
                    {
                        if (objUA.View == 1)
                        {
                            pnlViewAccounts.Visible = true;
                            Load_CrewBankAcc(CrewID);
                        }
                    }
                    if (Mode == "EDITAllotment")
                    {
                        tblEditMode.Visible = true;
                        tblDisplayMode.Visible = false;
                        GridView_BankAccounts.Visible = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        //EditCrewBankAccAllotment
        lnkEditAllotment.OnClientClick = "EditCrewBankAccAllotment(" + UDFLib.ConvertToInteger(Request.QueryString["CrewID"]).ToString() + "); return false;";
    }
    protected void ReadOnly_CrewBankAccDetails()
    {

        DisableControls();

    }
    protected void Load_CrewBankAcc(int CrewID)
    {
        DataTable dt = SMS.Business.PortageBill.BLL_PortageBill.Get_Crew_BankAccList(CrewID);
        GridView_BankAccounts.DataSource = dt;
        GridView_BankAccounts.DataBind();
    }
    protected void Load_CrewBankAccDetails(int AccID)
    {
        try
        {
            DataTable dt = SMS.Business.PortageBill.BLL_PortageBill.Get_Crew_BankAccDetails(AccID);
            DetailsView_NewAcc.DataSource = dt;
            DetailsView_NewAcc.DataBind();
        }
        catch (Exception){}
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlViewAccounts.Visible = false;
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
        }
        if (objUA.Add == 0)
        {
            pnlEditAccount.Visible = false;
            DetailsView_NewAcc.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            pnlEditAccount.Visible = false;
            GridView_BankAccounts.Columns[GridView_BankAccounts.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_BankAccounts.Columns[GridView_BankAccounts.Columns.Count - 2].Visible = false;
        }
        if (objUA.Approve == 0)
        {
            DetailsView_NewAcc.Fields[DetailsView_NewAcc.Fields.Count - 7].Visible = false;
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void DetailsView_NewAcc_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        string js = "prent.GetCrewBankAcc(" + HiddenField_CrewID.Value.ToString() + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
    protected void DetailsView_NewAcc_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {

        if (e.NewValues["Beneficiary"] == null || e.NewValues["Acc_NO"] == null || e.NewValues["Bank_Name"] == null || e.NewValues["Bank_Address"] == null || e.NewValues["SwiftCode"] == null)
        {
            string js = "parent.ShowNotification('Alert','Unable to save as some of the fields are left blank',true)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            if (((CheckBox)DetailsView_NewAcc.FindControl("chkMOAcc")).Checked == true)
                DisableControls();
        }
        else
        {

            int AccID = UDFLib.ConvertToInteger(HiddenField_AccID.Value);
            int CrewID = UDFLib.ConvertToInteger(HiddenField_CrewID.Value);
            string Beneficiary = e.NewValues["Beneficiary"].ToString();
            string Acc_No = e.NewValues["Acc_NO"].ToString();
            string Bank_Name = e.NewValues["Bank_Name"].ToString();
            string Bank_Address = e.NewValues["Bank_Address"].ToString();
            int Default_Acc = UDFLib.ConvertStringToNull(e.NewValues["Default_Acc"]) == null ? 0 : UDFLib.ConvertStringToNull(e.NewValues["Default_Acc"]) == "True" ? 1 : 0;
            int Verified = UDFLib.ConvertStringToNull(e.NewValues["Verified"]) == null ? 0 : UDFLib.ConvertStringToNull(e.NewValues["Verified"]) == "True" ? 1 : 0;
            string SwiftCode = e.NewValues["SwiftCode"].ToString();
            int Modified_By = UDFLib.ConvertToInteger(Session["USERID"]);
            int MOBank_ID = UDFLib.ConvertToInteger(e.NewValues["MOBank_ID"].ToString());

            string Bank_Code = Convert.ToString(e.NewValues["BANK_CODE"]);
            string Branch_Code = Convert.ToString(e.NewValues["BRANCH_CODE"]);
            int Account_Curr = UDFLib.ConvertToInteger(e.NewValues["ACCOUNT_CURR"].ToString());

            int Res = SMS.Business.PortageBill.BLL_PortageBill.ACC_Update_BankAccounts(AccID, CrewID, Beneficiary, Acc_No, Bank_Name, Bank_Address, Default_Acc, Verified, Modified_By, SwiftCode, Bank_Code, Branch_Code, Account_Curr, MOBank_ID);

            if (Res > 0)
            {
                if (e.CommandArgument.ToString() == "SaveAndClose")
                {
                    string js1 = "parent.hideModal('dvPopupFrame'); parent.GetCrewBankAcc(" + HiddenField_CrewID.Value.ToString() + ");parent.ShowNotification('Account Details Updated',true)";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js1, true);
                }
                else
                {
                    lblMsg.Text = "Account Details Updated!!";
                    string js2 = "parent.GetCrewBankAcc(" + HiddenField_CrewID.Value.ToString() + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js2, true);
                }
            }
        }
    }
    protected void DetailsView_NewAcc_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        if (e.Values["Beneficiary"] == null || e.Values["Acc_NO"] == null || e.Values["Bank_Name"] == null || e.Values["Bank_Address"] == null || e.Values["SwiftCode"] == null)
        {
            string js = "parent.ShowNotification('Alert','Unable to save as some of the fields are left blank',true)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            if (((CheckBox)DetailsView_NewAcc.FindControl("chkMOAcc")).Checked == true)
                DisableControls();
        }
        else
        {
            int AccID = UDFLib.ConvertToInteger(HiddenField_AccID.Value);
            int CrewID = UDFLib.ConvertToInteger(HiddenField_CrewID.Value);
            string Beneficiary = e.Values["Beneficiary"].ToString();
            string Acc_No = e.Values["Acc_NO"].ToString();
            string Bank_Name = e.Values["Bank_Name"].ToString();
            string Bank_Address = e.Values["Bank_Address"].ToString();
            int Default_Acc = UDFLib.ConvertStringToNull(e.Values["Default_Acc"]) == null ? 0 : UDFLib.ConvertStringToNull(e.Values["Default_Acc"]) == "True" ? 1 : 0;
            int Verified = UDFLib.ConvertStringToNull(e.Values["Verified"]) == null ? 0 : UDFLib.ConvertStringToNull(e.Values["Verified"]) == "True" ? 1 : 0;
            string SwiftCode = e.Values["SwiftCode"].ToString();
            int Modified_By = UDFLib.ConvertToInteger(Session["USERID"]);

            string Bank_Code = e.Values["BANK_CODE"] == null ? "" : e.Values["BANK_CODE"].ToString();
            string Branch_Code = e.Values["BRANCH_CODE"] == null ? "" : e.Values["BRANCH_CODE"].ToString();
            int Account_Curr = UDFLib.ConvertToInteger(e.Values["ACCOUNT_CURR"].ToString());
            int MOBank_ID = UDFLib.ConvertToInteger(e.Values["MOBank_ID"].ToString());

            int Res = SMS.Business.PortageBill.BLL_PortageBill.ACC_Insert_BankAccounts(CrewID, Beneficiary, Acc_No, Bank_Name, Bank_Address, Default_Acc, Verified, Modified_By, SwiftCode, Bank_Code, Branch_Code, Account_Curr, MOBank_ID);
            if (e.CommandArgument.ToString() == "SaveAndClose")
            {
                string js2 = "parent.hideModal('dvPopupFrame'); parent.GetCrewBankAcc(" + HiddenField_CrewID.Value.ToString() + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js2, true);
            }
            else
            {
                lblMsg.Text = "New Account Details Updated!!";
                string js3 = "parent.GetCrewBankAcc(" + HiddenField_CrewID.Value.ToString() + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js3, true);
            }
        }

    }
    protected void chkMOAcc_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked == true)
        {
            ((DropDownList)DetailsView_NewAcc.FindControl("ddlMO_Account")).Enabled = true;
            DisableControls();
        }
        else
        {
            ((DropDownList)DetailsView_NewAcc.FindControl("ddlMO_Account")).Enabled = false;
            ((DropDownList)DetailsView_NewAcc.FindControl("ddlMO_Account")).SelectedValue = "0";
            ((TextBox)DetailsView_NewAcc.FindControl("txtacc")).ReadOnly = false;
            ((TextBox)DetailsView_NewAcc.FindControl("txtbname")).ReadOnly = false;
            ((TextBox)DetailsView_NewAcc.FindControl("txtSwiftCode")).ReadOnly = false;
            ((TextBox)DetailsView_NewAcc.FindControl("txtbadd")).ReadOnly = false;
            ((TextBox)DetailsView_NewAcc.FindControl("txtBeneficiary")).ReadOnly = false;
            ((TextBox)DetailsView_NewAcc.FindControl("txtBank_Code")).ReadOnly = false;
            ((TextBox)DetailsView_NewAcc.FindControl("txtBRANCH_CODE")).ReadOnly = false;
            ((DropDownList)DetailsView_NewAcc.FindControl("ddlACCOUNT_CURR")).Enabled = true;
        }
        ClearControls();
    }
    protected void ddlMO_Account_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMOAccount = (DropDownList)sender;
        if (ddlMOAccount.SelectedValue != "0")
        {

            DataTable dt = objCrew.Get_MOBankAccount_Details(Convert.ToInt32(ddlMOAccount.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                ((TextBox)DetailsView_NewAcc.FindControl("txtacc")).Text = dt.Rows[0]["Acc_NO"].ToString();
                ((TextBox)DetailsView_NewAcc.FindControl("txtbname")).Text = dt.Rows[0]["Bank_Name"].ToString();
                ((TextBox)DetailsView_NewAcc.FindControl("txtSwiftCode")).Text = dt.Rows[0]["SwiftCode"].ToString();
                ((TextBox)DetailsView_NewAcc.FindControl("txtbadd")).Text = dt.Rows[0]["Bank_Address"].ToString();
                ((TextBox)DetailsView_NewAcc.FindControl("txtBeneficiary")).Text = dt.Rows[0]["Beneficiary"].ToString();
                ((TextBox)DetailsView_NewAcc.FindControl("txtBank_Code")).Text = dt.Rows[0]["BANK_CODE"].ToString();
                ((TextBox)DetailsView_NewAcc.FindControl("txtBRANCH_CODE")).Text = dt.Rows[0]["BRANCH_CODE"].ToString();

                if (((DropDownList)DetailsView_NewAcc.FindControl("ddlACCOUNT_CURR")).Items.FindByValue(dt.Rows[0]["ACCOUNT_CURR"].ToString()) != null)
                    ((DropDownList)DetailsView_NewAcc.FindControl("ddlACCOUNT_CURR")).SelectedValue = dt.Rows[0]["ACCOUNT_CURR"].ToString() != "" ? dt.Rows[0]["ACCOUNT_CURR"].ToString() : "0";
                else
                    ((DropDownList)DetailsView_NewAcc.FindControl("ddlACCOUNT_CURR")).SelectedValue = "0";
            }
        }
        else
        {
            ClearControls();
        }
        DisableControls();
    }
    protected void DisableControls()
    {
        ((TextBox)DetailsView_NewAcc.FindControl("txtacc")).ReadOnly = true;
        ((TextBox)DetailsView_NewAcc.FindControl("txtbname")).ReadOnly = true;
        ((TextBox)DetailsView_NewAcc.FindControl("txtSwiftCode")).ReadOnly = true;
        ((TextBox)DetailsView_NewAcc.FindControl("txtbadd")).ReadOnly = true;
        ((TextBox)DetailsView_NewAcc.FindControl("txtBeneficiary")).ReadOnly = true;
        ((TextBox)DetailsView_NewAcc.FindControl("txtBank_Code")).ReadOnly = true;
        ((TextBox)DetailsView_NewAcc.FindControl("txtBRANCH_CODE")).ReadOnly = true;
        ((DropDownList)DetailsView_NewAcc.FindControl("ddlACCOUNT_CURR")).Enabled = false;
    }
    protected void ClearControls()
    {
        ((TextBox)DetailsView_NewAcc.FindControl("txtacc")).Text = "";
        ((TextBox)DetailsView_NewAcc.FindControl("txtbname")).Text = "";
        ((TextBox)DetailsView_NewAcc.FindControl("txtSwiftCode")).Text = "";
        ((TextBox)DetailsView_NewAcc.FindControl("txtbadd")).Text = "";
        ((TextBox)DetailsView_NewAcc.FindControl("txtBeneficiary")).Text = "";
        ((TextBox)DetailsView_NewAcc.FindControl("txtBank_Code")).Text = "";
        ((TextBox)DetailsView_NewAcc.FindControl("txtBRANCH_CODE")).Text = "";
        ((DropDownList)DetailsView_NewAcc.FindControl("ddlACCOUNT_CURR")).SelectedValue = "0";
        ((CheckBox)DetailsView_NewAcc.FindControl("CheckBox1")).Checked = false;
    }

    protected void btnSaveAllotment_Click(object sender, EventArgs e)
    {
        try
        {
            objCrew.CRW_CD_UPD_BankAllotment(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]), rdoOptAllotment.SelectedValue.ToString());
            string js2 = "parent.hideModal('dvPopupFrame');parent.GetCrewBankAcc(" + HiddenField_CrewID.Value.ToString() + ");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js2, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}