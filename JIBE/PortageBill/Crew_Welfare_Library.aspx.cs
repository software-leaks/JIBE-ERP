using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.Data;

public partial class PortageBill_Crew_Welfare_Library : System.Web.UI.Page
{
    public string DFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DFormat = UDFLib.GetDateFormat();
            UserAccessValidation();
            Load_FleetList();
            Load_VesselList();
            Load_EffectiveDates();
            BindItems();
            calEfftv.Format = DFormat;
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = UDFLib.ConvertToInteger(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAddNew.Visible = false;
        }

        if (objUA.Edit == 0)
        {
            btnSaveDetails.Visible = false;
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }
    }

    protected void btnSaveDetails_Click(object s, EventArgs e)
    {
        try
        {
            int Vsl_ID = 0;
            if (UDFLib.ConvertToInteger(hdfWelfare_ID.Value) == 0)
                Vsl_ID = Convert.ToInt32(ddlVesselUpd.SelectedValue);

            BLL_PB_PortageBill.UPD_Lib_Crew_Welfare(UDFLib.ConvertToInteger(hdfWelfare_ID.Value), UDFLib.ConvertToDecimal(txtWelfareAmount.Text), Vsl_ID, UDFLib.ConvertToDate(txtEffectiveDate.Text, UDFLib.GetDateFormat()), Convert.ToInt32(Session["userid"]));
            BindItems();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "hideModal('dvWelfare')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MMCD", "showModal('dvWelfare')", true);
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSearch_Click(object s, EventArgs e)
    {
        BindItems();
    }

    public void Load_FleetList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
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
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, UserCompanyID, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;

        if (!IsPostBack)
        {
            ddlVesselUpd.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, UserCompanyID, "", UserCompanyID);
            ddlVesselUpd.DataTextField = "VESSEL_NAME";
            ddlVesselUpd.DataValueField = "VESSEL_ID";
            ddlVesselUpd.DataBind();
            ddlVesselUpd.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlVesselUpd.SelectedIndex = 0;
        }
    }

    protected void Load_EffectiveDates()
    {
        DataTable dt = BLL_PB_PortageBill.Get_Crew_Welfare_Effective_Dates(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
        UDFLib.ChangeColumnDataType(dt, "Effective_Date", typeof(string));
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["Effective_Date"].ToString()))
                {
                    dr["Effective_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["Effective_Date"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
            }
        }
        ddlEectiveDates.DataSource = dt;
        ddlEectiveDates.DataTextField = "EFFECTIVE_DATE";
        ddlEectiveDates.DataValueField = "EFFECTIVE_DATE";
        ddlEectiveDates.DataBind();
        ListItem liSelect = new ListItem("ALL", "0");
        ddlEectiveDates.Items.Insert(0, liSelect);
    }

    protected void BindItems()
    {
        try
        {
            int Record_Count = 0;
            DataTable dt = BLL_PB_PortageBill.Get_Lib_Crew_Welfare(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertDateToNull(ddlEectiveDates.SelectedValue), pagerWf.CurrentPageIndex, pagerWf.PageSize, ref Record_Count);
            UDFLib.ChangeColumnDataType(dt, "Effective_Date", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Effective_Date"].ToString()))
                        dr["Effective_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Effective_Date"].ToString()), UDFLib.GetDateFormat());
                }
            }
            gvWelfare.DataSource = dt;
            gvWelfare.DataBind();

            pagerWf.CountTotalRec = Record_Count.ToString();
            pagerWf.BuildPager();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();

    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_EffectiveDates();
    }
}