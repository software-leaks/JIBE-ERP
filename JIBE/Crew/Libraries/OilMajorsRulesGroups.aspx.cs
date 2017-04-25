using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Crew_Libraries_OilMajorsRuleGroup : System.Web.UI.Page
{
    #region Decalrations
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public int Result = 0;

    #endregion

    /// <summary>
    /// Page Load event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            OperationMode = "Add New Rule";

            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;

                BindOilMajorsRuleGroup();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Oil Majors
    /// </summary>
    public void BindOilMajorsRuleGroup()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.CRUD_OilMajorsRuleGroup("", 0, "R", 0, 0, txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ref Result);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvOilMajorsRuleGroup.DataSource = dt;
                gvOilMajorsRuleGroup.DataBind();
                ImgExpExcel.Visible = true;
            }
            else
            {
                gvOilMajorsRuleGroup.DataSource = null;
                gvOilMajorsRuleGroup.DataBind();
                ImgExpExcel.Visible = false;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }


    /// <summary>
    /// Delete Oil Major
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            int rowcount = 0;
            DataTable dt = objBLL.CRUD_OilMajorsRuleGroup("", 0, "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), null, "", null, null, null, ref  rowcount, ref Result);
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('" + UDFLib.GetException("SuccessMessage/DeleteMessage") + "')", true);

            BindOilMajorsRuleGroup();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }


    /// <summary>
    /// To save or update oil major
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(hdnOilMajorID.Value) > 0)
            {
                OperationMode = "Edit Oil Major";

                int rowcount = 0;
                DataTable dt = objBLL.CRUD_OilMajorsRuleGroup(txtGroupName.Text.Trim(), 0, "U", GetSessionUserID(), Convert.ToInt32(hdnOilMajorID.Value), null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("InformationMessage/DataExists") + "');showModal('divadd', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divadd');", true);
                    BindOilMajorsRuleGroup();
                }
            }
            else
            {
                int rowcount = 0;
                DataTable dt = objBLL.CRUD_OilMajorsRuleGroup(txtGroupName.Text.Trim(), 0, "I", GetSessionUserID(), 0, null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("InformationMessage/DataExists") + "');showModal('divadd', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divadd');", true);
                    BindOilMajorsRuleGroup();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Oil Major Row data Bound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvOilMajorsRuleGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Sorting 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="se"></param>
    protected void gvOilMajorsRuleGroup_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindOilMajorsRuleGroup();
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
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {
                ImgAdd.Visible = false;
            }
            if (objUA.Edit == 1)
                uaEditFlag = true;
            else
                btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            BindOilMajorsRuleGroup();
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
            txtfilter.Text = txtGroupName.Text = string.Empty;
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControl();
            BindOilMajorsRuleGroup();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.CRUD_OilMajorsRuleGroup("", 0, "R", 0, 0, txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                   , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ref Result);

            dt.Columns.Add("Group Type", typeof(string));

            string[] HeaderCaptions = { "Group"};
            string[] DataColumnsName = { "Group_Name" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "OilMajorsRuleGroup", "Oil Majors Rule Groups", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}