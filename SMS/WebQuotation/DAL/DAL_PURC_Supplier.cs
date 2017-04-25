using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;


/// <summary>
/// Summary description for DALVessels
/// </summary>

namespace SMS.Data.PURC
{
    public class DAL_PURC_Supplier
    {

        private static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Supplier()
        {
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
                //return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Insert_RequisitionDeliveryStages", obj);
                obj[14].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Insert_RequisitionDeliveryStages", obj);
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionDeliveryStages", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int InsertRequisitionStageStatus(string sRequisitionCode, string sVesselCode, string sDocumentCode,
                                               string ReqStatus, string ReqComments, int iCreated_By)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
           {
               new SqlParameter("@VesselCode",sVesselCode),
               new SqlParameter("@ReqCode",sRequisitionCode),
               new SqlParameter("@DocmentCode",sDocumentCode),
               new SqlParameter("@ReqStatus",ReqStatus),
               new SqlParameter("@ReqComments",ReqComments),
               new SqlParameter("@LogInUser",iCreated_By),
           };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_INS_REQUISITION_STAGE_STATUS", obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable SelectRequisitionDeliveryStatus(string VesselCode)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)
             };


                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_WQTN_Get_PendingDeliveryUpdate", obj);
                dtFleet = ds.Tables[0];
                return dtFleet;
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
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RequisitionDeliveryStagesLog", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update_Progress(string QtnNumber)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@Qtnnumber",QtnNumber)

             };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_WQTN_UPDATESENTRFQSTATUS", obj);

            }
            catch (Exception ex)
            {

            }

        }

        public static DataTable GET_SystemParameters_DL(int Code)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@Code",Code)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_SystemParameters", prm).Tables[0];
        }


        public static DataTable GET_ItemType_DL(string QuotationCode, string ItemRefCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@QuotationCode",QuotationCode),
               new SqlParameter("@ItemRefCode",ItemRefCode)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Wqtn_GET_ItemType", prm).Tables[0];
        }

        public static int INSERT_ItemType_DL(string QuotationCode, string ItemRefCode, int id, decimal UnitPrice, int ItemType)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("QuotationCode",QuotationCode),
               new SqlParameter("ItemRefCode",ItemRefCode),
               new SqlParameter("UnitPrice",UnitPrice),
               new SqlParameter("ItemType",ItemType),
               new SqlParameter("ID",id)
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Wqtn_INSERT_ItemType", prm);


        }

        public DataTable getDeliveryPort()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_DeliveryPort");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static DataTable GetSupplierUserDetails(string SuppCode, string User_type)
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

        public static DataSet Get_SentTo_Delivery_Status_DL(string SuppCode)
        {
            string query = "select ORDER_CODE from PURC_DTL_REQSNDELIVERYSTAGES where SentTo_UserID='" + SuppCode + "'";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, query);
        }

        public static int SaveAttachedFileInfo(string VesselCode, string ReqCode, string suppCode, string FileType, string FileName, string FilePath, string CreatedBy, int Port)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode), 
             new System.Data.SqlClient.SqlParameter("@ReqsCode",ReqCode),
             new System.Data.SqlClient.SqlParameter("@SuppCode", suppCode), 
             new System.Data.SqlClient.SqlParameter("@FileType",FileType),
             new System.Data.SqlClient.SqlParameter("@FileName", FileName), 
             new System.Data.SqlClient.SqlParameter("@FilePath",FilePath) ,
             new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
             new SqlParameter("@return",SqlDbType.Int),
             new SqlParameter("@PortID",Port)
           
             };

                obj[7].Direction = ParameterDirection.ReturnValue;
                int RetVal = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_File_Attachment_info", obj);
                if (RetVal == 1)
                {
                    RetVal = int.Parse(obj[7].Value.ToString());
                }
                return RetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static DataTable Get_VID_VesselDetails_DL(int VesselID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_INF_Get_VID_VesselDetails", new SqlParameter("@VesselID", VesselID)).Tables[0];
//            string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
//            SqlConnection objCon = new SqlConnection(strConn);

//            string SQL = @" select  (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000005' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as VesselExNames ,
//                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000035' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Hull_No ,
//                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000056' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Type ,
//                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000027' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Yard ,
//                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000034' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Delvry_Date ,
//                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000003' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_IMO_No,
//                            (select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000012' and SMSLOG_VESSEL_ID=" + VesselID.ToString() + @") as Vessel_Owner,
//                            (select Vessel_Name from LIB_VESSELS where  Vessel_ID=" + VesselID.ToString() + ") as Vessel_Name";
//            objCon.Open();
//            DataSet dsVsl = new DataSet();

//            SqlCommand objCom = new SqlCommand(SQL, objCon);
//            SqlDataAdapter sa = new SqlDataAdapter(objCom);
//            sa.Fill(dsVsl);
//            objCon.Close();
//            return dsVsl.Tables[0];

        }


        public static int GET_UPD_SUPPLIER_PASSWORD_DL(string userid)
        {

            SqlParameter[] prm = new SqlParameter[] { 
                new SqlParameter("@USERID", userid) ,
                new SqlParameter("@return",SqlDbType.Int)
            };

            prm[1].Direction = ParameterDirection.ReturnValue;

             SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_GET_UPD_SUPPLIER_PASSWORD", prm);

             return Convert.ToInt32(prm[1].Value.ToString());


        }


        public static int Get_NewContractRequest_DL(string Supplier_Code)
        {
            return Convert.ToInt32( SqlHelper.ExecuteScalar(_internalConnection,CommandType.StoredProcedure,"PURC_WQtn_GET_NewContrctRequest",new SqlParameter("supplier_code",Supplier_Code)));
        }

        public static DataTable Get_Contract_Status_DL(string QtnCode)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CONTRACT_STATUS", new SqlParameter("@QUOTATION_CODE", QtnCode)).Tables[0];
        }
    }
}