using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.VET;
using System.Data;
namespace SMS.Business.VET
{
    public  class BLL_VET_Planner
    {

        DAL_VET_Planner objDLPlan = new DAL_VET_Planner();

        /// <summary>
        /// Get Vetting 
        /// </summary>
        /// <param name="fleetcode"> Fleet Code</param>
        /// <param name="dtVessel">Vessel List</param>
        /// <param name="dtOilMajor"> Oil major List</param>
        /// <param name="IsPlanned">1:- Planned, 0: Unplanned</param>
        /// <param name="dtVetType">Vetting Type List</param>
        /// <param name="ExInDays">Expire in days</param>
        /// <param name="dtExtIspector">External Inspector List</param>
        /// <param name="LastVetFrom">Last Vetting From Date</param>
        /// <param name="LastVetTo">Last Vetting To Date</param>
        /// <param name="ExpDateFrom">Expiry Vetting From Date</param>
        /// <param name="ExpDateTo">Expiry Vetting To Date</param>
        /// <param name="PlanDateFrom">Planned Vetting From Date</param>
        /// <param name="PlanDateTo">Planned Vetting To Date</param>
        /// <param name="sortby"> Sort By Column</param>
        /// <param name="sortdirection"> Direction for Sorting Data</param>
        /// <returns>Data Set </returns>
        public DataSet VET_Get_Vetting(int? fleetcode, DataTable dtVessel, DataTable dtOilMajor, int IsPlanned, DataTable dtVetType, int? ExInDays, DataTable dtExtIspector, DateTime? LastVetFrom, DateTime? LastVetTo, DateTime? ExpDateFrom, DateTime? ExpDateTo, DateTime? PlanDateFrom, DateTime? PlanDateTo, string sortby, int? sortdirection)
        {
            return objDLPlan.VET_Get_Vetting(fleetcode, dtVessel, dtOilMajor, IsPlanned, dtVetType, ExInDays,  dtExtIspector, LastVetFrom, LastVetTo, ExpDateFrom, ExpDateTo, PlanDateFrom, PlanDateTo, sortby, sortdirection);

        }

        /// <summary>
        /// Get Vetting Type List
        /// </summary>
        /// <returns>Table</returns>
        public DataTable VET_Get_VettingType()
        {
            return objDLPlan.VET_Get_VettingType();
        
        }

        /// <summary>
        /// Check is VettingType is assign to Vessel
        /// </summary>
        /// <param name="VesselID">Id of Vessel</param>
        /// <param name="VettingTypeID">ID of Vetting Type ID</param>
        /// <returns></returns>
        public int VET_Get_VettingSetting(int VesselID, int VettingTypeID)
        {
            return objDLPlan.VET_Get_VettingSetting(VesselID, VettingTypeID);
        }

        /// <summary>
        /// Get Data for creating Calender view
        /// </summary>
        /// <param name="fleetcode"> Fleet Code</param>
        /// <param name="dtVessel">Vessel List</param>
        /// <param name="dtOilMajor"> Oil major List</param>
        /// <param name="IsPlanned">1:- Planned, 0: Unplanned</param>
        /// <param name="dtVetType">Vetting Type List</param>
        /// <param name="ExInDays">Expire in days</param>
        /// <param name="dtExtIspector">External Inspector List</param>
        /// <param name="LastVetFrom">Last Vetting From Date</param>
        /// <param name="LastVetTo">Last Vetting To Date</param>
        /// <param name="ExpDateFrom">Expiry Vetting From Date</param>
        /// <param name="ExpDateTo">Expiry Vetting To Date</param>
        /// <param name="PlanDateFrom">Planned Vetting From Date</param>
        /// <param name="PlanDateTo">Planned Vetting To Date</param>
        /// <param name="sortby"> Sort By Column</param>
        /// <param name="sortdirection"> Direction for Sorting Data</param>
        /// <returns></returns>
        public DataSet VET_Get_CalendarViewVetting(int? fleetcode, DataTable dtVessel, DataTable dtOilMajor, int IsPlanned, DataTable dtVetType, int? ExInDays, DataTable dtIntInspector, DataTable dtExtInspector, DateTime? LastVetFrom, DateTime? LastVetTo, DateTime? ExpDateFrom, DateTime? ExpDateTo, DateTime? PlanDateFrom, DateTime? PlanDateTo, string sortby, int? sortdirection)
        {
            return objDLPlan.VET_Get_CalendarViewVetting(fleetcode, dtVessel, dtOilMajor, IsPlanned, dtVetType, ExInDays, dtIntInspector, dtExtInspector, LastVetFrom, LastVetTo, ExpDateFrom, ExpDateTo, PlanDateFrom, PlanDateTo, sortby, sortdirection);

        }

