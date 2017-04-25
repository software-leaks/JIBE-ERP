using System.Data;
using SMS.Properties;
using SMS.Data.PURC;
using System;
using SMS.Data;

/// <summary>
/// Summary description for Vessels
/// </summary>
namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {


        DAL_PURC_Vessels objVessel = new DAL_PURC_Vessels();



        public DataSet SelectFleet()
        {

            return objVessel.SelectFleet();


        }
        public string SelectFleetByUser(string user)
        {

            return objVessel.SelectFleetByUser(user);

        }

        public DataTable SelectVessels()
        {
            return objVessel.SelectVessels();

        }

        public  string Get_LegalTerm(int LegalType)
        {
           return objVessel.Get_LegalTerm_DL(LegalType);
        }
             

    }
}
