using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Data;
using SMS.Business.LMS;
using SMS.Business.Infrastructure;
public partial class LMS_LMS_Drill_Calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (IsPostBack)
        {

            if (DDLVessel.SelectedIndex > 0)
            {
                string js = "LastLoad(" + DDLVessel.SelectedValue + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LastLoad", js, true);
            }
        }
        else
        {
            FillDDL();
        }

    }
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    public void FillDDL()
    {
        try
        {
            DataTable dtVessel = objBLLVessel.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            DDLVessel.Items.Insert(0, new ListItem("--SELECT--", null));
            DDLVessel.SelectedIndex = 0;
        }
        catch (Exception)
        {

            Response.Redirect("~/Account/Login.aspx");
        }


    }
    protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (DDLVessel.SelectedIndex <= 0)
        {
            string js1 = "alert('Vessel not selected!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js1, true);
            return;
        }

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DrillCalendar_" + DDLVessel.SelectedItem.Text + ".xls");
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        //  var h = Convert.ToDateTime(hf0.Text);  
        DateTime dt = DateTime.ParseExact(hf0.Text.Substring(0, 24), "ddd MMM d yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        string[] outputhtml = LoadDrillCalendar(dt);
        lblSelection.Text = outputhtml[1];
        newt0.InnerHtml = outputhtml[0];
        newt.InnerHtml = outputhtml[2];
        StringWriter sw = new StringWriter();
        HtmlTextWriter w = new HtmlTextWriter(sw);
        divexport.RenderControl(w);
        string excelstring = sw.GetStringBuilder().ToString();
        HttpContext.Current.Response.Write(excelstring);
        HttpContext.Current.Response.End();

    }
    public string[] LoadDrillCalendar(DateTime pStartDate)
    {
        //int StartMont = Convert.ToInt32(pStartMont);
        //int Year = Convert.ToInt32(pYear);
        DateTime pxStartDate = Convert.ToDateTime(pStartDate);
        List<string> MonthName = new List<string>();
        List<int> MonthDays = new List<int>();
        DateTime lStartDate = new DateTime(pxStartDate.Year, pxStartDate.Month, 1);
        DataSet ds = BLL_LMS_Training.GET_YEARLY_DRILL_REPORT(lStartDate.Month, lStartDate.Year, UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue));
        Table DrillTable = new Table();
        DrillTable.CellPadding = 0;
        DrillTable.CellSpacing = 0;
        Table DrillTable0 = new Table();
        DrillTable0.CellPadding = 0;
        DrillTable0.CellSpacing = 0;
        TableRow MonthRow;
        TableRow MonthProgramRow;
        TableCell monthCell;
        TableCell programHeadCell = new TableCell();
        MonthRow = new TableRow();
        programHeadCell.Text = "Drill Name";
        programHeadCell.CssClass = "ProgramHeadStyle";
        MonthRow.Cells.Add(programHeadCell);
        string infoText = lStartDate.ToString("MMMM", new CultureInfo("en-GB")) + " " + lStartDate.Year + " to " + lStartDate.AddMonths(11).ToString("MMMM", new CultureInfo("en-GB")) + " " + lStartDate.AddMonths(11).Year + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Vessel : " + DDLVessel.SelectedItem.Text;
        for (int i = 0; i < 12; i++)
        {
            //MonthName.Add(lStartDate.AddMonths(i).ToString("MMMM", new CultureInfo("en-GB")));
            monthCell = new TableCell();
            monthCell.Text = lStartDate.AddMonths(i).ToString("MMM/yy", new CultureInfo("en-GB"));
            //monthCell.ColumnSpan = DateTime.DaysInMonth(l.Year, l.Month);
            monthCell.CssClass = "MonthStyle";
            MonthRow.Cells.Add(monthCell);
        }
        DrillTable0.Rows.Add(MonthRow);

        List<int> usedPrograms = new List<int>();
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            string lProgramName = item["ProgramName"].ToString();
            int lProgram_ID = Convert.ToInt32(item["Program_ID"]);
            if (!usedPrograms.Contains(lProgram_ID))
            {
                usedPrograms.Add(lProgram_ID);
            }
            else
            {
                continue;
            }
            //DataRow[] drs = ds.Tables[0].Select("Program_ID=" + lProgram_ID);
            TableCell ProgramCell = new TableCell();
            ProgramCell.Text = lProgramName;
            ProgramCell.CssClass = "ProgramCellStyle";
            MonthProgramRow = new TableRow();
            MonthProgramRow.Cells.Add(ProgramCell);
            for (int i = 0; i < 12; i++)
            {
                DataRow[] drs = ds.Tables[0].Select("Program_ID=" + lProgram_ID + " and MONTHINDEX=" + lStartDate.AddMonths(i).Month);
                TableCell MonthProgramCell = new TableCell();
                if (drs.Length > 0)
                {
                    bool fStatus = true;
                    foreach (DataRow StatusRow in drs)
                    {
                        if (StatusRow["Status"].ToString() == "0")
                        {
                            fStatus = false;
                            break;
                        }
                    }
                    if (fStatus)
                    {
                        MonthProgramCell.CssClass = "MonthProgramCellSyle1";
                        MonthProgramCell.ToolTip = "Completed Drill (All drills are Completed)";
                    }

                    else
                    {
                        if (Convert.ToDateTime(drs[0]["Sch_Date"]).Date <= DateTime.Now.Date)
                        {
                            MonthProgramCell.CssClass = "MonthProgramCellSyle2";
                            MonthProgramCell.ToolTip = "Overdue Drill";
                        }
                        else
                        {
                            MonthProgramCell.CssClass = "MonthProgramCellSyle3";
                            MonthProgramCell.ToolTip = "Planned Drill";
                        }
                    }
                    MonthProgramCell.Text = "**";
                    System.Text.StringBuilder info = new System.Text.StringBuilder();
                    info.Append("<div style='margin:5px;border-radius:5px'>");
                    info.Append("<table cellpadding='3px' >");
                }
                else
                {
                    MonthProgramCell.CssClass = "MonthProgramCellSyle0";
                }
                MonthProgramRow.Cells.Add(MonthProgramCell);

            }
            DrillTable.Rows.Add(MonthProgramRow);
        }

        DrillTable.ID = "t01";
        DrillTable0.ID = "t00";
        string newt, newt0 = "";
        using (StringWriter sw = new StringWriter())
        {

            DrillTable.RenderControl(new HtmlTextWriter(sw));
            newt = sw.ToString();
        }
        using (StringWriter sw = new StringWriter())
        {

            DrillTable0.RenderControl(new HtmlTextWriter(sw));
            newt0 = sw.ToString();
        }
        String[] result = { newt, infoText, newt0 };
        return result;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLVessel.SelectedIndex > 0)
        {
            string js = "LastLoad(" + DDLVessel.SelectedValue + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LastLoad", js, true);
        }

    }
    protected void Cur_Click(object sender, EventArgs e)
    {
        if (DDLVessel.SelectedIndex > 0)
        {
            string js = "Current(" + DDLVessel.SelectedValue + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Current", js, true);
        }
        else
        {
            string js1 = "alert('Vessel not selected!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js1, true);
        }

    }
    protected void MovePrev_Click(object sender, EventArgs e)
    {
        if (DDLVessel.SelectedIndex > 0)
        {
            string js = "Prev(" + DDLVessel.SelectedValue + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Prev", js, true);
        }
        else
        {
            string js1 = "alert('Vessel not selected!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js1, true);
        }
    }
    protected void MoveNext_Click(object sender, EventArgs e)
    {
        if (DDLVessel.SelectedIndex > 0)
        {
            string js = "Next(" + DDLVessel.SelectedValue + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Next", js, true);
        }
        else
        {
            string js1 = "alert('Vessel not selected!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js1, true);
        }
    }
}