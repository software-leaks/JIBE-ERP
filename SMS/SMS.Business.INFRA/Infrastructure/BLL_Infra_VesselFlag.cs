using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_VesselFlag
    {

        DAL_Infra_VesselFlag objDAL = new DAL_Infra_VesselFlag();

        public BLL_Infra_VesselFlag()
        {
            
        }

        public DataTable SearchVesselFlag(string searchtext, int? VesselManager, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchVesselFlag(searchtext, VesselManager, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public DataTable SearchVesselFlag(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchVesselFlag(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public DataTable Get_VesselFlage_List(int? VslFlagID)
        {
            return objDAL.Get_VesselFlage_List_DL(VslFlagID);
        }

        public int EditVesselFlag(int VslFlagID, string Flag_Name, int? vessel_manager, string mailid, int CreatedBy)
        {
            return objDAL.EditVesselFlag_DL(VslFlagID, Flag_Name, vessel_manager, mailid, CreatedBy);
        }

        public int InsertVesselFlag(string Flag_Name, int? vessel_manager, string mailid, int CreatedBy)
        {
            return objDAL.InsertVesselFlag_DL(Flag_Name, vessel_manager, mailid, CreatedBy);
        }

        public int EditVesselFlag(int VslFlagID, string Flag_Name, string mailid, int CreatedBy)
        {
            return objDAL.EditVesselFlag_DL(VslFlagID, Flag_Name, mailid, CreatedBy);
        }

        public int InsertVesselFlag(string Flag_Name, string mailid, int CreatedBy)
        {
            return objDAL.InsertVesselFlag_DL(Flag_Name, mailid, CreatedBy);
        }

        public int DeleteVesselFlag(int UserTypeID, int CreatedBy)
        {
            return objDAL.DeleteVesselFlag_DL(UserTypeID, CreatedBy);
        }

    }
}
