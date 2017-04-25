using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.ASL
{
    public class DAL_SysVariable
    {
          IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_SysVariable(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_SysVariable()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_SysVariableList(int VariableID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@VariableID", VariableID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_SysVariable_List", sqlprm).Tables[0];
        }



        public DataTable Get_SysVariable_Search(string searchtext, string VariableType
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@VariableType", VariableType),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_SysVariable_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_SysVariable(string searchtext)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@searchtext", searchtext)
                  
                    
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_SysVariable", obj).Tables[0];


        }

        public int Insert_SysVariable(string type, string Name, string Code, string Value, string ColorCode, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@Type",type),
                                          new SqlParameter("@Name",Name),
                                          new SqlParameter("@Code",Code),
                                          new SqlParameter("@Value",Value),
                                          new SqlParameter("@ColorCode",ColorCode),
                                          new SqlParameter("@UserID",UserID),
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_INS_SysVariable", sqlprm));
        }


        public int Edit_SysVariable(int? VariableID, string type, string Name, string Code, string Value, string ColorCode, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@VariableID",VariableID),
                                          new SqlParameter("@Type",type),
                                          new SqlParameter("@Name",Name),
                                          new SqlParameter("@Code",Code),
                                            new SqlParameter("@Value",Value),
                                          new SqlParameter("@ColorCode",ColorCode),
                                          new SqlParameter("@UserID",UserID),
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_UPD_SysVariable", sqlprm));
        }



        public int Delete_SysVariable(int VariableID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                                        { 
                                            new SqlParameter("@VariableID", VariableID),
                                            new SqlParameter("@UserID", UserID),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Del_SysVariable", sqlprm);
        }

    }
}
