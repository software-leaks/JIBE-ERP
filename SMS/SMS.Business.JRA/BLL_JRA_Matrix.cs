using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.JRA;

namespace SMS.Business.JRA
{
    public class BLL_JRA_Matrix
    {

        //public static DataSet GET_JRA_MATRIX_DATA()
        //{
        //    return DAL_JRA_Matrix.GET_JRA_MATRIX_DATA();
        //}

        //public static DataSet GET_JRA_MATRIX_ROW_DATA()
        //{
        //    return DAL_JRA_Matrix.GET_JRA_MATRIX_ROW_DATA();
        //}

        public static DataSet GET_JRA_MATRIX_TABLE()
        {
            return DAL_JRA_Matrix.GET_JRA_MATRIX_TABLE();
        }
    }
}
