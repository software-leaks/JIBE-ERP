using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SMS.WCFMobileService
{
    public class DAL_JiBE
    {
        private static string connection = "";
        static DAL_JiBE()
        {
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataSet Get_UserDetails(string UserName, string Password, string EncPassword, string DeviceID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@UserName", UserName), new SqlParameter("@Password", Password), new SqlParameter("@DeviceID", DeviceID), new SqlParameter("@EncPassword", EncPassword) };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_UserDetails", prm);
        }

        public static int Upd_APNSDetails(DateTime? Sync_Time, int userID, string Device_ID, string APNS_Token)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@userID", userID),   
                                                new SqlParameter("@Device_ID", Device_ID),   
                                                new SqlParameter("@APNS_Token", APNS_Token),       
                                               
                                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_APNSToken", prm);
        }

        public static DataSet Get_Vessels(DateTime? Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Vessels", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataSet Get_UserList(int userID, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@userID", userID) };

            //prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_InspectorList", prm);
            //OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }


        public static DataSet Get_FollowUPSettings(string Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_FOLLOWUP", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataSet Get_AllFollowUP(int? vesselID, string Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@VesselID", vesselID), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ALL_FollowUp", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataSet UPD_FollowUps(int? PK_ID, int? FOLLOWUP_ID, string ACTIVITYID, int objectId, string FOLLOWUP, string FOLLOWUPType, int createdBY, string Authentication_Token)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
                new SqlParameter("@PK_ID", PK_ID),
                new SqlParameter("@FOLLOWUP_ID", FOLLOWUP_ID),
                new SqlParameter("@ACTIVITYID", ACTIVITYID),
                new SqlParameter("@objectId", objectId),
                new SqlParameter("@FOLLOWUP", FOLLOWUP),
                new SqlParameter("@FOLLOWUPType", FOLLOWUPType),
                new SqlParameter("@createdBY", createdBY),
                
                //new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) 
            };

            //prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_UPD_FollowUP_Beta", prm);
            //OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataTable Upd_FollowUPSettings(int? ID, int UserID, string FollowID, string FollowType, int Created_By, int Action)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@ID", ID), 
                                                new SqlParameter("@UserID",UserID), 
                                                new SqlParameter("@FollowID",FollowID),
                                                new SqlParameter("@FollowType", FollowType),
                                                new SqlParameter("@Created_By", Created_By), 
                                                new SqlParameter("@Action", Action), 
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_UPD_FollowUP_Seetings", prm);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }
            // SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_Activity_beta", prm);
        }


        public static DataSet Get_ALL_Vessels(DateTime? Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ALL_Vessels", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }



        //public static void Upd_Activity(DateTime ActionDate, string ActivityId, string CHILDID, int userId, int objectId, int sdeptId, int odeptId, DateTime DUEDATE, string NAME, string NavigationPath, int ORGANIZATIONID, string REMARK, string STATUS, int TYPE, int ncr, int? Inspection_ID = null, int? Node_ID = null, int? Location_ID = null)
        //{

        //    SqlParameter[] prm = new SqlParameter[] { 
        //                                        new SqlParameter("@ACTIONDATE", ActionDate), 
        //                                        new SqlParameter("@ACTIVITYID",ActivityId ), 
        //                                        new SqlParameter("@CHILDID",CHILDID ),
        //                                        new SqlParameter("@DUEDATE", DUEDATE),
        //                                        new SqlParameter("@userId", userId),
        //                                        new SqlParameter("@NAME",NAME ),
        //                                        new SqlParameter("@NavigationPath",NavigationPath),
        //                                        new SqlParameter("@objectId",objectId),
        //                                        new SqlParameter("@odeptId",odeptId),
        //                                        new SqlParameter("@ORGANIZATIONID",ORGANIZATIONID ),
        //                                        new SqlParameter("@REMARK",REMARK ),
        //                                        new SqlParameter("@sdeptId",sdeptId ),
        //                                        new SqlParameter("@STATUS",STATUS ),
        //                                        new SqlParameter("@TYPE",TYPE ),
        //                                       new SqlParameter("@ncr",ncr ),

        //                                        new SqlParameter("@Inspection_ID",Inspection_ID ),
        //                                        new SqlParameter("@Node_ID",Node_ID ),
        //                                        new SqlParameter("@Location_ID",Location_ID ),

        //                                            };

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_ACTIVITY", prm);
        //}

        public static void Upd_Activity(DateTime ActionDate, string ActivityId, string CHILDID, int userId, int objectId, int sdeptId, int odeptId, DateTime DUEDATE, string NAME, string NavigationPath, int ORGANIZATIONID, string REMARK, string STATUS, int TYPE, int ncr, int? Inspection_ID = null, int? Node_ID = null, int? Location_ID = null, int? Function_ID = null, int? Functional_Location_ID = null, int? SubsysLocation_ID = null)
        {
            if (UDFLib_WCF.ConvertStringToNull(REMARK) == null)
            {
                REMARK = null;
            }
            else if (REMARK == "null" || REMARK == "(null)")
            {
                REMARK = null;
            }
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@ACTIONDATE", ActionDate), 
                                                new SqlParameter("@ACTIVITYID",ActivityId ), 
                                                new SqlParameter("@CHILDID",CHILDID ),
                                                new SqlParameter("@DUEDATE", DUEDATE),
                                                new SqlParameter("@userId", userId),
                                                new SqlParameter("@NAME",NAME ),
                                                new SqlParameter("@NavigationPath",NavigationPath),
                                                new SqlParameter("@objectId",objectId),
                                                new SqlParameter("@odeptId",odeptId),
                                                new SqlParameter("@ORGANIZATIONID",ORGANIZATIONID ),
                                                new SqlParameter("@REMARK",REMARK ),
                                                new SqlParameter("@sdeptId",sdeptId ),
                                                new SqlParameter("@STATUS",STATUS ),
                                                new SqlParameter("@TYPE",TYPE ),
                                               new SqlParameter("@ncr",ncr ),

                                                new SqlParameter("@Inspection_ID",Inspection_ID ),
                                                new SqlParameter("@Node_ID",Node_ID ),
                                                new SqlParameter("@Location_ID",Location_ID ),

                                                 new SqlParameter("@FunctionID",Function_ID),
                                                new SqlParameter("@FunctionLocationID",Functional_Location_ID),
                                                new SqlParameter("@SubsystemID",SubsysLocation_ID),
                                               
                                                    };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_ACTIVITY", prm);
        }


        public static DataTable Upd_Activity_Beta(DateTime ActionDate, string ActivityId, int userId, int objectId, string NAME, string REMARK, string STATUS, int TYPE, int? StarFlag)
        {
            string CHILDID = null;
            int? sdeptId = null;
            int? odeptId = null;
            DateTime? DUEDATE = null;
            string NavigationPath = null;
            int? ORGANIZATIONID = null;
            int? ncr = null;
            int? Inspection_ID = null;
            int? Node_ID = null;
            int? Location_ID = null;

            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@ACTIONDATE", ActionDate), 
                                                new SqlParameter("@ACTIVITYID",ActivityId ), 
                                                new SqlParameter("@CHILDID",CHILDID ),
                                                new SqlParameter("@DUEDATE", DUEDATE),
                                                new SqlParameter("@userId", userId),
                                                new SqlParameter("@NAME",NAME ),
                                                new SqlParameter("@NavigationPath",NavigationPath),
                                                new SqlParameter("@objectId",objectId),
                                                new SqlParameter("@odeptId",odeptId),
                                                new SqlParameter("@ORGANIZATIONID",ORGANIZATIONID ),
                                                new SqlParameter("@REMARK",REMARK ),
                                                new SqlParameter("@sdeptId",sdeptId ),
                                                new SqlParameter("@STATUS",STATUS ),
                                                new SqlParameter("@TYPE",TYPE ),
                                                new SqlParameter("@StarFlag",StarFlag ),
                                                new SqlParameter("@ncr",ncr ),

                                                new SqlParameter("@Inspection_ID",Inspection_ID ),
                                                new SqlParameter("@Node_ID",Node_ID ),
                                                new SqlParameter("@Location_ID",Location_ID ),
                                               
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_UPD_Activity_beta", prm);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }
            // SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_Activity_beta", prm);
        }

        public static void Upd_ActivityObject(string ActivityId, int ActivityOBjId, string ImageaudioPath, int objectId, int userId)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@ActivityId", ActivityId), 
                                                new SqlParameter("@ActivityOBjId",ActivityOBjId ), 
                                                new SqlParameter("@ImageaudioPath", ImageaudioPath),
                                                new SqlParameter("@objectId",objectId),
                                                new SqlParameter("@userId",userId)
                                                    };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_ACTIVITYOBJECT", prm);
        }


        public static DataTable Upd_ActivityObject_Beta(string ActivityId, int ActivityOBjId, string ImageaudioPath, int objectId, int userId, int followUpID)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@ActivityId", ActivityId), 
                                                new SqlParameter("@ActivityOBjId",ActivityOBjId ), 
                                                new SqlParameter("@ImageaudioPath", ImageaudioPath),
                                                new SqlParameter("@objectId",objectId),
                                                new SqlParameter("@userId",userId),
                                                 new SqlParameter("@FollowUp_ID",followUpID)
                                                    };

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_UPD_ActivityObject_Beta", prm);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        public static DataSet Get_Activity_Details(int UERID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@USERID", UERID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_Activity_Details", prm);
        }


        public static DataSet Get_Activity(int? ObjectID, DateTime? Sync_Time, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Vessel_ID", ObjectID), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Activity", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataSet Get_ALL_Activity(int UserID, DateTime? Sync_Time, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@userID", UserID), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ALL_Activity", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataTable Get_Job_Status(int? ObjectID, string ActivityID, DateTime? Sync_Time)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Vessel_ID", ObjectID), new SqlParameter("@ActivityID", ActivityID), new SqlParameter("@Sync_Time", Sync_Time) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Job_Status", prm).Tables[0];
        }

        public static DataTable Get_Authentication_Details(string Authentication_Token)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_Authentication_Token", new SqlParameter("@Authentication_Token", Authentication_Token)).Tables[0];
        }


        public static int Upd_Create_User(string User_Name, string Password, string Email_Address, string Mobile_Number, string Company_Name)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@User_Name", User_Name), 
                                                new SqlParameter("@Password",Password ), 
                                                new SqlParameter("@Email_Address", Email_Address),
                                                new SqlParameter("@Mobile_Number",Mobile_Number),
                                                new SqlParameter("@Company_Name",Company_Name),
                                                new SqlParameter("@return",SqlDbType.Int)
                                                    };

            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Create_User", prm);

            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }


        public static int UPD_Verify_User(string Code)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "INSP_UPD_Verify_User", new SqlParameter("@Code", Code)));
        }

        public static int Upd_Contact_Us(int UserID, string Message)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@UserID", UserID), 
                                                new SqlParameter("@Message",Message ),                                                 
                                                new SqlParameter("@return",SqlDbType.Int)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_Upd_Contact_Us", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }

        public static DataSet Get_CurrentCheckList_DL(int CheckList_ID)
        {
            //System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CheckList_ID", CheckList_ID),               
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_Checklist", obj);
            return ds;
        }

        public static DataSet Get_AllCheckList_DL(int Vessel_ID, int? User_ID,DateTime ? Synctime)
        {
            //System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", Vessel_ID),   
                   new System.Data.SqlClient.SqlParameter("@User_ID", User_ID),   
                   new System.Data.SqlClient.SqlParameter("@SYNC_TIME", Synctime),  
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ChecklistRatingsOnVesselID", obj);
            return ds;
        }
        //-----------    This GET_ALLCHECKLIST_BY_USERID_DL function is used to get all checklist by USERID
        //------------      PARAMETRS
        //--------------       User_ID is used for passing user ID from device.    
        public static DataSet Get_AllCheckList_BY_UserID_DL(int User_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                    
                   new System.Data.SqlClient.SqlParameter("@User_ID", User_ID),   
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ChecklistRatingsOnUerID", obj);
            return ds;
        }

        public static DataSet Get_LocGradeType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_Grades");

        }

        public static int Upd_Activity_Status(int UserID, DateTime Date, string Status, string ActivityID)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@UserID", UserID), 
                                                new SqlParameter("@Date",Date ),   
                                                new SqlParameter("@Status",Status ),  
                                                new SqlParameter("@ActivityID",ActivityID )
                                               
                                                    };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_Activity_Status", prm);

        }

        public static int Upd_ActivityObject_ImageName(int UserID, string ActivityID, int ActivityObjectID, int ObjectID, string Activity_ImageName, string Activity_ImagePath, int? attachSize)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@userId", UserID), 
                                                new SqlParameter("@ACTIVITYID",ActivityID ),
                                                new SqlParameter("@ACTIVITYOBJID",ActivityObjectID ),
                                                new SqlParameter("@objectId",ObjectID ),
                                                new SqlParameter("@IMAGEAUDIONAME",Activity_ImageName ),
                                                new SqlParameter("@IMAGEAUDIOPATH",Activity_ImagePath ),
                                                 new SqlParameter("@AttachSize",attachSize )
                                               
                                                    };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_ActivityObject_imageName", prm);

        }

        public static int Upd_Checklist_Status(int Checklist_ID, int Inspection_ID, int Mobile_Status)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@Checklist_ID", Checklist_ID), 
                                                new SqlParameter("@Inspection_ID",Inspection_ID ),   
                                                new SqlParameter("@Mobile_Status",Mobile_Status )
                                                
                                               
                                                    };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_Update_MobileStatus", prm);

        }

        /// <summary>
        /// This function will update checklist items rating values.
        /// </summary>
        /// <param name="Inspection_ID">Valid inspection ID</param>
        /// <param name="Node_ID">Valid nodeid of checklist</param>
        /// <param name="Value">Valid ratings values NULL/Decimals</param>
        /// <returns></returns>
        public static int Upd_NodeValue(int Inspection_ID, int Node_ID, decimal? Value)
        {
            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@Inspection_ID", Inspection_ID), 
                                                new SqlParameter("@Node_ID",Node_ID ),   
                                                new SqlParameter("@Value",Value ),  
                                                                                              
                                                   };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_CheckList_Node_Value", prm);

        }


        public static DataSet Get_Vessels_By_Type(DateTime? Sync_Time, string Authentication_Token, ref string OutSyncTime) //DateTime? FN_SYNC_TIME, DateTime? CRW_SYN_TIME, DateTime? CHKLIST_SYN_TIME, 
        {
            SqlParameter[] prm = new SqlParameter[] { 
                new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time),
                //new SqlParameter("@VesselID",null) , new SqlParameter("@FN_SYNC_TIME", FN_SYNC_TIME) ,new SqlParameter("@CRW_SYN_TIME", CRW_SYN_TIME),new SqlParameter("@CHKLIST_SYN_TIME", CHKLIST_SYN_TIME),
                new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) 
                                                    };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Vessels_By_Type", prm); // later changed by hadish ""INSP_Get_Vessels_By_SynTme"" procedure was called ,Later changed by Hadish on 05-10-2016
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataSet Get_Vessels_By_Type_SyncTime(DateTime? Sync_Time, string Authentication_Token, DateTime? AC_SYNC_TIME, DateTime? FN_SYNC_TIME, DateTime? CRW_SYN_TIME, DateTime? CHKLIST_SYN_TIME, ref string OutSyncTime) // 
        {
            SqlParameter[] prm = new SqlParameter[] { 
                new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time),new SqlParameter("@VesselID",null) , 
                new SqlParameter("@AC_SYNC_TIME", AC_SYNC_TIME),
                new SqlParameter("@FN_SYNC_TIME", FN_SYNC_TIME) ,new SqlParameter("@CRW_SYN_TIME", CRW_SYN_TIME),new SqlParameter("@CHKLIST_SYN_TIME", CHKLIST_SYN_TIME),
                new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) 
                                                    };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Vessels_By_SynTme", prm); // later changed by hadish ""INSP_Get_Vessels_By_SynTme"" procedure was called ,Later changed by Hadish on 05-10-2016
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataSet Get_Vessels_By_Type_SyncTime(DateTime? Sync_Time, string Authentication_Token, DateTime? AC_SYNC_TIME, DateTime? FN_SYNC_TIME, DateTime? CRW_SYN_TIME, DateTime? CHKLIST_SYN_TIME, ref string OutSyncTime,DataTable dtInput) // 
        {
            SqlParameter[] prm = new SqlParameter[] { 
                new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time),new SqlParameter("@VesselID",null) , 
                new SqlParameter("@AC_SYNC_TIME", AC_SYNC_TIME),
                new SqlParameter("@FN_SYNC_TIME", FN_SYNC_TIME) ,new SqlParameter("@CRW_SYN_TIME", CRW_SYN_TIME),new SqlParameter("@CHKLIST_SYN_TIME", CHKLIST_SYN_TIME),
                new SqlParameter("@InputVesselANDSynctime", dtInput),
                new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) 
                                                    };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Vessels_By_SynTme", prm); 
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        public static DataTable Get_Company(DateTime? Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Authentication_Token", Authentication_Token), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Company", prm).Tables[0];
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return dt;
        }


        public static DataSet Get_FunctionalTree_DL(int? function, int? vesselID, int? EquipmentType, DateTime? Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Function", function), new SqlParameter("@Vessel_ID", vesselID), new SqlParameter("@Equipment_Type", EquipmentType), new SqlParameter("@SYNC_TIME", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data_Service", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }


        public static void Service_LOG(string Query, int? remarkType, int? createdBy)
        {

            SqlParameter[] prm = new SqlParameter[] { 
                                                new SqlParameter("@Query", Query),  
                                                new SqlParameter("@remarkType", remarkType),  
                                                new SqlParameter("@createdBy", createdBy),  
                                               
                                                    };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_INS_ServiceLogs", prm);
        }

        #region CrewUserList
        /// <summary>
        /// To get onboard crew details for selected vessel.
        /// </summary>
        /// <param name="VesselID">Id of vessel</param>
        /// <param name="Sync_Time">requested date and time</param>
        /// <param name="Authentication_Token">to authenticate user.</param>
        /// <returns>List of onboard crew details</returns>
        public static DataSet Get_CrewUser_List(int? vesselID, DateTime? Sync_Time, string Authentication_Token, ref string OutSyncTime)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@VesselID", vesselID), new SqlParameter("@Sync_Time", Sync_Time), new SqlParameter("@OutSyncTime", SqlDbType.VarChar, 1024) };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            // DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_CrewUser_List", prm);
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_CrewUser_List", prm);
            OutSyncTime = Convert.ToString(prm[prm.Length - 1].Value);
            return ds;
        }

        #endregion

        #region Upload Feedback Text
        public static DataSet UPD_Feedback_Text(int? UserID, int? Crew_ID, string Feedback_Desc, string AttachmentPath)
        {
            SqlParameter[] prm = new SqlParameter[] 
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@Crew_ID", Crew_ID),
                new SqlParameter("@Feedback_Desc", Feedback_Desc),
                new SqlParameter("@AttachmentPath", AttachmentPath)
            };


            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_INSERT_CREW_FEEDBACK", prm);

            return ds;
        }
        #endregion

        public static DataTable Get_Authentication_UserID(string Authentication_Token)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_Authentication_Token", new SqlParameter("@Authentication_Token", Authentication_Token)).Tables[0];
        }


        public static DataSet Get_Config_ALerts( string Authentication_Token)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Authentication_Token", Authentication_Token) };


            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_Alerts_details", prm);
            
            return ds;
        }

        public static int Upd_Alerts_Received(string AlertIDs, string Authentication_Token)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@ReceviedAlerts", AlertIDs),new SqlParameter("@Authentication_Token", Authentication_Token) };


            int op=SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_Alerts_Received", prm);
            return op;
            
        }

        public static int Upd_Alerts_Action(int AlertIDs, string Authentication_Token)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@ActionAlertsID", AlertIDs), new SqlParameter("@Authentication_Token", Authentication_Token) };


            int op = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INSP_UPD_Alerts_Action", prm);
            return op;

        }
    }
}
