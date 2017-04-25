using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_Port
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Approval_Limit
    {
        DAL_Infra_Approval_Limit objApprLimit = new DAL_Infra_Approval_Limit();

        public BLL_Infra_Approval_Limit()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable Get_Approval_Limit_List(int ID)
        {
            return objApprLimit.Get_Approval_Limit_List_DL(ID);
        }

        public DataTable Get_Approval_Limit_Search(string searchText, int? GROUP_ID, int? USER_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objApprLimit.Get_Approval_Limit_Search(searchText, GROUP_ID,USER_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int INS_Approval_Limit(int? Group_ID, string Approval_Type, DataTable dt, int Created_By)
        {
            return objApprLimit.INS_Approval_Limit_DL(Group_ID, Approval_Type, dt, Created_By);
        }
        public int Upd_Approval_Limit(int? ID, int? Group_ID, int? USER_ID, decimal? MAX_APPROVAL_LIMIT, int Created_By)
        {
            return objApprLimit.Upd_Approval_Limit_DL(ID, Group_ID, USER_ID, MAX_APPROVAL_LIMIT, Created_By);
        }
        public int Upd_Approval_Limit(int? ID, int? Group_ID, int? USER_ID, decimal? MAX_APPROVAL_LIMIT, decimal? MAX_Invoice_Limit, int PO_Approver, int Invoice_Approver, int Final_Invoice_Approver, int Advance_Approver, int Created_By)
        {
            return objApprLimit.Upd_Approval_Limit_DL(ID, Group_ID, USER_ID, MAX_APPROVAL_LIMIT, MAX_Invoice_Limit, PO_Approver, Invoice_Approver, Final_Invoice_Approver, Advance_Approver, Created_By);
        }

        public int Del_Approval_Limit(int? ID, int? Group_ID, int UserID)
        {
            return objApprLimit.Del_Approval_Limit_DL(ID, Group_ID,UserID);
        }


        public DataTable Get_Approval_Group()
        {
            return objApprLimit.Get_Approval_Group();
        }
        public DataTable Get_UserListApprovalLimit(string ApprovalType, int Group_ID)
        {
            try
            {
                return objApprLimit.Get_UserListApprovalLimit_DL(ApprovalType, Group_ID);
            }
            catch
            {
                throw;
            }
        }
        public int CheckAmountApplicable(string ApprovalType, int AmountApplicable)
        {
            try
            {
                return objApprLimit.CheckAmountApplicable_DL(ApprovalType, AmountApplicable);
            }
            catch
            {
                throw;
            }
        }

        
    }
}
