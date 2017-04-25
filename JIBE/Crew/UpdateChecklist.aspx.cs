using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Crew_UpdateChecklist : System.Web.UI.Page
{
    SMS.Business.Crew.BLL_Crew_CrewDetails objBLLCrew = new SMS.Business.Crew.BLL_Crew_CrewDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = objBLLCrew.UPDATE_VoyageDocumentChecklist();
            GridView1.DataBind();
        }
    }
}