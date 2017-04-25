using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SMS.DPLService;
using System.Data.SqlClient;
using System.Configuration;


namespace DALJibe
{
    public class JiBeDAL
    {
        private static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataSet GetPortList()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortList");
        }
        public static DataSet Get_PiracyAreaList()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_PiracyArea");
        }
        public static DataSet Get_VesselCurrentLocation(int VesselID, int FleetID, string TelegramType, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Vessel_ID", VesselID), new SqlParameter("@Fleet_ID", FleetID), new SqlParameter("@Telegram_Type", TelegramType)};

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_TelegramData_Vessel", prm);
        }
        public static DataSet Get_FleetList(int UserCompanyID, int VesselManager)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@UserCompanyID", UserCompanyID), new SqlParameter("@VesselManager", VesselManager) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FleetList", prm);
        }

        public static DataSet Get_VesselList(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@FleetID", FleetID), new SqlParameter("@VesselID", VesselID), new SqlParameter("@Vessel_Manager", Vessel_Manager), new SqlParameter("@SearchText", SearchText), new SqlParameter("@UserCompanyID", UserCompanyID), new SqlParameter("@IsVessel", IsVessel) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", prm);
        }
        public static DataSet Get_UserVesselList(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel,int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@FleetID", FleetID), new SqlParameter("@VesselID", VesselID), new SqlParameter("@Vessel_Manager", Vessel_Manager), new SqlParameter("@SearchText", SearchText), new SqlParameter("@UserCompanyID", UserCompanyID), new SqlParameter("@IsVessel", IsVessel), new SqlParameter("@UserID", UserID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserVesselList", prm);
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
        public static DataTable Get_PortDetailsByID_DL(int PortID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@PortID", PortID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortDetails", sqlprm).Tables[0];
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


        public static DataTable Get_PortArrivalDepatureByVesselRoute(int Vessel_ID, int Fleet_ID, DateTime? dtFromDate, DateTime? dtToDate, int PortID, string TelegramType)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                //new SqlParameter("@Fleet_ID", Fleet_ID),
                new SqlParameter("@FromDT",dtFromDate),
                new SqlParameter("@ToDate",dtToDate),
                //new SqlParameter("@PortID",PortID),
                new SqlParameter("@Telegram_Type",TelegramType)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_PortArrivalDepature", sqlprm).Tables[0];
        }
        public static DataTable Get_LastPortCallDetails(int PortID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@PortID", PortID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_DPL_Get_LastPortCalls", sqlprm).Tables[0];
        }
    }
}