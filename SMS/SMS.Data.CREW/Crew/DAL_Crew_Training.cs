using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Training
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        private static string connection = "";

        public DAL_Crew_Training(string ConnectionString)
        {
            connection = ConnectionString;
        }

        static DAL_Crew_Training()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_Crew_Trainings_DL(int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_CrewTrainings", sqlprm).Tables[0];
        }
        public static DataTable Get_Crew_Trainings_DL(int CrewID, int TrainingID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@TrainingID",TrainingID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_CrewTrainings", sqlprm).Tables[0];
        }
        public static int INSERT_Training_DL(int CrewID, DateTime Date_Of_Training, int TrainingType, int Trainer, string Remarks, decimal Result, int Created_By, DataTable Attachments, int CHAPTER_ID, string ITEM_NAME)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Date_Of_Training",Date_Of_Training),
                                            new SqlParameter("@TrainingType",TrainingType),
                                            new SqlParameter("@Trainer",Trainer),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Result",Result),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Attachments",Attachments),
                                             new SqlParameter("@CHAPTER_ID",CHAPTER_ID),
                                              new SqlParameter("@ITEM_NAME",ITEM_NAME),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Insert_Training", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Training_DL(int TrainingID, DateTime Date_Of_Training, int TrainingType, int Trainer, string Remarks, decimal Result, int Updated_By, DataTable Attachments)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingID",TrainingID),
                                            new SqlParameter("@Date_Of_Training",Date_Of_Training),
                                            new SqlParameter("@TrainingType",TrainingType),
                                            new SqlParameter("@Trainer",Trainer),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Result",Result),
                                            new SqlParameter("@Modified_By",Updated_By),
                                            new SqlParameter("@Attachments",Attachments),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Update_Training", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Training_DL(int TrainingID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingID",TrainingID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Delete_Training", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Crew_TrainingTypes_DL()
        {
            int RowCount = 0;
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RowCount",RowCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_TrainingTypes", sqlprm).Tables[0];
        }
        public static DataTable Get_Crew_TrainingTypes_DL(string Filter, int CurrentPageIndex, int PageSize, ref  int RowCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Filter",Filter),
                                            new SqlParameter("@CurrentPageIndex",CurrentPageIndex),
                                            new SqlParameter("@PageSize",PageSize),
                                            new SqlParameter("@RowCount",RowCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_TrainingTypes", sqlprm);

            RowCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Crew_TrainingTypes_DL(int TrainingTypeID)
        {
            int RowCount = 0;
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingTypeID",TrainingTypeID),
                                            new SqlParameter("@RowCount",RowCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_TrainingTypes", sqlprm).Tables[0];
        }


        public static int INSERT_TrainingType_DL(string TrainingType, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingType",TrainingType),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Insert_TrainingType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_TrainingType_DL(int TrainingTypeID, string TrainingType, int Updated_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingTypeID",TrainingTypeID),                                            
                                            new SqlParameter("@TrainingType",TrainingType),
                                            new SqlParameter("@Modified_By",Updated_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Update_TrainingType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_TrainingType_DL(int TrainingTypeID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingTypeID",TrainingTypeID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Delete_TrainingType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Crew_Trainers_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_CrewTrainers", sqlprm).Tables[0];
        }
        public static int INSERT_Crew_Trainer_DL(int TrainerID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainerID",TrainerID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Insert_Trainer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Crew_Trainer_DL(int TrainerID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainerID",TrainerID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Delete_Trainer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Training_Attachments_DL(int TrainingID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TrainingID",TrainingID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Get_Training_Attachments", sqlprm).Tables[0];
        }
        public static int INSERT_Training_Attachment_DL(string AttachmentName, string AttachmentPath, int AttachmentSize, int TrainingID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttachmentName",AttachmentName),
                                            new SqlParameter("@AttachmentPath",AttachmentPath),
                                            new SqlParameter("@AttachmentSize",AttachmentSize),
                                            new SqlParameter("@TrainingID",TrainingID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Insert_Training_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Training_Attachment_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_TRG_SP_Delete_Training_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
