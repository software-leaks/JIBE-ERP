using SMS.Data.PURC;
using System.Data;
using System.Collections.Generic;
using SMS.Properties;
using SMS.Data;
using System;



/// <summary>
/// Summary description for OrderRaised
/// </summary>
/// 
namespace SMS.Business.PURC
{
    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_OrderRaised objOrderRaised = new DAL_PURC_OrderRaised();



        public DataTable SelectSupplierToSendOrderEval(string ReqCode, string VesselCode, string QTNCode)
        {

            return objOrderRaised.SelectSupplierToSendOrderEval(ReqCode, VesselCode, QTNCode);


        }
        public DataTable SelectPOToSendSupplier(string ReqCode, string VesselCode)
        {

            return objOrderRaised.SelectPOToSendSupplier(ReqCode, VesselCode);

        }
        public DataTable SelectSupplierToSendOrder(string ReqCode, string VesselCode)
        {

            return objOrderRaised.SelectSupplierToSendOrder(ReqCode, VesselCode);


        }

        public DataTable SelectItemsHierarchyForPenDeliveredReqsn(string ReqCode, string VesselCode)
        {

            return objOrderRaised.SelectItemsHierarchyForPenDeliveredReqsn(ReqCode, VesselCode);

        }



        public int UpdReqQuotationStatus(string ReqCode, string VesselCode, string DocumentCode, string suppCode, string UpdatedBy)
        {


            return objOrderRaised.UpdReqQuotationStatus(ReqCode, VesselCode, DocumentCode, suppCode, UpdatedBy);

        }



        public DataSet GetDataToGeneratPO(string ReqCode, string VesselCode, string SuppCode, string QuotationCode, string DocumentCode)
        {

            return objOrderRaised.GetDataToGeneratPO(ReqCode, VesselCode, SuppCode, QuotationCode, DocumentCode);


        }



        public int InsertDataForPO(string ReqCode, string VesselCode, string SuppCode, string QuotationCode, string CreatedBy, string DocumentCode)
        {

            return objOrderRaised.InsertDataForPO(ReqCode, VesselCode, SuppCode, QuotationCode, CreatedBy, DocumentCode);

        }
        public DataTable SelectItemsHierarchyForPO(string VesselCode, string REQUISITION_CODE, string REQ_Supplier)
        {

            return objOrderRaised.SelectItemsHierarchyForPO(VesselCode, REQUISITION_CODE, REQ_Supplier);


        }


        public int UpdateDataForPO(string ReqCode, string VesselCode, string ItemsString, string QtyString)
        {


            return objOrderRaised.UpdateDataForPO(ReqCode, VesselCode, ItemsString, QtyString);


        }
        public DataTable PODetails(string RFQCODE, string QTCODE, string SUPLCODE, string DOCCODE, string VESSELCODE)
        {


            return objOrderRaised.PODetails(RFQCODE, QTCODE, SUPLCODE, DOCCODE, VESSELCODE);

        }
        public DataTable POArrove(string RFQCODE, string QTCODE, string SUPLCODE, string USERNAME)
        {


            return objOrderRaised.POArrove(RFQCODE, QTCODE, SUPLCODE, USERNAME);

        }
        public void POApproving(string RFQCODE, string QTCODE, string SUPLCODE, string USERNAME, string Comment, string VesselCode, string BudgetCode)
        {


            objOrderRaised.POApproving(RFQCODE, QTCODE, SUPLCODE, USERNAME, Comment, VesselCode, BudgetCode);


        }
        public DataTable SelectREFQ(string RFQCODE, string VesselCode, string SUPLCODE, string QTCODE)
        {


            return objOrderRaised.SelectREFQ(RFQCODE, VesselCode, SUPLCODE, QTCODE);


        }

        public int PMSUpdOtherPODetails(string Dlvinstruction, string DlvPort, string ETA, string Remark, string ETD, string AgentDTL, string DOCUMENT_CODE, string REQUISITION_CODE, int Vessel_Code, string QUOTATION_CODE, string QUOTATION_SUPPLIER, int modified_by)
        {

            return objOrderRaised.PMSUpdOtherPODetails(Dlvinstruction, DlvPort, ETA, Remark, ETD, AgentDTL, DOCUMENT_CODE, REQUISITION_CODE, Vessel_Code, QUOTATION_CODE, QUOTATION_SUPPLIER,modified_by);

        }
        public DataSet CancelPODetails(string RFQCODE, string QTCODE, string SUPLCODE, string DOCCODE, string VESSELCODE)
        {
            return objOrderRaised.CancelPODetails(RFQCODE, QTCODE, SUPLCODE, DOCCODE, VESSELCODE);

        }

    }
}