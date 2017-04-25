using System;
using System.Data;
using System.Configuration;
 
using SMS.Data;
using System.Data.SqlClient;
using System.Globalization;
/// <summary>
/// Summary description for QMSLogNamespace
/// </summary>
/// 

namespace SMS.Data.QMS
{
    public class DAL_QMS_Document
    {
        private string _messagename;	//Error or success message string
        private long _messagecode=0;	//Error code or success code value (0 for success)
        private string _constring;		//Connection String
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataSet getRootFolderName()
        {
            string sqlQueryView = "Select Top(1) FilePath from QMSdtlsFile_Log order by LogFileID,FilePath ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);

        }
        public DataTable GET_FolderUser(int FolderID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@FolderID",FolderID),
                                        };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_Get_FolderUser", sqlprm);
            return ds.Tables[0];

        }
        public void QMS_Update_FileApproval(int File_ID, int Level_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@File_ID",File_ID),
                                            new SqlParameter("@Level_ID",Level_ID),
                                            
                                        };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_Update_FileApproval", sqlprm);
        }
        public void CreateNewFolderApproval(int FolderID, DataTable FolderApprovalLevel, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@FolderID",FolderID),
                                            new SqlParameter("@FolderApprovalLevel",FolderApprovalLevel),
                                            new SqlParameter("@UserID",UserID),
                                            
                                        };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_CreateNewFolderApproval", sqlprm);

        }
        public DataTable Check_FolderApprovalExists(int ParentFolderID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@ParentFolderID",ParentFolderID),
                                        };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_Check_FolderApprovalExists", sqlprm);
            return ds.Tables[0];

        }
        public void CreateNewFileApproval(int FileID, string sFileName, int ParentFolderID, string iFolderName, DataTable dtApproval_Level, int User_ID,int CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@File_ID",FileID),
                                            new SqlParameter("@File_Name",sFileName),
                                            new SqlParameter("@Parent_ID",ParentFolderID),
                                            new SqlParameter("@Parent_Name",iFolderName),
                                            new SqlParameter("@FileApprovalLevel",dtApproval_Level),
                                             new SqlParameter("@User_ID",User_ID),
                                             new SqlParameter("@CompanyID",CompanyID)
                                        };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_CreateNewFileApproval", sqlprm);

        }
        public DataSet getLogInfo(string userID, string LFileID)
        {

            string sqlQuery = " select top 1(logdate),UserID,LogFileID,LOGManuals1, LOGManuals2,LOGManuals3 from ";
            sqlQuery += " QMSdtls_Log where UserID=@UserID AND LogFileID=@LogFileID order by logdate desc ";

            SqlParameter[] objParams = new SqlParameter[]
             {
                 new SqlParameter("@UserID", SqlDbType.VarChar),
                 new SqlParameter("@LogFileID", SqlDbType.VarChar),
                };
            objParams[0].Value = userID;
            objParams[1].Value = LFileID;

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery, objParams);
        }

        public DataSet getVesselList()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[SP_QMS_getVesselList]");

        }

        public DataSet getSyncdataFromDB(string QueryText)
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, QueryText);

        }

        public DataSet UpdateQmsLog(string FileName, string Status)
        {


            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@LogFileID",SqlDbType.VarChar), 
                new SqlParameter("@Status",SqlDbType.VarChar), 
            };

            obj[0].Value = FileName;
            obj[1].Value = Status;

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[SP_QMS_RevUpdate]", obj);

        }

        public DataSet getAllFiles(string FileNameID)
        {


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "SELECT     FilePath FROM         QMSdtlsFile_Log WHERE     (LOGManual1 = '" + FileNameID + "')");

        }

        public int insertDataQmsRev(int VesselCode, string RevPath, string Rev_Filename, string LogDate, int CreatedBy)
        {

            
            DateTime LogDateText = DateTime.Parse(LogDate, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            SqlParameter[] obj = new SqlParameter[]
                {    
                new SqlParameter("@VesselCode",SqlDbType.Int),
                new SqlParameter("@REV_filepath",SqlDbType.VarChar), 
                new SqlParameter("@REV_fileName",SqlDbType.VarChar), 
                new SqlParameter("@REV_DATE",SqlDbType.DateTime),
                new SqlParameter("@CREATED_BY",SqlDbType.Int),
                 new SqlParameter("@ID",SqlDbType.Int)
                };

            obj[0].Value = VesselCode;
            obj[1].Value = RevPath;
            obj[2].Value = Rev_Filename;
            obj[3].Value = LogDateText;
            obj[4].Value = CreatedBy;
            obj[5].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "[SP_QMS_RevInsert]", obj);
            return Convert.ToInt32(obj[5].Value);

        }

        public DataSet getFilpath()
        {
            string sqlQueryView = "Select FilePath,id,NodeType,LogFileID from QMSdtlsFile_Log order by id ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);
        }

        public DataTable getFileList(int UserID)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@UserID",UserID)
                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileList", obj).Tables[0];
        }
        public DataTable getFolderList(int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@UserID",UserID)                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FolderList", obj).Tables[0];
        }
        public DataSet getUserLog(string userID, string LFileID)
        {
            string sqlQuery = " select top 1(logdate),UserID,LogFileID,LOGManuals1, LOGManuals2,LOGManuals3 from ";
            sqlQuery += " QMSdtls_Log where UserID='" + userID + "' AND LogFileID='" + LFileID + "' order by ID Desc ,logdate desc ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        public DataSet getLogViewDetails(int userID)
        {
            string sqlQueryView = "Select logfileid,CONVERT(VARCHAR(8), logdate, 5) AS logdate,LOGManuals2 as logmanuals1,LOGManuals2 as logmanuals2  from";
            sqlQueryView += " QMSdtls_Log where logfileid IN(Select LogFileID from QMSdtlsFile_Log ) AND userid='" + userID + "' order by  LogDate DESC";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);
        }

        public DataSet getDetailsgViewNotRead(int userID, string fromdate, string toDate, string vMan)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@ID",SqlDbType.Int), 
                new SqlParameter("@FDate",SqlDbType.VarChar), 
                new SqlParameter("@TDate",SqlDbType.VarChar),
                new SqlParameter("@Manual",SqlDbType.VarChar)
                
            };

            obj[0].Value = userID;
            obj[1].Value = fromdate;
            obj[2].Value = toDate;
            obj[3].Value = vMan;


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[SP_QMSLog_ViewNotRead]", obj);
        }

        public DataSet getSearchResult(int userID, string todate, string fromDate, string Vmanual)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@ID",SqlDbType.Int), 
                new SqlParameter("@FDate",SqlDbType.VarChar), 
                new SqlParameter("@TDate",SqlDbType.VarChar),
                new SqlParameter("@Manual",SqlDbType.VarChar)
                
            };

            obj[0].Value = userID;
            obj[1].Value = todate;
            obj[2].Value = fromDate;
            obj[3].Value = Vmanual;


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_QMS_View_Read", obj);


        }

        public DataSet getDetailsgViewReadAll(int userID, string fromdate, string toDate, string ManualReadAll)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@ID",SqlDbType.Int), 
                new SqlParameter("@TDate",SqlDbType.VarChar), 
                new SqlParameter("@FDate",SqlDbType.VarChar), 
                new SqlParameter("@vManual",SqlDbType.VarChar) 

                
            };


            obj[0].Value = userID;
            obj[1].Value = toDate;
            obj[2].Value = fromdate;
            obj[3].Value = ManualReadAll;


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_QMS_File_ReadAll", obj);
        }

        public void insertDataQmsLog(int UserID, string LOGManuals2, string LOGManuals3, string FileName, string LogDate)
        {
            
            DateTime LogDateText = DateTime.Parse(LogDate, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@UserID",SqlDbType.Int), 
                new SqlParameter("@FileName",SqlDbType.VarChar,200), 
                new SqlParameter("@LOGManuals2",SqlDbType.VarChar,500), 
                new SqlParameter("@LOGManuals3",SqlDbType.VarChar,500), 
                new SqlParameter("@LogDate",SqlDbType.DateTime) 
            };

            obj[0].Value = UserID;
            obj[1].Value = FileName;
            //obj[2].Value = M1;
            obj[2].Value = LOGManuals2;
            obj[3].Value = LOGManuals3;
            obj[4].Value = LogDateText;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_QMSLog_InsertData", obj);
        }

        public DataSet FillManuals()
        {
            string sqlmanual = "Select distinct logmanual1 as logmanuals1 from QMSdtlsFile_Log order by Logmanual1";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlmanual);
        }

        public DataSet FillUser(int userID)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@userID",SqlDbType.Int)
            };

            obj[0].Value = userID;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_QMSLog_UserLoad", obj);

        }

        public DataSet FillDDUserByVessel(int vesselCode)
        {
            string sqlQueryView = "SELECT    CRW_Lib_Crew_Details.ID as userid, CRW_Lib_Crew_Details.Staff_Name + ' ' + CRW_Lib_Crew_Details.Staff_Midname + ' ' + CRW_Lib_Crew_Details.Staff_Surname AS User_name ";
            sqlQueryView += "FROM         CRW_Lib_Crew_Details INNER JOIN ";
            sqlQueryView += "                     CRW_Dtl_Crew_Voyages ON CRW_Lib_Crew_Details.Staff_Code = CRW_Dtl_Crew_Voyages.Staff_Code ";
            sqlQueryView += "WHERE     (CRW_Dtl_Crew_Voyages.Vessel_Code = " + vesselCode + ") ";
            sqlQueryView += "ORDER BY User_name";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);
        }

        public DataSet FillDDUserForOffice()
        {
            string sqlQueryView = "select User_name,UserID from Lib_User where Active_Status=1  order by User_name";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);
        }

        public string MessageName
        {
            get { return this._messagename; }
            set { this._messagename = value; }
        }

        public long MessageCode
        {
            get { return this._messagecode; }
        }

        public string ConnectionString
        {
            get { return this._constring; }
            set { this._constring = value; }
        }

        internal void insertDataQmsLog(string userID, string path, string fileName, DateTime date1)
        {
            throw new NotImplementedException();
        }

        public string Authentication(string username, string pasw)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@username",username),
                                           new SqlParameter("@PASSWORD",pasw)
                                       };
            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "SP_QMS_Authentication", sqlprm).ToString();
        }

        public int getUserIDbyUsername(string userName)
        {
            SqlParameter[] obj = new SqlParameter[]
        {
            new SqlParameter ("@UserName",SqlDbType.VarChar,200),
            new SqlParameter("@UserID",SqlDbType.Int)
         };

            obj[0].Value = userName;
            obj[1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_QMS_getUserIDbyUsername", obj);
            return Convert.ToInt32(obj[1].Value);
        }

        public DataSet getUsersDetailsByUserID(string userid)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@userid",userid)
                                       };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_QMS_UserDetailsbyId", sqlprm);
        }

        public void UpdatePwd(int username, string CurrentPwd, string NewPwd)
        {
            //string paswrd = DES_Encrypt_Decrypt.Encrypt(PWd);
            //string Npaswrd = DES_Encrypt_Decrypt.Encrypt(NewPwd);
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@username",username),
                                          new SqlParameter("@CurrentPwd",CurrentPwd),
                                          new SqlParameter("@NewPwd",NewPwd)
                                        };
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "SP_QMS_ChangePassword", sqlprm);
        }

        public void insertFileLogIntoDB(string ManualName, string FileName, string filePath, int UserID, string Remarks, int NodeType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@ManualName",ManualName),
                                          new SqlParameter("@FileName",FileName),
                                          new SqlParameter("@filePath",filePath),
                                          new SqlParameter("@UserID",UserID),
                                           new SqlParameter("@Remarks",Remarks),
                                             new SqlParameter("@NodeType",NodeType)
                                        };
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_FileLogIntoDB", sqlprm);
        }

        public int getFileCountByFileID(int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileID",SqlDbType.Int),
                                           new SqlParameter("@FileCount",SqlDbType.Int)
                                        };

            sqlprm[0].Value = FileID;
            sqlprm[1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileCountByFileID", sqlprm);
            return Convert.ToInt32(sqlprm[1].Value);
        }

        public void UpdateVersionInfoOfNewFileAdd(int fileID, string GuidFileName, int UserID, string Remarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@fileID",fileID),
                                          new SqlParameter("@GuidFileName",GuidFileName),
                                          new SqlParameter("@UserID",UserID),
                                           new SqlParameter("@Remarks",Remarks)
                                        };
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "QMS_SP_UpdateVersionInfoOfNewFileAdd", sqlprm);
        }

        public DataSet getFileVersion(int fileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileID",SqlDbType.Int)
                                        };

            sqlprm[0].Value = fileID;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileVersion", sqlprm);
        }

        public DataSet getCheckedFileInfo(int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileID",SqlDbType.Int)
                                        };

            sqlprm[0].Value = FileID;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_CheckedFileInfo", sqlprm);
        }

        public int getFileIDByPath(string FilePath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FilePath",FilePath),
                                          new SqlParameter("@FileID",SqlDbType.Int)
                                        };

            sqlprm[1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileIDByPath", sqlprm);
            return Convert.ToInt32(sqlprm[1].Value);
            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileIDByPath", sqlprm).Tables[0];
        }

      
        public int  checkFileExits(string sFileName, string sFolderPath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileName",sFileName),
                                          new SqlParameter("@FolderPath",sFolderPath),
                                          new SqlParameter("@DocID",SqlDbType.Int)
                                        };
            
            sqlprm[2].Direction = ParameterDirection.ReturnValue;
            
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Check_FileExists", sqlprm);
            return Convert.ToInt32(sqlprm[2].Value);
        }


       


        public int getFileIDByFileName(string FileName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileName",FileName),
                                          new SqlParameter("@FileID",SqlDbType.Int)
                                        };
            
            sqlprm[1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileIDByFileName", sqlprm);
            return Convert.ToInt32(sqlprm[1].Value);
        }
        public int Get_UserAccess_OnFile(int FileID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {                                          
                                          new SqlParameter("@FileID",FileID),
                                          new SqlParameter("@UserID",UserID),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_UserAccess_OnFile", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public void insertRecordAtCheckout(int userID, int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@userID",SqlDbType.Int),
                                          new SqlParameter("@FileID",SqlDbType.Int)
                                        };

            sqlprm[0].Value = userID;
            sqlprm[1].Value = FileID;
            SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_RecordAtCheckout", sqlprm);

        }

        public void insertRecordAtCheckIN(int FileID, string FileName, int UserID, long Size)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@FileID",SqlDbType.Int),
                                            new SqlParameter("@FileName",SqlDbType.Text),
                                            new SqlParameter("@UserID",SqlDbType.Int),
                                            new SqlParameter("@Size",SqlDbType.Int)

                                        };

            sqlprm[0].Value = FileID;
            sqlprm[1].Value = FileName;
            sqlprm[2].Value = UserID;
            sqlprm[3].Value = Size;
            SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_RecordAtCheckIN", sqlprm);

        }

        public DataSet getLatestFileOperationByUserID(int FileID, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileID",SqlDbType.Int),
                                           new SqlParameter("@userID",SqlDbType.Int)

                                        };

            sqlprm[0].Value = FileID;
            sqlprm[1].Value = UserId;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_LatestFileOperationByUserID", sqlprm);

        }

        public DataSet fileInfoAtTreeBind(string FileName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FileName",SqlDbType.VarChar,1000)

                                        };

            sqlprm[0].Value = FileName;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_QMS_fileInfoAtTreeBind", sqlprm);

        }

        public DataSet getFileDetailsByID(int DocID,int versionid)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@DocID",SqlDbType.Int),
                                           new SqlParameter("@VerNo",SqlDbType.Int)
                                        };

            sqlprm[0].Value = DocID;
            sqlprm[1].Value = versionid;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_GET_FileDetails", sqlprm);

