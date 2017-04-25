using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;

public partial class Operations_PreArrivalAttachments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

            if (!IsPostBack)
            {
                if (Request.QueryString["PreArrivalId"] != null)
                {

                    lblRiskName.Text = Request.QueryString["RiskName"];
                    lblRiskType.Text = Request.QueryString["RiskType"];
                     


                    BindInspAttchment();
                }

            }
            lblErrorMsg.Text = "";
        
    }
    public void BindInspAttchment()
    {



        DataTable dtAttachment = BLL_OPS_PortReport.Get_PORT_PORT_PreArrivalAttachments(Convert.ToInt32(Request.QueryString["PreArrivalId"].ToString()),
            Convert.ToInt32(Request.QueryString["Vessel_ID"].ToString()), Convert.ToInt32(Request.QueryString["Office_ID"].ToString())).Tables[0];

        gvInspectionAttachment.DataSource = dtAttachment;
        gvInspectionAttachment.DataBind();

    }
}