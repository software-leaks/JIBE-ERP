using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;

public partial class Crew_CrewListHistory : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    BLL_Crew_CrewList objCrewlst = new BLL_Crew_CrewList();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    public string DateFormat = "";
    public string TodayDateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format
       
        TodayDateFormat = UDFLib.DateFormatMessage();
        if (!IsPostBack)
        {
            BindVesselDLL();
            Load_CountryList();
            Load_RankList();

            txtSearchJoinFromDate.Text = DateTime.Now.ToString(Convert.ToString(Session["User_DateFormat"]), iFormatProvider);
            txtSearchJoinToDate.Text = DateTime.Now.ToString(Convert.ToString(Session["User_DateFormat"]), iFormatProvider);

            DDLVessel.SelectedValue = "0";
            TabCrwHistory.TabIndex = 1;

            if (!string.IsNullOrWhiteSpace(Request.QueryString["VesselID"]))
                DDLVessel.SelectedValue = Request.QueryString["VesselID"];

            ucCustomPager_CrewList.PageSize = 30;
            ucCustomPager1.PageSize = 30;
            BindCrewHistoryGrid();
        }
        string msgmodal = String.Format("Regsisterfunction();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refundclose", msgmodal, true);
    }
    public void Load_CountryList()
    {
        ddlCountry.DataSource = objCrew.Get_CrewNationality(GetSessionUserID());
        ddlCountry.DataTextField = "COUNTRY";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCountry.SelectedIndex = 0;
    }
    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;
    }

    public void BindVesselDLL()
    {
        int companyid;

        if (Session["USERCOMPANYID"] == null)
            companyid = 0;
        else
            companyid = Convert.ToInt32(Session["USERCOMPANYID"].ToString());

        DataTable dtVessel = objVessel.Get_VesselList(0, 0, companyid, "", companyid);

        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_id";
        DDLVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLVessel.Items.Insert(0, li);    
    }

    public void BindCrewHistoryGrid()
    {
        DateTime dtFrom = DateTime.Parse("1900/01/01");
        DateTime dtTo = DateTime.Parse("2900/01/01");

       
        if (txtSearchJoinFromDate.Text != "")
            dtFrom = UDFLib.ConvertToDate(Convert.ToString(txtSearchJoinFromDate.Text), UDFLib.GetDateFormat());

        if (txtSearchJoinToDate.Text != "")
            dtTo = UDFLib.ConvertToDate(Convert.ToString(txtSearchJoinToDate.Text), UDFLib.GetDateFormat());

        int PAGE_SIZE = ucCustomPager_CrewList.PageSize;
        int PAGE_INDEX = ucCustomPager_CrewList.CurrentPageIndex;

        int PAGE_SIZE1 = ucCustomPager1.PageSize;
        int PAGE_INDEX1 = ucCustomPager1.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager_CrewList.isCountRecord;
        int SelectRecordCount1 = ucCustomPager1.isCountRecord;

        DataSet ds = BLL_Crew_CrewList.Get_Crewlist_History(Convert.ToInt32(DDLVessel.SelectedValue), UDFLib.ConvertToInteger(ddlCountry.SelectedValue.ToString()), UDFLib.ConvertToInteger(ddlRank.SelectedValue.ToString()),
                                                            dtFrom, dtTo, PAGE_SIZE, PAGE_INDEX, PAGE_SIZE1, PAGE_INDEX1, ref SelectRecordCount, ref SelectRecordCount1);
        gvCrwHistroy.DataSource = ds.Tables[0];
        gvCrwHistroy.DataBind();

        if (ucCustomPager_CrewList.isCountRecord == 1)
        {
            ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewList.BuildPager();
        }


        if (ucCustomPager1.isCountRecord == 1)
        {
            ucCustomPager1.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager1.BuildPager();
        }

        ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[1].Columns["rank_category"], ds.Tables[2].Columns["rank_category"]));
        ds.Tables[2].TableName = "Members";

        rpt1.DataSource = ds.Tables[1];
        rpt1.DataBind();

        if (ucCustomPager_CrewList.isCountRecord == 1)
        {
            ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewList.BuildPager();
        }

        if (ucCustomPager1.isCountRecord == 1)
        {
            ucCustomPager1.CountTotalRec = SelectRecordCount1.ToString();
            ucCustomPager1.BuildPager();
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindCrewHistoryGrid();
        }
        catch(Exception ex)
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

    protected void rpt1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        DataRowView dv = e.Item.DataItem as DataRowView;
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {
                 nestedRepeater.DataSource = dv.CreateChildView("NestedCat");
                nestedRepeater.DataBind();
            }
        }
    }
    protected void rpt2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            Image ImgCard = ((ImageButton)e.Item.FindControl("ImgCard"));
            if (ImgCard != null)
            {
                
                if (DataBinder.Eval(e.Item.DataItem, "CardType").ToString() == "")
                {
                    ImgCard.Visible = false;
                }
                else
                {
                    ImgCard.ImageUrl = "../images/" + DataBinder.Eval(e.Item.DataItem, "CardType").ToString().Replace(" ", "") + "_" + DataBinder.Eval(e.Item.DataItem, "CardsTATUS").ToString() + ".png";
                    ImgCard.Visible = true;      
                }
            }
        }
        catch { }
    }
    protected void lnbHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/crew/default.aspx");
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        ddlCountry.SelectedIndex = 0;
        DDLVessel.SelectedIndex = 0;
        ddlRank.SelectedIndex = 0;
        BindCrewHistoryGrid();
    }
}