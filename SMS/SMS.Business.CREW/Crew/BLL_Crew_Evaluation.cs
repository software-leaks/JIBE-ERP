using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_Evaluation
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        #region Admin

        public static DataTable Get_CategoryList(string SearchText)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_CategoryList_DL(SearchText);
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
                return DAL_Crew_Evaluation.INSERT_Category_DL(Category_Name, Created_By);
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
                return DAL_Crew_Evaluation.UPDATE_Category_DL(ID, Category_Name, Updated_By);
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
                return DAL_Crew_Evaluation.DELETE_Category_DL(ID, Deleted_By);
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
                return DAL_Crew_Evaluation.Get_GradingList_DL();
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
                return DAL_Crew_Evaluation.INSERT_Grading_DL(Grading_Name,Grade_Type,  Min,  Max,  Divisions,  Created_By);
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
                return DAL_Crew_Evaluation.UPDATE_Grading_DL(ID, Grading_Name,Grading_Type, Min, Max, Divisions, Updated_By);
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
                return DAL_Crew_Evaluation.DELETE_Grading_DL(ID, Deleted_By);
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
                return DAL_Crew_Evaluation.Get_GradingOptions_DL(Grade_ID);
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
                return DAL_Crew_Evaluation.INSERT_GradingOption_DL(Grade_ID, OptionText, OptionValue, Created_By);
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
                return DAL_Crew_Evaluation.UPDATE_GradingOption_DL(Option_ID, OptionText, OptionValue, Modified_By);
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
                return DAL_Crew_Evaluation.DELETE_GradingOptions_DL(Grade_ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_EvaluationTypeList()
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationTypeList_DL();
            }
            catch
            {
                throw;
            }

        }

        public static int INSERT_EvaluationType(string EvaluationType, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_EvaluationType_DL(EvaluationType, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public static int UPDATE_EvaluationType(int ID, string EvaluationType, int Updated_By)
        {
            try
            {
                return DAL_Crew_Evaluation.UPDATE_EvaluationType_DL(ID, EvaluationType, Updated_By);
            }
            catch
            {
                throw;
            }

        }

        public static int DELETE_EvaluationType(int ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Evaluation.DELETE_EvaluationType_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }




        public static DataSet Get_EvaluationRanks(int ParentRank, string Configuration_Type)
        {

            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationRanks(ParentRank, Configuration_Type);
            }
            catch
            {
                throw;
            }



        }
        public static int Update_EvaluationRanks(int ParentRank, DataTable ChildRanks, DataTable VerifiersRanks, string Configuration_Type, int Updated_By)
        {
            try
            {
                return DAL_Crew_Evaluation.Update_EvaluationRanks(ParentRank, ChildRanks, VerifiersRanks, Configuration_Type, Updated_By);
            }
            catch
            {
                throw;
            }

        }
        
        #endregion


        #region QuestionBank
        public static DataTable Get_CriteriaList(string SearchText, int CategoryID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_CriteriaList_DL(SearchText, CategoryID);
            }
            catch
            {
                throw;
            }
        }

        public static int INSERT_Criteria(string Criteria, int CatID, int Grading_Type, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_Criteria_DL(Criteria, CatID,Grading_Type, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public static int UPDATE_Criteria(int ID, string Criteria, int CatID, int Grading_Type, int Updated_By)
        {
            try
            {
                return DAL_Crew_Evaluation.UPDATE_Criteria_DL(ID, Criteria, CatID,Grading_Type, Updated_By);
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
                return DAL_Crew_Evaluation.DELETE_Criteria_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region Evaluation
        
        public static DataTable Get_Evaluations()
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Evaluations_DL();
            }
            catch
            {
                throw;
            }

        }
        public static int INSERT_EvaluationLocation(int Evaluation_ID, int EvaluatorID, int PreNotifyDays, int Created_By, int ChkStatus)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_EvaluationLocation(Evaluation_ID, EvaluatorID, PreNotifyDays, Created_By, ChkStatus);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_EvaluationLocation(int EvaluationID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationLocation(EvaluationID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_Evaluations(string FilterName)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Evaluations_DL(FilterName);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_Assigned_Evaluations_ForCrew(int CrewID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Assigned_Evaluations_ForCrew_DL(CrewID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_Evaluations(int Evaluation_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Evaluations_DL(Evaluation_ID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_EvaluationMonths(int Evaluation_ID, int RankID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationMonths_DL(Evaluation_ID, RankID);
            }
            catch
            {
                throw;
            }

        }

        public static int INSERT_Evaluation(string Evaluation_Name, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_Evaluation_DL(Evaluation_Name, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public static int INSERT_EvaluationMonths(int Evaluation_ID, int RankID, int Month, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_EvaluationMonths_DL(Evaluation_ID,RankID, Month, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public static int UPDATE_EvaluationMonths(int Evaluation_ID, int RankID, int Month, int Active_Status, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.UPDATE_EvaluationMonths_DL(Evaluation_ID, RankID, Month, Active_Status, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public static int UPDATE_Evaluation(int Evaluation_ID, string Evaluation_Name, int Modified_By)
        {
            try
            {
                return DAL_Crew_Evaluation.UPDATE_Evaluation_DL(Evaluation_ID, Evaluation_Name, Modified_By);
            }
            catch
            {
                throw;
            }

        }

        public static int DELETE_Evaluation(int Eva_ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Evaluation.DELETE_Evaluation_DL(Eva_ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_Assigned_CriteriaList(int Evaluation_ID, string SearchText, int CategoryID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Assigned_CriteriaList_DL(Evaluation_ID, SearchText, CategoryID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_UnAssigned_CriteriaList(int Evaluation_ID, string SearchText, int CategoryID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_UnAssigned_CriteriaList_DL(Evaluation_ID, SearchText, CategoryID);
            }
            catch
            {
                throw;
            }
        }
        public static int Add_Criteria_ToEvaluation(int Criteria_ID, int Evaluation_ID, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.Add_Criteria_ToEvaluation_DL(Criteria_ID, Evaluation_ID, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public static int Remove_Criteria_FromEvaluation(int CriteriaID, int Evaluation_ID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Evaluation.Remove_Criteria_FromEvaluation_DL(CriteriaID, Evaluation_ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_Crew_EvaluationSchedule(int CrewID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Crew_EvaluationSchedule_DL(CrewID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_EvaluationScheduleByVessel(int Vessel_ID, int RankID, int StartMonth, int Eva_Type, string SearchText)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationScheduleByVessel_DL(Vessel_ID, RankID, StartMonth, Eva_Type, SearchText);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_EvaluationScheduleByVessel(int Vessel_ID, int StartMonth, int Eva_Type, string SearchText)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationScheduleByVessel_DL(Vessel_ID, StartMonth, Eva_Type, SearchText);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_CrewEvaluationResults(int CrewID, int Year)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_CrewEvaluationResults_DL(CrewID, Year);
            }
            catch
            {
                throw;
            }

        }

        public static void Swap_Criteria_Sort_Order(int Evaluation_ID, int Criteria_ID, int UpDown)
        {
            try
            {
                DAL_Crew_Evaluation.Swap_Criteria_Sort_Order_DL( Evaluation_ID,Criteria_ID, UpDown);
            }
            catch
            {
                throw;
            }
        }

        public static int INSERT_Crew_Evaluation(int CrewID, int Rank, int Evaluation_ID, int Evaluator_ID, string Evaluation_Date, int Created_By, int MonthNo, int Year, int Schedule_ID)
        {
            try
            {
                DateTime dtEvaluation_Date = DateTime.Parse(Evaluation_Date);
                return DAL_Crew_Evaluation.INSERT_Crew_Evaluation_DL(CrewID, Rank, Evaluation_ID, Evaluator_ID, dtEvaluation_Date, Created_By, MonthNo, Year, Schedule_ID);
            }
            catch
            {
                throw;
            }

        }
        public static int INSERT_Crew_Evaluation(int CrewID, int Evaluation_ID, int Evaluator_ID, string Evaluation_Date, int Created_By, string DueDate, int Schedule_ID)
        {
            try
            {
                DateTime dtEvaluation_Date = DateTime.Parse(Evaluation_Date);
                DateTime dtDueDate = DateTime.Parse(DueDate);

                return DAL_Crew_Evaluation.INSERT_Crew_Evaluation_DL(CrewID, Evaluation_ID, Evaluator_ID, dtEvaluation_Date, Created_By, dtDueDate, Schedule_ID);
            }
            catch
            {
                throw;
            }

        }

        public static int INSERT_Crew_Evaluation_Answer(int CrewEvaluation_ID, DataTable dt, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_Crew_Evaluation_Answer_DL(CrewEvaluation_ID, dt, Created_By);
            }
            catch
            {
                throw;
            }
        }
        //public static int INSERT_Crew_Evaluation_Answer(int CrewEvaluation_ID, int Criteria_ID, int UserAnswer, string Remarks, int Created_By)
        //{
        //    try
        //    {
        //        return DAL_Crew_Evaluation.INSERT_Crew_Evaluation_Answer_DL(CrewEvaluation_ID, Criteria_ID, UserAnswer, Remarks, Created_By);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        public static int INSERT_Crew_Evaluation_Answer(int CrewEvaluation_ID, int Criteria_ID, int UserAnswer,string TextAnswer, string Remarks, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.INSERT_Crew_Evaluation_Answer_DL(CrewEvaluation_ID, Criteria_ID, UserAnswer,TextAnswer, Remarks, Created_By);
            }
            catch
            {
                throw;
            }

        }
        
        public static DataTable Get_CrewEvaluation_Details(int CrewID, int CrewDtl_Evaluation_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_CrewEvaluation_Details_DL(CrewID, CrewDtl_Evaluation_ID);
            }
            catch
            {
                throw;
            }

        }

        public static int UPDATE_EvaluationRules(int Evaluation_ID, int RankID, int RuleID, int Active_Status, int Created_By)
        {
            try
            {
                return DAL_Crew_Evaluation.UPDATE_EvaluationRules_DL(Evaluation_ID, RankID, RuleID,Active_Status, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public static int DELETE_EvaluationRules(int Evaluation_ID, int RankID, int Deleted_By)
        {
            try
            {
                return DAL_Crew_Evaluation.DELETE_EvaluationRules_DL(Evaluation_ID, RankID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_EvaluationRules(int Evaluation_ID, int RankID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationRules_DL(Evaluation_ID, RankID);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_EvaluationSelectedRanks(int Evaluation_ID, int UserID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationSelectedRanks_DL(Evaluation_ID, UserID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_EvaluationUnSelectedRanks(int Evaluation_ID, int UserID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluationUnSelectedRanks_DL(Evaluation_ID, UserID);
            }
            catch
            {
                throw;
            }

        }

        public static int Plan_Crew_Evaluation_Adhoc(int CrewID, int Evaluation_ID, string DueDate, int Deleted_By)
        {
            DateTime dtDueDate = DateTime.Parse(DueDate); 
            return DAL_Crew_Evaluation.Plan_Crew_Evaluation_Adhoc_DL(CrewID, Evaluation_ID, dtDueDate, Deleted_By);
        }


        public static int Get_CrewEvaluation_FeedbackCount(int? UserID, int Dtl_Evaluation_ID, int Office_ID, int Vessel_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_CrewEvaluation_FeedbackCount_DL(UserID, Dtl_Evaluation_ID, Office_ID, Vessel_ID);
            }
            catch
            {
                throw;
            }

        }



        public static DataTable Get_CrewEvaluationFeedbackRequest_Details(int ID, int Vessel_ID, int Office_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_CrewEvaluationFeedbackRequest_Details_DL(ID, Vessel_ID, Office_ID);
            }
            catch
            {
                throw;
            }

        }
        public static int INS_CrewEvaluationFeedbackRequest_Details(int ID, int Vessel_ID, int Office_ID, int Created_By, int Requested_From, DateTime? DueDate, string Evaluation_ID, string Month, string Schedule_ID, int Crew_ID, string Req_Comment, int? FeedbackCategory_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.INS_CrewEvaluationFeedbackRequest_Details_DL(ID, Vessel_ID, Office_ID, Created_By, Requested_From, DueDate, Evaluation_ID, Month, Schedule_ID, Crew_ID, Req_Comment, FeedbackCategory_ID);
            }
            catch
            {
                throw;
            }

        }
        public static int INS_CrewEvaluationFeedback(int? ID, int Crew_ID, string Remarks, int Created_By, string Evaluation_ID, string Month, string Schedule_ID, int? Dtl_Evaluation_ID, int? Vessel_ID, int? Office_ID, int FeedbackCategory_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.INS_CrewEvaluationFeedback_DL(ID, Crew_ID, Remarks, Created_By, Evaluation_ID, Month, Schedule_ID, Dtl_Evaluation_ID, Vessel_ID, Office_ID, FeedbackCategory_ID);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_FeedbackCompleted(int CrewEvaluation_ID, int Vessel_ID, int Office_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_FeedbackCompleted_DL(CrewEvaluation_ID, Vessel_ID, Office_ID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_FeedbackCategories()
        {
            try
            {
                return DAL_Crew_Evaluation.Get_FeedbackCategories_DL();
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_Assigned_EvaluatorRank_ForCrew(int CrewID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_Assigned_EvaluatorRank_ForCrew_DL(CrewID);
            }
            catch
            {
                throw;
            }

        }











        public static int Add_MandatoryGrades_ToEvaluation(int Criteria_ID, int Evaluation_ID, int Created_By, DataTable dtOptionValue)
        {
            try
            {
                return DAL_Crew_Evaluation.Add_MandatoryGrades_ToEvaluation_DL(Criteria_ID, Evaluation_ID, Created_By, dtOptionValue);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_MandatoryGrades(int Evaluation_ID, int Criteria_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_MandatoryGrades_DL(Evaluation_ID, Criteria_ID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_MandatoryRemark(int ID, int Evaluation_ID, int Criteria_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_MandatoryRemark_DL(ID, Evaluation_ID, Criteria_ID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_CrewEvaluation_Verification(int? CrewId, int Dtl_Evaluation_ID, int Office_ID, int Vessel_ID)
        {
            return DAL_Crew_Evaluation.Get_CrewEvaluation_Verification(CrewId, Dtl_Evaluation_ID, Office_ID, Vessel_ID);
        }
        public static DataSet Update_CrewEvaluation_Verification(int? CrewId, int Dtl_Evaluation_ID, int Office_ID, int Vessel_ID, int UserId, string VerificationComment)
        {
            return DAL_Crew_Evaluation.Update_CrewEvaluation_Verification(CrewId, Dtl_Evaluation_ID, Office_ID, Vessel_ID, UserId, VerificationComment);
        }


        public static DataTable Get_EvaluatedCrewDetails(int Schedule_ID)
        {
            try
            {
                return DAL_Crew_Evaluation.Get_EvaluatedCrewDetails(Schedule_ID);
            }
            catch
            {
                throw;
            }

        }
        #endregion




    }
}
