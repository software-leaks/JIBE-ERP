using System;
using SMS.Data.PURC;
using System.Data;

using SMS.Data;
using System.Configuration;
using System.Linq;
using SMS.Properties;



/// <summary>
/// Summary description for Department_PMS
/// </summary>
/// 

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase : IDisposable
    {
       

        DAL_PURC_ApprovalLimit objApprovalLimit = new DAL_PURC_ApprovalLimit();

        public int SaveApprovalLimit(ApprovalLimitData objDOApprovalLimit)
        {
           
            return objApprovalLimit.SaveApprovalLimit(objDOApprovalLimit);


        }


        public int DeleteApprovalLimit(ApprovalLimitData objDOApprovalLimit)
        {
           
            return objApprovalLimit.DeleteApprovalLimit(objDOApprovalLimit);




        }


        public int EditApprovalLimit(ApprovalLimitData objDOApprovalLimit)
        {
           
            return objApprovalLimit.EditApprovalLimit(objDOApprovalLimit);



        }

        public DataTable SelectLibUsers()
        {

            return objApprovalLimit.SelectLibUsers();

        }

        public DataTable SelectApprovalLimit()
        {

            return objApprovalLimit.SelectApprovalLimit();

        }
        public int PURC_ItemResetSelection(int UserID, string Document_Code)
        {
            return objApprovalLimit.PURC_ItemResetSelection(UserID, Document_Code);
        }
        public int INSERT_Buyer_Remarks(string DocumentCode, int UserID, string BuyerRemarks)
        {
            return objApprovalLimit.INSERT_Buyer_Remarks(UserID,DocumentCode,BuyerRemarks);
        }
        public void Close()
        {

        }




        public void Dispose()
        {

        }
    }
}
