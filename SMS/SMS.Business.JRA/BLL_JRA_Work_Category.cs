using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.JRA;
using SMS.Properties;
using System.Data;
namespace SMS.Business.JRA
{
    public class BLL_JRA_Work_Category
    {
        public static int JRA_INS_WorkCategory(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_INS_WorkCategory(objJRALibData);
        }
        public static DataTable JRA_GET_WORK_CATEGORY_LIST(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(objJRALibData);
        }
        public static int JRA_INS_TYPE(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_INS_TYPE(objJRALibData);
        }
        public static DataTable JRA_GET_TYPE_LIST(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_GET_TYPE_LIST(objJRALibData);
        }
        public static DataTable JRA_GET_WORK_CATEGORY(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_GET_WORK_CATEGORY(objJRALibData);
        }

        public static DataTable JRA_SEARCH_WORK_CATEGORY(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_SEARCH_WORK_CATEGORY(objJRALibData);
        }
        public static DataTable JRA_GET_TYPE_SEARCH(JRA_Lib JRALib_Data)
        {
            return DAL_JRA_Work_Category.JRA_GET_TYPE_SEARCH(JRALib_Data);
        }
        public static int JRA_INS_RatingType(JRA_Lib objJRALibData)
        {
            return DAL_JRA_Work_Category.JRA_INS_RatingType(objJRALibData);
        }
        public static DataTable JRA_GET_RISK_TYPES(JRA_Lib JRALib_Data)
        {
            return DAL_JRA_Work_Category.JRA_GET_RISK_TYPES(JRALib_Data);
        }

        public static DataTable JRA_GET_RATINGS_SEARCH(JRA_Lib JRALib_Data)
        {
            return DAL_JRA_Work_Category.JRA_GET_RATINGS_SEARCH(JRALib_Data);
        }
    }
}
