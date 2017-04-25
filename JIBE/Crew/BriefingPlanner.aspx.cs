using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Data;

public partial class Crew_BriefingPlanner : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    UserAccess objUA = new UserAccess();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    public string TodayDateFormat = "";
    BLL_Infra_TimeZones objTimeZone = new BLL_Infra_TimeZones();
    int InterviewID = 0;
   public  string DFormat = "";
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
        lblMessage.Text = "";
        TodayDateFormat = UDFLib.DateFormatMessage();
        DFormat =CalendarExtender1.Format = UDFLib.GetDateFormat();

        if (!IsPostBack)
        {
            int CrewID = GetCrewID();
            int CurrentUserID = GetSessionUserID();
            Session["CrewID"] = CrewID;
            hdnUserID.Value = CurrentUserID.ToString();
            hdnCrewID.Value = CrewID.ToString();
            if (CrewID > 0)
            {
                int CurrentRankId = 0;

                DataTable dtPersonalDetail = objCrewBLL.Get_CrewPersonalDetailsByID1(CrewID);
                if (dtPersonalDetail.Rows.Count > 0)
                {
                    CurrentRankId = int.Parse(dtPersonalDetail.Rows[0]["CurrentRankID"].ToString());
                    txtPlanCrewName.Text = dtPersonalDetail.Rows[0]["Staff_FullName"].ToString();
                }

                ddlInterviewSheet.DataSource = objCrewAdmin.Get_InterviewSheets(CurrentRankId, "Briefing");
                ddlInterviewSheet.DataBind();

                ddlPlanRank.DataSource = objCrewAdmin.Get_RankList();
                ddlPlanRank.DataBind();
                ddlPlanRank.Enabled = false;

                DataTable dt = objTimeZone.Get_TimeZoneList();
                ddlTimeZone.DataSource = dt;
                ddlTimeZone.DataTextField = "DisplayName";
                ddlTimeZone.DataValueField = "TimeZone";
                ddlTimeZone.DataBind();
                try
                {
                    DataRow[] dr = dt.Select("DefaultTimeZone=1");
                    if (dr.Length > 0)
                    {
                        ddlTimeZone.SelectedValue = dr[0]["TimeZone"].ToString();
                    }
                }
                catch { }

                ddlPlanRank.SelectedValue = CurrentRankId.ToString();
                int IsMandatoryDocumentsUploaded = objCrewBLL.IsMandatoryDocumentsUploaded(CrewID);

                if (IsMandatoryDocumentsUploaded == 0)
                {
                    lblMessage.Text = "Briefing can not be planned for the crew. The necessery documents are not yet uploaded!!";
                    return;
                }

                GetVoyageCount(CrewID);
            }

        }



        UserAccessValidation();
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMessage.Text = "You don't have sufficient previlege to access the requested information.";
            dvPageContent.Visible = false;
        }
        if (objUA.Add == 0)
        {
            // lnkEditInterviewPlanning.Visible = false;
            //GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
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

    public int GetCrewID()
    {
        try
        {
            if (getQueryString("ID") != "")
            {
                return int.Parse(getQueryString("ID"));
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


    public void PlanBriefing()
    {
        try
        {
            int CrewID = int.Parse(hdnCrewID.Value);

            int CurrentRankId = 1;// int.Parse(objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "CurrentRankID"));

            ddlInterviewSheet.DataSource = objCrewAdmin.Get_InterviewSheets(CurrentRankId, "Briefing");
            ddlInterviewSheet.DataBind();

            ddlPlanRank.DataSource = objCrewAdmin.Get_RankList();
            ddlPlanRank.DataBind();
            ddlPlanRank.Enabled = false;

            DataTable dt = objTimeZone.Get_TimeZoneList();
            ddlTimeZone.DataSource = dt;
            ddlTimeZone.DataTextField = "DisplayName";
            ddlTimeZone.DataValueField = "TimeZone";
            ddlTimeZone.DataBind();
            try
            {
                DataRow[] dr = dt.Select("DefaultTimeZone=1");
                if (dr.Length > 0)
                {
                    ddlTimeZone.SelectedValue = dr[0]["TimeZone"].ToString();
                }
            }
            catch { }

            ddlPlanRank.SelectedValue = CurrentRankId.ToString();
            int IsMandatoryDocumentsUploaded = objCrewBLL.IsMandatoryDocumentsUploaded(CrewID);

            if (IsMandatoryDocumentsUploaded == 0)
            {
                lblMessage.Text = "Briefing can not be planned for the crew. The necessery documents are not yet uploaded!!";
                return;
            }

        }
        catch { }
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


    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }


    protected void gvCrewBriefPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string InterviewDate = DataBinder.Eval(e.Row.DataItem, "InterviewDate").ToString();

            if (InterviewDate != "")
            {
                ImageButton imgBtnEdit = (ImageButton)(e.Row.FindControl("imgBtnEdit"));
                if (imgBtnEdit != null)
                    imgBtnEdit.Visible = false;

                ImageButton imgBtnDelete = (ImageButton)(e.Row.FindControl("imgBtnDelete"));
                if (imgBtnDelete != null)
                    imgBtnDelete.Visible = false;
            }

            TextBox txt = (TextBox)e.Row.FindControl("txtPlanDate");

            DataRow dr = ((DataRowView)e.Row.DataItem).Row;         

            if (dr["InterviewPlanDate"].ToString() != "" &&  txt != null)
            {
                txt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["InterviewPlanDate"].ToString()));
            }
           

        }
    }
    protected void GetVoyageCount(int CrewID)
    {
        int Count = objCrewBLL.CRW_Get_CrewVoyageCountByCrewID(CrewID);

        if (Count <= 0)
        {
            btnSavePlanning.Enabled = false;
            ddlPlanH.Enabled = false;
            ddlPlanInterviewer.Enabled = false;
            ddlPlanM.Enabled = false;
            ddlPlanRank.Enabled = false;
            ddlTimeZone.Enabled = false;
            txtPlanCrewName.Enabled = false;
            txtPlanDate.Enabled = false;
            lblMessage.Text = "Briefing cann't be planned because voyage is not created for crew";
            return;

        }
    }
    protected void ddlPlanInterviewer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSavePlanning_Click(object sender, EventArgs e)
    {
        try
        {
            int iCrewID = int.Parse(hdnCrewID.Value);
            if (iCrewID == 0)
            {
                lblMessage.Text = "Please select a crew from the above list to plan an Briefing";
                return;
            }
            if (int.Parse(ddlPlanInterviewer.SelectedValue) == 0)
            {
                lblMessage.Text = "Please Select Briefer";
                return;
            }
            if (txtPlanDate.Text.Trim().Equals(""))
            {
                lblMessage.Text = "Please Enter Date"+TodayDateFormat;
                return;
            }
            if (int.Parse(ddlInterviewSheet.SelectedValue) == 0)
            {
                lblMessage.Text = "Please Select Briefing Sheet";
                return;
            }
            string PlanDateTime = UDFLib.ConvertToDate(txtPlanDate.Text,UDFLib.GetDateFormat()).ToShortDateString() + " " + ddlPlanH.Text + ":" + ddlPlanM.Text;
            if (int.Parse(ddlPlanH.Text) < 12)
                PlanDateTime += " AM";
            else
                PlanDateTime += " PM";

            string TimeZone = ddlTimeZone.SelectedValue;
            int iInterviewID = objCrewBLL.INS_CrewInterviewPlanning(iCrewID, 0, txtPlanCrewName.Text, PlanDateTime, int.Parse(ddlPlanInterviewer.SelectedValue), "", GetSessionUserID(), TimeZone, "Briefing", int.Parse(ddlInterviewSheet.SelectedValue));
            gvCrewBriefPlan.DataBind();
            hdnInterviewID.Value = iInterviewID.ToString();
            if (iInterviewID > 0)
            {
                lblMessage.Text = "Briefing planned for " + txtPlanCrewName.Text;
                SendMail_InterviewPlanning(iInterviewID);
                string js = "parent.GetInterviewResult(" + iCrewID + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                lblMessage.Text = "Briefing already planned for " + txtPlanCrewName.Text + " at same time " + PlanDateTime;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }
    protected void btnCancelPlanning_Click(object sender, EventArgs e)
    {
        int iCrewID = int.Parse(hdnCrewID.Value);
        //PlanBriefing();
        txtPlanDate.Text = "";
        lblMessage.Text = "";
        ddlPlanH.SelectedValue = "12";
        ddlPlanM.SelectedValue = "00";
        ddlPlanInterviewer.SelectedValue = "0";
        string js = "parent.GetInterviewResult(" + iCrewID + ",'Briefing');parent.hideModal('dvPlan');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }

    protected void SendMail_InterviewPlanning(int iInterviewID)
    {
        string ManningOfficeID = "0";
        string ManningOffice = "";
        string ManningOfficeMailID = "";
        string Staff_Name = "";
        string Rank = "";
        string ReadyToJoin = "";

        string Interviewer = "";
        string PlanDate = "";
        string PlanInterviewerMailID = "";
        string TimeZone = "";

        try
        {

            DataTable dt = objCrewBLL.Get_CrewPersonalDetailsByID(GetCrewID());
            if (dt.Rows.Count > 0)
            {
                ManningOfficeID = dt.Rows[0]["ManningOfficeID"].ToString();
                ManningOffice = dt.Rows[0]["Company_Name"].ToString();
                ManningOfficeMailID = dt.Rows[0]["ManningOfficeMailID"].ToString();

                Staff_Name = dt.Rows[0]["Staff_FullName"].ToString();
                Rank = dt.Rows[0]["rank_applied_name"].ToString();
                ReadyToJoin = dt.Rows[0]["available_from_date"].ToString();
            }

            DataTable dtInt = objCrewBLL.getInterviewDetails(iInterviewID);
            if (dtInt.Rows.Count > 0)
            {
                Interviewer = dtInt.Rows[0]["PlannedInterviewer"].ToString();
                PlanDate = dtInt.Rows[0]["InterviewPlanDate"].ToString();
                PlanInterviewerMailID = dtInt.Rows[0]["PlanInterviewerMailID"].ToString();
                TimeZone = dtInt.Rows[0]["TimeZone"].ToString();

            }
            string msgTo = PlanInterviewerMailID;
            string msgCC = "";

            string msgSubject = "Crew Briefing Planned for " + Interviewer + " at " + PlanDate + " " + TimeZone;

            string msgBody = @"Crew Briefing Planned for " + Interviewer + " at " + PlanDate + " " + TimeZone + @"

            Staff Name:   " + Staff_Name + @"
            Rank applied for: " + Rank + @"
            Manning Agent: " + ManningOffice + @"
            Readiness: " + ReadyToJoin + @" .

            Please ensure necessary arrangements are made according to this schedule,and the sea-farer is informed and available for the above Briefing.";


            objCrewBLL.Send_CrewNotification(GetCrewID(), int.Parse(ManningOfficeID), 0, 2, msgTo, msgCC, "", msgSubject, msgBody, "", "MEETING", PlanDate, GetSessionUserID(), "READY", TimeZone);

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }


    }

    protected void ObjectDataSource2_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["InterviewPlanDate"] = UDFLib.ConvertToDate(e.InputParameters["InterviewPlanDate"].ToString()).ToShortDateString();
        InterviewID = int.Parse(e.InputParameters["Id"].ToString());
    }

    protected void ObjectDataSource2_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (Convert.ToInt32(e.ReturnValue) > 0)
        {
            int iCrewID = int.Parse(hdnCrewID.Value);
            if (InterviewID > 0)
            {
                SendMail_InterviewPlanning(InterviewID);
                string js = "parent.GetInterviewResult(" + iCrewID + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
        }
    }

   
}