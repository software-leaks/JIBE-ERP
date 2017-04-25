using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 
using SMS.Business.Technical;

public partial class Technical_ERLog_ErLogTask_Followups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            int LOG_ID = UDFLib.ConvertToInteger(Request.QueryString["LID"].ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());


            DataTable dt = BLL_Tec_ErLog.Get_FollowupList(Vessel_ID, LOG_ID);

            grdFollowUps.DataSource =dt;
            grdFollowUps.DataBind();
            

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
}