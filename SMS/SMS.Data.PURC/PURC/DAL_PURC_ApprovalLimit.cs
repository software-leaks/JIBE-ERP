using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALDepartment
/// </summary>
/// 
namespace SMS.Data.PURC
{
    public class DAL_PURC_ApprovalLimit
    {


        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
  


        public DAL_PURC_ApprovalLimit()
        {
        }

    

        public int SaveApprovalLimit(ApprovalLimitData objDOApprovalLimit)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
        { 
                   new System.Data.SqlClient.SqlParameter("@UserID",objDOApprovalLimit.UserID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDOApprovalLimit.Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Category_Code", objDOApprovalLimit.Category),
                   new System.Data.SqlClient.SqlParameter("@Approval_Limit",Convert.ToDouble(objDOApprovalLimit.Approval_Limit)),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDOApprovalLimit.CurrentUser),
 
        };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Approval_Limit", sqlprm);
                return 0;

            }
            catch (Exception ex)
            {

                throw ex;


            }

        }

        public int DeleteApprovalLimit(ApprovalLimitData objDOApprovalLimit)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", objDOApprovalLimit.ID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", objDOApprovalLimit.CurrentUser)
            };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Del_Approval_Limit", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public int EditApprovalLimit(ApprovalLimitData objDOApprovalLimit)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@ID", objDOApprovalLimit.ID),
                   new System.Data.SqlClient.SqlParameter("@Category", objDOApprovalLimit.Category),
                   new System.Data.SqlClient.SqlParameter("@Approval_Limt", objDOApprovalLimit.Approval_Limit),
                   new System.Data.SqlClient.SqlParameter("@Modified_By", objDOApprovalLimit.CurrentUser) 
                 
            };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_upd_Approval_Limit]", obj);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public DataTable SelectLibUsers()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_Lib_USERS");
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable SelectApprovalLimit()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_USER_APPROVAL_LIMITS");
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int PURC_ItemResetSelection(int UserID, string Document_Code)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@Document_Code", Document_Code),
            };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Reset_Reqn_Item]", obj);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

            
        }

        public int INSERT_Buyer_Remarks(int UserID, string Document_Code,string Buyer_Remarks)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@Document_Code", Document_Code),
                   new System.Data.SqlClient.SqlParameter("@Buyer_Remarks", Buyer_Remarks),
                   new System.Data.SqlClient.SqlParameter("@User_ID", UserID),
            };
             return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_Buyer_Remarks", obj);
    
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }


    }
}