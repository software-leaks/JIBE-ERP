using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;


/// <summary>
/// Summary description for Department_PMS
/// </summary>
/// 

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_Items objItems = new DAL_PURC_Items();




        public int SaveItem(ItemsData objDOItem)
        {

            return objItems.SaveItems(objDOItem);

        }

        public int DeleteItem(ItemsData objDOItem)
        {

            return objItems.DeleteItems(objDOItem);

        }

        public int EditItem(ItemsData objDOItem)
        {

            return objItems.EditItems(objDOItem);


        }



        public DataTable SelectItems(string CatalogID, string SubCatalogID)
        {

            return objItems.SelectItems(CatalogID, SubCatalogID);


        }




        public DataTable SelectUnitnPackage()
        {

            return objItems.SelectUnitnPackage();



        }
        public DataSet SelectUnitnPackageDataSet()
        {

            return objItems.SelectUnitnPackageDataSet();



        }

    }
}
