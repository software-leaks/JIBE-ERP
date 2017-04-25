using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SMS.Business.PortageBill;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Xml;
using System.IO;
using System.Drawing;


public partial class Account_Portage_Bill_Crew_AddWages : System.Web.UI.Page
{
    private int VoyID = 0;
    private int CrewID = 0;
    private int Vessel_ID = 0;
    int RankScaleConsidered = 0, NationalityConsidered = 0, VesselFlagConsidered = 0;
    int RankId = 0;
    int WageContractId = 0;
    int Staff_Nationality = 0;
    DataView dataview;
    protected decimal TotalAmount;
    protected decimal TotalAmountDeducted;
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Contract objBLLCrewContract = new BLL_Crew_Contract();
    UserAccess objUA = new UserAccess();
    string FooterText = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender5.Format = UDFLib.GetDateFormat();

        if (Request.QueryString["VoyID"] != null)
        {
            VoyID = System.Convert.ToInt32(Request.QueryString["VoyID"].ToString().Trim());
        }
        if (Request.QueryString["CrewID"] != null)
        {
            CrewID = System.Convert.ToInt32(Request.QueryString["CrewID"].ToString().Trim());
        }

        lblMessage.Text = "";
        DataTable dtWages = objCrewAdmin.GetWagesSettings();
        if (dtWages != null && dtWages.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == true)
            {
                RankScaleConsidered = 1;
                lblRankScale.Visible = true;
                ddlRankScale.Visible = true;
            }
            else
            {
                lblRankScale.Visible = false;
                ddlRankScale.Visible = false;
            }
            if (Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]) == true)
            {
                NationalityConsidered = 1;
            }
            if (Convert.ToBoolean(dtWages.Rows[0]["VesselFlagConsidered"]) == true)
            {
                VesselFlagConsidered = 1;
            }
        }
        DataTable dtVoy = objCrew.Get_CrewVoyages(CrewID, VoyID);

        if (dtVoy.Rows.Count > 0)
        {
            if (dtVoy.Rows[0]["Joining_Rank"].ToString() != "")
            {
                RankId = int.Parse(dtVoy.Rows[0]["Joining_Rank"].ToString());
            }
        }
        DataTable dt = objCrew.Get_CrewPersonalDetailsByID1(CrewID);
        if (dt.Rows.Count > 0)
        {
            Staff_Nationality = int.Parse(dt.Rows[0]["Staff_Nationality"].ToString());
        }

        if (!IsPostBack)
        {
            try
            {
                UserAccessValidation();
                Load_CurrencyList(ddlCurrencyType);
                DataTable dtRankScale;
                if (NationalityConsidered == 1)
                    dtRankScale = objCrewAdmin.Get_RankScaleListForWages(RankId, Staff_Nationality);
                else
                    dtRankScale = objCrewAdmin.Get_RankScaleListForWages(RankId, 0);

                ddlRankScale.DataSource = dtRankScale;
                ddlRankScale.DataTextField = "RankScaleName";
                ddlRankScale.DataValueField = "ID";
                ddlRankScale.DataBind();
                ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));
                txtRankSeniorityRemarks.Enabled = false;
                btnUpdateSeniority.Enabled = false;

                int wagests = BLL_PortageBill.CRW_GET_Wage_Status(CrewID, VoyID);

                if (dt.Rows.Count > 0)
                {
                    lblStaffCode.Text = dt.Rows[0]["staff_code"].ToString();
                    lblStaffName.Text = dt.Rows[0]["staff_fullname"].ToString();
                }

                if (wagests == 0)
                {
                    if (dtVoy.Rows.Count > 0)
                    {
                        if (dtVoy.Rows[0]["Joining_Date"].ToString() != "")
                        {
                            txteffdt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtVoy.Rows[0]["Joining_Date"].ToString()));
                        }

                    }
                    int iAutoInsert = BLL_PortageBill.AutoPopulate_CrewWages(VoyID, GetSessionUserID());
                    if (iAutoInsert == 1)
                    {
                        View_CrewWages();
                        lblMessage.Text = "Wages now populated from RANK WAGE CONTRACT. Please verify and edit if required.";
                    }
                    else
                    {
                        DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID, GetSessionUserID());

                        pnlAddWages.Visible = true;
                        pnlViewWages.Visible = false;

                        lblincris.Visible = false;
                        chkbinc.Visible = false;

                        GridViewaddwages.DataSource = ds.Tables[1];
                        GridViewaddwages.DataBind();

                        btnEditWages.Visible = false;
                    }
                }
                else
                {
                    View_CrewWages();

                    pnlAddWages.Visible = false;
                    pnlViewWages.Visible = true;

                    btnEditWages.Visible = true;
                }

                if (dtVoy.Rows.Count > 0)
                {
                    Vessel_ID = UDFLib.ConvertToInteger(dtVoy.Rows[0]["vessel_id"].ToString());

                    if (dtVoy.Rows[0]["sign_off_date"].ToString() != "")
                    {
                        btnEditWages.Visible = false;
                        btnAddWages.Visible = false;

                        lblMessage.Text = "Wages can not be edited after crew staff is signed off";

                        pnlAddWages.Visible = false;
                        pnlViewWages.Visible = true;
                    }
                    // -- load seriority rates --
                    hdnCurrentSeniority.Value = dtVoy.Rows[0]["RankSeniorityYear"].ToString();
                    hdnSelectedSeniority.Value = dtVoy.Rows[0]["RankSeniorityYear"].ToString();
                    Load_Seniority_Rates(UDFLib.ConvertToInteger(dtVoy.Rows[0]["joining_rank"]));
                }

                //If contract is signed by office, then wages can not be edited
                System.Data.DataTable dtAgreement = objCrew.Get_CrewAgreementStatus(VoyID, GetSessionUserID());
                DataRow[] dr = dtAgreement.Select("StepID >= 4");
                if (dr.Length > 0)
                {
                    btnEditWages.Visible = false;
                    //btnAddWages.Visible = false;
                    //btnsave.Visible = false;
                    lblMessage.Text = "Contract is already signed by office";
                }

            }


            catch (Exception ex)
            {
                //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

            }

        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();

        string PageURL = UDFLib.GetPageURL(Request.Path);

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnsave.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            btnEditWages.Enabled = false;
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
        //-- VESSEL OWNER -- //
        else if (Session["UTYPE"].ToString() == "VESSEL OWNER")
        {

        }
        else//--- CREW TEAM LOGIN--
        {
        }
    }

    protected void View_CrewWages()
    {
        try
        {
            DataSet ds = BLL_PortageBill.Get_CrewWagesByVoyage(CrewID, VoyID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RankId = int.Parse(ds.Tables[0].Rows[0]["Joining_Rank"].ToString());

                // DropDownList ddlRankScale = (DropDownList)e.Row.FindControl("ddlRankScale");
                if (RankId > 0 && RankScaleConsidered == 1)
                {
                    int totalCount = ds.Tables[0].Rows.Count;
                    int RankScaleId = int.Parse(ds.Tables[0].Rows[totalCount - 1]["RankScaleId"].ToString());

                    if (ds.Tables[0].Rows[0]["RankScaleId"] != null && ds.Tables[0].Rows[0]["RankScaleId"].ToString() != "")
                        RankScaleId = int.Parse(ds.Tables[0].Rows[0]["RankScaleId"].ToString());

                    DataTable dtRankScale;
                    if (NationalityConsidered == 1)
                        dtRankScale = objCrewAdmin.Get_RankScaleListForWages(RankId, Staff_Nationality);
                    else
                        dtRankScale = objCrewAdmin.Get_RankScaleListForWages(RankId, 0);

                    ddlRankScale.DataSource = dtRankScale;
                    ddlRankScale.DataTextField = "RankScaleName";
                    ddlRankScale.DataValueField = "ID";
                    ddlRankScale.DataBind();
                    ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));

                    if (RankScaleId == 0 && ddlRankScale.SelectedValue != null)
                        RankScaleId = int.Parse(ddlRankScale.SelectedValue.ToString());

                    ddlRankScale.SelectedValue = RankScaleId.ToString();
                    ViewState["CurrentRankScale"] = RankScaleId;
                }



                ds.Relations.Add(new DataRelation("Parent", ds.Tables[0].Columns["Effective_Date"], ds.Tables[1].Columns["Effective_Date"]));
                ds.Tables[1].TableName = "Child";

                TotalAmount = 0;
                TotalAmountDeducted = 0;
                rpt1.DataSource = ds;
                rpt1.DataBind();
            }
            DataTable dtSideLetter = objCrew.Get_SideLetter_ForVoyage(VoyID, CrewID, GetSessionUserID());
            if (dtSideLetter.Rows.Count > 0)
            {
                lblSideLetterAmount.Text = dtSideLetter.Rows[0]["Amount"].ToString();
            }
        }
        catch (Exception ex)
        {
            //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }
    protected void Load_CurrencyList(DropDownList ddlCurrency_)
    {
        BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
        ddlCurrency_.DataTextField = "Currency_Code";
        ddlCurrency_.DataValueField = "Currency_ID";
        ddlCurrency_.DataSource = objBLLCurrency.Get_CurrencyList();
        ddlCurrency_.DataBind();
        ddlCurrency_.SelectedValue = "5";
    }

    protected void rpt1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        DataRowView dv = e.Item.DataItem as DataRowView;
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {
                nestedRepeater.DataSource = dv.CreateChildView("Parent");
                nestedRepeater.DataBind();
            }
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var col = e.Item.FindControl("lblRankScale");
            var col1 = e.Item.FindControl("txtRankScale");

            if (RankScaleConsidered == 0)
            {
                col.Visible = false;
                col1.Visible = false;
            }
            else
            {
                col.Visible = true;
                col1.Visible = true;
            }
        }

    }

    protected void rpt2_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblAmt = (Label)e.Item.FindControl("lblAmt");
            Label lblEarningDeduction = (Label)e.Item.FindControl("lblEarningDeduction");
            if (lblAmt != null && lblEarningDeduction.Text == "Earning")
            {
                decimal Amt = UDFLib.ConvertToDecimal(lblAmt.Text);
                TotalAmount += Amt;
            }
            if (lblAmt != null && lblEarningDeduction.Text == "Deduction")
            {
                decimal Amt1 = UDFLib.ConvertToDecimal(lblAmt.Text);
                TotalAmountDeducted += Amt1;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalAmt = (Label)e.Item.FindControl("lblTotalAmt");
            if (lblTotalAmt != null)
            {
                TotalAmount = TotalAmount - TotalAmountDeducted;
                lblTotalAmt.Text = TotalAmount.ToString("0.00");
            }
            TotalAmountDeducted = 0;
            TotalAmount = 0;
        }

    }

    protected void rptContractWages_ItemCreated(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalAmt = (Label)e.Item.FindControl("lblTotalAmt");
            if (lblTotalAmt != null)
            {
                TotalAmount = this.calculateTotals();
                lblTotalAmt.Text = TotalAmount.ToString("0.00");

                //Label lblCurrency = (Label)e.Item.FindControl("lblCurrency");
                //lblCurrency.Text = "USD";

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
    protected void btnAddWages_Click(object sender, EventArgs e)
    {
        lblAddEditWages.Text = "Add Wages:";
        ddlRankScale.Enabled = true;
        int wagests = BLL_PortageBill.CRW_GET_Wage_Status(CrewID, VoyID);

        //Load_CurrencyList(ddlCurrencyType);

        pnlAddWages.Visible = true;
        pnlViewWages.Visible = false;


        DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID);
        GridViewaddwages.DataSource = ds.Tables[1];
        GridViewaddwages.DataBind();


        if (RankScaleConsidered == 1)
        {
            DataSet ds1 = BLL_PortageBill.Get_CrewWagesByVoyage(CrewID, VoyID);
            int Contract_Type = 0;
            int RankScaleId = 0;
            if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                RankId = int.Parse(ds1.Tables[0].Rows[0]["Joining_Rank"].ToString());
                Contract_Type = int.Parse(ds1.Tables[0].Rows[0]["Contract_Type"].ToString());
                int totalCount = ds1.Tables[0].Rows.Count;
                RankScaleId = int.Parse(ds1.Tables[0].Rows[totalCount - 1]["RankScaleId"].ToString());
            }
            ddlRankScale.SelectedValue = RankScaleId.ToString();
            WageContractId = 0;
            DataSet ds2 = BLL_PortageBill.Get_Rank_WageContract(RankId, Contract_Type, NationalityConsidered, Staff_Nationality, RankScaleId);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                WageContractId = int.Parse(ds2.Tables[0].Rows[0]["WageContractID"].ToString());
                hdWageContractId.Value = ds2.Tables[0].Rows[0]["WageContractID"].ToString();

            }
        }

        if (wagests == 0)
        {
            DataTable dtVoy = objCrew.Get_CrewVoyages(CrewID, VoyID);
            if (dtVoy.Rows.Count > 0)
            {
                if (dtVoy.Rows[0]["Joining_Date"].ToString() != "")
                {
                    txteffdt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtVoy.Rows[0]["Joining_Date"].ToString()));
                }


            }
            txteffdt.Enabled = true;
            lblPrevEffectiveDate.Visible = false;

            lblincris.Visible = false;
            chkbinc.Visible = false;
        }
        else
        {
            lblPrevEffectiveDate.Text = "Previous Effective Date: " + UDFLib.ConvertUserDateFormat(ds.Tables[1].Rows[0]["effective_date"].ToString());
            txteffdt.Text = "";
            txteffdt.Enabled = true;
            lblPrevEffectiveDate.Visible = true;

            lblincris.Visible = true;
            chkbinc.Visible = true;
        }

    }

    protected void btnEditWages_Click(object sender, EventArgs e)
    {
        try
        {
            lblAddEditWages.Text = "Edit Wages:";
            ddlRankScale.Enabled = false;
            //Load_CurrencyList(ddlCurrencyType);
            DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID);

            pnlAddWages.Visible = true;
            pnlViewWages.Visible = false;

            lblincris.Visible = false;
            chkbinc.Visible = false;

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                if (dr["effective_date"].ToString() != "")
                {
                    txteffdt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["effective_date"]));
                    txteffdt.Enabled = false;
                    break;
                }

            }

            int totalCount = ds.Tables[0].Rows.Count;
            int RankScaleId = 0;
            if (RankScaleConsidered == 1 && ds.Tables[0].Rows.Count > 0 && (ds.Tables[0].Rows[totalCount - 1]["RankScaleId"] != null || ds.Tables[0].Rows[totalCount - 1]["RankScaleId"] != ""))
            {
                RankScaleId = int.Parse(ds.Tables[0].Rows[totalCount - 1]["RankScaleId"].ToString());
                ddlRankScale.SelectedValue = RankScaleId.ToString();
            }
            if (ds.Tables[0].Rows.Count > 0 && (ds.Tables[0].Rows[totalCount - 1]["WagesContractId"] != null || ds.Tables[0].Rows[totalCount - 1]["WagesContractId"] != ""))
            {
                hdWageContractId.Value = ds.Tables[0].Rows[totalCount - 1]["WagesContractId"].ToString();
            }
            lblPrevEffectiveDate.Visible = false;
            GridViewaddwages.DataSource = ds.Tables[1];
            GridViewaddwages.DataBind();
        }
        catch { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        View_CrewWages();
        pnlAddWages.Visible = false;
        pnlViewWages.Visible = true;
    }
    public void Save()
    {
        string sValidationMessage = "Enter Wages to be saved.";
        bool IsValidated = false;
        lblMessage.Text = "";

        try
        {
            UDFLib.ConvertToDate(txteffdt.Text.Trim(), UDFLib.GetDateFormat());
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Enter valid Effective Date" + UDFLib.DateFormatMessage();
            return;
        }

        if (RankScaleConsidered == 1 && ViewState["CurrentRankScale"] == null)
        {
            lblMessage.Text = "Rank Scale is mandatory";
        }
        else
        {
            DataTable dtWagesINS = new DataTable();

            dtWagesINS.Columns.Add("Vessel_ID", typeof(int));
            dtWagesINS.Columns.Add("VoyageID", typeof(int));
            dtWagesINS.Columns.Add("CrewID", typeof(int));
            dtWagesINS.Columns.Add("EntryType", typeof(int));
            dtWagesINS.Columns.Add("SalaryType", typeof(int));
            dtWagesINS.Columns.Add("PayAt", typeof(int));
            dtWagesINS.Columns.Add("Amount", typeof(decimal));
            dtWagesINS.Columns.Add("CURR", typeof(int));
            dtWagesINS.Columns.Add("Updated_BY", typeof(int));
            dtWagesINS.Columns.Add("Effective_Date", typeof(DateTime));
            dtWagesINS.Columns.Add("ID", typeof(int));

            foreach (GridViewRow gr in GridViewaddwages.Rows)
            {
                double amt = 0;

                int ii = Convert.ToInt32(GridViewaddwages.DataKeys[0].Value);

                if (double.TryParse(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text, out amt))
                {
                    if (Convert.ToString(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text) != "")
                    {
                        IsValidated = true;
                        sValidationMessage = "";
                        break;
                    }
                }

            }

            DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID);
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (txteffdt.Text != "")
                {
                    if (ds.Tables[1].Rows[0]["effective_date"].ToString() != "")
                    {
                        if (txteffdt.Enabled == false && UDFLib.ConvertToDate(txteffdt.Text, UDFLib.GetDateFormat()) < DateTime.Parse(ds.Tables[1].Rows[0]["effective_date"].ToString()))
                        {
                            IsValidated = false;
                            sValidationMessage = "Wages can not be saved as the new effective date can not be prior to the previous effective date.";
                        }
                        if (txteffdt.Enabled == true && UDFLib.ConvertToDate(txteffdt.Text, UDFLib.GetDateFormat()) <= DateTime.Parse(ds.Tables[1].Rows[0]["effective_date"].ToString()))
                        {
                            IsValidated = false;
                            sValidationMessage = "Wages can not be saved as the new effective date can not be prior to or equal to the previous effective date.";
                        }
                    }
                }
                else
                {
                    IsValidated = false;
                    sValidationMessage = "Please select effective date";
                }
            }

            if (IsValidated == true)
            {
                int currusd = 0;
                foreach (GridViewRow gr in GridViewaddwages.Rows)
                {
                    double amt = 0;

                    int ii = Convert.ToInt32(GridViewaddwages.DataKeys[0].Value);

                    if (double.TryParse(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text, out amt))
                    {
                        DateTime dt = new DateTime();
                        if (txteffdt.Text != "")
                        {
                            dt = UDFLib.ConvertToDate(txteffdt.Text.Trim(), UDFLib.GetDateFormat());
                        }
                        else
                        {
                            string efdt = "1900/01/01";
                            dt = DateTime.Parse(efdt);
                        }

                        string sentry = ((Label)GridViewaddwages.Rows[gr.RowIndex].FindControl("lblentry_type")).Text;

                        currusd = int.Parse(ddlCurrencyType.SelectedValue);

                        int iSalaryType = Convert.ToInt32(((RadioButtonList)gr.Cells[2].FindControl("SalaryTypes")).SelectedValue);
                        int iPayAt = Convert.ToInt32(((RadioButtonList)gr.Cells[2].FindControl("PayableAT")).SelectedValue);

                        DataRow dr = dtWagesINS.NewRow();
                        dr["Vessel_ID"] = Vessel_ID;
                        dr["VoyageID"] = VoyID;
                        dr["CrewID"] = CrewID;
                        dr["EntryType"] = UDFLib.ConvertToInteger(GridViewaddwages.DataKeys[gr.RowIndex].Value.ToString());
                        dr["SalaryType"] = iSalaryType;
                        dr["PayAt"] = iPayAt;
                        dr["Amount"] = amt;
                        dr["CURR"] = currusd;
                        dr["Updated_BY"] = GetSessionUserID();
                        dr["Effective_Date"] = dt.ToString("dd/MM/yyyy");
                        dr["ID"] = 0;


                        dtWagesINS.Rows.Add(dr);

                    }

                }
                if (hdWageContractId.Value != null && hdWageContractId.Value.ToString() != "")
                    WageContractId = int.Parse(hdWageContractId.Value.ToString());
                BLL_PortageBill.Ins_CrewWages(dtWagesINS, WageContractId);
                if (RankScaleConsidered == 1 && ViewState["CurrentRankScale"] != null)
                {
                    int CurrentRankScale = int.Parse(ViewState["CurrentRankScale"].ToString());
                    if (CurrentRankScale != int.Parse(ddlRankScale.SelectedValue.ToString()))
                    {
                        //Save entry in Log for change in Wage Rank Scale
                        BLL_PortageBill.Ins_CrewChangeWageLog(RankId, int.Parse(ddlRankScale.SelectedValue.ToString()), CrewID, GetSessionUserID());
                    }
                }
                if (hdnSelectedSeniority.Value != hdnCurrentSeniority.Value)
                {
                    int SelectedSeniority = 0;
                    if (hdnSelectedSeniority.Value != null && hdnSelectedSeniority.Value.ToString() != "")
                        SelectedSeniority = int.Parse(hdnSelectedSeniority.Value.ToString());
                    string Remarks = txtRankSeniorityRemarks.Text.Trim();
                    BLL_PortageBill.Update_CrewRankSeniorityYear(CrewID, RankId, SelectedSeniority, 0, VoyID, Remarks, UDFLib.ConvertToDate(txteffdt.Text.Trim()), Convert.ToInt32(Session["userid"]));
                }

                decimal Amount = UDFLib.ConvertToDecimal(txtSideLetterAmount.Text);
                if (txtSideLetterAmount.Text != "")
                {
                    BLL_PortageBill.Ins_Crew_SideLetter(VoyID, Amount, GetSessionUserID());
                }

                GridView_Voyages.DataBind();

                string retText = Generate_Crew_Agreement(CrewID, VoyID);

                lblMessage.Text = "Salary has been updated." + retText;

                View_CrewWages();
                pnlAddWages.Visible = false;
                pnlViewWages.Visible = true;

                int wagests = BLL_PortageBill.CRW_GET_Wage_Status(CrewID, VoyID);
                if (wagests > 0)
                {
                    btnEditWages.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = sValidationMessage;
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        Save();
    }

    protected void chkbinc_CheckedChanged(object sender, EventArgs e)
    {
        int wagests = BLL_PortageBill.CRW_GET_Wage_Status(CrewID, VoyID);
        if (chkbinc.Checked == true)
        {
            //RequiredFieldValidator1.Visible = true;
            if (wagests != 0)
            {
                GridViewaddwages.Enabled = true;
                btncancel.Enabled = true;
                btnsave.Enabled = true;
                DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID);
                GridViewaddwages.DataSource = ds.Tables[1];
                GridViewaddwages.DataBind();
            }
        }
        else
        {
            //RequiredFieldValidator1.Visible = false;
            if (wagests != 0)
            {
                GridViewaddwages.Enabled = false;
                btnsave.Enabled = false;
                btncancel.Enabled = false;
            }
        }
    }

    public string NumericFormator(string Value)
    {
        try
        {
            return String.Format("{0.00}", Value);
        }
        catch
        {
            return Value;
        }
    }

    public string NumericFormator(double Value)
    {
        try
        {
            return String.Format("{0.00}", Value);
        }
        catch
        {
            return Value.ToString();
        }
    }

    protected string Generate_Crew_Agreement(int CrewID, int VoyID)
    {
        int Contract_template_ID = 0;
        string retText = "";

        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        System.Data.DataTable dt = objCrewBLL.Get_CrewAgreementData(CrewID, VoyID);
        System.Data.DataTable dtPersonal = objCrewBLL.Get_CrewPersonalDetailsByID1(CrewID);
        System.Data.DataTable dtNOK = objCrewBLL.Get_Crew_DependentsByCrewID(CrewID, 1);

        string Photo = "";
        int NOK_Mandatory = 0, Photo_Mandatory = 0;

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
            retText = "<h1>Contract can not be printed at this moment as Photo is not yet uploaded for the Crew</h1>";
        }
        else if (NOK_Mandatory == 1 && dtNOK.Rows.Count == 0)
        {
            retText = "<h1>Contract can not be printed at this moment as Next Of Kin details is not yet updated</h1>";
        }
        else if (dt.Rows.Count == 0)
        {
            retText = "<h1>Contract can not be printed at this moment. Please enter the voyage details for the crew.</h1>";

        }
        else
        {
            try
            {
                DataSet ds = BLL_PortageBill.Get_CrewWagesByVoyage_ForAgreement(CrewID, VoyID);
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

                    rptContractWages.DataSource = dataview;
                    rptContractWages.DataBind();

                    System.Data.DataTable dtVoy = objCrewBLL.Get_CrewVoyages(CrewID, VoyID);
                    int Vessel_Flag = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Vessel_Flag"].ToString());
                    int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
                    int VesselID = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Vessel_ID"].ToString());
                    System.Data.DataSet dtCompany = objCrewBLL.Get_Crew_CompanyDetails(UDFLib.ConvertIntegerToNull(VesselID));
                    System.Data.DataTable dtTemplate;

                    int ContractId = UDFLib.ConvertToInteger(dtVoy.Rows[0]["ContractId"].ToString());
                    dtTemplate = objBLLCrewContract.Get_ContractTemplate(ContractId);

                    string TemplateText = "";

                    if (dtTemplate.Rows.Count > 0)
                    {
                        Contract_template_ID = UDFLib.ConvertToInteger(dtTemplate.Rows[0]["ID"].ToString());
                        FooterText = dtTemplate.Rows[0]["FooterText"].ToString();

                        TemplateText = "<template>" + dtTemplate.Rows[0]["template_text"].ToString() + "</template>";

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
                            rptContractWages.RenderControl(hw);

                            string tblWages = "<table style='width:100%'><tr style='background-color: #D8D8D8; color: Black; font-weight: bold;'><td>Earning/Deduction</td><td>Name</td><td>Currency</td><td>Amount</td><td>Salary Type</td></tr>";
                            tblWages += sw.ToString().Replace("\n\r", "").Replace("\r\n", "").Replace(" & ", " &#38; ");
                            tblWages += "</table>";

                            //tblWages = tblWages.Replace(' ','');
                            wages[0].InnerXml = string.Format(tblWages);

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
                                DateTime DtJoining = UDFLib.ConvertToDate(JoinDate, UDFLib.GetDateFormat());

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
                                string sFileName = CrewID.ToString() + "_" + VoyID.ToString() + "_1" + ".pdf";
                                string filePath = Server.MapPath("~/Uploads/CrewDocuments/") + sFileName;

                                System.Data.DataTable dtAgreement = objCrewBLL.Get_CrewAgreementRecords(CrewID, VoyID, 0, GetSessionUserID());
                                DataRow[] dr = dtAgreement.Select("agreement_stage = 1");
                                if (dr.Length > 0)
                                {
                                    ViewState["pagecount"] = dr[0]["PageCounts"].ToString();
                                }
                                else
                                {
                                    ViewState["pagecount"] = 0;
                                }
                                string WordFilePath1 = filePath.Replace(".pdf", ".doc");
                                //Write_HTML_ToWord(TemplateText, WordFilePath);
                                //Convert_Word_ToPDF(WordFilePath, filePath);

                                EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
                                "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
                                "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
                                "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
                                "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
                                "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");

                                EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
                                EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = "<center><img alt='' src='" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/images/Company_logo.jpg'  height='60px' /></center>";
                                //EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div><table style='width:100%'><tr><td style='text-align:left'>CHR 010.1</td><td style='text-align:right'>Rev .01/ 24‐Feb‐2012</td></tr></table></div>";
                                EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='color:white;z-order:100'><table style='width:100%'><tr><td style='width:33%;text-align:left;height:40px;vertical-align:bottom;'></td><td style='width:34%;text-align:center;height:40px;vertical-align:bottom;'>Page {page_number} of {total_pages}</td><td style='width:33%;text-align:right;height:40px;vertical-align:bottom;'></td></tr></table></div>";
                                EO.Pdf.HtmlToPdf.ConvertHtml(TemplateText, filePath);


                                Random r1 = new Random();
                                string ver1 = r1.Next().ToString();
                                int Pagecount = getNumberOfPdfPages(filePath);
                                if (Pagecount >= Convert.ToInt32(ViewState["pagecount"]))
                                {
                                    int DocID = objCrewBLL.Insert_CrewAgreementRecord(CrewID, VoyID, 1, Contract_template_ID, "Crew Agreement", sFileName, sFileName, GetSessionUserID(), Pagecount);
                                    if (DocID > 0)
                                    {
                                        //--Delete the existing file
                                        try
                                        {
                                            System.IO.FileInfo fi = new FileInfo(filePath);
                                            fi.Delete();
                                        }
                                        catch { }

                                        string WordFilePath = filePath.Replace(".pdf", ".doc");
                                        //Write_HTML_ToWord(TemplateText, WordFilePath);
                                        //Convert_Word_ToPDF(WordFilePath, filePath);

                                        /* Added by pranali_070715 License Key EO.Pdf*/
                                        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
                                        "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
                                        "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
                                        "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
                                        "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
                                        "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
                                        EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
                                        EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = "<center><img alt='' src='" + System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/images/Company_logo.jpg'  height='60px' /></center>";
                                        //EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div><table style='width:100%'><tr><td style='text-align:left'>CHR 010.1</td><td style='text-align:right'>Rev .01/ 24‐Feb‐2012</td></tr></table></div>";
                                        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='color:white;z-order:100'><table style='width:100%'><tr><td style='width:33%;text-align:left;height:40px;vertical-align:bottom;'></td><td style='width:34%;text-align:center;height:40px;vertical-align:bottom;'>Page {page_number} of {total_pages}</td><td style='width:33%;text-align:right;height:40px;vertical-align:bottom;'></td></tr></table></div>";
                                        EO.Pdf.HtmlToPdf.ConvertHtml(TemplateText, filePath);

                                        Random r = new Random();
                                        string ver = r.Next().ToString();
                                        retText = "Crew Agreement Generated. The agreement is yet to be signed by the office.";
                                    }
                                }
                                else
                                {
                                    retText = "The PDF document you are trying to upload is invalid.Page count is not matching downloaded document.";
                                }

                            }

                        }
                        else
                        {
                            retText = "Contract templete do not have wages section";
                        }

                    }

                    else
                    {
                        retText = "Contract templete not found for the vessel flag";
                    }
                }
            }
            catch (Exception ex)
            {
                retText = ex.Message;

            }
        }

        return retText;
    }
    //This function is called after every page is created
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
        footer.Style.BackgroundColor = Color.Blue;
        footer.Style.ForegroundColor = Color.White;
        render.Render(footer);


    }

    protected void GridViewaddwages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Key_Value = DataBinder.Eval(e.Row.DataItem, "Key_Value").ToString();
            string Entry_Type = DataBinder.Eval(e.Row.DataItem, "Entry_Type").ToString();
            string Entry_Name = DataBinder.Eval(e.Row.DataItem, "Name").ToString();

            if (Key_Value == "SENIORITYBONUS")
            {
                decimal SeniorityAmount = 0;
                int SeniorityYear = 0;
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                if (txtAmount != null)
                {
                    txtAmount.Enabled = false;
                    if (txtAmount.Text.ToString() == "0.00")
                    {
                        DataTable dtRankSeniority = BLL_PortageBill.GET_Crew_RankSeniorityAmount(CrewID, RankId, VoyID);
                        if (dtRankSeniority != null && dtRankSeniority.Rows.Count > 0)
                        {
                            SeniorityAmount = decimal.Parse(dtRankSeniority.Rows[0]["SeniorityAmount"].ToString());
                            SeniorityYear = int.Parse(dtRankSeniority.Rows[0]["SeniorityYear"].ToString());
                            hdnCurrentSeniority.Value = SeniorityYear.ToString();
                            Load_Seniority_Rates(RankId);
                        }
                        txtAmount.Text = SeniorityAmount.ToString();
                    }

                    LinkButton lnkUpdateSeniority = (LinkButton)e.Row.FindControl("lnkUpdateSeniority");
                    lnkUpdateSeniority.OnClientClick = "ShowUpdateSeniority('" + VoyID.ToString() + "');return false;";
                    lnkUpdateSeniority.Visible = true;
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
            if (Key_Value == "COMPANY_SENIORITY_BONUS")
            {
                int SeniorityAmount = BLL_PortageBill.GET_Crew_CompanySeniorityAmount(CrewID, RankId);

                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                if (txtAmount != null)
                {
                    txtAmount.Text = SeniorityAmount.ToString();
                    txtAmount.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
            if (Key_Value == "REJOINING_BONUS")
            {
                DataTable dt = BLL_PortageBill.GetRejoiningBonus(CrewID);
                if (dt.Rows.Count > 0)
                {
                    ((TextBox)e.Row.FindControl("txtAmount")).Text = dt.Rows[0]["RJBAmount"].ToString();
                    ((TextBox)e.Row.FindControl("txtAmount")).Enabled = false;
                    System.Web.UI.WebControls.Image lnkUpdateSeniority = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgInfo");
                    lnkUpdateSeniority.Visible = true;
                    lnkUpdateSeniority.Attributes.Add("onmouseover", "ShowReJoining_History(" + dt.Rows[0]["ID"].ToString() + ",event,this);return false;");

                }
                ((RadioButtonList)e.Row.FindControl("SalaryTypes")).SelectedValue = "27";
                ((RadioButtonList)e.Row.FindControl("PayableAT")).SelectedValue = "27";
            }
            try
            {
                int OnBoardEntry = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OnBoardEntry"));
                if (OnBoardEntry == 1)
                {
                    ((TextBox)e.Row.FindControl("txtAmount")).Enabled = false;
                }
            }
            catch { }
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
    protected void gvNewSeniority_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRankSeniorityRemarks.Enabled = true;
        btnUpdateSeniority.Enabled = true;
    }
    protected void Load_Seniority_Rates(int RankID)
    {
        //Get_Seniority_Rates
        DataTable dt = BLL_PortageBill.Get_Seniority_Rates(RankID, GetSessionUserID());
        if (dt.Rows.Count > 0)
        {
            gvNewSeniority.DataSource = dt;
            gvNewSeniority.DataBind();

            gvNewSeniority.SelectedIndex = UDFLib.ConvertToInteger(hdnCurrentSeniority.Value);
        }

    }
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Joining_Rank = DataBinder.Eval(e.Row.DataItem, "Joining_Rank").ToString();
        }
    }

    protected void ddlRankScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Contract_Type = 0, RankScaleId = 0;
        DataSet ds = BLL_PortageBill.Get_CrewWagesByVoyage(CrewID, VoyID);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            RankId = int.Parse(ds.Tables[0].Rows[0]["Joining_Rank"].ToString());
            Contract_Type = int.Parse(ds.Tables[0].Rows[0]["Contract_Type"].ToString());
        }
        RankScaleId = int.Parse(ddlRankScale.SelectedValue.ToString());
        ViewState["CurrentRankScale"] = RankScaleId;
        WageContractId = 0;
        if (RankScaleId > 0)
        {
            DataSet ds1 = BLL_PortageBill.Get_Rank_WageContract(RankId, Contract_Type, NationalityConsidered, Staff_Nationality, RankScaleId);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                WageContractId = int.Parse(ds1.Tables[0].Rows[0]["WageContractID"].ToString());
                hdWageContractId.Value = ds1.Tables[0].Rows[0]["WageContractID"].ToString();
                GridViewaddwages.DataSource = ds1.Tables[1];
                GridViewaddwages.DataBind();
            }
            else
            {
                GridViewaddwages.DataSource = null;
                GridViewaddwages.DataBind();
            }
        }
        else
        {
            GridViewaddwages.DataSource = null;
            GridViewaddwages.DataBind();
        }
    }
    protected void btnUpdateSeniority_Click(object sender, EventArgs e)
    {
        bool IsValidated = true;
        string sValidationMessage = "";
        DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID);
        if (ds.Tables[1].Rows.Count > 0)
        {
            if (txteffdt.Text != "")
            {
                if (ds.Tables[1].Rows[0]["effective_date"].ToString() != "")
                {
                    if (txteffdt.Enabled == false && UDFLib.ConvertToDate(txteffdt.Text.Trim(), UDFLib.GetDateFormat()) < DateTime.Parse(ds.Tables[1].Rows[0]["effective_date"].ToString()))
                    {
                        IsValidated = false;
                        sValidationMessage = "Wages can not be saved as the new effective date can not be prior to the previous effective date.";
                    }
                    if (txteffdt.Enabled == true && UDFLib.ConvertToDate(txteffdt.Text.Trim(), UDFLib.GetDateFormat()) <= DateTime.Parse(ds.Tables[1].Rows[0]["effective_date"].ToString()))
                    {
                        IsValidated = false;
                        sValidationMessage = "Wages can not be saved as the new effective date can not be prior to or equal to the previous effective date.";
                    }
                }
            }
            else
            {
                IsValidated = false;
                sValidationMessage = "Please select effective date";
            }
        }

        if (IsValidated == true)
        {
            GridViewRow row = gvNewSeniority.SelectedRow;
            foreach (GridViewRow dRow in GridViewaddwages.Rows)
            {
                string Entry_Name = (dRow.FindControl("lblentry_type") as Label).Text;
                string Key_Value = (dRow.FindControl("lblKey_Value") as Label).Text;
                if (Key_Value == "SENIORITYBONUS")
                {
                    TextBox txtAmount = (TextBox)dRow.FindControl("txtAmount");
                    if (txtAmount != null)
                    {
                        txtAmount.Text = row.Cells[1].Text;
                        hdnSelectedSeniority.Value = row.Cells[0].Text;
                    }
                }
            }
            Save();
            txtRankSeniorityRemarks.Enabled = false;
            string js3 = "hideModal('dvUpdateSeniority');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js3", js3, true);
        }
        else
        {
            string js = "alert('" + sValidationMessage + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }
    protected void btnCancelSeniorityUpdate_Click(object sender, EventArgs e)
    {
        txtRankSeniorityRemarks.Enabled = false;
        string js3 = "hideModal('dvUpdateSeniority');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "js3", js3, true);
    }
}
