using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewList_Print : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string vcode = "";
            int VesselID = 0;


            VesselID = UDFLib.ConvertToInteger(Request.QueryString["vid"]);
            if (VesselID == 0)
            {
                if (Request.QueryString["vcode"] != null)
                    vcode = Request.QueryString["vcode"].ToString();
                VesselID = objVessel.Get_VesselID_ByCode(vcode);
            }
            else
            {
                vcode = objVessel.Get_VesselCode_ByID(VesselID);
            }

            if (VesselID != 0)
            {
                FillGridViewAfterSearch(VesselID);
                ddlVessel.Visible = false;
                lblVesselName.Text = vcode.ToUpper();
            }
            else
            {
                //Load_VesselList(2);
                lblVesselName.Visible = true;
            }

        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    public void FillGridViewAfterSearch(int VesselID)
    {
        DataTable dtFilters = new DataTable();
        dtFilters.Columns.Add("VesselManager", typeof(int));
        dtFilters.Columns.Add("Fleet", typeof(int));
        dtFilters.Columns.Add("Vessel", typeof(int));
        dtFilters.Columns.Add("RankID", typeof(int));
        dtFilters.Columns.Add("Nationality", typeof(int));
        dtFilters.Columns.Add("Status", typeof(int));
        dtFilters.Columns.Add("CalculatedStatus", typeof(int));
        dtFilters.Columns.Add("ManningOfficeID", typeof(int));
        dtFilters.Columns.Add("EOCDueIn", typeof(int));
        dtFilters.Columns.Add("JoiningDateFrom", typeof(String));
        dtFilters.Columns.Add("JoiningDateTo", typeof(String));
        dtFilters.Columns.Add("SearchText", typeof(String));

        DateTime dtFrom = DateTime.Parse("1900/01/01");
        DateTime dtTo = DateTime.Parse("2900/01/01");

        int MainStatusId = 0;
        DataTable dtMainStatus = objCrewAdmin.Get_CrewMainStatus();
        dtMainStatus.DefaultView.RowFilter = "Value='Onboard'";
        if (dtMainStatus.DefaultView.Count > 0)
            MainStatusId = int.Parse(dtMainStatus.DefaultView[0]["Id"].ToString());

        dtFilters.Rows.Add(0, 0, VesselID, 0, 0, MainStatusId, 0, 0, 0, UDFLib.ConvertUserDateFormat(Convert.ToString(dtFrom.ToString())), UDFLib.ConvertUserDateFormat(Convert.ToString(dtTo.ToString())), "");

        int PAGE_SIZE = 100;
        int PAGE_INDEX = 1;
        int SelectRecordCount = 0;

        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Index(dtFilters, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    public void Load_VesselList(int Fleet_ID)
    {

        ddlVessel.DataSource = objVessel.GetVesselsByFleetID(Fleet_ID);
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
        ddlVessel.SelectedIndex = 0;
    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        FillGridViewAfterSearch(VesselID);
    }

    

}