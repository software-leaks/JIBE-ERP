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

public partial class Crew_CrewMatrix_Simulation : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    public string DateFormat = "";

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateFormat = UDFLib.GetDateFormat();
            CalendarExtender6.Format = CalendarExtender5.Format = UDFLib.GetDateFormat();

            if (GetSessionUserID() == 0)
            {
                string Host = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/crew/")) + "/";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirecttologin", "window.parent.location = '" + Host + "Account/Login.aspx';", true);
                return;
            }
            else
                UserAccessValidation();

            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                Load_VesselList();
                Load_ManningAgentList();
                Load_RankList(ddlRank_UA);
                Load_Nationality(ddlNationality);
                Load_VesselTypes();
                int RankID = UDFLib.ConvertToInteger(Request.QueryString["RankID"]);
                int CrewId = UDFLib.ConvertToInteger(Request.QueryString["CrewId"]);
                int VesselId = UDFLib.ConvertToInteger(Request.QueryString["VesselId"]);
                string VesselTypeId = "0";
                DataSet ds = new DataSet();
                ds = BLL_Crew_CrewList.Get_VesselTypeForCrewMatrix(VesselId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        VesselTypeId = Convert.ToString(ds.Tables[0].Rows[0]["Vessel_Type"]) == "" ? "N/A" : Convert.ToString(ds.Tables[0].Rows[0]["Vessel_Type"]);
                    }
                }


                //To select the default Vessel type
                CheckBoxList chk = ddlVesselType.FindControl("CheckBoxListItems") as CheckBoxList;
                foreach (ListItem chkitem in chk.Items)
                {
                    if (chkitem.Value == VesselTypeId)
                        chkitem.Selected = true;
                }

                ViewState["RankId"] = RankID;

                ViewState["OffSignerCrewId"] = CrewId;
                ddlRank_UA.SelectedValue = RankID.ToString();

            }
            hdnDefaultRankId.Value = ViewState["RankId"].ToString();
            string js = "$('.vesselinfo').InfoBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
    #region AccessValidation and Session details
    /// <summary>
    /// Get logged in user id
    /// </summary>
    protected int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Depending upon User Access ,Page will be viewed
    /// </summary>
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            dvPageContent.Visible = false;
            lblMessage.Text = "You don't have sufficient privilege to access the requested page.";
        }

    }
    /// <summary>
    /// Get Session value
    /// </summary>
    protected string getSessionString(string SessionField)
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
    #endregion
    #region FillDropDown
    /// <summary>
    /// Fill the Manning Agent dropdown
    /// </summary>
    protected void Load_ManningAgentList()
    {
        int UserCompanyID = 0;
        if (getSessionString("USERCOMPANYID") != "")
        {
            UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
        }

        DataTable dtManningOffice = new DataTable();
        if (Session["dtManningOffice"] == null)
        {
            Session["dtManningOffice"] = dtManningOffice = objCrew.Get_ManningAgentList(UserCompanyID);
        }
        else
            dtManningOffice = (DataTable)Session["dtManningOffice"];

        ddlManningOffice.DataSource = dtManningOffice;
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();
        ddlManningOffice.Items.Insert(0, new ListItem("-SELECT-", "0"));


    }
    /// <summary>
    /// Fill Rank dropdown
    /// </summary>
    protected void Load_RankList(DropDownList ddl)
    {
        DataTable dt = new DataTable();
        if (Session["dtRanks"] == null)
        {
            Session["dtRanks"] = dt = objCrewAdmin.Get_RankList();
        }
        else
        {
            dt = (DataTable)Session["dtRanks"];
        }

        ddl.DataSource = dt;
        ddl.DataTextField = "Rank_Short_Name";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddl.SelectedIndex = 0;

        dt = null;
    }
    /// <summary>
    /// Fill Nationality dropdown
    /// </summary>
    protected void Load_Nationality(DropDownList ddl)
    {
        DataTable dt = new DataTable();
        if (Session["dtNationality"] == null)
        {
            Session["dtNationality"] = dt = objCrew.Get_CrewNationality(GetSessionUserID());
        }
        else
        {
            dt = (DataTable)Session["dtNationality"];
        }
        ddl.DataSource = dt;
        ddl.DataTextField = "COUNTRY";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddl.SelectedIndex = 0;
    }
    /// <summary>
    /// Fill Vessel dropdown
    /// </summary>
    protected void Load_VesselList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel_UA.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);
        ddlVessel_UA.DataTextField = "VESSEL_NAME";
        ddlVessel_UA.DataValueField = "VESSEL_ID";
        ddlVessel_UA.DataBind();
        ddlVessel_UA.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlVessel_UA.SelectedIndex = 0;
    }
    /// <summary>
    /// Fill Vessel Type dropdown
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
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
    #region Grid Events
    protected void gvUnAssignedCrew_PreRender(object sender, EventArgs e)
    {
        if (int.Parse(UA_AvailableOptions.SelectedValue) == 2)
            gvUnAssignedCrew.Columns[5].HeaderText = "EOC";
        else
            gvUnAssignedCrew.Columns[5].HeaderText = "Readiness";
    }
    protected void gvUnAssignedCrew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Company_Name = DataBinder.Eval(e.Row.DataItem, "Company_Name").ToString();
                string InvalidText = DataBinder.Eval(e.Row.DataItem, "InvalidText").ToString();

                if (Company_Name.Length > 25)
                    Company_Name = Company_Name.Substring(0, 23) + "..";

                if (InvalidText.Length > 0)
                {
                    ((RadioButton)e.Row.FindControl("RowSelector")).Visible = false;
                    ImageButton ImgInvalid = (ImageButton)e.Row.FindControl("ImgInvalid");
                    if (ImgInvalid != null)
                    {
                        ImgInvalid.Visible = true;

                        ImgInvalid.Attributes.Add("onMouseOver", "javascript:js_ShowToolTip('<b><u>Missing Documents</u></b><br/> " + InvalidText + "',event,this)");
                    }
                }
                ((Label)e.Row.FindControl("lblVessel_CODE")).Text = Company_Name;
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvUnAssignedCrew_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        Search_UnAssigned();
    }
    #endregion
    #region Click Event
    /// <summary>
    /// Fill grid on filter criteria
    /// </summary>
    protected void btnFindUnAssignedCrew_Click(object sender, EventArgs e)
    {
        Search_UnAssigned();
    }
    /// <summary>
    /// Selected crew is considered for simulation as OnSigner
    /// </summary>


    protected void btnClearSearchUA_Click(object sender, EventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = "Rank_ID";
        ViewState["SORTDIRECTION"] = 0;
        Search_UnAssigned();
    }
    protected void btnSimulate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";

            int OffSignerCrewId = int.Parse(ViewState["OffSignerCrewId"].ToString());
            int OnSignerCrewId = 0;

            foreach (GridViewRow currentRow in gvUnAssignedCrew.Rows)
            {
                RadioButton selectButton = (RadioButton)currentRow.FindControl("RowSelector");

                if (selectButton.Checked)
                {
                    OnSignerCrewId = int.Parse(((Label)currentRow.FindControl("lblSTAFFID")).Text);
                    break;
                }
            }
            if (OnSignerCrewId == 0)
            {
                string js = "alert('Please select crew for simulation');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
            else
            {
                if (OffSignerCrewId == OnSignerCrewId)
                {
                    string js = "alert('The Unassigned crew which is selected is same who is signing off');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                }
                else
                {
                    string RankId = "0";
                    if (ViewState["RankId"] != null)
                        RankId = ViewState["RankId"].ToString();
                    string js = "parent.document.getElementById('frPopupFrame').src='';parent.hideModal('dvPopupFrame');parent.BindCrewGrid(" + RankId + "," + OffSignerCrewId + "," + OnSignerCrewId + "," + ddlRank_UA.SelectedValue + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
    #region Search
    /// <summary>
    /// Fill the grid with Unassigned Crews according to filter 
    /// </summary>
    protected void Search_UnAssigned()
    {
        try
        {
            int PAGE_SIZE = ucCustomPager_UnAssignedCrew.PageSize;
            int PAGE_INDEX = ucCustomPager_UnAssignedCrew.CurrentPageIndex;
            int SelectRecordCount = ucCustomPager_UnAssignedCrew.isCountRecord;
            int VesselId_OffSignner = int.Parse(ddlVessel_UA.SelectedValue.ToString());
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            //selected Vessel Type 
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

            DataTable dt = BLL_Crew_CrewList.Get_UnAssigned_CrewList(int.Parse(ddlManningOffice.SelectedValue), int.Parse(ddlNationality.SelectedValue), int.Parse(ddlRank_UA.SelectedValue), txtFromDt_UA.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtFromDt_UA.Text).ToShortDateString(), txtToDt_UA.Text.Trim() == "" ? "" : UDFLib.ConvertToDate(txtToDt_UA.Text).ToShortDateString(), txtFreeText_UA.Text, int.Parse(ddlVessel_UA.SelectedValue), VesselId_OffSignner, int.Parse(UA_AvailableOptions.SelectedValue), GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection, int.Parse(ddlMinYearOperator.SelectedValue), int.Parse(ddlMinYearsRank.SelectedValue), int.Parse(ddlMinYearsAllTankers.SelectedValue), dtVesselTypes);

            if (ucCustomPager_UnAssignedCrew.isCountRecord == 1)
            {
                ucCustomPager_UnAssignedCrew.CountTotalRec = SelectRecordCount.ToString();
                ucCustomPager_UnAssignedCrew.BuildPager();
            }

            gvUnAssignedCrew.DataSource = dt;
            gvUnAssignedCrew.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", "BindHeight();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion
}