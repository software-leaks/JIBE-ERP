using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using System.Collections;
using System.Text;
using SMS.Properties;


public partial class Technical_PMS_PMSJobChangeRequestAction : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();



    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        if (!IsPostBack)
        {

            BindPMSDepartmentDDL();
            BindFrequencyTypeDDL();
            BindRankDDL();

            BindJobChangeRequestsList();


        }

    }

    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {
            btnDivApprove.Enabled = false;
            btnDivReject.Enabled = false;
        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Delete == 0)
        {


        }

    }


    public void BindJobList(int jobid)
    {

        BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();
        DataSet ds = objJob.LibraryJobList(jobid);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtJobtitle.Text = dr["Job_Title"].ToString();
            txtjobDescription.Text = dr["Job_Description"].ToString();
            lstDepartment.SelectedValue = dr["Department_ID"].ToString();

            if ((dr["Rank_ID"].ToString() != "0") && (dr["Rank_ID"].ToString() != ""))
            {
                ddlRank.SelectedValue = dr["Rank_ID"].ToString();
            }
            txtFrequency.Text = dr["Frequency"].ToString();
            lstFrequency.SelectedValue = dr["Frequency_Type"].ToString();



            lblJobCode.Text = dr["Job_Code"].ToString();
            lblMachinery.Text = dr["System_Description"].ToString();
            lblSubsystem.Text = dr["Subsystem_Description"].ToString();

            optCMS.SelectedValue = dr["CMS"].ToString();
            optCritical.SelectedValue = dr["Critical"].ToString();
        }

    }

    public void BindJobChangeRequestsList()
    {
        BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();
        DataSet ds = objChangeRqst.TecJobChangeRequestList(Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString()), Convert.ToInt32(Request.QueryString["VESSELID"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            //Make a default font colour black.
            txtCRJobtitle.ForeColor = System.Drawing.Color.Black;
            txtCRjobDescription.ForeColor = System.Drawing.Color.Black;
            txtCRFrequency.ForeColor = System.Drawing.Color.Black;
            lstCRFrequency.ForeColor = System.Drawing.Color.Black;
            optCRCMS.ForeColor = System.Drawing.Color.Black;
            optCRCritical.ForeColor = System.Drawing.Color.Black;
            lstDepartment.ForeColor = System.Drawing.Color.Black;
            ddlCRRank.ForeColor = System.Drawing.Color.Black;
            txtCRActionedRemarks.ForeColor = System.Drawing.Color.Black;
            txtCRChangeReason.ForeColor = System.Drawing.Color.Black;


            DataRow dr = ds.Tables[0].Rows[0];

            ViewState["JOB_ID"] = dr["JOB_ID"].ToString();
            ViewState["System_ID"] = dr["System_ID"].ToString();
            ViewState["SubSystem_ID"] = dr["SubSystem_ID"].ToString();
            ViewState["REQUEST_FOR"] = dr["REQUEST_FOR"].ToString();

            ViewState["CR_Actual"] = dr["CR_Actual"].ToString();

            if (dr["REQUEST_FOR"].ToString() == "ADDNEW")
                lblRequestFor.Text = "Adding New Job";
            else if (dr["REQUEST_FOR"].ToString() == "EDIT")
                lblRequestFor.Text = "Editing Job";
            else if (dr["REQUEST_FOR"].ToString() == "DELETE")
                lblRequestFor.Text = "Delete this Job";




            txtJobtitle.Text = dr["JOB_TITLE"].ToString();
            txtCRJobtitle.Text = dr["CR_JOB_TITLE"].ToString();

            txtjobDescription.Text = dr["JOB_DESCRIPTION"].ToString();
            txtCRjobDescription.Text = dr["CR_JOB_DESCRIPTION"].ToString();

            txtFrequency.Text = dr["FREQUENCY"].ToString();
            txtCRFrequency.Text = dr["CR_FREQUENCY"].ToString();

            optCMS.SelectedValue = dr["CMS"].ToString();
            optCRCMS.SelectedValue = dr["CR_CMS"].ToString();

            optCritical.Text = dr["CRITICAL"].ToString();
            optCRCritical.Text = dr["CR_CRITICAL"].ToString();

            lstFrequency.SelectedValue = dr["FREQUENCY_TYPE"].ToString();
            lstCRFrequency.SelectedValue = dr["CR_FREQUENCY_TYPE"].ToString();

            lstDepartment.SelectedValue = dr["DEPARTMENT_ID"].ToString();
            lstCRDepartment.SelectedValue = dr["CR_DEPARTMENT_ID"].ToString();

            txtCRChangeReason.Text = dr["CR_Reason"].ToString();


            if ((dr["Rank_ID"].ToString() != "0") && (dr["Rank_ID"].ToString() != ""))
            {
                ddlRank.SelectedValue = dr["Rank_ID"].ToString();
            }
            if ((dr["CR_RANK_ID"].ToString() != "0") && (dr["CR_RANK_ID"].ToString() != ""))
            {
                ddlCRRank.SelectedValue = dr["CR_RANK_ID"].ToString();
            }

            txtCRActionedRemarks.Text = dr["CR_Remarked_Actioned"].ToString();
            lblActionedBy.Text = dr["ACTIONEDBY"].ToString();
            lblRequestedBy.Text = dr["REQUESTBY"].ToString();

            if (dr["JOB_ID"].ToString() != "")
            {
                BindJobList(Convert.ToInt32(ViewState["JOB_ID"].ToString()));
            }


            if (dr["REQUEST_FOR"].ToString() == "EDIT")
            {

                if (dr["DiffJobTitleFlag"].ToString() == "N")
                {
                    txtJobtitle.ForeColor = System.Drawing.Color.Blue;
                    txtCRJobtitle.ForeColor = System.Drawing.Color.Blue;
                }

                if (dr["DiffJobDescFlag"].ToString() == "N")
                {
                    txtjobDescription.ForeColor = System.Drawing.Color.Blue;
                    txtCRjobDescription.ForeColor = System.Drawing.Color.Blue;
                }

                if (dr["DiffFrequencyFlag"].ToString() == "N")
                {
                    txtFrequency.ForeColor = System.Drawing.Color.Blue;
                    txtCRFrequency.ForeColor = System.Drawing.Color.Blue;
                }

                if (dr["DiffFreqTypeFlag"].ToString() == "N")
                {
                    lstFrequency.ForeColor = System.Drawing.Color.Blue;
                    lstCRFrequency.ForeColor = System.Drawing.Color.Blue;
                }

                if (dr["DiffDeptFlag"].ToString() == "N")
                {
                    lstDepartment.ForeColor = System.Drawing.Color.Blue;
                    lstCRDepartment.ForeColor = System.Drawing.Color.Blue;
                }

                if (dr["DiffRankFlag"].ToString() == "N")
                {
                    ddlRank.ForeColor = System.Drawing.Color.Blue;
                    ddlCRRank.ForeColor = System.Drawing.Color.Blue;

                }

                if (dr["DiffCmsFlag"].ToString() == "N")
                {
                    optCMS.ForeColor = System.Drawing.Color.Blue;
                    optCRCMS.ForeColor = System.Drawing.Color.Blue;
                }

                if (dr["DiffCriticalFlag"].ToString() == "N")
                {
                    optCritical.ForeColor = System.Drawing.Color.Blue;
                    optCRCritical.ForeColor = System.Drawing.Color.Blue;
                }
            }


            if (Request.QueryString["Status"].ToString() == "PENDING" || Request.QueryString["Status"].ToString() == "")
            {
                btnDivApprove.Enabled = true;
                btnDivReject.Enabled = true;

                txtCRJobtitle.ReadOnly = false;
                txtCRjobDescription.ReadOnly = false;
                txtCRFrequency.ReadOnly = false;
                txtCRActionedRemarks.ReadOnly = false;
            }
            else
            {
                btnDivApprove.Enabled = false;
                btnDivReject.Enabled = false;

                txtCRJobtitle.ReadOnly = true;
                txtCRjobDescription.ReadOnly = true;
                txtCRFrequency.ReadOnly = true;
                txtCRActionedRemarks.ReadOnly = true;
            }
        }
    }

    private void BindPMSDepartmentDDL()
    {
        try
        {
            BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();

            DataTable dt = obj.LibraryGetPMSSystemParameterList("2487", "");

            lstDepartment.DataSource = dt;
            lstDepartment.DataValueField = "Code";
            lstDepartment.DataTextField = "Name";
            lstDepartment.DataBind();

            lstCRDepartment.DataSource = dt;
            lstCRDepartment.DataValueField = "Code";
            lstCRDepartment.DataTextField = "Name";
            lstCRDepartment.DataBind();


        }
        catch
        {
        }
    }

    private void BindFrequencyTypeDDL()
    {
        try
        {
            BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();
            DataTable dt = obj.LibraryGetPMSSystemParameterList("2491", "");
            lstFrequency.DataSource = dt;
            lstFrequency.DataTextField = "Name";
            lstFrequency.DataValueField = "Code";
            lstFrequency.DataBind();

            lstCRFrequency.DataSource = dt;
            lstCRFrequency.DataTextField = "Name";
            lstCRFrequency.DataValueField = "Code";
            lstCRFrequency.DataBind();
        }
        catch
        {
        }

    }

    private void BindRankDDL()
    {
        try
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();

            DataTable dtRank = new DataTable();
            dtRank = objCrewAdmin.Get_RankList();

            ddlRank.DataTextField = "Rank_Name";
            ddlRank.DataValueField = "ID";
            ddlRank.DataSource = dtRank;
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("SELECT", "0"));


            ddlCRRank.DataTextField = "Rank_Name";
            ddlCRRank.DataValueField = "ID";
            ddlCRRank.DataSource = dtRank;
            ddlCRRank.DataBind();
            ddlCRRank.Items.Insert(0, new ListItem("SELECT", "0"));

        }
        catch
        {
        }

    }

    protected void btnDivApprove_Click(object sender, EventArgs e)
    {

        BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();
        StringBuilder cr_actual_values = new StringBuilder();


        if ((string)ViewState["REQUEST_FOR"] == "ADDNEW")
        {
            string Jobid = "";

            objChangeRqst.TecJobChangeRequestSave(Convert.ToInt32(Session["userid"].ToString())
                , Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString()), txtCRActionedRemarks.Text, Convert.ToInt32(ViewState["System_ID"].ToString())
                , Convert.ToInt32(ViewState["SubSystem_ID"].ToString())
                , Convert.ToInt32(Request.QueryString["VESSELID"].ToString()), UDFLib.ConvertIntegerToNull(lstCRDepartment.SelectedValue)
                , UDFLib.ConvertIntegerToNull(ddlCRRank.SelectedValue), txtCRJobtitle.Text, txtCRjobDescription.Text, Convert.ToInt32(txtCRFrequency.Text)
                , Convert.ToInt32(lstCRFrequency.SelectedValue.ToString())
                , Convert.ToInt32(optCRCMS.SelectedValue), Convert.ToInt32(optCRCritical.SelectedValue), ref Jobid);

        }

        if ((string)ViewState["REQUEST_FOR"] == "DELETE")
        {

            objChangeRqst.TecJobChangeRequestDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString())
                 , txtCRActionedRemarks.Text, Convert.ToInt32(ViewState["JOB_ID"].ToString())
                 , Convert.ToInt32(Request.QueryString["VESSELID"].ToString()));

        }

        if ((string)ViewState["REQUEST_FOR"] == "EDIT")
        {

            cr_actual_values.Append("Job Title : ");
            cr_actual_values.Append(txtCRJobtitle.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Job Description :");
            cr_actual_values.Append(txtCRjobDescription.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Frequency :");
            cr_actual_values.Append(txtCRFrequency.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Frequency Type :");
            cr_actual_values.Append(lstCRFrequency.SelectedItem.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("CMS :");
            cr_actual_values.Append(optCRCMS.SelectedValue == "0" ? "N" : "Y");
            cr_actual_values.AppendLine();
            cr_actual_values.Append("CRITICAL:");
            cr_actual_values.Append(optCRCritical.SelectedValue == "0" ? "N" : "Y");
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Department:");
            cr_actual_values.Append(lstCRDepartment.SelectedItem.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Rank:");
            cr_actual_values.Append(ddlCRRank.SelectedItem.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Change Reason:");
            cr_actual_values.Append(txtCRChangeReason.Text);
            cr_actual_values.AppendLine();
            cr_actual_values.Append("Action Remarks:");
            cr_actual_values.Append(txtCRActionedRemarks.Text);
            cr_actual_values.AppendLine();



            objChangeRqst.TecJobChangeRequestUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString())
               , txtCRActionedRemarks.Text, Convert.ToInt32(ViewState["JOB_ID"].ToString())
               , Convert.ToInt32(Request.QueryString["VESSELID"].ToString()), UDFLib.ConvertIntegerToNull(lstCRDepartment.SelectedValue)
               , UDFLib.ConvertIntegerToNull(ddlCRRank.SelectedValue), txtCRJobtitle.Text, txtCRjobDescription.Text, Convert.ToInt32(txtCRFrequency.Text)
               , Convert.ToInt32(lstCRFrequency.SelectedValue.ToString())
               , Convert.ToInt32(optCRCMS.SelectedValue), Convert.ToInt32(optCRCritical.SelectedValue), cr_actual_values.ToString());


        }


        String script = String.Format("alert('Change Request has been Approved.');javascript:parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
         

    }

    protected void btnDivReject_Click(object sender, EventArgs e)
    {

        BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();
        objChangeRqst.TecJobChangeRequestReject(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString()), txtCRActionedRemarks.Text, Convert.ToInt32(Request.QueryString["VESSELID"].ToString()));

        String script = String.Format("alert('Change Request has been Rejected.');javascript:parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
    }

}