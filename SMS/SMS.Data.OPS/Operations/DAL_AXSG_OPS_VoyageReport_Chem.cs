﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SMS.Data.OPS
{
    public class DAL_AXSG_OPS_VoyageReport_Chem
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public static DataTable Get_DailyVoyageReportIndex_Chem_DL(string ReportType, int VesselID, int LocationCode, DateTime Fromdate, DateTime Todate, int FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REPORTTYPE", ReportType), 
                new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@LOCATION", LocationCode),
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@SORT_COLUMN",Sort_Column),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@IS_FETCH_COUNT", Record_Count),

            };
            parm[parm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_DTL_Get_VoyageReport_Index_Chem", parm).Tables[0];
            Record_Count = Convert.ToInt32(parm[parm.Length - 1].Value);
            return dt;
        }
        public static DataTable Get_VoyageReportIndex_AXSG(string ReportType, int VesselID, int LocationCode, DateTime? Fromdate, DateTime? Todate, int FleetID)
        {



            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REPORTTYPE", ReportType), 
                new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@LOCATION", LocationCode),
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@FleetID",FleetID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_VoyageReportIndex_AXSG", parm).Tables[0];
        }

        public static DataSet Get_DailyNoonReport_DL_AXSG(int VesselID, decimal TelegramID)
        {
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@Telegram_ID", TelegramID),
                new SqlParameter("@Vessel_ID", VesselID) 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_DailyNoonReportAtPort_AXSG", parm);
        }
    }
}
