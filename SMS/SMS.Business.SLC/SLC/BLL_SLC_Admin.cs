using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.SLC;

namespace SMS.Business.SLC
{
   public class BLL_SLC_Admin
    {
       DAL_SLC_Admin objDAL = new DAL_SLC_Admin();

       public DataTable Get_SlopchestIndex(int? VESSEL_ID, int? YEAR, int? MONTH, int PAGE_INDEX, int PAGE_SIZE, ref int IS_FETCH_COUNT)
       {
           return objDAL.Get_SlopchestIndex_DL(VESSEL_ID, YEAR, MONTH, PAGE_INDEX, PAGE_SIZE, ref IS_FETCH_COUNT);
       }

       public DataTable Get_SlopchestSettings()
       {
           return objDAL.Get_SlopchestSettings_DL();
       }

       public int INSERT_SlopChestSettings(string Key, int value, int? Created_By)
       {
           return objDAL.INSERT_SCSettings_DL(Key, value, Created_By);
       
       }

       public DataTable Get_SlopChestConsumptionReport(int? DATE, int? CREW, int? ITEM, int PAGE_INDEX, int PAGE_SIZE, ref int IS_FETCH_COUNT)
       {
           return objDAL.Get_SlopChestConsumptionReport_DL(DATE, CREW, ITEM, PAGE_INDEX, PAGE_SIZE, ref IS_FETCH_COUNT);
       }

       public DataTable SelectItems()
       {

           return objDAL.SelectItems();


       }


       public static DataTable Get_SlopChest_Items()
       {
           return DAL_SLC_Admin.Get_SlopChest_Items();
       }
       public static int INS_UPD_SlopChest_Commision(string STR, int UserID)
       {
           return DAL_SLC_Admin.INS_UPD_SlopChest_Commision(STR, UserID);
       }
    }
}
