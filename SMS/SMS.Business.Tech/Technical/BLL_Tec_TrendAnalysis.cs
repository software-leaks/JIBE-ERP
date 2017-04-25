using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Technical;

namespace SMS.Business.Technical
{
    public class BLL_Tec_TrendAnalysis
    {
        DAL_Tec_TrendAnalysis objDLL = new DAL_Tec_TrendAnalysis();


        public DataTable Get_INSP_PSC_And_Defects_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            return objDLL.Get_INSP_PSC_And_Defects_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        }

        public DataSet Get_INSP_Total_Inspections_Defects(int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            return objDLL.Get_INSP_Total_Inspections_Defects(FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        }

        public DataTable Get_INSP_NCR_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            return objDLL.Get_INSP_NCR_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        }

        public DataTable Get_INSP_NearMiss_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            return objDLL.Get_INSP_NearMiss_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        }

        public DataTable Get_INSP_Injuries_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            return objDLL.Get_INSP_Injuries_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        }

        public DataTable Get_INSP_PropertyPollution_Count(int FILTERBY, int FLEETCODE, DateTime FROMDATE, DateTime TODATE, int UserCompanyID)
        {
            return objDLL.Get_INSP_PropertyPollution_Count(FILTERBY, FLEETCODE, FROMDATE, TODATE, UserCompanyID);
        }
    }
}
