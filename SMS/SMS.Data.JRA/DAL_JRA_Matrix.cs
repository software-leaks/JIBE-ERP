using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace SMS.Data.JRA
{
    public class DAL_JRA_Matrix
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        //public static System.Data.DataSet GET_JRA_MATRIX_DATA()
        //{
        //    DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_JRA_MATRIX_DATA", null);
        //    return ds;
        //}

        //public static DataSet GET_JRA_MATRIX_ROW_DATA()
        //{
        //    DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_MATRIX_ROW_DATA", null);
        //    return ds;
        //}

        public static DataSet  GET_JRA_MATRIX_TABLE()
        {
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_MATRIX", null);
            return ds;
        }
    }
}
