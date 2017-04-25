using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALReports
/// </summary>
/// 
namespace SMS.Data.PURC
{
    public class DAL_PURC_Reports
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        static string _stinternalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_Reports()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        


        public DataTable GetReportBondItemCharters(string VesselCode, string FromDate, string ToDate)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               //new System.Data.SqlClient.SqlParameter("@Month",Month), 
               new System.Data.SqlClient.SqlParameter("@FromDate",FromDate),
               new System.Data.SqlClient.SqlParameter("@Todate",ToDate ),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)   
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_BondItem_Charteres", obj);
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetReportBondItemOwner(string VesselCode, string FromDate, string ToDate)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               //new System.Data.SqlClient.SqlParameter("@Month",Month), 
               new System.Data.SqlClient.SqlParameter("@FromDate",FromDate),
               new System.Data.SqlClient.SqlParameter("@Todate",ToDate ),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)   
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_BondItem_Ownner", obj);
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataSet GetReportProvisionItemCharters(string VesselCode, string FromDate, string ToDate)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               //new System.Data.SqlClient.SqlParameter("@Month",Month), 
               new System.Data.SqlClient.SqlParameter("@FromDate",FromDate),
               new System.Data.SqlClient.SqlParameter("@Todate",ToDate ),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)  
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_Provision_Charteres", obj);
                //dtDept = ds.Tables[0]; 
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataSet GetReportProvisionItemOwner(string VesselCode, string FromDate, string ToDate)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 

              //new System.Data.SqlClient.SqlParameter("@Month",Month), 
               new System.Data.SqlClient.SqlParameter("@FromDate",FromDate),
               new System.Data.SqlClient.SqlParameter("@Todate",ToDate ),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)    
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_Provision_Owner", obj);
                //dtDept = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataSet GetReportBondProvision_D11(string FromDate, string Todate, string VesselCode)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@FromDate",FromDate),
             new System.Data.SqlClient.SqlParameter("@Todate", Todate),
             new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode) 
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_Bond_Pro_D11", obj);
                //dtDept = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetReportC_2Mandays(string FromDate, string Todate, string VesselCode)
        {
            try
            {
                DataSet dtDept = new System.Data.DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@FromDate",FromDate),
             new System.Data.SqlClient.SqlParameter("@Todate", Todate),
             new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode) 
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_C2_Mandays", obj);
                //dtDept = ds.Tables[0]; 
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        public DataSet GetReportProvisionInventory(string VesselCode, string DeptCode)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@Vessel_Code",VesselCode),
             new System.Data.SqlClient.SqlParameter("@Dept_Code", DeptCode),
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_ProvisionInventory", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataSet GetReqItemsPreview(string ReqCode, string VesselCode, string Documentcode)
        {
            try
            {
                DataSet dtDept = new System.Data.DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@ReqsCode",ReqCode),
             new System.Data.SqlClient.SqlParameter("@Vessel_Code", VesselCode), 
              new System.Data.SqlClient.SqlParameter("@Document_code", Documentcode) 
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Report_ReqItemPreview", obj);
                //dtDept = ds.Tables[0]; 
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet ConfiguredSupplierPreview( string Document_code, string ReqsCode,string Searchtext)
        {
            try
            {
                DataSet dtDept = new System.Data.DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@Document_code",Document_code),
             new System.Data.SqlClient.SqlParameter("@ReqsCode",ReqsCode),
             new System.Data.SqlClient.SqlParameter("@Searchtext",Searchtext)
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_ConfiguredSupplierPreview", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        internal DataSet Rpt_DailyNoon_Details()
        {
            throw new NotImplementedException();
        }

        internal DataSet Rpt_DailyNoon_Details_Performance()
        {
            throw new NotImplementedException();
        }

        internal DataSet Rpt_SunWed_Get_Details()
        {
            throw new NotImplementedException();
        }

        internal DataSet getSunWedDetailsByID(int ID)
        {
            throw new NotImplementedException();
        }

        internal DataSet getArrivalDetailsByID(int ID)
        {
            throw new NotImplementedException();
        }

        internal DataSet getDepartureDetailsByID(int ID)
        {
            throw new NotImplementedException();
        }

        internal DataSet getMonthendPerformanceDetailsByID(int ID)
        {
            throw new NotImplementedException();
        }

        internal DataSet getQuerterlyPerformanceDetailsByID(int ID)
        {
            throw new NotImplementedException();
        }

        internal DataSet Rpt_Departure_Get_Details()
        {
            throw new NotImplementedException();
        }

        internal DataSet Rpt_QuarterlyPerformanceDetails()
        {
            throw new NotImplementedException();
        }

        internal DataSet Rpt_Arrival_Get_Details()
        {
            throw new NotImplementedException();
        }

        internal DataSet Rpt_MonthEndPerformanceDetails()
        {
            throw new NotImplementedException();
        }

        internal DataSet Search_MonthEndPerformanceDetails(string str1, string str2, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        internal SqlDataReader Rpt_DailyNoonIndividual_Details(int id, int vesselid)
        {
            throw new NotImplementedException();
        }

        internal DataSet Search_Rpt_DailyNoon_Details(string str1, string str2, string str3, string str4, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        internal DataSet Search_Rpt_SunWed_Details(string str1, string str2, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        internal DataSet Search_Rpt_Arrival_Details(string str1, string str2, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        internal DataSet Search_Rpt_Departure_Details(string str1, string str2, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        internal DataSet Search_Rpt_QuarterlyPerformanceDetails(string str1, string str2, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        internal SqlDataReader ddlLocaiton_fill()
        {
            throw new NotImplementedException();
        }

        internal SqlDataReader ddlVesselName_fill()
        {
            throw new NotImplementedException();
        }

        internal SqlDataReader ddlFleet_fill()
        {
            throw new NotImplementedException();
        }

        internal DataSet ddlVesselName()
        {
            throw new NotImplementedException();
        }

      
      

    }
}