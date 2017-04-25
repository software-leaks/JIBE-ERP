using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_Seniority
    {
        DAL_Crew_Seniority objDAL = new DAL_Crew_Seniority();
        public DataTable Get_SeniorityYearRewardList()
        {
            return objDAL.Get_SeniorityYearRewardList_DL();
        }
        public int Insert_Seniority(int SeniorityYear, int UserId)
        {
            return objDAL.Insert_Seniority_DL(SeniorityYear, UserId);
        }
        public int DELETE_Seniority(int ID, int Deleted_By)
        {
            return objDAL.DELETE_Seniority_DL(ID, Deleted_By);
        }
        public DataTable Get_CrewSeniorityRewardList(int CompanySeniorityYear, string RewardStatus,string searchText, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            return objDAL.Get_CrewSeniorityRewardList_DL( CompanySeniorityYear,RewardStatus, searchText, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
        }
        public DataTable Get_CrewSeniorityRewardDetails(int Crewid,int SeniorityYear)
        {
            return objDAL.Get_CrewSeniorityRewardList_DL(Crewid,SeniorityYear);
        }
        public int Insert_CrewSeniorityRewardDetails(int Crewid, int RankId, int SeniorityYear, string Remarks, string FileName,string FilePath, int UserId)
        {
            return objDAL.Insert_CrewSeniorityRewardDetails_DL(Crewid, RankId, SeniorityYear, Remarks, FileName, FilePath, UserId);
        }
        public DataTable GET_CrewRewardedStatus(int Crewid)
        {
            return objDAL.GET_CrewRewardedStatus_DL(Crewid);
        }
    }
}
