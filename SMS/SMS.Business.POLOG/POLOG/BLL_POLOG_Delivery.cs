using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.POLOG;

namespace SMS.Business.POLOG
{
    public class BLL_POLOG_Delivery
    {
        public static DataSet POLOG_Get_Delivery_List(int? Supply_ID, string CurrStatus, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLOG_Get_Delivery_List(Supply_ID, CurrStatus, CreatedBy);
        }
        public static int POLog_Insert_Delivery_Item(int? ID, int? Delivery_ID, string Name, decimal? PO_Qty, decimal? PO_Price, string Confirm_Unit, decimal? Con_Qty, decimal? Con_Price, string Remarks, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLog_Insert_Delivery_Item(ID, Delivery_ID, Name, PO_Qty, PO_Price, Confirm_Unit, Con_Qty, Con_Price, Remarks, CreatedBy);
        }
        public static string POLOG_Insert_Delivery_Details(string Delivery_ID, int? Supply_ID, DateTime DeliveryDate, string Location, int? PortCallID, string Remarks, string Action_By_Button, string DeliveryStatus, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLOG_Insert_Delivery_Details(Delivery_ID, Supply_ID, DeliveryDate, Location, PortCallID, Remarks, Action_By_Button, DeliveryStatus, CreatedBy);
        }
        public static int POLOG_Delete_Delivery_Details(string Delivery_ID, int? Supply_ID, string Action_By_Button, string DeliveryStatus, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLOG_Delete_Delivery_Details(Delivery_ID, Supply_ID, Action_By_Button, DeliveryStatus, CreatedBy);
        }
        public static DataSet POLOG_Get_Delivery_Details(string Delivery_ID, int? Supply_ID, string Type, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLOG_Get_Delivery_Details(Delivery_ID, Supply_ID, Type, CreatedBy);
        }
        public static DataSet POLOG_Get_Delivery_Item_Details(string Delivery_ID, int? Supply_ID, string Type, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLOG_Get_Delivery_Item_Details(Delivery_ID, Supply_ID, Type, CreatedBy);
        }
        public static int POLOG_Delete_Delivery_Item(int? ID, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLOG_Delete_Delivery_Item(ID, CreatedBy);
        }
       
        public static DataSet POLOG_Get_Delivery_Item_Details(int? ID, int? DeliveryID)
        {
            return DAL_POLOG_Delivery.POLOG_Get_Delivery_Item_Details(ID, DeliveryID);
        }
        public static int POLog_Insert_Delivery_Item(string Delivery_ID, int? Supply_ID, DataTable dtItem, int? CreatedBy)
        {
            return DAL_POLOG_Delivery.POLog_Insert_Delivery_Item(Delivery_ID, Supply_ID, dtItem, CreatedBy);
        }
    }
}
