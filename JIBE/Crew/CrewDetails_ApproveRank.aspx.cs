using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_ApproveRank : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

            if (objUA.View == 1)
                pnlApproveRank.Visible = true;

        }
    }
    
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --
      
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void btnApproveRank_Click(object sender, EventArgs e)
    {
        int VoyageID = UDFLib.ConvertToInteger(hdnVoyageID_Approve.Value);
        int RankID = UDFLib.ConvertToInteger(ddlRank_Approve.SelectedValue);


        int ret = objBLLCrew.Approve_JoiningRank(VoyageID, RankID, GetSessionUserID(), txtApproveRankRemark.Text);

        if (ret == 1)
        {
            string js = "parent.ShowNotification('Alert','Joining rank approved for the voyage.', true);parent.hideModal('dvPopupApproveRank');setTimeout(Bind_HoverEffect, 100);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsJ", js, true);

        }
        if (ret == 2)
        {
            string js = "parent.ShowNotification('Alert','Joining rank already approved.', true);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsJ", js, true);
        }
    }
}