using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_Libraries_CompanySeniorityReward : System.Web.UI.Page
{
    BLL_Crew_Seniority objBLLCrewSeniority = new BLL_Crew_Seniority();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_SeniorityList();
        }
    }
    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            dvPageContent.Visible = false;
        }
        if (objUA.Add == 0)
        {
            lnkAddNewSeniorityYear.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            gvSeniorityYear.Columns[gvSeniorityYear.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            gvSeniorityYear.Columns[gvSeniorityYear.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void Load_SeniorityList()
    {
        DataTable dt = objBLLCrewSeniority.Get_SeniorityYearRewardList();
        gvSeniorityYear.DataSource = dt;
        gvSeniorityYear.DataBind();
    }
    protected void gvSeniorityYear_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(gvSeniorityYear.DataKeys[e.RowIndex].Value.ToString());

        objBLLCrewSeniority.DELETE_Seniority(ID, GetSessionUserID());
        Load_SeniorityList();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
         lblMsg.Text = "";
        lblMsg.Visible = false;
        DataTable dt = new DataTable();
        string js = "";
       int retVal = objBLLCrewSeniority.Insert_Seniority(int.Parse(txtSeniorityYear.Text.Trim()), GetSessionUserID());
        if ( retVal == 1 )
        {
            js = "Seniority Year Updated";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
            txtSeniorityYear.Text = "";
            Load_SeniorityList();

            string msgdivResponseShow = string.Format("hideModal('dvAddNewSeniorityYear');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        }
        else
        {
            js ="Seniority Year already exsist for given year" ;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
        }
    }
    protected void AddNewSeniorityYear(object sender, EventArgs e)
    {
        txtSeniorityYear.Text = "";

        string msgdivResponseShow = string.Format("showModal('dvAddNewSeniorityYear',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        UpdatePanel1.Update();
    }
}