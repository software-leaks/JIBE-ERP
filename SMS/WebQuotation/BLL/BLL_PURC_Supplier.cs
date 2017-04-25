using SMS.Data.PURC;
using System.Data;
using SMS.Data;
using System;

/// <summary>
/// Summary description for  Supplier
/// </summary>
namespace BLLQuotation
{

    public  class BLL_PURC_Purchase:IDisposable
    {


        DAL_PURC_Supplier objSupplier = new DAL_PURC_Supplier();

        public DataTable SelectSupplier()
        {

            return objSupplier.SelectSupplier();

        }

        public DataSet GetCountry()
        {

            return objSupplier.GetCountry();

        }

        public int InsertRequisitionDeliveryStages(int iOffice_ID, int iVessel_Code, string strDocument_Code, string strRequisition_Code, string strOrder_Code, string strDeliveryStage, string strDeliveryStageDate, string strForwarderName,
                                                    int iForwarderCountry, string strAgentCode, string strEstDevOnBoardDate, string strDeliveryType, string strRemark, int iCreated_By)
        {

            return objSupplier.InsertRequisitionDeliveryStages(iOffice_ID, iVessel_Code, strDocument_Code, strRequisition_Code, strOrder_Code, strDeliveryStage,
                                                                            strDeliveryStageDate, strForwarderName, iForwarderCountry, strAgentCode, strEstDevOnBoardDate,
                                                                            strDeliveryType, strRemark, iCreated_By);


        }


        public DataSet BindRequisitionDeliveryStages(string strRequisition_Code, string strDocument_Code, int iVessel_Code, string strOrder_Code)
        {

            return objSupplier.BindRequisitionDeliveryStages(strRequisition_Code, strDocument_Code, iVessel_Code, strOrder_Code);

        }



        public int InsertRequisitionStageStatus(string sRequisitionCode, string sVesselCode, string sDocumentCode,
                                        string ReqStatus, string ReqComments, int iCreated_By)
        {

            return objSupplier.InsertRequisitionStageStatus(sRequisitionCode, sVesselCode, sDocumentCode,
                                            ReqStatus, ReqComments, iCreated_By);

        }

        public DataTable SelectRequisitionDeliveryStatus(string VesselCode)
        {

            return objSupplier.SelectRequisitionDeliveryStatus(VesselCode);



        }

        public DataSet BindRequisitionDeliveryStagesLog(string strRequisition_Code, string strDocument_Code, int iVessel_Code, string strOrder_Code)
        {

            return objSupplier.BindRequisitionDeliveryStagesLog(strRequisition_Code, strDocument_Code, iVessel_Code, strOrder_Code);

        }

        public void Update_Progress(string QtnNumber)
        {
            objSupplier.Update_Progress(QtnNumber);
        }

        public static DataTable GET_SystemParameters(int Code)
        {
            return DAL_PURC_Supplier.GET_SystemParameters_DL(Code);
        }

        public static DataTable GET_ItemType(string QuotationCode, string ItemRefCode)
        {
            return DAL_PURC_Supplier.GET_ItemType_DL(QuotationCode, ItemRefCode);
        }

        public static int INSERT_ItemType(string QuotationCode, string ItemRefCode, int id, decimal UnitPrice, int ItemType)
        {
           return DAL_PURC_Supplier.INSERT_ItemType_DL(QuotationCode,ItemRefCode,id,UnitPrice,ItemType);
        }

        void IDisposable.Dispose()
        {
            
        }

        public DataTable getDeliveryPort()
        {

            return objSupplier.getDeliveryPort();


        }

        public DataTable GetSupplierUserDetails(string SuppCode, string User_Type)
        {


            return DAL_PURC_Supplier.GetSupplierUserDetails(SuppCode, User_Type);


        }
        public static DataSet Get_SentTo_Delivery_Status(string SuppCode)
        {
            return DAL_PURC_Supplier.Get_SentTo_Delivery_Status_DL(SuppCode);
        }

        public static int SaveAttachedFileInfo(string VesselCode, string ReqCode, string suppCode, string FileType, string FileName, string FilePath, string CreatedBy, int Port)
        {

            return DAL_PURC_Supplier.SaveAttachedFileInfo(VesselCode, ReqCode, suppCode, FileType, FileName, FilePath, CreatedBy, Port);

        }
        public static DataTable Get_VID_VesselDetails(int VesselID)
        {
            return DAL_PURC_Supplier.Get_VID_VesselDetails_DL(VesselID);
        }

        public static int GET_UPD_SUPPLIER_PASSWORD(string userid)
        {
            return DAL_PURC_Supplier.GET_UPD_SUPPLIER_PASSWORD_DL(userid);
        }

        public static int Get_NewContractRequest(string Supplier_Code)
        {
          return DAL_PURC_Supplier.Get_NewContractRequest_DL(Supplier_Code);
        }

        public static DataTable Get_Contract_Status(string QtnCode)
        {
           return DAL_PURC_Supplier.Get_Contract_Status_DL(QtnCode);
        }

    }

}
