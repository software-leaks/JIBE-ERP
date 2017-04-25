using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class SCM_Report_Details : System.Web.UI.Page
{
    public MergeGridviewHeader_Info mergeheadgvSfvs = new MergeGridviewHeader_Info();
    public MergeGridviewHeader_Info mergeheadgvSwpt = new MergeGridviewHeader_Info();
    public MergeGridviewHeader_Info mergeheadgvOrtg = new MergeGridviewHeader_Info();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            string id = Request.QueryString["SCMID"];
            string id1 = Request.QueryString["SCMID"].ToString();

            string tabid = TabSCM.ActiveTab.ID;
            string tabindex = TabSCM.ActiveTab.TabIndex.ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["SCMID"].ToString()))
            {
                BindSCMList();
            }
            UserAccessValidation();
            BindSCMTabs();

            
        }

        mergeheadgvSfvs.AddMergedColumns(new int[] { 3, 4, 5 }, "Attendance", "HeaderStyle-css");
        mergeheadgvSwpt.AddMergedColumns(new int[] { 3, 4, 5 }, "Attendance", "HeaderStyle-css");
        mergeheadgvOrtg.AddMergedColumns(new int[] { 3, 4, 5 }, "Attendance", "HeaderStyle-css");
    }

    private int counter = 0;
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Approve == 0)
        {
            btnVerify.Visible = false;
            lblVerificationDate.Visible = false;
            lblVerificationDateH.Visible = false;
            lblVerifiedBy.Visible = false;
            lblVerifiedByH.Visible = false;
            lblComment.Visible = false;
            btnVerify.Visible = false;
            txtMessage.Visible = false;

        }
        ViewState["del"] = objUA.Delete;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected string GetRowColor()
    {
        if (counter++ % 2 == 0)
            return "white";
        else
            return "#f9f9fa";
    }

    protected void BindSCMTabs()
    {

        int p = 1;



        if (TabSCM.ActiveTab.TabIndex != 10)
        {

            DataSet ds = BLL_SCM_Report.SCMReportDetailsSearch(Convert.ToInt32(Request.QueryString["SCMID"]), TabSCM.ActiveTab.TabIndex, Convert.ToInt32(Request.QueryString["VesselID"]));
            if (ds.Tables[ds.Tables.Count - 1].Rows[0]["VerificationComment"].ToString().Length > 0)
            {
                txtMessage.Text = ds.Tables[ds.Tables.Count - 1].Rows[0]["VerificationComment"].ToString();
                txtMessage.ReadOnly = true;
                btnVerify.Visible = false;

                lblVerifiedBy.Text = ds.Tables[ds.Tables.Count - 1].Rows[0]["VerifiedByName"].ToString(); ;
                lblVerificationDate.Text = Convert.ToDateTime(ds.Tables[ds.Tables.Count - 1].Rows[0]["VerificationDate"].ToString()).ToString("dd/MMM/yyyy");
                lblVerificationDate.Visible = true;
                lblVerificationDateH.Visible = true;
                lblVerifiedBy.Visible = true;
                lblVerifiedByH.Visible = true;
                txtMessage.Visible = true;
                lblComment.Visible = true;
            }
            else
            {

                if (objUA.Approve != 0)
                {
                    txtMessage.Text = "";
                    btnVerify.Visible = true;

                    txtMessage.Visible = true;
                    lblVerificationDate.Visible = false;
                    lblVerificationDateH.Visible = false;
                    lblVerifiedBy.Visible = false;
                    lblVerifiedByH.Visible = false;
                }


            }

            switch (TabSCM.ActiveTab.TabIndex)
            {
                case 0:

                    gvInjd.DataSource = ds.Tables[0];
                    gvInjd.DataBind();

                    gvInjdLst.DataSource = ds.Tables[1];
                    gvInjdLst.DataBind();

                    break;
                case 1:

                    gvEmdrl.DataSource = ds.Tables[0];
                    gvEmdrl.DataBind();


                    gvEmdrlntdone.DataSource = ds.Tables[1];
                    gvEmdrlntdone.DataBind();

                    break;

                case 2:
                    gvSfvs.DataSource = ds.Tables[0];
                    gvSfvs.DataBind();

                    break;

                case 3:
                    gvSwpt.DataSource = ds.Tables[0];
                    gvSwpt.DataBind();

                    break;

                case 4:
                    gvOrtg.DataSource = ds.Tables[0];
                    gvOrtg.DataBind();

                    break;

                case 5:


                    break;

                case 6:
                    gvMetm.DataSource = ds.Tables[0];
                    gvMetm.DataBind();

                    break;

                case 7:
                    gvAbsn.DataSource = ds.Tables[0];
                    gvAbsn.DataBind();

                    break;

                case 8:
                    gvCncd.DataSource = ds.Tables[0];
                    gvCncd.DataBind();

                    break;

                case 9:
                    gvAttachments.DataSource = ds.Tables[0];
                    gvAttachments.DataBind();

                    break;
                case 12:
                    gvEnvironmental.DataSource = ds.Tables[0];
                    gvEnvironmental.DataBind();
                    dlEnvironmentalAttachments.DataSource = ds.Tables[1];
                    dlEnvironmentalAttachments.DataBind();
                    break;

                case 13:
                    gvHealthNutritionHygiene.DataSource = ds.Tables[0];
                    gvHealthNutritionHygiene.DataBind();
                    dtHealthAttachments.DataSource = ds.Tables[1];
                    dtHealthAttachments.DataBind();
                    break;
                
                default:
                    break;
            }
        }
    }


    protected void BindSCMList()
    {

        DataSet ds = BLL_SCM_Report.SCMReportList(Convert.ToInt32(Request.QueryString["SCMID"]), Convert.ToInt32(Request.QueryString["VesselID"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtVessel.Text = dr["Vessel_Name"].ToString();
            txtVesselPosition.Text = dr["PRESENT_POSITION"].ToString();
            txtMeetingDate.Text = Request.QueryString["MeetingDate"].ToString();

            ViewState["Fleet_ID"] = dr["FleetCode"].ToString();
            ViewState["Vessel_ID"] = dr["Vessel_ID"].ToString();

        }
    }

    protected void TabSCM_ActiveTabChanged(object sender, EventArgs e)
    {

        BindSCMTabs();
    }


    protected void gvMetm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvMetm.PageIndex = e.NewPageIndex;
        gvMetm.DataBind();
    }
      protected void gvAttachments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvAttachments.PageIndex = e.NewPageIndex;
        gvAttachments.DataBind();
    }

    
    
    protected void gvEmdrlntdone_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvEmdrlntdone.PageIndex = e.NewPageIndex;
        gvEmdrlntdone.DataBind();
    }


    protected void gvEmdrl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvEmdrl.PageIndex = e.NewPageIndex;
        gvEmdrl.DataBind();
    }

    protected void gvSfvs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvSfvs.PageIndex = e.NewPageIndex;
        gvSfvs.DataBind();
    }


    protected void gvSwpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvSwpt.PageIndex = e.NewPageIndex;
        gvSwpt.DataBind();
    }



    protected void gvOrtg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvOrtg.PageIndex = e.NewPageIndex;
        gvOrtg.DataBind();
    }


    protected void gvCncd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindSCMTabs();
        gvCncd.PageIndex = e.NewPageIndex;
        gvCncd.DataBind();
    }



    protected void gvSfvs_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {

            MergeGridviewHeader.SetProperty(mergeheadgvSfvs);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }
    }


    protected void gvSwpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(mergeheadgvSwpt);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
    }


    protected void gvOrtg_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(mergeheadgvOrtg);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }

    }



    protected void gvEmdrl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFilePathIfSingle = (Label)e.Row.FindControl("lblFilePathIfSingle");
            ImageButton ImgSCMAtt = (ImageButton)e.Row.FindControl("ImgSCMAtt");

            if (lblFilePathIfSingle.Text != "")
            {
                ImgSCMAtt.Attributes.Add("onclick", "DocOpen('" + lblFilePathIfSingle.Text + "'); return false;");
            }
            else
            {
                ImgSCMAtt.Attributes.Add("onclick", "javascript:window.open('../SCM/SCM_Drill_Images_Details.aspx?DRILLID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&Vessel_ID=" + DataBinder.Eval(e.Row.DataItem, "VESSEL_ID").ToString() + "'); return false;");
            }
        }
    }


    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string js;
        try
        {
            if (txtMessage.Text.Trim().Length == 0)
            {
                js = "alert('Verification comment is mandatory!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
                return;
            }


            int SCMID = Convert.ToInt32(Request.QueryString["SCMID"]);
            int VESSEL_ID = Convert.ToInt32(Request.QueryString["VesselID"]);
            int VerifiedBy = int.Parse(Session["USERID"].ToString());
            BLL_SCM_Report.ScmVerifyReport(SCMID, txtMessage.Text, VESSEL_ID, VerifiedBy);
            //btnVerify.Enabled = false;
            txtMessage.ReadOnly = true;
            btnVerify.Visible = false;
            lblVerificationDate.Visible = true;
            lblVerificationDateH.Visible = true;
            lblVerifiedBy.Visible = true;
            lblVerifiedByH.Visible = true;
            lblVerifiedBy.Text = Session["USERFULLNAME"].ToString();
            lblVerificationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            //LoadFollowUps(iJob_OfficeID, VESSEL_ID, Worklist_ID);
        }
        catch (Exception ex)
        {
            js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
       DataSet ds = BLL_SCM_Report.SCMReportDetailsSearch(Convert.ToInt32(Request.QueryString["SCMID"]), TabSCM.ActiveTab.TabIndex, Convert.ToInt32(Request.QueryString["VesselID"]));

        string[] HeaderCaptions = { "Staff Code","Staff Name","Staff Rank","Sigature"};
        string[] DataColumnsName = { "STAFF_CODE", "Staff_Name", "Rank_Short_Name", "Sigature" };
        ds.Tables[0].Columns.Add("Sigature");
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            item["Sigature"] = "";
        }
        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "SCM", "Safety Committee Meeting Attendance");
    }  
}
