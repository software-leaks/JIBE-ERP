using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_Training
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable Get_Crew_Trainings(int CrewID, int UserID)
        {
            return DAL_Crew_Training.Get_Crew_Trainings_DL(CrewID, UserID);
        }
        public static DataTable Get_Crew_Trainings(int CrewID, int TrainingID, int UserID)
        {
            return DAL_Crew_Training.Get_Crew_Trainings_DL(CrewID, TrainingID, UserID);
        }
        public static int INSERT_Training(int CrewID, DateTime Date_Of_Training, int TrainingType, int Trainer, string Remarks, decimal Result, int Created_By, DataTable Attachments, int CHAPTER_ID, string ITEM_NAME)
        {
            return DAL_Crew_Training.INSERT_Training_DL(CrewID, Date_Of_Training, TrainingType, Trainer, Remarks, Result, Created_By, Attachments, CHAPTER_ID, ITEM_NAME);
        }
        public static int UPDATE_Training(int TrainingID, DateTime Date_Of_Training, int TrainingType, int Trainer, string Remarks, decimal Result, int Updated_By, DataTable Attachments)
        {
            return DAL_Crew_Training.UPDATE_Training_DL(TrainingID, Date_Of_Training, TrainingType, Trainer, Remarks, Result, Updated_By, Attachments);
        }
        public static int DELETE_Training(int TrainingID, int Deleted_By)
        {
            return DAL_Crew_Training.DELETE_Training_DL(TrainingID, Deleted_By);
        }

        public static DataTable Get_Crew_TrainingTypes()
        {
            return DAL_Crew_Training.Get_Crew_TrainingTypes_DL();
        }
        public static DataTable Get_Crew_TrainingTypes(string Filter, int CurrentPageIndex, int PageSize, ref  int RowCount)
        {
            return DAL_Crew_Training.Get_Crew_TrainingTypes_DL(Filter, CurrentPageIndex, PageSize, ref  RowCount);
        }
        public static DataTable Get_Crew_TrainingTypes(int TrainingTypeID)
        {
            return DAL_Crew_Training.Get_Crew_TrainingTypes_DL(TrainingTypeID);
        }
        
        public static int INSERT_TrainingType(string TrainingType, int Created_By)
        {
            return DAL_Crew_Training.INSERT_TrainingType_DL( TrainingType, Created_By);
        }
        public static int UPDATE_TrainingType(int TrainingTypeID, string TrainingType, int Updated_By)
        {
            return DAL_Crew_Training.UPDATE_TrainingType_DL(TrainingTypeID, TrainingType, Updated_By);
        }
        public static int DELETE_TrainingType(int TrainingTypeID, int Deleted_By)
        {
            return DAL_Crew_Training.DELETE_TrainingType_DL(TrainingTypeID, Deleted_By);
        }

        public static DataTable Get_Crew_Trainers(int UserID)
        {
            return DAL_Crew_Training.Get_Crew_Trainers_DL(UserID);
        }
        public static int INSERT_Crew_Trainer(int TrainerID, int Created_By)
        {
            return DAL_Crew_Training.INSERT_Crew_Trainer_DL(TrainerID, Created_By);
        }
        public static int DELETE_Crew_Trainer(int TrainerID, int Deleted_By)
        {
            return DAL_Crew_Training.DELETE_Crew_Trainer_DL(TrainerID, Deleted_By);
        }

        public static DataTable Get_Training_Attachments(int TrainingID, int UserID)
        {
            return DAL_Crew_Training.Get_Training_Attachments_DL(TrainingID, UserID);
        }
        public static int INSERT_Training_Attachment(string AttachmentName, string AttachmentPath, int AttachmentSize, int TrainingID, int Created_By)
        {
            return DAL_Crew_Training.INSERT_Training_Attachment_DL(AttachmentName, AttachmentPath, AttachmentSize, TrainingID, Created_By);
        }
        public static int DELETE_Training_Attachment(int ID, int Deleted_By)
        {
            return DAL_Crew_Training.DELETE_Training_Attachment_DL(ID, Deleted_By);
        }
    }
}
