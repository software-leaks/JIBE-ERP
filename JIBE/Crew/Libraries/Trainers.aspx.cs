using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;

using SMS.Business.Crew;


public partial class Trainers : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindTrainers();
        }

    }

    public void BindTrainers()
    {

        DataTable dt = BLL_Crew_Training.Get_Crew_Trainers(GetSessionUserID());
        gvTrainers.DataSource = dt;
        gvTrainers.DataBind();

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
            ImgAdd.Visible = false;

        if (objUA.Edit == 0)
        {
            ImgAdd.Visible = false;
            uaEditFlag = false;
        }
        else
            uaEditFlag = false;

        if (objUA.Delete == 0) 
            uaDeleteFlage = false;
        else
            uaDeleteFlage = true;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lstUserList.SelectedValue) > 0)
        {
            int retval = BLL_Crew_Training.INSERT_Crew_Trainer(Convert.ToInt32(lstUserList.SelectedValue), Convert.ToInt32(Session["USERID"]));
            BindTrainers();
        }
        else
        {
            string js = "alert('Please select user name from the list')";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js1", js, true);

        }
    }
        
    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = BLL_Crew_Training.DELETE_Crew_Trainer(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindTrainers();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindTrainers();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindTrainers();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        
        DataTable dt = BLL_Crew_Training.Get_Crew_Trainers(GetSessionUserID());

        string[] HeaderCaptions = { "Trainer Name"};
        string[] DataColumnsName = { "TrainerName"};

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CrewTrainers", "Crew Trainers", "");

    }   
        
    protected void lstCompany_DataBound(object sender, EventArgs e)
    {
        if (lstCompany.Items.Count > 0)
        {
            try
            {
                if (Session["USERCOMPANYID"] != null)
                    lstCompany.Text = Session["USERCOMPANYID"].ToString();
            }
            catch
            { }
        }
    }
}