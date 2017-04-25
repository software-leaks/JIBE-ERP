using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.QMS;
using SMS.Business.Crew;
using SMS.Business.INFRA.Infrastructure;

public partial class QMS_FBM_FBM_Main_Read_Acces_Right : System.Web.UI.Page
{

    BLL_Infra_ReadAccessRight objBLL = new BLL_Infra_ReadAccessRight();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindRankIdDLL();
            BindReadAccessRight();
        }
    }
    private void BindRankIdDLL()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();
        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;
    }
    public void BindReadAccessRight()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.FbmReadAccessRight_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvRankCategory.DataSource = dt;
            gvRankCategory.DataBind();
        }
        else
        {
            gvRankCategory.DataSource = dt;
            gvRankCategory.DataBind();
        }

    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtRankcategory");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Rank Id";

        ClearField();

        string AddRankCategory = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRankCategory", AddRankCategory, true);
    }

    protected void ClearField()
    {

        txtRankcategoryID.Text = "";
        ddlRank.SelectedValue = "0";
        //txtRankcategory.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLL.InsertFbmReadAccessRight(ddlRank.SelectedValue, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLL.EditFbmReadAccessRight(Convert.ToInt32(txtRankcategoryID.Text.Trim()), ddlRank.SelectedValue, Convert.ToInt32(Session["USERID"]));
        }

        BindReadAccessRight();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Rank Id";


        DataTable dt = new DataTable();
        dt = objBLL.Get_FbmReadAccessRight_List(Convert.ToInt32(e.CommandArgument.ToString()));


        txtRankcategoryID.Text = dt.Rows[0]["id"].ToString();
        ddlRank.SelectedValue = dt.Rows[0]["RANK_ID"].ToString();

        string RankCategory = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RankCategory", RankCategory, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteFbmReadAccessRight(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindReadAccessRight();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindReadAccessRight();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindReadAccessRight();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.FbmReadAccessRight_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Rank Short Name", "Rank Name" };
        string[] DataColumnsName = { "Rank_Short_Name", "Rank_Name" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ReadAccessRight", "FBM Read Access Right", "");

    }

    protected void gvRankCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvRankCategory_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindReadAccessRight();
    }
}

