using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Survey;
using System.Web.UI.HtmlControls;

public partial class SurveyCertificateAuthority : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (MainDiv.Visible)
        {
            if (!IsPostBack)
            {
                ViewState["CA_SORTDIRECTION"] = null;
                ViewState["CA_SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;
                Load_AuthorityList();
            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
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

        if (objUA.Add == 0) lnkAddNewAuthority.Visible = false;
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

    protected void btnFilter_Click(object sender, EventArgs e)
    { Load_AuthorityList(); }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtFltrAuthority.Text = string.Empty;
        Load_AuthorityList();
    }

    protected void Load_AuthorityList()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["CA_SORTBYCOLOUMN"] == null) ? null : (ViewState["CA_SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["CA_SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["CA_SORTDIRECTION"].ToString());

            DataTable dt = objBLL.Get_Survay_CertificateAuthorityList(txtFltrAuthority.Text != "" ? txtFltrAuthority.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            GridView_Authority.DataSource = dt;
            GridView_Authority.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_Authority_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["CA_SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["CA_SORTDIRECTION"] != null && ViewState["CA_SORTDIRECTION"].ToString() == "0")
            ViewState["CA_SORTDIRECTION"] = 1;
        else
            ViewState["CA_SORTDIRECTION"] = 0;

        Load_AuthorityList();
    }

    protected void GridView_Authority_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["CA_SORTBYCOLOUMN"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["CA_SORTBYCOLOUMN"].ToString());
                    if (img != null)
                    {
                        if (ViewState["CA_SORTDIRECTION"] == null || ViewState["CA_SORTDIRECTION"].ToString() == "0")
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

    protected void GridView_Authority_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Authority.DataKeys[e.RowIndex].Value.ToString());

        objBLL.DELETE_Survey_CertificateAuthority(ID, GetSessionUserID());
        Load_AuthorityList();
    }
    /// <summary>
    /// Updation of survey certificate authority name
    /// Modified by Anjali DT:10-06-2016
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Authority_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = UDFLib.ConvertToInteger(GridView_Authority.DataKeys[e.RowIndex].Value.ToString());
            string Authority_Name = e.NewValues["Authority"].ToString();

            int res = objBLL.UPDATE_Survey_CertificateAuthority(ID, Authority_Name, GetSessionUserID());
            if (res == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", "alert('Authority updated successfuly');", true);
                GridView_Authority.EditIndex = -1;
                Load_AuthorityList();
            }
            else if (res == 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", "alert('Authority is already exists');", true);
            else if (res == -1)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", "alert('Authority updating unsuccessful');", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void GridView_Authority_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Authority.EditIndex = e.NewEditIndex;
            Load_AuthorityList();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void GridView_Authority_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Authority.EditIndex = -1;
            Load_AuthorityList();
        }
        catch
        {
        }
    }
    /// <summary>
    /// To save new survey certificate authority name and add new authority without closing popup.
    /// Modified by Anjali DT:10-06-2016
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveAndAdd_Click(object sender, EventArgs e)
    {
        int ID = objBLL.INSERT_Survey_CertificateAuthority(txtAuthority.Text.Trim(), GetSessionUserID());
        txtAuthority.Text = "";
        Load_AuthorityList();
        if (ID == 1)
        {
            string js2 = "alert('Authority saved successfuly.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
        else if (ID == 0)
        {
            string js2 = "alert('Authority is already exists.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
        else if (ID == -1)
        {
            string js2 = "alert(' Authority saved unsuccessful.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAuthority.Text = "";
        string js = "closeDivAddNewCategory();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
    }
    /// <summary>
    /// To save new survey certificate authority name and close popup.
    /// Modified by Anjali DT:10-06-2016
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = objBLL.INSERT_Survey_CertificateAuthority(txtAuthority.Text.Trim(), GetSessionUserID());
        txtAuthority.Text = "";
        Load_AuthorityList();
        if (ID == 1)
        {
            string js2 = "alert('Authority saved successfuly');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
        else if (ID == 0)
        {
            string js2 = "alert('Authority is already exists.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
        else if (ID == -1)
        {
            string js2 = "alert(' Authority saved unsuccessful.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }

        string js = "closeDivAddNewCategory();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
}