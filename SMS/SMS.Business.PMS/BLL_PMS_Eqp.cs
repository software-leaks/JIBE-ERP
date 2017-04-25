using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.PMS;
using SMS.Business;
namespace SMS.Business.PMS
{
    public class BLL_PMS_Eqp
    {
        DAL_PMS_Eqp objEqp = new DAL_PMS_Eqp ();
        public DataTable TEC_Get_FunctionalTreeDataEqpStruct(int Vessel_ID)
        {
            return objEqp.TEC_Get_FunctionalTreeDataEqpStruct(Vessel_ID);
        }
        public int TEC_Insert_PMSGroup(int? ParentEqpID, string EqpName, string EqpDescription, string Maker, string Model, string NodeType, int Function,int Vessel_ID, int CreatedBy,  int ActiveStatus)
        {
            
            return objEqp.TEC_Insert_PMSGroup(ParentEqpID,EqpName,EqpDescription,Maker,Model,NodeType,Function,Vessel_ID,CreatedBy,ActiveStatus);
        }
        public int TEC_Update_PMSGroup(int EqpID, string EqpName, string EqpDescription, string Maker, string Model, int ModifiedBy)
        {

            return objEqp.TEC_Update_PMSGroup(EqpID, EqpName, EqpDescription, Maker, Model, ModifiedBy);

        }
        public DataSet TEC_Get_PMSGroupInfo(int EQPID)
        {
            return objEqp.TEC_Get_PMSGroupInfo(EQPID);
        }

        public DataSet TEC_Get_PMSJobs(int? EqpID, int? vesselid, int? deptid, int? rankid, string jobtitle
         , int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objEqp.TEC_Get_PMSJobs(EqpID, vesselid, deptid, rankid, jobtitle, IsActive, sortby,  sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int TEC_Insert_PMSJobs(int VesselID, int EqpID, string JobCode, string JobTitle, string JobDesc, int Frequency, int FrequencyType, int DeptID, int RankID, int CMS, int Critical, int IsTechReq, int CreatedBy)
        {

            return objEqp.TEC_Insert_PMSJobs(VesselID, EqpID, JobCode, JobTitle, JobDesc, Frequency, FrequencyType, DeptID, RankID, CMS,Critical,IsTechReq,CreatedBy);
        }
        public DataSet TEC_Get_PMSJobListByJobID(int jobid)
        {
            return objEqp.TEC_Get_PMSJobListByJobID(jobid);
        }
    }
}
