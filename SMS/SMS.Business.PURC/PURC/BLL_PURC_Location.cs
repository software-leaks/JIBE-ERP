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

    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_Location objLocation = new DAL_PURC_Location();


        public DataTable Location_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objLocation.Location_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public int SaveLocation(LocationData objDOLocation)
        {

            return objLocation.SaveLocation(objDOLocation);



        }
        public int SaveEditLocation(LocationData objDOLocation)
        {

            return objLocation.SaveEditLocation(objDOLocation);



        }
        public int PMS_Insert_AssignLocation(string ShortCode, string ShortDesc, string SystemCode, string SubSysCode,  int VesselID, string CatCode, int CreatedBy)
        {
            return objLocation.PMS_Insert_AssignLocation(ShortCode, ShortDesc, SystemCode, SubSysCode,  VesselID, CatCode, CreatedBy);
        }
        public int DeleteLocation(LocationData objDOLocation)
        {

            return objLocation.DeleteLocation(objDOLocation);




        }

        public int EditLocation(LocationData objDOLocation)
        {

            return objLocation.EditLocation(objDOLocation);



        }



        public DataTable SelectLocation()
        {

            return objLocation.SelectLocation();

        }


        public int CheckchildCount(string Pcode)
        {

            return objLocation.CheckchildCount(Pcode);


        }

        public DataTable BindLocationCombo()
        {

            return objLocation.BindLocationCombo();


        }



    }
}
