using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SMS.Data.QMS;



namespace SMS.Business.QMS
{
    public class BLL_SCM_Report
    {
        public static DataSet SCMReportMainSearch(int? fleetcode, int? vesselid, DateTime? fromdate, DateTime? todate
                 , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_SCM_Report.SCMReportMainSearch(fleetcode, vesselid, fromdate, todate, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataSet SCMReportList(int? id, int vesselid)
        {
            return DAL_SCM_Report.SCMReportList(id, vesselid);
        }

        public static DataSet SCMReportDetailsSearch(int meetingid, int current_tab, int vesselid)
        {
            return DAL_SCM_Report.SCMReportDetailsSearch(meetingid, current_tab, vesselid);
        }

        public static DataTable SCMGetDrillAttachment(int? DrillID, int? VesselID)
        {
            return DAL_SCM_Report.SCMGetDrillAttachment(DrillID, VesselID);
        }

        public static DataTable SCMReportGetOfficeDepartment(int? companyid)
        {
            return DAL_SCM_Report.SCMReportGetOfficeDepartment(companyid);
        }


        public static DataTable SCMReportGetOfficeRepresentativeDept()
        {
            return DAL_SCM_Report.SCMReportGetOfficeRepresentativeDept();
        }


        public static int SCMReportOfficeIssueAssigned(int? userid, int? deptid, string tabname, int? responseid, int? tabpkid, int? vesselid, int? link)
        {
            return DAL_SCM_Report.SCMReportOfficeIssueAssigned(userid, deptid, tabname, responseid, tabpkid, vesselid, link);
        }


        public static int SCMReportOfficeResponseSave(int? userid, int? responseid, string officeresponse)
        {
            return DAL_SCM_Report.SCMReportOfficeResponseSave(userid, responseid, officeresponse);
        }

        public static int SCMReportOfficeDepartmentUpdate(int? userid, int? responseid, int? deptid)
        {

            return DAL_SCM_Report.SCMReportOfficeDepartmentUpdate(userid, responseid, deptid);
        }


        public static DataSet SCMReportOfficeResponseSearch(int? fleetcode, int? vesselid, int? userid, int? deptid, int? year, int? month, string searchtext, int? smsnextreview, int? responsestatus
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_SCM_Report.SCMReportOfficeResponseSearch(fleetcode, vesselid, userid, deptid, year, month, searchtext, smsnextreview, responsestatus
                , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }



        public static DataSet SCMReportIssueAssignmentSearch(int? fleetcode, int? vesselid, int? userid, int? deptid, int? year, int? month, string searchtext
             , int? assignestatus, int? smsreviewstatus
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_SCM_Report.SCMReportIssueAssignmentSearch(fleetcode, vesselid, userid, deptid, year, month, searchtext, assignestatus, smsreviewstatus
                , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }



        public static DataTable SCMReportGetEmailAddToSendMailForResponse(int? year, int? month)
        {
            return DAL_SCM_Report.SCMReportGetEmailAddToSendMailForResponse(year, month);
        }

        public static DataTable GetVesselIssuesYears()
        {

            return DAL_SCM_Report.GetVesselIssuesYears();
        }

        public static DataTable GetSCMReportResponseRelease()
        {

            return DAL_SCM_Report.GetSCMReportResponseRelease();
        }


        public static int SCMReportRealeaseResponseToShip(int? userid, int? year, int? month)
        {
            return DAL_SCM_Report.SCMReportRealeaseResponseToShip(userid, year, month);
        }


        public static int SCMReportSMSReviewFlagUpdate(int? responseid, int? sms_review_flag)
        {
            return DAL_SCM_Report.SCMReportSMSReviewFlagUpdate(responseid, sms_review_flag);
        }

        public static DataSet SCMOfficeRepresentativeSearch(int? deptid, int? deptname, int? offresemail, int? sortorder)
        {
            return DAL_SCM_Report.SCMOfficeRepresentativeSearch(deptid, deptname, offresemail, sortorder);
        }


        public static int SCMOfficeRepresentativeSave(int? deptid, string emailadd)
        {
            return DAL_SCM_Report.SCMOfficeRepresentativeSave(deptid, emailadd);
        }


        public static int SCMOfficeRepresentativeDelete(int? deptid, int? scm_reps_id)
        {
            return DAL_SCM_Report.SCMOfficeRepresentativeDelete(deptid, scm_reps_id);
        }



        public static DataTable SCMGetOfficeDepartment(int? companyid)
        {
            return DAL_SCM_Report.SCMGetOfficeDepartment(companyid);
        }


        public static DataTable SCMGetVesselDrillReports(int? fleetcode, int? vesselid, DateTime? fromdate, DateTime? todate, int? drilltype)
        {
            return DAL_SCM_Report.SCMGetVesselDrillReports(fleetcode, vesselid, fromdate, todate, drilltype);
        }


        public static DataTable SCMGetDrillTYPE()
        {
            return DAL_SCM_Report.SCMGetDrillTYPE();
        }
        public static void ScmVerifyReport(int? ScmId, string Remarks, int? Vessel_Id, int? UserId)
        {
            DAL_SCM_Report.ScmVerifyReport(ScmId, Remarks, Vessel_Id, UserId);
        }


        public static DataSet Get_Absentees_Report(DateTime? todate, int? Vessel_Id, int? FleetId)
        {
            return DAL_SCM_Report.Get_Absentees_Report(todate, Vessel_Id, Vessel_Id);
        }
    }
}
