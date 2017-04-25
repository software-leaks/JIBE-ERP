using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Admin
    {
        private string connection = "";

        public DAL_Crew_Admin(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Crew_Admin()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }



        public DataTable Get_RankList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankList").Tables[0];
        }

        public DataTable Get_RankList_Search(string searchtext, int? rankcategory, int? DeckOrEngine, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Rank_Category", rankcategory),
                   new System.Data.SqlClient.SqlParameter("@DeckOrEngine",DeckOrEngine),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankList_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public int EditRank_DL(int ID, string Rank_Name, string Rank_Short_Name, int Rank_Category, int Rank_sort_order, int? DeckOrEngine, int UserId, int? IsDeckOfficer)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Rank_Name",Rank_Name),
                                            new SqlParameter("@Rank_Short_Name",Rank_Short_Name),
                                            new SqlParameter("@Rank_Category",Rank_Category),
                                            new SqlParameter("@Rank_sort_order",Rank_sort_order),
                                            new SqlParameter("@DeckOrEngine",DeckOrEngine),
                                            new SqlParameter("@UserId",UserId),
                                            new SqlParameter("@IsDeckOfficer",IsDeckOfficer)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Rank", sqlprm);

        }

        public int InsertRank_DL(string Rank_Name, string Rank_Short_Name, int Rank_category, int? DeckOrEngine, int UserId, int? IsDeckOfficer)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Rank_Name",Rank_Name),
                                            new SqlParameter("@Rank_Short_Name",Rank_Short_Name),
                                            new SqlParameter("@Rank_category",Rank_category),
                                            new SqlParameter("@DeckOrEngine",DeckOrEngine),
                                            new SqlParameter("@UserId",UserId),
                                            new SqlParameter("@IsDeckOfficer",IsDeckOfficer)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Rank", sqlprm);
        }

        public int Swap_Rank_Sort_Order_DL(int ID, int MoveUpDown, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@MoveUpDown",MoveUpDown),
                                            new SqlParameter("@Modified_By",Modified_By)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Swap_Rank_Sort_Order", sqlprm);
        }

        public int DeleteRank_DL(int ID, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID",ID),
                new SqlParameter("@UserId",UserId)
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_Rank", sqlprm);
        }

        public DataTable Get_RankCategories_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankCategoryList").Tables[0];
        }

        public DataTable Get_RankCategoryByRankID_DL(int RankID)
        {
            SqlParameter sqlprm = new SqlParameter("@RankID", RankID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankCategoryByRankID", sqlprm).Tables[0];
        }

        public DataTable ExecuteQuery(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }

        public DataTable SignoffReason_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_SingoffReason_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_SignoffReason_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRE_Get_Signoff_Reason_List", obj);

            return ds.Tables[0];
        }

        public int InsertSignoffReason_DL(string Reason, int CreatedBy, int JoiningTypeId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Reason",Reason),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@JoiningTypeId",JoiningTypeId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Signoff_Reason", sqlprm);
        }

        public int EditSignoffReason_DL(int ID, string Reason, int CreatedBy, int JoiningTypeId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Reason",Reason),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@JoiningTypeId",JoiningTypeId)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Signoff_Reason", sqlprm);
        }

        public int DeleteSignoffReason_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Signoff_Reason", sqlprm);
        }

        public DataTable RankCategory_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Rank_Category_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_RankCategory_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Rank_Category_List", obj);

            return ds.Tables[0];
        }

        public int InsertRankCategory_DL(string Category, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Rank_Category", sqlprm);
        }

        public int EditRankCategory_DL(int ID, string Category, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Rank_Category", sqlprm);
        }

        public int DeleteRankCategory_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Rank_Category", sqlprm);
        }

        public DataTable JoiningType_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Joining_Type_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_JoiningType_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRE_Get_Joining_Type_List", obj);

            return ds.Tables[0];
        }

        public int InsertJoiningType_DL(string Joining_Type, string JCode, bool SeniorityConsidered, bool VessselPBill_Considered, bool ServiceConsidered, bool PBillConsidered, DataTable SalComponent, int CreatedBy, bool OperatorConsidered, bool WatchKeepingConsidered)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@JCode",JCode),
                                            new SqlParameter("@SeniorityConsidered",SeniorityConsidered),
                                            new SqlParameter("@VessselPBill_Considered",VessselPBill_Considered),
                                            new SqlParameter("@ServiceConsidered",ServiceConsidered),
                                            new SqlParameter("@PBillConsidered",PBillConsidered),
                                            new SqlParameter("@SalComponent",SalComponent),
                                            new SqlParameter("@CreatedBy",CreatedBy), 
                                             new SqlParameter("@OperatorConsidered",OperatorConsidered),
                                            new SqlParameter("@WatchKeepingConsidered",WatchKeepingConsidered),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Joining_Type", sqlprm);
        }

        public int EditJoiningType_DL(int ID, string Joining_Type, string JCode, bool SeniorityConsidered, bool VessselPBill_Considered, bool ServiceConsidered, bool PBillConsidered, DataTable SalComponent, int CreatedBy, bool OperatorConsidered, bool WatchKeepingConsidered)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@JCode",JCode),
                                            new SqlParameter("@SeniorityConsidered",SeniorityConsidered),
                                            new SqlParameter("@VessselPBill_Considered",VessselPBill_Considered),
                                            new SqlParameter("@ServiceConsidered",ServiceConsidered),
                                            new SqlParameter("@PBillConsidered",PBillConsidered),
                                            new SqlParameter("@SalComponent",SalComponent),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                             new SqlParameter("@OperatorConsidered",OperatorConsidered),
                                            new SqlParameter("@WatchKeepingConsidered",WatchKeepingConsidered),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Joining_Type", sqlprm);
        }

        public int DeleteJoiningType_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Joining_Type", sqlprm);
        }

        public DataTable StaffRemarkCategory_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Staff_Remark_Category_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_StaffRemarkCategory_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRE_Get_Staff_Remark_Category_List", obj);

            return ds.Tables[0];
        }

        public int InsertStaffRemarkCategory_DL(string Category, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Staff_Remark_Category", sqlprm);
        }

        public int EditStaffRemarkCategory_DL(int ID, string Category, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Staff_Remark_Category", sqlprm);
        }

        public int DeleteStaffRemarkCategory_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Staff_Remark_Category", sqlprm);
        }

        public DataTable Trade_Zones_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Trade_zones_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }



        public DataTable Get_Trade_Zones_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Trade_Zones_List", obj);

            return ds.Tables[0];
        }

        public int Insert_Trade_Zones_DL(string ZoneName, int ZoneListID, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ZoneName",ZoneName),
                                            new SqlParameter("@ZoneListID",ZoneListID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Trade_Zones", sqlprm);
        }


        public int Edit_Trade_Zones_DL(int ID, string ZoneName, int ZoneListID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@ZoneName",ZoneName),
                                            new SqlParameter("@ZoneListID",ZoneListID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Trade_Zones", sqlprm);
        }

        public int Delete_Trade_Zones_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Trade_Zones", sqlprm);
        }


        public DataTable Get_Rank_Onboard_Limit_Search(int Vessel_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", Vessel_ID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_GET_RANK_ONBOARD_LIMIT_SEARCH", obj).Tables[0];

        }

        public int Update_Rank_OnBoard_Limit(int Vessel_ID, int Rank_ID, int Min, int Max, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@RankID",Rank_ID),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Created_By",Created_By),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Rank_OnBoard_Limit", sqlprm);
        }




        public DataTable Agent_Bank_Account_Search(string searchtext, int? Account_Curr, int? MO_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Account_Curr", Account_Curr),
                   new System.Data.SqlClient.SqlParameter("@MO_ID", MO_ID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Agent_Bank_Account_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_Agent_Bank_Account_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Agent_Bank_Account_List", obj);

            return ds.Tables[0];
        }

        public int Insert_Agent_Bank_Account_DL(string Beneficiary, string Bank_Name, string Bank_Address, string Acc_NO, string SwiftCode
            , string BANK_CODE, string BRANCH_CODE, int? ACCOUNT_CURR, int MO_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Beneficiary",Beneficiary),
                                            new SqlParameter("@Bank_Name",Bank_Name),
                                            new SqlParameter("@Bank_Address",Bank_Address),
                                            new SqlParameter("@Acc_NO",Acc_NO),
                                            new SqlParameter("@SwiftCode",SwiftCode),
                                            new SqlParameter("@BANK_CODE",BANK_CODE),
                                            new SqlParameter("@BRANCH_CODE",BRANCH_CODE),
                                            new SqlParameter("@ACCOUNT_CURR",ACCOUNT_CURR),
                                             new SqlParameter("@MO_ID",MO_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Agent_Bank_Account", sqlprm);
        }

        public int Edit_Agent_Bank_Account_DL(int? ID, string Beneficiary, string Bank_Name, string Bank_Address, string Acc_NO, string SwiftCode
            , string BANK_CODE, string BRANCH_CODE, int? ACCOUNT_CURR, int MO_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Beneficiary",Beneficiary),
                                            new SqlParameter("@Bank_Name",Bank_Name),
                                            new SqlParameter("@Bank_Address",Bank_Address),
                                            new SqlParameter("@Acc_NO",Acc_NO),
                                            new SqlParameter("@SwiftCode",SwiftCode),
                                            new SqlParameter("@BANK_CODE",BANK_CODE),
                                            new SqlParameter("@BRANCH_CODE",BRANCH_CODE),
                                            new SqlParameter("@ACCOUNT_CURR",ACCOUNT_CURR),
                                             new SqlParameter("@MO_ID",MO_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_Agent_Bank_Account", sqlprm);
        }

        public int Delete_Agent_Bank_Account_DL(int ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_Agent_Bank_Account", sqlprm);
        }



        public DataTable Crew_Rules_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CRW_RULES_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }


        public DataTable Get_Crew_Rules_List_DL(int? RULE_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RULE_ID",@RULE_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CRW_RULES_LIST", obj);

            return ds.Tables[0];
        }


        public int Insert_Crew_Rules_DL(string Description, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INSERT_CRW_RULES", sqlprm);
        }


        public int Update_Crew_Rules_DL(int? RULE_ID, string Description, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@RULE_ID",RULE_ID),
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_CRW_RULES", sqlprm);
        }




        public int Delete_Crew_Rules_DL(int? RULE_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RULE_ID",RULE_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DELETE_CRW_RULES", sqlprm);
        }




        public DataTable Crew_Get_Vessel_Specific_Rules_Search(int? RULE_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RULE_ID", RULE_ID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_VESSEL_SPECIFIC_RULE_SEARCH", obj).Tables[0];

        }


        public DataTable Crew_Get_Rank_Specific_Rules_Search(int? RULE_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RULE_ID", RULE_ID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_RANK_SPECIFIC_RULE_SEARCH", obj).Tables[0];

        }


        public int Crew_Rank_Specific_Rules_Assignment_DL(int Rank_ID, int? Rule_ID, int IsApply, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Rank_ID",Rank_ID),
                                            new SqlParameter("@Rule_ID",Rule_ID),
                                            new SqlParameter("@IsApply",IsApply),
                                            new SqlParameter("@Created_By",Created_By),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_RANK_SPECIFIC_RULE_ASSIGNMENT", sqlprm);
        }

        public int Crew_Vessel_Specific_Rules_Assignment_DL(int Vessel_ID, int? Rule_ID, int IsApply, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Rule_ID",Rule_ID),
                                            new SqlParameter("@IsApply",IsApply),
                                            new SqlParameter("@Created_By",Created_By),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_VESSEL_SPECIFIC_RULE_ASSIGNMENT", sqlprm);
        }
        public DataTable Crew_HandOverQuestion_Search(string searchtext, int? Rank, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_HANDOVER_QUESTION_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public int Ins_Update_HandOverQuestion(int? ID, int rank, string Description, int? IsChecklist, string datatype, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
               { 
                      new SqlParameter("@ID",@ID),
                      new SqlParameter("@CREW_RANk",rank),
                      new SqlParameter("@HANDOVER_QUESTION",Description),
                      new SqlParameter("@ISCheckList",IsChecklist),
                      new SqlParameter("@DataType",datatype),
                      new SqlParameter("@UserID",Created_By)
               };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_UPD_HANDOVER_QUESTION", sqlprm);
        }

        public DataTable Get_Crew_HandOverQuestion(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_HANDOVER_QUESTION", obj);

            return ds.Tables[0];
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
        public int Insert_Crew_Assigner_DL(string Value, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VALUE",Value),
                                           
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_Assigner", sqlprm);
        }

        public int Edit_Crew_Assigner_DL(int ID, string Value, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@VALUE",Value),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Crew_Assigner", sqlprm);
        }

        public DataTable Get_Crew_Assigner_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Crew_Assigner_List", obj);

            return ds.Tables[0];
        }

        public int Delete_Crew_Assigner_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Crew_Assigner", sqlprm);
        }

        public DataTable Check_Crew_Assigner_DL(string AssignValue)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@VALUE", AssignValue) ,   
                  //new System.Data.SqlClient.SqlParameter("@Size_KB", UploadSize)   
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_Check_Crew_Assigner", obj);
            return ds.Tables[0];
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_check_FAQ_List", obj);

        }
        public DataTable Crew_Assigner_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Crew_Assigner_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public int SaveInterviewSettings_DL(bool Interview_Mandatory, bool Check_Rejected_Interview)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Interview_Mandatory",Interview_Mandatory),
                                            new SqlParameter("@Check_Rejected_Interview",Check_Rejected_Interview),
                                            
                                         };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_Save_Interview_Settings", sqlprm);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetInterviewSettings_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_Get_Interview_Settings").Tables[0];
        }
        public DataTable EvaluationFeedbackCategories_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_EvaluationFeedbackCategories_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public int InsertEvaluationFeedbackCategories_DL(string Category, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_EvaluationFeedbackCategories", sqlprm);
        }

        public int EditEvaluationFeedbackCategories_DL(int ID, string Category, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_EvaluationFeedbackCategories", sqlprm);
        }

        public int DeleteEvaluationFeedbackCategories_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_EvaluationFeedbackCategories", sqlprm);
        }
        public DataTable Get_EvaluationFeedbackCategories_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_EvaluationFeedbackCategories_List", obj);

            return ds.Tables[0];
        }
        public DataTable Get_InterviewSheets_DL(int RankId, string InterviewType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@InterviewType",InterviewType),
                                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_INTERVIEW_SHEET", sqlprm);
            return ds.Tables[0];
        }
        public int SaveWagesSettings_DL(bool Nationality, bool RankScale, bool VesselFlag)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@Nationality",Nationality), 
                    new SqlParameter("@RankScale",RankScale), 
                    new SqlParameter("@VesselFlag",VesselFlag),   
                };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Wages_Settings", sqlprm);
            }
            catch
            {
                throw;
            }
        }


        public int SaveMandatorySettings_DL(bool NOK, bool CrewPhotograph, bool BankAccDetails, bool Seniority, bool LeaveWithhold, int RankId )
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@NOK",NOK), 
                    new SqlParameter("@CrewPhotograph",CrewPhotograph),
                    new SqlParameter("@BankAccDetails",BankAccDetails), 
                    new SqlParameter("@Seniority",Seniority), 
                    new SqlParameter("@RankId",RankId), 
                    new SqlParameter("@LeaveWithhold",LeaveWithhold),
                    new SqlParameter("return",SqlDbType.Int) 
                };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Mandatory_Settings", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

                //return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Mandatory_Settings", sqlprm);
            }

            catch
            {
                throw;
            }

        }

        public int SaveMandatorySettings_DL(bool NOK, bool CrewPhotograph, bool BankAccDetails, bool Seniority, bool LeaveWithhold, int RankId, bool EvaluationDigitalSignature)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@NOK",NOK), 
                    new SqlParameter("@CrewPhotograph",CrewPhotograph),
                    new SqlParameter("@BankAccDetails",BankAccDetails), 
                    new SqlParameter("@Seniority",Seniority), 
                    new SqlParameter("@RankId",RankId), 
                    new SqlParameter("@LeaveWithhold",LeaveWithhold),
                    new SqlParameter("@EvaluationDigitalSignature",EvaluationDigitalSignature),
                    new SqlParameter("return",SqlDbType.Int) 
                };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Mandatory_Settings", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

                //return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Mandatory_Settings", sqlprm);
            }

            catch
            {
                throw;
            }

        }

        public DataTable GetWagesSettings_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_Wages_Settings").Tables[0];
        }


        public DataTable GetMandatorySettings_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_Mandatory_Settings").Tables[0];
        }



        public int SaveDocumentSettings_DL(bool VesselFlagConsidered, bool VesselConsidered)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@VesselFlagConsidered",VesselFlagConsidered), 
                    new SqlParameter("@VesselConsidered",VesselConsidered),                       
                };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Document_Settings", sqlprm);
            }
            catch
            {
                throw;
            }
        }
        public int SaveDocumentSettings_DL(bool VesselFlagConsidered, bool VesselConsidered, bool STCW_Deck_Considered, bool STCW_Engine_Considered)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@VesselFlagConsidered",VesselFlagConsidered), 
                    new SqlParameter("@VesselConsidered",VesselConsidered),          
                    new SqlParameter("@STCW_Deck_Considered",STCW_Deck_Considered),           
                    new SqlParameter("@STCW_Engine_Considered",STCW_Engine_Considered),                        
                };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Save_Document_Settings", sqlprm);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetDocumentSettings_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_Document_Settings").Tables[0];
        }

        public DataTable Get_RankScaleList_DL(int RankId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@RankId",RankId),                                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_RankScale", sqlprm);
            return ds.Tables[0];
        }

        public int Ins_Update_RankScale_DL(int ID, int RankId, string RankScaleName, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@RankId",RankId),
                                            new SqlParameter("@RankScaleName",RankScaleName),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_RankScale", sqlprm);
        }
        public int DELETE_RankScale_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_RankScale", sqlprm);
        }

        public DataTable Get_Manning_Report()
        {
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CREW_GET_MANNING_OFFICE_REPORT", null);
            return ds.Tables[0];
        }
        public DataTable Get_MOBankAccount(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@CrewID",CrewID),                                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_MOBankAccountList", sqlprm);
            return ds.Tables[0];
        }
        public DataTable Get_MOBankAccount_Details(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID",ID),                                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_MOBankAccountDetails", sqlprm);
            return ds.Tables[0];
        }
        public DataTable Get_MOBankAccountList_ByManningID(int MO_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@MO_ID",MO_ID),                                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_MOBankAccountList_ByManningID", sqlprm);
            return ds.Tables[0];
        }

        public DataTable Get_RankSeniorityList_DL(int RankId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@RankId",RankId),                                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CRW_SENIORITY_CONFIG", sqlprm);
            return ds.Tables[0];
        }
        public int Ins_Update_RankSeniority_DL(int RankId, bool CompanySeniority, bool RankSeniority, int UserId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankId",RankId),
                                            new SqlParameter("@CompanySeniority",CompanySeniority),
                                            new SqlParameter("@RankSeniority",RankSeniority),
                                            new SqlParameter("@UserId",UserId),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_RankSeniority", sqlprm);
        }
        public DataTable Get_RankScaleListForWages_DL(int RankId, int CountryId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@CountryId",CountryId),                            
           };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_RankScaleHavingWages", sqlprm);
            return ds.Tables[0];
        }
        public DataTable ServiceType_Search_DL(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Search_Service_Type", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public int InsertServiceType_DL(string Service_Type, string SCode, bool SeniorityConsidered, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Service_Type",Service_Type),
                                            new SqlParameter("@SCode",SCode),
                                            new SqlParameter("@SeniorityConsidered",SeniorityConsidered),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Service_Type", sqlprm);
        }
        public int EditServiceType_DL(int ID, string Service_Type, string SCode, bool SeniorityConsidered, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Service_Type",Service_Type),
                                            new SqlParameter("@SCode",SCode),
                                            new SqlParameter("@SeniorityConsidered",SeniorityConsidered),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_Service_Type", sqlprm);
        }
        public DataTable Get_ServiceType_List_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Service_Type_List", obj);

            return ds.Tables[0];
        }
        public int DeleteServiceType_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_Service_Type", sqlprm);
        }
        public int SaveSeniorityResetSettings_DL(bool AutomaticResetConsidered, int ResetYears, int UserId)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@AutomaticResetConsidered",AutomaticResetConsidered), 
                    new SqlParameter("@ResetYears",ResetYears),  
                    new SqlParameter("@UserId",UserId),  
                };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Save_Seniority_Reset_Settings", sqlprm);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetSeniorityResetSettings_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Seniority_Reset_Settings").Tables[0];
        }
        public DataTable Get_JoiningType_PBillComponent_DL(int? JoiningType_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@JoiningType_ID",JoiningType_ID), 
                  
                };
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_Get_Joining_PBill_Component", sqlprm).Tables[0];

            }
            catch
            {
                throw;
            }
        }
        public int GetOfficeVessel_DL()
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_GET_OfficeVessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_CrewMainStatus_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CREW_MAIN_STATUS").Tables[0];
        }
        public DataTable Get_CrewCalculatedStatus_DL(int MainStatusId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@MainStatusId",MainStatusId), 
                  
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CREW_CALCULATED_STATUS", sqlprm).Tables[0];
        }




        public DataTable Get_CrewMainStatus_Search_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_STATUS_Search", sqlprm).Tables[0];
        }






        public DataTable Get_Calc_Status_Search_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Calc_STATUS_Search", sqlprm).Tables[0];
        }





        public int UPDATE_Status_DL(int ID, string Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Name",Name),
                                            //new SqlParameter ("@Value",Value),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }




        public int UPDATE_Calc_Status_DL(int ID, string Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Name",Name),
                                             //new SqlParameter ("@Value",Value),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Update_Calc_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Calc_Status_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "-", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int DELETE_Status_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "-", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        public int INSERT_Status_DL(string Name, string Value, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Name",Name),
                                            new SqlParameter("@Value",Value),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int INSERT_Calc_Status_DL(string Name, string Value, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Name",Name),
                                            new SqlParameter("@Value",Value),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Calculated_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataSet SearchStatusStructure(string searchtext)
        {
            DataTable dt = new DataTable();

            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),                
                    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_STATUS_structure", obj);

        }

        public DataTable get_MainStatus_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Main_STATUS").Tables[0];

        }

        public DataTable get_Calc_StatuList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Calc_StatusList").Tables[0];

        }
        public DataTable get_JoiningTypeList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_JoiningType").Tables[0];

        }

        public DataTable StatusStructure_Edit_DL(int statusId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@StatusId", statusId),                
                    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Edit_STATUS_Mapping", obj).Tables[0];

        }

        public string Insert_Update_StatusStructure_DL(int StatusId, DataTable Calc_DT, DataTable JT_DT, int CurrentUser, int Add)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@StatusId",StatusId),
                                            //new SqlParameter("@Calculated_DT",Calc_DT),                                      
                                            new SqlParameter("@JoyningType_DT",JT_DT),
                                            new SqlParameter("@CurrentUser",CurrentUser), 
                                            new SqlParameter("@Adding",Add), 
                        new SqlParameter("return",SqlDbType.VarChar)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Status__Mapping", sqlprm);
            return Convert.ToString(sqlprm[sqlprm.Length - 1].Value);

            //  return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_Insert_StatusStructure", sqlprm)

        }

        public int Check_Document_Mandatory_DL(int RankId, int CountryId, string Document_Type)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RankID", RankId),             
                   new System.Data.SqlClient.SqlParameter("@CountryId", CountryId),              
                   new System.Data.SqlClient.SqlParameter("@Document_Type", Document_Type),              
                   new System.Data.SqlClient.SqlParameter("return",SqlDbType.Int)
             };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CHK_DOCUMENT_MANDATORY", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Check_Contract_Mandatory_DL(int RankId, int CountryId, int VesselId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RankID", RankId),   
                   new System.Data.SqlClient.SqlParameter("@CountryId", CountryId),              
                   new System.Data.SqlClient.SqlParameter("@VesselId", VesselId),              
                   new System.Data.SqlClient.SqlParameter("return",SqlDbType.Int)
             };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CHK_CONTRACT_MANDATORY", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable CRUD_OilMajors(string OilMajorName, string Action, int UserId, int OilMajorId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string Remarks, ref int isfetchcount, ref int result)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", OilMajorId),
                   new System.Data.SqlClient.SqlParameter("@Oil_Major_Name", OilMajorName),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                      new System.Data.SqlClient.SqlParameter("@Remarks",Remarks),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajors", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
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
        public DataTable CRUD_OilMajors(string OilMajorName, string DisplayName, string Action, int UserId, int OilMajorId, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string Remarks, int ActiveStatus, string Path, ref int isfetchcount, ref int result)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", OilMajorId),
                   new System.Data.SqlClient.SqlParameter("@Oil_Major_Name", OilMajorName),
                   new System.Data.SqlClient.SqlParameter("@Display_Name", DisplayName),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                      new System.Data.SqlClient.SqlParameter("@Remarks",Remarks),
                      new System.Data.SqlClient.SqlParameter("@Oil_Major_Logo",Path),
                      new System.Data.SqlClient.SqlParameter("@ActivStatus",ActiveStatus),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajors", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", OilMajorRuleId),
                   new System.Data.SqlClient.SqlParameter("@Rule", Rule),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajorsRules", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds;
            }
            catch (Exception ex) { ds = null; }

            return ds;
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", RuleGroupId),
                   new System.Data.SqlClient.SqlParameter("@Group_Name", RuleGroup),
                   new System.Data.SqlClient.SqlParameter("@Type", Type),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajorsRulesGroups", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
        }


        /// <summary>
        /// To Bind Rule Mapping Oil Majors, Oil Major Groups, Ranks, Oil Major Rule and Value
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="OilMajorId"></param>
        /// <returns></returns>
        public DataSet Bind_Rule_Mapping_Popup(string Action, int OilMajorId, int RuleMappingID)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@OilMajorId", OilMajorId),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@RuleMappingID",RuleMappingID)
               };
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajorsRulesMapping", obj);
                return ds;
            }
            catch (Exception ex) { ds = null; }

            return ds;
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
            int ReturnValue = 0;
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Action", "I"),   
                   new System.Data.SqlClient.SqlParameter("@OilMajor_IDs", OilMajor_IDs),              
                   new System.Data.SqlClient.SqlParameter("@Rank_IDs", Rank_IDs),
                   new System.Data.SqlClient.SqlParameter("@RuleGroup_ID", RuleGroup_ID),
                   new System.Data.SqlClient.SqlParameter("@OilMajorRule_ID", OilMajorRule_ID),
                   new System.Data.SqlClient.SqlParameter("@RuleMappingID", RuleMappingID),
                   new System.Data.SqlClient.SqlParameter("@Value", value),
                   new System.Data.SqlClient.SqlParameter("@UserId", UserId),
                   new System.Data.SqlClient.SqlParameter("@OilMajorValues", OilMajorsValues),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
             };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajorsRulesMapping", sqlprm);
                ReturnValue = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            }
            catch (Exception ex) { }
            return ReturnValue;
        }

        /// <summary>
        /// Delete Oil Major Rule Mapping Record and child table records
        /// </summary>
        /// <param name="RuleMappingID"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int DeleteOilMajorRuleMapping(int RuleMappingID, int UserId, int oilMajorID)
        {
            int ReturnValue = 0;
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Action", "D"),   
                   new System.Data.SqlClient.SqlParameter("@RuleMappingID", RuleMappingID),
                   new System.Data.SqlClient.SqlParameter("@OilMajorId", oilMajorID),
                   new System.Data.SqlClient.SqlParameter("@UserId", UserId),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
             };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_LIB_CM_CRUD_OilMajorsRulesMapping", sqlprm);
                ReturnValue = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            }
            catch (Exception ex) { }
            return ReturnValue;
        }

        public DataSet Get_Crew_Matrix_Configuration()
        {
            DataSet ds = new DataSet();
            try
            {
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_Matrix_Config");

            }
            catch
            {
                return null;
            }
        }


        public int UpdateCrewMatrixConfiguration(string Event, string KEYCONFIGURATION, string PARAMETERS, int DEFAULTVALUE, int CreatedBy)
        {


            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@Event", Event),   
                   new System.Data.SqlClient.SqlParameter("@KEYCONFIGURATION", KEYCONFIGURATION),   
                   new System.Data.SqlClient.SqlParameter("@PARAMETERS", PARAMETERS),              
                   new System.Data.SqlClient.SqlParameter("@DEFAULTVALUE", DEFAULTVALUE),
                    new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy)

            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_UPD_Matrix_Config", sqlprm);



        }

        public int InsertCrewMatrixConfiguration(string KEYCONFIGURATION, string PARAMETERS, string OPARAMETERS, int CreatedBy)
        {


            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@KEYCONFIGURATION", KEYCONFIGURATION),   
                   new System.Data.SqlClient.SqlParameter("@PARAMETERS", PARAMETERS),      
        new System.Data.SqlClient.SqlParameter("@OPARAMETERS", OPARAMETERS),      
                    new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy)

            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_Matrix_Config", sqlprm);



        }

        public DataSet getEnglishProficiency(int? CrewId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CREWID", CrewId) 
                  
    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_English_Proficiency", sqlprm);
        }

        public int InsertEnglishProficiency(int CrewId, string ENGLISHPROFICIENCY, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CREWID", CrewId),   
                   new System.Data.SqlClient.SqlParameter("@ENGLISHPROFICIENCY", ENGLISHPROFICIENCY) ,
                   new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy)
    
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_ENGLISH_PROFICIENCY", sqlprm);
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
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@OilMajorId", oilMajorID),   
                   new System.Data.SqlClient.SqlParameter("@EventID", EventId) ,
                   new System.Data.SqlClient.SqlParameter("@Date",Date),
                   new System.Data.SqlClient.SqlParameter("@VesselId",VesselID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DTL_CM_GetCrewMatrix", sqlprm);
        }
        public int InsertCrewMatrixGroup(int GroupID, int RankId, int CreatedBy, int Active_Status)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@GroupID", GroupID),   
                   new System.Data.SqlClient.SqlParameter("@RankID", RankId) ,
                   new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
                   new SqlParameter("@Active_Status",Active_Status)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CREWMatrix_Group", sqlprm);
        }
        public DataSet GetCrewMatrixGroup(int GroupID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@GroupID", GroupID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CREWMatrix_Group", sqlprm);
        }

        public DataSet GetOilMajor()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_Oil_Majors");
        }

        public int InsertAdditionalRule(int Parameters, string rule, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Parameters", Parameters),   
                   new System.Data.SqlClient.SqlParameter("@Rule", rule) ,
                   new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CM_Additional_Rule", sqlprm);
        }

        public DataSet getAdditionalRule()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CM_Additional_Rule");

        }


        public int UpdateAdditionalRule(int Parameters, string rule, int CreatedBy, int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Parameters", Parameters),   
                   new System.Data.SqlClient.SqlParameter("@Rule", rule) ,
                   new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
                    new System.Data.SqlClient.SqlParameter("@ID",ID)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_UPD_CM_Additional_Rule", sqlprm);
        }

        public int DeleteAdditionalRule(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Del_CM_Additional_Rule", sqlprm);
        }

        public int InsertCRWAddtional_Rule_Mapping(int RuleId, int OilMajorId, string key, string value, int CreatedBy, int IsActive)
        {
            //CRW_INS_CM_Addtional_Rule_Mapping
            SqlParameter[] sqlprm = new SqlParameter[] 


            { 
                   new System.Data.SqlClient.SqlParameter("@RuleId", RuleId),   
                   new System.Data.SqlClient.SqlParameter("@OilMajorId", OilMajorId) ,
                   new System.Data.SqlClient.SqlParameter("@Key",key),
                    new System.Data.SqlClient.SqlParameter("@value",value),
                   new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
                   new System.Data.SqlClient.SqlParameter("@IsActive",IsActive)
                   
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_INS_CM_Addtional_Rule_Mapping", sqlprm);

        }


        public int InsertCRWAddtional_Rule_Mapping(int EditParentId, int RuleId, int OilMajorId, string key, string value, int CreatedBy, int IsActive, ref int ParentID)
        {
            try
            {
                //CRW_INS_CM_Addtional_Rule_Mapping
                SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RuleId", RuleId),   
                   new System.Data.SqlClient.SqlParameter("@OilMajorId", OilMajorId) ,
                   new System.Data.SqlClient.SqlParameter("@Key",key),
                    new System.Data.SqlClient.SqlParameter("@value",value),
                   new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
                   new System.Data.SqlClient.SqlParameter("@IsActive",IsActive),
                   new System.Data.SqlClient.SqlParameter("@EditParentId",EditParentId),
                   new System.Data.SqlClient.SqlParameter("@ParentID",ParentID)
            };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INS_CM_Addtional_Rule_Mapping", sqlprm);
                ParentID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataSet GetAddtionalRuleMapping(int? RuleId, int? OilMajorId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RuleId", RuleId),   
                   new System.Data.SqlClient.SqlParameter("@OilMajorId", OilMajorId) 
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_Get_AddtionalRule_Mapping", sqlprm);
        }

        public int DeleteAdditionalRuleMapping(int RuleId, int OilMajorID, int ParentId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", RuleId)  ,
                     new System.Data.SqlClient.SqlParameter("@OilMajorID", OilMajorID),
                     new System.Data.SqlClient.SqlParameter("@ParentId", ParentId) 
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Del_CM_Additional_Rule_Mapping", sqlprm);
        }

        public DataSet GetCrewMatrixRankByGroup()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CM_Rank_Group");
        }


        /// <summary>
        /// Active/unactive additional rules
        /// </summary>
        /// <param name="RuleId"></param>
        /// <param name="OilMajorID"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public int ActiveUnactiveAdditionalRule(int RuleId, int OilMajorID, bool IsActive, int ParentId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RuleId", RuleId),
                     new System.Data.SqlClient.SqlParameter("@OilMajorId", OilMajorID),
                     new System.Data.SqlClient.SqlParameter("@Action", "CU"), 
                     new System.Data.SqlClient.SqlParameter("@IsActive", IsActive), 
                     new System.Data.SqlClient.SqlParameter("@ParentId", ParentId) 
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_Get_AddtionalRule_Mapping", sqlprm);
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", RaceID),
                   new System.Data.SqlClient.SqlParameter("@Race", Race),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CD_CRUD_Race", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
        }

        /// <summary>
        ///  CRUD Operation for Crew details Veteran Status
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", VeteranStatusID),
                   new System.Data.SqlClient.SqlParameter("@VeteranStatus", VeteranStatus),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CD_CRUD_VeteranStatus", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
        }

        /// <summary>
        ///  CRUD Operation for Crew details School
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", SchoolID),
                   new System.Data.SqlClient.SqlParameter("@School", School),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CD_CRUD_School", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
        }

        /// <summary>
        ///  CRUD Operation for Crew details Union
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", UnionID),
                   new System.Data.SqlClient.SqlParameter("@Union", Union),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CD_CRUD_Union", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
            }
            catch (Exception ex) { ds = null; }

            return ds;
        }


        /// <summary>
        ///  CRUD Operation for Crew details Union Branch
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", UnionBranchID),
                   new System.Data.SqlClient.SqlParameter("@UnionBranch", UnionBranch),
                   new System.Data.SqlClient.SqlParameter("@UnionID", UnionID),
                   new System.Data.SqlClient.SqlParameter("@AddressLine1", AddressLine1),
                   new System.Data.SqlClient.SqlParameter("@AddressLine2", AddressLine2),
                   new System.Data.SqlClient.SqlParameter("@City", City),
                   new System.Data.SqlClient.SqlParameter("@State", State),
                   new System.Data.SqlClient.SqlParameter("@Country", Country),
                   new System.Data.SqlClient.SqlParameter("@ZipCode", ZipCode),
                    new System.Data.SqlClient.SqlParameter("@PhoneNumber", PhoneNumber),
                     new System.Data.SqlClient.SqlParameter("@Email", Email),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_LIB_CD_CRUD_UnionBranch]", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
            }
            catch (Exception ex) { ds = null; }

            return ds;
        }

        /// <summary>
        ///  CRUD Operation for Crew details Union Branch
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", UnionBranchID),
                   new System.Data.SqlClient.SqlParameter("@UnionBranch", UnionBranch),
                   new System.Data.SqlClient.SqlParameter("@UnionID", UnionID),
                   new System.Data.SqlClient.SqlParameter("@Address", Address),
                   new System.Data.SqlClient.SqlParameter("@Country", Country),
                   new System.Data.SqlClient.SqlParameter("@PhoneNumber", PhoneNumber),
                   new System.Data.SqlClient.SqlParameter("@Email", Email),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_LIB_CD_CRUD_UnionBranch]", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
            }
            catch (Exception ex) { ds = null; }

            return ds;
        }

        public DataSet CRW_GetCDConfiguration(string Key)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Key", Key)
               };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CD_Configuration", obj);
        }

        public int CRW_UpdateConfig(DataTable dt)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@CRW_UDTT_CD_Configuration", dt)
               };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_UPD_CD_Configuration", obj);
        }

        public int CRW_UpdateConfigFields(int ID, string Key, string DisplayName)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   new System.Data.SqlClient.SqlParameter("@Key", Key),
                   new System.Data.SqlClient.SqlParameter("@DisplayName", DisplayName)
               };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_UPD_CD_ConFig_Fields", obj);
        }


        /// <summary>
        ///  CRUD Operation for Crew details Union Book
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
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", UnionBookID),
                   new System.Data.SqlClient.SqlParameter("@UnionBook", UnionBook),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CD_CRUD_UnionBook", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
        }

        public DataTable CRW_LIB_ExportUnionWithBranchs(string Union)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Action", "E"),
                   new System.Data.SqlClient.SqlParameter("@Union", Union),
               };
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_LIB_CD_CRUD_Union", obj);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex) { ds = null; }

            return ds.Tables[0];
        }

        public DataSet CRW_CD_GetConfidentialDetails(int? CrewID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@CrewID", CrewID)
               };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CD_Get_Confidential", obj);
        }

        public int CRW_CD_UPDConfidentialDetails(decimal? Height, decimal? Weight, decimal? Waist,  int? PermanentStatus, string SSN,
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
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
            
                   new System.Data.SqlClient.SqlParameter("@Height", Height),
                           new System.Data.SqlClient.SqlParameter("@Weight", Weight),
                                   new System.Data.SqlClient.SqlParameter("@Waist", Waist),
                                                   new System.Data.SqlClient.SqlParameter("@PermanentStatus", PermanentStatus),
                                                         new System.Data.SqlClient.SqlParameter("@SSN", SSN),
                                                                new System.Data.SqlClient.SqlParameter("@Union", Union),
                                                                  new System.Data.SqlClient.SqlParameter("@UnionBranch", UnionBranch),
                                                                    new System.Data.SqlClient.SqlParameter("@UnionBook", UnionBook),
                                                                      new System.Data.SqlClient.SqlParameter("@School", School),
                                                                        new System.Data.SqlClient.SqlParameter("@SchoolYearGraduated", SchoolYearGraduated),
                                                                          new System.Data.SqlClient.SqlParameter("@HireDate", HireDate),
                                                                            new System.Data.SqlClient.SqlParameter("@Race", Race),
                                                                              new System.Data.SqlClient.SqlParameter("@IDNumber", IDNumber),
                                                                                  new System.Data.SqlClient.SqlParameter("@VeteranStatus", VeteranStatus),
                                                                                    new System.Data.SqlClient.SqlParameter("@Naturaliztion", Naturaliztion),
                                                                                      new System.Data.SqlClient.SqlParameter("@NaturaliztionDate", NaturaliztionDate),
                                                                                        new System.Data.SqlClient.SqlParameter("@CustomField1", CustomField1),
                                                                                          new System.Data.SqlClient.SqlParameter("@CustomField2", CustomField2),
                                                                                            new System.Data.SqlClient.SqlParameter("@CustomField3", CustomField3),
                                                                                             new System.Data.SqlClient.SqlParameter("@CrewID", CrewID),
                                                                                              new System.Data.SqlClient.SqlParameter("@IssuePlaceM", IssuePlaceM),
                                                                                                   new System.Data.SqlClient.SqlParameter("@IssuePlaceT", IssuePlaceT),
                                                                                                        new System.Data.SqlClient.SqlParameter("@MMC", MMC),
                                                                                                             new System.Data.SqlClient.SqlParameter("@TWIC", TWIC),
                                                                                                                  new System.Data.SqlClient.SqlParameter("@IssueDateM", IssueDateM),
                                                                                                                       new System.Data.SqlClient.SqlParameter("@IssueDAteT", IssueDAteT),
                                                                                                                            new System.Data.SqlClient.SqlParameter("@ExpiryDateM", ExpiryDateM),
                                                                                                                                 new System.Data.SqlClient.SqlParameter("@ExpirtDateT", ExpirtDateT),
                                                                                                                                      new System.Data.SqlClient.SqlParameter("@IssuePlaceS", IssuePlaceS),
                                                                                                                                           new System.Data.SqlClient.SqlParameter("@IssueDateS", IssueDateS),
                                                                                                                                                new System.Data.SqlClient.SqlParameter("@Seaman", Seaman),
                                                                                                                                                     new System.Data.SqlClient.SqlParameter("@ExpirtDateS", ExpirtDateS),
                                                                                                                                                      new System.Data.SqlClient.SqlParameter("@TshirtSize", TshirtSize),
                                                                                                                                                        new System.Data.SqlClient.SqlParameter("@Cargosize", Cargosize),
                                                                                                                                                          new System.Data.SqlClient.SqlParameter("@OverallSize", OverallSize),
                                                                                                                                                            new System.Data.SqlClient.SqlParameter("@ShoeSize", ShoeSize),
                                                                                                                                                             new System.Data.SqlClient.SqlParameter("@USVisa", USVisa),
                                                                                                                                                              new System.Data.SqlClient.SqlParameter("@UsVisaIssueDate", UsVisaIssue),
                                                                                                                                                               new System.Data.SqlClient.SqlParameter("@UsVisaNumber", USVisaNumber),
                                                                                                                                                                new System.Data.SqlClient.SqlParameter("@UsVisaExpiryDate", UsVisaExpiry),
                                                                                                                                                                new System.Data.SqlClient.SqlParameter("@ModifiedBy",ModifiedBy)
                   
                   

                                                                                                        
               };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CD_UPD_Confidential", obj);
        }

        public DataSet CRUD_PermanentStatus(string Status, string Action, int UserId, int StatusID, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@ID", StatusID),
                   new System.Data.SqlClient.SqlParameter("@Status", Status),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_LIB_CD_CRUD_PermanentStatus]", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
            }
            catch (Exception ex) { ds = null; }

            return ds;
        }

        public int UPD_CrewDetails_Personal(int CrewID, string updatePanel, string Address1, string Address2, string City, string State, string Fax, int? country, string Zipcode, string Address, string NearestAirport, int? AirportID, int? Veteran, int? School,string SchoolYear, int? Naturalization, string English, DateTime? Naturalizationdate, string Remark,  string CNF1, string CNF2, string CNF3, string Mobile, string Email, string USVisa, DateTime? USVisaExpiry, string USVisaNo, DateTime? USVisaIssue)
        {
              System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                     new System.Data.SqlClient.SqlParameter("@CrewID", CrewID),
                      new System.Data.SqlClient.SqlParameter("@UpdatePanel", updatePanel),
                       new System.Data.SqlClient.SqlParameter("@Address1", Address1),
                        new System.Data.SqlClient.SqlParameter("@Address2", Address2),
                         new System.Data.SqlClient.SqlParameter("@City", City),
                          new System.Data.SqlClient.SqlParameter("@State", State),
                           new System.Data.SqlClient.SqlParameter("@Fax", Fax),
                           new System.Data.SqlClient.SqlParameter("@Country", country),
                            new System.Data.SqlClient.SqlParameter("@Zipcode", Zipcode),
                             new System.Data.SqlClient.SqlParameter("@Address", Address),
                              new System.Data.SqlClient.SqlParameter("@NearestAirport",NearestAirport ),
                               new System.Data.SqlClient.SqlParameter("@NearestAirportID", AirportID),
                                new System.Data.SqlClient.SqlParameter("@VeteranStatus", Veteran),
                                 new System.Data.SqlClient.SqlParameter("@School", School),
                                  new System.Data.SqlClient.SqlParameter("@SchoolYear", SchoolYear),
                                  new System.Data.SqlClient.SqlParameter("@Naturalization",Naturalization ),
                                   new System.Data.SqlClient.SqlParameter("@English_Proficiency", English),
                                    new System.Data.SqlClient.SqlParameter("@Naturalizationdate", Naturalizationdate),
                                     new System.Data.SqlClient.SqlParameter("@Remarks", Remark),
                                             new System.Data.SqlClient.SqlParameter("@CNF1", CNF1),
                                              new System.Data.SqlClient.SqlParameter("@CNF2", CNF2),
                                                new System.Data.SqlClient.SqlParameter("@CNF3", CNF3),
                                                 new System.Data.SqlClient.SqlParameter("@Mobile", Mobile),
                                                  new System.Data.SqlClient.SqlParameter("@Email", Email),
                                                   new System.Data.SqlClient.SqlParameter("@Us_Visa_Flag", USVisa),
                                                    new System.Data.SqlClient.SqlParameter("@Us_Visa_Expiry", USVisaExpiry),
                                                     new System.Data.SqlClient.SqlParameter("@Us_Visa_Number", USVisaNo),
                                                      new System.Data.SqlClient.SqlParameter("@Us_Issue_Date", USVisaIssue)
               };
                return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CD_UPD_PersonalDetails", obj);
            
        }

        public int CRW_CD_UPD_BankAllotment(int CrewID,string Allotment_AccType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                     new System.Data.SqlClient.SqlParameter("@CrewID", CrewID),
                     new System.Data.SqlClient.SqlParameter("@Allotment_AccType", Allotment_AccType)
               };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CD_UPD_BankAllotment", obj);
        }

          public int CRW_CD_UPDATE_MMC(string IssuePlaceM,string MMC,DateTime? IssueDateM,DateTime? ExpiryDateM, int CrewID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               {  new System.Data.SqlClient.SqlParameter("@IssuePlaceM", IssuePlaceM),
                   new System.Data.SqlClient.SqlParameter("@MMC", MMC),
                   new System.Data.SqlClient.SqlParameter("@IssueDateM", IssueDateM),
                   new System.Data.SqlClient.SqlParameter("@ExpiryDateM", ExpiryDateM),
                     new System.Data.SqlClient.SqlParameter("@CrewID", CrewID)
                    
               };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CD_UPD_MMC_Details", obj);
        }

          public int CRW_CD_UPDATE_TWIC( string IssuePlaceT,string TWIC,  DateTime? IssueDateT, DateTime? ExpiryDateT, int CrewID)
          {
              System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@IssuePlaceT", IssuePlaceT),
                   new System.Data.SqlClient.SqlParameter("@TWIC", TWIC),
                   new System.Data.SqlClient.SqlParameter("@IssueDateT", IssueDateT),
                   new System.Data.SqlClient.SqlParameter("@ExpiryDateT", ExpiryDateT),
                     new System.Data.SqlClient.SqlParameter("@CrewID", CrewID)
                    
               };
              return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CD_UPD_TWIC_Details", obj);
          }

        public int CRW_CD_Validate_SSN(string SSN,int CrewID)
          {
              System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@CrewID", CrewID),
                   new System.Data.SqlClient.SqlParameter("@SSN", SSN)
                    
               };
              return Convert.ToInt32( SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_CD_Validate_SSN", obj));
          }

        public DataTable CRW_LIB_AddressValidationSetting()
        {
            System.Data.DataTable ds = new DataTable();
            try
            {ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DTL_AddressValidationSetting",null).Tables[0];}
            catch (Exception ex) { ds = null; }
            return ds;
        }

        public DataTable CRW_LIB_Export_OilMajor()
        {
            System.Data.DataTable ds = new DataTable();
            try
            { ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Export_OilMajor", null).Tables[0]; }
            catch (Exception ex) { ds = null; }
            return ds;
        }
    }
}
