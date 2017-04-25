using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SMS.Data.OCAAdmin;

namespace SMS.Business.OCAAdmin
{
    public class BLL_QueryBuilder
    {
        DAL_QueryBuilder objDAL = new DAL_QueryBuilder();
        public DataTable  Get_SavedQuery(string DBServer, string DatabaseName, string DBUserName,  string Command_Type, string QueryName, int UserID)
        {
            return objDAL.Get_SavedQuery(DBServer, DatabaseName, DBUserName, Command_Type, QueryName, UserID);
        }

        public DataTable Get_SavedQuery(string QueryName, string Command_Type, int UserID)
        {
            return objDAL.Get_SavedQuery(QueryName, Command_Type, UserID);
        }


        public static int SaveQuery(string Query_Name, string Command_Type, string Display_Name, string Command_SQL, string Key_Field, string ResultType, string DBServer, string DatabaseName, string DBUserName, string DBPassword, int UserID)
        {
            return DAL_QueryBuilder.SaveQuery(Query_Name, Command_Type, Display_Name, Command_SQL, Key_Field, ResultType, DBServer, DatabaseName, DBUserName, DBPassword, UserID);
        }

        public static DataTable Get_QueryDeatil(string QueryName)
        {

            return DAL_QueryBuilder.Get_QueryDeatil(QueryName);
        }
        public DataTable Get_AllTables()
        {
            return objDAL.Get_AllTables();
        }

        public DataTable GET_Table_Columns(string TableName)
        {

            return objDAL.GET_Table_Columns(TableName);
        }

    }
}
