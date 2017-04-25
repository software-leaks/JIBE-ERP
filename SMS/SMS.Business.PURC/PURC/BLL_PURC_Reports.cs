using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;


/// <summary>
/// Summary description for Reports
/// </summary>
namespace SMS.Business.PURC
{
    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_Reports objReports = new DAL_PURC_Reports();

        //
        // TODO: Add constructor logic here
        //

        public DataTable GetReportBondItemCharters(string VesselCode, string FromDate, string ToDate)
        {

            return objReports.GetReportBondItemCharters(VesselCode, FromDate, ToDate);


        }

        public DataTable GetReportBondItemOwner(string VesselCode, string FromDate, string ToDate)
        {
            return objReports.GetReportBondItemOwner(VesselCode, FromDate, ToDate);


        }

        public DataSet GetReportProvisionItemCharters(string VesselCode, string FromDate, string ToDate)
        {

            return objReports.GetReportProvisionItemCharters(VesselCode, FromDate, ToDate);


        }

        public DataSet GetReportProvisionItemOwner(string VesselCode, string FromDate, string ToDate)
        {

            return objReports.GetReportProvisionItemOwner(VesselCode, FromDate, ToDate);


        }

        public DataSet GetReportC_2Mandays(string FromDate, string Todate, string VesselCode)
        {

            return objReports.GetReportC_2Mandays(FromDate, Todate, VesselCode);


        }



        public DataSet GetReportBondProvision_D11(string FromDate, string Todate, string VesselCode)
        {

            return objReports.GetReportBondProvision_D11(FromDate, Todate, VesselCode);


        }

        public DataSet GetReportProvisionInventory(string VesselCode, string DeptCode)
        {

            return objReports.GetReportProvisionInventory(VesselCode, DeptCode);


        }


        public DataSet GetReqItemsPreview(string ReqCode, string VesselCode, string Documentcode)
        {

            return objReports.GetReqItemsPreview(ReqCode, VesselCode, Documentcode);


        }
        public DataSet ConfiguredSupplierPreview(string Document_code, string ReqsCode, string Searchtext)
        {
            return objReports.ConfiguredSupplierPreview( Document_code, ReqsCode,Searchtext);
        }




    }
}
