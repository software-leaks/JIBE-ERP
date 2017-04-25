using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.TMSA
{


    public class DAL_TMSA_PI
    {
        SqlConnection conn;
        private static string connection = "";

        public DAL_TMSA_PI()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataTable Get_UserDetails_DL(int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]{  
                
                                                      new SqlParameter("@UserID",UserID)
   												  };
            DataSet ds = new DataSet();
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserDetails", obj).Tables[0];
        }
        static DAL_TMSA_PI()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataSet Get_PI_Details(int PI_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@PI_ID", PI_ID) ,                
                    
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_TMSA_PI.connection, CommandType.StoredProcedure, "TMSA_Get_PI_Details", obj);

            return ds;
        }

        public static int INS_PI_Details(string PI_Name, string PICode, string Interval, string Description, string Measurement_Detail, string Context, string UOM, string DataSource, int MeasuredForSBU, int IncludeBL,int PI_Status,int IsWorkList, int IsInspectionType,int IsVetting, int UserID)
        {
            int PI_ID=0;
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@Name",PI_Name),
                                            new SqlParameter("@PICode",PICode),
                                            new SqlParameter("@Interval",Interval),
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@Measurement_Detail",Measurement_Detail),
                                            new SqlParameter("@Context",Context),
                                            new SqlParameter("@UOM",UOM),
                                            new SqlParameter("@DataSource",DataSource),
                                            new SqlParameter("@MeasuredForSBU",MeasuredForSBU),
                                            new SqlParameter("@IncludeBL",IncludeBL),
                                            new SqlParameter("@PI_Status",PI_Status),
                                            new SqlParameter("@IsWorkList",IsWorkList),
                                            new SqlParameter("@IsInspectionType",IsInspectionType),
                                            new SqlParameter("@IsVetting",IsVetting),
                                            new SqlParameter("@Created_By",UserID),
                                            new SqlParameter("@PI_ID",PI_ID)
                                                    };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
           
            int result= SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Insert_PI", sqlprm);
            PI_ID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return PI_ID;

        }

        public static int Update_PI(int PI_ID, string PI_Name, string PICode, string Interval, string Description, string Measurement_Detail, string Context, string UOM, string DataSource, int MeasuredForSBU, int IncludeBL, int PI_Status, int IsWorkList, int IsInspectionType, int IsVetting, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Name",PI_Name),
                                            new SqlParameter("@PICode",PICode),
                                            new SqlParameter("@Interval",Interval),
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@Measurement_Detail",Measurement_Detail),
                                            new SqlParameter("@Context",Context),
                                            new SqlParameter("@UOM",UOM),
                                            new SqlParameter("@DataSource",DataSource),
                                            new SqlParameter("@MeasuredForSBU",MeasuredForSBU),
                                            new SqlParameter("@IncludeBL",IncludeBL),
                                            new SqlParameter("@PI_Status",PI_Status),
                                            new SqlParameter("@IsWorkList",IsWorkList),
                                            new SqlParameter("@IsInspectionType",IsInspectionType),
                                            new SqlParameter("@IsVetting",IsVetting),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Update_PI", sqlprm);

        }

        public static int INS_Worklist_PI_Details(int PI_ID, int Company_ID, string Job_Type,string Assigned_By, string Nature_List, string Primary_List, string Secondary_List, string Minor_List,bool Activate_Scheduler,DateTime? dtEffective, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Company_ID",Company_ID),
                                            new SqlParameter("@Job_Type",Job_Type),
                                            new SqlParameter("@Assigned_By",Assigned_By),
                                            new SqlParameter("@Nature_List",Nature_List),
                                            new SqlParameter("@Primary_List",Primary_List),
                                            new SqlParameter("@Secondary_List",Secondary_List),
                                            new SqlParameter("@Minor_List",Minor_List),
                                            new SqlParameter("@Activate_Scheduler",Activate_Scheduler),
                                            new SqlParameter("@Effective_From",dtEffective),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Insert_WorklistPI", sqlprm);

        }

        public static int Update_Worklist_PI(int WorkList_PI_ID, int PI_ID, int Company_ID, string Job_Type, string Assigned_By, string Nature_List, string Primary_List, string Secondary_List, string Minor_List,bool Activate_Scheduler,DateTime? dtEffective, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@WorkList_PI_ID",WorkList_PI_ID),
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Company_ID",Company_ID),
                                            new SqlParameter("@Job_Type",Job_Type),
                                            new SqlParameter("@Assigned_By",Assigned_By),
                                            new SqlParameter("@Nature_List",Nature_List),
                                            new SqlParameter("@Primary_List",Primary_List),
                                            new SqlParameter("@Secondary_List",Secondary_List),
                                            new SqlParameter("@Minor_List",Minor_List),
                                            new SqlParameter("@Activate_Scheduler",Activate_Scheduler),
                                            new SqlParameter("@Effective_From",dtEffective),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Update_WorklistPI", sqlprm);

        }


        public static int INS_Inspection_PI_Details(int PI_ID, int Company_ID, string Inspection_Type, string Assigned_By,bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Company_ID",Company_ID),
                                            new SqlParameter("@Inspection_Type",Inspection_Type),
                                            new SqlParameter("@Assigned_By",Assigned_By),
                                            new SqlParameter("@Activate_Scheduler",Activate_Scheduler),
                                            new SqlParameter("@Effective_From",dtEffective),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Insert_IspectionTypePI", sqlprm);

        }

        public static int Update_Inspection_PI(int WorkList_PI_ID, int PI_ID, int Company_ID,string Inspection_Type, string Assigned_By,bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@WorkList_PI_ID",WorkList_PI_ID),
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Company_ID",Company_ID),
                                            new SqlParameter("@Inspection_Type",Inspection_Type),
                                            new SqlParameter("@Assigned_By",Assigned_By),
                                            new SqlParameter("@Activate_Scheduler",Activate_Scheduler),
                                            new SqlParameter("@Effective_From",dtEffective),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Update_IspectionTypePI", sqlprm);

        }


        public static int INS_Vetting_PI_Details(int PI_ID, int Company_ID, string Vetting_Type, string Observation_Type, string Observation_Category, string Observation_Risk_Level, string Assigned_By, bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Company_ID",Company_ID),
                                            new SqlParameter("@Vetting_Type",Vetting_Type),
                                            new SqlParameter("@Observation_Category",Observation_Category),
                                            new SqlParameter("@Observation_Type",Observation_Type),
                                            new SqlParameter("@Observation_Risk_Level",Observation_Risk_Level),
                                            new SqlParameter("@Activate_Scheduler",Activate_Scheduler),
                                            new SqlParameter("@Effective_From",dtEffective),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Insert_VettingTypePI", sqlprm);

        }

        public static int Update_Vetting_PI(int WorkList_PI_ID, int PI_ID, int Company_ID,  string Vetting_Type, string Observation_Type, string Observation_Category, string Observation_Risk_Level, string Assigned_By, bool Activate_Scheduler, DateTime? dtEffective, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@WorkList_PI_ID",WorkList_PI_ID),
                                            new SqlParameter("@PI_ID",PI_ID),
                                            new SqlParameter("@Company_ID",Company_ID),
                                            new SqlParameter("@Vetting_Type",Vetting_Type),
                                            new SqlParameter("@Observation_Category",Observation_Category),
                                            new SqlParameter("@Observation_Type",Observation_Type),
                                            new SqlParameter("@Observation_Risk_Level",Observation_Risk_Level),
                                            new SqlParameter("@Activate_Scheduler",Activate_Scheduler),
                                            new SqlParameter("@Effective_From",dtEffective),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Update_VettingTypePI", sqlprm);

        }


        public static DataSet Get_PI_List(string SearchPI, string Category, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@SearchPI",SearchPI),
                                                       new SqlParameter("@Category",Category),
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            //DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_SP_Get_PI_List", prm);
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_PI_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }

        public static DataSet Get_PI_HeadList(int PI_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@PI_ID",PI_ID),
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_PI_HeadList", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }

        public static DataSet Get_PI_Head_Details(int PI_ID, int Detail_ID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@PI_ID",PI_ID),
                                                       new SqlParameter("@Detail_ID",Detail_ID)
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_PI_Head_Details", prm);

            return ds;

        }

        public static DataSet Get_All_Vessels( int CompanyId)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                             
                                                       new SqlParameter("@Company_ID",CompanyId)
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_All_Vessels", prm);

            return ds;

        }

        public static int Insert_PI_Head(int? PI_ID, string ForYear, string QtrMonth, DateTime? Effective_From, DateTime? Effective_To, double? SBU_Value, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@PI_ID", PI_ID),
                new SqlParameter("@ForYear",ForYear),
                new SqlParameter("@QtrMonth", QtrMonth),
                new SqlParameter("@Effective_From", Effective_From),
                new SqlParameter("@Effective_To", Effective_To),
                new SqlParameter("@SBU_Value", SBU_Value),
                new SqlParameter("@Created_By",UserID)
              };

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "TMSA_Insert_PI_Head", sqlprm);

        }

        public static void Update_PI_Head(int Detail_ID, int? PI_ID, string ForYear, string QtrMonth, DateTime? Effective_From, DateTime? Effective_To, double? SBU_Value,double? Value, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@Detail_ID", Detail_ID),
                new SqlParameter("@PI_ID", PI_ID),
                new SqlParameter("@ForYear",ForYear),
                new SqlParameter("@QtrMonth",  QtrMonth),
                new SqlParameter("@Effective_From", Effective_From),
                new SqlParameter("@Effective_To", Effective_To),
                new SqlParameter("@SBU_Value", SBU_Value),
                new SqlParameter("@Value",Value),
                new SqlParameter("@Created_By",UserID)
              };

            SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "TMSA_Update_PI_Head", sqlprm);

        }

        public static void INSERT_PI_Detail(int Detail_ID, double? Value, DataTable @dtPIDetails, int? PI_ID, string ForYear, string QtrMonth, DateTime? Effective_From, DateTime? Effective_To, double? SBU_Value, int UserID, ref int IsExist)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@Detail_Id",Detail_ID),
                new SqlParameter("@Value",Value),
                new SqlParameter("@PI_ID", PI_ID),
                new SqlParameter("@ForYear",ForYear),
                new SqlParameter("@QtrMonth",  QtrMonth),
                new SqlParameter("@Effective_From", Effective_From),
                new SqlParameter("@Effective_To", Effective_To),
                new SqlParameter("@SBU_Value", SBU_Value),
                new SqlParameter("@dtPIDetails", @dtPIDetails),
                new SqlParameter("@Created_By",UserID),
                new SqlParameter("@AlreadyExists",IsExist)
                };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

           // DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_PI_HeadList", sqlprm);
            
  

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_INSERT_PI_Detail", sqlprm);
            IsExist = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public static DataSet Get_PI_ListByKPI(int? KPI_ID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@KPI_ID",KPI_ID)
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_PI_ListByKPI", prm);

            return ds;

        }

        public static DataTable Get_KPI_Category()
        {

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_GET_KPI_Category").Tables[0];

            return dt;

        }

        public static DataSet Get_KPI_Detail(int? KPI_ID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@KPI_ID",KPI_ID)
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_KPI_Detail", prm);

            return ds;

        }

        
        public static DataSet Get_KPI_List(string SearchKPI, int? Page_Index, int? Page_Size, ref int is_Fetch_Count,string Category)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@SearchKPI",SearchKPI),
                                                       new SqlParameter("@zPAGE_INDEX",Page_Index),
                                                       new SqlParameter("@zPAGE_SIZE",Page_Size),
                                                       new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),
                                                       new SqlParameter("@Category",Category)
                                                    };
            prm[prm.Length - 2].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_KPI_List", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 2].Value);
            return ds;
        }


        public static int INS_KPI_Details(string KPI_Name, string KPICode, string Interval, string Description, DataTable dtPI,  int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                                            new SqlParameter("@Name",KPI_Name),
                                            new SqlParameter("@KPICode",KPICode),
                                            new SqlParameter("@Interval",Interval),
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@dtPI",dtPI),
                                            new SqlParameter("@Created_By",UserID)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Insert_KPI", sqlprm);

        }

        public static DataSet Search_PI_Values(int VID, int KPI_ID, string Startdate, string EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                            new SqlParameter("@KPI_ID",KPI_ID),
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Vessel_CO2_Values", sqlprm);
        }

        public static DataSet Search_Voyage_PI_Values(int VID, int KPI_ID, string telId1, string telId2)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                            new SqlParameter("@KPI_ID",KPI_ID),
                                             new SqlParameter("@telId1",telId1),
                                             new SqlParameter("@telId2",telId2)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Vessel_Voyage_CO2_Values", sqlprm);
        }


        public static DataSet Search_PI_Voyage_ValuesNOX(int VID,  string telId1, string telId2)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                             new SqlParameter("@telId1",telId1),
                                             new SqlParameter("@telId2",telId2)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Vessel_Voyage_NOx_Values", sqlprm);
        }


        public static DataSet Search_PI_Voyage_ValuesSOX(int VID, string telId1, string telId2)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                             new SqlParameter("@telId1",telId1),
                                             new SqlParameter("@telId2",telId2)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Vessel_Voyage_SOx_Values", sqlprm);
        }

        public static DataSet Search_PI_ValuesSOX(int VID,  string Startdate, string EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                          
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Vessel_SOx_Values", sqlprm);
        }

        public static DataSet Search_PI_ValuesNOX(int VID,  string Startdate, string EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                      
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Vessel_NOx_Values", sqlprm);
        }

        public static DataSet Get_Vessel_Values(int? Vessel_Id, DateTime? Startdate, DateTime? EndDate,int? PI_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Vessel_Id",Vessel_Id),
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate),
                                             new SqlParameter("@PI_ID",PI_ID),
                                             new SqlParameter("@zPAGE_INDEX",Page_Index),
                                            new SqlParameter("@zPAGE_SIZE",Page_Size),
                                            new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
                                        };

            
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PI_Get_Vessel_Values", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;
            
        }
        public static DataSet GetTelDate(string VI_ID, int Vessel_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VY_ID",VI_ID),
                                            new SqlParameter("@Vessel_Id",Vessel_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_GET_TEL_DATE", prm);
        }
        public static DataSet GetVoyageData(string TGID,int VID, int KPI_ID)
        {

            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TG_ID",TGID),
                                            new SqlParameter("@VID",VID),
                                            new SqlParameter("@KPID",KPI_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Voyage_Data", prm);
        }
        public static DataSet GetVoyageDataSOx(string TGID, int VID)
        {
            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TG_ID",TGID),
                                            new SqlParameter("@VID",VID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Voyage_DataSOx", prm);
        }

        public static DataSet GetVoyageDataNOx(string TGID, int VID)
        {
            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TG_ID",TGID),
                                            new SqlParameter("@VID",VID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Voyage_DataNOx", prm);
        }

        /*! \brief 
        *  Function is used to get details of  Saved Query 
        *
        *  \details 
        *  This function is used to  get details of  Saved Query 
        * \param[string] QueryName  defines SP or user defined query name
        *
        * \retval[Table] List
        */

        public DataTable Get_SavedQuery(string QueryName, string Command_Type, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query_Name",QueryName),
                                            new SqlParameter("@Command_Type", Command_Type),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_DAEMON_Get_Saved_Proc", sqlprm).Tables[0];
        }



        public DataTable Get_SavedQuery(string DBServer, string DatabaseName, string DBUserName, string Command_Type, string QueryName, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DBServer",DBServer),
                                            new SqlParameter("@DatabaseName",DatabaseName),
                                            new SqlParameter("@DBUserName",DBUserName),
                                            new SqlParameter("@Command_Type",Command_Type),
                                            new SqlParameter("@Query_Name",QueryName),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_DAEMON_Get_Saved_Proc", sqlprm).Tables[0];
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
            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Rank_Id",Rank_Id),
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate),
                                             new SqlParameter("@PI_ID",PI_ID),
                                             new SqlParameter("@zPAGE_INDEX",Page_Index),
                                            new SqlParameter("@zPAGE_SIZE",Page_Size),
                                            new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
                                        };


            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_CrewRetention_RankWise_Values", prm);
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds;

        }



        /// <summary>
        /// KPI crew retention rank wise update
        /// </summary>
        /// <param name="ID"> Record Id</param>
        /// <param name="Value"> PI value</param>
        /// <param name="UserID"> IUpdated by</param>
        /// <returns></returns>

        public void CrewRetention_UpdateRankValue(int ID, int Value, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Value", Value),
                                            new SqlParameter("@User_ID",UserID)
                                        };
           
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_CrewRetention_UpdateRankValue", sqlprm);
        }

    }
}
