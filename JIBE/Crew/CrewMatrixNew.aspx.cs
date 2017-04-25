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
using System.Text;
using System.Collections;


public partial class Crew_CrewMatrix : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    UserAccess objUA = new UserAccess();
    DataTable dtDeckOfficers;
    public int strFutureDateRes = 0;
    public string Host = "", SimulateIcon = "", AssignIcon = "";
    string TankerType = "";
    public string EventCrew = "Planned crew", SimulatedCrew = "Simulated crew", DifferentRank = "Crew Rank is different than the simulated rank!";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (UDFLib.GetDateFormat() != "")
                hdnDateFormat.Value = CalendarExtender5.Format = UDFLib.GetDateFormat();
            else
                CalendarExtender5.Format = "dd/MM/yyyy";

            Host = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().Trim().ToLower().IndexOf("/crew/crewmatrixnew.aspx")) + "/";
            SimulateIcon = Host + "Images/assign.gif";
            AssignIcon = Host + "Images/CrewChangetransparent.png";



            if (Request.QueryString.Count != 0)
            {
                if (GetSessionUserID() == 0)
                {
                    Response.Clear();
                    Response.Write("LOGOUT");
                    Response.End();
                }
            }

            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
            else
                UserAccessValidation();

            if (!IsPostBack)
            {
                try
                {
                    if (UDFLib.GetDateFormat() != "")
                        hdnTodayDate.Value = lblDate.Text = DateTime.Now.Date.ToString(UDFLib.GetDateFormat());
                    else
                        hdnTodayDate.Value = lblDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                    Load_VesselList();

                    BLL_Crew_Admin objBLL_Crew_Admin = new BLL_Crew_Admin();
                    int ref1 = 0, ref2 = 0;

                    DataTable dt = objBLL_Crew_Admin.CRUD_OilMajors("", "R", 0, 0, "", null, null, 1, 10000, "", ref ref1, ref ref2);
                    if (dt.Rows.Count > 0)
                    {
                        drpOilMajors.DataSource = dt;
                        drpOilMajors.DataValueField = "ID";
                        drpOilMajors.DataTextField = "Oil_Major_Name";
                        drpOilMajors.DataBind();
                    }
                    drpOilMajors.Items.Insert(0, new ListItem() { Text = "-SELECT-", Value = "0" });

                    drpCrewEvent.Items.Insert(0, new ListItem() { Text = "-SELECT-", Value = "0" });

                    ///Disable previous dates
                    CalendarExtender5.StartDate = DateTime.Now;

                    ///Disable future dates 
                    if (Convert.ToInt32(dt.Rows[0]["DateRestriction"]) != 0)
                    {
                        CalendarExtender5.EndDate = DateTime.Now.AddDays(Convert.ToInt32(dt.Rows[0]["DateRestriction"]));
                        strFutureDateRes = Convert.ToInt32(dt.Rows[0]["DateRestriction"]);
                    }
                }
                catch (Exception ex)
                {
                    UDFLib.WriteExceptionLog(ex);
                }
            }

            ///Simulate Crew
            if (Request.QueryString.Count != 0)
            {
                int VesselID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["VesselId"]));
                int OilMajorID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["OilMajorID"]));
                int CrewEventID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["CrewEventID"]));
                string Date = Convert.ToString(Request.QueryString["flrDate"]);
                int JoiningRank = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["JoiningRank"]));
                int OldCrewId = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["OldCrewId"]));
                int NewCrewId = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["SimulatedCrewId"]));
                int ActualCrewId = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["ActualCrewId"]));
                int ActualRank = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["ActualRank"]));
                int MinAggCrewId = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["MinAggCrewId"]));
                TankerType = Convert.ToString(Request.QueryString["TankerType"]);
                int MinAggCrewChkd = 0;
                if (Request.QueryString["MinAggCrewChkd"] != null)
                {
                    if (Convert.ToBoolean(Request.QueryString["MinAggCrewChkd"]))
                        MinAggCrewChkd = 1;
                    else
                        MinAggCrewChkd = 0;
                }


                if (OldCrewId == 0 && JoiningRank == 0 && NewCrewId == 0 && MinAggCrewId == 0)
                {
                    Session["dt_CM_NewCrew"] = null;
                    Session["dt_dtMinAggCrew"] = null;
                }
                /// Bind Crew matrix grid
                Response.Clear();
                Response.Write(BindCrewMetrixGrid(VesselID, CrewEventID, OilMajorID, Date, JoiningRank, OldCrewId, NewCrewId, ActualCrewId, ActualRank, MinAggCrewId, MinAggCrewChkd, TankerType).ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
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

    /// <summary>
    /// Bind Vessel List
    /// </summary>
    public void Load_VesselList()
    {
        try
        {
            int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
            DataTable dt = BLL_Crew_CrewList.Get_VesselForCrewMatrix(UserCompanyID);
            ddlVessel.DataSource = dt;
            ddlVessel.DataTextField = "Vessel_Name";
            ddlVessel.DataValueField = "Vessel_ID";
            ddlVessel.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
                ddlVessel.Items[i].Attributes.Add("VesselType", Convert.ToString(dt.Rows[i]["VesselTypes"]));

            ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
            ddlVessel.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Export crew matrix to excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselID = int.Parse(hdnVesselID.Value);
            int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

            DataTable dtexportdata = (DataTable)Session["dtExportExcel"];
            if (dtexportdata.Rows.Count > 0)
            {
                string[] HeaderCaptions = new string[] { "Rank", "Name", "Nationality", "Certificate Of Competency", " Issuing Country", "Administration Acceptance", "Tanker Certification", "STCW V Para", "Radio Qualification", "Years with Operator", "Years in Rank", "Years in this types of Tanker", "Years on All types of Tanker", "Months on vessel this Tour of Duty", "Years Watch", "English Proficiency" };
                string[] DataColumnsName = new string[] { "Rank", "Name", "Nationality", "Cert.Comp.", "Issuing Country", "Admin. Accept", "Tanker Cert.", "STCW V Para.", "Radio Qual.", "Years with Operator", "Years on Rank", "Years on Tanker Type", "Years on All Types", "M O Tour", "Years watch", "English Prof." };
                string vesselType = lblTankerType.Text == "N/A" ? "" : lblTankerType.Text + " - ";

                string Date = "";
                Date = lblDate.Text == "" ? drpCrewEvent.SelectedItem.Text : Convert.ToString(lblDate.Text);

                GridViewExportUtil.ShowExcel(dtexportdata, HeaderCaptions, DataColumnsName, "CrewMatrix-" + hdnVesselName.Value, "Crew Matrix: " + hdnVesselName.Value + " - " + vesselType + Date);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private DataTable BindDropDownWithDateFormat(string FieldName, string NewField, DataTable dt)
    {
        dt.Columns.Add(NewField, typeof(string));
        for (int i = 0; i < dt.Rows.Count; i++)
            dt.Rows[i][NewField] = UserDateFormat(Convert.ToString(dt.Rows[i][FieldName]));
        return dt;
    }

    private string UserDateFormat(string Date)
    {
        if (Date != "")
        {
            DateTime dt = Convert.ToDateTime(Date);
            return String.Format("{0:" + Convert.ToString(Session["User_DateFormat"]) + "}", dt);
        }
        else
            return "";
    }

    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVessel.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                ds = BLL_Crew_CrewList.Get_VesselTypeForCrewMatrix(Convert.ToInt32(ddlVessel.SelectedValue));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hdnVesselID.Value = ddlVessel.SelectedValue;
                        hdnVesselName.Value = ddlVessel.SelectedItem.Text;
                        lblTankerType.Text = Convert.ToString(ds.Tables[0].Rows[0]["VesselTypes"]) == "" ? "N/A" : Convert.ToString(ds.Tables[0].Rows[0]["VesselTypes"]);

                        hdnTankerType.Value = "";
                        if (Convert.ToString(ds.Tables[0].Rows[0]["TankerType"]) == "oil" || Convert.ToString(ds.Tables[0].Rows[0]["TankerType"]) == "chemical" || Convert.ToString(ds.Tables[0].Rows[0]["TankerType"]) == "gas")
                            hdnTankerType.Value = Convert.ToString(ds.Tables[0].Rows[0]["TankerType"]);
                    }

                    if (Convert.ToString(Session["User_DateFormat"]) != "")
                    {
                        foreach (DataRow item in ds.Tables[1].Rows)
                        {
                            item["Event_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Event_Date"]));
                        }
                    }


                    ///Bind Events for selected vessel 
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        drpCrewEvent.DataSource = ds.Tables[1];
                        drpCrewEvent.DataTextField = "Event_Date";
                        drpCrewEvent.DataValueField = "PKID";
                        drpCrewEvent.DataBind();
                        drpCrewEvent.Enabled = true;
                    }
                    else
                    {
                        drpCrewEvent.Enabled = false;
                        drpCrewEvent.DataSource = null;
                        drpCrewEvent.DataBind();
                    }
                    drpCrewEvent.Items.Insert(0, new ListItem() { Text = "-SELECT-", Value = "0" });
                }
            }
            else
            {
                hdnVesselID.Value = "0";
                hdnVesselName.Value = "";
                lblTankerType.Text = "";
                hdnTankerType.Value = "";
                drpCrewEvent.Enabled = false;
                drpCrewEvent.ClearSelection();
                drpCrewEvent.DataSource = null;
                drpCrewEvent.DataBind();
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }


    /// <summary>
    /// To Bind Crew matrix grid
    /// </summary>
    /// <param name="VesselId"></param>
    /// <param name="CrewEventId"></param>
    /// <param name="oilMajorID"></param>
    /// <param name="lblDate"></param>
    /// <param name="rankId"></param>
    /// <param name="oldCrewID"></param>
    /// <param name="newCrewID"></param>
    /// <param name="ActualCrewId"></param>
    /// <param name="ActualRankID"></param>
    /// <param name="MinAggCrewId"></param>
    /// <param name="MinAggCrewChkd"></param>
    /// <param name="TankerType"></param>
    /// <returns></returns>
    private StringBuilder BindCrewMetrixGrid(int VesselId, int CrewEventId, int oilMajorID, string lblDate, int rankId, int oldCrewID, int newCrewID, int ActualCrewId, int ActualRankID, int MinAggCrewId, int MinAggCrewChkd, string TankerType)
    {
        StringBuilder strGrid = new StringBuilder();
        try
        {
            DataSet ds = new DataSet();
            DataSet dsCrewDetails;
            DataTable dtMinAggCrew = new DataTable();
            string DateFilter = "";


            #region Datatable for export to excel feature
            DataTable dtExportToExcel = new DataTable();
            dtExportToExcel.Columns.Add("RankID", typeof(int));
            dtExportToExcel.Columns.Add("Rank", typeof(string));
            dtExportToExcel.Columns.Add("Name", typeof(string));
            dtExportToExcel.Columns.Add("Nationality", typeof(string));
            dtExportToExcel.Columns.Add("Cert.Comp.", typeof(string));
            dtExportToExcel.Columns.Add("Issuing Country", typeof(string));
            dtExportToExcel.Columns.Add("Admin. Accept", typeof(string));
            dtExportToExcel.Columns.Add("Tanker Cert.", typeof(string));
            dtExportToExcel.Columns.Add("STCW V Para.", typeof(string));
            dtExportToExcel.Columns.Add("Radio Qual.", typeof(string));
            dtExportToExcel.Columns.Add("Years with Operator", typeof(string));
            dtExportToExcel.Columns.Add("Years on Rank", typeof(string));
            dtExportToExcel.Columns.Add("Years on Tanker Type", typeof(string));
            dtExportToExcel.Columns.Add("Years on All Types", typeof(string));
            dtExportToExcel.Columns.Add("M O Tour", typeof(string));
            dtExportToExcel.Columns.Add("Years watch", typeof(string));
            dtExportToExcel.Columns.Add("English Prof.", typeof(string));
            dtExportToExcel.Columns.Add("Checked", typeof(bool));
            dtExportToExcel.Columns.Add("JoiningDate", typeof(DateTime));
            dtExportToExcel.Columns.Add("SignOnDate", typeof(DateTime));
            dtExportToExcel.Columns.Add("StaffCode", typeof(string));
            #endregion

            if (CrewEventId > 0)
            {
                /// Get crew on selected event
                ds = BLL_Crew_CrewList.CRW_DTL_CM_GetCrewMatrix(VesselId, CrewEventId, oilMajorID, "");
                if (ds != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DateTime Date = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Tables[1].Rows[0]["Event_Date"])), UDFLib.GetDateFormat());
                        DateFilter = Date.Year + "-" + Date.Month + "-" + Date.Day;
                    }
                }
            }
            else
            {
                //Get Crew on selected date
                DateTime Date = UDFLib.ConvertToDate(lblDate, UDFLib.GetDateFormat());
                DateFilter = Date.Year + "-" + Date.Month + "-" + Date.Day;
                ds = BLL_Crew_CrewList.CRW_DTL_CM_GetCrewMatrix(VesselId, CrewEventId, oilMajorID, DateFilter);
            }
            if (ds != null)
            {
                ///Datatable for deck officer
                dtDeckOfficers = ds.Tables[5];

                if (ds.Tables[3].Rows.Count > 0)
                {
                    gridOfficer.Visible = tblGrid.Visible = true;

                    #region Datatable for crew checked for min aggregated

                    if (Session["dt_dtMinAggCrew"] == null)
                    {
                        dtMinAggCrew.Columns.Add("CrewId", typeof(int));
                        dtMinAggCrew.Columns.Add("Checked", typeof(int));
                    }
                    else
                    {
                        dtMinAggCrew = (DataTable)Session["dt_dtMinAggCrew"];
                    }

                    if (MinAggCrewId != 0)
                    {
                        dtMinAggCrew.DefaultView.RowFilter = "";
                        dtMinAggCrew.DefaultView.RowFilter = "CrewId=" + MinAggCrewId;
                        if (dtMinAggCrew.DefaultView.Count > 0)
                        {
                            for (int i = 0; i < dtMinAggCrew.DefaultView.Count; i++)
                                dtMinAggCrew.DefaultView[i]["Checked"] = MinAggCrewChkd;
                        }
                        else
                        {
                            DataRow drNewAggCrew = dtMinAggCrew.NewRow();
                            drNewAggCrew["CrewId"] = MinAggCrewId;
                            drNewAggCrew["Checked"] = MinAggCrewChkd;
                            dtMinAggCrew.Rows.Add(drNewAggCrew);
                        }
                    }
                    #endregion

                    #region Simulate
                    ds.Tables[2].Columns.Add("NewAssign", typeof(bool));
                    ds.Tables[2].Columns.Add("OffSignerID", typeof(int));
                    ds.Tables[2].Columns.Add("ActualCrewID", typeof(int));
                    ds.Tables[2].Columns.Add("ActualRank", typeof(int));

                    //New Assigned Crew
                    DataTable dtNewCrew = new DataTable();
                    if (Session["dt_CM_NewCrew"] == null)
                    {
                        dtNewCrew.Columns.Add("CrewID", typeof(int));
                        dtNewCrew.Columns.Add("NewAssign", typeof(bool));
                        dtNewCrew.Columns.Add("OldCrewID", typeof(int));
                        dtNewCrew.Columns.Add("Joining_Rank", typeof(int));
                        dtNewCrew.Columns.Add("Vessel_ID", typeof(int));
                        dtNewCrew.Columns.Add("ActualCrewID", typeof(int));
                        dtNewCrew.Columns.Add("ActualRank", typeof(string));
                        dtNewCrew.Columns.Add("CurrentStatus", typeof(string));
                    }
                    else
                    {
                        dtNewCrew = (DataTable)Session["dt_CM_NewCrew"];
                    }


                    //check whether crew already exists or not  
                    dtNewCrew.DefaultView.RowFilter = "";
                    dtNewCrew.DefaultView.RowFilter = "ActualCrewID=" + ActualCrewId + " AND Joining_Rank=" + rankId;
                    if (dtNewCrew.DefaultView.Count > 0)//if Yes Then replace existing crew with new record  to session datatable
                    {
                        dtNewCrew.DefaultView[0]["Joining_Rank"] = rankId;
                        dtNewCrew.DefaultView[0]["CrewID"] = newCrewID;
                        dtNewCrew.DefaultView[0]["OldCrewID"] = oldCrewID;
                        dtNewCrew.DefaultView[0]["Vessel_ID"] = VesselId;
                        dtNewCrew.DefaultView[0]["NewAssign"] = true;
                        dtNewCrew.DefaultView[0]["ActualRank"] = ActualRankID;
                        if (rankId != ActualRankID)
                            dtNewCrew.DefaultView[0]["CurrentStatus"] = "DifferentRank";
                        else
                            dtNewCrew.DefaultView[0]["CurrentStatus"] = "Simulated";
                    }
                    else//if No Then add new record to session Datatable
                    {
                        DataRow newRow = dtNewCrew.NewRow();
                        newRow["CrewID"] = newCrewID;
                        newRow["OldCrewID"] = oldCrewID;
                        newRow["Joining_Rank"] = rankId;
                        newRow["Vessel_ID"] = VesselId;
                        newRow["NewAssign"] = true;
                        newRow["ActualCrewID"] = ActualCrewId;
                        newRow["ActualRank"] = ActualRankID;
                        if (rankId != ActualRankID)
                            newRow["CurrentStatus"] = "DifferentRank";
                        else
                            newRow["CurrentStatus"] = "Simulated";
                        dtNewCrew.Rows.Add(newRow);
                    }



                    bool IsExists = false;
                    for (int checkCrew = 0; checkCrew < dtNewCrew.Rows.Count; checkCrew++)
                    {
                        //check in event crew data
                        ds.Tables[2].DefaultView.RowFilter = "";
                        ds.Tables[2].DefaultView.RowFilter = "CrewID=" + Convert.ToInt32(dtNewCrew.Rows[checkCrew]["ActualCrewID"]) + " AND Joining_Rank=" + Convert.ToInt32(dtNewCrew.Rows[checkCrew]["Joining_Rank"]);

                        if (ds.Tables[2].DefaultView.Count > 0)
                        {
                            ds.Tables[2].DefaultView[0]["CrewID"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["CrewID"]);
                            ds.Tables[2].DefaultView.RowFilter = "CrewID=" + Convert.ToInt32(dtNewCrew.Rows[checkCrew]["CrewID"]);
                            ds.Tables[2].DefaultView[0]["NewAssign"] = true;
                            ds.Tables[2].DefaultView[0]["OffSignerID"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["OldCrewID"]);
                            ds.Tables[2].DefaultView[0]["ActualCrewID"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["ActualCrewID"]);
                            ds.Tables[2].DefaultView[0]["CurrentStatus"] = Convert.ToString(dtNewCrew.Rows[checkCrew]["CurrentStatus"]);
                            ds.Tables[2].DefaultView[0]["ActualRank"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["ActualRank"]);
                            ds.Tables[2].DefaultView[0]["Joining_Date"] = Convert.ToDateTime(DateFilter);
                            ds.Tables[2].DefaultView[0]["Sign_On_Date"] = Convert.ToDateTime(DateFilter);
                            if (Convert.ToInt32(dtNewCrew.Rows[checkCrew]["Joining_Rank"]) != UDFLib.ConvertToInteger(Convert.ToString(dtNewCrew.Rows[checkCrew]["ActualRank"])))
                                ds.Tables[2].DefaultView[0]["CurrentStatus"] = "DifferentRank";
                            else
                                ds.Tables[2].DefaultView[0]["CurrentStatus"] = "Simulated";

                            IsExists = true;
                        }

                        if (Convert.ToInt32(dtNewCrew.Rows[checkCrew]["ActualCrewID"]) == 0)
                        {
                            DataRow newCrewRow = ds.Tables[2].NewRow();
                            newCrewRow["CrewID"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["CrewID"]);
                            newCrewRow["Joining_Rank"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["Joining_Rank"]);
                            newCrewRow["Vessel_ID"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["Vessel_ID"]);
                            newCrewRow["OffSignerID"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["OldCrewID"]);
                            newCrewRow["ActualCrewID"] = UDFLib.ConvertToInteger(Convert.ToString(dtNewCrew.Rows[checkCrew]["ActualCrewID"]));
                            newCrewRow["ActualRank"] = Convert.ToInt32(dtNewCrew.Rows[checkCrew]["ActualRank"]);
                            newCrewRow["Joining_Date"] = Convert.ToDateTime(DateFilter);
                            newCrewRow["Sign_On_Date"] = Convert.ToDateTime(DateFilter);
                            newCrewRow["YearsWithOperator"] = 0;
                            newCrewRow["VoyageID"] = 0;

                            if (Convert.ToInt32(dtNewCrew.Rows[checkCrew]["Joining_Rank"]) != UDFLib.ConvertToInteger(Convert.ToString(dtNewCrew.Rows[checkCrew]["ActualRank"])))
                                newCrewRow["CurrentStatus"] = "DifferentRank";
                            else
                                newCrewRow["CurrentStatus"] = "Simulated";

                            newCrewRow["NewAssign"] = true;
                            ds.Tables[2].Rows.Add(newCrewRow);
                        }
                    }

                    if (dtNewCrew.Rows.Count > 0)
                    {
                        Session["dt_CM_NewCrew"] = dtNewCrew;
                    }
                    #endregion

                    DataView view = new DataView(ds.Tables[3]);
                    DataTable distinctValues = view.ToTable(true, "GroupID");

                    if (distinctValues.Rows.Count > 0)
                    {
                        #region Table Header
                        strGrid.Append("<table id=\"tblCrewMatrix\" class=\"GridView-css\" style=\"width: 100%; border-collapse: collapse;\" border=\"1\">");
                        strGrid.Append("<tr class=\"HeaderStyle\">");
                        strGrid.Append("<th align=\"center\" scope=\"col\">Rank</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\" width='100px'>Name</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">Nationality</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">&nbsp;Cert.Comp.&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">&nbsp;Issuing Country&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">&nbsp;Admin. Accept&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">Tanker Cert.</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">&nbsp;STCW V Para.&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">&nbsp;Radio Qual.&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" style=\"color: Blue;\" scope=\"col\">&nbsp;Operator&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" style=\"color: Blue;\" scope=\"col\">&nbsp;Rank&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" style=\"color: Blue;\" scope=\"col\">&nbsp;Tanker Type&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" style=\"color: Blue;\" scope=\"col\">&nbsp;All Types&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" style=\"color: Blue;\" scope=\"col\">&nbsp;M O Tour&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" style=\"color: Blue;\" scope=\"col\">&nbsp;Years watch&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\">&nbsp;English Prof.&nbsp;</th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\" width=\"20px\"></th>");
                        strGrid.Append("<th align=\"center\" scope=\"col\" width=\"80px\">&nbsp;Simulate/Assign&nbsp;</th>");
                        strGrid.Append("</tr>");
                        #endregion

                        int RowID = 0;
                        for (int i = 0; i < distinctValues.Rows.Count; i++)
                        {

                            var OilMajorRanks = from Ranks in ds.Tables[3].AsEnumerable()
                                                where Ranks.Field<int>("GroupID") == Convert.ToInt32(distinctValues.Rows[i]["GroupID"])
                                                select Ranks;

                            decimal dcYearsWithOperator = 0, dcYearsOnRank = 0, dcThisTypeOfTanker = 0, dcAllTypesOfTanker = 0;



                            if (OilMajorRanks != null)
                            {
                                DataTable dtRanks = OilMajorRanks.CopyToDataTable();
                                string Ranks = "";
                                string RanksForMinAggr = "|";

                                for (int k = 0; k < dtRanks.Rows.Count; k++)
                                {
                                    RowID++;
                                    //check on Board User 
                                    var OnVesselCrew = from onBoard in ds.Tables[2].AsEnumerable()
                                                       where onBoard.Field<int>("Joining_Rank") == Convert.ToInt32(dtRanks.Rows[k]["RankID"])
                                                       orderby onBoard.Field<decimal>("YearsWithOperator") descending
                                                       select onBoard;

                                    ///Get all ranks which are in Groups
                                    RanksForMinAggr += Convert.ToInt32(dtRanks.Rows[k]["RankID"]) + "|";

                                    DataTable dtOnVesselCrew = new DataTable();
                                    if (OnVesselCrew.Count() > 0)
                                    {
                                        string[] DistinctColumns = { "CrewID", "VoyageID", "Joining_Rank", "Vessel_ID", "Joining_Type", "YearsWithOperator", "CurrentStatus", "Checked", "OffSignerID", "ActualCrewID", "ActualRank", "NewAssign", "Joining_Date", "Sign_On_Date" };
                                        dtOnVesselCrew = new DataTable();
                                        dtOnVesselCrew = OnVesselCrew.CopyToDataTable().DefaultView.ToTable(true, DistinctColumns);
                                    }


                                    //If no Crew found then show only rank
                                    if (dtOnVesselCrew.Rows.Count == 0)
                                        strGrid.Append("<tr><td style=\"width: 90px;\">" + Convert.ToString(dtRanks.Rows[k]["Ranks"]) + "</td>");

                                    if (k == dtRanks.Rows.Count - 1)
                                        Ranks += Convert.ToString(dtRanks.Rows[k]["Ranks"]);
                                    else
                                        Ranks += Convert.ToString(dtRanks.Rows[k]["Ranks"]) + "+";


                                    if (dtOnVesselCrew.Rows.Count > 0)
                                    {
                                        for (int MultiCrew = 0; MultiCrew < dtOnVesselCrew.Rows.Count; MultiCrew++)
                                        {
                                            DataRow drExportexcel = dtExportToExcel.NewRow();
                                            drExportexcel["RankID"] = Convert.ToInt32(dtRanks.Rows[k]["RankID"]);
                                            drExportexcel["Rank"] = Convert.ToString(dtRanks.Rows[k]["Ranks"]);
                                            drExportexcel["JoiningDate"] = dtOnVesselCrew.Rows[MultiCrew]["Joining_Date"];
                                            drExportexcel["SignOnDate"] = dtOnVesselCrew.Rows[MultiCrew]["Sign_On_Date"];

                                            string FlagTitle = "";
                                            if (Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["CurrentStatus"]) == "Event")
                                                FlagTitle = EventCrew;
                                            else if (Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["CurrentStatus"]) == "Simulated")
                                                FlagTitle = SimulatedCrew;
                                            else if (Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["CurrentStatus"]) == "DifferentRank")
                                                FlagTitle = DifferentRank;


                                            //First Min Aggregated 									
                                            if (MinAggCrewId == 0 && MultiCrew == 0 && dtOnVesselCrew.Rows.Count > 0)
                                            {
                                                DataRow dr = dtMinAggCrew.NewRow();
                                                dr["CrewId"] = Convert.ToInt32(dtOnVesselCrew.Rows[MultiCrew]["CrewID"]);
                                                dr["Checked"] = 1;
                                                dtMinAggCrew.Rows.Add(dr);
                                            }

                                            strGrid.Append("<tr><td style=\"width: 90px;\"><div class='divflag " + dtOnVesselCrew.Rows[MultiCrew]["CurrentStatus"] + "' title='" + FlagTitle + "'></div><span style=\"margin-center\">" + Convert.ToString(dtRanks.Rows[k]["Ranks"]) + "</span></td>");

                                            objBLLCrew = new BLL_Crew_CrewDetails();
                                            dsCrewDetails = new DataSet();
                                            dsCrewDetails = objBLLCrew.CRW_Get_CM_CrewYearsExp(Convert.ToInt32(dtOnVesselCrew.Rows[MultiCrew]["CrewID"]), Convert.ToInt32(dtOnVesselCrew.Rows[MultiCrew]["Vessel_ID"]), DateFilter, Convert.ToInt32(dtRanks.Rows[k]["RankID"]), Convert.ToInt32(dtOnVesselCrew.Rows[MultiCrew]["VoyageID"]));

                                            if (dsCrewDetails != null)
                                            {
                                                #region Crew Name
                                                if (Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Name"]) != "")
                                                    strGrid.Append("<td> <span class='spnStaffName' rel='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Code"]) + "'><a target=\"_blank\" href=\"CrewDetails.aspx?ID=" + Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["CrewID"]) + "\"> " + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Name"]) + "</a></span></td>");
                                                else
                                                    strGrid.Append("<td></td>");
                                                drExportexcel["Name"] = Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Name"]);
                                                drExportexcel["StaffCode"] = Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Code"]);
                                                #endregion

                                                #region Nationality
                                                if (Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Nationality"]) != "")
                                                    strGrid.Append("<td>&nbsp;" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Nationality"]) + "&nbsp;</td>");
                                                else
                                                    strGrid.Append("<td></td>");
                                                drExportexcel["Nationality"] = Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Nationality"]);
                                                #endregion

                                                if (dsCrewDetails.Tables[8].Rows.Count > 0)
                                                {
                                                    #region Certificate_Of_Competency_DOC
                                                    if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Certificate_Of_Competency_DOC"]) != "")
                                                        strGrid.Append("<td>" + Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Certificate_Of_Competency_DOC"]) + "</td>");
                                                    else
                                                        strGrid.Append("<td></td>");
                                                    drExportexcel["Cert.Comp."] = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Certificate_Of_Competency_DOC"]);
                                                    #endregion
                                                    #region Issuing_Country_Name
                                                    if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Issuing_Country_Name"]) != "")
                                                        strGrid.Append("<td>" + Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Issuing_Country_Name"]) + "</td>");
                                                    else
                                                        strGrid.Append("<td></td>");
                                                    drExportexcel["Issuing Country"] = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Issuing_Country_Name"]);
                                                    #endregion
                                                    #region Administration_Acceptance
                                                    if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Administration_Acceptance"]) != "")
                                                        strGrid.Append("<td>" + Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Administration_Acceptance"]) + "</td>");
                                                    else
                                                        strGrid.Append("<td></td>");
                                                    drExportexcel["Admin. Accept"] = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Administration_Acceptance"]);
                                                    #endregion
                                                    #region Tanker_Certification
                                                    string TankerTypes = "";
                                                    try
                                                    {
                                                        if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Tanker_Certification"]) != "")
                                                        {
                                                            string[] arrayTankerCer = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Tanker_Certification"]).Split('|');
                                                            if (arrayTankerCer.Length > 0)
                                                            {
                                                                for (int j = 0; j < arrayTankerCer.Length; j++)
                                                                {
                                                                    var TankerCertPara = from tankerCert in dsCrewDetails.Tables[0].AsEnumerable()
                                                                                         where tankerCert.Field<int>("ID") == Convert.ToInt32(arrayTankerCer[j])
                                                                                         select tankerCert;
                                                                    if (TankerCertPara.Count() > 0)
                                                                    {
                                                                        if (j == arrayTankerCer.Length - 1)
                                                                            TankerTypes += Convert.ToString(TankerCertPara.CopyToDataTable().Rows[0]["PARAMETERS"]);
                                                                        else if (TankerCertPara.CopyToDataTable().Rows.Count > 0)
                                                                            TankerTypes += Convert.ToString(TankerCertPara.CopyToDataTable().Rows[0]["PARAMETERS"]) + ", ";
                                                                    }
                                                                }
                                                                strGrid.Append("<td>" + TankerTypes + "</td>");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            strGrid.Append("<td></td>");
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        strGrid.Append("<td></td>");
                                                    }

                                                    drExportexcel["Tanker Cert."] = TankerTypes;
                                                    #endregion
                                                    #region STCWVPara
                                                    if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["STCWVPara"]) != "")
                                                        strGrid.Append("<td>" + Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["STCWVPara"]) + "</td>");
                                                    else
                                                        strGrid.Append("<td></td>");
                                                    drExportexcel["STCW V Para."] = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["STCWVPara"]);
                                                    #endregion
                                                    #region Radio_Qualification
                                                    if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Radio_Qualification"]) != "")
                                                        strGrid.Append("<td>" + Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Radio_Qualification"]) + "</td>");
                                                    else
                                                        strGrid.Append("<td></td>");
                                                    drExportexcel["Radio Qual."] = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["Radio_Qualification"]);
                                                    #endregion
                                                }
                                                else
                                                    strGrid.Append("<td></td><td></td><td></td><td></td><td></td><td></td>");

                                                #region Min Aggregate calculation
                                                dtMinAggCrew.DefaultView.RowFilter = "";
                                                dtMinAggCrew.DefaultView.RowFilter = "CrewId=" + Convert.ToInt32(dtOnVesselCrew.Rows[MultiCrew]["CrewID"]) + " AND Checked=1";

                                                if (dtMinAggCrew.DefaultView.Count > 0)
                                                {
                                                    dcYearsWithOperator += UDFLib.ConvertToDecimal(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"]);
                                                    dcYearsOnRank += UDFLib.ConvertToDecimal(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"]);
                                                    dcThisTypeOfTanker += UDFLib.ConvertToDecimal(dsCrewDetails.Tables[3].Rows[0]["ThisTypeOfTanker"]);
                                                    dcAllTypesOfTanker += UDFLib.ConvertToDecimal(dsCrewDetails.Tables[4].Rows[0]["AllTypesOfTanker"]);
                                                }
                                                #endregion
                                                #region Years with Operator
                                                if (dsCrewDetails.Tables[1].Rows.Count > 0)
                                                {
                                                    if (Convert.ToDecimal(Convert.ToString(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"])) < 0)
                                                        dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"] = "0.00";

                                                    if (oilMajorID != 0)
                                                    {
                                                        if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Operator"]) > UDFLib.ConvertToDecimal(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"]))
                                                            strGrid.Append("<td style=\"width:55px\"><span class='error' title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Operator"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"]) + "</span></td>");
                                                        else
                                                            strGrid.Append("<td style=\"width:55px\"><span title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Operator"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"]) + "</span></td>");
                                                    }
                                                    else
                                                        strGrid.Append("<td style=\"width:55px\"><span>" + Convert.ToString(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"]) + "</span></td>");
                                                }
                                                else
                                                    strGrid.Append("<td style=\"width:55px\"></td>");
                                                drExportexcel["Years with Operator"] = Convert.ToString(dsCrewDetails.Tables[1].Rows[0]["YearsWithOperator"]);
                                                #endregion
                                                #region Years on Rank
                                                if (dsCrewDetails.Tables[2].Rows.Count > 0)
                                                {
                                                    if (Convert.ToDecimal(Convert.ToString(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"])) < 0)
                                                        dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"] = "0.00";

                                                    if (oilMajorID != 0)
                                                    {
                                                        if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Rank"]) > UDFLib.ConvertToDecimal(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"]))
                                                            strGrid.Append("<td style=\"width:55px\"><span class='error' title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Rank"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"]) + "</span></td>");
                                                        else
                                                            strGrid.Append("<td style=\"width:55px\"><span title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Rank"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"]) + "</span></td>");
                                                    }
                                                    else
                                                        strGrid.Append("<td style=\"width:55px\"><span>" + Convert.ToString(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"]) + "</span></td>");
                                                }
                                                else
                                                    strGrid.Append("<td style=\"width:55px\"></td>");
                                                drExportexcel["Years on Rank"] = Convert.ToString(dsCrewDetails.Tables[2].Rows[0]["YearsOnRank"]);
                                                #endregion
                                                #region This type of tanker
                                                if (TankerType.Trim() == "")
                                                {
                                                    strGrid.Append("<td style=\"width:55px\"></td>");
                                                    drExportexcel["Years on Tanker Type"] = "0";
                                                }
                                                else
                                                {
                                                    if (dsCrewDetails.Tables[3].Rows.Count > 0)
                                                    {
                                                        if (oilMajorID != 0)
                                                        {
                                                            if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Tanker type"]) > UDFLib.ConvertToDecimal(dsCrewDetails.Tables[3].Rows[0]["ThisTypeOfTanker"]))
                                                                strGrid.Append("<td style=\"width:55px\"><span class='error' title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Tanker type"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[3].Rows[0]["ThisTypeOfTanker"]) + "</span></td>");
                                                            else
                                                                strGrid.Append("<td style=\"width:55px\"><span title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["Tanker type"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[3].Rows[0]["ThisTypeOfTanker"]) + "</span></td>");
                                                        }
                                                        else
                                                            strGrid.Append("<td style=\"width:55px\"><span>" + Convert.ToString(dsCrewDetails.Tables[3].Rows[0]["ThisTypeOfTanker"]) + "</span></td>");
                                                    }
                                                    else
                                                        strGrid.Append("<td style=\"width:55px\"></td>");
                                                    drExportexcel["Years on Tanker Type"] = Convert.ToString(dsCrewDetails.Tables[3].Rows[0]["ThisTypeOfTanker"]);
                                                }
                                                #endregion
                                                #region All Types Of Tanker
                                                if (dsCrewDetails.Tables[4].Rows.Count > 0)
                                                {
                                                    if (oilMajorID != 0)
                                                    {
                                                        if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["All types"]) > UDFLib.ConvertToDecimal(dsCrewDetails.Tables[4].Rows[0]["AllTypesOfTanker"]))
                                                            strGrid.Append("<td style=\"width:55px\"><span class='error' title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["All types"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[4].Rows[0]["AllTypesOfTanker"]) + "</span></td>");
                                                        else
                                                            strGrid.Append("<td style=\"width:55px\"><span title='Minimum Value: " + UDFLib.ConvertToDecimal(dtRanks.Rows[k]["All types"]) + "'>" + Convert.ToString(dsCrewDetails.Tables[4].Rows[0]["AllTypesOfTanker"]) + "</span></td>");
                                                    }
                                                    else
                                                        strGrid.Append("<td style=\"width:55px\"><span>" + Convert.ToString(dsCrewDetails.Tables[4].Rows[0]["AllTypesOfTanker"]) + "</span></td>");
                                                }
                                                else
                                                    strGrid.Append("<td style=\"width:55px\"></td>");
                                                drExportexcel["Years on All Types"] = Convert.ToString(dsCrewDetails.Tables[4].Rows[0]["AllTypesOfTanker"]);
                                                #endregion
                                                #region M O Tour
                                                if (dsCrewDetails.Tables[5].Rows.Count > 0)
                                                    if (Convert.ToString(dsCrewDetails.Tables[5].Rows[0]["CrewStatus"]) == "-1")//For simulated crew. because no voyage
                                                        strGrid.Append("<td style=\"width:55px\">NA</td>");
                                                    else
                                                        strGrid.Append("<td style=\"width:55px\">" + Convert.ToString(dsCrewDetails.Tables[5].Rows[0]["MonthsOnVessel"]) + "</td>");
                                                else
                                                    strGrid.Append("<td style=\"width:55px\"></td>");
                                                drExportexcel["M O Tour"] = Convert.ToString(dsCrewDetails.Tables[5].Rows[0]["MonthsOnVessel"]);
                                                #endregion
                                                #region Years Watch
                                                if (dsCrewDetails.Tables[6].Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dsCrewDetails.Tables[6].Rows[0]["CrewStatus"]) == "-1")
                                                        strGrid.Append("<td style=\"width:55px\">NA</td>");
                                                    else
                                                        strGrid.Append("<td style=\"width:55px\">" + Convert.ToString(dsCrewDetails.Tables[6].Rows[0]["YearsWatch"]) + "</td>");
                                                }
                                                else
                                                    strGrid.Append("<td style=\"width:55px\"></td>");
                                                drExportexcel["Years watch"] = Convert.ToString(dsCrewDetails.Tables[6].Rows[0]["YearsWatch"]);
                                                #endregion
                                                #region English_Proficiency
                                                if (dsCrewDetails.Tables[8].Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["English_Proficiency"]) != "")
                                                        strGrid.Append("<td>" + Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["English_Proficiency"]) + "</td>");
                                                    else
                                                        strGrid.Append("<td></td>");
                                                    drExportexcel["English Prof."] = Convert.ToString(dsCrewDetails.Tables[8].Rows[0]["English_Proficiency"]);
                                                }
                                                else
                                                    strGrid.Append("<td></td>");

                                                #endregion

                                                //Check if multiple crew of same rank than show check box and first is selected by deafult
                                                if (dtOnVesselCrew.Rows.Count > 1)
                                                {
                                                    if (dtMinAggCrew.DefaultView.Count > 0)
                                                    {
                                                        strGrid.Append("<td><input type=\"checkbox\" class=\"chkMultiCrew\" checked='checked' RankID='" + Convert.ToString(dtRanks.Rows[k]["RankID"]) + "' CrewID='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["CrewID"]) + "'/></td>");
                                                        drExportexcel["Checked"] = true;
                                                    }
                                                    else
                                                    {
                                                        strGrid.Append("<td><input type=\"checkbox\" class=\"chkMultiCrew\"  RankID='" + Convert.ToString(dtRanks.Rows[k]["RankID"]) + "'  CrewID='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["CrewID"]) + "'/></td>");
                                                        drExportexcel["Checked"] = false;
                                                    }
                                                }
                                                else
                                                {
                                                    drExportexcel["Checked"] = true;
                                                    strGrid.Append("<td></td>");
                                                }


                                                strGrid.Append("<td style=\"text-align:left;\">");
                                                if (Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["ActualCrewID"]) == "")
                                                    strGrid.Append("<img src='" + SimulateIcon + "' title='Simulate Crew' id='btnSimulate_" + RowID + "' ActualCrewID='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["CrewID"]) + "' class=\"btnSimulate\" RankID='" + Convert.ToString(dtRanks.Rows[k]["RankID"]) + "' Nationality='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Nationality"]) + "' Crewid='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["CrewID"]) + "' VesselId='" + VesselId + "' type=\"submit\"  value=\"Simulate\" />");
                                                else
                                                    strGrid.Append("<img src='" + SimulateIcon + "'  title='Simulate Crew' id='btnSimulate_" + RowID + "'  ActualCrewID='" + Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["ActualCrewID"]) + "' class=\"btnSimulate\" RankID='" + Convert.ToString(dtRanks.Rows[k]["RankID"]) + "' Nationality='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["Staff_Nationality"]) + "' Crewid='" + Convert.ToString(dsCrewDetails.Tables[7].Rows[0]["CrewID"]) + "' VesselId='" + VesselId + "' type=\"submit\"  value=\"Simulate\" />");

                                                //show assign button on if vessel event is selected
                                                if (Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["ActualCrewID"]).ToLower() == "event")
                                                {
                                                    strGrid.Append("<img src='" + AssignIcon + "'  title='Assign Crew' id=\"btnAssign_" + RowID + "\"  class=\"btnAssign\" style='display:none;' type=\"submit\"  value=\"Assign\" />");
                                                }
                                                else
                                                {
                                                    if (Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["NewAssign"]).ToLower() == "true")
                                                        strGrid.Append("<img src='" + AssignIcon + "' title='Assign Crew' id=\"btnAssign_" + RowID + "\" vesselid='" + VesselId + "' rankId='" + Convert.ToString(dtRanks.Rows[k]["RankID"]) + "' onsignercrewid='" + Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["CrewID"]) + "' offsignercrewid='" + Convert.ToString(dtOnVesselCrew.Rows[MultiCrew]["OffSignerID"]) + "' class=\"btnAssign\" type=\"submit\"  value=\"Assign\" />");
                                                    else
                                                        strGrid.Append("<img src='" + AssignIcon + "' title='Assign Crew' id=\"btnAssign_" + RowID + "\"  class=\"btnAssign\" style='display:none;' type=\"submit\"  value=\"Assign\" />");
                                                }
                                                strGrid.Append("</td></tr>");
                                            }
                                            dtExportToExcel.Rows.Add(drExportexcel);
                                        }
                                    }
                                    else
                                    {
                                        //If No crew exists show assign and simulate buttons  only
                                        strGrid.Append("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                                        strGrid.Append("<td style=\"text-align:left;\"><img src='" + SimulateIcon + "'  title='Simulate Crew' id='btnSimulate_" + RowID + "' class=\"btnSimulate\" ActualCrewID='0' RankID='" + Convert.ToString(dtRanks.Rows[k]["RankID"]) + "' Nationality='0' Crewid='0'" + "' VesselId='" + VesselId + "'type=\"submit\"  value=\"Simulate\" />");
                                        strGrid.Append("<img src='" + AssignIcon + "' title='Assign Crew' id=\"btnAssign_" + RowID + "\"  class=\"btnAssign\" style='display:none;' type=\"submit\"  value=\"Assign\" /></td></tr>");
                                        strGrid.Append("</tr>");


                                        DataRow drExportexcel = dtExportToExcel.NewRow();
                                        drExportexcel["RankID"] = Convert.ToString(dtRanks.Rows[k]["RankID"]);
                                        drExportexcel["Rank"] = Convert.ToString(dtRanks.Rows[k]["Ranks"]);
                                        dtExportToExcel.Rows.Add(drExportexcel);
                                    }

                                    //Bind Min Aggregated
                                    if (k == dtRanks.Rows.Count - 1)
                                    {
                                        strGrid.Append("<tr class='minAgg'>");
                                        strGrid.Append("<td><span style='font-weight: bold;font-size: 11px;' class='MinAgg'>&nbsp;Min. Aggregated&nbsp;<span></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");

                                        ds.Tables[0].DefaultView.RowFilter = "";
                                        ds.Tables[0].DefaultView.RowFilter = "Group_Name='yearswithoperator' AND Ranks='" + Ranks + "'";
                                        if (oilMajorID != 0 && ds.Tables[0].DefaultView.Count > 0)
                                        {
                                            if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["MinAggOperator"]) > dcYearsWithOperator)
                                                strGrid.Append("<td><span class='error MinAgg'>" + Convert.ToString(dtRanks.Rows[k]["MinAggOperator"]) + "</span></td>");
                                            else
                                                strGrid.Append("<td><span>" + Convert.ToString(dtRanks.Rows[k]["MinAggOperator"]) + "</span></td>");
                                        }
                                        else
                                            strGrid.Append("<td></td>");

                                        ds.Tables[0].DefaultView.RowFilter = "";
                                        ds.Tables[0].DefaultView.RowFilter = "Group_Name='yearsinrank' AND Ranks='" + Ranks + "'";
                                        if (oilMajorID != 0 && ds.Tables[0].DefaultView.Count > 0)
                                        {
                                            if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["MinAggRank"]) > dcYearsOnRank)
                                                strGrid.Append("<td><span class='error MinAgg'>" + Convert.ToString(dtRanks.Rows[k]["MinAggRank"]) + "</span></td>");
                                            else
                                                strGrid.Append("<td><span>" + Convert.ToString(dtRanks.Rows[k]["MinAggRank"]) + "</span></td>");
                                        }
                                        else
                                            strGrid.Append("<td></td>");

                                        if (TankerType.Trim() == "")
                                        {
                                            strGrid.Append("<td></td>");
                                        }
                                        else
                                        {
                                            ds.Tables[0].DefaultView.RowFilter = "";
                                            ds.Tables[0].DefaultView.RowFilter = "Group_Name='yearontankertype' AND Ranks='" + Ranks + "'";
                                            if (oilMajorID != 0 && ds.Tables[0].DefaultView.Count > 0)
                                            {
                                                if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["MinAggTanker"]) > dcThisTypeOfTanker)
                                                    strGrid.Append("<td><span class='error MinAgg'>" + Convert.ToString(dtRanks.Rows[k]["MinAggTanker"]) + "</span></td>");
                                                else
                                                    strGrid.Append("<td><span>" + Convert.ToString(dtRanks.Rows[k]["MinAggTanker"]) + "</span></td>");
                                            }
                                            else
                                                strGrid.Append("<td></td>");
                                        }

                                        ds.Tables[0].DefaultView.RowFilter = "";
                                        ds.Tables[0].DefaultView.RowFilter = "Group_Name='yearonalltypes' AND Ranks='" + Ranks + "'";
                                        if (oilMajorID != 0 && ds.Tables[0].DefaultView.Count > 0)
                                        {
                                            if (UDFLib.ConvertToDecimal(dtRanks.Rows[k]["MinAggAllTypes"]) > dcAllTypesOfTanker)
                                                strGrid.Append("<td><span class='error MinAgg'>" + Convert.ToString(dtRanks.Rows[k]["MinAggAllTypes"]) + "</span></td>");
                                            else
                                                strGrid.Append("<td><span>" + Convert.ToString(dtRanks.Rows[k]["MinAggAllTypes"]) + "</span></td>");
                                        }
                                        else
                                            strGrid.Append("<td></td>");

                                        strGrid.Append("<td></td><td></td><td></td><td></td><td></td>");
                                        strGrid.Append("</tr>");
                                    }

                                }
                            }
                        }
                    }
                    strGrid.Append("</table>");

                    dtExportToExcel.DefaultView.RowFilter = "";
                    //dtExportToExcel.DefaultView.RowFilter = "Name IS NULL";
                    if (dtExportToExcel.DefaultView.Count > 0)
                    {
                        strGrid.Append("<br/>");
                        strGrid.Append(BindAdditionalRules(ds.Tables[4], dtExportToExcel, ds.Tables[3]));
                        strGrid.Append("<br/>");
                    }

                    /// Oil Majors remarks
                    if (Convert.ToString(ds.Tables[6].Rows[0]["Remarks"]) != "")
                        strGrid.Append("<div class='remarks'><span class='spnRemarks' style='float: left;'>Remarks :</span><div style='float: left; margin-left: 5px;'> " + Convert.ToString(ds.Tables[6].Rows[0]["Remarks"]) + "<div></div>");
                }
            }

            if (dtMinAggCrew.Rows.Count > 0)
                Session["dt_dtMinAggCrew"] = dtMinAggCrew;


            //Assgin export to excel datatble every time to session
            Session["dtExportExcel"] = null;
            dtExportToExcel.DefaultView.RowFilter = "";
            dtExportToExcel.DefaultView.RowFilter = "Checked=True OR Checked is null";
            Session["dtExportExcel"] = dtExportToExcel.DefaultView.ToTable();
        }
        catch (Exception ex)
        {
            strGrid.Clear();
            strGrid.Append("<table width='100%' style='color: red;font-weight: bold;margin-top: 23px;text-align: center;'><tr><td>" + UDFLib.GetException("SystemError/GeneralMessage") + "</td></tr></table>");
            UDFLib.WriteExceptionLog(ex);
        }
        //return strGrid.Append("<span style='display:none;'>" + Guid.NewGuid().ToString() + "</span>");
        return strGrid;
    }


    /// <summary>
    /// Bind additional rules to the crew matrix according to the current crew matrix.
    /// </summary>
    /// <param name="DtRule">Rules Data table</param>
    /// <param name="dtCrew">Crew matrix crew</param>
    /// <returns></returns>
    protected StringBuilder BindAdditionalRules(DataTable DtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder strAddtionalRules = new StringBuilder();
        try
        {
            bool isRulesExists = false;
            if (DtRule.Rows.Count > 0 && dtCrew.Rows.Count > 0)
            {
                strAddtionalRules.Append("<table id=\"tblCrewMatrixAddtionalRules\" cellsapcing=\"0\" cellpadding=\"5\" class=\"GridView-css\" style=\"max-width: 74%;color: #000; border-collapse: collapse;\" border=\"1\">");
                strAddtionalRules.Append("<tr class=\"HeaderStyle\">");
                strAddtionalRules.Append("<th align=\"left\" scope=\"col\" style=\"min-width: 500px\">Additional rules</th>");
                strAddtionalRules.Append("<th align=\"center\" scope=\"col\" width='100px'>Comply</th>");
                strAddtionalRules.Append("<th align=\"center\" scope=\"col\" width='100px'>Requirement</th>");
                strAddtionalRules.Append("<th align=\"center\" scope=\"col\" width='100px'>Actual</th>");
                strAddtionalRules.Append("</tr>");


                DataView view = new DataView(DtRule);
                DataTable distinctValues = view.ToTable(true, "RuleName", "ParentId");

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    DtRule.DefaultView.RowFilter = "";
                    DtRule.DefaultView.RowFilter = "RuleName='" + distinctValues.Rows[i]["RuleName"] + "' AND ParentId=" + distinctValues.Rows[i]["ParentId"];
                    if (DtRule.DefaultView.Count > 0)
                    {
                        #region Additional Rule 1
                        if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule1")
                        {
                            ///Space is added at the end of the rule to replace last rankID with rank name
                            strAddtionalRules.Append(AddtionalRule1(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 2
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule2")
                        {
                            strAddtionalRules.Append(AddtionalRule2(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 3
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule3")
                        {
                            strAddtionalRules.Append(AddtionalRule3(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 4
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule4")
                        {
                            strAddtionalRules.Append(AddtionalRule4(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 5
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule5")
                        {
                            strAddtionalRules.Append(AddtionalRule5(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 6
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule6")
                        {
                            strAddtionalRules.Append(AddtionalRule6(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 7
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule7")
                        {
                            strAddtionalRules.Append(AddtionalRule7(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 8
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule8")
                        {
                            strAddtionalRules.Append(AddtionalRule8(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 9
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule9")
                        {
                            strAddtionalRules.Append(AddtionalRule9(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 10
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule10")
                        {
                            strAddtionalRules.Append(AddtionalRule10(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 11
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule11")
                        {
                            strAddtionalRules.Append(AddtionalRule11(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 12
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule12")
                        {
                            strAddtionalRules.Append(AddtionalRule12(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 13
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule13")
                        {
                            strAddtionalRules.Append(AddtionalRule13(DtRule.DefaultView.ToTable(), dtCrew));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 14
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule14")
                        {
                            strAddtionalRules.Append(AddtionalRule14(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 15
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule15")
                        {
                            strAddtionalRules.Append(AddtionalRule15(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 16
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule16")
                        {
                            strAddtionalRules.Append(AddtionalRule16(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 17
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule17")
                        {
                            strAddtionalRules.Append(AddtionalRule17(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 18
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule18")
                        {
                            strAddtionalRules.Append(AddtionalRule18(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }
                        #endregion
                        #region Additional Rule 19
                        else if (Convert.ToString(distinctValues.Rows[i]["RuleName"]) == "AdditionalRule19")
                        {
                            strAddtionalRules.Append(AddtionalRule19(DtRule.DefaultView.ToTable(), dtCrew, dtCrewRankConfig));
                            isRulesExists = true;
                        }

                        #endregion
                    }
                }
                strAddtionalRules.Append("</table>");
                if (!isRulesExists)
                    strAddtionalRules.Clear();
            }
        }
        catch (Exception ex) { strAddtionalRules.Clear(); UDFLib.WriteExceptionLog(ex); }
        return strAddtionalRules;
    }


    /// <summary>
    /// Addiotnal rule 1:- Minimum N1 days between date of joinings for N2 and N3 
    /// </summary>
    /// <param name="strRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns>Returns HTML string for rule 1 </returns>
    private StringBuilder AddtionalRule1(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            DateTime[] Dt = new DateTime[2];
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            StringBuilder strRanks = new StringBuilder();

            // Bind days paramter
            int RuleValue = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            strRule = strRule.Replace("N1", RuleValue.ToString());

            int N2Rank = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            int N3Rank = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);

            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Rank;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            dtCrewRankConfig.DefaultView.RowFilter = "";
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Rank;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            bool IsExists = false;
            // Bind first rank
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N2Rank + " AND checked='True'";
            if (dtCrew.DefaultView.Count > 0)
            {
                IsExists = true;
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                Dt[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                IsExists = false;

            if (IsExists)
            {
                // Bind second rank
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Rank + " AND checked='True'";
                if (dtCrew.DefaultView.Count > 0)
                {
                    IsExists = true;
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    Dt[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                }
                else
                    IsExists = false;
            }

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");
            if (IsExists)//both ranks 
            {
                decimal daysDiff = 0;
                if (Dt[0] > Dt[1])
                    daysDiff = Convert.ToDecimal((Dt[0] - Dt[1]).TotalDays);
                else
                    daysDiff = Convert.ToDecimal((Dt[1] - Dt[0]).TotalDays);

                if (daysDiff >= Convert.ToDecimal(RuleValue))
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + RuleValue + "</td><td>" + daysDiff + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + RuleValue + "</td><td><span class='error'>" + daysDiff + "</span></td></tr>");
            }
            else { rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>"); }

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Addiotnal rule 2 - Minimum N1 Aggreated years on all types of tankers for all Deck officers
    /// </summary>
    /// <param name="strRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns>Returns HTML string for rule 2 </returns>
    private StringBuilder AddtionalRule2(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool IsExists = true;
            // Bind days paramter
            decimal RuleValue = Convert.ToDecimal(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            strRule = strRule.Replace("N1", RuleValue.ToString());

            decimal TotalExp = 0;
            dtDeckOfficers.DefaultView.RowFilter = "";
            dtDeckOfficers.DefaultView.RowFilter = "IsDeckOfficer=1";
            if (dtDeckOfficers.DefaultView.Count > 0)
            {
                StringBuilder strRanks = new StringBuilder();

                for (int i = 0; i < dtDeckOfficers.DefaultView.Count; i++)
                {
                    /// Check rank in rank config.
                    dtCrewRankConfig.DefaultView.RowFilter = "";
                    dtCrewRankConfig.DefaultView.RowFilter = "Ranks='" + Convert.ToString(dtDeckOfficers.DefaultView[i]["Rank_Short_Name"]) + "'";

                    if (dtCrewRankConfig.DefaultView.Count > 0)
                    {
                        dtCrew.DefaultView.RowFilter = "";
                        dtCrew.DefaultView.RowFilter = "Rank='" + Convert.ToString(dtDeckOfficers.DefaultView[i]["Rank_Short_Name"]) + "' AND Checked=true";

                        if (dtCrew.DefaultView.Count > 0 && IsExists == true)
                        {
                            for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                            {
                                TotalExp += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on All Types"]);
                                strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                            }
                        }
                        else
                            IsExists = false;
                    }
                }

                strRule = strRule.Replace("N1", RuleValue.ToString());

                rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");
                if (IsExists)
                {
                    if (TotalExp >= Convert.ToDecimal(RuleValue))
                        rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + RuleValue + "</td><td>" + TotalExp + "</td></tr>");
                    else
                        rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + RuleValue + "</td><td><span class='error'>" + TotalExp + "</span></td></tr>");
                }
                else
                    rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
            }
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// N1 English proficiency must be good
    /// </summary>
    /// <param name="strRule"></param>
    /// <param name="RuleValue"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule3(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);

            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            string Rule = "";
            for (int i = 0; i < Ranks.Length; i++)
            {
                Rule = strRule;
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankId=" + Convert.ToInt32(Ranks[i]) + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0)
                {
                    for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                    {
                        Rule = Rule.Replace("N1", dtCrew.DefaultView[j]["Rank"].ToString());///replace N1 with ranks
                        rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + Rule + "</td>");

                        if (Convert.ToString(dtCrew.DefaultView[j]["English Prof."]).ToLower() == "good")
                            rtnstr.Append("<td><span class='tdTooltip' rel='" + Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + "'>YES</span></td><td>Good</td><td>" + Convert.ToString(dtCrew.DefaultView[j]["English Prof."]) + "</td></tr>");
                        else
                            rtnstr.Append("<td><span class='error tdTooltip' rel='" + Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + "'>NO</span></td><td>Good</td><td><span class='error'>" + Convert.ToString(dtCrew.DefaultView[j]["English Prof."]) + "</span></td></tr>");
                    }
                }
            }
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Senior deck officers(N1) must have aggregate N2 years in rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule4(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool IsExists = true;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0 && IsExists == true)
                {
                    for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                    {
                        TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on Rank"]);
                        strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                    }
                }
                else
                    IsExists = false;
            }

            if (IsExists)
            {
                if (TotalExpRanks >= N2years)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Senior engineering officers(N1) must have aggregate N2 years in rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule5(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool IsExists = true;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0 && IsExists == true)
                {
                    for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                    {
                        TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on Rank"]);
                        strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                    }
                }
                else
                    IsExists = false;

            }

            if (IsExists)
            {
                if (TotalExpRanks >= N2years)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Combined  aggregated for all deck and engineering officers(N1) shall not be less than N2 years in rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule6(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool IsExists = true;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0 && IsExists == true)
                {
                    for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                    {
                        TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on Rank"]);
                        strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                    }
                }
                else
                    IsExists = false;
            }

            if (IsExists)
            {
                if (TotalExpRanks >= N2years)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// If the N1 has less than N2 year, the N3 shall not be less than N4 year in rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule7(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            string N1RanksName = "";

            ///N1
            int N1Rank = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N1Rank;
            strRule = strRule.Replace("N1", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));
            N1RanksName = Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]);

            ///N2
            decimal N2year = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            decimal N2CrewActualExp = 0;
            strRule = strRule.Replace("N2", N2year.ToString());

            ///N3
            int N3Rank = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "";
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Rank;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N4
            decimal N4year = Convert.ToDecimal(dtRule.Select("Key='N4'")[0].ItemArray[3]);
            decimal N4CrewActualExp = 0;
            strRule = strRule.Replace("N4", N4year.ToString());

            bool IsExists = false;
            /// Bind N1 rank
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N1Rank + "AND checked='True'";
            if (dtCrew.DefaultView.Count > 0)
            {

                dtCrew.DefaultView.Sort = "[Years on Rank] DESC";
                N2CrewActualExp = Convert.ToDecimal(dtCrew.DefaultView[0]["Years on Rank"]);
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                IsExists = true;
            }
            else
                IsExists = false;

            if (IsExists)
            {
                /// Bind N3 rank
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Rank + "AND checked='True'";
                if (dtCrew.DefaultView.Count > 0)
                {
                    dtCrew.DefaultView.Sort = "[Years on Rank] DESC";
                    N4CrewActualExp = Convert.ToDecimal(dtCrew.DefaultView[0]["Years on Rank"]);
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    IsExists = true;
                }
                else
                    IsExists = false;
            }

            ///Bind Rule
            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            if (IsExists)
            {
                if (N2CrewActualExp >= N2year)
                {
                    rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='" + N1RanksName + " experience is not less than " + N2year + " years in rank'>N/A</span></td><td>-</td><td>-</td></tr>");
                }
                else
                {
                    if (N2CrewActualExp < N2year && N4CrewActualExp >= N4year)
                        rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>-</td><td>-</td></tr>");
                    else
                        rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>-</td><td>-</td></tr>");
                }
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// If the N1 has less than N2 year, the N3 shall not be less than N4 year in rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule8(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover

            ///N1
            int N1Rank = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N1Rank;
            strRule = strRule.Replace("N1", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N2
            decimal N2year = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            decimal N2CrewActualExp = 0;
            strRule = strRule.Replace("N2", N2year.ToString());

            ///N3
            int N3Rank = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "";
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Rank;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N4
            decimal N4year = Convert.ToDecimal(dtRule.Select("Key='N4'")[0].ItemArray[3]);
            decimal N4CrewActualExp = 0;
            strRule = strRule.Replace("N4", N4year.ToString());

            bool IsExists = false;
            /// Bind N1 rank
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N1Rank + "AND checked='True'";
            if (dtCrew.DefaultView.Count > 0)
            {

                dtCrew.DefaultView.Sort = "[Years on Rank] DESC";
                N2CrewActualExp = Convert.ToDecimal(dtCrew.DefaultView[0]["Years on Rank"]);
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                IsExists = true;
            }
            else
                IsExists = false;

            if (IsExists)
            {
                /// Bind N3 rank
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Rank + "AND checked='True'";
                if (dtCrew.DefaultView.Count > 0)
                {
                    dtCrew.DefaultView.Sort = "[Years on Rank] DESC";
                    N4CrewActualExp = Convert.ToDecimal(dtCrew.DefaultView[0]["Years on Rank"]);
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    IsExists = true;
                }
                else
                    IsExists = false;
            }

            ///Bind Rule
            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            if (IsExists)
            {
                if (N2CrewActualExp < N2year && N4CrewActualExp >= N4year)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>-</td><td>-</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>-</td><td>-</td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///Deck and Engine senior officers (combined)(N1) shall not have less than N2 years of experience in Years with Operator
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule9(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0)///if Ranks doesnot exists in crew matrix
                    CrewExists = true;
                else
                    CrewExists = false;

                for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                {
                    TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years with Operator"]);
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                }
            }

            if (CrewExists)
            {
                if (TotalExpRanks >= N2years && CrewExists)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Deck and Engine 2nd and 3rd officers (combined)(N1) shall not have less than N2 year of experience in Years with Operator
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule10(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0)///if Ranks doesnot exists in crew matrix
                    CrewExists = true;
                else
                    CrewExists = false;

                for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                {
                    TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years with Operator"]);
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                }
            }

            if (CrewExists)
            {
                if (TotalExpRanks >= N2years && CrewExists)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Deck and Engine senior officers (combined)(N1) shall not have less than N2 years of experience in Years with Rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule11(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0)///if Ranks doesnot exists in crew matrix
                    CrewExists = true;
                else
                    CrewExists = false;

                for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                {
                    TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on Rank"]);
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                }
            }

            if (CrewExists)
            {
                if (TotalExpRanks >= N2years && CrewExists)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td ><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");

        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Deck and Engine 2nd and 3rd  officers (combined)(N1) shall not have less than N2 year of experience in Years with Rank
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule12(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            for (int i = 0; i < Ranks.Length; i++)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                if (dtCrew.DefaultView.Count > 0)///if Ranks doesnot exists in crew matrix
                    CrewExists = true;
                else
                    CrewExists = false;

                for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                {
                    TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on Rank"]);
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                }
            }

            if (CrewExists)
            {
                if (TotalExpRanks >= N2years && CrewExists)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    /// Deck and Engine senior officers (combined)(N1) shall not have less than N2 years of experience in Years on This Type of Tanker
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule13(DataTable dtRule, DataTable dtCrew)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {

            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;

            // Bind ranks parameter
            string[] Ranks = Convert.ToString(dtRule.Select("Key='N1'")[0].ItemArray[3]).Split('|');
            strRule = strRule.Replace("(N1)", ""); ///not showing ranks 

            decimal N2years = Convert.ToDecimal(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            strRule = strRule.Replace("N2", N2years.ToString());

            decimal TotalExpRanks = 0;

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");
            if (TankerType.ToLower().Trim() != "")
            {
                StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
                for (int i = 0; i < Ranks.Length; i++)
                {
                    dtCrew.DefaultView.RowFilter = "";
                    dtCrew.DefaultView.RowFilter = "RankID=" + Ranks[i] + "AND checked='True'";

                    if (dtCrew.DefaultView.Count > 0)///if Ranks doesnot exists in crew matrix
                        CrewExists = true;
                    else
                        CrewExists = false;

                    for (int j = 0; j < dtCrew.DefaultView.Count; j++)
                    {
                        TotalExpRanks += Convert.ToDecimal(dtCrew.DefaultView[j]["Years on Tanker Type"]);
                        strRanks.Append(Convert.ToString(dtCrew.DefaultView[j]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[j]["StaffCode"]) + ", ");
                    }
                }

                if (CrewExists)
                {
                    if (TotalExpRanks >= N2years && CrewExists)
                        rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + N2years + "</td><td>" + TotalExpRanks + "</td></tr>");
                    else
                        rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + N2years + "</td><td><span class='error'>" + TotalExpRanks + "</span></td></tr>");
                }
                else
                    rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
            }
            else
            {
                rtnstr.Append("<td><span class='error tdTooltip' nottankertype='1' rel='Selected vessel is not a tanker type'>NO</span></td><td></td><td></td></tr>");
            }
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///  N1 and N2 should not board at the same time
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule14(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            DateTime[] JoiningDates = new DateTime[2];

            int N1Ranks = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N1Ranks;
            strRule = strRule.Replace("N1", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            int N2Ranks = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "";
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Ranks;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            //Check for N1 rank
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N1Ranks + "AND checked='True'";
            if (dtCrew.DefaultView.Count > 0)//having Max. operator exp
            {
                CrewExists = true;
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                JoiningDates[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                CrewExists = false;

            //Check for N2 rank
            if (CrewExists)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N2Ranks + "AND checked='True'";
                if (dtCrew.DefaultView.Count > 0)//having Max. operator exp
                {
                    CrewExists = true;
                    JoiningDates[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                }
                else
                    CrewExists = false;
            }

            ///Bind Html
            if (CrewExists)
            {
                if (JoiningDates[0] != JoiningDates[1])
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>-</td><td>-</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>-</td><td>-</td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///  N1 and N2 should not board at the same time
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule15(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            bool CrewExists = false;
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            DateTime[] JoiningDates = new DateTime[2];

            int N1Ranks = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N1Ranks;
            strRule = strRule.Replace("N1", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            int N2Ranks = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "";
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Ranks;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            //Check for N1 rank
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N1Ranks + "AND checked='True'";
            if (dtCrew.DefaultView.Count > 0)//having Max. operator exp
            {
                CrewExists = true;
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                JoiningDates[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                CrewExists = false;

            //Check for N2 rank
            if (CrewExists)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N2Ranks + "AND checked='True'";
                if (dtCrew.DefaultView.Count > 0)//having Max. operator exp
                {
                    CrewExists = true;
                    JoiningDates[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                }
                else
                    CrewExists = false;
            }

            ///Bind Html
            if (CrewExists)
            {
                if (JoiningDates[0] != JoiningDates[1])
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>-</td><td>-</td></tr>");
                else
                    rtnstr.Append("<td><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>-</td><td>-</td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///  A minimum of N1 days shall lapse between replacement of the N2 and N3
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule16(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            DateTime[] JoiningDates = new DateTime[2];
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            bool IsCrewExists = true;

            ///N1
            int daysdiff = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            strRule = strRule.Replace("N1", daysdiff.ToString());

            ///N2
            int N2Ranks = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Ranks;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N3
            int N3Ranks = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Ranks;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            /// Check N2 rank exists
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N2Ranks + "AND checked='True'";
            dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
            if (dtCrew.DefaultView.Count > 0)
            {
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                JoiningDates[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                IsCrewExists = false;

            /// Check N3 rank exists
            if (IsCrewExists)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Ranks + "AND checked='True'";
                dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
                if (dtCrew.DefaultView.Count > 0)
                {
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    JoiningDates[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                }
                else
                    IsCrewExists = false;
            }

            if (IsCrewExists)
            {
                double CrewSignOnDaysDiff = 0;
                if (JoiningDates[0] > JoiningDates[1])
                    CrewSignOnDaysDiff = (JoiningDates[0] - JoiningDates[1]).TotalDays;
                else if (JoiningDates[1] > JoiningDates[0])
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;
                else
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;

                if (CrewSignOnDaysDiff >= daysdiff)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
                else
                    rtnstr.Append("<td ><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + daysdiff + "</td><td><span class='error'> " + CrewSignOnDaysDiff + "</span> </td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///  A minimum of N1 days shall lapse between replacement of the N2 and N3
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule17(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            DateTime[] JoiningDates = new DateTime[2];
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            bool IsCrewExists = true;

            ///N1
            int daysdiff = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            strRule = strRule.Replace("N1", daysdiff.ToString());

            ///N2
            int N2Ranks = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Ranks;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N3
            int N3Ranks = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Ranks;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            /// Check N2 rank exists
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N2Ranks + "AND checked='True'";
            dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
            if (dtCrew.DefaultView.Count > 0)
            {
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                JoiningDates[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                IsCrewExists = false;

            /// Check N3 rank exists
            if (IsCrewExists)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Ranks + "AND checked='True'";
                dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
                if (dtCrew.DefaultView.Count > 0)
                {
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    JoiningDates[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                }
                else
                    IsCrewExists = false;
            }

            if (IsCrewExists)
            {
                double CrewSignOnDaysDiff = 0;
                if (JoiningDates[0] > JoiningDates[1])
                    CrewSignOnDaysDiff = (JoiningDates[0] - JoiningDates[1]).TotalDays;
                else if (JoiningDates[1] > JoiningDates[0])
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;
                else
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;

                if (CrewSignOnDaysDiff >= daysdiff)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
                else
                    rtnstr.Append("<td ><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///  A minimum of N1 days shall lapse between replacement of the N2 and N3
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule18(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            DateTime[] JoiningDates = new DateTime[2];
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            bool IsCrewExists = true;

            ///N1
            int daysdiff = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            strRule = strRule.Replace("N1", daysdiff.ToString());

            ///N2
            int N2Ranks = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Ranks;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N3
            int N3Ranks = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Ranks;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            /// Check N2 rank exists
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N2Ranks + "AND checked='True'";
            dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
            if (dtCrew.DefaultView.Count > 0)
            {
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                JoiningDates[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                IsCrewExists = false;

            /// Check N3 rank exists
            if (IsCrewExists)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Ranks + "AND checked='True'";
                dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
                if (dtCrew.DefaultView.Count > 0)
                {
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    JoiningDates[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                }
                else
                    IsCrewExists = false;
            }

            if (IsCrewExists)
            {
                double CrewSignOnDaysDiff = 0;
                if (JoiningDates[0] > JoiningDates[1])
                    CrewSignOnDaysDiff = (JoiningDates[0] - JoiningDates[1]).TotalDays;
                else if (JoiningDates[1] > JoiningDates[0])
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;
                else
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;

                if (CrewSignOnDaysDiff >= daysdiff)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
                else
                    rtnstr.Append("<td ><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }

    /// <summary>
    ///  A minimum of N1 days shall lapse between replacement of the N2 and N3
    /// </summary>
    /// <param name="dtRule"></param>
    /// <param name="dtCrew"></param>
    /// <returns></returns>
    private StringBuilder AddtionalRule19(DataTable dtRule, DataTable dtCrew, DataTable dtCrewRankConfig)
    {
        StringBuilder rtnstr = new StringBuilder();
        try
        {
            string strRule = Convert.ToString(dtRule.Rows[0]["Rule"]);
            DateTime[] JoiningDates = new DateTime[2];
            StringBuilder strRanks = new StringBuilder();//To be displayed on mouseover
            bool IsCrewExists = true;

            ///N1
            int daysdiff = Convert.ToInt32(dtRule.Select("Key='N1'")[0].ItemArray[3]);
            strRule = strRule.Replace("N1", daysdiff.ToString());

            ///N2
            int N2Ranks = Convert.ToInt32(dtRule.Select("Key='N2'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N2Ranks;
            strRule = strRule.Replace("N2", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            ///N3
            int N3Ranks = Convert.ToInt32(dtRule.Select("Key='N3'")[0].ItemArray[3]);
            dtCrewRankConfig.DefaultView.RowFilter = "RankID=" + N3Ranks;
            strRule = strRule.Replace("N3", Convert.ToString(dtCrewRankConfig.DefaultView[0]["Ranks"]));

            rtnstr.Append("<tr style=\"text-align:center;\"><td style=\"text-align:left;\">" + strRule + "</td>");

            /// Check N2 rank exists
            dtCrew.DefaultView.RowFilter = "";
            dtCrew.DefaultView.RowFilter = "RankID=" + N2Ranks + "AND checked='True'";
            dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
            if (dtCrew.DefaultView.Count > 0)
            {
                strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]) + ", ");
                JoiningDates[0] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
            }
            else
                IsCrewExists = false;

            /// Check N3 rank exists
            if (IsCrewExists)
            {
                dtCrew.DefaultView.RowFilter = "";
                dtCrew.DefaultView.RowFilter = "RankID=" + N3Ranks + "AND checked='True'";
                dtCrew.DefaultView.Sort = "[Years with Operator] DESC";
                if (dtCrew.DefaultView.Count > 0)
                {
                    strRanks.Append(Convert.ToString(dtCrew.DefaultView[0]["Rank"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["Name"]) + ":" + Convert.ToString(dtCrew.DefaultView[0]["StaffCode"]));
                    JoiningDates[1] = Convert.ToDateTime(Convert.ToString(dtCrew.DefaultView[0]["SignOnDate"]));
                }
                else
                    IsCrewExists = false;
            }

            if (IsCrewExists)
            {
                double CrewSignOnDaysDiff = 0;
                if (JoiningDates[0] > JoiningDates[1])
                    CrewSignOnDaysDiff = (JoiningDates[0] - JoiningDates[1]).TotalDays;
                else if (JoiningDates[1] > JoiningDates[0])
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;
                else
                    CrewSignOnDaysDiff = (JoiningDates[1] - JoiningDates[0]).TotalDays;

                if (CrewSignOnDaysDiff >= daysdiff)
                    rtnstr.Append("<td><span class='tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>YES</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
                else
                    rtnstr.Append("<td ><span class='error tdTooltip' rel='" + strRanks.ToString().Trim().TrimEnd(',') + "'>NO</span></td><td>" + daysdiff + "</td><td>" + CrewSignOnDaysDiff + "</td></tr>");
            }
            else
                rtnstr.Append("<td><span class='tdTooltip' nottankertype='1' rel='Rank(s) are missing in crew matrix'>N/A</span></td><td>-</td><td>-</td></tr>");
        }
        catch (Exception ex) { rtnstr.Clear(); UDFLib.WriteExceptionLog(ex); }
        return rtnstr;
    }
}