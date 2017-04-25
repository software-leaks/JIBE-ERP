using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;


/// <summary>
/// Summary description for BLL_Infra_MenuManagement
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{

    public class BLL_Infra_MenuManagement
    {
        DAL_Infra_MenuManagement objDAL = new DAL_Infra_MenuManagement();

        public BLL_Infra_MenuManagement()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //public DataTable getMainModule_Collection()
        //{
        //    try
        //    {                
        //        return objDAL.getMainModule_Collection_DL();
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}

        public DataTable GetCollection_AllModules()
        {
            try
            {
                return objDAL.GetCollection_AllModules_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetCollection_SubModules(int mcode)
        {
            try
            {
                return objDAL.GetCollection_SubModules_DL(mcode);
            }
            catch
            {
                throw;
            }
        }


        public DataTable GetCollection_AllSubModules()
        {
            try
            {
                return objDAL.GetCollection_AllSubModules_DL();
            }
            catch
            {
                throw;
            }

        }
        //public DataTable GetCollection_MenuLinks(int mod_code)
        //{
        //    try
        //    {
        //        return objDAL.GetCollection_MenuLinks_DL(mod_code);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}

        public DataTable Get_UserMenuAccess(int mod_code, int userid, int LoginUser)
        {
            try
            {
                return objDAL.Get_UserMenuAccess_DL(mod_code, userid, LoginUser);
            }
            catch
            {
                throw;
            }

        }

        public DataSet Get_UserMenuApproach(int mod_code, int userid, int LoginUser)
        {
            try
            {
                return objDAL.Get_UserMenuApproach(mod_code, userid, LoginUser);
            }
            catch
            {
                throw;
            }

        }
        public void Swap_MenuSeqOrder(Int64 menucode1, Int64 menucode2)
        {
            try
            {
                objDAL.Swap_MenuSeqOrder_DL(menucode1, menucode2);
            }
            catch
            {
                throw;
            }
        }

        public void Update_MenuSequence(int mcode, int seqnumber)
        {
            try
            {
                objDAL.Update_MenuSequence_DL(mcode, seqnumber);
            }
            catch
            {
                throw;
            }
        }

        public void Update_MenuText(int mcode, string smenutext, string sUrl, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID)
        {
            try
            {
                objDAL.Update_MenuText_DL(mcode, smenutext, sUrl, SPM_Module_Stage_ID, Menu_Enable, DepID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Description: Method added to update menu.
        /// <summary>
        /// <param name="mcode">Menu Code</param>
        /// <param name="smenutext">Menu Text</param>
        /// <param name="sUrl">Menu URL</param>
        /// <param name="SPM_Module_Stage_ID"></param>
        /// <param name="Menu_Enable">Menu is enabled or disabled</param>
        /// <param name="DepID">Department ID</param>
        /// <param name="imageName">Image class name</param>
        public void Update_MenuText(int mcode, string smenutext, string sUrl, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID, string imageName)
        {
            try
            {
                objDAL.Update_MenuText_DL(mcode, smenutext, sUrl, SPM_Module_Stage_ID, Menu_Enable, DepID, imageName);
            }
            catch
            {
                throw;
            }
        }
        public int getMaximumMenuCode()
        {
            try
            {
                return objDAL.getMaximumMenuCode_DL();
            }
            catch
            {
                throw;
            }

        }

        public int getMaximumModID()
        {
            try
            {
                return objDAL.getMaximumModID_DL();
            }
            catch
            {
                throw;
            }
        }

        public int Insert_MenuModule(int modid, string modname, string crtdby)
        {
            try
            {
                return objDAL.Insert_MenuModule_DL(modid, modname, crtdby);
            }
            catch
            {
                throw;
            }

        }

        public int Insert_Lib_Menu(int menucode, int modcode, int menutype, string shortdesc, string linkname, string crtdby, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID)
        {
            try
            {
                return objDAL.Insert_Lib_Menu_DL(menucode, modcode, menutype, shortdesc, linkname, crtdby, SPM_Module_Stage_ID, Menu_Enable, DepID);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Description: Method added to insert menu.
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
        public int Insert_Lib_Menu(int menucode, int modcode, int menutype, string shortdesc, string linkname, string crtdby, int? SPM_Module_Stage_ID, int? Menu_Enable, int? DepID, string imageName)
        {
            try
            {
                return objDAL.Insert_Lib_Menu_DL(menucode, modcode, menutype, shortdesc, linkname, crtdby, SPM_Module_Stage_ID, Menu_Enable, DepID, imageName);
            }
            catch
            {
                throw;
            }

        }

        public int Del_LibMenu(int menucode, string deletedby)
        {
            try
            {
                return objDAL.Del_LibMenu_DL(menucode, deletedby);
            }
            catch
            {
                throw;
            }

        }

        public int Update_User_Menu_Access(int UserID, int Menu_Code, int Menu, int View, int Add, int Edit, int Delete, int Approve, int Admin, int Created_By)
        {
            try
            {
                return objDAL.Update_User_Menu_Access_DL(UserID, Menu_Code, Menu, View, Add, Edit, Delete, Approve, Admin, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public int Update_User_Menu_Access_DL(int UserID, int Menu_Code, int Menu, int View, int Add, int Edit, int Delete, int Approve, int Admin, int Unverify, int Revoke, int Unclose, int Urgent, int Close, int Created_By)
        {
            try
            {
                return objDAL.Update_User_Menu_Access_DL(UserID, Menu_Code, Menu, View, Add, Edit, Delete, Approve, Admin, Unverify, Revoke, Unclose, Urgent, Close, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public int Update_User_Menu_Access_DL_New(DataTable dtUpdate)
        {

            try
            {
                return objDAL.Update_User_Menu_Access_DL_New(dtUpdate);
            }

            catch
            {
                throw;
            }

        }

        public int Initialize_User_Menu(int UserID, int Mod_Code, int Menu, int View, int Add, int Edit, int Delete, int Approve, int Admin, int Created_By)
        {
            try
            {
                return objDAL.Initialize_User_Menu_DL(UserID, Mod_Code, Menu, View, Add, Edit, Delete, Approve, Admin, Created_By);
            }
            catch
            {
                throw;
            }
        }

        //public int Remove_Menu_Access(int UserID, int Mod_Code, int Created_By)
        //{
        //    try
        //    {
        //        return objDAL.Remove_Menu_Access_DL(UserID, Mod_Code, Created_By);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int Copy_MenuAccessFromUser(int CopyFromUserID, int CopyToUserID, int AppendMode, int Created_By)
        {
            try
            {
                return objDAL.Copy_MenuAccessFromUser_DL(CopyFromUserID, CopyToUserID, AppendMode, Created_By);
            }
            catch
            {
                throw;
            }

        }

        //public int Copy_MenuAccessFromUser(int CopyFromUserID, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
        //{
        //    try
        //    {
        //        return objDAL.Copy_MenuAccessFromUser_DL(CopyFromUserID, CopyToUserID, AppendMode, Selected_Mod_Code, Created_By);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        public int Copy_MenuAccessFromUser(int CopyFromUserID, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
        {
            try
            {
                return objDAL.Copy_MenuAccessFromUser(CopyFromUserID, CopyToUserID, AppendMode, Selected_Mod_Code, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public int Copy_MenuAccessFromRole(int RoleId, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
        {
            try
            {
                return objDAL.Copy_MenuAccessFromRole(RoleId, CopyToUserID, AppendMode, Selected_Mod_Code, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_MenuAccess(int? User_Id, int? Menu_Id)
        {
            try
            {

                return objDAL.Get_MenuAccess(User_Id, Menu_Id);
            }
            catch
            {
                throw;
            }
        }
        public int Get_Access(int? Menu_Id, int? Created_By, DataTable dt)
        {
            try
            {
                return objDAL.Get_Access(Menu_Id, Created_By, dt);
            }
            catch
            {
                throw;
            }
        }

        public int SyncMenu(DataTable dt)
        {
            try
            {
                return objDAL.SyncMenu(dt);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Role()
        {
            try
            {
                return objDAL.Get_Role();
            }
            catch
            {
                throw;
            }
        }
        public int Publish_Role(DataTable dtRole)
        {
            try
            {
                return objDAL.Publish_Role_DL(dtRole);
            }
            catch
            {
                throw;
            }
        }
        public int Publish_RoleMenuAcces(DataTable dtRoleMenuAcces)
        {
            try
            {
                return objDAL.Publish_RoleMenuAcces_DL(dtRoleMenuAcces);
            }
            catch
            {
                throw;
            }
        }
        public int InsertJITMENU(int Menu_Code, int Mod_Code, int Menu_Type, string Menu_Short_Discription, string Menu_Link, string Menu_Discription, int Created_By, DateTime Date_Of_Created, int Modified_By, DateTime Date_Of_Modified, int Deleted_By, DateTime Date_Of_Deleted
      , int Active_Status, int Priority_Sequence, int Sequence_Order, int Menu_Enable, int Access_Admin, int DepID)
        {
            return objDAL.InsertJITMENU(Menu_Code, Mod_Code, Menu_Type, Menu_Short_Discription, Menu_Link, Menu_Discription, Created_By, Date_Of_Created, Modified_By, Date_Of_Modified, Deleted_By, Date_Of_Modified,
                Active_Status, Priority_Sequence,Sequence_Order, Menu_Enable, Access_Admin, DepID);
        }

        public int SyncMenuAccess(DataTable dt)
        {
            return objDAL.SyncMenuAccess(dt);
        }
        //Method sends the filters to DB and Gets the records
        public DataSet Get_AccessRightChanges(DateTime? FromDate, DateTime? ToDate, int? Pageindex, int? Pagesize, string Sortcoloumn, int? SortOrder, int? UserID, string ModuleName, string PageName,int? CurrentUserID, ref int Count)
        {
            try
            {
                return objDAL.Get_AccessRightChanges_DL(FromDate, ToDate, Pageindex, Pagesize, Sortcoloumn, SortOrder, UserID, ModuleName, PageName, CurrentUserID, ref Count);
            }
            catch
            {
                throw;
            }
        }

        //Method Gets the newly changed in to search table 
        public void Get_AccessRightChanges()
        {
            try
            {
                objDAL.Get_AccessRightIncrementalData_DL();
            }
            catch
            {
                throw;
            }
        }

        //Method sends the filters to DB and Gets the records
        public DataTable Get_AccessRightAll(int MenuID)
        {
            return objDAL.Get_AccessRightAll_Dl(MenuID);
        }

        /// <summary>
        /// Get all company list
        /// </summary>
        /// <returns></returns>
        public DataTable INF_Get_CompanyList()
        {
            try
            {
                return objDAL.INF_Get_CompanyList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get all department list
        /// </summary>
        /// <param name="dtCompanyId">selected company id </param>
        /// <returns></returns>
        public DataTable INF_Get_DepartmentList()
        {
            try
            {
                return objDAL.INF_Get_DepartmentList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get all user list
        /// </summary>
        /// <param name="dtCompanyId">selected company id </param>
        /// <returns></returns>
        public DataTable INF_Get_UserList(DataTable dtCompanyId, DataTable dtDepartmentId)
        {
            try
            {
                return objDAL.INF_Get_UserList(dtCompanyId, dtDepartmentId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get all module list
        /// </summary>
        /// <returns></returns>
        public DataTable INF_Get_PageList(DataTable dtMenuId)
        {
            try
            {
                return objDAL.INF_Get_PageList(dtMenuId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get all menu list
        /// </summary>
        /// <param name="dtModuleId">selected module id</param>
        /// <returns></returns>
        public DataTable INF_Get_MenuList()
        {
            try
            {
                return objDAL.INF_Get_MenuList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get all sub menu list
        /// </summary>
        /// <param name="dtModuleId">selected menu id</param>
        /// <returns></returns>
        public DataTable INF_Get_SubMenuList(DataTable dtMenuId)
        {
            try
            {
                return objDAL.INF_Get_SubMenuList(dtMenuId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get access rights of office user
        /// </summary>
        /// <param name="dtCompanyId">selected company</param>
        /// <param name="dtMenuId">selected menu</param>
        /// <param name="dtDepartmentId">Selected deparment</param>
        /// <param name="dtSubMenuId">selected sub menu</param>
        /// <param name="dtUserId">selected user</param>
        /// <param name="dtPageId"> selected page</param>
        /// <param name="sortby">sort by column</param>
        /// <param name="sortdirection"> ASC/DESC</param>
        /// <param name="pagenumber">page number </param>
        /// <param name="pagesize"> page size to display data</param>
        /// <param name="isfetchcount">Total affected count</param>
        /// <returns></returns>
        public DataSet INF_Get_AccessRightsReport(DataTable dtCompanyId, DataTable dtMenuId, DataTable dtDepartmentId, DataTable dtSubMenuId, DataTable dtUserId, DataTable dtPageId, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objDAL.INF_Get_AccessRightsReport(dtCompanyId, dtMenuId, dtDepartmentId, dtSubMenuId, dtUserId, dtPageId, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
    }
}