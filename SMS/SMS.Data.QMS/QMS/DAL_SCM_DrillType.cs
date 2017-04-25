using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.QMS
{
    public class DAL_SCM_DrillType
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_SCM_DrillType(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_SCM_DrillType()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable SearchDrillType(int? VesselID, string DrillName
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                   new System.Data.SqlClient.SqlParameter("@DrillName", DrillName),
                   
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_SCM_Get_DrillType_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataTable Get_DrillTypeList(int DrillID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@DrillID", DrillID),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_SCM_Get_DrillType_List", obj);
            return ds.Tables[0];
        }


        //public DataTable Get_CurrencyList_DL()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CurrencyList").Tables[0];
        //}

        public int EditDrillType(int? VesselID, string DrillName, int? frequency, int AccessID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@VesselID",VesselID),
                                          new SqlParameter("@DrillName",DrillName),
                                          new SqlParameter("@frequency",frequency),
                                          new SqlParameter("@AccessID",AccessID),
                                          new SqlParameter("@Created_By",Created_By)
                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_SCM_Update_DrillType", sqlprm));
        }

        public int InsertDrillType(int? VesselID, string DrillName, int? frequency, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@VesselID",VesselID),
                                          new SqlParameter("@DrillName",DrillName),
                                          new SqlParameter("@frequency",frequency),
                                          new SqlParameter("@Created_By",Created_By)
                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_SCM_Insert_DrillType", sqlprm));
        }

        public int DeleteDrillType(int DrillID, int Created_By)
        {
    
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DrillID",DrillID),
                                            new SqlParameter("@Created_By",Created_By)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_SCM_Del_DrillType", sqlprm);
        }

    }
}
