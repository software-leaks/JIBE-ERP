using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.LMS;
using SMS.Data.LMS.LMS;
using System.Data;
using System.IO;
using System.Reflection;
using System.Management;
using System.Diagnostics;

namespace SMS.Business.LMS
{
    public static class BLL_LMS_Training
    {
        // Training List Item
        public static int Ins_Training_Items(string ITEM_NAME, string ITEM_Description, string ITEM_TYPE, string DURATION, string ITEM_PATH, string Attachment_Name, int CREATED_BY, int ACTIVE_STATUS, ref int ItemId)
        {

            return DAL_LMS_Training.Ins_Training_Items(ITEM_NAME, ITEM_Description, ITEM_TYPE, DURATION, ITEM_PATH, Attachment_Name, CREATED_BY, ACTIVE_STATUS, ref  ItemId);
        }
        public static int Ins_Training_Items(string ITEM_NAME, string ITEM_Description, string ITEM_TYPE, string DURATION, string ITEM_PATH, string Attachment_Name, int CREATED_BY, int ACTIVE_STATUS, ref int ItemId,string Menu_Link)
        {

            return DAL_LMS_Training.Ins_Training_Items(ITEM_NAME, ITEM_Description, ITEM_TYPE, DURATION, ITEM_PATH, Attachment_Name, CREATED_BY, ACTIVE_STATUS, ref  ItemId,  Menu_Link);
        }
        public static int Del_Training_Items(Int32 ID, int Deleted_By)
        {
            return DAL_LMS_Training.Del_Training_Items(ID, Deleted_By);
        }

        public static DataTable Get_Training_Items_List(string SearchItemName, int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int UserID)
        {
            return DAL_LMS_Training.Get_Training_Items_List(SearchItemName, Page_Index, Page_Size, ref is_Fetch_Count, UserID);
        }

        public static DataTable Get_MIMEType()
        {
            return DAL_LMS_Training.Get_MIMEType();
        }

        public static int Check_Duplicate_AttachmentFile(string AttachmentFile, string ITEM_NAME, ref string ITEM_PATH, ref int IsItemExist)
        {
            return DAL_LMS_Training.Check_Duplicate_AttachmentFile(AttachmentFile, ITEM_NAME, ref ITEM_PATH, ref IsItemExist);
        }

        // Chapter Details
        public static DataSet Get_ChapterDetails_List(int? Chapter_Id, string SearchItemName, string ITEM_TYPE, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_LMS_Training.Get_ChapterDetails_List(Chapter_Id, SearchItemName, ITEM_TYPE, Page_Index, Page_Size, ref is_Fetch_Count);
        }

        public static DataSet GET_Chapter_Trainer(int? Chapter_Id)
        {
            return DAL_LMS_Training.GET_Chapter_Trainer(Chapter_Id);
        }

        public static DataTable Get_Program_Category()
        {
            return DAL_LMS_Training.Get_Program_Category();
        }

        public static int Check_Chapter_Item(int? Program_Id, int? Chapter_Id, DataTable ChapterItemId)
        {
            return DAL_LMS_Training.Check_Chapter_Item(Program_Id, Chapter_Id, ChapterItemId);
        }

        public static DataTable Ins_Chapter_Details(int? Program_Id, string Program_Description, ref int? Chapter_Id, string Chapter_Name, DataTable ChapterItemId, DataTable ChapterTrainerId, int? CREATED_BY, int? ACTIVE_STATUS)
        {
            return DAL_LMS_Training.Ins_Chapter_Details(Program_Id, Program_Description, ref Chapter_Id, Chapter_Name, ChapterItemId, ChapterTrainerId, CREATED_BY, ACTIVE_STATUS);
        }

        public static int Del_TRAINING_CHAPTER(Int32 ChapterId, int Deleted_By)
        {
            return DAL_LMS_Training.Del_TRAINING_CHAPTER(ChapterId, Deleted_By);
        }

        public static int Check_Duplicate_CHAPTER(string CHAPTER_DESCRIPTION, int Program_Id, int? ChapterID = null)
        {
            return DAL_LMS_Training.Check_Duplicate_CHAPTER(CHAPTER_DESCRIPTION, Program_Id, ChapterID);
        }

        public static int Check_Duplicate_CHAPTER_ITEM(int Item_Id, int Program_Id)
        {
            return DAL_LMS_Training.Check_Duplicate_CHAPTER_ITEM(Item_Id, Program_Id);
        }

        public static DataSet Get_Video_Program_List(string Search)
        {
            return DAL_LMS_Training.Get_Video_Program_List(Search);
        }



        // Program List/Scheduled

        public static DataTable GetFleetList(int UserCompanyID, int VesselManager)
        {
            try
            {
                return DAL_LMS_Training.GetFleetList(UserCompanyID, VesselManager);
            }
            catch
            {
                throw;
            }
        }


