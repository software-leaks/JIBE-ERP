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

public partial class Technical_Vetting_Vetting_CreateNewVettingAttachmentType : System.Web.UI.Page
{
    public int Result = 0;
    BLL_VET_VettingLib objBLL = new BLL_VET_VettingLib();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

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

            DataTable dt = objBLL.VET_Get_VettingAttachmentTypeForLibrary(txtVetAttfilter.Text != "" ? txtVetAttfilter.Text : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            gvVettingAttachment.DataSource = dt;
            gvVettingAttachment.DataBind();
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
            txtVetAttfilter.Text = string.Empty;
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
            DataTable dt = objBLL.VET_Get_VettingAttachmentTypeForLibrary(txtVetAttfilter.Text != "" ? txtVetAttfilter.Text : null, sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            string[] HeaderCaptions = { "Attachment Type" };
            string[] DataColumnsName = { "Vetting_Attachmt_Type_Name" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "VettingAttachmentType" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), "Attachment Type", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvVettingAttachment_Sorting(object sender, GridViewSortEventArgs se)
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
                ImgVetAttAdd.Visible = false;

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
            objBLL.VET_Ins_Upd_Del_VettingTypeAttachment(Convert.ToInt32(e.CommandArgument.ToString()), "", GetSessionUserID(), "D", ref Result);

            if (Result == 0)
                BindGrid();


        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (hdnVetAttTypeID.Value != "")
            {

                if (txtVettingAttTypeName.Text.Trim() != "")
                {

                    objBLL.VET_Ins_Upd_Del_VettingTypeAttachment(Convert.ToInt32(hdnVetAttTypeID.Value), txtVettingAttTypeName.Text.Trim(), GetSessionUserID(), "U", ref Result);
                    if (Result < 0)
                    {                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", "alert('Attachment Type already exist.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetTitleonEdit", "<script type='text/javascript'>SetTitleonEdit();</script>", false);                      
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "showModal('divVetAtt', false);", true);

                    }
                    else if (Result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('Attachment Type Updated Successfully.')", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVetAtt", "hideModal('divVetAtt');", true);
                        BindGrid();
                    }
                }

            }
            else
            {

                if (txtVettingAttTypeName.Text.Trim() != "")
                {
                    objBLL.VET_Ins_Upd_Del_VettingTypeAttachment(0, txtVettingAttTypeName.Text.Trim(), GetSessionUserID(), "I", ref Result);
                    if (Result < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", "alert('Attachment Type already exist.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetTitleonAdd", "<script type='text/javascript'>SetTitleonAdd();</script>", false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "showModal('divVetAtt', false);", true);
                    }
                    else if (Result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('Attachment Type added Successfully.')", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UpdateVetAtt", "hideModal('divVetAtt');", true);
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
    protected void gvVettingAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEdit");
                Label lblVetAttTypeId = (Label)e.Row.FindControl("lblVetAttId");
                Label lblVetAttTypeName = (Label)e.Row.FindControl("lblVetAttachment");

                ImgEdit.Attributes.Add("onclick", "onEditClick(" + lblVetAttTypeId.Text + ",'" + lblVetAttTypeName.Text + "')");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}