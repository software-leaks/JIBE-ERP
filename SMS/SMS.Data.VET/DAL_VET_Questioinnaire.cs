using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SMS.Data.VET
{
   public class DAL_VET_Questioinnaire
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataSet VET_Get_Questionnaire(int? Module, DataTable dtVesselType, DataTable dtVettingType, DataTable dtStatus, string SearchQuest_Number, string SearchQuest_Version, string SearchQuestionnaire, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {

                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Module", Module),  
                   new System.Data.SqlClient.SqlParameter("@dtVesselType", dtVesselType),
                   new System.Data.SqlClient.SqlParameter("@dtVettingType", dtVettingType),
                   new System.Data.SqlClient.SqlParameter("@dtStatus",dtStatus ), 
                   new System.Data.SqlClient.SqlParameter("@SearchQuest_Number",SearchQuest_Number),
                   new System.Data.SqlClient.SqlParameter("@SearchQuest_Version",SearchQuest_Version),
                   new System.Data.SqlClient.SqlParameter("@SearchQuestionnaire",SearchQuestionnaire),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),              
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Questionnaire", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable VET_Get_QuestionnaireDetailsByID(int? Questionnaire_ID)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID", Questionnaire_ID),   
                   
                   
                    
            };
        
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionnaireDetailsByID", obj);
           
            return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet VET_Get_QuestionnaireDetails(int? Questionnaire_Id, DataTable dtSectionNO, DataTable dtQuestionNo, string SearchQuestion, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
           try
           {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID",Questionnaire_Id),
                   new System.Data.SqlClient.SqlParameter("@dtSectionNO",dtSectionNO),
                   new System.Data.SqlClient.SqlParameter("@dtQuestionNo",dtQuestionNo),
                   new System.Data.SqlClient.SqlParameter("@SearchQuestion",SearchQuestion),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),              
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionnaireDetails", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

        public DataSet VET_Get_VersionNumber(string Number, string PreVersion)
        {
            try{
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Number",Number),
                   new System.Data.SqlClient.SqlParameter("@PreVersion_No",PreVersion),
                   
                   
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VersionNumber", obj);

            return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Ins_NewVersionQuestionDetails(string Number, string PreVersion, string NewVersion, int? Questionnaire_ID, string Status, int userID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                                        { 
                                            new SqlParameter("@Number",Number),
                                            new SqlParameter("@PreVersion",PreVersion),
                                            new SqlParameter("@NewVersion",NewVersion),
                                            new SqlParameter("@Questionnaire_ID",Questionnaire_ID),   
                                            new SqlParameter("@Status",Status),   
                                            new SqlParameter("@UserID",userID)
                                        };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Ins_NewVersionQuestionDetails", obj);
                 return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Ins_Questionnaire(int? Module_ID, int? VetType_ID, DataTable VesselType_ID, string Questionnaire_Name, string Number, string Version, int userID)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                                        { 
                                            new SqlParameter("@Module_ID",Module_ID),
                                            new SqlParameter("@VetType_ID",VetType_ID),
                                            new SqlParameter("@dtVesselType",VesselType_ID),
                                            new SqlParameter("@Questionnaire_Name",Questionnaire_Name),
                                            new SqlParameter("@Number",Number),
                                            new SqlParameter("@Version",Version),
                                            new SqlParameter("@UserID",userID)
                                        };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Ins_Questionnaire", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Ins_SectionAndQuestion(int? Questionnaire_ID, string Type, int? Section_No, int? Level_1, int? Level_2, int? Level_3, string Question, string Remarks, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Questionnaire_ID",Questionnaire_ID),
                                            new SqlParameter("@Type",Type),
                                            new SqlParameter("@Section_No",Section_No),
                                            new SqlParameter("@Level_1",Level_1),
                                            new SqlParameter("@Level_2",Level_2),
                                            new SqlParameter("@Level_3",Level_3),
                                            new SqlParameter("@Question",Question),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_SectionAndQuestion", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Upd_QuestionAndSection(int? Question_ID, int? Section_No, int? Level_1, int? Level_2, int? Level_3, string Question, string Remarks, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Question_ID",Question_ID),
                                            new SqlParameter("@Section",Section_No),                                             
                                            new SqlParameter("@Level_1",Level_1),
                                            new SqlParameter("@Level_2",Level_2), 
                                            new SqlParameter("@Level_3",Level_3), 
                                            new SqlParameter("@Question",Question),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_QuestionAndSection", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Ins_QuestionnaireAttachment(int? Questionnaire_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID", Questionnaire_ID ),                  
                   new System.Data.SqlClient.SqlParameter("@Attachement_Name", Attachement_Name),
                   new System.Data.SqlClient.SqlParameter("@Attachement_Path", Attachement_Path),
                   new System.Data.SqlClient.SqlParameter("@UserID", userID),
                  
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_QuestionnaireAttachment", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Del_Questionnaire(int? Questionnaire_ID, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Questionnaire_ID",Questionnaire_ID),                                           
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_Questionnaire", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Upd_QuestionnaireStatus(string NewVersion, string Number, int userID, int? Questionnaire_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@NewVersion",NewVersion),
                                            new SqlParameter("@Number",Number),                                           
                                            new SqlParameter("@UserID",userID),
                                            new SqlParameter("@Questionnaire_ID",Questionnaire_ID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_QuestionnaireStatus", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_QuestionnaireAttachment(int? Questionnaire_ID)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID", Questionnaire_ID),   
                   
                   
                    
            };
           
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionnaireAttachment", obj);
          
            return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VET_Del_Question(int? Question_ID, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Question_ID",Question_ID),                                           
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_Question", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet VET_Get_ExistQuestionForQuestionnaire(int? Questionnaire_Id)
        {
            try
            {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_ID",Questionnaire_Id),
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExistQuestionForQuestionnaire", obj);

            return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet VET_Get_ExistQuestinnaire(int? Module_ID, int? VetType_ID, DataTable VesselType_ID, string Number, string Version)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
              
                                      
                                            new SqlParameter("@Module_ID",Module_ID),
                                            new SqlParameter("@VetType_ID",VetType_ID),
                                            new SqlParameter("@dtVesselType",VesselType_ID),
                                            new SqlParameter("@Number",Number),
                                            new SqlParameter("@Version",Version)
                                           
                                        };



                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExistQuestinnaire", obj);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_ExistQuestionNo(int? Questionnire_Id, int? Section, int? Level_1, int? Level_2, int? Level_3, string Mode, int? QuestionId)
        {
            try
            {
                System.Data.DataTable dt= new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj=new System.Data.SqlClient.SqlParameter[]
                {
                     
                      new System.Data.SqlClient.SqlParameter("@Questionnire_Id", Questionnire_Id),   
                      new System.Data.SqlClient.SqlParameter("@Section", Section), 
                      new System.Data.SqlClient.SqlParameter("@Level_1", Level_1),   
                      new System.Data.SqlClient.SqlParameter("@Level_2", Level_2), 
                      new System.Data.SqlClient.SqlParameter("@Level_3", Level_3),   
                      new System.Data.SqlClient.SqlParameter("@Mode", Mode),   
                      new System.Data.SqlClient.SqlParameter("@QuestionId", QuestionId),   
                      
                };
                 System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExistQuestionNo", obj);

                 return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
        public DataTable VET_Get_SectionList(int? Questionnnire_Id)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Questionnnire_Id", Questionnnire_Id),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_SectionList", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable VET_Get_QuestionList(int? Questionnnire_Id)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 { 
                   new System.Data.SqlClient.SqlParameter("@Questionnnire_Id", Questionnnire_Id),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionList", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_QuestionNoBySectionNo(int? Questionnaire_Id,DataTable dtSectionNo)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                 {  
                   new System.Data.SqlClient.SqlParameter("@Questionnaire_Id", Questionnaire_Id),
                   new System.Data.SqlClient.SqlParameter("@dtSectionNo", dtSectionNo),
                 };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_QuestionNoBySectionNo", obj);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
