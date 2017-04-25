using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.INFRA.Infrastructure;
using System.Data;

namespace SMS.Business.INFRA.Infrastructure
{
    public class BLL_Infra_ReadAccessRight
    {
        DAL_Infra_ReadAccessRight objDAL = new DAL_Infra_ReadAccessRight();
        public BLL_Infra_ReadAccessRight()
        {

        }
      
        public DataTable FbmReadAccessRight_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.FbmReadAccessRight_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int InsertFbmReadAccessRight(string RANK_ID, int CreatedBy)
        {
            return objDAL.InsertFbmReadAccessRight_DL(RANK_ID, CreatedBy);
        }
        public int EditFbmReadAccessRight(int ID, string RANK_ID, int CreatedBy)
        {
            return objDAL.EditFbmReadAccessRight_DL(ID, RANK_ID, CreatedBy);

        }
        public int DeleteFbmReadAccessRight(int ID, int CreatedBy)
        {
            return objDAL.DeleteFbmReadAccessRight_DL(ID, CreatedBy);
        }
        public DataTable Get_FbmReadAccessRight_List(int? ID)
        {
            return objDAL.Get_FbmReadAccessRight_List_DL(ID);
        }
    }
}
