using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit4;
using SMS.Business.LMS;
using System.Data;
using System.IO;

public partial class LMS_Chapter_Details : System.Web.UI.Page
{
    public Boolean blnChapterItem = false;
    public Boolean blnTrainer = false;

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillItemType();
            string IsProgram_Scheduled = null;
            Session["vsAttachmentData"] = null;
            Session["vsAttachmentFileName"] = null;
            DataTable dtChapterItemList = new DataTable();
            dtChapterItemList.Columns.Add("ChapterItemId", typeof(int));
            dtChapterItemList.PrimaryKey = new DataColumn[] { dtChapterItemList.Columns["ChapterItemId"] };
            ViewState["dtChapterItemList"] = dtChapterItemList;
            ViewState["Chapter_Id"] = Request.QueryString["Chapter_Id"];
            UserAccessValidation();
            BindChapterItem();
            BindTrainerlist();
            DataTable dt = BLL_LMS_Training.Get_ProgramDescriptionbyId(Convert.ToInt32(Request.QueryString["Program_Id"]));
            if (dt.Rows.Count > 0)
            {
                lblProgramName.Text = dt.Rows[0]["PROGRAM_Name"].ToString();
                IsProgram_Scheduled = dt.Rows[0]["Program_Scheduled"].ToString();
            }

            if (UDFLib.ConvertIntegerToNull(ViewState["Chapter_Id"]) != null)
            {
                txtChapterName.Text = BLL_LMS_Training.Get_ChapterDescriptionbyId(Convert.ToInt32(ViewState["Chapter_Id"]));
                btnDelete.Enabled = true;
            }

            else
            {
                btnDelete.Enabled = false;
            }

