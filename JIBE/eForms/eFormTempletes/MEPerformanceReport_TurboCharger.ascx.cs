using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MEPerformanceReport;

public partial class eForms_eFormTempletes_MEPerformanceReport_TurboCharger : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Show_TC_Record(int TCID, int Main_Report_ID, int Vessel_ID)
    {
        DataSet ds = BLL_FRM_MEPerformanceReport.Get_MEPerformanceReport_TC(TCID, Main_Report_ID, Vessel_ID);

        frmTurboCharger.DataSource = ds.Tables[1];
        frmTurboCharger.DataBind();

    }
}