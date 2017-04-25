using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALPendingRequisition
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_PendingRequisition
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_PendingRequisition()
        {
        }



        public DataTable SelectRequisitionForHierarchy()
        {

            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "Select distinct  Req.REQUISITION_CODE,convert(varchar(10),req.DOCUMENT_DATE,101) requestion_Date,  req.REQUISITION_CODE,dep.Name_Dept,req.TOTAL_ITEMS,a.SYSTEM_Description,Req.Line_type,B.EVALUATION_OPTION,A.SYSTEM_CODE  from dbo.PURC_DTL_REQSN Req  LEFT OUTER JOIN dbo.PURC_LIB_DEPARTMENTS dep on req.DEPARTMENT=dep.code   inner join  (SELECT DISTINCT SITEM.REQUISITION_CODE,slib.SYSTEM_Description,Slib.System_Code  FROM dbo.PURC_DTL_SUPPLY_ITEMS SITEM   INNER jOIN dbo.PURC_LIB_SYSTEMS Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE) A on   A.REQUISITION_CODE=Req.REQUISITION_CODE   LEFT Outer Join   (select distinct QUOTATION_CODE,EVALUATION_OPTION from dbo.PURC_DTL_QUOTED_PRICES where EVALUATION_OPTION=1) B on   Req.QUOTATION_CODE=B.QUOTATION_CODE ";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtFleet = ds.Tables[0];
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable SelectItemsForHierarchy(string strRequistionCode, string strVesselCode, string strDocumentCode)
        {

            try
            {



                System.Data.DataTable dtFleet = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",strRequistionCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",strVesselCode), 
               new System.Data.SqlClient.SqlParameter("@Document_code",strDocumentCode), 
             };

                //System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "SELECT DISTINCT itv.[ID],itv.[Part_Number],itv.[Short_Description],itv.[Unit_and_Packings], SITEM.[REQUESTED_QTY],SITEM.[ITEM_COMMENT], SITEM.REQUISITION_CODE,slib.SYSTEM_Description FROM dbo.PURC_DTL_SUPPLY_ITEMS SITEM  INNER jOIN dbo.PURC_LIB_SYSTEMS Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE INNER jOIN  dbo.PURC_LIB_ITEMS itv on itv.[ID]=SITEM.[ITEM_REF_CODE] where SITEM.REQUISITION_CODE = '"+strRequistionCode + "'";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Item_Hirarchy_Pending_req", obj);
                dtFleet = ds.Tables[0];
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }




        public DataTable SelectPendingRequistion(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();

                SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)

               
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingRequistion", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable SelectPendingRequistion(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount,string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();

                SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingRequistion", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectPendingQuatationReceive(string VesselCode)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingQuatationReceive", obj);
                dtFleet = ds.Tables[0];
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectPendingQuatationEvalution(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? LogedIn_User, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                    ,new SqlParameter("@LOGEDIN_USER",LogedIn_User)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingQuatationEvalution", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingQuatationEvalution(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? LogedIn_User, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                    ,new SqlParameter("@LOGEDIN_USER",LogedIn_User)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingQuatationEvalution", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingPurchasedOrderRaise(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingPurchasedOrderRaise", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingPurchasedOrderRaise(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingPurchasedOrderRaise", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingPOConfirm(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingPOConfirm", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingPOConfirm(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingPOConfirm", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingPOConfirm_Export(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
         , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@DEPT_CODE",deptcode)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_PendingPOConfirm_Export", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable SelectPendingDeliveryUpdate(int? Fleet_ID, int? Vessel_Code, string Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {
            try
            {
                DataTable dtFleet = new DataTable();
                SqlParameter[] obj = new SqlParameter[]
             { 
                new SqlParameter("@FLEET_ID",Fleet_ID), 
                new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                new SqlParameter("@DEPT_CODE",Dept_Code), 
                new SqlParameter("@DEPT_TYPE",Dept_Type), 
                new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code), 
                new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                      
                        
                new SqlParameter("@PAGE_INDEX",Page_Index), 
                new SqlParameter("@PAGE_SIZE",Page_Size), 
                new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count)
             };

                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingDeliveryUpdate", obj);
                dtFleet = ds.Tables[0];
                Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPendingDeliveryUpdate(DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {
            try
            {
                DataTable dtFleet = new DataTable();
                SqlParameter[] obj = new SqlParameter[]
             { 
                new SqlParameter("@FLEET_ID",Fleet_ID), 
                new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                new SqlParameter("@DEPT_CODE",Dept_Code), 
                new SqlParameter("@DEPT_TYPE",Dept_Type), 
                new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code), 
                new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                      
                        
                new SqlParameter("@PAGE_INDEX",Page_Index), 
                new SqlParameter("@PAGE_SIZE",Page_Size), 
                new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count)
             };

                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingDeliveryUpdate", obj);
                dtFleet = ds.Tables[0];
                Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectAllRequisitionStages(int? Fleet_ID, int? Vessel_Code, string Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, string Reqsn_Status, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {


            SqlParameter[] obj = new SqlParameter[]
                    { 
                        new SqlParameter("@FLEET_ID",Fleet_ID), 
                        new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                        new SqlParameter("@DEPT_CODE",Dept_Code), 
                        new SqlParameter("@DEPT_TYPE",Dept_Type), 
                        new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code), 
                        new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                        new SqlParameter("@REQSN_STATUS",Reqsn_Status), 
                        
                        new SqlParameter("@PAGE_INDEX",Page_Index), 
                        new SqlParameter("@PAGE_SIZE",Page_Size), 
                        new SqlParameter("@SORT_BY",Sort_By),
                        new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                        new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count), 
                     };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_ALL_Requisition_Stages", obj).Tables[0];
            Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
            return dt;


        }



        public DataTable SelectAllRequisitionStages(DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, string Reqsn_Status, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {


            SqlParameter[] obj = new SqlParameter[]
                    { 
                        new SqlParameter("@FLEET_ID",Fleet_ID), 
                        new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                        new SqlParameter("@DEPT_CODE",Dept_Code), 
                        new SqlParameter("@DEPT_TYPE",Dept_Type), 
                        new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code), 
                        new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                        new SqlParameter("@REQSN_STATUS",Reqsn_Status), 
                        
                        new SqlParameter("@PAGE_INDEX",Page_Index), 
                        new SqlParameter("@PAGE_SIZE",Page_Size), 
                        new SqlParameter("@SORT_BY",Sort_By),
                        new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                        new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count), 
                     };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_ALL_Requisition_Stages", obj).Tables[0];
            Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
            return dt;


        }





        public DataTable GetREQStatus()
        {
            try
            {
                System.Data.DataTable dtREQStatus = new System.Data.DataTable();

                // System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select code,short_code,Description from dbo.PURC_LIB_SYSTEM_PARAMETERS where  Parent_Type=139");
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSN_STATUS");
                dtREQStatus = ds.Tables[0];
                return dtREQStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetREQStatusBYCode(string code)
        {
            try
            {
                System.Data.DataTable dtREQStatus = new System.Data.DataTable();

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select code,short_code,Description from dbo.PURC_LIB_SYSTEM_PARAMETERS where  Parent_Type=139 and code < (select code from PURC_LIB_SYSTEM_PARAMETERS where short_code='" + code + "' )");
                dtREQStatus = ds.Tables[0];
                return dtREQStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable SelectNewRequisitionList(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtNewREQ = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
             };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_NewRequistionList", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable SelectNewRequisitionList(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtNewREQ = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
             };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_NewRequistionList", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsRequisitionOnHoldLogHistory(string sRequisitionCode, string sVesselCode, string sDocumentCode, string sLineType, string sOnHold, string sRemarks, string sSupplier, int iCreated_By)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@RequisitionCode",sRequisitionCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",sVesselCode), 
               new System.Data.SqlClient.SqlParameter("@DocumentCode",sDocumentCode),              
               new System.Data.SqlClient.SqlParameter("@LineType",sLineType), 
               new System.Data.SqlClient.SqlParameter("@OnHold",sOnHold),
               new System.Data.SqlClient.SqlParameter("@Remarks",sRemarks),
               new System.Data.SqlClient.SqlParameter("@SupplierCode",sSupplier),
               new System.Data.SqlClient.SqlParameter("@Created_By",iCreated_By),
               new SqlParameter("return",SqlDbType.Int)
             };
                obj[8].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_RequisitionOnHoldLogHistory", obj);
                return Convert.ToInt32(obj[8].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataTable GetRequisitionOnHoldLogHistory(string  sVesselCode, string sRequisitionCode, string sLineType, string sDocumentCode, string sOnHold, int iCreated_By)
        //{
        //    try
        //    {
        //        System.Data.DataTable dtGetReqHistory = new System.Data.DataTable();
        //        System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
        //         { 
        //           new System.Data.SqlClient.SqlParameter("@VesselCode",sVesselCode), 
        //           new System.Data.SqlClient.SqlParameter("@RequisitionCode",sRequisitionCode), 
        //           new System.Data.SqlClient.SqlParameter("@LineType",sLineType), 
        //           new System.Data.SqlClient.SqlParameter("@DocumentCode",sDocumentCode),
        //           new System.Data.SqlClient.SqlParameter("@OnHold",sOnHold),
        //           new System.Data.SqlClient.SqlParameter("@Created_By",iCreated_By),
        //         };
        //        System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionOnHoldLogHistory", obj);
        //        dtGetReqHistory = ds.Tables[0];
        //        return dtGetReqHistory;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public DataTable GetRequisitionOnHoldLogHistory()
        {
            try
            {
                System.Data.DataTable dtGetReqHistory = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionOnHoldLogHistory");
                dtGetReqHistory = ds.Tables[0];
                return dtGetReqHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRequisitionOnHoldLogHistory(int? Fleet, int? VesselID, string Stag, string ReqsnCode, string UserName, string DocumentCode, int? ddhold, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Fleet", Fleet),
                   new System.Data.SqlClient.SqlParameter("@Vessel", VesselID),
                   new System.Data.SqlClient.SqlParameter("@Stage", Stag),
                   new System.Data.SqlClient.SqlParameter("@ReqsnCode", ReqsnCode),
                   new System.Data.SqlClient.SqlParameter("@UserName", UserName),
                   new System.Data.SqlClient.SqlParameter("@DocumentCode", DocumentCode),
                   new System.Data.SqlClient.SqlParameter("@onHold", ddhold),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionOnHoldLogHistory_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];



        }		
        public DataTable GetRequisitionOnHoldLogHistory_ByReqsn(string Reqsn)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@Reqsn",Reqsn) 
             
             };
                System.Data.DataTable dtGetReqHistory = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionOnHoldLogHistory_ByReqsn", obj);
                dtGetReqHistory = ds.Tables[0];
                return dtGetReqHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int CancelRequisitionStages(int sVesselCode, string sRequisitionCode, string sDocumentCode, string sCancelStage, string sCancelReason, int iCreated_By, string FromStage, string QuotationCode = "")
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",sRequisitionCode), 
               new System.Data.SqlClient.SqlParameter("@DocumentCode",sDocumentCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",sVesselCode),              
               new System.Data.SqlClient.SqlParameter("@CanceledStage",sCancelStage), 
               new System.Data.SqlClient.SqlParameter("@CanceledReason",sCancelReason),
               new System.Data.SqlClient.SqlParameter("@CreatedBy",iCreated_By),
               new SqlParameter("@FromStage",FromStage),
               new SqlParameter("@Quotation_Code",QuotationCode),
               new SqlParameter("@return",SqlDbType.Int)
              };

                obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Cancel_RequisitionWithStage", obj);
                return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable GetRequisitionStageDetails(string sVesselCode, string sRequisitionCode, string sFrmDate, string sToDate)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@RequisitionNo",sRequisitionCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",sVesselCode)    
            
             };
                System.Data.DataTable dtGetReqStage = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionStageDetails", obj);
                dtGetReqStage = ds.Tables[0];
                return dtGetReqStage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequisitionStageDetails(int? Fleet, int? VesselID, string RequisitionNo, string PONo, string Department, string UrgencyofReq,int? timeLaps, int? HoldFlage, DateTime? fromdate, DateTime? Todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Fleet", Fleet),
                   new System.Data.SqlClient.SqlParameter("@VesselCode", VesselID),
                   new System.Data.SqlClient.SqlParameter("@RequisitionNo", RequisitionNo),
                   new System.Data.SqlClient.SqlParameter("@PONo", PONo),
                   new System.Data.SqlClient.SqlParameter("@Department", Department),
                   new System.Data.SqlClient.SqlParameter("@UrgencyofReq", UrgencyofReq),
                   //new System.Data.SqlClient.SqlParameter("@DOno", DOno),
                   new System.Data.SqlClient.SqlParameter("@timeLaps", timeLaps),
                   new System.Data.SqlClient.SqlParameter("@HoldFlage", HoldFlage),
                   //new System.Data.SqlClient.SqlParameter("@QtnRef ", QtnRef ),
                   new System.Data.SqlClient.SqlParameter("@fromdate", fromdate),
                   new System.Data.SqlClient.SqlParameter("@Todate", Todate),
                   //new System.Data.SqlClient.SqlParameter("@deliveyType", deliveyType),
                   //new System.Data.SqlClient.SqlParameter("@DeliveryStatus", DeliveryStatus),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionStageDetails_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];



        }		
        public DataSet GetReqItemsToAddExtraItem(string sReqCode, string sVesselCode, string sCatalogID)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",sReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",sVesselCode),   
               new System.Data.SqlClient.SqlParameter("@CatalogID",sCatalogID),  
             };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_REQ_ITEMS", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public int InsertReqItemsToAddExtraItem(string sReqCode, string sItemsCode, string sVesselCode,
                                                   string sDocumentCode, string sOrderCode, string sDeliveryCode,
                                                   string sInvoice_Code, string sIDVals, string sItemDescription,
                                                   string sitemUnits, string sItemReqstQty, string sItemComments,
                                                   string sReqStatus, string sUserId, string UnitPrice, string Discount, string strSuppCode, string BgtCodes, string ItemRefCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new  SqlParameter("@ReqsCode",sReqCode), 
               new  SqlParameter("@ITEM_SYSTEM_CODE",sItemsCode), 
               new  SqlParameter("@Vessel_Code",sVesselCode),              
               new  SqlParameter("@DocumentCode",sDocumentCode), 
               new  SqlParameter("@OrderCode",sOrderCode),
               new  SqlParameter("@DeliveryCode",sDeliveryCode),
               new  SqlParameter("@Invoice_Code",sInvoice_Code), 
               new  SqlParameter("IDVals",sIDVals), 
               new  SqlParameter("@ItemDescription",sItemDescription),              
               new  SqlParameter("@itemUnits",sitemUnits), 
               new  SqlParameter("@ItemReqstQty",sItemReqstQty),
               new  SqlParameter("@ItemComments",sItemComments),
               new  SqlParameter("@ReqStatus",sReqStatus),
               new  SqlParameter("@UserId",sUserId),
               new SqlParameter("@UnitPrices",UnitPrice),
               new SqlParameter("@Discounts",Discount),
               new SqlParameter("@SupplierCode",strSuppCode),
               new SqlParameter("@BugtCodes",BgtCodes),
               new SqlParameter("@ItemRefCodes",ItemRefCode),
              };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_ReqItemsToAddExtraItem", obj);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public int INS_Add_Extra_Items(string sReqCode, string sVesselCode, string sDocumentCode, string sOrderCode, string sDeliveryCode, string sInvoice_Code, string sReqStatus, string sUserId, string strSuppCode, string SystemCode, DataTable dtExtra_Items)
        {
           
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new  SqlParameter("@ReqsCode",sReqCode), 
               new  SqlParameter("@ITEM_SYSTEM_CODE",SystemCode), 
               new  SqlParameter("@Vessel_Code",sVesselCode),              
               new  SqlParameter("@DocumentCode",sDocumentCode), 
               new  SqlParameter("@OrderCode",sOrderCode),
               new  SqlParameter("@DeliveryCode",sDeliveryCode),
               new  SqlParameter("@Invoice_Code",sInvoice_Code),                        
               new  SqlParameter("@ReqStatus",sReqStatus),
               new  SqlParameter("@UserId",sUserId),             
               new SqlParameter("@SupplierCode",strSuppCode),
               new SqlParameter("@tbl_Extra_Items",dtExtra_Items),
               new SqlParameter("@RETVAL",SqlDbType.Int)
              };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_Add_Extra_Items", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public int InsertRequisitionStageStatus(string sRequisitionCode, string sVesselCode, string sDocumentCode,
                                                  string ReqStatus, string ReqComments, int iCreated_By, DataTable dtQTNCodes)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
           {
               new SqlParameter("@VesselCode",sVesselCode),
               new SqlParameter("@ReqCode",sRequisitionCode),
               new SqlParameter("@DocmentCode",sDocumentCode),
               new SqlParameter("@ReqStatus",ReqStatus),
               new SqlParameter("@ReqComments",ReqComments),
               new SqlParameter("@LogInUser",iCreated_By),
               new SqlParameter("@tblQTNCodes",dtQTNCodes)
           };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_INS_REQUISITION_STAGE_STATUS", obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }




        public DataTable SelectNewRequisitionList_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtNewREQ = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                 ,new SqlParameter("@DEPT_CODE",deptcode)
               
               
                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                 ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
             };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_NewRequistionList_New", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectPendingRequistion_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, int? Min_Quot_Received, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();

                SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_CODE",deptcode)


                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                ,new SqlParameter("@Min_Quot_Received",Min_Quot_Received)

                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)

               
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_PendingRequistion_New", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetApproverList(string PoType)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();

                SqlParameter[] obj = new SqlParameter[]
                {
                 new SqlParameter("@PO_Type",PoType)
                };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_APPROVER_LIST", obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectPendingQuatationEvalution_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, string Quot_Received, string Approver,string LoggedIN_User,int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();

                SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_CODE",deptcode)

                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                ,new SqlParameter("@Quot_Received",Quot_Received)
                ,new SqlParameter("@Quot_Approver",Approver)
                ,new SqlParameter("@LOGEDIN_USER",LoggedIN_User)

                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)

               
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_PendingQuatationEvalution_New", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectPendingPurchasedOrderRaise_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, string DeliveryPort, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();

                SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_CODE",deptcode)

                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                ,new SqlParameter("@DeliveryPort",DeliveryPort)

                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)

               
            };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_PendingPurchasedOrderRaise_New", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectPendingPOConfirm_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, string Supplier, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                SqlParameter[] obj = new SqlParameter[]
                 {
                     new SqlParameter("@FLEET_ID",fleetid)
                    ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                    ,new SqlParameter("@DEPT_CODE",deptcode)

                    ,new SqlParameter("@PO_TYPE",potype)
                    ,new SqlParameter("@ACC_TYPE",acctype)
                    ,new SqlParameter("@REQSN_TYPE",reqsntype)
                    ,new SqlParameter("@SYSTEM",catalogue)


                    ,new SqlParameter("@FROM_DATE",FromDate)
                    ,new SqlParameter("@TO_DATE",ToDate)
                    ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                    ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                    ,new SqlParameter("@REQ_STATUS",reqstatus)

                    ,new SqlParameter("@DEPT_TYPE",depttype)
                    ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                    ,new SqlParameter("@Supplier",Supplier)

                    ,new SqlParameter("@PAGE_INDEX",pageindex)
                    ,new SqlParameter("@PAGE_SIZE",pagesize)
                    ,new SqlParameter("@SORT_BY",Sort_By)
                    ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                    ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
                };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_PendingPOConfirm_New", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectAllRequisitionStages_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                 ,new SqlParameter("@DEPT_CODE",deptcode)
               
               
                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                 ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_ALL_Requisition_Stages_New", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_Subsytem_Requisitionwise(string Reqsncode, string DocumentCode)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@REQUISITION_CODE",Reqsncode),
                new SqlParameter("@DOCUMENT_CODE",DocumentCode)
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_SUBSYSTEM_REQUISITIONWISE", obj);
            return ds.Tables[0];

        }
    }
}