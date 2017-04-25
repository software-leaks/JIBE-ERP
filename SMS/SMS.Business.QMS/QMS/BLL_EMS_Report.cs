using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SMS.Data.QMS;


namespace SMS.Business.QMS
{
    public class BLL_EMS_Report
    {
        public static DataTable Get_Reports_Index_DL( int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_EMS_Report.Get_Reports_Index_DL(FleetID,VesselID, dtFrom, dtTo, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable Get_Reports_Details(int VesselID, int ID)
        {
            return DAL_EMS_Report.Get_Reports_Details_DL(VesselID, ID);
        }
    }
}
