using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Custome defined libararies
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_Airport
    {
        public DataSet GetAirPort(string Search_Text)
        {
            DAL_TRV_Airport ap = new DAL_TRV_Airport();
            try { return ap.Get_AirPorts(Search_Text); }
            catch { throw; }
            finally { ap = null; }
        }
    }
}
