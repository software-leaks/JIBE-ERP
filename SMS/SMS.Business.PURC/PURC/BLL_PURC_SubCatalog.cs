using System;
using System.Data;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;


namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_SubCatalog objSubCatalog = new DAL_PURC_SubCatalog();


        public DataTable SelectSubCatalogs()
        {

            return objSubCatalog.SelectSubCatalogs();

        }

        public DataTable GetSubCatalogsByCatalogs(string Catlg)
        {

            return objSubCatalog.GetSubCatalogsByCatalogs(Catlg);

        }

        public DataTable GetSubCatalogid()
        {

            return objSubCatalog.GetSubCatalogid();

        }

        public int EditSubCatalogs(SubCatalogData objsubCatlogDO)
        {

            return objSubCatalog.UpdateSubCatalogs(objsubCatlogDO);



        }

        public int DeleteSubCatelogs(SubCatalogData objsubCatlogDO)
        {
            return objSubCatalog.DeleteSubCatelogs(objsubCatlogDO);



        }

        public int SaveSubCatalogs(SubCatalogData objsubCatlogDO)
        {

            return objSubCatalog.SaveSubCatalogs(objsubCatlogDO);

        }

        public DataSet GetSupplierScope(string SupplCode)
        {

            return objSubCatalog.GetSupplierScope(SupplCode);


        }

        public void InsertSupplierScope(string RegScope, string UnRegScope, string CommentString)
        {

            objSubCatalog.InsertSupplierScope(RegScope, UnRegScope, CommentString);


        }

        public DataTable GetCategory_FileType()
        {

            return objSubCatalog.GetCategory_FileType();


        }


    }
}
