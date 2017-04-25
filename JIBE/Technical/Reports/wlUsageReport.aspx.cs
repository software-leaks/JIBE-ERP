using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Technical;

public partial class Technical_Reports_wlUsageReport : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy", iFormatProvider);
            txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy", iFormatProvider);

            //JobsCompleted_PrevWeeks();
        }
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        //JobsCompleted_PrevWeeks();
    }


    //protected void JobsCompleted_PrevWeeks()
    //{
    //    DateTime StDt = DateTime.Parse(txtStartDate.Text);

    //    lblPrevWk1St.Text = StDt.AddDays(-7).ToString("dd/MM/yyyy");
    //    lblPrevWk1End.Text = StDt.ToString("dd/MM/yyyy");
    //    lblPrevWk2St.Text = StDt.AddDays(-14).ToString("dd/MM/yyyy");
    //    lblPrevWk2End.Text = StDt.AddDays(-7).ToString("dd/MM/yyyy");

    //    BLL_Tec_Worklist objWL = new BLL_Tec_Worklist();

    //    int t1 = objWL.Get_CompletedJobs_Count(lblPrevWk1St.Text, lblPrevWk1End.Text);
    //    int t2 = objWL.Get_CompletedJobs_Count(lblPrevWk2St.Text, lblPrevWk2End.Text);

    //    lblPrevWk1Total.Text = t1.ToString();
    //    lblPrevWk2Total.Text = t2.ToString();

    //    //DataSet ds = objWL.Get_CompletedJobs_Prev2Wks(lblPrevWk1St.Text, lblPrevWk1End.Text);
    //    //GridView_PrevWk1.DataSource = ds.Tables[0];
    //    //GridView_PrevWk1.DataBind();

    //    //GridView_PrevWk2.DataSource = ds.Tables[1];
    //    //GridView_PrevWk2.DataBind();
    //}


    protected void GridViewJobsCompleted_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
        e.Row.Cells[e.Row.Cells.Count - 2].ForeColor = System.Drawing.Color.Black;
        e.Row.Cells[e.Row.Cells.Count - 3].ForeColor = System.Drawing.Color.Black;
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            e.Row.Cells[e.Row.Cells.Count - 1].ControlStyle.BackColor = System.Drawing.Color.Cyan;
            e.Row.Cells[e.Row.Cells.Count - 2].ControlStyle.BackColor = System.Drawing.Color.Cyan;
            if (e.Row.DataItem != null)
            {
                string Vessel_Short_Name = DataBinder.Eval(e.Row.DataItem, "Vessel").ToString();

                if (Vessel_Short_Name == "Total")
                {
                    e.Row.CssClass = "SelectedRow";
                }
            }
        }
    }

    protected void GridViewJobsIncomplete_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                string Vessel_Short_Name = DataBinder.Eval(e.Row.DataItem, "Vessel").ToString();

                if (Vessel_Short_Name == "Total")
                {
                    e.Row.CssClass = "SelectedRow";
                }
            }
        }
    }

    protected void GridViewJobsNoFollowup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                string Vessel_Short_Name = DataBinder.Eval(e.Row.DataItem, "Vessel").ToString();

                if (Vessel_Short_Name == "Total")
                {
                    e.Row.CssClass = "SelectedRow";
                }
            }
        }
    }

        protected void GridViewJobsWithFollowup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {

                string Vessel_Short_Name = DataBinder.Eval(e.Row.DataItem, "Vessel Name").ToString();

                if (Vessel_Short_Name == "Total")
                {
                    e.Row.CssClass = "SelectedRow";
                }
            }
        }
    }




        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {

        }
}