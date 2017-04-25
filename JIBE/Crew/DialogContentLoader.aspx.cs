using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Crew;


public partial class Crew_DialogContentLoader : System.Web.UI.Page
{

    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        string ContentName = Request.QueryString["N"].ToString();
        string CrewID = Request.QueryString["ID"].ToString();
        DataTable dt;
        switch (ContentName)
        {
            case "REMARKS":
                if (CrewID.Length > 0)
                {
                    dt = objBLLCrew.Get_StaffStatusRemarks(int.Parse(CrewID), int.Parse(Session["USERCOMPANYID"].ToString()));

                    GridView1.AutoGenerateColumns = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                break;
        }

    }
}