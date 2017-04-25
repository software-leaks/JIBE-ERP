using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace SMS.Data.LMS.LMS
{
    public class DAL_LMS_Training
    {
        private static string connection = "";
        public DAL_LMS_Training(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_LMS_Training()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        // Training Item 

        #region . Get

        public static DataTable Get_Training_Items_List(string SearchItemName, int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] {

                                                    
                                                      new SqlParameter("@SearchItemName",SearchItemName),
                                                      new SqlParameter("@PAGE_INDEX",Page_Index),
                                                      new SqlParameter("@PAGE_SIZE",Page_Size),
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),

                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_TRAINING_ITEMS_List", prm).Tables[0];

            is_Fetch_Count = int.Parse(prm[prm.Length - 1].Value.ToString());
            return dt;

        }


        public static DataSet Get_Video_Program_List(string Search)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Video_Program_List", new SqlParameter("@search", Search));
        }


        public static DataTable Get_MIMEType()
        {
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_MIMEType").Tables[0];
            return dt;
        }


        #endregion

        #region . Insert

        public static int Ins_Training_Items(string ITEM_NAME, string ITEM_Description, string ITEM_TYPE, string DURATION, string ITEM_PATH, string Attachment_Name, int CREATED_BY, int ACTIVE_STATUS, ref int ItemId)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
        { 
                   new System.Data.SqlClient.SqlParameter("@ITEM_NAME",ITEM_NAME),
                   new System.Data.SqlClient.SqlParameter("@ITEM_Description", ITEM_Description),
                   new System.Data.SqlClient.SqlParameter("@ITEM_TYPE", ITEM_TYPE),
                   new System.Data.SqlClient.SqlParameter("@DURATION",DURATION),
                   new System.Data.SqlClient.SqlParameter("@ITEM_PATH", ITEM_PATH),
                   new System.Data.SqlClient.SqlParameter("@Attachment_Name", Attachment_Name),
                   new System.Data.SqlClient.SqlParameter("@CREATED_BY", CREATED_BY),
                   new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ACTIVE_STATUS),
                   new System.Data.SqlClient.SqlParameter("@ItemId", SqlDbType.Int)
 
        };

                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Ins_TRAINING_ITEMS", sqlprm);
                ItemId = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);


                return 0;
            }
            catch (Exception ex)
            {

                throw ex;


            }

        }
        public static int Ins_Training_Items(string ITEM_NAME, string ITEM_Description, string ITEM_TYPE, string DURATION, string ITEM_PATH, string Attachment_Name, int CREATED_BY, int ACTIVE_STATUS, ref int ItemId,string Menu_Link)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
        { 
                   new System.Data.SqlClient.SqlParameter("@ITEM_NAME",ITEM_NAME),
                   new System.Data.SqlClient.SqlParameter("@ITEM_Description", ITEM_Description),
                   new System.Data.SqlClient.SqlParameter("@ITEM_TYPE", ITEM_TYPE),
                   new System.Data.SqlClient.SqlParameter("@DURATION",DURATION),
                   new System.Data.SqlClient.SqlParameter("@ITEM_PATH", ITEM_PATH),
                   new System.Data.SqlClient.SqlParameter("@Attachment_Name", Attachment_Name),
                   new System.Data.SqlClient.SqlParameter("@CREATED_BY", CREATED_BY),
                   new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ACTIVE_STATUS),
                     new System.Data.SqlClient.SqlParameter("@Menu_Link", Menu_Link),
                   new System.Data.SqlClient.SqlParameter("@ItemId", SqlDbType.Int),
                 
 
        };

                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Ins_TRAINING_ITEMS", sqlprm);
                ItemId = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);


                return 0;
            }
            catch (Exception ex)
            {

                throw ex;


            }

        }
        #endregion

        #region . Delete

        public static int Del_Training_Items(Int32 ID, int Deleted_By)
        {

            System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By",Deleted_By),
                   new SqlParameter("@return",SqlDbType.Int)
            };

            sqlpram[sqlpram.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Del_TRAINING_ITEMS", sqlpram);
            return Convert.ToInt32(sqlpram[sqlpram.Length - 1].Value);



        }

        #endregion


        #region . Check_Duplicate_AttachmentFile

        public static int Check_Duplicate_AttachmentFile(string AttachmentFile, string ITEM_NAME, ref string ITEM_PATH, ref int IsItemExist)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {   
                                            new SqlParameter("@ITEM_NAME",ITEM_NAME),
                                            new SqlParameter("@AttachmentFile",AttachmentFile),
                                            new SqlParameter("@ITEM_PATH",SqlDbType.VarChar,200,ITEM_PATH),
                                            new SqlParameter("@IsItemExist",SqlDbType.Int,IsItemExist),
                                            new SqlParameter("@return",SqlDbType.Int),
                                            
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            sqlprm[sqlprm.Length - 2].Direction = ParameterDirection.InputOutput;
            sqlprm[sqlprm.Length - 3].Direction = ParameterDirection.InputOutput;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Check_Duplicate_AttachmentFileAndItem", sqlprm);

            IsItemExist = Convert.ToInt32(sqlprm[sqlprm.Length - 2].Value);
            ITEM_PATH = Convert.ToString(sqlprm[sqlprm.Length - 3].Value);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        #endregion




        // Chapter Details

        #region . Get

        public static DataSet Get_ChapterDetails_List(int? Chapter_Id, string SearchItemName,string ITEM_TYPE, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@Chapter_Id",Chapter_Id),
                                                       new SqlParameter("@SearchItemName",SearchItemName),
                                                        new SqlParameter("@ITEM_TYPE",ITEM_TYPE),
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                       new SqlParameter("@is_Fetch_Count",is_Fetch_Count)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Chapter_Item_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }


        public static DataSet GET_Chapter_Trainer(int? Chapter_Id)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@Chapter_Id",Chapter_Id)
                                                    };


            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Chapter_Trainer", prm);

            return ds;

        }

        public static DataTable Get_Program_Category()
        {

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_Program_Category").Tables[0];
            return dt;

        }


        public static int Check_Chapter_Item(int? Program_Id, int? Chapter_Id, DataTable ChapterItemId)
        {

            SqlParameter[] prm = new SqlParameter[] {  
                                                       new SqlParameter("@Program_Id",Program_Id),
                                                       new SqlParameter("@Chapter_Id",Chapter_Id),
                                                       new SqlParameter("@ChapterItemId",ChapterItemId),
                                                       new SqlParameter("return",SqlDbType.Int)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Check_Chapter_Item", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);

        }

        public static int Check_Duplicate_CHAPTER(string CHAPTER_DESCRIPTION, int Program_Id, int? ChapterID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CHAPTER_DESCRIPTION",CHAPTER_DESCRIPTION),
                                            new SqlParameter("@Program_Id",Program_Id),
                                            new SqlParameter("@ChapterID",ChapterID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Check_Duplicate_CHAPTER", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Check_Duplicate_CHAPTER_ITEM(int Item_Id, int Program_Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Item_Id",Item_Id),
                                            new SqlParameter("@Program_Id",Program_Id),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Check_Duplicate_CHAPTER_ITEM", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        #endregion



        #region . Insert

        public static DataTable Ins_Chapter_Details(int? Program_Id, string Program_Description, ref int? Chapter_Id, string Chapter_Name, DataTable ChapterItemId, DataTable ChapterTrainerId, int? CREATED_BY, int? ACTIVE_STATUS)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                                                       new SqlParameter("@Program_Id",Program_Id),
                                                       new SqlParameter("@Program_Description",Program_Description),
                                                       new SqlParameter("@Chapter_Id",Chapter_Id),
                                                       new SqlParameter("@Chapter_Name",Chapter_Name),
                                                       new SqlParameter("@ChapterItemId",ChapterItemId),
                                                       new SqlParameter("@ChapterTrainerId",ChapterTrainerId),
                                                       new SqlParameter("@CREATED_BY",CREATED_BY),
                                                       new SqlParameter("@ACTIVE_STATUS",ACTIVE_STATUS)
                                                      
                                                    };
            prm[prm.Length - 6].Direction = ParameterDirection.InputOutput;
            DataTable dtChapterItemExist = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Ins_Chapter_Details", prm).Tables[0];
            Chapter_Id = Convert.ToInt32(prm[prm.Length - 6].Value);
            return dtChapterItemExist;

        }

        #endregion

        #region . Delete

        public static int Del_TRAINING_CHAPTER(Int32 ChapterId, int Deleted_By)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CHAPTER_ID", ChapterId),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By",Deleted_By)
            };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Del_TRAINING_CHAPTER", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        #endregion

        //Program List/Scheduled

        #region . Get

        public static DataTable GetFleetList(int UserCompanyID, int VesselManager)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FleetList", new SqlParameter("UserCompanyID", UserCompanyID), new SqlParameter("VesselManager", VesselManager)).Tables[0];
        }


        public static DataTable Get_VesselList(int? Program_Id, int? FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_INF_Get_VesselList", new SqlParameter("@Program_Id", Program_Id),
                                                                                                new SqlParameter("@FleetID", FleetID),
                                                                                                new SqlParameter("@VesselID", VesselID),
                                                                                                new SqlParameter("@Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("@SearchText", SearchText),
                                                                                                new SqlParameter("@UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("@IsVessel", IsVessel)
                                                                                                ).Tables[0];


        }


        public static DataSet GET_Program_List(string SearchProgramName, int? Program_Category_Id, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@SearchProgramName",SearchProgramName),
                                                        new SqlParameter("@Program_Category_Id",Program_Category_Id),

                                                       
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Program_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }


        public static DataTable GET_Program_List_Schedule()
        {

            DataTable dt_Program_List_Schedule = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Program_List_Schedule").Tables[0];
            return dt_Program_List_Schedule;

        }





        public static DataTable GET_FBM_Number()
        {

            DataTable dt_FBM_Number = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_FBM_Number").Tables[0];
            return dt_FBM_Number;

        }



        public static DataTable GET_EVALUATIONMONTHS(int Program_ID, int Vessel_Id)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_EVALUATIONMONTHS", new SqlParameter("@Program_ID", Program_ID), new SqlParameter("@Vessel_Id", Vessel_Id)).Tables[0];
        }

        public static DataTable GET_EVALUATIONRULES(int Program_ID, int Vessel_Id)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_EVALUATIONRULES", new SqlParameter("@Program_ID", Program_ID), new SqlParameter("@Vessel_Id", Vessel_Id)).Tables[0];
        }


        public static int UPDATE_EvaluationRules(int Program_Id, DataTable Vessel_IDS, int RuleID, int Active_Status, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Program_Id",Program_Id),
                                            new SqlParameter("@Vessel_IDS",Vessel_IDS),
                                            new SqlParameter("@RuleID",RuleID),
                                            new SqlParameter("@Active_Status",Active_Status),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Update_EvaluationRules", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public static int Update_EvaluationMonths(int Program_Id, DataTable Vessel_IDS, int Month, int Active_Status, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Program_Id",Program_Id),
                                            new SqlParameter("@Vessel_IDS",Vessel_IDS),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Active_Status",Active_Status),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Update_EvaluationMonths", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        #endregion


        #region . Delete

        public static int Del_TrainingProgram(Int32 Program_Id, int Deleted_By)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Program_Id", Program_Id),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By",Deleted_By)
            };

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Del_TrainingProgram", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        #endregion

        // Program Details


        public static DataSet GET_Program_Details(Int32? Program_Id)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Program_Details", new SqlParameter("@Program_Id", Program_Id));
        }

        public static DataTable Get_ProgramDescriptionbyId(Int32? Program_Id)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_ProgramDescriptionbyId", new SqlParameter("@Program_Id", Program_Id)).Tables[0];
        }

        public static string Get_ChapterDescriptionbyId(Int32? CHAPTER_ID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_ChapterDescriptionbyId", new SqlParameter("@CHAPTER_ID", CHAPTER_ID)).Tables[0].Rows[0].Field<string>("CHAPTER_DESCRIPTION");
        }

        // Training Program Details at Office.

        public static DataSet GET_Program_Summary(Int32? Program_Id, int Schedule_ID, int Office_Id, int Vessel_Id)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                                                       new SqlParameter("@Program_Id",Program_Id),
                                                       new SqlParameter("@Schedule_ID",Schedule_ID),
                                                         new SqlParameter("@Office_Id",Office_Id),
                                                           new SqlParameter("@Vessel_Id",Vessel_Id),
             };

            DataSet dsPrograms = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Program_Summary", prm);
            dsPrograms.Tables[0].TableName = "Chapter";
            dsPrograms.Tables[1].TableName = "Items";
            dsPrograms.Tables[2].TableName = "Attendees";
            return dsPrograms;
        }


        public static int Ins_Program_Details(int? Program_Id, int? Program_Category_Id, string Program_Name, string Program_Description, int? Duration, string PROGRAM_TYPE, int? CREATED_BY, int? ACTIVE_STATUS)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                                                       new SqlParameter("@Program_Id",Program_Id),
                                                       new SqlParameter("@Program_Category_Id",Program_Category_Id),
                                                       new SqlParameter("@Program_Name",Program_Name),
                                                       new SqlParameter("@Program_Description",Program_Description),
                                                       new SqlParameter("@CREATED_BY",CREATED_BY),
                                                       new SqlParameter("@ACTIVE_STATUS",ACTIVE_STATUS),
                                                       new SqlParameter("@DURATION",Duration),
                                                       new SqlParameter("@PROGRAM_TYPE",PROGRAM_TYPE),
                                                       new SqlParameter("@returnvalue",DbType.Int32) 
                                                    };

            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Ins_Program_Details", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }


        // Crew Training Index


        public static DataSet Get_Scheduled_Program_List(DataTable FleetCode, DataTable Vessel_ID, string ProgramName, DateTime? DueDateFrom, DateTime? DueDateTo, int? Program_Category_Id, int? Page_Index, int? Page_Size, string SortByColumn, string SortDirection, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@FLEET_ID",FleetCode),
                                                       new SqlParameter("@Vessel_ID",Vessel_ID),
                                                       new SqlParameter("@ProgramName",ProgramName),
                                                       new SqlParameter("@DueDateFrom",DueDateFrom),
                                                       new SqlParameter("@DueDateTo",DueDateTo),
                                                       new SqlParameter("@Program_Category_Id",Program_Category_Id),
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                        new SqlParameter("@SortByColumn",SortByColumn),
                                                         new SqlParameter("@SortDirection",SortDirection),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Scheduled_Program_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }


        public static DataSet GET_FileList_To_Sync(DataTable TBL_DATA)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@TBL_DATA",TBL_DATA)
                                                    };


            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_FileList_To_Sync", prm);

            return ds;

        }

        public static DataSet Get_Programs_To_Sync(int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {

            SqlParameter[] prm = new SqlParameter[] {
                                                        new SqlParameter("@PAGE_INDEX",Page_Index),
                                                       new SqlParameter("@PAGE_SIZE",Page_Size),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Programs_To_Sync", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }

        public static DataSet GET_Programs_To_Sync_His(int? Page_Index, int? Page_Size, DateTime? DateHis, ref int is_Fetch_Count)
        {

            SqlParameter[] prm = new SqlParameter[] {
                                                        new SqlParameter("@PAGE_INDEX",Page_Index),
                                                       new SqlParameter("@PAGE_SIZE",Page_Size),
                                                        new SqlParameter("@HIS_DATE",DateHis),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Programs_To_Sync_His", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }


        public static int Ins_Program_Sync(DataTable TBL_DATA, int CREATED_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TBL_DATA",TBL_DATA),   
                                            new SqlParameter("@CREATED_BY",CREATED_BY),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Ins_Program_Sync", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static DataTable GET_Programs_To_Sync_Date_List()
        {

            DataTable dt_Date_List = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_Programs_To_Sync_Date_List").Tables[0];
            return dt_Date_List;

        }

        public static void Del_TrainingProgram_Chk(int PROGRAM_ID, ref int Row_Num)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@PROGRAM_ID",PROGRAM_ID),
                                                       new SqlParameter("@Row_Num",Row_Num)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Del_TrainingProgram_Chk", prm);
            Row_Num = Convert.ToInt32(prm[prm.Length - 1].Value);


        }

        public static DataTable GET_DRILL_SCHEDULE(int? Vessel_ID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@Vessel_ID",Vessel_ID),
                                                     
                                                    };

            DataTable dt_DRILL = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_DRILL_SCHEDULE", prm).Tables[0];
            return dt_DRILL;

        }

        public static DataTable Get_Training_Calendar(string JobType, DateTime? StartDate, DateTime? EndDate, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@JobType", JobType),
                   new SqlParameter("@StartDate", StartDate),
                   new SqlParameter("@EndDate", EndDate),
                   new SqlParameter("@UserID", UserID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Get_Training_Calendar", sqlprm).Tables[0];
        }

        public static DataSet GET_YEARLY_DRILL_REPORT(int Month, int Year, int? Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@Month", Month),
                   new SqlParameter("@Year", Year), 
                    new SqlParameter("@Vessel_ID", Vessel_ID), 
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_YEARLY_DRILL_REPORT", sqlprm);
        }


        #region updatetrainingprogram
        public static void Update_Program_Details(int? Program_Id, string Frequency_Type, DateTime? EffectiveStartDate, DateTime? EffectiveEndDate, int? UserId)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                                                       new SqlParameter("@Program_Id",Program_Id),
                                                       new SqlParameter("@Frequency_Type",Frequency_Type),
                                                       new SqlParameter("@EffectiveStartDate",EffectiveStartDate),
                                                       new SqlParameter("@EffectiveEndDate",EffectiveEndDate),
                                                       new SqlParameter("@UserId",UserId)
                                                    };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_Update_Program_Details", prm);

        }
        public static void Update_Traing_ITem(int? ID, string SearchUrl)
        {
            SqlParameter[] prm = new SqlParameter[] {  
                                                       new SqlParameter("@ID",ID),
                                                       new SqlParameter("@SearchUrl",SearchUrl), 
                                                    };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_UPD_TRAING_ITEM", prm);

        }
        #endregion

        #region vesselVideos
        public static DataTable GET_VESSEL_VIDEOS(int? ID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@ID",ID),
                                                     
                                                    };

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_VESSEL_VIDEOS", prm).Tables[0];
            return dt;

        }


        public static DataTable INSUPD_VESSEL_VIDEOS(int ID, int Type, string Name, string OriginalFileName, string FileName, int ParentId, int UserId)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@ID",ID),
                                                        new SqlParameter("@Type",Type),
                                                         new SqlParameter("@Name",Name),
                                                           new SqlParameter("@OriginalFileName",OriginalFileName),
                                                          new SqlParameter("@FileName",FileName),
                                                        
                                                           new SqlParameter("@ParentId",ParentId),
                                                            new SqlParameter("@UserId",UserId),

                                                     
                                                    };
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_INSUPD_VESSEL_VIDEOS", prm).Tables[0];
            return dt;
        }

        public static DataTable DEL_VESSEL_VIDEOS(int? ID, int UserId)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@ID",ID),
                                                        new SqlParameter("@UserId",UserId),
                                                    };

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_DEL_VESSEL_VIDEOS", prm).Tables[0];
            return dt;

        }
        public static string GET_VideoFileName(int? Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@Id", Id),
                    
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_VideoFileName", sqlprm).Tables[0].Rows[0][0].ToString();
        }


        public static string Validate_SeaStaff( int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@Id", UserID),
                    
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_Check_SeaStaff", sqlprm).Tables[0].Rows[0][0].ToString();
        }


        #endregion
    }
}
