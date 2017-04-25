using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using SMS.Data.FMS;
/// <summary>
/// Summary description for FMSDocument
/// </summary>
/// 

namespace SMS.Business.FMS
{
    public class BLL_FMS_Document
    {
        DAL_FMS_Document obj = new DAL_FMS_Document();

        public DataSet FMS_GET_FORM_TREE(string FolderName)
        {
            return obj.FMS_GET_FORM_TREE(FolderName);
        }
        public int FMS_Get_FolderExist(string FileName, string FilePath)
        {

            return obj.FMS_Get_FolderExist(FileName, FilePath);
        }
        public DataSet FMS_Get_DocScheduleListByFolder(int File_ID, int PageIndex, int PageSize, ref int isfetchcount)
        {
            return obj.FMS_Get_DocScheduleListByFolder(File_ID, PageIndex, PageSize, ref isfetchcount);
        }
        public DataSet FMS_Get_FileRejectionInfo(int File_ID)
        {
            return obj.FMS_Get_FileRejectionInfo(File_ID);
        }
        public DataSet FMS_Get_FormTypeByID(int ID)
        {
            return obj.FMS_Get_FormTypeByID(ID);
        }
        public int FMS_Update_FormType(int FormTypeID, string FormType, int UserID)
        {

            return obj.FMS_Update_FormType(FormTypeID, FormType, UserID);
        }
        public int FMS_Restore_FormType(int FormTypeID, int UserID)
        {

            return obj.FMS_Restore_FormType(FormTypeID, UserID);
        }
        public int FMS_Insert_FormType(string FormType, int UserID)
        {

            return obj.FMS_Insert_FormType(FormType, UserID);
        }
        public int FMS_Delete_FormType(int FormTypeID, int UserID)
        {

            return obj.FMS_Delete_FormType(FormTypeID, UserID);
        }

