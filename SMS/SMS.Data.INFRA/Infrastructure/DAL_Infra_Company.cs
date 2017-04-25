using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


/// <summary>
/// Summary description for BLL_Infra_Company
/// </summary>
/// 
namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Company
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_Infra_Company(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_Company()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable SearchCompany(string searchtext, int? companytypeid, int? countryincorpid, int? currencyid, int? countryid
                , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Company_Type_ID", companytypeid),
                   new System.Data.SqlClient.SqlParameter("@Country_Incorp_ID", countryincorpid), 
                   new System.Data.SqlClient.SqlParameter("@Currency_ID", currencyid), 
                   new System.Data.SqlClient.SqlParameter("@Country_ID", countryid), 

                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Company_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable SearchCompany_Verify(string searchtext, int? companytypeid, int? countryincorpid, int? currencyid, int? countryid, string CompanyId
             , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Company_Type_ID", companytypeid),
                   new System.Data.SqlClient.SqlParameter("@Country_Incorp_ID", countryincorpid), 
                   new System.Data.SqlClient.SqlParameter("@Currency_ID", currencyid), 
                   new System.Data.SqlClient.SqlParameter("@Country_ID", countryid), 
                   new System.Data.SqlClient.SqlParameter("@Company_ID",CompanyId),

                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            // System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Company_Search", obj);
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Company_Search_Verify", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public DataSet Get_Company_Parent_Child_DL(int RelationshipType_Id, int Parent_Company_ID, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RelationshipType_Id", RelationshipType_Id),
                   new System.Data.SqlClient.SqlParameter("@Parent_Company_ID", Parent_Company_ID),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Company_Parent_Child", obj);
        }


        public DataTable Get_CompanyList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyList").Tables[0];
        }
        public DataTable Get_CompanyList_Verified_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyList_Verify").Tables[0];
        }
        public DataTable Get_CompanyListMini_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyListMini").Tables[0];
        }
        public DataTable Get_CompanyListMini_DL(int UserCompany)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@UserCompany", UserCompany) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyListMiniByUser", sqlprm).Tables[0];
        }

        public DataTable Get_CompanyType_DL(int CompanyId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@Company_ID", CompanyId) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_CompanyType", sqlprm).Tables[0];
        }

        public DataTable Get_VesselType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselType").Tables[0];

        }
        public DataTable Get_Company_By_User_Type(int UserCompanyId, string User_type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserCompanyId", UserCompanyId),
                new SqlParameter("@User_type", User_type)
            
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Company_By_UserTypeID", sqlprm).Tables[0];
        }




        public DataTable Get_CompanyListByType_DL(int CompanyType, int UserCompany)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                    new SqlParameter("@Company_TypeID", CompanyType) ,
                    new SqlParameter("@UserCompany", UserCompany) 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyListByType", sqlprm).Tables[0];
        }
        public DataTable Get_CompanyRelationType_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                    new SqlParameter("@UserID", UserID) 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyRelationType", sqlprm).Tables[0];
        }
        public DataTable Get_CompanyTypeList_ByType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_CompanyTypeList").Tables[0];
        }
        public DataTable Get_CompanyTypeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyTypeList").Tables[0];
        }

        public DataTable Get_CompanyTypeList_DL(string CompanyType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CompanyType",CompanyType)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyTypeList_ByType", sqlprm).Tables[0];

        }


        public DataTable Get_CompanyDepartmentList_DL(int CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CompanyID",CompanyID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_DepartmentList", sqlprm).Tables[0];

        }


        public int EditCompany_DL(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Short_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country
            , string Email1, string Phone1, string Email2, string Phone2, string Fax1, string Fax2, int Created_By)
        {
            DateTime DT_DATE_OF_INCORP = Convert.ToDateTime(Date_Of_Incorp);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Company_TypeID",Company_TypeID),
                                            new SqlParameter("@Company_Code",Company_Code),
                                            new SqlParameter("@Company_Name",Company_Name),
                                            new SqlParameter("@Reg_Number",Reg_Number),
                                            new SqlParameter("@Date_Of_Incorp",DT_DATE_OF_INCORP),
                                            new SqlParameter("@Country_Of_Incorp",Country_Of_Incorp),
                                            new SqlParameter("@Base_Curr",Base_Curr),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email1",Email1),
                                            new SqlParameter("@Phone1",Phone1),
                                            new SqlParameter("@Short_Name",Short_Name),
                                            new SqlParameter("@Email2",Email2),
                                            new SqlParameter("@Phone2",Phone2),
                                            new SqlParameter("@Fax1",Fax1),
                                            new SqlParameter("@Fax2",Fax2),
                                            new SqlParameter("@Created_By",Created_By) 

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Company", sqlprm);

        }

        /// <summary>
        /// Description: Added an overloaded method to save the date format settings
        /// Created By: Krishnapriya
        /// </summary>
        /// <param name="DateFormat">Selected date format will be sent as parameter</param>
        /// <returns></returns>
        public int EditCompany_DL(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Short_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country
            , string Email1, string Phone1, string Email2, string Phone2, string Fax1, string Fax2, int Created_By, string DateFormat)
        {
            DateTime DT_DATE_OF_INCORP = Convert.ToDateTime(Date_Of_Incorp);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Company_TypeID",Company_TypeID),
                                            new SqlParameter("@Company_Code",Company_Code),
                                            new SqlParameter("@Company_Name",Company_Name),
                                            new SqlParameter("@Reg_Number",Reg_Number),
                                            new SqlParameter("@Date_Of_Incorp",DT_DATE_OF_INCORP),
                                            new SqlParameter("@Country_Of_Incorp",Country_Of_Incorp),
                                            new SqlParameter("@Base_Curr",Base_Curr),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email1",Email1),
                                            new SqlParameter("@Phone1",Phone1),
                                            new SqlParameter("@Short_Name",Short_Name),
                                            new SqlParameter("@Email2",Email2),
                                            new SqlParameter("@Phone2",Phone2),
                                            new SqlParameter("@Fax1",Fax1),
                                            new SqlParameter("@Fax2",Fax2),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Date_Format", DateFormat)

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Company", sqlprm);

        }
        

        public int VerifyCompany_DL(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Short_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country
          , string Email1, string Phone1, string Email2, string Phone2, string Fax1, string Fax2, int Created_By, bool verified)
        {
            DateTime DT_DATE_OF_INCORP = Convert.ToDateTime(Date_Of_Incorp);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Company_TypeID",Company_TypeID),
                                            new SqlParameter("@Company_Code",Company_Code),
                                            new SqlParameter("@Company_Name",Company_Name),
                                            new SqlParameter("@Reg_Number",Reg_Number),
                                            new SqlParameter("@Date_Of_Incorp",DT_DATE_OF_INCORP),
                                            new SqlParameter("@Country_Of_Incorp",Country_Of_Incorp),
                                            new SqlParameter("@Base_Curr",Base_Curr),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email1",Email1),
                                            new SqlParameter("@Phone1",Phone1),
                                            new SqlParameter("@Short_Name",Short_Name),
                                            new SqlParameter("@Email2",Email2),
                                            new SqlParameter("@Phone2",Phone2),
                                            new SqlParameter("@Fax1",Fax1),
                                            new SqlParameter("@Fax2",Fax2),
                                            new SqlParameter("@Created_By",Created_By) ,
                                            new SqlParameter("@Verified",verified) 

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Company_Verify", sqlprm);

        }

        public int EditCompany_DL(int ID, int Company_Code, int Company_TypeID, string Company_Name, string Reg_Number, string Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country, string Email1, string Phone1)
        {
            DateTime DT_DATE_OF_INCORP = Convert.ToDateTime(Date_Of_Incorp);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Company_TypeID",Company_TypeID),
                                            new SqlParameter("@Company_Code",Company_Code),
                                            new SqlParameter("@Company_Name",Company_Name),
                                            new SqlParameter("@Reg_Number",Reg_Number),
                                            new SqlParameter("@Date_Of_Incorp",DT_DATE_OF_INCORP),
                                            new SqlParameter("@Country_Of_Incorp",Country_Of_Incorp),
                                            new SqlParameter("@Base_Curr",Base_Curr),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email1",Email1),
                                            new SqlParameter("@Phone1",Phone1),
                                            new SqlParameter("@Short_Name","")

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Company", sqlprm);

        }

        public int InsertCompany_DL(int Company_Code, int Company_TypeID, string Company_Name, string Reg_Number, DateTime? Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country
            , string Email1, string Phone1, int Created_By, string Short_Name, string Email2, string Phone2, string Fax1, string Fax2)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Company_Code",Company_Code),
                                            new SqlParameter("@Company_TypeID",Company_TypeID),
                                            new SqlParameter("@Company_Name",Company_Name),
                                            new SqlParameter("@Reg_Number",Reg_Number),
                                            new SqlParameter("@Date_Of_Incorp",Date_Of_Incorp),
                                            new SqlParameter("@Country_Of_Incorp",Country_Of_Incorp),
                                            new SqlParameter("@Base_Curr",Base_Curr),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email1",Email1),
                                            new SqlParameter("@Phone1",Phone1),
                                            new SqlParameter("@Short_Name",Short_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Email2",Email2),
                                            new SqlParameter("@Phone2",Phone2),
                                            new SqlParameter("@Fax1",Fax1),
                                            new SqlParameter("@Fax2",Fax2),

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Company", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        
         /// <summary>
        /// Description: Added an overloaded method to save the date format settings
        /// Created By: Krishnapriya
        /// </summary>
        /// <param name="DateFormat">Selected date format will be sent as parameter</param>
        /// <returns></returns>
        public int InsertCompany_DL(int Company_Code, int Company_TypeID, string Company_Name, string Reg_Number, DateTime? Date_Of_Incorp, int? Country_Of_Incorp, int? Base_Curr, string Address, int? Country
            , string Email1, string Phone1, int Created_By, string Short_Name, string Email2, string Phone2, string Fax1, string Fax2, string DateFormat)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Company_Code",Company_Code),
                                            new SqlParameter("@Company_TypeID",Company_TypeID),
                                            new SqlParameter("@Company_Name",Company_Name),
                                            new SqlParameter("@Reg_Number",Reg_Number),
                                            new SqlParameter("@Date_Of_Incorp",Date_Of_Incorp),
                                            new SqlParameter("@Country_Of_Incorp",Country_Of_Incorp),
                                            new SqlParameter("@Base_Curr",Base_Curr),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email1",Email1),
                                            new SqlParameter("@Phone1",Phone1),
                                            new SqlParameter("@Short_Name",Short_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Email2",Email2),
                                            new SqlParameter("@Phone2",Phone2),
                                            new SqlParameter("@Fax1",Fax1),
                                            new SqlParameter("@Fax2",Fax2),
                                            new SqlParameter("@Date_Format",DateFormat),

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Company", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        
        public DataTable CheckCompany_Exist_DL(string Short_Name)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {                                            
                                            new SqlParameter("@ShortName",Short_Name)                                    

                                                      };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyByShortName", sqlprm).Tables[0];
            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            //SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyByShortName", sqlprm);
            //return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Mail_Register_SurveyCompany_DL(string EmailID, string ContactDetails, string CompanyName)
        {
            SqlParameter[] sqlprm = new SqlParameter[] {new SqlParameter("@EmailID", EmailID),
                                                        new SqlParameter("@ContactDetails", ContactDetails),
                                                        new SqlParameter("@CompanyName", CompanyName),
                
                                                        new SqlParameter("@return", SqlDbType.Int)
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURVEY_Register_Company", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int Mail_Register_SRVVesselOwner_DL(string EmailID, string ContactDetails, string CompanyName, string RegisterLink)
        {
            SqlParameter[] sqlprm = new SqlParameter[] {new SqlParameter("@EmailID", EmailID),
                                                        new SqlParameter("@ContactDetails", ContactDetails),
                                                        new SqlParameter("@CompanyName", CompanyName),
                                                        new SqlParameter("@RegisterLink", RegisterLink),
                                                        new SqlParameter("@return", SqlDbType.Int)
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURVEY_Register_VesselOwner", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        // public int Mail_Verify_SurveyCompany_DL(string EmailID, string CompanyName, string fullCompanyName, int CompanyID, int Created_By, string Email)
        public DataTable Mail_Verify_SurveyCompany_DL(string EmailID, string CompanyName, string fullCompanyName, int CompanyID, int Created_By, string Email)
        {
            SqlParameter[] sqlprm = new SqlParameter[] {new SqlParameter("@EmailID", EmailID),
                                                        new SqlParameter("@CompanyName", fullCompanyName),
                                                        new SqlParameter("@ShortCompanyName", CompanyName),
                                                        new SqlParameter("@CompanyID", CompanyID),
                                                        new SqlParameter("@Created_By", Created_By),
                                                        new SqlParameter("@Email", Email)//,
                
                                                        //new SqlParameter("@return", SqlDbType.Int)
            };

            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            //SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURVEY_Company_Verified", sqlprm);

            //return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURVEY_Company_Verified", sqlprm).Tables[0];

        }

        public int InsertCompanyReletionship_DL(int ParentCompanyID, int ChildCompanyID, int RelationshipID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentCompanyID",ParentCompanyID),
                                            new SqlParameter("@ChildCompanyID",ChildCompanyID),
                                            new SqlParameter("@RelationshipID",RelationshipID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Company_Relationship", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UpdateCompanyReletionship_DL(int ParentCompanyID, DataTable dtChild, int RelationshipID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentCompanyID",ParentCompanyID),
                                            new SqlParameter("@dtChild",dtChild),
                                            new SqlParameter("@RelationshipID",RelationshipID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Company_Relationship", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_CompanyRelationships_DL(int CompanyID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                    new SqlParameter("@CompanyID", CompanyID),
                    new SqlParameter("@UserID", UserID) 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyRelationships", sqlprm).Tables[0];
        }
        public int DeleteCompany_DL(int ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
                    new SqlParameter("@ID", ID),
                    new SqlParameter("@Created_By",Created_By),
            };


            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_Company", sqlprm);
        }
        public int Insert_Bunker_Testing_Lab_DL(string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
             SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Lab_Name", Lab_Name),
                new SqlParameter("@Address", Address),
                new SqlParameter("@EMail", EMail),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@CountryID", CountryID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Insert_Bunker_Lab", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Update_Bunker_Testing_Lab_DL(int ID, string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@Lab_Name", Lab_Name),
                new SqlParameter("@Address", Address),
                new SqlParameter("@EMail", EMail),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@CountryID", CountryID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_Update_Bunker_Lab", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_BunkerTestingLabList_DL(string SearchText, int CountryID, int UserID, string sortbycoloumn, int? sortdirection, int CurrentPageIndex, int PageSize, ref int rowcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@SearchText", SearchText),
                new SqlParameter("@CountryID", CountryID),
                new SqlParameter("@SORTBY", sortbycoloumn),
                new SqlParameter("@SORTDIRECTION", sortdirection),
                new SqlParameter("@PAGENUMBER", CurrentPageIndex),
                new SqlParameter("@PAGESIZE", PageSize),
                new SqlParameter("@ISFETCHCOUNT", rowcount)               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_BunkerTestingLabList", sqlprm).Tables[0];
        }
        public DataTable Get_BunkerTestingLabByID_DL(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_BunkerTestingLabByID", sqlprm).Tables[0];
        }
        public DataSet Get_Dep_Cat_SubCat(string user)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@user", user),};
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PURC_Get_UserAccessed_Func_Sys_SubSys",sqlprm);
        }
    }

}