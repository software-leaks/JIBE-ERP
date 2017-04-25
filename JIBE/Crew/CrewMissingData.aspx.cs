using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewMissingData : System.Web.UI.Page
{
    decimal[] grdTotal = new decimal[13];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["USERID"] == null)
                Response.Redirect("~/account/login.aspx");
            
            Load_ReportData();
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    public void Load_ReportData()
    {
        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
        int vessel_manager = 1;
        int UserID = UDFLib.ConvertToInteger(Session["USERID"].ToString());
        DataSet ds = objCrew.Get_MissingDataReport(vessel_manager,UserID);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        GridView2.DataSource = ds.Tables[1];
        GridView2.DataBind();

        GridView3.DataSource = ds.Tables[2];
        GridView3.DataBind();                
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            for (int i = 1; i < 13; i++)
            {
                if ((HyperLink)e.Row.FindControl("Col" + i.ToString()) != null)
                {
                    string val = ((HyperLink)e.Row.FindControl("Col" + i.ToString())).Text;
                    if (val != "")
                    {
                        decimal rowTotal = Convert.ToDecimal(((HyperLink)e.Row.FindControl("Col" + i.ToString())).Text);
                        grdTotal[i] = grdTotal[i] + rowTotal;
                    }
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            for (int i = 1; i < 13; i++)
            {
                if ((Label)e.Row.FindControl("Total" + i.ToString()) != null)
                {
                    ((Label)e.Row.FindControl("Total" + i.ToString())).Text = grdTotal[i].ToString();
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Contains("&nbsp;")? "": UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[2].Text));
                e.Row.Cells[7].Text = e.Row.Cells[7].Text.Contains("&nbsp;") ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[7].Text));
                e.Row.Cells[8].Text = e.Row.Cells[8].Text.Contains("&nbsp;") ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[8].Text));
            }
            catch
            { }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Cells[5].Text = UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[5].Text));
           
            }
            catch
            { }
        }
    }

 

}