using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.FAQ;
using System.Data.SqlClient;

namespace SMS.Business.FAQ
{
    public class BLL_FAQ_Item
    {

        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataSet Get_FAQModule_List()
        {
            try
            {

                return DAL_FAQ_Item.Get_FAQModule_List();
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_FAQTopic_List(int Module_ID)
        {
            try
            {

                return DAL_FAQ_Item.Get_FAQTopic_List(Module_ID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Topic_FAQList(int? Topic_ID, int? FAQ_ID, string Description, int? pagenumber, int? pagesize, ref int isfetchcount, int UserID)
        {
            try
            {

                return DAL_FAQ_Item.Get_Topic_FAQList(Topic_ID, FAQ_ID, Description, pagenumber, pagesize, ref isfetchcount, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_FAQ_List(int FAQ_ID, int IsHelpful, int? UserID)
        {
            try
            {

                return DAL_FAQ_Item.Update_FAQ_List(FAQ_ID, IsHelpful, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_FAQ_Link(int FAQ_ID)
        {
            try
            {

                return DAL_FAQ_Item.Get_FAQ_Link(FAQ_ID);
            }
            catch
            {
                throw;
            }
        }
        public DataSet GetFAQDescList(string Search_Text)
        {
            DAL_FAQ_Item al = new DAL_FAQ_Item();
            try { return al.GetFAQDescList(Search_Text); }
            catch { throw; }
            finally { al = null; }
        }

        public static DataSet Get_FAQ_Items(int FAQ_ID, string Search_Text)
        {
            try
            {

                return DAL_FAQ_Item.Get_FAQ_Items(FAQ_ID, Search_Text);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_FAQ_Items(int FAQ_ID, DataTable tbl_FAQ, int? User_ID)
        {
            try
            {
                return DAL_FAQ_Item.Update_FAQ_Items(FAQ_ID, tbl_FAQ, User_ID);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_TrainingItem_IsHelpful(int Video_ID, int IsHelpful, int UserID)
        {
            try
            {

                return DAL_FAQ_Item.Update_TrainingItem_IsHelpful(Video_ID, IsHelpful, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_TopicModule(int Topic_ID)
        {
            try
            {

                return DAL_FAQ_Item.Get_TopicModule(Topic_ID);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_ModuleTopic(string Desc, int? ID, string Mode, int? ModuleID, int UserID)
        {
            try
            {

                return DAL_FAQ_Item.Update_ModuleTopic(Desc, ID, Mode, ModuleID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_ModuleTopic_Details(string Search, int? pagenumber, int? pagesize, string Mode, int? ID, int? ModuleID, ref int isfetchcount)
        {
            try
            {

                return DAL_FAQ_Item.Get_ModuleTopic_Details(Search, pagenumber, pagesize, Mode, ID, ModuleID, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
        public static void Del_ModuleTopic(string Mode, int ID, int UserID)
        {
            try
            {

                DAL_FAQ_Item.Del_ModuleTopic(Mode, ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static void Upd_Faq_ServiceDetails(int FAQ_ID, string Question, string Answer, int UserID, int TopicID, string Topic_Description, int ModuleID, string Module_Description)
        {
            try
            {

                DAL_FAQ_Item.Upd_Faq_ServiceDetails(FAQ_ID, Question, Answer, UserID, TopicID, Topic_Description, ModuleID, Module_Description);
            }
            catch
            {
                throw;
            }
        }
        public static void Del_Faq_ServiceDetails(int FAQ_ID, int UserID)
        {
            try
            {

                DAL_FAQ_Item.Del_Faq_ServiceDetails(FAQ_ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_HelpPage_List(String SearvhFaq, ref int is_Fetch_Count, int? Module, int? Topic, string Keywords, int CompanyID)
        {
            try
            {
                return DAL_FAQ_Item.Get_HelpPage_List(SearvhFaq, ref is_Fetch_Count, Module, Topic, Keywords, CompanyID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_HelpPage_List(String SearvhFaq, ref int is_Fetch_Count, int? Module, int? Topic, string Keywords, int CompanyID,string Searchurl)
        {
            try
            {
                return DAL_FAQ_Item.Get_HelpPage_List(SearvhFaq, ref is_Fetch_Count, Module, Topic, Keywords, CompanyID, Searchurl);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet Get_Menu_Link()
        {
            try
            {
                return DAL_FAQ_Item.Get_Menu_Link();
            }
            catch
            {
                throw;
            }
        }

        
         
    }
}
