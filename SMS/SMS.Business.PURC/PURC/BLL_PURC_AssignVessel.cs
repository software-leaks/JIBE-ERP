using System;
using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_AssignVessel objAssignVessel = new DAL_PURC_AssignVessel();

        public DataTable GetLocation()
        {


            return objAssignVessel.GetLocation();


        }

        public DataTable GetVesselName()
        {


            return objAssignVessel.GetVesselName();


        }

        public DataTable GetFleet()
        {

            return objAssignVessel.GetFleet();



        }

        public DataTable assignLocation()
        {


            return objAssignVessel.assignLocation();



        }
        public DataTable UnassignLocation()
        {


            return objAssignVessel.UnassignLocation();

        }

        public int VLassignLocation(AssignVesselData objDOAssignVessel)
        {


            return objAssignVessel.VLassignLocation(objDOAssignVessel);


        }
        public int VLUnassignLocation(AssignVesselData objDOAssignVessel)
        {


            return objAssignVessel.VLUnassignLocation(objDOAssignVessel);


        }




        public int VDeleteLocation(string Vessel_code, string code)
        {

            return objAssignVessel.VDeleteLocation(Vessel_code, code);

        }


        public int VDeletecatelog(string Vessel_code, string code, string System_Code)
        {

            return objAssignVessel.VDeletecatelog(Vessel_code, code, System_Code);

        }





        public DataTable AssignCatalogs()
        {

            return objAssignVessel.assignCatalogs();

        }
        public DataTable UnassignCatalogs()
        {

            return objAssignVessel.UnassignCatalogs();

        }



        public DataTable VassignCatalogs(string Vessel_code)
        {

            return objAssignVessel.VassignCatalogs(Vessel_code);

        }
        public DataTable VUnassignCatalogs(string Vessel_code)
        {


            return objAssignVessel.VUnassignCatalogs(Vessel_code);


        }

        public DataTable VLassignCatalogs(string Vessel_code, string code)
        {

            return objAssignVessel.VLassignCatalogs(Vessel_code, code);

        }
        public DataTable VLUnassignCatalogs(string Vessel_code, string code)
        {

            return objAssignVessel.VLUnassignCatalogs(Vessel_code, code);

        }

        public DataTable VassignLocation(string vessel_code)
        {


            return objAssignVessel.VassignLocation(vessel_code);




        }
        public DataTable VUnassignLocation(string Vessel_code)
        {


            return objAssignVessel.VUnassignLocation(Vessel_code);

        }


    }
}