        public static DataTable Get_VesselList(int Program_Id, int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        {
            try
            {
                return DAL_LMS_Training.Get_VesselList(Program_Id, FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GET_Program_List_Schedule()
        {
            try
            {
                return DAL_LMS_Training.GET_Program_List_Schedule();
            }
            catch
            {
                throw;
            }
        }

        public static DataSet GET_Program_List(String SearchProgramName, int? Program_Category_Id, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_LMS_Training.GET_Program_List(SearchProgramName, Program_Category_Id, Page_Index, Page_Size, ref is_Fetch_Count);
        }

        public static DataTable GET_FBM_Number()
        {
            return DAL_LMS_Training.GET_FBM_Number();
        }

        public static int Del_TrainingProgram(Int32 Program_Id, int Deleted_By)
        {
            return DAL_LMS_Training.Del_TrainingProgram(Program_Id, Deleted_By);
        }

        public static DataTable GET_EVALUATIONMONTHS(int Program_ID, int Vessel_Id)
        {
            return DAL_LMS_Training.GET_EVALUATIONMONTHS(Program_ID, Vessel_Id);
        }

        public static DataTable GET_EVALUATIONRULES(int Program_ID, int RankID)
        {
            return DAL_LMS_Training.GET_EVALUATIONRULES(Program_ID, RankID);
        }

        public static int Update_EvaluationMonths(int Program_Id, DataTable Vessel_IDS, int Month, int Active_Status, int Created_By)
        {
            return DAL_LMS_Training.Update_EvaluationMonths(Program_Id, Vessel_IDS, Month, Active_Status, Created_By);
        }


        public static int UPDATE_EvaluationRules(int Program_Id, DataTable Vessel_IDS, int RuleID, int Active_Status, int Created_By)
        {
            return DAL_LMS_Training.UPDATE_EvaluationRules(Program_Id, Vessel_IDS, RuleID, Active_Status, Created_By);
        }


        // Training Program 


        public static DataSet GET_Program_Details(Int32? Program_Id)
        {
            return DAL_LMS_Training.GET_Program_Details(Program_Id);
        }

        public static DataTable Get_ProgramDescriptionbyId(Int32? Program_Id)
        {
            return DAL_LMS_Training.Get_ProgramDescriptionbyId(Program_Id);
        }


        public static string Get_ChapterDescriptionbyId(Int32? CHAPTER_ID)
        {
            return DAL_LMS_Training.Get_ChapterDescriptionbyId(CHAPTER_ID);
        }

        // Training Program Summary

        public static DataSet GET_Program_Summary(Int32? Program_Id, int Schedule_ID, int Office_Id, int Vessel_Id)
        {
            return DAL_LMS_Training.GET_Program_Summary(Program_Id, Schedule_ID, Office_Id, Vessel_Id);
        }


        public static int Ins_Program_Details(int? Program_Id, int? Program_Category_Id, string Program_Name, string Program_Description, int? Duration, string PROGRAM_TYPE, int? CREATED_BY, int? ACTIVE_STATUS)
        {

            return DAL_LMS_Training.Ins_Program_Details(Program_Id, Program_Category_Id, Program_Name, Program_Description, Duration, PROGRAM_TYPE, CREATED_BY, ACTIVE_STATUS);
        }

        // Crew Training Index

        public static DataSet Get_Scheduled_Program_List(DataTable FleetCode, DataTable Vessel_ID, string ProgramName, DateTime? DueDateFrom, DateTime? DueDateTo, int? Program_Category_Id, int? Page_Index, int? Page_Size, string SortByColumn, string SortDirection, ref int is_Fetch_Count)
        {
            return DAL_LMS_Training.Get_Scheduled_Program_List(FleetCode, Vessel_ID, ProgramName, DueDateFrom, DueDateTo, Program_Category_Id, Page_Index, Page_Size, SortByColumn, SortDirection, ref  is_Fetch_Count);
        }

        public static DataSet Get_Programs_To_Sync(int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_LMS_Training.Get_Programs_To_Sync(Page_Index, Page_Size, ref is_Fetch_Count);
        }

        public static DataSet GET_Programs_To_Sync_His(int? Page_Index, int? Page_Size, DateTime? DateHis, ref int is_Fetch_Count)
        {
            return DAL_LMS_Training.GET_Programs_To_Sync_His(Page_Index, Page_Size, DateHis, ref is_Fetch_Count);
        }

        public static int Ins_Program_Sync(DataTable TBL_DATA, int CREATED_BY)
        {
            return DAL_LMS_Training.Ins_Program_Sync(TBL_DATA, CREATED_BY);
        }

        public static byte[] DownloadFile(string FileFullName)
        {
            byte[] FileContent = null;
            try
            {
                using (FileStream fileStream = new FileStream(FileFullName, FileMode.Open, FileAccess.Read))
                {
                    FileContent = new byte[fileStream.Length];
                    fileStream.Read(FileContent, 0, (int)fileStream.Length);
                    fileStream.Close();
                    fileStream.Dispose();
                    return FileContent;
                }
            }
            catch { }

            return FileContent;
        }



        public static DataSet GET_FileList_To_Sync(DataTable TBL_DATA)
        {


            return DAL_LMS_Training.GET_FileList_To_Sync(TBL_DATA);

        }



        public static void Update_Traing_ITem(int? ID, string SearchUrl)
        {
              DAL_LMS_Training.Update_Traing_ITem(ID,SearchUrl);
        }

        public static string RAR(string path, List<string> filenames)
        {
            //Create Bat file

            string rarFileName = "";
            if (!path.EndsWith(@"\"))
             path=   path + @"\";
            string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(filenames[0]);
            string batfilename = path + FileNameWithoutExtension + "z" + ".bat";


          

            try
            {
                TextWriter tw = new StreamWriter(batfilename);
                tw.WriteLine(batfilename.Substring(0,1) + ":");
                tw.WriteLine(@"cd\");
                tw.WriteLine("cd " + path);
                tw.WriteLine("rar a  " + FileNameWithoutExtension + ".rar " + Path.GetFileName(filenames[0]));

                if (filenames.Count > 0)
                    for (int i = 0; i < filenames.Count; i++)
                    {
                        if (File.Exists(path + filenames[i]))
                            tw.WriteLine("rar a -ep  " + FileNameWithoutExtension + @".rar   """ + filenames[i] + @"""");
                    }
                //foreach (var filename in filenames)
                //{

                //}


                tw.Close();

                // create cmd process and execute bat file
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = true;
                proc.StartInfo.FileName = batfilename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
                rarFileName = FileNameWithoutExtension + ".rar";
                return rarFileName;
            }
            catch (Exception ex)
            {
                string filePath = @"C:\Error.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                //Delete bat file
                throw ex;
            }
            finally
            {
                //Delete bat file
                if (File.Exists(batfilename))
                    File.Delete(batfilename);
                //if (File.Exists(path + "\\" + filename + ".txt"))
                //    File.Delete(path + "\\" + filename + ".txt");
            }


        }


        public static DataTable GET_Programs_To_Sync_Date_List()
        {
            return DAL_LMS_Training.GET_Programs_To_Sync_Date_List();
        }

        public static void Del_TrainingProgram_Chk(int PROGRAM_ID, ref int Row_Num)
        {

            DAL_LMS_Training.Del_TrainingProgram_Chk(PROGRAM_ID, ref Row_Num);

        }
        public static DataTable GET_DRILL_SCHEDULE(int? Vessel_ID)
        {
            return DAL_LMS_Training.GET_DRILL_SCHEDULE(Vessel_ID);
        }

        public static DataTable Get_Training_Calendar(string JobType, DateTime? StartDate, DateTime? EndDate, int UserID)
        {
            return DAL_LMS_Training.Get_Training_Calendar(JobType, StartDate, EndDate, UserID);
        }
        public static DataSet GET_YEARLY_DRILL_REPORT(int Month, int Year, int? Vessel_ID)
        {
            return DAL_LMS_Training.GET_YEARLY_DRILL_REPORT(Month, Year, Vessel_ID);
        }
        #region updatetrainingprogram
        public static void Update_Program_Details(int? Program_Id, string Frequency_Type, DateTime? EffectiveStartDate, DateTime? EffectiveEndDate, int? UserId)
        {
            DAL_LMS_Training.Update_Program_Details(Program_Id, Frequency_Type, EffectiveStartDate, EffectiveEndDate, UserId);
        }
        #endregion

        #region VesselVideos
        public static DataTable GET_VESSEL_VIDEOS(int? Id)
        {
            return DAL_LMS_Training.GET_VESSEL_VIDEOS(Id);
        }
        public static DataTable INSUPD_VESSEL_VIDEOS(int ID, int Type, string Name, string OriginalFileName, string FileName, int ParentId, int UserId)
        {
            return DAL_LMS_Training.INSUPD_VESSEL_VIDEOS(ID, Type, Name,OriginalFileName, FileName, ParentId, UserId);
        }


        public static DataTable DEL_VESSEL_VIDEOS(int? ID, int UserId)
        {
            return DAL_LMS_Training.DEL_VESSEL_VIDEOS(ID, UserId);

        }
        public static string GET_VideoFileName(int? Id)
        {
            return DAL_LMS_Training.GET_VideoFileName(Id);
        }

        public static string Validate_SeaStaff(int UserID)
        {
            return DAL_LMS_Training.Validate_SeaStaff(UserID);
        }

        #endregion
    }
}
