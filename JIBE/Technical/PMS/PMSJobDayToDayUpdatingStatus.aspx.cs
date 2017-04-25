using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PMS;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using System.Text;
using SMS.Business.Crew;
using System.Runtime.InteropServices;
using System.Diagnostics;


public partial class PMSJobDayToDayUpdatingStatus : System.Web.UI.Page
{

    BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            FillDDL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            //ucCustomPagerItems.Visible = false;
            //ucCustomPagerItems.PageSize = 30;
            txtFromDate.Text = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
           
            // BindDailyJobUpdatingSummary();

        }

    }


    public void FillDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();

        }
        catch (Exception ex)
        {

        }

    }


    public void BindDailyJobUpdatingSummary()
    {

        DataSet ds = new DataSet();
        string sortby = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        ds = objJobStatus.TecJobDailyUpdatingSummarySearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text));



        if (ds.Tables[0].Rows.Count > 0)
        {
            AddCoumnsInGrid(ds.Tables[0]);
            gvJobDailySummary.DataSource = ds.Tables[0];
            gvJobDailySummary.DataBind();
            gvJobDailySummary.Columns[5].Visible = false;
        }
        else
        {
            gvJobDailySummary.DataSource = ds.Tables[0];
            gvJobDailySummary.DataBind();
        }
    }

    protected void gvJobDailySummary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (gvJobDailySummary.Columns.Count > 5)
            {
                int j = 0;
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                for (int i = 0; i < gvJobDailySummary.Columns.Count - 1; i++)
                {
                    TableCell HeaderCell;
                    if (i == 0)
                    {
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Code";
                        HeaderCell.RowSpan = 2;
                        e.Row.Cells[i].Visible = false;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);

                    }
                    if (i == 1)
                    {
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Vessel";
                        HeaderCell.RowSpan = 2;
                        e.Row.Cells[i].Visible = false;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);

                    }
                    if (i == 2)
                    {
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Fleet";
                        HeaderCell.RowSpan = 2;
                        e.Row.Cells[i].Visible = false;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);

                    }
                    if (i == 3)
                    {
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Total Jobs";
                        HeaderCell.RowSpan = 2;
                        e.Row.Cells[i].Visible = false;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);

                    }
                    if (i == 4)
                    {
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Implementation of Ship-Specific PMS";
                        HeaderCell.Wrap = true;
                        HeaderCell.Width = 120;
                        HeaderCell.RowSpan = 2;
                        e.Row.Cells[i].Visible = false;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
               
                        HeaderGridRow.Cells.Add(HeaderCell);
                       
                    }

                    if (i == 5)
                    {
                        
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Jobs Updated";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderCell.ColumnSpan = gvJobDailySummary.Columns.Count - 5;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        i = gvJobDailySummary.Columns.Count;
                    }



                }

                gvJobDailySummary.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
    }

    protected void gvJobDailySummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "../../purchase/Image/arrowUp.png";

                    else
                        img.Src = "../../purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal strVale = decimal.Parse(e.Row.Cells[5].Text.Substring(0, e.Row.Cells[5].Text.Length - 1));
            e.Row.Cells[5].BackColor = System.Drawing.Color.LightPink;

            for (int i = 6; i < gvJobDailySummary.Columns.Count; i++)
            {
                if (decimal.Parse(e.Row.Cells[i].Text.Substring(0, e.Row.Cells[i].Text.Length - 1)) > strVale)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                    strVale = decimal.Parse(e.Row.Cells[i].Text.Substring(0, e.Row.Cells[i].Text.Length - 1));
                }
                //else if (decimal.Parse(e.Row.Cells[i].Text.Substring(0, e.Row.Cells[i].Text.Length - 1)) == strVale) 
                //{
                //    e.Row.Cells[i].BackColor = e.Row.Cells[i-1].BackColor;
                //    e.Row.Cells[i].ForeColor = e.Row.Cells[i - 1].ForeColor;
                //}
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    strVale = decimal.Parse(e.Row.Cells[i].Text.Substring(0, e.Row.Cells[i].Text.Length - 1));
                }

            }


        }

    }

    
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {

        BindDailyJobUpdatingSummary();

        UpdPnlGrid.Update();
    }

    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {


       

        txtFromDate.Text = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
        txtToDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");


        BindDailyJobUpdatingSummary();

        //UpdPnlGrid.Update();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {


    }

    private void AddCoumnsInGrid(DataTable datatable)
    {

        while (gvJobDailySummary.Columns.Count > 0)
        {
            gvJobDailySummary.Columns.RemoveAt(gvJobDailySummary.Columns.Count - 1);
        }

        BoundField VESSEL_SHORT_NAME = new BoundField();
        VESSEL_SHORT_NAME.DataField = "VESSEL_SHORT_NAME";
        VESSEL_SHORT_NAME.ItemStyle.CssClass = "PMSGridItemStyle-css";
        VESSEL_SHORT_NAME.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

        gvJobDailySummary.Columns.Add(VESSEL_SHORT_NAME);

        BoundField VESSEL_NAME = new BoundField();
        VESSEL_NAME.DataField = "VESSEL_NAME";
        VESSEL_NAME.ItemStyle.CssClass = "PMSGridItemStyle-css";
        gvJobDailySummary.Columns.Add(VESSEL_NAME);

        BoundField FLEETNAME = new BoundField();
        FLEETNAME.DataField = "FLEETNAME";
        FLEETNAME.ItemStyle.CssClass = "PMSGridItemStyle-css";
        gvJobDailySummary.Columns.Add(FLEETNAME);

        BoundField TOTALJOB = new BoundField();
        TOTALJOB.DataField = "TOTALJOB";
        TOTALJOB.ItemStyle.CssClass = "PMSGridItemStyle-css";
        TOTALJOB.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvJobDailySummary.Columns.Add(TOTALJOB);


        BoundField PMS_IMPLE_DATE = new BoundField();
        PMS_IMPLE_DATE.DataField = "PMS_IMPLE_DATE";
        PMS_IMPLE_DATE.ItemStyle.CssClass = "PMSGridItemStyle-css";
        PMS_IMPLE_DATE.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvJobDailySummary.Columns.Add(PMS_IMPLE_DATE);


        if (datatable.Columns.Count > 5)
        {
            for (int i = 5; i < datatable.Columns.Count; i++)
            {
                BoundField jobupdateboundfield = new BoundField();
                jobupdateboundfield.DataField = datatable.Columns[i].ColumnName;
                jobupdateboundfield.HeaderText = datatable.Columns[i].ColumnName;
                jobupdateboundfield.ItemStyle.CssClass = "PMSGridItemStyle-css";
                jobupdateboundfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gvJobDailySummary.Columns.Add(jobupdateboundfield);
            }
        }
    }

}