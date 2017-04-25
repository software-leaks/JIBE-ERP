using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_Company
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{

    public class BLL_Infra_Company
    {
        DAL_Infra_Company objDAL = new DAL_Infra_Company();

        public BLL_Infra_Company()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable SearchCompany(string searchtext, int? companytypeid, int? countryincorpid, int? currencyid, int? countryid
                                                 , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchCompany(searchtext, companytypeid, countryincorpid, currencyid, countryid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataTable SearchCompany_verify(string searchtext, int? companytypeid, int? countryincorpid, int? currencyid, int? countryid, string compID
                                                , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchCompany_Verify(searchtext, companytypeid, countryincorpid, currencyid, countryid, compID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet Get_Company_Parent_Child(int RelationshipType_Id, int Parent_Company_ID, int UserID)
        {
            return objDAL.Get_Company_Parent_Child_DL(RelationshipType_Id, Parent_Company_ID, UserID);
        }


        public DataTable Get_CompanyList()
        {
            try
            {
                return objDAL.Get_CompanyList_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyListVerified()
        {
            try
            {
                return objDAL.Get_CompanyList_Verified_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyListMini()
        {
            try
            {
                return objDAL.Get_CompanyListMini_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyListMini(int UserCompany)
        {
            try
            {
                return objDAL.Get_CompanyListMini_DL(UserCompany);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CompanyType(int CompanyId)
        {
            try
            {
                return objDAL.Get_CompanyType_DL(CompanyId);
            }
            catch
            {
                throw;
            }
        }


        public DataTable Get_Company_By_User_Type(int UserCompany, string User_type)
        {

            try
            {

                return objDAL.Get_Company_By_User_Type(UserCompany, User_type);

            }
            catch
            {

                throw;
            }


        }

        public DataTable Get_VesselType()
        {
            try
            {
                return objDAL.Get_VesselType_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CompanyListByType(int CompanyType, int UserCompany)
        {
            try
            {
                return objDAL.Get_CompanyListByType_DL(CompanyType, UserCompany);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyRelationType(int UserID)
        {
            try
            {
                return objDAL.Get_CompanyRelationType_DL(UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyTypeList_ByType()
        {
            try
            {
                return objDAL.Get_CompanyTypeList_ByType_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyTypeList()
        {
            try
            {
                return objDAL.Get_CompanyTypeList_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyTypeList(string CompanyType)
        {
            try
            {
                return objDAL.Get_CompanyTypeList_DL(CompanyType);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CompanyDepartmentList(int CompanyID)
        {
            try
            {
                return objDAL.Get_CompanyDepartmentList_DL(CompanyID);
            }
            catch
            {
                throw;
            }
        }

        public int EditCompany(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country, string Email1, string Phone1)
        {
            try
            {
                return objDAL.EditCompany_DL(ID, Company_Code, Company_TypeID, Company_Name, Reg_Number, Date_Of_Incorp, Country_Of_Incorp, Base_Curr, Address, Country, Email1, Phone1);
            }
            catch
            {
                throw;
            }
        }



        public int EditCompany(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Short_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr
            , string Address, int? Country, string Email1, string Phone1, string Email2, string Phone2, string Fax1, string Fax2, int Created_By)
        {
            try
            {

                return objDAL.EditCompany_DL(ID, Company_Code, Company_TypeID, Company_Name, Short_Name, Reg_Number, Date_Of_Incorp, Country_Of_Incorp, Base_Curr, Address, Country, Email1, Phone1, Email2, Phone2, Fax1, Fax2, Created_By);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Description: Added an overloaded method to save the date format settings
        /// Created By: Krishnapriya
        /// </summary>
        /// <param name="DateFormat">Selected date format will be sent as parameter</param>
        /// <returns></returns>
        public int EditCompany(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Short_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr
          , string Address, int? Country, string Email1, string Phone1, string Email2, string Phone2, string Fax1, string Fax2, int Created_By, string DateFormat)
        {
            try
            {

                return objDAL.EditCompany_DL(ID, Company_Code, Company_TypeID, Company_Name, Short_Name, Reg_Number, Date_Of_Incorp, Country_Of_Incorp, Base_Curr, Address, Country, Email1, Phone1, Email2, Phone2, Fax1, Fax2, Created_By, DateFormat);
            }
            catch
            {
                throw;
            }

        }

        public int VerifyCompany(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Short_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr
           , string Address, int? Country, string Email1, string Phone1, string Email2, string Phone2, string Fax1, string Fax2, int Created_By, bool verified)
        {
            try
            {

                return objDAL.VerifyCompany_DL(ID, Company_Code, Company_TypeID, Company_Name, Short_Name, Reg_Number, Date_Of_Incorp, Country_Of_Incorp, Base_Curr, Address, Country, Email1, Phone1, Email2, Phone2, Fax1, Fax2, Created_By, verified);
            }
            catch
            {
                throw;
            }

        }

        public int InsertCompany(int Company_Code, int Company_TypeID, string Company_Name, string Reg_Number, DateTime? Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country, string Email1, string Phone1, int Created_By, string Short_Name, string Email2, string Phone2, string Fax1, string Fax2)
        {
            try
            {
                return objDAL.InsertCompany_DL(Company_Code, Company_TypeID, Company_Name, Reg_Number, Date_Of_Incorp, Country_Of_Incorp, Base_Curr, Address, Country, Email1, Phone1, Created_By, Short_Name, Email2, Phone2, Fax1, Fax2);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Description: Added an overloaded method to save the date format settings
        /// Created By: Krishnapriya
        /// </summary>
        /// <param name="DateFormat">Selected date format will be sent as parameter</param>
        /// <returns></returns>
        public int InsertCompany(int Company_Code, int Company_TypeID, string Company_Name, string Reg_Number, DateTime? Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country, string Email1, string Phone1, int Created_By, string Short_Name, string Email2, string Phone2, string Fax1, string Fax2, string DateFormat)
        {
            try
            {
                return objDAL.InsertCompany_DL(Company_Code, Company_TypeID, Company_Name, Reg_Number, Date_Of_Incorp, Country_Of_Incorp, Base_Curr, Address, Country, Email1, Phone1, Created_By, Short_Name, Email2, Phone2, Fax1, Fax2, DateFormat);
            }
            catch
            {
                throw;
            }
        }

        public DataTable CheckCompanyExist(string Short_Name)
        {
            try
            {
                return objDAL.CheckCompany_Exist_DL(Short_Name);
            }
            catch
            {
                throw;
            }
        }

        public int SendSurveyCompanyRegistration(string EmailID, string ContactDetails, string CompanyName)
        {
            try
            {
                return objDAL.Mail_Register_SurveyCompany_DL(EmailID, ContactDetails, CompanyName);
            }
            catch
            {
                throw;
            }
        }

        //
        public int SendSurveyVesselORegistration(string EmailID, string ContactDetails, string CompanyName, string RegisterLink)
        {
            try
            {
                return objDAL.Mail_Register_SRVVesselOwner_DL(EmailID, ContactDetails, CompanyName, RegisterLink);
            }
            catch
            {
                throw;
            }
        }

        // public int SendSurveyCompanyVerification(string EmailID, string CompanyName, string fullCompanyName, int CompanyID, int Created_By, string Email)
        public DataTable SendSurveyCompanyVerification(string EmailID, string CompanyName, string fullCompanyName, int CompanyID, int Created_By, string Email)
        {
            try
            {
                return objDAL.Mail_Verify_SurveyCompany_DL(EmailID, CompanyName, fullCompanyName, CompanyID, Created_By, Email);
            }
            catch
            {
                throw;
            }
        }

        public int InsertCompanyReletionship(int ParentCompanyID, int ChildCompanyID, int RelationshipID, int UserID)
        {
            try
            {
                return objDAL.InsertCompanyReletionship_DL(ParentCompanyID, ChildCompanyID, RelationshipID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public int UpdateCompanyReletionship(int ParentCompanyID, DataTable dtChild, int RelationshipID, int UserID)
        {
            try
            {
                return objDAL.UpdateCompanyReletionship_DL(ParentCompanyID, dtChild, RelationshipID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CompanyRelationships(int CompanyID, int UserID)
        {
            try
            {
                return objDAL.Get_CompanyRelationships_DL(CompanyID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCompany_DL(int ID, int Created_By)
        {
            try
            {
                return objDAL.DeleteCompany_DL(ID, Created_By);
            }
            catch
            {
                throw;
            }
        }



        public int Insert_Bunker_Testing_Lab(string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            try
            {
                return objDAL.Insert_Bunker_Testing_Lab_DL(Lab_Name, Address, EMail, Phone, CountryID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Update_Bunker_Testing_Lab(int ID, string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            try
            {
                return objDAL.Update_Bunker_Testing_Lab_DL(ID, Lab_Name, Address, EMail, Phone, CountryID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_BunkerTestingLabList(string SearchText, int CountryID, int UserID, string sortbycoloumn, int? sortdirection, int CurrentPageIndex, int PageSize, ref int rowcount)
        {
            return objDAL.Get_BunkerTestingLabList_DL(SearchText, CountryID, UserID, sortbycoloumn, sortdirection, CurrentPageIndex, PageSize, ref rowcount);
        }
        public DataTable Get_BunkerTestingLabByID(int ID, int UserID)
        {
            return objDAL.Get_BunkerTestingLabByID_DL(ID, UserID);
        }
        public DataSet Get_Dep_Cat_SubCat(string user)
        {
            return objDAL.Get_Dep_Cat_SubCat(user);
        }  
    }

}