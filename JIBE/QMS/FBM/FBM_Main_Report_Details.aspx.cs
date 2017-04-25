using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;
using System.IO;
using SMS.Business.QMS;
using System.Configuration;
using SMS.Properties;

public partial class QMS_FBM_FBM_Main_Report_Details : System.Web.UI.Page
{

    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {


        txtMailBody.config.toolbar = new object[]
            {                
                new object[] { "Bold", "Italic", "Underline", "", "" },
                new object[] { "NumberedList", "BulletedList"},
            };

        txtMailBody.Height = 500;
        txtMailBody.BackColor = System.Drawing.Color.LightYellow;
        txtMailBody.BodyClass = "cke_show_borders";
        txtMailBody.Width = 1090;


        if (!IsPostBack)
        {

            DataTable dtFile = new DataTable();
            dtFile.Columns.Add("FileGuid");
            dtFile.Columns.Add("FileName");
            dtFile.Columns.Add("FileExtn");
            ViewState["vdtFile"] = dtFile;

            Get_UserDetails();
            FillDDLOfficeDept();
            FillDDLPrimaryCategory();

            FillDLLFBMApprovalList();

            btnInActive.Enabled = false;
            btnApproved.Enabled = false;
            btnRework.Enabled = false;


            divSendApproval.Visible = false;

            if (Request.QueryString["FBMID"] != null)
            {
                BindFBMReportsDetails(Convert.ToInt32(Request.QueryString["FBMID"].ToString()), false);
                BindFBMAttachment(Convert.ToInt32(Request.QueryString["FBMID"].ToString()));
            }

            if (Request.QueryString["FBMREQUESTID"] != null)
            {
                BindFBMReportsDetails(Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString()), true);
                BindFBMAttachment(Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString()));
            }

