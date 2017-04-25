using System;
using System.Collections.Generic;
 
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Infrastructure
{
   public class DAL_Infra_ExchangeRate
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public  DAL_Infra_ExchangeRate()
        {

        }
  

    #region exchange rate method

        public DataSet OCAP_Exch_Rate_fill_DL(bool showall)
        {
            SqlParameter sqlprm = new SqlParameter("@showall",showall);
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_OCAP_GetAcc_Exch_Rate", sqlprm);
        }

        public DataSet OCAP_Exch_Rate_fillHistory_DL(int currCode)
        {
            SqlParameter sqlprm = new SqlParameter("@currCode", currCode);
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_OCAP_GetAcc_ExchRate_History", sqlprm);
        }

        public int OCAP_Exch_Rate_Del_DL(int id)
        {
            SqlParameter sqlprm = new SqlParameter("id", id);
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_OCAP_DELAcc_Exch_Rate", sqlprm);
        }

        public int OCAP_Exch_Rate_Edit_DL(int id, int Currency_Type, float Exch_rate, string date, int Base_curr)
        {

            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime dt = DateTime.Parse(date, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@id",id),
                                          new SqlParameter("@curr_code",Currency_Type),
                                          new SqlParameter("@exch_rate",Exch_rate),
                                          new SqlParameter("base_curr",Base_curr),
                                          new SqlParameter("date",dt)
                                         };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_OCAP_UpdAcc_Exch_Rate", sqlprm);
        }

        public DataSet OCAP_Exch_Rate_fill_dropdown_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_OCAP_Fill_DropDN_currency"); 
        }

        public int OCAP_Exch_Rate_INS_DL(int Currency_code, float Exch_rate, string date, int Base_curr)
        {
            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime dt = DateTime.Parse(date, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@currcode",Currency_code),
                                          new SqlParameter("@Exch_rate",Exch_rate),
                                          new SqlParameter("@Base_curr",Base_curr),
                                          new SqlParameter("date",dt)
                                         };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_OCAP_InsAcc_Exch_Rate", sqlprm);
        }

        public int ACC_Ins_Exchrate_XML_DL(string exch)
        {
            SqlParameter prm = new SqlParameter("xmlexchrate", exch);
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_ACC_Ins_Exchrate_XML", prm);
        }

        public decimal ACC_Get_ExchRate_By_Code_DL(string Code)
        {
            SqlParameter prm = new SqlParameter("@Code", Code);
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "ACC_Gey_ExchRate_ByCode", prm));
            
        }

        #endregion 

    }
}
