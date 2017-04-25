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
public partial class Technical_Vetting_Vetting_VettingType : System.Web.UI.Page
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
            DataTable dt = objBLL.VET_Get_VettingTypForLibrary(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            gvVettingType.DataSource = dt;
            gvVettingType.DataBind();
            if (dt.Rows.Count > 0)
            {

                ImgExpExcel.Visible = true;
            }
            else
            {

                ImgExpExcel.Visible = false;
            }

            if (dt.Rows.Count == 4)
            {
                ImgAdd.Enabled = false;
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
            DataTable dt = objBLL.VET_Get_VettingTypForLibrary(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            string[] HeaderCaptions = { "Vetting Type Name ", "Expires In Days", "Is Internal" };
            string[] DataColumnsName = { "Vetting_Type_Name", "Expires_In_Days", "IsInternal" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "VettingType" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), "Vetting Type", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvVettingType_Sorting(object sender, GridViewSortEventArgs se)
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
            objBLL.VET_Del_Vetting_Type(Convert.ToInt32(e.CommandArgument.ToString()), GetSessionUserID(), ref Result);

            if (Result == 0)
                BindGrid();


        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ExInDays = -1;
            if (chkIsApp.Checked == false)
            {
                ExInDays = -1;
            }
            else
            {
                ExInDays = UDFLib.ConvertToInteger(txtExInDays.Text);
            }
            if (hdnVetTypeID.Value != "")
            {

                if (txtVettingTypeName.Text.Trim() != "")
                {

                    objBLL.VET_Upd_VettingType(Convert.ToInt32(hdnVetTypeID.Value), txtVettingTypeName.Text.Trim(), ExInDays, chkEIsInternal.Checked == true ? 1 : 0, chkActive.Checked == true ? 1 : 0, GetSessionUserID(), chkIsApp.Checked == true ? 1 : 0, ref Result);
                    if (Result < 0)
                    {
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetTitleonEdit", "<script type='text/javascript'>SetTitleonEdit();</script>", false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "showModal('divVettingType', false);", true);

                    }
                    else if (Result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('Vetting Type Updated Successfully')", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists1", "hideModal('divVettingType');", true);
                        BindGrid();
                    }
                }

            }
            else
            {

                if (txtVettingTypeName.Text.Trim() != "")
                {
                    objBLL.VET_Ins_VettingType(txtVettingTypeName.Text.Trim(), ExInDays, chkEIsInternal.Checked == true ? 1 : 0, chkActive.Checked == true ? 1 : 0, GetSessionUserID(), chkIsApp.Checked == true ? 1 : 0, ref Result);
                    if (Result < 0)
                    {
                        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetTitleonAdd", "<script type='text/javascript'>SetTitleonAdd();</script>", false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "showModal('divVettingType', false);", true);
                    }
                    else if (Result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('Vetting Type Updated Successfully')", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists1", "hideModal('divVettingType');", true);
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

    protected void gvVettingType_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Label lblVetTypeId = (Label)e.Row.FindControl("lblVetTypeId");
                Label lblVetTypeName = (Label)e.Row.FindControl("lblVetTypeName");
                Label lblExInDays = (Label)e.Row.FindControl("lblExInDays");
                Label lblIsActive = (Label)e.Row.FindControl("lblIsActive");
                CheckBox chkIsInternal = (CheckBox)e.Row.FindControl("chkIsInternal");
                string isInternal = "False";
                if (chkIsInternal.Checked == true)
                {
                    isInternal = "True";
                }
                else
                {
                    isInternal = "False";
                }

                string ExinDays = string.Empty;
                ExinDays = lblExInDays.Text;

                ImgEdit.Attributes.Add("onclick", "onEditClick(" + lblVetTypeId.Text + ",'" + lblVetTypeName.Text + "','" + lblExInDays.Text + "','" + isInternal + "','" + lblIsActive.Text + "')");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void chkIsApp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsApp.Checked == true)
        {
            txtExInDays.Enabled = true;
        }
        else
        {
            txtExInDays.Enabled = false;
        }
    }
}