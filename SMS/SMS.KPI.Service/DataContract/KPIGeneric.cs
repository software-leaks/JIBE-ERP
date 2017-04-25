using System.Runtime.Serialization;
namespace SMS.KPI.Service.DataContract
{
    [DataContract]
    public class KPIGeneric
    {
        private string Value = "";
        private string RDate = "";
        private string avrg = "";
        private string _Interval = "";
        private string vessel_Id = "";
        private string vessel_Name = "";
        private string _Start_date = "";
        private string _End_date = "";
        private string _Value_Type = "";
        private string tid = "";
        private string kid = "";
        private string efrom = "";
        private string eto = "";
        private string fport = "";
        private string tport = "";
        private string avalue = "";
        private string port = "";
        private string _KPI_Name = "";
        private string _Qtr = "";
        private string goal = "";
        
        [DataMember]
        public string TID
        {
            get { return tid; }
            set { tid = value; }
        }

        
        [DataMember]
        public string Value_Type
        {
            get { return _Value_Type; }
            set { _Value_Type = value; }
        }
        

        [DataMember]
        public string KID
        {
            get { return kid; }
            set { kid = value; }
        }
        [DataMember]
        public string VALUE
        {
            get { return Value; }
            set { Value = value; }
        }

        [DataMember]
        public string Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }
        [DataMember]
        public string End_date
        {
            get { return _End_date; }
            set { _End_date = value; }
        }

        [DataMember]
        public string Start_date
        {
            get { return _Start_date; }
            set { _Start_date = value; }
        }

        [DataMember]
        public string Vessel_Name
        {
            get { return vessel_Name; }
            set { vessel_Name = value; }
        }


        [DataMember]
        public string Vessel_Id
        {
            get { return vessel_Id; }
            set { vessel_Id = value; }
        }

        [DataMember]
        public string RDATE
        {
            get { return RDate; }
            set { RDate = value; }
        }

        [DataMember]
        public string AVERAGE
        {
            get { return avrg; }
            set { avrg = value; }
        }

        [DataMember]
        public string EFROM
        {
            get { return efrom; }
            set { efrom = value; }
        }
        [DataMember]
        public string ETO
        {
            get { return eto; }
            set { eto = value; }
        }
        [DataMember]
        public string FPORT
        {
            get { return fport; }
            set { fport = value; }
        }

        [DataMember]
        public string TPORT
        {
            get { return tport; }
            set { tport = value; }
        }
        [DataMember]
        public string PORT
        {
            get { return port; }
            set { port = value; }
        }


        [DataMember]
        public string KPI_Name
        {
            get { return _KPI_Name; }
            set { _KPI_Name = value; }
        }


        [DataMember]
        public string Qtr
        {
            get { return _Qtr; }
            set { _Qtr = value; }
        }


        [DataMember]
        public string GOAL
        {
            get { return goal; }
            set { goal = value; }
        }

    }

}



