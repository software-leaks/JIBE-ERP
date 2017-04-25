using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Customer defined libararies
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_Attachment
    {
        /// <summary>
        /// Returns attachemnt list
        /// </summary>
        /// <param name="RequestId">Travel request id for which we need the list of attache file</param>
        /// <returns>Dataset with list of attachements</returns>
        public DataSet GetAttachments(int RequestID, string Attach_Type,int? AgentID)
        {
            DAL_TRV_Attachment att = new DAL_TRV_Attachment();
            try { return att.Get_Attachements(RequestID, Attach_Type,AgentID); }
            catch { throw; }
            finally { att = null; }
        }

        /// <summary>
        /// Save attachements
        /// </summary>
        /// <param name="crewMailId"></param>
        /// <param name="Attachment_Name"></param>
        /// <param name="Attachment_Path"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Boolean SaveAttchement(int RequestID, string Attachment_Name, string Attachment_Path,
            string Attachment_type, string refNumber, int userid, int? AgentID)
        {
            DAL_TRV_Attachment att = new DAL_TRV_Attachment();
            try { return att.Save_Attachment(RequestID, Attachment_Name, Attachment_Path, Attachment_type, refNumber, userid, AgentID); }
            catch { throw; }
            finally { att = null; }
        }

        /// <summary>
        /// Delete attachment
        /// </summary>
        /// <param name="id">attachment id to be deleted</param>
        /// <param name="userid">userid who is deleteing the attachement</param>
        /// <returns>true on success, false otherwise</returns>
        public Boolean DeleteAttachment(int id, int userid)
        {
            DAL_TRV_Attachment att = new DAL_TRV_Attachment();
            try { return att.Del_Attachment(id, userid); }
            catch { throw; }
            finally { att = null; }
        }


        public int Send_Ticket(int RequsetID, int UserID)
        {
            DAL_TRV_Attachment att = new DAL_TRV_Attachment();
            return att.Sent_Ticket_DL(RequsetID, UserID);


        }

    }
}
