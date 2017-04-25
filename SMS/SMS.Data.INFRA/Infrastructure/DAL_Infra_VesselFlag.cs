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
    public class DAL_Infra_VesselFlag
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";

        public DAL_Infra_VesselFlag(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_VesselFlag()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }

        public DataTable SearchVesselFlag(string searchtext
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_VesselFlag_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable SearchVesselFlag(string searchtext, int? VesselManager
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_VesselFlag_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_VesselFlage_List_DL(int? VslFlagID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VslFlagID", VslFlagID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselFlag_List", obj).Tables[0];
        }

        public int EditVesselFlag_DL(int VslFlagID, string Flag_Name, int? vessel_manager, string mailid, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VslFlagID",VslFlagID),
                                            new SqlParameter("@Flag_Name",Flag_Name),
                                            new SqlParameter("@vessel_manager",vessel_manager),
                                            new SqlParameter("@mailid",mailid),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_VesselFlag", sqlprm);
        }

        public int InsertVesselFlag_DL(string Flag_Name, int? vessel_manager, string mailid, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Flag_Name",Flag_Name),
                                            new SqlParameter("@vessel_manager",vessel_manager),
                                            new SqlParameter("@mailid",mailid),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_VesselFlag", sqlprm);
        }

        public int EditVesselFlag_DL(int VslFlagID, string Flag_Name, string mailid, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VslFlagID",VslFlagID),
                                            new SqlParameter("@Flag_Name",Flag_Name),
                                            new SqlParameter("@mailid",mailid),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_VesselFlag", sqlprm);
        }

        public int InsertVesselFlag_DL(string Flag_Name, string mailid, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Flag_Name",Flag_Name),
                                            new SqlParameter("@mailid",mailid),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_VesselFlag", sqlprm);
        }
        public int DeleteVesselFlag_DL(int UserTypeID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@VslFlagID",UserTypeID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_VesselFlag", sqlprm);
        }

    }


}
