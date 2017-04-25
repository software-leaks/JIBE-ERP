using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SMS.KPI.Service.DataContract
{
    /// <summary>
    /// Class to maintain crew retention operation parameters
    /// </summary>
    [DataContract]
    public class CrewRetention
    {
        private string _Value = "";
        private string _avrg = "";
        private string _Rank = "";
        private string _Nationality = "";
        private string _Startdate = "";
        private string _End_date = "";
        private string _Quarter = "";
        private string _Year = "";
        private string _Category = "";
        private string _NTBR = "";
        private string _LeftAll = "";


        [DataMember]
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }



        [DataMember]
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }


        [DataMember]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        [DataMember]
        public string Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
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
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }


        [DataMember]
        public string AVERAGE
        {
            get { return _avrg; }
            set { _avrg = value; }
        }

        [DataMember]
        public string Quarter
        {
            get { return _Quarter; }
            set { _Quarter = value; }
        }

        [DataMember]
        public string NTBR
        {
            get { return _NTBR; }
            set { _NTBR = value; }
        }

        [DataMember]
        public string LeftAll
        {
            get { return _LeftAll; }
            set { _LeftAll = value; }
        }


    }


}
