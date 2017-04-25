using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

//using SMS.Data.Infrastructure;
//using SMS.Data.INFRA.Infrastructure;
using SMS.Data.Inspection;
namespace SMS.Business.Inspection
{
  public  class BLL_Insp_VESSELGA
    {
      DAL_Insp_VesselGA objDAL = new DAL_Insp_VesselGA();



        public BLL_Insp_VESSELGA()
        {
            //
            // TODO: Add constructor logic here
            //
        }


     public DataTable Get_VesselGASearch(string searchtext, int? Path_ID, int? Object_ID, int? Fleet_ID
                                                 , string sortby, int? sortdirection, int? pagenumber, int? pagesize, string CompanyID,ref int isfetchcount)
        {

            return objDAL.Get_VesselGASearch_DL(searchtext, Path_ID, Object_ID, Fleet_ID, sortby, sortdirection, pagenumber, pagesize, CompanyID, ref isfetchcount);

        }


     

        public DataSet Get_ParentID( int Parent_Company_ID)
        {
            return objDAL.Get_ParentID_DL(Parent_Company_ID);
        }

        public DataSet Get_ParentID_VT(int VesselType)
        {

       return objDAL.Get_ParentID_VT_DL(VesselType);
  }

        public int InsertVesselGA(string Path_ID, int Object_ID, string Image_Path, string SVG_Path, int Is_GA, string Parent_Path_ID, int? Vessel_TypeID, string Path_Name)
        {
            try
            {
                return objDAL.INS_VesselGA_DL(Path_ID, Object_ID, Image_Path, SVG_Path, Is_GA, Parent_Path_ID, Vessel_TypeID, Path_Name);
            }
            catch
            {
                throw;
            }
        }
        
      public int InsertImport_VesselGA(DataTable dtTableGA)
        {
            try
            {
                return objDAL.INS_IMPORT_VesselGA_DL(dtTableGA);
            }
            catch
            {
                throw;
            }
        }


        public DataTable Get_VesselGA()
        {
            try
            {
                return objDAL.Get_VesselGA_DL();
            }
            catch
            {
                throw;
            }
        }

        public int EditVesselGA(string Path_ID, int Object_ID, string Image_Path, string SVG_Path, int? Is_GA, string Parent_Path_ID, int? Vessel_TypeID, string Path_Name)
        {
            try
            {

                return objDAL.EditVesselGA_DL(Path_ID, Object_ID, Image_Path, SVG_Path, Is_GA, Parent_Path_ID, Vessel_TypeID, Path_Name);
            }
            catch
            {
                throw;
            }

        }

        public int DeleteVesselGA(string imgORsvg, string Path_ID)
        {
            try
            {

                return objDAL.DeleteVesselGA_DL(imgORsvg, Path_ID);
            }
            catch
            {
                throw;
            }

        }
      
    }
}
