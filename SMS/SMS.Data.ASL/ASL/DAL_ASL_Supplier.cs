using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using SMS.Data.ASL;
using System.Globalization;


namespace SMS.Data.ASL
{
    public class DAL_ASL_Supplier
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //private static string connection = "";
        public DAL_ASL_Supplier(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_ASL_Supplier()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        //public DAL_ASL_Supplier()
        //{
        //    connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //}

        public static DataTable Get_Proposed_Supplier_Search(string searchtext, int? Propose_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int? Created_By, int? Pageset)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Propose_Status", Propose_Status),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@USERID", Created_By),
                   new System.Data.SqlClient.SqlParameter("@Pageset", Pageset),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                  
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Proposed_Supplier_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Supplier_Search_SimilerName(string searchtext, DataTable dt, string SupplierCode, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@searchtext", searchtext),
                   new System.Data.SqlClient.SqlParameter("@dt", dt),
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", SupplierCode),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Search_SimilerName_new", obj);
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Search_SimilerName", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public static DataTable Get_Supplier_Search(string searchtext, int? Supp_Port, string Supp_Type, string Eval_Status, DataTable dtType, string CurrStatus, int ChkCredit, string SupplierDesc, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Supp_Port", Supp_Port),
                   new System.Data.SqlClient.SqlParameter("@Supp_Type", Supp_Type),
                   new System.Data.SqlClient.SqlParameter("@Eval_Status", Eval_Status),
                   new System.Data.SqlClient.SqlParameter("@dtType", dtType),
                   new System.Data.SqlClient.SqlParameter("@Supp_Status", CurrStatus),
                   new System.Data.SqlClient.SqlParameter("@ChkCredit", ChkCredit),
                   new System.Data.SqlClient.SqlParameter("@SupplierDesc", SupplierDesc),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Search", obj);
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Search_New", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataSet Get_ChangeRequest_Search(string Supplier_code, int CRID, int UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@Supplier_code", Supplier_code),
                new System.Data.SqlClient.SqlParameter("@CRID", CRID),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_ChangeRequest_Search", obj);
            return ds;
        }
        public static DataSet Get_ChangeRequest_Search(int? UserID, string Supplier_Code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code",Supplier_Code),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_CR_Search", obj);
            return ds;
        }
        public static DataSet Get_Pending_CR_List(int? UserID, string searchtext, int? Supp_Port, string Supp_Type, string Eval_Status, DataTable dtType, string CurrStatus, int ChkCredit, string SupplierDesc)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Supp_Port", Supp_Port),
                   new System.Data.SqlClient.SqlParameter("@Supp_Type", Supp_Type),
                   new System.Data.SqlClient.SqlParameter("@Eval_Status", Eval_Status),
                   new System.Data.SqlClient.SqlParameter("@dtType", dtType),
                   new System.Data.SqlClient.SqlParameter("@Supp_Status", CurrStatus),
                   new System.Data.SqlClient.SqlParameter("@ChkCredit", ChkCredit),
                   new System.Data.SqlClient.SqlParameter("@SupplierDesc", SupplierDesc),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Pending_CR_List", obj);
            return ds;
        }
        public static DataTable Get_ChangeRequest_History(string Supplier_Code)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code)         
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Search", obj);
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_ChangeRequest_History", obj);
           
            return ds.Tables[0];

        }

        public static DataTable Get_Proposed_Supplier_List(int? ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Proposed_Supplier_List", obj).Tables[0];

        }
        public static DataTable Get_Supplier_Email_Template(string Supp_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_EmailTemplate", obj).Tables[0];

        }
        public static DataSet Get_Supplier_Data_List(string Supp_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Data_List", obj);

        }
        public static DataSet Get_Supplier_General_Data_List(string Supp_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_General_Data_List", obj);

        }
        public static DataSet Get_Supplier_Change_Request_List(string Supp_ID, int CRID, int UserID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                new System.Data.SqlClient.SqlParameter("@CRID", CRID),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_CR_List", obj);
           // return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_ChangeRequest_List", obj);

        }



        public static int Proposed_Supplier_Insert(int? ID, string Supplier_Name, int? Propose_Type, int? Propose_Status, int? For_Period, string Address, string Phone, string Email, string PIC_NAME, int? Created_By, int Approved_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Supplier_Name", Supplier_Name),
                 new System.Data.SqlClient.SqlParameter("@Propose_Type", Propose_Type),
                 new System.Data.SqlClient.SqlParameter("@Propose_Status", Propose_Status),
                 new System.Data.SqlClient.SqlParameter("@For_Period", For_Period),
                 new System.Data.SqlClient.SqlParameter("@Address", Address),
                 new System.Data.SqlClient.SqlParameter("@Phone", Phone),
                 new System.Data.SqlClient.SqlParameter("@Email", Email),
                 new System.Data.SqlClient.SqlParameter("@PIC_NAME", PIC_NAME),
                 new System.Data.SqlClient.SqlParameter("@USERID", Created_By),
                 new System.Data.SqlClient.SqlParameter("@Approved_By", Approved_By),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Proposed_Supplier_Insert", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static int Proposed_Supplier_Delete(int? ID, int Created_By)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Proposed_Supplier_Delete", obj);
        }

        public static DataTable Get_ASL_System_Parameter(int Parent_Code, string searchtext,int? userid)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_CODE", Parent_Code), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
                    new System.Data.SqlClient.SqlParameter("@Userid", userid),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_System_Parameter_List", obj);
            dt = ds.Tables[0];
            return dt;
        }
        public static DataTable Get_ASL_System_Parameter_Evaluation(int Parent_Code, string searchtext, int? userid)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_CODE", Parent_Code), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
                    new System.Data.SqlClient.SqlParameter("@Userid", userid),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_System_Parameter_ForSuppIndex", obj);
            dt = ds.Tables[0];
            return dt;
        }
        public static DataTable Get_ASL_System_SupplierProposed_Parameter(int Parent_Code, string searchtext, int? userid)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_CODE", Parent_Code), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
                    new System.Data.SqlClient.SqlParameter("@Userid", userid),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_System_SupplierProposed_ParameterList", obj);
            dt = ds.Tables[0];
            return dt;
        }

        public static DataTable Get_ASL_System_SupplierEvaluation_Parameter(int Parent_Code, string searchtext, int? userid)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_CODE", Parent_Code), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
                    new System.Data.SqlClient.SqlParameter("@Userid", userid),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_System_SupplierEvaluation_ParameterList", obj);
            dt = ds.Tables[0];
            return dt;
        }

        public static DataSet Get_Supplier_Scope(string Supp_ID)
        {

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID), 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_SUPPLIER_SCOPE", obj);

        }


        public static DataSet Get_Supplier_PORT(string Supp_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID), 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_SUPPLIER_PORT", obj);

        }
        public static DataSet Get_Supplier_Scope_CR(string Supp_ID, int? CRID)
        {

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID), 
                   new System.Data.SqlClient.SqlParameter("@CRID", CRID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_SUPPLIER_SCOPE_CR", obj);

        }


        public static DataSet Get_Supplier_PORT_CR(string Supp_ID, int?  CRID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID), 
                   new System.Data.SqlClient.SqlParameter("@CRID", CRID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_SUPPLIER_PORT_CR", obj);

        }



        public static int ASL_Supplier_Scope_Insert(string Supp_ID, DataTable dtScope, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@dtScope", dtScope),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_SUPPLIER_SCOPE_INSERT", obj);

        }


        public static int ASL_Supplier_Port_Insert(string Supp_ID, DataTable dtService, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@dtService", dtService),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_SUPPLIER_PORT_INSERT", obj);

        }
        public static int ASL_Supplier_Scope_CR_Insert(string Supp_ID, DataTable dtScope, string CRStatus, int? CreatedBy, int? CRID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@dtScope", dtScope),
                  new System.Data.SqlClient.SqlParameter("@CR_Status", CRStatus),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("@CRID", CRID),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_SUPPLIER_SCOPE_INSERT_CR", obj);

        }


        public static int ASL_Supplier_Port_CR_Insert(string Supp_ID, DataTable dtService, string CRStatus, int? CreatedBy, int? CRID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@dtService", dtService),
                 new System.Data.SqlClient.SqlParameter("@CR_Status", CRStatus),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("@CRID", CRID),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_SUPPLIER_PORT_INSERT_CR", obj);

        }
        public static DataTable Supplier_Email_Verification(string ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Supplier_Email_Verification", obj).Tables[0];

        }

        public static DataSet Get_Supplier_Evaluation_List(string Supp_ID, int? EvaluationID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@EvaluationID", EvaluationID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Evaluation", obj);

        }
        public static int Evaluation_Data_Insert(int? ID, string Supp_ID, string Action_On_Data_Form, string Prposed_status, int? For_Period, string JustificationRemark, string VerificationRemark, string ApprovalRemark, int Created_By, int Verify_By, int Approve_By, string chkUrgent, string Approveurl, string Rejecturl,string SupplierDesc)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@Propose_Status", Prposed_status),
                 new System.Data.SqlClient.SqlParameter("@For_Period", For_Period),
                 new System.Data.SqlClient.SqlParameter("@JustificationRemark", JustificationRemark),
                 new System.Data.SqlClient.SqlParameter("@VerificationRemark", VerificationRemark),
                 new System.Data.SqlClient.SqlParameter("@ApprovalRemark", ApprovalRemark),
                 new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form),
                 new System.Data.SqlClient.SqlParameter("@USERID", Created_By),
                 new System.Data.SqlClient.SqlParameter("@Verify_By", Verify_By),
                 new System.Data.SqlClient.SqlParameter("@Approve_By", Approve_By),
                 new System.Data.SqlClient.SqlParameter("@chkUrgent", chkUrgent),
                 new System.Data.SqlClient.SqlParameter("@Approveurl", Approveurl),
                 new System.Data.SqlClient.SqlParameter("@Rejecturl", Rejecturl),
                 new System.Data.SqlClient.SqlParameter("@SupplierDesc", SupplierDesc),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Evaluation_Supplier_Insert", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Pending_Evaluation_Approve_Reject(int? Evaluation_ID, string Supplier_Code, string EvaluationStatus, int? Created_By, string IpAddress, string Browser_Agent)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Evaluation_ID", Evaluation_ID),
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@EvaluationStatus", EvaluationStatus),
                 new System.Data.SqlClient.SqlParameter("@USERID", Created_By),
                 new System.Data.SqlClient.SqlParameter("@IpAddress", IpAddress),
                 new System.Data.SqlClient.SqlParameter("@Browser_Agent", Browser_Agent)
            };
            
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Evaluation_Supplier_Approve_Reject", obj);
        }
        public static int Evaluation_Send_Email(int? Evaluation_ID, string Supplier_Code, string EvaluationStatus, int? Created_By, string ApproveUrl, string RejectUrl)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Evaluation_ID", Evaluation_ID),
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@EvaluationStatus", EvaluationStatus),
                 new System.Data.SqlClient.SqlParameter("@USERID", Created_By),
                new System.Data.SqlClient.SqlParameter("@Approveurl", ApproveUrl),
                 new System.Data.SqlClient.SqlParameter("@Rejecturl", RejectUrl),
            };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Send_Evaluation_Mail", obj);
        }
        public static int Evaluation_Data_Delete(int? ID, string Supp_ID, int Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@USERID", Created_By),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Evaluation_Supplier_Delete", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Supplier_Data_Insert(int? ID, string Supp_ID, string Register_Name, string Address, string State
            , string CountryName, int? CountryID, string Postal_Code, string Comp_Phone1, string Comp_Phone2, string Comp_AOH_NO1, string Comp_AOH_NO2
            , string Comp_Fax1, string Comp_Fax2, string Comp_Email1, string Comp_Email2,
            string Comp_WebSite, string Comp_Reg_No
            , string Comp_Tex_Reg_No, string Comp_ISO_No, string Supplier_Currency, int? Supplier_CurrencyID, string PIC_NAME_Title1, string PIC_NAME_Title2
            , string PIC_NAME1, string PIC_NAME2, string PIC_Designation1, string PIC_Designation2, string PIC_Email1, string PIC_Email2
            , string PIC_Phone1, string PIC_Phone2, string Bank_Name, string Bank_Address, string Bank_Code, string Branch_Code
            , string SWIFT_Code, string IBAN_Code, string Beneficiary_Name, string Beneficiary_Address, string Beneficiary_Account
            , string Beneficiary_Account_Curr, string Notification_Payment, string Notification_Email, string Verified_Person, string Designation, string Action_On_Data_Form, int? Created_By,
            string SkypeAddress, string SkypeAddress2, string PICMobileNo, string PICmobileNo1, string SuppScope, string AddService, string OtherBankInformation, string TaxAccNumber)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID ),					 
			 
               new System.Data.SqlClient.SqlParameter("@Register_Name", Register_Name ),				 
               new System.Data.SqlClient.SqlParameter("@Address", Address ),	
			   new System.Data.SqlClient.SqlParameter("@State", State ),
               new System.Data.SqlClient.SqlParameter("@CountryName", CountryName ),		
               new System.Data.SqlClient.SqlParameter("@CountryID", CountryID ),			
               new System.Data.SqlClient.SqlParameter("@Postal_Code", Postal_Code ),	
               new System.Data.SqlClient.SqlParameter("@Comp_Phone1", Comp_Phone1 ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_Phone2",Comp_Phone2  ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_AOH_NO1",Comp_AOH_NO1  ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_AOH_NO2", Comp_AOH_NO2 ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_Fax1",Comp_Fax1  ),					 
               new System.Data.SqlClient.SqlParameter("@Comp_Fax2",Comp_Fax2  ),		 
               new System.Data.SqlClient.SqlParameter("@Comp_Email1",Comp_Email1  ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_Email2", Comp_Email2 ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_WebSite", Comp_WebSite ),				 
               new System.Data.SqlClient.SqlParameter("@Comp_Reg_No",Comp_Reg_No  ),			  
               new System.Data.SqlClient.SqlParameter("@Comp_Tex_Reg_No",Comp_Tex_Reg_No  ),			 
               new System.Data.SqlClient.SqlParameter("@Comp_ISO_No", Comp_ISO_No ),		
		       new System.Data.SqlClient.SqlParameter("@Supplier_Currency",Supplier_Currency  ),	
               new System.Data.SqlClient.SqlParameter("@Supplier_CurrencyID",Supplier_CurrencyID  ),			 
               new System.Data.SqlClient.SqlParameter("@PIC_NAME_Title1",  PIC_NAME_Title1),			 
               new System.Data.SqlClient.SqlParameter("@PIC_NAME_Title2",PIC_NAME_Title2  ),			 
               new System.Data.SqlClient.SqlParameter("@PIC_NAME1",PIC_NAME1  ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_NAME2", PIC_NAME2 ),					  
               new System.Data.SqlClient.SqlParameter("@PIC_Designation1", PIC_Designation1 ),			 
               new System.Data.SqlClient.SqlParameter("@PIC_Designation2", PIC_Designation2 ),			 
               new System.Data.SqlClient.SqlParameter("@PIC_Email1",PIC_Email1  ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_Email2", PIC_Email2 ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_Phone1", PIC_Phone1 ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_Phone2", PIC_Phone2 ),					 
               new System.Data.SqlClient.SqlParameter("@Bank_Name", Bank_Name ),					 
               new System.Data.SqlClient.SqlParameter("@Bank_Address", Bank_Address ),				 
               new System.Data.SqlClient.SqlParameter("@Bank_Code", Bank_Code ),					 
               new System.Data.SqlClient.SqlParameter("@Branch_Code",Branch_Code  ),				 
               new System.Data.SqlClient.SqlParameter("@SWIFT_Code",SWIFT_Code  ),					 
               new System.Data.SqlClient.SqlParameter("@IBAN_Code", IBAN_Code ),					 
               new System.Data.SqlClient.SqlParameter("@Beneficiary_Name", Beneficiary_Name ),			 
               new System.Data.SqlClient.SqlParameter("@Beneficiary_Address",Beneficiary_Address  ), 		 
               new System.Data.SqlClient.SqlParameter("@Beneficiary_Account", Beneficiary_Account ),		 
               new System.Data.SqlClient.SqlParameter("@Beneficiary_Account_Curr",Beneficiary_Account_Curr),	 
               new System.Data.SqlClient.SqlParameter("@Notification_Payment", Notification_Payment ),		 
               new System.Data.SqlClient.SqlParameter("@Notification_Email", Notification_Email ),			 
               new System.Data.SqlClient.SqlParameter("@Verified_Person", Verified_Person ),				 
               new System.Data.SqlClient.SqlParameter("@Designation",Designation ),	
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("@SkypeAddress", SkypeAddress ),
               new System.Data.SqlClient.SqlParameter("@SkypeAddress2", SkypeAddress2 ),
               new System.Data.SqlClient.SqlParameter("@PICMobileNo", PICMobileNo ),
               new System.Data.SqlClient.SqlParameter("@PICmobileNo1", PICmobileNo1 ),
               new System.Data.SqlClient.SqlParameter("@SuppScope", SuppScope ),
               new System.Data.SqlClient.SqlParameter("@AddService", AddService ),
               new System.Data.SqlClient.SqlParameter("@OtherBankInformation", OtherBankInformation ),
               new System.Data.SqlClient.SqlParameter("@TaxAccNumber", TaxAccNumber ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_SUPPLIER_DATA_INSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static string Supplier_General_Data_Insert(int? ID, string Supplier_Code, string Supp_Type, string Supp_Status, string Register_Name, string Short_Name, int? group, string Address1,
             string CountryName, string City, string Comp_Phone1, string Comp_Fax1, string Comp_Email1, string Supplier_Currency,
              string PIC_NAME1, string PIC_NAME2, string PIC_Email1, string PIC_Email2, string PIC_Phone1, string PIC_Phone2,
             int? ownerShip, string Payment_Instructions, string Notification_Email, string BizCorporation, string Invoice_Status, string Direct_Invoice_Upload,string PaymentHistory,string SendPo,
            string PaymentPriority, int? Payment_Interval, int? Payment_Terms_Days, string Payment_Terms, decimal? TaxRate, string Action_On_Data_Form, int? Created_By, int? CountryID,
            string Supplier_Description, string SubType, string TaxAccNumber, string Company_Reg_No, string GST_Registration_No, decimal? Withholding_Tax_Rate, string Supplier_Short_Code, string ShipSmart_Supplier_Code)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code ),					 
               new System.Data.SqlClient.SqlParameter("@Supp_Type",Supp_Type  ),					 
               new System.Data.SqlClient.SqlParameter("@Supp_Status",Supp_Status  ),				 
               new System.Data.SqlClient.SqlParameter("@Register_Name", Register_Name ),	
			   new System.Data.SqlClient.SqlParameter("@Short_Name", Short_Name ),
               new System.Data.SqlClient.SqlParameter("@group", group ),
               new System.Data.SqlClient.SqlParameter("@Address", Address1 ),	
               new System.Data.SqlClient.SqlParameter("@CountryName", CountryName ),		
               new System.Data.SqlClient.SqlParameter("@City", City ),	
               new System.Data.SqlClient.SqlParameter("@Comp_Phone1", Comp_Phone1 ),				 		 
               new System.Data.SqlClient.SqlParameter("@Comp_Fax1",Comp_Fax1  ),					 
               new System.Data.SqlClient.SqlParameter("@Comp_Email1",Comp_Email1  ),				 
		       new System.Data.SqlClient.SqlParameter("@Supplier_Currency",Supplier_Currency  ),	
               new System.Data.SqlClient.SqlParameter("@PIC_NAME1",PIC_NAME1  ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_NAME2", PIC_NAME2 ),					  
               new System.Data.SqlClient.SqlParameter("@PIC_Email1",PIC_Email1  ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_Email2", PIC_Email2 ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_Phone1", PIC_Phone1 ),					 
               new System.Data.SqlClient.SqlParameter("@PIC_Phone2", PIC_Phone2 ),					 
               new System.Data.SqlClient.SqlParameter("@ownerShip", ownerShip ),
               new System.Data.SqlClient.SqlParameter("@Payment_Instructions", Payment_Instructions ),
               new System.Data.SqlClient.SqlParameter("@Notification_Email", Notification_Email),			 
               new System.Data.SqlClient.SqlParameter("@BizCorporation", BizCorporation),	
               new System.Data.SqlClient.SqlParameter("@Invoice_Status", Invoice_Status),	
               new System.Data.SqlClient.SqlParameter("@Invoice_Upload", Direct_Invoice_Upload),	
                new System.Data.SqlClient.SqlParameter("@PaymentHistory", PaymentHistory),	
                new System.Data.SqlClient.SqlParameter("@SendPo", SendPo),	
                new System.Data.SqlClient.SqlParameter("@PaymentPriority", PaymentPriority),
               new System.Data.SqlClient.SqlParameter("@Payment_Interval", Payment_Interval),	
               new System.Data.SqlClient.SqlParameter("@Payment_Terms_Days", Payment_Terms_Days),	
               new System.Data.SqlClient.SqlParameter("@Payment_Terms", Payment_Terms),	
               new System.Data.SqlClient.SqlParameter("@TaxRate", TaxRate),	
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("@CountryID", CountryID ),
                new System.Data.SqlClient.SqlParameter("@Supplier_Description", Supplier_Description ),
               new System.Data.SqlClient.SqlParameter("@Counter_Parties_Type", SubType ),
               new System.Data.SqlClient.SqlParameter("@TaxAccNumber", TaxAccNumber ),
                 new System.Data.SqlClient.SqlParameter("@Company_Reg_No", Company_Reg_No ),
               new System.Data.SqlClient.SqlParameter("@GST_Registration_No", GST_Registration_No ),
               new System.Data.SqlClient.SqlParameter("@Withholding_Tax_Rate", Withholding_Tax_Rate ),
                new System.Data.SqlClient.SqlParameter("@Supplier_Short_Code", Supplier_Short_Code ),
               new System.Data.SqlClient.SqlParameter("@ShipSmart_Supplier_Code", ShipSmart_Supplier_Code ),
               //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return  (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_SUPPLIER_GENERAL_DATA_INSERT", obj);
            //return Convert.ToString(obj[obj.Length - 1].Value);
        }

        public static int Supplier_ChangeRequest_Data_Insert(int? ID, string Supp_ID, string Supp_Type, string Register_Name, string Short_Name, string Address1,
            string CountryName, int? countryID, string City, string Comp_Email1, string Comp_Phone1, string Comp_Fax1, string Supplier_Currency,
             string PIC_NAME1, string PIC_Email1, string PIC_Phone1, string PIC_NAME2, string PIC_Email2, string PIC_Phone2,
            int? ownerShip, string PaymentInstrucation, string Notification_Email, string BizCorporation, string Invoice_Status,
            string Direct_Invoice_Upload, string PaymentHistory, string PaymentPriority, int? Payment_Interval, int? Payment_Terms_Days, string Payment_Terms, string TaxRate, string EmailReason, string BankReason, string Action_On_Data_Form, string CR_Status, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID ),					 
               new System.Data.SqlClient.SqlParameter("@Supp_Type",Supp_Type  ),					 				 
               new System.Data.SqlClient.SqlParameter("@Register_Name", Register_Name ),	
			   new System.Data.SqlClient.SqlParameter("@Short_Name", Short_Name ),
               new System.Data.SqlClient.SqlParameter("@Address", Address1 ),	
               new System.Data.SqlClient.SqlParameter("@CountryName", CountryName ),	
	           new System.Data.SqlClient.SqlParameter("@countryID", countryID ),		
               new System.Data.SqlClient.SqlParameter("@City", City ),	
               new System.Data.SqlClient.SqlParameter("@Comp_Email1",Comp_Email1  ),
				 new System.Data.SqlClient.SqlParameter("@Comp_Phone1", Comp_Phone1 ),				 		 
               new System.Data.SqlClient.SqlParameter("@Comp_Fax1",Comp_Fax1  ), 
		       new System.Data.SqlClient.SqlParameter("@Supplier_Currency",Supplier_Currency  ),	
               new System.Data.SqlClient.SqlParameter("@PIC_NAME1",PIC_NAME1  ),
			   new System.Data.SqlClient.SqlParameter("@PIC_Email1",PIC_Email1  ),	
		       new System.Data.SqlClient.SqlParameter("@PIC_Phone1", PIC_Phone1 ),	
               new System.Data.SqlClient.SqlParameter("@PIC_NAME2", PIC_NAME2 ),					  
               new System.Data.SqlClient.SqlParameter("@PIC_Email2", PIC_Email2 ),					 		 
               new System.Data.SqlClient.SqlParameter("@PIC_Phone2", PIC_Phone2 ),					 
               new System.Data.SqlClient.SqlParameter("@ownerShip", ownerShip ),
               new System.Data.SqlClient.SqlParameter("@PaymentInstrucation", PaymentInstrucation ),
               new System.Data.SqlClient.SqlParameter("@Notification_Email", Notification_Email),			 
               new System.Data.SqlClient.SqlParameter("@BizCorporation", BizCorporation),	
               new System.Data.SqlClient.SqlParameter("@Invoice_Status", Invoice_Status),	
               new System.Data.SqlClient.SqlParameter("@Invoice_Upload", Direct_Invoice_Upload),	
                new System.Data.SqlClient.SqlParameter("@PaymentHistory", PaymentHistory),	
                new System.Data.SqlClient.SqlParameter("@PaymentPriority", PaymentPriority),
               new System.Data.SqlClient.SqlParameter("@Payment_Interval", Payment_Interval),	
               new System.Data.SqlClient.SqlParameter("@Payment_Terms_Days", Payment_Terms_Days),	
               new System.Data.SqlClient.SqlParameter("@Payment_Terms", Payment_Terms),	
               new System.Data.SqlClient.SqlParameter("@TaxRate", TaxRate),	
                new System.Data.SqlClient.SqlParameter("@EmailReason", EmailReason),	
                 new System.Data.SqlClient.SqlParameter("@BankReason", BankReason),	
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
                new System.Data.SqlClient.SqlParameter("@CR_Status", CR_Status ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_ChangeRequest_Insert", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static int Supplier_ChangeRequest_Data_Insert(int? CRID, string Supp_ID, DataTable dtCR, string CR_Status, string Action_On_Data_Form, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@CRID",  CRID),							 
		       new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID ),					 
               new System.Data.SqlClient.SqlParameter("@dtCR",dtCR  ),
               new System.Data.SqlClient.SqlParameter("@CR_Status", CR_Status ),	
			   new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_CR_Insert", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Supplier_ChangeRequest_Status_Update(int? CRID, string Supplier_Code, DataTable dtCR, string CR_Status, string Action_On_Data_Form, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
              new System.Data.SqlClient.SqlParameter("@CRID",  CRID),							 
		       new System.Data.SqlClient.SqlParameter("@Supp_ID", Supplier_Code ),					 
               new System.Data.SqlClient.SqlParameter("@dtCR",dtCR  ),
               new System.Data.SqlClient.SqlParameter("@CR_Status", CR_Status ),	
			   new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_CR_Update", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Supplier_Update_Unverify(int? ID, string Supplier_Code, string Action_On_Data_Form, string Status, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code ),					 
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("@Status", Status ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_UPD_Unverify", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Supplier_Insert_Rework(int? ID, string Supplier_Code, string Action_On_Data_Form, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code ),					 
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_UPD_Unverify", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Supplier_ChangeRequest_Status_Update(int? ID, string Supp_ID, string Action_On_Data_Form, string CR_Status, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            
               new System.Data.SqlClient.SqlParameter("@ID",  ID),							 
		       new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID ),					 
               new System.Data.SqlClient.SqlParameter("@Created_By", Created_By ),
               new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form ),
               new System.Data.SqlClient.SqlParameter("@CR_Status", CR_Status ),
               new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_For_Rework", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int Supplier_Data_Send_For_Verification(int? Supp_ID, int? Supp_Data_ID, int? Proposed_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@Supp_Data_ID", Supp_Data_ID),
                 new System.Data.SqlClient.SqlParameter("@Proposed_By", Proposed_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Send_For_Verification", obj);

        }


        public static int Supplier_Data_Send_For_Approval(int? Supp_ID, int? Supp_Data_ID, int? Manager_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@Supp_Data_ID", Supp_Data_ID),
                 new System.Data.SqlClient.SqlParameter("@Manager_ID", Manager_ID),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Send_For_Approval", obj);

        }
        

        public static int Supplier_Data_Approval(int? Supp_ID, int? Supp_Data_ID, int? Supp_Status, int? BOSS_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@Supp_Data_ID", Supp_Data_ID),
                 new System.Data.SqlClient.SqlParameter("@Supp_Status", Supp_Status),
                 new System.Data.SqlClient.SqlParameter("@BOSS_ID", BOSS_ID),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Approval", obj);

        }

        public static DataSet Get_Supplier_PaymentHistory(string Supp_ID, int Type, int PaymentID, int PaymentYear)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@Type",Type),
                   new System.Data.SqlClient.SqlParameter("@PaymentID",PaymentID),
                   new System.Data.SqlClient.SqlParameter("@PaymentYear",PaymentYear),
                    
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_PaymentHistory", obj);

        }

        public static DataTable Get_Supplier_Eval_History_Search(string searchtext, int? Supp_ID, int? Evaluation_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int? Pageset, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                   new System.Data.SqlClient.SqlParameter("@Evaluation_Status", Evaluation_Status),
                      
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@Pageset",Pageset),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Eval_History_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Supplier_Pending_Evaluation(string searchtext, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@searchtext",searchtext),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Pending_Supplier_Approval", obj);
            //isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Supplier_Eval_Search(int? UserID, string searchtext, int? Supp_Port, string Supp_Type, string Eval_Status, DataTable dtType, string CurrStatus, int ChkCredit, string SupplierDesc)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                    new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Supp_Port", Supp_Port),
                   new System.Data.SqlClient.SqlParameter("@Supp_Type", Supp_Type),
                   new System.Data.SqlClient.SqlParameter("@Eval_Status", Eval_Status),
                   new System.Data.SqlClient.SqlParameter("@dtType", dtType),
                   new System.Data.SqlClient.SqlParameter("@Supp_Status", CurrStatus),
                   new System.Data.SqlClient.SqlParameter("@ChkCredit", ChkCredit),
                   new System.Data.SqlClient.SqlParameter("@SupplierDesc", SupplierDesc),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Eval_Search", obj);
            return ds.Tables[0];
        }



        public static DataSet Get_Supplier_Attachment(string Supplier_Code)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_Supplier_Attachment", obj);

        }
        public static DataSet Get_Supplier_Upload_Document(string Supplier_Code, string Supp_ID, string InvoiceType,string FileID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@InvoiceType", InvoiceType),
                  new System.Data.SqlClient.SqlParameter("@FileID", FileID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_Supplier_Upload_Docs", obj);

        }
        public static int Delete_Uploaded_Files(int? FileID, int? USERID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@FileID", FileID),
                 new System.Data.SqlClient.SqlParameter("@USERID", USERID),
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Upload_Docs_Delete", obj);


        }
        public static int Supplier_Upload_Document_Insert(int? FileID, string Supplier_Code, string Supply_ID, string File_Name, string File_Path, string File_Extension, string InvioceRef, DateTime? InvoiceDate, decimal? InvoiceAmount, decimal? TaxAmount, string Remarks, DateTime? InvoiceDueDate, string FileStatus, string Action_On_Data_Form,string InvoiceType, int? Created_By, int TYPE)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@FileID", FileID),
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@File_Name", File_Name),
                 new System.Data.SqlClient.SqlParameter("@File_Path", File_Path),
                 new System.Data.SqlClient.SqlParameter("@File_Extension", File_Extension),
                 new System.Data.SqlClient.SqlParameter("@InvioceRef", InvioceRef),
                 new System.Data.SqlClient.SqlParameter("@InvoiceDate", InvoiceDate),
                 new System.Data.SqlClient.SqlParameter("@InvoiceAmount", InvoiceAmount),
                 new System.Data.SqlClient.SqlParameter("@TaxAmount", TaxAmount),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@InvoiceDueDate", InvoiceDueDate),
                  new System.Data.SqlClient.SqlParameter("@FileStatus", FileStatus),
                   new System.Data.SqlClient.SqlParameter("@Action_On_Data_Form", Action_On_Data_Form),
                    new System.Data.SqlClient.SqlParameter("@InvoiceType", InvoiceType),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("@TYPE", TYPE),
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Supplier_Upload_Invoice_Insert", obj));

        }
        public static DataTable Get_Supplier_Remarks(string Supp_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_Supplier_Remarks", obj).Tables[0];

        }
        public static DataTable Get_Supplier_Remarks_List(int  RemarksID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@RemarksID", RemarksID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_Supplier_Remarks_List", obj).Tables[0];

        }
        public static int Delete_Supplier_Remarks(int? RemarksID, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@RemarksID", RemarksID),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Remarks_Delete", obj);
            

        }
        public static int Supplier_Remarks_Insert(int? RemarksID, string Supp_ID, string AmendType, string GeneralType, string GreenType, string YellowType, string RedType, string Remarks, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RemarksID", RemarksID),
                 new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@AmendType", AmendType),
                 new System.Data.SqlClient.SqlParameter("@GeneralType", GeneralType),
                 new System.Data.SqlClient.SqlParameter("@GreenType", GreenType),
                 new System.Data.SqlClient.SqlParameter("@YellowType", YellowType),
                 new System.Data.SqlClient.SqlParameter("@RedType", RedType),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
 
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Remarks_Insert", obj);

        }
        public static int Supplier_Attachment_Insert(string Supplier_Code, string FileName, string Filepath, string FileExtension, int? Created_By, string TYPE, string DocType)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@FileName", FileName),
                 new System.Data.SqlClient.SqlParameter("@Filepath", Filepath),
                 new System.Data.SqlClient.SqlParameter("@FileExtension", FileExtension),
                 new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                 new System.Data.SqlClient.SqlParameter("@TYPE", TYPE),
                 new System.Data.SqlClient.SqlParameter("@DocType", DocType),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Attachment_Insert", obj);

        }


        public static int Supplier_Attachment_Delete(int? File_ID, string Supp_ID, int? Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@File_ID", File_ID), 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Attachment_Delete", obj);

        }



        public static int Supplier_Extend_Period(int? Supp_ID, int? For_period)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                new System.Data.SqlClient.SqlParameter("@For_period", For_period),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Extend_Expiry_Period", obj);
        
        }
        public static DataSet Get_Supplier_ApproverList(int? UserID, string Supplier_Type, string Approver_Type)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                    new System.Data.SqlClient.SqlParameter("@Supplier_Type", Supplier_Type),
                    new System.Data.SqlClient.SqlParameter("@Approver_Type", Approver_Type),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_ApproverList", obj);

            return ds;
        }
        public static DataSet Get_Supplier_CR_ApproverList(int? UserID, string Supplier_Type, string Approver_Type,string Group_Name)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                    new System.Data.SqlClient.SqlParameter("@Supplier_Type", Supplier_Type),
                    new System.Data.SqlClient.SqlParameter("@Approver_Type", Approver_Type),
                    new System.Data.SqlClient.SqlParameter("@Group_Name", Group_Name),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_CR_ApproverList", obj);

            return ds;
        }
        public static DataSet Get_Supplier_POInvoiceHistory(string Supp_ID, int? VesselID, string VesselName, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
                   new System.Data.SqlClient.SqlParameter("@VesselName",VesselName),
                    new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_POInvoice", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_POInvoice", obj);

        }
        public static DataSet Get_Supplier_POInvoiceWIP(string Supp_ID,int? userID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                  new System.Data.SqlClient.SqlParameter("@userID", userID),
                    new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_POInvoice_WIP", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_POInvoice_WIP", obj);

        }
        public static DataSet Get_Supplier_PO_PendingInvoice(string Supp_ID, int? VesselID, string VesselName)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
                   new System.Data.SqlClient.SqlParameter("@VesselName",VesselName),
                    
            };
            
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_PO_Pending_InvoiceStatus", obj);
            return ds;
        }
        public static DataSet ASL_GET_POInvoice_Status(string Supp_ID, int? VesselID, string VesselName)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                 new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
                   new System.Data.SqlClient.SqlParameter("@VesselName",VesselName),

            };
            //obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_POInvoice_Status", obj);
            //isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_POInvoice_Status", obj);

        }
        public static DataSet Get_Supplier_PO_OutStanding(string Supp_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supp_ID", Supp_ID),
                    new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_PO_OutStanding", obj);
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_PO_OutStanding", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
        public static DataSet Get_Supplier_Statistics(string Supp_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supp_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_GET_Supplier_Statistics", obj);
            return ds;
        }
        public static DataTable ASL_Get_TransactionLog(string Supplier_Code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_TransactionLog", obj);
            return ds.Tables[0];
        }
        public static int ASL_Send_CR_mail(string Status, int Created_By, string Supplier_Code, int? CRID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
             
                new System.Data.SqlClient.SqlParameter("@Status", Status),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                new System.Data.SqlClient.SqlParameter("@CRID", CRID),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Send_CR_mail", obj);

        }

        public static DataSet Get_Supplier_Properties(string Supplier_Code, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code), 
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID), 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Properties", obj);

        }
        public static int ASL_Supplier_Properties_Insert(string Supplier_Code, DataTable dt, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@dt", dt),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Supplier_Properties_Insert", obj);

        }
        public static DataSet Get_CR_Supplier_Properties(string Supplier_Code,int? CRID, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code), 
                   new System.Data.SqlClient.SqlParameter("@CRID", CRID), 
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID), 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_CR_Get_Supplier_Properties", obj);

        }
        public static int ASL_CR_Supplier_Properties_Insert(string Supplier_Code, DataTable dt, string Status, int? CreatedBy, int CRID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@dt", dt),
                 new System.Data.SqlClient.SqlParameter("@Status", Status),
                 new System.Data.SqlClient.SqlParameter("@CRID", CRID),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_CR_Supplier_Properties_Insert", obj);

        }
        public static int ASL_CR_Supplier_Child_Insert(int? CRID, string Supplier_Code, string Status, string Action_Data_Form, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@CRID", CRID),
                 new System.Data.SqlClient.SqlParameter("@Supplier_Code", Supplier_Code),
                 new System.Data.SqlClient.SqlParameter("@CR_Status", Status),
                 new System.Data.SqlClient.SqlParameter("@Action_Data_Form", Action_Data_Form),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
                
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_CR_Properties_Insert", obj);

        }
    }

}
