using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Contract
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";

        public DAL_Crew_Contract(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Crew_Contract()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataTable Get_ContractList_DL(int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractId",ContractId),
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CREW_ContractList", sqlprm).Tables[0];
        }
        public int Insert_Contract_DL(String ContractType, DataTable dt, string ContractName, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractType",ContractType),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@ContractName",ContractName),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_Contract", sqlprm);
        }
        public int Update_Contract_DL(int ContractId, String ContractType, DataTable dt, string ContractName, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("@ContractType",ContractType),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@ContractName",ContractName),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_UPD_Contract", sqlprm);
        }
        public int DELETE_Contract_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_Contract", sqlprm);
        }
        public DataTable Get_NationalityList_DL(int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ContractId",ContractId),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CONTRACT_NationalityList", sqlprm).Tables[0];
        }
        public DataTable Get_VesselFlagList_DL(int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ContractId",ContractId),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CONTRACT_VesselFlagList", sqlprm).Tables[0];
        }
        public int INS_NationalityList_DL(int ContractId, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CONTRACT_Nationality", sqlprm);
        }
        public int INS_VesselFlagList_DL(int ContractId, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CONTRACT_VesselFlag", sqlprm);
        }
        public DataTable Get_ContractTemplateList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_ContractTemplateList").Tables[0];
        }

        public DataTable Get_CrewContractList_DL(int CountryId, int VesselFlag)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CountryId",CountryId),
                                            new SqlParameter("@VesselFlag",VesselFlag),
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_ContractList", sqlprm).Tables[0];
        }
        public DataTable Get_ContractTemplate_DL(int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{ 
											new SqlParameter("@ContractId",ContractId)
										};

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_ContractTemplate", sqlprm).Tables[0];
        }

        public DataTable Crew_Contract_Period_Search(string searchtext, int? Rank, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Rank", Rank),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crew_Contract_Period_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public DataTable Get_Crew_Contract_Period_List(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crew_Contract_Period_List", obj);

            return ds.Tables[0];
        }

        public int Insert_Crew_Contract_Period(int? Rank, int? Days, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@Days",Days),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Crew_Contract_Period", sqlprm);
        }

        public int Edit_Crew_Contract_Period(int? ID, int? Rank, int? Days, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@Days",Days),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_Crew_Contract_Period", sqlprm);
        }

        public int Delete_Crew_Contract_Period(int ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_Crew_Contract_Period", sqlprm);
        }

        public DataTable Crew_Contract_Withhold_Search(string searchtext, string Withhold_Type, int? Entry_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Withhold_Type", Withhold_Type),
                   new System.Data.SqlClient.SqlParameter("@Entry_Type", Entry_Type),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crew_Contract_Withhold_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public DataTable Get_Crew_Contract_Withhold_List(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crew_Contract_Withhold_List", obj);

            return ds.Tables[0];
        }

        public int Insert_Crew_Contract_Withhold(int Contract_Number, Decimal? Withhold_Amount, string Withhold_Type, int? Entry_Type, DateTime? Effective_Date, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Contract_Number",Contract_Number),
                                            new SqlParameter("@Withhold_Amount",Withhold_Amount),
                                            new SqlParameter("@Withhold_Type",Withhold_Type),
                                            new SqlParameter("@Entry_Type",Entry_Type),
                                            new SqlParameter("@Effective_Date",Effective_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Crew_Contract_Withhold", sqlprm);
        }

        public int Edit_Crew_Contract_Withhold(int? ID, int Contract_Number, Decimal? Withhold_Amount, string Withhold_Type, int? Entry_Type, DateTime? Effective_Date, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Contract_Number",Contract_Number),
                                            new SqlParameter("@Withhold_Amount",Withhold_Amount),
                                            new SqlParameter("@Withhold_Type",Withhold_Type),
                                            new SqlParameter("@Entry_Type",Entry_Type),
                                            new SqlParameter("@Effective_Date",Effective_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_Crew_Contract_Withhold", sqlprm);
        }

        public int Delete_Crew_Contract_Withhold(int ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_Crew_Contract_Withhold", sqlprm);
        }

        public DataTable Get_Crew_Contract_Withhold_Entry_Type()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crew_Contract_Withhold_Entry_Type").Tables[0];

        }

    }
}
