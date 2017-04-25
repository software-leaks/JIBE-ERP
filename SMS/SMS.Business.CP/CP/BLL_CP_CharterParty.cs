using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web;


using SMS.Data.CP;


namespace SMS.Business.CP
{
    public class BLL_CP_CharterParty
    {

        DAL_CP_CharterParty objDALCP = new DAL_CP_CharterParty();
        public int Ins_CharterParty(int? VesselId, string Supplier_Code, string Owner_Code, string Broker_Code, string Broker_Code2, string Broker_Code3,
            DateTime? Vessel_Opening_Date, int? Vessel_Opening_Port_ID, string Delivery_Port, string ReDelivery_Port, string Delivery_Time_Zone, string ReDelivery_Time_Zone,
            DateTime? Delivery_Date, DateTime? ReDelivery_Date, DateTime? CP_Date, int? CP_Type_ID, int? CP_Status_ID, string LayCan, string LayCan_Time_Zone, DateTime? LayCan_Start, DateTime? LayCan_End, string Hire_Terms, string Commission_Terms,
            double Hire_Rate, double Address_Comm, string Billing_Period, string Billing_Cycle_Type, string Billing_Cycle_Value, double Brokerage_Comm, double Brokerage_Comm2, double Brokerage_Comm3,
            string Brokerage_Comm_Payment, string Brokerage2_Comm_Payment, string Brokerage3_Comm_Payment,
            int? Hire_Type_Id, string BANK_ACCOUNT_ID, double? Redelivery_Notice_Days, int Created_By)
        {
            return objDALCP.Ins_CharterParty( VesselId,  Supplier_Code,  Owner_Code,  Broker_Code,Broker_Code2,Broker_Code3,  Vessel_Opening_Date, Vessel_Opening_Port_ID,  Delivery_Port,  ReDelivery_Port, Delivery_Time_Zone,ReDelivery_Time_Zone,
             Delivery_Date, ReDelivery_Date, CP_Date, CP_Type_ID, CP_Status_ID, LayCan, LayCan_Time_Zone, LayCan_Start, LayCan_End, Hire_Terms, Commission_Terms,
             Hire_Rate, Address_Comm, Billing_Period, Billing_Cycle_Type, Billing_Cycle_Value, Brokerage_Comm,Brokerage_Comm2,Brokerage_Comm3,Brokerage_Comm_Payment,Brokerage2_Comm_Payment,Brokerage3_Comm_Payment,
             Hire_Type_Id, BANK_ACCOUNT_ID, Redelivery_Notice_Days, Created_By);
        }

        public int UPD_CharterParty(int? Charterer_Party_Id, int? VesselId, string Supplier_Code, string Owner_Code, string Broker_Code, string Broker_Code2, string Broker_Code3,
            DateTime? Vessel_Opening_Date, int? Vessel_Opening_Port_ID, string Delivery_Port, string ReDelivery_Port, string Delivery_Time_Zone, string ReDelivery_Time_Zone,
           DateTime? Delivery_Date, DateTime? ReDelivery_Date, DateTime? CP_Date, int? CP_Type_ID, int? CP_Status_ID, string LayCan, string LayCan_Time_Zone, DateTime? LayCan_Start, DateTime? LayCan_End, string Hire_Terms, string Commission_Terms,
            double Hire_Rate, double Address_Comm, string Billing_Period, string Billing_Cycle_Type, string Billing_Cycle_Value,
           double Brokerage_Comm, double Brokerage_Comm2, double Brokerage_Comm3, string Brokerage_Comm_Payment, string Brokerage2_Comm_Payment, string Brokerage3_Comm_Payment, int? Hire_Type_Id, string BANK_ACCOUNT_ID, double? Redelivery_Notice_Days, int Created_By)
        {
          return objDALCP.UPD_CharterParty(Charterer_Party_Id, VesselId, Supplier_Code, Owner_Code, Broker_Code, Broker_Code2,  Broker_Code3, Vessel_Opening_Date, Vessel_Opening_Port_ID, Delivery_Port, ReDelivery_Port, Delivery_Time_Zone, ReDelivery_Time_Zone,
            Delivery_Date, ReDelivery_Date, CP_Date,  CP_Type_ID, CP_Status_ID,  LayCan,  LayCan_Time_Zone,  LayCan_Start,  LayCan_End,  Hire_Terms,  Commission_Terms,
             Hire_Rate,  Address_Comm,  Billing_Period, Billing_Cycle_Type,  Billing_Cycle_Value,  Brokerage_Comm,Brokerage_Comm2,Brokerage_Comm3,Brokerage_Comm_Payment,Brokerage2_Comm_Payment,Brokerage3_Comm_Payment,  Hire_Type_Id,  BANK_ACCOUNT_ID,  Redelivery_Notice_Days, Created_By);
        }

