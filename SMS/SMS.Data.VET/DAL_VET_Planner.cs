using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace SMS.Data.VET
{
   public class DAL_VET_Planner
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DataSet VET_Get_Vetting(int? fleetcode, DataTable dtVessel, DataTable dtOilMajor, int IsPlanned, DataTable dtVetType, int? ExInDays, DataTable dtExtIspector, DateTime? LastVetFrom, DateTime? LastVetTo, DateTime? ExpDateFrom, DateTime? ExpDateTo, DateTime? PlanDateFrom, DateTime? PlanDateTo, string sortby, int? sortdirection)
        {

            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FLEETCODE",fleetcode),
                                            new SqlParameter("@VESSELID",dtVessel),
                                            new SqlParameter("@OilMajors",dtOilMajor),
                                            new SqlParameter("@IsPlanned",IsPlanned),
                                            new SqlParameter("@VettingType",dtVetType),
                                            new SqlParameter("@ExpiresInDays",ExInDays),
                                            new SqlParameter("@ExtInspector",dtExtIspector),
                                            new SqlParameter("@LastVetFrom",LastVetFrom),
                                            new SqlParameter("@LastVetTo",LastVetTo),
                                            new SqlParameter("@ExpDateFrom",ExpDateFrom),
                                            new SqlParameter("@ExpDateTo",ExpDateTo),
                                            new SqlParameter("@PlanDateFrom",PlanDateFrom),
                                            new SqlParameter("@PlanDateTo",PlanDateTo),
                                            new SqlParameter("@SORTBY",sortby), 
                                            new SqlParameter("@SORTDIRECTION",sortdirection), 
                                         
                                        };
               
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Vetting", sqlprm);
                
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public DataTable VET_Get_VettingType()
        {
            try
            {
             
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingType");
             
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Get_VettingSetting(int VesselID, int VettingTypeID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",VesselID),
                                            new SqlParameter("@Vetting_Type_ID",VettingTypeID),
                                        };
                int res = Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingSetting", sqlprm).ToString());

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet VET_Get_CalendarViewVetting(int? fleetcode, DataTable dtVessel, DataTable dtOilMajor, int IsPlanned, DataTable dtVetType, int? ExInDays, DataTable dtIntInspector, DataTable dtExtInspector, DateTime? LastVetFrom, DateTime? LastVetTo, DateTime? ExpDateFrom, DateTime? ExpDateTo, DateTime? PlanDateFrom, DateTime? PlanDateTo, string sortby, int? sortdirection)
        {

            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FLEETCODE",fleetcode),
                                            new SqlParameter("@VESSELID",dtVessel),
                                            new SqlParameter("@OilMajors",dtOilMajor),
                                            new SqlParameter("@IsPlanned",IsPlanned),
                                            new SqlParameter("@VettingType",dtVetType),
                                            new SqlParameter("@ExpiresInDays",ExInDays),
                                            new SqlParameter("@IntInspectors",dtIntInspector),
                                            new SqlParameter("@ExtInspectors",dtExtInspector),
                                            new SqlParameter("@LastVetFrom",LastVetFrom),
                                            new SqlParameter("@LastVetTo",LastVetTo),
                                            new SqlParameter("@ExpDateFrom",ExpDateFrom),
                                            new SqlParameter("@ExpDateTo",ExpDateTo),
                                            new SqlParameter("@PlanDateFrom",PlanDateFrom),
                                            new SqlParameter("@PlanDateTo",PlanDateTo),
                                            new SqlParameter("@SORTBY",sortby), 
                                            new SqlParameter("@SORTDIRECTION",sortdirection), 
                                         
                                        };

                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_CalendarViewVetting", sqlprm);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet VET_Get_VettingDetails(int VettingID, DataTable dtSection, DataTable dtQuestion, DataTable dtVetType, DataTable dtObsStatus, DataTable dtJobStatus, DataTable dtCategory, DataTable dtRiskLevel, string sortby, int? sortdirection)
        {

            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vetting_ID",VettingID),
                                            new SqlParameter("@Section_No",dtSection),
                                            new SqlParameter("@Question_No",dtQuestion),
                                            new SqlParameter("@Type",dtVetType),
                                            new SqlParameter("@OBSStatus",dtObsStatus),
                                            new SqlParameter("@JobStatus",dtJobStatus),
                                            new SqlParameter("@Category",dtCategory),
                                            new SqlParameter("@RiskLevel",dtRiskLevel),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                           
                                         
                                        };

                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingDetails", sqlprm);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      

        public string VET_Get_VettingByVTypeVessel(int VesselID, int VettingTypeID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",VesselID),
                                            new SqlParameter("@Vetting_Type_ID",VettingTypeID),
                                        };
                string res = SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingByVTypeVessel", sqlprm).ToString();

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet VET_GET_FiltersForVetting(int VettingID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VettingID",VettingID),
                                          
                                        };
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_GET_FiltersForVetting", sqlprm);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable VET_Get_PendingJobsTooltip(int? Vetting_ID, int? Question_ID,int? Observation_ID)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),  
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID),  
                    new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID)  
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_PendingJobsTooltip", obj);
            return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_QuestionForToolTip(int? Questionnaire_ID, int? SectionNo, int? Question_ID)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@QuestionnaireID", Questionnaire_ID),  
                   new System.Data.SqlClient.SqlParameter("@SectionNo", SectionNo),  
                    new System.Data.SqlClient.SqlParameter("@QuestionID", Question_ID)  
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionForToolTip", obj);
            return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_VettingRemarks(int? Vetting_ID)
        {
           try
           {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),  
                 
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingRemarks", obj);
            return ds.Tables[0];
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

        public int VET_Ins_VettingRemark(int? Vetting_ID,string Remark,int CreatedBy)
        {
          try
          {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),  
                   new System.Data.SqlClient.SqlParameter("@Remark", Remark),  
                   new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),  
                 
                    
            };

            int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_VettingRemark", obj);
            return res;
          }
          catch (Exception ex)
          {
              throw ex;
          }
        }
        public int VET_Del_Observation(int Observation_ID, int Question_ID, int Vetting_ID, int UserID)
        {
          try
          {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID),  
                   new System.Data.SqlClient.SqlParameter("@Question_ID", Question_ID),  
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),  
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),  
                    
            };

            int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_Observation", obj);
            return res;
          }
          catch (Exception ex)
          {
              throw ex;
          }
        }

        public int VET_Upd_ObservationStatus(int Observation_ID, string Status, int UserId)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Observation_ID", Observation_ID),  
                   new System.Data.SqlClient.SqlParameter("@Status", Status),  
                   new System.Data.SqlClient.SqlParameter("@UserID", UserId),  
               
                 
                    
            };

            int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_ObservationStatus", obj);
            return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int VET_Upd_VettingStatus(int Vetting_ID,DateTime? SelectedDate ,string Status,int UserID)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Vetting_ID", Vetting_ID),  
                   new System.Data.SqlClient.SqlParameter("@Date", SelectedDate),  
                    new System.Data.SqlClient.SqlParameter("@VetttingStatus", Status),  
                      new System.Data.SqlClient.SqlParameter("@UserID", UserID),  
               
               
                 
                    
            };

            int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_VettingStatus", obj);
            return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }    

}
