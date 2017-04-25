using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;

public partial class Purchase_TrackDeliveryStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

        gvStatus.DataSource = objTechService.BindRequisitionDeliveryStagesLog(Request.QueryString["sOrderCode"]);
        gvStatus.DataBind();
    }
}