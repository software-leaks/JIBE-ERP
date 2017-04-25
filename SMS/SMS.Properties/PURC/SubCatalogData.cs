using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{
  

    public class SubCatalogData
    {
        public SubCatalogData()
        {

        }

        private string _CatalogID;
        private string _functions;
        private string _maker;
        private string _model_type;
        private string _preferred_supplier;
        private string _set_installed;
        private string _subsystem_description;
        private string _subsystem_particulars;
        private string _system_code;
        private string _constring;
        private string _Current_User;

        private string _Link;
        private string _subCatalogID;




         
        public string CatalogID
        {
            get { return _CatalogID; }
            set { _CatalogID = value; }
        }
        
         
        public string SubCatalogId
        {
            get { return _subCatalogID; }
            set { _subCatalogID = value; }
        }

         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }
        
         
        public string Constring
        {
            get { return _constring; }
            set { _constring = value; }
        }

         
        public string Functions
        {
            get { return _functions; }
            set { _functions = value;}
        }

         
        public string Maker
        {
            get{return _maker;}
            set{_maker = value;}
        }
         
        public string Model_Type
        {
            get {return _model_type;}
            set {_model_type = value;}
        }
         
        public string Preferred_Supplier
        {
            get { return _preferred_supplier;}
            set { _preferred_supplier = value;}
        }

         
        public string Set_Installed
        {
            get { return _set_installed;}
            set {_set_installed = value;}
        }
         
        public string SubSystem_Description
        {
            get {return _subsystem_description;}
            set {_subsystem_description = value;}
        }
         
        public string Subsystem_Particulars
        {
            get {return _subsystem_particulars;}
            set {_subsystem_particulars = value;}
        }

         
        public string System_code
        {
            get {return _system_code; }
            set {_system_code = value;}
        }

         
        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }



    }

}
