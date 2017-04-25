using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Collections;

using System.Globalization;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Crew;
using SMS.Business.VET;
using System.IO;


public partial class Technical_Job_List_AddNewJob : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSettingforFunctions();

        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");
        UserAccessValidation();

        if (GvObservationList.Rows.Count > 0)
        {
            SetObservationText();
        }
        if (!IsPostBack == true)
        {
            int OFFICE_ID = 0;
            int WORKLIST_ID = 0;
            int VESSEL_ID = 0;

            if (Request.QueryString["OFFID"] != null)
            {
                OFFICE_ID = UDFLib.ConvertToInteger(Request.QueryString["OFFID"].ToString());
            }

            if (Request.QueryString["WLID"] != null)
            {
                WORKLIST_ID = UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString());
            }

            if (Request.QueryString["VID"] != null)
            {
                VESSEL_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());
                BindVetting(VESSEL_ID);
            }

          
           
            Load_FleetList();
            Load_VesselList();
            BindPIC();
            BindJobType();
            BindDeptInOffice();
            BindDeptOnShip();
            BindAssigner();
            BindPSCSIRE();
            BindJobPriority();
            BindNature();
            BindNatureForDropDown();
         

            //}
            txtFollowupDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            if (objBLL.IsVessel(VESSEL_ID) == 1)
                ddlPIC.Enabled = false;
            else
                ddlPIC.Enabled = true;

            //-- Add new Job
            if (OFFICE_ID == 0 && WORKLIST_ID == 0 && VESSEL_ID == 0)
            {
                btnSaveJob.Visible = true;
                btnUpdateJob.Visible = false;
        
                try
                {
                    ddlWLType.SelectedValue = "DEFECT";
                }
                catch (Exception ex)
                {
                    ddlWLType.SelectedIndex = 1;
                    UDFLib.WriteExceptionLog(ex);

                }


                rdoDeferToDrydock.Items[1].Selected = true;
                txtRaisedOn.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtCompletedOn.Enabled = false;
                ImgBtnAddFollowup.Visible = false;
                hplCloseThisJob.Visible = false;
                hplRework.Visible = false;
                pnlRootCause.Visible = false;
                pnlCrewComplaint.Visible = false;
            }
            else if (OFFICE_ID == 0 && WORKLIST_ID == 0 && VESSEL_ID > 0)
            {
                Bindfunction();
                BindSystem_Location();
                BindSubSystem_Location();
                btnSaveJob.Visible = true;
                btnUpdateJob.Visible = false;
                //rdoNCR.Items[1].Selected = true;
              

                ddlVessel.SelectedValue = VESSEL_ID.ToString();
               
                rdoDeferToDrydock.Items[1].Selected = true;
                txtRaisedOn.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtCompletedOn.Enabled = false;
                ImgBtnAddFollowup.Visible = false;
                hplCloseThisJob.Visible = false;
                hplRework.Visible = false;
                pnlRootCause.Visible = false;
                pnlCrewComplaint.Visible = false;
               
            }
            else
            {
                //-- Edit Job
                btnSaveJob.Visible = false;
                btnUpdateJob.Visible = true;
             

                LoadJobToEdit(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

                Load_CrewComplaintsLog();


            }

            if (Request.QueryString["Vetting_ID"] != null)
            {
                ViewState["Vetting_ID"] = UDFLib.ConvertToInteger(Request.QueryString["Vetting_ID"].ToString());              
                ViewState["AssignedBy"] = ddlAssignedBy.Items.FindByText("Vetting").Value;
                if (ViewState["AssignedBy"] != null)
                {
                    ddlAssignedBy.SelectedValue = ViewState["AssignedBy"].ToString();
                }
                pnlVetting.Visible = true;
                pnlVettingJobs.Visible = true;
                pnlVettingObsJobs.Visible = true;

                ddlVetting.SelectedValue = ViewState["Vetting_ID"].ToString();
                ddlWLType.SelectedValue = "NCR";
                ddlWLType.Enabled = false;
                ddlVetting.Enabled = false;
              


            }

            if (Request.QueryString["Question_ID"] != null)
            {
                ViewState["Question_ID"] = UDFLib.ConvertToInteger(Request.QueryString["Question_ID"].ToString());

            }

            if (Request.QueryString["Observation_ID"] != null)
            {
                ViewState["Observation_ID"] = UDFLib.ConvertToInteger(Request.QueryString["Observation_ID"].ToString());

                hdfSelectObsID.Value = ViewState["Observation_ID"].ToString();
                updateVetting.Update();
            }
            if (Request.QueryString["Vetting_ID"] != null && Request.QueryString["Question_ID"] != null && Request.QueryString["Observation_ID"] != null)
            {
                BLL_VET_Index ObjBLLIndx = new BLL_VET_Index();
                DataTable dt = ObjBLLIndx.VET_Get_Observation(UDFLib.ConvertIntegerToNull(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertIntegerToNull(ViewState["Question_ID"].ToString()), UDFLib.ConvertIntegerToNull(ViewState["Observation_ID"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ObsDescription"].ToString().Length > 100)
                    {
                        lblSelectObs.Text = dt.Rows[0]["ObsDescription"].ToString().Substring(1, 100);
                        lblSelectObs.Attributes.Add("onmouseover", "js_ShowToolTip('" + dt.Rows[0]["ObsDescription"].ToString() + "',event,this)");

                    }
                    else
                    {
                        lblSelectObs.Text = dt.Rows[0]["ObsDescription"].ToString();
                        lblSelectObs.Attributes.Add("onmouseover", "js_ShowToolTip('" + dt.Rows[0]["ObsDescription"].ToString() + "',event,this)");
                    }
                }
            }
        }




    }
    /// <summary>
    ///Bind Observation label
    /// </summary>
    protected void SetObservationText()
    {

        for (int i = 0; i <= GvObservationList.Rows.Count - 1 ; i++)
        {
            GridViewRow row = GvObservationList.Rows[i];
            RadioButton rdb = (RadioButton)row.FindControl("rdbSelectObs");

            if (rdb.Checked == true)
            {
                Label lblselect = (Label)row.FindControl("Description");
                lblSelectObs.Text = lblselect.Text;
            }

        }

    }
    protected void BindPSCSIRE()
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        DataSet ds = objBLL.TEC_WL_Get_ActivePSCSIRE();
        if (ds.Tables.Count > 0)
        {
            ddlPSCSIRE.DataSource = ds.Tables[0];
            ddlPSCSIRE.DataTextField = "PSC_SIRE";
            ddlPSCSIRE.DataValueField = "ID";
            ddlPSCSIRE.DataBind();
            ddlPSCSIRE.Items.Insert(0, new ListItem("-Select-", "0"));
        }
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
            Response.Redirect("~/default.aspx?msgid=2");
           
        }
        if (objUA.Edit == 0)
        {
            Response.Redirect("~/default.aspx?msgid=3");
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
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
    /// <summary>
    /// Added by Anjali DT:3-Jun-2016. JIT:9888
    /// To validate Drop down against null values or values not in drop down item list
    /// </summary>
    /// <param name="_dropDown">Drop down to be validated</param>
    /// <param name="_value">Value to be assigned to drop down</param>
    /// <returns>True/False. True : Validation pass || False : validation fail.</returns>
    private bool ValidateDropdownlist(DropDownList _dropDown, string _value)
    {
        if (_value == string.Empty)
        {
            return false;
        }
        if (_dropDown.Items.FindByValue(_value) == null)
        {
            return false;
        }

        return true;
    }
    /// <summary>
    ///  Modified by Anjali DT:3-Jun-2016. JIT:9888
    /// Load all data to respective controls ,when user edit job.
    /// </summary>
    /// <param name="OFFICE_ID">One of the unique key between combination of Vessel ID, Office Id and Worklist ID.</param>
    /// <param name="WORKLIST_ID">One of the unique key between combination of Vessel ID, Office Id and Worklist ID.</param>
    /// <param name="VESSEL_ID">for which vessel job was created.One of the unique key between combination of Vessel ID, Office Id and Worklist ID.</param>
    protected void LoadJobToEdit(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
            if (dtsJobDetails != null && dtsJobDetails.Tables[0].Rows.Count > 0)
            {
                string temp = "";

                int WL_OFFICE_ID = 0;

                hplCloseThisJob.Visible = false;
                hplRework.Visible = false;
                pnlRootCause.Visible = false;

                // check the if Job is verified and ncr job created at office side. verifiy only if job created only office side.

                if (dtsJobDetails.Tables[0].Rows[0]["IsVerified"].ToString() == "0" && dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() != "0" && (dtsJobDetails.Tables[0].Rows[0]["ISVESSEL"].ToString() == "0" || dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() == "NCR"))
                {

                    btnUpdateJob.Visible = true;
                    if (objBLL.Get_Worklist_Access_ByUser(Convert.ToInt32(Session["USERID"]), "CLOSE", dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString()))
                    {
                        hplCloseThisJob.Visible = true;
                    }
                    else
                    {
                        hplCloseThisJob.Visible = true;
                    }

                }

                if (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "")
                {
                    btnUpdateJob.Visible = false;
                    //Modified by Anjali DT:7-Jun-2016 JIT:9929 || To visible Rework button conditionally on rigths ,after job is verified.
                    if (objBLL.Get_Worklist_Access_ByUser(Convert.ToInt32(Session["USERID"]), "REWORK", dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString()))
                    {
                        hplRework.Visible = true;
                    }

                }

                if (dtsJobDetails.Tables[0].Rows.Count > 0)
                {

                    txtOfficeID.Value = dtsJobDetails.Tables[0].Rows[0]["Office_ID"].ToString();
                    hdnWorklistID.Value = dtsJobDetails.Tables[0].Rows[0]["Worklist_ID"].ToString();

                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlFleet, dtsJobDetails.Tables[0].Rows[0]["fleetcode"].ToString()))
                    {
                        ddlFleet.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["fleetcode"].ToString();
                    }
                    else
                    {
                        ddlFleet.SelectedIndex = 0;
                    }

                    ddlFleet.Enabled = false;

                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlVessel, dtsJobDetails.Tables[0].Rows[0]["Vessel_id"].ToString()))
                    {
                        ddlVessel.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["Vessel_id"].ToString();
                    }
                    else
                    {
                        ddlVessel.SelectedIndex = 0;
                    }

                    ddlVessel.Enabled = false;

                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlPIC, dtsJobDetails.Tables[0].Rows[0]["PIC"].ToString()))
                    {
                        ddlPIC.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["PIC"].ToString();
                    }
                    else
                    {
                        ddlPIC.SelectedIndex = 0;
                    }


                    WL_OFFICE_ID = UDFLib.ConvertToInteger(dtsJobDetails.Tables[0].Rows[0]["OFFICE_ID"].ToString());
                    hdnWLOfficeID.Value = WL_OFFICE_ID.ToString();

                    // crew complain visible/disable

                    if ((dtsJobDetails.Tables[0].Rows[0]["Office_ID"].ToString() == "0") && (dtsJobDetails.Tables[0].Rows[0]["ASSIGNOR"].ToString() == "11") && (dtsJobDetails.Tables[0].Rows[0]["VESSEL_ID"].ToString() != "11"))
                        pnlCrewComplaint.Visible = true;
                    else
                        pnlCrewComplaint.Visible = false;


                    if (dtsJobDetails.Tables[0].Rows[0]["ISVESSEL"].ToString() == "1")
                    {
                        ddlPIC.Enabled = false;
                    }

                    txtJobCode.Text = dtsJobDetails.Tables[0].Rows[0]["WLID_DISPLAY"].ToString();

                    if (dtsJobDetails.Tables[0].Rows[0]["DATE_RAISED"].ToString() != "")
                    {
                        temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_RAISED"].ToString()).ToString("dd/MM/yyyy");
                        txtRaisedOn.Text = temp == "01-Jan-1900" ? "" : temp;
                    }
                    txtRaisedOn.Enabled = false;

                    if (dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString() != "")
                    {
                        temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_ESTMTD_CMPLTN"].ToString()).ToString("dd/MM/yyyy");
                        txtExpectedComp.Text = temp == "01-Jan-1900" ? "" : temp;
                    }


                    ddlOnShip.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["DEPT_SHIP"].ToString();
                    ddlInOffice.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["DEPT_OFFICE"].ToString();
                    ddlPSCSIRE.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["PSC_SIRE"].ToString() == "" ? "0" : dtsJobDetails.Tables[0].Rows[0]["PSC_SIRE"].ToString();

                    string CREATED_DATE = dtsJobDetails.Tables[0].Rows[0]["CREATED_DATE"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["CREATED_DATE"].ToString();
                    string MODIFIED_DATE = dtsJobDetails.Tables[0].Rows[0]["MODIFIED_DATE"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["MODIFIED_DATE"].ToString();
                    string VERIFIED_DATE = dtsJobDetails.Tables[0].Rows[0]["VERIFIED_DATE"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["VERIFIED_DATE"].ToString();
                    string COMPLETED_DATE = dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() == "" ? "" : "<br>" + dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString();

                    txtCompletedOn.Text = "";

                    if (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "")
                    {
                        temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString()).ToString("dd-MMM-yyyy");
                        txtCompletedOn.Text = temp == "01-Jan-1900" ? "" : temp;
                        if (txtCompletedOn.Text != "")
                            ImgBtnAddFollowup.Visible = false;
                    }

                    ddlWLType.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString();

                    if (dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() == "NCR")
                    {

                        ddlWLType.Enabled = false;

                        txtCompletedOn.Enabled = false;
                        txtNCRNo.Text = dtsJobDetails.Tables[0].Rows[0]["NCR_NUM"].ToString();
                        txtNCRNo_Year.Text = dtsJobDetails.Tables[0].Rows[0]["NCR_YEAR"].ToString();

                        btnSaveJob.Visible = false;
                        lblCompletedOnCaption.Text = "Closed On :";
                        lblCompletedByCaption.Text = "Closed By :";


                        if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString() != "")
                        {
                            if (VERIFIED_DATE != "")
                            {
                                txtCompletedOn.Text = VERIFIED_DATE.Substring(4).ToString();
                            }
                            lknCompletedBy.Text = dtsJobDetails.Tables[0].Rows[0]["Verified_By_Name"].ToString() + VERIFIED_DATE;
                            lknCompletedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString();
                        }
                        else
                        {
                            lknCompletedBy.Visible = false;
                        }

                        if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString() != "" && dtsJobDetails.Tables[0].Rows[0]["WL_TYPE"].ToString() == "NCR")
                        {
                            pnlRootCause.Visible = true;
                        }

                    }
                    else
                    {
                        ddlWLType.Enabled = true;
                        txtNCRNo.Text = ""; ;
                        txtNCRNo_Year.Text = "";
                        lblCompletedOnCaption.Text = "Completed On :";
                        lblCompletedByCaption.Text = "Completed By :";

                        if (dtsJobDetails.Tables[0].Rows[0]["VESSEL_ID"].ToString().ToUpper() == "11") //office side job shoud display verified date
                        {

                            if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString() != "")
                            {
                                pnlRootCause.Visible = true;
                                if (VERIFIED_DATE != "")
                                {
                                    txtCompletedOn.Text = VERIFIED_DATE.Substring(4).ToString();
                                }

                                lknCompletedBy.Text = dtsJobDetails.Tables[0].Rows[0]["Verified_By_Name"].ToString() + VERIFIED_DATE;
                                lknCompletedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString();
                            }
                            else
                            {
                                lknCompletedBy.Visible = false;
                            }
                        }
                        else  // vessel side job should display date_completed
                        {
                            if (dtsJobDetails.Tables[0].Rows[0]["DATE_COMPLETED"].ToString() != "")
                            {
                                if (COMPLETED_DATE != "")
                                {
                                    txtCompletedOn.Text = COMPLETED_DATE.Substring(4).ToString();
                                }
                                lknCompletedBy.Text = dtsJobDetails.Tables[0].Rows[0]["Modified_By_Name"].ToString() + COMPLETED_DATE;
                                lknCompletedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Modified_By_CrewID"].ToString();
                            }
                            else
                            {
                                lknCompletedBy.Visible = false;
                            }
                        }

                    }

                    if (dtsJobDetails.Tables[0].Rows[0]["DEFER_TO_DD"].ToString() == "0")
                        rdoDeferToDrydock.Items[1].Selected = true;
                    else
                        rdoDeferToDrydock.Items[0].Selected = true;

                    //  Added by Anjali DT:3-Jun-2016. JIT:9888
                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlAssignedBy, dtsJobDetails.Tables[0].Rows[0]["ASSIGNOR"].ToString()))
                    {
                        ddlAssignedBy.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["ASSIGNOR"].ToString();
                    }
                    else
                    {
                        ddlAssignedBy.SelectedIndex = 0;
                    }
                    // Added for vetting
                    if (ddlAssignedBy.SelectedItem.Text == "Vetting")
                    {
                        pnlVetting.Visible = true;
                        BindVetting(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
                     
                    }
                    else
                    {
                        pnlVetting.Visible = false;
                    }

                    uc_ReqRef.FilterText = dtsJobDetails.Tables[0].Rows[0]["Vessel_id"].ToString();
                    uc_ReqRef.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["REQSN_MSG_REF"].ToString();

                    //  Added by Anjali DT:3-Jun-2016. JIT:9888
                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlNature, dtsJobDetails.Tables[0].Rows[0]["CATEGORY_NATURE"].ToString()))
                    {
                        ddlNature.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["CATEGORY_NATURE"].ToString();
                    }
                    else
                    {
                        ddlNature.SelectedIndex = 0;
                    }
                    BindPrimaryByNatureID(Convert.ToInt32(ddlNature.SelectedValue));

                    //  Added by Anjali DT:3-Jun-2016. JIT:9888
                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlPrimary, dtsJobDetails.Tables[0].Rows[0]["CATEGORY_PRIMARY"].ToString()))
                    {
                        ddlPrimary.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["CATEGORY_PRIMARY"].ToString();
                    }
                    else
                    {
                        ddlPrimary.SelectedIndex = 0;
                    }
                    BindSecondaryByPrimaryID(Convert.ToInt32(ddlPrimary.SelectedValue));

                    //  Added by Anjali DT:3-Jun-2016. JIT:9888
                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlSecondary, dtsJobDetails.Tables[0].Rows[0]["CATEGORY_SECONDARY"].ToString()))
                    {
                        ddlSecondary.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["CATEGORY_SECONDARY"].ToString();
                    }
                    else
                    {
                        ddlSecondary.SelectedIndex = 0;
                    }
                    BindMinorBySecondaryID(Convert.ToInt32(ddlSecondary.SelectedValue));

                    //  Added by Anjali DT:3-Jun-2016. JIT:9888
                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlMinorCat, dtsJobDetails.Tables[0].Rows[0]["CATEGORY_MINOR"].ToString()))
                    {
                        ddlMinorCat.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["CATEGORY_MINOR"].ToString();
                    }
                    else
                    {
                        ddlMinorCat.SelectedIndex = 0;
                    }

                    if (dtsJobDetails.Tables[0].Rows[0]["PRIORITY"].ToString() != "")
                        ddlPriority.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["PRIORITY"].ToString();

                    txtDescribe.Text = dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();
                    txtDescribe.Enabled = false;

                    if (dtsJobDetails.Tables[0].Rows[0]["Causes"].ToString() == "")
                        lblCauses.BackColor = System.Drawing.Color.Yellow;
                    else
                        txtCauses.Text = dtsJobDetails.Tables[0].Rows[0]["Causes"].ToString();

                    if (dtsJobDetails.Tables[0].Rows[0]["Correc_Action"].ToString() == "")
                        lblCorrectiveAction.BackColor = System.Drawing.Color.Yellow;
                    else
                        txtCorrectiveAction.Text = dtsJobDetails.Tables[0].Rows[0]["Correc_Action"].ToString();

                    if (dtsJobDetails.Tables[0].Rows[0]["Preven_Action"].ToString() == "")
                        lblPreventiveAction.BackColor = System.Drawing.Color.Yellow;
                    else
                        txtPreventiveAction.Text = dtsJobDetails.Tables[0].Rows[0]["Preven_Action"].ToString();

                    if (dtsJobDetails.Tables[0].Rows[0]["IsVessel"].ToString() == "1")
                    {
                        if (dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() == "0")
                            ImgBtnAddFollowup.Visible = false;
                    }
                    else
                    {
                        ImgBtnAddFollowup.Visible = (txtCompletedOn.Text != "") ? false : true;
                    }

                    //  Added by Anjali DT:3-Jun-2016. JIT:9888
                    // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                    if (ValidateDropdownlist(ddlInspector, dtsJobDetails.Tables[0].Rows[0]["INSPECTOR"].ToString()))
                    {
                        ddlInspector.SelectedValue = dtsJobDetails.Tables[0].Rows[0]["INSPECTOR"].ToString();
                    }
                    else
                    {
                        ddlInspector.SelectedIndex = 0;
                    }


                    if (dtsJobDetails.Tables[0].Rows[0]["INSPECTION_DATE"].ToString() != "")
                    {
                        temp = Convert.ToDateTime(dtsJobDetails.Tables[0].Rows[0]["INSPECTION_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtInspectionDate.Text = temp == "01-Jan-1900" ? "" : temp;
                    }

                    pnlNCR.Visible = false;

                    if (dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString() != "")
                    {
                        lnkVerifiedBy.Text = dtsJobDetails.Tables[0].Rows[0]["Verified_By_Name"].ToString() + VERIFIED_DATE;
                        lnkVerifiedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dtsJobDetails.Tables[0].Rows[0]["Verified_By_CrewID"].ToString();
                    }
                    else
                    {
                        lnkVerifiedBy.Visible = false;
                    }
                }
                //Display the vetting and observation itself
                if (dtsJobDetails.Tables[1].Rows.Count > 0)
                {
                    ddlVetting.SelectedValue = dtsJobDetails.Tables[1].Rows[0]["Vetting_ID"].ToString();
                    BindObservation();     
                    lblSelectObs.Text = dtsJobDetails.Tables[1].Rows[0]["Description"].ToString();
                    txtObersvation.Text = dtsJobDetails.Tables[1].Rows[0]["Description"].ToString();
                    BindObservationJobs(int.Parse(dtsJobDetails.Tables[1].Rows[0]["Observation_ID"].ToString()), "Observation");
                    BindVettingJobs(int.Parse(dtsJobDetails.Tables[1].Rows[0]["Vetting_ID"].ToString()), "Vetting");
                    hdfSelectObsID.Value=dtsJobDetails.Tables[1].Rows[0]["Observation_ID"].ToString();                  
                    SetSelectedRecord();
                    updateVetting.Update();
                }
                //Display the responses related to this observation
                if (dtsJobDetails.Tables[2].Rows.Count > 0)
                {
                    GvObservation.DataSource = dtsJobDetails.Tables[2];
                    GvObservation.DataBind();
                }
                LoadFollowUps(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
                Bindfunction();

                DataTable _functionTable = objBLL.GetJob_Function_Details(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
                if (_functionTable.Rows.Count > 0)
                {
                    if (_functionTable.Rows[0]["Function_ID"].ToString() != "0")
                    {
                        //  Added by Anjali DT:3-Jun-2016. JIT:9888
                        // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                        if (ValidateDropdownlist(ddlFunction, _functionTable.Rows[0]["Function_ID"].ToString()))
                        {
                            ddlFunction.SelectedValue = _functionTable.Rows[0]["Function_ID"].ToString();
                        }
                        else
                        {
                            ddlFunction.SelectedIndex = 0;
                        }

                        BindSystem_Location();
                        DataTable dtSys = objBLL.Set_Exist_SystemLocation(Convert.ToInt32(_functionTable.Rows[0]["Location_ID"].ToString() == "" ? "0" : _functionTable.Rows[0]["Location_ID"].ToString()));

                        if (dtSys.Rows.Count > 0) //Added by Anjali DT : 17-May-2016 JIT: 9610
                        {
                            //  Added by Anjali DT:3-Jun-2016. JIT:9888
                            // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                            if (ValidateDropdownlist(ddlSysLocation, dtSys.Rows[0]["AssginLocationID"].ToString()))
                            {
                                ddlSysLocation.SelectedValue = dtSys.Rows[0]["AssginLocationID"].ToString();
                            }
                            else
                            {
                                ddlSysLocation.SelectedIndex = 0;
                            }
                        }
                        BindSubSystem_Location();

                        DataTable dtSubSys = objBLL.Set_Exist_SubSystemLocation(Convert.ToInt32(_functionTable.Rows[0]["Sub_Location_ID"].ToString() == "" ? "0" : _functionTable.Rows[0]["Sub_Location_ID"].ToString()));

                        if (dtSubSys.Rows.Count > 0) //Added by Anjali DT : 17-May-2016 JIT: 9610
                        {
                            //  Added by Anjali DT:3-Jun-2016. JIT:9888
                            // When value to be assigned to control is null or not available in item list error generated.To avoid error.
                            if (ValidateDropdownlist(ddlSubSystem_location, dtSubSys.Rows[0]["AssginLocationID"].ToString()))
                            {
                                ddlSubSystem_location.SelectedValue = dtSubSys.Rows[0]["AssginLocationID"].ToString();
                            }
                            else
                            {
                                ddlSubSystem_location.SelectedIndex = 0;
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

    protected void Load_FleetList()
    {
        BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objBLLVessel.GetFleetList(UserCompanyID);
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
        BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
        ddlVessel.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, 0, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void BindPIC()
    {
        try
        {
            BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
            int iCompID = int.Parse(Session["USERCOMPANYID"].ToString());

            ddlPIC.DataSource = objBLLUser.Get_UserList(iCompID);
            ddlPIC.DataTextField = "UserName";
            ddlPIC.DataValueField = "UserID";
            ddlPIC.DataBind();
            ddlPIC.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            ddlInspector.DataSource = objBLLUser.Get_UserList(iCompID);
            ddlInspector.DataTextField = "UserName";
            ddlInspector.DataValueField = "UserID";
            ddlInspector.DataBind();
            ddlInspector.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindJobType()
    {
        try
        {
          
            //int iCompID = int.Parse(Session["USERCOMPANYID"].ToString());
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlWLType.DataSource = objBLL.GetAllWorklistType();
            ddlWLType.DataTextField = "Worklist_Type_Display";
            ddlWLType.DataValueField = "Worklist_Type";
            ddlWLType.DataBind();
            ddlWLType.SelectedValue = objBLL.GetAllWorklistType().Tables[0].Rows[0]["Worklist_Type"].ToString();
        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindDeptOnShip()
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlOnShip.DataSource = objBLL.Get_Dept_OnShip();
            ddlOnShip.DataTextField = "value";
            ddlOnShip.DataValueField = "id";
            ddlOnShip.DataBind();
            ddlOnShip.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindDeptInOffice()
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlInOffice.DataSource = objBLL.Get_Dept_InOffice();
            ddlInOffice.DataTextField = "value";
            ddlInOffice.DataValueField = "id";
            ddlInOffice.DataBind();
            ddlInOffice.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindJobPriority()
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlPriority.DataSource = objBLL.Get_JobPriority();
            ddlPriority.DataTextField = "value";
            ddlPriority.DataValueField = "id";
            ddlPriority.DataBind();
            ddlPriority.Items[3].Selected = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindAssigner()
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlAssignedBy.DataSource = objBLL.Get_Assigner();
            ddlAssignedBy.DataTextField = "value";
            ddlAssignedBy.DataValueField = "id";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindNature()
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            DataSet dts = objBLL.GetAllNature();
            if (dts.Tables[0].Rows.Count > 0)
            {
                // Bind the Nature List Box
                lbNature.DataSource = dts;
                lbNature.DataTextField = "Name";
                lbNature.DataValueField = "Code";
                lbNature.DataBind();
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private void BindPrimary(int intNature)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            // Bind the Nature List Box
            DataSet dts = objBLL.GetPrimaryByNatureID(intNature);
            if (dts.Tables[0].Rows.Count > 0)
            {
                lbPrimary.DataSource = dts;
                lbPrimary.DataTextField = "Name";
                lbPrimary.DataValueField = "Code";
                lbPrimary.DataBind();
                lbPrimary.SelectedIndex = 0;

                txtNature.Text = Convert.ToString(lbNature.SelectedItem.Text.ToString());

                BindSecondary(Convert.ToInt32(lbPrimary.SelectedValue)); // Bind the Secondary In Office ComboBox By Primary Id
            }
            else
            {
                txtNature.Text = Convert.ToString(lbNature.SelectedItem.Text.ToString());
                lbPrimary.Items.Clear();
                lbSecondary.Items.Clear();
                txtPrimary.Text = "";
                txtSecondary.Text = "";
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private void BindSecondary(int intPrimary)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            DataSet dts = objBLL.GetSecondaryByPrimaryID(intPrimary);
            if (dts.Tables[0].Rows.Count > 0)
            {
                lbSecondary.DataSource = dts;
                lbSecondary.DataTextField = "Name";
                lbSecondary.DataValueField = "Code";
                lbSecondary.DataBind();
                lbSecondary.SelectedIndex = 0;

                txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
                txtSecondary.Text = Convert.ToString(lbSecondary.SelectedItem.Text.ToString());
            }
            else
            {
                txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
                lbSecondary.Items.Clear();
                txtSecondary.Text = "";
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private void BindMinor(int intSecondary)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            DataSet dts = objBLL.GetMinorBySecondaryID(intSecondary);
            if (dts.Tables[0].Rows.Count > 0)
            {
                lbMinor.DataSource = dts;
                lbMinor.DataTextField = "Name";
                lbMinor.DataValueField = "Code";
                lbMinor.DataBind();
           

                txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
                txtSecondary.Text = Convert.ToString(lbSecondary.SelectedItem.Text.ToString());
                txtMinor.Text = Convert.ToString(lbMinor.SelectedItem.Text.ToString());

            }
            else
            {
                txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
                txtSecondary.Text = Convert.ToString(lbSecondary.SelectedItem.Text.ToString());

                lbMinor.Items.Clear();
                txtMinor.Text = "";
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to get vetting list
    /// </summary>
    protected void BindVetting(int Vessel_ID)
    {
        try
        {
         
            BLL_VET_VettingLib ObjBLLVET = new BLL_VET_VettingLib();
            DataTable dt = ObjBLLVET.VET_Get_VettingList(Vessel_ID);
            if (dt.Rows.Count > 0)
            {
                // Bind the Vetting List 
                ddlVetting.DataSource = dt;
                ddlVetting.DataTextField = "VettingName";
                ddlVetting.DataValueField = "Vetting_ID";
                ddlVetting.DataBind();
                ddlVetting.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to get observation list according to vetting
    /// </summary>
    protected void BindObservation()
    {
        try
        {
            int Vetting_ID = int.Parse(ddlVetting.SelectedValue);
            BLL_VET_VettingLib ObjBLLVET = new BLL_VET_VettingLib();
            DataTable dt = ObjBLLVET.VET_Get_ObservationList(Vetting_ID);
            if (dt.Rows.Count > 0)
            {
                // Bind the Observation List 
            
                GvObservationList.DataSource = dt;
                GvObservationList.DataBind();
                lnkObservationLink.Visible = true;
                lblObservation.Visible = false;
                lblObservation.Text = "";
            } 
            else
            {              
                lnkObservationLink.Visible = false;
                lblObservation.Visible = true;
                lblObservation.Text = "No Observations added.";
            }
            UpdObservation.Update();
            updateVetting.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to select already saved observation.
    /// </summary>
    private void SetSelectedRecord()
    {
        for (int i = 0; i < GvObservationList.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)GvObservationList.Rows[i].Cells[0].FindControl("rdbSelectObs");
            if (rb != null)
            {
                if (hdfSelectObsID != null )
                {
                    if (hdfSelectObsID.Value == rb.Attributes["rel"].ToString())
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Display the jobs that related to this specific observation
    /// </summary>
    /// <param name="FilterJobsBy">Passed observation ID</param>
    /// <param name="Mode">Mode is Observation</param>
    protected void BindObservationJobs(int FilterJobsBy, string Mode)
    {
        try
        {
            BLL_VET_Index ObjBLLIndx = new BLL_VET_Index();
            DataTable dt = ObjBLLIndx.VET_Get_Vetting_Jobs_Details(FilterJobsBy, Mode);
            GvObservationJobs.DataSource = dt;
            GvObservationJobs.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Display all the jobs related to this specific vetting
    /// </summary>
    /// <param name="FilterJobBy">Passed Vetting ID</param>
    /// <param name="Mode">Mode is Vetting</param>
    protected void BindVettingJobs(int FilterJobBy, string Mode)
    {
        try
        {
            BLL_VET_Index ObjBLLIndx = new BLL_VET_Index();
            DataTable dt = ObjBLLIndx.VET_Get_Vetting_Jobs_Details(FilterJobBy, Mode);
            GvVettingJobs.DataSource = dt;
            GvVettingJobs.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindPrimaryByNatureID(Convert.ToInt32(ddlNature.SelectedValue));

    }

    protected void ddlPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindSecondaryByPrimaryID(Convert.ToInt32(ddlPrimary.SelectedValue));

    }

    protected void ddlSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindMinorBySecondaryID(Convert.ToInt32(ddlSecondary.SelectedValue));

    }

    protected void lbNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        BindPrimary(Convert.ToInt32(lbNature.SelectedValue)); // Bind the Primary In Office ComboBox By Nature Id
    }

    protected void lbPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        BindSecondary(Convert.ToInt32(lbPrimary.SelectedValue)); // Bind the Secondary In Office ComboBox By Primary Id
    }

    protected void lbSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        BindMinor(int.Parse(lbSecondary.SelectedValue));
    }

    protected void lbMinor_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        txtMinor.Text = lbMinor.SelectedItem.Text;
    }


    protected void BindNatureForDropDown()
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlNature.DataTextField = "Name";
            ddlNature.DataValueField = "Code";
            ddlNature.DataSource = objBLL.GetAllNature();
            ddlNature.DataBind();
            ddlNature.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlPrimary.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlSecondary.Items.Insert(0, new ListItem("-Select-", "0"));

            if (hdnNature.Value == "0")
            {
                ddlNature.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlNature.SelectedValue = Convert.ToString(hdnNature.Value);
                BindPrimaryByNatureID(Convert.ToInt32(hdnNature.Value));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindPrimaryByNatureID(Int32 i32NatureID)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlSecondary.Items.Clear();

            ddlPrimary.DataTextField = "Name";
            ddlPrimary.DataValueField = "Code";
            ddlPrimary.DataSource = objBLL.GetPrimaryByNatureID(i32NatureID);
            ddlPrimary.DataBind();
            ddlPrimary.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlSecondary.Items.Insert(0, new ListItem("-Select-", "0"));

            if (hdnPrimary.Value == "0")
            {
                ddlPrimary.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlPrimary.SelectedValue = Convert.ToString(hdnPrimary.Value);
                BindSecondaryByPrimaryID(Convert.ToInt32(hdnPrimary.Value));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
         
        }

    }

    protected void BindSecondaryByPrimaryID(Int32 i32PrimaryID)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlSecondary.Items.Clear();
            ddlSecondary.DataTextField = "Name";
            ddlSecondary.DataValueField = "Code";
            ddlSecondary.DataSource = objBLL.GetSecondaryByPrimaryID(Convert.ToInt32(ddlPrimary.SelectedValue));
            ddlSecondary.DataBind();

            ddlSecondary.Items.Insert(0, new ListItem("-Select-", "0"));

            if (hdnSecondary.Value == "0")
            {
                ddlSecondary.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlSecondary.SelectedValue = Convert.ToString(hdnSecondary.Value);
                BindMinorBySecondaryID(Convert.ToInt32(hdnSecondary.Value));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
         
        }
    }

    protected void BindMinorBySecondaryID(Int32 i32SecondaryID)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            ddlMinorCat.Items.Clear();
            ddlMinorCat.DataTextField = "Name";
            ddlMinorCat.DataValueField = "Code";
            ddlMinorCat.DataSource = objBLL.GetMinorBySecondaryID(i32SecondaryID);
            ddlMinorCat.DataBind();

            ddlMinorCat.Items.Insert(0, new ListItem("-Select-", "0"));

            if (hdnMinor.Value == "0")
            {
                ddlMinorCat.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlMinorCat.SelectedValue = Convert.ToString(hdnMinor.Value);
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
          
        }
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        uc_ReqRef.SelectedValue = "0";
        Load_VesselList();
    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            uc_ReqRef.SelectedValue = "0";
            if (UDFLib.ConvertToInteger(ddlVessel.SelectedValue) == 0)
                uc_ReqRef.Enable = false;
            else
            {
                uc_ReqRef.Enable = true;
                uc_ReqRef.FilterText = ddlVessel.SelectedValue;

                reset_ddl();

                Bindfunction();
                

            }

            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            if (objBLL.IsVessel(int.Parse(ddlVessel.SelectedValue)) == 0)
            {
                ddlPIC.Enabled = true;
                ddlPIC.SelectedIndex = 0;

            
                ddlAssignedBy.SelectedIndex = 0;

            }
            else
            {

                ddlPIC.Enabled = false;
                ddlPIC.SelectedIndex = 0;
            }
         
            rdoNCR_SelectedIndexChanged(null, null);
            ddlSysLocation.SelectedIndex = 0;
            ddlSubSystem_location.SelectedIndex = 0;

            if (ddlAssignedBy.SelectedItem.Text == "Vetting")
            {
                pnlVetting.Visible = true;
                BindVetting(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));

            }
            else
            {
                pnlVetting.Visible = false;
            }

        }

        catch(Exception ex) { 
        UDFLib.WriteExceptionLog(ex);
        }

    }

    protected Boolean ValidateForm(string Mode)
    {

        string js = "";
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        if (ddlOnShip.SelectedIndex <= 0)
        {
            js = "alert('Department on ship is mandatory.');";
        }
        if (ddlInOffice.SelectedIndex <= 0)
        {
            js = "alert('Department in office is mandatory.');";
        }
        if (ddlVessel.SelectedIndex == 0)
        {
            js = "alert('Select vessel.');";

        }
        else if (DateTime.Compare(DateTime.Parse(txtRaisedOn.Text, iFormatProvider), DateTime.Today) > 0)
        {
            js = "alert('Raise date can not be greater than current date.');";
        }
        else if (txtExpectedComp.Text.Trim().Length == 0)
        {
            js = "alert('Select expected completion date.');";

        }
        else
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            if (objBLL.IsVessel(int.Parse(ddlVessel.SelectedValue)) == 0)
            {
                if (ddlPIC.SelectedIndex == 0)
                {
                    js = "alert('Select PIC.');";
                }

            }
            else
            {
                if (ddlNature.SelectedIndex == 0)
                {
                    js = "alert('Select category - nature.');";
                }
                else if (ddlPrimary.SelectedIndex == 0)
                {
                   
                    js = "alert('Select category - primary.');";
                }
                else if (ddlSecondary.SelectedIndex == 0)
                {
                   
                    js = "alert('Select category - secondary.');";
                }
            }

        }


        if (txtDescribe.Text.Trim() == "")
        {
            js = "alert('Enter job description.');";

        }
        if (txtRaisedOn.Text.Trim() == "")
        {
            js = "alert('Select raised on date.');";

        }
        if (ddlFunction.SelectedIndex > 0)
        {
            if (ddlSysLocation.SelectedIndex == 0)
            {
                js = "alert('Select system location.');";

            }
            if ((ddlSysLocation.SelectedIndex > 0) && (ddlSubSystem_location.SelectedIndex == 0))
            {
                js = "alert('Select subsystem location.');";
            }

        }
        if (ddlFunction.SelectedIndex == 0)
        {
            if (ddlSysLocation.SelectedIndex > 0)
            {
                js = "alert('Function not selected.');";
            }
            if (ddlSubSystem_location.SelectedIndex > 0)
            {
                js = "alert('Function not selected.');";
            }

        }

        if (ddlWLType.SelectedValue == "NCR")
        {
            if (txtCompletedOn.Text.Trim() != "")
            {
                if (Mode == "UPDATE")
                {
                    if (txtCauses.Text == "")
                    {
                        js = "alert('Enter  causes / possible causes for the NCR.');";
                    }
                    if (txtCorrectiveAction.Text == "")
                    {
                        js = "alert('Enter all the corrective actions taken for the NCR.');";
                    }
                    if (txtPreventiveAction.Text == "")
                    {
                        js = "alert('Enter preventive actions taken to avoid recurrence.');";
                    }
                }
                else
                {
                    if (txtCausesNew.Text == "")
                    {
                        js = "alert('Enter  causes / possible causes for the NCR.');";
                    }
                    if (txtCorrectiveActionNew.Text == "")
                    {
                        js = "alert('Enter all the corrective actions taken for the NCR.');";
                    }
                    if (txtPreventiveActionNew.Text == "")
                    {
                        js = "alert('Enter preventive actions taken to avoid recurrence.');";
                    }
                }
            }

        }

        if (js.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            return false;
        }
        else
            return true;
    }

    protected void btnSaveJob_OnClick(object sender, EventArgs e)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            if (ValidateForm("ADD"))
            {
                int VESSEL_ID = Convert.ToInt32(ddlVessel.SelectedValue);
                string JOB_DESCRIPTION = txtDescribe.Text;

                string DATE_RAISED = "1900/1/1";
                if (txtRaisedOn.Text.Trim() != "")
                    DATE_RAISED = txtRaisedOn.Text;

                string DATE_ESTMTD_CMPLTN = "1900/1/1";
                if (txtExpectedComp.Text.Trim() != "")
                    DATE_ESTMTD_CMPLTN = txtExpectedComp.Text;

                string DATE_COMPLETED = "1900/1/1";
                if (txtCompletedOn.Text.Trim() != "")
                    DATE_COMPLETED = txtCompletedOn.Text;

                int PRIORITY = int.Parse(ddlPriority.SelectedValue);
                int ASSIGNOR = int.Parse(ddlAssignedBy.SelectedValue);
              

                int NCR_YN = 0;
                string WL_Type = null;
         
                WL_Type = ddlWLType.SelectedValue.ToString();
                int DEPT_SHIP = int.Parse(ddlOnShip.SelectedValue);
                int DEPT_OFFICE = int.Parse(ddlInOffice.SelectedValue);
                string REQSN_MSG_REF = uc_ReqRef.SelectedValue;

                int DEFER_TO_DD = int.Parse(rdoDeferToDrydock.SelectedValue);

                int CATEGORY_NATURE = int.Parse(ddlNature.SelectedValue);
                int CATEGORY_PRIMARY = int.Parse(ddlPrimary.SelectedValue);
                int CATEGORY_SECONDARY = int.Parse(ddlSecondary.SelectedValue);
                int CATEGORY_MINOR = int.Parse(ddlMinorCat.SelectedValue);

                int PIC = 0;
                if (ddlPIC.SelectedIndex > 0)
                    PIC = int.Parse(ddlPIC.SelectedValue.ToString());

                int CREATED_BY = int.Parse(Session["USERID"].ToString());
                int Inspector = UDFLib.ConvertToInteger(ddlInspector.SelectedValue);
                string InspectionDate = "1900/1/1";
                if (txtInspectionDate.Text.Trim() != "")
                    InspectionDate = txtInspectionDate.Text;

                int TOSYNC = 0;
                string Causes = "";
                string CorrectiveAction = "";
                string PreventiveAction = "";

                if (WL_Type == "NCR")
                {
                    Causes = txtCausesNew.Text;
                    CorrectiveAction = txtCorrectiveActionNew.Text;
                    PreventiveAction = txtPreventiveActionNew.Text;
                    NCR_YN = -1; //This conditon is added by Hadish on 24-Nov-16 JIT 11076,Under guidance of Pranav.
                }
                else
                {
                    Causes = string.Empty;
                    CorrectiveAction = string.Empty;
                    PreventiveAction = string.Empty;
                    NCR_YN = 0;
                }

                int PSC_SIRE = UDFLib.ConvertToInteger(ddlPSCSIRE.SelectedValue);
                if (objBLL.IsVessel(VESSEL_ID) == 1)
                    TOSYNC = 1;
                DataTable dtNewRecord = new DataTable();


                dtNewRecord = objBLL.Insert_NewJob(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF,
                                                    DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction,
                                                    PreventiveAction, Inspector, InspectionDate, WL_Type, PSC_SIRE, int.Parse(ddlFunction.SelectedValue), ddlSysLocation.SelectedValue.ToString().Split(',')[0], ddlSubSystem_location.SelectedValue.ToString().Split(',')[0]);



                if (dtNewRecord.Rows.Count > 0)
                {
                    hdfWorklistID.Value= dtNewRecord.Rows[0]["WORKLIST_ID"].ToString();
                    hdfVesselID.Value = dtNewRecord.Rows[0]["VESSEL_ID"].ToString();
                    hdfOfficeID.Value = dtNewRecord.Rows[0]["OFFICE_ID"].ToString();
                    if (ddlWLType.SelectedValue == "NCR")
                    {
                        txtNCRNo.Text = dtNewRecord.Rows[0]["NCR_NUM"].ToString();
                        txtNCRNo_Year.Text = dtNewRecord.Rows[0]["NCR_YEAR"].ToString();
                    }

                    txtOfficeID.Value = dtNewRecord.Rows[0]["OFFICE_ID"].ToString();
                    txtJobCode.Text = "0";

                    //if (Request.QueryString["InspID"] != null && Request.QueryString["LocationID"] != null)
                    //{
                    //    int inspID=Convert.ToInt32(Request.QueryString["InspID"]);
                    //    int WorlistID=Convert.ToInt32(dtNewRecord.Rows[0]["WORKLIST_ID"].ToString());
                    //    int OfficeID=Convert.ToInt32(dtNewRecord.Rows[0]["OFFICE_ID"].ToString());
                    //    int LocationID = Convert.ToInt32(Request.QueryString["LocationID"].ToString());
                    //    objBLL.TEC_WL_INS_InspectionWorklist_Location(Convert.ToInt32(Request.QueryString["InspID"]), WorlistID, VESSEL_ID, OfficeID, LocationID, CREATED_BY);
                    //}
                }

                if (ddlAssignedBy.SelectedItem.Text == "Vetting")
                {
                    
                    string js = "alert('Job added successfully!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js, true);
                    btnSaveJob.Enabled = false;
                }
                else
                {
                    string js1 = "alert('Job added successfully'); window.close();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);
                }
            }

        }
        catch (Exception ex)
        {
           
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnSaveAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtMessage.Text.Trim().Length == 0)
            {
                string OpenFollowupDiv = "alert('Message description is mandatory field.');OpenFollowupDiv();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenFollowupDiv", OpenFollowupDiv, true);
                return;
            }
            int OFFICE_ID = int.Parse(txtOfficeID.Value);
            int WORKLIST_ID = int.Parse(hdnWorklistID.Value);
            int VESSEL_ID = int.Parse(ddlVessel.SelectedValue);

            string FOLLOWUP = txtMessage.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());
            int TOSYNC = 1;
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            int newFollowupID = objBLL.Insert_Followup(OFFICE_ID, WORKLIST_ID, VESSEL_ID, FOLLOWUP, CREATED_BY, TOSYNC);

            LoadFollowUps(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

            txtMessage.Text = "";

            string js = "CloseFollowupDiv();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeDiv", js, true);


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
     

        }
    }

    private void LoadFollowUps(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    {
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            DataTable dtFollowUps = objBLL.Get_FollowupList(OFFICE_ID, VESSEL_ID, WORKLIST_ID);

            grdFollowUps.DataSource = dtFollowUps;
            grdFollowUps.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
        
        }
    }

    protected void btnUpdateJob_OnClick(object sender, EventArgs e)
    {

        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            if (ValidateForm("UPDATE"))
            {
                int VESSEL_ID = Convert.ToInt32(ddlVessel.SelectedValue);
                int OFFICE_ID = int.Parse(txtOfficeID.Value);
                int WORKLIST_ID = int.Parse(hdnWorklistID.Value);

                string JOB_DESCRIPTION = txtDescribe.Text;


                string DATE_RAISED = "1900/1/1";
                if (txtRaisedOn.Text.Trim() != "")
                    DATE_RAISED = txtRaisedOn.Text;

                string DATE_ESTMTD_CMPLTN = "1900/1/1";
                if (txtExpectedComp.Text.Trim() != "")
                    DATE_ESTMTD_CMPLTN = txtExpectedComp.Text;

                string DATE_COMPLETED = "1900/1/1";
                if (txtCompletedOn.Text.Trim() != "")
                    DATE_COMPLETED = txtCompletedOn.Text;

                int PRIORITY = int.Parse(ddlPriority.SelectedValue);
                int ASSIGNOR = int.Parse(ddlAssignedBy.SelectedValue);
                //  int NCR_YN = int.Parse(ddlWLType.SelectedValue=="-1"?"-1":"0");
                int DEPT_SHIP = int.Parse(ddlOnShip.SelectedValue);
                int DEPT_OFFICE = int.Parse(ddlInOffice.SelectedValue);
                string REQSN_MSG_REF = uc_ReqRef.SelectedValue;
                int DEFER_TO_DD = int.Parse(rdoDeferToDrydock.SelectedValue);
                int CATEGORY_NATURE = int.Parse(ddlNature.SelectedValue);
                int CATEGORY_PRIMARY = int.Parse(ddlPrimary.SelectedValue);
                int CATEGORY_SECONDARY = int.Parse(ddlSecondary.SelectedValue);
                int CATEGORY_MINOR = int.Parse(ddlMinorCat.SelectedValue);
                string PIC = ddlPIC.SelectedValue.ToString();

                int MODIFIED_BY = int.Parse(Session["USERID"].ToString());
                int Inspector = UDFLib.ConvertToInteger(ddlInspector.SelectedValue);
                string InspectionDate = "1900/1/1";
                if (txtInspectionDate.Text.Trim() != "")
                    InspectionDate = txtInspectionDate.Text;


                int TOSYNC = 0;

                if (objBLL.IsVessel(VESSEL_ID) == 1)
                    TOSYNC = 1;

                if (txtCompletedOn.Text.Trim() != "" && txtCompletedOn.Text.Trim() != "1/01/00")
                    ImgBtnAddFollowup.Visible = false;
                string WL_Type = null;
                WL_Type = ddlWLType.SelectedValue.ToString();
                int PSC_SIRE = UDFLib.ConvertToInteger(ddlPSCSIRE.SelectedValue);

            
                int res = objBLL.Update_Job(VESSEL_ID, WORKLIST_ID, OFFICE_ID, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, 0, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD,
                                           CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, MODIFIED_BY, TOSYNC, Inspector, InspectionDate, WL_Type, PSC_SIRE,
                                           int.Parse(ddlFunction.SelectedValue), ddlSysLocation.SelectedValue.ToString().Split(',')[0], ddlSubSystem_location.SelectedValue.ToString().Split(',')[0]);

                if (ddlWLType.SelectedValue == "NCR")
                {
                    objBLL.Update_Job_Causes(txtCauses.Text, VESSEL_ID, WORKLIST_ID, OFFICE_ID, MODIFIED_BY, 1);
                    objBLL.Update_Job_CorrectiveAction(txtCorrectiveAction.Text, VESSEL_ID, WORKLIST_ID, OFFICE_ID, MODIFIED_BY, 1);
                    objBLL.Update_Job_PreventiveAction(txtPreventiveAction.Text, VESSEL_ID, WORKLIST_ID, OFFICE_ID, MODIFIED_BY, 1);
                }

                objBLL.Sync_Job_AfterUpdate(VESSEL_ID, WORKLIST_ID, OFFICE_ID);

                string js = "";

                if (sender == null)
                {
                    if (ddlWLType.SelectedValue == "NCR")
                    {
                        try
                        {
                            string FOLLOWUP = "This job is converted to NCR on: " + DateTime.Today.ToString("dd/MMM/yyyy");
                            int CREATED_BY = int.Parse(Session["USERID"].ToString());

                            int newFollowupID = objBLL.Insert_Followup(OFFICE_ID, WORKLIST_ID, VESSEL_ID, FOLLOWUP, CREATED_BY, TOSYNC);

                            LoadFollowUps(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

                        }
                        catch(Exception ex)
                        {
                            UDFLib.WriteExceptionLog(ex);
                        }
                        js = "alert('Job is changed to NCR.');";
                    }
                }
                else
                {
                    js = "alert('Job details updated.');window.close();";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

            }

        }
        catch (Exception ex)
        {
           
            UDFLib.WriteExceptionLog(ex);

        }


        //Response.Redirect("../Job List/ViewJob.aspx");
        //fillvalue(Convert.ToInt32(hdnJobID.Value));
    }

    protected void btnSelectAndClose_OnClick(object sender, EventArgs e)
    {
        hdnNature.Value = Convert.ToString(lbNature.SelectedValue);
        hdnPrimary.Value = Convert.ToString(lbPrimary.SelectedValue);
        hdnSecondary.Value = Convert.ToString(lbSecondary.SelectedValue);
        hdnMinor.Value = Convert.ToString(lbMinor.SelectedValue);


        BindNatureForDropDown();

    }

    protected void btnSaveCauses_Click(object sender, EventArgs e)
    {
        if (txtOfficeID.Value == "")
            return;

        int VESSEL_ID = Convert.ToInt32(ddlVessel.SelectedValue);
        int OFFICE_ID = int.Parse(txtOfficeID.Value);
        int WorkList_ID = int.Parse(hdnWorklistID.Value);

        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        string Causes = txtCauses.Text;
        int UserID = GetSessionUserID();
        int ToSync = 0;

        if (objBLL.IsVessel(VESSEL_ID) == 1)
            ToSync = 1;

        if (Causes != "")
        {
            int result = objBLL.Update_Job_Causes(Causes, VESSEL_ID, WorkList_ID, OFFICE_ID, UserID, ToSync);
            if (result == 1)
            {
                lblCauses.BackColor = System.Drawing.Color.Transparent;
                string js = "alert('Causes saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
        }
    }

    protected void btnCorrectiveAction_Click(object sender, EventArgs e)
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        if (txtOfficeID.Value == "")
            return;

        int VESSEL_ID = Convert.ToInt32(ddlVessel.SelectedValue);
        int OFFICE_ID = int.Parse(txtOfficeID.Value);
        int WorkList_ID = int.Parse(hdnWorklistID.Value);

        string CorrectiveAction = txtCorrectiveAction.Text;
        int UserID = GetSessionUserID();
        int ToSync = 0;

        if (objBLL.IsVessel(VESSEL_ID) == 1)
            ToSync = 1;

        if (CorrectiveAction != "")
        {
            int result = objBLL.Update_Job_CorrectiveAction(CorrectiveAction, VESSEL_ID, WorkList_ID, OFFICE_ID, UserID, ToSync);
            if (result == 1)
            {
                lblCorrectiveAction.BackColor = System.Drawing.Color.Transparent;

                string js = "alert('Corrective action saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

            }

        }
    }

    protected void btnPreventiveAction_Click(object sender, EventArgs e)
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        if (txtOfficeID.Value == "")
            return;

        int VESSEL_ID = Convert.ToInt32(ddlVessel.SelectedValue);
        int OFFICE_ID = int.Parse(txtOfficeID.Value);
        int WorkList_ID = int.Parse(hdnWorklistID.Value);


        string PreventiveAction = txtPreventiveAction.Text;
        int UserID = GetSessionUserID();
        int ToSync = 0;

        if (objBLL.IsVessel(VESSEL_ID) == 1)
            ToSync = 1;

        if (PreventiveAction != "")
        {
            int result = objBLL.Update_Job_PreventiveAction(PreventiveAction, VESSEL_ID, WorkList_ID, OFFICE_ID, UserID, ToSync);
            if (result == 1)
            {
                lblPreventiveAction.BackColor = System.Drawing.Color.Transparent;

                string js = "alert('Preventive action saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

            }
        }
    }

    protected void rdoNCR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["JID"] == null)
        {

            if (ddlWLType.SelectedValue == "NCR")
            {
                pnlNCR.Visible = true;
                lblCompletedOnCaption.Text = "Closed On:";
                lblCompletedByCaption.Text = "Closed By :";
                string sel = ddlWLType.SelectedValue;
                string js = "toggleTabs(1);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);


            }
            else
            {
                pnlNCR.Visible = false;

                lblCompletedOnCaption.Text = "Completed On :";
                lblCompletedByCaption.Text = "Completed By :";
                string sel = ddlWLType.SelectedValue;
                string js = "toggleTabs(0);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }

        }
        else
        {
            string sel = ddlWLType.SelectedValue;
            string js = "toggleTabs(" + sel + ");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            //btnUpdateJob_OnClick(null, null);
        }
        if (ddlAssignedBy.SelectedItem.Text == "Vetting")
        {
         
            ddlWLType.SelectedValue = "NCR";
        }
    }

    protected void Load_CrewComplaintsLog()
    {
        BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
        int Worklist_ID = UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString());
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());
        int USERID = UDFLib.ConvertToInteger(Session["USERID"].ToString());
        DataSet ds = objBLLCrew.Get_CrewComplaintLog(Worklist_ID, Vessel_ID, USERID);

        rptComplaintsToDPA.DataSource = ds.Tables[0];
        rptComplaintsToDPA.DataBind();

        DataRow[] dr = ds.Tables[0].Select("escalation_level>=3");

        if (dr.Length == 1)
        {
            btnReleaseToFlag.Enabled = true;
            pnlReleaseToFlag.Visible = true;
        }
        else if (dr.Length == 2)
        {
            btnReleaseToFlag.Enabled = false;
            pnlReleaseToFlag.Visible = false;
        }
        else
        {
            pnlReleaseToFlag.Visible = false;
        }


    }

    protected void btnReleaseToFlag_Click(object sender, EventArgs e)
    {
        int OFFICE_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
        int WORKLIST_ID = Convert.ToInt32(Request.QueryString["WLID"]);
        int VESSEL_ID = Convert.ToInt32(Request.QueryString["VID"]);
        int USERID = int.Parse(Session["USERID"].ToString());
        if (txtDPARemark.Text.Trim() == "")
        {
            string js = "alert('Please enter remarks.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDPA", js, true);
        }
        else
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            objBLL.ReleaseComplaint_ToFlag(VESSEL_ID, WORKLIST_ID, OFFICE_ID, USERID, txtDPARemark.Text);
            SendMail_ReleaseToFlag(VESSEL_ID, WORKLIST_ID, OFFICE_ID, USERID);
            Load_CrewComplaintsLog();
        }
    }

    protected void SendMail_ReleaseToFlag(int VESSEL_ID, int WORKLIST_ID, int OFFICE_ID, int USERID)
    {
        BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";

        string msgBody = "";
        string msgSubject = "";

        DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

        DataSet ds = objBLLCrew.Get_CrewComplaintLog(WORKLIST_ID, VESSEL_ID, USERID);

        DataTable dtMain = dtsJobDetails.Tables[0];
        DataTable dtDetail = ds.Tables[0];

        DataRow[] dr1 = ds.Tables[0].Select("escalation_level = 1");


        foreach (DataRow dr in dtMain.Rows)
        {
            msgSubject = "Crew complaint  escalated to Flag: " + dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString() + " - " + dr1[0]["escalated_by_staff_code"].ToString() + " - " + dr1[0]["escalated_by_name"].ToString();

            msgBody = "Dear Sir/Madam,";
            msgBody += "<br><br>";

            msgBody += "Crew complaint from vessel " + dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString() + " has been escalated for your attention.";
            msgBody += "<br><br>";

            msgBody += "Escalated By: " + dr1[0]["escalated_by_name"].ToString() + "<br>";
            msgBody += "Escalated On: " + dr1[0]["escalated_on"].ToString() + "<br>";

            msgBody += "Complaint: " + dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString().Replace("\n", "<br>");


            int MsgID = objBLLCrew.Send_CrewNotification(0, 0, 0, 0, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
            if (MsgID > 0)
            {
                string js = "window.open('../../Crew/EmailEditor.aspx?discard=1&ID=" + MsgID + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NotifyFlag", js, true);
            }

        }

    }

    protected void hplRework_Click(object sender, EventArgs e)
    {
        ViewState["WORKLIST_STATUS"] = "REWORKED";
        lblWorklistTitle.Text = "Rework job";
        txtWorklistStatusRemark.Text = "";

        UpdatePanel4.Update();

        string js = "showModal('dvReworkClose');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);

    }

    protected void hplCloseThisJob_Click(object sender, EventArgs e)
    {
        ViewState["WORKLIST_STATUS"] = "CLOSED";
        lblWorklistTitle.Text = "Verify and close job";
        txtWorklistStatusRemark.Text = "";

        UpdatePanel4.Update();

        string js = "showModal('dvReworkClose');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);
    }


    protected void btnSaveStatus_Click(object s, EventArgs e)
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        objBLL.Upd_Worklist_Status(UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString())
            , UDFLib.ConvertToInteger(Request.QueryString["OFFID"].ToString()), Convert.ToInt32(Session["USERID"]), txtWorklistStatusRemark.Text, Convert.ToString(ViewState["WORKLIST_STATUS"]));


        LoadJobToEdit(UDFLib.ConvertToInteger(Request.QueryString["OFFID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString()));
        Load_CrewComplaintsLog();

    }

    public static long GetFileSize(string file)
    {
        FileInfo info = new FileInfo(file);
        long SIZE_BYTES = info.Length;
        return SIZE_BYTES;
    }


    #region Function_Location_SubLocation

    private void reset_ddl()
    {
        DataTable dtemt = new DataTable();


        DataColumn workCol = dtemt.Columns.Add("AssginLocationID", typeof(Int32));

        dtemt.Columns.Add("LocationName", typeof(String));
        DataRow drsys = dtemt.NewRow();
        drsys["AssginLocationID"] = 0;
        drsys["LocationName"] = "--SELECT --";
        dtemt.Rows.InsertAt(drsys, 0);
        ddlSysLocation.DataSource = dtemt;
        ddlSysLocation.DataValueField = "AssginLocationID";
        ddlSysLocation.DataTextField = "LocationName";
        ddlSysLocation.DataBind();
        ddlSysLocation.Items.Insert(0, new ListItem("-SELECT-", "0"));


        DataTable dts = new DataTable();
        DataColumn workCols = dts.Columns.Add("AssginLocationID", typeof(String));

        dts.Columns.Add("LocationName", typeof(String));
        DataRow drsyss = dts.NewRow();
        drsyss["AssginLocationID"] = "0,0";
        drsyss["LocationName"] = "--SELECT --";
        dts.Rows.InsertAt(drsyss, 0);
        ddlSubSystem_location.DataSource = dts;
        ddlSubSystem_location.DataValueField = "AssginLocationID";
        ddlSubSystem_location.DataTextField = "LocationName";
        ddlSubSystem_location.DataBind();
        ddlSubSystem_location.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }

    private void Bindfunction()
    {
        try
        {

            reset_ddl();
            SMS.Business.PURC.BLL_PURC_Purchase objBLLPurc = new SMS.Business.PURC.BLL_PURC_Purchase();
            DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", "");
            ddlFunction.Items.Clear();
            ddlFunction.DataSource = dt;
            ddlFunction.DataValueField = "CODE";
            ddlFunction.DataTextField = "DESCRIPTION";
            ddlFunction.DataBind();
            ddlFunction.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
         
        }
    }

    private void BindSystem_Location()
    {

        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        DataTable dt = objBLL.GET_SYSTEM_LOCATION(UDFLib.ConvertToInteger(ddlFunction.SelectedValue), UDFLib.ConvertToInteger(ddlVessel.SelectedValue.ToString()));

        ddlSysLocation.Items.Clear();
        ddlSysLocation.DataSource = dt;
        ddlSysLocation.DataValueField = "AssginLocationID";
        ddlSysLocation.DataTextField = "LocationName";
        ddlSysLocation.DataBind();
        ddlSysLocation.Items.Insert(0, new ListItem("- SELECT-", "0,0"));
        if (ddlSysLocation.SelectedIndex == -1)
            ddlSysLocation.SelectedIndex = 0;

    }

    private void BindSubSystem_Location()
    {
        try
        {
            DataTable dt;
            if (ddlSysLocation.SelectedValue != "0")
            {
                ddlSubSystem_location.Items.Clear();
                BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
                dt = objBLL.GET_SUBSYTEMSYSTEM_LOCATION(ddlSysLocation.SelectedValue.ToString().Split(',')[1], null, UDFLib.ConvertToInteger(ddlVessel.SelectedValue.ToString()));
                ddlSubSystem_location.Items.Clear();
                ddlSubSystem_location.DataSource = dt;
                ddlSubSystem_location.DataValueField = "AssginLocationID";
                ddlSubSystem_location.DataTextField = "LocationName";
                ddlSubSystem_location.DataBind();
                ddlSubSystem_location.Items.Insert(0, new ListItem("- SELECT-", "0,0"));
                if (ddlSubSystem_location.SelectedIndex == -1)
                    ddlSubSystem_location.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            ddlSubSystem_location.Items.Insert(0, new ListItem("-SELECT-", "0"));
          
        }

    }

    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        reset_ddl();
        if (ddlFunction.SelectedIndex != 0)
        {
            BindSystem_Location();
        }

        ddlSubSystem_location.SelectedIndex = 0;
    }

    protected void ddlSysLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSystem_Location();
    }

    private void CheckSettingforFunctions()
    {
        DataTable _settingTable;
        DataRow[] foundRows;
        
        try
        {
            BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
            _settingTable = objBLL.GetSettingforFunctions();


            if (_settingTable.Rows.Count > 0)
            {
                foundRows = _settingTable.Select("Settings_Key = 'View Functions To Jobs'");

                if (foundRows.Length > 0)
                {
                    if (_settingTable.Rows[0]["Settings_Value"].ToString() == "0")           //Convert datatype bit to varchar  
                    // if ((bool)foundRows[0]["Settings_Value"] == false)
                    {
                        tblfunction.Visible = false;
                        txtDescribe.Height = 220;
                    }
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            throw ex;
          
        }

    }



    #endregion
    protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedValue != "0")
        {
            if (ddlAssignedBy.SelectedItem.Text == "Vetting")
            {
                pnlVetting.Visible = true;
                BindVetting(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
                ddlWLType.SelectedValue = "NCR";
            }
            else
            {
                pnlVetting.Visible = false;
            }
        }
      
    }
    protected void ddlVetting_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSelectObs.Text = "";
        BindObservation();     

    }

    protected void btnSaveVetting_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int OFFICE_ID = 0;
            int WORKLIST_ID = 0;
            int VESSEL_ID = 0;
            int UserID = GetSessionUserID();
            BLL_VET_Index ObjBLLIndx = new BLL_VET_Index();
            if (Request.QueryString["OFFID"] != null)
            {
                OFFICE_ID = UDFLib.ConvertToInteger(Request.QueryString["OFFID"].ToString());
            }

            if (Request.QueryString["WLID"] != null)
            {
                WORKLIST_ID = UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString());
            }

            if (Request.QueryString["VID"] != null)
            {
                VESSEL_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());
            }
            if (lblSelectObs.Text != "" & lblSelectObs.Text != null)
            {


            if (hdfWorklistID.Value.ToString() != "")          
            {
                
                int result = ObjBLLIndx.VET_INS_Assign_Job_To_Be_Vetting(int.Parse(hdfSelectObsID.Value), int.Parse(hdfWorklistID.Value), int.Parse(hdfVesselID.Value), int.Parse(hdfOfficeID.Value), UserID);

                if (result > 0)
                {
                    string js = "alert('Vetting attached to job successfully.'); saveClose(); window.close(); ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                }

             
            }
            else if (OFFICE_ID != 0 && WORKLIST_ID != 0 && VESSEL_ID != 0)
            {

                int result = ObjBLLIndx.VET_INS_Assign_Job_To_Be_Vetting(int.Parse(hdfSelectObsID.Value), WORKLIST_ID, VESSEL_ID, OFFICE_ID, UserID);
                if (result > 0)
                {
                    string js = "alert('Vetting attached updated successfully.');saveClose();window.close(); ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);    
                }
                
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Please save worklist job then assign vetting.')", true);
            }
            }
            else
            {
                string js = "alert('Please select Observation');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDPA", js, true);            
               
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    protected void GvObservationJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDATE_ESTMTD_CMPLTN = (Label)e.Row.FindControl("lblDATE_ESTMTD_CMPLTN");
                Label lblCompletedOn = (Label)e.Row.FindControl("lblCompletedOn");
                if (lblDATE_ESTMTD_CMPLTN.Text != "")
                {
                    lblDATE_ESTMTD_CMPLTN.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblDATE_ESTMTD_CMPLTN.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
                if (lblCompletedOn.Text != "")
                {
                    lblCompletedOn.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblCompletedOn.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GvVettingJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDATE_ESTMTD_CMPLTN = (Label)e.Row.FindControl("lblDATE_ESTMTD_CMPLTN");
                Label lblgrdCompletedOn = (Label)e.Row.FindControl("lblgrdCompletedOn");
                if (lblDATE_ESTMTD_CMPLTN.Text != "")
                {
                    lblDATE_ESTMTD_CMPLTN.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblDATE_ESTMTD_CMPLTN.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
                if (lblgrdCompletedOn.Text != "")
                {
                    lblgrdCompletedOn.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(lblgrdCompletedOn.Text).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}



