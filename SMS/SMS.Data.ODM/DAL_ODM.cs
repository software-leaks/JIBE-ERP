using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;



namespace SMS.Data.ODM
{
    public class DAL_ODM
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        
        private string connection = "";
        public DAL_ODM(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_ODM()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }



        public int Insert_ODM_Vessel(string ODM_SUBJECT, string MSG_TEXT, int VesselId , int ODM_Department_Id,bool IsallVessels, int Created_By, ref int OutGroupId)
        {
            int result = 0;
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@VesselId", VesselId),
                                        new SqlParameter("@ODM_SUBJECT", ODM_SUBJECT),
                                        new SqlParameter("@MSG_TEXT", MSG_TEXT),
                                        new SqlParameter("@ODM_Department_Id", ODM_Department_Id),
                                         new SqlParameter("@SendToAll", IsallVessels),
                                        new SqlParameter("@Created_By", Created_By),
                                        new SqlParameter("@OutGroupId",OutGroupId)
                                    };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            result = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ODM_INS_Vessel_ODM", sqlprm);
            OutGroupId = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return result;
        }


        public int UPD_Vessel_ODM(int Group_Id, DataTable VesselIds, bool IsallVessels, string ODM_SUBJECT, string MSG_TEXT, int ODM_Department_Id, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Group_Id", Group_Id),
                                        new SqlParameter("@VesselIds", VesselIds),
                                        new SqlParameter("@ODM_SUBJECT", ODM_SUBJECT),
                                        new SqlParameter("@MSG_TEXT", MSG_TEXT),
                                        new SqlParameter("@SendToAll", IsallVessels),
                                        new SqlParameter("@ODM_Department_Id", ODM_Department_Id),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ODM_UPD_Vessel_ODM", sqlprm);
        }

        public int Ins_ODM_Attachments(int Group_Id, string ATTACHMENT_NAME, string ATTACHMENT_PATH, string Attachment_ID, int Sent_Size, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@GroupId", Group_Id),
                                        new SqlParameter("@ATTACHMENT_NAME", ATTACHMENT_NAME),
                                        new SqlParameter("@ATTACHMENT_PATH", ATTACHMENT_PATH),
                                        new SqlParameter("@Attachment_ID", Attachment_ID),
                                        new SqlParameter("@Sent_Size", Sent_Size),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Ins_ODM_Attachments", sqlprm);
        }

        public DataTable Get_ODM_Vessels(int? GroupId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GroupId",GroupId),
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_ODM_Vessels", sqlprm).Tables[0];
        }

        public DataTable GET_ODM_Attachments(int? GroupId)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GroupId",GroupId),
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_ODM_Attachments", sqlprm).Tables[0];
        }


        public DataTable GET_ODM_Attachments_PendingList()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_ODM_Attachments_PendingList").Tables[0];
        }


        public int ODM_Update_SentStatus(int GroupId, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@GroupId", GroupId),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ODM_Update_SentStatus", sqlprm);
        }

        public int Del_Vessel_ODM(int? GroupId, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@GroupId", GroupId),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ODM_Del_Vessel_ODM", sqlprm);
        }



         public DataTable Get_ODM_Departments()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_ODM_Departments").Tables[0];
        }


        public DataTable GET_ODM_VesselsAll()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_ODM_VesselsAll").Tables[0];
        }

        public DataTable GET_GroupVessels(int GroupId)
        {
            SqlParameter prm = new SqlParameter("@GroupId", GroupId);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ODM_GET_GroupVessels", prm).Tables[0];
        }
        
        
         public DataTable Get_ODM_QueueList(string searchtext  , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
         {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
             obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
             DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ODM_Get_MessageQueueListByGroup", obj);
             isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
             return ds.Tables[0];
         }


         public DataTable Search_ODM_History(int? Vessel_Id, string searchtext,  DateTime? StartDate, DateTime? EndDate, DataTable dtDepartmentIds, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
         {
             System.Data.DataTable dt = new System.Data.DataTable();
             string sEndDate = DBNull.Value.ToString(), sStartDate=DBNull.Value.ToString();

             if (EndDate != null && StartDate != null)
             {
                 sStartDate = Convert.ToDateTime(StartDate).ToString("yyyy-MM-dd HH:mm");
                 sEndDate = Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd HH:mm");

             }

             System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    
                   new System.Data.SqlClient.SqlParameter("@Vessel_Id",Vessel_Id), 
                   new System.Data.SqlClient.SqlParameter("@StartDate", sStartDate), 
                   new System.Data.SqlClient.SqlParameter("@EndDate", sEndDate),
                   new System.Data.SqlClient.SqlParameter("@dtDepartment",dtDepartmentIds),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
             obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
             dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Search_ODM_History", obj).Tables[0];
             isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
             return dt;
         }


         public int Attachment_Delete(int? File_ID, int? Created_By)
         {

             System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Attachment_ID", File_ID), 
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
            };

             return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ODM_Attachment_Delete", obj);

         }
        
    }
}
