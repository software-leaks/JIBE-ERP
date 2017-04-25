using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Properties;
using System.Web.UI.HtmlControls;

public partial class Crew_RelievePlanning : System.Web.UI.Page
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
        catch { }
    }

   protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        DateFormat = UDFLib.GetDateFormat();//Get User date format
       

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            try
            {
                UserAccessValidation();
                CalendarExtender1.Format = CalendarExtender4.Format = CalendarExtender5.Format = CalendarExtender6.Format = UDFLib.GetDateFormat(); ;

                txtFromDt.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFromDt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtFromDt.Text));
                txtToDt.Text = DateTime.Today.AddMonths(3).ToString("dd/MM/yyyy");
                txtToDt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtToDt.Text));
                txtFromDt_UA.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtFromDt_UA.Text));
                txtToDt_UA.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtToDt_UA.Text));

                Load_RankList(ddlRank);
                Load_Nationality(ddlNationality_SOff);
                Load_FleetList();
                Load_VesselList();
                BindVesselTypes();
                Search_SigningOff(); 
            }
            catch
            { }
        }
       string js = "$('.vesselinfo').InfoBox();";
       ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
    }
    protected int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAssign.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }

    }
    protected void Load_RankList(DropDownList ddl)
    {
        DataTable dt = objCrewAdmin.Get_RankList();

        ddl.DataSource = dt;
        ddl.DataTextField = "Rank_Short_Name";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddl.SelectedIndex = 0;     

        dt = null;
    }
    protected void Load_ManningAgentList()
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
    protected void Load_Nationality(DropDownList ddl)
    {
        DataTable dt = objCrew.Get_CrewNationality(GetSessionUserID());
        ddl.DataSource = dt;
        ddl.DataTextField = "COUNTRY";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddl.SelectedIndex = 0;   
    }

    protected void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;


    }
    protected void Load_VesselList_UA()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel_UA.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);
        ddlVessel_UA.DataTextField = "VESSEL_NAME";
        ddlVessel_UA.DataValueField = "VESSEL_ID";
        ddlVessel_UA.DataBind();
        ddlVessel_UA.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel_UA.SelectedIndex = 0;
    }

    protected void Search_SigningOff()
    {
        try
        {
            int iVesselID = 0;
            if (ddlVessel.Items.Count > 0)
                iVesselID = int.Parse(ddlVessel.SelectedValue);

            int PAGE_SIZE = ucCustomPager_OffSigners.PageSize;
            int PAGE_INDEX = ucCustomPager_OffSigners.CurrentPageIndex;
            int SelectRecordCount = ucCustomPager_OffSigners.isCountRecord;

            DataTable dt = BLL_Crew_CrewList.Get_DetailCrewAssignment_List(int.Parse(ddlFleet.SelectedValue), iVesselID, int.Parse(ddlRank.SelectedValue), int.Parse(ddlNationality_SOff.SelectedValue), UDFLib.ConvertToDefaultDt(txtFromDt.Text), UDFLib.ConvertToDefaultDt(txtToDt.Text), txtFreeText.Text, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

            if (ucCustomPager_OffSigners.isCountRecord == 1)
            {
                ucCustomPager_OffSigners.CountTotalRec = SelectRecordCount.ToString();
                ucCustomPager_OffSigners.BuildPager();
            }

            gvSignOffCrew.DataSource = dt;
            gvSignOffCrew.DataBind();
        }
        catch { }
    }
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
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_VesselList();
        }
        catch {}
    }
    protected void btnFindSignOffCrew_Click(object sender, EventArgs e)
    {
      
        string msg = "";
        if (txtFromDt.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtFromDt.Text));
            }
            catch
            {
                msg += "Enter Valid From Date\\n";
            }
        }
        if (txtToDt.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtToDt.Text));
            }
            catch
            {
                msg += "Enter Valid To Date\\n";
            }
        }
        if (msg != "")
        {
            string js = "alert('" + msg + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            return ;
        }
      
        Search_SigningOff();
    }
    protected void gvSignOffCrew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string DaysLeft = DataBinder.Eval(e.Row.DataItem, "DaysLeft").ToString();
            try
            {
                if (int.Parse(DaysLeft) <= 30)
                {
                    e.Row.Cells[e.Row.Cells.Count - 11].CssClass = "bgPink";
                }
            }
            catch{}
        }
    }
    protected void ddlRank_UA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Search_UnAssigned();
    }

    protected void Search_UnAssigned()
    {
        try
        {
            lblStaffHistory.Text = "";

            int PAGE_SIZE = ucCustomPager_OnSigners.PageSize;
            int PAGE_INDEX = ucCustomPager_OnSigners.CurrentPageIndex;
            int SelectRecordCount = ucCustomPager_OnSigners.isCountRecord;
            int VesselId_OffSignner = int.Parse(lblVesselIdOffsigner.Text);
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
           
            DataTable dt = BLL_Crew_CrewList.Get_UnAssigned_CrewList(int.Parse(ddlManningOffice.SelectedValue), int.Parse(ddlNationality.SelectedValue), int.Parse(ddlRank_UA.SelectedValue), UDFLib.ConvertToDefaultDt(txtFromDt_UA.Text), UDFLib.ConvertToDefaultDt(txtToDt_UA.Text), txtFreeText_UA.Text, int.Parse(ddlVessel_UA.SelectedValue), VesselId_OffSignner, int.Parse(UA_AvailableOptions.SelectedValue), GetSessionUserID(), dtVesselTypes, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);

            if (ucCustomPager_OnSigners.isCountRecord == 1)
            {
                ucCustomPager_OnSigners.CountTotalRec = SelectRecordCount.ToString();
                ucCustomPager_OnSigners.BuildPager();
            }


            gvUnAssignedCrew.DataSource = dt;
            gvUnAssignedCrew.DataBind();

            if (Session["UTYPE"].ToString() == "ADMIN")
            {
                if (dt.Rows.Count == 0)
                {
                    dt = BLL_Crew_CrewList.Get_UnAssigned_CrewList_History(txtFreeText_UA.Text, UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        lblStaffHistory.Text = "<br><br>Please find the below information which can help finding the staff.<br>";
                        foreach (DataRow dr in dt.Rows)
                        {
                            lblStaffHistory.Text += "<br>" + dr[0];
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string js3 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", js3, true);

        }
    }
       
    public void imgbtnAssign_Click(object s, EventArgs e)
    { 
        Load_RankList(ddlRank_UA);
        Load_RankList(ddlJoiningRank);
        Load_ManningAgentList(); 
        Load_Nationality(ddlNationality);
        Load_VesselList_UA();

        ddlVessel_UA.SelectedIndex = 0;
        ddlManningOffice.SelectedIndex = 0;
        ddlNationality.SelectedIndex = 0;
        ddlRank_UA.SelectedIndex = 0;
        txtFromDt_UA.Text = "";
        txtToDt_UA.Text = "";
        txtFreeText_UA.Text = "";
        UA_AvailableOptions.SelectedValue = "1";

        string msgdiv = string.Format("showModal('divUnassigned',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdiv", msgdiv, true);
        UpdatePnlAssign.Update();
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ddlVessel.SelectedIndex = 0;
        ddlFleet.SelectedIndex = 0;
        ddlNationality_SOff.SelectedIndex = 0;
        ddlRank.SelectedIndex = 0;
        lblMessage.Text = "";
        txtFromDt.Text = "";
        txtToDt.Text = "";
        txtFreeText.Text = "";
        Search_SigningOff();
        gvSignOffCrew.SelectedIndex = -1;
        tdSEQ.Visible = false;
        lblSEQ.Text = "";
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        if (VesselID > 0)
        {
            tdSEQ.Visible = true;
            lblSEQ.Text = objCrew.Get_SEQAndONBD(VesselID);
        }
        else
        {
            tdSEQ.Visible = false;
            lblSEQ.Text = "";
        }

    }
  
    protected void btnFindUnAssignedCrew_Click(object sender, EventArgs e)
    {
        Search_UnAssigned();
    }
    protected void btnClearSearchUA_Click(object sender, EventArgs e)
    {
        try
        {
            ddlVessel_UA.SelectedIndex = 0;
            ddlManningOffice.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlRank_UA.SelectedIndex = 0;
            txtFromDt_UA.Text = "";
            txtToDt_UA.Text = "";
            txtFreeText_UA.Text = "";
            UA_AvailableOptions.SelectedValue = "1";

           Search_UnAssigned();
        }
        catch
        {
        }
    }
    protected void gvUnAssignedCrew_PreRender(object sender, EventArgs e)
    {
        if ( int.Parse(UA_AvailableOptions.SelectedValue) == 2)
            gvUnAssignedCrew.Columns[5].HeaderText = "EOC";
        else
            gvUnAssignedCrew.Columns[5].HeaderText = "Readiness";
    }
    protected void gvUnAssignedCrew_RowDataBound(object sender, GridViewRowEventArgs e)
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
                  

                   
                    //ImgInvalid.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Missing Data] body=[" + InvalidText + "]");
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


    protected void GridView1_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        Search_UnAssigned();
    }


    protected void gvSignOffCrew_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int SigningOffCrewId = 0;
        string VesselTypeId ;
        Label lblRankId = ((Label)gvSignOffCrew.Rows[e.NewSelectedIndex].FindControl("lblRankId"));
        Label lblRank = ((Label)gvSignOffCrew.Rows[e.NewSelectedIndex].FindControl("lblRank"));
        Label lblVesselId = ((Label)gvSignOffCrew.Rows[e.NewSelectedIndex].FindControl("lblVesselId"));
        Label lblVesselType = ((Label)gvSignOffCrew.Rows[e.NewSelectedIndex].FindControl("lblVesselType"));

        ddlRank_UA.SelectedValue = lblRankId.Text.ToString();
        ddlRank_UA.SelectedItem.Text = lblRank.Text.ToString();

        ddlJoiningRank.SelectedValue = lblRankId.Text.ToString();
        ddlJoiningRank.SelectedItem.Text = lblRank.Text.ToString();

        lblVesselIdOffsigner.Text = lblVesselId.Text.ToString();
        VesselTypeId = lblVesselType.Text.ToString();
        ddlVesselType.ClearSelection();
        SigningOffCrewId = int.Parse(gvSignOffCrew.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        CheckBoxList chk = ddlVesselType.FindControl("CheckBoxListItems") as CheckBoxList;
        int i = 0;
        foreach (ListItem chkitem in chk.Items)
        {
            if (chkitem.Value == VesselTypeId)
                chkitem.Selected = true;
            i++;
        }
        if (i > 0)
            ddlVesselType.Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white-OnFilterApplied";
        Search_UnAssigned();
    }


    protected void gvSignOffCrew_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        objCrew.Delete_CrewAssignment(int.Parse(e.Keys[3].ToString()), GetSessionUserID());      
        Search_SigningOff();
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string js ="";
            lblMessage.Text = "";

            if (ddlJoiningRank.SelectedValue == "0")
            {
                js = "alert('Select Joining Rank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
            else
            {

                int id1 = 0,id2 = 0,SigningOffVesselId = 0;

                foreach (GridViewRow currentRow in gvUnAssignedCrew.Rows)
                {
                    RadioButton selectButton = (RadioButton)currentRow.FindControl("RowSelector");

                    if (selectButton.Checked)
                    {
                        id2 = int.Parse(((Label)currentRow.FindControl("lblSTAFFID")).Text);
                        break;
                    }
                }
                 
                if (gvSignOffCrew.SelectedValue != null)
                {
                    SigningOffVesselId = int.Parse(gvSignOffCrew.DataKeys[gvSignOffCrew.SelectedIndex]["Vessel_ID"].ToString());
                    id1 = int.Parse(gvSignOffCrew.SelectedValue.ToString());
                }
                if (gvUnAssignedCrew.SelectedValue != null)
                    id2 = int.Parse(gvUnAssignedCrew.SelectedValue.ToString());

                if (id1 == 0 || id2 == 0)
                {
                    js = "alert('Select crew for the assignment');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                }
                else
                {
                    if (id1 == id2)
                    {
                        js = "alert('The Unassigned crew which is selected is same who is signing off');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                    }
                    else
                    {

                        foreach (GridViewRow currentRow in gvUnAssignedCrew.Rows)
                        {
                            RadioButton selectButton1 = (RadioButton)currentRow.FindControl("RowSelector");

                            if (selectButton1.Checked)
                            {
                                id2 = int.Parse(((Label)currentRow.FindControl("lblSTAFFID")).Text);
                                break;
                            }
                        }

                        if (gvSignOffCrew.SelectedValue != null)
                        {
                            SigningOffVesselId = int.Parse(gvSignOffCrew.DataKeys[gvSignOffCrew.SelectedIndex]["Vessel_ID"].ToString());
                        }
                        DataTable dtVesselType = objCrew.CheckVesselTypeForCrew(id2, SigningOffVesselId);
                        if (dtVesselType.Rows.Count > 0 && dtVesselType.Rows[0][0].ToString() == "-1")
                        {
                            rdbVesselTypeAssignmentList.SelectedValue = "1";
                            lblConfirmationTitle.Text = dtVesselType.Rows[0]["StaffName"].ToString() + " does not have the required vessel type assignment.Choose if you want to add " + dtVesselType.Rows[0]["VesselType"].ToString() + " to his vessel type list,or to assign him one time only";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "showVesselType();", true);
                        }
                        else
                        {
                            if (js == "")
                            {
                                Assign(id2);
                            }
                        }
                    }
                } 
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void Assign(int CrewID_UnAssigned)
    {
        int CrewID_SigningOff = 0;
        int Res;
        String js = "";
        if (gvSignOffCrew.SelectedValue != null)
        {
            CrewID_SigningOff = int.Parse(gvSignOffCrew.SelectedValue.ToString());
        }      
        Res = objCrew.ADD_CrewAssignment(CrewID_SigningOff, CrewID_UnAssigned, int.Parse(ddlJoiningRank.SelectedValue), GetSessionUserID());

        if (Res == -2)
        {
            lblMessage.Text = "Assignment can not be done as the ON-SIGNER  has an open assignment without SIGN-ON-DATE";
            js = "alert('Assignment can not be done as the ON-SIGNER  has an open assignment without SIGN-ON-DATE');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", js, true);
        }
        else if (Res == -3)
        {
            lblMessage.Text = "The ON-SIGNER can not join this vessel as this is his first voyage and the ship does not allow new joiners as ratings.";
            js = "alert('The ON-SIGNER can not join this vessel as this is his first voyage and the ship does not allow new joiners as ratings.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", js, true);
        }
        else if (Res == -4)
        {
            lblMessage.Text = "The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel";
            js = "showNationalityApproval();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scr_nationality", js, true);
        }
        else if (Res == -5)
        {
            lblMessage.Text = "The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel";
            js = "showNationalityApproval();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scr_nationality", js, true);
        }
        else
        {
            lblMessage.Text = "Assignment saved for the selected staff.";
            js = "alert('Assignment saved for the selected staff');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser0", js, true);
            Search_SigningOff();
            string msgdiv = string.Format("hideModal('divUnassigned');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdiv", msgdiv, true);
            UpdatePnlAssign.Update();
        }
    }
    protected void rbtnSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int selectedRowIndex = row.RowIndex;
            foreach (GridViewRow currentRow in gvUnAssignedCrew.Rows)
            {
                if (selectButton.Checked)
                {
                    if (currentRow.RowIndex != selectedRowIndex)
                    {
                        if (currentRow.RowType == DataControlRowType.DataRow)
                        {
                            RadioButton radioBtn = ((RadioButton)currentRow.FindControl("RowSelector"));
                            if (radioBtn != null)
                            {
                                radioBtn.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    public void btnAssignVesselType_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselTypeAssignment = int.Parse(rdbVesselTypeAssignmentList.SelectedValue);
            int UnAssignedCrewID = 0, SigningOffVesselId = 0;
            int Result = 0;
            
                if (gvSignOffCrew.SelectedValue != null)
                {
                    SigningOffVesselId = int.Parse(gvSignOffCrew.DataKeys[gvSignOffCrew.SelectedIndex]["Vessel_ID"].ToString());
                }
                foreach (GridViewRow currentRow in gvUnAssignedCrew.Rows)
                {
                    RadioButton selectButton1 = (RadioButton)currentRow.FindControl("RowSelector");

                    if (selectButton1.Checked)
                    {
                        UnAssignedCrewID = int.Parse(((Label)currentRow.FindControl("lblSTAFFID")).Text);
                        break;
                    }
                }
                if (UnAssignedCrewID > 0 && SigningOffVesselId > 0)
                {
                    DataTable dtCrewId = new DataTable();
                    dtCrewId.Columns.Add("CrewId");
                    DataRow dr = dtCrewId.NewRow();
                    dr["CrewId"] = UnAssignedCrewID;
                    dtCrewId.Rows.Add(dr);

                    if (VesselTypeAssignment == 1)
                    {
                        objCrew.CRW_INS_AddVesselTye(dtCrewId, SigningOffVesselId, GetSessionUserID(), ref Result);
                    }
                    Assign(UnAssignedCrewID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "hideVesselType();", true);
                }
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void BindVesselTypes()
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
}