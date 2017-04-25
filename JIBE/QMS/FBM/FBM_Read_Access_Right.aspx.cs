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
using SMS.Business.QMS;
using SMS.Properties;
using SMS.Business.Crew;


public partial class QMS_FBM_Read_Access_Right : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["ID"] = null;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_RankList();
            Load_Rank_Category();

            BindFBMAccessRight();

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

        }
    }
    public void BindFBMAccessRight()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_FBM_Report.FBM_Get_RankList_Search(txtSearchBy.Text, UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRankCategory.SelectedValue)
                , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            gvFBMAcessRight.DataSource = dt;
            gvFBMAcessRight.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        ddlRank.DataSource = objCrewAdmin.Get_RankList();
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void Load_Rank_Category()
    {
        BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
        ddlRankCategory.DataSource = objBLL.Get_RankCategories();
        ddlRankCategory.DataTextField = "category_name";
        ddlRankCategory.DataValueField = "id";
        ddlRankCategory.DataBind();
        ddlRankCategory.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void gvFBMAcessRight_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        }
    }

    protected void gvFBMAcessRight_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton ImgFBMAtt = (ImageButton)e.Row.FindControl("ImgFBMAtt");
            Label lblUserID = (Label)e.Row.FindControl("lblUserID");
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "../../purchase/Image/arrowUp.png";

                    else
                        img.Src = "../../purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

    }
    protected void gvFBMAcessRight_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindFBMAccessRight();


    }
    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = BLL_FBM_Report.FBM_READ_ACCESS_RIGHT_REMOVE(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        BindFBMAccessRight();
        UpdPnlGrid.Update();

    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {

        BindFBMAccessRight();
        UpdPnlGrid.Update();
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {


        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        txtSearchBy.Text = "";

        ddlRank.SelectedValue = "0";
        ddlRankCategory.SelectedValue = "0";

        BindFBMAccessRight();
        UpdPnlGrid.Update();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_FBM_Report.FBM_Get_RankList_Search(txtSearchBy.Text, UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRankCategory.SelectedValue)
                     , sortbycoloumn, sortdirection, null, null, ref  rowcount);

            string[] HeaderCaptions = { "Rank Name", "Rank Short Name", "Read Access" };
            string[] DataColumnsName = { "Rank_Name", "Rank_Short_Name", "READ_ACCESS_FLAG" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "FBMReadAccess", "FBM Read Access");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void chkAccess_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkAccess = (sender as CheckBox);
            GridViewRow row = chkAccess.NamingContainer as GridViewRow;

            int retVal = BLL_FBM_Report.FBM_READ_ACCESS_RIGHT_SAVE(UDFLib.ConvertIntegerToNull(((Label)row.FindControl("lblID")).Text)
                    , UDFLib.ConvertIntegerToNull(((Label)row.FindControl("lblRankID")).Text)
                    , Convert.ToInt32(Session["USERID"])
                    , ((CheckBox)row.FindControl("chkAccess")).Checked == true ? 1 : 0);

            BindFBMAccessRight();

            UpdPnlGrid.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}