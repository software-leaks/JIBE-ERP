using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.Operation
{
    public class DAL_OPS_PortReport
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_OPS_PortReport(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_OPS_PortReport()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public static DataSet Get_PreArrivalPortInfo_DL(int Vessel_Id)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {   
                new SqlParameter("@Vessel_Id", Vessel_Id),  
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PRE_ARRIVAL_INFO", parm);
            return ds;
        }
        public static DataSet Get_PreArrivalInfoDetail_DL(int Id, int Vessel_Id)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {   
                new SqlParameter("@Id", Id), 
                new SqlParameter("@Vessel_Id", Vessel_Id),  
            };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PRE_ARRIVAL_INFO_DTL", parm);
            return ds;
        }

        #region GETPreArrival
        public static DataSet Get_PORT_PreArrival_Info(int ReportID, int Vessel_Id, int Office_Id)
        {
            SqlParameter[] prm = new SqlParameter[] 
            { new SqlParameter("@ReportID", ReportID), 
              new SqlParameter("@Vessel_Id", Vessel_Id) ,
              new SqlParameter("@Office_Id",Office_Id), 
            
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PORT_PreArrival_Info", prm);
        }

        public static DataSet Get_PORT_PORT_PreArrivalAttachments(int PreArrivalID, int Vessel_Id, int Office_Id, int? Mode = null)
        {
            SqlParameter[] prm = new SqlParameter[] 
            { new SqlParameter("@PreArrivalID", PreArrivalID), 
              new SqlParameter("@Vessel_Id", Vessel_Id) ,
              new SqlParameter("@Office_Id",Office_Id),
                new SqlParameter("@Mode",Mode),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PORT_PreArrivalAttachments", prm);
        }

        public static DataSet GET_PORT_Incidents(int PreArrivalID, int Vessel_Id, int Office_Id)
        {
            SqlParameter[] prm = new SqlParameter[] 
            { new SqlParameter("@PreArrivalID", PreArrivalID), 
              new SqlParameter("@Vessel_Id", Vessel_Id) ,
              new SqlParameter("@Office_Id",Office_Id),
              
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PORT_Incidents", prm);
        }
        #endregion
    }
}
