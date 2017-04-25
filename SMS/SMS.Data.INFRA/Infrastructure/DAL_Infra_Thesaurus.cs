using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Thesaurus
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Thesaurus_Tags_DL(string Project, string Module, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                { 
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@Project", Project),
                    new SqlParameter("@Module", Module),
                };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Thesaurus_Tags", sqlprm).Tables[0];
            
        }

        public static DataTable Thesaurus_Tag_Options_DL(string Project, string Module, int UserID, string FindTag)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                { 
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@Project", Project),
                    new SqlParameter("@Module", Module),
                    new SqlParameter("@FindTag", FindTag),
                };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Thesaurus_Tag_Options", sqlprm).Tables[0];

        }

        public static DataTable Thesaurus_Desc_DL(int UserID, int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                { 
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@ID", ID)
                };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Thesaurus_Desc", sqlprm).Tables[0];

        }
    }
}
