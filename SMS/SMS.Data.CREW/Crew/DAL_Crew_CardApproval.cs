using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Crew
{
    public class DAL_Crew_CardApproval
    {
        private string connection = "";
        public DAL_Crew_CardApproval(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Crew_CardApproval()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataTable Get_CrewCardApprovalDetails_DL(string ApproverType, string SerchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ApproverType",ApproverType),
                new SqlParameter("@SerchText",SerchText)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_Card_Approval_Details", sqlprm).Tables[0];
        }
        public DataTable Get_CardApprovalUserList_DL(string ApproverType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ApproverType",ApproverType)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CardApprovalUserList", sqlprm).Tables[0];
        }
        public int INS_CardApproval_DL(string ApproverType, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApproverType",ApproverType),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CardApproval", sqlprm);
        }
        public int Del_CardApprover_DL(int? CardApproverId, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                    new SqlParameter("@CardApprovalId",CardApproverId),
                    new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DEL_Card_Approver", sqlprm);

        }
    }
}
