using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using SMS.Data;
/// <summary>
/// Summary description for DALTechnical
/// </summary>
/// 

namespace ClsDALTechnical
{
    public partial class DALTechnical
    {
        public DALTechnical()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public int UpdateDelivery(string ReqCode, string DocCode, string SuppCode, string OrderCode
            , string VesselCode, string CreatedBy, string PayAt, decimal TotalPay, decimal RoundoffAmt,
            string SystemCode, string DeptCode, string ItemIDs, string DelQtys, string ROBQtys, decimal dclSuppOrdDiscount)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@DocumentCode",DocCode),         
               new SqlParameter("@SupplierCode",SuppCode),         
               new SqlParameter("@OrderCode",OrderCode),         
               new SqlParameter("@VesselCode",VesselCode),         
               new SqlParameter("@CreatedBy",CreatedBy) ,        
               new SqlParameter("@PayAt",PayAt),        
               new SqlParameter("@Total_pay",TotalPay),         
               new SqlParameter("@RoundOffAmt",RoundoffAmt),         
               new SqlParameter("@SystemCode",SystemCode),         
               new SqlParameter("@DeptCode",DeptCode),         
               new SqlParameter("@ItemStrings",ItemIDs),     
               new SqlParameter("@QtyString",DelQtys),        
               new SqlParameter("@ROBString",ROBQtys) , 
               new SqlParameter("@SuppOrderDiscount",dclSuppOrdDiscount)   
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_Delivery_Item", sqlprm);

        }


