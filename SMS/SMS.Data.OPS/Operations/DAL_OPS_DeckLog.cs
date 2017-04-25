using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.Operations
{
    public class DAL_OPS_DeckLog
    {

        private static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;



        public static DataTable Get_DeckLogBook_Index(int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
       
                new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@VESSELID",VesselID),
                new System.Data.SqlClient.SqlParameter("@FromDate",dtFrom),
                new System.Data.SqlClient.SqlParameter("@ToDate",dtTo),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }



        public static DataTable Get_DeckLogBook_Details(int LogBookID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LogBookID",LogBookID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Details_Search", obj);
            return ds.Tables[0];
        }



        public static DataTable Get_DeckLogBook_List(int LogBookID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LogBookID",LogBookID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),

            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_List", obj);
            return ds.Tables[0];
        }

        public static DataTable Get_DeckLogBook_Wheel_Look_Out_Details(int LogBookID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LogBookID",LogBookID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Wheel_And_Look_Out_Search", obj);
            return ds.Tables[0];
        }



        public static DataTable Get_DeckLogBook_Water_In_Hold_Details(int LogBookID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LogBookID",LogBookID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Hold_Search", obj);
            return ds.Tables[0];
        }


        public static DataTable Get_DeckLogBook_Water_In_Tank_Details(int LogBookID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LogBookID",LogBookID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Tank_Search", obj);
            return ds.Tables[0];
        }





        public static DataSet Get_DeckLogBook_Thrashold_List(int Vessel_ID, int? LogBookId, int? Threshold_Id)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@LogBookId",LogBookId),
                    new System.Data.SqlClient.SqlParameter("@Threshold_Id",Threshold_Id),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Threshold_List", obj);
            return ds;
        }


        public static DataTable Get_DeckLogBook_Water_In_Tank_Thrashold_List(int Vessel_ID, int LogBookId)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                 new System.Data.SqlClient.SqlParameter("@LogBookId",LogBookId),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Tank_Threshold_List", obj);
            return ds.Tables[0];


        }

        public static DataTable Get_DeckLogBook_Water_In_Hold_Thrashold_List(int Vessel_ID, int LogBookId)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                 new System.Data.SqlClient.SqlParameter("@LogBookId",LogBookId),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Hold_Threshold_List", obj);
            return ds.Tables[0];


        }





        public static int Insert_DeckLogBook_Thrashold(int? ID,int? Vessel_ID, decimal? MIN_AirTemp, decimal? MAX_AirTemp, decimal? MIN_Barometer, decimal? MAX_Barometer
            , decimal? MIN_Error_Gyro, decimal? MAX_Error_Gyro, decimal? MIN_Error_Standard, decimal? MAX_Error_Standard, decimal? MIN_Sea, decimal? MAX_Sea
            , decimal? MIN_SeaTemp, decimal? MAX_SeaTemp, decimal? MIN_Visibility, decimal? MAX_Visibility, decimal? MIN_Winds_Direction, decimal? MAX_Winds_Direction
            , decimal? MIN_Winds_Force, decimal? MAX_Winds_Force, decimal? MIN_Water_In_Hold_Capacity100, decimal? MAX_Water_In_Hold_Capacity100, 
            decimal? MIN_Water_In_Tank_Capacity100, decimal? MAX_Water_In_Tank_Capacity100, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID",ID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),

                new System.Data.SqlClient.SqlParameter("@MIN_AirTemp",MIN_AirTemp),
                new System.Data.SqlClient.SqlParameter("@MAX_AirTemp",MAX_AirTemp),

                new System.Data.SqlClient.SqlParameter("@MIN_Barometer",MIN_Barometer),
                new System.Data.SqlClient.SqlParameter("@MAX_Barometer",MAX_Barometer),

                new System.Data.SqlClient.SqlParameter("@MIN_Error_Gyro",MIN_Error_Gyro),
                new System.Data.SqlClient.SqlParameter("@MAX_Error_Gyro",MAX_Error_Gyro),

                new System.Data.SqlClient.SqlParameter("@MIN_Error_Standard",MIN_Error_Standard),
                new System.Data.SqlClient.SqlParameter("@MAX_Error_Standard",MAX_Error_Standard),

                new System.Data.SqlClient.SqlParameter("@MIN_Sea",MIN_Sea),
                new System.Data.SqlClient.SqlParameter("@MAX_Sea",MAX_Sea),

                new System.Data.SqlClient.SqlParameter("@MIN_SeaTemp",MIN_SeaTemp),
                new System.Data.SqlClient.SqlParameter("@MAX_SeaTemp",MAX_SeaTemp),

                new System.Data.SqlClient.SqlParameter("@MIN_Visibility",MIN_Visibility),
                new System.Data.SqlClient.SqlParameter("@MAX_Visibility",MAX_Visibility),


                new System.Data.SqlClient.SqlParameter("@MIN_Winds_Direction",MIN_Winds_Direction),
                new System.Data.SqlClient.SqlParameter("@MAX_Winds_Direction",MAX_Winds_Direction),

                new System.Data.SqlClient.SqlParameter("@MIN_Winds_Force",MIN_Winds_Force),
                new System.Data.SqlClient.SqlParameter("@MAX_Winds_Force",MAX_Winds_Force),

                 new System.Data.SqlClient.SqlParameter("@MIN_Water_In_Hold_Capacity100",MIN_Water_In_Hold_Capacity100),
                new System.Data.SqlClient.SqlParameter("@MAX_Water_In_Hold_Capacity100",MAX_Water_In_Hold_Capacity100),


                 new System.Data.SqlClient.SqlParameter("@MIN_Water_In_Tank_Capacity100",MIN_Water_In_Tank_Capacity100),
                new System.Data.SqlClient.SqlParameter("@MAX_Water_In_Tank_Capacity100",MAX_Water_In_Tank_Capacity100),

                new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
                    
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Threshold_Insert", obj);

        }


        public static int Insert_DeckLogBook_Water_In_Hold_Thrashold(int? Vessel_ID, decimal? MIN_Water_In_Hold_Sounding, decimal? MAX_Water_In_Hold_Sounding, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),

                new System.Data.SqlClient.SqlParameter("@MIN_Water_In_Hold_Sounding",MIN_Water_In_Hold_Sounding),
                new System.Data.SqlClient.SqlParameter("@MAX_Water_In_Hold_Sounding",MAX_Water_In_Hold_Sounding),
             
                new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
            
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Hold_Threshold_INSERT", obj);

        }




        public static int Insert_DeckLogBook_Water_In_Tank_Thrashold(int? Vessel_ID, decimal? MIN_Water_In_Tank_Sounding, decimal? MAX_Water_In_Tank_Sounding, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),

                new System.Data.SqlClient.SqlParameter("@MIN_Water_In_Tank_Sounding",MIN_Water_In_Tank_Sounding),
                new System.Data.SqlClient.SqlParameter("@MAX_Water_In_Tank_Sounding",MAX_Water_In_Tank_Sounding),
             
                new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
            
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Tank_Threshold_INSERT", obj);

        }


        public static DataTable Get_DeckLogBook_Values_Changes(int? Vessel_ID, int? LogBook_Dtl_ID, int? LogHours_ID, string Column_Name)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@LogBook_Dtl_ID",LogBook_Dtl_ID),
                new System.Data.SqlClient.SqlParameter("@LogHours_ID",LogHours_ID),
                new System.Data.SqlClient.SqlParameter("@Column_Name",Column_Name),


            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Values_Changes", obj).Tables[0];

        }



        public static DataTable Get_DeckLogBook_Wheel_And_Look_Values_Changes(int? Vessel_ID, int? LookOut_Dtl_ID, int? Log_WATCH_ID, string Column_Name)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@LookOut_Dtl_ID",LookOut_Dtl_ID),
                new System.Data.SqlClient.SqlParameter("@Log_WATCH_ID",Log_WATCH_ID),
                new System.Data.SqlClient.SqlParameter("@Column_Name",Column_Name),


            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Wheel_And_Look_Out_LogChanges", obj).Tables[0];

        }



        public static DataTable Get_DeckLogBook_Water_In_Hold_Values_Changes(int? Vessel_ID, int? WaterInHold_Dtl_ID, int? Hold_Tank_ID, string Column_Name)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@WaterInHold_Dtl_ID",WaterInHold_Dtl_ID),
                new System.Data.SqlClient.SqlParameter("@Hold_Tank_ID",Hold_Tank_ID),
                new System.Data.SqlClient.SqlParameter("@Column_Name",Column_Name),


            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Hold_LogChanges", obj).Tables[0];

        }




        public static DataTable Get_DeckLogBook_Water_In_Tank_Values_Changes(int? Vessel_ID, int? WaterInTank_Dtl_ID, int? Hold_Tank_ID, string Column_Name)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@WaterInTank_Dtl_ID",WaterInTank_Dtl_ID),
                new System.Data.SqlClient.SqlParameter("@Hold_Tank_ID",Hold_Tank_ID),
                new System.Data.SqlClient.SqlParameter("@Column_Name",Column_Name),


            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Water_In_Tank_LogChanges", obj).Tables[0];

        }


        public static DataTable Get_DeckLogBook_Incident_Report_Search(int LogBookID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LogBookID",LogBookID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Incident_Report_Search", obj);
            return ds.Tables[0];
        }



        public static DataSet Get_DeckLogBook_Incident_Participant_Search(int Incident_ID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Incident_ID",Incident_ID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Incident_Participant_Search", obj);

        }



        public static DataSet Get_DeckLogBook_Incident_Participant_Att_Search(int Incident_ID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Incident_ID",Incident_ID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_Book_Incident_Participant_Att_Search", obj);

        }
        public static int CopyDeckLogBookThreshold(int Vessel_ID, int copyfromvesselid, int userid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                 
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@COPYFROMVESSEL_ID",copyfromvesselid),    
                  new System.Data.SqlClient.SqlParameter("@USERID", userid)
                    
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "OPS_PR_DeckLog_COPY_THRESHOLD", obj);

        }

    }
}
