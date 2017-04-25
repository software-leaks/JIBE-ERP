



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

public partial class eForms_eFormTempletes_TankInspection : System.Web.UI.Page
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

            LoadDetails(Dtl_Report_ID, Vessel_ID);

        }


    }
    
    protected void GridView_Tanker_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItemIndex == 22)
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red; 
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red; 
               
            }

        }
    }

    private void LoadDetails(int Main_Report_ID, int Vessel_ID)
    {

        try
        {

            DataSet ds = BLL_TankerInspection.GET_TANK_REPORT_DETAILS(Vessel_ID, Main_Report_ID);
            if (ds.Tables[1].Rows.Count > 0)
            {

                lblYear.Text = ds.Tables[0].Rows[0]["Report_Year"].ToString();
                lblVesselName.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
                txtremarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                if ( ds.Tables[0].Rows[0]["VslMasterID"].ToString() != "" &&  ds.Tables[0].Rows[0]["VslMasterID"].ToString() != "0")
                {
                    lnkMaster.Text =  ds.Tables[0].Rows[0]["VslMasterName"].ToString();
                    lnkMaster.NavigateUrl = "~/crew/crewdetails.aspx?ID=" +  ds.Tables[0].Rows[0]["VslMasterID"].ToString();
                    lnkMaster.Target = "_blank";
                    imgMaster.ImageUrl = hdnBaseURL.Value + "uploads/CrewImages/" +  ds.Tables[0].Rows[0]["PhotoUrl1"].ToString();
                    lnkMaster.Visible = true;
                    imgMaster.Visible = true;
                }
                else
                {
                    lnkMaster.Visible = false;
                    imgMaster.Visible = false;
                }

                if ( ds.Tables[0].Rows[0]["VslChiefOfficerID"].ToString() != "" &&  ds.Tables[0].Rows[0]["VslChiefOfficerID"].ToString() != "0")
                {
                    lnkChiefOfficer.Text =  ds.Tables[0].Rows[0]["VslChiefOfficerName"].ToString();
                    lnkChiefOfficer.NavigateUrl = "~/crew/crewdetails.aspx?ID=" +  ds.Tables[0].Rows[0]["VslChiefOfficerID"].ToString();
                    lnkChiefOfficer.Target = "_blank";
                    imgChiefOfficer.ImageUrl = hdnBaseURL.Value + "uploads/CrewImages/" +  ds.Tables[0].Rows[0]["PhotoUrl2"].ToString();
                    lnkChiefOfficer.Visible = true;
                    imgChiefOfficer.Visible = true;
                }
                else
                {
                    lnkChiefOfficer.Visible = false;
                    imgChiefOfficer.Visible = false;
                }

            }
            GridView_Tanker.DataSource = ds.Tables[1];
            GridView_Tanker.DataBind();
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4, 5, 6 }, "Units Received", "HeaderStyle-css");
           
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
        ////string VESSEL = UDFLib.ConvertStringToNull(lblVesselName.Text);

        //string VESSEL = Convert.ToString(lblVesselName.Text);
        //string rptddate = Convert.ToString(lblReportDate.Text);

        //string str = "lashing Gear Inventory Report" + " &nbsp; " + "Vessel Name: " + VESSEL + "&nbsp; " + "Report Date:" + rptddate + ".";
        //DataSet ds = BLL_LGInventoryReport.Get_LGI_REPORT_Log(UDFLib.ConvertToInteger(Request.QueryString["DtlID"]), UDFLib.ConvertToInteger(Request.QueryString["VID"]));

        //string[] HeaderCaptions = { "Item Description", "Model", "Opening R.O.B.", "Owner", "Charterer", "Owner", "Charterer", "Units Refurbished/Repaired", "Units Claimed from Stevedores", "Closing R.O.B.", "Requirement per Cargo Securing Manual", "Supply Required" };
        //string[] DataColumnsName = { "Item_Description", "Model_No", "OpeningROB", "UR_Owner_No", "UR_Charterer_No", "OP_Owner_No", "OP_Charterer_No", "UNIT_Repaired_No", "Unit_Claimed_No", "ClosingROB", "Carg_Securing_Mannual_No", "SupplyReq" };
        //GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "lashingGearInventoryReport", str);


    }
}