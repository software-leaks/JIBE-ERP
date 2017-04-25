using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Lib_ModuleTemp
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
      
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        //public DAL_Infra_Lib_ModuleTemp(string ConnectionString)
        //{
        //    connection = ConnectionString;
        //}

        //public static DAL_Infra_Lib_ModuleTemp()
        //{
        //    connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //}
        /// <summary>
        /// Get the module name from Table INFRA_LIB_ModuleType
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public  static DataTable Get_ModuleList_DL(int ModuleID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ModuleID",ModuleID),
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[SP_Infra_GET_ModuleList_by_ModuleID]", sqlprm).Tables[0];
        }
      
        /// <summary>
        /// Update the Table INFRA_LIB_ModuleType (Module name will be updated)
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="ModuleName"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static int Update_ModuleName_DL(int ModuleID, string ModuleName, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ModuleID",ModuleID),                                                                                       
                                            new SqlParameter("@ModuleName",ModuleName),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Inf_Lib_UpdateModuleName", sqlprm);
        }
        /// <summary>
        /// Update the  Table INFRA_LIB_ModuleType (Active_status will be set as '0' )
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="ModuleName"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static int Delete_Modul_DL(int ModuleID, string ModuleName, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ModuleID",ModuleID),                                                                                       
                                            new SqlParameter("@ModuleName",ModuleName),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Inf_Lib_DeleteModule", sqlprm);
        }     
      
     
        
        /// <summary>
        /// Get Module list from Table 'NFRA_LIB_ModuleType'  (get the all active Module list )
        /// </summary>
        /// <returns></returns>
        public static DataTable Get_ModuleList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFRA_Get_ModuleList").Tables[0];
        }


        /// <summary>
        /// Get the Template list  from Table 'INFRA_LIB_MODULETEMPLATE'(BY Template ID)
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public static DataTable Get_ModuleTemplateList_DL(int TemplateID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TemplateID",TemplateID),
                                          
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFRA_Get_ModuleTemplateList", sqlprm).Tables[0];
        }

  
        /// <summary>
        /// Module Template data will be save in Table 'INFRA_LIB_MODULETEMPLATE'(Based on ModuleID)
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="TemplateName"></param>
        /// <param name="TemplateText"></param>
        /// <param name="Created_By"></param>
        /// <param name="TemplateType"></param>
        /// <returns></returns>
        public static int Save_Module_Template_DL(int ModuleID, string TemplateName, string TemplateText, int Created_By, int TemplateType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{ 
											new SqlParameter("@ModuleID",ModuleID),
											new SqlParameter("@TemplateName",TemplateName),
											new SqlParameter("@TemplateText",TemplateText),
											new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@TemplateType",TemplateType),
											new SqlParameter("return",SqlDbType.Int)
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INFAR_Save_ModuleTemplate", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        /// <summary>
        /// Insert new record in Table 'NFRA_LIB_ModuleType'
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public static int Save_Module_DL(string Module, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Module",Module),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INFRA_Insert_Module", sqlprm);
        }


        /// <summary>
        /// Get Module Template from INFRA_LIB_ModuleTemplate (By ModuleID)
        /// </summary>
        /// <param name="moduletypeID"></param>
        /// <returns></returns>
        public static DataTable Get_ModuleTemplate_DL(int moduletypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{ 
											new SqlParameter("@moduletypeID",moduletypeID)
										};

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INFRA_Get_ModuleTemplate_By_MaduleID", sqlprm).Tables[0];
        }
    }
}
