using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_Currency
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Currency
    {
        DAL_Infra_Currency objDAL = new DAL_Infra_Currency();

        public BLL_Infra_Currency()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable SearchCurrency(string searchtext, int? countrycode, int? isfavorite
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchCurrency(searchtext, countrycode, isfavorite, sortby, sortdirection, pagenumber,  pagesize, ref isfetchcount);

        }


        public DataTable Get_CurrencyList()
        {
            try
            {
                return objDAL.Get_CurrencyList_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CurrencyList(string FilterString)
        {
            string SQL = @"SELECT     LIB_CURRENCY.Currency_ID, LIB_CURRENCY.Currency_Code, LIB_CURRENCY.Currency_Discription, ISNULL(LIB_CURRENCY.Country, 0) AS Country, 
                      ISNULL(LIB_CURRENCY.Favorite, 0) AS Favorite, derivedtbl_1.COUNTRY AS country_name
                FROM         LIB_CURRENCY INNER JOIN
                          (SELECT     ID, Country_Name AS COUNTRY
                            FROM          LIB_Country) AS derivedtbl_1 ON LIB_CURRENCY.Country = derivedtbl_1.ID ";
            try
            {
                if (FilterString != null)
                {
                    string[] cols = { "LIB_CURRENCY.Currency_Code", "LIB_CURRENCY.Currency_Discription", "derivedtbl_1.COUNTRY" };
                    string[] arParam = FilterString.Split(' ');
                    string sqlTemp = "";
                    if (FilterString.Length > 0)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            for (int j = 0; j < arParam.Length; j++)
                            {
                                if (sqlTemp.Length > 0)
                                    sqlTemp += " OR ";

                                sqlTemp += cols[i] + " like '%" + arParam[j] + "%' ";
                            }
                        }

                        if (sqlTemp.Length > 0)
                            SQL += " AND (" + sqlTemp + ")";
                    }

                    SQL += " ORDER BY Currency_Code,Currency_Discription";
                }

                return objDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }
        }

        public int EditCurrency(string Currency_Code, string Currency_Discription, int Country, int Currency_ID, bool favorite, int Created_By)
        {
            try
            {
                return objDAL.EditCurrency_DL(Currency_Code, Currency_Discription, Country, Currency_ID, favorite, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int InsertCurrency(string Currency_Code, string Currency_Discription, int Country, bool favorite, int Created_By)
        {
            try
            {
                return objDAL.InsertCurrency_DL(Currency_Code, Currency_Discription, Country, favorite, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCurrency(int Currency_ID, int Created_By)
        {
            try
            {
                return objDAL.DeleteCurrency_DL(Currency_ID, Created_By);
            }
            catch
            {
                throw;
            }
        }
    }

}