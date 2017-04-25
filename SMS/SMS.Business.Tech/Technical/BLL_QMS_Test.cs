using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Technical;
namespace SMS.Business.Technical
{
    public class BLL_QMS_Test
    {
        
        public static DataTable QMS_Test_Search()
        {
            return DAL_QMS_Test.QMS_Test_Search();
        }
        public static DataTable QMS_Test_Edit(int id)
        {
            return DAL_QMS_Test.QMS_Test_Edit(id);
        }

        public static int QMS_Test_Update(int id,string details)
        {
            return DAL_QMS_Test.QMS_Test_Update(id, details);
        }
        public static int QMS_Test_Insert(string details)
        {
            return DAL_QMS_Test.QMS_Test_Insert(details);
        }
    }
}
