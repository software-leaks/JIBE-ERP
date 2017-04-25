using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data;
using SMS.Data.Crew;
using SMS.Data.PortageBill;



namespace SMS.Business.PortageBill
{
    public class BLL_PortageBill
    {
        public static DataSet Get_CrewWagesByVoyage(int CrewID, int VoyageID)
        {
            try
            {
                return DAL_PortageBill.Get_CrewWagesByVoyage_DL(CrewID, VoyageID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_CrewWagesByVoyage_ForAgreement(int CrewID, int VoyageID)
        {
            try
            {
                return DAL_PortageBill.Get_CrewWagesByVoyage_ForAgreement_DL(CrewID, VoyageID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_Crew_BankAccList(int CrewID)
        {
            try
            {

                return DAL_PortageBill.Get_Crew_BankAccList_DL(CrewID);

            }
            catch
            {
                throw;
            }
            finally
            {
                
            }
        }
        public static DataTable Get_Crew_BankAccDetails(int AccID)
        {
            try
            {

                return DAL_PortageBill.Get_Crew_BankAccDetails_DL(AccID);

            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public static int ACC_Insert_BankAccounts(int CrewID, string Beneficiary, string Acc_No, string Bank_Name, string Bank_Address, int Default_Acc, int Verified, int Created_By, string SwiftCode, string Bank_Code, string Branch_Code, int Account_Curr, int MOBank_ID)
       
        {
            try
            {

                return DAL_PortageBill.ACC_Insert_BankAccounts_DL(CrewID, Beneficiary, Acc_No, Bank_Name, Bank_Address, Default_Acc, Verified, Created_By, SwiftCode, Bank_Code, Branch_Code, Account_Curr, MOBank_ID);

            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public static int ACC_Update_BankAccounts(int ID, int CrewID, string Beneficiary, string Acc_No, string Bank_Name, string Bank_Address, int Default_Acc, int Verified, int Modified_By, string SwiftCode, string Bank_Code, string Branch_Code, int Account_Curr, int MOBank_ID)
        {
            try
            {
                return DAL_PortageBill.ACC_Update_BankAccounts_DL(ID, CrewID, Beneficiary, Acc_No, Bank_Name, Bank_Address, Default_Acc, Verified, Modified_By, SwiftCode, Bank_Code, Branch_Code, Account_Curr, MOBank_ID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public static int ACC_Del_BankAccounts(int ID, int CrewID, int Deleted_By)
        {
            try
            {

                return DAL_PortageBill.ACC_Del_BankAccounts_DL(ID, CrewID, Deleted_By);

            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public static int CRW_GET_Wage_Status(int CrewID, int VoyID)
        {
            try
            {
                return DAL_PortageBill.CRW_GET_Wage_Status(CrewID, VoyID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_CrewWageContract(int CrewID, int VoyID)
        {

            try
            {
                return DAL_PortageBill.Get_CrewWageContract_DL(CrewID, VoyID);
            }
            catch
            {
                throw;
            }

        }
        public static DataSet Get_CrewWageContract(int CrewID, int VoyID, int UserID)
        {

            try
            {
                return DAL_PortageBill.Get_CrewWageContract_DL(CrewID, VoyID, UserID);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_WageContract(int Vessel_Flag)
        {
            try
            {
                return DAL_PortageBill.Get_WageContract_DL(Vessel_Flag);
            }
            catch
            {
                throw;
            }

        }

        public static DataSet Get_Rank_WageContract()
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL();
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Rank_WageContract(int RankID)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL(RankID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Rank_WageContract(int RankID, int WageContractID)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL(RankID, WageContractID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Rank_WageContract(int RankID, int Contract_Type, int WageContractID)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL(RankID, Contract_Type, WageContractID);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Rank_WageContract(int RankID, int Contract_Type, int WageContractID,int NationalityConsidered,int CountryId,int RankScaleId,int VesselId)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL(RankID, Contract_Type, WageContractID, NationalityConsidered, CountryId, RankScaleId, VesselId);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Rank_WageContract(int RankID, int Contract_Type, int WageContractID, int NationalityConsidered, int CountryId, int RankScaleId)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL(RankID, Contract_Type, WageContractID, NationalityConsidered, CountryId, RankScaleId);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Get_Rank_WageContract_AddNew(int RankID, int Contract_Type, int NationalityConsidered, int CountryId, int RankScaleId)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_AddNew_DL(RankID, Contract_Type, NationalityConsidered, CountryId, RankScaleId);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet ACC_Get_ExchRateSS()
        {

            try
            {
                return DAL_PortageBill.ACC_Get_ExchRateSS();
            }
            catch
            {
                throw;
            }

        }
        public static DataSet CRW_BPShip_GetSalary_Rank(int id, int rank, string scaleeffecDT)
        {
            try
            {
                return DAL_PortageBill.CRW_BPShip_GetSalary_Rank(id, rank, scaleeffecDT);
            }
            catch
            {
                throw;
            }

        }
        public static DataSet Ins_CrewWages(string xmlwages)
        {
            try
            {
                return DAL_PortageBill.Ins_CrewWages(xmlwages);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Ins_CrewWages(DataTable dtWages)
        {
            try
            {
                return DAL_PortageBill.Ins_CrewWages(dtWages);
            }
            catch
            {
                throw;
            }
        }
        public static int Ins_Crew_SideLetter(int VoyageID, decimal Amount, int UserID)
        {
            try
            {
                return DAL_PortageBill.Ins_Crew_SideLetter_DL( VoyageID, Amount,  UserID);
            }
            catch
            {
                throw;
            }
        }
        
        public static DataTable Get_Salary_Types()
        {
            try
            {
                return DAL_PortageBill.Get_Salary_Types_DL();
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_Wages_EntryType()
        {
            try
            {
                return DAL_PortageBill.Get_Wages_EntryType_DL();
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Wages_EntryType_Deduction()
        {
            try
            {
                return DAL_PortageBill.Get_Wages_EntryType_Deduction_DL();
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Rule_Type()
        {
            try
            {
                return DAL_PortageBill.Get_Rule_Type_DL();
            }
            catch
            {
                throw;
            }
        }

        public static DataSet Ins_Rank_Wage_Contract(DataTable dtWages)
        {
            try
            {
                return DAL_PortageBill.Ins_Rank_Wage_Contract_DL(dtWages);
            }
            catch
            {
                throw;
            }
        }

        public static int AutoPopulate_CrewWages(int VoyID, int UserID)
        {

            try
            {
                return DAL_PortageBill.AutoPopulate_CrewWages_DL(VoyID, UserID);
            }
            catch
            {
                throw;
            }

        }

        public static int Update_Seniority_Rates(DataTable dtRates, int RankID, int UserID)
        {

            try
            {
                return DAL_PortageBill.Update_Seniority_Rates_DL(dtRates, RankID, UserID);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_Seniority_Rates(int RankID, int UserID)
        {

            try
            {
                return DAL_PortageBill.Get_Seniority_Rates_DL(RankID, UserID);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_SeniorityRecords(int FleetID, int Vessel_ID, int Rank, string Month, string Year, string SearchText, int? CrewID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            try
            {
                return DAL_PortageBill.Get_SeniorityRecords_DL(FleetID, Vessel_ID, Rank, Month, Year, SearchText, CrewID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
            }
            catch
            {
                throw;
            }
        }

        public static int Update_CrewSeniorityYear(int VoyID, int SeniorityYear, int UserID)
        {

            try
            {
                return DAL_PortageBill.Update_CrewSeniorityYear_DL(VoyID, SeniorityYear, UserID);
            }
            catch
            {
                throw;
            }

        }

        public static DataSet Get_Rank_WageContract(int RankID, int Contract_Type, int NationalityConsidered, int CountryId, int RankScaleId)
        {
            try
            {
                return DAL_PortageBill.Get_Rank_WageContract_DL(RankID, Contract_Type, NationalityConsidered, CountryId, RankScaleId);
            }
            catch
            {
                throw;
            }
        }
        public static DataSet Ins_CrewWages(DataTable dtWages, int WageContractId)
        {
            try
            {
                return DAL_PortageBill.Ins_CrewWages(dtWages, WageContractId);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_CompanySeniority_Rates(int RankID)
        {

            try
            {
                return DAL_PortageBill.Get_CompanySeniority_Rates_DL(RankID);
            }
            catch
            {
                throw;
            }
        }

        public static int Save_Company_Seniority_Rates(int RankID,int CompanySeniorityYear,decimal CompanySeniorityAmount, int UserID)
        {

            try
            {
                return DAL_PortageBill.Save_Company_Seniority_Rates_DL(RankID,CompanySeniorityYear,CompanySeniorityAmount,UserID);
            }
            catch
            {
                throw;
            }
        }

        public static int Update_Company_Seniority_Rates(DataTable dtRates, int RankID, int UserID)
        {

            try
            {
                return DAL_PortageBill.Update_Company_Seniority_Rates_DL(dtRates, RankID, UserID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_NationalityForWages(int RankID, int Contract_Type)
        {
            try
            {
                return DAL_PortageBill.Get_NationalityForWages_DL(RankID, Contract_Type);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_CrewSeniorityRecords(int Rank, string Status, int CompanySeniorityYear,string searchText, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            try
            {
                return DAL_PortageBill.Get_CrewSeniorityRecords_DL(Rank, Status, CompanySeniorityYear, searchText,PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_CrewCompanySeniority(int CrewID, int SeniorityYear,int SeniorityDays, string Remarks,DateTime CompanyEffectiveDate,int UserID)
        {
            try
            {
                return DAL_PortageBill.Update_CrewCompanySeniority_DL(CrewID, SeniorityYear, SeniorityDays, Remarks, CompanyEffectiveDate,UserID);
            }
            catch
            {
                throw;
            }
        }
        public static int GET_Crew_CompanySeniorityAmount(int CrewID, int RankId)
        {
            try
            {
                return DAL_PortageBill.GET_Crew_CompanySeniorityAmount_DL(CrewID, RankId);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable GET_Crew_RankSeniorityAmount(int CrewID, int RankId, int VoyID)
        {
            try
            {
                return DAL_PortageBill.GET_Crew_RankSeniorityAmount_DL(CrewID, RankId, VoyID);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_CrewRankSeniorityYear(int CrewID, int RankId, int SeniorityYear, int SeniorityDays, int VoyageId, string Remarks, DateTime RankEffectiveDate, int UserID)
        {
            try
            {
                return DAL_PortageBill.Update_CrewRankSeniorityYear_DL(CrewID, RankId, SeniorityYear, SeniorityDays, VoyageId, Remarks, RankEffectiveDate, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static int Update_CrewRankSeniority(int CrewID,int RankId, int SeniorityYear, int SeniorityDays,int VoyageId,string Remarks, DateTime RankEffectiveDate,int UserID)
        {
            try
            {
                return DAL_PortageBill.Update_CrewRankSeniority_DL(CrewID,RankId, SeniorityYear,SeniorityDays, VoyageId,Remarks,RankEffectiveDate, UserID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable GetRejoiningBonus(int CrewID)
        {
            try
            {
                return DAL_PortageBill.GetRejoiningBonus(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_RJBDetails(int ID)
        {
            try
            {
                return DAL_PortageBill.Get_RJBDetails(ID);
            }
            catch
            {
                throw;
            }
        }
        public static int CopyRankWageFromExistingCountry(int VesselFlag,int RankId,int CountryFromId,DateTime EffectiveDate,int Currency,int CompanyId,DataTable dtCountryToList,int CreatedBy)
        {
            try
            {
                return DAL_PortageBill.CopyRankWageFromExistingCountry_DL(VesselFlag, RankId, CountryFromId,EffectiveDate,Currency,CompanyId, dtCountryToList, CreatedBy);
            }
            catch
            {
                throw;
            }
        }



        public static DataTable Get_NationalityGroupForWege_Contract(int RankID)
        {
            try
            {
                return DAL_PortageBill.Get_NationalityGroupForWege_Contract_DL(RankID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Check_NationalityGroup(int RankID, int NationalityGroupId, String NationalityGroupName, DataTable dt)
        {
            try
            {
                return DAL_PortageBill.Check_NationalityGroup_DL(RankID, NationalityGroupId, NationalityGroupName, dt);
            }
            catch
            {
                throw;
            }
        }


        public static int Insert_NationalityGroup(int RankID, int NationalityGroupId, String NationalityGroupName, DataTable dt, int UserId)
        {
            try
            {
                return DAL_PortageBill.Insert_NationalityGroup_DL(RankID, NationalityGroupId, NationalityGroupName, dt, UserId);
            }
            catch
            {
                throw;
            }
        }


        public static DataSet Get_RankWise_GroupDetails(int NationalityGroupId)
        {
            try
            {
                return DAL_PortageBill.Get_RankWise_GroupDetails_DL(NationalityGroupId);
            }
            catch
            {
                throw;
            }
        }
        public static int DeleteNationalityGroup(int NationalityGroupId, int UserId)
        {
            try
            {
                return DAL_PortageBill.DeleteNationalityGroup_DL(NationalityGroupId, UserId);
            }
            catch
            {
                throw;
            }
        }


        public static DataTable Get_NationalityGroupbyGroupID(int NationalityGroupId)
        {
            try
            {
                return DAL_PortageBill.Get_NationalityGroupbyGroupID_DL(NationalityGroupId);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_VesselType()
        {
            try
            {
                return DAL_PortageBill.Get_VesselType_DL();
            }
            catch
            {
                throw;
            }
        }


        public static DataTable Get_VesselList(int vesselTypeID)
        {
            try
            {
                return DAL_PortageBill.Get_VesselList_DL(vesselTypeID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_VesselSpecificWage(int VesselTypeID, int WageContractId, int SalaryCode)
        {
            try
            {
                return DAL_PortageBill.Get_VesselSpecificWage_DL(VesselTypeID,WageContractId, SalaryCode);
            }
            catch
            {
                throw;
            }
        }
        public static int Ins_CrewChangeWageLog(int RankId, int RankScaleId, int CrewId,int UserID)
        {

            try
            {
                return DAL_PortageBill.Ins_CrewChangeWageLog_DL(RankId, RankScaleId,CrewId, UserID);
            }
            catch
            {
                throw;
            }

        }
     
    }

}
