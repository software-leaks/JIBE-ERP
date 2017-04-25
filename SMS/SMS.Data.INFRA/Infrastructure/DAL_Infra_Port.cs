using System;
using System.Collections.Generic;
 
 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Port
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Port(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_Port()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_PortList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortList").Tables[0];
        }

        public DataTable Get_PortList_Search(string searchtext   ,int? countryID
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@CountryID", countryID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortList_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }






        public DataTable Get_PortDetailsByID_DL(int PortID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@PortID", PortID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortDetails",sqlprm).Tables[0];
        }

        public DataTable Get_PortList_Mini_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortList_Mini").Tables[0];
        }

        public DataTable Get_PortList_Mini_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@SearchText", SearchText) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortList_Mini", sqlprm).Tables[0];
        }
        
        public int EditPort_DL(int ID, string UNCTAD, string PORT_NAME, int PORT_COUNTRY, string BP_CODE, int LAT_DEG, int LAT_MIN, string LAT_NS, int LON_DEG, int LON_MIN, string LON_EW)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@UNCTAD",UNCTAD),
                                          new SqlParameter("@PORT_NAME",PORT_NAME),
                                          new SqlParameter("@PORT_COUNTRY",PORT_COUNTRY),
                                          new SqlParameter("@BP_CODE",BP_CODE),
                                          new SqlParameter("@LAT_DEG",LAT_DEG),
                                          new SqlParameter("@LAT_MIN",LAT_MIN),
                                          new SqlParameter("@LAT_NS",LAT_NS),
                                          new SqlParameter("@LON_DEG",LON_DEG),
                                          new SqlParameter("@LON_MIN",LON_MIN),
                                          new SqlParameter("@LON_EW",LON_EW)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Port", sqlprm);
        }

        public int EditPort_DL(int Port_ID, string PORT_NAME, int COUNTRY_ID, string BP_CODE, string PORT_LAT, string PORT_LON, string OCEAN, int? UTC, string Country_Name, bool WarRisk, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",Port_ID),
                                          new SqlParameter("@PORT_NAME",PORT_NAME),
                                          new SqlParameter("@COUNTRY_ID",COUNTRY_ID),
                                          new SqlParameter("@BP_CODE",BP_CODE),
                                          new SqlParameter("@PORT_LAT",PORT_LAT),
                                          new SqlParameter("@PORT_LON",PORT_LON),
                                          new SqlParameter("@OCEAN",OCEAN),
                                          new SqlParameter("@UTC",UTC),
                                          new SqlParameter("@COUNTRY_NAME",Country_Name),
                                          new SqlParameter("@WarRisk",WarRisk),
                                          new SqlParameter("@CreatedBy",CreatedBy),
                                   
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Port", sqlprm);
        }

        public int InsertPort_DL(string UNCTAD, string PORT_NAME, int PORT_COUNTRY, string BP_CODE, int LAT_DEG, int LAT_MIN, string LAT_NS, int LON_DEG, int LON_MIN, string LON_EW)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@UNCTAD",UNCTAD),
                                          new SqlParameter("@PORT_NAME",PORT_NAME),
                                          new SqlParameter("@PORT_COUNTRY",PORT_COUNTRY),
                                          new SqlParameter("@BP_CODE",BP_CODE),
                                          new SqlParameter("@LAT_DEG",LAT_DEG),
                                          new SqlParameter("@LAT_MIN",LAT_MIN),
                                          new SqlParameter("@LAT_NS",LAT_NS),
                                          new SqlParameter("@LON_DEG",LON_DEG),
                                          new SqlParameter("@LON_MIN",LON_MIN),
                                          new SqlParameter("@LON_EW",LON_EW)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Port_old", sqlprm);
        }

        public int InsertPort_DL(string PORT_NAME, int COUNTRY_ID, string BP_CODE, string PORT_LAT, string PORT_LON, string OCEAN, int? UTC,string Country_Name, bool WarRisk, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                                          new SqlParameter("@Port_Name",PORT_NAME),
                                          new SqlParameter("@Country_ID",COUNTRY_ID),
                                          new SqlParameter("@BP_CODE",BP_CODE),
                                          new SqlParameter("@PORT_LAT",PORT_LAT),
                                          new SqlParameter("@PORT_LON",PORT_LON),
                                          new SqlParameter("@OCEAN",OCEAN),
                                          new SqlParameter("@UTC",UTC),
                                          new SqlParameter("@PORT_COUNTRY",Country_Name),
                                          new SqlParameter("@WarRisk",WarRisk),
                                          new SqlParameter("@CreatedBy", CreatedBy)
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Port", sqlprm);
        }
        public int DeletePort_DL(int ID , int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                 new SqlParameter("@ID", ID),
                 new SqlParameter("@CreatedBy", CreatedBy),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_Port", sqlprm);
        }

      
    }

}
