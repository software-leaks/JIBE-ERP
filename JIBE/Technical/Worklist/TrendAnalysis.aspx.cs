using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.Technical;
using System.Web.Script.Serialization;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Net;
using SpreadsheetLight.Drawing;

public partial class Technical_Worklist_TrendAnalysis : System.Web.UI.Page
{
    #region Global Declaration - Variables and instances
    //Local variables
    DateTime FROMDATE = Convert.ToDateTime("01-01-1900");
    DateTime TODATE = Convert.ToDateTime(DateTime.Now.ToShortDateString());
    string DurationSelected = "All";
    int UserCompanyID = 0;
    //Local Class which is used to plot charts in case of PSC and Defeciency
    public class PSC_Inspections
    {
        public string Vessel { get; set; }
        public string NCR_Count { get; set; }
        public string Deficiency_Count { get; set; }
        public string PSC_Count { get; set; }
    }
    //Creating an Instance of PSC_Inspections class
    PSC_Inspections itemPSC_Inspections = new PSC_Inspections();
    //Creating a list object of PSC_Inspections class
    public List<PSC_Inspections> lstPSC = new List<PSC_Inspections>();


    //Local Class which is used to plot charts in case of NCR, Near Missed
    //Injuries, Incident/Accident
    public class ChartGeneral
    {
        public string Vessel { get; set; }
        public string SumCount { get; set; }
    }
    //Creating an Instance of ChartGeneral class
    ChartGeneral itemCharts = new ChartGeneral();
    //Creating a list object of ChartGeneral class
    public List<ChartGeneral> lstCharts = new List<ChartGeneral>();
    BLL_Tec_TrendAnalysis objBLL = new BLL_Tec_TrendAnalysis();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    #endregion

    #region Page Events
    //Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            btnall.BackColor = System.Drawing.Color.Yellow;

