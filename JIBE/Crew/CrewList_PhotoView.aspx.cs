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

public partial class CrewList_PhotoView : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string vcode = "";
            int VesselID = 0;

            if (Request.QueryString["vcode"] != null)
            {
                vcode = Request.QueryString["vcode"].ToString();
                this.Title = "Crew List - " + vcode;
            }

            if (vcode != "")
            {
                pnlVessel.Visible = false;
                VesselID = objVessel.Get_VesselID_ByCode(vcode);
                FillGridViewAfterSearch(VesselID);
            }
            else
            {
                Load_VesselList();
            }

        }
        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    public void FillGridViewAfterSearch(int VesselID)
    {
        DataTable dtFilters = new DataTable();
        dtFilters.Columns.Add("VesselManager", typeof(int));
        dtFilters.Columns.Add("Fleet", typeof(int));
        dtFilters.Columns.Add("Vessel", typeof(int));
        dtFilters.Columns.Add("RankID", typeof(int));
        dtFilters.Columns.Add("Nationality", typeof(int));
        dtFilters.Columns.Add("Status", typeof(int));
        dtFilters.Columns.Add("CalculatedStatus", typeof(int));
        dtFilters.Columns.Add("ManningOfficeID", typeof(int));
        dtFilters.Columns.Add("EOCDueIn", typeof(int));
        dtFilters.Columns.Add("JoiningDateFrom", typeof(String));
        dtFilters.Columns.Add("JoiningDateTo", typeof(String));
        dtFilters.Columns.Add("SearchText", typeof(String));

        DateTime dtFrom = DateTime.Parse(UDFLib.ConvertUserDateFormat(Convert.ToString("1900/01/01")));
        DateTime dtTo = DateTime.Parse(UDFLib.ConvertUserDateFormat(Convert.ToString("2900/01/01")));

        int MainStatusId = 0;
        DataTable dtMainStatus = objCrewAdmin.Get_CrewMainStatus();
        dtMainStatus.DefaultView.RowFilter = "Value='Onboard'";
        if ( dtMainStatus.DefaultView.Count > 0 )
            MainStatusId = int.Parse(dtMainStatus.DefaultView[0]["Id"].ToString());

        dtFilters.Rows.Add(0, 0, VesselID, 0, 0, MainStatusId,0, 0, 0, dtFrom.ToString(UDFLib.ConvertUserDateFormat(Convert.ToString("yyyy/MM/dd"))), dtTo.ToString(UDFLib.ConvertUserDateFormat(Convert.ToString("yyyy/MM/dd"))), "");

        int PAGE_SIZE = 100;
        int PAGE_INDEX = 1;
        int SelectRecordCount = 0;

        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Index(dtFilters, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        ltCrewCount.Text = "Total number of crew on board : " + dt.Rows.Count.ToString();
        DataSet ds = BLL_Crew_CrewList.Get_Crewlist_IconView(VesselID, GetSessionUserID());

        ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[0].Columns["rank_category"], ds.Tables[1].Columns["rank_category"]));
        ds.Tables[1].TableName = "Members";

        rpt1.DataSource = ds;
        rpt1.DataBind();
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
                //DataRow dr = (DataRow)e.Item.DataItem;
                if (DataBinder.Eval(e.Item.DataItem, "CardType").ToString() == "")
                {
                    ImgCard.Visible = false;
                }
                else
                {
                    ImgCard.ImageUrl = "../images/" + DataBinder.Eval(e.Item.DataItem, "CardType").ToString().Replace(" ", "") + "_" + DataBinder.Eval(e.Item.DataItem, "CardsTATUS").ToString() + ".png";
                    ImgCard.Visible = true;
                    //ImgCard.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + DataBinder.Eval(e.Item.DataItem, "CardType").ToString() + " " + DataBinder.Eval(e.Item.DataItem, "CardsTATUS").ToString() + "] body=[Proposed by " + DataBinder.Eval(e.Item.DataItem, "ProposedBy").ToString() + "<br>" + DataBinder.Eval(e.Item.DataItem, "ProposedRemarks").ToString() + "]");
                    //ImgCard.Attributes.Add("onclick", "showDiv('dvProposeYellowCard','" + DataBinder.Eval(e.Item.DataItem, "ID").ToString() + "');");
                    
                }
            }

        }
        catch { }

    }

    public void Load_VesselList()
    {
        int Fleet_ID = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = 1;

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string PlannedInterviewID = DataBinder.Eval(e.Row.DataItem, "PlannedInterviewID").ToString();
            string strRemarks = DataBinder.Eval(e.Row.DataItem, "Voyage_Remarks").ToString();
            string CrewName = DataBinder.Eval(e.Row.DataItem, "staff_fullname").ToString();
            string ManningOfficeStatus = DataBinder.Eval(e.Row.DataItem, "ManningOfficeStatus").ToString();
            string CrewStatus = DataBinder.Eval(e.Row.DataItem, "CrewStatus").ToString();


            switch (CrewStatus)
            {
                case "CURRENT":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_Current";
                    break;
                case "COC DUE":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_SigningOff";
                    break;
                case "SIGNED OFF":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_SignedOff";
                    break;
                case "ASSIGNED":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_Assigned";
                    break;
                case "PLANNED":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_Planned";
                    break;
                case "PENDING":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_Pending";
                    break;
                case "NO VOYAGE":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_NoVoyage";
                    break;
                case "INACTIVE":
                    e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "CrewStatus_Inactive";
                    break;
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
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        FillGridViewAfterSearch(VesselID);
    }

    

    public string getVID()
    {
        int VesselID = 0;
        string vcode = "";

        if (Request.QueryString["vcode"] != null)
        {
            vcode = Request.QueryString["vcode"].ToString();
        }

        if (vcode != "")
        {
            VesselID = objVessel.Get_VesselID_ByCode(vcode);
        }

        return VesselID.ToString();
    }
}