        public int INS_BunkerDetail(int? Charter_Party_ID, int? Fuel_Type_Id, string Fuel_Type, string Operation_Type, double? Fuel_Amt, double? Unit_Price, int Created_By)
      {
          return objDALCP.INS_BunkerDetail(Charter_Party_ID, Fuel_Type_Id, Fuel_Type, Operation_Type, Fuel_Amt, Unit_Price, Created_By);
      }

        public int UPD_BunkerDetail(int? Delivery_Bunker_ID, int? Charter_Party_ID, int? Fuel_Type_Id, string Fuel_Type, string Operation_Type, double? Fuel_Amt, double? Unit_Price, int Created_By)
      {
          return objDALCP.UPD_BunkerDetail(Delivery_Bunker_ID, Charter_Party_ID,  Fuel_Type_Id, Fuel_Type, Operation_Type, Fuel_Amt, Unit_Price, Created_By);
      }

      public DataTable Get_BunkerDetail(int? Delivery_Bunker_ID)
      {
          return objDALCP.Get_BunkerDetail(Delivery_Bunker_ID);
      }

      public int Delete_BunkerDetail(int? Delivery_Bunker_ID, int? Created_By)
      {
          return objDALCP.Delete_BunkerDetail(Delivery_Bunker_ID,Created_By);
      }

      public DataTable GET_VesselOwner_Detail(int? VesselId)
      {
          return objDALCP.GET_VesselOwner_Detail(VesselId);
      }

      public DataTable Get_BunkerList(int? Charter_Party_ID, string Type)
      {
          return objDALCP.Get_BunkerList(Charter_Party_ID, Type);
      }
      public DataTable GET_Charter_Party_Details(int? Charter_Party_ID)
        {
            return objDALCP.GET_Charter_Party_Details(Charter_Party_ID);
        }

        public DataTable CP_GetStatus_List()
        {
            return objDALCP.CP_GetStatus_List();
        }
        public DataTable Get_FuelTypes()
        {
            return objDALCP.Get_FuelTypes();
        }

        public DataTable GetType_List()
        {
            return objDALCP.GetType_List();
        }

        public DataTable GetHireType_List()
        {
            return objDALCP.GetHireType_List();
        }
        public DataTable GetSupplierOwner_List()
        {
            return objDALCP.GetSupplier_List("O");
        }

        public DataTable GetSupplierCharterer_List()
        {
            return objDALCP.GetSupplier_List("C");
        }
        public DataTable GetSupplierBroker_List()
        {
            return objDALCP.GetSupplier_List("B");
        }

        public DataTable GetAgent_List()
        {
            return objDALCP.GetSupplier_List("A");
        }

        public DataTable GetOwnerBank_List()
        {
            return objDALCP.GetOwnerBank_List();
        }





        public DataTable GetCharterSupplier_List()
        {
            return objDALCP.GetCharterSupplier_List();
        }


