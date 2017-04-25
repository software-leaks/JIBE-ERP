using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;

public partial class Fleet : System.Web.UI.Page
{
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Company objBLLComp = new BLL_Infra_Company();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    UserAccess objUA = new UserAccess();
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            FillDDL();
            FillVesselManagerDLL();
            BindFleet();
        }
    }


    public void BindFleet()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
      
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchFleet(txtfilter.Text != "" ? txtfilter.Text : null,UDFLib.ConvertIntegerToNull(ddlFleetVslMgrFilter.SelectedValue),null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvFleet.DataSource = dt;
            gvFleet.DataBind();
        }
        else
        {
            gvFleet.DataSource = dt;
            gvFleet.DataBind();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSaveFleet.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void FillDDL()
    {
        try
        {

            DataTable dtComp = objCrew.Get_VesselManagerList(0);

            ddlFleet_Vessel_Manager.DataSource = dtComp;
            ddlFleet_Vessel_Manager.DataTextField = "COMPANY_NAME";
            ddlFleet_Vessel_Manager.DataValueField = "ID";
            ddlFleet_Vessel_Manager.DataBind();
            ddlFleet_Vessel_Manager.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
        }
    }

    public void FillVesselManagerDLL()
    {

        /* Company Type  5 = VESSEL MANAGER  */

        DataTable dtComptype = objBLLComp.Get_CompanyListByType(5, UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));

        ddlFleet_Vessel_Manager.DataSource = dtComptype;
        ddlFleet_Vessel_Manager.DataTextField = "COMPANY_NAME";
        ddlFleet_Vessel_Manager.DataValueField = "ID";
        ddlFleet_Vessel_Manager.DataBind();
        ddlFleet_Vessel_Manager.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlFleetVslMgrFilter.DataSource = dtComptype;
        ddlFleetVslMgrFilter.DataTextField = "COMPANY_NAME";
        ddlFleetVslMgrFilter.DataValueField = "ID";
        ddlFleetVslMgrFilter.DataBind();
        ddlFleetVslMgrFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
    
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtFleetName");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Fleet";

        ClearField();

        string AddFleetmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddFleetmodal", AddFleetmodal, true);
    }

    public void ClearField()
    {
        txtFleetName.Text = "";
        ddlFleet_Vessel_Manager.SelectedValue = "0";
        txtSuptdEmail.Text = "";
        txtTechTeamEmail.Text = "";
    }

    protected void btnSaveFleet_Click(object sender, EventArgs e)
    {
        if (HiddenFlag.Value == "Add")
        {
            int Res = objBLL.INSERT_New_Fleet(txtFleetName.Text, UDFLib.ConvertToInteger(ddlFleet_Vessel_Manager.SelectedValue),txtSuptdEmail.Text,txtTechTeamEmail.Text
                , UDFLib.ConvertToInteger(Session["USERID"].ToString()));
        }
        else
        {
            int Res = objBLL.Update_Fleet(Convert.ToInt32(txtFleetID.Text), txtFleetName.Text, UDFLib.ConvertToInteger(ddlFleet_Vessel_Manager.SelectedValue),txtSuptdEmail.Text,txtTechTeamEmail.Text
                , UDFLib.ConvertToInteger(Session["USERID"].ToString()));
        }

        BindFleet();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Fleet";

        DataTable dt = new DataTable();
        dt = objBLL.GetFleetList_ByID(Convert.ToInt32(e.CommandArgument.ToString()),null);
      
        txtFleetID.Text = dt.DefaultView[0]["fleetcode"].ToString();
        txtFleetName.Text = dt.DefaultView[0]["fleetname"].ToString();
        ddlFleet_Vessel_Manager.SelectedValue = dt.DefaultView[0]["Vessel_Manager"].ToString() != "" ? dt.DefaultView[0]["Vessel_Manager"].ToString() : "0";
        txtSuptdEmail.Text = dt.DefaultView[0]["Super_MailID"].ToString();
        txtTechTeamEmail.Text = dt.DefaultView[0]["TechTeam_MailID"].ToString();




        string InfoDiv = "Get_Record_Information_Details('LIB_FLEETS','FleetCode=" + txtFleetID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string Fleetmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Fleetmodal", Fleetmodal, true);
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLL.Delete_Fleet(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));

        BindFleet();


    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        ddlFleetVslMgrFilter.SelectedValue = "0";

        BindFleet();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindFleet();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchFleet(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlFleetVslMgrFilter.SelectedValue), null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Name", "Suptd. Email" ,"Tech Email","Vessel Manager" };
        string[] DataColumnsName = { "NAME", "SUPER_MAILID", "TECHTEAM_MAILID", "VesselManager" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Fleet", "Fleet", "");

    }

    protected void gvFleet_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvFleet_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindFleet();
    }

}