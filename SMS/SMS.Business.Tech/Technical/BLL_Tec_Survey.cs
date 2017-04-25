using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Technical;

namespace SMS.Business.Technical
{
    public class BLL_Tec_Survey
    {
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        DAL_Tec_Survey objDLL = new DAL_Tec_Survey();
        public DataTable Get_Survey_MainCategoryList()
        {
            return objDLL.Get_Survey_MainCategoryList_DL();
        }
        public DataTable Get_Survay_CategoryList()
        {
            return objDLL.Get_Survey_CategoryList_DL("");
        }
        public DataTable Get_Survey_CategoryList_ByMainCategoryId(int MainCategoryId)
        {
            return objDLL.Get_Survey_CategoryList_ByMainCategoryId_DL(MainCategoryId);
        }
        public DataTable Get_Survay_CategoryList(string SearchText)
        {
            return objDLL.Get_Survey_CategoryList_DL(SearchText);
        }

        public DataTable Get_SurvayCertificate_List(int CategoryID)
        {
            return objDLL.Get_SurveyCertificate_List_DL(CategoryID);
        }

        public DataTable Get_SurvayCertificate_List(int CategoryID, string SearchText)
        {
            return objDLL.Get_SurveyCertificate_List_DL(CategoryID, SearchText);
        }

        public DataTable Get_SurvayCertificate_List(DataTable dtCatIDList, int CatID, int FleetID, int VesselID)
        {
            return objDLL.Get_SurveyCertificate_List_DL(dtCatIDList, CatID, FleetID, VesselID);
        }

