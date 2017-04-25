using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties.PURC
{
    public class POConfig
    {
        public POConfig()
        {
        }
        private string _ID;
        private string _POType;
        private string _Owner;
        private string _Delivery_Port;
        private string _Delivery_Port_date;
        private string _Vessel_movement_Date;
        private string _Item_Category;
        private string _Quote_required;
        private string _QuoteNo;
        private string _Vessel_Processing_PO;
        private string _Enable_Free_text;
        private string _copy_to_vessel;
        private string _Sup_Po_Confirmation;
        private string _Vessel_Delivery_Confirm;
        private string _Office_Delivery_Confirmation;
        private string _Withhold_tax;
        private string _Vat_Config_Purc;
        private string _require_verify;
        private string _Auto_POClosing;
        private string _Currentuser;



        public string Currentuser
        {
            get { return _Currentuser; }
            set { _Currentuser = value; }
        }
        public string Office_Delivery_Confirmation
        {
            get { return _Office_Delivery_Confirmation; }
            set { _Office_Delivery_Confirmation = value; }
        }
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string POType
        {
            get { return _POType; }
            set { _POType = value; }
        }
        public string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        public string Delivery_Port
        {
            get { return _Delivery_Port; }
            set { _Delivery_Port = value; }
        }

        public string Delivery_Port_date
        {
            get { return _Delivery_Port_date; }
            set { _Delivery_Port_date = value; }
        }

        public string Vessel_movement_Date
        {
            get { return _Vessel_movement_Date; }
            set { _Vessel_movement_Date = value; }
        }

        public string Item_Category
        {
            get { return _Item_Category; }
            set { _Item_Category = value; }
        }

        public string Quote_required
        {
            get { return _Quote_required; }
            set { _Quote_required = value; }
        }
        public string QuoteNo
        {
            get { return _QuoteNo; }
            set { _QuoteNo = value; }
        }

        public string Vessel_Processing_PO
        {
            get { return _Vessel_Processing_PO; }
            set { _Vessel_Processing_PO = value; }
        }

        public string Enable_Free_text
        {
            get { return _Enable_Free_text; }
            set { _Enable_Free_text = value; }
        }

        public string copy_to_vessel
        {
            get { return _copy_to_vessel; }
            set { _copy_to_vessel = value; }
        }

        public string Sup_Po_Confirmation
        {
            get { return _Sup_Po_Confirmation; }
            set { _Sup_Po_Confirmation = value; }
        }

        public string Vessel_Delivery_Confirm
        {
            get { return _Vessel_Delivery_Confirm; }
            set { _Vessel_Delivery_Confirm = value; }
        }

        public string Withhold_tax
        {
            get { return _Withhold_tax; }
            set { _Withhold_tax = value; }
        }

        public string Vat_Config_Purc
        {
            get { return _Vat_Config_Purc; }
            set { _Vat_Config_Purc = value; }
        }

        public string require_verify
        {
            get { return _require_verify; }
            set { _require_verify = value; }
        }

        public string Auto_POClosing
        {
            get { return _Auto_POClosing; }
            set { _Auto_POClosing = value; }
        }




    }
}
