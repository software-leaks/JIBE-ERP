using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;
/// <summary>
/// Summary description for DALDeliveredItems
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_DeliveredItems
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_DeliveredItems()
        {

        }
        

        public DataTable GetOrderedItems(string Requision, string VesselCode, string OrderNumber, string CatalogID)
        {

            try
            {
                System.Data.DataTable dtVessels = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@RequiCode",Requision), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode) , 
               new System.Data.SqlClient.SqlParameter("@OrderNumber",OrderNumber), 
               new System.Data.SqlClient.SqlParameter("@CatalogID",CatalogID),
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_OrderItem_Bind", obj);
                dtVessels = ds.Tables[0];
                return dtVessels;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetRequitionForOrderItem(string VesselCode, string CatalogID)
        {
            try
            {
                System.Data.DataTable dtVessels = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@CatalogID",CatalogID)  
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GetRequ_ForOrderedItems", obj);
                dtVessels = ds.Tables[0];
                return dtVessels;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataTable GetVatSurcharge(string Requision, string VesselCode, string OrderNumber)
        {
            try
            {
                System.Data.DataTable dtVatSur = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",Requision), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@OrderCode",OrderNumber)  
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Vat_Surcharge", obj);
                dtVatSur = ds.Tables[0];
                return dtVatSur;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }



}