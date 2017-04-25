using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.QMS;
using System.Data;

public partial class QMS_EMS_EMS_Report_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtReport = BLL_EMS_Report.Get_Reports_Details(Convert.ToInt32(Request.QueryString["VSLID"]), Convert.ToInt32(Request.QueryString["ID"]));
            fvBunk.DataSource = dtReport;
            fvBunk.DataBind();

            fvEng.DataSource = dtReport;
            fvEng.DataBind();

            fvCargo.DataSource = dtReport;
            fvCargo.DataBind();

            fvSeca.DataSource = dtReport;
            fvSeca.DataBind();

            fvSlud.DataSource = dtReport;
            fvSlud.DataBind();

            fvWater.DataSource = dtReport;
            fvWater.DataBind();
            if (dtReport.Rows.Count > 0)
            {
                lbldtFrom.Text = dtReport.Rows[0]["DATE_FROM"].ToString();
                lbldtTO.Text = dtReport.Rows[0]["DATE_TO"].ToString();
                lblRemark.Text = dtReport.Rows[0]["GENERAL_REMARKS"].ToString();
                lblVslName.Text = dtReport.Rows[0]["Vessel_Name"].ToString();
            }
        }
    }
}