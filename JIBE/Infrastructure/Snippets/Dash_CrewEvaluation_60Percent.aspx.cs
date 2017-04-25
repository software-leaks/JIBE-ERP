using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

public partial class Infrastructure_Snippets_Dash_CrewEvaluation_60Percent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvEvaluationSchedules.DataSource = BLL_Infra_DashBoard.Get_Evaluation_60Percent(Convert.ToInt32(Session["userid"].ToString()));
        gvEvaluationSchedules.DataBind();
    }
}