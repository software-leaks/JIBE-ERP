using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SMS.KPI.Service.DataContract
{
    /// <summary>
    /// Class to define datacontracts for Worklist reports 
    /// </summary>
    [DataContract]
   public  class WorkList
    {

            private string Year = "";
            private string Month = "";
            private string Value = "";
            private string _Type= "";
            private string vessel_Id = "";
            private string vessel_Name = "";
            private string _Start_date = "";
            private string _End_date = "";
            private string _SubType = "";


            private string _Years = "";
            private string _vessel_Ids = "";

            [DataMember]
            public string Years
            {
                get { return _Years; }
                set { _Years = value; }
            }
            [DataMember]
            public string Vessel_IDs
            {
                get { return _vessel_Ids; }
                set { _vessel_Ids = value; }
            }

            [DataMember]
            public string VALUE
            {
                get { return Value; }
                set { Value = value; }
            }

            [DataMember]
            public string Type
            {
                get { return _Type; }
                set { _Type = value; }
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
            public string YEAR
            {
                get { return Year; }
                set { Year = value; }
            }

            [DataMember]
            public string MONTH
            {
                get { return Month; }
                set { Month = value; }
            }
            [DataMember]
            public string SubType
            {
                get { return _SubType; }
                set { _SubType = value; }
            }

        }

}
