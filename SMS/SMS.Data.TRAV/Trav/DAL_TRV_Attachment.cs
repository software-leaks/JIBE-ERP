using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//custome defined libraries
using SMS.Data;
using SMS.Properties;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_Attachment
    {
        SqlConnection conn;

        /// <summary>
        /// Returns attachemnt list
        /// </summary>
        /// <param name="RequestId">Travel request id for which we need the list of attache file</param>
        /// <returns>Dataset with list of attachements</returns>
        public DataSet Get_Attachements(int RequestID, string Attach_Type,int? AgentID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {   new SqlParameter("@RequestID", RequestID),
                                            new SqlParameter("@Attach_Type", Attach_Type),
                                            new SqlParameter("@AgentID",AgentID)
                                        };

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Attachment", sqlprm);
            }
            catch { throw; }
        }

        /// <summary>
        /// Save attachements
        /// </summary>
        /// <param name="crewMailId"></param>
        /// <param name="Attachment_Name"></param>
        /// <param name="Attachment_Path"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Boolean Save_Attachment(int RequestID, string Attachment_Name, string Attachment_Path,
            string Attachment_type, string refNumber, int userid, int? AgentID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                        new SqlParameter("@attachment_name", Attachment_Name),
                                        new SqlParameter("@attachment_path", Attachment_Path),
                                        new SqlParameter("@attachment_type", Attachment_type),
                                        new SqlParameter("@refNumber", refNumber),
                                        new SqlParameter("@userid", userid),
                                        new SqlParameter("@AgentID",AgentID)
                                        };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Add_Attachment", sqlprm);
                return true;
            }
            catch { throw; }
        }

        /// <summary>
        /// Delete attachment
        /// </summary>
        /// <param name="id">attachment id to be deleted</param>
        /// <param name="userid">userid who is deleteing the attachement</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean Del_Attachment(int id, int userid)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@id", id),
                                          new SqlParameter("@userid", userid)
                                      };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Del_Attachment", sqlprm);
                return true;
            }
            catch { throw; }
        }


        public int Sent_Ticket_DL(int RequsetID, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequsetID),
                                          new SqlParameter("@Created_by", UserID),
                                          new SqlParameter("@Return",SqlDbType.Int)
                                      };

                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_Send_Ticket", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
               
            }
            catch { throw; }
        }
    }


}
