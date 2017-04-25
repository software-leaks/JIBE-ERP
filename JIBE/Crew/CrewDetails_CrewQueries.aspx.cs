using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.IO;
using AjaxControlToolkit4;

public partial class Crew_CrewDetails_CrewQueries : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

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
        if (Session["USERID"] == null)
        {
            lblMsg.Text = "Session Expired!! Log-out and log-in again.";
        }
        else
        {

            if (!IsPostBack)
            {
                UserAccessValidation();
                if (objUA.View == 1)
                {
                    Load_Crew_Queries();
                }
            }
        }
    }

    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
        }
        if (objUA.Add == 0)
        {
            
        }
        if (objUA.Edit == 0)
        {
            
        }
        if (objUA.Delete == 0)
        {
            
        }
        if (objUA.Approve == 0)
        {            
        }
        //-- MANNING OFFICE LOGIN --
       
    }

    protected void Load_Crew_Queries()
    {
        int FleetCode = 0;
        int Vessel_ID = 0;
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        int Query_Type = 0;
        int Status = 0;
        int PAGE_SIZE = 1000;
        int PAGE_INDEX = 1;
        int SelectRecordCount = 0;
        string SortBy = (ViewState["SORTBY"] == null) ? null : (ViewState["SORTBY"].ToString());
        int? SortDirection = null;
        if (ViewState["SORTDIRECTION"] != null) SortDirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Crew_Queries.Get_CrewQueries(FleetCode, Vessel_ID, CrewID, Query_Type, Status, null, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, SortBy, SortDirection);
        
        GridView_CrewQueries.DataSource = dt;
        GridView_CrewQueries.DataBind();

        
    }

    protected void GridView_CrewQueries_Sorting(object sender, EventArgs e)
    {

    }

}