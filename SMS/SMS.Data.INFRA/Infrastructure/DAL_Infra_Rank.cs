using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using SMS.Properties;

namespace SMS.Data.Infrastructure
{
    
    public class DAL_Infra_Rank
    {
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        private string connection = "";
        private string OCA_connection = "";

         public DAL_Infra_Rank(string ConnectionString)
        {
            connection = ConnectionString;
        }
         public DAL_Infra_Rank()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            OCA_connection = ConfigurationManager.ConnectionStrings["demoasp"].ConnectionString;
        }

        public DataTable Get_RankMandatoryList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_RankMandatoryList").Tables[0];
        }

        public int INS_RankMandatoryList_DL(DataTable dt, int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                                                      new SqlParameter("@dt",dt),
                                                      new SqlParameter("@UserID",UserID)
   												  };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_ReferenceMandatoryForRank", obj);
        }


        public int INS_RankSnippetList_DL(DataTable dt, int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                                                      new SqlParameter("@dt",dt),
                                                      new SqlParameter("@UserID",UserID)
   												  };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_CrewPerformSnippet_Rank", obj);
        }
        public DataTable Get_Ranks_Snippet_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Ranks_Snippet").Tables[0];
        }

        public int INS_Rank_Configuration(int Rank, int? MR, int? HOChk, int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                                                      new SqlParameter("@RANKID",Rank),
                                                        new SqlParameter("@MR",MR),
                                                          new SqlParameter("@HOChk",HOChk),
                                                      new SqlParameter("@UserID",UserID)
   												  };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_Rank_Configuration", obj);
        }

        public DataTable Get_Rank_Configuration()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Rank_Configuration").Tables[0];
        }
    }
    
}
