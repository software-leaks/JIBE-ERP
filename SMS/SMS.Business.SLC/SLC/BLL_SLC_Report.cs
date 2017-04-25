using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.SLC;

namespace SMS.Business.SLC
{

    public class BLL_SLC_Report
    {
        DAL_SLC_Report objDAL = new DAL_SLC_Report();

        public DataTable SelectVessel()
        {
            return objDAL.SelectVessel();
        }

        public DataTable Get_SlopChestReport(int? VESSEL_ID, int? YEAR, int? MONTH)
        {
            return objDAL.Get_SlopchestReport_DL(VESSEL_ID, YEAR, MONTH);
        }
    }
}
