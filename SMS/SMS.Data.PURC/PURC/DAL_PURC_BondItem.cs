using System;
using System.Configuration;
 
 
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
 
using   SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALBondItem
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_BondItem
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_BondItem()
        {

        }

       public DataTable getItemList(string SystemCode)
        {
            try
            {
                System.Data.DataTable dtVessels = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select Items.Id,Items.Short_Description,Items.System_Code, IPrice.ITEM_PRICE_PER_PACKAGE from dbo.PURC_LIB_ITEMS Items inner join dbo.PURC_DTL_ITEM_PRICE IPrice on Items.id = IPrice.ITEM_REF_CODE where Items.System_Code='" + SystemCode + "'";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtVessels = ds.Tables[0];
                return dtVessels;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable getStaffList()
        {
            try
            {
                System.Data.DataTable dtVessels = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select cv.id,cv.Staff_Code,cv.Joining_Date, cd.Staff_Name +' ' + cd.Staff_Midname + ' ' +cd.Staff_Surname as Staff_Name from dbo.CRW_Dtl_Crew_Voyages cv inner join dbo.CRW_Lib_Crew_Details cd on cv.Staff_Code=cd.Staff_Code where  Sign_Off_Date is null";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtVessels = ds.Tables[0];
                return dtVessels;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int getROb(string ItemRefCode)
        {
            int returnData = 10;
            string qry;
            try
            {

                qry = "select isnull(Inventory_Qty,0) ROb from dbo.PURC_DTL_VESSELS_INVENTORY where id=(select isnull(Max(id),0) from dbo.PURC_DTL_VESSELS_INVENTORY where Item_Ref_Code='" + ItemRefCode + "')";
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnData = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }

                //returnData = ds.Tables[0];
            }
            catch (Exception ex)
            {

                returnData = -2;
            }
            return returnData;
        }

        public int ExequetString(string sqlQuery)
        {
            int returnData = 0;
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
                  new System.Data.SqlClient.SqlParameter("@StringQuery",sqlQuery),
             };
                returnData = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);

            }
            catch (Exception ex)
            {

                returnData = -2;
            }
            return returnData; ;
        }

        public DataTable GetBondConsumption(string strFromdate, string strTodate, string VesselCode)
        {

            try
            {
                DataTable dtPayDetails = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@FromDate",strFromdate),
               new System.Data.SqlClient.SqlParameter("@ToDate",strTodate ),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode) ,  
                 
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_BondConsumption", obj);
                dtPayDetails = ds.Tables[0];
                return dtPayDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}