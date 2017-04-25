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

public partial class Crew_CrewDetails_EvaluationResult : System.Web.UI.Page
{
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
                int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

                if (objUA.View == 1)
                    Load_CrewEvaluationResults(CrewID);

            }
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
            GridView_Evaluation.Columns[GridView_Evaluation.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --
        
    }
    public int GetSessionUserID()
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
            string CrewEvaluation_ID = DataBinder.Eval(e.Row.DataItem, "CrewEvaluation_ID").ToString();
            string DueDate = DataBinder.Eval(e.Row.DataItem, "DueDate").ToString();
            string SchID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string Evaluation_Date = DataBinder.Eval(e.Row.DataItem, "Evaluation_Date").ToString();
  
            HyperLink btnViewEvaluation = (HyperLink)e.Row.FindControl("btnViewEvaluation");
            HyperLink btnPendingEval = (HyperLink)e.Row.FindControl("BtnPendingEvaluation");
            if (btnViewEvaluation != null)
            {
                btnViewEvaluation.NavigateUrl = "../CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID + "&DTLID=" + CrewEvaluation_ID + "&M=" + DueDate.Replace("/", "-") + "&SchID=" + SchID;
            }
            if (btnPendingEval != null)
            {
                btnPendingEval.NavigateUrl = "../CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID + "&M=" + DueDate.Replace("/", "-") + "&SchID=" + SchID;

            }
            DateTime dtRes = DateTime.Now;
            if (string.IsNullOrEmpty(Evaluation_Date))
            {
                if (DateTime.TryParse(DueDate, out dtRes) && dtRes < DateTime.Today)
                {
                    e.Row.Cells[e.Row.Cells.Count - 3].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[e.Row.Cells.Count - 3].ForeColor = System.Drawing.Color.Yellow;
                }
            }
         }
    }
}