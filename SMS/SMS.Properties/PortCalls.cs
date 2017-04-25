using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class PortCalls
    {
        public int Port_Call_ID
        { get; set; }

        public int Port_ID
        { get; set; }

        public string Port_Name
        { get; set; }

        public DateTime? Arrival_Date
        { get; set; }

        public DateTime? Departure_Date
        { get; set; }

        public string Owners_Agent_Name
        { get; set; }

        public string Charterers_Agent_Name
        { get; set; }

        public string Owners_Agent_Code
        { get; set; }

        public string Charterers_Agent_Code
        { get; set; }


    }

    
}
