﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.Data;


public partial class PortageBill_VesselMinCTM : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();
            Load_VesselMinCTM();
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


    protected void Load_VesselMinCTM()
    {
        DataTable dt = BLL_PB_PortageBill.Get_VesselMinCTM(UDFLib.ConvertToInteger(ddlFleet.SelectedValue), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), GetSessionUserID());
        gvVesselMinCTM.DataSource = dt;
        gvVesselMinCTM.DataBind();

    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_VesselMinCTM();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselMinCTM();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow dr in gvVesselMinCTM.Rows)
        {
            try
            {
                string Vessel_ID = ((HiddenField)dr.FindControl("hdnVesselID")).Value;
                string Min_CTM = (((TextBox)dr.FindControl("txtMinCTM")).Text).ToString();
                if (Min_CTM != "")
                {
                    BLL_PB_PortageBill.UPDATE_VesselMinCTM(int.Parse(Vessel_ID), decimal.Parse(Min_CTM), GetSessionUserID());
                }
            }
            catch { }
        }

        Load_VesselMinCTM();
    }

}