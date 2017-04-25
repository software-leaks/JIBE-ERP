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

public partial class Crew_CrewMissingDataDetails : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
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

        int MOID = Request.QueryString["MOID"] == null ? 0 : UDFLib.ConvertToInteger(Request.QueryString["MOID"].ToString());
        string COL = Request.QueryString["C"] == null?"":Request.QueryString["C"].ToString();

        DataSet ds = objCrew.Get_MissingDataDetails(vessel_manager, UserID, MOID, COL);

        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();
       
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        GridView4.AllowPaging = false;
        Load_ReportData();
        GridViewExportUtil.Export("Crew Missing-Data.xls", GridView4);
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Cells[7].Text = (!e.Row.Cells[7].Text.Contains("&nbsp")) ? UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[7].Text)) : "";
                e.Row.Cells[9].Text = (!e.Row.Cells[9].Text.Contains("&nbsp")) ? UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[9].Text)) : "";

            }
            catch
            { }
        }
    }

}