        /// <summary>
        /// Get Vetting Details
        /// </summary>
        /// <param name="VettingID">ID of vetting </param>
        /// <param name="dtSection">List Of Sections</param>
        /// <param name="dtQuestion">List of Questions</param>
        /// <param name="dtVetType">List Of Vetting Type</param>
        /// <param name="dtObsStatus">List Of Observation Status</param>
        /// <param name="dtJobStatus">List Of Job Status</param>
        /// <param name="dtCategory">List Category</param>
        /// <param name="dtRiskLevel">List Risk Level</param>
        /// <param name="sortby"> Sort By Column</param>
        /// <param name="sortdirection"> Direction for Sorting Data</param>
        /// <returns>Taqble List</returns>
        public DataSet VET_Get_VettingDetails(int VettingID, DataTable dtSection, DataTable dtQuestion, DataTable dtVetType, DataTable dtObsStatus, DataTable dtJobStatus, DataTable dtCategory, DataTable dtRiskLevel, string sortby, int? sortdirection)
        {
            return objDLPlan.VET_Get_VettingDetails(VettingID, dtSection, dtQuestion, dtVetType, dtObsStatus, dtJobStatus, dtCategory, dtRiskLevel, sortby, sortdirection);

        }
        /// <summary>
        /// Bind all filters related to vetting details based on Vetting ID
        /// </summary>
        /// <param name="VettingID">Vetting ID</param>
        /// <returns></returns>
        public DataSet VET_GET_FiltersForVetting(int VettingID)
        {
            return objDLPlan.VET_GET_FiltersForVetting(VettingID);
        }
        /// <summary>
        /// Get Pending Jobs List for tool tip
        /// </summary>
        /// <param name="Vetting_ID">Vetting ID</param>
        /// <param name="Question_ID">Question ID</param>
        /// <param name="Observation_ID">Observation ID</param>
        /// <returns></returns>
        public DataTable VET_Get_PendingJobsTooltip(int? Vetting_ID,int? Question_ID, int? Observation_ID)
        {
            try
            {
                return objDLPlan.VET_Get_PendingJobsTooltip(Vetting_ID, Question_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Question List for tool tip
        /// </summary>
        /// <param name="Questionnaire_ID">ID of Questionnaire</param>
        /// <param name="SectionNo">No of Section</param>
        /// <param name="Question_ID">ID of Section </param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionForToolTip(int? Questionnaire_ID, int? SectionNo, int? Question_ID)
        {
            try
            {
                return objDLPlan.VET_Get_QuestionForToolTip(Questionnaire_ID, SectionNo, Question_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Remark added for Vetting
        /// </summary>
        /// <param name="Vetting_ID">Id of Vetting</param>
        /// <returns></returns>
        public DataTable VET_Get_VettingRemarks(int? Vetting_ID)
        {
            try
            {
                return objDLPlan.VET_Get_VettingRemarks(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert remark for vetting
        /// </summary>
        /// <param name="Vetting_ID">ID for Vetting</param>
        /// <param name="Remark">Remark for vetting</param>
        /// <param name="CreatedBy">ID of user who added remark</param>
        /// <returns></returns>
        public int VET_Ins_VettingRemark(int? Vetting_ID, string Remark, int CreatedBy)
        {
            try
            {
                return objDLPlan.VET_Ins_VettingRemark(Vetting_ID, Remark, CreatedBy);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete observation added to Vetting
        /// </summary>
        /// <param name="Observation_ID">ID of Observation </param>
        /// <param name="Question_ID">ID of Question</param>
        /// <param name="Vetting_ID">ID of vetting</param>
        /// <param name="UserID">ID of User whoc delete observation</param>
        /// <returns></returns>
        public int VET_Del_Observation(int Observation_ID, int Question_ID, int Vetting_ID, int UserID)
        {
            try
            {
                return objDLPlan.VET_Del_Observation(Observation_ID, Question_ID, Vetting_ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update Observation Status
        /// </summary>
        /// <param name="Observation_ID">ID of observation</param>
        /// <param name="Status">Status of Observation</param>
        /// <param name="UserId">ID of Observation whoi update the status</param>
        /// <returns></returns>
        public int VET_Upd_ObservationStatus(int Observation_ID, string Status, int UserId)
        {
            try
            {
                return objDLPlan.VET_Upd_ObservationStatus(Observation_ID, Status, UserId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Updaete Vetting Status
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="SelectedDate">Date related to vetting according to vetting status</param>
        /// <param name="Status">Status of vetting</param>
        /// <param name="UserID">ID of user who change the status</param>
        /// <returns></returns>
        public int VET_Upd_VettingStatus(int Vetting_ID, DateTime? SelectedDate, string Status, int UserID)
        {
            try
            {
                return objDLPlan.VET_Upd_VettingStatus(Vetting_ID, SelectedDate, Status, UserID);
            }
            catch
            {
                throw;
            }
        }
    }
}