//            string sqlQuery = @"SELECT     QMSdtlsFile_Log.ID, QMSdtlsFile_Log.LogFileID, QMSdtlsFile_Log.FilePath, QMSdtlsFile_Log.Version, QMSdtlsFile_Log.Date_Of_Creatation, 
//                                  lastOpp.Operation_Date, lastOpp.Operation_Type, lib_user.First_Name , lib_user.Last_Name, 
//                                  (lib_user.First_Name + ' ' + lib_user.Last_Name) as UserName, lib_user.UserID, QMSdtlsFile_Log.Created_By, CreatedUserInfo.First_Name AS CreatedBYFirstName
//            FROM         lib_user AS CreatedUserInfo RIGHT OUTER  JOIN
//                                  QMSdtlsFile_Log ON CreatedUserInfo.UserID = QMSdtlsFile_Log.Created_By LEFT OUTER JOIN
//                                  lib_user INNER JOIN
//                                      (SELECT     FileID, Operation_Date, Operation_Type, User_ID
//                                        FROM          QMS_FileOperationInfo
//                                        WHERE      (ID IN
//                                                                   (SELECT     MAX(ID) AS id
//                                                                     FROM          QMS_FileOperationInfo AS QMS_FileOperationInfo_1
//                                                                     GROUP BY FileID))) AS lastOpp ON lib_user.UserID = lastOpp.User_ID ON QMSdtlsFile_Log.ID = lastOpp.FileID
//            WHERE     QMSdtlsFile_Log.ID  = " + DocID;
//            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        public DataSet GetLastestFileInfoByID(int FileID)
        {
            string sqlQuery = @"SELECT     QMSdtlsFile_Log.ID, QMSdtlsFile_Log.LogFileID, 
                                        QMSdtlsFile_Log.Date_Of_Creatation,
                                        tblCreatedBy.First_Name + ' ' + tblCreatedBy.Last_Name AS  Created_User,
                                        LastOpp.Operation_Date, LastOpp.Operation_Type, 
                                        lib_user.First_Name + ' ' + lib_user.Last_Name as Opp_User			
                            FROM         lib_user AS tblCreatedBy RIGHT OUTER JOIN
                                                  QMSdtlsFile_Log ON tblCreatedBy.UserID = QMSdtlsFile_Log.Created_By LEFT OUTER JOIN
                                                      (SELECT     ID, FileID, Operation_Date, User_ID, Operation_Type
                                                        FROM          QMS_FileOperationInfo AS QMS_FileOperationInfo_3
                                                        WHERE      (ID =
                                                                                   (SELECT     MAX(ID) AS ID
                                                                                     FROM          QMS_FileOperationInfo AS QMS_FileOperationInfo_2
                                                                                     WHERE      (FileID = " + FileID + ")))) AS LastOpp INNER JOIN";
            sqlQuery += " lib_user ON LastOpp.User_ID = lib_user.UserID ON QMSdtlsFile_Log.ID = LastOpp.FileID";
            sqlQuery += "    WHERE     (QMSdtlsFile_Log.ID = " + FileID + ")";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        public DataSet GetOprationDetailsByID(int FileID)
        {
            string sqlQuery = @"(SELECT     QMS_FileOperationInfo.ID, QMS_FileOperationInfo.User_ID, QMS_FileOperationInfo.Operation_Type, QMS_FileOperationInfo.Operation_Date, 
                      lib_user.First_Name AS First_Name, lib_user.Last_Name, (lib_user.First_Name + ' ' + lib_user.Last_Name) as UserName, QMSdtlsFile_Log.LogFileID, 
                      QMS_FileOperationInfo.Version, QMSdtlsFile_Log.ID AS DocID
                FROM         QMS_FileOperationInfo INNER JOIN
                                      QMSdtlsFile_Log ON QMS_FileOperationInfo.FileID = QMSdtlsFile_Log.ID INNER JOIN
                                      lib_user ON QMS_FileOperationInfo.User_ID = lib_user.UserID
                WHERE     (QMSdtlsFile_Log.ID = " + FileID + @" ) ) UNION
             ( SELECT QMS_File_Approval.ID ,Approve_By User_ID,case when ApprovalStatus!=0 then 'APPROVED' else 'PENDING APPROVAL' end Operation_Type ,
                cast(Date_Of_Approval as datetime) Operation_Date, LIB_USER.First_Name AS First_Name, 
                      LIB_USER.Last_Name, (LIB_USER.First_Name + ' ' + LIB_USER.Last_Name) as UserName, LogFileID
                ,QMSdtlsFile_Log.Version, QMSdtlsFile_Log.ID AS DocID FROM QMS_File_Approval 
                INNER JOIN QMSdtlsFile_Log ON QMS_File_Approval.QMSID = QMSdtlsFile_Log.ID INNER JOIN
                                   LIB_USER on QMS_File_Approval.Approve_By=LIB_USER.UserID where QMS_File_Approval.Active_Status=1 AND QMS_File_Approval.QMSID=" + FileID + ") ORDER BY Operation_Date ";

                                                  
                                                  //") ORDER BY QMS_FileOperationInfo.ID DESC";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        public string getMachineNameByPath(string FolderPath)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "select distinct FilePath from QMSdtlsFile_Log where FilePath like  '%" + FolderPath + "%' "));
        }

        public int getMaxVersionFromParentTable(int fileID)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "select Version from QMSdtlsFile_Log where ID=" + fileID));
        }

        public int insertRecordAtFolderCreation(string LogFileID, string LogManuals, string FolderPath, int NodeType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@LogFileID",LogFileID),
                                            new SqlParameter("@logManuals",LogManuals),
                                            new SqlParameter("@FolderPath",FolderPath),
                                            new SqlParameter("@NodeType",NodeType),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_RecordAtFolderCreation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int Insert_Dept_Folder_Access_DL(int FolderID, int DeptID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FolderID",FolderID),
                                          new SqlParameter("@DeptID",DeptID),
                                          new SqlParameter("@Created_By",UserID),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_Dept_Folder_Access", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Insert_User_Folder_Access_DL(int FolderID, int UserID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FolderID",FolderID),
                                          new SqlParameter("@UserID",UserID),
                                          new SqlParameter("@Created_By",CreatedBy),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_User_Folder_Access", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public void Delete_Dept_Folder_Access_DL(int FolderID, int DeptID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FolderID",FolderID),
                                          new SqlParameter("@DeptID",DeptID)
                                        };
            
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Delete_Dept_Folder_Access", sqlprm);            
        }
        public void Delete_User_Folder_Access_DL(int FolderID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FolderID",FolderID),
                                          new SqlParameter("@UserID",UserID)
                                        };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Delete_User_Folder_Access", sqlprm);
        }
        
        public DataSet getFileIDByDocInfo(string DocumentName)
        {
            string queryText = @"SELECT     QMSdtlsFile_Log.ID, QMSdtlsFile_Log.FilePath, v.version
            FROM         QMSdtlsFile_Log LEFT OUTER JOIN
                                      (SELECT     FileID, MAX(Version) AS version
                                        FROM          QMS_FileVersionInfo
                                        GROUP BY FileID) AS v ON QMSdtlsFile_Log.ID = v.FileID
            WHERE     (QMSdtlsFile_Log.LogFileID = '" + DocumentName + "')";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, queryText);
        }

        public void AddNewUser(string Fname, string Lname, string Mname, string User, string Pwd, string Email, int AccessLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Fname",Fname),
                                          new SqlParameter("@Lname",Lname),
                                          new SqlParameter("@Mname",Mname),
                                          new SqlParameter("@Username",User),
                                          new SqlParameter("@Pwd",Pwd),
                                          new SqlParameter("@Email",Email),
                                          new SqlParameter("@AccessLevel",AccessLevel),
                                         };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_QMS_AddNewUser", sqlprm);
        }

        public DataSet AccessLevel()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_AccessLevel");
        }

        public DataSet UserDetails()
        {
            string queryText = @" SELECT     lib_user.Active_Status, lib_user.UserID, lib_user.First_Name AS FirstName, 
                                  lib_user.Middle_Name AS MiddleName, lib_user.Last_Name AS Lastname, lib_user.MailId, lib_user.Role, 
                                  al.accesslevel, lib_user.AccessLevel AS AccessLevel_id, lib_user.ManagerID, (mgr.First_Name + ' ' + mgr.Last_Name) as  Mgr
            FROM         lib_user LEFT OUTER JOIN
                                  lib_user AS MGR ON lib_user.ManagerID = MGR.UserID LEFT OUTER JOIN
                                      (SELECT     a.ID AS level_id, a.Name AS accesslevel
                                        FROM          QMS_SystemParameters AS a INNER JOIN
                                                               QMS_SystemParameters AS b ON a.Prarent_Code = b.ID
                                        WHERE      (UPPER(b.Name) = 'ACCESSLEVEL')) AS al ON lib_user.AccessLevel = al.level_id 
            WHERE     (lib_user.Active_Status = 1) ";

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, queryText);
        }

        public DataSet getUserDetailsByID(int userID)
        {
            string queryText = @"SELECT     UserID, UserName, First_Name, Last_Name, Middle_Name, MailId, Role, Password, AccessLevel
                    FROM         lib_user
                    WHERE     (UserID = " + userID + ") ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, queryText);
        }

        public void UpdateUserInfo(int USERID, string FirstName, string LastName, string MiddleName, string userName, string password, string Email, int accessLevel)
        {
            SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@userID", SqlDbType.Int),
                 new SqlParameter("@FirstName", SqlDbType.VarChar,200),
                 new SqlParameter("@LastName", SqlDbType.VarChar,200),
                 new SqlParameter("@MiddleName", SqlDbType.VarChar,200),
                 new SqlParameter("@userName", SqlDbType.VarChar,200),
                 new SqlParameter("@password", SqlDbType.VarChar,200),
                 new SqlParameter("@MailId",SqlDbType.VarChar,100),
                 new SqlParameter("@AccessLevelID", SqlDbType.Int)
             };

            obj[0].Value = USERID;
            obj[1].Value = FirstName;
            obj[2].Value = LastName;
            obj[3].Value = MiddleName;
            obj[4].Value = userName;
            obj[5].Value = password;
            obj[6].Value = Email;
            obj[7].Value = accessLevel;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_QMS_UpdateUserInfo", obj);
        }

        public void DeleteUserByID(int USERID)
        {
            //SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, "UPDATE  lib_user  set Active_Status=0 where UserID=" + USERID);
        }

        public void RenameFolder(string ExistingFolderPath, string NewFolderPath, string NewDocName)
        {
            string queryText = "update QMSdtlsFile_Log  set LogFileID='" + NewDocName + "', LogDate=getdate(),FilePath='" + NewFolderPath + "' where FilePath= '" + ExistingFolderPath + "'";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

            queryText = "update QMSdtlsFile_Log  set FilePath=(select replace(FilePath,'" + ExistingFolderPath + "','" + NewFolderPath + "')) where FilePath like '" + ExistingFolderPath + "%' AND LogFileID!='" + NewDocName + "'";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

        }
        /// <summary>
        /// This Function Update New Folder Name with Old Folder Name
        /// </summary>
        /// <param name="ExistingFolderPath">Old Folder Path to replaced with new folder path</param>
        /// <param name="NewFolderPath">New Folder Path to be update in place of old path</param>
        /// <param name="NewDocName">Name Of The Folder</param>
        /// <param name="FolderID">ID Of The Folder</param>
        public void RenameFolder(string ExistingFolderPath, string NewFolderPath, string NewDocName,int FolderID)
        {
            /*Update LogFileID(Name of folder) with New Folder Name, Update Log Date, Update FilePath with new folder path   */
            string queryText = "update QMSdtlsFile_Log  set LogFileID='" + NewDocName + "', LogDate=getdate(),FilePath='" + NewFolderPath + "' where FilePath= '" + ExistingFolderPath + "'";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

            /* This will update File Path, LOGManual1, LOGManual2 Of the files which are inside the folder   */
            queryText = "update QMSdtlsFile_Log  set LOGManual1='" + NewFolderPath + "',LOGManual2='" + NewFolderPath + "', FilePath=(select replace(FilePath,'" + ExistingFolderPath + "','" + NewFolderPath + "')) where FilePath like '" + ExistingFolderPath + "%' AND LogFileID!='" + NewDocName + "'";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

            /* This will Sync the folder record to vessel  */
            queryText = "EXEC SYNC_SP_DataSynch_MultiPK_DataLog 'QMSdtlsFile_Log', 'ID=" + Convert.ToString(FolderID) + "',0 ";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

            /* This will Sync actual folder to vessel */
            queryText = @"DECLARE @Filepath varchar(2000)='', @ATTACHMENT_PATH VARCHAR(1000)
		                  SELECT @Filepath = FilePath+'/' from QMSdtlsFile_Log where  id = (select parentid FROM  QMSdtlsFile_Log where ID = "+FolderID+")";
            queryText += "\r\n  SELECT @ATTACHMENT_PATH = REPLACE(FilePath,@Filepath,'') FROM  QMSdtlsFile_Log where ID = " + FolderID;
		    queryText+="\r\n EXEC [SYNC_INS_DataLog_Attachments] @ATTACHMENT_PATH, 0 ";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

            /* This will get the ID of files which are inside the folder which is rename */
            queryText = "select ID from QMSdtlsFile_Log where ParentID="+ FolderID+ " and Active_Status=1";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, queryText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                /* This will sync record for documents inside folder which is rename */
                string DocID = ds.Tables[0].Rows[i][0].ToString();
                queryText = "EXEC SYNC_SP_DataSynch_MultiPK_DataLog 'QMSdtlsFile_Log', 'ID=" + Convert.ToString(DocID) + "',0 ";
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);


                /* This will sync the actual physical document inside the folder which is rename */
                queryText = @"DECLARE @Filepath varchar(2000)='', @ATTACHMENT_PATH VARCHAR(1000)
		                  SELECT @Filepath = FilePath+'/' from QMSdtlsFile_Log where  id = (select parentid FROM  QMSdtlsFile_Log where ID = " + DocID + ")";
                queryText += "\r\n  SELECT @ATTACHMENT_PATH = REPLACE(FilePath,@Filepath,'') FROM  QMSdtlsFile_Log where ID = " + DocID;
                queryText += "\r\n EXEC [SYNC_INS_DataLog_Attachments] @ATTACHMENT_PATH, 0 ";
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);
            }
               

        }
        public void DeleteFolder(string ExistingFolderPathArr)
        {
            string queryText = @"delete from  QMSdtlsFile_Log FilePath='" + ExistingFolderPathArr + "' ";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);
        }

        public DataSet getFolderAsync(int UserID, int dir, string PageLink)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Dir",dir),
                 new SqlParameter("@PageLink",PageLink)     
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FolderAsync", obj);
        }

        public int CreateNewFolder(string NewFolderName, int ParentFolderID, DataTable FolderAccessUserList, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@NewFolderName",NewFolderName),
                                            new SqlParameter("@ParentFolderID",ParentFolderID),
                                            new SqlParameter("@FolderAccessUserList",FolderAccessUserList),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            sqlprm[2].SqlDbType = SqlDbType.Structured;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_CreateNewFolder", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int Add_NewDocument_DL(int ParentFolderID, string FileName, string filePath, int UserID, string Remarks, long Size)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@ParentFolderID",ParentFolderID),
                                            new SqlParameter("@FileName",FileName),
                                            new SqlParameter("@filePath",filePath),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@Remarks",Remarks),
                                             new SqlParameter("@Size",Size),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Insert_NewDocument", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }



        public DataTable QMS_Files_Approval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Approval_Status", Approval_Status),
                   new System.Data.SqlClient.SqlParameter("@Approved_By", Approved_By),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Files_Approval_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public DataTable QMS_Check_FileApprovalExists(int? FileID, int? Approver_ID,  int? Approval_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                                        {
                                            new System.Data.SqlClient.SqlParameter("@File_ID",FileID),
                                            new System.Data.SqlClient.SqlParameter("@Approved_By",Approver_ID),
                                            new System.Data.SqlClient.SqlParameter("@Approval_Status",Approval_Status),
                                            new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                                            new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                                            new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                                            new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                                            new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                                        };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_Check_FileApprovalExists", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "JIT_DMS_Check_FileApprovalExists", sqlprm);
            //return ds.Tables[0];

        }
        public DataTable QMS_Files_Approval_List(int ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Files_Approval_List", obj);
         return ds.Tables[0];
        }


        public int QMS_Files_Approved(int QMSID, int Approve_By, string Remark, int Created_by, int Version)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@QMSID",QMSID),
                                            new SqlParameter("@Approve_By",Approve_By),
                                            new SqlParameter("@Remark",Remark),
                                            new SqlParameter("@Created_by",Created_by),
                                            new SqlParameter("@Version",Version),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_File_Approved", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public DataTable Get_FileName(string Path, string FileName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Path",Path),
                                            new SqlParameter("@FileName",FileName),
                                        };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_GET_FileName", sqlprm);
            return ds.Tables[0];

        }
        public DataTable getFileIDByFullPath(string FilePath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@FilePath",FilePath),
                                          
                                        };


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Get_FileIDByFullPath", sqlprm).Tables[0];
        }
        public DataTable GET_FolderApproverList(int FolderID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@FolderID",FolderID),
                                           
                                        };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_GET_FolderApproverList", sqlprm);
            return ds.Tables[0];

        }
        public void UpdateDMSApprovarList(int FolderID, DataTable FolderApprovalLevel, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@FolderID",FolderID),
                                            new SqlParameter("@FolderApprovalLevel",FolderApprovalLevel),
                                            new SqlParameter("@UserID",UserID),
                                            
                                        };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_UpdateApprovarList", sqlprm);

        }
        public void Delete_DMSFile_Folder(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@UserID",UserID),
                                            
                                        };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_Delete_File_Folder", sqlprm);

        }

        public DataSet getDocumentReadListbyDocument_ID_DL(int DocID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@DocID",SqlDbType.Int)                                         
                                        };

            sqlprm[0].Value = DocID;
            //sqlprm[1].Value = versionid;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_GET_ReadDocument", sqlprm);
          
        }
        public DataTable QMS_SP_Files_SyncApproval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int DownloadRequired, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Approval_Status", Approval_Status),
                   new System.Data.SqlClient.SqlParameter("@Approved_By", Approved_By),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    new System.Data.SqlClient.SqlParameter("@DownloadRequired",DownloadRequired),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Files_SyncApproval_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public DataTable QMS_SP_Files_SyncApproval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int DownloadRequired,DataTable SizeRange, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Approval_Status", Approval_Status),
                   new System.Data.SqlClient.SqlParameter("@Approved_By", Approved_By),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    new System.Data.SqlClient.SqlParameter("@DownloadRequired",DownloadRequired),
                     new System.Data.SqlClient.SqlParameter("@SizeRange",SizeRange),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "QMS_SP_Files_SyncApproval_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public int QMS_SP_File_Sync(int QMSID, int Approve_By, string Remark, int Created_by, int Version)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@QMSID",QMSID),
                                            new SqlParameter("@Approve_By",Approve_By),
                                            new SqlParameter("@Remark",Remark),
                                            new SqlParameter("@Created_by",Created_by),
                                            new SqlParameter("@Version",Version),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "QMS_SP_File_Sync", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
    }
}
