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
using SMS.Business.QMS;


public partial class SCM_Vessels_Drill_Reports : System.Web.UI.Page
{
    BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERFLEETID"] == null)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            FillDDL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            BindDrillTypeDDL();

            //txtFromDate.Text = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
            //txtToDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            BindVesselsDrillReport();
           

        }
      

    }

    

    public void FillDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DDLFleet.Items.Clear();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);

        }
        catch (Exception ex)
        {

        }

    }

    public void BindVesselDDL()
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

    public void BindDrillTypeDDL()
    {
        try
        {

            DataTable dt = BLL_SCM_Report.SCMGetDrillTYPE();
            DDLDrillType.Items.Clear();
            DDLDrillType.DataSource = dt;
            DDLDrillType.DataTextField = "DRILL_NAME";
            DDLDrillType.DataValueField = "ID";
            DDLDrillType.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLDrillType.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }



    public void BindVesselsDrillReport()
    {

        DataTable dt = new DataTable();
        string sortby = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        dt = BLL_SCM_Report.SCMGetVesselDrillReports(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), null, null, UDFLib.ConvertIntegerToNull(DDLDrillType.SelectedValue));

        if (dt.Rows.Count > 0)
        {

            DataTable dtpb = UDFLib.PivotTable("DRILL_NAME", "DRILL_DATE", "Sort_order", new string[] { "VESSEL_ID" }, new string[] { "VESSEL_ID", "ID", "Sort_order" }, dt);
            dtpb.DefaultView.Sort = "Vessel";


            gvVesselsDrill.DataSource = dtpb;
            gvVesselsDrill.DataBind();

            int[] freq = new int[10];


            GridViewRow RowHeader = gvVesselsDrill.HeaderRow;
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

            foreach (GridViewRow gr in gvVesselsDrill.Rows)
            {
                j = 0;
                foreach (TableCell cell in gr.Cells)
                {

                    if (cell.Text == "Not Applicable")
                    {
                        cell.ForeColor = System.Drawing.Color.Blue;
                    }
                    if (j > 1 && cell.Text != "Not Applicable" && cell.Text.Trim() != "")
                    {
                        if (Convert.ToDateTime(cell.Text).AddDays(freq[j]).CompareTo(DateTime.Now) < 1)
                        {
                            cell.BackColor = System.Drawing.Color.Tomato;
                            cell.ForeColor = System.Drawing.Color.White;
                            
                        }

                    }
                    j++;
                }
            }
        }
        else
        {
            gvVesselsDrill.DataSource = dt;
            gvVesselsDrill.DataBind();
        }
    }


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

        DDLVessel.Items.Clear();
        DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_ID";
        DDLVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLVessel.Items.Insert(0, li);


    }

    protected void gvVesselsDrill_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvVesselsDrill_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindVesselsDrillReport();

    }

    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {

        BindVesselsDrillReport();

         UpdPnlGrid.Update();
    }

    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        //txtFromDate.Text = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
        //txtToDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");


        FillDDL();
        BindVesselDDL();
        DDLDrillType.SelectedValue = "0";

        BindVesselsDrillReport();

         UpdPnlGrid.Update();
    }

    /// <summary>
    /// Export data to excel
    /// </summary>
    
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        BindVesselsDrillReport();
        if (gvVesselsDrill.Rows.Count > 0)                                                             
        {
            gvVesselsDrill.GridLines = GridLines.Both;
            gvVesselsDrill.HeaderRow.Height = 40;
            GridViewExportUtil.Export("VesselDrillSummary.xls", gvVesselsDrill);
            gvVesselsDrill.GridLines = GridLines.None;
        }
        

    }


}