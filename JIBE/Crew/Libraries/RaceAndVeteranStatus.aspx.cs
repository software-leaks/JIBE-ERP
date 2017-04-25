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

public partial class Crew_RaceAndVeteranStatus : System.Web.UI.Page
{
    #region Decalrations
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
            Page.Title = "Race & Veteran Status Library Page";
            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ViewState["SORTDIRECTION_VS"] = null;
                ViewState["SORTBYCOLOUMN_VS"] = null;
                ucCustomPagerItems.PageSize = 20;
                if (Tabs.ActiveTabIndex == 0)
                    BindRace();
                else if (Tabs.ActiveTabIndex == 1)
                    BindVeteranStatus();
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void Tabs_OnActiveTabChanged(object sender, EventArgs e)
    {
        if (Tabs.ActiveTabIndex == 0)
            BindRace();
        else if (Tabs.ActiveTabIndex == 1)
            BindVeteranStatus();
    }

    #region Race
    /// <summary>
    /// Bind Race
    /// </summary>
    public void BindRace()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_Race("", "R", 0, 0, txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ref Result);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvRace.DataSource = dt;
                gvRace.DataBind();
                ImgExpExcel.Visible = true;
            }
            else
            {
                gvRace.DataSource = null;
                gvRace.DataBind();
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
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_Race("", "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), null, "", null, null, null, ref  rowcount, ref Result);
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindRace();
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
            if (Convert.ToInt32(hdnRaceID.Value) > 0)
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_Race(txtRace.Text.Trim(), "U", GetSessionUserID(), Convert.ToInt32(hdnRaceID.Value), null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Race already exist in the system');showModal('divadd', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divadd');", true);
                    BindRace();
                }
            }
            else
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_Race(txtRace.Text.Trim(), "I", GetSessionUserID(), 0, null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Race already exist in the system');showModal('divadd', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divadd');", true);
                    BindRace();
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
    protected void gvRace_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }
        catch (Exception ex)
        { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// Sorting 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="se"></param>
    protected void gvRace_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindRace();
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


            if (objUA.Edit == 1)
                uaEditFlag = true;
            else
            {
                btnSaveVeteranStatus.Visible = btnsave.Visible = false;
            }


            if (objUA.Add == 0)
            {
                ImgAddVeteran.Visible = ImgAdd.Visible = false;
            }
            else
            {
                ImgAddVeteran.Visible = ImgAdd.Visible = true;
                btnSaveVeteranStatus.Visible = btnsave.Visible = true;
            }

            if (objUA.Delete == 1) uaDeleteFlage = true;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            BindRace();
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
        ViewState["SORTBYCOLOUMN"] = null;
        ViewState["SORTDIRECTION"] = null;
        ucCustomPagerItems.CurrentPageIndex = 1;
        txtfilter.Text = txtRace.Text = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ClearControl();
        BindRace();
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

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_Race("", "R", 0, 0, txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection, null, null, ref  rowcount, ref Result);

            string[] HeaderCaptions = { "Race" };
            string[] DataColumnsName = { "Race" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Race", "Race", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Veteran Status
    private void BindVeteranStatus()
    {
        try
        {
            int rowcount = ucCustomPagerItemsVeteranStatus.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN_VS"] == null) ? null : (ViewState["SORTBYCOLOUMN_VS"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION_VS"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION_VS"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_VeteranStatus("", "R", 0, 0, txtVeteranStatusFilter.Text != "" ? txtVeteranStatusFilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItemsVeteranStatus.CurrentPageIndex, ucCustomPagerItemsVeteranStatus.PageSize, ref  rowcount, ref Result);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItemsVeteranStatus.CountTotalRec = rowcount.ToString();
                ucCustomPagerItemsVeteranStatus.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvVeteranStatus.DataSource = dt;
                gvVeteranStatus.DataBind();
                ImgExpExcelVeteran.Visible = true;
            }
            else
            {
                gvVeteranStatus.DataSource = null;
                gvVeteranStatus.DataBind();
                ImgExpExcelVeteran.Visible = false;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
    /// <summary>
    /// Delete Oil Major
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDeleteVeteranStatus(object source, CommandEventArgs e)
    {
        try
        {
            int rowcount = 0;
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_VeteranStatus("", "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), null, "", null, null, null, ref  rowcount, ref Result);
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindVeteranStatus();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// To save or update Veteran Status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveVeteranStatus_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(hdnVeteranStatusID.Value) > 0)
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_VeteranStatus(txtVeteranStatus.Text.Trim(), "U", GetSessionUserID(), Convert.ToInt32(hdnVeteranStatusID.Value), null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Veteran Status already exist in the system');showModal('divAddVeteranStatus', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divAddVeteranStatus');", true);
                    BindVeteranStatus();
                }
            }
            else
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_VeteranStatus(txtVeteranStatus.Text.Trim(), "I", GetSessionUserID(), 0, null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Veteran Status already exist in the system');showModal('divAddVeteranStatus', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divAddVeteranStatus');", true);
                    BindVeteranStatus();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvVeteranStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SORTBYCOLOUMN_VS"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN_VS"].ToString());
                    if (img != null)
                    {
                        if (ViewState["SORTDIRECTION_VS"] == null || ViewState["SORTDIRECTION_VS"].ToString() == "0")
                            img.Src = "~/purchase/Image/arrowUp.png";
                        else
                            img.Src = "~/purchase/Image/arrowDown.png";

                        img.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        { UDFLib.WriteExceptionLog(ex); }
    }

    protected void gvVeteranStatus_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN_VS"] = se.SortExpression;
            if (ViewState["SORTDIRECTION_VS"] != null && ViewState["SORTDIRECTION_VS"].ToString() == "0")
                ViewState["SORTDIRECTION_VS"] = 1;
            else
                ViewState["SORTDIRECTION_VS"] = 0;
            BindVeteranStatus();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilterVeteran_Click(object sender, EventArgs e)
    {
        try
        {
            BindVeteranStatus();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Clear All controls
    /// </summary>
    private void ClearControlVeteranStatus()
    {
        ViewState["SORTBYCOLOUMN_VS"] = null;
        ViewState["SORTDIRECTION_VS"] = null;
        ucCustomPagerItemsVeteranStatus.CurrentPageIndex = 1;
        txtVeteranStatusFilter.Text = txtVeteranStatus.Text = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefreshVeteran_Click(object sender, EventArgs e)
    {
        ClearControlVeteranStatus();
        BindVeteranStatus();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgExpExcelVeteran_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItemsVeteranStatus.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN_VS"] == null) ? null : (ViewState["SORTBYCOLOUMN_VS"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION_VS"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION_VS"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_VeteranStatus("", "R", 0, 0, txtVeteranStatusFilter.Text != "" ? txtVeteranStatusFilter.Text : null, sortbycoloumn, sortdirection, null, null, ref  rowcount, ref Result);

            string[] HeaderCaptions = { "Veteran Status" };
            string[] DataColumnsName = { "VeteranStatus" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "VeteranStatus", "VeteranStatus", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
}