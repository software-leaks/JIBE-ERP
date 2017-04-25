using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Evaluation
    {
        private static string connection = "";
        public DAL_Crew_Evaluation(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_Crew_Evaluation()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        #region Admin

        public static int INSERT_EvaluationLocation(int Evaluation_ID, int EvaluatorID, int PreNotifyDays, int Created_By,int ChkStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EvaluationID",Evaluation_ID),
                                            new SqlParameter("@EvaluatorID",EvaluatorID),
                                            new SqlParameter("@PreNotifyDays",PreNotifyDays),
                                            new SqlParameter("@ChkStatus",ChkStatus),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                        };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_Insert_EvaluationLocation", sqlprm);
        }
        public static DataTable Get_EvaluationLocation(int EvaluationID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EvaluationID",EvaluationID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_Get_EvaluationLocation", sqlprm).Tables[0];
        }
        public static DataTable Get_CategoryList_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_CategoryList", sqlprm).Tables[0];
        }
        public static int INSERT_Category_DL(string Category_Name, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category_Name",Category_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Category_DL(int ID, string Category_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category_Name",Category_Name),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Category_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_GradingList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_GradingList").Tables[0];
        }
        public static int INSERT_Grading_DL(string Grading_Name,int Grade_Type, int Min, int Max, int Divisions, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_Name",Grading_Name),
                                            new SqlParameter("@Grade_Type",Grade_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Grading_DL(int ID, string Grading_Name, int Grading_Type, int Min, int Max, int Divisions, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Grade_Name",Grading_Name),
                                            new SqlParameter("@Grade_Type",Grading_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Grading_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_GradingOptions", sqlprm).Tables[0];
        }
        public static int INSERT_GradingOption_DL(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID),
                                            new SqlParameter("@OptionText",OptionText),
                                            new SqlParameter("@OptionValue",OptionValue),                                            
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_GradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_GradingOption_DL(int Option_ID, string OptionText, decimal OptionValue, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Option_ID",Option_ID),
                                            new SqlParameter("@OptionText",OptionText),
                                            new SqlParameter("@OptionValue",OptionValue),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_GradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_GradingOptions_DL(int Grade_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_GradingOptions", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public static DataTable Get_EvaluationTypeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationTypeList").Tables[0];
        }
        public static int INSERT_EvaluationType_DL(string EvaluationType, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EvaluationType",EvaluationType),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_EvaluationType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_EvaluationType_DL(int ID, string EvaluationType, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@EvaluationType",EvaluationType),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_EvaluationType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_EvaluationType_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_EvaluationType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public static int Update_EvaluationRanks(int ParentRank, DataTable ChildRanks, DataTable VerifiersRanks, string Configuration_Type, int Updated_By)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentRank",ParentRank),
                                            new SqlParameter("@ChildRanks",ChildRanks),
                                            new SqlParameter("@VerifiersRanks",VerifiersRanks),
                                               new SqlParameter("@Configuration_Type",Configuration_Type),
                                            new SqlParameter("@UserId",Updated_By),
                                            
                                        };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_UPDATE_EVALUATIONRANKS", sqlprm);
                return 1;
            }
            catch
            {
                throw;
            }

        }
        public static DataSet Get_EvaluationRanks(int ParentRank, string Configuration_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentRank",ParentRank),
                                               new SqlParameter("@Configuration_Type",Configuration_Type),
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_GET_EVALUATIONRANKS", sqlprm);
        }
        
        #endregion

        #region QuestionBank
        public static DataTable Get_CriteriaList_DL(string SearchText, int CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Category_ID",CategoryID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_CriteriaList",sqlprm).Tables[0];
        }

        public static int INSERT_Criteria_DL(string Criteria, int CatID,int Grading_Type, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Criteria",Criteria),
                                            new SqlParameter("@Category_ID",CatID),
                                            new SqlParameter("@Grading_Type",Grading_Type),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Criteria", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Criteria_DL(int ID, string Criteria, int CatID, int Grading_Type, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Criteria",Criteria),
                                            new SqlParameter("@Category_ID",CatID),
                                            new SqlParameter("@Grading_Type",Grading_Type),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_Criteria", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Criteria_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_Criteria", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion

        #region Evaluation

        public static DataTable Get_Evaluations_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluations").Tables[0];

        }
        public static DataTable Get_Evaluations_DL(string FilterName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FilterName",FilterName)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluations_Search", sqlprm).Tables[0];

        }
        public static DataTable Get_Assigned_Evaluations_ForCrew_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Assigned_Evaluations", sqlprm).Tables[0];

        }
        public static DataTable Get_Evaluations_DL(int Evaluation_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluations", sqlprm).Tables[0];

        }
        public static DataTable Get_EvaluationMonths_DL(int Evaluation_ID, int RankID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@RankID",RankID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationMonths", sqlprm).Tables[0];

        }

        public static int INSERT_Evaluation_DL(string Evaluation_Name, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_Name",Evaluation_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Evaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int INSERT_EvaluationMonths_DL(int Evaluation_ID,int RankID, int Month, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_EvaluationMonths", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_EvaluationMonths_DL(int Evaluation_ID, int RankID, int Month,int Active_Status, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Active_Status",Active_Status),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_EvaluationMonths", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_Evaluation_DL(int Evaluation_ID, string Evaluation_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Evaluation_Name",Evaluation_Name),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_Evaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Evaluation_DL(int Evaluation_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_Evaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Assigned_CriteriaList_DL(int Evaluation_ID, string SearchText, int CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@CategoryID",CategoryID),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Assigned_CriteriaList", sqlprm).Tables[0];
        }
        public static DataTable Get_UnAssigned_CriteriaList_DL(int Evaluation_ID, string SearchText, int CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@CategoryID",CategoryID),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_UnAssigned_CriteriaList", sqlprm).Tables[0];
        }

        public static int Add_Criteria_ToEvaluation_DL(int Criteria_ID, int Evaluation_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Criteria_ID",Criteria_ID),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Add_Criteria_ToEvaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Remove_Criteria_FromEvaluation_DL(int Criteria_ID, int Evaluation_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Criteria_ID",Criteria_ID),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Remove_Criteria_FromEvaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Crew_EvaluationSchedule_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Crew_EvaluationSchedule",sqlprm).Tables[0];

        }
        public static DataTable Get_EvaluationScheduleByVessel_DL(int Vessel_ID, int RankID, int StartMonth, int Eva_Type, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@StartMonth",StartMonth),
                                            new SqlParameter("@Eva_Type",Eva_Type),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationScheduleByVessel", sqlprm).Tables[0];

        }
        public static DataTable Get_EvaluationScheduleByVessel_DL(int Vessel_ID, int StartMonth, int Eva_Type, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@StartMonth",StartMonth),
                                            new SqlParameter("@Eva_Type",Eva_Type),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationScheduleByVessel", sqlprm).Tables[0];

        }

        public static void Swap_Criteria_Sort_Order_DL(int Evaluation_ID, int Criteria_ID, int UpDown)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Criteria_ID",Criteria_ID),
                                            new SqlParameter("@UpDown",UpDown),
                                        };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Swap_Criteria_Sort_Order", sqlprm);

        }

        public static DataTable Get_CrewEvaluationResults_DL(int CrewID, int Year)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Year",Year) 
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Crew_Evaluation_Result", sqlprm).Tables[0];

        }

        public static int INSERT_Crew_Evaluation_DL(int CrewID, int Rank, int Evaluation_ID, int Evaluator_ID, DateTime Evaluation_Date, int Created_By, int MonthNo, int Year, int Schedule_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Evaluator_ID",Evaluator_ID),
                                            new SqlParameter("@Evaluation_Date",Evaluation_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@MonthNo",MonthNo),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int INSERT_Crew_Evaluation_DL(int CrewID,  int Evaluation_ID, int Evaluator_ID, DateTime Evaluation_Date, int Created_By, DateTime DueDate, int Schedule_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Evaluator_ID",Evaluator_ID),
                                            new SqlParameter("@Evaluation_Date",Evaluation_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@DueDate",DueDate),                                            
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int INSERT_Crew_Evaluation_Answer_DL(int CrewEvaluation_ID, DataTable dt, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewEvaluation_ID",CrewEvaluation_ID),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation_Answer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        //public static int INSERT_Crew_Evaluation_Answer_DL(int CrewEvaluation_ID, int Criteria_ID, int UserAnswer,string Remarks, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewEvaluation_ID",CrewEvaluation_ID),
        //                                    new SqlParameter("@Criteria_ID",Criteria_ID),
        //                                    new SqlParameter("@UserAnswer",UserAnswer),
        //                                    new SqlParameter("@Remarks",Remarks),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation_Answer", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        public static int INSERT_Crew_Evaluation_Answer_DL(int CrewEvaluation_ID, int Criteria_ID, int UserAnswer,string TextAnswer, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewEvaluation_ID",CrewEvaluation_ID),
                                            new SqlParameter("@Criteria_ID",Criteria_ID),
                                            new SqlParameter("@UserAnswer",UserAnswer),
                                            new SqlParameter("@TextAnswer",TextAnswer),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation_Answer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        
        public static DataTable Get_CrewEvaluation_Details_DL(int CrewID, int CrewDtl_Evaluation_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@CrewDtl_Evaluation_ID",CrewDtl_Evaluation_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_CrewEvaluation_Details", sqlprm).Tables[0];

        }

        public static int UPDATE_EvaluationRules_DL(int Evaluation_ID, int RankID, int RuleID, int Active_Status, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@RuleID",RuleID),
                                            new SqlParameter("@Active_Status",Active_Status),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_EvaluationRules", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int DELETE_EvaluationRules_DL(int Evaluation_ID, int RankID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_EvaluationRules", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_EvaluationRules_DL(int Evaluation_ID, int RankID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@RankID",RankID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationRules", sqlprm).Tables[0];

        }

        public static DataTable Get_EvaluationSelectedRanks_DL(int Evaluation_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationSelectedRanks", sqlprm).Tables[0];

        }
        public static DataTable Get_EvaluationUnSelectedRanks_DL(int Evaluation_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationUnSelectedRanks", sqlprm).Tables[0];

        }

        public static int Plan_Crew_Evaluation_Adhoc_DL(int CrewID, int Evaluation_ID, DateTime DueDate, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@DueDate",DueDate),
                                            new SqlParameter("@Created_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Plan_Crew_Evaluation_Adhoc", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Get_CrewEvaluation_FeedbackCount_DL(int? UserID, int Dtl_Evaluation_ID, int Office_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@CrewEvaluation_ID",Dtl_Evaluation_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_Get_Feedback_Count", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_Get_Feedback_Count", sqlprm).Tables[0];

        }


        public static DataTable Get_CrewEvaluationFeedbackRequest_Details_DL(int ID, int Vessel_ID, int Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                             new SqlParameter("@Office_ID",Office_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_Get_FeedbackRequest", sqlprm).Tables[0];

        }
        public static int INS_CrewEvaluationFeedbackRequest_Details_DL(int ID, int Vessel_ID, int Office_ID, int Created_By, int Requested_From, DateTime? DueDate, string Evaluation_ID, string Month, string Schedule_ID, int Crew_ID, string Req_Comment, int? FeedbackCategory_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                             new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Requested_From",Requested_From),
                                            new SqlParameter("@DueDate",DueDate),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                             new SqlParameter("@Crew_ID",Crew_ID),
                                              new SqlParameter("@Remarks",Req_Comment),
                                               new SqlParameter("@FeedbackCategory_ID",FeedbackCategory_ID),
                                              
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_INS_FeedbackRequest", sqlprm);

        }
        public static int INS_CrewEvaluationFeedback_DL(int? ID, int Crew_ID, string Remarks, int Created_By, string Evaluation_ID, string Month, string Schedule_ID, int? Dtl_Evaluation_ID, int? Vessel_ID, int? Office_ID, int FeedbackCategory_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Crew_ID",Crew_ID),
                                             new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                            new SqlParameter("@Dtl_Evaluation_ID",Dtl_Evaluation_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                              new SqlParameter("@FeedbackCategory_ID",FeedbackCategory_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_INS_Feedback", sqlprm);

        }

        public static DataTable Get_BelowAverageEvaluation_Details_DL(int CrewID, int CrewDtl_Evaluation_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@CrewDtl_Evaluation_ID",CrewDtl_Evaluation_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_BelowAverageEvaluation_Details", sqlprm).Tables[0];

        }
        public static DataTable Get_FeedbackCompleted_DL(int ID, int Vessel_ID, int Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                             new SqlParameter("@Office_ID",Office_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_Get_FeedbackCompleted", sqlprm).Tables[0];

        }
        public static DataTable Get_FeedbackCategories_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_GET_FeedbackCategories").Tables[0];

        }


        public static DataTable Get_Assigned_EvaluatorRank_ForCrew_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                           
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_VesselEvaluatorRankList", sqlprm).Tables[0];

        }












        public static int Add_MandatoryGrades_ToEvaluation_DL(int Criteria_ID, int Evaluation_ID, int Created_By, DataTable dtOptionValue)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Criteria_ID",Criteria_ID),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                             new SqlParameter("@OptionText",dtOptionValue),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Add_MandatoryGrades", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static DataTable Get_MandatoryGrades_DL(int Evaluation_ID, int Criteria_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Criteria_ID",Criteria_ID),
                                           
                                           
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_MandatoryGrades", sqlprm).Tables[0];
        }

        public static DataTable Get_MandatoryRemark_DL(int ID, int Evaluation_ID, int Criteria_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Evaluation_ID",Evaluation_ID),
                                            new SqlParameter("@Criteria_ID",Criteria_ID),                                        
                                           
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_MandatoryRemark", sqlprm).Tables[0];
        }
        public static DataSet Get_CrewEvaluation_Verification(int? CrewId, int Dtl_Evaluation_ID, int Office_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId),
                                            new SqlParameter("@CrewEvaluation_ID",Dtl_Evaluation_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_Get_Verification", sqlprm);
        }

        public static DataSet Update_CrewEvaluation_Verification(int? CrewId, int Dtl_Evaluation_ID, int Office_ID, int Vessel_ID, int UserId, string VerificationComment)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId),
                                            new SqlParameter("@CrewEvaluation_ID",Dtl_Evaluation_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserId",UserId),
                                            new SqlParameter("@VerificationComment",VerificationComment),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_Update_Verification", sqlprm);
        }


        public static DataTable Get_EvaluatedCrewDetails(int Schedule_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Schedule_ID",Schedule_ID),                                           
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Evaluation_SP_Get_EvaluatedCrewDetails", sqlprm).Tables[0];
        }
        #endregion

    }

    
}
