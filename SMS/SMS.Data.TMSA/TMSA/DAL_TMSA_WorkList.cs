using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace SMS.Data.TMSA
{
    public class DAL_TMSA_WorkList
    {
        SqlConnection conn;
        private static string connection = "";

        public DAL_TMSA_WorkList()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        /// <summary>
        /// Description: Method to fetch yearwise worklist count for a jobtype
        /// Created By: Bhairab
        /// Created On: 06/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years for searching worklist</param>
        /// <param name="Job_Type"> Type of job</param>
        public DataSet GetYearWiseWorklistCount(string Vessel_Ids, string Years, string Job_Type)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_IDs",Vessel_Ids),
                                            new SqlParameter("@Years",Years),
                                            new SqlParameter("@Type",Job_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetYearWiseCount", sqlprm);

        }


        /// <summary>
        /// Description: Method to fetch mmonthly vesselwise count for a jobtype
        /// Created By: Bhairab
        /// Created On: 06/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>
        public DataSet GetMonthlyWorklistCountByVessel(string Vessel_Ids, int Year, string Job_Type)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_IDs",Vessel_Ids),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@Type",Job_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetMonthlyNCRCountByVessel", sqlprm);

        }

        /// <summary>
        /// Description: Method to fetch near misses count
        /// Created By: Krishnapriya
        /// Created On: 19/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetVesselCountNearMisses(string Vessel_Ids, string Year, string Job_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Years", Year),
                new SqlParameter("@Type", Job_Type)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetYearWiseNearMissCount", sqlprm);
        }

        /// <summary>
        /// Description: Method to fetch incident count
        /// Created By: Krishnapriya
        /// Created On: 21/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetIncidentCount(string Vessel_Ids, string Year, string Job_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Year", Year),
                new SqlParameter("@Type", Job_Type),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetYearWiseIncidentCount", sqlprm);
        }


        /// <summary>
        /// Description: Method to fetch incident count for all vessel per year
        /// Created By: Krishnapriya
        /// Created On: 21/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetMultipleVesselIncidentCount(string Vessel_Ids, string Years, string Job_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Years", Years),
                new SqlParameter("@Type", Job_Type)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetMultipleVesselIncidentCount", sqlprm);
        }

        
        /// <summary>
        /// Description: Method to fetch incident count for all categories per year
        /// Created By: Krishnapriya
        /// Created On: 22/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetCategoryIncidentCount(string Vessel_Ids, string Years, string Job_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Years", Years),
                new SqlParameter("@Type", Job_Type)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetMultipleYearIncidentCount", sqlprm);
        }


        
        /// <summary>
        /// Description: Method to fetch Injury/Death incident count
        /// Created By: Krishnapriya
        /// Created On: 23/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>
        /// <param name="SubType"> Sub Type of job</param>

        public DataSet GetInjuryIncidentCount(string Vessel_Ids, string Year, string Job_Type, string SubType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Year", Year),
                new SqlParameter("@Type", Job_Type),
                new SqlParameter("@SubType", SubType)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetYearWiseInjuryCount", sqlprm);
        }


        /// <summary>
        /// Description: Method to fetch Injury/Death incident count for all categories per year
        /// Created By: Krishnapriya
        /// Created On: 22/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>
        /// <param name="SubType"> Sub Type of job</param>

        public DataSet GetCategoryInjuryIncidentCount(string Vessel_Ids, string Years, string Job_Type, string SubType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Years", Years),
                new SqlParameter("@Type", Job_Type),
                new SqlParameter("@SubType", SubType)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetMultipleYearInjuryCount", sqlprm);
        }


        
        /// <summary>
        /// Description: Method to fetch incident count for all vessel per year
        /// Created By: Krishnapriya
        /// Created On: 21/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected year</param>
        /// <param name="Job_Type"> Type of job</param>
        /// <param name="SubType"> Sub Type of job</param>

        public DataSet GetMultipleVesselInjuryIncidentCount(string Vessel_Ids, string Years, string Job_Type, string SubType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs", Vessel_Ids),
                new SqlParameter("@Years", Years),
                new SqlParameter("@Type", Job_Type),
                new SqlParameter("@SubType", SubType)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Report_GetMultipleVesselIncidentCount", sqlprm);
        }
    }
}
