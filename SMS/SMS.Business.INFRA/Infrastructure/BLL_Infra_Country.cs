using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Country
    {
        DAL_Infra_Country objDAL = new DAL_Infra_Country();

        public BLL_Infra_Country()
        {

        }


        public DataTable SearchCountry(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchCountry(searchtext, sortby, sortdirection, pagenumber,pagesize, ref isfetchcount);
        
        }



        public DataTable Get_CountryList()
        {
            try
            {
                return objDAL.Get_CountryList_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CountryList(string FilterString)
        {
            string SQL = @"SELECT  ID, Country_Name,ISO_CODE FROM  LIB_Country WHERE Active_Status=1 ";
            try
            {
                if (FilterString != null)
                {
                    string[] cols = { "Country_Name", "ISO_CODE" };
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

                    SQL += " ORDER BY Country_Name,ISO_CODE";
                }

                return objDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }
        }

        public int EditCountry(int ID, string Country_Name, string ISO_Code, int CreatedBy)
        {
            try
            {
                return objDAL.EditCountry_DL(ID, Country_Name, ISO_Code, CreatedBy);
            }
            catch
            {
                throw;
            }
        }

        public int InsertCountry(string Country, string ISO_Code, int CreatedBy)
        {
            try
            {
                return objDAL.InsertCountry_DL(Country, ISO_Code,CreatedBy);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCountry(int ID, int CreatedBy)
        {
            try
            {
                return objDAL.DeleteCountry_DL(ID,CreatedBy);
            }
            catch
            {
                throw;
            }
        }
    }

}