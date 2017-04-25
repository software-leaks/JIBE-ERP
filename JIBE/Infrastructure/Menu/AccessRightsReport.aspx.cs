using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.Services;

public partial class Infrastructure_Menu_AccessRightsReport : System.Web.UI.Page
{
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        caltxtFrom.Format = caltxtTo.Format = UDFLib.GetDateFormat(); //Get the Current Date format of user
        if (!IsPostBack)
        {
            ViewState["SORTADIRECTION"] = 1;
            ViewState["SORTBYACOLOUMN"] = null;
            txtTo.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.ToString()); 
            txtFrom.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.Date.AddDays(-30).ToString());

            string URL12 = String.Format("showprogressbar();"); 
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "URL12", URL12, true);

            BindAccessGrid();
            BindModule();
            BindUsers();
        }
    }
    //Method used to Bind Main Module names to the drop down list.
    public void BindModule()
    {
        DataTable dt= objMenuBLL.GetCollection_AllModules();
        if(dt != null)
            if (dt.Rows.Count > 0)
            {
                ddlModule.DataSource = dt;
                ddlModule.DataTextField = "Menu_Short_Discription";
                ddlModule.DataValueField = "mcodeseq";
                ddlModule.DataBind();
            }
        ddlModule.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    //Method used to bind all Users to the drop down list.
    public void BindUsers()
    {
            BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
            int iCompID = UDFLib.ConvertToInteger(ddlUser.SelectedValue);
            DataTable dtfilter = objUserBLL.Get_UserList(iCompID);
            if (dtfilter != null)
                if (dtfilter.Rows.Count > 0)
                 {
                     ddlUser.Items.Clear();
                     ddlUser.DataSource = dtfilter;
                     ddlUser.DataTextField = "UserName";
                     ddlUser.DataValueField = "UserID";
                     ddlUser.DataBind();
                 }
            ddlUser.Items.Insert(0, new ListItem("-SELECT-", "0"));      
            }
    //Method used to check user validations
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
    //method gets Session ID of current User
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Method is called at the time of binding records from database to grid
    /// </summary>  
    public void BindAccessGrid()
    {
        //string URL = String.Format("showprogressbar();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "URL", URL, true);
        try
        {
            int Count = ucCustomPagerItems.isCountRecord;
            //Take the values of Sorting order and sort column name
            string sortbycoloumn = (ViewState["SORTBYACOLOUMN"] == null) ? null : (ViewState["SORTBYACOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTADIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTADIRECTION"].ToString());
        
            DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtFrom.Text));
            DateTime dt2 = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTo.Text));

            string moduleName = "";
            if ((ddlModule.SelectedItem == null) || (ddlModule.SelectedItem.ToString() == "-SELECT-"))
            {
                moduleName = "";
            }
            else
                moduleName = ddlModule.SelectedItem.ToString();
            
            DataSet dt = objMenuBLL.Get_AccessRightChanges(UDFLib.ConvertDateToNull(dt1), UDFLib.ConvertDateToNull(dt2), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, sortbycoloumn, sortdirection,
                UDFLib.ConvertIntegerToNull(ddlUser.SelectedValue), UDFLib.ConvertStringToNull(moduleName), txtPage.Text, int.Parse(Session["USERID"].ToString()), ref Count);
        
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = Count.ToString();
                ucCustomPagerItems.BuildPager();
            }
            if (dt.Tables[0].Rows.Count > 0)
            {
                grdAccessReport.DataSource = dt;
                grdAccessReport.DataBind();
            }
            else
            {
                grdAccessReport.DataSource = dt;
                grdAccessReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is called at the time of sorting as per columns
    /// </summary>    
    protected void grdAccessReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYACOLOUMN"] = e.SortExpression;
        //As per sorting expressing set Column name and Sorting Order in Viewsatate for further use.
        if (ViewState["SORTADIRECTION"] != null && ViewState["SORTADIRECTION"].ToString() == "0")
            ViewState["SORTADIRECTION"] = 1;
        else
            ViewState["SORTADIRECTION"] = 0;

        BindAccessGrid();
    }
    /// <summary>
    /// Method is called at the time of binding rows to the database
    /// </summary>    
    protected void grdAccessReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYACOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYACOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTADIRECTION"] == null || ViewState["SORTADIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }               
    }
    /// <summary>
    /// Method called To Reset all filters
    /// </summary>    
    protected void imgBtnrefresh_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["SORTADIRECTION"] = null;
        ViewState["SORTBYACOLOUMN"] = null;
        ddlModule.SelectedValue = "0";
        ddlUser.SelectedValue = "0";
        txtPage.Text = "";
        txtTo.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.ToString());
        txtFrom.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.Date.AddDays(-30).ToString());
        BindAccessGrid();
    }
    /// <summary>
    /// Method called when Export button is clicked
    /// </summary>    
    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        //Check whether date is proper
        if (txtFrom.Text.Trim() != "")
        {
            try
            {
                DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtFrom.Text));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('From date is invalid.')", true);
                return;
            }
        }
        if (txtTo.Text.Trim() != "")
        {
            try
            {
                DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTo.Text));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('To date is invalid.')", true);
                return;
            }
        }
        //Compare From and To date
        if (DateTime.Compare(DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTo.Text)), DateTime.Parse(UDFLib.ConvertToDefaultDt(txtFrom.Text))) < 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('From date must be less than To date.')", true);
            return;
        }
        try
        {
            int Count = 1; string moduleName = "";
            if (ddlModule.SelectedItem.ToString() == "-SELECT-")
            {
                moduleName = "";
            }
            else
                moduleName = ddlModule.SelectedItem.ToString();
            //Take the values of Sorting order and sort column name
            string sortbycoloumn = (ViewState["SORTBYACOLOUMN"] == null) ? null : (ViewState["SORTBYACOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTADIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTADIRECTION"].ToString());

            DataSet dt = objMenuBLL.Get_AccessRightChanges(UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(txtFrom.Text)), UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(txtTo.Text)), null, null, sortbycoloumn, sortdirection,
               UDFLib.ConvertIntegerToNull(ddlUser.SelectedValue), UDFLib.ConvertStringToNull(moduleName), txtPage.Text, int.Parse(Session["USERID"].ToString()), ref Count);

            UDFLib.ChangeColumnDataType(dt.Tables[0], "Date", typeof(string));

            if (dt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Tables[0].Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Date"].ToString()))
                    {
                        dr["Date"] = "&nbsp;"+ UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["Date"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                    }
                }
                string fileHeader = "<table><tr style='font-weight:bold;'><td  colspan='8'><center><h1>" + "Access Rights Changes Report</h1></center></td><td style='font-weight:bold;'>Client Name:</td><td>" + dt.Tables[1].Rows[0]["Company_Name"].ToString() + "</td></tr>";
                fileHeader += "<tr><td style='font-weight:bold;'>From Date:</td><td>&nbsp;" + txtFrom.Text + "</td><td style='font-weight:bold;'>To Date:</td><td>&nbsp;" + txtTo.Text + "</td><td style='font-weight:bold;'>Selected User:</td><td>"
                    + (ddlUser.SelectedItem.ToString() == "-SELECT-" ? "All" : ddlUser.SelectedItem.ToString()) + "</td><td style='font-weight:bold;'>Selected Module:</td><td>" + (ddlModule.SelectedItem.ToString() == "-SELECT-" ? "All" : ddlModule.SelectedItem.ToString()) + "</td><td style='font-weight:bold;'>Page:</td><td>" + txtPage.Text + "</td></tr>";

                fileHeader += "</table>";
                string[] HeaderCaptions = { "Date", "User Name", "First Name", "Last Name", "Module", "Page Description", "Page Link", "Changes", "Changed By", "User IP" };
                string[] DataColumnsName = { "Date", "User_name", "First_Name", "Last_Name", "Module", "Description", "Page", "Changes", "Chaged_By", "UserIP" };

                GridViewExportUtil.ShowExcel(dt.Tables[0], HeaderCaptions, DataColumnsName, "AccessReport", fileHeader, "");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method called when Search button is clicked
    /// </summary>    
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        //Check whether date is proper
        if (txtFrom.Text.Trim() != "")
        {
            try
            {
                DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtFrom.Text));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('From date is invalid.')", true);
                return;
            }
        }
        if (txtTo.Text.Trim() != "")
        {
            try
            {
                DateTime dt1 = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTo.Text));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('To date is invalid.')", true);
                return;
            }
        }
        //Compare From and To date
        if (DateTime.Compare( DateTime.Parse(UDFLib.ConvertToDefaultDt(txtTo.Text)),DateTime.Parse(UDFLib.ConvertToDefaultDt(txtFrom.Text)))<0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nature", "alert('From date must be less than To date.')", true);
            return;
        }
        BindAccessGrid();
        ucCustomPagerItems.CurrentPageIndex = 1;
        ucCustomPagerItems.DataBind();
    }
    //Method used to show tooltip data
    [WebMethod]
    public static string SearchMenuData(int menuID)
    {        
        string tblaccess = "";
        try
        {
            BLL_Infra_MenuManagement obj = new BLL_Infra_MenuManagement();
            DataTable dt = obj.Get_AccessRightAll(menuID);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    tblaccess = dt.Rows[0]["Remarks"].ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

        return tblaccess;
    }

// get newly changed record in to snapshot table 
    protected void imgBtnRunDaemon_Click(object sender, ImageClickEventArgs e)
    {
        objMenuBLL.Get_AccessRightChanges();
    }
}