using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
public partial class Purchase_Delivery_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLL_PURC_Purchase objdl = new BLL_PURC_Purchase();
            gvDlHistory.DataSource = objdl.Get_Delivery_History(Request.QueryString["itemcode"].ToString(), Request.QueryString["VesselCode"].ToString());
            gvDlHistory.DataBind();
        }

    }
}