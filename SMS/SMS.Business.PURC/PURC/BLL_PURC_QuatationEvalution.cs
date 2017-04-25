using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;

/// <summary>
/// Summary description for QuatationEvalution
/// </summary>
/// 
namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_QuatationEvalution objQuatationEvalution = new DAL_PURC_QuatationEvalution();



        public DataTable SelectQuatationSendBySupplier(string req, string Vessel_code, string DocumentCode)
        {

            return objQuatationEvalution.SelectQuatationSendBySupplier(req, Vessel_code, DocumentCode);



        }

        public DataTable SelectQuatationSendBySupplier_Report(string req, string Vessel_code, string DocumentCode)
        {

            return objQuatationEvalution.SelectQuatationSendBySupplier_Report(req, Vessel_code, DocumentCode);



        }
        public DataTable SelectItemsQuatationSuppliedBySupplier(string strQuery)
        {

            return objQuatationEvalution.SelectItemsQuatationSuppliedBySupplier(strQuery);



        }


        public DataTable getRequisionForQuatation()
        {

            return objQuatationEvalution.getRequisionForQuatation();


        }



        public DataTable GetSupplierForQuatation(string Reqcode, string VesselCode, string DocumentCode)
        {

            return objQuatationEvalution.GetSupplierForQuatation(Reqcode, VesselCode, DocumentCode);


        }

        public DataTable GetItemsSendedToSupplier(string ReqCode)
        {

            return objQuatationEvalution.GetItemsSendedToSupplier(ReqCode);


        }


        public int UpdateQuotForRework(string ReqCode, string VesselCode, string DocumentCode, string SuppCode, string QuotCode)
        {

            return objQuatationEvalution.UpdateQuotForRework(ReqCode, VesselCode, DocumentCode, SuppCode, QuotCode);
        }


        public DataTable CountRFQSendForReQuotation(string ReqCode, string VesselCode, string DocumentCode)
        {

            return objQuatationEvalution.CountRFQSendForReQuotation(ReqCode, VesselCode, DocumentCode);

        }

        public DataTable SelectPendingPOApproval(string VesselCode)
        {

            return objQuatationEvalution.SelectPendingPOApproval(VesselCode);



        }

        public DataTable SelectRequisitionDeliveryStatus(int? CurrentCity, int? CurrentPort, int? DeliveryPort, string CurrentStage, int? Fleet_ID, int? Vessel_Code, string Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type,string Supplier_name,string PO_Date, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {

            return objQuatationEvalution.SelectRequisitionDeliveryStatus( CurrentCity,  CurrentPort,  DeliveryPort, CurrentStage,  Fleet_ID,  Vessel_Code,  Dept_Code,  Dept_Type,  REQ_ORD_Code,  Reqsn_Type,Supplier_name, PO_Date,  Page_Index,  Page_Size, ref  Is_Fetch_Count,Sort_By,Sort_Direction);



        }

        public DataTable SelectRequisitionDeliveryStatus(int? CurrentCity, int? CurrentPort, int? DeliveryPort, DataTable CurrentStage
            , DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, DataTable Supplier_name
            , DateTime? PO_Date, DateTime? To_PO_Date, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {

            return objQuatationEvalution.SelectRequisitionDeliveryStatus(CurrentCity, CurrentPort, DeliveryPort, CurrentStage
                , Fleet_ID, Vessel_Code, Dept_Code, Dept_Type, REQ_ORD_Code, Reqsn_Type, Supplier_name, PO_Date, To_PO_Date, Page_Index, Page_Size, ref  Is_Fetch_Count, Sort_By, Sort_Direction);



        }







        public DataTable SelectRequisitionDeliveryStatus_Export(int? CurrentCity, int? CurrentPort, int? DeliveryPort, DataTable CurrentStage
            , DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code
            , int? Reqsn_Type, DataTable Supplier_name, DateTime? PO_Date, DateTime? To_PO_Date, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {

            return objQuatationEvalution.SelectRequisitionDeliveryStatus_Export(CurrentCity, CurrentPort, DeliveryPort, CurrentStage, Fleet_ID, Vessel_Code, Dept_Code, Dept_Type, REQ_ORD_Code, Reqsn_Type, Supplier_name, PO_Date,To_PO_Date, Page_Index, Page_Size, ref  Is_Fetch_Count);



        }

        public DataTable Get_Approval_Limit(int UserID, string DeptShortCode)
        {
            return objQuatationEvalution.Get_Approval_Limit_DL(UserID, DeptShortCode);
        }

        public DataTable Get_Approver_History(string Reqsn)
        {
           return objQuatationEvalution.Get_Approver_History_DL(Reqsn);
        }

        public DataTable Get_Delivery_History(string ItemSystemcode ,string VesselCode)
        {
            return objQuatationEvalution.Get_Delivery_History_DL(ItemSystemcode, VesselCode);  
        }
        public DataSet GetSupplierQuote(string req, string Vessel_code, string DocumentCode)
        {
            return objQuatationEvalution.GetSupplierQuote(req,Vessel_code,DocumentCode);
        }
        public DataTable SelectRequisitionDeliveryStatus_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, string FromDate, string ToDate, string accClass, string dtUrgency, string reqstatus, string Delivery_Port, string Supplier, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return objQuatationEvalution.SelectRequisitionDeliveryStatus_New(fleetid, vesselcode, depttype, deptcode, req_order_code, potype, acctype, reqsntype, catalogue, FromDate, ToDate, accClass, dtUrgency, reqstatus, Delivery_Port, Supplier, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }
        
    }
}