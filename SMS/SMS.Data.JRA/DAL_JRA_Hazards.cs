using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SMS.Data.JRA
{
    public class DAL_JRA_Hazards
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public static DataSet GET_ASSESSMENT(int Assessment_ID, int Vessel_ID, int UserID, int? Assessment_Dtl_ID, int? Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                        new SqlParameter("@Assessment_ID",Assessment_ID) ,        
                        new SqlParameter("@Vessel_ID",Vessel_ID) ,        
                          new SqlParameter("@Assessment_Dtl_ID",Assessment_Dtl_ID) ,        
                             new SqlParameter("@Office_ID",Office_ID) ,  
                                new SqlParameter("@UserID",UserID) ,  
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_ASSESSMENT", sqlprm);
            return ds;
        }
        public static void DEL_ASSESSMENT_DETAILS(int Assessemnt_Dtl_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                        new SqlParameter("@Assessemnt_Dtl_ID",Assessemnt_Dtl_ID) ,        
                        new SqlParameter("@Vessel_ID",Vessel_ID) ,        
                        new SqlParameter("@Office_ID",Office_ID) , 
                          new SqlParameter("@UserID",UserID) ,        
                        
            };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_DEL_ASSESSMENT_DETAILS", sqlprm);

        }
        public static void DEL_HAZARD_TRMPLATE(int Hazard_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                        new SqlParameter("@Hazard_ID",Hazard_ID) ,        
                        new SqlParameter("@UserID",UserID) ,        
                        
                       
            };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_DEL_HAZARD_TRMPLATE", sqlprm);

        }

        public static int INSUPD_HAZARD_TRMPLATE(int? Hazard_ID, int Work_Categ_ID, string Hazard_Description, string Control_Measure, int Severity_ID, int Likelihood_ID, int Initial_Risk_Value, string Additional_Control_Measures, string Modified_Risk_Value, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                    new SqlParameter("@Hazard_ID",Hazard_ID) ,        
                    new SqlParameter("@Work_Categ_ID",Work_Categ_ID) ,   
                    new SqlParameter("@Hazard_Description",Hazard_Description) , 
                    new SqlParameter("@Control_Measure",Control_Measure) , 
                    new SqlParameter("@Severity_ID",Severity_ID) , 
                    new SqlParameter("@Likelihood_ID",Likelihood_ID) , 
                    new SqlParameter("@Initial_Risk_Value",Initial_Risk_Value) ,   
                    new SqlParameter("@Additional_Control_Measures",Additional_Control_Measures) ,                  
                    new SqlParameter("@Modified_Risk_Value",Modified_Risk_Value) ,  
                    new SqlParameter("@UserID",UserID) ,    
                    new SqlParameter("@return", SqlDbType.Int)
                        
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INSUPD_HAZARD_TRMPLATE", sqlprm);
            int ID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ID;
        }
        public static DataSet JRA_GET_MODIFIED_RISKS()
        {

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_MODIFIED_RISKS", null);
            return ds;
        }

        public static DataSet GET_RISK_RATINGS(int? Rating_Value)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                        new SqlParameter("@Rating_Value",Rating_Value)          
                       
                       
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_RISK_RATINGS", sqlprm);
            return ds;
        }
        public static DataSet GET_TYPE(string Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                        new SqlParameter("@Type",Type)          
                       
                       
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_TYPE", sqlprm);
            return ds;
        }
        public static DataSet GET_HAZARD_TEMPLATE_LIST(string Hazard_ID, string Work_Categ_ID, string Search, int? PAGE_INDEX, int? PAGE_SIZE, string SortBy, string SORT_DIRECTION, ref int Rowcnt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                        new SqlParameter("@Hazard_ID",Hazard_ID) ,        
                        new SqlParameter("@Work_Categ_ID",Work_Categ_ID) , 
                        new SqlParameter("@Search",Search) , 
                        new SqlParameter("@PAGE_INDEX",PAGE_INDEX) , 
                        new SqlParameter("@PAGE_SIZE",PAGE_SIZE) , 
                        new SqlParameter("@SortBy",SortBy) , 
                        new SqlParameter("@SORT_DIRECTION",SORT_DIRECTION) , 
                         new SqlParameter("@return", SqlDbType.Int)
                        
                       
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_HAZARD_TEMPLATE_LIST", sqlprm);
            Rowcnt = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);



            return ds;
        }


        public static DataSet GET_ASSESSMENT_SEARCH(string Vesse_ID, string Assessment_ID, string Work_Categ_ID, string Assessment_Status, DateTime? From_Date, DateTime? To_Date, string Search, int UserID, int? PAGE_INDEX, int? PAGE_SIZE, string SortBy, string SORT_DIRECTION, ref int Rowcnt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                         new SqlParameter("@Vessel_ID",Vesse_ID) ,       
                        new SqlParameter("@Assessment_ID",Assessment_ID) ,        
                        new SqlParameter("@Work_Categ_ID",Work_Categ_ID) , 
                        new SqlParameter("@Assessment_Status",Assessment_Status) , 
                        new SqlParameter("@From_Date",From_Date) , 
                        new SqlParameter("@To_Date",To_Date) , 
                        new SqlParameter("@Search",Search) , 
                          new SqlParameter("@UserID",UserID) , 
                        new SqlParameter("@PAGE_INDEX",PAGE_INDEX) , 
                        new SqlParameter("@PAGE_SIZE",PAGE_SIZE) , 
                        new SqlParameter("@SortBy",SortBy) , 
                        new SqlParameter("@SORT_DIRECTION",SORT_DIRECTION) , 
                        new SqlParameter("@return", SqlDbType.Int)
                        
                       
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_ASSESSMENT_SEARCH", sqlprm);
            Rowcnt = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);



            return ds;
        }










        public static void UPD_ASSESSMENT_STATUS(int Assessment_ID, int Work_categ_ID, int Vessel_ID, string Status, string Remark, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                    new SqlParameter("@Assessment_ID",Assessment_ID) ,        
                    new SqlParameter("@Work_categ_ID",Work_categ_ID) ,        
                    new SqlParameter("@Vessel_ID",Vessel_ID) ,   
                     new SqlParameter("@UserID",UserID) , 
                    new SqlParameter("@Status",Status) ,        
                    new SqlParameter("@Remark",Remark) ,        
                       
            };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_UPD_ASSESSMENT_STATUS", sqlprm);

        }


        public static int INSUPD_ASSESSMENT(int Assessment_ID, int? Assessment_Dtl_ID, int Vessel_ID, int UserID, int? Hazard_ID, string Hazard_Description, string Control_Measure,
            string Severity_ID, string Likelihood_ID,
          string Severity, string Likelihood, string Initial_Risk, string Initial_Risk_Value, string Initial_Risk_Color,
            string Additional_Control_Measures,
            string Modified_Risk, string Modified_Risk_Value, string Modified_Risk_Color, int? Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
            new SqlParameter("@Assessment_ID",Assessment_ID) ,     
            new SqlParameter("@Assessment_Dtl_ID",Assessment_Dtl_ID) ,     
            new SqlParameter("@Vessel_ID",Vessel_ID) ,     
            new SqlParameter("@UserID",UserID) ,     
            new SqlParameter("@Hazard_ID",Hazard_ID) ,                        
            new SqlParameter("@Hazard_Description",Hazard_Description) ,         
            new SqlParameter("@Control_Measure",Control_Measure) , 
            new SqlParameter("@Severity_ID",Severity_ID) , 
            new SqlParameter("@Likelihood_ID",Likelihood_ID) , 
            new SqlParameter("@Severity",Severity) , 
            new SqlParameter("@Likelihood",Likelihood) , 
            new SqlParameter("@Initial_Risk",Initial_Risk) ,   
            new SqlParameter("@Initial_Risk_Value",Initial_Risk_Value) ,   
            new SqlParameter("@Initial_Risk_Color",Initial_Risk_Color) ,   
            new SqlParameter("@Additional_Control_Measures",Additional_Control_Measures) ,  
            new SqlParameter("@Modified_Risk",Modified_Risk) ,  
            new SqlParameter("@Modified_Risk_Value",Modified_Risk_Value) ,  
            new SqlParameter("@Modified_Risk_Color",Modified_Risk_Color) ,  
            new SqlParameter("@Office_ID",Office_ID) ,    
            new SqlParameter("@return", SqlDbType.Int) 
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INSUPD_ASSESSMENT", sqlprm);
            int ID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ID;
        }

        public static DataSet Get_Approvar(int Work_Categ_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Work_Categ_ID",Work_Categ_ID)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_Get_Approvar", sqlprm);


        }
        public static DataSet Get_ApprovarByLevel(int Work_Categ_ID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                            new SqlParameter("@Work_Categ_ID",Work_Categ_ID),
                                             new SqlParameter("@ApprovalLevel",ApprovalLevel)
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_Get_ApprovarByLevel", sqlprm);


        }

        public static int Insert_Approvar(int Work_Categ_ID, int ApprovalLevel, int ApprovarID, int CreatedBy, int Mode, DataTable ApprovarIDTBL)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@Work_Categ_ID",Work_Categ_ID),
                                          new SqlParameter("@ApprovalLevel",ApprovalLevel),
                                          new SqlParameter("@ApprovarID",ApprovarID),
                                          new SqlParameter("@CreatedBy",CreatedBy),
                                           new SqlParameter("@Mode",Mode),
                                               new SqlParameter("@ApprovarIDTBL",ApprovarIDTBL),
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INS_Approvar", sqlprm);
        }
        public static int Insert_ApprovalLevels(int Work_Categ_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@Work_Categ_ID",Work_Categ_ID),
                                          new SqlParameter("@CreatedBy",CreatedBy)
                                         
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INS_ApprovalLevels", sqlprm);
        }
        public static int Insert_REMARKS(int Assessment_ID, int Vessel_ID, int UserID, string Remarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                    new SqlParameter("@Assessment_ID",Assessment_ID) ,        
                    new SqlParameter("@Vessel_ID",Vessel_ID) ,   
                    new SqlParameter("@Remark",Remarks) , 
                    new SqlParameter("@UserID",UserID) ,    
                    new SqlParameter("@return", SqlDbType.Int)
                        
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INS_REMARKS", sqlprm);
            int RemarkID = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return RemarkID;
        }

        public static DataSet GET_REMARKS(int? Assessment_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
               
                        new SqlParameter("@Assessment_ID",Assessment_ID) ,        
                        new SqlParameter("@Vessel_ID",Vessel_ID) ,        
                    
                  
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_REMARKS", sqlprm);
            return ds;
        }

        public static int UPD_Approvar(int Work_Categ_ID, int ApprovalLevel)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@Work_Categ_ID",Work_Categ_ID),
                                          new SqlParameter("@ApprovalLevel",ApprovalLevel)
                                         
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_UPD_Approvar", sqlprm);
        }


        public static DataSet COPY_APPROVAL(int From_Work_Categ_ID, DataTable To_Work_Categ_ID, int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@From_Work_Categ_ID",From_Work_Categ_ID),
                                          new SqlParameter("@To_Work_Categ_ID",To_Work_Categ_ID),
                                            new SqlParameter("@User_ID",User_ID)

                                          
                                         
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_COPY_APPROVAL", sqlprm);
        }
        public static DataTable GET_WORK_CATEGORY_LIST(int? Work_Categ_Parent_ID,int Mode,int Active_Status)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@Work_Categ_Parent_ID",Work_Categ_Parent_ID),
                 new SqlParameter("@Mode",Mode),    
                  new SqlParameter("@Active_Status",Active_Status),    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_WORK_CATEGORY_LIST", sqlprm).Tables[0];

        }

        public static DataTable GET_Sev_Cons(int Seveity_ID, int Consequence_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@Severity_ID",Seveity_ID),
                 new SqlParameter("@Consequence_ID",Consequence_ID),    
                 
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "JRA_GET_SEVERITY_CONSEQENCE", sqlprm).Tables[0];

        }

        public static void INSUPD_Sev_Cons(int Seveity_ID, int Consequence_ID, string SC_Description,int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@Severity_ID",Seveity_ID),
                 new SqlParameter("@Consequence_ID",Consequence_ID),    
                 new SqlParameter("@SC_Description",SC_Description),    
                 new SqlParameter("@UserID",UserID),    
                 
            };
              SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "JRA_INSUPD_SEVERITY_CONSEQENCE", sqlprm);

        }
    }
}
