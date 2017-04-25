using System;
using System.Collections.Generic; 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{
   public class DAL_Infra_InspectionType
    {
         IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
         
        //private string connection = "";
        private static string connection = "";
        public DAL_Infra_InspectionType(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_InspectionType()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }
    
        //public DataTable Get_Module_FileUpload_DL(string InspectionTypeName)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@InspectionTypeName", InspectionTypeName) };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FileUploadSize", sqlprm).Tables[0];
           
        //    //SqlParameter[] sqlprm = new SqlParameter[]
        //    //                            {  new SqlParameter("@InspectionTypeName",InspectionTypeName),
                                        
        //    //                             };
        //    //return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_FileUploadSize", sqlprm);
        //}
        public DataTable SearchInspectionType(string searchtext
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_InspectionType_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public int InsertInspectionType_DL(string InspectionTypeName, int CreatedBy,string color)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  new SqlParameter("@InspectionTypeName",InspectionTypeName),
                                            new SqlParameter("@Color",color),
                                           new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_InspectionType", sqlprm);
        }
        public int EditInspectionType_DL(int InspectionTypeId, string InspectionTypeName, int UserId,string color)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@InspectionTypeId",InspectionTypeId),
                                          new SqlParameter("@InspectionTypeName",InspectionTypeName),
                                            new SqlParameter("@Color",color),
                                          new SqlParameter("@UserId",UserId),

                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_InspectionType", sqlprm);
        }

        public DataTable Get_InspectionTypeList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_InspectionType").Tables[0];
        }
        public int DeleteInspectionType_DL(int InspectionTypeId, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@InspectionTypeId",InspectionTypeId),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_InspectionType", sqlprm);
        }
        public  DataTable Check_InspectionType(string InspectionTypeName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@InspectionTypeName", InspectionTypeName) ,   
                 
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Infra_InspectionType.connection, CommandType.StoredProcedure, "SP_INF_Check_InspectionType", obj);
            return ds.Tables[0];
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_check_FAQ_List", obj);
         
        }
    }
}
