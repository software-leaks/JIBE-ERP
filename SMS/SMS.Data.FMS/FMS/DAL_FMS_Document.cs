using System;
using System.Data;
using System.Configuration;

using SMS.Data;
using System.Data.SqlClient;
using System.Globalization;
/// <summary>
/// Summary description for FMSDocument
/// </summary>
/// 
namespace SMS.Data.FMS
{
    public class DAL_FMS_Document
    {
        private string _messagename;	//Error or success message string
        private long _messagecode = 0;	//Error code or success code value (0 for success)
        private string _constring;		//Connection String
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        static string _internalConnectionstatic = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DataSet FMS_GET_FORM_TREE(string FolderName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  
										   new SqlParameter("@Search",FolderName)
										  
										 
									  
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_GET_FORM_TREE", sqlprm);
        }


        public int FMS_Get_FolderExist(string FileName, string FilePath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  
										   new SqlParameter("@FilePath",FilePath),
											new SqlParameter("@FileName",FileName)
										 
									  
										};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FolderExist", sqlprm));
        }
        public DataSet FMS_Get_DocScheduleListByFolder(int File_ID, int PageIndex, int PageSize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@ID",File_ID),
										   new SqlParameter("@PAGE_INDEX",PageIndex),
											 new SqlParameter("@PAGE_SIZE",PageSize),
												new SqlParameter("@ISFETCHCOUNT",isfetchcount)
									  
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_DocScheduleListByFolder", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
            //  return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_DocScheduleListByFolder", sqlprm);
        }

