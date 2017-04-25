using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.Infrastructure;


public partial class CrewEvaluation_EvaluationSchedules : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();
            Load_RankList();
            //Load_EvaluationTypeList();
            Bind_EvaluationSchedules();

        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlRank.SelectedIndex = 0;
    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_EvaluationSchedules();
    }

    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Bind_EvaluationSchedules();
    }

    protected void Bind_EvaluationSchedules()
    {
        if (hdnStartMonth.Value == "")
            hdnStartMonth.Value = "-5";

        int StartMonth = UDFLib.ConvertToInteger(hdnStartMonth.Value);

        int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
        int Eva_Type = 0; // UDFLib.ConvertToInteger(ddlEvaluationType.SelectedValue);
        
        DataTable dt = BLL_Crew_Evaluation.Get_EvaluationScheduleByVessel(VesselID, RankID, StartMonth, Eva_Type, txtfilter.Text);

        DataView dataView = new DataView(dt);
        try
        {
            if (ViewState["sortExpression"] != null)
                dataView.Sort = ViewState["sortExpression"].ToString();
        }
        catch { }

        //GridView_Evaluation.DataSource = dataView;
        //GridView_Evaluation.DataBind();

        if (dataView.Count > 0)
        {
            tdNavigator.Visible = true;
            GridView_Evaluation.DataSource = dataView;
            GridView_Evaluation.DataBind();
        }
        else
        {
            tdNavigator.Visible = false;
            GridView_Evaluation.DataSource = null;
            GridView_Evaluation.DataBind();
        }
       // lblSelection.Text = DateTime.Today.AddMonths(StartMonth).ToString("MMM/yyyy") + " to " + DateTime.Today.AddMonths(StartMonth + 11).ToString("MMM/yyyy");
        lblSelection.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(DateTime.Today.AddMonths(StartMonth))) + " to " + UDFLib.ConvertUserDateFormat(Convert.ToString(DateTime.Today.AddMonths(StartMonth + 11)));

    }

    protected void GridView_Evaluation_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["pageindex"] = GridView_Evaluation.PageIndex;

        if (ViewState["sortDirection"] == null)
        {
            ViewState["sortDirection"] = SortDirection.Ascending;
            ViewState["sortExpression"] = e.SortExpression;
        }
        else
        {
            if ((SortDirection)ViewState["sortDirection"] == SortDirection.Ascending)
            {
                ViewState["sortDirection"] = SortDirection.Descending;
                ViewState["sortExpression"] = e.SortExpression + " DESC";
            }
            else
            {
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["sortExpression"] = e.SortExpression;
            }

        }

        Bind_EvaluationSchedules();
    }
    protected void GridView_Evaluation_Sorted(object sender, EventArgs e)
    {
        GridView_Evaluation.PageIndex = Convert.ToInt32(Session["pageindex"]);

    }
    protected void GridView_Evaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int FixCellCount = 9;

        e.Row.Cells[0].Visible = false;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string CrewID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();

            DateTime S_ON_Dt = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "Sign_On_Date").ToString());
            DateTime COC_Dt = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "COC").ToString());
            DateTime St_Dt = DateTime.Parse(DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "StDate").ToString()).ToString("dd/MM/yyyy"));
            DateTime End_Dt = DateTime.Parse(DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "EndDate").ToString()).ToString("dd/MM/yyyy"));
            DateTime Cell_Dt;
            int MonCount = 0;


            for (int i = 0; i < FixCellCount; i++)
            {
                e.Row.Cells[i].CssClass = "Fixed";
                //e.Row.Cells[i].Attributes.Add("onclick", "window.open('../Crew/CrewDetails.aspx?ID=" + CrewID + "');");
            }

            e.Row.Cells[1].Width = 50;
            e.Row.Cells[2].Width = 50;
            e.Row.Cells[2].CssClass += " link";
            e.Row.Cells[2].Attributes.Add("onclick", "window.open('../Crew/CrewDetails.aspx?ID=" + CrewID + "');");
            e.Row.Cells[3].Width = 200;
            e.Row.Cells[4].Width = 50;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

            S_ON_Dt = S_ON_Dt.AddDays(-1 * S_ON_Dt.Day);
            COC_Dt = COC_Dt.AddDays(-1 * COC_Dt.Day).AddMonths(1);

            for (int i = FixCellCount; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Width = 50;

                Cell_Dt = St_Dt.AddMonths(MonCount);

                MonCount++;

                if (Cell_Dt.Month == DateTime.Today.Month && Cell_Dt.Year == DateTime.Today.Year)
                    e.Row.Cells[i].CssClass += " ThisMonth";

                System.Globalization.DateTimeFormatInfo mfi = new
                System.Globalization.DateTimeFormatInfo();
                string strMonthName = mfi.GetAbbreviatedMonthName(Cell_Dt.Month).ToString();

                strMonthName += ' ' + Cell_Dt.Year.ToString();
                if (DataBinder.Eval(e.Row.DataItem, strMonthName).ToString() != "")
                {
                    if (Cell_Dt >= S_ON_Dt && Cell_Dt <= COC_Dt)
                    {
                        e.Row.Cells[i].CssClass = " Current";
                        e.Row.Cells[i].Attributes.Add("onclick", "window.open('CrewEvaluations.aspx?CrewID=" + CrewID + "');");
                    }
                }
                if (e.Row.Cells[i].Text.Trim() == "Completed")
                {
                    //e.Row.Cells[i].Attributes.Add("onclick", "window.open('CrewEvaluations.aspx?CrewID=" + CrewID + "');");
                    e.Row.Cells[i].CssClass += " Complete";
                }
                else if (e.Row.Cells[i].Text.Trim() == "Pending")
                {
                    //e.Row.Cells[i].Attributes.Add("onclick", "window.open('CrewEvaluations.aspx?CrewID=" + CrewID + "');");
                    e.Row.Cells[i].CssClass += " Pending";
                }
                else if (e.Row.Cells[i].Text.Trim() == "Overdue")
                {
                    //e.Row.Cells[i].Attributes.Add("onclick", "window.open('CrewEvaluations.aspx?CrewID=" + CrewID + "');");
                    e.Row.Cells[i].CssClass += " overdue";
                    e.Row.Cells[i].Text = "Pending";
                }

                //e.Row.Cells[i].ToolTip = "";
            }

        }
    }

    protected void MovePrev_Click(object sender, EventArgs e)
    {
        int StartMonth = UDFLib.ConvertToInteger(hdnStartMonth.Value);
        StartMonth = StartMonth - 6;
        hdnStartMonth.Value = StartMonth.ToString();

        Bind_EvaluationSchedules();
    }
    protected void MoveCurrent_Click(object sender, EventArgs e)
    {
        hdnStartMonth.Value = "-5";
        Bind_EvaluationSchedules();
    }
    protected void MoveNext_Click(object sender, EventArgs e)
    {
        int StartMonth = UDFLib.ConvertToInteger(hdnStartMonth.Value);
        StartMonth = StartMonth + 6;
        hdnStartMonth.Value = StartMonth.ToString();

        Bind_EvaluationSchedules();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlFleet.SelectedIndex = 0;
        ddlVessel.SelectedIndex = 0;
        ddlRank.SelectedIndex = 0;

        Bind_EvaluationSchedules();
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        //Bind_EvaluationSchedules();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_EvaluationSchedules();
    }


}