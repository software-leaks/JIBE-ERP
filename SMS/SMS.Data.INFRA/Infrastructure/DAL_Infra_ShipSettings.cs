using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_ShipSettings
    {

        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Get_Projects_DL(int Project_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Project_ID",Project_ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Projects_List", sqlprm).Tables[0];
        }

        public static DataTable Get_Modules_DL(int Module_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Module_ID",Module_ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Modules_List", sqlprm).Tables[0];
        }

        public static DataTable Get_Screens_DL(int Screen_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Screen_ID",Screen_ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Screens_List", sqlprm).Tables[0];
        }

        public static DataTable Get_Project_Search_DL(string SearchText, string sortby, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Projects_Search", sqlprm).Tables[0];
        }

        public static DataTable Get_Module_Search_DL(string SearchText, int? Project_ID, string sortby, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Project_ID",Project_ID),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Modules_Search", sqlprm).Tables[0];
        }

        public static DataTable Get_Screen_Search_DL(string SearchText, int? Module_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Module_ID",Module_ID),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                            new SqlParameter("@PAGENUMBER",pagenumber),
                                            new SqlParameter("@PAGESIZE",pagesize),
                                            new SqlParameter("@ISFETCHCOUNT",isfetchcount),
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Screens_Search", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataTable Get_Screen_Assign_Search_DL(string SearchText, string Class_Name, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Class_Name",Class_Name),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                            new SqlParameter("@PAGENUMBER",pagenumber),
                                            new SqlParameter("@PAGESIZE",pagesize),
                                            new SqlParameter("@ISFETCHCOUNT",isfetchcount),
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Screens_Assign_Search", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }



        public static int Ins_Upd_Assign_Child_Screen_DL(int? Nav_Module_ID,int ChkStatus , int? Screen_ID, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Nav_Module_ID",Nav_Module_ID),
                                           new SqlParameter("@Screen_ID",Screen_ID),
                                           new SqlParameter("@ChkStatus",ChkStatus),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Assign_Child_Screen", sqlprm);
        }



        public static int Ins_Upd_Project_DL(int? Project_ID, string Project_Name, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@Project_Name",Project_Name),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Projects", sqlprm);
        }

        public static int Ins_Upd_Modules_DL(int? Module_ID, int Project_ID, string Module_Name, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Module_ID",Module_ID),
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@Module_Name",Module_Name),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Modules", sqlprm);
        }

        public static int Ins_Upd_Screens_DL(int? Screen_ID, int ScreenType ,int Module_ID, string Screen_Name, string Class_Name,string Assembly_Name,string Image_Path, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Screen_ID",Screen_ID),
                                            new SqlParameter("@ScreenType",ScreenType),
                                            new SqlParameter("@Module_ID",Module_ID),
                                            new SqlParameter("@Screen_Name",Screen_Name),
                                            new SqlParameter("@Class_Name",Class_Name),
                                            new SqlParameter("@Assembly_Name",Assembly_Name),
                                            new SqlParameter("@Image_Path",Image_Path),
                                            new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Screen", sqlprm);
        }

        public static int Delete_Projects_DL(int? Project_ID, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Delete_Projects", sqlprm);
        }

        public static int Delete_Modules_DL(int? Module_ID, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Module_ID",Module_ID),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Delete_Modules", sqlprm);
        }

        public static int Delete_Screen_DL(int? Screen_ID, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Screen_ID",Screen_ID),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Delete_Screen", sqlprm);
        }

        public static DataTable Get_Nav_Projects_DL(int Project_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Project_ID",Project_ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Nav_Projects_List", sqlprm).Tables[0];
        }

        public static DataTable Get_Nav_Modules_DL(int Module_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Module_ID",Module_ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Nav_Modules_List", sqlprm).Tables[0];
        }

        public static DataTable Get_Nav_Project_Search_DL(string SearchText, string sortby, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Nav_Projects_Search", sqlprm).Tables[0];
        }

        public static DataTable Get_Nav_Module_Search_DL(string SearchText, int? Project_ID, string sortby, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Project_ID",Project_ID),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Nav_Modules_Search", sqlprm).Tables[0];
        }

        public static int Ins_Upd_Nav_Project_DL(int? Project_ID,int Screen_ID , string Name, string Image_path, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@Screen_ID",Screen_ID),
                                           new SqlParameter("@Name",Name),
                                           new SqlParameter("@Image_path",Image_path),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Nav_Projects", sqlprm);
        }

        public static int Ins_Upd_Nav_Modules_DL(int? Module_ID, int Project_ID, int Screen_ID, string Name, string Image_path, int? Default_Module, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Module_ID",Module_ID),
                                            new SqlParameter("@Project_ID",Project_ID),
                                            new SqlParameter("@Screen_ID",Screen_ID),
                                            new SqlParameter("@Name",Name),
                                            new SqlParameter("@Image_path",Image_path),
                                            new SqlParameter("@Default_Module",Default_Module),
                                            new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Nav_Modules", sqlprm);
        }
        
        public static int Delete_Nav_Projects_DL(int? Project_ID, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Delete_Nav_Projects", sqlprm);
        }

        public static int Delete_Nav_Modules_DL(int? Module_ID, int USER_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Module_ID",Module_ID),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Delete_Nav_Modules", sqlprm);
        }



        public static int Default_Nav_Module_DL(int? Module_ID, int USER_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Module_ID",Module_ID),
                                           new SqlParameter("@USER_ID",USER_ID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Default_Nav_Modules", sqlprm);
        }



        public static DataTable Get_Project_Templete_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Project_Templete").Tables[0];
        }

        public static DataTable Get_Module_Screens_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Module_Screen").Tables[0];
        }

        public static DataSet Get_Project_Module_Tree_DL(int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                     
                                           new SqlParameter("@UserID",UserID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Project_Module_Tree", sqlprm);
        
        }


        public static int Ins_Upd_Rank_Menu_Acess_DL(int? Menu_ID, int Vessel_ID, int Rank_ID, int Screen_ID, int Access_Menu, int Access_View
            , int Access_Add, int Access_Edit, int Access_Delete, int Access_Approve, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Menu_ID",Menu_ID),
                                           new SqlParameter("@Vessel_ID",Vessel_ID),
                                           new SqlParameter("@Rank_ID",Rank_ID),
                                           new SqlParameter("@Screen_ID",Screen_ID),
                                           new SqlParameter("@Access_Menu",Access_Menu),
                                           new SqlParameter("@Access_View",Access_View),
                                           new SqlParameter("@Access_Add",Access_Add),
                                           new SqlParameter("@Access_Edit",Access_Edit),
                                           new SqlParameter("@Access_Delete",Access_Delete),
                                           new SqlParameter("@Access_Approve",Access_Approve),
                                           new SqlParameter("@UserID",UserID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Ins_Upd_Rank_Menu_Access", sqlprm);
        }


        public static int Ins_Remove_Rank_Menu_Acess_DL(int? Menu_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Menu_ID",Menu_ID),
                                           new SqlParameter("@UserID",UserID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_REMOVE_RANK_MENU_ACCESS", sqlprm);
        }

        public static int Copy_Rank_Menu_Acess_DL(int FromVessel_ID, int FromRank_ID, int ToVessel_ID, int? ToRank_ID, int Remove_Retain_Flage, int All_Selected_Module_Flage, int? Project_ID, int? Module_ID, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@FromVessel_ID",FromVessel_ID),
                                           new SqlParameter("@FromRank_ID",FromRank_ID),
                                           new SqlParameter("@ToVessel_ID",ToVessel_ID),
                                           new SqlParameter("@ToRank_ID",ToRank_ID),
                                           new SqlParameter("@Remove_Retain_Flage",Remove_Retain_Flage),
                                           new SqlParameter("@All_Selected_Module_Flage",All_Selected_Module_Flage),
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@Module_ID",Module_ID),
                                           new SqlParameter("@UserID",UserID),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Copy_Rank_Menu_Access", sqlprm);

        }


        public static DataTable Get_Rank_Menu_Acess_DL(int? Project_ID, int? Module_ID, int? Vessel_ID, int? Rank_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Project_ID",Project_ID),
                                           new SqlParameter("@Module_ID",Module_ID),
                                           new SqlParameter("@Vessel_ID",Vessel_ID),
                                           new SqlParameter("@Rank_ID",Rank_ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Get_Rank_Menu_Access", sqlprm).Tables[0];
        
        }




        public static DataTable Get_Help_File_Settings_Search(string SearchText, int? Screen_ID, int? Module_ID
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   

                new System.Data.SqlClient.SqlParameter("@SearchText",SearchText),
                new System.Data.SqlClient.SqlParameter("@Screen_ID",Screen_ID),
                new System.Data.SqlClient.SqlParameter("@Module_ID",Module_ID),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_Get_Help_File_Settings_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }



        public static DataTable Get_Help_File_Settings_List(int? ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",ID),
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_Help_File_Settings_List", sqlprm).Tables[0];

        }


        public static int UPD_Help_File_Settings(int ID, int Screen_ID, string Topic_ID, string Topic_Description, string Help_File_Name, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",ID),
                                           new SqlParameter("@Screen_ID",Screen_ID),
                                           new SqlParameter("@Topic_ID",Topic_ID),
                                           new SqlParameter("@Topic_Description",Topic_Description),
                                           new SqlParameter("@Help_File_Name",Help_File_Name),
                                           new SqlParameter("@Created_By",Created_By),
                                        
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_UPD_Help_File_Settings_DETAILS", sqlprm);

        }


        public static int INS_Help_File_Settings(int Screen_ID, string Topic_ID, string Topic_Description, string Help_File_Name, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Screen_ID",Screen_ID),
                                           new SqlParameter("@Topic_ID",Topic_ID),
                                           new SqlParameter("@Topic_Description",Topic_Description),
                                           new SqlParameter("@Help_File_Name",Help_File_Name),
                                           new SqlParameter("@Created_By",Created_By),
                                        
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_INS_Help_File_Settings_DETAILS", sqlprm);

        }

        public static int DEL_Help_File_Settings(int Screen_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",Screen_ID),
                                           new SqlParameter("@Created_By",Created_By),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_DEL_Help_File_Settings_DETAILS", sqlprm);
        }

        public static DataTable Get_RankList_DL(int Screen_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",Screen_ID)                                  
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_RankList",sqlprm).Tables[0];
        }

               public static DataTable Check_Record_Exist_Dtails_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",ID)                                  
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_RANK_SCREEN", sqlprm).Tables[0];
        }
        public static int Ins_RANK_SCREEN_Access_DL(int Screen_ID, string Key, int Created_By, ref int ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ScreenID",Screen_ID),                                          
                                           new SqlParameter("@Key",Key),                                           
                                           new SqlParameter("@Created_By",Created_By),                                      
                                        new SqlParameter("@ID",ID),
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
             SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_INS_RANK_SCREEN_Access", sqlprm);          
            return ID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public static int Ins_RANK_SCREEN_Access_Details_DL(DataTable dt)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@dt",dt)                                      
                                         
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_INS_RANK_SCREEN_Access_Details", sqlprm);          
            
        }




        public static int Update_RANK_SCREEN_Access_DL(int ID, string Key, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",ID),                                          
                                           new SqlParameter("@Key",Key),                                           
                                           new SqlParameter("@Created_By",Created_By),                                       
                                        
                                        };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_UPDT_RANK_SCREEN_Access", sqlprm);            
        }


        public static int Update_RANK_SCREEN_Access_Details_DL(DataTable dt,int ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@dt",dt),                                     
                                          new SqlParameter("@ID",ID)  
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_UPDT_RANK_SCREEN_Access_Details", sqlprm);

        }

        public static DataTable Get_Exist_Dtails_ByScreenID_DL(int screenID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ScreenID",screenID)                                  
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_RANK_SCREEN_BY_ScreenID", sqlprm).Tables[0];
        }

        public static int Delete_RANK_SCREEN_Access_Details_DL(int Details_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Details_ID",Details_ID),   
                                             new SqlParameter("@Created_By",Created_By)      
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "Delete_RANK_SCREEN_Access_Details", sqlprm);
        }

    }



}
