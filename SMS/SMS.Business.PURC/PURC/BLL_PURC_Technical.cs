using System;
using System.Data;
using System.Configuration;
using ClsDALTechnical;

/// <summary>
/// Summary description for TechnicalBAL
/// </summary>
/// 

namespace ClsBLLTechnical
{
    public partial class TechnicalBAL
    {

        DALTechnical objDALTech = new DALTechnical();

        public TechnicalBAL()
        {
        }

        public int UpdateDelivery(string ReqCode, string DocCode, string SuppCode, string OrderCode
            , string VesselCode, string CreatedBy, string PayAt, decimal TotalPay, decimal RoundoffAmt,
            string SystemCode, string DeptCode, string ItemIDs, string DelQtys, string ROBQtys, decimal dclSuppOrdDiscount)
        {
            return objDALTech.UpdateDelivery(ReqCode, DocCode, SuppCode, OrderCode, VesselCode, CreatedBy, PayAt, TotalPay, RoundoffAmt, SystemCode, DeptCode, ItemIDs, DelQtys, ROBQtys, dclSuppOrdDiscount);

        }


        public DataSet POApprovalHierarchy(string ReqCode, string DocCode, string QuotCode, string SuppCode, string VesselCode, string LogInUser, string CategoryCode)
        {
            return objDALTech.POApprovalHierarchy(ReqCode, DocCode, QuotCode, SuppCode, VesselCode, LogInUser, CategoryCode);

        }
        public DataSet InvoiceApprovalEmil(string ReqCode, string InvoiceNo, string SuppCode, string VesselCode, string LogInUser)
        {
            return objDALTech.InvoiceApprovalEmil(ReqCode, InvoiceNo, SuppCode, VesselCode, LogInUser);

        }


        public int InsertUserApprovalEntries(string ReqCode, string DocCode, string VesselCode, string LogInUser, string ApproverID, decimal RqstAmt, decimal ApporveAmt, string Supplier, string Remark, DataTable dtQuotationList)
        {
            return objDALTech.InsertUserApprovalEntries(ReqCode, DocCode, VesselCode, LogInUser, ApproverID, RqstAmt, ApporveAmt, Supplier, Remark,  dtQuotationList);
        }


        public int ExecuteQuery(string SqlQuery)
        {
            return objDALTech.ExecuteQuery(SqlQuery);
        }

        public System.Data.DataTable GetTable(string SqlQuery)
        {

            return objDALTech.GetTable(SqlQuery);
        }
        public int UpdatePOConfirmation(string ReqCode, string DocCode, string SuppCode, string VesselCode, string OrdCode, string QuotCode)
        {
            return objDALTech.UpdatePOConfirmation(ReqCode, DocCode, SuppCode, VesselCode, OrdCode, QuotCode);
        }

        public int UpdateReqForApprovalPending(string ReqCode, string DocCode, string QuotCode, string SuppCode, string VesselCode, string Approver, string BudgetCode, string Comment)
        {
            return objDALTech.UpdateReqForApprovalPending(ReqCode, DocCode, QuotCode, SuppCode, VesselCode, Approver, BudgetCode, Comment);
        }

        public DataSet GetPMS_Report_POApproval(string vcode, string dcode)
        {
            return objDALTech.GetPMS_Report_POApproval(vcode, dcode);
        }
        public DataSet GetPMS_Report_DeliveryStatus(string vcode, string dcode)
        {
            return objDALTech.GetPMS_Report_DeliveryStatus(vcode, dcode);
        }



        public DataTable GetToopTipsForQtnRecve()
        {
            return objDALTech.GetToopTipsForQtnRecve();
        }


        public DataTable GetToopTipsForQtnSent()
        {
            return objDALTech.GetToopTipsForQtnSent();
        }

        public DataTable GetToopTipsForQtnINProgress()
        {
            return objDALTech.GetToopTipsForQtnINProgress();
        }

        public DataSet GetRequisitionSummary(string ReqCode, string DocCode, string VesselCode)
        {
            return objDALTech.GetRequisitionSummary(ReqCode, DocCode, VesselCode);
        }
        public DataSet GetSubCatalogueDetails(string ReqCode, string Catalogue_Code, string VesselCode)
        {
            return objDALTech.GetSubCatalogueDetails(ReqCode, Catalogue_Code, VesselCode);
        }
        public DataSet CancelledGetRequisitionSummary(string ReqCode, string DocCode, string VesselCode)
        {
            return objDALTech.CancelledGetRequisitionSummary(ReqCode, DocCode, VesselCode);
        }


        public DataSet GetDeliveryOrderSummary(string ReqCode, string DocCode, string VesselCode, string DeliverCode)
        {
            return objDALTech.GetDeliveryOrderSummary(ReqCode, DocCode, VesselCode, DeliverCode);
        }


