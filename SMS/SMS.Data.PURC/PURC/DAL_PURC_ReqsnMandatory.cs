using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.PURC
{
    public class DAL_PURC_ReqsnMandatory
    {
         private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
         public DAL_PURC_ReqsnMandatory()
        { 
        }
         public DataSet Get_MandatoryData_DAL()
         {
             System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_ReqsnMandat_data");

             return ds;

         }
          public string PURC_UPD_Mandatory_DAL(DataTable dt)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@dt",dt),
                                                      
                                                     };
            prm[prm.Length - 1].Direction = ParameterDirection.Input;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_ReqsnMandatory", prm);
            return prm[prm.Length - 1].Value.ToString();


        }
    }

 
}
