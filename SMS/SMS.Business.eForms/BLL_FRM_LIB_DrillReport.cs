using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.eForms;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;

namespace SMS.Business.eForms
{
    public class BLL_FRM_LIB_DrillReport
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        DAL_FRM_LIB_DrillReport objDAL = new DAL_FRM_LIB_DrillReport();
        public DataTable Drill_Report_QuestionDetails_Searchh(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Drill_Report_QuestionDetails_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int Insert_Drill_Report_QuestionDetails(string Question, int CreatedBy)
        {
            return objDAL.Insert_Drill_Report_QuestionDetails(Question, CreatedBy);
        }

        public int Edit_Drill_Report_QuestionDetails(int ID, string Question, int CreatedBy)
        {
            return objDAL.Edit_Drill_Report_QuestionDetails(ID, Question, CreatedBy);
        }
        public DataTable Get_Drill_Report_QuestionDetails(int? ID)
        {
            return objDAL.Get_Drill_Report_QuestionDetails_DL(ID);
        }

        public int Delete_Drill_Report_QuestionDetails(int ID, int CreatedBy)
        {
            return objDAL.Drill_Report_QuestionDetails_DL(ID, CreatedBy);
        }

        // Report

        public static DataSet Get_DRILL_REPORT_Log(int Vessel_ID, int Schedule_Id, int? Office_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                                                                     
                                            new SqlParameter("@Schedule_Id",Schedule_Id),  
                                            new SqlParameter("@Office_Id",Office_Id),  
                                             
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Drill_Activity_Details_Reports", sqlprm);

        }
    }
}
