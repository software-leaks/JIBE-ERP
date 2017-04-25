using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

public partial class CrewEvaluation_CrewEvaluationList_DashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvEvaluationSchedules.DataSource = BLL_Infra_DashBoard.Get_Evaluation_Schedules(Convert.ToInt32(Session["userid"].ToString()));
        gvEvaluationSchedules.DataBind();
    }
}