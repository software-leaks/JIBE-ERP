using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_Admin
    {
        DAL_Crew_Admin objCrewDAL = new DAL_Crew_Admin();

        public DataTable Get_RankList()
        {
            try
            {
                return objCrewDAL.Get_RankList_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RankList_Search(string searchtext, int? rankcategory, int? DeckOrEngine, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.Get_RankList_Search(searchtext, rankcategory, DeckOrEngine, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_RankList(string FilterString)
        {
            string SQL = @"SELECT     CRW_LIB_Crew_Ranks.ID, CRW_LIB_Crew_Ranks.Rank_Name, CRW_LIB_Crew_Ranks.Rank_Short_Name, CRW_LIB_Crew_Ranks.Rank_category, 
                                  CRW_LIB_Crew_Rank_Category.CATEGORY_NAME, CRW_LIB_Crew_Ranks.RANK_SORT_ORDER
                                FROM         CRW_LIB_Crew_Ranks INNER JOIN
                                  CRW_LIB_Crew_Rank_Category ON CRW_LIB_Crew_Ranks.Rank_category = CRW_LIB_Crew_Rank_Category.ID WHERE  CRW_LIB_Crew_Ranks.Active_Status=1 ";
            try
            {
                if (FilterString != null)
                {
                    string[] cols = { "RANK_NAME", "RANK_SHORT_NAME", "CRW_LIB_Crew_Rank_Category.CATEGORY_NAME" };
                    string[] arParam = FilterString.Split(' ');
                    string sqlTemp = "";
                    if (FilterString.Length > 0)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            for (int j = 0; j < arParam.Length; j++)
                            {
                                if (sqlTemp.Length > 0)
                                    sqlTemp += " OR ";

                                sqlTemp += cols[i] + " like '%" + arParam[j] + "%' ";
                            }
                        }

                        if (sqlTemp.Length > 0)
                            SQL += " AND (" + sqlTemp + ")";
                    }

                }
                SQL += " ORDER BY CRW_LIB_Crew_Ranks.Rank_Sort_Order";
                return objCrewDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }
        }

        public int EditRank(int ID, string Rank_Name, string Rank_Short_Name, int Rank_Category, int Rank_Sort_Order, int? DeckOrEngine, int UserId, int? IsDeckOfficer)
        {
            try
            {
                return objCrewDAL.EditRank_DL(ID, Rank_Name, Rank_Short_Name, Rank_Category, Rank_Sort_Order, DeckOrEngine, UserId, IsDeckOfficer);
            }
            catch
            {
                throw;
            }
        }

        public int InsertRank(string Rank_Name, string Rank_Short_Name, int Rank_category, int? DeckOrEngine, int UserId, int? IsDeckOfficer)
        {
            try
            {
                return objCrewDAL.InsertRank_DL(Rank_Name, Rank_Short_Name, Rank_category, DeckOrEngine, UserId, IsDeckOfficer);
            }
            catch
            {
                throw;
            }
        }

        public int Swap_Rank_Sort_Order(int ID, int MoveUpDown, int Modified_By)
        {
            try
            {
                return objCrewDAL.Swap_Rank_Sort_Order_DL(ID, MoveUpDown, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteRank(int ID, int UserId)
        {
            try
            {
                return objCrewDAL.DeleteRank_DL(ID, UserId);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RankCategories()
        {
            try
            {
                return objCrewDAL.Get_RankCategories_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RankCategoryByRankID(int RankID)
        {
            try
            {
                return objCrewDAL.Get_RankCategoryByRankID_DL(RankID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_JoiningTypes()
        {
            try
            {
                return objCrewDAL.ExecuteQuery("SELECT JCODE,JOINING_TYPE FROM  CRW_LIB_JOININGTYPES where Active_Status = 1");
            }
            catch
            {
                throw;
            }
        }

        public DataTable ExecuteQuery(string SQL)
        {
            try
            {
                return objCrewDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }
        }

        public DataTable SignoffReason_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.SignoffReason_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_SignoffReason_List(int? ID)
        {
            return objCrewDAL.Get_SignoffReason_List_DL(ID);
        }

        public int InsertSignoffReason(string Reason, int CreatedBy, int JoiningTypeId)
        {
            return objCrewDAL.InsertSignoffReason_DL(Reason, CreatedBy, JoiningTypeId);
        }

        public int EditSignoffReason(int ID, string Reason, int CreatedBy, int JoiningTypeId)
        {
            return objCrewDAL.EditSignoffReason_DL(ID, Reason, CreatedBy, JoiningTypeId);
        }

        public int DeleteSignoffReason(int ID, int CreatedBy)
        {
            return objCrewDAL.DeleteSignoffReason_DL(ID, CreatedBy);
        }



        public DataTable RankCategory_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objCrewDAL.RankCategory_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_RankCategory_List(int? ID)
        {
            return objCrewDAL.Get_RankCategory_List_DL(ID);
        }

        public int InsertRankCategory(string category, int CreatedBy)
        {
            return objCrewDAL.InsertRankCategory_DL(category, CreatedBy);
        }

        public int EditRankCategory(int ID, string category, int CreatedBy)
        {
            return objCrewDAL.EditRankCategory_DL(ID, category, CreatedBy);

        }

        public int DeleteRankCategory(int ID, int CreatedBy)
        {
            return objCrewDAL.DeleteRankCategory_DL(ID, CreatedBy);
        }




        public DataTable JoiningType_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.JoiningType_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_JoiningType_List(int? ID)
        {
            return objCrewDAL.Get_JoiningType_List_DL(ID);
        }

        public int InsertJoiningType(string Joining_Type, string JCode, bool SeniorityConsidered, bool VessselPBill_Considered, bool ServiceConsidered, bool PBillConsidered, DataTable SalComponent, int CreatedBy, bool OperatorConsidered, bool WatchKeepingConsidered)
        {
            return objCrewDAL.InsertJoiningType_DL(Joining_Type, JCode, SeniorityConsidered, VessselPBill_Considered, ServiceConsidered, PBillConsidered, SalComponent, CreatedBy, OperatorConsidered, WatchKeepingConsidered);
        }

        public int EditJoiningType(int ID, string Joining_Type, string JCode, bool SeniorityConsidered, bool VessselPBill_Considered, bool ServiceConsidered, bool PBillConsidered, DataTable SalComponent, int CreatedBy, bool OperatorConsidered, bool WatchKeepingConsidered)
        {
            return objCrewDAL.EditJoiningType_DL(ID, Joining_Type, JCode, SeniorityConsidered, VessselPBill_Considered, ServiceConsidered, PBillConsidered, SalComponent, CreatedBy, OperatorConsidered, WatchKeepingConsidered);
        }

        public int DeleteJoiningType(int ID, int CreatedBy)
        {
            return objCrewDAL.DeleteJoiningType_DL(ID, CreatedBy);
        }


        public DataTable StaffRemarkCategory_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.StaffRemarkCategory_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_StaffRemarkCategory_List(int? ID)
        {
            return objCrewDAL.Get_StaffRemarkCategory_List_DL(ID);
        }

        public int InsertStaffRemarkCategory(string Category, int CreatedBy)
        {
            return objCrewDAL.InsertStaffRemarkCategory_DL(Category, CreatedBy);
        }

        public int EditStaffRemarkCategory(int ID, string Category, int CreatedBy)
        {
            return objCrewDAL.EditStaffRemarkCategory_DL(ID, Category, CreatedBy);
        }

        public int DeleteStaffRemarkCategory(int ID, int CreatedBy)
        {
            return objCrewDAL.DeleteStaffRemarkCategory_DL(ID, CreatedBy);
        }



        public DataTable Trade_Zones_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.Trade_Zones_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_Trade_Zones_List(int? ID)
        {
            return objCrewDAL.Get_Trade_Zones_List_DL(ID);
        }

        public int Insert_Trade_Zones(string ZoneName, int ZoneListID, int CreatedBy)
        {
            return objCrewDAL.Insert_Trade_Zones_DL(ZoneName, ZoneListID, CreatedBy);
        }

        public int Edit_Trade_Zones(int ID, string ZoneName, int ZoneListID, int CreatedBy)
        {
            return objCrewDAL.Edit_Trade_Zones_DL(ID, ZoneName, ZoneListID, CreatedBy);
        }

        public int Delete_Trade_Zones(int ID, int CreatedBy)
        {
            return objCrewDAL.Delete_Trade_Zones_DL(ID, CreatedBy);
        }

        public DataTable Get_Rank_Onboard_Limit_Search(int Vessel_ID)
        {
            return objCrewDAL.Get_Rank_Onboard_Limit_Search(Vessel_ID);
        }

        public int Update_Rank_OnBoard_Limit(int Vessel_ID, int Rank_ID, int Min, int Max, int Created_By)
        {
            return objCrewDAL.Update_Rank_OnBoard_Limit(Vessel_ID, Rank_ID, Min, Max, Created_By);
        }



        public DataTable Agent_Bank_Account_Search(string searchtext, int? Account_Curr, int? MO_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.Agent_Bank_Account_Search(searchtext, Account_Curr, MO_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public DataTable Get_Agent_Bank_Account_List(int? ID)
        {
            return objCrewDAL.Get_Agent_Bank_Account_List_DL(ID);
        }


        public int Insert_Agent_Bank_Account(string Beneficiary, string Bank_Name, string Bank_Address, string Acc_NO, string SwiftCode
        , string BANK_CODE, string BRANCH_CODE, int? ACCOUNT_CURR, int MO_ID, int Created_By)
        {
            return objCrewDAL.Insert_Agent_Bank_Account_DL(Beneficiary, Bank_Name, Bank_Address, Acc_NO, SwiftCode, BANK_CODE, BRANCH_CODE, ACCOUNT_CURR, MO_ID, Created_By);

        }

        public int Edit_Agent_Bank_Account(int? ID, string Beneficiary, string Bank_Name, string Bank_Address, string Acc_NO, string SwiftCode
          , string BANK_CODE, string BRANCH_CODE, int? ACCOUNT_CURR, int MO_ID, int Created_By)
        {

            return objCrewDAL.Edit_Agent_Bank_Account_DL(ID, Beneficiary, Bank_Name, Bank_Address, Acc_NO, SwiftCode, BANK_CODE, BRANCH_CODE, ACCOUNT_CURR, MO_ID, Created_By);
        }

        public int Delete_Agent_Bank_Account(int ID, int Created_By)
        {
            return objCrewDAL.Delete_Agent_Bank_Account_DL(ID, Created_By);
        }




        public DataTable Crew_Rules_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.Crew_Rules_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_Crew_Rules_List(int? RULE_ID)
        {
            return objCrewDAL.Get_Crew_Rules_List_DL(RULE_ID);
        }

        public int Insert_Crew_Rules(string Description, int Created_By)
        {
            return objCrewDAL.Insert_Crew_Rules_DL(Description, Created_By);
        }

        public int Update_Crew_Rules(int? RULE_ID, string Description, int Created_By)
        {
            return objCrewDAL.Update_Crew_Rules_DL(RULE_ID, Description, Created_By);
        }

        public int Delete_Crew_Rules(int? RULE_ID, int Created_By)
        {
            return objCrewDAL.Delete_Crew_Rules_DL(RULE_ID, Created_By);
        }

        public DataTable Crew_Get_Vessel_Specific_Rules_Search(int? RULE_ID)
        {
            return objCrewDAL.Crew_Get_Vessel_Specific_Rules_Search(RULE_ID);
        }

        public DataTable Crew_Get_Rank_Specific_Rules_Search(int? RULE_ID)
        {
            return objCrewDAL.Crew_Get_Rank_Specific_Rules_Search(RULE_ID);
        }

        public int Crew_Rank_Specific_Rules_Assignment(int Rank_ID, int? Rule_ID, int IsApply, int Created_By)
        {
            return objCrewDAL.Crew_Rank_Specific_Rules_Assignment_DL(Rank_ID, Rule_ID, IsApply, Created_By);
        }

        public int Crew_Vessel_Specific_Rules_Assignment(int Vessel_ID, int? Rule_ID, int IsApply, int Created_By)
        {
            return objCrewDAL.Crew_Vessel_Specific_Rules_Assignment_DL(Vessel_ID, Rule_ID, IsApply, Created_By);
        }
        public DataTable Crew_HandOverQuestion_Search(string searchtext, int? Rank, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.Crew_HandOverQuestion_Search(searchtext, Rank, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public int Ins_Update_HandOverQuestion(int? ID, int rank, string Description, int? IsChecklist, string datatype, int Created_By)
        {
            return objCrewDAL.Ins_Update_HandOverQuestion(ID, rank, Description, IsChecklist, datatype, Created_By);
        }
        public DataTable Get_Crew_HandOverQuestion(int? ID)
        {
            return objCrewDAL.Get_Crew_HandOverQuestion(ID);
        }
        public int Delete_Crew_Contract_Period(int ID, int Created_By)
        {
            return objCrewDAL.Delete_Crew_Contract_Period(ID, Created_By);
        }
        public int SaveInterviewSettings(bool Interview_Mandatory, bool Check_Rejected_Interview)
        {
            try
            {
                return objCrewDAL.SaveInterviewSettings_DL(Interview_Mandatory, Check_Rejected_Interview);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetInterviewSettings()
        {
            try
            {
                return objCrewDAL.GetInterviewSettings_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable EvaluationFeedbackCategories_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objCrewDAL.EvaluationFeedbackCategories_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int InsertEvaluationFeedbackCategories(string category, int CreatedBy)
        {
            return objCrewDAL.InsertEvaluationFeedbackCategories_DL(category, CreatedBy);
        }
        public int EditEvaluationFeedbackCategories(int ID, string category, int CreatedBy)
        {
            return objCrewDAL.EditEvaluationFeedbackCategories_DL(ID, category, CreatedBy);

        }
        public int DeleteEvaluationFeedbackCategories(int ID, int CreatedBy)
        {
            return objCrewDAL.DeleteEvaluationFeedbackCategories_DL(ID, CreatedBy);
        }
        public DataTable Get_EvaluationFeedbackCategories_List(int? ID)
        {
            return objCrewDAL.Get_EvaluationFeedbackCategories_List_DL(ID);
        }
        public DataTable Get_InterviewSheets(int RankId, string InterviewType)
        {
            try
            {
                return objCrewDAL.Get_InterviewSheets_DL(RankId, InterviewType);
            }
            catch
            {
                throw;
            }
        }
        public int SaveWagesSettings(bool Nationality, bool RankScale, bool VesselFlag)
        {
            try
            {
                return objCrewDAL.SaveWagesSettings_DL(Nationality, RankScale, VesselFlag);
            }
            catch
            {
                throw;
            }
        }

        public int SaveMandatorySettings(bool NOK, bool CrewPhotograph, bool BankAccDetails, bool Seniority, bool LeaveWithhold, int RankId )
        {
            try
            {
                return objCrewDAL.SaveMandatorySettings_DL(NOK, CrewPhotograph, BankAccDetails, Seniority, LeaveWithhold, RankId );
            }
            catch
            {
                throw;
            }
        }

        public int SaveMandatorySettings(bool NOK, bool CrewPhotograph, bool BankAccDetails, bool Seniority, bool LeaveWithhold, int RankId, bool EvaluationDigitalSignature)
        {
            try
            {
                return objCrewDAL.SaveMandatorySettings_DL(NOK, CrewPhotograph, BankAccDetails, Seniority, LeaveWithhold, RankId, EvaluationDigitalSignature);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetWagesSettings()
        {
            try
            {
                return objCrewDAL.GetWagesSettings_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetMandatorySettings()
        {
            try
            {
                return objCrewDAL.GetMandatorySettings_DL();
            }

            catch
            {
                throw;
            }
        }

        public int SaveDocumentSettings(bool VesselFlagConsidered, bool VesselConsidered)
        {
            try
            {
                return objCrewDAL.SaveDocumentSettings_DL(VesselFlagConsidered, VesselConsidered);
            }
            catch
            {
                throw;
            }
        }
        public int SaveDocumentSettings(bool VesselFlagConsidered, bool VesselConsidered, bool STCW_Deck_Considered, bool STCW_Engine_Considered)
        {
            try
            {
                return objCrewDAL.SaveDocumentSettings_DL(VesselFlagConsidered, VesselConsidered, STCW_Deck_Considered, STCW_Engine_Considered);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetDocumentSettings()
        {
            try
            {
                return objCrewDAL.GetDocumentSettings_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RankScaleList(int RankId)
        {
            try
            {
                return objCrewDAL.Get_RankScaleList_DL(RankId);
            }
            catch
            {
                throw;
            }
        }
        public int Ins_Update_RankScale(int ID, int RankId, string RankScaleName, int UserId)
        {
            return objCrewDAL.Ins_Update_RankScale_DL(ID, RankId, RankScaleName, UserId);
        }
        public int DELETE_RankScale(int ID, int Deleted_By)
        {
            return objCrewDAL.DELETE_RankScale_DL(ID, Deleted_By);
        }

        public DataTable Get_Manning_Report()
        {
            return objCrewDAL.Get_Manning_Report();
        }
        public DataTable Get_MOBankAccount(int CrewID)
        {
            return objCrewDAL.Get_MOBankAccount(CrewID);
        }
        public DataTable Get_MOBankAccount_Details(int ID)
        {
            return objCrewDAL.Get_MOBankAccount_Details(ID);
        }
        public DataTable Get_MOBankAccountList_ByManningID(int MO_ID)
        {
            return objCrewDAL.Get_MOBankAccountList_ByManningID(MO_ID);
        }

        public DataTable Get_RankSeniorityList(int RankId)
        {
            try
            {
                return objCrewDAL.Get_RankSeniorityList_DL(RankId);
            }
            catch
            {
                throw;
            }
        }
        public int Ins_Update_RankSeniority(int RankId, bool CompanySeniority, bool RankSeniority, int UserId)
        {
            return objCrewDAL.Ins_Update_RankSeniority_DL(RankId, CompanySeniority, RankSeniority, UserId);
        }
        public DataTable Get_RankScaleListForWages(int RankId, int CountryId)
        {
            try
            {
                return objCrewDAL.Get_RankScaleListForWages_DL(RankId, CountryId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable ServiceType_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.ServiceType_Search_DL(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int InsertServiceType(string Service_Type, string SCode, bool SeniorityConsidered, int CreatedBy)
        {
            return objCrewDAL.InsertServiceType_DL(Service_Type, SCode, SeniorityConsidered, CreatedBy);
        }
        public int EditServiceType(int ID, string Service_Type, string SCode, bool SeniorityConsidered, int CreatedBy)
        {
            return objCrewDAL.EditServiceType_DL(ID, Service_Type, SCode, SeniorityConsidered, CreatedBy);
        }
        public DataTable Get_ServiceType_List(int? ID)
        {
            return objCrewDAL.Get_ServiceType_List_DL(ID);
        }
        public int DeleteServiceType(int ID, int CreatedBy)
        {
            return objCrewDAL.DeleteServiceType_DL(ID, CreatedBy);
        }
        public int SaveSeniorityResetSettings(bool AutomaticResetConsidered, int Years, int UserId)
        {
            try
            {
                return objCrewDAL.SaveSeniorityResetSettings_DL(AutomaticResetConsidered, Years, UserId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetSeniorityResetSettings()
        {
            try
            {
                return objCrewDAL.GetSeniorityResetSettings_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_JoiningType_PBillComponent(int? JoiningType_ID)
        {
            try
            {
                return objCrewDAL.Get_JoiningType_PBillComponent_DL(JoiningType_ID);
            }
            catch
            {
                throw;
            }
        }
        public int GetOfficeVessel()
        {
            try
            {
                return objCrewDAL.GetOfficeVessel_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewMainStatus()
        {
            try
            {
                return objCrewDAL.Get_CrewMainStatus_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewCalculatedStatus(int MainStatusId)
        {
            try
            {
                return objCrewDAL.Get_CrewCalculatedStatus_DL(MainStatusId);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewMainStatus_Search(string SearchText)
        {
            try
            {
                return objCrewDAL.Get_CrewMainStatus_Search_DL(SearchText);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_Calc_Status_Search(string SearchText)
        {
            try
            {
                return objCrewDAL.Get_Calc_Status_Search_DL(SearchText);
            }
            catch
            {
                throw;
            }
        }


        public int UPDATE_Status(int ID, string Name, int Updated_By)
        {
            try
            {
                return objCrewDAL.UPDATE_Status_DL(ID, Name, Updated_By);
            }
            catch
            {
                throw;
            }

        }




        public int UPDATE_Calc_Status(int ID, string Name, int Updated_By)
        {
            try
            {
                return objCrewDAL.UPDATE_Calc_Status_DL(ID, Name, Updated_By);
            }
            catch
            {
                throw;
            }

        }
        //Not in Use Currently 
        public int DELETE_Calc_Status(int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DELETE_Status_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }

        public int DELETE_Status(int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DELETE_Status_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }
        //End
        public int Insert_Status(string Name, string Value, int Created_By)
        {
            try
            {
                return objCrewDAL.INSERT_Status_DL(Name, Value, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public int Insert_Calc_Status(string Name, string Value, int Created_By)
        {
            try
            {
                return objCrewDAL.INSERT_Calc_Status_DL(Name, Value, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public DataSet SearchStatusStructure(string searchtext)
        {
            return objCrewDAL.SearchStatusStructure(searchtext);
        }



        public DataTable get_MainStatus()
        {
            return objCrewDAL.get_MainStatus_DL();
        }


        public DataTable get_Calc_StatuList()
        {
            return objCrewDAL.get_Calc_StatuList_DL();
        }

        public DataTable get_JoiningTypeList()
        {
            return objCrewDAL.get_JoiningTypeList_DL();
        }

        public DataTable StatusStructure_Edit(int StatusId)
        {
            return objCrewDAL.StatusStructure_Edit_DL(StatusId);
        }

        public string Insert_Update_StatusStructure(int StatusId, DataTable Calc_DT, DataTable JT_DT, int CurrentUser, int Add)
        {
            return objCrewDAL.Insert_Update_StatusStructure_DL(StatusId, Calc_DT, JT_DT, CurrentUser, Add);
        }

        public int Check_Document_Mandatory(int RankId, int CountryId, string Document_Type)
        {
            return objCrewDAL.Check_Document_Mandatory_DL(RankId, CountryId, Document_Type);
        }
        public int Check_Contract_Mandatory(int RankId, int CountryId, int VesselId)
        {
            return objCrewDAL.Check_Contract_Mandatory_DL(RankId, CountryId, VesselId);
        }


        public DataTable CRUD_OilMajors(string OilMajorName, string Action, int UserId, int OilMajorId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string Remarks, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_OilMajors(OilMajorName,  Action, UserId, OilMajorId, searchtext, sortby, sortdirection, pagenumber, pagesize, Remarks, ref isfetchcount, ref result);
        }

        /// <summary>
        /// CRUD Operations for Oil Majors
        /// </summary>
        /// <param name="OilMajorName"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="OilMajorId"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable CRUD_OilMajors(string OilMajorName,string DisplayName, string Action, int UserId, int OilMajorId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string Remarks,int ActiveStatus,string Path, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_OilMajors(OilMajorName,DisplayName, Action, UserId, OilMajorId, searchtext, sortby, sortdirection, pagenumber, pagesize, Remarks,ActiveStatus,Path, ref isfetchcount, ref result);
        }

        /// <summary>
        /// CRUD Operations for Oil Majors Rules
        /// </summary>
        /// <param name="Rule"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="OilMajorRuleId"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataSet CRUD_OilMajorsRules(string Rule, string Action, int UserId, int OilMajorRuleId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_OilMajorsRules(Rule, Action, UserId, OilMajorRuleId, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        /// <summary>
        /// Oil Major Rule Groups 
        /// </summary>
        /// <param name="Rule"></param>
        /// <param name="Type"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RuleGroupId"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable CRUD_OilMajorsRuleGroup(string RuleGroup, int Type, string Action, int UserId, int RuleGroupId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_OilMajorsRuleGroup(RuleGroup, Type, Action, UserId, RuleGroupId, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }


        /// <summary>
        /// To Bind Rule Mapping Oil Majors, Oil Major Groups, Ranks, Oil Major Rule and Value
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="OilMajorId"></param>
        /// <returns></returns>
        public DataSet Bind_Rule_Mapping_Popup(string Action, int OilMajorId, int RuleMappingID)
        {
            return objCrewDAL.Bind_Rule_Mapping_Popup(Action, OilMajorId, RuleMappingID);
        }


        /// <summary>
        /// To Insert/Update oil major rule mapping data
        /// </summary>
        /// <param name="OilMajorRuleId"></param>
        /// <param name="value"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int InsertUpdateOilMajorRuleMapping(string OilMajor_IDs, string Rank_IDs, int RuleGroup_ID, int OilMajorRule_ID, int RuleMappingID, string value, int UserId, string OilMajorsValues)
        {
            int Result = 0;
            Result = objCrewDAL.InsertUpdateOilMajorRuleMapping(OilMajor_IDs, Rank_IDs, RuleGroup_ID, OilMajorRule_ID, RuleMappingID, value, UserId, OilMajorsValues);
            return Result;
        }


        /// <summary>
        /// Delete Oil Major Rule Mapping Record and child table records
        /// </summary>
        /// <param name="RuleMappingID"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int DeleteOilMajorRuleMapping(int RuleMappingID, int UserId, int oilMajorID)
        {
            return objCrewDAL.DeleteOilMajorRuleMapping(RuleMappingID, UserId, oilMajorID);
        }

        public DataSet Get_Crew_Matrix_Configuration()
        {
            return objCrewDAL.Get_Crew_Matrix_Configuration();
        }
        public int UpdateCrewMatrixConfiguration(string Event, string KEYCONFIGURATION, string PARAMETERS, int DEFAULTVALUE, int CreatedBy)
        {
            return objCrewDAL.UpdateCrewMatrixConfiguration(Event, KEYCONFIGURATION, PARAMETERS, DEFAULTVALUE, CreatedBy);
        }

        public int InsertCrewMatrixConfiguration(string KEYCONFIGURATION, string PARAMETERS, string OPARAMETERS, int CreatedBy)
        {
            return objCrewDAL.InsertCrewMatrixConfiguration(KEYCONFIGURATION, PARAMETERS, OPARAMETERS, CreatedBy);
        }

        public DataSet getEnglishProficiency(int? CrewId)
        {

            return objCrewDAL.getEnglishProficiency(CrewId);
        }

        public int InsertEnglishProficiency(int CrewId, string ENGLISHPROFICIENCY, int CreatedBy)
        {

            return objCrewDAL.InsertEnglishProficiency(CrewId, ENGLISHPROFICIENCY, CreatedBy);
        }

        /// <summary>
        /// Get Crew matrix details
        /// </summary>
        /// <param name="oilMajorID"></param>
        /// <param name="EventId"></param>
        /// <param name="VesselID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public DataSet GetCrewMatrix(int oilMajorID, int EventId, int VesselID, string Date)
        {
            return objCrewDAL.GetCrewMatrix(oilMajorID, EventId, VesselID, Date);
        }


        public int InsertCrewMatrixGroup(int GroupID, int RankId, int CreatedBy, int Active_Status)
        {
            return objCrewDAL.InsertCrewMatrixGroup(GroupID, RankId, CreatedBy, Active_Status);
        }
        public DataSet GetCrewMatrixGroup(int GroupID)
        {
            return objCrewDAL.GetCrewMatrixGroup(GroupID);
        }

        public DataSet GetOilMajor()
        {

            return objCrewDAL.GetOilMajor();
        }

        public int InsertAdditionalRule(int Parameters, string rule, int CreatedBy)
        {
            return objCrewDAL.InsertAdditionalRule(Parameters, rule, CreatedBy);
        }

        public DataSet getAdditionalRule()
        {
            return objCrewDAL.getAdditionalRule();
        }

        public int UpdateAdditionalRule(int Parameters, string rule, int CreatedBy, int ID)
        {
            return objCrewDAL.UpdateAdditionalRule(Parameters, rule, CreatedBy, ID);
        }

        public int DeleteAdditionalRule(int ID)
        {
            return objCrewDAL.DeleteAdditionalRule(ID);
        }

        public int InsertCRWAddtional_Rule_Mapping(int RuleId, int OilMajorId, string key, string value, int CreatedBy, int IsActive)
        {
            return objCrewDAL.InsertCRWAddtional_Rule_Mapping(RuleId, OilMajorId, key, value, CreatedBy, IsActive);
        }

        public int InsertCRWAddtional_Rule_Mapping(int EditParentId, int RuleId, int OilMajorId, string key, string value, int CreatedBy, int IsActive, ref int ParentID)
        {
            return objCrewDAL.InsertCRWAddtional_Rule_Mapping(EditParentId, RuleId, OilMajorId, key, value, CreatedBy, IsActive, ref ParentID);
        }
        public DataSet GetAddtionalRuleMapping(int? RuleId, int? OilMajorId)
        {
            return objCrewDAL.GetAddtionalRuleMapping(RuleId, OilMajorId);
        }

        public int DeleteAdditionalRuleMapping(int RuleId, int OilMajorID, int ParentId)
        {
            return objCrewDAL.DeleteAdditionalRuleMapping(RuleId, OilMajorID, ParentId);
        }
        public DataSet GetCrewMatrixRankByGroup()
        {
            return objCrewDAL.GetCrewMatrixRankByGroup();
        }

        /// <summary>
        /// Active unactive additional rule
        /// </summary>
        /// <param name="RuleId"></param>
        /// <param name="OilMajorID"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public int ActiveUnactiveAdditionalRule(int RuleId, int OilMajorID, bool IsActive, int ParentId)
        {
            return objCrewDAL.ActiveUnactiveAdditionalRule(RuleId, OilMajorID, IsActive, ParentId);
        }

        /// <summary>
        /// CRUD Operation for Crew details race library
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable CRUD_Race(string Race, string Action, int UserId, int RaceID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_Race(Race, Action, UserId, RaceID, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        /// <summary>
        /// CRUD Operation for Crew details Veteran Status
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable CRUD_VeteranStatus(string VeteranStatus, string Action, int UserId, int VeteranStatusID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_VeteranStatus(VeteranStatus, Action, UserId, VeteranStatusID, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        /// <summary>
        /// CRUD Operation for Crew details school
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable CRUD_School(string School, string Action, int UserId, int SchoolID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_School(School, Action, UserId, SchoolID, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        /// <summary>
        /// CRUD Operation for Crew details Union
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataSet CRUD_Union(string Union, string Action, int UserId, int UnionID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_Union(Union, Action, UserId, UnionID, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }


        /// <summary>
        /// CRUD Operation for Crew details Union Branch
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataSet CRUD_UnionBranch(string UnionBranch, int UnionBranchID, int UnionID, string AddressLine1, string AddressLine2, string City, string State, int Country, string ZipCode, string PhoneNumber, string Email, string Action, int UserId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_UnionBranch(UnionBranch, UnionBranchID, UnionID, AddressLine1, AddressLine2, City, State, Country, ZipCode, PhoneNumber, Email, Action, UserId, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        /// <summary>
        /// CRUD Operation for Crew details Union Branch
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataSet CRUD_UnionBranchUS(string UnionBranch, int UnionBranchID, int UnionID, string Address, int Country, string PhoneNumber, string Email, string Action, int UserId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_UnionBranchUS(UnionBranch, UnionBranchID, UnionID, Address, Country, PhoneNumber, Email, Action, UserId, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        public DataSet CRW_GetCDConfiguration(String Key)
        {
            return objCrewDAL.CRW_GetCDConfiguration(Key);
        }
        public int CRW_UpdateConfig(DataTable dt)
        {
            return objCrewDAL.CRW_UpdateConfig(dt);
        }

        public int CRW_UpdateConfigFields(int ID, string Key, string DisplayName)
        {
            return objCrewDAL.CRW_UpdateConfigFields(ID, Key, DisplayName);
        }


        /// <summary>
        /// CRUD Operation for Crew details Union book
        /// </summary>
        /// <param name="Race"></param>
        /// <param name="Action"></param>
        /// <param name="UserId"></param>
        /// <param name="RaceID"></param>
        /// <param name="searchtext"></param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <param name="isfetchcount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable CRUD_UnionBook(string UnionBook, string Action, int UserId, int UnionBookID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_UnionBook(UnionBook, Action, UserId, UnionBookID, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }

        public DataTable CRW_LIB_ExportUnionWithBranchs(string Union)
        {
            return objCrewDAL.CRW_LIB_ExportUnionWithBranchs(Union);
        }
        public DataSet CRW_CD_GetConfidentialDetails(int? CrewID)
        {
            return objCrewDAL.CRW_CD_GetConfidentialDetails(CrewID);
        }
        public int CRW_CD_UPDConfidentialDetails(decimal? Height, decimal? Weight, decimal? Waist, int? PermanentStatus, string SSN,
                                                   int? Union,
                                                   int? UnionBranch,
                                                   int? UnionBook,
                                                   int? School,
                                                   string SchoolYearGraduated,
                                                   DateTime? HireDate,
                                                   int? Race,
                                                   string IDNumber,
                                                   int? VeteranStatus,
                                                   int? Naturaliztion,
                                                   DateTime? NaturaliztionDate,
                                                   string CustomField1,
                                                   string CustomField2,
                                                   string CustomField3,
                                                   int CrewID,
                                                   string IssuePlaceM,
            string IssuePlaceT, string MMC, DateTime? IssueDateM, DateTime? @IssueDAteT, DateTime? ExpiryDateM, string TWIC, DateTime? ExpirtDateT
        , string @IssuePlaceS, DateTime? IssueDateS, string @Seaman, DateTime? ExpirtDateS, string TshirtSize, string Cargosize, string OverallSize, string ShoeSize
         , string USVisa, DateTime? UsVisaIssue, string USVisaNumber, DateTime? UsVisaExpiry, int ModifiedBy
            )
        {
            return objCrewDAL.CRW_CD_UPDConfidentialDetails(Height, Weight, Waist,  PermanentStatus, SSN, Union, UnionBranch, UnionBook, School, SchoolYearGraduated, HireDate, Race, IDNumber, VeteranStatus, Naturaliztion,
                         NaturaliztionDate,
                         CustomField1,
                         CustomField2,
                         CustomField3,
                          CrewID, IssuePlaceM, IssuePlaceT, MMC, IssueDateM, IssueDAteT, ExpiryDateM, TWIC, ExpirtDateT, IssuePlaceS, IssueDateS, Seaman, ExpirtDateS, TshirtSize, Cargosize, OverallSize, ShoeSize, USVisa, UsVisaIssue, USVisaNumber, UsVisaExpiry,ModifiedBy);

        }

        public DataSet CRUD_PermanentStatus(string Status, string Action, int UserId, int StatusID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            return objCrewDAL.CRUD_PermanentStatus(Status, Action, UserId, StatusID, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
        }
        public int UPD_CrewDetails_Personal(int CrewID, string updatePanel, string Address1, string Address2, string City, string State, string Fax, int? country, string Zipcode, string Address, string NearestAirport, int? AirportID, int? Veteran, int? School,string SchoolYear, int? Naturalization, string English, DateTime? Naturalizationdate, string Remark,  string CNF1, string CNF2, string CNF3, string Mobile, string Email, string USVisa, DateTime? USVisaExpiry, string USVisaNo, DateTime? USVisaIssue)
        {
            return objCrewDAL.UPD_CrewDetails_Personal(CrewID, updatePanel, Address1, Address2, City, State,Fax, country, Zipcode, Address, NearestAirport, AirportID, Veteran, School,SchoolYear, Naturalization, English, Naturalizationdate, Remark,  CNF1, CNF2, CNF3,Mobile,Email,USVisa,USVisaExpiry,USVisaNo,USVisaIssue);
        }
          public int CRW_CD_UPD_BankAllotment(int CrewID,string Allotment_AccType)
        {
            return objCrewDAL.CRW_CD_UPD_BankAllotment(CrewID, Allotment_AccType);
        }
          public int CRW_CD_UPDATE_MMC(string IssuePlaceM, string MMC, DateTime? IssueDateM, DateTime? ExpiryDateM, int CrewID)
          {
              return objCrewDAL.CRW_CD_UPDATE_MMC(IssuePlaceM, MMC, IssueDateM, ExpiryDateM, CrewID);
          }

          public int CRW_CD_UPDATE_TWIC(string IssuePlaceT, string TWIC, DateTime? IssueDateT, DateTime? ExpiryDateT, int CrewID)
          {
              return objCrewDAL.CRW_CD_UPDATE_TWIC(IssuePlaceT, TWIC, IssueDateT, ExpiryDateT, CrewID);
          }
          public int CRW_CD_Validate_SSN(string SSN,int CrewID)
          {
              return objCrewDAL.CRW_CD_Validate_SSN(SSN, CrewID);
          }

          public DataTable CRW_LIB_AddressValidationSetting()
          {
              return objCrewDAL.CRW_LIB_AddressValidationSetting();
          }

          public DataTable CRW_LIB_Export_OilMajor()
          {
              return objCrewDAL.CRW_LIB_Export_OilMajor();
          }
    }
}
