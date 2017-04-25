using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.POLOG;

namespace SMS.Business.POLOG
{
    public  class BLL_POLOG_Invoice
    {


        public static DataSet POLOG_Get_Online_Invoice(string searchtext, int? Supply_ID, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Online_Invoice(searchtext, Supply_ID, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataSet POLOG_Get_PedingOnline_Invoice(string searchtext, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Invoice.POLOG_Get_PedingOnline_Invoice(searchtext, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataSet POLOG_Get_Invoice_List(string searchtext, int? Supply_ID, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Invoice_List(searchtext, Supply_ID, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static int POLog_Delete_Invoice(string Invoice_ID, int? Supply_ID, int? CreatedBy)
        {
            return DAL_POLOG_Invoice.POLog_Delete_Invoice(Invoice_ID, Supply_ID, CreatedBy);
        }
        public static DataSet POLOG_Get_Invoice_Details(string Invoice_ID, int UserID)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Invoice_Details(Invoice_ID, UserID);
        }
        public static string POLog_Insert_Invoice(string Invoice_ID, int? Supply_ID, string Type, DateTime? InvoiceDate, string Referance, DateTime? ReceivedDate, decimal? InvoiceValue,
                                                       string Currency, decimal? GST, DateTime? DueDate, string InvStatus, DateTime? PaymentDate, string Remarks, string Status, int? CreatedBy)
        {
            return DAL_POLOG_Invoice.POLog_Insert_Invoice(Invoice_ID, Supply_ID, Type, InvoiceDate, Referance, ReceivedDate, InvoiceValue, Currency, GST, DueDate, InvStatus, PaymentDate, Remarks, Status, CreatedBy);
        }
        public static DataSet POLOG_Get_Invoice_Attachments(string Invoice_ID, string DOCType)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Invoice_Attachments(Invoice_ID, DOCType);
        }
        public static DataSet POLOG_Get_Rework_Invoice(int CreatedBy, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Rework_Invoice(CreatedBy,  sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        
        public static DataSet Get_Invoice_UserList(int CreatedBy)
        {
            return DAL_POLOG_Invoice.Get_Invoice_UserList(CreatedBy);
        }
        public static DataSet POLOG_Get_Supplier_Deatils(int? Supply_ID, int? CreatedBy)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Supplier_Deatils(Supply_ID, CreatedBy);
        }
        public static void POLOG_Insert_Mail_Details(string Subject, string MailTo, string CC, string Body, int CreatedBy)
        {
            DAL_POLOG_Invoice.POLOG_Insert_Mail_Details(Subject, MailTo, CC, Body, CreatedBy);
        }
        public static int POLog_Update_CTM_Invoice(string InvoiceID, string InvStatus, int? CreatedBy)
        {
            return DAL_POLOG_Invoice.POLog_Update_CTM_Invoice(InvoiceID, InvStatus, CreatedBy);
        }
        public static int POLog_Revoke_Payment_Invoice(string InvoiceID, string RevokeType, int? CreatedBy)
        {
            return DAL_POLOG_Invoice.POLog_Revoke_Payment_Invoice(InvoiceID, RevokeType, CreatedBy);
        }
        public static DataSet POLOG_Get_Invoice_Transfer_Cost(string InvoiceID,int? Supply_ID, int Created_By)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Invoice_Transfer_Cost(InvoiceID,Supply_ID, Created_By);
        }
        public static string POLOG_INS_Transfer_Cost(int Supply_ID, string Invoice_Id, int Created_By, string  Transfer_Id, string Status, double? Transfer_Amount, int vesselId, string desc, string AccountClassification, string OwnerCode)
        {
            return DAL_POLOG_Invoice.POLOG_INS_Transfer_Cost(Supply_ID, Invoice_Id, Created_By, Transfer_Id, Status, Transfer_Amount, vesselId, desc, AccountClassification, OwnerCode);
        }
        public static int POLOG_Del_Transfer_Cost(int Transfer_ID, int Created_By)
        {
            return DAL_POLOG_Invoice.POLOG_Del_Transfer_Cost(Transfer_ID, Created_By);
        }
        public static DataSet POLOG_Get_Transfer_Cost(string Transfer_ID, int Created_By)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Transfer_Cost(Transfer_ID, Created_By);
        }
        public static int POLOG_UPD_Transfer_Cost(string Transfer_Id, string Invoice_Id, int Supply_id, int Created_By)
        {
            return DAL_POLOG_Invoice.POLOG_UPD_Transfer_Cost(Transfer_Id, Invoice_Id, Supply_id, Created_By);
        }
        public static DataSet POLOG_Get_Final_Invoice_Search(string Supplier_Code, int? VesselID, string InvoiceStatus,  DataTable dt, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Invoice.POLOG_Get_Final_Invoice_Search(Supplier_Code, VesselID, InvoiceStatus, dt, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
      
        public static DataSet Get_Account_Classification(string SupplyId, string Invoiceid)
        {
            return DAL_POLOG_Invoice.Get_Account_Classification(SupplyId, Invoiceid);
        }
      
        public static int POLOG_Update_Invoice_Data(string Action, string File_Id, string Invoice_Ref, string Invoice_Date, double Invoice_Amount, string Invoice_Due_date, string Period_From, string Period_to, string Invoice_Remarks, string Invoice_Flag, double Invoice_GST_Amount, string Supply_ID, string Invoice_Rejection_Flag, string Invoice_Rejection_Remark, int UpdatedBy)
        {
            return DAL_POLOG_Invoice.POLOG_Update_Invoice_Data(Action,File_Id,Invoice_Ref,Invoice_Date,Invoice_Amount,Invoice_Due_date,Period_From,Period_to,Invoice_Remarks,Invoice_Flag,Invoice_GST_Amount,Supply_ID,Invoice_Rejection_Flag,Invoice_Rejection_Remark,UpdatedBy);
        }
    }
}
