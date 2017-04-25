using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_OfficeDepartment
    {

        DAL_Infra_OfficeDepartment objDept = new DAL_Infra_OfficeDepartment();

        public DataTable SearchOfficeDepartment(string searchtext, int? VesselManager
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDept.SearchOfficeDepartment(searchtext, VesselManager, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);       
        
        }


        public DataTable Get_OfficeDepartment_List(int? DeptID)
        {
            return objDept.Get_OfficeDepartment_List_DL(DeptID);
        }

        public int EditOfficeDepartment(int DeptID, string DeptName, int? vessel_manager, int CreatedBy)
        {

            return objDept.EditOfficeDepartment_DL(DeptID, DeptName, vessel_manager, CreatedBy);
        }

        public int InsertOfficeDepartment(string DeptName, int? vessel_manager, int CreatedBy)
        {
            return objDept.InsertOfficeDepartment_DL(DeptName, vessel_manager, CreatedBy);
        }

        public int DeleteOfficeDepartment_DL(int DeptID, int CreatedBy)
        {

            return objDept.DeleteOfficeDepartment_DL(DeptID, CreatedBy);
        }


    }
}
