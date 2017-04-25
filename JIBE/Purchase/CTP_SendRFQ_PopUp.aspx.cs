using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_CTP_SendRFQ_Popup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            uc_Purc_Ctp_Send_RFQMore.SetAttribute_Refresh();
            uc_Purc_Ctp_Send_RFQMore.Contract_ID = Convert.ToInt32(Request.QueryString["Contract_ID"]);
        }
    }
}