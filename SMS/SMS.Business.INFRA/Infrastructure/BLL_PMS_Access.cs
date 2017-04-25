using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.INFRA.Infrastructure;
using System.Data;

namespace SMS.Business.INFRA.Infrastructure
{
    public class BLL_PMS_Access
    {
        DAL_PMS_Access objDAL = new DAL_PMS_Access();
        /// <summary>
        /// Search Saved access rights
        /// </summary>
        /// <param name="Search">search by entered value</param>
        /// <param name="ActionType">Action type : VERIFY/DELETE</param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber">selected page</param>
        /// <param name="pagesize"> selected page size.</param>
        /// <param name="isfetchcount">no of records found.</param>
        /// <returns>list of details.</returns>
        public DataTable SearchPMSAccess(string Search, string ActionType, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchPMSAccess(Search, ActionType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        /// <summary>
        /// get details of rank against access id
        /// </summary>
        /// <param name="AccessID">Primary key</param>
        /// <returns>list of details.</returns>
        public DataSet Get_PMSAccessList(int AccessID)
        {
            return objDAL.Get_PMSAccessList(AccessID);

        }

        /// <summary>
        /// Add , update and delete access rights
        /// </summary>
        /// <param name="RankID">id of selected rank </param>
        /// <param name="PMS_Access_ID">primarky key incase of update and delete.|| for new record is null</param>
        /// <param name="ActionType">Action type : VERIFY/DELETE</param>
        /// <param name="userID">Logged in user id</param>
        /// <param name="mode">Mode: A:Add new record.U: update the record. D:delete</param>
        /// <returns>no .of rows affected.</returns>
        public int Insert_Update_Delete_PMS_Access(int? RankID, int? PMS_Access_ID, string ActionType, int userID, char mode)
        {
            try
            {
                return objDAL.Insert_Update_Delete_PMS_Access(RankID, PMS_Access_ID, ActionType, userID, mode);
            }
            catch
            {
                throw;
            }
        }
    }


}
