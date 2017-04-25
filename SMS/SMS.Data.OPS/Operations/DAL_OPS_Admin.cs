using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;

namespace SMS.Data.Operations
{
   
    public class DAL_OPS_Admin
    {

        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_OPS_Admin()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable LubeOilCategory_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Lube_Oil_Category_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_LubeOilCategory_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Lube_Oil_Category_List", obj);

            return ds.Tables[0];
        }

        public int InsertLubeOilCategory_DL(string Category, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Insert_Lube_Oil_Category", sqlprm);
        }

        public int EditLubeOilCategory_DL(int ID, string Category, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Update_Lube_Oil_Category", sqlprm);
        }

        public int DeleteLubeOilCategory_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Delete_Lube_Oil_Category", sqlprm);
        }






        public DataTable Hold_Tank_Search(string searchtext,int? Structure_Type,int? Vessel_ID,  string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Structure_Type", Structure_Type),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_PR_DeckLog_HOLD_TANK_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_Hold_Tank_List_DL(int? ID, int Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   new SqlParameter("@Vessel_ID",Vessel_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_PR_DeckLog_HOLD_TANK_List", obj);

            return ds.Tables[0];
        }

        public int Insert_Hold_Tank_DL(string Hold_Tank_Name, int? Structure_Type, int? Vessel_ID, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Hold_Tank_Name",Hold_Tank_Name),
                                            new SqlParameter("@Structure_Type",Structure_Type),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Insert_HOLD_TANK", sqlprm);
        }


        public int Edit_Hold_Tank_DL(int ID,string Hold_Tank_Name,int? Structure_Type, int? Vessel_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Hold_Tank_Name",Hold_Tank_Name),
                                            new SqlParameter("@Structure_Type",Structure_Type),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Update_HOLD_TANK", sqlprm);
        }

        public int Delete_Hold_Tank_DL(int ID, int Vessel_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Delete_HOLD_TANK", sqlprm);
        }
        public DataTable Get_CategoryList_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PTR_SP_Get_RatingCategoryList", sqlprm).Tables[0];
        }
        public int INSERT_Category_DL(int? Category_ID, string Category_Name, int Category_Order_By, int? GradeType_Id, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category_ID",Category_ID),
                                            new SqlParameter("@Category_Name",Category_Name),
                                            new SqlParameter("@Category_Order_By",Category_Order_By),
                                            new SqlParameter("@GradeType_Id",GradeType_Id),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Insert_RATING_CATERGORY", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_Category_DL(int ID, string Category_Name, int Category_Order_By, int GradeType_Id, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category_Name",Category_Name),
                                             new SqlParameter("@Category_Order_By",Category_Order_By),
                                                new SqlParameter("@GradeType_Id",GradeType_Id),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Update_Rating_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Category_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Delete_RATING_CATERGORY", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_GradingList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PTR_SP_Get_RatingGradingList").Tables[0];
        }
        public int INSERT_Grading_DL(string Rating_Name, int Grade_Type, int Min, int Max, int Divisions, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Rating_Name",Rating_Name),
                                            new SqlParameter("@Grade_Type",Grade_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Insert_RatingGrade", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_Grading_DL(int ID, string Rating_Name, int Grading_Type, int Min, int Max, int Divisions, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Rating_Name",Rating_Name),
                                            new SqlParameter("@Grade_Type",Grading_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Update_RatingGrade", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Grading_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Delete_RatingGrade", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PTR_SP_Get_GradingOptions", sqlprm).Tables[0];
        }
        public int INSERT_GradingOption_DL(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
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
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Insert_RatingGradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_CriteriaList_DL(int Category_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category_ID",Category_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PTR_SP_Get_RatingCriteriaList", sqlprm).Tables[0];
        }
        public int INS_Rank_Voyage_Report(int MstRankID,int CERankID, string Mode, int UserID)
        {
            SqlParameter[] SqlParam = new SqlParameter[]
            {  
                new SqlParameter("@MstRankID",MstRankID),
                new SqlParameter("@CERankID",CERankID),
                new SqlParameter("@Mode",Mode),
                new SqlParameter("@User_ID",UserID)
   	        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_INS_UPD_RANK_CONFIG", SqlParam);
        }
        public DataTable Get_Rank_Voyage_Config()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_LIB_RANK_CONFIG", null).Tables[0];
        }
        //public int INSERT_Criteria_DL(int Category_ID, string Criteria_Name, int Criteria_Order_By, int GradeType_Id, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Category_ID",Category_ID),
        //                                    new SqlParameter("@Criteria_Name",Criteria_Name),
        //                                    new SqlParameter("@Criteria_Order_By",Criteria_Order_By),
        //                                    new SqlParameter("@GradeType_Id",GradeType_Id),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PTR_SP_Insert_RATING_CRITERIA", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

    }
}
