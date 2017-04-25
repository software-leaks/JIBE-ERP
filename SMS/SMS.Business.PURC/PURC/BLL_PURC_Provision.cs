using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.PURC;
using SMS.Data.PURC;

namespace SMS.Business.PURC
{
    public class BLL_PURC_Provision
    {
        public BLL_PURC_Provision()
        {

        }
        public static DataSet Get_YearMonthList()
        {
            return DAL_PURC_Provision.Get_YearMonthList();
        }

        public static DataTable Get_Provison_Victualing_Rate(DataTable dtFleet, DataTable dtVessel, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_Victualing_Rate(dtFleet, dtVessel, Page_Index, Page_Size, ref Record_count);
        }

        public static DataTable Get_Provison_Victualing_Rate(string searchtext, DataTable dtFleet, DataTable dtVessel, string sortby, int? sortdirection, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_Victualing_Rate(searchtext, dtFleet, dtVessel, sortby, sortdirection, Page_Index, Page_Size, ref Record_count);
        }
        public static int InsUpt_Victualing_Rate(int? ID, int vesselid, Decimal victualingRate, DateTime? FromDate, int userId)
        {
            return DAL_PURC_Provision.InsUpt_Victualing_Rate(ID, vesselid, victualingRate, FromDate, userId);
        }
        public static DataTable Get_Victualing_Rate_Edit(int ID)
        {
            return DAL_PURC_Provision.Get_Victualing_Rate_Edit(ID);
        }
        public static DataTable Get_Provison_MostExpensive_ItemList(DataTable dtFleet, DataTable dtVessel, string FromDate, string ToDate, int reportType, string SearchText, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_MostExpensive_ItemList(dtFleet, dtVessel, FromDate, ToDate, reportType, SearchText, Page_Index, Page_Size, ref Record_count);
        }

        public static DataTable Get_Provison_MostOrdered_ItemList(DataTable dtFleet, DataTable dtVessel, string FromDate, string ToDate, int reportType, string SearchText, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_MostOrdered_ItemList(dtFleet, dtVessel, FromDate, ToDate, reportType, SearchText, Page_Index, Page_Size, ref Record_count);
        }

        public static DataTable Get_Provison_Item_frequency_List(string searchtext, string sortby, int? sortdirection, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_Item_frequency_List(searchtext, sortby, sortdirection, Page_Index, Page_Size, ref Record_count);
        }
        public static int InsUpt_Provison_Item_frequency(int? ID, int? Vessel_id, string subsystem_code, Decimal reqsn_indays, int userId)
        {
            return DAL_PURC_Provision.InsUpt_Provison_Item_frequency(ID, Vessel_id, subsystem_code, reqsn_indays, userId);
        }
        public static DataTable Get_Provison_Item_frequency_Edit(int ID)
        {
            return DAL_PURC_Provision.Get_Provison_Item_frequency_Edit(ID);
        }

        public static DataTable Get_Provison_ExtraMeal(DataTable dtFleet, DataTable dtVessel, string FromDate, string ToDate, string sortby, int? sortdirection, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_ExtraMeal(dtFleet, dtVessel, FromDate, ToDate, sortby, sortdirection, Page_Index, Page_Size, ref Record_count);
        }
        public static DataTable Get_Provison_OrderVictualing_rate(DataTable dtFleet, DataTable dtVessel, string searchtext, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            return DAL_PURC_Provision.Get_Provison_OrderVictualing_rate(dtFleet, dtVessel, searchtext, Page_Index, Page_Size, ref Record_count);
        }

        public static DataTable Get_Reqsn_Items_Percent(string Reqsn_Code, string Order_Code, string System_Code)
        {
            return DAL_PURC_Provision.Get_Reqsn_Items_Percent(Reqsn_Code, Order_Code, System_Code);
        }

        public static DataSet Get_Calculate_Victualling_Rate(int VesselCode, string requisitioncode, string ordercode, int userid)
        {
            return DAL_PURC_Provision.Get_Calculate_Victualling_Rate(VesselCode, requisitioncode, ordercode, userid);
        }
        public static DataSet PURC_GET_SUBSYSTEMCODE_PROVISIONTYPE(int userid)
        {
            return DAL_PURC_Provision.PURC_GET_SUBSYSTEMCODE_PROVISIONTYPE(userid);
        }
        public static int PURC_UPDATE_SUBSYSTEMCODE_PROVISIONTYPE(int ProviID, string ProvisionType, int CreatedBy)
        {
            return DAL_PURC_Provision.PURC_UPDATE_SUBSYSTEMCODE_PROVISIONTYPE(ProviID, ProvisionType, CreatedBy);
        }
        public static int PURC_DELETE_SUBSYSTEMCODE_PROVISIONTYPE(int ProviID, string ProvisionType, int CreatedBy)
        {
            return DAL_PURC_Provision.PURC_DELETE_SUBSYSTEMCODE_PROVISIONTYPE(ProviID, ProvisionType, CreatedBy);
        }
        public static int PURC_INS_SUBSYSTEMCODE_PROVISIONTYPE(int SUBSYSTEM_ID, string ProvisionType, int UserID)
        {
            return DAL_PURC_Provision.PURC_INS_SUBSYSTEMCODE_PROVISIONTYPE(SUBSYSTEM_ID, ProvisionType, UserID);
        }
        public static DataSet PURC_GET_PROVISIONTYPE()
        {
            return DAL_PURC_Provision.PURC_GET_PROVISIONTYPE();
        }
    }
}
