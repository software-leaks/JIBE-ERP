using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Operation;

namespace SMS.Business.Operation
{
    public class BLL_OPS_DPL
    {
        public static DataTable Get_Ports_NearVessel(string ship_long, string ship_lat, string longdire, string latdire)
        {
            return DAL_OPS_DPL.Get_Ports_NearVessel_DL(ship_long, ship_lat, longdire, latdire);
        }

        public static DataTable Get_TelegramData(int Vessel_ID, int Fleet_ID, string Telegram_Type)
        {
            return DAL_OPS_DPL.Get_TelegramData_DL(Vessel_ID, Fleet_ID, Telegram_Type);
        }

        public static DataTable Get_TelegramData_Route(int Vessel_ID, int Fleet_ID, DateTime? FromDT, DateTime? ToDt, string Telegram_Type)
        {
            return DAL_OPS_DPL.Get_TelegramData_Route_DL(Vessel_ID, Fleet_ID, FromDT, ToDt, Telegram_Type);
        }

        public static DataTable Get_PiracyArea()
        {
            return DAL_OPS_DPL.Get_PiracyArea_DL();
        }

        public static DataTable Get_Piracy_Alarms()
        {
            return DAL_OPS_DPL.Get_Piracy_Alarms_DL();
        }
        public static DataTable Get_Piracy_Alarm_Change_Log(int Vessel_ID)
        {
            return DAL_OPS_DPL.Get_Piracy_Alarm_Change_Log_DL(Vessel_ID);
        }

        public static int Toggle_Piracy_Alarm_Status(int Vessel_ID, int UserID,string Remarks)
        {
            return DAL_OPS_DPL.Toggle_Piracy_Alarm_Status_DL(Vessel_ID, UserID, Remarks);
        }
    
    }
    
}
