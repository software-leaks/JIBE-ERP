using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{
    

    public class CatalogData
    {
        public CatalogData()
        {

        }
        private string _functions;
        private string _maker;
        private string _model_type;
        private string _preferred_supplier;
        private string _set_installed;
        private string _system_description;
        private string _system_particulars;
        private string _CatalogID;
        private string _System_Code;
        private string _Link;
        private string _Vessel_Code;
        private string _Current_User;
        private string _Update_Query;


         
       
        public string SystemCode
         { 
            get { return _System_Code;}
            set { _System_Code =value;}
         } 
          
         
        public string CatalogID
        {
            get { return _CatalogID ;}
            set { _CatalogID =value;}
        }

       
         
        public string Maker
        {
            get {return _maker; }
            set {_maker=value ; }
        }

         
        public string Model_Type
        {
            get { return _model_type; }
            set {_model_type=value;   }
        }

         
        public string Preferred_Supplier
        {
            get {return _preferred_supplier; }
            set {_preferred_supplier=value;  }
        }

         
        public string Set_Installed
        {
            get { return _set_installed; }
            set {_set_installed=value ;  }
        }

         
        public string System_Description
        {
            get {return _system_description; }
            set {_system_description=value ; }
        }

         
        public string System_Particulars
        {
            get
            {return _system_particulars; }
            set {_system_particulars=value; }
        }

         
        public string Functions
        {
            get
            {return _functions; }
            set
            {_functions=value;}
        }

         
        public string Link
        {
            get { return _Link;}
            set { _Link = value; }
        }
        
         
        public string VesselCode
        {
            get { return _Vessel_Code; }
            set { _Vessel_Code = value; }
        }

         

        public string updateQuery
        {
            get { return _Update_Query; }
            set { _Update_Query = value; }
        }

         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }
    }

}
