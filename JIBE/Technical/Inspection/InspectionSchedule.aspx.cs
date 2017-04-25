using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Data;

using SMS.Business.Inspection;
public partial class Technical_Worklist_InspectionSchedule : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    //BLL_Tec_Worklist objWorklist = new BLL_Tec_Worklist();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();
             
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
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
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
        
        int Eva_Type = 0; // UDFLib.ConvertToInteger(ddlEvaluationType.SelectedValue);

        DataTable dt = objInsp.Get_InspectionScheduleByVessel(VesselID, StartMonth, Eva_Type, "");
        DataTable dtCopy = dt.Copy(); 
        List<string> lVessel = new List<string>();
        foreach (DataRow item in dt.Rows)
        {
            if (!lVessel.Contains(item["Vessel"].ToString()))
            {
                lVessel.Add(item["Vessel"].ToString());
            }
            else
            {
                DataRow lRemRow = dtCopy.Select("Id = " + item["ID"].ToString())[0];
                dtCopy.Rows.Remove(lRemRow);
            }
        }


        DataView dataView = new DataView(dtCopy);
        try
        {
            if (ViewState["sortExpression"] != null)
                dataView.Sort = ViewState["sortExpression"].ToString();
        }
        catch { }

        GridView_Evaluation.DataSource = dataView;
        GridView_Evaluation.DataBind();

        lblSelection.Text = DateTime.Today.AddMonths(StartMonth).ToString("MMM/yyyy") + " to " + DateTime.Today.AddMonths(StartMonth + 11).ToString("MMM/yyyy");

        //string js = "FindAndReplace('Overdue','Pending');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFindAndReplace", js, true);

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
        int FixCellCount = 8;

     

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;

            

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Vessel_ID = e.Row.Cells[7].Text.Trim().ToString();
            string CrewID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();

            DateTime S_ON_Dt = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "Schedule_Date").ToString());
          
            DateTime St_Dt = DateTime.Parse(DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "StDate").ToString()).ToString("dd/MM/yyyy"));
            DateTime End_Dt = DateTime.Parse(DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "EndDate").ToString()).ToString("dd/MM/yyyy"));
            DateTime Cell_Dt;
            int MonCount = 0;


            for (int i = 0; i < FixCellCount; i++)
            {
                e.Row.Cells[i].CssClass = "Fixed";
               
            }

          
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;

            S_ON_Dt = S_ON_Dt.AddDays(-1 * S_ON_Dt.Day);
         

            for (int i = FixCellCount; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Width = 50;

                Cell_Dt = St_Dt.AddMonths(MonCount);

                MonCount++;

           

                DataTable dt  = ((DataView)GridView_Evaluation.DataSource).ToTable();
                string[] montyear = dt.Columns[i].ToString().Split(' ');

                int mon  =  DateTime.ParseExact(montyear[0], "MMM", System.Globalization.CultureInfo.InvariantCulture).Month;
                DateTime sdate = new DateTime(Convert.ToInt32(montyear[1]),mon,1);
                DateTime edate = sdate.AddMonths(1);


                if (e.Row.Cells[i].Text.Trim().Length > 0  && e.Row.Cells[i].Text.Trim()!="&nbsp;")
                {
                    e.Row.Cells[i].Attributes.Add("onclick", "window.open('SuperintendentInspection.aspx?StartDate=" + sdate + "&EndDate=" + edate.AddDays(-1)+ "&Vessel_ID=" + Vessel_ID + "');");
                    e.Row.Cells[i].CssClass += " Current ";
                }
                 

                
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
        ddlVessel.SelectedIndex = 0; 
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