using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Web;
using System.ServiceModel.Activation;
using System.IO;
using System.ServiceModel.Web;
using System.Configuration;
using System.IO.Compression;
using System.Diagnostics;
using System.IO.Packaging;


namespace SMS.WCFMobileService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class JiBEService : iJiBEService
    {

        private static string directoryPath = @"c:\temp";
        /// <summary>
        /// Modified by Hadish on 19-10-2016
        /// Requirement :- Two more fields having info like User_Rank, Staff code should be visible 
        /// </summary>
        /// <param name="UserName">For whihc user name the details should come</param>
        /// <param name="Password">To authenticate the user</param>
        /// <param name="DeviceID">To find the device</param>
        /// <returns></returns>
        public List<UserType> Get_UserDetails(string UserName, string Password, string DeviceID)
        {
            List<UserType> Vsl = new List<UserType>();
            try
            {
                DataSet dsData = DAL_JiBE.Get_UserDetails(UserName, Password, INF.Encrypt_Decrypt.Encrypt(Password), DeviceID);
                DataTable dtuser = dsData.Tables[0];
                foreach (DataRow dr in dtuser.Rows)
                {
                    UserType ob = new UserType();
                    ob.User_ID = Convert.ToInt32(dr["UserID"]);
                    ob.User_Name = Convert.ToString(dr["User_name"]);
                    ob.Name = Convert.ToString(dr["name"]);
                    ob.Email = Convert.ToString(dr["MailID"]);
                    ob.Company_ID = UDFLib_WCF.ConvertToInteger(dr["CompanyID"]);
                    ob.Company_Name = Convert.ToString(dr["Company_Name"]);
                    ob.Phone = Convert.ToString(dr["Mobile_Number"]);
                    ob.Status = Convert.ToInt32(dr["Active_Status"]);
                    ob.Role = Convert.ToString(dr["Role"]);
                    ob.Password = Convert.ToString(dr["Password"]);
                    ob.Authentication_Token = Convert.ToString(dr["Authentication_Token"]);
                    ob.Approved_User = UDFLib_WCF.ConvertToInteger(dr["Approved_User"]);
                    ob.Max_Activity = UDFLib_WCF.ConvertToInteger(dr["Max_Activity"]);
                    ob.Max_Image = UDFLib_WCF.ConvertToInteger(dr["Max_Image"]);
                    ob.Max_Audio = UDFLib_WCF.ConvertToInteger(dr["Max_Audio"]);
                    ob.Audio_Count = UDFLib_WCF.ConvertToInteger(dr["Audio_Count"]);
                    ob.Activity_Count = UDFLib_WCF.ConvertToInteger(dr["Activity_Count"]);
                    ob.Image_Count = UDFLib_WCF.ConvertToInteger(dr["Image_Count"]);
                    ob.Staff_Code = UDFLib_WCF.ConvertToInteger(dr["Staff_Code"]);
                    ob.User_Rank = Convert.ToString(dr["RANK"]);

                    List<DepartmentType> DepartmentList = new List<DepartmentType>();
                    foreach (DataRow drdp in dsData.Tables[1].Rows)
                    {
                        DepartmentType dep = new DepartmentType();
                        dep.deptId = UDFLib_WCF.ConvertToInteger(drdp["deptId"]);
                        dep.deptName = Convert.ToString(drdp["deptName"]);
                        dep.deptType = Convert.ToString(drdp["deptType"]);

                        DepartmentList.Add(dep);
                    }

                    ob.DepartmentList = DepartmentList;
                    List<JOBType> JOBTYPEList = new List<JOBType>();
                    foreach (DataRow drdp in dsData.Tables[2].Rows)
                    {
                        JOBType JT = new JOBType();
                        JT.ID = UDFLib_WCF.ConvertToInteger(drdp["ID"]);
                        JT.Worklist_Type = Convert.ToString(drdp["Worklist_Type"]);
                        JT.Worklist_Type_Display = Convert.ToString(drdp["Worklist_Type_Display"]);

                        JOBTYPEList.Add(JT);
                    }

                    ob.JobTypeList = JOBTYPEList;


                    Vsl.Add(ob);

                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception LOG -Get_UserDetails-  " + ex.Message, null, null);
            }

            return Vsl;
        }
        //------------This UPD_APNSTOKEN function is used for updating APNS token which is supplied by Devices.
        //--------------Parameters---------------------------
        //       User_ID,Device_ID,Authentication_Token and APNS_Token is input parameter
        public bool Upd_APNSToken(string Sync_Time, string User_ID, string Device_ID, string Authentication_Token, string APNS_Token)
        {
            try
            {
                if (Authenticate(Authentication_Token))
                {
                    int OP = DAL_JiBE.Upd_APNSDetails(UDFLib_WCF.ConvertDateToNull(Sync_Time), Convert.ToInt32(User_ID), Device_ID, APNS_Token);
                    if (OP == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public VesselTypeResponse Get_Vessels(string Sync_Time, string Authentication_Token)
        {
            VesselTypeResponse objVslRes = new VesselTypeResponse();

            List<VesselType> VslList = new List<VesselType>();

            string OutSyncTime = "";

            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
                DAL_JiBE.Service_LOG("-- URL LOG -- Get_Vessels --  " + requesturl, null, null);

                if (Authenticate(Authentication_Token))
                {
                    DataSet dsVslInsp = DAL_JiBE.Get_Vessels(UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);


                    DataTable dtvsl = dsVslInsp.Tables[0];

                    DataTable dtGA = dsVslInsp.Tables[2];

                    foreach (DataRow dr in dtvsl.Rows)
                    {
                        VesselType obvsl = new VesselType();
                        obvsl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                        obvsl.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                        obvsl.Image_Path = Convert.ToString(dr["Image_Path"]);
                        obvsl.SVG_Path = Convert.ToString(dr["SVG_Path"]);


                        List<ObjectGAType> ObjGAList = new List<ObjectGAType>();

                        DataRow[] drObjGA = dtGA.Select(" Object_ID='" + obvsl.Vessel_ID.ToString() + "'");

                        foreach (DataRow drGA in drObjGA)
                        {
                            ObjectGAType objGA = new ObjectGAType();
                            objGA.ObjectID = UDFLib_WCF.ConvertToInteger(drGA["Object_ID"]);
                            objGA.ChildID = Convert.ToString(drGA["Path_ID"]);
                            objGA.Image_Path = Convert.ToString(drGA["Image_Path"]);
                            objGA.SVG_Path = Convert.ToString(drGA["SVG_Path"]);
                            objGA.Parent_ID = Convert.ToString(drGA["Parent_Path_ID"]);
                            objGA.ChildName = Convert.ToString(drGA["Path_Name"]);

                            ObjGAList.Add(objGA);
                        }

                        obvsl.ObjectGAList = ObjGAList;

                        VslList.Add(obvsl);

                    }
                    objVslRes.VesselList = VslList;

                    if (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0)
                        objVslRes.Sync_Time = OutSyncTime;

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Vessels --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception LOG - Get_Vessels -  " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return objVslRes;
        }

        public UserListResponse Get_User_List(string userID, string Sync_Time, string Authentication_Token)
        {
            UserListResponse objVslRes = new UserListResponse();

            List<UserList> VslList = new List<UserList>();

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsVslInsp = DAL_JiBE.Get_UserList(int.Parse(userID), Authentication_Token, ref OutSyncTime);

                    DataTable dtvsl = dsVslInsp.Tables[0];

                    foreach (DataRow dr in dtvsl.Rows)
                    {
                        UserList obvsl = new UserList();
                        obvsl.Inspector_name = Convert.ToString(dr["USERNAME"]);
                        obvsl.Inspector_Id = Convert.ToInt32(dr["USERID"]);
                        obvsl.Role = Convert.ToString(dr["Company_Type"]);
                        obvsl.FollowFlag = Convert.ToInt32(dr["FollowFlag"]);

                        VslList.Add(obvsl);

                    }
                    objVslRes.UserList = VslList;



                }
            }
            catch (Exception ex) { }

            return objVslRes;
        }


        public ResponseClass UPD_FollowUP(string PKID, string followupID, string ActivityID, string ObjectID, string Followup, string FOLLOWUPType, string CreatedBy, string Sync_Time, string Authentication_Token)
        {

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ActivityID;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.UPD_FollowUps(UDFLib_WCF.ConvertIntegerToNull(PKID), UDFLib_WCF.ConvertIntegerToNull(followupID), ActivityID, int.Parse(ObjectID), Followup, FOLLOWUPType, int.Parse(CreatedBy), Authentication_Token);
                else
                    resp.Status = "false";


            }
            catch { resp.Status = "false"; }
            return resp;

        }

        public FollowUPResponse GET_ALL_FollowUP(string VesselID, string Sync_Time, string Authentication_Token)
        {
            FollowUPResponse objVslRes = new FollowUPResponse();

            List<FollowupBetaType> VslList = new List<FollowupBetaType>();

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsVslInsp = DAL_JiBE.Get_AllFollowUP(UDFLib_WCF.ConvertIntegerToNull(VesselID), Sync_Time, Authentication_Token, ref OutSyncTime);

                    DataTable dtvsl = dsVslInsp.Tables[0];

                    foreach (DataRow dr in dtvsl.Rows)
                    {
                        FollowupBetaType obvsl = new FollowupBetaType();
                        obvsl.Followup = Convert.ToString(dr["Followup"]);
                        obvsl.FollowupType = Convert.ToString(dr["FollowUPType"]);
                        if (dr["createdBy"].ToString() != "")
                            obvsl.createdBy = Convert.ToString(dr["createdBy"]);
                        if (dr["activityId"].ToString() != "")
                            obvsl.activityId = Convert.ToString(dr["activityId"]);
                        obvsl.followupId = Convert.ToInt32(dr["followupId"]);
                        obvsl.objectId = Convert.ToInt32(dr["objectId"]);
                        obvsl.date = Convert.ToString(dr["createDate"]);

                        VslList.Add(obvsl);

                    }
                    objVslRes.FolowUPList = VslList;


                    if (dtvsl.Rows.Count > 0)
                        objVslRes.Sync_Time = OutSyncTime;

                }
            }
            catch (Exception ex) { }

            return objVslRes;
        }


        public GETFollowUPListResponse Get_FollowUPSettings(string Sync_Time, string Authentication_Token)
        {
            GETFollowUPListResponse objVslRes = new GETFollowUPListResponse();

            List<GETFollowUP> VslList = new List<GETFollowUP>();

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsVslInsp = DAL_JiBE.Get_FollowUPSettings(Sync_Time, Authentication_Token, ref OutSyncTime);

                    DataTable dtvsl = dsVslInsp.Tables[0];

                    foreach (DataRow dr in dtvsl.Rows)
                    {
                        GETFollowUP obvsl = new GETFollowUP();
                        obvsl.Id = Convert.ToInt32(dr["Id"]);
                        obvsl.UserID = Convert.ToInt32(dr["UserID"]);
                        obvsl.FollowID = Convert.ToString(dr["FollowID"]);
                        obvsl.FollowType = Convert.ToString(dr["FollowType"]);
                        obvsl.Active_Status = Convert.ToInt32(dr["Active_Status"]);

                        VslList.Add(obvsl);

                    }
                    objVslRes.FollowUPList = VslList;

                    if (dtvsl.Rows.Count > 0)
                        objVslRes.Sync_Time = OutSyncTime;

                }
            }
            catch (Exception ex) { }

            return objVslRes;
        }

        public ResponseClass UPD_POST_FollowUPSetting(string ID, string UserID, string FollowID, string FollowType, string CreatedBy, string Action, string Sync_Time, string Authentication_Token)
        {

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = FollowID;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_FollowUPSettings(UDFLib_WCF.ConvertIntegerToNull(ID), int.Parse(UserID), FollowID, FollowType, int.Parse(CreatedBy), int.Parse(Action));
                else
                    resp.Status = "false";


            }
            catch { resp.Status = "false"; }
            return resp;
        }






        public VesselTypeResponse Get_ALL_Vessels(string Sync_Time, string Authentication_Token)
        {
            VesselTypeResponse objVslRes = new VesselTypeResponse();

            List<VesselType> VslList = new List<VesselType>();

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsVslInsp = DAL_JiBE.Get_ALL_Vessels(UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);


                    DataTable dtvsl = dsVslInsp.Tables[0];

                    DataTable dtGA = dsVslInsp.Tables[2];

                    foreach (DataRow dr in dtvsl.Rows)
                    {
                        VesselType obvsl = new VesselType();
                        obvsl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                        obvsl.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                        obvsl.Image_Path = Convert.ToString(dr["Image_Path"]);
                        obvsl.SVG_Path = Convert.ToString(dr["SVG_Path"]);
                        obvsl.Notation = Convert.ToString(dr["Vessel_Notation"]);
                        obvsl.FollowFlag = Convert.ToInt32(dr["FollowFlag"]);

                        obvsl.status = Convert.ToInt32(dr["Active_Status"]);
                        obvsl.vesselTypeId = Convert.ToInt32(dr["Vessel_type"]);
                        obvsl.companyId = Convert.ToInt32(dr["companyId"]);


                        List<ObjectGAType> ObjGAList = new List<ObjectGAType>();

                        DataRow[] drObjGA = dtGA.Select(" Object_ID='" + obvsl.Vessel_ID.ToString() + "'");

                        foreach (DataRow drGA in drObjGA)
                        {
                            ObjectGAType objGA = new ObjectGAType();
                            objGA.ObjectID = UDFLib_WCF.ConvertToInteger(drGA["Object_ID"]);
                            objGA.ChildID = Convert.ToString(drGA["Path_ID"]);
                            objGA.Image_Path = Convert.ToString(drGA["Image_Path"]);
                            objGA.SVG_Path = Convert.ToString(drGA["SVG_Path"]);
                            objGA.Parent_ID = Convert.ToString(drGA["Parent_Path_ID"]);
                            objGA.ChildName = Convert.ToString(drGA["Path_Name"]);

                            ObjGAList.Add(objGA);
                        }

                        obvsl.ObjectGAList = ObjGAList;

                        VslList.Add(obvsl);

                    }
                    objVslRes.VesselList = VslList;

                    if (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0)
                        objVslRes.Sync_Time = OutSyncTime;

                }
            }
            catch (Exception ex) { }

            return objVslRes;
        }

        /// <summary>
        /// To upload file 
        /// </summary>
        /// <param name="FileByteStream">file</param>
        /// <returns>auto generated guid</returns>
        public ResponseClass UploadFileStream(Stream FileByteStream)
        {

            ResponseClass resp = new ResponseClass();
            resp.Status = "true";

            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsolutePath;
                var segment = context.IncomingRequest.UriTemplateMatch.WildcardPathSegments;


                DAL_JiBE.Service_LOG("-- URL LOG -- UploadFileStream  --  " + requesturl, null, null);

                string file = string.Join("\\", new string[] { segment[0], segment[1] });

                Guid GUID = Guid.NewGuid();

                string Flag_Attach = "TEC_" + GUID.ToString() + Path.GetExtension(file);

                string FilePath = Path.Combine(ConfigurationManager.AppSettings["InspectionAttachmentPath"].ToString(), Path.GetFileName(Flag_Attach));
                string FullFilename = FilePath;

                if (!Directory.Exists(Path.GetDirectoryName(FullFilename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(FullFilename));

                FileStream instream = new FileStream(FullFilename, FileMode.Create, FileAccess.Write);
                const int bufferLen = 524288;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                int bytecount = 0;

                while ((count = FileByteStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    instream.Write(buffer, 0, count);
                    bytecount += count;
                }

                instream.Close();
                instream.Dispose();


                if (segment.Count == 6)
                {
                    string activityID = segment[2];
                    int activityObjectID = Convert.ToInt32(segment[3]);
                    int ObjectID = Convert.ToInt32(segment[4]);
                    int UserID = Convert.ToInt32(segment[5]);
                    string imgName = segment[1];

                    int op = DAL_JiBE.Upd_ActivityObject_ImageName(UserID, activityID, activityObjectID, ObjectID, imgName, Flag_Attach, UDFLib_WCF.ConvertIntegerToNull(bytecount));

                }

                resp.ResponseItem = Flag_Attach;
            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception LOG - UploadFileStream -  " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }
            return resp;
        }

        public string UploadFile(string FileName, byte[] FileContent)
        {
            string FullFilename;

            // FileStream fs = new FileStream("C:\\imgres.jpg", FileMode.Open, FileAccess.Read);


            try
            {
                string UploadPath = @"c:\temp";

                if (!Directory.Exists(UploadPath))
                    Directory.CreateDirectory(UploadPath);

                Guid GUID = Guid.NewGuid();

                FullFilename = Path.Combine(UploadPath, FileName);
                using (FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(FileContent, 0, FileContent.Length);
                    fileStream.Close();
                }

            }
            catch
            {
                FullFilename = null;
            }

            return FullFilename;

        }

        public ResponseClass Upd_Activity(string ActionDate, string ActivityId, string CHILDID, string userId, string objectId, string sdeptId, string odeptId, string DUEDATE, string NAME, string NavigationPath, string ORGANIZATIONID, string REMARK, string STATUS, string TYPE, string NCR, string functionID, string systemLocationID, string subsysLocationID, string Authentication_Token)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ActivityId;
            resp.Status = "true";
            try
            {


                DAL_JiBE.Service_LOG("-- URL LOG -- Upd_Activity --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(userId));


                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_Activity(Convert.ToDateTime(ActionDate), ActivityId, CHILDID, int.Parse(userId), int.Parse(objectId), int.Parse(sdeptId), int.Parse(odeptId), Convert.ToDateTime(DUEDATE), NAME, NavigationPath, UDFLib_WCF.ConvertToInteger(ORGANIZATIONID),
                        REMARK, STATUS, UDFLib_WCF.ConvertToInteger(TYPE), UDFLib_WCF.ConvertToInteger(NCR), null, null, null, UDFLib_WCF.ConvertIntegerToNull(functionID), UDFLib_WCF.ConvertIntegerToNull(systemLocationID)
                        , UDFLib_WCF.ConvertIntegerToNull(subsysLocationID));
                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upd_Activity --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(userId));
                }

            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception Fail LOG -- Upd_Activity --  Exeception - " + ex + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(userId));
            }
            return resp;
        }

        public ResponseClass Upd_Post_Activity(string ActionDate, string ActivityId, string userId, string objectId, string NAME, string REMARK, string STATUS, string TYPE, string starflag, string Authentication_Token)
        {

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ActivityId;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_Activity_Beta(Convert.ToDateTime(ActionDate), ActivityId, int.Parse(userId), int.Parse(objectId), NAME, REMARK, STATUS, UDFLib_WCF.ConvertToInteger(TYPE), UDFLib_WCF.ConvertIntegerToNull(starflag));
                else
                    resp.Status = "false";


            }
            catch { resp.Status = "false"; }
            return resp;

        }

        public ResponseClass Upd_Post_ActivityObject(string ActivityId, string ActivityObjId, string ObjectId, string UserId, string ImageAudioPath, string followUpID, string Authentication_Token)
        {
            if (followUpID == "")
            {
                followUpID = "0";
            }

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;

            DAL_JiBE.Service_LOG("-- URL LOG -- Upd_Post_ActivityObject --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ActivityId;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_ActivityObject_Beta(ActivityId, int.Parse(ActivityObjId), ImageAudioPath, int.Parse(ObjectId), int.Parse(UserId), int.Parse(followUpID));
                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upd_Post_ActivityObject --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));
                }


            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception LOG -- Upd_Post_ActivityObject --  Exeception - " + ex + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));
            }
            return resp;

        }

        public ResponseClass Upd_Activity_Checklist(string ActionDate, string ActivityId, string CHILDID, string userId, string objectId, string sdeptId, string odeptId, string DUEDATE, string NAME, string NavigationPath, string ORGANIZATIONID, string REMARK, string STATUS, string TYPE, string NCR, string Inspection_ID, string Node_ID, string Location_ID, string functionID, string systemLocationID, string subsysLocationID, string Authentication_Token)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;

            DAL_JiBE.Service_LOG("-- URL LOG -- Upd_Activity_Checklist --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(userId));

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ActivityId;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_Activity(Convert.ToDateTime(ActionDate), ActivityId, CHILDID, int.Parse(userId), int.Parse(objectId), int.Parse(sdeptId), int.Parse(odeptId), Convert.ToDateTime(DUEDATE), NAME, NavigationPath, int.Parse(ORGANIZATIONID),
                        REMARK, STATUS, UDFLib_WCF.ConvertToInteger(TYPE), UDFLib_WCF.ConvertToInteger(NCR), UDFLib_WCF.ConvertToInteger(Inspection_ID), UDFLib_WCF.ConvertToInteger(Node_ID), UDFLib_WCF.ConvertIntegerToNull(Location_ID),
                        UDFLib_WCF.ConvertIntegerToNull(functionID), UDFLib_WCF.ConvertIntegerToNull(systemLocationID), UDFLib_WCF.ConvertIntegerToNull(subsysLocationID));
                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upd_Activity_Checklist --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(userId));
                }


            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception  LOG -- Upd_Activity_Checklist --  Exeception - " + ex + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(userId));
            }
            return resp;
        }

        public ResponseClass Upd_ActivityObject(string ActivityId, string ActivityObjId, string ObjectId, string UserId, string ImageAudioPath, string Authentication_Token)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Upd_ActivityObject --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ImageAudioPath;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_ActivityObject(ActivityId, int.Parse(ActivityObjId), ImageAudioPath, int.Parse(ObjectId), int.Parse(UserId));
                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upd_ActivityObject --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));
                }

            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception  LOG -- Upd_ActivityObject --  Token - " + ex + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));
            }

            return resp;
        }

        /// <summary>
        /// This is edited by Pranav Sakpal on 2016-06-06 
        /// GetActivityList this will give you jobs attached to inspection ie. added jobs and updated jobs as per sync time.
        /// 
        /// This will fech all the jobs for valid objectID ie vesselID on basis of sync time ie added/updated after last sync time.
        ///        
        /// eg : GetActivityList(91, 2016-06-06 10:15:00.000, aysTSjnsUSjkaJSLAHYMBVBsmma)
        /// 
        /// </summary>
        /// <param name="ObjectID">This is valid objectID.</param>
        /// <param name="Sync_Time">This is valid synctime as string format,this will be a datetime. </param>
        /// <param name="Authentication_Token">This is valid Authentication_Token of user.</param>
        /// <returns>
        /// 
        /// This will return ActivityListResponse this will contain 4 response as follows:
        /// 
        /// 1- List<ActivityType> this will contain all the properties of worklist job like actiondate,activityID,userId,name,description,objectId ie. vesselid ,etc.
        /// 
        /// 2- List<ActivityObjectType> this will contain attachment details like activtyID for which attachemnts are added, attachmentid ie activityObjId,
        ///    objectId ie. vesselid ,userId,imageaudioPath this will contain path of attachment
        ///    one worklist may have multiple attachments attached with it.
        /// 
        /// 3- List<FollowupType> this will contain details of followups added to job ie. worklist 1worklist can have multiple followups.
        ///    This will have details of followups properties like activtyID for which followups are added,followupId id of followup table,Followup ie text followup which is added for that job,
        ///    date on which followup is added,createdBy ie. followup creator id (ie userID) ,objectId ie. vesselid
        /// 
        /// 4- OutSyncTime this will send the new sync time to device and this sync time will be send to service again when performing sync.
        /// 
        /// </returns>
        public ActivityListResponse GetActivityList(string ObjectID, string Sync_Time, string Authentication_Token)
        {
            //This region is service log region and this will trace URL log on each time when service is called.
            #region service log region

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- GetActivityList --  " + requesturl, null, null);
            //Service_LOG this function will call service log code this will create URL log for GetActivityList called

            #endregion

            ActivityListResponse objActList = new ActivityListResponse();
            string OutSyncTime = "";

            try
            {
                //First we will check for user is authenticated or not by using Authenticate method 
                //This method will return true/false if Authentication_Token is valid and authenticated then Authenticate method will return true
                if (Authenticate(Authentication_Token))
                {
                    //calling DAL method to get activity by passing valid parameters ObjectID ie. vesselID,Sync_Time this can be null or valid datetime,OutSyncTime as output
                    DataSet dsData = DAL_JiBE.Get_Activity(UDFLib_WCF.ConvertIntegerToNull(ObjectID), UDFLib_WCF.ConvertDateToNull(Sync_Time), ref OutSyncTime);

                    List<ActivityType> ActivityList = new List<ActivityType>();

                    List<ActivityObjectType> ActivityObjectList = new List<ActivityObjectType>();

                    List<FollowupType> FollowupList = new List<FollowupType>();

                    //This will check if Get_Activity results tables in dataset 
                    if (dsData.Tables.Count > 0)
                    {

                        DataTable Activity = dsData.Tables[0];
                        DataTable ActivityObj = dsData.Tables[1];
                        DataTable Followup = dsData.Tables[2];

                        //This will fetch row by row data from Activity table
                        foreach (DataRow dract in Activity.Rows)
                        {
                            //In this foreach loop we will create new object for ActivityType and we will assign all the properties from datatable
                            ActivityType objAct = new ActivityType();
                            objAct.actionDate = Convert.ToString(dract["actionDate"]);
                            objAct.activityId = Convert.ToString(dract["activityId"]);
                            objAct.childId = Convert.ToString(dract["childId"]);
                            objAct.dueDate = Convert.ToString(dract["dueDate"]);
                            objAct.userId = UDFLib_WCF.ConvertToInteger(dract["userId"]);
                            objAct.name = Convert.ToString(dract["name"]);
                            objAct.navigationPath = Convert.ToString(dract["navigationPath"]);
                            objAct.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                            objAct.odeptId = UDFLib_WCF.ConvertToInteger(dract["odeptId"]);
                            objAct.organizationId = UDFLib_WCF.ConvertToInteger(dract["organizationId"]);
                            objAct.remark = Convert.ToString(dract["remark"]);
                            objAct.sdeptId = UDFLib_WCF.ConvertToInteger(dract["sdeptId"]);
                            objAct.status = Convert.ToString(dract["status"]);
                            objAct.type = UDFLib_WCF.ConvertToInteger(dract["type"]);
                            objAct.inspectorName = Convert.ToString(dract["inspectorName"]);
                            objAct.ncr = UDFLib_WCF.ConvertToInteger(dract["ncr"]);
                            objAct.InspectionId = UDFLib_WCF.ConvertToInteger(dract["InspectionDetailId"]);
                            objAct.LocationId = UDFLib_WCF.ConvertToInteger(dract["LocationID"]);
                            objAct.NodeId = UDFLib_WCF.ConvertToInteger(dract["LocationNodeID"]);

                            objAct.FunctionId = UDFLib_WCF.ConvertToInteger(dract["Function_ID"]);
                            objAct.TreeLocationId = UDFLib_WCF.ConvertToInteger(dract["Location_ID"]);
                            objAct.SubLocationId = UDFLib_WCF.ConvertToInteger(dract["Sub_Location_ID"]);
                            objAct.AttachedStatus = UDFLib_WCF.ConvertToInteger(dract["AttachedStatus"]);

                            //This object will be added to list  ActivityList here
                            ActivityList.Add(objAct);
                        }

                        //This will fetch row by row data from ActivityObj table
                        foreach (DataRow dract in ActivityObj.Rows)
                        {
                            //In this foreach loop we will create new object for ActivityObjectType and we will assign all the properties from datatable
                            ActivityObjectType objActobj = new ActivityObjectType();
                            objActobj.activityId = Convert.ToString(dract["activityId"]);
                            objActobj.activityObjId = UDFLib_WCF.ConvertToInteger(dract["activityObjId"]);
                            objActobj.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                            objActobj.userId = UDFLib_WCF.ConvertToInteger(dract["userId"]);

                            //This line is edited by Pranav Sakpal on 06-06-2016 for JIT : 9891
                            //If dract["imageaudioPath"] - attachment name is with path details then  GetFileName will remove all this part and will return file name and its extension.
                            objActobj.imageaudioPath = Path.GetFileName(Convert.ToString(dract["imageaudioPath"]));

                            //This object will be added to list  ActivityObjectList here
                            ActivityObjectList.Add(objActobj);
                        }
                        //This will fetch row by row data from Followup table
                        foreach (DataRow dract in Followup.Rows)
                        {
                            //In this foreach loop we will create new object for FollowupType and we will assign all the properties from datatable
                            FollowupType objAFlw = new FollowupType();
                            objAFlw.activityId = Convert.ToString(dract["activityId"]);
                            objAFlw.followupId = UDFLib_WCF.ConvertToInteger(dract["followupId"]);
                            objAFlw.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                            objAFlw.Followup = Convert.ToString(dract["Followup"]);
                            objAFlw.date = Convert.ToString(dract["createDate"]);
                            objAFlw.createdBy = Convert.ToString(dract["createdBy"]);

                            //This object will be added to list  FollowupList here
                            FollowupList.Add(objAFlw);
                        }


                        objActList.ActivityList = ActivityList;
                        objActList.ActivityObjectList = ActivityObjectList;
                        objActList.FollowupList = FollowupList;
                        //This will not assign outsync time if any of this 3 tables has 0 rows count
                        if (Activity.Rows.Count > 0 || ActivityObj.Rows.Count > 0 || Followup.Rows.Count > 0)
                            objActList.Sync_Time = OutSyncTime;

                    }
                }
                else
                {
                    #region service log Authentication Fail
                    //This will log service URL for Authentication Fail  
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- GetActivityList --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                    return objActList;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region service log for Exception log
                objActList.Sync_Time = ex.Message;
                //This will log service URL and exception details for Exception Fail  
                DAL_JiBE.Service_LOG("Exception LOG -- GetActivityList --  Token - " + ex + " -- URL --" + requesturl + "      -- EXCEPTION --  " + ex.Message, null, null);

                #endregion
            }

            return objActList;
            //Return responselist

        }

        public ActivityListBetaResponse Get_ALL_ActivityList(string UserID, string Sync_Time, string Authentication_Token)
        {
            ActivityListBetaResponse objActList = new ActivityListBetaResponse();
            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {

                    DataSet dsData = DAL_JiBE.Get_ALL_Activity(int.Parse(UserID), UDFLib_WCF.ConvertDateToNull(Sync_Time), ref OutSyncTime);

                    List<ActivityBetaType> ActivityList = new List<ActivityBetaType>();

                    List<ActivityObjectBetaType> ActivityObjectList = new List<ActivityObjectBetaType>();

                    List<FollowupBetaType> FollowupList = new List<FollowupBetaType>();

                    DataTable Activity = dsData.Tables[0];
                    DataTable ActivityObj = dsData.Tables[1];
                    DataTable Followup = dsData.Tables[2];

                    foreach (DataRow dract in Activity.Rows)
                    {
                        ActivityBetaType objAct = new ActivityBetaType();
                        objAct.actionDate = Convert.ToString(dract["actionDate"]);
                        objAct.activityId = Convert.ToString(dract["activityId"]);
                        objAct.childId = Convert.ToString(dract["childId"]);
                        objAct.dueDate = Convert.ToString(dract["dueDate"]);
                        objAct.userId = UDFLib_WCF.ConvertToInteger(dract["userId"]);
                        objAct.name = Convert.ToString(dract["name"]);
                        objAct.navigationPath = Convert.ToString(dract["navigationPath"]);
                        objAct.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                        objAct.odeptId = UDFLib_WCF.ConvertToInteger(dract["odeptId"]);
                        objAct.organizationId = UDFLib_WCF.ConvertToInteger(dract["organizationId"]);
                        objAct.remark = Convert.ToString(dract["remark"]);
                        objAct.sdeptId = UDFLib_WCF.ConvertToInteger(dract["sdeptId"]);
                        objAct.status = Convert.ToString(dract["status"]);
                        objAct.type = UDFLib_WCF.ConvertToInteger(dract["type"]);
                        objAct.inspectorName = Convert.ToString(dract["inspectorName"]);
                        objAct.ncr = UDFLib_WCF.ConvertToInteger(dract["ncr"]);
                        objAct.InspectionId = UDFLib_WCF.ConvertToInteger(dract["InspectionDetailId"]);
                        objAct.LocationId = UDFLib_WCF.ConvertToInteger(dract["LocationID"]);
                        objAct.NodeId = UDFLib_WCF.ConvertToInteger(dract["LocationNodeID"]);
                        objAct.StarFlag = UDFLib_WCF.ConvertToInteger(dract["StarFlag"]);
                        objAct.EditableFlag = UDFLib_WCF.ConvertToInteger(dract["EditableFlag"]);
                        objAct.FollowFlag = UDFLib_WCF.ConvertToInteger(dract["FollowFlag"]);

                        ActivityList.Add(objAct);
                    }

                    foreach (DataRow dract in ActivityObj.Rows)
                    {
                        ActivityObjectBetaType objActobj = new ActivityObjectBetaType();
                        objActobj.activityId = Convert.ToString(dract["activityId"]);
                        objActobj.activityObjId = UDFLib_WCF.ConvertToInteger(dract["activityObjId"]);
                        objActobj.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                        objActobj.userId = UDFLib_WCF.ConvertToInteger(dract["userId"]);
                        objActobj.imageaudioPath = Convert.ToString(dract["imageaudioPath"]);

                        ActivityObjectList.Add(objActobj);
                    }

                    foreach (DataRow dract in Followup.Rows)
                    {
                        FollowupBetaType objAFlw = new FollowupBetaType();
                        objAFlw.activityId = Convert.ToString(dract["activityId"]);
                        objAFlw.followupId = UDFLib_WCF.ConvertToInteger(dract["followupId"]);
                        objAFlw.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                        objAFlw.Followup = Convert.ToString(dract["Followup"]);
                        objAFlw.date = Convert.ToString(dract["createDate"]);
                        objAFlw.createdBy = Convert.ToString(dract["createdBy"]);
                        objAFlw.FollowupType = Convert.ToString(dract["FollowUPType"]);
                        objAFlw.editableFlag = UDFLib_WCF.ConvertToInteger(dract["EditableFlag"]);

                        FollowupList.Add(objAFlw);
                    }


                    objActList.ActivityList = ActivityList;
                    objActList.ActivityObjectList = ActivityObjectList;
                    objActList.FollowupList = FollowupList;

                    if (Activity.Rows.Count > 0 || ActivityObj.Rows.Count > 0 || Followup.Rows.Count > 0)
                        objActList.Sync_Time = OutSyncTime;
                }
            }
            catch (Exception ex) { objActList.Sync_Time = ex.Message; }

            return objActList;

        }

        public JobStatusTypeResponse Get_Job_Status(string ObjectID, string ActivityID, string Sync_Time, string Authentication_Token)
        {
            JobStatusTypeResponse JobStatusRes = new JobStatusTypeResponse();
            List<JobStatusType> JobStatusList = new List<JobStatusType>();

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_Job_Status --  " + requesturl, null, null);

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataTable dtJobStatus = DAL_JiBE.Get_Job_Status(UDFLib_WCF.ConvertIntegerToNull(ObjectID), ActivityID, UDFLib_WCF.ConvertDateToNull(Sync_Time));

                    foreach (DataRow drjob in dtJobStatus.Rows)
                    {
                        JobStatusType Job = new JobStatusType();
                        Job.activityId = Convert.ToString(drjob["activityId"]);
                        Job.objectId = Convert.ToString(drjob["objectId"]);
                        Job.activityStatus = Convert.ToString(drjob["JOB_STATUS"]);

                        JobStatusList.Add(Job);
                    }
                    JobStatusRes.JobList = JobStatusList;

                    if (dtJobStatus.Rows.Count > 0)
                        JobStatusRes.Sync_Time = Convert.ToString(dtJobStatus.Rows[0]["SYNC_TIME"]);
                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Job_Status --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception LOG -- Get_Job_Status --  Exception - " + ex + " -- URL --" + requesturl, null, null);
            }

            return JobStatusRes;
        }

        public ResponseClass Upd_Create_User(string User_Name, string Password, string Email_Address, string Mobile_Number, string Company_Name)
        {
            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = "";
            resp.Status = "true";
            try
            {
                int UserID = DAL_JiBE.Upd_Create_User(User_Name, Password, Email_Address, Mobile_Number, Company_Name);
                resp.ResponseItem = UserID.ToString();

            }
            catch { resp.Status = "false"; }
            return resp;
        }

        public Stream DownloadFile(string FileName)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- DownloadFile --  " + requesturl, null, null);

            FileStream StreamedFile = null;
            try
            {

                string FilePath = Path.Combine(ConfigurationManager.AppSettings["InspectionAttachmentPath"].ToString(), Path.GetFileName(FileName));

                StreamedFile = File.OpenRead(FilePath);


            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Authentication Fail LOG -- DownloadFile --  Token - NULL -- URL --" + requesturl, null, null);

            }

            return StreamedFile;
        }

        public string ConvertDataTabletoJson(DataTable dtData)
        {


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dtData.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    if (col.DataType.ToString() == "System.DateTime")
                        row.Add(col.ColumnName, Convert.ToString(dr[col]));
                    else
                        row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        private bool Authenticate(string Authentication_Token)
        {
            bool Status = false;

            try
            {
                Status = DAL_JiBE.Get_Authentication_Details(Authentication_Token).Rows.Count > 0 ? true : false;
            }
            catch { }

            return Status;
        }

        public string VerifyAccount(string Code)
        {
            string sts = "0";
            try
            {
                sts = DAL_JiBE.UPD_Verify_User(Code).ToString();
            }
            catch (Exception ex)
            {

            }
            return sts;
        }

        public ResponseClass Upd_Contact_Us(string UserID, string Message, string Authentication_Token)
        {

            ResponseClass resp = new ResponseClass();

            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    resp.ResponseItem = DAL_JiBE.Upd_Contact_Us(int.Parse(UserID), Message).ToString();
                else
                    resp.Status = "false";


            }
            catch { resp.Status = "false"; }
            return resp;
        }

        public ResponseClass Upd_ActivityStatus(string ActivityId, string Date, string UserId, string Status, string Authentication_Token)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Upd_ActivityStatus --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = ActivityId;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_Activity_Status(Convert.ToInt32(UserId), Convert.ToDateTime(Date), Status, ActivityId).ToString();
                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upd_ActivityStatus --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));
                }

            }
            catch (Exception ex)
            {

                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception  LOG -- Upd_ActivityStatus --  Exception - " + ex + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserId));
            }
            return resp;

        }

        public ResponseClass UploadFileStreamZip(Stream FileByteStream)
        {

            ResponseClass resp = new ResponseClass();
            resp.Status = "true";

            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsolutePath;
                var segment = context.IncomingRequest.UriTemplateMatch.WildcardPathSegments;
                string file = string.Join("\\", segment);

                resp.ResponseItem = Path.GetFileName(file);

                string FilePath = Path.Combine(ConfigurationManager.AppSettings["InspectionAttachmentPath"].ToString(), Path.GetFileName(file));
                string FullFilename = FilePath;

                if (!Directory.Exists(Path.GetDirectoryName(FullFilename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(FullFilename));

                FileStream instream = new FileStream(FullFilename, FileMode.Create, FileAccess.Write);
                const int bufferLen = 524288;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                int bytecount = 0;
                while ((count = FileByteStream.Read(buffer, 0, bufferLen)) > 0)
                {

                    instream.Write(buffer, 0, count);

                    bytecount += count;
                }
                instream.Close();
                instream.Dispose();
                string zipPath = FilePath;
                string extractPath = Path.GetDirectoryName(FilePath);
                if (!extractPath.EndsWith(@"\"))
                    extractPath = extractPath + @"\";
                string batfilename = extractPath + Path.GetFileNameWithoutExtension(FilePath) + "ze" + ".bat";
                TextWriter tw = new StreamWriter(batfilename);
                tw.WriteLine("WinRar  x  " + extractPath + Path.GetFileNameWithoutExtension(FilePath) + ".zip" + " *.* " + extractPath);
                tw.Close();
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = true;
                proc.StartInfo.FileName = batfilename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
                if (File.Exists(batfilename))
                    File.Delete(batfilename);


                // save to worklist folder
                try
                {
                    File.Copy(FilePath, Path.Combine(ConfigurationManager.AppSettings["WorkListAttachmentPath"].ToString(), Path.GetFileName(file)));
                }
                catch { }

            }
            catch
            {
                resp.Status = "false";
            }

            return resp;

        }

        public Stream DownloadFileZip(string FileName)
        {
            FileStream StreamedFile = null;
            try
            {

                string FilePath = Path.Combine(ConfigurationManager.AppSettings["InspectionAttachmentPath"].ToString(), Path.GetFileName(FileName));


                string batfilename = Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileNameWithoutExtension(FilePath) + "z" + ".bat";
                TextWriter tw = new StreamWriter(batfilename);
                tw.WriteLine("cd " + Path.GetDirectoryName(FilePath));
                tw.WriteLine("winrar a -afzip " + Path.GetFileNameWithoutExtension(FilePath) + ".zip " + Path.GetFileName(FilePath));
                tw.Close();
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = true;
                proc.StartInfo.FileName = batfilename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
                if (File.Exists(batfilename))
                    File.Delete(batfilename);
                StreamedFile = File.OpenRead(Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileNameWithoutExtension(FilePath) + ".zip ");


            }
            catch { }

            return StreamedFile;
        }

        /// <summary>
        /// This method will fetched the data based on vessel id and syntime
        /// </summary>
        /// <param name="Sync_Time">To get the updated record as per time passed as value</param>
        /// <param name="Authentication_Token">it is used to validate the user</param>
        /// <param name="VesselID">To get data of particular vessel</param>
        /// <param name="FN_SYNC_TIME">To get the updated record from functional tree table</param>
        /// <param name="CRW_SYN_TIME">To get the updated record from crew list </param>
        /// <param name="CHKLIST_SYN_TIME">To get the updated record from check list</param>
        /// <returns></returns>

        public VesselCategoryTypeResponse Get_Vessels_By_Type(string Sync_Time, string Authentication_Token)
        {
            VesselCategoryTypeResponse objVslRes = new VesselCategoryTypeResponse();

            List<VesselTypeList> VslTypeList = new List<VesselTypeList>();

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_Vessels_By_Type --  " + requesturl, null, null);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsVslInsp = DAL_JiBE.Get_Vessels_By_Type(UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);

                    DataTable dtVesselType = dsVslInsp.Tables[0];//That will store vessel Type data from dsVslInsp to dtVesselType

                    DataTable dtvsl = dsVslInsp.Tables[1]; // dtvsl will have vessel data

                    DataTable dtGA = dsVslInsp.Tables[3]; //dtGA will have GA list

                    foreach (DataRow drVslType in dtVesselType.Rows)
                    {
                        VesselTypeList objVslType = new VesselTypeList();

                        List<ObjectGAType> ObjGAList = new List<ObjectGAType>();

                        List<VesselCategoryType> VslList = new List<VesselCategoryType>();

                        DataRow[] drObjGA = dtGA.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");

                        foreach (DataRow drGA in drObjGA)
                        {
                            ObjectGAType objGA = new ObjectGAType();

                            objGA.ChildID = Convert.ToString(drGA["Path_ID"]);
                            objGA.Image_Path = Convert.ToString(drGA["Image_Path"]);
                            objGA.SVG_Path = Convert.ToString(drGA["SVG_Path"]);
                            objGA.Parent_ID = Convert.ToString(drGA["Parent_Path_ID"]);
                            objGA.ChildName = Convert.ToString(drGA["Path_Name"]);

                            ObjGAList.Add(objGA);
                        }


                        DataRow[] drObjVsl = dtvsl.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");
                        foreach (DataRow dr in drObjVsl)
                        {
                            VesselCategoryType obvsl = new VesselCategoryType();
                            obvsl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                            obvsl.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);

                            if (dr["ACTIVE_STATUS"].ToString() == "0")
                            {
                                obvsl.Active_Status = 0;
                            }
                            else if (dr["ACTIVE_STATUS"].ToString() == "1")
                            {
                                obvsl.Active_Status = 1;
                            }

                            VslList.Add(obvsl);

                        }

                        objVslType.VesselList = VslList;
                        objVslType.GAList = ObjGAList;
                        objVslType.Vessel_TypeID = Convert.ToInt32(drVslType["vessel_TypeID"]);
                        objVslType.Vessel_TypeName = Convert.ToString(drVslType["VesselTypes"]);

                        VslTypeList.Add(objVslType);
                    }


                    objVslRes.VesselTypeList = VslTypeList;
                    if (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0)
                        objVslRes.Sync_Time = OutSyncTime;

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Vessels_By_Type --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_Vessels_By_Type --  Exception - " + ex.ToString() + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return objVslRes;
        }

        /// <summary>
        /// This function will update checklist items rating values when mobile application user updates ratings from mobile clicked on save then this service will be called.
        /// </summary>
        /// <param name="InspectionID">Valid inspection ID</param>
        /// <param name="NodeID">Valid nodeID of checklist</param>
        /// <param name="Value">Valid ratings values NULL/Decimals</param>
        /// <param name="Authentication_Token">Valid User Authentication Token</param>
        /// <returns></returns>
        public ResponseClass Upd_NodeValue(string InspectionID, string NodeID, string Value, string Authentication_Token)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Upd_NodeValue --  " + requesturl, null, null);

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = NodeID;
            resp.Status = "true";

            decimal? Tempvalue;
            if (Value == "NULL" || Value == "null" || Value == "(NULL)" || Value == "(null)")
            {
                Tempvalue = null;
            }
            else
            {
                Tempvalue = Convert.ToDecimal(Value);
            }

            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_NodeValue(Convert.ToInt32(InspectionID), Convert.ToInt32(NodeID), Tempvalue);

                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upd_NodeValue --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }


            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception  LOG -- Upd_NodeValue --  Exception - " + ex + " -- URL --" + requesturl, null, null);


            }
            return resp;
        }


        public CompanyTypeResponse Get_Company(string Sync_Time, string Authentication_Token)
        {
            CompanyTypeResponse objComlRes = new CompanyTypeResponse();

            List<CompanyType> ComList = new List<CompanyType>();

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataTable dtCompany = DAL_JiBE.Get_Company(UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);

                    foreach (DataRow dr in dtCompany.Rows)
                    {
                        CompanyType objComp = new CompanyType();
                        objComp.Company_ID = Convert.ToInt32(dr["Company_ID"]);
                        objComp.Company_Name = Convert.ToString(dr["Company_Name"]);


                        ComList.Add(objComp);

                    }
                    objComlRes.CompanylList = ComList;

                    if (dtCompany.Rows.Count > 0)
                        objComlRes.Sync_Time = OutSyncTime;

                }
            }
            catch (Exception ex) { }

            return objComlRes;
        }



        #region checklist
        /// <summary>
        /// Edited by Pranav Sakpal on 06-06-2016 for JIT : 9890 
        /// Get_Checklist - This function will call from mobile to get checklists and scheduled inspections for particular vessel.
        /// 
        /// This method will send response -ChecklistResponse  ie. all the details of checklist its groups,locations and items as well as this will return grading type lib.
        /// 
        /// </summary>
        /// <param name="VesselID">Valid vesselid to get checklist and scheduled inspection for it. </param>
        /// <param name="UserID">Valid UserID - currently this is not userspecific but you can fetch by userID as well.  </param>
        /// <param name="Sync_Time">Sync time implementation not done.</param>
        /// <param name="Authentication_Token">Valid user authentication token to proceed.</param>
        /// <returns>
        /// This will result three collections :
        /// 
        /// 1- checklist collection
        /// 2- Checklist Item collection
        /// 3- Gradings collection
        /// 
        /// Later edited by Hadish on 03-11-2016
        /// </returns>
        public ChecklistResponse Get_Checklist(string VesselID, string UserID, string Sync_Time, string Authentication_Token)
        {
            //This region is service log region and this will trace URL log on each time when service is called.
            #region URL LOG
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_Checklist --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserID));
            //Service_LOG this function will call service log code this will create URL log for Get_Checklist called

            #endregion

            ChecklistResponse objVslRes = new ChecklistResponse();

            List<Checklist> CHList = new List<Checklist>();
            List<ChecklistItem> chkItems = new List<ChecklistItem>();
            List<Gradings> chkGrades = new List<Gradings>();

            int? vID = UDFLib_WCF.ConvertIntegerToNull(VesselID);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsCHK = new DataSet();
                    //This will check if VesselID is null 
                    if (vID != null)
                    {
                        //If vesselID is not null then this function will be called and this will return data on basis of vesselID
                        //dsCHK = DAL_JiBE.Get_AllCheckList_DL(int.Parse(VesselID), null); // If user id is null then uncoment this line and comment the following code ,Done by Hadish
                        if(Sync_Time.ToUpper()=="null" || Sync_Time.ToUpper()=="(null)")
                            dsCHK = DAL_JiBE.Get_AllCheckList_DL(int.Parse(VesselID), UDFLib_WCF.ConvertToInteger(UserID),null);
                        else
                            dsCHK = DAL_JiBE.Get_AllCheckList_DL(int.Parse(VesselID), UDFLib_WCF.ConvertToInteger(UserID), UDFLib_WCF.ConvertDateToNull(Sync_Time));
                    }
                    else
                    {
                        //If vesselID is null then this function will be called and this will return data on basis of UserID
                        dsCHK = DAL_JiBE.Get_AllCheckList_BY_UserID_DL(Convert.ToInt32(UserID));
                    }

                    //If dsCHK has result ie. table counts then only this will go for loop
                    if (dsCHK.Tables.Count > 0)
                    {
                        //This will fetch 1 by 1 row from dsCHK.Tables
                        foreach (DataRow dr in dsCHK.Tables[0].Rows)
                        {
                            //This will check for checklist name and its nodetype will not like Group, Location or Item 
                            if (dr["NodeType"].ToString() != "Group" && dr["NodeType"].ToString() != "Location" && dr["NodeType"].ToString() != "Item")
                            {
                                //This will add checklist details in  Checklist object.
                                Checklist obCHKList = new Checklist();
                                obCHKList.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_ID"]);
                                obCHKList.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                                obCHKList.scheduledate = Convert.ToString(dr["SheduleDate"]);
                                obCHKList.completiondate = Convert.ToString(dr["CompletionDate"]);
                                obCHKList.originalscheduledate = Convert.ToString(dr["OriginalSheduleDate"]);
                                obCHKList.inspectionstatus = Convert.ToString(dr["inspectionStatus"]);
                                obCHKList.InspectionType = Convert.ToString(dr["InspectionType"]);
                                obCHKList.checklistname = Convert.ToString(dr["CheckList_Name"]);
                                if (vID != null)
                                {
                                    obCHKList.vesselid = Convert.ToInt32(vID);
                                }
                                else
                                {
                                    obCHKList.vesselid = Convert.ToInt32(dr["Vessel_ID"]);
                                }
                                obCHKList.checklisttypename = Convert.ToString(dr["CheckList_Type_name"]);
                                obCHKList.checklisttype = UDFLib_WCF.ConvertToInteger(dr["checklistType"]);
                                obCHKList.gradingtype = UDFLib_WCF.ConvertToInteger(dr["Grading_Type"]);
                                obCHKList.vesseltypename = Convert.ToString(dr["NodeType"]);
                                obCHKList.vesseltype = UDFLib_WCF.ConvertToInteger(dr["Vessel_Type"]);
                                obCHKList.submitstatus = Convert.ToString(dr["Final_Stat"]);
                                obCHKList.inspActiveStatus = Convert.ToInt32(dr["Active_Status"]);
                                obCHKList.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                                //This will filter dsCHK table by node type as group and not null parentID and this  differentiated datatable will be passed to getrecurtionGroupParent as parameter.
                                dsCHK.Tables[0].DefaultView.RowFilter = "NodeType= 'Group' AND Parent_ID IS NULL AND Checklist_IDCopy='" + dr["Checklist_ID"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                                DataTable dtgroupDiff = dsCHK.Tables[0].DefaultView.ToTable();
                                if (dtgroupDiff.Rows.Count > 0)
                                {
                                    obCHKList.Group = getrecurtionGroupParent(dtgroupDiff, dsCHK.Tables[0], null);
                                    //This will add group data collection to checklist object.
                                }

                                //This will filter dsCHK table by node type as Location and not null parentID and this  differentiated datatable will be passed to getrecurtionLocation as parameter.
                                dsCHK.Tables[0].DefaultView.RowFilter = "NodeType= 'Location' AND Parent_ID IS NULL AND Checklist_IDCopy='" + dr["Checklist_ID"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                                DataTable dtLocationDiff = dsCHK.Tables[0].DefaultView.ToTable();
                                if (dtLocationDiff.Rows.Count > 0)
                                {
                                    obCHKList.Location = getrecurtionLocation(dtLocationDiff, null);
                                    //This will add location data collection to checklist object under group.
                                }

                                CHList.Add(obCHKList);
                                //This is all collections are added to checklist collection object.
                            }
                            else if (dr["NodeType"].ToString() == "Item")// This will check for nodetype with value ITEM this all items are stored in checklistItem Collection and its a seperate result
                            {
                                ChecklistItem objchkItem = new ChecklistItem();
                                objchkItem.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_IDCopy"]);
                                objchkItem.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                                objchkItem.checklistitemid = UDFLib_WCF.ConvertToInteger(dr["ChecklistItem_ID"]);
                                objchkItem.checklistitemname = Convert.ToString(dr["CheckList_Name"]);
                                objchkItem.nodetype = Convert.ToString(dr["NodeType"]);
                                objchkItem.nodevalue = UDFLib_WCF.ConvertDecimalToNull(dr["ItemRates"]) != null ? Convert.ToDecimal(dr["ItemRates"]) : 0;
                                objchkItem.gradeid = UDFLib_WCF.ConvertToInteger(dr["ItemGrading_Type"]);
                                objchkItem.locationid = UDFLib_WCF.ConvertToInteger(dr["Parent_ID"]);
                                objchkItem.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                                chkItems.Add(objchkItem);
                                //checklist item collection is added to checklist collection object.
                            }

                        }

                        //This Get_LocGradeType_DL will give you all the grading types
                        DataSet dsGrading = DAL_JiBE.Get_LocGradeType_DL();

                        //This will fetch row by row data from dsGrading table and add to Gradings objects and its collection object
                        foreach (DataRow dr in dsGrading.Tables[0].Rows)
                        {
                            Gradings obGradings = new Gradings();
                            obGradings.gradenames = Convert.ToString(dr["Grade_Name"]);
                            obGradings.gradeid = UDFLib_WCF.ConvertToInteger(dr["ID"]);
                            obGradings.optiontext = Convert.ToString(dr["OptionText"]);
                            obGradings.optionvalue = dr["OptionValue"].ToString() == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(dr["OptionValue"]);

                            chkGrades.Add(obGradings);
                            //Gradings objects added to collection object 1 by 1.
                        }

                        objVslRes.CheckList = CHList;
                        objVslRes.CheckListItems = chkItems;
                        objVslRes.ObjectGradesList = chkGrades;
                    }
                }
                else
                {
                    //This region is service log region and this will trace URL log authentication failiure.
                    #region URL LOG Authentication Failiure
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Checklist --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserID));
                    //Service_LOG this function will call service log code for authentication failiure this will create URL log for Get_Checklist called
                    #endregion
                }

            }
            catch (Exception ex)
            {
                //This region is service log region and this will trace URL log on exception.
                #region URL LOG Exception  log
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_Checklist --  Exception - " + ex + " -- URL --" + requesturl + " -- Exception Log -- " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(UserID));
                #endregion
            }

            return objVslRes;
            //This will return response collection of checklist,checklist Items,Gradings to phone.
        }


        /// <summary>
        /// getrecurtionGroupParent - edited by Pranav Sakpal on 06-06-2016 for JIT : 9890 
        /// 
        /// This function will contain parent groups details.
        /// 
        /// by calling recursive functions for location and child groups this will be added to resulted collection of group
        /// 
        /// eg: Group-Parent                    -- This is parent group
        ///         Group-child                 -- This is its first child group of a parent group
        ///             groupchild-Location     -- This is its first location of first child group
        ///         groupparent-Location        -- This is its first location of a parent group 
        /// 
        /// </summary>
        /// <param name="dtdiffTable">Contains result filterd data.</param>
        /// <param name="dtTable">Contains original resulted data.</param>
        /// <param name="ParentID">ParentID is to check is null or not</param>
        /// <returns>
        /// List<ChecklistGroup>- This will return Parent group lists to calling function.
        /// </returns>
        public static List<ChecklistGroup> getrecurtionGroupParent(DataTable dtdiffTable, DataTable dtTable, string ParentID)
        {
            List<ChecklistGroup> listGroup = new List<ChecklistGroup>();
            List<ChecklistLocation> listLocation = new List<ChecklistLocation>();
            //This will check for parentid is null if null then this recurtion function will only returns group data of parentID null values
            if (ParentID == null)
            {
                //This loop will fetch 1 by 1 row from dtdiffTable
                foreach (DataRow dr in dtdiffTable.Rows)
                {
                    if (dr["Parent_ID"] == DBNull.Value)
                    {
                        //This condition will check for node type is  Group and its parentid will be null if condition is satisfied then assign all the properties of ChecklistGroup 
                        if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                        {
                            ChecklistGroup chGroup = new ChecklistGroup();
                            chGroup.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_IDCopy"]);
                            chGroup.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                            chGroup.groupname = Convert.ToString(dr["CheckList_Name"]);
                            chGroup.parentid = UDFLib_WCF.ConvertToInteger(dr["Parent_ID"]);
                            chGroup.groupid = UDFLib_WCF.ConvertToInteger(dr["ChecklistItem_ID"]);
                            chGroup.nodetype = Convert.ToString(dr["NodeType"]);
                            chGroup.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                            //parent
                            //This datatable filtering will check for child groups inside it
                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group' AND Checklist_IDCopy='" + dr["Checklist_IDCopy"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                            //If child groups found inside the group then this will be added in dtGroupDiff
                            DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                            //if child groups found and dtGroupDiff have records ie rows then this function will call getrecurtionGroupChild ie child group recurtion function and pass its difference table in it (dtGroupDiff)
                            if (dtGroupDiff.Rows.Count > 0)
                            {
                                //child
                                List<ChecklistGroup> listchildGroup = new List<ChecklistGroup>();
                                //call for the child group recursive method
                                listchildGroup = getrecurtionGroupChild(dtGroupDiff, dtTable, dr["ChecklistItem_ID"].ToString());
                                chGroup.Group = listchildGroup;
                                //by calling this recursive methods this will result in hirachical way.

                            }

                            //This will be filtering locations under this group if any of locations found inside this group then dtLocationDiff datable will have rows in it.
                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location' AND Checklist_IDCopy='" + dr["Checklist_IDCopy"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                            DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                            //If dtLocationDiff current group have any locations inside it then this will have rows in datatable.
                            if (dtLocationDiff.Rows.Count > 0)
                            {
                                //if condition is fullfilled then this will call getrecurtionLocation to append the locations in group.
                                //This method call is changed because there is no need of resulting original table.
                                listLocation = getrecurtionLocation(dtLocationDiff, dr["ChecklistItem_ID"].ToString());

                                chGroup.Location = listLocation;
                                //locations collections will be added to group 

                            }

                            listGroup.Add(chGroup);
                            //This will list of group

                        }

                    }

                }
            }

            return listGroup;
            //returns result of groups and its childs to calling function.

        }

        /// <summary>
        /// getrecurtionGroupChild - edited by Pranav Sakpal on 06-06-2016 for JIT : 9890 
        /// 
        /// This function will contain child groups details.
        /// 
        ///  by calling recursive functions for location this will be added to resulted collection of locations in the group
        ///  
        /// eg: 
        ///         Group-child                 -- This is its first child group of a parent group
        ///             groupchild-Location     -- This is its first location of first child group
        ///             
        ///             OR
        ///             
        ///         Group-child                                     -- This is its first child group of a parent group
        ///             child-of-Group-child                        -- This is 
        ///                     Location-of-child-of-Group-child
        ///                     
        ///             groupchild-Location                         -- This is its first location of first child group
        ///     
        /// </summary>
        /// <param name="dtdiffTable">Contains result filterd data.</param>
        /// <param name="dtTable">Contains original resulted data.</param>
        /// <param name="ParentID">ParentID is to check is null or not</param>
        /// <returns>
        /// List<ChecklistGroup>- This will return Child group lists to calling function.
        /// </returns>
        public static List<ChecklistGroup> getrecurtionGroupChild(DataTable dtdiffTable, DataTable dtTable, string ParentID)
        {
            List<ChecklistGroup> listGroup = new List<ChecklistGroup>();
            List<ChecklistLocation> listLocation = new List<ChecklistLocation>();

            //This loop will fetch 1 by 1 row from dtdiffTable
            foreach (DataRow dr in dtdiffTable.Rows)
            {
                if (dr["Parent_ID"] != DBNull.Value)
                {
                    //This condition will check for node type is  Group and its parentid will be null if condition is satisfied then assign all the properties of ChecklistGroup 
                    if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                    {
                        ChecklistGroup chGroup = new ChecklistGroup();
                        chGroup.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_IDCopy"]);
                        chGroup.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                        chGroup.groupname = Convert.ToString(dr["CheckList_Name"]);
                        chGroup.parentid = UDFLib_WCF.ConvertToInteger(dr["Parent_ID"]);
                        chGroup.groupid = UDFLib_WCF.ConvertToInteger(dr["ChecklistItem_ID"]);
                        chGroup.nodetype = Convert.ToString(dr["NodeType"]);
                        chGroup.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                        //This datatable filtering will check for child groups inside it
                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group' AND Checklist_IDCopy='" + dr["Checklist_IDCopy"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                        DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                        //if child groups found and dtGroupDiff have records ie rows then this function will call getrecurtionGroupChild ie child group recurtion function and pass its difference table in it (dtGroupDiff)
                        if (dtGroupDiff.Rows.Count > 0)
                        {
                            List<ChecklistGroup> listchildGroup = new List<ChecklistGroup>();
                            //call for the child group recursive method
                            listchildGroup = getrecurtionGroupChild(dtGroupDiff, dtTable, dr["ChecklistItem_ID"].ToString());
                            chGroup.Group = listchildGroup;
                        }

                        //This will be filtering locations under this group if any of locations found inside this group then dtLocationDiff datable will have rows in it.
                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location' AND Checklist_IDCopy='" + dr["Checklist_IDCopy"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                        DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                        //If dtLocationDiff current group have any locations inside it then this will have rows in datatable.
                        if (dtLocationDiff.Rows.Count > 0)
                        {
                            //if condition is fullfilled then this will call getrecurtionLocation to append the locations in group.
                            //This method call is changed because there is no need of resulting original table.
                            listLocation = getrecurtionLocation(dtLocationDiff, dr["ChecklistItem_ID"].ToString());
                            chGroup.Location = listLocation;
                            //locations collections will be added to group 
                        }
                        listGroup.Add(chGroup);
                        //This will list of group

                    }
                }
            }

            return listGroup;
            //returns result of groups and its childs to calling function.
        }

        /// <summary>
        /// This is edited by Pranav Sakpal on 04-06-2016 for JIT : 9890 
        /// 
        /// getrecurtionLocation this function will return collection of locations.
        /// 
        /// It has properties like checklistid,inspectionid,locationname,parentid,locationid,nodetype,componentid,componentname,index,nodevalue.
        /// 
        /// There are two cases ParentID is null in this dtdiffTable check with ParentID is null else it will directly fetch the records for locations
        /// 
        /// </summary>
        /// <param name="dtdiffTable">This table contains location data only</param>
        /// <param name="ParentID">seperate parentid null condition</param>
        /// <returns>
        /// List<ChecklistLocation>- This will return checklist locations collection back to calling function 
        /// </returns>
        public static List<ChecklistLocation> getrecurtionLocation(DataTable dtdiffTable, string ParentID)
        {
            List<ChecklistLocation> listloc = new List<ChecklistLocation>();
            //This will check for parentid is null if null then this recurtion function will only returns location data of parentID null values
            if (ParentID == null)
            {
                //This loop will fetch 1 by 1 row from dtdiffTable
                foreach (DataRow dr in dtdiffTable.Rows)
                {
                    if (dr["Parent_ID"] == DBNull.Value)
                    {
                        //This condition will check for node type is  location and its parentid will be null if condition is satisfied then assign all the properties of ChecklistLocation 
                        if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location" && dr["Parent_ID"] == DBNull.Value)
                        {
                            ChecklistLocation chlocation = new ChecklistLocation();
                            chlocation.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_IDCopy"]);
                            chlocation.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                            chlocation.locationname = Convert.ToString(dr["CheckList_Name"]);
                            chlocation.parentid = UDFLib_WCF.ConvertToInteger(dr["Parent_ID"]);
                            chlocation.locationid = UDFLib_WCF.ConvertToInteger(dr["ChecklistItem_ID"]);
                            chlocation.nodetype = Convert.ToString(dr["NodeType"]);
                            chlocation.componentid = UDFLib_WCF.ConvertToInteger(dr["Location_ID"]);
                            chlocation.componentname = Convert.ToString(dr["LocationName"]);
                            chlocation.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                            //This is added by Pranav Sakpal on 04-06-2016. 
                            //To send node values as null if sp retun it as null  As per JIT : 9890 This code will check for DBNull values if found nodevalue is set as null.
                            if (dr["LocationRates"] == DBNull.Value)
                                chlocation.nodevalue = null;
                            else
                                chlocation.nodevalue = UDFLib_WCF.ConvertDecimalToNull(dr["LocationRates"]);

                            listloc.Add(chlocation);
                            // ChecklistLocation objects are added to list
                        }
                    }

                }
            }
            else
            {
                //This loop will fetch 1 by 1 row from dtdiffTable
                foreach (DataRow dr in dtdiffTable.Rows)
                {
                    //This condition will check for node type is location if condition is satisfied then assign all the properties of ChecklistLocation 
                    if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location")
                    {
                        ChecklistLocation chlocation = new ChecklistLocation();
                        chlocation.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_IDCopy"]);
                        chlocation.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                        chlocation.locationname = Convert.ToString(dr["CheckList_Name"]);
                        chlocation.parentid = UDFLib_WCF.ConvertToInteger(dr["Parent_ID"]);
                        chlocation.locationid = UDFLib_WCF.ConvertToInteger(dr["ChecklistItem_ID"]);
                        chlocation.nodetype = Convert.ToString(dr["NodeType"]);
                        chlocation.componentid = UDFLib_WCF.ConvertToInteger(dr["Location_ID"]);
                        chlocation.componentname = Convert.ToString(dr["LocationName"]);

                        //This is added by Pranav Sakpal on 04-06-2016.
                        //to send node values as null if sp retun it as null  As per JIT : 9890 This code will check for DBNull values if found nodevalue is set as null
                        if (dr["LocationRates"] == DBNull.Value)
                            chlocation.nodevalue = null;
                        else
                            chlocation.nodevalue = Convert.ToDecimal(dr["LocationRates"]);

                        chlocation.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);


                        listloc.Add(chlocation);
                        // ChecklistLocation objects are added to list
                    }
                }
            }

            return listloc;
            //This will return checklist locations collection back to calling function 
        }

        public ResponseClass Submit_Status(string Checklist_ID, string Inspection_ID, string Mobile_Status, string Sync_Time, string Authentication_Token)
        {
            int stat = 0;
            if (Mobile_Status == null || Mobile_Status == "")
                stat = 1;
            else
                stat = Convert.ToInt32(Mobile_Status);

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Submit_Status --  " + requesturl, null, null);

            ResponseClass resp = new ResponseClass();
            resp.ResponseItem = Checklist_ID;
            resp.Status = "true";
            try
            {
                if (Authenticate(Authentication_Token))
                    DAL_JiBE.Upd_Checklist_Status(Convert.ToInt32(Checklist_ID), Convert.ToInt32(Inspection_ID), stat);
                else
                {
                    resp.Status = "false";
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Submit_Status --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }


            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception  LOG -- Submit_Status --  Exception - " + ex + " -- URL --" + requesturl, null, null);
            }
            return resp;


        }

        #endregion checklist

        public FunctionalTreeResponse Get_FunctionalTree(string Sync_Time, string VesselID, string Authentication_Token)
        {
            FunctionalTreeResponse objTreeRes = new FunctionalTreeResponse();
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_FunctionalTree --  " + requesturl, null, null);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsFunctionTreeData = DAL_JiBE.Get_FunctionalTree_DL(null, UDFLib_WCF.ConvertIntegerToNull(VesselID), null, UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);


                    DataTable dtFunctions = dsFunctionTreeData.Tables[0];

                    DataTable dtSystems = dsFunctionTreeData.Tables[1];

                    DataTable dtSubSystems = dsFunctionTreeData.Tables[2];

                    List<function> lstFunction = new List<function>();
                    List<FunctionalSystem> lstSystem = new List<FunctionalSystem>();
                    List<SubSystem> lstSubsystem = new List<SubSystem>();

                    foreach (DataRow dr in dtFunctions.Rows)
                    {
                        function obFunction = new function();
                        obFunction.Code = Convert.ToInt32(dr["Code"]);
                        obFunction.Description = Convert.ToString(dr["Description"]);
                        obFunction.Active_Status = Convert.ToInt32(dr["Active_Status"]);
                        obFunction.VesselID = Convert.ToInt32(dr["VesselID"]);


                        lstFunction.Add(obFunction);

                    }
                    foreach (DataRow dr in dtSystems.Rows)
                    {
                        FunctionalSystem obSystem = new FunctionalSystem();
                        obSystem.Functions = Convert.ToInt32(dr["Functions"]);
                        obSystem.SystemCode = Convert.ToInt32(dr["System_Code"]);
                        obSystem.Description = Convert.ToString(dr["Description"]);
                        obSystem.Active_Status = Convert.ToInt32(dr["Active_Status"]);
                        obSystem.LocationCode = Convert.ToInt32(dr["Location_Code"]);
                        obSystem.VesselID = Convert.ToInt32(dr["VesselID"]);

                        lstSystem.Add(obSystem);

                    }

                    foreach (DataRow dr in dtSubSystems.Rows)
                    {
                        SubSystem obSubSystem = new SubSystem();
                        obSubSystem.SubsystemCode = Convert.ToInt32(dr["Subsystem_Code"]);
                        obSubSystem.SystemCode = Convert.ToInt32(dr["System_Code"]);
                        obSubSystem.Description = Convert.ToString(dr["Description"]);
                        obSubSystem.Active_Status = Convert.ToInt32(dr["Active_Status"]);
                        obSubSystem.LocationCode = Convert.ToInt32(dr["Location_Code"]);
                        obSubSystem.VesselID = Convert.ToInt32(dr["VesselID"]);


                        lstSubsystem.Add(obSubSystem);

                    }


                    objTreeRes.Functions = lstFunction;
                    objTreeRes.Systems = lstSystem;
                    objTreeRes.Subsystems = lstSubsystem;

                    if (dtFunctions.Rows.Count > 0 || dtSystems.Rows.Count > 0 || dtSubSystems.Rows.Count > 0)
                        objTreeRes.Sync_Time = OutSyncTime;

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_FunctionalTree --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_FunctionalTree --  Exception - " + ex + " -- URL --" + requesturl, null, null);
            }

            return objTreeRes;
        }

        #region CrewUserList
        /// <summary>
        ///Author:- Modified by Hadish 
        /// To get onboard crew details for selected vessel.
        /// </summary>
        /// <param name="VesselID">Id of vessel</param>
        /// <param name="Sync_Time">requested date and time</param>
        /// <param name="Authentication_Token">to authenticate user.</param>
        /// <returns>List of onboard crew details</returns>
        public CrewUserListResponse Get_CrewUser_List(string VesselID, string Sync_Time, string Authentication_Token)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_CrewUser_List --  " + requesturl, null, null);
            CrewUserListResponse _crewListResponse = new CrewUserListResponse();
            List<CrewUserListType> _crewUserList = new List<CrewUserListType>();
            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet _dsCrewList = DAL_JiBE.Get_CrewUser_List(UDFLib_WCF.ConvertIntegerToNull(VesselID), UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);
                    if (_dsCrewList != null)
                    {
                        DataTable _dtCrewTable = _dsCrewList.Tables[0];
                        foreach (DataRow _dr in _dtCrewTable.Rows)
                        {
                            CrewUserListType _objCrewUser = new CrewUserListType();
                            _objCrewUser.Rank = Convert.ToString(_dr["Rank"]);
                            _objCrewUser.StaffID = UDFLib_WCF.ConvertIntegerToNull(_dr["StaffID"]);
                            _objCrewUser.UserName = Convert.ToString(_dr["UserName"]);
                            _objCrewUser.ProfilePicPath = Convert.ToString(_dr["ProfilePicPath"]);
                            _objCrewUser.Activestatus = Convert.ToBoolean(_dr["ActiveStatus"]);
                            _objCrewUser.Nationality = Convert.ToString(_dr["Nationality"]);
                            _objCrewUser.Dob_Crew_User = Convert.ToString(_dr["Dob_Crew_User"]);
                            _objCrewUser.Company_Seniority_Years =Convert.ToString(_dr["Company_Seniority_Years"]);
                            _objCrewUser.Company_Contract = UDFLib_WCF.ConvertIntegerToNull(_dr["Company_Contract"]);
                            _objCrewUser.Rank_Seniority_Years =Convert.ToString(_dr["Rank_Seniority_Years"]);
                            _objCrewUser.Rank_Contract = UDFLib_WCF.ConvertIntegerToNull(_dr["Rank_Contract"]);
                            _objCrewUser.Approved_Cards = Convert.ToString(_dr["Approved_Cards"]);
                            _objCrewUser.Approver_Remarks = Convert.ToString(_dr["Approver_Remarks"]);
                            _objCrewUser.Cards_Status = Convert.ToString(_dr["Cards_Status"]);
                            _objCrewUser.Cards_Remark = Convert.ToString(_dr["Cards_Remark"]);
                            _objCrewUser.Average_Evaluation = UDFLib_WCF.ConvertIntegerToNull(_dr["Average_Evaluation"]);
                            _objCrewUser.Last_Evalation = UDFLib_WCF.ConvertIntegerToNull(_dr["Last_Evalation"]);
                            _objCrewUser.Staff_Code = UDFLib_WCF.ConvertIntegerToNull(_dr["Staff_Code"]);
                            if (_dr["Rank_Sort_Order"] == null)
                                _objCrewUser.Rank_Sort_Order = null;
                            else
                                _objCrewUser.Rank_Sort_Order = Convert.ToInt32(_dr["Rank_Sort_Order"]); // This field is added as per changes suggested.

                            _crewUserList.Add(_objCrewUser);
                        }
                        _crewListResponse.CrewUserList = _crewUserList;

                        if (_dtCrewTable.Rows.Count > 0)
                        {
                            _crewListResponse.Sync_Time = OutSyncTime;
                        }
                    }
                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_FunctionalTree --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_CrewUser_List --  Exception - " + ex + " -- URL --" + requesturl + "--Exception--" + ex.Message.ToString(), null, null);
            }
            return _crewListResponse;

        }

        /// <summary>
        /// Get_CrewUser_List This Method use to fetch crew basic details like its rank,dob,profile pic etc
        /// </summary>
        /// <param name="VesselID"></param>
        /// <param name="Sync_Time">To get upto date data ,First time it will null </param>
        /// <param name="Authentication_Token">This is to find user is authorize or not</param>
        /// <returns>Profile Pic</returns>
        public Stream Get_ProfilePicPath(string ProfilePicPath)
        {
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_ProfilePicPath --  " + requesturl, null, null);

            FileStream StreamedFile = null;
            try
            {
                string FilePath = Path.Combine(ConfigurationManager.AppSettings["CrewProfilePicPath"].ToString(), Path.GetFileName(ProfilePicPath));
                try
                {
                    StreamedFile = File.OpenRead(FilePath);
                }
                catch (Exception ex)
                {
                    DAL_JiBE.Service_LOG("File Not Found  -- Get_ProfilePicPath --  Token - NULL -- URL --" + requesturl + "--Exception--" + ex.ToString(), null, null);
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception LOG -- Get_ProfilePicPath --  Token - NULL -- URL --" + requesturl + "--Exception--" + ex.Message.ToString(), null, null);
            }
            return StreamedFile;
        }

        #endregion


        /// <summary>
        /// Created By Hadish on 27-09-2016
        /// Upload_Feedback_Text is method using in service
        /// </summary>
        /// <param name="UserId">Value will be the tne who is logged in </param>
        /// <param name="CrewId">Crew id </param>
        /// <param name="feedback">Its kind of remark will be passed</param>
        /// <param name="attachmentPath">It is not mendatory</param>
        /// <param name="Authentication_Token">To autheticate the logged in user</param>
        /// <returns></returns>

        #region Upload Feedback Text
        public Upload_FeedBack_ResponseClass Upload_Feedback_Text(int UserId, int CrewId, string feedback, string attachmentPath, string Authentication_Token)
        {
            Upload_FeedBack_ResponseClass Upldresp = new Upload_FeedBack_ResponseClass();
            var requesturl = "";
            Upldresp.Status = "true";

            try
            {
                var context = WebOperationContext.Current;
                requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;

                // log will be generated by this with Log name and requested Url 
                DAL_JiBE.Service_LOG("-- URL LOG -- Upload_FeedBack --  " + requesturl, null, null);

                if (Authenticate(Authentication_Token))
                {
                    DataSet _dsUploadFeedback =
                    DAL_JiBE.UPD_Feedback_Text(UDFLib_WCF.ConvertIntegerToNull(UserId), UDFLib_WCF.ConvertIntegerToNull(CrewId), feedback, attachmentPath);
                    if (_dsUploadFeedback != null)
                    {
                        DataTable _dtUploadFeedback = _dsUploadFeedback.Tables[0];
                        Upldresp = new Upload_FeedBack_ResponseClass();
                        Upldresp.FeedbackId = _dtUploadFeedback.Rows[0]["FeedBackID"].ToString();
                        Upldresp.Status = _dtUploadFeedback.Rows[0]["Status"].ToString();

                    }
                }
                else
                    Upldresp.Status = "false";


            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Authentication Fail LOG -- Upload_FeedBack --  Token - NULL -- URL --" + requesturl + "--Exception--" + ex.Message.ToString(), null, null);
                Upldresp.Status = "false";
            }
            return Upldresp;
        }
        #endregion

        /// <summary>
        /// Created by Hadish on 28-09-2016
        /// UploadFileStream 
        /// </summary>
        /// <param name="FileByteStream">Audio file of type stream</param>
        /// <returns></returns>

        #region AudioFeedbackUpload
        public AudioUploadResponseClass UploadAudioFeedbackFileStream(Stream FileByteStream)
        {

            AudioUploadResponseClass resp = new AudioUploadResponseClass();
            resp.Status = "true";


            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsolutePath;
                var segment = context.IncomingRequest.UriTemplateMatch.WildcardPathSegments;
                DAL_JiBE.Service_LOG("-- URL LOG -- UploadAudioFeedbackFileStream  --  " + requesturl, null, null);
                string file = string.Join("\\", segment.Last());//To get the last Parameter sent via URL,By Hadish on 15-Dec-2016
                Guid GUID = Guid.NewGuid();
                if (file != null && Path.GetExtension(file) != "")
                {
                    string Flag_Attach = GUID.ToString() + Path.GetExtension(file);
                    string FilePath = Path.Combine(ConfigurationManager.AppSettings["CrewFeedbackAudioAttachmentPath"].ToString(), Path.GetFileName(Flag_Attach));
                    string FullFilename = FilePath;
                    if (!Directory.Exists(Path.GetDirectoryName(FullFilename)))
                        Directory.CreateDirectory(Path.GetDirectoryName(FullFilename));
                    FileStream instream = new FileStream(FullFilename, FileMode.Create, FileAccess.Write);
                    const int bufferLen = 524288;
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    int bytecount = 0;
                    while ((count = FileByteStream.Read(buffer, 0, bufferLen)) > 0)
                    {

                        instream.Write(buffer, 0, count);
                        bytecount += count;
                    }
                    instream.Close();
                    instream.Dispose();

                    if (segment.Count == 6)
                    {
                        string activityID = segment[2];
                        int activityObjectID = Convert.ToInt32(segment[3]);
                        int ObjectID = Convert.ToInt32(segment[4]);
                        int UserID = Convert.ToInt32(segment[5]);
                        string imgName = segment[1];
                        int op = DAL_JiBE.Upd_ActivityObject_ImageName(UserID, activityID, activityObjectID, ObjectID, imgName, Flag_Attach, UDFLib_WCF.ConvertIntegerToNull(bytecount));
                    }
                    resp.ResponseItem = Flag_Attach;
                }

            }
            catch (Exception ex)
            {
                resp.Status = "false";
                DAL_JiBE.Service_LOG("Exception LOG - UploadAudioFeedbackFileStream -  " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return resp;

        }
        #endregion
        /// <summary>
        /// Requirement :- Get the Vessel data by type and SynTIme
        /// Edited by :- Hadish shah on 21-Nov-2016
        /// Purpose :-Attachment size getting 0 despite of having size and Synctime should not visible when vessel type returns no record
        /// </summary>
        /// <param name="Sync_Time">To get the current time when last data fetched</param>
        /// <param name="Authentication_Token">To validate the user</param>
        /// <param name="FN_SYNC_TIME">To get the current time when last data fetched of functions</param>
        /// <param name="CRW_SYN_TIME">To get the current time when last data of crew</param>
        /// <param name="CHKLIST_SYN_TIME">To get the current time when last data fetched of cehcklist</param>
        /// <returns></returns>
        #region Implementing service method for Get vessel by tupe and sync time
        public VesselCategoryTypeResponse Get_Vessels_By_Type_SyncTime(string Sync_Time, string Authentication_Token, string AC_SYNC_TIME, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME)
        {

            VesselCategoryTypeResponse objVslRes = new VesselCategoryTypeResponse();

            List<VesselTypeList> VslTypeList = new List<VesselTypeList>();

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_Vessels_By_Type_SyncTime --  " + requesturl, null, null);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {

                    DataSet dsVslInsp = DAL_JiBE.Get_Vessels_By_Type_SyncTime(UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, UDFLib_WCF.ConvertDateToNull(AC_SYNC_TIME), UDFLib_WCF.ConvertDateToNull(FN_SYNC_TIME), UDFLib_WCF.ConvertDateToNull(CRW_SYN_TIME), UDFLib_WCF.ConvertDateToNull(CHKLIST_SYN_TIME), ref OutSyncTime);
                    if (dsVslInsp != null && dsVslInsp.Tables.Count > 0)
                    {

                        DataTable dtVesselType = dsVslInsp.Tables[0];//That will store vessel Type data from dsVslInsp to dtVesselType

                        DataTable dtvsl = dsVslInsp.Tables[1]; // dtvsl will have vessel data

                        DataTable dtGA = dsVslInsp.Tables[3]; //dtGA will have GA list

                        foreach (DataRow drVslType in dtVesselType.Rows)
                        {
                            VesselTypeList objVslType = new VesselTypeList();

                            List<ObjectGAType> ObjGAList = new List<ObjectGAType>();

                            List<VesselCategoryType> VslList = new List<VesselCategoryType>();

                            DataRow[] drObjGA = dtGA.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");

                            foreach (DataRow drGA in drObjGA)
                            {
                                ObjectGAType objGA = new ObjectGAType();

                                objGA.ChildID = Convert.ToString(drGA["Path_ID"]);
                                objGA.Image_Path = Convert.ToString(drGA["Image_Path"]);
                                objGA.SVG_Path = Convert.ToString(drGA["SVG_Path"]);
                                objGA.Parent_ID = Convert.ToString(drGA["Parent_Path_ID"]);
                                objGA.ChildName = Convert.ToString(drGA["Path_Name"]);

                                ObjGAList.Add(objGA);
                            }


                            DataRow[] drObjVsl = dtvsl.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");
                            foreach (DataRow dr in drObjVsl)
                            {
                                VesselCategoryType obvsl = new VesselCategoryType();
                                obvsl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                                obvsl.Vessel_ID = UDFLib_WCF.ConvertToInteger(dr["Vessel_ID"]);
                                //obvsl.Data_Size = dr["DataSize"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["DataSize"]); //To convert null to 0 this line is using
                                obvsl.Attachment_Size = dr["Attachment_Size"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["Attachment_Size"]);

                                if (dr["ACTIVE_STATUS"].ToString() == "0")
                                {
                                    obvsl.Active_Status = 0;
                                }
                                else if (dr["ACTIVE_STATUS"].ToString() == "1")
                                {
                                    obvsl.Active_Status = 1;
                                }

                                VslList.Add(obvsl);

                            }

                            objVslType.VesselList = VslList;
                            objVslType.GAList = ObjGAList;
                            objVslType.Vessel_TypeID = Convert.ToInt32(drVslType["vessel_TypeID"]);
                            objVslType.Vessel_TypeName = Convert.ToString(drVslType["VesselTypes"]);

                            VslTypeList.Add(objVslType);
                        }


                        objVslRes.VesselTypeList = VslTypeList;
                        //if (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0) After discussed with kalpesh commented this line , Condition to check count in vessel Type dataset
                        if (dtVesselType.Rows.Count > 0 && (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0))

                            objVslRes.Sync_Time = OutSyncTime;
                    }
                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Vessels_By_Type_SyncTime --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_Vessels_By_Type_SyncTime --  Exception - " + ex.ToString() + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return objVslRes;

        }
        #endregion



        /// <summary>
        /// Requirement :- Get the Acitiylist data 
        /// </summary>
        /// <param name="ObjectID">Its nothing but vessel id , based on vessel id it provides the data</param>
        /// <param name="Sync_Time">To get the current time when last data fetched</param>
        /// <param name="Authentication_Token">To validate the user</param>
        /// <param name="requesturl">To log service called by which url?</param>
        /// <returns></returns>
        #region  Copying the existing function for calling purpose for service
        public ActivityListResponse GetActivityList_Service(string ObjectID, string Sync_Time, string Authentication_Token, string requesturl)
        {

            ActivityListResponse objActList = new ActivityListResponse();
            string OutSyncTime = "";

            try
            {
                //First we will check for user is authenticated or not by using Authenticate method 
                //This method will return true/false if Authentication_Token is valid and authenticated then Authenticate method will return true
                if (Authenticate(Authentication_Token))
                {
                    //calling DAL method to get activity by passing valid parameters ObjectID ie. vesselID,Sync_Time this can be null or valid datetime,OutSyncTime as output
                    DataSet dsData = DAL_JiBE.Get_Activity(UDFLib_WCF.ConvertIntegerToNull(ObjectID), UDFLib_WCF.ConvertDateToNull(Sync_Time), ref OutSyncTime);

                    List<ActivityType> ActivityList = new List<ActivityType>();

                    List<ActivityObjectType> ActivityObjectList = new List<ActivityObjectType>();

                    List<FollowupType> FollowupList = new List<FollowupType>();

                    //This will check if Get_Activity results tables in dataset 
                    if (dsData.Tables.Count > 0)
                    {

                        DataTable Activity = dsData.Tables[0];
                        DataTable ActivityObj = dsData.Tables[1];
                        DataTable Followup = dsData.Tables[2];

                        //This will fetch row by row data from Activity table
                        foreach (DataRow dract in Activity.Rows)
                        {
                            //In this foreach loop we will create new object for ActivityType and we will assign all the properties from datatable
                            ActivityType objAct = new ActivityType();
                            objAct.actionDate = Convert.ToString(dract["actionDate"]);
                            objAct.activityId = Convert.ToString(dract["activityId"]);
                            objAct.childId = Convert.ToString(dract["childId"]);
                            objAct.dueDate = Convert.ToString(dract["dueDate"]);
                            objAct.userId = UDFLib_WCF.ConvertToInteger(dract["userId"]);
                            objAct.name = Convert.ToString(dract["name"]);
                            objAct.navigationPath = Convert.ToString(dract["navigationPath"]);
                            objAct.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                            objAct.odeptId = UDFLib_WCF.ConvertToInteger(dract["odeptId"]);
                            objAct.organizationId = UDFLib_WCF.ConvertToInteger(dract["organizationId"]);
                            objAct.remark = Convert.ToString(dract["remark"]);
                            objAct.sdeptId = UDFLib_WCF.ConvertToInteger(dract["sdeptId"]);
                            objAct.status = Convert.ToString(dract["status"]);
                            objAct.type = UDFLib_WCF.ConvertToInteger(dract["type"]);
                            objAct.inspectorName = Convert.ToString(dract["inspectorName"]);
                            objAct.ncr = UDFLib_WCF.ConvertToInteger(dract["ncr"]);
                            objAct.InspectionId = UDFLib_WCF.ConvertToInteger(dract["InspectionDetailId"]);
                            objAct.LocationId = UDFLib_WCF.ConvertToInteger(dract["LocationID"]);
                            objAct.NodeId = UDFLib_WCF.ConvertToInteger(dract["LocationNodeID"]);

                            objAct.FunctionId = UDFLib_WCF.ConvertToInteger(dract["Function_ID"]);
                            objAct.TreeLocationId = UDFLib_WCF.ConvertToInteger(dract["Location_ID"]);
                            objAct.SubLocationId = UDFLib_WCF.ConvertToInteger(dract["Sub_Location_ID"]);
                            objAct.AttachedStatus = UDFLib_WCF.ConvertToInteger(dract["AttachedStatus"]);

                            //This object will be added to list  ActivityList here
                            ActivityList.Add(objAct);
                        }

                        //This will fetch row by row data from ActivityObj table
                        foreach (DataRow dract in ActivityObj.Rows)
                        {
                            //In this foreach loop we will create new object for ActivityObjectType and we will assign all the properties from datatable
                            ActivityObjectType objActobj = new ActivityObjectType();
                            objActobj.activityId = Convert.ToString(dract["activityId"]);
                            objActobj.activityObjId = UDFLib_WCF.ConvertToInteger(dract["activityObjId"]);
                            objActobj.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                            objActobj.userId = UDFLib_WCF.ConvertToInteger(dract["userId"]);

                            //This line is edited by Pranav Sakpal on 06-06-2016 for JIT : 9891
                            //If dract["imageaudioPath"] - attachment name is with path details then  GetFileName will remove all this part and will return file name and its extension.
                            objActobj.imageaudioPath = Path.GetFileName(Convert.ToString(dract["imageaudioPath"]));

                            //This object will be added to list  ActivityObjectList here
                            ActivityObjectList.Add(objActobj);
                        }
                        //This will fetch row by row data from Followup table
                        foreach (DataRow dract in Followup.Rows)
                        {
                            //In this foreach loop we will create new object for FollowupType and we will assign all the properties from datatable
                            FollowupType objAFlw = new FollowupType();
                            objAFlw.activityId = Convert.ToString(dract["activityId"]);
                            objAFlw.followupId = UDFLib_WCF.ConvertToInteger(dract["followupId"]);
                            objAFlw.objectId = UDFLib_WCF.ConvertToInteger(dract["objectId"]);
                            objAFlw.Followup = Convert.ToString(dract["Followup"]);
                            objAFlw.date = Convert.ToString(dract["createDate"]);
                            objAFlw.createdBy = Convert.ToString(dract["createdBy"]);

                            //This object will be added to list  FollowupList here
                            FollowupList.Add(objAFlw);
                        }


                        objActList.ActivityList = ActivityList;
                        objActList.ActivityObjectList = ActivityObjectList;
                        objActList.FollowupList = FollowupList;
                        //This will not assign outsync time if any of this 3 tables has 0 rows count
                        if (Activity.Rows.Count > 0 || ActivityObj.Rows.Count > 0 || Followup.Rows.Count > 0)
                            objActList.Sync_Time = OutSyncTime;

                    }
                }
                else
                {
                    #region service log Authentication Fail
                    //This will log service URL for Authentication Fail  
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- GetActivityList_Service --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                    return objActList;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region service log for Exception log
                objActList.Sync_Time = ex.Message;
                //This will log service URL and exception details for Exception Fail  
                DAL_JiBE.Service_LOG("Exception LOG -- GetActivityList_Service --  Exception - " + ex + " -- URL --" + requesturl + "      -- EXCEPTION --  " + ex.Message, null, null);

                #endregion
            }

            return objActList;
            //Return responselist

        }
        #endregion




        /// <summary>
        /// Requirement :- Get the list of crew 
        /// </summary>
        /// <param name="VesselID">based on vessel id it provides the data</param>
        /// <param name="Sync_Time">To get the current time when last data fetched</param>
        /// <param name="Authentication_Token">To validate the user</param>
        /// <param name="requesturl">To log service called by which url?</param>
        /// <returns></returns>
        #region  Copying the existing function Get_CrewUser_List with little editon for calling purpose for service
        public CrewUserListResponse Get_CrewUser_List_Service(string VesselID, string Sync_Time, string Authentication_Token, string requesturl)
        {

            CrewUserListResponse _crewListResponse = new CrewUserListResponse();
            List<CrewUserListType> _crewUserList = new List<CrewUserListType>();
            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet _dsCrewList = DAL_JiBE.Get_CrewUser_List(UDFLib_WCF.ConvertIntegerToNull(VesselID), UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);
                    if (_dsCrewList != null)
                    {
                        DataTable _dtCrewTable = _dsCrewList.Tables[0];
                        foreach (DataRow _dr in _dtCrewTable.Rows)
                        {
                            CrewUserListType _objCrewUser = new CrewUserListType();
                            _objCrewUser.Rank = Convert.ToString(_dr["Rank"]);
                            _objCrewUser.StaffID = UDFLib_WCF.ConvertIntegerToNull(_dr["StaffID"]);
                            _objCrewUser.UserName = Convert.ToString(_dr["UserName"]);
                            _objCrewUser.ProfilePicPath = Convert.ToString(_dr["ProfilePicPath"]);
                            _objCrewUser.Activestatus = Convert.ToBoolean(_dr["ActiveStatus"]);
                            _objCrewUser.Nationality = Convert.ToString(_dr["Nationality"]);
                            _objCrewUser.Dob_Crew_User = Convert.ToString(_dr["Dob_Crew_User"]);
                            //_objCrewUser.Company_Seniority_Years = UDFLib_WCF.ConvertIntegerToNull(_dr["Company_Seniority_Yers"]); Changes done by Hadish on 17-Dec-2016 JIT 12387
                            _objCrewUser.Company_Seniority_Years = Convert.ToString(_dr["Company_Seniority_Years"]);
                            _objCrewUser.Company_Contract = UDFLib_WCF.ConvertIntegerToNull(_dr["Company_Contract"]);
                            //_objCrewUser.Rank_Seniority_Years = UDFLib_WCF.ConvertIntegerToNull(_dr["Rank_Seniority_Years"]);
                            _objCrewUser.Rank_Seniority_Years = Convert.ToString(_dr["Rank_Seniority_Years"]);
                            _objCrewUser.Rank_Contract = UDFLib_WCF.ConvertIntegerToNull(_dr["Rank_Contract"]);
                            _objCrewUser.Approved_Cards = Convert.ToString(_dr["Approved_Cards"]);
                            _objCrewUser.Approver_Remarks = Convert.ToString(_dr["Approver_Remarks"]);
                            _objCrewUser.Cards_Status = Convert.ToString(_dr["Cards_Status"]);
                            _objCrewUser.Cards_Remark = Convert.ToString(_dr["Cards_Remark"]);
                            _objCrewUser.Average_Evaluation = UDFLib_WCF.ConvertIntegerToNull(_dr["Average_Evaluation"]);
                            _objCrewUser.Last_Evalation = UDFLib_WCF.ConvertIntegerToNull(_dr["Last_Evalation"]);
                            _objCrewUser.Staff_Code = UDFLib_WCF.ConvertIntegerToNull(_dr["Staff_Code"]);

                            _crewUserList.Add(_objCrewUser);
                        }
                        _crewListResponse.CrewUserList = _crewUserList;

                        if (_dtCrewTable.Rows.Count > 0)
                        {
                            _crewListResponse.Sync_Time = OutSyncTime;
                        }
                    }
                }
                else
                {
                    #region service log Authentication Fail
                    //This will log service URL for Authentication Fail  
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_CrewUser_List_Service --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_CrewUser_List_Service --  Exception - " + ex + " -- URL --" + requesturl + "--Exception--" + ex.Message.ToString(), null, null);
            }
            return _crewListResponse;

        }
        #endregion




        /// <summary>
        /// Requirement :- Get the list of Function data 
        /// </summary>
        /// <param name="Sync_Time">To get the current time when last data fetched</param>
        /// <param name="VesselID">based on vessel id it provides the data</param>
        /// <param name="Authentication_Token">To validate the user</param>
        /// <param name="requesturl">To log service called by which url?</param>
        /// <returns></returns>
        #region  Copying the existing function Get_FunctionalTree with little editon for calling purpose for service
        public FunctionalTreeResponse Get_FunctionalTree_Service(string Sync_Time, string VesselID, string Authentication_Token, string requesturl)
        {
            FunctionalTreeResponse objTreeRes = new FunctionalTreeResponse();

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsFunctionTreeData = DAL_JiBE.Get_FunctionalTree_DL(null, UDFLib_WCF.ConvertIntegerToNull(VesselID), null, UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, ref OutSyncTime);


                    DataTable dtFunctions = dsFunctionTreeData.Tables[0];

                    DataTable dtSystems = dsFunctionTreeData.Tables[1];

                    DataTable dtSubSystems = dsFunctionTreeData.Tables[2];

                    List<function> lstFunction = new List<function>();
                    List<FunctionalSystem> lstSystem = new List<FunctionalSystem>();
                    List<SubSystem> lstSubsystem = new List<SubSystem>();

                    foreach (DataRow dr in dtFunctions.Rows)
                    {
                        function obFunction = new function();
                        obFunction.Code = Convert.ToInt32(dr["Code"]);
                        obFunction.Description = Convert.ToString(dr["Description"]);
                        obFunction.Active_Status = Convert.ToInt32(dr["Active_Status"]);
                        obFunction.VesselID = Convert.ToInt32(dr["VesselID"]);


                        lstFunction.Add(obFunction);

                    }
                    foreach (DataRow dr in dtSystems.Rows)
                    {
                        FunctionalSystem obSystem = new FunctionalSystem();
                        obSystem.Functions = Convert.ToInt32(dr["Functions"]);
                        obSystem.SystemCode = Convert.ToInt32(dr["System_Code"]);
                        obSystem.Description = Convert.ToString(dr["Description"]);
                        obSystem.Active_Status = Convert.ToInt32(dr["Active_Status"]);
                        obSystem.LocationCode = Convert.ToInt32(dr["Location_Code"]);
                        obSystem.VesselID = Convert.ToInt32(dr["VesselID"]);

                        lstSystem.Add(obSystem);

                    }

                    foreach (DataRow dr in dtSubSystems.Rows)
                    {
                        SubSystem obSubSystem = new SubSystem();
                        obSubSystem.SubsystemCode = Convert.ToInt32(dr["Subsystem_Code"]);
                        obSubSystem.SystemCode = Convert.ToInt32(dr["System_Code"]);
                        obSubSystem.Description = Convert.ToString(dr["Description"]);
                        obSubSystem.Active_Status = Convert.ToInt32(dr["Active_Status"]);
                        obSubSystem.LocationCode = Convert.ToInt32(dr["Location_Code"]);
                        obSubSystem.VesselID = Convert.ToInt32(dr["VesselID"]);


                        lstSubsystem.Add(obSubSystem);

                    }


                    objTreeRes.Functions = lstFunction;
                    objTreeRes.Systems = lstSystem;
                    objTreeRes.Subsystems = lstSubsystem;

                    if (dtFunctions.Rows.Count > 0 || dtSystems.Rows.Count > 0 || dtSubSystems.Rows.Count > 0)
                        objTreeRes.Sync_Time = OutSyncTime;

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_FunctionalTree_Service --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_FunctionalTree_Service --  Exception - " + ex + " -- URL --" + requesturl, null, null);
            }

            return objTreeRes;
        }
        #endregion




        /// <summary>
        ///  Requirement :- Get the Checklist data
        /// </summary>
        /// <param name="VesselID">based on vessel id it provides the data</param>
        /// <param name="UserID">based on User id it provides the data</param>
        /// <param name="Sync_Time">To get the current time when last data fetched</param>
        /// <param name="Authentication_Token">To validate the user</param>
        /// <param name="requesturl">To log service called by which url?</param>
        /// <returns></returns>
        #region Copying the existing function Get_Checklist with little editon for calling purpose for service
        public ChecklistResponse Get_Checklist_Service(string VesselID, string UserID, string Sync_Time, string Authentication_Token, string requesturl)
        {
            //This region is service log region and this will trace URL log on each time when service is called.
            #region URL LOG
            //var context = WebOperationContext.Current;
            //var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            //DAL_JiBE.Service_LOG("-- URL LOG -- Get_Checklist --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserID));
            //Service_LOG this function will call service log code this will create URL log for Get_Checklist called

            #endregion

            ChecklistResponse objVslRes = new ChecklistResponse();

            List<Checklist> CHList = new List<Checklist>();
            List<ChecklistItem> chkItems = new List<ChecklistItem>();
            List<Gradings> chkGrades = new List<Gradings>();

            int? vID = UDFLib_WCF.ConvertIntegerToNull(VesselID);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataSet dsCHK = new DataSet();
                    //This will check if VesselID is null 
                    if (vID != null)
                    {
                        //If vesselID is not null then this function will be called and this will return data on basis of vesselID
                        //dsCHK = DAL_JiBE.Get_AllCheckList_DL(int.Parse(VesselID), null);
                        int? userid = Authenticated_UserId(Authentication_Token);
                        if (userid == 0)
                            userid = null;
                        if (Sync_Time.ToUpper() != "(null)".ToUpper() || Sync_Time.ToUpper() != "null".ToUpper())
                        dsCHK = DAL_JiBE.Get_AllCheckList_DL(int.Parse(VesselID), userid, UDFLib_WCF.ConvertDateToNull(Sync_Time));
                        else
                            dsCHK = DAL_JiBE.Get_AllCheckList_DL(int.Parse(VesselID), userid, null);
                    }
                    else
                    {
                        //If vesselID is null then this function will be called and this will return data on basis of UserID
                        dsCHK = DAL_JiBE.Get_AllCheckList_BY_UserID_DL(Convert.ToInt32(UserID));
                    }

                    //If dsCHK has result ie. table counts then only this will go for loop
                    if (dsCHK.Tables.Count > 0)
                    {
                        //This will fetch 1 by 1 row from dsCHK.Tables
                        foreach (DataRow dr in dsCHK.Tables[0].Rows)
                        {
                            //This will check for checklist name and its nodetype will not like Group, Location or Item 
                            if (dr["NodeType"].ToString() != "Group" && dr["NodeType"].ToString() != "Location" && dr["NodeType"].ToString() != "Item")
                            {
                                //This will add checklist details in  Checklist object.
                                Checklist obCHKList = new Checklist();
                                obCHKList.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_ID"]);
                                obCHKList.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                                obCHKList.scheduledate = Convert.ToString(dr["SheduleDate"]);
                                obCHKList.completiondate = Convert.ToString(dr["CompletionDate"]);
                                obCHKList.originalscheduledate = Convert.ToString(dr["OriginalSheduleDate"]);
                                obCHKList.inspectionstatus = Convert.ToString(dr["inspectionStatus"]);
                                obCHKList.InspectionType = Convert.ToString(dr["InspectionType"]);
                                obCHKList.checklistname = Convert.ToString(dr["CheckList_Name"]);
                                if (vID != null)
                                {
                                    obCHKList.vesselid = Convert.ToInt32(vID);
                                }
                                else
                                {
                                    obCHKList.vesselid = Convert.ToInt32(dr["Vessel_ID"]);
                                }
                                obCHKList.checklisttypename = Convert.ToString(dr["CheckList_Type_name"]);
                                obCHKList.checklisttype = UDFLib_WCF.ConvertToInteger(dr["checklistType"]);
                                obCHKList.gradingtype = UDFLib_WCF.ConvertToInteger(dr["Grading_Type"]);
                                obCHKList.vesseltypename = Convert.ToString(dr["NodeType"]);
                                obCHKList.vesseltype = UDFLib_WCF.ConvertToInteger(dr["Vessel_Type"]);
                                obCHKList.submitstatus = Convert.ToString(dr["Final_Stat"]);
                                obCHKList.inspActiveStatus = Convert.ToInt32(dr["Active_Status"]);
                                obCHKList.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                                //This will filter dsCHK table by node type as group and not null parentID and this  differentiated datatable will be passed to getrecurtionGroupParent as parameter.
                                dsCHK.Tables[0].DefaultView.RowFilter = "NodeType= 'Group' AND Parent_ID IS NULL AND Checklist_IDCopy='" + dr["Checklist_ID"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                                DataTable dtgroupDiff = dsCHK.Tables[0].DefaultView.ToTable();
                                if (dtgroupDiff.Rows.Count > 0)
                                {
                                    obCHKList.Group = getrecurtionGroupParent(dtgroupDiff, dsCHK.Tables[0], null);
                                    //This will add group data collection to checklist object.
                                }

                                //This will filter dsCHK table by node type as Location and not null parentID and this  differentiated datatable will be passed to getrecurtionLocation as parameter.
                                dsCHK.Tables[0].DefaultView.RowFilter = "NodeType= 'Location' AND Parent_ID IS NULL AND Checklist_IDCopy='" + dr["Checklist_ID"] + "' AND Inspection_ID = '" + dr["Inspection_ID"] + "'";
                                DataTable dtLocationDiff = dsCHK.Tables[0].DefaultView.ToTable();
                                if (dtLocationDiff.Rows.Count > 0)
                                {
                                    obCHKList.Location = getrecurtionLocation(dtLocationDiff, null);
                                    //This will add location data collection to checklist object under group.
                                }

                                CHList.Add(obCHKList);
                                //This is all collections are added to checklist collection object.
                            }
                            else if (dr["NodeType"].ToString() == "Item")// This will check for nodetype with value ITEM this all items are stored in checklistItem Collection and its a seperate result
                            {
                                ChecklistItem objchkItem = new ChecklistItem();
                                objchkItem.checklistid = UDFLib_WCF.ConvertToInteger(dr["Checklist_IDCopy"]);
                                objchkItem.inspectionid = Convert.ToInt32(dr["Inspection_ID"]);
                                objchkItem.checklistitemid = UDFLib_WCF.ConvertToInteger(dr["ChecklistItem_ID"]);
                                objchkItem.checklistitemname = Convert.ToString(dr["CheckList_Name"]);
                                objchkItem.nodetype = Convert.ToString(dr["NodeType"]);
                                objchkItem.nodevalue = UDFLib_WCF.ConvertDecimalToNull(dr["ItemRates"]) != null ? Convert.ToDecimal(dr["ItemRates"]) : 0;
                                objchkItem.gradeid = UDFLib_WCF.ConvertToInteger(dr["ItemGrading_Type"]);
                                objchkItem.locationid = UDFLib_WCF.ConvertToInteger(dr["Parent_ID"]);
                                objchkItem.index = UDFLib_WCF.ConvertToInteger(dr["Index_No"]);

                                chkItems.Add(objchkItem);
                                //checklist item collection is added to checklist collection object.
                            }

                        }

                        //This Get_LocGradeType_DL will give you all the grading types
                        DataSet dsGrading = DAL_JiBE.Get_LocGradeType_DL();

                        //This will fetch row by row data from dsGrading table and add to Gradings objects and its collection object
                        foreach (DataRow dr in dsGrading.Tables[0].Rows)
                        {
                            Gradings obGradings = new Gradings();
                            obGradings.gradenames = Convert.ToString(dr["Grade_Name"]);
                            obGradings.gradeid = UDFLib_WCF.ConvertToInteger(dr["ID"]);
                            obGradings.optiontext = Convert.ToString(dr["OptionText"]);
                            obGradings.optionvalue = dr["OptionValue"].ToString() == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(dr["OptionValue"]);

                            chkGrades.Add(obGradings);
                            //Gradings objects added to collection object 1 by 1.
                        }

                        objVslRes.CheckList = CHList;
                        objVslRes.CheckListItems = chkItems;
                        objVslRes.ObjectGradesList = chkGrades;
                    }
                }
                else
                {
                    //This region is service log region and this will trace URL log authentication failiure.
                    #region URL LOG Authentication Failiure
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Checklist_Service --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(UserID));
                    //Service_LOG this function will call service log code for authentication failiure this will create URL log for Get_Checklist called
                    #endregion
                }

            }
            catch (Exception ex)
            {
                //This region is service log region and this will trace URL log on exception.
                #region URL LOG Exception  log
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_Checklist_Service --  Exception - " + ex + " -- URL --" + requesturl + " -- Exception Log -- " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(UserID));
                #endregion
            }

            return objVslRes;
            //This will return response collection of checklist,checklist Items,Gradings to phone.
        }
        #endregion






        ///// <summary>
        ///// This method "Zip" is use to make directory as zip folder
        ///// </summary>
        ///// <param name="path">From whre it will look for folder to make it zip</param>
        ///// <param name="filename">What name you want to give, will be set here</param>
        //public static void Zip(string path, string filename)
        //{
        //    //Create Bat file
        //    //string[] filenames = filename.Split('.');
        //    string batfilename = Path.Combine(path, filename + ".bat");
        //    string[] FoldertoCompress = path.Split('\\');
        //    try
        //    {
        //        TextWriter tw = new StreamWriter(batfilename);
        //        tw.WriteLine("c:");
        //        tw.WriteLine(@"cd\");
        //        tw.WriteLine("cd " + path);
        //        tw.WriteLine("rar a -d " + filename + ".zip " + filename);
        //        //tw.WriteLine("zip -r " + filename + ".zip " + filename);
        //        //tw.WriteLine("rar a -r " + filename + ".rar " + filename);
        //        tw.Close();

        //        // create cmd process and execute bat file
        //        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        //        proc.EnableRaisingEvents = true;
        //        proc.StartInfo.FileName = batfilename;
        //        proc.StartInfo.CreateNoWindow = true;
        //        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //        proc.Start();
        //        proc.WaitForExit();
        //        proc.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Delete bat file
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //Delete bat file
        //        if (File.Exists(batfilename))
        //            File.Delete(batfilename);
        //        if (File.Exists(Path.Combine(path, filename + ".txt")))
        //            File.Delete(Path.Combine(path, filename + ".txt"));
        //    }


        //}



        /// <summary>
        /// This method "Zip" is use to make directory as zip folder 
        /// Author - Pranav Sakpal created on 20-10-2016
        /// </summary>
        /// <param name="path">From whre it will look for folder to make it zip</param>
        /// <param name="filename">What name you want to give, will be set here</param>
        public static void Zip(string path, string filename)
        {
            string[] strArrPath = path.Split('\\');
            string strOutputPath = "";
            for (int i = 0; i < strArrPath.Length - 1; i++)
            {
                if (i == 0)
                    strOutputPath = strArrPath[i];
                else
                    strOutputPath = strOutputPath + "\\" + strArrPath[i];
            }
            string strCurrentDirectoryName = strArrPath[strArrPath.Length - 1];

            using (Package package = ZipPackage.Open(strOutputPath + "\\" + strCurrentDirectoryName + ".zip", FileMode.Create))
            {
                foreach (string currentFile in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    Console.WriteLine("Packing " + currentFile);
                    Uri relUri = GetRelativeUri(currentFile, strCurrentDirectoryName);

                    PackagePart packagePart = package.CreatePart(relUri, System.Net.Mime.MediaTypeNames.Application.Octet, CompressionOption.Maximum);
                    using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read))
                    {
                        CopyStream(fileStream, packagePart.GetStream());
                    }
                }
            }


        }

        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 16384;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }

        private static Uri GetRelativeUri(string currentFile, string cuttingPath)
        {
            string relPath = currentFile.Substring(currentFile.IndexOf('\\')).Replace('\\', '/').Replace(' ', '_');
            string[] strArr = relPath.Split('/');
            int j = 0;
            string relPathTemp = "";
            for (int i = 0; i < strArr.Length; i++)
            {
                if (strArr[i] == cuttingPath)
                {
                    j = i;
                }

                if (j != 0)
                {
                    if (j < i)
                    {
                        relPathTemp = relPathTemp + "/" + strArr[i];
                    }
                }
            }



            return new Uri(RemoveAccents(relPathTemp), UriKind.Relative);
            //return new Uri(RemoveAccents(relPath), UriKind.Relative);
        }

        private static string RemoveAccents(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormKD);
            Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage, new EncoderReplacementFallback(""), new DecoderReplacementFallback(""));
            byte[] bytes = removal.GetBytes(normalized);
            return Encoding.ASCII.GetString(bytes);
        }



        #region Download All Vessel Data Zip
        //public Stream Download_Zip_Data_byVessel(string VesselID)

        //static string DataFolder = "";
        /// <summary>
        /// This Method "Download_Zip_Data_byVessel" is created by Hadish with the help of pranav on 13-10-2016
        /// Requirement: Its for post service that returns zip file containing data like activity,crewlist,functional tree and checklist 
        /// </summary>
        /// <param name="Authentication_Token">To validate the login user</param>
        /// <param name="VesselID">For which vessel the data should be come out</param>
        /// <param name="Sync_Time">To get the records by current date and time</param>
        /// <param name="FN_SYNC_TIME">To get the records of functional tree by current date and time </param>
        /// <param name="CRW_SYN_TIME">To get the records of crew list by current date and time</param>
        /// <param name="CHKLIST_SYN_TIME">To get the records of Check list by current date and time</param>
        /// <returns></returns>
        
        public Stream Download_Zip_Data_byVessel(string Authentication_Token, string VesselID, string Sync_Time, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME)
        {
            string DataFolder = "";
            //This region is service log region and this will trace URL log on each time when service is called.
            #region URL LOG
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Download_Zip_Data_byVessel --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(Authenticated_UserId(Authentication_Token)));
            //Service_LOG this function will call service log code this will create URL log for Get_Checklist called
            #endregion

            FileStream StreamedFile = null;

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    Guid GUID = Guid.NewGuid();            //It give unique random alphanumeric long name
                    string GlobalUniqID = GUID.ToString();  // Storing that unique name in variable
                    string FilePath = Path.Combine(ConfigurationManager.AppSettings["ServiceData"].ToString(), GlobalUniqID);  //Retreiving base folder path of ServiceData ,GlobalUniqID is optional you can pass an empty string
                    string FullFilename = FilePath;  //Storing that path inside a variable 
                    if (!Directory.Exists(Path.GetDirectoryName(FullFilename)))  //Checking whether ServiceData Folder is present or not ,if not then it will create 
                        Directory.CreateDirectory(Path.GetDirectoryName(FullFilename));

                    string RandomFolder = Path.GetDirectoryName(FullFilename); //Next 3 line written For creating random folder ,first retrive the path 
                    RandomFolder = RandomFolder + @"\" + GlobalUniqID;
                    Directory.CreateDirectory(RandomFolder);

                    //Inside Random folder we want another folder named as DataFolder                  
                    string RandFoldName = Path.GetFileName(RandomFolder);       //Next 3 line will create folder with name DataFolder
                    DataFolder = RandomFolder + @"\" + "DataFolder";
                    if (!Directory.Exists(DataFolder))
                        Directory.CreateDirectory(DataFolder);

                    //Inside DataFolder folder we want another two folder named as Data and Attachment     
                    string Data = DataFolder + @"\" + "Data";
                    string Attachment = DataFolder + @"\" + "Attachment";
                    string Technical = "", Crew = "";
                    if (!Directory.Exists(Data) && !Directory.Exists(Attachment))//Checking whether Data and Attachment folder exists or not if not then create 
                    {
                        Directory.CreateDirectory(Data);
                        Directory.CreateDirectory(Attachment);
                        Technical = Attachment + @"\" + "Technical";
                        Crew = Attachment + @"\" + "Crew";

                        if (!Directory.Exists(Technical) && !Directory.Exists(Crew))
                        {
                            Directory.CreateDirectory(Technical);
                            Directory.CreateDirectory(Crew);
                        }
                    }

                    //For Activty List
                    //Calling method specially for service and getting Acitivty list data
                    ActivityListResponse objActivityList = new ActivityListResponse();
                    //objActivityList = GetActivityList_Service("91", null, "1F46DCEA-C786-40C2-91E1-BAD81FC86C86", ""); This commented line is to quick check for testing purpose
                    objActivityList = GetActivityList_Service(VesselID, Sync_Time, Authentication_Token, "");
                    //GetActivityList_Service returns 3 list with data, Cteating list object for each result set in list 
                    List<object> listobjActivityList = objActivityList.ActivityList.Cast<object>().ToList();
                    List<object> listobjObjectList = objActivityList.ActivityObjectList.Cast<object>().ToList();
                    List<object> listobjFollowupList = objActivityList.FollowupList.Cast<object>().ToList();

                    //To convert list data into json, next 2 line is useful
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var jsonAcitvity = serializer.Serialize(listobjActivityList);
                    var jsonAcitvity_Objects = serializer.Serialize(listobjObjectList);
                    var jsonAcitvity_followups = serializer.Serialize(listobjFollowupList);



                    //For Crew List
                    //Doing same thing for crew user list and storing its data into json file
                    //CrewUserListResponse objCrewUserList = Get_CrewUser_List_Service("91", null, "1F46DCEA-C786-40C2-91E1-BAD81FC86C86", ""); This commented line is to quick check for testing purpose
                    CrewUserListResponse objCrewUserList = Get_CrewUser_List_Service(VesselID, CRW_SYN_TIME, Authentication_Token, "");
                    List<object> objCrewList = objCrewUserList.CrewUserList.Cast<object>().ToList();
                    objCrewList = objCrewUserList.CrewUserList.Cast<object>().ToList();
                    var jsonCrewList = serializer.Serialize(objCrewList);



                    //For Functional List
                    //Doing same thing for functional and storing its data into json file
                    FunctionalTreeResponse objFunTree = new FunctionalTreeResponse();
                    //objFunTree = Get_FunctionalTree_Service(null, "91", "1F46DCEA-C786-40C2-91E1-BAD81FC86C86", ""); This commented line is to quick check for testing purpose
                    objFunTree = Get_FunctionalTree_Service(FN_SYNC_TIME, VesselID, Authentication_Token, "");
                    List<object> FunctionalList = objFunTree.Functions.Cast<object>().ToList();
                    List<object> objSystem = objFunTree.Systems.Cast<object>().ToList();
                    List<object> objSubSystem = objFunTree.Subsystems.Cast<object>().ToList();

                    //Get_FunctionalTree_Service returns 3 list with data so storing in 3 differnt json files
                    var jsonFunctioanlTree = serializer.Serialize(FunctionalList);
                    var jsonSystem = serializer.Serialize(objSystem);
                    var jsonSubSystem = serializer.Serialize(objSubSystem);



                    //For Check List
                    //Doing same thing for Checklist and storing its data into json file
                    ChecklistResponse objChecklistResponse = new ChecklistResponse();

                    int? user_id =  Authenticated_UserId(Authentication_Token);
                    if (user_id == 0)
                        user_id = null;
                    objChecklistResponse = Get_Checklist_Service(VesselID, user_id.ToString(), CHKLIST_SYN_TIME, Authentication_Token, "");
                    List<object> CheckList = objChecklistResponse.CheckList.Cast<object>().ToList();
                    List<object> ObjCheckListItem = objChecklistResponse.CheckListItems.Cast<object>().ToList();
                    List<object> ObjCheckListGradesList = objChecklistResponse.ObjectGradesList.Cast<object>().ToList();
                    var jsonCheckList = serializer.Serialize(CheckList);
                    var jsonCheckListItem = serializer.Serialize(ObjCheckListItem);
                    var jsonCheckListGradList = serializer.Serialize(ObjCheckListGradesList);

                    //Inside Data folder our json files should be create with some formatted text {\"GetActivityListResult\": {\"ActivityList\ etc ae just to format as simple text
                    File.WriteAllText(@Data + @"\" + "Activity.json", "{\"GetActivityListResult\": {\"ActivityList\":  " + jsonAcitvity.ToString() + ", \"ActivityObjectList\": " + jsonAcitvity_Objects.ToString() + ", \"FollowupList\": " + jsonAcitvity_followups.ToString() + ", \"Sync_Time\": \"" + objActivityList.Sync_Time + "\"}}");
                    File.WriteAllText(@Data + @"\" + "CrewList.json", "{\"Get_CrewUser_ListResult\": {\"CrewUserList\":  " + jsonCrewList.ToString() + ",  \"Sync_Time\": \"" + objCrewUserList.Sync_Time + "\"}}");
                    File.WriteAllText(@Data + @"\" + "FunctionalTree.json", "{\"Get_FunctionalTreeResult\": {\"Functions\":  " + jsonFunctioanlTree.ToString() + ",\"Systems\":" + jsonSystem.ToString() + ",\"Subsystems\":" + jsonSubSystem.ToString() + ",\"Sync_Time\" :\"" + objFunTree.Sync_Time + "\"}}");
                    File.WriteAllText(@Data + @"\" + "CheckList.json", "{\"Get_ChecklistResult\":{\"CheckList\":  " + jsonCheckList.ToString() + ",\"CheckListItems\": " + jsonCheckListItem.ToString() + ", \"ObjectGradesList\":" + jsonCheckListGradList.ToString() + ",\"Sync_Time\" :\"" + objChecklistResponse.Sync_Time + "\"}}");




                    string PathConcat = "", PathConcatCrew = "";

                    //Copy To porcess, This loop is Checking if PathConcat folder having same file/Attachment then it will copy file to Technical folder
                    for (int i = 0; i < objActivityList.ActivityObjectList.Count; i++)
                    {
                        PathConcat = Path.Combine(ConfigurationManager.AppSettings["InspectionAttachmentPath"].ToString(), objActivityList.ActivityObjectList[i].imageaudioPath);//

                        if (File.Exists(PathConcat))
                        {
                            if (System.IO.Directory.Exists(@Technical))
                            {
                                File.Copy(PathConcat, @Technical + @"\" + objActivityList.ActivityObjectList[i].imageaudioPath, true);
                            }

                        }
                    }

                    //Copy To porcess, This loop is  Checking if PathConcatCrew folder having same file/Crew image then it will copy file to Crew folder
                    for (int i = 0; i < objCrewUserList.CrewUserList.Count; i++)
                    {
                        PathConcatCrew = Path.Combine(ConfigurationManager.AppSettings["CrewProfilePicPath"].ToString(), objCrewUserList.CrewUserList[i].ProfilePicPath);//

                        if (File.Exists(PathConcatCrew))
                        {
                            if (System.IO.Directory.Exists(@Crew))
                            {
                                File.Copy(PathConcatCrew, @Crew + @"\" + objCrewUserList.CrewUserList[i].ProfilePicPath, true);
                            }

                        }
                    }


                    Zip(RandomFolder, "DataFolder"); //To make zip folder 
                    //StreamedFile = File.OpenRead(Path.GetDirectoryName(DataFolder) + "\\" + Path.GetFileNameWithoutExtension(DataFolder) + ".zip ");
                    string[] strArrPath = RandomFolder.Split('\\');
                    string strOutputPath = "";
                    for (int i = 0; i < strArrPath.Length - 1; i++)
                    {
                        if (i == 0)
                            strOutputPath = strArrPath[i];
                        else
                            strOutputPath = strOutputPath + "\\" + strArrPath[i];
                    }
                    string strCurrentDirectoryName = strArrPath[strArrPath.Length - 1];
                    StreamedFile = File.OpenRead(strOutputPath + "\\" + strCurrentDirectoryName + ".zip");
                    //StreamedFile = File.OpenRead(Path.GetDirectoryName(DataFolder) + "\\" + Path.GetFileNameWithoutExtension(DataFolder) + ".rar ");

                    //Following commented code can be useful in future hence let it be there
                    //var fileName = Path.GetFileName(DataFolder); 
                    //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                    //WebOperationContext.Current.OutgoingResponse.Headers.Add("content-disposition", "inline; filename=" + "DataFolder" + ".zip");

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Download_Zip_Data_byVessel --  Token - " + Authentication_Token + " -- URL --" + "", null, null);
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception  LOG -- Download_Zip_Data_byVessel --  Exeception - " + ex.ToString() + " -- URL --" + "", null, null);
            }

            return StreamedFile;
        }

        /// <summary>
        /// This function is used to get zipfileDetails 
        /// </summary>
        /// <param name="Authentication_Token">Valid user token</param>
        /// <param name="VesselID">valid vessel ID</param>
        /// <param name="Sync_Time">Valid sync time for the vessel</param>
        /// <param name="FN_SYNC_TIME"></param>
        /// <param name="CRW_SYN_TIME"></param>
        /// <param name="CHKLIST_SYN_TIME"></param>
        /// <returns>zipfileDetails  like zip file name ,data size ,vessel ID</returns>
        public DownloadZip_Result Get_zipFileDetails(string VesselID, string Sync_Time, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME, string Authentication_Token)
        //public void Get_zipFileDetails(string VesselID, string Sync_Time, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME, string Authentication_Token)
        {
            string DataFolder = "";
            DownloadZip_Result objResult = new DownloadZip_Result();
            //This region is service log region and this will trace URL log on each time when service is called.
            #region URL LOG
            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_zipFileDetails --  " + requesturl, null, UDFLib_WCF.ConvertIntegerToNull(Authenticated_UserId(Authentication_Token)));
            //Service_LOG this function will call service log code this will create URL log for Get_Checklist called
            #endregion

            FileStream StreamedFile = null;

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    Guid GUID = Guid.NewGuid();            //It give unique random alphanumeric long name
                    string GlobalUniqID = GUID.ToString();  // Storing that unique name in variable
                    string FilePath = Path.Combine(ConfigurationManager.AppSettings["ServiceData"].ToString(), GlobalUniqID);  //Retreiving base folder path of ServiceData ,GlobalUniqID is optional you can pass an empty string
                    string FullFilename = FilePath;  //Storing that path inside a variable 
                    if (!Directory.Exists(Path.GetDirectoryName(FullFilename)))  //Checking whether ServiceData Folder is present or not ,if not then it will create 
                        Directory.CreateDirectory(Path.GetDirectoryName(FullFilename));

                    string RandomFolder = Path.GetDirectoryName(FullFilename); //Next 3 line written For creating random folder ,first retrive the path 
                    RandomFolder = RandomFolder + @"\" + GlobalUniqID;
                    Directory.CreateDirectory(RandomFolder);

                    //Inside Random folder we want another folder named as DataFolder                  
                    string RandFoldName = Path.GetFileName(RandomFolder);       //Next 3 line will create folder with name DataFolder
                    DataFolder = RandomFolder + @"\" + "DataFolder";
                    if (!Directory.Exists(DataFolder))
                        Directory.CreateDirectory(DataFolder);

                    //Inside DataFolder folder we want another two folder named as Data and Attachment     
                    string Data = DataFolder + @"\" + "Data";
                    string Attachment = DataFolder + @"\" + "Attachment";
                    string Technical = "", Crew = "";
                    if (!Directory.Exists(Data) && !Directory.Exists(Attachment))//Checking whether Data and Attachment folder exists or not if not then create 
                    {
                        Directory.CreateDirectory(Data);
                        Directory.CreateDirectory(Attachment);
                        Technical = Attachment + @"\" + "Technical";
                        Crew = Attachment + @"\" + "Crew";

                        if (!Directory.Exists(Technical) && !Directory.Exists(Crew))
                        {
                            Directory.CreateDirectory(Technical);
                            Directory.CreateDirectory(Crew);
                        }
                    }

                    //For Activty List
                    //Calling method specially for service and getting Acitivty list data
                    ActivityListResponse objActivityList = new ActivityListResponse();
                    objActivityList = GetActivityList_Service(VesselID, Sync_Time, Authentication_Token, "");
                    List<object> listobjActivityList = objActivityList.ActivityList.Cast<object>().ToList();
                    List<object> listobjObjectList = objActivityList.ActivityObjectList.Cast<object>().ToList();
                    List<object> listobjFollowupList = objActivityList.FollowupList.Cast<object>().ToList();

                    //To convert list data into json, next 2 line is useful
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var jsonAcitvity = serializer.Serialize(listobjActivityList);
                    var jsonAcitvity_Objects = serializer.Serialize(listobjObjectList);
                    var jsonAcitvity_followups = serializer.Serialize(listobjFollowupList);



                    //For Crew List
                    //Doing same thing for crew user list and storing its data into json file
                    CrewUserListResponse objCrewUserList = Get_CrewUser_List_Service(VesselID, CRW_SYN_TIME, Authentication_Token, "");
                    List<object> objCrewList = objCrewUserList.CrewUserList.Cast<object>().ToList();
                    objCrewList = objCrewUserList.CrewUserList.Cast<object>().ToList();
                    var jsonCrewList = serializer.Serialize(objCrewList);



                    //For Functional List
                    //Doing same thing for functional and storing its data into json file
                    FunctionalTreeResponse objFunTree = new FunctionalTreeResponse();
                    objFunTree = Get_FunctionalTree_Service(FN_SYNC_TIME, VesselID, Authentication_Token, "");
                    List<object> FunctionalList = objFunTree.Functions.Cast<object>().ToList();
                    List<object> objSystem = objFunTree.Systems.Cast<object>().ToList();
                    List<object> objSubSystem = objFunTree.Subsystems.Cast<object>().ToList();

                    //Get_FunctionalTree_Service returns 3 list with data so storing in 3 differnt json files
                    var jsonFunctioanlTree = serializer.Serialize(FunctionalList);
                    var jsonSystem = serializer.Serialize(objSystem);
                    var jsonSubSystem = serializer.Serialize(objSubSystem);



                    //For Check List
                    //Doing same thing for Checklist and storing its data into json file
                    ChecklistResponse objChecklistResponse = new ChecklistResponse();

                    int? user_id = Authenticated_UserId(Authentication_Token);
                    if (user_id == 0)
                        user_id = null;
                    objChecklistResponse = Get_Checklist_Service(VesselID, user_id.ToString(), CHKLIST_SYN_TIME, Authentication_Token, "");
                    List<object> CheckList = objChecklistResponse.CheckList.Cast<object>().ToList();
                    List<object> ObjCheckListItem = objChecklistResponse.CheckListItems.Cast<object>().ToList();
                    List<object> ObjCheckListGradesList = objChecklistResponse.ObjectGradesList.Cast<object>().ToList();
                    var jsonCheckList = serializer.Serialize(CheckList);
                    var jsonCheckListItem = serializer.Serialize(ObjCheckListItem);
                    var jsonCheckListGradList = serializer.Serialize(ObjCheckListGradesList);

                    //Inside Data folder our json files should be create with some formatted text {\"GetActivityListResult\": {\"ActivityList\ etc ae just to format as simple text
                    File.WriteAllText(@Data + @"\" + "Activity.json", "{\"GetActivityListResult\": {\"ActivityList\":  " + jsonAcitvity.ToString() + ", \"ActivityObjectList\": " + jsonAcitvity_Objects.ToString() + ", \"FollowupList\": " + jsonAcitvity_followups.ToString() + ", \"Sync_Time\": \"" + objActivityList.Sync_Time + "\"}}");
                    File.WriteAllText(@Data + @"\" + "CrewList.json", "{\"Get_CrewUser_ListResult\": {\"CrewUserList\":  " + jsonCrewList.ToString() + ",  \"Sync_Time\": \"" + objCrewUserList.Sync_Time + "\"}}");
                    File.WriteAllText(@Data + @"\" + "FunctionalTree.json", "{\"Get_FunctionalTreeResult\": {\"Functions\":  " + jsonFunctioanlTree.ToString() + ",\"Systems\":" + jsonSystem.ToString() + ",\"Subsystems\":" + jsonSubSystem.ToString() + ",\"Sync_Time\" :\"" + objFunTree.Sync_Time + "\"}}");
                    File.WriteAllText(@Data + @"\" + "CheckList.json", "{\"Get_ChecklistResult\":{\"CheckList\":  " + jsonCheckList.ToString() + ",\"CheckListItems\": " + jsonCheckListItem.ToString() + ", \"ObjectGradesList\":" + jsonCheckListGradList.ToString() + ",\"Sync_Time\" :\"" + objChecklistResponse.Sync_Time + "\"}}");




                    string PathConcat = "", PathConcatCrew = "";

                    //Copy To porcess, This loop is Checking if PathConcat folder having same file/Attachment then it will copy file to Technical folder
                    for (int i = 0; i < objActivityList.ActivityObjectList.Count; i++)
                    {
                        PathConcat = Path.Combine(ConfigurationManager.AppSettings["InspectionAttachmentPath"].ToString(), objActivityList.ActivityObjectList[i].imageaudioPath);//

                        if (File.Exists(PathConcat))
                        {
                            if (System.IO.Directory.Exists(@Technical))
                            {
                                File.Copy(PathConcat, @Technical + @"\" + objActivityList.ActivityObjectList[i].imageaudioPath, true);
                            }

                        }
                    }

                    //Copy To porcess, This loop is  Checking if PathConcatCrew folder having same file/Crew image then it will copy file to Crew folder
                    for (int i = 0; i < objCrewUserList.CrewUserList.Count; i++)
                    {
                        PathConcatCrew = Path.Combine(ConfigurationManager.AppSettings["CrewProfilePicPath"].ToString(), objCrewUserList.CrewUserList[i].ProfilePicPath);//

                        if (File.Exists(PathConcatCrew))
                        {
                            if (System.IO.Directory.Exists(@Crew))
                            {
                                File.Copy(PathConcatCrew, @Crew + @"\" + objCrewUserList.CrewUserList[i].ProfilePicPath, true);
                            }

                        }
                    }


                    Zip(RandomFolder, "DataFolder"); //To make zip folder 
                    
                    string[] strArrPath = RandomFolder.Split('\\');
                    string strOutputPath = "";
                    for (int i = 0; i < strArrPath.Length - 1; i++)
                    {
                        if (i == 0)
                            strOutputPath = strArrPath[i];
                        else
                            strOutputPath = strOutputPath + "\\" + strArrPath[i];
                    }
                    string strCurrentDirectoryName = strArrPath[strArrPath.Length - 1];
                  

                    FileInfo objInfo = new FileInfo(strOutputPath + "\\" + strCurrentDirectoryName + ".zip");// This changes done instead of reading from stream file used file info to get file information.
                     
                    string len = objInfo.Length.ToString();

                    objResult.datasize = len;
                    objResult.filename = strCurrentDirectoryName + ".zip";
                    objResult.vesselid = VesselID;

               
                    
                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_zipFileDetails --  Token - " + Authentication_Token + " -- URL --" + "", null, null);
                    objResult.error = "Authentication Fail";
                }
            }
            catch (Exception ex)
            {
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_zipFileDetails --  Exeception - " + ex.ToString() + " -- URL --" + "", null, null);
                objResult.error = "Exception - " + ex.Message.ToString();
            }
            
            
            return objResult;
     
            
        }

        #endregion

        #region To find the user id based on authentication token
        private int  Authenticated_UserId(string Authentication_Token)
        {
            int UserId = 0;

            try
            {
                UserId = Convert.ToInt32(DAL_JiBE.Get_Authentication_UserID(Authentication_Token).Rows[0]["User_ID"]);
            }
            catch { }

            return UserId;
        }
        #endregion

        /// <summary>
        /// This service will used to get configured alert list.
        /// </summary>
        /// <param name="Authentication_Token">Valid Authentication Token</param>
        /// <returns>This will result alerts Dictionary </returns>
        public Configurable_Alerts_Result Get_Config_Alerts(string Authentication_Token)
        {
            Configurable_Alerts_Result objResult = new Configurable_Alerts_Result();
            List<Configurable_Alerts> objAlertList = new List<Configurable_Alerts>();
            
            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
                DAL_JiBE.Service_LOG("-- URL LOG -- Get_Config_Alerts --  " + requesturl, null, null);

                if (Authenticate(Authentication_Token))
                {
                    DataSet dsAlerts = DAL_JiBE.Get_Config_ALerts(Authentication_Token);

                    if (dsAlerts.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dsAlerts.Tables[0].Rows)
                        {
                            Configurable_Alerts objAlert = new Configurable_Alerts();
                            objAlert.id = Convert.ToInt32(dr["ID"]);
                            objAlert.userid = Convert.ToInt32(dr["User_ID"]);
                            objAlert.message = Convert.ToString(dr["Message"]);
                            objAlert.buttontxt1 = Convert.ToString(dr["Button1txt1"]);
                            objAlert.buttontxt2 = Convert.ToString(dr["Button1txt2"]);
                            objAlert.prompt_type = Convert.ToInt32(dr["Prompt_type"]);

                            objAlertList.Add(objAlert);
                        }
                    }

                    objResult.alerts_List = objAlertList;

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Config_Alerts --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception LOG - Get_Config_Alerts -  " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return objResult;
        }

        /// <summary>
        /// This service will update deliver flag for alert.
        /// </summary>
        /// <param name="AlertsID">Valid Alert ids comma seperated values</param>
        /// <param name="Authentication_Token">Valid Authentication Token</param>
        /// <returns>this will result alert id list comma seperated and status as true/false.</returns>
        public Updated_Configurable_Alerts_rec UPD_Alerts_Received(string AlertsID, string Authentication_Token)
        {
            Updated_Configurable_Alerts_rec objResult = new Updated_Configurable_Alerts_rec();

            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
                DAL_JiBE.Service_LOG("-- URL LOG -- UPD_Alerts_Received --  " + requesturl, null, null);

                if (Authenticate(Authentication_Token))
                {
                    int op = DAL_JiBE.Upd_Alerts_Received(AlertsID, Authentication_Token);

                    objResult.id = AlertsID;
                    if (op > 0)
                        objResult.status = "true";
                    else
                        objResult.status = "False";

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- UPD_Alerts_Received --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception LOG - UPD_Alerts_Received -  " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return objResult;
        }

        /// <summary>
        /// This service will update action flag for alert.
        /// </summary>
        /// <param name="AlertsID">Valid Alert id </param>
        /// <param name="Authentication_Token">Valid Authentication Token</param>
        /// <returns>It will result alertid and result as true/false.</returns>
        public Updated_Configurable_Alerts_Action UPD_Alerts_Action(string AlertsID, string Authentication_Token)
        {
            Updated_Configurable_Alerts_Action objResult = new Updated_Configurable_Alerts_Action();

            try
            {
                var context = WebOperationContext.Current;
                var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
                DAL_JiBE.Service_LOG("-- URL LOG -- UPD_Alerts_Action --  " + requesturl, null, null);

                if (Authenticate(Authentication_Token))
                {
                    int op = DAL_JiBE.Upd_Alerts_Action(Convert.ToInt32(AlertsID), Authentication_Token);

                    objResult.id = Convert.ToInt32(AlertsID);
                    if (op == 1)
                        objResult.status = "true";
                    else
                        objResult.status = "False";

                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- UPD_Alerts_Action --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception LOG - UPD_Alerts_Action -  " + ex.Message, null, UDFLib_WCF.ConvertIntegerToNull(null));
            }

            return objResult;
        }

        /// <summary>
        /// Requirement :- Get the Vessel data by type and SynTIme
        /// Edited by :- Pranav sakpal on 17-Jan-2017
        /// Purpose :-Shows assigned vessels and their attachement size as result.
        /// </summary>
        /// <param name="Sync_Time">To get the current time when last data fetched</param>
        /// <param name="Authentication_Token">To validate the user</param>
        /// <param name="FN_SYNC_TIME">To get the current time when last data fetched of functions</param>
        /// <param name="CRW_SYN_TIME">To get the current time when last data of crew</param>
        /// <param name="CHKLIST_SYN_TIME">To get the current time when last data fetched of cehcklist</param>
        /// <param name="VesselsAndSynctimes">This parameter is list of vesselId and synctime</param>
        /// <returns></returns>
        public VesselCategoryTypeResponse Get_Vessels_By_Type_MultiVesselSync(string Sync_Time, string Authentication_Token, string AC_SYNC_TIME, string FN_SYNC_TIME, string CRW_SYN_TIME, string CHKLIST_SYN_TIME,List<VesselIDs_Synctimes> VesselsAndSynctimes)
        {

            VesselCategoryTypeResponse objVslRes = new VesselCategoryTypeResponse();

            List<VesselTypeList> VslTypeList = new List<VesselTypeList>();

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_Vessels_By_Type_MultiVesselSync --  " + requesturl, null, null);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(Authentication_Token))
                {
                    DataTable dtTempInput = new DataTable();
                    dtTempInput.Columns.Add("VesselID");
                    dtTempInput.Columns.Add("Synctime");

                    List<VesselIDs_Synctimes> VesselsAndSynctimeList = VesselsAndSynctimes;
                    if (VesselsAndSynctimeList != null)
                    {
                        foreach (VesselIDs_Synctimes obj in VesselsAndSynctimeList)
                        {
                            dtTempInput.Rows.Add(obj.vesselid, obj.synctime);
                            dtTempInput.AcceptChanges();
                        }
                    }

                    DataSet dsVslInsp = DAL_JiBE.Get_Vessels_By_Type_SyncTime(UDFLib_WCF.ConvertDateToNull(Sync_Time), Authentication_Token, UDFLib_WCF.ConvertDateToNull(AC_SYNC_TIME), UDFLib_WCF.ConvertDateToNull(FN_SYNC_TIME), UDFLib_WCF.ConvertDateToNull(CRW_SYN_TIME), UDFLib_WCF.ConvertDateToNull(CHKLIST_SYN_TIME), ref OutSyncTime, dtTempInput);
                    if (dsVslInsp != null && dsVslInsp.Tables.Count > 0)
                    {

                        DataTable dtVesselType = dsVslInsp.Tables[0];//That will store vessel Type data from dsVslInsp to dtVesselType

                        DataTable dtvsl = dsVslInsp.Tables[1]; // dtvsl will have vessel data

                        DataTable dtGA = dsVslInsp.Tables[3]; //dtGA will have GA list

                        foreach (DataRow drVslType in dtVesselType.Rows)
                        {
                            VesselTypeList objVslType = new VesselTypeList();

                            List<ObjectGAType> ObjGAList = new List<ObjectGAType>();

                            List<VesselCategoryType> VslList = new List<VesselCategoryType>();

                            DataRow[] drObjGA = dtGA.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");

                            foreach (DataRow drGA in drObjGA)
                            {
                                ObjectGAType objGA = new ObjectGAType();

                                objGA.ChildID = Convert.ToString(drGA["Path_ID"]);
                                objGA.Image_Path = Convert.ToString(drGA["Image_Path"]);
                                objGA.SVG_Path = Convert.ToString(drGA["SVG_Path"]);
                                objGA.Parent_ID = Convert.ToString(drGA["Parent_Path_ID"]);
                                objGA.ChildName = Convert.ToString(drGA["Path_Name"]);

                                ObjGAList.Add(objGA);
                            }


                            DataRow[] drObjVsl = dtvsl.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");
                            foreach (DataRow dr in drObjVsl)
                            {
                                VesselCategoryType obvsl = new VesselCategoryType();
                                obvsl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                                obvsl.Vessel_ID = UDFLib_WCF.ConvertToInteger(dr["Vessel_ID"]);
                                obvsl.Attachment_Size = dr["Attachment_Size"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["Attachment_Size"]);

                                if (dr["ACTIVE_STATUS"].ToString() == "0")
                                {
                                    obvsl.Active_Status = 0;
                                }
                                else if (dr["ACTIVE_STATUS"].ToString() == "1")
                                {
                                    obvsl.Active_Status = 1;
                                }

                                VslList.Add(obvsl);

                            }

                            objVslType.VesselList = VslList;
                            objVslType.GAList = ObjGAList;
                            objVslType.Vessel_TypeID = Convert.ToInt32(drVslType["vessel_TypeID"]);
                            objVslType.Vessel_TypeName = Convert.ToString(drVslType["VesselTypes"]);

                            VslTypeList.Add(objVslType);
                        }


                        objVslRes.VesselTypeList = VslTypeList;
                        if (dtVesselType.Rows.Count > 0 && (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0))

                            objVslRes.Sync_Time = OutSyncTime;
                    }
                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Vessels_By_Type_MultiVesselSync --  Token - " + Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {
               
                DAL_JiBE.Service_LOG("Exception  LOG -- Get_Vessels_By_Type_MultiVesselSync --  Exception - " + ex.ToString() + requesturl, null, null);
            }

            return objVslRes;

        }

        /// <summary>
        /// Requirement :- Get the Vessel data by type and SynTIme
        /// Edited by :- Pranav sakpal on 20-Jan-2017
        /// Purpose :-Shows assigned vessels and their attachement size as result.
        /// </summary>
        /// <param name="VesselsAndSynctimes">clss object with all required propertieds</param>
        /// <returns>vessel list with attachment size for all vessel.</returns>
        public VesselCategoryTypeResponse Get_Vessels_By_Type_MultiVesselSync_Test(VesselIDs_Synctimes_Input VesselsAndSynctimes)
        {

            VesselCategoryTypeResponse objVslRes = new VesselCategoryTypeResponse();
            VesselIDs_Synctimes_Input obj1 = VesselsAndSynctimes;

            List<VesselTypeList> VslTypeList = new List<VesselTypeList>();

            var context = WebOperationContext.Current;
            var requesturl = context.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            DAL_JiBE.Service_LOG("-- URL LOG -- Get_Vessels_By_Type_MultiVesselSync_Test --  " + requesturl, null, null);

            string OutSyncTime = "";

            try
            {
                if (Authenticate(obj1.Authentication_Token))
                {
                    DataTable dtTempInput = new DataTable();
                    dtTempInput.Columns.Add("VesselID");
                    dtTempInput.Columns.Add("Synctime");

                    List<VesselIDs_Synctimes> VesselsAndSynctimeList = obj1.VSList;
                    if (VesselsAndSynctimeList != null)
                    {
                        foreach (VesselIDs_Synctimes obj in VesselsAndSynctimeList)
                        {
                            if(obj.synctime =="null")
                                dtTempInput.Rows.Add(obj.vesselid, null  );
                            else
                                dtTempInput.Rows.Add(obj.vesselid, obj.synctime);

                            dtTempInput.AcceptChanges();
                        }
                    }

                    DataSet dsVslInsp = DAL_JiBE.Get_Vessels_By_Type_SyncTime(UDFLib_WCF.ConvertDateToNull(obj1.Sync_Time), obj1.Authentication_Token, UDFLib_WCF.ConvertDateToNull(obj1.AC_SYNC_TIME), UDFLib_WCF.ConvertDateToNull(obj1.FN_SYNC_TIME), UDFLib_WCF.ConvertDateToNull(obj1.CRW_SYN_TIME), UDFLib_WCF.ConvertDateToNull(obj1.CHKLIST_SYN_TIME), ref OutSyncTime, dtTempInput);
                    if (dsVslInsp != null && dsVslInsp.Tables.Count > 0)
                    {

                        DataTable dtVesselType = dsVslInsp.Tables[0];//That will store vessel Type data from dsVslInsp to dtVesselType

                        DataTable dtvsl = dsVslInsp.Tables[1]; // dtvsl will have vessel data

                        DataTable dtGA = dsVslInsp.Tables[3]; //dtGA will have GA list

                        foreach (DataRow drVslType in dtVesselType.Rows)
                        {
                            VesselTypeList objVslType = new VesselTypeList();

                            List<ObjectGAType> ObjGAList = new List<ObjectGAType>();

                            List<VesselCategoryType> VslList = new List<VesselCategoryType>();

                            DataRow[] drObjGA = dtGA.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");

                            foreach (DataRow drGA in drObjGA)
                            {
                                ObjectGAType objGA = new ObjectGAType();

                                objGA.ChildID = Convert.ToString(drGA["Path_ID"]);
                                objGA.Image_Path = Convert.ToString(drGA["Image_Path"]);
                                objGA.SVG_Path = Convert.ToString(drGA["SVG_Path"]);
                                objGA.Parent_ID = Convert.ToString(drGA["Parent_Path_ID"]);
                                objGA.ChildName = Convert.ToString(drGA["Path_Name"]);

                                ObjGAList.Add(objGA);
                            }


                            DataRow[] drObjVsl = dtvsl.Select(" Vessel_TypeID='" + Convert.ToString(drVslType["vessel_TypeID"]) + "'");
                            foreach (DataRow dr in drObjVsl)
                            {
                                VesselCategoryType obvsl = new VesselCategoryType();
                                obvsl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                                obvsl.Vessel_ID = UDFLib_WCF.ConvertToInteger(dr["Vessel_ID"]);
                                obvsl.Attachment_Size = dr["Attachment_Size"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["Attachment_Size"]);

                                if (dr["ACTIVE_STATUS"].ToString() == "0")
                                {
                                    obvsl.Active_Status = 0;
                                }
                                else if (dr["ACTIVE_STATUS"].ToString() == "1")
                                {
                                    obvsl.Active_Status = 1;
                                }

                                VslList.Add(obvsl);

                            }

                            objVslType.VesselList = VslList;
                            objVslType.GAList = ObjGAList;
                            objVslType.Vessel_TypeID = Convert.ToInt32(drVslType["vessel_TypeID"]);
                            objVslType.Vessel_TypeName = Convert.ToString(drVslType["VesselTypes"]);

                            VslTypeList.Add(objVslType);
                        }


                        objVslRes.VesselTypeList = VslTypeList;
                        if (dtVesselType.Rows.Count > 0 && (dtvsl.Rows.Count > 0 || dtGA.Rows.Count > 0))

                            objVslRes.Sync_Time = OutSyncTime;
                    }
                }
                else
                {
                    DAL_JiBE.Service_LOG("Authentication Fail LOG -- Get_Vessels_By_Type_MultiVesselSync_Test --  Token - " + obj1.Authentication_Token + " -- URL --" + requesturl, null, null);
                }
            }
            catch (Exception ex)
            {

                DAL_JiBE.Service_LOG("Exception  LOG -- Get_Vessels_By_Type_MultiVesselSync_Test --  Exception - " + ex.ToString() + requesturl, null, null);
            }

            return objVslRes;

        }
    }



    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FullFileName;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}


