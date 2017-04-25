using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.TMSA
{
    public class DAL_TMSA_KPI
    {
        private string connection = "";

        public DAL_TMSA_KPI()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public DataSet Get_SPIList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_SPIList");
        }
        public DataSet Get_KPIDetails_DL(int KPI_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@KPI_ID", KPI_ID), 
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_KPIDetail_SPI", sqlprm);
        }
        //public DataTable Get_SPIDetails()
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[] { 
        //        new SqlParameter("@DocTypeID", DocTypeID), 
        //        new SqlParameter("@AttributeID", AttributeID),
        //        new SqlParameter("@IsRequired", IsRequired) 
        //    };
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_SPIDetails", sqlprm).Tables[0];
        //}

        public int INSERT_Category_DL(string Category_Name, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Category_Name",Category_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_INS_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_Category_DL(int ID, string Category_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Category_Name",Category_Name),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_UPD_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Category_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_Del_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_CategoryList_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_CatList", sqlprm).Tables[0];
        }

        public int INSERT_Unit_DL(string Category_Name, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Unit_Name",Category_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_INS_Unit", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_Units_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_KPIUnits", sqlprm).Tables[0];
        }



        public int UPDATE_Unit(int ID, string Category_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Unit_Name",Category_Name),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_UPD_Unit", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Unit(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_DEL_Unit", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        //


        public int INSERT_Interval_DL(string Category_Name, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Interval_Name",Category_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_INS_Interval", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_Intervals_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_KPIIntervals", sqlprm).Tables[0];
        }



        public int UPDATE_Interval(int ID, string Category_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Interval_Name",Category_Name),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_UPD_Interval", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Interval(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "KPI_DEL_Interval", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_AllPI_List_DL(string Interval)
        {
            SqlParameter[] prm = new SqlParameter[] {

              new SqlParameter("@Interval", Interval)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_AllPI_List", prm).Tables[0];
        }
        public DataTable UPD_KPIDetails_DL(int? KPI_ID, DataTable Formula, string KPI_Name, string KPI_Desc, string Interval, string TimePeriod, string Measurement, string DataSource, int UserID, int Status, string Category, string url, int KPI_ApplicableFor)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@KPI_ID", KPI_ID), 
              new SqlParameter("@Formula", Formula), 
              new SqlParameter("@KPI_Name", KPI_Name), 
              new SqlParameter("@KPI_Desc", KPI_Desc), 
              new SqlParameter("@Interval", Interval), 
              new SqlParameter("@TimePeriod", TimePeriod), 
              new SqlParameter("@Measurement", Measurement),
              new SqlParameter("@DataSource", DataSource),
              new SqlParameter("@Created_By", UserID), 
              new SqlParameter("@Status", Status), 
               new SqlParameter("@Category", Category),
               new SqlParameter("@URL", url),
               new SqlParameter("@KPI_ApplicableFor", KPI_ApplicableFor)
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_UPD_KPI_Details", sqlprm).Tables[0];
        }

        public DataSet Get_KPI_DetailGoals(int? KPI_ID, DataTable dtFleet, int Vessel_Manager, int UserCompanyID, int User_id, string Goal_Applicable_For)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                       new SqlParameter("@KPI_ID",KPI_ID),
                new SqlParameter("@FleetIDList", dtFleet), 
              new SqlParameter("@Vessel_Manager", Vessel_Manager), 
              new SqlParameter("@UserCompanyID", UserCompanyID), 
              new SqlParameter("@User_Id", User_id), 
              new SqlParameter("@Goal_Applicable_For", Goal_Applicable_For)};

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_KPI_DetailGoals", prm);

            return ds;

        }

        public void INSERT_KPI_GoalDetail(int KPI_ID, DataTable @dtPIDetails, int UserID, string Goal_Applicable_For)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@KP_ID",KPI_ID),
                new SqlParameter("@dtKPIDetails", @dtPIDetails),
                new SqlParameter("@Created_By",UserID),
                new SqlParameter("@Goal_Applicable_For",Goal_Applicable_For)
                };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_INSERT_KPI_Goal", sqlprm);

        }

        public DataTable Get_Fleet_Vessel_List(DataTable dtFleet, int Vessel_Manager, int UserCompanyID, int User_id)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@FleetIDList", dtFleet), 
              new SqlParameter("@Vessel_Manager", Vessel_Manager), 
              new SqlParameter("@UserCompanyID", UserCompanyID), 
              new SqlParameter("@User_Id", User_id), 

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Fleet_VesselList", sqlprm).Tables[0];
        }

        public DataTable Get_CO2_Values(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@dtVessel_Ids", dtVessel), 
              new SqlParameter("@Effective_From", Effective_From), 
              new SqlParameter("@Effective_To", Effective_To)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_CO2_Average", sqlprm).Tables[0];
        }

        public DataSet Get_SOx_Average(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@dtVessel_Ids", dtVessel), 
              new SqlParameter("@Effective_From", Effective_From), 
              new SqlParameter("@Effective_To", Effective_To)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_SOx_Average", sqlprm);
        }
        public DataTable Get_NOx_Average(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@dtVessel_Ids", dtVessel), 
              new SqlParameter("@Effective_From", Effective_From), 
              new SqlParameter("@Effective_To", Effective_To)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_NOx_Average", sqlprm).Tables[0];
        }
        public DataTable Get_CO2_Average(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@dtVessel_Ids", dtVessel), 
              new SqlParameter("@Effective_From", Effective_From), 
              new SqlParameter("@Effective_To", Effective_To)

            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_CO2_Average", sqlprm).Tables[0];
        }
        /// <summary>
        ///Description: Method to load the list of voyages for a vessel within a date range  
        /// </summary>
        /// <param name="Vessel_Id"></param>
        /// <param name="Effective_From"></param>
        /// <param name="Effective_To"></param>
        /// <returns></returns>
        public DataTable GetVoyageList(int Vessel_Id, DateTime Effective_From, DateTime Effective_To)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@Vessel_ID", Vessel_Id),
              new SqlParameter("@Effective_From", Effective_From), 
              new SqlParameter("@Effective_To", Effective_To)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_GetVoyageList", sqlprm).Tables[0];
        }

        public DataTable GetGoal(int Vessel_Id, string KPI_CODE, int KPI_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
              new SqlParameter("@Vessel_ID", Vessel_Id),
              new SqlParameter("@KPI_ID", KPI_ID),
              new SqlParameter("@KPI_CODE", KPI_CODE)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_KPI_Get_Goal", sqlprm).Tables[0];
        }

        public DataTable Get_KPIList()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_KPI_Get_KPIList").Tables[0];
        }


        public DataSet Get_Vessel_KPI_Values(int VID, int KPI_ID, string Interval, string Value_Type, DateTime? Startdate, DateTime? EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@VID",VID),
                                            new SqlParameter("@KPI_ID",KPI_ID),
                                            new SqlParameter("@Interval",Interval),
                                             new SqlParameter("@Value_Type",Value_Type),
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_KPI_Get_Vessel_KPI_Values", sqlprm);

        }
        public  DataSet GetVoyageGenericData(string TGID, int VID, int KPI_ID, DateTime? Startdate, DateTime? EndDate)
        {
            SqlParameter[] prm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TG_ID",TGID),
                                            new SqlParameter("@VID",VID),
                                            new SqlParameter("@KPID",KPI_ID),
                                             new SqlParameter("@EFrom",Startdate),
                                             new SqlParameter("@ETo",EndDate)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_Voyage_Generic_Data", prm);
        }

        public DataSet Get_Multiple_Generic_Values(DataTable dtVID, int KPI_ID, string Interval, string Value_Type, DateTime? Startdate, DateTime? EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@dtVessel_Ids",dtVID),
                                            new SqlParameter("@KPI_ID",KPI_ID),
                                            new SqlParameter("@Interval",Interval),
                                             new SqlParameter("@Value_Type",Value_Type),
                                             new SqlParameter("@Effective_From",Startdate),
                                             new SqlParameter("@Effective_To",EndDate)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_KPI_Get_Multiple_Generic_Values", sqlprm);

        }
        /// <summary>
        /// Description: Method to search crew retention rate 
        /// Created By: Bhairab
        /// Created On: 30/05/2016
        /// </summary>
        /// <param name="Rank_Ids"> Selected ranks will be send as parameter </param>
        /// <param name="Years"> Selected years for searching crew retention w</param>
        /// <param name="Category_Id"> For particular category retention rate, category will be send a as parameter</param>
        public DataSet Search_CrewRetention(string Rank_Ids, string Years, int Category_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Rank_Ids",Rank_Ids),
                                            new SqlParameter("@Years",Years),
                                            new SqlParameter("@CategoryID",Category_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Search_CrewRetention", sqlprm);

        }


        /// <summary>
        /// Description: Method to search PMS overdue jobs rate
        /// Created By: Bhairab
        /// Created On: 09/06/2016
        /// </summary>
        /// <param name="dtVID"> Selected Vessels will be send as parameter </param>
        /// <param name="StartDate"> From date to be searched </param>
        /// <param name="EndDate"> To Date to be searched</param>
        /// <param name="KPIID"> KPI if critical or non critical overdue</param>
        public DataSet Get_PM_Overdue(DataTable dtVID, DateTime? Startdate, DateTime? EndDate,int? KPIID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                        new SqlParameter("@dtVessel_Ids",dtVID),
                                        new SqlParameter("@Effective_From",Startdate),
                                        new SqlParameter("@Effective_To",EndDate),
                                        new SqlParameter("@KPI_ID",KPIID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_PMS_OverDue", sqlprm);

        }


        /// <summary>
        /// Description: Method to search PMS overdue jobs rate
        /// </summary>
        /// <param name="dtVID"> Selected Vessels will be send as parameter </param>
        /// <param name="EndDate"> To Date to be searched</param>
        public DataSet Get_PM_OverdueLastMonth(DataTable dtVID, DateTime? EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                        new SqlParameter("@dtVessel_Ids",dtVID),
                                        new SqlParameter("@Effective_To",EndDate)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_PMS_OverDue_LastMonth", sqlprm);

        }


        /// <summary>
        /// Description: Method to get PMS overdue jobs by vessel
        /// Created By: Bhairab
        /// Created On: 09/06/2016
        /// </summary>
        /// <param name="Vessel_Id"> Selected Vessel will be send as parameter </param>
        /// <param name="Startdate"> From date selected to be searched </param>
        /// <param name="EndDate"> To date selected to be searched </param>
        public DataSet Get_PMS_OverDue_ByVessel(int Vessel_Id, DateTime? Startdate, DateTime? EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_Id",Vessel_Id),
                                             new SqlParameter("@Effective_From",Startdate),
                                              new SqlParameter("@Effective_To",EndDate),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "KPI_Get_PMS_OverDue_ByVessel", sqlprm);

        }
        /// <summary>
        /// Description: Method to get Rank list
        /// Created By: Krishnapriya
        /// Created On: 23/11/2016
        /// </summary>
        public DataTable Get_RankList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankList").Tables[0];
        }


        /// <summary>
        /// Description: Method to get vetting KPI list
        /// Created By: Bhairab
        /// Created On: 14/03/2017
        /// </summary>
        public DataTable Get_Vetting_KPI_List_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_KPI_Get_Vetting_KPI_List").Tables[0];
        }

        /// <summary>
        /// Description: Method to get company KPI
        /// Created By: Bhairab
        /// </summary>
        /// <param name="Startdate"> From date selected to be searched </param>
        /// <param name="EndDate"> To date selected to be searched </param>
        public DataSet Get_Vetting_KPI_ByCompany( string Qtr, int KPI_Id, DateTime? Startdate, DateTime? EndDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Qtr",Qtr),
                                            new SqlParameter("@KPI_ID",KPI_Id),
                                            new SqlParameter("@start_date",Startdate),
                                            new SqlParameter("@end_date",EndDate),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_KPI_Get_Vetting_KPI_ByCompany", sqlprm);

        }



        /// <summary>
        /// Description: Method to get list of PIs for KPI
        /// Created By: Bhairab

        public DataSet Get_PI_ListByKPI(int KPI_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@KPI_ID",KPI_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_PI_ListByKPI", sqlprm);

        }



         /// <summary>
        /// Description: Method to get list of PIs for KPI with table format
        /// Created By: Bhairab

        public DataSet Get_PI_ListByKPI_Async(int KPI_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@KPI_ID",KPI_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Get_PI_ListByKPI_Async", sqlprm);

        }

        
    }
}
