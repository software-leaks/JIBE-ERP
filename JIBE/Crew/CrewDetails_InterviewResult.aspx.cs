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

public partial class Crew_CrewDetails_InterviewResult : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    int UserAccessAdminRights = 0;
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
            UserAccessValidation();
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

            if (objUA.View == 1)
            {
                Load_InterviewResults(CrewID, "Interview");
                Load_BriefResults(CrewID, "Briefing");
            }
        }
    }
    protected void Load_InterviewResults(int CrewID, string InterviewType)
    {
        DataSet ds = BLL_Crew_Interview.Get_Crew_Interview_Results(CrewID, GetSessionUserID(), InterviewType);
        if (ds.Tables.Count > 0)
        {
            GridView_InterviewResult.DataSource = ds.Tables[0];
            GridView_InterviewResult.DataBind();
        }
     }
    protected void Load_BriefResults(int CrewID, string InterviewType)
    {
        DataSet ds = BLL_Crew_Interview.Get_Crew_Interview_Results(CrewID, GetSessionUserID(), InterviewType);
        if (ds.Tables.Count > 0)
        {
            gvBriefResult.DataSource = ds.Tables[0];
            gvBriefResult.DataBind();
        }
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
        if (objUA.Admin == 1 && (Session["UTYPE"] != null && !Session["UTYPE"].Equals("MANNING AGENT")))
        {
            UserAccessAdminRights = 1;
        }
        //-- MANNING OFFICE LOGIN --
       
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void GridView_InterviewResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkInterview_Name = (HyperLink) e.Row.FindControl("lnkInterview_Name");
            if (lnkInterview_Name != null)
            {
                int IQID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "IQID"));
                int ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID"));
                int CrewID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "CrewID"));
                int Result = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Result"));

                if (IQID > 0)
                {
                    lnkInterview_Name.NavigateUrl = "CrewInterview.aspx?ID=" + ID.ToString() + "&CrewID=" + CrewID.ToString();
                    e.Row.CssClass = "RowStyle-css RowStyle-css-new";
                }
                else
                    lnkInterview_Name.NavigateUrl = "Interview.aspx?ID=" + ID.ToString() + "&CrewID=" + CrewID.ToString();

                if (Result == 1)
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "interview-title-approved";
                else
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "interview-title-rejected";
            }
            ImageButton imgDelete = (ImageButton)e.Row.FindControl("ImgDelete");
            if (UserAccessAdminRights == 1)
                imgDelete.Visible = true;
            else
                imgDelete.Visible = false;
      
        }
    }

     protected void GridView_BriefResult_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             HyperLink lnkInterview_Name = (HyperLink)e.Row.FindControl("lnkBriefingGivenTo");
             int ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID"));
             int CrewID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "CrewID"));
             lnkInterview_Name.NavigateUrl = "CrewBriefing.aspx?ID=" + ID.ToString() + "&CrewID=" + CrewID.ToString();
             e.Row.CssClass = "RowStyle-css RowStyle-css-new";

             ImageButton imgDelete = (ImageButton)e.Row.FindControl("ImgDelete");
             if (UserAccessAdminRights == 1)
                 imgDelete.Visible = true;
             else
                 imgDelete.Visible = false;
         }
     }

}