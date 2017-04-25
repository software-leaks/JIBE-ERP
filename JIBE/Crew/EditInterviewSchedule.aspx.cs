using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class Crew_EditInterviewSchedule : System.Web.UI.Page
{
    public string DateFormatMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormatMessage = UDFLib.DateFormatMessage();
        if (!IsPostBack)
        {           
            int InterviewID = UDFLib.ConvertToInteger(Request.QueryString["InterviewID"]);
            Load_InterviewToEdit(InterviewID);
            string ParentPage = Request.QueryString["ParentPage"];
            if (ParentPage != null && ParentPage.Equals("Interview.aspx"))
            {
                (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).Visible = false;
                (frmInterviewDetails.FindControl("lblInterviewSheet") as Label).Visible = false;
                (frmInterviewDetails.FindControl("txtPlanDate") as TextBox).Text = UDFLib.ConvertUserDateFormat(Convert.ToString((frmInterviewDetails.FindControl("txtPlanDate") as TextBox).Text));
                (frmInterviewDetails.FindControl("CalendarExtender1") as AjaxControlToolkit.CalendarExtender).Format = UDFLib.GetDateFormat(); 
            }

            AjaxControlToolkit.CalendarExtender cal = new AjaxControlToolkit.CalendarExtender();
            cal = frmInterviewDetails.FindControl("CalendarExtender1") as AjaxControlToolkit.CalendarExtender;
            if (cal != null)
            {
                cal.Format = UDFLib.GetDateFormat();
            }
        }
    }

    protected void Load_InterviewToEdit(int InterviewID)
    {
        DataTable dt = BLL_Crew_Interview.getInterviewDetails(InterviewID);

        try
        {
            frmInterviewDetails.DefaultMode = FormViewMode.Edit;
            frmInterviewDetails.DataSource = dt;
            frmInterviewDetails.DataBind();

            if (dt.Rows.Count > 0)
            {
                (frmInterviewDetails.FindControl("ddlPlanM") as DropDownList).SelectedValue = UDFLib.ConvertToInteger(dt.Rows[0]["InterviewPlanM"]).ToString();
                (frmInterviewDetails.FindControl("ddlPlanInterviewer") as DropDownList).SelectedValue = UDFLib.ConvertToInteger(dt.Rows[0]["PlannedInterviewerID"]).ToString();

                LoadInterviewSheet(int.Parse((dt.Rows[0]["RankID"]).ToString()));
                (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).SelectedValue = UDFLib.ConvertToInteger(dt.Rows[0]["IQID"]).ToString();

                BLL_Infra_TimeZones objTimeZone = new BLL_Infra_TimeZones();
                DataTable dt2 = objTimeZone.Get_TimeZoneList();
                (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).DataSource = dt2;
                (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).DataTextField = "DisplayName";
                (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).DataValueField = "TimeZone";
                (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).DataBind();

                if (Convert.ToString(dt.Rows[0]["TimeZone"]) != "")
                {
                    (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).SelectedValue = Convert.ToString(dt.Rows[0]["TimeZone"]);
                }
                else
                {
                    DataRow[] dr = dt2.Select("DefaultTimeZone=1");
                    if (dr.Length > 0)
                    {
                        (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).SelectedValue = dr[0]["TimeZone"].ToString();
                    }
                }
            }
        }
        catch { }    
    }

    protected void ddlInterviewRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iRankID = int.Parse((frmInterviewDetails.FindControl("ddlInterviewRank") as DropDownList).SelectedValue);
        LoadInterviewSheet(iRankID);
    }

    protected void LoadInterviewSheet(int RankId)
    {
        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).Items.Clear();
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_InterviewSheets(RankId, "Interview");

        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).DataSource = dt;
        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).DataTextField = "INTERVIEW_NAME";
        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).DataValueField = "ID";
        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).DataBind();

        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).Items.Insert(0, new ListItem("-SELECT-", "0"));
        (frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).SelectedIndex = 0;
    }

    protected void frmInterviewDetails_Updating(object sender, FormViewUpdateEventArgs e)        
    {
        try
        {
            string js = "";
            string InterviewDate = (frmInterviewDetails.FindControl("txtPlanDate") as TextBox).Text.ToString();
            if (InterviewDate == "")
            {
                js = "alert('Interview Date is mandatory');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
            else
            {
                if (!UDFLib.DateCheck(InterviewDate))
                {
                    string js3 = "alert('Enter valid Interview Date" + DateFormatMessage + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "searchError", js3, true);
                    return;
                }
            }            

            int InterviewID = UDFLib.ConvertToInteger(Request.QueryString["InterviewID"]);
            string H = Convert.ToString(e.NewValues["InterviewPlanH"]);
            string M = Convert.ToString((frmInterviewDetails.FindControl("ddlPlanM") as DropDownList).SelectedValue);
            string planDate = UDFLib.ConvertToDefaultDt((frmInterviewDetails.FindControl("txtPlanDate") as TextBox).Text.ToString());
            string InterviewPlanDate = planDate + " " + H + ":" + M;
         
            int PlannedInterviewerID = UDFLib.ConvertToInteger((frmInterviewDetails.FindControl("ddlPlanInterviewer") as DropDownList).SelectedValue);
            int InterviewRank = UDFLib.ConvertToInteger(e.NewValues["InterviewRank"]);

            string TimeZone = (frmInterviewDetails.FindControl("ddlTimeZone") as DropDownList).SelectedValue;

            if (int.Parse((frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).SelectedValue) == 0)
            {
                string ParentPage = Request.QueryString["ParentPage"];
                if (ParentPage != null && ParentPage.Equals("CrewInterview.aspx"))
                {
                    js = "alert('Interview Sheet is mandatory');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    return;
                }
            }
            int IQID = UDFLib.ConvertToInteger((frmInterviewDetails.FindControl("ddlInterviewSheet") as DropDownList).SelectedValue);
            if (PlannedInterviewerID == 0 )
            {
                js = "alert('Select planned interviewer!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                int Res = BLL_Crew_Interview.UPDATE_CrewInterviewPlanning(InterviewID, InterviewPlanDate, PlannedInterviewerID, InterviewRank, Convert.ToInt32(Session["UserID"]), TimeZone, IQID);

                if (Res == 1)
                {
                    js = "alert('Interview Schedule Updated!!');parent.hideModal('dvPopupFrame');parent.window.location.reload();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
                else if (Res == -1)
                {
                    js = "alert('Another interview is scheduled for the interviewer at the same time. Interview schedule not updated!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
            }
        }
        catch { }
    }
}