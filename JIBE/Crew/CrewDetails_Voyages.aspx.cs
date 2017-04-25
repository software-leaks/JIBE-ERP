using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_Voyages : System.Web.UI.Page
{
    BLL_Crew_Contract objBLLInfra = new BLL_Crew_Contract();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    UserAccess objUA = new UserAccess();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    int ApproveRights = 0;
    int RankScaleConsidered = 0;
    int NationalityConsidered = 0;
    int Staff_Nationality = 0;
    string CrewStatus;
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
        CalendarExtender1.Format = CalendarExtender2.Format = CalendarExtender3.Format = CalendarExtender4.Format = CalendarExtender6.Format = UDFLib.GetDateFormat();
        string jsDate = "var strDateFormat = '" + UDFLib.GetDateFormat() + "';";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "status", jsDate, true);

        if (Session["USERID"] == null)
        {
            lblMsg.Text = "Session Expired!! Log-out and log-in again.";
        }
        else
        {
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);

            string Mode = Request.QueryString["Mode"];
            string js = "";

            DataTable dtWages = objCrewAdmin.GetWagesSettings();
            if (dtWages != null && dtWages.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == true)
                {
                    RankScaleConsidered = 1;
                    trRankScale.Visible = true;
                }
                else
                {
                    trRankScale.Visible = false;
                }
                if (Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]) == true)
                {
                    NationalityConsidered = 1;
                }
            }

            DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID1(CrewID);
            if (dt != null && dt.Rows.Count > 0)
            {
                Staff_Nationality = int.Parse(dt.Rows[0]["Staff_Nationality"].ToString());
                CrewStatus = dt.Rows[0]["Crew_Status"].ToString();
            }
            if (!IsPostBack)
            {
                ViewState["CrewID"] = CrewID;
                ViewState["VoyID"] = VoyID;
                UserAccessValidation();
                try
                {
                    DataTable DtAttachment = objBLLCrew.Check_Get_AttachedFilePath(CrewID);

                    if (Convert.ToString((DtAttachment.Rows[0]["Result"])) == "1")
                    {
                        Session["FilePath"] = "";
                        string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\CrewDocuments");
                        string AttachmentFilePath = Convert.ToString((DtAttachment.Rows[0]["File_Path"]));
                        AttachmentFilePath = @"~\uploads\CrewDocuments\" + System.IO.Path.GetFileName(AttachmentFilePath);
                        hlnkAttachmentPath.NavigateUrl = AttachmentFilePath;
                        hlnkAttachmentPath.Visible = true;
                    }
                    else
                    {
                        hlnkAttachmentPath.Visible = false;
                    }
                }
                catch (Exception)
                {
                    hlnkAttachmentPath.Visible = false;
                }
                if (dt.Rows.Count > 0)
                {
                    DataTable dtSignOffReason = objBLLCrew.Get_Sign_Off_Reasons();
                    ddlSignOffReason.DataSource = dtSignOffReason;
                    ddlSignOffReason.DataTextField = "Reason";
                    ddlSignOffReason.DataValueField = "Id";
                    ddlSignOffReason.DataBind();
                    ddlSignOffReason.Items.Insert(0, new ListItem("-SELECT -", "0"));

                    if (Convert.ToString(dt.Rows[0]["Staff_Code"]) == "")
                    {
                        lblMsg.Text = "Staff is not yet APPROVED. Please approve the staff to proceed with service creation.";
                        ImgAddVoyage.Visible = false;
                        pnlView_Voyages.Visible = false;
                    }
                    if (Mode == "EDIT_VOY" && objUA.Edit == 1)
                    {
                        pnlAdd_Voyages.Visible = true;
                        Load_FleetList();
                        Load_VesselList();
                        Get_JoiningType();
                        Load_Voyage_Edit(CrewID, VoyID);

                        if (RankScaleConsidered == 1 && ApproveRights == 0)
                        {
                            DDLJoiningRank.Enabled = false;
                            ddlRankScale.Enabled = false;
                        }
                    }
                    else if (Mode == "ADD_VOY" && objUA.Add == 1)
                    {
                        Load_FleetList();
                        Load_VesselList();
                        pnlAdd_Voyages.Visible = true;
                        Get_JoiningType();
                        txtSignOnDate.Enabled = false;

                        Load_ContractList(Staff_Nationality, 0);
                        //User with Approve rights will be able to change Rank & Rank Scale
                        DataTable dtRank = objBLLCrew.Get_GET_CrewRankScale(CrewID);
                        int RankId = int.Parse(dtRank.Rows[0]["RankId"].ToString());
                        int RankScaleId = int.Parse(dtRank.Rows[0]["RankScaleId"].ToString());
                        if (RankId > 0)
                        {
                            DDLJoiningRank.SelectedValue = RankId.ToString();
                            DDLJoiningRank.Enabled = false;
                            if (RankScaleConsidered == 1)
                            {
                                Load_RankScaleList(RankId);
                                if (RankScaleId > 0)
                                {
                                    ddlRankScale.SelectedValue = RankScaleId.ToString();
                                    ddlRankScale.Enabled = false;
                                }
                            }
                        }
                        if (ApproveRights == 1)
                        {
                            DDLJoiningRank.Enabled = true;
                            if (RankScaleConsidered == 1)
                            {
                                ddlRankScale.Enabled = true;
                            }
                        }
                        if (CrewStatus == "INACTIVE")
                        {
                            js = "parent.ShowNotification('Alert','Service cannot be created as crew is Inactive',false);";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                            btnSaveVoyage.Enabled = false;
                        }
                        else if (CrewStatus == "NTBR")
                        {
                            js = "parent.ShowNotification('Alert','Service cannot be created as crew is marked as NTBR',false);";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "status1", js, true);
                            btnSaveVoyage.Enabled = false;
                        }
                        else
                            btnSaveVoyage.Enabled = true;
                    }
                    else if (objUA.View == 1)
                    {
                        pnlView_Voyages.Visible = true;
                        Load_Voyages(CrewID, VoyID);
                    }
                }
                else
                {
                    lblMsg.Text = "Staff's profile details are still not entered.";
                }
            }

        }


    }
    protected void UserAccessValidation()
    {
        if (Session["USERCOMPANYID"] == null)
        {
            Response.Write("Session expired!! Please log out and login again");
            Response.End();
        }
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
        }
        if (objUA.Add == 0)
        {
            pnlAdd_Voyages.Visible = false;
            ImgAddVoyage.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            pnlAdd_Voyages.Visible = false;
            GridView_Voyages.Columns[GridView_Voyages.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Voyages.Columns[GridView_Voyages.Columns.Count - 2].Visible = false;
        }
        if (objUA.Approve == 1)
        {
            ApproveRights = 1;
        }
        //-- MANNING OFFICE LOGIN --
        if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
        {
            GridView_Voyages.Columns[GridView_Voyages.Columns.Count - 6].Visible = false; // salary instruction
        }
        else//--- CREW TEAM LOGIN--
        {
        }


    }
    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_ContractList(int Staff_Nationality, int VesselFlag)
    {
        DataTable dtContract = objBLLInfra.Get_CrewContractList(Staff_Nationality, VesselFlag);

        ddlContract.DataSource = dtContract;
        ddlContract.DataTextField = "Contract_Name";
        ddlContract.DataValueField = "ContractId";
        ddlContract.DataBind();
        ddlContract.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    public void Load_FleetList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    public void Load_VesselList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
        int Vessel_Manager = UserCompanyID;

        ddlVesselList.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVesselList.DataTextField = "VESSEL_NAME";
        ddlVesselList.DataValueField = "VESSEL_ID";
        ddlVesselList.DataBind();
        ddlVesselList.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlVesselList.SelectedIndex = 0;
    }
    public void Load_RankScaleList(int RankId)
    {
        DataTable dt;
        if (NationalityConsidered == 1)
            dt = objCrewAdmin.Get_RankScaleListForWages(RankId, Staff_Nationality);
        else
            dt = objCrewAdmin.Get_RankScaleListForWages(RankId, 0);
        ddlRankScale.DataSource = dt;
        ddlRankScale.DataTextField = "RankScaleName";
        ddlRankScale.DataValueField = "ID";
        ddlRankScale.DataBind();
        ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void ddlVesselList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        int VesselId = int.Parse(ddlVesselList.SelectedValue);
        int VesselFlag = 0;
        DataTable dt1 = objVessel.GetVesselDetails_ByID(VesselId);
        if (dt1.Rows.Count > 0)
        {
            VesselFlag = int.Parse(dt1.Rows[0]["L_Vessel_Flag"].ToString());
        }
        Load_ContractList(Staff_Nationality, VesselFlag);
        Check_Nationality_ForJoiner();
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
    protected void DDLJoiningRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RankID = 0;
        if (DDLJoiningRank.SelectedValue != "0" && DDLJoiningRank.SelectedValue != "")
        {
            RankID = int.Parse(DDLJoiningRank.SelectedValue);
        }
        if (txtJoiningDate.Text != "")
        {
            if (RankID > 0)
            {
                Set_EOC_Date();
            }
        }
        Check_Nationality_ForJoiner();
        if (RankScaleConsidered == 1)
        {
            Load_RankScaleList(RankID);
        }
    }

    protected void Load_Voyages(int CrewID, int VoyID)
    {
        DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, VoyID, GetSessionUserID());
        GridView_Voyages.DataSource = dt;
        GridView_Voyages.DataBind();
    }

    protected void GridView_Voyages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
            string VoyID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();
            string SignOffDate = DataBinder.Eval(e.Row.DataItem, "Sign_Off_Date").ToString();
            string SignOnDate = DataBinder.Eval(e.Row.DataItem, "Sign_On_Date").ToString();
            string EventID_ON = DataBinder.Eval(e.Row.DataItem, "EventID_ON").ToString();
            string Joining_Rank = DataBinder.Eval(e.Row.DataItem, "Joining_Rank").ToString();
            string Joining_Date = DataBinder.Eval(e.Row.DataItem, "Joining_Date").ToString();
            string Rank_category = DataBinder.Eval(e.Row.DataItem, "Rank_category").ToString();
            string NextVoyageID = DataBinder.Eval(e.Row.DataItem, "NextVoyageID").ToString();
            string Pending_Verification = DataBinder.Eval(e.Row.DataItem, "Pending_Verification").ToString();
            string Recommended = DataBinder.Eval(e.Row.DataItem, "Recommended").ToString();
            string Event_Status_ON = DataBinder.Eval(e.Row.DataItem, "Event_Status_ON").ToString();
            string AgreementID = DataBinder.Eval(e.Row.DataItem, "AgreementID").ToString();
            string Agreement_Stage = DataBinder.Eval(e.Row.DataItem, "Agreement_Stage").ToString();
            string Agreement_VerifiedBy = DataBinder.Eval(e.Row.DataItem, "Agreement_VerifiedBy").ToString();
            string Sign_On_Validation = DataBinder.Eval(e.Row.DataItem, "Sign_On_Validation").ToString();
            string SideLetter_Amount = DataBinder.Eval(e.Row.DataItem, "SideLetter_Amount").ToString();


            Image imgCrewTransferPromotion = (Image)e.Row.FindControl("imgCrewTransferPromotion");
            Image imgNewTravelRequest = (Image)e.Row.FindControl("imgNewTravelRequest");
            Image imgViewContract = (Image)e.Row.FindControl("imgViewContract");
            Image imgViewWages = (Image)e.Row.FindControl("imgViewWages");
            Image imgSalaryInstruction = (Image)e.Row.FindControl("imgSalaryInstruction");
            Image imgSideLetter = (Image)e.Row.FindControl("imgSideLetter");
            Label lblSeniority = (Label)e.Row.FindControl("lblSeniority");

            ImageButton lnkEditVoyage = (ImageButton)e.Row.FindControl("lnkEditVoyage");
            ImageButton lnkDeleteVoyage = (ImageButton)e.Row.FindControl("lnkDeleteVoyage");

            if (imgCrewTransferPromotion != null)
            {
                imgCrewTransferPromotion.Attributes.Add("onmouseover", "showTransferLog(event," + CrewID.ToString() + "," + VoyID + "," + GetSessionUserID().ToString() + ")");
                if (SignOnDate != "" && SignOffDate == "")
                    imgCrewTransferPromotion.Attributes.Add("onclick", "javascript:window.open('CrewTransferPromotion.aspx?CrewID=" + CrewID.ToString() + "&VoyID=" + VoyID + "')");
                else
                {
                    imgCrewTransferPromotion.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Transfer Promotion] body=[ Transfer promotion can't be planned for this service.]");
                    imgCrewTransferPromotion.ImageUrl = "../images/transferoff.png";
                }

            }
            if (imgNewTravelRequest != null)
            {
                imgNewTravelRequest.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[New Travel Request] body=[ Click to initiate new travel request.]");
                imgNewTravelRequest.Attributes.Add("onclick", "javascript:window.open('../Travel/NewRequest.aspx?CrewID=" + CrewID.ToString() + "&VoyID=" + VoyID + "')");
            }
            if (imgViewContract != null)
            {
                imgViewContract.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[View Contract] body=[ Click to view or print contracts, upload contract signed by office]");
                imgViewContract.Attributes.Add("onclick", "javascript:window.open('CrewContract.aspx?CrewID=" + CrewID.ToString() + "&VoyID=" + VoyID + "')");
            }
            if (imgViewWages != null)
            {
                imgViewWages.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[View Wages] body=[ Click to add/edit or view wages]");
                imgViewWages.Attributes.Add("onclick", "javascript:window.open('../Portagebill/Crew_AddWages.aspx?CrewID=" + CrewID.ToString() + "&VoyID=" + VoyID + "')");
            }
            if (imgSalaryInstruction != null)
            {
                imgSalaryInstruction.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Salary Instruction] body=[ Click add new salary instruction]");
                imgSalaryInstruction.Attributes.Add("onclick", "AddNewSalaryInstruction(event," + CrewID.ToString() + "," + VoyID + "," + Vessel_ID + ")");
            }

            //lblSeniority
            if (lblSeniority != null)
            {
                lblSeniority.Attributes.Add("onmouseover", "Show_Seniority_Log(event," + CrewID.ToString() + "," + VoyID + "," + GetSessionUserID().ToString() + ")");
                lblSeniority.Attributes.Add("onmouseout", "js_HideTooltip();");
            }

            if (SignOffDate != "" && DataBinder.Eval(e.Row.DataItem, "VesselPortageBillConsidered").ToString() == "True")
            {
                //Staff is signed off
                if (lnkEditVoyage != null)
                    lnkEditVoyage.Visible = false;

                if (lnkDeleteVoyage != null)
                    lnkDeleteVoyage.Visible = false;

                if (imgSalaryInstruction != null)
                    imgSalaryInstruction.Visible = false;

                if (imgNewTravelRequest != null)
                    imgNewTravelRequest.Visible = false;
            }
            if (SignOnDate == "")
            {
                //Voyage created - staff not joined yet
                if (imgSalaryInstruction != null)
                    imgSalaryInstruction.Visible = false;

                // Remove edit button if the voyage is not the next voyage to sign-on
                if (VoyID != NextVoyageID && lnkEditVoyage != null)
                    lnkEditVoyage.Visible = false;

            }
            else
            {
                //Staff is signed-on
                if (lnkDeleteVoyage != null && DataBinder.Eval(e.Row.DataItem, "VesselPortageBillConsidered").ToString() == "True")
                    lnkDeleteVoyage.Visible = false;
            }

            LinkButton lnkCOCDate = (LinkButton)e.Row.FindControl("lnkCOCDate");
            if (lnkCOCDate != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "COCDate").ToString() == "")
                    lnkCOCDate.Text = "[Edit]";
            }
            Image ImbCOCModified = (Image)e.Row.FindControl("ImgCOCModified");
            if (ImbCOCModified != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "COCRemark").ToString() == "")
                    ImbCOCModified.Visible = false;
                else
                    ImbCOCModified.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[EOC Modified] body=[" + DataBinder.Eval(e.Row.DataItem, "COCRemark").ToString().Replace("\n", "<br>") + "]");
            }
            //   // Remove flight booking icon
            if (Convert.ToString(Session["UTYPE"]) != "OFFICE USER")
            {
                if (imgNewTravelRequest != null)
                    imgNewTravelRequest.Visible = false;
            }
            //Salary Instruction button
            if (Joining_Date == "")
            {
                if (imgSalaryInstruction != null)
                    imgSalaryInstruction.Visible = false;
            }

            //Contract download button
            if (UDFLib.ConvertToInteger(Agreement_Stage) == 2 && Convert.ToString(Session["UTYPE"]) == "MANNING AGENT")
            {
                Image imgContractDownload = (Image)e.Row.FindControl("imgContractDownload");
                if (imgContractDownload != null)
                {
                    imgContractDownload.Visible = true;
                    imgContractDownload.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Download Contract] body=[ Click to download contract]");
                    imgContractDownload.Attributes.Add("onclick", "javascript:window.open('DownloadFile.aspx?AgreementID=" + AgreementID + "')");
                }
                else
                {
                    imgContractDownload.Visible = false;
                }
            }

            // No need to validate voyage
            // txtSignOnDate, imgPendingCount, Sign_On_Validation
            Image imgPendingCount = (Image)e.Row.FindControl("imgPendingCount");
            if (Sign_On_Validation != "")
            {
                if (imgPendingCount != null)
                    imgPendingCount.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Missing Data] body=[" + Sign_On_Validation + "]");
            }
            else
            {
                if (imgPendingCount != null)
                    imgPendingCount.Visible = false;
            }

            //SideLetter_Amount
            if (UDFLib.ConvertToDecimal(SideLetter_Amount) > 0)
            {
                if (imgSideLetter != null)
                {
                    imgSideLetter.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Side Letter] body=[USD " + SideLetter_Amount + "]");

                    if (ApproveRights == 1)
                        imgSideLetter.Attributes.Add("onclick", "javascript:window.open('CrewContract.aspx?CrewID=" + CrewID.ToString() + "&VoyID=" + VoyID + "')");
                }
            }
            else
            {
                if (imgSideLetter != null)
                    imgSideLetter.Visible = false;
            }
        }
    }

    protected void Load_Voyage_Edit(int CrewID, int VoyID)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, VoyID, GetSessionUserID());
            if (dt.Rows.Count > 0)
            {
                DataTable dtCrewDetails = objBLLCrew.Get_CrewPersonalDetailsByID1(CrewID);
                if (dtCrewDetails.Rows.Count > 0)
                {
                    int Staff_Nationality = int.Parse(dtCrewDetails.Rows[0]["Staff_Nationality"].ToString());
                    int VesselFlag = 0;
                    if (dt.Rows[0]["Vessel_Flag"].ToString() != "")
                        VesselFlag = int.Parse(dt.Rows[0]["Vessel_Flag"].ToString());

                    DataTable dtContract = objBLLInfra.Get_CrewContractList(Staff_Nationality, VesselFlag);

                    ddlContract.DataSource = dtContract;
                    ddlContract.DataTextField = "Contract_Name";
                    ddlContract.DataValueField = "ContractId";
                    ddlContract.DataBind();
                    ddlContract.Items.Insert(0, new ListItem("-SELECT-", "0"));

                    ddlContract.SelectedValue = dt.Rows[0]["ContractId"].ToString();
                }

                ddlVesselList.SelectedValue = dt.Rows[0]["Vessel_ID"].ToString();
                DDLJoiningRank.SelectedValue = dt.Rows[0]["Joining_Rank"].ToString();
                ddlJoinType.SelectedValue = dt.Rows[0]["Joining_Type"].ToString();
                JoiningTypeChange();
                if (RankScaleConsidered == 1)
                {
                    Load_RankScaleList(int.Parse(dt.Rows[0]["Joining_Rank"].ToString()));
                    ddlRankScale.SelectedValue = dt.Rows[0]["RankScaleId"].ToString();
                }
                if (dt.Rows[0]["Joining_Date"].ToString() != "")
                {
                    txtJoiningDate.Text = Convert.ToDateTime(dt.Rows[0]["Joining_Date"].ToString(), iFormatProvider).ToString("dd/MM/yyyy");
                    txtJoiningDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtJoiningDate.Text));
                }

                HiddenField_OldContractDate.Value = dt.Rows[0]["Joining_Date"].ToString();

                if (dt.Rows[0]["Sign_On_Date"].ToString() != "")
                {
                    txtSignOnDate.Text = Convert.ToDateTime(dt.Rows[0]["Sign_On_Date"].ToString(), iFormatProvider).ToString("dd/MM/yyyy");
                    txtSignOnDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtSignOnDate.Text));
                }
                if (dt.Rows[0]["DOA_HomePort"].ToString() != "")
                {
                    txtDOAHomePort.Text = Convert.ToDateTime(dt.Rows[0]["DOA_HomePort"].ToString(), iFormatProvider).ToString("dd/MM/yyyy");
                    txtDOAHomePort.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtDOAHomePort.Text));
                }
                if (txtSignOnDate.Text.Trim().Length > 0)
                {
                    txtSignOnDate.Enabled = false;
                    pnlSignOff.Enabled = true;
                    ddlFleet.Enabled = false;
                    ddlVesselList.Enabled = false;
                    ddlContract.Enabled = false;
                    ddlJoinType.Enabled = false;
                    DDLJoiningRank.Enabled = false;
                    ddlRankScale.Enabled = false;
                    txtJoiningDate.Enabled = false;
                    txtCOCDate.Enabled = false;
                }
                else
                {
                    pnlSignOff.Enabled = false;
                    ddlFleet.Enabled = true;
                    ddlVesselList.Enabled = true;
                    ddlContract.Enabled = true;
                    ddlJoinType.Enabled = true;
                    DDLJoiningRank.Enabled = true;
                    ddlRankScale.Enabled = true;
                    txtJoiningDate.Enabled = true;
                    txtCOCDate.Enabled = true;
                }

                DataTable dtJoiningTypeDetails = objCrewAdmin.Get_JoiningType_List(int.Parse(ddlJoinType.SelectedValue.ToString()));
                if (dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "False")
                {
                    pnlSignOff.Enabled = true;
                    txtSignOffDate.Enabled = true;
                    txtSignOnDate.Enabled = true;
                    ddlVesselList.Enabled = false;
                    ddlContract.Enabled = false;
                    txtJoiningDate.Enabled = false;
                    pnlJoiningPort.Enabled = false;
                    txtCOCDate.Enabled = false;
                    pnlSignOffPort.Enabled = false;
                    ddlSignOffReason.Enabled = false;
                    txtMPARef.Enabled = false;
                    txtDOAHomePort.Enabled = false;
                    ddlFleet.Enabled = false;
                }
                else
                {
                    if (txtJoiningDate.Text.Trim().Length > 0 && dt.Rows[0]["Sign_On_Validation"].ToString() == "")
                        txtSignOnDate.Enabled = true;
                    else
                        txtSignOnDate.Enabled = false;
                }
                ctlJoiningPort.SelectedValue = dt.Rows[0]["Joining_Port"].ToString();

                if (dt.Rows[0]["COCDate"].ToString() != "")
                {
                    txtCOCDate.Text = Convert.ToDateTime(dt.Rows[0]["COCDate"].ToString(), iFormatProvider).ToString("dd/MM/yyyy");
                    txtCOCDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtCOCDate.Text));
                }
                if (dt.Rows[0]["Sign_Off_Date"].ToString() != "")
                {
                    txtSignOffDate.Text = Convert.ToDateTime(dt.Rows[0]["Sign_Off_Date"].ToString(), iFormatProvider).ToString("dd/MM/yyyy");
                    txtSignOffDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtSignOffDate.Text));
                }


                ctlSignOffPort.SelectedValue = dt.Rows[0]["Sign_Off_Port"].ToString();
                ddlSignOffReason.SelectedValue = dt.Rows[0]["Sign_Off_Reason"].ToString();
                txtMPARef.Text = dt.Rows[0]["MPA_Ref"].ToString();

                // No Need to validate for XT
                if (dt.Rows[0]["Sign_On_Validation"].ToString() != "")
                {
                    txtSignOnDate.Enabled = false;
                    imgSignOn.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Missing Data] body=[" + dt.Rows[0]["Sign_On_Validation"].ToString() + "]");
                    imgSignOn.Visible = true;
                    lblMsg.Text = "There are mandatory documents missing for this service!!";
                }
                else
                {
                    imgSignOn.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void txtJoiningDate_TextChanged(object sender, EventArgs e)
    {
        if (DDLJoiningRank.SelectedValue != "0" && DDLJoiningRank.SelectedValue != "")
        {
            if (txtJoiningDate.Text != "")
            {
                Set_EOC_Date();
            }
        }
    }
    protected void Set_EOC_Date()
    {
        int RankID = int.Parse(DDLJoiningRank.SelectedValue);
        int Days = 0;

        try
        {
            DataTable dt = new DataTable();
            dt = objBLLInfra.Get_Crew_Contract_Period_List(RankID);

            if (dt.Rows.Count > 0 && dt.Rows[0]["Days"].ToString() != "")
                Days = int.Parse(dt.Rows[0]["Days"].ToString());

            if (Days > 0)
            {
                txtCOCDate.Text = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtJoiningDate.Text)).AddDays(Days).ToString("dd/MM/yyyy");
                txtCOCDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtCOCDate.Text));
            }
        }
        catch
        {
            string js = "parent.ShowNotification('Alert','Joining Date is not in correct format.',true)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }

    // Interface to find whether date falls in between startdate & enddate

    public interface IRange<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
    }

    public class DateRange : IRange<DateTime?>
    {
        public DateRange(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        public DateTime? Start { get; private set; }
        public DateTime? End { get; private set; }

        public bool Includes(DateTime? value)
        {
            return (Start <= value) && (value <= End);
        }

        public bool Includes(IRange<DateTime?> range)
        {
            return (Start <= range.Start) && (range.End <= End);
        }
    }

    //protected void AddVesselType()
    //{
    //    int Result = 0;
    //    if (objBLLCrew.AddVesselType(UDFLib.ConvertToInteger(ViewState["CrewID"]), int.Parse(ddlVesselList.SelectedValue), GetSessionUserID(), ref Result) > 0)
    //    {
    //        Result = 1;
    //    }
    //}

    public void btnAssignVesselType_Click(object sender, EventArgs e)
    {
        int VesselTypeAssignment = int.Parse(rdbVesselTypeAssignmentList.SelectedValue);
        SaveVoyageDetail(VesselTypeAssignment);
    }
    public void SaveVoyageDetail(int VesselTypeAssignment)
    {
        string js_notify = "";
        int ContractId = 0;
        int RankScaleId = 0;
        if (RankScaleConsidered == 1 && ddlRankScale.Items.Count > 0)
            RankScaleId = int.Parse(ddlRankScale.SelectedValue);
        int PortID = UDFLib.ConvertToInteger(ctlJoiningPort.SelectedValue);
        ContractId = int.Parse(ddlContract.SelectedValue.ToString());
        if (UDFLib.ConvertToInteger(Request.QueryString["VoyID"]) == 0)
        {
            int result = objBLLCrew.INS_CrewVoyages(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]), UDFLib.ConvertToInteger(ddlJoinType.SelectedValue),
                                           UDFLib.ConvertToDefaultDt(txtJoiningDate.Text), UDFLib.ConvertToDefaultDt(txtSignOnDate.Text), UDFLib.ConvertToDefaultDt(txtSignOffDate.Text),
                                            UDFLib.ConvertToInteger(DDLJoiningRank.SelectedValue),
                                            int.Parse(ddlVesselList.SelectedValue),
                                            UDFLib.ConvertToDefaultDt(txtCOCDate.Text),
                                            PortID,
                                            GetSessionUserID(), RankScaleId, ContractId, txtDOAHomePort.Text, VesselTypeAssignment);

            if (result == 1)
            {
                lblMsg.Text = "Record created successfully !!";
                js_notify = "parent.ShowNotification('Alert','Record created successfully !!',true);";
            }
            else if (result == -1)
            {
                lblMsg.Text = "Crew is already in another service. Sign-off the crew from his current service to assign him to the new service.";
                js_notify = "parent.ShowNotification('Alert','Crew is already in another service. Sign-off the crew from his current service to assign him to the new service',true);";
            }
            else if (result == -2)
            {
                lblMsg.Text = "Crew is NOT ALLOWED to join this ship as this is his first service and the ship does not allow new joiners as ratings.";
                js_notify = "parent.ShowNotification('Alert','Crew is NOT ALLOWED to join this ship as this is his first service and the ship does not allow new joiners as ratings',true);";
            }
            else if (result == -3)
            {
                js_notify = "parent.ShowNotification('Alert','Unable to updated service !!Sign-on date should be greater than the last sign-on date.',true);";
            }
            else if (result == -4)
            {
                lblMsg.Text = "Crew is already in another service. Sign-off the crew from his current service to assign him to the new service.";
                js_notify = "parent.ShowNotification('Alert','Crew is already in another service but Sign-on date is not assigned.',true);";
            }
            else if (result == -5)
            {
                lblMsg.Text = "Selected Vessel's, Vessel type is not assigned to this crew .";
                js_notify = "parent.ShowNotification('Alert','Crew is already in another service but Sign-on date is not assigned.',true);";
            }
            else
            {
                lblMsg.Text = "Unable to add new Service/Status !!";
                js_notify = "parent.ShowNotification('Alert','Unable to add new Service/Status !!',true);";
            }

            js_notify = js_notify + "parent.GetCrewVoyages(" + Request.QueryString["CrewID"].ToString() + ");parent.hideModal('dvPopupFrame');parent.hideVessel();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "InsertVoyage", js_notify, true);
        }
        else
        {
            int updateresult = objBLLCrew.UPDATE_CrewVoyages(UDFLib.ConvertToInteger(Request.QueryString["VoyID"]),
                                                                    UDFLib.ConvertToInteger(Request.QueryString["CrewID"]),
                                                                    UDFLib.ConvertToInteger(ddlJoinType.SelectedValue),
                                                                    UDFLib.ConvertToInteger(ddlVesselList.SelectedValue),
                                                                   UDFLib.ConvertToDefaultDt(txtJoiningDate.Text),
                                                                    UDFLib.ConvertToInteger(DDLJoiningRank.SelectedValue),
                                                                    UDFLib.ConvertToInteger(ctlJoiningPort.SelectedValue),
                                                                    UDFLib.ConvertToDefaultDt(txtSignOnDate.Text),
                                                                    UDFLib.ConvertToDefaultDt(txtSignOffDate.Text),
                                                                    UDFLib.ConvertToInteger(ctlSignOffPort.SelectedValue),
                                                                    UDFLib.ConvertToInteger(ddlSignOffReason.SelectedValue),
                                                                    GetSessionUserID(),
                                                                    txtMPARef.Text,
                                                                    txtDOAHomePort.Text, RankScaleId, ContractId, UDFLib.ConvertToDefaultDt(txtCOCDate.Text), VesselTypeAssignment);
            if (updateresult == 1)
            {
                js_notify = "parent.ShowNotification('Alert','Record updated successfully !!',true);";
            }
            else if (updateresult == -1)
            {
                js_notify = "parent.ShowNotification('Alert','Crew is already in another service. Sign-off the crew from his current service to assign him to the new service.',true);";
            }
            else if (updateresult == -2)
            {
                js_notify = "parent.ShowNotification('Alert','Unable to update service !!Sign-on date should be greater than the last sign-on date.',true);";
            }
            else if (updateresult == 0)
            {
                js_notify = "parent.ShowNotification('Alert','Unable to updated service !!', true);";
            }

            if (updateresult == 1 && UDFLib.ConvertDateToNull(HiddenField_OldContractDate.Value) != UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(txtJoiningDate.Text)))
            {
                js_notify += "parent.ShowNotification('Alert','You have changed the contract date. Please update the EOC date accordingly.',true);";
            }
            lblMsg.Text = "";
            js_notify += "hideVessel();parent.GetCrewVoyages(" + Request.QueryString["CrewID"].ToString() + ");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateVoyage", js_notify, true);
        }
    }
    protected void CheckSignOnDate(ref bool validSignOnDate)
    {
        DataTable dt = objBLLCrew.Get_CrewVoyages(UDFLib.ConvertToInteger(ViewState["CrewID"]), 0, GetSessionUserID());
        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DateRange range = new DateRange(UDFLib.ConvertDateToNull(dt.Rows[x]["Sign_On_Date"]), UDFLib.ConvertDateToNull(dt.Rows[x]["Sign_Off_Date"]));
                if (range.Includes(UDFLib.ConvertDateToNull(txtSignOnDate.Text)) == true)
                {
                    string js = "";
                    if (dt.Rows[x]["sign_off_date"].ToString() == null)
                    {
                        js = "parent.ShowNotification('Alert','Crew cannot be onboard in multiple vessels',true)";
                    }
                    else
                    {
                        js = "parent.ShowNotification('Alert','Crew was already in another service',true)";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    validSignOnDate = false;
                }
            }
        }
    }
    protected void btnSaveVoyage_Click(object sender, EventArgs e)
    {
        if (txtCOCDate.Text != "")
        {
            if (!UDFLib.DateCheck(txtCOCDate.Text))
            {
                string js = "alert('Enter valid EOC Date" + UDFLib.DateFormatMessage() + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
        }

        if (txtJoiningDate.Text != "")
        {
            if (!UDFLib.DateCheck(txtJoiningDate.Text))
            {
                string js = "alert('Enter valid Contract Date" + UDFLib.DateFormatMessage() + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
        }
        bool validSignOnDate = true;

        if (!string.IsNullOrEmpty(txtSignOnDate.Text))
        {
            CheckSignOnDate(ref validSignOnDate);
        }
        if (validSignOnDate == true)
        {
            string js = "";
            int ContractId = 0;
            int iValidate = 1;
            ContractId = int.Parse(ddlContract.SelectedValue.ToString());
            if (int.Parse(ddlJoinType.SelectedValue) == 0)
            {
                js = "parent.ShowNotification('Alert','Joining Type is mandatory field.',true)";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                int RankScaleId = 0;
                int Contract_Mandatory = 0;
                if (RankScaleConsidered == 1 && ddlRankScale.Items.Count > 0)
                    RankScaleId = int.Parse(ddlRankScale.SelectedValue);

                DataTable dtJoiningTypeDetails = objCrewAdmin.Get_JoiningType_List(int.Parse(ddlJoinType.SelectedValue.ToString()));
                if ((dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "True"))
                {
                    //Joining type as Voyage
                    if (int.Parse(ddlContract.SelectedValue) == 0 && UDFLib.ConvertToInteger(ddlVesselList.SelectedValue) > 0)
                    {
                        Contract_Mandatory = objCrewAdmin.Check_Contract_Mandatory(int.Parse(DDLJoiningRank.SelectedValue), Staff_Nationality, UDFLib.ConvertToInteger(ddlVesselList.SelectedValue));
                    }

                    if (txtJoiningDate.Text.Trim() == "" || int.Parse(DDLJoiningRank.SelectedValue) == 0 || (Contract_Mandatory == 1 && int.Parse(ddlContract.SelectedValue) == 0) || UDFLib.ConvertToInteger(ddlVesselList.SelectedValue) == 0 || txtCOCDate.Text == "" || (RankScaleConsidered == 1 && ddlRankScale.SelectedIndex == 0))
                    {
                        if (Contract_Mandatory == 1 && int.Parse(ddlContract.SelectedValue) == 0)
                        {
                            if (RankScaleConsidered == 1 && ddlRankScale.SelectedIndex == 0)
                                js = "parent.ShowNotification('Alert','Vessel Name, Contract Template, Joining Rank,Rank Scale, Joining Date and EOC Date are mandatory fields. You might have missed one of the fields.',true)";
                            else
                                js = "parent.ShowNotification('Alert','Vessel Name, Contract Template, Joining Rank, Joining Date and EOC Date are mandatory fields. You might have missed one of the fields.',true)";
                        }
                        else
                        {
                            if (RankScaleConsidered == 1 && ddlRankScale.SelectedIndex == 0)
                                js = "parent.ShowNotification('Alert','Vessel Name,Joining Rank,Rank Scale, Joining Date and EOC Date are mandatory fields. You might have missed one of the fields.',true)";
                            else
                                js = "parent.ShowNotification('Alert','Vessel Name, Joining Rank, Joining Date and EOC Date are mandatory fields. You might have missed one of the fields.',true)";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    }
                    if (js != "")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
                else
                {
                    //Joining type as Status
                    if (int.Parse(DDLJoiningRank.SelectedValue) == 0 || txtSignOnDate.Text.Trim() == "")
                    {
                        if ((dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"].ToString() == "True"))
                        {
                            if (RankScaleConsidered == 1 && ddlRankScale.SelectedIndex == 0)
                                js = "parent.ShowNotification('Alert','Sign-On Date,Joining Rank and Rank Scale are mandatory fields.',true)";
                            else
                                js = "parent.ShowNotification('Alert','Sign-On Date,Joining Rank are mandatory fields.',true)";
                        }
                        else
                        {
                            js = "parent.ShowNotification('Alert','Sign-On Date,Joining Rank are mandatory fields.',true)";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    }
                    else if (RankScaleConsidered == 1 && ddlRankScale.SelectedIndex == 0 && (dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"].ToString() == "True"))
                    {
                        js = "parent.ShowNotification('Alert','Rank Scale is mandatory field.',true)";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    }
                }
                if (js == "")
                {
                    if (ValidateDateFormat() == true)
                    {
                        if (UDFLib.ConvertToInteger(Request.QueryString["VoyID"]) == 0)
                        {
                            if ((dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"].ToString() == "True") ||
                                (dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "True"))
                            {
                                DataTable dtVesselType = objBLLCrew.CheckVesselTypeForCrew(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]), int.Parse(ddlVesselList.SelectedValue));
                                if (dtVesselType.Rows.Count > 0 && dtVesselType.Rows[0][0].ToString() == "-1")
                                {
                                    lblConfirmationTitle.Text = dtVesselType.Rows[0]["StaffName"].ToString() + " does not have the required vessel type assignment.Choose if you want to add " + dtVesselType.Rows[0]["VesselType"].ToString() + " to his vessel type list,or to assign him one time only";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "showNVessel();", true);
                                    iValidate = 0;
                                }
                            }

                        }
                        if (iValidate == 1 && UDFLib.ConvertToInteger(Request.QueryString["VoyID"]) > 0)
                        {
                            if (dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "True")
                            {
                                if (ValidateSignOnDate(txtJoiningDate.Text, txtSignOnDate.Text) == true)
                                    iValidate = 1;
                                else
                                    iValidate = 0;
                            }
                        }
                        if (iValidate == 1)
                        {
                            SaveVoyageDetail(-1);
                        }
                    }
                }
            }
        }
    }
    protected Boolean ValidateDateFormat()
    {
        Boolean ret = true;
        string msg = "";
        if (txtSignOnDate.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSignOnDate.Text.Trim()));
            }
            catch
            {
                ret = false;
                msg = "Sign-On date is not in correct format.";
            }
        }
        if (txtJoiningDate.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtJoiningDate.Text.Trim()));
            }
            catch
            {
                ret = false;
                msg = "Contract date is not in correct format.";
            }
        }
        if (txtSignOffDate.Text.Trim() != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSignOffDate.Text.Trim()));
            }
            catch
            {
                ret = false;
                msg = "Sign-Off date is not in correct format.";
            }
        }
        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgValidation", js, true);
        }
        return ret;
    }
    protected Boolean ValidateSignOnDate(string JoiningDate, string SignOnDate)
    {
        Boolean ret = true;
        string msg = "";

        if (SignOnDate != "" && JoiningDate == "")
        {
            ret = false;
            msg = "Contract Date is mandatory";
        }
        if (SignOnDate != "" && JoiningDate != "" && Convert.ToDateTime(UDFLib.ConvertToDefaultDt(JoiningDate)) > Convert.ToDateTime(UDFLib.ConvertToDefaultDt(SignOnDate)))
        {
            ret = false;
            msg = "Contract Date cannot be greater than Sign On Date";
        }
        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgValidation", js, true);
        }
        return ret;
    }
    protected void btnCloseVoyage_Click(object sender, EventArgs e)
    {
        pnlAdd_Voyages.Visible = false;
        pnlView_Voyages.Visible = true;
        lblMsg.Text = "";

    }

    protected void Get_JoiningType()
    {
        DataTable dt = objCrewAdmin.Get_JoiningType_List(null);
        ddlJoinType.DataSource = dt;
        ddlJoinType.DataBind();
        JoiningTypeChange();
    }
    /// <summary>
    /// Before joining a crew in vessel, Nationality check is done ie how many staff of same nationality can join the vessel.
    /// If specific no.of staff is already on Vessel, approval needs to be taken if that staff can still join the vessel 
    /// </summary>
    protected void Check_Nationality_ForJoiner()
    {
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselList.SelectedValue);
        int JoiningRank = UDFLib.ConvertToInteger(DDLJoiningRank.SelectedValue);

        int NationalityCheck = objBLLCrew.NationalityCheck_NewJoiner(Vessel_ID, CrewID, JoiningRank, GetSessionUserID());

        if (NationalityCheck <= 0)
        {
            btnSaveVoyage.Enabled = false;
            string Vessel_Name = ddlVesselList.SelectedItem.Text;
            string Rank_Name = DDLJoiningRank.SelectedItem.Text;

            hdnAppVesselID.Value = Vessel_ID.ToString();
            hdnAppCrewID.Value = CrewID.ToString();
            hdnAppCurrentRankID.Value = "0";
            hdnAppJoiningRankID.Value = JoiningRank.ToString();

            lblAppVessel.Text = Vessel_Name;
            lblAppRank.Text = Rank_Name;

            lblMsg.Text = "The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel";

            string js = "showNationalityApproval();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);

        }
        else
        {
            btnSaveVoyage.Enabled = true;
            lblMsg.Text = "";
        }

    }
    /// <summary>
    /// A mail is send to Approver to Approve/Reject for Staff with same nationality should be allowed on vessel 
    /// </summary>
    protected void btnNationalityApproval_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnAppVesselID.Value);
        int CrewID = UDFLib.ConvertToInteger(hdnAppCrewID.Value);
        int JoiningRank_ID = UDFLib.ConvertToInteger(hdnAppJoiningRankID.Value);

        int retval = objBLLCrew.NationalityCheck_SendForApproval(Vessel_ID, CrewID, 0, JoiningRank_ID, txtAppRequest.Text, GetSessionUserID(), 0, 0);
        if (retval > 0)
        {
            string js = "alert('Approval has been sent');hideModal('dvNationalityApproval');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);

        }
        if (retval == -1)
        {
            string js2 = "alert('A previous request for approval has been pending for the same');hideModal('dvNationalityApproval');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js2, true);

        }
    }


    protected void txtSignOnDate_TextChanged(object sender, EventArgs e)
    {
        if (txtSignOnDate.Text.Trim().Length > 0)
        {
            try
            {
                var date = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtSignOnDate.Text));
                DataTable dtJoiningTypeDetails = objCrewAdmin.Get_JoiningType_List(int.Parse(ddlJoinType.SelectedValue.ToString()));
                if ((dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "False"))
                    txtJoiningDate.Text = txtSignOnDate.Text;
                pnlSignOff.Enabled = true;
            }
            catch (Exception)
            {

                pnlSignOff.Enabled = false;
                txtSignOnDate.Text = "";
            }
        }
        else
        {
            pnlSignOff.Enabled = false;
        }
    }

    protected void txtSignOffDate_TextChanged(object sender, EventArgs e)
    {
        DataTable dtJoiningTypeDetails = objCrewAdmin.Get_JoiningType_List(int.Parse(ddlJoinType.SelectedValue.ToString()));
        if (txtSignOffDate.Text.Trim().Length > 0)
        {
            try
            {
                var date = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtSignOffDate.Text));
                if ((dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "False"))
                {
                    txtCOCDate.Text = txtSignOffDate.Text;
                    txtDOAHomePort.Text = txtSignOffDate.Text;
                }
            }
            catch (Exception)
            {
                //msg = "Contract date is not in correct format. Please enter in DD/MM/YYYY format";
            }
        }
        else
        {
            if ((dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "False"))
            {
                txtCOCDate.Text = "";
                txtDOAHomePort.Text = "";
            }
        }
    }

    protected void VisibilityByJoiningType(bool visibility)
    {
        ddlContract.Enabled = visibility;
        txtJoiningDate.Enabled = visibility;
        pnlJoiningPort.Enabled = visibility;
        txtCOCDate.Enabled = visibility;
        pnlSignOffPort.Enabled = visibility;
        ddlSignOffReason.Enabled = visibility;
        txtMPARef.Enabled = visibility;
        txtDOAHomePort.Enabled = visibility;
        ddlFleet.Enabled = visibility;
        ddlVesselList.Enabled = visibility;
        string Mode = Request.QueryString["Mode"];

        if (!Mode.Equals("EDIT_VOY"))
        {
            ctlJoiningPort.SelectedValue = "0";
            ctlSignOffPort.SelectedValue = "0";
            ddlContract.SelectedIndex = 0;
            txtJoiningDate.Text = "";
            txtCOCDate.Text = "";
            ddlSignOffReason.SelectedIndex = 0;
            txtMPARef.Text = "";
            txtDOAHomePort.Text = "";
            ddlFleet.SelectedIndex = 0;
            ddlVesselList.SelectedIndex = 0;
        }
    }
    protected void ddlJoinType_SelectedIndexChanged(object sender, EventArgs e)
    {
        JoiningTypeChange();
    }

    /// <summary>
    /// To Bind joining type data
    /// </summary>
    protected void JoiningTypeChange()
    {
        try
        {
            int OfficeVesselId;
            if (Convert.ToInt32(ddlJoinType.SelectedValue) > 0)
            {
                if (RankScaleConsidered == 1)
                {
                    ddlRankScale.SelectedIndex = 0;
                }
                DataTable dtJoiningTypeDetails = objCrewAdmin.Get_JoiningType_List(int.Parse(ddlJoinType.SelectedValue.ToString()));
                if (dtJoiningTypeDetails.Rows.Count > 0)
                {
                    if (dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "False")
                    {
                        pnlSignOff.Enabled = true;
                        VisibilityByJoiningType(false);
                        txtSignOnDate.Enabled = true;
                        txtSignOffDate.Enabled = true;
                        OfficeVesselId = objCrewAdmin.GetOfficeVessel();
                        ddlVesselList.SelectedValue = OfficeVesselId.ToString();
                    }
                    else if (dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"] != null && dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "True")
                    {
                        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                        int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
                        string Mode = Request.QueryString["Mode"];

                        if (Mode.Equals("EDIT_VOY"))
                        {
                            DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, VoyID, GetSessionUserID());
                            if (dt.Rows[0]["Sign_On_Validation"].ToString() == "")
                            {
                                txtSignOnDate.Enabled = true;
                            }
                        }
                        else
                        {
                            txtSignOnDate.Enabled = false;
                        }
                        VisibilityByJoiningType(true);
                        txtSignOnDate.Text = "";
                        pnlSignOff.Enabled = false;
                    }


                    /// Check for rank scale according to Vessel and office portage bill
                    if (dtJoiningTypeDetails.Rows[0]["VesselPortageBillConsidered"].ToString() == "True" || dtJoiningTypeDetails.Rows[0]["OfficePortageBillConsidered"].ToString() == "True")
                        ddlRankScale.Enabled = true;
                    else
                        ddlRankScale.Enabled = false;
                }
            }
            else
            {
                pnlSignOff.Enabled = false;
                VisibilityByJoiningType(true);
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
    protected void lbtn_Attachment_Click(object sender, EventArgs e)
    {

        Response.Write("<script>");
        Response.Write("window.open('" + Session["FilePath"].ToString() + "','_blank', ' fullscreen=yes')");
        //Response.Write("window.open(" + path + ",'_blank')");
        Response.Write("</script>");
    }

    protected void lnkVerified_OnClick(object sender, EventArgs e)
    {

    }
}