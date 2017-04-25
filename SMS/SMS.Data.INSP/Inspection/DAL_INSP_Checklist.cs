using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Inspection
{
    public class DAL_INSP_Checklist
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private static string connection = "";
        public DAL_INSP_Checklist(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_INSP_Checklist()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }

        public DataTable Get_CurrentCheckList_DL(int CheckList_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID),               
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_Checklist", obj);
            return ds.Tables[0];
        }

        public DataTable Get_CheckListALL_DL(string searchtext, int? checklistType, int? VesselType, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext), 
                   new System.Data.SqlClient.SqlParameter("@checklistType", checklistType),   
                   new System.Data.SqlClient.SqlParameter("@VesselType", VesselType), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY", sortby),   
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION", sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER", pagenumber),   
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE", pagesize), 
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT", isfetchcount),   
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ALLChecklist", obj);
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ALLChecklist");
            isfetchcount = int.Parse(obj[obj.Length - 1].Value.ToString());
            return ds.Tables[0];
        }


        public DataTable Get_CheckListItemsQB_DL(int CheckList_ID, int Parent_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID), 
                   new System.Data.SqlClient.SqlParameter("@Parent_ID", Parent_ID),
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_CHKItems", obj);
            return ds.Tables[0];
        }

        public DataTable Get_CheckListItemsQB_DL(int CheckList_ID, int Parent_ID, string desription)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID), 
                   new System.Data.SqlClient.SqlParameter("@Parent_ID", Parent_ID),
                   new System.Data.SqlClient.SqlParameter("@Description", desription),
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_CHKItems_BY_Description", obj);
            return ds.Tables[0];
        }

        public DataTable Get_CheckListName_DL(int ScheduleID, int Inspid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ScheduleID", ScheduleID),  
                   new System.Data.SqlClient.SqlParameter("@Inspid", Inspid),
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_ChecklistForRating_Dtl", obj);
            return ds.Tables[0];
        }

        public DataTable Get_CheckListLocations_DL(int VESSEL_Type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   //new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID), 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_Type", VESSEL_Type),
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_SUBSYTEMSYSTEM_LOCATION", obj);
            return ds.Tables[0];
        }

        public DataTable Get_Functional_Tree_Checklist(string Function_ID, int Vessel_ID, string Equipment_Type)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Function", Function_ID),
                 //new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                 //new SqlParameter("@Equipment_Type",Equipment_Type)
                  
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data_ManageSystem", obj).Tables[0];

        }

        //All functions related to checklist type  INSERT UPDATE DELETE 

        public DataTable Get_CheckListType_DL(int? CHKID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CHKID", CHKID),                  
                    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_LIB_Get_CheckListType", obj).Tables[0];

        }

        public DataTable Get_Search_CheckListType_DL(string SearchText)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                           
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_CheckListTypeList", obj);

            DataTable dt = ds.Tables[0];
            //DataTable dt1 = ds.Tables[1];

            //dt.Merge(dt1);

            return dt;

            //  return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_CheckListTypeList", obj).Tables[0];

        }

        public DataTable Get_SheduleCheckList_DL(int CheckList_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID),               
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_SheduledChecklist", obj);
            return ds.Tables[0];
        }

        public int DELETE_ChecklistType_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_LIB_Delete_ChecklistType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_ChecklistType_DL(int ID, string EvaluationType, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@EvaluationType",EvaluationType),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_LIB_Update_ChecklistType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INSERT_ChecklistType_DL(string EvaluationType, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EvaluationType",EvaluationType),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_LIB_Insert_ChecklistType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        // Checklist type functions Ends here

        //All functions related to Gradings and options

        public DataTable Get_GradeswithOptions_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_Grades").Tables[0];

        }

        public DataTable Get_LocGradeType_DL(int? CheckList_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CHKID", CheckList_ID),               
                    
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_LIB_Get_Grading", obj).Tables[0];

        }

        public DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_GradingOptions", sqlprm).Tables[0];
        }

        public int INSERT_Grading_DL(string Grading_Name, int Grade_Type, int Min, int Max, int Divisions, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_Name",Grading_Name),
                                            new SqlParameter("@Grade_Type",Grade_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Insert_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int UPDATE_Grading_DL(int ID, string Grading_Name, int Grading_Type, int Min, int Max, int Divisions, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Grade_Name",Grading_Name),
                                            new SqlParameter("@Grade_Type",Grading_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Update_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Grading_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Delete_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INSERT_GradingOption_DL(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID),
                                            new SqlParameter("@OptionText",OptionText),
                                            new SqlParameter("@OptionValue",OptionValue),                                            
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Insert_GradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_GradingOption_DL(int Option_ID, string OptionText, decimal OptionValue, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Option_ID",Option_ID),
                                            new SqlParameter("@OptionText",OptionText),
                                            new SqlParameter("@OptionValue",OptionValue),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Update_GradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_GradingOptions_DL(int Grade_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_GradingOptions", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        //Gradings and options functions ends Here

        #region QuestionBank
        //public DataTable Get_QuestionList_DL(string SearchText, int CategoryID)
        public DataTable Get_QuestionList_DL(string SearchText, int CategoryID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Category_ID",CategoryID),
                                            new SqlParameter("@SORTBY",sortby), 
                                            new SqlParameter("@SORTDIRECTION",sortdirection), 
                                            new SqlParameter("@PAGENUMBER",pagenumber),
                                            new SqlParameter("@PAGESIZE",pagesize),
                                            new SqlParameter("@ISFETCHCOUNT",isfetchcount),
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_LIB_Checklist", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }

        public int INSERT_Question_DL(string Criteria, int CatID, int Grading_Type, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Criteria",Criteria),
                                            new SqlParameter("@Category_ID",CatID),
                                            new SqlParameter("@Grading_Type",Grading_Type),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Insert_LIB_ChecklistItem", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_Question_DL(int ID, string Criteria, int CatID, int Grading_Type, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Criteria",Criteria),
                                            new SqlParameter("@Category_ID",CatID),
                                            new SqlParameter("@Grading_Type",Grading_Type),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Update_LIB_ChecklistItem", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Question_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Delete_LIB_ChecklistItem", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion

        public int INS_Checklist_DL(int? Checklist_ID, int? Parent_ID, string Version, string CheckList_Name, int Vessel_Type, int checklist_Type, int Location_Grade, int? Created_By, int? Modified_By, int Status)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Checklist_ID",Checklist_ID),
                                            new SqlParameter("@Parent_ID",Parent_ID),
                                            new SqlParameter("@Version",Version),
                                            new SqlParameter("@CheckList_Name",CheckList_Name),
                                            new SqlParameter("@Vessel_Type",Vessel_Type),
                                            new SqlParameter("@CheckList_Type",checklist_Type),
                                            new SqlParameter("@Grading_Type",Location_Grade),                                          
                                            new SqlParameter("@Created_By",Created_By),    
                                            new SqlParameter("@Modified_By",Modified_By),  
                                            new SqlParameter("@Status",Status), 

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_CheckList", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int INS_Publish_Checklist_DL(int? Checklist_ID, int? Created_By, int Status)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Checklist_ID",Checklist_ID),   
                                             new SqlParameter("@Modified_By",Created_By),   
                                            new SqlParameter("@Status",Status), 

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Publish_CheckList", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int INS_UpdateSheduleAndChecklist_DL(int? Checklist_ID, DataTable table, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Checklist_ID",Checklist_ID), 
                                              new SqlParameter("@ChecklistAndShedulID",table),   
                                             new SqlParameter("@Created_By",Created_By),   
                                            

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_UpdateSheduleChecklist", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_Edit_Checklist_DL(int? Checklist_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Checklist_IDPara",Checklist_ID),   
                                            // new SqlParameter("@Modified_By",Created_By),   
                                            //new SqlParameter("@Status",Status), 

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Edit_CheckList", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_ChecklistItems_DL(int? ID, int? Parent_Id, int Checklist_ID, int? Location_ID, DataTable dtCHKItemID, string Node_Type, string Description, int? Created_By, int? Modified_By, int? Active_Status)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Parent_Id",Parent_Id),
                                            new SqlParameter("@Checklist_ID",Checklist_ID),
                                            new SqlParameter("@Location_ID",Location_ID),
                                            
                                            new SqlParameter("@Checklist_Item_ID",dtCHKItemID),
                                            
                                            
                                            
                                            new SqlParameter("@Node_Type",Node_Type),
                                            new SqlParameter("@Description",Description),                                                                                    
                                            new SqlParameter("@Created_By",Created_By),    
                                            new SqlParameter("@Modified_By",Modified_By),   
                                             new SqlParameter("@Active_Status",Active_Status),   

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_CheckListItems", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Save_INSP_ScheduleAndChecklist_DL(int ChecklistID, int ScheduleID, int Created_By, int Active_Status)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@ChecklistID",ChecklistID),
                new SqlParameter("@ScheduleID",ScheduleID),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("@Active_Status",Active_Status),
                //new SqlParameter("return",SqlDbType.Int)
            };
            //sqlprm[0].SqlDbType = SqlDbType.Structured;
            //sqlprm[1].SqlDbType = SqlDbType.Structured;
            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_WL_Save_InspectionScheduleChecklist", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_Edit_ChecklistOnVesselType_DL(int? Vessel_ID, int? Shedule_ID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@Shedule_ID",Shedule_ID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_ChecklistOnVesselTypeEdit", sqlprm).Tables[0];


        }

        public DataTable Get_ChecklistOnVesselType_DL(int? Vessel_ID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                //new SqlParameter("@Fleet_ID",Fleet_ID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_ChecklistOnVesselType", sqlprm).Tables[0];


        }

        #region ChecklistRating

        public DataTable Get_CheckListName_DL(int ScheduleID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ScheduleID", ScheduleID),               
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_WL_Get_ChecklistForRating", obj);
            return ds.Tables[0];
        }

        public int INS_LocationRatings_DL(int? Location_ID, int Node_ID, decimal System_Current_Report, string Location_Rating, int? Schedule_ID, int Inspection_ID, string Additional_Remarks, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Node_ID",Node_ID),
                                           new SqlParameter("@Location_ID",Location_ID),
                                            new SqlParameter("@System_Current_Report",System_Current_Report),
                                            new SqlParameter("@Location_Rating",Location_Rating),
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                            
                                            new SqlParameter("@Inspection_ID",Inspection_ID),
                                            new SqlParameter("@Additional_Remarks",Additional_Remarks),
                                            
                                            new SqlParameter("@Created_By",Created_By),    
                                            
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_LocationRating", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        //public int INS_ChecklistRatings_DL(int Node_ID, decimal System_Current_Report,  int Inspection_ID, int? Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                   new SqlParameter("@Node_ID",Node_ID),
        //                                   //new SqlParameter("@Location_ID",Location_ID),
        //                                    new SqlParameter("@System_Current_Report",System_Current_Report),
        //                                    //new SqlParameter("@Location_Rating",Location_Rating),
        //                                    //new SqlParameter("@Schedule_ID",Schedule_ID),

        //                                    new SqlParameter("@Inspection_ID",Inspection_ID),
        //                                    //new SqlParameter("@Additional_Remarks",Additional_Remarks),

        //                                    new SqlParameter("@Created_By",Created_By),    

        //                                    new SqlParameter("return",SqlDbType.Int)
        //    };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Checklist_Rating", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        public DataTable INS_ChecklistRatings_DL(DataTable dt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@tblRatings",dt),    
                                            
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            //SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Checklist_Rating", sqlprm);
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_INS_Checklist_Rating", sqlprm);
            return ds.Tables[0];
            //return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_Finalize_ChecklistRatings_DL(int ChecklistID, int Inspection_ID, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Checklist_ID",ChecklistID),
                                           new SqlParameter("@Inspection_ID",Inspection_ID),
                                           new SqlParameter("@Created_By",Created_By),    
                                           new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Finalize_Checklist ", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_ItemRatings_DL(int Item_ID, decimal System_Current_Report, string Item_Rating, int? Schedule_ID, int Inspection_ID, string Additional_Remarks, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Item_ID",Item_ID),
                                            new SqlParameter("@System_Current_Report",System_Current_Report),
                                            new SqlParameter("@Item_Rating",Item_Rating),
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                            
                                            new SqlParameter("@Inspection_ID",Inspection_ID),
                                            new SqlParameter("@Additional_Remarks",Additional_Remarks),
                                            
                                            new SqlParameter("@Created_By",Created_By),    
                                            
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_ItemRating", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_CurrentCheckListRatings_DL(int CheckList_ID, int InspID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID),    
                   new System.Data.SqlClient.SqlParameter("@Inspection_ID", InspID),    
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ChecklistRatings", obj);
            return ds.Tables[0];
        }

        public DataTable Get_LocationNode_AND_LocationID_DL(int CheckList_ID, int InspID, int CheckListItem_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID),    
                   new System.Data.SqlClient.SqlParameter("@Inspection_ID", InspID),    
                   new System.Data.SqlClient.SqlParameter("@ChecklistItem_ID", CheckListItem_ID),    
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_LocationNodeANDLocationID", obj);
            return ds.Tables[0];
        }

        //public int INS_Finalize_DL(int Item_ID, decimal System_Current_Report, string Item_Rating, int? Schedule_ID, int Inspection_ID, string Additional_Remarks, int? Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Item_ID",Item_ID),
        //                                    new SqlParameter("@System_Current_Report",System_Current_Report),
        //                                    new SqlParameter("@Item_Rating",Item_Rating),
        //                                    new SqlParameter("@Schedule_ID",Schedule_ID),

        //                                    new SqlParameter("@Inspection_ID",Inspection_ID),
        //                                    new SqlParameter("@Additional_Remarks",Additional_Remarks),

        //                                    new SqlParameter("@Created_By",Created_By),    

        //                                    new SqlParameter("return",SqlDbType.Int)
        //    };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_ItemRating", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        #endregion


        #region ChecklistInspectionReport

        public DataTable Get_WorklistReportWithCategoryGrouping_DL(int InspectionID, int ShowImages, string ReportType, string ChecklistIDs)//,string TBODY)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            string @TBODY = "";

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@InspectionID", InspectionID), 
                    new System.Data.SqlClient.SqlParameter("@ShowImages", ShowImages), 
                     new System.Data.SqlClient.SqlParameter("@ReportType", ReportType), 
                      new System.Data.SqlClient.SqlParameter("@ChecklistIDs", ChecklistIDs), 
                       new SqlParameter("@TBODY",@TBODY)
                       
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_WorklistReportWithCategoryGroupingCHK", obj);
            return ds.Tables[0];
        }

        public DataTable Get_WorklistReportWithAllGrouping_DL(int InspectionID)//,string TBODY)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //string @TBODY = "";

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@InspID", InspectionID), 
                    
                       
                    
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_WorklistReportWithAllGroupingCHK", obj);
            return ds.Tables[0];
        }

        #endregion
        #region
        public DataTable Get_CategoryList()
        {
            //System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            //{ 
            //       new System.Data.SqlClient.SqlParameter("@CHKID", CHKID),                  
                    
            //};
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_LIB_GetCategoryList").Tables[0];

        }
        public int INSERT_UPDATE_DELETE_Category(int CategoryID, string Category, int LogUserID, char operationType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CategoryID",CategoryID),
                                            new SqlParameter("@Category",Category),
                                            new SqlParameter("@LogUserID",LogUserID),
                                            new SqlParameter("@OperationType",operationType),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_LIB_Insert_Update_Delete_Category", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_Search_CheckListCategory(string SearchText)
        {
            SqlParameter[] obj = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText),
                                           
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_CheckListCategory", obj);

            return ds.Tables[0];
        }
        #endregion

    }
}
