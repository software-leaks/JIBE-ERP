using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using System.Data;

public partial class Operations_PreArrivalReportDetails : System.Web.UI.Page
{
    int Id = 0;
    int VesselId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Id = int.Parse(Request.QueryString["Id"].ToString());
        VesselId = int.Parse(Request.QueryString["VesselId"].ToString());
        BindReport();
    }

    protected void BindReport()
    {
        DataSet ds = BLL_OPS_PortReport.Get_PreArrivalInfoDetail(Id, VesselId);

        if (ds.Tables[0] != null)
        {
            fvPortInfoReport.DataSource = ds.Tables[0];
            fvPortInfoReport.DataBind();

            lblGridHeader.Text  = ds.Tables[0].Rows[0]["CompanyName"].ToString() + " performance history in this port";
        }
        if (ds.Tables[1] != null)
        {
            GridView1.DataSource = ds.Tables[1];
            GridView1.DataBind();
        }
        if (ds.Tables[2] != null)
        {
            GridViewHelper helper = new GridViewHelper(this.GridView2);
            helper.RegisterGroup("SubType", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);

            GridView2.DataSource = ds.Tables[2];
            GridView2.DataBind();
        }
        if (ds.Tables[3] != null)
        {
            GridView3.DataSource = ds.Tables[3];
            GridView3.DataBind();
        }
        if (ds.Tables[4] != null)
        {
            GridView4.DataSource = ds.Tables[4];
            GridView4.DataBind();
        }
        if (ds.Tables[5] != null)
        {
            GridView5.DataSource = ds.Tables[5];
            GridView5.DataBind();
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
}