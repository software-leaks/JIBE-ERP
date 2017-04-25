using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.QMSDB;

namespace SMS.Business.QMSDB
{
    public class BLL_QMSDB_ProcedureSection
    {
        //public static DataTable QMSDBProcedureSection_Search(int? FleetId, int? VesselId, int? DepartmentId, int? UserId)
        //{
        //    return DAL_QMSDB_ProcedureSection.QMSDBProcedureSection_Search(FleetId, VesselId, DepartmentId, UserId);
        //}
        public static DataTable QMSDBProcedureSection_List(int? procedureid)
        {
            return DAL_QMSDB_ProcedureSection.QMSDBProcedureSection_List(procedureid);
        }
        public static DataTable QMSDBProcedureSection_Edit(int SectionId, int procedureid)
        {
            return DAL_QMSDB_ProcedureSection.QMSDBProcedureSection_Edit(SectionId, procedureid);
        }

        public static DataTable Get_Section_Details(int Section_ID)
        {
           return DAL_QMSDB_ProcedureSection.Get_Section_Details(Section_ID);
        }

        public static DataTable Get_All_Sections(int Procedure_ID,string Section_Type=null)
        {
            return DAL_QMSDB_ProcedureSection.Get_All_Sections(Procedure_ID, Section_Type);
        }

        public static int QMSDBProcedureSection_Update(int? sectionid, int? procedureid, string sectionname, string sectionheader, string details, string  checkoutdetails, int? checkstatus, int section_order, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_ProcedureSection.QMSDBProcedureSection_Update(sectionid,procedureid, sectionname, sectionheader, details, checkoutdetails, checkstatus,section_order, ActiveStatus, UserId);
        }

        public static int Upd_Section_Details(int Section_ID, string Section_Details, int UserId)
        {
            return DAL_QMSDB_ProcedureSection.Upd_Section_Details(Section_ID, Section_Details, UserId);
        }

        public static int QMSDBProcedureSection_Insert(int? procedureid, string sectionname, string sectionheader, string details, int? checkoutdetails, int? checkstatus, int section_order, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_ProcedureSection.QMSDBProcedureSection_Insert(procedureid, sectionname, sectionheader, details, checkoutdetails, checkstatus,section_order, ActiveStatus, UserId);
        }
        public static int QMSDBProcedureSection_Update_Details(int? sectionid, int? procedureid, string checkoutdetails, int? UserId)
        {
            return DAL_QMSDB_ProcedureSection.QMSDBProcedureSection_Update_Details(sectionid, procedureid,  checkoutdetails, UserId);
        }


        public static int Upd_All_Sections(int Procedure_ID, DataTable tbl_Section, int UserID, string Procedure_Code=null, string Procedure_Name=null)
        {
            return DAL_QMSDB_ProcedureSection.Upd_All_Sections(Procedure_ID, tbl_Section, UserID, Procedure_Code, Procedure_Name);

        }

        public static int Upd_Delete_Section(int Section_ID,int UserID)
        {
           return DAL_QMSDB_ProcedureSection.Upd_Delete_Section(Section_ID,UserID);
        }


        public static DataTable Upd_Publish_Procedure(int Procedure_ID, int UserID,string comments,ref int Result)
        {
            return DAL_QMSDB_ProcedureSection.Upd_Publish_Procedure(Procedure_ID, UserID,comments, ref Result);
        }


        public static int Ins_Procedure_Attachment(int Procedure_ID, string Attachment_Name, int UserID)
        {
            return DAL_QMSDB_ProcedureSection.Ins_Procedure_Attachment(Procedure_ID, Attachment_Name, UserID);

        }

        public static int Ins_Procedure_File(int ID, string File_Name, string ContentType, string FileData)
        {
             return DAL_QMSDB_ProcedureSection.Ins_Procedure_File(ID,  File_Name,  ContentType,FileData);
        }

        public static DataTable Get_RankList_Search(string SearchText, int? Rank_ID, int? Rank_Category, int? FolderID
                        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_QMSDB_ProcedureSection.Get_RankList_Search(SearchText, Rank_ID, Rank_Category,FolderID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public static int InsertReadMandatory(int? FOLDER_ID, int? RANK_ID, int? USER_ID, int? READ_ACCESS)
        {
            return DAL_QMSDB_ProcedureSection.InsertReadMandatory(FOLDER_ID, RANK_ID, USER_ID, READ_ACCESS);
        }

        public static int DeleteReadMandatory(int? FOLDER_ID, int? RANK_ID, int? USER_ID)
        {

            return DAL_QMSDB_ProcedureSection.DeleteReadMandatory(FOLDER_ID, RANK_ID, USER_ID);

        }
    }
}
