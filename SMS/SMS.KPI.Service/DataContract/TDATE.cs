using System.Runtime.Serialization;
namespace SMS.KPI.Service.DataContract
{
    [DataContract]
    public class TDATE
    {
        private string tid = "";
        private string vid = ""; private string kpid = "";
        [DataMember]
        public string TID
        {
            get { return tid; }
            set { tid = value; }
        }
        [DataMember]
        public string VID
        {
            get { return vid; }
            set { vid = value; }
        }
        [DataMember]
        public string KPID
        {
            get { return kpid; }
            set { kpid = value; }
        }
    }     
}