            Load_FleetList();
            BindPSCGrid();
            //TabPSCDeficiency.ActiveTabIndex = 0;
        }
    }
    protected void rblFilterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblFilterType.SelectedValue == "0")
        {
            ddlFleet.SelectedValue = "0";
            ddlFleet.Enabled = false;
        }
        else
        {
            ddlFleet.SelectedValue = "0";
            ddlFleet.Enabled = true;
        }
        //BindPSCGrid();
    }
    //protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindPSCGrid();
    //}
    protected void btnall_Click(object sender, EventArgs e)
    {
        txtfrom.Text = "";
        txtto.Text = "";
        DurationSelected = "All";
        btnall.BackColor = System.Drawing.Color.Yellow;
        btn3Months.BackColor = System.Drawing.Color.White;
        btn6Months.BackColor = System.Drawing.Color.White;
        btnYTD.BackColor = System.Drawing.Color.White;
        BindPSCGrid();
    }
    protected void btn3Months_Click(object sender, EventArgs e)
    {
        txtfrom.Text = "";
        txtto.Text = "";
        DurationSelected = "3Months";
        btnall.BackColor = System.Drawing.Color.White;
        btn3Months.BackColor = System.Drawing.Color.Yellow;
        btn6Months.BackColor = System.Drawing.Color.White;
        btnYTD.BackColor = System.Drawing.Color.White;
        BindPSCGrid();
    }
    protected void btn6Months_Click(object sender, EventArgs e)
    {
        txtfrom.Text = "";
        txtto.Text = "";
        DurationSelected = "6Months";
        btnall.BackColor = System.Drawing.Color.White;
        btn3Months.BackColor = System.Drawing.Color.White;
        btn6Months.BackColor = System.Drawing.Color.Yellow;
        btnYTD.BackColor = System.Drawing.Color.White;
        BindPSCGrid();
    }
    protected void btnYTD_Click(object sender, EventArgs e)
    {
        txtfrom.Text = "";
        txtto.Text = "";
        DurationSelected = "YTD";
        btnall.BackColor = System.Drawing.Color.White;
        btn3Months.BackColor = System.Drawing.Color.White;
        btn6Months.BackColor = System.Drawing.Color.White;
        btnYTD.BackColor = System.Drawing.Color.Yellow;
        BindPSCGrid();
    }
    protected void ImgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {

        DurationSelected = "All";
        btnall.BackColor = System.Drawing.Color.White;
        btn3Months.BackColor = System.Drawing.Color.White;
        btn6Months.BackColor = System.Drawing.Color.White;
        btnYTD.BackColor = System.Drawing.Color.White;
        BindPSCGrid();
    }
    protected void ImgBtnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        rblFilterType.SelectedIndex = 0;
        ddlFleet.SelectedIndex = 0;
        txtfrom.Text = "";
        txtto.Text = "";
        DurationSelected = "All";
        btnall.BackColor = System.Drawing.Color.Yellow;
        btn3Months.BackColor = System.Drawing.Color.White;
        btn6Months.BackColor = System.Drawing.Color.White;
        btnYTD.BackColor = System.Drawing.Color.White;
        BindPSCGrid();
    }


    #endregion

    #region Page Methods
    //Method to load Fleet Dropdown
    public void Load_FleetList()
    {

        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    //Method to populate all grids and calling appropriate methods to create charts
    public void BindPSCGrid()
    {
        if (rblFilterType.SelectedValue == "0")
            ddlFleet.Enabled = false;
        else
            ddlFleet.Enabled = true;
        UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        //int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        //Setting all Tabs Tab index to 0
        TabPSCDeficiency.ActiveTabIndex = 0;
        TabNCR.ActiveTabIndex = 0;
        TabNearMiss.ActiveTabIndex = 0;
        TabInjury.ActiveTabIndex = 0;
        TabIncidentAccident.ActiveTabIndex = 0;


        int FILTERBY = int.Parse(rblFilterType.SelectedValue);
        int FLEETCODE = int.Parse(ddlFleet.SelectedValue);


        //Setting FromDate
        //if txtFromDate Textbox is not null or empty we will consider the textbox date
        if (!string.IsNullOrEmpty(txtfrom.Text))
            FROMDATE = Convert.ToDateTime(txtfrom.Text.Trim());
        else
        {
            //Incase of Page initial load and if All button is clicked. From Date = '01-01-1900'
            if (DurationSelected == "All")
                FROMDATE = Convert.ToDateTime("01-01-1900");
            //Incase of 3Months button is clicked. From Date = CurrentDate -90
            if (DurationSelected == "3Months")
                FROMDATE = Convert.ToDateTime(DateTime.Now.AddDays(-90).ToShortDateString());
            //Incase of 6Months button is clicked. From Date = CurrentDate -180
            if (DurationSelected == "6Months")
                FROMDATE = Convert.ToDateTime(DateTime.Now.AddDays(-180).ToShortDateString());
            ////Incase of YTD button is clicked. From Date = Current Year's Start date i.e 01-01-CurrentYear
            if (DurationSelected == "YTD")
            {
                string CurrentYear = DateTime.Now.Year.ToString();
                FROMDATE = Convert.ToDateTime("01-01-" + CurrentYear);
            }
        }
        //Setting ToDate 
        //if txtTODate Textbox is not null or empty we will consider the textbox date
        if (!string.IsNullOrEmpty(txtto.Text))
            TODATE = Convert.ToDateTime(txtto.Text.Trim());
        //In all other cases ToDate will be set to CurrentDate
        else
            TODATE = DateTime.Now;

        if (string.IsNullOrEmpty(txtfrom.Text) && string.IsNullOrEmpty(txtto.Text) && DurationSelected == "All")
        {
            DurationSelected = "All";
            btnall.BackColor = System.Drawing.Color.Yellow;
            btn3Months.BackColor = System.Drawing.Color.White;
            btn6Months.BackColor = System.Drawing.Color.White;
            btnYTD.BackColor = System.Drawing.Color.White;

        }

        //Populating PSC Gridview and creating PSC charts
        DataTable dtPSC = objBLL.Get_INSP_PSC_And_Defects_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        DataView dv = dtPSC.DefaultView;
        dv.Sort = "DEFECTS desc";
        dtPSC = dv.ToTable();
        ViewState["dtPSC"] = dtPSC;

        //Incase dtPSC datatable is not null and row count is greater than 0
        if (dtPSC != null && dtPSC.Rows.Count > 0)
        {
            grdPscInspections.DataSource = dtPSC;
            grdPscInspections.DataBind();

            grdPscInspections.FooterRow.BackColor = System.Drawing.Color.Gold;
            int totalPSC = dtPSC.AsEnumerable().Sum(row => row.Field<int>("PSC"));
            int totalDefects = dtPSC.AsEnumerable().Sum(row => row.Field<int>("DEFECTS"));
            int totalNCR = dtPSC.AsEnumerable().Sum(row => row.Field<int>("NCRCOUNT"));
            grdPscInspections.FooterRow.Cells[0].Text = "Grand Total";
            grdPscInspections.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grdPscInspections.FooterRow.Cells[1].Text = totalPSC.ToString();
            grdPscInspections.FooterRow.Cells[2].Text = totalDefects.ToString();
            grdPscInspections.FooterRow.Cells[3].Text = totalNCR.ToString();
            grdPscInspections.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            grdPscInspections.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            grdPscInspections.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;

            //Methods to Plot Chart
            BindPSCInspectionsCharts(dtPSC);
        }
        //Incase dtPSC datatable is null and has no rows
        else
        {
            grdPscInspections.DataSource = dtPSC;
            grdPscInspections.DataBind();
            //Methods to Plot Chart
            BindPSCInspectionsCharts(dtPSC);
        }



        //Populating Data for Inspection Tab
        DataSet ds = objBLL.Get_INSP_Total_Inspections_Defects(FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        DataTable dtTotalInspections = ds.Tables[0];
        DataTable dtTotalDefects = ds.Tables[1];

        //Check if TOTALINSPECTIONS is not null or empty
        if (!string.IsNullOrEmpty(dtTotalInspections.Rows[0]["TOTALINSPECTIONS"].ToString()))
            lblTotalInspections.Text = dtTotalInspections.Rows[0]["TOTALINSPECTIONS"].ToString();
        //Incase TOTALINSPECTIONS is null or empty, setting lblTotalInspections to 0
        else
            lblTotalInspections.Text = "0";
        //Check if TOTALDEFECTS is not null or empty
        if (!string.IsNullOrEmpty(dtTotalDefects.Rows[0]["TOTALDEFECTS"].ToString()))
            lblTotalDefects.Text = dtTotalDefects.Rows[0]["TOTALDEFECTS"].ToString();
        //Incase TOTALDEFECTS is null or empty, setting lblTotalDefects to 0
        else
            lblTotalDefects.Text = "0";

        //Calculation for Average Deficiency Per Inspection
        lblAvgDefPerInsp.Text = (Math.Round((double.Parse(lblTotalInspections.Text) / double.Parse(lblTotalDefects.Text)), 2)).ToString();

        if (lblAvgDefPerInsp.Text.Trim() == "NaN")
            lblAvgDefPerInsp.Text = "0";


        DataTable dtAverageDefects = new DataTable();

        dtAverageDefects.Columns.Add("Col1", typeof(String));
        dtAverageDefects.Columns.Add("Col2", typeof(String));
        dtAverageDefects.Rows.Add("", "");
        dtAverageDefects.Rows.Add("Total Inspections", lblTotalInspections.Text);
        dtAverageDefects.Rows.Add("Total Deficiency", lblTotalDefects.Text);
        dtAverageDefects.Rows.Add("Avg Deficiency Per Inspection", lblAvgDefPerInsp.Text);
        ViewState["dtAverageDefects"] = dtAverageDefects;


        //Populating NCR Gridview and creating NCR charts
        DataTable dtNCR = objBLL.Get_INSP_NCR_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        DataView dvNCR = dtNCR.DefaultView;
        dvNCR.Sort = "NCR desc";
        dtNCR = dvNCR.ToTable();
        ViewState["dtNCR"] = dtNCR;

        //Incase dtNCR datatable is not null and row count is greater than 0
        if (dtNCR != null && dtNCR.Rows.Count > 0)
        {
            grdTotalNcrRaised.DataSource = dtNCR;
            grdTotalNcrRaised.DataBind();

            grdTotalNcrRaised.FooterRow.BackColor = System.Drawing.Color.Gold;
            int totalNCR = dtNCR.AsEnumerable().Sum(row => row.Field<int>("NCR"));

            grdTotalNcrRaised.FooterRow.Cells[0].Text = "Grand Total";
            grdTotalNcrRaised.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grdTotalNcrRaised.FooterRow.Cells[1].Text = totalNCR.ToString();
            grdTotalNcrRaised.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //Methods to Plot Chart
            BindNCRCharts(dtNCR);
        }
        //Incase dtNCR datatable is null and has no rows
        else
        {
            grdTotalNcrRaised.DataSource = dtNCR;
            grdTotalNcrRaised.DataBind();
            //Methods to Plot Chart
            BindNCRCharts(dtNCR);
        }


        //Populating NearMiss Gridview and NearMiss PSC charts
        DataTable dtNearMiss = objBLL.Get_INSP_NearMiss_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        DataView dvNearMiss = dtNearMiss.DefaultView;
        dvNearMiss.Sort = "NEARMISS desc";
        dtNearMiss = dvNearMiss.ToTable();
        ViewState["dtNearMiss"] = dtNearMiss;
        //Incase dtNearMiss datatable is not null and row count is greater than 0
        if (dtNearMiss != null && dtNearMiss.Rows.Count > 0)
        {
            grdTotalNearMiss.DataSource = dtNearMiss;
            grdTotalNearMiss.DataBind();

            grdTotalNearMiss.FooterRow.BackColor = System.Drawing.Color.Gold;
            int totalNearMiss = dtNearMiss.AsEnumerable().Sum(row => row.Field<int>("NEARMISS"));

            grdTotalNearMiss.FooterRow.Cells[0].Text = "Grand Total";
            grdTotalNearMiss.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grdTotalNearMiss.FooterRow.Cells[1].Text = totalNearMiss.ToString();
            grdTotalNearMiss.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //Methods to Plot Chart
            BindNearMissCharts(dtNearMiss);
        }
        //Incase dtNearMiss datatable is null and has no rows
        else
        {
            grdTotalNearMiss.DataSource = dtNearMiss;
            grdTotalNearMiss.DataBind();
            //Methods to Plot Chart
            BindNearMissCharts(dtNearMiss);
        }

        //Populating Injury Gridview and creating PSC charts
        DataTable dtDeathInjuries = objBLL.Get_INSP_Injuries_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        DataView dvINJURIES = dtDeathInjuries.DefaultView;
        dvINJURIES.Sort = "INJURIES desc";
        dtDeathInjuries = dvINJURIES.ToTable();
        ViewState["dtInjury"] = dtDeathInjuries;
        //Incase dtDeathInjuries datatable is not null and row count is greater than 0
        if (dtDeathInjuries != null && dtDeathInjuries.Rows.Count > 0)
        {
            grdTotalInjuries.DataSource = dtDeathInjuries;
            grdTotalInjuries.DataBind();

            grdTotalInjuries.FooterRow.BackColor = System.Drawing.Color.Gold;
            int totalDeathInjuries = dtDeathInjuries.AsEnumerable().Sum(row => row.Field<int>("INJURIES"));

            grdTotalInjuries.FooterRow.Cells[0].Text = "Grand Total";
            grdTotalInjuries.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grdTotalInjuries.FooterRow.Cells[1].Text = totalDeathInjuries.ToString();
            grdTotalInjuries.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //Methods to Plot Chart
            BindInjuryCharts(dtDeathInjuries);
        }
        //Incase dtDeathInjuries datatable is null and has no rows
        else
        {
            grdTotalInjuries.DataSource = dtDeathInjuries;
            grdTotalInjuries.DataBind();
            //Methods to Plot Chart
            BindInjuryCharts(dtDeathInjuries);
        }


        //Populating Incident/Accident Gridview and creating Incident/Accident charts
        DataTable dtPropertyPollution = objBLL.Get_INSP_PropertyPollution_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        DataView dvPROPERTYPOLLUTION = dtPropertyPollution.DefaultView;
        dvPROPERTYPOLLUTION.Sort = "PROPERTYPOLLUTION desc";
        dtPropertyPollution = dvPROPERTYPOLLUTION.ToTable();
        ViewState["dtIncidentAccident"] = dtPropertyPollution;
        //Incase dtPropertyPollution datatable is not null and row count is greater than 0
        if (dtPropertyPollution != null && dtPropertyPollution.Rows.Count > 0)
        {
            grdTotalPropertyPollution.DataSource = dtPropertyPollution;
            grdTotalPropertyPollution.DataBind();

            grdTotalPropertyPollution.FooterRow.BackColor = System.Drawing.Color.Gold;
            int totalPropertyPollution = dtPropertyPollution.AsEnumerable().Sum(row => row.Field<int>("PROPERTYPOLLUTION"));

            grdTotalPropertyPollution.FooterRow.Cells[0].Text = "Grand Total";
            grdTotalPropertyPollution.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grdTotalPropertyPollution.FooterRow.Cells[1].Text = totalPropertyPollution.ToString();
            grdTotalPropertyPollution.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //Methods to Plot Chart
            BindPropertyPollutionCharts(dtPropertyPollution);
        }
        //Incase dtPropertyPollution datatable is null and has no rows
        else
        {
            grdTotalPropertyPollution.DataSource = dtPropertyPollution;
            grdTotalPropertyPollution.DataBind();
            //Methods to Plot Chart
            BindPropertyPollutionCharts(dtPropertyPollution);
        }

    }

    //Method to plot PSCInspection chart
    public void BindPSCInspectionsCharts(DataTable dtPSCInspections)
    {
        try
        {
            lstPSC = new List<PSC_Inspections>();
            JavaScriptSerializer j = new JavaScriptSerializer();


            if (dtPSCInspections.Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dtPSCInspections.Rows.Count; i++)
                {
                    if (int.Parse(dtPSCInspections.Rows[i][2].ToString()) >= 0 || int.Parse(dtPSCInspections.Rows[i][3].ToString()) >= 0)
                    {
                        itemPSC_Inspections = new PSC_Inspections();
                        itemPSC_Inspections.Vessel = dtPSCInspections.Rows[i][1].ToString();
                        itemPSC_Inspections.NCR_Count = dtPSCInspections.Rows[i][4].ToString();
                        itemPSC_Inspections.PSC_Count = dtPSCInspections.Rows[i][2].ToString();
                        itemPSC_Inspections.Deficiency_Count = dtPSCInspections.Rows[i][3].ToString();
                        lstPSC.Add(itemPSC_Inspections);
                    }
                }

                if (lstPSC.Count > 0)
                {
                    string js4 = "drawChartPscDeficiency(" + j.Serialize(lstPSC) + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PscDeficiencyChart", js4, true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Method to plot NCR chart
    public void BindNCRCharts(DataTable dtNCR)
    {
        try
        {
            lstCharts = new List<ChartGeneral>();
            JavaScriptSerializer j = new JavaScriptSerializer();

            if (dtNCR.Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dtNCR.Rows.Count; i++)
                {
                    if (int.Parse(dtNCR.Rows[i][1].ToString()) >= 0)
                    {
                        itemCharts = new ChartGeneral();
                        itemCharts.Vessel = dtNCR.Rows[i][0].ToString();
                        itemCharts.SumCount = dtNCR.Rows[i][1].ToString();

                        lstCharts.Add(itemCharts);
                    }
                }
                if (lstCharts.Count > 0)
                {
                    string js4 = "drawChartNCR(" + j.Serialize(lstCharts) + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NcrChart", js4, true);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Method to plot NearMiss chart
    public void BindNearMissCharts(DataTable dtNearMiss)
    {
        try
        {
            lstCharts = new List<ChartGeneral>();
            JavaScriptSerializer j = new JavaScriptSerializer();

            if (dtNearMiss.Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dtNearMiss.Rows.Count; i++)
                {
                    if (int.Parse(dtNearMiss.Rows[i][1].ToString()) >= 0)
                    {
                        itemCharts = new ChartGeneral();
                        itemCharts.Vessel = dtNearMiss.Rows[i][0].ToString();
                        itemCharts.SumCount = dtNearMiss.Rows[i][1].ToString();

                        lstCharts.Add(itemCharts);
                    }
                }

                if (lstCharts.Count > 0)
                {
                    string js4 = "drawChartNearMiss(" + j.Serialize(lstCharts) + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NearMissChart", js4, true); 
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Method to plot Injury chart
    public void BindInjuryCharts(DataTable dtInjury)
    {
        try
        {
            lstCharts = new List<ChartGeneral>();
            JavaScriptSerializer j = new JavaScriptSerializer();

            if (dtInjury.Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dtInjury.Rows.Count; i++)
                {
                    if (int.Parse(dtInjury.Rows[i][1].ToString()) >= 0)
                    {
                        itemCharts = new ChartGeneral();
                        itemCharts.Vessel = dtInjury.Rows[i][0].ToString();
                        itemCharts.SumCount = dtInjury.Rows[i][1].ToString();

                        lstCharts.Add(itemCharts);
                    }
                }
                if (lstCharts.Count > 0)
                {
                    string js4 = "drawChartInjury(" + j.Serialize(lstCharts) + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "InjuryChart", js4, true);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Method to plot Incident/Accident chart
    public void BindPropertyPollutionCharts(DataTable dtPropertyPollution)
    {
        try
        {
            lstCharts = new List<ChartGeneral>();
            JavaScriptSerializer j = new JavaScriptSerializer();

            if (dtPropertyPollution.Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dtPropertyPollution.Rows.Count; i++)
                {
                    if (int.Parse(dtPropertyPollution.Rows[i][1].ToString()) >= 0)
                    {
                        itemCharts = new ChartGeneral();
                        itemCharts.Vessel = dtPropertyPollution.Rows[i][0].ToString();
                        itemCharts.SumCount = dtPropertyPollution.Rows[i][1].ToString();

                        lstCharts.Add(itemCharts);
                    }
                }
                if (lstCharts.Count > 0)
                {
                    string js4 = "drawChartPropertyPollution(" + j.Serialize(lstCharts) + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PropertyPollutionChart", js4, true);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    protected void ibtnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {

        Array.ForEach(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Uploads/Temp"), File.Delete);
        
        //Converting PSC Gridview Html Data to string
        var DataPSc = new StringBuilder();
        dvPSCinspections.RenderControl(new HtmlTextWriter(new StringWriter(DataPSc)));
        string sDataPSc = DataPSc.ToString();


        //Converting Average Deficiency Html Data  to string
        var DataAverageDeficiency = new StringBuilder();
        dvGridAvgDeficiency.RenderControl(new HtmlTextWriter(new StringWriter(DataAverageDeficiency)));
        string sDataAverageDeficiency = DataAverageDeficiency.ToString();

        //Creating SpreadSheetLight Object
        //Used to create a new excel document
        SLDocument sl = new SLDocument();

        //PSC Defects NCR

        string PSC = hdfPSC.Value;
        byte[] bPSC = ConvertChartPathToImage(PSC);
        SLPicture pic = new SLPicture(bPSC, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);

        sl.MergeWorksheetCells("A1", "Q1");
        SLStyle style = sl.CreateStyle();
        sl.SetColumnWidth(12, 40);
        sl.SetColumnWidth(13, 10);
        sl.SetColumnWidth(14, 30);
        sl.SetColumnWidth(15, 15);
        sl.SetColumnWidth(16, 16);
        sl.SetColumnWidth(17, 10);
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        sl.SetCellStyle(1, 1, style);
        pic.SetPosition(2, 2);
        sl.InsertPicture(pic);
        sl.SetCellValue("A1", "PSC Inspections Per Ship");

        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(3, 14, style);
        sl.SetCellStyle(3, 15, style);
        sl.SetCellStyle(3, 16, style);
        sl.SetCellStyle(3, 17, style);

        DataTable dtPSC = (DataTable)ViewState["dtPSC"];
        dtPSC.Columns.Remove("VESSELID");
        dtPSC.Columns[0].ColumnName = "VESSEL/FLEET";
        dtPSC.Columns[2].ColumnName = "DEFICIENCIES";
        dtPSC.Columns[3].ColumnName = "NCR";
        // Declare an object variable.
        object sumObjectPSC;
        sumObjectPSC = dtPSC.Compute("Sum(PSC)", "");

        object sumObjectDefects;
        sumObjectDefects = dtPSC.Compute("Sum(DEFICIENCIES)", "");

        object sumObjectNCR;
        sumObjectNCR = dtPSC.Compute("Sum(NCR)", "");

        dtPSC.Rows.Add("Grand Total", sumObjectPSC.ToString(), sumObjectDefects.ToString(), sumObjectNCR.ToString());


        int iStartRowIndex = 3;
        int iStartColumnIndex = 14;
        sl.ImportDataTable(iStartRowIndex, iStartColumnIndex, dtPSC, true);
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(3 + dtPSC.Rows.Count, 14, style);
        sl.SetCellStyle(3 + dtPSC.Rows.Count, 15, style);
        sl.SetCellStyle(3 + dtPSC.Rows.Count, 16, style);
        sl.SetCellStyle(3 + dtPSC.Rows.Count, 17, style);

        int PSCCellCount = 4;
        for (int i = 0; i < dtPSC.Rows.Count; i++)
        {
            style = sl.CreateStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellStyle(PSCCellCount, 15, style);
            sl.SetCellStyle(PSCCellCount, 16, style);
            sl.SetCellStyle(PSCCellCount, 17, style);
            sl.SetCellStyle(PSCCellCount, 18, style);
            PSCCellCount++;
        }
        //PSC Defects NCR



        //Average Defects
        int CellCount = 3;
        if (dtPSC.Rows.Count > 20)
            CellCount += dtPSC.Rows.Count + 2;
        else
            CellCount += 22;

        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        sl.SetCellStyle(CellCount, 1, style);
        sl.MergeWorksheetCells("A" + CellCount.ToString(), "Q" + CellCount.ToString());
        sl.SetCellValue("A" + CellCount.ToString(), "Average Deficiency Per Ship");
        DataTable dtAverageDefects = (DataTable)ViewState["dtAverageDefects"];

        iStartRowIndex = CellCount;
        iStartColumnIndex = 12;


        sl.ImportDataTable(iStartRowIndex, iStartColumnIndex, dtAverageDefects, true);
        CellCount = CellCount + 2;
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount, 12, style);
        sl.SetCellStyle(CellCount, 13, style);
        CellCount = CellCount + 1;
        sl.SetCellStyle(CellCount, 12, style);
        sl.SetCellStyle(CellCount, 13, style);
        CellCount = CellCount + 1;
        sl.SetCellStyle(CellCount, 12, style);
        sl.SetCellStyle(CellCount, 13, style);

        PSCCellCount = CellCount - 3;
        for (int i = 0; i < dtAverageDefects.Rows.Count; i++)
        {
            style = sl.CreateStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellStyle(PSCCellCount, 13, style);
            PSCCellCount++;
        }
        //Average Defects


        //NCR Count

        string NCR = hdfNCR.Value;
        byte[] bNCR = ConvertChartPathToImage(NCR);
        pic = new SLPicture(bNCR, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);

        CellCount = CellCount + 3;
        sl.MergeWorksheetCells("A" + CellCount.ToString(), "Q" + CellCount.ToString());
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        sl.SetCellStyle(CellCount, 1, style);
        sl.SetCellValue("A" + CellCount.ToString(), "Total NCRs Raised In The Fleet, Per Vessel");
        CellCount = CellCount + 2;
        pic.SetPosition(CellCount, 2);
        sl.InsertPicture(pic);




        DataTable dtNCR = (DataTable)ViewState["dtNCR"];

        dtNCR.Columns[0].ColumnName = "VESSEL/FLEET";
        dtNCR.Columns[1].ColumnName = "NCR";

        // Declare an object variable.

        object sumObjectNCR2;
        sumObjectNCR2 = dtNCR.Compute("Sum(NCR)", "");

        dtNCR.Rows.Add("Grand Total", sumObjectNCR2.ToString());
        CellCount = CellCount + 1;
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount, 14, style);
        sl.SetCellStyle(CellCount, 15, style);
        iStartRowIndex = CellCount;
        iStartColumnIndex = 14;
        sl.ImportDataTable(iStartRowIndex, iStartColumnIndex, dtNCR, true);
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 14, style);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 15, style);
        PSCCellCount = CellCount + 1;
        for (int i = 0; i < dtNCR.Rows.Count; i++)
        {
            style = sl.CreateStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellStyle(PSCCellCount, 15, style);
            PSCCellCount++;
        }
        //NCR Count

        //Near Miss
        if (dtNCR.Rows.Count > (54 - CellCount))
            CellCount += dtNCR.Rows.Count + 2;
        else
            CellCount += 22;


        string NearMiss = hdfNearMiss.Value;
        byte[] bNearMiss = ConvertChartPathToImage(NearMiss);
        pic = new SLPicture(bNearMiss, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);

        CellCount = CellCount + 3;
        sl.MergeWorksheetCells("A" + CellCount.ToString(), "Q" + CellCount.ToString());
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        sl.SetCellStyle(CellCount, 1, style);
        sl.SetCellValue("A" + CellCount.ToString(), "Total Near Miss Raised In The Fleet, Per Vessel");
        CellCount = CellCount + 2;
        pic.SetPosition(CellCount, 0);
        sl.InsertPicture(pic);




        DataTable dtNearMiss = (DataTable)ViewState["dtNearMiss"];
        object sumObjectNearMiss;
        sumObjectNearMiss = dtNearMiss.Compute("Sum(NEARMISS)", "");
        dtNearMiss.Columns[0].ColumnName = "VESSEL/FLEET";
        dtNearMiss.Columns[1].ColumnName = "Near-Miss";

        // Declare an object variable.



        dtNearMiss.Rows.Add("Grand Total", sumObjectNearMiss.ToString());
        CellCount = CellCount + 1;
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount, 14, style);
        sl.SetCellStyle(CellCount, 15, style);
        iStartRowIndex = CellCount;
        iStartColumnIndex = 14;
        sl.ImportDataTable(iStartRowIndex, iStartColumnIndex, dtNearMiss, true);
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 14, style);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 15, style);
        PSCCellCount = CellCount + 1;
        for (int i = 0; i < dtNearMiss.Rows.Count; i++)
        {
            style = sl.CreateStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellStyle(PSCCellCount, 15, style);
            PSCCellCount++;
        }
        //Near Miss

        //Injury


        if (dtNearMiss.Rows.Count > (77 - CellCount))
            CellCount += dtNearMiss.Rows.Count + 2;
        else
            CellCount += 15;


        string Injury = hdfInjury.Value;
        byte[] bInjury = ConvertChartPathToImage(Injury);
        pic = new SLPicture(bInjury, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);

        CellCount = CellCount + 3;
        sl.MergeWorksheetCells("A" + CellCount.ToString(), "Q" + CellCount.ToString());
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        sl.SetCellStyle(CellCount, 1, style);
        sl.SetCellValue("A" + CellCount.ToString(), "Total Injuries In The Fleet");
        CellCount = CellCount + 2;
        pic.SetPosition(CellCount, 0);
        sl.InsertPicture(pic);




        DataTable dtInjury = (DataTable)ViewState["dtInjury"];
        object sumObjectInjury;
        sumObjectInjury = dtInjury.Compute("Sum(INJURIES)", "");
        dtInjury.Columns[0].ColumnName = "VESSEL/FLEET";
        dtInjury.Columns[1].ColumnName = "Sum Of Injury";

        // Declare an object variable.



        dtInjury.Rows.Add("Grand Total", sumObjectInjury.ToString());
        CellCount = CellCount + 1;
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount, 14, style);
        sl.SetCellStyle(CellCount, 15, style);
        iStartRowIndex = CellCount;
        iStartColumnIndex = 14;
        sl.ImportDataTable(iStartRowIndex, iStartColumnIndex, dtInjury, true);
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 14, style);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 15, style);
        PSCCellCount = CellCount + 1;
        for (int i = 0; i < dtInjury.Rows.Count; i++)
        {
            style = sl.CreateStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellStyle(PSCCellCount, 15, style);
            PSCCellCount++;
        }
        //Injury


        //Incident Accident
        if (dtInjury.Rows.Count > (102 - CellCount))
            CellCount += dtInjury.Rows.Count + 2;
        else
            CellCount += 15;


        string IncidentAccident = hdfIncidentAccident.Value;
        byte[] bIncidentAccident = ConvertChartPathToImage(IncidentAccident);
        pic = new SLPicture(bIncidentAccident, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);

        CellCount = CellCount + 3;
        sl.MergeWorksheetCells("A" + CellCount.ToString(), "Q" + CellCount.ToString());
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
        sl.SetCellStyle(CellCount, 1, style);
        sl.SetCellValue("A" + CellCount.ToString(), "Total Incidents/Accidents In The Fleet");
        CellCount = CellCount + 2;
        pic.SetPosition(CellCount, 0);
        sl.InsertPicture(pic);




        DataTable dtIncidentAccident = (DataTable)ViewState["dtIncidentAccident"];
        object sumObjectIncidentAccident;
        sumObjectIncidentAccident = dtIncidentAccident.Compute("Sum(PROPERTYPOLLUTION)", "");
        dtIncidentAccident.Columns[0].ColumnName = "VESSEL/FLEET";
        dtIncidentAccident.Columns[1].ColumnName = "Sum Of Count";

        // Declare an object variable.



        dtIncidentAccident.Rows.Add("Grand Total", sumObjectIncidentAccident.ToString());
        CellCount = CellCount + 1;
        style = sl.CreateStyle();

        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount, 14, style);
        sl.SetCellStyle(CellCount, 15, style);
        iStartRowIndex = CellCount;
        iStartColumnIndex = 14;
        sl.ImportDataTable(iStartRowIndex, iStartColumnIndex, dtIncidentAccident, true);
        style = sl.CreateStyle();
        style.SetPatternFill(PatternValues.Solid, System.Drawing.Color.LightGray, System.Drawing.Color.LightGray);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 14, style);
        sl.SetCellStyle(CellCount + dtPSC.Rows.Count, 15, style);
        PSCCellCount = CellCount + 1;
        for (int i = 0; i < dtIncidentAccident.Rows.Count; i++)
        {
            style = sl.CreateStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellStyle(PSCCellCount, 15, style);
            PSCCellCount++;
        }
        //Incident Accident


        string fileNamewithpath = "";
        string SaveExcelFileName = "";
        string folder = AppDomain.CurrentDomain.BaseDirectory + "Uploads/Temp";
        string[] BaseDirectory = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
        string domainname = BaseDirectory[BaseDirectory.Length - 2];
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        SaveExcelFileName = "TrendAnalysis" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Second + DateTime.Now.Millisecond + ".xlsx";
        fileNamewithpath = folder + "/" + SaveExcelFileName;
        sl.SaveAs(fileNamewithpath);

        BindPSCGrid();

        Response.ContentType = "Application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + SaveExcelFileName);
        Response.TransmitFile(Server.MapPath("../../Uploads/Temp/" + SaveExcelFileName));
        Response.End();

        

    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
    }

    //Common Method to Convert Chart to Images
    private byte[] ConvertChartPathToImage(string DataPath)
    {
        DataPath = DataPath.Replace("<img src=\"", "");
        DataPath = DataPath.Replace("\">", "");
        string data = DataPath;

        var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        var binData = Convert.FromBase64String(base64Data);
        var stream = new MemoryStream(binData);
        System.Drawing.Image image = new Bitmap(stream);
        //image.Save(Server.MapPath("../../Uploads/Temp/" + ImageName));
        return stream.ToArray();

    }
   
    
}