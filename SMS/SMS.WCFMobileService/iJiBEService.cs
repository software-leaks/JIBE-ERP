using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO;
using System.Text;
using System.ServiceModel.Web;

namespace SMS.WCFMobileService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface iJiBEService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_UserDetails/user_name/{UserName}/user_password/{Password}/DeviceID/{DeviceID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<UserType> Get_UserDetails(string UserName, string Password, string DeviceID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Upd_APNSToken/Sync_Time/{Sync_Time}/User_ID/{User_ID}/Device_ID/{Device_ID}/Authentication_Token/{Authentication_Token}/APNS_Token/{APNS_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Upd_APNSToken(string Sync_Time, string User_ID, string Device_ID, string Authentication_Token, string APNS_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_Vessels/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        VesselTypeResponse Get_Vessels(string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_All_Vessels/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        VesselTypeResponse Get_ALL_Vessels(string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_User_List/userID/{userID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserListResponse Get_User_List(string userID, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UPD_FollowUP/PKID/{PKID}/followupID/{followupID}/ActivityID/{ActivityID}/ObjectID/{ObjectID}/Followup/{Followup}/FOLLOWUPType/{FOLLOWUPType}/CreatedBy/{CreatedBy}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass UPD_FollowUP(string PKID, string followupID, string ActivityID, string ObjectID, string Followup, string FOLLOWUPType, string CreatedBy, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GET_ALL_FollowUP/VesselID/{VesselID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        FollowUPResponse GET_ALL_FollowUP(string VesselID, string Sync_Time, string Authentication_Token);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_FollowUPSettings/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        GETFollowUPListResponse Get_FollowUPSettings(string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UPD_POST_FollowUPSetting/ID/{ID}/UserID/{UserID}/FollowID/{FollowID}/FollowType/{FollowType}/CreatedBy/{CreatedBy}/Action/{Action}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass UPD_POST_FollowUPSetting(string ID, string UserID, string FollowID, string FollowType, string CreatedBy, string Action, string Sync_Time, string Authentication_Token);


        [OperationContract]
        [WebInvoke(UriTemplate = "*", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        ResponseClass UploadFileStream(Stream FileByteStream);

        [OperationContract]
        //[WebInvoke(Method = "GET", UriTemplate = "/UploadActivity/ActionDate/{ActionDate}/ActivityId/{ActivityId}/ChildId/{ChildId}/UserId/{UserId}/ObjectId/{ObjectId}/SDeptId/{SDeptId}/ODeptId/{ODeptId}/DueDate/{DueDate}/Name/{Name}/NavigationPath/{NavigationPath}/OrganizationId/{OrganizationId}/Remark/{Remark}/Status/{Status}/Type/{Type}/NCR/{NCR}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [WebInvoke(Method = "GET", UriTemplate = "/UploadActivity/ActionDate/{ActionDate}/ActivityId/{ActivityId}/ChildId/{ChildId}/UserId/{UserId}/ObjectId/{ObjectId}/SDeptId/{SDeptId}/ODeptId/{ODeptId}/DueDate/{DueDate}/Name/{Name}/NavigationPath/{NavigationPath}/OrganizationId/{OrganizationId}/Remark/{Remark}/Status/{Status}/Type/{Type}/NCR/{NCR}/functionID/{functionID}/systemLocationID/{systemLocationID}/subsystemLocationID/{subsysLocationID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_Activity(string ActionDate, string ActivityId, string CHILDID, string userId, string objectId, string sdeptId, string odeptId, string DUEDATE, string NAME, string NavigationPath, string ORGANIZATIONID, string REMARK, string STATUS, string TYPE, string NCR, string functionID, string systemLocationID, string subsysLocationID, string Authentication_Token);
        //ResponseClass Upd_Activity(string ActionDate, string ActivityId, string CHILDID, string userId, string objectId, string sdeptId, string odeptId, string DUEDATE, string NAME, string NavigationPath, string ORGANIZATIONID, string REMARK, string STATUS, string TYPE, string NCR, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UploadPostActivity/ActionDate/{ActionDate}/ActivityId/{ActivityId}/UserId/{UserId}/ObjectId/{ObjectId}/Name/{Name}/Remark/{Remark}/Status/{Status}/Type/{Type}/starflag/{starflag}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_Post_Activity(string ActionDate, string ActivityId, string userId, string objectId, string NAME, string REMARK, string STATUS, string TYPE, string starflag, string Authentication_Token);


        [OperationContract]
        //[WebInvoke(Method = "GET", UriTemplate = "/Upd_Activity_Checklist/ActionDate/{ActionDate}/ActivityId/{ActivityId}/ChildId/{ChildId}/UserId/{UserId}/ObjectId/{ObjectId}/SDeptId/{SDeptId}/ODeptId/{ODeptId}/DueDate/{DueDate}/Name/{Name}/NavigationPath/{NavigationPath}/OrganizationId/{OrganizationId}/Remark/{Remark}/Status/{Status}/Type/{Type}/NCR/{NCR}/Inspection_ID/{Inspection_ID}/Node_ID/{Node_ID}/Location_ID/{Location_ID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [WebInvoke(Method = "GET", UriTemplate = "/Upd_Activity_Checklist/ActionDate/{ActionDate}/ActivityId/{ActivityId}/ChildId/{ChildId}/UserId/{UserId}/ObjectId/{ObjectId}/SDeptId/{SDeptId}/ODeptId/{ODeptId}/DueDate/{DueDate}/Name/{Name}/NavigationPath/{NavigationPath}/OrganizationId/{OrganizationId}/Remark/{Remark}/Status/{Status}/Type/{Type}/NCR/{NCR}/Inspection_ID/{Inspection_ID}/Node_ID/{Node_ID}/Location_ID/{Location_ID}/functionID/{functionID}/systemLocationID/{systemLocationID}/subsystemLocationID/{subsysLocationID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_Activity_Checklist(string ActionDate, string ActivityId, string CHILDID, string userId, string objectId, string sdeptId, string odeptId, string DUEDATE, string NAME, string NavigationPath, string ORGANIZATIONID, string REMARK, string STATUS, string TYPE, string NCR, string Inspection_ID, string Node_ID, string Location_ID, string functionID, string systemLocationID, string subsysLocationID, string Authentication_Token);
        //ResponseClass Upd_Activity_Checklist(string ActionDate, string ActivityId, string CHILDID, string userId, string objectId, string sdeptId, string odeptId, string DUEDATE, string NAME, string NavigationPath, string ORGANIZATIONID, string REMARK, string STATUS, string TYPE, string NCR, string Inspection_ID, string Node_ID, string Location_ID, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UploadActivityObject/ActivityId/{ActivityId}/ActivityObjId/{ActivityObjId}/ObjectId/{ObjectId}/UserId/{UserId}/ImageAudioPath/{ImageAudioPath}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_ActivityObject(string ActivityId, string ActivityObjId, string ObjectId, string UserId, string ImageAudioPath, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UploadPostActivityObject/ActivityId/{ActivityId}/ActivityObjId/{ActivityObjId}/ObjectId/{ObjectId}/UserId/{UserId}/ImageAudioPath/{ImageAudioPath}/followUpID/{followUpID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_Post_ActivityObject(string ActivityId, string ActivityObjId, string ObjectId, string UserId, string ImageAudioPath, string followUpID, string Authentication_Token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DownloadFile/FileName/{FileName}", Method = "GET", RequestFormat = WebMessageFormat.Json)]
        Stream DownloadFile(string FileName);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetActivityList/ObjectID/{ObjectID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ActivityListResponse GetActivityList(string ObjectID, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_ALL_ActivityList/UserID/{UserID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ActivityListBetaResponse Get_ALL_ActivityList(string UserID, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetActivityStatus/ObjectID/{ObjectID}/ActivityID/{ActivityID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        JobStatusTypeResponse Get_Job_Status(string ObjectID, string ActivityID, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Upd_Create_User/User_Name/{User_Name}/Password/{Password}/Email_Address/{Email_Address}/Mobile_Number/{Mobile_Number}/Company_Name/{Company_Name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_Create_User(string User_Name, string Password, string Email_Address, string Mobile_Number, string Company_Name);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/VerifyAccount/Code/{Code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string VerifyAccount(string Code);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Upd_Contact_Us/UserID/{UserID}/Message/{Message}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_Contact_Us(string UserID, string Message, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        ResponseClass UploadFileStreamZip(Stream FileByteStream);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DownloadFileZip/FileName/{FileName}", Method = "GET", RequestFormat = WebMessageFormat.Json)]
        Stream DownloadFileZip(string FileName);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UpdateActivityStatus/ActivityId/{ActivityId}/Date/{Date}/UserId/{UserId}/Status/{Status}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_ActivityStatus(string ActivityId, string Date, string UserId, string Status, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_Vessels_By_Type/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        VesselCategoryTypeResponse Get_Vessels_By_Type(string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_Checklist/VesselID/{VesselID}/UserID/{UserID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ChecklistResponse Get_Checklist(string VesselID, string UserID, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Submit_Status/Checklist_ID/{Checklist_ID}/Inspection_ID/{Inspection_ID}/Mobile_Status/{Mobile_Status}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Submit_Status(string Checklist_ID, string Inspection_ID, string Mobile_Status, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Upd_NodeValue/InspectionID/{InspectionID}/NodeID/{NodeID}/Value/{Value}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResponseClass Upd_NodeValue(string InspectionID, string NodeID, string Value, string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_FunctionalTree/Sync_Time/{Sync_Time}/VesselID/{VesselID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        FunctionalTreeResponse Get_FunctionalTree(string Sync_Time, string VesselID, string Authentication_Token);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_CrewUser_List/VesselID/{VesselID}/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CrewUserListResponse Get_CrewUser_List(string VesselID, string Sync_Time, string Authentication_Token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Get_ProfilePicPath/ProfilePicPath/{ProfilePicPath}", Method = "GET", RequestFormat = WebMessageFormat.Json)]
        Stream Get_ProfilePicPath(string ProfilePicPath);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Upload_Feedback_Text", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Upload_FeedBack_ResponseClass Upload_Feedback_Text(int UserId, int CrewId, string feedback, string attachmentPath, string Authentication_Token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/UploadAudioFeedbackFileStream/FileName/*", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        AudioUploadResponseClass UploadAudioFeedbackFileStream(Stream FileByteStream);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_Vessels_By_Type_SyncTime/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}/AC_SYNC_TIME/{AC_SYNC_TIME}/FN_SYNC_TIME/{FN_SYNC_TIME}/CRW_SYN_TIME/{CRW_SYN_TIME}/CHKLIST_SYN_TIME/{CHKLIST_SYN_TIME}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        VesselCategoryTypeResponse Get_Vessels_By_Type_SyncTime(string Sync_Time, string Authentication_Token, string AC_SYNC_TIME, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME);


        [OperationContract]
        [WebInvoke(UriTemplate = "/Download_Zip_Data_byVessel/Authentication_Token/{Authentication_Token}/VesselID/{VesselID}/Sync_Time/{Sync_Time}/FN_SYNC_TIME/{FN_SYNC_TIME}/CRW_SYN_TIME/{CRW_SYN_TIME}/CHKLIST_SYN_TIME/{CHKLIST_SYN_TIME}",Method = "GET", RequestFormat = WebMessageFormat.Json)]        
        Stream Download_Zip_Data_byVessel(string Authentication_Token, string VesselID, string Sync_Time, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_Config_Alerts/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Configurable_Alerts_Result Get_Config_Alerts(string Authentication_Token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UPD_Alerts_Received/AlertsID/{AlertsID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Updated_Configurable_Alerts_rec UPD_Alerts_Received(string AlertsID, string Authentication_Token);
        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UPD_Alerts_Action/AlertsID/{AlertsID}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Updated_Configurable_Alerts_Action UPD_Alerts_Action(string AlertsID, string Authentication_Token);

        [OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/Get_Vessels_By_Type_MultiVesselSync/Sync_Time/{Sync_Time}/Authentication_Token/{Authentication_Token}/AC_SYNC_TIME/{AC_SYNC_TIME}/FN_SYNC_TIME/{FN_SYNC_TIME}/CRW_SYN_TIME/{CRW_SYN_TIME}/CHKLIST_SYN_TIME/{CHKLIST_SYN_TIME}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [WebInvoke(Method = "POST", UriTemplate = "/Get_Vessels_By_Type_MultiVesselSync", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        VesselCategoryTypeResponse Get_Vessels_By_Type_MultiVesselSync(string Sync_Time, string Authentication_Token, string AC_SYNC_TIME, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME, List<VesselIDs_Synctimes> VesselsAndSynctimes);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Get_Vessels_By_Type_MultiVesselSync_Test", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        VesselCategoryTypeResponse Get_Vessels_By_Type_MultiVesselSync_Test(VesselIDs_Synctimes_Input VesselsAndSynctimes);

        //[OperationContract]
        //[WebInvoke(Method = "GET", UriTemplate = "/Get_zipFileDetails/VesselID/{VesselID}/Sync_Time/{Sync_Time}/FN_SYNC_TIME/{FN_SYNC_TIME}/CRW_SYN_TIME/{CRW_SYN_TIME}/CHKLIST_SYN_TIME/{CHKLIST_SYN_TIME}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //DownloadZip_Result Get_zipFileDetails(string VesselID, string Sync_Time, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME, string Authentication_Token);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_zipFileDetails/VesselID/{VesselID}/Sync_Time/{Sync_Time}/FN_SYNC_TIME/{FN_SYNC_TIME}/CRW_SYN_TIME/{CRW_SYN_TIME}/CHKLIST_SYN_TIME/{CHKLIST_SYN_TIME}/Authentication_Token/{Authentication_Token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        DownloadZip_Result Get_zipFileDetails(string VesselID, string Sync_Time, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME, string Authentication_Token);

   
    }



}
