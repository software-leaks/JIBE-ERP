using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Crew ;
using SMS.Business.Infrastructure;

/// <summary>
/// Crew Setting is configuration screen which will have access only to User with Admin access
/// </summary>
public partial class Infrastructure_Libraries_Crew_Performance_SnippetSettings : System.Web.UI.Page
{
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_Rank objRank = new BLL_Infra_Rank();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        if (!IsPostBack)
        {           
            DataTable dtRankList = objRank.Get_Ranks_Snippet();
            gvRankList.DataSource = dtRankList;
            gvRankList.DataBind();
            UpdatePanel1.Update();
          
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Admin == 0)
        {
            btnSaveRank.Enabled = false;
         
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
   
    protected void btnSaveRank_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RankId", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        int Mandatory;
        int RankId;
        foreach (GridViewRow dr in gvRankList.Rows)
        {
            RankId = int.Parse(((Label)dr.FindControl("lblRankId")).Text.ToString());
            Mandatory = ((CheckBox)dr.FindControl("chkSelected")).Checked == true ? 1 : 0;
            dt.Rows.Add(RankId, Mandatory);
        }
        int status = objRank.INS_RankSnippetList(dt, Convert.ToInt32(Session["USERID"]));
        if (status > 0)
        {
            string js = "alert('Crew Performance Snippet Setting Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }
         
   
}