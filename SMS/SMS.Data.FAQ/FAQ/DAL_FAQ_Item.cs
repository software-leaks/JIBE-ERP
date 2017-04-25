using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.FAQ
{
    public class DAL_FAQ_Item
    {
        private static string connection = "";
        static DAL_FAQ_Item()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DAL_FAQ_Item()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        //*** Help Center ***//
        public static DataSet Get_FAQModule_List()
        {
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_Module_List");
            return ds;
        }

        public static DataSet Get_FAQTopic_List(int Module_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Module_ID", Module_ID) ,                      
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_Topic_List", obj);
            return ds;
        }
        public static DataSet Get_Topic_FAQList(int? Topic_ID, int? FAQ_ID, string Description, int? pagenumber, int? pagesize, ref int isfetchcount, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                    new System.Data.SqlClient.SqlParameter("@Topic_ID", Topic_ID) ,  
                    new System.Data.SqlClient.SqlParameter("@FAQ_ID", FAQ_ID) ,  
                    new System.Data.SqlClient.SqlParameter("@Description", Description) ,  
                    new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                    new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                    new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_List", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
        public static int Update_FAQ_List(int FAQ_ID, int IsHelpful, int? UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@FAQ_ID", FAQ_ID) ,     
                  new System.Data.SqlClient.SqlParameter("@IsHelpful", IsHelpful) ,
                  new System.Data.SqlClient.SqlParameter("@Reported_By", UserID) , 
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_SP_Upd_FAQ_IsHelpful", obj);
        }
        public static DataSet Get_FAQ_Link(int FAQ_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
              
                  new System.Data.SqlClient.SqlParameter("@FAQ_ID", FAQ_ID) ,  
             
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_ListItems", obj);
            return ds;
        }
        public DataSet GetFAQDescList(string sText)
        {


            SqlParameter sqlprm = new SqlParameter("@Search_Text", sText);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_AutoText", sqlprm);

        }
        public static DataSet Get_FAQ_Items(int FAQ_ID, string Search_Text)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
              
                  new System.Data.SqlClient.SqlParameter("@FAQ_ID", FAQ_ID) ,  
                  new System.Data.SqlClient.SqlParameter("@Search_Text", Search_Text) ,  
             
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_Items", obj);
            return ds;
        }
        public static int Update_FAQ_Items(int FAQ_ID, DataTable tbl_FAQ, int? User_ID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@FAQ_ID", FAQ_ID),
                                                        new SqlParameter("@tbl_FAQ", tbl_FAQ),
                                                        new SqlParameter("@User_ID", User_ID)
                                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_SP_Upd_FAQ_Items", prm);
        }
        public static int Update_TrainingItem_IsHelpful(int Video_ID, int IsHelpful, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Video_ID", Video_ID) ,     
                  new System.Data.SqlClient.SqlParameter("@IsHelpful", IsHelpful) ,
                  new System.Data.SqlClient.SqlParameter("@UserID", UserID) , 
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_SP_Upd_TrainingItem_IsHelpful", obj);
        }
        public static DataTable Get_TopicModule(int Topic_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Topic_ID", Topic_ID) ,                      
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_TopicModule", obj).Tables[0];

        }
        public static int Update_ModuleTopic(string Desc, int? ID, string Mode, int? ModuleID, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Desc", Desc) ,     
                  new System.Data.SqlClient.SqlParameter("@ID", ID) ,
                  new System.Data.SqlClient.SqlParameter("@Mode", Mode) , 
                   new System.Data.SqlClient.SqlParameter("@ModuleID", ModuleID) , 
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID) , 
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_UPD_FAQ_ModuleTopic", obj);
        }
        public static DataSet Get_ModuleTopic_Details(string Search, int? pagenumber, int? pagesize, string Mode, int? ID, int? ModuleID, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                    new System.Data.SqlClient.SqlParameter("@Search", Search) ,  
                    new System.Data.SqlClient.SqlParameter("@PAGENUMBER", pagenumber) ,  
                    new System.Data.SqlClient.SqlParameter("@PAGESIZE", pagesize) ,  
                    new System.Data.SqlClient.SqlParameter("@Mode",Mode),
                    new System.Data.SqlClient.SqlParameter("@ID",ID),
                    new System.Data.SqlClient.SqlParameter("@ModuleID",ModuleID),
                    new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_SP_Get_FAQ_ModuleTopic", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
        public static void Del_ModuleTopic(string Mode, int ID, int UserID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@ID", ID),                
                     new SqlParameter("@Mode",Mode) ,
                     new SqlParameter("@UserID",UserID)  

                    
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Del_FAQ_ModuleTopic", obj);


        }
        public static void Upd_Faq_ServiceDetails(int FAQ_ID, string Question, string Answer, int UserID, int TopicID, string Topic_Description, int ModuleID, string Module_Description)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                     new SqlParameter("@FAQ_ID", FAQ_ID),                
                     new SqlParameter("@Question",Question) ,
                     new SqlParameter("@Answer",Answer)  ,
                     new SqlParameter("@UserID", UserID),                
                     new SqlParameter("@TopicID",TopicID) ,
                     new SqlParameter("@Topic_Description",Topic_Description)  ,
                     new SqlParameter("@ModuleID", ModuleID),                
                     new SqlParameter("@Module_Description",Module_Description) ,
                    
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_UPD_FAQ_ByService", obj);
        }
        public static void Del_Faq_ServiceDetails(int FAQ_ID, int UserID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                     new SqlParameter("@FAQ_ID", FAQ_ID),                
                     new SqlParameter("@UserID", UserID),                
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_DEL_FAQ_ByService", obj);
        }
        public static DataSet Get_HelpPage_List(string SearchFaq, ref int is_Fetch_Count, int? Module, int? Topic, string Keywords, int Company)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@SearchFaq",SearchFaq),
                                                       new SqlParameter("@Module",Module),
                                                       new SqlParameter("@Topic",Topic),
                                                       new SqlParameter("@Keywords",Keywords),
                                                        new SqlParameter("@CompanyID",Company),
                                                        new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),
                                                       
                                                        
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_HelpPage_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }
        public static DataSet Get_HelpPage_List(string SearchFaq, ref int is_Fetch_Count, int? Module, int? Topic, string Keywords, int Company, string Searchurl)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@SearchFaq",SearchFaq),
                                                       new SqlParameter("@Module",Module),
                                                       new SqlParameter("@Topic",Topic),
                                                       new SqlParameter("@Keywords",Keywords),
                                                        new SqlParameter("@CompanyID",Company),
                                                         new SqlParameter("@Searchurl",Searchurl), 
                                                        new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count), 
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_HelpPage_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }
        public static DataSet Get_Menu_Link()
        {
      

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_MENU_LINK", null);
          
            return ds;

        }
         
    }
}
