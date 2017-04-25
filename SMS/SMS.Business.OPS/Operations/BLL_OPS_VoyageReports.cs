using System;
using System.Data;
using SMS.Data.Operation;


namespace SMS.Business.Operation
{
    public class BLL_OPS_VoyageReports
    {
        DAL_OPS_VoyageReports objDAL = new DAL_OPS_VoyageReports();

        public BLL_OPS_VoyageReports()
        {

        }        
       public static DataTable Get_BunkerReportIndex(int VesselID, int PortId, string FromDate, string ToDate, int FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {
            DateTime fromdt = DateTime.Parse(FromDate);
            DateTime todt = DateTime.Parse(ToDate);
            return DAL_OPS_VoyageReports.Get_BunkerReportIndex_DL(VesselID, PortId, fromdt, todt, FleetID, Page_Index, Page_Size, ref Record_Count, Sort_Column, Sort_Direction);
        }
       public static DataTable Get_BunkerReportIndex(int VesselID, int PortId, string sFromDate, string sToDate, int FleetID)
        {
            DateTime? FromDate;
            DateTime? ToDate;

            if (sFromDate == "")
            {
                FromDate = null;
            }
            else
            {
                FromDate = DateTime.Parse(sFromDate);
            }
            if (sToDate == "")
            {
                ToDate = null;
            }
            else
            {
                ToDate = DateTime.Parse(sToDate);
            }

             return DAL_OPS_VoyageReports.Get_BunkerReportIndex_DL(VesselID, PortId, FromDate, ToDate, FleetID);
        }
        public static DataTable Get_BunkerReport(int BunkerReportId,int VesselId)
        {
            return DAL_OPS_VoyageReports.Get_BunkerReport_DL(BunkerReportId, VesselId);
        }

        public static DataTable Get_VoyageReportIndex(string ReportType, int VesselID, int LocationCode, string sFromDate, string sToDate, int FleetID)
        {
            DateTime? FromDate;
            DateTime? ToDate;

            if (sFromDate == "")
            {
                FromDate = null;
            }
            else
            {
                FromDate = DateTime.Parse(sFromDate);
            }
            if (sToDate == "")
            {
                ToDate = null;
            }
            else
            {
                ToDate = DateTime.Parse(sToDate);
            }

           return DAL_OPS_VoyageReports.Get_VoyageReportIndex_DL(ReportType, VesselID, LocationCode, FromDate, ToDate, FleetID);

        }

        public static DataTable Get_VoyageReportIndex(string ReportType, int VesselID, int LocationCode, string FromDate, string ToDate, int FleetID, int? Page_Index, int? Page_Size, ref int Record_Count, string Sort_Column, string Sort_Direction)
        {

            DateTime fromdt = DateTime.Parse(FromDate);
            DateTime todt = DateTime.Parse(ToDate);

            return DAL_OPS_VoyageReports.Get_VoyageReportIndex_DL(ReportType, VesselID, LocationCode, fromdt, todt, FleetID, Page_Index, Page_Size,ref Record_Count, Sort_Column, Sort_Direction);

        }
        public static DataTable OPS_Get_VesselInfo(int Vessel_ID)
        {
            return DAL_OPS_VoyageReports.OPS_Get_VesselInfo_DL(Vessel_ID);
        }
        public static DataTable Get_ArrivalReport(int PKID)
        {
            return DAL_OPS_VoyageReports.Get_ArrivalReport_DL(PKID);
        }

        public static DataTable Get_DepartureReport(int PKID)
        {
            return DAL_OPS_VoyageReports.Get_DepartureReport_DL(PKID);
        }

        public static DataTable Get_DepartureReport_LOSamples(int Telegram_ID, int Vessel_ID)
        {
            return DAL_OPS_VoyageReports.Get_DepartureReport_LOSamples_DL(Telegram_ID, Vessel_ID);
        }

        public static DataTable Get_DailyNoonReport(int PKID)
        {
            return DAL_OPS_VoyageReports.Get_DailyNoonReport_DL(PKID);
        }

        public static DataTable OPS_SP_Get_CPEntries(int Datatype, int Vesselid, int Userid, int CurrentSTS, int Fleetid)
        {
            return DAL_OPS_VoyageReports.OPS_SP_Get_CPEntries_DL(Datatype, Vesselid, Userid, CurrentSTS, Fleetid);
        }

        public static DataTable OPS_SP_Get_CPEntriesType()
        {
            return DAL_OPS_VoyageReports.OPS_SP_Get_CPEntriesType_DL();
        }

        public static int OPS_SP_Ins_CPEntries(int Datatype, int Vesselid, int Userid, decimal Datavalue)
        {
            return DAL_OPS_VoyageReports.OPS_SP_Ins_CPEntries_DL(Datatype, Vesselid, Userid, Datavalue);
        }

        public static DataTable OPS_Get_RecentCPROB()
        {
            return DAL_OPS_VoyageReports.OPS_Get_RecentCPROB_DL();
        }

        public static DataTable Get_LatestNoonReport(string Vessel_Code)
        {
            return DAL_OPS_VoyageReports.Get_LatestNoonReport_DL(Vessel_Code);
        }

        public static DataTable OPS_Get_CPDtaType()
        {
            return DAL_OPS_VoyageReports.OPS_Get_CPDtaType_DL();
        }
        public static int OPS_Ins_CPDtaType(string DataType, string DataCode)
        {
            return DAL_OPS_VoyageReports.OPS_Ins_CPDtaType_DL(DataType, DataCode);
        }

        public static DataTable Get_VoyageReport(string ReportType, int VesselID, int LocationCode, string FromDate, string ToDate, int FleetID)
        {

            DateTime fromdt = DateTime.Parse(FromDate);
            DateTime todt = DateTime.Parse(ToDate);

            return DAL_OPS_VoyageReports.Get_VoyageReport_DL(ReportType, VesselID, LocationCode, fromdt, todt, FleetID);

        }
        public static DataTable Get_Telegram_ToMail()
        {
            return DAL_OPS_VoyageReports.Get_Telegram_ToMail_DL();

        }

        public static DataTable Get_Purplefinder_Position(int PKID)
        {
            return DAL_OPS_VoyageReports.Get_Purplefinder_Position_DL(PKID);
        }

        public static DataTable Get_Bilge_Water_Sludge(int PkID)
        {
            return DAL_OPS_VoyageReports.Get_Bilge_Water_Sludge_DL(PkID);
        }
        public DataTable OPS_SP_Get_CPEntries(int? Datatype, int? Vesselid, int? Userid, int CurrentSTS, int? Fleetid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Get_OPS_SP_Get_CPEntries_DL(Datatype, Vesselid, Userid, CurrentSTS, Fleetid,  sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataSet GET_PORT_REPORT(int PortReportId, int Vessel_ID)
        {
            return DAL_OPS_VoyageReports.GET_PORT_REPORT(PortReportId, Vessel_ID);
        }
        public static DataTable Get_PortReportIndex(int Vessel_ID, DateTime? Fromdate, DateTime? Todate, string Sort_Column, string Sort_Direction, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {
            return DAL_OPS_VoyageReports.Get_PortReportIndex(Vessel_ID, Fromdate, Todate, Sort_Column, Sort_Direction, Page_Index, Page_Size, ref Is_Fetch_Count);
        }

        public static DataTable Get_PRT_Attachment(int Page_Type_Id, int Page_Id,int Vessel_Id)
        {
            return DAL_OPS_VoyageReports.Get_PRT_Attachment_DL(Page_Type_Id, Page_Id, Vessel_Id);
        }

        public static int Send_PortTerminalInfoReportToVessel(DataTable dtVesselList, int VesselID, int PortInfoReportId)
        {
            return DAL_OPS_VoyageReports.Send_PortTerminalInfoReportToVessel_DL(dtVesselList, VesselID, PortInfoReportId);
        }
        public static DataTable Get_PortInfoReportIndex(int VesselID, int PortId, int CountryID,string Terminal,string searchText, string Sort_Column, string Sort_Direction, int? Page_Index, int? Page_Size, ref int Record_Count)
        {
            return DAL_OPS_VoyageReports.Get_PortInfoReportIndex_DL(VesselID, PortId, CountryID,Terminal,searchText, Sort_Column, Sort_Direction, Page_Index, Page_Size, ref Record_Count);
        }
        public static DataSet Get_PortTerminalInfoReport(int VesselID, int PortInfoReportId,int Office_Id)
        {
            return DAL_OPS_VoyageReports.Get_PortTerminalInfoReport_DL(VesselID, PortInfoReportId,Office_Id);
        }
        public static DataSet Get_PortInfoReport(int VesselID, int PortInfoReportId)
        {
            return DAL_OPS_VoyageReports.Get_PortInfoReport_DL(VesselID, PortInfoReportId);
        }
        public static DataSet Get_TerminalInfoReport(int VesselID, int PortInfoReportId)
        {
            return DAL_OPS_VoyageReports.Get_TerminalInfoReport_DL(VesselID, PortInfoReportId);
        }
        public static DataSet GET_MOOR_Attachment_List(int VesselID, int PortInfoReportId, int Office_Id)
        {
            return DAL_OPS_VoyageReports.GET_MOOR_Attachment_List(VesselID, PortInfoReportId,Office_Id);
        }
        public static DataSet Get_PilotInfo(int VesselID, int PortInfoReportId)
        {
            return DAL_OPS_VoyageReports.Get_PilotInfo_DL(VesselID, PortInfoReportId);
        }
        public static DataSet Get_PilotDetailInfo(int id, int Vessel_Id)
        {
            return DAL_OPS_VoyageReports.Get_PilotDetailInfo_DL(id, Vessel_Id);
        }
        public static int Insert_VoyageNo(string voyageNo, int Userid)
        {
            return DAL_OPS_VoyageReports.OPS_Insert_VoyageNo_DL(voyageNo, Userid);
        }
        public static int Update_VoyageNo(string voyageNo, int Userid, int id)
        {
            return DAL_OPS_VoyageReports.OPS_Update_VoyageNo_DL(voyageNo, Userid, id);
        }

        public static int DeleteRestore_VoyageNo(int Userid, int id)
        {
            return DAL_OPS_VoyageReports.OPS_DeleteRestoreVoyageNo_DL(Userid, id);
        }

        public static int Delete_VoyageNo(int Userid, int id)
        {
            return DAL_OPS_VoyageReports.OPS_Delete_VoyageNo_DL(Userid, id);
        }

        public static DataTable Get_VoyageNo(int id)
        {
            return DAL_OPS_VoyageReports.Get_VoyageNo_DL(id);
        }
        public static DataTable BindVoyageNumber(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_OPS_VoyageReports.BindVoyageNumber_DL(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
    }
}
