using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//custome defined libraries
using SMS.Data;
using SMS.Properties;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_Request
    {
        SqlConnection conn;

        /// <summary>
        /// return list of fleets
        /// </summary>
        /// <returns></returns>
        public DataSet GetFleetList()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString.ToString());
            try
            {
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "SP_INF_Get_FleetList");
            }
            catch { throw; }
            finally { conn = null; }
        }

        /// <summary>
        /// returns list of vessels
        /// </summary>
        /// <param name="FleetCode">fleetcode for which vessel list is required.</param>
        /// <returns></returns>
        public DataSet GetVesselList(int @FleetCode)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString.ToString());
            try
            {
                SqlParameter sqlprm = new SqlParameter("@FleetCode", FleetCode);
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "SP_INF_Get_VesselList", sqlprm);
            }
            catch { throw; }
            finally { conn = null; }
        }

        /// <summary>
        /// Create new travel request
        /// </summary>
        /// <param name="request">Object containing all necessary values to create travel request</param>
        /// <returns></returns>
        public int ADD_TravelRequest(TRV_Request request, int Vessel_ID, int ReqID = 0, string Change_Remark = "")
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter pramaout = new SqlParameter("@ReqID", ReqID);
                pramaout.Direction = ParameterDirection.InputOutput;

                SqlParameter[] pList =  { new SqlParameter("@Staff_ID", request.StaffID), 
                                       new SqlParameter("@Travel_Origin", request.Travel_Origin),
                                       new SqlParameter("@Travel_Destination", request.Travel_Destination),
                                       new SqlParameter("@Departure_Date", request.Departure_Date),
                                       new SqlParameter("@Return_Date", request.Return_Date),
                                       new SqlParameter("@Preferred_Departure_Time", request.Preferred_Departure_Time),
                                       new SqlParameter("@Preferred_Airline", request.Preferred_Airline),
                                       new SqlParameter("@Travel_Class", request.Travel_Class),
                                       new SqlParameter("@Travel_Type", request.Travel_Type),
                                       new SqlParameter("@Is_Seaman_Ticket", request.Is_Seaman_Ticket),
                                       new SqlParameter("@isPersonal_Ticket", request.isPersonal_Ticket),
                                       new SqlParameter("@Remarks", request.Remarks),
                                       new SqlParameter("@userid", request.Created_By),
                                       new SqlParameter("@Vessel_ID",Vessel_ID),
                                       new SqlParameter("@PrefDepHrs",request.PrefDepHrs),
                                       new SqlParameter("@PrefDepMin",request.PrefDepMin),
                                       new SqlParameter("@Change_Remark",Change_Remark),

                                        pramaout
                                      };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ADD_TravelRequest", pList);
                return Convert.ToInt32(pramaout.Value);
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        /// <summary>
        /// Get Current status of request
        /// </summary>
        /// <param name="RequestID">Request id for which status is seed</param>
        /// <param name="AgentID">Agent id if status is request at agent level, 0 otherwise</param>
        /// <returns>string as result</returns>
        public string Get_Current_Status(int RequestID, int AgentID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlDataReader dr;
            string result;
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                        new SqlParameter("@userid", AgentID),
                                    };
                dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_CurrentStatus", sqlprm);
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr.GetString(0);
                }
                else { result = "NA"; }
                return result;
            }
            catch { throw; }
        }

        /// <summary>
        /// Add Passenger to Travel Request
        /// </summary>
        /// <param name="RequestID">Request id in which pax need to be added</param>
        /// <param name="StaffID">Pax id, that need to be added</param>
        /// <param name="CreatedBy">User who added the pax</param>
        /// <returns></returns>
        public Boolean ADD_PAX_IN_TravelRequest(int RequestID, int StaffID, int CreatedBy, int VoyageID, int EventID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                        new SqlParameter("@Staff_ID", StaffID),
                                        new SqlParameter("@userid", CreatedBy),
                                        new SqlParameter("@VoyageID", VoyageID),
                                        new SqlParameter("@EventID", EventID)
                                        };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_AddPaxInTravelRequest", sqlprm);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Add Passenger to Travel Request
        /// </summary>
        /// <param name="RequestID">Request id in which pax need to be added</param>
        /// <param name="StaffID">Pax id, that need to be added</param>
        /// <param name="CreatedBy">User who added the pax</param>
        /// <returns></returns>
        public Boolean Remove_PAX_From_TravelRequest(int RequestPaxId, int Deleted_By)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {new SqlParameter("@RequestPaxId", RequestPaxId),
                                            new SqlParameter("@Deleted_By", Deleted_By)
                                        };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_RemovePaxFromTravelRequest", sqlprm);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Add Flight in Request
        /// </summary>
        /// <param name="RequestID">Request ID</param>
        /// <param name="request">Request object, which contains the flight details in it</param>
        /// <returns></returns>
        public Boolean ADD_FLIGHT_TO_TravelRequest(int RequestID, TRV_Request request)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList =  { new SqlParameter("@RequestID", RequestID), 
                                       new SqlParameter("@Travel_Origin", request.Travel_Origin),
                                       new SqlParameter("@Travel_Destination", request.Travel_Destination),
                                       new SqlParameter("@Departure_Date", request.Departure_Date),
                                       new SqlParameter("@Return_Date", request.Return_Date),
                                       new SqlParameter("@Preferred_Departure_Time", request.Preferred_Departure_Time),
                                       new SqlParameter("@Preferred_Airline", request.Preferred_Airline),
                                       new SqlParameter("@Travel_Class", request.Travel_Class),
                                       new SqlParameter("@isPersonal_Ticket", request.isPersonal_Ticket),
                                       new SqlParameter("@userid", request.Created_By),
                                         new SqlParameter("@PrefDephours", request.PrefDepHrs),
                                           new SqlParameter("@PrefDepmins", request.PrefDepMin)
                                      };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ADD_FlightInTravelRequest", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Get flight details by request id, as requested
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public DataSet Get_Fligts_By_RequestID(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter sqlprm = new SqlParameter("@RequestID", RequestID);

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Flights_ByRequestID", sqlprm);
            }
            catch { throw; }
        }

        /// <summary>
        /// Get Travel request by request ID
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public DataSet GET_TravelRequest_ByID(int RequestID, int AgentID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable();
            DataTable dtPax = new DataTable(); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = {   new SqlParameter("@RequestID", RequestID), 
                                            new SqlParameter("@IS_AGENT", AgentID)
                                      };

                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequestsByID", sqlprm);
                Ds.Tables[0].TableName = "Request";

                Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        /// <summary>
        /// reutrn travel request with pax list in it
        /// </summary>
        /// <returns>Dataset containing relation to Request id and pax in the the travel request</returns>
        public DataSet GET_TravelRequest()
        {
            int rowcount = 0;

            return Search_Travel_Request(0, 0, 0, "", "", "", "", "", "", null, null, null, ref rowcount);
        }

        /// <summary>
        /// Get travel request by agent id
        /// </summary>
        /// <param name="AgentID">AgentID</param>
        /// <returns>Dataset with requests its associated pax</returns>
        public DataSet GET_TravelRequest_By_AgentID(int AgentID, int fleetid, int vesselid, string travelFrom,
            string travelTo, string dateFrom, string dateTo, string name, string status)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable("Request");
            DataTable dtPax = new DataTable("Pax"); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@AgentID", AgentID),
                                          new SqlParameter("@FleetID", fleetid), 
                                       new SqlParameter("@VesselID", vesselid),
                                       new SqlParameter("@sFrom", travelFrom),
                                       new SqlParameter("@sTo", travelTo),
                                       new SqlParameter("@DateFrom", dateFrom),
                                       new SqlParameter("@DateTo", dateTo),
                                       new SqlParameter("@Name", name),
                                       new SqlParameter("@Status", status)
                                    };
                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequests_By_AgentID", sqlprm);
                Ds.Tables[0].TableName = "Request";

                Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
                //return Search_Travel_Request(0, 0, AgentID, "", "", "", "", "", "");
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        public DataSet Search_Travel_Request(int? fleetid, int? vesselid, int? agentid, string travelFrom,
            string travelTo, string dateFrom, string dateTo, string name, string status, int? Approver_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable("Request");
            DataTable dtPax = new DataTable("Pax"); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@FleetID", fleetid), 
                                       new SqlParameter("@VesselID", vesselid),                                                                              
                                       new SqlParameter("@TravelAgent", agentid),
                                       new SqlParameter("@sFrom", travelFrom),
                                       new SqlParameter("@sTo", travelTo),
                                       new SqlParameter("@DateFrom", dateFrom),
                                       new SqlParameter("@DateTo", dateTo),
                                       new SqlParameter("@Name", name),
                                       new SqlParameter("@Status", status),
                                       new SqlParameter("@Approver_ID",Approver_ID),
                                       new SqlParameter("@PAGENUMBER",pagenumber),
                                       new SqlParameter("@PAGESIZE",pagesize),
                                       new SqlParameter("@ISFETCHCOUNT",isfetchcount) 

                                    };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequests", sqlprm);
                isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                Ds.Tables[0].TableName = "Request";

                //Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        public DataSet Search_Travel_Request(int vessel_id, string stage, string request_id, string pax_name, string airport, int agentid, ref int rowcount)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable("Request");
            DataTable dtPax = new DataTable("Pax"); ;
            DataSet Ds = new DataSet();

            try
            {
                SqlParameter[] sqlprm = { 
                                       new SqlParameter("@VesselID", vessel_id),                                                                              
                                       new SqlParameter("@TravelAgent", agentid),
                                       new SqlParameter("@Request_ID", request_id),
                                       new SqlParameter("@sFrom", airport),
                                       new SqlParameter("@sTo", airport),
                                       new SqlParameter("@Name", pax_name),
                                       new SqlParameter("@Status", stage),
                                       new SqlParameter("@ISFETCHCOUNT",rowcount) 

                                    };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequests", sqlprm);
                rowcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                Ds.Tables[0].TableName = "Request";

                Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        public DataSet Get_TravelRequests_Agent_DL(int fleetid, int vesselid, int UserID, string travelFrom,
           string travelTo, string dateFrom, string dateTo, string name, string status, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable("Request");
            DataTable dtPax = new DataTable("Pax"); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@FleetID", fleetid), 
                                       new SqlParameter("@VesselID", vesselid),                                                                              
                                       new SqlParameter("@UserID", UserID),
                                       new SqlParameter("@sFrom", travelFrom),
                                       new SqlParameter("@sTo", travelTo),
                                       new SqlParameter("@DateFrom", dateFrom),
                                       new SqlParameter("@DateTo", dateTo),
                                       new SqlParameter("@Name", name),
                                       new SqlParameter("@Status", status),
                                        new SqlParameter("@PAGENUMBER",pagenumber),
                                       new SqlParameter("@PAGESIZE",pagesize),
                                       new SqlParameter("@ISFETCHCOUNT",isfetchcount) 

                                    };

                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequests_Agent", sqlprm);
                isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                Ds.Tables[0].TableName = "Request";

                Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        public DataSet Get_TravelRequests_Agent_DL(int fleetid, int vesselid, int UserID, string travelFrom,
      string travelTo, string dateFrom, string dateTo, string name, string status, int? pagenumber, int? pagesize, ref int isfetchcount, int? AppCompanyID, int? QuotedBY = null, int? RequestedBy = null)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable("Request");
            DataTable dtPax = new DataTable("Pax"); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@FleetID", fleetid), 
                                       new SqlParameter("@VesselID", vesselid),                                                                              
                                       new SqlParameter("@UserID", UserID),
                                       new SqlParameter("@sFrom", travelFrom),
                                       new SqlParameter("@sTo", travelTo),
                                       new SqlParameter("@DateFrom", dateFrom),
                                       new SqlParameter("@DateTo", dateTo),
                                       new SqlParameter("@Name", name),
                                       new SqlParameter("@Status", status),
                                       new SqlParameter("@AppCompanyID",AppCompanyID),
                                       new SqlParameter("@QuotedBY",QuotedBY),
                                       new SqlParameter("@RequestedBY",RequestedBy),



                                        new SqlParameter("@PAGENUMBER",pagenumber),
                                       new SqlParameter("@PAGESIZE",pagesize),
                                       new SqlParameter("@ISFETCHCOUNT",isfetchcount) 

                                    };

                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequests_Agent", sqlprm);
                isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                Ds.Tables[0].TableName = "Request";

                //Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }


        /// <summary>
        /// Add Remarks
        /// </summary>
        /// <param name="requestid">Request id to add remarks</param>
        /// <param name="remarks">Remarks</param>
        /// <param name="remarkby">username adding remarks</param>
        /// <param name="agentid">agentid if agent is adding this remark</param>
        /// <returns>true on successfull, false otherwise</returns>
        public Boolean Add_Remarks(int requestid, string remarks, int remarkby, int agentid, DataTable RemarkAgentIDs = null)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList =  { new SqlParameter("@RequestID", requestid), 
                                       new SqlParameter("@Remark", remarks),
                                       new SqlParameter("@RemarkBy", remarkby),
                                       new SqlParameter("@AgentID", agentid),
                                       new SqlParameter("@RemarkAgentIDs",RemarkAgentIDs)

                                        };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ADD_Request_Remarks", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Get Remarks
        /// </summary>
        /// <param name="requestid">Request id to get the remark for</param>
        /// <param name="agentid">Agentid to get the remarks specific to that agent/supplier</param>
        /// <returns>Dataset with the result</returns>
        public DataSet Get_Remarks(int RequestID, int AgentID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = { new SqlParameter("@RequestID", RequestID),
                                       new SqlParameter("@AgentID", AgentID)};

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Request_Remarks", pList);
            }
            catch { throw; }
        }

        /// <summary>
        /// Cancel the 
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public Boolean Cancel_TravelRequest(int RequestID, int Deleted_By)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = {new SqlParameter("@RequestID", RequestID),
                                           new SqlParameter("@Deleted_By", Deleted_By)
                                       };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_DEL_Request", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Reset the quotation flag, so that agent/supplier can reqoute for the request
        /// </summary>
        /// <param name="RequestID">Request id that we need to reset for quotation</param>
        /// <param name="AgentID">AgentID for whom we want to allow for requote</param>
        /// <returns>true on successsfull, false otherwise</returns>
        public int Reset_Quotations(int RequestID, int AgentID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = { new SqlParameter("@RequestID", RequestID),
                                       new SqlParameter("@AgentID", AgentID),
                                       new SqlParameter("@return",SqlDbType.Int)
                                       };
                pList[pList.Length - 1].Direction = ParameterDirection.ReturnValue;

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ResetForQuotation", pList);
                return Convert.ToInt32(pList[pList.Length - 1].Value);
            }
            catch { throw; }

        }

        public int Reset_Quotations(int RequestID, int AgentID, int QuoteID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = { new SqlParameter("@RequestID", RequestID),
                                       new SqlParameter("@AgentID", AgentID),
                                       new SqlParameter("@QuoteID",QuoteID),
                                       new SqlParameter("@return",SqlDbType.Int)
                                       };
                pList[pList.Length - 1].Direction = ParameterDirection.ReturnValue;

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ResetForQuotation", pList);
                return Convert.ToInt32(pList[pList.Length - 1].Value);
            }
            catch { throw; }

        }


        public int Update_Travel_Flag(int AgentReqstID, int Userid, int RequsetID = 0)
        {

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = { 
                                           new SqlParameter("@AgentReqstID", AgentReqstID),
                                           new SqlParameter("@UserID", Userid),
                                           new SqlParameter("@RequsetID",RequsetID)
                                       };

                return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_UPDATETRAVEL_FLAG", pList);

            }
            catch { throw; }

        }






        /// <summary>
        /// Get Agetn list to whom RFQ has been sent
        /// </summary>
        /// <param name="RequestID">Request ID</param>
        /// <returns>Agent list as datatable</returns>
        public DataTable Get_Quote_Agent(int RequestID, int Quoted)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                            new SqlParameter("@Quoted", Quoted)
                                        };
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_RFQ_Agents", sqlprm).Tables[0];
            }
            catch { throw; }
        }


        /// <summary>
        /// Get Pax user list for a Request numbe
        /// </summary>
        /// <param name="RequestID">Request ID</param>
        /// <returns>Agent list as datatable</returns>
        public DataTable Get_Pax_Users(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                            
                                        };
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Pax_Users", sqlprm).Tables[0];
            }
            catch { throw; }
        }


        public DataTable GetRoutInfo(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                            
                                        };
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Route_Info", sqlprm).Tables[0];
            }
            catch { throw; }
        }


        public DataTable GetPersonNameToTravelWithinfiveDays(string staffid)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            try
            {

                SqlParameter[] sqlprm = { new SqlParameter("@staffid", staffid),
                                        };

                DataSet ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_CHECK_TRAVEL_WITHIN_FIVE_DAYS", sqlprm);
                return ds.Tables[0];

            }
            catch { throw; }

        }



        /// <summary>
        /// Put the request for Refund.
        /// </summary>
        /// <param name="RequestID">request id to put for refund</param>
        /// <returns>true on successfull</returns>
        public Boolean Refund_Request_DL(int RequestID, int Created_By, string Refund_Remarks)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {new SqlParameter("@RequestID", RequestID),
                                        new SqlParameter("@Created_By", Created_By),
                                        new SqlParameter("@Refund_Remarks", Refund_Remarks),
                                        };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Refund_Request", sqlprm);
                return true;
            }
            catch { throw; }
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
        public int Save_Email(string subject, string mailTO, string MailCC, string body, int userid)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@subject", subject),
                                        new SqlParameter("@mailto", mailTO),
                                        new SqlParameter("@mailCC", MailCC),
                                        new SqlParameter("@body", body),
                                        new SqlParameter("@userid", userid),
                                        };

                return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_SP_CREW_MAIL_SAVE", sqlprm);

            }
            catch { throw; }
        }

        /// <summary>
        /// Update eTicketNumber to the travel request flight
        /// </summary>
        /// <param name="id">flight id for which eTicketNumber has to be updated</param>
        /// <param name="eTicketNumber">eTicketNumber</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Update_eTicketNumber(int RequestID, int Flightid, string eTicketNumber, int updatedby, int PaxID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                        new SqlParameter("@Flightid", Flightid),
                                        new SqlParameter("@eTicketNumber", eTicketNumber),
                                        new SqlParameter("@UpdatedBy", updatedby),
                                        new SqlParameter("@PaxID", PaxID)
                                        };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Update_ETicketNumber", sqlprm);
                return true;

            }
            catch { throw; }
        }

        /// <summary>
        /// Get crew list by event id
        /// </summary>
        /// <param name="EventID">EventId for which crew list is desired</param>
        /// <returns>Dataset containing list of crew</returns>
        public DataSet Get_CrewList_By_EventID(int EventID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter sqlprm = new SqlParameter("@EventID", EventID);
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_CrewByEventID", sqlprm);
            }
            catch { throw; }
        }


        /// <summary>
        /// Get crew list by event id
        /// </summary>
        /// <param name="VoyageID">voyage id for which crew list is desired</param>
        /// <returns>Dataset containing the list of crew</returns>
        public DataSet Get_CrewList_By_VoyageID(int VoyageID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter sqlprm = new SqlParameter("@VoyageID", VoyageID);
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_CrewByVoyageID", sqlprm);
            }
            catch { throw; }
        }

        /// <summary>
        /// Get crew detail
        /// </summary>
        /// <param name="crewid">crew id to get single crew</param>
        /// <param name="userid">userid</param>
        /// <param name="Search_Text">filter text</param>
        /// <returns></returns>
        public DataSet Get_SearchCrew_DL(int crewid, int EventID, int VoyageID, int userid, string Search_Text)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {
                                            new SqlParameter("@Crewid", crewid),
                                            new SqlParameter("@EventID", EventID),
                                            new SqlParameter("@VoyageID", VoyageID),
                                            new SqlParameter("@userid", userid),
                                            new SqlParameter("@SearchText", Search_Text)
                                            
                                        };

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_SearchCrew", sqlprm);
            }
            catch { throw; }
        }

        public DataSet Get_Request_Pax_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter sqlprm = new SqlParameter("@RequestID", RequestID);
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_TravelRequestPax", sqlprm);
            }
            catch { throw; }
        }

        public DataTable Get_ETicket_By_RequestID_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter sqlprm = new SqlParameter("@RequestID", RequestID);
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_ETicketByRequestID", sqlprm).Tables[0];
            }
            catch { throw; }
        }

        public string RequestUserPreference_DL(int RequestID, int UserID, DataTable dtUser)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {
                                            new SqlParameter("@RequestID", RequestID),
                                            new SqlParameter("@UserID", UserID),
                                            new SqlParameter("@tblUserID",dtUser)
                                           
                                        };

                return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_RequestUserPreference", sqlprm).ToString();


            }
            catch { throw; }
        }

        public int UPD_Request_Vessel_DL(int Requset_ID, int Vessel_ID, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] sqlprm = {
                                            new SqlParameter("@REQUEST_ID", Requset_ID),
                                            new SqlParameter("@VESSEL_ID", Vessel_ID),
                                            new SqlParameter("@USEERID",UserID)
                                           
                                        };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_UPD_REQUEST_VESSEL", sqlprm);
        }


        public string UPD_Request_DeptDate_DL(int Requset_ID, DateTime DeptDate, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] sqlprm = {
                                            new SqlParameter("@REQUEST_ID", Requset_ID),
                                            new SqlParameter("@DeptDate", DeptDate),
                                            new SqlParameter("@USEERID",UserID),
                                            new SqlParameter("@outDeptDate",SqlDbType.VarChar,20)
                                           
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_UPD_REQUEST_DeptDate", sqlprm);
            return Convert.ToString(sqlprm[sqlprm.Length - 1].Value);
        }


        public DataSet Get_User_Preference_DL(int RequestID)
        {

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {
                                            new SqlParameter("@RequestID", RequestID),
                                           
                                           
                                        };

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_User_Preference", sqlprm);


            }
            catch { throw; }
        }

        public string Get_Pax_Validation_DL(int RequestID, string Action)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {
                                            new SqlParameter("@RequestID", RequestID),
                                            new SqlParameter("@Action", Action)
                                        };

                SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Pax_Validation ", sqlprm);
                if (dr.HasRows)
                {
                    dr.Read();
                    return dr["result"].ToString();
                }
                else
                    return "Unknown error";
            }
            catch { throw; }
        }


        public DataTable Get_RequestFlight_DL(int Request_Id)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_RequestFlight", new SqlParameter("@Request_ID", Request_Id)).Tables[0];
        }

        public DataSet Get_RequestPax_DL(int Request_Id)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_RequestPax", new SqlParameter("@Request_ID", Request_Id));

        }

        public int RollBack_TravelRequest_DL(int ReqID, string Remark, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] sqlprm = {
                                            new SqlParameter("@ReqID", ReqID),
                                            new SqlParameter("@Change_Remark", Remark),
                                            new SqlParameter("@userid",UserID)
                                        };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_RollBack_TravelRequest", sqlprm);
        }

        public DataTable Get_Travel_Request_Status_DL(int ReqID, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] sqlprm = {
                                            new SqlParameter("@Request_ID", ReqID),
                                            new SqlParameter("@UserID", UserID)
                                            
                                        
                                        };

            
          return  SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Travel_Request_Status", sqlprm).Tables[0];
           
        }

        public int Get_Marked_IsTravelled_DL(int Request_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("Request_ID", Request_ID), new SqlParameter("@retuen", SqlDbType.Int) };
            prm[1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Marked_IsTravelled", prm);
            return Convert.ToInt32(prm[1].Value);
        }

        public int UPD_Quote_Send_For_Approval_DL(int QuoteID, int Status, int RequestID, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] sqlprm = {
                                            new SqlParameter("@QuoteID", QuoteID),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@RequestID",RequestID),
                                            new SqlParameter("@UserID",UserID)
                                                                                    
                                        };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_UPD_Quote_Send_For_Approval", sqlprm);
        }

        public DataTable Get_Request_ApprovalStatus_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Request_ApprovalStatus", new SqlParameter("@RequestID", RequestID)).Tables[0];
        }

        public int UPD_Rework_TravelPIC_DL(string Remark, int RequestID, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] sqlprm = {
                                            new SqlParameter("@Remark",Remark),
                                            new SqlParameter("@RequestID",RequestID),
                                            new SqlParameter("@UserID",UserID)
                                                                                    
                                        };

            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_Rework_TravelPIC", sqlprm);
        }


        public int Get_Quote_Count_Approval_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Quote_Count_Approval", new SqlParameter("@RequestID", RequestID)));
        }

        public DataSet Get_QuoateForApprovals_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_QuoateForApprovals", new SqlParameter("@RequestID", RequestID));
        }

        public int Upd_Approve_TravelPO_Mob_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_UPD_Approve_TravelPO_Mob");
        }
        public DataTable Get_tktBooked_DL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Travel_DashBoard_TktBooked").Tables[0];
        }
        public DataTable Get_tktByVessel_DL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Travel_DashBoard_TktByVessel").Tables[0];
        }
        public DataTable Get_AvgPricePerTicket_DL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Travel_DashBoard_AvgPrice").Tables[0];
           
        }
        public DataTable Get_TotalAmount_DL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Travel_DashBoard_TotalAmount").Tables[0];
        }

        public DataTable Get_TravelStatus(int id)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] sqlprm = {
                                            new SqlParameter("@id",id)
                                            
                                                                                    
                                        };

            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_TravelStatus", sqlprm).Tables[0];
        }
    }
}
