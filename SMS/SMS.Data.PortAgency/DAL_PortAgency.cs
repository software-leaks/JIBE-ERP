using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.PortAgency
{
    public class DAL_PortAgency
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        
        private string connection = "";
        public DAL_PortAgency(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_PortAgency()
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

        public DataSet GetRequestCount()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DA_SP_GetRequestCount");
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

        public DataTable Get_PortCallAlertList( int? User_ID,  string Order)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@User_Id",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "VM_GET_Port_Call_AlertList", sqlprm).Tables[0];
        }


        public DataTable GetAllPorts()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DA_SP_GetAllPorts").Tables[0];
        }

        public DataTable GetAllVessels()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DA_SP_GetAllVessels").Tables[0];
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



        public DataTable Get_PortCall_List(int? VesselId, string Port_Name, DateTime? Startdate, DateTime? EndDate, bool FilterProformaRequest, bool FilterProformaApproval, bool FilterAdditionalJobs,
            bool FilterAgencyWorkRequest, bool FilterCrewChangeRequest,  string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", VesselId), 
                   new System.Data.SqlClient.SqlParameter("@Port_Name", Port_Name),
                   new System.Data.SqlClient.SqlParameter("@Startdate", Startdate), 
                   new System.Data.SqlClient.SqlParameter("@EndDate", EndDate), 
                   new System.Data.SqlClient.SqlParameter("@FilterProformaRequest", FilterProformaRequest), 
                   new System.Data.SqlClient.SqlParameter("@FilterProformaApproval", FilterProformaApproval), 
                   new System.Data.SqlClient.SqlParameter("@FilterAdditionalJobs", FilterAdditionalJobs), 
                   new System.Data.SqlClient.SqlParameter("@FilterAgencyWorkRequest", FilterAgencyWorkRequest), 
                   new System.Data.SqlClient.SqlParameter("@FilterCrewChangeRequest", FilterCrewChangeRequest), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DA_Get_PortCall_List", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }



    }
}
