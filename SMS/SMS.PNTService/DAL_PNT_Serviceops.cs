 

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using SMS.Data;
namespace SMS.PNTService
{
    public class DAL_PNT_Serviceops
    {    
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_PNT_Serviceops(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_PNT_Serviceops()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public static DataSet Get_PntInfo(DataTable OPS_PNT_MAX_UDT, int Max_DDL_ID)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {   
                new SqlParameter("@OPS_PNT_MAX_UDT", OPS_PNT_MAX_UDT),  
                new SqlParameter("@Max_DDL_ID", Max_DDL_ID),  
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_PNT_INFO", parm);
            return ds;
        }

        public static void INS_PNTRPTINFO(DataTable OPS_PRE_ARRIVAL_INFO_UDT, DataTable OPS_DTL_PRE_ARRIVAL_UDT,
                                     DataTable OPS_DTL_Port_Incidents_RPT_UDT, DataTable OPS_PRE_ARRIVAL_ATTACHMENTS_UDT )
        {
            SqlParameter[] parm = new SqlParameter[] 
            {   
       
                new SqlParameter("@OPS_PRE_ARRIVAL_INFO_UDT", OPS_PRE_ARRIVAL_INFO_UDT),  
                new SqlParameter("@OPS_DTL_PRE_ARRIVAL_UDT", OPS_DTL_PRE_ARRIVAL_UDT),  
                new SqlParameter("@OPS_DTL_Port_Incidents_RPT_UDT", OPS_DTL_Port_Incidents_RPT_UDT),  
                new SqlParameter("@OPS_PRE_ARRIVAL_ATTACHMENTS_UDT", OPS_PRE_ARRIVAL_ATTACHMENTS_UDT),  
               
            };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JCS_INS_PNTRPTINFO", parm);

        }


        public static DataSet Get_PreArrivalInfoDetail_DL(int Id,int Vessel_Id)
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

        public static DataSet Get_PORT_PORT_PreArrivalAttachments(int PreArrivalID, int Vessel_Id, int Office_Id,int? Mode=null)
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
