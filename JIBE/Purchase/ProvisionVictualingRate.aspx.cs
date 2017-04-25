using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class Purchase_ProvisionVictualingRate : System.Web.UI.Page
{
    public string LocationID;
    public BLL_PURC_Provision objProv = new BLL_PURC_Provision();
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public string currentid = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVesselF.SelectedValues;
            FillDDLF();
            BindrgdVictulingRate();
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
            btnSaveLocation.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;


    }


    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Victualing Rate";
        DataTable dtItems = BLL_PURC_Provision.Get_Victualing_Rate_Edit(Convert.ToInt32(e.CommandArgument.ToString()));
        HiddenItemID.Value = e.CommandArgument.ToString();
        ddlVessel.SelectedValue = dtItems.Rows[0]["Vessel_Id"].ToString();
        txtFromDate.Text = dtItems.Rows[0]["Victualing_From_Date"].ToString();

        txtVictualingRate.Text = dtItems.Rows[0]["Victualing_Rate"].ToString();
        ddlVessel.Enabled = false;
        txtFromDate.Enabled = false;
        lblError.Text = string.Empty;
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
            BindrgdVictulingRate();

        }
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

    public void FillDDLF()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVesselF.DataSource = dtVessel;
            DDLVesselF.DataTextField = "Vessel_name";
            DDLVesselF.DataValueField = "Vessel_id";
            DDLVesselF.DataBind();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    protected void DDLFleet_SelectedIndexChanged()
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        StringBuilder sbFilterFlt = new StringBuilder();
        string VslFilter = "";
        foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
        {
            sbFilterFlt.Append(dr[0]);
            sbFilterFlt.Append(",");
        }

        DataTable dtVesselF = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        if (sbFilterFlt.Length > 1)
        {
            sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
            VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
            dtVesselF.DefaultView.RowFilter = VslFilter;
        }

        DDLVesselF.DataSource = dtVesselF;
        DDLVesselF.DataTextField = "Vessel_name";
        DDLVesselF.DataValueField = "Vessel_id";
        DDLVesselF.DataBind();
        Session["sVesselCode"] = DDLVesselF.SelectedValues;
        Session["sFleet"] = DDLFleet.SelectedValues;


    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVesselF.SelectedValues;
        BindrgdVictulingRate();
    }



    public void BindrgdVictulingRate()
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_PURC_Provision.Get_Provison_Victualing_Rate(null, (DataTable)DDLFleet.SelectedValues, (DataTable)DDLVesselF.SelectedValues, sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdVictulingRate.DataSource = dt;
            rgdVictulingRate.DataBind();
        }
        else
        {
            rgdVictulingRate.DataSource = dt;
            rgdVictulingRate.DataBind();
        }



    }

    //private void BindLocationCombo()
    //{

    //    ddlMonthList.Items.Clear();
    //    ddlYearList.Items.Clear();
    //    DataSet ds = BLL_PURC_Provision.Get_YearMonthList();

    //    ddlMonthList.DataSource = ds.Tables[0];
    //    ddlMonthList.DataTextField = "Month_Name";
    //    ddlMonthList.DataValueField = "MonthID";
    //    ddlMonthList.DataBind();

    //    ddlYearList.DataSource = ds.Tables[1];
    //    ddlYearList.DataTextField = "Year_Name";
    //    ddlYearList.DataValueField = "Year_Code";
    //    ddlYearList.DataBind();


    //    }






    protected void DivbtnSave_Click(object sender, EventArgs e)
    {

        string CurrentUser = Session["userid"].ToString();

        #region Commented_Code_25/02/2015_Pranali
        //if (HiddenFlag.Value == "Add")
        //{
        //    int retVal = BLL_PURC_Provision.InsUpt_Victualing_Rate(null, Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToDecimal(txtVictualingRate.Text), UDFLib.ConvertDateToNull(txtFromDate.Text), Convert.ToInt32(CurrentUser));
        //}
        //else
        //{
        //    int retVal = BLL_PURC_Provision.InsUpt_Victualing_Rate(UDFLib.ConvertIntegerToNull(HiddenItemID.Value), Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToDecimal(txtVictualingRate.Text), UDFLib.ConvertDateToNull(txtFromDate.Text), Convert.ToInt32(CurrentUser));
        //}

        //BindrgdVictulingRate();
        //string hidemodal = String.Format("hideModal('divaddLocation')");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        #endregion

        //Added By Pranali 25/02/2015 Purpose: to avoid duplicate code.
        #region
        int? v_HiddenItemID = 0;
        if (HiddenFlag.Value == "Add")
        {
            v_HiddenItemID = null;
        }
        else
        {
            v_HiddenItemID = UDFLib.ConvertIntegerToNull(HiddenItemID.Value);
        }
        int retVal = BLL_PURC_Provision.InsUpt_Victualing_Rate(v_HiddenItemID, Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToDecimal(txtVictualingRate.Text), UDFLib.ConvertDateToNull(txtFromDate.Text), Convert.ToInt32(CurrentUser));
        if (retVal != -1)
        {
            BindrgdVictulingRate();
            string hidemodal = String.Format("hideModal('divaddLocation')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        else
        {
            string showModal = String.Format("showModal('divaddLocation',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showModal", showModal, true);
            lblError.Text = "Record already exists..";
        }
        #endregion


    }



    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindrgdVictulingRate();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.CurrentPageIndex = 1;
        FillDDLF();
        BindrgdVictulingRate();
    }


    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtVictualingRate");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Victualing Rate";

        ddlVessel.SelectedIndex = 0;
        txtVictualingRate.Text = "";
        txtFromDate.Text = "";

        ddlVessel.Enabled = true;
        txtFromDate.Enabled = true;
        string AddLocmodal = String.Format("showModal('divaddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddLocmodal", AddLocmodal, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = int.Parse(ucCustomPagerItems.CountTotalRec);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_PURC_Provision.Get_Provison_Victualing_Rate(null, (DataTable)DDLFleet.SelectedValues, (DataTable)Session["sVesselCode"], sortbycoloumn, sortdirection
        , 1, rowcount, ref  rowcount);

        //// DataTable dt = objBLLLoc.Location_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
        //                                          , null, null, ref  rowcount);


        //string[] HeaderCaptions = { "Vessel Name", "Victuling Rate", "  Vessel Victuling Rate","Month" ,"Year"};
        //string[] DataColumnsName = { "Vessel_Name", "Victuling_Rate", "Vessel_Victuling_Rate", "Month_Name", "Rate_Year" };

        string[] HeaderCaptions = { "Vessel Name", "Victualing Rate", "Effective Date" };
        string[] DataColumnsName = { "Vessel_Name", "Victualing_Rate", "Victualing_From_Date" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Vessel Victualing Rate", " Vessel Victualing Rate", "");

    }


    protected void rgdVictulingRate_RowDataBound(object sender, GridViewRowEventArgs e)
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




    protected void rgdVictulingRate_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdVictulingRate();
    }    
}