using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Operations;
using System.Web.UI.HtmlControls;


public partial class Lib_LubeOil_Category : System.Web.UI.Page
{
    BLL_OPS_Admin objBLLOps = new BLL_OPS_Admin();
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
            BindRankCategory();
        }

    }

    public void BindRankCategory()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.LubeOilCategory_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvLoCategory.DataSource = dt;
            gvLoCategory.DataBind();
        }
        else
        {
            gvLoCategory.DataSource = dt;
            gvLoCategory.DataBind();
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
        this.SetFocus("ctl00_MainContent_txtLOCategoryName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Lube Oil Category";

        ClearField();

        string AddRankCategory = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRankCategory", AddRankCategory, true);
    }

    protected void ClearField()
    {
        txtLOCategoryID.Text = "";
        txtLOCategoryName.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int retval = 0;
        if (HiddenFlag.Value == "Add")
        {
            retval = objBLLOps.InsertLubeOilCategory(txtLOCategoryName.Text, Convert.ToInt32(Session["USERID"]));           
        }
        else
        {
            retval = objBLLOps.EditLubeOilCategory(Convert.ToInt32(txtLOCategoryID.Text.Trim()), txtLOCategoryName.Text, Convert.ToInt32(Session["USERID"]));
        }
        if (retval > 0)
        {
            BindRankCategory();
        }
        else
        {
            string js = "";
            js = "alert('Lube Oil Category with same name already present. Please enter different name...');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Lube Oil Category";

        DataTable dt = new DataTable();
        dt = objBLLOps.Get_LubeOilCategory_List(Convert.ToInt32(e.CommandArgument.ToString()));


        txtLOCategoryID.Text = dt.Rows[0]["ID"].ToString();
        txtLOCategoryName.Text = dt.Rows[0]["Category_Name"].ToString();

        string RankCategory = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RankCategory", RankCategory, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLLOps.DeleteLubeOilCategory(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindRankCategory();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindRankCategory();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindRankCategory();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.LubeOilCategory_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Lube Oil Category" };
        string[] DataColumnsName = { "Category_Name" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "LubeOilCategory", "Lube Oil Category", "");

    }

    protected void gvLoCategory_RowDataBound(object sender, GridViewRowEventArgs e)
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


        //ImageButton ImgUpdate = (ImageButton)e.Row.FindControl("ImgUpdate");


        //if (objUA.Delete == 1)
        //{ 
        
        //}

    }

    protected void gvLoCategory_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindRankCategory();
    }
}