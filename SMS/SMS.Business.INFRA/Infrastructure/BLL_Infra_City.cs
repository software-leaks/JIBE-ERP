using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_City
    {
        DAL_Infra_City objDAL = new DAL_Infra_City();

        public BLL_Infra_City()
        {
            
        }

        public DataTable Get_CityDetailsByID(int CityID)
        {
            try
            {
                return objDAL.Get_CityDetailsByID_DL(CityID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CityList_Mini()
        {
            try
            {
                return objDAL.Get_CityList_Mini_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CityList_Mini(string SearchText)
        {
            try
            {
                return objDAL.Get_CityList_Mini_DL(SearchText);
            }
            catch
            {
                throw;
            }
        }

       
    }

}