using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace SMS.Data.VM.VM
{
    public class DAL_VM_PortCall
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");


        private string connection = "";
        public DAL_VM_PortCall(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_VM_PortCall()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public DataTable Get_PortCall_VesselList(DataTable FleetIDList, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_VesselList", new SqlParameter("FleetIDList", FleetIDList),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel)
                                                                                                ).Tables[0];


        }
        public DataTable Get_PortCall_VesselList(DataTable FleetIDList, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel, int UserID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_VesselList", new SqlParameter("FleetIDList", FleetIDList),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel),
                                                                                                  new SqlParameter("UserID", UserID)
                                                                                                ).Tables[0];


        }
        public DataTable Get_PortCall_List_DL(int Port_Call_ID, int Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_Detail", sqlprm).Tables[0];
        }



        public DataTable Get_PortCall_Search(string searchText, int? Vessel_ID, int? Port_ID, DateTime fromDate, DateTime Todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 
                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Port_ID", Port_ID),
                new System.Data.SqlClient.SqlParameter("@FromDate", fromDate),
                new System.Data.SqlClient.SqlParameter("@ToDate", Todate),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_List_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public DataSet Get_PortCall_Search_DPL(DataTable dtFleet, DataTable dtVessel, DateTime fromDate)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@dtFleet", dtFleet),
                new SqlParameter("@dtVessel", dtVessel),
                 new SqlParameter("@fromDate", fromDate),

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_DPL", sqlprm);
        }
        public DataSet Get_PortCall_Search_List(int? Vessel_ID, int? Port_CallID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Vessel_ID", Vessel_ID),
                 new SqlParameter("@Port_CallID", Port_CallID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_List", sqlprm);
        }
        public int Ins_PortCall_Details_DL(int Port_CallID, int Vessel_ID, int Port_ID, DateTime? Arrival, string Berthing
           , DateTime? Departure, string ArrivalTime, string BerthingTime, string DepTime, string Port_Remarks, string Owners_ID
           , string Charter_ID, int Created_By, int IsWarrisk, int isShipcrane, string PortCallStatus, string PortLocation, bool AutoDate)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Port_CallID",Port_CallID),
                                            new SqlParameter("@Arrival",Arrival),
                                            new SqlParameter("@Berthing",Berthing),
                                            new SqlParameter("@Departure",Departure),
                                             new SqlParameter("@ArrivalTime",ArrivalTime),
                                            new SqlParameter("@BearthingTime",BerthingTime),
                                            new SqlParameter("@DepartureTime",DepTime),

                                            new SqlParameter("@Port_Remarks",Port_Remarks),
                                            new SqlParameter("@Owners_ID",Owners_ID),
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@IsWarRisk",IsWarrisk),
                                            new SqlParameter("@isShipcrane",isShipcrane),
                                            new SqlParameter("@PortCallStatus",PortCallStatus),
                                            new SqlParameter("@PortName",PortLocation),
                                            new SqlParameter("@AutoDate",AutoDate)
                                        };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "VM_INS_Port_Call_DETAILS", sqlprm);
        }
        public int Ins_PortCall_Details_DL(int? Port_CallID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Port_CallID",Port_CallID),
                                            
                                        };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "VM_SMS_INS_Port_Call_DETAILS", sqlprm);
        }
        public int Upd_PortCall_Details_DL(int Port_CallID, int Vessel_ID, int Port_ID, DateTime? Arrival, string Berthing
           , DateTime? Departure, string ArrivalTime, string BerthingTime, string DepTime, string Port_Remarks, string Owners_ID
           , string Charter_ID, int Created_By, int IsWarrisk, int isShipcrane, string PortCallStatus, string PortLocation, bool AutoDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Port_CallID",Port_CallID),
                                            new SqlParameter("@Arrival",Arrival),
                                            new SqlParameter("@Berthing",Berthing),
                                            new SqlParameter("@Departure",Departure),
                                             new SqlParameter("@ArrivalTime",ArrivalTime),
                                            new SqlParameter("@BearthingTime",BerthingTime),
                                            new SqlParameter("@DepartureTime",DepTime),

                                            new SqlParameter("@Port_Remarks",Port_Remarks),
                                            new SqlParameter("@Owners_ID",Owners_ID),
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@IsWarRisk",IsWarrisk),
                                            new SqlParameter("@isShipcrane",isShipcrane),
                                            new SqlParameter("@PortCallStatus",PortCallStatus),
                                            new SqlParameter("@PortName",PortLocation),
                                            new SqlParameter("@AutoDate",AutoDate)
                                        };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "VM_UPD_Port_Call_DETAILS", sqlprm);

        }
        public int Update_PortCall(int Port_CallID, int Vessel_ID, string Charter_ID, string Charter_Agent, string Owner_ID, string Port_Remarks, int Created_By, string PortCallStatus)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Port_CallID",Port_CallID),
                                            new SqlParameter("@Port_Remarks",Port_Remarks),
                                            new SqlParameter("@Owners_ID",Owner_ID),
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            new SqlParameter("@Charter_Agent",Charter_Agent),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@PortCallStatus",PortCallStatus),
                                          
                                        };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "VM_UPDATE_Port_Call_DETAILS", sqlprm);

        }

        public int Update_PortCallLinkPort(int Port_CallID, string Port_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Port_CallID",Port_CallID),
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Created_By",Created_By)
                                          
                                        };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "VM_UPDATE_Port_Call_LinkPort", sqlprm);

        }

        public int Del_PortCall_Details_DL(int Port_Call_ID, int Vessel_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Port_Call_ID", Port_Call_ID),
                                        new SqlParameter("@Vessel_ID", Vessel_ID),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_DEL_Port_Call_DETAILS", sqlprm);

        }
        public int Update_PortCall_Details_AutoDate(int Port_Call_ID, int Vessel_ID, string Autodate, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Port_Call_ID", Port_Call_ID),
                                        new SqlParameter("@Vessel_ID", Vessel_ID),
                                        new SqlParameter("@Autodate", Autodate),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Update_Port_Call_AutoDate", sqlprm);

        }

        public int Update_PortCall_Details_MoveDPLSeq(int Direction, int Vessel_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Direction", Direction),
                                        new SqlParameter("@Vessel_ID", Vessel_ID),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Update_Port_Call_DPLSeq", sqlprm);
        }
        public DataTable Get_PortCall_PortList(int Port_Call_ID, int? Vessel_ID, int Type)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Type",Type),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_PortList", sqlprm).Tables[0];
        }
        public int Ins_Copy_PortCall_Details(int Vessel_ID, int Port_ID, string Port_Name, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Port_Name",Port_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_INS_Copy_Port_Call_DETAILS", sqlprm);
        }
        public DataTable Get_PortCalls(int? Port_Call_ID, int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_LastRecord", sqlprm).Tables[0];
        }
        public DataTable Get_PortCallTemplate(int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_Template", sqlprm).Tables[0];
        }
        public int Upd_PortCall_Template_Details(int Port_CallTemplate_ID, int Vessel_Id, string FromPort, string ToPort, string Seatime, string InPortTime, string Charter_ID, string Owners_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Port_CallTemplate_ID",Port_CallTemplate_ID),
                                            new SqlParameter("@Vessel_Id", Vessel_Id),
                                            
                                            new SqlParameter("@Seatime",Seatime),
                                            new SqlParameter("@InPortTime",InPortTime),
                                            new SqlParameter("@FromPort",FromPort),
                                            new SqlParameter("@ToPort",ToPort),
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            new SqlParameter("@Owners_ID",Owners_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_UPD_Port_Call_Template_DETAILS", sqlprm);

        }

        public int Del_Port_Call_Template(int? Port_CallTemplate_ID, int? Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@PortCall_Template_Id",Port_CallTemplate_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Del_Port_Call_Template", sqlprm);

        }

        public DataTable Get_PortCall_CrewList(int? Vessel_ID, DateTime AsofDate, int? Status)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@Vessel",Vessel_ID),
                                          new SqlParameter("@SingOffDate",AsofDate),
                                          new SqlParameter("@CrewStatus",Status) 
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_CREWList", sqlprm).Tables[0];
        }
        public DataSet Get_PortCall_CrewChange(int? Port_call_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@Port_call_ID",Port_call_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_CrewChange", sqlprm);
        }
        public DataTable Get_PortCallHistory(int Type, int? Port_ID, int? Vessel_ID, DateTime Startdate, DateTime EndDate, string Order)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                             new SqlParameter("@Type",Type),
                                             new SqlParameter("@Startdate",Startdate),
                                             new SqlParameter("@EndDate",EndDate),
                                             new SqlParameter("@Order",Order),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_CallHistory", sqlprm).Tables[0];
        }

        public DataTable Get_PortCallAlertList(int? User_ID, string Order)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@User_Id",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_AlertList", sqlprm).Tables[0];
        }



        public DataSet Get_PortCall_AgentList(int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_AgentList", sqlprm);
        }
        public DataSet Get_PortCall_CharterParty(int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_CharterParty", sqlprm);
        }
        public int Insert_PortCall_Activity(int ActiVityID, int Port_call_ID, int Vessel_ID, DateTime? ActivateDate, string Request_Type, Decimal? Cost, string Desc, int? Created_By, int ActiVityType)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@ActiVityID",ActiVityID),
                                            new SqlParameter("@Port_call_ID",Port_call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),

                                            new SqlParameter("@ActivateDate",ActivateDate),
                                            new SqlParameter("@Request_Type",Request_Type),
                                            new SqlParameter("@Cost",Cost),
                                            new SqlParameter("@Desc",Desc),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@ActiVityType",ActiVityType),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Insert_Port_Call_Activity", sqlprm);

        }
        public DataSet Get_PortCall_ActivitySearch(int? Port_call_ID, int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Port_call_ID",Port_call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_Activity", sqlprm);
        }
        public DataSet Get_PortCall_TaskSearch(int? Port_call_ID, int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Port_call_ID",Port_call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_Plan", sqlprm);
        }
        public DataSet Get_PortCall_PurchaseOrder(int? Port_call_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@Port_call_ID",Port_call_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_PurchaseOrder", sqlprm);
        }
        public DataSet Get_PortCall_ActivityList(int? ActiVityID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@ActiVityID",ActiVityID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_ActiVityList", sqlprm);
        }
        public int Delete_PortCall_ActiVity(int? ActiVityID, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@ActiVityID",ActiVityID),
                                            new SqlParameter("@UserID",UserID),
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Delete_Port_Call_Activity", sqlprm);

        }
        public DataSet Get_PortCall_PortCost(int? PortID, DataTable dtVessel, DateTime Startdate, DateTime EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@PortID",PortID),
                                            new SqlParameter("@dtVessel",dtVessel),
                                            new SqlParameter("@Startdate",Startdate),
                                            new SqlParameter("@EndDate",EndDate)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_PortCost", sqlprm);
        }
        public DataSet Get_PortCall_DAItem(string DA_ID, string Agent_Code, string PortID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@DA_ID",DA_ID),
                                            new SqlParameter("@Agent_Code",Agent_Code),
                                             new SqlParameter("@PortID",PortID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_PortCost_Item", sqlprm);
        }
        public DataSet Get_PortCall_DA(int? Port_call_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@Port_call_ID",Port_call_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_DAPortCost", sqlprm);
        }
        public DataTable Get_PortCall_CrewList_New(int? Vessel_ID, DateTime AsofDate, int? Status)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@Vessel",Vessel_ID),
                                          new SqlParameter("@SingOffDate",AsofDate),
                                          new SqlParameter("@CrewStatus",Status) 
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_CREWList", sqlprm).Tables[0];
        }



        public int Update_Port_Call_Notification(int NotificationID, string Notification_Status, string Notification_Name, DateTime? Start_Date, DateTime? End_Date, string Notification_Description, DataTable dtNotification, int Created_By,
                                               bool IsAllCountries, bool IsAllPorts, bool IsAllVessels, bool IsAllUsers)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@NotificationID",NotificationID),
                                            new SqlParameter("@Notification_Status",Notification_Status),
                                            new SqlParameter("@Notification_Name",Notification_Name),
                                            new SqlParameter("@End_Date",End_Date),
                                            new SqlParameter("@Start_Date",Start_Date),
                                            new SqlParameter("@Notification_Description",Notification_Description),
                                            new SqlParameter("@dtNotification",dtNotification),            
                                            new SqlParameter("@CreatedBy",Created_By),
                                            new SqlParameter("@IsAllCountries",IsAllCountries),
                                            new SqlParameter("@IsAllPorts",IsAllPorts),
                                            new SqlParameter("@IsAllVessels",IsAllVessels),
                                            new SqlParameter("@IsAllUsers",IsAllUsers)

                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Update_Port_Call_Notification", sqlprm);

        }


        public DataTable Get_PortCall_Notification_List(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Port_Call_Notification_List").Tables[0];
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_List", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public DataTable Get_PortCall_Notification_detail(int? Notification_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@NotificationID",Notification_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_Details", sqlprm).Tables[0]; ;
        }

        public DataTable Get_Port_Call_Notification_Report(string Notification_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@Notification_ID",Notification_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_Report", sqlprm).Tables[0]; ;
        }




        public DataTable Get_PortCall_Notification_detail_Ports(int? Notification_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@NotificationID",Notification_ID),
                                           new SqlParameter("@InType",1)
                                              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_Details", sqlprm).Tables[0]; ;
        }

        public DataTable Get_PortCall_Notification_detail_Countries(int? Notification_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@NotificationID",Notification_ID),
                                           new SqlParameter("@InType",2)
                                              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_Details", sqlprm).Tables[0]; ;
        }

        public DataTable Get_PortCall_Notification_detail_Vessels(int? Notification_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@NotificationID",Notification_ID),
                                           new SqlParameter("@InType",3)
                                              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_Details", sqlprm).Tables[0]; ;
        }

        public DataTable Get_PortCall_Notification_detail_Users(int? Notification_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@NotificationID",Notification_ID),
                                           new SqlParameter("@InType",4)
                                              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_Get_Port_Call_Notification_Details", sqlprm).Tables[0]; ;
        }

        public int Del_PortCall_Notification(int? Notification_ID, int? Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Notification_ID", Notification_ID),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Delete_Port_Call_Notification", sqlprm);

        }

        public int Update_Port_Call_Alert(int? Notification_Id, int? Port_Call_Id, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Notification_Id",Notification_Id),
                                            new SqlParameter("@Port_Call_Id",Port_Call_Id),
                                            new SqlParameter("@User_ID",UserID),
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "VM_Update_Port_Call_Alert", sqlprm);

        }

        public DataTable Get_Port_Call_Arrival_Report(int? Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                           new SqlParameter("@VesselId",Vessel_ID)
                                              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_VesselArrival_Report", sqlprm).Tables[0]; ;
        }
        public DataSet Get_WarRisk_Ports()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_VM_Get_WarRisk_Ports");
        }
        public int Get_WarRisk_PortsUpdate(int? Lport, string val)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                                          new SqlParameter("@Lport",Lport),
                                          new SqlParameter("@Val",val),
                                         
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_VM_Get_WarRisk_PortsUpdate", sqlprm);

        }
        public DataTable GET_Port_Call_DPL_Details(int Vessel_Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                                          new SqlParameter("@Vessel_Id",Vessel_Id)
                                         
                                         
                                         };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_DPL_Details", sqlprm).Tables[0];

        }
        public DataTable GetSupplier_List(string sType)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@Supplier_Type", sType) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GetSupplier_List", sqlprm).Tables[0];
        }
    }
}
