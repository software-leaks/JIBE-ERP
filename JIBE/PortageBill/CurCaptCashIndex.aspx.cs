using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Web.Configuration;
using SMS.Business.PortageBill;
using SMS.Business.Infrastructure;


public partial class PortageBill_CurCaptCashIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_FleetList();
            Load_VesselList();
            CaptCashBind();
        }
    }
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
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
            Response.Redirect("~/default.aspx?msgid=1");
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
    public void CaptCashBind()
    {
        int? Vessel_ID;
        if (ddlVessel.SelectedValue.ToString() == "" || ddlVessel.SelectedValue.ToString() == "0")
        {
            Vessel_ID = null;
        }
        else
        {
            Vessel_ID = Convert.ToInt32(ddlVessel.SelectedValue);
        }


        DataSet CaptCashDs = BLL_PB_PortageBill.Get_CapCashReportCur(Vessel_ID);
        CaptCashGV.DataSource = CaptCashDs.Tables[0];
        CaptCashGV.DataBind();

    }

    protected void CaptCashGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CaptCashGV.PageIndex = e.NewPageIndex;
        CaptCashBind();
    }

    protected void unlockreport(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        //BAL.UnlockCaptCash();
    }



    protected void CaptCashGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            ResponseHelper.Redirect("CurCaptRpt.aspx?Vessel_ID=" + Args[0] + "&ID=" + Args[1], "_blank", "");
        }
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
        int Vessel_Manager = 1;

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

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
        CaptCashBind();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        CaptCashBind();
    }
}
