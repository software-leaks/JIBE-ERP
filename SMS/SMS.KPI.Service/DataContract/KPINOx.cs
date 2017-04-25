using System.Runtime.Serialization;
namespace SMS.KPI.Service.DataContract
{
    [DataContract]
    public class KPINOx
    {
        private string Value = "";
        private string RDate = "";
        private string avrg = "";
        private string eedi = "";
        private string _Start_date = "";
        private string _End_date = "";
        private string vessel_Id = "";
        private string vessel_Name = "";

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
        public string VALUE
        {
            get { return Value; }
            set { Value = value; }
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
        public string EEDI
        {
            get { return eedi; }
            set { eedi = value; }
        }
    }
    [DataContract]
    public class KPINOxVOYAGE
    {
        private string efrom = "";
        private string eto = "";
        private string fport = "";
        private string tport = "";
        private string avalue = "";
        private string port = "";
        private string avrg = "";
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
        public string VALUE
        {
            get { return avalue; }
            set { avalue = value; }
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
        public string AVERAGE
        {
            get { return avrg; }
            set { avrg = value; }
        }
    }
}
