using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using SMS.Data.POLOG;

namespace SMS.Business.POLOG
{
    public class BLL_POLOG_Lib
    {
        public static DataSet Get_Approval_Group_Item(string Group_Code, string PO_Type)
        {
            return DAL_POLOG_Lib.Get_Approval_Group_Item(Group_Code, PO_Type);
        }
        public static DataSet POLOG_Get_Approval_Group_User(string Group_Code, string UserType)
        {
            return DAL_POLOG_Lib.POLOG_Get_Approval_Group_User(Group_Code, UserType);
        }
        public static DataTable Get_Approval_Group_List(string Group_Code)
        {
            return DAL_POLOG_Lib.Get_Approval_Group_List(Group_Code);
        }

        public static DataSet Get_Approval_Group(string Group_Name, string PO_Type, string Acct, int UserBy, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Lib.Get_Approval_Group(Group_Name, PO_Type, Acct, UserBy, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable Get_Approval_Group_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Lib.Get_Approval_Group_Search(searchText, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static int INS_Approval_Group_Item(string Group_Code, DataTable dtAcc, DataTable dtUser, int? CreatedBy)
        {
            return DAL_POLOG_Lib.INS_Approval_Group_Item(Group_Code, dtAcc, dtUser, CreatedBy);
        }
        public static string INS_Approval_Group(string Group_Code, string Group_Name, string PO_Type, int? Created_By)
        {
            return DAL_POLOG_Lib.INS_Approval_Group(Group_Code, Group_Name, PO_Type, Created_By);
        }

        public static int Upd_Approval_Group(string Group_Code, string Group_Name, string Po_Type, int Created_By)
        {
            return DAL_POLOG_Lib.Upd_Approval_Group_DL(Group_Code, Group_Name, Po_Type, Created_By);
        }

        public static int Del_Approval_Group(string Group_Code, int Created_By)
        {
            return DAL_POLOG_Lib.Del_Approval_Group(Group_Code, Created_By);
        }
        public static DataSet POLOG_Get_Approval_Limit(string Limit_ID)
        {
            return DAL_POLOG_Lib.POLOG_Get_Approval_Limit(Limit_ID);
        }
        public static DataSet POLOG_Get_Approval_Limit(string Limit_ID, string Group_Code)
        {
            return DAL_POLOG_Lib.POLOG_Get_Approval_Limit(Limit_ID, Group_Code);
        }
        public static string POLOG_INS_Approval_Limit(string LimitID, string Group_Code, int? AdvancUserID, decimal? MinApprovalLimit, decimal? ApprovalLimit, int? UserID)
        {
            return DAL_POLOG_Lib.POLOG_INS_Approval_Limit(LimitID, Group_Code, AdvancUserID, MinApprovalLimit, ApprovalLimit, UserID);
        }
        public static DataTable POLOG_Get_Approval_Limit_Search(string searchText, string Group_Code, int? USER_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Lib.POLOG_Get_Approval_Limit_Search(searchText, Group_Code, USER_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable POLOG_Get_Approval_Limit_Search(string Group_Code, int? USER_ID)
        {
            return DAL_POLOG_Lib.POLOG_Get_Approval_Limit_Search(Group_Code, USER_ID);
        }
        public static int POLOG_Del_Approval_Limit(string Group_Code, int? UserID)
        {
            return DAL_POLOG_Lib.POLOG_Del_Approval_Limit(Group_Code, UserID);
        }
        public static string POLOG_INS_Approval_Limit_Approver(string LimitID, DataTable dtApprover, int? UserID)
        {
            return DAL_POLOG_Lib.POLOG_INS_Approval_Limit_Approver(LimitID, dtApprover, UserID);
        }

        public static DataTable POLOG_Get_Approval_Setting_Report(string PO_Type, string Acct, int UserID, string Group_Code, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_POLOG_Lib.POLOG_Get_Approval_Setting_Report(PO_Type, Acct, UserID, Group_Code, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
    }
}
