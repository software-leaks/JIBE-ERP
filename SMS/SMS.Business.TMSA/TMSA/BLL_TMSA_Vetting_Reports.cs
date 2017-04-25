using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.TMSA;

namespace SMS.Business.TMSA
{
    public class BLL_TMSA_Vetting_Reports
    {
        DAL_TMSA_Vetting_Reports objDAL = new DAL_TMSA_Vetting_Reports();

        /// <summary>
        /// Description: Method to fetch Observations By Vessel Count
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetObservationsByVesselCnt(string Vessel_Ids, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetObservationsByVesselCnt(Vessel_Ids, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }
        /// <summary>
        /// Description: Method to fetch Observations By Vessel Count
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetObservationsByFleetCnt(string Vessel_Ids, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetObservationsByFleetCnt(Vessel_Ids, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }
        /// <summary>
        /// Description: Method to fetch  Vessel Observations Count by category
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetVesselObservationCntByCategory(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetVesselObservationCntByCategory(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }

        /// <summary>
        /// Description: Method to fetch  Fleet Observations Count by category
        /// Created By: Krishnapriya
        /// Created On: 03-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetFleetObservationCntByCategory(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetFleetObservationCntByCategory(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID,fleetID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 04/03/2017
        /// Decription : To bind the grid of Oil Major Names
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>

        public DataSet GetVesselwiseOilMajors(String Years, String Vessel_IDs)
        {
            return objDAL.GetVesselwiseOilMajors(Years, Vessel_IDs);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 06/03/2017
        /// Decription :  To load the pie  Chart for Oil Major Names for Vessel 
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameCount(String Years, String Vessel_IDs)
        {
            return objDAL.GetOilMajorNameCount(Years, Vessel_IDs);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To load the jqx grid with OilMajorNames and Rec_count with Yearwise
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorCountYearwise(String Years, String Vessel_IDs)
        {
            return objDAL.GetOilMajorCountYearwise(Years,Vessel_IDs);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To load the jqx grid with OilMajorNames and Rec_count with Yearwise
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameColumnChart(String Years, String Vessel_IDs)
        {
           return objDAL.GetOilMajorNameColumnChart(Years,Vessel_IDs);
        }
        /// <summary>
        /// Description: Method to fetch  Vessel Observations Count year wise
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetVesselObservationsCntYearWise(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetVesselObservationsCntYearWise(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }
        
        /// <summary>
        /// Description: Method to fetch  Fleet Observations Count year wise
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetFleetObservationCntYearWise(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetFleetObservationCntYearWise(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }

        /// <summary>
        /// Description: Method to fetch observation by Category count
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetObservationByCategoryCount(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetObservationByCategoryCount(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }

        /// <summary>
        /// Description: Method to fetch observation by Category count to bind pie chart
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetObservationByCategoryCountForChart(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetObservationByCategoryCount(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }

        /// <summary>
        /// Description: Method to fetch observation by Category count for multiple years
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years </param>
        public DataSet GetMultipleYearCategoryCount(string VesselIDs, string Years, string vettingTypeID, string categoryID, string observationTypeID, string fleetID)
        {
            return objDAL.GetMultipleYearCategoryCount(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID);
        }

        ///<summary>
        /// Created By : Harshal
        /// Created On : 04/03/2017
        /// Decription : To bind the grid of Oil Major Names
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>

        public DataSet GetVesselwiseOilMajors(String Years,String Vessel_IDs,String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID,String OilMajorID)
        {
            return objDAL.GetVesselwiseOilMajors(Years, Vessel_IDs,CategoryID,VettingTypeID,ObservationTypeID,FleetID,OilMajorID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 06/03/2017
        /// Decription :  To load the pie  Chart for Oil Major Names for Vessel 
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameCount(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID,String OilMajorID)
        {
            return objDAL.GetOilMajorNameCount(Years, Vessel_IDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID,OilMajorID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To load the jqx grid with OilMajorNames and Rec_count with Yearwise
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorCountYearwise(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID, String OilMajorID)
        {
            return objDAL.GetOilMajorCountYearwise(Years, Vessel_IDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID,OilMajorID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 09/03/2017
        /// Description : To load the jqx column chart with OilMajorNames and Rec_count with Yearwise
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <returns></returns>
        public DataSet GetOilMajorNameColumnChart(String Years, String Vessel_IDs, String CategoryID, String VettingTypeID, String ObservationTypeID, String FleetID)
        {
            return objDAL.GetOilMajorNameColumnChart(Years, Vessel_IDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 14-03-2017
        /// Description : To load the jqx grid1 with Risk Level Observation and Rec_count 
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <param name="Risk_Level"></param>
        /// <param name="FleetID"></param>
        /// <returns></returns>
        public DataSet GetRiskLevelObservation(String Years, String Vessel_IDs,String FleetID,String Risk_Level,String VettingTypeID, String ObservationTypeID, String CategoryID)
        {
            return objDAL.GetRiskLevelObservation(Years, Vessel_IDs, FleetID, Risk_Level,VettingTypeID,ObservationTypeID,CategoryID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Decription :  To load the pie  Chart for Risk Level Observation
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <param name="Risk_Level"></param>
        /// <param name="FleetID"></param>
        /// <returns></returns>
        public DataSet GetRiskLevelObservationPieChart(String Years, String Vessel_IDs, String FleetID, String Risk_Level,String VettingTypeID, String ObservationTypeID, String CategoryID)
        {
            return objDAL.GetRiskLevelObservation(Years, Vessel_IDs, FleetID, Risk_Level,VettingTypeID,ObservationTypeID,CategoryID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Decription :  To load Risk Level Observation Yearwise to jqx grid2
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <param name="Risk_Level"></param>
        /// <param name="FleetID"></param>
        /// <returns></returns>
        public DataSet GetRiskLevelObservationgrid(String Years, String Vessel_IDs, String FleetID, String Risk_Level, String VettingTypeID, String ObservationTypeID, String CategoryID)
        {
            return objDAL.GetRiskLevelObservationgrid(Years, Vessel_IDs, FleetID, Risk_Level, VettingTypeID,ObservationTypeID,CategoryID);
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Decription :  To load Fleet Observation with Risk Level count Yearwise to jqx grid3
        /// </summary>
        /// <param name="Years"></param>
        /// <param name="Vessel_IDs"></param>
        /// <param name="FleetID"></param>
        /// <param name="Risk_Level"></param>
        /// <returns></returns>
        public DataSet GetFleetObservationRiskLevelCntYearwise(String Years, String Vessel_IDs, String FleetID, String Risk_Level)
        {
            return objDAL.GetFleetObservationRiskCntYearwise(Years, Vessel_IDs, FleetID, Risk_Level);
        }
    }


    }

