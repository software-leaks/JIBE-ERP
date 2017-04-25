using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.eForms
{
    public class DAL_eForms_Admin
    {
        private static string connection = "";
        public DAL_eForms_Admin(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_eForms_Admin()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public static DataTable Get_ReportIndex(DataTable dtFleetCodes, DataTable dtVessel_ID, DateTime? From_Date, DateTime? To_Date, string SearchText, int UserID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
           
                new System.Data.SqlClient.SqlParameter("@dtFleetCodes",dtFleetCodes),
                new System.Data.SqlClient.SqlParameter("@dtVessel_ID",dtVessel_ID),
                new System.Data.SqlClient.SqlParameter("@From_Date",From_Date),
                new System.Data.SqlClient.SqlParameter("@To_Date",To_Date),
                new System.Data.SqlClient.SqlParameter("@SearchText",SearchText),
                new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                                                        
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
   				};

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataTable ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_ReportIndex", obj).Tables[0];
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


       
        public static DataTable Get_eFormAssign_Vessel_List(int eFormID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]{  
                                                         new SqlParameter("@eFormID",eFormID),
                                                      };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_eForm_Assignment_Vessel", sqlprm).Tables[0];
        }




        public static DataTable Get_Form_Library()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_FORM_LIBRARY").Tables[0];
        }


        public static int INSERT_VESSEL_eForm_ASSIGNMENT(int ID, int Form_ID, int Vessel_ID, int Show_Left_Logo, int Show_Right_Logo, int Created_By, int IsCheck)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{  
                                                      new SqlParameter("@ID",ID),
                                                      new SqlParameter("@Form_ID",Form_ID),
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@Show_Left_Logo",Show_Left_Logo),
                                                      new SqlParameter("@Show_Right_Logo",Show_Right_Logo),
                                                      new SqlParameter("@Created_By",Created_By),
                                                      new SqlParameter("@IsCheck",IsCheck)
   												  };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_INS_VESSEL_eForm_ASSIGNMENT", sqlprm);
        }

        public static DataSet GET_ReportDetails(int Main_Report_ID, int Vessel_ID,string ProcedureName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
										{ 
											new SqlParameter("@Main_Report_ID",Main_Report_ID),
											new SqlParameter("@Vessel_ID",Vessel_ID)
										};
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, ProcedureName, sqlprm);

        }

    }
}
