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
   public class DAL_Infra_ShipDepartment
   {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";

        public DAL_Infra_ShipDepartment(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_ShipDepartment()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }

        public DataTable SearchShipDepartment(string searchtext, int? VesselManager
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Manager", VesselManager),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_ShipDepartment_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_ShipDepartment_List_DL(int? DeptID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@DeptID", DeptID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_ShipDepartment_List", obj).Tables[0];
        }

        public int EditShipDepartment_DL(int DeptID, string DeptName, int? vessel_manager, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DeptID",DeptID),
                                            new SqlParameter("@DeptName",DeptName),
                                            new SqlParameter("@CompanyID",vessel_manager),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_ShipDepartment", sqlprm);
        }

        public int InsertShipDepartment_DL(string DeptName, int? vessel_manager, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DeptName",DeptName),
                                            new SqlParameter("@CompanyID",vessel_manager),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_ShipDepartment", sqlprm);
        }

        public int DeleteShipDepartment_DL(int DeptID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@DeptID",DeptID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_ShipDepartment", sqlprm);
        }




   }
}
