using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.POLOG
{
    public class DAL_POLOG_Register
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //private static string connection = "";
        public DAL_POLOG_Register(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_POLOG_Register()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
      

        public static DataTable POLOG_Get_PO_Search(string searchtext, string Supplier_Code, int? VesselID, string AccountClassification, string AccountType, string InvoiceStatus, DataTable dtType, string chkClosed, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                   new System.Data.SqlClient.SqlParameter("@AccountClassification", AccountClassification),
                     new System.Data.SqlClient.SqlParameter("@AccountType", AccountType),
                       new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                    new System.Data.SqlClient.SqlParameter("@dtType", dtType),
                     new System.Data.SqlClient.SqlParameter("@chkClosed", chkClosed),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_PO_Search", obj);
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_PO_Search_New", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataSet POLOG_Get_PO_Deatils(int? SUPPLY_ID, string PO_Type, int? Created_By)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", SUPPLY_ID),
                new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_PO_Deatils", obj);
        }
        /*
      * Method: To Get records for PO Preview
      * Parameter: SUPPLY_ID, PO_Type Created_By user
      * Created By: Alok
      * Created On: 07/07/2015
      */
        public static DataSet POLOG_Get_PO_Preview(int? SUPPLY_ID, string PO_Type, int? Created_By)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", SUPPLY_ID),
                new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_PO_Preview_Deatils", obj);
        }
        public static DataSet POLOG_Get_Type(int? UserID, string FilterType)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@FilterType", FilterType),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Type", obj);
        }
        public static DataSet POLOG_Get_CharterParty(int? Vessel_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_CharterParty", obj);
        }
        public static DataSet POLOG_Get_AccountClassification(int? UserID, string PO_Type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                 new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Account_Classification", obj);
        }
        public static DataSet POLOG_Get_Item_Details(int? ID, int? Supply_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID",ID),
                    new System.Data.SqlClient.SqlParameter("@Supply_ID",Supply_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Item_Deatils", obj);
            return ds;
        }
        public static DataSet POLOG_Get_Item_List(int? SUPPLY_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@SUPPLY_ID",SUPPLY_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Item_List", obj);
            return ds;
        }
        public static int POLOG_Delete_Item(int ItemID, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ItemID",ItemID),
                    new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Item", obj);
            //return ds;
        }
        public static int POLOG_Delete_PO(int? SUPPLY_ID, string POStatus, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@SUPPLY_ID",SUPPLY_ID),
                new System.Data.SqlClient.SqlParameter("@POStatus",POStatus),
                    new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_PO", obj);
            //return ds;
        }
        public static int POLOG_Insert_Update_POItem(int? ID, int? Supply_ID, string ItemCode, string ItemName, string Packing, decimal? Quantity, decimal? Price,
                 decimal? Discount, string Remarks, decimal? Total, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@ItemCode", ItemCode),
                 new System.Data.SqlClient.SqlParameter("@ItemName", ItemName),
                 new System.Data.SqlClient.SqlParameter("@Packing", Packing),
                 new System.Data.SqlClient.SqlParameter("@Quantity", Quantity),
                 new System.Data.SqlClient.SqlParameter("@Price", Price),
                 new System.Data.SqlClient.SqlParameter("@Discount", Discount),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@Total", Total),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_INS_UPD_PO_Item", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLOG_Insert_Update_POItem(int? Supply_ID, DataTable dtExtraItems, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@dtExtraItems", dtExtraItems),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_UPD_PO_Item", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLOG_Update_Amount(int? Supply_ID, decimal? discount, string DiscountType, decimal? DiscountAmount, string Hidetext, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@discount", discount),
                 new System.Data.SqlClient.SqlParameter("@DiscountType", DiscountType),
                 new System.Data.SqlClient.SqlParameter("@DiscountAmount", DiscountAmount),
                 new System.Data.SqlClient.SqlParameter("@Hidetext", Hidetext),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_Amount", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static int POLOG_Insert_Update_PO(int? ID, string POType, int? Supply_ID, string POReferance
                , string ShipReferance, int? VesselID, string Port, string ETA, string Urgency, string CurrChange, string SuppRef
                , string Remarks, string CurrencyID, string Agent_Code, string AccountType,
                string AccClassifictaion, string Supplier_Code
                , string Owner_Code, string CharterParty, int? VerificationID, int? Approval, string Action_On_Data_Form, string Status, int? Created_By, string Port_Call_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@POType", POType ),					 
			 
               new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID ),				 
               new System.Data.SqlClient.SqlParameter("@POReferance", POReferance ),	
			   new System.Data.SqlClient.SqlParameter("@ShipReferance", ShipReferance ),
               new System.Data.SqlClient.SqlParameter("@VesselID", VesselID ),				 
               new System.Data.SqlClient.SqlParameter("@Port", Port ),		
               new System.Data.SqlClient.SqlParameter("@ETA", ETA ),			
               new System.Data.SqlClient.SqlParameter("@Urgency", Urgency ),	
               new System.Data.SqlClient.SqlParameter("@CurrChange", CurrChange ),				 
               new System.Data.SqlClient.SqlParameter("@SuppRef",SuppRef  ),				 
               new System.Data.SqlClient.SqlParameter("@Remarks",Remarks  ),				 
               new System.Data.SqlClient.SqlParameter("@CurrencyID", CurrencyID ),				 
               new System.Data.SqlClient.SqlParameter("@Agent_Code",Agent_Code  ),					 
               new System.Data.SqlClient.SqlParameter("@AccountType",AccountType  ),		 
               new System.Data.SqlClient.SqlParameter("@AccClassifictaion",AccClassifictaion  ),				 
               new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code ),				 
               new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code ),				 
               new System.Data.SqlClient.SqlParameter("@CharterParty",CharterParty  ),			  
               new System.Data.SqlClient.SqlParameter("@VerificationID",VerificationID  ),			 
               new System.Data.SqlClient.SqlParameter("@ApprovalID", Approval ),		
		       new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form",Action_On_Data_Form  ),	
               new System.Data.SqlClient.SqlParameter("@Status",Status  ),	
		       new System.Data.SqlClient.SqlParameter("@Port_Call_ID",Port_Call_ID  ),
               new System.Data.SqlClient.SqlParameter("@Created_By",  Created_By),			 
               //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_PO", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataTable POLOG_Current_Currency_Exchangerate(string Currency)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Currency",Currency),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Current_ExchangeRate", obj);
            return ds.Tables[0];
        }
        public static DataTable POLOG_Get_Remarks(int? RemarksID, int? Supply_ID, int Type, string Remarks_Type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@RemarksID",RemarksID),
                    new System.Data.SqlClient.SqlParameter("@Supply_ID",Supply_ID),
                    new System.Data.SqlClient.SqlParameter("@Type",Type),
                    new System.Data.SqlClient.SqlParameter("@Remarks_Type",Remarks_Type),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Remarks", obj);
            return ds.Tables[0];
        }
        public static DataTable POLOG_Get_TransactionLog(string PO_Code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Code",PO_Code),
                   
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_TransactionLog", obj);
            return ds.Tables[0];
        }
        public static int POLOG_Insert_Remarks(int? RemarksID, int? Supply_ID, string Remarks, string Remarks_Type, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RemarksID", RemarksID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@Remarks_Type", Remarks_Type),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_INS_Remarks", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLOG_Update_Remarks(int? RemarksID, int? Supply_ID, string Remarks_Action, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RemarksID", RemarksID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Remarks_Action", Remarks_Action),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_UPD_Remarks", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataSet POLOG_Get_ApprovalDeatils(int? Supply_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Supply_ID",Supply_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_ApprovalLimit_Details", obj);
            return ds;
        }
        public static int POLOG_Delete_Remarks(int? RemarksID, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RemarksID", RemarksID),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Remarks", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataTable POLOG_Get_Attachments(string ID, string DOCType)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@DOCType", DOCType)
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Attachments", obj);
            return ds.Tables[0];
        }
        public static string POLOG_Insert_AttachedFile(string ID, string File_Type, string File_Name, string File_Path, string Docs_Type, int created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@File_Type", File_Type),
                 new System.Data.SqlClient.SqlParameter("@File_Name", File_Name),
                 new System.Data.SqlClient.SqlParameter("@File_Path", File_Path),
                 new System.Data.SqlClient.SqlParameter("@Docs_Type", Docs_Type),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", created_By),
            };
           
           return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Attachment", obj);
          
        }
        public static int POLOG_Delete_Attachments(string FileID, string ID, int created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@FileID", FileID),
                new System.Data.SqlClient.SqlParameter("@ID", ID),
                new System.Data.SqlClient.SqlParameter("@created_By", created_By),
                new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int),
                
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Attachment", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
          
        }
        public static DataSet POLOG_Get_Verifier_Approver(string AccClassifictaion, string POType, decimal? Po_Used_Value, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@AccClassifictaion",AccClassifictaion),
                    new System.Data.SqlClient.SqlParameter("@POType",POType),
                    new System.Data.SqlClient.SqlParameter("@Po_Used_Value",Po_Used_Value),
                     new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Verifier_Approver", obj);
            return ds;
        }

        public static int POLOG_Send_For_Approval(int? Supply_ID, int? ApprovalID, string POStatus, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@ApprovalID", ApprovalID),
                  new System.Data.SqlClient.SqlParameter("@POStatus", POStatus),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Send_For_Approval", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataTable POLOG_Get_Pending_Approval(int? UserID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Pending_Approval", obj).Tables[0];

        }
        public static int POLOG_Calculate_Outstanding(int? Supply_ID, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Calculate_Outstanding", obj);

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
        public static int POLOG_Update_PODeatils(int? Supply_ID, string Type,  int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Type", Type),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_UPD_PODetails", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLOG_Update_PO_Admin(int? Supply_ID, int? Vessel_ID, string Supplier_Code, int? Terms, string CurrencyID, string IssueBy, string CharterParty
                    , string AccClassifictaion, string AccountType, string POType
                    , string Owner_Code, string Payment_Type, string Hide, string Remarks, string Action_On_Data_Form, string Status, int? Created_By)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@Terms", Terms),
                 new System.Data.SqlClient.SqlParameter("@CurrencyID", CurrencyID),
                  new System.Data.SqlClient.SqlParameter("@IssueBy", IssueBy),
                   new System.Data.SqlClient.SqlParameter("@CharterParty", CharterParty),
                   new System.Data.SqlClient.SqlParameter("@AccClassifictaion", AccClassifictaion),
                    new System.Data.SqlClient.SqlParameter("@AccountType", AccountType),
                     new System.Data.SqlClient.SqlParameter("@POType", POType),
                     new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                     new System.Data.SqlClient.SqlParameter("@Payment_Type", Payment_Type),
                        new System.Data.SqlClient.SqlParameter("@Hide", Hide),
                         new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                     new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form),
                     new System.Data.SqlClient.SqlParameter("@Status", Status),
                     new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_UPD_PO_ByAdmin", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static int POLOG_Update_PODeatils(int? Supply_ID, string Type, string CloseDate,  int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@Type", Type),
                 new System.Data.SqlClient.SqlParameter("@CloseDate", CloseDate),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Close_PO_ByAdmin", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataTable POLOG_Get_Port_Call(string Delivery_ID, int? Supply_ID, int? Vessel_ID,int Type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Type", Type),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Port_Call", obj).Tables[0];
        }
        public static DataTable POLOG_Get_Supplier(string SupplierType)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@SupplierType",SupplierType),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Supplier_List", obj);
            return ds.Tables[0];
        }
        public static int POLOG_Insert_Transactionlog(string Code, string Action, string Description, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Code", Code),
                 new System.Data.SqlClient.SqlParameter("@Action", Action),
                 new System.Data.SqlClient.SqlParameter("@Description", Description),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_INS_TransactionLog", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        //Invoice Approval

        public static DataSet POLOG_Get_Pending_Invoice_Search(string Supplier_Code, int? VesselID, string InvoiceStatus,string Urgent, DataTable dt, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                   new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                   new System.Data.SqlClient.SqlParameter("@Urgent", Urgent),
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Pending_Invoice_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        
        public static DataSet POLOG_Get_Remarks_ByInvoiceID(string Invoice_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Invoice_ID",Invoice_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Remarks_InvoiceWise", obj);
            return ds;
        }
        public static DataSet POLOG_Get_SupplierPO(string PO_Code, string Type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@PO_Code",PO_Code),
                     new System.Data.SqlClient.SqlParameter("@Type",Type),
                   
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_SupplierPO", obj);
            return ds;
        }
        public static DataTable POLOG_Get_Invoice(int? Supply_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Supply_ID",Supply_ID),
                   
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice", obj);
            return ds.Tables[0];
        }
        public static DataTable POLOG_Get_DuplicateInvoice(string Invoice_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Invoice_ID",Invoice_ID),
                   
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Duplicate_Invoice", obj);
            return ds.Tables[0];
        }
        public static DataTable POLOG_Get_Delivery_Invoice(string Invoice_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Invoice_ID",Invoice_ID),
                   
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Delivery_InvoiceWise", obj);
            return ds.Tables[0];
        }
        public static int POLog_Update_Invoice(string InvoiceID, int? Supply_ID, int? ReworkUserID, string InvStatus, string Status, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@InvoiceID", InvoiceID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@ReworkUserID", ReworkUserID),
                 new System.Data.SqlClient.SqlParameter("@InvStatus", InvStatus),
                 new System.Data.SqlClient.SqlParameter("@Status", Status),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_UPD_Invoice", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLog_Insert_Withhold_Invoice(string InvoiceID, int? Supply_ID, int? ReworkUserID, string Invoice_Type, decimal? Invoice_Amount, string Reason, int? CreatedBy,string Withhold_Mode, string Invoice_Currency)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@InvoiceID", InvoiceID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@ReworkUserID", ReworkUserID),
                 new System.Data.SqlClient.SqlParameter("@Invoice_Type", Invoice_Type),
                 new System.Data.SqlClient.SqlParameter("@InvoiceAmount", Invoice_Amount),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Reason),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                new System.Data.SqlClient.SqlParameter("@Withhold_Mode", Withhold_Mode),
                new System.Data.SqlClient.SqlParameter("@Invoice_Currency", Invoice_Currency),
                new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_Insert_Withhold_Invoice", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLog_Update_Invoice(DataTable dtInvoice,  string InvStatus, string Status, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@dtInvoice", dtInvoice),
                 new System.Data.SqlClient.SqlParameter("@InvStatus", InvStatus),
                 new System.Data.SqlClient.SqlParameter("@Status", Status),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
            };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_Invoice_Status", obj);
        }
        public static DataSet POLOG_Get_Supplier_InvoiceWise(int? UserID, string SearchType)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                 new System.Data.SqlClient.SqlParameter("@SearchType", SearchType),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Supplier_InvoiceWise", obj);
        }


        //Payment Approval
        
        public static DataTable POLOG_Get_Approved_Invoice_Count(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, int? UserID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                   new System.Data.SqlClient.SqlParameter("@UrgentChk", UrgentChk),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                    
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_Approved_Count", obj);
            //isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataSet POLOG_Get_Approved_Invoice_Search(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, string InvoiceStatus, string InvoiceWorkflow, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                   new System.Data.SqlClient.SqlParameter("@UrgentChk", UrgentChk),
                   new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                   new System.Data.SqlClient.SqlParameter("@InvoiceWorkflow", InvoiceWorkflow),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approved_Invoice_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public static DataTable POLOG_Get_Approved_Payment_Invoice_Search(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk,  string InvoiceStatus,string PaymentStatus, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                   new System.Data.SqlClient.SqlParameter("@UrgentChk", UrgentChk),
                   
                   new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                   new System.Data.SqlClient.SqlParameter("@PaymentStatus", PaymentStatus),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approved_Payment_INV_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataSet POLOG_Get_Withhold_Invoice_Search(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, string InvoiceStatus,  int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                   new System.Data.SqlClient.SqlParameter("@UrgentChk", UrgentChk),
                   new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Withold_INV_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public static DataSet POLOG_Get_Payment_Schedule_Amount(string Supplier_Code, int? Vessel_Code, string Owner_Code, int UrgentChk, int? UserID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                   new System.Data.SqlClient.SqlParameter("@UrgentChk", UrgentChk),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Payment_Schedule_Amount", obj);
            return ds;
        }
        public static int POLog_Update_Payment_Invoice(string InvoiceID, string InvStatus, string Status, int? CreatedBy, DataTable dt)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@InvoiceID", InvoiceID),
                 new System.Data.SqlClient.SqlParameter("@InvStatus", InvStatus),
                 new System.Data.SqlClient.SqlParameter("@Status", Status),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("@dtInvoice", dt),
            };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_Payment_Invoice", obj);
        }

        
        //Payment Processing
        public static DataSet POLOG_Get_Payment_Supplier_Invoice(string Supplier_Code, string PayMode, int? Created_By)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@PayMode", PayMode),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Payment_Supplier_Invoice", obj);
        }
        public static DataTable POLOG_Get_Payment_Processing_Invoice_Search(string Currency, int AutoChk, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Currency", Currency),
                   new System.Data.SqlClient.SqlParameter("@AutoChk", AutoChk),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approved_Payment_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataSet POLOG_Get_Payment_Details(string Payment_ID, string Supplier_Code,int? Payment_Year, int? Created_By)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@Payment_ID", Payment_ID),
                new System.Data.SqlClient.SqlParameter("@Payment_Year", Payment_Year),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Payment_Details", obj);
        }

        public static string POLOG_Insert_Payment_Details(string Payment_ID, int Payment_Year, string Supplier_Code, decimal? PaymentAmount, string BankRef,
                                                              DateTime? PayDate, string PaymentMode, string Account, decimal? BankAmt, decimal? BankCharge, string Payment_Status, string Remarks, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Payment_ID", Payment_ID),
                new System.Data.SqlClient.SqlParameter("@Payment_Year", Payment_Year),
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@PaymentAmount", PaymentAmount),
                new System.Data.SqlClient.SqlParameter("@BankRef", BankRef),
                new System.Data.SqlClient.SqlParameter("@PayDate", PayDate),
                new System.Data.SqlClient.SqlParameter("@PaymentMode", PaymentMode),
                new System.Data.SqlClient.SqlParameter("@Account", Account),
                new System.Data.SqlClient.SqlParameter("@BankAmt", BankAmt),
                new System.Data.SqlClient.SqlParameter("@BankCharge", BankCharge),
                new System.Data.SqlClient.SqlParameter("@Payment_Status", Payment_Status),
                new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                new System.Data.SqlClient.SqlParameter("@Created_By", UserID),
            };
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_Payment_Record", obj);
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_UPD_Payment_Record", obj);
        }
        public static string POLOG_Link_Payment_Invoice(string PaymodeID, string Supplier_Code, int? Payment_Year, DataTable dtInvoice, int? UserID)
        {
           
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Payment_ID", PaymodeID),
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@Payment_Year", Payment_Year),
                new System.Data.SqlClient.SqlParameter("@dt", dtInvoice),
                new System.Data.SqlClient.SqlParameter("@Created_By", UserID),
            };
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_UPD_Payment_Invoice_Record", obj);
           
        }
        public static string POLOG_Delete_Payment_Invoice(string Payment_ID, string Supplier_Code, int? Payment_Year, int UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@Payment_ID", Payment_ID),
                new System.Data.SqlClient.SqlParameter("@Payment_Year", Payment_Year),
                new System.Data.SqlClient.SqlParameter("@Created_By", UserID),
            };

            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_DEL_Payment_Record", obj);
        }

        //Batch Payment Setup
        public static string POLOG_INS_Batch_Payment(string Payment_ID, string Supplier_Code, string PaymentType, string PaymentCurrency,
                                                                string BankName, string Country, string State, string SwiftCode, string ABANumber, string BankCode, string BranchCode, string AccountNumber, string Beneficiary, string PaymentAccount, string PayMode, int? UserID, string Record_Status)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Payment_ID", Payment_ID),
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@PaymentType", PaymentType),
                new System.Data.SqlClient.SqlParameter("@PaymentCurrency", PaymentCurrency),
                new System.Data.SqlClient.SqlParameter("@Country", Country),
                new System.Data.SqlClient.SqlParameter("@State", State),
                new System.Data.SqlClient.SqlParameter("@SwiftCode", SwiftCode),
                new System.Data.SqlClient.SqlParameter("@ABANumber", ABANumber),
                new System.Data.SqlClient.SqlParameter("@BankCode", BankCode),
                new System.Data.SqlClient.SqlParameter("@BranchCode", BranchCode),
                new System.Data.SqlClient.SqlParameter("@AccountNumber", AccountNumber),
                 new System.Data.SqlClient.SqlParameter("@Beneficiary", Beneficiary),
                  new System.Data.SqlClient.SqlParameter("@PaymentAccount", PaymentAccount),
                   new System.Data.SqlClient.SqlParameter("@PayMode", PayMode),
                new System.Data.SqlClient.SqlParameter("@Created_By", UserID),
                new System.Data.SqlClient.SqlParameter("@Record_Status", Record_Status),
            };
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_UPD_Batch_Payment_Setup", obj);
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_UPD_Payment_Record", obj);
        }
        public static DataTable POLOG_Get_Batch_Payment_Setup_Search(string Supplier_Name,  int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", Supplier_Name),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Batch_Payment_Setup_List", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public static DataSet POLOG_Get_Batch_Payment_Setup(string Payment_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Payment_Mode_ID", Payment_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Batch_Payment_Setup", obj);
        }

        public static int POLOG_DEL_Batch_Payment_Setup(string Payment_ID, int? created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Payment_Mode_ID", Payment_ID),
                new System.Data.SqlClient.SqlParameter("@UserID", created_By),
                new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int),
                
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Batch_Payment_Setup", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);

        }
        public static DataSet POLOG_Get_Purchase_Report(string Supplier_Code, DataTable dtVessel, DataTable dtAccountType, DataTable dtAccClassification, string POStatus, DateTime FromDate, DateTime ToDate, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                    new System.Data.SqlClient.SqlParameter("@dtVessel", dtVessel),
                     new System.Data.SqlClient.SqlParameter("@dtAccountType", dtAccountType),
                      new System.Data.SqlClient.SqlParameter("@dtAccClassification", dtAccClassification),
                       new System.Data.SqlClient.SqlParameter("@POStatus", POStatus),
                        new System.Data.SqlClient.SqlParameter("@FromDate", FromDate),
                         new System.Data.SqlClient.SqlParameter("@ToDate", ToDate),
                          new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Purchase_Report", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
          public static DataSet POLOG_Get_Invoice_Summary_Report(string Supplier_Code, int? Vessel_ID, string Owner_Code, string POStatus, string UnpaidInvoiceStatus, string InvoiceStatus, DateTime FromDate, DateTime ToDate, DateTime FromPayment, DateTime ToPayment, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                    new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                     new System.Data.SqlClient.SqlParameter("@Owner_Code", Owner_Code),
                     new System.Data.SqlClient.SqlParameter("@POStatus", POStatus),
                       new System.Data.SqlClient.SqlParameter("@UnpaidInvoiceStatus", UnpaidInvoiceStatus),
                       new System.Data.SqlClient.SqlParameter("@InvoiceStatus", InvoiceStatus),
                        new System.Data.SqlClient.SqlParameter("@FromDate", FromDate),
                         new System.Data.SqlClient.SqlParameter("@ToDate", ToDate),
                           new System.Data.SqlClient.SqlParameter("@FromPayment", FromPayment),
                         new System.Data.SqlClient.SqlParameter("@ToPayment", ToPayment),
                          new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Invoice_Summary_Report", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public static DataSet POLOG_Get_Stale_PO_Report(string Supplier_Code, int? Vessel_ID, string Ageing, string MyView, int? UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                    new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                     new System.Data.SqlClient.SqlParameter("@Ageing", Ageing),
                     new System.Data.SqlClient.SqlParameter("@MyView", MyView),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Stale_PO_Report", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
   
    }
}
