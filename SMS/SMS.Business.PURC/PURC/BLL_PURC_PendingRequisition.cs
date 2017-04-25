
using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;

/// <summary>
/// Summary description for Vessels
/// </summary>
namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_PendingRequisition objPendingReq = new DAL_PURC_PendingRequisition();

        public DataTable SelectRequisitionForHierarchy()
        {

            return objPendingReq.SelectRequisitionForHierarchy();


        }

        public DataTable SelectItemsForHierarchy(string strRequistionCode, string strVesselCode, string strDocumentCode)
        {

            return objPendingReq.SelectItemsForHierarchy(strRequistionCode, strVesselCode, strDocumentCode);

        }



        public DataTable SelectPendingRequistion(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingRequistion(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }

        public DataTable SelectPendingRequistion(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingRequistion(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }

        public DataTable SelectPendingQuatationReceive(string VesselCode)
        {

            return objPendingReq.SelectPendingQuatationReceive(VesselCode);

        }

        public DataTable SelectPendingQuatationEvalution(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? LogedIn_User, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingQuatationEvalution(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, LogedIn_User, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }


        public DataTable SelectPendingQuatationEvalution(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? LogedIn_User, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingQuatationEvalution(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, LogedIn_User, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }

        public DataTable SelectPendingPurchasedOrderRaise(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingPurchasedOrderRaise(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }


        public DataTable SelectPendingPurchasedOrderRaise(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingPurchasedOrderRaise(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }


        public DataTable SelectPendingPOConfirm(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingPOConfirm(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }

        public DataTable SelectPendingPOConfirm(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingPOConfirm(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }

        public DataTable SelectPendingPOConfirm_Export(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
         , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount)
        {

            return objPendingReq.SelectPendingPOConfirm_Export(fleetid, vesselcode, depttype, deptcode
                , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount);


        }

        public DataTable SelectPendingDeliveryUpdate(int? Fleet_ID, int? Vessel_Code, string Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {

            return objPendingReq.SelectPendingDeliveryUpdate(Fleet_ID, Vessel_Code, Dept_Code, Dept_Type, REQ_ORD_Code, Reqsn_Type, Page_Index, Page_Size, ref  Is_Fetch_Count);


        }

        public DataTable SelectPendingDeliveryUpdate(DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {

            return objPendingReq.SelectPendingDeliveryUpdate(Fleet_ID, Vessel_Code, Dept_Code, Dept_Type, REQ_ORD_Code, Reqsn_Type, Page_Index, Page_Size, ref  Is_Fetch_Count);


        }

        public DataTable SelectAllRequisitionStages(int? Fleet_ID, int? Vessel_Code, string Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, string Reqsn_Status, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectAllRequisitionStages(Fleet_ID, Vessel_Code, Dept_Code, Dept_Type, REQ_ORD_Code, Reqsn_Type, Reqsn_Status, Page_Index, Page_Size, ref Is_Fetch_Count, Sort_By, Sort_Direction);


        }

        public DataTable SelectAllRequisitionStages(DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, string Reqsn_Status, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectAllRequisitionStages(Fleet_ID, Vessel_Code, Dept_Code, Dept_Type, REQ_ORD_Code, Reqsn_Type, Reqsn_Status, Page_Index, Page_Size, ref Is_Fetch_Count,Sort_By,Sort_Direction);


        }
        public DataTable GetREQStatus()
        {

            return objPendingReq.GetREQStatus();

        }
        public DataTable GetREQStatusBYCode(string Code)
        {
            return objPendingReq.GetREQStatusBYCode(Code);

        }
        public DataTable SelectNewRequisitionList(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectNewRequisitionList(fleetid, vesselcode, depttype, deptcode
                            , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);

        }

        public DataTable SelectNewRequisitionList(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectNewRequisitionList(fleetid, vesselcode, depttype, deptcode
                            , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount,Sort_By,Sort_Direction);

        }
        public int InsRequisitionOnHoldLogHistory(string sRequisitionCode, string sVesselCode, string sDocumentCode, string sLineType, string sOnHold, string sRemarks, string sSupplier, int Created_By)
        {

            return objPendingReq.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVesselCode, sDocumentCode, sLineType, sOnHold, sRemarks, sSupplier, Created_By);

        }



        public DataTable GetRequisitionOnHoldLogHistory()
        {

            return objPendingReq.GetRequisitionOnHoldLogHistory();


        }

        public DataTable GetRequisitionOnHoldLogHistory(int? Fleet, int? VesselID, string Stag, string ReqsnCode, string UserName, string DocumentCode, int? ddhold, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objPendingReq.GetRequisitionOnHoldLogHistory(Fleet, VesselID, Stag, ReqsnCode, UserName, DocumentCode, ddhold, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public DataTable GetRequisitionOnHoldLogHistory_ByReqsn(string Reqsn)
        {
            return objPendingReq.GetRequisitionOnHoldLogHistory_ByReqsn(Reqsn);
        }

        public int CancelRequisitionStages(int sVesselCode, string sRequisitionCode, string sDocumentCode, string sCancelStage, string sCancelReason, int iCreated_By, string FromStage, string QuotationCode = "")
        {

            return objPendingReq.CancelRequisitionStages(sVesselCode, sRequisitionCode, sDocumentCode, sCancelStage, sCancelReason, iCreated_By, FromStage, QuotationCode);

        }

        public DataTable GetRequisitionStageDetails(string sVesselCode, string sRequisitionCode, string sFrmDate, string sToDate)
        {

            return objPendingReq.GetRequisitionStageDetails(sVesselCode, sRequisitionCode, sFrmDate, sToDate);

        }
        public DataTable GetRequisitionStageDetails(int? Fleet, int? VesselID, string RequisitionNo, string PONo, string Department, string UrgencyofReq,int? timeLaps, int? HoldFlage,DateTime? fromdate, DateTime? Todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objPendingReq.GetRequisitionStageDetails(Fleet, VesselID, RequisitionNo, PONo, Department, UrgencyofReq, timeLaps, HoldFlage, fromdate, Todate,  sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        
        public DataSet GetReqItemsToAddExtraItem(string sReqCode, string sVesselCode, string sCatalogID)
        {

            return objPendingReq.GetReqItemsToAddExtraItem(sReqCode, sVesselCode, sCatalogID);

        }


        public int InsertReqItemsToAddExtraItem(string sReqCode, string sItemsCode, string sVesselCode,
                                                string sDocumentCode, string sOrderCode, string sDeliveryCode,
                                                string sInvoice_Code, string sIDVals, string sItemDescription,
                                                string sitemUnits, string sItemReqstQty, string sItemComments,
                                                string sReqStatus, string sUserId, string UnitPrice, string Discount, string strSuppCode, string BgtCodes, string ItemRefCode)
        {


            return objPendingReq.InsertReqItemsToAddExtraItem(sReqCode, sItemsCode, sVesselCode,
                                                                            sDocumentCode, sOrderCode, sDeliveryCode,
                                                                            sInvoice_Code, sIDVals, sItemDescription,
                                                                            sitemUnits, sItemReqstQty, sItemComments,
                                                                            sReqStatus, sUserId, UnitPrice, Discount, strSuppCode, BgtCodes, ItemRefCode);


        }

        public int INS_Add_Extra_Items(string sReqCode, string sVesselCode, string sDocumentCode, string sOrderCode, string sDeliveryCode, string sInvoice_Code, string sReqStatus, string sUserId, string strSuppCode, string SystemCode, DataTable dtExtra_Items)
        {
            return objPendingReq.INS_Add_Extra_Items(sReqCode, sVesselCode, sDocumentCode, sOrderCode, sDeliveryCode, sInvoice_Code, sReqStatus, sUserId, strSuppCode, SystemCode, dtExtra_Items);
        }

        public int InsertRequisitionStageStatus(string sRequisitionCode, string sVesselCode, string sDocumentCode,
                                                string ReqStatus, string ReqComments, int iCreated_By, DataTable dtQTNCodes)
        {

            return objPendingReq.InsertRequisitionStageStatus(sRequisitionCode, sVesselCode, sDocumentCode,
                                            ReqStatus, ReqComments, iCreated_By,dtQTNCodes);

        }

        #region New purchase CR
        public DataTable SelectNewRequisitionList_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectNewRequisitionList_New(fleetid, vesselcode, depttype, deptcode
                            , req_order_code,potype,acctype,reqsntype,catalogue,FromDate,ToDate,accClass,dtUrgency,reqstatus, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);

        }

        public DataTable SelectPendingRequistion_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, int? Min_Quot_Received, int? pageindex, int? pagesize, 
                            ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingRequistion_New(fleetid, vesselcode, depttype, deptcode
                ,  req_order_code,potype,acctype,reqsntype,catalogue,FromDate,ToDate,accClass,dtUrgency,reqstatus,Min_Quot_Received, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }
        public DataTable SelectPendingQuatationEvalution_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, string Quot_Received, string Approver, string LoggedIN_User, int? pageindex, int? pagesize, 
                            ref int isfetchcount, string Sort_By, string Sort_Direction)
        {

            return objPendingReq.SelectPendingQuatationEvalution_New(fleetid, vesselcode, depttype, deptcode
                , req_order_code, potype, acctype, reqsntype, catalogue, FromDate, ToDate, accClass, dtUrgency, reqstatus, Quot_Received,Approver,LoggedIN_User, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);


        }
        public DataTable SelectPendingPurchasedOrderRaise_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, string DeliveryPort, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return objPendingReq.SelectPendingPurchasedOrderRaise_New(fleetid, vesselcode, depttype, deptcode, req_order_code, potype, acctype, reqsntype, catalogue, FromDate, ToDate, accClass, dtUrgency, reqstatus, DeliveryPort, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }
        public DataTable SelectPendingPOConfirm_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, string Supplier, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return objPendingReq.SelectPendingPOConfirm_New(fleetid, vesselcode, depttype, deptcode, req_order_code, potype, acctype, reqsntype, catalogue, FromDate, ToDate, accClass, dtUrgency, reqstatus, Supplier, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }
        public DataTable SelectAllRequisitionStages_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, DateTime? FromDate, DateTime? ToDate, string accClass, string dtUrgency, string reqstatus, int? pageindex, int? pagesize, 
                            ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return objPendingReq.SelectAllRequisitionStages_New(fleetid, vesselcode, depttype, deptcode, req_order_code,potype,acctype,reqsntype,catalogue,FromDate,ToDate,accClass,dtUrgency,reqstatus, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }
         public DataTable GetApproverList(string PoType)
         {
             return objPendingReq.GetApproverList(PoType);


         }
         public DataTable Get_Subsytem_Requisitionwise(string Reqsncode,string DocumentCode)
         {
             return objPendingReq.Get_Subsytem_Requisitionwise(Reqsncode, DocumentCode);
         }

        #endregion

    }
}