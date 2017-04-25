using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;



/// <summary>
/// Summary description for BL_Tec_WL_Worklist
/// </summary>
/// 
namespace SMS.Data.Technical
{
    public class DAL_Tec_Worklist
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_Tec_Worklist(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Tec_Worklist()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataSet TEC_WL_Get_PSCSIRE()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_PSCSIRE");
        }
        public DataSet TEC_WL_Get_ActivePSCSIRE()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_ActivePSCSIRE");
        }
        public DataSet TEC_WL_Get_PSCSIREByID(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@ID", ID),
               
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_PSCSIREByID", sqlprm);
        }
        public int TEC_WL_Insert_PSCSIRE(string PSCSIRE, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@PSC_SIRE", PSCSIRE),
                   new SqlParameter("@CreatedBy", CreatedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_Insert_PSCSIRE", sqlprm);
        }

        public int TEC_WL_Update_PSCSIRE(int ID, string PSCSIRECODE, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@ID", ID),
                  new SqlParameter("@PSC_SIRE", PSCSIRECODE),
                  new SqlParameter("@ModifiedBy", ModifiedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_Update_PSCSIRE", sqlprm);
        }

        public int TEC_WL_Delete_PSCSIRE(int ID, int DeletedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@ID", ID),
                
                   new SqlParameter("@DeletedBy", DeletedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_Delete_PSCSIRE", sqlprm);
        }
        public int TEC_WL_Restore_PSCSIRE(int ID, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@ID", ID),
                
                   new SqlParameter("@ModifiedBy", ModifiedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_Restore_PSCSIRE", sqlprm);
        }

        public DataSet TEC_Get_Category()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_Get_Category");
        }
        public DataSet TEC_Get_ActiveCategory()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_Get_ActiveCategory");
        }
        public DataSet TEC_Get_CategoryByID(int CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@CategoryID", CategoryID),
               
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_Get_CategoryByID", sqlprm);
        }
        public int TEC_Insert_Category(string CategoryName, string CategoryType, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@CategoryName", CategoryName),
                   new SqlParameter("@CategoryType", CategoryType),
                   new SqlParameter("@Created_By", CreatedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_Insert_Category", sqlprm);
        }

        public int TEC_Update_Category(int CategoryID, string CategoryName, string CategoryType, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@CategoryID", CategoryID),
                   new SqlParameter("@CategoryName", CategoryName),
                   new SqlParameter("@CategoryType", CategoryType),
                   new SqlParameter("@ModifiedBy", ModifiedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_Update_Category", sqlprm);
        }

        public int TEC_Delete_Category(int CategoryID, int DeletedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@CategoryID", CategoryID),
                
                   new SqlParameter("@DeletedBy", DeletedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_Delete_Category", sqlprm);
        }
        public int TEC_Restore_Category(int CategoryID, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@CategoryID", CategoryID),
                
                   new SqlParameter("@ModifiedBy", ModifiedBy)
              };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_Restore_Category", sqlprm);
        }
        public DataSet GetAllNature_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_AllNature");
        }
        public DataSet GetPrimaryByNatureID_DL(Int32 i32NatureID)
        {
            SqlParameter sqlprm = new SqlParameter("@NatureCode", i32NatureID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_PrimaryByNatureID", sqlprm);
        }
        public DataSet GetSecondaryByPrimaryID_DL(int intNature)
        {
            SqlParameter sqlprm = new SqlParameter("@intNature", intNature);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_SecondaryByPrimaryID", sqlprm);
        }
        public DataSet GetMinorBySecondaryID_DL(int SecondaryID)
        {
            SqlParameter sqlprm = new SqlParameter("@SecondaryID", SecondaryID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_MinorBySecondaryID", sqlprm);
        }

        public DataTable Get_Dept_OnShip()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Dept_OnShip").Tables[0];
        }
        public DataTable Get_Dept_InOffice()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Dept_InOffice").Tables[0];
        }
        public DataTable Get_JobPriority()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_JobPriority").Tables[0];
        }
        public DataTable Get_Assigner()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Assigner").Tables[0];
        }

        public DataTable Insert_NewJob_DL(int VESSEL_ID, string JOB_DESCRIPTION,
            string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
            int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
            int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, string Activity_ID, string status, int? LocationID, int? PSC_SIRE)
        {

            DateTime DT_DATE_RAISED = DateTime.Parse(DATE_RAISED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_DATE_ESTMTD_CMPLTN = DateTime.Parse(DATE_ESTMTD_CMPLTN, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_DATE_COMPLETED = DateTime.Parse(DATE_COMPLETED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_INSPECTIONDATE = DateTime.Parse(InspectionDate, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);


            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@JOB_DESCRIPTION",JOB_DESCRIPTION),
                new SqlParameter("@DATE_RAISED", DT_DATE_RAISED),
                new SqlParameter("@DATE_ESTMTD_CMPLTN",DT_DATE_ESTMTD_CMPLTN),
                new SqlParameter("@DATE_COMPLETED", DT_DATE_COMPLETED),
                new SqlParameter("@PRIORITY",PRIORITY),
                new SqlParameter("@ASSIGNOR",ASSIGNOR),
                new SqlParameter("@NCR_YN",NCR_YN),
                new SqlParameter("@DEPT_SHIP", DEPT_SHIP),
                new SqlParameter("@DEPT_OFFICE",DEPT_OFFICE),
                new SqlParameter("@REQSN_MSG_REF",REQSN_MSG_REF),
                new SqlParameter("@DEFER_TO_DD",DEFER_TO_DD),
                new SqlParameter("@CATEGORY_NATURE",CATEGORY_NATURE),
                new SqlParameter("@CATEGORY_PRIMARY",CATEGORY_PRIMARY),
                new SqlParameter("@CATEGORY_SECONDARY",CATEGORY_SECONDARY),
                new SqlParameter("@CATEGORY_MINOR",CATEGORY_MINOR),
                new SqlParameter("@PIC",PIC),
                new SqlParameter("@CREATED_BY",CREATED_BY),
                new SqlParameter("@TOSYNC",TOSYNC),
                new SqlParameter("@Causes",Causes),
                new SqlParameter("@CorrectiveAction",CorrectiveAction),
                new SqlParameter("@PreventiveAction",PreventiveAction),
                new SqlParameter("@Inspector",Inspector),
                new SqlParameter("@INSPECTIONDATE",DT_INSPECTIONDATE),
                new SqlParameter("@WL_TYPE",WL_TYPE),
                new SqlParameter("@Activity_ID",Activity_ID),
                new SqlParameter("@LocationID",LocationID),
                new SqlParameter("@status",status),
                  new SqlParameter("@PSC_SIRE",PSC_SIRE),
                new SqlParameter("@RetWorklistId",SqlDbType.Int),                
                new SqlParameter("@RetOfficeId",SqlDbType.Int)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_INS_WORKLIST", sqlprm).Tables[0];


        }

        public DataSet Get_FilterWorklist(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL);
        }

        public DataTable Get_WorkList_Index(DataTable dtFilter, ref int Record_Count)
        {
            SqlParameter[] sqlprm = new SqlParameter[dtFilter.Rows.Count + 1];

            int i = 0;
            foreach (DataRow dr in dtFilter.Rows)
            {
                sqlprm[i] = new SqlParameter(dr["PRM_NAME"].ToString(), dr["PRM_VALUE"]);
                i++;
            }
            sqlprm[i] = new SqlParameter("@return", SqlDbType.Int);
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            DataTable ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_GET_WORKLIST", sqlprm).Tables[0];
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
        }

        public DataSet Get_InspectorName_ByInspectionID_DL(int InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@InspectionID", InspectionID)
                  
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_InspectorList_BY_InspectionID", sqlprm);
        }

        public DataSet INSP_Get_Worklist(DataTable dtFilter, ref int Record_Count)
        {
            SqlParameter[] sqlprm = new SqlParameter[dtFilter.Rows.Count + 1];

            int i = 0;
            foreach (DataRow dr in dtFilter.Rows)
            {
                sqlprm[i] = new SqlParameter(dr["PRM_NAME"].ToString(), dr["PRM_VALUE"]);
                i++;
            }
            sqlprm[i] = new SqlParameter("@return", SqlDbType.Int);
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Worklist", sqlprm);
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
        }
        public DataSet Get_JobDetails_ByID(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@OFFICE_ID", OFFICE_ID),
                   new SqlParameter("@WORKLIST_ID", WORKLIST_ID),
                   new SqlParameter("@VESSEL_ID", VESSEL_ID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_JobDetails_ByID", sqlprm);
        }

        public int Create_Mail_Job_Details(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@OFFICE_ID", OFFICE_ID),
                   new SqlParameter("@WORKLIST_ID", WORKLIST_ID),
                   new SqlParameter("@VESSEL_ID", VESSEL_ID)
              };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_TEC_WL_Create_Mail_JobDetails", sqlprm));
        }

        public DataTable Get_WorkList_Calendar(string JobType, DateTime? StartDate, DateTime? EndDate, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@JobType", JobType),
                   new SqlParameter("@StartDate", StartDate),
                   new SqlParameter("@EndDate", EndDate),
                   new SqlParameter("@UserID", UserID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_WorkList_Calendar", sqlprm).Tables[0];
        }


        public int IsVessel(int Vessel_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@VESSEL_ID", Vessel_ID)
              };

                string strRet = Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.Text, "SELECT ISVESSEL FROM LIB_VESSELS WHERE VESSEL_ID=@VESSEL_ID", sqlprm));
                return Convert.ToInt32(strRet);
            }
            catch
            {
                return -1;
            }
        }

        public DataSet GetAllWorklistType_DL()
        {
            //SqlParameter[] sqlprm = new SqlParameter[1];

            //sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ALL_WorklistType");
        }


        // //Added by anjali Dt:18-02-2016
        public DataTable Insert_NewJob_DL(int VESSEL_ID, string JOB_DESCRIPTION, string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY, int ASSIGNOR, int NCR_YN,
                                          int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD, int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR,
                                          int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate,
                                            string WL_TYPE, int? PSC_SIRE, int _functionID, string _locationID, string _subLocation)
        {

            DateTime DT_DATE_RAISED = DateTime.Parse(DATE_RAISED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_DATE_ESTMTD_CMPLTN = DateTime.Parse(DATE_ESTMTD_CMPLTN, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_DATE_COMPLETED = DateTime.Parse(DATE_COMPLETED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_INSPECTIONDATE = DateTime.Parse(InspectionDate, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);


            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@JOB_DESCRIPTION",JOB_DESCRIPTION),
                new SqlParameter("@DATE_RAISED", DT_DATE_RAISED),
                new SqlParameter("@DATE_ESTMTD_CMPLTN",DT_DATE_ESTMTD_CMPLTN),
                new SqlParameter("@DATE_COMPLETED", DT_DATE_COMPLETED),
                new SqlParameter("@PRIORITY",PRIORITY),
                new SqlParameter("@ASSIGNOR",ASSIGNOR),
                new SqlParameter("@NCR_YN",NCR_YN),
                new SqlParameter("@DEPT_SHIP", DEPT_SHIP),
                new SqlParameter("@DEPT_OFFICE",DEPT_OFFICE),
                new SqlParameter("@REQSN_MSG_REF",REQSN_MSG_REF),
                new SqlParameter("@DEFER_TO_DD",DEFER_TO_DD),
                new SqlParameter("@CATEGORY_NATURE",CATEGORY_NATURE),
                new SqlParameter("@CATEGORY_PRIMARY",CATEGORY_PRIMARY),
                new SqlParameter("@CATEGORY_SECONDARY",CATEGORY_SECONDARY),
                new SqlParameter("@CATEGORY_MINOR",CATEGORY_MINOR),
                new SqlParameter("@PIC",PIC),
                new SqlParameter("@CREATED_BY",CREATED_BY),
                new SqlParameter("@TOSYNC",TOSYNC),
                new SqlParameter("@Causes",Causes),
                new SqlParameter("@CorrectiveAction",CorrectiveAction),
                new SqlParameter("@PreventiveAction",PreventiveAction),
                new SqlParameter("@Inspector",Inspector),
                new SqlParameter("@INSPECTIONDATE",DT_INSPECTIONDATE),
                new SqlParameter("@WL_TYPE",WL_TYPE),
                new SqlParameter("@PSC_SIRE",PSC_SIRE),
                new SqlParameter("@RetWorklistId",SqlDbType.Int),
                new SqlParameter("@RetOfficeId",SqlDbType.Int),

                new SqlParameter("@Function_ID",_functionID), 
                new SqlParameter("@SYSTEM_ID",_locationID), 
                new SqlParameter("@SUB_SYSTEM_ID",_subLocation)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_INS_WORKLIST", sqlprm).Tables[0];


        }

        public int Update_Job_DL(int VESSEL_ID, int WorkList_ID, int OFFICE_ID, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY, int ASSIGNOR, int NCR_YN, int DEPT_SHIP,
                                int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD, int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, string PIC,
                                int MODIFIED_BY, int TOSYNC, int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE, int _functionID, string _locationID, string _subLocation)
        {
            try
            {

                DateTime DT_DATE_ESTMTD_CMPLTN = DateTime.Parse(DATE_ESTMTD_CMPLTN, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
                DateTime DT_DATE_COMPLETED = DateTime.Parse(DATE_COMPLETED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
                DateTime DT_INSPECTIONDATE = DateTime.Parse(InspectionDate, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);

                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@WORKLIST_ID",WorkList_ID), 
                new SqlParameter("@OFFICE_ID",OFFICE_ID), 
                new SqlParameter("@DATE_ESTMTD_CMPLTN",DT_DATE_ESTMTD_CMPLTN),
                new SqlParameter("@DATE_COMPLETED", DT_DATE_COMPLETED),
                new SqlParameter("@PRIORITY",PRIORITY),
                new SqlParameter("@ASSIGNOR",ASSIGNOR),
                new SqlParameter("@NCR_YN",NCR_YN),
                new SqlParameter("@DEPT_SHIP", DEPT_SHIP),
                new SqlParameter("@DEPT_OFFICE",DEPT_OFFICE),
                new SqlParameter("@REQSN_MSG_REF",REQSN_MSG_REF),
                new SqlParameter("@DEFER_TO_DD",DEFER_TO_DD),
                new SqlParameter("@CATEGORY_NATURE",CATEGORY_NATURE),
                new SqlParameter("@CATEGORY_PRIMARY",CATEGORY_PRIMARY),
                new SqlParameter("@CATEGORY_SECONDARY",CATEGORY_SECONDARY),
                new SqlParameter("@CATEGORY_MINOR",CATEGORY_MINOR),
                new SqlParameter("@PIC",PIC),
                new SqlParameter("@MODIFIED_BY",MODIFIED_BY),
                new SqlParameter("@TOSYNC",TOSYNC),
                new SqlParameter("@Inspector",Inspector),
                new SqlParameter("@INSPECTIONDATE",DT_INSPECTIONDATE),
                new SqlParameter("@WL_TYPE",WL_TYPE),
                 new SqlParameter("@PSC_SIRE",PSC_SIRE),

                 new SqlParameter("@Function_ID",_functionID), 
                new SqlParameter("@SYSTEM_ID",_locationID), 
                new SqlParameter("@SUB_SYSTEM_ID",_subLocation)
            };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_WORKLIST", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }

        }

        // //Added by anjali Dt:18-02-2016
        public DataTable Insert_NewJob_DL(int VESSEL_ID, string JOB_DESCRIPTION,
           string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
           int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
           int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE)
        {

            DateTime DT_DATE_RAISED = DateTime.Parse(DATE_RAISED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_DATE_ESTMTD_CMPLTN = DateTime.Parse(DATE_ESTMTD_CMPLTN, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_DATE_COMPLETED = DateTime.Parse(DATE_COMPLETED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
            DateTime DT_INSPECTIONDATE = DateTime.Parse(InspectionDate, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);


            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@JOB_DESCRIPTION",JOB_DESCRIPTION),
                new SqlParameter("@DATE_RAISED", DT_DATE_RAISED),
                new SqlParameter("@DATE_ESTMTD_CMPLTN",DT_DATE_ESTMTD_CMPLTN),
                new SqlParameter("@DATE_COMPLETED", DT_DATE_COMPLETED),
                new SqlParameter("@PRIORITY",PRIORITY),
                new SqlParameter("@ASSIGNOR",ASSIGNOR),
                new SqlParameter("@NCR_YN",NCR_YN),
                new SqlParameter("@DEPT_SHIP", DEPT_SHIP),
                new SqlParameter("@DEPT_OFFICE",DEPT_OFFICE),
                new SqlParameter("@REQSN_MSG_REF",REQSN_MSG_REF),
                new SqlParameter("@DEFER_TO_DD",DEFER_TO_DD),
                new SqlParameter("@CATEGORY_NATURE",CATEGORY_NATURE),
                new SqlParameter("@CATEGORY_PRIMARY",CATEGORY_PRIMARY),
                new SqlParameter("@CATEGORY_SECONDARY",CATEGORY_SECONDARY),
                new SqlParameter("@CATEGORY_MINOR",CATEGORY_MINOR),
                new SqlParameter("@PIC",PIC),
                new SqlParameter("@CREATED_BY",CREATED_BY),
                new SqlParameter("@TOSYNC",TOSYNC),
                new SqlParameter("@Causes",Causes),
                new SqlParameter("@CorrectiveAction",CorrectiveAction),
                new SqlParameter("@PreventiveAction",PreventiveAction),
                new SqlParameter("@Inspector",Inspector),
                new SqlParameter("@INSPECTIONDATE",DT_INSPECTIONDATE),
                new SqlParameter("@WL_TYPE",WL_TYPE),
                 new SqlParameter("@PSC_SIRE",PSC_SIRE),
                new SqlParameter("@RetWorklistId",SqlDbType.Int),
                new SqlParameter("@RetOfficeId",SqlDbType.Int)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_INS_WORKLIST", sqlprm).Tables[0];


        }

        public int Update_Job_DL(int VESSEL_ID, int WorkList_ID, int OFFICE_ID, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
                int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
                int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, string PIC, int MODIFIED_BY, int TOSYNC, int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE)
        {
            try
            {

                DateTime DT_DATE_ESTMTD_CMPLTN = DateTime.Parse(DATE_ESTMTD_CMPLTN, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
                DateTime DT_DATE_COMPLETED = DateTime.Parse(DATE_COMPLETED, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);
                DateTime DT_INSPECTIONDATE = DateTime.Parse(InspectionDate, iFormatProvider, DateTimeStyles.NoCurrentDateDefault);

                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@WORKLIST_ID",WorkList_ID), 
                new SqlParameter("@OFFICE_ID",OFFICE_ID), 
                new SqlParameter("@DATE_ESTMTD_CMPLTN",DT_DATE_ESTMTD_CMPLTN),
                new SqlParameter("@DATE_COMPLETED", DT_DATE_COMPLETED),
                new SqlParameter("@PRIORITY",PRIORITY),
                new SqlParameter("@ASSIGNOR",ASSIGNOR),
                new SqlParameter("@NCR_YN",NCR_YN),
                new SqlParameter("@DEPT_SHIP", DEPT_SHIP),
                new SqlParameter("@DEPT_OFFICE",DEPT_OFFICE),
                new SqlParameter("@REQSN_MSG_REF",REQSN_MSG_REF),
                new SqlParameter("@DEFER_TO_DD",DEFER_TO_DD),
                new SqlParameter("@CATEGORY_NATURE",CATEGORY_NATURE),
                new SqlParameter("@CATEGORY_PRIMARY",CATEGORY_PRIMARY),
                new SqlParameter("@CATEGORY_SECONDARY",CATEGORY_SECONDARY),
                new SqlParameter("@CATEGORY_MINOR",CATEGORY_MINOR),
                new SqlParameter("@PIC",PIC),
                new SqlParameter("@MODIFIED_BY",MODIFIED_BY),
                new SqlParameter("@TOSYNC",TOSYNC),
                new SqlParameter("@Inspector",Inspector),
                new SqlParameter("@INSPECTIONDATE",DT_INSPECTIONDATE),
                new SqlParameter("@WL_TYPE",WL_TYPE),
                  new SqlParameter("@PSC_SIRE",PSC_SIRE)
            };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_WORKLIST", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }

        }

        public int INSP_Delete_WorklistJob(int VESSEL_ID, int WorkList_ID, int OFFICE_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VesselID",VESSEL_ID), 
                new SqlParameter("@WorklistID",WorkList_ID), 
                new SqlParameter("@OfficeID",OFFICE_ID)
              };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Delete_WorklistJob", sqlprm);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int Update_Job_Causes_DL(string CAUSES, int VESSEL_ID, int WorkList_ID, int OFFICE_ID, int MODIFIED_BY, int TOSYNC)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@WORKLIST_ID",WorkList_ID), 
                new SqlParameter("@OFFICE_ID",OFFICE_ID), 
                new SqlParameter("@CAUSES",CAUSES),
                new SqlParameter("@MODIFIED_BY",MODIFIED_BY),
                new SqlParameter("@TOSYNC",TOSYNC)               
            };
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_WORKLIST_CAUSES", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public int Update_Job_CorrectiveAction_DL(string CorrectiveAction, int VESSEL_ID, int WorkList_ID, int OFFICE_ID, int MODIFIED_BY, int TOSYNC)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@WORKLIST_ID",WorkList_ID), 
                new SqlParameter("@OFFICE_ID",OFFICE_ID), 
                new SqlParameter("@CorrectiveAction",CorrectiveAction),
                new SqlParameter("@MODIFIED_BY",MODIFIED_BY),
                new SqlParameter("@TOSYNC",TOSYNC)               
            };
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_WORKLIST_CORRECTIVEACTION", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public int Update_Job_PreventiveAction_DL(string PreventiveAction, int VESSEL_ID, int WorkList_ID, int OFFICE_ID, int MODIFIED_BY, int TOSYNC)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID), 
                new SqlParameter("@WORKLIST_ID",WorkList_ID), 
                new SqlParameter("@OFFICE_ID",OFFICE_ID), 
                new SqlParameter("@PreventiveAction",PreventiveAction),
                new SqlParameter("@MODIFIED_BY",MODIFIED_BY),
                new SqlParameter("@TOSYNC",TOSYNC)               
            };
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_WORKLIST_PREVENTIVEACTION", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public int Insert_Followup(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID, string FOLLOWUP, int CREATED_BY, int TOSYNC)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID),
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID),
                new SqlParameter("@VESSEL_ID",VESSEL_ID),
                new SqlParameter("@FOLLOWUP",FOLLOWUP), 
                new SqlParameter("@CREATED_BY",CREATED_BY),
                new SqlParameter("@TOSYNC", TOSYNC)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_TEC_WL_INS_FollowUp", sqlprm));
        }
        //public DataTable Get_FollowupList_ByJobID(int OFFICE_ID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //      { 
        //        new SqlParameter("@OFFICE_ID",OFFICE_ID)                
        //    };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_FollowupList_ByJobID", sqlprm).Tables[0];
        //}

        public DataTable Get_FollowupList_DL(int OFFICE_ID, int VESSEL_ID, int WORKLIST_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID) ,               
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID)                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_FollowupList", sqlprm).Tables[0];
        }



        public DataTable Get_WorkList_Crew_Involved_DL(int OFFICE_ID, int VESSEL_ID, int WORKLIST_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID) ,               
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID)                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_WorkList_Crew_Involved", sqlprm).Tables[0];
        }


        public DataTable Get_Crew_Involved_To_Add_List_DL(string SearchText, int? VESSEL_ID, int? Rank_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
               
                new SqlParameter("@SearchText",SearchText) , 
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
                new SqlParameter("@Rank_ID",Rank_ID)                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_WorkList_Crew_Involved_To_Add_Search", sqlprm).Tables[0];
        }

        public int WorkList_Crew_Involved_Insert_DL(int? OFFICE_ID, int? VESSEL_ID, int? WORKLIST_ID, int? VOYAGE_ID, int? CREATED_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID) ,               
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID) ,   
                new SqlParameter("@VOYAGE_ID",VOYAGE_ID) ,   
                new SqlParameter("@CREATED_BY",CREATED_BY) ,   
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "WORKLIST_CREW_INVOLVED_Insert", sqlprm);
        }

        public int WorkList_Crew_Involved_Delete_DL(int? ID, int? VESSEL_ID, int? OFFICE_ID, int? CREATED_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@ID",ID) ,      
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
                new SqlParameter("@OFFICE_ID",OFFICE_ID) ,   
                new SqlParameter("@CREATED_BY",CREATED_BY) ,   
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "WORKLIST_CREW_INVOLVED_Delete", sqlprm);
        }



        public DataTable Get_IncompleteJobs_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_IncompleteJobs").Tables[0];
        }
        public DataTable Get_CompletedJobs_DL(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@StartDate",StartDate) ,               
                new SqlParameter("@EndDate",EndDate)               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_CompletedJobs", sqlprm).Tables[0];
        }
        public DataSet Get_CompletedJobs_Prev2Wks_DL(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@StartDate",StartDate) ,               
                new SqlParameter("@EndDate",EndDate)               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_CompletedJobsPrev2Wks", sqlprm);
        }
        public int Get_CompletedJobsCount_DL(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@StartDate",StartDate) ,               
                new SqlParameter("@EndDate",EndDate)               
            };
            string strRet = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_CompletedJobsCount", sqlprm).ToString();
            if (strRet.Length > 0)
                return int.Parse(strRet);
            else
                return 0;
        }
        public DataTable Get_IncompleteJobs_NoFollowUp_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_IncompleteJobs_NoFollowUp").Tables[0];
        }

        public DataTable Get_IncompleteJobs_WithFollowUp_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_IncompleteJobs_WithFollowUp").Tables[0];
        }

        public int Sync_Job_AfterUpdate_DL(int VESSEL_ID, int WORKLIST_ID, int OFFICE_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID),
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID),
                new SqlParameter("@OFFICE_ID",OFFICE_ID)
            };

            SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_TEC_WL_Sync_Job_AfterUpdate", sqlprm);
            return 1;
        }
        public int UPDATE_Tech_Meeting_Flag_DL(int VESSEL_ID, int WORKLIST_ID, int OFFICE_ID, int FLAG_Tech_Meeting, int Flagged_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",VESSEL_ID),
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID),
                new SqlParameter("@OFFICE_ID",OFFICE_ID),
                new SqlParameter("@FLAG_Tech_Meeting",FLAG_Tech_Meeting),
                new SqlParameter("@Flagged_By",Flagged_By)
            };

            SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_Tech_Meeting_Flag", sqlprm);
            return 1;
        }

        public int ReleaseComplaint_ToFlag_DL(int Vessel_ID, int Worklist_ID, int OFFICE_ID, int Released_By, string DPARemarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VESSEL_ID",Vessel_ID),
                new SqlParameter("@WORKLIST_ID",Worklist_ID),
                new SqlParameter("@OFFICE_ID",OFFICE_ID),
                new SqlParameter("@Released_By",Released_By),
                new SqlParameter("@DPARemarks",DPARemarks)
            };

            SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_TEC_WL_ReleaseComplaint_ToFlag", sqlprm);
            return 1;
        }

        public DataTable Get_Worklist_Attachments_DL(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@WL_Office_ID", WL_Office_ID),
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Worklist_Attachments", sqlprm).Tables[0];
        }

        public int Insert_Worklist_Attachment_DL(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string Attach_Name, string Attach_Path, long Attach_Size, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@WL_Office_ID", WL_Office_ID),
                new SqlParameter("@Attach_Name", Attach_Name),
                new SqlParameter("@Attach_Path", Attach_Path),
                new SqlParameter("@Attach_Size", Attach_Size),
                new SqlParameter("@Created_By", Created_By),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }


        public int Insert_ActivityObject_DL(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string imageaudioPath, string Attach_Path, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@Vessel_ID", Vessel_ID),                
                new SqlParameter("@OFFICE_ID", WL_Office_ID),
                new SqlParameter("@imageaudioName", imageaudioPath),   
                new SqlParameter("@imageaudioPath", Attach_Path),               
                new SqlParameter("@Created_By", Created_By),
                //new SqlParameter("return",SqlDbType.Int)
            };
            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_INS_ActivityObject", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }
        public int Delete_Worklist_Attachment_DL(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@WL_Office_ID", WL_Office_ID),
                new SqlParameter("@AttachmentID", AttachmentID),
                new SqlParameter("@Deleted_By", Deleted_By),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_TEC_WL_Delete_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }
        public int INSP_Delete_WorklistAttachment(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselID", Vessel_ID),
                new SqlParameter("@WorklistID", Worklist_ID),
                new SqlParameter("@OfficeID", WL_Office_ID),
                new SqlParameter("@AttachmentID", AttachmentID),
                new SqlParameter("@DeletedBy", Deleted_By),
              
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Delete_WorklistAttachment", sqlprm);


            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }

        public int INSP_Delete_ActivityObject(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselID", Vessel_ID),
                new SqlParameter("@WorklistID", Worklist_ID),
                new SqlParameter("@OfficeID", WL_Office_ID),
                new SqlParameter("@AttachmentID", AttachmentID),
                //new SqlParameter("@DeletedBy", Deleted_By),
              
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Delete_ActivityObject", sqlprm);


            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }
        public DataTable Get_InspectorList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Get_InspectorList").Tables[0];
        }

        public int Upd_Worklist_Status(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int User_ID, string Remark, string Action_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@WL_Office_ID", WL_Office_ID),
                new SqlParameter("@REMARK", Remark),
                new SqlParameter("@ACTION_TYPE", Action_Type),
                new SqlParameter("@USER_ID", User_ID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_UPD_WORKLIST_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public DataTable Get_Reqsn_List(string Search, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Search", Search),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_GET_REQSN_LIST", sqlprm).Tables[0];
        }

        public int INSP_Delete_Activity(int VESSEL_ID, int WorkList_ID, int OFFICE_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@VesselID",VESSEL_ID), 
                new SqlParameter("@WorklistID",WorkList_ID), 
                new SqlParameter("@OfficeID",OFFICE_ID)
              };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Delete_Activity", sqlprm);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable Get_Worklist_Access_ByUser(int User_ID, string Action_Type, string Job_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@User_ID", User_ID),
                new SqlParameter("@Action_Type",Action_Type),
                new SqlParameter("@Job_Type",Job_Type),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_Get_Worklist_Access_ByUser", sqlprm).Tables[0];
        }

        public DataTable Get_WL_JobCreationTrend()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_JobCreationTrend").Tables[0];
        }
        public DataTable Get_WL_JobsUpdated_InLast24Hours()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_JobsUpdated_InLast24Hours").Tables[0];
        }
        public DataTable Get_WL_JobsCreated_InLast24Hours()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_JobsCreated_InLast24Hours").Tables[0];
        }
        public DataTable Get_WL_Jobs_Super_Inspected()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_Jobs_Super_Inspected").Tables[0];
        }
        public DataTable Get_WL_NCR_Raised_InLast3Months()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_NCR_Raised_InLast3Months").Tables[0];
        }
        public DataTable Get_WL_NCR_Trend_BarChart()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_NCR_Trend_BarChart").Tables[0];
        }
        public DataTable Get_WL_Super_Inspected_BarChart()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_Super_Inspected_BarChart").Tables[0];
        }
        public DataTable Get_WL_HighPriority_Jobs()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_HighPriority_Jobs").Tables[0];
        }
        public DataTable Get_Worklist_Audio_Files()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_Get_Worklist_Audio_Files").Tables[0];
        }
        public int Upd_Worklist_Attachment_FOLLOWUP(int Vessel_ID, int Followup_ID, int Office_ID, int User_ID, string Followup_Status, string Attach_Path, string Action_type, int Worklist_ID, string Followup)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Followup_ID", Followup_ID),
                new SqlParameter("@WL_Office_ID", Office_ID),
                new SqlParameter("@USER_ID", User_ID),
                new SqlParameter("@FOLLOWUP_STATUS", Followup_Status),
                new SqlParameter("@ATTACH_PATH", Attach_Path),
                new SqlParameter("@ACTION_TYPE", Action_type),
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@FOLLOWUP", Followup),
                 new SqlParameter("@return",SqlDbType.Int)
               
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_UPD_Worklist_Attachment_Followup", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }




        public DataTable Generate_Report(int InspectionDetailId, int ShowImages)
        {
            string @TBODY = "";
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionDetailId",InspectionDetailId),
                new SqlParameter("@ShowImages",ShowImages),
                new SqlParameter("@TBODY",@TBODY)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_INSPECTIONWORKLIST", sqlprm).Tables[0];
        }
        public DataSet INSP_Get_WorklistReport(int InspectionID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionID",InspectionID),
            
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_WorklistReport", sqlprm);
        }


        public DataSet Generate_Excel_Report(DataTable WLIDS)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@WLIDS",WLIDS) 
               
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_WORKLISTTOEXCEL", sqlprm);
        }

        public DataTable Get_Crewlist_Index_By_Vessel(string SearchText, int? RankID, int? Status, int? UserID, int? PAGE_SIZE, int? PAGE_INDEX, string _SORT_COLUMN, string _SORT_DIRECTION, int? Vessel_ID, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@SearchText",SearchText),
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@Status",Status),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@SORT_COLUMN",_SORT_COLUMN),
                new SqlParameter("@SORT_DIRECTION",_SORT_DIRECTION),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crewlist_Index_By_Vessel", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }
        public DataSet Get_WorkList_Index_Filter(int Filter_Type, int PAGE_INDEX, int? PAGE_SIZE, ref int Record_Count)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@Filter_Type", Filter_Type),
                   new SqlParameter("@PAGE_INDEX", PAGE_INDEX),
                   new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                     new SqlParameter("@return",  SqlDbType.Int)
              };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_GET_WORKLIST_CUSTOM", sqlprm);
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
        }
        public DataSet Get_WorkList_Index_Filter(int Filter_Type, int PAGE_INDEX, int? PAGE_SIZE, ref int Record_Count, string SortBy, string SORT_DIRECTION)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@Filter_Type", Filter_Type),
                   new SqlParameter("@PAGE_INDEX", PAGE_INDEX),
                   new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                     new SqlParameter("@SortBy", SortBy),
                   new SqlParameter("@SORT_DIRECTION", SORT_DIRECTION),
                     new SqlParameter("@return",  SqlDbType.Int)
              };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_GET_WORKLIST_CUSTOM", sqlprm);
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
        }

        //Added by vasu Dt:18-02-2016
        #region Function_Location_SubLocation

        public DataTable GetSettingforFunctions()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GetSettingforFunctions").Tables[0];
        }



        public int SaveJobFunctionSetting(DataTable dt, int _logUserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@dt", dt),                              
                   new SqlParameter("@ModifiedBy", _logUserID)
              };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_Update_Settings", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public DataTable GetJob_Function_Details(int _officeID, int _WorklistID, int _VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@OFFICE_ID", _officeID),
                   new SqlParameter("@WORKLIST_ID", _WorklistID),
                   new SqlParameter("@VESSEL_ID", _VesselID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GetJob_Function_Details", sqlprm).Tables[0];
        }


        public DataTable GET_SYSTEM_LOCATION(int Function, int VESSEL_ID)
        {

            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Function", Function), 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID), 
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_SYSTEM_LOCATION", obj);
            dt = ds.Tables[0];
            return dt;

        }

        public DataTable GET_SUBSYTEMSYSTEM_LOCATION(string SYSTEMCODE, int? SUBSYSTEMID, int VESSEL_ID)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", SYSTEMCODE), 
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", SUBSYSTEMID), 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_GET_SUBSYTEMSYSTEM_LOCATION", obj);
            dt = ds.Tables[0];
            return dt;

        }

        public string asyncGet_Function_Information(int Worklist_ID, int WL_Office_ID, int Vessel_ID)
        {
             DataTable dt = new DataTable();
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@Worklist_ID", Worklist_ID),
                 new SqlParameter("@WL_Office_ID", WL_Office_ID),                 
                 new SqlParameter("@Vessel_ID", Vessel_ID),  

               
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_GET_Function_Info", sqlprm);
            dt = ds.Tables[0];
            return dt.Rows[0][0].ToString();
        }

        public  DataTable Set_Exist_SystemLocation(int code)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@Code", code),
                   
                
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Set_SYSTEM_LOCATION", sqlprm).Tables[0];

        }
        public  DataTable Set_Exist_SubSystemLocation(int code)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@Code", code),
                   
                
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_SET_SUBSYTEMSYSTEM_LOCATION", sqlprm).Tables[0];

        }

        //Adding end by vasu Dt:18-02-2016
        #endregion

        public int IsJobAssignedInInspection(int Vessel_ID, int WL_Office_ID, int Worklist_ID)
        {
             SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VESSEL_ID",Vessel_ID),
                new SqlParameter("@OFFICE_ID",WL_Office_ID),
                new SqlParameter("@WORKLIST_ID",Worklist_ID)
            };

             return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "TEC_WL_JobAssignedTo_Inspection", sqlprm);
        }
    }
}