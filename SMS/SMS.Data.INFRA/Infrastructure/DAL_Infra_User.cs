using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using SMS.Properties;

/// <summary>
/// Summary description for DAL_User_Credentials
/// </summary>

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_UserCredentials
    {

        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        private string connection = "";

        private string OCA_connection = "";

        public DAL_Infra_UserCredentials(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_UserCredentials()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            OCA_connection = ConfigurationManager.ConnectionStrings["demoasp"].ConnectionString;
        }

        public DataSet Get_UserCredentials_DL(string userid, string password)
        {
            string DecPassword = DMS.DES_Encrypt_Decrypt.Decrypt(password);

            SqlParameter[] obj = new SqlParameter[]{  
                
                                                      new SqlParameter("@Username",SqlDbType.VarChar,50),
                                                      new SqlParameter("@Password",SqlDbType.VarChar,25),
                                                      new SqlParameter("@DecPassword",SqlDbType.VarChar,25)
   												  };
            obj[0].Value = userid.ToString();
            obj[1].Value = password.ToString();
            obj[2].Value = DecPassword.ToString();

            DataSet ds = new DataSet();

            SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserCredentials", ds, new string[] { "Login" }, obj);
            return ds;
        }
        public DataTable Get_UserDetails_DL(int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                
                                                      new SqlParameter("@UserID",UserID)
   												  };
            DataSet ds = new DataSet();
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserDetails", obj).Tables[0];
        }
        public DataTable Get_UserDetails_DL(string UserName)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                
                                                      new SqlParameter("@UserName",UserName)
   												  };
            DataSet ds = new DataSet();
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserDetailsByUserName", obj).Tables[0];
        }

        public DataTable Get_UserDetails_DL(int UserID, string FieldName)
        {
            string SQL = "select " + FieldName + " from lib_user where userid =" + UserID;

            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];

        }

        public DataTable Get_UserAccessForPage_DL(int UserID, string PageLink)
        {
            SqlParameter[] obj = new SqlParameter[]{
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@PageLink",PageLink)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserAccessForPage", obj).Tables[0];
        }

        public string Get_MenuAccessID_DL(int UserID, string PageLink)
        {
            SqlParameter[] obj = new SqlParameter[]{
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@PageLink",PageLink)
   												  };
            return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Get_MenuAccessID", obj));
        }

        public string Get_UserData_DL(int UserID, string DataColumn)
        {
            string SQL = "";

            SQL = "SELECT " + DataColumn + " FROM LIB_USER WHERE USERID=@UserID";

            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
   												  };

            return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL, obj));
        }

        public DataTable Get_UserList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserList").Tables[0];
        }
        public DataTable Get_UserList_ApprovalLimit_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserList_ApprovalLimit").Tables[0];
        }


        public DataTable Get_UserList_Search(string searchtext, int? companyid, int? deptid, int? managerid, int? activestatus, string usertype, int? techmgrid
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@CompanyID", companyid),
                   new System.Data.SqlClient.SqlParameter("@DeptID", deptid),
                   new System.Data.SqlClient.SqlParameter("@ManagerID", managerid),
                   new System.Data.SqlClient.SqlParameter("@ActiveStatus", activestatus),
                   new System.Data.SqlClient.SqlParameter("@UserType", usertype),
                   new System.Data.SqlClient.SqlParameter("@Tech_Mgr_ID", techmgrid),
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserListSearch", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }




        public DataTable Get_UserList_DL(int CompanyID)
        {
            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@CompanyID",CompanyID)
   												  };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserListByCompany", obj).Tables[0];
        }
        public DataTable Get_UserList_DL(int CompanyID, string FilterText)
        {
            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@CompanyID",CompanyID),
                                                      new SqlParameter("@FilterText",FilterText)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserListBySearch", obj).Tables[0];
        }
        public DataTable Get_UserList_DL(int CompanyID, string FilterText, int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@CompanyID",CompanyID),
                                                      new SqlParameter("@FilterText",FilterText),
                                                      new SqlParameter("@UserID",UserID)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserListBySearch_MenuAccess", obj).Tables[0];
        }

        public DataTable Get_Menu_Lib_Access_DL(int UserID)
        {

             SqlParameter[] obj = new SqlParameter[]{ 
                                                      
                                                      new SqlParameter("@UserID",UserID)
   												  };
             return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_MNU_Get_MenuLibAccess", obj).Tables[0];

        }
        public DataTable Get_UserList_By_Dept_DL(int CompanyID, string DeptList)
        {
            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@Dept",DeptList),
                                                      new SqlParameter("@CompanyID",CompanyID)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserListByDeptCompany", obj).Tables[0];
        }


        public DataSet Get_UserMenu_DL(string username, string usrole)
        {
            SqlParameter[] obj = new SqlParameter[]{   
                
                                                      new SqlParameter("@UserRole",SqlDbType.VarChar,50),
                                                      new SqlParameter("@UserName",SqlDbType.VarChar,50),
                                                      new SqlParameter("@Result",SqlDbType.Int,2,ParameterDirection.Output,true,0,0,"",DataRowVersion.Current,null)
   												  };
            obj[0].Value = usrole.ToString();
            obj[1].Value = username.ToString();

            DataSet ds = new DataSet();

            SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserMenu", ds, new string[] { "menus" }, obj);
            return ds;
        }

        public DataTable Get_UserQuickMenu_DL(int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{ 
                new SqlParameter("@UserID",UserID),
   			};

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserQuickMenu", obj).Tables[0];

        }

        public DataTable Get_UserTypeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserTypeList").Tables[0];

        }

        public DataTable Get_UserTypeList_DL(int usertypeId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{   
                                                      new SqlParameter("@UserTypeID",usertypeId),
                                                      
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserTypeList", sqlprm).Tables[0];
        }

        public int CreateUser_DL(UserProperties User)
        {
            //int CompanyID, string FName, string LName, DateTime DOB, string UserName, string PWD, string uType, string Designation, double ApprovalLimit, string PresentAddress, string PermanentAddress, DateTime DateOfJoining, DateTime DateOfProbation, int DepartmentID, string NetWkId, int FleetID

            SqlParameter[] sqlprm = new SqlParameter[]{   
                                                      new SqlParameter("@CompanyID",User.CompanyId),
                                                      new SqlParameter("@FName",User.First_Name),
                                                      new SqlParameter("@LName",User.Last_Name),
                                                      new SqlParameter("@DOB",User.DOB),
                                                      new SqlParameter("@UserName",User.UserName),
                                                      new SqlParameter("@PWD",User.Password),
                                                      new SqlParameter("@uType",User.UserType),
                                                      new SqlParameter("@Designation",User.Designation),
                                                      new SqlParameter("@ApprovalLimit",User.ApprovalLimit),                                                  
                                                      new SqlParameter("@PresentAddress",User.PresentAddress),
                                                      new SqlParameter("@PermanentAddress",User.PermanentAddress),
                                                      new SqlParameter("@DateOfJoining",User.DateOfJoining),
                                                      new SqlParameter("@DateOfProbation",User.DateOfProbation),
                                                      new SqlParameter("@DepartmentID",User.DepartmentID),
                                                      new SqlParameter("@NetWkId",User.NetWkId),
                                                      new SqlParameter("@FleetID",User.FleetID),
                                                      new SqlParameter("@Created_By",User.Created_By),
                                                      new SqlParameter("@Mobile",User.Mobile),
                                                      new SqlParameter("@Email",User.Email),
                                                      new SqlParameter("@ManagerID",User.ManagerID),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_CREATE_USER", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public void Start_Session_DL(int UserID, string SessionID, string UserIP, string ClientBrowser)
        {
            SqlParameter[] param = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@SessionID",SessionID),
                                                      new SqlParameter("@UserIP",UserIP),
                                                      new SqlParameter("@ClientBrowser",ClientBrowser)
   												  };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Start_Session", param);
        }

        public void End_Session_DL(int UserID)
        {
            SqlParameter[] param = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
   												  };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_End_Session", param);
        }

        public DataTable Get_LoggedinUsers_DL(int SessionUserID)
        {
            SqlParameter[] param = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",SessionUserID)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_LoggedinUsers", param).Tables[0];
        }

        public DataTable Get_UserSessionLog_DL(int SessionUserID, DateTime Dt_StartDate, DateTime DT_EndDate)
        {
            SqlParameter[] param = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",SessionUserID),
                                                      new SqlParameter("@StartDate",Dt_StartDate),
                                                      new SqlParameter("@EndDate",DT_EndDate)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserSessionLog", param).Tables[0];
        }

        public int Update_UserDetails_DL(int UserID, string First_Name, string Middle_Name, string Last_Name, string MailID, int DeptID, int ManagerID, int Approval_Limit, string Designation, string Mobile_Number)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@First_Name", First_Name),
                                                      new SqlParameter("@Middle_Name",Middle_Name),
                                                      new SqlParameter("@Last_Name",Last_Name),
                                                      new SqlParameter("@MailID",MailID),
                                                      new SqlParameter("@DeptID",DeptID),
                                                      new SqlParameter("@ManagerID",ManagerID),
                                                      new SqlParameter("@Approval_Limit",Approval_Limit),
                                                      new SqlParameter("@Designation",Designation),
                                                      new SqlParameter("@Mobile_Number",Mobile_Number),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_UserDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int CreateUser_DL(int Created_By, string First_Name, string Middle_Name, string Last_Name, string UserName, string Password, string NetWkId, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
           , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address
           , string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]{   
                                                      new SqlParameter("@CompanyID",CompanyID),
                                                      new SqlParameter("@FName",First_Name),
                                                      new SqlParameter("@LName",Last_Name),
                                                      new SqlParameter("@DOB",DOB),
                                                      new SqlParameter("@UserName",UserName),
                                                      new SqlParameter("@PWD",Password),
                                                      new SqlParameter("@uType",User_Type),
                                                      new SqlParameter("@Designation",Designation),
                                                      new SqlParameter("@ApprovalLimit",Approval_Limit),                                                  
                                                      new SqlParameter("@PresentAddress",Present_Address),
                                                      new SqlParameter("@PermanentAddress",Parmanent_Address),
                                                      new SqlParameter("@DateOfJoining",DOJ),
                                                      new SqlParameter("@DateOfProbation",DOP),
                                                      new SqlParameter("@DepartmentID",DeptID),
                                                      new SqlParameter("@NetWkId",NetWkId),
                                                      new SqlParameter("@FleetID",Tech_ManagerID),
                                                      new SqlParameter("@Created_By",Created_By),
                                                      new SqlParameter("@Mobile",Mobile_Number),
                                                      new SqlParameter("@Email",MailID),
                                                      new SqlParameter("@ManagerID",ManagerID),
                                                      new SqlParameter("@SiteID",SiteID),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_CREATE_USER", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int CreateUser_DL(int Created_By, string First_Name, string Middle_Name, string Last_Name, string UserName, string Password, string NetWkId, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
           , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address
           , string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID, int NationalityId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]{   
                                                      new SqlParameter("@CompanyID",CompanyID),
                                                      new SqlParameter("@FName",First_Name),
                                                      new SqlParameter("@LName",Last_Name),
                                                      new SqlParameter("@DOB",DOB),
                                                      new SqlParameter("@UserName",UserName),
                                                      new SqlParameter("@PWD",Password),
                                                      new SqlParameter("@uType",User_Type),
                                                      new SqlParameter("@Designation",Designation),
                                                      new SqlParameter("@ApprovalLimit",Approval_Limit),                                                  
                                                      new SqlParameter("@PresentAddress",Present_Address),
                                                      new SqlParameter("@PermanentAddress",Parmanent_Address),
                                                      new SqlParameter("@DateOfJoining",DOJ),
                                                      new SqlParameter("@DateOfProbation",DOP),
                                                      new SqlParameter("@DepartmentID",DeptID),
                                                      new SqlParameter("@NetWkId",NetWkId),
                                                      new SqlParameter("@FleetID",Tech_ManagerID),
                                                      new SqlParameter("@Created_By",Created_By),
                                                      new SqlParameter("@Mobile",Mobile_Number),
                                                      new SqlParameter("@Email",MailID),
                                                      new SqlParameter("@ManagerID",ManagerID),
                                                      new SqlParameter("@SiteID",SiteID),
                                                      new SqlParameter("@NationalityId",NationalityId),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_CREATE_USER", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Update_UserDetails_DL(int UserID, string First_Name, string Middle_Name, string Last_Name, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
            , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address
            , string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID, string NetWkId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@First_Name", First_Name),
                                                      new SqlParameter("@Middle_Name",Middle_Name),
                                                      new SqlParameter("@Last_Name",Last_Name),
                                                      new SqlParameter("@MailID",MailID),
                                                      new SqlParameter("@DeptID",DeptID),
                                                      new SqlParameter("@ManagerID",ManagerID),
                                                      new SqlParameter("@Approval_Limit",Approval_Limit),
                                                      new SqlParameter("@Designation",Designation),
                                                      new SqlParameter("@Mobile_Number",Mobile_Number),
                                                      new SqlParameter("@DOB",DOB),
                                                      new SqlParameter("@DOJ",DOJ),
                                                      new SqlParameter("@DOP",DOP),
                                                      new SqlParameter("@ParmanentAdd",Parmanent_Address),
                                                      new SqlParameter("@PresentAdd",Present_Address),
                                                      new SqlParameter("@UserType",User_Type),
                                                      new SqlParameter("@CompanyID",CompanyID),
                                                      new SqlParameter("@TechMgr",Tech_ManagerID),
                                                      new SqlParameter("@SiteID",SiteID),
                                                      new SqlParameter("@NetWkId",NetWkId),

                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_UserDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Update_UserDetails_DL(int UserID, string First_Name, string Middle_Name, string Last_Name, string MailID, int? DeptID, int? ManagerID, decimal? Approval_Limit
            , string Designation, string Mobile_Number, DateTime? DOB, DateTime? DOJ, DateTime? DOP, string Parmanent_Address, string Present_Address
            , string User_Type, int? CompanyID, int? Tech_ManagerID, int? SiteID, string NetWkId,int NationalityId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@First_Name", First_Name),
                                                      new SqlParameter("@Middle_Name",Middle_Name),
                                                      new SqlParameter("@Last_Name",Last_Name),
                                                      new SqlParameter("@MailID",MailID),
                                                      new SqlParameter("@DeptID",DeptID),
                                                      new SqlParameter("@ManagerID",ManagerID),
                                                      new SqlParameter("@Approval_Limit",Approval_Limit),
                                                      new SqlParameter("@Designation",Designation),
                                                      new SqlParameter("@Mobile_Number",Mobile_Number),
                                                      new SqlParameter("@DOB",DOB),
                                                      new SqlParameter("@DOJ",DOJ),
                                                      new SqlParameter("@DOP",DOP),
                                                      new SqlParameter("@ParmanentAdd",Parmanent_Address),
                                                      new SqlParameter("@PresentAdd",Present_Address),
                                                      new SqlParameter("@UserType",User_Type),
                                                      new SqlParameter("@CompanyID",CompanyID),
                                                      new SqlParameter("@TechMgr",Tech_ManagerID),
                                                      new SqlParameter("@SiteID",SiteID),
                                                      new SqlParameter("@NetWkId",NetWkId),
                                                       new SqlParameter("@NationalityId",NationalityId),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_UserDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        /// <summary>
        /// Description: Added a method to save the date format setting for logged in user.
        /// Created By: Krishnapriya
        /// </summary>
        /// <param name="UserID">Logged in User's ID will be sent as parameter</param>
        /// <param name="User_DateFormat">Selected date format will be sent as parameter</param>
        /// <returns></returns>
        public int Update_UserDetails_DL(int UserID, string User_DateFormat)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{
                                                      new SqlParameter("@UserID",UserID),                                                      
						                              new SqlParameter("@User_DateFormat",User_DateFormat),
                                                      new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_UserDateFormat", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        public int Delete_User_DL(int UserID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID),                                                      
                                                      new SqlParameter("@Deleted_By",Deleted_By),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Delete_User", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Update_UserPassword_DL(int UserID, string OldPassword, string NewPassword)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@OldPassword", OldPassword),
                                                      new SqlParameter("@NewPassword",NewPassword),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_Update_UserPassword", sqlprm);
            return int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
        }

        public DataTable Get_User_Vessel_Assignment_DL(int Vessel_ID, string User_Name)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@User_Name",User_Name)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_User_Vessel_Assignment", sqlprm).Tables[0];
        }
        public int INSERT_User_Vessel_Assignment_DL(int User_ID, int Vessel_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@User_ID",User_ID),                                                      
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@Created_By",Created_By),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_User_Vessel_Assignment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_User_Vessel_Assignment_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@ID",ID),                                                      
                                                      new SqlParameter("@Deleted_By",Deleted_By),
                                                      new SqlParameter("return",SqlDbType.Int)
   												  };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Delete_User_Vessel_Assignment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_Fleet_By_UserID_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
                                                        												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_FLEET_BY_USERID", sqlprm).Tables[0];

        }

        public DataTable Get_User_By_Manager_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
                                                        												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_USER_BY_MANAGER", sqlprm).Tables[0];

        }

        public DataTable Get_User_By_CompanyID_DL(int CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@CompanyID",CompanyID)
                                                        												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_User_BYCompanyID", sqlprm).Tables[0];

        }

        public string Get_User_Role_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",UserID)
                                       };
            return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "INF_Get_User_Role", sqlprm));
        }



        public DataTable Get_Travel_Quotation_Acess_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Travel_Quotation_Access").Tables[0];
        }


        public int Insert_Travel_Quotation_Acess_DL(int UserID, string OperationFlage, int CurrUserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@UserID", UserID),
                                                         new SqlParameter("@OperationFlage", OperationFlage), 
                                                         new SqlParameter("@CurrUserID", CurrUserID), 

                                                       };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Ins_Travel_Quotation_Access", sqlprm);
        }

        public DataTable Get_Super_List_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_Super_List").Tables[0];
        }
        public DataTable Get_SuperAttendingVessel_List_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_SuperAttendingVessel_List").Tables[0];
        }
        public int UPDATE_SuperAttendingVessel_DL(int SuperUserID, string OperationFlage, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@UserID", UserID),
                                                         new SqlParameter("@OperationFlage", OperationFlage), 
                                                         new SqlParameter("@Super_ID", SuperUserID)
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_UPDATE_SuperAttendingVessel", sqlprm);
        }

        public int UPD_Reset_Password(string EmailID, string Enc_TempPassword, string TempPassword)
        {
            SqlParameter[] sqlprm = new SqlParameter[] {new SqlParameter("@EmailID", EmailID),
                                                        new SqlParameter("@ENC_TEMPPASSWORD", Enc_TempPassword),
                                                        new SqlParameter("@TEMPPASSWORD", TempPassword),
                
                                                        new SqlParameter("@return", SqlDbType.Int)
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_RESET_PASSWORD", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public DataTable Get_OfficeUser_List_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_InspectorAssignment_List").Tables[0];
        }

        public int UPDATE_InspectorAssignmentl_DL(int SuperUserID, string OperationFlage, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@ID", UserID),
                                                         new SqlParameter("@OperationFlage", OperationFlage), 
                                                         new SqlParameter("@User_Id", SuperUserID)
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_SP_UPDATE_InspectorAssignment", sqlprm);
        }
        public DataTable Get_InspectorAssignment_List_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_InspectorAssignmet").Tables[0];
        }

        public int Update_UserMaster(string UserID, string Passkey)
        {

            return SqlHelper.ExecuteNonQuery(OCA_connection, CommandType.Text, "UPDATE USER_MASTER SET PassKey='" + Passkey + "' WHERE SMSLOG_User_ID=" + UserID );

        }

        public DataTable Get_UserMaster(string PassKey,string UserName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@PassKey",PassKey),
                                                      new SqlParameter("@UserName",UserName)                                          
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OCA_SP_Get_UserMaster", sqlprm).Tables[0];
        }
        public int CRW_CHECK_Card_Approval_DL(string CardType, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] {new SqlParameter("@UserId", UserId),
                                                        new SqlParameter("@CardType", CardType),
                                                        new SqlParameter("@return", SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CHECK_Card_Approval", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable INSP_Get_InspectorList()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_InspectorList").Tables[0];
        }
        public DataTable Get_OfficeUserList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_OfficeUserList").Tables[0];
        }

        public DataSet Get_Supplier_UserCredentials(string Supplier_Code)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                
                                                      new SqlParameter("@Supplier_Code",Supplier_Code)
   												  };
            DataSet ds = new DataSet();
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Supplier_UserCredentials", obj);

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
            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@Dept",_dtDepartmentTable),
                                                      new SqlParameter("@CompanyID",CompanyID)
   												  };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FMS_Get_UserListByDeptCompany", obj).Tables[0];
        }
        
    }
}