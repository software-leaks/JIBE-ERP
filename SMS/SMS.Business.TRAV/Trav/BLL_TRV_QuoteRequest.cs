using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_QuoteRequest
    {
        DAL_TRV_RFQ RFQ;

        /// <summary>
        /// Add RFQ
        /// </summary>
        /// <param name="RequestID">Request id</param>
        /// <param name="AgentID">Agent id</param>
        /// <param name="QuoteBy">Quotation due date</param>
        /// <param name="CreatedBy">username adding this RFQ</param>
        /// <returns></returns>
        public Boolean AddRequestForQuote(int RequestID, int AgentID, string QuoteBy, int CreatedBy)
        {
            RFQ = new DAL_TRV_RFQ();
            try { return RFQ.Add_RFQ(RequestID, AgentID, QuoteBy, CreatedBy); }
            catch { throw; }
            finally { RFQ = null; }
        }

        /// <summary>
        /// Quotation submitted by Agent
        /// </summary>
        /// <param name="RequestID">Request id</param>
        /// <param name="AgentID">Agent id who is submitting quotes</param>
        /// <param name="AirlineLocator">airline locator / PNR</param>
        /// <param name="GDSLocator">GDS Locator / PNR</param>
        /// <param name="TravelFrom">Travel origin place</param>
        /// <param name="Destination">Travel destination place</param>
        /// <param name="FlightName">flight name in full</param>
        /// <param name="FlightNo">flight number</param>
        /// <param name="TravelClass">class of travel</param>
        /// <param name="DepartureDate">flight departure date & time</param>
        /// <param name="ArrivalDate">flight arrival date & time</param>
        /// <param name="TicketingDeadline">ticketing deadline</param>
        /// <param name="Status">flight status confirmed/waitlist</param>
        /// <param name="Fare">fare for this quotation / per pax</param>
        /// <param name="Tax">tax for this quotation / per pax</param>
        /// <param name="Remarks">remarks if any</param>
        /// <param name="CreatedBy">agent / user adding this quotation</param>
        /// <returns></returns>
        public int AddQuotation(int RequestID, int AgentID, string GDSLocator,
            DateTime TicketingDeadline, decimal Fare, decimal Tax, string Remarks, string sCurrency, int CreatedBy, string TimeHours, string TimeMins, int Quote_ID, decimal? Baggage_Charge, decimal? Date_Change_Charge, decimal? Cancellation_Charge)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Add_Quotation(RequestID, AgentID, GDSLocator, TicketingDeadline, Fare, Tax,
                    Remarks, sCurrency, CreatedBy, TimeHours, TimeMins, Quote_ID, Baggage_Charge, Date_Change_Charge, Cancellation_Charge);
            }
            catch { throw; }
            finally { RFQ = null; }
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
        /// <returns></returns>
        public Boolean AddQuotationFlights(int QuoteID, string AirlineLocator, string TravelFrom, string Destination,
            string FlightName, string FlightNo, string TravelClass, DateTime DepartureDate, DateTime? ArrivalDate,
            string Status, string Remarks, int CreatedBy,
            string TimeArrHours, string TimeArrMins, string TimeDepHours, string TimeDepMins, int Flights_ID)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Add_QuotationFlights(QuoteID, AirlineLocator, TravelFrom, Destination, FlightName, FlightNo,
                    TravelClass, DepartureDate, ArrivalDate, Status, Remarks, CreatedBy,
                    TimeArrHours, TimeArrMins, TimeDepHours, TimeDepMins, Flights_ID);
            }
            catch { throw; }
            finally { RFQ = null; }
        }

        /// <summary>
        /// Delete quotation
        /// </summary>
        /// <param name="ID">quote id to be deleted</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean DeleteQuotation(int ID, int Deleted_By)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Delete_Quotation(ID, Deleted_By);
            }
            catch { throw; }
            finally { RFQ = null; }
        }

        /// <summary>
        /// Get quotations either by agent id or request id
        /// </summary>
        /// <param name="AgentID">Agent id to get the quotation for</param>
        /// <param name="RequestID">Request id</param>
        /// <returns>Dataset with the quotations</returns>
        public DataSet GetQuotation(int AgentID, int RequestID)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Get_Quotation(AgentID, RequestID);
            }
            catch { throw; }
            finally { RFQ = null; }
        }


        public DataSet Get_Quotation_Details(int RequestID, int QuoteID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Quotation_Details_DL(RequestID, QuoteID);
        }

        public DataSet Get_Quotation_Ticket(int RequestID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Quotation_Ticket_DL(RequestID);
        }
        /// <summary>
        /// Get quotation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetQuotaionByID(int id)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Get_Quotation_ByID(id);
            }
            catch { throw; }
            finally { RFQ = null; }
        }

        /// <summary>
        /// Get flight details of a quotation
        /// </summary>
        /// <param name="QuoteID">quote id</param>
        /// <returns>Dataset containing flight details</returns>
        public DataSet GetQuotationFlightsByQuoteID(int QuoteID)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Get_Quotation_Flights_By_QuoteID(QuoteID);
            }
            catch { throw; }
            finally { RFQ = null; }

        }

        /// <summary>
        /// Get Quotation for evaluation process
        /// </summary>
        /// <param name="requestID">Request id for evaluation</param>
        /// <returns>Dataset with Agent Quotes and Lag details</returns>
        public DataSet GetQuotationForEvaluation(int requestID)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.GET_QuotationForEvaluation(requestID);
            }
            catch { throw; }
            finally { RFQ = null; }
        }


        public DataSet GetCheapestOptions(int requestID)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.GetCheapestOptions(requestID);
            }
            catch { throw; }
            finally { RFQ = null; }
        }



        
        /// <param name="requestid">requestid for the the quotation is submited</param>
        /// <param name="agentid">agentid who is submiting the qoute</param>
        /// <returns>true on successful, false otherwise</returns>
        public Boolean SendQuotaion(int quoteid, int UserName)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.SendQuotation(quoteid, UserName);
            }
            catch { throw; }
            finally { RFQ = null; }

        }

        /// <summary>
        /// Update Quotation
        /// </summary>
        /// <param name="ID">Quotation id</param>
        /// <param name="GDSLocator">Amadeus Locator /  pnr</param>
        /// <param name="Fare">total fare</param>
        /// <param name="Tax">total tax</param>
        /// <param name="TicketingDeadline">deadline to issue the ticket</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean UpdateQuotation(int ID, string GDSLocator, decimal Fare, decimal Tax,
            DateTime TicketingDeadline, string Currency, int udpated_by, string Remarks, int TimeHours, int TimeMins)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Update_Quotation(ID, GDSLocator, Fare, Tax, TicketingDeadline,
                    Currency, udpated_by, Remarks, TimeHours, TimeMins);
            }
            catch { throw; }
            finally { RFQ = null; }
        }

        public Boolean UpdateQuoteFlight(int ID, string AirlineLocator, string TravelFrom, string TravelTo,
            string FlightName, string FlightNo, string TravelClass, DateTime DepartureDate, DateTime ArrivalDate,
            string FlightStatus, string Remarks, int UpdatedBy,
            string TimeArrHours, string TimeArrMins, string TimeDepHours, string TimeDepMins)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Update_Quote_Flight(ID, AirlineLocator, TravelFrom, TravelTo, FlightName,
                    FlightNo, TravelClass, DepartureDate, ArrivalDate, FlightStatus, Remarks, UpdatedBy,
                    TimeArrHours, TimeArrMins, TimeDepHours, TimeDepMins);
            }
            catch { throw; }
            finally { RFQ = null; }
        }

        public Boolean DeleteQuoteFlight(int id, int Deleted_By)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Delete_QuotationFlight(id, Deleted_By);
            }
            catch { throw; }
            finally { RFQ = null; }
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
        public int ApproveQuotation(int requestid, int quoteid, int approvedby, string approverremarks, out int Mail_ID)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Approve_Quotation(requestid, quoteid, approvedby, approverremarks,out Mail_ID);
            }
            catch { throw; }
            finally { RFQ = null; }
        }

        /// <summary>
        /// for user to update their preference for option
        /// </summary>
        /// <param name="QuoteID">Quote id for which preference has to be set</param>
        /// <param name="PreferenceBy">userid, setting up the preference</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean UpdateUserPreference(int RequestID, int QuoteID, int Preference_By)
        {
            RFQ = new DAL_TRV_RFQ();
            try
            {
                return RFQ.Update_User_Preference(RequestID, QuoteID, Preference_By);
            }
            catch { throw; }
            finally { RFQ = null; }
        }
        public decimal Get_Approveal_Limit(int UserID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Approveal_Limit_DL(UserID);
        }
        public DataTable Get_Qtn_Approver()
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Qtn_Approver_DL();

        }
        public DataTable Get_Qtn_Approver_DeptMgr(int RequestID, int UserID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Qtn_Approver_DeptMgr_DL(RequestID, UserID);

        }

        public DataTable Get_Pending_Manager_Approval(int RequestID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Pending_Manager_Approval_DL(RequestID);

        }
        public int Insert_Approval_Entry(int LogedInUser_ID, int Approver_ID, decimal Approval_Amount, int Request_ID, string Purchaser_Remark, string Approver_Remark, int ForApproval = 1, int? ApprovedQuoteID = null)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Insert_Approval_Entry_DL(LogedInUser_ID, Approver_ID, Approval_Amount, Request_ID, Purchaser_Remark, Approver_Remark, ForApproval, ApprovedQuoteID);
        }

        public DataTable Get_Approval_History(int Request_ID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Approval_History_DL(Request_ID);
        }


        public DataSet Get_Request_Ticket_Details(int Request_ID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Request_Ticket_Details(Request_ID);
        }


        public DataTable AddHour()
        {
            DataTable dtHour = new DataTable();
            dtHour.Columns.Add("HrText");
            dtHour.Columns.Add("HrValue");
            DataRow dr = null;
            for (int time = 0; time < 24; time++)
            {
                dr = dtHour.NewRow();
                dr["HrText"] = (time < 10) ? "0" + time.ToString() : time.ToString();
                dr["HrValue"] = time.ToString();
                dtHour.Rows.Add(dr);
            }
            return dtHour;
        }

        public DataTable AddMinute()
        {
            DataTable dtMinute = new DataTable();
            dtMinute.Columns.Add("MnText");
            dtMinute.Columns.Add("MnValue");
            DataRow dr = null;
            for (int mins = 0; mins < 60; mins++)
            {
                dr = dtMinute.NewRow();
                dr["MnText"] = (mins < 10) ? "0" + mins.ToString() : mins.ToString();
                dr["MnValue"] = mins.ToString();
                dtMinute.Rows.Add(dr);
            }

            return dtMinute;
        }

        public DataSet Get_Quotation_Additional_Charge(int QuotationRequest_ID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Quotation_Additional_Charge_DL(QuotationRequest_ID);
        }


        public int Upd_Quotation_Additional_Charge(int QuotationRequest_ID, DataTable tbl_Items, int UserID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Upd_Quotation_Additional_Charge_DL(QuotationRequest_ID, tbl_Items, UserID);
        }

        public int Upd_Quotation_Additional_Charge_Item(int ID, int UserID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Upd_Quotation_Additional_Charge_Item_DL(ID, UserID);
        }
        public DataTable Get_Agents_By_Request(int Request_ID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Agents_By_Request_DL(Request_ID);
        }


        public DataTable Get_PODetails_ByReqID(string Request_ID, string Supplier_Code)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_PODetails_ByReqID_DL(Request_ID, Supplier_Code);
        }


        public DataTable Get_Quoted_By_Agent(int UserID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Quoted_By_Agent_DL(UserID);
        }
        public DataTable Get_Requestor_By_Agent(int UserID)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Requestor_By_Agent_DL(UserID);
        }


        public DataTable Get_Generate_PO_PDF(string Order_Code)
        {
            RFQ = new DAL_TRV_RFQ();
            return RFQ.Get_Generate_PO_PDF(Order_Code);
        }
    }
}
