using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Inspection;
using SMS.Business.Infrastructure;

public partial class Surveys_InspectionList : System.Web.UI.Page
{
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    BLL_Infra_Port objInfra = new BLL_Infra_Port();
    public string Status = "";
    public DateTime IssueDate;
    public int VesselId;
    public string InspectionType;
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateFormat = UDFLib.GetDateFormat();
            calFromDate.Format = calToDate.Format = UDFLib.GetDateFormat();
            if (Request.QueryString.Count != 0)
            {
               if (Request.QueryString["VesselId"] != null && Request.QueryString["VesselId"] != "")
                    VesselId = Convert.ToInt16(Request.QueryString["VesselId"].ToString());
               if (Request.QueryString["InspectionType"] != null && Request.QueryString["InspectionType"] != "")
                   InspectionType = Request.QueryString["InspectionType"].ToString();
            }
            if (!IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    if (Request.QueryString["Status"] != null && Request.QueryString["Status"] != "")
                        Status = Request.QueryString["Status"].ToString();
                    if (Request.QueryString["IssueDate"] != null && Request.QueryString["IssueDate"] != "")
                    {
                        IssueDate = UDFLib.ConvertToDate(Request.QueryString["IssueDate"].ToString());
                        txtInspectionFromDate.Text = UDFLib.ConvertUserDateFormat(IssueDate.ToShortDateString(), DateFormat);
                    }                   
                }
                BindPort();
                ddlStatus.ClearSelection();
                ddlStatus.Items.FindByValue(Status).Selected = true;
                DataSet ds = objInsp.Get_Current_Schedules(Status, VesselId, IssueDate.ToShortDateString().Equals("01/01/0001") ? null : UDFLib.ConvertDateToNull(IssueDate),null, 0);
                gvInspectionSchedule.DataSource = ds.Tables[0];
                gvInspectionSchedule.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Bind the port dropdown
    /// </summary>
    protected void BindPort()
    {
        try
        {
            DDLPort.DataSource = objInfra.Get_PortList();
            DDLPort.DataValueField = "PORT_ID";
            DDLPort.DataTextField = "PORT_NAME";
            DDLPort.DataBind();
            DDLPort.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Selected Inspection will be set to parent textbox either to Certificate Inspection or Renewal Inspection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelectEvaluation_Click(object sender, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int InspectionID;
        try
        {
            InspectionID = UDFLib.ConvertToInteger(arg[1]);
            string dt = UDFLib.ConvertUserDateFormat(arg[0].ToString());

            string js = "parent.BindInspectionDate('" + InspectionType + "'," + InspectionID + ",'" + dt + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Open the Inspection List based on Certificate Inspection /Renewable Inspection icon
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchInspection_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objInsp.Get_Current_Schedules(ddlStatus.SelectedValue, VesselId, txtInspectionFromDate.Text == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtInspectionFromDate.Text)), txtInspectionToDate.Text == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtInspectionToDate.Text)), Convert.ToInt16(DDLPort.SelectedValue));
            if (ds.Tables.Count > 0)
            {
                gvInspectionSchedule.DataSource = ds.Tables[0];
                gvInspectionSchedule.DataBind();
            }
            else
            {
                gvInspectionSchedule.DataSource = null;
                gvInspectionSchedule.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}