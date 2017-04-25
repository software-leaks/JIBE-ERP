using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.LMS;
using System.Data;


namespace SMS.Business.LMS
{
    public static class BLL_LMS_Help
    {
   
      

        public static DataSet Get_Enabled_Menu()
        {
            return DAL_LMS_Help.Get_Enabled_Menu();
        }
        public static DataSet Get_Help_Resources(int? Menu_Code)
        {
            return DAL_LMS_Help.Get_Help_Resources(Menu_Code); 
        }
        public static void InsertUpdate_Help_Resource(int Menu_Code, int Parent_ID, string Parent_Type, int UserID)
        {
            DAL_LMS_Help.InsertUpdate_Help_Resource(Menu_Code,Parent_ID,Parent_Type,UserID);
             
        }

        public static void Delete_Help_Resource(int? Help_ID, int? UserID)
        {
            DAL_LMS_Help.Delete_Help_Resource(Help_ID, UserID); 

        }
    }
}
