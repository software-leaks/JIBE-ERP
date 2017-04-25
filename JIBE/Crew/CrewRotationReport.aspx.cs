using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;


public partial class Crew_CrewRotationReport : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_Country objCountry = new BLL_Infra_Country();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        calFrom.Format = DateFormat;
        calTo.Format = DateFormat;

        if (Session["USERID"] == null)
            Response.Redirect("~/ACCOUNT/LOGIN.ASPX");

        if (!IsPostBack)
        {
            Load_VesselManagerList();
            Load_ManningAgentList();
            Load_FleetList();
            Load_VesselList();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private int GetSessionUserCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    public void Load_VesselManagerList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        ddlVessel_Manager.DataSource = objCrew.Get_VesselManagerList(UserCompanyID);
        ddlVessel_Manager.DataTextField = "COMPANY_NAME";
        ddlVessel_Manager.DataValueField = "ID";
        ddlVessel_Manager.DataBind();
        ddlVessel_Manager.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

    }
    public void Load_ManningAgentList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlManningOffice.DataSource = objCrew.Get_ManningAgentList(UserCompanyID);
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();

        try
        {
            if (Session["UTYPE"].ToString() == "MANNING AGENT")
            {
                ddlManningOffice.Text = Session["USERCOMPANYID"].ToString();
                ddlManningOffice.Enabled = false;
            }
            else
            {
                ddlManningOffice.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                
            }
        }
        catch
        {

        }
    }
    public void Load_FleetList()
    {
        int VesselManager = UDFLib.ConvertToInteger(ddlVessel_Manager.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID, VesselManager);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int Vessel_Manager = int.Parse(ddlVessel_Manager.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void ddlVessel_Manager_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_FleetList();
        Load_VesselList();
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int RankID = 0;

        DataSet ds = BLL_Crew_CrewRotation.Get_RotationReport(0,UDFLib.ConvertToInteger(ddlVessel_Manager.SelectedValue),UDFLib.ConvertToInteger(ddlManningOffice.SelectedValue),UDFLib.ConvertToInteger(ddlFleet.SelectedValue),UDFLib.ConvertToInteger(ddlVessel.SelectedValue),RankID,UDFLib.ConvertToDefaultDt(txtFromDate.Text), UDFLib.ConvertToDefaultDt(txtToDate.Text), txtSearch.Text,GetSessionUserID());

        UDFLib.AddParentTable(ds.Tables[0], "Parent", new string[] { "Vessel_ID" },
        new string[] { "vessel_name"}, "ChildMembers");


        rpt1.DataSource = ds;
        rpt1.DataMember = "Parent";
        rpt1.DataBind();

        //ExportToExcel(rpt1);
    }
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
        
    //}
    //protected void ExportToExcel(Repeater rpt1) 
    //{

    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("Content-Disposition",
    //        "attachment; filename=ExcelFile.xls");

    //    Response.BufferOutput = true;
    //    Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    Response.Charset = "UTF-8";
    //    EnableViewState = false;

    //    System.IO.StringWriter tw = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter hw =
    //        new System.Web.UI.HtmlTextWriter(tw);

    //    rpt1.RenderControl(hw);

    //    Response.Write(tw.ToString());
    //    //Response.End();

    //} 

}