using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using SMS.Properties;

public partial class Technical_Vetting_Vetting_AddObservationNotes : System.Web.UI.Page
{
   
    string Vetting_ID, Question_ID, Observation_ID, Vessel_ID, FleetCode, Vessel_Name, Vetting_Type_ID, Vetting_Type_Name, Vetting_Status;
    string Mode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                ViewState["FleetCode"] = "";
                ViewState["Vessel_ID"] = "";
                ViewState["Vetting_ID"] = "";
                ViewState["Question_ID"] = "";
                ViewState["Observation_ID"] = "";
                ViewState["Vessel_Name"] = "";
                ViewState["Vetting_Type_ID"] = "";
                ViewState["Vetting_Type_Name"] = "";
                ViewState["Mode"] = "";
           

                if (Request.QueryString["FleetCode"] != null)
                {
                    FleetCode = Request.QueryString["FleetCode"].ToString();
                    ViewState["FleetCode"] = Request.QueryString["FleetCode"].ToString();
                }
                if (Request.QueryString["Vessel_ID"] != null)
                {
                    Vessel_ID = Request.QueryString["Vessel_ID"].ToString();
                    ViewState["Vessel_ID"] = Request.QueryString["Vessel_ID"].ToString();
                }
                if (Request.QueryString["Vessel_Name"] != null)
                {
                    Vessel_Name = Request.QueryString["Vessel_Name"].ToString();
                    ViewState["Vessel_Name"] = Request.QueryString["Vessel_Name"].ToString();
                }
                if (Request.QueryString["Vetting_Type_ID"] != null)
                {
                    Vetting_Type_ID = Request.QueryString["Vetting_Type_ID"].ToString();
                    ViewState["Vetting_Type_ID"] = Request.QueryString["Vetting_Type_ID"].ToString();
                }
                if (Request.QueryString["Vetting_Type_Name"] != null)
                {
                    Vetting_Type_Name = Request.QueryString["Vetting_Type_Name"].ToString();
                    ViewState["Vetting_Type_Name"] = Request.QueryString["Vetting_Type_Name"].ToString();
                }
                if (Request.QueryString["Vetting_ID"] != null)
                {
                    Vetting_ID = Request.QueryString["Vetting_ID"].ToString();
                    ViewState["Vetting_ID"] = Request.QueryString["Vetting_ID"].ToString();

                }
                if (Request.QueryString["Question_ID"] != null)
                {
                    Question_ID = Request.QueryString["Question_ID"].ToString();
                    ViewState["Question_ID"] = Request.QueryString["Question_ID"].ToString();

                }
                if (Request.QueryString["Observation_ID"] != null)
                {
                    Observation_ID = Request.QueryString["Observation_ID"].ToString();
                    hdnQryStrObservationId.Value = Observation_ID;
                    ViewState["Observation_ID"] = Request.QueryString["Observation_ID"].ToString();
                    Mode = "Edit";

                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Mode"])))
                {
                    Mode = Request.QueryString["Mode"].ToString();
                    ViewState["Mode"] = Request.QueryString["Mode"].ToString();

                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Vetting_Status"])))
                {
                    Vetting_Status = Request.QueryString["Vetting_Status"].ToString();                  

                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Opn_Obs_Count"])))
                {

                    ViewState["Opn_Obs_Count"] = Request.QueryString["Opn_Obs_Count"].ToString();
                    lnkRelatedObs.Text = ViewState["Opn_Obs_Count"].ToString();
                    lnkRelatedObs.NavigateUrl = "Vetting_ObservationIndex.aspx?Question_ID=" + Question_ID + "&FleetCode=" + FleetCode + "&Vetting_Type_ID=" + Vetting_Type_ID + "&Status=Open" + "&Parent=AON";

                    if (lnkRelatedObs.Text == "0")
                    {
                        lnkRelatedObs.Enabled = false;
                    }
                }
                VET_Get_SectionListByVettingId();
                VET_Get_QuestionNoByVettingId();
                VET_Get_ObservationCategories();
                VET_Get_ObservationTypeList();
                if (Mode == "Add")
                {
                    txtObsDescription.Enabled = true;
                    ImgUpdate.Enabled = false;
                    ImgAddNewJob.Enabled = false;
                    ImglnkJob.Enabled = false;
                    ImgAddResponse.Enabled = false;
                    dvbadge.Visible = false;
                }
                if (Mode == "Edit")
                {
                    VET_Get_Observation();
                    //ImgAddNewJob.NavigateUrl = "../Worklist/AddNewJob.aspx?VID=" + ViewState["Vessel_ID"].ToString() + "&Vetting_ID=" + ViewState["Vetting_ID"].ToString() + "&Question_ID=" + ddlQuestion.SelectedValue + "&Observation_ID=" + ViewState["Observation_ID"].ToString() + "&WLID=0&OFFID=0";
                  //enable the edit button in read-only mode to view observation
                    if (Vetting_Status == "Completed")
                    {
                        ImgAddNewJob.Enabled = false;
                        ImglnkJob.Enabled = false;
                        ImgAddResponse.Enabled = false;
                        btnSave.Enabled = false;
                        BtnSaveClose.Enabled = false;
                        ImgAddResponse.Enabled = false;
                        ImgUpdate.Enabled = false;
                    }
                    else
                    {
                        ImgAddNewJob.Enabled = true;
                        ImglnkJob.Enabled = true;
                        ImgAddResponse.Enabled = true;
                    }                                  
                    dvbadge.Visible = true;
                    ImglnkJob.Attributes.Add("onClick", "PopupAssignJob(" + Observation_ID + "," + Vessel_ID + ")");
                    UpdPnlAddObservation_Naote.Update();

                }

                VET_Get_Response();
                VET_Get_ObsRelatedJobs();

               

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void GetRelatedOpenObservationCount(int QuestionID,int FleetCode)
    {
        BLL_VET_Index objBLLIndx = new BLL_VET_Index();
        DataTable dtOpenObsCount = objBLLIndx.VET_Get_RelatedOpenObsCount();

        string filterpenexpression = filterpenexpression = " Question_ID=" + UDFLib.ConvertToInteger(QuestionID) + " and FleetCode=" + UDFLib.ConvertToInteger(FleetCode);
        DataRow[] drOpnObsCount = dtOpenObsCount.Select(filterpenexpression);
        if (drOpnObsCount.Length <= 0)
        {
            lnkRelatedObs.Text = "0";
            lnkRelatedObs.Enabled = false;
        }
        else
        {
            lnkRelatedObs.Text = drOpnObsCount[0][0].ToString();
            lnkRelatedObs.Enabled = true;
        }
      
    }
    /// <summary>
    /// Method is used to check wheather user have access to this page or not.
    /// </summary>
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }
       
    }
    /// <summary>
    /// Method is used to bind Section list by vetting id
    /// </summary>
    public void VET_Get_SectionListByVettingId()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtSectionNo = objBLLIndx.VET_Get_SectionListByVettingId(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), ViewState["Mode"].ToString());
            ddlSection.Items.Clear();
            ddlSection.DataSource = dtSectionNo;
            ddlSection.DataTextField = "Section_No";
            ddlSection.DataValueField = "Section_No";
            ddlSection.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddlSection.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to bind question no. list by vetting id
    /// </summary>
    public void VET_Get_QuestionNoByVettingId()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtQuestionNo = objBLLIndx.VET_Get_QuestionNoByVettingId(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), ViewState["Mode"].ToString());
            ddlQuestion.Items.Clear();
            ddlQuestion.DataSource = dtQuestionNo;
            ddlQuestion.DataTextField = "Question_No";
            ddlQuestion.DataValueField = "ID";
            ddlQuestion.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddlQuestion.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to bind observation categories
    /// </summary>
    public void VET_Get_ObservationCategories()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtCategory = objBLLIndx.VET_Get_ObservationCategories(ViewState["Mode"].ToString());
            ddlCategory.Items.Clear();
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataTextField = "OBSCategory_Name";
            ddlCategory.DataValueField = "OBSCategory_ID";
            ddlCategory.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddlCategory.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }


    /// <summary>
    /// Method is used to bind observation type list
    /// </summary>
    public void VET_Get_ObservationTypeList()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtObsType = objBLLIndx.VET_Get_ObservationTypeList();
            ddlType.Items.Clear();
            ddlType.DataSource = dtObsType;
            ddlType.DataTextField = "ObsTypName";
            ddlType.DataValueField = "ID";
            ddlType.DataBind();
            ddlType.Items.FindByText(Convert.ToString(dtObsType.Rows[1]["ObsTypName"])).Selected = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void ddlQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            if (ddlQuestion.SelectedValue != "0")
            {
                ViewState["ddlQuestionNo"] = ddlQuestion.SelectedValue;
                DataTable dtQuestionNo = objBLLIndx.VET_Get_QuestionByQuestionNo(UDFLib.ConvertToInteger(ViewState["ddlQuestionNo"].ToString()));
                if (dtQuestionNo.Rows.Count > 0)
                {
                    if (Convert.ToString(dtQuestionNo.Rows[0]["Question"]) != "")
                    {
                        lblQuestion.Text = dtQuestionNo.Rows[0]["Question"].ToString();
                    }
                }

                GetRelatedOpenObservationCount(UDFLib.ConvertToInteger(ddlQuestion.SelectedValue), UDFLib.ConvertToInteger(ViewState["FleetCode"].ToString()));
                dvbadge.Visible = true;
                lnkRelatedObs.Enabled = true;
                lnkRelatedObs.NavigateUrl = "Vetting_ObservationIndex.aspx?Question_ID=" + ddlQuestion.SelectedValue + "&FleetCode=" + ViewState["FleetCode"].ToString() + "&Vetting_Type_ID=" + ViewState["Vetting_Type_ID"].ToString() + "&Status=Open" + "&Parent=AON";
            }
            else
            {
                lblQuestion.Text = "";
                dvbadge.Visible = false ;
                lnkRelatedObs.Enabled = false ;
            }
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to get observation details for display
    /// </summary>
    public void VET_Get_Observation()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtObs = objBLLIndx.VET_Get_Observation(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["Question_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()));
            
            if (dtObs.Rows.Count > 0)
            {
                if (Convert.ToString(dtObs.Rows[0]["Section_No"]) != "")
                {
                    ddlSection.ClearSelection();
                    ddlSection.Items.FindByText(Convert.ToString(dtObs.Rows[0]["Section_No"])).Selected = true;
                }
                if (Convert.ToString(dtObs.Rows[0]["Question_No"]) != "")
                {
                    VET_Get_QuestionByVettingId_SectionNo();
                    ddlQuestion.ClearSelection();
                    ddlQuestion.Items.FindByText(Convert.ToString(dtObs.Rows[0]["Question_No"])).Selected = true;
                }
                if (Convert.ToString(dtObs.Rows[0]["Category"]) != "")
                {
                    ddlCategory.ClearSelection();
                    ddlCategory.Items.FindByText(Convert.ToString(dtObs.Rows[0]["Category"])).Selected = true;
                }
                if (Convert.ToString(dtObs.Rows[0]["Risk_Level"]) != "")
                {
                    ddlRiskLevel.ClearSelection();
                    ddlRiskLevel.Items.FindByText(Convert.ToString(dtObs.Rows[0]["Risk_Level"])).Selected = true;
                }
                if (Convert.ToString(dtObs.Rows[0]["Status"]) != "")
                {
                    ddlStatus.ClearSelection();
                    ddlStatus.Items.FindByText(Convert.ToString(dtObs.Rows[0]["Status"])).Selected = true;
                }
                if (Convert.ToString(dtObs.Rows[0]["ObsType"]) != "")
                {
                    ddlType.ClearSelection();
                    ddlType.Items.FindByText(Convert.ToString(dtObs.Rows[0]["ObsType"])).Selected = true;
                }
                if (Convert.ToString(dtObs.Rows[0]["ObsDescription"]) != "")
                {                 
                    txtObsDescription.Text = dtObs.Rows[0]["ObsDescription"].ToString();
                }
                if (Convert.ToString(dtObs.Rows[0]["Question"]) != "")
                {
                    lblQuestion.Text = dtObs.Rows[0]["Question"].ToString();                 
                }
               
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void ImgUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
         
            txtObsDescription.Enabled = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to get response of particular vetting and observation.
    /// </summary>
    public void VET_Get_Response()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataSet ds = new DataSet();
            ds = objBLLIndx.VET_Get_Response(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()));
            if (ds != null)
            {
               
                    gvResponse.DataSource = ds.Tables[0];
                    gvResponse.DataBind();

                
               
                
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to get observation related worklist jobs of particular vetting and observation.
    /// </summary>
    public void VET_Get_ObsRelatedJobs()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataSet ds = new DataSet();
            ds = objBLLIndx.VET_Get_ObsRelatedJobs(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()));
            if (ds != null)
            {
                    gvRelatedJob.DataSource = ds.Tables[0];
                    gvRelatedJob.DataBind();
              
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to insert/update observation note details
    /// </summary>
    /// <param name="Mode">Mode of operation. Value is Add or Edit</param>
    public void VET_Ins_Observation_Note(string Mode)
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            if (Mode == "Add")                
            {
                DataTable dtObservation=new DataTable();
                dtObservation = objBLLIndx.VET_Ins_Observation_Note(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertToInteger(ddlQuestion.SelectedValue), UDFLib.ConvertToInteger(ddlType.SelectedValue), txtObsDescription.Text.Trim(), UDFLib.ConvertToInteger(ddlCategory.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRiskLevel.SelectedValue), ddlStatus.SelectedItem.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                if (dtObservation.Rows.Count > 0)
                {
                    hdnObservationId.Value = dtObservation.Rows[0]["Observation_ID"].ToString();
                    hdnQryStrObservationId.Value = dtObservation.Rows[0]["Observation_ID"].ToString(); 
                    ViewState["Observation_ID"] = hdnObservationId.Value;
                    
                  
                }
                string jsSqlError2 = "alert('Observation saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
            }
            else if (Mode == "Edit")
            {
               
                objBLLIndx.VET_Upd_Observation_Note(UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()), UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertToInteger(ddlQuestion.SelectedValue), UDFLib.ConvertToInteger(ddlType.SelectedValue), txtObsDescription.Text.Trim(), UDFLib.ConvertToInteger(ddlCategory.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRiskLevel.SelectedValue), ddlStatus.SelectedItem.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                string jsSqlError2 = "alert('Observation updated successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Method is used to  control enable false
    /// </summary>
    protected void EnableDisableControls()
    {
        txtObsDescription.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    { 
        try
        {

            VET_Ins_Observation_Note(ViewState["Mode"].ToString());
            ViewState["Mode"] = "Edit";
        

                VET_Get_Observation();
               // ImgAddNewJob.NavigateUrl = "../Worklist/AddNewJob.aspx?VID=" + ViewState["Vessel_ID"].ToString() + "&Vetting_ID=" + ViewState["Vetting_ID"].ToString() + "&Question_ID=" + ddlQuestion.SelectedValue + "&Observation_ID=" + ViewState["Observation_ID"].ToString() + "&WLID=0&OFFID=0";

                ImgAddNewJob.Enabled = true;
                ImglnkJob.Enabled = true;
                ImgAddResponse.Enabled = true;
                dvbadge.Visible = true;
                ImglnkJob.Attributes.Add("onClick", "PopupAssignJob(" + ViewState["Observation_ID"].ToString() + "," + ViewState["Vessel_ID"].ToString() + ")");
                UpdPnlAddObservation_Naote.Update();
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnJobHidden_Click(object sender, EventArgs e)
    {
        VET_Get_ObsRelatedJobs();
    }

    protected void BtnResHidden_Click(object sender, EventArgs e)
    {
        VET_Get_Response();
    }

    protected void gvResponse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                Label lblCDate = (Label)e.Row.FindControl("lblCDate");
                lblCDate.Text = UDFLib.ConvertUserDateFormat(lblCDate.Text, UDFLib.GetDateFormat());
                if (Vetting_Status == "Completed")
                {
                    ImageButton ImgAttatch = (ImageButton)e.Row.FindControl("ImgAttatch");
                    ImgAttatch.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void gvRelatedJob_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                Label lblDateRaised = (Label)e.Row.FindControl("lblDateRaised");

                HiddenField hdnWLID = (HiddenField)e.Row.FindControl("hdnWLID");
                HiddenField hdnOFFID = (HiddenField)e.Row.FindControl("hdnOFFID");
                HiddenField hdnVID = (HiddenField)e.Row.FindControl("hdnVID");
             
                lblDateRaised.Text = UDFLib.ConvertUserDateFormat(lblDateRaised.Text, UDFLib.GetDateFormat());
                HyperLink lnkVerJobsCnt = (HyperLink)e.Row.FindControl("lnkVerJobsCnt");
             
                lnkVerJobsCnt.NavigateUrl = "../Worklist/ViewJob.aspx?Vetting_ID=" + ViewState["Vetting_ID"].ToString() + "&WLID=" + hdnWLID.Value + "&OFFID=" + hdnOFFID.Value + "&VID=" + hdnVID.Value;
                if (Vetting_Status == "Completed")
                {
                    ImageButton ImglnkJob = (ImageButton)e.Row.FindControl("ImglnkJob");
                    ImglnkJob.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string js = "parent.CloseNote_Observation();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closepopup", js, true);
    }

  
    protected void gvRelatedJob_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            int OBSJobID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
            int res = objBLLIndx.VET_Upd_UnlinkWorklistJobs(OBSJobID, UDFLib.ConvertToInteger(Session["USERID"].ToString()));
            VET_Get_ObsRelatedJobs();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvRelatedJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string js = "window.open(../Worklist/AddNewJob.aspx?Vetting_ID=" + ViewState["Vetting_ID"].ToString() + ")";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsNewWindow", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        try
        {
            VET_Ins_Observation_Note(ViewState["Mode"].ToString());
            string js = "parent.CloseNote_Observation();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closepopuponsave", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dvbadge.Visible = false;
            lblQuestion.Text = "";
            if (ddlSection.SelectedValue != "0")
            {
                ddlQuestion.Items.Clear();
                VET_Get_QuestionByVettingId_SectionNo();
            }
            else
            {
                VET_Get_QuestionNoByVettingId();                
               
            }
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    public void VET_Get_QuestionByVettingId_SectionNo()
    {
        BLL_VET_Index objBLLIndx = new BLL_VET_Index();
        ddlQuestion.DataSource = objBLLIndx.VET_Get_QuestionByVettingId_SectionNo(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), UDFLib.ConvertToInteger(ddlSection.SelectedValue), ViewState["Mode"].ToString());
        ddlQuestion.DataTextField = "Question_No";
        ddlQuestion.DataValueField = "ID";
        ddlQuestion.DataBind();
        ddlQuestion.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void ImgAddNewJob_Click(object sender, EventArgs e)
    {
      
        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", " window.open('../Worklist/AddNewJob.aspx?VID=" + ViewState["Vessel_ID"].ToString() + "&Vetting_ID=" + ViewState["Vetting_ID"].ToString() + "&Question_ID=" + ddlQuestion.SelectedValue + "&Observation_ID=" + ViewState["Observation_ID"].ToString() + "&WLID=0&OFFID=0', '_blank');", true);
       
    }
}