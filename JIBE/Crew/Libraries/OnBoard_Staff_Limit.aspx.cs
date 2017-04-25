using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.Data;

public partial class Crew_Libraries_OnBoard_Staff_Limit : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    BLL_Crew_Admin objCrew = new BLL_Crew_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();

            ddlVessel.SelectedValue = "1";

            BindOnBoardRankLimit();
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
            btnSave.Enabled = false;
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
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }

    protected void Load_FleetList()
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

    protected void BindOnBoardRankLimit()
    {
        DataTable dt = objCrew.Get_Rank_Onboard_Limit_Search(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
        gvRankLimit.DataSource = dt;
        gvRankLimit.DataBind();

    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        BindOnBoardRankLimit();
    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOnBoardRankLimit();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow dr in gvRankLimit.Rows)
        {
            try
            {

                string Max_Limit = ((TextBox)dr.FindControl("txtMaxLimit")).Text != "" ? ((TextBox)dr.FindControl("txtMaxLimit")).Text : "0";
                string Min_Limit = ((TextBox)dr.FindControl("txtMinLimit")).Text != "" ? ((TextBox)dr.FindControl("txtMinLimit")).Text : "0";
                string RankID = ((Label)dr.FindControl("lblRankID")).Text;

                int Retval = objCrew.Update_Rank_OnBoard_Limit(Convert.ToInt32(ddlVessel.SelectedValue)
                    , Convert.ToInt32(((Label)dr.FindControl("lblRankID")).Text)
                    , Convert.ToInt32(Min_Limit)
                    , Convert.ToInt32(Max_Limit)
                    , Convert.ToInt32(Session["USERID"].ToString()));

                if (Retval > 0)
                {
                    string js = "alert('Onboard staff limits saved successfully!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);

                }
            }
            catch { }
        }

        BindOnBoardRankLimit();
    }
}