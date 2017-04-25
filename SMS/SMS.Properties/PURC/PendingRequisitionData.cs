using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{
   

    public class PendingRequisitionData
    {
        public PendingRequisitionData()
        {

        }

        private string _Vessel_Code;
        private string _Vessel_Name;
        private string _Vessel_Short_Name;
        private string _Tech_Manager;
        private string _Managed_By_Comp;
        private string _FleetCode;
        private string _Current_User;

        private string _Vessel_email;
        private string _Authorized_Key;
        private string _Liecence_Key;



         
        public string VesselCode
        {
            get { return _Vessel_Code; }
            set { _Vessel_Code = value; }
        }

         
        public string VesselName
        {
            get { return _Vessel_Name; }
            set { _Vessel_Name = value; }
        }

         
        public string PendingRequisitionDatahortName
        {
            get { return _Vessel_Short_Name; }
            set { _Vessel_Short_Name = value; }
        }

         
        public string TechManager
        {
            get { return _Tech_Manager; }
            set { _Tech_Manager = value; }
        }

         
        public string ManagedByComp
        {
            get { return _Managed_By_Comp; }
            set { _Managed_By_Comp = value; }
        }

         

        public string FleetCode
        {
            get { return _FleetCode; }
            set { _FleetCode = value; }
        }

         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }

         
        public string VesselEmail
        {
            get { return _Vessel_email; }
            set { _Vessel_email = value; }
        }

         
        public string AuthorizedKey
        {
            get { return _Authorized_Key; }
            set { _Authorized_Key = value; }
        }
         

        public string LiecenceKey
        {
            get { return _Liecence_Key; }
            set { _Liecence_Key = value; }
        }
    }
}
