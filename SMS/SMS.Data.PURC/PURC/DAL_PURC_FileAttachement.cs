using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALFileAttachement
/// </summary>
/// 
namespace SMS.Data.PURC
{
    public class DAL_PURC_FileAttachement
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_FileAttachement()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public DataTable GetAttachedFileInfo(string VesselCode)
        {
            try
            {
                DataTable dtFileInfo = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode)
             // ,new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
             //new System.Data.SqlClient.SqlParameter("@FileType" ,FileType)    
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Attached_file_info_Search", obj);
                dtFileInfo = ds.Tables[0];
                return dtFileInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        public DataTable GetAttachedFileInfo(int? fleetcode, int? ddlvessel, string search, string category, int? supplier,
              string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                   new System.Data.SqlClient.SqlParameter("@Fleet", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@Vessel",ddlvessel),
                   new System.Data.SqlClient.SqlParameter("@SerchText", search),
                   new System.Data.SqlClient.SqlParameter("@category", category),
                   new System.Data.SqlClient.SqlParameter("@supplier", supplier),
                
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Attached_file_info_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public int SaveAttachedFileInfo(string VesselCode, string ReqCode, string suppCode, string FileType, string FileName, string FilePath, string CreatedBy, int Port)
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




        public int SaveAttachedFileInfo_New(string VesselCode, string DocCode, string suppCode, string FileType, string FileName, string FilePath, string CreatedBy, int Port)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode), 
             new System.Data.SqlClient.SqlParameter("@DocCode",DocCode),
             new System.Data.SqlClient.SqlParameter("@SuppCode", suppCode), 
             new System.Data.SqlClient.SqlParameter("@FileType",FileType),
             new System.Data.SqlClient.SqlParameter("@FileName", FileName), 
             new System.Data.SqlClient.SqlParameter("@FilePath",FilePath) ,
             new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
             new SqlParameter("@return",SqlDbType.Int),
             new SqlParameter("@PortID",Port)
           
             };

                obj[7].Direction = ParameterDirection.ReturnValue;
                int RetVal = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_FILE_ATTACHMENT", obj);
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

        public DataTable Purc_Get_Reqsn_Attachments_DL(string Reqsnno,int vesselID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@ReqsnNo", Reqsnno), 
             new System.Data.SqlClient.SqlParameter("@Vessel",vesselID)
            
           
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Reqsn_Attachments", obj).Tables[0];

        }
        public DataTable Purc_Get_Reqsn_Attachments_DL_New( string DocCode, int vesselID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@DocCode", DocCode), 
             new System.Data.SqlClient.SqlParameter("@Vessel",vesselID)
            
           
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSN_ATTACHMENTS_NEW", obj).Tables[0];

        }

        public int Purc_Delete_Reqsn_Attachments_DL(int ID, int Office_ID, int Vessel_ID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
                new SqlParameter("@ID", ID),
                new SqlParameter("@Office_ID", Office_ID),
                new SqlParameter("@Vessel_ID", Vessel_ID)
          
             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_DELETE_Reqsn_Attachment", obj);

        }
        public DataTable Purc_Get_Reqsn_Attachments_Supplier_DL(string Reqsnno, int vesselID,string SuppCode)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@ReqsnNo", Reqsnno), 
             new System.Data.SqlClient.SqlParameter("@Vessel",vesselID),
             new System.Data.SqlClient.SqlParameter("@SuppCode",SuppCode)
            
           
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSN_ATTACHMENTS_SUPPLIER", obj).Tables[0];

        }

    }
}