using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_TravelRequest
    {
        /// <summary>
        /// return list of fleets
        /// </summary>
        /// <returns>Dataset with list of fleets</returns>
        public DataSet GetFleetList()
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.GetFleetList(); }
            catch { throw; }
        }

        /// <summary>
        /// returns list of vessels
        /// </summary>
        /// <param name="FleetCode">fleetcode for which vessel list is required.</param>
        /// <returns>Dataset as list of vessels</returns>
        public DataSet GetVesselList(int @FleetCode)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.GetVesselList(FleetCode); }
            catch { throw; }
        }

        /// <summary>
        /// Create a new travel request
        /// </summary>
        /// <param name="newRequest">object as new request with all properties set</param>
        /// <returns>New created request id</returns>
        public int CreateTravelRequest(TRV_Request newRequest, int Vessel_ID = 0, int ReqID = 0, string Change_Remark = "")
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.ADD_TravelRequest(newRequest, Vessel_ID, ReqID, Change_Remark); }
            catch { throw; }
        }

        /// <summary>
        /// Get Current status of request
        /// </summary>
        /// <param name="RequestID">Request id for which status is seed</param>
        /// <param name="AgentID">Agent id if status is request at agent level, 0 otherwise</param>
        /// <returns>string as result</returns>
        public string GetCurrentStatus(int RequestID, int AgentID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_Current_Status(RequestID, AgentID); }
            catch { throw; }
        }

        /// <summary>
        /// Add Pax to an exisiting request.
        /// </summary>
        /// <param name="RequestID">Exisitng request id</param>
        /// <param name="StaffID">Staffid to be added to the list</param>
        /// <param name="CreatedBy">Person name adding pax to the request</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean AddPaxToTravelRequest(int RequestID, int StaffID, int CreatedBy, int VoyageID, int EventID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                return TRequest.ADD_PAX_IN_TravelRequest(RequestID, StaffID, CreatedBy, VoyageID, EventID);
            }
            catch { throw; }
        }

        /// <summary>
        /// Remove Pax from an exisiting request.
        /// </summary>
        /// <param name="RequestID">Exisitng request id</param>
        /// <param name="StaffID">Staffid to be added to the list</param>
        /// <param name="RemovedBy">Person name adding pax to the request</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean RemovePaxFromTravelRequest(int RequestPaxId, int Deleted_By)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Remove_PAX_From_TravelRequest(RequestPaxId, Deleted_By); }
            catch { throw; }
        }

        /// <summary>
        /// Add Flights to the existing request
        /// </summary>
        /// <param name="RequestID">Existing request id</param>
        /// <param name="FlightDetail">object wrapped in trv_request class</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean AddFlightToTravelRequest(int RequestID, TRV_Request FlightDetail)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.ADD_FLIGHT_TO_TravelRequest(RequestID, FlightDetail); }
            catch { throw; }
        }


        /// <summary>
        /// Add Flights to the existing request
        /// </summary>
        /// <param name="RequestID">Existing request id</param>
        /// <param name="FlightDetail">object wrapped in trv_request class</param>
        /// <returns>true on success, false otherwise</returns>
        public DataSet GetFlightByRequestID(int RequestID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_Fligts_By_RequestID(RequestID); }
            catch { throw; }
        }


        /// <summary>
        /// Get all travel Requests in a hyrarichel manner with request at top and pax underneath
        /// </summary>
        /// <returns>Dataset with 2 tables. Parent & child. Parent Table = Request, Child table = Pax</returns>
        public DataSet GetRequestList()
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.GET_TravelRequest(); }
            catch { throw; }
        }

        /// <summary>
        /// Get Travel Request by RequestID
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns>Dataset filled with 1 table having travel request details</returns>
        public DataSet GetTravelRequestByID(int RequestID, int AgentID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.GET_TravelRequest_ByID(RequestID, AgentID); }
            catch { throw; }
        }

        ///// <summary>
        ///// Get travel request list by AgentID in a hyrarichel manner with request at top and pax underneath
        ///// </summary>
        ///// <param name="AgentID">AgentID</param>
        ///// <returns>Dataset with 2 tables. Parent & child. Parent Table = Request, Child table = Pax</returns>
        //public DataSet GetTravelRequestByAgentID(int AgentID, int fleetid, int vesselid, string travelFrom,
        //    string travelTo, string dateFrom, string dateTo, string name, string status)
        //{
        //    DAL_TRV_Request TRequest = new DAL_TRV_Request();
        //    try
        //    {
        //        return TRequest.GET_TravelRequest_By_AgentID(AgentID, fleetid, vesselid, travelFrom, travelTo, dateFrom, dateTo, name, status);
        //    }
        //    catch { throw; }
        //}

        /// <summary>
   
        /// </summary>
        /// <param name="fleetid">filter requests by this fleet id</param>
        /// <param name="vesselid">filter requests by this vessel id</param>
        /// <param name="agentid">filter requests for this agent</param>
        /// <param name="travelFrom">filter request for travel place from</param>
        /// <param name="travelTo">filter request for destination</param>
        /// <param name="dateFrom">filter for travel date from</param>
        /// <param name="dateTo">filter for travel date</param>
        /// <param name="name">filter for the selected pax only</param>
        /// <param name="status">filter by status of the request</param>
        /// <returns>Dataset containing all requests</returns>

        public DataSet Get_TravelRequests_Agent(int UserID, int fleetid, int vesselid, string travelFrom,
           string travelTo, string dateFrom, string dateTo, string name, string status, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                //return TRequest.GET_TravelRequest_By_AgentID(AgentID);
                return TRequest.Get_TravelRequests_Agent_DL(fleetid, vesselid, UserID, travelFrom, travelTo, dateFrom, dateTo, name, status, pagenumber, pagesize, ref isfetchcount);
            }
            catch { throw; }
        }

        public DataSet Get_TravelRequests_Agent(int UserID, int fleetid, int vesselid, string travelFrom,
        string travelTo, string dateFrom, string dateTo, string name, string status, int? pagenumber, int? pagesize, ref int isfetchcount, int? AppCompanyID, int? QuotedBY = null, int? RequestedBy = null)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                //return TRequest.GET_TravelRequest_By_AgentID(AgentID);
                return TRequest.Get_TravelRequests_Agent_DL(fleetid, vesselid, UserID, travelFrom, travelTo, dateFrom, dateTo, name, status, pagenumber, pagesize, ref isfetchcount, AppCompanyID, QuotedBY, RequestedBy);
            }
            catch { throw; }
        }

        public DataSet GetRequestList(int? fleetid, int? vesselid, int? agentid, string travelFrom,
            string travelTo, string dateFrom, string dateTo, string name, string status, int? Approver_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                //return TRequest.GET_TravelRequest();
                return TRequest.Search_Travel_Request(fleetid, vesselid, agentid, travelFrom, travelTo, dateFrom, dateTo, name, status, Approver_ID, pagenumber, pagesize, ref isfetchcount);
            }
            catch { throw; }
        }

        public DataSet GetRequestList(int vessel_id, string stage, string request_id, string pax_name, string airport, int agentid, ref int rowcount)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                //return TRequest.GET_TravelRequest();
                return TRequest.Search_Travel_Request(vessel_id, stage, request_id, pax_name, airport, agentid, ref rowcount);
            }
            catch { throw; }
        }

        /// <summary>
        /// Add Remarks in travel request
        /// </summary>
        /// <param name="requestid">Request id to add remarks</param>
        /// <param name="remarks">remarks to be added</param>
        /// <param name="remarkby">username adding remarks</param>
        /// <param name="agentid">agent id if remark is added by agent/supplier</param>
        /// <returns></returns>
        public Boolean AddRemarks(int requestid, string remarks, int remarkby, int agentid, string RemarkAgentIDs = "")
        {

            DataTable dtAgentIDs = new DataTable();
            dtAgentIDs.Columns.Add("TAID");
            DataRow drID;
            foreach (string sid in RemarkAgentIDs.Split(','))
            {
                if (sid.Trim() != "")
                {
                    drID = dtAgentIDs.NewRow();
                    drID["TAID"] = sid;
                    dtAgentIDs.Rows.Add(drID);
                }
            }



            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Add_Remarks(requestid, remarks, remarkby, agentid, dtAgentIDs); }
            catch { throw; }
            finally { TRequest = null; }
        }

        /// <summary>
        /// Get Remarks
        /// </summary>
        /// <param name="requestid">Requestid to get the remarks for</param>
        /// <param name="agentid">AgentID </param>
        /// <returns></returns>
        public DataSet GetRemarks(int requestid, int agentid)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            DataSet ds = new DataSet();
            try
            {
                ds = TRequest.Get_Remarks(requestid, agentid);
                return ds;
            }
            catch { throw; }
            finally { TRequest = null; }
        }

        /// <summary>
        /// Cancels the travel request
        /// </summary>
        /// <param name="RequestID">Requestid to be cancelled</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean CancelRequest(int RequestID, int Deleted_By)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Cancel_TravelRequest(RequestID, Deleted_By); }
            catch { throw; }
            finally { TRequest = null; }
        }

        /// <summary>
        /// Reset the quotation flag, so that agent/supplier can reqoute for the request
        /// </summary>
        /// <param name="RequestID">Request id that we need to reset for quotation</param>
        /// <param name="AgentID">AgentID for whom we want to allow for requote</param>
        /// <returns>true on successsfull, false otherwise</returns>
        public int ResetQuotation(int RequestID, int AgentID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Reset_Quotations(RequestID, AgentID); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public int ResetQuotation(int RequestID, int AgentID, int QuoteID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Reset_Quotations(RequestID, AgentID, QuoteID); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public int Update_Travel_Flag(int AgentReqstID, int Userid, int RequsetID = 0)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();

            try { return TRequest.Update_Travel_Flag(AgentReqstID, Userid, RequsetID); }
            catch { throw; }
            finally { TRequest = null; }

        }


        /// <summary>
        /// Get Agetn list to whom RFQ has been sent
        /// </summary>
        /// <param name="RequestID">Request ID</param>
        /// <returns>Agent list as datatable</returns>
        public DataTable GetQuoteAgents(int RequestID, int Quoted)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_Quote_Agent(RequestID, Quoted); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public DataTable Get_Pax_Users(int RequestID)
        {

            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_Pax_Users(RequestID); }
            catch { throw; }
            finally { TRequest = null; }

        }

        public DataTable GetRoutInfo(int RequestID)
        {

            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.GetRoutInfo(RequestID); }
            catch { throw; }
            finally { TRequest = null; }

        }


        public DataTable GetPersonNameToTravelWithinfiveDays(string staffid)
        {

            DAL_TRV_Request TRequest = new DAL_TRV_Request();

            try
            {
                return TRequest.GetPersonNameToTravelWithinfiveDays(staffid);
            }
            catch { throw; }
            finally { TRequest = null; }

        }


        /// <summary>
        /// Put the request for Refund.
        /// </summary>
        /// <param name="RequestID">request id to put for refund</param>
        /// <returns>true on successfull</returns>
        public Boolean RefundRequest(int RequestID, int Created_By, string Refund_Remarks)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Refund_Request_DL(RequestID, Created_By, Refund_Remarks); }
            catch { throw; }
            finally { TRequest = null; }
        }

        /// <summary>
        /// Save email in database
        /// </summary>
        /// <param name="subject">suject of email</param>
        /// <param name="mailTO">mail to list, separated by commas</param>
        /// <param name="MailCC">mail to cc list, separated by commas</param>
        /// <param name="body">mail body</param>
        /// <param name="userid">user id sending this email</param>
        /// <returns>int, newly created email record id in database</returns>
        public int SaveEmail(string subject, string mailTO, string MailCC, string body, int userid)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Save_Email(subject, mailTO, MailCC, body, userid); }
            catch { throw; }
        }

        /// <summary>
        /// Update eTicketNumber to the travel request flight
        /// </summary>
        /// <param name="id">flight id for which eTicketNumber has to be updated</param>
        /// <param name="eTicketNumber">eTicketNumber</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean UpdateETicketNumber(int RequestID, int Flightid, string eTicketNumber, int updated_by, int PaxID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Update_eTicketNumber(RequestID, Flightid, eTicketNumber, updated_by, PaxID); }
            catch { throw; }
        }


        /// <summary>
        /// Get crew list by event id
        /// </summary>
        /// <param name="EventID">EventId for which crew list is desired</param>
        /// <returns>Dataset containing list of crew</returns>
        public DataSet GetCrewListByEventID(int EventID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_CrewList_By_EventID(EventID); }
            catch { throw; }
            finally { TRequest = null; }
        }

        /// <summary>
        /// Get crew list by event id
        /// </summary>
        /// <param name="VoyageID">voyage id for which crew list is desired</param>
        /// <returns>Dataset containing the list of crew</returns>
        public DataSet GetCrewListByVoyageID(int EventID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_CrewList_By_EventID(EventID); }
            catch { throw; }
            finally { TRequest = null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crewid"></param>
        /// <param name="userid"></param>
        /// <param name="Search_Text"></param>
        /// <returns></returns>
        public DataSet Get_SearchCrew(int crewid, int EventID, int VoyageID, int userid, string Search_Text)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_SearchCrew_DL(crewid, EventID, VoyageID, userid, Search_Text); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public DataSet Get_Request_Pax(int RequestID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_Request_Pax_DL(RequestID); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public DataTable Get_ETicket_By_RequestID(int RequestID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_ETicket_By_RequestID_DL(RequestID); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public string RequestUserPreference(int RequestID, int UserID, DataTable dtUser)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                return TRequest.RequestUserPreference_DL(RequestID, UserID, dtUser);


            }
            catch { throw; }
            finally { TRequest = null; }
        }


        public string RequestUserPreference(int RequestID, int UserID)
        {
            return "";
        }


        public string Get_User_Preference(int RequestID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try
            {
                DataSet ds = TRequest.Get_User_Preference_DL(RequestID);
                StringBuilder strUserID = new StringBuilder();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    strUserID.Append(dr["STAFFID"] + ",");
                }
                if (strUserID.Length > 1)
                {
                    strUserID.Remove(strUserID.Length - 1, 1);
                }
                return strUserID.ToString();

            }
            catch { throw; }
            finally { TRequest = null; }

        }




        public string Get_Pax_Validation(int RequestID, string Action)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            try { return TRequest.Get_Pax_Validation_DL(RequestID, Action); }
            catch { throw; }
            finally { TRequest = null; }
        }

        public int UPD_Request_Vessel(int Requset_ID, int Vessel_ID, int UserID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            return TRequest.UPD_Request_Vessel_DL(Requset_ID, Vessel_ID, UserID);
        }

        public string UPD_Request_DeptDate(int Requset_ID, string DeptDate, int UserID)
        {
            DAL_TRV_Request TRequest = new DAL_TRV_Request();
            return TRequest.UPD_Request_DeptDate_DL(Requset_ID, Convert.ToDateTime(DeptDate), UserID);
        }



        public DataTable Get_RequestFlight(int Request_Id)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_RequestFlight_DL(Request_Id);
        }


        public DataSet Get_RequestPax(int Request_Id)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_RequestPax_DL(Request_Id);
        }

        public int RollBack_TravelRequest(int ReqID, string Remark, int UserID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.RollBack_TravelRequest_DL(ReqID, Remark, UserID);
        }

        public DataTable Get_Travel_Request_Status(int ReqID,int UserID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_Travel_Request_Status_DL(ReqID,UserID);
        }

        public int Get_Marked_IsTravelled(int Request_ID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_Marked_IsTravelled_DL(Request_ID);
        }

        public int UPD_Quote_Send_For_Approval(int QuoteID, int Status, int RequestID, int UserID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.UPD_Quote_Send_For_Approval_DL(QuoteID, Status, RequestID, UserID);
        }

        public DataTable Get_Request_ApprovalStatus(int RequestID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_Request_ApprovalStatus_DL(RequestID);

        }

        public int UPD_Rework_TravelPIC(string Remark, int RequestID, int UserID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.UPD_Rework_TravelPIC_DL(Remark, RequestID, UserID);
        }


        public int Get_Quote_Count_Approval(int RequestID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_Quote_Count_Approval_DL(RequestID);
        }

        public DataSet Get_QuoateForApprovals(int RequestID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_QuoateForApprovals_DL(RequestID);
        }

        public int Upd_Approve_TravelPO_Mob(int RequestID)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Upd_Approve_TravelPO_Mob_DL(RequestID);
        }
        public DataTable Get_tktBooked()
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_tktBooked_DL();
        }
        public DataTable Get_tktByVessel()
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_tktByVessel_DL();
        }
        public DataTable Get_AvgPricePerTicket()
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_AvgPricePerTicket_DL();
        }
        public DataTable Get_TotalAmount()
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_TotalAmount_DL();
        }
         public DataTable Get_TravelStatus(int id)
        {
            DAL_TRV_Request objR = new DAL_TRV_Request();
            return objR.Get_TravelStatus(id);
        }
    }
}
