using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_LicenseKey
    {
      
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;






        public static DataTable LicenseKeySearch(int? Vessel_ID, int? Status,string SearchText ,string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                           
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Status",Status),

                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                            new SqlParameter("@PAGENUMBER",pagenumber),
                                            new SqlParameter("@PAGESIZE",pagesize),
                                            new SqlParameter("@ISFETCHCOUNT",isfetchcount),
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_LicenseKey_Search", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }



        public static int UpdateLicenseKey(int ID, int? VESSEL_ID, string LICENSE_KEY, int SENT_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {  
                  
                   new SqlParameter("@ID",ID),
                   new SqlParameter("@VESSEL_ID",VESSEL_ID),
                   new SqlParameter("@LICENSE_KEY",LICENSE_KEY),
                   new SqlParameter("@SENT_BY",SENT_BY),

         	};

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_Upd_LicenseKey", sqlprm);
        }




    }
}
