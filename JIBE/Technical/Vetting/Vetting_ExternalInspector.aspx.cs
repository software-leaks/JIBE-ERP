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
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Technical_Vetting_Vetting_ExternalInspector : System.Web.UI.Page
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
                    HiddenFlagAdd.Value = "Add";
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
    /// Method is used to bind inspector details in gridview
    /// </summary>
    public void BindGrid()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataTable dt = objBLL.VET_Get_ExternalInspector(txtfilter.Text != "" ? txtfilter.Text.Trim() : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }           
                gvExternalInspector.DataSource = dt;
                gvExternalInspector.DataBind();
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

            DataTable dt = objBLL.VET_Export_ExternaInspector();
            string[] HeaderCaptions = { "Inspector Name", "Company Name", "Document Type", "Document Number", "Vetting Type" };
            string[] DataColumnsName = { "Inspector_Name", "Company_Name", "Document_Type", "Document_Number", "Vetting_Type_Name" };
            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ExternalInspector" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), "Inspector Details", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void gvExternalInspector_Sorting(object sender, GridViewSortEventArgs se)
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
    /// <summary>
    /// Delete inspector
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            objBLL.VET_DEL_ExternalInspector(Convert.ToInt32(e.CommandArgument.ToString()), GetSessionUserID(), ref Result);
            if (Result == 0)
            {
                BindGrid();
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }



    protected void gvExternalInspector_RowDataBound(object sender, GridViewRowEventArgs e)
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
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
   
}