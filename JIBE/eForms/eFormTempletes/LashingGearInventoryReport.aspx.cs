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

public partial class eForms_eFormTempletes_LashingGearInventoryReport : System.Web.UI.Page
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

            Load_LashingGearInventoryReport_Details(Dtl_Report_ID, Vessel_ID);

        }


    }
    //protected void UserAccessValidation()
    //{

    //    string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

    //    objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

    //    if (objUA.View == 0)
    //    {
    //        Response.Redirect("~/crew/default.aspx?msgid=1");
    //    }

    //    if (objUA.Add == 0)
    //    {

    //    }
    //}
    protected void GridView_LashingInventoryRPT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);


        }
    }

    private void Load_LashingGearInventoryReport_Details(int Main_Report_ID, int Vessel_ID)
    {

        try
        {

            DataSet ds = BLL_LGInventoryReport.Get_LGI_REPORT_Log(Main_Report_ID, Vessel_ID);
            if (ds.Tables[1].Rows.Count > 0)
            {

                lblReportDate.Text = ds.Tables[1].Rows[0]["RptDate"].ToString();
                lblVesselName.Text = ds.Tables[1].Rows[0]["Vessel_Name"].ToString();
                txtremarks.Text = ds.Tables[1].Rows[0]["Remarks"].ToString();
                lblMasterSignature.Text = ds.Tables[1].Rows[0]["MasterName"].ToString();
                //lblmasterDate.Text = ds.Tables[1].Rows[0]["CrewMsDate"].ToString();
                lblchiefofficer.Text = ds.Tables[1].Rows[0]["COName"].ToString();
                //lblchiefofficerdate.Text = ds.Tables[1].Rows[0]["CrewCoDate"].ToString();

            }
            GridView_LashingInventoryRPT.DataSource = ds.Tables[0];
            GridView_LashingInventoryRPT.DataBind();
            objChangeReqstMerge.AddMergedColumns(new int[] { 4, 5 }, "Units Received", "HeaderStyle-css");
            objChangeReqstMerge.AddMergedColumns(new int[] { 6, 7 }, "Operational Losses or Condemned", "HeaderStyle-css");
            //UserAccessValidation();


        }
        catch
        {

        }
    }
    //private void Load_LashingGearInventoryReport_Details(int Main_Report_ID, int Vessel_ID)
    //{ 

    //}
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {
        //string VESSEL = UDFLib.ConvertStringToNull(lblVesselName.Text);

        string VESSEL = Convert.ToString(lblVesselName.Text);
        string rptddate = Convert.ToString(lblReportDate.Text);

        string str = "lashing Gear Inventory Report" + " &nbsp; " + "Vessel Name: " + VESSEL + "&nbsp; " + "Report Date:" + rptddate + ".";
        DataSet ds = BLL_LGInventoryReport.Get_LGI_REPORT_Log(UDFLib.ConvertToInteger(Request.QueryString["DtlID"]), UDFLib.ConvertToInteger(Request.QueryString["VID"]));

        string[] HeaderCaptions = { "Item Description", "Model", "Opening R.O.B.", "Owner", "Charterer", "Owner", "Charterer", "Units Refurbished/Repaired", "Units Claimed from Stevedores", "Closing R.O.B.", "Requirement per Cargo Securing Manual", "Supply Required" };
        string[] DataColumnsName = { "Item_Description", "Model_No", "OpeningROB", "UR_Owner_No", "UR_Charterer_No", "OP_Owner_No", "OP_Charterer_No", "UNIT_Repaired_No", "Unit_Claimed_No", "ClosingROB", "Carg_Securing_Mannual_No", "SupplyReq" };
        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "lashingGearInventoryReport", str);


    }
}