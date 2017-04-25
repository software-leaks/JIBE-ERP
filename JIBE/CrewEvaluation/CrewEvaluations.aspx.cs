using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class CrewEvaluation_CrewEvaluations : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    

    bool Eval_Due = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            int CrewID = 0;

            if (Request.QueryString["CrewID"] != null)
                CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());

            Bind_CrewEvaluationResults();
            Load_CrewPersonalDetails(CrewID);
        }
        UserAccessValidation();
    }

    protected void UserAccessValidation()
    {
        
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
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

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void Bind_CrewEvaluationResults()
    {
        int CrewID = 0;

        if (Request.QueryString["CrewID"] != null)
            CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
        int Year = DateTime.Today.Year;

        DataTable dt = BLL_Crew_Evaluation.Get_CrewEvaluationResults(CrewID,Year);

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
    protected void Load_CrewPersonalDetails(int ID)
    {
        DataTable dt = objCrew.Get_CrewPersonalDetailsByID(ID);
        if (dt.Rows.Count > 0)
        {

            lblStaffName.Text = dt.Rows[0]["STAFF_FULLNAME"].ToString();
            lblStaffCode.Text = dt.Rows[0]["STAFF_CODE"].ToString();
            lblRank.Text = dt.Rows[0]["RANK_NAME"].ToString();
            hdnCrewrank.Value = dt.Rows[0]["CurrentRankID"].ToString();
            

            if (dt.Rows[0]["Sign_On_Date"].ToString() != "")
                lblSignOn.Text = DateTime.Parse(dt.Rows[0]["Sign_On_Date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));

            if (dt.Rows[0]["Est_Sing_Off_Date"].ToString() != "")
                lblCOC.Text = DateTime.Parse(dt.Rows[0]["Est_Sing_Off_Date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));
            
            if (dt.Rows[0]["Sign_Off_Date"].ToString() != "")
                lblCOC.Text = DateTime.Parse(dt.Rows[0]["Sign_Off_Date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));
        }
    }
    protected void btnEvaluate_Click(object sender, EventArgs e)
    {
        string EID = ((Button)sender).CommandArgument;
        string CrewID = Request.QueryString["CrewID"].ToString();

        ResponseHelper.Redirect("DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID, "_blank", "");
    }
    protected void btnViewEvaluation_Click(object sender, EventArgs e)
    {
        string EID = ((LinkButton)sender).CommandArgument;
        string CrewID = Request.QueryString["CrewID"].ToString();

        ResponseHelper.Redirect("DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID, "_blank", "");
    }

    protected void Enable_Evaluation()
    {
        for (int i = GridView_Evaluation.Rows.Count - 1; i >= 0; i--)
        {
            GridViewRow row = GridView_Evaluation.Rows[i];

            if (row.RowType == DataControlRowType.DataRow)
            {
                if (Eval_Due == false)
                {
                    Button btnEvaluate = (Button)row.FindControl("btnEvaluate");
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
            string CrewID = Request.QueryString["CrewID"].ToString();
            string EID = DataBinder.Eval(e.Row.DataItem, "Evaluation_ID").ToString();
            string CrewEvaluation_ID = DataBinder.Eval(e.Row.DataItem, "CrewEvaluation_ID").ToString();
            string DueDate = DataBinder.Eval(e.Row.DataItem, "DueDate").ToString();
            string SchID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string Evaluation_Date = DataBinder.Eval(e.Row.DataItem, "Evaluation_Date").ToString();
            string Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();
            string Office_ID = DataBinder.Eval(e.Row.DataItem, "Office_ID").ToString();
            string MonthNo = DataBinder.Eval(e.Row.DataItem, "MonthNo").ToString();

            HyperLink btnViewEvaluation = (HyperLink)e.Row.FindControl("btnViewEvaluation");
            HyperLink btnPendingEvaluation = (HyperLink)e.Row.FindControl("BtnPendingEvaluation");
            if (btnViewEvaluation != null)
            {
                btnViewEvaluation.NavigateUrl = "../CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID + "&DTLID=" + CrewEvaluation_ID + "&M=" + DueDate.Replace("/", "-") + "&SchID=" + SchID;
            }
            if (btnPendingEvaluation != null)
            {
                btnPendingEvaluation.NavigateUrl = "../CrewEvaluation/DoEvaluation.aspx?CrewID=" + CrewID + "&EID=" + EID + "&M=" + DueDate.Replace("/", "-") + "&SchID=" + SchID;
            }

            ImageButton lnkAddFeedBk = (ImageButton)e.Row.FindControl("lnkAddFeedBk");
            ImageButton lnkReqFeedBk = (ImageButton)e.Row.FindControl("lnkReqFeedBk");
            if (lnkAddFeedBk != null)
            {
                string PageURL = "FeedbackRequest.aspx?ID=" + CrewEvaluation_ID + "&Vessel_ID=" + Vessel_ID + "&Office_ID=" + Office_ID + "&Crew_ID=" + CrewID + "&Evaluation_ID=" + EID + "&Month=" + MonthNo + "&Schedule_ID=" + SchID + "&btnID=" + lnkAddFeedBk.ID.ToString();
                lnkAddFeedBk.OnClientClick = "showDialog('" + PageURL + "');return false;";
            }
            if (lnkReqFeedBk != null)
            {
                string PageURL = "FeedbackRequest.aspx?ID=" + CrewEvaluation_ID + "&Vessel_ID=" + Vessel_ID + "&Office_ID=" + Office_ID + "&Crew_ID=" + CrewID + "&Evaluation_ID=" + EID + "&Month=" + MonthNo + "&Schedule_ID=" + SchID + "&btnID=" + lnkReqFeedBk.ID.ToString();
                lnkReqFeedBk.OnClientClick = "showDialog('" + PageURL + "');return false;";
            }

            DateTime dtRes = DateTime.Now;
            if (string.IsNullOrEmpty(Evaluation_Date))
            {
                if (DateTime.TryParse(DueDate, out dtRes) && dtRes < DateTime.Today)
                {
                    e.Row.Cells[e.Row.Cells.Count - 2].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[e.Row.Cells.Count - 2].ForeColor = System.Drawing.Color.Yellow;
                }
            }
        }
    }
}