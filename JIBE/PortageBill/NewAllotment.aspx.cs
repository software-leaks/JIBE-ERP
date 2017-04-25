using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using SMS.Data.Infrastructure;


public partial class PortageBill_NewAllotment : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_Currency objCurr = new BLL_Infra_Currency();

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

        if (!IsPostBack)
        {
            UserAccessValidation();

            ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-1).Year.ToString(), DateTime.Now.AddYears(-1).Year.ToString()));
            ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(0).Year.ToString(), DateTime.Now.AddYears(0).Year.ToString()));
            ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(1).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString()));
            ddlYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.Text = DateTime.Now.Month.ToString();

            if (Request.QueryString["search"] != null)
            {
                if (Request.QueryString["search"].ToString() != "")
                {
                    txtSearchText.Text = Request.QueryString["search"].ToString();
                    Load_CrewList();
                }
            }
            if (Convert.ToInt16(Request.QueryString["prm"]) == 0)
            {
                //lblPageHeader.Text = "Allotment";
                //pnlSpecialAllotment.Visible = false;
            }
            else if (Convert.ToInt16(Request.QueryString["prm"]) == 1)
            {
                //lblPageHeader.Text = "Side Letter";
                //pnlSpecialAllotment.Visible = false;
            }
            else if (Convert.ToInt16(Request.QueryString["prm"]) == 2)
            {
                //lblPageHeader.Text = "Special Allotment";
                //pnlSpecialAllotment.Visible = true;
                //chkSpecialAllotment.Checked = true;
            }

        }
        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    protected string Bindvoyage_name(string voyage_name)
    {
        if (voyage_name != "")
        {
            string[] str = voyage_name.Split(new string[] { "to" }, StringSplitOptions.RemoveEmptyEntries);
            string[] str1 = str[0].Split('-');
            if (str1[1] != "")
                voyage_name = str1[0] + "-" + UDFLib.ConvertUserDateFormat(str1[1].ToString()) + " to " + str[1].ToString();
        }
        return voyage_name;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMessage.Text = "You don't have sufficient previlege to access the requested information.";
            pnlAllotment.Visible = false;
        }
        if (objUA.Add == 0)
        {
            lblMessage.Text = "You don't have sufficient previlege to access the requested information.";
            pnlAllotment.Visible = false;
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
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_CurrencyList()
    {
        DataTable dt = objCurr.Get_CurrencyList();

    }


    protected void Load_CrewList()
    {
        //DataTable dtFilters = new DataTable();
        //dtFilters.Columns.Add("VesselManager", typeof(int));
        //dtFilters.Columns.Add("Fleet", typeof(int));
        //dtFilters.Columns.Add("Vessel", typeof(int));
        //dtFilters.Columns.Add("RankID", typeof(int));
        //dtFilters.Columns.Add("Nationality", typeof(int));
        //dtFilters.Columns.Add("Status", typeof(String));
        //dtFilters.Columns.Add("ManningOfficeID", typeof(int));
        //dtFilters.Columns.Add("EOCDueIn", typeof(int));
        //dtFilters.Columns.Add("JoiningDateFrom", typeof(String));
        //dtFilters.Columns.Add("JoiningDateTo", typeof(String));
        //dtFilters.Columns.Add("SearchText", typeof(String));

        //DateTime dtFrom = DateTime.Parse("1900/01/01");
        //DateTime dtTo = DateTime.Parse("2900/01/01");

        //dtFilters.Rows.Add(
        //    1,
        //    0,
        //    0,
        //    0,
        //    0,
        //    "",
        //    0,
        //    0,
        //    dtFrom.ToString("yyyy/MM/dd"),
        //    dtTo.ToString("yyyy/MM/dd"),
        //    txtSearchText.Text);

        //int PAGE_SIZE = 100;
        //int PAGE_INDEX = 1;

        int SelectRecordCount = 1;

        //DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Index(dtFilters, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        string PBillDate = "01/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;

        DataTable dt = BLL_Crew_CrewList.Get_CrewList_Allotment(0, txtSearchText.Text, PBillDate, GetSessionUserID(), 100, 1, ref SelectRecordCount);
        gvSelectedCrew.DataSource = dt;
        gvSelectedCrew.DataBind();

    }

    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        Load_CrewList();
    }

    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CrewList();
    }

    protected void gvSelectedCrew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int CrewID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString());

            DropDownList ddlBankAcc = (DropDownList)e.Row.FindControl("ddlBankAcc");
            if (ddlBankAcc != null)
            {
                DataTable dt = SMS.Business.PortageBill.BLL_PortageBill.Get_Crew_BankAccList(CrewID);
                ddlBankAcc.DataSource = dt;
                ddlBankAcc.DataBind();
                if (dt.Rows.Count > 1)
                    ddlBankAcc.Enabled = true;
                else
                    ddlBankAcc.Enabled = false;
            }

            //DropDownList ddlVoyages = (DropDownList)e.Row.FindControl("ddlVoyages");
            //if (ddlVoyages != null)
            //{
            //    //DataTable dtV = objCrewBLL.Get_CrewLastVoyage(CrewID);                
            //    //ddlVoyages.DataSource = dtV;
            //    //ddlVoyages.DataBind();

            //    DataTable dtV = objCrewBLL.Get_CrewVoyages(CrewID, GetSessionUserID());
            //    dtV.DefaultView.RowFilter = ("Vessel_ID = " + Vessel_ID.ToString());
            //    ddlVoyages.DataSource = dtV.DefaultView;

            //    ddlVoyages.DataBind();
            //    if (dtV.Rows.Count > 1)
            //        ddlVoyages.Enabled = true;
            //    else
            //        ddlVoyages.Enabled = false;
            //}
        }
    }

    protected void gvSelectedCrew_RowCommand(object source, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "ADDSTAFF")
            {
                ViewState["IsClose"] = "N";

                string[] Arg = e.CommandArgument.ToString().Split(',');
                Button btnSelect = (Button)e.CommandSource;
                GridViewRow gr = (GridViewRow)btnSelect.Parent.Parent;
                //int Vessel_ID = UDFLib.ConvertToInteger(DataBinder.Eval(gr.DataItem, "Vessel_ID").ToString());

                string Amount = ((TextBox)gr.FindControl("txtAmount")).Text;
                string BankAcc = ((DropDownList)gr.FindControl("ddlBankAcc")).SelectedValue;
                string PBillDate = ddlYear.SelectedValue + "/" + ddlMonth.SelectedValue + "/01";

                int IsSpecialAllot = ((CheckBox)gr.FindControl("chkIsSpecial")).Checked == true ? 1 : 0;

                AddNewAllotment(UDFLib.ConvertToInteger(Arg[0]), UDFLib.ConvertToInteger(Arg[1]), UDFLib.ConvertToInteger(Arg[2]), UDFLib.ConvertToInteger(BankAcc), UDFLib.ConvertToDecimal(Amount), PBillDate, GetSessionUserID(), IsSpecialAllot);

            }
            if (e.CommandName.ToUpper() == "ADDPAXANDCLOSE")
            {
                ViewState["IsClose"] = "Y";
                //AddToDataTable(Convert.ToInt32(e.CommandArgument), 0, 0);
            }


        }
        catch { }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //int VesselID = UDFLib.ConvertToInteger(ddlVessels.SelectedValue);
            //int CrewID = UDFLib.ConvertToInteger(ddlCrew.SelectedValue);            
            //BLL_PB_PortageBill.INSERT_Allotment(VesselID, CrewID, ddlAccount.SelectedValue, txtdate.Text, UDFLib.ConvertToInteger(ddlCurrency.SelectedValue), Convert.ToDecimal(txtamount.Text), 0, GetSessionUserID());

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void AddNewAllotment(int CrewID, int VoyageID, int Vessel_ID, int BankAccID, decimal Amount, string PBill_Date, int UserID, int IsSpecialAllot)
    {
        string js = "";

        if (VoyageID == 0)
            js = "alert('Allotment can not be saved without voyage');";
        else if (BankAccID == 0)
            js = "alert('Allotment can not be saved without bank account');";
        else if (PBill_Date == "")
            js = "alert('Allotment can not be saved without portagebill date');";


        if (js.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
        else
        {


            int Res = BLL_PB_PortageBill.AddNew_Allotment(CrewID, VoyageID, Vessel_ID, BankAccID, Amount, PBill_Date, UserID, IsSpecialAllot);
            if (Res == 1)
            {
                js = "alert('New allotment added');parent.ReloadAllotment();";
            }
            else if (Res == -1)
            {
                js = "alert('Allotment for the same month already exists for the staff.');parent.ReloadAllotment();";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }
}