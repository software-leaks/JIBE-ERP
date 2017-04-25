using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using SMS.Data;

namespace SMS.Data.CP
{
    public class DAL_CP_CharterParty
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";

        public DAL_CP_CharterParty(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_CP_CharterParty()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }



        public int Ins_CharterParty(int? VesselId, string Supplier_Code, string Owner_Code, string Broker_Code, string Broker_Code2, string Broker_Code3,
            DateTime? Vessel_Opening_Date, int? Vessel_Opening_Port_ID, string Delivery_Port, string ReDelivery_Port, string Delivery_Time_Zone, string ReDelivery_Time_Zone,
            DateTime? Delivery_Date, DateTime? ReDelivery_Date, DateTime? CP_Date, int? CP_Type_ID, int? CP_Status_ID, string LayCan, string LayCan_Time_Zone, DateTime? LayCan_Start, DateTime? LayCan_End, string Hire_Terms, string Commission_Terms,
            double Hire_Rate, double Address_Comm, string Billing_Period, string Billing_Cycle_Type, string Billing_Cycle_Value, double Brokerage_Comm, double Brokerage_Comm2, double Brokerage_Comm3,
            string Brokerage_Comm_Payment, string Brokerage2_Comm_Payment, string Brokerage3_Comm_Payment,
            int? Hire_Type_Id, string BANK_ACCOUNT_ID, double? Redelivery_Notice_Days, int Created_By)
        {
            int Charter_Party_ID = 0;
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Vessel_ID", VesselId),
                                        new SqlParameter("@Charterer_Code", Supplier_Code),
                                        new SqlParameter("@Owner_Code", Owner_Code),
                                        new SqlParameter("@Broker_Code", Broker_Code),
                                        new SqlParameter("@Broker_Code2", Broker_Code2),
                                        new SqlParameter("@Broker_Code3", Broker_Code3),
                                        new SqlParameter("@Vessel_Opening_Date", Vessel_Opening_Date),
                                        new SqlParameter("@Vessel_Opening_Port_ID", Vessel_Opening_Port_ID),
                                        new SqlParameter("@Delivery_Port", Delivery_Port),
                                        new SqlParameter("@ReDelivery_Port", ReDelivery_Port),
                                         new SqlParameter("@Delivery_Date", Delivery_Date),
                                        new SqlParameter("@ReDelivery_Date", ReDelivery_Date),
                                        new SqlParameter("@CP_Date", CP_Date),
                                        new SqlParameter("@CP_Type_ID", CP_Type_ID),
                                        new SqlParameter("@CP_Status_ID", CP_Status_ID),
                                        new SqlParameter("@LayCan", LayCan),
                                        new SqlParameter("@LayCan_Time_Zone", LayCan_Time_Zone),
                                        new SqlParameter("@LayCan_Start", LayCan_Start),
                                        new SqlParameter("@LayCan_End", LayCan_End),
                                        new SqlParameter("@Hire_Terms", Hire_Terms),
                                        new SqlParameter("@Commission_Terms", Commission_Terms),
                                        new SqlParameter("@Hire_Rate", Hire_Rate),
                                        new SqlParameter("@Address_Comm", Address_Comm),
                                        new SqlParameter("@Billing_Period", Billing_Period),
                                        new SqlParameter("@Billing_Cycle_Value", Billing_Cycle_Value),
                                        new SqlParameter("@Billing_Cycle_Type", Billing_Cycle_Type),                                                                
                                        new SqlParameter("@Brokerage_Comm", Brokerage_Comm),
                                        new SqlParameter("@Brokerage_Comm2", Brokerage_Comm2),
                                        new SqlParameter("@Brokerage_Comm3", Brokerage_Comm3),
                                        new SqlParameter("@Brokerage_Comm_Payment", Brokerage_Comm_Payment),
                                        new SqlParameter("@Brokerage2_Comm_Payment", Brokerage2_Comm_Payment),
                                        new SqlParameter("@Brokerage3_Comm_Payment", Brokerage3_Comm_Payment),
                                        new SqlParameter("@Hire_Type_Id", Hire_Type_Id),
                                        new SqlParameter("@BANK_ACCOUNT_ID", BANK_ACCOUNT_ID),
                                        new SqlParameter("@Delivery_Time_Zone", Delivery_Time_Zone),
                                        new SqlParameter("@ReDelivery_Time_Zone", ReDelivery_Time_Zone),
                                        new SqlParameter("@Redelivery_Notice_Days", Redelivery_Notice_Days),
                                        new SqlParameter("@Created_By", Created_By),
                                        new SqlParameter("@Charter_Party_ID", Charter_Party_ID)
                                    };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_INS_Charter_Party", sqlprm);
            Charter_Party_ID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return Charter_Party_ID;
        }


        public int UPD_CharterParty(int? Charterer_Party_Id, int? VesselId, string Supplier_Code, string Owner_Code, string Broker_Code, string Broker_Code2, string Broker_Code3,
            DateTime? Vessel_Opening_Date, int? Vessel_Opening_Port_ID, string Delivery_Port, string ReDelivery_Port, string Delivery_Time_Zone, string ReDelivery_Time_Zone,
           DateTime? Delivery_Date, DateTime? ReDelivery_Date, DateTime? CP_Date, int? CP_Type_ID, int? CP_Status_ID, string LayCan, string LayCan_Time_Zone, DateTime? LayCan_Start, DateTime? LayCan_End, string Hire_Terms, string Commission_Terms,
            double Hire_Rate, double Address_Comm, string Billing_Period, string Billing_Cycle_Type, string Billing_Cycle_Value,
           double Brokerage_Comm, double Brokerage_Comm2, double Brokerage_Comm3, string Brokerage_Comm_Payment, string Brokerage2_Comm_Payment, string Brokerage3_Comm_Payment, int? Hire_Type_Id, string BANK_ACCOUNT_ID, double? Redelivery_Notice_Days, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 

                                        new SqlParameter("@Charter_Party_ID", Charterer_Party_Id),
                                        new SqlParameter("@Vessel_ID", VesselId),
                                        new SqlParameter("@Charterer_Code", Supplier_Code),
                                        new SqlParameter("@Owner_Code", Owner_Code),
                                        new SqlParameter("@Broker_Code", Broker_Code),
                                        new SqlParameter("@Broker_Code2", Broker_Code2),
                                        new SqlParameter("@Broker_Code3", Broker_Code3),
                                        new SqlParameter("@Vessel_Opening_Date", Vessel_Opening_Date),
                                        new SqlParameter("@Vessel_Opening_Port_ID", Vessel_Opening_Port_ID),
                                        new SqlParameter("@Delivery_Port", Delivery_Port),
                                        new SqlParameter("@ReDelivery_Port", ReDelivery_Port),
                                        new SqlParameter("@Delivery_Date", Delivery_Date),
                                        new SqlParameter("@ReDelivery_Date", ReDelivery_Date),
                                        new SqlParameter("@CP_Date", CP_Date),
                                        new SqlParameter("@CP_Type_ID", CP_Type_ID),
                                        new SqlParameter("@CP_Status_ID", CP_Status_ID),
                                        new SqlParameter("@LayCan", LayCan),
                                        new SqlParameter("@LayCan_Time_Zone", LayCan_Time_Zone),
                                        new SqlParameter("@LayCan_Start", LayCan_Start),
                                        new SqlParameter("@LayCan_End", LayCan_End),
                                        new SqlParameter("@Hire_Terms", Hire_Terms),
                                        new SqlParameter("@Commission_Terms", Commission_Terms),
                                        new SqlParameter("@Hire_Rate", Hire_Rate),
                                        new SqlParameter("@Address_Comm", Address_Comm),
                                        new SqlParameter("@Billing_Period", Billing_Period),
                                        new SqlParameter("@Billing_Cycle_Value", Billing_Cycle_Value),
                                        new SqlParameter("@Billing_Cycle_Type", Billing_Cycle_Type),  
                                        new SqlParameter("@Brokerage_Comm", Brokerage_Comm),
                                        new SqlParameter("@Brokerage_Comm2", Brokerage_Comm2),
                                        new SqlParameter("@Brokerage_Comm3", Brokerage_Comm3),
                                        new SqlParameter("@Brokerage_Comm_Payment", Brokerage_Comm_Payment),
                                        new SqlParameter("@Brokerage2_Comm_Payment", Brokerage2_Comm_Payment),
                                        new SqlParameter("@Brokerage3_Comm_Payment", Brokerage3_Comm_Payment),
                                        new SqlParameter("@Hire_Type_Id", Hire_Type_Id),
                                        new SqlParameter("@BANK_ACCOUNT_ID", BANK_ACCOUNT_ID),
                                        new SqlParameter("@Delivery_Time_Zone", Delivery_Time_Zone),
                                        new SqlParameter("@ReDelivery_Time_Zone", ReDelivery_Time_Zone),
                                        new SqlParameter("@Redelivery_Notice_Days", Redelivery_Notice_Days),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_UPD_Charter_Party", sqlprm);
        }

        public int INS_BunkerDetail(int? Charter_Party_ID, int? Fuel_Type_Id, string Fuel_Type, string Operation_Type, double? Fuel_Amt, double? Unit_Price, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Charter_Party_ID", Charter_Party_ID),
                                        new SqlParameter("@Fuel_Type_Id", Fuel_Type_Id),
                                        new SqlParameter("@Fuel_Type", Fuel_Type),
                                        new SqlParameter("@Operation_Type", Operation_Type),
                                        new SqlParameter("@Fuel_Amt", Fuel_Amt),
                                        new SqlParameter("@Unit_Price", Unit_Price),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_BunkerDetail", sqlprm);
        }

        public int INS_Remarks(int? Charter_ID, int? For_Action_By, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Charter_ID", Charter_ID),
                                        new SqlParameter("@For_Action_By", For_Action_By),
                                        new SqlParameter("@Remarks", Remarks),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_Remarks", sqlprm);
        }

        public int UPD_Remarks(int? Remarks_ID, string Status, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Remarks_ID", Remarks_ID),
                                        new SqlParameter("@Status", Status),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_UPD_Remarks", sqlprm);
        }

        public DataTable Get_RemarksAll(int? Charter_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_RemarksAll", sqlprm).Tables[0];
        }

        public DataTable Get_GeneralRemarks(int? Charter_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_GeneralRemarks", sqlprm).Tables[0];
        }

        public int UPD_BunkerDetail(int? Delivery_Bunker_ID, int? Charter_Party_ID, int? Fuel_Type_Id, string Fuel_Type, string Operation_Type, double? Fuel_Amt, double? Unit_Price, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Delivery_Bunker_ID", Delivery_Bunker_ID),
                                        new SqlParameter("@Charter_Party_ID", Charter_Party_ID),
                                        new SqlParameter("@Fuel_Type_Id", Fuel_Type_Id),
                                         new SqlParameter("@Fuel_Type", Fuel_Type),
                                        new SqlParameter("@Operation_Type", Operation_Type),
                                        new SqlParameter("@Fuel_Amt", Fuel_Amt),
                                        new SqlParameter("@Unit_Price", Unit_Price),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_UPD_BunkerDetail", sqlprm);
        }



        public DataTable Get_BunkerList(int? Charter_Party_ID, string Type)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_Party_ID",Charter_Party_ID),
                                            new SqlParameter("@Type",Type)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_BunkerList", sqlprm).Tables[0];
        }

        public DataTable Get_BunkerDetail(int? Delivery_Bunker_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Delivery_Bunker_ID",Delivery_Bunker_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_BunkerDetail", sqlprm).Tables[0];
        }

        public int Delete_BunkerDetail(int? Delivery_Bunker_ID, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Delivery_Bunker_ID", Delivery_Bunker_ID), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Cp_Delete_BunkerDetail", obj);

        }

        public DataTable GET_Charter_Party_Details(int? Charter_Party_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_Party_ID",Charter_Party_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Charter_Party_Details", sqlprm).Tables[0];
        }


        public DataTable GetCharterSupplier_List()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetCharterSupplier_List").Tables[0];
        }

        public DataTable Get_FuelTypes()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_FuelTypes").Tables[0];
        }

        public DataTable GET_VesselOwner_Detail(int? VesselId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@Vessel_ID", VesselId) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_VesselOwner_Detail", sqlprm).Tables[0];
        }
        public DataTable GetSupplier_List(string sType)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@Supplier_Type", sType) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetSupplier_List", sqlprm).Tables[0];
        }

        public DataTable CP_GetStatus_List()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetStatus_List").Tables[0];
        }
        public DataTable GetOwnerBank_List()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetOwnerBank_List").Tables[0];
        }
        public DataTable GetType_List()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetType_List").Tables[0];
        }
        public DataTable GetHireType_List()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetHireType_List").Tables[0];
        }

        public DataTable Get_Charter_Party_List_Search(int? VesselId, string Supplier_Code, DataTable dtStatusIds, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@dt_CP_StatusIDs", dtStatusIds),
                
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", VesselId), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Charter_Party_List_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        /*
         * Method: To get the redelivery details for charter party
         * Created By: Bhairab
         * Parameter: VesselID,display page number, page records , total record count
         * TO DO: If needed  filter parameters to be added.
         */

        public DataTable Redelivery_GetDetail(int VesselID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID",VesselID),
                   new System.Data.SqlClient.SqlParameter("@PAGE_INDEX",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGE_SIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Redelivery_GetDetail", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        /*
        * Method: To Delete records for   charter party
        * Parameter: CharterId, Deleted user
        * Created By: Bhairab
        * Created On: 07/07/2015
        */

        public int Delete_Charterer(int? Chaterer_Id, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Charter_ID", Chaterer_Id), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_Delete_Charterer", obj);

        }


        public DataTable GET_Trading_Period(int? Charter_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Trading_Period", sqlprm).Tables[0];
        }

        public DataSet GET_Billing_Items_Detail(int? Charter_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Billing_Items_Detail", sqlprm);
        }


        public int INS_UPD_Trading_Range(int? Charter_ID, DataTable dtTradingRange, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Charter_Party_ID", Charter_ID),
                                        new SqlParameter("@dtTradingRange", dtTradingRange),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_UPD_Trading_Range", sqlprm);
        }


        public int Delete_Trading_Period(int? Trading_Range_Id, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Trading_Range_Id", Trading_Range_Id), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Cp_Delete_Trading_Period", obj);

        }

        public DataTable Get_Remittance_Receipt_List(int? Charter_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Remittance_Receipt_List", sqlprm).Tables[0];
        }

        public DataSet Get_Owner_Expenses(int? Charter_ID, bool Hide_Matched, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID)
                                            , new SqlParameter("@Hide_Matched",Hide_Matched)
                                            , new SqlParameter("@User_Id",UserId)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Owner_Expenses", sqlprm);
        }

        public int Unmatched_Remittance(int? Charter_ID, string Remittance_ID, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Charter_ID", Charter_ID), 
                new System.Data.SqlClient.SqlParameter("@Remittance_ID", Remittance_ID), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_Unmatched_Remittance", obj);

        }

        public DataSet Get_Remittance_Receipt_List(int? Charter_ID, int? Billing_Item_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CPID",Charter_ID)
                                            , new SqlParameter("@Billing_Item_ID",Billing_Item_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Billing_Item_Info", sqlprm);
        }

        public int INS_UPD_Billing_Item_Price(int? Charter_ID, int? Billing_Item_ID, DataTable dtItemAmt, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Charter_Party_Id", Charter_ID),
                                        new SqlParameter("@Billing_Item_ID", Billing_Item_ID),
                                        new SqlParameter("@dtItemAmt", dtItemAmt),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_UPD_Billing_Item_Price", sqlprm);
        }


        public int INS_UPD_Billing_Item(int? Charter_ID, int? Billing_Item_ID, string Item_Group, string Item_Description, int Billing_Interval, string Billing_Interval_Unit, string Item_Rate, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Charter_Party_Id", Charter_ID),
                                        new SqlParameter("@Billing_Item_ID", Billing_Item_ID),
                                        new SqlParameter("@Item_Group", Item_Group),
                                        new SqlParameter("@Item_Description", Item_Description),
                                        new SqlParameter("@Billing_Interval", Billing_Interval),
                                        new SqlParameter("@Billing_Interval_Unit", Billing_Interval_Unit),
                                        new SqlParameter("@Item_Rate", Item_Rate),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_UPD_Billing_Item", sqlprm);
        }

        public int INS_UPD_Trading_RangeByItem(int? Trading_Range_Id, int? Charter_ID, DateTime? Trading_Date, string Trading_Range, string Date_Zone, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Trading_Range_Id", Trading_Range_Id),
                                        new SqlParameter("@Charter_ID", Charter_ID),
                                        new SqlParameter("@Trading_Date", Trading_Date),
                                        new SqlParameter("@Trading_Range", Trading_Range),
                                        new SqlParameter("@Date_Zone", Date_Zone),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_UPD_Trading_RangeByItem", sqlprm);
        }
        public int DEL_Billing_Item(int? Billing_Item_Id, int Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Billing_Item_Id", Billing_Item_Id), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_DEL_Billing_Item", obj);

        }


        public int INS_Attachments(int? CPID, string ATTACHMENT_NAME, string ATTACHMENT_PATH, string Unique_ID, string Description, int Sent_Size, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@CPID", CPID),
                                        new SqlParameter("@ATTACHMENT_NAME", ATTACHMENT_NAME),
                                        new SqlParameter("@ATTACHMENT_PATH", ATTACHMENT_PATH),
                                        new SqlParameter("@Unique_ID", Unique_ID),
                                        new SqlParameter("@Description", Description),
                                        new SqlParameter("@Sent_Size", Sent_Size),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_INS_Attachments", sqlprm);
        }


        public DataTable GET_Attachments(int CPID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CPID",CPID),
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Attachments", sqlprm).Tables[0];
        }


        public DataTable GET_CharterFiles(string Charter_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_CharterFiles", sqlprm).Tables[0];
        }


        public int DEL_Attachments(int? CPID, int? Attchments_ID, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Attchments_ID", Attchments_ID), 
                new System.Data.SqlClient.SqlParameter("@CPID", CPID), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_DEL_Attachments", obj);

        }

        public DataTable Get_OutstandingRemarks(int? CPID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",CPID),
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_OutstandingRemarks", sqlprm).Tables[0];
        }

        public DataTable GetFolderId(string Department_Code, int userid)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Department_Code",Department_Code),
                                              new SqlParameter("@USERID",userid)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_FolderID", obj).Tables[0];
        }

        public DataTable Insert_UploadFiles(string filename, string extension, double? filesize, int Createdby, string folderid, string Title, string Content, string CID, string fun, string fid)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { 
                                            new SqlParameter("@File_Name",filename),
                                            new SqlParameter("@File_Extention",extension),
                                            new SqlParameter("@File_Size",filesize),
                                            new SqlParameter("@Created_By",Createdby),
                                            new SqlParameter("@Folder_ID",folderid),
                                             new SqlParameter("@Title",Title),
                                              new SqlParameter("@Content",Content),
                                              new SqlParameter("@CID",CID),
                                              new SqlParameter("@Function",fun),
                                              new SqlParameter("@FID",fid)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Insert_UploadedFiles", obj).Tables[0];
        }

        public DataSet Get_OwnerAccess(int UserID, string FolderID)
        {
            SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@USERID",UserID),
                  new SqlParameter("@FolderID",FolderID)
             };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Owner_Access", obj);
        }

        public DataSet Get_Vessel_List()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Vessel_List");
        }

        public int INS_Charter_Receipts(string Charter_ID, int? Vessel_ID, DateTime? Received_Date, string Received_Currency, double? Amount_Received, string Remittance_Remarks,int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                   new SqlParameter("@Charter_ID",Charter_ID),
                  new SqlParameter("@Vessel_ID",Vessel_ID),
                     new SqlParameter("@Received_Date",Received_Date),
                  new SqlParameter("@Received_Currency",Received_Currency),
                    new SqlParameter("@Amount_Received",Amount_Received),
                  new SqlParameter("@Remittance_Remarks",Remittance_Remarks),
                
                    new SqlParameter("@Created_By",Created_By)
                  
            };
               return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_INS_Charter_Receipts", obj);
        }

        public DataSet Get_Charter_Details_List(string Vessel_Code, string Charterer_Code, string CP_Status)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                   new SqlParameter("@Vessel_Code",Vessel_Code),
                  new SqlParameter("@Charterer_Code",Charterer_Code),
                     new SqlParameter("@CP_Status",CP_Status)
                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Charter_Details_List", obj);
        }

        public DataSet Get_Remittance_Details(string Charter_ID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@Charter_ID",Charter_ID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Remittance_Details", obj);
        }

        public int Update_Remittance_Details(DateTime? Received_Date,double? Amount_Received,string Remittance_Remarks,string Remittance_ID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                   new SqlParameter("@Received_Date",Received_Date),
                  new SqlParameter("@Amount_Received",Amount_Received),
                   
                  new SqlParameter("@Remittance_Remarks",Remittance_Remarks),
                    new SqlParameter("@Remittance_ID",Remittance_ID)
                  
                 
                  
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_Update_Remittance_Details", obj);
        }
    }
}
