using System.Data;
using SMS.Data.PURC;
using System;

namespace SMS.Business.PURC
{
    public class BLL_PURC_Report
    {
        public static DataTable GET_Reqsn_Processing_Time_BY_Reqsn(string Requisition_Code)
        {
            return DAL_PURC_Report.GET_Reqsn_Processing_Time_BY_Reqsn_DL(Requisition_Code);
        }

        public static DataTable Get_Items_Quantity_List(DataTable dtFleet, DataTable dtVessel, string Search_Items,string Department_Code,string System_Code,string SubSystem_Code ,int? Latest, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Report.Get_Items_Quantity_List(dtFleet, dtVessel, Search_Items, Department_Code, System_Code, SubSystem_Code, Latest, Page_Index, Page_Size, ref Record_count);
        }

        public static int UPD_Item_Quantity(int Vessel_ID, string Item_Ref_Code, int ID, decimal Min_Qty, decimal Max_Qty, string Effective_Date, int User_ID)
        {
            int sts = 0;
            try
            {
                sts = DAL_PURC_Report.UPD_Item_Quantity(Vessel_ID, Item_Ref_Code, ID, Min_Qty, Max_Qty, Effective_Date, User_ID);
            }
            catch { sts = 0; }
            return sts;
        }

        public static DataTable Get_Inventory_UpdatedBy(int ID, int Office_ID, int Vessel_ID)
        {
            return DAL_PURC_Report.Get_Inventory_UpdatedBy(ID, Office_ID, Vessel_ID);
        }
    }
}
