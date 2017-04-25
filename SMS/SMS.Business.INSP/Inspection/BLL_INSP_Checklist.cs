using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//using SMS.Data.Infrastructure;
using SMS.Data.Inspection;

namespace SMS.Business.Inspection
{
    public class BLL_INSP_Checklist
    {
        DAL_INSP_Checklist objDAL = new DAL_INSP_Checklist();

        public BLL_INSP_Checklist()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable Get_CurrentCheckList(int CheckList_ID)
        {
            return objDAL.Get_CurrentCheckList_DL(CheckList_ID);
        }
        public DataTable Get_CheckListAll(string searchtext, int? checklistType, int? VesselType, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Get_CheckListALL_DL(searchtext, checklistType, VesselType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_CheckListItemsQB(int CheckList_ID, int Parent_ID)
        {
            return objDAL.Get_CheckListItemsQB_DL(CheckList_ID, Parent_ID);
        }
        public DataTable Get_CheckListItemsQB(int CheckList_ID, int Parent_ID, string Description)
        {
            return objDAL.Get_CheckListItemsQB_DL(CheckList_ID, Parent_ID, Description);
        }


        public DataTable Get_CheckListName(int ScheduleID, int Inspid)
        {
            return objDAL.Get_CheckListName_DL(ScheduleID, Inspid);
        }

        public DataTable Get_CheckListLocations(int VesselType)
        {
            return objDAL.Get_CheckListLocations_DL(VesselType);
        }

        public DataTable Get_CheckListLocationsTree(string Function_ID, int Vessel_ID, string Equipment_Type)
        {
            return objDAL.Get_Functional_Tree_Checklist(Function_ID, Vessel_ID, Equipment_Type);
        }

        //All functions related to checklist type  INSERT UPDATE DELETE 
        public DataTable Get_CheckListType(int? CHKID)
        {
            return objDAL.Get_CheckListType_DL(CHKID);
        }

        public DataTable Get_Search_CheckListType(string searchtext)
        {
            return objDAL.Get_Search_CheckListType_DL(searchtext);
        }

        public DataTable Get_SheduleCheckList(int CheckList_ID)
        {
            return objDAL.Get_SheduleCheckList_DL(CheckList_ID);
        }

        public int DELETE_EvaluationType(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_ChecklistType_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_EvaluationType(int ID, string EvaluationType, int Updated_By)
        {
            try
            {
                return objDAL.UPDATE_ChecklistType_DL(ID, EvaluationType, Updated_By);
            }
            catch
            {
                throw;
            }

        }

        public int INSERT_CHecklistType(string EvaluationType, int Created_By)
        {
            try
            {
                return objDAL.INSERT_ChecklistType_DL(EvaluationType, Created_By);
            }
            catch
            {
                throw;
            }

        }

        // Checklist type functions Ends here

        //All functions related to Gradings and options

        public DataTable Get_GradesWithOption()
        {
            return objDAL.Get_GradeswithOptions_DL();
        }

        public DataTable Get_Grades(int? CheckList_ID)
        {
            return objDAL.Get_LocGradeType_DL(CheckList_ID);
        }

        public DataTable Get_GradingOptions(int Grade_ID)
        {
            try
            {
                return objDAL.Get_GradingOptions_DL(Grade_ID);
            }
            catch
            {
                throw;
            }

        }

        public int INSERT_Grading(string Grading_Name, int Grade_Type, int Min, int Max, int Divisions, int Created_By)
        {
            // try
            // {
            return objDAL.INSERT_Grading_DL(Grading_Name, Grade_Type, Min, Max, Divisions, Created_By);
            //   }
            //catch
            //{
            //    throw;
            //}

        }

        public int UPDATE_Grading(int ID, string Grading_Name, int Grading_Type, int Min, int Max, int Divisions, int Updated_By)
        {
            try
            {
                return objDAL.UPDATE_Grading_DL(ID, Grading_Name, Grading_Type, Min, Max, Divisions, Updated_By);
            }
            catch
            {
                throw;
            }

        }

        public int DELETE_Grading(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_Grading_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public int INSERT_GradingOption(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
            try
            {
                return objDAL.INSERT_GradingOption_DL(Grade_ID, OptionText, OptionValue, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_GradingOption(int Option_ID, string OptionText, decimal OptionValue, int Modified_By)
        {
            try
            {
                return objDAL.UPDATE_GradingOption_DL(Option_ID, OptionText, OptionValue, Modified_By);
            }
            catch
            {
                throw;
            }

        }

        public int DELETE_GradingOptions(int Grade_ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_GradingOptions_DL(Grade_ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }


        //Gradings and options functions ends Here

        #region QuestionBank
        //public DataTable Get_QuestionList(string SearchText, int CategoryID)
        public DataTable Get_QuestionList(string SearchText, int CategoryID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objDAL.Get_QuestionList_DL(SearchText, CategoryID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }

        public int INSERT_Question(string Criteria, int CatID, int Grading_Type, int Created_By)
        {
            try
            {
                return objDAL.INSERT_Question_DL(Criteria, CatID, Grading_Type, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_Question(int ID, string Criteria, int CatID, int Grading_Type, int Updated_By)
        {
            try
            {
                return objDAL.UPDATE_Question_DL(ID, Criteria, CatID, Grading_Type, Updated_By);
            }
            catch
            {
                throw;
            }

        }

        public int DELETE_Question(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_Question_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        #endregion


        public int Insert_CheckList(int? Checklist_ID, int? Parent_ID, string Version, string CheckList_Name, int Vessel_Type, int checklist_Type, int Location_Grade, int? Created_By, int? Modified_By, int Status)
        {
            try
            {
                return objDAL.INS_Checklist_DL(Checklist_ID, Parent_ID, Version, CheckList_Name, Vessel_Type, checklist_Type, Location_Grade, Created_By, Modified_By, Status);
            }
            catch
            {
                throw;
            }
        }

        public int Insert_PublishCheckList(int? Checklist_ID, int? Created_By, int Status)
        {
            try
            {
                return objDAL.INS_Publish_Checklist_DL(Checklist_ID, Created_By, Status);
            }
            catch
            {
                throw;
            }
        }

        public int Insert_EditCheckList(int? Checklist_ID)
        {
            try
            {
                return objDAL.INS_Edit_Checklist_DL(Checklist_ID);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateS_SheduleAndChecklist(int? Checklist_ID, DataTable table, int? Created_By)
        {
            try
            {
                return objDAL.INS_UpdateSheduleAndChecklist_DL(Checklist_ID, table, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int Insert_CheckListItem(int? ID, int? Parent_Id, int Checklist_ID, int? Location_ID, DataTable dtCHKItemID, string Node_Type, string Description, int? Created_By, int? Modified_By, int? Active_Status)
        {
            try
            {
                return objDAL.INS_ChecklistItems_DL(ID, Parent_Id, Checklist_ID, Location_ID, dtCHKItemID, Node_Type, Description, Created_By, Modified_By, Active_Status);
            }
            catch
            {
                throw;
            }
        }

        public int Save_ScheduleAndCheckList(int ChecklistID, int ScheduleID, int Created_By, int Active_Status)
        {
            return objDAL.Save_INSP_ScheduleAndChecklist_DL(ChecklistID, ScheduleID, Created_By, Active_Status);
        }
        public DataTable Get_ChecklistsEdit(int? Vessel_ID, int? Shedule_ID)
        {
            return objDAL.Get_Edit_ChecklistOnVesselType_DL(Vessel_ID, Shedule_ID);
        }
        public DataTable Get_Checklists(int? Vessel_ID)
        {
            return objDAL.Get_ChecklistOnVesselType_DL(Vessel_ID);
        }

        #region checklistRating
        public DataTable Get_CheckListName(int ScheduleID)
        {
            return objDAL.Get_CheckListName_DL(ScheduleID);
        }

        public int Insert_LocationRatings(int? Location_ID, int Node_ID, decimal System_Current_Report, string Location_Rating, int? Schedule_ID, int Inspection_ID, string Additional_Remarks, int? Created_By)
        {
            return objDAL.INS_LocationRatings_DL(Location_ID, Node_ID, System_Current_Report, Location_Rating, Schedule_ID, Inspection_ID, Additional_Remarks, Created_By);
        }

        //public int Insert_ChecklistRatings( int Node_ID, decimal System_Current_Report,  int Inspection_ID, int? Created_By)
        public DataTable Insert_ChecklistRatings(DataTable dt)
        {
            //return objDAL.INS_ChecklistRatings_DL( Node_ID, System_Current_Report,   Inspection_ID, Created_By);
            return objDAL.INS_ChecklistRatings_DL(dt);
        }

        public int Insert_Final_ChecklistRatings(int ChecklistID, int Inspection_ID, int? Created_By)
        {
            return objDAL.INS_Finalize_ChecklistRatings_DL(ChecklistID, Inspection_ID, Created_By);
        }

        public int Insert_ItemRatings(int Item_ID, decimal System_Current_Report, string Item_Rating, int? Schedule_ID, int Inspection_ID, string Additional_Remarks, int? Created_By)
        {
            return objDAL.INS_ItemRatings_DL(Item_ID, System_Current_Report, Item_Rating, Schedule_ID, Inspection_ID, Additional_Remarks, Created_By);
        }

        public DataTable Get_CurrentCheckListRatings(int CheckList_ID, int InspID)
        {
            return objDAL.Get_CurrentCheckListRatings_DL(CheckList_ID, InspID);
        }

        public DataTable Get_LocationNodeANDLocationID(int CheckList_ID, int InspID, int CheckListItem_ID)
        {
            return objDAL.Get_LocationNode_AND_LocationID_DL(CheckList_ID, InspID, CheckListItem_ID);
        }
        #endregion


        #region ChecklistInspectionReport

        public DataTable Get_WorklistReportWithCategoryGrouping(int InspectionID, int ShowImages, string ReportType, string ChecklistIDs)//,string TBODY)
        {
            return objDAL.Get_WorklistReportWithCategoryGrouping_DL(InspectionID, ShowImages, ReportType, ChecklistIDs);
        }

        public DataTable Get_WorklistReportWithAllGrouping(int InspectionID)
        {
            return objDAL.Get_WorklistReportWithAllGrouping_DL(InspectionID);
        }

        #endregion

        #region CHECKLISTCATEGORY

        public DataTable Get_CategoryList()
        {
            return objDAL.Get_CategoryList();
        }
        
        public int INSERT_UPDATE_DELETE_Category(int CategoryID, string Category, int LogUserID, char operationType)
        {
            try
            {
                return objDAL.INSERT_UPDATE_DELETE_Category(CategoryID, Category, LogUserID, operationType);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_Search_CheckListCategory(string searchtext)
        {
            return objDAL.Get_Search_CheckListCategory(searchtext);
        }
        #endregion

    }
}