            if (ViewState["DeptID"].ToString() != "")
                DDLOfficeDept.SelectedValue = ViewState["DeptID"].ToString();




        }

        UserAccessValidation();
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
            btnSent.Enabled = false;
            btnDraft.Enabled = false;
        }

        if (objUA.Approve == 0)
        {
            btnInActive.Enabled = false;
            btnApproved.Enabled = false;
            btnRework.Enabled = false;
        }


    }



    public void Get_UserDetails()
    {
        BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();

        DataTable dtuser = new DataTable();

        if (Request.QueryString["UserID"] != null)
            dtuser = ojbInfra.Get_UserDetails(Convert.ToInt32(Request.QueryString["UserID"].ToString()));
        else
            dtuser = ojbInfra.Get_UserDetails(Convert.ToInt32(Session["userid"].ToString()));

        ViewState["DeptID"] = dtuser.Rows[0]["Dep_Code"].ToString();

    }

    public void FillDDLOfficeDept()
    {
        try
        {

            int? companyid = null;
            if ((Session["USERCOMPANYID"].ToString() != "") || (Session["USERCOMPANYID"] == null))
                companyid = Convert.ToInt32(Session["USERCOMPANYID"].ToString());


            DataTable dtOfficeDept = BLL_FBM_Report.GetOfficeDepartment(companyid);
            DDLOfficeDept.Items.Clear();
            DDLOfficeDept.DataSource = dtOfficeDept;
            DDLOfficeDept.DataTextField = "VALUE";
            DDLOfficeDept.DataValueField = "ID";
            DDLOfficeDept.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLOfficeDept.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void FillDDLPrimaryCategory()
    {
        try
        {
            int? deptid = null;
            if (ViewState["DeptID"] != null)
                deptid = Convert.ToInt32(ViewState["DeptID"].ToString());

            DataTable dtPrimaryCategory = BLL_FBM_Report.FBMGetSystemParameterList("1", "", deptid);
            DDLPrimaryCategory.Items.Clear();
            DDLPrimaryCategory.DataSource = dtPrimaryCategory;
            DDLPrimaryCategory.DataTextField = "NAME";
            DDLPrimaryCategory.DataValueField = "CODE";
            DDLPrimaryCategory.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLPrimaryCategory.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void FillDLLFBMApprovalList()
    {
        DataTable dtfbmuserapproval = new DataTable();

        if (Request.QueryString["APPROVARID"] != null)
            dtfbmuserapproval = BLL_FBM_Report.GetFBMUserApprovalList(Convert.ToInt32(Request.QueryString["APPROVARID"].ToString()));
        else
            dtfbmuserapproval = BLL_FBM_Report.GetFBMUserApprovalList(Convert.ToInt32(Session["userid"].ToString()));

        ddlFbmApprover.Items.Clear();

        ddlFbmApprover.DataSource = dtfbmuserapproval;
        ddlFbmApprover.DataTextField = "ApprovalName";
        ddlFbmApprover.DataValueField = "UserID";
        ddlFbmApprover.DataBind();
        ListItem li = new ListItem("-- SELECT --", "0");
        ddlFbmApprover.Items.Insert(0, li);


    }

    protected void DDLPrimaryCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtSecondryCategory = BLL_FBM_Report.FBMGetSystemParameterList(DDLPrimaryCategory.SelectedValue.ToString(), "", null);
        DDLSecondryCategory.Items.Clear();
        DDLSecondryCategory.DataSource = dtSecondryCategory;
        DDLSecondryCategory.DataTextField = "NAME";
        DDLSecondryCategory.DataValueField = "CODE";
        DDLSecondryCategory.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLSecondryCategory.Items.Insert(0, li);
    }

    protected void BindFBMReportsDetails(int fbmid, bool approver)
    {
        DataSet ds = BLL_FBM_Report.FBMReportList(fbmid);

        if (ds.Tables[0].Rows.Count > 0)
        {

            DataRow dr = ds.Tables[0].Rows[0];

            DDLOfficeDept.SelectedValue = dr["DEPARTMENT"].ToString() != "" ? dr["DEPARTMENT"].ToString() : "0";
            DDLPrimaryCategory.SelectedValue = dr["PRIMARY_CATEGORY"].ToString() != "" ? dr["PRIMARY_CATEGORY"].ToString() : "0";

            DDLPrimaryCategory_SelectedIndexChanged(null, null);

            DDLSecondryCategory.SelectedValue = dr["SECONDRY_CATEGORY"].ToString();

            optForUser.SelectedValue = dr["FOR_USER"].ToString();
            optPriority.SelectedValue = dr["URGENT"].ToString();
            txtSubject.Text = dr["SUBJECT"].ToString();
            txtMailBody.Text = dr["BODY"].ToString();
            //txtMailBody.Text = dr["BODY"].ToString();
            txtFBMNumber.Text = dr["FBM_NUMBER"].ToString();
            txtDateSent.Text = dr["DATE_SENT"].ToString();

            ViewState["FBM_NUMBER"] = dr["FBM_NUMBER"].ToString();
            ViewState["DATE_SENT"] = dr["DATE_SENT"].ToString();
            ViewState["ACTIVE"] = dr["ACTIVE"].ToString();
            ViewState["FBM_STATUS"] = dr["FBM_STATUS"].ToString();
            ViewState["APPROVED_BY"] = dr["APPROVED_BY"].ToString();
            ViewState["APPROVED_ON"] = dr["APPROVED_ON"].ToString();
            ViewState["MADE_INACTIVE_ON"] = dr["MADE_INACTIVE_ON"].ToString();

            ViewState["FBM_CREATED_BY"] = dr["CREATED_BY"].ToString();


            if (ViewState["ACTIVE"].ToString() == "N")
            {
                btnInActive.Text = "Make Active";
            }


            if (ViewState["FBM_STATUS"].ToString() == "SENT")
            {
                btnInActive.Enabled = true;


                btnDraft.Enabled = false;
                btnSent.Enabled = false;
                btnApproved.Enabled = false;
                btnRework.Enabled = false;


                ImgTempFBMAttDelete.Enabled = false;
                DDLPrimaryCategory.Enabled = false;
                DDLSecondryCategory.Enabled = false;
                optForUser.Enabled = false;
                optPriority.Enabled = false;
                txtSubject.ReadOnly = true;

                txtMailBody.ReadOnly = true;
                lstfbmAttachment.Enabled = false;
            }

            if (ViewState["FBM_STATUS"].ToString() == "PENDINGAPPROVAL")
            {

                if (ViewState["APPROVED_BY"].ToString().Trim() == Session["userid"].ToString().Trim())
                {
                    btnApproved.Enabled = true;
                    btnRework.Enabled = true;
                }
                else
                {
                    btnApproved.Enabled = false;
                    btnRework.Enabled = false;
                }

                btnSent.Enabled = false;
                btnDraft.Enabled = false;

                ImgTempFBMAttDelete.Enabled = false;
                DDLPrimaryCategory.Enabled = false;
                DDLSecondryCategory.Enabled = false;
                optForUser.Enabled = false;
                optPriority.Enabled = false;
                txtSubject.ReadOnly = true;
                txtMailBody.ReadOnly = true;
                lstfbmAttachment.Enabled = false;
            }

            if (ViewState["FBM_STATUS"].ToString() == "DRAFT")
            {
                btnDraft.Enabled = true;
                btnSent.Enabled = true;
                btnApproved.Enabled = false;
                btnRework.Enabled = false;
            }

            if (ViewState["FBM_STATUS"].ToString() == "REWORK")
            {
                btnDraft.Enabled = true;
                btnSent.Enabled = true;
                btnApproved.Enabled = false;
                btnRework.Enabled = false;
            }




        }

        if (approver)
        {

            if (ViewState["FBM_STATUS"].ToString() == "PENDINGAPPROVAL")
            {
                btnDraft.Enabled = false;
                btnSent.Enabled = false;

                btnApproved.Enabled = true;
                btnRework.Enabled = true;

                btnSent.Enabled = false;
                btnDraft.Enabled = false;
                ImgTempFBMAttDelete.Enabled = false;
                DDLPrimaryCategory.Enabled = false;
                DDLSecondryCategory.Enabled = false;
                optForUser.Enabled = false;
                optPriority.Enabled = false;
                txtSubject.ReadOnly = true;
                txtMailBody.ReadOnly = true;
                lstfbmAttachment.Enabled = false;
            }
        }

    }

    protected void EnableDisableControl(Boolean btnbool)
    {





    }

    protected void BindFBMAttachment(int fbmid)
    {
        DataTable dt = BLL_FBM_Report.FBMAttachmentSearch(fbmid);
        gvFBMAtt.DataSource = dt;
        gvFBMAtt.DataBind();
    }

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtMailBody.Text.Trim() != "")
            {
                int fbmidout = 0;
                int? fbmid = null;
                if (Request.QueryString["FBMID"] != null)
                    fbmid = Convert.ToInt32(Request.QueryString["FBMID"].ToString());

                int retval = BLL_FBM_Report.FBMMessageSave(fbmid, Convert.ToInt32(Session["userid"].ToString()), txtSubject.Text
                   , UDFLib.ConvertIntegerToNull(DDLOfficeDept.SelectedValue), optForUser.SelectedValue.ToString().Trim()
                   , txtMailBody.Text, 0, 1, UDFLib.ConvertIntegerToNull(DDLPrimaryCategory.SelectedValue)
                   , UDFLib.ConvertIntegerToNull(DDLSecondryCategory.SelectedValue)
                   , Convert.ToInt32(optPriority.SelectedValue), "DRAFT", ref fbmidout);

                SaveFBMAttachment(fbmidout, false);

                if (Request.QueryString["FBMID"] != null)
                {
                    BindFBMAttachment(Convert.ToInt32(Request.QueryString["FBMID"].ToString()));
                }

                String script = String.Format("alert('FBM has been Save as Draft.');window.opener.location.reload(true);window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
            }
            else
            {
                String script = String.Format("alert('e-Mail Body is required.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bodymsgdraft", script, true);
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnSent_Click(object sender, EventArgs e)
    {
        if (txtMailBody.Text.Trim() != "")
        {
            divSendApproval.Visible = true;
        }
        else
        {
            String script = String.Format("alert('e-Mail Body is required.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "bodymsgSent", script, true);
        }
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["FBMREQUESTID"] == null)
        {

            int retval = BLL_FBM_Report.FBMMessageApproved(Convert.ToInt32(Request.QueryString["FBMID"].ToString()), Convert.ToInt32(Session["userid"].ToString()), "SENT");

            SendEmail(Convert.ToInt32(Request.QueryString["FBMID"].ToString()));

            String script = String.Format("alert('FBM has been approved.');window.opener.location.reload(true);window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

        }
        else
        {

            int retval = BLL_FBM_Report.FBMMessageApproved(Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString()), Convert.ToInt32(Request.QueryString["APPROVARID"].ToString()), "SENT");

            SendEmail(Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString()));

            String script = String.Format("alert('FBM has been approved.');window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
        }

    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {

        int? fbmid = null;
        if (Request.QueryString["FBMREQUESTID"] != null)
            fbmid = Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString());
        else if (Request.QueryString["FBMID"] != null)
            fbmid = Convert.ToInt32(Request.QueryString["FBMID"].ToString());

        int retval = BLL_FBM_Report.FBMMessageInActive(Convert.ToInt32(Session["userid"].ToString()), fbmid);

        //String script = String.Format("alert('FBM has been InActiveted.');window.opener.location.reload(true);window.close();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

        String script = String.Format("window.opener.location.reload(true);window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdfd", script, true);

    }

    protected void btnRework_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["FBMREQUESTID"] == null)
        {
            int retval = BLL_FBM_Report.FBMMessageRework(Convert.ToInt32(Request.QueryString["FBMID"].ToString()), "REWORK");

            SendEmailForRework(Convert.ToInt32(Request.QueryString["FBMID"].ToString()), Convert.ToInt32(ViewState["FBM_CREATED_BY"].ToString())
                , Convert.ToInt32(Session["userid"].ToString()));

            String script = String.Format("alert('FBM has been send for Rework.');window.opener.location.reload(true);window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

        }
        else
        {

            int retval = BLL_FBM_Report.FBMMessageRework(Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString()), "REWORK");

            SendEmailForRework(Convert.ToInt32(Request.QueryString["FBMREQUESTID"].ToString()), Convert.ToInt32(Request.QueryString["REQUESTERID"].ToString())
                , Convert.ToInt32(Request.QueryString["APPROVARID"].ToString()));

            String script = String.Format("alert('FBM has been send for Rework.');window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
        }

    }

    protected void btnDivSendApproval_click(object sender, EventArgs e)
    {
        try
        {

            int fbmidout = 0;
            int? fbmid = null;
            if (Request.QueryString["FBMID"] != null)
                fbmid = Convert.ToInt32(Request.QueryString["FBMID"].ToString());

            int requesterid = 0;
            if (Request.QueryString["UserID"] != null)
                requesterid = Convert.ToInt32(Request.QueryString["UserID"].ToString());
            else
                requesterid = Convert.ToInt32(Session["userid"].ToString());


            int retval = BLL_FBM_Report.FBMMessageSent(fbmid, Convert.ToInt32(Session["userid"].ToString()), txtSubject.Text
                    , UDFLib.ConvertIntegerToNull(DDLOfficeDept.SelectedValue), optForUser.SelectedValue.ToString().Trim()
                    , txtMailBody.Text, 1, 1, UDFLib.ConvertIntegerToNull(DDLPrimaryCategory.SelectedValue)
                    , UDFLib.ConvertIntegerToNull(DDLSecondryCategory.SelectedValue)
                    , Convert.ToInt32(optPriority.SelectedValue), "PENDINGAPPROVAL", Convert.ToInt32(ddlFbmApprover.SelectedValue), ref fbmidout);

            SaveFBMAttachment(fbmidout, true);

            SendEmailToApprovar(fbmidout, requesterid, Convert.ToInt32(ddlFbmApprover.SelectedValue));

            String script = String.Format("alert('FBM has been sent to for approval.');window.opener.location.reload(true);window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

        }
        catch (Exception ex)
        {

        }

    }

    protected void btnDivCancel_click(object sender, EventArgs e)
    {
        ddlFbmApprover.SelectedValue = "0";
        divSendApproval.Visible = false;

    }

    protected void SaveFBMAttachment(int fbmid, bool SyncAttachment)
    {

        string sourchfilepath = "";
        string destinationpath = "";

        DataTable dt = new DataTable();


        if (Request.QueryString["FBMID"] != null)
        {
            dt = BLL_FBM_Report.FBMAttachmentSearch(Convert.ToInt32(Request.QueryString["FBMID"].ToString()));
        }


        DataTable vdtFile = (DataTable)ViewState["vdtFile"];

        foreach (DataRow dr in vdtFile.Rows)
        {

            sourchfilepath = Server.MapPath(@"~/QMS\\TempUpload\\");
            sourchfilepath += dr["FileGuid"].ToString() + dr["FileExtn"].ToString();

            destinationpath = Server.MapPath(@"~/Uploads\\FBM\\");
            destinationpath += dr["FileGuid"].ToString() + dr["FileExtn"].ToString();


            BLL_FBM_Report.FBMAttachmentSave(Convert.ToInt32(Session["userid"].ToString()), fbmid, dr["FileName"].ToString(), dr["FileGuid"].ToString() + dr["FileExtn"].ToString());

            if (!File.Exists(destinationpath))
                File.Copy(sourchfilepath, destinationpath);

        }

    }

    protected void ImgFBMAttDelete_Click(object sender, CommandEventArgs e)
    {

        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string fileid = cmdargs[0].ToString();
        string fileName = cmdargs[1].ToString();

        string filepath = "../../uploads/fbm/" + fileName;


        int retval = BLL_FBM_Report.FBMAttachmentDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(fileid));

        if (File.Exists(Server.MapPath(filepath)))
            File.Delete(Server.MapPath(filepath));

        if (Request.QueryString["FBMID"] != null)
        {

            BindFBMAttachment(Convert.ToInt32(Request.QueryString["FBMID"].ToString()));
        }

        if (Request.QueryString["FBMREQUESTID"] != null)
        {

            BindFBMAttachment(Convert.ToInt32(Request.QueryString["FBMID"].ToString()));
        }




    }

    protected void ImgTempFBMAttDelete_click(object sender, ImageClickEventArgs e)
    {
        int slindex = lstfbmAttachment.SelectedIndex;

        if (lstfbmAttachment.SelectedIndex != -1)
        {
            lstfbmAttachment.Items.RemoveAt(lstfbmAttachment.SelectedIndex);
            DataTable tempfiledt = (DataTable)ViewState["vdtFile"];

            if (tempfiledt.Rows.Count > 0)
            {
                tempfiledt.Rows[slindex].Delete();
            }
            ViewState["vdtFile"] = tempfiledt;
        }
    }

    protected void SendEmailForRework(int fbmid, int requesterid, int approverid)
    {


        int crewfbmidout = 0;

        BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();
        DataTable dtRequesterDetails = ojbInfra.Get_UserDetails(requesterid);

        DataTable dtapprovalDetails = ojbInfra.Get_UserDetails(approverid);


        DataSet ds = BLL_FBM_Report.FBMReportList(Convert.ToInt32(fbmid));

        StringBuilder sbEmailbody = new StringBuilder();
        string subject = "Rework - FBM Number :" + ds.Tables[0].Rows[0]["FBM_NUMBER"].ToString();

        sbEmailbody.Append("Dear  ");
        sbEmailbody.Append(dtRequesterDetails.Rows[0]["First_Name"].ToString() + ",");
        sbEmailbody.AppendLine("<br><br>");

        sbEmailbody.AppendLine("The FBM Number :" + ds.Tables[0].Rows[0]["FBM_NUMBER"].ToString() + " is sending for Rework.");

        sbEmailbody.AppendLine("<br><br>");
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine("Best Regards,");
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine(dtapprovalDetails.Rows[0]["User_name"].ToString().ToUpper() + " " + dtapprovalDetails.Rows[0]["Last_Name"].ToString().ToUpper());
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine(dtapprovalDetails.Rows[0]["Designation"].ToString());
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine(Convert.ToString(Session["Company_Address_GL"]));
        sbEmailbody.AppendLine("<br>");


        int val = BLL_FBM_Report.FBMCrewMailSave(Convert.ToInt32(Session["userid"].ToString()), subject, dtRequesterDetails.Rows[0]["MailID"].ToString(), "", sbEmailbody.ToString(), ref crewfbmidout);

    }

    protected void SendEmailToApprovar(int fbmid, int requesterid, int approvarid)
    {
        int crewfbmidout = 0;

        BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();
        DataTable dtapprovalDetails = ojbInfra.Get_UserDetails(approvarid);

        DataTable dtRequesterDetails = ojbInfra.Get_UserDetails(requesterid);

        DataSet ds = BLL_FBM_Report.FBMReportList(Convert.ToInt32(fbmid));


        StringBuilder sbEmailbody = new StringBuilder();
        string subject = "Pending for Approval - FBM Number :" + ds.Tables[0].Rows[0]["FBM_NUMBER"].ToString();
        string path = System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/QMS/FBM/FBM_Main_Report_Details.aspx?FBMREQUESTID=" + ds.Tables[0].Rows[0]["ID"].ToString() + "&APPROVARID=" + approvarid + "&REQUESTERID=" + requesterid;

        sbEmailbody.Append("Dear  ");
        sbEmailbody.Append(dtapprovalDetails.Rows[0]["First_Name"].ToString() + ",");
        sbEmailbody.AppendLine("<br><br>");
        sbEmailbody.AppendLine("FBM Number :" + ds.Tables[0].Rows[0]["FBM_NUMBER"].ToString() + " is Pending for your Approval, Click on below link to approve it.");
        sbEmailbody.Append("<a href=" + path + ">" + path + "</a>");
        sbEmailbody.AppendLine("<br><br>");
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine("Best Regards,");
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine(dtRequesterDetails.Rows[0]["User_name"].ToString().ToUpper() + " " + dtRequesterDetails.Rows[0]["Last_Name"].ToString().ToUpper());
        sbEmailbody.AppendLine("<br>");
        sbEmailbody.AppendLine(dtRequesterDetails.Rows[0]["Designation"].ToString());
        sbEmailbody.AppendLine(Convert.ToString(Session["Company_Address_GL"]));
        sbEmailbody.AppendLine("<br>");



        int val = BLL_FBM_Report.FBMCrewMailSave(Convert.ToInt32(Session["userid"].ToString()), subject, dtapprovalDetails.Rows[0]["MailID"].ToString(), "", sbEmailbody.ToString(), ref crewfbmidout);


    }

    protected void SendEmail(int fbmid)
    {

        string ToMail = "";
        int crewfbmidout = 0;

        if (optForUser.SelectedValue == "COMPANY") //company
            ToMail = ConfigurationManager.AppSettings["FBM_TO_COMPANY_EMAIL"].ToString();
        else if (optForUser.SelectedValue == "OFFICE") //office
            ToMail = ConfigurationManager.AppSettings["FBM_TO_OFFICE_EMAIL"].ToString();
        else if (optForUser.SelectedValue == "SHIP") // ship email  
            ToMail = ConfigurationManager.AppSettings["FBM_TO_OFFICE_EMAIL"].ToString();

        // if there is a Attachment is associated with FBM Then Send an Email to IT Department.
        DataTable dtAttachments = BLL_FBM_Report.FBMAttachmentSearch(fbmid);

        DataSet ds = BLL_FBM_Report.FBMReportList(Convert.ToInt32(fbmid));

        // Check the attachment
        if (dtAttachments.Rows.Count > 0)
        {
            // IF USER TYPE IS SHIP THEN EMAI SHOULD GO TO IT DEPT FOR REDUCING THE FILE.

            if (ds.Tables[0].Rows[0]["FOR_USER"].ToString().ToUpper() == "SHIP")
            {
                StringBuilder sbEmailbody = new StringBuilder();
                string subject = "FBM Number :" + ds.Tables[0].Rows[0]["FBM_NUMBER"].ToString();


                string path = System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + @"\uploads\FBM\\";


                sbEmailbody.Append("Dear IT,");
                sbEmailbody.AppendLine("<br><br>");
                sbEmailbody.AppendLine("A FBM has been sent with one or more attachments to it.");
                sbEmailbody.AppendLine("<br><br>");
                sbEmailbody.AppendLine("Arrange to reduce the size of the attachment/s listed below:");
                sbEmailbody.AppendLine("<br><br>");
                foreach (DataRow dr in dtAttachments.Rows)
                {
                    sbEmailbody.Append(dr["FILEPATH"].ToString());
                    sbEmailbody.AppendLine("<br>");
                }
                sbEmailbody.Append("This file is located in folder:   <a href=" + path + ">" + path + "</a>");
                //sbEmailbody.Append("<a href=" + path + ">" + path + "</a>");
                sbEmailbody.AppendLine("<br><br>");
                sbEmailbody.AppendLine("Best Regards,");
                sbEmailbody.AppendLine("<br>");
                sbEmailbody.AppendLine("Jibe AutoMessenger");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    subject = ds.Tables[0].Rows[0]["FBM_NUMBER"].ToString();
                }

                int val = BLL_FBM_Report.FBMCrewMailSave(Convert.ToInt32(Session["userid"].ToString()), subject, ToMail, "", sbEmailbody.ToString(), ref crewfbmidout);

            }
            //IF USER TYPE IS COMPANY OR OFFICE THEN NO ACTION AS PER SATVINDER SIR.
            else
            {

                //string filepathSave = @"\\server01\uploads\FBM\";
                //int val = BLL_FBM_Report.FBMCrewMailSave(Convert.ToInt32(Session["userid"].ToString()), txtSubject.Text, ToMail, "", txtMailBody.Text, ref crewfbmidout);
                //foreach (DataRow dr in dtAttachments.Rows)
                //{
                //    BLL_FBM_Report.FBMCrewAttachmentSave(Convert.ToInt32(Session["userid"].ToString()), crewfbmidout, dr[2].ToString(), filepathSave + dr[2].ToString());
                //}

            }
        }


    }

    protected void gvFBMAtt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().Equals("DELETE"))
        {
            GridView _gridView = (GridView)sender;
            int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());
            string fbmattid = ((Label)gvFBMAtt.Rows[nCurrentRow].FindControl("lblFileId")).Text;


            if (fbmattid.ToString() == "")
            {
                DataTable dt = (DataTable)ViewState["TempDtLocation"];
                dt.Rows[nCurrentRow].Delete();
                dt.AcceptChanges();
            }
            else
            {
                int retval = BLL_FBM_Report.FBMAttachmentDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(fbmattid));

            }

        }
    }

    protected void gvFBMAtt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgFBMAttDelete = (ImageButton)e.Row.FindControl("ImgFBMAttDelete");


            if (ViewState["FBM_STATUS"].ToString() == "DRAFT" || ViewState["FBM_STATUS"].ToString() == "REWORK")
                ImgFBMAttDelete.Visible = true;
            else
                ImgFBMAttDelete.Visible = false;



        }

    }

    //public void OnUploadCompleted(object sender, EventArgs e)
    //{

    //    string[] filearr = Convert.ToString(Session["Attached_Files"]).Split('?');
    //    Session["Attached_Files"] = "";

    //    DataTable dtFile = (DataTable)ViewState["vdtFile"];
    //    DataRow dr;


    //    for (int i = 0; i < filearr.Length - 1; i++)
    //    {
    //        dr = dtFile.NewRow();
    //        dr["FileGuid"] = filearr[i].Split('|')[0];
    //        dr["FileName"] = filearr[i].Split('|')[1]; ;
    //        dr["FileExtn"] = filearr[i].Split('|')[2]; ;
    //        dtFile.Rows.Add(dr);


    //        int pos = lstfbmAttachment.Items.Count == 0 ? 0 : lstfbmAttachment.Items.Count;
    //        lstfbmAttachment.Items.Insert(pos, filearr[i].Split('|')[1]);

    //    }


    //    ViewState["vdtFile"] = dtFile;
    //    UpdlstfbmAttachment.Update();
    //    updAttach.Update();



    //}


    protected void Upload_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("QMS_");
      
        if (dt.Rows.Count > 0)
        {
            string datasize = dt.Rows[0]["Size_KB"].ToString();
            if (FBMFileUploader.HasFile)
            {
                if (FBMFileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    try
                    {

                        if (FBMFileUploader.FileName != "")
                        {
                            string strLocalPath = FBMFileUploader.PostedFile.FileName;
                            string FileName = Path.GetFileName(strLocalPath);
                            string FileExtension = Path.GetExtension(strLocalPath);

                            string FileGuid = "FBM_" + System.Guid.NewGuid().ToString();


                            DataTable dtFile = (DataTable)ViewState["vdtFile"];
                            DataRow dr;
                            dr = dtFile.NewRow();
                            dr["FileGuid"] = FileGuid;
                            dr["FileName"] = FileName;
                            dr["FileExtn"] = FileExtension;
                            dtFile.Rows.Add(dr);

                            ViewState["vdtFile"] = dtFile;

                            FBMFileUploader.PostedFile.SaveAs(Server.MapPath("..\\..\\QMS\\TempUpload\\" + FileGuid + FileExtension));

                            if (!File.Exists(Server.MapPath("..\\..\\QMS\\TempUpload\\" + FileGuid + FileExtension)))
                            {
                                FBMFileUploader.PostedFile.SaveAs(Server.MapPath("..\\..\\QMS\\TempUpload\\" + FileGuid + FileExtension));
                            }


                            int pos = lstfbmAttachment.Items.Count == 0 ? 0 : lstfbmAttachment.Items.Count;
                            lstfbmAttachment.Items.Insert(pos, FileName);

                        }
                     }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "ERROR: " + ex.Message.ToString();
                    }
                }
                else
                {
                    //lblMessage.Text = lblhdn.Text + " KB File size exceeds maximum limit";
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                }
            }
           
        }
       else
        {
            string js2 = "alert('Upload size not set!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }    
    }
  }
