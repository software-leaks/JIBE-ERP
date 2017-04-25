using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.TMSA;
using System.Data;

namespace SMS.Business.TMSA
{
  public  class BLL_TMSA_EEDI
    {
      DAL_TMSA_EEDI objDAL = new DAL_TMSA_EEDI();
      public int INSERT_EEDI_BL(int VesselID, double EEDI_Value, string Remarks, int Created_By)
      {
        return   objDAL.INSERT_EEDI_DL(VesselID, EEDI_Value, Remarks, Created_By);
      }

      public DataTable Check_EEDI(int VesselId)
      {
          return objDAL.Check_EEDI(VesselId);
      }

      public DataTable SearchEEDI(int Vessel_ID,  int? pagenumber, int? pagesize, ref int isfetchcount)
      {
          return objDAL.SearchEEDI(Vessel_ID, pagenumber, pagesize, ref isfetchcount);
      }
      public DataTable Get_EEDI(int VesselId)
      {
          return objDAL.Get_EEDI(VesselId);
      }
      public int Delete_EEDI(int VesselID, int CreatedBy)
      {
          return objDAL.Delete_EEDI(VesselID, CreatedBy);
      }

      public int Edit_EEDI(int vesselid, decimal eedival, string remarks, int created_by)
      {
          return objDAL.Edit_EEDI(vesselid, eedival, remarks, created_by);
      }
    }
}
