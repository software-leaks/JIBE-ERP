using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public static class BLL_Crew_CrewMail
    {
        public static DataTable Get_PendingItems(int FleetCode, int Vessel_ID, int UserID, string SearchText,int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewMail.Get_PendingItems_DL(FleetCode, Vessel_ID, UserID, SearchText,PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }

        public static DataSet Get_VesselOpenPacket(int Vessel_ID, int UserID)
        {
            return DAL_Crew_CrewMail.Get_VesselOpenPacket_DL(Vessel_ID, UserID);
        }
        public static DataSet Get_MailPacket(int PacketID, int UserID)
        {
            return DAL_Crew_CrewMail.Get_MailPacket_DL(PacketID, UserID);
        }

        public static DataTable Get_PacketItems_Async(int PacketID)
        {
            return DAL_Crew_CrewMail.Get_PacketItems_Async(PacketID);
        }

        public static DataSet Get_PacketItems(int FleetCode, int Vessel_ID, int Status, int UserID, string SearchText, int? PAGE_SIZE, int? PAGE_INDEX,  ref int SelectRecordCount)
        {
            return DAL_Crew_CrewMail.Get_PacketItems_DL(FleetCode, Vessel_ID, Status , UserID, SearchText, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }

        public static int INSERT_CrewMailItem(int Vessel_ID, string Item_Ref, string Item_Desc, decimal Item_Qty, string Item_Remarks, string DatePlaced, int Created_By)
        {
            DateTime dtDatePlaced = DateTime.Today;

            if (DatePlaced != "")
                dtDatePlaced = DateTime.Parse(DatePlaced);
            return DAL_Crew_CrewMail.INSERT_CrewMailItem_DL(Vessel_ID, Item_Ref, Item_Desc, Item_Qty, Item_Remarks, dtDatePlaced, Created_By);
        }

        public static int DELETE_CrewMailItem(int ID, int Deleted_By)
        {
            return DAL_Crew_CrewMail.DELETE_CrewMailItem_DL(ID, Deleted_By);
        }

        public static int Save_MailPacket(int PacketID, string DateSent, string SentUsing, string AirwayBill, int DeliveryPort, string ETA, int Modified_By,string ApproverRemarks)
        {
            DateTime dtDateSent = DateTime.Parse("1900/01/01");
            DateTime dtETA = DateTime.Parse("1900/01/01");

            if (DateSent != "")
                dtDateSent = DateTime.Parse(DateSent);

            if (ETA != "")
                dtETA = DateTime.Parse(ETA);

            return DAL_Crew_CrewMail.Save_MailPacket_DL(PacketID, dtDateSent, SentUsing, AirwayBill, DeliveryPort, dtETA, Modified_By, ApproverRemarks);
        }

        public static int SaveAndSend_MailPacket(int PacketID, string DateSent, string SentUsing, string AirwayBill, int DeliveryPort, string ETA, int Modified_By, string ApproverRemarks)
        {
            DateTime dtDateSent = DateTime.Parse("1900/01/01");
            DateTime dtETA = DateTime.Parse("1900/01/01");

            if (DateSent != "")
                dtDateSent = DateTime.Parse(DateSent);

            if (ETA != "")
                dtETA = DateTime.Parse(ETA);

            return DAL_Crew_CrewMail.SaveAndSend_MailPacket_DL(PacketID, dtDateSent, SentUsing, AirwayBill, DeliveryPort, dtETA, Modified_By, ApproverRemarks);
        }

        public static int Discard_MailPacket(int PacketID, int Modified_By)
        {
            return DAL_Crew_CrewMail.Discard_MailPacket_DL(PacketID, Modified_By);
        }

        public static int AddItemTo_Packet(int Vessel_ID, int PacketID, int ItemID, int Created_By)
        {
            return DAL_Crew_CrewMail.AddItemTo_Packet_DL(Vessel_ID, PacketID, ItemID, Created_By);
        }

        public static int RemoveItem_FromPacket(int ID, int Deleted_By)
        {
            return DAL_Crew_CrewMail.RemoveItem_FromPacket_DL(ID, Deleted_By);
        }

        public static int SendMail_Vessel(int PacketID, int UserID)
        {
            return DAL_Crew_CrewMail.SendMail_Vessel_DL(PacketID, UserID);
        }

        public static DataTable Generate_CoverLetter(int PacketID, int UserID)
        {
            return DAL_Crew_CrewMail.Generate_CoverLetter_DL(PacketID, UserID);
        }
    }
}
