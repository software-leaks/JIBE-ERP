using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.eForms;
using SMS.Data;

namespace SMS.Business.eForms
{
    public class BLL_eForms_Admin
    {
        private static string connection = "";
        public  BLL_eForms_Admin(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static BLL_eForms_Admin()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_ReportIndex(DataTable dtFleetCodes, DataTable dtVessel_ID, DateTime? From_Date, DateTime? To_Date, string SearchText, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_eForms_Admin.Get_ReportIndex(dtFleetCodes, dtVessel_ID, From_Date, To_Date, SearchText, UserID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }



        public static DataTable Get_eFormAssign_Vessel_List(int eFormID)
        {

            return DAL_eForms_Admin.Get_eFormAssign_Vessel_List(eFormID);
        
        
        }

        
        public static DataTable Get_Form_Library()
        {
            return DAL_eForms_Admin.Get_Form_Library();
        }

        public static int INSERT_VESSEL_eForm_ASSIGNMENT(int ID, int Form_ID, int Vessel_ID, int Show_Left_Logo, int Show_Right_Logo, int Created_By, int IsCheck)
        {
            return DAL_eForms_Admin.INSERT_VESSEL_eForm_ASSIGNMENT(ID, Form_ID, Vessel_ID, Show_Left_Logo, Show_Right_Logo, Created_By, IsCheck);
        }

        public static DataSet GET_ReportDetails(int Main_Report_ID, int Vessel_ID,string ProcedureName)
        {

            return DAL_eForms_Admin.GET_ReportDetails(Main_Report_ID, Vessel_ID, ProcedureName);
        }




    }
}
