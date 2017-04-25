using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Seniority
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";

        public DAL_Crew_Seniority(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Crew_Seniority()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataTable Get_SeniorityYearRewardList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CRW_SeniorityRewardYearList").Tables[0];
        }
        public int Insert_Seniority_DL(int SeniorityYear, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SeniorityYear",SeniorityYear),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INS_CRW_SeniorityYearForReward", sqlprm);
        }
        public int DELETE_Seniority_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Delete_CRW_SeniorityYearForReward", sqlprm);
        }
        public DataTable Get_CrewSeniorityRewardList_DL(int CompanySeniorityYear, string RewardStatus, string searchText, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CompanySeniorityYear",CompanySeniorityYear),
                                            new SqlParameter("@RewardStatus",RewardStatus),
                                            new SqlParameter("@SearchText",searchText),
                                            new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@ORDER_BY",sortbycoloumn),
                                            new SqlParameter("@SORT_DIRECTION",sortdirection),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_CRW_CrewSenorityRewardList", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }
        public DataTable Get_CrewSeniorityRewardList_DL(int CrewId, int SeniorityYear)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewId),
                                            new SqlParameter("@SeniorityYear",SeniorityYear),
                                         };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CRW_SeniorityYearRewardDetails", sqlprm);
            return ds.Tables[0];
        }
        public int Insert_CrewSeniorityRewardDetails_DL(int Crewid, int RankId, int SeniorityYear, string Remarks, string FileName,string FilePath, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",Crewid),
                                            new SqlParameter("@RankId",RankId),
                                            new SqlParameter("@SeniorityYear",SeniorityYear),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@AttachmentFileName",FileName),
                                             new SqlParameter("@AttachmentFilePath",FilePath),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INS_CRW_SeniorityRewardDetails", sqlprm);
        }
        public DataTable GET_CrewRewardedStatus_DL(int CrewId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewId)
                                         };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CrewRewardedStatus", sqlprm);
            return ds.Tables[0];
        }
    }
}