        public DataSet FMS_Get_FileRejectionInfo(int File_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@File_ID",File_ID),
										 
									  
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileRejectionInfo", sqlprm);
        }
        public DataSet FMS_Get_FormTypeByID(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@ID",ID),
										 
									  
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FormTypeByID", sqlprm);
        }
        public DataSet FMS_Get_FormType()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FormType");
        }
        public DataSet FMS_Get_FormTypeList()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FormTypeList");
        }
        public int FMS_Update_FormType(int FormTypeID, string FormType, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@FormTypeID",FormTypeID),
										  new SqlParameter("@FormType",FormType),
											new SqlParameter("@Modified_By",UserID)
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_FormType", sqlprm);
        }
        public int FMS_Update_Document(int DocumentID, string FileName, int FormType, int Department, int UserID, string Remarks, DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@DocumentID",DocumentID),
										  new SqlParameter("@FileName",FileName),
											new SqlParameter("@FormType",FormType),
                                              new SqlParameter("@Department",Department),
										  new SqlParameter("@UserID",UserID),
											new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@dt",dt)
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_Document", sqlprm);
        }
        /// <summary>
        /// This function is created by Pranav Sakpal on 28-07-2016
        /// This function will update file details will  by calling FMS_Update_Document Procedure.
        /// </summary>
        /// <param name="DocumentID">Valid documentID.</param>
        /// <param name="FileName">Valid updated file name.</param>
        /// <param name="FormType">Valid Form type ID</param>
        /// <param name="Department">Valid department ID</param>
        /// <param name="UserID">valid User ID </param>
        /// <param name="Remarks">Valid Remarks</param>
        /// <param name="ParentID">Valid Parent folder ID</param>
        /// <param name="dt">Valid datatable contains RA forms.</param>
        /// <returns>Returns integer value </returns>
        public int FMS_Update_Document(int DocumentID, string FileName, int FormType, int Department, int UserID, string Remarks,int ParentID, DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@DocumentID",DocumentID),
										  new SqlParameter("@FileName",FileName),
											new SqlParameter("@FormType",FormType),
                                              new SqlParameter("@Department",Department),
										  new SqlParameter("@UserID",UserID),
                                          new SqlParameter("@ParentID",ParentID),
											new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@dt",dt)
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_Document", sqlprm);
        }

        public int FMS_Restore_FormType(int FormTypeID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@FormTypeID",FormTypeID),
											new SqlParameter("@RestoreBy",UserID)
										 
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Restore_FormType", sqlprm);
        }
        public int FMS_Insert_FormType(string FormType, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@FormType",FormType),
										  new SqlParameter("@CreatedBy",UserID),
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_FormType", sqlprm);
        }
        public int FMS_Delete_FormType(int FormTypeID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@FormTypeID",FormTypeID),
										  new SqlParameter("@DeletedBy",UserID),
										 
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Delete_FormType", sqlprm);
        }





        public int FMS_Delete_FileApprovar(int AppLevelID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@AppLevelID",AppLevelID),
										  new SqlParameter("@UserID",UserID),
									  
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Delete_FileApprovar", sqlprm);
        }
        public DataSet FMS_Get_ScheduleFileApprovalByScheduleID(int StatusID, int VesselID, int OfficeID, int FileID, int ApprovedBy, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
												new SqlParameter("@StatID",StatusID),
												new SqlParameter("@VesselID",VesselID),
												new SqlParameter("@OfficeID",OfficeID),
												new SqlParameter("@File_ID",FileID),
												new SqlParameter("@Approved_By",ApprovedBy),
												new SqlParameter("@ISFETCHCOUNT",isfetchcount)
										 
																				   
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApprovalByScheduleID", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds;

        }
        public DataSet FMS_Get_ScheduleFileApprovalByFileID(int StatusID, int VesselID, int OfficeID, int FileID, int ApprovedBy, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
												new SqlParameter("@StatID",StatusID),
												new SqlParameter("@VesselID",VesselID),
												new SqlParameter("@OfficeID",OfficeID),
												new SqlParameter("@File_ID",FileID),
												new SqlParameter("@Approved_By",ApprovedBy),
												new SqlParameter("@ISFETCHCOUNT",isfetchcount)
										 
																				   
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApprovalByFileID", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds;

        }
        public int FMS_Update_DocumentSchedule(int ScheduleID, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										   new SqlParameter("@ScheduleID",ScheduleID),
											new SqlParameter("@Modified_By",ModifiedBy)
										  
										
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_DocumentSchedule", sqlprm);
        }

        public static DataTable FMS_Get_ScheduleFileApprovalOverdue(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@UserID",UserID),
			 
		  
			   
			};
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(_internalConnectionstatic, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApprovalOverdue", sqlprm);
            return ds.Tables[0];
        }

        public static DataTable FMS_Get_ScheduleFileReceivingOverdue(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@UserID",UserID),
			 
		  
			   
			};
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(_internalConnectionstatic, CommandType.StoredProcedure, "FMS_Get_ScheduleFileReceivingOverdue", sqlprm);
            return ds.Tables[0];
        }
        public DataSet FMS_Get_ScheduleStatusVoyageInfo(int StatusID, int OfficeID, int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@StatusID",StatusID),
				new SqlParameter("@OfficeID",OfficeID),
				new SqlParameter("@VESSELID",VesselID)
		  
			   
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleStatusVoyageInfo", sqlprm);
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
                SqlParameter[] sqlprm = new SqlParameter[]
										{
										 
										  new SqlParameter("@Modified_By",ModifiedBy),
										  new SqlParameter("@StatusID",StatusID),
										  new SqlParameter("@OfficeID",OfficeID),
										   new SqlParameter("@VesselID",VesselID),
										   new SqlParameter("@Remark",Remark),
											new SqlParameter("@Version",Version),
										    new SqlParameter("@Approval_Level",Level),
										    new SqlParameter("@File_ID",File_ID),
                                            new SqlParameter("return",SqlDbType.Int)
						
										};
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_ScheduleStatusForRework", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet FMS_Get_ApprovedScheduleStatus(int ApprovedBy, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string searchText, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@ApproveBy",ApprovedBy),
											  new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
												new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
												new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
												new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
												 new System.Data.SqlClient.SqlParameter("@Searchtext",searchText),
												new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
										 
																				   
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ApprovedScheduleStatus", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);



            return ds;
        }
        public DataSet FMS_Get_ScheduleFileApprovalByStatus(int StatusID, int OfficeID, int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@StatusID",StatusID),
										  new SqlParameter("@OfficeID",OfficeID),
										  new SqlParameter("@VesselID",VesselID)
																				   
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApprovalByStatus", sqlprm);
        }
        public DataSet FMS_Get_ApprovedSchedule(int StatusID, int OfficeID, int VesselID, int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@StatusID",StatusID),
										  new SqlParameter("@OfficeID",OfficeID),
										  new SqlParameter("@VesselID",VesselID),
										  new SqlParameter("@FileID",FileID)
																				   
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ApprovedSchedule", sqlprm);
        }
        public DataSet FMS_Get_ScheduleApprovalHistory(int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID)
									  
																				   
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleApprovalHistory", sqlprm);
        }
        public int FMS_Update_ScheduleStatus(int Status_ID, int OfficeID, int VesselID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@Status_ID",Status_ID),
										   new SqlParameter("@OfficeID",OfficeID),
											new SqlParameter("@VesselID",VesselID),
										new SqlParameter("@Modified_By",Modified_By),
									 
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_ScheduleStatus", sqlprm);
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
                SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@Status_ID",Status_ID),
										  new SqlParameter("@Office_ID",OfficeID),
										  new SqlParameter("@Vessel_ID",VesselID),
										  new SqlParameter("@File_ID",File_ID),
										  new SqlParameter("@Remark",Remark),
										  new SqlParameter("@Approve_By",Approved_By),
										  new SqlParameter("@Created_By",CreatedBy),
										  new SqlParameter("@Version",version),
										  new SqlParameter("@Approval_Level",Approval_Level),
										  new SqlParameter("@Approval_Status",Approval_Status),
										  new SqlParameter("return",SqlDbType.Int)
										};
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_ScheduleFileApproval", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int FMS_Get_ScheduleFileApprovalExists(int StatusID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{ 
										  new SqlParameter("@StatusID",StatusID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel)
										 
										};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApprovalExists", sqlprm));
        }

        public DataSet FMS_Get_ScheduleFileApproval(int? ApprovedBy, string sortby, int? sortdirection, int? pagenumber, int? pagesize, string SearchText, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
												new SqlParameter("@Approved_By",ApprovedBy),
												new SqlParameter("@SORTBY",sortby),
												new SqlParameter("@SORTDIRECTION",sortdirection),
												new SqlParameter("@PAGENUMBER",pagenumber),
												new SqlParameter("@PAGESIZE",pagesize),
												new SqlParameter("@Searchtext",SearchText),
												new SqlParameter("@ISFETCHCOUNT",isfetchcount),
										 
										};


            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApproval", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);



            return ds;

        }
        public int FMS_Insert_ApprovalLevels(int FileID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@CreatedBy",CreatedBy)
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_ApprovalLevels", sqlprm);
        }
        public int FMS_Delete_ApprovalLevel(int FileID, int DeletedBy, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel),
                                           new SqlParameter("@DeletedBy",DeletedBy)
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Delete_ApprovalLevel", sqlprm);
        }
        public int FMS_Get_ApproverInLevel(int FileID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel),
                                        
										 
										};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ApproverInLevel", sqlprm));
        }
        public int FMS_Get_ApproverInLevel(int FileID, int ApprovalLevel, int? UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel),
                                           new SqlParameter("@UserID",UserID),
                                        
										 
										};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ApproverInLevel", sqlprm));
        }
        public int FMS_Get_ApproverByLevel(int FileID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@Approval_Level",ApprovalLevel),
                                          									 
										};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ApproverByLevel", sqlprm));
        }
        public DataSet FMS_Get_VesselSchInfo(int FileID, int UserCompanyId, int? FleetID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@UserCompanyID",UserCompanyId),
                                          new SqlParameter("@FleetID",FleetID),	
                                                                                 										 
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_VesselSchInfo", sqlprm);
        }
        public int FMS_Insert_FileApprovar(int FileID, int ApprovalLevel, int ApprovarID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel),
										  new SqlParameter("@ApprovarID",ApprovarID),
										  new SqlParameter("@CreatedBy",CreatedBy)
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_FileApprovar", sqlprm);
        }
        /// <summary>
        /// This function is edited by Pranav Sakpal on 23-06-2016 
        /// This function will call for SP to update document assignments to vessel
        /// On performing assign and un-assign @Activestatus values will be changed and this will update active_Status of schedule table.
        /// There is one more parameter added @Activestatus.
        /// </summary>
        /// <param name="FileID">This is valid document ID </param>
        /// <param name="VesselID">Valid vesselID</param>
        /// <param name="CreatedBy">User id as creator</param>
        /// <param name="ActiveStatus">Active status for assignment 1 and un-assignment 0.</param>
        /// <returns></returns>
        public int FMS_Insert_AssignFormToVessel(int FileID, int VesselID, int CreatedBy, int ActiveStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@DocID",FileID),
										  new SqlParameter("@VesselID",VesselID),
										  new SqlParameter("@UserID",CreatedBy),
                                          new SqlParameter("@Activestatus",ActiveStatus),//This is added to set active status of schedule.
										
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_AssignFormToVessel", sqlprm);
        }
        public int FMS_Update_FileApprovar(int FileID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel)
										
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_FileApprovar", sqlprm);
        }
        public int FMS_Update_FileApprovarById(int FileID, int ApprovalLevel, int ApprovarID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ApprovalLevel",ApprovalLevel),
										  new SqlParameter("@ApprovarID",ApprovarID),
										  new SqlParameter("@CreatedBy",CreatedBy)
										 
										};
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_FileApprovarById", sqlprm);
        }

        public DataSet getRootFolderName()
        {
            string sqlQueryView = "Select Top(1) FilePath from FMS_DTL_File order by LogFileID,FilePath ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);

        }
        public DataSet FMS_Get_UserListWithFilter(string SearchText)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@SearchText",SearchText),
											
											
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_UserListWithFilter", sqlprm);


        }
        public DataSet FMS_Get_UserList()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_UserList");


        }
        public DataSet FMS_Get_ApprovarByLevel(int FileID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FileID",FileID),
											 new SqlParameter("@ApprovalLevel",ApprovalLevel)
											
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ApprovarByLevel", sqlprm);


        }
        public DataSet FMS_Get_FileApprovalLevel(int FileID, int Status_ID, int Office_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FileID",FileID),
                                            new SqlParameter("@Status_ID",Status_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
											
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileApprovalLevel", sqlprm);


        }

        public DataSet FMS_Get_FileApprovalLevelStatus(int FileID, int Status_ID, int Office_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FileID",FileID),
                                            new SqlParameter("@Status_ID",Status_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
											
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileApprovalLevelStatus", sqlprm);


        }
        public DataSet FMS_Get_FileApprovalLevel(int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FileID",FileID),
                                       
											
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileApprovalLevel", sqlprm);


        }
        public DataSet FMS_Get_FileApprovar(int FileID, int? Level)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FileID",FileID),
											new SqlParameter("@Level",Level)

											
										};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileApprovar", sqlprm);


        }
        public DataTable GET_FolderUser(int FolderID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FolderID",FolderID),
										};
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FolderUser", sqlprm);
            return ds.Tables[0];

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
                SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@File_ID",File_ID),
											new SqlParameter("@Level_ID",Level_ID),
											new SqlParameter("@Remark",Remark),
                                            new SqlParameter("return",SqlDbType.Int)
										};
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_FileApproval", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
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

                SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@File_ID",File_ID),
											new SqlParameter("@Remark",Remark),
											new SqlParameter("@LevelID",Level_ID),
											new SqlParameter("@Version",Version),
											new SqlParameter("@Rejected_By",RejectedBy),
											 new SqlParameter("return",SqlDbType.Int)
											
										};
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_FileRejection", sqlprm);
                return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateNewFolderApproval(int FolderID, DataTable FolderApprovalLevel, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FolderID",FolderID),
											new SqlParameter("@FolderApprovalLevel",FolderApprovalLevel),
											new SqlParameter("@UserID",UserID),
											
										};

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_CreateNewFolderApproval", sqlprm);

        }
        public DataTable Check_FolderApprovalExists(int ParentFolderID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@ParentFolderID",ParentFolderID),
										};
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FolderApprovalExists", sqlprm);
            return ds.Tables[0];

        }
        public void CreateNewFileApproval(int FileID, string sFileName, int ParentFolderID, string iFolderName, DataTable dtApproval_Level, int User_ID, int CompanyID)
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

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_CreateNewFileApproval", sqlprm);

        }
        public DataSet getLogInfo(string userID, string LFileID)
        {

            string sqlQuery = " select top 1(logdate),UserID,LogFileID,LOGManuals1, LOGManuals2,LOGManuals3 from ";
            sqlQuery += " FMSdtls_Log where UserID=@UserID AND LogFileID=@LogFileID order by logdate desc ";

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

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[SP_FMS_getVesselList]");

        }

        public DataSet getSyncdataFromDB(string QueryText)
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, QueryText);

        }

        public DataSet UpdateFMSLog(string FileName, string Status)
        {


            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@LogFileID",SqlDbType.VarChar), 
				new SqlParameter("@Status",SqlDbType.VarChar), 
			};

            obj[0].Value = FileName;
            obj[1].Value = Status;

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[SP_FMS_RevUpdate]", obj);

        }

        public DataSet getAllFiles(string FileNameID)
        {


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "SELECT     FilePath FROM         FMS_DTL_File WHERE     (LOGManual1 = '" + FileNameID + "')");

        }

        public int insertDataFMSRev(int VesselCode, string RevPath, string Rev_Filename, string LogDate, int CreatedBy)
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
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "[SP_FMS_RevInsert]", obj);
            return Convert.ToInt32(obj[5].Value);

        }

        public DataSet getFilpath()
        {
            string sqlQueryView = "Select FilePath,id,NodeType,LogFileID from FMS_DTL_File order by id ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQueryView);
        }

        public DataTable getFileList(int UserID)
        {

            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@UserID",UserID)
				
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Get_FileList", obj).Tables[0];
        }
        public DataTable getFolderList(int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@UserID",UserID)                
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Get_FolderList", obj).Tables[0];
        }
        public DataSet getUserLog(string userID, string LFileID)
        {
            string sqlQuery = " select top 1(logdate),UserID,LogFileID,LOGManuals1, LOGManuals2,LOGManuals3 from ";
            sqlQuery += " FMSdtls_Log where UserID='" + userID + "' AND LogFileID='" + LFileID + "' order by ID Desc ,logdate desc ";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        public DataSet getLogViewDetails(int userID)
        {
            string sqlQueryView = "Select logfileid,CONVERT(VARCHAR(8), logdate, 5) AS logdate,LOGManuals2 as logmanuals1,LOGManuals2 as logmanuals2  from";
            sqlQueryView += " FMSdtls_Log where logfileid IN(Select LogFileID from FMS_DTL_File ) AND userid='" + userID + "' order by  LogDate DESC";
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


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[SP_FMSLog_ViewNotRead]", obj);
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


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_FMS_View_Read", obj);


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


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_FMS_File_ReadAll", obj);
        }

        public void insertDataFMSLog(int UserID, string LOGManuals2, string LOGManuals3, string FileName, string LogDate)
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

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_FMSLog_InsertData", obj);
        }

        public DataSet FillManuals()
        {
            string sqlmanual = "Select distinct logmanual1 as logmanuals1 from FMS_DTL_File order by Logmanual1";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlmanual);
        }

        public DataSet FillUser(int userID)
        {

            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@userID",SqlDbType.Int)
			};

            obj[0].Value = userID;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_FMSLog_UserLoad", obj);

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

        internal void insertDataFMSLog(string userID, string path, string fileName, DateTime date1)
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
            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "SP_FMS_Authentication", sqlprm).ToString();
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_FMS_getUserIDbyUsername", obj);
            return Convert.ToInt32(obj[1].Value);
        }

        public DataSet getUsersDetailsByUserID(string userid)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
									   {
										   new SqlParameter("@userid",userid)
									   };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_FMS_UserDetailsbyId", sqlprm);
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
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "SP_FMS_ChangePassword", sqlprm);
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
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Insert_FileLogIntoDB", sqlprm);
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Get_FileCountByFileID", sqlprm);
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
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_UpdateVersionInfoOfNewFileAdd", sqlprm);
        }

        public DataSet getFileVersion(int fileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",SqlDbType.Int)
										};

            sqlprm[0].Value = fileID;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileVersion", sqlprm);
        }

        public DataSet getCheckedFileInfo(int FileID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",SqlDbType.Int)
										};

            sqlprm[0].Value = FileID;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_CheckedFileInfo", sqlprm);
        }

        public int getFileIDByPath(string FilePath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FilePath",FilePath),
										  new SqlParameter("@FileID",SqlDbType.Int)
										};

            sqlprm[1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileIDByPath", sqlprm);
            return Convert.ToInt32(sqlprm[1].Value);
            //return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Get_FileIDByPath", sqlprm).Tables[0];
        }


        public int checkFileExits(string sFileName, string sFolderPath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileName",sFileName),
										  new SqlParameter("@FolderPath",sFolderPath),
										  new SqlParameter("@DocID",SqlDbType.Int)
										};

            sqlprm[2].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileExists", sqlprm);
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
            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileIDByFileName", sqlprm);
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

            SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_UserAccess_OnFile", sqlprm);
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
            SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_RecordAtCheckout", sqlprm);

        }

        public void insertRecordAtCheckIN(int FileID, string FileName, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FileID",SqlDbType.Int),
											new SqlParameter("@FileName",SqlDbType.Text),
											new SqlParameter("@UserID",SqlDbType.Int)

										};

            sqlprm[0].Value = FileID;
            sqlprm[1].Value = FileName;
            sqlprm[2].Value = UserID;
            SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_RecordAtCheckIN", sqlprm);

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
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_LatestFileOperationByUserID", sqlprm);

        }

        public DataSet fileInfoAtTreeBind(string FileName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileName",SqlDbType.VarChar,1000)

										};

            sqlprm[0].Value = FileName;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_FMS_fileInfoAtTreeBind", sqlprm);

        }

        public DataSet getFileDetailsByID(int DocID, int versionid)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@DocID",SqlDbType.Int),
										   new SqlParameter("@VerNo",SqlDbType.Int)
										};

            sqlprm[0].Value = DocID;
            sqlprm[1].Value = versionid;
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileDetails", sqlprm);

            //            string sqlQuery = @"SELECT     FMS_DTL_File.ID, FMS_DTL_File.LogFileID, FMS_DTL_File.FilePath, FMS_DTL_File.Version, FMS_DTL_File.Date_Of_Creatation, 
            //                                  lastOpp.Operation_Date, lastOpp.Operation_Type, lib_user.First_Name , lib_user.Last_Name, 
            //                                  (lib_user.First_Name + ' ' + lib_user.Last_Name) as UserName, lib_user.UserID, FMS_DTL_File.Created_By, CreatedUserInfo.First_Name AS CreatedBYFirstName
            //            FROM         lib_user AS CreatedUserInfo RIGHT OUTER  JOIN
            //                                  FMS_DTL_File ON CreatedUserInfo.UserID = FMS_DTL_File.Created_By LEFT OUTER JOIN
            //                                  lib_user INNER JOIN
            //                                      (SELECT     FileID, Operation_Date, Operation_Type, User_ID
            //                                        FROM          FMS_FileOperationInfo
            //                                        WHERE      (ID IN
            //                                                                   (SELECT     MAX(ID) AS id
            //                                                                     FROM          FMS_FileOperationInfo AS FMS_FileOperationInfo_1
            //                                                                     GROUP BY FileID))) AS lastOpp ON lib_user.UserID = lastOpp.User_ID ON FMS_DTL_File.ID = lastOpp.FileID
            //            WHERE     FMS_DTL_File.ID  = " + DocID;
            //            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// Modified by Anjali DT:21-06-2016
        /// </summary>
        /// <param name="FileID">ID of Selected file.</param>
        /// <param name="showArchivedForms">If showArchivedForms = 0 then do not show archieved forms. || If showArchivedForms = 1 then do show archieved forms.</param>
        /// <returns>List of information related to selected file.</returns>
        public DataSet GetLastestFileInfoByID(int FileID, int showArchivedForms)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ShowArchievedFilesInfo",showArchivedForms)
										  
										};

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileInfoByID", sqlprm);
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
        public DataSet GetLastestFileInfoByID(int FileID, int showArchivedForms,int StatusID,int OfficeID,int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FileID",FileID),
										  new SqlParameter("@ShowArchievedFilesInfo",showArchivedForms),
                                          new SqlParameter("@StatusID",StatusID),
										  new SqlParameter("@OfficeID",OfficeID),
										  new SqlParameter("@VesselID",VesselID),
										};

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileInfoByStatusID", sqlprm);
        }
        public int FMS_Get_FormScheduleStatus(int DocumentID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@DocumentID",DocumentID),
											
										};



            string res = SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FormScheduleStatus", sqlprm).ToString();
            return Convert.ToInt32(res);
        }
        public DataSet GetOprationDetailsByID(int FileID)
        {
            string sqlQuery = @"(SELECT     FMS_DTL_FileOperationInfo.ID, FMS_DTL_FileOperationInfo.User_ID, FMS_DTL_FileOperationInfo.Operation_Type, FMS_DTL_FileOperationInfo.Operation_Date, 
					  lib_user.First_Name AS First_Name, lib_user.Last_Name, (lib_user.First_Name + ' ' + lib_user.Last_Name) as UserName, FMS_DTL_File.LogFileID, 
					  FMS_DTL_FileOperationInfo.Version, FMS_DTL_File.ID AS DocID
				FROM         FMS_DTL_FileOperationInfo INNER JOIN
									  FMS_DTL_File ON FMS_DTL_FileOperationInfo.FileID = FMS_DTL_File.ID INNER JOIN
									  lib_user ON FMS_DTL_FileOperationInfo.User_ID = lib_user.UserID
				WHERE     (FMS_DTL_File.ID = " + FileID + @" ) ) UNION
			 ( SELECT FMS_DTL_FileApproval.ID ,Approve_By User_ID,case when ApprovalStatus!=0 then 'APPROVED' else 'PENDING APPROVAL' end Operation_Type ,
				cast(Date_Of_Approval as datetime) Operation_Date, LIB_USER.First_Name AS First_Name, 
					  LIB_USER.Last_Name, (LIB_USER.First_Name + ' ' + LIB_USER.Last_Name) as UserName, LogFileID
				,'1' as Version, FMS_DTL_File.ID AS DocID 
				FROM FMS_DTL_FileApproval 
				INNER JOIN FMS_DTL_File ON FMS_DTL_FileApproval.FMSID = FMS_DTL_File.ID 
			  
				INNER JOIN  LIB_USER on FMS_DTL_FileApproval.Approve_By=LIB_USER.UserID where FMS_DTL_FileApproval.Active_Status=1 AND FMS_DTL_FileApproval.FMSID=" + FileID + ") ORDER BY Operation_Date ";


            //") ORDER BY FMS_FileOperationInfo.ID DESC";
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, sqlQuery);
        }

        public string getMachineNameByPath(string FolderPath)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "select distinct FilePath from FMS_DTL_File where FilePath like  '%" + FolderPath + "%' "));
        }

        public int getMaxVersionFromParentTable(int fileID)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "select Version from FMS_DTL_File where ID=" + fileID));
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Insert_RecordAtFolderCreation", sqlprm);
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Insert_Dept_Folder_Access", sqlprm);
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
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_UserFolderAccess", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public void Delete_Dept_Folder_Access_DL(int FolderID, int DeptID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FolderID",FolderID),
										  new SqlParameter("@DeptID",DeptID)
										};

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Delete_Dept_Folder_Access", sqlprm);
        }
        public void Delete_User_Folder_Access_DL(int FolderID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FolderID",FolderID),
										  new SqlParameter("@UserID",UserID)
										};

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Delete_UserFolderAccess", sqlprm);
        }

        public DataSet getFileIDByDocInfo(string DocumentName)
        {
            string queryText = @"SELECT     FMS_DTL_File.ID, FMS_DTL_File.FilePath, v.version
			FROM         FMS_DTL_File LEFT OUTER JOIN
									  (SELECT     FileID, MAX(Version) AS version
										FROM          FMS_FileVersionInfo
										GROUP BY FileID) AS v ON FMS_DTL_File.ID = v.FileID
			WHERE     (FMS_DTL_File.LogFileID = '" + DocumentName + "')";
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

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_FMS_AddNewUser", sqlprm);
        }

        public DataSet AccessLevel()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Get_AccessLevel");
        }

        public DataSet UserDetails()
        {
            string queryText = @" SELECT     lib_user.Active_Status, lib_user.UserID, lib_user.First_Name AS FirstName, 
								  lib_user.Middle_Name AS MiddleName, lib_user.Last_Name AS Lastname, lib_user.MailId, lib_user.Role, 
								  al.accesslevel, lib_user.AccessLevel AS AccessLevel_id, lib_user.ManagerID, (mgr.First_Name + ' ' + mgr.Last_Name) as  Mgr
			FROM         lib_user LEFT OUTER JOIN
								  lib_user AS MGR ON lib_user.ManagerID = MGR.UserID LEFT OUTER JOIN
									  (SELECT     a.ID AS level_id, a.Name AS accesslevel
										FROM          FMS_SystemParameters AS a INNER JOIN
															   FMS_SystemParameters AS b ON a.Prarent_Code = b.ID
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

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_FMS_UpdateUserInfo", obj);
        }

        public void DeleteUserByID(int USERID)
        {
            //SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, "UPDATE  lib_user  set Active_Status=0 where UserID=" + USERID);
        }

        public void RenameFolder(string ExistingFolderPath, string NewFolderPath, string NewDocName)
        {
            string queryText = "update FMS_DTL_File  set LogFileID='" + NewDocName + "', LogDate=getdate(),FilePath='" + NewFolderPath + "' where FilePath= '" + ExistingFolderPath + "'";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

            queryText = "update FMS_DTL_File  set FilePath=(select replace(FilePath,'" + ExistingFolderPath + "','" + NewFolderPath + "')) where FilePath like '" + ExistingFolderPath + "%' AND LogFileID!='" + NewDocName + "'";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);

        }
        public void DeleteFolder(string ExistingFolderPathArr)
        {
            string queryText = @"delete from  FMS_DTL_File FilePath='" + ExistingFolderPathArr + "' ";
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, queryText);
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
            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@UserID",UserID),
				new SqlParameter("@Dir",dir),
				 new SqlParameter("@PageLink",PageLink) , 
				 new SqlParameter("@ShowArchievedForms",showArchivedForms)     
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FolderAsync", obj);
        }

        public DataSet FMS_Get_SchTree(int UserID, int dir, string PageLink)
        {
            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@UserID",UserID),
				new SqlParameter("@Dir",dir),
				 new SqlParameter("@PageLink",PageLink)     
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_SchTree", obj);
        }

        /// <summary>
        /// This function is added by Pranav Sakpal on 25-07-2016 
        /// This will simply fetch parent folder list from SP for folder tree
        /// </summary>
        /// <param name="UserID">Valid user id </param>
        /// <param name="dir">valid dir</param>
        /// <param name="PageLink">valid page link</param>
        /// <returns>This will return folder tree dataset</returns>
        public DataSet FMS_Get_ParentFolders_SchTree(int UserID, int dir, string PageLink)
        {
            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@UserID",UserID),
				new SqlParameter("@Dir",dir),
				 new SqlParameter("@PageLink",PageLink)     
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ParentFolders_SchTree", obj);
        }

        /// <summary>
        /// This function is creeated by Pranav Sakpal on 26-07-2016.
        /// This Function will call FMS_Get_ParentFoldersID SP to get ParentId of document on basis of documentID.
        /// </summary>
        /// <param name="DocID">This will accept one parameter documentID </param>
        /// <returns></returns>
        public DataSet FMS_Get_ParentFoldersID(int DocID)
        {
            SqlParameter[] obj = new SqlParameter[]
			{                   
				new SqlParameter("@DocID",DocID),
			
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ParentFoldersID", obj);
        }

        public int CreateNewFolder(string NewFolderName, int ParentFolderID, DataTable FolderAccessUserList, int UserID, string vURL)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@NewFolderName",NewFolderName),
											new SqlParameter("@ParentFolderID",ParentFolderID),
											new SqlParameter("@FolderAccessUserList",FolderAccessUserList),
											new SqlParameter("@UserID",UserID),
											 new SqlParameter("@vURL",vURL),
											new SqlParameter("return",SqlDbType.Int)
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            sqlprm[2].SqlDbType = SqlDbType.Structured;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_CreateNewFolder", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int FMS_Update_Folder(int FolderID, string FilePath, string FileName, string vURL)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FolderID",FolderID),
											new SqlParameter("@FilePath",FilePath),
											new SqlParameter("@FileName",FileName),
										   new SqlParameter("@vURL",vURL),
										};

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_Folder", sqlprm);


        }


        public int Add_NewDocument_DL(int ParentFolderID, string FileName, string filePath, int UserID, string Remarks, int formType, int Department, string Format, DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@ParentFolderID",ParentFolderID),
											new SqlParameter("@FileName",FileName),
											new SqlParameter("@filePath",filePath),
											 new SqlParameter("@FormType",formType),
											  new SqlParameter("@Department",Department),
											   new SqlParameter("@Format",Format),
											new SqlParameter("@UserID",UserID),
											new SqlParameter("@Remarks",Remarks),
											new SqlParameter("@dt",dt),
											new SqlParameter("return",SqlDbType.Int)
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_NewDocument", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }



        public DataTable FMS_Files_Approval_Search(string searchtext, int Approval_Status, int? Approved_By, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileApproval", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public DataTable FMS_Get_FileApprovalExists(int? FileID, int? Approver_ID, int? Approval_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileApprovalExists", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "JIT_DMS_Check_FileApprovalExists", sqlprm);
            //return ds.Tables[0];

        }
        public DataTable FMS_Files_Approval_List(int ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
			{ 
				   new System.Data.SqlClient.SqlParameter("@ID", ID),
			};
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_SP_Files_Approval_List", obj);
            return ds.Tables[0];
        }


        public int FMS_Files_Approved(int FMSID, int Approve_By, string Remark, int Created_by, int Version)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FMSID",FMSID),
											new SqlParameter("@Approve_By",Approve_By),
											new SqlParameter("@Remark",Remark),
											new SqlParameter("@Created_by",Created_by),
											new SqlParameter("@Version",Version),
											new SqlParameter("return",SqlDbType.Int)
										};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_SP_File_Approved", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public DataTable Get_FileName(string Path, string FileName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@Path",Path),
											new SqlParameter("@FileName",FileName),
										};
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_GET_FileName", sqlprm);
            return ds.Tables[0];

        }
        public DataTable getFileIDByFullPath(string FilePath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
										  new SqlParameter("@FilePath",FilePath),
										  
										};


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FileIDByFullPath", sqlprm).Tables[0];
        }
        public DataTable GET_FolderApproverList(int FolderID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@FolderID",FolderID),
										   
										};
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FolderApproverList", sqlprm);
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

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_ApprovarList", sqlprm);

        }
        public void Delete_DMSFile_Folder(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@ID",ID),
											new SqlParameter("@UserID",UserID),
											
										};

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Delete_FileFolder", sqlprm);

        }

        public DataSet Get_Schedule_Details_DL(int DocumentID, int ScheduleID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@DocumentID",DocumentID),
				new SqlParameter("@ScheduleID",ScheduleID),
			   
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_DocumentSchedule_Details", sqlprm);
        }
        public int Save_Schedule_DL(DataTable dtSchedule, DataTable dtSettings, int UserID, int DocumentID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@FMS_DOCUMENT_SCHEDULE",dtSchedule),
				new SqlParameter("@FMS_DOCUMENT_SETTINGS",dtSettings),
				new SqlParameter("@UserID",UserID),
				new SqlParameter("@DocumentID",DocumentID),
				new SqlParameter("return",SqlDbType.Int)
			};
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[1].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_DocumentSchedule", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_DocScheduleList_DL(int DocumentID, int? VesselID, int? FleetID, string Status, int PageIndex, int PageSize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@DocumentID",DocumentID),
				new SqlParameter("@VESSELID",VesselID),
				new SqlParameter("@FleetID",FleetID),
				new SqlParameter("@Status",Status),
			   new SqlParameter("@PAGE_INDEX",PageIndex),
			  new SqlParameter("@PAGE_SIZE",PageSize),
			  new SqlParameter("@ISFETCHCOUNT",isfetchcount),
			   
			};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_DocScheduleList", sqlprm).Tables[0];
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
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
        public DataTable Get_DocScheduleList_DL(int DocumentID, int? VesselID, int? FleetID, string Status, int PageIndex, int PageSize, ref int isfetchcount, DateTime? DateFrom, DateTime? DateTill)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@DocumentID",DocumentID),
				new SqlParameter("@VESSELID",VesselID),
				new SqlParameter("@FleetID",FleetID),
				new SqlParameter("@Status",Status),
			    new SqlParameter("@PAGE_INDEX",PageIndex),
			    new SqlParameter("@PAGE_SIZE",PageSize),
                new SqlParameter("@DateFrom",DateFrom),
                new SqlParameter("@DateTill",DateTill),
			    new SqlParameter("@ISFETCHCOUNT",isfetchcount),			   
			};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_DocScheduleList", sqlprm).Tables[0];
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }
        public DataSet GetDocSchedule_Details_DL(int ScheduleStatusID, int SchOfficeID, int SchVesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@ScheduleStatusID",ScheduleStatusID),
				new System.Data.SqlClient.SqlParameter("@SchOfficeID", SchOfficeID),
				new System.Data.SqlClient.SqlParameter("@SchVesselID", SchVesselID)    
			   
			};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_DocSchedule_Details", sqlprm);
        }
        public DataTable Get_DocSchedule_Attachments_DL(int ScheduleStatusID, int OfficeID, int VesselID)
        {
            SqlParameter[] obj = new SqlParameter[]
			 {
			 
			  new System.Data.SqlClient.SqlParameter("@ScheduleStatusID", ScheduleStatusID),
			  new System.Data.SqlClient.SqlParameter("@SchOfficeID", OfficeID),
			  new System.Data.SqlClient.SqlParameter("@SchVesselID", VesselID)
			 };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_GET_DocScheduleAttachment", obj).Tables[0];

        }
        public int Delete_DocSchedule_Attachments_DL(int Code, int UserID, int OfficeID, int VesselID)
        {
            SqlParameter[] obj = new SqlParameter[]
			 {
			 
			 new System.Data.SqlClient.SqlParameter("@Code", Code),

				new System.Data.SqlClient.SqlParameter("@OfficeID", OfficeID),
				   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
		  new System.Data.SqlClient.SqlParameter("@UserID", UserID),
			 };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Delete_DocScheduleAttachment", obj);
        }
        public int Insert_DocSchedule_Attachment_DL(int ScheduleStatusID, string Attachment_Name, string Attachment_Path, int UserID, int SchOfficeID)
        {
            SqlParameter[] obj = new SqlParameter[]
			{                   

				new System.Data.SqlClient.SqlParameter("@ScheduleStatusID",ScheduleStatusID),
				new System.Data.SqlClient.SqlParameter("@AttachName",Attachment_Name),
				new System.Data.SqlClient.SqlParameter("@AttachPath",Attachment_Path),
				new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                new System.Data.SqlClient.SqlParameter("@SchOfficeID",SchOfficeID)
			};

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_DocScheduleAttachment", obj);
        }
        public int Insert_DocSchedule_Attachment_DL(int ScheduleStatusID, string Attachment_Name, string Attachment_Path, int UserID, int SchOfficeID, int Vessel_ID)
        {
            SqlParameter[] obj = new SqlParameter[]
			{                   

				new System.Data.SqlClient.SqlParameter("@ScheduleStatusID",ScheduleStatusID),
				new System.Data.SqlClient.SqlParameter("@AttachName",Attachment_Name),
				new System.Data.SqlClient.SqlParameter("@AttachPath",Attachment_Path),
				new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                new System.Data.SqlClient.SqlParameter("@SchOfficeID",SchOfficeID),
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID)
                
			};

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_DocScheduleAttachment", obj);
        }
        public DataTable Insert_DocSchedule_Remark_DL(string Remark, int ScheduleStatusID, int UserID, int SchOfficeID, int SchVesselID)
        {
            SqlParameter[] obj = new SqlParameter[]
			 {
			 new System.Data.SqlClient.SqlParameter("@Remark", Remark),
			 new System.Data.SqlClient.SqlParameter("@ScheduleStatusID", ScheduleStatusID),
			 new System.Data.SqlClient.SqlParameter("@UserID", UserID),
             new System.Data.SqlClient.SqlParameter("@SchStatusOffID", SchOfficeID),
              new System.Data.SqlClient.SqlParameter("@SchStatusVesselID", SchVesselID),
			 };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_DocScheduleRemark", obj).Tables[0];

        }
        public DataTable Get_VesselDocHistroy_DL(int DocumentID, int VesselID, int StatusID, int OfficeID)
        {
            SqlParameter[] obj = new SqlParameter[]
			 {
			 new System.Data.SqlClient.SqlParameter("@DocumentID", DocumentID),
			 new System.Data.SqlClient.SqlParameter("@VESSELID", VesselID),
			 new System.Data.SqlClient.SqlParameter("@StatusID", StatusID),
			 new System.Data.SqlClient.SqlParameter("@OfficeID", OfficeID)
			 };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_VesselDocHistroy", obj).Tables[0];

        }

        public DataTable Get_WorkCategoryList_DL(string Category_Name)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
										{ 
										   new System.Data.SqlClient.SqlParameter("@Category_Name", Category_Name)
										};
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_WorkCategoryList", sqlprm).Tables[0];
            }
            catch (Exception)
            {

                throw;
            }


        }
        public DataTable Get_RAFormsByDocID_DL(int fileID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
										{ 
										   new System.Data.SqlClient.SqlParameter("@fileID", fileID)
										};
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_RAFormsByDocID", sqlprm).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }


        }
        public DataTable Get_RAFormsBy_ScheduleID_DL(int scheduleID, int vesselID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
										{ 
										   new System.Data.SqlClient.SqlParameter("@scheduleID", scheduleID),
											  new System.Data.SqlClient.SqlParameter("@vesselID", vesselID)
										};
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_Scheduled_RAForms", sqlprm).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FMS_Get_FormReceived_ByDate_DL(string Period, string Mode, int? IsApproved, int? ApproverID, int? VesselAssignUserID)
        {
            SqlParameter[] obj = new SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@Period", Period),
             new System.Data.SqlClient.SqlParameter("@Mode", Mode),
             new System.Data.SqlClient.SqlParameter("@IsApproved",IsApproved),
             new System.Data.SqlClient.SqlParameter("@ApproverID",ApproverID),
             new System.Data.SqlClient.SqlParameter("@VesselAssignUserID",VesselAssignUserID)           
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FormReceived_ByDate", obj);

        }

        /// <summary>
        /// Added by Kavita :22-06-2016
        /// Overloaded function FMS_Get_FormReceived_ByDate_DL
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
        /// <param name="FormType">Selected Form Type</param>
        /// <returns>Returns Form Details according to the selection parameters</returns>
        public DataSet FMS_Get_FormReceived_ByDate_DL(string Period, string Mode, int? IsApproved, int? ApproverID, int? VesselAssignUserID, int? FleetID, DataTable dtVessel, int? StatusID, int? DepartmentID, string SearchBy, int? FormType)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@Period", Period),
             new System.Data.SqlClient.SqlParameter("@Mode", Mode),
             new System.Data.SqlClient.SqlParameter("@IsApproved",IsApproved),
             new System.Data.SqlClient.SqlParameter("@ApproverID",ApproverID),
             new System.Data.SqlClient.SqlParameter("@VesselAssignUserID",VesselAssignUserID),
             new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
             new System.Data.SqlClient.SqlParameter("@VESSELID",dtVessel),
             new System.Data.SqlClient.SqlParameter("@StatusID",StatusID),
             new System.Data.SqlClient.SqlParameter("@DepartmentID",DepartmentID),
             new System.Data.SqlClient.SqlParameter("@SearchBy",SearchBy),
             new System.Data.SqlClient.SqlParameter("@FormType",FormType)
             };

                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_FormReceived_ByDate", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Added by Kavita : 22-06-2016
        /// Retrive status
        /// </summary>
        /// <returns>returns status</returns>
        public DataTable FMS_Get_StatusList_DL()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_StatusList").Tables[0];
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
                SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@documentID",_documentID),
											new SqlParameter("@UserID",_userID),
											
										};

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Update_FileFolder_UnArchive", sqlprm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Added by Anjali DT:24-06-2016
        /// </summary>
        /// <param name="_mailID"> Mail id where mail is to be sent.</param>
        /// <param name="_userID">Id of user who created /modified mail id.</param>
        public void SaveMailID(string _mailID, int _userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@MailID",_mailID),
											new SqlParameter("@USERID",_userID),
											
										};

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_Insert_Update_MailID", sqlprm);
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
                return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "FMS_GET_MAILID"));
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
                try
                {
                    SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@documentID",_documentID),
											new SqlParameter("@UserID",_userID),
											new SqlParameter("@forwardAttchment",_forwardAttchment),
											
										};

                    SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FMS_SaveForwardAttchmentToForms", sqlprm);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Added by anjali DT:06-07-2016.
        /// When schedule status is 'Re-schedule' and 'Un-assigned' then 
        /// </summary>
        /// <param name="Status_ID"></param>
        /// <returns></returns>
        public DataTable  FMS_Get_Reschedule_FormInfo(int Status_ID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@Status_ID",Status_ID),
                                        };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_Reschedule_FormInfo", sqlprm).Tables[0];
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
                    SqlParameter[] sqlprm = new SqlParameter[]
										{
											new SqlParameter("@DocumentID",DocumentID),
											new SqlParameter("@ParenID",ParenID),
                                          
                                        };
             
                     return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_Exits_Folder", sqlprm).Tables[0];

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

     
    }
}
