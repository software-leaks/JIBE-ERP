using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace SMS.Data.VET
{
   public class DAL_VET_Index
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataSet VET_Get_VettingIndex(DataTable dtVessel, DataTable dtVetType, DataTable dtVetIndxStatus, int? DueInDays, int? IsValid, int? IsOpnObs, DataTable dtOilMajor, DataTable dtInspector, DataTable dtExInspector, DateTime? VetDateFrom, DateTime? VetDateTo, DataTable dtJobStatus, string SearchVessel, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@dtVessel", dtVessel),   
                   new System.Data.SqlClient.SqlParameter("@dtVetType", dtVetType),
                   new System.Data.SqlClient.SqlParameter("@dtVetIndxStatus", dtVetIndxStatus),
                   new System.Data.SqlClient.SqlParameter("@DueInDays",DueInDays ), 
                   new System.Data.SqlClient.SqlParameter("@IsValid",IsValid),
                   new System.Data.SqlClient.SqlParameter("@IsOpnObs",IsOpnObs),
                   new System.Data.SqlClient.SqlParameter("@dtOilMajor",dtOilMajor),
                   new System.Data.SqlClient.SqlParameter("@IntInspectors",dtInspector),
                   new System.Data.SqlClient.SqlParameter("@ExtInspectors",dtExInspector),
                   new System.Data.SqlClient.SqlParameter("@VetDateFrom",VetDateFrom),
                   new System.Data.SqlClient.SqlParameter("@VetDateTo",VetDateTo),
                   new System.Data.SqlClient.SqlParameter("@dtJobStatus",dtJobStatus),
                   new System.Data.SqlClient.SqlParameter("@SearchVessel",SearchVessel),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),              
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingIndex", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public int VET_Del_PlannedVetting(int? Vetting_ID, int UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Vetting_ID",Vetting_ID),                                           
                                            new SqlParameter("@UserID",UserID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_PlannedVetting", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int VET_Upd_VettingReworkStatus(int? Vetting_ID, int UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Vetting_ID",Vetting_ID),                                           
                                            new SqlParameter("@UserID",UserID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_VettingReworkStatus", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_OpenObservationTooltip(int? Vetting_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID)                  
                  
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_OpenObservationTooltip", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_CloseObservationTooltip(int? Vetting_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID)                  
                  
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_CloseObservationTooltip", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_NoteTooltip(int? Vetting_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID)                  
                 
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_NoteTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_PendingJobsTooltip(int? Vetting_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID)  
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_PendingJobsTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_GET_QuestionnireList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_GET_QuestionnireList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable VET_Get_SectionByQuestionnireId(DataTable dtQuestionnaire_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@dtQuestionnaire_ID", dtQuestionnaire_ID),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_SectionByQuestionnireId", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_QuestionNoByQuestionnireId(DataTable dtQuestionnaire_ID, DataTable dtSectionNo)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@dtQuestionnaire_ID", dtQuestionnaire_ID),
                   new System.Data.SqlClient.SqlParameter("@dtSectionNo", dtSectionNo),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionNoByQuestionnireId", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_ObservationCategories(string Mode)
        {
          
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   
                   new System.Data.SqlClient.SqlParameter("@Mode", Mode),
                   
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObservationCategories", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet VET_Get_ObservationIndex(DataTable dtQuestionnaire_ID, DataTable dtSectionNO, DataTable dtQuestionNo, int? ObsNoteType, string ObservationStatus, int? Fleet_Id, DataTable dtVessel, DataTable dtOilMajor, DataTable dtInspector, DataTable dtExInspector, DataTable dtCategories, DataTable dtRiskLevel, string Search_Obs_Vessel, DateTime? VetDateFrom, DateTime? VetDateTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@dtQuestionnaire_ID", dtQuestionnaire_ID),   
                   new System.Data.SqlClient.SqlParameter("@dtSectionNO", dtSectionNO),
                   new System.Data.SqlClient.SqlParameter("@dtQuestionNo", dtQuestionNo),
                   new System.Data.SqlClient.SqlParameter("@ObsNoteType",ObsNoteType ), 
                   new System.Data.SqlClient.SqlParameter("@ObservationStatus",ObservationStatus ), 
                   new System.Data.SqlClient.SqlParameter("@Fleet_Id",Fleet_Id),
                   new System.Data.SqlClient.SqlParameter("@dtVessel",dtVessel),
                   new System.Data.SqlClient.SqlParameter("@dtOilMajor",dtOilMajor),
                   new System.Data.SqlClient.SqlParameter("@IntInspectors",dtInspector),
                   new System.Data.SqlClient.SqlParameter("@ExtInspectors",dtExInspector),
                   new System.Data.SqlClient.SqlParameter("@dtCategories",dtCategories),
                   new System.Data.SqlClient.SqlParameter("@dtRiskLevel",dtRiskLevel),
                   new System.Data.SqlClient.SqlParameter("@Search_Obs_Vessel",Search_Obs_Vessel),
                   new System.Data.SqlClient.SqlParameter("@VetDateFrom",VetDateFrom),
                   new System.Data.SqlClient.SqlParameter("@VetDateTo",VetDateTo),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),              
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObservationIndex", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_ObsIndxRelatedJobsTooltip(int? Question_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID),
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)  
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObsIndxRelatedJobsTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method is used to insert observations details that is attched with worklist job
        /// </summary>
        /// <param name="Observation_ID">Observation id that is attached to worklist job</param>
        /// <param name="Jobhistory_ID">Valid worklist id</param>
        /// <param name="Vessel_ID">For which vessel worklist job is created</param>
        /// <param name="Office_ID">Valid office id</param>
        /// <param name="UserID">Login user id</param>
        /// <returns>rows affected</returns>
        public int VET_INS_Assign_Job_To_Be_Vetting(int Observation_ID, int Jobhistory_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Observation_ID",Observation_ID),   
                                            new SqlParameter("@Jobhistory_ID",Jobhistory_ID),           
                                            new SqlParameter("@Vessel_ID",Vessel_ID),           
                                            new SqlParameter("@Office_ID",Office_ID),           
                                            new SqlParameter("@User_ID",UserID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_INS_Assign_Job_To_Be_Vetting", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method is used to display observation wise jobs and veeting wise job list
        /// </summary>
        /// <param name="FilterJobsBy"> Observation ID or Vetting ID</param>
        /// <param name="Mode">Type of filter :value is  Observation or Vetting</param>
        /// <returns>Return jobs details</returns>
        public DataTable VET_Get_Vetting_Jobs_Details(int FilterJobsBy, string Mode)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@FilterJobsBy",FilterJobsBy),   
                                            new SqlParameter("@Mode",Mode),           
                                           
                                        };

                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Vetting_Jobs_Details", sqlprm).Tables[0];
     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method is used to fetch vetting details for report
        /// </summary>
        /// <param name="Vetting_ID">Selected vetting ID from vetting index</param>
        /// <param name="UserID">Login user id</param>
        /// <returns>retrun report details in html format</returns>
        public DataTable VET_Get_Vetting_Report(int Vetting_ID, int UserID)
        {
            try
            {

                string @TBODY = "";
                SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vetting_ID",Vetting_ID),          
                new SqlParameter("@UserID",UserID),          
                new SqlParameter("@TBODY",@TBODY)
            };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Vetting_Report", sqlprm).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_VettingAttachmentType()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingAttachmentType").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int VET_Ins_VettingAttachments(int? Vetting_ID, int? Vetting_AttahmtType_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ),      
                   new System.Data.SqlClient.SqlParameter("@Vetting_AttahmtType_ID", Vetting_AttahmtType_ID ), 
                   new System.Data.SqlClient.SqlParameter("@Attachement_Name", Attachement_Name),
                   new System.Data.SqlClient.SqlParameter("@Attachement_Path", Attachement_Path),
                   new System.Data.SqlClient.SqlParameter("@UserID", userID),
                  
                };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_VettingAttachments", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet VET_Get_VettingExistAttachment(int? Vetting_ID, int? Vetting_AttahmtType_ID)
        {
             try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ),      
                new System.Data.SqlClient.SqlParameter("@Vetting_AttahmtType_ID", Vetting_AttahmtType_ID )
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingExistAttachment", obj);

            return ds;
            }
             catch (Exception ex)
             {
                 throw ex;
             }
        }

        public DataTable VET_Get_VettingAttachment(int? Vetting_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                 new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ) ,   
                   
                   
                    
            };
                
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingAttachment", obj);
              
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Upd_VettingAttachments(int? Vetting_ID, int? Vetting_AttahmtType_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ),      
                   new System.Data.SqlClient.SqlParameter("@Vetting_AttahmtType_ID", Vetting_AttahmtType_ID ), 
                   new System.Data.SqlClient.SqlParameter("@Attachement_Name", Attachement_Name),
                   new System.Data.SqlClient.SqlParameter("@Attachement_Path", Attachement_Path),
                   new System.Data.SqlClient.SqlParameter("@UserID", userID),
                  
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_VettingAttachments", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Del_VettingAttachment(int? Vetting_Attachmt_ID, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Vetting_Attachmt_ID",Vetting_Attachmt_ID),                                           
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_VettingAttachment", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_SectionListByVettingId(int? Vetting_ID, string Mode)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                   new System.Data.SqlClient.SqlParameter("@Mode", Mode),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_SectionListByVettingId", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_QuestionNoByVettingId(int? Vetting_ID, string Mode)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                   new System.Data.SqlClient.SqlParameter("@Mode", Mode),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionNoByVettingId", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_ObservationTypeList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObservationTypeList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable VET_Get_RelatedOpenObsCount()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_RelatedOpenObsCount").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable VET_Get_QuestionByQuestionNo(int? Question_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionByQuestionNo", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_Observation(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID),
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Observation", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public DataSet VET_Get_Response(int? Vetting_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID) ,         
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)             
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Response", obj);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet VET_Get_ObsRelatedJobs(int? Vetting_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID) ,         
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)             
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObsRelatedJobs", obj);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Ins_Observation_Note(int? Vetting_ID, int? Question_ID, int? Observation_Type_ID, string Description, int? OBSCategory_ID, int? Risk_Level, string Status, int UserID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ),      
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID ), 
                   new System.Data.SqlClient.SqlParameter("@Observation_Type_ID", Observation_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@Description", Description),
                   new System.Data.SqlClient.SqlParameter("@OBSCategory_ID", OBSCategory_ID),
                   new System.Data.SqlClient.SqlParameter("@Risk_Level", Risk_Level),
                   new System.Data.SqlClient.SqlParameter("@Status", Status),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                  
                };

                System.Data.DataSet ds= SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Ins_Observation_Note", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Upd_Observation_Note(int? Observation_ID,int? Vetting_ID, int? Question_ID, int? Observation_Type_ID, string Description, int? OBSCategory_ID, int? Risk_Level, string Status, int UserID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID ),      
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ),      
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID ), 
                   new System.Data.SqlClient.SqlParameter("@Observation_Type_ID", Observation_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@Description", Description),
                   new System.Data.SqlClient.SqlParameter("@OBSCategory_ID", OBSCategory_ID),
                   new System.Data.SqlClient.SqlParameter("@Risk_Level", Risk_Level),
                   new System.Data.SqlClient.SqlParameter("@Status", Status),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                  
                };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_Observation_Note", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Ins_ResponseAttachment(int? Response_ID, string Attachement_Name, string Attachement_Path, int UserID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Response_ID", Response_ID ),                  
                   new System.Data.SqlClient.SqlParameter("@Attachement_Name", Attachement_Name),
                   new System.Data.SqlClient.SqlParameter("@Attachement_Path", Attachement_Path),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                  
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_ResponseAttachment", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Ins_Response(int? Observation_ID, string Response, int UserID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID ),                  
                   new System.Data.SqlClient.SqlParameter("@Response", Response),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                  
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Ins_Response", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_ResponseAttachment(int? Response_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                 new System.Data.SqlClient.SqlParameter("@Response_ID", Response_ID ) ,   
                   
                   
                    
            };
                
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ResponseAttachment", obj);
               
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet VET_Get_ObservationWorklistJobs(int Vessel_ID,int? Observation_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@Observation_ID",Observation_ID),
               
                 
            };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObservationWorklistJobs", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int VET_INS_Obs_Assign_Jobs(DataTable dtInspectionWorklist, int Observation_ID, int UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@TBL_ObservationJobs",dtInspectionWorklist),              
                new SqlParameter("@Observation_ID",Observation_ID) ,
                new SqlParameter("@UserID",UserID) 
               
            };

                int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_INS_Obs_Assign_Jobs", sqlprm);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int VET_Upd_UnlinkWorklistJobs(int OBSJobID, int UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ObsJobID",OBSJobID),   
                new SqlParameter("@UserID",UserID)   
              
               
            };

                int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_UnlinkWorklistJobs", sqlprm);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_PortList()
        {
            try
            {

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_INF_Get_PortList");

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_InspectorListByVettingType(int Vetting_Type_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VettingTypeID",Vetting_Type_ID) ,            
              
               
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_InspectorListByVettingType", sqlprm);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Upd_PerformVettingDetails(int? Vetting_ID, string Vetting_Name, DateTime? Vetting_Date, int? Vetting_Type_ID, string Vetting_Type_Name, int? Inspector_ID, int? OilMajor_ID, int? Port_ID, int? Port_Call_ID, int? No_Of_Days, DateTime? Response_Next_Due, int? UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
            {
               
                 new SqlParameter("@Vetting_ID",Vetting_ID),  
                 new SqlParameter("@Vetting_Name",Vetting_Name),
                 new SqlParameter("@Vetting_Date",Vetting_Date),
                 new SqlParameter("@Vetting_Type_ID",Vetting_Type_ID),
                 new SqlParameter("@Vetting_Type_Name",Vetting_Type_Name),
                 new SqlParameter("@InspectorID",Inspector_ID),
                 new SqlParameter("@OilMajor_ID",OilMajor_ID),
                 new SqlParameter("@Port_ID",Port_ID),
                 new SqlParameter("@PortCall_ID",Port_Call_ID),
                 new SqlParameter("@No_Of_Days",No_Of_Days),
                 new SqlParameter("@Response_Next_Due",Response_Next_Due),
                 new SqlParameter("@UserID",UserID),
               
            };

                int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_PerformVettingDetails", sqlprm);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int VET_Upd_VettingDetails(int Vetting_ID, string Vetting_Name,int Vetting_Type_ID,string Vetting_Type_Name, DateTime Vetting_Date, int Questionnaire_ID, int Inspector_ID, int OilMajor_ID, int Port_ID,  int No_Of_Days, int UserID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
            {
               
                 new SqlParameter("@Vetting_ID",Vetting_ID),  
                 new SqlParameter("@Vetting_Name",Vetting_Name),
                  new SqlParameter("@Vetting_Type_ID",Vetting_Type_ID),
                 new SqlParameter("@Vetting_Type_Name",Vetting_Type_Name),
                 new SqlParameter("@Vetting_Date",Vetting_Date),
                 new SqlParameter("@Questionnaire_ID",Questionnaire_ID),
                 new SqlParameter("@InspectorID",Inspector_ID),
                 new SqlParameter("@OilMajor_ID",OilMajor_ID),
                 new SqlParameter("@Port_ID",Port_ID),
                 new SqlParameter("@No_Of_Days",No_Of_Days),
                 new SqlParameter("@UserID",UserID),
                
              
               
            };

                int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_VettingDetails", sqlprm);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_QuestionnaireIdByQuestionId(int? Question_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 {                    
                    new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID )                    
                    
                 };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionnaireIdByQuestionId", obj);             
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_Perform_Vetting_Detail(int Vetting_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
             {
                 new SqlParameter("@Vetting_ID",Vetting_ID), 
             };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Perform_Vetting_Detail", sqlprm).Tables[0];
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
          
        }


        public DataTable VET_GET_Published_QuestionnireList(int VesselID,int Vetting_Type_ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID)  ,
                      new System.Data.SqlClient.SqlParameter("@Vetting_Type_ID", Vetting_Type_ID)  
                    
            };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_GET_Published_QuestionnireList",obj).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public DataTable VET_Get_CompletedJobsTooltip(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                     new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID)  ,
                       new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)  
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_CompletedJobsTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_VerifiedJobsTooltip(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                     new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID)  ,
                       new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)   
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VerifiedJobsTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_DefferedJobsTooltip(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                     new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID)  ,
                       new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)  
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_DefferedJobsTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_ObsResponseTooltip(int? Observation_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)  
                    
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObsResponseTooltip", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int VET_Ins_Vetting(int? Vessel_Id, DateTime? Vetting_Date, int? Vetting_Type_ID, string Vetting_Type_Name, int? Questionaire_ID, int? Oil_Major_ID, int? Inspector_ID, int? Port_ID, int? PortCallID,
                              int? UserID, ref int ReturnValue)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_Id),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Date",Vetting_Date),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_ID",Vetting_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_Name",Vetting_Type_Name),
                   new System.Data.SqlClient.SqlParameter("@Questionaire_ID",Questionaire_ID),
                   new System.Data.SqlClient.SqlParameter("@Oil_Major_ID",Oil_Major_ID),
                   new System.Data.SqlClient.SqlParameter("@Inspector_ID",Inspector_ID),
                   new System.Data.SqlClient.SqlParameter("@Port_ID",Port_ID),
                   new System.Data.SqlClient.SqlParameter("@PortCallID",PortCallID),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),                
                   new System.Data.SqlClient.SqlParameter("@ReturnValue",ReturnValue)    
               };
                obj[obj.Length - 1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_Vetting", obj);
                ReturnValue = Convert.ToInt32(obj[obj.Length - 1].Value);
                return  ReturnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable VET_Get_ExistVetting(int? Vessel_Id, DateTime? Vetting_Date, int? Vetting_Type_ID, int? VettingID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_Id),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Date",Vetting_Date),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_ID",Vetting_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@VettingID",VettingID),
               };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExistVetting", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_QuestionByVettingId_SectionNo(int? Vetting_ID, int? Section_No,string Mode)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),
                   new System.Data.SqlClient.SqlParameter("@Section_No", Section_No),
                   new System.Data.SqlClient.SqlParameter("@Mode", Mode),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionByVettingId_SectionNo", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method is used to insert observations which is import by user.
        /// </summary>
        /// <param name="Vetting_ID">import data on which vetting id against.</param>
        /// <param name="Questionaire_ID">Questionnaire id of vetting</param>
        /// <param name="Vessel_Name">for which veseel</param>
        /// <param name="Vetting_Date">vetting date</param>
        /// <param name="dtQuestion">it has three columns data- question number, type and inspector comments</param>
        /// <param name="UserID">login users id</param>
        /// <returns></returns>
        public int VET_Ins_Import_Obs(int? Vetting_ID, int? Questionaire_ID,DataTable dtQuestion, int? UserID, ref int ReturnValue)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID",Vetting_ID),
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID",Questionaire_ID),                  
                   new System.Data.SqlClient.SqlParameter("@dtQuestion",dtQuestion),           
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),   
                   new System.Data.SqlClient.SqlParameter("@ReturnValue",ReturnValue)  
                  
               };

                obj[obj.Length - 1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_Import_Obs", obj);
                ReturnValue = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ReturnValue;
                
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Procedure is used to check wheather provided question details is exists or not
        /// </summary>
        /// <param name="Questionaire_ID">questionnaire id for this vetting</param>
        /// <param name="QuestionNum">valid question number</param>
        /// <param name="ReturnValue">return question id </param> 
        public int  VET_Questionnaire_Exists(int? Questionaire_ID, string QuestionNum, ref int ReturnValue)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               {                  
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID",Questionaire_ID),
                   new System.Data.SqlClient.SqlParameter("@QuestionNum",QuestionNum),
                   new System.Data.SqlClient.SqlParameter("@ReturnValue",ReturnValue)              
                  
               };
                obj[obj.Length - 1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Questionnaire_Exists", obj);
                ReturnValue = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ReturnValue;
               

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Procedure is used to get questionnaire number from questionniare id
        /// </summary>
        /// <param name="Questionaire_ID">questionnaire id of vetting</param>
        /// <param name="QuestionnaireNum">question number</param>    
        public int VET_Get_QuestNumber_By_QuestionnaireID(int? Questionaire_ID,ref int QuestionnaireNum)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               {                  
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID",Questionaire_ID),                
                   new System.Data.SqlClient.SqlParameter("@QuestionnaireNum",QuestionnaireNum)              
                  
               };
                obj[obj.Length - 1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestNumber_By_QuestionnaireID", obj);
                QuestionnaireNum = Convert.ToInt32(obj[obj.Length - 1].Value);
                return QuestionnaireNum;

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insert import attachment
        /// </summary>
        /// <param name="Vetting_ID">vetting id for which is import is done</param>
        /// <param name="Attachement_Name">attachment path file name</param>
        /// <param name="Attachement_Path">path of file</param>
        /// <param name="userID">log user id</param>  
        public int VET_Ins_AttatmentImport(int? Vetting_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ),                  
                   new System.Data.SqlClient.SqlParameter("@Attachement_Name", Attachement_Name),
                   new System.Data.SqlClient.SqlParameter("@Attachement_Path", Attachement_Path),
                   new System.Data.SqlClient.SqlParameter("@UserID", userID),
                  
                };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_AttatmentImport", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 	Insert import ERROR LOG details
        /// </summary>
        /// <param name="Vetting_ID">vetting id for which is import is done</param>
        /// <param name="Attachement_Name">attachment path file name</param>
        /// <param name="userID">log user id</param>   
       public int VET_Ins_ImportObs_ErrorLog(int? Vetting_ID,string Attachement_Path, int userID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID ), 
                   new System.Data.SqlClient.SqlParameter("@Attachement_Path", Attachement_Path),
                   new System.Data.SqlClient.SqlParameter("@UserID", userID),
                  
                };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_ImportObs_ErrorLog", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       /// <summary>
       /// get  import ERROR LOG details
       /// </summary>
       /// <param name="Vetting_ID">vetting id for which is import is done</param>    
        public DataTable VET_Get_ImportObs_ErrorLog(int? Vetting_ID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),                 
                 };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ImportObs_ErrorLog", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
