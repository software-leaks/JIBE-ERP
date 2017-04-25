using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;


namespace SMS.Business.eForms
{
    public class BLL_TankerInspection
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        private static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        public static DataSet GET_TANK_REPORT_DETAILS(int Vessel_ID, int Main_Report_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_GET_TANK_REPORT_DETAILS", sqlprm);
        }
    }
}
