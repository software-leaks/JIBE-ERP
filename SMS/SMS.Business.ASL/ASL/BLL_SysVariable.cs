using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.ASL;
using System.Data;

namespace SMS.Business.ASL
{
   public class BLL_SysVariable
    {
       DAL_SysVariable objDAL = new DAL_SysVariable();
       public BLL_SysVariable()
        {
            
        }

        public DataTable Get_SysVariableList(int VariableID)
        {
            return objDAL.Get_SysVariableList(VariableID);
        }


        public DataTable Get_SysVariable_Search(string searchtext, string VariableType
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.Get_SysVariable_Search(searchtext, VariableType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_SysVariable(string searchtext)
        {
            return objDAL.Get_SysVariable(searchtext);
        }

        public int Insert_SysVariable(string type, string Name, string Code,string Value,string ColorCode, int UserID)
        {

            return objDAL.Insert_SysVariable(type, Name, Code,Value,ColorCode,  UserID);
        }

        public int Edit_SysVariable(int? VariableID, string type, string Name, string Code, string Value, string ColorCode, int UserID)
        {

            return objDAL.Edit_SysVariable(VariableID, type, Name, Code, Value, ColorCode, UserID);
        }

        public int Delete_SysVariable(int VariableID, int UserID)
        {
            return objDAL.Delete_SysVariable(VariableID, UserID);
        
        }
    }
}
