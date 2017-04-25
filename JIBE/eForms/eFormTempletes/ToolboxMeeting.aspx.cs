using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Properties;
using SMS.Business.eForms;

public partial class eForms_eFormTempletes_ToolboxMeeting : System.Web.UI.Page
{
    int Form_ID;
    int Dtl_Report_ID;
    int Vessel_ID;
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    public UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Form_ID = UDFLib.ConvertToInteger(Request.QueryString["Form_ID"]);
            Dtl_Report_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"]);
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

            Load_ToolboxMeeting_Details(Dtl_Report_ID, Vessel_ID);
        }
    }
    /// <summary>
    /// binding nested repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rpt1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        DataRowView dv = e.Item.DataItem as DataRowView;
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {

                nestedRepeater.DataSource = dv.CreateChildView("NestedCat");
                nestedRepeater.DataBind();
            }
        }
    }
    /// <summary>
    /// Modify binding gridview to repeater
    /// </summary>
    /// <param name="Main_Report_ID"></param>
    /// <param name="Vessel_ID"></param>
    private void Load_ToolboxMeeting_Details(int Main_Report_ID, int Vessel_ID)
    {
        try
        {
            DataSet ds = BLL_ToolboxMeetingReport.Get_ToolboxMeeting_Report(Main_Report_ID, Vessel_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblReportDate.Text = ds.Tables[0].Rows[0]["RptDate"].ToString();
                lblVesselName.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[1].Rows[0];
                ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[2].Columns["TopicType"], ds.Tables[1].Columns["TopicType"]));

                ds.Tables[2].TableName = "Members";

                rpt1.DataSource = ds.Tables[2];
                rpt1.DataBind();
            }
        
        }
        catch
        {

        }
    }
  
}