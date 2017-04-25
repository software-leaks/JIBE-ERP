using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Crew_InterviewPlannerPopup : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_TimeZones objTimeZone = new BLL_Infra_TimeZones();
    UserAccess objUA = new UserAccess();
    int InterviewID = 0;
    public string DateFormatMessage = "";
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
        CalendarExtender1.Format = UDFLib.GetDateFormat();
        DateFormatMessage = UDFLib.DateFormatMessage();
        hdnDateFormat.Value = UDFLib.GetDateFormat();//Get User date format
        if (!IsPostBack)
        {
            int CrewID = GetCrewID();
            Session["CrewID"] = CrewID;
            int CurrentUserID = GetSessionUserID();

            hdnUserID.Value = CurrentUserID.ToString();
            hdnCrewID.Value = CrewID.ToString();

            int CurrentRankId = 0;
            DataTable dtPersonalDetail = objCrewBLL.Get_CrewPersonalDetailsByID1(CrewID);
            if (dtPersonalDetail.Rows.Count > 0)
            {
                CurrentRankId = int.Parse(dtPersonalDetail.Rows[0]["CurrentRankID"].ToString());
                txtPlanCrewName.Text = dtPersonalDetail.Rows[0]["Staff_FullName"].ToString(); 
            }
            LoadInterviewSheet(CurrentRankId);
            LoadRankList();
            LoadTimeZone();

            ddlPlanRank.SelectedValue = CurrentRankId.ToString();
            ddlInterviewRank.SelectedValue = CurrentRankId.ToString();

            int IsMandatoryDocumentsUploaded = objCrewBLL.IsMandatoryDocumentsUploaded(CrewID);
            if (IsMandatoryDocumentsUploaded == 0)
            {
                lblMessage.Text = "Interview can not be planned for the crew. The necessery documents are not yet uploaded!!";
                return;
            }
        }
        lblMessage.Text = "";
        UserAccessValidation();
       
    }
    protected void LoadRankList()
    {
        DataTable dtRankList = objCrewAdmin.Get_RankList();
        ddlPlanRank.DataSource = dtRankList;
        ddlPlanRank.DataBind();
        ddlPlanRank.Enabled = false;

        ddlInterviewRank.DataSource = dtRankList;
        ddlInterviewRank.DataBind();
    }
    protected void Load()
    {
    }
    protected void LoadTimeZone()
    {
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
            //lnkEditInterviewPlanning.Visible = false;
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

    protected void ddlPlanInterviewer_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iUserID = int.Parse(ddlPlanInterviewer.SelectedValue);
        txtPlanInterviewerPosition.Text = objCrewBLL.GetCurrentUserDesignation(iUserID);
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
    }

    protected void btnSavePlanning_Click(object sender, EventArgs e)
    {
        try
        {
            int iCrewID = int.Parse(hdnCrewID.Value);
            if (iCrewID == 0)
            {
                lblMessage.Text = "Please select a crew from the above list to plan an interview";
                return;
            }
            string PlanDateTime =  UDFLib.ConvertToDefaultDt(txtPlanDate.Text) + " " + ddlPlanH.Text + ":" + ddlPlanM.Text;
            if (int.Parse(ddlPlanH.Text) < 12)
                PlanDateTime += " AM";
            else
                PlanDateTime += " PM";

            string TimeZone = ddlTimeZone.SelectedValue;

            int iInterviewID = objCrewBLL.INS_CrewInterviewPlanning(iCrewID, int.Parse(ddlInterviewRank.SelectedValue), txtPlanCrewName.Text, PlanDateTime, int.Parse(ddlPlanInterviewer.SelectedValue), txtPlanInterviewerPosition.Text, GetSessionUserID(), TimeZone, "Interview", int.Parse(ddlInterviewSheet.SelectedValue));
            hdnInterviewID.Value = iInterviewID.ToString();

            GridView2.DataBind();

            string js = "thisMonth();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);

            if (iInterviewID > 0)
            {                
                lblMessage.Text = "Interview planned for " + txtPlanCrewName.Text;
                SendMail_InterviewPlanning(iInterviewID);
                js = "parent.GetInterviewResult(" + iCrewID + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                lblMessage.Text = "Interview already planned for " + txtPlanCrewName.Text + " at same time " + PlanDateTime;
            }
            txtPlanDate.Text = "";
            ddlPlanInterviewer.SelectedIndex = 0;
            ddlInterviewRank.SelectedIndex = 0;

            ddlInterviewRank.SelectedValue = ddlPlanRank.SelectedValue;
            LoadInterviewSheet(int.Parse(ddlInterviewRank.SelectedValue));

            ddlPlanH.SelectedValue = "12";
            ddlPlanM.SelectedValue = "00";

            try
            {
                DataTable dt = (DataTable)ddlTimeZone.DataSource;
                DataRow[] dr = dt.Select("DefaultTimeZone=1");
                if (dr.Length > 0)
                {
                    ddlTimeZone.SelectedValue = dr[0]["TimeZone"].ToString();
                }
            }
            catch { }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }
    protected void btnCancelPlanning_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        int iCrewID = int.Parse(hdnCrewID.Value);
        string js = "parent.GetInterviewResult(" + iCrewID + ");parent.hideModal('dvPlan');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
    protected void LoadInterviewSheet(int RankId)
    {
        ddlInterviewSheet.Items.Clear();
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        ddlInterviewSheet.DataSource = objCrewAdmin.Get_InterviewSheets(RankId, "Interview");
        ddlInterviewSheet.DataBind();
        ddlInterviewSheet.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void lnkCancelEditInterviewPlanning_Click(object sender, EventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
        lblMessage.Text = "";
    }

    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
        int iCrewID = int.Parse(hdnCrewID.Value);
        js = "parent.GetInterviewResult(" + iCrewID + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
    protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
        int iCrewID = int.Parse(hdnCrewID.Value);
        js = "parent.GetInterviewResult(" + iCrewID + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
    protected void rdoShowInterviews_SelectedIndexChanged(object sender, EventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (dr["InterviewPlanDate"].ToString() != "" && txt != null)
            {
                txt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["InterviewPlanDate"].ToString()));
            }
        }
    }


    protected void SendMail_InterviewPlanning(int iInterviewID)
    {
        string ManningOfficeID = "0";
        string ManningOffice = "";
        string ManningOfficeMailID = "";
        string Staff_Name = "";
        string Rank = "",InterviewRank ="";
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
                Rank = dt.Rows[0]["Applied_Rank_Name"].ToString();
                ReadyToJoin = dt.Rows[0]["available_from_date"].ToString();
            }

            DataTable dtInt = objCrewBLL.getInterviewDetails(iInterviewID);
            if (dtInt.Rows.Count > 0)
            {
                Interviewer = dtInt.Rows[0]["PlannedInterviewer"].ToString();
                PlanDate = dtInt.Rows[0]["InterviewPlanDate"].ToString();
                PlanInterviewerMailID = dtInt.Rows[0]["PlanInterviewerMailID"].ToString();
                TimeZone = dtInt.Rows[0]["TimeZone"].ToString();
                InterviewRank = dtInt.Rows[0]["Rank_Name"].ToString();
            }
            string msgTo = PlanInterviewerMailID;
            string msgCC = "";

            if (ManningOfficeMailID != "")
                msgCC += ";" + ManningOfficeMailID;


            string msgSubject = "Crew Interview Planned for " + Interviewer + " at " + PlanDate + " " + TimeZone;

            string msgBody = @"Crew Interview Planned for " + Interviewer + " at " + PlanDate + " " + TimeZone + @"

            Staff Name:   " + Staff_Name + @"
            Rank applied for: " + Rank + @"
            Interview for Rank : " + InterviewRank + @"
            Manning Agent: " + ManningOffice + @"
            Readiness: " + ReadyToJoin + @". "

            + ManningOffice + "-RIC, please ensure necessary arrangements are made according to this schedule,and the sea-farer is informed and available for the above interview.";

            objCrewBLL.Send_CrewNotification(GetCrewID(), int.Parse(ManningOfficeID), 0, 2, msgTo, msgCC, "", msgSubject, msgBody, "", "MEETING", PlanDate, GetSessionUserID(), "READY", TimeZone);

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }


    }
    protected void ddlInterviewRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iRankID = int.Parse(ddlInterviewRank.SelectedValue);
        LoadInterviewSheet(iRankID);
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
        int iCrewID = int.Parse(hdnCrewID.Value);
        js = "parent.GetInterviewResult(" + iCrewID + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
    }

    protected void ObjectDataSource2_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow currentRow = (GridViewRow)((ImageButton)sender).Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtPlanDate");
            ImageButton imgBtn = (ImageButton)currentRow.FindControl("LinkButton1");
            if ((imgBtn != null) && (txt != null))
            {
                if (!UDFLib.DateCheck(txt.Text))
                {
                    lblMessage.Text = "Enter valid date" + DateFormatMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                txt.Text = UDFLib.ConvertToDefaultDt(txt.Text);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}