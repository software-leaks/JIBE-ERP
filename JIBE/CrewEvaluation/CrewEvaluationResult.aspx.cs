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

public partial class Crew_CrewEvaluationResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

            Load_CrewEvaluationResults(CrewID);

        }
    }    
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

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
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_CrewEvaluationResults(int CrewID)
    {
        
        int Year = DateTime.Today.Year;

        DataTable dt = BLL_Crew_Evaluation.Get_CrewEvaluationResults(CrewID, Year);

        DataView dataView = new DataView(dt);
        try
        {
            if (ViewState["sortExpression"] != null)
                dataView.Sort = ViewState["sortExpression"].ToString();
        }
        catch { }

        GridView_Evaluation.DataSource = dataView;
        GridView_Evaluation.DataBind();

        Enable_Evaluation();
    }

    protected void Enable_Evaluation()
    {
        bool Eval_Due = false;
        for (int i = GridView_Evaluation.Rows.Count - 1; i >= 0; i--)
        {
            GridViewRow row = GridView_Evaluation.Rows[i];

            if (row.RowType == DataControlRowType.DataRow)
            {
                if (Eval_Due == false)
                {
                    HyperLink btnEvaluate = (HyperLink)row.FindControl("btnEvaluate");
                    if (btnEvaluate != null)
                    {
                        if (btnEvaluate.Visible == true)
                        {
                            btnEvaluate.Enabled = true;
                            Eval_Due = true;
                        }
                    }
                }
            }
        }
    }


    protected void GridView_Evaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string CrewID = Request.QueryString["ID"].ToString();
            string EID = DataBinder.Eval(e.Row.DataItem, "Evaluation_ID").ToString();

            HyperLink btnEvaluate = (HyperLink)e.Row.FindControl("btnEvaluate");
            if (btnEvaluate != null)
            {
                btnEvaluate.NavigateUrl = "../CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID;
            }

            HyperLink btnViewEvaluation = (HyperLink)e.Row.FindControl("btnViewEvaluation");
            if (btnViewEvaluation != null)
            {
                btnViewEvaluation.NavigateUrl = "../CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID;
            }
        }
    }
}