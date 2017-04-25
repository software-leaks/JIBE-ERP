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
using System.Web.UI.HtmlControls;

public partial class CrewList : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_Country objCountry = new BLL_Infra_Country();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess(); public string TodayDateFormat = "";
    MergeGridviewHeader_Info objContractList = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = CalendarExtender1.Format = UDFLib.GetDateFormat();
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        TodayDateFormat = UDFLib.DateFormatMessage();
        objContractList.AddMergedColumns(new int[] { 13, 14 }, "Eval(%)", "HeaderStyle-css-2");

        if (!IsPostBack)
        {
            ConfidentialityCheck();
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            int CurrentUserID = GetSessionUserID();
            hdnUserID.Value = CurrentUserID.ToString();

            //txtSearchJoinFromDate.Text = DateTime.Now.AddYears(-5).ToString("dd/MM/yyyy", iFormatProvider);
            txtSearchJoinFromDate.Text = DateTime.Now.AddYears(-5).ToString("dd/MM/yyyy", iFormatProvider);
            txtSearchJoinFromDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtSearchJoinFromDate.Text));

            txtSearchJoinToDate.Text = DateTime.Now.ToString("dd/MM/yyyy", iFormatProvider);
            txtSearchJoinToDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtSearchJoinToDate.Text));

            Load_CountryList();
            Load_FleetList();
            Load_ManningAgentList();
            Load_RankList();
            Load_VesselList();
            Load_MainStatus();
            Load_VesselTypes();

            ucCustomPager_CrewList.PageSize = 30;

            FillGridViewAfterSearch();

            if (Session["UTYPE"].ToString() == "MANNING AGENT")
            {
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[14].Visible = false;
            }
        }
        string js = "Timer();RefreshButtonPopups();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
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
            btnAddNewCrew.Enabled = false;
            ImgFeedback.Visible = false;
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
    /// <summary>
    /// Bind all vessel types
    /// </summary>
    public void Load_VesselTypes()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVesselType = objVsl.Get_VesselTypeList();
            ddlVesselType.DataSource = dtVesselType;
            ddlVesselType.DataTextField = "VesselTypes";
            ddlVesselType.DataValueField = "ID";
            ddlVesselType.DataBind();
        }
        catch (Exception ex)
        {

        }
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
    public void Load_CountryList()
    {
        ddlCountry.DataSource = objCrew.Get_CrewNationality(GetSessionUserID());
        ddlCountry.DataTextField = "COUNTRY";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCountry.SelectedIndex = 0;
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, 0, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    public void Load_RankList()
    {
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;


    }
    public void Load_ManningAgentList()
    {
        int UserCompanyID = 0;
        if (getSessionString("USERCOMPANYID") != "")
        {
            UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
        }
        ddlManningOffice.DataSource = objCrew.Get_ManningAgentList(UserCompanyID);
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();

        ddlManningOffice.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    public void Load_MainStatus()
    {
        DataTable dtMainStatus = objCrewAdmin.Get_CrewMainStatus();

        ddlMainStatus.DataSource = dtMainStatus;
        ddlMainStatus.DataTextField = "NAME";
        ddlMainStatus.DataValueField = "ID";
        ddlMainStatus.DataBind();
        ddlMainStatus.SelectedIndex = 0;

        dtMainStatus.DefaultView.RowFilter = "Value='Onboard'";
        ddlMainStatus.SelectedValue = dtMainStatus.DefaultView[0]["Id"].ToString();
        hdOnBoardStatusId.Value = dtMainStatus.DefaultView[0]["Id"].ToString();
        Load_CalculatedStatus(int.Parse(ddlMainStatus.SelectedValue));
    }
    public void Load_CalculatedStatus(int MainStatusId)
    {
        ddlCalculatedStatus.DataSource = objCrewAdmin.Get_CrewCalculatedStatus(MainStatusId);

        ddlCalculatedStatus.DataTextField = "NAME";
        ddlCalculatedStatus.DataValueField = "ID";
        ddlCalculatedStatus.DataBind();
        ddlCalculatedStatus.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCalculatedStatus.SelectedIndex = 0;
    }

    public void FillGridViewAfterSearch()
    {
        DataTable dtFilters = GetSearchDataTable();

        int PAGE_SIZE = ucCustomPager_CrewList.PageSize;
        int PAGE_INDEX = ucCustomPager_CrewList.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager_CrewList.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //Passing selected Vessel type for filter
        int i = 1;
        DataTable dtVesselTypes = new DataTable();
        dtVesselTypes.Columns.Add("PID");
        dtVesselTypes.Columns.Add("VALUE");

        foreach (DataRow dr in ddlVesselType.SelectedValues.Rows)
        {
            DataRow dr1 = dtVesselTypes.NewRow();
            dr1["PID"] = i;
            dr1["VALUE"] = dr[0];
            dtVesselTypes.Rows.Add(dr1);
            i++;
        }

        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Index(dtFilters, dtVesselTypes, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);


        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (ucCustomPager_CrewList.isCountRecord == 1)
        {
            ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewList.BuildPager();
        }
        string js = "Timer();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Timer", js, true);
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (Session["UTYPE"].ToString() != "MANNING AGENT")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    MergeGridviewHeader.SetProperty(objContractList);

                    e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                    ViewState["DynamicHeaderCSS"] = "HeaderStyle-css-2";
                }
            }
        }
        catch { }
    }

    private int GetColumnIndexByName(string name)
    {
        foreach (DataControlField col in GridView1.Columns)
        {
            if (col.HeaderText.ToLower().Trim() == name.ToLower().Trim())
            {
                return GridView1.Columns.IndexOf(col);
            }
        }

        return -1;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string PlannedInterviewID = DataBinder.Eval(e.Row.DataItem, "PlannedInterviewID").ToString();
            string IQID = DataBinder.Eval(e.Row.DataItem, "IQID").ToString();
            string Status_Remark = DataBinder.Eval(e.Row.DataItem, "Status_Remark").ToString();
            string CrewName = DataBinder.Eval(e.Row.DataItem, "staff_fullname").ToString();
            string ManningOfficeStatus = DataBinder.Eval(e.Row.DataItem, "ManningOfficeStatus").ToString();
            string CrewStatus = DataBinder.Eval(e.Row.DataItem, "CrewStatus").ToString().ToUpper();
            string CrewID_UnAssigned = DataBinder.Eval(e.Row.DataItem, "CrewID_UnAssigned").ToString();
            string CrewID_SigningOff = DataBinder.Eval(e.Row.DataItem, "CrewID_SigningOff").ToString();

            string Staff_Code_OffSigner = DataBinder.Eval(e.Row.DataItem, "Staff_Code_OffSigner").ToString();
            string Staff_Name_OffSigner = DataBinder.Eval(e.Row.DataItem, "Staff_Name_OffSigner").ToString();
            string Staff_Code_OnSigner = DataBinder.Eval(e.Row.DataItem, "Staff_Code_OnSigner").ToString();
            string Staff_Name_OnSigner = DataBinder.Eval(e.Row.DataItem, "Staff_Name_OnSigner").ToString();

            string SignOffFromVessel = DataBinder.Eval(e.Row.DataItem, "OffSignerVessel").ToString();
            string SignOnToVessel = DataBinder.Eval(e.Row.DataItem, "Vessel_Name").ToString();

            string Available_From_Date = DataBinder.Eval(e.Row.DataItem, "Available_From_Date").ToString();
            string Sign_Off_Date = DataBinder.Eval(e.Row.DataItem, "Sign_Off_Date").ToString();
            string MDFStaff = DataBinder.Eval(e.Row.DataItem, "MDFStaff").ToString();

            int Marks = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Marks").ToString());
            int AllAvg = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "AllAvg").ToString());
            int Last_Evaluation_Days = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Last_Evaluation_Days").ToString());

            string CrewID_Sign_On_Validation = DataBinder.Eval(e.Row.DataItem, "Sign_On_Validation").ToString();
            if (CrewID_Sign_On_Validation.Contains("Passport is expiring/expired on :"))
            {
                CrewID_Sign_On_Validation = CrewID_Sign_On_Validation.Replace("Passport is expiring/expired on :", "");
                string str = CrewID_Sign_On_Validation.Substring(0, 10);
                CrewID_Sign_On_Validation = CrewID_Sign_On_Validation.Replace(str, "");
                if (!string.IsNullOrEmpty(str))
                    str = UDFLib.ConvertUserDateFormat(str, UDFLib.GetDateFormat());
                CrewID_Sign_On_Validation = "Passport is expiring/expired on :" + str + CrewID_Sign_On_Validation;
            }
            if (CrewID_Sign_On_Validation.Contains("Seaman is expiring/expired on :"))
            {
                CrewID_Sign_On_Validation = CrewID_Sign_On_Validation.Replace("Seaman is expiring/expired on :", "");
                string str = CrewID_Sign_On_Validation.Substring(0, 10);
                CrewID_Sign_On_Validation = CrewID_Sign_On_Validation.Replace(str, "");
                if (!string.IsNullOrEmpty(str))
                    str = UDFLib.ConvertUserDateFormat(str, UDFLib.GetDateFormat());
                CrewID_Sign_On_Validation = "Seaman is expiring/expired on :" + str + CrewID_Sign_On_Validation;
            }
            DataRow[] dr;
            DataTable dt = objCrewAdmin.CRW_GetCDConfiguration(null).Tables[0];
            if (dt.Rows.Count > 0)
            {
                // string confSSN=dt.row
                string str = "Age";
                dr = dt.Select("Key ='" + str + "'");
                if (dr.Length > 0)
                {
                    if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                    {
                        GridView1.Columns[GetColumnIndexByName("Age")].Visible = true;
                    }
                    else
                    {
                        GridView1.Columns[GetColumnIndexByName("Age")].Visible = false;
                    }
                }
            }
            switch (CrewStatus)
            {
                case "ONBOARD":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Current";
                    break;
                case "EOC DUE":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_SigningOff";
                    break;
                case "ON LEAVE":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_SignedOff";
                    break;
                case "SIGNON-PLANNED":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Assigned";
                    break;
                case "SIGNOFF-PLANNED":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Assigned";
                    break;
                case "SIGNON-EVENT":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Planned";
                    break;
                case "SIGNOFF-EVENT":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Planned";
                    break;
                case "CANDIDATE":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Pending";
                    break;
                case "NO VOYAGE":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_NoVoyage";
                    if (Available_From_Date != "")
                    {
                        if (DateTime.Parse(Available_From_Date) < DateTime.Today)
                        {
                            //Available - No Voyage
                            Label lblAvailable_From_Date = (Label)e.Row.FindControl("lblAvailable_From_Date");
                            if (lblAvailable_From_Date != null)
                            {
                                lblAvailable_From_Date.CssClass = "Ready_NoVoyage";
                            }
                        }
                    }
                    break;
                case "INACTIVE":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Inactive";
                    break;
                case "NTBR":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_NTBR";
                    e.Row.CssClass = "CrewStatus_NTBR_Row";
                    break;
                case "REJECTED":
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "CrewStatus_Rejected";
                    break;
            }

            if (Status_Remark != "")
            {
                Status_Remark = Status_Remark.Replace("\n", "<br>");
                e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + Status_Remark + "]");
            }


            if (UDFLib.ConvertToInteger(PlannedInterviewID) > 0)
            {
                ImageButton ImgInterview = (ImageButton)(e.Row.FindControl("ImgInterview"));
                if (ImgInterview != null)
                {
                    if (UDFLib.ConvertToInteger(IQID) > 0)
                        ImgInterview.Attributes.Add("onclick", "window.open('CrewInterview.aspx?ID=" + PlannedInterviewID + "&CrewID=" + strRowId + "');");
                    else
                        ImgInterview.Attributes.Add("onclick", "window.open('Interview.aspx?ID=" + PlannedInterviewID + "&CrewID=" + strRowId + "');");

                    ImgInterview.ImageUrl = "~/Images/Interview_1.png";
                }
            }
            else
            {
                ImageButton ImgInterview = (ImageButton)(e.Row.FindControl("ImgInterview"));
                if (ImgInterview != null)
                {
                    ImgInterview.Visible = false;
                }
            }
            if (CrewID_UnAssigned != "")
            {
                ImageButton ImgCrewAssigned = (ImageButton)(e.Row.FindControl("ImgCrewAssigned"));
                if (ImgCrewAssigned != null)
                {
                    ImgCrewAssigned.Attributes.Add("onclick", "window.open('CrewDetails.aspx?ID=" + CrewID_UnAssigned + "');");
                    ImgCrewAssigned.ImageUrl = "~/Images/crew-on.png";
                    ImgCrewAssigned.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[On-Signer] body=[" + Staff_Code_OnSigner + " - " + Staff_Name_OnSigner + "<br>To Vessel: " + SignOnToVessel + "]");
                }
            }
            else
            {
                ImageButton ImgCrewAssigned = (ImageButton)(e.Row.FindControl("ImgCrewAssigned"));
                if (ImgCrewAssigned != null)
                {
                    ImgCrewAssigned.Visible = false;
                }
            }
            if (CrewID_Sign_On_Validation != "")
            {
                ImageButton ImgMissingDocuments = (ImageButton)(e.Row.FindControl("ImgMissingDocuments"));
                if (ImgMissingDocuments != null)
                {
                    ImgMissingDocuments.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Missing Data] body=[" + CrewID_Sign_On_Validation + "]");
                    ImgMissingDocuments.Visible = true;
                    ImgMissingDocuments.ImageUrl = "~/Images/folder-error.png";
                }
            }
            if (CrewID_SigningOff != "")
            {
                ImageButton ImgCrewSigningOff = (ImageButton)(e.Row.FindControl("ImgCrewSigningOff"));
                if (ImgCrewSigningOff != null)
                {
                    ImgCrewSigningOff.Attributes.Add("onclick", "window.open('CrewDetails.aspx?ID=" + CrewID_SigningOff + "');");
                    ImgCrewSigningOff.ImageUrl = "~/Images/crew-off.png";
                    ImgCrewSigningOff.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Off-Signer] body=[" + Staff_Code_OffSigner + " - " + Staff_Name_OffSigner + "<br>From Vessel: " + SignOffFromVessel + "]");
                }
            }
            else
            {
                ImageButton ImgCrewSigningOff = (ImageButton)(e.Row.FindControl("ImgCrewSigningOff"));
                if (ImgCrewSigningOff != null)
                {
                    ImgCrewSigningOff.Visible = false;
                }
            }

            if (objUA.Edit > 0)
            {
                ImageButton ImgEdit = (ImageButton)(e.Row.FindControl("ImgEdit"));
                if (ImgEdit != null)
                {
                    //ImgEdit.Attributes.Add("onclick", "window.open('AddEditCrew.aspx?ID=" + strRowId + "');");
                    ImgEdit.Attributes.Add("onclick", "window.open('AddEditCrewNew.aspx?crewid=" + strRowId + "');");
                    ImgEdit.ImageUrl = "~/Images/edit.gif";
                }
            }
            else
            {
                ImageButton ImgEdit = (ImageButton)(e.Row.FindControl("ImgEdit"));
                if (ImgEdit != null)
                {
                    ImgEdit.Visible = false;
                }
            }
            Image ImbCOCModified = (Image)e.Row.FindControl("ImbCOCModified");
            if (ImbCOCModified != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "COCRemark").ToString() == "")
                {
                    ImbCOCModified.Visible = false;
                }
                else
                {
                    ImbCOCModified.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[COC Modified] body=[" + DataBinder.Eval(e.Row.DataItem, "COCRemark").ToString().Replace("\n", "<br>") + "]");
                }
            }
            Label lblX = (Label)e.Row.FindControl("lblX");
            if (lblX != null)
            {
                int iCount = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "InterviewCount").ToString());

                if (iCount > 0 && iCount <= 2)
                {
                    lblX.Text = "*";
                }
                if (iCount > 2)
                {
                    lblX.Text = "**";
                }
            }
            //Added a check so that only Office User or Admin can Propose or Approve Red/Yellow card(Manning agent should not be able to propose/approve card)
            if (Session["UTYPE"].ToString() == "OFFICE USER" || Session["UTYPE"].ToString() == "ADMIN")
            {
                Image ImgCard = (Image)e.Row.FindControl("ImgCard");
                if (ImgCard != null)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "CardType").ToString() == "")
                    {
                        ImgCard.Visible = false;
                    }
                    else
                    {
                        ImgCard.ImageUrl = "../images/" + DataBinder.Eval(e.Row.DataItem, "CardType").ToString().Replace(" ", "") + "_" + DataBinder.Eval(e.Row.DataItem, "CardsTATUS").ToString() + ".png";
                        ImgCard.Visible = true;
                        ImgCard.Attributes.Add("onclick", "showDiv('dvProposeYellowCard','" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "');");

                        ImgCard.Attributes.Add("onmouseover", "showRemarks(" + DataBinder.Eval(e.Row.DataItem, "CardID").ToString() + ",event,this);");
                    }
                }
            }
            if (MDFStaff == "1")
            {
                e.Row.Cells[e.Row.Cells.Count - 10].Text = "";
            }

            Label lblMarks = (Label)e.Row.FindControl("lblMarks");
            if (lblMarks != null)
            {
                if (Marks < AllAvg)
                {
                    e.Row.Cells[e.Row.Cells.Count - 4].CssClass = "eval-background-pink";
                }
                if (Marks > AllAvg)
                {
                    e.Row.Cells[e.Row.Cells.Count - 4].CssClass = "eval-background-lightgreen";
                }

                if (Last_Evaluation_Days > 90)
                {
                    e.Row.Cells[e.Row.Cells.Count - 4].CssClass = "eval-background-yellow";
                }

                e.Row.Cells[e.Row.Cells.Count - 4].ToolTip = "Last evaluation done before: " + Last_Evaluation_Days.ToString() + " days";
            }

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/Images/arrowUp.png";
                    else
                        img.Src = "~/Images/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteCrew")
        {
            string arg = e.CommandArgument.ToString();
            if (arg != "")
            {
                int CrewID = int.Parse(arg);
                objCrew.Delete_Crew(CrewID, GetSessionUserID());
                FillGridViewAfterSearch();
            }
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPager_CrewList.isCountRecord = 1;
            ucCustomPager_CrewList.CurrentPageIndex = 1;
            FillGridViewAfterSearch();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RefreshButtonPopups", "RefreshButtonPopups();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnAddNewCrew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditCrew.aspx");
    }

    protected void ConfidentialityCheck()
    {
        DataRow[] dr;
        DataTable dt = objCrewAdmin.CRW_GetCDConfiguration(null).Tables[0];
        string str = "";
        foreach (ListItem li in CheckBoxList1.Items)
        {

            if (li.Value == "SEAMAN_BOOK_NUMBER")
            {
                if (dt.Rows.Count > 0)
                {
                    str = "Seaman";
                    dr = dt.Select("Key ='" + str + "'");
                    if (dr.Length > 0)
                    {
                        if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                        {
                            li.Selected = true;
                            li.Enabled = true;

                        }
                        else
                        {
                            li.Selected = false;
                            li.Enabled = false;
                            li.Attributes.CssStyle.Add("display", "none");
                        }
                    }
                }
            }
        }

        str = "Uniform";
        dr = dt.Select("Key ='" + str + "'");
        if (dr.Length > 0)
        {
            if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
            {
                lnkExportUniform.Visible = true;
                imgUniform.Visible = true;
            }
            else
            {
                lnkExportUniform.Visible = false;
                imgUniform.Visible = false;
            }
        }
    }
    protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        string css = @"
        <style type='text/css'>
        .CrewStatus_Current
        {
            background-color: #aabbdd;
        }
        .CrewStatus_SigningOff
        {
            background-color: #F3F781;
        }
        .CrewStatus_SignedOff
        {
            background-color: #F5A9A9;
        }
        .CrewStatus_Assigned
        {
            background-color: #BBB6FF;
        }
        .CrewStatus_Planned
        {
            background-color: #F781F3;
        }
        .CrewStatus_Pending
        {
            background-color: #81BEF7;
        }
        .CrewStatus_Inactive
        {
            background-color: #848484;
            color: #E6E6E6;
        }
        .CrewStatus_NoVoyage
        {
            background-color: #A9F5D0;
        }
        .CrewStatus_NTBR
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_NTBR_Row
        {
            color: Red;            
        }
        .CrewStatus_UNFIT
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_UNFIT_Row
        {
            color: Red;            
        }
        .imgCOC
        {
            visible: false;
        }
        .CrewStatus_Rejected
        {
            background-color: RED;
            color: Yellow;
        }
        </style>
        ";


        GridView1.AllowPaging = false;
        GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        FillGridViewAfterSearch();
        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected == false)
                GridView1.Columns[int.Parse(li.Value)].Visible = false;
        }

        GridViewExportUtil.Export("CrewList.xls", GridView1, css);
    }

    protected void ImgFeedback_Click(object sender, ImageClickEventArgs e)
    {
        int FleetID = 0;
        int VesselID = 0;
        int Nationality = 0;
        int ManningOfficeID = 0;
        int RankID = 0;
        int COC = 0;
        string SearchText = "";

        int StatusId = UDFLib.ConvertToInteger(ddlMainStatus.SelectedValue.ToString());
        int CalCulatedStatusId = UDFLib.ConvertToInteger(ddlCalculatedStatus.SelectedValue.ToString());

        if (ddlFleet.SelectedValue != "")
            FleetID = int.Parse(ddlFleet.SelectedValue);
        if (ddlCountry.SelectedValue != "")
            Nationality = int.Parse(ddlCountry.SelectedValue);
        if (ddlVessel.SelectedValue != "")
            VesselID = int.Parse(ddlVessel.SelectedValue);
        if (ddlManningOffice.SelectedValue != "")
            ManningOfficeID = int.Parse(ddlManningOffice.SelectedValue);

        SearchText = txtSearchText.Text;
        COC = int.Parse(ddlCOC.SelectedValue);
        RankID = int.Parse(ddlRank.SelectedValue);

        ResponseHelper.Redirect("RequestFeedback.aspx?vid=" + VesselID + "&flt=" + FleetID + "&nat=" + Nationality + "&rank=" + RankID + "&search=" + SearchText + "&st=" + StatusId + "&cst=" + CalCulatedStatusId + "&mo=" + ManningOfficeID + "&coc=" + COC + "&jFrom=" + txtSearchJoinFromDate.Text + "&jTo=" + txtSearchJoinToDate.Text, "_blank", "");
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();

    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        if (VesselID > 0)
        {
            lblSEQ.Text = objCrew.Get_SEQAndONBD(VesselID).Replace("<br>", "&nbsp;&nbsp;&nbsp;&nbsp;");
            lnkCrewList.NavigateUrl = "~/Crew/CrewList_PhotoView.aspx?vcode=" + objVessel.Get_VesselCode_ByID(VesselID);
            lnkCrewList_Print.NavigateUrl = "~/Crew/CrewList_Print.aspx?vcode=" + objVessel.Get_VesselCode_ByID(VesselID);
        }
        else
        {
            lblSEQ.Text = "";
            lnkCrewList.NavigateUrl = "";
            lnkCrewList_Print.NavigateUrl = "";
        }
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        string[] HeaderCaptions = { };
        string[] DataColumnsName = { };

        DataTable dtFilters = GetSearchDataTable();

        DataTable dtExportColumns = new DataTable();
        dtExportColumns.Columns.Add("ParamName", typeof(String));
        dtExportColumns.Columns.Add("ParamValue", typeof(String));

        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected == true)
            {
                dtExportColumns.Rows.Add(li.Value.ToString(), "1");

                Array.Resize(ref HeaderCaptions, HeaderCaptions.Length + 1); HeaderCaptions[HeaderCaptions.Length - 1] = li.Text.ToString();
                Array.Resize(ref DataColumnsName, DataColumnsName.Length + 1); DataColumnsName[DataColumnsName.Length - 1] = li.Value.ToString();

            }
        }
        if (dtExportColumns.Rows.Count == 0)
        {
            string js = "alert('Please Select at least one column!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            return;
        }
        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Export(dtFilters, dtExportColumns, GetSessionUserID());

        ChangeColumnDataType(dt, "EOC", typeof(string));
        ChangeColumnDataType(dt, "Joining_date", typeof(string));
        ChangeColumnDataType(dt, "Staff_Birth_Date", typeof(string));

        foreach (DataRow item in dt.Rows)
        {
            if (!string.IsNullOrEmpty(item["EOC"].ToString()))
                item["EOC"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["EOC"]), UDFLib.GetDateFormat());
            if (!string.IsNullOrEmpty(item["Joining_date"].ToString()))
                item["Joining_date"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["Joining_date"]), UDFLib.GetDateFormat());
            if (!string.IsNullOrEmpty(item["Staff_Birth_Date"].ToString()))
                item["Staff_Birth_Date"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["Staff_Birth_Date"]), UDFLib.GetDateFormat());
        }

        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "CrewListExport.xls", "CrewList Export");
    }

    protected void lnkExportToText_Click(object sender, EventArgs e)
    {
        string[] HeaderCaptions = { };
        string[] DataColumnsName = { };
        DataTable dtFilters = GetSearchDataTable();

        DataTable dtExportColumns = new DataTable();
        dtExportColumns.Columns.Add("ParamName", typeof(String));
        dtExportColumns.Columns.Add("ParamValue", typeof(String));

        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected == true)
            {
                dtExportColumns.Rows.Add(li.Value.ToString(), "1");

                Array.Resize(ref HeaderCaptions, HeaderCaptions.Length + 1); HeaderCaptions[HeaderCaptions.Length - 1] = li.Text.ToString();
                Array.Resize(ref DataColumnsName, DataColumnsName.Length + 1); DataColumnsName[DataColumnsName.Length - 1] = li.Value.ToString();
            }
        }
        if (dtExportColumns.Rows.Count == 0)
        {
            string js = "alert('Please select at least one column in Export Options!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "least", js, true);
            return;
        }
        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Export(dtFilters, dtExportColumns, GetSessionUserID());

        ChangeColumnDataType(dt, "EOC", typeof(string));
        ChangeColumnDataType(dt, "Joining_date", typeof(string));
        ChangeColumnDataType(dt, "Staff_Birth_Date", typeof(string));

        foreach (DataRow item in dt.Rows)
        {
            if (!string.IsNullOrEmpty(item["EOC"].ToString()))
                item["EOC"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["EOC"]), UDFLib.GetDateFormat());
            if (!string.IsNullOrEmpty(item["Joining_date"].ToString()))
                item["Joining_date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Joining_date"]), UDFLib.GetDateFormat());
            if (!string.IsNullOrEmpty(item["Staff_Birth_Date"].ToString()))
                item["Staff_Birth_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Staff_Birth_Date"]), UDFLib.GetDateFormat());
        }
        using (StringWriter sw = new StringWriter())
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sw.WriteLine((i + 1).ToString());
                sw.WriteLine("------------------------------");
                foreach (ListItem li in CheckBoxList1.Items)
                {
                    if (li.Selected == true)
                    {
                        sw.WriteLine(li.Text + ": " + dt.Rows[i][li.Value.ToString()].ToString());
                    }
                }
                sw.WriteLine("        ");
            }
            string fileName = "CrewList.txt";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }

    }

    protected void lnkExportUniform_Click(object sender, EventArgs e)
    {
        string[] HeaderCaptions = { };
        string[] DataColumnsName = { };

        DataTable dtFilters = GetSearchDataTable();

        DataTable dt = objCrew.Get_UniformSizeReport(dtFilters, GetSessionUserID());

        HeaderCaptions = new string[] { "Vessel", "Staff Code", "Staff Name", "Shoe Size", "T-Shirt Size", "Cargo Pant Size", "Overall Size" };
        DataColumnsName = new string[] { "Vessel_Name", "Staff_Code", "staff_fullname", "SHOE_SIZE", "TSHIRT_SIZE", "PANT_SIZE", "OVERALL_SIZE" };

        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "UniformSize.xls", "Uniform Size Report");
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        FillGridViewAfterSearch();
    }

    protected DataTable GetSearchDataTable()
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

        if (txtSearchJoinFromDate.Text != "")
            dtFrom = UDFLib.ConvertToDate(Convert.ToString(txtSearchJoinFromDate.Text), UDFLib.GetDateFormat());

        if (txtSearchJoinToDate.Text != "")
            dtTo = UDFLib.ConvertToDate(Convert.ToString(txtSearchJoinToDate.Text), UDFLib.GetDateFormat());

        dtFilters.Rows.Add(
            1,
            UDFLib.ConvertToInteger(ddlFleet.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlVessel.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlRank.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlCountry.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlMainStatus.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlCalculatedStatus.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlManningOffice.SelectedValue.ToString()),
            UDFLib.ConvertToInteger(ddlCOC.SelectedValue.ToString()),
            dtFrom.ToString("yyyy/MM/dd"),
            dtTo.ToString("yyyy/MM/dd"),
            txtSearchText.Text);

        return dtFilters;
    }
    /// <summary>
    /// Check is made so that only Onboard Crew should have From & To date filter
    /// </summary>
    protected void ddlMainStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CalculatedStatus(int.Parse(ddlMainStatus.SelectedValue));
        if (!ddlMainStatus.SelectedValue.ToString().Equals(hdOnBoardStatusId.Value))
        {
            txtSearchJoinFromDate.Text = "";
            txtSearchJoinToDate.Text = "";
            txtSearchJoinFromDate.Enabled = false;
            txtSearchJoinToDate.Enabled = false;
        }
        else
        {
            txtSearchJoinFromDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.AddYears(-5).ToString());
            txtSearchJoinToDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.ToString());
            txtSearchJoinFromDate.Enabled = true;
            txtSearchJoinToDate.Enabled = true;
        }
    }

    public string ConvertToDefaultDt(string Date)
    {
        if (Date != "")
        {
            try
            {

                if (UDFLib.GetDateFormat().ToLower() == "mm-dd-yyyy")
                {
                    Date = (Date.Split('-')[2] + "-" + Date.Split('-')[0] + "-" + Date.Split('-')[1]);
                }

                DateTime.Parse(Date);
                DateTime dt = Convert.ToDateTime(Date);
                return String.Format("{0:yyyy/MM/dd}", dt);


            }
            catch (Exception)
            {
                return "";
            }
        }
        else
            return "";
    }

    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temporary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}