using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{
    

    public class LocationData
    {
        public LocationData()
        {

        }

        private string _ParentType;
        private string _Code;
        private string _Short_Discription;
        private string _Long_Discription;
        private string _Short_Code;
        private string _Current_User;
        private string _NoOfLoc;
        private string _VesselType;
        private int _LocationID;

        public int LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
         
        public string ParentType
        {
            get { return _ParentType; }
            set { _ParentType = value; }
        }

         
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

         
        public string ShortDiscription
        {
            get { return _Short_Discription; }
            set { _Short_Discription = value; }
        }

         
        public string LongDiscription
        {
            get { return _Long_Discription; }
            set { _Long_Discription = value; }
        }

         
        public string ShortCode
        {
            get { return _Short_Code; }
            set { _Short_Code = value; }
        }

         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }

        public string NoOfLoc
        {
            get { return _NoOfLoc; }
            set { _NoOfLoc = value; }
        }
        public string VesselType
        {
            get { return _VesselType; }
            set { _VesselType = value; }
        }

        
    }
}
