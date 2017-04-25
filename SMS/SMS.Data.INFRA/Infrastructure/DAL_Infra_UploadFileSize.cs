using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{

  public   class DAL_Infra_UploadFileSize
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_UploadFileSize(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_UploadFileSize()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }

        public DataTable Get_Module_FileUpload_DL(string Attach_Prefix)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@Attach_Prefix", Attach_Prefix) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FileUploadSize", sqlprm).Tables[0];
           
            //SqlParameter[] sqlprm = new SqlParameter[]
            //                            {  new SqlParameter("@Attach_Prefix",Attach_Prefix),
                                        
            //                             };
            //return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_FileUploadSize", sqlprm);
        }

        public DataTable SearchConfigureFileSize(string searchtext
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                
               
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Configurefilesize_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public int InsertConfigureFileSize_DL(string AttachPrefix, int UploadSize, int CreatedBy, int VesselSyncable)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  new SqlParameter("@Attach_Prefix",AttachPrefix),
                                           new SqlParameter("@Size_KB",UploadSize),
                                           new SqlParameter("@CreatedBy",CreatedBy),
                                           new SqlParameter("@VesselSyncable",VesselSyncable)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Configurefilesize", sqlprm);
        }
        public int EditConfigureFileSize_DL (int Rule_ID, string AttachPrefix, int UploadSize, int CreatedBy,int VesselSyncable)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Rule_ID",Rule_ID),
                                          new SqlParameter("@Attach_Prefix",AttachPrefix),
                                           new SqlParameter("@Size_KB",UploadSize),
                                          new SqlParameter("@CreatedBy",CreatedBy),
                                          new SqlParameter("@VesselSyncable",VesselSyncable)

                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Configurefilesize", sqlprm);
        }

        public DataTable Get_ConfigurefilesizeList_DL(int? Rule_ID)
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Configurefilesize").Tables[0];
        }
        public int DeleteConfigureFileSize_DL(int Rule_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Rule_ID",Rule_ID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_Configurefilesize", sqlprm);
        }
    }
}
