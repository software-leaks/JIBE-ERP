using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data;
using SMS.Data.Crew;
using SMS.Data.PortageBill;

namespace SMS.Business.PortageBill
{
    public class BLL_PB_VesselAllowance
    {
        public static DataTable Get_NationalityGroupForVesselAllowance(int VesselId)
        {
            try
            {
                return DAL_PB_VesselAllowance.Get_NationalityGroupForVesselAllowance_DL(VesselId);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_NationalityForVesselAllowance(int NationalityGroupId)
        {
            try
            {
                return DAL_PB_VesselAllowance.Get_NationalityForVesselAllowance_DL(NationalityGroupId);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet Get_RankWiseVesselAllowance(int NationalityGroupId)
        {
            try
            {
                return DAL_PB_VesselAllowance.Get_RankWiseVesselAllowance_DL(NationalityGroupId);
            }
            catch
            {
                throw;
            }
        }
        public static int Insert_NationalityGroup(int VesselId,int NationalityGroupId,String NationalityGroupName,DataTable dt, int UserId)
        {
            try
            {
                return DAL_PB_VesselAllowance.Insert_NationalityGroup_DL(VesselId,NationalityGroupId, NationalityGroupName, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_NationalityForNationalityGroup(int NationalityGroupId)
        {
            try
            {
                return DAL_PB_VesselAllowance.Get_NationalityForNationalityGroup_DL(NationalityGroupId);
            }
            catch
            {
                throw;
            }
        }
        public static int DeleteNationalityGroup(int NationalityGroupId, int UserId)
        {
            try
            {
                return DAL_PB_VesselAllowance.DeleteNationalityGroup_DL(NationalityGroupId, UserId);
            }
            catch
            {
                throw;
            }
        }
        public static int Insert_VesselAllowance(int VesselId, DataTable dt, DateTime EffectiveDate,int UserId)
        {
            try
            {
                return DAL_PB_VesselAllowance.Insert_VesselAllowance_DL(VesselId, dt,EffectiveDate, UserId);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Check_NationalityGroup(int VesselId, int NationalityGroupId, String NationalityGroupName, DataTable dt)
        {
            try
            {
                return DAL_PB_VesselAllowance.Check_NationalityGroup_DL(VesselId, NationalityGroupId, NationalityGroupName, dt);
            }
            catch
            {
                throw;
            }
        }
    }
}
