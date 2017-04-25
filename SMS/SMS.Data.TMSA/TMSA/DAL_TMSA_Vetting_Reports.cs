using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.TMSA
{
    public class DAL_TMSA_Vetting_Reports
    {
        private static string connection = "";

        public DAL_TMSA_Vetting_Reports()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        /// <summary>
        /// Description:  Method to fetch Vessel Observation Count
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years for searching worklist</param>
        public DataSet GetObservationsByVesselCnt(string Vessel_Ids, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs",Vessel_Ids),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetObservationsByVesselCnt", sqlprm);
        }

        /// <summary>
        /// Description:  Method to fetch Fleet Observation Count
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        public DataSet GetObservationsByFleetCnt(string Vessel_Ids, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs",Vessel_Ids),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetObservationsByFleetCnt", sqlprm);
        }


        /// <summary>
        /// Description:  Method to fetch Vessel Observation Count By Category
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years for searching worklist</param>
        public DataSet GetVesselObservationCntByCategory(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs",VesselIDs),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetVesselObservationsCntByCategory",sqlprm);
        }

        /// <summary>
        /// Description:  Method to fetch Fleet Observation Count By Category
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years for searching worklist</param>
        public DataSet GetFleetObservationCntByCategory(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs",VesselIDs),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetFleetObservationsCntByCategory", sqlprm);
        }


        /// <summary>
        /// Created By : Harshal
        /// Created on : 06/03/2016
        /// Description: Method to Fetch Oil Majors Names and Rec_Count to display on grid  
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>

        public DataSet GetVesselwiseOilMajors(String Years, String Vessel_IDs)
        {
            SqlParameter[] Sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Year",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_VettingReport_GetVeseelwise_OilMajor", Sqlprm);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created on : 06/03/2016
        /// Description : To Fetch Oil Major Names and Rec_Count to display on pie chart
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameCount(String Years, String Vessel_IDs)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Year",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_VettingReport_GetVeseelwise_OilMajor", sqlprm);

        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To Fetch OilMajorNames and Rec_Count Yearwise for grid
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorCountYearwise(String Years, String Vessel_IDs)
        {
            SqlParameter[]sqlprm=new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_GetOilMajorCount_Yearwise", sqlprm);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To Fetch OilMajorNames and Rec_Count Yearwise for Column Chart
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameColumnChart(String Years, String Vessel_IDs)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),            
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_GetOilMajorCount_Yearwise", sqlprm);
        }

        /// <summary>
        /// Description:  Method to fetch Vessel Observation Count Year wise
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        public DataSet GetVesselObservationsCntYearWise(string Vessel_Ids, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_IDs",Vessel_Ids),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetVesselObservationsCntYearWise", sqlprm);
        }

        /// <summary>
        /// Description:  Method to fetch Fleet Observation Count Year wise
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        public DataSet GetFleetObservationCntYearWise(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                 new SqlParameter("@Vessel_IDs",VesselIDs),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetFleetObservationsCntYearWise", sqlprm);
        }

        /// <summary>
        /// Description:   Method to fetch observation by Category count
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        public DataSet GetObservationByCategoryCount(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                 new SqlParameter("@Vessel_IDs",VesselIDs),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetObservationCategoryCount", sqlprm);
        }  
            
         /// <summary>
        /// Description:   Method to fetch observation by Category count for multiple years
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        public DataSet GetMultipleYearCategoryCount(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                 new SqlParameter("@Vessel_IDs",VesselIDs),
                new SqlParameter("@Years",Years),
                new SqlParameter("@VettingTypeID",vettingTypeID),
                new SqlParameter("@CategoryID",categoryID),
                new SqlParameter("@ObservationTypeID",observationTypeID),
                new SqlParameter("@fleetID",fleetID)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_VT_Report_GetMultipleYearCategoryCount", sqlprm);
        }


        /// <summary>
        /// Created By : Harshal
        /// Created on : 06/03/2016
        /// Description: Method to Fetch Oil Majors Names and Rec_Count to display on grid  
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>

        public DataSet GetVesselwiseOilMajors(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID,String OilMajorID)
        {
            SqlParameter[] Sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Year",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),
                new SqlParameter("@CategoryID",CategoryID),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@OilMajorID",OilMajorID),

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_VettingReport_GetVeseelwise_OilMajor", Sqlprm);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created on : 06/03/2016
        /// Description : To Fetch Oil Major Names and Rec_Count to display on pie chart
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameCount(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID, String OilMajorID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Year",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),
                new SqlParameter("@CategoryID",CategoryID),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@OilMajorID",OilMajorID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_VettingReport_GetVeseelwise_OilMajor", sqlprm);

        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To Fetch OilMajorNames and Rec_Count Yearwise for grid
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorCountYearwise(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID, String OilMajorID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),
                new SqlParameter("@CategoryID",CategoryID),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@OilMajorID",OilMajorID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_GetOilMajorCount_Yearwise", sqlprm);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To Fetch OilMajorNames and Rec_Count Yearwise for Column Chart
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameColumnChart(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs), 
                new SqlParameter("@CategoryID",CategoryID),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@FleetID",FleetID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_GetOilMajorCount_Yearwise", sqlprm);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 14/03/2017
        /// Description : To Fetch Risk Level Observation and Rec_Count for jqx grid1
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetRiskLevelObservation(String Years, String Vessel_IDs, String FleetID, String Risk_Level, String VettingTypeID, String ObservationTypeID, String CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),                
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@Risk_Level",Risk_Level),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@CategoryID",CategoryID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_Get_ObservationRiskLevel", sqlprm);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Description : To Fetch Risk Level Observation and Rec_Count for pie Chart
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <param name="Risk_Level"></param>
        /// <param name="FleetID"></param>
        /// <returns></returns>
        public DataSet GetRiskLevelObservationPieChart(String Years, String Vessel_IDs, String FleetID, String Risk_Level, String VettingTypeID, String ObservationTypeID, String CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),                
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@Risk_Level",Risk_Level),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@CategoryID",CategoryID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_Get_ObservationRiskLevel", sqlprm);
        }


        /// <summary>
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Description : To Fetch Risk Level Observation to jqx grid2 Yearwise
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <param name="CategoryID"></param>
        /// <param name="VettingTypeID"></param>
        /// <param name="ObservationTypeID"></param>
        /// <param name="FleetID"></param>
        /// <returns></returns>
        public DataSet GetRiskLevelObservationgrid(String Years, String Vessel_IDs, String FleetID, String Risk_Level,String VettingTypeID, String ObservationTypeID, String CategoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
               new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),                                                
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@Risk_Level",Risk_Level),
                new SqlParameter("@VettingTypeID",VettingTypeID),
                new SqlParameter("@ObservationTypeID",ObservationTypeID),
                new SqlParameter("@CategoryID",CategoryID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_Get_RiskObservationYearwise", sqlprm);
        }

        ///// <summary>
        ///// Created By : Harshal
        ///// Created On : 15/03/2017
        ///// Description : To Fetch Risk Level Observation to Column Chart1
        ///// </summary>
        ///// <param name="Years"></param>
        ///// <param name="Vessel_IDs"></param>
        ///// <param name="CategoryID"></param>
        ///// <param name="VettingTypeID"></param>
        ///// <param name="ObservationTypeID"></param>
        ///// <param name="FleetID"></param>
        ///// <returns></returns>
        //public DataSet GetRiskLevelObservationColumnChart(String Years, String Vessel_IDs, String FleetID, String Risk_Level)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //    {
        //       new SqlParameter("@Years",Years),
        //        new SqlParameter("@Vessel_IDs",Vessel_IDs),
        //        new SqlParameter("@FleetID",FleetID),                
        //        new SqlParameter("@Risk_Level",Risk_Level),                
        //    };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_Get_RiskObservationYearwise", sqlprm);
        //}

        /// <summary>
        /// Created By : Harshal
        /// Created on : 16/03/2017
        /// Decription :  To load Year Observation with Risk Level count Yearwise to jqx grid3
        /// </summary>
        /// <returns></returns>
        public DataSet GetFleetObservationRiskCntYearwise(String Years, String Vessel_IDs, String FleetID, String Risk_Level)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Years",Years),
                new SqlParameter("@Vessel_IDs",Vessel_IDs),                                                
                new SqlParameter("@FleetID",FleetID),
                new SqlParameter("@Risk_Level",Risk_Level),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_GetFleetObservationRiskLevelCntYearWise", sqlprm);
        }


    }
}