        public DataSet GetReqsnOrderApprovalSummary(string ReqCode, string DocCode, string VesselCode, string OrderNumber, string SuppCode)
        {
            return objDALTech.GetReqsnOrderApprovalSummary(ReqCode, DocCode, VesselCode, OrderNumber, SuppCode);
        }


        public DataSet GetRequQuotationSummary(string ReqCode, string DocCode, string VesselCode, string QuotCode)
        {
            return objDALTech.GetRequQuotationSummary(ReqCode, DocCode, VesselCode, QuotCode);
        }

        public DataTable GetSuppCodeQuotCode(string VesselCode, string ReqCode, string OrderCode)
        {
            return objDALTech.GetSuppCodeQuotCode(VesselCode, ReqCode, OrderCode);
        }
        public DataTable GetRequsitionDetails(string VesselCode, string ReqCode, string ReqStatus)
        {
            return objDALTech.GetRequsitionDetails(VesselCode, ReqCode, ReqStatus);
        }


        public int RequisitionItemsOnSplit(string VesselCode, string ReqCode, string DocCode, string QuotCode, string sItemIDs, string CreatedBy)
        {
            return objDALTech.RequisitionItemsOnSplit(VesselCode, ReqCode, DocCode, QuotCode, sItemIDs, CreatedBy);
        }


        public DataSet GetAuditTrailSummary(string Reqcode, string Vesselcode, string Doccode, string QuotCode)
        {
            return objDALTech.GetAuditTrailSummary(Reqcode, Vesselcode, Doccode, QuotCode);
        }

        public DataSet GetsupplierSummary(string strvalue)
        {
            return objDALTech.GetsupplierSummary(strvalue);
        }
        public DataSet GetsupplierSummarybySuppCode(string SuppCode)
        {
            return objDALTech.GetsupplierSummarybySuppCode(SuppCode);
        }

        public int ins_supplierdtl(string SUPPLIER, string xSHIP, double TotalDiscountRate, double VesselDiscountRate, double VATRate,
            double ECTRate, double HETRate, double TDSRate, double RebateAmount, string RebateDesc,
            string GSTOffSet, double FlatDiscountAmt, string DiscountDesc, int Created_By)
        {
            return objDALTech.ins_supplierdtl(SUPPLIER, xSHIP, TotalDiscountRate, VesselDiscountRate, VATRate,
            ECTRate, HETRate, TDSRate, RebateAmount, RebateDesc,
            GSTOffSet, FlatDiscountAmt, DiscountDesc, Created_By);

        }
        public DataSet GetSupplier_HSEQuestion(string SuppCode, int HSEId)
        {
            return objDALTech.GetSupplier_HSEQuestion(SuppCode, HSEId);
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
            return objDALTech.ins_ASL_DTL_Supplier_HSEQ(Supplier, Company_Name, Company_Operations, Company_Address, Company_Country,
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

        public int Get_SendToVessel(string ID, string Reqsn)
        {
            return objDALTech.Get_SendToVessel(ID, Reqsn);
        }

        public void Insert_sendToVessel(string ID, string Reqsn, int Status)
        {
            objDALTech.Insert_sendToVessel(ID, Reqsn, Status);
        }

        public int PURC_Update_SentToSupdt(int ID, int Officeid, string Reqsn, int Sentby, string Remark, DataTable dtQUotationList)
        {
            return objDALTech.PURC_Update_SentToSupdt_DL(ID, Officeid, Reqsn, Sentby,Remark, dtQUotationList);
        }


        public DataTable Get_ReqsnType()
        {
            return objDALTech.Get_ReqsnType_DL();
        }

        public DataTable Get_ReqsnCancelLog(string Reqsn)
        {
            return objDALTech.Get_ReqsnCancelLog_DL(Reqsn);
        }

        public void Update_ReqsnType(string Reqsn, int ReqsnType, string Remark, int UserID)
        {
            objDALTech.Update_ReqsnType_DL(Reqsn, ReqsnType, Remark, UserID);
        }
        public void Update_AccountType(string Reqsn, string Account_Type, string Remark, int UserID)
        {
            objDALTech.Update_AccountType_DL(Reqsn, Account_Type, Remark, UserID);
        }
        public DataTable Get_CrewList(int VesselCode)
        {
            return objDALTech.Get_CrewList_DL(VesselCode);
        }

        public DataTable Get_VID_VesselDetails(int VesselID)
        {
            return objDALTech.Get_VID_VesselDetails_DL(VesselID);
        }

        public int Update_CancelReqsn(string Reqsn, string DocCode, string Remark, int CancelledBy)
        {
            return objDALTech.Update_CancelReqsn_DL(Reqsn, DocCode, Remark, CancelledBy);
        }
       
      
    }
}