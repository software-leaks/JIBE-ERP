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

public partial class Account_Portage_Bill_Wage_Contract : System.Web.UI.Page
{
    //private int VoyID = 0;
    //private int CrewID = 0;
    //private int Vessel_ID = 0;
    int NationalityConsidered = 0, RankScaleConsidered = 0, VesselFlagConsidered = 0;
    DataView dataview;
    protected decimal TotalAmount;

    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    UserAccess objUA = new UserAccess();
    BLL_Infra_Approval_Limit objBLLAppLimit = new BLL_Infra_Approval_Limit();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblMessage.Text = "";
        CalendarExtender5.Format = UDFLib.GetDateFormat();

        DataTable dtWages = objCrewAdmin.GetWagesSettings();
        if (dtWages != null && dtWages.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtWages.Rows[0]["VesselFlagConsidered"]) == false)
            {
                divVesselFlag.Visible = false;
            }
            else
                VesselFlagConsidered = 1;
            if (Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]) == false)
            {
                tdNationality.Visible = false;
                tdAddGroup.Visible = false;
            }
            else
                NationalityConsidered = 1;

            if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == false)
            {
                divRankScale.Visible = false;
            }
            else
            {
                divRankScale.Visible = true;
                RankScaleConsidered = 1;
            }
        }
        if (!IsPostBack)
        {
            try
            {
                Load_Vessel_Flags();
                Load_RankList();
                Load_CurrencyList(ddlCurrencyType);
                btnAddCountry.Enabled = false;
                LoadNationalityGroup(0);
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
        //-- VESSEL OWNER -- //
        else if (Session["UTYPE"].ToString() == "VESSEL OWNER")
        {

        }
        else//--- CREW TEAM LOGIN--
        {
        }
    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlRank.SelectedIndex = 0;
    }

    public void Load_RankScaleList(int RankId)
    {
        ddlRankScale.Items.Clear();
        if (RankId > 0)
        {
            DataTable dt = objCrewAdmin.Get_RankScaleList(RankId);
            ddlRankScale.DataSource = dt;
            ddlRankScale.DataTextField = "RankScaleName";
            ddlRankScale.DataValueField = "ID";
            ddlRankScale.DataBind();

        }
        ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void Load_Vessel_Flags()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        DataTable dt = objVessel.Get_VesselFlagList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));
        lstFlags.DataSource = dt;
        lstFlags.DataValueField = "vessel_flag";
        lstFlags.DataTextField = "flag_name";
        lstFlags.DataBind();
    }
    public void Load_CountryList(int RankID, int Contract_Type)
    {
        lblNationality.DataSource = objCrew.Get_WagesNationalityList(RankID, Contract_Type); ;
        lblNationality.DataTextField = "COUNTRY";
        lblNationality.DataValueField = "ID";
        lblNationality.DataBind();
    }
    protected void Add_CrewWages(int RankID, int Contract_Type, int NationalityConsidered, int CountryId, int RankScaleId)
    {
        try
        {
            DataSet ds = BLL_PortageBill.Get_Rank_WageContract_AddNew(RankID, Contract_Type, NationalityConsidered, CountryId, RankScaleId);
            txteffdt.Enabled = true;
            GridViewaddwages.DataSource = ds.Tables[1];
            GridViewaddwages.DataBind();
            pnlAddWages.Visible = true;
            pnlViewWages.Visible = false;
            pnlAddWages.Visible = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void View_CrewWages()
    {
        try
        {


            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            int Contract_Type = 0;
            int CountryId = 0, RankScaleId = 0, WageContractID = 0;
            if (NationalityConsidered == 1 && lblNationality.Items.Count > 0)
            {
                CountryId = UDFLib.ConvertToInteger(lblNationality.Items[0].Value);
            }
            if (RankScaleConsidered == 1 && ddlRankScale.SelectedItem != null)
            {
                RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
            }
            if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
            {
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
            }
            DataSet ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, WageContractID, NationalityConsidered, CountryId, RankScaleId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Relations.Add(new DataRelation("Parent", ds.Tables[0].Columns["ID"], ds.Tables[1].Columns["WageContractID"]));
                ds.Tables[1].TableName = "Child";

                dataview = ds.Tables[1].DefaultView;
                rpt1.DataSource = ds;
                rpt1.DataBind();

                int totalCount = ds.Tables[0].Rows.Count;
                //rpt1.FindControl("lnkEditWage"

                pnlViewWages.Visible = true;
                pnlAddWages.Visible = false;
            }
            else
            {
                Add_CrewWages(RankID, Contract_Type, NationalityConsidered, CountryId, RankScaleId);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void Edit_CrewWages(int RankID, int Contract_Type, int WageContractID, int NationalityConsidered, int CountryId, int RankScaleId)
    {
        try
        {
            lblMessage.Text = "";
            DataSet ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, WageContractID, NationalityConsidered, CountryId, RankScaleId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["effective_date"].ToString() != "")
                {
                    txteffdt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Tables[0].Rows[0]["effective_date"].ToString()));
                    txteffdt.Enabled = false;
                }
                else
                    txteffdt.Enabled = true;
                //Commented the code after confirming from Maneesh..need to check workflow
                // if (DateTime.Today < DateTime.Parse(ds.Tables[0].Rows[0]["effective_date"].ToString()))
                // {
                GridViewaddwages.DataSource = ds.Tables[1];
                GridViewaddwages.DataBind();

                pnlViewWages.Visible = false;
                pnlAddWages.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
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
        LinkButton lb = e.Item.FindControl("lnkEditWage") as LinkButton;
        if (e.Item.ItemIndex > 0)
            lb.Visible = false;
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
    }

    protected void rpt1_ItemCommand(object sender, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            int Contract_Type = 0;
            int CountryId = 0, RankScaleId = 0;
            if (RankScaleConsidered == 1)
            {
                RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
            }
            if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
            {
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
            }
            if (NationalityConsidered == 1 && lblNationality.Items.Count > 0)
            {
                CountryId = UDFLib.ConvertToInteger(lblNationality.Items[0].Value);
            }
            Edit_CrewWages(RankID, Contract_Type, UDFLib.ConvertToInteger(e.CommandArgument.ToString()), NationalityConsidered, CountryId, RankScaleId);
        }
    }

    protected void rpt2_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblAmt = (Label)e.Item.FindControl("lblAmt");
            Label lblEarningDeduction = (Label)e.Item.FindControl("lblEarningDeduction");

            if (lblAmt != null)
            {
                decimal Amt = UDFLib.ConvertToDecimal(lblAmt.Text);

                if (lblEarningDeduction.Text == "Earning")
                    TotalAmount += Amt;
                else
                    TotalAmount -= Amt;
            }
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalAmt = (Label)e.Item.FindControl("lblTotalAmt");
            if (lblTotalAmt != null)
            {
                //TotalAmount = this.calculateTotals();
                lblTotalAmt.Text = TotalAmount.ToString("0.00");
                TotalAmount = 0;
            }
        }

    }

    protected void btnAddWages_Click(object sender, EventArgs e)
    {
        try
        {
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            int Contract_Type = 0;
            int CountryID = 0;
            int RankScaleId = 0;
            txteffdt.Text = "";
            if (RankScaleConsidered == 1)
            {
                RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
            }
            if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
            {
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
            }
            if (NationalityConsidered == 1)
            {
                foreach (ListItem liCountry in lblNationality.Items)
                {
                    CountryID = UDFLib.ConvertToInteger(liCountry.Value);
                }
            }
            Add_CrewWages(RankID, Contract_Type, NationalityConsidered, CountryID, RankScaleId);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        View_CrewWages();
    }


    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        bool saveStatus = Save(dt);
        View_CrewWages();
    }
    protected bool Save(DataTable dtVessel)
    {
        bool IsValidated = true;
        try
        {
            DataTable dtWagesINS = new DataTable();

            dtWagesINS.Columns.Add("ID", typeof(int));
            dtWagesINS.Columns.Add("CompanyID", typeof(int));
            dtWagesINS.Columns.Add("Contract_Type", typeof(int));
            dtWagesINS.Columns.Add("RankID", typeof(int));
            dtWagesINS.Columns.Add("EntryType", typeof(int));
            dtWagesINS.Columns.Add("SalaryType", typeof(int));
            dtWagesINS.Columns.Add("PayAt", typeof(int));
            dtWagesINS.Columns.Add("Amount", typeof(decimal));
            dtWagesINS.Columns.Add("Currency", typeof(int));
            dtWagesINS.Columns.Add("Updated_BY", typeof(int));
            dtWagesINS.Columns.Add("Effective_Date", typeof(DateTime));
            dtWagesINS.Columns.Add("CountryId", typeof(int));
            dtWagesINS.Columns.Add("RankScaleId", typeof(int));
            dtWagesINS.Columns.Add("VesselID", typeof(int));

            List<int> lstCountries = new List<int>();
            string sValidationMessage = "";
            lblMessage.Text = "";

            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            int Contract_Type = 0;
            int CountryId = 0;
            int RankScaleId = 0;
            if (RankID == 0)
            {
                IsValidated = false;
                sValidationMessage = "Please select rank";
            }
            if (IsValidated == true && VesselFlagConsidered == 1)
            {
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
                if (Contract_Type == 0)
                {
                    IsValidated = false;
                    sValidationMessage = "Please select contract type";
                }
            }
            if (IsValidated == true && RankScaleConsidered == 1)
            {
                if (ddlRankScale.SelectedItem != null && int.Parse(ddlRankScale.SelectedItem.Value) > 0)
                {
                    RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
                }
                else
                {
                    IsValidated = false;
                    sValidationMessage = "Please select Rank Scale";
                }
            }
            if (IsValidated == true)
            {
                if (NationalityConsidered == 1)
                {
                    foreach (ListItem liCountry in lblNationality.Items)
                    {
                        CountryId = UDFLib.ConvertToInteger(liCountry.Value);

                        DataSet ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, 0, NationalityConsidered, CountryId, RankScaleId);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (txteffdt.Text == "")
                            {
                                //    if (ds.Tables[0].Rows[0]["effective_date"].ToString() != "")
                                //    {
                                //        if (DateTime.Parse(txteffdt.Text) < DateTime.Parse(ds.Tables[0].Rows[0]["effective_date"].ToString()))
                                //        {
                                //            IsValidated = false;
                                //            sValidationMessage = "Wages can not be saved as the new effective date can not be prior to the previous effective date.";
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                IsValidated = false;
                                sValidationMessage = "Please select effective date";
                            }
                        }


                        if (IsValidated == true)
                        {
                            int currusd = 0;
                            int RecCount = 0;

                            DateTime dt = new DateTime();
                            if (txteffdt.Text != "")
                            {
                                dt = UDFLib.ConvertToDate(txteffdt.Text.Trim(),UDFLib.GetDateFormat());
                            }
                            else
                            {
                                string efdt = "1900/01/01";
                                dt = DateTime.Parse(efdt);
                            }
                            currusd = int.Parse(ddlCurrencyType.SelectedValue);
                            foreach (GridViewRow gr in GridViewaddwages.Rows)
                            {
                                double amt = 0;

                                int ii = Convert.ToInt32(GridViewaddwages.DataKeys[0].Value);
                                if (Convert.ToString(((TextBox)gr.FindControl("txtAmount")).Text) != "" && Convert.ToString(((Label)gr.FindControl("lblVesselSpecific")).Text) == "False")
                                {
                                    if (double.TryParse(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text, out amt))
                                    {
                                        string sentry = ((Label)GridViewaddwages.Rows[gr.RowIndex].FindControl("lblentry_type")).Text;

                                        int iSalaryType = Convert.ToInt32(((RadioButtonList)gr.Cells[2].FindControl("SalaryTypes")).SelectedValue);
                                        int iPayAt = Convert.ToInt32(((RadioButtonList)gr.Cells[2].FindControl("PayableAT")).SelectedValue);

                                        DataRow dr = dtWagesINS.NewRow();

                                        RecCount += 1;

                                        dr["ID"] = RecCount;
                                        if (Session["USERCOMPANYID"] != null)
                                            dr["CompanyID"] = int.Parse(Session["USERCOMPANYID"].ToString());

                                        dr["Contract_Type"] = Contract_Type;
                                        dr["RankID"] = RankID;
                                        dr["Effective_Date"] = dt.ToString("dd/MM/yyyy");

                                        dr["EntryType"] = UDFLib.ConvertToInteger(GridViewaddwages.DataKeys[gr.RowIndex].Values[1].ToString());
                                        dr["SalaryType"] = iSalaryType;
                                        dr["PayAt"] = iPayAt;
                                        dr["Amount"] = amt;
                                        dr["Currency"] = currusd;
                                        dr["Updated_BY"] = GetSessionUserID();
                                        dr["CountryId"] = CountryId;
                                        dr["RankScaleId"] = RankScaleId;
                                        dr["VesselID"] = 0;
                                        dtWagesINS.Rows.Add(dr);
                                    }
                                }
                            }
                            foreach (DataRow dr1 in dtVessel.Rows)
                            {
                                DataRow dr = dtWagesINS.NewRow();

                                RecCount += 1;

                                dr["ID"] = RecCount;
                                if (Session["USERCOMPANYID"] != null)
                                    dr["CompanyID"] = int.Parse(Session["USERCOMPANYID"].ToString());

                                dr["Contract_Type"] = Contract_Type;
                                dr["RankID"] = RankID;
                                dr["Effective_Date"] = dt.ToString("dd/MM/yyyy");

                                dr["EntryType"] = dr1["EntryType"];
                                dr["SalaryType"] = dr1["SalaryType"];
                                dr["PayAt"] = dr1["PayAt"];
                                dr["Amount"] = dr1["Amount"];
                                dr["Currency"] = currusd;
                                dr["Updated_BY"] = GetSessionUserID();
                                dr["CountryId"] = CountryId;
                                dr["RankScaleId"] = RankScaleId;
                                dr["VesselID"] = dr1["VesselID"];
                                dtWagesINS.Rows.Add(dr);
                            }

                            if (RecCount > 0)
                            {
                                BLL_PortageBill.Ins_Rank_Wage_Contract(dtWagesINS);
                                dtWagesINS.Clear();
                                lblMessage.Text = "Rank wage contract updated.";
                            }
                        }
                    }// for loop country increment
                }//if for NationalityConsidered
                else
                {
                    if (IsValidated == true)
                    {
                        int currusd = 0;
                        int RecCount = 0;

                        DateTime dt = new DateTime();
                        if (txteffdt.Text != "")
                        {
                            dt = UDFLib.ConvertToDate(txteffdt.Text.Trim());
                        }
                        else
                        {
                            string efdt = "1900/01/01";
                            dt = DateTime.Parse(efdt);
                        }
                        currusd = int.Parse(ddlCurrencyType.SelectedValue);
                        foreach (GridViewRow gr in GridViewaddwages.Rows)
                        {
                            double amt = 0;

                            int ii = Convert.ToInt32(GridViewaddwages.DataKeys[0].Value);
                            if (Convert.ToString(((TextBox)gr.FindControl("txtAmount")).Text) != "" && Convert.ToString(((Label)gr.FindControl("lblVesselSpecific")).Text) == "False")
                            {
                                if (double.TryParse(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text, out amt))
                                {
                                    string sentry = ((Label)GridViewaddwages.Rows[gr.RowIndex].FindControl("lblentry_type")).Text;

                                    int iSalaryType = Convert.ToInt32(((RadioButtonList)gr.Cells[2].FindControl("SalaryTypes")).SelectedValue);
                                    int iPayAt = Convert.ToInt32(((RadioButtonList)gr.Cells[2].FindControl("PayableAT")).SelectedValue);

                                    DataRow dr = dtWagesINS.NewRow();

                                    RecCount += 1;

                                    dr["ID"] = RecCount;
                                    if (Session["USERCOMPANYID"] != null)
                                        dr["CompanyID"] = int.Parse(Session["USERCOMPANYID"].ToString());

                                    dr["Contract_Type"] = Contract_Type;
                                    dr["RankID"] = RankID;
                                    dr["Effective_Date"] = dt.ToString("dd/MM/yyyy");

                                    dr["EntryType"] = UDFLib.ConvertToInteger(GridViewaddwages.DataKeys[gr.RowIndex].Values[1].ToString());
                                    dr["SalaryType"] = iSalaryType;
                                    dr["PayAt"] = iPayAt;
                                    dr["Amount"] = amt;
                                    dr["Currency"] = currusd;
                                    dr["Updated_BY"] = GetSessionUserID();
                                    dr["CountryId"] = 0;
                                    dr["RankScaleId"] = RankScaleId;
                                    dr["VesselID"] = 0;
                                    dtWagesINS.Rows.Add(dr);
                                }
                            }
                        }
                        foreach (DataRow dr1 in dtVessel.Rows)
                        {
                            DataRow dr = dtWagesINS.NewRow();

                            RecCount += 1;

                            dr["ID"] = RecCount;
                            if (Session["USERCOMPANYID"] != null)
                                dr["CompanyID"] = int.Parse(Session["USERCOMPANYID"].ToString());

                            dr["Contract_Type"] = Contract_Type;
                            dr["RankID"] = RankID;
                            dr["Effective_Date"] = dt.ToString("dd/MM/yyyy");

                            dr["EntryType"] = dr1["EntryType"];
                            dr["SalaryType"] = dr1["SalaryType"];
                            dr["PayAt"] = dr1["PayAt"];
                            dr["Amount"] = dr1["Amount"];
                            dr["Currency"] = currusd;
                            dr["Updated_BY"] = GetSessionUserID();
                            dr["CountryId"] = 0;
                            dr["RankScaleId"] = RankScaleId;
                            dr["VesselID"] = dr1["VesselID"];
                            dtWagesINS.Rows.Add(dr);
                        }

                        if (RecCount > 0)
                        {
                            BLL_PortageBill.Ins_Rank_Wage_Contract(dtWagesINS);
                            dtWagesINS.Clear();
                            lblMessage.Text = "Rank wage contract updated.";
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = sValidationMessage;
            }
        }
        catch (Exception e)
        {
            UDFLib.WriteExceptionLog(e);
        }
        //1-> stores the current scale no. of crew in Crew_Scales table for first time after that 
        //scale will be changed based on fixed criteria.
        //2->save the sharesave  amount with original currency in ACC_BPShip_Dtl_Crew_ShareSave and store in joinningearndeduction table  in usd        
        return IsValidated;
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
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        txteffdt.Text = "";
        //pnlAddWages.Visible = true;
        //pnlViewWages.Visible = true;
        DataTable dtnull = new DataTable();
        ViewState["NationalityGroupId"] = null;
        lblNationality.DataSource = dtnull;
        lblNationality.DataBind();
        int Contract_Type = 0;
        if (ddlRank.SelectedItem != null)
        {
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            DataTable dtWages = objCrewAdmin.GetWagesSettings();
            if (dtWages != null && dtWages.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]) == true)
                    NationalityConsidered = 1;
                if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == true)
                    RankScaleConsidered = 1;
                if (Convert.ToBoolean(dtWages.Rows[0]["VesselFlagConsidered"]) == true)
                {
                    VesselFlagConsidered = 1;
                    if (lstFlags.SelectedItem != null)
                        Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
                }
            }
            btnAddCountry.Enabled = false;
            if (RankID > 0)
            {
                btnAddCountry.Enabled = true;
            }
            if (NationalityConsidered == 1)
                LoadNationalityGroup(RankID);

            if (RankScaleConsidered == 1)
                Load_RankScaleList(RankID);

            View_CrewWages();
        }
    }
    public void GroupSelected(object sender, CommandEventArgs e)
    {
        string[] cmdargs = e.CommandArgument.ToString().Split(',');
        int RowIndex = int.Parse(cmdargs[0].ToString());
        int NationalityGroupId = int.Parse(cmdargs[1].ToString());
        ViewState["NationalityGroupId"] = NationalityGroupId;
        txteffdt.Enabled = true;

        for (int i = 0; i < gvNationalityGroup.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvNationalityGroup.Rows[i];
            if (i == RowIndex)
                selectedRow.BackColor = System.Drawing.Color.SkyBlue;
            else
                selectedRow.BackColor = System.Drawing.Color.White;
        }

        DataSet ds = BLL_PortageBill.Get_RankWise_GroupDetails(NationalityGroupId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtnull = new DataTable();
            lblNationality.Items.Clear();
            lblNationality.DataSource = dtnull;
            lblNationality.DataBind();
            lblNationality.DataSource = ds.Tables[0];
            lblNationality.DataTextField = "COUNTRY";
            lblNationality.DataValueField = "Country_Id";
            lblNationality.DataBind();
        }
        if (RankScaleConsidered == 1)
            ddlRankScale.Enabled = true;
        View_CrewWages();
    }
    protected void ddlRankScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
        if (RankScaleId == 0)
        {
            pnlAddWages.Visible = false;
            pnlViewWages.Visible = false;
        }
        else
        {
            View_CrewWages();
        }
    }
    protected void btnAddCountry_Click(object sender, EventArgs e)
    {
        int Contract_Type = 0;
        txtGroupName.Text = string.Empty;
        txtGroupName.Enabled = true;
        DataTable dtnull = new DataTable();
        chkCountryList.DataSource = dtnull;
        chkCountryList.DataBind();
        rpt1.DataSource = dtnull;
        rpt1.DataBind();
        chkCountryList.ClearSelection();
        if (ddlRank.SelectedItem != null)
        {
            if (VesselFlagConsidered == 1)
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("COUNTRY", typeof(string));

            dt = BLL_PortageBill.Get_NationalityForWages(RankID, Contract_Type);
            chkCountryList.DataSource = dt;
            chkCountryList.DataBind();
            string msgdivResponseShow = string.Format("showModal('divCountryList',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            pnlAddCountries.Update();
            ViewState["NationalityGroupId"] = null;
        }
        ddlRankScale.Enabled = false;
    }
    protected void btnAddSelectedCountries(object sender, EventArgs e)
    {

        int Contract_Type = 0;
        int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
        if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
        {
            Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
        }
        lblNationality.Items.Clear();

        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("COUNTRY", typeof(string));

        List<int> lstCountryList = new List<int>();
        for (int i = 0; i < chkCountryList.Items.Count; i++)
        {
            if (chkCountryList.Items[i].Selected)
            {
                DataRow dr = dt.NewRow();

                dr["ID"] = chkCountryList.Items[i].Value;
                dr["COUNTRY"] = chkCountryList.Items[i].Text;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
        }

        lblNationality.DataSource = dt;
        lblNationality.DataBind();

        string msgdivResponseShow = string.Format("hideModal('divCountryList',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        pnlAddCountries.Update();
        pnlAddWages.Visible = false;
        pnlViewWages.Visible = false;
    }
    protected void btnCopyWages_Click(object sender, EventArgs e)
    {
        if (lstFlags.SelectedItem != null && ddlRank.SelectedItem != null && lblNationality.SelectedItem != null)
        {
            txteffdt1.Text = "";
            Load_CurrencyList(ddlCurrencyType1);
            int Contract_Type = 0;
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
            {
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
            }
            string FromCountryName = lblNationality.SelectedItem.Text;
            lblCountryName.Text = FromCountryName;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            dt = BLL_PortageBill.Get_NationalityForWages(RankID, Contract_Type);
            dt1 = dt.Copy();

            int totalCountryCount = dt.Rows.Count;
            for (int i = 0; i < totalCountryCount; i++)
            {
                if (dt.Rows[i]["Selected"].ToString() == "1")
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["ID"].Equals(dt.Rows[i]["ID"]))
                        {
                            dt1.Rows[j].Delete();
                            dt1.AcceptChanges();
                            break;
                        }
                    }
                }
            }
            chkCountryList.DataSource = dt1; ///chkCountryList1
            chkCountryList.DataBind();         ///chkCountryList1
            string msgdivResponseShow = string.Format("showModal('divCountryList1',true);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            pnlAddWagesForCountries.Update();
        }
    }
    protected void btnAddWagesForSelectedCountries_OnClick(object sender, EventArgs e)
    {
        int Contract_Type = 0;
        int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
        int CountryId = int.Parse(lblNationality.SelectedValue.ToString());
        int Currency = UDFLib.ConvertToInteger(ddlCurrencyType1.SelectedItem.Value);
        int CompanyId = int.Parse(Session["USERCOMPANYID"].ToString());
        if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
        {
            Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
        }
        DateTime dtEffectiveDate = UDFLib.ConvertToDate(txteffdt1.Text.ToString(),UDFLib.GetDateFormat());
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        for (int i = 0; i < chkCountryList.Items.Count; i++)
        {
            if (chkCountryList.Items[i].Selected)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = chkCountryList.Items[i].Value;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
        }
        if (dt.Rows.Count > 0)
        {
            BLL_PortageBill.CopyRankWageFromExistingCountry(Contract_Type, RankID, CountryId, dtEffectiveDate, Currency, CompanyId, dt, GetSessionUserID());
            lblMessage.Text = "Rank wage contract updated.";
        }

        lblNationality.Items.Clear();
        lblNationality.DataSource = objCrew.Get_WagesNationalityList(RankID, Contract_Type); ;
        lblNationality.DataTextField = "COUNTRY";
        lblNationality.DataValueField = "ID";
        lblNationality.DataBind();
        string msgdivResponseShow = string.Format("hideModal('divCountryList1',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        pnlAddWagesForCountries.Update();
        pnlAddWages.Visible = false;
        pnlViewWages.Visible = false;
    }
    protected void btnSaveNationalityGroup_OnClick(object sender, EventArgs e)
    {
        string js = "";
        int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
        string GroupName = txtGroupName.Text.Trim();
        txteffdt.Text = "";

        DataTable dt = new DataTable();
        dt.Columns.Add("NationalityGroupId", typeof(int));
        dt.Columns.Add("CountryId", typeof(int));

        int NationalityGroupId = 0;
        if (ViewState["NationalityGroupId"] != null)
            NationalityGroupId = int.Parse(ViewState["NationalityGroupId"].ToString());

        List<int> lstCountryList = new List<int>();
        for (int i = 0; i < chkCountryList.Items.Count; i++)
        {
            if (chkCountryList.Items[i].Selected)
            {
                DataRow dr = dt.NewRow();
                dr["NationalityGroupId"] = NationalityGroupId;
                dr["CountryId"] = chkCountryList.Items[i].Value;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
        }
        if (dt.Rows.Count == 0)
        {
            js = "Please select at least one Country";
            string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divCountryList',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            return;
        }
        DataTable dtCheck = BLL_PortageBill.Check_NationalityGroup(RankID, NationalityGroupId, GroupName, dt);
        if (dtCheck != null && dtCheck.Rows.Count > 0)
        {
            if (int.Parse(dtCheck.Rows[0]["GroupNameExsist"].ToString()) > 0 && NationalityGroupId != 0)
                js = "";
            if (int.Parse(dtCheck.Rows[0]["CountryNameExsist"].ToString()) > 0 && NationalityGroupId != 0)
                js = "One or more Countries belongs to other Nationality Group";
            if (int.Parse(dtCheck.Rows[0]["GroupNameExsist"].ToString()) > 0 && NationalityGroupId == 0)
                js = "Nationality Group already exsist";
            if (int.Parse(dtCheck.Rows[0]["CountryNameExsist"].ToString()) > 0 && NationalityGroupId == 0)
                js = "One or more Countries belongs to other Nationality Group";

            if (js != "")
            {
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divCountryList',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
        }
        if (js == "")
        {
            int retVal = BLL_PortageBill.Insert_NationalityGroup(RankID, NationalityGroupId, GroupName, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            if (NationalityGroupId != 0)
                js = "Nationality Group Updated";
            else
                js = "Nationality Group Created";

            string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divCountryList',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            LoadNationalityGroup(RankID);
            if (NationalityGroupId != 0)
            {
                DataSet ds = BLL_PortageBill.Get_RankWise_GroupDetails(NationalityGroupId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtnull = new DataTable();
                    lblNationality.Items.Clear();
                    lblNationality.DataSource = dtnull;
                    lblNationality.DataBind();
                    lblNationality.DataSource = ds.Tables[0];
                    lblNationality.DataTextField = "COUNTRY";
                    lblNationality.DataValueField = "Country_Id";
                    lblNationality.DataBind();
                }
            }
            else
                lblNationality.Items.Clear();
        }
        ViewState["NationalityGroupId"] = null;
    }
    private void LoadNationalityGroup(int RankID)
    {
        DataTable dt = BLL_PortageBill.Get_NationalityGroupForWege_Contract(RankID);
        gvNationalityGroup.DataSource = dt;
        gvNationalityGroup.DataBind();
    }
    protected void DeleteNationality(object source, CommandEventArgs e)
    {
        try
        {
            DataTable dtnull = new DataTable();
            rpt1.DataSource = dtnull;
            rpt1.DataBind();
            // btnAddWages.Visible = false;
            lblNationality.DataSource = dtnull;
            lblNationality.DataBind();
            // tdDetails.Visible = false;
            int i = BLL_PortageBill.DeleteNationalityGroup(int.Parse(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            string js = "Nationality deleted";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);

            if (ddlRank.SelectedItem != null)
            {
                btnAddCountry.Enabled = true;
                LoadNationalityGroup(UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value));
            }
            View_CrewWages();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void EditNationality(object source, CommandEventArgs e)
    {
        try
        {
            txtGroupName.Enabled = false;
            chkCountryList.Items.Clear();
            int Contract_Type = 0;
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            string[] cmdargs = e.CommandArgument.ToString().Split(',');

            int GroupId = UDFLib.ConvertToInteger(cmdargs[0].ToString());
            txtGroupName.Text = cmdargs[1].ToString();
            if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
            {
                Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
            }
            DataTable dt1 = new DataTable();

            dt1 = BLL_PortageBill.Get_NationalityForWages(RankID, Contract_Type);
            chkCountryList.DataTextField = "COUNTRY";
            chkCountryList.DataValueField = "Country_ID";
            chkCountryList.DataSource = dt1;
            chkCountryList.DataBind();

            DataSet ds = BLL_PortageBill.Get_RankWise_GroupDetails(GroupId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkCountryList.DataSource = ds.Tables[0];
                chkCountryList.DataTextField = "COUNTRY";
                chkCountryList.DataValueField = "Country_Id";

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    chkCountryList.Items.FindByValue(item["Country_Id"].ToString()).Selected = true;
                }
            }

            ViewState["NationalityGroupId"] = GroupId;
            string msgdivResponseShow = string.Format("showModal('divCountryList',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            pnlAddCountries.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void Load_VesselType()
    {
        DataTable dt = BLL_PortageBill.Get_VesselType();
        ddlVesselType.DataSource = dt;
        ddlVesselType.DataTextField = "VesselTypes";
        ddlVesselType.DataValueField = "VesselType_Id";
        ddlVesselType.DataBind();
        ddlVesselType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void EditAmt(object source, CommandEventArgs e)
    {

        lblVesselAllowanceMessage.Text = "";
        try
        {
            ViewState["EntryType"] = null;
            ViewState["SalaryType"] = null;
            ViewState["PayAt"] = null;
            ViewState["RowIndex"] = null;
            txtVesselAmount.Text = "";
            int VesselTypeID = 0, RowIndex = 0; ;
            txtGroupName.Enabled = false;
            chkCountryList.Items.Clear();
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            string[] cmdargs = e.CommandArgument.ToString().Split(',');
            lblAllowances.Text = cmdargs[0].ToString() + ":";
            ViewState["EntryType"] = cmdargs[3].ToString();
            ViewState["RowIndex"] = cmdargs[5].ToString();
            string Mode = cmdargs[6].ToString();
            int SalaryCode = UDFLib.ConvertToInteger(cmdargs[3].ToString());
            int WageContractId = UDFLib.ConvertToInteger(cmdargs[4].ToString());

            if (ViewState["RowIndex"] != null)
                RowIndex = int.Parse(ViewState["RowIndex"].ToString());

            ViewState["WageContractId"] = WageContractId;

            if (Mode == "View")
            {
                btnSaveAmt.Enabled = false;
                ViewState["SalaryType"] = cmdargs[1].ToString();
                ViewState["PayAt"] = cmdargs[2].ToString();
            }
            else
            {
                btnSaveAmt.Enabled = true;
                ViewState["SalaryType"] = ((RadioButtonList)GridViewaddwages.Rows[RowIndex].FindControl("SalaryTypes")).SelectedValue;
                ViewState["PayAt"] = ((RadioButtonList)GridViewaddwages.Rows[RowIndex].FindControl("PayableAT")).SelectedValue;
            }
            PayableATList.SelectedValue = ViewState["PayAt"].ToString();
            SalaryTypeList.SelectedValue = ViewState["SalaryType"].ToString();

            Load_VesselType();

            if (ddlVesselType.SelectedIndex != 0)
            {
                VesselTypeID = UDFLib.ConvertToInteger(ddlVesselType.SelectedValue);
            }
            if (WageContractId == 0)
            {
                int Contract_Type = 0;
                int CountryID = 0;
                int RankScaleId = 0;
                if (RankScaleConsidered == 1)
                {
                    RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
                }
                if (NationalityConsidered == 1 && lblNationality.Items.Count > 0)
                {
                    CountryID = UDFLib.ConvertToInteger(lblNationality.Items[0].Value);
                }
                if (VesselFlagConsidered == 1 && lstFlags.Items.Count > 0)
                {
                    Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
                }
                DataSet ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, 0, NationalityConsidered, CountryID, RankScaleId);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                    WageContractId = int.Parse(dt1.Rows[0]["WageContractId"].ToString());
            }
            DataTable dtVesselSpecificWage = BLL_PortageBill.Get_VesselSpecificWage(0, WageContractId, SalaryCode);
            gvAllowance.DataSource = dtVesselSpecificWage;
            gvAllowance.DataBind();

            string msgdivResponseShow = string.Format("showModal('divVesselAllowance',true);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            pnlAddAmt.Update();
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnPopulateAllowance_OnClick(object sender, EventArgs e)
    {
        lblVesselAllowanceMessage.Text = "";
        foreach (GridViewRow gr in gvAllowance.Rows)
        {
            if ((TextBox)gr.FindControl("txtVesselAmount") != null)
            {
                ((TextBox)gr.FindControl("txtVesselAmount")).Text = txtVesselAmount.Text;
            }
        }
        string msgdivResponseShow = string.Format("showModal('divVesselAllowance',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        pnlAddAmt.Update();
    }
    protected void btnSaveAmt_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txteffdt.Text == "" || txteffdt.Text == null)
            {
                string js = "";
                js = "Please select Effective Date";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divCountryList',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                return;
            }
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("VesselID", typeof(int));
            dt1.Columns.Add("EntryType", typeof(int));
            dt1.Columns.Add("SalaryType", typeof(int));
            dt1.Columns.Add("PayAt", typeof(int));
            dt1.Columns.Add("Amount", typeof(decimal));

            int VesselTypeID = 0;
            int NationalityGroupId = 0;
            if (ViewState["NationalityGroupId"] != null)
                NationalityGroupId = int.Parse(ViewState["NationalityGroupId"].ToString());
            if (ddlVesselType.SelectedIndex != 0)
            {
                VesselTypeID = UDFLib.ConvertToInteger(ddlVesselType.SelectedValue);
            }
            DataTable dt = new DataTable();
            dt = BLL_PortageBill.Get_VesselList(VesselTypeID);
            double amt = 0;
            //   if (NationalityConsidered == 1)
            {
                // List<int> lstCountryList = new List<int>();
                foreach (GridViewRow gr in gvAllowance.Rows)
                {
                    if (((double.TryParse(((TextBox)gr.FindControl("txtVesselAmount")).Text, out amt))))
                    {
                        DataRow dr = dt1.NewRow();

                        dr["EntryType"] = UDFLib.ConvertToInteger(ViewState["EntryType"].ToString());
                        dr["SalaryType"] = UDFLib.ConvertToInteger(SalaryTypeList.SelectedValue);
                        dr["PayAt"] = UDFLib.ConvertToInteger(PayableATList.SelectedValue);
                        dr["Amount"] = amt;
                        dr["VesselID"] = UDFLib.ConvertToInteger(((Label)gr.FindControl("lblVesselId")).Text);
                        dt1.Rows.Add(dr);
                        dt1.AcceptChanges();
                    }
                }
            }

            bool saveStatus = Save(dt1);
            string msgdivResponseShow1;
            int RowIndex = UDFLib.ConvertToInteger(ViewState["RowIndex"].ToString());
            if (saveStatus == true)
            {
                lblVesselAllowanceMessage.Text = "Amount Updated";
                ((RadioButtonList)GridViewaddwages.Rows[RowIndex].FindControl("SalaryTypes")).SelectedValue = SalaryTypeList.SelectedValue.ToString();
                ((RadioButtonList)GridViewaddwages.Rows[RowIndex].FindControl("PayableAT")).SelectedValue = PayableATList.SelectedValue.ToString();


                msgdivResponseShow1 = string.Format("showModal('divVesselAllowance',true);");
            }
            else
            {
                msgdivResponseShow1 = string.Format("showModal('divVesselAllowance',true);");

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow1, true);
            pnlAddAmt.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlVesselType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtVesselAmount.Text = "";
        lblVesselAllowanceMessage.Text = "";
        if (ddlRank.SelectedItem != null)
        {
            int Contract_Type = 0, SalaryCode = 0;
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedItem.Value);
            int RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
            int CountryID = 0, WageContractId = 0;
            int VesselTypeID = 0;
            if (NationalityConsidered == 1 && lblNationality.SelectedItem != null)
            {
                CountryID = UDFLib.ConvertToInteger(lblNationality.SelectedItem.Value);
            }
            if (ddlVesselType.SelectedIndex != 0)
            {
                VesselTypeID = UDFLib.ConvertToInteger(ddlVesselType.SelectedValue);
            }
            if (ViewState["WageContractId"] != null)
            {
                WageContractId = int.Parse(ViewState["WageContractId"].ToString());
            }
            if (WageContractId == 0)
            {
                if (RankScaleConsidered == 1)
                {
                    RankScaleId = UDFLib.ConvertToInteger(ddlRankScale.SelectedItem.Value);
                }
                if (NationalityConsidered == 1 && lblNationality.Items.Count > 0)
                {
                    CountryID = UDFLib.ConvertToInteger(lblNationality.Items[0].Value);
                }
                if (VesselFlagConsidered == 1 && lstFlags.SelectedItem != null)
                {
                    Contract_Type = UDFLib.ConvertToInteger(lstFlags.SelectedItem.Value);
                }
                DataSet ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, 0, NationalityConsidered, CountryID, RankScaleId);
                DataTable dt1 = ds.Tables[1];
                if (dt1.Rows.Count > 0)
                    WageContractId = int.Parse(dt1.Rows[0]["WageContractId"].ToString());
            }
            if (ViewState["EntryType"] != null)
            {
                SalaryCode = int.Parse(ViewState["EntryType"].ToString());
            }
            if (SalaryCode > 0)
            {
                DataTable dtVesselSpecificWage = BLL_PortageBill.Get_VesselSpecificWage(VesselTypeID, WageContractId, SalaryCode);
                gvAllowance.DataSource = dtVesselSpecificWage;
                gvAllowance.DataBind();
            }
            string msgdivResponseShow = string.Format("showModal('divVesselAllowance',true);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            pnlAddAmt.Update();
        }

    }
}
