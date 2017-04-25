using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace SMS.Data.QMSDB
{
    public class DAL_QMS_RestHours
    {
        private static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;



        public static DataTable Get_RestHours_Index(int? FleetID, int? VesselID, string Crewdetails, string crewrank, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
       
                //new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@VESSELID",VesselID),
                  new System.Data.SqlClient.SqlParameter("@Crew",Crewdetails),
                new System.Data.SqlClient.SqlParameter("@CrewRank",crewrank),
                new System.Data.SqlClient.SqlParameter("@FromDate",dtFrom),
                new System.Data.SqlClient.SqlParameter("@ToDate",dtTo),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOURS_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }

        public static DataTable Get_RestHours_OverTime(int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
       
                //new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",VesselID),
                new System.Data.SqlClient.SqlParameter("@FROMDATE",dtFrom),
                new System.Data.SqlClient.SqlParameter("@TODATE",dtTo),
                  new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOURS_OVERTIME", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }




        public static DataTable Get_RestHours_Details(int ID, int Vessel_ID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID",ID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOURS_EDIT", obj);
            return ds.Tables[0];
        }

        public static DataSet GetRestHoursRulesSearch(string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SEARCHDETAILS", SearchText),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOUR_RULES_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public static int Insert_RestHours_Rules(string description, int? Rule_Value, int? Rule_Period, string rule_unit, int? Rules_TYPE, int? userid)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RULE_DESCRIPTION",description),
                new System.Data.SqlClient.SqlParameter("@Rule_Value",Rule_Value),
                 new System.Data.SqlClient.SqlParameter("@Rule_Period",Rule_Period),
                new System.Data.SqlClient.SqlParameter("@Rule_Unit",rule_unit),
                 new System.Data.SqlClient.SqlParameter("@Rules_TYPE",Rules_TYPE),
                new System.Data.SqlClient.SqlParameter("@Created_By",userid),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CRW_INS_RESTHOUR_RULES", obj);

        }

        public static int Update_RestHours_Rules(int ID, string description, int? Rule_Value, int? Rule_Period, string rule_unit, int? Rules_TYPE, int? userid)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID",ID),
                new System.Data.SqlClient.SqlParameter("@RULE_DESCRIPTION",description),
                new System.Data.SqlClient.SqlParameter("@Rule_Value",Rule_Value),
                 new System.Data.SqlClient.SqlParameter("@Rule_Period",Rule_Period),
                new System.Data.SqlClient.SqlParameter("@Rule_Unit",rule_unit),
                 new System.Data.SqlClient.SqlParameter("@Rules_TYPE",Rules_TYPE),
                new System.Data.SqlClient.SqlParameter("@Created_By",userid),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CRW_UPDATE_RESTHOUR_RULES", obj);
        }


        public static DataTable Get_RestHours_Rules_Details(int? ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID",ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOUR_RULES_EDIT", obj).Tables[0];
        }

        public static int Delete_RestHours_Rules(int? ID, int userid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID" ,ID),          	 
               new System.Data.SqlClient.SqlParameter("@USERID",userid)
		    	
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CRW_DELETE_CREW_RESTHOUR_RULES", obj);
        }

        public static DataTable Get_RestHours_Report(DateTime? DateFrom, DateTime? DateTo, int CrewID, int Vessel_ID)
        {



            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@DateFrom",DateFrom),
                new System.Data.SqlClient.SqlParameter("@DateTo",DateTo),
                new System.Data.SqlClient.SqlParameter("@CrewID",CrewID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOURS_Report", obj);
            return ds.Tables[0];
        }

        public static DataTable CrewList_by_Date(DateTime? DateFrom, DateTime? DateTo, int Vessel_ID)
        {



            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@DateFrom",DateFrom),
                new System.Data.SqlClient.SqlParameter("@DateTo",DateTo),
               
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_CrewList_by_Date", obj);
            return ds.Tables[0];
        }


        public static DataTable Get_RestHours_Exceptions(int RestHourID, int Vessel_ID)
        {



            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@RestHourID",RestHourID),       
                new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_GET_RESTHOURS_EXCEPTION", obj);
            return ds.Tables[0];
        }


    }
}

