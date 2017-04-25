using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;

public partial class Operations_PreArrivalDetails : System.Web.UI.Page
{
    int _Vessel_Id = 0;
    int _Office_Id = 0;
    int _PortInfoReportId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Request.QueryString["VesselId"] != null && Request.QueryString["PortInfoReportId"] != null && Request.QueryString["OfficeId"] != null)
        {
            _Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
            _PortInfoReportId = int.Parse(Request.QueryString["PortInfoReportId"].ToString());
            _Office_Id = int.Parse(Request.QueryString["OfficeId"].ToString());
            DataSet ds = BLL_OPS_PortReport.Get_PORT_PreArrival_Info(_PortInfoReportId, _Vessel_Id, _Office_Id);

            dgNav.DataSource = ds.Tables[0];
            dgNav.DataBind();
            GridViewHelper helper = new GridViewHelper(this.dgCounter);
            helper.RegisterGroup("SubType", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            dgCounter.DataSource = ds.Tables[1];
            dgCounter.DataBind();
            dgPsc.DataSource = ds.Tables[2];
            dgPsc.DataBind();

        }



    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "High")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[7].Text == "Medium")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Orange;
            }
            if (e.Row.Cells[7].Text == "Low")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
            }
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text == "High")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
            }
            if (e.Row.Cells[5].Text == "Medium")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Orange;
            }
            if (e.Row.Cells[5].Text == "Low")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
            }
        }
    }


    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "SubType")
        {
            row.BackColor = System.Drawing.Color.LightGray;

            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;

        }

    }
}