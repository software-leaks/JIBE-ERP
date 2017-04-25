using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.TMSA;

namespace SMS.Business.TMSA
{
    public class BLL_TMSA_Worklist
    {
        DAL_TMSA_WorkList objDAL = new DAL_TMSA_WorkList();


        /// <summary>
        /// Description: Method to fetch yearwise worklist count for a jobtype
        /// Created By: Bhairab
        /// Created On: 06/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Years"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>
        public DataSet GetYearWiseWorklistCount(string Vessel_Ids, string Years, string Job_Type)
        {
            return objDAL.GetYearWiseWorklistCount(Vessel_Ids, Years, Job_Type);
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
            return objDAL.GetMonthlyWorklistCountByVessel(Vessel_Ids, Year, Job_Type);
        }

        /// <summary>
        /// Description: Method to fetch yearwise Near misses count for a vessels
        /// Created By: Krishnapriya
        /// Created On: 16/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetVesselCountNearMisses(string Vessel_Ids, string Year, string Job_Type)
        {
            return objDAL.GetVesselCountNearMisses(Vessel_Ids, Year, Job_Type);
        }

        
        /// <summary>
        /// Description: Method to fetch Incident count for vessels
        /// Created By: Krishnapriya
        /// Created On: 21/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetIncidentCount(string Vessel_Ids, string Year, string Job_Type)
        {
            return objDAL.GetIncidentCount(Vessel_Ids, Year, Job_Type);
        }

        /// <summary>
        /// Description: Method to fetch Incident count for all vessels per year
        /// Created By: Krishnapriya
        /// Created On: 21/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetMultipleVesselIncidentCount(string Vessel_Ids, string Years, string Job_Type)
        {
            return objDAL.GetMultipleVesselIncidentCount(Vessel_Ids, Years, Job_Type);
        }

        
         /// <summary>
        /// Description: Method to fetch Incident count for all categories per year
        /// Created By: Krishnapriya
        /// Created On: 22/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>

        public DataSet GetCategoryIncidentCount(string Vessel_Ids, string Years, string Job_Type)
        {
            return objDAL.GetCategoryIncidentCount(Vessel_Ids, Years, Job_Type);
        }

        /// <summary>
        /// Description: Method to fetch Injury/Death Incident count for vessels
        /// Created By: Krishnapriya
        /// Created On: 23/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>
        /// <param name="SubType"> Sub Type of job</param>

        public DataSet GetInjuryIncidentCount(string Vessel_Ids, string Year, string Job_Type, string SubType)
        {
            return objDAL.GetInjuryIncidentCount(Vessel_Ids, Year, Job_Type, SubType);
        }

        /// <summary>
        /// Description: Method to fetch injury/death Incident count for all categories per year
        /// Created By: Krishnapriya
        /// Created On: 23/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>
        /// <param name="SubType"> Sub Type of job</param>

        public DataSet GetCategoryInjuryIncidentCount(string Vessel_Ids, string Years, string Job_Type, string SubType)
        {
            return objDAL.GetCategoryInjuryIncidentCount(Vessel_Ids, Years, Job_Type, SubType);
        }

        
        /// <summary>
        /// Description: Method to fetch injury/death Incident count for all vessels per year
        /// Created By: Krishnapriya
        /// Created On: 23/12/2016
        /// </summary>
        /// <param name="Vessel_Ids"> Selected vessels parameter </param>
        /// <param name="Year"> Selected years for searching crew retention w</param>
        /// <param name="Job_Type"> Type of job</param>
        /// <param name="SubType"> Sub Type of job</param>

        public DataSet GetMultipleVesselInjuryIncidentCount(string Vessel_Ids, string Years, string Job_Type, string SubType)
        {
            return objDAL.GetMultipleVesselInjuryIncidentCount(Vessel_Ids, Years, Job_Type, SubType);
        }

    }
}
