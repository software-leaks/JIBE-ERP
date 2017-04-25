using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using SMS.Business.TMSA;
using SMS.Business.Technical;
using SMS.Business.VET;

public partial class PI_Entry : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private int PI_Id = 0;
    private string DataSource = "";
    private string Assignor_Ids = "";
    private string Job_Type = "";
    private string Nature_Ids = "";
    private string Primary_IDs = "";
    private string Secondary_IDs = "";
    private string Minor_IDs = "";
    private string InspectionType_IDs = "";
    private string VettingType_IDs = "";
    private string Observation_Category = "";
    private string Observation_Type = "";
    private string Observation_Risk_Level = "";


    private int MeasuredForSBU = 0;
    private int IncludeBL = 0;
    private int IsWorkList=0;
    private int IsInspection = 0;
    private int IsVetting = 0;
    HiddenField hdnWorkList_PI_ID = new HiddenField();


    public UserAccess objUA = new UserAccess();
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_TMSA_PI objPI = new BLL_TMSA_PI();
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    BLL_VET_VettingLib objVet = new BLL_VET_VettingLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        cteEffectiveDate.Format = Convert.ToString(Session["User_DateFormat"]);
        //UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["Worklist_PIID"] = null;
            BindInterval();
            BindUnits();
            LoadQueryList();
            if (Request.QueryString[0] != null)
            {
                BindPIDetail();
            }
         

            if(Request.QueryString["UOM"]!=null)
                ddlUOM.SelectedValue = Request.QueryString["UOM"];
            if(Request.QueryString["Intr"]!=null)
                ddlInterval.SelectedValue=Request.QueryString["Intr"];
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            //btnsave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void ClearControls()
    {
        txtPICode.Text = "";
        txtName.Text = "";
        txtContext.Text = "";
        txtDescription.Text = "";
        chkBallastLaden.Checked = false;
        chkMeasuredForSBU.Checked = false;

    }
    protected void LoadQueryList()
    {
        ddlListSource.DataSource = objPI.Get_SavedQuery("", "TMSA_Daemon_SP",GetSessionUserID());
        ddlListSource.DataTextField = "ObjectName";
        ddlListSource.DataValueField = "ObjectName";
        ddlListSource.DataBind();
        ddlListSource.Items.Insert(0, new ListItem("-SELECT-","0"));
    }
    /// <summary>
    /// Method to bind PI controls
    /// </summary>
    protected void BindPIDetail()
    {
        try
        {

            PI_Id = Convert.ToInt32(Request.QueryString[0]);
            if (PI_Id != 0)
                ddlInterval.Enabled = false;
            DataTable dtDetail = BLL_TMSA_PI.Get_PI_Details(PI_Id).Tables[0];
            DataTable dtWorklist = BLL_TMSA_PI.Get_PI_Details(PI_Id).Tables[1];
            if (dtDetail.Rows.Count > 0)
            {

                txtName.Text = dtDetail.Rows[0]["Name"].ToString();
                txtPICode.Text = dtDetail.Rows[0]["Code"].ToString();
                txtDescription.Text = dtDetail.Rows[0]["Description"].ToString();
                txtContext.Text = dtDetail.Rows[0]["Context"].ToString();

                string sInterval = dtDetail.Rows[0]["Interval"].ToString();
                string sUOM = dtDetail.Rows[0]["UOM"].ToString();
                string sDatasource = dtDetail.Rows[0]["Datasource"].ToString();
                string sStatus = dtDetail.Rows[0]["PI_Status"].ToString();
                MeasuredForSBU = Convert.ToInt16(dtDetail.Rows[0]["MeasuredForSBU"]);
                IsWorkList = Convert.ToInt16(dtDetail.Rows[0]["IsWorkList"]);
                IsInspection = Convert.ToInt16(dtDetail.Rows[0]["IsInspectionType"]);
                IsVetting = Convert.ToInt16(dtDetail.Rows[0]["IsVetting"]);

                if (ddlStatus.Items.FindByValue(sStatus) != null)
                    ddlStatus.SelectedValue = sStatus;
                IncludeBL = Convert.ToInt16(dtDetail.Rows[0]["IncludeBL"]);

                if (ddlListSource.Items.FindByValue(sDatasource) != null)
                    ddlListSource.SelectedValue = sDatasource;
                if (MeasuredForSBU == 1)
                    chkMeasuredForSBU.Checked = true;
                if (IncludeBL == 1)
                    chkBallastLaden.Checked = true;

                if (ddlInterval.Items.FindByValue(sInterval) != null)
                {
                    ddlInterval.SelectedValue = ddlInterval.Items.FindByValue(sInterval).Value;
                }

                if (ddlUOM.Items.FindByValue(sUOM) != null)
                {
                    ddlUOM.SelectedValue = ddlUOM.Items.FindByValue(sUOM).Value;
                }

                if (IsWorkList == 1)//For worklist PI
                {
                    chkIsWorklist.Checked = true;

                    if (ddlInterval.Items.FindByValue("Monthly") != null)
                    {
                        ddlInterval.SelectedValue = "Monthly";
                        ddlInterval.Enabled = false;
                    }

                    if (ddlUOM.Items.FindByValue("Numbers") != null)
                    {
                        ddlUOM.SelectedValue = "Numbers";
                        ddlUOM.Enabled = false;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
                    BindNatureForDropDown();
                }
                else
                {
                    ddlInterval.Enabled = true;
                    ddlUOM.Enabled = true;
                }


                if (IsInspection == 1)//For inspection type PI
                {
                    chkInspection.Checked = true;

                    if (ddlInterval.Items.FindByValue("Monthly") != null)
                    {
                        ddlInterval.SelectedValue = "Monthly";
                        ddlInterval.Enabled = false;
                    }

                    if (ddlUOM.Items.FindByValue("Numbers") != null)
                    {
                        ddlUOM.SelectedValue = "Numbers";
                        ddlUOM.Enabled = false;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
                    BindInspectionType();
                }
                else
                {
                    ddlInterval.Enabled = true;
                    ddlUOM.Enabled = true;
                }


                if (IsVetting == 1)//For vetting type PI
                {
                    chkVetting.Checked = true;

                    if (ddlInterval.Items.FindByValue("Monthly") != null)
                    {
                        ddlInterval.SelectedValue = "Monthly";
                        ddlInterval.Enabled = false;
                    }

                    if (ddlUOM.Items.FindByValue("Numbers") != null)
                    {
                        ddlUOM.SelectedValue = "Numbers";
                        ddlUOM.Enabled = false;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
                    BindVettingType();
                }
                else
                {
                    ddlInterval.Enabled = true;
                    ddlUOM.Enabled = true;
                }

                if (IsVetting == 2)//For vetting observations PI
                {
                    chkObservation.Checked = true;

                    if (ddlInterval.Items.FindByValue("Monthly") != null)
                    {
                        ddlInterval.SelectedValue = "Monthly";
                        ddlInterval.Enabled = false;
                    }

                    if (ddlUOM.Items.FindByValue("Numbers") != null)
                    {
                        ddlUOM.SelectedValue = "Numbers";
                        ddlUOM.Enabled = false;
                    }
                    BindVettingObservationCategories();
                    BindVettingTypeForObservation();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
                   // BindVettingObservations();
                }
                else
                {
                    ddlInterval.Enabled = true;
                    ddlUOM.Enabled = true;
                }


                //To bind the work list setting for PI
                if (dtWorklist != null && dtWorklist.Rows.Count > 0)
                {
                    ViewState["Worklist_PIID"] = dtWorklist.Rows[0]["WorkList_PI_ID"].ToString();
                    Assignor_Ids = dtWorklist.Rows[0]["Assigned_By"].ToString();
                    chkActivate_Scheduler.Checked = Convert.ToBoolean(dtWorklist.Rows[0]["Activate_Scheduler"]);
                    InspectionType_IDs = dtWorklist.Rows[0]["Inspection_Type"].ToString();
                    VettingType_IDs = dtWorklist.Rows[0]["Vetting_Type"].ToString();


                    string[] splitStr;
                    string sValue = "0";
                    if (IsWorkList == 1)
                    {
                        Job_Type = dtWorklist.Rows[0]["Job_Type"].ToString();
                        Nature_Ids = dtWorklist.Rows[0]["Nature_List"].ToString();
                        Primary_IDs = dtWorklist.Rows[0]["Primary_List"].ToString();
                        Secondary_IDs = dtWorklist.Rows[0]["Secondary_List"].ToString();
                        Minor_IDs = dtWorklist.Rows[0]["Minor_List"].ToString();




                        //To select the assignor
                        splitStr = Assignor_Ids.Split(',');
                        ddlAssignor.ClearSelection();
                        //loop through the array items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString().Trim();

                            if (ddlAssignor.Items.FindByValue(sValue) != null)

                                ddlAssignor.Items.FindByValue(sValue).Selected = true;
                        }

                        //To select the Job type
                        splitStr = Job_Type.Split(',');

                        //loop through the arraylist items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (lstJOB.Items.FindByValue(sValue) != null)

                                lstJOB.Items.FindByValue(sValue).Selected = true;
                        }


                        //To select the nature category
                        splitStr = Nature_Ids.Split(',');
                        //loop through the arraylist items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();
                            if (sValue != "0")
                            {

                                if (ddlNature.Items.FindByValue(sValue) != null)

                                    ddlNature.Items.FindByValue(sValue).Selected = true;

                                BindPrimaryByNatureID(Convert.ToInt32(sValue));

                            }
                        }

                        //To select the primary category
                        splitStr = Primary_IDs.Split(',');

                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();
                            if (sValue != "0")
                            {
                                if (ddlPrimary.Items.FindByValue(sValue) != null)

                                    ddlPrimary.Items.FindByValue(sValue).Selected = true;

                                BindSecondaryByPrimaryID(Convert.ToInt32(sValue));
                            }
                            else
                                ddlPrimary.SelectedValue = "0";
                        }

                        //To select the secondary category
                        splitStr = Secondary_IDs.Split(',');
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();
                            if (sValue != "0")
                            {
                                if (ddlSecondary.Items.FindByValue(sValue) != null)

                                    ddlSecondary.Items.FindByValue(sValue).Selected = true;

                                BindMinorBySecondatyID(Convert.ToInt32(sValue));
                            }
                            else
                                ddlSecondary.SelectedValue = "0";

                        }


                        //To select the minor category
                        splitStr = Minor_IDs.Split(',');
                        ddlMinor.ClearSelection();
                        //loop through the arraylist items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();
                            if (sValue != "0")
                            {
                                if (ddlMinor.Items.FindByValue(sValue) != null)

                                    ddlMinor.Items.FindByValue(sValue).Selected = true;
                            }
                            else
                                ddlMinor.SelectedValue = "0";
                        }

                    }
                    else if (IsInspection == 1)
                    {
                        //To select the assignor
                        splitStr = Assignor_Ids.Split(',');
                        ddlAssignor.ClearSelection();
                        chkActivateInspectionScheduler.Checked = Convert.ToBoolean(dtWorklist.Rows[0]["Activate_Scheduler"]);
                        splitStr = InspectionType_IDs.Split(',');

                        //loop through the arraylist items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (ddlInspectionType.Items.FindByValue(sValue) != null)

                                ddlInspectionType.Items.FindByValue(sValue).Selected = true;
                        }

                    }

                    else if (IsVetting == 1)//For vetting type
                    {
                        ddlVettingType.ClearSelection();
                        chkActivateVettingScheduler.Checked = Convert.ToBoolean(dtWorklist.Rows[0]["Activate_Scheduler"]);
                        splitStr = VettingType_IDs.Split(',');

                        //loop through the arraylist items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (ddlVettingType.Items.FindByValue(sValue) != null)

                                ddlVettingType.Items.FindByValue(sValue).Selected = true;
                        }

                    }

                    else if (IsVetting == 2)//For vetting observation
                    {
                        lstObservationVettingType.ClearSelection();
                        chkActivateObservationScheduler.Checked = Convert.ToBoolean(dtWorklist.Rows[0]["Activate_Scheduler"]);
                        Observation_Category = dtWorklist.Rows[0]["Observation_Category"].ToString();
                        Observation_Type = dtWorklist.Rows[0]["Observation_Type"].ToString();
                        Observation_Risk_Level = dtWorklist.Rows[0]["Observation_Risk_Level"].ToString();
                        splitStr = VettingType_IDs.Split(',');

                        //loop through the arraylist items & add the  to Dropdownlist
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (lstObservationVettingType.Items.FindByValue(sValue) != null)

                                lstObservationVettingType.Items.FindByValue(sValue).Selected = true;
                        }

                        splitStr = Observation_Category.Split(',');
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (lstObservationCategory.Items.FindByValue(sValue) != null)

                                lstObservationCategory.Items.FindByValue(sValue).Selected = true;
                        }

                        splitStr = Observation_Type.Split(',');
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (lstObservationType.Items.FindByValue(sValue) != null)

                                lstObservationType.Items.FindByValue(sValue).Selected = true;
                        }

                        splitStr = Observation_Risk_Level.Split(',');
                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            sValue = splitStr[i].ToString();

                            if (lstRiskLevel.Items.FindByValue(sValue) != null)

                                lstRiskLevel.Items.FindByValue(sValue).Selected = true;
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

    /// <summary>
    /// Descrition: Event method to populate worklist information
    /// </summary>
    protected void chkIsWorklist_OnCheckedChanged(object sender, EventArgs args)
    {
      

        ddlNature.Items.Clear();
        ddlPrimary.Items.Clear();
        ddlSecondary.Items.Clear();
        ddlMinor.Items.Clear();
        ddlAssignor.Items.Clear();
        lstJOB.ClearSelection();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
        CheckBox checkbox = (CheckBox)sender;
        if (checkbox.Checked)
        {
            if (ddlInterval.Items.FindByValue("Monthly") != null)
            {
                ddlInterval.SelectedValue = "Monthly";
                ddlInterval.Enabled = false;
            }

            if (ddlUOM.Items.FindByValue("Numbers") != null)
            {
                ddlUOM.SelectedValue = "Numbers";
                ddlUOM.Enabled = false;
            }

            chkInspection.Checked = false;
            chkVetting.Checked = false;
            chkObservation.Checked = false;
            BindNatureForDropDown();
        }
        else
        {
            ddlInterval.Enabled = true;
            ddlUOM.Enabled = true;
        }
    }


    /// <summary>
    /// Descrition: Event method to populate inspection information
    /// </summary>
    protected void chkInspection_OnCheckedChanged(object sender, EventArgs args)
    {

        Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
        CheckBox checkbox = (CheckBox)sender;
        if (checkbox.Checked)
        {
            IsWorkList = 1;
            if (ddlInterval.Items.FindByValue("Monthly") != null)
            {
                ddlInterval.SelectedValue = "Monthly";
                ddlInterval.Enabled = false;
            }

            if (ddlUOM.Items.FindByValue("Numbers") != null)
            {
                ddlUOM.SelectedValue = "Numbers";
                ddlUOM.Enabled = false;
            }

            chkIsWorklist.Checked = false;
            chkVetting.Checked = false;
            chkObservation.Checked = false;
            BindInspectionType();
        }
        else
        {
            IsWorkList = 0;
            ddlInterval.Enabled = true;
            ddlUOM.Enabled = true;
        }
    }


    protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
            BindPrimaryByNatureID(Convert.ToInt32(ddlNature.SelectedValue));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
            BindSecondaryByPrimaryID(Convert.ToInt32(ddlPrimary.SelectedValue));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
            BindMinorBySecondatyID(Convert.ToInt32(ddlSecondary.SelectedValue));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }



    /// <summary>
    /// Description: To load worklist list boxes
    /// </summary>
    protected void BindNatureForDropDown()
    {
        try
        {

            ddlAssignor.DataTextField = "Value";
            ddlAssignor.DataValueField = "ID";
            ddlAssignor.DataSource = objBLL.Get_Assigner();
            ddlAssignor.DataBind();
            ddlAssignor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


            ddlNature.DataTextField = "Name";
            ddlNature.DataValueField = "Code";
            ddlNature.DataSource = objBLL.GetAllNature();
            ddlNature.DataBind();

            ddlNature.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlPrimary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));



        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Description: Method to load primary category by selected Nature category
    /// </summary>
    /// <param name="i32NatureID">Nature category</param>

    protected void BindPrimaryByNatureID(Int32 i32NatureID)
    {
        try
        {

            int NatureSelectedCount=0;
            foreach (ListItem item in ddlNature.Items)
            {
                if (item.Selected)
                    NatureSelectedCount++;
            }

            if (ddlNature.Items[0].Selected)
            {
                ddlNature.ClearSelection();
                ddlNature.SelectedValue = "0";
            }
            if (NatureSelectedCount > 1 || i32NatureID == 0)
            {
                ddlPrimary.Items.Clear();
                ddlPrimary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlPrimary.SelectedValue = "0";
                ddlPrimary.Enabled = false;

                ddlSecondary.Items.Clear();
                ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlSecondary.SelectedValue = "0";
                ddlSecondary.Enabled = false;

                ddlMinor.Items.Clear();
                ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlMinor.SelectedValue = "0";
                ddlMinor.Enabled = false;

            }
            else
            {
                ddlPrimary.Enabled = true;
                ddlSecondary.Enabled = true;
                ddlMinor.Enabled = true;
                ddlSecondary.Items.Clear();
                ddlMinor.Items.Clear();
                ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlNature.SelectedValue = i32NatureID.ToString();
                ddlPrimary.Items.Clear();
                ddlPrimary.DataTextField = "Name";
                ddlPrimary.DataValueField = "Code";
                ddlPrimary.DataSource = objBLL.GetPrimaryByNatureID(i32NatureID);
                ddlPrimary.DataBind();
                ddlPrimary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Description: Method to load secondary category by selected primary category
    /// </summary>
    /// <param name="i32NatureID">Primary category</param>

    protected void BindSecondaryByPrimaryID(Int32 i32PrimaryID)
    
    {
        
       try
        {
            int PrimarySelectedCount=0;
            foreach (ListItem item in ddlPrimary.Items)
            {
                if (item.Selected)
                    PrimarySelectedCount++;
            }


            if (PrimarySelectedCount > 1 || i32PrimaryID==0)
            {

                ddlSecondary.Items.Clear();
                ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlSecondary.SelectedValue = "0";
                ddlSecondary.Enabled = false;

                ddlMinor.Items.Clear();
                ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlMinor.SelectedValue = "0";
                ddlMinor.Enabled = false;

            }
            else
            {
                ddlPrimary.SelectedValue = i32PrimaryID.ToString();
                ddlMinor.Enabled = true;

                ddlMinor.Items.Clear();
                ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlMinor.Items.Clear();
                ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlSecondary.Items.Clear();
                ddlSecondary.DataTextField = "Name";
                ddlSecondary.DataValueField = "Code";
                ddlSecondary.DataSource = objBLL.GetSecondaryByPrimaryID(i32PrimaryID);
                ddlSecondary.DataBind();
                ddlSecondary.Enabled = true;
                ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Description: Method to load minor category by selected secondary category
    /// </summary>
    /// <param name="i32NatureID">Minor category</param>

    protected void BindMinorBySecondatyID(Int32 i32SecondaryID)
    {
        try
        {
            
            int SecondarySelectedCount=0;
            foreach (ListItem item in ddlSecondary.Items)
            {
                if (item.Selected)
                    SecondarySelectedCount++;
            }

            if (SecondarySelectedCount > 1 || i32SecondaryID == 0)
            {

                ddlMinor.Items.Clear();
                ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlMinor.SelectedValue = "0";
                ddlMinor.Enabled = false;

            }
            else
            {
                    ddlSecondary.SelectedValue = i32SecondaryID.ToString();
                    ddlMinor.Items.Clear();
                    ddlMinor.DataTextField = "Name";
                    ddlMinor.DataValueField = "Code";
                    ddlMinor.DataSource = objBLL.GetMinorBySecondaryID(i32SecondaryID);
                    ddlMinor.DataBind();

                    ddlMinor.Enabled = true;
                    ddlMinor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
/// <summary>
/// Method to load inspection types
/// </summary>
    protected void BindInspectionType()
    {
        try
        {
            BLL_Infra_InspectionType onjInsp = new BLL_Infra_InspectionType();
            DataTable dtInsp = onjInsp.Get_InspectionTypeList();
            ddlInspectionType.DataSource = dtInsp;
            ddlInspectionType.DataTextField = "InspectionTypeName";
            ddlInspectionType.DataValueField = "InspectionTypeId";
            ddlInspectionType.DataBind();
            ddlInspectionType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    ///Descrition: Method to load vetting types
    /// </summary>
    protected void BindVettingType()
    {
        try
        {
            DataTable dtVetType = objVet.VET_Get_VettingTypeList();
            ddlVettingType.DataSource = dtVetType;
            ddlVettingType.DataTextField = "Vetting_Type_Name";
            ddlVettingType.DataValueField = "Vetting_Type_ID";
            ddlVettingType.DataBind();
            ddlVettingType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }




    /// <summary>
    ///Descrition: Method to load vetting types
    /// </summary>
    protected void BindVettingTypeForObservation()
    {
        try
        {
            DataTable dtVetType = objVet.VET_Get_VettingTypeList();
            lstObservationVettingType.DataSource = dtVetType;
            lstObservationVettingType.DataTextField = "Vetting_Type_Name";
            lstObservationVettingType.DataValueField = "Vetting_Type_ID";
            lstObservationVettingType.DataBind();
            lstObservationVettingType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }



    /// <summary>
    /// Bind observation categories
    /// </summary>
    public void BindVettingObservationCategories()
    {
        try
        {
            BLL_VET_Index oVT = new BLL_VET_Index();
            DataTable dt = oVT.VET_Get_ObservationCategories("Edit");
            lstObservationCategory.DataSource = dt;
            lstObservationCategory.DataTextField = "OBSCategory_Name";
            lstObservationCategory.DataValueField = "OBSCategory_ID";
            lstObservationCategory.DataBind();
            lstObservationCategory.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }





    /// <summary>
    /// Descrition: Event method to populate vetting information
    /// </summary>
    protected void chkVetting_OnCheckedChanged(object sender, EventArgs args)
    {
        ddlInspectionType.Items.Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
        CheckBox checkbox = (CheckBox)sender;
        if (checkbox.Checked)
        {
            IsVetting = 1;
            if (ddlInterval.Items.FindByValue("Monthly") != null)
            {
                ddlInterval.SelectedValue = "Monthly";
                ddlInterval.Enabled = false;
            }

            if (ddlUOM.Items.FindByValue("Numbers") != null)
            {
                ddlUOM.SelectedValue = "Numbers";
                ddlUOM.Enabled = false;
            }
            chkIsWorklist.Checked = false;
            chkInspection.Checked = false;
            chkObservation.Checked = false;
            BindVettingType();
        }
        else
        {
            IsVetting = 0;
            ddlInterval.Enabled = true;
            ddlUOM.Enabled = true;
        }
    }




    /// <summary>
    /// Descrition: Event method to populate vetting observation information
    /// </summary>
    protected void chkObservation_OnCheckedChanged(object sender, EventArgs args)
    {
        ddlInspectionType.Items.Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "toggleAdvSearch", "toggleAdvSearch()", true);
        CheckBox checkbox = (CheckBox)sender;
        if (checkbox.Checked)
        {
            IsInspection = 1;
            if (ddlInterval.Items.FindByValue("Monthly") != null)
            {
                ddlInterval.SelectedValue = "Monthly";
                ddlInterval.Enabled = false;
            }

            if (ddlUOM.Items.FindByValue("Numbers") != null)
            {
                ddlUOM.SelectedValue = "Numbers";
                ddlUOM.Enabled = false;
            }
            chkIsWorklist.Checked = false;
            chkVetting.Checked = false;
            chkInspection.Checked = false;
            BindVettingTypeForObservation();
            BindVettingObservationCategories();
        }
        else
        {
            IsInspection = 0;
            ddlInterval.Enabled = true;
            ddlUOM.Enabled = true;
        }
    }

    

    protected void ibtnDelete_click(object sender, CommandEventArgs  e)
    {
            DataTable dt = new DataTable();
            try
            {
                PI_Id = Convert.ToInt32(e.CommandArgument);
               // oBLL_Openings.DEL_Opening_Update(Progress_Id, GetSessionUserID());
                BindPIDetail();
            }
            catch { }

    }
    /// <summary>
    /// Description: Method to add/update PI settings including worklist settings
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        try
        {
            bool Activate_Scheduler = false;
            int WorklistPIId = 0;
            string interval = ddlInterval.SelectedItem.Value;
            string UOM = ddlUOM.SelectedValue;
            int result = 0;
            int Company_Id = Convert.ToInt32(Session["USERCOMPANYID"]);
            string Minor_Ids = "";
            if (ddlListSource.SelectedValue != "0")
                DataSource = ddlListSource.SelectedValue;
            int PI_status = Convert.ToInt32(ddlStatus.SelectedValue);
            if (chkIsWorklist.Checked)
            {
                Activate_Scheduler = chkActivate_Scheduler.Checked;
                Job_Type= string.Join(",", lstJOB.Items.Cast<ListItem>()
                         .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Assignor_Ids = string.Join(",", ddlAssignor.Items.Cast<ListItem>()
                .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Nature_Ids = string.Join(",", ddlNature.Items.Cast<ListItem>()
                 .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Primary_IDs = string.Join(",", ddlPrimary.Items.Cast<ListItem>()
                 .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Secondary_IDs = string.Join(",", ddlSecondary.Items.Cast<ListItem>()
                 .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Minor_Ids = string.Join(",", ddlMinor.Items.Cast<ListItem>()
                .Where(li => li.Selected).Select(x => x.Value).ToArray());


            }
            if (chkInspection.Checked)
            {
                Activate_Scheduler = chkActivateInspectionScheduler.Checked;
                InspectionType_IDs=string.Join(",", ddlInspectionType.Items.Cast<ListItem>()
                                .Where(li => li.Selected).Select(x => x.Value).ToArray());
            }

            if (chkVetting.Checked)
            {
                Activate_Scheduler = chkActivateVettingScheduler.Checked;
                VettingType_IDs = string.Join(",", ddlVettingType.Items.Cast<ListItem>()
                                .Where(li => li.Selected).Select(x => x.Value).ToArray());
            }


            if (chkObservation.Checked)
            {
                Activate_Scheduler = chkActivateObservationScheduler.Checked;
                VettingType_IDs = string.Join(",", lstObservationVettingType.Items.Cast<ListItem>()
                                .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Observation_Category = string.Join(",", lstObservationCategory.Items.Cast<ListItem>()
                .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Observation_Type = string.Join(",", lstObservationType.Items.Cast<ListItem>()
                .Where(li => li.Selected).Select(x => x.Value).ToArray());

                Observation_Risk_Level = string.Join(",", lstRiskLevel.Items.Cast<ListItem>()
                .Where(li => li.Selected).Select(x => x.Value).ToArray());
            }

            WorklistPIId = UDFLib.ConvertToInteger(ViewState["Worklist_PIID"]);

            if (Request.QueryString[0] != null)
            {
                PI_Id = Convert.ToInt32(Request.QueryString[0]);
                if (chkMeasuredForSBU.Checked)
                    MeasuredForSBU = 1;
                if (chkBallastLaden.Checked)
                    IncludeBL = 1;

                if (chkIsWorklist.Checked)
                    IsWorkList = 1;
                if (chkInspection.Checked)
                    IsInspection = 1;
                if (chkVetting.Checked)
                    IsVetting = 1;

                if (chkObservation.Checked)
                    IsVetting = 2;

                //To insert new PI
                if (Convert.ToInt32(Request.QueryString[0]) == 0)
                {
                    result = BLL_TMSA_PI.INS_PI_Details(txtName.Text, txtPICode.Text, interval, txtDescription.Text, null, txtContext.Text, UOM, DataSource, MeasuredForSBU, IncludeBL, PI_status, IsWorkList,IsInspection,IsVetting, GetSessionUserID());
                    PI_Id = result;
                }
                //To update PI setting
                else
                {
                    result = BLL_TMSA_PI.Update_PI(PI_Id, txtName.Text, txtPICode.Text, interval, txtDescription.Text, null, txtContext.Text, UOM, DataSource, MeasuredForSBU, IncludeBL, PI_status, IsWorkList,IsInspection,IsVetting, GetSessionUserID());
                }

                //To insert worklist PI  setting
                if (chkIsWorklist.Checked)
                {

                    DateTime? dtEffective = UDFLib.ConvertDateToNull(txtEffectivedate.Text);
                    if (WorklistPIId != null && WorklistPIId != 0)
                    {
                        result = BLL_TMSA_PI.Update_Worklist_PI(WorklistPIId, PI_Id, Company_Id, Job_Type, Assignor_Ids, Nature_Ids, Primary_IDs, Secondary_IDs, Minor_Ids, Activate_Scheduler, dtEffective, GetSessionUserID());
                    }
                    else
                        result = BLL_TMSA_PI.INS_Worklist_PI_Details(PI_Id, Company_Id, Job_Type, Assignor_Ids, Nature_Ids, Primary_IDs, Secondary_IDs, Minor_Ids, Activate_Scheduler, dtEffective, GetSessionUserID());



                }
                else if (chkInspection.Checked)//For inspection type
                {
                    Assignor_Ids="";
                    DateTime? dtEffective = UDFLib.ConvertDateToNull(txtMigrateFormInspection.Text);
                    if (WorklistPIId != null && WorklistPIId != 0)
                    {
                        result = BLL_TMSA_PI.Update_Inspection_PI(WorklistPIId, PI_Id, Company_Id,InspectionType_IDs, Assignor_Ids,  Activate_Scheduler, dtEffective, GetSessionUserID());
                    }
                    else
                        result = BLL_TMSA_PI.INS_Inspection_PI_Details(PI_Id, Company_Id, InspectionType_IDs, Assignor_Ids,Activate_Scheduler, dtEffective, GetSessionUserID());

                }
                else if (chkVetting.Checked)//For vetting type
                {
                    Assignor_Ids = "";
                    DateTime? dtEffective = UDFLib.ConvertDateToNull(txtMigrateFormVetting.Text);
                    if (WorklistPIId != null && WorklistPIId != 0)
                    {
                        result = BLL_TMSA_PI.Update_Vetting_PI(WorklistPIId, PI_Id, Company_Id, VettingType_IDs,Observation_Type,Observation_Category,Observation_Risk_Level, Assignor_Ids, Activate_Scheduler, dtEffective, GetSessionUserID());
                    }
                    else
                        result = BLL_TMSA_PI.INS_Vetting_PI_Details(PI_Id, Company_Id, VettingType_IDs, Observation_Type, Observation_Category, Observation_Risk_Level, Assignor_Ids, Activate_Scheduler, dtEffective, GetSessionUserID());

                }
                else if (chkObservation.Checked)//For observation setting 
                {
                    Assignor_Ids = "";
                    DateTime? dtEffective = UDFLib.ConvertDateToNull(txtMigrateFormObservation.Text);
                    if (WorklistPIId != null && WorklistPIId != 0)
                    {
                        result = BLL_TMSA_PI.Update_Vetting_PI(WorklistPIId, PI_Id, Company_Id, VettingType_IDs, Observation_Type, Observation_Category, Observation_Risk_Level, Assignor_Ids, Activate_Scheduler, dtEffective, GetSessionUserID());
                    }
                    else
                        result = BLL_TMSA_PI.INS_Vetting_PI_Details(PI_Id, Company_Id, VettingType_IDs, Observation_Type, Observation_Category, Observation_Risk_Level, Assignor_Ids, Activate_Scheduler, dtEffective, GetSessionUserID());

                }
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Description: Method to bind the PI intervals
    /// </summary>
    private void BindInterval()
    {
        DataTable dt = objKPI.Get_Intervals("");
        ddlInterval.DataSource = dt;
        ddlInterval.DataTextField = "Interval_Name";
        ddlInterval.DataValueField = "Interval_Name";
        ddlInterval.DataBind();
        ddlInterval.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    private void BindUnits()
    {
        DataTable dt = objKPI.Get_Units("");
        ddlUOM.DataSource = dt;
        ddlUOM.DataTextField = "Unit_Name";
        ddlUOM.DataValueField = "Unit_Name";
        ddlUOM.DataBind();
        ddlUOM.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
}
