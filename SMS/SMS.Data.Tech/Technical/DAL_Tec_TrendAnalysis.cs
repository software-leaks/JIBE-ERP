using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;

namespace SMS.Data.Technical
{
    public class DAL_Tec_TrendAnalysis
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_Tec_TrendAnalysis(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Tec_TrendAnalysis()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public System.Data.DataTable Get_INSP_PSC_And_Defects_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@FILTERBY", FILTERBY),
                   new SqlParameter("@FLEETCODE", FLEETCODE),
                   new SqlParameter("@FROMDATE", FROMDATE),
                   new SqlParameter("@TODATE", TODATE),
                   new SqlParameter("@USERCOMPANYID", UserCompanyID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_TREND_ANALYSIS_GET_PSC_DEFICENCIES", sqlprm).Tables[0];
        }

        public DataSet Get_INSP_Total_Inspections_Defects(int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@FLEETCODE",FLEETCODE) ,               
                new SqlParameter("@FROMDATE",FROMDATE) ,               
                new SqlParameter("@TODATE",TODATE),
                new SqlParameter("@USERCOMPANYID", UserCompanyID)               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_TREND_ANALYSIS_GET_INSPECTIONS_DEFECTS", sqlprm);
        }

        public DataTable Get_INSP_NCR_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@FILTERBY", FILTERBY),
                   new SqlParameter("@FLEETCODE", FLEETCODE),
                   new SqlParameter("@FROMDATE", FROMDATE),
                   new SqlParameter("@TODATE", TODATE),
                   new SqlParameter("@USERCOMPANYID", UserCompanyID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_TREND_ANALYSIS_GET_NCR_COUNT", sqlprm).Tables[0];
        }

        public DataTable Get_INSP_NearMiss_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@FILTERBY", FILTERBY),
                   new SqlParameter("@FLEETCODE", FLEETCODE),
                   new SqlParameter("@FROMDATE", FROMDATE),
                   new SqlParameter("@TODATE", TODATE),
                    new SqlParameter("@USERCOMPANYID", UserCompanyID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_TREND_ANALYSIS_GET_NEAR_MISS_COUNT", sqlprm).Tables[0];
        }

        public DataTable Get_INSP_Injuries_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@FILTERBY", FILTERBY),
                   new SqlParameter("@FLEETCODE", FLEETCODE),
                   new SqlParameter("@FROMDATE", FROMDATE),
                   new SqlParameter("@TODATE", TODATE),
                    new SqlParameter("@USERCOMPANYID", UserCompanyID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_TREND_ANALYSIS_GET_DEATH_INJURY_COUNT", sqlprm).Tables[0];
        }

        public DataTable Get_INSP_PropertyPollution_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@FILTERBY", FILTERBY),
                   new SqlParameter("@FLEETCODE", FLEETCODE),
                   new SqlParameter("@FROMDATE", FROMDATE),
                   new SqlParameter("@TODATE", TODATE),
                    new SqlParameter("@USERCOMPANYID", UserCompanyID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_TREND_ANALYSIS_GET_POLLUTION_PROPERTY_COUNT", sqlprm).Tables[0];
        }
    }
}
