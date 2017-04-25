using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Interview
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        private static string connection = "";
        public DAL_Crew_Interview(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_Crew_Interview()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        #region Crew Interview

        public static int Delete_CrewInterview_DL(int InterviewId,int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewId",InterviewId),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DEL_SP_Crew_Interview", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataSet Get_Crew_Interview_Results_DL(int CrewID, int UserID, string InterviewType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@InterviewType",InterviewType)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_Crew_Interview_Results", sqlprm);
        }

        public static DataTable Get_InterviewDetails_DL(int InterviewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewDetails", sqlprm).Tables[0];
        }
        
        public static SqlDataReader Get_UserAnswers_DL(int CrewID, int InterviewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_UserAnswers", sqlprm);
        }

        public static int INS_CrewInterviewPlanning_DL(int CrewID, int Rank, string CandidateName, DateTime InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@CandidateName",CandidateName),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Insert_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_CrewInterviewPlanning_DL(int InterviewID, string InterviewPlanDate, int PlannedInterviewerID, int InterviewRank, int Modified_By)
        {
            DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@InterviewPlanDate",dtInterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@InterviewRank",InterviewRank),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_CrewInterviewPlanning_DL(int InterviewID, string InterviewPlanDate, int PlannedInterviewerID, int InterviewRank, int Modified_By, string TimeZone, int IQID)
        {
            DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@InterviewPlanDate",dtInterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@InterviewRank",InterviewRank),
                                            new SqlParameter("@TimeZone",TimeZone),
                                             new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_PlannedInterviewDetails_DL(int UserID, int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_PlannedInterviewDetailsForTheCrew", sqlprm).Tables[0];
        }
        public static int UPDATE_CrewInterviewResult_DL(int CrewID, int InterviewID, int InterviewerID, string InterviewerPosition, DateTime InterviewDate, int CrewRankID, string ResultText, int Selected, string OtherText, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@InterviewerID",InterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@InterviewDate",InterviewDate),
                                            new SqlParameter("@CrewRankID",CrewRankID),
                                            new SqlParameter("@ResultText",ResultText),
                                            new SqlParameter("@Selected",Selected),
                                            new SqlParameter("@OtherText",OtherText),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_CrewInterviewResult", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_CrewInterviewResult_DL(int CrewID, int InterviewID, int InterviewerID, string InterviewerPosition, DateTime InterviewDate, int CrewRankID, string ResultText, int Selected, string OtherText, int Modified_By, DataTable dtAnswers)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@InterviewerID",InterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@InterviewDate",InterviewDate),
                                            new SqlParameter("@CrewRankID",CrewRankID),
                                            new SqlParameter("@ResultText",ResultText),
                                            new SqlParameter("@Selected",Selected),
                                            new SqlParameter("@OtherText",OtherText),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@dtAnswers",dtAnswers),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_CrewInterviewSheet", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static SqlDataReader Get_InterviewResultsForCrew_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewResultsForCrew", sqlprm);
        }
        public static int UPDATE_CrewInterviewAnswer_DL(int InterviewID, int QuestionID, int SelectedOptionID, string Remarks, int NotApplicable, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@SelectedOptionID",SelectedOptionID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@NotApplicable",NotApplicable),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_CrewInterviewAnswer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion

        #region Admin


        public static DataTable Get_CategoryList_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_CategoryList", sqlprm).Tables[0];
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Insert_Category", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_Category", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Delete_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_GradingList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_GradingList").Tables[0];
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Insert_Grading", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_Grading", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Delete_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_GradingOptions", sqlprm).Tables[0];
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Insert_GradingOption", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_GradingOption", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Delete_GradingOptions", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Interviews_DL(int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_Interviews", sqlprm).Tables[0];

        }
        public static DataTable Get_Interviews_DL(string FilterName, int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@FilterName",FilterName)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_Interviews_Search", sqlprm).Tables[0];

        }
        public static int Delete_Interview_DL(int IQID, int RankID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Delete_Interview", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Interview_DL(int IQID, string Interview_Name, int RankID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@Interview_Name",Interview_Name),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_Interview", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Create_Interview_DL(string Interview_Name, int RankID, int CopyFrom, int Created_By, string InterviewType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Interview_Name",Interview_Name),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@CopyFrom",CopyFrom),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@InterviewType",InterviewType),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Insert_Interview", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_InterviewQuestions_DL(int IQID, int RankID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@RankID",RankID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewQuestions", sqlprm).Tables[0];
        }
        public static DataTable Get_InterviewQuestion_Options_DL(int QuestionID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@QuestionID",QuestionID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewQuestion_Options", sqlprm).Tables[0];
        }

        public static DataTable Get_InterviewQuestions_UnAssigned_DL(int IQID, int RankID, int CategoryID, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@CategoryID",CategoryID),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewQuestions_UnAssigned", sqlprm).Tables[0];
        }

        public static int Add_QuestionToInterview_DL(int QuestionID, int RankID, int IQID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Add_QuestionToInterview", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public static int Remove_QuestionFromInterview_DL(int QuestionID, int IQID, int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Deleted_By",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Remove_QuestionFromInterview", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public static void Swap_InterviewQuestion_Sort_Order_DL(int IQID, int RankID, int QID, int UpDown, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@IQID",IQID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@QID",QID),
                                            new SqlParameter("@UpDown",UpDown),
                                            new SqlParameter("@UserID",UserID)
                                        };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Swap_InterviewQuestion_Sort_Order", sqlprm);

        }
        public static DataSet Get_InterviewQuestionAnswers_DL(int InterviewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewQuestionAnswers", sqlprm);
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

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INT_SP_Get_InterviewCriteriaList", sqlprm).Tables[0];
        }

        public static int INSERT_Criteria_DL(string Criteria, string Answer, int QuestionType, int CatID, int Grading_Type, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Criteria",Criteria),
                                            new SqlParameter("@Answer",Answer),
                                            new SqlParameter("@QuestionType",QuestionType),
                                            new SqlParameter("@Category_ID",CatID),
                                            new SqlParameter("@Grading_Type",Grading_Type),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Insert_InterviewCriteria", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Criteria_DL(int ID, string Criteria, string Answer, int CatID, int Grading_Type, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Criteria",Criteria),
                                            new SqlParameter("@Answer",Answer),
                                            new SqlParameter("@Category_ID",CatID),
                                            new SqlParameter("@Grading_Type",Grading_Type),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Update_InterviewCriteria", sqlprm);
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INT_SP_Delete_InterviewCriteria", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion

        //#region Evaluation

        //public static DataTable Get_Evaluations_DL()
        //{

        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluations").Tables[0];

        //}
        //public static DataTable Get_Evaluations_DL(int Evaluation_ID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluations", sqlprm).Tables[0];

        //}
        //public static DataTable Get_EvaluationMonths_DL(int Evaluation_ID, int RankID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@RankID",RankID)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationMonths", sqlprm).Tables[0];

        //}

        //public static int INSERT_Evaluation_DL(string Evaluation_Name, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_Name",Evaluation_Name),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Evaluation", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public static int INSERT_EvaluationMonths_DL(int Evaluation_ID,int RankID, int Month, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@RankID",RankID),
        //                                    new SqlParameter("@Month",Month),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_EvaluationMonths", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public static int UPDATE_EvaluationMonths_DL(int Evaluation_ID, int RankID, int Month,int Active_Status, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@RankID",RankID),
        //                                    new SqlParameter("@Month",Month),
        //                                    new SqlParameter("@Active_Status",Active_Status),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_EvaluationMonths", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        //public static int UPDATE_Evaluation_DL(int Evaluation_ID, string Evaluation_Name, int Modified_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@Evaluation_Name",Evaluation_Name),
        //                                    new SqlParameter("@Modified_By",Modified_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_Evaluation", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public static int DELETE_Evaluation_DL(int Evaluation_ID, int Deleted_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@Deleted_By",Deleted_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_Evaluation", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        //public static DataTable Get_Assigned_CriteriaList_DL(int Evaluation_ID, string SearchText, int CategoryID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@CategoryID",CategoryID),
        //                                    new SqlParameter("@SearchText",SearchText)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Assigned_CriteriaList", sqlprm).Tables[0];
        //}
        //public static DataTable Get_UnAssigned_CriteriaList_DL(int Evaluation_ID, string SearchText, int CategoryID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@CategoryID",CategoryID),
        //                                    new SqlParameter("@SearchText",SearchText)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_UnAssigned_CriteriaList", sqlprm).Tables[0];
        //}

        //public static int Add_Criteria_ToEvaluation_DL(int Criteria_ID, int Evaluation_ID, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Criteria_ID",Criteria_ID),
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Add_Criteria_ToEvaluation", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        //public static int Remove_Criteria_FromEvaluation_DL(int Criteria_ID, int Evaluation_ID, int Deleted_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Criteria_ID",Criteria_ID),
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@Deleted_By",Deleted_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Remove_Criteria_FromEvaluation", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        //public static DataTable Get_Crew_EvaluationSchedule_DL(int CrewID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewID",CrewID)                                            
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Crew_EvaluationSchedule",sqlprm).Tables[0];

        //}
        //public static DataTable Get_EvaluationScheduleByVessel_DL(int Vessel_ID, int StartMonth, int Eva_Type, string SearchText)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Vessel_ID",Vessel_ID),
        //                                    new SqlParameter("@StartMonth",StartMonth),
        //                                    new SqlParameter("@Eva_Type",Eva_Type),
        //                                    new SqlParameter("@SearchText",SearchText)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationScheduleByVessel", sqlprm).Tables[0];

        //}

        //public static void Swap_Criteria_Sort_Order_DL(int Evaluation_ID, int Criteria_ID, int UpDown)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@Criteria_ID",Criteria_ID),
        //                                    new SqlParameter("@UpDown",UpDown),
        //                                };
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Swap_Criteria_Sort_Order", sqlprm);

        //}

        //public static DataTable Get_CrewEvaluationResults_DL(int CrewID, int Year)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewID",CrewID),
        //                                    new SqlParameter("@Year",Year) 
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Crew_Evaluation_Result", sqlprm).Tables[0];

        //}

        //public static int INSERT_Crew_Evaluation_DL(int CrewID, int Rank, int Evaluation_ID, int Evaluator_ID, DateTime Evaluation_Date, int Created_By, int MonthNo, int Year, int Schedule_ID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewID",CrewID),
        //                                    new SqlParameter("@Rank",Rank),
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@Evaluator_ID",Evaluator_ID),
        //                                    new SqlParameter("@Evaluation_Date",Evaluation_Date),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("@MonthNo",MonthNo),
        //                                    new SqlParameter("@Year",Year),
        //                                    new SqlParameter("@Schedule_ID",Schedule_ID),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

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

        //public static int INSERT_Crew_Evaluation_Answer_DL(int CrewEvaluation_ID, int Criteria_ID, int UserAnswer,string TextAnswer, string Remarks, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewEvaluation_ID",CrewEvaluation_ID),
        //                                    new SqlParameter("@Criteria_ID",Criteria_ID),
        //                                    new SqlParameter("@UserAnswer",UserAnswer),
        //                                    new SqlParameter("@TextAnswer",TextAnswer),
        //                                    new SqlParameter("@Remarks",Remarks),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Crew_Evaluation_Answer", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        
        //public static DataTable Get_CrewEvaluation_Details_DL(int CrewID, int CrewDtl_Evaluation_ID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewID",CrewID),
        //                                    new SqlParameter("@CrewDtl_Evaluation_ID",CrewDtl_Evaluation_ID)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_CrewEvaluation_Details", sqlprm).Tables[0];

        //}

        //public static int UPDATE_EvaluationRules_DL(int Evaluation_ID, int RankID, int RuleID, int Active_Status, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@RankID",RankID),
        //                                    new SqlParameter("@RuleID",RuleID),
        //                                    new SqlParameter("@Active_Status",Active_Status),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_EvaluationRules", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public static DataTable Get_EvaluationRules_DL(int Evaluation_ID, int RankID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Evaluation_ID",Evaluation_ID),
        //                                    new SqlParameter("@RankID",RankID)
        //                                };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationRules", sqlprm).Tables[0];

        //}
        //#endregion

    }

    
}
