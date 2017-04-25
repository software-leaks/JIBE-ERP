using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace SMS.Data.Infrastructure
{

   public  class DAL_Infra_InspectionReportConfig
    {
       private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataSet INSP_Get_ReportConfig()
        {
            try
            {
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ReportConfig");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet INSP_Get_ReportConfigByKeyNo(int KeyNo)
        {
            try

            {

                System.Data.SqlClient.SqlParameter[] sqlparam = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@KeyNo", KeyNo),
   
                 };
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ReportConfigByKeyNo", sqlparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int INSP_Insert_ReportConfig(string KeyDescription,string KeyValue,int CreatedBy,DateTime DateOfCreation)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] sqlparam = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@KeyDescription", KeyDescription),
                  
                    new System.Data.SqlClient.SqlParameter("@KeyValue",KeyValue), 
                    new System.Data.SqlClient.SqlParameter("@Created_By",CreatedBy), 
                    new System.Data.SqlClient.SqlParameter("@Date_Of_Creation",DateOfCreation),
               
                    
                 };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Insert_ReportConfig", sqlparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int INSP_Update_ReportConfig(int KeyNo,string KeyDescription, string KeyValue, int ModifiedBy, DateTime DateOfModification)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] sqlparam = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@KeyNo", KeyNo),
                    new System.Data.SqlClient.SqlParameter("@KeyDescription", KeyDescription),
                    
                    new System.Data.SqlClient.SqlParameter("@KeyValue",KeyValue), 
                    new System.Data.SqlClient.SqlParameter("@Modified_By",ModifiedBy), 
                    new System.Data.SqlClient.SqlParameter("@Date_Of_Modification",DateOfModification),
               
                    
                 };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Update_ReportConfig", sqlparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int INSP_DeleteRestore_ReportConfig(int KeyNo, int ActiveStatus, int ModifiedBy, DateTime DateOfModification)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] sqlparam = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@KeyNo", KeyNo),
                    new System.Data.SqlClient.SqlParameter("@ActiveStatus", ActiveStatus),
                    
                    new System.Data.SqlClient.SqlParameter("@ModifiedBy",ModifiedBy), 
                    new System.Data.SqlClient.SqlParameter("@Date_Of_Modification",DateOfModification),
               
                    
                 };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_DeleteRestore_ReportConfig", sqlparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int INSP_Update_InspectionReportConfigStaus(int KeyNo, int ActiveStatus,int ModifiedBy, DateTime DateOfModification)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] sqlparam = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@KeyNo", KeyNo),
                    new System.Data.SqlClient.SqlParameter("@Active_Status", ActiveStatus),
                    new System.Data.SqlClient.SqlParameter("@Modified_By",ModifiedBy), 
                    new System.Data.SqlClient.SqlParameter("@Date_Of_Modification",DateOfModification),
               
                    
                 };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Update_InspectionReportConfigStaus", sqlparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
