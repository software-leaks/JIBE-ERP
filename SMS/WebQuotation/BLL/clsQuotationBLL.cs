using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using DALQuotation;


/// <summary>
/// Summary description for clsQuotation
/// </summary>
/// 

namespace BLLQuotation
{
    public class clsQuotationBLL
    {
        clsQuotationDAL objQuoDAL = new clsQuotationDAL();
        public clsQuotationBLL()
        {
        }

       public bool IsValidSupplier(string UserId, string strPassword)
       {
           return objQuoDAL.IsValidSupplier(UserId, strPassword);
       }

       public DataTable GetSupplierInfo(string UserId, string strPassword)
       {
           return objQuoDAL.GetSupplierInfo(UserId ,strPassword ); 
       }


       public DataTable GetSupplierName(string SupplierID)
       {
           return objQuoDAL.GetSupplierName(SupplierID);
       }


       public DataTable  GetVessel()
       {
           return objQuoDAL.GetVessel(); 
       }

       public DataTable GetWebQuotation(string strSupplierCode, string strVesselCode, string strStatus, string sFromDT, string sToDT, string Req_code, int? Page_Index, int? Page_Size, ref int IS_FETCH_COUNT)
       {
           IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
           DateTime FromDT = DateTime.Parse(sFromDT, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
           DateTime ToDT = DateTime.Parse(sToDT, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

           return objQuoDAL.GetWebQuotation(strSupplierCode,strVesselCode,strStatus,FromDT,ToDT,Req_code, Page_Index, Page_Size, ref IS_FETCH_COUNT);
       }



       //public DataTable GetWebQuotationItems(string strVesselCode, string strSupplierCode, string strReqcode, string strDocCode)
       //{
       //    return objQuoDAL.GetWebQuotation(strVesselCode, strSupplierCode);
       //}



       public DataTable GetCurrency()
       {
           return objQuoDAL.GetCurrency(); 
       }


       public DataTable GetRequisition_info(string strReqcode, string strDocCode)
       {
           return objQuoDAL.GetRequisition_info(strReqcode, strDocCode); 
       }

       public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode,string QtnCode)
       {
           return objQuoDAL.GetDataToGenerateRFQ(SuppCode, ReqCode, VesselCode, DocumentCode,QtnCode); 
       }


       public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode)
       {
           return objQuoDAL.GetDataToGenerateRFQ(SuppCode, ReqCode, VesselCode, DocumentCode); 
       }


       public string GetQuotStatus(string ReqCode, string DocumentCode, string strQuoSupp, string QTNCode)
       {
           return objQuoDAL.GetQuotStatus(ReqCode, DocumentCode, strQuoSupp, QTNCode); 
       }


       public DataTable  SearchQuotation(string SupplierCode,string VesselCode, string Status, string FrmDate, string ToDate,string Req_Code)
       {
           return objQuoDAL.SearchQuotation(SupplierCode,VesselCode, Status, FrmDate, ToDate,Req_Code); 
       }

       public DataTable PODetails(string RFQCODE, string QTCODE, string SUPLCODE, string DOCCODE, string VESSELCODE)
       {
           return objQuoDAL.PODetails(RFQCODE, QTCODE, SUPLCODE, DOCCODE, VESSELCODE); 
       }


       public DataTable GetDeptCode(string DeptCode)
       {
           return objQuoDAL.GetDeptCode(DeptCode); 
       }

       public DataTable GetRebateType()
       {
           return objQuoDAL.GetRebateType(); 
       }

       public DataSet GetInfoToSendEmailToESM(string ReqCode, string DocumentCode, string SupplierCode, string VesselCode)
        {

            return objQuoDAL.GetInfoToSendEmailToESM(ReqCode, DocumentCode, SupplierCode, VesselCode); 
        }

       public int UpdatePOConfirm(string ReqCode, string VesselCode, string DocumentCode, string QuotCode, string SuppCode)
       {
           return objQuoDAL.UpdatePOConfirm(ReqCode,VesselCode, DocumentCode, QuotCode, SuppCode); 
       
       }
        

       public int ExecuteQuery(string SqlQuery)
       {
           return objQuoDAL.ExecuteQuery(SqlQuery);
       
       }

