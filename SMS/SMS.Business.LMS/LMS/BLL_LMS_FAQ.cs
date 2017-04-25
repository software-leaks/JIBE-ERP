using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.LMS;
using System.Data.SqlClient;

namespace SMS.Business.LMS
{
    public class BLL_LMS_FAQ
    {
        DAL_LMS_Faq objDAL = new DAL_LMS_Faq();
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);


        public static int Upd_Faq_Details(int FAQ_ID, string Question, string Answer, int UserID, int? TopicID)
        {
            return DAL_LMS_Faq.Upd_Faq_Details(FAQ_ID, Question, Answer, UserID, TopicID);
        }
        public static int Upd_Faq_Details(int FAQ_ID, string Question, string Answer, int UserID, int? TopicID, string Menu_Link)
        {
            return DAL_LMS_Faq.Upd_Faq_Details(FAQ_ID, Question, Answer, UserID, TopicID, Menu_Link);
        }
        public static DataTable Get_FAQ_Details(int FAQ_ID)
        {
            return DAL_LMS_Faq.Get_FAQ_Details(FAQ_ID);

        }  
        
        public static DataTable Ins_Attachment(int FAQ_ID, int UserID, DataTable AttachmentList,int Sync_To_Vessel)
        {
            return DAL_LMS_Faq.Ins_Attachment(FAQ_ID, UserID, AttachmentList, Sync_To_Vessel);

        }    
        
        public static DataSet Get_FAQ_List(String SearvhFaq, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_LMS_Faq.Get_FAQ_List(SearvhFaq, Page_Index, Page_Size, ref is_Fetch_Count);
        }

        public static DataTable Check_Faq_List(string Question, string Answer, int? TopicID)
        {
            return DAL_LMS_Faq.Check_Faq_List(Question, Answer, TopicID);
        }


        public static int Del_Faq_Attachment(string ATTACHMENT_NAME, int Sync_To_Vessel)
        {
            return DAL_LMS_Faq.Del_Faq_Attachment(ATTACHMENT_NAME, Sync_To_Vessel);
        }
        public static int Ins_Compare_Attachment(int FAQ_ID, int UserID, DataTable AttachmentList)
        {
            return DAL_LMS_Faq.Ins_Compare_Attachment(FAQ_ID, UserID, AttachmentList);

        }
      
        public DataTable Get_UserDetails(int userid)
        {
            try
            {

                return objDAL.Get_UserDetails_DL(userid);
            }
            catch
            {
                throw;
            }

        }
    }
}