        public DataSet POApprovalHierarchy(string ReqCode, string DocCode, string QuotCode, string SuppCode, string VesselCode, string LogInUser, string CategoryCode)
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@DocumentCode",DocCode),         
               new SqlParameter("@QuotCode",QuotCode),   
               new SqlParameter("@SuppCode",SuppCode),       
               new SqlParameter("@VesselCode",VesselCode), 
               new SqlParameter("@LogInUserID",LogInUser), 
               new SqlParameter("@CategoryCode",CategoryCode), 
               
            };
            return ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_POAmount", sqlprm);
        }
        public DataSet InvoiceApprovalEmil(string ReqCode, string InvoiceNo, string SuppCode, string VesselCode, string LogInUser)
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@INVOICENO",InvoiceNo),         
               new SqlParameter("@SuppCode",SuppCode),       
               new SqlParameter("@VesselCode",VesselCode), 
               new SqlParameter("@LogInUserID",LogInUser), 
             
               
            };
            return ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Invoice_ApproveMail", sqlprm);
        }




        public int InsertUserApprovalEntries(string ReqCode, string DocCode, string VesselCode, string LogInUser, string ApproverID, decimal RqstAmt, decimal ApporveAmt, string Supplier, string Remark, DataTable dtQuotationList)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@DocumentCode",DocCode),  
               new SqlParameter("@VesselCode",VesselCode), 
               new SqlParameter("@LogInUserID",LogInUser), 
               new SqlParameter("@ApproverID",int.Parse(ApproverID)), 
               new SqlParameter("@RqstAmt",RqstAmt), 
             
               new SqlParameter("@supplier",Supplier),
               new SqlParameter("@Remark",Remark),
               new SqlParameter("@QTNCode",dtQuotationList)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_INS_User_Approvals_Entry", sqlprm);

        }


        public int ExecuteQuery(string SqlQuery)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@StringQuery",SqlQuery)                                          
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "ExeQuery", sqlprm);

        }


        public System.Data.DataTable GetTable(string SqlQuery)
        {
            System.Data.DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@StringQuery",SqlQuery)                                          
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "ExeQuery", sqlprm);
            dt = ds.Tables[0];
            return dt;

        }
        public int UpdatePOConfirmation(string ReqCode, string DocCode, string SuppCode, string VesselCode, string OrdCode, string QuotCode)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@SupplierCode",SuppCode),        
               new SqlParameter("@ReqCode",ReqCode), 
               new SqlParameter("@VesselCode",VesselCode),
               new SqlParameter("@Document_code",DocCode),
               new SqlParameter("@OrdCode",OrdCode),
                new SqlParameter("@QuotCode",QuotCode)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_PO_Confirm", sqlprm);

        }
        public int UpdateReqForApprovalPending(string ReqCode, string DocCode, string QuotCode, string SuppCode, string VesselCode, string Approver, string BudgetCode, string Comment)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@DocumentCode",DocCode),         
               new SqlParameter("@QuotCode",QuotCode),         
               new SqlParameter("@SuppCode",SuppCode),         
               new SqlParameter("@VesselCode",VesselCode),         
               new SqlParameter("@Approver",Approver),
               new SqlParameter("@Budget_Code", BudgetCode),
               new SqlParameter("@Comment",Comment)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_ReqForApprovalPending", sqlprm);

        }

        public DataSet GetPMS_Report_POApproval(string vcode, String dcode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("VesselCode",vcode),        
               new SqlParameter("Dpt_code",dcode)         
               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_POApproval", sqlprm);

        }

        public DataSet GetPMS_Report_DeliveryStatus(string vcode, String dcode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("VesselCode",vcode),        
               new SqlParameter("Dpt_code",dcode)         
               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_PendingDeliveryStatus", sqlprm);

        }

        public DataTable GetToopTipsForQtnRecve()
        {
            System.Data.DataTable dt;
            //SqlParameter[] sqlprm = new SqlParameter[]
            //{
            //   new SqlParameter("@QuotCode",QuotCode),        
            //   new SqlParameter("@DocumentCode",DocCode),  
            //   new SqlParameter("@VesselCode",VesselCode), 
            //};

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_TOOLTIPS_QTN_RECVD");
            return dt = ds.Tables[0];
        }
       
        public DataTable GetToopTipsForQtnSent()
        {
            System.Data.DataTable dt;
            //SqlParameter[] sqlprm = new SqlParameter[]
            //{
            //   new SqlParameter("@QuotCode",QuotCode),        
            //   new SqlParameter("@DocumentCode",DocCode),  
            //   new SqlParameter("@VesselCode",VesselCode),  
            //};

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_TOOLTIPS_QTN_SENT");
            return dt = ds.Tables[0];


        }

       

        public DataTable GetToopTipsForQtnINProgress()
        {
            System.Data.DataTable dt;
            //SqlParameter[] sqlprm = new SqlParameter[]
            //{
            //   new SqlParameter("@QuotCode",QuotCode),        
            //   new SqlParameter("@DocumentCode",DocCode),  
            //   new SqlParameter("@VesselCode",VesselCode),  
            //};

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_TOOLTIPS_QTN_IN_PROGRESS");
            return dt = ds.Tables[0];


        }
       

        public DataSet GetRequisitionSummary(string ReqCode, string DocCode, string VesselCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@Document_code",DocCode)    
               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Requisition_Summary", sqlprm);

        }
        public DataSet GetSubCatalogueDetails(string ReqCode, string Catalogue_Code, string VesselCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@System_code",Catalogue_Code)    
               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SubCatalogue_Details", sqlprm);

        }

        public DataSet CancelledGetRequisitionSummary(string ReqCode, string DocCode, string VesselCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@Document_code",DocCode)    
               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_CancelledGet_Requisition_Summary", sqlprm);

        }

        public DataSet GetDeliveryOrderSummary(string ReqCode, string DocCode, string VesselCode, string DeliverCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@Document_code",DocCode), 
               new SqlParameter("@DeliverCode",DeliverCode)    
               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_DeliveryOrder_Summary", sqlprm);

        }

        public DataSet GetReqsnOrderApprovalSummary(string ReqCode, string DocCode, string VesselCode, string OrderNumber, string SuppCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@Document_code",DocCode), 
               new SqlParameter("@OrderCode",OrderNumber)    ,
               new SqlParameter("@SuppCode",SuppCode)    ,

               
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_ReqsnOrderApprovalSummary", sqlprm);

        }


        public DataSet GetRequQuotationSummary(string ReqCode, string DocCode, string VesselCode, string QuotCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@Document_code",DocCode) ,
               new SqlParameter("@QuotCode",QuotCode) 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Quotation_Summary", sqlprm);
        }


        public DataTable GetSuppCodeQuotCode(string VesselCode, string ReqCode, string OrderCode)
        {
            System.Data.DataTable dt;

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",ReqCode),        
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@OrderCode",OrderCode) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SuppQouatation_ForOrder", sqlprm);
            return dt = ds.Tables[0];


        }
        public DataTable GetRequsitionDetails(string VesselCode, string ReqCode, string ReqStatus)
        {
            System.Data.DataTable dt;

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@VesselCode",VesselCode),
               new SqlParameter("@ReqStatus",ReqStatus) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Get_REQ_DETAILS]", sqlprm);
            return dt = ds.Tables[0];
        }


        public int RequisitionItemsOnSplit(string VesselCode, string ReqCode, string DocCode, string QuotCode, string sItemIDs, string CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqCode",ReqCode),        
               new SqlParameter("@VesselCode",VesselCode),         
               new SqlParameter("@Document_code",DocCode),         
               new SqlParameter("@Quotation_Code",QuotCode),         
               new SqlParameter("@Items",sItemIDs),         
               new SqlParameter("@CreatedBy",CreatedBy)  
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_SplitOnItems", sqlprm);
        }

        public DataSet GetAuditTrailSummary(string Reqcode, string Vesselcode, string Doccode, string QuotCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@ReqsCode",Reqcode),        
               new SqlParameter("@Vessel_Code",Vesselcode),
               new SqlParameter("@Document_code",Doccode) ,
               new SqlParameter("@QuotCode",QuotCode) 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_AuditTrail_Summary", sqlprm);
        }
        public DataSet GetsupplierSummary(string strvalue)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
               new SqlParameter("@checkvalue",strvalue)    
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Getsupplier", sqlprm);
        }
        public DataSet GetsupplierSummarybySuppCode(string SuppCode)
        {
            SqlParameter[] sqlparam = new SqlParameter[]
          {
              new  SqlParameter("@SupplierCode",SuppCode)
          };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Sp_GetSupplierbySuppliercode", sqlparam);
        }


        public int ins_supplierdtl(string SUPPLIER, string xSHIP, double TotalDiscountRate, double VesselDiscountRate, double VATRate,
            double ECTRate, double HETRate, double TDSRate, double RebateAmount, string RebateDesc,
            string GSTOffSet, double FlatDiscountAmt, string DiscountDesc, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
               new SqlParameter("@SUPPLIER",SUPPLIER),        
               new SqlParameter("@xSHIP",xSHIP),  
               new SqlParameter("@TotalDiscountRate",TotalDiscountRate), 
               new SqlParameter("@VesselDiscountRate",VesselDiscountRate), 
               new SqlParameter("@VATRate",VATRate), 
               new SqlParameter("@ECTRate",ECTRate), 
               new SqlParameter("@HETRate",HETRate), 
               
               new SqlParameter("@TDSRate",TDSRate), 
               new SqlParameter("@RebateAmount",RebateAmount), 
               new SqlParameter("@RebateDesc",RebateDesc), 
               new SqlParameter("@GSTOffSet",GSTOffSet), 

               new SqlParameter("@FlatDiscountAmt",FlatDiscountAmt), 
               new SqlParameter("@DiscountDesc",DiscountDesc), 
               new SqlParameter("@Created_By",Created_By)
              
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "Sp_Ins_Supplier", sqlprm);

        }


        public DataSet GetSupplier_HSEQuestion(string SuppCode, int HSEId)
        {
            SqlParameter[] sqlparam = new SqlParameter[]
          {
              new  SqlParameter("@SupplierCode",SuppCode),
              new  SqlParameter("@Id",HSEId)
          };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Sp_GetSupplier_HSEQ", sqlparam);
        }


        public int ins_ASL_DTL_Supplier_HSEQ
            (
          string Supplier,
string Company_Name,
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
int Created_By
            )
        {
            SqlParameter[] sqlparam = new SqlParameter[]
          {
new  SqlParameter("@Supplier",Supplier),
 new  SqlParameter("@Company_Name",Company_Name),
 new  SqlParameter("@Company_Operations",Company_Operations),
new  SqlParameter("@Company_Address",Company_Address),
 new  SqlParameter("@Company_Country",Company_Country),
 new  SqlParameter("@Company_Phone",Company_Phone),
 new  SqlParameter("@Company_Fax",Company_Fax),
 new  SqlParameter("@Company_Email",Company_Email),
 new  SqlParameter("@Company_WebSite",Company_WebSite),
 new  SqlParameter("@Company_PIC",Company_PIC),
 new  SqlParameter("@Emergency_Contact_No",Emergency_Contact_No),
 new  SqlParameter("@Size_Turnover",Size_Turnover),
 new  SqlParameter("@Age_Of_Business",Age_Of_Business),
  new  SqlParameter("@Services_Provided",Services_Provided),
 new  SqlParameter("@Core_Business_Activity",Core_Business_Activity),
  new  SqlParameter("@Regulatory_Control",Regulatory_Control),
  new  SqlParameter("@Regulatory_Control_Remarks",Regulatory_Control_Remarks),
 new  SqlParameter("@Customer1_Name",Customer1_Name),
 new  SqlParameter("@Customer2_Name",Customer2_Name),
 new  SqlParameter("@Customer3_Name",Customer3_Name),
 new  SqlParameter("@Customer1_Years",Customer1_Years),
 new  SqlParameter("@Customer2_Years",Customer2_Years),
 new  SqlParameter("@Customer3_Years",Customer3_Years),
 new  SqlParameter("@Service_Description1",Service_Description1),
 new  SqlParameter("@Service_Description2",Service_Description2),
 new  SqlParameter("@Service_Description3",Service_Description3),
 new  SqlParameter("@Hours_Worked_YTD",Hours_Worked_YTD),
 new  SqlParameter("@Hours_Worked_2Yrs",Hours_Worked_2Yrs),
 new  SqlParameter("@Fatal_injuries_YTD",Fatal_injuries_YTD),
 new SqlParameter("@Fatal_Injuries_2Yrs", Fatal_Injuries_2Yrs),
 new  SqlParameter("@LostDay_Injuries_YTD",LostDay_Injuries_YTD),
 new  SqlParameter("@LostDay_Injuries_2Yrs",LostDay_Injuries_2Yrs),
new  SqlParameter("@Incidence_Rate_YTD",Incidence_Rate_YTD),
 new  SqlParameter("@Incidence_Rate_2Yrs",Incidence_Rate_2Yrs),
   new  SqlParameter("@Government_Insp_3Yrs",Government_Insp_3Yrs),
  new  SqlParameter("@Significant_Incidents_3Yrs",Significant_Incidents_3Yrs),
  new  SqlParameter("@Insurance_Indemity",Insurance_Indemity),
  new  SqlParameter("@Insurance_Indemity_Remarks",Insurance_Indemity_Remarks),
 new  SqlParameter("@Quality_Assurance",Quality_Assurance),
  new  SqlParameter("@Quality_Assurance_Remarks",Quality_Assurance_Remarks),
 new  SqlParameter("@Key_Personnel_Qualifications",Key_Personnel_Qualifications),
 new  SqlParameter("@Training_New_Employees",Training_New_Employees),
 new  SqlParameter("@Training_New_Employees_Remarks",Training_New_Employees_Remarks),
 new  SqlParameter("@Training_Exisiting_Employees",Training_Exisiting_Employees),
  new  SqlParameter("@Training_Exisiting_Employees_Remarks",Training_Exisiting_Employees_Remarks),
 new  SqlParameter("@Client_List",Client_List),
 new  SqlParameter("@Incident_Reporting",Incident_Reporting),
 new  SqlParameter("@Incident_Reporting_Remarks",Incident_Reporting_Remarks),
 new  SqlParameter("@Near_Miss_Reporting",Near_Miss_Reporting),
 new  SqlParameter("@Near_Miss_Reporting_Remarks",Near_Miss_Reporting_Remarks),
 new  SqlParameter("@Safety_Equipment",Safety_Equipment),
 new  SqlParameter("@Safety_Equipment_Remarks",Safety_Equipment_Remarks),
 new  SqlParameter("@Safety_Equipment_Type",Safety_Equipment_Type),
 new  SqlParameter("@Employee_Equip_Training",Employee_Equip_Training),
  new  SqlParameter("@Employee_Equip_Training_Remarks",Employee_Equip_Training_Remarks),
 new  SqlParameter("@Contractor_Equip_Training_Remarks",Contractor_Equip_Training_Remarks),
 new  SqlParameter("@Contractor_Equip_Training",Contractor_Equip_Training),
 new  SqlParameter("@Clean_Working_Environment",Clean_Working_Environment),
 new  SqlParameter("@Clean_Working_Environment_Remarks",Clean_Working_Environment_Remarks),
  new  SqlParameter("@Equipment_Calibration",Equipment_Calibration),
   new  SqlParameter("@Calibration_Certificates",Calibration_Certificates),
  new  SqlParameter("@Equipment_Calibration_Remarks",Equipment_Calibration_Remarks),
   new  SqlParameter("@Calibration_Certificates_Remarks",Calibration_Certificates_Remarks),
  new  SqlParameter("@Client_Familiarization_visits_Remarks",Client_Familiarization_visits_Remarks),
 new  SqlParameter("@Client_Familiarization_visits",Client_Familiarization_visits),
 new  SqlParameter("@Meet_Standard_Requirements",Meet_Standard_Requirements),
 new  SqlParameter("@Meet_Standard_Requirements_Remarks",Meet_Standard_Requirements_Remarks),
  new  SqlParameter("@HSEQ_Submitted_Date",HSEQ_Submitted_Date),
  new  SqlParameter("@HSEQ_Submitted_By",HSEQ_Submitted_By),
  new  SqlParameter("@ESM_Competitive_Quote",ESM_Competitive_Quote),
 new  SqlParameter("@ESM_Competitive_Quote_Remarks",ESM_Competitive_Quote_Remarks),
 new  SqlParameter("@ESM_Quick_Response",ESM_Quick_Response),
 new  SqlParameter("@ESM_Quick_Response_Remarks",ESM_Quick_Response_Remarks),
 new  SqlParameter("@ESM_Ontime_Delivery",ESM_Ontime_Delivery),
  new  SqlParameter("@ESM_Ontime_Delivery_Remarks",ESM_Ontime_Delivery_Remarks),
  new  SqlParameter("@ESM_Prompt_Advice",ESM_Prompt_Advice),
  new  SqlParameter("@ESM_Prompt_Advice_Remarks",ESM_Prompt_Advice_Remarks),
 new  SqlParameter("@Created_By",Created_By)

          };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "Sp_ins_GetSupplier_HSEQ", sqlparam);
        }


        public int Get_SendToVessel(string ID, string Reqsn)
        {
            SqlParameter[] prm = new SqlParameter[]{
            new SqlParameter("@id",ID),
            new SqlParameter("@REQSNcode",Reqsn)
        };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Get_SENDTOVESSEL", prm));
        }
        public void Insert_sendToVessel(string ID, string Reqsn, int Status)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@STATUS",Status),
                new SqlParameter("@REQSNcode",Reqsn),
                new SqlParameter("@ID",ID)
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_SENDTOVESSEL", prm);
        }


        public int PURC_Update_SentToSupdt_DL(int ID, int Officeid, string Reqsn, int Sentby, string Remark, DataTable dtQUotationList)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@id",ID),
                new SqlParameter("@officeid",Officeid),
                new SqlParameter("@Reqsn",Reqsn),
                new SqlParameter("@senttosupdtby",Sentby),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@QTNCode",dtQUotationList)
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Insert_SentToSupdt", prm);
        }

        public DataTable Get_ReqsnType_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PRUC_GET_ReqsnType").Tables[0];
        }

        public DataTable Get_ReqsnCancelLog_DL(string Reqsn)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@ReqsnNo",Reqsn)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_ReqsnCancelLog", prm).Tables[0];
        }

        public void Update_ReqsnType_DL(string Reqsn, int ReqsnType,string Remark,int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@ReqsnNo",Reqsn),
                new SqlParameter("@ReqsnType",ReqsnType),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@UserID",UserID)
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_ReqsnType", prm);
        }
        public void Update_AccountType_DL(string Reqsn, string Account_Type, string Remark, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@ReqsnNo",Reqsn),
                new SqlParameter("@Account_Type",Account_Type),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@UserID",UserID)
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_AccountType", prm);
        }
        public DataTable Get_CrewList_DL(int VesselCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@VesselCode",VesselCode)
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_CrewList", prm).Tables[0];
        }

        public DataTable Get_VID_VesselDetails_DL(int VesselID)
        {

            string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            SqlConnection objCon = new SqlConnection(strConn);

            string SQL = @" select  (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000005' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as VesselExNames ,
                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000035' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Hull_No ,
                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000056' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Type ,
                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000027' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Yard ,
                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000034' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Delvry_Date ,
                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000003' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_IMO_No,
                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000012' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + ") as Vessel_Owner ";
            objCon.Open();
            DataSet dsVsl = new DataSet();

            SqlCommand objCom = new SqlCommand(SQL, objCon);
            SqlDataAdapter sa = new SqlDataAdapter(objCom);
            sa.Fill(dsVsl);
            objCon.Close();
            return dsVsl.Tables[0];

        }

        public int Update_CancelReqsn_DL(string Reqsn, string DocCode, string Remark, int CancelledBy)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@Reqsn",Reqsn),
                new SqlParameter("@DocumentCode",DocCode),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@CancelledBy",CancelledBy)
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_CancelReqsn", prm);
        }
        
    }
}