        public int FMS_Delete_FileApprovar(int AppLevelID, int UserID)
        {
            return obj.FMS_Delete_FileApprovar(AppLevelID, UserID);
        }
        public DataSet FMS_Get_ScheduleFileApprovalByScheduleID(int StatusID, int VesselID, int OfficeID, int FileID, int ApprovedBy, ref int isfetchcount)
        {
            return obj.FMS_Get_ScheduleFileApprovalByScheduleID(StatusID, VesselID, OfficeID, FileID, ApprovedBy, ref  isfetchcount);
        }
        public DataSet FMS_Get_ScheduleFileApprovalByFileID(int StatusID, int VesselID, int OfficeID, int FileID, int ApprovedBy, ref int isfetchcount)
        {
            return obj.FMS_Get_ScheduleFileApprovalByFileID(StatusID, VesselID, OfficeID, FileID, ApprovedBy, ref  isfetchcount);
        }
        public DataSet FMS_Get_FormType()
        {
            return obj.FMS_Get_FormType();
        }
        public DataSet FMS_Get_FormTypeList()
        {
            return obj.FMS_Get_FormTypeList();
        }
        public int FMS_Update_Document(int DocumentID, string FileName, int FormType, int Department, int UserID, string Remarks, DataTable dt)
        {
            return obj.FMS_Update_Document(DocumentID, FileName, FormType, Department, UserID, Remarks, dt);
        }
       /// <summary>
       /// This function is created by Pranav Sakpal on 28/07/2016
       /// This function will calls dal function to update file/form details
       /// </summary>
       /// <param name="DocumentID">Valid document ID </param>
       /// <param name="FileName">Valid file name </param>
       /// <param name="FormType">Valid Form type</param>
       /// <param name="Department">Valid department ID</param>
       /// <param name="UserID">Valid userID</param>
       /// <param name="Remarks">Valid Remarks</param>
       /// <param name="ParentID">Valid Parent folder ID</param>
       /// <param name="dt">datatable of RA forms</param>
       /// <returns></returns>
        public int FMS_Update_Document(int DocumentID, string FileName, int FormType, int Department, int UserID, string Remarks, int ParentID, DataTable dt)
        {
            return obj.FMS_Update_Document(DocumentID, FileName, FormType, Department, UserID, Remarks, ParentID, dt);
        }
        public int FMS_Update_DocumentSchedule(int ScheduleID, int ModifiedBy)
        {
            return obj.FMS_Update_DocumentSchedule(ScheduleID, ModifiedBy);
        }
        public static DataTable FMS_Get_ScheduleFileApprovalOverdue(int UserID)
        {
            return DAL_FMS_Document.FMS_Get_ScheduleFileApprovalOverdue(UserID);
        }
        public static DataTable FMS_Get_ScheduleFileReceivingOverdue(int UserID)
        {
            return DAL_FMS_Document.FMS_Get_ScheduleFileReceivingOverdue(UserID);
        }
        public DataSet FMS_Get_ScheduleStatusVoyageInfo(int StatusID, int OfficeID, int VesselID)
        {
            return obj.FMS_Get_ScheduleStatusVoyageInfo(StatusID, OfficeID, VesselID);
        }
        /// <summary>
        /// Update status after rework the file
        /// </summary>
        /// <param name="ModifiedBy">It is a user who are rework the file</param>
        /// <param name="StatusID">It is a File scheduled status id</param>
        /// <param name="OfficeID">It is a file scheduled office id</param>
        /// <param name="VesselID">It is a vessel id on which file are scheduled</param>
        /// <param name="Remark">It is a file scheduled status id</param>
        /// <param name="Version">It is a version of file</param>
        /// <param name="Level">It is a level on which approver are rework the file</param>
        /// <param name="File_ID">It is a Selected file id</param>
        /// <returns>return 1 for approve,2 for rework or else 0 </returns>
        public int FMS_Update_ScheduleStatusForRework(int ModifiedBy, int StatusID, int OfficeID, int VesselID, string Remark, int Version, int Level, int File_ID)
        {
            try
            {

                return obj.FMS_Update_ScheduleStatusForRework(ModifiedBy, StatusID, OfficeID, VesselID, Remark, Version, Level, File_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet FMS_Get_ScheduleApprovalHistory(int FileID)
        {
            return obj.FMS_Get_ScheduleApprovalHistory(FileID);
        }
        public DataSet FMS_Get_ApprovedScheduleStatus(int ApprovedBy, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string searchText, ref int isfetchcount)
        {
            return obj.FMS_Get_ApprovedScheduleStatus(ApprovedBy, sortby, sortdirection, pagenumber, pagesize, searchText, ref isfetchcount);
        }
        public DataSet FMS_Get_ScheduleFileApprovalByStatus(int StatusID, int OfficeID, int VesselID)
        {
            return obj.FMS_Get_ScheduleFileApprovalByStatus(StatusID, OfficeID, VesselID);
        }
        public DataSet FMS_Get_ApprovedSchedule(int StatusID, int OfficeID, int VesselID, int FileID)
        {
            return obj.FMS_Get_ApprovedSchedule(StatusID, OfficeID, VesselID, FileID);
        }
        public int FMS_Update_ScheduleStatus(int Status_ID, int OfficeID, int VesselID, int Modified_By)
        {
            return obj.FMS_Update_ScheduleStatus(Status_ID, OfficeID, VesselID, Modified_By);
        }
        /// <summary>
        /// Method is used to approve Form at Office side and insert record for next approvar level user.
        /// </summary>
        /// <param name="Status_ID">It is a File scheduled status id</param>
        /// <param name="OfficeID">It is a file scheduled office id</param>
        /// <param name="VesselID">It is a vessel id on which file are scheduled</param>
        /// <param name="File_ID">It is a Selected file id</param>
        /// <param name="Remark">It is a remark entered at the time of approved the file</param>
        /// <param name="Approved_By"> It is a user who are approving the file</param>
        /// <param name="CreatedBy"> It is a login user's ID </param>
        /// <param name="version">It is a version of file</param>
        /// <param name="Approval_Level">It is a level of approval on which approver are approved the file</param>
        /// <param name="Approval_Status">It is a status of approval</param>
        /// <returns>return 1 for approve,2 for rework or else 0 </returns>
        public int FMS_Insert_ScheduleFileApproval(int Status_ID, int OfficeID, int VesselID, int File_ID, string Remark, int Approved_By, int CreatedBy, int version, int Approval_Level, int Approval_Status)
        {
            try
            {
                return obj.FMS_Insert_ScheduleFileApproval(Status_ID, OfficeID, VesselID, File_ID, Remark, Approved_By, CreatedBy, version, Approval_Level, Approval_Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int FMS_Get_ScheduleFileApprovalExists(int StatusID, int ApprovalLevel)
        {
            return obj.FMS_Get_ScheduleFileApprovalExists(StatusID, ApprovalLevel);
        }

        public DataSet FMS_Get_ScheduleFileApproval(int? ApprovedBy, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string SearchText, ref int isfetchcount)
        {
            return obj.FMS_Get_ScheduleFileApproval(ApprovedBy, sortby, sortdirection, pagenumber, pagesize, SearchText, ref isfetchcount);
        }
        public int FMS_Insert_ApprovalLevels(int FileID, int CreatedBy)
        {
            return obj.FMS_Insert_ApprovalLevels(FileID, CreatedBy);
        }
        public int FMS_Delete_ApprovalLevel(int FileID, int DeletedBy, int ApprovalLevel)
        {
            return obj.FMS_Delete_ApprovalLevel(FileID, DeletedBy, ApprovalLevel);
        }
        public int FMS_Get_ApproverInLevel(int FileID, int ApprovalLevel)
        {
            return obj.FMS_Get_ApproverInLevel(FileID, ApprovalLevel);
        }
        public int FMS_Get_ApproverInLevel(int FileID, int ApprovalLevel, int? UserID)
        {
            return obj.FMS_Get_ApproverInLevel(FileID, ApprovalLevel, UserID);
        }
        public DataSet FMS_Get_VesselSchInfo(int FileID, int UserCompanyId, int? FleetID)
        {
            return obj.FMS_Get_VesselSchInfo(FileID, UserCompanyId, FleetID);
        }
        /// <summary>
        /// This function is edited by Pranav Sakpal on 23-06-2016 
        /// This function will call for dal method to update document assignments to vessel
        /// On performing assign and un-assign @Activestatus values will be changed and this will update active_Status of schedule table.
        /// There is one more parameter added @Activestatus.
        /// </summary>
        /// <param name="FileID">This is valid document ID</param>
        /// <param name="VesselID">Valid vesselID</param>
        /// <param name="CreatedBy">User id as creator</param>
        /// <param name="ActiveStatus">Active status for assignment 1 and un-assignment 0.</param>
        /// <returns>return 1 for approve,2 for rework or else 0 </returns>
        public int FMS_Insert_AssignFormToVessel(int FileID, int VesselID, int CreatedBy, int ActiveStatus)
        {
            return obj.FMS_Insert_AssignFormToVessel(FileID, VesselID, CreatedBy, ActiveStatus);//Added ActiveStatus parameter. 
        }
        public int FMS_Get_ApproverByLevel(int FileID, int ApprovalLevel)
        {
            return obj.FMS_Get_ApproverByLevel(FileID, ApprovalLevel);
        }
        public int FMS_Insert_FileApprovar(int FileID, int ApprovalLevel, int ApprovarID, int CreatedBy)
        {
            return obj.FMS_Insert_FileApprovar(FileID, ApprovalLevel, ApprovarID, CreatedBy);
        }
        public int FMS_Update_FileApprovar(int FileID, int ApprovalLevel)
        {
            return obj.FMS_Update_FileApprovar(FileID, ApprovalLevel);
        }
        public int FMS_Update_FileApprovarById(int FileID, int ApprovalLevel, int ApprovarID, int CreatedBy)
        {
            return obj.FMS_Update_FileApprovarById(FileID, ApprovalLevel, ApprovarID, CreatedBy);
        }
        public int FMS_Update_Folder(int FolderID, string FilePath, string FileName, string vURL)
        {
            return obj.FMS_Update_Folder(FolderID, FilePath, FileName, vURL);
        }
        public DataSet FMS_Get_UserListWithFilter(string SearchText)
        {
            return obj.FMS_Get_UserListWithFilter(SearchText);
        }
        public DataSet FMS_Get_UserList()
        {
            return obj.FMS_Get_UserList();
        }
        public DataSet FMS_Get_ApprovarByLevel(int FileID, int ApprovalLevel)
        {
            return obj.FMS_Get_ApprovarByLevel(FileID, ApprovalLevel);
        }
        public DataSet FMS_Get_FileApprovalLevel(int FileID, int Status_ID, int Office_ID, int Vessel_ID)
        {
            return obj.FMS_Get_FileApprovalLevel(FileID, Status_ID, Office_ID, Vessel_ID);
        }
        public DataSet FMS_Get_FileApprovalLevelStatus(int FileID, int Status_ID, int Office_ID, int Vessel_ID)
        {
            return obj.FMS_Get_FileApprovalLevelStatus(FileID, Status_ID, Office_ID, Vessel_ID);
        }
        public DataSet FMS_Get_FileApprovalLevel(int FileID)
        {
            return obj.FMS_Get_FileApprovalLevel(FileID);
        }
        public DataSet FMS_Get_FileApprovar(int FileID, int? Level)
        {
            return obj.FMS_Get_FileApprovar(FileID, Level);
        }
        public DataSet getFilpath()
        {
            return obj.getFilpath();
        }

        public DataTable getFileList(int UserID)
        {
            return obj.getFileList(UserID);
        }
        public DataTable getFolderList(int UserID)
        {
            return obj.getFolderList(UserID);
        }
        public DataSet getAllFiles(string FileName)
        {
            return obj.getAllFiles(FileName);
        }

        public DataSet getVesselList()
        {
            return obj.getVesselList();
        }

        public DataSet getSyncdataFromDB(string QueryText)
        {
            return obj.getSyncdataFromDB(QueryText);
        }

        public DataSet UpdateFMSLog(string FileName, string Status)
        {
            return obj.UpdateFMSLog(FileName, Status);
        }

        public int insertDataFMSRev(int VesselCode, string RevPath, string Rev_Filename, string LogDate, int CreatedBy)
        {
            return obj.insertDataFMSRev(VesselCode, RevPath, Rev_Filename, LogDate, CreatedBy);
        }

        public DataSet getLogInfo(string userID, string LFileID)
        {
            return obj.getLogInfo(userID, LFileID);
        }

        public DataSet getUserLog(string userID, string LFileID)
        {
            return obj.getUserLog(userID, LFileID);
        }

        public string getRootFolderName()
        {
            string RootFolder = "";

            try
            {
                DataSet ds = obj.getRootFolderName();

                if (ds.Tables[0].Rows.Count == 0)
                    RootFolder = "";

                else
                {
                    RootFolder = Convert.ToString(ds.Tables[0].Rows[0][0]);

                    string[] arr = RootFolder.Split('\\');
                    RootFolder = arr[2];
                }
            }
            catch (Exception ex)
            {
                // DLL.clsErrorMessageandLog.error(ex, false, 0);
                RootFolder = "";
            }

            return RootFolder;
        }

        public DataSet getLogViewDetails(int userID)
        {
            return obj.getLogViewDetails(userID);
        }

        //public DataSet getFMSUser(string userID)
        //{
        //    return obj.getFMSUser(userID);
        //}

        public void insertDataFMSLog(int UserID, string LOGManuals2, string LOGManuals3, string FileName, string LogDate)
        {
            obj.insertDataFMSLog(UserID, LOGManuals2, LOGManuals3, FileName, LogDate);
        }

        public DataSet getSearchResult(int userID, string toDate, string fromDate, string Vmanual)
        {
            return obj.getSearchResult(userID, toDate, fromDate, Vmanual);
        }

        public DataSet getDetailsgViewNotRead(int userID, string fromDate, string todate, string vMan)
        {
            return obj.getDetailsgViewNotRead(userID, todate, fromDate, vMan);
        }

        public DataSet getDetailsgViewReadAll(int userID, string fromDate, string todate, string ManualReadAll)
        {
            return obj.getDetailsgViewReadAll(userID, fromDate, todate, ManualReadAll);
        }

        public DataSet FillManuals()
        {
            return obj.FillManuals();
        }

        public DataSet FillUser(int userID)
        {
            return obj.FillUser(userID);
        }

        public DataSet FillDDUserByVessel(int vesselCode)
        {
            return obj.FillDDUserByVessel(vesselCode);
        }

        public DataSet FillDDUserForOffice()
        {
            return obj.FillDDUserForOffice();
        }

        public string Authentication(string username, string pasw)
        {
            return obj.Authentication(username, pasw);
        }

        public int getUserIDbyUsername(string userName)
        {
            return obj.getUserIDbyUsername(userName);
        }

        public DataSet getUsersDetailsByUserID(string userid)
        {
            return obj.getUsersDetailsByUserID(userid);
        }

        public void UpdatePwd(int username, string CurrentPwd, string NewPwd)
        {
            obj.UpdatePwd(username, CurrentPwd, NewPwd);

        }

        public void insertFileLogIntoDB(string ManualName, string FileName, string filePath, int UserID, string Remarks, int NodeType)
        {
            obj.insertFileLogIntoDB(ManualName, FileName, filePath, UserID, Remarks, NodeType);

        }

        public void UpdateVersionInfoOfNewFileAdd(int fileID, string GuidFileName, int UserID, string Remarks)
        {
            obj.UpdateVersionInfoOfNewFileAdd(fileID, GuidFileName, UserID, Remarks);

        }

        public int getFileCountByFileID(int FileID)
        {
            return obj.getFileCountByFileID(FileID);

        }

        public DataSet getFileVersion(int fileID)
        {
            return obj.getFileVersion(fileID);

        }

        public DataSet getCheckedFileInfo(int FileID)
        {
            return obj.getCheckedFileInfo(FileID);

        }

        public int getFileIDByPath(string FilePath)
        {
            return obj.getFileIDByPath(FilePath);

        }


        public int checkFileExits(string sFileName, string sFolderPath)
        {
            return obj.checkFileExits(sFileName, sFolderPath);

        }




        public int Get_UserAccess_OnFile(int FileID, int UserID)
        {
            return obj.Get_UserAccess_OnFile(FileID, UserID);

        }



        //public int getFileIDByFileName(string FileName)
        //{
        //    return obj.getFileIDByFileName(FileName);
        //}

        public void insertRecordAtCheckout(int userID, int fileID)
        {
            obj.insertRecordAtCheckout(userID, fileID);

        }

        public void insertRecordAtCheckIN(int fileID, string FileName, int UserID)
        {

            obj.insertRecordAtCheckIN(fileID, FileName, UserID);

        }

        public DataSet getLatestFileOperationByUserID(int fileID, int userID)
        {
            return obj.getLatestFileOperationByUserID(fileID, userID);

        }

        public DataSet fileInfoAtTreeBind(string FileName)
        {
            return obj.fileInfoAtTreeBind(FileName);

        }

        public DataSet getFileDetailsByID(int DocID, int VersionID)
        {
            return obj.getFileDetailsByID(DocID, VersionID);

        }

        /// <summary>
        /// Modified by Anjali DT:21-06-2016
        /// </summary>
        /// <param name="FileID">ID of Selected file.</param>
        /// <param name="showArchivedForms">If showArchivedForms = 0 then do not show archieved forms. || If showArchivedForms = 1 then do show archieved forms.</param>
        /// <returns>List of information related to selected file.</returns>
        public DataSet GetLastestFileInfoByID(int FileID, int showArchivedForms)
        {
            return obj.GetLastestFileInfoByID(FileID, showArchivedForms);

        }
        /// <summary>
        /// created by kavita DT:29-01-2016
        /// </summary>
        /// <param name="FileID">ID of Selected file.</param>
        /// <param name="showArchivedForms">If showArchivedForms = 0 then do not show archieved forms. || If showArchivedForms = 1 then do show archieved forms.</param>
        /// <param name="StatusID">It is a File scheduled status ID.</param>
        /// <param name="OfficeID">It is a file scheduled office ID</param>
        /// <param name="VesselID">It is a vessel id on which file are scheduled</param>
        /// <returns>List of information related to selected file.</returns>
        public DataSet GetLastestFileInfoByID(int FileID, int showArchivedForms, int StatusID, int OfficeID, int VesselID)
        {
            return obj.GetLastestFileInfoByID(FileID, showArchivedForms, StatusID, OfficeID, VesselID);

        }
        public int FMS_Get_FormScheduleStatus(int DocumentID)
        {
            return obj.FMS_Get_FormScheduleStatus(DocumentID);
        }
        public DataSet GetOprationDetailsByID(int FileID)
        {
            return obj.GetOprationDetailsByID(FileID);

        }

        public string getMachineNameByPath(string FolderPath)
        {
            return obj.getMachineNameByPath(FolderPath);
        }

        public int getMaxVersionFromParentTable(int fileID)
        {
            return obj.getMaxVersionFromParentTable(fileID);
        }

        public int insertRecordAtFolderCreation(string LogFileID, string LogManuals, string FolderPath, int NodeType)
        {
            return obj.insertRecordAtFolderCreation(LogFileID, LogManuals, FolderPath, NodeType);
        }
        public void Insert_Dept_Folder_Access(int FolderID, int DeptID, int UserID)
        {
            obj.Insert_Dept_Folder_Access_DL(FolderID, DeptID, UserID);
        }
        public void Insert_User_Folder_Access(int FolderID, int UserID, int CreatedBy)
        {
            obj.Insert_User_Folder_Access_DL(FolderID, UserID, CreatedBy);
        }

        public void Delete_Dept_Folder_Access(int FolderID, int DeptID)
        {
            obj.Delete_Dept_Folder_Access_DL(FolderID, DeptID);
        }

        public void Delete_User_Folder_Access(int FolderID, int UserID)
        {
            obj.Delete_User_Folder_Access_DL(FolderID, UserID);
        }

        public DataSet getFileIDByDocInfo(string DocumentName)
        {
            return obj.getFileIDByDocInfo(DocumentName);
        }
        public void AddNewUser(string FirstName, string LastName, string MiddleName, string userName, string password, string Email, int accessLevel)
        {
            obj.AddNewUser(FirstName, LastName, MiddleName, userName, password, Email, accessLevel);
        }

        public DataSet AccessLevel()
        {
            return obj.AccessLevel();

        }

        public DataSet UserDetails()
        {
            return obj.UserDetails();
        }

        public DataSet getUserDetailsByID(int userID)
        {
            return obj.getUserDetailsByID(userID);
        }

        public void UpdateUserInfo(int USERID, string FirstName, string LastName, string MiddleName, string userName, string password, string Email, int accessLevel)
        {
            obj.UpdateUserInfo(USERID, FirstName, LastName, MiddleName, userName, password, Email, accessLevel);

        }

        public void DeleteUserByID(int USERID)
        {
            obj.DeleteUserByID(USERID);

        }
        public void RenameFolder(string ExistingFolderPath, string NewDocName)
        {
            string NewFolderPath = ExistingFolderPath.Substring(0, ExistingFolderPath.LastIndexOf('/')) + "/" + NewDocName;

            obj.RenameFolder(ExistingFolderPath, NewFolderPath, NewDocName);

        }
        public void DeleteFolder(string ExistingFolderPath)
        {
            obj.DeleteFolder(ExistingFolderPath);

        }

        /// <summary>
        /// Modified by Anjali DT:21-06-2016 
        /// </summary>
        /// <param name="UserID">Id of Login user</param>
        /// <param name="dir">Directory id user clicked to expand.</param>
        /// <param name="PageLink"></param>
        /// <param name="showArchivedForms">If showArchivedForms = 0 then do not show archieved forms. || If showArchivedForms = 1 then do show archieved forms.</param>
        /// <returns>List of all forms under the clicked directory. </returns>
        public DataSet getFolderAsync(int UserID, int dir, string PageLink, int showArchivedForms)
        {
            return obj.getFolderAsync(UserID, dir, PageLink, showArchivedForms);
        }
        public DataSet FMS_Get_SchTree(int UserID, int dir, string PageLink)
        {
            return obj.FMS_Get_SchTree(UserID, dir, PageLink);
        }
        /// <summary>
        /// This function is creeated by Pranav Sakpal on 25-07-2016.
        /// This will simply fetch parent folder list from SP for folder tree
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="dir"></param>
        /// <param name="PageLink"></param>
        /// <returns>Returns all folders datatable on basis of assignments of folder</returns>
        public DataSet FMS_Get_ParentFolders_SchTree(int UserID, int dir, string PageLink)
        {
            return obj.FMS_Get_ParentFolders_SchTree(UserID, dir, PageLink);
        }

        /// <summary>
        /// This function is creeated by Pranav Sakpal on 25-07-2016.
        ///  This will simply fetch parent folder ID 
        /// </summary>
        /// <param name="DocID">Valid document id</param>
        /// <returns>returns details of document and ParentID in datatable.</returns>
        public DataSet FMS_Get_ParentFoldersID(int DocID)
        {
            return obj.FMS_Get_ParentFoldersID(DocID);
        }


        public int CreateNewFolder(string NewFolderName, int ParentFolderID, DataTable FolderAccessUserList, int UserID, string vURL)
        {
            return obj.CreateNewFolder(NewFolderName, ParentFolderID, FolderAccessUserList, UserID, vURL);
        }

        public int Add_NewDocument(int ParentFolderID, string FileName, string filePath, int UserID, string Remarks, int formType, int Department, string Format, DataTable dt)
        {
            return obj.Add_NewDocument_DL(ParentFolderID, FileName, filePath, UserID, Remarks, formType, Department, Format, dt);

        }

        public DataTable FMS_Files_Approval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return obj.FMS_Files_Approval_Search(searchtext, Approval_Status, Approved_By, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);
        }
        public DataTable FMS_Get_FileApprovalExists(int? FileID, int? Approver_ID, int? Approval_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            try
            {
                return obj.FMS_Get_FileApprovalExists(FileID, Approver_ID, Approval_Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

            }
            catch
            {
                throw;
            }

        }
        public DataTable GET_FolderUser(int FolderID)
        {
            return obj.GET_FolderUser(FolderID);
        }
        public DataTable FMS_Files_Approval_List(int ID)
        {
            return obj.FMS_Files_Approval_List(ID);
        }

        public int FMS_Files_Approved(int FMSID, int Approve_By, string Remark, int Created_by, int Version)
        {
            return obj.FMS_Files_Approved(FMSID, Approve_By, Remark, Created_by, Version);
        }
        public DataTable Get_FileName(string Path, string FileName)
        {
            try
            {
                return obj.Get_FileName(Path, FileName);
            }
            catch
            {
                throw;
            }
        }
        public DataTable getFileIDByFullPath(string FilePath)
        {
            return obj.getFileIDByFullPath(FilePath);

        }
        public void CreateNewFolderApproval(int FolderID, DataTable FolderApprovalLevel, int UserID)
        {

            try
            {
                obj.CreateNewFolderApproval(FolderID, FolderApprovalLevel, UserID);

            }
            catch
            {
                throw;
            }

        }
        public DataTable Check_FolderApprovalExists(int ParentFolderID)
        {
            try
            {
                return obj.Check_FolderApprovalExists(ParentFolderID);

            }
            catch
            {
                throw;
            }

        }
        public void CreateNewFileApproval(int FileID, string sFileName, int ParentFolderID, string iFolderName, DataTable dtApproval_Level, int User_ID, int Company)
        {

            try
            {
                obj.CreateNewFileApproval(FileID, sFileName, ParentFolderID, iFolderName, dtApproval_Level, User_ID, Company);

            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Method is used update form details after approved form by any approver.
        /// </summary>
        /// <param name="File_ID">Form id which is approved</param>
        /// <param name="Level_ID">Level id in which form is apporved</param>
        /// <param name="Remark">remark entered by approver</param>
        /// <returns>Return 1 or 0</returns>
        public int FMS_Update_FileApproval(int File_ID, int Level_ID, string Remark)
        {

            try
            {
                return obj.FMS_Update_FileApproval(File_ID, Level_ID, Remark);

            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Method is used to update form details after reject form by any approver.
        /// </summary>
        /// <param name="File_ID">Form id which is approved</param>
        /// <param name="Remark">remark entered by approver</param>
        /// <param name="Level_ID">Level id in which form is apporved</param>
        /// <param name="Version">Version of form which is reject</param>
        /// <param name="RejectedBy">User id of approver who is rejected form</param>
        /// <returns>Return 1 or 0</returns>
        public int FMS_Insert_FileRejection(int File_ID, string Remark, int Level_ID, int Version, int RejectedBy)
        {

            try
            {
                return obj.FMS_Insert_FileRejection(File_ID, Remark, Level_ID, Version, RejectedBy);

            }
            catch
            {
                throw;
            }

        }
        public DataTable GET_FolderApproverList(int FolderID)
        {
            try
            {
                return obj.GET_FolderApproverList(FolderID);
            }
            catch
            {
                throw;
            }
        }
        public void UpdateDMSApprovarList(int FolderID, DataTable FolderApprovalLevel, int UserID)
        {

            try
            {
                obj.UpdateDMSApprovarList(FolderID, FolderApprovalLevel, UserID);

            }
            catch
            {
                throw;
            }

        }
        public void Delete_DMSFile_Folder(int ID, int UserID)
        {

            try
            {
                obj.Delete_DMSFile_Folder(ID, UserID);

            }
            catch
            {
                throw;
            }

        }
        public DataSet Get_Schedule_Details(int DocumentID, int ScheduleID)
        {
            try
            {
                return obj.Get_Schedule_Details_DL(DocumentID, ScheduleID);
            }
            catch
            {
                throw;
            }
        }
        public int Save_Schedule(DataTable dtSchedule, DataTable dtSettings, int UserID, int DocumentID)
        {
            try
            {
                return obj.Save_Schedule_DL(dtSchedule, dtSettings, UserID, DocumentID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_DocScheduleList(int DocumentID, int? VesselID, int? FleetID, string Status, int PageIndex, int PageSize, ref int isfetchcount)
        {
            try
            {
                return obj.Get_DocScheduleList_DL(DocumentID, VesselID, FleetID, Status, PageIndex, PageSize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Added By Kavita :23-06-2016
        /// Overloaded function Get_DocScheduleList_DL
        /// </summary>
        /// <param name="DocumentID">Selected Form ID</param>
        /// <param name="VesselID">selected vessel ID</param>
        /// <param name="FleetID">selected Fleet ID</param>
        /// <param name="Status">Selected Status</param>
        /// <param name="PageIndex">value is 1</param>
        /// <param name="PageSize">value is 10</param>
        /// <param name="isfetchcount">No. of Rows</param>
        /// <returns>Forms Schedule details according to the selection parameters.</returns>
        public DataTable Get_DocScheduleList(int DocumentID, int? VesselID, int? FleetID, string Status, int PageIndex, int PageSize, ref int isfetchcount, DateTime? DateFrom, DateTime? DateTill)
        {
            try
            {
                return obj.Get_DocScheduleList_DL(DocumentID, VesselID, FleetID, Status, PageIndex, PageSize, ref isfetchcount, DateFrom, DateTill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetDocSchedule_Details(int ScheduleStatusID, int SchOfficeID, int SchVesselID)
        {
            try
            {
                return obj.GetDocSchedule_Details_DL(ScheduleStatusID, SchOfficeID, SchVesselID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_DocSchedule_Attachments(int ScheduleStatusID, int OfficeID, int VesselID)
        {
            try
            {
                return obj.Get_DocSchedule_Attachments_DL(ScheduleStatusID, OfficeID, VesselID);
            }
            catch
            {
                throw;
            }
        }
        public int Delete_DocSchedule_Attachments(int Code, int UserID, int OfficeID, int VesselID)
        {
            try
            {
                return obj.Delete_DocSchedule_Attachments_DL(Code, UserID, OfficeID, VesselID);
            }
            catch
            {
                throw;
            }
        }
        public int Insert_DocSchedule_Attachment(int ScheduleStatusID, string Attachment_Name, string Attachment_Path, int UserID, int SchOfficeID)
        {
            try
            {
                return obj.Insert_DocSchedule_Attachment_DL(ScheduleStatusID, Attachment_Name, Attachment_Path, UserID, SchOfficeID);
            }
            catch
            {
                throw;
            }
        }
        public int Insert_DocSchedule_Attachment(int ScheduleStatusID, string Attachment_Name, string Attachment_Path, int UserID, int SchOfficeID, int Vessel_ID)
        {
            try
            {
                return obj.Insert_DocSchedule_Attachment_DL(ScheduleStatusID, Attachment_Name, Attachment_Path, UserID, SchOfficeID, Vessel_ID);
            }
            catch
            {
                throw;
            }
        }



        public DataTable Insert_DocSchedule_Remark(string Remark, int ScheduleStatusID, int UserID, int SchOfficeID, int SchVesselID)
        {
            try
            {
                return obj.Insert_DocSchedule_Remark_DL(Remark, ScheduleStatusID, UserID, SchOfficeID, SchVesselID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_VesselDocHistroy(int DocumentID, int VesselID, int StatusID, int OfficeID)
        {
            try
            {
                return obj.Get_VesselDocHistroy_DL(DocumentID, VesselID, StatusID, OfficeID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_WorkCategoryList(string Category_Name)
        {
            try
            {
                return obj.Get_WorkCategoryList_DL(Category_Name);

            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RAFormsByDocID(int fileID)
        {
            try
            {
                return obj.Get_RAFormsByDocID_DL(fileID);
            }
            catch (Exception)
            {
                throw;
            }


        }
        public DataTable Get_RAFormsBy_ScheduleID(int scheduleID, int vesselID)
        {
            try
            {
                return obj.Get_RAFormsBy_ScheduleID_DL(scheduleID, vesselID);
            }
            catch (Exception)
            {
                throw;
            }


        }
        public DataSet FMS_Get_FormReceived_ByDate(string Period, string Mode, int? IsApproved, int? ApproverID, int? VesselAssignUserID)
        {
            try
            {
                return obj.FMS_Get_FormReceived_ByDate_DL(Period, Mode, IsApproved, ApproverID, VesselAssignUserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Added by Kavita :22-06-2016
        /// Overloaded function FMS_Get_FormReceived_ByDate
        /// function is use to call function that return form Details according to the selection.
        /// </summary>
        /// <param name="Period">Selected No. of Days</param>
        /// <param name="Mode">Mode of filter.Received/Due,Overdue/Pending for Approval</param>
        /// <param name="IsApproved">form is approve or not.1 OR 0 value</param>
        /// <param name="ApproverID">Login User ID </param>
        /// <param name="VesselAssignUserID">Login User ID For Vessel Assignment Filter</param>
        /// <param name="FleetID">Selected Fleet</param>
        /// <param name="VesselID">Selectde Vessel</param>
        /// <param name="StatusID">Selectde Status</param>
        /// <param name="DepartmentID">Selected Department</param>
        /// <param name="SearchBy">Form name entered in Search control</param>
        /// <param name="FormType">Selected Form Type</param>
        /// <returns>Returns Form Details according to the selection parameters</returns>
        public DataSet FMS_Get_FormReceived_ByDate(string Period, string Mode, int? IsApproved, int? ApproverID, int? VesselAssignUserID, int? FleetID, DataTable dtVessel, int? StatusID, int? DepartmentID, string SearchBy, int? FormType)
        {
            try
            {
                return obj.FMS_Get_FormReceived_ByDate_DL(Period, Mode, IsApproved, ApproverID, VesselAssignUserID, FleetID, dtVessel, StatusID, DepartmentID, SearchBy,FormType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Added by Kavita : 22-06-2016
        /// function use to call method for binding status dropdwoun.
        /// </summary>
        /// <returns>returns status</returns>
        public DataTable FMS_Get_StatusList()
        {
            try
            {
                return obj.FMS_Get_StatusList_DL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Added by Anjali DT:22-06-2016
        /// To unarchive forms.
        /// </summary>
        /// <param name="_documentID">Selected archived document ID.</param>
        /// <param name="_userID">Id of Logged in user.</param>
        public void UnArchiveForms(int _documentID, int _userID)
        {
            try
            {
                obj.UnArchiveForms(_documentID, _userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Added by Anjali DT:24-06-2016
        /// To save mail id.
        /// </summary>
        /// <param name="_mailID"> Mail id where mail is to be sent.</param>
        /// <param name="_userID">Id of user who created /modified mail id</param>
        public void SaveMailID(string _mailID, int _userID)
        {
            try
            {
                obj.SaveMailID(_mailID, _userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Added by Anjali DT:24-06-2016.||To retrive saved Mail id.
        /// </summary>
        public string GetMailID()
        {
            try
            {
                return obj.GetMailID();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Added by anjali DT:24-06-2016.To save configuration settings of  , attchment of selected form is forward via mail or not.
        /// </summary>
        /// <param name="_documentID"> Id of selected document</param>
        /// <param name="_userID">id of logged in user.</param>
        /// <param name="_forwardAttchment"> if _forwardAttchment = 1 then forward attchment else not. </param>
        public void SaveForwardAttchmentToForms(int _documentID, int _userID, int _forwardAttchment)
        {
            try
            {
                obj.SaveForwardAttchmentToForms(_documentID, _userID, _forwardAttchment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable  FMS_Get_Reschedule_FormInfo(int Status_ID)
        {
            try
            {
               return  obj.FMS_Get_Reschedule_FormInfo(Status_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable FMS_Get_Exits_Folder(int DocumentID, int ParenID)
        {
            try
            {
                {
                    return obj.FMS_Get_Exits_Folder(DocumentID, ParenID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        
    }
}
