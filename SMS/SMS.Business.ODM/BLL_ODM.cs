using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.ODM;


namespace SMS.Business.ODM
{
    public class BLL_ODM
    {

        DAL_ODM objDAL_ODM = new DAL_ODM();
        public int Insert_ODM_Vessel(string ODM_SUBJECT, string MSG_TEXT, int VesselId , int ODM_Department_Id, bool IsallVessels, int Created_By,  ref int  OutGroupId)
        {

            return objDAL_ODM.Insert_ODM_Vessel(ODM_SUBJECT, MSG_TEXT, VesselId, ODM_Department_Id , IsallVessels, Created_By,  ref OutGroupId);
        }
        public int Upd_ODM_Vessel(int ODM_Id, DataTable VesselIds, bool IsallVessels, string ODM_SUBJECT, string MSG_TEXT, int ODM_Department_Id, int Created_By)
        {

            return objDAL_ODM.UPD_Vessel_ODM(ODM_Id, VesselIds, IsallVessels, ODM_SUBJECT, MSG_TEXT,  ODM_Department_Id, Created_By);
        }

        public int Ins_ODM_Attachments(int ODM_Id, string ATTACHMENT_NAME, string ATTACHMENT_PATH, string Attachment_ID, int Sent_Size, int? Created_By)
        {
            return objDAL_ODM.Ins_ODM_Attachments(ODM_Id, ATTACHMENT_NAME, ATTACHMENT_PATH, Attachment_ID, Sent_Size, Created_By);
        }
        public DataTable GET_ODM_Attachments(int? ODM_ID)
        {
            return objDAL_ODM.GET_ODM_Attachments(ODM_ID);
        }

        public DataTable Get_ODM_Vessels(int? Group_ID)
        {
            return objDAL_ODM.Get_ODM_Vessels(Group_ID);
        }

        public DataTable GET_ODM_VesselsAll()
        {
            return objDAL_ODM.GET_ODM_VesselsAll();
        }

        public DataTable GET_GroupVessels(int Group_ID)
        {
            return objDAL_ODM.GET_GroupVessels(Group_ID);
        }
        public int Attachment_Delete(int? ODM_ID, int? Created_By)
        {
            return objDAL_ODM.Attachment_Delete(ODM_ID, Created_By);
        }

        public int ODM_Update_SentStatus(int ODM_Id, int? Created_By)
        {
            return objDAL_ODM.ODM_Update_SentStatus(ODM_Id, Created_By);
        }


        public int Del_Vessel_ODM(int? GroupId, int? Created_By)
        {
            return objDAL_ODM.Del_Vessel_ODM(GroupId, Created_By);
        }
        public DataTable Get_ODM_Departments()
        {
            return objDAL_ODM.Get_ODM_Departments();
        }


        public DataTable GET_ODM_Attachments_PendingList()
        {
            return objDAL_ODM.GET_ODM_Attachments_PendingList();
        }

        public DataTable Get_ODM_QueueList(string searchtext  , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL_ODM.Get_ODM_QueueList(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Search_ODM_History(int? Vessel_Id, string searchtext, DateTime? StartDate, DateTime? EndDate, DataTable dtDepartmentIds, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL_ODM.Search_ODM_History(Vessel_Id, searchtext,  StartDate, EndDate, dtDepartmentIds, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

    }
}
