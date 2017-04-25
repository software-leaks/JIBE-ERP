using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Operations;

public partial class DeckLogBookParticipant : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindParticipantList();
        }

    }

   public void BindParticipantList()
    {

        DataSet ds = BLL_OPS_DeckLog.Get_DeckLogBook_Incident_Participant_Search(int.Parse(Request.QueryString["Incident_ID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (ds.Tables[0].Rows.Count > 0)
        {

            lblIncidentDate.Text = ds.Tables[0].Rows[0]["INCIDENT_DATE"].ToString();
            lblIncidentType.Text = ds.Tables[0].Rows[0]["INCIDENT_TYPE"].ToString();
            txtActionTaken.Text = ds.Tables[0].Rows[0]["ACTION_TAKEN"].ToString();
            txtIncidentDetails.Text = ds.Tables[0].Rows[0]["DETAILS_OF_INCIDENT"].ToString();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            rpIncidentParticipant.DataSource = ds.Tables[1];
            rpIncidentParticipant.DataBind();
        }
    }


}