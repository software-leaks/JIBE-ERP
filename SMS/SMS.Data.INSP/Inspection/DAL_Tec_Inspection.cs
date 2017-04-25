using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;
using System.Globalization;

namespace SMS.Data.Inspection
{
    public class DAL_Tec_Inspection
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        public DAL_Tec_Inspection()
        {
            //
            // TODO: Add constructor logic here
            //
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

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_Worklist", sqlprm);
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
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

            DataTable ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_GET_WORKLIST", sqlprm).Tables[0];
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
        }
        public DataTable INSP_GET_WORKLIST_JOB(DataTable dtFilter, ref int Record_Count)
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

            DataTable ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_GET_WORKLIST_JOB", sqlprm).Tables[0];
            Record_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_INS_ActivityObject", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }
        public int Insert_ActivityObject_DL(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string imageaudioPath, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@Vessel_ID", Vessel_ID),                
                new SqlParameter("@OFFICE_ID", WL_Office_ID),
                 new SqlParameter("@imageaudioPath", imageaudioPath),               
                new SqlParameter("@Created_By", Created_By),
                //new SqlParameter("return",SqlDbType.Int)
            };
            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_INS_ActivityObject", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
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

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_INS_WORKLIST", sqlprm).Tables[0];


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

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_UPDATE_WORKLIST", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }

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

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Delete_WorklistAttachment", sqlprm);


            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
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
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Worklist_Attachments", sqlprm).Tables[0];
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_UPD_WORKLIST_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

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

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Delete_Activity", sqlprm);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
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

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Delete_ActivityObject", sqlprm);


            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
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

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Delete_WorklistJob", sqlprm);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet Get_JobDetails_ByID(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@OFFICE_ID", OFFICE_ID),
                   new SqlParameter("@WORKLIST_ID", WORKLIST_ID),
                   new SqlParameter("@VESSEL_ID", VESSEL_ID)
              };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Get_JobDetails_ByID", sqlprm);
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Insert_Attachment", sqlprm).Tables[0];
        }
        public int IsVessel(int Vessel_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@VESSEL_ID", Vessel_ID)
              };

                string strRet = Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "SELECT ISVESSEL FROM LIB_VESSELS WHERE VESSEL_ID=@VESSEL_ID", sqlprm));
                return Convert.ToInt32(strRet);
            }
            catch
            {
                return -1;
            }
        }
        public DataSet INSP_Get_WorklistReport(int InspectionID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionID",InspectionID),
            
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistReport", sqlprm);
        }
        public DataTable Get_Dept_OnShip()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Dept_OnShip").Tables[0];
        }
        public DataTable Get_Dept_InOffice()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_Get_Dept_InOffice").Tables[0];
        }
        public DataSet GetAllWorklistType_DL()
        {
            //SqlParameter[] sqlprm = new SqlParameter[1];

            //sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_ALL_WorklistType");
        }
        public int CheckExists_Schedule_DL(int INSPECTORID, string scheduledate, int durJobs, int UserID, int returnval)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@INSPECTORID",INSPECTORID),
                new SqlParameter("@scheduledate",scheduledate),
                new SqlParameter("@durJobs",durJobs),
                //new SqlParameter("@UserID",UserID),               
                //new SqlParameter("@InspectionID",InspectionID),
                new SqlParameter("return",SqlDbType.Int)
               // new SqlParameter("return",SqlDbType.VarChar)
            };
            //sqlprm[0].SqlDbType = SqlDbType.Structured;
            //sqlprm[1].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_CheckExists_Shedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Save_Schedule_DL(DataTable dtSchedule, DataTable dtSettings, DataTable dtChecklist, int UserID, int? InspectionID, DataTable dtPortList)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@TBL_INSPECTION_SCHEDULE",dtSchedule),
                new SqlParameter("@TBL_INSPECTION_SETTINGS",dtSettings),
                new SqlParameter("@TBL_INSPECTION_SHEDULECHECKLIST",dtChecklist),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@TBL_INSPECTION_PORTS",dtPortList),
                new SqlParameter("@InspectionID",InspectionID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[1].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Save_InspectionSchedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataSet INSP_Get_SuptInspReportIconConfig(int CompanyID)
        {
            try
            {

                SqlParameter[] obj = new SqlParameter[]
             {
             
                new SqlParameter("@ClientID", CompanyID)
                    
             };
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_SuptInspReportIconConfig", obj);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int INSP_Update_InspectionForReOpen(int InspectionID, int UserID, string Reason)
        {
            try
            {

                SqlParameter[] obj = new SqlParameter[]
             {
             
                new SqlParameter("@InspectionID", InspectionID),
                   new SqlParameter("@ModifiedBy", UserID),
                   new SqlParameter("@Reason", Reason)
                    
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Update_InspectionForReOpen", obj);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int INSP_Get_RatingsFromChecklistRating(int InspectionID, string ChecklistIDs)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];

            sqlprm[0] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[1] = new SqlParameter("@ChecklistIDs", ChecklistIDs);

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Get_RatingsFromChecklistRating", sqlprm);
        }
        public DataTable GetAttachedFileInfo(string Vessel_ID)
        {
            try
            {
                DataTable dtFileInfo = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID)
             // ,new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
             //new System.Data.SqlClient.SqlParameter("@FileType" ,FileType)    
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_Attached_file_info_Search", obj);
                dtFileInfo = ds.Tables[0];
                return dtFileInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetAttachedFileInfo(int? fleetcode, int? ddlvessel, string search, string category, int? supplier,
              string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                   new System.Data.SqlClient.SqlParameter("@Fleet", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@Vessel",ddlvessel),
                   new System.Data.SqlClient.SqlParameter("@SerchText", search), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_Attached_file_info_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public int SaveAttachedFileInfo(string Vessel_ID, string InspectionDetailId, string FileName, string FilePath, string CreatedBy)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID), 
             new System.Data.SqlClient.SqlParameter("@InspectionDetailId",InspectionDetailId),  
             new System.Data.SqlClient.SqlParameter("@FileName", FileName), 
             new System.Data.SqlClient.SqlParameter("@FilePath",FilePath) ,
             new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
             new SqlParameter("@return",SqlDbType.Int),
             
           
             };

                obj[5].Direction = ParameterDirection.ReturnValue;
                int RetVal = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Ins_File_Attachment_info", obj);
                if (RetVal == 1)
                {
                    RetVal = int.Parse(obj[5].Value.ToString());
                }
                return RetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable INSP_Get_Attachments_DL(string InspectionDetailId, int vesselID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@InspectionDetailId", InspectionDetailId), 
             new System.Data.SqlClient.SqlParameter("@Vessel",vesselID)
            
           
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_GET_Attachments", obj).Tables[0];

        }

        public int INSP_Delete_Attachments_DL(int ID, int Vessel_ID, int UserID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
                new SqlParameter("@ID", ID), 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                     new SqlParameter("@UserID", UserID)
          
             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_DELETE_Attachment", obj);

        }


        public DataTable Get_VesselAttendence(int InspectorID, int InspectionID)
        {
            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@ActualInspectorID", InspectorID), 
             new System.Data.SqlClient.SqlParameter("@InspectionId",InspectionID)
            
           
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_InspectionAttendanceReport", obj).Tables[0];
        }
        public DataSet INSP_Get_RatingLegends()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_RatingLegends");
        }

        public DataTable TEC_Get_SupritendentAttendanceWithPort(DateTime StartDateOfMonth, int CompanyID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
                
             new System.Data.SqlClient.SqlParameter("@StartDateOfMonth", StartDateOfMonth), 
             new System.Data.SqlClient.SqlParameter("@CompanyID", CompanyID), 

            
              
             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_SupritendentAttendanceWithPort", obj).Tables[0];
        }
        public DataTable TEC_Get_SupritendentAttendanceWithPort(DateTime StartDateOfMonth)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
                
             new System.Data.SqlClient.SqlParameter("@StartDateOfMonth", StartDateOfMonth), 
            // new System.Data.SqlClient.SqlParameter("@CompanyID", CompanyID), 

            
              
             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_SupritendentAttendanceWithPort", obj).Tables[0];
        }
        public DataTable TEC_Get_SupInspectionDetailsByDate(DateTime CurrentDate, int InspectorID, int CompanyID)
        {
            SqlParameter[] obj = new SqlParameter[]
             {
             
                
             new System.Data.SqlClient.SqlParameter("@CurrentDate", CurrentDate), 
                new System.Data.SqlClient.SqlParameter("@InspectorID", InspectorID),
                 new System.Data.SqlClient.SqlParameter("@CompanyID", CompanyID)
            
            
           
             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_SupInspectionDetailsByDate", obj).Tables[0];
        }
        public DataTable TEC_Get_SupInspectionDetailsByDate(DateTime CurrentDate, int InspectorID)
        {
            SqlParameter[] obj = new SqlParameter[]
             {
             
                
             new System.Data.SqlClient.SqlParameter("@CurrentDate", CurrentDate), 
                new System.Data.SqlClient.SqlParameter("@InspectorID", InspectorID),
                
            
            
           
             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_SupInspectionDetailsByDate", obj).Tables[0];
        }
        public DataSet Get_CaledndarData(DateTime? DateFrom, DateTime? DateTo, int? Company_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                    new SqlParameter("@DateFrom",DateFrom),
                    new SqlParameter("@DateTo",DateTo), 
                     new SqlParameter("@Company_ID",Company_ID), 
            };

            DataSet dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_CaledndarData", sqlprm);
            //   is_Fetch_Count = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
            return dt;


        }
        public DataSet Get_CaledndarDataBySupt(DateTime? DateFrom, DateTime? DateTo, int? Company_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                    new SqlParameter("@DateFrom",DateFrom),
                    new SqlParameter("@DateTo",DateTo), 
                     new SqlParameter("@Company_ID",Company_ID), 
            };

            DataSet dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_CaledndarDataBySupt", sqlprm);
            //   is_Fetch_Count = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
            return dt;


        }


        public DataTable Insert_NewJob_DL(int VESSEL_ID, string JOB_DESCRIPTION,
          string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
          int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
          int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, string Activity_ID, string status, int? LocationID, int? PSC_SIRE, int IsConditionalReport)
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
                   new SqlParameter("@IsConditionalReport",IsConditionalReport),
                new SqlParameter("@RetWorklistId",SqlDbType.Int),                
                new SqlParameter("@RetOfficeId",SqlDbType.Int),
               
                
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_WL_INS_WORKLIST", sqlprm).Tables[0];


        }



        #region Inspection New Transfer Function From Worklist 04-05-2015

        public int INSP_Get_RatingsByRating(string Rating)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];


            sqlprm[0] = new SqlParameter("@Rating", Rating);


            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "INSP_Get_RatingsByRating", sqlprm));
        }
        public DataSet GetVesselInspectionDetails(int InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_VesselInsptInfo_ExecutiveSummary", sqlprm);
        }
        public DataSet GetExecutiveSummary(int InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_Executive_Summary", sqlprm);
        }
        public DataSet INSP_Get_InspectionReportInfo(int InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_InspectionReportInfo", sqlprm);
        }
        public int INSP_Update_ExecutiveSummary(string ReportNo, string CompanyName, int InspectionID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[4];
            sqlprm[0] = new SqlParameter("@ReportNo", ReportNo);
            sqlprm[1] = new SqlParameter("@CompanyName", CompanyName);
            sqlprm[2] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[3] = new SqlParameter("@CreatedBy", CreatedBy);


            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Update_ExecutiveSummary", sqlprm);

        }
        public int InsertExecutiveSummary(int SummaryTopicKey, string SummaryTopicDetails, int InspectionID, int CreatedBy, DateTime DataOfCreation, int ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[7];
            sqlprm[0] = new SqlParameter("@SummaryTopicKey", SummaryTopicKey);
            sqlprm[1] = new SqlParameter("@SummaryTopicDetails", SummaryTopicDetails);
            sqlprm[2] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[3] = new SqlParameter("@Created_By", CreatedBy);
            sqlprm[4] = new SqlParameter("@Date_Of_Creation", DataOfCreation);
            sqlprm[5] = new SqlParameter("@Active_Status", ActiveStatus);
            //sqlprm[6] = new SqlParameter("@ReportNo", ReportNo);

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Insert_Executive_Summary", sqlprm);
        }
        public int InsertExecutiveSummaryBunkers(int InspectionID, string TotalFOLog, string TotalFOMeasured, string TotalMDOLog, string TotalMDOMeasured, string TotalMGOLog, string TotalMGOMeasured, int CreatedBy, DateTime DateOfCreation, int ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[10];
            if (TotalFOLog != "")
            {
                sqlprm[0] = new SqlParameter("@TotalFO_Log", Convert.ToDecimal(TotalFOLog));
            }

            else
            {
                sqlprm[0] = new SqlParameter("@TotalFO_Log", DBNull.Value);
            }
            if (TotalFOMeasured != "")
            {
                sqlprm[1] = new SqlParameter("@TotalFO_Measured", Convert.ToDecimal(TotalFOMeasured));
            }
            else
            {
                sqlprm[1] = new SqlParameter("@TotalFO_Measured", DBNull.Value);
            }
            if (TotalMDOLog != "")
            {
                sqlprm[2] = new SqlParameter("@TotalMDO_Log", Convert.ToDecimal(TotalMDOLog));
            }
            else
            {
                sqlprm[2] = new SqlParameter("@TotalMDO_Log", DBNull.Value);
            }
            if (TotalMDOMeasured != "")
            {
                sqlprm[3] = new SqlParameter("@TotalMDO_Measured", Convert.ToDecimal(TotalMDOMeasured));
            }
            else
            {
                sqlprm[3] = new SqlParameter("@TotalMDO_Measured", DBNull.Value);
            }
            if (TotalMGOLog != "")
            {
                sqlprm[4] = new SqlParameter("@TotalMGO_Log", Convert.ToDecimal(TotalMGOLog));
            }
            else
            {
                sqlprm[4] = new SqlParameter("@TotalMGO_Log", DBNull.Value);
            }
            if (TotalMGOLog != "")
            {
                sqlprm[5] = new SqlParameter("@TotalMGO_Measured", Convert.ToDecimal(TotalMGOMeasured));
            }
            else
            {
                sqlprm[5] = new SqlParameter("@TotalMGO_Measured", DBNull.Value);
            }

            sqlprm[6] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[7] = new SqlParameter("@Created_By", CreatedBy);
            sqlprm[8] = new SqlParameter("@Date_Of_Creation", DateOfCreation);
            sqlprm[9] = new SqlParameter("@Active_Status", ActiveStatus);

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Insert_Executive_Summary_Bunkers", sqlprm);
        }


        public DataSet GetRatings(string ChecklistIDs)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@ChecklistIDs", ChecklistIDs);
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_Ratings", sqlprm);
        }
        public DataSet GetRatings()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_Ratings");
        }
        public DataSet GetRatingsByID(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@ID", ID);
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_GetByID_Ratings", sqlprm);
        }
        public DataSet GetRatingsByValue(string RatingValue)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@RatingValue", RatingValue);
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_GetByValue_Ratings", sqlprm);
        }
        public DataSet GetCategoryRating(string InspectionID, string CheckListIDs)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];

            sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            sqlprm[1] = new SqlParameter("@ChecklistIDs", CheckListIDs);
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_SystemRating", sqlprm);
        }
        public DataSet GetCategoryRating(string InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[1];

            sqlprm[0] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_SystemRating", sqlprm);
        }
        public DataSet GetSubCategoryRating(string ParentCode, string InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];
            sqlprm[0] = new SqlParameter("@Parent_Code", Convert.ToInt32(ParentCode));
            sqlprm[1] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_SubSystemRating", sqlprm);
        }
        public int InsertRating(string rating, string RatingValue, string RatingColor, string CreatedBy, DateTime DateOfCreation, int ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[6];
            sqlprm[0] = new SqlParameter("@RatingValue", Convert.ToInt32(RatingValue));
            sqlprm[1] = new SqlParameter("@Rating", rating);
            sqlprm[2] = new SqlParameter("@RatingColor", RatingColor);
            sqlprm[3] = new SqlParameter("@Created_By", CreatedBy);
            sqlprm[4] = new SqlParameter("@Date_Of_Creation", DateOfCreation);
            sqlprm[5] = new SqlParameter("@Active_Status", ActiveStatus);

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Insert_Ratings", sqlprm);


        }
        public int UpdateRating(int ID, string rating, int RatingValue, string RatingColor, string ModifiedBy, DateTime DateOfModification, int ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[7];
            sqlprm[0] = new SqlParameter("@ID", ID);
            sqlprm[1] = new SqlParameter("@RatingValue", Convert.ToInt32(RatingValue));
            sqlprm[2] = new SqlParameter("@Rating", rating);
            sqlprm[3] = new SqlParameter("@RatingColor", RatingColor);
            sqlprm[4] = new SqlParameter("@Modified_By", ModifiedBy);
            sqlprm[5] = new SqlParameter("@Date_Of_Modification", DateOfModification);
            sqlprm[6] = new SqlParameter("@Active_Status", ActiveStatus);

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Update_Ratings", sqlprm);


        }
        public int DeleteRestoreRating(int ID, bool ActiveStatus, string ModifiedBy, DateTime DateOfModification)
        {
            SqlParameter[] sqlprm = new SqlParameter[4];
            sqlprm[0] = new SqlParameter("@ID", Convert.ToInt32(ID));
            sqlprm[1] = new SqlParameter("@ActiveStatus", ActiveStatus);
            sqlprm[2] = new SqlParameter("@ModifiedBy", ModifiedBy);
            sqlprm[3] = new SqlParameter("@DateOfModification", DateOfModification);
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_DeleteRestore_Ratings", sqlprm);
        }

        public int InsertCategoryRating(string SystemCode, string SystemLastReport, string SystemCurrentReport, string SystemRating, string ScheduleID, string InspectionID, string CreatedBy, DateTime DateOfCreation, string ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[9];
            sqlprm[0] = new SqlParameter("@System_ID", Convert.ToInt32(SystemCode));

            if (SystemLastReport == "")
            {
                sqlprm[1] = new SqlParameter("@System_Last_Report", DBNull.Value);
            }
            else
            {
                sqlprm[1] = new SqlParameter("@System_Last_Report", Convert.ToDecimal(SystemLastReport));
            }
            if (SystemCurrentReport == "")
            {
                sqlprm[2] = new SqlParameter("@System_Current_Report", DBNull.Value);
            }
            else
            {
                sqlprm[2] = new SqlParameter("@System_Current_Report", Convert.ToDecimal(SystemCurrentReport));
            }
            if (SystemRating == "")
            {

                sqlprm[3] = new SqlParameter("@System_Rating", DBNull.Value);
            }
            else
            {
                sqlprm[3] = new SqlParameter("@System_Rating", SystemRating);
            }
            sqlprm[4] = new SqlParameter("@Schedule_ID", DBNull.Value);
            sqlprm[5] = new SqlParameter("@Inspection_ID", Convert.ToInt32(InspectionID));
            sqlprm[6] = new SqlParameter("@Created_By", CreatedBy);
            sqlprm[7] = new SqlParameter("@Date_Of_Creation", DateOfCreation);
            sqlprm[8] = new SqlParameter("@Active_Status", Convert.ToInt32(ActiveStatus));

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Insert_SystemRating", sqlprm);
        }
        public int InsertSubCategoryRating(string SystemCode, string SubSystemCode, string SubSystemSecLastReport, string SubSystemLastReport, string SubSystemCurrentReport, string SubSystemRating, string Remarks, string AdditionalRemark, string ScheduleID, string InspectionID, string CreatedBy, DateTime DateOfCreation, string ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[13];
            sqlprm[0] = new SqlParameter("@System_ID", Convert.ToInt32(SystemCode));
            sqlprm[1] = new SqlParameter("@SubSystem_ID", Convert.ToInt32(SubSystemCode));
            if (SubSystemSecLastReport == "" || SubSystemSecLastReport == "--SELECT--")
            {
                sqlprm[2] = new SqlParameter("@SubSystem_Second_Last_Report", DBNull.Value);
            }
            else
            {
                sqlprm[2] = new SqlParameter("@SubSystem_Second_Last_Report", Convert.ToDecimal(SubSystemSecLastReport));
            }

            if (SubSystemLastReport == "" || SubSystemLastReport == "--SELECT--")
            {
                sqlprm[3] = new SqlParameter("@SubSystem_Last_Report", DBNull.Value);
            }
            else
            {
                sqlprm[3] = new SqlParameter("@SubSystem_Last_Report", Convert.ToDecimal(SubSystemLastReport));
            }
            if (SubSystemCurrentReport == "" || SubSystemCurrentReport == "--SELECT--")
            {
                sqlprm[4] = new SqlParameter("@SubSystem_Current_Report", DBNull.Value);
            }
            else
            {
                sqlprm[4] = new SqlParameter("@SubSystem_Current_Report", Convert.ToDecimal(SubSystemCurrentReport));
            }
            if (SubSystemRating == "")
            {
                sqlprm[5] = new SqlParameter("@SubSystem_Rating", DBNull.Value);
            }
            else
            {
                sqlprm[5] = new SqlParameter("@SubSystem_Rating", SubSystemRating);
            }
            sqlprm[6] = new SqlParameter("@Additional_Remarks", AdditionalRemark);

            sqlprm[7] = new SqlParameter("@Remarks", Remarks);
            sqlprm[8] = new SqlParameter("@Schedule_ID", DBNull.Value);
            sqlprm[9] = new SqlParameter("@Inspection_ID", Convert.ToInt32(InspectionID));
            sqlprm[10] = new SqlParameter("@Created_By", CreatedBy);
            sqlprm[11] = new SqlParameter("@Date_Of_Creation", DateOfCreation);
            sqlprm[12] = new SqlParameter("@Active_Status", Convert.ToInt32(ActiveStatus));

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Insert_SubsystemRating", sqlprm);
        }


        public int INSP_Update_SubSystemRating(int InspectionID, int SubSystemID, string RatingValue, string Rating, int ModifiedBy, DateTime DateOfModification)
        {

            SqlParameter[] sqlprm = new SqlParameter[6];

            sqlprm[0] = new SqlParameter("@SubSystem_ID", Convert.ToInt32(SubSystemID));

            if (RatingValue == "" || RatingValue == "--SELECT--")
            {
                sqlprm[1] = new SqlParameter("@RatingValue", DBNull.Value);
            }
            else
            {
                sqlprm[1] = new SqlParameter("@RatingValue", Convert.ToInt32(RatingValue));
            }
            if (Rating == "")
            {
                sqlprm[2] = new SqlParameter("@Rating", DBNull.Value);
            }
            else
            {
                sqlprm[2] = new SqlParameter("@Rating", Rating);
            }

            sqlprm[3] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            sqlprm[4] = new SqlParameter("@ModifiedBy", ModifiedBy);
            sqlprm[5] = new SqlParameter("@Date_Of_Modification", DateOfModification);
            //sqlprm[6] = new SqlParameter("@Active_Status", Convert.ToInt32(ActiveStatus));

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Update_SubSystemRating", sqlprm);

        }

        public int INSP_Update_SubSystemRating(int InspectionID, int? SubSystemID, string RatingValue, string Rating, int ModifiedBy, DateTime DateOfModification)
        {

            SqlParameter[] sqlprm = new SqlParameter[6];

            sqlprm[0] = new SqlParameter("@SubSystem_ID", Convert.ToInt32(SubSystemID));

            if (RatingValue == "" || RatingValue == "--SELECT--")
            {
                sqlprm[1] = new SqlParameter("@RatingValue", DBNull.Value);
            }
            else
            {
                sqlprm[1] = new SqlParameter("@RatingValue", Convert.ToInt32(RatingValue));
            }
            if (Rating == "")
            {
                sqlprm[2] = new SqlParameter("@Rating", DBNull.Value);
            }
            else
            {
                sqlprm[2] = new SqlParameter("@Rating", Rating);
            }

            sqlprm[3] = new SqlParameter("@InspectionID", Convert.ToInt32(InspectionID));
            sqlprm[4] = new SqlParameter("@ModifiedBy", ModifiedBy);
            sqlprm[5] = new SqlParameter("@Date_Of_Modification", DateOfModification);
            //sqlprm[6] = new SqlParameter("@Active_Status", Convert.ToInt32(ActiveStatus));

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_Update_SubSystemRating", sqlprm);

        }
        public DataSet GetYearlyInspectionSummary(string EndYear)
        {
            SqlParameter sqlprm = new SqlParameter("@ENDYEAR", EndYear);

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_INSPECTION_YEAR_SUMMARY", sqlprm);
        }




        public DataSet Get_Schedule_Details_DL(int ScheduleID, int UserID, int SchDetailId, int SubSysID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID),
                 new SqlParameter("@SchDetailId",SchDetailId),
                  new SqlParameter("@SubSysID",SubSysID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule_Details", sqlprm);
        }

        public DataSet Get_Schedule_Details_DL(int ScheduleID, int UserID, int SchDetailId, int? LocationID, int LocationNodeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID),
                 new SqlParameter("@SchDetailId",SchDetailId),
                  new SqlParameter("@SubSysID",LocationID),
                  new SqlParameter("@LocationNodeID",LocationNodeID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule_Details_LocationNodeID", sqlprm);
        }



        public void Update_Inspection(int InspectionDetailID, int InspectorID, DateTime? InspectionDate, String Status, int User_Id, int OnBoard, int OnShore, DateTime? CompletionDate, int? DurJobs)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionDetailID",InspectionDetailID),
                new SqlParameter("@InspectorID",InspectorID),
                 new SqlParameter("@InspectionDate",InspectionDate),
                  new SqlParameter("@Status",Status),
                   new SqlParameter("@User_Id",User_Id),
                     new SqlParameter("@OnBoard ",OnBoard ),
                   new SqlParameter("@OnShore",OnShore),
                     new SqlParameter("@CompletionDate",CompletionDate),
                       new SqlParameter("@DurJobs",DurJobs),
                       //  new SqlParameter("@PortID",PortID),
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Update_Inspection", sqlprm);
        }

        public DataTable INSP_Get_WorklistReportWithImages(int InspectionDetailId, int ShowImages, string ReportType)
        {
            string @TBODY = "";
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionID",InspectionDetailId),
                new SqlParameter("@ShowImages",ShowImages),
                new SqlParameter("@ReportType",ReportType),
                 new SqlParameter("@TBODY",@TBODY)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistReportWithImages", sqlprm).Tables[0];
        }

        public DataTable INSP_Get_WorklistReportWithCategoryGrouping(int InspectionDetailId, int ShowImages, string ReportType, string ChecklistIDs)
        {
            string @TBODY = "";
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionID",InspectionDetailId),
                new SqlParameter("@ShowImages",ShowImages),
                new SqlParameter("@ReportType",ReportType),
                  new SqlParameter("@ChecklistIDs",ChecklistIDs),
                 new SqlParameter("@TBODY",@TBODY)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistReportWithCategoryGrouping", sqlprm).Tables[0];
        }
        public DataTable INSP_Get_WorklistReportWithCategoryGrouping(int InspectionDetailId, int ShowImages, string ReportType)
        {
            string @TBODY = "";
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionID",InspectionDetailId),
                new SqlParameter("@ShowImages",ShowImages),
                new SqlParameter("@ReportType",ReportType),
                 
                 new SqlParameter("@TBODY",@TBODY)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistReportWithCategoryGrouping", sqlprm).Tables[0];
        }

        public DataTable Get_InspectionScheduleByVessel_DL(int Vessel_ID, int StartMonth, int Eva_Type, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@StartMonth",StartMonth),
                                            new SqlParameter("@Eva_Type",Eva_Type),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionScheduleByVessel", sqlprm).Tables[0];

        }
        public DataSet INSP_Get_InspectionPort(int InspectionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];


            sqlprm[0] = new SqlParameter("@InspectionID", InspectionID);


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_InspectionPorts", sqlprm);
        }
        public int INSP_InsertUpdate_InspectionPort(DataTable dtPorts, int InspectionID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[3];


            sqlprm[0] = new SqlParameter("@dtPort", dtPorts);

            sqlprm[1] = new SqlParameter("@CreatedBy", CreatedBy);
            sqlprm[2] = new SqlParameter("@InspectionID", InspectionID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "INSP_InsertUpdate_InspectionPort", sqlprm));
        }


        public int INSP_Get_WorklistReportByLocationID(int InspectionID, int SubSystemID)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];


            sqlprm[0] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[1] = new SqlParameter("@LocationID", SubSystemID);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistReportByLocationID", sqlprm));
        }

        public int INSP_Get_WorklistJobsCountByLocationID(int InspectionID, int SubSystemID)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];


            sqlprm[0] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[1] = new SqlParameter("@LocationID", SubSystemID);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistJobsCountByLocationID", sqlprm));
        }
        public int INSP_Get_WorklistJobsCountByLocationID(int InspectionID, int? SubSystemID)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];


            sqlprm[0] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[1] = new SqlParameter("@LocationID", SubSystemID);

            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistJobsCountByLocationIDRet", sqlprm);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistJobsCountByLocationID", sqlprm));
        }

        public int INSP_Get_WorklistJobsCountByLocationNodeID(int InspectionID, int LocationNodeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[2];


            sqlprm[0] = new SqlParameter("@InspectionID", InspectionID);
            sqlprm[1] = new SqlParameter("@LocationNodeID", LocationNodeID);

            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistJobsCountByLocationIDRet", sqlprm);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "INSP_Get_WorklistJobsCountByLocationNodeID", sqlprm));
        }

        public DataSet Get_Current_Schedules_DL(string Status, int Vessel_ID, DateTime? InspectionFromDate, DateTime? InspectionToDate, int PortId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Status",Status),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@InspectionFromDate",InspectionFromDate),
                 new SqlParameter("@InspectionToDate",InspectionToDate),
                new SqlParameter("@PortId",PortId)
            };
            DataSet dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule", sqlprm);
            return dt;
        }


        public DataSet Get_Current_Schedules_DL(string Frequency_Type, int Last_Run_Success, string Status, string SearchText, int UserID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int? Vessel_ID, int? InspectorID, DateTime? DateFrom, DateTime? DateTo, int? InspectionTypeId, int? OverDue, string sortexp, int? Fleet_Id, int? Company_ID = null)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
            new SqlParameter("@Frequency_Type",Frequency_Type),
            new SqlParameter("@Last_Run_Success",Last_Run_Success),
            new SqlParameter("@Status",Status),
            new SqlParameter("@SearchText",SearchText),
            new SqlParameter("@UserID",UserID) ,
            new SqlParameter("@PAGE_INDEX",Page_Index),
            new SqlParameter("@PAGE_SIZE",Page_Size),
              new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@InspectorID",InspectorID),
                  new SqlParameter("@DateFrom",DateFrom),
                    new SqlParameter("@DateTo",DateTo),
                     new SqlParameter("@InspectionTypeId",InspectionTypeId),
                     new SqlParameter("@OverDue",OverDue),
                      new SqlParameter("@sortexp",sortexp),
                      new SqlParameter("@Fleet_Id",Fleet_Id),
                       new SqlParameter("@Company_ID",Company_ID),
            new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule_Index", sqlprm);
            is_Fetch_Count = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
            return dt;


        }

        public DataSet Get_Current_Schedules_DL(int InspectionId, string Frequency_Type, int Last_Run_Success, string Status, string SearchText, int UserID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int? Vessel_ID, int? InspectorID, DateTime? DateFrom, DateTime? DateTo, int? InspectionTypeId, int? OverDue, string sortexp, int? Fleet_Id, int? Company_ID = null)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Frequency_Type",Frequency_Type),
                new SqlParameter("@Last_Run_Success",Last_Run_Success),
                new SqlParameter("@Status",Status),
                new SqlParameter("@SearchText",SearchText),
                new SqlParameter("@UserID",UserID) ,
                new SqlParameter("@PAGE_INDEX",Page_Index),
                new SqlParameter("@PAGE_SIZE",Page_Size),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@InspectorID",InspectorID),
                new SqlParameter("@DateFrom",DateFrom),
                new SqlParameter("@DateTo",DateTo),
                new SqlParameter("@InspectionTypeId",InspectionTypeId),
                new SqlParameter("@OverDue",OverDue),
                new SqlParameter("@sortexp",sortexp),
                new SqlParameter("@Fleet_Id",Fleet_Id),
                new SqlParameter("@Company_ID",Company_ID),
                new SqlParameter("@InspectionId",InspectionId),
                new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule_Index", sqlprm);
            is_Fetch_Count = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
            return dt;


        }

        public DataTable Get_Supritendent_Users(int? Fleet_ID, int? Vessel_ID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Fleet_ID",Fleet_ID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_Supritendent_Users", sqlprm).Tables[0];


        }

        public DataTable TEC_WL_Get_Supritendent_UsersByCompanyID(int? CompanyID, int? Vessel_ID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CompanyID",CompanyID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_Supritendent_UsersByCompanyID", sqlprm).Tables[0];


        }

        public DataTable Get_ChecklistOnVesselType_DL(int? Vessel_ID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                //new SqlParameter("@Fleet_ID",Fleet_ID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_ChecklistOnVesselType", sqlprm).Tables[0];


        }

        public DataTable Get_ImagePath_DL(DataTable dtInspectionWorklist)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                //new SqlParameter("@Fleet_ID",Fleet_ID),
                new SqlParameter("@TBL_INSPECTIONWORKLIST",dtInspectionWorklist),
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_GET_ImagePath", sqlprm).Tables[0];


        }

        public int Save_InspectionWorklistWithNodeVal_DL(DataTable dtInspectionWorklist, int? InspectionId, int? LocationID, int? LocationNodeID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@TBL_INSPECTIONWORKLIST",dtInspectionWorklist), 
                 new SqlParameter("@InspectionId",InspectionId), 
                  new SqlParameter("@LocationID",LocationID), 
                   new SqlParameter("@LocationNodeID",LocationNodeID), 
                new SqlParameter("@UserID",UserID) 
               
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Save_InspectionWorklistNodeVal", sqlprm);
            return 1;
        }

        public DataSet Get_CheckList_Worklist_DL(int Vesel_ID, int NodeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vesel_ID",Vesel_ID),
                new SqlParameter("@NodeID",NodeID),
                 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule_Details_LocationNodeID", sqlprm);
        }

        public int Save_InspectionWorklist(DataTable dtInspectionWorklist, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@TBL_INSPECTIONWORKLIST",dtInspectionWorklist), 
                new SqlParameter("@UserID",UserID) 
               
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Save_InspectionWorklist", sqlprm);
            return 1;
        }

        public int Save_InspectionWorklistWithNodeVal_DL(DataTable dtInspectionWorklist, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@TBL_INSPECTIONWORKLIST",dtInspectionWorklist), 
                new SqlParameter("@UserID",UserID) 
               
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Save_InspectionWorklistNodeVal", sqlprm);
            return 1;
        }

        public int TEC_WL_INS_InspectionWorklist_Location(int InspectionId, int WorkiListID, int VesselID, int OfficeID, int LocationID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionDetailId",InspectionId), 
                new SqlParameter("@WORKLIST_ID",WorkiListID), 
                 new SqlParameter("@VESSEL_ID",VesselID), 
                new SqlParameter("@OFFICE_ID",OfficeID),
                 new SqlParameter("@LocationID",LocationID), 
                new SqlParameter("@Created_By",CreatedBy) 
               
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_INS_InspectionWorklist_Location", sqlprm);
            return 1;
        }

        public int TEC_WL_INS_InspectionWorklist_Location(int InspectionId, int WorkiListID, int VesselID, int OfficeID, int? LocationID, int LocationNodeID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@InspectionDetailId",InspectionId), 
                new SqlParameter("@WORKLIST_ID",WorkiListID), 
                new SqlParameter("@VESSEL_ID",VesselID), 
                new SqlParameter("@OFFICE_ID",OfficeID),
                new SqlParameter("@LocationID",LocationID),
                new SqlParameter("@LocationNodeID",LocationNodeID), 
                new SqlParameter("@Created_By",CreatedBy) 
               
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_INS_InspectionWorklist_Location", sqlprm);
            return 1;
        }

        public void Del_ScheduledInspection(int? ID, int? DELETED_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ID",ID) ,
                new SqlParameter("@DELETED_BY",DELETED_BY) 
               
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_WL_DEL_ScheduledInspection", sqlprm);
        }

        public DataSet Get_InspInfo(int? UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@UserId",UserId)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_GETINSPINFO", sqlprm);
        }
        public DataSet Get_CheckList_Worklist_DL_Direct(int Vesel_ID, int InspDetailID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vesel_ID",Vesel_ID),                
                    new SqlParameter("@InspDetailID",InspDetailID),   
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_WL_Get_InspectionSchedule_Details_Direct", sqlprm);
        }


        public DataSet Get_SurveyCertificateToolTip(int Surv_Details_ID, int Surv_Vessel_ID, int Vessel_ID, int OfficeID, char Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                   new SqlParameter("@Surv_Details_ID",Surv_Details_ID),
                   new SqlParameter("@Surv_Vessel_ID",Surv_Vessel_ID), 
                   new SqlParameter("@Vessel_ID",Vessel_ID), 
                   new SqlParameter("@OfficeID",OfficeID),
                   new SqlParameter("@Type",Type)
            };

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INSP_Get_SurveyCertificateToolTip", sqlprm);
            return ds;
        }

        public int SurveyRenewalInspection(int Surv_Details_ID, int Surv_Vessel_ID, int Vessel_ID, int OfficeID, int ScheduleID, ref int ReturnInspectionID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                   {
                    new SqlParameter("@Surv_Details_ID",Surv_Details_ID), 
                    new SqlParameter("@Surv_Vessel_ID",Surv_Vessel_ID), 
                    new SqlParameter("@Vessel_ID",Vessel_ID), 
                    new SqlParameter("@OfficeID",OfficeID),
                    new SqlParameter("@ScheduleID",ScheduleID),
                    new SqlParameter("@ReturnInspectionID",ReturnInspectionID)
                   };

                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;
                sqlprm[sqlprm.Length - 1].SqlDbType = SqlDbType.Int;

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INSP_UPD_SurveyRenewalInspection", sqlprm);
                ReturnInspectionID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}
