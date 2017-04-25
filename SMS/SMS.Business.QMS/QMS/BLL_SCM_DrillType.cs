using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SMS.Data.QMS;

namespace SMS.Business.QMS
{
    public class BLL_SCM_DrillType
    {

         DAL_SCM_DrillType objDAL = new DAL_SCM_DrillType();

         public BLL_SCM_DrillType()
        {
            //
            // TODO: Add constructor logic here
            //
        }


         public DataTable SearchDrillType(int? VesselID, string DrillName
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchDrillType(VesselID, DrillName,  sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

         public DataTable Get_DrillTypeList(int DrillID)
         {
             return objDAL.Get_DrillTypeList(DrillID);

         }


         public int EditDrillType(int? VesselID, string DrillName,int? frequency, int AccessID, int Created_By)
        {
            try
            {
                return objDAL.EditDrillType(VesselID, DrillName,frequency, AccessID, Created_By);
            }
            catch
            {
                throw;
            }
        }

         public int InsertDrillType(int? VesselID, string DrillName, int? frequency, int Created_By)
        {
            try
            {
                return objDAL.InsertDrillType(VesselID, DrillName,frequency, Created_By);
            }
            catch
            {
                throw;
            }
        }

         public int DeleteDrillType(int DrillID, int Created_By)
        {
            try
            {
                return objDAL.DeleteDrillType(DrillID, Created_By);
            }
            catch
            {
                throw;
            }
        }
    }
}
