using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Text;
using SMS.Business.LMS;

public partial class LMS_LMS_Drill_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillDDL();
            BindIndex();
        }
    }
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {
       
        gvDrill.GridLines = GridLines.Both;
        gvDrill.HeaderRow.Height = 40;
        GridViewExportUtil.Export("VesselDrillSummary.xls", gvDrill);
        gvDrill.GridLines = GridLines.None;
    }
    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindIndex();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvERLogIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

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
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            DDLVessel.Items.Insert(0, new ListItem("--SELECT ALL--", null));
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    public void BindIndex()
    {
        int? Vessel_ID = null;
        if (DDLVessel.SelectedIndex > 0)
        {
            Vessel_ID = UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue);
        }

        
        DataTable drilltable = BLL_LMS_Training.GET_DRILL_SCHEDULE(Vessel_ID);

        DataTable dt = UDFLib.PivotTable("PROGRAM_NAME", "SCHEDULED_DATE", "PROGRAM_ID", new string[] { "VESSEL_ID" }, new string[] { "VESSEL_ID", "ID", "PROGRAM_ID" }, drilltable);


        gvDrill.DataSource = dt;
        gvDrill.DataBind();

        int[] freq = new int[50];
    
        if (dt.Rows.Count > 0)
        {
            GridViewRow RowHeader = gvDrill.HeaderRow;
            int j = 2;
            foreach (TableCell cell in RowHeader.Cells)
            {
                //string[] hrow = (cell.Controls[0] as LinkButton).Text.Split('~');
                string[] hrow = cell.Text.Split('~');

                if (hrow.Length > 1)
                {
                    cell.Text = hrow[0] + "<br>" + hrow[1];
                    freq[j] = int.Parse(hrow[1].Split(' ')[0]);
                    j++;
                }

            }

            foreach (GridViewRow gr in gvDrill.Rows)
            {
                j = 0;
                foreach (TableCell cell in gr.Cells)
                {

                    if (cell.Text == "Not Applicable")
                    {
                        cell.ForeColor = System.Drawing.Color.Blue;
                    }
                    if (j > 1 && cell.Text != "Not Applicable" && cell.Text.Trim() != "" && cell.Text.Trim() != "&nbsp;")
                    {
                        DateTime testDateTime = new DateTime();
                        if (DateTime.TryParse(cell.Text, out testDateTime))
                        {
                            
                            if (Convert.ToDateTime(testDateTime).AddDays(freq[j]).CompareTo(DateTime.Now) < 1)
                            {
                                cell.BackColor = System.Drawing.Color.Tomato;
                                cell.ForeColor = System.Drawing.Color.White;

                            }
                        }
                       
                       

                    }
                    j++;
                }
            }
        }
    }
}