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
using System.IO;
public partial class Crew_Libraries_OilMajors : System.Web.UI.Page
{
    #region Decalrations


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
            OperationMode = "Add/Edit Oil Major";

            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;

                BindOilMajors();
            }

            txtRemarks.config.toolbar = new object[]
            {
                new object[] { "Preview"},
                new object[] { "Cut", "Copy", "Paste", "PasteText", "-", "Print", "SpellChecker" },
                new object[] { "Undo", "Redo", "-", "Find", "Replace", "-"},
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                "/",
                new object[] { "HorizontalRule" },
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" },
                new object[] { "Maximize", "ShowBlocks"}
            };
        }
        catch { 
        
        
        }
    }

    /// <summary>
    /// Bind Oil Majors
    /// </summary>
    public void BindOilMajors()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_OilMajors("", "", "R", 0, 0, txtfilter.Text != "" ? txtfilter.Text.Trim() : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtRemarks.Text, 0, "", ref  rowcount, ref Result);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            gvOilMajors.DataSource = dt;
            gvOilMajors.DataBind();

            if (dt.Rows.Count > 0)
            {               
                ImgExpExcel.Visible = true;
            }
            else
            {              
                ImgExpExcel.Visible = false;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }


    protected void onEdit(object source, CommandEventArgs e)
    {
        ImageButton Imgbtn = (ImageButton)source;

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

    
        BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
        DataTable dt = objBLL.CRUD_OilMajors("", "", "R", 0, Convert.ToInt32(Imgbtn.Attributes["rel"]), "", sortbycoloumn, sortdirection
         , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, txtRemarks.Text, 0, "", ref rowcount, ref Result);
        string imagefile = "";
        if (dt.Rows.Count > 0)
        {
            hdnOilMajorID.Value = Convert.ToString(dt.Rows[0]["ID"]);
            txtOilMajorName.Text = Convert.ToString(dt.Rows[0]["Oil_Major_Name"]);
            txtDisplayName.Text = Convert.ToString(dt.Rows[0]["Display_Name"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["Remarks"]);
            imagefile = Convert.ToString(dt.Rows[0]["Oil_Major_Logo"]);

            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/OilMajorLogo/"));
            List<ListItem> files = new List<ListItem>();
            string fileName = Path.GetFileName(imagefile);
            files.Add(new ListItem(fileName, "~/Uploads/OilMajorLogo/" + fileName));
          
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditControl", " $(\"#divadd_dvModalPopupTitle\").text(\"Add/Edit Oil Major\"); showModal('divadd', false);", true);
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
            DataTable dt = objBLL.CRUD_OilMajors("", "", "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), null, "", null, null, null, "", 0, "", ref  rowcount, ref Result);
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindOilMajors();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void ImgAddOnClick(object sender, EventArgs e)
    {
        txtRemarks.Text = "";
        txtOilMajorName.Text = "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "shownewpopup", "showModal('divadd',false);", true);
    }


    /// <summary>
    /// To save or update oil major
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_OnClick(object sender, EventArgs e)
    {
        string path = "";
        int ActiveStatus = 0;
        if (chkIsActive.Checked == true)
            ActiveStatus = 1;
        else
            ActiveStatus = 0;
        try
        {
            if (hdnUploadFileName.Value != "")
            {
                FileInfo fn = new FileInfo(hdnUploadFileName.Value);
                Guid gid = Guid.NewGuid();
                string filename = "OLM_" + gid + fn.Extension;
                path = "~/Uploads/OilMajorLogo/" + filename;
                
                FileUpload1.SaveAs(Server.MapPath("~/Uploads/OilMajorLogo/" + filename));
                
            }
            if (txtDisplayName.Text == "")
                txtDisplayName.Text = txtOilMajorName.Text.Trim();
            if (Convert.ToInt32(hdnOilMajorID.Value) > 0)
            {
                OperationMode = "Add/Edit Oil Major";

                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_OilMajors(txtOilMajorName.Text.Trim(), txtDisplayName.Text.Trim(), "U", GetSessionUserID(), Convert.ToInt32(hdnOilMajorID.Value), null, "", null, null, null, txtRemarks.Text, ActiveStatus, path, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("InformationMessage/DataExists") + "');showModal('divadd', false);", true);
               if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divadd');", true);
                    hdnUploadFileName.Value = "";
                    BindOilMajors();
                }
            }
            else
            {
                OperationMode = "Add Oil Major";
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_OilMajors(txtOilMajorName.Text.Trim(), txtDisplayName.Text.Trim(), "I", GetSessionUserID(), 0, null, "", null, null, null, txtRemarks.Text, ActiveStatus, path, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("InformationMessage/DataExists") + "');showModal('divadd', false);", true);
                if (Result > 0)
                {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divadd');", true);
                    BindOilMajors();
                }
            }
            // it will clear all the values and avoid adding the Duplicate records !Desc:- when one intend to page refreseh ,javascript alert msg raise, says it will perform the same action again,so value might gets duplicate , so we can avoid this by below code
            //Response.Redirect(Request.Url.AbsoluteUri);
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
    protected void gvOilMajors_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Label lblRemark = (Label)e.Row.FindControl("lblRemarks");
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Remarks")) == "")
                    lblRemark.Text = "";
              
                Image img = (Image)e.Row.FindControl("imgOilMajor");
                ImageButton ImgDelete = (ImageButton)e.Row.FindControl("ImgDelete");
                 HiddenField hdnActiveStatus = (HiddenField)e.Row.FindControl("hdnActiveStatus");
                   
               

                if (hdnActiveStatus.Value == "1")
                {
                    ImgDelete.Visible = true;
                }
                else
                {
                    ImgDelete.Visible = false;
                }

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
    protected void gvOilMajors_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindOilMajors();
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
            BindOilMajors();
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
        txtfilter.Text = txtOilMajorName.Text = string.Empty;

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
            BindOilMajors();
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

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRW_LIB_Export_OilMajor();

            if (dt.Rows.Count > 0)
            {
                string[] HeaderCaptions = { "Oil Major Name", "Display Name", "Remark", "Active Status" };
                string[] DataColumnsName = { "Oil_Major_Name", "Display_Name", "Remarks", "Active Status" };

                GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "OilMajors" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), "Oil Majors", "");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {

        if (UDFLib.ConvertToInteger(hdnOilMajorID.Value) > 0)
        {
            string msg4 = string.Format("document.getElementById('iFrmCopyJobs').src ='../Crew/Libraries/OM_FileUploader.aspx?Oil_MajorId=" + hdnOilMajorID.Value);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg4", msg4, true);
        }
        else
        {
            string msg4 = string.Format("alert('Please select or save job !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jobsavemsg", msg4, true);
        }
    }

}