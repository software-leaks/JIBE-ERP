using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.eForms;
using SMS.Business.Infrastructure;
using System.Text;

public partial class eForms_eFormIndex : System.Web.UI.Page
{
    //BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sFleet"] = ddlFleet.SelectedValues;
            Session["sVesselCode"] = ddlVessel.SelectedValues;
            Load_FleetList();
            Load_VesselList();
            Load_Report_Index();
        }
    }
    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Added pagination on 27 th may 2016
    /// </summary>
    protected void Load_Report_Index()
    {

        
        DataTable dtFleetCodes = ddlFleet.SelectedValues;
        DataTable dtVessel_ID = ddlVessel.SelectedValues;

        DateTime? From_Date = UDFLib.ConvertDateToNull(txtFromDate.Text);
        DateTime? To_Date = UDFLib.ConvertDateToNull(txtToDate.Text);
        string SearchText = txtSearch.Text;
        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_eForms_Admin.Get_ReportIndex(dtFleetCodes, dtVessel_ID, From_Date, To_Date, SearchText, GetSessionUserID(), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }



        GridView_Reports.DataSource = dt;
        GridView_Reports.DataBind();
    }

    public void Load_FleetList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataBind();

    }

    public void Load_VesselList()
    {
        try
        {
            BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
            DataTable dtFleetList = ddlFleet.SelectedValues;
            DataTable dtVesselList = objVessel.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            ddlVessel.DataTextField = "Vessel_name";
            ddlVessel.DataValueField = "Vessel_id";
            ddlVessel.DataSource = dtVesselList;
            ddlVessel.DataBind();
        }
        catch { }
    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        StringBuilder sbFilterFlt = new StringBuilder();
        string VslFilter = "";
        foreach (DataRow dr in ddlFleet.SelectedValues.Rows)
        {
            sbFilterFlt.Append(dr[0]);
            sbFilterFlt.Append(",");
        }
        DataTable dtFleetList = ddlFleet.SelectedValues;
        DataTable dtVessel = objVessel.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        if (sbFilterFlt.Length > 1)
        {
            sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
            VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
            dtVessel.DefaultView.RowFilter = VslFilter;
        }

        ddlVessel.DataSource = dtVessel;
        ddlVessel.DataTextField = "Vessel_name";
        ddlVessel.DataValueField = "Vessel_id";
        ddlVessel.DataBind();
        Session["sVesselCode"] = ddlVessel.SelectedValues;
        Session["sFleet"] = ddlFleet.SelectedValues;
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = ddlVessel.SelectedValues;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_Report_Index();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        ddlVessel.ClearSelection();
        ddlFleet.ClearSelection();
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtSearch.Text = "";
        Load_Report_Index();
    }
}