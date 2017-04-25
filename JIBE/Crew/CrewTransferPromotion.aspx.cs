using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.PortageBill;
using SMS.Properties;


public partial class Crew_CrewTransferPromotion : System.Web.UI.Page
{
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_Port objPort = new BLL_Infra_Port();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_Contract objBLLInfra = new BLL_Crew_Contract();
    UserAccess objUA = new UserAccess();
    int RankScaleConsidered = 0;
    int NationalityConsidered = 0, Staff_Nationality = 0;
    protected decimal TotalAmount;
    protected decimal TotalAmountDeducted;
    public string DateFormat = "", TodayDateFormat = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/Account/Login.aspx");

        DateFormat = CalendarExtender4.Format = CalendarExtender2.Format = CalendarExtender15.Format = CalendarExtender1.Format = CalendarExtender3.Format = UDFLib.GetDateFormat();
        TodayDateFormat = UDFLib.DateFormatMessage();
        lblMessage.Text = "";

        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(GetCrewID());
        Staff_Nationality = int.Parse(dt.Rows[0]["Staff_Nationality"].ToString());

        DataTable dtWages = objCrewAdmin.GetWagesSettings();
        if (dtWages != null && dtWages.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]) == true)
                NationalityConsidered = 1;
            if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == true)
            {
                RankScaleConsidered = 1;
                trRankScale.Visible = true;
            }
            else
            {
                trRankScale.Visible = false;
            }
        }
        if (!IsPostBack)
        {
            UserAccessValidation();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
        DataTable dt = objBLLCrew.Get_Transfer_Promotions(GetCrewID(), VoyID, GetSessionUserID());

        if (dt.Rows.Count > 0)
        {
            lblMessage.Text = "The staff has a " + dt.Rows[0]["TransferType"].ToString() + " already planned in the system for the selected voyage";


            Load_CrewPersonalDetails(GetCrewID());
            Load_CurrentVoyageDetails(GetCrewID(), VoyID);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            rdoOptions.Enabled = false;
            pnlViewTransfer.Visible = true;
        }
        else
        {
            Load_FleetList();
            Load_VesselList();
            Load_RankList();
            Load_CrewPersonalDetails(GetCrewID());
            Load_CurrentVoyageDetails(GetCrewID(), VoyID);
            pnlViewTransfer.Visible = false;
            rdoOptions.ClearSelection();
            rdoOptions.Enabled = true;
            pnlSave.Visible = false;
            btnSaveVoyage.Enabled = true;
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

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSaveVoyage.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            btnSaveVoyage.Enabled = false;
        }
        if (objUA.Delete == 0)
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
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
    protected void GridViewaddwages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Key_Value = DataBinder.Eval(e.Row.DataItem, "Key_Value").ToString();
            if (Key_Value == "SENIORITYBONUS")
            {
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                if (txtAmount != null)
                {
                    txtAmount.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
            if (Key_Value == "COMPANY_SENIORITY_BONUS")
            {
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                if (txtAmount != null)
                {
                    txtAmount.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
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
    protected void rdoOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CurrentVoyageID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
        DataTable dtVoy = objBLLCrew.Get_CrewVoyages(GetCrewID(), CurrentVoyageID);

        pnlSave.Visible = true;

        if (dtVoy.Rows.Count > 0)
        {
            string Joining_Date = dtVoy.Rows[0]["Joining_Date"].ToString();
            int Joining_Rank = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Joining_Rank"].ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Vessel_ID"].ToString());
            string COCDate = dtVoy.Rows[0]["COCDate"].ToString();
            int Joining_Port = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Joining_Port"].ToString());
            string Sign_On_Date = dtVoy.Rows[0]["Sign_On_Date"].ToString();
            int RankScale = UDFLib.ConvertToInteger(dtVoy.Rows[0]["RankScaleId"].ToString());
            pnlTransfer.Visible = true;
            switch (rdoOptions.SelectedValue)
            {
                case "1":
                    ddlJoiningRank.SelectedValue = Joining_Rank.ToString();
                    ddlJoiningRank.Enabled = false;
                    ddlRankScale.Enabled = false;
                    //ddlVessel_Manager.Enabled = true;
                    ddlFleet.Enabled = true;
                    ddlVessel.Enabled = true;
                    pnlPromotion.Visible = false;
                    break;
                case "2":
                    ddlJoiningRank.Enabled = true;
                    ddlRankScale.Enabled = true;
                    //ddlVessel_Manager.Enabled = false;
                    ddlFleet.Enabled = false;
                    ddlVessel.SelectedValue = Vessel_ID.ToString();
                    VesselSelectionChange();
                    ddlVessel.Enabled = false;
                    View_CrewWages();
                    Edit_CrewWages();
                    pnlPromotion.Visible = true;
                    break;
                case "3":
                    ddlJoiningRank.Enabled = true;
                    ddlRankScale.Enabled = true;
                    //ddlVessel_Manager.Enabled = true;
                    ddlFleet.Enabled = true;
                    ddlVessel.Enabled = true;
                    View_CrewWages();
                    Edit_CrewWages();
                    pnlPromotion.Visible = true;
                    break;
            }
            int RankID = 0;
            if (ddlJoiningRank.SelectedValue != "0" && ddlJoiningRank.SelectedValue != "" && RankScaleConsidered == 1)
            {
                RankID = int.Parse(Joining_Rank.ToString());
                Load_RankScaleList(RankID);
                ddlRankScale.SelectedValue = RankScale.ToString();
            }
        }


    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    protected string Save_Wages(int CrewID, int VoyID, int Vessel_ID)
    {

        DataTable dtWagesINS = new DataTable();
        string sValidationMessage = "";

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

        bool IsValidated = false;

        foreach (GridViewRow gr in GridViewaddwages.Rows)
        {
            double amt = 0;

            int ii = Convert.ToInt32(GridViewaddwages.DataKeys[0].Value);

            if (double.TryParse(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text, out amt))
            {
                if (Convert.ToString(((TextBox)gr.Cells[4].FindControl("txtAmount")).Text) != "")
                {
                    IsValidated = true;
                    break;
                }
            }

        }

        DataSet ds = BLL_PortageBill.Get_CrewWageContract(CrewID, VoyID, GetSessionUserID());
        if (ds.Tables[1].Rows.Count > 0)
        {
            if (txteffdt.Text != "")
            {
                if (ds.Tables[1].Rows[0]["effective_date"].ToString() != "")
                {
                    if (DateTime.Parse(txteffdt.Text) < DateTime.Parse(ds.Tables[1].Rows[0]["effective_date"].ToString()))
                    {
                        IsValidated = false;
                        sValidationMessage = "Wages can not be saved as the new effective date can not be prior to the previous effective date.";
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
                        string efdt = txteffdt.Text.Trim().ToString();
                        dt = DateTime.Parse(efdt);

                    }
                    else
                    {
                        string efdt = "1900/01/01";
                        dt = DateTime.Parse(efdt);
                    }

                    string sentry = ((Label)GridViewaddwages.Rows[gr.RowIndex].FindControl("lblentry_type")).Text;

                    currusd = 5;

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
                    dr["Effective_Date"] = dt.ToString(Convert.ToString(Session["User_DateFormat"]));
                    dr["ID"] = 0;

                    dtWagesINS.Rows.Add(dr);

                    sValidationMessage = "Salary Updated.";
                }

            }
            int WageContractId = 0;
            if (hdWageContractId.Value != null && hdWageContractId.Value.ToString() != "")
                WageContractId = int.Parse(hdWageContractId.Value.ToString());

            BLL_PortageBill.Ins_CrewWages(dtWagesINS, WageContractId);
        }

        return sValidationMessage;

    }

    protected void txtJoiningDate_TextChanged(object sender, EventArgs e)
    {
        txteffdt.Text = txtJoiningDate.Text;
        if (rdoOptions.SelectedValue == "2")
        {
            txtSignOnDate.Text = txtJoiningDate.Text;
        }
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

    protected void ddlRankScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
        int RankId = int.Parse(ddlJoiningRank.SelectedValue.ToString());
        int Contract_Type = 0;
        int VesselId = int.Parse(ddlVessel.SelectedValue.ToString());
        DataSet ds = BLL_PortageBill.Get_CrewWagesByVoyage(GetCrewID(), VoyID);
        DataTable dtVoy = objBLLCrew.Get_CrewVoyages(GetCrewID(), VoyID);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            Contract_Type = int.Parse(ds.Tables[0].Rows[0]["Contract_Type"].ToString());
        }
        if (dtVoy != null && dtVoy.Rows.Count > 0 && VesselId == 0)
        {
            VesselId = int.Parse(dtVoy.Rows[0]["Vessel_ID"].ToString());
        }
        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(GetCrewID());
        int RankScaleId = int.Parse(ddlRankScale.SelectedValue.ToString());
        int CountryId = int.Parse(dt.Rows[0]["Staff_Nationality"].ToString());
        Staff_Nationality = int.Parse(dt.Rows[0]["Staff_Nationality"].ToString());
        int WageContractId = 0;
        DataSet ds1 = BLL_PortageBill.Get_Rank_WageContract(RankId, Contract_Type, WageContractId, NationalityConsidered, CountryId, RankScaleId, VesselId);
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
    public void Load_FleetList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int Vessel_Manager = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID, Vessel_Manager);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("- SELECT -", "0"));
    }
    public void Load_VesselList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = 0;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("- SELECT -", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    public void Load_RankList()
    {

        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlJoiningRank.DataSource = dt;
        ddlJoiningRank.DataTextField = "Rank_Short_Name";
        ddlJoiningRank.DataValueField = "ID";
        ddlJoiningRank.DataBind();
        ddlJoiningRank.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["CrewID"] != null)
            {
                return int.Parse(Request.QueryString["CrewID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    protected void Load_CrewPersonalDetails(int ID)
    {
        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
        if (dt.Rows.Count > 0)
        {
            string RankID = dt.Rows[0]["CurrentRankID"].ToString();
            lblStaffName.Text = dt.Rows[0]["STAFF_FULLNAME"].ToString();
            lblStaffCode.Text = dt.Rows[0]["STAFF_CODE"].ToString();
            lblRank.Text = dt.Rows[0]["RANK_short_NAME"].ToString();
            hdnCrewrank.Value = dt.Rows[0]["CurrentRankID"].ToString();

            ddlJoiningRank.Text = RankID;
        }
    }
    protected void Load_CurrentVoyageDetails(int CrewID, int VoyageID)
    {
        DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, VoyageID);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Joining_Date"].ToString() != "")
                lblContractDate.Text = DateTime.Parse(dt.Rows[0]["Joining_Date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));

            if (dt.Rows[0]["sign_on_date"].ToString() != "")
                lblSignOn.Text = DateTime.Parse(dt.Rows[0]["sign_on_date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));

            if (dt.Rows[0]["sign_off_date"].ToString() != "")
                lblCOC.Text = DateTime.Parse(dt.Rows[0]["sign_off_date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));
            else if (dt.Rows[0]["COCDate"].ToString() != "")
            {
                lblCOC.Text = DateTime.Parse(dt.Rows[0]["COCDate"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));
                txtCOCDate.Text = DateTime.Parse(dt.Rows[0]["COCDate"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));
            }
        }
    }

    protected void View_CrewWages()
    {
        int CrewID = GetCrewID();
        int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);

        DataSet ds = BLL_PortageBill.Get_CrewWagesByVoyage(CrewID, VoyID);
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            ds.Relations.Add(new DataRelation("Parent", ds.Tables[0].Columns["Effective_Date"], ds.Tables[1].Columns["Effective_Date"]));
            ds.Tables[1].TableName = "Child";

            rpt1.DataSource = ds;
            rpt1.DataBind();
        }
    }

    protected void Edit_CrewWages()
    {
        int RankID = 0;
        int VesselId = int.Parse(ddlVessel.SelectedValue.ToString());
        if (ddlJoiningRank.SelectedValue != "0" && ddlJoiningRank.SelectedValue != "")
        {
            RankID = int.Parse(ddlJoiningRank.SelectedValue);
        }
        if (RankScaleConsidered == 1)
        {
            Load_RankScaleList(RankID);
        }
        // load rank based salary 

        int CurrentVoyageID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
        int Contract_Type = 0, CountryId = 0, RankScaleId = 0;
        try
        {
            //Get Current voyage details
            DataTable dtVoy = objBLLCrew.Get_CrewVoyages(GetCrewID(), CurrentVoyageID);

            if (dtVoy.Rows.Count > 0)
            {
                Contract_Type = int.Parse(dtVoy.Rows[0]["Vessel_Flag"].ToString());
            }
            DataSet ds = new DataSet();
            if (RankScaleConsidered == 0)
            {
                ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, 0, NationalityConsidered, CountryId, RankScaleId, VesselId);
                hdWageContractId.Value = ds.Tables[1].Rows[0]["WageContractID"].ToString();
                GridViewaddwages.DataSource = ds.Tables[1];
                GridViewaddwages.DataBind();
            }
            else
            {
                GridViewaddwages.DataSource = null;
                GridViewaddwages.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
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
                TotalAmount = 0;
                TotalAmountDeducted = 0;
            }
        }

    }

    protected void btnSaveVoyage_Click(object sender, EventArgs e)
    {
        string Message = "";
        string Joining_Date = "";
        int Joining_Rank = 0, CurrentRankId = 0, CurrentRankScaleId = 0;
        int Vessel_ID = 0;
        string COCDate = "";
        int Joining_Port = 0;
        string Sign_On_Date = "";

        int Join_Type = UDFLib.ConvertToInteger(rdoOptions.SelectedValue);
        int CurrentVoyageID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
        int New_Voy_ID = 0, RankScaleId = 0;

        try
        {
            //Get Current voyage details
            DataTable dtVoy = objBLLCrew.Get_CrewVoyages(GetCrewID(), CurrentVoyageID);

            if (dtVoy.Rows.Count > 0)
            {
                Joining_Date = dtVoy.Rows[0]["Joining_Date"].ToString();
                Joining_Rank = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Joining_Rank"].ToString());
                Vessel_ID = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Vessel_ID"].ToString());
                COCDate = dtVoy.Rows[0]["COCDate"].ToString();
                Joining_Port = UDFLib.ConvertToInteger(dtVoy.Rows[0]["Joining_Port"].ToString());
                Sign_On_Date = dtVoy.Rows[0]["Sign_On_Date"].ToString();
                RankScaleId = UDFLib.ConvertToInteger(dtVoy.Rows[0]["RankScaleId"].ToString());
                CurrentRankId = Joining_Rank;
                CurrentRankScaleId = RankScaleId;
            }

            if (rdoOptions.SelectedValue == "1")
            {
                if (txtJoiningDate.Text.Trim() == "" || txtSignOnDate.Text.Trim() == "" || UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) == 0
                    || UDFLib.ConvertToInteger(ddlVessel.SelectedValue) == 0 || txtCOCDate.Text == "" || txtSignOffDate.Text == "" || UDFLib.ConvertToInteger(ctlSignOffPort.SelectedValue) == 0
                    || UDFLib.ConvertToInteger(ctlJoiningPort.SelectedValue) == 0 || UDFLib.ConvertToInteger(ddlContract.SelectedValue) == 0)
                {
                    Message = "Vessel Name,Contract Template, Joining Rank, Contract Date, Sign-On Date, Sign-Off-Date, Sign-Off-Port, Joining Port and EOC Date are mandatory fields. You might have missed one of the fields.";
                }
                else if (UDFLib.ConvertToDate(txtJoiningDate.Text) < UDFLib.ConvertDateToNull(COCDate))
                {
                    Message = "Contract date should be greater than the EOC of previous voyage";
                }
                else if (txtJoiningDate.Text.Trim() != "" && txtSignOnDate.Text != "" && UDFLib.ConvertToDate(txtJoiningDate.Text) > UDFLib.ConvertToDate(txtSignOnDate.Text))
                {
                    Message = "Sign-On Date can not be less than the Contract date";
                }
                else if (UDFLib.ConvertToDate(txtSignOffDate.Text) < DateTime.Today.AddDays(1))
                {
                    Message = "The transfer has to be planned atleast 1 day in advance";
                }
                else if (UDFLib.ConvertToDate(txtSignOffDate.Text) > UDFLib.ConvertToDate(txtSignOnDate.Text))
                {
                    Message = "The sign-off/sign-on dates are incorrect. Sign-off has to happen before the next sign-on";
                }
                else if (Vessel_ID == UDFLib.ConvertToInteger(ddlVessel.SelectedValue))
                {
                    Message = "Please select the new vessel different from the staff's current vessel.";
                }
                else
                {
                    Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
                    Joining_Rank = UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue);

                    New_Voy_ID = objBLLCrew.TransferCrew(CurrentVoyageID, GetCrewID(), Vessel_ID, Join_Type, Joining_Rank, UDFLib.ConvertToDate(txtJoiningDate.Text).ToShortDateString(), UDFLib.ConvertToDate(txtSignOnDate.Text).ToShortDateString(), UDFLib.ConvertToInteger(ctlJoiningPort.SelectedValue), UDFLib.ConvertToDate(txtCOCDate.Text).ToShortDateString(), GetSessionUserID(), UDFLib.ConvertToDate(txtSignOffDate.Text).ToShortDateString(), UDFLib.ConvertToInteger(ctlSignOffPort.SelectedValue), UDFLib.ConvertToInteger(ddlContract.SelectedValue));
                    if (New_Voy_ID > 0)
                    {
                        Message = "Crew transfer planned";
                        btnSaveVoyage.Enabled = false;
                    }
                    else
                    {
                        Message = "The transfer is already planned for the crew";
                    }
                }
            }
            if (rdoOptions.SelectedValue == "2")
            {
                if (txtJoiningDate.Text.Trim() == "" || txtSignOnDate.Text.Trim() == "" || UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) == 0 || txtCOCDate.Text == "" || UDFLib.ConvertToInteger(ddlContract.SelectedValue) == 0)
                {
                    Message = "Contract Template,Joining Rank, Contract Date, Sign-On Date and EOC Date are mandatory fields. You might have missed one of the fields.";
                }
                else if (Joining_Rank == UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) && RankScaleConsidered == 1 && RankScaleId == UDFLib.ConvertToInteger(ddlRankScale.SelectedValue))
                {
                    Message = "Promoted rank scale should not be the same as the current rank scale.";
                }
                else if (Joining_Rank == UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) && RankScaleConsidered == 0)
                {
                    Message = "Promoted rank should not be the same as the current rank.";
                }
                else if (UDFLib.ConvertDateToNull(txtJoiningDate.Text) < UDFLib.ConvertDateToNull(COCDate))
                {
                    Message = "Contract date should be greater than the EOC of previous voyage";
                }
                else if (txtJoiningDate.Text.Trim() != "" && txtSignOnDate.Text != "" && UDFLib.ConvertDateToNull(txtJoiningDate.Text) > UDFLib.ConvertDateToNull(txtSignOnDate.Text))
                {
                    Message = "Sign-On Date can not be less than the Contract date";
                }
                else if (UDFLib.ConvertDateToNull(txtSignOnDate.Text) < DateTime.Today.AddDays(1))
                {
                    Message = "The promotion has to be planned atleast 1 day in advance";
                }
                else
                {
                    Joining_Rank = UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue);

                    int NewWageContractId = 0;
                    if (hdWageContractId.Value != null && hdWageContractId.Value.ToString() != "")
                        NewWageContractId = int.Parse(hdWageContractId.Value.ToString());

                    New_Voy_ID = objBLLCrew.TransferCrew(CurrentVoyageID, GetCrewID(), Vessel_ID, Join_Type, Joining_Rank, UDFLib.ConvertToDate(txtJoiningDate.Text).ToShortDateString(), UDFLib.ConvertToDate(txtSignOnDate.Text).ToShortDateString(), UDFLib.ConvertToInteger(ctlJoiningPort.SelectedValue), UDFLib.ConvertToDate(txtCOCDate.Text).ToShortDateString(), GetSessionUserID(), "", 0, NewWageContractId, UDFLib.ConvertToInteger(ddlContract.SelectedValue));
                    if (New_Voy_ID > 0)
                    {
                        Message = "Staff promotion planned";

                        Message = Message + Save_Wages(GetCrewID(), New_Voy_ID, Vessel_ID);
                        btnSaveVoyage.Enabled = false;
                        if ((CurrentRankId != Joining_Rank) || (RankScaleConsidered == 1 && CurrentRankScaleId != UDFLib.ConvertToInteger(ddlRankScale.SelectedValue)))
                        {
                            //Save entry in Log for change in Wage Rank Scale
                            BLL_PortageBill.Ins_CrewChangeWageLog(CurrentRankId, UDFLib.ConvertToInteger(ddlRankScale.SelectedValue.ToString()), GetCrewID(), GetSessionUserID());
                        }
                    }
                    else
                    {
                        Message = "The transfer is already planned for the crew";
                    }
                }
            }
            if (rdoOptions.SelectedValue == "3")
            {
                if (UDFLib.ConvertToInteger(ddlVessel.SelectedValue) == 0 || txtJoiningDate.Text.Trim() == "" || txtSignOnDate.Text.Trim() == "" || UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) == 0
                    || txtCOCDate.Text == "" || txtSignOffDate.Text == "" || UDFLib.ConvertToInteger(ctlSignOffPort.SelectedValue) == 0 || UDFLib.ConvertToInteger(ddlContract.SelectedValue) == 0)
                {
                    Message = "Vessel Name,Contract Template, Joining Rank, Contract Date, Sign-On Date, Sign-Off-Date, Sign-Off-Port and EOC Date are mandatory fields. You might have missed one of the fields.";
                }
                else if (Joining_Rank == UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) && RankScaleConsidered == 1 && RankScaleId == UDFLib.ConvertToInteger(ddlRankScale.SelectedValue))
                {
                    Message = "Promoted rank scale should not be the same as the current rank scale.";
                }
                else if (Joining_Rank == UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue) && RankScaleConsidered == 0)
                {
                    Message = "Promoted rank should not be the same as the current rank.";
                }
                else if (Vessel_ID == UDFLib.ConvertToInteger(ddlVessel.SelectedValue))
                {
                    Message = "Please select the new vessel different from the staff's current vessel.";
                }

                else if (UDFLib.ConvertDateToNull(txtJoiningDate.Text) < UDFLib.ConvertDateToNull(COCDate))
                {
                    Message = "Contract date should be greater than the EOC of previous voyage";
                }
                else if (txtJoiningDate.Text.Trim() != "" && txtSignOnDate.Text != "" && UDFLib.ConvertDateToNull(txtJoiningDate.Text) > UDFLib.ConvertDateToNull(txtSignOnDate.Text))
                {
                    Message = "Sign-On Date can not be less than the Contract date";
                }
                else if (UDFLib.ConvertDateToNull(txtSignOffDate.Text) < DateTime.Today.AddDays(1))
                {
                    Message = "The transfer has to be planned atleast 1 day in advance";
                }
                else if (UDFLib.ConvertDateToNull(txtSignOffDate.Text) > UDFLib.ConvertDateToNull(txtSignOnDate.Text))
                {
                    Message = "The sign-off/sign-on dates are incorrect. Sign-off has to happen before the next sign-on";
                }
                else
                {
                    Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
                    Joining_Rank = UDFLib.ConvertToInteger(ddlJoiningRank.SelectedValue);
                   New_Voy_ID = objBLLCrew.TransferCrew(CurrentVoyageID, GetCrewID(), Vessel_ID, Join_Type, Joining_Rank, UDFLib.ConvertToDate(txtJoiningDate.Text).ToShortDateString(), UDFLib.ConvertToDate(txtSignOnDate.Text).ToShortDateString(), UDFLib.ConvertToInteger(ctlJoiningPort.SelectedValue), UDFLib.ConvertToDate(txtCOCDate.Text).ToShortDateString(), GetSessionUserID(), UDFLib.ConvertToDate(txtSignOffDate.Text).ToShortDateString(), UDFLib.ConvertToInteger(ctlSignOffPort.SelectedValue), UDFLib.ConvertToInteger(ddlContract.SelectedValue));
                 

                    if (New_Voy_ID > 0)
                    {
                        Message = "Staff transfer promotion planned";
                        Message = Message + Save_Wages(GetCrewID(), New_Voy_ID, Vessel_ID);
                        btnSaveVoyage.Enabled = false;
                        if ((CurrentRankId != Joining_Rank) || (RankScaleConsidered == 1))
                        {
                            //Save entry in Log for change in Wage Rank Scale
                            if (RankScaleConsidered == 1 && CurrentRankScaleId != UDFLib.ConvertToInteger(ddlRankScale.SelectedValue))
                                BLL_PortageBill.Ins_CrewChangeWageLog(CurrentRankId, UDFLib.ConvertToInteger(ddlRankScale.SelectedValue.ToString()), GetCrewID(), GetSessionUserID());
                            else
                                BLL_PortageBill.Ins_CrewChangeWageLog(CurrentRankId, 0, GetCrewID(), GetSessionUserID());
                        }
                    }
                    else
                    {
                        Message = "The transfer is already planned for the crew";
                    }
                }
            }

            if (New_Voy_ID > 0)
            {
                int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
                DataTable dt = objBLLCrew.Get_Transfer_Promotions(GetCrewID(), VoyID, GetSessionUserID());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                pnlViewTransfer.Visible = true;
                pnlTransfer.Visible = false;
                pnlPromotion.Visible = false;

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        if (Message.Length > 0)
        {
            lblMessage.Text = Message;
        }

    }
    protected void btnCancelVoyage_Click(object sender, EventArgs e)
    {
        //pnlTransfer.Visible = false;
    }

    protected void ddlJoiningRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RankID = 0;
        int VesselId = int.Parse(ddlVessel.SelectedValue.ToString());
        if (ddlJoiningRank.SelectedValue != "0" && ddlJoiningRank.SelectedValue != "")
        {
            RankID = int.Parse(ddlJoiningRank.SelectedValue);
        }
        if (RankScaleConsidered == 1)
        {
            Load_RankScaleList(RankID);
        }
        // load rank based salary 

        int CurrentVoyageID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
        int WageContractID = 0;
        int Contract_Type = 0, CountryId = 0, RankScaleId = 0;
        try
        {
            //Get Current voyage details
            DataTable dtVoy = objBLLCrew.Get_CrewVoyages(GetCrewID(), CurrentVoyageID);

            if (dtVoy.Rows.Count > 0)
            {
                Contract_Type = int.Parse(dtVoy.Rows[0]["Vessel_Flag"].ToString());
            }
            DataSet ds = new DataSet();
            if (RankScaleConsidered == 0)
            {
                ds = BLL_PortageBill.Get_Rank_WageContract(RankID, Contract_Type, 0, NationalityConsidered, CountryId, RankScaleId, VesselId);
                hdWageContractId.Value = ds.Tables[1].Rows[0]["WageContractID"].ToString();
                GridViewaddwages.DataSource = ds.Tables[1];
                GridViewaddwages.DataBind();
            }
            else
            {
                GridViewaddwages.DataSource = null;
                GridViewaddwages.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void Load_RankScaleList(int RankId)
    {
        //DataTable dt = objCrewAdmin.Get_RankScaleList(RankId);
        DataTable dt;
        if (NationalityConsidered == 1)
            dt = objCrewAdmin.Get_RankScaleListForWages(RankId, Staff_Nationality);
        else
            dt = objCrewAdmin.Get_RankScaleListForWages(RankId, 0);

        ddlRankScale.DataSource = dt;
        ddlRankScale.DataTextField = "RankScaleName";
        ddlRankScale.DataValueField = "ID";
        ddlRankScale.DataBind();
        ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void txtSignOnDate_TextChanged(object sender, EventArgs e)
    {
        //txteffdt.Text = txtSignOnDate.Text;
    }
    protected void amount_text_changed(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        decimal v1 = UDFLib.ConvertToDecimal(txt.CssClass);
        decimal v2 = UDFLib.ConvertToDecimal(txt.Text);
        if (v2 < v1)
        {
            txt.BackColor = System.Drawing.Color.Red;
            txt.ForeColor = System.Drawing.Color.White;
        }
        else
        {
            txt.BackColor = System.Drawing.Color.White;
            txt.ForeColor = System.Drawing.Color.Black;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Sign_On_date = DataBinder.Eval(e.Row.DataItem, "Sign_On_date").ToString();
            string Sign_Off_date = DataBinder.Eval(e.Row.DataItem, "Sign_Off_date").ToString();

            if (UDFLib.ConvertDateToNull(Sign_On_date) <= DateTime.Today && UDFLib.ConvertDateToNull(Sign_Off_date) <= DateTime.Today)
            {
                ImageButton lnkDelete = (ImageButton)e.Row.FindControl("lnkDelete");
                if (lnkDelete != null)
                    lnkDelete.Visible = false;
            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string js = "";
        int Transfer_ID = UDFLib.ConvertToInteger(e.Keys[0]);
        if (Transfer_ID > 0)
        {
            int Res = objBLLCrew.Delete_Transfer_Planning(Transfer_ID, GetSessionUserID());
            if (Res == 1)
            {
                BindGrid();
                js = "alert('Transfer / Promotion record deleted.')";
            }
            else if (Res == -1)
                js = "alert('Transfer / Promotion record can not be deleted as the staff is already signed on to the planned voyage.')";
            else if (Res == -2)
                js = "alert('Transfer / Promotion record can not be deleted as the staff is already signed off from the current voyage.')";
            else
                js = "alert('Transfer / Promotion record could not be deleted due to unknown reason.')";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);
        }
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        VesselSelectionChange();
    }

    protected void VesselSelectionChange()
    {
        int VesselFlag = 0;
        int VesselId = int.Parse(ddlVessel.SelectedValue);
        DataTable dt1 = objVessel.GetVesselDetails_ByID(VesselId);
        if (dt1.Rows.Count > 0)
            VesselFlag = int.Parse(dt1.Rows[0]["L_Vessel_Flag"].ToString());

        DataTable dtContract = objBLLInfra.Get_CrewContractList(Staff_Nationality, VesselFlag);

        ddlContract.DataSource = dtContract;
        ddlContract.DataTextField = "Contract_Name";
        ddlContract.DataValueField = "ContractId";
        ddlContract.DataBind();
        ddlContract.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
}