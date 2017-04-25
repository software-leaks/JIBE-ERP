using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.Survey;
using System.Web.UI.HtmlControls;

public partial class Surveys_Survey_Vessel_Assign : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (MainDiv.Visible)
        {
            try
            {
                trAuthority.Visible = false;
                if (!IsPostBack)
                {
                    ViewState["CA_SORTDIRECTION"] = null;
                    ViewState["CA_SORTBYCOLOUMN"] = null;

                    ucCustomPagerItems.PageSize = 20;

                    Load_FleetList();
                    Load_VesselList();
                    Load_MainCategoryList();
                    Load_CategoryList();
                    Load_CertificateList();
                }
            }
            catch (Exception)
            {

            }
        }
    }

    protected void UserAccessValidation()
    {
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(GetSessionUserID(), PageURL);

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
            GridView_Certificate.Columns[GridView_Certificate.Columns.Count - 1].Visible = false;
            GridView_Certificate.Columns[GridView_Certificate.Columns.Count - 2].Visible = false;
        }
    }
    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public void Load_FleetList()
    {
        try
        {
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
            ddlFleet.DataTextField = "NAME";
            ddlFleet.DataValueField = "CODE";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        }
        catch (Exception)
        {
        }

    }
    public void Load_VesselList()
    {
        try
        {
            int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
                Vessel_Manager = UserCompanyID;

            ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

            ddlVessel.DataTextField = "VESSEL_NAME";
            ddlVessel.DataValueField = "VESSEL_ID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlVessel.SelectedIndex = 0;
        }
        catch (Exception)
        {
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        ddlVessel.SelectedIndex = 0;
        ddlCategoryFilter.SelectedIndex = 0;
        ddlMainCategory.SelectedIndex = 0;
     
        ViewState["CA_SORTDIRECTION"] = null;
        ViewState["CA_SORTBYCOLOUMN"] = null;

        Load_CertificateList();
    }

    protected void GridView_Certificate_OnRowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void GridView_Certificate_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["CA_SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["CA_SORTDIRECTION"] != null && ViewState["CA_SORTDIRECTION"].ToString() == "0")
            ViewState["CA_SORTDIRECTION"] = 1;
        else
            ViewState["CA_SORTDIRECTION"] = 0;

        Load_CertificateList();
    }

    protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CertificateList();
    }
    /// <summary>
    /// Loading Certificate 
    /// </summary>
    protected void Load_CertificateList()
    {
        try
        {
            int FleetID = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
            int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            int CatID = UDFLib.ConvertToInteger(ddlCategoryFilter.SelectedValue);
            int MainCatID = UDFLib.ConvertToInteger(ddlMainCategory.SelectedValue);

            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["CA_SORTBYCOLOUMN"] == null) ? null : (ViewState["CA_SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["CA_SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["CA_SORTDIRECTION"].ToString());

            DataTable dt = objBLL.Get_AssignedSurvayList(FleetID, VesselID, MainCatID, CatID, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            GridView_Certificate.DataSource = dt;
            GridView_Certificate.DataBind();
        }
        catch (Exception)
        {
        }
    }
    /// <summary>
    /// Loading Main Category
    /// </summary>
    public void Load_MainCategoryList()
    {
        try
        {
            DataTable dtMainCategory = objBLL.Get_Survey_MainCategoryList();

            ddlMainCategory.DataSource = dtMainCategory;
            ddlMainCategory.DataTextField = "Survey_Category";
            ddlMainCategory.DataValueField = "Id";
            ddlMainCategory.DataBind();
            ddlMainCategory.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception)
        {
        }
    }
    // loding Category 
    public void Load_CategoryList()
    {
        try
        {
            ddlCategoryFilter.DataSource = objBLL.Get_Survey_CategoryList_ByMainCategoryId(int.Parse(ddlMainCategory.SelectedValue.ToString()));
            ddlCategoryFilter.DataTextField = "Survey_Category";
            ddlCategoryFilter.DataValueField = "ID";
            ddlCategoryFilter.DataBind();
            ddlCategoryFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlCategoryFilter.SelectedIndex = 0;
        }
        catch (Exception)
        {
        }
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_VesselList();
            Load_CertificateList();
        }
        catch (Exception)
        {
        }
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CertificateList();
    }
    protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_CategoryList();
            Load_CertificateList();
        }
        catch (Exception)
        {
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            if (VesselID > 0)
            {

                hdnSurv_ID.Value = ((Button)sender).CommandArgument;
                pnlAssign.Visible = true;

                string js = "$('#dvAssignSurvey').draggable();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                pnlAssignSurvey.Focus();
            }
            else
            {
                string js2 = "alert('Please select vessel');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js2, true);

            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            int Surv_Vessel_ID = UDFLib.ConvertToInteger(((Button)sender).CommandArgument);
            int ID = objBLL.UPDATE_SurveyStatus(VesselID, Surv_Vessel_ID, 1, GetSessionUserID());

            Load_CertificateList();
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveAssignment_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            int Surv_ID = UDFLib.ConvertToInteger(hdnSurv_ID.Value);

            int ID = objBLL.Assign_SurveyToVessel(VesselID, Surv_ID, txtEquipmentType.Text.Trim(), txtIssuingAuthority.Text.Trim(), GetSessionUserID());
            txtEquipmentType.Text = "";
            txtIssuingAuthority.Text = "";
            pnlAssign.Visible = false;
            Load_CertificateList();
        }
        catch (Exception)
        {
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlAssign.Visible = false;
    }


}