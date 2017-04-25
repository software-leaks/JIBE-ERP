using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;

using SMS.Data;
using SMS.Properties;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_RFQ
    {
        SqlConnection conn;

        /// <summary>
        /// add request for quotation
        /// </summary>
        /// <param name="RequestID">Request id</param>
        /// <param name="AgentID">Agent Id</param>
        /// <param name="QuoteBy">Quote due date</param>
        /// <param name="CreatedBy">user who has created this record</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Add_RFQ(int RequestID, int AgentID, string QuoteBy, int CreatedBy)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList =  { new SqlParameter("@RequestID", RequestID), 
                                       new SqlParameter("@AgentID", AgentID),
                                       new SqlParameter("@QuoteBy", QuoteBy),
                                       new SqlParameter("@userid", CreatedBy)};

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Add_RFQ", pList);
                return true;
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        /// <summary>
        /// Add flight options to the travel request
        /// </summary>
        /// <param name="RequestID">RequestID from main travel request</param>
        /// <param name="AgentID">Agent ID, who is adding the quotations</param>
        /// <param name="GDSLocator">Amadeus Locator /  pnr</param>
        /// <param name="TicketingDeadline">deadline to issue the ticket</param>
        /// <param name="Fare">total fare</param>
        /// <param name="Tax">total tax</param>
        /// <param name="Remarks">any remarks for the option</param>
        /// <param name="CreatedBy">username who created this create</param>
        /// <returns>Quotation id on success, throw exception otherwise</returns>
        public int Add_Quotation(int RequestID, int AgentID, string GDSLocator, DateTime TicketingDeadline,
                decimal Fare, decimal Tax, string Remarks, string sCurrency, int CreatedBy, string TimeHours, string TimeMins, int Quote_ID, decimal? Baggage_Charge, decimal? Date_Change_Charge, decimal? Cancellation_Charge)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter pramaout = new SqlParameter("@QuoteID", Quote_ID);
                pramaout.Direction = ParameterDirection.InputOutput;

                SqlParameter[] pList =  { new SqlParameter("@RequestID", RequestID), 
                                       new SqlParameter("@AgentID", AgentID),
                                       new SqlParameter("@GDSLocator", GDSLocator),
                                       new SqlParameter("@TicketingDeadline", TicketingDeadline),
                                       new SqlParameter("@Fare", Fare),
                                       new SqlParameter("@Tax", Tax),
                                       new SqlParameter("@Remarks", Remarks),
                                       new SqlParameter("@Currency", sCurrency),
                                       new SqlParameter("@userid", CreatedBy),
                                       new SqlParameter("@TimeHours", TimeHours),
                                       new SqlParameter("@TimeMins", TimeMins),
                                       new SqlParameter("@Baggage_Charge", Baggage_Charge),
                                       new SqlParameter("@Date_Change_Charge", Date_Change_Charge),
                                       new SqlParameter("@Cancellation_Charge", Cancellation_Charge),
                                        pramaout            
                                      };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ADD_Quotation", pList);
                return Convert.ToInt32(pramaout.Value);
            }
            catch { throw; }
        }

        /// <summary>
        /// Add Flights to the flight quotation/option
        /// </summary>
        /// <param name="QuoteID">QuoteId/Option id to which flights are added / deleted</param>
        /// <param name="AirlineLocator">Airline locator / pnr</param>
        /// <param name="TravelFrom">travel from location</param>
        /// <param name="Destination">travel to location</param>
        /// <param name="FlightName">flight name</param>
        /// <param name="FlightNo">flight no</param>
        /// <param name="TravelClass">class of travel</param>
        /// <param name="DepartureDate">flight departure date and time</param>
        /// <param name="ArrivalDate">flight arrival date and time</param>
        /// <param name="Status">flight booking status confirm/waitlist</param>
        /// <param name="Remarks">remarks if any</param>
        /// <param name="CreatedBy">user who has created this record</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Add_QuotationFlights(int QuoteID, string AirlineLocator, string TravelFrom, string Destination,
            string FlightName, string FlightNo, string TravelClass, DateTime DepartureDate, DateTime? ArrivalDate,
            string Status, string Remarks, int CreatedBy,
            string TimeArrHours, string TimeArrMins, string TimeDepHours, string TimeDepMins, int Flights_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList =  { new SqlParameter("@QuoteID", QuoteID),
                                           new SqlParameter("@AirlineLocator", AirlineLocator),
                                           new SqlParameter("@TravelFrom", TravelFrom),
                                           new SqlParameter("@Destination", Destination),
                                           new SqlParameter("@FlightName", FlightName),
                                           new SqlParameter("@FlightNo", FlightNo),
                                           new SqlParameter("@TravelClass", TravelClass),
                                           new SqlParameter("@DepartureDate", DepartureDate),
                                           new SqlParameter("@TimeDepHours", TimeDepHours),
                                           new SqlParameter("@TimeDepMins", TimeDepMins),
                                           new SqlParameter("@ArrivalDate", ArrivalDate),                                                                                  
                                           new SqlParameter("@TimeArrHours", TimeArrHours),
                                           new SqlParameter("@TimeArrMins", TimeArrMins),
                                           new SqlParameter("@Status", Status),
                                           new SqlParameter("@Remarks", Remarks),
                                           new SqlParameter("@userid", CreatedBy),
                                           new SqlParameter("@Flights_ID",Flights_ID)        
                                        };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_ADD_QuotationFlights", pList);
                return true;
            }
            catch { throw; }
        }


        /// <summary>
        /// Update Quotation
        /// </summary>
        /// <param name="ID">Quotation id</param>
        /// <param name="GDSLocator">Amadeus Locator /  pnr</param>
        /// <param name="Status">current booking status (waitlist or confirmed) </param>
        /// <param name="Fare">total fare</param>
        /// <param name="Tax">total tax</param>
        /// <param name="TicketingDeadline">deadline to issue the ticket</param>
        /// <returns>true on success, false otherwise</returns>
        /// 

        public Boolean Update_Quotation(int ID, string GDSLocator, decimal Fare, decimal Tax,
            DateTime TicketingDeadline, string Currency, int updated_by, string Remarks, int TimeHours, int TimeMins)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList =  {  new SqlParameter("@ID", ID),
                                           new SqlParameter("@GDSLocator", GDSLocator),
                                           new SqlParameter("@TicketingDeadline", TicketingDeadline),
                                           new SqlParameter("@Fare", Fare),
                                           new SqlParameter("@Tax", Tax),
                                           new SqlParameter("@Currency", Currency),
                                           new SqlParameter("@updated_by", updated_by),
                                           new SqlParameter("@Remarks", Remarks),
                                           new SqlParameter("@TimeHours", TimeHours),
                                           new SqlParameter("@TimeMins", TimeMins)
                                       };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Update_Quotation", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Delete quotation submited by agent
        /// </summary>
        /// <param name="ID">Quoatation id to be deleted</param>
        /// <returns>true on successfull, false otherwise</returns>
        public Boolean Delete_Quotation(int ID, int Deleted_By)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = {new SqlParameter("@ID", ID),
                    new SqlParameter("@Deleted_By", Deleted_By)
                };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Delete_Quotation", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Get quotations for as request by agent id
        /// </summary>
        /// <param name="AgentID">Agent ID</param>
        /// <param name="RequestID">Request id</param>
        /// <returns>dataset with quotations</returns>
        public DataSet Get_Quotation(int AgentID, int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtQuotation = new DataTable();
            DataTable dtQuotationFlight = new DataTable(); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] pList = { new SqlParameter("@RequestID", RequestID),
                                       new SqlParameter("@AgentID", AgentID)};

                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Quotations", pList);
                Ds.Tables[0].TableName = "Quotation";

                Ds.Relations.Add("QuotationFlight", Ds.Tables[0].Columns["ID"], Ds.Tables[1].Columns["QuoteID"]);
                return Ds;
            }
            catch { throw; }
        }


        public DataSet Get_Quotation_Details_DL(int RequestID, int QuoteID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            SqlParameter[] pList = { new SqlParameter("@RequestID", RequestID),
                                       new SqlParameter("@QuoteID", QuoteID)};

            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Quotation_Details", pList);
        }

        public DataSet Get_Quotation_Ticket_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtQuotation = new DataTable();
            DataTable dtQuotationFlight = new DataTable(); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] pList = { new SqlParameter("@RequestID", RequestID)
                                       };

                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Quotations_Ticket", pList);



                return Ds;
            }
            catch { throw; }
        }


        /// <summary>
        /// Get quotation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet Get_Quotation_ByID(int id)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter pList = new SqlParameter("@ID", id);

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_QuotationByID", pList);
            }
            catch { throw; }
        }

        /// <summary>
        /// Get flight details of a quotation
        /// </summary>
        /// <param name="QuoteID">quote id</param>
        /// <returns>Dataset containing flight details</returns>
        public DataSet Get_Quotation_Flights_By_QuoteID(int QuoteID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter pList = new SqlParameter("@QuoteID", QuoteID);

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_QuotationFlight", pList);
            }
            catch { throw; }
        }

        /// <summary>
        /// Get Travel request by request ID for evaluation
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns>Return dataset</returns>
        public DataSet GET_QuotationForEvaluation(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtQuotes = new DataTable();
            DataTable dtLags = new DataTable(); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter sqlprm = new SqlParameter("@RequestID", RequestID);
                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_QuoateForEvaluation", sqlprm);
                Ds.Tables[0].TableName = "dtQuotes";

                Ds.Relations.Add("RequestQuotes", Ds.Tables[0].Columns["id"], Ds.Tables[1].Columns["QuoteID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }


        public DataSet GetCheapestOptions(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter sqlprm = new SqlParameter("@RequestID", RequestID);
                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_GET_CHEAPEST_OPTIONS", sqlprm);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }



        /// <summary>
         
        /// </summary>
        /// <param name="requestid">requestid for the the quotation is submited</param>
        /// <param name="agentid">agentid who is submiting the qoute</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean SendQuotation(int id, int UserName)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = {new SqlParameter("@id", id),
                                       new SqlParameter("@userid", UserName)
                                       };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Send_Quotation", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// approve quotation
        /// </summary>
        /// <param name="requestid">request id to be approved</param>
        /// <param name="quoteid">quote id to be approved</param>
        /// <param name="optionid">option id from the quote to be approved</param>
        /// <param name="approvedby">approver's name</param>
        /// <param name="approverremarks">approver's remaks</param>
        /// <returns>true on success, false otherwise.</returns>
        public int Approve_Quotation(int requestid, int quoteid, int approvedby, string approverremarks, out int Mail_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = {    new SqlParameter("@RequestID", requestid),
                                            new SqlParameter("@quoteid", quoteid),
                                            new SqlParameter("@approvedby", approvedby),
                                            new SqlParameter("@approverremarks", approverremarks),
                                            new SqlParameter("@CRW_DTL_CREW_MAILID",SqlDbType.Int),
                                            new SqlParameter("@Return", SqlDbType.Int),
                                       
                                       };
                pList[pList.Length - 1].Direction = ParameterDirection.ReturnValue;
                pList[pList.Length - 2].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Approve_Quotation", pList);
                Mail_ID = Convert.ToInt32(pList[pList.Length - 2].Value);
                return Convert.ToInt32(pList[pList.Length - 1].Value);

            }
            catch { throw; }
        }

        /// <summary>
        /// Update Flight
        /// </summary>
        /// <param name="id">id of flight need to be updated</param>
        /// <param name="AirlineLocator">Airline locator / pnr</param>
        /// <param name="TravelFrom">travel from location</param>
        /// <param name="Destination">travel to location</param>
        /// <param name="FlightName">flight name</param>
        /// <param name="FlightNo">flight no</param>
        /// <param name="TravelClass">class of travel</param>
        /// <param name="DepartureDate">flight departure date and time</param>
        /// <param name="ArrivalDate">flight arrival date and time</param>
        /// <param name="Status">flight booking status confirm/waitlist</param>
        /// <param name="Remarks">remarks if any</param>
        /// <param name="CreatedBy">user who has created this record</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Update_Quote_Flight(int ID, string AirlineLocator, string TravelFrom, string Destination,
            string FlightName, string FlightNo, string TravelClass, DateTime DepartureDate, DateTime ArrivalDate,
            string Status, string Remarks, int CreatedBy,
            string TimeArrHours, string TimeArrMins, string TimeDepHours, string TimeDepMins)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList =  { new SqlParameter("@id", ID),
                                           new SqlParameter("@AirlineLocator", AirlineLocator),
                                           new SqlParameter("@TravelFrom", TravelFrom),
                                           new SqlParameter("@Destination", Destination),
                                           new SqlParameter("@FlightName", FlightName),
                                           new SqlParameter("@FlightNo", FlightNo),
                                           new SqlParameter("@TravelClass", TravelClass),
                                           new SqlParameter("@DepartureDate", DepartureDate),
                                           new SqlParameter("@TimeDepHours", TimeDepHours),
                                           new SqlParameter("@TimeDepMins", TimeDepMins),
                                           new SqlParameter("@ArrivalDate", ArrivalDate),                                                                                  
                                           new SqlParameter("@TimeArrHours", TimeArrHours),
                                           new SqlParameter("@TimeArrMins", TimeArrMins),                                 
                                           new SqlParameter("@Status", Status),
                                           new SqlParameter("@Remarks", Remarks),
                                           new SqlParameter("@userid", CreatedBy)                                                   
                                        };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Update_QuotationFlight", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Delete quoted flight
        /// </summary>
        /// <param name="id">flight id</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Delete_QuotationFlight(int id, int Deleted_By)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = {new SqlParameter("@id", id),
                                       new SqlParameter("@Deleted_By", Deleted_By)
                                       };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Delete_QuotationFlight", pList);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// for user to update their preference for option
        /// </summary>
        /// <param name="QuoteID">Quote id for which preference has to be set</param>
        /// <param name="PreferenceBy">userid, setting up the preference</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Update_User_Preference(int RequestID, int QuoteID, int Preference_By)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] pList = {    new SqlParameter("@RequestID", RequestID),
                                           new SqlParameter("@QuoteID", QuoteID),
                                       new SqlParameter("@Preference_By", Preference_By)
                                       };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Update_Preference", pList);
                return true;
            }
            catch { throw; }

        }

        public decimal Get_Approveal_Limit_DL(int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "CRW_TRV_GET_APPROVAL_LIMIT", new SqlParameter("@LogedInUserID", UserID)));
        }

        public DataTable Get_Qtn_Approver_DL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_GET_QTN_Approver").Tables[0];
        }

        public DataTable Get_Qtn_Approver_DeptMgr_DL(int RequestID, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] pList = {    
                                       new SqlParameter("@RequestID", RequestID),
                                       new SqlParameter("@UserID", UserID)
                                   };

            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_GET_QTN_Approver_DeptMgr", pList).Tables[0];
        }

        public DataTable Get_Pending_Manager_Approval_DL(int RequestID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] pList = {    
                                       new SqlParameter("@RequestID", RequestID),
                                       
                                   };

            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Pending_Manager_Approval", pList).Tables[0];
        }



        public int Insert_Approval_Entry_DL(int LogedInUser_ID, int Approver_ID, decimal Approval_Amount, int Request_ID, string Purchaser_Remark, string Approver_Remark, int ForApproval = 1, int? ApprovedQuoteID = null)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] pList = {    new SqlParameter("@LogInUserID", LogedInUser_ID),
                                        new SqlParameter("@ApproverID", Approver_ID),
                                        new SqlParameter("@ApprovalAmt", Approval_Amount),
                                        new SqlParameter("@Request_ID",Request_ID),
                                        new SqlParameter("@Purchaser_Remark",Purchaser_Remark),
                                        new SqlParameter("@Approver_Remark",Approver_Remark),
                                        new SqlParameter("@ForApproval",ForApproval),
                                        new SqlParameter("@ApprovedQuoteID",ApprovedQuoteID)
                                       };
            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_INS_Approvals_Entry", pList);
        }

        public DataTable Get_Approval_History_DL(int Request_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] pList = {    new SqlParameter("@RequestID", Request_ID),
                               
                                       };
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_GET_Approval_History", pList).Tables[0];
        }


        public DataSet Get_Request_Ticket_Details(int Request_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);

            SqlParameter[] pList = {    new SqlParameter("@RequestID", Request_ID),
                               
                                       };
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Request_Ticket_Details", pList);
        }

        public DataSet Get_Quotation_Additional_Charge_DL(int QuotationRequest_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_Quotation_Additional_Charge", new SqlParameter("@QuotationRequest_ID", QuotationRequest_ID));
        }

        public int Upd_Quotation_Additional_Charge_DL(int QuotationRequest_ID, DataTable tbl_Items, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@QuotationRequest_ID", QuotationRequest_ID), new SqlParameter("@tbl_Items", tbl_Items), new SqlParameter("@UserID", UserID) };
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_Upd_Quotation_Additional_Charge", prm);

        }



        public int Upd_Quotation_Additional_Charge_Item_DL(int ID, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@ID", ID), new SqlParameter("@UserID", UserID) };
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_Upd_Quotation_Additional_Charge_Item", prm);

        }

        public DataTable Get_Agents_By_Request_DL(int Request_ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_GET_Agents_By_Request", new SqlParameter("@Request_ID", Request_ID)).Tables[0];
        }


        public DataTable Get_PODetails_ByReqID_DL(string Request_ID, string Supplier_Code)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_Get_PODetails_ByReqID", new SqlParameter[] { new SqlParameter("@Request_ID", Request_ID), new SqlParameter("@Supplier_Code", Supplier_Code) }).Tables[0];
        }

        public DataTable Get_Quoted_By_Agent_DL(int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_GET_Quoted_By_Agent", new SqlParameter("@UserID", UserID)).Tables[0];
        }

        public DataTable Get_Requestor_By_Agent_DL(int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_GET_Requestor_By_Agent", new SqlParameter("@UserID", UserID)).Tables[0];

        }


        public DataTable Get_Generate_PO_PDF(string Order_Code)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "TRV_GET_Generate_PO_PDF", new SqlParameter("@DocumentCode", Order_Code)).Tables[0];

        }


    }
}
