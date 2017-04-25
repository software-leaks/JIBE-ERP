using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.IO;
using AjaxControlToolkit4;
using SMS.Business.Crew;


public partial class Account_Portage_Bill_CrewSeniority : System.Web.UI.Page
{

    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    decimal TotalAmount = 0;
    int? CrewID = null;



    int? AllotmentID = null;


    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");

        
            if (!IsPostBack)
            {
                ucCustomPager.PageSize = 20;

                Load_RankList();
                Load_FleetList();
                Load_VesselList();
                Load_Years();
                


                if (Request.QueryString["arg"] != null)
                {
                    string arg = Request.QueryString["arg"].ToString();
                    string[] arr;

                    arr = arg.Split('~');

                    string Vessel = arr[0].ToString();
                    //string Month = DateTime.Parse(arr[1]).Month.ToString();
                    //string Year = DateTime.Parse(arr[1]).Year.ToString();
                    try
                    {
                        ddlVessel.Items.FindByValue(Vessel).Selected = true;
                        //ddlYear.Items.FindByValue(Year).Selected = true;
                        //ddlMonth.Items.FindByValue(Month).Selected = true;
                    }
                    catch { }
                }
                else
                {
                    //string Month = DateTime.Today.Month.ToString();
                    //string Year = DateTime.Today.Year.ToString();
                    //try
                    //{
                    //    ddlYear.Items.FindByValue(Year).Selected = true;
                    //    ddlMonth.Items.FindByValue(Month).Selected = true;
                    //}
                    //catch { }
                }
                Load_SeniorityRecords();
            }



        

    }


    protected DataTable createDtFlagAttach()
    {
        DataTable dtattach = new DataTable();
        dtattach.Columns.Add("PKID");
        dtattach.Columns.Add("Flag_Attach");
        dtattach.Columns.Add("Flag_Attach_Name");
        dtattach.PrimaryKey = new DataColumn[] { dtattach.Columns["PKID"] };
        return dtattach;
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {
            gvSeniorityRecords.Columns[gvSeniorityRecords.Columns.Count - 3].Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            gvSeniorityRecords.Columns[gvSeniorityRecords.Columns.Count - 2].Visible = false;

        }


    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;
    }
    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        //ddlVessel.SelectedIndex = 0;
    }
    protected void Load_Years()
    {
        int Y = DateTime.Today.Year;

        for (int i = 0; i < 10; i++)
        {
            ddlYear.Items.Add(new ListItem((Y - i).ToString(), (Y - i).ToString()));
        }
        ddlYear.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_SeniorityRecords()
    {

        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_PortageBill.Get_SeniorityRecords(int.Parse(ddlFleet.SelectedValue)
                                                         , int.Parse(ddlVessel.SelectedValue)
                                                         , int.Parse(ddlRank.SelectedValue)
                                                         , ddlMonth.SelectedValue
                                                         , ddlYear.SelectedValue
                                                         , txtSearch.Text
                                                         , CrewID
                                                         , PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection
                                                         );
        gvSeniorityRecords.DataSource = dt;
        gvSeniorityRecords.DataBind();


        if (ucCustomPager.isCountRecord == 1)
        {
            ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager.BuildPager();
        }


    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_SeniorityRecords();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        Load_SeniorityRecords();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }
    
    protected void gvSeniorityRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void gvSeniorityRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSeniorityRecords.PageIndex = e.NewPageIndex;
        Load_SeniorityRecords();

    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }

    protected void gvSeniorityRecords_RowEditing(object sender, GridViewEditEventArgs e)
    {

        gvSeniorityRecords.EditIndex = e.NewEditIndex;
        Load_SeniorityRecords();

    }
    protected void gvSeniorityRecords_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int VoyageID = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.RowIndex].Value.ToString());
               
        int SeniorityYear = UDFLib.ConvertToInteger(e.NewValues["SeniorityYear"]);

        BLL_PortageBill.Update_CrewSeniorityYear(VoyageID, SeniorityYear, Convert.ToInt32(Session["userid"]));

        gvSeniorityRecords.EditIndex = -1;
        Load_SeniorityRecords();
    }
    protected void gvSeniorityRecords_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSeniorityRecords.EditIndex = -1;
        Load_SeniorityRecords();
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        try
        {
            string[] strArg = ((ImageButton)s).CommandArgument.Split(',');

            BLL_PB_PortageBill.Delete_Allotment(Int32.Parse(strArg[2]), Convert.ToInt32(strArg[0]), Convert.ToDateTime(strArg[1]), GetSessionUserID());
            Load_SeniorityRecords();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
