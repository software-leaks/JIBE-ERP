using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.INFRA;
using SMS.Data.INFRA.Infrastructure;

namespace SMS.Business.INFRA.Infrastructure
{
    public class BLL_Infra_DrillQuestionnaire
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static int INS_UPD_DEL_Grading(int? ID, string Grading_Name, int? Grade_Type, int? Min, int? Max, int? Divisions, int UserID, string Mode)
        {
            try
            {
                return DAL_Infra_DrillQuestionnaire.INS_UPD_DEL_Grading_DL(ID, Grading_Name, Grade_Type, Min, Max, Divisions, UserID, Mode);
            }
            catch
            {
                throw;
            }

        }
        public static int INS_UPD_DEL_GradingOption(int Grade_ID, string OptionText, decimal OptionValue, int UserID)
        {
            try
            {
                return DAL_Infra_DrillQuestionnaire.INS_UPD_DEL_GradingOption_DL(Grade_ID, OptionText, OptionValue, UserID);
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
                return DAL_Infra_DrillQuestionnaire.Get_GradingList_DL();
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
                return DAL_Infra_DrillQuestionnaire.Get_GradingOptions_DL(Grade_ID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_QuestionList()
        {
            try
            {
                return DAL_Infra_DrillQuestionnaire.Get_QuestionList_DL();
            }
            catch
            {
                throw;
            }

        }
        public static int INS_UPD_DEL_Question(int? ID, string Question, int? Grading_Type, int UserID, string Mode)
        {
            try
            {
                return DAL_Infra_DrillQuestionnaire.INS_UPD_DEL_Question_DL(ID, Question, Grading_Type, UserID, Mode);
            }
            catch
            {
                throw;
            }
        }
    }
}
