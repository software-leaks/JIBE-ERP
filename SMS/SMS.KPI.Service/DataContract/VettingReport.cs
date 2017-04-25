using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SMS.KPI.Service.DataContract
{
    /// <summary>
    /// Class to define datacontracts for Vetting reports 
    /// </summary>
    [DataContract]
    public class VettingReport
    {
        private string _Vessel_Name;
        private string _Year = "";
        private string _vessel_Id = "";
        private string _Start_date = "";
        private string _End_date = "";
        private string _Fleet_Name = "";
        private string _Rec_Count = "";
        private string _CategoryID = "";
        private string _VettingTypeID = "";
        private string _ObvTypeID = "";
        private string _FleetID = "";
        private string _Category_Name = "";
        private string _Risk_Level = "";
        private string _Risk_ID = "";
        private string _Oil_ID = "";
        private string _Oil_MajorName = "";

        [DataMember]
        public string Vessel_Name
        {
            get { return _Vessel_Name; }
            set { _Vessel_Name = value; }
        }
        
         [DataMember]
        public string Rec_Count
        {
            get { return _Rec_Count;}
            set { _Rec_Count = value; }
        }

         [DataMember]
         public string Vessel_IDs
         {
             get { return _vessel_Id; }
             set { _vessel_Id = value; }
         }
         [DataMember]
         public string Start_date
         {
             get { return _Start_date; }
             set { _Start_date = value; }
         }
         [DataMember]
         public string End_date
         {
             get { return _End_date; }
             set { _End_date = value; }
         }
         [DataMember]
         public string Years
         {
             get { return _Year; }
             set { _Year = value; }
         }
         [DataMember]
         public string Fleet_Name
         {
             get { return _Fleet_Name; }
             set { _Fleet_Name = value; }
         }

         [DataMember]
         public string CategoryID
         {
             get { return _CategoryID; }
             set { _CategoryID = value; }
         }

         [DataMember]
         public string VettingTypeID
         {
             get { return _VettingTypeID; }
             set { _VettingTypeID = value; }
         }

        [DataMember]
        public string ObvTypeID
        {
            get{return _ObvTypeID;}
            set{_ObvTypeID = value;}
        }

        [DataMember]
        public string FleetID
        {
            get { return _FleetID; }
            set { _FleetID = value; }
        }

        [DataMember]
        public string CategoryName
        {
            get { return _Category_Name; }
            set { _Category_Name = value; }
        }  
   
         [DataMember]
        public string Risk_Level
        {
            get { return _Risk_Level; }
            set { _Risk_Level = value; }
        }

        [DataMember]
        public string Risk_ID
        {
            get { return _Risk_ID; }
            set { _Risk_ID = value; }
        }

        [DataMember]
        public string Oil_ID
        {
            get { return _Oil_ID; }
            set { _Oil_ID = value; }
        }

        [DataMember]
        public string Oil_majorName
        {
            get { return _Oil_MajorName; }
            set { _Oil_MajorName = value; }
        }

    }

    [DataContract]
    public class VettingType
    {
        private string _Vetting_Type_ID = "";
        private string _Vetting_Type_Name="";

        [DataMember]
        public string Vetting_Type_ID
        {
            get { return _Vetting_Type_ID; }
            set { _Vetting_Type_ID = value; }
        }

        [DataMember]
        public string Vetting_Type_Name
        {
            get { return _Vetting_Type_Name; }
            set { _Vetting_Type_Name = value; }
        }
    }

    [DataContract]
    public class Category
    {
        private string _Category_ID = "";
        private string _Category_Name = "";

        [DataMember]
        public string Category_ID
        {
            get { return _Category_ID; }
            set { _Category_ID = value; }
        }

        [DataMember]
        public string Category_Name
        {
            get { return _Category_Name; }
            set { _Category_Name = value; }
        }
    }


    [DataContract]
    public class ObservationType
    {
        private string _ObservationType_ID = "";
        private string _ObservationType_Name = "";

        [DataMember]
        public string ObservationType_ID
        {
            get { return _ObservationType_ID; }
            set { _ObservationType_ID = value; }
        }

        [DataMember]
        public string ObservationType_Name
        {
            get { return _ObservationType_Name; }
            set { _ObservationType_Name = value; }
        }
    }

    [DataContract]
    public class Fleet
    {
        private string _Fleet_ID = "";
        private string _Flt_Name = "";

        [DataMember]
        public string Fleet_ID
        {
            get { return _Fleet_ID; }
            set { _Fleet_ID = value; }
        }

        [DataMember]
        public string Flt_Name
        {
            get { return _Flt_Name; }
            set { _Flt_Name = value; }
        }
    }

    [DataContract]
    public class OilMajors
    {
        private string _Oil_Major_Name = "";
        private string _Oil_ID = "";

        [DataMember]
        public string OilMajorName
        {
            get { return _Oil_Major_Name; }
            set { _Oil_Major_Name = value; }
        }

        [DataMember]
        public string OilID
        {
            get { return _Oil_ID; }
            set { _Oil_ID = value; }
        }

    }

     
    }


