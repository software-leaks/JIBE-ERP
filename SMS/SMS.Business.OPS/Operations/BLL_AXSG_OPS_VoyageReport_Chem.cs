using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.OPS;
using System.Data;
using SMS.Data.Operation;

namespace SMS.Business.OPS
{
    public class BLL_AXSG_OPS_VoyageReport_Chem
    {
        DAL_AXSG_OPS_VoyageReport_Chem objDAL = new DAL_AXSG_OPS_VoyageReport_Chem();
        public static DataTable Get_DailyVoyageReportIndex_Chem(string ReportType, int VesselID, int LocationCode, string FromDate, string ToDate, int FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {

            DateTime fromdt = DateTime.Parse(FromDate);
            DateTime todt = DateTime.Parse(ToDate);

            return DAL_AXSG_OPS_VoyageReport_Chem.Get_DailyVoyageReportIndex_Chem_DL(ReportType, VesselID, LocationCode, fromdt, todt, FleetID, Page_Index, Page_Size, ref Record_Count, Sort_Column, Sort_Direction);

        }
        public static DataTable Get_VoyageReportIndex_AXSG(string ReportType, int VesselID, int LocationCode, string sFromDate, string sToDate, int FleetID)
        {
            DateTime? FromDate;
            DateTime? ToDate;

            if (sFromDate == "")
            {
                FromDate = null;
            }
            else
            {
                FromDate = DateTime.Parse(sFromDate);
            }
            if (sToDate == "")
            {
                ToDate = null;
            }
            else
            {
                ToDate = DateTime.Parse(sToDate);
            }

            return DAL_AXSG_OPS_VoyageReport_Chem.Get_VoyageReportIndex_AXSG(ReportType, VesselID, LocationCode, FromDate, ToDate, FleetID);

        }
        public static DataSet Get_DailyNoonReport_DL_AXSG(int VesselID, decimal TelegramID)
        {
            return DAL_AXSG_OPS_VoyageReport_Chem.Get_DailyNoonReport_DL_AXSG(VesselID, TelegramID);
        }
    }
}
