using System;
using System.Data;
using System.Configuration;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;


/// <summary>
/// 
/// </summary>
/// 

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_ItemLocation
    {


        public static DataTable GetLocation_Search(int? parent_id, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_PURC_ItemLocation.GetLocation_Search(parent_id, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static int InsertLocation(int ParentId, string Location_Name, int Userid)
        {
            return DAL_PURC_ItemLocation.InsertLocation(ParentId, Location_Name, Userid);
        }

        public static int DeleteLocation(int LocationID,  int Userid)
        {

            return DAL_PURC_ItemLocation.DeleteLocation(LocationID, Userid);
        }

        public static int UpdateLocation(int LocationID, int ParentId, string Location_Name, int Userid)
        {

            return DAL_PURC_ItemLocation.UpdateLocation(LocationID, ParentId, Location_Name, Userid);
        }
        public static DataTable GetParentLocation()
        {

            return DAL_PURC_ItemLocation.GetParentLocation();
        }
        public static DataTable EditLocation(int LocationID)
        {
            return DAL_PURC_ItemLocation.EditLocation(LocationID);
        }

    }
}
