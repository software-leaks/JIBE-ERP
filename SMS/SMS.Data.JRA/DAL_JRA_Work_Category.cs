using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Properties;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;




namespace SMS.Data.JRA
{
    public class DAL_JRA_Work_Category
    {
        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_JRA_Work_Category(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_JRA_Work_Category()
        {
            
        }
        public static int JRA_INS_WorkCategory(JRA_Lib objJRALibData)
        {
            int ReturnVal = 0;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Work_Categ_ID",objJRALibData.Work_Categ_ID),
                new SqlParameter("@Work_Categ_Value",objJRALibData.Work_Categ_Value),

                new SqlParameter("@Work_Category_Name",objJRALibData.Work_Category_Name),
                new SqlParameter("@Work_Categ_Parent_ID",objJRALibData.Work_Categ_Parent_ID),

                new SqlParameter("@UserID",objJRALibData.UserID),
                new SqlParameter("@DB_Mode",objJRALibData.DB_Mode),
                new SqlParameter("@ReturnVal",objJRALibData.ReturnVal)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INSUPD_WORK_CATEGORY_LIST", sqlprm);
            ReturnVal = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ReturnVal;
        }

        public static DataTable JRA_GET_WORK_CATEGORY_LIST(JRA_Lib objJRALibData)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@Work_Categ_Parent_ID",objJRALibData.Work_Categ_Parent_ID),
                 new SqlParameter("@Mode",objJRALibData.Mode)    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_WORK_CATEGORY_LIST",sqlprm).Tables[0];

        }

        public static int JRA_INS_TYPE(JRA_Lib objJRALibData)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Type_ID",objJRALibData.Type_ID),
                new SqlParameter("@Type",objJRALibData.Type),

                new SqlParameter("@Type_Value",objJRALibData.Type_Value),
                new SqlParameter("@Type_Display_Text",objJRALibData.Type_Display_Text),
                new SqlParameter("@Type_Description",objJRALibData.Type_Description),
                new SqlParameter("@Type_Color",objJRALibData.Type_Color),

                new SqlParameter("@UserID",objJRALibData.UserID),
                new SqlParameter("@DB_Mode",objJRALibData.DB_Mode)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INSUPD_TYPE_LIST", sqlprm);
        }

        public static DataTable JRA_GET_TYPE_LIST(JRA_Lib objJRALibData)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Type",objJRALibData.Type_ID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_TYPE", sqlprm).Tables[0];
        }

        public static DataTable JRA_GET_WORK_CATEGORY(JRA_Lib objJRALibData)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Type",objJRALibData.Type_ID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_TYPE", sqlprm).Tables[0];
        }

        public static DataTable JRA_SEARCH_WORK_CATEGORY(JRA_Lib objJRALibData)
        {
            SqlParameter[] sqlPrm = new SqlParameter[]
            {
                new SqlParameter("@SearchText",objJRALibData.SearchText),
                new SqlParameter("@SearchCate",objJRALibData.SearchCate),
                new SqlParameter("@PAGENUMBER",objJRALibData.PageNumber),
                new SqlParameter("@PAGESIZE",objJRALibData.PageSize)
                
            };
            DataTable dt= SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_WORK_CATEGORY_SEARCH",sqlPrm).Tables[0];

            return dt;
            
        }

        public static DataTable JRA_GET_TYPE_SEARCH(JRA_Lib JRALib_Data)
        {
            SqlParameter[] sqlPrm = new SqlParameter[]
            {
                new SqlParameter("@SearchText",JRALib_Data.SearchText),
                new SqlParameter("@SearchType",JRALib_Data.SearchType),
                new SqlParameter("@PAGENUMBER",JRALib_Data.PageNumber),
                new SqlParameter("@PAGESIZE",JRALib_Data.PageSize),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_TYPE_SEARCH",sqlPrm).Tables[0];
        }

        public static int JRA_INS_RatingType(JRA_Lib objJRALibData)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Rating_ID",objJRALibData.Rating_ID),
                new SqlParameter("@Risk_TYPE",objJRALibData.RiskType),
                new SqlParameter("@Rating_VALUE",objJRALibData.RatingValue),

                new SqlParameter("@UserID",objJRALibData.UserID),
                new SqlParameter("@DB_Mode",objJRALibData.DB_Mode)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INSUPD_RISK_RATING", sqlprm);
        }
        public static DataTable JRA_GET_RISK_TYPES(JRA_Lib JRALib_Data)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_RISK_TYPES").Tables[0];
        }

        public static DataTable JRA_GET_RATINGS_SEARCH(JRA_Lib JRALib_Data)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_RATINGS_SEARCH").Tables[0];
        }

    }
}
