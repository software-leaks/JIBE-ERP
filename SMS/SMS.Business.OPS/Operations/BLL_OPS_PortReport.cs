using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using SMS.Data.Operation;
using System.Configuration;
using SMS.Data.OPS;
namespace SMS.Business.Operation
{
    public class BLL_OPS_PortReport
    {
        public static DataSet Get_PreArrivalPortInfo(int Vessel_ID)
        {
            return DAL_OPS_PortReport.Get_PreArrivalPortInfo_DL(Vessel_ID);
        }
        public static DataSet Get_PreArrivalInfoDetail(int Id, int VesselID)
        {
            return DAL_OPS_PortReport.Get_PreArrivalInfoDetail_DL(Id, VesselID);
        }

        public static DataSet Get_PORT_PreArrival_Info(int ReportID, int Vessel_Id, int Office_Id)
        {
            return DAL_OPS_PortReport.Get_PORT_PreArrival_Info(ReportID, Vessel_Id, Office_Id);
        }

        public static DataSet Get_PORT_PORT_PreArrivalAttachments(int PreArrivalID, int Vessel_Id, int Office_Id, int? Mode = null)
        {
            return DAL_OPS_PortReport.Get_PORT_PORT_PreArrivalAttachments(PreArrivalID, Vessel_Id, Office_Id, Mode);
        }
        public static DataSet GET_PORT_Incidents(int PreArrivalID, int Vessel_Id, int Office_Id)
        {
            return DAL_OPS_PortReport.GET_PORT_Incidents(PreArrivalID, Vessel_Id, Office_Id);
        }

    }
}
