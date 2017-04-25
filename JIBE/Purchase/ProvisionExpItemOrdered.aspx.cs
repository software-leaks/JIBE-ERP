using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Text;
using SMS.Business.PURC;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Purchase_ProvisionExpItemOrdered : System.Web.UI.Page
{
    public string LocationID;
    public BLL_PURC_Provision objProv = new BLL_PURC_Provision();
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //  UserAccessValidation();

        if (!IsPostBack)
        {

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;



            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            FillDDL();
            BindrgdLocation();
            HiddenFlag.Value = "Add";

        }
    }
    public void FillDDL()
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
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindrgdLocation();
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

        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        if (sbFilterFlt.Length > 1)
        {
            sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
            VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
            dtVessel.DefaultView.RowFilter = VslFilter;
        }

        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_id";
        DDLVessel.DataBind();
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        Session["sFleet"] = DDLFleet.SelectedValues;


    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");


        if (objUA.Delete == 1) uaDeleteFlage = true;


    }




    public void BindrgdLocation()
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_PURC_Provision.Get_Provison_MostExpensive_ItemList((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, txtFromDate.Text, txtToDate.Text, int.Parse(rdoReportType.SelectedValue), txtSearch.Text.Trim(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdLocation.DataSource = dt;
            rgdLocation.DataBind();
        }
        else
        {
            rgdLocation.DataSource = dt;
            rgdLocation.DataBind();
        }


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindrgdLocation();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        rdoReportType.SelectedValue = "0";
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtSearch.Text = "";
        ucCustomPagerItems.CurrentPageIndex = 1;
        FillDDL();
        BindrgdLocation();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = int.Parse(ucCustomPagerItems.CountTotalRec);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataTable dt = BLL_PURC_Provision.Get_Provison_MostExpensive_ItemList((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, txtFromDate.Text, txtToDate.Text, int.Parse(rdoReportType.SelectedValue), txtSearch.Text.Trim(), 1, rowcount, ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "Fleet Name", "Item Description", "Unit", "Item Price" };
        string[] DataColumnsName = { "Vessel_Name", "FleetName", "Short_Description", "Unit_and_Packings", "ORDER_PRICE" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Expensive Item Ordered List", "Expensive Item Ordered List", "");

    }


    protected void rgdLocation_RowDataBound(object sender, GridViewRowEventArgs e)
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


    }




    protected void rgdLocation_Sorting(object sender, GridViewSortEventArgs se)
    {


        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdLocation();
    }

    protected void rdoReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCustomPagerItems.CurrentPageIndex = 1;
    }
}