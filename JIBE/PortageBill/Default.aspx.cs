using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using SMS.Business.Infrastructure;
using System.Data;


public partial class PortageBill_Default : System.Web.UI.Page
{
    MergeGridviewHeader_Info objItemColumn = new MergeGridviewHeader_Info();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        objItemColumn.AddMergedColumns(new int[] { 6, 7, 8, 9,10 }, "Scanned Copy from vessel", "HeaderStyle-css");
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_Years();
            Load_FleetList();
            Load_VesselList();


            Load_PB_Received();
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
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }

        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_Years()
    {
        DataSet dsdate = BLL_PB_PortageBill.Get_PB_YearMonth(null);

        ddlYear.DataSource = dsdate.Tables[0];
        ddlYear.DataTextField = "pbyear";
        ddlYear.DataValueField = "pbyear";
        ddlYear.DataBind();

        ddlMonth.DataSource = dsdate.Tables[1];
        ddlMonth.DataTextField = "smonth";
        ddlMonth.DataValueField = "imonth";
        ddlMonth.DataBind();

        ddlYear.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlMonth.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        if (ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        if (ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()) != null)
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
    }
    protected void Load_PB_Received()
    {
        int Fleet_ID = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int? Month = UDFLib.ConvertIntegerToNull(ddlMonth.SelectedValue);
        int? Year = UDFLib.ConvertIntegerToNull(ddlYear.SelectedValue);

        int iscount = ucCustomPagerPB.isCountRecord;

        GridViewPB.DataSource = BLL_PB_PortageBill.Get_PortageBills(Fleet_ID, Vessel_ID, Month, Year, ucCustomPagerPB.CurrentPageIndex, ucCustomPagerPB.PageSize, ref iscount);
        GridViewPB.DataBind();

        ucCustomPagerPB.CountTotalRec = iscount.ToString();
        ucCustomPagerPB.BuildPager();



    }
    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_PB_Received();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PB_Received();
    }

  
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PB_Received();
        ViewState["Month"] = ddlMonth.SelectedValue.ToString();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsdate = BLL_PB_PortageBill.Get_PB_YearMonth(UDFLib.ConvertIntegerToNull( ddlYear.SelectedValue.ToString()));
        ddlMonth.ClearSelection();
        ddlMonth.DataSource = dsdate.Tables[1];
        ddlMonth.DataTextField = "smonth";
        ddlMonth.DataValueField = "imonth";
        ddlMonth.DataBind();

        ddlMonth.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        if (ViewState["Month"] != null && ViewState["Month"] != "0")
        {
            if (dsdate.Tables[1].Rows.Count > 0)
            {
                DataRow[] result = dsdate.Tables[1].Select("imonth = '" + UDFLib.ConvertIntegerToNull(ViewState["Month"].ToString()) + "' ");
                if (result.Length > 0)
                    ddlMonth.SelectedValue = ViewState["Month"].ToString();
                else
                {
                    ddlMonth.SelectedIndex = 0;
                    ViewState["Month"] = null;
                }
            }
        }
        Load_PB_Received();
        int Fleet_ID = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int? Month = UDFLib.ConvertIntegerToNull(ddlMonth.SelectedValue);
        int? Year = UDFLib.ConvertIntegerToNull(ddlYear.SelectedValue);

        int iscount = ucCustomPagerPB.isCountRecord;

       
    }
    protected void GridViewPB_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objItemColumn);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }
    }
}