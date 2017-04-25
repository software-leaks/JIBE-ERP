using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
using System.Web.UI.HtmlControls;

public partial class Technical_Vetting_Vetting_ObservationCategories : System.Web.UI.Page
{
    public int Result = 0;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    BLL_VET_VettingLib objBLL = new BLL_VET_VettingLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
            {
                divLoggout.Visible = true;
                MainContent.Visible = false;
            }
            else
            {
                MainContent.Visible = true;
                divLoggout.Visible = false;

                UserAccessValidation();
                if (MainDiv.Visible)
                {
                    if (!IsPostBack)
                    {
                        ViewState["SORTDIRECTION"] = 0;
                        ViewState["SORTBYCOLOUMN"] = null;

                        ucCustomPagerItems.PageSize = 10;
                        BindGrid();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to bind gird
    /// </summary>
    public void BindGrid()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataTable dt = objBLL.VET_Get_Category(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ref Result);
            
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            gvCategory.DataSource = dt;
            gvCategory.DataBind();
            if (dt.Rows.Count > 0)
            {
                
                ImgExpExcel.Enabled = true;
            }
            else
            {

                ImgExpExcel.Enabled = false;
            }             
         
              
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Clear All controls
    /// </summary>
    private void ClearControl()
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = null;
            ViewState["SORTDIRECTION"] = null;
            ucCustomPagerItems.CurrentPageIndex = 1;
            txtfilter.Text = string.Empty;
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControl();
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataTable dt = objBLL.VET_Get_Category(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ref Result);
            string[] HeaderCaptions = { "Category Name" };
            string[] DataColumnsName = { "OBSCategory_Name" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ObservationCategory" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), "Observation Category", "");
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvCategory_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindGrid();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// Check for access rights
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            UserAccess objUA = new UserAccess();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
            {
                MainDiv.Visible = false;
                AccessMsgDiv.Visible = true;
            }
            else
            {
                MainDiv.Visible = true;
                AccessMsgDiv.Visible = false;
            }

            if (objUA.Add == 0)
            {
                ImgAdd.Visible = false;
                btnSave.Visible = false;
            }
            if (objUA.Edit == 1)
                uaEditFlag = true;

            if (objUA.Delete == 1)
                uaDeleteFlage = true;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Get UserId
    /// </summary>
    /// <returns></returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

 
    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            objBLL.VET_DEL_Categories(Convert.ToInt32(e.CommandArgument.ToString()), GetSessionUserID(), ref Result);

            if (Result == 0)
                BindGrid();

            
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
        if (hdnCatid.Value != "")
        {

            if (txtCategoryName.Text.Trim() != "")
            {
                objBLL.VET_UPD_Categories(Convert.ToInt32(hdnCatid.Value), txtCategoryName.Text.Trim(), GetSessionUserID(), ref Result);
                if (Result < 0)
                {
                    string jsSqlError3 = "alert('Category already exist.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetTitleonEdit", "<script type='text/javascript'>SetTitleonEdit();</script>", false);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "showModal('divCategory1', false);", true);
                    
                }
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "hideModal('divCategory1');", true);
                    BindGrid();
                }
            }
        }
        else
        {

            if (txtCategoryName.Text.Trim() != "")
            {
                objBLL.VET_INS_Categories(txtCategoryName.Text.Trim(), GetSessionUserID(), ref Result);
                if (Result < 0)
                {
                    string jsSqlError3 = "alert('Category already exist.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetTitleonAdd", "<script type='text/javascript'>SetTitleonAdd();</script>", false);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "showModal('divCategory1', false);", true);
                }
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "hideModal('divCategory1');", true);
                    BindGrid();
                }
            }
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("Edit");
                Label lblCategoryId = (Label)e.Row.FindControl("lblCategoryIdEdit");
                Label lblCategoryName = (Label)e.Row.FindControl("lblCategoryName");

                ImgEdit.Attributes.Add("onclick", "onEditClick(" + lblCategoryId.Text + ",'" + lblCategoryName.Text + "')");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}