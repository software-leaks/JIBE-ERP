using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_CompanyType
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_CompanyType(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_CompanyType()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable SearchCompanyType(string searchtext 
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_CompanyTypeList_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_CompanyTypeListByID_DL(int? CompanyTypeID)
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyTypeList_ByID").Tables[0];
        }

        public int EditCompanyType_DL(int CompanyTypeID, string CompanyType,string CompanyTypeDesc , int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@CompanyTypeID",CompanyTypeID),
                                           new SqlParameter("@Company_Type",CompanyType),
                                           new SqlParameter("@Company_Type_Desc",CompanyTypeDesc),
                                           new SqlParameter("@ModifiedBy",ModifiedBy),

                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_CompanyType", sqlprm);
        }



        public int InsertCompanyType_DL(string CompanyType, string CompanyTypeDesc, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  
                                            new SqlParameter("@Company_Type",CompanyType),
                                            new SqlParameter("@Company_Type_Desc",CompanyTypeDesc),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_CompanyType", sqlprm);
        }

        public int DeleteCompanyType_DL(int CompanyTypeID, int DeletedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CompanyTypeID",CompanyTypeID),
                                          new SqlParameter("@DeletedBy",DeletedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_CompanyType", sqlprm);
        }
 
    }

}
