using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Crew_Libraries_Ranks : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;
                ucCustomPagerItems.PageSize = 20;

                BindRankDLL();
                BindRankGrid();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {
            ImgAddRank.Visible = false;
        }

        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
 

    protected void BindRankDLL()
    {
        DataTable dt = objBLL.Get_RankCategories();

        ddlRankCategory.DataSource = dt;
        ddlRankCategory.DataTextField = "category_name";
        ddlRankCategory.DataValueField = "id";
        ddlRankCategory.DataBind();
        ddlRankCategory.Items.Insert(0, new ListItem("-Select-", "0"));


        DDLRankCategoryFilter.DataSource = dt;
        DDLRankCategoryFilter.DataTextField = "category_name";
        DDLRankCategoryFilter.DataValueField = "id";
        DDLRankCategoryFilter.DataBind();
        DDLRankCategoryFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
    
    }

    public void BindRankGrid()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.Get_RankList_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(DDLRankCategoryFilter.SelectedValue)
            ,UDFLib.ConvertIntegerToNull(optDeckEnginefilter.SelectedValue)
            ,sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            GridViewRank.DataSource = dt;
            GridViewRank.DataBind();
        }
        else
        {
            GridViewRank.DataSource = dt;
            GridViewRank.DataBind();
        }
    }

    protected void Load_RankShortOrder(DropDownList ddlRankShortOrder)
    {
        //ddlRankShortOrder.DataSource = objBLL.ExecuteQuery("SELECT RANK_SORT_ORDER FROM CRW_LIB_CREW_RANKS");
        //ddlRankShortOrder.DataTextField = "Rank_Sort_Order";
        //ddlRankShortOrder.DataValueField = "Rank_Sort_Order";
        //ddlRankShortOrder.DataBind();
        //ddlRankShortOrder.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    
    protected void GridViewRank_RowDataBound(object sender, GridViewRowEventArgs e)
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
        
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int? IsDeckOfficer = null;
            if (chkDeckOfficer.Checked)
            {
                IsDeckOfficer = 1;
            }
        
            if (HiddenFlag.Value == "Add")
            {
                int retval = objBLL.InsertRank(txtRankName.Text.Trim(), txtRankShortName.Text.Trim(), int.Parse(ddlRankCategory.SelectedValue),UDFLib.ConvertIntegerToNull(optDeckEngine.SelectedValue), GetSessionUserID(),IsDeckOfficer);
            }
            else
            {
                int retval = objBLL.EditRank(Convert.ToInt32(txtRankID.Text), txtRankName.Text, txtRankShortName.Text, int.Parse(ddlRankCategory.SelectedValue)
                    , int.Parse(txtRankOrder.Text), UDFLib.ConvertIntegerToNull(optDeckEngine.SelectedValue), GetSessionUserID(),IsDeckOfficer);
            }
        
            BindRankGrid();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        DDLRankCategoryFilter.SelectedValue = "0";

        BindRankGrid();
    }

    public void ClearFields()
    {
        txtRankID.Text = "";
        txtRankName.Text = "";
        txtRankShortName.Text = "";
        ddlRankCategory.SelectedValue = "0";
        txtRankOrder.Text = "";
        chkDeckOfficer.Checked = false;
    }

    protected void GridViewRank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ID = 0;
            if (e.CommandName == "MoveUp")
            {
                ID = int.Parse(e.CommandArgument.ToString());
                objBLL.Swap_Rank_Sort_Order(ID, -1, Convert.ToInt32(Session["UserID"].ToString()));
                BindRankGrid();

            }
            else if (e.CommandName == "MoveDown")
            {
                ID = int.Parse(e.CommandArgument.ToString());
                objBLL.Swap_Rank_Sort_Order(ID, 1, Convert.ToInt32(Session["UserID"].ToString()));
                BindRankGrid();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
     

    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Rank";
        
            DataTable dt = objBLL.Get_RankList();
            dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

            txtRankID.Text = dt.DefaultView[0]["ID"].ToString();
            txtRankName.Text = dt.DefaultView[0]["Rank_Name"].ToString();
            txtRankShortName.Text = dt.DefaultView[0]["Rank_Short_Name"].ToString();
           ddlRankCategory.SelectedValue = dt.DefaultView[0]["Rank_category"].ToString() != "" ? dt.DefaultView[0]["Rank_category"].ToString() : "0";
            txtRankOrder.Text = dt.DefaultView[0]["Rank_Sort_Order"].ToString();
            if (!string.IsNullOrEmpty(dt.DefaultView[0]["IsDeckOfficer"].ToString()))
            {
                chkDeckOfficer.Checked = true;
            }
            else {
                chkDeckOfficer.Checked = false;
            }

            optDeckEngine.SelectedValue = "0";

            if (dt.DefaultView[0]["Link"].ToString() != "")
            {
                if (dt.DefaultView[0]["Link"].ToString() == "1")
                    optDeckEngine.SelectedValue = "1";
                else if (dt.DefaultView[0]["Link"].ToString() == "2")
                    optDeckEngine.SelectedValue = "2";
            }
        
            string Rankmodal = String.Format("showModal('dvAddNewRank',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Rankmodal", Rankmodal, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
       int retval = objBLL.DeleteRank(int.Parse(e.CommandArgument.ToString()), GetSessionUserID());
       BindRankGrid();
    }

    protected void GridViewRank_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindRankGrid();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindRankGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        DDLRankCategoryFilter.SelectedValue = "0";
        optDeckEnginefilter.SelectedValue = "0";
        BindRankGrid();
    }

    protected void ImgAddRank_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlVesselManager");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Rank";

        ClearFields();

        string AddNewRank = String.Format("showModal('dvAddNewRank',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewRank", AddNewRank, true);
    }




    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
       try
       {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.Get_RankList_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(DDLRankCategoryFilter.SelectedValue)
                , UDFLib.ConvertIntegerToNull(optDeckEnginefilter.SelectedValue)
                , sortbycoloumn, sortdirection,null, null, ref  rowcount);


            string[] HeaderCaptions = { "Name", "Short Name", "Category","Deck/Engine","ISDeckOfficer" };
            string[] DataColumnsName = { "Rank_Name", "Rank_Short_Name", "category_Name", "DeckOrEngine", "DeckOfficer" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CrewRank", "Crew Rank", "");
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }
    }
    
}