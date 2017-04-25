using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.TMSA;

namespace SMS.Business.TMSA
{
    public class BLL_TMSA_KPI
    {
        DAL_TMSA_KPI objDAL = new DAL_TMSA_KPI();

        public DataSet Get_SPIList()
        {
            try
            {
                return objDAL.Get_SPIList_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_KPIDetails(int KPI_ID)
        {
            try
            {
                return objDAL.Get_KPIDetails_DL(KPI_ID);
            }
            catch
            {
                throw;
            }
        }
        public int INSERT_Category_DL(string Category_Name, int Created_By)
        {
            try
            {
                return objDAL.INSERT_Category_DL(Category_Name, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_Category(int ID, string Category_Name, int Modified_By)
        {
            try
            {
                return objDAL.UPDATE_Category_DL(ID, Category_Name, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int DELETE_Category(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_Category_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
      
        public DataTable Get_CategoryList(string SearchText)
        {
            try
            {
                return objDAL.Get_CategoryList_DL(SearchText);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Units(string SearchText)
        {
            try
            {
                return objDAL.Get_Units_DL(SearchText);
            }
            catch
            {
                throw;
            }
        }
        public int INSERT_Unit_DL(string Category_Name, int Created_By)
        {
            try
            {
                return objDAL.INSERT_Unit_DL(Category_Name, Created_By);
            }
            catch
            {
                throw;
            }
        }
       

        public int UPDATE_Unit(int ID, string Category_Name, int Modified_By)
        {
            try
            {
                return objDAL.UPDATE_Unit(ID, Category_Name, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int DELETE_Unit(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_Unit(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }

        //

        public DataTable Get_Intervals(string SearchText)
        {
            try
            {
                return objDAL.Get_Intervals_DL(SearchText);
            }
            catch
            {
                throw;
            }
        }
        public int INSERT_Interval_DL(string Category_Name, int Created_By)
        {
            try
            {
                return objDAL.INSERT_Interval_DL(Category_Name, Created_By);
            }
            catch
            {
                throw;
            }
        }


        public int UPDATE_Interval(int ID, string Category_Name, int Modified_By)
        {
            try
            {
                return objDAL.UPDATE_Interval(ID, Category_Name, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int DELETE_Interval(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_Interval(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method to return PI list based on interval type specified for KPI
        /// </summary>
        /// <param name="Interval"></param>
        /// <returns></returns>
        public DataTable Get_AllPI_List(string Interval)
        {
            try
            {
                return objDAL.Get_AllPI_List_DL(Interval);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Description: This method Inserts/Update KPI details. 
        /// </summary>
        /// <param name="KPI_ApplicableFor">Holds the Applicable For dropdown's value</param>
        /// <returns>Returns  a datatable which contains KPI_ID and KPI_code value</returns>
        public DataTable UPD_KPIDetails(int? KPI_ID, DataTable Formula, string KPI_Name, string KPI_Desc, string Interval, string TimePeriod, string Measurement, string DataSource, int UserID, int Status, string Category, string url, int KPI_ApplicableFor)
        {
            try
            {
                return objDAL.UPD_KPIDetails_DL(KPI_ID, Formula, KPI_Name, KPI_Desc, Interval, TimePeriod, Measurement, DataSource, UserID, Status, Category, url, KPI_ApplicableFor);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Description: This method fetches the KPI goals based on the option selected in "Applicable For" dropdown. 
        /// </summary>
        /// <param name="KPI_ApplicableFor">Holds the Applicable For dropdown's value</param>
        /// <returns>List of Goals</returns>
      public DataSet Get_KPI_DetailGoals(int? KPI_ID, DataTable dtFleet, int Vessel_Manager, int UserCompanyID, int User_id, string Goal_Applicable_For)
        {
            try
            {
                return objDAL.Get_KPI_DetailGoals(KPI_ID, dtFleet, Vessel_Manager, UserCompanyID, User_id, Goal_Applicable_For);
                  
            }
            catch
            {
                throw;
            }
        }
      /// <summary>
      /// Description: This method saves the KPI Goal detail values in Database. 
      /// </summary>
      /// <param name="KPI_ApplicableFor">Holds the Applicable For dropdown's value</param>
      public void INSERT_KPI_GoalDetail(int KPI_ID, DataTable @dtPIDetails, int UserID, string Goal_Applicable_For)
        {
            objDAL.INSERT_KPI_GoalDetail(KPI_ID, dtPIDetails, UserID, Goal_Applicable_For);
          
        }

        public DataTable Get_Fleet_Vessel_List(DataTable dtFleet, int Vessel_Manager,  int UserCompanyID, int  User_id)
        {
            try
            {
                return objDAL.Get_Fleet_Vessel_List(dtFleet, Vessel_Manager, UserCompanyID,   User_id);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_CO2_Average(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {
            try
            {
                return objDAL.Get_CO2_Average(dtVessel, Effective_From, Effective_To);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_NOx_Average(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {
            try
            {
                return objDAL.Get_NOx_Average(dtVessel, Effective_From, Effective_To);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_SOx_Average(DataTable dtVessel, DateTime Effective_From, DateTime Effective_To)
        {
            try
            {
                return objDAL.Get_SOx_Average(dtVessel, Effective_From, Effective_To);
            }
            catch
            {
                throw;
            }
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
            try
            {
                return objDAL.GetVoyageList(Vessel_Id, Effective_From, Effective_To);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetGoal(int Vessel_Id, string KPI_CODE, int KPI_ID)
        {
            try
            {
                return objDAL.GetGoal(Vessel_Id,KPI_CODE, KPI_ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_KPIList()
        {
            try
            {
                return objDAL.Get_KPIList();
            }
            catch
            {
                throw;
            }
        }


        public DataSet Get_Vessel_KPI_Values(int VID, int KPI_ID, string Interval, string Value_Type, DateTime? Startdate, DateTime? EndDate)
        {
            try
            {
                return objDAL.Get_Vessel_KPI_Values( VID,  KPI_ID,  Interval, Value_Type, Startdate,  EndDate);
            }
            catch
            {
                throw;
            }
        }

        public  DataSet GetVoyageGenericData(string TGID, int VID, int KPI_ID, DateTime? Startdate, DateTime? EndDate)
        {
            return objDAL.GetVoyageGenericData(TGID, VID, KPI_ID, Startdate, EndDate);
        }

        public DataSet Get_Multiple_Generic_Values(DataTable dtVID, int KPI_ID, string Interval, string Value_Type, DateTime? Startdate, DateTime? EndDate)
        {
            try
            {
                return objDAL.Get_Multiple_Generic_Values(dtVID, KPI_ID, Interval, Value_Type, Startdate, EndDate);
            }
            catch
            {
                throw;
            }
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
            try
            {
                return objDAL.Search_CrewRetention(Rank_Ids, Years, Category_Id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Description: Method to search PMS overdue jobs rate
        /// Created By: Bhairab
        /// Created On: 09/06/2016
        /// </summary>
        /// <param name="dtVID"> Selected Vessels will be send as parameter </param>
        /// <param name="EndDate"> To Date to be searched</param>

        public DataSet Get_PM_OverdueLastMonth(DataTable dtVID, DateTime? EndDate)
        {
            try
            {
                return objDAL.Get_PM_OverdueLastMonth(dtVID, EndDate);
            }
            catch
            {
                throw;
            }
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
        public DataSet Get_PM_Overdue(DataTable dtVID, DateTime? Startdate, DateTime? EndDate, int? KPIID)
        {
            try
            {
                return objDAL.Get_PM_Overdue(dtVID, Startdate, EndDate, KPIID);
            }
            catch
            {
                throw;
            }

        }



        /// <summary>
        /// Desccriptin: Method to pivot table rows as colums based on some column value
        /// </summary>
        /// <param name="PivotColumnName"></param>
        /// <param name="PivotValueColumnName"></param>
        /// <param name="PivotColumnOrder"></param>
        /// <param name="PrimaryKeyColumns"></param>
        /// <param name="HideColumns"></param>
        /// <param name="dtTableToPivot"></param>
        /// <returns></returns>

        public  DataTable PivotTable(string PivotColumnName, string PivotValueColumnName, string PivotColumnOrder, string[] PrimaryKeyColumns, string[] HideColumns, DataTable dtTableToPivot)
        {
            StringBuilder sbPKs = new StringBuilder();

            DataTable dtFinalResult = new DataTable();
            DataView dvPivotColumnNames;
            if (!string.IsNullOrWhiteSpace(PivotColumnOrder))
            {
                dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName, PivotColumnOrder }).DefaultView;
                dvPivotColumnNames.Sort = PivotColumnOrder;
            }
            else
            {
                dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName }).DefaultView;

            }
            DataTable dtPivotPrimaryKeys = dtTableToPivot.DefaultView.ToTable(true, PrimaryKeyColumns);


            foreach (DataColumn dcol in dtTableToPivot.Columns)
            {
                if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                {
                    dtFinalResult.Columns.Add(dcol.ColumnName);

                }
            }

            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {
                dtFinalResult.Columns.Add(drCol[0].ToString());
            }


            foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
            {
                DataRow drNew = dtFinalResult.NewRow();

                foreach (DataColumn dcol in dtTableToPivot.Columns)
                {
                    if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                    {
                        sbPKs.Clear();
                        foreach (string pk in PrimaryKeyColumns)
                        {

                            if (pk == "MonthYear" || pk == "Vessel" || pk == "KPI" || pk == "MONTH" || pk == "OilMajorName" || pk == "Risk_Level")
                                sbPKs.Append(pk + " = '" + drPK[pk].ToString() + "' and ");
                            else
                                sbPKs.Append(pk + " = '" + drPK[pk].ToString() + "' and ");

                        }
                        sbPKs.Append(" 1=1  ");

                        DataRow[] drcoll = dtTableToPivot.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                        drNew[dcol.ColumnName] = drcoll[0][dcol.ColumnName];
                    }
                }

                foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
                {
                    sbPKs.Clear();
                    foreach (string pk in PrimaryKeyColumns)
                    {
                        if (pk == "MonthYear" || pk == "Vessel" || pk == "KPI" || pk == "MONTH" || pk == "OilMajorName" || pk == "Risk_Level")
                            sbPKs.Append(pk + " = '" + drPK[pk].ToString() + "' and ");
                        else
                            sbPKs.Append(pk + " = '" + drPK[pk].ToString() + "' and ");

                    }

                    DataRow[] drValue = dtTableToPivot.Select(sbPKs.ToString() + PivotColumnName + " = '" + drCol[0].ToString() + "' ");
                    if (drValue.Length > 0)
                        drNew[drCol[0].ToString()] = drValue[0][PivotValueColumnName];
                    else
                        drNew[drCol[0].ToString()] = null;
                }

                dtFinalResult.Rows.Add(drNew);
            }



            if (HideColumns != null)
            {
                foreach (string strColToremove in HideColumns)
                {
                    if (dtFinalResult.Columns.IndexOf(strColToremove) > -1)
                        dtFinalResult.Columns.Remove(strColToremove);
                }
            }
            return dtFinalResult;
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
           return  objDAL.Get_PMS_OverDue_ByVessel(Vessel_Id,  Startdate,  EndDate);
        }

        /// <summary>
        /// Description: Method to fetch the list of Ranks
        /// Created By: Krishnapriya
        /// Created On: 23/11/2016
        /// </summary>
        public DataTable Get_RankList()
        {
            try
            {
                return objDAL.Get_RankList_DL();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Description: Method to get vetting KPI list
        /// Created By: Bhairab
        /// Created On: 14/03/2017
        /// </summary>
        public DataTable Get_Vetting_KPI_List()
        {
            return objDAL.Get_Vetting_KPI_List_DL();
        }



        /// <summary>
        /// Description: Method to get company KPI
        /// Created By: Bhairab
        /// </summary>
        /// <param name="Startdate"> From date selected to be searched </param>
        /// <param name="EndDate"> To date selected to be searched </param>

        public DataSet Get_Vetting_KPI_ByCompany(string Qtr, int KPI_ID, DateTime? Startdate, DateTime? EndDate)
        {
            return objDAL.Get_Vetting_KPI_ByCompany(Qtr, KPI_ID, Startdate, EndDate);
        }



        /// <summary>
        /// Description: Method to get company KPI
        /// Created By: Bhairab
        /// </summary>

        public DataSet Get_PI_ListByKPI(int KPI_ID)
        {
            return objDAL.Get_PI_ListByKPI(KPI_ID);
        }

         /// <summary>
        /// Description: Method to get list of PIs for KPI with table format
        /// Created By: Bhairab

        public DataSet Get_PI_ListByKPI_Async(int KPI_Id)
        {
            return objDAL.Get_PI_ListByKPI_Async(KPI_Id);
        }
    }
}
