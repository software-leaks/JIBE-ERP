using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;



namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_UserType
    {

        DAL_Infra_UserType objDAL = new DAL_Infra_UserType();

        public BLL_Infra_UserType()
        {
            
        }

        public DataTable SearchUserType(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

           return objDAL.SearchUserType(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        
        }

        public DataTable Get_UserTypeList(int? UserTypeID)
        {
            return objDAL.Get_UserTypeList_DL(UserTypeID);
        }

        public int EditUserType(int UserTypeID, string UserType, int CreatedBy)
        {
            return objDAL.EditUserType_DL(UserTypeID, UserType, CreatedBy);
        
        }

        public int InsertUserType(string UserType, int CreatedBy)
        {
            return objDAL.InsertUserType_DL(UserType, CreatedBy);
        }

        public int DeleteUserType(int UserTypeID,int CreatedBy)
        {
            return objDAL.DeleteUserType_DL(UserTypeID,CreatedBy);
        
        }

    }

}
