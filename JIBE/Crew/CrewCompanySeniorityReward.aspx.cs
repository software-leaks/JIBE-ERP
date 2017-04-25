using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.IO;
using AjaxControlToolkit4;
public partial class Crew_CrewCompanySeniorityReward : System.Web.UI.Page
{
    BLL_Crew_Seniority objBLLCrewSeniority = new BLL_Crew_Seniority();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ucCustomPager.PageSize = 20;
            if (Request.QueryString["Staff_Code"] != null && Request.QueryString["Staff_Code"].ToString() != "")
            {
                txtSearchText.Text = Request.QueryString["Staff_Code"].ToString();
            }           
            Load_SeniorityYear();
            if (Request.QueryString["SeniorityYear"] != null && Request.QueryString["SeniorityYear"].ToString() != "")
            {
                ddlSeniorityYear.SelectedValue = Request.QueryString["SeniorityYear"].ToString();
            }
            Load_SeniorityRecords();
        }

        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);   
    }
    public void Load_SeniorityYear()
    {
        DataTable dt = objBLLCrewSeniority.Get_SeniorityYearRewardList();

        ddlSeniorityYear.DataSource = dt;
        ddlSeniorityYear.DataTextField = "SeniorityYear";
        ddlSeniorityYear.DataValueField = "SeniorityYear";
        ddlSeniorityYear.DataBind();
        ddlSeniorityYear.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlSeniorityYear.SelectedIndex = 0;
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {

        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {
            gvSeniorityRecords.Columns[gvSeniorityRecords.Columns.Count - 3].Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            gvSeniorityRecords.Columns[gvSeniorityRecords.Columns.Count - 2].Visible = false;

        }


    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }
    protected void Load_SeniorityRecords()
    {

        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

         int CompanySeniorityYear = int.Parse(ddlSeniorityYear.SelectedValue.ToString());
        string RewardStatus = rblRewardStatus.SelectedValue.ToString(); 
        DataTable dt = objBLLCrewSeniority.Get_CrewSeniorityRewardList(CompanySeniorityYear,RewardStatus, txtSearchText.Text.Trim(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
        gvSeniorityRecords.DataSource = dt;
        gvSeniorityRecords.DataBind();


        if (ucCustomPager.isCountRecord == 1)
        {
            ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager.BuildPager();
        }


    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        // User can save file to File System, database or in session state
        Dictionary<string, Byte[]> Files = new Dictionary<string, byte[]>();
        if (file != null)
        {
            Byte[] fileBytes = file.GetContents();
            string FileName = file.FileName;
            int SIZE_BYTES = fileBytes.Length;

            Guid GUID = Guid.NewGuid();
            string FilPath = Path.Combine(Server.MapPath("~/Uploads/CrewRewardDocument/"), GUID.ToString() + Path.GetExtension(FileName));
            string Attach_Name = Path.GetFileName(FileName);
            string Attach_Path = GUID.ToString() + Path.GetExtension(FileName);

            FileStream fileStream = new FileStream(FilPath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

            Session["FileName"] = Attach_Name;
            Session["FilPath"] = Attach_Path;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        int CrewId = int.Parse(txtCrewId.Text.ToString());
        int RankId  = int.Parse(txtRankId.Text.ToString());
        int SeniorityYear = int.Parse(txtSeniorityYear.Text.ToString());
        string FileName = "", FilPath="";
        if (Session["FileName"] != null)
            FileName = Session["FileName"].ToString();
        if (Session["FilPath"] != null)
            FilPath = Session["FilPath"].ToString();

        int retVal = objBLLCrewSeniority.Insert_CrewSeniorityRewardDetails(CrewId, RankId, SeniorityYear, txtRemarks.Text.Trim(), FileName,FilPath, GetSessionUserID());
        string js = "alert('Seniority Reward Updated Successfully');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);

        string msgDivResponseHide = string.Format("hideModal('divSeniorityReward');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDivResponseHide", msgDivResponseHide, true);

        Load_SeniorityRecords();
   }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        int SeniorityYear = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.NewEditIndex].Values[0].ToString());
        int CrewID = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.NewEditIndex].Values[1].ToString());
        string Rank = gvSeniorityRecords.DataKeys[e.NewEditIndex].Values[2].ToString();
        int RankID = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.NewEditIndex].Values[3].ToString());
        int CrewSeniorityRewardId = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.NewEditIndex].Values[4].ToString());
        DataTable dt = objBLLCrewSeniority.Get_CrewSeniorityRewardDetails(CrewID, SeniorityYear);
        if (dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["Staff_FullName"].ToString();
            txtCrewId.Text = CrewID.ToString();
            txtRankId.Text = RankID.ToString();
            txtRank.Text = Rank;
            txtSeniorityYear.Text = SeniorityYear.ToString();
            if (dt.Rows[0]["Remarks"] != null || dt.Rows[0]["Remarks"] != "")
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            else
                txtRemarks.Text = "";
            if (dt.Rows[0]["AttachmentFilePath"] != null && dt.Rows[0]["AttachmentFilePath"].ToString() != "")
            {
                lnkAttachment.Text = dt.Rows[0]["AttachmentFileName"].ToString();
                lnkAttachment.NavigateUrl = "~/Uploads/CrewRewardDocument/" + dt.Rows[0]["AttachmentFilePath"].ToString();
            }
            else
            {
                lnkAttachment.Text = "";
            }
        }
        if (CrewSeniorityRewardId > 0)
        {
            btnSave.Enabled = false;
            AttachmentUploader.Visible = false;
        }
        else
        {
            btnSave.Enabled = true;
            AttachmentUploader.Visible = true;
        }
        string msgdivResponseShow = string.Format("showModal('divSeniorityReward',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

       UpdatePnl.Update();
    }
  
    protected void gvSeniorityRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSeniorityRecords.PageIndex = e.NewPageIndex;
        Load_SeniorityRecords();

    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }

    protected void gvSeniorityRecords_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSeniorityRecords.EditIndex = e.NewEditIndex;
        Load_SeniorityRecords();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ddlSeniorityYear.SelectedIndex = 0;
        rblRewardStatus.SelectedValue = "All";
        Load_SeniorityRecords();
    }
 }