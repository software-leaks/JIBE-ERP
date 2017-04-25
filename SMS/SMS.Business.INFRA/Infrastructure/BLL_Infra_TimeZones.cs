using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;



namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_TimeZones
    {

        DAL_Infra_TimeZones objDAL = new DAL_Infra_TimeZones();

        public BLL_Infra_TimeZones()
        {
            
        }

        public DataTable Get_TimeZoneList(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Get_TimeZoneList_DL(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);        
        }

        public DataTable Get_TimeZoneList(int? TimeZoneID)
        {
            return objDAL.Get_TimeZoneList_DL(TimeZoneID);
        }

        public DataTable Get_TimeZoneList()
        {
            return objDAL.Get_TimeZoneList_DL(null);
        }

        public int EditTimeZone(int TimeZoneID, string TimeZone_DisplayName, string BaseUtcOffSet, int DefaultTimeZone, int CreatedBy)
        {
            return objDAL.EditTimeZone_DL(TimeZoneID, TimeZone_DisplayName,BaseUtcOffSet,DefaultTimeZone, CreatedBy);        
        }

        public int PopulateTimeZones(int CreatedBy)
        {
            return objDAL.PopulateTimeZones_DL(CreatedBy);
        }

        public int InsertTimeZone(string TimeZone, int CreatedBy)
        {
            return objDAL.InsertTimeZone_DL(TimeZone, CreatedBy);
        }

        public int DeleteTimeZone(int TimeZoneID, int CreatedBy)
        {
            return objDAL.DeleteTimeZone_DL(TimeZoneID, CreatedBy);
        }

    }

}
