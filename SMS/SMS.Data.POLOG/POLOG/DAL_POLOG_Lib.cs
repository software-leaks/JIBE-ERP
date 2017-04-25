using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.POLOG
{
    public class DAL_POLOG_Lib
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //private static string connection = "";
        public DAL_POLOG_Lib(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_POLOG_Lib()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataSet Get_Approval_Group_Item(string Group_Code, string PO_Type)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Group_Code", Group_Code),
                    new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Group_Item", obj);
        }

        public static DataSet POLOG_Get_Approval_Group_User(string Group_Code, string UserType)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Group_Code", Group_Code),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Group_User", obj);
        }

        public static DataTable Get_Approval_Group_List(string Group_Code)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Group_Code",Group_Code),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_GET_Approval_Group_List", sqlprm).Tables[0];
        }

        public static DataSet Get_Approval_Group(string Group_Name, string PO_Type, string Acct, int UserBy, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Group_Name", Group_Name),
                   new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
                   new System.Data.SqlClient.SqlParameter("@Acct", Acct),
                   new System.Data.SqlClient.SqlParameter("@UserBy", UserBy),
                     new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                  new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                  new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Group_Details", obj);
        }
        public static int INS_Approval_Group_Item(string Group_Code, DataTable dtAcc, DataTable dtUser, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Group_Code", Group_Code),
                 new System.Data.SqlClient.SqlParameter("@dtAcc", dtAcc),
                 new System.Data.SqlClient.SqlParameter("@dtUser", dtUser),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_INS_Approval_Group_Item", obj);
        }
        public static DataTable Get_Approval_Group_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                  
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Group_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }


        public static string INS_Approval_Group(string Group_Code, string Group_Name, string PO_Type, int? Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Group_Code",Group_Code),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            new SqlParameter("@PO_Type",PO_Type),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Approval_Group", sqlprm);
        }

        public static int Upd_Approval_Group_DL(string Group_Code, string Group_Name, string PO_Type, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Group_Code",Group_Code),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            new SqlParameter("@PO_Type",PO_Type),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_UPD_Approval_Group", sqlprm);

        }


        public static int Del_Approval_Group(string Group_Code, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                         new SqlParameter("@Group_Code",Group_Code),
                                         new SqlParameter("@Created_By",Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Approval_Group", sqlprm);

        }
        public static string POLOG_INS_Approval_Limit(string LimitID, string Group_Code, int? AdvanceUserID, decimal? MinApprovalLimit, decimal? MaxApprovalLimit, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LimitID",LimitID),
                new System.Data.SqlClient.SqlParameter("@Group_Code",Group_Code),
                new System.Data.SqlClient.SqlParameter("@AdvanceUserID",AdvanceUserID),
                new System.Data.SqlClient.SqlParameter("@MinApprovalLimit",MinApprovalLimit),
                new System.Data.SqlClient.SqlParameter("@MaxApprovalLimit",MaxApprovalLimit),
                new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Approval_Limit", obj);
            //return ds;
        }

        public static string POLOG_INS_Approval_Limit_Approver(string LimitID, DataTable dtApprover, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@LimitID",LimitID),
                new System.Data.SqlClient.SqlParameter("@dtApprover",dtApprover),
                new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Approval_Limit_Approver", obj);
            //return ds;
        }
        public static DataSet POLOG_Get_Approval_Limit(string Limit_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Limit_ID", Limit_ID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Limit", obj);
        }
        public static DataSet POLOG_Get_Approval_Limit(string Limit_ID, string Group_Code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Limit_ID", Limit_ID),
                new System.Data.SqlClient.SqlParameter("@Group_Code", Group_Code),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Limit_Details", obj);
        }
        public static DataTable POLOG_Get_Approval_Limit_Search(string searchText, string GROUP_Code, int? USER_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                new System.Data.SqlClient.SqlParameter("@GROUP_ID", GROUP_Code),
                new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
                  
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Limit_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataTable POLOG_Get_Approval_Limit_Search(string GROUP_Code, int? USER_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@GROUP_Code", GROUP_Code),
                new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),  
            };
            // obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Group_Limit", obj);
            return ds.Tables[0];

        }
        public static int POLOG_Del_Approval_Limit(string Limit_ID, int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Limit_ID",Limit_ID),
                new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_Del_Approval_Limit", obj);
            //return ds;
        }
        public static DataTable POLOG_Get_Approval_Setting_Report(string PO_Type, string Acct, int UserID, string Group_Code, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Req_Type", PO_Type),
                new System.Data.SqlClient.SqlParameter("@Acct", Acct),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                  new System.Data.SqlClient.SqlParameter("@Group_Code", Group_Code),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Approval_Report", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
    }
}
