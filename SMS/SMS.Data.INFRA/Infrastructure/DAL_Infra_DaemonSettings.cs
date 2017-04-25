using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_DaemonSettings
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataSet Get_Current_Schedules_DL(int Frequency_Type, int Last_Run_Success, int Status, string SearchText, int UserID, int ShowDept, int DeptId, int IsAdmin, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
    new SqlParameter("@Frequency_Type",Frequency_Type),
    new SqlParameter("@Last_Run_Success",Last_Run_Success),
    new SqlParameter("@Status",Status),
    new SqlParameter("@SearchText",SearchText),
    new SqlParameter("@UserID",UserID),    
    new SqlParameter("@ShowDept",ShowDept),  
    new SqlParameter("@DeptId",DeptId),  
    new SqlParameter("@IsAdmin",IsAdmin),  
    new SqlParameter("@PAGE_INDEX",Page_Index),
    new SqlParameter("@PAGE_SIZE",Page_Size),
    new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_DaemonSettings", sqlprm);
            is_Fetch_Count = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
            return dt;


        }

        public static int Save_Schedule_DL(DataTable dtSchedule, DataTable dtSettings, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@TBL_DAEMON_SCHEDULE",dtSchedule),
                new SqlParameter("@TBL_DAEMON_SETTINGS",dtSettings),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[1].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Save_DaemonSchedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataSet Get_Schedule_Details_DL(int ScheduleID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_DaemonSchedule", sqlprm);
        }
        public static int Pause_Schedule_DL(int ScheduleID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Pause_Daemon_Schedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Toggle_Schedule_DL(int ScheduleID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Toggle_Status_Daemon_Schedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Run_Schedule_DL(int ScheduleID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Run_Daemon_Schedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Execure_Schedule_Now_DL(int ScheduleID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@SCHEDULE_ID",ScheduleID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Manual_Execute",1),                
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Execute_Daemon_Procedure", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Execute_Daemon_Process_DL()
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Run_Daemon_Process", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
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
        public static int SaveQuery(string Query_Name, string Command_Type, string Command_SQL, string ResultType, string DBServer, string DatabaseName, string DBUserName, string DBPassword, int UserID)
        {

            System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Query_Name", Query_Name),
                   new System.Data.SqlClient.SqlParameter("@Command_Type", Command_Type),
                   new System.Data.SqlClient.SqlParameter("@Command_SQL", Command_SQL),
                   new System.Data.SqlClient.SqlParameter("@ResultType", ResultType),
                   new System.Data.SqlClient.SqlParameter("@DBServer", DBServer),
                   new System.Data.SqlClient.SqlParameter("@DatabaseName", DatabaseName),
                   new System.Data.SqlClient.SqlParameter("@DBUserName", DBUserName),                    
                   new System.Data.SqlClient.SqlParameter("@DBPassword",DBPassword), 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                   new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Insert_Daemon_Query", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public static DataTable Get_SavedQuery(int QueryID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@QueryID",QueryID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Daemon_Queries", sqlprm).Tables[0];
        }
        public static DataTable Get_SavedQuery(string DBServer, string DatabaseName, string DBUserName, int QueryID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DBServer",DBServer),
                                            new SqlParameter("@DatabaseName",DatabaseName),
                                            new SqlParameter("@DBUserName",DBUserName),
                                            new SqlParameter("@QueryID",QueryID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Daemon_Queries", sqlprm).Tables[0];
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


        public static DataTable Generate_Report(int ScheduleID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_DAEMON_PROC_CUSTOM_WORKLIST", sqlprm).Tables[0];
        }
        public static DataTable GetSelectedSuppliers(int ScheduleID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PURC_GET_Daemon_SupplierList", sqlprm).Tables[0];
        }
        public static int UpdateSupplierList(DataTable dtSuppliers, int ScheduleID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@SupplierCode",dtSuppliers),
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@UserID",UserID),
               
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PURC_INSERT_Daemon_SupplierList", sqlprm);
            
        }
        public static DataTable Generate_PO_Report(int ScheduleID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID",ScheduleID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_DAEMON_PROC_PO_REPORT_PURC", sqlprm).Tables[0];
        }
    }
}
