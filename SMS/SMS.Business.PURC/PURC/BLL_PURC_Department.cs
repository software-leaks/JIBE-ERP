using System;
using System.Data;
using System.Configuration;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;


/// <summary>
/// Summary description for Department_PMS
/// </summary>
/// 

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_Department objDept = new DAL_PURC_Department();



        public DataTable Department_Search(string searchtext, string Form_Type, string Ac_Classi_Code , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDept.Department_Search(searchtext !="" ?searchtext:null, Form_Type !="" ? Form_Type : null , null , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

      


        public int SaveDepartment(DepartmentData objDODepartment)
        {

            return objDept.SaveDepartment(objDODepartment);


        }

        public DataTable GetAccount()
        {

            return objDept.GetAccount();

        }



        public int DeleteDepartment(DepartmentData objDODepartment)
        {

            return objDept.DeleteDepartment(objDODepartment);



        }


        public int EditDepartment(DepartmentData objDODepartment)
        {

            return objDept.EditDepartment(objDODepartment);



        }

        public DataTable SelectDepartment()
        {

            return objDept.SelectDepartment();


        }

    

        public DataTable GetDepartmentMaster()
        {


            return objDept.GetDepartmentMaster();


        }

        public DataTable SelectDepartmentType()
        {


            return objDept.SelectDepartmentType();

        }
        public int AssingAccountDept(string DeptCode, string CatlgCode, string SubCatlgCode, string AccCode)
        {

            return objDept.AssingAccountDept(DeptCode, CatlgCode, SubCatlgCode, AccCode);
        }

        public DataTable GetAccountDetails()
        {

            return objDept.GetAccountDetails();

        }

        public DataTable GetAccountClassification()
        {
            return objDept.GetAccountClassification();
        }

        public DataTable CheckExist(string DeptCode, string CatlgCode, string SubCatlgCode)
        {

            return objDept.CheckExist(DeptCode, CatlgCode, SubCatlgCode);

        }
        public int UpdateAccount(string ID, string AccCode)
        {


            return objDept.UpdateAccount(ID, AccCode);


        }
        public DataTable GetDeptType()
        {

            return objDept.GetDeptType();


        }

        public DataTable GetApprovalCode()
        {
            return objDept.GetApprovalCode();
        }

        // Req_Type Po_Type mapping-------------------------

        public DataTable POType_Search(int? Req_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDept.POType_Search(Req_Type, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public DataSet GetPOType()
        {
            return objDept.GetPOType();
        }
        public int SavePOType(int? Req_Type, int? BudgetCode, string PO_Type, string Account_Type, int UserID)
        {
            return objDept.SavePOType(Req_Type, BudgetCode, PO_Type, Account_Type, UserID);
        }
        public int UpdatePOType(int? ID, int? Req_Type, int? BudgetCode, string PO_Type, string Account_Type, int UserID)
        {
            return objDept.UpdatePOType(ID, Req_Type, BudgetCode, PO_Type, Account_Type, UserID);
        }
        public int DeletePOType(int? ID, int UserID)
        {
            return objDept.DeletePOType(ID, UserID);
        }
        public DataTable Get_Req_POType(int? ID)
        {
            return objDept.Get_Req_POType(ID);
        }
        public DataSet Get_Email_System_Parameter(string Parent_Short_Code,int UserID)
        {
            return objDept.Get_Email_System_Parameter(Parent_Short_Code, UserID);
        }
        public DataTable Search_Email_Template_List(string Email_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDept.Search_Email_Template_List(Email_Type, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int PURC_Ins_Email_Template(string Email_Type, string EmailBody, string EmailSubject, int CreatedBy)
        {
            return objDept.PURC_Ins_Email_Template(Email_Type, EmailBody, EmailSubject, CreatedBy);
        }

        public int PURC_UPD_Email_Template(int ID, string Email_Type, string EmailBody, string EmailSubject, int CreatedBy)
        {
            return objDept.PURC_UPD_Email_Template(ID, Email_Type, EmailBody, EmailSubject, CreatedBy);
        }
        public DataTable PURC_Get_Email_Template(int? ID)
        {
            return objDept.PURC_Get_Email_Template(ID);
        }
        public int PURC_Del_Email_Template(int ID, int CreatedBy)
        {
            return objDept.PURC_Del_Email_Template(ID, CreatedBy);

        }

    }
}
