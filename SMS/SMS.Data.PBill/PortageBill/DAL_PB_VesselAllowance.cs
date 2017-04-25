using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.PortageBill
{
    public class DAL_PB_VesselAllowance
    {
        private static string connection = "";      
        public DAL_PB_VesselAllowance(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_PB_VesselAllowance()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public static DataTable Get_NationalityGroupForVesselAllowance_DL(int VesselId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselId",VesselId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_NationalityGroupForVesselAllowance", sqlprm).Tables[0];
        }
        public static DataTable Get_NationalityForVesselAllowance_DL(int NationalityGroupId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@NationalityGroupId",NationalityGroupId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_NationalityForVesselAllowance", sqlprm).Tables[0];
        }
        public static DataSet Get_RankWiseVesselAllowance_DL(int NationalityGroupId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@NationalityGroupId",NationalityGroupId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_RankWiseVesselAllowance", sqlprm);
        }
        public static int Insert_NationalityGroup_DL(int VesselId,int NationalityGroupId,String NationalityGroupName, DataTable dt, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselId",VesselId),
                                            new SqlParameter("@NationalityGroupId",NationalityGroupId),
                                            new SqlParameter("@NationalityGroupName",NationalityGroupName),
                                            new SqlParameter("@dtCountry",dt),
                                            new SqlParameter("@UserId",UserId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_INS_NationalityGroupForVesselAllowance", sqlprm);
        }
        public static DataTable Get_NationalityForNationalityGroup_DL(int NationalityGroupId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@NationalityGroupId",NationalityGroupId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_Nationality", sqlprm).Tables[0];
        }
        public static int DeleteNationalityGroup_DL(int NationalityGroupId, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@NationalityGroupId",NationalityGroupId),
                                            new SqlParameter("@UserId",UserId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_DEL_NationalityForVesselAllowance", sqlprm);
        }
        public static int Insert_VesselAllowance_DL(int VesselId, DataTable dt, DateTime EffectiveDate, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselId",VesselId),
                                            new SqlParameter("@dtAllowance",dt),
                                            new SqlParameter("@EffectiveDate",EffectiveDate),
                                            new SqlParameter("@UserId",UserId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_INS_VesselAllowance", sqlprm);
        }
        public static DataTable Check_NationalityGroup_DL(int VesselId, int NationalityGroupId, String NationalityGroupName, DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselId",VesselId),
                                            new SqlParameter("@NationalityGroupId",NationalityGroupId),
                                            new SqlParameter("@NationalityGroupName",NationalityGroupName),
                                            new SqlParameter("@dtCountry",dt)
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_CHK_NationalityGroup", sqlprm).Tables[0];
        }
    }
}
