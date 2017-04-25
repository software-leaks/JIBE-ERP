using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.QMSDB;

namespace SMS.Business.QMSDB
{
    public class  BLL_QMS_RestHours
    {
              
        
        public static DataTable Get_RestHours_Index(int? FleetID, int? VesselID, string Crewdetails,string crewrank,  DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_QMS_RestHours.Get_RestHours_Index(FleetID, VesselID, Crewdetails, crewrank, dtFrom, dtTo, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable Get_RestHours_OverTime(int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMS_RestHours.Get_RestHours_OverTime(FleetID, VesselID, dtFrom, dtTo,sortby,sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable Get_RestHours_Details(int ID, int Vessel_ID)
        {
            return DAL_QMS_RestHours.Get_RestHours_Details(ID , Vessel_ID);
        }

        public static DataSet GetRestHoursRulesSearch(string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_QMS_RestHours.GetRestHoursRulesSearch(SearchText, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static int Insert_RestHours_Rules(string description, int? Rule_Value, int? Rule_Period, string rule_unit, int? Rules_TYPE, int? userid)
        {
            return DAL_QMS_RestHours.Insert_RestHours_Rules(description, Rule_Value, Rule_Period, rule_unit, Rules_TYPE, userid);
        }
       
        public static int Update_RestHours_Rules(int ID, string description, int? Rule_Value, int? Rule_Period, string rule_unit, int? Rules_TYPE, int? userid)
        {
            return DAL_QMS_RestHours.Update_RestHours_Rules(ID, description, Rule_Value, Rule_Period, rule_unit, Rules_TYPE, userid);
        }

        public static DataTable Get_RestHours_Rules_Details(int? ID)
        {
            return DAL_QMS_RestHours.Get_RestHours_Rules_Details(ID);
        }


        public static int Delete_RestHours_Rules(int? ID, int userid)
        {
            return DAL_QMS_RestHours.Delete_RestHours_Rules(ID, userid);
        }

        public static DataTable Get_RestHours_Report(DateTime? DateFrom, DateTime? DateTo, int CrewID, int Vessel_ID)
        {
            return DAL_QMS_RestHours.Get_RestHours_Report(DateFrom, DateTo, CrewID, Vessel_ID);
        }
        public static DataTable CrewList_by_Date(DateTime? DateFrom, DateTime? DateTo, int Vessel_ID)
        {
            return DAL_QMS_RestHours.CrewList_by_Date(DateFrom, DateTo, Vessel_ID);
        }

        public static DataTable Get_RestHours_Exceptions(int RestHourID, int Vessel_ID)
        {
            return DAL_QMS_RestHours.Get_RestHours_Exceptions(RestHourID, Vessel_ID);
        }

        
    }
}

  
