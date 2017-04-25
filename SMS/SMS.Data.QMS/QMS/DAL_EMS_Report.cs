using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;

namespace SMS.Data.QMS
{
    public class DAL_EMS_Report
    {
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Get_Reports_Index_DL(int? FleetID , int? VesselID, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   
                new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
                new System.Data.SqlClient.SqlParameter("@dtFrom",dtFrom),
                new System.Data.SqlClient.SqlParameter("@dtTo",dtTo),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "EMS_GET_Reports_Index", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Reports_Details_DL(int VesselID, int ID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@VesselID",VesselID),
                new SqlParameter("@ID",ID),
                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "EMS_GET_Reports_Details", obj).Tables[0];
        }

    }


}
