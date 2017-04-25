using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_ShipSettings
    {

        public static DataTable Get_Projects(int Project_ID)
        {
            return DAL_Infra_ShipSettings.Get_Projects_DL(Project_ID);
        }


        public static DataTable Get_Modules(int Module_ID)
        {
            return DAL_Infra_ShipSettings.Get_Modules_DL(Module_ID);
        }


        public static DataTable Get_Screens(int Screen_ID)
        {
            return DAL_Infra_ShipSettings.Get_Screens_DL(Screen_ID);
        }

        public static DataTable Get_Project_Search(string SearchText, string sortby, int? sortdirection)
        {
            return DAL_Infra_ShipSettings.Get_Project_Search_DL(SearchText, sortby, sortdirection);
        }

        public static DataTable Get_Module_Search(string SearchText, int? Project_ID, string sortby, int? sortdirection)
        {
            return DAL_Infra_ShipSettings.Get_Module_Search_DL(SearchText, Project_ID, sortby, sortdirection);
        }

        public static DataTable Get_Screen_Search(string SearchText, int? Module_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Infra_ShipSettings.Get_Screen_Search_DL(SearchText, Module_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable Get_Screen_Assign_Search(string SearchText, string Class_Name, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Infra_ShipSettings.Get_Screen_Assign_Search_DL(SearchText, Class_Name, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public static int Ins_Upd_Assign_Child_Screen(int? Nav_Module_ID, int ChkStatus, int? Screen_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Assign_Child_Screen_DL(Nav_Module_ID, ChkStatus, Screen_ID, USER_ID);
        }



        public static int Ins_Upd_Project(int? Project_ID, string Project_Name, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Project_DL(Project_ID, Project_Name, USER_ID);
        }

        public static int Ins_Upd_Modules(int? Module_ID, int Project_ID, string Module_Name, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Modules_DL(Module_ID, Project_ID, Module_Name, USER_ID);
        }

        public static int Ins_Upd_Screens(int? Screen_ID, int ScreenType, int Module_ID, string Screen_Name, string Class_Name, string Assembly_Name, string Image_Path, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Screens_DL(Screen_ID, ScreenType, Module_ID, Screen_Name, Class_Name, Assembly_Name, Image_Path, USER_ID);
        }

        public static int Delete_Projects(int? Project_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Delete_Projects_DL(Project_ID, USER_ID);
        }

        public static int Delete_Modules(int? Module_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Delete_Modules_DL(Module_ID, USER_ID);
        }

        public static int Delete_Screen(int? Screen_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Delete_Screen_DL(Screen_ID, USER_ID);
        }

        public static DataTable Get_Nav_Projects(int Project_ID)
        {
            return DAL_Infra_ShipSettings.Get_Nav_Projects_DL(Project_ID);
        }

        public static DataTable Get_Nav_Modules(int Module_ID)
        {
            return DAL_Infra_ShipSettings.Get_Nav_Modules_DL(Module_ID);
        }

        public static DataTable Get_Nav_Project_Search(string SearchText, string sortby, int? sortdirection)
        {
            return DAL_Infra_ShipSettings.Get_Nav_Project_Search_DL(SearchText, sortby, sortdirection);
        }

        public static DataTable Get_Nav_Module_Search(string SearchText, int? Project_ID, string sortby, int? sortdirection)
        {
            return DAL_Infra_ShipSettings.Get_Nav_Module_Search_DL(SearchText, Project_ID, sortby, sortdirection);
        }

        public static int Ins_Upd_Nav_Project(int? Project_ID, int Screen_ID, string Name, string Image_path, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Nav_Project_DL(Project_ID, Screen_ID, Name, Image_path, USER_ID);
        }

        public static int Ins_Upd_Nav_Modules(int? Module_ID, int Project_ID, int Screen_ID, string Name, string Image_path, int? Default_Module, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Nav_Modules_DL(Module_ID, Project_ID, Screen_ID, Name, Image_path, Default_Module, USER_ID);
        }

        public static int Delete_Nav_Projects(int? Project_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Delete_Nav_Projects_DL(Project_ID, USER_ID);
        }

        public static int Delete_Nav_Modules(int? Module_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Delete_Nav_Modules_DL(Module_ID, USER_ID);
        }


        public static int Default_Nav_Module(int? Module_ID, int USER_ID)
        {
            return DAL_Infra_ShipSettings.Default_Nav_Module_DL(Module_ID, USER_ID);
        }



        public static DataTable Get_Project_Templete()
        {

            return DAL_Infra_ShipSettings.Get_Project_Templete_DL();

        }


        public static DataTable Get_Module_Screens()
        {

            return DAL_Infra_ShipSettings.Get_Module_Screens_DL();

        }

        public static DataSet Get_Project_Module_Tree(int UserID)
        {
            return DAL_Infra_ShipSettings.Get_Project_Module_Tree_DL(UserID);

        }


        public static int Ins_Upd_Rank_Menu_Acess(int? Menu_ID, int Vessel_ID, int Rank_ID, int Screen_ID, int Access_Menu, int Access_View
        , int Access_Add, int Access_Edit, int Access_Delete, int Access_Approve, int UserID)
        {
            return DAL_Infra_ShipSettings.Ins_Upd_Rank_Menu_Acess_DL(Menu_ID, Vessel_ID, Rank_ID, Screen_ID, Access_Menu, Access_View
                               , Access_Add, Access_Edit, Access_Delete, Access_Approve, UserID);

        }

        public static int Ins_Remove_Rank_Menu_Acess(int? Menu_ID, int UserID)
        {
            return DAL_Infra_ShipSettings.Ins_Remove_Rank_Menu_Acess_DL(Menu_ID, UserID);
        }


        public static int Copy_Rank_Menu_Acess(int FromVessel_ID, int FromRank_ID, int ToVessel_ID, int? ToRank_ID, int Remove_Retain_Flage, int All_Selected_Module_Flage, int? Project_ID, int? Module_ID, int UserID)
        {
            return DAL_Infra_ShipSettings.Copy_Rank_Menu_Acess_DL(FromVessel_ID, FromRank_ID, ToVessel_ID, ToRank_ID, Remove_Retain_Flage, All_Selected_Module_Flage, Project_ID, Module_ID, UserID);
        }

        public static DataTable Get_Rank_Menu_Acess(int? Project_ID, int? Module_ID, int? Vessel_ID, int? Rank_ID)
        {
            return DAL_Infra_ShipSettings.Get_Rank_Menu_Acess_DL(Project_ID, Module_ID, Vessel_ID, Rank_ID);
        }


        public static DataTable Get_Help_File_Settings_Search(string SearchText, int? Screen_ID, int? Module_ID
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_Infra_ShipSettings.Get_Help_File_Settings_Search(SearchText, Screen_ID, Module_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable Get_Help_File_Settings_List(int? ID)
        {
            return DAL_Infra_ShipSettings.Get_Help_File_Settings_List(ID);
        }

        public static int UPD_Help_File_Settings(int ID, int Screen_ID, string Topic_ID, string Topic_Description, string Help_File_Name, int Created_By)
        {
            return DAL_Infra_ShipSettings.UPD_Help_File_Settings(ID, Screen_ID, Topic_ID, Topic_Description, Help_File_Name, Created_By);
        }

        public static int INS_Help_File_Settings(int Screen_ID, string Topic_ID, string Topic_Description, string Help_File_Name, int Created_By)
        {
            return DAL_Infra_ShipSettings.INS_Help_File_Settings(Screen_ID, Topic_ID, Topic_Description, Help_File_Name, Created_By);
        }

        public static int DEL_Help_File_Settings(int Screen_ID, int Created_By)
        {
            return DAL_Infra_ShipSettings.DEL_Help_File_Settings(Screen_ID, Created_By);
        }

        public static DataTable Get_RankList(int screenID)
        {
            try
            {
                return DAL_Infra_ShipSettings.Get_RankList_DL(screenID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Check_Record_Exist_Dtails(int ID)
        {
            try
            {
                return DAL_Infra_ShipSettings.Check_Record_Exist_Dtails_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public static int Ins_RANK_SCREEN_Access(int Screen_ID, string Key, int Created_By,ref int ID)
        {
            return DAL_Infra_ShipSettings.Ins_RANK_SCREEN_Access_DL(Screen_ID,Key,Created_By,ref ID);
        }

        public static int Ins_RANK_SCREEN_Access_Details(DataTable dt)
        {
            return DAL_Infra_ShipSettings.Ins_RANK_SCREEN_Access_Details_DL(dt);
        }
        public static int Update_RANK_SCREEN_Access(int ID, string Key, int Created_By)
        {
            return DAL_Infra_ShipSettings.Update_RANK_SCREEN_Access_DL(ID, Key, Created_By);
        }

        public static int Update_RANK_SCREEN_Access_Details(DataTable dt,int ID)
        {
            return DAL_Infra_ShipSettings.Update_RANK_SCREEN_Access_Details_DL(dt,ID);
        }
        public static DataTable Get_Exist_Dtails_ByScreenID(int ScreenID)
        {
            try
            {
                return DAL_Infra_ShipSettings.Get_Exist_Dtails_ByScreenID_DL(ScreenID);
            }
            catch
            {
                throw;
            }
        }
        public static int Delete_RANK_SCREEN_Access_Details(int Details_ID,int Created_By)
        {
            try
            {
                return DAL_Infra_ShipSettings.Delete_RANK_SCREEN_Access_Details_DL(Details_ID, Created_By);
            }
            catch
            {
                throw;
            }
        }
    }
}
