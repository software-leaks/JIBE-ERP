using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SMS.Data.Infrastructure;

namespace SMS.Business.Infrastructure
{
    public class BLL_Worklist_Access
    {
        DAL_Infra_Worklist_Access objDAL = new DAL_Infra_Worklist_Access();

        public BLL_Worklist_Access()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable SearchWorklistAccess(string Search, string ActionType
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchWorklistAccess(Search, ActionType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataSet Get_WorkListAccessList(int AccessID)
        {
            return objDAL.Get_WorkListAccessList(AccessID);

        }


        public int EditWorklistAccess(int? UserID, string ActionType, int AccessID, int Created_By, DataTable JobTypes)
        {
            try
            {
                return objDAL.EditWorklistAccess(UserID, ActionType, AccessID, Created_By, JobTypes);
            }
            catch
            {
                throw;
            }
        }

        public int InsertWorklistAccess(int? UserID, string ActionType, int Created_By, DataTable JobTypes)
        {
            try
            {
                return objDAL.InsertWorklistAccess(UserID, ActionType, Created_By, JobTypes);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteWorklistAccess(int AccessID, int Created_By)
        {
            try
            {
                return objDAL.DeleteWorklistAccess(AccessID, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public DataTable SearchWorklistAccess(int? UserID, string ActionType
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchWorklistAccess(UserID, ActionType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataTable Get_WorkListAccessList(int AccessID,int Temp=0)
        {
            return objDAL.Get_WorkListAccessList(AccessID, Temp);

        }


        public int EditWorklistAccess(int? UserID, string ActionType, int AccessID, int Created_By)
        {
            try
            {
                return objDAL.EditWorklistAccess(UserID, ActionType, AccessID, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int InsertWorklistAccess(int? UserID, string ActionType, int Created_By)
        {
            try
            {
                return objDAL.InsertWorklistAccess(UserID, ActionType, Created_By);
            }
            catch
            {
                throw;
            }
        }
    }
}
