using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using SMS.Business.TMSA;

public partial class KPI_Crew_Retantion : System.Web.UI.Page
{

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private int Category_Id = 0;
    public UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            if (Request.QueryString[0] != null)
            {
                BindRetentionDetail();

  
            }
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

        if (objUA.Add == 0)
        {
            //btnsave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    /// <summary>
    /// Method to  get the category wise retention data
    /// Inputs of RankIds and Years as querystring 
    /// </summary>

    protected void BindRetentionDetail()
    {
        try
        {
            BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
            Category_Id = Convert.ToInt32(Request.QueryString[0]);
            string RankIDs = Request.QueryString[1];
            string Year = Request.QueryString[2];
            lblformula.Text = "Formula : 100 - (PI 41- PI 16)/PI 06*100";
            DataTable dtRetention = objKPI.Search_CrewRetention(RankIDs, Year, Category_Id).Tables[0];

            if (Request.QueryString["Category"] != null)
                ltTitle.Text = "Crew Retention Details : " + Request.QueryString["Category"];
            if (dtRetention.Rows.Count > 0)
            {
                gvCategory.DataSource = dtRetention;
                gvCategory.DataBind();
               
            }


            
        }
        catch (Exception ex)
        {
            string ERR = ex.ToString();
        }

    }




    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


}
