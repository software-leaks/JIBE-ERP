using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.Operation
{
    public class DAL_OPS_VoyageReports
    {
        //IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");



        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_OPS_VoyageReports(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_OPS_VoyageReports()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }       
        public static DataTable Get_BunkerReportIndex_DL(int? VesselID, int? PortId, DateTime? Fromdate, DateTime? Todate, int? FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {
            SqlParameter[] parm = new SqlParameter[] 
            { 
               
                new SqlParameter("@BUNKERPORTID", PortId),
                 new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@FLEETID",FleetID),
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@SORT_COLUMN",Sort_Column),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@return",SqlDbType.Int)

            };
            parm[parm.Length - 1].Direction = ParameterDirection.ReturnValue;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_BunkerReportIndex", parm).Tables[0];
            Record_Count = Convert.ToInt32(parm[parm.Length - 1].Value);
            return dt;
        }

        public static DataTable Get_BunkerReportIndex_DL(int VesselID, int PortId, DateTime? Fromdate, DateTime? Todate, int FleetID)
        {
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@BUNKERPORTID", PortId),
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@FLEETID",FleetID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_BunkerReportIndex", parm).Tables[0];
        }

        public static DataTable Get_BunkerReport_DL(int BunkerReportId, int VesselId)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {
                new SqlParameter("@Bunker_Report_ID", BunkerReportId),
                new SqlParameter("@Vessel_ID", VesselId) 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_BunkerReport", parm).Tables[0];
        }


        public static DataTable Get_VoyageReportIndex_DL(string ReportType, int VesselID, int LocationCode, DateTime? Fromdate, DateTime? Todate, int FleetID)
        {



            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REPORTTYPE", ReportType), 
                new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@LOCATION", LocationCode),
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("@FleetID",FleetID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_VoyageReportIndex", parm).Tables[0];
        }

        public static DataTable Get_VoyageReportIndex_DL(string ReportType, int VesselID, int LocationCode, DateTime Fromdate, DateTime Todate, int FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {
            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REPORTTYPE", ReportType), 
                new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@LOCATION", LocationCode),
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("FleetID",FleetID),
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@SORT_COLUMN",Sort_Column),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@return",SqlDbType.Int)

            };
            parm[parm.Length - 1].Direction = ParameterDirection.ReturnValue;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_VoyageReportIndex", parm).Tables[0];
            Record_Count = Convert.ToInt32(parm[parm.Length - 1].Value);
            return dt;
        }
        public static DataTable OPS_Get_VesselInfo_DL(int Vessel_ID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Vessel_ID", Vessel_ID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_VesselInfo", parm).Tables[0];
        }
        public static DataTable Get_ArrivalReport_DL(int PKID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@PKID", PKID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_ArrivalReport", parm).Tables[0];
        }

        public static DataTable Get_DepartureReport_DL(int PKID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@PKID", PKID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_DepartureReport", parm).Tables[0];
        }

        public static DataTable Get_DepartureReport_LOSamples_DL(int Telegram_ID, int Vessel_ID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Telegram_ID", Telegram_ID), new SqlParameter("@Vessel_ID", Vessel_ID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_DepartureReport_LOSamples", parm).Tables[0];
        }

        public static DataTable Get_DailyNoonReport_DL(int PKID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@PKID", PKID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_DailyNoonReport", parm).Tables[0];
        }

        public static DataTable OPS_SP_Get_CPEntries_DL(int Datatype, int Vesselid, int Userid, int CurrentSTS, int Fleetid)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                                                      new SqlParameter("@Datatype", Datatype),
                                                      new SqlParameter("@Vesselid", Vesselid),
                                                      new SqlParameter("@Userid", Userid),
                                                      new SqlParameter("@CurrentSTS",CurrentSTS),
                                                      new SqlParameter("@Fleetid", Fleetid),
                                                     };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_CPEntries", parm).Tables[0];
        }

        public static DataTable OPS_SP_Get_CPEntriesType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_CPEntriesType").Tables[0];
        }

        public static int OPS_SP_Ins_CPEntries_DL(int Datatype, int Vesselid, int Userid, decimal Datavalue)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                                                      new SqlParameter("@Datatype", Datatype),
                                                      new SqlParameter("@Vesselid", Vesselid),
                                                      new SqlParameter("@Userid", Userid),
                                                      new SqlParameter("@Datavalue",Datavalue)
                                                     };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Ins_CPEntries", parm);
        }

        public static DataTable OPS_Get_RecentCPROB_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_RecentCPROB").Tables[0];
        }

        public static DataTable Get_LatestNoonReport_DL(string Vessel_Code)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Vessel_Code", Vessel_Code) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_LatestNoonReport", parm).Tables[0];
        }

        public static DataTable OPS_Get_CPDtaType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_CPDtaType").Tables[0];

        }
        public static int OPS_Ins_CPDtaType_DL(string DataType, string DataCode)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@DataType", DataType), new SqlParameter("@DataCode", DataCode) };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_Ins_CPDtaType", parm);

        }

        public static DataTable Get_VoyageReport_DL(string ReportType, int VesselID, int LocationCode, DateTime Fromdate, DateTime Todate, int FleetID)
        {



            SqlParameter[] parm = new SqlParameter[] 
            { 
                new SqlParameter("@REPORTTYPE", ReportType), 
                new SqlParameter("@VESSEL", VesselID), 
                new SqlParameter("@LOCATION", LocationCode),
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate),
                new SqlParameter("FleetID",FleetID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_VoyageReport", parm).Tables[0];
        }

        public static DataTable Get_Telegram_ToMail_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Telegram_ToMail").Tables[0];
        }

        public static DataTable Get_Purplefinder_Position_DL(int PKID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@PKID", PKID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Purplefinder_Position", parm).Tables[0];
        }

        public static DataTable Get_Bilge_Water_Sludge_DL(int PkID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@PkID", PkID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Bilge_Water_Sludge", parm).Tables[0];
        }
        public DataTable Get_OPS_SP_Get_CPEntries_DL(int? Datatype, int? Vesselid, int? Userid, int CurrentSTS, int? Fleetid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Datatype", Datatype),
                   new System.Data.SqlClient.SqlParameter("@Vesselid", Vesselid),
                   new System.Data.SqlClient.SqlParameter("@Userid", Userid),
                   new System.Data.SqlClient.SqlParameter("@CurrentSTS",CurrentSTS),
                   new System.Data.SqlClient.SqlParameter("@Fleetid", Fleetid),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@RecordCount",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_CPEntries_Serach", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];



        }

        public static DataSet GET_PORT_REPORT(int PortReportId, int Vessel_ID)
        {

            SqlParameter[] parm = new SqlParameter[] 
            { 
               
                new SqlParameter("@PortReportId", PortReportId),  
                 new SqlParameter("@Vessel_ID", Vessel_ID),  
             
              
            };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PORT_REPORT", parm);

            return ds;
        }
        public static DataTable Get_PortReportIndex(int Vessel_ID, DateTime? Fromdate, DateTime? Todate, string Sort_Column, string Sort_Direction, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {

            SqlParameter[] parm = new SqlParameter[] 
            { 
               
                new SqlParameter("@Vessel_ID", Vessel_ID),  
                new SqlParameter("@FROMDATE",Fromdate),
                new SqlParameter("@TODATE",Todate), 
                  new SqlParameter("@Sort_Column",Sort_Column),
                new SqlParameter("@Sort_Direction",Sort_Direction),
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@Is_Fetch_Count",Is_Fetch_Count),
              
            };
            parm[parm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_PortReportIndex", parm).Tables[0];
            Is_Fetch_Count = Convert.ToInt32(parm[parm.Length - 1].Value);
            return dt;
        }
        public static DataTable Get_PRT_Attachment_DL(int Page_Type_Id, int Page_Id, int Vessel_Id)
       {
            SqlParameter[] parm = new SqlParameter[] 
            {               
                new SqlParameter("@Page_Type_Id", Page_Type_Id), 
                new SqlParameter("@Page_Id", Page_Id),  
                new SqlParameter("@Vessel_Id", Vessel_Id), 
            };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_PRT_Attachment_List", parm);

            return ds.Tables[0];
        }

        public static int Send_PortTerminalInfoReportToVessel_DL(DataTable dtVesselList, int VesselID, int PortInfoReportId)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {               
                new SqlParameter("@dtVesselList", dtVesselList), 
                new SqlParameter("@Vessel_Id", VesselID), 
                new SqlParameter("@PortInfoReportId", PortInfoReportId),
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Send_PortTerminalInfoReportToVessel", parm);
        }
        public static DataTable Get_PortInfoReportIndex_DL(int? VesselID, int? PortId, int? CountryId, string Terminal, string searchText, string Sort_Column, string Sort_Direction, int? Page_Index, int? Page_Size, ref int Record_Count)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@PortId", PortId),
                new SqlParameter("@VesselId", VesselID), 
                new SqlParameter("@CountryId",CountryId),
                 new SqlParameter("@Terminal", Terminal), 
                new SqlParameter("@SearchText",searchText),
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@SORT_COLUMN",Sort_Column),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@return",SqlDbType.Int)

            };
            parm[parm.Length - 1].Direction = ParameterDirection.ReturnValue;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PORT_INFO_INDEX_REPORT", parm).Tables[0];
            Record_Count = Convert.ToInt32(parm[parm.Length - 1].Value);
            return dt;
        }
        public static DataSet Get_PortTerminalInfoReport_DL(int VesselID, int PortInfoReportId, int Office_Id)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@PortInfoReportId", PortInfoReportId),
                new SqlParameter("@Vessel_ID", VesselID), 
                 new SqlParameter("@Office_Id", Office_Id), 
             };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_PortTerminalReport", parm);
            return ds;
        }
        public static DataSet Get_PortInfoReport_DL(int VesselID, int PortInfoReportId)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@PortInfoReportId", PortInfoReportId),
                new SqlParameter("@Vessel_ID", VesselID), 
             };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Port_Info", parm);
            return ds;
        }
        public static DataSet Get_TerminalInfoReport_DL(int VesselID, int PortInfoReportId)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@PortInfoReportId", PortInfoReportId),
                new SqlParameter("@Vessel_ID", VesselID), 
             };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Terminal_Info", parm);
            return ds;
        }
        public static DataSet GET_MOOR_Attachment_List(int Vessel_Id, int Page_Id, int Office_Id)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@Vessel_Id", Vessel_Id),  
                new SqlParameter("@Page_Id", Page_Id), 
                  new SqlParameter("@Office_Id", Office_Id), 
            };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_MOOR_FILE_ATTACH", parm);
            return ds;
        }
        public static DataSet Get_PilotInfo_DL(int VesselID, int PortInfoReportId)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@PortInfoId", PortInfoReportId),
                new SqlParameter("@Vessel_ID", VesselID), 
             };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_PTR_GET_PILOT_INFO_LIST", parm);
            return ds;
        }
        public static DataSet Get_PilotDetailInfo_DL(int id, int Vessel_Id)
        {
            SqlParameter[] parm = new SqlParameter[] 
            {                
                new SqlParameter("@PILOTINFOID", id),
                new SqlParameter("@Vessel_ID", Vessel_Id), 
             };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_PTR_GET_PILOT_INFO_DETAIL", parm);
            return ds;
        }
        public static int OPS_Insert_VoyageNo_DL(string voyage_No, int Userid)
        {
            SqlParameter[] parm = new SqlParameter[] {                                                       
                                                      new SqlParameter("@voyage_No", voyage_No),
                                                      new SqlParameter("@Userid", Userid),
                                       
                                                     };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_Insert_Voyage_number", parm);
        }


        public static int OPS_Update_VoyageNo_DL(string voyageNo, int Userid, int Id)
        {
            SqlParameter[] parm = new SqlParameter[] {   
                                                     new SqlParameter("@Id", Id),
                                                      new SqlParameter("@voyage_No", voyageNo),
                                                      new SqlParameter("@Userid", Userid),
                                       
                                                     };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_Update_Voyage_number", parm);
        }

        public static int OPS_DeleteRestoreVoyageNo_DL(int Userid, int Id)
        {
            SqlParameter[] parm = new SqlParameter[] {   
                                                     new SqlParameter("@Id", Id),
                                                     new SqlParameter("@Userid", Userid),
                                       
                                                     };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_DeleteRestore_Voyage_number", parm);
        }

        public static int OPS_Delete_VoyageNo_DL(int Userid, int Id)
        {
            SqlParameter[] parm = new SqlParameter[] {   
                                                     new SqlParameter("@Id", Id),
                                                     new SqlParameter("@Userid", Userid),                                       
                                                     };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_Delete_Voyage_number", parm);
        }
        public static DataTable Get_VoyageNo_DL(int id)
        {
            DataTable dt ;
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                
                new SqlParameter("@ID", id),                
                 
              };
            return dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Voyage_numberByID", obj).Tables[0];
 
        }
        public static DataTable BindVoyageNumber_DL(string searchtext
       , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                
               
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_Get_Voyage_number", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
    }

}
