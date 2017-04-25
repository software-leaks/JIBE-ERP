using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.PortageBill
{
    public class DAL_PB_PhoneCard
    {
        private static string connection = "";
        public DAL_PB_PhoneCard(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_PB_PhoneCard()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public static DataTable PhoneCord_RequestItem_Search(int requestId,int vessel_id,
            string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REQUEST_ID", requestId),
                new SqlParameter("@VESSELID", vessel_id),
                new SqlParameter("@SORTBY",sortby), 
                new SqlParameter("@SORTDIRECTION",sortdirection), 
                new SqlParameter("@PAGENUMBER",pagenumber),
                new SqlParameter("@PAGESIZE",pagesize),
                new SqlParameter("@ISFETCHCOUNT",isfetchcount)
                
            };
            parm[parm.Length - 1].Direction = ParameterDirection.InputOutput;
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_ITEM_SEARCH", parm);
            isfetchcount = Convert.ToInt32(parm[parm.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataTable PhoneCord_Request_Edit(int requestid, int VesselID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@ID", requestid) , 
                new SqlParameter("@VESSELID", VesselID), 
            };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_EDIT", parm);
            return ds.Tables[0];

        }

        public static void PhoneCord_Request_UpdateStatus(int requestid, int VesselID, int Created_By)
        {
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REQUEST_ID", requestid),   
                new SqlParameter("@VESSELID", VesselID),  
                new SqlParameter("@USERID",Created_By)
            };
            int n = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_DAEMON_UPDATE_PHONE_CARD_ITEM", parm);
        }

        public static DataTable PhoneCord_RequestItem_Edit(int crewid)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@ID", crewid)                
            };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_TITEM_EDIT", parm);
            return ds.Tables[0];
        }

        public static DataTable PhoneCord_Kitty_Search(string CardNumber, string CardUnit, string CardTitle, string CardSubTitle,
            int? VoyageId, int? SupplierId, DataTable VesselList, DateTime? Fromdate, DateTime? Todate,
          string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@CARDNUMBER", CardNumber),
                new SqlParameter("@UNIT", CardUnit),
                new SqlParameter("@TITLE", CardTitle), 
                new SqlParameter("@SUBTITLE", CardSubTitle), 
                new SqlParameter("@VOYAGEID", VoyageId), 
                new SqlParameter("@SUPPLIER_ID", SupplierId),
                new SqlParameter("@VESSELLIST", VesselList), 
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@SORTBY",sortby), 
                new SqlParameter("@SORTDIRECTION",sortdirection), 
                new SqlParameter("@PAGENUMBER",pagenumber),
                new SqlParameter("@PAGESIZE",pagesize),
                new SqlParameter("@ISFETCHCOUNT",isfetchcount)
                
            };
            parm[parm.Length - 1].Direction = ParameterDirection.InputOutput;
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_KITTY_SEARCH", parm);
            isfetchcount = Convert.ToInt32(parm[parm.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataTable PhoneCord_Request_Search(string tc_ref_number, DataTable VesselList, DateTime? Fromdate, DateTime? Todate, string status,
          string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REQUEST_NUMBER", tc_ref_number),
                new SqlParameter("@VESSELLIST", VesselList), 
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@STATUS",status),
                new SqlParameter("@SORTBY",sortby), 
                new SqlParameter("@SORTDIRECTION",sortdirection), 
                new SqlParameter("@PAGENUMBER",pagenumber),
                new SqlParameter("@PAGESIZE",pagesize),
                new SqlParameter("@ISFETCHCOUNT",isfetchcount)
                
            };
            parm[parm.Length - 1].Direction = ParameterDirection.InputOutput;
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_SEARCH", parm);
            isfetchcount = Convert.ToInt32(parm[parm.Length - 1].Value);
            return ds.Tables[0];

        }
        public static int PhoneCard_Kitty_Insert(int cardNumber, string PinCode, int CardUnit, string Title, string subtitle, string fileName, int Created_By,int? VesselId, int? supplierId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CARDNUMBER",cardNumber) ,
                new SqlParameter("@PINCODE",PinCode),  
                new SqlParameter("@UNIT",CardUnit) ,
                new SqlParameter("@TITLE",Title) ,
                new SqlParameter("@SUBTITLE",subtitle) ,
                new SqlParameter("@FILENAME",fileName) ,
                new SqlParameter("@USERID",Created_By),
                new SqlParameter("@VesselID",VesselId) ,
                new SqlParameter("@SupplierID",supplierId)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_INS_PHONE_CARD_KITTY", sqlprm);

        }
        public static int PhoneCard_RequestItem_Update(int CrewID, int kitty_id, int VesselId, int VoyageId, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ID",CrewID) ,
                new SqlParameter("@Kitty_ID",kitty_id),  
                new SqlParameter("@VESSELID",VesselId) ,
                new SqlParameter("@VOYAGEID",VoyageId) ,               
                new SqlParameter("@USERID",Created_By)};
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPDATE_PHONE_CARD_ITEM", sqlprm);
        }

        public static DataTable PhoneCard_Kitty_GetCard()
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] { };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_KITTY_LIST", parm);

            return ds.Tables[0];

        }
        public static DataTable PhoneCard_VoyagesList()
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] { };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_SP_GET_CREWVOYAGE_LIST", parm);

            return ds.Tables[0];

        }

        public static DataTable PhoneCard_Consumption_GetCardByVessl(int VesselID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@VESSELID", VesselID) };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_CONSUMPTION_VESSEL", parm);
            return ds.Tables[0];
        }
        public static DataTable PhoneCard_Consumption_GetCardByCrew(int VodgesId)
        {

            DataSet ds = new DataSet();
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@VOYAGEID", VodgesId) };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_CONSUMPTION", parm);
            return ds.Tables[0];
        }
        public static DataTable PhoneCard_Consumption_GetCardByMonth(string cMonth, string cYear)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@PBILLMONTH", cMonth), new SqlParameter("@PBILYEAR", cYear) };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_CONSUMPTION_MONTH", parm);
            return ds.Tables[0];
        }

        ////Added by vasu 22/03/16 --start--
        public static DataTable PhoneCord_Request_Export(string tc_ref_number, DataTable VesselList, DateTime? Fromdate, DateTime? Todate, string status)
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REQUEST_NUMBER", tc_ref_number),
                new SqlParameter("@VESSELLIST", VesselList), 
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@STATUS",status)                
                
            };
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_Export", parm);
            return ds.Tables[0];

        }

        public static DataTable PhoneCord_Kitty_Export(string CardNumber, string CardUnit, string CardTitle, string CardSubTitle,
          int? VoyageId, int? SupplierId, DataTable VesselList, DateTime? Fromdate, DateTime? Todate,
        string sortby, int? sortdirection, int? pagenumber, int? pagesize)
        {

            DataSet ds = new DataSet();

            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@CARDNUMBER", CardNumber),
                new SqlParameter("@UNIT", CardUnit),
                new SqlParameter("@TITLE", CardTitle), 
                new SqlParameter("@SUBTITLE", CardSubTitle), 
                new SqlParameter("@VOYAGEID", VoyageId), 
                new SqlParameter("@SUPPLIER_ID", SupplierId),
                new SqlParameter("@VESSELLIST", VesselList), 
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@SORTBY",sortby), 
                new SqlParameter("@SORTDIRECTION",sortdirection), 
                new SqlParameter("@PAGENUMBER",pagenumber),
                new SqlParameter("@PAGESIZE",pagesize)                
                
            };
            //parm[parm.Length - 1].Direction = ParameterDirection.InputOutput;
            ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PHONE_CARD_KITTY_Export", parm);
            //isfetchcount = Convert.ToInt32(parm[parm.Length - 1].Value);
            return ds.Tables[0];

        }
        //// Added by vasu 22/03/16 --End--




    }
}