        public DataSet Get_SurvayList(int FleetID, int VesselID, int MainCategoryId, int CategoryID, int CertificateID, string IssueFrom, string IssueTo, string ExpFrom, string ExpTo, int ExpiryInDays, int Verified, string SearchText, Boolean ShowAll, string CatIDList, int? PAGE_SIZE, int? PAGE_INDEX, int? SelectRecordCount)
        {

            DateTime dtIssueFrom = DateTime.Parse(IssueFrom);
            DateTime dtIssueTo = DateTime.Parse(IssueTo);
            DateTime dtExpFrom = DateTime.Parse(ExpFrom);
            DateTime dtExpTo = DateTime.Parse(ExpTo);


            //Overdue = -1 //Custom Dates =0
            if (ExpiryInDays == -1)
            {
                //Overdue
                dtExpFrom = DateTime.Parse("1900/01/01");
                dtExpTo = DateTime.Parse(DateTime.Today.AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (ExpiryInDays == 0)
            {
                //Custom Dates
            }
            else
            {
                //30, 60, 90 days
                //dtExpFrom = DateTime.Parse(DateTime.Today.ToString("yyyy/MM/dd")); - Changed on 05/05/2011
                dtExpFrom = DateTime.Parse("1900/01/01");
                dtExpTo = DateTime.Parse(DateTime.Today.AddDays(ExpiryInDays).ToString("yyyy/MM/dd"));
            }

            return objDLL.Get_SurvayList_DL(FleetID, VesselID, MainCategoryId, CategoryID, CertificateID, dtIssueFrom, dtIssueTo, dtExpFrom, dtExpTo, Verified, SearchText, ExpiryInDays, ShowAll, CatIDList, PAGE_SIZE, PAGE_INDEX, SelectRecordCount);
        }

        public DataTable Get_AssignedSurvayList(int FleetID, int VesselID, int MainCatID, int CatID)
        {
            return objDLL.Get_AssignedSurvayList_DL(FleetID, VesselID, MainCatID, CatID);
        }

        public DataTable Get_NASurvayList(int FleetID, int VesselID, int CatID, int CertificateID, int? Verified)
        {
            return objDLL.Get_NASurvayList_DL(FleetID, VesselID, CatID, CertificateID, Verified);
        }

        public DataTable Get_NASurvayList(int FleetID, int VesselID, DataTable CatIDList, int CertificateID, int? Verified)
        {
            return objDLL.Get_NASurvayList_DL(FleetID, VesselID, CatIDList, CertificateID, Verified);
        }

        public DataTable Get_SurvayDetailList(int Vessel_ID, int Surv_Vessel_ID)
        {
            return objDLL.Get_SurvayDetailList_DL(Vessel_ID, Surv_Vessel_ID);
        }

        public DataTable Get_SurvayDetails(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int OfficeID)
        {
            return objDLL.Get_SurvayDetails_DL(Vessel_ID, Surv_Vessel_ID, Surv_Details_ID, OfficeID);
        }

        public DataSet Get_NewSurvayDetails(int Vessel_ID, int Surv_Vessel_ID)
        {
            return objDLL.Get_NewSurvayDetails_DL(Vessel_ID, Surv_Vessel_ID);
        }

        public DataTable Get_SurvayAttachments(int Vessel_ID, int Surv_Details_ID)
        {
            return objDLL.Get_SurvayAttachments_DL(Vessel_ID, Surv_Details_ID);
        }

        public DataTable Get_SurvayAttachments(int Vessel_ID, int Surv_Details_ID, ref int Allowed_Size)
        {
            return objDLL.Get_SurvayAttachments_DL(Vessel_ID, Surv_Details_ID, ref Allowed_Size);
        }

        public DataTable Get_SurvayFollowups(int Vessel_ID, int Surv_Details_ID, int OfficeID)
        {
            return objDLL.Get_SurvayFollowups_DL(Vessel_ID, Surv_Details_ID, OfficeID);
        }

        public int INSERT_New_Followup(int Vessel_ID, int Surv_Details_ID, int OfficeID, string FollowUpText, int CreatedBy)
        {
            return objDLL.INSERT_New_Followup_DL(Vessel_ID, Surv_Details_ID, OfficeID, FollowUpText, CreatedBy);
        }

        public int INSERT_New_Attachment(int Vessel_ID, int Surv_Details_ID, string AttachmentName, string AttachmentPath, long Size_Bytes, int CreatedBy, int MaxSizeFileSize,int DocTypeId,string IssueDate,string Remarks)
        {
            return objDLL.INSERT_New_Attachment_DL(Vessel_ID, Surv_Details_ID, AttachmentName, AttachmentPath, Size_Bytes, CreatedBy, MaxSizeFileSize, DocTypeId,IssueDate,Remarks);
        }

        public int INSERT_SurveyDetails(int Vessel_ID, int Surv_Vessel_ID, string DateOfIssue, string DateOfExpiry, string Remarks, string FollowupReminderDt, string FollowupReminder, int Created_By, int NoExpiry, int GraceRange, string ExtensionDate,int CertificateNo,int IssuingAuthorityID)
        {
            DateTime dtDateOfIssue = DateTime.Parse(DateOfIssue, iFormatProvider);
            DateTime dtDateOfExpiry = DateTime.Parse("1900/01/01");
            DateTime dtDateOfExtension = DateTime.Parse("1900/01/01");
            DateTime dtFollowupReminderDt = DateTime.Parse("1900/01/01");
            if (ExtensionDate != "")
                dtDateOfExtension = DateTime.Parse(ExtensionDate, iFormatProvider);
            if (DateOfExpiry != "")
                dtDateOfExpiry = DateTime.Parse(DateOfExpiry, iFormatProvider);           
            if (FollowupReminderDt != "")
                dtFollowupReminderDt = DateTime.Parse(FollowupReminderDt, iFormatProvider);

            return objDLL.INSERT_SurveyDetails_DL(Vessel_ID, Surv_Vessel_ID, dtDateOfIssue, dtDateOfExpiry, Remarks, dtFollowupReminderDt, FollowupReminder, Created_By, NoExpiry, GraceRange, dtDateOfExtension, CertificateNo, IssuingAuthorityID);
        }

        public int UPDATE_SurveyDetails(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int Office_ID, string DateOfIssue, string DateOfExpiry, string Remarks, string FollowupReminderDt, string FollowupReminder, int Modified_By, int NoExpiry, int GraceRange, string ExtensionDate, int CertificateNo, int IssuingAuthorityID)
        {
            DateTime dtDateOfIssue = DateTime.Parse(DateOfIssue);
            DateTime dtDateOfExpiry = DateTime.Parse("1900/01/01");
            DateTime dtDateOfExtension = DateTime.Parse("1900/01/01");
            DateTime dtFollowupReminderDt = DateTime.Parse("1900/01/01");
            if (ExtensionDate != "")
                dtDateOfExtension = DateTime.Parse(ExtensionDate, iFormatProvider);
            if (DateOfExpiry != "")
                dtDateOfExpiry = DateTime.Parse(DateOfExpiry, iFormatProvider);            
            if (FollowupReminderDt != "")
                dtFollowupReminderDt = DateTime.Parse(FollowupReminderDt, iFormatProvider);
            return objDLL.UPDATE_SurveyDetails_DL(Vessel_ID, Surv_Vessel_ID, Surv_Details_ID, Office_ID, dtDateOfIssue, dtDateOfExpiry, Remarks, dtFollowupReminderDt, FollowupReminder, Modified_By, NoExpiry, GraceRange, dtDateOfExtension, CertificateNo, IssuingAuthorityID);
        }

        public int Verify_Survey(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int Office_ID, int VerificationStatus, int Verified_By)
        {
            return objDLL.Verify_Survey_DL(Vessel_ID, Surv_Vessel_ID, Surv_Details_ID, Office_ID, VerificationStatus, Verified_By);
        }

        public int UPDATE_SurveyStatus(int Vessel_ID, int Surv_Vessel_ID, int SurveyStatus, int Modified_By)
        {
            return objDLL.UPDATE_SurveyStatus_DL(Vessel_ID, Surv_Vessel_ID, SurveyStatus, Modified_By);
        }

        public int Verify_NAMarked_Survey(int Vessel_ID, int Surv_Vessel_ID, int VerifyStatus, int Modified_By)
        {
            return objDLL.Verify_NAMarked_Survey_DL(Vessel_ID, Surv_Vessel_ID, VerifyStatus, Modified_By);
        }

        public int INSERT_Survey_Category(int MainCategoryId, string CategoryName, int CreatedBy)
        {
            return objDLL.INSERT_Survey_Category_DL(MainCategoryId, CategoryName, CreatedBy);
        }

        public int INSERT_Survey_Certificate(string CertificateName, int CategoryID, int CreatedBy, int Term, string Survey_Cert_remarks, int? Alert_Insurance, int? GraceDateRange)
        {
            return objDLL.INSERT_Survey_Certificate_DL(CertificateName, CategoryID, CreatedBy, Term, Survey_Cert_remarks, Alert_Insurance, GraceDateRange);
        }

        public int UPDATE_Survey_Category(int MainCategoryId, int CategoryID, string CategoryName, int ModifiedBy)
        {
            return objDLL.UPDATE_Survey_Category_DL(MainCategoryId, CategoryID, CategoryName, ModifiedBy);
        }

        public int UPDATE_Survey_Certificate(int CertificateID, string CertificateName, int CategoryID, int ModifiedBy, int Term, string Survey_Cert_remarks, int? Alert_Insurance, int? GraceDateRange)
        {
            return objDLL.UPDATE_Survey_Certificate_DL(CertificateID, CertificateName, CategoryID, ModifiedBy, Term, Survey_Cert_remarks, Alert_Insurance, GraceDateRange);
        }

        public int DELETE_Survey_Category(int CategoryID, int DeletedBy)
        {
            return objDLL.DELETE_Survey_Category_DL(CategoryID, DeletedBy);
        }

        public int DELETE_Survey_Certificate(int CertificateID, int DeletedBy)
        {
            return objDLL.DELETE_Survey_Certificate_DL(CertificateID, DeletedBy);
        }

        public int Assign_SurveyToVessel(int VesselID, int Surv_ID, string EquipmentType, string IssuingAuth, int CreatedBy)
        {
            return objDLL.Assign_SurveyToVessel_DL(VesselID, Surv_ID, EquipmentType, IssuingAuth, CreatedBy);
        }

        public int UPDATE_Survey_CertificateRemarks(int VesselID, int Surv_Vessel_ID, int Surv_Detail_ID, int ModifiedBy, string Survey_Cert_remarks, int? Term, string MakeModel, string IssuingAuthority)
        {
            return objDLL.UPDATE_Survey_CertificateRemarks_DL(VesselID, Surv_Vessel_ID, Surv_Detail_ID, ModifiedBy, Survey_Cert_remarks, Term, MakeModel, IssuingAuthority);
        }

        public DataTable Get_Survey_Certificate_Search(string searchtext, int? MainCategoryID, int? SurvCategoryID
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDLL.Get_Survey_Certificate_Search(searchtext, MainCategoryID, SurvCategoryID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataTable Get_Survey_Certificate_List_By_SurvID(int? Surv_Certificate_ID)
        {
            return objDLL.Get_Survey_Certificate_List_By_SurvID_DL(Surv_Certificate_ID);
        }

        public DataTable Get_Survey_CategoryList(int? Surv_Cetegory_ID)
        {
            return objDLL.Get_Survey_CategoryList_DL(Surv_Cetegory_ID);
        }

        public DataTable Get_Survey_Category_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDLL.Get_Survey_Category_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int INSERT_Document_Type(int Id, string DocType, int CreatedBy)
        {
            return objDLL.INSERT_Document_Type_DL(Id, DocType, CreatedBy);
        }
        public int UPDATE_Document_Type(int Id, string DocType, int CreatedBy)
        {
            return objDLL.UPDATE_Document_Type_DL(Id, DocType, CreatedBy);
        }
        public int DELETE_Document_Type(int Id, int DeletedBy)
        {
            return objDLL.DELETE_SDocument_Type_DL(Id, DeletedBy);
        }
        public DataTable Get_Survey_Document_Type_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDLL.Get_Survey_Document_Type_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public DataTable Get_Survey_Document_Type(int? ID)
        {
            return objDLL.Get_Survey_Document_Type_DL(ID);
        }
        public DataTable Get_Survey_Document_Type_List()
        {
            return objDLL.Get_Survey_Document_Type_List_DL();
        }

        public DataTable Get_Survay_CertificateAuthorityList(string SearchText)
        {
            return objDLL.Get_Survay_CertificateAuthorityList_DL(SearchText);
        }
        public int INSERT_Survey_CertificateAuthority(string Authority, int CreatedBy)
        {
            return objDLL.INSERT_Survey_CertificateAuthority_DL(Authority, CreatedBy);
        }
        public int UPDATE_Survey_CertificateAuthority(int ID, string CategoryName, int ModifiedBy)
        {
            return objDLL.UPDATE_Survey_CertificateAuthority_DL(ID, CategoryName, ModifiedBy);
        }
        public int DELETE_Survey_CertificateAuthority(int ID, int DeletedBy)
        {
            return objDLL.DELETE_Survey_Category_DL(ID, DeletedBy);
        }

        public DataTable Get_Authorit()
        {
            return objDLL.Get_Authorit_DL();
        }


        public DataTable Get_AuthoritBySurv_Details_ID(int survDetailsID, int survVesselId)
        {
            return objDLL.Get_AuthoritBySurv_Details_ID_DL(survDetailsID, survVesselId);
        }
    }


}
