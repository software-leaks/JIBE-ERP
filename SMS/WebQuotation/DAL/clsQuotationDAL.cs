using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Data.SqlClient;

using SMS.Data;


/// <summary>
/// Summary description for clsQuotationDAL
/// </summary>
/// 

namespace DALQuotation
{
    public class clsQuotationDAL
    {
        public clsQuotationDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public bool IsValidSupplier(string UserId, string strPassword)
        {

            DataTable dt = new DataTable();
            string Query = "select count(*) from PURC_Lib_Quotation_User where UserID = '" + UserId + "'  and Password ='" + strPassword + "'";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];

            if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0)
                return true;
            else
                return false;
        }


        public DataTable GetSupplierInfo(string UserId, string strPassword)
        {
            DataTable dt = new DataTable();
            string Query = "select * from PURC_Lib_Quotation_User where UserID = '" + UserId + "'";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];
            return dt;

        }

        public DataTable GetSupplierName(string SupplierID)
        {
            DataTable dt = new DataTable();
            string Query = "select * from Lib_Suppliers where SUPPLIER = '" + SupplierID + "'";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];
            return dt;

            //   if (dt.Rows.Count > 0)
            //       return dt.Rows[0][0].ToString();
            //   else
            //       return "";
            //}
        }




        public DataTable GetVessel()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT row_Number() over(order by Vessel_Code)  as row_number, vessel_ID as [Vessel_Code],Vessel_Name as Vessels, [Vessel_Short_Name] ,Tech_Manager  FROM Lib_Vessels where [Active_Status]=1 and Vessel_Manager=1 and ISVESSEL=1 order by Vessel_Name";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];
            return dt;
        }

        public DataTable GetWebQuotation(string strSupplierCode, string strVesselCode, string strStatus, DateTime FromDT, DateTime ToDT, string Req_code, int? Page_Index, int? Page_Size, ref int IS_FETCH_COUNT)
        {
            DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
              
                new SqlParameter("@Supplier_Code",strSupplierCode),
                 new SqlParameter("@VesselCode",strVesselCode),
               new SqlParameter("@Status",strStatus), 
               new SqlParameter("@FromDate",FromDT),
               new SqlParameter("@Todate",ToDT) , 
               new SqlParameter("@Requisition_Code",Req_code) ,

               new SqlParameter("@PAGE_INDEX",Page_Index) ,
               new SqlParameter("@PAGE_SIZE",Page_Size) ,
               new SqlParameter("@IS_FETCH_COUNT",IS_FETCH_COUNT) 
       

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_Get_Quatations", sqlprm);
            dt = ds.Tables[0];
            IS_FETCH_COUNT = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;

        }

    

        public DataTable GetCurrency()
        {
            DataTable dt = new DataTable();
            string Query = "select Currency_Code,Currency_ID,Currency_Discription, Currency_Discription ,Country from Lib_Currency where Active_Status =1 order by Currency_Code";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];
            return dt;

        }

        public DataTable GetRequisition_info(string strReqcode, string strDocCode)
        {
            DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ReqCode",strReqcode),
                new SqlParameter("@Document_code",strDocCode),  
            };

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_sp_Get_Requisition_Info", sqlprm);
            dt = ds.Tables[0];
            return dt;


        }

        public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string QtnCode)
        {

            DataSet dsRFQ = new DataSet();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new SqlParameter("@SupplierCode",SuppCode),
               new SqlParameter("@ReqCode",ReqCode), 
               new SqlParameter("@VesselCode",VesselCode),
               new SqlParameter("@Document_code",DocumentCode),
               new SqlParameter("@QtnCode", QtnCode)
             };
            dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_Get_RFQ_Items", obj);
            return dsRFQ;

        }


        public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode)
        {
            try
            {
                DataSet dsRFQ = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode)   
             };
                dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Generate_RFQ", obj);
                return dsRFQ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string GetQuotStatus(string ReqCode, string DocumentCode, string strQuoSupp, string QTNCode)
        {
            DataTable dt = new DataTable();
            string Query = "select distinct Quotation_Status from PURC_Dtl_Reqsn where REQUISITION_CODE='" + ReqCode + "' and DOCUMENT_CODE='" + DocumentCode + "' and QUOTATION_SUPPLIER = '" + strQuoSupp.Trim() + "' and Line_type='Q' and QUOTATION_Code= '" + QTNCode + "'";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";

        }

        public DataTable SearchQuotation(string SupplierCode, string VesselCode, string Status, string FrmDate, string ToDate, string Req_Code)
        {

            DataTable dt;
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { new SqlParameter("@SupplierCode",SupplierCode) ,  
               new SqlParameter("@VesselCode",VesselCode),
               new SqlParameter("@Status",Status), 
               new SqlParameter("@FromDate",FrmDate),
               new SqlParameter("@Todate",ToDate) , 
               new SqlParameter("@Requisition_Code",Req_Code)  
             };
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_Search_Quotation", obj);
            dt = ds.Tables[0];
            return dt;
        }


        public DataTable PODetails(string RFQCODE, string QTCODE, string SUPLCODE, string DOCCODE, string VESSELCODE)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@RFQCODE",RFQCODE),
               new System.Data.SqlClient.SqlParameter("@QTCODE",QTCODE), 
               new System.Data.SqlClient.SqlParameter("@SUPLCODE",SUPLCODE),
               new System.Data.SqlClient.SqlParameter("@DOCCODE",DOCCODE),
               new System.Data.SqlClient.SqlParameter("@VESSELCODE",VESSELCODE)
             };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POReport", obj);
                return dsPO.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetInfoToSendEmailToESM(string ReqCode, string DocumentCode, string SupplierCode, string VesselCode)
        {
            try
            {
                DataSet dsEmailInfo = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode), 
               new System.Data.SqlClient.SqlParameter("@Supplier_Code",SupplierCode),
               new System.Data.SqlClient.SqlParameter("@Vessel_Code",VesselCode), 
               
             };
                dsEmailInfo = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_Get_Info_Send_Email_ESM", obj);
                return dsEmailInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int UpdatePOConfirm(string ReqCode, string VesselCode, string DocumentCode, string QuotCode, string SuppCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                 { 
                   new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
                   new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
                   new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode),
                   new System.Data.SqlClient.SqlParameter("@QuotCode",QuotCode),
                   new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode)
                 };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_Upd_PO_Confirm", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDeptCode(string DeptCode)
        {
            DataTable dt = new DataTable();
            string Query = "select dept_name as Name_Dept,dept_short_code as Code, Form_type from PURC_Lib_Departments where dept_short_code = '" + DeptCode + "'";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, Query);
            dt = ds.Tables[0];
            return dt;

        }

        public DataTable GetRebateType()
        {
            DataTable dtRebate = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "PURC_WQuot_Sp_Get_RebateType");
            dtRebate = ds.Tables[0];
            return dtRebate;
        }

        public int ExecuteQuery(string SqlQuery)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@StringQuery",SqlQuery)                                          
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "ExeQuery", sqlprm);

        }

        public int ExecuteQuery_String(string SqlQuery)
        {


            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, SqlQuery);

        }






        public DataTable GetTable(string SqlQuery)
        {
            DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@StringQuery",SqlQuery)                                          
            };

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "ExeQuery", sqlprm);
            dt = ds.Tables[0];
            return dt;

        }

        /// <summary>
        /// This method is to get the RFQ attachment for particular requisition with respec to particular supplier
        /// </summary>
        /// <param name="strReqCode"></param>
        /// <param name="strSupplierCode"></param>
        /// <param name="iVesselCode"></param>
        /// <returns></returns>
        public DataSet GetRFQAttachment(string strReqCode, string strSupplierCode, Int32 iVesselCode)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@ReqCode", strReqCode),
                    new SqlParameter("@SupplierCode", strSupplierCode),
                    new SqlParameter("@VesselCode", iVesselCode)
                };
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_GetRFQAttachment", sqlprm);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet UpdateSupplRemarks(string ReqCode, string QUTCODE, string DocCode, string VslCode, string ItemRefCode, string SuppCode, string Remarks)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@ReqCode", ReqCode),
                    new SqlParameter("@QUTCODE", QUTCODE),
                    new SqlParameter("@DocCode", DocCode),
                    new SqlParameter("@VslCode", VslCode),
                    new SqlParameter("@ItemRefCode", ItemRefCode),
                    new SqlParameter("@SuppCode", SuppCode),
                    new SqlParameter("@Remarks", Remarks)
                   
                };
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Item_Remarks_Update", sqlprm);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetSupplRemarks(string QUTCODE, string ItemRefCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                { 
                   
                    new SqlParameter("@QUTCODE", QUTCODE),
                 
                    new SqlParameter("@ItemRefCode", ItemRefCode)
                 
                   
                };
          return  SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supplier_Item_Remark", sqlprm).ToString();
        }


        public int UpdChangePassword(string SuppCode, string oldpwd, string newpwd)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new SqlParameter("SuppCode",SuppCode.Trim()),
               new SqlParameter("NewPWD",newpwd.Trim()), 
               new SqlParameter("OldPWD",oldpwd.Trim()),
               new SqlParameter("@return",SqlDbType.Int)   
             };
            obj[3].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_ChangePassword", obj);
            return Convert.ToInt32(obj[3].Value);

        }
        public DataTable GetItemType()
        {
            DataSet Ds = new DataSet();
            Ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select code,Description from dbo.PURC_Lib_System_Parameters where Parent_Type='153'");
            return Ds.Tables[0];
        }
        public DataSet GetMechDetails(string ReqCode)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                 new SqlParameter("@ReqCode",ReqCode)
              };
            DataSet Ds = new DataSet();
            Ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Mech_Details", obj);
            return Ds;
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
        public string GetExchRate(string Curr_Code)
        {
            try
            {
                return SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "select top 1 Exch_Rate from dbo.Acc_Exch_Rate where curr_code='" + Curr_Code + "' order by date DESC").ToString();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataSet GetSupplier_HSEQuestion(string SuppCode)
        {
            SqlParameter[] sqlparam = new SqlParameter[]
          {
              new  SqlParameter("@SupplierCode",SuppCode)
          };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Sp_GetSupplier_HSEQ", sqlparam);
        }


        public DataTable GetLastHSCQuesUpdate(string SuppCode)
        {
            DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@SupplierCode",SuppCode)                                          
            };

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQuot_Sp_GetLast_HSEQ_Update", sqlprm);
            dt = ds.Tables[0];
            return dt;
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

        public DataSet GetSupplierScopebyscode(string SupplCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", SupplCode)
            };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supplier_Scope", obj);
                return ds;
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
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RegScopexml",RegScope),
                   new System.Data.SqlClient.SqlParameter("@UnRegScopexml", UnRegScope),
                    new System.Data.SqlClient.SqlParameter("@CommentStringxml", CommentString)
                    
            };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[ASL_INS_Supplier_Scope]", obj);

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
                System.Data.DataTable dtSupp = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@Document_code",Documentcode) 
               
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Requisition_Info", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
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
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                //System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "select distinct SUPPLIER,SHORT_NAME SUPPLIER_NAME from Lib_Suppliers where SUPPLIER_TYPE='S' and  SUPPLIER is not null order by SHORT_NAME";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Supplier");
                dtSupp = ds.Tables[0];
                return dtSupp;
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
                //System.Data.DataTable dtSupp = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@Country",Country==null?"":Country)
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Supplier_Filter", obj);
                //dtSupp = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public string getQuotation(string QuatationNumber)
        {
            string returnData = "";
            string qry;
            try
            {

                qry = "select isnull( Max(Right(QUOTATION_CODE,7)),0)+1 from dbo.PURC_Dtl_Reqsn where QUOTATION_CODE like '" + QuatationNumber + "%'";
                returnData = SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, qry).ToString();
                return returnData;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public string getDeliveredNumber(string DeliveredNumber)
        {
            string returnData = "";
            string qry;
            try
            {

                qry = "select isnull( Max(Right(DELIVERY_CODE,7)),0)+1 from dboPURC_Dtl_Reqsn where DELIVERY_CODE like '" + DeliveredNumber + "%'";
                returnData = SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, qry).ToString();
                return returnData;

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

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, sqlQuery);

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
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = sqlQuery.ToString();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;

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
        //        DataSet dsRFQ = new DataSet();

        //        System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
        //     { 

        //       new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
        //       new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
        //       new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
        //       new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode)   
        //     };
        //        dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Generate_RFQ", obj);
        //        return dsRFQ;
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
                DataSet dsRFQ = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode)   
             };
                dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Sp_Generate_RFQ_ForNotListedSupplier", obj);
                return dsRFQ;
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
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@RequCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@SuppCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy) ,
               new System.Data.SqlClient.SqlParameter("@QuotDueDate",QuotDueDate) ,
               new System.Data.SqlClient.SqlParameter("@BuyerRemarks",BuyerRemarks) 
               
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_RFQ_quote_price", obj);
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
                DataTable dtSupplier = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Sp_Get_SupplierDetails", obj);
                dtSupplier = ds.Tables[0];
                return dtSupplier;

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
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@LogOnUserID",LogOnUserID) 

             };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_info_send_Email", obj);
                return ds;
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
                DataTable dtEmailInfo = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode)   
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_NotListedSupplier_send_Email", obj);
                dtEmailInfo = ds.Tables[0];
                return dtEmailInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectSuppCity()
        {

            System.Data.DataTable dtSupp = new System.Data.DataTable();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Supplier_City");
            dtSupp = ds.Tables[0];
            return dtSupp;
        }
        public DataTable SelectSuppCountry()
        {
            System.Data.DataTable dtSupp = new System.Data.DataTable();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Supplier_Country");
            dtSupp = ds.Tables[0];
            return dtSupp;
        }

        public DataSet GetAllCountries()
        {
            try
            {
                DataSet ds = new DataSet();
                string query = "SELECT DISTINCT COUNTRY FROM Lib_Suppliers where COUNTRY != ' ' and COUNTRY is not null order by COUNTRY asc";
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, query);
                return ds;
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "SELECT * FROM Gc_Country_State_City WHERE CountryId is null OR CountryId=0 ORDER BY Description");
                return ds;
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
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                DateTime dtDeliveryStageDate = DateTime.Parse(strDeliveryStageDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                DateTime dtEstDevOnBoardDate = DateTime.Parse(strEstDevOnBoardDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                   new System.Data.SqlClient.SqlParameter("@Office_ID",iOffice_ID), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code",iVessel_Code),
                   new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE",strDocument_Code),
                   new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE",strRequisition_Code),
                   new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrder_Code) ,
                   new System.Data.SqlClient.SqlParameter("@DeliveryStage",strDeliveryStage),
                   new System.Data.SqlClient.SqlParameter("@DeliveryStageDate",dtDeliveryStageDate),
                   new System.Data.SqlClient.SqlParameter("@ForwarderName",strForwarderName),
                   new System.Data.SqlClient.SqlParameter("@ForwarderCountry",iForwarderCountry),
                   new System.Data.SqlClient.SqlParameter("@AgentCode",strAgentCode),
                   new System.Data.SqlClient.SqlParameter("@EstDevOnBoardDate",dtEstDevOnBoardDate),
                   new System.Data.SqlClient.SqlParameter("@DeliveryType",strDeliveryType),
                   new System.Data.SqlClient.SqlParameter("@Remark",strRemark),
                   new System.Data.SqlClient.SqlParameter("@Created_By",iCreated_By),
                   new System.Data.SqlClient.SqlParameter("@return",SqlDbType.Int)
             };
                //return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Insert_RequisitionDeliveryStages", obj);
                obj[14].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Insert_RequisitionDeliveryStages", obj);
                return Convert.ToInt32(obj[14].Value);

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
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE",strRequisition_Code),
               new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE",strDocument_Code), 
               new System.Data.SqlClient.SqlParameter("@Vessel_Code",iVessel_Code),
               new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrder_Code)

             };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_RequisitionDeliveryStages", obj);
                return ds;
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
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE",strRequisition_Code),
               new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE",strDocument_Code), 
               new System.Data.SqlClient.SqlParameter("@Vessel_Code",iVessel_Code),
               new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrder_Code)

             };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_RequisitionDeliveryStagesLog", obj);
                return ds;
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
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode)
             };

                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supplier_Having_Attachment", obj);
                return ds;
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
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@REQCODE",ReqCode),
               new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE",DocCode),
               new System.Data.SqlClient.SqlParameter("@VSL_CODE",VslCode)
             };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_CHANGE_STATUS_SUPPLIER", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ViewSupplierDetails(string SuppCode)
        {

            DataTable dtSupp = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SuppCode",SuppCode),
              
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_ViewSupplierDetails", obj);
            dtSupp = ds.Tables[0];
            return ds.Tables[0];
        }

        public DataTable GetSendedRFQSuppliersList(string ReqCode, string VesselCode, string DocumentCode)
        {
            DataTable dtSupp = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
              
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Sended_SupplierList", obj);
            dtSupp = ds.Tables[0];
            return ds.Tables[0];

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

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER",strSUPPLIER), 
                   new System.Data.SqlClient.SqlParameter("@SHORT_NAME",strSHORT_NAME),
                   new System.Data.SqlClient.SqlParameter("@Full_NAME",strFull_NAME),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_CATEGORY",strSUPPLIER_CATEGORY),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_TYPE",strSUPPLIER_TYPE) ,
                   new System.Data.SqlClient.SqlParameter("@BOOKS_CATEGORY",strBOOKS_CATEGORY),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_STATUS",strSUPPLIER_STATUS),
                   new System.Data.SqlClient.SqlParameter("@LEDGER_CARD",strLEDGER_CARD),
                   new System.Data.SqlClient.SqlParameter("@URL",strURL),
                   new System.Data.SqlClient.SqlParameter("@USER_PWD",strUSER_PWD),
                   new System.Data.SqlClient.SqlParameter("@REMARK",strREMARK),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_1",strADDRESS_1),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_2",strADDRESS_2),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_3",strADDRESS_3),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_4",strADDRESS_4),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_5",strADDRESS_5),
                   new System.Data.SqlClient.SqlParameter("@COUNTRY",strCOUNTRY),
                   new System.Data.SqlClient.SqlParameter("@CITY",strCITY),
                   new System.Data.SqlClient.SqlParameter("@POSTAL_CODE",strPOSTAL_CODE),
                   new System.Data.SqlClient.SqlParameter("@PHONE1",strPHONE1),
                   new System.Data.SqlClient.SqlParameter("@PHONE2",strPHONE2),
                   new System.Data.SqlClient.SqlParameter("@TELEX1",strTELEX1),
                   new System.Data.SqlClient.SqlParameter("@TELEX2",strTELEX2),
                   new System.Data.SqlClient.SqlParameter("@FAX1",strFAX1),
                   new System.Data.SqlClient.SqlParameter("@FAX2",strFAX2),
                   new System.Data.SqlClient.SqlParameter("@EMAIL1",strEMAIL1),
                   new System.Data.SqlClient.SqlParameter("@EMAIL2",strEMAIL2),
                   new System.Data.SqlClient.SqlParameter("@BANK_NAME",strBANK_NAME),
                   new System.Data.SqlClient.SqlParameter("@BANK_BRANCH",strBANK_BRANCH),
                   new System.Data.SqlClient.SqlParameter("@BANK_ACCOUNT",strBANK_ACCOUNT),
                   new System.Data.SqlClient.SqlParameter("@BANK_ADDRESS",strBANK_ADDRESS),
                   new System.Data.SqlClient.SqlParameter("@BANK_COUNTRY",strBANK_COUNTRY),
                   new System.Data.SqlClient.SqlParameter("@BANK_CITY",strBANK_CITY),
                   new System.Data.SqlClient.SqlParameter("@CURRENCY",strCURRENCY),

                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_NAME",strBANK_NAME),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_BRANCH",strBANK_BRANCH),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_ACCOUNT",strBANK_ACCOUNT),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_ADDRESS",strBANK_ADDRESS),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_COUNTRY",strBANK_COUNTRY),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_CITY",strBANK_CITY),
                   new System.Data.SqlClient.SqlParameter("@INTER_CURRENCY",strINTER_CURRENCY),
                   new System.Data.SqlClient.SqlParameter("@Created_By",iCreated_By),
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Ins_LibSupplier", obj);
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

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER",strSUPPLIER), 
                   new System.Data.SqlClient.SqlParameter("@SHORT_NAME",strSHORT_NAME),
                   new System.Data.SqlClient.SqlParameter("@Full_NAME",strFull_NAME),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_CATEGORY",strSUPPLIER_CATEGORY),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_TYPE",strSUPPLIER_TYPE) ,
                   new System.Data.SqlClient.SqlParameter("@BOOKS_CATEGORY",strBOOKS_CATEGORY),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_STATUS",strSUPPLIER_STATUS),
                   new System.Data.SqlClient.SqlParameter("@LEDGER_CARD",strLEDGER_CARD),
                   new System.Data.SqlClient.SqlParameter("@URL",strURL),
                   new System.Data.SqlClient.SqlParameter("@USER_PWD",strUSER_PWD),
                   new System.Data.SqlClient.SqlParameter("@REMARK",strREMARK),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_1",strADDRESS_1),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_2",strADDRESS_2),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_3",strADDRESS_3),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_4",strADDRESS_4),
                   new System.Data.SqlClient.SqlParameter("@ADDRESS_5",strADDRESS_5),
                   new System.Data.SqlClient.SqlParameter("@COUNTRY",strCOUNTRY),
                   new System.Data.SqlClient.SqlParameter("@CITY",strCITY),
                   new System.Data.SqlClient.SqlParameter("@POSTAL_CODE",strPOSTAL_CODE),
                   new System.Data.SqlClient.SqlParameter("@PHONE1",strPHONE1),
                   new System.Data.SqlClient.SqlParameter("@PHONE2",strPHONE2),
                   new System.Data.SqlClient.SqlParameter("@TELEX1",strTELEX1),
                   new System.Data.SqlClient.SqlParameter("@TELEX2",strTELEX2),
                   new System.Data.SqlClient.SqlParameter("@FAX1",strFAX1),
                   new System.Data.SqlClient.SqlParameter("@FAX2",strFAX2),
                   new System.Data.SqlClient.SqlParameter("@EMAIL1",strEMAIL1),
                   new System.Data.SqlClient.SqlParameter("@EMAIL2",strEMAIL2),
                   new System.Data.SqlClient.SqlParameter("@BANK_NAME",strBANK_NAME),
                   new System.Data.SqlClient.SqlParameter("@BANK_BRANCH",strBANK_BRANCH),
                   new System.Data.SqlClient.SqlParameter("@BANK_ACCOUNT",strBANK_ACCOUNT),
                   new System.Data.SqlClient.SqlParameter("@BANK_ADDRESS",strBANK_ADDRESS),
                   new System.Data.SqlClient.SqlParameter("@BANK_COUNTRY",strBANK_COUNTRY),
                   new System.Data.SqlClient.SqlParameter("@BANK_CITY",strBANK_CITY),
                   new System.Data.SqlClient.SqlParameter("@CURRENCY",strCURRENCY),

                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_NAME",strINTER_BANK_NAME),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_BRANCH",strINTER_BANK_BRANCH),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_ACCOUNT",strINTER_BANK_ACCOUNT),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_ADDRESS",strINTER_BANK_ADDRESS),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_COUNTRY",strINTER_BANK_COUNTRY),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_CITY",strINTER_BANK_CITY),
                   new System.Data.SqlClient.SqlParameter("@INTER_CURRENCY",strINTER_CURRENCY),
                   new System.Data.SqlClient.SqlParameter("@Modified_By",iModified_By),
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Upd_LibSupplier", obj);
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "SELECT * FROM Gc_Country_State_City WHERE CountryId=" + iCountryId + " AND Description!='Select' AND StateId IS NOT NULL ORDER BY Description");
                return ds;
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_SupplierCategory");
                return ds;
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_SupplierType");
                return ds;
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_SupplierStatus");
                return ds;
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_SupplierScope");
                return ds;
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
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_SupplierBookingCategory");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSupplierBySupplierCode(string strSupplierCode)
        {
            DataSet dtSupp = new DataSet();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SUPPLIER",strSupplierCode)              
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_LibSupplierBySupplierCode", obj);
            dtSupp = ds;
            return dtSupp;

        }

        public DataTable GetSupplierScopeDetails(string strSupplierCode)
        {
            DataTable dtSupp = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SUPPLIER",strSupplierCode)              
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_LibSupplierScopeDetails", obj);
            dtSupp = ds.Tables[0];
            return ds.Tables[0];

        }

        public string Get_LegalTerm_DL(int LegalType)
        {
            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LegalTerms", new SqlParameter("@LegalType", LegalType)).ToString();
        }

        public string getQuotation_Status(string Requisitioncode, string QUOTATION_CODE, string SuppCode, string Document_Code, int Vessel_Code)
        {
            string returnData = "";
            try
            {

                DataTable dtEmailInfo = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 
                   new System.Data.SqlClient.SqlParameter("@Requisitioncode",Requisitioncode), 
                   new System.Data.SqlClient.SqlParameter("@QUOTATION_CODE",QUOTATION_CODE),
                   new System.Data.SqlClient.SqlParameter("@SuppCode",SuppCode),   
                   new System.Data.SqlClient.SqlParameter("@Document_Code",Document_Code), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code",Vessel_Code),
                  
                };
                returnData = SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Get_QuotationStatus", obj).ToString();
                return returnData;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetRequistion_Quotation_Status(string RqsnCode, string QuotationCode)
        {
            DataTable dtReqStatus = new DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter[] Sqlparams = new System.Data.SqlClient.SqlParameter[]
               {
                   new SqlParameter("@REQUSITIONCODE",RqsnCode),
                   new SqlParameter("@QUOTATION_CODE",QuotationCode),
               };
                dtReqStatus = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQUISTION_STAGE_CODE", Sqlparams).Tables[0];
            }
            catch
            {
            }
            return dtReqStatus;
        }

    }
}