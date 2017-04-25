using System;
using System.Collections.Generic;
 
 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_City
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_City(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_City()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_CityDetailsByID_DL(int CityID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@CityID", CityID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CityDetails",sqlprm).Tables[0];
        }

        public DataTable Get_CityList_Mini_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CityList_Mini").Tables[0];
        }

        public DataTable Get_CityList_Mini_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@SearchText", SearchText) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CityList_Mini", sqlprm).Tables[0];
        }
     
    }

}
