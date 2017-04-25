using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.OCAAdmin
{
    public class DAL_QueryBuilder
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public static DataTable ExecuteQuery(string SQL, int UserID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }
        public static DataTable ExecuteQuery(string ConnectionString, string SQL, int UserID)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL).Tables[0];
        }
        public static DataTable ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm, int UserID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm).Tables[0];
        }
        public static DataTable ExecuteQuery(string ConnectionString, string SQLCommandText, SqlParameter[] sqlprm, int UserID)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQLCommandText, sqlprm).Tables[0];
        }

        public static DataSet ExecuteDataset(string SQLCommandText, SqlParameter[] sqlprm, int UserID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm);
        }


    /*! \brief 
     *  Function is used to add/modify objects for infobase library
     *
     *  \details 
     *  This function is used to add/modify table or SPs for infobase library
     *
     * \param[string] Command_Type  defines the object type Eg. SPs or  Tables
     *
     * \param[string] Query_Name  defines the sp or table name
     *
     * \param[string] display_Name  defines the name to be dispalyed
     *
     * \retval[int] Object Id
     */
        public static int SaveQuery(string Query_Name, string Command_Type, string Display_Name, string Command_SQL, string Key_Field, string ResultType, string DBServer, string DatabaseName, string DBUserName, string DBPassword, int UserID)
        {


            System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new SqlParameter("@Query_Name", Query_Name),
                   new SqlParameter("@Display_Name", Display_Name),
                   new SqlParameter("@Command_Type", Command_Type),

                   new SqlParameter("@Command_SQL", Command_SQL),
                   
                   new SqlParameter("@Key_Field", Key_Field),

                   new SqlParameter("@ResultType", ResultType),
                   new SqlParameter("@DBServer", DBServer),
                   new SqlParameter("@DatabaseName", DatabaseName),
                   new SqlParameter("@DBUserName", DBUserName),                    
                   new SqlParameter("@DBPassword",DBPassword), 
                   new SqlParameter("@UserID",UserID),
                   new SqlParameter("return",SqlDbType.Int)
            };


            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INFO_SP_Insert_Query", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        /*! \brief 
         *  Function is used to get details of  Saved Query 
         *
         *  \details 
         *  This function is used to  get details of  Saved Query 
         * \param[string] QueryName  defines SP or user defined query name
         *
         * \retval[Table] List
         */

        public  DataTable Get_SavedQuery(string QueryName,string Command_Type, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query_Name",QueryName),
                                            new SqlParameter("@Command_Type", Command_Type),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_Get_Saved_Proc", sqlprm).Tables[0];
        }

        /*! \brief 
         *  Function is used to get Saved Query  list
         *
         *  \details 
         *  This function is used to  get Saved Query  list for infobase
         *
         * \param[string] DBServer  Database server name
         *
         * \param[string] DatabaseName  defines Database name
         *
         * \param[string] DBUserName  defines the Username of database
         *
         * \param[string] QueryName  defines SP or user defined query name
         *
         * \retval[Table] List
         */

        public DataTable Get_SavedQuery(string DBServer, string DatabaseName, string DBUserName,  string Command_Type, string QueryName, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DBServer",DBServer),
                                            new SqlParameter("@DatabaseName",DatabaseName),
                                            new SqlParameter("@DBUserName",DBUserName),
                                            new SqlParameter("@Command_Type",Command_Type),
                                            new SqlParameter("@Query_Name",QueryName),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_Get_Saved_Proc", sqlprm).Tables[0];
        }

        public static DataTable Get_QueryDeatil(string QueryName)
        {
            SqlParameter sqlprm = new SqlParameter("@Query_Name", QueryName);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_Get_QueryDeatil", sqlprm).Tables[0];
        }

        public static DataTable Get_DatabaseProcedures()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Daemon_DBProcs").Tables[0];
        }


        public static DataTable Get_DatabaseProcedureSQL(string DBProcName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DBProcName",DBProcName)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Daemon_DBProc_SQL", sqlprm).Tables[0];
        }

        public  DataTable Get_AllTables()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_Get_AllTables").Tables[0];
        }

        public DataTable GET_Table_Columns(string TableName)
        {
            SqlParameter sqlprm = new SqlParameter("@Table_Name", TableName);

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_GET_Table_Columns", sqlprm).Tables[0];
        }





    }

}
