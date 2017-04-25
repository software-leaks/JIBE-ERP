using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Travel_UploadInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GetTravelRequests(string status)
    {
        //try
        //{
        //    BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        //    DataSet ds = new DataSet();

        //    int VCode = 0;
        //    if (!String.IsNullOrEmpty(cmbVessel.SelectedValue))
        //        VCode = Convert.ToInt32(cmbVessel.SelectedValue);

        //    ds = treq.Get_TravelRequests_Agent(Convert.ToInt32(Session["USERID"].ToString()),
        //            Convert.ToInt32(cmbFleet.SelectedValue), VCode,
        //        txtSectorFrom.Text, txtSectorTo.Text, txtTrvDateFrom.Text,
        //        txtTrvDateTo.Text, txtPaxName.Text, status);

        //    rptParent.DataSource = ds;
        //    rptParent.DataBind();
        //}
        //catch { }
    }
}