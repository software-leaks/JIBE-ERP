using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;
 


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_AirPort
    {

        DAL_Infra_AirPort objDAL = new DAL_Infra_AirPort();
        public BLL_Infra_AirPort()
        {
            
        }

        public DataTable Get_AirPortList(int AirPortID)
        {
            return objDAL.Get_AirPortList(AirPortID);
        }


        public DataTable Get_AirPort_Search(string searchtext, int? countryID
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.Get_AirPort_Search(searchtext, countryID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_AirPort(string searchtext)
        {
            return objDAL.Get_AirPort(searchtext);
        }

        public int Insert_AirPort(string Indent, string type, string AirPortName, string Latitude_degree, string Longitude_degree
          , int? Elevation_ft, string Continent, string ISO, int? CountryID, string ISO_Region, string Municipality, string Scheduled_service, string Gps_Code
          , string Iata_code, string Local_Code, int UserID)
        {

            return objDAL.Insert_AirPort_DL(Indent, type, AirPortName, Latitude_degree, Longitude_degree, Elevation_ft, Continent
                , ISO, CountryID, ISO_Region, Municipality, Scheduled_service, Gps_Code, Iata_code, Local_Code, UserID);
        }

        public int Edit_AirPort(int AirportID, string Indent, string type, string AirPortName, string Latitude_degree, string Longitude_degree
         , int? Elevation_ft, string Continent, string ISO, int? CountryID, string ISO_Region, string Municipality, string Scheduled_service, string Gps_Code
         , string Iata_code, string Local_Code, int UserID)
        {

            return objDAL.Edit_AirPort_DL(AirportID, Indent, type, AirPortName, Latitude_degree, Longitude_degree, Elevation_ft, Continent
                    , ISO, CountryID, ISO_Region, Municipality, Scheduled_service, Gps_Code, Iata_code, Local_Code, UserID);
        }

        public int Delete_AirPort(int AirPortID, int UserID)
        {
            return objDAL.Delete_AirPort_DL(AirPortID,UserID);
        
        }
    }
}
