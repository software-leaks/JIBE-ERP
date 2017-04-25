using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;

namespace SMS.Data.QMS
{
    public class DAL_SCM_Report
    {

        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public static DataSet SCMReportMainSearch(int? fleetcode, int? vesselid, DateTime? fromdate, DateTime? todate
             , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
              
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_REPORT_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        public static DataSet SCMReportList(int? id, int vesselid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", id) ,
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_REPORT_LIST", obj);
            return ds;
        }


        public static DataSet SCMReportDetailsSearch(int meetingid, int current_tab, int vesselid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@MEETINGID", meetingid),
                   new System.Data.SqlClient.SqlParameter("@CURRENTTAB", current_tab),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_REPORT_DETAILS_SEARCH", obj);
            return ds;

        }

        public static DataTable SCMGetDrillAttachment(int? DrillID, int? VesselID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@DRILLID", DrillID),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_DRILL_ATTACHMENT", obj).Tables[0];

        }


        public static DataTable SCMReportGetOfficeDepartment(int? companyid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            new System.Data.SqlClient.SqlParameter("@COMPANYID", companyid),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_OFFICE_DEPT", obj);

            return ds.Tables[0];
        }




        public static DataTable SCMReportGetOfficeRepresentativeDept()
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_OFFICE_REPRESENTATIVE_DEPT");

            return ds.Tables[0];
        }




