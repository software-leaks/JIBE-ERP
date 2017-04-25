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
    public class BLL_TRV_Airline
    {
        public DataSet GetAirline(string Search_Text)
        {
            DAL_TRV_Airline al = new DAL_TRV_Airline();
            try { return al.Get_Airlines(Search_Text); }
            catch { throw; }
            finally { al = null; }
        }
    }
}
