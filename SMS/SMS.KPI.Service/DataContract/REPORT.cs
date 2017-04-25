using System.Runtime.Serialization;
namespace SMS.KPI.Service.DataContract
{
    [DataContract]
    public class REPORT
    {
        private string _ElementID = "";
        private string _ElementCode = "";
        private string _ElementDescription = "";
        private string _VersionNo = "";
        private string _LevelNo = "";
        private string _StageID = "";
        private string _StageCode = "";
        private string _StageDescription = "";
        private string _KpiTmsa = "";
        private string _BestPractices = "";
        private string _AuditedProcess = "";
        private string _Compliance = "";
        private string _Procedure = "";
        private string _Module = "";
        private string _KPI = "";
        private string _Notes = "";
        
        private string _LinkID = "";
        private string _DocPath = "";
        private string _ID = "";
        private string _ParentID = "";
        private string _LinkType = "";
        private string _Edit = "";
        private string _Value = "";
        private string _Role = "";

        private string _LinkExists = "";

        [DataMember]
        public string LinkExists
        {
            get { return _LinkExists; }
            set { _LinkExists = value; }
        }

        [DataMember]
        public string Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        [DataMember]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        [DataMember]
        public string Edit
        {
            get { return _Edit; }
            set { _Edit = value; }
        }
        [DataMember]
        public string DocPath
        {
            get { return _DocPath; }
            set { _DocPath = value; }
        }
        [DataMember]
        public string Procedure
        {
            get { return _Procedure; }
            set { _Procedure = value; }
        }
        [DataMember]
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        [DataMember]
        public string KPI
        {
            get { return _KPI; }
            set { _KPI = value; }
        }
        [DataMember]
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        [DataMember]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [DataMember]
        public string ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        [DataMember]
        public string ElementID
        {
            get { return _ElementID; }
            set { _ElementID = value; }
        }
        [DataMember]
        public string ElementCode
        {
            get { return _ElementCode; }
            set { _ElementCode = value; }
        }
        [DataMember]
        public string ElementDescription
        {
            get { return _ElementDescription; }
            set { _ElementDescription = value; }
        }
        [DataMember]
        public string VersionNo
        {
            get { return _VersionNo; }
            set { _VersionNo = value; }
        }
        [DataMember]
        public string LevelNo
        {
            get { return _LevelNo; }
            set { _LevelNo = value; }
        }
        [DataMember]
        public string StageID
        {
            get { return _StageID; }
            set { _StageID = value; }
        }
        [DataMember]
        public string StageCode
        {
            get { return _StageCode; }
            set { _StageCode = value; }
        }

        [DataMember]
        public string StageDescription
        {
            get { return _StageDescription; }
            set { _StageDescription = value; }
        }
        [DataMember]
        public string KpiTmsa
        {
            get { return _KpiTmsa; }
            set { _KpiTmsa = value; }
        }
        [DataMember]
        public string BestPractices
        {
            get { return _BestPractices; }
            set { _BestPractices = value; }
        }
        [DataMember]
        public string AuditedProcess
        {
            get { return _AuditedProcess; }
            set { _AuditedProcess = value; }
        }
        [DataMember]
        public string Compliance
        {
            get { return _Compliance; }
            set { _Compliance = value; }
        }

        [DataMember]
        public string LinkID
        {
            get { return _LinkID; }
            set { _LinkID = value; }
        }

        [DataMember]
        public string LinkType
        {
            get { return _LinkType; }
            set { _LinkType = value; }
        }

        


    }

    [DataContract]
    public class ELEMENTDATA
    {
        private string _elementCode = "";
        private string _elementId = "";

        [DataMember]
        public string ELEMENTCODE
        {
            get { return _elementCode; }
            set { _elementCode = value; }
        }
        [DataMember]
        public string ELEMENTID
        {
            get { return _elementId; }
            set { _elementId = value; }
        }

    }

    [DataContract]
    public class STAGEDATA
    {
        private string _StageCode = "";
        private string _StageId = "";
        private string _elementCode = "";


        [DataMember]
        public string STAGECODE
        {
            get { return _StageCode; }
            set { _StageCode = value; }
        }
        [DataMember]
        public string STAGEID
        {
            get { return _StageId; }
            set { _StageId = value; }
        }
        [DataMember]
        public string ELEMENTCODE
        {
            get { return _elementCode; }
            set { _elementCode = value; }
        }

    }

    [DataContract]
    public class LEVELDATA
    {
        private string _LevelNo = "";
        private string _LevelId = "";


        [DataMember]
        public string LEVELNO
        {
            get { return _LevelNo; }
            set { _LevelNo = value; }
        }

        [DataMember]
        public string LEVELID
        {
            get { return _LevelId; }
            set { _LevelId = value; }
        }

 
    }

    [DataContract]
    public class VERSIONDATA
    {
        private string _VersionNo = "";
        private string _VersionName = "";

        [DataMember]
        public string VERSIONNO
        {
            get { return _VersionNo; }
            set { _VersionNo = value; }
        }
        [DataMember]
        public string VERSIONNAME
        {
            get { return _VersionName; }
            set { _VersionName = value; }
        }

    }

    [DataContract]
    public class KPIURL
    {
        private string _ParentID = "";
        private string _LinkType = "";
        private int _Count = 0;

        [DataMember]
        public string ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [DataMember]
        public string LinkType
        {
            get { return _LinkType; }
            set { _LinkType = value; }
        }
        [DataMember]
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

    }
}
