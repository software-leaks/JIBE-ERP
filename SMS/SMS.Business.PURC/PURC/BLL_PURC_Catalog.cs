using System;
using System.Data;
using System.Configuration;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;


/// <summary>
/// Summary description for Catalog_PMS
/// </summary>
/// 

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {



        DAL_PURC_Catalog objCatalog = new DAL_PURC_Catalog();

        public int SaveCatalog(CatalogData objDOCatalog)
        {

            return objCatalog.SaveCatalog(objDOCatalog);

        }

        public int DeleteCatalog(CatalogData objDOCatalog)
        {

            return objCatalog.DeleteCatalog(objDOCatalog);

        }


        public int EditCatalog(CatalogData objDOCatalog)
        {

            return objCatalog.EditCatalog(objDOCatalog);

        }

        public DataTable SelectFunctionType()
        {

            return objCatalog.SelectFunctionType();

        }

        public DataTable SelectCatalog()
        {

            return objCatalog.SelectCatalog();


        }



        public DataTable SelectCatalogMaster()
        {

            return objCatalog.SelectCatalogMaster();

        }

        public DataTable SelectCatalogByDept(string Dept)
        {

            return objCatalog.SelectCatalogByDept(Dept);

        }

        public static DataTable Get_Catalogues(string Dept_Type,string Dept_Code)
        {

            return DAL_PURC_Catalog.Get_Catalogues(Dept_Type, Dept_Code);

        }




    }
}