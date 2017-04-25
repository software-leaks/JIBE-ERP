using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using SMS.Properties;

public partial class Infrastructure_Menu_AccessRightsCReport : System.Web.UI.Page
{
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        
        UserAccessValidation();
        if (!IsPostBack)
        {
            INF_Get_CompanyList();
            INF_Get_DepartmentList();
            INF_Get_UserList();
            INF_Get_MenuList();
            INF_Get_SubMenuList();
            INF_Get_PageListByMenu();
            
            Bind_AccessRightsReport();
        }
    }

    /// <summary>
    /// Method used to check user validations
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
            SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
            SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// method gets Session ID of current User
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
    /// Bind all company list
    /// </summary>
    public void INF_Get_CompanyList()
    {
        try
        {
            DDLCompany.DataSource = objMenuBLL.INF_Get_CompanyList();
            DDLCompany.DataTextField = "Company_Name";
            DDLCompany.DataValueField = "Id";
            DDLCompany.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind all department list
    /// </summary>
    public void INF_Get_DepartmentList()
    {
        try
        {
            DDLDepartment.DataSource = objMenuBLL.INF_Get_DepartmentList();
            DDLDepartment.DataTextField = "Dept_Name";
            DDLDepartment.DataValueField = "ID";
            DDLDepartment.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind all user list
    /// </summary>
    public void INF_Get_UserList()
    {
        try
        {
            DDLUser.DataSource = objMenuBLL.INF_Get_UserList(DDLCompany.SelectedValues,DDLDepartment.SelectedValues);
            DDLUser.DataTextField = "UserName";
            DDLUser.DataValueField = "UserID";
            DDLUser.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind all page list
    /// </summary>
    public void INF_Get_PageListByMenu()
    {
        try
        {
            DDLPageName.DataSource = objMenuBLL.INF_Get_PageList(DDLMenu.SelectedValues);
            DDLPageName.DataTextField = "Page_Name";
            DDLPageName.DataValueField = "Menu_Code";
            DDLPageName.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// display page list by sub menu
    /// </summary>
    public void INF_Get_PageListBySubMenu()
    {
        try
        {
            DDLPageName.DataSource = objMenuBLL.INF_Get_PageList(DDLSubMenu.SelectedValues);
            DDLPageName.DataTextField = "Page_Name";
            DDLPageName.DataValueField = "Menu_Code";
            DDLPageName.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }


    /// <summary>
    /// Bind all menu list
    /// </summary>
    public void INF_Get_MenuList()
    {
        try
        {
            DDLMenu.DataSource = objMenuBLL.INF_Get_MenuList();
            DDLMenu.DataTextField = "Menu";
            DDLMenu.DataValueField = "Menu_Code";
            DDLMenu.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind all sub menu list
    /// </summary>
    public void INF_Get_SubMenuList()
    {
        try
        {
            DDLSubMenu.DataSource = objMenuBLL.INF_Get_SubMenuList(DDLMenu.SelectedValues);
            DDLSubMenu.DataTextField = "Menu";
            DDLSubMenu.DataValueField = "Menu_Code";
            DDLSubMenu.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind onApplySearch of company
    /// </summary>
    public void BindDepartment_UserList()
    {
        try
        {
            INF_Get_DepartmentList();
            INF_Get_UserList();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind onApplySearch of Menu
    /// </summary>
    public void BindSubMenu_PageList()
    {
        try
        {
            INF_Get_SubMenuList();
            INF_Get_PageListByMenu();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// display access rights
    /// </summary>
    public void Bind_AccessRightsReport()
    {
        try
        {
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            ds = objMenuBLL.INF_Get_AccessRightsReport(DDLCompany.SelectedValues, DDLMenu.SelectedValues, DDLDepartment.SelectedValues, DDLSubMenu.SelectedValues,DDLUser.SelectedValues,DDLPageName.SelectedValues, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            gvAccessRightsReport.DataSource = ds.Tables[0];
            gvAccessRightsReport.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }

            UpdPnlGrid.Update();          

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ucCustomPagerItems.CurrentPageIndex = 1;
            Bind_AccessRightsReport();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLCompany.ClearSelection();
            DDLDepartment.ClearSelection();
            DDLMenu.ClearSelection();
            DDLMenu.ClearSelection();
            DDLSubMenu.ClearSelection();
            DDLUser.ClearSelection();
            DDLPageName.ClearSelection();
            Bind_AccessRightsReport();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            ds = objMenuBLL.INF_Get_AccessRightsReport(DDLCompany.SelectedValues, DDLMenu.SelectedValues, DDLDepartment.SelectedValues, DDLSubMenu.SelectedValues, DDLUser.SelectedValues, DDLPageName.SelectedValues, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

            string[] HeaderCaptions = { "User ID", "Full Name", "Department", "Company", "Last Login", "Station IP", "Menu","Sub Menu","Page Name","Page Description","Access to","Access Description","Last Changed","Changed by" };
            string[] DataColumnsName = { "userid", "Full_Name", "Department", "Company_Name", "Last_Login", "Station_IP",  "Menu", "Sub_Menu", "Page_Name", "Page_Description", "Access_to", "Access_Desp", "Last_changed", "Changed_by" };

            string FileName = "AccessRightsReport" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            string FilePath = Server.MapPath(@"~/Uploads\\Temp\\"); string ExportTempFilePath = Server.MapPath(@"~/Uploads\\ExportTemplete\\");

            if ((sender as ImageButton).CommandArgument == "ExportFrom_IE")
            {
                GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, FileName, "Access Rights Report", "");
            }
            else
            {
                GridViewExportUtil.Export_To_Excel_Interop(ds.Tables[0], HeaderCaptions, DataColumnsName, "Access Rights Report", FilePath + FileName, ExportTempFilePath, @"~\\Uploads\\Temp\\" + FileName);

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvAccessRightsReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = e.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            Bind_AccessRightsReport();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void gvAccessRightsReport_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Label lblLastLoginDate = (Label)e.Row.FindControl("lblLastLogin");
                Label lblLastChangedDate = (Label)e.Row.FindControl("lblLastChanged");
                Label lblAccessTo = (Label)e.Row.FindControl("lblAccessTo");
                Label lblAccessDescription = (Label)e.Row.FindControl("lblAccessDescription");
              
                string strAccTo=string.Empty;
                string[] strArrAccTo = lblAccessTo.Text.Split(',');
                for (int i = 0; i < strArrAccTo.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strArrAccTo[i].ToString()))
                    {
                        strAccTo += strArrAccTo[i].ToString() + ",";
                    }
                }
                lblAccessTo.Text = strAccTo.TrimEnd(',');

                string strAccDesp = string.Empty;
                string[] strArrAccDesp = lblAccessDescription.Text.Split('/');
                for (int i = 0; i < strArrAccDesp.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strArrAccDesp[i].ToString()))
                    {
                        strAccDesp += strArrAccDesp[i].ToString() + "/";
                    }
                }
                lblAccessDescription.Text = strAccDesp.TrimEnd('/');

                if (lblLastLoginDate.Text != "")
                {
                    lblLastLoginDate.Text = UDFLib.ConvertUserDateFormat(lblLastLoginDate.Text, UDFLib.GetDateFormat());
                }
                if (lblLastChangedDate.Text != "")
                {
                    lblLastChangedDate.Text = UDFLib.ConvertUserDateFormat(lblLastChangedDate.Text, UDFLib.GetDateFormat());
                }


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
}