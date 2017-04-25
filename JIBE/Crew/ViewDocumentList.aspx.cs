using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SMS.Business.Crew;

public partial class Crew_ViewDocumentList : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        int CrewID = 0;
        string FilterString = "";
        string DocTypeName = "";

        if (Request.QueryString["CrewID"] != null)
            CrewID = int.Parse(Request.QueryString["CrewID"].ToString());

        if (Request.QueryString["FilterString"] != null) 
            FilterString = Request.QueryString["FilterString"].ToString();

        if (Request.QueryString["DocTypeName"] != null)
            DocTypeName = Request.QueryString["DocTypeName"].ToString();

        Load_DocumentList(CrewID, FilterString, DocTypeName);
    }

    private void Load_DocumentList(int CrewID, string FilterString, string DocTypeName)
    {
        GridView_Documents.DataSource = objCrewBLL.Get_CrewDocumentList(CrewID,int.Parse(Session["USERCOMPANYID"].ToString()), FilterString, DocTypeName);
        GridView_Documents.DataBind();
    }
}