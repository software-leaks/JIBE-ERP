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

public partial class Crew_CrewDetails_NewEvaluation : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string DFormat = "";
    public int CrewID = 0;

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
        else if (!IsPostBack)
        {
            CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            UserAccessValidation();
            DFormat = UDFLib.GetDateFormat();
            calEvaDate.Format = DFormat;
            txtEvaDueDate.Text = DateTime.Today.AddDays(3).ToString(DFormat);
            if (objUA.View == 1)
            {
               
            }

        }
    }
    protected void Load_EvaluatorRank(int CrewID)
    {
        try
        {
            GridView_Evaluation.DataSource = BLL_Crew_Evaluation.Get_Assigned_EvaluatorRank_ForCrew(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]));
            GridView_Evaluation.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void Load_Assigned_Evaluations_ForCrew()
    {
        DataTable dt = BLL_Crew_Evaluation.Get_Assigned_Evaluations_ForCrew(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]));
        if (dt.Rows.Count > 0)
        {
            ddlEvaluations.DataSource = dt;
            ddlEvaluations.DataBind();
        }
        else
        {
            lblMsg.Text = "There is no evaluation sheet assigned to this rank. Please contact your administrator.";
            pnlUnplannedEval.Visible = false;
        }
    }
    protected void UserAccessValidation()
    {
        if (new BLL_Infra_UserCredentials().Get_UserAccessForPage(GetSessionUserID(), UDFLib.GetPageURL(Request.Path.ToUpper())).View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            pnlUnplannedEval.Visible = false;
        }
        else
        {
            pnlUnplannedEval.Visible = true;
            Load_Assigned_Evaluations_ForCrew();
            Load_EvaluatorRank(CrewID);
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

    protected void btnUnplannedEvaluation_Click(object sender, EventArgs e)
    {
        if (txtEvaDueDate.Text.Trim() != "")
        {
            string DueDate = txtEvaDueDate.Text.Replace("/", "-");
            string EID = ddlEvaluations.SelectedValue;
            string CrewID = Request.QueryString["CrewID"].ToString();

            ResponseHelper.Redirect("~/CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID + "&DueDate=" + DueDate, "_blank", "");
        }
        else
        {
            string js = "alert('Enter Due date');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
        }
    }

    protected void btnPlanEvaluationForMaster_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridView_Evaluation.Rows.Count > 0)
            {
                if (txtEvaDueDate.Text.Trim() != "")
                {
                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
                    DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, 0, GetSessionUserID());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Sign_On_Date"].ToString() != "")
                        {
                            int Res = BLL_Crew_Evaluation.Plan_Crew_Evaluation_Adhoc(CrewID, UDFLib.ConvertToInteger(ddlEvaluations.SelectedValue), UDFLib.ConvertToDate(txtEvaDueDate.Text.Trim()).ToString(), GetSessionUserID());

                            string js = "";
                            if (Res == -1)
                                js = "alert('There is another evaluation already planned for this date.');";
                            if (Res == -2)
                                js = "alert('There is no voyage found for this staff.');";
                            if (Res > 0)
                                js = "alert('Evaluation Planned.');parent.hideModal('dvPopupFrame');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        }
                        else
                        {
                            string js = "alert('Evaluation cannot be planned as  crew is not onboard.');parent.hideModal('dvPopupFrame');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        }
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "alert('Select due date.');", true);
            }
            else
            {
                string js = "alert('There is no evaluator on Vessel.Evaluation cannot be planned now.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}