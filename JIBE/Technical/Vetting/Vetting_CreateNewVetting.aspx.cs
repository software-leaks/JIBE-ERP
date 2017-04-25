using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
using SMS.Business.Crew;
using SMS.Properties;
using System.IO;
using System.Text;


public partial class Technical_Vetting_Vetting_CreateNewVetting : System.Web.UI.Page
{
      
    public string VettingMode = "";
    public string AttachPath = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (GetSessionUserID() == 0)
        {
            divLoggout.Visible = true;
            divMain.Visible = false;
        }
        else
        {
            divLoggout.Visible = false;
            divMain.Visible = true;

            try
            {
                setDateFormat();
                if (!IsPostBack)
                {
                   // GUIDSession.Value = Guid.NewGuid().ToString();

                    FillVesselDropdown();
                    FillOilMajorDropdown();
                    FillPortDropdown();
                    FillVettingTypeDropdown(Convert.ToInt32(ddlVessel.SelectedValue));
                   
                    if (Request.QueryString["Vetting_Type_ID"] != null && Request.QueryString["Vetting_Type_ID"] != "0")
                    {

                        ddlType.SelectedValue = Request.QueryString["Vetting_Type_ID"].ToString();
                        FillInspectorDropdown(UDFLib.ConvertToInteger(ddlType.SelectedValue));

                    }

                    if ((Request.QueryString["Vetting_ID"] != null && Request.QueryString["Mode"].ToString().Trim() == "PV"))
                    {

                        VettingMode = "PV";
                        ViewState["Vetting_ID"] = Convert.ToInt32(Request.QueryString["Vetting_ID"]);
                        BindPerformVetting(Convert.ToInt32(ViewState["Vetting_ID"].ToString()));
                        ddlVessel.Enabled = false;
                        ddlType.Enabled = false;
                        lblVettingName.Visible = true;
                        txtVettingName.Visible = true;
                        txtVettingName.ReadOnly = true;
                        lblupload.Visible = true;
                        btnAttachment.Visible = true;
                        trQuestionnaire.Attributes.Add("style", "display:none");
                        btnAttachment.Attributes.Add("Onclick", "AddAttachment(" + Request.QueryString["Vetting_ID"].ToString() + ");");
                        btnCreatVetting.Text = "Save and add questions";
                        txtVettingName.Attributes.Add(" onMouseOver", "js_ShowToolTip('" + txtVettingName.Text + "', event, this);");
                        txtDays.Text = "1";
                    }
                    else
                    {
                        VettingMode = "CV";
                        ddlVessel.Enabled = true;
                        ddlType.Enabled = true;
                        txtVettingName.Visible = false;
                        lblQuestionaireMan.Visible = true;
                        lblQuestionaire.Visible = true;
                        lblupload.Visible = false;
                        btnAttachment.Visible = false;
                        txtResponseDate.Text = "";
                        btnCreatVetting.Text = "Create Vetting";
                        lblInspMan.Attributes.Add("style", "display:none");
                        trNoDays.Attributes.Add("style", "display:none");
                        trresponseNext.Attributes.Add("style", "display:none");

                    }

                    if (Request.QueryString["Port"] != null && Request.QueryString["Port"] != "0")
                    {
                        string PortID = Request.QueryString["Port"].ToString();
                        FillPortDropdown();
                        ddlPort.Select(PortID);
                        updatePort.Update();

                    }
                    if (Request.QueryString["PortCallID"] != null && Request.QueryString["PortCallID"] != "0")
                    {
                        hdfPorCallID.Value = Request.QueryString["PortCallID"].ToString();

                    }
                    BindCreateVetting();


                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }
    }
    /// <summary>
    /// Method is used to bind vetting details when mode is perform vetting
    /// </summary>
    /// <param name="Vetting_ID">selected Vetting id</param>
    protected void BindPerformVetting(int Vetting_ID)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
            DataTable dtVetting = objIndex.VET_Get_Perform_Vetting_Detail(Vetting_ID);
            if (dtVetting.Rows.Count > 0)
            {
                ddlVessel.SelectedValue = dtVetting.Rows[0]["Vessel_ID"].ToString();
                txtVettingName.Text = dtVetting.Rows[0]["Vetting_Name"].ToString();
                FillVettingTypeDropdown(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
                ddlType.SelectedValue = dtVetting.Rows[0]["Vetting_Type_ID"].ToString();
                if (ddlType.SelectedItem.Text == "SIRE")
                {
                    trImportXML.Visible = true;
                }
                txtDate.Text = UDFLib.ConvertUserDateFormat(dtVetting.Rows[0]["Vetting_Date"].ToString());
                txtDays.Text = dtVetting.Rows[0]["No_Of_Days"].ToString();
                ddlOilMajor.SelectedValue = dtVetting.Rows[0]["Oil_Major_ID"].ToString();
                AddInspectorImg();
                FillInspectorDropdown(UDFLib.ConvertToInteger(ddlType.SelectedValue));
                ddlInspector.SelectedValue = dtVetting.Rows[0]["Inspector_ID"].ToString();
                ddlPort.Select(dtVetting.Rows[0]["Port_ID"].ToString());
                hdfPorCallID.Value = dtVetting.Rows[0]["Port_Call_ID"].ToString();
                FillQuestionaireDropdown(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToInteger(ddlType.SelectedValue));
                ddlQuestionaire.SelectedValue = dtVetting.Rows[0]["Questionnaire_ID"].ToString();
                hdnQuestionnaireID.Value = dtVetting.Rows[0]["Questionnaire_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Method is used to bind vetting's already selected field after closing port pop up.
    /// </summary>
    protected void BindCreateVetting()
    {
        try
        {
            if (Request.QueryString["VesselID"] != null && Request.QueryString["VesselID"] != "0")
            {
                ddlVessel.SelectedValue = Request.QueryString["VesselID"].ToString();
                FillVettingTypeDropdown(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));

            }
            if (Request.QueryString["TypeID"] != null && Request.QueryString["TypeID"] != "0")
            {
                ddlType.SelectedValue = Request.QueryString["TypeID"].ToString();
                FillInspectorDropdown(UDFLib.ConvertToInteger(ddlType.SelectedValue));
                FillQuestionaireDropdown(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToInteger(ddlType.SelectedValue));
            }
         
            if (Request.QueryString["Date"] != null && Request.QueryString["Date"] != "")
            {
                    txtDate.Text = Request.QueryString["Date"];
            }
            if (Request.QueryString["Questionnaire"] != null && Request.QueryString["Questionnaire"] != "0")
            {

                ddlQuestionaire.SelectedValue = Request.QueryString["Questionnaire"].ToString();
            }
            if (Request.QueryString["OilMajor"] != null && Request.QueryString["OilMajor"] != "0")
            {
                ddlOilMajor.SelectedValue = Request.QueryString["OilMajor"].ToString();
            }
           
            if (Request.QueryString["Inspector"] != null && Request.QueryString["Inspector"] != "0")
            {
                ddlInspector.SelectedValue = Request.QueryString["Inspector"].ToString();
            }
            
            if (Request.QueryString["NoDays"] != null && Request.QueryString["NoDays"] != "")
            {
                txtDays.Text = Request.QueryString["NoDays"].ToString();
            }
            if (Request.QueryString["RespNextDue"] != null && Request.QueryString["RespNextDue"] != "")
            {
                txtResponseDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(Request.QueryString["RespNextDue"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
            }
            AddInspectorImg();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Method is used to bind vessel dropdown
    /// </summary>
    public void FillVesselDropdown()
    {
        try
        {
            BLL_Infra_VesselLib objInfra = new BLL_Infra_VesselLib();
            DataTable dtVessel = objInfra.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            if (dtVessel.Rows.Count > 0)
            {
                ddlVessel.DataSource = dtVessel;
                ddlVessel.DataTextField = "Vessel_Name";
                ddlVessel.DataValueField = "Vessel_ID";
                ddlVessel.DataBind();
                ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to bind vetting type dropdown
    /// </summary>
    public void FillVettingTypeDropdown(int VesselID)
    {
        try
        {
            DataTable dtVettingType = new DataTable();
            BLL_VET_VettingLib objVett = new BLL_VET_VettingLib();
            if (VettingMode == "PV")
            {
                dtVettingType = objVett.VET_Get_VettingTypeList(); 
            }
            else
            {
                dtVettingType = objVett.VET_Get_VettingType_BySetting(VesselID);
            }
            ViewState["VettingType"] = dtVettingType;
            uplCreateVetting.Update();
            ddlType.Items.Clear();
            if (dtVettingType.Rows.Count > 0)
            {
                ddlType.Items.Clear();
                ddlType.DataSource = dtVettingType;
                ddlType.DataTextField = "Vetting_Type_Name";
                ddlType.DataValueField = "Vetting_Type_ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("-SELECT-", "0"));

            }
            else
            {
                ddlType.Items.Clear();
                ddlType.DataSource = null;
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to bind Questionire dropdown
    /// </summary>
    public void FillQuestionaireDropdown(int VesselID, int Vetting_Type_ID)
    {
        try
        {
            DataTable dtQuestionaire = new DataTable();
            BLL_VET_Index objIndex = new BLL_VET_Index();
            dtQuestionaire = objIndex.VET_GET_Published_QuestionnireList(VesselID, Vetting_Type_ID);
            ddlQuestionaire.ClearSelection();
            if (dtQuestionaire.Rows.Count > 0)
            {
                ddlQuestionaire.Items.Clear();
                ddlQuestionaire.DataSource = dtQuestionaire;
                ddlQuestionaire.DataTextField = "Name";
                ddlQuestionaire.DataValueField = "Questionnaire_ID";
                ddlQuestionaire.DataBind();
                ddlQuestionaire.Items.Insert(0, new ListItem("-SELECT-", "0"));

            }
            else
            {
                ddlQuestionaire.Items.Clear();
                ddlQuestionaire.DataSource = null;
                ddlQuestionaire.DataBind();
                ddlQuestionaire.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to bind Oil major dropdown
    /// </summary>
    public void FillOilMajorDropdown()
    {
        try
        {
            DataSet dsOilMajor = new DataSet();
            BLL_Crew_Admin objCrew = new BLL_Crew_Admin();
            dsOilMajor = objCrew.GetOilMajor();
            if (dsOilMajor.Tables.Count > 0)
            {
                if (dsOilMajor.Tables[0].Rows.Count > 0)
                {
                    ddlOilMajor.DataSource = dsOilMajor.Tables[0];
                    ddlOilMajor.DataTextField = dsOilMajor.Tables[0].Columns["Display_Name"].ToString();
                    ddlOilMajor.DataValueField = dsOilMajor.Tables[0].Columns["ID"].ToString();
                    ddlOilMajor.DataBind();
                    ddlOilMajor.Items.Insert(0, new ListItem("-SELECT-", "0"));

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Methos is used to bind inspector dropdown according to the vetting type
    /// </summary>
    /// <param name="Vetting_Type_ID">Selected vetting type</param>
    public void FillInspectorDropdown(int Vetting_Type_ID)
    {
        try
        {
            DataTable dtInspector = new DataTable();
            BLL_VET_Index objIndex = new BLL_VET_Index();
            dtInspector = objIndex.VET_Get_InspectorListByVettingType(Vetting_Type_ID);
            if (dtInspector.Rows.Count > 0)
            {
                ddlInspector.DataSource = dtInspector;
                ddlInspector.DataTextField = "NAME";
                ddlInspector.DataValueField = "UserID";
                ddlInspector.DataBind();
                ddlInspector.Items.Insert(0, new ListItem("-SELECT-", "0"));

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to bind port dropdown
    /// </summary>
    public void FillPortDropdown()
    {
        try
        {
            DataTable dtPort = new DataTable();
            BLL_VET_Index objIndex = new BLL_VET_Index();
            dtPort = objIndex.VET_Get_PortList();
            if (dtPort.Rows.Count > 0)
            {
                ddlPort.DataSource = dtPort;
                ddlPort.DataTextField = "PORT_NAME";
                ddlPort.DataValueField = "PORT_ID";
                ddlPort.DataBind();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AddInspectorImg();
            if (ddlType.SelectedIndex != 0)
            {
                FillInspectorDropdown(UDFLib.ConvertToInteger(ddlType.SelectedValue));
                FillQuestionaireDropdown(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToInteger(ddlType.SelectedValue));
                uplCreateVetting.Update();
            }
            else
            {
                ddlInspector.DataSource = null;
                ddlInspector.Items.Clear();
                ddlInspector.DataBind();
                ImgAdd.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Methos is used to enabled add inspector image according to type isinternal value
    /// </summary>
    protected void AddInspectorImg()
    {
        try
        {
            string isInternal = "";
            DataTable dtVettingType = (DataTable)ViewState["VettingType"];
            if (dtVettingType.Rows.Count > 0)
            {
                string filtervettingType = " Vetting_Type_ID=" + ddlType.SelectedValue;
                DataRow[] drVettingType = dtVettingType.Select(filtervettingType);

                isInternal = drVettingType[0]["IsInternal"].ToString();

                if (isInternal != "0" && isInternal != "")
                {
                    ImgAdd.Enabled = false;
                }
                else
                {
                    ImgAdd.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCreatVetting_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index();
            int ddlportID = 0;         

           
            if (ddlPort.SelectedValues.Rows.Count > 0)
            {
                if (ddlPort.SelectedValues.Rows.Count > 1)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "alert('Please select only one port.');", true);
                    return;
                }
                else
                {                  
                        ddlportID = UDFLib.ConvertToInteger(ddlPort.SelectedValues.Rows[0][0]);                    
                }
            }

                  if (ViewState["Vetting_ID"] != null)
                    {                      

                        if (txtResponseDate.Text != "")
                        {
                            if (UDFLib.ConvertToDate(txtResponseDate.Text) < UDFLib.ConvertToDate(txtDate.Text))
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Response-Next due date is can not be less than vetting date.');", true);
                                return;
                            }
                        }
                       
                            DataTable dtVetting = objIndex.VET_Get_ExistVetting(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToDate(txtDate.Text), UDFLib.ConvertToInteger(ddlType.SelectedValue), UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()));
                            if (dtVetting.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", "alert('Vetting already exists with same vessel,same type and same date.');", true);
                                return;
                            }
                            else
                            {
                                if (FileUpXMLReport.HasFile)
                                {
                                    int Vetting_ID = Convert.ToInt32(ViewState["Vetting_ID"]);

                                    int IsDataInValid = 0;
                                    int IsObsExists = 0;
                                    var FileExtension = "." + Path.GetExtension(FileUpXMLReport.PostedFile.FileName).Substring(1);
                                    if (FileExtension != ".xml")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('File type should be only xml.');", true);

                                    }
                                    else
                                    {
                                        DataSet ds = new DataSet();
                                        DataTable dt = new DataTable();
                                        string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\ImportedObservation\\");

                                        if (!Directory.Exists(Server.MapPath("~/uploads/Vetting/ImportedObservation")))
                                            Directory.CreateDirectory(Server.MapPath("~/uploads/Vetting/ImportedObservation"));

                                        Guid GUID = Guid.NewGuid();
                                        string AttachPath = "VET_ImportObs" + GUID.ToString() + FileExtension;
                                        ViewState["AttachPath"] = AttachPath;
                                        FileUpXMLReport.SaveAs(sPath + AttachPath);
                                        ds.ReadXml(sPath + AttachPath);
                                        Session["VET_ImportDataset" + GUIDSession.Value.ToString()] = ds;
                                        if (ds.Tables.Count > 0)
                                        {
                                            ImportObservation objImportObs = new ImportObservation();
                                            if (string.IsNullOrWhiteSpace(objImportObs.ImportValidation(ds, Vetting_ID)))
                                            {
                                                Dictionary<string, string> objDicResult = objImportObs.ImportValueValidation(ds, ddlQuestionaire.SelectedValue, ddlVessel.SelectedItem.Text, ddlPort.SelectedTexts.Rows[0]["SelectedText"].ToString(), txtDate.Text, Vetting_ID, ref IsDataInValid, ref IsObsExists);
                                                if (objDicResult.Keys.Contains("QUESTIONNAIRE"))
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + objDicResult["QUESTIONNAIRE"] + "');", true);
                                                }
                                                else if (string.IsNullOrWhiteSpace(objDicResult["ERRORMESSAGE"]))
                                                {


                                                    if (IsDataInValid == 0 && IsObsExists == 0)
                                                    {

                                                        if (string.IsNullOrWhiteSpace(objImportObs.SaveImportObservation(ds, ddlQuestionaire.SelectedValue, Vetting_ID)))
                                                        {
                                                            objIndex.VET_Ins_AttatmentImport(Vetting_ID, Path.GetFileName(FileUpXMLReport.PostedFile.FileName), AttachPath, GetSessionUserID());
                                                            objIndex.VET_Upd_PerformVettingDetails(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), txtVettingName.Text, UDFLib.ConvertToDate(txtDate.Text), UDFLib.ConvertToInteger(ddlType.SelectedValue), ddlType.SelectedItem.Text, UDFLib.ConvertToInteger(ddlInspector.SelectedValue), UDFLib.ConvertToInteger(ddlOilMajor.SelectedValue), UDFLib.ConvertToInteger(ddlPort.SelectedValues.Rows[0][0]), UDFLib.ConvertIntegerToNull(hdfPorCallID.Value), UDFLib.ConvertToInteger(txtDays.Text), UDFLib.ConvertToDate(txtResponseDate.Text), GetSessionUserID());
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Perform Vetting saved successfully');", true);
                                                            ClearFields();
                                                            Session["VET_ImportDataset" + GUIDSession.Value.ToString()] = null;
                                                            string js = "parent.UpdatePageAfterSave(" + ViewState["Vetting_ID"].ToString() + ");";
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Error while importing.');", true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Confirm", "ImportConfirm(" + IsDataInValid + "," + IsObsExists + ",'" + ddlVessel.SelectedItem.Text + "','" + ddlPort.SelectedTexts.Rows[0]["SelectedText"].ToString() + "','" + txtDate.Text + "');", true);
                                                    }

                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file.');", true);

                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file.');", true);
                                            }

                                        }
                                    }

                                }
                                else
                                {

                                    objIndex.VET_Upd_PerformVettingDetails(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), txtVettingName.Text, UDFLib.ConvertToDate(txtDate.Text), UDFLib.ConvertToInteger(ddlType.SelectedValue), ddlType.SelectedItem.Text, UDFLib.ConvertToInteger(ddlInspector.SelectedValue), UDFLib.ConvertIntegerToNull(ddlOilMajor.SelectedValue), ddlportID, UDFLib.ConvertIntegerToNull(hdfPorCallID.Value), UDFLib.ConvertToInteger(txtDays.Text), UDFLib.ConvertDateToNull(txtResponseDate.Text), GetSessionUserID());
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Perform Vetting saved successfully');", true);
                                    ClearFields();
                                    string js = "parent.UpdatePageAfterSave(" + ViewState["Vetting_ID"].ToString() + ");";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
                                }

                            }
                        //}
                    }
                    else
                    {
                        DataTable dtVetting = objIndex.VET_Get_ExistVetting(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToDate(txtDate.Text), UDFLib.ConvertToInteger(ddlType.SelectedValue), null);
                        if (dtVetting.Rows.Count > 0)
                        {
                            string jsSqlError3 = "alert('Vetting already exists with same vessel,same type and same date.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                        }
                        else
                        {
                            int Vetting_ID = 0;
                            int ReturnValue = 0;                         
                            Vetting_ID = objIndex.VET_Ins_Vetting(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToDate(txtDate.Text), UDFLib.ConvertToInteger(ddlType.SelectedValue), ddlType.SelectedItem.Text, UDFLib.ConvertToInteger(ddlQuestionaire.SelectedValue), UDFLib.ConvertIntegerToNull(ddlOilMajor.SelectedValue), UDFLib.ConvertToInteger(ddlInspector.SelectedValue), ddlportID , UDFLib.ConvertIntegerToNull(hdfPorCallID.Value), GetSessionUserID(), ref ReturnValue);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Vetting saved successfully');", true);
                            ClearFields();
                            string js = "parent.VettingDetail(" + Vetting_ID + ");";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
                        }
                    }
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method is used to set date format
    /// </summary>
    public void setDateFormat()
    {
        calDate.Format = UDFLib.GetDateFormat();
        CalResponseDate.Format = UDFLib.GetDateFormat();
    }

    /// <summary>
    /// Method is used to clear controls after save vetting
    /// </summary>
    public void ClearFields()
    {
        try
        {
            ddlVessel.SelectedIndex = 0;
            txtDate.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            ddlQuestionaire.SelectedIndex = 0;
            ddlOilMajor.SelectedIndex = 0;
            ddlInspector.SelectedIndex = 0;
            ddlPort.ClearSelection();
            txtResponseDate.Text = string.Empty;
            ImgAdd.Enabled = false;
            txtDays.Text = string.Empty;
            txtVettingName.Text = string.Empty;
            AttachPath = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string js = "parent.UpdatePageAfeterSave();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnInspHidden_Click(object sender, EventArgs e)
    {
        try
        {
            uplCreateVetting.Update();
            FillInspectorDropdown(UDFLib.ConvertToInteger(ddlType.SelectedValue));
            ddlInspector.SelectedValue = hdnInspectorId.Value;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillVettingTypeDropdown(Convert.ToInt32(ddlVessel.SelectedValue));
            FillQuestionaireDropdown(UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToInteger(ddlType.SelectedValue));
            uplCreateVetting.Update();
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnconfirm_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_Index objIndex = new BLL_VET_Index(); 
                string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\ImportedObservation\\");

                DataSet ds = new DataSet();

                ds.ReadXml(sPath + ViewState["AttachPath"].ToString());
                ImportObservation objImportObs = new ImportObservation();               

                if (string.IsNullOrWhiteSpace(objImportObs.SaveImportObservation(ds, ddlQuestionaire.SelectedValue, Convert.ToInt32(ViewState["Vetting_ID"]))))
                {
                    objIndex.VET_Ins_AttatmentImport(Convert.ToInt32(ViewState["Vetting_ID"]), Path.GetFileName(FileUpXMLReport.PostedFile.FileName), AttachPath, GetSessionUserID());
                    objIndex.VET_Upd_PerformVettingDetails(UDFLib.ConvertToInteger(ViewState["Vetting_ID"].ToString()), txtVettingName.Text, UDFLib.ConvertToDate(txtDate.Text), UDFLib.ConvertToInteger(ddlType.SelectedValue), ddlType.SelectedItem.Text, UDFLib.ConvertToInteger(ddlInspector.SelectedValue), UDFLib.ConvertIntegerToNull(ddlOilMajor.SelectedValue), UDFLib.ConvertToInteger(ddlPort.SelectedValues.Rows[0][0]), UDFLib.ConvertIntegerToNull(hdfPorCallID.Value), UDFLib.ConvertToInteger(txtDays.Text), UDFLib.ConvertToDate(txtResponseDate.Text), GetSessionUserID());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Perform Vetting saved successfully');", true);
                    ClearFields();
                    Session["VET_ImportDataset" + GUIDSession.Value.ToString()] = null;
                    string js = "parent.UpdatePageAfterSave(" + ViewState["Vetting_ID"].ToString() + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('Invalid file.');", true);
                }

            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}