        public static int SCMReportOfficeIssueAssigned(int? userid, int? deptid, string tabname, int? responseid, int? tabpkid, int? vesselid, int? link)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid),
                new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                new System.Data.SqlClient.SqlParameter("@TAB_NAME", tabname),
                new System.Data.SqlClient.SqlParameter("@RESPONSEID", responseid),

                new System.Data.SqlClient.SqlParameter("@TABPKID", tabpkid),
                new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                new System.Data.SqlClient.SqlParameter("@LINK", link),
 
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_ISSUE_ASSIGNED", obj);
        }



        public static int SCMReportOfficeResponseSave(int? userid, int? responseid, string officeresponse)
        {



            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid),
                new System.Data.SqlClient.SqlParameter("@RESPONSEID", responseid),
                new System.Data.SqlClient.SqlParameter("@OFFICE_RESPONSE", officeresponse),
               
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_RESPONSE_SAVE", obj);


        }




        public static int SCMReportOfficeDepartmentUpdate(int? userid, int? responseid, int? deptid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid),
                new System.Data.SqlClient.SqlParameter("@RESPONSEID", responseid),
                new System.Data.SqlClient.SqlParameter("@DeptID" , deptid)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_DEPARTMENT_UPDATE", obj);

        }



        public static DataSet SCMReportOfficeResponseSearch(int? fleetcode, int? vesselid, int? userid, int? deptid, int? year, int? month, string searchtext, int? smsnextreview, int? responsestatus
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
               
                new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                new System.Data.SqlClient.SqlParameter("@USERID", userid),
                new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                new System.Data.SqlClient.SqlParameter("@YEAR", year),
                new System.Data.SqlClient.SqlParameter("@MONTH", month),
                new System.Data.SqlClient.SqlParameter("@SEARCH_TEXT", searchtext),

                new System.Data.SqlClient.SqlParameter("@RESPONSE_STATUS", responsestatus),
                new System.Data.SqlClient.SqlParameter("@SMS_REVIEW_STATUS", smsnextreview),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
   
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_RESPONSE_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }



        public static DataSet SCMReportIssueAssignmentSearch(int? fleetcode, int? vesselid, int? userid, int? deptid, int? year, int? month, string searchtext
            , int? assignestatus, int? smsreviewstatus
       , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
               
                new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                new System.Data.SqlClient.SqlParameter("@USERID", userid),
                new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                new System.Data.SqlClient.SqlParameter("@YEAR", year),
                new System.Data.SqlClient.SqlParameter("@MONTH", month),
                new System.Data.SqlClient.SqlParameter("@SEARCH_TEXT", searchtext),
                new System.Data.SqlClient.SqlParameter("@ASSIGN_STATUS", assignestatus),
                new System.Data.SqlClient.SqlParameter("@SMS_REVIEW_STATUS", smsreviewstatus),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
   
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_ISSUE_ASSIGNMENT_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }




        public static DataTable SCMReportGetEmailAddToSendMailForResponse(int? year, int? month)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@YEAR", year),
                new System.Data.SqlClient.SqlParameter("@MONTH", month),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_SEND_EMAIL_TO_UPDATE_RESPONSE", obj);
            dt = ds.Tables[0];
            return dt;
        }



        public static DataTable GetVesselIssuesYears()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_RESPONSE_YEARS");
            dt = ds.Tables[0];
            return dt;
        }


        public static DataTable GetSCMReportResponseRelease()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_RESPONSE_RELEASE");
            dt = ds.Tables[0];
            return dt;
        }

        public static int SCMReportRealeaseResponseToShip(int? userid, int? year, int? month)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid),
                new System.Data.SqlClient.SqlParameter("@YEAR", year),
                new System.Data.SqlClient.SqlParameter("@MONTH", month),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_REALEASE_RESPONSE_TO_SHIP", obj);

        }


        public static int SCMReportSMSReviewFlagUpdate(int? responseid, int? sms_review_flag)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RESPONSEID", responseid),
                new System.Data.SqlClient.SqlParameter("@SMS_REVIEW_FLAG", sms_review_flag),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_SMS_REVIEW_FLAGE_UPDATE", obj);

        }


        public static DataSet SCMOfficeRepresentativeSearch(int? deptid, int? deptname, int? offresemail, int? sortorder)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                new System.Data.SqlClient.SqlParameter("@DEPTNAME", deptname),
                new System.Data.SqlClient.SqlParameter("@REPRE_EMAIL", offresemail),
                new System.Data.SqlClient.SqlParameter("@SORT_ORDER", sortorder),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_REPRESENTATIVE_SEARCH", obj);
            return ds;
        }



        public static int SCMOfficeRepresentativeSave(int? deptid, string emailadd)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                new System.Data.SqlClient.SqlParameter("@EMAIL_ADD", emailadd),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_REPRESENTATIVE_SAVE", obj);

        }


        public static int SCMOfficeRepresentativeDelete(int? deptid, int? scm_reps_id)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                new System.Data.SqlClient.SqlParameter("@SCM_REPS_ID", scm_reps_id),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_PR_OFFICE_REPRESENTATIVE_DELETE", obj);

        }




        public static DataTable SCMGetOfficeDepartment(int? companyid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
            new System.Data.SqlClient.SqlParameter("@COMPANYID", companyid),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_OFFICE_DEPT", obj);

            return ds.Tables[0];
        }




        public static DataTable SCMGetVesselDrillReports(int? fleetcode, int? vesselid, DateTime? fromdate, DateTime? todate, int? drilltype)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
              new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
              new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
              new System.Data.SqlClient.SqlParameter("@TODATE", todate),
              new System.Data.SqlClient.SqlParameter("@DRILLTYPE", drilltype),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_VESSELS_DRILL_REPORT", obj);

            return ds.Tables[0];
        }

        public static DataTable SCMGetDrillTYPE()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_PR_GET_DRILL_TYPE").Tables[0];

        }
        public static void ScmVerifyReport(int? ScmId, string Remarks, int? Vessel_Id, int? UserId)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@ScmId", ScmId),
              new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
              new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id),
              new System.Data.SqlClient.SqlParameter("@UserId", UserId), 
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SCM_VERIFY_REPORT", obj);

        }
        public static DataSet Get_Absentees_Report(DateTime? todate, int? Vessel_Id, int? FleetId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@To_Date", todate),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id),
                   new System.Data.SqlClient.SqlParameter("@FleetI_d", FleetId),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SCM_Get_Absentees_Report", obj);
            return ds;

        }
    }

}
