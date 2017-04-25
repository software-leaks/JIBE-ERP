using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
namespace SMS.Data.INFRA.Infrastructure
{

    public class DAL_Infra_ReadAccessRight
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";

        public DAL_Infra_ReadAccessRight(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_ReadAccessRight()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
       
        public DataTable FbmReadAccessRight_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_FBM_READ_ACCESS_RIGHT_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public int InsertFbmReadAccessRight_DL(string RANK_ID, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RANK_ID",RANK_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_FBM_Insert_READ_ACCESS_RIGHT", sqlprm);
        }

        public int EditFbmReadAccessRight_DL(int ID, string RANK_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@RANK_ID",RANK_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_FBM_Update_READ_ACCESS_RIGHT", sqlprm);
        }

        public int DeleteFbmReadAccessRight_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_FBM_Delete_READ_ACCESS_RIGHT", sqlprm);
        }
        public DataTable Get_FbmReadAccessRight_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_FBM_Get_READ_ACCESS_RIGHT_List", obj);

            return ds.Tables[0];
        }
    }
}


