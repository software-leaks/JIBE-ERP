using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AEPerformanceReport;

public partial class eForms_eFormTempletes_AEPerfReport_Engine : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Show_Engine_Record(int EngineID, int Main_Report_ID, int Vessel_ID)
    {        
        DataSet ds = BLL_FRM_AEPerformanceReport.Get_AEPerformanceReport_Engine(EngineID, Main_Report_ID, Vessel_ID);

        frmEngine.DataSource = ds.Tables[2];
        frmEngine.DataBind();

        GridView GridView_Units = (GridView)frmEngine.FindControl("GridView_Units");
        if (GridView_Units != null)
        {
            GridView_Units.DataSource = ds.Tables[3];
            GridView_Units.DataBind();
        }

    }

}