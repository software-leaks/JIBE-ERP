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
using System.IO;
using AjaxControlToolkit4;

public partial class Crew_CrewDetails_Training : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
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
            CalendarExtender5.Format = UDFLib.GetDateFormat();
            if (!IsPostBack)
            {
                UserAccessValidation();
                int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                int TrainingID = UDFLib.ConvertToInteger(Request.QueryString["TrainingID"]);
                string Mode = Request.QueryString["Mode"];

                HiddenField_CrewID.Value = CrewID.ToString();

                Session["dtTrainingAttachments"] = CreateAttachmentTable();
                if (Mode == "EDIT")
                {
                    if (objUA.Edit == 1)
                    {
                        pnlEditTraining.Visible = true;
                        btnSaveAndAdd.Visible = false;

                        Load_TrainingToEdit(TrainingID);
                        Load_Trainers();
                        Load_TraiingTypes();
                        Load_Attachments(TrainingID);
                    }
                }
                else if (Mode == "ATT")
                {
                    pnlAtt.Visible = true;
                    Load_Attachments(TrainingID);

                }

                else if (Mode == "INSERT")
                {
                    if (objUA.Add == 1)
                    {
                        pnlEditTraining.Visible = true;
                        btnSaveAndAdd.Visible = true;
                        Load_Trainers();
                        Load_TraiingTypes();
                    }
                }
                else
                {
                    if (objUA.View == 1)
                    {
                        pnlFragmentTool.Visible = true;
                        pnlViewTrainings.Visible = true;
                        Load_Trainings();
                    }
                }
            }
        }
    }
    protected DataTable CreateAttachmentTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("Attach_Name");
        dt.Columns.Add("Attach_Path");
        dt.Columns.Add("Attach_Size");
        dt.PrimaryKey = new DataColumn[] { dt.Columns["PKID"] };
        return dt;
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
            //   ImgAddTraining.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Trainings.Columns[GridView_Trainings.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Trainings.Columns[GridView_Trainings.Columns.Count - 2].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --
        if (Session["USERCOMPANYID"].ToString() != "1")
        {
        }
        else//--- CREW TEAM LOGIN--
        {
        }
    }

    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void Load_TrainingToEdit(int TrainingID)
    {
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        DataTable dt = BLL_Crew_Training.Get_Crew_Trainings(CrewID, TrainingID, GetSessionUserID());
        if (dt.Rows.Count > 0)
        {
            txtDateofTraining.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["StartDate"]));
            ddlTrainer.SelectedValue = dt.Rows[0]["Trainer"].ToString();
            ddlTrainingType.SelectedValue = dt.Rows[0]["TrainingType"].ToString();
            txtResult.Text = dt.Rows[0]["Result"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
        Load_Attachments(TrainingID);
    }
    protected void Load_Attachments(int TrainingID)
    {

        DataTable dtAttachments = BLL_Crew_Training.Get_Training_Attachments(TrainingID, GetSessionUserID());
        rptAttachments.DataSource = dtAttachments;
        rptAttachments.DataBind();
        rptAttachmentse.DataSource = dtAttachments;
        rptAttachmentse.DataBind();

    }
    protected void Load_Trainings()
    {
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        DataTable dt = BLL_Crew_Training.Get_Crew_Trainings(CrewID, GetSessionUserID());
        GridView_Trainings.DataSource = dt;
        GridView_Trainings.DataBind();
    }
    protected void Load_Trainers()
    {
        DataTable dt = BLL_Crew_Training.Get_Crew_Trainers(GetSessionUserID());
        ddlTrainer.DataSource = dt;
        ddlTrainer.DataValueField = "LibUserID";
        ddlTrainer.DataTextField = "TrainerName";
        ddlTrainer.DataBind();
    }
    protected void Load_TraiingTypes()
    {
        DataTable dt = BLL_Crew_Training.Get_Crew_TrainingTypes();
        ddlTrainingType.DataSource = dt;
        ddlTrainingType.DataValueField = "TrainingTypeID";
        ddlTrainingType.DataTextField = "TrainingTypeName";
        ddlTrainingType.DataBind();
    }


    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        Save_Training("SaveAndClose");
    }

    protected void btnSaveAndAdd_Click(object sender, EventArgs e)
    {
        Save_Training("SaveAndAdd");
    }

    private int Save_Training(string Mode)
    {
        int Res = 0;
        string js = "";

        if (UDFLib.ConvertDateToNull(txtDateofTraining.Text) == null || UDFLib.ConvertToInteger(ddlTrainingType.SelectedValue) == 0 || UDFLib.ConvertToInteger(ddlTrainer.SelectedValue) == 0)
        {
            js = "alert('Unable to save as some of the fields are left blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);
        }
        else
        {
            int CrewID = UDFLib.ConvertToInteger(HiddenField_CrewID.Value);
            string Date_Of_Training = txtDateofTraining.Text;
            int TrainingType = UDFLib.ConvertToInteger(ddlTrainingType.Text);
            int Trainer = UDFLib.ConvertToInteger(ddlTrainer.SelectedValue);
            decimal Result = UDFLib.ConvertToDecimal(txtResult.Text);
            string Remarks = txtRemarks.Text;

            string EditMode = Request.QueryString["Mode"];

            DataTable dt = new DataTable();
            dt.Columns.Add("PID");

            if (HiddenField_SelectedFiles.Value != "")
            {
                string Attachments = HiddenField_SelectedFiles.Value;
                foreach (string id in Attachments.Split(','))
                {
                    DataRow dr = dt.NewRow();
                    dr["PID"] = id;
                    dt.Rows.Add(dr);
                }
            }

            if (EditMode == "EDIT")
            {
                int TrainingID = UDFLib.ConvertToInteger(Request.QueryString["TrainingID"]);
                Res = BLL_Crew_Training.UPDATE_Training(TrainingID, Convert.ToDateTime(txtDateofTraining.Text), UDFLib.ConvertToInteger(ddlTrainingType.SelectedValue), UDFLib.ConvertToInteger(ddlTrainer.SelectedValue), txtRemarks.Text, Result, GetSessionUserID(), dt);

                js = "parent.GetCrewTrainingLog(" + HiddenField_CrewID.Value.ToString() + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            //else if (EditMode == "INSERT")
            //{
            //    Res = BLL_Crew_Training.INSERT_Training(CrewID, Convert.ToDateTime(txtDateofTraining.Text), UDFLib.ConvertToInteger(ddlTrainingType.SelectedValue), UDFLib.ConvertToInteger(ddlTrainer.SelectedValue), txtRemarks.Text, Result, GetSessionUserID(), dt, 0, "");

            //    js = "parent.GetCrewTrainingLog(" + HiddenField_CrewID.Value.ToString() + ");";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            //}

            if (Mode == "SaveAndClose")
            {
                string js1 = "parent.hideModal('dvPopupFrame');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgc", js1, true);
            }
            else
                Load_Attachments(UDFLib.ConvertToInteger(Request.QueryString["TrainingID"]));
        }
        return Res;

    }

    protected void GridView_Trainings_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Cells[1].Text = (!e.Row.Cells[1].Text.Contains("&nbsp")) ? UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[1].Text)) : "";
                e.Row.Cells[0].Text = (!e.Row.Cells[0].Text.Contains("&nbsp")) ? UDFLib.ConvertUserDateFormat(Convert.ToString(e.Row.Cells[0].Text)) : "";
            }
            catch
            { }
        }
    }

}