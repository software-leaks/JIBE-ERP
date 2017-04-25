using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;


/// <summary>
/// Summary description for FileAttachement
/// </summary>
/// 

namespace SMS.Business.PURC
{
    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_FileAttachement objFileAttachment = new DAL_PURC_FileAttachement();


        public DataTable GetAttachedFileInfo(string VesselCode)
        {

            return objFileAttachment.GetAttachedFileInfo(VesselCode);

        }

        public DataTable GetAttachedFileInfo(int? fleetcode, int? ddlvessel, string search, string category, int? supplier
            ,string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objFileAttachment.GetAttachedFileInfo(fleetcode, ddlvessel, search, category, supplier,
                sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public int SaveAttachedFileInfo(string VesselCode, string ReqCode, string suppCode, string FileType, string FileName, string FilePath, string CreatedBy, int Port)
        {

            return objFileAttachment.SaveAttachedFileInfo(VesselCode, ReqCode, suppCode, FileType, FileName, FilePath, CreatedBy, Port);

        }

        public int SaveAttachedFileInfo_New(string VesselCode, string DocCode, string suppCode, string FileType, string FileName, string FilePath, string CreatedBy, int Port)
        {

            return objFileAttachment.SaveAttachedFileInfo_New(VesselCode, DocCode, suppCode, FileType, FileName, FilePath, CreatedBy, Port);

        }
        public DataTable Purc_Get_Reqsn_Attachments(string ReqCode, int VesselID)
        {
            return objFileAttachment.Purc_Get_Reqsn_Attachments_DL(ReqCode, VesselID);
        }

        public DataTable Purc_Get_Reqsn_Attachments_New(string DocCode, int VesselID)
        {
            return objFileAttachment.Purc_Get_Reqsn_Attachments_DL_New(DocCode, VesselID);
        }

        // remove this method
        public int Purc_Delete_Reqsn_Attachments(int ID)
        {
            return objFileAttachment.Purc_Delete_Reqsn_Attachments_DL(ID, 0, 0);
        }

        public int Purc_Delete_Reqsn_Attachments(int ID, int Office_ID, int Vessel_ID)
        {
            return objFileAttachment.Purc_Delete_Reqsn_Attachments_DL(ID,  Office_ID,  Vessel_ID);
        }
        public DataTable Purc_Get_Reqsn_Attachments_Supplier(string Reqsnno, int VesselID,string SuppCode)
        {
            return objFileAttachment.Purc_Get_Reqsn_Attachments_Supplier_DL(Reqsnno, VesselID, SuppCode);
        }      

    }
}
