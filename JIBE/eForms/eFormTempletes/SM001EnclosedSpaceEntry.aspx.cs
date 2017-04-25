using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AERunningHoursMonthlyReport;
using SMS.Business.eForms;

public partial class SM001EnclosedSpaceEntry : System.Web.UI.Page
{
    int Form_ID;
    int Dtl_Report_ID;
    int Vessel_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form_ID = UDFLib.ConvertToInteger(Request.QueryString["Form_ID"]);
            Dtl_Report_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"]);
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]); 
            
            Load_AE_Report_Details(Dtl_Report_ID, Vessel_ID);
        }
    }

    private void Load_AE_Report_Details(int Report_ID, int Vessel_ID)
    {
        DataTable dt = BLL_DANFormO1DrugAndAlcoholPolicyDeclaration.GetDrugAndAlcoholPolicy(Report_ID, Vessel_ID);
        
        frmMain.DataSource =dt;
        frmMain.DataBind();

    }
}