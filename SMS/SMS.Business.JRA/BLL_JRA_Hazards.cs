using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Business.JRA;
using SMS.Data.JRA;
namespace SMS.Business.JRA
{
    public class BLL_JRA_Hazards
    {
        public static DataSet GET_ASSESSMENT(int Assessment_ID, int Vessel_ID, int UserID, int? Assessment_Dtl_ID = null, int? Office_ID = null)
        {
            return DAL_JRA_Hazards.GET_ASSESSMENT(Assessment_ID, Vessel_ID, UserID, Assessment_Dtl_ID, Office_ID);
        }
        public static void DEL_ASSESSMENT_DETAILS(int Assessemnt_Dtl_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            DAL_JRA_Hazards.DEL_ASSESSMENT_DETAILS(Assessemnt_Dtl_ID, Vessel_ID, Office_ID, UserID);
        }
        public static void DEL_HAZARD_TRMPLATE(int Hazard_ID, int UserID)
        {
            DAL_JRA_Hazards.DEL_HAZARD_TRMPLATE(Hazard_ID, UserID);
        }
        public static int INSUPD_HAZARD_TRMPLATE(int? Hazard_ID, int Work_Categ_ID, string Hazard_Desciption, string Control_Measure, int Severity_ID, int Likelihood_ID, int Initial_Risk_Value, string Additona_Control_Measures, string Modified_Risk_Value, int UserID)
        {
            return DAL_JRA_Hazards.INSUPD_HAZARD_TRMPLATE(Hazard_ID, Work_Categ_ID, Hazard_Desciption, Control_Measure, Severity_ID, Likelihood_ID, Initial_Risk_Value, Additona_Control_Measures, Modified_Risk_Value, UserID);
        }
        public static DataSet JRA_GET_MODIFIED_RISKS()
        {
            return DAL_JRA_Hazards.JRA_GET_MODIFIED_RISKS();
        }
        public static DataSet GET_RISK_RATINGS(int? Rating_Value)
        {
            return DAL_JRA_Hazards.GET_RISK_RATINGS(Rating_Value);
        }
        public static DataSet GET_TYPE(string Type)
        {
            return DAL_JRA_Hazards.GET_TYPE(Type);
        }
        public static DataSet GET_HAZARD_TEMPLATE_LIST(string Hazard_ID, string Work_Categ_ID, string Search, int? PAGE_INDEX, int? PAGE_SIZE, string SortBy, string SORT_DIRECTION, ref int Rowcnt)
        {
            return DAL_JRA_Hazards.GET_HAZARD_TEMPLATE_LIST(Hazard_ID, Work_Categ_ID, Search, PAGE_INDEX, PAGE_SIZE, SortBy, SORT_DIRECTION, ref Rowcnt);
        }
        public static void UPD_ASSESSMENT_STATUS(int Assessment_ID, int Work_categ_ID, int Vessel_ID, string Status, string Remark, int UserID)
        {
            DAL_JRA_Hazards.UPD_ASSESSMENT_STATUS(Assessment_ID, Work_categ_ID, Vessel_ID, Status, Remark, UserID);
        }
        public static int INSUPD_ASSESSMENT(int Assessment_ID, int? Assessment_Dtl_ID, int Vessel_ID, int UserID, int? Hazard_ID, string Hazard_Description, string Control_Measure, string Severity_ID, string Likelihood_ID,
          string Severity, string Likelihood, string Initial_Risk, string Initial_Risk_Value, string Initial_Risk_Color,
            string Additional_Control_Measures,
            string Modified_Risk, string Modified_Risk_Value, string Modified_Risk_Color, int? Office_ID)
        {
            return DAL_JRA_Hazards.INSUPD_ASSESSMENT(Assessment_ID, Assessment_Dtl_ID, Vessel_ID, UserID, Hazard_ID, Hazard_Description, Control_Measure, Severity_ID,
                Likelihood_ID, Severity, Likelihood, Initial_Risk, Initial_Risk_Value, Initial_Risk_Color, Additional_Control_Measures,
                Modified_Risk, Modified_Risk_Value, Modified_Risk_Color, Office_ID);
        }

        public static DataSet GET_ASSESSMENT_SEARCH(string Vessel_ID, string Assessment_ID, string Work_Categ_ID, string Assessment_Status, DateTime? From_Date, DateTime? To_Date, string Search, int UserID, int? PAGE_INDEX, int? PAGE_SIZE, string SortBy, string SORT_DIRECTION, ref int Rowcnt)
        {
            return DAL_JRA_Hazards.GET_ASSESSMENT_SEARCH(Vessel_ID, Assessment_ID, Work_Categ_ID, Assessment_Status, From_Date, To_Date, Search, UserID, PAGE_INDEX, PAGE_SIZE, SortBy, SORT_DIRECTION, ref   Rowcnt);
        }
        public static DataSet Get_Approvar(int Work_Categ_ID)
        {
            return DAL_JRA_Hazards.Get_Approvar(Work_Categ_ID);
        }

        public static DataSet Get_ApprovarByLevel(int Work_Categ_ID, int ApprovalLevel)
        {
            return DAL_JRA_Hazards.Get_ApprovarByLevel(Work_Categ_ID, ApprovalLevel);


        }
        public static int Insert_Approvar(int Work_Categ_ID, int ApprovalLevel, int ApprovarID, int CreatedBy, int Mode, DataTable ApprovarIDTBL)
        {
            return DAL_JRA_Hazards.Insert_Approvar(Work_Categ_ID, ApprovalLevel, ApprovarID, CreatedBy, Mode, ApprovarIDTBL);
        }
        public static int Insert_ApprovalLevels(int Work_Categ_ID, int CreatedBy)
        {
            return DAL_JRA_Hazards.Insert_ApprovalLevels(Work_Categ_ID, CreatedBy);
        }

        public static int Insert_REMARKS(int Assessment_ID, int Vessel_ID, int UserID, string Remarks)
        {
            return DAL_JRA_Hazards.Insert_REMARKS(Assessment_ID, Vessel_ID, UserID, Remarks);
        }

        public static DataSet GET_REMARKS(int Assessment_ID, int Vessel_ID)
        {
            return DAL_JRA_Hazards.GET_REMARKS(Assessment_ID, Vessel_ID);
        }
        public static int UPD_Approvar(int Work_Categ_ID, int ApprovalLevel)
        {
            return DAL_JRA_Hazards.UPD_Approvar(Work_Categ_ID, ApprovalLevel);
        }

        public static DataSet COPY_APPROVAL(int From_Work_Categ_ID, DataTable To_Work_Categ_ID, int User_ID)
        {
            return DAL_JRA_Hazards.COPY_APPROVAL(From_Work_Categ_ID, To_Work_Categ_ID, User_ID);
        }
        public static DataTable GET_WORK_CATEGORY_LIST(int? Work_Categ_Parent_ID, int Mode, int Active_Status)
        {
            return DAL_JRA_Hazards.GET_WORK_CATEGORY_LIST(Work_Categ_Parent_ID, Mode, Active_Status);

        }
        public static DataTable GET_Sev_Cons(int Seveity_ID, int Consequence_ID)
        {

            return DAL_JRA_Hazards.GET_Sev_Cons(Seveity_ID, Consequence_ID);

        }

        public static void INSUPD_Sev_Cons(int Seveity_ID, int Consequence_ID, string SC_Description, int UserID)
        {
            DAL_JRA_Hazards.INSUPD_Sev_Cons(Seveity_ID, Consequence_ID, SC_Description, UserID);

        }


    }
}