            if (IsProgram_Scheduled == "Y")
            {
                txtChapterName.Enabled = false;
                btnDelete.Enabled = false;
                // btnAddNewItem.Enabled=false;
                // gvItemList.Enabled =false;
                gvTrainerRank.Enabled = false;
                //btnsaveandclose.Enabled =false;
                //btnCancel.Enabled = false;
            }
            if (UDFLib.ConvertIntegerToNull(Request.QueryString["ProgramCategory"]) == 4)
            {
                gvTrainerRank.Enabled = false;
            }
        }
    }

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
            btnAddNewItem.Visible = false;

            btnsaveandclose.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            btnAddNewItem.Visible = false;
            btnsaveandclose.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            btnDelete.Visible = false;

        }

    }


    protected void BindChapterItem()
    {
        SaveSelectedChapterItems();

        int is_Fetch_Count = ucCustomPagerChapterDetails.isCountRecord;
        DataSet ds = BLL_LMS_Training.Get_ChapterDetails_List(UDFLib.ConvertIntegerToNull(ViewState["Chapter_Id"]), UDFLib.ConvertStringToNull(txtSearchItemName.Text), UDFLib.ConvertStringToNull(ddlItemType.SelectedIndex == 0 ? null : ddlItemType.SelectedItem.Text), ucCustomPagerChapterDetails.CurrentPageIndex, ucCustomPagerChapterDetails.PageSize, ref is_Fetch_Count);
        gvItemList.DataSource = ds;
        gvItemList.DataBind();
        ucCustomPagerChapterDetails.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerChapterDetails.BuildPager();

        DataTable dtChapterItemList = ViewState["dtChapterItemList"] as DataTable;
        foreach (GridViewRow gr in gvItemList.Rows)
        {
            int Item_ID = Convert.ToInt32(gvItemList.DataKeys[gr.RowIndex].Value);

            if (dtChapterItemList.Rows.Contains(Item_ID))
            {
                (gr.FindControl("chkSelected") as CheckBox).Checked = true;
            }
        }
    }

    protected void BindTrainerlist()
    {

        DataSet ds = BLL_LMS_Training.GET_Chapter_Trainer(UDFLib.ConvertToInteger(ViewState["Chapter_Id"]));
        gvTrainerRank.DataSource = ds;
        gvTrainerRank.DataBind();

    }
    protected void btnSearchItem_Click(object sender, EventArgs e)
    {

        BindChapterItem();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string js = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);
    }

    private void SaveSelectedChapterItems()
    {
        DataTable dtChapterItemList = ViewState["dtChapterItemList"] as DataTable;
        DataRow dr;
        foreach (GridViewRow gr in gvItemList.Rows)
        {
            bool Checked = (gr.FindControl("chkSelected") as CheckBox).Checked;
            if (Checked)
            {
                int Item_ID = Convert.ToInt32(gvItemList.DataKeys[gr.RowIndex].Value);
                blnChapterItem = true;
                if (!dtChapterItemList.Rows.Contains(Item_ID))
                {
                    dr = dtChapterItemList.NewRow();
                    dr["ChapterItemId"] = Item_ID;
                    dtChapterItemList.Rows.Add(dr);
                }
            }
        }

        ViewState["dtChapterItemList"] = dtChapterItemList;
    }



    protected void btnSaveandClose_Click(object sender, EventArgs e)
    {

        if (UDFLib.ConvertStringToNull(ViewState["Chapter_Id"]) == null)
        {
            if (BLL_LMS_Training.Check_Duplicate_CHAPTER(txtChapterName.Text, Convert.ToInt32(Request.QueryString["Program_Id"])) == 1)
            {
                string msgmodal = String.Format("alert('Chapter already exists.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
            }

            else
            {

                CreateorUpdateChapter();

            }
        }

        else
        {
            CreateorUpdateChapter();
        }
    }


    protected void CreateorUpdateChapter()
    {
        if (BLL_LMS_Training.Check_Duplicate_CHAPTER(txtChapterName.Text, Convert.ToInt32(Request.QueryString["Program_Id"]), UDFLib.ConvertIntegerToNull(ViewState["Chapter_Id"])) == 1)
        {
            string msgmodal = String.Format("alert('Chapter already exists.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
            return;
        }
        blnChapterItem = false;
        blnTrainer = false;
        SaveSelectedChapterItems();
        DataTable dtChapterItemList = ViewState["dtChapterItemList"] as DataTable;


        foreach (GridViewRow gr in gvItemList.Rows)
        {
            int Item_ID = Convert.ToInt32(gvItemList.DataKeys[gr.RowIndex].Value);
            if ((gr.FindControl("chkSelected") as CheckBox).Checked == false)
            {
                if (dtChapterItemList.Select("ChapterItemId=" + Item_ID).Length > 0)
                    dtChapterItemList.Rows.Remove(dtChapterItemList.Select("ChapterItemId=" + Item_ID)[0]);
            }


        }


        DataTable dtChapterTrainerList = new DataTable();
        dtChapterTrainerList.Columns.Add("ChapterTrainerId", typeof(int));
        DataRow dr_Trainer;
        foreach (GridViewRow gr in gvTrainerRank.Rows)
        {
            dr_Trainer = dtChapterTrainerList.NewRow();
            bool Checked = (gr.FindControl("chkSelected") as CheckBox).Checked;
            if (Checked)
            {
                blnTrainer = true;
                dr_Trainer["ChapterTrainerId"] = Convert.ToInt32(gvTrainerRank.DataKeys[gr.RowIndex].Value);
                dtChapterTrainerList.Rows.Add(dr_Trainer);
            }
        }
        if (dtChapterItemList != null)
            if (dtChapterItemList.Rows.Count > 0)
                blnChapterItem = true;
        if (blnChapterItem == false)
        {
            string msgmodal = String.Format("alert('Please select at least One chapter Item.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
            return;
        }



        if (BLL_LMS_Training.Check_Chapter_Item(UDFLib.ConvertIntegerToNull(Request.QueryString["Program_Id"]), UDFLib.ConvertIntegerToNull(ViewState["Chapter_Id"]), dtChapterItemList) == 1)
        {
            string msgmodal = String.Format("alert('Chapter items already exist in this program.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
            return;
        }



        if (blnTrainer == false && UDFLib.ConvertIntegerToNull(Request.QueryString["ProgramCategory"]) != 4)
        {
            string msgmodal = String.Format("alert('Please select at least one Trainer.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "TrainingItem", msgmodal, true);
            return;
        }
        int? Chapter_Id = UDFLib.ConvertToInteger(ViewState["Chapter_Id"]);
        DataTable ChapterItemExists = BLL_LMS_Training.Ins_Chapter_Details(UDFLib.ConvertIntegerToNull(Request.QueryString["Program_Id"]), txtChapterName.Text.Trim(), ref Chapter_Id, txtChapterName.Text, dtChapterItemList, dtChapterTrainerList, Convert.ToInt32(Session["USERID"]), 1);



        if (Chapter_Id != 0)
        {
            ViewState["Chapter_Id"] = Chapter_Id;
        }

        Boolean blnchk = false;
        foreach (GridViewRow row in gvItemList.Rows)
        {
            foreach (DataRow dr in ChapterItemExists.Rows)
            {
                if (UDFLib.ConvertIntegerToNull(dr["ITEM_ID"]) == Convert.ToInt32(gvItemList.DataKeys[row.RowIndex].Values[0]))
                {
                    row.BackColor = System.Drawing.Color.Red;
                    blnchk = true;
                }

            }

        }
        string js;
        if (blnchk == true)
        {
            js = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);
            return;
        }

        js = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);

    }


    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            Session["vsAttachmentData"] = file.GetContents();
            Session["vsAttachmentFileName"] = file.FileName;

        }
        catch (Exception ex)
        {

        }

    }
    /*
     * Delete chapter if training is not sceduled.
     * The pageSize passed in GET_Program_List is -1 as it lists all program in table LMS_DTL_TRAINING_PROGRAM
    
     * */
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string IsProgram_Scheduled = string.Empty;
            int is_Fetch_Count = 1;
            DataTable dt = BLL_LMS_Training.GET_Program_List("", null, 1, -1, ref is_Fetch_Count).Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("PROGRAM_ID='" + Convert.ToInt32(Request.QueryString["Program_Id"]) + "'");
                if (dr.Length > 0)
                {
                    if (dr[0].ItemArray[2].ToString() != "" && dr[0].ItemArray[3].ToString() != "")
                    {
                        string js = String.Format("alert('Chapter can not be deleted as Training schedule has already been planned for this chapter.'); " + "parent.ReloadParent_ByButtonID();");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);
                    }
                    else
                    {
                        int Result = BLL_LMS_Training.Del_TRAINING_CHAPTER(UDFLib.ConvertToInteger(ViewState["Chapter_Id"]), Convert.ToInt32(Session["USERID"]));
                        // Response.Redirect("LMS_Chapter_Details.aspx");
                        string js = String.Format("alert('Chapter Deleted Successfully'); " + "parent.ReloadParent_ByButtonID();");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReloadParent", js, true);
                    }
                }

            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void btnNewItem_Click(object sender, EventArgs e)
    {

        String msgretv = String.Format("OpenPopupWindowBtnID('POP__ItemDetails', 'Add New Item','LMS_ItemDetails.aspx', 'popup', 350, 980, null, null, false, false, true, false,'" + btnSearchItem.ClientID + "')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

    }
    protected void FillItemType()
    {
        ListItem li0 = new ListItem("-- Select --", null);
        ddlItemType.Items.Insert(0, li0);
        ListItem li1 = new ListItem("RESOURCE", "1");
        ddlItemType.Items.Insert(1, li1);
        ListItem li2 = new ListItem("FBM", "2");
        ddlItemType.Items.Insert(2, li2);
        ListItem li3 = new ListItem("ARTICLE", "3");
        ddlItemType.Items.Insert(3, li3);
        ListItem li4 = new ListItem("VIDEO MATERIALS", "4");
        ddlItemType.Items.Insert(4, li4);
    }
}



