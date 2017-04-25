using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.eForms
{
    public class DAL_FRM_LIB_DrillReport
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_FRM_LIB_DrillReport()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataTable Drill_Report_QuestionDetails_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                 
                                  

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_LIB_Drill_Report_QuestionDeatails_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public int Insert_Drill_Report_QuestionDetails(string Question, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Question",Question),
                                           
                                          
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_LIB_Insert_Drill_Report_QuestionDeatails", sqlprm);
        }

        public int Edit_Drill_Report_QuestionDetails(int ID, string Question, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Question",Question),
                                         
                                           
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_LIB_Update_Drill_Report_QuestionDeatails", sqlprm);
        }

        public DataTable Get_Drill_Report_QuestionDetails_DL(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_LIB_Drill_Report_QuestionDeatails_List", obj);

            return ds.Tables[0];
        }

        public int Drill_Report_QuestionDetails_DL(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                           
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_LIB_Delete_Drill_Report_QuestionDeatails", sqlprm);
        }
    }
}
