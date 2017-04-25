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
public partial class Purchase_LOG_Logistics_PO_List : System.Web.UI.Page
{

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        uc_SupplierList1.Web_Method_URL = "/"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+"/JibeWebService.asmx/asyncGet_Supplier_List";

        if (!IsPostBack)
        {
          
            string stsClientID = "";
            if (UDFLib.ConvertToInteger(Request.QueryString["LPOAPPROVAL"]) == 1)
            {
                ViewState["LOG_STATUS"] = "APPROVAL";
                chkShowAll.Checked = false;
                stsClientID = lnkMenu2.ClientID;
            }
            else
            {
                stsClientID = lnkMenu1.ClientID;
                ViewState["LOG_STATUS"] = "NEWLOG";
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + stsClientID + "');", true);
 

            BindFleetDLL();
            DDLFleet.SelectItems(new string[] { Session["USERFLEETID"].ToString() });
            BindVesselDDL();

            BindDataItems();
        }
        hdf_User_ID.Value = Session["userid"].ToString();
        
    }
 
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
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
        catch (Exception ex)
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        BindVesselDDL();

        BindDataItems();
    }

    public void BindDataItems()
    {
        int? ShowAllUser = (chkShowAll.Checked) ? UDFLib.ConvertIntegerToNull(0) : Convert.ToInt32(Session["userid"]);
        int? ShowActive = (chkShowActive.Checked) ? 1 : 0;
        int is_Fetch_Count = ucCustomPagerPO.isCountRecord;
        gvOrderList.DataSource = BLL_PURC_LOG.Get_Log_LogisticPO_List((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, UDFLib.ConvertStringToNull(uc_SupplierList1.SelectedValue), UDFLib.ConvertStringToNull(txtPoNumber.Text), ShowAllUser, null, UDFLib.ConvertStringToNull(ViewState["LOG_STATUS"]), ucCustomPagerPO.CurrentPageIndex, ucCustomPagerPO.PageSize, ref is_Fetch_Count);
        gvOrderList.DataBind();

        ucCustomPagerPO.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerPO.BuildPager();

    }

    protected void btnSearchPO_Click(object sender, EventArgs e)
    {
        BindDataItems();
    }
    
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        DDLFleet.ClearSelection();
        DDLVessel.ClearSelection();
        txtPoNumber.Text = "";
        BindFleetDLL();
        BindVesselDDL();
        uc_SupplierList1.SelectedValue = "0";

        ViewState["LOG_STATUS"] = "0";

        BindDataItems();

    }
    
    protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
    {
        BindDataItems();
    }

    protected void DDLVessel_SelectedIndexChanged()
    {
        BindDataItems();
    }
    
    protected void NavMenu_Click(object sender, EventArgs e)
    {
        string value = ((LinkButton)sender).CommandArgument;
        switch (value)
        {
            case "NLP":

                ViewState["LOG_STATUS"] = "NEWLOG";
                break;
            case "APR":

                ViewState["LOG_STATUS"] = "APPROVAL";
                break;
            case "RPO":

                ViewState["LOG_STATUS"] = "RAISEPO";
                break;
            case "POI":

                ViewState["LOG_STATUS"] = "POISSUED";
                break;
            case "DEL":
                ViewState["LOG_STATUS"] = "DELETED";
                break;
            case "ALL":

                ViewState["LOG_STATUS"] = "0";
                break;
        }
        BindDataItems();
        txtSelMenu.Value = ((LinkButton)sender).ClientID;
       

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
        }
    }

}