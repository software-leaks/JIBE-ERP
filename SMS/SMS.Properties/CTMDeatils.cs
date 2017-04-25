using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMS.Properties
{
    public class CTM_Deatils
    {

        public int CTM_ID
        {
            get;
            set;
        }

        public int Vessel_ID
        {
            get;
            set;
        }

        public int Office_ID
        {
            get;
            set;
        }

        public DateTime? CTM_Date
        {
            get;
            set;
        }

        public int CTM_Port
        {
            get;
            set;
        }

        public decimal BOW_Calculated_Amt
        {
            get;
            set;
        }

        public decimal CTM_Requested_Amt
        {
            get;
            set;
        }
        public decimal Cash_OnBoard
        {
            get;
            set;
        }
        public DataTable Denomination
        {
            get;
            set;
        }
        public DataTable OffSigners
        {
            get;
            set;
        }

        public string CTM_Remark
        {
            get;
            set;
        }
       

    }
}
