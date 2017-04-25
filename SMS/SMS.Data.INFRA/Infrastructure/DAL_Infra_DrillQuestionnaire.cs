using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SMS.Data.INFRA.Infrastructure
{
    public class DAL_Infra_DrillQuestionnaire
    {
        private static string connection = "";
        public DAL_Infra_DrillQuestionnaire(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_Infra_DrillQuestionnaire()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        #region Grading with option

        public static int INS_UPD_DEL_Grading_DL(int? ID, string Grade_Name, int? GradeType, int? Min, int? Max, int? Division, int UserID, string Mode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Grade_Name",Grade_Name),
                                            new SqlParameter("@Grade_Type",GradeType),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Division),
                                            new SqlParameter("@User_ID",UserID),
                                            new SqlParameter("@Mode",Mode),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_INS_DRILL_GRADING", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int INS_UPD_DEL_GradingOption_DL(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
            string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            SqlParameter[] sqlprm = new SqlParameter[]
                                            { 
                                                new SqlParameter("@Grade_ID",Grade_ID),
                                                new SqlParameter("@OptionText",OptionText),
                                                new SqlParameter("@OptionValue",OptionValue),                                            
                                                new SqlParameter("@User_ID",Created_By),
                                                new SqlParameter("@Mode","A"),
                                                new SqlParameter("return",SqlDbType.Int)
                                            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_INS_DRILL_GRADINGOPTIONS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_GradingList_DL()
        {
            string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_GET_DRILL_GRADING").Tables[0];
        }

        public static DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Grade_ID",Grade_ID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_GET_DRILL_GRADINGOPTIONS", sqlprm).Tables[0];
        }
        #endregion


        #region Question
        public static int INS_UPD_DEL_Question_DL(int? ID, string Question, int? Grading_Type, int UserID, string Mode)
        {
            string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@ID",ID),
                    new SqlParameter("@Question",Question),
                    new SqlParameter("@Grading_Type",Grading_Type),
                    new SqlParameter("@User_ID",UserID),
                    new SqlParameter("@Mode",Mode),
                    new SqlParameter("return",SqlDbType.Int)
                };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_INS_DRILL_QUESTION", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_QuestionList_DL()
        {
            string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_GET_DRILL_QUESTION").Tables[0];
        }

        #endregion


    }
}
