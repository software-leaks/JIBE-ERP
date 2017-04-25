using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SMS.KPI.Service.DataContract
{
    /// <summary>
    /// Class to maintain PMS Overdue  parameters
    /// </summary>
    [DataContract]
    public class PMSOverdue
    {
        private string _Value = "";
        private string _NonCriticalOverdue = "";
        private string _AllOverdue = "";
        private string _CriticalOverdue = "";
        private string _Startdate = "";
        private string _End_date = "";
        private string _MonthYear = "";
        private string _Category = "";
        private string _VesselIDs= "";
        private string _KPIID = "";
        private string _Vessel = "";

        [DataMember]
        public string VesselIDs
        {
            get { return _VesselIDs; }
            set { _VesselIDs = value; }
        }

        [DataMember]
        public string MonthYear
        {
            get { return _MonthYear; }
            set { _MonthYear = value; }
        }

        [DataMember]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        [DataMember]
        public string NonCriticalOverdue
        {
            get { return _NonCriticalOverdue; }
            set { _NonCriticalOverdue = value; }
        }


        [DataMember]
        public string AllOverdue
        {
            get { return _AllOverdue; }
            set { _AllOverdue = value; }
        }

        [DataMember]
        public string Start_date
        {
            get { return _Startdate; }
            set { _Startdate = value; }
        }


        [DataMember]
        public string End_date
        {
            get { return _End_date; }
            set { _End_date = value; }
        }

        [DataMember]
        public string CriticalOverdue
        {
            get { return _CriticalOverdue; }
            set { _CriticalOverdue = value; }
        }


        [DataMember]
        public string KPIID
        {
            get { return _KPIID; }
            set { _KPIID = value; }
        }

        [DataMember]
        public string Vessel
        {
            get { return _Vessel; }
            set { _Vessel = value; }
        }
    }


}
