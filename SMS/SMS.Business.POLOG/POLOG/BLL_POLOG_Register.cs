using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.POLOG;

namespace SMS.Business.POLOG
{
     public class BLL_POLOG_Register
      {

       


         public static DataTable POLOG_Get_PO_Search(string searchtext, string Supplier_Code, int? VesselID, string AccountClassification, string AccountType, string InvoiceStatus, DataTable dtType, string chkClosed, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_PO_Search(searchtext, Supplier_Code, VesselID, AccountClassification, AccountType, InvoiceStatus, dtType, chkClosed, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
         public static DataSet POLOG_Get_PO_Deatils(int? SUPPLY_ID, string PO_Type, int? Created_By)
            {
                return DAL_POLOG_Register.POLOG_Get_PO_Deatils(SUPPLY_ID, PO_Type, Created_By);
            }
         /*
       * Method: To Get records for PO Preview
       * Parameter: SUPPLY_ID, PO_Type Created_By user
       * Created By: Alok
       * Created On: 07/07/2015
       */
         public static DataSet POLOG_Get_PO_Preview(int? SUPPLY_ID, string PO_Type, int? Created_By)
         {
             return DAL_POLOG_Register.POLOG_Get_PO_Preview(SUPPLY_ID, PO_Type, Created_By);
         }
          public static DataSet POLOG_Get_Type(int? UserID,string FilterType)
            {
                return DAL_POLOG_Register.POLOG_Get_Type(UserID, FilterType);
            }
          public static DataSet POLOG_Get_CharterParty(int? Vessel_ID)
          {
              return DAL_POLOG_Register.POLOG_Get_CharterParty(Vessel_ID);
          }
          public static DataSet POLOG_Get_AccountClassification(int? UserID,string PO_Type)
          {
              return DAL_POLOG_Register.POLOG_Get_AccountClassification(UserID, PO_Type);
          }
            public static DataSet POLOG_Get_Item_Details(int? ID, int? Supply_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_Item_Details(ID, Supply_ID);
            }
            public static DataSet POLOG_Get_Item_List(int? SUPPLY_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_Item_List(SUPPLY_ID);
            }
            public static int POLOG_Delete_Item(int ItemID, int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Delete_Item(ItemID, UserID);
            }
            public static int POLOG_Delete_PO(int? SUPPLY_ID, string POStatus, int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Delete_PO(SUPPLY_ID, POStatus, UserID);
            }
            public static int POLOG_Update_Amount(int? Supply_ID, decimal? discount, string DiscountType, decimal? DiscountAmount, string Hidetext, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Update_Amount(Supply_ID, discount, DiscountType, DiscountAmount, Hidetext, CreatedBy);
            }
            public static int POLOG_Insert_Update_PO(int? ID, string POType, int? Supply_ID, string POReferance
                , string ShipReferance, int? VesselID, string Port, string ETA, string Urgency, string CurrChange, string SuppRef
                , string Remarks, string CurrencyID, string Agent_Code, string AccountType,
                string AccClassifictaion, string Supplier_Code
                , string Owner_Code, string CharterParty, int? VerificationID, int? Approval, string Action_On_Data_Form, string Status, int? Created_By,string Port_Call_ID)
            {

                return DAL_POLOG_Register.POLOG_Insert_Update_PO(ID, POType, Supply_ID, POReferance, ShipReferance
                    , VesselID, Port, ETA, Urgency, CurrChange, SuppRef, Remarks
                    , CurrencyID, Agent_Code, AccountType, AccClassifictaion, Supplier_Code, Owner_Code
                    , CharterParty, VerificationID, Approval, Action_On_Data_Form, Status, Created_By, Port_Call_ID);
            }

            public static int POLOG_Insert_Update_POItem(int? ID, int? Supply_ID, string ItemCode, string ItemName, string Packing, decimal? Quantity, decimal? Price,
                 decimal? Discount, string Remarks, decimal? Total, int? Created_By)
            {

                return DAL_POLOG_Register.POLOG_Insert_Update_POItem(ID, Supply_ID, ItemCode, ItemName, Packing, Quantity, Price, Discount, Remarks, Total, Created_By);
            }
           

            public static int POLOG_Insert_Update_POItem(int? Supply_ID, DataTable dtExtraItems, int? Created_By)
            {

                return DAL_POLOG_Register.POLOG_Insert_Update_POItem(Supply_ID, dtExtraItems, Created_By);
            }
            public static int POLOG_Insert_Remarks(int? RemarksID, int? Supply_ID,  string Remarks,string Remarks_Type, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Insert_Remarks(RemarksID, Supply_ID, Remarks,Remarks_Type, CreatedBy);
            }
            public static int POLOG_Update_Remarks(int? RemarksID, int? Supply_ID, string Remarks_Action, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Update_Remarks(RemarksID, Supply_ID, Remarks_Action, CreatedBy);
            }
            public static DataTable POLOG_Current_Currency_Exchangerate(string Currency)
            {
                return DAL_POLOG_Register.POLOG_Current_Currency_Exchangerate(Currency);
            }
            public static DataTable POLOG_Get_Remarks(int? RemarksID,int? PO_ID,int Type,string Remarks_Type)
            {
                return DAL_POLOG_Register.POLOG_Get_Remarks(RemarksID, PO_ID, Type, Remarks_Type);
            }
            public static DataTable POLOG_Get_TransactionLog(string PO_Code)
            {
                return DAL_POLOG_Register.POLOG_Get_TransactionLog(PO_Code);
            }
            public static int POLOG_Delete_Remarks(int? RemarksID, int? Created_By)
            {
                return DAL_POLOG_Register.POLOG_Delete_Remarks(RemarksID, Created_By);
            }
            public static DataSet POLOG_Get_ApprovalDeatils(int? PO_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_ApprovalDeatils(PO_ID);
            }
            public static DataTable POLOG_Get_Attachments(string ID, string DOCType)
            {
                return DAL_POLOG_Register.POLOG_Get_Attachments(ID, DOCType);
            }
            public static string POLOG_Insert_AttachedFile(string ID, string File_Type, string File_Name, string File_Path, string Docs_Type, int created_By)
            {
                return DAL_POLOG_Register.POLOG_Insert_AttachedFile(ID, File_Type, File_Name, File_Path, Docs_Type, created_By);
            }
            public static int POLOG_Delete_Attachments(string FileID, string ID, int created_By)
            {
                return DAL_POLOG_Register.POLOG_Delete_Attachments(FileID, ID, created_By);
            }
            public static DataSet POLOG_Get_Verifier_Approver(string AccClassifictaion, string POType, decimal? Po_Used_Value,int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Get_Verifier_Approver(AccClassifictaion, POType, Po_Used_Value, UserID);
            }
            public static int POLOG_Send_For_Approval(int? Supply_ID, int? ApprovalID, string POStatus, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Send_For_Approval(Supply_ID, ApprovalID, POStatus, CreatedBy);
            }
            public static DataTable POLOG_Get_Pending_Approval(int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Get_Pending_Approval(UserID);
            }

            public static DataSet POLOG_Get_Supplier_Deatils(int? Supply_ID,  int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Get_Supplier_Deatils(Supply_ID,  CreatedBy);
            }
            
            public static int POLOG_Update_PODeatils(int? Supply_ID, string Type, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Update_PODeatils(Supply_ID, Type, CreatedBy);
            }
           
            public static DataTable POLOG_Get_Supplier(String SupplierType)
            {
                return DAL_POLOG_Register.POLOG_Get_Supplier(SupplierType);
            }
            public static int POLOG_Insert_Transactionlog(string Code, string Action, string Description, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Insert_Transactionlog(Code, Action, Description, CreatedBy);
            }
            public static DataTable POLOG_Get_Port_Call(string Delivery_ID, int? Supply_ID, int? Vessel_ID, int Type)
            {
                return DAL_POLOG_Register.POLOG_Get_Port_Call(Delivery_ID, Supply_ID, Vessel_ID, Type);
            }
            public static int POLOG_Update_PODeatils(int? Supply_ID, string Type, string CloseDate,  int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Update_PODeatils(Supply_ID, Type, CloseDate,  CreatedBy);
            }


            public static int POLOG_Update_PO_Admin(int? Supply_ID, int? Vessel_ID, string Supplier_Code, int? Terms, string CurrencyID, string IssueBy, string CharterParty
                             , string AccClassifictaion, string AccountType, string POType
                             , string Owner_Code, string Payment_Type, string Hide, string Remarks, string Action_On_Data_Form, string Status, int? Created_By)
            {

                return DAL_POLOG_Register.POLOG_Update_PO_Admin(Supply_ID, Vessel_ID, Supplier_Code
                    , Terms, CurrencyID, IssueBy, CharterParty, AccClassifictaion, AccountType, POType
                    , Owner_Code, Payment_Type, Hide, Remarks, Action_On_Data_Form, Status, Created_By);
            }

            public static int POLOG_Calculate_Outstanding(int? Supply_ID, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLOG_Calculate_Outstanding(Supply_ID, CreatedBy);
            }
           //Invoice Approval

            public static DataSet POLOG_Get_Pending_Invoice_Search(string Supplier_Code, int? VesselID, string InvoiceStatus,string Urgent, DataTable dt, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Pending_Invoice_Search(Supplier_Code, VesselID, InvoiceStatus,Urgent, dt, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            //public static DataTable POLOG_Get_Approved_Invoice_Search(string Supplier_Code, int? VesselID, string InvoiceStatus, DataTable dt, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            //{
            //    return DAL_POLOG_Register.POLOG_Get_Approved_Invoice_Search(Supplier_Code, VesselID, InvoiceStatus, dt, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            //}
            public static int POLog_Update_Invoice(string InvoiceID, int? Supply_ID, int? ReworkUserID, string InvStatus, string Status, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLog_Update_Invoice(InvoiceID, Supply_ID, ReworkUserID, InvStatus, Status, CreatedBy);
            }
            public static int POLog_Insert_Withhold_Invoice(string InvoiceID, int? Supply_ID, int? ReworkUserID, string Invoice_Type, decimal? Invoice_Amount, string Reason, int? CreatedBy, string Withhold_Mode,string Invoice_Currency)
            {
                return DAL_POLOG_Register.POLog_Insert_Withhold_Invoice(InvoiceID, Supply_ID, ReworkUserID, Invoice_Type, Invoice_Amount, Reason, CreatedBy, Withhold_Mode, Invoice_Currency);
            }
            public static int POLog_Update_Invoice(DataTable dtInvoice, string InvStatus, string Status, int? CreatedBy)
            {
                return DAL_POLOG_Register.POLog_Update_Invoice(dtInvoice, InvStatus, Status, CreatedBy);
            }
            public static DataSet POLOG_Get_SupplierPO(string PO_Code,string Type)
            {
                return DAL_POLOG_Register.POLOG_Get_SupplierPO(PO_Code, Type);
            }
            public static DataSet POLOG_Get_Remarks_ByInvoiceID(string Invoice_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_Remarks_ByInvoiceID(Invoice_ID);
            }
            public static DataTable POLOG_Get_Invoice(int? Supply_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_Invoice(Supply_ID);
            }
            public static DataTable POLOG_Get_DuplicateInvoice(string Invoice_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_DuplicateInvoice(Invoice_ID);
            }
            public static DataTable POLOG_Get_Delivery_Invoice(string Invoice_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_Delivery_Invoice(Invoice_ID);
            }

            public static DataSet POLOG_Get_Supplier_InvoiceWise(int? UserID, string SearchType)
            {
                return DAL_POLOG_Register.POLOG_Get_Supplier_InvoiceWise(UserID, SearchType);
            }

            //Payment Approval

          
            public static DataSet POLOG_Get_Approved_Invoice_Search(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk,string InvoiceStatus,string InvoiceWorkflow, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Approved_Invoice_Search(Supplier_Code, Vessel_Code, Owner_Code, UrgentChk,InvoiceStatus,InvoiceWorkflow, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataTable POLOG_Get_Approved_Payment_Invoice_Search(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk,  string InvoiceStatus,string PaymentStatus, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Approved_Payment_Invoice_Search(Supplier_Code, Vessel_Code, Owner_Code, UrgentChk, InvoiceStatus,PaymentStatus, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataSet POLOG_Get_Withhold_Invoice_Search(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, string InvoiceStatus,  int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Withhold_Invoice_Search(Supplier_Code, Vessel_Code, Owner_Code, UrgentChk, InvoiceStatus, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataTable POLOG_Get_Approved_Invoice_Count(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Get_Approved_Invoice_Count(Supplier_Code, Vessel_Code, Owner_Code, UrgentChk, UserID);
            }

            public static DataSet POLOG_Get_Payment_Schedule_Amount(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Get_Payment_Schedule_Amount(Supplier_Code, Vessel_Code,Owner_Code,UrgentChk ,UserID);
            }
            public static int POLog_Update_Payment_Invoice(string InvoiceID, string InvStatus, string Status, int? CreatedBy,DataTable dt)
            {
                return DAL_POLOG_Register.POLog_Update_Payment_Invoice(InvoiceID, InvStatus, Status, CreatedBy, dt);
            }
            //Payment Processing
            public static DataSet POLOG_Get_Payment_Supplier_Invoice(string SupplierCode, string Paymode, int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Get_Payment_Supplier_Invoice(SupplierCode, Paymode, UserID);
            }
         
            public static DataTable POLOG_Get_Payment_Processing_Invoice_Search(string Currency, int AutoChk, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Payment_Processing_Invoice_Search(Currency, AutoChk, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataSet POLOG_Get_Payment_Details(string Payment_ID, string Supplier_Code, int? Payment_Year, int UserID)
            {
                return DAL_POLOG_Register.POLOG_Get_Payment_Details(Payment_ID,Supplier_Code,Payment_Year, UserID);
            }
            public static string POLOG_Insert_Payment_Details(string Payment_ID, int Payment_Year, string Supplier_Code, decimal? PaymentAmount,string BankRef,
                                                              DateTime? PayDate,string PaymentMode,string Account,decimal? BankAmt,decimal? BankCharge,string Payment_Status,string Remarks, int UserID)
            {
                return DAL_POLOG_Register.POLOG_Insert_Payment_Details(Payment_ID, Payment_Year, Supplier_Code, PaymentAmount, BankRef, PayDate, PaymentMode, Account, BankAmt, BankCharge, Payment_Status, Remarks, UserID);
            }
            public static string POLOG_Link_Payment_Invoice(string PaymodeID, string Supplier_Code, int? Payment_Year, DataTable dt, int? UserID)
            {
                return DAL_POLOG_Register.POLOG_Link_Payment_Invoice(PaymodeID, Supplier_Code, Payment_Year, dt, UserID);
            }
            public static string POLOG_Delete_Payment_Invoice(string Payment_ID, string Supplier_Code, int? Payment_Year, int UserID)
            {
                return DAL_POLOG_Register.POLOG_Delete_Payment_Invoice(Payment_ID, Supplier_Code, Payment_Year, UserID);
            }

            //Batch_Payment Update
            public static string POLOG_INS_Batch_Payment(string Payment_ID, string Supplier_Code, string PaymentType, string PaymentCurrency,
                                                                string BankName, string Country, string State, string SwiftCode, string ABANumber, string BankCode, string BranchCode, string AccountNumber, string Beneficiary, string PaymentAccount, string PayMode, int? UserID, string Record_Status)
            {
                return DAL_POLOG_Register.POLOG_INS_Batch_Payment(Payment_ID, Supplier_Code, PaymentType, PaymentCurrency, BankName, Country, State, SwiftCode, ABANumber, BankCode, BranchCode, AccountNumber, Beneficiary, PaymentAccount, PayMode, UserID, Record_Status);
            }
            public static DataTable POLOG_Get_Batch_Payment_Setup_Search(string SearchText, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Batch_Payment_Setup_Search(SearchText, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataSet POLOG_Get_Batch_Payment_Setup(string Payment_ID)
            {
                return DAL_POLOG_Register.POLOG_Get_Batch_Payment_Setup(Payment_ID);
            }
            public static int POLOG_DEL_Batch_Payment_Setup(string Payment_ID,int UserID)
            {
                return DAL_POLOG_Register.POLOG_DEL_Batch_Payment_Setup(Payment_ID, UserID);
            }
            //  Purchase Report
            public static DataSet POLOG_Get_Purchase_Report(string Supplier_Code, DataTable dtVessel, DataTable dtAccountType, DataTable dtAccClassification, string POStatus, DateTime FromDate, DateTime ToDate, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Purchase_Report(Supplier_Code, dtVessel, dtAccountType, dtAccClassification, POStatus, FromDate, ToDate, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataSet POLOG_Get_Invoice_Summary_Report(string Supplier_Code, int? Vessel_ID, string Owner_Code, string Status, string UnpaidInvoiceStatus, string InvoiceStatus, DateTime FromDate, DateTime ToDate, DateTime FromPayment, DateTime ToPayment, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Invoice_Summary_Report(Supplier_Code, Vessel_ID, Owner_Code, Status, UnpaidInvoiceStatus, InvoiceStatus, FromDate, ToDate, FromPayment, ToPayment, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            public static DataSet POLOG_Get_Stale_PO_Report(string Supplier_Code, int? Vessel_ID, string Ageing, string MyView, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
            {
                return DAL_POLOG_Register.POLOG_Get_Stale_PO_Report(Supplier_Code, Vessel_ID, Ageing, MyView, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
    }
}
