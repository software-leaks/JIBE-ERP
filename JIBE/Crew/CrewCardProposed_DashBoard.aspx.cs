using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_CrewCardProposed_DashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

        DataTable dt = objCrew.Get_CrewCardIndex(0, 0, 0, 0, "", Convert.ToInt32(Session["userid"]));

        GridView1.DataSource = dt;
        GridView1.DataBind();
        
    }
}