using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.PortageBill
{
    public class DAL_PB_PortageBill
    {
        private static string connection = "";
        public DAL_PB_PortageBill(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_PB_PortageBill()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_PortageBills_DL(int Fleet_ID, int Vessel_ID, int? Month = null, int? Year = null)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Fleet_ID",Fleet_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Year",Year)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_PortageBills", sqlprm).Tables[0];

        }

        public static DataSet Get_PortageBill(int Month, int Year, int? Vessel_ID, string Search, int? Generate_PBill)
        {
            SqlParameter[] prm = new SqlParameter[]
          {
              new SqlParameter("@Month",Month),
              new SqlParameter("@Year",Year),
              new SqlParameter("@Vessel_ID",Vessel_ID),
              new SqlParameter("@Search",Search),
              new SqlParameter("@GENERATE_PBILL",Generate_PBill)
          };

           DataSet DsPbill=SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PortageBill", prm);
           DsPbill.Tables[0].TableName = "Data";
           DsPbill.Tables[1].TableName = "Comment";
           DsPbill.Tables[2].TableName = "EntryType";
           return DsPbill;

        }

        public static DataTable Get_PortageBills_DL(int Fleet_ID, int Vessel_ID, int? Month, int? Year, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {

            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Fleet_ID",Fleet_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@Page_Index",Page_Index),
                                            new SqlParameter("@Page_Size",Page_Size),
                                            new SqlParameter("@is_Fetch_Count",is_Fetch_Count)
                                        };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_PortageBills", prm).Tables[0];

            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value.ToString());
            return dt;
        }

        public static string Get_LastPortageBillDate_DL(int Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ACC_SP_Get_LastPortageBillDate", sqlprm).ToString();

        }
        public static DataSet Get_PortageBill_Details_DL(int Vessel_ID, string Month, string Year, int Prepared_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@syear",Year),                                            
                                            new SqlParameter("@smonth",Month),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@uid",Prepared_By)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_GetPortage_Bill_Monthly_Office", sqlprm);

        }


        public static DataSet Get_CapCashReport_DL(string month, string year, string vessel)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                        new SqlParameter("@month",SqlDbType.Int) , 
                        new SqlParameter("@year",SqlDbType.Int) ,
                        new SqlParameter("@vessel",SqlDbType.Int)
            };
            obj[0].Value = month;
            obj[1].Value = year;
            obj[2].Value = vessel;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_CaptCashReports", obj);
        }

        public static DataSet LatestReportID_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, "select max(ID),vessel_id from ACC_DTL_CAPTCASH group by vessel_id");
        }
        public static DataSet ReportDetailsByID_DL(string Id, string Vessel)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                        new SqlParameter("@Vessel",SqlDbType.Int) ,
                         new SqlParameter("@Id",SqlDbType.Int)  
            };
            obj[0].Value = Vessel;
            obj[1].Value = Id;

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_CaptCashfollwUP", obj);
        }

        public static int AddNew_Allotment_DL(int CrewID, int VoyageID, int Vessel_ID, int BankAccID, decimal Amount, string PBill_Date, int UserID, int is_special_alt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),                                            
                                            new SqlParameter("@VoyageID",VoyageID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@BankAcc",BankAccID),
                                            new SqlParameter("@Amount",Amount),
                                            new SqlParameter("@Date",PBill_Date),
                                            new SqlParameter("@Created_By",UserID),
                                            new SqlParameter("@is_special_alt",is_special_alt),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_INSERT_Allotment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public static DataTable Get_Allotments_DL(int FleetID, int Vessel_ID, string Month, string Year, int Approve_Status, int BankID, int Sent_Status, string SearchText, int? CrewID, int? Verification_sts, int? AllotmentID, int? Amountvalue = null, bool FlagedItems = false, int Country_ID = 0)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetID",FleetID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@Approve_Status",Approve_Status),
                                            new SqlParameter("@BankID",BankID),
                                            new SqlParameter("@Sent_Status",Sent_Status),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@AllotmentID",AllotmentID),
                                            new SqlParameter("@Amountvalue",Amountvalue),
                                            new SqlParameter("@Verification_sts",Verification_sts),
                                            new SqlParameter("@FlagedItems",FlagedItems),
                                            new SqlParameter("@Country_ID",Country_ID)

                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_GetAllotments", sqlprm).Tables[0];

        }

        public static int Verify_Allotment_DL(int Vessel_ID, int AllotmentID, int Verified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_ID",Vessel_ID) ,
                new SqlParameter("@AllotmentID",AllotmentID) ,
                new SqlParameter("@Verified_By",Verified_By),
                new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Verify_Allotment", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public static int INSERT_Allotment_DL(int Vessel_ID, int CrewID, string BankAcc, string CreateDate, int Currency_ID, Decimal Amount, int is_special_alt, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_ID",Vessel_ID) ,
                new SqlParameter("@CrewID",CrewID),  
                new SqlParameter("@BankAcc",BankAcc) ,
                new SqlParameter("@Date",CreateDate) ,
                new SqlParameter("@Currency_ID",Currency_ID) ,
                new SqlParameter("@Amount",Amount) ,
                new SqlParameter("@is_special_alt",is_special_alt) ,
                new SqlParameter("@Created_By",Created_By) ,
                new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_INSERT_Allotment", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public static int Delete_Allotment_DL(int AllotmentID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@AllotmentID",AllotmentID) ,
                new SqlParameter("@Deleted_By",Deleted_By) ,
                new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_DELETE_Allotment", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }


        public static int Delete_Allotment_DL(int AllotmentID, int Vessel_ID, DateTime PBDate, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@AllotmentID",AllotmentID) ,
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@PBDate",PBDate),
                new SqlParameter("@Deleted_By",Deleted_By) ,
                new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_DELETE_Allotment", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public static DataTable Get_CTM_Requests_DL(DataTable FleetID, DataTable Vessel_ID, DateTime? FromDate, DateTime? ToDate, string CTMStatus, string SearchText, int? PendingWithUserID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@FleetID", FleetID),
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@FromDate", FromDate),
                                            new SqlParameter("@ToDate", ToDate),
                                            new SqlParameter("@CTMStatus", CTMStatus),
                                            new SqlParameter("@SearchText", SearchText),
                                            new SqlParameter("@PendingWithUserID", PendingWithUserID),

                                            new SqlParameter("@Page_Index",Page_Index),
                                            new SqlParameter("@Page_Size",Page_Size),
                                            new SqlParameter("@is_Fetch_Count",is_Fetch_Count)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_CTM_Requests", sqlprm).Tables[0];
            is_Fetch_Count = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }

        public static DataSet Get_CTM_Details_DL(int Vessel_ID, int ID, int Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@ID", ID),
                                            new SqlParameter("@Office_ID", Office_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_CTM_Details", sqlprm);
        }

        public static DataTable Get_Cash_On_Board(int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                           
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_Cash_On_Board", sqlprm).Tables[0];
        }

        public static DataTable Get_CTM_Denominations_DL(int Vessel_ID, int CTM_ID, int Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@ID", CTM_ID),
                                            new SqlParameter("@Office_ID", Office_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_CTM_Denominations", sqlprm).Tables[0];
        }

        public static int UPDATE_CTM_Denominations_DL(int Vessel_ID, int CTM_ID, int CTM_Office_ID, int ID, int Notes_Office, int Denomination, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@CTM_ID", CTM_ID),
                                            new SqlParameter("@Office_ID", CTM_Office_ID),
                                            new SqlParameter("@ID", ID),
                                            new SqlParameter("@Notes_Office", Notes_Office),
                                            new SqlParameter("@Denomination", Denomination),
                                            new SqlParameter("@Modified_By", Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_UPDATE_CTM_Denominations", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_CTM_Approval_DL(int Vessel_ID, int CTM_ID, int CTM_Office_ID, decimal ApprovedAmt, int Approved_By, string Remarks, int ApprovalStatus, string Order_Supplier, decimal Supplier_Commission)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@CTM_ID", CTM_ID),
                                            new SqlParameter("@Office_ID", CTM_Office_ID),
                                            new SqlParameter("@ApprovedAmt", ApprovedAmt),
                                            new SqlParameter("@Approved_By", Approved_By),
                                            new SqlParameter("@Remarks", Remarks),
                                            new SqlParameter("@ApprovalStatus", ApprovalStatus),
                                            new SqlParameter("@Order_Supplier",Order_Supplier),
                                            new SqlParameter("@Supplier_Commission",Supplier_Commission),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_UPDATE_CTM_Approval", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_CTM_Port_DL(int Vessel_ID, int CTM_ID, int CTM_Office_ID, string CTM_Date, int CTM_Port, int Modified_By, int PortCall_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@CTM_ID", CTM_ID),
                                            new SqlParameter("@Office_ID", CTM_Office_ID),
                                            new SqlParameter("@CTM_Date", Convert.ToDateTime(CTM_Date)),
                                            new SqlParameter("@CTM_Port", CTM_Port),
                                            new SqlParameter("@Modified_By", Modified_By),
                                            new SqlParameter("@PortCall_ID",PortCall_ID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_UPDATE_CTM_Port", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_CTM_Finalize_DL(int Vessel_ID, int CTM_ID, int CTM_Office_ID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@CTM_ID", CTM_ID),
                                            new SqlParameter("@Office_ID", CTM_Office_ID),
                                            new SqlParameter("@Modified_By", Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_UPDATE_CTM_Finalize", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_VesselMinCTM_DL(int Fleet_ID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Fleet_ID", Fleet_ID),
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@UserID", UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Vessel_MinCTM", sqlprm).Tables[0];
        }

        public static int UPDATE_VesselMinCTM_DL(int Vessel_ID, decimal MinCTM, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@MinCTM", MinCTM),
                                            new SqlParameter("@Modified_By", Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_UPDATE_Vessel_MinCTM", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Crew_Salary_Instructions_DL(int CrewID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@CrewID", CrewID),
                                            new SqlParameter("@VoyageID", VoyageID),
                                            new SqlParameter("@UserID", UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Crew_Salary_Instructions", sqlprm).Tables[0];
        }

        public static int INS_Crew_Salary_Instruction_DL(int CrewID, int Vessel_ID, int Voyage_ID, int Earn_Deduction, DateTime PBill_Date, int SalaryType, decimal Amount, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Voyage_ID",Voyage_ID),
                                            new SqlParameter("@Earn_Deduction",Earn_Deduction),
                                            new SqlParameter("@PBill_Date",PBill_Date),
                                            new SqlParameter("@SalaryType",SalaryType),
                                            new SqlParameter("@Amount",Amount),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Insert_Salary_Instruction", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_Crew_Salary_Instruction_DL(int ID, int CrewID, int Vessel_ID, int Voyage_ID, int Earn_Deduction, string PBill_Date, int SalaryType, decimal Amount, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Voyage_ID",Voyage_ID),
                                            new SqlParameter("@Earn_Deduction",Earn_Deduction),
                                            new SqlParameter("@PBill_Date",PBill_Date),
                                            new SqlParameter("@SalaryType",SalaryType),
                                            new SqlParameter("@Amount",Amount),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Update_Salary_Instruction", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Update_Allotment_DL(int Vessel_ID, int Allotment_ID, decimal Amount, int Account_ID, int Modified_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Allotment_ID",Allotment_ID),
                                            new SqlParameter("@Amount",Amount),
                                            new SqlParameter("@Account_ID",Account_ID),
                                            new SqlParameter("@Modified_BY",Modified_BY),
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_Allotment", sqlprm);
        }


        public static DataSet ACC_Get_CrewWages_ByMonth_DL(int CrewID, int Month, int Year)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID", CrewID),
                                            new SqlParameter("@Month",Month),
                                             new SqlParameter("@Year",Year),
                                         
                                          
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_CrewWages_ByMonth", sqlprm);
        }

        public static DataSet Get_Allotments_ByCrewID_DL(int CrewID, int PBMonth, int PBYear)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID", CrewID),
                                            new SqlParameter("@PBMonth",PBMonth),
                                             new SqlParameter("@PBYear",PBYear),
                                         
                                          
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_Allotments_ByCrewID", sqlprm);
        }

        public static DataTable Get_BOW_Document_DL(int Vessel_ID, DateTime? PBill_Date)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@PBill_Date",PBill_Date)
                                            
                                                                               
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_BOW_DOCUMENT", sqlprm).Tables[0];
        }

        public static DataSet Get_PB_YearMonth_DL(int? Year)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Year",Year)
                                            
                                                                               
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_PB_YearMonth",sqlprm);
        }

        public static int Upd_CTM_Rework_DL(int CTM_ID, int Vessel_ID, string Remark, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Return",SqlDbType.Int)   ,
                                            new SqlParameter("@CTM_ID", CTM_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Remark",Remark),
                                            new SqlParameter("@UserID",UserID)
                                                                                                                       
                                        };
            sqlprm[0].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_CTM_Rework", sqlprm);
            return Convert.ToInt32(sqlprm[0].Value);



        }


        public static void Send_Mail_CTM_Received_DL()
        {
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_Send_Mail_CTM_Received");
        }

        public static DataTable Get_CaptCash_Items_Attachments(int Vessel_ID, int CaptCash_Details_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@CaptCash_Details_ID",CaptCash_Details_ID)
                                                                                  
                                          
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CAPTCASH_ITEMS_ATTACHMENTS", sqlprm).Tables[0];
        }



        public static DataTable Get_Portage_Bill_Attachments(int Vessel_ID, string month ,string year, int Doc_type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID", Vessel_ID),
                                            new SqlParameter("@Month",month),
                                            new SqlParameter("@Year", year),
                                            new SqlParameter("@Doc_Type",Doc_type)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_PORTAGE_BILL_ATTACHMENTS", sqlprm).Tables[0];
        }




        public static DataSet ACC_GET_Allotment_Flag(int Allotment_ID, int Vessel_ID, DateTime PBill_Date)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Allotment_ID", Allotment_ID), 
                                                      new SqlParameter("@Vessel_ID", Vessel_ID), 
                                                      new SqlParameter("@PBill_Date", PBill_Date) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_Allotment_Flag", prm);
        }

        public static int ACC_INS_Allotment_Flag(int Allotment_ID, int Vessel_ID, DateTime PBill_Date, string Remark, int Created_By, DataTable tblAttach, string Flag_Status)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Allotment_ID", Allotment_ID), 
                                                      new SqlParameter("@Vessel_ID", Vessel_ID), 
                                                      new SqlParameter("@PBill_Date", PBill_Date),
                                                      new SqlParameter("@Remark",Remark ),
                                                      new SqlParameter("@Created_By",Created_By ),
                                                      new SqlParameter("@tblAttach",tblAttach ),
                                                      new SqlParameter("@Flag_Status",Flag_Status ),
            
                                                    };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_INS_Allotment_Flag", prm);
        }
        public static string ACC_Get_Allotment_Flag_Mail(int UserID)
        {
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ACC_Get_Allotment_Flag_Mail", new SqlParameter("@USERID", UserID)).ToString();
        }

        public static DataTable Allotment_Flag_Mail_Users(string Search)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_Allotment_Flag_Mail_Users", new SqlParameter("@Search", Search)).Tables[0];
        }

        public static int UPD_Allotment_Flag_Mail_Users(DataTable dtList, int UserID)
        {
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_Allotment_Flag_Mail_Users", new SqlParameter[] { new SqlParameter("@USER_LIST", dtList), new SqlParameter("@USER_ID", UserID) });
        }

        public static int UPD_Save_CTM_Details(int User_ID, SMS.Properties.CTM_Deatils prCTM)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                 new SqlParameter("@BOW_Calculated_Amt",prCTM.BOW_Calculated_Amt ), 
                 new SqlParameter("@Cash_OnBoard",prCTM.Cash_OnBoard ), 
                 new SqlParameter("@CTM_Date",prCTM.CTM_Date ), 
                 new SqlParameter("@CTM_Port",prCTM.CTM_Port ), 
                 new SqlParameter("@CTM_Requested_Amt",prCTM.CTM_Requested_Amt ), 
                 new SqlParameter("@Vessel_ID",prCTM.Vessel_ID ), 
                 new SqlParameter("@CTM_ID", prCTM.CTM_ID),
                 new SqlParameter("@Denomination", prCTM.Denomination), 
                 new SqlParameter("@OffSigners", prCTM.OffSigners), 
                 new SqlParameter("@User_ID", User_ID), 
                 new SqlParameter("@CTM_Remark", prCTM.CTM_Remark), 
                 new SqlParameter("@Return",SqlDbType.Int)
                 };

            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_Save_CTM_Details", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }

        public static DataTable Get_CTM_OffSignersList(int CTM_ID, int Vessel_ID)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                 new SqlParameter("@CTM_ID",CTM_ID ), 
                 new SqlParameter("@VESSEL_ID",Vessel_ID )
             };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_CTM_OFFSIGNERSLIST", prm).Tables[0];


        }

        public static DataTable GET_Side_Letter(int Voyage_ID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_SIDE_LETTER", new SqlParameter("@Voyage_ID", Voyage_ID)).Tables[0];
        }

        public static DataTable Get_Crew_Welfare_Details(int Welfare_ID, int Vessel_ID, DateTime? PB_Date)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Welfare_ID", Welfare_ID), new SqlParameter("@Vessel_ID", Vessel_ID), new SqlParameter("@PB_Date", PB_Date) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_Crew_Welfare_Details", prm).Tables[0];
        }

        public static DataTable Get_Welfare_Details_Documents(int Welfare_Details_ID, int Vessel_ID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Welfare_Details_ID", Welfare_Details_ID), new SqlParameter("@Vessel_ID", Vessel_ID) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_Welfare_Details_Documents", prm).Tables[0];
        }

        public static DataTable Get_Crew_Welfare_Effective_Dates(int? Vessel_ID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_Crew_Welfare_Effective_Dates", new SqlParameter("@VESSEL_ID", Vessel_ID)).Tables[0];
        }

        public static DataTable Get_Lib_Crew_Welfare(int? Vessel_ID, DateTime? Effective_Date, int Page_Index, int Page_Size, ref int Record_Count)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                new SqlParameter("@Vessel_ID", Vessel_ID), 
                new SqlParameter("@EFFECTIVE_DATE", Effective_Date) ,
                new SqlParameter("@PAGE_INDEX", Page_Index) ,
                new SqlParameter("@PAGE_SIZE", Page_Size) ,
                new SqlParameter("@IS_FETCH_COUNT", Record_Count) 
            };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dtres = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_LIB_Crew_Welfare", prm).Tables[0];
            Record_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dtres;


        }

        public static int UPD_Lib_Crew_Welfare(int Welfare_ID, decimal Welfare_Amount, int Vessel_ID, DateTime Effective_Date, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                new SqlParameter("@Welfare_ID", Welfare_ID), 
                new SqlParameter("@Welfare_Amount", Welfare_Amount) ,
                new SqlParameter("@Vessel_ID", Vessel_ID) ,
                new SqlParameter("@Effective_Date", Effective_Date) ,
                new SqlParameter("@UserID", UserID) 
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_LIB_Crew_Welfare", prm);
        }

        public static int Get_Bank_Account_Status(int AllotmentID, int Vessel_ID)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ACC_Get_Bank_Account_Status", new SqlParameter[] { new SqlParameter("@AllotmentID", AllotmentID), new SqlParameter("@Vessel_ID", Vessel_ID) }));
        }

        public static DateTime? Get_Valid_Date_TO_Rework_Allotment(int Vessel_ID)
        {
            object pdate = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ACC_GET_Valid_Date_TO_Rework_Allotment", new SqlParameter("@Vessel_ID", Vessel_ID));

            DateTime? retdt = null;
            if (!string.IsNullOrWhiteSpace(Convert.ToString(pdate)))
                retdt = Convert.ToDateTime(pdate);

            return retdt;
        }

        public static int UPD_Rework_Allotment(int Vessel_ID, int UserID, DateTime PBill_Date)
        {
            SqlParameter[] prm = new SqlParameter[] {   new SqlParameter("@Vessel_ID", Vessel_ID), 
                                                        new SqlParameter("@UserID", UserID), 
                                                        new SqlParameter("@PBill_Date",PBill_Date),
                                                        new SqlParameter("@Return",SqlDbType.Int)};

            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_Rework_Allotment", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);



        }
        public static int Unverify_Allotment_DL(int Vessel_ID, int AllotmentID, int Verified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Vessel_ID",Vessel_ID) ,
                new SqlParameter("@AllotmentID",AllotmentID) ,
                new SqlParameter("@Verified_By",Verified_By),
                new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Unverify_Allotment", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public static DataTable Get_CTM_Attachments_DL(int CTM_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@CTM_ID", CTM_ID),
                                           new SqlParameter("@Vessel_ID", Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_CTM_Attachments", sqlprm).Tables[0];
        }
        public static DataTable Get_PerMOAllotments_DL(int FleetID, int Vessel_ID, string Month, string Year, int MO_ID, int BankID, int? CrewID = null, int Country_ID = 0)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetID",FleetID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@MO_ID",MO_ID),
                                            new SqlParameter("@BankID",BankID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Country_ID",Country_ID)

                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_GetPerMOAllotments", sqlprm).Tables[0];

        }

        //New Methods for Add Capt Cash (Un-Finalized) 
        public static DataSet Get_CapCashReport_Cur_DL(int? Vessel_ID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                       
                        new SqlParameter("@Vessel_ID",SqlDbType.Int)
            };

            obj[0].Value = Vessel_ID;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_CaptCashReports_Cur", obj);
        }
        public static DataSet ReportDetailsByID_Cur_DL(string Id, string Vessel)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                        new SqlParameter("@Vessel",SqlDbType.Int) ,
                         new SqlParameter("@Id",SqlDbType.Int)  
            };
            obj[0].Value = Vessel;
            obj[1].Value = Id;

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_CaptCashfollwUP_Cur", obj);
        }


        public static int Upd_Generate_PortageBill(string Month, string Year, int Vessel_ID, int UserID, int? Crew_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
          {
              new SqlParameter("@smonth",Month),
              new SqlParameter("@syear",Year),
              new SqlParameter("@pVessel_ID",Vessel_ID),
              new SqlParameter("@UserID",UserID),
              new SqlParameter("@VoyageID",Crew_ID),

          };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_Generate_PortageBill", prm);


        }

        public static int Upd_Finalize_PortageBill(int Month, int Year, int Vessel_ID, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
          {
              new SqlParameter("@Month",Month),
              new SqlParameter("@Year",Year),
              new SqlParameter("@Vessel_ID",Vessel_ID),
              new SqlParameter("@UserID",UserID),
            
              new SqlParameter("@return",SqlDbType.Int)

          };

            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_UPD_Finalize_PortageBill", prm);

            return Convert.ToInt32(prm[prm.Length - 1].Value);

        }

        public static DataTable Get_Open_PortageBill()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_Open_PortageBill").Tables[0];
        }

        public static int INS_Initial_Office_Portage_Bill_DL(int Vessel_ID, DateTime? Date, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {                                           
                                            new SqlParameter("@VesselID",Vessel_ID),
                                            new SqlParameter("@PbillDate",Date),                                            
                                            new SqlParameter("@UserId",Created_By), 
                                             new SqlParameter("@return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_INS_Initialize_PortageBill", sqlprm);
              return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
