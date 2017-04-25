using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;


namespace SMS.Business.Infrastructure
{
   public class BLL_Infra_ShipDepartment
    {

       DAL_Infra_ShipDepartment objDept = new DAL_Infra_ShipDepartment();

       public DataTable SearchShipDepartment(string searchtext, int? VesselManager , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDept.SearchShipDepartment(searchtext, VesselManager, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

       public DataTable Get_ShipDepartment_List(int? DeptID)
        {
            return objDept.Get_ShipDepartment_List_DL(DeptID);
        }

       public int EditShipDepartment(int DeptID, string DeptName, int? vessel_manager, int CreatedBy)
        {

            return objDept.EditShipDepartment_DL(DeptID, DeptName, vessel_manager, CreatedBy);
        }

        public int InsertShipDepartment(string DeptName, int? vessel_manager, int CreatedBy)
        {
            return objDept.InsertShipDepartment_DL(DeptName, vessel_manager, CreatedBy);
        }

        public int DeleteShipDepartment(int DeptID, int CreatedBy)
        {

            return objDept.DeleteShipDepartment_DL(DeptID, CreatedBy);
        }

    }
}
