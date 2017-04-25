using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_Port
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Port
    {
        DAL_Infra_Port objDAL = new DAL_Infra_Port();

        public BLL_Infra_Port()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable Get_PortList()
        {
            try
            {
                return objDAL.Get_PortList_DL();
            }
            catch
            {
                throw;
            }
        }


        public DataTable Get_PortList_Search(string searchtext ,int? countryID 
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.Get_PortList_Search(searchtext,countryID ,sortby, sortdirection,pagenumber, pagesize , ref isfetchcount);
        
        }




        public DataTable Get_PortDetailsByID(int PortID)
        {
            try
            {
                return objDAL.Get_PortDetailsByID_DL(PortID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_PortList_Mini()
        {
            try
            {
                return objDAL.Get_PortList_Mini_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_PortList_Mini(string SearchText)
        {
            try
            {
                return objDAL.Get_PortList_Mini_DL(SearchText);
            }
            catch
            {
                throw;
            }
        }

        public int EditPort(int ID, string UNCTAD, string PORT_NAME, int PORT_COUNTRY, string BP_CODE, int LAT_DEG, int LAT_MIN, string LAT_NS, int LON_DEG, int LON_MIN, string LON_EW)
        {
            try
            {
                return objDAL.EditPort_DL(ID, UNCTAD, PORT_NAME, PORT_COUNTRY, BP_CODE, LAT_DEG, LAT_MIN, LAT_NS, LON_DEG, LON_MIN, LON_EW);
            }
            catch
            {
                throw;
            }
        }


        public int EditPort(int PORT_ID, string PORT_NAME, int COUNTRY_ID, string BP_CODE, string PORT_LAT, string PORT_LON, string OCEAN, int? UTC, string Country_Name ,bool WarRisk, int CreatedBy)
        {
            try
            {
                return objDAL.EditPort_DL(PORT_ID, PORT_NAME, COUNTRY_ID, BP_CODE, PORT_LAT, PORT_LON, OCEAN, UTC, Country_Name,WarRisk,CreatedBy);
            }
            catch
            {
                throw;
            }
        }

        public int InsertPort(string UNCTAD, string PORT_NAME, int PORT_COUNTRY, string BP_CODE, int LAT_DEG, int LAT_MIN, string LAT_NS, int LON_DEG, int LON_MIN, string LON_EW)
        {
            try
            {
                return objDAL.InsertPort_DL(UNCTAD, PORT_NAME, PORT_COUNTRY, BP_CODE, LAT_DEG, LAT_MIN, LAT_NS, LON_DEG, LON_MIN, LON_EW);
            }
            catch
            {
                throw;
            }
        }

        public int InsertPort(string PORT_NAME, int COUNTRY_ID, string BP_CODE, string PORT_LAT, string PORT_LON, string OCEAN, int? UTC,string Country_Name, bool WarRisk ,int CreatedBy)
        {
            try
            {
                return objDAL.InsertPort_DL(PORT_NAME, COUNTRY_ID, BP_CODE,  PORT_LAT, PORT_LON, OCEAN, UTC, Country_Name,WarRisk ,CreatedBy);
            }
            catch
            {
                throw;
            }
        }

        public int DeletePort_DL(int Port_ID, int CreatedBy)
        {
            try
            {
                return objDAL.DeletePort_DL(Port_ID,CreatedBy);
            }
            catch
            {
                throw;
            }
        }

      
  
    }

}