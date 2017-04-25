using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.QMSDB
{
    public class DAL_QMSDB_ProcedureSection
    {
        //private static string connection = "";
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            }
        }

        //public static DataTable QMSDBProcedureSection_Search(int? FleetId, int? VesselId, int? DepartmentId, int? UserId)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
        //    {
        //          new System.Data.SqlClient.SqlParameter("@FLEET_ID", FleetId) ,   
        //          new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselId) ,
        //          new System.Data.SqlClient.SqlParameter("@DEPARTMENT_ID", DepartmentId) ,  
        //          new System.Data.SqlClient.SqlParameter("@USERID", UserId)          
        //    };
        //    System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURESECTIONLIST", obj);
        //    return ds.Tables[0];
        //}
        public static DataTable QMSDBProcedureSection_List(int? procedureid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                  new System.Data.SqlClient.SqlParameter("@PROCEDUREID", procedureid) 
                           
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURESECTIONLIST", obj);
            return ds.Tables[0];

        }
        public static DataTable QMSDBProcedureSection_Edit(int sectionid, int procedureid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@SECTIONID", sectionid) , 
                  new System.Data.SqlClient.SqlParameter("@PROCEDUREID", procedureid)               
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_PROCEDURESECTION_EDIT", obj);

            return ds.Tables[0];
        }


        public static DataTable Get_Section_Details(int Section_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  
                  new System.Data.SqlClient.SqlParameter("@Section_ID", Section_ID)               
                    
            };

            return SqlHelper.ExecuteDataset(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_SECTION_DETAILS", obj).Tables[0];


        }


        public static DataTable Get_All_Sections(int Procedure_ID, string Section_Type = null)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  
                  new System.Data.SqlClient.SqlParameter("@Procedure_ID", Procedure_ID) ,
                  new SqlParameter("@SECTION_TYPE",Section_Type)
                    
            };

            return SqlHelper.ExecuteDataset(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_ALL_SECTIONS", obj).Tables[0];


        }

        public static int QMSDBProcedureSection_Update(int? sectionid, int? procedureid, string sectionname, string sectionheader, string details, string checkoutdetails, int? checkstatus, int section_order, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {     
                  new System.Data.SqlClient.SqlParameter("@SECTION_ID", sectionid) , 
                  new System.Data.SqlClient.SqlParameter("@PROCEDUREID", procedureid) ,   
                  new System.Data.SqlClient.SqlParameter("@SECTIONNAME", sectionname) ,
                  new System.Data.SqlClient.SqlParameter("@SECTIONHEADER", sectionheader) ,  
                  new System.Data.SqlClient.SqlParameter("@DETAILS", details) , 
                  new System.Data.SqlClient.SqlParameter("@CHECKOUTDETAILS", checkoutdetails) ,   
                  new System.Data.SqlClient.SqlParameter("@CHECKSTATUS", checkstatus) ,
                  new System.Data.SqlClient.SqlParameter("@SECTION_ORDER", section_order) ,
                  new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ActiveStatus) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)                            
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPDATE_PROCEDURESECTION", obj);

        }


        public static int Upd_Section_Details(int Section_ID, string Section_Details, int UserId)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {     
                  new System.Data.SqlClient.SqlParameter("@SECTION_ID", Section_ID) , 
                  new System.Data.SqlClient.SqlParameter("@Section_Details", Section_Details) , 
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)                            
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPDATE_SECTION_DETAILS", obj);

        }

        public static int QMSDBProcedureSection_Insert(int? procedureid, string sectionname, string sectionheader, string details, int? checkoutdetails, int? checkstatus, int section_order, int ActiveStatus, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {    
                 new System.Data.SqlClient.SqlParameter("@PROCEDUREID", procedureid) ,   
                  new System.Data.SqlClient.SqlParameter("@SECTIONNAME", sectionname) ,
                  new System.Data.SqlClient.SqlParameter("@SECTIONHEADER", sectionheader) ,  
                  new System.Data.SqlClient.SqlParameter("@DETAILS", details) , 
                  new System.Data.SqlClient.SqlParameter("@CHECKOUTDETAILS", checkoutdetails) ,   
                  new System.Data.SqlClient.SqlParameter("@CHECKSTATUS", checkstatus) ,
                  new System.Data.SqlClient.SqlParameter("@SECTION_ORDER", section_order) ,
                  new System.Data.SqlClient.SqlParameter("@ACTIVE_STATUS", ActiveStatus) ,  
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)       
            };

            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_PROCEDURESECTION", obj);
        }

        public static int QMSDBProcedureSection_Update_Details(int? sectionid, int? procedureid, string checkoutdetails, int? UserId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {     
                  new System.Data.SqlClient.SqlParameter("@SECTION_ID", sectionid) , 
                  new System.Data.SqlClient.SqlParameter("@PROCEDUREID", procedureid) ,   
                  new System.Data.SqlClient.SqlParameter("@CHECKOUTDETAILS", checkoutdetails) ,   
                  new System.Data.SqlClient.SqlParameter("@USERID", UserId)                            
            };
            return SqlHelper.ExecuteNonQuery(DAL_QMSDB_ProcedureSection.ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPDATE_PROCEDURESECTION_DETAILS", obj);

        }


        public static int Upd_All_Sections(int Procedure_ID, DataTable tbl_Section, int UserID, string Procedure_Code=null, string Procedure_Name=null)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
                new SqlParameter("@Procedure_ID",Procedure_ID),
                new SqlParameter("@tbl_Section",tbl_Section),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Procedure_Code",Procedure_Code),
                new SqlParameter("@Procedure_Name",Procedure_Name),
                             
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPD_ALL_SECTIONS", prm);
        }


        public static int Upd_Delete_Section(int Section_ID, int UserID)
        {

            SqlParameter[] prm = new SqlParameter[] 
            {
               
                new SqlParameter("@Section_ID",Section_ID),
                new SqlParameter("@UserID",UserID),
                             
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPD_DELETE_SECTION", prm);
        }

        public static DataTable Upd_Publish_Procedure(int Procedure_ID, int UserID,string comments, ref int Result)
        {
            DataTable dtAttachments = new DataTable();
            SqlParameter[] prm = new SqlParameter[] 
            {
               
                new SqlParameter("@Procedure_ID",Procedure_ID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Comments",comments),
                new SqlParameter("@Return",SqlDbType.Int)
                             
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            dtAttachments= SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "QMS_DB_UPD_PUBLISH_PROCEDURE", prm).Tables[0];
             Result = Convert.ToInt32(prm[prm.Length - 1].Value);
             return dtAttachments;

        }

        public static int Ins_Procedure_Attachment(int Procedure_ID,string Attachment_Name,int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
               
                new SqlParameter("@Procedure_ID",Procedure_ID),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Attachment_Name",Attachment_Name)
                             
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_PROCEDURE_ATTACHMENT", prm);
        }

        public static int Ins_Procedure_File(int ID, string File_Name,string ContentType, string FileData)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
               
                new SqlParameter("@ID",ID),
                new SqlParameter("@FileName",File_Name),
                new SqlParameter("@ContentType",ContentType),
                 new SqlParameter("@FileData",FileData)
                             
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_FileData", prm);
        }

        public static DataTable Get_RankList_Search(string SearchText, int? Rank_ID, int? Rank_Category, int? FolderID
                           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Rank_ID", Rank_ID),
                   new System.Data.SqlClient.SqlParameter("@Rank_Category", Rank_Category),
                   new System.Data.SqlClient.SqlParameter("@FolderID", FolderID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "QMS_DB_GET_RANKMANDATORY_SEARCH", obj).Tables[0];
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return dt;
        }

        public static int InsertReadMandatory(int? FOLDER_ID, int? RANK_ID, int? USER_ID, int? READ_ACCESS)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FOLDER_ID),
                  new System.Data.SqlClient.SqlParameter("@RANK_ID", RANK_ID),
                  new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
                  new System.Data.SqlClient.SqlParameter("@READ_ACCESS", READ_ACCESS),
            };
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "QMS_DB_INS_READMANDATORY", obj);
        }


        public static int DeleteReadMandatory(int? FOLDER_ID, int? RANK_ID, int? USER_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@FOLDERID", FOLDER_ID),
                  new System.Data.SqlClient.SqlParameter("@RANK_ID", RANK_ID),
                  new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
            };
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "QMS_DB_DELETE_READMANDATORY", obj);
        }

    }
}
