using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

public partial class Infrastructure_Snippets_Dash_PortCalls_Vessel : System.Web.UI.Page
{
    DataTable dtVsl = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        dtVsl = BLL_Infra_DashBoard.Get_Portcalls_Vessel(Convert.ToInt32(Session["userid"]));
        ChrtPortCallsVessel.DataSource = dtVsl;
        ChrtPortCallsVessel.DataBind();

    }

    protected void ChrtPortCallsVessel_DataBound(object s, EventArgs e)
    {
        DataRow []dr;
        foreach (DataPoint pt in ChrtPortCallsVessel.Series[0].Points)
        {
            pt.IsValueShownAsLabel = true;
            pt.LabelForeColor = System.Drawing.Color.Black;

            pt.LabelBackColor = System.Drawing.Color.WhiteSmoke;
           dr= dtVsl.Select("RNUM="+pt.XValue.ToString()+"");
           pt.AxisLabel = dr[0]["Vessel_code"].ToString();

        }
    }
}