        public DataTable Get_Charter_Party_List_Search(int? VesselId, string Supplier_Code, DataTable dtStatusIds, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDALCP.Get_Charter_Party_List_Search(VesselId, Supplier_Code, dtStatusIds, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        /*
         * Method: To get the redelivery details for charter party
         * Created By: Bhairab
         * Parameter: VesselID,display page number, page records , total record count
         * TO DO: If needed  filter parameters to be added.
         */
        public DataTable Redelivery_GetDetail(int VesselId,int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDALCP.Redelivery_GetDetail(VesselId, pagenumber, pagesize, ref isfetchcount);
        }

        /*
        * Method: To Delete records for   charter party
        * Parameter: CharterId, Deleted user
        * Created By: Bhairab
        * Created On: 07/07/2015
        */
        public int Delete_Charterer(int? Chaterer_Id, int? Created_By)
        {
            return objDALCP.Delete_Charterer(Chaterer_Id, Created_By);
        }
        public int INS_Remarks(int? Charter_ID, int? For_Action_By, string Remarks, int Created_By)
        {
            return objDALCP.INS_Remarks(Charter_ID, For_Action_By, Remarks, Created_By);
        }

        public int UPD_Remarks(int? Remarks_ID, string Status, int Created_By)
        {
            return objDALCP.UPD_Remarks(Remarks_ID, Status, Created_By);
        }

        public int INS_UPD_Trading_Range(int? Charter_ID, DataTable dtTradingRange, int? Created_By)
        {
            return objDALCP.INS_UPD_Trading_Range(Charter_ID, dtTradingRange, Created_By);
        }
        public DataTable Get_RemarksAll(int? Charter_ID)
        {
            return objDALCP.Get_RemarksAll(Charter_ID);
        }

        public DataTable GeneralRemarks(int? Charter_ID)
        {
            return objDALCP.Get_GeneralRemarks(Charter_ID);
        }

        
        public DataSet GET_Billing_Items_Detail(int? Charter_ID)
        {
            return objDALCP.GET_Billing_Items_Detail(Charter_ID);
        }

        public DataTable GET_Trading_Period(int? Charter_ID)
        {
            return objDALCP.GET_Trading_Period(Charter_ID);
        }

        public int Delete_Trading_Period(int? Trading_Range_Id, int? Created_By)
        {
            return objDALCP.Delete_Trading_Period(Trading_Range_Id, Created_By);
        }

        public DataTable Get_Remittance_Receipt_List(int? Charter_ID)
        {
            return objDALCP.Get_Remittance_Receipt_List(Charter_ID);
        }

        public DataSet Get_Owner_Expenses(int? Charter_ID, bool Hide_Matched, int UserId)
        {
            return objDALCP.Get_Owner_Expenses(Charter_ID, Hide_Matched, UserId);
        }
        
        public int Unmatched_Remittance(int? Charter_ID,string Remittance_ID, int? Created_By)
        {
            return objDALCP.Unmatched_Remittance(Charter_ID,Remittance_ID, Created_By);
        }

        public DataSet Get_Remittance_Receipt_List(int? Charter_ID, int? Billing_Item_ID)
        {
            return objDALCP.Get_Remittance_Receipt_List(Charter_ID, Billing_Item_ID);
        }
        public int INS_UPD_Billing_Item_Price(int? Charter_ID, int? Billing_Item_ID, DataTable dtItemAmt, int? Created_By)
        {
            return objDALCP.INS_UPD_Billing_Item_Price(Charter_ID, Billing_Item_ID, dtItemAmt, Created_By);
        }
        public int INS_UPD_Billing_Item(int? Charter_ID, int? Billing_Item_ID, string Item_Group, string Item_Description, int Billing_Interval, string Billing_Interval_Unit, string Item_Rate, int? Created_By)
        {
            return objDALCP.INS_UPD_Billing_Item(Charter_ID, Billing_Item_ID, Item_Group, Item_Description, Billing_Interval, Billing_Interval_Unit, Item_Rate, Created_By);
        }
        public int INS_UPD_Trading_RangeByItem(int? Trading_Range_Id, int? Charter_ID, DateTime? Trading_Date, string Trading_Range, string Date_Zone, int? Created_By)
        {
            return objDALCP.INS_UPD_Trading_RangeByItem(Trading_Range_Id, Charter_ID, Trading_Date, Trading_Range, Date_Zone, Created_By);
        }

        public int DEL_Billing_Item(int? Billing_Item_Id, int Created_By)
        {
            return objDALCP.DEL_Billing_Item(Billing_Item_Id, Created_By);
        }

        public int INS_Attachments(int? CPID, string ATTACHMENT_NAME, string ATTACHMENT_PATH, string Unique_ID, string Description, int Sent_Size, int? Created_By)
        {
            return objDALCP.INS_Attachments(CPID, ATTACHMENT_NAME, ATTACHMENT_PATH, Unique_ID, Description, Sent_Size, Created_By);
        }
        public DataTable GET_Attachments(int CPID)
        {
            return objDALCP.GET_Attachments(CPID);
        }

        public DataTable GET_CharterFiles(string Charter_ID)
        {
            return objDALCP.GET_CharterFiles(Charter_ID);
        }

        public int DEL_Attachments(int? CPID, int? Attchments_ID, int? Created_By)
        {
            return objDALCP.DEL_Attachments(CPID, Attchments_ID, Created_By);
        }


        public DataTable Get_OutstandingRemarks(int? CPID)
        {
            return objDALCP.Get_OutstandingRemarks(CPID);
        }

        public DataTable GetFolderId(string Department_Code,int userid)
        {
            return objDALCP.GetFolderId(Department_Code, userid);
        }

        public DataTable Insert_UploadFiles(string filename, string extension, double? filesize, int Createdby, string folderid, string Title, string Content, string CID, string fun, string fid)
        {
            return objDALCP.Insert_UploadFiles(filename, extension, filesize, Createdby, folderid, Title, Content, CID,fun,fid);
        }

        public DataSet Get_OwnerAccess(int UserID, string FolderID)
        {
            return objDALCP.Get_OwnerAccess(UserID,FolderID);
        }
        public DataSet Get_Vessel_List()
        {
            return objDALCP.Get_Vessel_List();
        }
        public int INS_Charter_Receipts(string Charter_ID, int? Vessel_ID, DateTime? Received_Date, string Received_Currency, double? Amount_Received, string Remittance_Remarks, int Created_By)
        {
            return objDALCP.INS_Charter_Receipts(Charter_ID, Vessel_ID, Received_Date, Received_Currency, Amount_Received, Remittance_Remarks, Created_By);
        }
        public DataSet Get_Charter_Details_List(string Vessel_Code, string Charterer_Code, string CP_Status)
        {
            return objDALCP.Get_Charter_Details_List(Vessel_Code, Charterer_Code, CP_Status);
        }
        public DataSet Get_Remittance_Details(string Charter_ID)
        {
            return objDALCP.Get_Remittance_Details(Charter_ID);
        }
        public int Update_Remittance_Details(DateTime? Received_Date, double? Amount_Received, string Remittance_Remarks, string Remittance_ID)
        {
            return objDALCP.Update_Remittance_Details(Received_Date, Amount_Received, Remittance_Remarks, Remittance_ID);
        }
    }
}
