using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{
  

    public class ItemsData
    {
        public ItemsData()
        {

        }

        private string _ItemID;
        private string _Item_Intern_Ref;
        private string _system_code;
        private string _subSystem_code;
        private string _part_number;
        private string _short_description;
        private string _long_description;
        private string _Unit_and_Packings;
        private string _max_qty;
        private string _min_qty;
        private string _Item_Address;
        private string _drawing_number;
        private string _Link;
        private string _Current_User;


         
        public string ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

         
        public string ItemInternRef
        {
            get { return _Item_Intern_Ref; }
            set { _Item_Intern_Ref = value; }

        }
         
        public string System_code
        {
            get {return _system_code;}
            set {_system_code = value;}
        }

         
        public string SubSystem_Code
        {
            get { return _subSystem_code; }
            set { _subSystem_code = value;}
        }
         
        public string Part_Number
        {
            get {return _part_number;}
            set {_part_number = value;}
        }

         
        public string Short_Description
        {
            get{return _short_description;}
            set{_short_description = value;}
        }

         
        public string Long_Description
        {
            get{return _long_description;}
            set{_long_description = value;}
        }

         
        public string Unit_and_Packings
        {
            get { return _Unit_and_Packings; }
            set { _Unit_and_Packings = value; }

        }
         
        public string Max_Qty
        {
            get{return _max_qty;}
            set{_max_qty = value;}
        }

         
        public string Min_Qty
        {
            get
            {return _min_qty;}
            set{_min_qty = value;}
        }
         
        public string Drawing_Number
        {
            get { return _drawing_number; }
            set { _drawing_number = value; }
        }

         
        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }

         
        public string Item_Address
        {
            get { return _Item_Address; }
            set { _Item_Address = value; }
        }
         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }

    }
}
