using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MEPerformanceReport;

public partial class eForms_eFormTempletes_MEPerformanceReport : System.Web.UI.Page
{
    int Form_ID;
    int Dtl_Report_ID;
    int Vessel_ID;
    decimal PRESS_MAX = 0, PRESS_COMP = 0, PRESS_MAX_COMP = 0, MIP = 0, FUEL_PUMP_INDEX = 0, FUEL_PUMP_VIT = 0, EXH_TEMP = 0, IHP = 0, MAX_CYL_WEAR_AT_LASTOVERHAUL = 0;

    MergeGridviewHeader_Info objMergeList = new MergeGridviewHeader_Info();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form_ID = UDFLib.ConvertToInteger(Request.QueryString["Form_ID"]);
            Dtl_Report_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"]);
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

            objMergeList.AddMergedColumns(new int[] { 5, 6 }, "Fuel Pump", "HeaderStyle-css-2");
            objMergeList.AddMergedColumns(new int[] { 10, 11, 12, 13 }, "Running Hours Since", "HeaderStyle-css-2");

            Load_ME_Perf_Report_Details(Dtl_Report_ID, Vessel_ID);
        }
    }

    protected void GridView_CYL_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (Session["UTYPE"].ToString() != "MANNING AGENT")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    MergeGridviewHeader.SetProperty(objMergeList);

                    e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                    ViewState["DynamicHeaderCSS"] = "HeaderStyle-css-2";
                }
            }
        }
        catch { }
    }

    private void Load_ME_Perf_Report_Details(int Report_ID, int Vessel_ID)
    {
        DataSet ds = BLL_FRM_MEPerformanceReport.Get_MEPerformanceReport(Report_ID, Vessel_ID);

        frmMain.DataSource = ds.Tables[4];
        frmMain.DataBind();

        frmMiscData.DataSource = ds.Tables[4];
        frmMiscData.DataBind();

        GridView_CYL.DataSource = ds.Tables[3];
        GridView_CYL.DataBind();

        TC1.Show_TC_Record(1, Report_ID, Vessel_ID);
        TC2.Show_TC_Record(2, Report_ID, Vessel_ID);
        TC3.Show_TC_Record(3, Report_ID, Vessel_ID);
        TC4.Show_TC_Record(4, Report_ID, Vessel_ID);

    }

    protected void GridView_CYL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PRESS_MAX = PRESS_MAX + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "PRESS_MAX").ToString());
            PRESS_COMP = PRESS_COMP + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "PRESS_COMP").ToString());
            PRESS_MAX_COMP = PRESS_MAX_COMP + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "PRESS_MAX_COMP").ToString());
            MIP = MIP + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "MIP").ToString());
            FUEL_PUMP_INDEX = FUEL_PUMP_INDEX + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "FUEL_PUMP_INDEX").ToString());
            FUEL_PUMP_VIT = FUEL_PUMP_VIT + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "FUEL_PUMP_VIT").ToString());
            EXH_TEMP = EXH_TEMP + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "EXH_TEMP").ToString());
            IHP = IHP + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "IHP").ToString());
            MAX_CYL_WEAR_AT_LASTOVERHAUL = MAX_CYL_WEAR_AT_LASTOVERHAUL + UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "MAX_CYL_WEAR_AT_LASTOVERHAUL").ToString());
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAVG_PRESS_MAX = (Label)e.Row.FindControl("lblAVG_PRESS_MAX");
            if (lblAVG_PRESS_MAX != null)
                lblAVG_PRESS_MAX.Text = (PRESS_MAX/12).ToString("0.00");

            Label lblAVG_PRESS_COMP = (Label)e.Row.FindControl("lblAVG_PRESS_COMP");
            if (lblAVG_PRESS_COMP != null)
                lblAVG_PRESS_COMP.Text = (PRESS_COMP / 12).ToString("0.00");

            Label lblAVG_PRESS_MAX_COMP = (Label)e.Row.FindControl("lblAVG_PRESS_MAX_COMP");
            if (lblAVG_PRESS_MAX_COMP != null)
                lblAVG_PRESS_MAX_COMP.Text = (PRESS_MAX_COMP / 12).ToString("0.00");

            Label lblAVG_MIP = (Label)e.Row.FindControl("lblAVG_MIP");
            if (lblAVG_MIP != null)
                lblAVG_MIP.Text = (MIP/ 12).ToString("0.00");

            Label lblAVG_FUEL_PUMP_INDEX = (Label)e.Row.FindControl("lblAVG_FUEL_PUMP_INDEX");
            if (lblAVG_FUEL_PUMP_INDEX != null)
                lblAVG_FUEL_PUMP_INDEX.Text = (FUEL_PUMP_INDEX/ 12).ToString("0.00");


            Label lblAVG_FUEL_PUMP_VIT = (Label)e.Row.FindControl("lblAVG_FUEL_PUMP_VIT");
            if (lblAVG_FUEL_PUMP_VIT != null)
                lblAVG_FUEL_PUMP_VIT.Text = (FUEL_PUMP_VIT/ 12).ToString("0.00");


            Label lblAVG_EXH_TEMP = (Label)e.Row.FindControl("lblAVG_EXH_TEMP");
            if (lblAVG_EXH_TEMP != null)
                lblAVG_EXH_TEMP.Text = (EXH_TEMP / 12).ToString("0.00");


            Label lblAVG_IHP = (Label)e.Row.FindControl("lblAVG_IHP");
            if (lblAVG_IHP != null)
                lblAVG_IHP.Text = (IHP / 12).ToString("0.00");


            Label lblAVG_MAX_CYL_WEAR_AT_LASTOVERHAUL = (Label)e.Row.FindControl("lblAVG_MAX_CYL_WEAR_AT_LASTOVERHAUL");
            if (lblAVG_MAX_CYL_WEAR_AT_LASTOVERHAUL != null)
                lblAVG_MAX_CYL_WEAR_AT_LASTOVERHAUL.Text = (MAX_CYL_WEAR_AT_LASTOVERHAUL / 12).ToString("0.00");

        }
    }
}