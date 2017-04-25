using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALVessels
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_OpeningBalance
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_OpeningBalance()
        {
        }

        

        public DataTable SelectOBItemsForEachRequisiton(string StrQuery)
        {

            try
            {
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = StrQuery;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectOBRequistionNumbers()
        {

            try
            {
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select Distinct Req.ID,Req.VESSEL_CODE, Req.REQUISITION_CODE,Req.DEPARTMENT,SItem.ITEM_SYSTEM_CODE,  SItem.ITEM_SUBSYSTEM_CODE from dbo.PURC_DTL_REQSN Req   inner join PURC_DTL_SUPPLY_ITEMS SItem on sitem.REQUISITION_CODE=Req.REQUISITION_CODE where Req.LINE_TYPE='R'";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable SelectOBIndexGetQuation()
        {

            try
            {
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select distinct Library.subsystem_code,Library.Subsystem_Description,A.*,isnull(B.Total_Qty,0) Total_Qty,isnull(B.total_Amount,0) total_Amount from (SELECT PURC_LIB_SYSTEMS.[SYSTEM_CODE],PURC_LIB_SYSTEMS.[SYSTEM_DESCRIPTION],PURC_LIB_DEPARTMENTS.code,PURC_LIB_DEPARTMENTS.Name_Dept from PURC_LIB_DEPARTMENTS inner join PURC_LIB_SYSTEMS on PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept1 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept2 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept3 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept4 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept5 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept6 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept7 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept8 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept9 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept10 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept11 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept12 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept13 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept14 or PURC_LIB_DEPARTMENTS.code= PURC_LIB_SYSTEMS.dept15 where PURC_LIB_DEPARTMENTS.Active_Status=1 and PURC_LIB_DEPARTMENTS.Form_type is not null and PURC_LIB_SYSTEMS.Active_Status=1 ) A inner join dbo.PURC_LIB_SUBSYSTEMS Library on A.System_code=Library.System_code LEFT outer Join (select Req.DEPARTMENT, Item.ITEM_SYSTEM_CODE,Item.ITEM_SUBSYSTEM_CODE,sum(Item.DELIVERD_QTY) Total_Qty,sum(Item.DELIVERD_QTY*isnull(Item.ORDER_RATE,0)) total_Amount from dbo.PURC_DTL_SUPPLY_ITEMS Item inner join dbo.PURC_DTL_REQSN Req on Req.REQUISITION_CODE=item.REQUISITION_CODE where  Req.Line_type='D' group by Req.DEPARTMENT, Item.ITEM_SYSTEM_CODE,Item.ITEM_SUBSYSTEM_CODE) B on B.Item_System_code=Library.System_code and B.Item_SubSystem_code=Library.SUBSystem_code where A.code<>'S'";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectProvisionItemConsumption(string VesselCode, string ClosingDate)
        {

            try
            {
                DataTable dtClosingBal = new DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode),
                   new System.Data.SqlClient.SqlParameter("@Closing_Date", ClosingDate)
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Provision_ClosingBal", obj);
                dtClosingBal = ds.Tables[0];
                return dtClosingBal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet GetClosingDate(string VesselCode)
        {
            try
            {
                DataSet dtClosingDate = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode),
                   
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Provision_ClosingDate", obj);
                //dtClosingDate = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public int UnLockClosingBal(string vesselCode, string closingDate)
        {
            try
            {
                DataTable dtClosingDate = new DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselCode", vesselCode),
                   new System.Data.SqlClient.SqlParameter("@Closing_Date", closingDate)
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_UnLock_ClosingBal", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataTable GetClosingAmountSum(string vesselCode, string closingDate)
        {
            try
            {
                DataTable dtClosingBalsum = new DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselCode", vesselCode),
                   new System.Data.SqlClient.SqlParameter("@Closing_Date", closingDate)
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Provision_ClosingAmount_Sum", obj);
                dtClosingBalsum = ds.Tables[0];
                return dtClosingBalsum;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}