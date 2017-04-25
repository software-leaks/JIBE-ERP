using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using SMS.Data; 


namespace DALReportsNameSpace
{
    public class DALReports
    {
        public DALReports()
        {
          
        }

        string _internalConnection =   ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DataSet Rpt_DailyNoon_Details()
        {
            string sqltext="Select Telegram_Id,Vessel_Name,Tech_Manager,Convert(Varchar(25),Telegram_Date,103)  [Telegram_Date],VOYAGE [Voyage],Name as Location, ";
                     sqltext+="convert(varchar(5), Longitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  Longitude_N_S as Longitude,convert(varchar(5), Latitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  LATITUDE_E_W as Latitude, " ;
                     sqltext += "Vessel_Course,AVERAGE_SPEED,INSTRUCTED_SPEED,SLIP_FACTOR,CAST(ME_LOAD_IND AS INT) AS ME_LOAD_IND,Telegram_Id,Consumption_FO_Static,Consumption_FO_Man,Consumption_DO_Static,Consumption_DO_Man,CAST(Consumption_FO_Man AS VARCHAR(10)) + '/' + CAST(Consumption_DO_Man AS VARCHAR(10)) AS Actual_Consumption,CAST(Consumption_FO_Static AS VARCHAR(10)) + '/' + CAST(Consumption_DO_Static AS VARCHAR(10)) AS CPConsumption,TDT.Vessel_Code  From TEC_Dtl_Telegrams TDT inner join TEC_Lib_Systems_Parameters tlsp on tlsp.code = tdt.location_code and parent_code = 1 inner join Lib_Vessels lv on lv.Vessel_Code = tdt.Vessel_Code  where  TDT.Telegram_Type='N' Order by Vessel_Name";
            
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqltext); 
 

        }
                public DataSet Rpt_DailyNoon_Details_Performance()
        {
            string sqltext="Select Telegram_Id,Vessel_Name,Tech_Manager,Convert(Varchar(25),Telegram_Date,103)  [Telegram_Date],VOYAGE [Voyage],Name as Location, ";
                     sqltext+="convert(varchar(5), Longitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  Longitude_N_S as Longitude,convert(varchar(5), Latitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  LATITUDE_E_W as Latitude, " ;
                     sqltext += "Vessel_Course,AVERAGE_SPEED,INSTRUCTED_SPEED,SLIP_FACTOR,Consumption_FO_Static,Consumption_FO_Man,Consumption_DO_Static,Consumption_DO_Man,CAST(ME_LOAD_IND AS INT) AS ME_LOAD_IND,Telegram_Id,CAST(Consumption_FO_Man AS VARCHAR(10)) + '/' + CAST(Consumption_DO_Man AS VARCHAR(10)) AS Actual_Consumption,CAST(Consumption_FO_Static AS VARCHAR(10)) + '/' + CAST(Consumption_DO_Static AS VARCHAR(10)) AS CPConsumption,TDT.Vessel_Code  From TEC_Dtl_Telegrams TDT inner join TEC_Lib_Systems_Parameters tlsp on tlsp.code = tdt.location_code and parent_code = 1 inner join Lib_Vessels lv on lv.Vessel_Code = tdt.Vessel_Code  where  TDT.Telegram_Type='N' And  (Consumption_FO_Man > Consumption_FO_Static Or Consumption_DO_Man > Consumption_DO_Static Or AVERAGE_SPEED < INSTRUCTED_SPEED)  Order by Vessel_Name";
            
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqltext); 
 

        }
         
        public DataSet Rpt_SunWed_Get_Details()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_SunWed_Get_Details"); 
        }

        public DataSet Rpt_Arrival_Get_Details()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_Arrival_Get_DetailsForGrid"); 
        }

        public DataSet Rpt_Departure_Get_Details()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_Departure_Get_Details"); 
        }

        public DataSet Rpt_MonthEndPerformanceDetails()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_MonthEndPerformanceDetails"); 
        }
        public DataSet Rpt_QuarterlyPerformanceDetails()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_GetDetailsQuaPerformanceReport"); 
        }

        public DataSet Search_MonthEndPerformanceDetails(string strvesselname, string strfleet, string dtfromdate, string dttodate)
        {
           string strsql=" SELECT MPR.ID,B.Vessel_Name,B.Tech_Manager,Convert(Varchar(25),MPR.Date,103) As Date,";
                  strsql+="MPR.MECC_Consumption,MPR.AECC_Consumption,MPR.Victualling,MPR.ROB_MECYL,MPR.ROB_AECC, ";
                  strsql+="MPR.Other_Oil_ROB,MPR.MECC_Received,MPR.AECC_Received,MPR.Deck_Overtime,MPR.Engine_Overtime, ";
                  strsql += "MPR.Catering_Overtime,MPR.Other_Comm_Expenses FROM Month_End_Performance_ReportDetails as MPR inner join ";
                  strsql+="Lib_Vessels as B on MPR.Vessel_Code = B.Vessel_Code where ";

                    if (strvesselname != "--All--")
                    {
                        strsql = strsql + " B.Vessel_Name Like  '" + strvesselname + "'";
                    }
                    if (strvesselname != "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + "And B.Tech_Manager Like '" + strfleet + "'";
                    }
                    if (strvesselname == "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + " B.Tech_Manager Like '" + strfleet + "'";
                    }

                    if (strvesselname == "--All--" && strfleet == "--All--")
                    {
                        strsql = strsql + " MPR.Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
                    else
                    {
                        strsql = strsql + " And MPR.Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }

                   return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, strsql); 
        }

        public DataSet Search_Rpt_QuarterlyPerformanceDetails(string strvesselname, string strfleet, string dtfromdate, string dttodate)
        {
           string strsql=" select QPR.ID,B.Vessel_Name, B.Tech_Manager,Convert(Varchar(25),QPR.Date,103) As Date,QPR.AvgSpeed_Ballast,";
                    strsql+=" QPR.RPMonPassages_Ballast,QPR.Avg_FO_cons_Ballast,QPR.Avg_DO_cons_Ballast,QPR.Avg_BoilerOil_cons_Ballast,";
                    strsql+=" QPR.Avg_tankClean_cons_Ballast,QPR.Avg_DailySpeed_Loaded,QPR.Avg_RPMonPassage_Loaded,QPR.Avg_FO_cons_Loaded, ";
                    strsql+=" QPR.Avg_DO_cons_Loaded,QPR.Avg_BoilerOil_cons_Loaded,QPR.Avg_CargoHeating_cons_Tanker,QPR.Avg_MECC_Consumption, ";
                    strsql+=" QPR.Avg_MECC_Gms_BHP_Hr,QPR.Avg_AECC_Consumption,QPR.MECYL_OilGms_BHP_Hr,QPR.Total_SteamTime_Ballast, ";
                    strsql += " QPR.Total_SteamTime_Loaded,QPR.Total_Stoppage_Ballast,QPR.Total_Stoppage_Loaded ";
                    strsql+=" from Quarterly_Performance_ReportDetails as QPR inner join Lib_Vessels as B on QPR.Vessel_Code=B.Vessel_Code  where ";

                    if (strvesselname != "--All--")
                    {
                        strsql = strsql + " B.Vessel_Name Like  '" + strvesselname + "'";
                    }
                    if (strvesselname != "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + "And B.Tech_Manager Like '" + strfleet + "'";
                    }
                    if (strvesselname == "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + " B.Tech_Manager Like '" + strfleet + "'";
                    }

                    if (strvesselname == "--All--" && strfleet == "--All--")
                    {
                        strsql = strsql + " QPR.Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
                    else
                    {
                        strsql = strsql + " And QPR.Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }

                   return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, strsql); 
        }



        public DataSet getSunWedDetailsByID(int ID, int vesselCode)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@vesselCode",vesselCode),
                                        };

            obj[0].Value = ID;
            obj[1].Value = vesselCode;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_GetDetailsSunWedReport",obj);

        }
        public DataSet getMonthendPerformanceDetailsByID(int ID, int vesselCode)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@vesselCode",vesselCode),
                                        };

            obj[0].Value = ID;
            obj[1].Value = vesselCode;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_MonthEndPerformanceDetailsbByID", obj);

        }
        public DataSet getQuerterlyPerformanceDetailsByID(int ID, int vesselCode)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@vesselCode",vesselCode),
                                        };

            obj[0].Value = ID;
            obj[1].Value = vesselCode;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_QuerterlyPerformanceDetailsbByID", obj);

        }

        public DataSet getArrivalDetailsByID(int ID, int vesselCode)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@vesselCode",vesselCode),
                                        };

            obj[0].Value = ID;
            obj[1].Value = vesselCode;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_getArrivalDetailsByID", obj);

        }

        public DataSet getDepartureDetailsByID(int ID, int vesselCode)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@vesselCode",vesselCode),
                                        };

            obj[0].Value = ID;
            obj[1].Value = vesselCode;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_Rpt_getDepatureDetailsByID", obj);

        }

               
        
        public SqlDataReader Rpt_DailyNoonIndividual_Details(int telegramid,int vesselid)
        {
            return SqlHelper.ExecuteReader(_internalConnection, CommandType.Text, "Select Convert(Varchar(25),Telegram_Date,103) As Telegram_Date,VOYAGE,(CASE BALAST_FLAG WHEN 'Y' THEN 'Laden' ELSE 'Ballast' END) AS LADENORBALLAST,(Select Name From TEC_Lib_Systems_Parameters tlsp where tlsp.code = tdt.location_code) AS LOCATION, Longitude_Degrees,Longitude_Seconds, " +
                           " Longitude_N_S,convert(varchar(5), Longitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +   convert(varchar(5), Longitude_Seconds) + ' ' +  Longitude_N_S as Longitude,Latitude_Degrees,Latitude_Seconds,LATITUDE_E_W,convert(varchar(5), Latitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  convert(varchar(5), Latitude_Seconds) + ' ' +  LATITUDE_E_W as Latitude,Wind_Direction, " +
                           " Sea_Direction,Wind_Force,	Swell_Direction,Swell_Hieght,Vessel_Course,AVERAGE_SPEED,INSTRUCTED_SPEED,SLIP_FACTOR,Next_Port,CARGO_NAME_1, " +
                           " SEA_TEMPERATURE,CDFrmStormCenter,ME_LOAD_IND,TimeOfCPA,Remarks,Vessel_Name,Tech_Manager,Consumption_FO_Static,Consumption_FO_Man,Consumption_DO_Static,Consumption_DO_Man,Eta_Next_Port From TEC_Dtl_Telegrams tdt inner join Lib_Vessels lv on lv.Vessel_Code = tdt.Vessel_Code where telegram_Id = " + telegramid + " and TDT.Vessel_Code = " + vesselid + " and Telegram_Type='N' "); 
            
        }
        public DataSet Search_Rpt_DailyNoon_Details(string strvesselname,string strfleet,string strlocation,string strladen,string dtfromdate,string dttodate)
        {
            string strsql = "Select Telegram_Id,Vessel_Name,Tech_Manager,Convert(Varchar(25),Telegram_Date,103)  [Telegram_Date],VOYAGE [Voyage],Name [Location], " +
                           " convert(varchar(5), Longitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  Longitude_N_S as Longitude,convert(varchar(5), Latitude_Degrees) + ' ' + convert(varchar(5), Latitude_Minutes) + ' ' +  LATITUDE_E_W as Latitude, " +
                           " Vessel_Course,AVERAGE_SPEED,INSTRUCTED_SPEED,SLIP_FACTOR,ME_LOAD_IND,Telegram_Id,CAST(ME_LOAD_IND AS INT) AS ME_LOAD_IND,Telegram_Id,Consumption_FO_Static,Consumption_FO_Man,Consumption_DO_Static,Consumption_DO_Man,CAST(Consumption_FO_Man AS VARCHAR(10)) + '/' + CAST(Consumption_DO_Man AS VARCHAR(10)) AS Actual_Consumption,CAST(Consumption_FO_Static AS VARCHAR(10)) + '/' + CAST(Consumption_DO_Static AS VARCHAR(10)) AS CPConsumption,TDT.Vessel_Code  From TEC_Dtl_Telegrams TDT inner join TEC_Lib_Systems_Parameters tlsp on tlsp.code = tdt.location_code and parent_code = 1 inner join Lib_Vessels lv on lv.Vessel_Code = tdt.Vessel_Code " +
                           " Where  ";

            if (strvesselname != "--All--")
            {
                strsql = strsql + " Vessel_Name Like  '" + strvesselname + "'";
            }
            if (strvesselname != "--All--" && strfleet != "--All--")
            {
                strsql = strsql + "And Tech_Manager Like '" + strfleet + "'";
            }
            if (strvesselname == "--All--" && strfleet != "--All--")
            {
                strsql = strsql + " Tech_Manager Like '" + strfleet + "'";
            }
            
            if ((strvesselname != "--All--" || strfleet != "--All--") && strlocation != "--All--")
            {
                strsql = strsql + " And Name Like '" + strlocation + "'";
            }
            if (strvesselname == "--All--" && strfleet == "--All--" && strlocation != "--All--")
            {
                strsql = strsql + " Name Like '" + strlocation + "'";
            }

            if ((strvesselname != "--All--" || strfleet != "--All--" || strlocation != "--All--") && strladen != "All")
            {
                strsql = strsql + " And Balast_Flag Like '" + strladen + "'";
            }

            if (strvesselname == "--All--" && strfleet == "--All--" && strlocation == "--All--" && strladen != "All")
            {
                strsql = strsql + " Balast_Flag Like '" + strladen + "'";
            }
 
            if (strvesselname == "--All--" && strfleet == "--All--" && strlocation == "--All--" && strladen == "All")
            {
                strsql = strsql + " Telegram_Date Between '" + dtfromdate + "' and '" + dttodate + "'";
            }
            else
            {
                strsql = strsql + " And Telegram_Date Between '" + dtfromdate + "' and '" + dttodate + "'";
            }
            strsql = strsql + " And TDT.Telegram_Type='N' Order by Vessel_Name";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text,strsql); 
        }

        public DataSet Search_Rpt_SunWed_Details(string strvesselname, string strfleet, string dtfromdate, string dttodate)
        {

            string strsql = "Select id,B.Vessel_Name,B.Tech_Manager,Convert(Varchar(25),Telegram_Date,103) ";
                    strsql += "[Telegram_Date],Sea_Water_Temp,Generator_Load,Log_Speed,Displacement, ";
                    strsql += "RPM,Fuel_Rack,Agent, BHP,Max_Comp_Pressure from Sun_Wed_Noon_Rpt ";
                    strsql += "as A inner join  Lib_Vessels as B on A.Vessel_Code = B.Vessel_Code where ";



                    if (strvesselname != "--All--")
                    {
                        strsql = strsql + " Vessel_Name Like  '" + strvesselname + "'";
                    }
                    if (strvesselname != "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + "And B.Tech_Manager Like '" + strfleet + "'";
                    }
                    if (strvesselname == "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + " B.Tech_Manager Like '" + strfleet + "'";
                    }

                    if (strvesselname == "--All--" && strfleet == "--All--")
                    {
                        strsql = strsql + " Telegram_Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
                    else
                    {
                        strsql = strsql + " And Telegram_Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
          

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text,strsql); 
        }

        public DataSet Search_Rpt_Arrival_Details(string strvesselname, string strfleet, string dtfromdate, string dttodate)
        {

            string strsql = "Select AR.ID,B.Vessel_Name,B.Tech_Manager,Convert(Varchar(25),AR.Date,103) As Date, ";
            strsql += "(Select Port_Name from dbo.PORTS_LIBRARY JLP Where JLP.ID = AR.Port) As Port, ";
                    strsql+="AR.TimeZone,Convert(Varchar(25),AR.ArrivalDate,103)+' '+ ";
                    strsql+=" +  Convert(Varchar(25),AR.ArrivalDate,108) ArrivalDate,Convert(Varchar(25),AR.ETB,103)+' '+ ";
                    strsql+=" +  Convert(Varchar(25),AR.ETB,108) ETB,Convert(Varchar(25),AR.ETD,103)+' '+ ";
                    strsql+="+  Convert(Varchar(25),AR.ETD,108) ETD , ";
                    strsql+="AR.ROBHO,AR.ROBDO,AR.ROBFW,AR.ROBMECC,AR.ROBMECYL,AR.ROBAECC,AR.ROBFREON, ";
                    strsql += "AR.SLIP,AR.AvgMECCConsumption,AR.AvgMECYLConsumption,AR.AvgAECCConsumption, ";
                    strsql+="AR.OffHireFrom,AR.OffHireTo, AR.Send_To_Office FROM Arrival_Report as AR inner join Lib_Vessels as B on AR.Vessel_Code = B.Vessel_Code where ";

                    if (strvesselname != "--All--")
                    {
                        strsql = strsql + " B.Vessel_Name Like  '" + strvesselname + "'";
                    }
                    if (strvesselname != "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + "And B.Tech_Manager Like '" + strfleet + "'";
                    }
                    if (strvesselname == "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + " B.Tech_Manager Like '" + strfleet + "'";
                    }

                    if (strvesselname == "--All--" && strfleet == "--All--")
                    {
                        strsql = strsql + " AR.Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
                    else
                    {
                        strsql = strsql + " And AR.Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
          

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text,strsql); 
        }

        public DataSet Search_Rpt_Departure_Details(string strvesselname, string strfleet, string dtfromdate, string dttodate)
        {

            string strsql = "Select id,B.Vessel_Name,B.Tech_Manager,Convert(Varchar(25),Date,103) ";
            strsql += "[Date],Port,ETD,Next_Port,Dist_To_Next_Port,Next_Port_Operation,Cargo_Type,Vessel_Condition,Quantity_Loaded,Quantity_Discharged from Departure_report ";
                    strsql += "as A inner join  Lib_Vessels as B on A.Vessel_Code = B.Vessel_Code where ";



                    if (strvesselname != "--All--")
                    {
                        strsql = strsql + " Vessel_Name Like  '" + strvesselname + "'";
                    }
                    if (strvesselname != "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + "And B.Tech_Manager Like '" + strfleet + "'";
                    }
                    if (strvesselname == "--All--" && strfleet != "--All--")
                    {
                        strsql = strsql + " B.Tech_Manager Like '" + strfleet + "'";
                    }

                    if (strvesselname == "--All--" && strfleet == "--All--")
                    {
                        strsql = strsql + " Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
                    else
                    {
                        strsql = strsql + " And Date Between '" + dtfromdate + "' and '" + dttodate + "'";
                    }
          

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text,strsql); 
        }
        

        public SqlDataReader ddlLocaiton_fill()
        {
            return SqlHelper.ExecuteReader(_internalConnection, CommandType.Text, "Select Code,Name From TEC_Lib_Systems_Parameters tlsp Where tlsp.Parent_Code = 1");
            
        }
        public SqlDataReader ddlVesselName_fill()
        {
            return SqlHelper.ExecuteReader(_internalConnection, CommandType.Text, "Select Vessel_Name From Lib_Vessels Where Active_Status = 1 Order by Vessel_Name"); 
        }
        public SqlDataReader ddlFleet_fill()
        {
            return SqlHelper.ExecuteReader(_internalConnection, CommandType.Text, "select Name As Tech_Manager from Lib_VesselLib_Systems_Parameters where Parent_Code=1"); 
        }
        public DataSet ddlVesselName()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "Select  Vessel_Code, Vessel_Name From Lib_Vessels Where Active_Status = 1 Order by Vessel_Name");
        }
   
    }
}
