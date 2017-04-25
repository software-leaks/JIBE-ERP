using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Crew_CrewWall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string getVID()
    {
        if (Request.QueryString["vid"] != null)
            return Request.QueryString["vid"].ToString();
        else
            return "0";

    }
}