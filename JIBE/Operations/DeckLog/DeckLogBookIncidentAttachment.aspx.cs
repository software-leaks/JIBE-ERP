using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PMS;
using System.IO;
using SMS.Business.Operations;


public partial class  DeckLogBookIncidentAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindParticipantAttachment();
        }
    }

    private void BindParticipantAttachment()
    {
        
  
        DataSet ds = BLL_OPS_DeckLog.Get_DeckLogBook_Incident_Participant_Att_Search(int.Parse(Request.QueryString["Incident_ID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            //Bind the header

            lblIncidentDate.Text = ds.Tables[0].Rows[0]["INCIDENT_DATE"].ToString();
            lblIncidentType.Text = ds.Tables[0].Rows[0]["INCIDENT_TYPE"].ToString();
            txtActionTaken.Text = ds.Tables[0].Rows[0]["ACTION_TAKEN"].ToString();
            txtIncidentDetails.Text = ds.Tables[0].Rows[0]["DETAILS_OF_INCIDENT"].ToString();

        }

        if (ds.Tables[1].Rows.Count > 0)
        {
        
        //Bind the Photos
            rptDrillImages.DataSource = ds.Tables[1];
            rptDrillImages.DataBind();

            string FileName = ds.Tables[1].Rows[0]["PhotoUrl"].ToString();
            string FilePath = "../../Uploads/Incident/" + Path.GetFileName(ds.Tables[1].Rows[0]["PhotoUrl"].ToString());

            if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
            {
                Random r = new Random();
                string ver = r.Next().ToString();
                frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
            }
        }
    }


    protected void rptDrillImages_ItemCommand(Object sender, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDocument")
            {
                string FileName = e.CommandArgument.ToString();
                string FilePath = "../../Uploads/Incident/" + Path.GetFileName(FileName);
                frmContract.Attributes.Add("src", "../../Images/FileNotFound.png");

                if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
                {
                    Random r = new Random();
                    string ver = r.Next().ToString();
                    frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
                }
            }
        }
        catch { }

    }

    protected void rptDrillImages_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            object obj = e.Item.DataItem;
            Image imgDocIcon = (Image)e.Item.FindControl("imgDocIcon");
            if (imgDocIcon != null)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                string icon = Path.GetExtension(dr.Row["PhotoUrl"].ToString()).Replace(".", "");
                if (System.IO.File.Exists(Server.MapPath("~/images/DocTree/" + icon + ".gif")) == true)
                {
                    imgDocIcon.ImageUrl = "~/images/DocTree/" + icon + ".gif";
                }
            }
        }
    }
}