using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.ASL;
using SMS.Data.ASL.ASL;


namespace SMS.Business.ASL
{
    public class BLL_ASL_Lib
    {
        DAL_ASL_Lib objASLDAL = new DAL_ASL_Lib();


        public int INS_ASL_Group_Item(DataTable dt, int CreatedBy)
        {
            return objASLDAL.INS_ASL_Group_Item(dt, CreatedBy);
        }
        public DataTable SupplierScope_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objASLDAL.SupplierScope_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int Supplier_Group_Insert(int? GroupID, string GroupName, int CreatedBy)
        {
            return objASLDAL.Supplier_Group_Insert(GroupID, GroupName, CreatedBy);
        }
        public int Supplier_Group_Delete(int GroupID,  int CreatedBy)
        {
            return objASLDAL.Supplier_Group_Delete(GroupID,  CreatedBy);
        }
        public DataTable Get_SupplierScope_List(int? ID)
        {
            return objASLDAL.Get_SupplierScope_List(ID);
        }
        public DataSet ASL_Supplier_ColumnGroup_List(int UserID)
        {
            return objASLDAL.ASL_Supplier_ColumnGroup_List(UserID);
        }
        public DataTable Get_Supplier_Group_List(int GroupID, int UserID)
        {
            return objASLDAL.Get_Supplier_Group_List(GroupID,UserID);
        }
      
        public int InsertSupplierScope(string SupplierScope, int CreatedBy)
        {
            return objASLDAL.InsertSupplierScope(SupplierScope, CreatedBy);
        }

        public int EditSupplierScope(int ID, string SupplierScope, int CreatedBy)
        {
            return objASLDAL.EditSupplierScope(ID, SupplierScope, CreatedBy);
        }

        public int DeleteSupplierScope(int ID, int CreatedBy)
        {
            return objASLDAL.DeleteSupplierScope(ID, CreatedBy);
        }

        public DataTable SupplierService_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objASLDAL.SupplierService_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_SupplierService_List(int? ID)
        {
            return objASLDAL.Get_SupplierService_List(ID);
        }

        public int InsertSupplierService(string SupplierService, int CreatedBy)
        {
            return objASLDAL.InsertSupplierService(SupplierService, CreatedBy);
        }

        public int EditSupplierService(int ID, string SupplierService, int CreatedBy)
        {
            return objASLDAL.EditSupplierService(ID, SupplierService, CreatedBy);
        }

        public int DeleteSupplierService(int ID, int CreatedBy)
        {
            return objASLDAL.DeleteSupplierService(ID, CreatedBy);
        }
        public DataTable SupplierApprover_Search(string SearchText, string Group_Name, string Approver_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objASLDAL.SupplierApprover_Search(SearchText, Group_Name, Approver_Type, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_SupplierApprover_List(int? ID)
        {
            return objASLDAL.Get_SupplierApprover_List(ID);
        }

        public string InsertSupplierApprover(int? ApproveID, int Approver, int FinalApprover, int CreatedBy, string ApproverType, string Group_Name)
        {
            return objASLDAL.InsertSupplierApprover(ApproveID, Approver, FinalApprover, CreatedBy, ApproverType, Group_Name);
        }

        public string EditSupplierApprover(int? ID, int? ApproveID, int Approver, int FinalApprover, int CreatedBy, string ApproverType, string Group_Name)
        {
            return objASLDAL.EditSupplierApprover(ID, ApproveID, Approver, FinalApprover, CreatedBy, ApproverType, Group_Name);
        }

        public int DeleteSupplierApprover(int ID, int CreatedBy)
        {
            return objASLDAL.DeleteSupplierApprover(ID, CreatedBy);
        }
        public DataSet Get_Supplier_ApproverUserList(int? UserID)
        {
            return objASLDAL.Get_Supplier_ApproverUserList(UserID);
        }
        public DataTable SearchEmailTemplate(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objASLDAL.SearchEmailTemplate(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public int InsertASL_EmailTemplate(string EmailStatus, string EmailBody, string EmailSubject, int CreatedBy)
        {
            return objASLDAL.InsertASL_EmailTemplate_DL(EmailStatus, EmailBody, EmailSubject, CreatedBy);
        }

        public int EditASL_EmailTemplate(int ID, string EmailStatus, string EmailBody, string EmailSubject, int CreatedBy)
        {
            return objASLDAL.EditASL_EmailTemplate_DL(ID, EmailStatus, EmailBody, EmailSubject, CreatedBy);

        }
        public DataTable Get_ASL_EmailTemplateList(int? EmailTemplateId)
        {
            return objASLDAL.Get_ASL_EmailTemplateList_DL(EmailTemplateId);
        }
        public int DeleteEmailTemplate(int EmailTemplateID, int CreatedBy)
        {
            return objASLDAL.DeleteVesselType_DL(EmailTemplateID, CreatedBy);

        }
        public DataSet Get_SupplierColumnGroupDetails(int UserID)
        {
            return objASLDAL.Get_SupplierColumnGroupDetails(UserID);
        }
    }
}
