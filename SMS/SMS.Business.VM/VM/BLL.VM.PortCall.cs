using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.VM;
using SMS.Data.VM.VM;

namespace SMS.Business.VM
{
    public class BLL_VM_PortCall
    {
        DAL_VM_PortCall objPortCall = new DAL_VM_PortCall();


        //public DAL_VM_PortCall()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        public DataTable Get_PortCall_VesselList(DataTable FleetIDList, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        {
            try
            {
                return objPortCall.Get_PortCall_VesselList(FleetIDList, VesselID, VesselManager, SearchText, UserCompanyID, -1);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_PortCall_VesselList(DataTable FleetIDList, int VesselID, int VesselManager, string SearchText, int UserCompanyID,int UserID)
        {
            try
            {
                return objPortCall.Get_PortCall_VesselList(FleetIDList, VesselID, VesselManager, SearchText, UserCompanyID, -1, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_PortCall_List(int Port_Call_ID, int Vessel_ID)
        {
            return objPortCall.Get_PortCall_List_DL(Port_Call_ID, Vessel_ID);
        }

        public DataTable Get_PortCall_Search(string searchText, int? Vessel_ID, int? Port_ID, DateTime fromDate, DateTime Todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objPortCall.Get_PortCall_Search(searchText, Vessel_ID, Port_ID, fromDate, Todate,sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public DataSet Get_PortCall_Search_DPL(DataTable dtFleet, DataTable dtVessel, DateTime fromDate)
        {
            return objPortCall.Get_PortCall_Search_DPL(dtFleet,dtVessel, fromDate);
        }
        public DataSet Get_PortCall_Search_List(int? Vessel_ID, int? Port_CallID)
        {
            return objPortCall.Get_PortCall_Search_List(Vessel_ID, Port_CallID);
        }

        public int Ins_PortCall_Details(int Port_CallID, int Vessel_ID, int Port_ID, DateTime? Arrival, string Berthing
           , DateTime? Departure, string ArrivalTime, string BerthingTime, string DepTime, string Port_Remarks, string Owners_ID
           , string Charter_ID, int Created_By, int IsWarrisk, int isShipcrane, string PortCallStatus, string PortLocation, bool bAutoDate)
        {

            return objPortCall.Ins_PortCall_Details_DL(Port_CallID, Vessel_ID, Port_ID, Arrival, Berthing, Departure, ArrivalTime, BerthingTime, DepTime, Port_Remarks, Owners_ID,
                Charter_ID, Created_By, IsWarrisk, isShipcrane, PortCallStatus, PortLocation, bAutoDate);
        }

        public int Ins_PortCall_Details(int? Port_CallID)
        {

            return objPortCall.Ins_PortCall_Details_DL(Port_CallID);
        }
        public int Upd_PortCall_Details(int Port_CallID, int Vessel_ID, int Port_ID, DateTime? Arrival, string Berthing
           , DateTime? Departure, string ArrivalTime, string BerthingTime, string DepTime, string Port_Remarks, string Owners_ID
           , string Charter_ID, int Created_By, int IsWarrisk, int isShipcrane, string PortCallStatus, string PortLocation, bool bAutoDate)
        {

            return objPortCall.Upd_PortCall_Details_DL(Port_CallID, Vessel_ID, Port_ID, Arrival, Berthing, Departure, ArrivalTime, BerthingTime, DepTime, Port_Remarks, Owners_ID,
                Charter_ID, Created_By, IsWarrisk, isShipcrane, PortCallStatus, PortLocation, bAutoDate);
        }

        public int Update_PortCall(int Port_CallID, int Vessel_ID, string Charter_ID, string Charter_Agent,  string Owner_ID, string Port_Remarks, int Created_By, string PortCallStatus)
        {

            return objPortCall.Update_PortCall(Port_CallID, Vessel_ID,Charter_ID, Charter_Agent,Owner_ID, Port_Remarks, Created_By, PortCallStatus);
        }
        public int Update_PortCallLink(int Port_CallID, string Port_ID, int Created_By)
        {

            return objPortCall.Update_PortCallLinkPort(Port_CallID, Port_ID, Created_By);
        }
        public int Del_PortCall_Details_DL(int Port_Call_ID, int Vessel_ID, int Created_By)
        {
            return objPortCall.Del_PortCall_Details_DL(Port_Call_ID, Vessel_ID, Created_By);
        }
        public int Update_PortCall_Details_AutoDate(int Port_Call_ID, int Vessel_ID, string Autodate, int Created_By)
        {
            return objPortCall.Update_PortCall_Details_AutoDate(Port_Call_ID, Vessel_ID, Autodate, Created_By);
        }
        public int Update_PortCall_Details_MoveDPLSeq(int Direction, int Vessel_ID, int Created_By)
        {
            return objPortCall.Update_PortCall_Details_MoveDPLSeq(Direction, Vessel_ID, Created_By);
        }
        public DataTable Get_PortCall_PortList(int Port_Call_ID, int? Vessel_ID,int Type)
        {
            return objPortCall.Get_PortCall_PortList(Port_Call_ID, Vessel_ID, Type);
        }
        public int Ins_Copy_PortCall_Details(int Vessel_ID, int Port_ID, string Port_Name, int Created_By)
        {

            return objPortCall.Ins_Copy_PortCall_Details(Vessel_ID, Port_ID,Port_Name, Created_By);
        }
        public DataTable Get_PortCalls(int? Port_Call_ID, int? Vessel_ID)
        {

            return objPortCall.Get_PortCalls(Port_Call_ID, Vessel_ID);
        }
        public DataTable Get_PortCallTemplate(int? Vessel_ID)
        {

            return objPortCall.Get_PortCallTemplate(Vessel_ID);
        }
        public int Upd_PortCall_Template_Details(int Port_CallTemplate_ID, int Vessel_id, string FromPort, string ToPort,string Seatime, string InPortTime, string Charter_ID, string Owners_ID, int Created_By)
        {

            return objPortCall.Upd_PortCall_Template_Details(Port_CallTemplate_ID, Vessel_id, FromPort,  ToPort, Seatime, InPortTime, Charter_ID, Owners_ID, Created_By);
        }

        public int Del_Port_Call_Template(int? Port_CallTemplate_ID, int? Created_By)
        {
            return objPortCall.Del_Port_Call_Template(Port_CallTemplate_ID, Created_By);
        }

        public DataTable Get_PortCall_CrewList(int? Vessel_ID,DateTime AsofDate,int? Status)
        {

            return objPortCall.Get_PortCall_CrewList(Vessel_ID, AsofDate, Status);
        }
        public DataTable Get_PortCallHistory(int Type, int? Port_ID, int? Vessel_ID,DateTime Startdate, DateTime EndDate,string Order)
        {

            return objPortCall.Get_PortCallHistory(Type, Port_ID, Vessel_ID, Startdate, EndDate,Order);
        }

        public DataTable Get_PortCallAlertList(int? User_ID, string Order)
        {

            return objPortCall.Get_PortCallAlertList(User_ID, Order);
        }
        public DataSet Get_PortCall_AgentList(int? Vessel_ID)
        {

            return objPortCall.Get_PortCall_AgentList(Vessel_ID);
        }
        public DataSet Get_PortCall_CharterParty(int? Vessel_ID)
        {

            return objPortCall.Get_PortCall_CharterParty(Vessel_ID);
        }
        public DataSet Get_PortCall_CrewChange(int? Port_call_ID)
        {

            return objPortCall.Get_PortCall_CrewChange(Port_call_ID);
        }
        public int Insert_PortCall_Activity(int ActiVityID, int Port_call_ID, int Vessel_ID, DateTime? ActivateDate, string Request_Type, Decimal? Cost, string Desc, int? Created_By, int ActiVityType)
        {

            return objPortCall.Insert_PortCall_Activity(ActiVityID, Port_call_ID, Vessel_ID, ActivateDate, Request_Type, Cost, Desc, Created_By, ActiVityType);
        }
        public DataSet Get_PortCall_ActivitySearch(int? Port_call_ID, int? Vessel_ID)
        {

            return objPortCall.Get_PortCall_ActivitySearch(Port_call_ID, Vessel_ID);
        }
        public DataSet Get_PortCall_TaskSearch(int? Port_call_ID, int? Vessel_ID)
        {

            return objPortCall.Get_PortCall_TaskSearch(Port_call_ID, Vessel_ID);
        }
        public DataSet Get_PortCall_PurchaseOrder(int? Port_call_ID)
        {

            return objPortCall.Get_PortCall_PurchaseOrder(Port_call_ID);
        }
        public DataSet Get_PortCall_ActivityList(int? ActiVityID)
        {

            return objPortCall.Get_PortCall_ActivityList(ActiVityID);
        }
        public int Delete_PortCall_ActiVity(int? ActiVityID,int UserID)
        {

            return objPortCall.Delete_PortCall_ActiVity(ActiVityID, UserID);
        }
        public DataSet Get_PortCall_PortCost(int? PortID, DataTable dtVessel,DateTime Startdate, DateTime EndDate)
        {

            return objPortCall.Get_PortCall_PortCost(PortID, dtVessel, Startdate, EndDate);
        }
        public DataSet Get_PortCall_DAItem(string DA_ID, string Agent_Code, string PortID)
        {

            return objPortCall.Get_PortCall_DAItem(DA_ID, Agent_Code, PortID);
        }
        public DataSet Get_PortCall_DA(int? Port_call_ID)
        {

            return objPortCall.Get_PortCall_DA(Port_call_ID);
        }
        public  DataTable Get_PortCall_CrewList_New(int? Vessel_ID, DateTime AsofDate, int? Status)
        {

            return objPortCall.Get_PortCall_CrewList_New(Vessel_ID, AsofDate, Status);
        }

        public DataTable Get_PortCall_Notification_List(string searchtext
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objPortCall.Get_PortCall_Notification_List(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public int Update_Port_Call_Notification(int NotificationID, string Notification_Status, string Notification_Name, DateTime? Start_Date, DateTime? End_Date, string Notification_Description, DataTable dtNotification, int Created_By, bool IsAllCountries, bool IsAllPorts, bool IsAllVessels, bool IsAllUsers)
        {

            return objPortCall.Update_Port_Call_Notification(NotificationID, Notification_Status, Notification_Name, Start_Date, End_Date, Notification_Description, dtNotification, Created_By, IsAllCountries, IsAllPorts, IsAllVessels, IsAllUsers);
        }

        public DataTable Get_PortCall_Notification_detail(int? NotificationId)
        {
            return objPortCall.Get_PortCall_Notification_detail(NotificationId);
        }

        public DataTable Get_PortCall_Notification_detail_Ports(int? NotificationId)
        {
            return objPortCall.Get_PortCall_Notification_detail_Ports(NotificationId);
        }
        public DataTable Get_PortCall_Notification_detail_Countries(int? NotificationId)
        {
            return objPortCall.Get_PortCall_Notification_detail_Countries(NotificationId);
        }
        public DataTable Get_PortCall_Notification_detail_Vessels(int? NotificationId)
        {
            return objPortCall.Get_PortCall_Notification_detail_Vessels(NotificationId);
        }
        public DataTable Get_PortCall_Notification_detail_Users(int? NotificationId)
        {
            return objPortCall.Get_PortCall_Notification_detail_Users(NotificationId);
        }

        public DataTable Get_Port_Call_Notification_Report(string NotificationId)
        {
            return objPortCall.Get_Port_Call_Notification_Report(NotificationId);
        }

        public int Del_PortCall_Notification(int? NotificationId, int? Created_By)
        {
            return objPortCall.Del_PortCall_Notification(NotificationId, Created_By);
        }

        public int Update_Port_Call_Alert(int? Notification_Id, int?Port_Call_Id, int UserID)
        {
            return objPortCall.Update_Port_Call_Alert(Notification_Id, Port_Call_Id, UserID);
        }
        public DataTable Get_Port_Call_Arrival_Report(int? VesselId)
        {
            return objPortCall.Get_Port_Call_Arrival_Report(VesselId);
        }
        public DataSet Get_WarRisk_Ports()
        {
            try
            {
                return objPortCall.Get_WarRisk_Ports();
            }
            catch
            {
                throw;
            }
        }
        public int Get_WarRisk_PortsUpdate(int? Lport, string val)
        {
            try
            {
                return objPortCall.Get_WarRisk_PortsUpdate(Lport, val);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GET_Port_Call_DPL_Details(int Vessel_Id)
        {
            try
            {
                return objPortCall.GET_Port_Call_DPL_Details(Vessel_Id);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAgent_List()
        {
            return objPortCall.GetSupplier_List("A");
        }

    }
}
