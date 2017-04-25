using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.TMSA;


namespace SMS.Business.TMSA
{


    public class BLL_TMSA_PI

    {
        DAL_TMSA_PI objDAL = new DAL_TMSA_PI();
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);


        public static int INS_PI_Details(string PI_Name, string PICode, string Interval, string Description, string Measurement_Detail, string Context, string UOM, string DataSource, int MeasuredForSBU, int IncludeBL, int PI_Status, int IsWorkList, int IsInspectionType, int IsVetting, int UserID)
        {
            return DAL_TMSA_PI.INS_PI_Details(PI_Name, PICode, Interval, Description, Measurement_Detail, Context,UOM,DataSource, MeasuredForSBU,IncludeBL,PI_Status,IsWorkList,IsInspectionType,IsVetting, UserID);
        }

        public static int Update_PI(int PI_ID, string PI_Name, string PICode, string Interval, string Description, string Measurement_Detail, string Context, string UOM,string DataSource, int MeasuredForSBU, int IncludeBL,int PI_Status, int IsWorkList,int IsInspectionType,int IsVetting, int UserID)
        {
            return DAL_TMSA_PI.Update_PI(PI_ID, PI_Name, PICode, Interval, Description, Measurement_Detail, Context, UOM, DataSource, MeasuredForSBU, IncludeBL,PI_Status,  IsWorkList, IsInspectionType,IsVetting, UserID);
        }


