using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using SMS.Business.PMS;

public partial class Technical_PMS_TEC_Equipment_Replacement_History : System.Web.UI.Page
{
    BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                BindFleetDLL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                BindVesselDDL();

                Bindfunction();
                BindSystem_Location();
                BindSubSystem_Location();
                BindItems();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    /// <summary>
    /// bind list of function to drop down.
    /// </summary>
    public void Bindfunction()
    {

        DataTable dt = objBLLPURC.LibraryGetSystemParameterList("115", "");

        ddlFunction.Items.Clear();
        ddlFunction.DataSource = dt;
        ddlFunction.DataValueField = "CODE";
        ddlFunction.DataTextField = "DESCRIPTION";
        ddlFunction.DataBind();
        ddlFunction.Items.Insert(0, new ListItem("--ALL--", "0"));
        ddlFunction.SelectedIndex = 0;

    }

    /// <summary>
    /// To bind system for selected of vessel and function
    /// </summary>
    public void BindSystem_Location()
    {
        DataTable dt = objBLLPURC.GET_SYSTEM_LOCATION(UDFLib.ConvertToInteger(ddlFunction.SelectedValue), UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

        ddlSystem_location.Items.Clear();
        ddlSystem_location.DataSource = dt;
        ddlSystem_location.DataValueField = "AssginLocationID";
        ddlSystem_location.DataTextField = "LocationName";
        ddlSystem_location.DataBind();
        ddlSystem_location.Items.Insert(0, new ListItem("--ALL--", "0,0"));
        if (ddlSystem_location.SelectedIndex == -1)
            ddlSystem_location.SelectedIndex = 0;


    }

    /// <summary>
    /// To bind sub-system on for selected system.
    /// </summary>
    public void BindSubSystem_Location()
    {
        DataTable dt = objBLLPURC.GET_SUBSYTEMSYSTEM_LOCATION(ddlSystem_location.SelectedValue.ToString().Split(',')[1], null, UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

        ddlSubSystem_location.Items.Clear();
        ddlSubSystem_location.DataSource = dt;
        ddlSubSystem_location.DataValueField = "AssginLocationID";
        ddlSubSystem_location.DataTextField = "LocationName";
        ddlSubSystem_location.DataBind();
        ddlSubSystem_location.Items.Insert(0, new ListItem("--ALL--", "0,0"));
        if (ddlSubSystem_location.SelectedIndex == -1)
            ddlSubSystem_location.SelectedIndex = 0;

    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselDDL();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSystem_Location();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void ddlSystem_location_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSubSystem_Location();
        }
        catch (Exception ex)
        {
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// to bind list of fleets to drop down
    /// </summary>
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
            ListItem li = new ListItem("--ALL--", "0");
            DDLFleet.Items.Insert(0, li);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// to bind list of vessels to drop down
    /// </summary>
    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataBind();
            ListItem li = new ListItem("--ALL--", "0");
            DDLVessel.Items.Insert(0, li);
            DDLVessel.SelectedIndex = 0;


            if (Request.QueryString["Vessel_ID"] != null)
            {
                DDLVessel.SelectedValue = Request.QueryString["Vessel_ID"].ToString();

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Bind history to gridview.
    /// </summary>
    public void BindItems()
    {
        BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();
        int rowcount = 1;

        gvEQPHistory.DataSource = obj.Get_Equipment_Replacement_History(UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue),
                                                                      UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue),
                                                                      UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]),
                                                                      UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0]),
                                                                      ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        gvEQPHistory.DataBind();

        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        ucCustomPagerItems.BuildPager();
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        try
        {
            BindItems();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            DDLFleet.ClearSelection();
            DDLVessel.ClearSelection();
            ddlFunction.ClearSelection();

            BindSystem_Location();
            BindSubSystem_Location();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSystem_Location();
            BindSubSystem_Location();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
}