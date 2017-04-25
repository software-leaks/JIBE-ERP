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

public partial class Crew_Interview : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdminBLL = new BLL_Crew_Admin();


    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);

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
                DataTable dt = objCrewBLL.getInterviewDetails(InterviewID);

                lnkEditSchedule.Attributes.Add("onclick", "EditInterviewSchedule('Interview.aspx'," + InterviewID.ToString() + "); return false;");


                if (dt.Rows.Count > 0)
                {
                    pnlInterviewPlanning.Visible = false;
                    pnlEdit_InterviewResult.Visible = true;

                    hdnInterviewID.Value = InterviewID.ToString();
                    hdnCrewID.Value = dt.Rows[0]["CrewID"].ToString();

                    int iCrewID = int.Parse(dt.Rows[0]["CrewID"].ToString());

                    InterviewPlannedDate = dt.Rows[0]["InterviewPlanDate"].ToString();
                    lnkOpenProfile.NavigateUrl = "CrewDetails.aspx?ID=" + iCrewID;
                    lblPlannedTimeZone.Text = dt.Rows[0]["DisplayName"].ToString();

                    //txtPosition.Text = objCrewBLL.GetCurrentUserDesignation(iUserID);
                    //txtPosition.Enabled = false;
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

                    if (dt.Rows[0]["rank"].ToString() != "")
                    {
                        ddlRank.SelectedValue = dt.Rows[0]["rank"].ToString();
                    }
                    ddlRank.Enabled = false;
                    txtStaffCode.Text = dt.Rows[0]["staff_code"].ToString();

                    if (dt.Rows[0]["Interviewer"].ToString().Trim() != "")
                    {
                        ddlUserList.Items.Clear();
                        ddlUserList.Items.Add(new ListItem(dt.Rows[0]["Interviewer"].ToString(), dt.Rows[0]["InterviewerID"].ToString()));
                        ddlUserList.SelectedIndex = 0;

                    }
                    else
                    {
                        ddlUserList.DataBind();
                        ddlUserList.SelectedValue = GetSessionUserID().ToString();
                    }
                    ddlUserList.Enabled = false;

                    txtPlanInterviewerPosition.Text = dt.Rows[0]["Designation"].ToString();

                    lblPlannedInterviewer.Text = dt.Rows[0]["PlannedInterviewer"].ToString();
                    lblPlannedDate.Text = UDFLib.ConvertUserDateFormatTime(Convert.ToString(dt.Rows[0]["InterviewPlanDate"]));
                    lblPlannedBy.Text = dt.Rows[0]["PlannedBy"].ToString();

                    rdoSelected.SelectedValue = dt.Rows[0]["Result"].ToString();
                    txtResultText.Text = dt.Rows[0]["ResultText"].ToString();


                    SqlDataReader drRes = objCrewBLL.Get_UserAnswers(iCrewID, InterviewID);
                    DataSet ds = objCrewBLL.Get_InterviewerRecomendations(iCrewID, InterviewID);

                    //lstVessels.Enabled = false;


                    if (drRes.HasRows == true)
                    {
                        int QuestionID = 0;
                        string UserAnswar = "0";
                        string UserRemark = "";


                        //pnlEdit_InterviewResult.Enabled = false;
                        btnSaveInterviewResult.Enabled = false;
                        lnkEditSchedule.Visible = false;

                        while (drRes.Read())
                        {
                            QuestionID = int.Parse(drRes["QuestionID"].ToString());
                            UserAnswar = drRes["UserAnswer"].ToString();
                            UserRemark = drRes["UserRemark"].ToString();

                            switch (QuestionID)
                            {
                                case 1:
                                    rdoEnglishRead.SelectedValue = UserAnswar;
                                    break;
                                case 2:
                                    rdoEnglishWrite.SelectedValue = UserAnswar;
                                    break;
                                case 3:
                                    rdoEnglishComm.SelectedValue = UserAnswar;
                                    break;
                                case 4:
                                    rdo2.SelectedValue = UserAnswar;
                                    txtRemark2.Text = UserRemark;
                                    break;
                                case 5:
                                    rdo3.SelectedValue = UserAnswar;
                                    txtRemark3.Text = UserRemark;
                                    break;
                                case 6:
                                    rdo4.SelectedValue = UserAnswar;
                                    txtRemark4.Text = UserRemark;
                                    break;
                                case 7:
                                    rdo5.SelectedValue = UserAnswar;
                                    txtRemark5.Text = UserRemark;
                                    break;
                                case 8:
                                    rdo6.SelectedValue = UserAnswar;
                                    txtRemark6.Text = UserRemark;
                                    break;
                                case 9:
                                    rdo7.SelectedValue = UserAnswar;
                                    txtRemark7.Text = UserRemark;
                                    break;
                                case 10:
                                    rdo8.SelectedValue = UserAnswar;
                                    txtRemark8.Text = UserRemark;
                                    break;
                                case 11:
                                    rdo9.SelectedValue = UserAnswar;
                                    txtRemark9.Text = UserRemark;
                                    break;
                                case 12:
                                    rdo10.SelectedValue = UserAnswar;
                                    txtRemark10.Text = UserRemark;
                                    break;
                                case 13:
                                    rdo11.SelectedValue = UserAnswar;
                                    txtRemark11.Text = UserRemark;
                                    break;
                                case 14:
                                    rdo12.SelectedValue = UserAnswar;
                                    txtRemark12.Text = UserRemark;
                                    break;

                            }

                            
                        }
                        CalculateMarks(null, null);

                        chkTradingArea.DataBind();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            for (var i = 1; i < lstVessels.Items.Count; i++)
                            {
                                if (lstVessels.Items[i].Value == dr["vesselid"].ToString())
                                {
                                    lstVessels.Items[i].Selected = true;
                                }
                            }
                        }
                        foreach (DataRow dr in ds.Tables[1].Rows)
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
                    else
                    {
                        //pnlEdit_InterviewResult.Enabled = true;
                        btnSaveInterviewResult.Enabled = true;
                        lnkEditSchedule.Visible = true;
                    }
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

        HighLight_MandatoryQ();
    }

    public void Load_VesselList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int Fleet_ID = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        lstVessels.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        lstVessels.DataTextField = "VESSEL_NAME";
        lstVessels.DataValueField = "VESSEL_ID";
        lstVessels.DataBind();
        lstVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        //lstVessels.SelectedIndex = 0;
    }

    protected void Load_RankList()
    {
        DataTable dt = objCrewAdminBLL.Get_RankList();
        ddlRank.DataSource = dt;
        ddlRank.DataBind();

        ddlPlanRank.DataSource = dt;
        ddlPlanRank.DataBind();

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
    public int GetInterviewID()
    {
        return UDFLib.ConvertToInteger(Request.QueryString["ID"]);
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


    //protected void Load_InterviewResult(int CrewID, int InterviewID)
    //{

    //    SqlDataReader drRes = objCrewBLL.Get_UserAnswers(CrewID, InterviewID);
    //    while (drRes.Read())
    //    {
    //        switch (drRes["QuestionID"].ToString())
    //        {
    //            case "1":
    //                rdoEnglishRead.SelectedValue = drRes["UserAnswer"].ToString();
    //                break;
    //            case "2":
    //                rdoEnglishWrite.SelectedValue = drRes["UserAnswer"].ToString();
    //                break;
    //            case "3":
    //                rdoEnglishComm.SelectedValue = drRes["UserAnswer"].ToString();
    //                break;
    //            case "4":
    //                rdo2.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "5":
    //                rdo3.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "6":
    //                rdo4.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "7":
    //                rdo5.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "8":
    //                rdo6.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "9":
    //                rdo7.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "10":
    //                rdo8.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "11":
    //                rdo9.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "12":
    //                rdo10.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "13":
    //                rdo11.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //            case "14":
    //                rdo12.SelectedValue = drRes["UserAnswer"].ToString();
    //                txtRemark2.Text = drRes["UserRemark"].ToString();
    //                break;
    //        }
    //    }


    //}

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

            int iInterviewID = objCrewBLL.INS_CrewInterviewPlanning(iCrewID, int.Parse(ddlPlanRank.SelectedValue), txtPlanCrewName.Text, PlanDateTime, int.Parse(ddlPlanInterviewer.SelectedValue), "", GetSessionUserID());
            hdnInterviewID.Value = iInterviewID.ToString();

            lblMessage.Text = "Interview planned for " + txtPlanCrewName.Text;
            txtInterviewDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));
            //ddlRank.SelectedValue = ddlPlanRank.SelectedValue;

            string InterviewPlannedDate = "";
            string InterviewID = "";
            int iUserID = GetSessionUserID();
            string CrewName = objCrewBLL.Get_CrewPersonalDetailsByID(iCrewID, "staff_name");

            DataTable dt = objCrewBLL.Get_PlannedInterviewDetails(iUserID, iCrewID);
            if (dt.Rows.Count > 0)
            {
                InterviewID = dt.Rows[0]["ID"].ToString();
                InterviewPlannedDate = dt.Rows[0]["InterviewPlanDate"].ToString();
                hdnInterviewID.Value = InterviewID;

                //ddlRank.SelectedValue = dt.Rows[0]["Rank"].ToString();
                //ddlRank.Enabled = false;

                //txtPosition.Text = objCrewBLL.GetCurrentUserDesignation(iUserID);
                //txtPosition.Enabled = false;
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

    protected void btnSaveInterviewResult_Click(object sender, EventArgs e)
    {
        string js = "";

        try
        {
            if (ValidateSaving() == true)
            {
                int iCrewID = GetCrewID();
                int iUserID = GetSessionUserID();
                int iInterviewID = int.Parse(hdnInterviewID.Value);
                int InterviewerID = GetSessionUserID();

                if (iInterviewID > 0)
                {
                    objCrewBLL.UPDATE_CrewInterviewResult(iCrewID, iInterviewID, InterviewerID,UDFLib.ConvertToDefaultDt( txtInterviewDate.Text), int.Parse(ddlRank.SelectedValue), "", txtResultText.Text, rdoSelected.SelectedValue, "", iUserID);
                    try
                    {
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 1, int.Parse(rdoEnglishRead.SelectedValue), "", iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 2, int.Parse(rdoEnglishWrite.SelectedValue), "", iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 3, int.Parse(rdoEnglishComm.SelectedValue), "", iUserID);

                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 4, int.Parse(rdo2.SelectedValue), txtRemark2.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 5, int.Parse(rdo3.SelectedValue), txtRemark3.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 6, int.Parse(rdo4.SelectedValue), txtRemark4.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 7, int.Parse(rdo5.SelectedValue), txtRemark5.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 8, int.Parse(rdo6.SelectedValue), txtRemark6.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 9, int.Parse(rdo7.SelectedValue), txtRemark7.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 10, int.Parse(rdo8.SelectedValue), txtRemark8.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 11, int.Parse(rdo9.SelectedValue), txtRemark9.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 12, int.Parse(rdo10.SelectedValue), txtRemark10.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 13, int.Parse(rdo11.SelectedValue), txtRemark11.Text, iUserID);
                        objCrewBLL.INS_CrewInterviewAnswer(iCrewID, iInterviewID, 14, int.Parse(rdo12.SelectedValue), txtRemark12.Text, iUserID);

                        if (lstVessels.Items[0].Selected == true)
                        {
                            for (var i = 1; i < lstVessels.Items.Count; i++)
                            {
                                objCrewBLL.INS_Crew_RecomendedVessels(iCrewID, iInterviewID, int.Parse(lstVessels.Items[i].Value), iUserID);
                            }
                        }
                        else
                        {
                            for (var i = 1; i < lstVessels.Items.Count; i++)
                            {
                                if (lstVessels.Items[i].Selected == true)
                                {
                                    objCrewBLL.INS_Crew_RecomendedVessels(iCrewID, iInterviewID, int.Parse(lstVessels.Items[i].Value), iUserID);
                                }
                            }
                        }

                        for (var j = 0; j < chkTradingArea.Items.Count; j++)
                        {
                            if (chkTradingArea.Items[j].Selected == true)
                            {
                                int ZoneID = UDFLib.ConvertToInteger(chkTradingArea.Items[j].Value);
                                objCrewBLL.INS_Crew_RecomendedZones(iCrewID, iInterviewID, ZoneID, iUserID);
                            }
                        }


                        //if (UDFLib.ConvertToInteger(Session["USERCOMPANYID"]) != 1)
                        //{
                        //    if (int.Parse(rdoSelected.SelectedValue) == 1)
                        //        SendMail_RecomendationByManningOffice();
                        //}

                        btnSaveInterviewResult.Enabled = false;
                        lnkEditSchedule.Enabled = false;
                        js = "Interview result updated.";
                        lblMessage.Text = "Interview result updated.";
                    }
                    catch
                    {
                        objCrewBLL.DEL_CrewInterviewAnswers(iCrewID, iInterviewID, GetSessionUserID());
                        js = "Interview result NOT SAVED.";
                    }

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('" + js + "');", true);

        }
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("CrewList.aspx");
    //}

    protected void ddlPlanInterviewer_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iUserID = int.Parse(ddlPlanInterviewer.SelectedValue);
        txtPlanInterviewerPosition.Text = objCrewBLL.GetCurrentUserDesignation(iUserID);
    }

    protected void ddlUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iUserID = int.Parse(ddlUserList.SelectedValue);
        //txtPosition.Text = objCrewBLL.GetCurrentUserDesignation(iUserID);
        //txtPosition.Enabled = false;
    }

    protected void CalculateMarks(object sender, EventArgs e)
    {

        int FullMark = GetFullMark();
        double Marks = TotalMarks();
        double Avg = Marks / FullMark;

        lblTotalMarks.Text = Avg.ToString("0.0");

        string js = "";
        if (Avg > 2)
            js = "setDotColor('green');";
        else
            js = "setDotColor('red');";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
    }

    private int GetFullMark()
    {
        int FullMark = 14;

        if (rdo2.SelectedValue == "0")
            FullMark -= 1;

        if (rdo3.SelectedValue == "0")
            FullMark -= 1;

        if (rdo4.SelectedValue == "0")
            FullMark -= 1;

        if (rdo5.SelectedValue == "0")
            FullMark -= 1;

        if (rdo6.SelectedValue == "0")
            FullMark -= 1;

        if (rdo7.SelectedValue == "0")
            FullMark -= 1;

        if (rdo8.SelectedValue == "0")
            FullMark -= 1;

        if (rdo9.SelectedValue == "0")
            FullMark -= 1;

        if (rdo10.SelectedValue == "0")
            FullMark -= 1;

        if (rdo11.SelectedValue == "0")
            FullMark -= 1;

        if (rdo12.SelectedValue == "0")
            FullMark -= 1;

        return FullMark;

    }

    protected double TotalMarks()
    {
        double Marks = 0;
        if (rdoEnglishRead.SelectedValue == "1")
            Marks += 5;

        if (rdoEnglishWrite.SelectedValue == "1")
            Marks += 5;

        if (rdoEnglishComm.SelectedValue == "1")
            Marks += 5;

        for (int i = 2; i < 13; i++)
        {
            RadioButtonList obj = (RadioButtonList)pnlEdit_InterviewResult.FindControl("rdo" + i.ToString());
            if (obj != null)
            {
                switch (obj.SelectedValue)
                {
                    case "1":
                        Marks += 5;
                        break;
                    case "2":
                        Marks += 3;
                        break;
                    case "3":
                        Marks += 2;
                        break;
                    case "4":
                        Marks += 0;
                        break;
                }

                if (obj.SelectedValue == "4")
                {
                    TextBox objTxt = (TextBox)pnlEdit_InterviewResult.FindControl("txtRemark" + i.ToString());
                    if (objTxt != null)
                    {
                        objTxt.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else
                {
                    TextBox objTxt = (TextBox)pnlEdit_InterviewResult.FindControl("txtRemark" + i.ToString());
                    if (objTxt != null)
                    {
                        objTxt.BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }
        return Marks;
    }

    protected Boolean ValidateSaving()
    {
        string js = "";
        int CurrentUserID = GetSessionUserID();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        DataTable dt = objUser.Get_UserDetails(CurrentUserID);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Dep_Code"].ToString() == "5")//Technical
            {
                if (rdo7.SelectedValue == "0")
                    js = "Question NO: 7 need to be answered ";
                if (rdo10.SelectedValue == "0")
                    js = "Question NO: 10 need to be answered ";
                if (rdo11.SelectedValue == "0")
                    js = "Question NO: 11 need to be answered ";
                if (rdo12.SelectedValue == "0")
                    js = "Question NO: 12 need to be answered ";
                
            }
            else if (dt.Rows[0]["Dep_Code"].ToString() == "4")//Operations
            {
                if (rdo4.SelectedValue == "0")
                    js = "Question NO: 4 need to be answered ";
                if (rdo5.SelectedValue == "0")
                    js = "Question NO: 5 need to be answered ";
                if (rdo6.SelectedValue == "0")
                    js = "Question NO: 6 need to be answered ";
                if (rdo8.SelectedValue == "0")
                    js = "Question NO: 8 need to be answered ";
                if (rdo9.SelectedValue == "0")
                    js = "Question NO: 9 need to be answered ";
                if (rdo12.SelectedValue == "0")
                    js = "Question NO: 12 need to be answered ";

            }
            else if (dt.Rows[0]["Dep_Code"].ToString() == "9")//Chartering
            {
                if (rdo8.SelectedValue == "0")
                    js = "Question NO: 8 need to be answered ";
                if (rdo9.SelectedValue == "0")
                    js = "Question NO: 9 need to be answered ";
                
            }
            else if (dt.Rows[0]["Dep_Code"].ToString() == "7")//SQA
            {
                if (rdo4.SelectedValue == "0")
                    js = "Question NO: 4 need to be answered ";
                if (rdo5.SelectedValue == "0")
                    js = "Question NO: 5 need to be answered ";
            }
        }



        if (ddlRank.SelectedValue == "0")
        {
            js = "Select Rank";

        }
        else if (rdoEnglishRead.SelectedValue == "" || rdoEnglishWrite.SelectedValue == "" || rdoEnglishComm.SelectedValue == "")
        {
            js = "All options related to Question NO: 1 need to be answered ";

        }
        else if (rdo2.SelectedValue == "0")
        {
            js = "Question NO: 2 need to be answered ";

        }
        else if (rdo3.SelectedValue == "0")
        {
            js = "Question NO: 3 need to be answered ";

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

        if (rdo2.SelectedValue == "4" && txtRemark2.Text == "")
            js = "Enter remarks for Question NO: 2";
        else if (rdo3.SelectedValue == "4" && txtRemark3.Text == "")
            js = "Enter remarks for Question NO: 3";
        else if (rdo4.SelectedValue == "4" && txtRemark4.Text == "")
            js = "Enter remarks for Question NO: 4";
        else if (rdo5.SelectedValue == "4" && txtRemark5.Text == "")
            js = "Enter remarks for Question NO: 5";
        else if (rdo6.SelectedValue == "4" && txtRemark6.Text == "")
            js = "Enter remarks for Question NO: 6";
        else if (rdo7.SelectedValue == "4" && txtRemark7.Text == "")
            js = "Enter remarks for Question NO: 7";
        else if (rdo8.SelectedValue == "4" && txtRemark8.Text == "")
            js = "Enter remarks for Question NO: 8";
        else if (rdo9.SelectedValue == "4" && txtRemark9.Text == "")
            js = "Enter remarks for Question NO: 9";
        else if (rdo10.SelectedValue == "4" && txtRemark10.Text == "")
            js = "Enter remarks for Question NO: 10";
        else if (rdo11.SelectedValue == "4" && txtRemark11.Text == "")
            js = "Enter remarks for Question NO: 11";
        else if (rdo12.SelectedValue == "4" && txtRemark12.Text == "")
            js = "Enter remarks for Question NO: 12";

        if (js.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('" + js + "');", true);
            return false;
        }
        else
            return true;

    }

    protected void HighLight_MandatoryQ()
    {
        
        int CurrentUserID = GetSessionUserID();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        DataTable dt = objUser.Get_UserDetails(CurrentUserID);
        if (dt.Rows.Count > 0)
        {

            rdo2.CssClass = "highlight";
            rdo3.CssClass = "highlight";

            if (dt.Rows[0]["Dep_Code"].ToString() == "5")//Technical
            {
                    rdo7.CssClass = "highlight";
                    rdo10.CssClass = "highlight";
                    rdo11.CssClass = "highlight";
                    rdo12.CssClass = "highlight";

            }
            else if (dt.Rows[0]["Dep_Code"].ToString() == "4")//Operations
            {
                    rdo4.CssClass = "highlight";
                    rdo5.CssClass = "highlight";
                    rdo6.CssClass = "highlight";
                    rdo8.CssClass = "highlight";
                    rdo9.CssClass = "highlight";
                    rdo12.CssClass = "highlight";

            }
            else if (dt.Rows[0]["Dep_Code"].ToString() == "9")//Chartering
            {
                    rdo8.CssClass = "highlight";
                    rdo9.CssClass = "highlight";

            }
            else if (dt.Rows[0]["Dep_Code"].ToString() == "7")//SQA
            {
                    rdo4.CssClass = "highlight";
                    rdo5.CssClass = "highlight";
            }
        }
    }

    //protected void SendMail_RecomendationByManningOffice()
    //{
    //    string ManningOffice = "";
    //    string Staff_Name = "";
    //    string Rank = "";
    //    string ReadyToJoin = "";
    //    int ManningOfficeID = 0;

    //    DataTable dt = objCrewBLL.Get_CrewPersonalDetailsByID(GetCrewID());
    //    if (dt.Rows.Count > 0)
    //    {
    //        ManningOfficeID = int.Parse(dt.Rows[0]["ManningOfficeID"].ToString());
    //        ManningOffice = dt.Rows[0]["Company_Name"].ToString();
    //        Staff_Name = dt.Rows[0]["Staff_Name"].ToString();
    //        Rank = dt.Rows[0]["rank_applied_name"].ToString();
    //        ReadyToJoin = dt.Rows[0]["available_from_date"].ToString();
    //    }

    //    string msgTo = "crew@unimarships.com ";
    //    string msgCC = "";
    //    string msgBCC = "";

    //    string msgSubject = "New Crew Recomendation from: " + ManningOffice;

    //    string msgBody = " ----   System Notification: Please do not reply to this mail.   ----";
    //    msgBody += "<br><br>";

    //    msgBody += "Staff Name:   " + Staff_Name + "<br>";
    //    msgBody += "Rank applied for: " + Rank + "<br>";
    //    msgBody += "Readiness: " + ReadyToJoin + "<br> <br><br>";

    //    string querystring = UDFLib.Encrypt("id=" + getQueryString("ID"));

    //    msgBody += "<a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/crew/crewdetails.aspx" + querystring + "'>--- Click to view the crew details ---</a><br>";

      //  objCrewBLL.Send_CrewNotification(GetCrewID(), ManningOfficeID, 0, 1, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "READY");

    //}

    
}