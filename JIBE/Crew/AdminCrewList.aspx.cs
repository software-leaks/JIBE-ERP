using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.IO;

public partial class Crew_AdminCrewList : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        if (!IsPostBack)
        {
            ucCustomPager_CrewList.PageSize = 30;
            Load_ManningAgentList(ddlManningOfficeList);
            txtFreeTextSearch.Text = "";
            FillGridView();
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Delete == 0)
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
   
   public void FillGridView()
   {
       string strFreeTextSearch = txtFreeTextSearch.Text;
       int manningOfficeId = int.Parse(ddlManningOfficeList.SelectedValue.ToString());
       int PAGE_SIZE = ucCustomPager_CrewList.PageSize;
       int PAGE_INDEX = ucCustomPager_CrewList.CurrentPageIndex;

       int SelectRecordCount = ucCustomPager_CrewList.isCountRecord;
       DataTable dt = BLL_Crew_CrewList.Get_AdminCrewlist(strFreeTextSearch,manningOfficeId, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);


       GridView1.DataSource = dt;
       GridView1.DataBind();

       if (ucCustomPager_CrewList.isCountRecord == 1)
       {
           ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
           ucCustomPager_CrewList.BuildPager();
       }
    }
   protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
   {
       int id = e.NewEditIndex;
       Label lblId = ((Label)GridView1.Rows[id].FindControl("lblId"));
       ViewState["CrewID"] = lblId.Text;
       Load_ManningAgentList(ddlManningOffice);
       lblMsg.Visible = false;
       txtCrewRemarks.Text = "";
       string msgdivResponseShow = string.Format("showModal('divManningOffice',false);");
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

       UpdatePnl.Update();
   }
   public void Load_ManningAgentList(DropDownList ddl)
   {
       int UserCompanyID = 0;
       if (getSessionString("USERCOMPANYID") != "")
       {
           UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
       }
       ddl.DataSource = objCrew.Get_ManningAgentList(UserCompanyID);
       ddl.DataTextField = "COMPANY_NAME";
       ddl.DataValueField = "ID";
       ddl.DataBind();

       ddl.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
       ddl.SelectedIndex = 0;
   }
   protected void btnSave_Click(object sender, EventArgs e)
   {
       if (int.Parse(ddlManningOffice.SelectedValue.ToString()) > 0)
       {
           lblMsg.Visible = false;

           int ManningOfficeId = int.Parse(ddlManningOffice.SelectedValue.ToString());
           int CrewId = int.Parse(ViewState["CrewID"].ToString());

           if (!txtCrewRemarks.Text.Trim().Equals(""))
           {
               objCrew.INS_CrewRemarks(CrewId, txtCrewRemarks.Text, null, GetSessionUserID(), 0);
           }
           objCrew.Update_CrewManningOffice(CrewId, ManningOfficeId);

           string js = "alert('Manning Office Updated Successfully');";
           ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);

           string msgDivResponseHide = string.Format("hideModal('divManningOffice');");
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDivResponseHide", msgDivResponseHide, true);

           FillGridView();
       }
       else
       {
           lblMsg.Text = "Please select Manning Office";
           lblMsg.Visible = true;
           string js = String.Format("showModal('divManningOffice',false);");
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);

           
       }
   }

   protected void btnSearch_Click(object sender, EventArgs e)
   {
       ucCustomPager_CrewList.isCountRecord = 1;
       FillGridView();
   }
   protected void btnClearFilter_Click(object sender, EventArgs e)
   {
       ddlManningOfficeList.SelectedIndex = 0;
       txtFreeTextSearch.Text = "";
       ucCustomPager_CrewList.isCountRecord = 1;
       FillGridView();
    }
    
}