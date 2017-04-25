using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.QMSDB;
using SMS.Business.Infrastructure;
using System.IO;
using System.Configuration;

public partial class ViewProcedures : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblProcedureId.Attributes.Add("style", "visibility:hidden");
        if (!IsPostBack)
        {
            txtProcedureDetails.config.toolbar = new object[] { };
            txtProcedureDetails.BodyClass = "cke_show_borders";
            txtProcedureDetails.Enabled = false;
            txtProcedureDetails.ReadOnly = true;
            txtProcedureDetails.Height = 500;
            txtProcedureDetails.FilebrowserImageUploadUrl = "true";
            txtProcedureDetails.BackColor = System.Drawing.Color.LightYellow;
            txtProcedureDetails.Width = 1200;
            string procedureId = Request.QueryString["PROCEDURE_ID"].ToString();
            BindProcedures(procedureId);
            lblProcedureId.Text = procedureId;
            BindUserList();

        }
    }

    private void BindProcedures(string procedureId)
    {
         DataTable dtProc;
         txtProcedureDetails.ReadOnly =true; 
         if (Request.QueryString["FileStatus"].ToString() == "View")
         {
             dtProc = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(procedureId));
             if (dtProc.Rows.Count > 0)
             {
                 txtProcedureDetails.Text = dtProc.Rows[0]["HeaderDetails"].ToString().Replace("@ProcedureCode", dtProc.Rows[0]["PROCEDURE_CODE"].ToString()).Replace("@publishdate", dtProc.Rows[0]["PUBLISHED_DATE"].ToString()).Replace("@ApproverName", dtProc.Rows[0]["PUBLISHED_BY"].ToString()).Replace("@CreatedBy", dtProc.Rows[0]["CREATED_BY_USER"].ToString()).Replace("@UserName", dtProc.Rows[0]["CREATED_BY_USER"].ToString()).Replace("@FolderName", dtProc.Rows[0]["FOLDER_NAME"].ToString()).Replace("@historiCode", dtProc.Rows[0]["PUBLISH_VERSION"].ToString()) + dtProc.Rows[0]["DETAILS"].ToString();
                 lblProcedureName.Text = dtProc.Rows[0]["PROCEDURE_CODE"].ToString() + " / " + dtProc.Rows[0]["PROCEDURES_NAME"].ToString();
                 pnlCtl.Visible = false; 
             }
         }
         else if (Request.QueryString["FileStatus"].ToString() == "ViewEdit")
         {
             dtProc = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(procedureId));
             if (dtProc.Rows.Count > 0)
             {
                 txtProcedureDetails.Text = dtProc.Rows[0]["HeaderDetails"].ToString().Replace("@ProcedureCode", dtProc.Rows[0]["PROCEDURE_CODE"].ToString()).Replace("@publishdate", dtProc.Rows[0]["PUBLISHED_DATE"].ToString()).Replace("@ApproverName", dtProc.Rows[0]["PUBLISHED_BY"].ToString()).Replace("@CreatedBy", dtProc.Rows[0]["CREATED_BY_USER"].ToString()).Replace("@UserName", dtProc.Rows[0]["CREATED_BY_USER"].ToString()).Replace("@FolderName", dtProc.Rows[0]["FOLDER_NAME"].ToString()).Replace("@historiCode", dtProc.Rows[0]["PUBLISH_VERSION"].ToString()) + dtProc.Rows[0]["CHANGE_DETAILS"].ToString();
                 lblProcedureName.Text = dtProc.Rows[0]["PROCEDURE_CODE"].ToString() + " / " + dtProc.Rows[0]["PROCEDURES_NAME"].ToString();                 


             }
         }
         else if (Request.QueryString["FileStatus"].ToString() == "Compare")
         {
             dtProc = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(procedureId));
             if (dtProc.Rows.Count > 0)
             {
                 Merger a = new Merger(dtProc.Rows[0]["DETAILS"].ToString(), dtProc.Rows[0]["CHANGE_DETAILS"].ToString());

                 lblProcedureName.Text = dtProc.Rows[0]["PROCEDURE_CODE"].ToString()+" / " + dtProc.Rows[0]["PROCEDURES_NAME"].ToString();
                 txtProcedureDetails.Text = a.merge();

             }
         }
    }
    protected void BindUserList()
    {
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
        int UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
        DataTable dtUsers = objInfra.Get_UserList(UserCompanyID);
        lstUser.DataSource = dtUsers;
        lstUser.DataBind();
        lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));

    }
    protected void btnSendTo_Click(object sender, EventArgs e)
    {
        string js = "showModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        string js = "showModal('DivApproval');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);


        //int sts = 0;
        //DataTable dtAttach = BLL_QMSDB_ProcedureSection.Upd_Publish_Procedure(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), Convert.ToInt32(Session["USERID"]), ref sts);
        //if (sts == 1)
        //{
        //    // delete the attachment from folder
        //    foreach (DataRow drAttach in dtAttach.Rows)
        //    {
        //        File.Delete(Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/") + drAttach["ATTACHMENT_NAME"].ToString());
        //    }
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "alert('Published successfully.');window.open('','_self');window.close();", true);
        //}

    }  

    protected void btnCancelApp_Click(object sender, EventArgs e)
    {
        string js = "hideModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        int i = BLL_QMSDB_Procedures.QMSDBProcedures_SendApprovel(int.Parse(lblProcedureId.Text), txtComments.Text, UDFLib.ConvertIntegerToNull(lstUser.SelectedValue), ddlStatus.SelectedValue, int.Parse(Session["USERID"].ToString()));
        string js = "alert('Query Saved !!');hideModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

    }
    protected void BtnSaveApproved_Click(object s, EventArgs e)
    {


        int sts = 0;
        DataTable dtAttach = BLL_QMSDB_ProcedureSection.Upd_Publish_Procedure(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), Convert.ToInt32(Session["USERID"]),txtApprovalComments.Text , ref sts);
        if (sts == 1)
        {
            // delete the attachment from folder
            foreach (DataRow drAttach in dtAttach.Rows)
            {
                File.Delete(Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/") + drAttach["ATTACHMENT_NAME"].ToString());
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "hideModal('DivApproval');alert('Published successfully.');window.open('','_self');window.close();", true);
        }

     

    }
    protected void btnCancelPublish_Click(object sender, EventArgs e)
    {
        string js = "hideModal('DivApproval');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }
}
