using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.PMS
{
    public class DAL_PMS_Change_Request
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PMS_Change_Request()
        {

        }


        public DataSet TecMachineryChangeRequestSearch(int? Fleet_ID, int? Vessel_ID, string SearchText, int? Status, int? System_ID, string Department_ID
          , DateTime? fromdate, DateTime? todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Fleet_ID", Fleet_ID),  
                new System.Data.SqlClient.SqlParameter("@VESSELID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT",SearchText),
                   new System.Data.SqlClient.SqlParameter("@STATUS",Status),
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@Department_ID",Department_ID),


                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_MACHINERY_CHANGE_REQUEST_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }




        public DataSet TecMachineryChangeRequestList(int? ID, int? Vessel_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSCHAGERQSTID", ID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_MACHINERY_CHANGE_REQUEST_LIST", obj);
            return ds;
        }



        public int TecMachineryChangeRequestSave(int userid, int changereqstid, string actionedremark, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
            , string dept, int? functionid, int? accountcode, int? vesselid, string SerialNo, ref string systemcode)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark), 
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1",dept),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                     new System.Data.SqlClient.SqlParameter("@SerialNO",SerialNo),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE",SqlDbType.VarChar,20),


            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEM_INSERT_APPROVED_CR", obj);
            systemcode = Convert.ToString(obj[obj.Length - 1].Value);
            return retval;


        }


        public int TecMachineryChangeRequestUpdate(int userid, int changereqstid, string actionedremark, int systemid, string systemdesc, string stystemparticular, string setinstalled, string model
            , int? vesselid, string Cr_Actual_values)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark),   
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@Cr_Actual_values",Cr_Actual_values),
                
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMS_UPDATE_APPROVED_CR", obj);
        }

        /// <summary>
        /// Overloaded function created with "dept" as addition parameter  By Someshwar Mandre On 09-06-2016
        /// </summary>
        /// <param name="userid"> User ID of Person who approve the machinary change request.</param>
        /// <param name="changereqstid">ID of chnage request generated.</param>
        /// <param name="actionedremark">remark added while approving change request.</param>
        /// <param name="systemid">ID of system for whom change request is generated.</param>
        /// <param name="systemdesc">Description of System for whom change request has been raised , which going to be change.</param>
        /// <param name="stystemparticular">particulars of System for whom change request has been raised , which going to be change</param>
        /// <param name="setinstalled">Number of sets installed of system.</param>
        /// <param name="model">Model number of system which is going to be change.</param>
        /// <param name="vesselid">ID of vessel from where change request has been raised.</param>
        /// <param name="dept">Department of system for whom change request has been raised, which going to be changed.</param>
        /// <param name="Cr_Actual_values">contain old details of system before change request details is being updated.</param>
        /// <returns>integer number indicates whether query is sucessfully executed or not.</returns>
        public int TecMachineryChangeRequestUpdate(int userid, int changereqstid, string actionedremark, int systemid, string systemdesc, string stystemparticular, string setinstalled, string model
         , int? vesselid, string dept, string Cr_Actual_values) /* Overloaded function created with "dept" as addition parameter  By Someshwar Mandre On 09-06-2016  */
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark),   
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@Dept1",dept),
                   new System.Data.SqlClient.SqlParameter("@Cr_Actual_values",Cr_Actual_values),
                
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMS_UPDATE_APPROVED_CR", obj);
        }

        public int TecMachineryChangeRequestDelete(int userid, int changereqstid, string actionedremark, int vesselid, int systemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark),   
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                    new System.Data.SqlClient.SqlParameter("@SYSTEMID",systemid)
                
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMS_DELETE_APPROVED_CR", obj);
        }




        public int TecMachineryChangeRequestReject(int userid, int changereqstid, string actionedremark, int vesselid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark),   
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMS_REJECT_CR", obj);
        }



        public int TecMachineryChangeRequestActualFieldsUpdate(int userid, int changereqsid, string cr_actual_values, int Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqsid),   
                   new System.Data.SqlClient.SqlParameter("@Cr_Actual_values" ,cr_actual_values),  
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID" ,Vessel_ID), 
                  
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMS_CR_Actual_Fields_Updated", obj);

        }

        public DataSet TecJobChangeRequestSearch(int? Fleet_ID, int? Vessel_ID, string SearchText, int? Status
            , int? SYSTEMID, int? SUBSYSTEMID, int? DEPARTMENTID, int? RANKID, int? CRITICAL, int? CMS
            , DateTime? fromdate, DateTime? todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@VESSELID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@SEARCHTEXT",SearchText),
                new System.Data.SqlClient.SqlParameter("@STATUS",Status),

                new System.Data.SqlClient.SqlParameter("@SYSTEMID",SYSTEMID),	 
                new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",SUBSYSTEMID),
                new System.Data.SqlClient.SqlParameter("@DEPARTMENTID",DEPARTMENTID),		 
                new System.Data.SqlClient.SqlParameter("@RANKID",RANKID),			     
                new System.Data.SqlClient.SqlParameter("@CRITICAL",CRITICAL),			 
                new System.Data.SqlClient.SqlParameter("@CMS",CMS),					 

                new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_GET_JOB_CHANGE_REQUEST_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecJobChangeRequestList(int? ID, int? Vessel_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@JOBCHANGERQSTID", ID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_CHANGE_REQUEST_LIST", obj);
            return ds;
        }


        public int TecJobChangeRequestSave(int userid, int changereqstid, string actionedremark, int systemid, int subsystemid, int vesselid, int? deptid, int? rankid, string jobtitle
          , string jobdesc, int? frequency, int? frequencytype, int? cms, int? critical, ref string jobid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark), 
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@JOBID",SqlDbType.VarChar,20),
            };

            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INSERT_APPROVED_CR", obj);
            jobid = Convert.ToString(obj[obj.Length - 1].Value);
            return retval;

        }


        public int TecJobChangeRequestUpdate(int userid, int changereqstid, string actionedremark, int jobsid, int vesselid, int? deptid, int? rankid, string jobtitle
          , string jobdesc, int frequency, int frequencytype, int? cms, int? critical, string cr_actual_values)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark), 
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobsid ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Cr_Actual_values" ,cr_actual_values), 
                   
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_UPDATE_APPROVED_CR", obj);

        }

        public int TecJobChangeRequestDelete(int userid, int changereqstid, string actionedremark, int jobsid, int vesselid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark), 
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobsid ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_Delete_APPROVED_CR", obj);

        }




        public int TecJobChangeRequestReject(int userid, int changereqstid, string actionedremark, int vesselid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqstid),   
                   new System.Data.SqlClient.SqlParameter("@Actioned_Remark" ,actionedremark),   
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_REJECT_CR", obj);
        }

        public int TecJobChangeRequestActualFieldsUpdate(int userid, int changereqsid, string cr_actual_values, int? Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@Chang_Reqst_ID" ,changereqsid),   
                   new System.Data.SqlClient.SqlParameter("@Cr_Actual_values" ,cr_actual_values), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_CR_Actual_Fields_Updated", obj);

        }





    }
}
