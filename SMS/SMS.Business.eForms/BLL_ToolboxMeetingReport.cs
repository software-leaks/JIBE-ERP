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
    public class BLL_ToolboxMeetingReport
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        private static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataSet Get_ToolboxMeeting_Report(int? Main_Report_ID, int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_ToolboxMeeting_Report", sqlprm);
           // return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_ToolboxMeeting_Report_New", sqlprm);

        }
    }
}
