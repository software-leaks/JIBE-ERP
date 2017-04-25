using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.INFRA.Infrastructure;
using System.Data.SqlClient;

/// <summary>
/// Created by Anjali :DT:16-06-2016
/// </summary>
public partial class Infrastructure_Libraries_PMS_Access : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_PMS_Access objBLL = new BLL_PMS_Access();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    /// <summary>
    /// Load event of page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            Load_RankList();

            BindPMS_Access();
        }
    }
    /// <summary>
    /// To check acces for logged in user.
    /// like Add,Edit ,delete access for requested page.
    /// </summary>
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

        if (objUA.Delete == 1) uaDeleteFlage = true;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// To load rank in drop down list
    /// </summary>
    public void Load_RankList()
    {
        try
        {
            DataTable dt = objCrewAdmin.Get_RankList();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlRankName.DataSource = dt;
                    ddlRankName.DataTextField = "Rank_Short_Name";
                    ddlRankName.DataValueField = "ID";
                    ddlRankName.DataBind();
                    ddlRankName.SelectedIndex = 0;
                }
            }
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind grid with declared ranks with their access rights data.
    /// </summary>
    public void BindPMS_Access()
    {
        try
        {

            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.SearchPMSAccess(txtSearch.Text.Trim(), UDFLib.ConvertStringToNull(ddlAction_Type.SelectedValue), sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            gvPMSAccess.DataSource = dt;
            gvPMSAccess.DataBind();

            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Add new access rights.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlRankName");
        HiddenFlag.Value = "Add";
        ddlRankName.SelectedIndex = 0;
        ddlActionType.SelectedIndex = 0;

        OperationMode = "Add PMS Access";
        lblMsg.Text = "";
        ddlActionType.Enabled = true;

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
    }

    /// <summary>
    /// Update ranks for already declared access rights.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit PMS Access";
            lblMsg.Text = "";
            DataSet ds = new DataSet();

            ds = objBLL.Get_PMSAccessList(Convert.ToInt32(e.CommandArgument.ToString()));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAccessID.Text = e.CommandArgument.ToString();
                    ddlRankName.SelectedValue = ds.Tables[0].Rows[0]["Rank_ID"].ToString();
                    ddlActionType.SelectedValue = ds.Tables[0].Rows[0]["Action_Type"].ToString();
                    ddlActionType.Enabled = false;

                    string Deptmodal = String.Format("showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);
                }
            }
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Delete the rights.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            objBLL.Insert_Update_Delete_PMS_Access(null, Convert.ToInt32(e.CommandArgument.ToString()), null, Convert.ToInt32(Session["USERID"]), Convert.ToChar("D"));
            string js = "alert('Access deleted successfully.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

            BindPMS_Access();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Save access rights.Call to function to save.|| Event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateControls())
            {
                SavePMS_Access();
            }
        }
        catch (SqlException ex)
        {

            lblMsg.Text = ex.Message.ToString();
            string AddDeptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }

    }

    /// <summary>
    /// Save access rights.
    /// </summary>
    private void SavePMS_Access()
    {
        try
        {
            if (HiddenFlag.Value == "Add")
            {

                objBLL.Insert_Update_Delete_PMS_Access(UDFLib.ConvertIntegerToNull(ddlRankName.SelectedValue), UDFLib.ConvertIntegerToNull(txtAccessID.Text), UDFLib.ConvertStringToNull(ddlActionType.SelectedValue),
                                                        Convert.ToInt32(Session["USERID"]), Convert.ToChar("A"));


                string jsSqlError2 = "alert('Access saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);

            }
            else
            {
                objBLL.Insert_Update_Delete_PMS_Access(UDFLib.ConvertIntegerToNull(ddlRankName.SelectedValue), UDFLib.ConvertIntegerToNull(txtAccessID.Text), UDFLib.ConvertStringToNull(ddlActionType.SelectedValue),
                                                        Convert.ToInt32(Session["USERID"]), Convert.ToChar("U"));

                string jsSqlError3 = "alert('Access updated successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);

            }

            BindPMS_Access();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// To validate controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        try
        {
            if (ddlActionType.SelectedValue == "0")
            {
                string js = "alert('Please select action type.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return false;
            }
            if (ddlRankName.SelectedIndex <= 0)
            {
                string js = "alert('Please select rank.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.CurrentPageIndex = 1;
        BindPMS_Access();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        txtSearch.Text = "";
        ddlAction_Type.SelectedValue = "0";
        BindPMS_Access();
    }

    /// <summary>
    /// Excel export.
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


            DataTable dt = objBLL.SearchPMSAccess(txtSearch.Text.Trim(), UDFLib.ConvertStringToNull(ddlAction_Type.SelectedValue), sortbycoloumn, sortdirection, null, null, ref  rowcount);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string[] HeaderCaptions = { "Rank Name", "Action Type" };
                    string[] DataColumnsName = { "Rank_Name", "Action_Type" };

                    GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "PMS_Access", "PMS Access");
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }

    }

    protected void gvPMSAccess_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvPMSAccess_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindPMS_Access();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}