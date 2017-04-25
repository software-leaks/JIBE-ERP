using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;
using System.Data.SqlClient;

namespace SMS.Business.Crew
{
    /// <summary>
    /// Interview related methods
    /// Interview Scheduling , Interview Questionary ,Interview Results in Crew Deatils tab
    /// </summary>
    public class BLL_Crew_Interview
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        
        #region Crew Interview

        public int Delete_CrewInterview(int InterviewId,int CrewID, int UserID)
        {
            return DAL_Crew_Interview.Delete_CrewInterview_DL(InterviewId,CrewID, UserID);
        }

        public static DataSet Get_Crew_Interview_Results(int CrewID, int UserID, string InterviewType)
        {
            return DAL_Crew_Interview.Get_Crew_Interview_Results_DL(CrewID, UserID, InterviewType);
        }

        public static DataTable getInterviewDetails(int InterviewID)
        {
            return DAL_Crew_Interview.Get_InterviewDetails_DL(InterviewID);
        }
        public static SqlDataReader Get_UserAnswers(int CrewID, int InterviewID)
        {
            return DAL_Crew_Interview.Get_UserAnswers_DL(CrewID, InterviewID);
        }        
        public static int INS_CrewInterviewPlanning(int CrewID, int Rank, string CandidateName, string InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By)
        {
            try
            {
                DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

                return DAL_Crew_Interview.INS_CrewInterviewPlanning_DL(CrewID, Rank, CandidateName, dtInterviewPlanDate, PlannedInterviewerID, InterviewerPosition, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_CrewInterviewPlanning(int InterviewID, string InterviewPlanDate, int PlannedInterviewerID, int InterviewRank, int Modified_By, string TimeZone, int IQID)
        {
            try
            {

                return DAL_Crew_Interview.UPDATE_CrewInterviewPlanning_DL(InterviewID, InterviewPlanDate, PlannedInterviewerID, InterviewRank, Modified_By, TimeZone, IQID);
            }
            catch
            {
                throw;
            }
        }
        public static int UPDATE_CrewInterviewPlanning(int InterviewID, string InterviewPlanDate, int PlannedInterviewerID, int InterviewRank, int Modified_By)
        {
            try
            {

                return DAL_Crew_Interview.UPDATE_CrewInterviewPlanning_DL(InterviewID, InterviewPlanDate, PlannedInterviewerID, InterviewRank, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_PlannedInterviewDetails(int UserID, int CrewID)
        {
            return DAL_Crew_Interview.Get_PlannedInterviewDetails_DL(UserID, CrewID);
        }

        public static int UPDATE_CrewInterviewResult(int CrewID, int InterviewID, int InterviewerID, string InterviewDate, int RankID, string InterviewerPosition, string ResultText, string Selected, string OtherText, int Modified_By)
        {
            try
            {
                DateTime dtInterviewDate = DateTime.Parse(InterviewDate, iFormatProvider);
                int iSelected = 0;
                if (Selected != "")
                    iSelected = int.Parse(Selected);

                return DAL_Crew_Interview.UPDATE_CrewInterviewResult_DL(CrewID, InterviewID, InterviewerID, InterviewerPosition, dtInterviewDate, RankID, ResultText, iSelected, OtherText, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        
        public static int UPDATE_CrewInterviewResult(int CrewID, int InterviewID, int InterviewerID, string InterviewDate, int RankID, string InterviewerPosition, string ResultText, string Selected, string OtherText, int Modified_By, DataTable dtAnswers)
        {
            try
            {
                DateTime dtInterviewDate = DateTime.Parse(InterviewDate, iFormatProvider);
                int iSelected = 0;
                if (Selected != "")
                    iSelected = int.Parse(Selected);

                return DAL_Crew_Interview.UPDATE_CrewInterviewResult_DL(CrewID, InterviewID, InterviewerID, InterviewerPosition, dtInterviewDate, RankID, ResultText, iSelected, OtherText, Modified_By, dtAnswers);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_CrewInterviewAnswer(int InterviewID, int QuestionID, int SelectedOptionID, string Remarks,int NotApplicable, int Created_By)
        {
            try
            {
                return DAL_Crew_Interview.UPDATE_CrewInterviewAnswer_DL(InterviewID, QuestionID, SelectedOptionID, Remarks,NotApplicable, Created_By);
            }
            catch
            {
                throw;
            }
        }
            
        #endregion


        #region Admin

        public static DataTable Get_CategoryList(string SearchText)
        {
            try
            {
                return DAL_Crew_Interview.Get_CategoryList_DL(SearchText);
            }
            catch
            {
                throw;
            }

        }
        public static int INSERT_Category(string Category_Name, int Created_By)
        {
            try
            {
                return DAL_Crew_Interview.INSERT_Category_DL(Category_Name, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public static int UPDATE_Category(int ID, string Category_Name, int Updated_By)
        {
            try
            {
                return DAL_Crew_Interview.UPDATE_Category_DL(ID, Category_Name, Updated_By);
            }
            catch
            {
                throw;
            }

        }
        public static int DELETE_Category(int ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Interview.DELETE_Category_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_GradingList()
        {
            try
            {
                return DAL_Crew_Interview.Get_GradingList_DL();
            }
            catch
            {
                throw;
            }

        }
        public static int INSERT_Grading(string Grading_Name, int Grade_Type,  int Min, int Max, int Divisions, int Created_By)
        {
            try
            {
                return DAL_Crew_Interview.INSERT_Grading_DL(Grading_Name,Grade_Type,  Min,  Max,  Divisions,  Created_By);
            }
            catch
            {
                throw;
            }

        }
        public static int UPDATE_Grading(int ID, string Grading_Name,int Grading_Type, int Min, int Max, int Divisions, int Updated_By)
        {
            try
            {
                return DAL_Crew_Interview.UPDATE_Grading_DL(ID, Grading_Name,Grading_Type, Min, Max, Divisions, Updated_By);
            }
            catch
            {
                throw;
            }

        }
        public static int DELETE_Grading(int ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Interview.DELETE_Grading_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_GradingOptions(int Grade_ID)
        {
            try
            {
                return DAL_Crew_Interview.Get_GradingOptions_DL(Grade_ID);
            }
            catch
            {
                throw;
            }

        }
        public static int INSERT_GradingOption(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
            try
            {
                return DAL_Crew_Interview.INSERT_GradingOption_DL(Grade_ID, OptionText, OptionValue, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public static int UPDATE_GradingOption(int Option_ID, string OptionText, decimal OptionValue, int Modified_By)
        {
            try
            {
                return DAL_Crew_Interview.UPDATE_GradingOption_DL(Option_ID, OptionText, OptionValue, Modified_By);
            }
            catch
            {
                throw;
            }

        }
        public static int DELETE_GradingOptions(int Grade_ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Interview.DELETE_GradingOptions_DL(Grade_ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_Interviews(int RankID, int UserID)
        {
            try
            {
                return DAL_Crew_Interview.Get_Interviews_DL(RankID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_Interviews(string FilterName, int RankID, int UserID)
        {
            try
            {
                return DAL_Crew_Interview.Get_Interviews_DL(FilterName,RankID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static int Delete_Interview(int IQID, int RankID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Interview.Delete_Interview_DL(IQID, RankID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public static int UPDATE_Interview(int IQID, string Interview_Name, int RankID, int Modified_By)
        {
            try
            {
                return DAL_Crew_Interview.UPDATE_Interview_DL(IQID, Interview_Name, RankID, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public static int INS_Interview(string Interview_Name, int RankID, int CopyFrom, int Created_By, string InterviewType)
        {
            try
            {
                return DAL_Crew_Interview.Create_Interview_DL(Interview_Name, RankID, CopyFrom, Created_By, InterviewType);
            }
            catch
            {
                throw;
            }
        }

        public static int Add_QuestionToInterview(int QuestionID, int RankID, int IQID, int UserID)
        {
            return DAL_Crew_Interview.Add_QuestionToInterview_DL(QuestionID, RankID, IQID, UserID);
        }
        public static int Remove_QuestionFromInterview(int QuestionID, int IQID, int RankID, int UserID)
        {
            return DAL_Crew_Interview.Remove_QuestionFromInterview_DL(QuestionID, IQID, RankID, UserID);
        }

        public static DataTable Get_InterviewQuestions(int IQID, int RankID)
        {
            return DAL_Crew_Interview.Get_InterviewQuestions_DL(IQID, RankID);
        }
        public static DataTable Get_InterviewQuestion_Options(int QuestionID)
        {
            return DAL_Crew_Interview.Get_InterviewQuestion_Options_DL(QuestionID);
        }
        public static DataTable Get_InterviewQuestions_UnAssigned(int IQID, int RankID, int CategoryID, string SearchText)
        {
            return DAL_Crew_Interview.Get_InterviewQuestions_UnAssigned_DL(IQID, RankID,  CategoryID, SearchText);
        }
        public static void Swap_InterviewQuestion_Sort_Order(int IQID, int RankID, int QID, int UpDown, int UserID)
        {
            try
            {
                DAL_Crew_Interview.Swap_InterviewQuestion_Sort_Order_DL(IQID, RankID, QID, UpDown, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_InterviewQuestionAnswers(int InterviewID, int UserID)
        {
            return DAL_Crew_Interview.Get_InterviewQuestionAnswers_DL(InterviewID, UserID);
        }
          
        #endregion


        #region QuestionBank
        public static DataTable Get_CriteriaList(string SearchText, int CategoryID)
        {
            try
            {
                return DAL_Crew_Interview.Get_CriteriaList_DL(SearchText, CategoryID);
            }
            catch
            {
                throw;
            }
        }

        public static int INSERT_Criteria(string Criteria, string Answer, int QuestionType, int CatID, int Grading_Type, int Created_By)
        {
            try
            {
                return DAL_Crew_Interview.INSERT_Criteria_DL(Criteria, Answer, QuestionType,CatID, Grading_Type, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public static int UPDATE_Criteria(int ID, string Criteria, string Answer, int CatID, int Grading_Type, int Updated_By)
        {
            try
            {
                return DAL_Crew_Interview.UPDATE_Criteria_DL(ID, Criteria, Answer, CatID, Grading_Type, Updated_By);
            }
            catch
            {
                throw;
            }

        }

        public static int DELETE_Criteria(int ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Interview.DELETE_Criteria_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        #endregion

    }
}
