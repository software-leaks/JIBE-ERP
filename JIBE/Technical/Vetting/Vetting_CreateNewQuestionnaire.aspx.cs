using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using SMS.Business.VET;
using SMS.Properties;

public partial class Technical_Vetting_CreateNewVettingQuestionnaire : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            if (!IsPostBack)
            {
                VET_Get_Module();
                VET_Get_VettingTypeList();
                Get_VesselType();
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
    /// To display Vessel Type
    /// </summary>
    public void Get_VesselType()
    {
        try
        {
            BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
            DataTable dtVesselType = objBLL.Get_VesselType();
            DDLVesselType.DataSource = dtVesselType;
            DDLVesselType.DataTextField = "VesselTypes";
            DDLVesselType.DataValueField = "ID";
            DDLVesselType.DataBind();
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// To display Vetting Type
    /// </summary>
    public void VET_Get_VettingTypeList()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtVetType = objBLLVetLib.VET_Get_VettingTypeList();
            DDLVetType.DataSource = dtVetType;
            DDLVetType.DataTextField = "Vetting_Type_Name";
            DDLVetType.DataValueField = "Vetting_Type_ID";
            DDLVetType.DataBind();
            DDLVetType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// To display Module
    /// </summary>
    public void VET_Get_Module()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtModule = objBLLVetLib.VET_Get_Module();
            DDLModule.DataSource = dtModule;
            DDLModule.DataTextField = "Name";
            DDLModule.DataValueField = "Module_ID";
            DDLModule.DataBind();
            DDLModule.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    
    /// <summary>
    /// To insert Questionnaire 
    /// </summary>
    public void VET_Ins_Questionnaire()
    {
        try
        {
            string strQuestionnaireName = txtQuestionnaireName.Text.Trim();
            string strNumber = txtNumber.Text.Trim();
            string strVersion = txtVersion.Text.Trim();
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataSet ds = new DataSet();
            //DDLVesselType.SelectedValues added for multiple vessel type selection
            ds = objBLLQuest.VET_Get_ExistQuestinnaire(UDFLib.ConvertIntegerToNull(DDLModule.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVetType.SelectedValue),DDLVesselType.SelectedValues, strNumber, strVersion);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string jsSqlError2 = "alert('Questionnaire already exist.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
                }
                else
                {
                    DataTable dtQuestionnaire = new DataTable();
                   
                    dtQuestionnaire = objBLLQuest.VET_Ins_Questionnaire(UDFLib.ConvertIntegerToNull(DDLModule.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVetType.SelectedValue), DDLVesselType.SelectedValues, strQuestionnaireName, strNumber, strVersion, Convert.ToInt32(Session["USERID"]));
                    if (dtQuestionnaire.Rows.Count > 0)
                    {
                        hdnQuestionnaireId.Value = dtQuestionnaire.Rows[0]["Questionnaire_ID"].ToString();
                        hdnQuestnirStatus.Value = dtQuestionnaire.Rows[0]["Status"].ToString();
                    }
                    string jsSqlError2 = "alert('Questionnaire created.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDLVesselType.SelectedValues.Rows.Count == 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "alert('Please select atleast one Vessel Type.');", true);
                return;

            }
            else
            {
                VET_Ins_Questionnaire();
                ClearFields();

                string js2 = "parent.UpdateQuestionnairePage(" + hdnQuestionnaireId.Value + ",'" + hdnQuestnirStatus.Value + "')";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "js2", js2, true);
            }
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);        
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string js2 = "parent.HideQuestionnairePage(); ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);   
        }
    }
    /// <summary>
    /// Clear all fields
    /// </summary>
    public void ClearFields()
    {
        DDLModule.SelectedValue = "0";
        DDLVesselType.ClearSelection();
        DDLVetType.SelectedValue = "0";
        txtQuestionnaireName.Text = "";
        txtNumber.Text = "";
        txtVersion.Text = "";
    }
}