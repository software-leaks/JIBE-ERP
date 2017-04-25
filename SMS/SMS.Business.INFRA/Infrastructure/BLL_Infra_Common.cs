using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Business.Infrastructure
{
   public class BLL_Infra_Common
    {

       public enum Moldule_ID
       {
           CrewMain = 1,
           TravelMain = 2,
           PurchasingMain = 3,
           VesselReports = 5,
           TaskList = 6,
           StandardTasks = 7,
           AssigningCompanyProjects = 8,
           TAsite = 9,
           Daemon = 11,
           AddinInterface = 12,
           Common = 13,
           FBM = 16,
           PurchSuppliers = 17,
           Company = 18,
           UserAccess = 20,
           Worklist = 21,
           UserMenu = 22,
           PortageBillMain = 24,
           Allotment = 25,
           DailyTaskPlanner = 26,
           QMS = 27,
           BunkerSampleAnalysis = 28,
           CrewMailMain = 29,
           LOSampleAnalysis = 30,
           SurveyMain = 31,
           CrewEvaluation = 33,
           MappPurchase = 34,
           PortageBillAttachament = 35,
           CrewBOW = 36,
           Main = 37,
           PMS = 38,
           SCM = 39,
           BulkPurchase = 40,
           ContractPricing = 41,
           LogisticPO = 42,
           EMS = 43,
           DPLMap = 44,
           DashBoard = 45,
           ChartererPartyReport = 46,
           CBAMain = 47,
           VesselSynchronizer = 48,
           CTM = 50
       }


        public enum project_ID
        {
            Crew = 1,
            Travel = 2,
            Purchasing = 3,
            PortageBill = 4,
            Operations = 5,
            TravelModule = 6,
            SPM = 7,
            OutlookAddin = 8,
            Common = 9,
            Quality = 10,
            Infrastructure = 11,
            Technical = 13,
            CrewMailBag = 14,
            SurveyandCertificates = 15,
            JibeMApp = 16,
            MeetingRoombooking = 17,
            CBA = 18,
            Synchronizer = 19
        }

        public static  SqlDataReader Get_SPM_Module_ID(string ScreenName)
        {
            return DAL_Infra_Common.Get_SPM_Module_ID(ScreenName);
        }

        public static DataTable Get_SPM_Module_Stages(int Company_ID)
        {
            return DAL_Infra_Common.Get_SPM_Module_Stages(Company_ID);
        }

       public static int Insert_EmailAttachedFile(int EmailID, string FileName, string FilePath)
        {
            return DAL_Infra_Common.Insert_EmailAttachedFile_DL(EmailID, FileName, FilePath);
        }
        public static DataSet Get_EmailAttachedFile(int EmailID)
        {
            return DAL_Infra_Common.Get_EmailAttachedFile_DL(EmailID);
        }
        public static int Delete_EmailAttachedFile_DL(int ID)
        {
           return DAL_Infra_Common.Delete_EmailAttachedFile_DL(ID);
        }
        public static DataTable SearchDMN_UpdatesErrors(int? VesselID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Infra_Common.SearchDMN_UpdatesErrors(VesselID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable Get_Exception_Search(int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo, string searchtext,  int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Infra_Common.Get_Exception_Search(FleetID, VesselID, dtFrom, dtTo, searchtext, pagenumber, pagesize, ref isfetchcount);
        }

        public static int Upd_User_Vessel_Assignment(int UserID, DataTable tblVessel_ID, int Created_By)
        {
            return DAL_Infra_Common.Upd_User_Vessel_Assignment(UserID, tblVessel_ID, Created_By);
        }
        public static DataTable Get_User_Vessel_Assignment(int UserID, int? FleetId,int? companyID)
        {
            return DAL_Infra_Common.Get_User_Vessel_Assignment(UserID, FleetId, companyID);
        }

    }
}
