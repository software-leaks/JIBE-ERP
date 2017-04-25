using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.eForms;

namespace SMS.Business.eForms
{
    public class BLL_Infra_LashingGearInventoryLib
    {
        DAL_Infra_LashingGearInventoryLib objDAL = new DAL_Infra_LashingGearInventoryLib();
        

        public DataTable Lashing_Gear_Inventory_Search(string searchtext, string ItemModel, int? Vessel_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Lashing_Gear_Inventory_Search(searchtext, ItemModel, Vessel_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int Insert_Lashing_Gear_Inventory(string Item_Description, string Model_No, string Carg_Securing_Mannual_No, int? Vessel_ID, int CreatedBy)
        {
            return objDAL.Insert_Lashing_Gear_Inventory(Item_Description, Model_No, Carg_Securing_Mannual_No, Vessel_ID, CreatedBy);
        }

        public int Edit_Lashing_Gear_Inventory(int ID, string Item_Description, string Model_No, string Carg_Securing_Mannual_No, int? Vessel_ID, int CreatedBy)
        {
            return objDAL.Lashing_Gear_Inventory_DL(ID, Item_Description, Model_No, Carg_Securing_Mannual_No, Vessel_ID, CreatedBy);
        }
        public DataTable Get_Lashing_Gear_Inventory(int? ID, int Vessel_ID)
        {
            return objDAL.Lashing_Gear_Inventory_DL(ID, Vessel_ID);
        }

        public int Delete_Lashing_Gear_Inventory(int ID, int Vessel_ID, int CreatedBy)
        {
            return objDAL.Delete_Lashing_Gear_Inventory_DL(ID, Vessel_ID, CreatedBy);
        }
    }
}
