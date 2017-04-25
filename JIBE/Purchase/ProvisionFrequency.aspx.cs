using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Properties;
using System.Web.UI.HtmlControls;

public partial class Purchase_ProvisionFrequency : System.Web.UI.Page
{
    public string LocationID;
    public BLL_PURC_Purchase objBLLLoc = new BLL_PURC_Purchase();
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            BindrgdFrequency();
            FillDDL();


            HiddenFlag.Value = "Add";

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;


    }
    public void FillDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlVessel.DataSource = dtVessel;
            ddlVessel.DataTextField = "Vessel_name";
            ddlVessel.DataValueField = "Vessel_id";
            ddlVessel.DataBind();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    /*Added by Pranali 25/02/2015 
      Purpose:Enable disable controls
    */
    #region Enable_Disable_Control
    private void Enable_Disable_Control(bool Mode)
    {
        try
        {
            ddlProvisionType.Enabled = Mode;
            ddlVessel.Enabled = Mode;
        }
        catch
        {
        }
    }
    #endregion

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Frequency";
        lblError.Text = string.Empty;

        DataTable dtItems = BLL_PURC_Provision.Get_Provison_Item_frequency_Edit(Convert.ToInt32(e.CommandArgument.ToString()));

        HiddenItemID.Value = e.CommandArgument.ToString();
        ddlVessel.SelectedValue = dtItems.Rows[0]["VESSEL_ID"].ToString();
        ddlProvisionType.SelectedValue = dtItems.Rows[0]["PROVISION_TYPE"].ToString();
        txtDuration.Text = dtItems.Rows[0]["SUPPLY_PERIOD"].ToString();

        Enable_Disable_Control(false);

        string AssginLocmodal = String.Format("showModal('divaddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        string[] strIds = e.CommandArgument.ToString().Split(',');
        string sCode = strIds[0];
        string sParentType = strIds[1];

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            LocationData objLocationDO = new LocationData();

            LocationID = sCode;
            objLocationDO.Code = sCode;

            objLocationDO.CurrentUser = Session["userid"].ToString();
            int count = objTechService.DeleteLocation(objLocationDO);
            BindrgdFrequency();

        }
    }



    public void BindrgdFrequency()
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_PURC_Provision.Get_Provison_Item_frequency_List(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdFrequency.DataSource = dt;
            rgdFrequency.DataBind();
        }
        else
        {
            rgdFrequency.DataSource = dt;
            rgdFrequency.DataBind();
        }

    }



    protected void DivbtnSave_Click(object sender, EventArgs e)
    {
        #region Commented_Code_Pranali_25/02/2015
        //if (HiddenFlag.Value == "Add")
        //{
        //    int retVal = BLL_PURC_Provision.InsUpt_Provison_Item_frequency(null,UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), ddlProvisionType.SelectedValue, Convert.ToDecimal(txtDuration.Text), Convert.ToInt32(CurrentUser));
        //}
        //else
        //{
        //    int retVal = BLL_PURC_Provision.InsUpt_Provison_Item_frequency(UDFLib.ConvertIntegerToNull(HiddenItemID.Value),UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), ddlProvisionType.SelectedValue, Convert.ToDecimal(txtDuration.Text), Convert.ToInt32(CurrentUser));
        //}

        //BindrgdFrequency();

        //string hidemodal = String.Format("hideModal('divaddLocation')");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        #endregion

        //Added By Pranali 25/02/2015 Purpose: to avoid duplicate code.
        #region
        try
        {
            int? v_HiddenItemID = null;
            string CurrentUser = Session["userid"].ToString();
            if (HiddenFlag.Value == "Add")
            {
                v_HiddenItemID = null;
            }
            else
            {
                v_HiddenItemID = UDFLib.ConvertIntegerToNull(HiddenItemID.Value);
            }
            int retVal = BLL_PURC_Provision.InsUpt_Provison_Item_frequency(v_HiddenItemID, UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), ddlProvisionType.SelectedValue, Convert.ToDecimal(txtDuration.Text), Convert.ToInt32(CurrentUser));
            if (retVal != -1)
            {
                BindrgdFrequency();
                string hidemodal = String.Format("hideModal('divaddLocation')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
            else
            {
                string showModal = String.Format("showModal('divaddLocation',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showModal", showModal, true);
                lblError.Text = "You have already add frequency for this sub catalogue and vessel";
            }
        }
        catch
        {

        }
        #endregion
    }



    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindrgdFrequency();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.CurrentPageIndex = 1;
        txtSearch.Text = "";
        BindrgdFrequency();
    }


    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        Enable_Disable_Control(true);//Added by Pranali_250
        this.SetFocus("ctl00_MainContent_txtDuration");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Frequency";
        ddlVessel.SelectedIndex = 0;
        ddlProvisionType.SelectedIndex = 0;
        lblError.Text = string.Empty;
        txtDuration.Text = "";


        string AddLocmodal = String.Format("showModal('divaddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddLocmodal", AddLocmodal, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = int.Parse(ucCustomPagerItems.CountTotalRec);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_PURC_Provision.Get_Provison_Item_frequency_List(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection, 1, rowcount, ref  rowcount);
        // DataTable dt = objBLLLoc.Location_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection      , null, null, ref  rowcount);


        string[] HeaderCaptions = { "VESSEL", "Provision Type", " Supply Period (in Days)" };
        string[] DataColumnsName = { "VESSEL_NAME", "PROVISION_TYPE", "SUPPLY_PERIOD" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Frequency", "Frequency", "");

    }


    protected void rgdFrequency_RowDataBound(object sender, GridViewRowEventArgs e)
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




    protected void rgdFrequency_Sorting(object sender, GridViewSortEventArgs se)
    {


        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdFrequency();
    }
}