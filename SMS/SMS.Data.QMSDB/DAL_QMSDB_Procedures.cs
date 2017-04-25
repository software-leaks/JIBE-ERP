using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.QMSDB
{
    public class DAL_QMSDB_Procedures
    {
        private static string connection = "";
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            }
        }

        public static DataTable QMSDBProcedures_Search(DataTable FleetId, DataTable VesselId, int? DepartmentId, int? UserId,string filesname, string sortexpression, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENT_ID", DepartmentId) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId),
                  new System.Data.SqlClient.SqlParameter("@FILESNAME", filesname),
                  new System.Data.SqlClient.SqlParameter("@SORTBY",sortexpression),
                  new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                  new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURES_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable QMSDBProcedures_List(int? FleetId, int? VesselId, int? DepartmentId, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@DEPARTMENT_ID", DepartmentId) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)          
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "PRQMS_TESTSEARCH", obj);
            return ds.Tables[0];

        }
        public static DataTable QMSDBProcedures_Edit(int ProcedureId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@ID", ProcedureId)                 
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURES_EDIT", obj);

            return ds.Tables[0];
        }
        public static int QMSDBProcedures_Update(int ProcedureId, int? Procedure_Code, string Procedure_Name, int FolderId, int WaterMark, int Header_Template, int Footer_Template, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_CODE", Procedure_Code) ,   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId) ,
                  new System.Data.SqlClient.SqlParameter("@FILESNAME", Procedure_Name) ,                   
                  new System.Data.SqlClient.SqlParameter("@ISWATERMARK", WaterMark) ,
                  new System.Data.SqlClient.SqlParameter("@HEADERTEMLATE", Header_Template) , 
                  new System.Data.SqlClient.SqlParameter("@FOOTERTEMPLATE", Footer_Template) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) ,
                  new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ActiveStatus)      
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPDATE_PROCEDURES", obj);

        }
        public static int QMSDBProcedures_Insert(int? ParentFolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_CODE", ParentFolderId) ,   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderName) ,
                  new System.Data.SqlClient.SqlParameter("@FILESNAME", ActiveStatus) , 
                  new System.Data.SqlClient.SqlParameter("@ISWATERMARK", ParentFolderId) ,   
                  new System.Data.SqlClient.SqlParameter("@HEDERTEMLATE", FolderName) ,
                  new System.Data.SqlClient.SqlParameter("@FOOTERTEMPLATE", ActiveStatus) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)       
            };

            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_PROCEDURES", obj);
        }

        public static int QMSDBProcedures_InsertWithSection(string PROCEDURE_CODE, string procedureName, int? FolderId, int iswatermark, int? hedertemplate, int? footertemplate, int? UserId, string sectiondetails)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_CODE", PROCEDURE_CODE) ,   
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId) ,
                  new System.Data.SqlClient.SqlParameter("@PROCEDURES_NAME", procedureName) , 
                  new System.Data.SqlClient.SqlParameter("@ISWATERMARK", iswatermark) ,   
                  new System.Data.SqlClient.SqlParameter("@HEDERTEMLATE", hedertemplate) ,
                  new System.Data.SqlClient.SqlParameter("@FOOTERTEMPLATE", footertemplate) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) ,
                  new System.Data.SqlClient.SqlParameter("@SECTIONDETAIL", sectiondetails)  ,
                 
                  new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_PROCEDURES_WITH_SECTION", obj);

            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }


        public static DataTable QMSDBProcedures_History(int ProcedureId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@PROCEDUREID", ProcedureId)                 
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURESHISTORY", obj);

            return ds.Tables[0];
        }
        public static int QMSDBProcedures_SendApprovel(int ProcrdureId, string User_Comments, int? sendto, string filestatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@PROCEDUREID", ProcrdureId) ,   
                  new System.Data.SqlClient.SqlParameter("@USER_COMMENTS", User_Comments) ,
                  new System.Data.SqlClient.SqlParameter("@SENT_TO", sendto) ,
                  new System.Data.SqlClient.SqlParameter("@FILESTATUS", filestatus) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)                  
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_PROCEDURESSTATUS", obj);
        }
        public static int QMSDBProcedure_CheckOUT(int ProcedureId, int userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", userId)    
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPDATE_PROCEDURE_CHECKOUT", obj);
        }
        public static DataTable  QMSDBProcedure_PendingApprovel(int userId,int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                 new System.Data.SqlClient.SqlParameter("@USERID", userId) , 
                 new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount) 
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURE_STATUS_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];           
        }

        public static DataTable QMSDBProcedure_VesselAccess(int VesselId, int? FolderId, int? ProcedureId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId) ,
                   new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,  
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount) 
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_VESSEL_PROCEDURE_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static DataTable QMSDBProcedur_UserAccess(int UserId, int? FolderId, int? ProcedureId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) ,
                  new System.Data.SqlClient.SqlParameter("@FOLDER_ID", FolderId) ,
                   new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,  
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount) 
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_USER_PROCEDURE_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        public static int QMSDBProcedure_Insert_UserAccess(int? AccesUserId, int? ProcedureId, int CanView, int CanAdd, int CanEdit, int CanDelete, int? UserId)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@ACC_USERID", AccesUserId) ,
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId),
                  new System.Data.SqlClient.SqlParameter("@ACCESS_VIEW", CanView), 
                  new System.Data.SqlClient.SqlParameter("@ACCESS_ADD", CanAdd), 
                  new System.Data.SqlClient.SqlParameter("@ACCESS_EDIT", CanEdit),
                  new System.Data.SqlClient.SqlParameter("@ACCESS_DELETE", CanDelete),
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) 
 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_USER_PROCEDURE", obj);

        }
        public static int QMSDBProcedure_Insert_VesselAccess(int? VesselId, int? ProcedureId, int? UserId)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_VESSEL_PROCEDURE", obj);

        }
        public static int QMSDBProcedure_Delete_VesselAccess(int? VesselId, int? ProcedureId, int? UserId)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPD_DELETE_VESSEL_PROCEDURE", obj);


        }
        public static int QMSDBProcedure_Delete_UserAccess(int? AccUserId, int? ProcedureId, int? UserId)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@ACC_USERID", AccUserId) ,
                  new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPD_DELETE_USER_PROCEDURE", obj);


        }

        public static int Upd_QMSDBProcedures_Delete(int ProcedureId, int UserId)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                   new System.Data.SqlClient.SqlParameter("@PROCEDURE_ID", ProcedureId) ,
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId) 
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_Folders.ConnectionString, CommandType.StoredProcedure, "QMS_DB_DEL_PROCEDURES", obj);


        }
        public static DataTable ProceduresCheckListSearch(int? folderId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
          
                  new System.Data.SqlClient.SqlParameter("@PARRENTFOLDERID",folderId),
                  new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                  new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                  new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_GET_CURRECTIONLIST_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable GetProcedureList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURELIST", obj);
            return ds.Tables[0];

        }
        public static DataSet GetReadProcedureSearch(int? Fleet_ID, int? Vessel_ID, int? ProcedureID, string SearchText,
         int? Rank_ID, int? procedure_status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@Fleet_ID", Fleet_ID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@ProcedureID", ProcedureID),
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Rank_ID",Rank_ID),
                   new System.Data.SqlClient.SqlParameter("@PROCEDURE_STATUS",procedure_status),

                   
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_Procedures.ConnectionString, CommandType.StoredProcedure, "QMS_DB_READ_MANDATORY_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
    }
}
