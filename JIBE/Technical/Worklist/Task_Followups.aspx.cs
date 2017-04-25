using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.Technical;

public partial class Operations_TaskPlanner_Task_Followups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();

            int Worklist_ID = UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["OFFID"].ToString());

            DataTable dt = objBLL.Get_FollowupList(Office_ID,Vessel_ID,Worklist_ID);

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