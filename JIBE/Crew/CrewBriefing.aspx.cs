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
using System.Configuration;

public partial class Crew_CrewBriefing : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdminBLL = new BLL_Crew_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = Convert.ToString(Session["User_DateFormat"]);

        if (!IsPostBack)
        {
            UserAccessValidation();
            string InterviewPlannedDate = "";
            int InterviewID = 0;
            int iUserID = GetSessionUserID();

            Load_RankList();
            if (getQueryString("ID") != "")
            {
                InterviewID = int.Parse(getQueryString("ID"));
                DataTable dt = BLL_Crew_Interview.getInterviewDetails(InterviewID);

                if (dt.Rows.Count > 0)
                {
                    pnlEdit_InterviewResult.Visible = true;

                    hdnInterviewID.Value = InterviewID.ToString();
                    hdnCrewID.Value = dt.Rows[0]["CrewID"].ToString();

                    int iCrewID = int.Parse(dt.Rows[0]["CrewID"].ToString());

                    InterviewPlannedDate = dt.Rows[0]["InterviewPlanDate"].ToString();
                    lnkOpenProfile.NavigateUrl = "CrewDetails.aspx?ID=" + iCrewID;

                    lblPlannedInterviewer.Text = dt.Rows[0]["PlannedInterviewer"].ToString();
                    lblPlannedDate.Text = UDFLib.ConvertUserDateFormatTime(Convert.ToString(dt.Rows[0]["InterviewPlanDate"]));
                    lblPlannedBy.Text = dt.Rows[0]["PlannedBy"].ToString();
                    lblPlannedTimeZone.Text = dt.Rows[0]["DisplayName"].ToString();

                    txtPersonInterviewed.Text = dt.Rows[0]["CandidateName"].ToString();
                    txtPersonInterviewed.Enabled = false;
                    if (dt.Rows[0]["InterviewDate"].ToString().Trim() != "")
                    {
                        txtInterviewDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["InterviewDate"]));
                    }
                    else
                    {
                        txtInterviewDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));
                    }

                    txtInterviewDate.Enabled = false;

                    if (dt.Rows[0]["RANKID"].ToString() != "")
                    {
                        ddlRank.SelectedValue = dt.Rows[0]["RANKID"].ToString();
                        ddlRank.Enabled = false;
                    }
                    txtStaffCode.Text = dt.Rows[0]["staff_code"].ToString();
                    if (dt.Rows[0]["Interviewer"].ToString().Trim() != "")
                    {
                        ddlUserList.Items.Clear();
                        ddlUserList.Items.Add(new ListItem(dt.Rows[0]["Interviewer"].ToString(), dt.Rows[0]["InterviewerID"].ToString()));
                        ddlUserList.SelectedIndex = 0;
                        btnSaveInterviewResult.Enabled = false;
                    }
                    else
                    {
                        ddlUserList.DataBind();
                        ddlUserList.SelectedValue = GetSessionUserID().ToString();
                        btnSaveInterviewResult.Enabled = true;
                    }
                    ddlUserList.Enabled = false;

                    lblPlannedDate.Text = UDFLib.ConvertUserDateFormatTime(Convert.ToString(dt.Rows[0]["InterviewPlanDate"]));
                    lblPlannedBy.Text = dt.Rows[0]["PlannedBy"].ToString();

                    // int IQID = UDFLib.ConvertToInteger(dt.Rows[0]["IQID"]);

                    DataSet dsQA = BLL_Crew_Interview.Get_InterviewQuestionAnswers(InterviewID, GetSessionUserID());
                    GridView_AssignedCriteria.DataSource = dsQA.Tables[0];
                    GridView_AssignedCriteria.DataBind();
                }
            }
            else
            {
                int CrewID = GetCrewID();
                DataTable dtCD = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID);
                if (dtCD.Rows.Count > 0)
                {
                    txtInterviewDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));
                    txtInterviewDate.Enabled = false;
                    //ddlRank.Text = dtCD.Rows[0]["Rank_Applied"].ToString();

                    pnlEdit_InterviewResult.Visible = false;
                    lblMessage.Text = "Interview is not yet planned for the crew. Please fill the interview plan";
                }
                else
                    Response.Redirect("Crewlist.aspx");
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser1", "initScript();", true);
    }

    protected void Load_RankList()
    {
        DataTable dt = objCrewAdminBLL.Get_RankList();
        ddlRank.DataSource = dt;
        ddlRank.DataBind();
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        if (CurrentUserID == 0)
            Response.Redirect("~/account/login.aspx");

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
        if (Session["USERCOMPANYID"].ToString() != "1")
        {
            //Response.Redirect("~/default.aspx?msgid=2");
        }
        else//--- CREW TEAM LOGIN--
        {
        }
    }

    public int GetCrewID()
    {
        try
        {
            if (getQueryString("CrewID") != "")
            {
                return int.Parse(getQueryString("CrewID"));
            }
            else if (hdnCrewID.Value != "0")
            {
                return int.Parse(hdnCrewID.Value);
            }
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }

    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }

    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int iCrewID = GetCrewID();
        string js = "window.close();parent.GetInterviewResult(" + iCrewID + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }

    protected void btnSaveInterviewResult_Click(object sender, EventArgs e)
    {
        string js = "";

        try
        {
            int iCrewID = GetCrewID();
            int iUserID = GetSessionUserID();
            int InterviewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
            int InterviewerID = GetSessionUserID();
            int QustionAnswered = 0;
            if (InterviewID > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("QID");
                dt.Columns.Add("SelectedOptionID");
                dt.Columns.Add("NotApplicable");
                dt.Columns.Add("Remarks");

                foreach (GridViewRow gvr in GridView_AssignedCriteria.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkNA");
                    RadioButtonList rdoOptions = (RadioButtonList)gvr.FindControl("rdoOptions");
                    TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");
                    int QID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[gvr.RowIndex].Value);

                    int NotApplicable = -1;
                    if (chk != null)
                    {
                        NotApplicable = chk.Checked == true ? 1 : 0;
                        if (NotApplicable == 1)
                            QustionAnswered = 1;
                    }

                    int SelectedOptionID = 0;
                    if (rdoOptions != null)
                    {
                        if (rdoOptions.SelectedIndex != -1)
                        {
                            SelectedOptionID = UDFLib.ConvertToInteger(rdoOptions.SelectedValue.Split(',')[0]);
                            QustionAnswered = 1;
                        }
                    }
                    string Remarks = "";
                    if (txtRemarks != null)
                        Remarks = txtRemarks.Text;
                    DataRow dr = dt.NewRow();
                    dr["QID"] = QID;
                    dr["SelectedOptionID"] = SelectedOptionID;
                    dr["NotApplicable"] = NotApplicable;
                    dr["Remarks"] = Remarks;
                    dt.Rows.Add(dr);
                }
                if (QustionAnswered > 0)
                {
                    string InterviewDate = UDFLib.ConvertToDate(Convert.ToString(txtInterviewDate.Text), UDFLib.GetDateFormat()).ToString();
                    int Ret = BLL_Crew_Interview.UPDATE_CrewInterviewResult(iCrewID, InterviewID, InterviewerID, InterviewDate, 0, "", "", "", "", iUserID, dt);
                    js = "Briefing result updated";
                    btnSaveInterviewResult.Enabled = false;
                }
                else
                {
                    js = "Answer atleast one question before saving briefing";
                }
            }
            else
                lblMessage.Text = "Please select the crew again to fill the interview result.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        if (js.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser3", "alert('" + js + "');", true);

        }
    }

    protected void GridView_AssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iQuestionID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "QID").ToString());
                int Grading_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
                // int iRankID = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
                int iInterviewID = int.Parse(hdnInterviewID.Value);

                int iCrewID = GetCrewID();
                int iUserID = GetSessionUserID();

                string SelectedOptionID;

                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                if (rdo != null)
                {
                    DataTable dt = BLL_Crew_Interview.Get_GradingOptions(Grading_Type);
                    rdo.DataSource = dt;
                    rdo.DataBind();

                    if (DataBinder.Eval(e.Row.DataItem, "SelectedOptionID") != null)
                    {
                        SelectedOptionID = DataBinder.Eval(e.Row.DataItem, "SelectedOptionID").ToString();
                        if (SelectedOptionID != "")
                        {
                            e.Row.CssClass = "crew-interview-grid-selected-row";
                            Select_Option(rdo, SelectedOptionID);
                        }
                    }

                }
            }

        }
        catch
        {
        }
    }

    private void Select_Option(RadioButtonList rdo, string SelectedOptionID)
    {
        foreach (ListItem Opt in rdo.Items)
        {
            if (Opt.Value.StartsWith(SelectedOptionID))
            {
                Opt.Selected = true;
                break;
            }
        }
    }
}