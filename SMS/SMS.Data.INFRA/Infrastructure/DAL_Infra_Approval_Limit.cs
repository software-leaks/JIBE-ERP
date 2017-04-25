using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Approval_Limit
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Approval_Limit(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_Approval_Limit()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_Approval_Limit_List_DL(int ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_Approval_Limit_List", sqlprm).Tables[0];
        }



        public DataTable Get_Approval_Limit_Search(string searchText, int? GROUP_ID, int? USER_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                new System.Data.SqlClient.SqlParameter("@GROUP_ID", GROUP_ID),
                new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
                  
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Approval_Limit_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public int INS_Approval_Limit_DL(int? Group_ID, string Approval_Type, DataTable dt, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Group_ID",Group_ID),
                                            new SqlParameter("@Approval_Type",Approval_Type),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Approval_Limit_DETAILS", sqlprm);
        }

        public int Upd_Approval_Limit_DL(int? ID, int? Group_ID, int? USER_ID, decimal? MAX_APPROVAL_LIMIT, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Group_ID",Group_ID),
                                            new SqlParameter("@USER_ID",USER_ID),
                                            new SqlParameter("@MAX_APPROVAL_LIMIT",MAX_APPROVAL_LIMIT),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Approval_Limit_DETAILS", sqlprm);

        }
        public int Upd_Approval_Limit_DL(int? ID, int? Group_ID, int? USER_ID, decimal? MAX_APPROVAL_LIMIT, decimal? MAX_Invoice_Limit, int PO_Approver, int Invoice_Approver, int Final_Invoice_Approver, int Advance_Approver, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Group_ID",Group_ID),
                                            new SqlParameter("@USER_ID",USER_ID),
                                            new SqlParameter("@MAX_APPROVAL_LIMIT",MAX_APPROVAL_LIMIT),
                                            new SqlParameter("@MAX_Invoice_Limit",MAX_Invoice_Limit),
                                             new SqlParameter("@PO_Approver",PO_Approver),
                                            new SqlParameter("@Invoice_Approver",Invoice_Approver),
                                            new SqlParameter("@Final_Invoice_Approver",Final_Invoice_Approver),
                                            new SqlParameter("@Advance_Approver",Advance_Approver),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Approval_Limit_DETAILS", sqlprm);

        }



        public int Del_Approval_Limit_DL(int? ID, int? Group_ID, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                         new SqlParameter("@ID",ID),
                                         new SqlParameter("@Group_ID",@Group_ID),
                                         new SqlParameter("@UserID",UserID),

                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DEL_Approval_Limit_DETAILS", sqlprm);

        }



        public DataTable Get_Approval_Group()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_Approval_Group").Tables[0];
        
        }

        public DataTable Get_UserListApprovalLimit_DL(string ApprovalType, int Group_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                         new SqlParameter("@ApprovalType",ApprovalType),
                                         new SqlParameter("@Group_ID",Group_ID),
                                    };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserListApprovalLimit", sqlprm);
            return ds.Tables[0];
        }
        public int CheckAmountApplicable_DL(string ApprovalType, int AmountApplicable)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{
				new SqlParameter("ApprovalType", ApprovalType),                
				new SqlParameter("return",SqlDbType.Int)
			};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_CHK_AMOUNT_APPLICABLE", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        
    }
}
