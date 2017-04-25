using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.SLC;

namespace SMS.Business.SLC
{
    public class BLL_SLC_ConsumptionReport
    {

        DAL_SLC_ConsumptionReport objDAL = new DAL_SLC_ConsumptionReport();

        public DataTable Get_SlopChestConsumptionReport(string DATE, string CREW, int? ITEM, int? PAGE_INDEX, int? PAGE_SIZE, ref int IS_FETCH_COUNT)
        {
            return objDAL.Get_SlopChestConsumptionReport_DL(DATE, CREW, ITEM, PAGE_INDEX, PAGE_SIZE, ref IS_FETCH_COUNT);
        }

        public DataTable SelectItems()
        {

            return objDAL.SelectItems();


        }


       

    }
}
