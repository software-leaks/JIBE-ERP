using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.LMS
{
     
    public class DAL_LMS_Faq
    {
        SqlConnection conn;
        private static string connection = "";

        public DAL_LMS_Faq()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
         public DataTable Get_UserDetails_DL(int UserID)
         {
             SqlParameter[] obj = new SqlParameter[]{  
                
                                                      new SqlParameter("@UserID",UserID)
   												  };
             DataSet ds = new DataSet();
             return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserDetails", obj).Tables[0];
         }
        static DAL_LMS_Faq()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_FAQ_Details(int faq_id)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@faq_id", faq_id) ,                
                    
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_LMS_Faq.connection, CommandType.StoredProcedure, "LMS_GET_FAQ_Details", obj);

            return ds.Tables[0];
        }

        public static DataTable Ins_Attachment(int FAQ_ID, int UserID, DataTable AttachmentList, int Sync_To_Vessel)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
                 new SqlParameter("@FAQ_ID",FAQ_ID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@AttachmentList",AttachmentList),
                new SqlParameter("@SYNC_TO_VESSEL",Sync_To_Vessel)
                         
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_INS_FAQ_ATTACHMENTS", prm).Tables[0];
        }

        public static int Upd_Faq_Details(int FAQ_ID, string Questione, string Answer, int UserID, int? TopicID)
        {
            //var returnValue = 0;
             SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@FAQ_ID",FAQ_ID),
                                            new SqlParameter("@Question",Questione),
                                            new SqlParameter("@Answer",Answer),
                                            new SqlParameter("@UserID",UserID),
                                             new SqlParameter("@TopicID",TopicID),
                                            new SqlParameter("Return", SqlDbType.Int)
                                        };
             sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
             SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_UPD_FAQ_Details", sqlprm);
             return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return returnValue;
        }
        public static int Upd_Faq_Details(int FAQ_ID, string Questione, string Answer, int UserID, int? TopicID, string Menu_Link)
        {
            //var returnValue = 0;
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@FAQ_ID",FAQ_ID),
                                            new SqlParameter("@Question",Questione),
                                            new SqlParameter("@Answer",Answer),
                                            new SqlParameter("@UserID",UserID),
                                             new SqlParameter("@TopicID",TopicID),
                                                 new SqlParameter("@Menu_Link",Menu_Link),
                                            new SqlParameter("Return", SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_UPD_FAQ_Details", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            //return returnValue;
        }
        public static DataTable Check_Faq_List(string Question, string Answer, int? TopicID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Question", Question) ,   
                  new System.Data.SqlClient.SqlParameter("@Answer", Answer),
                   new System.Data.SqlClient.SqlParameter("@TopicID",TopicID)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_LMS_Faq.connection, CommandType.StoredProcedure, "LMS_check_FAQ_List", obj);

            return ds.Tables[0];
        }
       
        public static int Ins_Compare_Attachment(int FAQ_ID, int UserID, DataTable AttachmentList)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
                 new SqlParameter("@FAQ_ID",FAQ_ID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@AttachmentList",AttachmentList)
              
                             
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_INS_COMPARE_FAQ", prm);
        }
        public static int Del_Faq_Attachment(string ATTACHMENT_NAME, int Sync_To_Vessel)
        {
          
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@ATTACHMENT_NAME", ATTACHMENT_NAME),                
                new SqlParameter("@SYNC_TO_VESSEL",Sync_To_Vessel)  
                    
            };

          return SqlHelper.ExecuteNonQuery(DAL_LMS_Faq.connection, CommandType.StoredProcedure, "LMS_Del_FAQ_Attachment", obj);

            
        }

      
        public static DataSet Get_FAQ_List(string SearchFaq, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@SearchFaq",SearchFaq),
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_FAQ_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }
        
    }

    }
