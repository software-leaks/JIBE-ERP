using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.QMSDB;

namespace SMS.Business.QMSDB
{
    public class BLL_QMSDB_Folders
    {
        public static DataTable QMSDBFoldes_Search(int? FleetId, int? VesselId, int? DepartmentId, int? UserId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_Search(FleetId, VesselId, DepartmentId, UserId);
        }
        public static DataTable QMSDBFoldes_List(int? FleetId, int? VesselId, int? DepartmentId, int? UserId, int? FolderId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_List(FleetId, VesselId, DepartmentId, UserId, FolderId);
        }
        public static DataTable QMSDBFoldes_ProcedureList(int? FleetId, int? VesselId, int? DepartmentId, int? UserId, int? FolderId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_ProcedureList(FleetId, VesselId, DepartmentId, UserId, FolderId);
        }
        public static DataTable QMSDBFoldes_Edit(int FolderId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_Edit(FolderId);
        }
        public static int QMSDBFoldes_Update(int FolderId, int? ParentFolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_Update(FolderId, ParentFolderId, FolderName, ActiveStatus, UserId);
        }
        public static int Upd_QMSDBFoldes_Rename(int FolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_Folders.Upd_QMSDBFoldes_Rename(FolderId, FolderName, ActiveStatus, UserId);
        }
        public static int QMSDBFoldes_Insert(int? ParentFolderId, string FolderName, int ActiveStatus, int? UserId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_Insert(ParentFolderId, FolderName, ActiveStatus, UserId);
        }
        public static DataTable QMSDBFoldes_VesselAccess(DataTable FleetId, DataTable VesselId, int? FolderId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_VesselAccess(FleetId, VesselId, FolderId, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable QMSDBFoldes_UserAccess(int? DepartmentId, int? UserId, int? FolderId, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_UserAccess(DepartmentId, UserId, FolderId, pagenumber, pagesize, ref isfetchcount);
        }
        public static int QMSDBFoldes_Insert_Vessel(int? FleetId, int? VesselId, int? FolderId, int? UserId, int vessel_access)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_Insert_Vessel(FleetId, VesselId, FolderId, UserId, vessel_access);
        }
        public static int QMSDBFoldes_Insert_UserAccess(int? DepartmentId, int? AccesUserId, int? FolderId, int CanView, int CanAdd, int CanEdit, int CanDelete, int? UserId)
        {
            return DAL_QMSDB_Folders.QMSDBFoldes_Insert_UserAccess(DepartmentId, AccesUserId, FolderId, CanView, CanAdd, CanEdit, CanDelete, UserId);
        }

        public static DataTable QMSDBHeaderList()
        {
            return DAL_QMSDB_Folders.QMSDBHeaderList();
        }
    }
}
