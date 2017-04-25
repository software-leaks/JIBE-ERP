﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Customer defined libaries
using SMS.Data;
using SMS.Properties;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_Airline
    {
        SqlConnection conn;

        public DataSet Get_Airlines(string sText)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter sqlprm = new SqlParameter("@Search_Text", sText);
                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Airlines", sqlprm);
            }
            catch { throw; }
            finally { conn.Close(); }
        }
    }
}
