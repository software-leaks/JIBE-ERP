using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SMS.Data.QMS;

namespace SMS.Business.QMS
{
    public class BLL_FBM_Report
    {


        public static DataTable GetFBMYears()
        {
            return DAL_FBM_Report.GetFBMYears();
        }

        public static DataSet FBMReportList(int? id)
        {
            return DAL_FBM_Report.FBMReportList(id);
        }

        public static DataTable GetCountFBMOnCurrenUserDepartment(int? deptid)
        {
            return DAL_FBM_Report.GetCountFBMOnCurrenUserDepartment(deptid);
        }


        public static DataTable GetFBMUserApprovalList(int? userid)
        {
            return DAL_FBM_Report.GetFBMUserApprovalList(userid);
        }

        public static DataTable  GetOfficeDepartment(int? companyid)
        {
            return DAL_FBM_Report.GetOfficeDepartment(companyid);
        }

        public static DataTable FBMGetSystemParameterList(string parenttypecode, string searchtext,int? deptid)
        {
            return DAL_FBM_Report.FBMGetSystemParameterList(parenttypecode, searchtext, deptid);
        }

        public static int FBMMessageSave(int? fbmid, int? userid, string subject, int? department, string foruser, string body, int? tosync, int? active
          , int? primarycategory, int? secondrycategory, int? urgent, string fbmstatus, ref int fbmidout)
        {

            return DAL_FBM_Report.FBMMessageSave(fbmid, userid, subject, department, foruser, body, tosync, active, primarycategory, secondrycategory, urgent, fbmstatus, ref fbmidout);
        }

        public static int FBMMessageSent(int? fbmid, int? userid, string subject, int? department, string foruser, string body, int? tosync, int? active
       , int? primarycategory, int? secondrycategory, int? urgent, string fbmstatus, int? approvarid, ref int fbmidout)
        {
            return DAL_FBM_Report.FBMMessageSent(fbmid, userid, subject, department, foruser, body, tosync, active, primarycategory, secondrycategory, urgent, fbmstatus, approvarid, ref fbmidout);
        }

        public static int FBMMessageApproved(int? fbmid, int? userid, string fbmstatus)
        {
            return DAL_FBM_Report.FBMMessageApproved(fbmid, userid,fbmstatus);
        }


        public static int FBMMessageRework(int? fbmid, string fbmstatus)
        {
            return DAL_FBM_Report.FBMMessageRework(fbmid,   fbmstatus);
        }


        public static DataSet FBMMessageSearch(int? userid, string fbmnumber, int? Department, string foruser,
        int? active, int? primarycategory, int? secondcategory, string year, DateTime? fromdate, DateTime? todate,
        string searchtext, string isdeptsarchonsent, string  msgtype,
         string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_FBM_Report.FBMMessageSearch(userid, fbmnumber, Department, foruser, active, primarycategory, secondcategory, year, fromdate, todate, searchtext, isdeptsarchonsent, msgtype, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public static int FBMMessageInActive(int? userid, int? fbmid)
        {
            return DAL_FBM_Report.FBMMessageInActive(userid, fbmid);
        }


        public static DataTable FBMAttachmentSearch(int? fbmid)
        {
            return DAL_FBM_Report.FBMAttachmentSearch(fbmid);
        }

        public static int FBMAttachmentSave(int? userid, int? fbmid ,string attachmentname ,string attachmentpath)
        {
            return DAL_FBM_Report.FBMAttachmentSave(userid, fbmid, attachmentname, attachmentpath);
        }


        public static int FBMAttachmentDelete(int? userid, int? attachmentid)
        {

            return DAL_FBM_Report.FBMAttachmentDelete(userid, attachmentid);

        }


        public static int FBMCrewAttachmentSave(int? userid, int? crewmailid, string attachmentname, string attchmentpath)
        {
            return DAL_FBM_Report.FBMCrewAttachmentSave(userid, crewmailid, attachmentname, attchmentpath);
        
        }


        public static int FBMCrewMailSave(int userid,string subject, string mailto,string cc,string body, ref int crewfbmidout)
        {
            return DAL_FBM_Report.FBMCrewMailSave(userid, subject, mailto, cc, body, ref crewfbmidout);
        }


        public static DataSet FBMReadReportSearch(int? Fleet_ID, int? Vessel_ID, int? FBM_ID, string SearchText,
         int? Rank_ID, int? FBM_READ_STATUS ,string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_FBM_Report.FBMReadReportSearch(Fleet_ID, Vessel_ID, FBM_ID, SearchText, Rank_ID, FBM_READ_STATUS, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public static DataTable GetFBMList()
        {
            return DAL_FBM_Report.GetFBMList();
        }

        public static DataTable FBM_Get_RankList_Search(string SearchText, int? Rank_ID, int? Rank_Category
                          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_FBM_Report.FBM_Get_RankList_Search(SearchText, Rank_ID, Rank_Category, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

 
        public static int FBM_READ_ACCESS_RIGHT_SAVE(int? ID, int? RANK_ID, int? USER_ID, int? READ_ACCESS)
        {
            return DAL_FBM_Report.FBM_READ_ACCESS_RIGHT_SAVE(ID, RANK_ID, USER_ID, READ_ACCESS);
        }


        public static int FBM_READ_ACCESS_RIGHT_REMOVE(int? ID, int? USER_ID)
        {

            return DAL_FBM_Report.FBM_READ_ACCESS_RIGHT_REMOVE(ID, USER_ID);
        
        }



    }
}
