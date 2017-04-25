using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class TRV_Request
    {
        public int StaffID { get; set; }
        public string Travel_Origin { get; set; }
        public string Travel_Destination { get; set; }
        public string Departure_Date { get; set; }
        public string Return_Date { get; set; }
        public string Preferred_Departure_Time { get; set; }
        public string Preferred_Airline { get; set; }
        public string Travel_Class { get; set; }
        public string Travel_Type { get; set; }
        public int Is_Seaman_Ticket { get; set; }
        public int isPersonal_Ticket { get; set; }
        public string Remarks { get; set; }
        public int Created_By { get; set; }
        public int EnentID { get; set; }
        public int VoyageID { get; set; }
        public string PrefDepHrs { get; set; }
        public string PrefDepMin { get; set; }
    }
}
