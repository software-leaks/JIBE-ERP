using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Crew;
using System.Data;

namespace SMS.Business.Crew
{
    public class BLL_Crew_CardApproval
    {
        DAL_Crew_CardApproval objDAL = new DAL_Crew_CardApproval();

        public DataTable Get_CrewCardApprovalDetails(string ApprovalType, string SerchText)
        {
            try
            {
                return objDAL.Get_CrewCardApprovalDetails_DL(ApprovalType, SerchText);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CardApprovalUserList(string ApprovalType)
        {
            try
            {
                return objDAL.Get_CardApprovalUserList_DL(ApprovalType);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CardApproval(string ApprovalType, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_CardApproval_DL(ApprovalType, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public int Del_CardApprover(int? CardApproverId, int UserID)
        {
            return objDAL.Del_CardApprover_DL(CardApproverId, UserID);
        }
    }
}
