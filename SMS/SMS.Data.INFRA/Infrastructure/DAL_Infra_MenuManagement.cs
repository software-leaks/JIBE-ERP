using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;



/// <summary>
///     Summary description for BLL_Infra_MenuManagement
/// </summary>

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_MenuManagement
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_Infra_MenuManagement(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_MenuManagement()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        //public DataTable getMainModule_Collection_DL()
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("select convert(varchar,Menu_Code)+','+isnull(convert(varchar,Sequence_Order),'0')  +','+isnull(convert(varchar,Mod_Code),'0') as mcodeseq,Menu_Type,Menu_Short_Discription,Menu_Link,Sequence_Order from Lib_Menu where Active_Status=1 and Menu_Type is null  order by Sequence_Order", connection);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "usernames");
        //    return ds.Tables[0];
        //}

        public DataTable GetCollection_SubModules_DL(int mcode)
        {

            SqlParameter[] obj = new SqlParameter[] { new SqlParameter("@mcode", SqlDbType.Int), };
            obj[0].Value = mcode;
            DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_GetCollection_SubModules", ds, new string[] { "usdnme" }, obj);
            return ds.Tables[0];
        }
        public DataTable GetCollection_AllModules_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_GetCollection_AllModules").Tables[0];
        }

        public DataTable GetCollection_AllSubModules_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_GetCollection_AllSubModules").Tables[0];
        }

        //public DataTable GetCollection_MenuLinks_DL(int mod_code)
        //{
        //    SqlParameter[] obj = new SqlParameter[] { new SqlParameter("@mod_code", mod_code)};            
        //    DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_GetCollection_MenuLinks", ds, new string[] { "usdnme" }, obj);
        //    return ds.Tables[0];
        //}

        public DataTable Get_UserMenuAccess_DL(int mod_code, int userid, int LoginUser)
        {
            SqlParameter[] obj = new SqlParameter[] { 
                new SqlParameter("@userid", userid),
                new SqlParameter("@mod_code", mod_code),
                new SqlParameter("@loginUser",LoginUser)
            };
            DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserMenuAccess", ds, new string[] { "usdnme" }, obj);
            return ds.Tables[0];
        }

        public DataSet Get_UserMenuApproach(int mod_code, int userid, int LoginUser)
        {
            SqlParameter[] obj = new SqlParameter[] { 
                new SqlParameter("@userid", userid),
                new SqlParameter("@mod_code", mod_code),
                new SqlParameter("@loginUser",LoginUser)
            };
            DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserMenu_Approach", ds, new string[] { "usdnme" }, obj);
            return ds;
        }

        public void Swap_MenuSeqOrder_DL(Int64 menucode1, Int64 menucode2)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@mcode1",SqlDbType.BigInt),
                new SqlParameter("@mcode2",SqlDbType.BigInt)
            };
            obj[0].Value = menucode1;
            obj[1].Value = menucode2;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Swap_MenuSeqOrder", obj);
        }

        public void Update_MenuSequence_DL(int mcode, int seqnumber)//duplicate
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@mcode",SqlDbType.Int),
                new SqlParameter("@seqnumber",SqlDbType.Int)
            };
            obj[0].Value = mcode;
            obj[1].Value = seqnumber;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_MenuSequence", obj);
        }

        public void Update_MenuText_DL(int mcode, string smenutext, string sUrl, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@mcode",SqlDbType.Int),
                new SqlParameter("@smenutext",SqlDbType.VarChar,80),
                new SqlParameter("@sUrl",SqlDbType.VarChar,500),
                new SqlParameter("@SPM_Module_Stage_ID",SqlDbType.Int),
                new SqlParameter("@Menu_Enable",SqlDbType.Int),
                new SqlParameter("@DepID",SqlDbType.Int)
            };
            obj[0].Value = mcode;
            obj[1].Value = smenutext;
            obj[2].Value = sUrl;
            obj[3].Value = SPM_Module_Stage_ID;
            obj[4].Value = Menu_Enable;
            obj[5].Value = DepID;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_MenuText", obj);

        }
        /// <summary>
        /// Description: Method added to update menu text.
        /// <summary>
        /// <param name="mcode">Menu Code</param>
        /// <param name="smenutext">Menu Text</param>
        /// <param name="sUrl">Menu URL</param>
        /// <param name="SPM_Module_Stage_ID"></param>
        /// <param name="Menu_Enable">Menu is enabled or disabled</param>
        /// <param name="DepID">Department ID</param>
        /// <param name="imageName">Image class name</param>
         public void Update_MenuText_DL(int mcode, string smenutext, string sUrl, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID, string imageName)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@mcode",SqlDbType.Int),
                new SqlParameter("@smenutext",SqlDbType.VarChar,80),
                new SqlParameter("@sUrl",SqlDbType.VarChar,500),
                new SqlParameter("@SPM_Module_Stage_ID",SqlDbType.Int),
                new SqlParameter("@Menu_Enable",SqlDbType.Int),
                new SqlParameter("@DepID",SqlDbType.Int),
                new SqlParameter("@ImageName", SqlDbType.VarChar, 200)
            };
            obj[0].Value = mcode;
            obj[1].Value = smenutext;
            obj[2].Value = sUrl;
            obj[3].Value = SPM_Module_Stage_ID;
            obj[4].Value = Menu_Enable;
            obj[5].Value = DepID;
            obj[6].Value = imageName;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_MenuText", obj);

        }
         
        public int getMaximumMenuCode_DL()
        {

            string queryString = "SELECT MAX(MENU_CODE) FROM LIB_MENU";

            int menucode = 0;
            string strRet = SqlHelper.ExecuteScalar(connection, CommandType.Text, queryString).ToString();

            if (strRet == "")
            { return menucode = 1; }
            else
            {
                menucode = int.Parse(strRet) + 1;
                return menucode;
            }
        }


        //this function is used for getting the maximum module id from the module table
        public int getMaximumModID_DL()
        {
            string queryString = "SELECT MAX(MOD_CODE) FROM LIB_MENU";

            int modid = 0;
            string strRet = SqlHelper.ExecuteScalar(connection, CommandType.Text, queryString).ToString();

            if (strRet == "")
            { return modid = 1; }
            else
            {
                modid = int.Parse(strRet) + 1;
                return modid;
            }
        }

        public int Insert_MenuModule_DL(int modid, string modname, string crtdby)//duplicate
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@modcode",SqlDbType.Int),
                new SqlParameter("@modname",SqlDbType.VarChar,30),
                new SqlParameter("@crtdby",SqlDbType.VarChar,30)
            };
            obj[0].Value = modid;
            obj[1].Value = modname;
            obj[2].Value = crtdby;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_MenuModule", obj);
            return 1;

        }


        public int Insert_Lib_Menu_DL(int menucode, int modcode, int menutype, string shortdesc, string linkname, string crtdby, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@Menu_Code",SqlDbType.Int),
                new SqlParameter("@Mod_Code",SqlDbType.Int),
                new SqlParameter("@Menu_Type",SqlDbType.Int),
                new SqlParameter("@Menu_Short_Discription",SqlDbType.VarChar,25),
                new SqlParameter("@Menu_Link",SqlDbType.VarChar,200),
                new SqlParameter("@Created_By",SqlDbType.VarChar,25),
                new SqlParameter("@Menu_Enable",SqlDbType.Int),
                new SqlParameter("@SPM_Module_Stage_ID",SqlDbType.Int),
                 new SqlParameter("@depID",SqlDbType.Int)
            };
            obj[0].Value = menucode;
            obj[1].Value = modcode;
            obj[2].Value = menutype;
            obj[3].Value = shortdesc;
            obj[4].Value = linkname;
            obj[5].Value = crtdby;
            obj[6].Value = Menu_Enable;
            obj[7].Value = SPM_Module_Stage_ID;
            obj[8].Value = DepID;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Lib_Menu", obj);
            return 1;
        }
        /// <summary>
        /// Description: Method added to insert menu text.
        /// <summary>
        /// <param name="mcode">Menu Code</param>
        /// <param name="modcode">Menu mod code</param>
        /// <param name="menutype">Menu type</param>
        /// <param name="shortdesc">Menu Short Description</param>
        /// <param name="linkname">Menu link name</param>
        /// <param name="crtdby">Menu Created By</param>
        /// <param name="SPM_Module_Stage_ID"></param>
        /// <param name="Menu_Enable">Menu is enabled or disabled</param>
        /// <param name="DepID">Dept ID</param>
        /// <param name="imageName">Menu image class name</param>
        public int Insert_Lib_Menu_DL(int menucode, int modcode, int menutype, string shortdesc, string linkname, string crtdby, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID, string imageName)        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@Menu_Code",SqlDbType.Int),
                new SqlParameter("@Mod_Code",SqlDbType.Int),
                new SqlParameter("@Menu_Type",SqlDbType.Int),
                new SqlParameter("@Menu_Short_Discription",SqlDbType.VarChar,25),
                new SqlParameter("@Menu_Link",SqlDbType.VarChar,200),
                new SqlParameter("@Created_By",SqlDbType.VarChar,25),
                new SqlParameter("@Menu_Enable",SqlDbType.Int),
                new SqlParameter("@SPM_Module_Stage_ID",SqlDbType.Int),
                 new SqlParameter("@depID",SqlDbType.Int),
               new SqlParameter("@ImageName",SqlDbType.VarChar,200)
            };
            obj[0].Value = menucode;
            obj[1].Value = modcode;
            obj[2].Value = menutype;
            obj[3].Value = shortdesc;
            obj[4].Value = linkname;
            obj[5].Value = crtdby;
            obj[6].Value = Menu_Enable;
            obj[7].Value = SPM_Module_Stage_ID;
            obj[8].Value = DepID;
            obj[9].Value = imageName;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Lib_Menu", obj);
            return 1;
        }
        //Added by Krishnapriya- to save images for menu
        public int Del_LibMenu_DL(int menucode, string deletedby)//duplicate
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@MenuCode",SqlDbType.Int),
                new SqlParameter("@DeletedBy",SqlDbType.VarChar,25)
            };
            obj[0].Value = menucode;
            obj[1].Value = deletedby;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_LibMenu", obj);
            return 1;
        }

        public int Update_User_Menu_Access_DL(int UserID, int Menu_Code, int Menu, int View, int Add, int Edit, int Delete, int Approve, int Admin, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Menu_Code",Menu_Code),
                new SqlParameter("@Access_Menu",Menu),
                new SqlParameter("@Access_View",View),
                new SqlParameter("@Access_Add",Add),
                new SqlParameter("@Access_Edit",Edit),
                new SqlParameter("@Access_Delete",Delete),
                new SqlParameter("@Access_Approve",Approve),
                 new SqlParameter("@Access_Admin",Admin),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_User_Menu_Access", obj);
            return 1;

        }


        public int Update_User_Menu_Access_DL(int UserID, int Menu_Code, int Menu, int View, int Add, int Edit, int Delete, int Approve, int Admin, int Unverify, int Revoke, int Unclose, int Urgent, int Close, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Menu_Code",Menu_Code),
                new SqlParameter("@Access_Menu",Menu),
                new SqlParameter("@Access_View",View),
                new SqlParameter("@Access_Add",Add),
                new SqlParameter("@Access_Edit",Edit),
                new SqlParameter("@Access_Delete",Delete),
                new SqlParameter("@Access_Approve",Approve),
                 new SqlParameter("@Access_Admin",Admin),
                 new SqlParameter("@Unverify",Unverify),
                  new SqlParameter("@Revoke",Revoke),
                   new SqlParameter("@Unclose",Unclose),
                    new SqlParameter("@Urgent",Urgent),
                     new SqlParameter("@Close",Close),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_User_Menu_Access", obj);
            return 1;

        }

        public int Update_User_Menu_Access_DL_New(DataTable dtUpdate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        
                                          new SqlParameter("@dtUpdateMenuAccess", dtUpdate)
                                    };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_User_Menu_Access_New", sqlprm);
             
           
        }

        public int Initialize_User_Menu_DL(int UserID, int Mod_Code, int Menu, int View, int Add, int Edit, int Delete, int Approve, int Admin, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Mod_Code",Mod_Code),
                new SqlParameter("@Access_Menu",Menu),
                new SqlParameter("@Access_View",View),
                new SqlParameter("@Access_Add",Add),
                new SqlParameter("@Access_Edit",Edit),
                new SqlParameter("@Access_Delete",Delete),
                new SqlParameter("@Access_Approve",Approve),
                  new SqlParameter("@Access_Admin",Admin),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Initialize_User_Menu", obj);
            return 1;

        }
        public int Copy_MenuAccessFromUser_DL(int CopyFromUserID, int CopyToUserID, int AppendMode, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@CopyFromUserID",CopyFromUserID),
                new SqlParameter("@CopyToUserID",CopyToUserID),
                new SqlParameter("@AppendMode",AppendMode),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Copy_MenuAccessFromUser", obj);
            return 1;
        }



        //public int Copy_MenuAccessFromUser_DL(int CopyFromUserID, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
        //{
        //    SqlParameter[] obj = new SqlParameter[]{
        //        new SqlParameter("@CopyFromUserID",CopyFromUserID),
        //        new SqlParameter("@CopyToUserID",CopyToUserID),
        //        new SqlParameter("@AppendMode",AppendMode),
        //        new SqlParameter("@Selected_Mod_Code",Selected_Mod_Code),
        //        new SqlParameter("@Created_By",Created_By)
        //    };

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Copy_MenuAccessFromUser", obj);
        //    return 1;
        //}

        public int Copy_MenuAccessFromUser(int CopyFromUserID, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@CopyFromUserID",CopyFromUserID),
                new SqlParameter("@CopyToUserID",CopyToUserID),
                new SqlParameter("@AppendMode",AppendMode),
                new SqlParameter("@Selected_Mod_Code",Selected_Mod_Code),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Copy_MenuAccessFromUser", obj);
            return 1;
        }

        public int Copy_MenuAccessFromRole(int RoleId, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@RoleId",RoleId),
                new SqlParameter("@CopyToUserID",CopyToUserID),
                new SqlParameter("@AppendMode",AppendMode),
                new SqlParameter("@Selected_Mod_Code",Selected_Mod_Code),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Copy_MenuAccessFromRole", obj);
            return 1;
        }

        public DataTable Get_MenuAccess(int? User_Id, int? Menu_Id)
        {
            SqlParameter[] obj = new SqlParameter[] { 
                new SqlParameter("@User_Id", User_Id),
                new SqlParameter("@Menu_Id", Menu_Id),
                
            };
            DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "INF_SP_GET_MenuAccessFields", ds, new string[] { "usdnme" }, obj);
            return ds.Tables[0];
        }

        public int Get_Access(int? Menu_Id, int? Created_By, DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Menu_Id", Menu_Id),
                                      
                                        new SqlParameter("@Created_By", Created_By),
                                          new SqlParameter("@dtInsert", dt)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "INF_SP_GET_INSERTFIELDS", sqlprm);
        }

        public int SyncMenu(DataTable dt)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        
                                          new SqlParameter("@dtMenu", dt)
                                    };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Sync_Menu", sqlprm);

        }

        public DataTable Get_Role()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Role").Tables[0];

        }
        public int Publish_RoleMenuAcces_DL(DataTable dtRoleMenuAcces)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                                        
                                          new SqlParameter("@dtRoleMenuAcces", dtRoleMenuAcces)
                                    };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INSERT_ROLE_MENU_Access", sqlprm);

        }
        public int Publish_Role_DL(DataTable dtRole)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        
                                          new SqlParameter("@dtRole", dtRole)
                                    };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INSERT_ROLES", sqlprm);

        }

        public int InsertJITMENU(int Menu_Code, int Mod_Code, int Menu_Type, string Menu_Short_Discription, string Menu_Link, string Menu_Discription, int Created_By, DateTime Date_Of_Created, int Modified_By, DateTime Date_Of_Modified, int Deleted_By, DateTime Date_Of_Deleted
         , int Active_Status, int Priority_Sequence, int Sequence_Order, int Menu_Enable, int Access_Admin, int DepID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        
                                          new SqlParameter("@Menu_Code", Menu_Code),
                                            new SqlParameter("@Mod_Code", Mod_Code),
                                              new SqlParameter("@Menu_Type", Menu_Type),
                                                new SqlParameter("@Menu_Short_Discription", Menu_Short_Discription),
                                                  new SqlParameter("@Menu_Link", Menu_Link),
                                                    new SqlParameter("@Menu_Discription", Menu_Discription),
                                                      new SqlParameter("@Created_By", Created_By),
                                                        new SqlParameter("@Date_Of_Created", Date_Of_Created),
                                                        new SqlParameter("@Modified_By", Modified_By),
                                                            new SqlParameter("@Date_Of_Modified", Date_Of_Modified),
                                                              new SqlParameter("@Deleted_By", Deleted_By),
                                                                new SqlParameter("@Date_Of_Deleted", Date_Of_Deleted),
                                                                  new SqlParameter("@Active_Status", Active_Status),
                                                                    new SqlParameter("@Priority_Sequence", Priority_Sequence),
                                                                      new SqlParameter("@Sequence_Order", Sequence_Order),
                                                                        new SqlParameter("@Menu_Enable", Menu_Enable),
                                                                          new SqlParameter("@Access_Admin", Access_Admin),
                                                                            new SqlParameter("@DepID", DepID)

                                    };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Insert_JIT_Menu", sqlprm);

        }

        public int SyncMenuAccess(DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        
                                          new SqlParameter("@dtMenu", dt)
                                    };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Sync_Menu_Access", sqlprm);

        }
        //Method sends the filters to DB and Gets the records
        public DataSet Get_AccessRightChanges_DL(DateTime? FromDate, DateTime? ToDate, int? Pageindex, int? Pagesize, string Sortcoloumn, int? SortOrder, int? UserID, string ModuleName, string PageName, int? CurrentUserID, ref int Count)
        {            
            SqlParameter[] prm = new SqlParameter[]
            { 
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@Pageindex", Pageindex),
                    new SqlParameter("@Pagesize", Pagesize),
                    new SqlParameter("@Sortcoloumn", Sortcoloumn),
                    new SqlParameter("@SORTDIRECTION", SortOrder),
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@Module", ModuleName),
                    new SqlParameter("@Page", PageName),
                    new SqlParameter("@CurrentUserID", CurrentUserID),
                    new SqlParameter("@Count", Count)
            };            
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_AccessRightLogReport", prm);
            Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;
        }
        //Method get updated incremental data of access right changes on request 
        public void Get_AccessRightIncrementalData_DL()
        {
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_GET_AccessRightAuditLogReport");
        }
        //Method sends the filters to DB and Gets the records
        public DataTable Get_AccessRightAll_Dl(int MenuID)
        {
            SqlParameter[] prm = new SqlParameter[]
            { 
                    new SqlParameter("@MenuID", MenuID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_MenuAccessDetails", prm).Tables[0];
        }

        public DataTable INF_Get_CompanyList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_CompanyList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable INF_Get_DepartmentList()
        {           
            try
            {
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_DepartmentList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable INF_Get_UserList(DataTable dtCompanyId, DataTable dtDepartmentId)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                {
                   new System.Data.SqlClient.SqlParameter("@dtCompanyId", dtCompanyId),
                   new System.Data.SqlClient.SqlParameter("@dtDepartmentId", dtDepartmentId),
                };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_UserList", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable INF_Get_PageList(DataTable dtMenuId)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                {
                   new System.Data.SqlClient.SqlParameter("@ParentMenuId", dtMenuId),
                };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_PageList", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public DataTable INF_Get_MenuList()
        {  
            try
            {
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_MenuList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable INF_Get_SubMenuList(DataTable dtMenuId)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                {
                   new System.Data.SqlClient.SqlParameter("@dtMenuId", dtMenuId),
                };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_SubMenuList", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet INF_Get_AccessRightsReport(DataTable dtCompanyId, DataTable dtMenuId, DataTable dtDepartmentId, DataTable dtSubMenuId, DataTable dtUserId, DataTable dtPageId, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@dtCompanyId", dtCompanyId),   
                   new System.Data.SqlClient.SqlParameter("@dtMenuId", dtMenuId),
                   new System.Data.SqlClient.SqlParameter("@dtDepartmentId", dtDepartmentId),
                   new System.Data.SqlClient.SqlParameter("@dtSubMenuId",dtSubMenuId ),
                   new System.Data.SqlClient.SqlParameter("@dtUserId",dtUserId),                    
                   new System.Data.SqlClient.SqlParameter("@dtPageId",dtPageId),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),              
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),                  
                    
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_AccessRightsReport", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

    }
}