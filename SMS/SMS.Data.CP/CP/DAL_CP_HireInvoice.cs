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
    public class DAL_CP_HireInvoice
    {
        
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
       
        public DAL_CP_HireInvoice(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_CP_HireInvoice()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }



        public int INS_Hire_Invoice(int? Charter_ID, int? StatusId,string Status, string Bank_Account_ID, string InvoiceRef, double? Address_Commision, double? Billed_Amount, DateTime? Billing_Period_From, DateTime? Billing_Period_To, DateTime? Hire_Inv_Date, DateTime? Due_Date, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Charter_Party_ID", Charter_ID),
                                        new SqlParameter("@StatusId", StatusId),
                                        new SqlParameter("@Inv_Status", Status),       
                                        new SqlParameter("@Bank_Account_ID", Bank_Account_ID),
                                        new SqlParameter("@InvoiceRef", InvoiceRef),
                                        new SqlParameter("@Billed_Amount", Billed_Amount),
                                        new SqlParameter("@Address_Commision", Address_Commision),
                                        new SqlParameter("@Billing_Period_From", Billing_Period_From),
                                        new SqlParameter("@Billing_Period_To", Billing_Period_To),
                                        new SqlParameter("@Hire_Inv_Date", Hire_Inv_Date),
                                        new SqlParameter("@Due_Date", Due_Date),
                                        new SqlParameter("@Remarks", Remarks),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_Hire_Invoice", sqlprm);
        }


        public int UPD_Hire_Invoice(int? Hire_Invoice_Id, string InvoiceRef, int? StatusId,string Status, string Bank_Account_ID, double? Address_Commision, double? Received_Amount, double? Billed_Amount, DateTime? Billing_Period_From, DateTime? Billing_Period_To, DateTime? Hire_Inv_Date, DateTime? Due_Date, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@ID", Hire_Invoice_Id),
                                        new SqlParameter("@Hire_Invoice_No", InvoiceRef),
                                        new SqlParameter("@StatusId", StatusId),
                                        new SqlParameter("@Inv_Status", Status),   
                                        new SqlParameter("@Bank_Account_ID", Bank_Account_ID),
                                        new SqlParameter("@Received_Amount", Received_Amount),
                                        new SqlParameter("@Billed_Amount", Billed_Amount),
                                        new SqlParameter("@Address_Commision", Address_Commision),
                                        new SqlParameter("@Billing_Period_From", Billing_Period_From),
                                        new SqlParameter("@Billing_Period_To", Billing_Period_To),
                                        new SqlParameter("@Hire_Inv_Date", Hire_Inv_Date),
                                        new SqlParameter("@Due_Date", Due_Date),
                                        new SqlParameter("@Remarks", Remarks),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_UPD_Hire_Invoice", sqlprm);
        }


        public int INS_UPD_Inv_Items(int? Hire_Invoice_Id, DataTable dtInvitems, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Hire_Inv_ID", Hire_Invoice_Id),
                                        new SqlParameter("@dtExtraItems", dtInvitems),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_INS_UPD_Inv_Items", sqlprm);
        }


        public DataTable GET_Hire_InvStatusList()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Hire_InvStatusList").Tables[0];
        }

        public DataTable GET_Hire_Invoice_Item_Group_ALL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_DTL_Hire_Invoice_Item_Group_ALL").Tables[0];
        }


        public DataTable Get_Hire_InvALL(int? Charter_Party_ID, DataTable dtStatusIDs, DataTable dtPaymentStatus)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_Party_ID",Charter_Party_ID),
                                            new SqlParameter("@StatusIDs", dtStatusIDs),
                                            new SqlParameter("@PaymentStatus",dtPaymentStatus)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Hire_InvALL", sqlprm).Tables[0];
        }


        public DataTable Get_Hire_InvPrep(int? Charter_Party_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_Party_ID",Charter_Party_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Hire_InvPrep", sqlprm).Tables[0];
        }


        public DataTable GET_Hire_Invoice_Items(int? Charter_ID, int? ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_Party_ID",Charter_ID),
                                             new SqlParameter("@ID", ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GET_Hire_Invoice_Items", sqlprm).Tables[0];
        }


        public DataTable Get_Hire_InvDetail(int? Hire_Invoice_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID", Hire_Invoice_Id)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Hire_InvDetail", sqlprm).Tables[0];
        }

        public int Delete_Hire_Invoice_Items(int? Created_By, int? Hire_Invoice_Item_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Created_By", Created_By),
                                             new SqlParameter("@Hire_Invoice_Item_Id", Hire_Invoice_Item_Id)
                                            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_Delete_Hire_Invoice_Items", sqlprm);
        }

        public DataTable Get_Hire_Matching_Inv(int CPID, bool HideMatched)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_Party_ID",CPID),
                                            new SqlParameter("@HideMatched",HideMatched)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Hire_Matching_Inv", sqlprm).Tables[0];
        }
        public DataTable Get_Transaction_Matching_Inv(int CPID, bool HideMatched)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",CPID),
                                            new SqlParameter("@HideMatched",HideMatched)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Remittance_Received", sqlprm).Tables[0];
        }


        public DataTable Get_Invoice_Received(int CPID, bool HideMatched)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Charter_ID",CPID),
                                            new SqlParameter("@HideMatched",HideMatched)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Invoice_Received", sqlprm).Tables[0];
        }


        public DataTable Get_Hire_OffSet(int OffsetType, string ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Type",OffsetType),
                                            new SqlParameter("@ID",ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_Get_Hire_OffSet", sqlprm).Tables[0];
        }

        public int Upd_Inv_Approve(int CPID, int In_Type, string Invoice_Id, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                         new SqlParameter("@Charter_ID",CPID),
                                        new SqlParameter("@In_Type", In_Type),
                                        new SqlParameter("@Invoice_Id", Invoice_Id),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_Upd_Inv_Approve", sqlprm);
        }


        public int Upd_Outstandingremarks(int Inv_ID,  string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Inv_ID", Inv_ID),
                                        new SqlParameter("@Remarks", Remarks),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_Upd_Outstandingremarks", sqlprm);
        }

        public int Upd_MatchAmt(int Inv_ID, double Match_Amount, string TranType, int Charter_Id, string Inv_Ref, string Remittance_Id_Ref,  int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                            new SqlParameter("@Match_Amount", Match_Amount),
                            new SqlParameter("@Type", TranType),
                            new SqlParameter("@Charter_Id", Charter_Id),
                            new SqlParameter("@Inv_Ref", Inv_Ref),
                            new SqlParameter("@Remittance_Id_Ref", Remittance_Id_Ref),
                            new SqlParameter("@Inv_ID", Inv_ID),
                            //new SqlParameter("@Remarks", Remarks),
                            new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_Upd_MatchAmt", sqlprm);
        }



        public DataTable GetOwnerBank_Deatils(string Bank_Account_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Bank_Account_Id",Bank_Account_ID)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_GetOwnerBank_Deatils", sqlprm).Tables[0];
        }
     
    }
}
