using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_CrewCardAttachments : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int CardID = UDFLib.ConvertToInteger(Request.QueryString["CardID"]);
            int UserID = UDFLib.ConvertToInteger(Request.QueryString["UserID"]);

            Load_Attachments(CardID, UserID);
        }
    }

    protected void Load_Attachments(int CardID, int UserID)
    {
        DataTable dt = objBLLCrew.Get_CardAttachments(CardID, UserID);
        rptAttachments.DataSource = dt;
        rptAttachments.DataBind();
    }

}