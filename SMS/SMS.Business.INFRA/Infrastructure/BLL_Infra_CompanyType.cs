using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_CompanyType
    {

        DAL_Infra_CompanyType objDAL = new DAL_Infra_CompanyType();

        public DataTable SearchCompanyType(string searchtext
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchCompanyType(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_CompanyTypeListByID(int? CompanyTypeID)
        {
            return objDAL.Get_CompanyTypeListByID_DL(CompanyTypeID);
        }

        public int EditCompanyType(int CompanyTypeID, string CompanyType, string CompanyTypeDesc, int ModifiedBy)
        {
            return objDAL.EditCompanyType_DL(CompanyTypeID, CompanyType, CompanyTypeDesc, ModifiedBy);
        }

        public int InsertCompanyType(string CompanyType, string CompanyTypeDesc, int CreatedBy)
        {
            return objDAL.InsertCompanyType_DL(CompanyType, CompanyTypeDesc, CreatedBy);
        }

        public int DeleteCompanyType(int CompanyTypeID, int DeletedBy)
        {
            return objDAL.DeleteCompanyType_DL(CompanyTypeID, DeletedBy);
        }

    }
}
