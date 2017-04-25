using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Globalization;

public partial class VesselMovement_PortCallHistory : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //dateValidator.ValueToCompare = DateTime.Now.ToShortDateString();
        //This change has been done to change the date format as per user selection
        CalStartDate.Format = Convert.ToString(Session["User_DateFormat"]);
        CalEndDate.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format

        if (!IsPostBack)
        {
            DateTime dt = System.DateTime.Now;
            DateTime Fistdaydate = dt.AddDays(-(dt.Day - 1));

            DateTime LastdayDate = dt.AddMonths(1);
            LastdayDate = LastdayDate.AddDays(-(LastdayDate.Day));
            //This change has been done to change the date format as per user selection
            txtStartDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(Fistdaydate));

            txtEndDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(LastdayDate));
           
                Load_VesselList();
                GetVesselID();
                Load_PortList();
                BindPortCallHistory(1, null, null);
        }
    }
    public void Load_PortList()
    {
        DataTable dtport = objBLLPort.Get_PortList_Mini();

        ddlportfilter.DataSource = dtport;
        ddlportfilter.DataTextField = "Port_Name";
        ddlportfilter.DataValueField = "Port_ID";
        ddlportfilter.DataBind();
        ddlportfilter.Items.Insert(0, new ListItem("All Port", "0"));

    }
    public void Load_VesselList()
    {
        if (Session["sFleet"] == null)
        {
            DataTable dtable = new DataTable();
            dtable.Columns.Add("PKID");
           

           
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            for(int i=0;i<FleetDT.Rows.Count;i++)
            {
                DataRow dr = dtable.NewRow();
                dr["PKID"] = FleetDT.Rows[i]["code"].ToString();

                dtable.Rows.Add(dr);
            }

            Session["sFleet"] = dtable;
        }
        
       

        DataTable dt = objPortCall.Get_PortCall_VesselList((DataTable)Session["sFleet"], 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        ddlvessel.DataSource = dt;
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public int GetVesselID()
    {
        try
        {

            if (Session["Vessel_ID"] != null)
            {
                return int.Parse(Session["Vessel_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    protected void BindPortCallHistory(int Type, int? PortID, int? VesselID)
    {


        DateTime dateTime;

        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
        {
            //This change has been done to validate the date format as per user selection
            if (DateTime.TryParseExact(txtStartDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) && DateTime.TryParseExact(txtEndDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                // Format your DateTime for valid date time error

                var from = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
                var To = UDFLib.ConvertToDefaultDt(txtEndDate.Text);

                DateTime Startdate = Convert.ToDateTime(from);
                DateTime EndDate = Convert.ToDateTime(To);

            DataTable dt = objPortCall.Get_PortCallHistory(Type, PortID, VesselID, Startdate, EndDate, rdborder.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                gvPortCallHistory.DataSource = dt;
                gvPortCallHistory.DataBind();
                gvPortCallHistory.Visible = true;
            }
            else
            {
                gvPortCallHistory.DataSource = dt;
                gvPortCallHistory.DataBind();
            }
           }
        }
    }
    protected void btnportfilter_Click(object sender, ImageClickEventArgs e)
    {
        
        BindPortCallHistory(1, UDFLib.ConvertIntegerToNull(ddlportfilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue));
    }

    protected void gvPortCallHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPortCallHistory.PageIndex = e.NewPageIndex;
        BindPortCallHistory(1, UDFLib.ConvertIntegerToNull(ddlportfilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue));
    }


    protected void DatesValidator_Validate(object source, ServerValidateEventArgs args)
    {

        DateTime dateTime;
        // Format your DateTime for valid date time error
        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
        {
            if (System.DateTime.TryParseExact(txtStartDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) && DateTime.TryParseExact(txtEndDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                // Format your DateTime for valid date time error
                var from = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
                var To = UDFLib.ConvertToDefaultDt(txtEndDate.Text);

                DateTime Startdate = Convert.ToDateTime(from);
                DateTime EndDate = Convert.ToDateTime(To);

                if (EndDate < Startdate)
                {
                    args.IsValid = false;
                    //Validate.Visible = true;
                }
            }
        }
    }
}