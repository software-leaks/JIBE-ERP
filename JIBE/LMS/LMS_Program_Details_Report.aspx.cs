using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.LMS;
using SMS.Business.Crew;
using System.Text;

public partial class LMS_Program_Details_Report : System.Web.UI.Page
{
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean uaScheduleFlag = false;
    DataSet ds_TrgPrgDetails;
    DataTable dtResourceItem, dtAttendees;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Vessel_Id = Convert.ToInt32(Request.QueryString["Vessel_Id"]);
            lblVessel.Text = Convert.ToString(Request.QueryString["Vessel_Short_Name"]);
            lblProgramName.Text = BLL_LMS_Training.Get_ProgramDescriptionbyId(Convert.ToInt32(Request.QueryString["Program_Id"])).Rows[0]["PROGRAM_Name"].ToString();
            lblProgramDescription.Text = BLL_LMS_Training.Get_ProgramDescriptionbyId(Convert.ToInt32(Request.QueryString["Program_Id"])).Rows[0]["PROGRAM_DESCRIPTION"].ToString();
            ds_TrgPrgDetails = BLL_LMS_Training.GET_Program_Summary(Convert.ToInt32(Request.QueryString["Program_Id"]), Convert.ToInt32(Request.QueryString["SCHEDULE_ID"]), Convert.ToInt32(Request.QueryString["Office_Id"]), Vessel_Id);
            lblRemarks.Text = ds_TrgPrgDetails.Tables[3].Rows[0]["Remarks"].ToString();
            if (ds_TrgPrgDetails.Tables[3].Rows[0]["TRG_START_DATE"].ToString().Trim() != "")
                lblTrgStDate.Text = UDFLib.ConvertDateToNull(ds_TrgPrgDetails.Tables[3].Rows[0]["TRG_START_DATE"].ToString()).Value.ToString("dd/MMM/yyyy");
            if (ds_TrgPrgDetails.Tables[3].Rows[0]["TRG_END_DATE"].ToString().Trim() != "")
                lblTrgEnDate.Text = UDFLib.ConvertDateToNull(ds_TrgPrgDetails.Tables[3].Rows[0]["TRG_END_DATE"].ToString()).Value.ToString("dd/MMM/yyyy");
            if (ds_TrgPrgDetails.Tables[3].Rows[0]["Program_Category_Id"].ToString() == "4")
                btndrillactivity.Visible = true;
            else
                btndrillactivity.Visible = false;
            dtResourceItem = ds_TrgPrgDetails.Tables[1];
            dtAttendees = ds_TrgPrgDetails.Tables[2];
            gvTrainingProgram_Details.DataSource = ds_TrgPrgDetails.Tables[0];
            gvTrainingProgram_Details.DataBind();
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void GridView_Selected_SelectedIndexChanged(object sender, EventArgs e)
    {


    }


    protected void GridView_Selected_RowDeleting(object sender, EventArgs e)
    {

    }

    protected void BindTrainingProgramDetailsInGrid()
    {

    }

    protected void gvTrainingProgram_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt;
            string Chapter_Id = Convert.ToString(gvTrainingProgram_Details.DataKeys[e.Row.RowIndex].Value);
            dtResourceItem.DefaultView.RowFilter = "CHAPTER_ID=" + Chapter_Id;
            DataList dlResourceItem = (DataList)e.Row.FindControl("dlResourceItem");
            if (dtResourceItem.DefaultView.ToTable().Rows.Count != 0)
            {
                dlResourceItem.DataSource = dtResourceItem.DefaultView.ToTable();
            }
            else
            {
                e.Row.Visible = false;
            }

            dtResourceItem.DefaultView.RowFilter = "";
            dtResourceItem.DefaultView.RowFilter = "CHAPTER_ID <>" + Chapter_Id;
            dt = dtResourceItem.DefaultView.ToTable();
            dtResourceItem = dt.Copy();
            dlResourceItem.DataBind();
            dtAttendees.DefaultView.RowFilter = "CHAPTER_ID=" + Chapter_Id;
            DataList dlAttendees = (DataList)e.Row.FindControl("dlAttendees");
            dlAttendees.DataSource = dtAttendees.DefaultView.ToTable();
            dlAttendees.DataBind();
        }

    }
    protected void btndrillactivity_Click(object sender, EventArgs e)
    {

        int Vessel_Id = Convert.ToInt32(Request.QueryString["Vessel_Id"]);
        //string Program_Name =Convert.ToString(Request.QueryString["PROGRAM_Name"]).ToString();
        int Schedule_Id = Convert.ToInt32(Request.QueryString["SCHEDULE_ID"]);
        int? Office_Id = Convert.ToInt32(Request.QueryString["Office_Id"]);
        //Response.Redirect("eForms/eFormTempletes/DrillReport.aspx?   =" + Vessel_Id );

        Response.Redirect(string.Format("~/eForms/eFormTempletes/DrillReport.aspx?param1={0}&param2={1}&param3={2}", Vessel_Id, Schedule_Id, Office_Id));



    }
}