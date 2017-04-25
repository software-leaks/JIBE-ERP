using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.QMSDB
{
    public class DAL_QMSDB_Folders
    {
        private static string connection = "";
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            }
        }

        public static DataTable QMSDBFoldes_Search(int? FleetId,int? VesselId,int? DepartmentId, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENT_ID", DepartmentId) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)          
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_FOLDERSLIST", obj);
            return ds.Tables[0];
        }
        public static DataTable QMSDBFoldes_List(int? FleetId, int? VesselId, int? DepartmentId, int? UserId, int? FolderId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENT_ID", DepartmentId) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) , 
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FolderId) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_FOLDERSLIST", obj);
            return ds.Tables[0];

        }
        public static DataTable QMSDBFoldes_ProcedureList(int? FleetId, int? VesselId, int? DepartmentId, int? UserId, int? FolderId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENT_ID", DepartmentId) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) , 
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FolderId) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_FOLDERPROCEDURELIST", obj);
            return ds.Tables[0];

        }

        public static DataTable QMSDBFoldes_Edit(int FolderId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId)                 
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_FOLDERSLIST", obj);

            return ds.Tables[0];
        }
        public static int Upd_QMSDBFoldes_Rename(int FolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDER_NAME", FolderName) ,
                  new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ActiveStatus) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)                        
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPD_FOLDERS_RENAME", obj);

        }
        public static int QMSDBFoldes_Update(int FolderId, int? ParentFolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId) ,
                  new System.Data.SqlClient.SqlParameter("@PARENT_FOLDER_ID", ParentFolderId) ,   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_NAME", FolderName) ,
                  new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ActiveStatus) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)                        
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPDATE_FOLDERS", obj);

        }
        public static int QMSDBFoldes_Insert(int? ParentFolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Parent_FOLDER_ID", ParentFolderId) ,   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_NAME", FolderName) ,
                  new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ActiveStatus) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)       
            };

            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_FOLDERS", obj);
        }

        public static DataTable QMSDBFoldes_VesselAccess(DataTable FleetId, DataTable VesselId, int? FolderId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FolderId), 
                  new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_FOLDER_VESSEL", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
         

        }
        public static DataTable QMSDBFoldes_UserAccess(int? DepartmentId, int? UserId, int? FolderId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENTID", DepartmentId) ,   
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FolderId), 
                 new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_FOLDER_USER_ACCESS", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
       

        }
        public static int QMSDBFoldes_Insert_UserAccess(int? DepartmentId, int? AccesUserId, int? FolderId, int CanView, int CanAdd, int CanEdit, int CanDelete, int? UserId)
        {
         
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENT", DepartmentId) ,   
                  new System.Data.SqlClient.SqlParameter("@ACC_USERID", AccesUserId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId),
                  new System.Data.SqlClient.SqlParameter("@ACCESS_VIEW", CanView), 
                  new System.Data.SqlClient.SqlParameter("@ACCESS_ADD", CanAdd), 
                  new System.Data.SqlClient.SqlParameter("@ACCESS_EDIT", CanEdit),
                  new System.Data.SqlClient.SqlParameter("@ACCESS_DELETE", CanDelete),
                  new System.Data.SqlClient.SqlParameter("@USEID", UserId) 
 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_FOLDER_USER_ACCESS", obj);

        }
        public static int QMSDBFoldes_Insert_Vessel(int? FleetId, int? VesselId, int? FolderId, int? UserId, int vessel_access)
        {           
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FolderId) ,
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ACCESS", vessel_access) ,
                  new System.Data.SqlClient.SqlParameter("@USEID", UserId) 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_FOLDER_VESSEL", obj);


        }
        public static DataTable QMSDBHeaderList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]  {  };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_HEADERLIST", obj);
            return ds.Tables[0];

        }
    }
}
