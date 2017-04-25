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

public partial class Crew_CrewInterview : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdminBLL = new BLL_Crew_Admin();

    decimal TotalMarks = 0;
    decimal UserMarks = 0;

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
            Load_VesselList();

            if (getQueryString("ID") != "")
            {
                InterviewID = int.Parse(getQueryString("ID"));
                DataTable dt = BLL_Crew_Interview.getInterviewDetails(InterviewID);

                if (dt.Rows.Count > 0)
                {
                    pnlInterviewPlanning.Visible = false;
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
                        txtInterviewDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString( dt.Rows[0]["InterviewDate"]));
                        lblInterviewDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["InterviewDate"]));                       
                    }
                    else
                    {
                        txtInterviewDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));
                    }

                    txtInterviewDate.Enabled = false;

                    if (dt.Rows[0]["RANKID"].ToString() != "")
                    {
                        txtInterviewRank.Text = dt.Rows[0]["Rank_Short_Name"].ToString();
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
                        lnkEditSchedule.Visible = false;

                    }
                    else
                    {
                        ddlUserList.DataBind();
                        ddlUserList.SelectedValue = GetSessionUserID().ToString();
                        btnSaveInterviewResult.Enabled = true;
                        lnkEditSchedule.Visible = true;
                        lnkEditSchedule.Attributes.Add("onclick", "EditInterviewSchedule('CrewInterview.aspx'," + InterviewID.ToString() + "); return false;");
                    }
                    ddlUserList.Enabled = false;

                    lblInterviewSheet_Name.Text = dt.Rows[0]["Interview_Name"].ToString();
                    txtPlanInterviewerPosition.Text = dt.Rows[0]["Designation"].ToString();

                    lblPlannedDate.Text = UDFLib.ConvertUserDateFormatTime(Convert.ToString(dt.Rows[0]["InterviewPlanDate"]));
                    lblPlannedBy.Text = dt.Rows[0]["PlannedBy"].ToString();

                    rdoSelected.SelectedValue = dt.Rows[0]["Result"].ToString();
                    txtResultText.Text = dt.Rows[0]["ResultText"].ToString();

                    int IQID = UDFLib.ConvertToInteger(dt.Rows[0]["IQID"]);
                    int RankID = UDFLib.ConvertToInteger(dt.Rows[0]["RankID"]);

                    DataSet dsQA = BLL_Crew_Interview.Get_InterviewQuestionAnswers(InterviewID, GetSessionUserID());
                    GridView_AssignedCriteria.DataSource = dsQA.Tables[0];
                    GridView_AssignedCriteria.DataBind();

                    if (dsQA.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsQA.Tables[1].Rows)
                        {
                            for (var i = 1; i < lstVessels.Items.Count; i++)
                            {
                                if (lstVessels.Items[i].Value == dr["vesselid"].ToString())
                                {
                                    lstVessels.Items[i].Selected = true;
                                }
                            }
                        }
                    }
                    if (dsQA.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsQA.Tables[2].Rows)
                        {
                            for (var i = 0; i < chkTradingArea.Items.Count; i++)
                            {
                                if (chkTradingArea.Items[i].Value == dr["zoneid"].ToString())
                                {
                                    chkTradingArea.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                    CalculateMarks(InterviewID);
                }
                else
                {
                    pnlInterviewPlanning.Visible = true;
                    pnlEdit_InterviewResult.Visible = false;

                    int CrewID = GetCrewID();
                    DataTable dtCrew = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID);
                    if (dtCrew.Rows.Count > 0)
                    {
                        txtPlanCrewName.Text = dtCrew.Rows[0]["staff_fullname"].ToString();
                        txtPlanCrewName.Enabled = false;

                        txtInterviewDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                        txtInterviewDate.Enabled = false;

                        ddlPlanRank.Text = dtCrew.Rows[0]["Rank_Applied"].ToString();
                        ddlRank.Text = dtCrew.Rows[0]["Rank_Applied"].ToString();

                        pnlInterviewPlanning.Visible = true;
                        pnlEdit_InterviewResult.Visible = false;

                        lblMessage.Text = "Interview is not yet planned for the crew. Please fill the interview plan";
                    }
                }
            }
            else
            {
                int CrewID = GetCrewID();
                DataTable dtCD = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID);
                if (dtCD.Rows.Count > 0)
                {
                    txtPlanCrewName.Text = dtCD.Rows[0]["staff_fullname"].ToString();
                    txtPlanCrewName.Enabled = false;

                    txtInterviewDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtInterviewDate.Enabled = false;

                    ddlPlanRank.Text = dtCD.Rows[0]["Rank_Applied"].ToString();
                    ddlRank.Text = dtCD.Rows[0]["Rank_Applied"].ToString();

                    pnlInterviewPlanning.Visible = true;
                    pnlEdit_InterviewResult.Visible = false;

                    lblMessage.Text = "Interview is not yet planned for the crew. Please fill the interview plan";
                }
                else
                    Response.Redirect("Crewlist.aspx");
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser1", "initScript();", true);
    }

    public void Load_VesselList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int Fleet_ID = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UserCompanyID;

        lstVessels.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        lstVessels.DataTextField = "VESSEL_NAME";
        lstVessels.DataValueField = "VESSEL_ID";
        lstVessels.DataBind();
        lstVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    protected void Load_RankList()
    {
        DataTable dt = objCrewAdminBLL.Get_RankList();
        ddlRank.DataSource = dt;
        ddlRank.DataBind();
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "id";
        ddlPlanRank.DataSource = dt;
        ddlPlanRank.DataBind();

    }

    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        int InterviewID = int.Parse(getQueryString("ID"));
        DataTable dt = BLL_Crew_Interview.getInterviewDetails(InterviewID);

        if (dt.Rows.Count > 0)
        {
            int IQID = UDFLib.ConvertToInteger(dt.Rows[0]["IQID"]);
            int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
            
            if (dt.Rows[0]["RankID"].ToString() != ddlRank.SelectedValue)
            {
                //BLL_Crew_Interview.UPDATE_CrewInterviewPlanning(InterviewID, dt.Rows[0]["InterviewPlanDate"].ToString(), UDFLib.ConvertToInteger(dt.Rows[0]["PlannedInterviewerID"].ToString()), RankID, GetSessionUserID());
            }
            
            lblMaxMarks.Text = "";
            lblUserMarks_P.Text = "";
            lblUserMarks.Text = "";
            lblOutOf5.Text = "";

            DataSet dsQA = BLL_Crew_Interview.Get_InterviewQuestionAnswers(InterviewID, RankID);
            GridView_AssignedCriteria.DataSource = dsQA.Tables[0];
            GridView_AssignedCriteria.DataBind();
        }
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
            //Response.Redirect("~/default.aspx?msgid=1");

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

    protected void lnkEditInterviewPlan_Click(object sender, EventArgs e)
    {

    }

    protected void btnSavePlanning_Click(object sender, EventArgs e)
    {
        try
        {

            int iCrewID = GetCrewID();

            string PlanDateTime = txtPlanDate.Text + " " + ddlPlanH.Text + ":" + ddlPlanM.Text;
            if (int.Parse(ddlPlanH.Text) < 12)
                PlanDateTime += " AM";
            else
                PlanDateTime += " PM";

            int iInterviewID = BLL_Crew_Interview.INS_CrewInterviewPlanning(iCrewID, int.Parse(ddlPlanRank.SelectedValue), txtPlanCrewName.Text, PlanDateTime, int.Parse(ddlPlanInterviewer.SelectedValue), txtPlanInterviewerPosition.Text, GetSessionUserID());
            hdnInterviewID.Value = iInterviewID.ToString();

            lblMessage.Text = "Interview planned for " + txtPlanCrewName.Text;
            txtInterviewDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            ddlRank.SelectedValue = ddlPlanRank.SelectedValue;

            string InterviewPlannedDate = "";
            string InterviewID = "";
            int iUserID = GetSessionUserID();
            string CrewName = objCrewBLL.Get_CrewPersonalDetailsByID(iCrewID, "staff_name");

            DataTable dt = BLL_Crew_Interview.Get_PlannedInterviewDetails(iUserID, iCrewID);
            if (dt.Rows.Count > 0)
            {
                InterviewID = dt.Rows[0]["ID"].ToString();
                InterviewPlannedDate = dt.Rows[0]["InterviewPlanDate"].ToString();
                hdnInterviewID.Value = InterviewID;

                ddlRank.SelectedValue = dt.Rows[0]["Rank"].ToString();
                ddlRank.Enabled = false;

                txtPersonInterviewed.Text = CrewName; //getQueryString("Name");
                txtPersonInterviewed.Enabled = false;
                txtInterviewDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));
                txtInterviewDate.Enabled = false;

                lblPlannedDate.Text = UDFLib.ConvertUserDateFormatTime(Convert.ToString(dt.Rows[0]["InterviewPlanDate"]));
                lblPlannedBy.Text = dt.Rows[0]["PlannedBy"].ToString();

                pnlInterviewPlanning.Visible = false;
                pnlEdit_InterviewResult.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }

    protected void btnCancelPlanning_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewList.aspx");
    }

    protected Boolean ValidateSaving()
    {
        string js = "";

        if (ddlRank.SelectedValue == "0")
        {
            js = "Select Rank";
        }
        else if (rdoSelected.SelectedIndex == -1)
        {
            js = "Please mention if the crew is Approved or Rejected";
        }
        else if (txtResultText.Text == "")
        {
            js = "Enter your comment for the crew";
        }

        if (rdoSelected.SelectedIndex == 0)
        {
            int Count = 0;
            foreach (ListItem li in lstVessels.Items)
            {
                if (li.Selected == true)
                {
                    Count++;
                }
            }
            if (Count == 0)
            {
                js = "Select recomended vessel(s).";
            }

            Count = 0;
            foreach (ListItem li in chkTradingArea.Items)
            {
                if (li.Selected == true)
                {
                    Count++;
                }
            }
            if (Count == 0)
            {
                js = "Select recomended trade zone(s).";
            }
        }
        if (js.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", "alert('" + js + "');", true);
            return false;
        }
        else
            return true;
    }

    protected void CalculateMarks(int InterviewID)
    {
        decimal UserMarks = 0;
        decimal FullMark = 0;
        decimal Avg = 0;
        string js = "";

        DataSet dsQA = BLL_Crew_Interview.Get_InterviewQuestionAnswers(InterviewID, GetSessionUserID());
        
        foreach (DataRow dr in dsQA.Tables[0].Rows)
        {            
            if (UDFLib.ConvertToDecimal(dr["NotApplicable"].ToString()) == 0)
            {
                FullMark += UDFLib.ConvertToDecimal(dr["Max"].ToString());
                UserMarks += UDFLib.ConvertToDecimal(dr["UserAnswer"].ToString());
                if(FullMark > 0)
                Avg = UserMarks / FullMark * 100;
            }
        }

        lblMaxMarks.Text = FullMark.ToString();
        lblUserMarks_P.Text = Avg.ToString("0.0");
        lblUserMarks.Text = UserMarks.ToString("0.0");

        if (FullMark > 0)
        {
            lblOutOf5.Text = (UserMarks / FullMark * 5).ToString("0.0");
            if ((UserMarks / FullMark * 5) > 2)
                js = "setDotColor('green');";
            else
                js = "setDotColor('red');";
        }
        else
        {
            lblOutOf5.Text = "0.0";
            js = "setDotColor('red');";
        }        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "setDotColor_", js, true);
    }

    protected void btnSaveInterviewResult_Click(object sender, EventArgs e)
    {
        string js = "";
        int QustionAnswered = 0;
        try
        {
            foreach (GridViewRow gvr in GridView_AssignedCriteria.Rows)
            {
                QustionAnswered = 0;
                string TextAnswer = "";
                CheckBox chk = (CheckBox)gvr.FindControl("chkNA");
                RadioButtonList rdoOptions = (RadioButtonList)gvr.FindControl("rdoOptions");
                int QID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[gvr.RowIndex].Value);

                int NotApplicable = -1;
                if (chk != null)
                {
                    NotApplicable = chk.Checked == true ? 1 : 0;
                    if (NotApplicable == 1)
                        QustionAnswered = 1;
                }
                string Remarks = ((TextBox)(gvr.FindControl("txtRemarks"))).Text;
                ((TextBox)(gvr.FindControl("txtRemarks"))).BackColor = System.Drawing.Color.White;
                TextBox txtAns = (TextBox)gvr.FindControl("txtAnswer");
                if (txtAns != null)
                {
                    TextAnswer = txtAns.Text;
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
                if (QustionAnswered == 0 && TextAnswer.Length == 0 && Remarks.Length == 0)
                {
                    js = "All interview questions are mandatory";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser3", "alert('" + js + "');", true);
                    break;
                }
                else
                    QustionAnswered = 1;
            }
            if (QustionAnswered == 1 && ValidateSaving() == true)
            {
                int iCrewID = GetCrewID();
                int iUserID = GetSessionUserID();
                int InterviewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
                int InterviewerID = GetSessionUserID();

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
                            NotApplicable = chk.Checked == true ? 1 : 0;

                        int SelectedOptionID = 0;
                        if (rdoOptions != null)
                        {
                            if(rdoOptions.SelectedIndex != -1)
                                SelectedOptionID = UDFLib.ConvertToInteger(rdoOptions.SelectedValue.Split(',')[0]);
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

                    int Ret = BLL_Crew_Interview.UPDATE_CrewInterviewResult(iCrewID, InterviewID, InterviewerID,UDFLib.ConvertToDefaultDt( txtInterviewDate.Text), int.Parse(ddlRank.SelectedValue), "", txtResultText.Text, rdoSelected.SelectedValue, "", iUserID,dt);

                    if (lstVessels.Items[0].Selected == true)
                    {
                        for (var i = 1; i < lstVessels.Items.Count; i++)
                        {
                            objCrewBLL.INS_Crew_RecomendedVessels(iCrewID, InterviewID, int.Parse(lstVessels.Items[i].Value), iUserID);
                        }
                    }
                    else
                    {
                        for (var i = 1; i < lstVessels.Items.Count; i++)
                        {
                            if (lstVessels.Items[i].Selected == true)
                            {
                                objCrewBLL.INS_Crew_RecomendedVessels(iCrewID, InterviewID, int.Parse(lstVessels.Items[i].Value), iUserID);
                            }
                        }
                    }

                    for (var j = 0; j < chkTradingArea.Items.Count; j++)
                    {
                        if (chkTradingArea.Items[j].Selected == true)
                        {
                            int ZoneID = UDFLib.ConvertToInteger(chkTradingArea.Items[j].Value);
                            objCrewBLL.INS_Crew_RecomendedZones(iCrewID, InterviewID, ZoneID, iUserID);
                        }
                    }
                    
                    js = "Interview result updated";
                    btnSaveInterviewResult.Enabled = false;
                    lnkEditSchedule.Visible = false;
                    UpdatePanel4.Update();
                }
                else
                    lblMessage.Text = "Please select the crew again to fill the interview result.";
            }
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

    protected void ddlPlanInterviewer_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iUserID = int.Parse(ddlPlanInterviewer.SelectedValue);
        txtPlanInterviewerPosition.Text = objCrewBLL.GetCurrentUserDesignation(iUserID);
    }

    protected void GridView_AssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int iQuestionID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "QID").ToString());
                int Grading_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
                //int iCrewID = GetCrewID();
                //int iUserID = GetSessionUserID();
                
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
                            Select_Option(rdo,SelectedOptionID);
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