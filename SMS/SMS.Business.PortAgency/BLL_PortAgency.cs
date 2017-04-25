using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.PortAgency;

namespace SMS.Business.PortAgency
{
    public class BLL_PortAgency
    {
        DAL_PortAgency objDALPortAgency = new DAL_PortAgency();

        public DataSet GetRequestCount()
        {
            return objDALPortAgency.GetRequestCount();
        }



        public DataTable GetAllPorts()
        {
            return objDALPortAgency.GetAllPorts();
        }


        public DataTable Get_PortCall_List(int? VesselId, string Port_Name, DateTime? Startdate, DateTime? EndDate, bool FilterProformaRequest, bool FilterProformaApproval, bool FilterAdditionalJobs,
    bool FilterAgencyWorkRequest, bool FilterCrewChangeRequest, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDALPortAgency.Get_PortCall_List(VesselId, Port_Name, Startdate, EndDate, FilterProformaRequest, FilterProformaApproval, FilterAdditionalJobs, FilterAgencyWorkRequest, FilterCrewChangeRequest,
                sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
    }
}
