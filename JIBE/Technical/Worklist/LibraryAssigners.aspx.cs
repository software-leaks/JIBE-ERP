using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using System.Web.UI.HtmlControls;

public partial class LibraryAssigners : System.Web.UI.Page
{
    BLL_Tec_Worklist_Admin objBLL = new BLL_Tec_Worklist_Admin();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;


            BindAssigners();
        }

    }



    public void BindAssigners()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchAssigner(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        //=============================================================================
        gvAssigner.DataSource = dt;
        gvAssigner.DataBind();

        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        ucCustomPagerItems.BuildPager();
        //=============================================================================

        //if (ucCustomPagerItems.isCountRecord == 1)
        //{
        //    ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        //    ucCustomPagerItems.BuildPager();
        //}

        //if (dt.Rows.Count > 0)
        //{
        //    gvAssigner.DataSource = dt;
        //    gvAssigner.DataBind();
        //}
        //else
        //{
        //    gvAssigner.DataSource = dt;
        //    gvAssigner.DataBind();
        //}

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            //  gvAssigner.Columns[gvAssigner.Columns.Count - 2].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            // gvAssigner.Columns[gvAssigner.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {

        }

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
        this.SetFocus("ctl00_MainContent_txtAssignerName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Assignor";

        ClearField();

        string OfficeDept = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OfficeDept", OfficeDept, true);
    }

    protected void ClearField()
    {

        txtAssignerID.Text = "";
        txtAssignerName.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        try
        {

            if (HiddenFlag.Value == "Add")
            {
                int retval = objBLL.InsertAssigner(txtAssignerName.Text, Convert.ToInt32(Session["USERID"]));
            }
            else
            {
                int retval = objBLL.EditAssigner(Convert.ToInt32(txtAssignerID.Text.Trim()), txtAssignerName.Text, Convert.ToInt32(Session["USERID"]));
            }

            BindAssigners();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message.ToString() + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
            txtAssignerName.Focus();
            string hideModal = String.Format("hideModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideModal", hideModal, true);

        }

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Assignor";

        DataTable dt = new DataTable();
        dt = objBLL.Get_Assigner_List(Convert.ToInt32(e.CommandArgument.ToString()));


        txtAssignerID.Text = dt.Rows[0]["ID"].ToString();
        txtAssignerName.Text = dt.Rows[0]["VALUE"].ToString();

        string AddAssignermodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteAssigner(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindAssigners();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindAssigners();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindAssigners();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchAssigner(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
           , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Assignor" };
        string[] DataColumnsName = { "VALUE" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Assignor", "Assignor", "");

    }

    protected void gvAssigner_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvAssigner_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindAssigners();
    }
}