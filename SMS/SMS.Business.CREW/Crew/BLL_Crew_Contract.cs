using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_Contract
    {
        DAL_Crew_Contract objDAL = new DAL_Crew_Contract();

        public BLL_Crew_Contract()
        {
            
        }
        public DataTable Get_ContractList(int ContractId)
        {
            return objDAL.Get_ContractList_DL(ContractId);
        }
        public int Insert_Contract(String ContractType,DataTable dt, string ContractName, int UserId)
        {
            return objDAL.Insert_Contract_DL(ContractType,dt, ContractName, UserId);
        }
        public int Update_Contract(int ContractId, String ContractType, DataTable dt, string ContractName, int UserId)
        {
            return objDAL.Update_Contract_DL(ContractId,ContractType, dt, ContractName, UserId);
        }
        public int DELETE_Contract(int ID, int Deleted_By)
        {
            return objDAL.DELETE_Contract_DL(ID, Deleted_By);
        }
        public DataTable Get_NationalityList(int ContractId)
        {
            try
            {
                return objDAL.Get_NationalityList_DL(ContractId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_VesselFlagList(int ContractId)
        {
            try
            {
                return objDAL.Get_VesselFlagList_DL(ContractId);
            }
            catch
            {
                throw;
            }
        }
        public int INS_NationalityList(int DocTypeID, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_NationalityList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public int INS_VesselFlagList(int DocTypeID, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_VesselFlagList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_ContractTemplateList()
        {
            return objDAL.Get_ContractTemplateList_DL();
        }
        public DataTable Get_CrewContractList(int CountryId,int VesselFlag)
        {
            try
            {
                return objDAL.Get_CrewContractList_DL(CountryId, VesselFlag);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_ContractTemplate(int ContractId)
        {
            try
            {
                return objDAL.Get_ContractTemplate_DL(ContractId);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Crew_Contract_Period_Search(string searchtext, int? Rank, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Crew_Contract_Period_Search(searchtext, Rank, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public DataTable Get_Crew_Contract_Period_List(int? ID)
        {
            return objDAL.Get_Crew_Contract_Period_List(ID);
        }


        public int Insert_Crew_Contract_Period(int? Rank, int? Days, int Created_By)
        {
            return objDAL.Insert_Crew_Contract_Period(Rank, Days, Created_By);
        }

        public int Edit_Crew_Contract_Period(int? ID, int? Rank, int? Days, int Created_By)
        {
            return objDAL.Edit_Crew_Contract_Period(ID, Rank, Days, Created_By);
        }


        public int Delete_Crew_Contract_Period(int ID, int Created_By)
        {
            return objDAL.Delete_Crew_Contract_Period(ID, Created_By);
        }

        public DataTable Crew_Contract_Withhold_Search(string searchtext, string Withhold_Type, int? Entry_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Crew_Contract_Withhold_Search(searchtext, Withhold_Type, Entry_Type, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public DataTable Get_Crew_Contract_Withhold_List(int? ID)
        {
            return objDAL.Get_Crew_Contract_Withhold_List(ID);
        }


        public int Insert_Crew_Contract_Withhold(int Contract_Number, Decimal? Withhold_Amount, string Withhold_Type, int? Entry_Type, DateTime? Effective_Date, int Created_By)
        {
            return objDAL.Insert_Crew_Contract_Withhold(Contract_Number, Withhold_Amount, Withhold_Type, Entry_Type, Effective_Date, Created_By);
        }

        public int Edit_Crew_Contract_Withhold(int? ID, int Contract_Number, Decimal? Withhold_Amount, string Withhold_Type, int? Entry_Type, DateTime? Effective_Date, int Created_By)
        {
            return objDAL.Edit_Crew_Contract_Withhold(ID, Contract_Number, Withhold_Amount, Withhold_Type, Entry_Type, Effective_Date, Created_By);
        }


        public int Delete_Crew_Contract_Withhold(int ID, int Created_By)
        {
            return objDAL.Delete_Crew_Contract_Withhold(ID, Created_By);
        }



        public DataTable Get_Crew_Contract_Withhold_Entry_Type()
        {

            return objDAL.Get_Crew_Contract_Withhold_Entry_Type();

        }
    }
}
