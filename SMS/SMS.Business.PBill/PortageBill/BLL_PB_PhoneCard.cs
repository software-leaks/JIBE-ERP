using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.PortageBill;

namespace SMS.Business.PortageBill
{
    public class BLL_PB_PhoneCard
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable PhoneCord_RequestItem_Search(int RequestId, int VesselId, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_PB_PhoneCard.PhoneCord_RequestItem_Search(RequestId,VesselId, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public static void PhoneCord_Request_UpdateStatus(int RequestId,int VesselID, int Created_By)
        {
            DAL_PB_PhoneCard.PhoneCord_Request_UpdateStatus(RequestId, VesselID, Created_By);
           
        }
        public static DataTable PhoneCord_Request_Edit(int RequestId,int vesselid)
        {
            return DAL_PB_PhoneCard.PhoneCord_Request_Edit(RequestId, vesselid);
        }
        public static DataTable PhoneCord_RequestItem_Edit(int CrewId)
        {
            return DAL_PB_PhoneCard.PhoneCord_RequestItem_Edit(CrewId);
        }

        public static DataTable PhoneCord_Request_Search(string Request_Number, DataTable VesselList, string FromDate, string ToDate, string status
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);

            return DAL_PB_PhoneCard.PhoneCord_Request_Search(Request_Number, VesselList, fromdt, todt, status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public static DataTable PhoneCord_Kitty_Search(string CardNumber, string CardUnit, string CardTitle, string CardSubTitle,
             int? VoyageId, int? SupplierId, DataTable VesselList, string FromDate, string ToDate,
           string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);

            return DAL_PB_PhoneCard.PhoneCord_Kitty_Search(CardNumber, CardUnit, CardTitle, CardSubTitle, VoyageId, SupplierId, VesselList, fromdt, todt, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public static int PhoneCard_Kitty_Insert(int cardNumber, string PinCode, int CardUnit, string Title, string subtitle, string fileName, int Created_By, int? VesselId, int? supplierId)
        {
            return DAL_PB_PhoneCard.PhoneCard_Kitty_Insert(cardNumber, PinCode, CardUnit, Title, subtitle, fileName, Created_By, VesselId, supplierId);
        }

        public static int PhoneCard_RequestItem_Update(int CrewID, int kitty_id, int VesselId, int VoyageId, int Created_By)
        {
            return DAL_PB_PhoneCard.PhoneCard_RequestItem_Update(CrewID, kitty_id, VesselId, VoyageId, Created_By);
        }

        public static DataTable PhoneCard_Kitty_GetCard()
        {
            return DAL_PB_PhoneCard.PhoneCard_Kitty_GetCard();
        }
        public static DataTable PhoneCard_VoyagesList()
        {
            return DAL_PB_PhoneCard.PhoneCard_VoyagesList();
        }

        public static DataTable PhoneCard_Consumption_GetCardByVessl(int VesselID)
        {
            return DAL_PB_PhoneCard.PhoneCard_Consumption_GetCardByVessl(VesselID);
        }

        public static DataTable PhoneCard_Consumption_GetCardByCrew(int VodgesId)
        {
            return DAL_PB_PhoneCard.PhoneCard_Consumption_GetCardByCrew(VodgesId);
        }

        public static DataTable PhoneCard_Consumption_GetCardByMonth(string cMonth, string cYear)
        {
            return DAL_PB_PhoneCard.PhoneCard_Consumption_GetCardByMonth(cMonth, cYear);
        }

        public static DataTable PhoneCord_Request_Export(string Request_Number, DataTable VesselList, string FromDate, string ToDate, string status)
        {

            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);

            return DAL_PB_PhoneCard.PhoneCord_Request_Export(Request_Number, VesselList, fromdt, todt, status);

        }
        public static DataTable PhoneCord_Kitty_Export(string CardNumber, string CardUnit, string CardTitle, string CardSubTitle,
         int? VoyageId, int? SupplierId, DataTable VesselList, string FromDate, string ToDate,
       string sortby, int? sortdirection, int? pagenumber, int? pagesize)
        {

            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);

            return DAL_PB_PhoneCard.PhoneCord_Kitty_Export(CardNumber, CardUnit, CardTitle, CardSubTitle, VoyageId, SupplierId, VesselList, fromdt, todt, sortby, sortdirection, pagenumber, pagesize);

        }
    }
}
