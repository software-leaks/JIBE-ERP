using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;


/// <summary>
/// Summary description for DeliveredItems
/// </summary>
namespace SMS.Business.PURC
{
    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_DeliveredItems objDeliveredItems = new DAL_PURC_DeliveredItems();

        public DataTable GetOrderedItems(string Requision, string VesselCode, string OrderNumber, string CatalogID)
        {

            return objDeliveredItems.GetOrderedItems(Requision, VesselCode, OrderNumber, CatalogID);


        }

        public DataTable GetRequitionForOrderItem(string VesselCode, string CatalogID)
        {

            return objDeliveredItems.GetRequitionForOrderItem(VesselCode, CatalogID);


        }

        public DataTable GetVatSurcharge(string Requision, string VesselCode, string OrderNumber)
        {

            return objDeliveredItems.GetVatSurcharge(Requision, VesselCode, OrderNumber);

        }

    }
}
