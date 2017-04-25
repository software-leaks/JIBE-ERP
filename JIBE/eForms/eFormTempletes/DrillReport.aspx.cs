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

public partial class eForms_eFormTempletes_DrillReport : System.Web.UI.Page
{
    int Form_ID;
    int Dtl_Report_ID;
    int Vessel_ID;
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    public UserAccess objUA = new UserAccess();
    BLL_FRM_LIB_DrillReport objBLLOps = new BLL_FRM_LIB_DrillReport();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.QueryString["param1"] != null && Request.QueryString["param2"] != null) && Request.QueryString["param3"] != null)
        {
            int Vessel_Id =  Convert.ToInt32(Request.QueryString["param1"]);        
            int Schedule_Id = Convert.ToInt32 (Request.QueryString["param2"]);
            int? Office_Id = Convert.ToInt32(Request.QueryString["param3"]);

            hdnOfficeID.Value = Office_Id.Value.ToString();
            hdnVesselId.Value = Vessel_Id.ToString();
            hdnSchId.Value = Schedule_Id.ToString();
            Load_Drill_Report_Details(Vessel_Id, Schedule_Id, Office_Id);
            UserAccessValidation();
        }
    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);
        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {
        }
    }
    private void Load_Drill_Report_Details(int Vessel_ID, int Schedule_Id, int? Office_Id)
    {
        try
        {
            DataSet ds = BLL_FRM_LIB_DrillReport.Get_DRILL_REPORT_Log(Vessel_ID, Schedule_Id, Office_Id);
            if (ds.Tables[0].Rows.Count > 0)
            {

                lblReportDate.Text = ds.Tables[0].Rows[0]["ReportDate"].ToString();
                lblVesselName.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
                lblDrillName.Text = ds.Tables[0].Rows[0]["PROGRAM_NAME"].ToString();
                lblLocation.Text = ds.Tables[0].Rows[0]["Location"].ToString();
       
            }
            grd_Drill_Description.DataSource = ds.Tables[1];
            grd_Drill_Description.DataBind();
            grd_Details_Answer.DataSource = ds.Tables[2];
            grd_Details_Answer.DataBind();
            gvTrainingItems.DataSource = ds.Tables[3];
            gvTrainingItems.DataBind();
        }
        catch
        {

        }
    }
 }