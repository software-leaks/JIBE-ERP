using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ClsBLLTechnical;
using   SMS.Business.PURC ;

public partial class Technical_INV_HSEQuestionnaire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowSupplierDetails();

        }
    }

    private void ShowSupplierDetails()
    {
        try
        {
            TechnicalBAL objbal = new TechnicalBAL();
            //DataTable dtSupplierData = objbal.GetSupplier_HSEQuestion(Request.QueryString["SupplierCode"].ToString()).Tables[0];
            DataTable dtSupplierData = objbal.GetSupplier_HSEQuestion(Session["SupplierCode"].ToString(),Convert.ToInt16(Request.QueryString["HSEId"].ToString())).Tables[0];
            
            txtCompanyName.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Name"]);
            txtOperations.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Operations"]);
            txtAddress.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Address"]);
            txtCountry.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Country"]);
            txtPhone.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Phone"]);
            txtFax.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Fax"]);
            txtEmail.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_Email"]);
            txtWebsiet.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_WebSite"]);
            txtPIC.Text = Convert.ToString(dtSupplierData.Rows[0]["Company_PIC"]);
            txtEmergencyContactNo.Text = Convert.ToString(dtSupplierData.Rows[0]["Emergency_Contact_No"]);
           
            txtSizeTurnover.Text = Convert.ToString(dtSupplierData.Rows[0]["Size_Turnover"]);

            txtAgeOfBusiness.Text = Convert.ToString(dtSupplierData.Rows[0]["Age_Of_Business"]);
            txtServicesProvided.Text = Convert.ToString(dtSupplierData.Rows[0]["Services_Provided"]);
            ddlRegulatoryControl.Text = Convert.ToString(dtSupplierData.Rows[0]["Regulatory_Control"]);
            txtRegulatoryControlRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Regulatory_Control_Remarks"]);

            txtCustomer1Name.Text = Convert.ToString(dtSupplierData.Rows[0]["Customer1_Name"]);
            txtCustomer1Years.Text = Convert.ToString(dtSupplierData.Rows[0]["Customer1_Years"]);
            txtServiceDescription1.Text = Convert.ToString(dtSupplierData.Rows[0]["Service_Description1"]);
            txtCustomer2Name.Text = Convert.ToString(dtSupplierData.Rows[0]["Customer2_Name"]);
            txtCustomer2Years.Text = Convert.ToString(dtSupplierData.Rows[0]["Customer2_Years"]);
            txtServiceDescription2.Text = Convert.ToString(dtSupplierData.Rows[0]["Service_Description2"]);
            txtCustomer3Name.Text = Convert.ToString(dtSupplierData.Rows[0]["Customer3_Name"]);
            txtCustomer3Years.Text = Convert.ToString(dtSupplierData.Rows[0]["Customer3_Years"]);
            txtServiceDescription3.Text = Convert.ToString(dtSupplierData.Rows[0]["Service_Description3"]);

            txtHoursWorkedYTD.Text = Convert.ToString(dtSupplierData.Rows[0]["Hours_Worked_YTD"]);
            txtHoursWorked2Yrs.Text = Convert.ToString(dtSupplierData.Rows[0]["Hours_Worked_2Yrs"]);

            txtFatalInjuriesYTD.Text = Convert.ToString(dtSupplierData.Rows[0]["Fatal_injuries_YTD"]);
            txtFatalInjuries2Yrs.Text = Convert.ToString(dtSupplierData.Rows[0]["Fatal_Injuries_2Yrs"]);

            txtLostDayInjuriesYTD.Text = Convert.ToString(dtSupplierData.Rows[0]["LostDay_Injuries_YTD"]);
            txtLostDayInjuries2Yrs.Text = Convert.ToString(dtSupplierData.Rows[0]["LostDay_Injuries_2Yrs"]);
            txtIncidenceRateYTD.Text = Convert.ToString(dtSupplierData.Rows[0]["Incidence_Rate_YTD"]);
            txtIncidenceRate2Yrs.Text = Convert.ToString(dtSupplierData.Rows[0]["Incidence_Rate_2Yrs"]);
            txtGovernmentInsp3Yrs.Text = Convert.ToString(dtSupplierData.Rows[0]["Government_Insp_3Yrs"]);
            txtSignificantIncidents3Yrs.Text = Convert.ToString(dtSupplierData.Rows[0]["Significant_Incidents_3Yrs"]);
            ddlInsuranceIndemity.Text = Convert.ToString(dtSupplierData.Rows[0]["Insurance_Indemity"]);
            txtInsuranceIndemityRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Insurance_Indemity_Remarks"]);
            ddlQualityAssurance.Text = Convert.ToString(dtSupplierData.Rows[0]["Quality_Assurance"]);
            txtQualityAssuranceRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Quality_Assurance_Remarks"]);
            ddlTrainingNewEmployees.Text = Convert.ToString(dtSupplierData.Rows[0]["Training_New_Employees"]);
            txtTrainingNewEmployeesRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Training_New_Employees_Remarks"]);
            ddlTrainingExisitingEmployees.Text = Convert.ToString(dtSupplierData.Rows[0]["Training_Exisiting_Employees"]);
            txtTrainingExisitingEmployeesRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Training_Exisiting_Employees_Remarks"]);
            txtClientList.Text = Convert.ToString(dtSupplierData.Rows[0]["Client_List"]);
            ddlIncidentReporting.Text = Convert.ToString(dtSupplierData.Rows[0]["Incident_Reporting"]);
            txtIncidentReportingRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Incident_Reporting_Remarks"]);
            ddlNearMissReporting.Text = Convert.ToString(dtSupplierData.Rows[0]["Near_Miss_Reporting"]);
            txtNearMissReportingRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Near_Miss_Reporting_Remarks"]);
            ddlSafetyEquipment.Text = Convert.ToString(dtSupplierData.Rows[0]["Safety_Equipment"]);
            txtSafetyEquipmentRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Safety_Equipment_Remarks"]);

            ddlEmployeeEquipTraining.Text = Convert.ToString(dtSupplierData.Rows[0]["Employee_Equip_Training"]);
            txtEmployeeEquipTrainingRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Employee_Equip_Training_Remarks"]);
            ddlContractorEquipTraining.Text = Convert.ToString(dtSupplierData.Rows[0]["Contractor_Equip_Training"]);
            txtContractorEquipTrainingRemark.Text = Convert.ToString(dtSupplierData.Rows[0]["Contractor_Equip_Training_Remarks"]);

            ddlCleanWorkingEnvironment.Text = Convert.ToString(dtSupplierData.Rows[0]["Clean_Working_Environment"]);
            txtCleanWorkingEnvironmentRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Clean_Working_Environment_Remarks"]);
            ddlEquipmentCalibration.Text = Convert.ToString(dtSupplierData.Rows[0]["Equipment_Calibration"]);
            txtEquipmentCalibrationRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Equipment_Calibration_Remarks"]);

            ddlCalibrationCertificates.Text = Convert.ToString(dtSupplierData.Rows[0]["Calibration_Certificates"]);
            txtCalibrationCertificatesRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Calibration_Certificates_Remarks"]);

            ddlClientFamiliarizationvisits.Text = Convert.ToString(dtSupplierData.Rows[0]["Client_Familiarization_visits"]);
            txtClientFamiliarizationVisitsRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Calibration_Certificates_Remarks"]);

            ddlMeetStandardRequirements.Text = Convert.ToString(dtSupplierData.Rows[0]["Meet_Standard_Requirements"]);
            txtMeetStandardRequirementsRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["Meet_Standard_Requirements_Remarks"]);
            txtHSEQSubmittedDate.Text = Convert.ToDateTime(dtSupplierData.Rows[0]["HSEQ_Submitted_Date"]).ToString("dd-MM-yyyy");
            txtHSEQSubmittedBy.Text = Convert.ToString(dtSupplierData.Rows[0]["HSEQ_Submitted_By"]);
            ddlESMCompetitiveQuote.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Competitive_Quote"]);
            txtESMCompetitiveQuoteRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Competitive_Quote_Remarks"]);
            ddlESMQuickResponsee.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Quick_Response"]);
            txtESMQuickResponseRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Quick_Response_Remarks"]);
            ddlESMOntimeDelivery.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Ontime_Delivery"]);
            txtESMOntimeDeliveryRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Ontime_Delivery_Remarks"]);
            ddlESMPromptAdvice.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Prompt_Advice"]);
            txtESMPromptAdviceRemarks.Text = Convert.ToString(dtSupplierData.Rows[0]["ESM_Prompt_Advice_Remarks"]);
            lblDateOfCreation.Text = Convert.ToDateTime(dtSupplierData.Rows[0]["Date_Of_Creatation"]).ToString("dd-MMM-yyyy");
            
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string st = txtCompanyName.Text;

            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime HSEQSubmittedDate = DateTime.Parse(txtHSEQSubmittedDate.Text.Trim(), provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            TechnicalBAL objbal = new TechnicalBAL();
            int x = objbal.ins_ASL_DTL_Supplier_HSEQ(
            Session["SupplierCode"].ToString(), txtCompanyName.Text.Trim(), txtOperations.Text.Trim(), txtAddress.Text.Trim(), txtCountry.Text.Trim(),
            txtPhone.Text.Trim(), txtFax.Text.Trim(), txtEmail.Text.Trim(), txtWebsiet.Text.Trim(), txtPIC.Text.Trim(), txtEmergencyContactNo.Text.Trim(),
            txtSizeTurnover.Text.Trim(), txtAgeOfBusiness.Text.Trim(), txtServicesProvided.Text.Trim(), "", ddlRegulatoryControl.Text.Trim(),
            txtRegulatoryControlRemarks.Text.Trim(), txtCustomer1Name.Text.Trim(), txtCustomer2Name.Text.Trim(), txtCustomer3Name.Text.Trim(), txtCustomer1Years.Text.Trim(),
            txtCustomer2Years.Text.Trim(), txtCustomer3Years.Text.Trim(), txtServiceDescription1.Text.Trim(), txtServiceDescription2.Text.Trim(), txtServiceDescription3.Text.Trim(), Convert.ToInt32(txtHoursWorkedYTD.Text.Trim()),
            Convert.ToInt32(txtHoursWorked2Yrs.Text.Trim()), Convert.ToInt32(txtFatalInjuriesYTD.Text.Trim()), Convert.ToInt32(txtFatalInjuries2Yrs.Text.Trim()), Convert.ToInt32(txtLostDayInjuriesYTD.Text.Trim()),
            Convert.ToInt32(txtLostDayInjuries2Yrs.Text.Trim()), Convert.ToInt32(txtIncidenceRateYTD.Text.Trim()), Convert.ToInt32(txtIncidenceRate2Yrs.Text.Trim()), txtGovernmentInsp3Yrs.Text.Trim(), txtSignificantIncidents3Yrs.Text.Trim(),
            ddlInsuranceIndemity.Text.Trim(), txtInsuranceIndemityRemarks.Text.Trim(), ddlQualityAssurance.Text.Trim(), txtQualityAssuranceRemarks.Text.Trim(), "",
            ddlTrainingNewEmployees.Text.Trim(), txtTrainingNewEmployeesRemarks.Text.Trim(), ddlTrainingExisitingEmployees.Text.Trim(), txtTrainingExisitingEmployeesRemarks.Text.Trim(),
            txtClientList.Text.Trim(), ddlIncidentReporting.Text.Trim(), txtIncidentReportingRemarks.Text.Trim(), ddlNearMissReporting.Text.Trim(), txtNearMissReportingRemarks.Text.Trim(),
            ddlSafetyEquipment.Text.Trim(), txtSafetyEquipmentRemarks.Text.Trim(), "", ddlEmployeeEquipTraining.Text.Trim(),
            txtEmployeeEquipTrainingRemarks.Text.Trim(), txtContractorEquipTrainingRemark.Text.Trim(), ddlContractorEquipTraining.Text.Trim(),
            ddlCleanWorkingEnvironment.Text.Trim(), txtCleanWorkingEnvironmentRemarks.Text.Trim(), ddlEquipmentCalibration.Text.Trim(), ddlCalibrationCertificates.Text.Trim(),
            txtEquipmentCalibrationRemarks.Text.Trim(), txtCalibrationCertificatesRemarks.Text.Trim(), txtClientFamiliarizationVisitsRemarks.Text.Trim(), ddlClientFamiliarizationvisits.Text.Trim(),
            ddlMeetStandardRequirements.Text.Trim(), txtMeetStandardRequirementsRemarks.Text.Trim(), HSEQSubmittedDate, txtHSEQSubmittedBy.Text.Trim(),
            ddlESMCompetitiveQuote.Text.Trim(), txtESMCompetitiveQuoteRemarks.Text.Trim(), ddlESMQuickResponsee.Text.Trim(), txtESMQuickResponseRemarks.Text.Trim(),
            ddlESMOntimeDelivery.Text.Trim(), txtESMOntimeDeliveryRemarks.Text.Trim(), ddlESMPromptAdvice.Text.Trim(), txtESMPromptAdviceRemarks.Text.Trim(),
            10);
            
            if (x > 0)
            {
                String msg = String.Format("alert('HSE Questionnaire save successfully.');");
                ScriptManager.RegisterStartupScript( Page, Page.GetType(), "msg", msg, true);
            }

        }
        catch //(Exception ex)
        {

        }
    }
    protected void txtServicesProvided_TextChanged(object sender, EventArgs e)
    {
        
    }
}
