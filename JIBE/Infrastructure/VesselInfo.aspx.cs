using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;

public partial class Infrastructure_VesselInfo : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);
            string Vessel_Code = UDFLib.ConvertStringToNull(Request.QueryString["VCODE"]);
            if (Vessel_ID != 0 || Vessel_Code != null)
            {
                Load_VesselInfo(Vessel_ID, Vessel_Code);
                lblDateStamp.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                lblDateStamp.Text = UDFLib.ConvertUserDateFormatTime(lblDateStamp.Text);
            }
        }
    }

    protected void Load_VesselInfo(int Vessel_ID, string Vessel_Code)
    {
        DataTable dt = objVessel.Get_VesselInfo_VID(Vessel_ID, Vessel_Code, UDFLib.ConvertToInteger(Session["UserID"]));

        rpt1.DataSource = dt;
        rpt1.DataBind();
    }

}