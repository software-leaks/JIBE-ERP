using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;
using SMS.Properties;

/// <summary>
/// Summary description for BLL_User_Credentials
/// </summary>

namespace SMS.Business.Infrastructure
{

    public class BLL_Infra_UserCredentials
    {
        DAL_Infra_UserCredentials objDal = new DAL_Infra_UserCredentials();
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public BLL_Infra_UserCredentials()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable Get_UserDetails(int userid)
        {
            try
            {

                return objDal.Get_UserDetails_DL(userid);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_UserDetails(string UserName)
        {
            try
            {
                return objDal.Get_UserDetails_DL(UserName);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_UserDetails(int userid, string FieldName)
        {
            try
            {

                return objDal.Get_UserDetails_DL(userid, FieldName);
            }
            catch
            {
                throw;
            }

        }

        public DataSet Get_UserCredentials(string userid, string password)
        {
            try
            {

                return objDal.Get_UserCredentials_DL(userid, password);
            }
            catch
            {
                throw;
            }

        }

        public UserAccess Get_UserAccessForPage(int UserID, string PageLink)
        {
            try
            {
                UserAccess objUserAccess = new UserAccess();

                if (UserID == 1)
                {
                    objUserAccess.IsAdmin = 1;
                    objUserAccess.Id = 1;
                    objUserAccess.UserId = 1;
                    objUserAccess.Menu_Code = 1;
                    objUserAccess.View = 1;
                    objUserAccess.Add = 1;
                    objUserAccess.Edit = 1;
                    objUserAccess.Delete = 1;
                    objUserAccess.Approve = 1;
                    objUserAccess.Admin = 1;
                    objUserAccess.Unverify = 1;
                    objUserAccess.Urgent = 1;
                    objUserAccess.Revoke = 1;
                    objUserAccess.Close = 1;
                    objUserAccess.Unclose = 1;
                }
                else
                {
                    DataTable dtUserAccess = objDal.Get_UserAccessForPage_DL(UserID, PageLink);


                    if (dtUserAccess.Rows.Count > 0)
                    {
                        objUserAccess.IsAdmin = 0;
                        objUserAccess.Id = int.Parse(dtUserAccess.Rows[0]["id"].ToString());
                        objUserAccess.UserId = int.Parse(dtUserAccess.Rows[0]["UserID"].ToString());
                        objUserAccess.Menu_Code = int.Parse(dtUserAccess.Rows[0]["Menu_Code"].ToString());
                        objUserAccess.View = int.Parse(dtUserAccess.Rows[0]["Access_View"].ToString());
                        objUserAccess.Add = int.Parse(dtUserAccess.Rows[0]["Access_Add"].ToString());
                        objUserAccess.Edit = int.Parse(dtUserAccess.Rows[0]["Access_Edit"].ToString());
                        objUserAccess.Delete = int.Parse(dtUserAccess.Rows[0]["Access_Delete"].ToString());
                        objUserAccess.Approve = int.Parse(dtUserAccess.Rows[0]["Access_Approve"].ToString());
                        if (dtUserAccess.Rows[0]["Access_Admin"].ToString().Trim().Length == 0)
                        {
                            objUserAccess.Admin = 0;
                        }
                        else
                        {
                            objUserAccess.Admin = int.Parse(dtUserAccess.Rows[0]["Access_Admin"].ToString());
                        }
                        objUserAccess.Unverify = int.Parse(dtUserAccess.Rows[0]["Unverify"].ToString());
                        objUserAccess.Urgent = int.Parse(dtUserAccess.Rows[0]["Urgent"].ToString());
                        objUserAccess.Revoke = int.Parse(dtUserAccess.Rows[0]["Revoke"].ToString());
                        objUserAccess.Close = int.Parse(dtUserAccess.Rows[0]["Close"].ToString());
                        objUserAccess.Unclose = int.Parse(dtUserAccess.Rows[0]["Unclose"].ToString());
                    }

                }
                return objUserAccess;
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_UserList()
        {
            try
            {
                return objDal.Get_UserList_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_UserList_ApprovalLimit()
        {
            try
            {
                return objDal.Get_UserList_ApprovalLimit_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_UserList_Search(string searchtext, int? companyid, int? deptid, int? managerid, int? activestatus,string usertype, int? techmgrid
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDal.Get_UserList_Search(searchtext, companyid, deptid, managerid, activestatus, usertype, techmgrid
                , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }





        public DataTable Get_UserList(int CompanyID)
        {
            try
            {
                return objDal.Get_UserList_DL(CompanyID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_UserList(int CompanyID, string FilterText)
        {
            try
            {
                if (FilterText == null)
                    FilterText = "";

                return objDal.Get_UserList_DL(CompanyID, FilterText);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_UserList(int CompanyID, string FilterText, int UserID)
        {
            try
            {
                if (FilterText == null)
                    FilterText = "";

                return objDal.Get_UserList_DL(CompanyID, FilterText, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Menu_Lib_Access(int UserID)
        {
            try
            {

                return objDal.Get_Menu_Lib_Access_DL(UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_UserList_By_Dept_DL(int CompanyID, int DeptID)
        {
            try
            {
                return objDal.Get_UserList_By_Dept_DL(CompanyID, DeptID.ToString());
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_UserList_By_Dept_DL(int CompanyID, string DeptIDcsv)
        {
            try
            {
                return objDal.Get_UserList_By_Dept_DL(CompanyID, DeptIDcsv);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_UserMenu(string username, string usrole)
        {
            try
            {
                return objDal.Get_UserMenu_DL(username, usrole);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public DataTable Get_UserQuickMenu(int UserID)
        {
            try
            {
                return objDal.Get_UserQuickMenu_DL(UserID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public DataTable Get_UserTypeList()
        {
            try
            {
                return objDal.Get_UserTypeList_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_UserTypeList(int usertypeId)
        {
            try
            {
                return objDal.Get_UserTypeList_DL(usertypeId);
            }
            catch
            {
                throw;
            }
        }

        public int CreateUser(UserProperties User)
        {
            //int CompanyID, string FName, string LName, DateTime DOB, string UserName, string PWD, string uType, string Designation, double ApprovalLimit, string PresentAddress, string PermanentAddress, DateTime DateOfJoining, DateTime DateOfProbation, int DepartmentID, string NetWkId, int FleetID
            return objDal.CreateUser_DL(User);
        }


        public int CreateUser(int Created_By, string First_Name, string Middle_Name, string Last_Name, string UserName, string Password, string NetWkId, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
         , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address
         , string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID)
        {

            return objDal.CreateUser_DL(Created_By, First_Name, Middle_Name, Last_Name, UserName, Password, NetWkId, MailID, DeptID, ManagerID, Approval_Limit, Designation, Mobile_Number
                , DOB, DOJ, DOP, Parmanent_Address, Present_Address, User_Type, CompanyID, Tech_ManagerID, SiteID);


        }
        public int CreateUser(int Created_By, string First_Name, string Middle_Name, string Last_Name, string UserName, string Password, string NetWkId, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
        , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address
        , string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID,int NationalityId)
        {

            return objDal.CreateUser_DL(Created_By, First_Name, Middle_Name, Last_Name, UserName, Password, NetWkId, MailID, DeptID, ManagerID, Approval_Limit, Designation, Mobile_Number
                , DOB, DOJ, DOP, Parmanent_Address, Present_Address, User_Type, CompanyID, Tech_ManagerID, SiteID, NationalityId);


        }



        public void Start_Session(int UserID, string SessionID, string UserIP, string ClientBrowser)
        {
            objDal.Start_Session_DL(UserID, SessionID, UserIP, ClientBrowser);
        }

        public void End_Session(int UserID)
        {
            objDal.End_Session_DL(UserID);
        }

        public DataTable Get_LoggedinUsers(int SessionUserID)
        {
            return objDal.Get_LoggedinUsers_DL(SessionUserID);
        }

        public DataTable Get_UserSessionLog(int SessionUserID, string StartDate, string EndDate)
        {
            DateTime Dt_StartDate = DateTime.Today.AddDays(-7);
            if (StartDate.Length > 0)
                Dt_StartDate = DateTime.Parse(StartDate);

            DateTime DT_EndDate = DateTime.Now;
            if (EndDate.Length > 0)
                DT_EndDate = DateTime.Parse(EndDate);

            return objDal.Get_UserSessionLog_DL(SessionUserID, Dt_StartDate, DT_EndDate);
        }

        public int Update_UserDetails(int UserID, string First_Name, string Middle_Name, string Last_Name, string MailID, int Dep_Code, int ManagerID, int Approval_Limit, string Designation, string Mobile_Number)
        {
            return objDal.Update_UserDetails_DL(UserID, First_Name, Middle_Name, Last_Name, MailID, Dep_Code, ManagerID, Approval_Limit, Designation, Mobile_Number);
        }



        public int Update_UserDetails(int UserID, string First_Name, string Middle_Name, string Last_Name, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
          , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address, string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID, string NetWkId)
        {
            return objDal.Update_UserDetails_DL(UserID, First_Name, Middle_Name, Last_Name, MailID, DeptID, ManagerID, Approval_Limit, Designation
                , Mobile_Number, DOB, DOJ, DOP, Parmanent_Address, Present_Address, User_Type, CompanyID, Tech_ManagerID, SiteID, NetWkId);

        }
        public int Update_UserDetails(int UserID, string First_Name, string Middle_Name, string Last_Name, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
          , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address, string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID, string NetWkId,int NationalityId)
        {
            return objDal.Update_UserDetails_DL(UserID, First_Name, Middle_Name, Last_Name, MailID, DeptID, ManagerID, Approval_Limit, Designation
                , Mobile_Number, DOB, DOJ, DOP, Parmanent_Address, Present_Address, User_Type, CompanyID, Tech_ManagerID, SiteID, NetWkId, NationalityId);

        }
        /// <summary>
        /// Description: Added a method to save the date format setting for logged in user.
        /// Created By: Krishnapriya
        /// </summary>
        /// <param name="UserID">Logged in User's ID will be sent as parameter</param>
        /// <param name="User_DateFormat">Selected date format will be sent as parameter</param>
        /// <returns></returns>
        public int Update_UserDetails(int UserID, string User_DateFormat)
        {
            return objDal.Update_UserDetails_DL(UserID, User_DateFormat);
        }

        public int Update_UserPassword(int UserID, string OldPassword, string NewPassword)
        {
            return objDal.Update_UserPassword_DL(UserID, OldPassword, NewPassword);
        }

        public int Delete_User(int UserID, int Deleted_By)
        {
            return objDal.Delete_User_DL(UserID, Deleted_By);
        }

        public DataTable Get_User_Vessel_Assignment(int Vessel_ID, string User_Name)
        {
            return objDal.Get_User_Vessel_Assignment_DL(Vessel_ID, User_Name);
        }

        public int INSERT_User_Vessel_Assignment(int User_ID, int Vessel_ID, int Created_By)
        {
            return objDal.INSERT_User_Vessel_Assignment_DL(User_ID, Vessel_ID, Created_By);
        }
        public int DELETE_User_Vessel_Assignment(int ID, int Deleted_By)
        {
            return objDal.DELETE_User_Vessel_Assignment_DL(ID, Deleted_By);
        }

        public DataTable Get_Fleet_By_UserID(int UserID)
        {
            return objDal.Get_Fleet_By_UserID_DL(UserID);
        }

        public DataTable Get_User_By_Manager(int UserID)
        {

            return objDal.Get_User_By_Manager_DL(UserID);
        }
        public string Get_User_Role(int UserID)
        {
            return objDal.Get_User_Role_DL(UserID);
        }

        public DataTable Get_Travel_Quotation_Acess()
        {
            return objDal.Get_Travel_Quotation_Acess_DL();
        }

        public int Insert_Travel_Quotation_Acess(int UserID, string OperationFlage, int CurrUserID)
        {
            return objDal.Insert_Travel_Quotation_Acess_DL(UserID, OperationFlage, CurrUserID);
        }

        public DataTable Get_Super_List()
        {
            return objDal.Get_Super_List_DL();
        }
        public DataTable Get_SuperAttendingVessel_List()
        {
            return objDal.Get_SuperAttendingVessel_List_DL();
        }
        public int UPDATE_SuperAttendingVessel(int SuperUserID, string OperationFlage, int UserID)
        {
            return objDal.UPDATE_SuperAttendingVessel_DL(SuperUserID, OperationFlage, UserID);
        }

        public int UPD_Reset_Password(string EmailID, string Enc_TempPassword, string TempPassword)
        {
            return objDal.UPD_Reset_Password(EmailID,Enc_TempPassword,TempPassword);
        }
        public DataTable Get_OfficeUser_List()
        {
            return objDal.Get_OfficeUser_List_DL();
        }
        public int UPDATE_InspectorAssignmentl(int SuperUserID, string OperationFlage, int UserID)
        {
            return objDal.UPDATE_InspectorAssignmentl_DL(SuperUserID, OperationFlage, UserID);
        }
        public DataTable Get_InspectorAssignment_List()
        {
            return objDal.Get_InspectorAssignment_List_DL();
        }

        public DataTable Get_User_By_CompanyID(int CompanyID)
        {

            return objDal.Get_User_By_CompanyID_DL(CompanyID);
        }

        public int Update_UserMaster(string UserID, string Passkey)
        {
            return objDal.Update_UserMaster(UserID, Passkey);
        }

        public DataTable Get_UserMaster(string PassKey, string UserName)
        {
            return objDal.Get_UserMaster(PassKey, UserName);
        }
        public int CRW_CHECK_Card_Approval(string CardType, int UserId)
        {
            return objDal.CRW_CHECK_Card_Approval_DL(CardType, UserId);
        }

        public DataTable INSP_Get_InspectorList()
        {
            try
            {
                return objDal.INSP_Get_InspectorList();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_OfficeUserList()
        {
            try
            {
                return objDal.Get_OfficeUserList_DL();
            }
            catch
            {
                throw;
            }
        }
        //Supplier Login Check
        public DataSet Get_Supplier_UserCredentials(string Supplier_Code)
        {
            try
            {
                return objDal.Get_Supplier_UserCredentials(Supplier_Code);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Author : Anjali
        /// Created Date : 06-08-2016
        /// </summary>
        /// <param name="CompanyID">Logged in user's Company</param>
        /// <param name="_dtDepartmentTable">Id of selected departments</param>
        /// <returns>list of users in selected departments</returns>
        public DataTable Get_UserList_By_Dept_DL(int CompanyID, DataTable _dtDepartmentTable)
        {
            try
            {
                return objDal.Get_UserList_By_Dept_DL(CompanyID, _dtDepartmentTable);
            }
            catch
            {
                throw;
            }
        }
    }
}