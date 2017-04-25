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
using SMS.Properties;


public partial class Purchase_ProvisionVicRateByOrder : System.Web.UI.Page
{
    public string LocationID;
    public BLL_PURC_Provision objProv = new BLL_PURC_Provision();
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public string Title = "";
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
            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            FillDDL();
            BindVictuallingRate();


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

    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindVictuallingRate();
    }

    public void BindVictuallingRate()
    {


        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = BLL_PURC_Provision.Get_Provison_OrderVictualing_rate((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], txtSearch.Text == "" ? null : txtSearch.Text, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        DataTable dt = BLL_PURC_Provision.Get_Provison_OrderVictualing_rate((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, txtSearch.Text == "" ? null : txtSearch.Text, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


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
        BindVictuallingRate();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ucCustomPagerItems.CurrentPageIndex = 1;
        FillDDL();
        BindVictuallingRate();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = int.Parse(ucCustomPagerItems.CountTotalRec);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_PURC_Provision.Get_Provison_OrderVictualing_rate((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], txtSearch.Text == "" ? null : txtSearch.Text, 1, rowcount, ref  rowcount);
        //// DataTable dt = objBLLLoc.Location_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
        //                                          , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Vessel ", " Requisition Code", "  Order Code", "Victualing Rate", " Delivery Date" };
        string[] DataColumnsName = { "Vessel_Name", "Requisition_Code", "Order_Code", "Order_Victuling_rate", "Delivery_Date" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Victualing Rate By Order", "Victualing Rate By Order", "");

    }

    private void BindData(int Vessel_ID, string Reqsn_Code, string Order_Code)
    {
        Title = "Provision Details";
        gvVictuallingRate.DataSource = BLL_PURC_Provision.Get_Calculate_Victualling_Rate(Vessel_ID, Reqsn_Code, Order_Code, Convert.ToInt32(Session["USERID"]));
        gvVictuallingRate.DataBind();
    }



    protected void rgdLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Vessel_Code = "";
            string REQSN_CODE = "";
            string ORDER_CODE = "";
            DataRowView d = (DataRowView)e.Row.DataItem;

            REQSN_CODE = Convert.ToString(d["Requisition_Code"]);
            ORDER_CODE = Convert.ToString(d["Order_Code"]);
            Vessel_Code = Convert.ToString(d["VESSEL_CODE"]);
            string showProvision = string.Format("showModal('divProvision');");
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkProvi");

            //string url = "Reqsn_Vic_Meat_Percent.aspx?VESSEL_ID=" + Vessel_Code + "&REQSN_CODE=" + REQSN_CODE + "&ORDER_CODE=" + ORDER_CODE;
            BindData(Convert.ToInt32(Vessel_Code), REQSN_CODE, ORDER_CODE);
            lnk.Attributes.Add("onClick", "showModal('divProvision');return false;");//"JavaScript: window.open('" + url + "','','_blank','heigh=300,width=200,left=10,top=10,resizable=no,scrollbars=no,toolbar=no,menubar=no,location=no,direction=no,status=no')");

            //window.open(url, 'popupWindow', 'heigh=700,width=900,left=10,top=10,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,direction=no,status=yes')
            //LinkButton lnkProvi = (LinkButton)e.Row.FindControl("lnkProvi");
            //lnkProvi.PostBackUrl="Reqsn_Vic_Meat_Percent.aspx?VESSEL_ID=" + Request.QueryString["Vessel_Code"].ToString() + "&REQSN_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&ORDER_CODE=0";

            //lnkProvi.Attributes.Add("onclick", "ViewReqsnProvisionDetails('Reqsn_Vic_Meat_Percent.aspx?VESSEL_ID=" + Request.QueryString["Vessel_Code"].ToString() + "&REQSN_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&ORDER_CODE=0');");
        }
    }
    protected void rgdLocation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //int id = e.NewEditIndex;
        //gvVictuallingRate.DataSource = null; gvVictuallingRate.DataBind();
        //string showProvision = string.Format("showModal('divProvision');");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showProvision", showProvision, true);
    }
}