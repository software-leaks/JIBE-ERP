using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;


public partial class Crew_CrewMatrix : System.Web.UI.Page
{
    
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        if (!IsPostBack)
        {
            lblDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy"); 
            Load_VesselList();
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
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
    public void Load_VesselList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel.DataSource = BLL_Crew_CrewList.Get_VesselForCrewMatrix(UserCompanyID);

        ddlVessel.DataTextField = "Vessel_Name";
        ddlVessel.DataValueField = "Vessel_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        DataTable dt = new DataTable();
        DataTable ds = new DataTable();
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        if (VesselID > 0)
        {
            int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
            ds1 = BLL_Crew_CrewList.Get_VesselTypeForCrewMatrix(VesselID);
            dt = ds1.Tables[0];
            hdnVesselID.Value = Convert.ToString(VesselID);
            hdnVesselName.Value = ddlVessel.SelectedItem.Text;
            hdnVesselType.Value =Convert.ToString(dt.Rows[0]["Vessel_type"]);
            lblTankerType.Text = Convert.ToString(dt.Rows[0]["VesselTypes"]) == "" ? "N/A" : Convert.ToString(dt.Rows[0]["VesselTypes"]);
            
            ds = BLL_Crew_CrewList.Get_CrewMatrix_Report(VesselID, UDFLib.ConvertIntegerToNull(hdnVesselType.Value), UserCompanyID);
            GridViewHelper helper = new GridViewHelper(this.GridView1);
            helper.RegisterGroup("Dept", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            GridView1.DataSource = ds;
            GridView1.DataBind();
  
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            
            lblTankerType.Text = "";
        }
    }
   
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Dept")
        {
            row.BackColor = System.Drawing.Color.LightGray;

            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;

        }

    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            int VesselID = int.Parse(hdnVesselID.Value);
            int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

            DataTable dtexportdata = BLL_Crew_CrewList.Get_CrewMatrix_Report(VesselID, UDFLib.ConvertIntegerToNull(hdnVesselType.Value), UserCompanyID);
            if (dtexportdata.Rows.Count > 0)
            {
                string[] HeaderCaptions = new string[] { "Department", "Rank", "Nationality", "Certificate Of Competency", " Issuing Country", "Adminstration Acceptance", "Tanker Certification", "STCW V Para For Current Cargo", "Radio Qualification", "Year With Operator", "Year in Rank", "Year in this type of Tanker", "Year on All types of Tanker", "Months on vessel this Tour of Duty", "English Proficiency" };
                string[] DataColumnsName = new string[] { "Dept", "Rank_Name", "Nationality", "Certificate_Of_Competency", "Country_Name", "Adminstration_Acceptance", "Tanker_Certification", "STCWVPara", "Radio_Qualification", "YearsOfOperator", "YearsOfRank", "YearsOfTanker", "YearsOfAllTanker", "Months", "English_Proficiency" };
                string vesselType = lblTankerType.Text == "N/A" ? "" : lblTankerType.Text + " - ";
                string FileHeaderName = "Crew Matrix: " + hdnVesselName.Value + " - " + vesselType + lblDate.Text;
                string FileName = "CrewMatrix-" + hdnVesselName.Value;
                GridViewExportUtil.ShowExcel(dtexportdata, HeaderCaptions, DataColumnsName, FileName, FileHeaderName);
            }
        }
        else
        {
            string CrewMatrix;
            if ( ddlVessel.SelectedIndex == 0 )
                CrewMatrix = String.Format("alert('Select a vessel to export crew matrix.');");
            else
                CrewMatrix = String.Format("alert('No data found to export to excel.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CrewMatrix", CrewMatrix, true);
        }
    }
}