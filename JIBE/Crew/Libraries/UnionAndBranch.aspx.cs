using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Crew_UnionAndBranch : System.Web.UI.Page
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
            Page.Title = "Union, Union Branch and Union Book Library";
            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ViewState["SORTDIRECTION_VS"] = null;
                ViewState["SORTBYCOLOUMN_VS"] = null;
                ucCustomPagerItems.PageSize = 20;
                BindUnion();
                Session["UNIONID"] = null;
            }
        }
        catch { }
    }

    /// <summary>
    /// Bind Union
    /// </summary>
    public void BindUnion()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataSet dt = objBLL.CRUD_Union("", "R", 0, 0, txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ref Result);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Tables[0].Rows.Count > 0)
            {
                gvUnion.DataSource = dt.Tables[0];
                gvUnion.DataBind();

                drpUnion.DataSource = dt.Tables[0];
                drpUnion.DataTextField = "UnionName";
                drpUnion.DataValueField = "ID";
                drpUnion.DataBind();
                drpUnion.Items.Insert(0, new ListItem() { Text = "-Select-", Value = "0" });

                divBranch.Visible = false;
                gvUnionBranch.DataSource = null;
                gvUnionBranch.DataBind();
            }
            else
            {
                gvUnion.DataSource = null;
                gvUnion.DataBind();
            }
            if (dt.Tables[1].Rows.Count > 0)
            {
                drpCountry.DataSource = dt.Tables[1];
                drpCountry.DataTextField = "Country_Name";
                drpCountry.DataValueField = "ID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem() { Text = "-Select-", Value = "0" });

                dt.Tables[1].DefaultView.RowFilter = "";
                dt.Tables[1].DefaultView.RowFilter = "ISO_Code='US'";
                if (dt.Tables[1].DefaultView.Count > 0)
                    hdnUSCountryID.Value = Convert.ToString(dt.Tables[1].DefaultView[0]["ID"]);
            }
            if (dt.Tables[2].Rows.Count > 0)
            {
                ///0- US Client
                ///1- International Client
                if (Convert.ToInt32(dt.Tables[2].Rows[0]["Value"]) == 0)
                {
                    hdnAddressType.Value = "0";
                    trUSAddress.Visible = true;
                    trInternationAddress.Visible = false;
                }
                else
                {
                    hdnAddressType.Value = "1";
                    trInternationAddress.Visible = true;
                    trUSAddress.Visible = false;
                }
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
            DataTable dt = objBLL.CRUD_Union("", "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), null, "", null, null, null, ref  rowcount, ref Result).Tables[0];
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindUnion();
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
            if (Convert.ToInt32(hdnUnionID.Value) > 0)
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_Union(txtUnion.Text.Trim(), "U", GetSessionUserID(), Convert.ToInt32(hdnUnionID.Value), null, "", null, null, null, ref rowcount, ref Result).Tables[0];

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Union already exist in the system');showModal('divadd', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divadd');", true);
                    BindUnion();
                }
            }
            else
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_Union(txtUnion.Text.Trim(), "I", GetSessionUserID(), 0, null, "", null, null, null, ref rowcount, ref Result).Tables[0];

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Union already exist in the system');showModal('divadd', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divadd');", true);
                    BindUnion();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvUnion_RowDataBound(object sender, GridViewRowEventArgs e)
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
                LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("LinkButton1");
                LinkButton1.ToolTip = LinkButton1.Text;
                if (LinkButton1.Text.Length>40)
                    LinkButton1.Text = LinkButton1.Text.Substring(0, 40) + "...";
                
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
    protected void gvUnion_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindUnion();
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
                btnSaveUnionBranch.Visible = btnSaveUnionBook.Visible = btnsave.Visible = false;

            if (objUA.Add == 0)
            {
                ImgAdd.Visible = false;
                ImgbtnAddBranch.Visible = false;
                ImgBtnAddNewUnionBook.Visible = false;
            }
            else
            {
                btnSaveUnionBranch.Visible = btnSaveUnionBook.Visible = btnsave.Visible = true;
            }

            if (objUA.Delete == 1) { uaDeleteFlage = true; }
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
        BindUnion();
    }

    protected void ImgbtnFilterBranch_Click(object sender, EventArgs e)
    {
        BindUnionBranch();
    }

    /// <summary>
    /// Clear All controls
    /// </summary>
    private void ClearControl()
    {
        ViewState["SORTBYCOLOUMN"] = null;
        ViewState["SORTDIRECTION"] = null;
        ucCustomPagerItems.CurrentPageIndex = 1;
        txtfilter.Text = txtUnion.Text = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Session["UNIONID"] = null;
        ClearControl();
        BindUnion();
        divBranch.Visible = false;
    }

    protected void BindUnionBranchsOnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            foreach (GridViewRow row in gvUnion.Rows)
                row.Style.Remove("background-color");

            GridViewRow gvr = (GridViewRow)lnkbtn.NamingContainer;
            Session["UNIONID"] = Convert.ToInt32(lnkbtn.CommandArgument);
            gvr.Style.Add("background-color", "#FEECEC");

            lblUnionName.Text = lnkbtn.Text;
            lblUnionName.ToolTip = lnkbtn.ToolTip;

            ViewState["SORTBYCOLOUMN_UNIONBRANCH"] = null;
            ViewState["SORTDIRECTION_UNIONBRANCH"] = null;

            txtUnionBranchFilter.Text = "";
            BindUnionBranch();
            divBranch.Visible = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void BindUnionBranch()
    {
        try
        {
            int rowcount = ucCustomPagerItemsBranch.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN_UNIONBRANCH"] == null) ? null : (ViewState["SORTBYCOLOUMN_UNIONBRANCH"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION_UNIONBRANCH"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION_UNIONBRANCH"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataSet dt = objBLL.CRUD_UnionBranch("", 0, Convert.ToInt32(Session["UNIONID"]), "", "", "", "", 0, "", "", "", "R", GetSessionUserID(), txtUnionBranchFilter.Text != "" ? txtUnionBranchFilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItemsBranch.CurrentPageIndex, ucCustomPagerItemsBranch.PageSize, ref  rowcount, ref Result);

            if (ucCustomPagerItemsBranch.isCountRecord == 1)
            {
                ucCustomPagerItemsBranch.CountTotalRec = rowcount.ToString();
                ucCustomPagerItemsBranch.BuildPager();
            }

            if (dt.Tables[0].Rows.Count > 0)
            {
                gvUnionBranch.DataSource = dt.Tables[0];
                gvUnionBranch.DataBind();
            }
            else
            {
                gvUnionBranch.DataSource = null;
                gvUnionBranch.DataBind();
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void gvUnionBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SORTBYCOLOUMN_UNIONBRANCH"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN_UNIONBRANCH"].ToString());
                    if (img != null)
                    {
                        if (ViewState["SORTDIRECTION_UNIONBRANCH"] == null || ViewState["SORTDIRECTION_UNIONBRANCH"].ToString() == "0")
                            img.Src = "~/purchase/Image/arrowUp.png";
                        else
                            img.Src = "~/purchase/Image/arrowDown.png";

                        img.Visible = true;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrAddress = (Literal)e.Row.FindControl("ltrAddress");
                StringBuilder strAddress = new StringBuilder();
                string Rel = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ID"));

                Label lblUnion = (Label)e.Row.FindControl("lblUnion");
                lblUnion.ToolTip = lblUnion.Text;
                if (lblUnion.Text.Length>20)
                    lblUnion.Text = lblUnion.Text.Substring(0, 20) + "...";

                if (hdnAddressType.Value == "0")//US Client
                {
                    strAddress.Append("<table class='tblAddress'><tr><td  width=\"99px\"><b>Address Line 1</b></td><td>:</td><td class='lblAddressLine1' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AddressLine1")) + "</td></tr>");
                    strAddress.Append("<tr><td><b>Address Line 2</b></td><td>:</td><td class='lblAddressLine2' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AddressLine2")) + "</td></tr>");
                    strAddress.Append("<tr><td><b>City</b></td><td>:</td><td class='lblCity' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "City")) + "</td></tr>");
                    strAddress.Append("<tr><td><b>State</b></td><td>:</td><td class='lblState' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "State")) + "</td></tr>");
                    strAddress.Append("<tr><td><b>Country</b></td><td>:</td><td class='lblCountry' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Country")) + "</td></tr>");
                    strAddress.Append("<tr><td><b>ZipCode</b></td><td>:</td><td class='lblZipCode' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ZipCode")) + "</td></tr></table>");
                    ltrAddress.Text = strAddress.ToString();
                }
                else
                {
                    if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Address")) != "")
                        ltrAddress.Text = "<span class='lblAddress' rel='" + Rel + "'>" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Address")) + "<span>";
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
    protected void gvUnionBranch_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN_UNIONBRANCH"] = se.SortExpression;
            if (ViewState["SORTDIRECTION_UNIONBRANCH"] != null && ViewState["SORTDIRECTION_UNIONBRANCH"].ToString() == "0")
                ViewState["SORTDIRECTION_UNIONBRANCH"] = 1;
            else
                ViewState["SORTDIRECTION_UNIONBRANCH"] = 0;
            BindUnionBranch();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void onDeleteBranch(object source, CommandEventArgs e)
    {
        try
        {
            int rowcount = 0;
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_UnionBranch("", Convert.ToInt32(e.CommandArgument.ToString()), 0, "", "", "", "", 0, "", "", "", "D", GetSessionUserID(), "", "", null, null, null, ref  rowcount, ref Result).Tables[0];
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindUnionBranch();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnSaveUnionBranch_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            int rowcount = 0;

            if (Convert.ToInt32(hdnUnionBranchID.Value) > 0)
            {
                if (hdnAddressType.Value == "1")
                    dt = objBLL.CRUD_UnionBranchUS(txtUnionBranch.Text.Trim(), Convert.ToInt32(hdnUnionBranchID.Value), Convert.ToInt32(drpUnion.SelectedValue), txtAddress.Text.Trim(), Convert.ToInt32(drpCountry.SelectedValue), txtPhoneNumber.Text.Trim(), txtEmail.Text.Trim(), "U", GetSessionUserID(), "", "", null, null, null, ref rowcount, ref Result).Tables[0];
                else
                    dt = objBLL.CRUD_UnionBranch(txtUnionBranch.Text.Trim(), Convert.ToInt32(hdnUnionBranchID.Value), Convert.ToInt32(drpUnion.SelectedValue), txtAddressLine1.Text.Trim(), txtAddressLine2.Text.Trim(), txtCity.Text.Trim(), txtState.Text, Convert.ToInt32(drpCountry.SelectedValue), txtZipCode.Text.Trim(), txtPhoneNumber.Text.Trim(), txtEmail.Text.Trim(), "U", GetSessionUserID(), "", "", null, null, null, ref rowcount, ref Result).Tables[0];

                if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divAddUnionBranch');", true);
                }
            }
            else
            {
                if (hdnAddressType.Value == "1")
                    dt = objBLL.CRUD_UnionBranchUS(txtUnionBranch.Text.Trim(), 0, Convert.ToInt32(drpUnion.SelectedValue), txtAddress.Text.Trim(), Convert.ToInt32(drpCountry.SelectedValue), txtPhoneNumber.Text.Trim(), txtEmail.Text.Trim(), "I", GetSessionUserID(), "", "", null, null, null, ref rowcount, ref Result).Tables[0];
                else
                    dt = objBLL.CRUD_UnionBranch(txtUnionBranch.Text.Trim(), 0, Convert.ToInt32(drpUnion.SelectedValue), txtAddressLine1.Text.Trim(), txtAddressLine2.Text.Trim(), txtCity.Text.Trim(), txtState.Text, Convert.ToInt32(drpCountry.SelectedValue), txtZipCode.Text.Trim(), txtPhoneNumber.Text.Trim(), txtEmail.Text.Trim(), "I", GetSessionUserID(), "", "", null, null, null, ref rowcount, ref Result).Tables[0];

                if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divAddUnionBranch');", true);
                }
            }

            if (Result < 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Union Branch already exist in the system');showModal('divAddUnionBranch', false);", true);
            BindUnionBranch();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ImgbtnRefreshBranch_Click(object sender, EventArgs e)
    {
        ViewState["SORTBYCOLOUMN_UNIONBRANCH"] = null;
        ViewState["SORTDIRECTION_UNIONBRANCH"] = null;
        txtUnionBranchFilter.Text = "";
        BindUnionBranch();

    }
    protected void ImgBtnExportUnion_OnClick(object sender, EventArgs e)
    {
        try
        {
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dtUnion = objBLL.CRW_LIB_ExportUnionWithBranchs(txtfilter.Text.Trim());

            string[] HeaderCaptions = null;
            string[] DataColumnsName = null;

            if (hdnAddressType.Value == "0")//US
            {
                HeaderCaptions = new string[10] { "Union ", "Union Branch", "Address Line 1", "Address Line 2", "City", "State", "ZipCode", "Country", "Phone Number", "Email" };
                DataColumnsName = new string[10] { "UnionName", "UnionBranch", "AddressLine1", "AddressLine2", "City", "State", "ZipCode", "Country_Name", "PhoneNumber", "Email" };
            }
            else if (hdnAddressType.Value == "1")///International
            {
                HeaderCaptions = new string[6] { "Union ", "Union Branch", "Address", "Country", "Phone Number", "Email" };
                DataColumnsName = new string[6] { "UnionName", "UnionBranch", "Address", "Country_Name", "PhoneNumber", "Email" };
            }
            GridViewExportUtil.ShowExcel(dtUnion, HeaderCaptions, DataColumnsName, "Union", "Union", "");
        }
        catch (Exception ex)
        {

        }
    }

    #region Union Book
    /// <summary>
    /// Bind School
    /// </summary>
    public void BindUnionBook()
    {
        try
        {
            int rowcount = ucCustomPager1UnionBook.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN_UNIONBOOK"] == null) ? null : (ViewState["SORTBYCOLOUMN_UNIONBOOK"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION_UNIONBOOK"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION_UNIONBOOK"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_UnionBook("", "R", 0, 0, txtUnionBookFilter.Text != "" ? txtUnionBookFilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPager1UnionBook.CurrentPageIndex, ucCustomPager1UnionBook.PageSize, ref  rowcount, ref Result);


            if (ucCustomPager1UnionBook.isCountRecord == 1)
            {
                ucCustomPager1UnionBook.CountTotalRec = rowcount.ToString();
                ucCustomPager1UnionBook.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvUnionBook.DataSource = dt;
                gvUnionBook.DataBind();
                ImgBtnExportUnionBook.Visible = true;
            }
            else
            {
                gvUnionBook.DataSource = null;
                gvUnionBook.DataBind();
                ImgBtnExportUnionBook.Visible = false;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// Delete Oil Major
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDeleteUnionBook(object source, CommandEventArgs e)
    {
        try
        {
            int rowcount = 0;
            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_UnionBook("", "D", GetSessionUserID(), Convert.ToInt32(e.CommandArgument.ToString()), null, "", null, null, null, ref  rowcount, ref Result);
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record deleted successfully')", true);

            BindUnionBook();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// To save or update oil major
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveUnionBook_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(hdnUnionBookId.Value) > 0)
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_UnionBook(txtUnionBook.Text.Trim(), "U", GetSessionUserID(), Convert.ToInt32(hdnUnionBookId.Value), null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Union Book already exist in the system');showModal('divAddUnionBook', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/UpdateMessage") + "');hideModal('divAddUnionBook');", true);
                    BindUnionBook();
                }
            }
            else
            {
                int rowcount = 0;
                BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
                DataTable dt = objBLL.CRUD_UnionBook(txtUnionBook.Text.Trim(), "I", GetSessionUserID(), 0, null, "", null, null, null, ref rowcount, ref Result);

                ///Result == 2 Already exists
                if (Result < 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('The selected Union Book already exist in the system');showModal('divAddUnionBook', false);", true);
                else if (Result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlreadyExists", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divAddUnionBook');", true);
                    BindUnionBook();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvUnionBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SORTBYCOLOUMN_UNIONBOOK"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN_UNIONBOOK"].ToString());
                    if (img != null)
                    {
                        if (ViewState["SORTDIRECTION_UNIONBOOK"] == null || ViewState["SORTDIRECTION_UNIONBOOK"].ToString() == "0")
                            img.Src = "~/purchase/Image/arrowUp.png";
                        else
                            img.Src = "~/purchase/Image/arrowDown.png";

                        img.Visible = true;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblUnionBook = (Label)e.Row.FindControl("lblUnionBook");
                lblUnionBook.ToolTip = lblUnionBook.Text;
                if (lblUnionBook.Text.Length>60)
                    lblUnionBook.Text = lblUnionBook.Text.Substring(0, 60) + "...";
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
    protected void gvUnionBook_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN_UNIONBOOK"] = se.SortExpression;
            if (ViewState["SORTDIRECTION_UNIONBOOK"] != null && ViewState["SORTDIRECTION_UNIONBOOK"].ToString() == "0")
                ViewState["SORTDIRECTION_UNIONBOOK"] = 1;
            else
                ViewState["SORTDIRECTION_UNIONBOOK"] = 0;
            BindUnionBook();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilterUnionBook_Click(object sender, EventArgs e)
    {
        try
        {
            BindUnionBook();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Clear All controls
    /// </summary>
    private void ClearControlUnionBook()
    {
        ViewState["SORTBYCOLOUMN_UNIONBOOK"] = null;
        ViewState["SORTDIRECTION_UNIONBOOK"] = null;
        ucCustomPager1UnionBook.CurrentPageIndex = 1;
        txtUnionBookFilter.Text = txtUnionBook.Text = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgBtnRefreshUnionBook_Click(object sender, EventArgs e)
    {
        ClearControlUnionBook();
        BindUnionBook();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgBtnExportUnionBook_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
            DataTable dt = objBLL.CRUD_UnionBook("", "R", 0, 0, txtUnionBookFilter.Text != "" ? txtUnionBookFilter.Text : null, sortbycoloumn, sortdirection, null, null, ref  rowcount, ref Result);

            string[] HeaderCaptions = { "Union Book" };
            string[] DataColumnsName = { "UnionBook" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Union Book", "Union Book", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    #endregion

    protected void TabPanel_OnActiveTabChanged(object sender, EventArgs e)
    {
        if (TabPanel.ActiveTabIndex == 0)
            BindUnion();
        else
            BindUnionBook();
    }
}