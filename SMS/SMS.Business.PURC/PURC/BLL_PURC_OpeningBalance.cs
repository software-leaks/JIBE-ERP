using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;

/// <summary>
/// Summary description for QuatationEvalution
/// </summary>
/// 
namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_OpeningBalance objOpeningBalance = new DAL_PURC_OpeningBalance();



        public DataTable SelectOBRequistionNumbers()
        {

            return objOpeningBalance.SelectOBRequistionNumbers();



        }

        public DataTable SelectOBItemsForEachRequisiton(string StrQuery)
        {

            return objOpeningBalance.SelectOBItemsForEachRequisiton(StrQuery);

        }


        public DataTable SelectOBIndexGetQuation()
        {

            return objOpeningBalance.SelectOBIndexGetQuation();


        }



        public DataTable SelectProvisionItemConsumption(string VesselCode, string ClosingDate)
        {

            return objOpeningBalance.SelectProvisionItemConsumption(VesselCode, ClosingDate);


        }


        public DataSet GetClosingDate(string VesselCode)
        {

            return objOpeningBalance.GetClosingDate(VesselCode);



        }


        public int UnLockClosingBal(string vesselCode, string closingDate)
        {

            return objOpeningBalance.UnLockClosingBal(vesselCode, closingDate);


        }

        public DataTable GetClosingAmountSum(string vesselCode, string closingDate)
        {


            return objOpeningBalance.GetClosingAmountSum(vesselCode, closingDate);


        }

    }




}