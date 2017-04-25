using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.OPS.Operations;
using System.Data;

namespace SMS.Business.OPS.Operations
{
    public class BLL_OPS_VesselParameters
    {
        DAL_OPS_VesselParameters objVesselParameter = new DAL_OPS_VesselParameters();
        //New Parameters added for Chemical Tanker
        //Abs_eng_rat_power
        //Eng_rat_power
        //SFOC
        //Cyl_oil_calc_mothod

        public int INS_VesselParameter_DL(int VesselID, decimal PropellorPitch, decimal QMCR, decimal RPM_Max, decimal Abs_eng_rat_power, decimal Eng_rat_power, decimal SFOC, decimal Cyl_oil_calc_mothod, int Created_By)
        {
            return objVesselParameter.INS_VesselParameter_DL(VesselID, PropellorPitch, QMCR,RPM_Max,Abs_eng_rat_power,Eng_rat_power,SFOC,Cyl_oil_calc_mothod,Created_By);
        }
        public int UPD_VesselParameter_DL(int GeneralParameterID, int VesselID, decimal PropellorPitch, decimal QMCR, decimal RPM_Max, decimal Abs_eng_rat_power, decimal Eng_rat_power, decimal SFOC, decimal Cyl_oil_calc_mothod, int Modified_By)
        {
            return objVesselParameter.UPD_VesselParameter_DL(GeneralParameterID, VesselID, PropellorPitch, QMCR, RPM_Max, Abs_eng_rat_power, Eng_rat_power, SFOC, Cyl_oil_calc_mothod, Modified_By);
        }
        //public DataTable Get_VesselParameter_DL(int VesselID, int StartIndex, int PageSize, ref int TotalCount)
        //{
        //    return objVesselParameter.Get_VesselParameter_DL(VesselID,StartIndex,PageSize,TotalCount);
        //}
        public int DEL_VesselParameter_DL(int ID, int Deleted_By)
        {
            return objVesselParameter.DEL_VesselParameter_DL(ID, Deleted_By);
        }
        public DataTable Get_VesselParameter_By_ID_DL(int ID)
        {
            return objVesselParameter.Get_VesselParameterBy_ID_DL(ID);
        }
        public DataTable Get_VesselParameter_DL(int VesselID)
        {
            return objVesselParameter.Get_VesselParameter_DL(VesselID);
        }
    }
}
