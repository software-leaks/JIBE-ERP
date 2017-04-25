using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SMS.Data.Infrastructure;
using SMS.Properties;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Rank
    {
        DAL_Infra_Rank objDal = new DAL_Infra_Rank();
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public BLL_Infra_Rank()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable Get_RankMandatoryList()
        {
            try
            {
                return objDal.Get_RankMandatoryList_DL();
            }
            catch
            {
                throw;
            }
        }

        public int INS_RankMandatoryList(DataTable dt, int UserID)
        {
            try
            {
                return objDal.INS_RankMandatoryList_DL(dt,UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Ranks_Snippet()
        {
            try
            {
                return objDal.Get_Ranks_Snippet_DL();
            }
            catch
            {
                throw;
            }
        }

        public int INS_RankSnippetList(DataTable dt, int UserID)
        {
            try
            {
                return objDal.INS_RankSnippetList_DL(dt, UserID);
            }
            catch
            {
                throw;
            }
        }
        public int INS_Rank_Configuration(int Rank, int? MR, int? HOChk, int UserID)
        {
            try
            {
                return objDal.INS_Rank_Configuration(Rank, MR, HOChk, UserID);
            }
            catch { throw; }

        }


        public DataTable Get_Rank_Configuration()
        {
            try { return objDal.Get_Rank_Configuration(); }
            catch { throw; }
        }
    }
    
}
