using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;

/// <summary>
/// Summary description for  Supplier
/// </summary>
namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {


        DAL_PURC_Supplier objSupplier = new DAL_PURC_Supplier();


        
        public DataTable SelectRequistionToSupplier(string ReqCode, string Documentcode)
        {

            return objSupplier.SelectRequistionToSupplier(ReqCode, Documentcode);

        }
        public DataTable GeRequisitiontEvaluation(string ReqCode, string Documentcode)
        {

            return objSupplier.GeRequisitiontEvaluation(ReqCode, Documentcode);

        }
        public DataTable SelectSupplier()
        {

            return objSupplier.SelectSupplier();

        }

        public DataSet SelectSupplier_Filter(string Country)
        {


            return objSupplier.SelectSupplier_Filter(Country);


        }
        public DataSet SelectSupplier_Filter(string supcode, int? pagenumber, int? pagesize, ref int isfetchcount, string CITY, string SUPPLIER_NAME, string Country)
        {


            return objSupplier.SelectSupplier_Filter(supcode, pagenumber, pagesize, ref isfetchcount, CITY, SUPPLIER_NAME, Country);


        }
        public string getQuotation(string QuatationNumber)
        {

            return objSupplier.getQuotation(QuatationNumber);

        }
        public string Save_Approved_PO(int user, string DocCode, int qnty, string Item_ref_code)
        {

            return objSupplier.Save_Approved_PO(user, DocCode, qnty, Item_ref_code);

        }

        public string getDeliveredNumber(string DeliveredNumber)
        {

            return objSupplier.getDeliveredNumber(DeliveredNumber);


        }

        public int InsertQuoted_Price(string sqlQuery)
        {

            return objSupplier.InsertQuoted_Price(sqlQuery);



        }


        public DataTable GetDataExportToExcel(string sqlQuery)
        {

            return objSupplier.GetDataExportToExcel(sqlQuery);

        }



        public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode,string QtnCode="")
        {

            return objSupplier.GetDataToGenerateRFQ(SuppCode, ReqCode, VesselCode, DocumentCode,QtnCode);


        }
        public DataSet InsertSupplierProperties(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string VatApplicable, string QtnCode,int CreatedBy)
        {

            return objSupplier.InsertSupplierProperties(SuppCode, ReqCode, VesselCode, DocumentCode, VatApplicable, QtnCode, CreatedBy);
        }

        public DataSet GetDataToGenerateRFQForNotListedSupplier(string ReqCode, string VesselCode, string DocumentCode)
        {

            return objSupplier.GetDataToGenerateRFQForNotListedSupplier(ReqCode, VesselCode, DocumentCode);

        }

        public string InsertQuotedPriceForRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string CreatedBy, string QuotDueDate, string BuyerRemarks, string port, string dldate, string DeliveryInstruction,string Items)
        {
            DateTime Deliverydate = new DateTime();

            if (dldate == "")
            {
                dldate = "1900/01/01";
            }

            Deliverydate = DateTime.Parse(dldate);

            return objSupplier.InsertQuotedPriceForRFQ(SuppCode, ReqCode, VesselCode, DocumentCode, CreatedBy, QuotDueDate, BuyerRemarks, port, Deliverydate, DeliveryInstruction,Items);


        }

        public DataTable GetSupplierUserDetails(string SuppCode, string User_Type)
        {


            return objSupplier.GetSupplierUserDetails(SuppCode, User_Type);


        }

        public DataSet GetRFQsuppInfoSendEmail(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string LogOnUserID)
        {

            return objSupplier.GetRFQsuppInfoSendEmail(SuppCode, ReqCode, VesselCode, DocumentCode, LogOnUserID);

        }

        public DataTable GetRFQNotListedsuppSendEmail(string ReqCode, string VesselCode, string DocumentCode)
        {

            return objSupplier.GetRFQNotListedsuppSendEmail(ReqCode, VesselCode, DocumentCode);

        }

        public DataTable SelectSuppCity()
        {

            return objSupplier.SelectSuppCity();


        }
        public DataTable SelectSuppCountry()
        {

            return objSupplier.SelectSuppCountry();

        }

        public DataSet GetAllCountries()
        {

            return objSupplier.GetAllCountries();


        }

        public DataSet GetCountry()
        {

            return objSupplier.GetCountry();

        }

        public DataSet InsertRequisitionDeliveryStages(
           string strOrder_Code,
            string strEstDevOnBoardDate,
            string strDeliveryType,
            string strRemark,
            int iCreated_By,
            string SentTo_UserType,
            string SentTo_UserID,
            string Sent_Date,
            string Planned_SendDate,
            string Delivery_DateAtDestin,
            int Packet_Count,
            string Packet_Details,
            string RecvdBy_UserType,
            string RecvdBy_UserID,
            string Recvd_Date,
            int Recvd_AT,
            int EstDevOnBoar_Port,
            string SentFrom_UserType,
            string SentFrom_UserID
            )
        {

            return objSupplier.InsertRequisitionDeliveryStages(
                strOrder_Code,
                strEstDevOnBoardDate,
                strDeliveryType,
                strRemark,
                iCreated_By,
                SentTo_UserType,
                SentTo_UserID,
                Sent_Date,
                Planned_SendDate,
                Delivery_DateAtDestin,
                Packet_Count,
                Packet_Details,
                RecvdBy_UserType,
                RecvdBy_UserID,
                Recvd_Date,
                Recvd_AT,
                EstDevOnBoar_Port,
                SentFrom_UserType,
                SentFrom_UserID
                );


        }


        public DataSet BindRequisitionDeliveryStages(string strRequisition_Code, string strDocument_Code, int iVessel_Code, string strOrder_Code)
        {

            return objSupplier.BindRequisitionDeliveryStages(strRequisition_Code, strDocument_Code, iVessel_Code, strOrder_Code);

        }


        public DataSet BindRequisitionDeliveryStagesMovement(string strRequisition_Code, string   iVessel_Code, string strOrder_Code)
        {

            return objSupplier.BindRequisitionDeliveryStagesMovement(strRequisition_Code, iVessel_Code, strOrder_Code);
        }



        public int InsertRequisitionDeliveryStagesMovement(string strOrderCode, string strCurrentStage, string strStageHolderCode,
                                  string strDelivery_type ,string   OrderReadiness ,string Delivery_Remarks ,string strCurrentCity,
                                  string strCurrentPort, string strDeliveryDate, string strDeliveryPort, string strRemarks, string strCreatedby 
            , int  intCurrentActiveStage)
        {   

            if (OrderReadiness == null || OrderReadiness == "")
                OrderReadiness = "1900/01/01";

            if (strDeliveryDate == null || strDeliveryDate == "")
                strDeliveryDate = "1900/01/01";

            DateTime dtOrderReadiness = DateTime.Parse(OrderReadiness);
            DateTime dtDeliveryDate = DateTime.Parse(strDeliveryDate);

            return objSupplier.InsertRequisitionDeliveryStagesMovement(strOrderCode, strCurrentStage, strStageHolderCode, strDelivery_type, dtOrderReadiness
                                                                        , Delivery_Remarks, strCurrentCity, strCurrentPort, dtDeliveryDate
                                                                        , strDeliveryPort, strRemarks, strCreatedby, intCurrentActiveStage);
        }



        public DataSet BindRequisitionDeliveryStagesLog( string strOrder_Code)
        {

            return objSupplier.BindRequisitionDeliveryStagesLog(strOrder_Code);

        }
        public DataSet GetSuupplierHavingAttachment(string ReqCode)
        {

            return objSupplier.GetSuupplierHavingAttachment(ReqCode);

        }

        public void ChangeSupplierPOStatus(string ReqCode, string DocCode, string VslCode)
        {

            objSupplier.ChangeSupplierPOStatus(ReqCode, DocCode, VslCode);

        }


        public DataTable ViewSupplierDetails(string SuppCode)
        {


            return objSupplier.ViewSupplierDetails(SuppCode);

        }

        public DataTable GetSendedRFQSuppliersList(string ReqCode, string VesselCode, string DocumentCode)
        {

            return objSupplier.GetSendedRFQSuppliersList(ReqCode, VesselCode, DocumentCode);
        }

        public int InsertLibSupplier(string strSUPPLIER, string strSHORT_NAME, string strFull_NAME, string strSUPPLIER_CATEGORY,
                                    string strSUPPLIER_TYPE, string strBOOKS_CATEGORY, string strSUPPLIER_STATUS, string strLEDGER_CARD, string strURL,
                                    string strUSER_PWD, string strREMARK, string strADDRESS_1, string strADDRESS_2, string strADDRESS_3, string strADDRESS_4,
                                    string strADDRESS_5, string strCOUNTRY, string strCITY, string strPOSTAL_CODE, string strPHONE1, string strPHONE2,
                                    string strTELEX1, string strTELEX2, string strFAX1, string strFAX2, string strEMAIL1, string strEMAIL2, string strBANK_NAME,
                                    string strBANK_BRANCH, string strBANK_ACCOUNT, string strBANK_ADDRESS, string strBANK_COUNTRY, string strBANK_CITY,
                                    string strCURRENCY, string strINTER_BANK_NAME, string strINTER_BANK_BRANCH, string strINTER_BANK_ACCOUNT, string strINTER_BANK_ADDRESS,
                                    string strINTER_BANK_COUNTRY, string strINTER_BANK_CITY, string strINTER_CURRENCY, int iCreated_By)
        {
            return objSupplier.InsertLibSupplier(strSUPPLIER, strSHORT_NAME, strFull_NAME, strSUPPLIER_CATEGORY,
                                strSUPPLIER_TYPE, strBOOKS_CATEGORY, strSUPPLIER_STATUS, strLEDGER_CARD, strURL,
                                strUSER_PWD, strREMARK, strADDRESS_1, strADDRESS_2, strADDRESS_3, strADDRESS_4,
                                strADDRESS_5, strCOUNTRY, strCITY, strPOSTAL_CODE, strPHONE1, strPHONE2,
                                strTELEX1, strTELEX2, strFAX1, strFAX2, strEMAIL1, strEMAIL2, strBANK_NAME,
                                strBANK_BRANCH, strBANK_ACCOUNT, strBANK_ADDRESS, strBANK_COUNTRY, strBANK_CITY,
                                strCURRENCY, strINTER_BANK_NAME, strINTER_BANK_BRANCH, strINTER_BANK_ACCOUNT, strINTER_BANK_ADDRESS,
                                strINTER_BANK_COUNTRY, strINTER_BANK_CITY, strINTER_CURRENCY, iCreated_By);

        }

        public int UpdateLibSupplier(string strSUPPLIER, string strSHORT_NAME, string strFull_NAME, string strSUPPLIER_CATEGORY,
                                    string strSUPPLIER_TYPE, string strBOOKS_CATEGORY, string strSUPPLIER_STATUS, string strLEDGER_CARD, string strURL,
                                    string strUSER_PWD, string strREMARK, string strADDRESS_1, string strADDRESS_2, string strADDRESS_3, string strADDRESS_4,
                                    string strADDRESS_5, string strCOUNTRY, string strCITY, string strPOSTAL_CODE, string strPHONE1, string strPHONE2,
                                    string strTELEX1, string strTELEX2, string strFAX1, string strFAX2, string strEMAIL1, string strEMAIL2, string strBANK_NAME,
                                    string strBANK_BRANCH, string strBANK_ACCOUNT, string strBANK_ADDRESS, string strBANK_COUNTRY, string strBANK_CITY,
                                    string strCURRENCY, string strINTER_BANK_NAME, string strINTER_BANK_BRANCH, string strINTER_BANK_ACCOUNT, string strINTER_BANK_ADDRESS,
                                    string strINTER_BANK_COUNTRY, string strINTER_BANK_CITY, string strINTER_CURRENCY, int iModified_By)
        {

            return objSupplier.UpdateLibSupplier(strSUPPLIER, strSHORT_NAME, strFull_NAME, strSUPPLIER_CATEGORY,
                                strSUPPLIER_TYPE, strBOOKS_CATEGORY, strSUPPLIER_STATUS, strLEDGER_CARD, strURL,
                                strUSER_PWD, strREMARK, strADDRESS_1, strADDRESS_2, strADDRESS_3, strADDRESS_4,
                                strADDRESS_5, strCOUNTRY, strCITY, strPOSTAL_CODE, strPHONE1, strPHONE2,
                                strTELEX1, strTELEX2, strFAX1, strFAX2, strEMAIL1, strEMAIL2, strBANK_NAME,
                                strBANK_BRANCH, strBANK_ACCOUNT, strBANK_ADDRESS, strBANK_COUNTRY, strBANK_CITY,
                                strCURRENCY, strINTER_BANK_NAME, strINTER_BANK_BRANCH, strINTER_BANK_ACCOUNT, strINTER_BANK_ADDRESS,
                                strINTER_BANK_COUNTRY, strINTER_BANK_CITY, strINTER_CURRENCY, iModified_By);

        }

        public DataSet GetCityByCountryId(int iCountryId)
        {

            return objSupplier.GetCityByCountryId(iCountryId);

        }

        public DataSet GetSupplierCategory()
        {

            return objSupplier.GetSupplierCategory();

        }

        public DataSet GetSupplierType()
        {

            return objSupplier.GetSupplierType();

        }

        public DataSet GetSupplierStatus()
        {
            return objSupplier.GetSupplierStatus();

        }

        public DataSet GetSupplierScope()
        {

            return objSupplier.GetSupplierScope();

        }

        public DataSet GetSupplierBookingCategory()
        {

            return objSupplier.GetSupplierBookingCategory();

        }

        public DataSet GetSupplierBySupplierCode(string strSupplierCode)
        {

            return objSupplier.GetSupplierBySupplierCode(strSupplierCode);
        }

        public DataTable GetSupplierScopeDetails(string strSupplierCode)
        {

            return objSupplier.GetSupplierScopeDetails(strSupplierCode);
        }

    }

}
