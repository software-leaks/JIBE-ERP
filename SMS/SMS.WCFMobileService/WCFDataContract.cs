using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace SMS.WCFMobileService
{
    [DataContract]
    public class VesselType
    {
        private string _vesselname = "";
        private int _vessel_id = 0;
        private string _notation = "";

        private int _companyId = 0;
        private int _status = 0;
        private int _vesselTypeId = 0;
        private int _FollowFlag = 0;

        [DataMember]
        public string Vessel_Name
        {
            get { return _vesselname; }
            set { _vesselname = value; }
        }

        [DataMember]
        public int Vessel_ID
        {
            get { return _vessel_id; }
            set { _vessel_id = value; }
        }

        [DataMember]
        public string Image_Path { get; set; }

        [DataMember]
        public string SVG_Path { get; set; }

        [DataMember]
        public string Notation
        {
            get { return _notation; }
            set { _notation = value; }
        }


        [DataMember]
        public int companyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        [DataMember]
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        public int vesselTypeId
        {
            get { return _vesselTypeId; }
            set { _vesselTypeId = value; }
        }
        [DataMember]
        public int FollowFlag
        {
            get { return _FollowFlag; }
            set { _FollowFlag = value; }
        }



        [DataMember]
        public List<ObjectGAType> ObjectGAList { get; set; }




    }

    public class VesselTypeResponse
    {
        [DataMember]
        public List<VesselType> VesselList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    public class UserListResponse
    {
        [DataMember]
        public List<UserList> UserList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }


    [DataContract]
    public class UserList
    {
        private string _inspector_name = "";
        private string _Role = "";
        private int _Inspector_Id = 0;
        private int _FollowFlag = 0;


        [DataMember]
        public string Inspector_name
        {
            get { return _inspector_name; }
            set { _inspector_name = value; }
        }

        [DataMember]
        public string Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        [DataMember]
        public int Inspector_Id
        {
            get { return _Inspector_Id; }
            set { _Inspector_Id = value; }
        }

        [DataMember]
        public int FollowFlag
        {
            get { return _FollowFlag; }
            set { _FollowFlag = value; }
        }

    }

    public class FollowUPResponse
    {
        [DataMember]
        public List<FollowupBetaType> FolowUPList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }


    //[DataContract]
    //public class FollowUP
    //{
    //    private string _Description = "";
    //    private string _FOLLOWUPType = "";
    //    private string _PostActivity_Id = "";
    //    private int _Inspector_Id = 0;
    //    private int _FolloUp_Id = 0;
    //    private int _PKID = 0;

    //    [DataMember]
    //    public int PKID
    //    {
    //        get { return _PKID; }
    //        set { _PKID = value; }
    //    }

    //    [DataMember]
    //    public string Description
    //    {
    //        get { return _Description; }
    //        set { _Description = value; }
    //    }

    //    [DataMember]
    //    public string FOLLOWUPType
    //    {
    //        get { return _FOLLOWUPType; }
    //        set { _FOLLOWUPType = value; }
    //    }

    //    [DataMember]
    //    public string PostActivity_Id
    //    {
    //        get { return _PostActivity_Id; }
    //        set { _PostActivity_Id = value; }
    //    }

    //    [DataMember]
    //    public int Inspector_Id
    //    {
    //        get { return _Inspector_Id; }
    //        set { _Inspector_Id = value; }
    //    }

    //    [DataMember]
    //    public int FolloUp_Id
    //    {
    //        get { return _FolloUp_Id; }
    //        set { _FolloUp_Id = value; }
    //    }

    //}


    public class GETFollowUPListResponse
    {
        [DataMember]
        public List<GETFollowUP> FollowUPList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    [DataContract]
    public class GETFollowUP
    {
        //ID, UserID, FollowID, FollowType, Active_Status

        private int _Id = 0;
        private int _UserID = 0;
        private string _FollowID = "";
        private string _FollowType = "";
        private int _Active_Status = 0;



        [DataMember]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }


        [DataMember]
        public string FollowID
        {
            get { return _FollowID; }
            set { _FollowID = value; }
        }


        [DataMember]
        public string FollowType
        {
            get { return _FollowType; }
            set { _FollowType = value; }
        }


        [DataMember]
        public int Active_Status
        {
            get { return _Active_Status; }
            set { _Active_Status = value; }
        }

    }


    [DataContract]
    public class ActivityListResponse
    {
        [DataMember]
        public List<ActivityType> ActivityList { get; set; }

        [DataMember]
        public List<ActivityObjectType> ActivityObjectList { get; set; }

        [DataMember]
        public List<FollowupType> FollowupList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    [DataContract]
    public class ActivityType
    {
        [DataMember]
        public string actionDate { get; set; }
        [DataMember]
        public String activityId { get; set; }
        [DataMember]
        public String childId { get; set; }
        [DataMember]
        public string dueDate { get; set; }
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String navigationPath { get; set; }
        [DataMember]
        public int objectId { get; set; }
        [DataMember]
        public int odeptId { get; set; }
        [DataMember]
        public int organizationId { get; set; }
        [DataMember]
        public String remark { get; set; }
        [DataMember]
        public int sdeptId { get; set; }
        [DataMember]
        public String status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public string inspectorName { get; set; }
        [DataMember]
        public int ncr { get; set; }
        [DataMember]
        public int InspectionId { get; set; }
        [DataMember]
        public int NodeId { get; set; }
        [DataMember]
        public int LocationId { get; set; }

        [DataMember]
        public int TreeLocationId { get; set; }

        [DataMember]
        public int FunctionId { get; set; }

        [DataMember]
        public int SubLocationId { get; set; }

        [DataMember]
        public int AttachedStatus { get; set; }

    }

    [DataContract]
    public class ActivityObjectType
    {
        [DataMember]
        public string activityId { get; set; }
        [DataMember]
        public int activityObjId { get; set; }
        [DataMember]
        public string imageaudioName { get; set; }
        [DataMember]
        public string imageaudioPath { get; set; }
        [DataMember]
        public int objectId { get; set; }
        [DataMember]
        public int userId { get; set; }
    }

    [DataContract]
    public class FollowupType
    {
        [DataMember]
        public string activityId { get; set; }
        [DataMember]
        public int followupId { get; set; }
        [DataMember]
        public string Followup { get; set; }
        [DataMember]
        public int objectId { get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public string date { get; set; }


    }


    [DataContract]
    public class ActivityListBetaResponse
    {
        [DataMember]
        public List<ActivityBetaType> ActivityList { get; set; }

        [DataMember]
        public List<ActivityObjectBetaType> ActivityObjectList { get; set; }

        [DataMember]
        public List<FollowupBetaType> FollowupList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    [DataContract]
    public class ActivityBetaType
    {
        [DataMember]
        public string actionDate { get; set; }
        [DataMember]
        public String activityId { get; set; }
        [DataMember]
        public String childId { get; set; }
        [DataMember]
        public string dueDate { get; set; }
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String navigationPath { get; set; }
        [DataMember]
        public int objectId { get; set; }
        [DataMember]
        public int odeptId { get; set; }
        [DataMember]
        public int organizationId { get; set; }
        [DataMember]
        public String remark { get; set; }
        [DataMember]
        public int sdeptId { get; set; }
        [DataMember]
        public String status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public string inspectorName { get; set; }
        [DataMember]
        public int ncr { get; set; }
        [DataMember]
        public int InspectionId { get; set; }
        [DataMember]
        public int NodeId { get; set; }
        [DataMember]
        public int LocationId { get; set; }
        [DataMember]
        public int StarFlag { get; set; }
        [DataMember]
        public int EditableFlag { get; set; }
        [DataMember]
        public int FollowFlag { get; set; }

    }

    [DataContract]
    public class ActivityObjectBetaType
    {
        [DataMember]
        public string activityId { get; set; }
        [DataMember]
        public int activityObjId { get; set; }
        [DataMember]
        public string imageaudioPath { get; set; }
        [DataMember]
        public int objectId { get; set; }
        [DataMember]
        public int userId { get; set; }
    }

    [DataContract]
    public class FollowupBetaType
    {
        [DataMember]
        public string activityId { get; set; }
        [DataMember]
        public int followupId { get; set; }
        [DataMember]
        public string Followup { get; set; }
        [DataMember]
        public string FollowupType { get; set; }
        [DataMember]
        public int objectId { get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public int editableFlag { get; set; }


    }





    [DataContract]
    public class UserType
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }
        [DataMember]
        public string Email
        {
            get;
            set;
        }
        [DataMember]
        public int Company_ID
        {
            get;
            set;
        }
        [DataMember]
        public string Company_Name
        {
            get;
            set;
        }
        [DataMember]
        public string User_Name
        {
            get;
            set;
        }
        [DataMember]
        public string Password
        {
            get;
            set;
        }
        [DataMember]
        public string Phone
        {
            get;
            set;
        }
        [DataMember]
        public int User_ID
        {
            get;
            set;
        }
        [DataMember]
        public string Role
        {
            get;
            set;
        }
        [DataMember]
        public int Status
        {
            get;
            set;
        }
        [DataMember]
        public List<DepartmentType> DepartmentList { get; set; }

        [DataMember]
        public List<JOBType> JobTypeList { get; set; }

        [DataMember]
        public string Authentication_Token
        {
            get;
            set;
        }
        [DataMember]
        public int Max_Activity { get; set; }
        [DataMember]
        public int Max_Audio { get; set; }
        [DataMember]
        public int Max_Image { get; set; }
        [DataMember]
        public int Activity_Count { get; set; }
        [DataMember]
        public int Audio_Count { get; set; }
        [DataMember]
        public int Image_Count { get; set; }
        [DataMember]
        public int Approved_User { get; set; }
        [DataMember]
        public int Staff_Code { get; set; }
        [DataMember]
        public string User_Rank { get; set; }





    }


    [DataContract]
    public class JOBType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Worklist_Type { get; set; }
        [DataMember]
        public string Worklist_Type_Display { get; set; }

    }

    [DataContract]
    public class DepartmentType
    {
        [DataMember]
        public int deptId { get; set; }
        [DataMember]
        public string deptName { get; set; }
        [DataMember]
        public string deptType { get; set; }

    }

    [DataContract]
    public class ObjectGAType
    {
        [DataMember]
        public int ObjectID { get; set; }
        [DataMember]
        public string ChildID { get; set; }
        [DataMember]
        public string Image_Path { get; set; }
        [DataMember]
        public string SVG_Path { get; set; }
        [DataMember]
        public string Parent_ID { get; set; }
        [DataMember]
        public string ChildName { get; set; }

    }

    [DataContract]
    public class JobStatusType
    {
        [DataMember]
        public string activityId { get; set; }

        [DataMember]
        public string objectId { get; set; }

        [DataMember]
        public string activityStatus { get; set; }


    }

    [DataContract]
    public class JobStatusTypeResponse
    {
        [DataMember]
        public List<JobStatusType> JobList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }

    }

    [DataContract]
    public class ResponseClass
    {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ResponseItem { get; set; }


    }

    [DataContract]
    public class Activity_Beta
    {
        [DataMember]
        public string Post_Tiltle { get; set; }

        [DataMember]
        public string PostActivity_Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int type { get; set; }

        [DataMember]
        public int inspector_Id { get; set; }

        [DataMember]
        public int Vesses_Id { get; set; }

        [DataMember]
        public int StarFlag { get; set; }

        [DataMember]
        public int EditableFlag { get; set; }


    }

    [DataContract]
    public class Activity_Beta_List
    {

        [DataMember]
        public List<Activity_Beta> PostActivtybeta { get; set; }

        //[DataMember]
        //public string Error { get; set; }

    }

    [DataContract]
    public class ActivityObject_Beta
    {
        [DataMember]
        public string PostActivity_Id { get; set; }

        [DataMember]
        public string PostActivityObject_Id { get; set; }

        [DataMember]
        public string imageaudioPath { get; set; }

        [DataMember]
        public int Folloup_Id { get; set; }

        [DataMember]
        public int inspector_Id { get; set; }

        [DataMember]
        public int Vesses_Id { get; set; }

        [DataMember]
        public string Authentication_Token { get; set; }


    }

    [DataContract]
    public class ActivityObject_Beta_List
    {

        [DataMember]
        public List<ActivityObject_Beta> PostActivtyObjectbeta { get; set; }

        //[DataMember]
        //public string Error { get; set; }


    }

    public class ChecklistResponse
    {
        [DataMember]
        public List<Checklist> CheckList { get; set; }

        [DataMember]
        public List<ChecklistItem> CheckListItems { get; set; }

        [DataMember]
        public List<Gradings> ObjectGradesList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }

    }

    [DataContract]
    public class Checklist
    {
        private int _Checklist_ID = 0;
        private int _Inspection_ID = 0;
        private string _SheduleDate = "";
        private string _CompletionDate = "";
        private string _OriginalSheduleDate = "";
        private string _InspectionStatus = "";
        private int _ChecklistItem_ID = 0;
        private string _CheckList_Name = "";
        private int _VesselID = 0;
        private string _NodeType = "";

        private string _CheckList_Type_name = "";
        private int _Vessel_Type = 0;
        private int _checklistType = 0;
        private int _Grading_Type = 0;
        private int _Checklist_IDCopy = 0;
        private int _Parent_ID = 0;
        private int _Location_ID = 0;
        private int _ItemGrading_Type = 0;
        private string _MobileStatus = "";

        private int _Index = 0;
        private int _inspActiveStatus = 0;

        private string _inspType = "";


        [DataMember]
        public int checklistid
        {
            get { return _Checklist_ID; }
            set { _Checklist_ID = value; }
        }

        [DataMember]
        public int inspectionid
        {
            get { return _Inspection_ID; }
            set { _Inspection_ID = value; }
        }

        [DataMember]
        public string scheduledate
        {
            get { return _SheduleDate; }
            set { _SheduleDate = value; }
        }

        [DataMember]
        public string completiondate
        {
            get { return _CompletionDate; }
            set { _CompletionDate = value; }
        }

        [DataMember]
        public string originalscheduledate
        {
            get { return _OriginalSheduleDate; }
            set { _OriginalSheduleDate = value; }
        }

        [DataMember]
        public string inspectionstatus
        {
            get { return _InspectionStatus; }
            set { _InspectionStatus = value; }
        }

        [DataMember]
        public string checklistname
        {
            get { return _CheckList_Name; }
            set { _CheckList_Name = value; }
        }
        [DataMember]
        public string vesseltypename
        {
            get { return _NodeType; }
            set { _NodeType = value; }
        }

        [DataMember]
        public int vesselid
        {
            get { return _VesselID; }
            set { _VesselID = value; }
        }
        //[DataMember]
        //public string vesseltypename
        //{
        //    get { return _NodeType; }
        //    set { _NodeType = value; }
        //}

        [DataMember]
        public string checklisttypename
        {
            get { return _CheckList_Type_name; }
            set { _CheckList_Type_name = value; }
        }
        [DataMember]
        public int vesseltype
        {
            get { return _Vessel_Type; }
            set { _Vessel_Type = value; }
        }
        [DataMember]
        public int checklisttype
        {
            get { return _checklistType; }
            set { _checklistType = value; }
        }
        [DataMember]
        public int gradingtype
        {
            get { return _Grading_Type; }
            set { _Grading_Type = value; }
        }

        [DataMember]
        public string submitstatus
        {
            get { return _MobileStatus; }
            set { _MobileStatus = value; }
        }

        [DataMember]
        public int inspActiveStatus
        {
            get { return _inspActiveStatus; }
            set { _inspActiveStatus = value; }
        }

        [DataMember]
        public int index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        [DataMember]
        public string InspectionType
        {
            get { return _inspType; }
            set { _inspType = value; }
        }

        //[DataMember]
        //public List<ChecklistGroup> CheckListParentGroup { get; set; }

        [DataMember]
        public List<ChecklistGroup> Group { get; set; }

        //[DataMember]
        //public List<ChecklistLocation> CheckListParentLocation { get; set; }

        [DataMember]
        public List<ChecklistLocation> Location { get; set; }

    }

    [DataContract]
    public class ChecklistGroup
    {
        private int _Checklist_ID = 0;
        private int _Inspection_ID = 0;
        private int _ChecklistItem_ID = 0;
        private string _CheckList_Name = "";
        private string _NodeType = "";
        private int _Parent_ID = 0;
        private int _Index = 0;



        [DataMember]
        public int checklistid
        {
            get { return _Checklist_ID; }
            set { _Checklist_ID = value; }
        }
        [DataMember]
        public int inspectionid
        {
            get { return _Inspection_ID; }
            set { _Inspection_ID = value; }
        }
        [DataMember]
        public int groupid
        {
            get { return _ChecklistItem_ID; }
            set { _ChecklistItem_ID = value; }
        }
        [DataMember]
        public string groupname
        {
            get { return _CheckList_Name; }
            set { _CheckList_Name = value; }
        }
        [DataMember]
        public string nodetype
        {
            get { return _NodeType; }
            set { _NodeType = value; }
        }

        [DataMember]
        public int parentid
        {
            get { return _Parent_ID; }
            set { _Parent_ID = value; }
        }

        [DataMember]
        public int index
        {
            get { return _Index; }
            set { _Index = value; }
        }


        //[DataMember]
        //public List<ChecklistGroup> CheckListsubGroup { get; set; }

        [DataMember]
        public List<ChecklistGroup> Group { get; set; }

        //[DataMember]
        //public List<ChecklistLocation> CheckListsubLocation { get; set; }

        [DataMember]
        public List<ChecklistLocation> Location { get; set; }
    }

    /// <summary>
    /// Checklist location properties are grouped in 'ChecklistLocation' class    
    /// </summary>
    [DataContract]
    public class ChecklistLocation
    {
        private int _Checklist_ID = 0;
        private int _Inspection_ID = 0;
        private int _ChecklistItem_ID = 0;
        private string _CheckList_Name = "";
        private string _NodeType = "";
        private string _CheckList_Type_name = "";
        private decimal? _Nodevalue = 0;
        private int _Vessel_Type = 0;
        private int _checklistType = 0;
        private int _Grading_Type = 0;
        private int _Checklist_IDCopy = 0;
        private int _Parent_ID = 0;
        private int _Location_ID = 0;
        private int _ItemGrading_Type = 0;
        private string _ComponentName = "";
        private int _Index = 0;


        //checklistid will contain id of checklist
        [DataMember]
        public int checklistid
        {
            get { return _Checklist_ID; }
            set { _Checklist_ID = value; }
        }
        //inspectionid this will contain inspection id 
        [DataMember]
        public int inspectionid
        {
            get { return _Inspection_ID; }
            set { _Inspection_ID = value; }
        }
        //componentid this will contain componant id  ie : Actual Location ID attached to checklist
        [DataMember]
        public int componentid
        {
            get { return _ChecklistItem_ID; }
            set { _ChecklistItem_ID = value; }
        }
        //locationname this will contain name of location in checklist
        [DataMember]
        public string locationname
        {
            get { return _CheckList_Name; }
            set { _CheckList_Name = value; }
        }
        //componentname this wil contain actual name of the location/componant attached to checklist
        [DataMember]
        public string componentname
        {
            get { return _ComponentName; }
            set { _ComponentName = value; }
        }
        //This is edited by Pranav Sakpal on 06-06-2016 for JIT : 9890
        //nodevalue this will contain ratings assigned to the location , sometimes this can be null 
        [DataMember]
        public decimal? nodevalue
        {
            get { return _Nodevalue; }
            set { _Nodevalue = value; }
        }

        //nodetype this will contain type of node checklist have ie: LOCATION
        [DataMember]
        public string nodetype
        {
            get { return _NodeType; }
            set { _NodeType = value; }
        }
        //parentid this will contain parent id for location in checklist this will be a groupid which contains current location
        [DataMember]
        public int parentid
        {
            get { return _Parent_ID; }
            set { _Parent_ID = value; }
        }
        //locationid this will contain id for the location
        [DataMember]
        public int locationid
        {
            get { return _Location_ID; }
            set { _Location_ID = value; }
        }
        //index this will contain index number for location 
        //eg : group will have location1 and location2 then this will have its index no as 1 and 2
        [DataMember]
        public int index
        {
            get { return _Index; }
            set { _Index = value; }
        }

    }

    [DataContract]
    public class ChecklistItem
    {
        private int _Checklist_ID = 0;
        private int _Inspection_ID = 0;
        private int _ChecklistItem_ID = 0;
        private string _CheckList_Name = "";
        private string _NodeType = "";
        private decimal _NodeValue = 0;
        private string _CheckList_Type_name = "";
        private int _Vessel_Type = 0;
        private int _checklistType = 0;
        private int _Grading_Type = 0;
        private int _Checklist_IDCopy = 0;
        private int _Parent_ID = 0;
        private int _Location_ID = 0;
        private int _ItemGrading_Type = 0;
        private int _Index = 0;

        [DataMember]
        public int checklistid
        {
            get { return _Checklist_ID; }
            set { _Checklist_ID = value; }
        }
        [DataMember]
        public int inspectionid
        {
            get { return _Inspection_ID; }
            set { _Inspection_ID = value; }
        }
        [DataMember]
        public int checklistitemid
        {
            get { return _ChecklistItem_ID; }
            set { _ChecklistItem_ID = value; }
        }
        [DataMember]
        public string checklistitemname
        {
            get { return _CheckList_Name; }
            set { _CheckList_Name = value; }
        }
        [DataMember]
        public string nodetype
        {
            get { return _NodeType; }
            set { _NodeType = value; }
        }

        [DataMember]
        public decimal nodevalue
        {
            get { return _NodeValue; }
            set { _NodeValue = value; }
        }

        [DataMember]
        public int locationid
        {
            get { return _Parent_ID; }
            set { _Parent_ID = value; }
        }
        [DataMember]
        public int gradeid
        {
            get { return _Location_ID; }
            set { _Location_ID = value; }
        }

        [DataMember]
        public int index
        {
            get { return _Index; }
            set { _Index = value; }
        }

    }


    [DataContract]
    public class Gradings
    {
        private int _ID = 0;
        private int _optionID = 0;
        private string _GradeName = "";
        private string _OptionText = "";
        private decimal _OptionValue = 0;

        //CLG.OptionText,CLG.OptionValue

        [DataMember]
        public int gradeid
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [DataMember]
        public string gradenames
        {
            get { return _GradeName; }
            set { _GradeName = value; }
        }
        [DataMember]
        public string optiontext
        {
            get { return _OptionText; }
            set { _OptionText = value; }
        }
        [DataMember]
        public decimal optionvalue
        {
            get { return _OptionValue; }
            set { _OptionValue = value; }
        }
    }


    public class VesselCategoryTypeResponse
    {
        [DataMember]
        public List<VesselTypeList> VesselTypeList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    public class VesselTypeList
    {
        [DataMember]
        public int Vessel_TypeID { get; set; }

        [DataMember]
        public string Vessel_TypeName { get; set; }

        [DataMember]
        public List<ObjectGAType> GAList { get; set; }

        [DataMember]
        public List<VesselCategoryType> VesselList { get; set; }


    }


    [DataContract]
    public class VesselCategoryType
    {
        private string _vesselname = "";
        private int _vessel_id = 0;

        [DataMember]
        public string Vessel_Name
        {
            get { return _vesselname; }
            set { _vesselname = value; }
        }

        [DataMember]
        public int Vessel_ID
        {
            get { return _vessel_id; }
            set { _vessel_id = value; }
        }

        //[DataMember]
        //public int Company_ID
        //{
        //    get;
        //    set;
        //}

        [DataMember]
        public int Active_Status
        {
            get;
            set;
        }

        [DataMember]
        public int Attachment_Size
        {
            get;
            set;
        }

        [DataMember]
        public int Data_Size
        {
            get;
            set;
        }


    }


    [DataContract]
    public class CompanyType
    {

        [DataMember]
        public int Company_ID { get; set; }

        [DataMember]
        public string Company_Name { get; set; }


    }

    public class CompanyTypeResponse
    {
        [DataMember]
        public List<CompanyType> CompanylList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }


    public class FunctionalTreeResponse
    {
        [DataMember]
        public List<function> Functions { get; set; }

        [DataMember]
        public List<FunctionalSystem> Systems { get; set; }

        [DataMember]
        public List<SubSystem> Subsystems { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    public class function
    {
        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public int Active_Status { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int VesselID { get; set; }


    }


    public class FunctionalSystem
    {
        [DataMember]
        public int SystemCode { get; set; }

        [DataMember]
        public int Functions { get; set; }

        [DataMember]
        public int Active_Status { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int LocationCode { get; set; }

        [DataMember]
        public int VesselID { get; set; }

    }

    public class SubSystem
    {
        [DataMember]
        public int SubsystemCode { get; set; }

        [DataMember]
        public int SystemCode { get; set; }

        [DataMember]
        public int Active_Status { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int LocationCode { get; set; }

        [DataMember]
        public int VesselID { get; set; }

    }


    #region CrewUserList

    public class CrewUserListResponse
    {
        [DataMember]
        public List<CrewUserListType> CrewUserList { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }

    [DataContract]
    public class CrewUserListType
    {
        [DataMember]
        public string Rank { get; set; }

        [DataMember]
        public int? StaffID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string ProfilePicPath { get; set; }

        [DataMember]
        public bool Activestatus { get; set; }

        [DataMember]
        public string Nationality { get; set; }

        [DataMember]
        public string Dob_Crew_User { get; set; }

        [DataMember]
        public string Company_Seniority_Years { get; set; }

        [DataMember]
        public int? Company_Contract { get; set; }

        [DataMember]
        public string Rank_Seniority_Years { get; set; }

        [DataMember]
        public int? Rank_Contract { get; set; }

        [DataMember]
        public string Approved_Cards { get; set; } // It will refer Card Type in stored procedure

        [DataMember]
        public string Approver_Remarks
        { get; set; }


        [DataMember]
        public string Cards_Status { get; set; }

        [DataMember]
        public string Cards_Remark { get; set; }


        [DataMember]
        public int? Average_Evaluation { get; set; }

        [DataMember]
        public int? Last_Evalation { get; set; }

        [DataMember]
        public int? Rank_Sort_Order { get; set; }

        [DataMember]
        public int? Staff_Code { get; set; }






    }
    #endregion

    #region Upload Feedback Text
    [DataContract]
    public class Upload_FeedBack_ResponseClass
    {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string FeedbackId { get; set; }


    }
    #endregion

    #region Upload Audio file
    [DataContract]
    public class AudioUploadResponseClass
    {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ResponseItem { get; set; }

    }
    #endregion

    #region Download All Vessel Data
    [DataContract]
    public class All_Vessel_Data_Response
    {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Sync_Time { get; set; }
    }
    #endregion


    public class Configurable_Alerts_Result
    {
        [DataMember]
        public List<Configurable_Alerts> alerts_List { get; set; }

    }

    public class Configurable_Alerts
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public int userid { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string buttontxt1 { get; set; }

        [DataMember]
        public string buttontxt2 { get; set; }       

        [DataMember]
        public int prompt_type { get; set; }


    }


    public class Updated_Configurable_Alerts_rec
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string status { get; set; }

        

    }

    public class Updated_Configurable_Alerts_Action
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string status { get; set; }



    }

    public class VesselIDs_Synctimes
    {
        [DataMember]
        public int vesselid { get; set; }

        [DataMember]
        public string synctime { get; set; }



    }


    public class VesselIDs_Synctimes_Input
    {
        
        [DataMember]
        public string Sync_Time { get; set; }

        [DataMember]
        public string Authentication_Token { get; set; }

        [DataMember]
        public string AC_SYNC_TIME { get; set; }

        [DataMember]
        public string FN_SYNC_TIME { get; set; }

        [DataMember]
        public string CRW_SYN_TIME { get; set; }

        [DataMember]
        public string CHKLIST_SYN_TIME { get; set; }

        [DataMember]
        public List<VesselIDs_Synctimes> VSList { get; set; }



    }


    public class DownloadZip_Result
    {
       
        [DataMember]
        public string filename { get; set; }

        [DataMember]
        public string datasize { get; set; }

        [DataMember]
        public string vesselid { get; set; }
        
        [DataMember]
        public string error { get; set; }


     
    }

}


