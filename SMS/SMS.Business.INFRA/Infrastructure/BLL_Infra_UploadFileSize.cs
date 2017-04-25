using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_UploadFileSize
    {
        DAL_Infra_UploadFileSize objDAL = new DAL_Infra_UploadFileSize();

        public BLL_Infra_UploadFileSize()
        {

        }


        public DataTable Get_Module_FileUpload(string Attach_Prefix)
        {
            return objDAL.Get_Module_FileUpload_DL(Attach_Prefix);
        }
        public DataTable SearchConfigureFileSize(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchConfigureFileSize(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public int InsertConfigureFileSize(string AttachPrefix, int UploadSize, int CreatedBy, int VesselSyncable)
        {
            return objDAL.InsertConfigureFileSize_DL(AttachPrefix, UploadSize, CreatedBy, VesselSyncable);
        }
        public int EditConfigureFileSize(int Rule_ID, string AttachPrefix, int UploadSize, int CreatedBy, int VesselSyncable)
        {
            return objDAL.EditConfigureFileSize_DL(Rule_ID, AttachPrefix, UploadSize, CreatedBy, VesselSyncable);

        }
        public DataTable Get_ConfigurefilesizeList(int? Rule_ID)
        {
            return objDAL.Get_ConfigurefilesizeList_DL(Rule_ID);
        }
        public int DeleteConfigureFileSize(int Rule_ID, int CreatedBy)
        {
            return objDAL.DeleteConfigureFileSize_DL(Rule_ID, CreatedBy);

        }
    }
}