using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{

    public class AssignVesselData
    {
        public AssignVesselData()
        {

        }

        private string _systemcode;
        private string _Location_code;
        private string _Vessel_code;
        private string _Location_Comments;
        private string _Current_User;


         
        public string Vessel_code
        {
            get { return _Vessel_code; }
            set { _Vessel_code = value; }
        }

         
        public string LocationCode
        {
            get { return _Location_code; }
            set { _Location_code = value; }
        }

         
        public string Systemcode
        {
            get { return _systemcode; }
            set { _systemcode = value; }
        }

         
        public string LocationComments
        {
            get { return _Location_Comments; }
            set { _Location_Comments = value; }
        }


         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }

      
    }
}
