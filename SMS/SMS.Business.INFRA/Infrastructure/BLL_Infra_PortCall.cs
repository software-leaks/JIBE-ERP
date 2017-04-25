using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_Port
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_PortCall
    {
        DAL_Infra_PortCall objPortCall = new DAL_Infra_PortCall();


        public BLL_Infra_PortCall()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable Get_PortCall_List(int Port_Call_ID, int Vessel_ID)
        {
            return objPortCall.Get_PortCall_List_DL(Port_Call_ID, Vessel_ID);
        }

        public DataTable Get_PortCall_Search(string searchText, int? Vessel_ID, int? Port_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objPortCall.Get_PortCall_Search(searchText, Vessel_ID, Port_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }



        public int Ins_PortCall_Details(string Vessel_Code, string Sub_Voyage, string Port_Name, DateTime? Arrival, DateTime? Berthing
           , DateTime? Departure, string Auto_Date, string Berth_Number, string Port_Remarks, int? Charterers_Agent, int? Owners_Agent
           , int Port_ID, string Charter_ID, int Vessel_ID, int Created_By)
        {

            return objPortCall.Ins_PortCall_Details_DL(Vessel_Code, Sub_Voyage, Port_Name, Arrival, Berthing, Departure, Auto_Date, Berth_Number, Port_Remarks, Charterers_Agent, Owners_Agent
                , Port_ID, Charter_ID, Vessel_ID, Created_By);
        }


        public int Upd_PortCall_Details(int Port_Call_ID, string Vessel_Code, string Sub_Voyage, string Port_Name, DateTime? Arrival, DateTime? Berthing
        , DateTime? Departure, string Auto_Date, string Berth_Number, string Port_Remarks, int? Charterers_Agent, int? Owners_Agent
        , int Port_ID, string Charter_ID, int Vessel_ID, int Created_By)
        {

            return objPortCall.Upd_PortCall_Details_DL(Port_Call_ID, Vessel_Code, Sub_Voyage, Port_Name, Arrival, Berthing, Departure, Auto_Date, Berth_Number, Port_Remarks, Charterers_Agent, Owners_Agent
               , Port_ID, Charter_ID, Vessel_ID, Created_By);
        }


        public int Del_PortCall_Details_DL(int Port_Call_ID, int Vessel_ID, int Created_By)
        {
            return objPortCall.Del_PortCall_Details_DL(Port_Call_ID, Vessel_ID, Created_By);
        }
    }
}
