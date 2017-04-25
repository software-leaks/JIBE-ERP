using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.INFRA.Infrastructure
{
    public class DAL_PMS_Access
    {
        private string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        /// <summary>
        /// Search Saved access rights
        /// </summary>
        /// <param name="Search">search by entered value</param>
        /// <param name="ActionType">Action type : VERIFY/DELETE</param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber">selected page</param>
        /// <param name="pagesize"> selected page size.</param>
        /// <param name="isfetchcount">no of records found.</param>
        /// <returns>list of details.</returns>
        public DataTable SearchPMSAccess(string Search, string ActionType, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_PMSAccess_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        /// <summary>
        /// get details of rank against access id
        /// </summary>
        /// <param name="AccessID">Primary key</param>
        /// <returns>list of details.</returns>
        public DataSet Get_PMSAccessList(int AccessID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@AccessID", AccessID),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_PMSAccess_List", obj);
            return ds;
        }

        /// <summary>
        /// Add , update and delete access rights
        /// </summary>
        /// <param name="RankID">id of selected rank </param>
        /// <param name="PMS_Access_ID">primarky key incase of update and delete.|| for new record is null</param>
        /// <param name="ActionType">Action type : VERIFY/DELETE</param>
        /// <param name="userID">Logged in user id</param>
        /// <param name="mode">Mode: A:Add new record.U: update the record. D:delete</param>
        /// <returns>no .of rows affected.</returns>
        public int Insert_Update_Delete_PMS_Access(int? RankID, int? PMS_Access_ID, string ActionType, int userID, char mode)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@PMS_Access_ID",PMS_Access_ID),
                                            new SqlParameter("@ActionType",ActionType),
                                            new SqlParameter("@UserID",userID),
                                            new SqlParameter("@Mode",mode)
                                        };

                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Insert_Update_Delete_PMS_Access", sqlprm);
            }
            catch
            {
                throw;
            }
        }
    }
}
