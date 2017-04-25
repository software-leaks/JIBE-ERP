using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;


namespace SMS.Data.Operation
{
    public class DAL_OPS_DPL
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_OPS_DPL(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_OPS_DPL()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_Ports_NearVessel_DL(string ship_long, string ship_lat, string longdire, string latdire)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ship_long", ship_long),
                new SqlParameter("@ship_lat", ship_lat),
                new SqlParameter("@longdire", longdire),
                new SqlParameter("@latdire", latdire) 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_Ports_NearVessel", sqlprm).Tables[0];
        }

        public static DataTable Get_TelegramData_DL(int Vessel_ID, int Fleet_ID, string Telegram_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Fleet_ID", Fleet_ID),
                new SqlParameter("@Telegram_Type",Telegram_Type)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_TelegramData",sqlprm).Tables[0];
        }
        public static DataTable Get_TelegramData_Route_DL(int Vessel_ID, int Fleet_ID, DateTime? FromDT, DateTime? ToDt, string Telegram_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Fleet_ID", Fleet_ID),
                new SqlParameter("@FromDT",FromDT),
                new SqlParameter("@ToDate",ToDt),
                new SqlParameter("@Telegram_Type",Telegram_Type)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_TelegramData_Route", sqlprm).Tables[0];
        }

        public static DataTable Get_PiracyArea_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_PiracyArea").Tables[0];
        }

        public static DataTable Get_Piracy_Alarms_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Piracy_Alarms").Tables[0];
        }

        public static DataTable Get_Piracy_Alarm_Change_Log_DL(int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Piracy_Alarm_Change_Log", sqlprm).Tables[0];
        }


        public static int Toggle_Piracy_Alarm_Status_DL(int Vessel_ID, int UserID, string Remarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Toggle_Piracy_Alarm_Status", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }

}
