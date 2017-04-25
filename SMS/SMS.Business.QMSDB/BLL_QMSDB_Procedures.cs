using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.QMSDB;

namespace SMS.Business.QMSDB
{
    public class BLL_QMSDB_Procedures
    {
        public static DataTable QMSDBProcedures_Search(DataTable FleetId, DataTable VesselId, int? DepartmentId, int? UserId, string filefoldername, string sortexpression, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_Search(FleetId, VesselId, DepartmentId, UserId, filefoldername,sortexpression, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable QMSDBProcedures_List(int? FleetId, int? VesselId, int? DepartmentId, int? UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_List(FleetId, VesselId, DepartmentId, UserId);
        }
        public static DataTable QMSDBProcedures_Edit(int ProcedureId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_Edit(ProcedureId);
        }
        public static int QMSDBProcedures_Update(int ProcedureId, int? Procedure_Code, string Procedure_Name, int FolderId, int WaterMark, int Header_Template, int Footer_Template, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_Update(ProcedureId, Procedure_Code, Procedure_Name, FolderId, WaterMark, Header_Template, Footer_Template, ActiveStatus, UserId);
        }
        public static int QMSDBProcedures_Insert(int? ParentFolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_Insert(ParentFolderId, FolderName, ActiveStatus, UserId);
        }
        public static int QMSDBProcedures_InsertWithSection(string procedure_code, string procedureName, int? FolderId, int iswatermark, int? hedertemplate, int? footertemplate, int? UserId, string sectiondetails)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_InsertWithSection(procedure_code, procedureName, FolderId,iswatermark,hedertemplate,footertemplate, UserId, sectiondetails);
        }
        public static DataTable QMSDBProcedures_History(int ProcedureId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_History(ProcedureId);
        }
        public static int QMSDBProcedure_CheckOUT(int ProcedureId,int UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_CheckOUT(ProcedureId, UserId);
        }
        public static int QMSDBProcedures_SendApprovel(int ProcrdureId, string User_Comments, int? sendto, string filestatus, int? UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedures_SendApprovel(ProcrdureId, User_Comments, sendto, filestatus, UserId);
        }
        public static DataTable QMSDBProcedure_PendingApprovel(int userId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_PendingApprovel(userId, pagenumber, pagesize,ref isfetchcount);
        }
        public static DataTable QMSDBProcedur_VesselAccess(int VesselId, int? FolderId, int?  ProcedureId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_VesselAccess(VesselId, FolderId, ProcedureId, pagenumber, pagesize, ref  isfetchcount);
        }
        public static DataTable QMSDBProcedur_UserAccess(int UserId, int? FolderId, int? ProcedureId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedur_UserAccess(UserId, FolderId, ProcedureId, pagenumber, pagesize, ref  isfetchcount);
        }
        public static int QMSDBProcedure_Insert_UserAccess(int AccUserId, int? ProcedureId, int CanView, int CanAdd, int CanEdit, int CanDelete, int? UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_Insert_UserAccess(AccUserId, ProcedureId, CanView, CanAdd, CanEdit, CanDelete, UserId);
        }
        public static int QMSDBProcedure_Insert_VesselAccess(int vesselid, int? ProcedureId, int UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_Insert_VesselAccess(vesselid, ProcedureId, UserId);
        }
        public static int QMSDBProcedure_Delete_VesselAccess(int vesselid, int? ProcedureId, int UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_Delete_VesselAccess(vesselid, ProcedureId, UserId);
        }
        public static int QMSDBProcedure_Delete_UserAccess(int AccUserId, int? ProcedureId, int UserId)
        {
            return DAL_QMSDB_Procedures.QMSDBProcedure_Delete_UserAccess(AccUserId, ProcedureId, UserId);
        }
        public static int Upd_QMSDBProcedures_Delete(int ProcedureId, int UserId)
        {
            return DAL_QMSDB_Procedures.Upd_QMSDBProcedures_Delete(ProcedureId, UserId);
        }
        public static DataTable ProceduresCheckListSearch(int? folderId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Procedures.ProceduresCheckListSearch(folderId, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable GetProcedureList()
        {
            return DAL_QMSDB_Procedures.GetProcedureList();
        }
        public static DataSet GetReadProcedureSearch(int? Fleet_ID, int? Vessel_ID, int? ProcedureID, string SearchText,
       int? Rank_ID, int? procedure_status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_QMSDB_Procedures.GetReadProcedureSearch(Fleet_ID, Vessel_ID, ProcedureID, SearchText, Rank_ID, procedure_status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
    }
}
