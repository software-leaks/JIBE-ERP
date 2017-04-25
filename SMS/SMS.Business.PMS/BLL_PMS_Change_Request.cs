using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PMS;
using System.Data;

namespace SMS.Business.PMS
{
    public class BLL_PMS_Change_Request
    {

        SMS.Data.PMS.DAL_PMS_Change_Request objchangRqst = new DAL_PMS_Change_Request();

        public DataSet TecMachineryChangeRequestSearch(int? Fleet_ID, int? Vessel_ID, string SearchText, int? Status, int? System_ID, string Department_ID
          , DateTime? fromdate, DateTime? todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objchangRqst.TecMachineryChangeRequestSearch(Fleet_ID, Vessel_ID, SearchText, Status, System_ID, Department_ID, fromdate
                , todate, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet TecMachineryChangeRequestList(int? ID , int? Vessel_ID)
        {
            return objchangRqst.TecMachineryChangeRequestList(ID, Vessel_ID);
        }


        public int TecMachineryChangeRequestUpdate(int userid, int changereqstid, string actionedremark, int systemid, string systemdesc, string stystemparticular, string setinstalled, string model
          , int? vesselid,string Cr_Actual_values)
        {

            return objchangRqst.TecMachineryChangeRequestUpdate(userid, changereqstid, actionedremark, systemid, systemdesc, stystemparticular, setinstalled, model, vesselid,Cr_Actual_values);
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
         , int? vesselid, string dept, string Cr_Actual_values)
        {

            return objchangRqst.TecMachineryChangeRequestUpdate(userid, changereqstid, actionedremark, systemid, systemdesc, stystemparticular, setinstalled, model, vesselid, dept, Cr_Actual_values);
        }

        public int TecMachineryChangeRequestDelete(int userid, int changereqstid, string actionedremark, int vesselid,int systemid)
        {
            return objchangRqst.TecMachineryChangeRequestDelete(userid, changereqstid, actionedremark, vesselid,systemid);
        }


        public int TecMachineryChangeRequestSave(int userid, int changereqstid, string actionedremark, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
          , string dept, int? functionid, int? accountcode, int? vesselid,string SerialNo, ref string systemcode)
        {
            return objchangRqst.TecMachineryChangeRequestSave(userid, changereqstid, actionedremark, systemdesc, stystemparticular, maker, setinstalled, model, dept, functionid, accountcode, vesselid,SerialNo, ref systemcode);

        }


        public int TecMachineryChangeRequestReject(int userid, int changereqstid, string actionedremark, int vesselid)
        {
            return objchangRqst.TecMachineryChangeRequestReject(userid, changereqstid, actionedremark, vesselid);

        }

        public int TecMachineryChangeRequestActualFieldsUpdate(int userid, int changereqsid, string cr_actual_values,int Vessel_ID)
        {

            return objchangRqst.TecMachineryChangeRequestActualFieldsUpdate(userid, changereqsid, cr_actual_values, Vessel_ID);
        }


        public DataSet TecJobChangeRequestSearch(int? Fleet_ID, int? Vessel_ID, string SearchText, int? Status
            , int? SYSTEMID, int? SUBSYSTEMID, int? DEPARTMENTID, int? RANKID, int? CRITICAL, int? CMS
            , DateTime? fromdate, DateTime? todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objchangRqst.TecJobChangeRequestSearch(Fleet_ID, Vessel_ID, SearchText, Status, SYSTEMID, SUBSYSTEMID, DEPARTMENTID, RANKID, CRITICAL, CMS
                , fromdate, todate, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }


        public DataSet TecJobChangeRequestList(int? ID, int? Vessel_ID)
        {
            return objchangRqst.TecJobChangeRequestList(ID, Vessel_ID);
        }


        public int TecJobChangeRequestSave(int userid, int changereqstid, string actionedremark, int systemid, int subsystemid, int vesselid, int? deptid, int? rankid, string jobtitle
          , string jobdesc, int? frequency, int? frequencytype, int? cms, int? critical, ref string jobid)
        {
            return objchangRqst.TecJobChangeRequestSave(userid, changereqstid, actionedremark, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, ref jobid);
        }


        public int TecJobChangeRequestUpdate(int userid, int changereqstid, string actionedremark, int jobsid , int vesselid, int? deptid, int? rankid, string jobtitle
      , string jobdesc, int frequency, int frequencytype, int? cms, int? critical, string cr_actual_values)
        {
            return objchangRqst.TecJobChangeRequestUpdate(userid, changereqstid, actionedremark, jobsid, vesselid, deptid, rankid, jobtitle,
                jobdesc, frequency, frequencytype, cms, critical,cr_actual_values);
        }


        public int TecJobChangeRequestDelete(int userid, int changereqstid, string actionedremark, int jobsid, int vesselid)
        {
            return objchangRqst.TecJobChangeRequestDelete(userid, changereqstid, actionedremark, jobsid, vesselid);
        }


        public int TecJobChangeRequestActualFieldsUpdate(int userid, int changereqsid, string cr_actual_values, int Vessel_ID)
        {
            return objchangRqst.TecJobChangeRequestActualFieldsUpdate(userid, changereqsid, cr_actual_values, Vessel_ID);
        }
        
        public int TecJobChangeRequestReject(int userid, int changereqstid, string actionedremark, int vesselid)
        {
            return objchangRqst.TecJobChangeRequestReject(userid, changereqstid, actionedremark, vesselid);   
        }

    }
}
