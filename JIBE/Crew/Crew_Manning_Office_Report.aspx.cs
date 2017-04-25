using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_Crew_Manning_Office_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        BLL_Crew_Admin obj = new BLL_Crew_Admin();
        DataTable dt = obj.Get_Manning_Report();
        grdManning.DataSource = dt;
        grdManning.DataBind();
    }
}