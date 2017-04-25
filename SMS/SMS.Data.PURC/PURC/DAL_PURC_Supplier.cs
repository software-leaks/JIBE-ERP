using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALVessels
/// </summary>

namespace SMS.Data.PURC
{
    public class DAL_PURC_Supplier
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Supplier()
        {
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

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Requisition_Info", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GeRequisitiontEvaluation(string ReqCode, string Documentcode)
        {

            try
            {
                System.Data.DataTable dtSupp = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@Document_code",Documentcode) 
               
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Get_ReqsnEvaluation", obj);
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
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Supplier");
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
               new System.Data.SqlClient.SqlParameter("@Acc_Class",Country==null?"":Country)
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Supplier_Filter", obj);
                //dtSupp = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataSet SelectSupplier_Filter(string supcode, int? pagenumber, int? pagesize, ref int isfetchcount, string CITY, string SUPPLIER_NAME, string Country)
        {

            try
            {
                //System.Data.DataTable dtSupp = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@Acc_Class",supcode==null?"":supcode),
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),

                    new System.Data.SqlClient.SqlParameter("@CITY",CITY),
                   new System.Data.SqlClient.SqlParameter("@SUPPLIER_NAME",SUPPLIER_NAME),
                     new System.Data.SqlClient.SqlParameter("@Country",Country==null?"":Country),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
             };
                obj[obj.Length - 1].Direction = ParameterDirection.Output;
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Supplier_Filter_RFQ", obj);
                isfetchcount = Convert.ToInt16(obj[obj.Length - 1].Value.ToString());
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

                qry = "select isnull( Max(Right(QUOTATION_CODE,7)),0)+1 from dbo.PURC_DTL_REQSN where QUOTATION_CODE like '" + QuatationNumber + "%'";
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

                qry = "select isnull( Max(Right(DELIVERY_CODE,7)),0)+1 from dbo.PURC_DTL_REQSN where DELIVERY_CODE like '" + DeliveredNumber + "%'";
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

        public string Save_Approved_PO(int user, string DocumentCode, int qnty, string Item_ref_code)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 

                   
               new System.Data.SqlClient.SqlParameter("@User",user), 
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@Qty",qnty),
               new System.Data.SqlClient.SqlParameter("@Item_ref_code",Item_ref_code),
              
               
             };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Update_ApprovePO", obj).ToString();
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

        public DataSet GetDataToGenerateRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string QtnCode)
        {
            try
            {
                DataSet dsRFQ = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode),
               new SqlParameter("@QtnCode",QtnCode)
             };
                dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Generate_RFQ", obj);
                return dsRFQ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet InsertSupplierProperties(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string VatApplicable, string QtnCode, int CreatedBy)
        {
            try
            {
                DataSet dsRFQ = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@VatApplicable",VatApplicable),
                new System.Data.SqlClient.SqlParameter("@created_By",CreatedBy),
               new SqlParameter("@QtnCode",QtnCode)
             };
                dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Supplier_Properties", obj);
                return dsRFQ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
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
                dsRFQ = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Generate_RFQ_ForNotListedSupplier", obj);
                return dsRFQ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string InsertQuotedPriceForRFQ(string SuppCode, string ReqCode, string VesselCode, string DocumentCode, string CreatedBy, string QuotDueDate, string BuyerRemarks, string port, DateTime dldate, string DeliveryInstruction, string items)
        {
            try
            {
                if (QuotDueDate == "Null")
                    QuotDueDate = "";

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 

                   
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@RequCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@SuppCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy) ,
               new System.Data.SqlClient.SqlParameter("@QuotDueDate",QuotDueDate) ,
               new System.Data.SqlClient.SqlParameter("@BuyerRemarks",BuyerRemarks),
               new System.Data.SqlClient.SqlParameter("@DeliveryPort",port),
                new System.Data.SqlClient.SqlParameter("@DeliveryDate",dldate),
                new SqlParameter("@DeliveryInstruction",DeliveryInstruction),
                new SqlParameter("@itemRefCodes",items),
                new SqlParameter("@outputQuotationCode",SqlDbType.VarChar,100)
                
               
             };
                obj[obj.Length - 1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_RFQ_quote_price", obj);
                return obj[obj.Length - 1].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetSupplierUserDetails(string SuppCode, string User_type)
        {
            try
            {
                DataTable dtSupplier = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@User_type",User_type)
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierDetails", obj);
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_info_send_Email", obj);
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
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_NotListedSupplier_send_Email", obj);
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Supplier_City");
            dtSupp = ds.Tables[0];
            return dtSupp;
        }
        public DataTable SelectSuppCountry()
        {
            System.Data.DataTable dtSupp = new System.Data.DataTable();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Supplier_Country");
            dtSupp = ds.Tables[0];
            return dtSupp;
        }

        public DataSet GetAllCountries()
        {
            try
            {
                DataSet ds = new DataSet();
                //string query = "SELECT DISTINCT COUNTRY FROM Lib_Suppliers where COUNTRY != ' ' and COUNTRY is not null order by COUNTRY asc";
                string query = "SELECT DISTINCT rtrim(ltrim(COUNTRY)) as COUNTRY FROM Lib_Suppliers where COUNTRY != ' ' and COUNTRY is not null and COUNTRY <> '0' order by rtrim(ltrim(COUNTRY)) asc";
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "SELECT * FROM LIB_Country  ORDER BY Country_Name");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);


                DateTime dtEstDevOnBoardDate = DateTime.Parse(strEstDevOnBoardDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
      
                   new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrder_Code) ,
                   new System.Data.SqlClient.SqlParameter("@EstDevOnBoardDate",dtEstDevOnBoardDate),
                   new System.Data.SqlClient.SqlParameter("@DeliveryType",strDeliveryType),
                   new System.Data.SqlClient.SqlParameter("@Remark",strRemark),
                   new System.Data.SqlClient.SqlParameter("@Created_By",iCreated_By),
                     new System.Data.SqlClient.SqlParameter("@SentTo_UserType",SentTo_UserType),
                    new System.Data.SqlClient.SqlParameter("@SentTo_UserID",SentTo_UserID),
                    new System.Data.SqlClient.SqlParameter("@Sent_Date",DateTime.Parse(Sent_Date, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault)),
                   new System.Data.SqlClient.SqlParameter("@Planned_SendDate",DateTime.Parse(Planned_SendDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault)),
                  new System.Data.SqlClient.SqlParameter("@Delivery_DateAtDestin",DateTime.Parse(Delivery_DateAtDestin, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault)),
                  new System.Data.SqlClient.SqlParameter("@Packet_Count",Packet_Count),
                   new System.Data.SqlClient.SqlParameter("@Packet_Details",Packet_Details),
                  new System.Data.SqlClient.SqlParameter("@RecvdBy_UserType",RecvdBy_UserType),
                  new System.Data.SqlClient.SqlParameter("@RecvdBy_UserID",RecvdBy_UserID),
                   new System.Data.SqlClient.SqlParameter("@Recvd_Date",DateTime.Parse(Recvd_Date, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault)),
                   new System.Data.SqlClient.SqlParameter("@Recvd_AT",Recvd_AT),
                   new System.Data.SqlClient.SqlParameter("@EstDevOnBoar_Port",EstDevOnBoar_Port),

                    new System.Data.SqlClient.SqlParameter("@SentFrom_UserType",SentFrom_UserType),
                   new System.Data.SqlClient.SqlParameter("@SentFrom_UserID",SentFrom_UserID),

                  
		


                 
             };
                //return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Insert_RequisitionDeliveryStages", obj);

                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Insert_RequisitionDeliveryStages", obj);


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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionDeliveryStages", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet BindRequisitionDeliveryStagesLog(string strOrder_Code)
        {
            try
            {
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
              
               new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrder_Code)

             };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionDeliveryStagesLog", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataSet BindRequisitionDeliveryStagesMovement(string strRequisition_Code, string iVessel_Code, string strOrder_Code)
        {
            try
            {
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE",strRequisition_Code),
               new System.Data.SqlClient.SqlParameter("@Vessel_Code",iVessel_Code),
               new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrder_Code)

             };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionDeliveryStagesMovement", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int InsertRequisitionDeliveryStagesMovement(string strOrderCode, string strCurrentStage, string strStageHolderCode,
                                  string strDelivery_type, DateTime OrderReadiness, string Delivery_Remarks, string strCurrentCity,
                                  string strCurrentPort, DateTime strDeliveryDate, string strDeliveryPort, string strRemarks, string strCreatedby, int intCurrentActiveStage)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 
                    new System.Data.SqlClient.SqlParameter("@ORDER_CODE",strOrderCode),
                    new System.Data.SqlClient.SqlParameter("@CURRENT_STAGE",strCurrentStage),
                    new System.Data.SqlClient.SqlParameter("@STAGE_HOLDER_CODE",strStageHolderCode),
                    new System.Data.SqlClient.SqlParameter("@DELIVERY_TYPE",strDelivery_type),
                    new System.Data.SqlClient.SqlParameter("@ORDER_READINESS",OrderReadiness),
                    new System.Data.SqlClient.SqlParameter("@DELIVERY_REMARK",Delivery_Remarks),
                    new System.Data.SqlClient.SqlParameter("@CURRENT_CITY",strCurrentCity),
                    new System.Data.SqlClient.SqlParameter("@CURRENT_PORT",strCurrentPort),
                    new System.Data.SqlClient.SqlParameter("@DELIVERY_DATE",strDeliveryDate),
                    new System.Data.SqlClient.SqlParameter("@DELIVERY_PORT",strDeliveryPort),
                    new System.Data.SqlClient.SqlParameter("@REMARKS",strRemarks),
                    new System.Data.SqlClient.SqlParameter("@CREATED_BY",strCreatedby),
                    new System.Data.SqlClient.SqlParameter("@CURRENT_ACTIVE_STAGE",intCurrentActiveStage),
 
                };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionDeliveryStagesMovementInsert", obj);
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_ViewSupplierDetails", obj);
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Sended_SupplierList", obj);
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
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_LibSupplier", obj);
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

                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_NAME",strBANK_NAME),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_BRANCH",strBANK_BRANCH),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_ACCOUNT",strBANK_ACCOUNT),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_ADDRESS",strBANK_ADDRESS),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_COUNTRY",strBANK_COUNTRY),
                   new System.Data.SqlClient.SqlParameter("@INTER_BANK_CITY",strBANK_CITY),
                   new System.Data.SqlClient.SqlParameter("@INTER_CURRENCY",strINTER_CURRENCY),
                   new System.Data.SqlClient.SqlParameter("@Modified_By",iModified_By),
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_LibSupplier", obj);
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierCategory");
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierType");
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierStatus");
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierScope");
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierBookingCategory");
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_LibSupplierBySupplierCode", obj);
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_LibSupplierScopeDetails", obj);
            dtSupp = ds.Tables[0];
            return ds.Tables[0];

        }
    }
}