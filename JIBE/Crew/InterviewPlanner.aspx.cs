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


public partial class Crew_InterviewPlanner : System.Web.UI.Page
{

    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    UserAccess objUA = new UserAccess();
    int InterviewID = 0;
    string UAAdminRights;
    public string DateFormat = "";

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
        DateFormat = hdnDateFormat.Value = CalendarExtender1.Format = UDFLib.GetDateFormat();

        if (!IsPostBack)
        {
            int CrewID = GetCrewID();
            int CurrentUserID = GetSessionUserID();

            hdnUserID.Value = CurrentUserID.ToString();
            hdnCrewID.Value = CrewID.ToString();
            lnkEditInterviewPlanning.Enabled = false;
            if (CrewID > 0)
            {
                txtStaffCode.Text = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "Staff_Code");
                txtStaffName.Text = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "Staff_Name");
                txtPlanCrewName.Text = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "Staff_FullName");

                lnkEditInterviewPlanning.Enabled = true;
            }
            if (getQueryString("Mode") == "Add")
            {
                lnkEditInterviewPlanning_Click();
            }
        }
        lblMessage.Text = "";
        UserAccessValidation();
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {
            lnkEditInterviewPlanning.Visible = false;
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
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
        UAAdminRights = objUA.Admin.ToString();

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
    }

    protected void btnSavePlanning_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPlanDate.Text != "")
            {
                if (!UDFLib.DateCheck(txtPlanDate.Text))
                {
                    lblMessage.Text = "Enter valid Interview Date" + UDFLib.DateFormatMessage();
                    return;
                }
            }
            int iCrewID = int.Parse(hdnCrewID.Value);
            if (iCrewID == 0)
            {
                lblMessage.Text = "Please select a crew from the above list to plan an interview";
                return;
            }
            if (int.Parse(ddlInterviewSheet.SelectedValue) == 0)
            {
                lblMessage.Text = "Please select interview sheet";
                return;
            }
            if (int.Parse(ddlPlanInterviewer.SelectedValue) == 0)
            {
                lblMessage.Text = "Interviewer is mandatory";
                return;
            }
            string PlanDateTime = UDFLib.ConvertUserDateFormat(Convert.ToString(txtPlanDate.Text)) + " " + ddlPlanH.Text + ":" + ddlPlanM.Text;
            if (int.Parse(ddlPlanH.Text) < 12)
                PlanDateTime += " AM";
            else
                PlanDateTime += " PM";

            string TimeZone = ddlTimeZone.SelectedValue;

            int iInterviewID = objCrewBLL.INS_CrewInterviewPlanning(iCrewID, int.Parse(ddlInterviewRank.SelectedValue), txtPlanCrewName.Text, PlanDateTime, int.Parse(ddlPlanInterviewer.SelectedValue), txtPlanInterviewerPosition.Text, GetSessionUserID(), TimeZone, "Interview", int.Parse(ddlInterviewSheet.SelectedValue));
            hdnInterviewID.Value = iInterviewID.ToString();
            lblMessage.Text = "Interview planned for " + txtPlanCrewName.Text;

            GridView2.DataBind();

            pnlEdit_InterviewPlanning.Visible = false;
            pnlView_InterviewPlanning.Visible = true;


            string js = "thisMonth();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);

            if (iInterviewID > 0)
                SendMail_InterviewPlanning(iInterviewID);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }
    protected void btnCancelPlanning_Click(object sender, EventArgs e)
    {
        //Response.Redirect("CrewList.aspx");
        pnlEdit_InterviewPlanning.Visible = false;
        pnlView_InterviewPlanning.Visible = true;
        lblMessage.Text = "";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        int CrewID = int.Parse(GridView1.SelectedDataKey["ID"].ToString());
        hdnCrewID.Value = CrewID.ToString();

        if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
        {
            string ManningOfficeStatus = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "ManningOfficeStatus");

            if (ManningOfficeStatus != "")
            {
                if (int.Parse(ManningOfficeStatus) > 0)
                    lnkEditInterviewPlanning.Enabled = false;
                else
                    lnkEditInterviewPlanning.Enabled = true;
            }
            else
                lnkEditInterviewPlanning.Enabled = true;
        }
        else
            lnkEditInterviewPlanning.Enabled = true;

        txtPlanCrewName.Text = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "Staff_FullName");

        pnlEdit_InterviewPlanning.Visible = false;
        pnlView_InterviewPlanning.Visible = true;

        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
    }
    protected void lnkEditInterviewPlanning_Click(object sender, EventArgs e)
    {
        lnkEditInterviewPlanning_Click();
    }
    protected void lnkEditInterviewPlanning_Click()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        BLL_Infra_TimeZones objTimeZone = new BLL_Infra_TimeZones();

        try
        {
            int CrewID = int.Parse(hdnCrewID.Value);
            int CurrentRankId = 0;
            if (objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "CurrentRankID") != null && objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "CurrentRankID") != "")
                CurrentRankId = int.Parse(objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "CurrentRankID"));
            LoadInterviewSheet(CurrentRankId);
            ddlPlanRank.DataSource = objCrewAdmin.Get_RankList();
            ddlPlanRank.DataBind();
            ddlPlanRank.Enabled = false;

            ddlInterviewRank.DataSource = objCrewAdmin.Get_RankList();
            ddlInterviewRank.DataBind();

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

            string rank = objCrewBLL.Get_CrewPersonalDetailsByID(CrewID, "CurrentRankID");
            if (rank != "")
            {
                ddlPlanRank.SelectedValue = rank;
                ddlInterviewRank.SelectedValue = rank;
            }
            else
            {
                ddlPlanRank.SelectedIndex = 0;
            }

            int IsMandatoryDocumentsUploaded = objCrewBLL.IsMandatoryDocumentsUploaded(CrewID);

            if (IsMandatoryDocumentsUploaded == 0)
            {
                lblMessage.Text = "Interview can not be planned for the crew. The necessery documents are not yet uploaded!!";
                return;
            }

        }
        catch { }

        pnlEdit_InterviewPlanning.Visible = true;
        pnlView_InterviewPlanning.Visible = false;
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
        pnlEdit_InterviewPlanning.Visible = false;
        pnlView_InterviewPlanning.Visible = true;
        lblMessage.Text = "";
    }

    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
    }
    protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        string js = "thisMonth();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "cal", js, true);
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
            }
            ImageButton imgBtnDelete = (ImageButton)(e.Row.FindControl("imgBtnDelete"));
            if (UAAdminRights.Equals("1"))
            {
                if (imgBtnDelete != null)
                    imgBtnDelete.Visible = true;
            }
            else
            {
                if (imgBtnDelete != null && InterviewDate != "")
                {
                    imgBtnDelete.Visible = false;
                }
            }

            TextBox txt = (TextBox)e.Row.FindControl("InterviewPlanDate");

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
        string Rank = "", InterviewRank = "";
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

            string msgBCC = "";

            string msgSubject = "Crew Interview Planned for " + Interviewer + " at " + PlanDate + " " + TimeZone;

            string msgBody = @"Crew Interview Planned for " + Interviewer + " at " + PlanDate + " " + TimeZone + @"

            Staff Name:   " + Staff_Name + @"
            Rank applied for: " + Rank + @"
            Interview for Rank : " + InterviewRank + @"
            Manning Agent: " + ManningOffice + @"
            Readiness: " + ReadyToJoin + @". "

            + ManningOffice + "-RIC, please ensure necessary arrangements are made according to this schedule,and the sea-farer is informed and available for the above interview.";
            objCrewBLL.Send_CrewNotification(GetCrewID(), int.Parse(ManningOfficeID), 0, 2, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MEETING", PlanDate, GetSessionUserID(), "READY", TimeZone);
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
    }

    protected void ObjectDataSource2_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (e.InputParameters["InterviewPlanDate"].ToString() != null)
        {
            if (!UDFLib.DateCheck(e.InputParameters["InterviewPlanDate"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", "alert('Enter valid Date" + UDFLib.DateFormatMessage() + "');", true);
                return;
            }
        }

        e.InputParameters["InterviewPlanDate"] = UDFLib.ConvertToDate(e.InputParameters["InterviewPlanDate"].ToString()).ToShortDateString();
        InterviewID = int.Parse(e.InputParameters["Id"].ToString());
    }

    /// <summary>
    /// After interview update following method called.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSource2_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (Convert.ToInt32(e.ReturnValue) > 0)
            {
                int iCrewID = int.Parse(hdnCrewID.Value);
                if (InterviewID > 0)
                {
                    SendMail_InterviewPlanning(InterviewID);
                }
            }
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }
}