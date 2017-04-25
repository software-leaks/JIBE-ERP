using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace SMS.Data.POLOG
{
    public class DAL_POLOG_Invoice
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //private static string connection = "";
        public DAL_POLOG_Invoice(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_POLOG_Invoice()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public static DataSet POLOG_Get_Online_Invoice(string searchtext, int? Supply_ID, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Searchtext", searchtext),
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Online_Invoice", obj);
        }
        public static DataSet POLOG_Get_Invoice_List(string searchtext, int? Supply_ID, int UserID,string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Searchtext", searchtext),
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_List", obj);
        }

        public static string POLog_Insert_Invoice(string Invoice_ID, int? Supply_ID, string Type, DateTime? InvoiceDate, string Referance, DateTime? ReceivedDate, decimal? InvoiceValue,
                                                        string Currency, decimal? GST, DateTime? DueDate, string InvStatus, DateTime? PaymentDate, string Remarks, string Status, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_ID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Type", Type),
                 new System.Data.SqlClient.SqlParameter("@InvoiceDate", InvoiceDate),
                 new System.Data.SqlClient.SqlParameter("@Referance", Referance),
                 new System.Data.SqlClient.SqlParameter("@ReceivedDate", ReceivedDate),
                 new System.Data.SqlClient.SqlParameter("@InvoiceValue", InvoiceValue),
                 new System.Data.SqlClient.SqlParameter("@Currency", Currency),
                 new System.Data.SqlClient.SqlParameter("@GST", GST),
                 new System.Data.SqlClient.SqlParameter("@DueDate", DueDate),
                 new System.Data.SqlClient.SqlParameter("@InvStatus", InvStatus),
                 new System.Data.SqlClient.SqlParameter("@PaymentDate", PaymentDate),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@Status", Status),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Invoice", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static int POLog_Delete_Invoice(string Invoice_ID, int? Supply_ID, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_ID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Invoice", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataSet POLOG_Get_Invoice_Details(string Invoice_ID, int UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_ID),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_Deatils", obj);
        }

        public static DataSet POLOG_Get_Invoice_Attachments(string Invoice_ID, string DOCType)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_ID),
                 new System.Data.SqlClient.SqlParameter("@DOCType", DOCType)
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_Attachments", obj);
            return ds;
        }
        public static DataSet POLOG_Get_Rework_Invoice(int CreatedBy, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Rework_Invoice", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
     
        public static DataSet Get_Invoice_UserList(int CreatedBy)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy)
                
                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_UserList",obj);
        }
        public static DataSet POLOG_Get_Supplier_Deatils(int? Supply_ID, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Supplier_Details", obj);
        }
        public static void POLOG_Insert_Mail_Details(string Subject, string MailTo, string CC, string Body, int CreatedBy)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Subject", Subject),
                 new System.Data.SqlClient.SqlParameter("@MailTo", MailTo),
                  new System.Data.SqlClient.SqlParameter("@CC", CC),
                   new System.Data.SqlClient.SqlParameter("@Body", Body),
                    new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy)
            };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Insert_Mail_Details", obj);
        }
        public static int POLog_Update_CTM_Invoice(string InvoiceID, string InvStatus, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@InvoiceID", InvoiceID),
                 new System.Data.SqlClient.SqlParameter("@InvStatus", InvStatus),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
            };
            
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_CTM_Invoice", obj);
        }
        public static int POLog_Revoke_Payment_Invoice(string InvoiceID, string RevokeType, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@InvoiceID", InvoiceID),
                 new System.Data.SqlClient.SqlParameter("@RevokeType", RevokeType),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
            };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_CTM_Invoice", obj);
        }
        public static string POLOG_INS_Transfer_Cost(int Supply_ID, string Invoice_Id, int Created_By, string Transfer_Id, string Status, double? Transfer_Amount, int vesselId, string desc, string AccountClassification, string OwnerCode)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_Id),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("@Transfer_ID", Transfer_Id),
                 new System.Data.SqlClient.SqlParameter("@Status", Status),
                   new System.Data.SqlClient.SqlParameter("@Transfer_Amount", Transfer_Amount),
                   new System.Data.SqlClient.SqlParameter("@VesselId", vesselId),
                   new System.Data.SqlClient.SqlParameter("@Description", desc),
                   new System.Data.SqlClient.SqlParameter("@AccountClassification", AccountClassification),
                   new System.Data.SqlClient.SqlParameter("@OwnerCode", OwnerCode),
                  
                
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
           return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Transfer_Cost", obj);
            //return (obj[obj.Length - 1].Value).ToString();
        }
        public static int POLOG_Del_Transfer_Cost(int Transfer_ID, int Created_By)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("@Transfer_ID", Transfer_ID)
                
                
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Del_Transfer_Cost", obj);
        }

        public static DataSet POLOG_Get_Transfer_Cost(string Transfer_ID, int Created_By)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("@Transfer_ID", Transfer_ID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Transfer_Cost", obj);
        }
        public static int POLOG_UPD_Transfer_Cost(string Transfer_Id, string Invoice_Id, int Supply_id, int Created_By)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                 new System.Data.SqlClient.SqlParameter("@Transfer_ID", Transfer_Id),
                   new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_Id),
                     new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_id),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By)
                
                
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_UPD_Transfer_Cost", obj);
        }
        public static DataSet POLOG_Get_Invoice_Transfer_Cost(string Invoice_Id, int? Supply_ID, int Created_By)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@Invoice_ID", Invoice_Id),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_Transfer_Cost", obj);
        }
        public static DataSet POLOG_Get_Final_Invoice_Search(string Supplier_Code, int? VesselID, string InvoiceStatus, DataTable dt, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                   new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                   new System.Data.SqlClient.SqlParameter("@dt", dt),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_PO_Search", obj);
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Final_Invoice_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public static DataSet Get_Account_Classification(string SupplyId, string InvoiceId)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@SupplyId",SupplyId),
                 new System.Data.SqlClient.SqlParameter("@InvoiceId",InvoiceId)
                
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_Amount", obj);

        }


        public static DataSet POLOG_Get_PedingOnline_Invoice(string searchtext, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Searchtext", searchtext),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Pending_Online_Invoice", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
       
        public static int POLOG_Update_Invoice_Data(string Action, string File_Id, string Invoice_Ref, string Invoice_Date, double Invoice_Amount, string Invoice_Due_date, string Period_From, string Period_to, string Invoice_Remarks, string Invoice_Flag, double Invoice_GST_Amount, string Supply_ID, string Invoice_Rejection_Flag, string Invoice_Rejection_Remark, int UpdatedBy)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                 new System.Data.SqlClient.SqlParameter("@Action", Action),
                 new System.Data.SqlClient.SqlParameter("@File_ID", File_Id),
                 new System.Data.SqlClient.SqlParameter("@Invoice_Reference", Invoice_Ref),
             new System.Data.SqlClient.SqlParameter("@Invoice_Date", Invoice_Date),
             new System.Data.SqlClient.SqlParameter("@Invoice_Amount", Invoice_Amount),
             new System.Data.SqlClient.SqlParameter("@Invoice_Due_date", Invoice_Due_date),
             new System.Data.SqlClient.SqlParameter("@Period_From", Period_From),
            new System.Data.SqlClient.SqlParameter("@Period_To", Period_to),
             new System.Data.SqlClient.SqlParameter("@Invoice_Remarks", Invoice_Remarks),
             new System.Data.SqlClient.SqlParameter("@Invoice_Flag", Invoice_Flag),
              new System.Data.SqlClient.SqlParameter("@Invoice_GST_Amount", Invoice_GST_Amount),
               new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
               new System.Data.SqlClient.SqlParameter("@Invoice_Rejection_Flag", Invoice_Rejection_Flag),
               new System.Data.SqlClient.SqlParameter("@Invoice_Rejection_Remark", Invoice_Rejection_Remark),
               new System.Data.SqlClient.SqlParameter("@UpdatedBy", UpdatedBy)
                
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Get_Online_Invoice_Data", obj);
        }
       
    }
}
