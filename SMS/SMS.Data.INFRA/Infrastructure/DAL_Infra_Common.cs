using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Common
    {
        static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static SqlDataReader Get_SPM_Module_ID(string ScreenName)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ScreenName",ScreenName),
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "INF_GET_SPM_Module_ID", sqlprm);


        }


        public static DataTable Get_SPM_Module_Stages(int Company_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Company_ID",Company_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_SPM_Modules_Stages", sqlprm).Tables[0];


        }

        //public static int Insert_EmailAttachedFile_DL(int EmailID, string FileName, string FilePath)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@EmailID",EmailID),
        //                                    new SqlParameter("@FileName",FileName),
        //                                    new SqlParameter("@FilePath",FilePath)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INSERT_EmailAttachedFile", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        public static int Insert_EmailAttachedFile_DL(int EmailID, string FileName, string FilePath)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EmailID",EmailID),
                                            new SqlParameter("@FileName",FileName),
                                            new SqlParameter("@FilePath",FilePath)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INSERT_EmailAttachedFile", sqlprm);
        }
        public static DataSet Get_EmailAttachedFile_DL(int EmailID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EmailID",EmailID)
                                          
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_EmailAttachedFile", sqlprm);
        }

        public static int Delete_EmailAttachedFile_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID)
                                           
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DELETE_EmailAttachedFile", sqlprm);
        }

        public static DataTable Get_Record_Information(string Table_Name, string Where)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TABLE_NAME",Table_Name),
                                            new SqlParameter("@WHERE",Where),
                                           
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_RECORD_INFORMATION", sqlprm).Tables[0];

        }
        public static DataTable INF_GET_CREWRECORD_INFORMATION(string Table_Name, string Where)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TABLE_NAME",Table_Name),
                                            new SqlParameter("@WHERE",Where),
                                           
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_CREWRECORD_INFORMATION", sqlprm).Tables[0];

        }
        public static DataTable SearchDMN_UpdatesErrors(int? VesselID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSELID", VesselID),                 
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMN_GET_UPDATESERRORS_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataTable Get_Exception_Search(int? FleetID, int? VesselID, DateTime? dtFrom, DateTime? dtTo,string searchtext, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
       
                new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@VESSELID",VesselID),
                new System.Data.SqlClient.SqlParameter("@FromDate",dtFrom),
                new System.Data.SqlClient.SqlParameter("@ToDate",dtTo),
                new System.Data.SqlClient.SqlParameter("@Exception",searchtext),               
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_EXCEPTIONS_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }


        public static int Upd_User_Vessel_Assignment(int UserID, DataTable tblVessel_ID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@tblVessel_ID",tblVessel_ID),
                                            new SqlParameter("@CREATED_BY",Created_By)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_User_Vessel_Assignment", sqlprm);
        }

        public static DataTable Get_User_Vessel_Assignment(int UserID, int? FleetId,int? CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@FleetId",FleetId),
                                            new SqlParameter("@UserCompanyID",CompanyID),
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_User_Vessel_Assignment", sqlprm).Tables[0];
        }

        //Added by Anjali
        /// <summary>
        /// To retrive tool tip information
        /// </summary>
        /// <param name="UserID">Id of logged in user</param>
        /// <param name="Date">date when record added/modfied</param>
        /// <returns>record information</returns>
        public static DataTable Get_Record_Information_ToolTip(string UserID, string Date)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@USERID",UserID),
                                            new SqlParameter("@DATE",Date),
                                           
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_RECORD_INFORMATION_DETAILS", sqlprm).Tables[0];

        }

         public static DataTable Get_Crew_Information(int CrewID, DateTime? Date)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DATE",Date),
                                           
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_CREW_INFORMATION", sqlprm).Tables[0];
        }

         public static DataTable Get_Crew_Information_ToolTip(string CrewID)
         {
             SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                           
                                        };

             return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_CREW_INFORMATION_DETAILS", sqlprm).Tables[0];

         }

    }
}
