using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;
using System.Data.SqlClient;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_DaemonSettings
    {
        public static DataSet Get_Current_Schedules(int Frequency_Type, int Last_Run_Success, int Status, string SearchText, int UserID, int ShowDept, int DeptId, int IsAdmin, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_Infra_DaemonSettings.Get_Current_Schedules_DL(Frequency_Type, Last_Run_Success, Status, SearchText, UserID, ShowDept, DeptId, IsAdmin, Page_Index, Page_Size, ref is_Fetch_Count);
        }
        public static int Save_Schedule(DataTable dtSchedule, DataTable dtSettings, int UserID)
        {
            return DAL_Infra_DaemonSettings.Save_Schedule_DL(dtSchedule, dtSettings, UserID);
        }
        public static DataSet Get_Schedule_Details(int ScheduleID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Get_Schedule_Details_DL(ScheduleID, UserID);
        }
        public static int Pause_Schedule(int ScheduleID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Pause_Schedule_DL(ScheduleID, UserID);
        }
        public static int Toggle_Schedule(int ScheduleID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Toggle_Schedule_DL(ScheduleID, UserID);
        }
        public static int Run_Schedule(int ScheduleID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Run_Schedule_DL(ScheduleID, UserID);
        }
        public static int Execure_Schedule_Now(int ScheduleID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Execure_Schedule_Now_DL(ScheduleID, UserID);
        }

        public static int Execute_Daemon_Process()
        {
            return DAL_Infra_DaemonSettings.Execute_Daemon_Process_DL();
        }

        public static DataTable ExecuteQuery(string SQL, int UserID)
        {
            return DAL_Infra_DaemonSettings.ExecuteQuery(SQL, UserID);
        }
        public static DataTable ExecuteQuery(string ConnectionString, string SQL, int UserID)
        {
            return DAL_Infra_DaemonSettings.ExecuteQuery(ConnectionString, SQL, UserID);
        }

        public static DataTable ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm, int UserID)
        {
            return DAL_Infra_DaemonSettings.ExecuteQuery(SQLCommandText, sqlprm, UserID);
        }
        public static DataTable ExecuteQuery(string ConnectionString, string SQLCommandText, SqlParameter[] sqlprm, int UserID)
        {
            return DAL_Infra_DaemonSettings.ExecuteQuery(ConnectionString, SQLCommandText, sqlprm, UserID);
        }

        public static DataSet ExecuteDataset(string SQLCommandText, SqlParameter[] sqlprm, int UserID)
        {
            return DAL_Infra_DaemonSettings.ExecuteDataset(SQLCommandText, sqlprm, UserID);
        }
        public static int SaveQuery(string Query_Name, string Command_Type, string Command_SQL, string ResultType, string DBServer, string DatabaseName, string DBUserName, string DBPassword, int UserID)
        {
            return DAL_Infra_DaemonSettings.SaveQuery(Query_Name, Command_Type, Command_SQL, ResultType, DBServer, DatabaseName, DBUserName, DBPassword, UserID);
        }

        public static DataTable Get_SavedQuery(int QueryID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Get_SavedQuery(QueryID, UserID);
        }

        public static DataTable Get_SavedQuery(string DBServer, string DatabaseName, string DBUserName, int QueryID, int UserID)
        {
            return DAL_Infra_DaemonSettings.Get_SavedQuery(DBServer, DatabaseName, DBUserName, QueryID, UserID);
        }
        public static DataTable Get_DatabaseProcedures()
        {
            return DAL_Infra_DaemonSettings.Get_DatabaseProcedures();
        }
        public static DataTable Get_DatabaseProcedureSQL(string DBProcName)
        {
            return DAL_Infra_DaemonSettings.Get_DatabaseProcedureSQL(DBProcName);
        }

        public static DataTable Generate_Report(int ScheduleID)
        {
            return DAL_Infra_DaemonSettings.Generate_Report(ScheduleID);
        }
        public static DataTable GetSelectedSuppliers(int ScheduleID)
        {
            return DAL_Infra_DaemonSettings.GetSelectedSuppliers(ScheduleID);
        }
        public static int UpdateSupplierList(DataTable dtSuppliers,int ScheduleID, int UserID)
        {
            return DAL_Infra_DaemonSettings.UpdateSupplierList(dtSuppliers, ScheduleID,  UserID);
        }
        public static DataTable Generate_PO_Report(int ScheduleID)
        {
            return DAL_Infra_DaemonSettings.Generate_PO_Report(ScheduleID);
        }
    }
}
