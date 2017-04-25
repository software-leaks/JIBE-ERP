using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Operations;

namespace SMS.Business.Operations
{
    public class BLL_OPS_DeckLog
    {
      
        
        public static DataTable Get_DeckLogBook_Index(int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_OPS_DeckLog.Get_DeckLogBook_Index(FleetID, VesselID, dtFrom, dtTo, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable Get_DeckLogBook_Details(int LogBookID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Details(LogBookID , Vessel_ID);
        }


        public static DataTable Get_DeckLogBook_List(int LogBookID ,int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_List(LogBookID,Vessel_ID);
        }


        public static DataTable Get_DeckLogBook_Wheel_Look_Out_Details(int LogBookID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Wheel_Look_Out_Details(LogBookID, Vessel_ID);
        }


        public static DataTable Get_DeckLogBook_Water_In_Hold_Details(int LogBookID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Water_In_Hold_Details(LogBookID,Vessel_ID);
        }

        public static DataTable Get_DeckLogBook_Water_In_Tank_Details(int LogBookID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Water_In_Tank_Details(LogBookID, Vessel_ID);
        }




        public static DataSet Get_DeckLogBook_Thrashold_List(int Vessel_ID, int? LogBookId, int? Threshold_Id)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Thrashold_List(Vessel_ID, LogBookId, Threshold_Id);
        }


        public static DataTable Get_DeckLogBook_Water_In_Tank_Thrashold_List(int Vessel_ID, int LogBookId)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Water_In_Tank_Thrashold_List(Vessel_ID, LogBookId);
        }

        public static DataTable Get_DeckLogBook_Water_In_Hold_Thrashold_List(int Vessel_ID, int LogBookId)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Water_In_Hold_Thrashold_List(Vessel_ID, LogBookId);
        }


        public static int Insert_DeckLogBook_Thrashold(int? ID,int? Vessel_ID, decimal? MIN_AirTemp, decimal? MAX_AirTemp, decimal? MIN_Barometer, decimal? MAX_Barometer
         , decimal? MIN_Error_Gyro, decimal? MAX_Error_Gyro, decimal? MIN_Error_Standard, decimal? MAX_Error_Standard, decimal? MIN_Sea, decimal? MAX_Sea
         , decimal? MIN_SeaTemp, decimal? MAX_SeaTemp, decimal? MIN_Visibility, decimal? MAX_Visibility, decimal? MIN_Winds_Direction, decimal? MAX_Winds_Direction
         , decimal? MIN_Winds_Force, decimal? MAX_Winds_Force, decimal? MIN_Water_In_Hold_Capacity100, decimal? MAX_Water_In_Hold_Capacity100, decimal? MIN_Water_In_Tank_Capacity100, decimal? MAX_Water_In_Tank_Capacity100,
            int? CreatedBy)
        {

            return DAL_OPS_DeckLog.Insert_DeckLogBook_Thrashold(ID,Vessel_ID, MIN_AirTemp, MAX_AirTemp, MIN_Barometer, MAX_Barometer,
                         MIN_Error_Gyro,MAX_Error_Gyro,MIN_Error_Standard,MAX_Error_Standard, MIN_Sea,MAX_Sea,
                         MIN_SeaTemp, MAX_SeaTemp, MIN_Visibility, MAX_Visibility, MIN_Winds_Direction,MAX_Winds_Direction,
                         MIN_Winds_Force, MAX_Winds_Force, MIN_Water_In_Hold_Capacity100, MAX_Water_In_Hold_Capacity100, MIN_Water_In_Tank_Capacity100, MAX_Water_In_Tank_Capacity100, CreatedBy);
        }

        public static int Insert_DeckLogBook_Water_In_Hold_Thrashold(int? Vessel_ID, decimal? MIN_Water_In_Hold_Sounding, decimal? MAX_Water_In_Hold_Sounding, int? CreatedBy)
        {
            return DAL_OPS_DeckLog.Insert_DeckLogBook_Water_In_Hold_Thrashold(Vessel_ID, MIN_Water_In_Hold_Sounding, MAX_Water_In_Hold_Sounding, CreatedBy);
        }


        public static int Insert_DeckLogBook_Water_In_Tank_Thrashold(int? Vessel_ID, decimal? MIN_Water_In_Tank_Sounding, decimal? MAX_Water_In_Tank_Sounding, int? CreatedBy)
        {
            return DAL_OPS_DeckLog.Insert_DeckLogBook_Water_In_Tank_Thrashold(Vessel_ID, MIN_Water_In_Tank_Sounding, MAX_Water_In_Tank_Sounding, CreatedBy);
        }






        public static DataTable Get_DeckLogBook_Values_Changes(int? Vessel_ID, int? LogBook_Dtl_ID, int? LogHours_ID, string Column_Name)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Values_Changes(Vessel_ID, LogBook_Dtl_ID, LogHours_ID, Column_Name);
        }


        public static DataTable Get_DeckLogBook_Wheel_And_Look_Values_Changes(int? Vessel_ID, int? LookOut_Dtl_ID, int? Log_WATCH_ID, string Column_Name)
        {
             return DAL_OPS_DeckLog.Get_DeckLogBook_Wheel_And_Look_Values_Changes(Vessel_ID,LookOut_Dtl_ID, Log_WATCH_ID, Column_Name);
        }

        public static DataTable Get_DeckLogBook_Water_In_Hold_Values_Changes(int? Vessel_ID, int? WaterInHold_Dtl_ID, int? Hold_Tank_ID, string Column_Name)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Water_In_Hold_Values_Changes(Vessel_ID, WaterInHold_Dtl_ID, Hold_Tank_ID, Column_Name);
        }

        public static DataTable Get_DeckLogBook_Water_In_Tank_Values_Changes(int? Vessel_ID, int? WaterInTank_Dtl_ID, int? Hold_Tank_ID, string Column_Name)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Water_In_Tank_Values_Changes(Vessel_ID, WaterInTank_Dtl_ID, Hold_Tank_ID, Column_Name);
        }


        public static DataTable Get_DeckLogBook_Incident_Report_Search(int LogBookID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Incident_Report_Search(LogBookID, Vessel_ID);
        }



        public static DataSet Get_DeckLogBook_Incident_Participant_Search(int Incident_ID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Incident_Participant_Search(Incident_ID, Vessel_ID);
        
        }

        public static DataSet Get_DeckLogBook_Incident_Participant_Att_Search(int Incident_ID, int Vessel_ID)
        {
            return DAL_OPS_DeckLog.Get_DeckLogBook_Incident_Participant_Att_Search(Incident_ID, Vessel_ID);
        
        }
        public static int CopyDeckLogBookThreshold(int Vessel_ID, int copyfromvesselid, int userid)
        {
            return DAL_OPS_DeckLog.CopyDeckLogBookThreshold(Vessel_ID, copyfromvesselid, userid);

        }

        
    }
}
