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

using SMS.Data.QMS;

/// <summary>
/// Summary description for PortageBillBAL
/// </summary>
/// 

namespace SMS.Business.QMS
{
    public class BLL_QMS_Document
    {
        DAL_QMS_Document obj = new DAL_QMS_Document();

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
        
        public DataSet UpdateQmsLog(string FileName, string Status)
        {
           return obj.UpdateQmsLog(FileName, Status);
        }

        public int insertDataQmsRev(int VesselCode,string RevPath, string Rev_Filename, string LogDate,int CreatedBy)
        {
            return obj.insertDataQmsRev(VesselCode,RevPath, Rev_Filename, LogDate, CreatedBy);
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

        //public DataSet getQMSUser(string userID)
        //{
        //    return obj.getQMSUser(userID);
        //}

        public void insertDataQmsLog(int UserID, string LOGManuals2, string LOGManuals3, string FileName, string LogDate)
        {
            obj.insertDataQmsLog(UserID, LOGManuals2, LOGManuals3, FileName, LogDate);
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
            return obj.Authentication(username,pasw);
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

        public void insertFileLogIntoDB(string ManualName, string FileName, string filePath,int UserID,string Remarks,int NodeType)
        {
            obj.insertFileLogIntoDB(ManualName, FileName, filePath, UserID, Remarks,NodeType);
          
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


        public int checkFileExits(string sFileName , string sFolderPath)
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

        public void insertRecordAtCheckIN(int fileID, string FileName, int UserID,long Size)
        {

            obj.insertRecordAtCheckIN(fileID, FileName, UserID, Size);

        }

        public DataSet getLatestFileOperationByUserID(int fileID, int userID)
        {
          return   obj.getLatestFileOperationByUserID(fileID, userID);

        }

        public DataSet fileInfoAtTreeBind(string FileName)
        {
            return obj.fileInfoAtTreeBind(FileName);

        }

        public DataSet getFileDetailsByID(int DocID, int VersionID)
        {
            return obj.getFileDetailsByID(DocID, VersionID);

        }

        public DataSet GetLastestFileInfoByID(int FileID)
        {
            return obj.GetLastestFileInfoByID(FileID);

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

        public void Delete_Dept_Folder_Access(int FolderID,int DeptID)
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
        public void  AddNewUser(string FirstName,string LastName,string MiddleName,string userName,string password,string Email, int accessLevel)
        {
            obj.AddNewUser(FirstName, LastName, MiddleName,userName, password, Email, accessLevel);
        }

        public DataSet AccessLevel()
        {
          return  obj.AccessLevel();

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
        public void RenameFolder(string ExistingFolderPath,string NewDocName)
        {
            string NewFolderPath = ExistingFolderPath.Substring(0, ExistingFolderPath.LastIndexOf('/')) + "/" + NewDocName;

            obj.RenameFolder(ExistingFolderPath, NewFolderPath, NewDocName);

        }
        /// <summary>
        /// Function to rename the folder 
        /// </summary>
        /// <param name="ExistingFolderPath">Current Folder path which is to be rename</param>
        /// <param name="NewDocName">New folder name</param>
        /// <param name="FolderID">ID of folder</param>
        public void RenameFolder(string ExistingFolderPath, string NewDocName,int FolderID)
        {
            string NewFolderPath = ExistingFolderPath.Substring(0, ExistingFolderPath.LastIndexOf('/')) + "/" + NewDocName;

            obj.RenameFolder(ExistingFolderPath, NewFolderPath, NewDocName, FolderID);

        }
        public void DeleteFolder(string ExistingFolderPath)
        {
            obj.DeleteFolder(ExistingFolderPath);

        }

        public DataSet getFolderAsync(int UserID, int dir, string PageLink)
        {
            return obj.getFolderAsync(UserID, dir, PageLink);
        }

        public int CreateNewFolder(string NewFolderName, int ParentFolderID, DataTable FolderAccessUserList, int UserID)
        {
            return obj.CreateNewFolder(NewFolderName, ParentFolderID, FolderAccessUserList, UserID);
        }

        public int Add_NewDocument(int ParentFolderID, string FileName, string filePath, int UserID, string Remarks,long Size)
        {
            return obj.Add_NewDocument_DL(ParentFolderID, FileName, filePath, UserID, Remarks,Size);

        }

        public DataTable QMS_Files_Approval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return obj.QMS_Files_Approval_Search(searchtext, Approval_Status, Approved_By, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);
        }
        public DataTable QMS_Check_FileApprovalExists(int? FileID, int? Approver_ID, int? Approval_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            try
            {
                return obj.QMS_Check_FileApprovalExists(FileID, Approver_ID, Approval_Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

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
        public DataTable QMS_Files_Approval_List(int ID)
        {
            return obj.QMS_Files_Approval_List(ID);
        }

        public int QMS_Files_Approved(int QMSID, int Approve_By, string Remark, int Created_by,int Version)
        {
            return obj.QMS_Files_Approved(QMSID, Approve_By, Remark, Created_by,Version);
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
        public void CreateNewFileApproval(int FileID, string sFileName, int ParentFolderID, string iFolderName, DataTable dtApproval_Level, int User_ID,int Company)
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

        public void QMS_Update_FileApproval(int File_ID, int Level_ID)
        {

            try
            {
                obj.QMS_Update_FileApproval(File_ID, Level_ID);

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
        public  DataSet getDocumentReadListbyDocument_ID(int DocID)
        {
            try
            {
                return obj.getDocumentReadListbyDocument_ID_DL(DocID);
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        public DataTable QMS_SP_Files_SyncApproval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize,int DownloadRequired, ref int isfetchcount)
        {
            return obj.QMS_SP_Files_SyncApproval_Search(searchtext, Approval_Status, Approved_By, sortby, sortdirection, pagenumber, pagesize, DownloadRequired,ref  isfetchcount);
        }
        public DataTable QMS_SP_Files_SyncApproval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int DownloadRequired,DataTable SizeRange, ref int isfetchcount)
        {
            return obj.QMS_SP_Files_SyncApproval_Search(searchtext, Approval_Status, Approved_By, sortby, sortdirection, pagenumber, pagesize, DownloadRequired, SizeRange,ref  isfetchcount);
        }
        public int QMS_SP_File_Sync(int QMSID, int Approve_By, string Remark, int Created_by, int Version)
        {
            return obj.QMS_SP_File_Sync(QMSID, Approve_By, Remark, Created_by, Version);
        }
    }

}