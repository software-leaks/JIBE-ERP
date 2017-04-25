using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data.PMS;


namespace SMS.Business.PMS
{
    public class BLL_PMS_Library_Items
    {
        SMS.Data.PMS.DAL_PMS_Library_Items objItems = new DAL_PMS_Library_Items();
        public BLL_PMS_Library_Items()
        {
        }
        public DataSet LibraryItemsGetToCopy(string systemcode, int? subsystemid, int? vesselid, int? IsActive, string Resqn, string functions)
        {
            return objItems.LibraryItemsGetToCopy(systemcode, subsystemid, vesselid, IsActive,Resqn, functions);
        }

        public DataSet LibraryItemsGetToMove(string systemcode, int? subsystemid, int? vesselid, int? IsActive, string Resqn, string functions, string searchtxt)
        {
            return objItems.LibraryItemsGetToMove(systemcode, subsystemid, vesselid, IsActive, Resqn, functions,searchtxt);
        }

        public int Items_MOVE(string systemcode, int? subsystemid, int? vesselid, string selectedItems, string user)
        {
            return objItems.Items_MOVE(systemcode, subsystemid, vesselid, selectedItems, user);
        }
        //public int Items_MOVE(string systemcode, int? subsystemid, int? vesselid, int? IsActive, string Resqn, string functions,string Frm_systemcode, int? Frm_subsystemid, int? 
        //    Frm_vesselid, 
        //    string Frm_functions ,string user)
        //{
        //    return objItems.Items_MOVE(systemcode, subsystemid, vesselid, IsActive, Resqn, functions, Frm_systemcode,  Frm_subsystemid, 
        //    Frm_vesselid,
        //    Frm_functions , user);
        //}

        public int PURC_Items_COPY_Append(string systemcode, int? subsystemid, int? vesselid,string TOsystemcode, string TOsubsystemid, int? IsActive, string user)
        {
            return objItems.PURC_Items_COPY_Append(systemcode, subsystemid, vesselid, TOsystemcode,  TOsubsystemid, IsActive, user);
        }

        public int PURC_Items_COPY_OVERWRITE(string FROMsystemcode, string FROMsubsystemid, string FROMvesselid, string user, string TOsystemcode, string TOsubsystemid, string TOvesselid)
        {
            return objItems.PURC_Items_COPY_OVERWRITE(FROMsystemcode, FROMsubsystemid, FROMvesselid, user, TOsystemcode, TOsubsystemid, TOvesselid);
        }
        public DataTable Get_Functional_Tree_Data_ManageSystems( int Vessel_ID,  string searchText,  int? IsActive, string FormType)
        {
           
            return objItems.Get_Functional_Tree_Data_ManageSystems( Vessel_ID,searchText, IsActive, FormType);
        }

        public DataTable Get_Functional_Tree_ManageSystem_Data(int Vessel_ID, string searchText, int? IsActive, string FormType, string Function, string Equipment_Type, string FilterFunctionCode)
        {

            return objItems.Get_Functional_Tree_ManageSystem_Data(Vessel_ID, searchText, IsActive, FormType,Function,Equipment_Type,FilterFunctionCode);
        }
       
    }

}
