using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PURC;
using System.Data;


namespace SMS.Business.PURC
{
    public class BLL_PURC_ReqsnMandatory
    {

        DAL_PURC_ReqsnMandatory objDalReqsnMandat = new DAL_PURC_ReqsnMandatory();
       
        public DataSet Get_MandatoryData_BLL()
        {
           return objDalReqsnMandat.Get_MandatoryData_DAL();
            
        }
        public string PURC_UPD_Mandatory_BLL(DataTable dt)
        {
            return objDalReqsnMandat.PURC_UPD_Mandatory_DAL(dt);
        }
    }
}
