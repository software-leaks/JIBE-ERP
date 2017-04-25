using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.CP;


namespace SMS.Business.CP
{
    public class BLL_CP_HireInvoice
    {

        DAL_CP_HireInvoice objDALHireInv = new DAL_CP_HireInvoice();

        public int INS_Hire_Invoice(int? Charter_ID, int? StatusId, string Status, string Bank_Account_ID, string InvoiceRef, double? Address_Commision,double? Billed_Amount, DateTime? Billing_Period_From, DateTime? Billing_Period_To, DateTime? Hire_Inv_Date, DateTime? Due_Date, string Remarks, int Created_By)
        {
           return objDALHireInv.INS_Hire_Invoice(Charter_ID,StatusId, Status,  Bank_Account_ID, InvoiceRef, Address_Commision, Billed_Amount, Billing_Period_From,  Billing_Period_To,  Hire_Inv_Date,  Due_Date,  Remarks,  Created_By );
        }

        public int UPD_Hire_Invoice(int? Hire_Invoice_Id, string InvoiceRef, int? StatusId, string Status, string Bank_Account_ID, double? Address_Commision, double? Billed_Amount, double? Received_Amount, DateTime? Billing_Period_From, DateTime? Billing_Period_To, DateTime? Hire_Inv_Date, DateTime? Due_Date, string Remarks, int Created_By)
        {
            return objDALHireInv.UPD_Hire_Invoice(Hire_Invoice_Id,InvoiceRef, StatusId, Status, Bank_Account_ID, Address_Commision,Received_Amount, Billed_Amount, Billing_Period_From, Billing_Period_To, Hire_Inv_Date, Due_Date, Remarks, Created_By);
        }


        public DataTable GET_Hire_InvStatusList()
        {
            return objDALHireInv.GET_Hire_InvStatusList();
        }
        public DataTable GET_Hire_Invoice_Item_Group_ALL()
        {
            return objDALHireInv.GET_Hire_Invoice_Item_Group_ALL();
        }


        public int INS_UPD_Inv_Items(int? Hire_Invoice_Id, DataTable dtInvitems, int? Created_By)
        {
            return objDALHireInv.INS_UPD_Inv_Items(Hire_Invoice_Id, dtInvitems, Created_By);
        }
        public DataTable Get_Hire_InvALL(int? Charter_ID, DataTable dtStatusIDs, DataTable dtPaymentStatus)
        {
            return objDALHireInv.Get_Hire_InvALL(Charter_ID, dtStatusIDs, dtPaymentStatus);
        }

        public DataTable GET_Hire_Invoice_Items(int? Charter_ID, int? Hire_Invoice_Id)
        {
            return objDALHireInv.GET_Hire_Invoice_Items(Charter_ID, Hire_Invoice_Id);
        }
        public DataTable Get_Hire_InvPrep(int? Charter_ID)
        {
            return objDALHireInv.Get_Hire_InvPrep(Charter_ID);
        }

        public DataTable Get_Hire_InvDetail(int? Hire_Invoice_Id)
        {
            return objDALHireInv.Get_Hire_InvDetail(Hire_Invoice_Id);
        }

        public int Delete_Hire_Invoice_Items(int? Created_By, int? Hire_Invoice_Item_Id)
        {
            return objDALHireInv.Delete_Hire_Invoice_Items(Created_By, Hire_Invoice_Item_Id);
        }
        public DataTable Get_Hire_Matching_Inv(int CPID, bool HideMatched)
        {
            return objDALHireInv.Get_Hire_Matching_Inv(CPID, HideMatched);
        }
        public DataTable Get_Transaction_Matching_Inv(int CPID, bool HideMatched)
        {
            return objDALHireInv.Get_Transaction_Matching_Inv(CPID, HideMatched);
        }

        public DataTable Get_Invoice_Received(int CPID, bool HideMatched)
        {
            return objDALHireInv.Get_Invoice_Received(CPID, HideMatched);
        }


        public DataTable Get_Hire_OffSet_Inv(int Type, string ID)
        {
            return objDALHireInv.Get_Hire_OffSet(Type, ID);
        }

        public int Upd_Inv_Approve(int CPID, int In_Type, string Invoice_Id, int Created_By)
        {
            return objDALHireInv.Upd_Inv_Approve( CPID,In_Type, Invoice_Id, Created_By);
        }

        public int Upd_Outstandingremarks(int Inv_ID, string Remarks, int Created_By)
        {
            return objDALHireInv.Upd_Outstandingremarks(Inv_ID, Remarks, Created_By);
        }
        public DataTable GetOwnerBank_Deatils(string Bank_Account_ID)
        {
            return objDALHireInv.GetOwnerBank_Deatils(Bank_Account_ID);
        }

        public int Upd_MatchAmt(int Inv_ID, double Match_Amount, string TranType, int Charter_Id, string Inv_Ref, string Remittance_Id_Ref, int Created_By)
        {
            return objDALHireInv.Upd_MatchAmt(Inv_ID,Match_Amount,TranType,Charter_Id,Inv_Ref,Remittance_Id_Ref,  Created_By);
        }
    }
}
