using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SMS.Data.OPS.Operations
{
    public class DAL_OPS_VesselParameters
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_OPS_VesselParameters(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_OPS_VesselParameters()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public int INS_VesselParameter_DL(int VesselID, decimal PropellorPitch, decimal QMCR, decimal RPM_Max, decimal Abs_eng_rat_power, decimal Eng_rat_power, decimal SFOC, decimal Cyl_oil_calc_mothod, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselID",VesselID),
                new SqlParameter("@Propeller_Pitch",PropellorPitch),
                 new SqlParameter("@Qmcr",QMCR),
                 new SqlParameter("@RPM_Max",RPM_Max),
                 /*New Parameters added for Chemical Tanker*/
                 new SqlParameter("@Abs_eng_rat_power",Abs_eng_rat_power),
                 new SqlParameter("@Eng_rat_power",Eng_rat_power),
                 new SqlParameter("@SFOC",SFOC),
                 new SqlParameter("@Cyl_oil_calc_mothod",Cyl_oil_calc_mothod),
                 /*end*/
                new SqlParameter("@Created_By",Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_INS_VesselParameters", sqlprm);
        }
        public int UPD_VesselParameter_DL(int GeneralParameterID, int VesselID, decimal PropellorPitch, decimal QMCR, decimal RPM_Max, decimal Abs_eng_rat_power, decimal Eng_rat_power, decimal SFOC, decimal Cyl_oil_calc_mothod, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@GeneralParameterID",GeneralParameterID),
                new SqlParameter("@VesselID",VesselID),
                new SqlParameter("@Propeller_Pitch",PropellorPitch),
                new SqlParameter("@Qmcr",QMCR),
                new SqlParameter("@RPM_Max",RPM_Max),
                 /*New Parameters added for Chemical Tanker*/
                new SqlParameter("@Abs_eng_rat_power",Abs_eng_rat_power),
                 new SqlParameter("@Eng_rat_power",Eng_rat_power),
                 new SqlParameter("@SFOC",SFOC),
                 new SqlParameter("@Cyl_oil_calc_mothod",Cyl_oil_calc_mothod),
                 /*end*/
                new SqlParameter("@Modified_By",Modified_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_UPD_VesselParameters", sqlprm);
        }
        public DataTable Get_VesselParameter_DL(int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselID",VesselID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_VesselParameters", sqlprm);
            return ds.Tables[0];
        }
        public int DEL_VesselParameter_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID",ID),
                new SqlParameter("@Deleted_By",Deleted_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_DEL_VesselParameters", sqlprm);
        }

        public DataTable Get_VesselParameterBy_ID_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@GeneralParameterID",ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_VesselParametersByID", sqlprm);
            return ds.Tables[0];
        }

        public DataTable Get_VesselParameter_DL(int VesselID, int StartIndex, int PageSize, ref int TotalCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@VesselID",VesselID),
                new SqlParameter("@StartIndex", StartIndex),
                new SqlParameter("@PageSize", PageSize),
                new SqlParameter("@TotalCount", TotalCount)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_GET_VesselParameters_Paging", sqlprm);
            TotalCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
    }
}
