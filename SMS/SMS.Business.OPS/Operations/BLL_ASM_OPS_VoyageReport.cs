using System;
using System.Data;
using SMS.Data.Operation;


namespace SMS.Business.Operation
{

    public class BLL_ASM_OPS_VoyageReport
    {
        DAL_OPS_VoyageReports objDAL = new DAL_OPS_VoyageReports();
        public static DataTable Get_DailyVoyageReportIndex(string ReportType, int VesselID, int LocationCode, string FromDate, string ToDate, int FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {

            DateTime fromdt = DateTime.Parse(FromDate);
            DateTime todt = DateTime.Parse(ToDate);

            return DAL_ASM_OPS_VoyageReport.Get_DailyVoyageReportIndex_DL(ReportType, VesselID, LocationCode, fromdt, todt, FleetID, Page_Index, Page_Size, ref Record_Count, Sort_Column, Sort_Direction);

        }
        public static DataTable Get_VoyageReportIndex_ASM(string ReportType, int VesselID, int LocationCode, string sFromDate, string sToDate, int FleetID)
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

            return DAL_ASM_OPS_VoyageReport.Get_VoyageReportIndex_DL_ASM(ReportType, VesselID, LocationCode, FromDate, ToDate, FleetID);

        }
        public static DataSet Get_DailyNoonReport_DL_ASM(int VesselID, decimal TelegramID)
        {
            return DAL_ASM_OPS_VoyageReport.Get_DailyNoonReport_DL_ASM(VesselID, TelegramID);
        }
    }
}
