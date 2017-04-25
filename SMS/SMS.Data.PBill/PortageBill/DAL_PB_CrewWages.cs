using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SMS.Data.PortageBill
{
    public class DAL_PortageBill
    {
        private static string connection = "";
        public DAL_PortageBill(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_PortageBill()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        #region - GET -


        public static DataTable Get_Crew_BankAccList_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {   new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Crew_BankAccList", sqlprm).Tables[0];
        }
        public static DataTable Get_Crew_BankAccDetails_DL(int AccID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {   new SqlParameter("@AccID",AccID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Crew_BankAccDetails", sqlprm).Tables[0];
        }
        public static int CRW_GET_Wage_Status(int CrewID, int VoyID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@VoyID",VoyID),
                new SqlParameter("@return",SqlDbType.Int)
            };
            prm[2].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_ACC_GET_Wage_Status", prm);
            return Convert.ToInt32(prm[2].Value);

        }
        
        public static DataSet Get_CrewWageContract_DL(int CrewID, int VoyID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@VoyID",VoyID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_CrewWageContract", sqlprm);

        }
        public static DataSet Get_CrewWageContract_DL(int CrewID, int VoyID, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@VoyID",VoyID),
                                          new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_CrewWageContract", sqlprm);

        }
        public static DataSet Get_CrewWagesByVoyage_DL(int CrewID, int VoyID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@VoyID",VoyID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_CrewWagesByVoyage", sqlprm);

        }
        public static DataSet Get_CrewWagesByVoyage_ForAgreement_DL(int CrewID, int VoyID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@VoyID",VoyID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_CrewWagesByVoyage_ForAgreement", sqlprm);

        }
        public static DataSet ACC_Get_ExchRateSS()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_GetExchRateSS");
        }
        
        public static DataSet CRW_BPShip_GetSalary_Rank(int id, int rank, string scaleeffecDT)
        {
            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime dt = DateTime.Parse(scaleeffecDT, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {   new SqlParameter("@rank",rank),
                                            new SqlParameter("@crdid",id),
                                            new SqlParameter("@scaleeffecDT",dt)                                                                                   
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_BPShip_GetSalary_Rank", sqlprm);
        }

        public static DataTable Get_Salary_Types_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text,"SELECT code,name from ACC_Lib_SalaryStructure where parent_code=26").Tables[0];

        }
        public static DataTable Get_Wages_EntryType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_Wages_EntryType").Tables[0];
        }
        public static DataTable Get_Wages_EntryType_Deduction_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_Wages_EntryType_Deduction").Tables[0];
        }
        
        public static DataTable Get_WageContract_DL(int Vessel_Flag)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Vessel_Flag",Vessel_Flag)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_WageContract", sqlprm).Tables[0];
        }

        public static DataSet Get_Rank_WageContract_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Rank_WageContract");
        }
        public static DataSet Get_Rank_WageContract_DL(int RankID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@RankID",RankID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Rank_WageContract", sqlprm);
        }
        public static DataSet Get_Rank_WageContract_DL(int RankID, int WageContractID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@WageContractID",WageContractID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Rank_WageContract", sqlprm);
        }
        public static DataSet Get_Rank_WageContract_DL(int RankID, int Contract_Type, int WageContractID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@WageContractID",WageContractID),
                                            new SqlParameter("@Contract_Type",Contract_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Rank_WageContract", sqlprm);
        }
        public static DataSet Get_Rank_WageContract_DL(int RankID, int Contract_Type, int WageContractID, int NationalityConsidered, int CountryId, int RankScaleId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@WageContractID",WageContractID),
                                            new SqlParameter("@Contract_Type",Contract_Type),
                                            new SqlParameter("@NationalityConsidered",NationalityConsidered),
                                            new SqlParameter("@CountryId",CountryId),
                                            new SqlParameter("@RankScaleId",RankScaleId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Rank_WageContract", sqlprm);
        }
        public static DataSet Get_Rank_WageContract_DL(int RankID, int Contract_Type, int WageContractID, int NationalityConsidered, int CountryId, int RankScaleId,int VesselId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@WageContractID",WageContractID),
                                            new SqlParameter("@Contract_Type",Contract_Type),
                                            new SqlParameter("@NationalityConsidered",NationalityConsidered),
                                            new SqlParameter("@CountryId",CountryId),
                                            new SqlParameter("@RankScaleId",RankScaleId),
                                             new SqlParameter("@VesselId",VesselId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_Rank_WageContract_Vesselwise", sqlprm);
        }        
        public static DataSet Get_Rank_WageContract_AddNew_DL(int RankID, int Contract_Type, int NationalityConsidered, int CountryId, int RankScaleId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Contract_Type",Contract_Type),
                                            new SqlParameter("@NationalityConsidered",NationalityConsidered),
                                            new SqlParameter("@CountryId",CountryId),
                                            new SqlParameter("@RankScaleId",RankScaleId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Rank_WageContract_AddNew", sqlprm);
        }

        public static DataTable Get_SeniorityRecords_DL(int FleetID, int Vessel_ID, int Rank, string Month, string Year, string SearchText, int? CrewID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetID",FleetID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@ORDER_BY",sortbycoloumn),
                                            new SqlParameter("@SORT_DIRECTION",sortdirection),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_SeniorityRecords", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];            
        }

        public static int Update_CrewSeniorityYear_DL(int VoyageID, int SeniorityYear, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VoyageID",VoyageID),
                new SqlParameter("@SeniorityYear",SeniorityYear),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_ACC_Update_CrewSeniorityYear", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        #endregion

        #region - INSERT / UPDATE-
        public static int ACC_Insert_BankAccounts_DL(int CrewID, string Beneficiary, string Acc_No, string Bank_Name, string Bank_Address, int Default_Acc, int Verified, int Created_By, string SwiftCode, string Bank_Code, string Branch_Code, int Account_Curr, int MOBank_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@Beneficiary",Beneficiary),
                                          new SqlParameter("@Acc_No",Acc_No),
                                          new SqlParameter("@Bank_Name",Bank_Name),
                                          new SqlParameter("@Bank_Address",Bank_Address),
                                          new SqlParameter("@Default_Acc",Default_Acc),
                                          new SqlParameter("@Verified",Verified),
                                          new SqlParameter("@SwiftCode",SwiftCode),
                                          new SqlParameter("@Created_By",Created_By),
                                          new SqlParameter("@Bank_Code",Bank_Code),
                                          new SqlParameter("@Branch_Code",Branch_Code),
                                          new SqlParameter("@Account_Curr",Account_Curr),
                                          new SqlParameter("@MOBank_ID", MOBank_ID),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Insert_Crew_BankAcc", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int ACC_Update_BankAccounts_DL(int ID, int CrewID, string Beneficiary, string Acc_No, string Bank_Name, string Bank_Address, int Default_Acc, int Verified, int Modified_By, string SwiftCode, string Bank_Code, string Branch_Code, int Account_Curr, int MOBank_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@AccID",ID),
                                          new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@Beneficiary",Beneficiary),
                                          new SqlParameter("@Acc_No",Acc_No),
                                          new SqlParameter("@Bank_Name",Bank_Name),
                                          new SqlParameter("@Bank_Address",Bank_Address),
                                          new SqlParameter("@Default_Acc",Default_Acc),
                                          new SqlParameter("@Verified",Verified),
                                          new SqlParameter("@SwiftCode",SwiftCode),
                                          new SqlParameter("@Modified_By",Modified_By),
                                          new SqlParameter("@Bank_Code",Bank_Code),
                                          new SqlParameter("@Branch_Code",Branch_Code),
                                          new SqlParameter("@Account_Curr",Account_Curr),
                                          new SqlParameter("@MOBank_ID", MOBank_ID),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Update_Crew_BankAcc", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int ACC_Del_BankAccounts_DL(int ID, int CrewID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@AccID",ID),
                                          new SqlParameter("@CrewID",CrewID),                                          
                                          new SqlParameter("@Deleted_By",Deleted_By),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Delete_Crew_BankAcc", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int ACC_SetAs_DefaultAccount_DL(int CrewID, string AccID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@AccID",AccID),
                                          new SqlParameter("@Modified_By",Modified_By),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_SetAs_DefaultAccount", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataSet Ins_CrewWages(string xmlwages)
        {
            SqlParameter[] prmxml = new SqlParameter[]
            {
                new SqlParameter("xmlwages",xmlwages)           
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Ins_CrewWages_XML", prmxml);
        }
        public static DataSet Ins_CrewWages(DataTable dtWages)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("dtWages",dtWages)           
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Ins_CrewWages", sqlprm);
        }
        public static DataTable Get_Rule_Type_DL()
        {
            //DataSet ds = new DataSet();
            DataColumn dc = new DataColumn("Rule_Type");
            DataTable dt = new DataTable();
            dt.Columns.Add(dc);

            DataRow dr0 = dt.NewRow();
            dr0["Rule_Type"] = "Select";
            DataRow dr1 = dt.NewRow();
            dr1["Rule_Type"] = "days";
            DataRow dr2 = dt.NewRow();
            dr2["Rule_Type"] = "percents";
            dt.Rows.Add(dr0);
            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            //ds.Tables.Add(dt);
            return dt;
        }
        public static DataSet Ins_Rank_Wage_Contract_DL(DataTable dtWages)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("dtWages",dtWages)           
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Ins_Rank_Wage_Contract", sqlprm);
        }
        public static int AutoPopulate_CrewWages_DL(int VoyID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@VoyID",VoyID),
                                          new SqlParameter("@UserID",UserID),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_CrewWages_AutoPopulate", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Ins_Crew_SideLetter_DL( int VoyageID, decimal Amount, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VoyageID",VoyageID),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_ACC_Ins_SideLetter", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Update_Seniority_Rates_DL(DataTable dtRates, int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@dtRates",dtRates),
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_ACC_Update_Seniority_Rates", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public static DataTable Get_Seniority_Rates_DL(int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@UserID",UserID)
            };            
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Get_Seniority_Rates", sqlprm).Tables[0];
            
        }

        public static DataSet Get_Rank_WageContract_DL(int RankID, int Contract_Type, int NationalityConsidered, int CountryId, int RankScaleId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Contract_Type",Contract_Type),
                                            new SqlParameter("@NationalityConsidered",NationalityConsidered),
                                            new SqlParameter("@CountryId",CountryId),
                                            new SqlParameter("@RankScaleId",RankScaleId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_Rank_WageContract", sqlprm);
        }

        public static DataSet Ins_CrewWages(DataTable dtWages,int WageContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("dtWages",dtWages),
                new SqlParameter("WageContractId",WageContractId)         
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ACC_Ins_CrewWages", sqlprm);
        }

        public static DataTable Get_CompanySeniority_Rates_DL(int RankID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@RankID",RankID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_ACC_CompanySeniority_Rates", sqlprm).Tables[0];

        }


        public static int Save_Company_Seniority_Rates_DL(int RankID, int CompanySeniorityYear, decimal CompanySeniorityAmount, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@CompanyYear",CompanySeniorityYear),
                new SqlParameter("@SeniorityAmount",CompanySeniorityAmount),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INS_ACC_Company_Seniority_Rates", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }


        public static int Update_Company_Seniority_Rates_DL(DataTable dtRates, int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@dtRates",dtRates),
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "UPD_ACC_Company_Seniority_Rates", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public static DataTable Get_NationalityForWages_DL(int RankID, int Contract_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Contract_Type",Contract_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_NationalityForWages", sqlprm).Tables[0];
        }
        public static DataTable Get_CrewSeniorityRecords_DL(int Rank, string Status, int CompanySeniorityYear,string searchText, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",Rank),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@CompanySeniorityYear",CompanySeniorityYear),
                                            new SqlParameter("@SearchText",searchText),
                                            new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@ORDER_BY",sortbycoloumn),
                                            new SqlParameter("@SORT_DIRECTION",sortdirection),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_CRW_Crewlist_Senority", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }
        public static int Update_CrewCompanySeniority_DL(int CrewID, int SeniorityYear, int SeniorityDays, string Remarks,DateTime CompanyEffectiveDate, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@SeniorityYear",SeniorityYear),
                new SqlParameter("@SeniorityDays",SeniorityDays),
                new SqlParameter("@CompanySeniorityRemarks",Remarks),
                new SqlParameter("@EffectiveDate",CompanyEffectiveDate),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Update_ACC_CrewCompanySeniority", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int GET_Crew_CompanySeniorityAmount_DL(int CrewID, int RankId)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@RankID",RankId),
                new SqlParameter("@return",SqlDbType.Int)
            };
            prm[2].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "GET_ACC_CrewSeniorityAmount", prm);
            return Convert.ToInt32(prm[2].Value);
        }
        public static DataTable GET_Crew_RankSeniorityAmount_DL(int CrewID, int RankId, int VoyID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@RankID",RankId),
                new SqlParameter("@VoyID",VoyID),
                new SqlParameter("@return",SqlDbType.Int)
            };
            prm[3].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_ACC_CrewRankSeniorityAmount", prm);
            return ds.Tables[0];
        }
        public static int Update_CrewRankSeniorityYear_DL(int CrewID, int RankId, int SeniorityYear, int SeniorityDays, int VoyageId, string Remarks, DateTime RankEffectiveDate, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@SeniorityYear",SeniorityYear),
                new SqlParameter("@SeniorityDays",SeniorityDays),
                new SqlParameter("@VoyageId",VoyageId),
                new SqlParameter("@RankSeniorityRemarks",Remarks),
                new SqlParameter("@EffectiveDate",RankEffectiveDate),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Update_ACC_CrewRankSeniorityYear", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Update_CrewRankSeniority_DL(int CrewID,int RankId, int SeniorityYear, int SeniorityDays,int VoyageId, string Remarks,DateTime RankEffectiveDate,int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@SeniorityYear",SeniorityYear),
                new SqlParameter("@SeniorityDays",SeniorityDays),
                new SqlParameter("@VoyageId",VoyageId),
                new SqlParameter("@RankSeniorityRemarks",Remarks),
                new SqlParameter("@EffectiveDate",RankEffectiveDate),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Update_ACC_CrewRankSeniority", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static DataTable GetRejoiningBonus(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CrewID",CrewID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_ACC_Get_ReJoining_Amount", sqlprm).Tables[0];
        }
        public static DataTable Get_RJBDetails(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ID",ID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_RJBDetails", sqlprm).Tables[0];
        }
        public static int CopyRankWageFromExistingCountry_DL(int ContractType, int RankId, int CountryFromId,DateTime EffectiveDate ,int Currency,int CompanyId,DataTable dtCountryToList, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ContractType",ContractType),
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@CountryFromId",CountryFromId),
                new SqlParameter("@EffectiveDate",EffectiveDate),
                new SqlParameter("@Currency",Currency),
                new SqlParameter("@CompanyId",CompanyId),
                new SqlParameter("@dtCountryToList",dtCountryToList),
                new SqlParameter("@CreatedBy",CreatedBy),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INS_ACC_RankWagesFromExistingCountry", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public static DataTable Get_NationalityGroupForWege_Contract_DL(int RankID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@RankId",RankID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_NationalityGroupForWage_contract", sqlprm).Tables[0];
        }




        public static DataTable Check_NationalityGroup_DL(int RankID, int NationalityGroupId, String NationalityGroupName, DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankId",RankID),
                                            new SqlParameter("@NationalityGroupId",NationalityGroupId),
                                            new SqlParameter("@NationalityGroupName",NationalityGroupName),
                                            new SqlParameter("@dtCountry",dt)
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_WageContract_CHK_NationalityGroup", sqlprm).Tables[0];
        }




        public static int Insert_NationalityGroup_DL(int RankId, int NationalityGroupId, String NationalityGroupName, DataTable dt, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankId",RankId),
                                            new SqlParameter("@NationalityGroupId",NationalityGroupId),
                                            new SqlParameter("@NationalityGroupName",NationalityGroupName),
                                            new SqlParameter("@dtCountry",dt),
                                            new SqlParameter("@UserId",UserId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_WageContarct_INS_NationalityGroup", sqlprm);
        }




        public static DataSet Get_RankWise_GroupDetails_DL(int NationalityGroupId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@NationalityGroupId",NationalityGroupId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Crew_Get_GroupDetails", sqlprm);
        }

        public static int DeleteNationalityGroup_DL(int NationalityGroupId, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@NationalityGroupId",NationalityGroupId),
                                            new SqlParameter("@UserId",UserId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DEL_NationalityForWage_Contract", sqlprm);
        }


        public static DataTable Get_NationalityGroupbyGroupID_DL(int NationalityGroupId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@NationalityGroupId",NationalityGroupId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_NationalityByGroupID_Wage_contract", sqlprm).Tables[0];
        }

        public  static DataTable Get_VesselType_DL()
        {          
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_VesselType"); 
            return ds.Tables[0];
        }



        public static DataTable Get_VesselList_DL(int vesselTypeId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselTypeId",vesselTypeId)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_VesselDetails", sqlprm).Tables[0];
        }
        public static DataTable Get_VesselSpecificWage_DL(int VesselTypeID,int WageContractId, int SalaryCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@VesselTypeId",VesselTypeID),
                new SqlParameter("@WageContractId",WageContractId),
                new SqlParameter("@SalaryCode",SalaryCode)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_VesselSpecificWage", sqlprm).Tables[0];
        }
        public static int Ins_CrewChangeWageLog_DL(int RankId, int RankScaleId, int CrewId, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@RankScaleId",RankScaleId),
                new SqlParameter("@CrewId",CrewId),
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CrewChangeWageLog", sqlprm);
        }
        #endregion
    }
}
