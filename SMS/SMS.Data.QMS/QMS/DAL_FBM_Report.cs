using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SMS.Data.QMS
{
    public class DAL_FBM_Report
    {

        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public static DataTable GetFBMYears()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_GET_YEARS");
            dt = ds.Tables[0];
            return dt;
        }



        public static DataSet FBMReportList(int? id)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", id) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_GET_FBM_REPORT_LIST", obj);
            return ds;
        }

        public static DataTable GetCountFBMOnCurrenUserDepartment(int? deptid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@DEPTID", deptid) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_GET_COUNT_FBM_ON_CURRENT_USER_DEPARTMENT", obj);
            dt = ds.Tables[0];
            return dt;
        }


        public static DataTable GetFBMUserApprovalList(int? userid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CURRENTUSER", userid) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_GET_APPROVAL_USER_LIST", obj);
            dt = ds.Tables[0];
            return dt;
        }


        public static DataTable GetOfficeDepartment(int? companyid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            new System.Data.SqlClient.SqlParameter("@COMPANYID", companyid),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_GET_OFFICE_DEPT", obj);

            return ds.Tables[0];
        }

        public static DataTable FBMGetSystemParameterList(string parenttypecode, string searchtext,int? deptid)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_CODE", parenttypecode), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
                   new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_GETSYSTEM_PARAMETER_LIST", obj);
            dt = ds.Tables[0];
            return dt;

        }


        public static int FBMMessageSave(int? fbmid, int? userid, string subject, int? department, string foruser, string body, int? tosync, int? active
           , int? primarycategory, int? secondrycategory, int? urgent,string fbmstatus, ref int fbmidout)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FMBID", fbmid ),  
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SUBJECT", subject),
                   new System.Data.SqlClient.SqlParameter("@DEPARTMENT", department),
                   new System.Data.SqlClient.SqlParameter("@FOR_USER", foruser),
                   new System.Data.SqlClient.SqlParameter("@BODY",body),
                   new System.Data.SqlClient.SqlParameter("@ACTIVE",active),
                   new System.Data.SqlClient.SqlParameter("@PRIMARY_CATEGORY",primarycategory),
                   new System.Data.SqlClient.SqlParameter("@SECONDRY_CATEGORY",secondrycategory),
                   new System.Data.SqlClient.SqlParameter("@URGENT",urgent ),
                   new System.Data.SqlClient.SqlParameter("@STATUS",fbmstatus ),
                   new System.Data.SqlClient.SqlParameter("@FBM_ID",SqlDbType.Int),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_MESSAGE_SAVE", obj);
            fbmidout = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;

        }


        public static int FBMMessageSent(int? fbmid, int? userid, string subject, int? department, string foruser, string body, int? tosync, int? active
         , int? primarycategory, int? secondrycategory, int? urgent,string fbmstatus,int? approvarid, ref int fbmidout)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FBMID", fbmid ),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SUBJECT", subject),
                   new System.Data.SqlClient.SqlParameter("@DEPARTMENT", department),
                   new System.Data.SqlClient.SqlParameter("@FOR_USER", foruser),
                   new System.Data.SqlClient.SqlParameter("@BODY",body),
                   new System.Data.SqlClient.SqlParameter("@ACTIVE",active),
                   new System.Data.SqlClient.SqlParameter("@PRIMARY_CATEGORY",primarycategory),
                   new System.Data.SqlClient.SqlParameter("@SECONDRY_CATEGORY",secondrycategory),
                   new System.Data.SqlClient.SqlParameter("@URGENT",urgent ),
                   new System.Data.SqlClient.SqlParameter("@STATUS",fbmstatus ),
                   new System.Data.SqlClient.SqlParameter("@APPROVARID",approvarid ),
                   new System.Data.SqlClient.SqlParameter("@FBM_ID",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_MESSAGE_SENT", obj);
            fbmidout = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;

        }


        public static int FBMMessageApproved(int? fbmid, int? userid, string fbmstatus)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FBMID", fbmid ),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@STATUS",fbmstatus )

            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_MESSAGE_APPROVED", obj);
        }



        public static int FBMMessageRework(int? fbmid, string fbmstatus)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FBMID", fbmid ),
                   new System.Data.SqlClient.SqlParameter("@STATUS",fbmstatus )
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_MESSAGE_REWORK", obj);
        }


        public static DataSet FBMMessageSearch(int? userid, string fbmnumber, int? Department, string foruser,
            int? active, int? primarycategory, int? secondcategory, string year, DateTime? fromdate, DateTime? todate,
            string searchtext,string isdeptsarchonsent,string msgtype,
          string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@FBMNUMBER", fbmnumber),
                   new System.Data.SqlClient.SqlParameter("@DEPARTMENT", Department),
                   new System.Data.SqlClient.SqlParameter("@FORUSER", foruser),
                   new System.Data.SqlClient.SqlParameter("@ACTIVE",active),
                   new System.Data.SqlClient.SqlParameter("@PRIMARYCATEGORY",primarycategory),
                   new System.Data.SqlClient.SqlParameter("@SECONDRYCATEGORY",secondcategory),
                   new System.Data.SqlClient.SqlParameter("@YEAR",year),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXTBY", searchtext),
                   new System.Data.SqlClient.SqlParameter("@ISDEPTSARCHONSENT", isdeptsarchonsent),
                   new System.Data.SqlClient.SqlParameter("@MSGTYPE" ,msgtype),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_FBMMessage_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }



        public static int FBMMessageInActive(int? userid, int? fbmid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@FBMID", fbmid )
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_MESSAGE_INACTIVE", obj);
        }



        public static DataTable FBMAttachmentSearch(int? fbmid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FBMID", fbmid ) 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_ATTACHMENT_SEARCH" ,obj).Tables[0];
        }


        public static int FBMAttachmentSave(int? userid, int? fbmid, string attachmentname, string attachmentpath)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@FBMID", fbmid),
                   new System.Data.SqlClient.SqlParameter("@ATTACHMENT_NAME", attachmentname),
                   new System.Data.SqlClient.SqlParameter("@ATTACHMENT_PATH", attachmentpath),

            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_ATTACHMENT_SAVE", obj);

        }


        public static int FBMAttachmentDelete(int? userid, int? attachmentid)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@ID", attachmentid),
        
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_ATTACHMENT_DELETE", obj);

        }


        public static int FBMCrewMailSave(int userid, string subject, string mailto, string cc, string body, ref int crewfbmidout)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@SUBJECT", subject),
                   new System.Data.SqlClient.SqlParameter("@MAILTO", mailto),
                   new System.Data.SqlClient.SqlParameter("@CC", cc),
                   new System.Data.SqlClient.SqlParameter("@BODY", body),
                   new System.Data.SqlClient.SqlParameter("@CREW_MAX_MAILE_ID",SqlDbType.Int),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_CREW_MAIL_SAVE", obj);
            crewfbmidout = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;
        }
  


        public static int FBMCrewAttachmentSave(int? userid, int? crewmailid, string attachmentname, string attchmentpath)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@USERID", userid),
                  new System.Data.SqlClient.SqlParameter("@CREWMAILID", crewmailid),
                  new System.Data.SqlClient.SqlParameter("@ATTACHMENT_NAME", attachmentname),
                  new System.Data.SqlClient.SqlParameter("@ATTACHMENT_PATH", attchmentpath),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_CREW_MAIL_ATTACHMENT_SAVE", obj);
        }



        public static DataSet FBMReadReportSearch(int? Fleet_ID, int? Vessel_ID, int? FBM_ID, string SearchText,
          int? Rank_ID, int? FBM_READ_STATUS, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@Fleet_ID", Fleet_ID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@FBM_ID", FBM_ID),
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Rank_ID",Rank_ID),
                   new System.Data.SqlClient.SqlParameter("@FBM_READ_STATUS",FBM_READ_STATUS),

                   
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_FBM_READ_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }



        public static DataTable GetFBMList()
        {
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_PR_FBM_List").Tables[0];
            return dt;
        }




        public static DataTable FBM_Get_RankList_Search(string SearchText, int? Rank_ID, int? Rank_Category
                            ,string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Rank_ID", Rank_ID),
                   new System.Data.SqlClient.SqlParameter("@Rank_Category", Rank_Category),
                 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FBM_Get_RankList_Search", obj).Tables[0];
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return dt;
        }

        public static int FBM_READ_ACCESS_RIGHT_SAVE(int? ID, int? RANK_ID, int? USER_ID, int? READ_ACCESS)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@ID", ID),
                  new System.Data.SqlClient.SqlParameter("@RANK_ID", RANK_ID),
                  new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
                  new System.Data.SqlClient.SqlParameter("@READ_ACCESS", READ_ACCESS),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_READ_ACCESS_RIGHT_SAVE", obj);
        }


        public static int FBM_READ_ACCESS_RIGHT_REMOVE(int? ID, int? USER_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                  new System.Data.SqlClient.SqlParameter("@ID", ID),
                  new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "FBM_PR_READ_ACCESS_RIGHT_REMOVE", obj);
        }


    }
}
