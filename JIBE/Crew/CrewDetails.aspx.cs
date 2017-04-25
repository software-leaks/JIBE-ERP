using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;
using System.IO;

using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.DMS;
using SMS.Business.PortageBill;
using System.Xml;
using AjaxControlToolkit4;
using System.Text.RegularExpressions;




public partial class Crew_CrewDetails : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_Infra_Country objInfraBLL = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();
    BLL_Infra_Port objPort = new BLL_Infra_Port();
    int i = 1;
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    string FooterText = "";
    bool Eval_Due = false;
    protected decimal TotalAmount;
    DataView dtvSal;
    public string DFormat = "";
    public string CID = "", HOST = "";
    public string TodayDateFormat = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            TodayDateFormat = UDFLib.DateFormatMessage();

            HOST = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/crew/crewdetails.aspx"));

            DFormat = UDFLib.GetDateFormat();
            CalendarExtender11.Format = CalendarExtender5.Format = CalendarExtender1.Format = DFormat;
            CalendarExtender5.EndDate = DateTime.Now;

            string ClientBrowser = Request.UserAgent;

            int CrewID = GetCrewID();
            CID = CrewID.ToString();
            if (CrewID == 0)
                Response.Redirect("~/default.aspx?msgid=2");

            HiddenField_CrewID.Value = GetCrewID().ToString();
            HiddenField_UserID.Value = GetSessionUserID().ToString();
            HiddenField_PhotoUploadPath.Value = Server.MapPath("../Uploads/CrewImages");
            HiddenField_DocumentUploadPath.Value = Server.MapPath("../Uploads/CrewDocuments");



            string parameter = Request["__EVENTARGUMENT"];

            if (!IsPostBack)
            {
                UserAccessValidation();
                BindUnion();
                BindUnionBook();
                BindPermanentStatus();
                Load_RankList(ddlRankAppliedFor);
                Load_CrewPersonalDetails(CrewID);
                ddlUnion_SelectedIndexChanged(sender, e);
                Load_CrewCardStatus(CrewID);
                pnlView_PersonalDetails.Visible = true;
                BindVesselTypes(CrewID);
                if (Request.QueryString["Tab"] != null)
                {
                    if (Request.QueryString["Tab"].ToString() != "")
                        HiddenField_SelectTab.Value = Request.QueryString["Tab"].ToString();
                    else
                        HiddenField_SelectTab.Value = "3";
                }
            }

            if (parameter == "PD")
            {
                Load_CrewPersonalDetails(GetCrewID());
            }
            this.Title = hdnPageTitle.Value;
            lblMessagePersonalDetails.Text = "";

            btnUndoRejection.Visible = false;
            if (Session["UTYPE"] != null && !Session["UTYPE"].Equals("MANNING AGENT"))
            {
                /// btnUndoRejection: Visible only to Office User if Crew is rejected. To undo the rejection 
                /// Get_RejectedCount() : To check Crew's Approval Status 
                int RejectedCount = objBLLCrew.Get_RejectedCount(CrewID);
                if (RejectedCount > 0)
                    btnUndoRejection.Visible = true;
            }
            if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
            {
                ImgBtnPlanInterview.Visible = false;
            }
            ImgBtnRecommendation.Visible = false;
            if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
            {
                /// Get_RejectedInterviewCount_MA() : Checks if Manning Agent has rejected crew 
                /// If rejected ImgBtnRecommendation button will be visible to revert back rejection. Visible only to Manning Agent
                int RejectedInterviewCount = objBLLCrew.Get_RejectedInterviewCount_MA(CrewID);
                if (RejectedInterviewCount > 0)
                    ImgBtnRecommendation.Visible = true;
            }
            ConfidentialityCheck();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        string pageNOKURL = "CREW/CREWDETAILS_NOK.ASPX";
        UserAccess objUANOK = new UserAccess();
        objUANOK = objUser.Get_UserAccessForPage(CurrentUserID, pageNOKURL);

        if (objUANOK.Add == 0)
        {
            ImgAddNewDependent.Visible = false;
            ImgReloadDependents.Visible = false;
        }
        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgAddNewDependent.Visible = false;
            btnUnplannedEval.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            lnkEditPersonalDetails.Visible = false;
            imgCrewPic.Enabled = false;
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0 || (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT")))
        {
            lnkNTBR.Visible = false;
            ImgBtnCrewApproval.Visible = false;
            btnUndoRejection.Visible = false;
            imgBtnBrief.Visible = false;
        }
        else
        {
            lnkNTBR.Visible = true;
            lnkNTBR.Attributes.Add("onclick", "EditCrewPrifileStatus(" + GetCrewID().ToString() + ")");

            ImgBtnCrewApproval.Visible = true;
            btnUndoRejection.Visible = true;
            imgBtnBrief.Visible = true;
        }
        if (objUA.Admin == 0)
        {
            lnkResetCrewPassword.Visible = false;
        }
    }

    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    public int GetSessionUserID()
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

    private void Load_CountryList()
    {
        try
        {
            if (ddlPD_Nationality.Items.Count == 0)
            {
                BLL_Infra_Country objCountry = new BLL_Infra_Country();

                ddlPD_Nationality.DataSource = objCountry.Get_CountryList();
                ddlPD_Nationality.DataTextField = "COUNTRY";
                ddlPD_Nationality.DataValueField = "ID";
                ddlPD_Nationality.DataBind();
                ddlPD_Nationality.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch { }
    }
    public void Load_ManningAgentList()
    {
        try
        {
            if (ddlManningOffice.Items.Count == 0)
            {
                BLL_Infra_Company objComp = new BLL_Infra_Company();
                ddlManningOffice.DataSource = objBLLCrew.Get_ManningAgentList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));
                ddlManningOffice.DataTextField = "COMPANY_NAME";
                ddlManningOffice.DataValueField = "ID";
                ddlManningOffice.DataBind();

                ddlManningOffice.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch { }
    }
    public void Load_RankList(DropDownList ddlRank)
    {
        if (ddlRank.Items.Count == 0)
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dt = objCrewAdmin.Get_RankList();

            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "Rank_Short_Name";
            ddlRank.DataValueField = "ID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }

    protected void Load_CrewPersonalDetails(int ID)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
            if (dt.Rows.Count > 0)
            {

                lblSurname.Text = dt.Rows[0]["Staff_Surname"].ToString();
                if (dt.Rows[0]["Staff_Surname"].ToString().Length > 35)
                {
                    lblSurname.Text = dt.Rows[0]["Staff_Surname"].ToString().Substring(0, 35) + "...";
                    lblSurname.Attributes.Add("rel", dt.Rows[0]["Staff_Surname"].ToString());
                }
                lblGivenName.Text = dt.Rows[0]["Staff_Name"].ToString();
                lblGivenName.Attributes.Remove("rel");
                if (dt.Rows[0]["Staff_Name"].ToString().Length > 35)
                {
                    lblGivenName.Text = dt.Rows[0]["Staff_Name"].ToString().Substring(0, 35) + "...";
                    lblGivenName.Attributes.Add("rel", dt.Rows[0]["Staff_Name"].ToString());
                }
                lblAppliedRank.Text = dt.Rows[0]["Rank_Applied_Name"].ToString();
                lblCode.Text = dt.Rows[0]["Staff_Code"].ToString();
                lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
                lblMail.Text = dt.Rows[0]["Email"].ToString();
                if (dt.Rows[0]["HireDate"].ToString() != "")
                    lblHireDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["HireDate"].ToString()));
                if (dt.Rows[0]["Staff_Birth_Date"].ToString() != "")
                    lblDateOfBirth.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Staff_Birth_Date"].ToString()));
                if (ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()) != null)
                {
                    lblUnion.Attributes.Remove("rel");
                    if (ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()).Text.Length > 15)
                    {
                        lblUnion.Text = ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()).Text.Substring(0, 12) + "...";
                        lblUnion.Attributes.Add("rel", ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()).Text);
                    }
                    else
                        lblUnion.Text = ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()).Text;
                }

                if (ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()) != null)
                {
                    ddlUnion.SelectedValue = dt.Rows[0]["UnionId"].ToString();
                }
                else
                {
                    ddlUnion.SelectedIndex = 0;
                }
                BindBranch();
                if (lblUnion.Text == "-SELECT-")
                    lblUnion.Text = "";
                if (ddlUnion.SelectedIndex != 0)
                {
                    if (ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()) != null)
                    {
                        ddlUnionBranch.Attributes.Remove("rel");
                        if (ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()).Text.Length > 12)
                        {
                            lblUnionBranch.Text = ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()).Text.Substring(0, 12) + "...";
                            lblUnionBranch.Attributes.Add("rel", ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()).Text);
                        }
                        else
                            lblUnionBranch.Text = ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()).Text;
                    }
                }

                if (lblUnionBranch.Text == "-SELECT-")
                    lblUnionBranch.Text = "";
                if (ddlUnionBook.Items.FindByValue(dt.Rows[0]["UnionBook"].ToString()) != null)
                    lblBook.Text = ddlUnionBook.Items.FindByValue(dt.Rows[0]["UnionBook"].ToString()).Text;
                if (lblBook.Text == "-SELECT-")
                    lblBook.Text = "";
                if (ddlPermamnent.Items.FindByValue(dt.Rows[0]["PermanentStatus"].ToString()) != null)
                    lblPermanent.Text = ddlPermamnent.Items.FindByValue(dt.Rows[0]["PermanentStatus"].ToString()).Text;
                if (lblPermanent.Text == "-SELECT-")
                    lblPermanent.Text = "";
                lblNationality.Text = dt.Rows[0]["Country_Name"].ToString();
                lblTelephone.Text = dt.Rows[0]["Telephone"].ToString();
                /*following link for Email added for jit 8605*/

                lblManningOffice.Text = dt.Rows[0]["Company_Name"].ToString();
                lblVesselTypes.Text = dt.Rows[0]["VesselTypes"].ToString();
                if (dt.Rows[0]["Available_From_Date"].ToString() != "")
                    lblDtAvailablefrom.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Available_From_Date"].ToString()));
                else
                    lblDtAvailablefrom.Text = "";

                if (dt.Rows[0]["PhotoURL"].ToString() != "")
                {
                    if (File.Exists(Server.MapPath("~/Uploads/CrewImages/" + dt.Rows[0]["PhotoURL"].ToString())))
                    {
                        imgCrewPic.ImageUrl = "~/Uploads/CrewImages/" + dt.Rows[0]["PhotoURL"].ToString();
                        imgCrewPic.Attributes.Add("style", "display:inline;");
                        imgNoPic.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        imgCrewPic.Attributes.Add("style", "display:none");
                        imgNoPic.Attributes.Add("style", "display:inline;");
                    }
                }
                else
                {
                    imgCrewPic.Attributes.Add("style", "display:none");
                    imgNoPic.Attributes.Add("style", "display:inline;");
                }

                lblStaffRank.Text = dt.Rows[0]["CurrentRank"].ToString();
                HiddenField_CurrentRank.Value = dt.Rows[0]["CurrentRankID"].ToString();
                HiddenField_NationalityId.Value = dt.Rows[0]["Country_id"].ToString();
                if (dt.Rows[0]["Crew_Status"].ToString() == "NTBR" || dt.Rows[0]["Crew_Status"].ToString() == "INACTIVE")
                    lblCrewStatus.Text = " - - " + dt.Rows[0]["Crew_Status"].ToString() + " - - ";
                else
                    lblCrewStatus.Text = "";

                if (dt.Rows[0]["Crew_Status"].ToString() == "NTBR")
                    lnkNTBR.Visible = false;

                lblPDLegend.Text = dt.Rows[0]["staff_code"].ToString() + " - " + dt.Rows[0]["Staff_Name"].ToString() + " " + dt.Rows[0]["Staff_Surname"].ToString();

                if (lblPDLegend.Text.Length > 80)
                {
                    lblPDLegend.Text = lblPDLegend.Text.Substring(0, 80) + "...";
                    lblPDLegend.Attributes.Add("rel", "First Name:" + dt.Rows[0]["Staff_Name"].ToString() + "<br/>Surname:" + dt.Rows[0]["Staff_Surname"].ToString());
                }

                hdnPageTitle.Value = "Staff: " + dt.Rows[0]["staff_code"].ToString() + "-" + dt.Rows[0]["Staff_Name"].ToString() + " " + dt.Rows[0]["Staff_Surname"].ToString();

                if (Convert.ToString(dt.Rows[0]["staff_code"]) != "")
                    spnVesselTypeMandatory.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.GetException(ex.Message);
        }

    }
    public void BindVesselTypes(int CrewId)
    {
        try
        {
            DataTable dtVesselType = objBLLCrew.GET_VesselTypeForCrew(CrewId);
            ddlVesselType.DataSource = dtVesselType;
            ddlVesselType.DataTextField = "VesselTypes";
            ddlVesselType.DataValueField = "ID";
            ddlVesselType.DataBind();
            dtVesselType.PrimaryKey = new DataColumn[] { dtVesselType.Columns["VesselTypes"] };
            CheckBoxList chk = ddlVesselType.FindControl("CheckBoxListItems") as CheckBoxList;
            foreach (ListItem chkitem in chk.Items)
            {
                DataRow dr = dtVesselType.Rows.Find(chkitem);
                if (dr["Selected"].ToString() == "1")
                {
                    chkitem.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void Load_CrewPersonalDetails_Edit(int ID)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
            if (dt.Rows.Count > 0)
            {
                txtPD_Surname.Text = dt.Rows[0]["Staff_Surname"].ToString();
                txtPD_Givenname.Text = dt.Rows[0]["Staff_Name"].ToString();
                txtStaffCode.Text = dt.Rows[0]["Staff_Code"].ToString();
                txtMobilePhone.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmailID.Text = dt.Rows[0]["EMail"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["HireDate"].ToString()))
                    txtHireDates.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["HireDate"].ToString()));
                else
                    txtHireDates.Text = "";
                if (ddlUnion.Items.FindByValue(dt.Rows[0]["UnionId"].ToString()) != null)
                {
                    ddlUnion.SelectedValue = dt.Rows[0]["UnionId"].ToString();
                }
                else
                {
                    ddlUnion.SelectedIndex = 0;
                }
                BindBranch();
                if (ddlUnionBook.Items.FindByValue(dt.Rows[0]["unionbook"].ToString()) != null)
                {
                    ddlUnionBook.SelectedValue = dt.Rows[0]["unionbook"].ToString();
                }
                else
                {
                    ddlUnionBook.SelectedIndex = 0;
                }

                if (ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()) != null)
                {
                    ddlUnionBranch.SelectedValue = dt.Rows[0]["UnionBranch"].ToString();
                }
                else
                {
                    ddlUnionBranch.SelectedIndex = 0;
                }
                if (ddlUnionBranch.Items.FindByValue(dt.Rows[0]["UnionBranch"].ToString()) != null)
                {
                    ddlUnionBranch.SelectedValue = dt.Rows[0]["UnionBranch"].ToString();
                }
                else
                {
                    ddlUnionBranch.SelectedIndex = 0;
                }
                if (ddlPermamnent.Items.FindByValue(dt.Rows[0]["PermanentStatus"].ToString()) != null)
                {
                    ddlPermamnent.SelectedValue = dt.Rows[0]["PermanentStatus"].ToString();
                }
                else
                {
                    ddlPermamnent.SelectedIndex = 0;
                }
                txtPD_DOB.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Staff_Birth_Date"].ToString()));


                if (dt.Rows[0]["Staff_Nationality"].ToString() != "" && ddlPD_Nationality.Items.FindByValue(dt.Rows[0]["Staff_Nationality"].ToString()) != null)
                    ddlPD_Nationality.Text = dt.Rows[0]["Staff_Nationality"].ToString();



                if (dt.Rows[0]["Available_From_Date"].ToString() != "")
                    txtDateAvailableFrom.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Available_From_Date"].ToString()));

                if (ddlManningOffice.Items.FindByValue(dt.Rows[0]["ManningOfficeID"].ToString()) != null)
                {
                    ddlManningOffice.Text = dt.Rows[0]["ManningOfficeID"].ToString();
                }
                else
                {
                    ddlManningOffice.Text = "0";
                }

                txtPD_Phone.Text = dt.Rows[0]["Telephone"].ToString();

                Load_RankList(ddlRankAppliedFor);
                if (ddlRankAppliedFor.Items.FindByValue(dt.Rows[0]["AppliedRank_ID"].ToString()) != null)
                {
                    ddlRankAppliedFor.Text = dt.Rows[0]["AppliedRank_ID"].ToString();
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.GetException(ex.Message);
        }

    }

    protected void lnkEditPersonalDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Load_CountryList();
            Load_ManningAgentList();
            Load_CrewPersonalDetails_Edit(GetCrewID());

            pnlEdit_PersonalDetails.Visible = true;
            pnlView_PersonalDetails.Visible = false;
            lnkEditPersonalDetails.Visible = false;
            divCrewAction.Visible = false;
            lblMessagePersonalDetails.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.GetException(ex.Message);
        }


    }
    public void btnOk_Click(object sender, EventArgs e)
    {
        UpdateCrewPersonalDetails();
    }
    protected void btnSavePersonalDetails_Click(object sender, EventArgs e)
    {
        Boolean Valid = ValidatePersonalDetails();
        string response = "";
        CrewProperties objCrw = new CrewProperties();
        try
        {
            if (Valid == true)
            {
                //Vessel Type 
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

                int VesselTypeId = 0, isOnboard = 0;
                if (!lblCode.Text.Equals("") && dtVesselTypes.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select at least one Vessel Type!');", true);
                }
                else
                {
                    if (dtVesselTypes.Rows.Count > 0)
                    {
                        DataTable dtSavedVesselType = objBLLCrew.GET_VesselTypeForCrew(int.Parse(CID));
                        DataTable dtVesselType = objBLLCrew.CheckVesselTypeForCrew(int.Parse(CID), 0);
                        if (dtSavedVesselType != dtVesselType)
                        {
                            if (dtVesselType.Rows.Count > 0)
                            {
                                if (dtVesselType.Rows[0][0].ToString() != "-1") //Crew has no open assignment
                                {
                                    VesselTypeId = int.Parse(dtVesselType.Rows[0][0].ToString());
                                    isOnboard = int.Parse(dtVesselType.Rows[0]["Onboard"].ToString());
                                    dtVesselTypes.DefaultView.RowFilter = "VALUE = " + VesselTypeId;
                                    if (dtVesselTypes.DefaultView.Count == 0)
                                    {
                                        if (isOnboard == 1)
                                            response = dtVesselType.Rows[0]["StaffName"].ToString() + " is currently onboard on " + dtVesselType.Rows[0]["VesselName"].ToString() + " having " + dtVesselType.Rows[0]["VesselTypeName"].ToString() + " as vessel type. Would you like to continue?";
                                        else
                                            response = dtVesselType.Rows[0]["StaffName"].ToString() + " is having a open assignment  on " + dtVesselType.Rows[0]["VesselName"].ToString() + " having " + dtVesselType.Rows[0]["VesselTypeName"].ToString() + " as vessel type. Would you like to continue?";

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "confirmation('" + response + "');", true);
                                    }
                                }
                            }
                        }
                    }
                    if (response.Equals(""))
                    {
                        UpdateCrewPersonalDetails();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessagePersonalDetails.Text = ex.Message;
        }
    }

    public void UpdateCrewPersonalDetails()
    {
        CrewProperties objCrw = new CrewProperties();
        objCrw.CrewID = GetCrewID();
        objCrw.Surname = txtPD_Surname.Text;
        objCrw.GivenName = txtPD_Givenname.Text;

        objCrw.DateOfBirth = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPD_DOB.Text));
        objCrw.Nationality = int.Parse(ddlPD_Nationality.SelectedValue);
        objCrw.Telephone = txtPD_Phone.Text;

        objCrw.ManningOfficeID = int.Parse(ddlManningOffice.SelectedValue);
        objCrw.Available_From_Date = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDateAvailableFrom.Text));
        objCrw.RankID = int.Parse(ddlRankAppliedFor.SelectedValue);
        objCrw.Modified_By = GetSessionUserID();
        if (!string.IsNullOrEmpty(txtHireDates.Text))
            objCrw.HireDate = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtHireDates.Text));
        else
            objCrw.HireDate = null;
        objCrw.Mobile = txtMobilePhone.Text;
        objCrw.EMail = txtEmailID.Text;
        objCrw.UnionID = int.Parse(ddlUnion.SelectedValue);
        objCrw.UnionBook = int.Parse(ddlUnionBook.SelectedValue);
        objCrw.UnionBranch = int.Parse(ddlUnionBranch.SelectedValue);
        objCrw.Permanent = int.Parse(ddlPermamnent.SelectedValue);


        objCrw.Modified_By = GetSessionUserID();


        //Vessel Type 
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

        int result = 0;
        if (!lblCode.Text.Equals("") && dtVesselTypes.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select at least one Vessel Type!');", true);
        }
        else
        {
            result = objBLLCrew.UPDATE_CrewPersonalDetails(objCrw, dtVesselTypes);

            objCrw = null;
            Load_CrewPersonalDetails(GetCrewID());

            pnlEdit_PersonalDetails.Visible = false;
            pnlView_PersonalDetails.Visible = true;
            divCrewAction.Visible = true;
            lblMessagePersonalDetails.Text = "";


            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Personal Details Updated Successfully..!');", true);
            }
            else
            {
                string js = "ShowNotification('Alert','Personal Details Updating Unsuccessful..', true);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
        }
        string jss = "funHitMap();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgs", jss, true);
        lnkEditPersonalDetails.Visible = true;
    }
    protected void btnClosePersonalDetails_Click(object sender, EventArgs e)
    {
        pnlEdit_PersonalDetails.Visible = false;
        pnlView_PersonalDetails.Visible = true;
        divCrewAction.Visible = true;
        lnkEditPersonalDetails.Visible = true;
        lblMessagePersonalDetails.Text = "";
        string jss = "funHitMap();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgs", jss, true);
    }

    protected void ImgAdd_PreJoiningExp_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void ImgReloadPreJoining_Click(object sender, ImageClickEventArgs e)
    {
    }
    /// <summary>
    /// If Crew is rejected ones,cannot be approved again.
    /// This method will undo the rejection so that crew can be approved if required
    /// </summary>
    protected void btnUndoRejection_Click(object sender, EventArgs e)
    {
        try
        {
            if (HiddenField_CrewID.Value != "")
            {
                int returnValue = objBLLCrew.Undo_Rejection(int.Parse(HiddenField_CrewID.Value), GetSessionUserID());
                if (returnValue == 1)
                {
                    btnUndoRejection.Visible = false;
                    string js = "alert('Rejection UNDO Successfully');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void OpenFileinDMS(CommandEventArgs e)
    {

    }

    protected Boolean Validate(string sValue, string sDataType)
    {
        Boolean ret = true;
        string js = "";

        switch (sDataType)
        {
            case "DATETIME":
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(sValue));
                    ret = true;
                }
                catch
                {
                    ret = false;
                    js = "ShowNotification('Alert','Entered value is not a valid Date', true)";
                }

                break;
        }

        if (js != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        return ret;
    }
    protected bool checkSpecialCharacter(string Value_)
    {
        Regex reg = new Regex(@"^[A-Za-z\s]{2,200}$");

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected bool checkEmail(string Value_)
    {
        Regex reg = new Regex(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z][A-Z]{2})?)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected Boolean ValidatePersonalDetails()
    {
        Boolean ret = true;
        string msg = "";
        if (txtPD_Givenname.Text.Trim() == "" || !checkSpecialCharacter(txtPD_Givenname.Text.Trim()))
        {
            ret = false;
            msg += "Enter First Name\\n";
        }
        if (ddlPD_Nationality.SelectedValue == "0")
        {
            ret = false;
            msg += "Select Nationality\\n";
        }
        if (txtPD_DOB.Text.Trim() == "")
        {
            ret = false;
            msg += "Enter Date of Birth\\n";
        }
        if (txtPD_DOB.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPD_DOB.Text));
                if (dt > DateTime.Parse(UDFLib.ConvertToDefaultDt(DateTime.Now.ToString(DFormat))))
                {
                    ret = false;
                    msg += "Date of Birth cannot be future date\\n";
                }
            }
            catch
            {
                ret = false;
                msg += "Enter valid Date of Birth" + TodayDateFormat + "\\n";
            }
        }
        if (txtMobilePhone.Text.Trim() == "")
        {
            ret = false;
            msg += "Enter Mobile Number\\n";
        }

        if (txtEmailID.Text.Trim() == "")
        {
            ret = false;
            msg += "Enter Email Address\\n";
        }
        if (trHireDatetxt.Visible)
        {
            if (txtHireDates.Text.Trim() == "")
            {
                ret = false;
                msg += "Enter Hire Date\\n";
            }
        }
        if (txtDateAvailableFrom.Text.Trim() == "")
        {
            ret = false;
            msg += "Enter Available From Date\\n";
        }
        if (ddlRankAppliedFor.SelectedValue == "0")
        {
            ret = false;
            msg += "Select Applied Rank\\n";
        }
        if (ddlManningOffice.SelectedValue == "0")
        {
            ret = false;
            msg += "Select Manning Office\\n";
        }
        if (ddlUnion.Visible == true)
        {
            if (ddlUnion.SelectedValue == "0")
            {
                ret = false;
                msg += "Select Union\\n";
            }

            else if (ddlUnionBranch.SelectedValue == "0")
            {
                ret = false;
                msg += "Select Union Branch\\n";
            }
            else if (ddlUnionBook.SelectedValue == "0")
            {
                ret = false;
                msg += "Select Union Book\\n";
            }
        }

        if (txtDateAvailableFrom.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtDateAvailableFrom.Text));
                if (dt > DateTime.Today.AddMonths(6))
                {
                    ret = false;
                    msg += "Date of available cannot be more than 6 months!!\\n";
                }
            }
            catch
            {
                ret = false;
                msg += "Enter valid Available From Date" + TodayDateFormat + "\\n";
            }
        }
        if (txtHireDates.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtHireDates.Text));
            }
            catch
            {
                ret = false;
                msg += "Enter valid Hire Date" + TodayDateFormat + "\\n";
            }
        }
        if (msg != "")
        {
            string js = "alert('" + msg + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        return ret;
    }



    /// <summary>
    /// Displays Crew's Card Status ie whether Red/Yellow card is proposed to him or link is provided if needed to propose Red/yellow card
    /// </summary>
    protected void Load_CrewCardStatus(int CrewID)
    {
        if (Session["UTYPE"].ToString() == "OFFICE USER" || Session["UTYPE"].ToString() == "ADMIN")
        {
            DataTable dt = objBLLCrew.Get_CrewCardStatus(CrewID, int.Parse(Session["USERID"].ToString())).Tables[0];
            if (dt.Rows.Count > 0)
            {
                imgCardStatus.ImageUrl = "../images/" + dt.Rows[0]["cardtype"].ToString().Replace(" ", "") + "_" + dt.Rows[0]["cardstatus"].ToString() + ".png";
                imgCardStatus.Visible = true;
                lblCardStatus.Text = dt.Rows[0]["cardtype"].ToString() + " " + dt.Rows[0]["cardstatus"].ToString();
                lblCardStatus.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + dt.Rows[0]["cardtype"].ToString() + " " + dt.Rows[0]["cardstatus"].ToString() + "] body=[<b>Proposed By:</b> " + dt.Rows[0]["proposedby"].ToString() + "<br><b>Reason:</b> " + dt.Rows[0]["proposedremarks"].ToString() + "]");
            }
            else
            {
                lblCardStatus.Text = "<u>Propose Yellow/Red Card</u>";
            }
        }
    }
    protected void lnkResetCrewPassword_Click(object sender, EventArgs e)
    {
        try
        {
            int returnval = objBLLCrew.ResetCrewPassword(GetCrewID(), GetSessionUserID());
            if (returnval == 1)
            {
                string js = "alert('Password Reset Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected int GetRankCategoryID(int RankID)
    {
        int retVal = 0;
        DataTable dt = objCrewAdmin.Get_RankCategoryByRankID(RankID);
        if (dt.Rows.Count > 0)
        {
            retVal = UDFLib.ConvertToInteger(dt.Rows[0]["id"].ToString());
        }
        return retVal;
    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            int CrewID = GetCrewID();
            int UserID = GetSessionUserID();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\CrewDocuments");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);
            int sts = objBLLCrew.Insert_Crew_MatrixAttachment(CrewID, Path.GetFileName(file.FileName), Flag_Attach, UserID);
            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {

        }

    }

    /// <summary>
    /// Visible only to Manning Agent
    /// If crew is rejected by MO then he can be Re-Recommended 
    /// After Re-Recommendation a new interview sheet is schedule for the candidate.Previous interview's status is updated to 0 ie.Active_Status =0
    /// </summary>
    protected void ImgBtnRecommendation_Click(object sender, EventArgs e)
    {
        int CrewID = GetCrewID();
        int returnValue = objBLLCrew.SP_CRW_Recommendation(CrewID, GetSessionUserID());
        if (returnValue == 1)
        {
            ImgBtnRecommendation.Visible = false;
            string js = "GetInterviewResult('" + CrewID + "');alert('New interview is scheduled for this candidate');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }


        if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
        {

            int RejectedInterviewCount = objBLLCrew.Get_RejectedInterviewCount_MA(CrewID);
            if (RejectedInterviewCount > 0)
            {
                ImgBtnRecommendation.Visible = true;
            }
            else
            {
                ImgBtnRecommendation.Visible = false;
            }
        }
    }


    protected void btnRefreshCrewStatus_Click(object sender, EventArgs e)
    {
        try
        {
            Load_CrewPersonalDetails(GetCrewID());
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnRefreshCrewCardStatus_Click(object sender, EventArgs e)
    {
        try
        {
            Load_CrewCardStatus(GetCrewID());
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {

        int CrewID = GetCrewID();
        string js = "GetInterviewResult('" + CrewID + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
        {

            int RejectedInterviewCount = objBLLCrew.Get_RejectedInterviewCount_MA(CrewID);
            if (RejectedInterviewCount > 0)
            {
                ImgBtnRecommendation.Visible = true;
            }
            else
            {
                ImgBtnRecommendation.Visible = false;
            }
        }
    }


    protected void BindUnion()
    {

        ddlUnion.DataSource = objCrewAdmin.CRUD_Union("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlUnion.DataValueField = "ID";
        ddlUnion.DataTextField = "UnionName";
        ddlUnion.DataBind();
        ddlUnion.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindUnionBook()
    {

        ddlUnionBook.DataSource = objCrewAdmin.CRUD_UnionBook("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlUnionBook.DataValueField = "ID";
        ddlUnionBook.DataTextField = "UnionBook";
        ddlUnionBook.DataBind();
        ddlUnionBook.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindPermanentStatus()
    {

        ddlPermamnent.DataSource = objCrewAdmin.CRUD_PermanentStatus("", "R", 0, 0, null, null, null, 1, 100, ref  i, ref i);
        ddlPermamnent.DataValueField = "ID";
        ddlPermamnent.DataTextField = "Status";
        ddlPermamnent.DataBind();
        ddlPermamnent.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindBranch()
    {
        ddlUnionBranch.DataSource = objCrewAdmin.CRUD_UnionBranch("", 0, Convert.ToInt32(ddlUnion.SelectedValue), "", "", "", "", 0, "", "", "", "R", GetSessionUserID(), null, "UnionBranch", null, 1, 100000, ref  i, ref i);
        ddlUnionBranch.DataValueField = "ID";
        ddlUnionBranch.DataTextField = "UnionBranch";
        ddlUnionBranch.DataBind();
        ddlUnionBranch.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void ddlUnion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlUnionBranch.DataSource = objCrewAdmin.CRUD_UnionBranch("", 0, Convert.ToInt32(ddlUnion.SelectedValue), "", "", "", "", 0, "", "", "", "R", GetSessionUserID(), null, "UnionBranch", null, 1, 100000, ref  i, ref i);
            ddlUnionBranch.DataValueField = "ID";
            ddlUnionBranch.DataTextField = "UnionBranch";
            ddlUnionBranch.DataBind();
            ddlUnionBranch.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ConfidentialityCheck()
    {

        DataSet ds = objCrewAdmin.CRW_GetCDConfiguration(null);
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            string str = "";
            DataRow[] dr;
            if (dt.Rows.Count > 0)
            {
                str = "PermanentStatus";
                dr = dt.Select("Key ='" + str + "'");
                if (dr.Length > 0)
                {
                    if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                    {
                        lblPermanent.Visible = true;
                        tdlblPermanent.Visible = true;
                        tdtxtPermanent.Attributes.Add("class", "data");
                        tdtxtPermanentEdit.Visible = true;
                        ddlPermamnent.Visible = true;
                    }
                    else
                    {
                        lblPermanent.Visible = false;
                        tdlblPermanent.Visible = false;
                        tdtxtPermanent.Attributes.Remove("class");
                        tdtxtPermanentEdit.Visible = false;
                        ddlPermamnent.Visible = false;
                    }
                }


                str = "HireDate";
                dr = dt.Select("Key ='" + str + "'");
                if (dr.Length > 0)
                {
                    if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                    {
                        tdHireDatelbl.Visible = true;
                        trHireDatetxt.Visible = true;

                    }
                    else
                    {
                        tdHireDatelbl.Visible = false;
                        trHireDatetxt.Visible = false;
                    }
                }
                str = "UUB";
                dr = dt.Select("Key ='" + str + "'");
                if (dr.Length > 0)
                {
                    if (dr[0].ItemArray[3].ToString() == "True" && dr[0].ItemArray[4].ToString() == "False")
                    {
                        tdUnion.Visible = true;
                        tdlblUnionBook.Visible = true;
                        tdtxtUnionBook.Visible = true;
                        trtxtUnion.Visible = true;
                        tdlblUnionBookEdit.Visible = true;
                        tddrpUnionBookEdit.Visible = true;
                    }
                    else
                    {
                        tdUnion.Visible = false;
                        tdlblUnionBook.Visible = false;
                        tdtxtUnionBook.Visible = false;
                        trtxtUnion.Visible = false;
                        tdlblUnionBookEdit.Visible = false;
                        tddrpUnionBookEdit.Visible = false;
                    }
                }

            }
        }

    }
}