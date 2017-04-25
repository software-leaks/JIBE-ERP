using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Crew
{
    public class DAL_Crew_CrewRotation
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        
        public static DataTable ExecuteQuery(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }
        public static DataTable ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm).Tables[0];
        }
        public static DataSet ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm, int DataSet)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm);
        }

        public static DataSet ExecuteDataset(string SQLCommandText, SqlParameter[] sqlprm)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm);
        }

        public static DataSet Get_RotationReport_DL(int CrewID, int Vessel_Manager, int ManningOfficeID, int FleetID, int VESSEL_ID, int RankID, string From_Dt, string To_Dt, string SearchText, int UserID)
        {

            DateTime Dt_From_Dt = DateTime.Parse("1900/01/01");
            if (From_Dt.Length > 0)
                Dt_From_Dt = DateTime.Parse(From_Dt, iFormatProvider);

            DateTime Dt_To_Dt = DateTime.Parse("2099/01/01");
            if (To_Dt.Length > 0)
                Dt_To_Dt = DateTime.Parse(To_Dt, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@CrewID",CrewID),
                                                      new SqlParameter("@Vessel_Manager",Vessel_Manager),
                                                      new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                                      new SqlParameter("@FleetID",FleetID),
                                                      new SqlParameter("@VesselID",VESSEL_ID),
                                                      new SqlParameter("@RankID",RankID),
                                                      new SqlParameter("@From_Dt",Dt_From_Dt),
                                                      new SqlParameter("@To_Dt",Dt_To_Dt),
                                                      new SqlParameter("@SearchText",SearchText),
                                                      new SqlParameter("@UserID",UserID)
   												  };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RotationReport", sqlprm);
        }

        public static string Get_UserData_DL(int UserID, string DataColumn)
        {
            string SQL = "";

            SQL = "SELECT " + DataColumn + " FROM LIB_USER WHERE USERID=@UserID";

            SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
   												  };

            return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL, obj));
        }
    }
}