        /// <summary>
        ///Description: Method to insert setting for worklist PI
        /// </summary>
        /// <param name="Job_Type">Type of job</param>
        /// <param name="Nature_List"> Nature categories</param>
        /// <param name="Primary_List">Primary Categories</param>
        /// <param name="Secondary_List">Secondary Categories</param>
        /// <param name="Minor_List">Minor categories</param>
        /// <returns></returns>
        public static int INS_Worklist_PI_Details(int PI_ID, int Company_ID, string Job_Type, string Assigned_By, string Nature_List, string Primary_List, string Secondary_List, string Minor_List, bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {
            return DAL_TMSA_PI.INS_Worklist_PI_Details(PI_ID, Company_ID, Job_Type, Assigned_By, Nature_List, Primary_List, Secondary_List, Minor_List, Activate_Scheduler, dtEffective,UserID);
        }


        /// <summary>
        ///Description: Method to update setting for worklist PI
        /// </summary>
        /// <param name="Job_Type">Type of job</param>
        /// <param name="Nature_List"> Nature categories</param>
        /// <param name="Primary_List">Primary Categories</param>
        /// <param name="Secondary_List">Secondary Categories</param>
        /// <param name="Minor_List">Minor categories</param>
        /// <returns></returns>
        public static int Update_Worklist_PI(int Worklist_Id, int PI_ID, int Company_ID, string Job_Type, string Assigned_By, string Nature_List, string Primary_List, string Secondary_List, string Minor_List, bool Activate_Scheduler,DateTime? dtEffective, int UserID)
        {
            return DAL_TMSA_PI.Update_Worklist_PI(Worklist_Id, PI_ID, Company_ID, Job_Type,Assigned_By, Nature_List, Primary_List, Secondary_List, Minor_List,Activate_Scheduler,dtEffective, UserID);
        }


        /// <summary>
        ///Description: Method to insert setting for inspection PI
        /// </summary>
        /// <param name="Job_Type">Type inspection</param>
        /// <returns></returns>
        public static int INS_Inspection_PI_Details(int PI_ID, int Company_ID, string Inspection_Type, string Assigned_By,bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {
            return DAL_TMSA_PI.INS_Inspection_PI_Details(PI_ID, Company_ID, Inspection_Type, Assigned_By, Activate_Scheduler, dtEffective, UserID);
        }


        /// <summary>
        ///Description: Method to update setting for inspection PI
        /// </summary>
        /// <param name="Job_Type">Type of inspection</param>
        /// <returns></returns>
        public static int Update_Inspection_PI(int WorkList_PI_ID, int PI_ID, int Company_ID, string Inspection_Type, string Assigned_By, bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {
            return DAL_TMSA_PI.Update_Inspection_PI(WorkList_PI_ID, PI_ID, Company_ID, Inspection_Type, Assigned_By, Activate_Scheduler, dtEffective, UserID);
        }

        /// <summary>
        ///Description: Method to insert setting for vetting  PI
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="Company_ID"></param>
        /// <param name="Vetting_Type">Types of vetting</param>
        /// <param name="Observation_Type">Types of observation</param>
        /// <param name="Observation_Category">Observation category</param>
        /// <param name="Observation_Risk_Level">Risk level</param>

        public static int INS_Vetting_PI_Details(int PI_ID, int Company_ID, string Vetting_Type, string Observation_Type, string Observation_Category, string Observation_Risk_Level, string Assigned_By, bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {
            return DAL_TMSA_PI.INS_Vetting_PI_Details(PI_ID, Company_ID, Vetting_Type, Observation_Type, Observation_Category, Observation_Risk_Level, Assigned_By, Activate_Scheduler, dtEffective, UserID);
        }

        /// <summary>
        ///Description: Method to Update setting for vetting  PI
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="Company_ID"></param>
        /// <param name="Vetting_Type">Types of vetting</param>
        /// <param name="Observation_Type">Types of observation</param>
        /// <param name="Observation_Category">Observation category</param>
        /// <param name="Observation_Risk_Level">Risk level</param>
        public static int Update_Vetting_PI(int WorkList_PI_ID, int PI_ID, int Company_ID, string Vetting_Type, string Observation_Type, string Observation_Category, string Observation_Risk_Level, string Assigned_By, bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {
            return DAL_TMSA_PI.Update_Vetting_PI(WorkList_PI_ID, PI_ID, Company_ID, Vetting_Type, Observation_Type, Observation_Category, Observation_Risk_Level, Assigned_By, Activate_Scheduler, dtEffective, UserID);
        }
        public static DataSet Get_PI_Details(int PI_ID)
        {
            return DAL_TMSA_PI.Get_PI_Details(PI_ID);

        }

        public static DataSet Get_PI_List(String SearchText,string Category ,int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_TMSA_PI.Get_PI_List(SearchText, Category,Page_Index, Page_Size, ref is_Fetch_Count);
        }


        public static DataSet Get_PI_HeadList(int PI_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_TMSA_PI.Get_PI_HeadList(PI_ID, Page_Index, Page_Size, ref is_Fetch_Count);

        }

        public static DataSet Get_PI_Head_Details(int PI_ID, int Detail_ID)
        {
            return DAL_TMSA_PI.Get_PI_Head_Details(PI_ID, Detail_ID);

        }

        public static DataSet Get_All_Vessels(int CompanyId)
        {

            return DAL_TMSA_PI.Get_All_Vessels( CompanyId);

        }

        public static int Insert_PI_Head(int? PI_ID, string ForYear, string QtrMonth, DateTime? Effective_From, DateTime? Effective_To, double? SBU_Value, int UserID)
        {
            return DAL_TMSA_PI.Insert_PI_Head(PI_ID, ForYear, QtrMonth, Effective_From, Effective_To, SBU_Value, UserID);
        }


        public static void Update_PI_Head(int Detail_ID, int? PI_ID, string ForYear, string QtrMonth, DateTime? Effective_From, DateTime? Effective_To, double? SBU_Value,double? Value, int UserID)
        {
            DAL_TMSA_PI.Update_PI_Head(Detail_ID, PI_ID, ForYear, QtrMonth, Effective_From, Effective_To, SBU_Value,Value, UserID);
        }
        public static void INSERT_PI_Detail(int Detail_ID, double? Value, DataTable @dtPIDetails, int? PI_ID, string ForYear, string QtrMonth, DateTime? Effective_From, DateTime? Effective_To, double? SBU_Value, int UserID, ref int IsExist)
        {
            DAL_TMSA_PI.INSERT_PI_Detail(Detail_ID,   Value, @dtPIDetails,  PI_ID,  ForYear,  QtrMonth,  Effective_From,  Effective_To,  SBU_Value,  UserID, ref IsExist);

        }

        public static DataSet Get_KPI_List(string SearchKPI, int? Page_Index, int? Page_Size, ref int is_Fetch_Count,string Category)
        {
            return DAL_TMSA_PI.Get_KPI_List(SearchKPI, Page_Index, Page_Size, ref is_Fetch_Count,Category);
        }

        public static DataSet Get_PI_ListByKPI(int? KPI_ID)
        {
            return DAL_TMSA_PI.Get_PI_ListByKPI(KPI_ID);
        }
        public static DataSet Get_KPI_Detail(int? KPI_ID)
        {
            return DAL_TMSA_PI.Get_KPI_Detail(KPI_ID);
        }

        public static DataTable Get_KPI_Category()
        {
            return DAL_TMSA_PI.Get_KPI_Category();
        }

        public static int INS_KPI_Details(string KPI_Name, string KPICode, string Interval, string Description, DataTable dtPI, int UserID)
        {
            return DAL_TMSA_PI.INS_KPI_Details(KPI_Name, KPICode, Interval, Description, dtPI, UserID);
        }
        public static DataSet Search_PI_Values(int VID, int KPI_ID, string Startdate, string EndDate)
        {
            return DAL_TMSA_PI.Search_PI_Values(VID, KPI_ID, Startdate, EndDate);
        }

        public static DataSet Search_Voyage_PI_Values(int VID, int KPI_ID, string telId1, string telId2)
        {
            return DAL_TMSA_PI.Search_Voyage_PI_Values(VID, KPI_ID, telId1, telId2);
        }
        public static DataSet Search_PI_ValuesSOX(int VID, string Startdate, string EndDate)
        {

           return DAL_TMSA_PI.Search_PI_ValuesSOX(VID,Startdate, EndDate);
        }

        public static DataSet Search_Voyage_PI_ValuesSOX(int VID, string telId1, string telId2)
        {

            return DAL_TMSA_PI.Search_PI_Voyage_ValuesSOX(VID, telId1, telId2);
        }

        public static DataSet Search_PI_ValuesNOX(int VID, string Startdate, string EndDate)
        {

            return DAL_TMSA_PI.Search_PI_ValuesNOX(VID, Startdate, EndDate);
        }

        public static DataSet Search_Voyage_PI_ValuesNOX(int VID, string telId1, string telId2)
        {

            return DAL_TMSA_PI.Search_PI_Voyage_ValuesNOX(VID, telId1, telId2);
        }

        public static DataSet Get_Vessel_Values(int? Vessel_Id, DateTime? Startdate, DateTime? EndDate, int? PI_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_TMSA_PI.Get_Vessel_Values(Vessel_Id, Startdate, EndDate,PI_ID, Page_Index, Page_Size, ref is_Fetch_Count);
        }
        public static DataSet GetTelDate(string VI_ID,int Vessel_Id)
        {
            return DAL_TMSA_PI.GetTelDate(VI_ID, Vessel_Id);
        }
        public static DataSet GetVoyageData(string TGID, int VID, int KPI_ID)
        {
            return DAL_TMSA_PI.GetVoyageData(TGID, VID, KPI_ID);
        }
        public static DataSet GetVoyageDataSOx(string TGID, int VID)
        {
            return DAL_TMSA_PI.GetVoyageDataSOx(TGID, VID);
        }
        public static DataSet GetVoyageDataNOx(string TGID, int VID)
        {
            return DAL_TMSA_PI.GetVoyageDataNOx(TGID, VID);
        }


        public DataTable Get_SavedQuery(string QueryName, string Command_Type, int UserID)
        {
            return objDAL.Get_SavedQuery(QueryName, Command_Type, UserID);
        }
        public DataTable Get_SavedQuery(string DBServer, string DatabaseName, string DBUserName, string Command_Type, string QueryName, int UserID)
        {

            return objDAL.Get_SavedQuery(DBServer, DatabaseName, DBUserName, Command_Type, QueryName, UserID);
        }

        
        /// <summary>
        /// Description: Method to get rankwise search for crew retention PI values
        /// Created By: Bhairab
        /// Created Date:31-05-2016
        /// </summary>
        /// <param name="Rank_Id"> Crew Rank Id</param>
        /// <param name="Startdate"> From date</param>
        /// <param name="EndDate"> To Date</param>
        /// <param name="PI_ID"> PI Id </param>
        /// <param name="Page_Index">Page index from user control</param>
        /// <param name="Page_Size">No of Rows</param>
        /// <param name="is_Fetch_Count"></param>
        /// <returns></returns>

        public static DataSet Get_RankWise_Values(int? Rank_Id, DateTime? Startdate, DateTime? EndDate, int? PI_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_TMSA_PI.Get_RankWise_Values(Rank_Id, Startdate, EndDate, PI_ID, Page_Index, Page_Size, ref is_Fetch_Count);
        }



        /// <summary>
        /// KPI crew retention rank wise update
        /// </summary>
        /// <param name="ID"> Record Id</param>
        /// <param name="Value"> PI value</param>
        /// <param name="UserID"> IUpdated by</param>
        /// <returns></returns>


        public  void CrewRetention_UpdateRankValue(int ID, int Value, int UserID)
        {
             objDAL.CrewRetention_UpdateRankValue(ID, Value, UserID);
        }
    }


}