       public int ExecuteQuery_String(string Query)
        {
             return objQuoDAL.ExecuteQuery_String(Query);
        }

        public System.Data.DataTable GetTable(string SqlQuery)
        {

            return objQuoDAL.GetTable(SqlQuery);
        }

        public DataSet GetRFQAttachment(string strReqCode, string strSupplierCode, Int32 iVesselCode)
        {
            return objQuoDAL.GetRFQAttachment(strReqCode, strSupplierCode, iVesselCode);
        }
        public DataSet UpdateSupplRemarks(string ReqCode, string QUTCODE, string DocCode, string VslCode, string ItemRefCode, string SuppCode, string Remarks)
        {
            return objQuoDAL.UpdateSupplRemarks(ReqCode, QUTCODE, DocCode, VslCode, ItemRefCode, SuppCode, Remarks);
        }

        public string GetSupplRemarks(string QUTCODE, string ItemRefCode)
        {
            return objQuoDAL.GetSupplRemarks(QUTCODE, ItemRefCode);
        }

        public int UpdChangePassword(string SuppCode, string oldpwd, string newpwd)
        {
            return objQuoDAL.UpdChangePassword(SuppCode, oldpwd, newpwd);
        }
        public DataTable GetItemType()
        {
            return objQuoDAL.GetItemType();
        }
        public DataSet GetMechDetails(string ReqCode)
        {
            return objQuoDAL.GetMechDetails(ReqCode);
        }
        public DataSet GetRequisitionSummary(string ReqCode, string DocCode, string VesselCode)
        {
            return objQuoDAL.GetRequisitionSummary(ReqCode, DocCode, VesselCode);
        }
        public string GetExchRate(string Curr_Code)
        {
            try
            {
                return objQuoDAL.GetExchRate(Curr_Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetSupplier_HSEQuestion(string SuppCode)
        {
            return objQuoDAL.GetSupplier_HSEQuestion(SuppCode);
        }

        
        public DataTable GetLastHSCQuesUpdate(string SuppCode)
        {
            return objQuoDAL.GetLastHSCQuesUpdate(SuppCode);
        
        }


        public int ins_ASL_DTL_Supplier_HSEQ
             (string Supplier, string Company_Name,
                string Company_Operations,
                string Company_Address,
                string Company_Country,
                string Company_Phone,
                string Company_Fax,
                string Company_Email,
                string Company_WebSite,
                string Company_PIC,
                string Emergency_Contact_No,
                string Size_Turnover,
                string Age_Of_Business,
                string Services_Provided,
                string Core_Business_Activity,
                string Regulatory_Control,
                string Regulatory_Control_Remarks,
                string Customer1_Name,
                string Customer2_Name,
                string Customer3_Name,
                string Customer1_Years,
                string Customer2_Years,
                string Customer3_Years,
                string Service_Description1,
                string Service_Description2,
                string Service_Description3,
                int Hours_Worked_YTD,
                int Hours_Worked_2Yrs,
                int Fatal_injuries_YTD,
                int Fatal_Injuries_2Yrs,
                int LostDay_Injuries_YTD,
                int LostDay_Injuries_2Yrs,
                int Incidence_Rate_YTD,
                int Incidence_Rate_2Yrs,
                string Government_Insp_3Yrs,
                string Significant_Incidents_3Yrs,
                string Insurance_Indemity,
                string Insurance_Indemity_Remarks,
                string Quality_Assurance,
                string Quality_Assurance_Remarks,
                string Key_Personnel_Qualifications,
                string Training_New_Employees,
                string Training_New_Employees_Remarks,
                string Training_Exisiting_Employees,
                string Training_Exisiting_Employees_Remarks,
                string Client_List,
                string Incident_Reporting,
                string Incident_Reporting_Remarks,
                string Near_Miss_Reporting,
                string Near_Miss_Reporting_Remarks,
                string Safety_Equipment,
                string Safety_Equipment_Remarks,
                string Safety_Equipment_Type,
                string Employee_Equip_Training,
                string Employee_Equip_Training_Remarks,
                string Contractor_Equip_Training_Remarks,
                string Contractor_Equip_Training,
                string Clean_Working_Environment,
                string Clean_Working_Environment_Remarks,
                string Equipment_Calibration,
                string Calibration_Certificates,
                string Equipment_Calibration_Remarks,
                string Calibration_Certificates_Remarks,
                string Client_Familiarization_visits_Remarks,
                string Client_Familiarization_visits,
                string Meet_Standard_Requirements,
                string Meet_Standard_Requirements_Remarks,
                DateTime HSEQ_Submitted_Date,
                string HSEQ_Submitted_By,
                string ESM_Competitive_Quote,
                string ESM_Competitive_Quote_Remarks,
                string ESM_Quick_Response,
                string ESM_Quick_Response_Remarks,
                string ESM_Ontime_Delivery,
                string ESM_Ontime_Delivery_Remarks,
                string ESM_Prompt_Advice,
                string ESM_Prompt_Advice_Remarks,
                int Created_By)
        {
            return objQuoDAL.ins_ASL_DTL_Supplier_HSEQ(Supplier, Company_Name, Company_Operations, Company_Address, Company_Country,
            Company_Phone, Company_Fax, Company_Email, Company_WebSite, Company_PIC, Emergency_Contact_No,
            Size_Turnover, Age_Of_Business, Services_Provided, Core_Business_Activity, Regulatory_Control,
            Regulatory_Control_Remarks, Customer1_Name, Customer2_Name, Customer3_Name, Customer1_Years,
            Customer2_Years, Customer3_Years, Service_Description1, Service_Description2, Service_Description3, Hours_Worked_YTD,
            Hours_Worked_2Yrs, Fatal_injuries_YTD, Fatal_Injuries_2Yrs, LostDay_Injuries_YTD, LostDay_Injuries_2Yrs, Incidence_Rate_YTD,
            Incidence_Rate_2Yrs, Government_Insp_3Yrs, Significant_Incidents_3Yrs, Insurance_Indemity, Insurance_Indemity_Remarks,
            Quality_Assurance, Quality_Assurance_Remarks, Key_Personnel_Qualifications, Training_New_Employees, Training_New_Employees_Remarks,
            Training_Exisiting_Employees, Training_Exisiting_Employees_Remarks, Client_List, Incident_Reporting, Incident_Reporting_Remarks,
            Near_Miss_Reporting, Near_Miss_Reporting_Remarks, Safety_Equipment, Safety_Equipment_Remarks, Safety_Equipment_Type,
            Employee_Equip_Training, Employee_Equip_Training_Remarks, Contractor_Equip_Training_Remarks, Contractor_Equip_Training,
            Clean_Working_Environment, Clean_Working_Environment_Remarks, Equipment_Calibration, Calibration_Certificates, Equipment_Calibration_Remarks,
            Calibration_Certificates_Remarks, Client_Familiarization_visits_Remarks, Client_Familiarization_visits, Meet_Standard_Requirements,
            Meet_Standard_Requirements_Remarks, HSEQ_Submitted_Date, HSEQ_Submitted_By, ESM_Competitive_Quote, ESM_Competitive_Quote_Remarks,
            ESM_Quick_Response, ESM_Quick_Response_Remarks, ESM_Ontime_Delivery, ESM_Ontime_Delivery_Remarks, ESM_Prompt_Advice,
            ESM_Prompt_Advice_Remarks, Created_By);

        }

        public DataSet GetSupplierScopebyscode(string SupplCode)
        {
            try
            {
                //objSubCatalog.Open(elogconn);
                return objQuoDAL.GetSupplierScopebyscode(SupplCode);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertSupplierScope(string RegScope, string UnRegScope, string CommentString)
        {
            try
            {
                //objSubCatalog.Open(elogconn);
                
                objQuoDAL.InsertSupplierScope(RegScope, UnRegScope, CommentString);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable SelectRequistionToSupplier(string ReqCode, string Documentcode)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataTable dtSupplierRequisition = objQuoDAL.SelectRequistionToSupplier(ReqCode, Documentcode);
                return dtSupplierRequisition;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable SelectSupplier()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataTable dtSupplier = objQuoDAL.SelectSupplier();
                return dtSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataSet SelectSupplier_Filter(string Country)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataSet dsSupplier = objQuoDAL.SelectSupplier_Filter(Country);
                return dsSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public string getQuotation(string QuatationNumber)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                string QuatationNum = objQuoDAL.getQuotation(QuatationNumber);
                return QuatationNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string getDeliveredNumber(string DeliveredNumber)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                string QuatationNum = objQuoDAL.getDeliveredNumber(DeliveredNumber);
                return QuatationNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int InsertQuoted_Price(string sqlQuery)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.InsertQuoted_Price(sqlQuery);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public DataTable GetDataExportToExcel(string sqlQuery)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataTable dtSupplier = objQuoDAL.GetDataExportToExcel(sqlQuery);
                return dtSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        //public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode)
        //{
        //    try
        //    {
        //        //objQuoDAL.Open(elogconn);
        //        DataSet dsSupplier = objQuoDAL.GetDataToGenerateRFQ(SuppCode, ReqCode, VesselCode, DocumentCode);
        //        return dsSupplier;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public DataSet GetDataToGenerateRFQForNotListedSupplier(string ReqCode, string VesselCode, string DocumentCode)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataSet dsSupplier = objQuoDAL.GetDataToGenerateRFQForNotListedSupplier(ReqCode, VesselCode, DocumentCode);
                return dsSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int InsertQuotedPriceForRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string CreatedBy, string QuotDueDate, string BuyerRemarks)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.InsertQuotedPriceForRFQ(SuppCode, ReqCode, VesselCode, DocumentCode, CreatedBy, QuotDueDate, BuyerRemarks);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetSupplierUserDetails(string SuppCode)
        {

            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSupplierUserDetails(SuppCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetRFQsuppInfoSendEmail(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string LogOnUserID)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetRFQsuppInfoSendEmail(SuppCode, ReqCode, VesselCode, DocumentCode, LogOnUserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRFQNotListedsuppSendEmail(string ReqCode, string VesselCode, string DocumentCode)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetRFQNotListedsuppSendEmail(ReqCode, VesselCode, DocumentCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectSuppCity()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.SelectSuppCity();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataTable SelectSuppCountry()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.SelectSuppCountry();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetAllCountries()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataSet dsSupplier = objQuoDAL.GetAllCountries();
                return dsSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetCountry()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                DataSet dsSupplier = objQuoDAL.GetCountry();
                return dsSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int InsertRequisitionDeliveryStages(int iOffice_ID, int iVessel_Code, string strDocument_Code, string strRequisition_Code, string strOrder_Code, string strDeliveryStage, string strDeliveryStageDate, string strForwarderName,
                                                    int iForwarderCountry, string strAgentCode, string strEstDevOnBoardDate, string strDeliveryType, string strRemark, int iCreated_By)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                int returnVal = objQuoDAL.InsertRequisitionDeliveryStages(iOffice_ID, iVessel_Code, strDocument_Code, strRequisition_Code, strOrder_Code, strDeliveryStage,
                                                                            strDeliveryStageDate, strForwarderName, iForwarderCountry, strAgentCode, strEstDevOnBoardDate,
                                                                            strDeliveryType, strRemark, iCreated_By);
                return returnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet BindRequisitionDeliveryStages(string strRequisition_Code, string strDocument_Code, int iVessel_Code, string strOrder_Code)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.BindRequisitionDeliveryStages(strRequisition_Code, strDocument_Code, iVessel_Code, strOrder_Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BindRequisitionDeliveryStagesLog(string strRequisition_Code, string strDocument_Code, int iVessel_Code, string strOrder_Code)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.BindRequisitionDeliveryStagesLog(strRequisition_Code, strDocument_Code, iVessel_Code, strOrder_Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetSuupplierHavingAttachment(string ReqCode)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSuupplierHavingAttachment(ReqCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeSupplierPOStatus(string ReqCode, string DocCode, string VslCode)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                objQuoDAL.ChangeSupplierPOStatus(ReqCode, DocCode, VslCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ViewSupplierDetails(string SuppCode)
        {

            //objQuoDAL.Open(elogconn);
            return objQuoDAL.ViewSupplierDetails(SuppCode);

        }

        public DataTable GetSendedRFQSuppliersList(string ReqCode, string VesselCode, string DocumentCode)
        {
            //objQuoDAL.Open(elogconn);
            return objQuoDAL.GetSendedRFQSuppliersList(ReqCode, VesselCode, DocumentCode);
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
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.InsertLibSupplier(strSUPPLIER, strSHORT_NAME, strFull_NAME, strSUPPLIER_CATEGORY,
                                    strSUPPLIER_TYPE, strBOOKS_CATEGORY, strSUPPLIER_STATUS, strLEDGER_CARD, strURL,
                                    strUSER_PWD, strREMARK, strADDRESS_1, strADDRESS_2, strADDRESS_3, strADDRESS_4,
                                    strADDRESS_5, strCOUNTRY, strCITY, strPOSTAL_CODE, strPHONE1, strPHONE2,
                                    strTELEX1, strTELEX2, strFAX1, strFAX2, strEMAIL1, strEMAIL2, strBANK_NAME,
                                    strBANK_BRANCH, strBANK_ACCOUNT, strBANK_ADDRESS, strBANK_COUNTRY, strBANK_CITY,
                                    strCURRENCY, strINTER_BANK_NAME, strINTER_BANK_BRANCH, strINTER_BANK_ACCOUNT, strINTER_BANK_ADDRESS,
                                    strINTER_BANK_COUNTRY, strINTER_BANK_CITY, strINTER_CURRENCY, iCreated_By);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.UpdateLibSupplier(strSUPPLIER, strSHORT_NAME, strFull_NAME, strSUPPLIER_CATEGORY,
                                    strSUPPLIER_TYPE, strBOOKS_CATEGORY, strSUPPLIER_STATUS, strLEDGER_CARD, strURL,
                                    strUSER_PWD, strREMARK, strADDRESS_1, strADDRESS_2, strADDRESS_3, strADDRESS_4,
                                    strADDRESS_5, strCOUNTRY, strCITY, strPOSTAL_CODE, strPHONE1, strPHONE2,
                                    strTELEX1, strTELEX2, strFAX1, strFAX2, strEMAIL1, strEMAIL2, strBANK_NAME,
                                    strBANK_BRANCH, strBANK_ACCOUNT, strBANK_ADDRESS, strBANK_COUNTRY, strBANK_CITY,
                                    strCURRENCY, strINTER_BANK_NAME, strINTER_BANK_BRANCH, strINTER_BANK_ACCOUNT, strINTER_BANK_ADDRESS,
                                    strINTER_BANK_COUNTRY, strINTER_BANK_CITY, strINTER_CURRENCY, iModified_By);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCityByCountryId(int iCountryId)
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetCityByCountryId(iCountryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierCategory()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSupplierCategory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierType()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSupplierType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierStatus()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSupplierStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierScope()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSupplierScope();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierBookingCategory()
        {
            try
            {
                //objQuoDAL.Open(elogconn);
                return objQuoDAL.GetSupplierBookingCategory();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierBySupplierCode(string strSupplierCode)
        {
            //objQuoDAL.Open(elogconn);
            return objQuoDAL.GetSupplierBySupplierCode(strSupplierCode);
        }

        public DataTable GetSupplierScopeDetails(string strSupplierCode)
        {
            //objQuoDAL.Open(elogconn);
            return objQuoDAL.GetSupplierScopeDetails(strSupplierCode);
        }

        public string Get_LegalTerm(int LegalType)
        {
            return objQuoDAL.Get_LegalTerm_DL(LegalType);
        }

        public string getQuotation_Status(string Requisitioncode, string QUOTATION_CODE, string SuppCode, string Document_Code, int Vessel_Code)
        {
            try
            {

                string QuatationStatus = objQuoDAL.getQuotation_Status(Requisitioncode, QUOTATION_CODE, SuppCode, Document_Code, Vessel_Code);
                return QuatationStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetRequistion_Quotation_Status(string RqsnCode, string QuotationCode)
        {
            return objQuoDAL.GetRequistion_Quotation_Status(RqsnCode, QuotationCode);
        }
    }
}