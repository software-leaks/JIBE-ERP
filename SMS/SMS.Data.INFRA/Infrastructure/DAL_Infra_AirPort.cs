using System;
using System.Collections.Generic;
 
 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_AirPort
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_AirPort(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_AirPort()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_AirPortList(int AirPortID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@AirPortID", AirPortID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_AirPort_List", sqlprm).Tables[0];
        }



        public DataTable Get_AirPort_Search(string searchtext, int? countryID
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_AirPort_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_AirPort(string searchtext)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext)
                  
                    
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_AirPort", obj).Tables[0];


        }

        public int Insert_AirPort_DL(string Indent, string type, string AirPortName, string Latitude_degree, string Longitude_degree
           , int? Elevation_ft, string Continent, string ISO, int? CountryID, string ISO_Region, string Municipality, string Scheduled_service, string Gps_Code, string Iata_code, string Local_Code, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@Indent",Indent),
                                          new SqlParameter("@type",type),
                                          new SqlParameter("@AirPortName",AirPortName),
                                          new SqlParameter("@Latitude_degree",Latitude_degree),
                                          new SqlParameter("@Longitude_degree",Longitude_degree),
                                          new SqlParameter("@Elevation_ft",Elevation_ft),
                                          new SqlParameter("@Continent",Continent),
                                          new SqlParameter("@ISO",ISO),
                                          new SqlParameter("@CountryID",CountryID),
                                          new SqlParameter("@ISO_Region",ISO_Region),
                                          new SqlParameter("@Municipality",Municipality),
                                          new SqlParameter("@Scheduled_service",Scheduled_service),
                                          new SqlParameter("@Gps_Code",Gps_Code),
                                          new SqlParameter("@Iata_code",Iata_code),
                                          new SqlParameter("@Local_Code",Local_Code),
                                          new SqlParameter("@UserID",UserID),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_AirPort", sqlprm);
        }


        public int Edit_AirPort_DL(int AirportID, string Indent, string type, string AirPortName, string Latitude_degree, string Longitude_degree
            , int? Elevation_ft, string Continent, string ISO, int? CountryID, string ISO_Region,string Municipality,string Scheduled_service,string Gps_Code,string Iata_code,string Local_Code,int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@AirPortID",AirportID),
                                          new SqlParameter("@Indent",Indent),
                                          new SqlParameter("@type",type),
                                          new SqlParameter("@AirPortName",AirPortName),
                                          new SqlParameter("@Latitude_degree",Latitude_degree),
                                          new SqlParameter("@Longitude_degree",Longitude_degree),
                                          new SqlParameter("@Elevation_ft",Elevation_ft),
                                          new SqlParameter("@Continent",Continent),
                                          new SqlParameter("@ISO",ISO),
                                          new SqlParameter("@CountryID",CountryID),
                                          new SqlParameter("@ISO_Region",ISO_Region),
                                          new SqlParameter("@Municipality",Municipality),
                                          new SqlParameter("@Scheduled_service",Scheduled_service),
                                          new SqlParameter("@Gps_Code",Gps_Code),
                                          new SqlParameter("@Iata_code",Iata_code),
                                          new SqlParameter("@Local_Code",Local_Code),
                                          new SqlParameter("@UserID",UserID),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_AirPort", sqlprm);
        }



        public int Delete_AirPort_DL(int AirPortID , int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                                        { 
                                            new SqlParameter("@AirPortID", AirPortID),
                                            new SqlParameter("@UserID", UserID),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_AirPort", sqlprm);
        }


    }
}




 