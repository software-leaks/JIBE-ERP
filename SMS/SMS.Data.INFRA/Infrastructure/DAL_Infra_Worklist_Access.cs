using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Worklist_Access
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Worklist_Access(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_Worklist_Access()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable SearchWorklistAccess(string Search, string ActionType
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Search", Search),
                   new System.Data.SqlClient.SqlParameter("@ActionType", ActionType),
                   
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_WorklistAccess_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataSet Get_WorkListAccessList(int AccessID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@AccessID", AccessID),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_WorklistAccess_List", obj);
            return ds;
        }


        //public DataTable Get_CurrencyList_DL()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CurrencyList").Tables[0];
        //}

        public int EditWorklistAccess(int? UserID, string ActionType, int AccessID, int Created_By, DataTable JobTypes)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@UserID",UserID),
                                          new SqlParameter("@ActionType",ActionType),
                                          new SqlParameter("@AccessID",AccessID),
                                          new SqlParameter("@Created_By",Created_By),
                                          new SqlParameter("@JobTypes",JobTypes)
                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Update_WorklistAccess", sqlprm));
        }

        public int InsertWorklistAccess(int? UserID, string ActionType, int Created_By, DataTable JobTypes)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@UserID",UserID),
                                          new SqlParameter("@ActionType",ActionType),
                                          new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@JobTypes",JobTypes)
                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Insert_WorklistAccess", sqlprm));
        }

        public int DeleteWorklistAccess(int AccessID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AccessID",AccessID),
                                            new SqlParameter("@Created_By",Created_By)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_WorklistAccess", sqlprm);
        }

        public DataTable SearchWorklistAccess(int? UserID, string ActionType
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@ActionType", ActionType),
                   
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_WorklistAccess_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataTable Get_WorkListAccessList(int AccessID,int Temp)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@AccessID", AccessID),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_WorklistAccess_List", obj);
            return ds.Tables[0];
        }


        //public DataTable Get_CurrencyList_DL()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CurrencyList").Tables[0];
        //}

        public int EditWorklistAccess(int? UserID, string ActionType, int AccessID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@UserID",UserID),

                                          new SqlParameter("@ActionType",ActionType),
                                          new SqlParameter("@AccessID",AccessID),
                                          new SqlParameter("@Created_By",Created_By)

                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Update_WorklistAccess", sqlprm));
        }

        public int InsertWorklistAccess(int? UserID, string ActionType, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@UserID",UserID),
                                          new SqlParameter("@ActionType",ActionType),
                                          new SqlParameter("@Created_By",Created_By)

                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Insert_WorklistAccess", sqlprm));
        }



    }
}
