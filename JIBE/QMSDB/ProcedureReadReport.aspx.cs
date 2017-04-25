using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.QMSDB;
using SMS.Business.Crew;
using System.Data;

public partial class ProcedureReadReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();
            BindRankDDL();
            BindFBMDLL();


            if (!string.IsNullOrEmpty(Request.QueryString["FBM_ID"].ToString()))
                ddlFbmList.SelectedValue = Request.QueryString["FBM_ID"].ToString();


            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 30;
            BindFbmReadSearch();


        }
    }


    public void BindFBMDLL()
    {

        try
        {

            DataTable dtFbmList = BLL_QMSDB_Procedures.GetProcedureList();
            ddlFbmList.DataTextField = "PROCEDURES_NAME";
            ddlFbmList.DataValueField = "PROCEDURE_ID";
            ddlFbmList.DataSource = dtFbmList;
            ddlFbmList.DataBind();
            ddlFbmList.Items.Insert(0, new ListItem("-SELECT-", "0"));

        }
        catch
        
        {
        }


    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("-SELECT ALL-", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("-SELECT ALL-", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    private void BindRankDDL()
    {
        try
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dtRank = new DataTable();
            dtRank = objCrewAdmin.Get_RankList();

            ddlRank.DataSource = dtRank;
            ddlRank.DataTextField = "Rank_Short_Name";
            ddlRank.DataValueField = "ID";

            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        }
        catch
        {
        }

    }

    public void BindFbmReadSearch()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = BLL_QMSDB_Procedures.GetReadProcedureSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlFbmList.SelectedValue), txtSearchBy.Text.Trim()
            , UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue),UDFLib.ConvertIntegerToNull(rdoFBMReadStatus.SelectedValue)
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvFbmRead.DataSource = ds.Tables[0];
            gvFbmRead.DataBind();
        }
        else
        {
            gvFbmRead.DataSource = ds.Tables[0];
            gvFbmRead.DataBind();
        }

    }

    protected void gvFbmRead_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvFbmRead_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvFbmRead_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

        DDLVessel.Items.Clear();
        DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_ID";
        DDLVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLVessel.Items.Insert(0, li);


    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        BindFbmReadSearch();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        DDLFleet.SelectedValue = "0";
        BindVesselDDL();
        ddlRank.SelectedValue = "0";

        rdoFBMReadStatus.SelectedValue = "1";

        txtSearchBy.Text = "";

        BindFbmReadSearch();
    }


    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = BLL_QMSDB_Procedures.GetReadProcedureSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlFbmList.SelectedValue), txtSearchBy.Text.Trim()
            , UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), UDFLib.ConvertIntegerToNull(rdoFBMReadStatus.SelectedValue)
            , sortbycoloumn, sortdirection,null, null, ref  rowcount);

        string[] HeaderCaptions = {   "Vessel", "Rank", "Staff Code", "Name", " Read On" };
        string[] DataColumnsName = { "Vessel_Name", "Rank_Short_Name", "Staff_Code", "Staff_FullName", "FBM_DATE_READ" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Fbm Read Report", "Fbm Read Report");

    }
}