using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Crew_CrewMR_HO_Checklist : System.Web.UI.Page
{

    #region BusinessLayerAccess
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
    BLL_Infra_Rank obj = new BLL_Infra_Rank();
    UserAccess objUA = new UserAccess();
    #endregion


    #region GlobalValriableDeclaration
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    #endregion



    #region PageLoadEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            BindRank();
            getCheckedRank();

        }
    }
    #endregion
 
    #region AccessValidation & UserDetails

    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");


        if (objUA.Add == 1)
            btnSave.Visible = true;
        else
            btnSave.Visible = false;

       

    }

    #endregion
  
    #region BindRankDetails

    public void BindRank()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        chkRank1.DataSource = dt;
        chkRank1.DataTextField = "Rank_Name";
        chkRank1.DataValueField = "ID";
        chkRank1.DataBind();


        chkRank2.DataSource = dt;
        chkRank2.DataTextField = "Rank_Name";
        chkRank2.DataValueField = "ID";
        chkRank2.DataBind();
    }


    #endregion

    #region GetRankDetails
    public void getCheckedRank()
    {
        DataTable dt = obj.Get_Rank_Configuration();
        DataRow[] rows1 = dt.Select("[MasterReview] is not null");
        foreach (DataRow dr in rows1)
        {
            for (int i = 0; i < chkRank1.Items.Count; i++)
            {
                if (UDFLib.ConvertToInteger(dr["Rank"].ToString()) == UDFLib.ConvertToInteger(chkRank1.Items[i].Value))
                {
                    chkRank1.Items[i].Selected = true;
                }

            }

        }

        DataRow[] rows2 = dt.Select("[HandOver] is not null");
        foreach (DataRow dr in rows2)
        {
            for (int i = 0; i < chkRank2.Items.Count; i++)
            {
                if (UDFLib.ConvertToInteger(dr["Rank"].ToString()) == UDFLib.ConvertToInteger(chkRank2.Items[i].Value))
                {
                    chkRank2.Items[i].Selected = true;
                }

            }

        }
    }
    #endregion


    #region Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Save MasterReview 1 if checked else null
        // Save Handover 1 if checked else null

        for (int i = 0; i < chkRank1.Items.Count; i++)
        {
            int? MR = 0;
            int? HOchk = 0;
            if (chkRank1.Items[i].Selected)
            {
                MR = 1;
            }
            else { MR = null; }
            if (chkRank2.Items[i].Selected)
            {
                HOchk = 1;
            }
            else { HOchk = null; }
            obj.INS_Rank_Configuration(UDFLib.ConvertToInteger(chkRank1.Items[i].Value), MR, HOchk, GetSessionUserID());

        }
        string js = "Rank configuration has been updated successfully.";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('" + js + "');", true);
    }
    #endregion
   

   

}