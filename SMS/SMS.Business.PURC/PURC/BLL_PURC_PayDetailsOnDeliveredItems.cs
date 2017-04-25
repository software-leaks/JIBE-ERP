using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;
/// <summary>
/// Summary description for PayDetailsOnDeliveredItems
/// </summary>
namespace SMS.Business.PURC
{
    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_PayDetailsOnDeliveredItems objPayDetails = new DAL_PURC_PayDetailsOnDeliveredItems();




        public DataTable SelectPayDetailsOnDeliveredItems(string strFromDate, string strToDate, string CatalogID, string VesselCode)
        {
            return objPayDetails.SelectPayDetailsOnDeliveredItems(strFromDate, strToDate, CatalogID, VesselCode);



        }

        public DataTable BindDeliveredItemsInHirarchy(string strRequisition, string strDeliverCode, string strCatalogID, string strVesselCode)
        {

            return objPayDetails.BindDeliveredItemsInHirarchy(strRequisition, strDeliverCode, strCatalogID, strVesselCode);



        }
        public void CancelPO(string QUTCODE, string ODRCODE, string SUPCODE, string DOCCODE, string REQCODE, string VSLCODE)
        {

            objPayDetails.CancelPO(QUTCODE, ODRCODE, SUPCODE, DOCCODE, REQCODE, VSLCODE);




        }



    }
}
