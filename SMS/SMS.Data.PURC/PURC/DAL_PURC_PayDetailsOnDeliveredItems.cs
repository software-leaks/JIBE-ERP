using System;
using System.Configuration;
 using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
 using   SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALPayDetailsOnDeliveredItems
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_PayDetailsOnDeliveredItems
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_PayDetailsOnDeliveredItems()
        {

        }

      


        public DataTable SelectPayDetailsOnDeliveredItems(string strFromDate, string strToDate, string CatalogID, string VesselCode)
        {



            try
            {
                DataTable dtPayDetails = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@FromDate",strFromDate),
               new System.Data.SqlClient.SqlParameter("@ToDate",strToDate ),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode) ,  
               new System.Data.SqlClient.SqlParameter("@CatalogID",CatalogID)   
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_REQ_PAY_DETAILS", obj);
                dtPayDetails = ds.Tables[0];
                return dtPayDetails;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable BindDeliveredItemsInHirarchy(string strRequisition, string strDeliverCode, string strCatalogID, string strVesselCode)
        {
            try
            {
                DataTable dtItmHirachy = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@ReqCode",strRequisition),
               new System.Data.SqlClient.SqlParameter("@CatalogID",strCatalogID ),
               new System.Data.SqlClient.SqlParameter("@DeliverCode",strDeliverCode) ,  
               new System.Data.SqlClient.SqlParameter("@VesselCode",strVesselCode)   
               
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Item_Hirarchy_Req_Pay", obj);
                dtItmHirachy = ds.Tables[0];
                return dtItmHirachy;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CancelPO(string QUTCODE, string ODRCODE, string SUPCODE, string DOCCODE, string REQCODE, string VSLCODE)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@QUTCODE",QUTCODE),
               new System.Data.SqlClient.SqlParameter("@ODRCODE",ODRCODE),
               new System.Data.SqlClient.SqlParameter("@SUPCODE",SUPCODE) ,
                new System.Data.SqlClient.SqlParameter("@DOCCODE",DOCCODE)  ,
                 new System.Data.SqlClient.SqlParameter("@REQCODE",REQCODE)  ,
                  new System.Data.SqlClient.SqlParameter("@VSLCODE",VSLCODE) 
            
               
             };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_PMS_CancelPO", obj);
        }


    }
}