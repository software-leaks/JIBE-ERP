using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;
namespace SMS.Data.PMS
{
   public class DAL_PMS_Eqp
    {

       private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

       public DataTable TEC_Get_FunctionalTreeDataEqpStruct(int Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
          
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                         
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_FunctionalTreeDataEqpStruct", obj).Tables[0];

        }

       public int TEC_Insert_PMSGroup(int? ParentEqpID,string EqpName,string EqpDescription,string Maker,string Model,string NodeType,int Function,int Vessel_ID,int CreatedBy,int ActiveStatus)
       {
           System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
          
                new System.Data.SqlClient.SqlParameter("@Parent_EQP_ID", ParentEqpID),
                new System.Data.SqlClient.SqlParameter("@EQP_Name", EqpName),
                new System.Data.SqlClient.SqlParameter("@EQP_Description", EqpDescription),
                new System.Data.SqlClient.SqlParameter("@maker", Maker),
                new System.Data.SqlClient.SqlParameter("@model", Model),
                new System.Data.SqlClient.SqlParameter("@Node_Type", NodeType),
                new System.Data.SqlClient.SqlParameter("@Functions", Function),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                new System.Data.SqlClient.SqlParameter("@Active_Status", ActiveStatus),
                         
            };

           return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_Insert_PMSGroup", obj);

       }
       public int TEC_Update_PMSGroup(int EqpID, string EqpName, string EqpDescription, string Maker, string Model, int ModifiedBy)
       {
           System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
          
                new System.Data.SqlClient.SqlParameter("@EQP_ID", EqpID),
                new System.Data.SqlClient.SqlParameter("@EQP_Name", EqpName),
                new System.Data.SqlClient.SqlParameter("@EQP_Description", EqpDescription),
                new System.Data.SqlClient.SqlParameter("@maker", Maker),
                new System.Data.SqlClient.SqlParameter("@model", Model),
                new System.Data.SqlClient.SqlParameter("@ModifiedBy", ModifiedBy)
               
                         
            };

           return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_Update_PMSGroup", obj);

       }
       public DataSet TEC_Get_PMSGroupInfo(int EQPID)
       {
           System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
          
                 new System.Data.SqlClient.SqlParameter("@EQPID", EQPID),
                         
            };

           return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_PMSGroupInfo", obj);
       }

       public DataSet TEC_Get_PMSJobs(int? EqpID, int? vesselid, int? deptid, int? rankid, string jobtitle
          , int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
       {
           System.Data.DataTable dt = new System.Data.DataTable();

           System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@EQPID", EqpID),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANKID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE", jobtitle),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
           obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
           System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_PMSJobs", obj);
           isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
           return ds;
       }

       public DataSet TEC_Get_PMSJobListByJobID(int jobid)
       {

           System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),
           };

           return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_Get_PMSJobListByJobID", obj);
       }

       public int TEC_Insert_PMSJobs(int VesselID, int EqpID, string JobCode, string JobTitle, string JobDesc, int Frequency, int FrequencyType, int DeptID, int RankID, int CMS,int Critical,int IsTechReq,int CreatedBy)
       {
           System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
          
                new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                new System.Data.SqlClient.SqlParameter("@EQPID", EqpID),
                new System.Data.SqlClient.SqlParameter("@JobCode", JobCode),
                new System.Data.SqlClient.SqlParameter("@JobTitle", JobTitle),
                new System.Data.SqlClient.SqlParameter("@JobDescription", JobDesc),
                new System.Data.SqlClient.SqlParameter("@Frequency", Frequency),
                new System.Data.SqlClient.SqlParameter("@FrequencyType", FrequencyType),
                new System.Data.SqlClient.SqlParameter("@DepartmentID", DeptID),
                new System.Data.SqlClient.SqlParameter("@RankID", RankID),
                new System.Data.SqlClient.SqlParameter("@CMS", CMS),
                new System.Data.SqlClient.SqlParameter("@Critical", Critical),
                new System.Data.SqlClient.SqlParameter("@IsTechRequired", IsTechReq),
                new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
              
            };

           return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_Insert_PMSJobs", obj);

       }

    }
}
