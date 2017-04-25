using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using SMS.Business.Infrastructure;

public partial class Operations_PreArrivalReportIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindVesselGrid();
        BindItems(0);
    }

    protected void BindVesselGrid()
    {
        int Fleet_ID = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = 1;

        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        gvVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);
        gvVessel.DataBind();
    }
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AllVessel")
        {
         }
    }
    protected void BindItems(int Vessel_ID)
    {
        int Record_count = 0;
        ViewState["Sort_Column"] = "";
        ViewState["Sort_Direction"] = "";

        gvPreArrivalReportIndex.DataSource = BLL_OPS_PortReport.Get_PreArrivalPortInfo(Vessel_ID);
        gvPreArrivalReportIndex.DataBind();

        //ucCustomPagerItems.CountTotalRec = Record_count.ToString();
        //ucCustomPagerItems.BuildPager();
    }


    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvVessel, "Select$" + e.Row.RowIndex);
        }
    }


    protected void gvPreArrivalReportIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string strPortId = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
                string strVesselId = DataBinder.Eval(e.Row.DataItem, "Vessel_Id").ToString();

                for (int ix = 1; ix < e.Row.Cells.Count - 1; ix++)
                {
                    e.Row.Cells[ix].Attributes.Add("onclick", "showdetails('" + strPortId + "," + strVesselId + "')");
                    e.Row.Cells[ix].Attributes.Add("style", "cursor:pointer");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void gvVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvVessel != null && gvVessel.SelectedIndex > 0)
        {
            GridViewRow row = gvVessel.SelectedRow;
            row.BackColor = System.Drawing.Color.Yellow;
            if (row != null && row.Cells[1].Text != "")
            {
                BindItems(int.Parse(row.Cells[1].Text.ToString()));
            }
        }
    }
}