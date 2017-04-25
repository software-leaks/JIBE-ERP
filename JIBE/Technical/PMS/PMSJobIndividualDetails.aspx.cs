using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PMS;


public partial class Technical_PMS_PMSJobIndividualDetails : System.Web.UI.Page
{
    public int VID = 0;
    public int JID = 0;
    public int JHID = 0;
    public int SysLocID = 0;
    public int SUbSysLocID = 0;
    public int FunctionID = 0;

    BLL_PMS_Job_Status objJobStat = new BLL_PMS_Job_Status();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            JID = Convert.ToInt32(Request.QueryString["JobID"]);
            JHID = UDFLib.ConvertToInteger(Request.QueryString["JobHistoryID"]);
            VID = Convert.ToInt32(Request.QueryString["VID"]);

            // FunctionID = Convert.ToInt32(Request.QueryString["FunctionID"].ToString().Split(',')[0]);
            SysLocID = Convert.ToInt32(Request.QueryString["System_ID"].ToString().Split(',')[0]);
            SUbSysLocID = Convert.ToInt32(Request.QueryString["SubSystem_ID"].ToString().Split(',')[0]);

            if (JHID == 0)
                trMaintenanceLink.Visible = false;

            BindJobIndividualDetails();
            BindPmsJobAttachment();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// For binding Function, SystemLocation & SubSystemLoaction
    /// </summary>
    /// <param name="jobid">Job Id </param>
    /// <param name="JobHistoryID">Job History ID</param>
    /// <param name="QueryFlag">QueryFlag </param>
    /// <param name="SysLocID">System location Id</param>
    /// <param name="SubSysLocID">Sub system location Id</param>
    /// <returns></returns>
    private void BindJobIndividualDetails()
    {
        try
        {
            BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
            DataSet ds = objJobs.LibraryJobIndividualDetails(Convert.ToInt32(Request.QueryString["JobID"].ToString()), UDFLib.ConvertIntegerToNull(Request.QueryString["JobHistoryID"].ToString()), Request.QueryString["Qflag"].ToString(), Convert.ToInt32(Request.QueryString["System_ID"].ToString().Split(',')[0]), Convert.ToInt32(Request.QueryString["SubSystem_ID"].ToString().Split(',')[0]));

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataRow dr = ds.Tables[0].Rows[0];

                    txtJobCode.Text = dr["JobCode"].ToString();
                    txtVessel.Text = dr["Vessel_Name"].ToString();
                    // txtMachinery.Text = dr["Machinery"].ToString();

                    txtFun.Text = dr["FunctionName"].ToString();
                    txtLocation.Text = dr["Location"].ToString();
                    txtSubsystemLoc.Text = dr["SubSysLocation"].ToString();
                    txtJobTitle.Text = dr["Job_Title"].ToString();
                    txtJobDescription.Text = dr["Job_Description"].ToString();
                    txtDepartment.Text = dr["DepartmentName"].ToString();
                    txtRank.Text = dr["RankShortName"].ToString();
                    txtcms.Text = dr["CMS"].ToString();

                    if (dr["CMS"].ToString() == "Y")
                        txtcms.BackColor = System.Drawing.Color.RosyBrown;

                    if (dr["CRITICAL"].ToString() == "Y")
                        txtCritical.BackColor = System.Drawing.Color.RosyBrown;


                    txtCritical.Text = dr["CRITICAL"].ToString();
                    txtFrequencyName.Text = dr["FrequencyName"].ToString();
                    txtFrequencyType.Text = dr["Frequency"].ToString();

                }
            }
            if (ds != null)
            {

                if (ds.Tables[1].Rows.Count > 0)
                {

                    DataRow dr = ds.Tables[1].Rows[0];
                    txtLocation.Text = dr["Location"].ToString();
                    txtSubsystemLoc.Text = dr["SubSysLocation"].ToString();
                    txtDateOriginallyDue.Text = dr["DateOriginallyDue"].ToString();
                    txtDateNextDue.Text = dr["DateNextDue"].ToString();
                    txtDateJobDone.Text = dr["DateJobDone"].ToString();
                    txtRhrsWhenJobDone.Text = dr["RhrsWhenJobDone"].ToString();
                    txtJobRemark.Text = dr["History"].ToString();

                    lbnCreatedBy.Text = dr["Created_By_Name"].ToString();
                    txtCreatedOn.Text = dr["Created_On"].ToString();
                    lbnVerifiedBy.Text = dr["Verified_By_Name"].ToString();
                    txtVerifiedOn.Text = dr["Verified_On"].ToString();
                    ViewState["CreatedByID"] = dr["CreatedByID"].ToString();
                    ViewState["VerifiedByID"] = dr["VerifierID"].ToString();

                    lbnRequisition.Text = dr["REQUISITION_CODE"].ToString();

                    ViewState["DOCUMENT_CODE"] = dr["DOCUMENT_CODE"].ToString();
                    ViewState["REQUISITION_CODE"] = dr["REQUISITION_CODE"].ToString();
                    ViewState["DEPARTMENT"] = dr["DEPARTMENT"].ToString();


                }
            }

            tdg.Visible = false;

            if (ds != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {


                    DataView dvImage = ds.Tables[2].DefaultView;
                    dvImage.RowFilter = "Is_Image='1' ";

                    ListView1.DataSource = dvImage;
                    ListView1.DataBind();
                    ListView2.DataSource = dvImage;
                    ListView2.DataBind();
                    hidenTotalrecords.Value = dvImage.Count.ToString();
                    HCurrentIndex.Value = "0";
                    if (dvImage.Count == 0)
                    {
                        tdg.Visible = false;
                    }
                    else
                    {
                        tdg.Visible = true;
                    }

                    ds.Tables[2].DefaultView.RowFilter = "Is_Image='0'  ";
                    rptDrillImages.DataSource = ds.Tables[2].DefaultView;
                    rptDrillImages.DataBind();


                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// Binding job attachment
    /// </summary>
    /// <param name="JOB_ID">Job Id </param>
    /// <param name="VESSEL_ID">Vessel ID</param>

    protected void BindPmsJobAttachment()
    {
        try
        {
            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
            DataTable dt = objjobs.LibraryGetJobInstructionAttachment(UDFLib.ConvertToInteger(Request.QueryString["VID"]), UDFLib.ConvertToInteger(Request.QueryString["JobID"].ToString()));

            gvPMSJobAttachment.DataSource = dt;
            gvPMSJobAttachment.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    protected void lbnCreatedBy_Click(object sender, EventArgs e)
    {

        ResponseHelper.Redirect("/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Crew/CrewDetails.aspx?ID=" + ViewState["CreatedByID"].ToString(), "_blank", "");
    }

    protected void lbnVerifiedBy_Click(object sender, EventArgs e)
    {
        ResponseHelper.Redirect("/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Crew/CrewDetails.aspx?ID=" + ViewState["VerifiedByID"].ToString(), "_blank", "");
    }

    protected void lbnRequisition_Click(object sender, EventArgs e)
    {

        ResponseHelper.Redirect("/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/purchase/RequisitionSummary.aspx?REQUISITION_CODE=" + ViewState["REQUISITION_CODE"].ToString() + "&Document_Code=" + ViewState["DOCUMENT_CODE"].ToString() + "&Vessel_Code=" + VID + "&Dept_Code=" + ViewState["DEPARTMENT"].ToString(), "_blank", "");


    }

}