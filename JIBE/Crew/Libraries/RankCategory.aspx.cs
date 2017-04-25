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
using SMS.Business.Crew;

public partial class RankCategory : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

    UserAccess objUA = new UserAccess();

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

                BindRankCategory();
            }
        }
        catch (Exception)
        {
            throw;
        }

    }

    public void BindRankCategory()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.RankCategory_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
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
        catch (Exception)
        {
            throw;
        }
    }

    protected void UserAccessValidation()
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }

    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.SetFocus("ctl00_MainContent_txtRankcategory");
            HiddenFlag.Value = "Add";
            OperationMode = "Add rank category";

            ClearField();

            string AddRankCategory = String.Format("showModal('divadd',false);validatetxt();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRankCategory", AddRankCategory, true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ClearField()
    {

        txtRankcategoryID.Text = "";
        txtRankcategory.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {   
            if (HiddenFlag.Value == "Add")
            {

                int retval = objBLL.InsertRankCategory(txtRankcategory.Text.Trim(), Convert.ToInt32(Session["USERID"]));
                if (retval == 1)
                {
                    BindRankCategory();
                    string hidemodal = String.Format("hideModal('divadd',true);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert('Added new category successfully.' );", true);

                }
                else
                {
                    BindRankCategory();
                    string hidemodal = String.Format("showModal('divadd',true);validatetxt();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert('Category name already Exists.' );", true);
                }

            }
            else
            {
                int retval = objBLL.EditRankCategory(Convert.ToInt32(txtRankcategoryID.Text.Trim()), txtRankcategory.Text.Trim(), Convert.ToInt32(Session["USERID"]));
                if (retval == 1)
                {
                    BindRankCategory();
                    string hidemodal = String.Format("hideModal('divadd',true);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert('Updated successfully.' );", true);
                }
                else
                {
                    BindRankCategory();
                    string hidemodal = String.Format("showModal('divadd',true);validatetxt();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert('Category name already Exists.' );", true);
                }


            }
        }
        catch (Exception)
        { throw; }


    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit rank category";

            DataTable dt = new DataTable();
            dt = objBLL.Get_RankCategory_List(Convert.ToInt32(e.CommandArgument.ToString()));


            txtRankcategoryID.Text = dt.Rows[0]["ID"].ToString();
            txtRankcategory.Text = dt.Rows[0]["Category_Name"].ToString();

            string RankCategory = String.Format("showModal('divadd',false);validatetxt();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RankCategory", RankCategory, true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            int retval = objBLL.DeleteRankCategory(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            BindRankCategory();
        }
        catch (Exception)
        {
            throw;
        }
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
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.RankCategory_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);


            string[] HeaderCaptions = { "Rank Category" };
            string[] DataColumnsName = { "Category_Name" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "RankCategory", "Rank Category", "");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void gvRankCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception)
        {
            throw;
        }

    }

    protected void gvRankCategory_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindRankCategory();
        }
        catch (Exception)
        {
            throw;
        }
    }
}