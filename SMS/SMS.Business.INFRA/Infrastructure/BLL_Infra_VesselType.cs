
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_VesselType
    {
    DAL_Infra_VesselType objDAL = new  DAL_Infra_VesselType();

        public BLL_Infra_VesselType()
        {

        }
        public DataTable SearchVesselType(string searchtext,  string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchVesselType(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public int InsertVesselType(string UserType, int CreatedBy, string TankerType)
        {
            return objDAL.InsertVesselType_DL(UserType, CreatedBy,TankerType);
        }
        public int EditVesselType(int VesselTypeID, string UserType, int CreatedBy, string TankerType)
        {
            return objDAL.EditVesselType_DL(VesselTypeID, UserType, CreatedBy,TankerType);

        }
        public int DeleteVesselType(int UserTypeID, int CreatedBy)
        {
            return objDAL.DeleteVesselType_DL(UserTypeID, CreatedBy);

        }
        public DataTable Get_VesselTypeList(int? VesselTypeID)
        {
            return objDAL.Get_VesselTypeList_DL(VesselTypeID);
        }
        public DataTable Get_VesselType()
        {
            try
            {
                return objDAL.Get_VesselType_DL();
            }
            catch
            {
                throw;
            }
        }
    }
}
