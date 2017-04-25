using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

public partial class Infrastructure_Snippets_Dash_PortCall_Month : System.Web.UI.Page
{
    DataTable dtMonths = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        dtMonths = BLL_Infra_DashBoard.Get_Portcalls_Month(Convert.ToInt32(Session["userid"]));
        ChrtPortCallsMonth.DataSource = dtMonths;
      
        ChrtPortCallsMonth.DataBind();
    }
    protected void ChrtPortCallsMonth_DataBound(object sender, EventArgs e)
    {
        DataRow[] dr;
        foreach (DataPoint pt in ChrtPortCallsMonth.Series[0].Points)
        {
            pt.IsValueShownAsLabel = true;
            pt.LabelForeColor = System.Drawing.Color.Black;
            pt.LabelBackColor = System.Drawing.Color.WhiteSmoke;

            dr = dtMonths.Select("rownum=" + pt.XValue.ToString() + "");
            pt.AxisLabel = dr[0]["pmonth"].ToString() + "-" + dr[0]["year2"].ToString() + Environment.NewLine +"VC-"+ dr[0]["VesselCount"].ToString();
       
        }
    }
}