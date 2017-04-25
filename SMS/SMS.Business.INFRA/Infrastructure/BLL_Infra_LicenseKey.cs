using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;

namespace SMS.Business.Infrastructure
{
  
    public class BLL_Infra_LicenseKey
    {

 
        public static DataTable LicenseKeySearch(int? Vessel_ID, int? Status, string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Infra_LicenseKey.LicenseKeySearch(Vessel_ID, Status, sortby, SearchText,sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public static int UpdateLicenseKey(int ID, int? VESSEL_ID, string LICENSE_KEY, int SENT_BY)
        {
            return DAL_Infra_LicenseKey.UpdateLicenseKey(ID, VESSEL_ID, LICENSE_KEY, SENT_BY);
        }

    }

}
