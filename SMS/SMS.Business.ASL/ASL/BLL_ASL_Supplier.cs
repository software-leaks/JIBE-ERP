using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using SMS.Data.ASL;


namespace SMS.Business.ASL 
{
    public class BLL_ASL_Supplier
    {

        public static DataTable Get_Proposed_Supplier_Search(string searchtext, int? Propose_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int Created_By, int? Pageset)
        {
            return DAL_ASL_Supplier.Get_Proposed_Supplier_Search(searchtext, Propose_Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, Created_By, Pageset);
        }

        public static DataTable Get_Supplier_Search_SimilerName(string searchtext, DataTable dt,string SupplierCode, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_ASL_Supplier.Get_Supplier_Search_SimilerName(searchtext, dt, SupplierCode, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable Get_Supplier_Search(string searchtext, int? Supp_Port, string Supp_Type, string Eval_Status, DataTable dtType, string CurrStatus,int ChkCredit,string SupplierDesc, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_ASL_Supplier.Get_Supplier_Search(searchtext, Supp_Port, Supp_Type, Eval_Status, dtType, CurrStatus, ChkCredit, SupplierDesc, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataSet Get_ChangeRequest_Search(string Supplier_code, int CRID, int UserID)
        {
            return DAL_ASL_Supplier.Get_ChangeRequest_Search(Supplier_code, CRID, UserID);
        }
        public static DataSet Get_Pending_CR_List(int? UserID, string searchtext, int? Supp_Port, string Supp_Type, string Eval_Status, DataTable dtType, string CurrStatus, int ChkCredit, string SupplierDesc)
        {
            return DAL_ASL_Supplier.Get_Pending_CR_List(UserID, searchtext, Supp_Port, Supp_Type, Eval_Status, dtType, CurrStatus, ChkCredit, SupplierDesc);
        }
        public static DataSet Get_ChangeRequest_Search(int? UserID, string Supplier_Code)
        {
            return DAL_ASL_Supplier.Get_ChangeRequest_Search(UserID, Supplier_Code);
        }
        public static DataTable Get_ChangeRequest_History(string Supplier_Code)
        {
            return DAL_ASL_Supplier.Get_ChangeRequest_History(Supplier_Code);
        }
        public static DataTable Get_Proposed_Supplier_List(int? ID)
        {
            return DAL_ASL_Supplier.Get_Proposed_Supplier_List(ID);
        }
        public static DataTable Get_Supplier_Email_Template(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Email_Template(Supp_ID);
        }
        public static DataSet Get_Supplier_Data_List(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Data_List(Supp_ID);        
        }
        public static DataSet Get_Supplier_General_Data_List(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_General_Data_List(Supp_ID);
        }
        public static DataSet Get_Supplier_Change_Request_List(string Supp_ID,int CRID,int UserID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Change_Request_List(Supp_ID, CRID, UserID);
        }
        public static int Proposed_Supplier_Insert(int? ID, string Supplier_Name, int? Propose_Type, int? Propose_Status, int? For_Period, string Address, string Phone, string Email, string PIC_NAME, int Created_By,int Approved_By)
        {
            return DAL_ASL_Supplier.Proposed_Supplier_Insert(ID, Supplier_Name, Propose_Type, Propose_Status, For_Period, Address, Phone, Email, PIC_NAME, Created_By, Approved_By);
        }

        public static int Proposed_Supplier_Delete(int? ID, int Created_By)
        {
            return DAL_ASL_Supplier.Proposed_Supplier_Delete(ID, Created_By);
        }

        public static DataTable Get_ASL_System_Parameter(int Parent_Code, string searchtext,int? UserID)
        {
            return DAL_ASL_Supplier.Get_ASL_System_Parameter(Parent_Code, "", UserID);
        }
        public static DataTable Get_ASL_System_Parameter_Evaluation(int Parent_Code, string searchtext, int? UserID)
        {
            return DAL_ASL_Supplier.Get_ASL_System_Parameter_Evaluation(Parent_Code, "", UserID);
        }
        public static DataTable Get_ASL_System_SupplierProposed_Parameter(int Parent_Code, string searchtext, int? UserID)
        {
            return DAL_ASL_Supplier.Get_ASL_System_SupplierProposed_Parameter(Parent_Code, "", UserID);
        }

        public static DataTable Get_ASL_System_SupplierEvaluation_Parameter(int Parent_Code, string searchtext, int? UserID)
        {
            return DAL_ASL_Supplier.Get_ASL_System_SupplierEvaluation_Parameter(Parent_Code, "", UserID);
        }

        public static DataSet Get_Supplier_Scope(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Scope(Supp_ID);
        }

        public static DataSet Get_Supplier_PORT(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_PORT(Supp_ID);
        }
        public static DataSet Get_Supplier_Scope_CR(string Supp_ID, int? CRID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Scope_CR(Supp_ID, CRID);
        }

        public static DataSet Get_Supplier_PORT_CR(string Supp_ID, int? CRID)
        {
            return DAL_ASL_Supplier.Get_Supplier_PORT_CR(Supp_ID, CRID);
        }
        public static int ASL_Supplier_Scope_Insert(string Supp_ID, DataTable dtScope, int? CreatedBy)
        {
            return DAL_ASL_Supplier.ASL_Supplier_Scope_Insert(Supp_ID, dtScope, CreatedBy);
        }
        public static int ASL_Supplier_Port_Insert(string Supp_ID, DataTable dtService, int? CreatedBy)
        {
            return DAL_ASL_Supplier.ASL_Supplier_Port_Insert(Supp_ID, dtService, CreatedBy);
        }


        public static int ASL_Supplier_Scope_CR_Insert(string Supp_ID, DataTable dtScope, string CRStatus, int? CreatedBy, int? CRID)
        {
            return DAL_ASL_Supplier.ASL_Supplier_Scope_CR_Insert(Supp_ID, dtScope, CRStatus, CreatedBy, CRID);
        }
        public static int ASL_Supplier_Port_CR_Insert(string Supp_ID, DataTable dtService, string CRStatus, int? CreatedBy, int? CRID)
        {
            return DAL_ASL_Supplier.ASL_Supplier_Port_CR_Insert(Supp_ID, dtService, CRStatus, CreatedBy, CRID);
        }

        public static int Evaluation_Data_Insert(int? ID, string Supp_ID, string Action_On_Data_Form, string Prposed_status, int? For_Period, string JustificationRemark, string VerificationRemark, string ApprovalRemark, int Created_By, int Verify_By, int Approve_By, string chk, string Approveurl, string Rejecturl, string SupplierDesc)
        {
            return DAL_ASL_Supplier.Evaluation_Data_Insert(ID, Supp_ID, Action_On_Data_Form, Prposed_status, For_Period, JustificationRemark, VerificationRemark, ApprovalRemark, Created_By, Verify_By, Approve_By, chk, Approveurl, Rejecturl, SupplierDesc);
        }
        public static int Pending_Evaluation_Approve_Reject(int? Evaluation_ID, string Supplier_Code, string EvaluationStatus, int? Created_By, string IpAddress, string Browser_Agent)
        {
            return DAL_ASL_Supplier.Pending_Evaluation_Approve_Reject(Evaluation_ID, Supplier_Code, EvaluationStatus, Created_By, IpAddress, Browser_Agent);
        }
        public static int Evaluation_Send_Email(int? Evaluation_ID, string Supplier_Code, string EvaluationStatus, int? Created_By, string ApproveUrl, string RejectUrl)
        {
            return DAL_ASL_Supplier.Evaluation_Send_Email(Evaluation_ID, Supplier_Code, EvaluationStatus, Created_By, ApproveUrl, RejectUrl);
        }
        public static int Evaluation_Data_Delete(int? ID, string Supp_ID, int Created_By)
        {
            return DAL_ASL_Supplier.Evaluation_Data_Delete(ID, Supp_ID, Created_By);
        }
        public static DataTable Supplier_Email_Verification(string ID)
        {
            return DAL_ASL_Supplier.Supplier_Email_Verification(ID);
        }
        public static DataSet Get_Supplier_Evaluation_List(string Supp_ID, int? EvaluationID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Evaluation_List(Supp_ID, EvaluationID);
        }
       
        public static int Supplier_Data_Insert(int? ID, string Supp_ID, string Register_Name, string Address, string State
            ,string CountryName, int? CountryID,string Postal_Code,  string Comp_Phone1, string Comp_Phone2, string Comp_AOH_NO1, string Comp_AOH_NO2
            , string Comp_Fax1, string Comp_Fax2, string Comp_Email1, string Comp_Email2,
            string Comp_WebSite, string Comp_Reg_No
            , string Comp_Tex_Reg_No, string Comp_ISO_No,string Supplier_Currency, int? Supplier_CurrencyID, string PIC_NAME_Title1, string PIC_NAME_Title2
            , string PIC_NAME1, string PIC_NAME2, string PIC_Designation1, string PIC_Designation2, string PIC_Email1, string PIC_Email2
            , string PIC_Phone1, string PIC_Phone2, string Bank_Name, string Bank_Address, string Bank_Code, string Branch_Code
            , string SWIFT_Code, string IBAN_Code, string Beneficiary_Name, string Beneficiary_Address, string Beneficiary_Account
            , string Beneficiary_Account_Curr, string Notification_Payment, string Notification_Email, string Verified_Person, string Designation, string Action_On_Data_Form, int? Created_By,
            string SkypeAddress,string SkypeAddress2,string PICMobileNo,string PICmobileNo1,string SuppScope,string AddService,string OtherBankInformation,string TaxAccNumber)
        {

            return DAL_ASL_Supplier.Supplier_Data_Insert(ID, Supp_ID, Register_Name, Address, State
                , CountryName,  CountryID,  Postal_Code, Comp_Phone1, Comp_Phone2, Comp_AOH_NO1, Comp_AOH_NO2
                , Comp_Fax1, Comp_Fax2, Comp_Email1, Comp_Email2, Comp_WebSite, Comp_Reg_No
                , Comp_Tex_Reg_No, Comp_ISO_No,Supplier_Currency, Supplier_CurrencyID, PIC_NAME_Title1, PIC_NAME_Title2
                , PIC_NAME1, PIC_NAME2, PIC_Designation1, PIC_Designation2, PIC_Email1, PIC_Email2
                , PIC_Phone1, PIC_Phone2, Bank_Name, Bank_Address, Bank_Code, Branch_Code
                , SWIFT_Code, IBAN_Code, Beneficiary_Name, Beneficiary_Address, Beneficiary_Account
                , Beneficiary_Account_Curr, Notification_Payment, Notification_Email, Verified_Person, Designation, Action_On_Data_Form, Created_By, SkypeAddress, SkypeAddress2, PICMobileNo, PICmobileNo1, SuppScope, AddService, OtherBankInformation, TaxAccNumber);
        }

        public static string Supplier_General_Data_Insert(int? ID, string Supplier_Code, string Supp_Type, string Supp_Status, string Register_Name, string Short_Name, int? group, string Address1,
             string CountryName,  string City,  string Comp_Phone1, string Comp_Fax1,  string Comp_Email1,string Supplier_Currency, 
              string PIC_NAME1, string PIC_NAME2,  string PIC_Email1, string PIC_Email2,string PIC_Phone1, string PIC_Phone2, 
             int? ownerShip, string Payment_Instructions, string Notification_Email, string BizCorporation, string Invoice_Status,string Direct_Invoice_Upload,string PaymentHistory,string SendPo,
            string PaymentPriority, int? Payment_Interval, int? Payment_Terms_Days, string Payment_Terms, decimal? TaxRate, string Action_On_Data_Form, int? Created_By, int? countryID,
            string Supplier_Description, string SubType, string TaxAccNumber, string Company_Reg_No, string GST_Registration_No, decimal? Withholding_Tax_Rate, string Supplier_Short_Code,string ShipSmart_Supplier_Code)
        {

            return DAL_ASL_Supplier.Supplier_General_Data_Insert(ID, Supplier_Code, Supp_Type, Supp_Status, Register_Name, Short_Name, group, Address1, CountryName, City, Comp_Phone1 
                , Comp_Fax1, Comp_Email1, Supplier_Currency,  PIC_NAME1, PIC_NAME2,PIC_Email1, PIC_Email2
                , PIC_Phone1, PIC_Phone2, ownerShip, Payment_Instructions, Notification_Email, BizCorporation, Invoice_Status, Direct_Invoice_Upload, PaymentHistory, SendPo,
                PaymentPriority, Payment_Interval, Payment_Terms_Days, Payment_Terms, TaxRate, Action_On_Data_Form, Created_By, countryID, Supplier_Description, SubType, TaxAccNumber, Company_Reg_No, GST_Registration_No, Withholding_Tax_Rate, Supplier_Short_Code, ShipSmart_Supplier_Code);
        }
        public static int Supplier_ChangeRequest_Data_Insert(int? ID, string Supp_ID, string Supp_Type, string Register_Name, string Short_Name, string Address1,
            string CountryName, int? countryID, string City, string Comp_Email1, string Comp_Phone1, string Comp_Fax1, string Supplier_Currency,
             string PIC_NAME1, string PIC_Email1, string PIC_Phone1, string PIC_NAME2, string PIC_Email2, string PIC_Phone2,
            int? ownerShip, string PaymentInstrucation, string Notification_Email, string BizCorporation, string Invoice_Status,
            string Direct_Invoice_Upload, string PaymentHistory, string PaymentPriority, int? Payment_Interval, int? Payment_Terms_Days, string Payment_Terms, string TaxRate, string EmailReason, string BankReason, string Action_On_Data_Form, string CR_Status, int? Created_By)
        {

            return DAL_ASL_Supplier.Supplier_ChangeRequest_Data_Insert(ID, Supp_ID, Supp_Type, Register_Name, Short_Name, Address1, CountryName, countryID, City, Comp_Email1, Comp_Phone1, Comp_Fax1, Supplier_Currency, PIC_NAME1, PIC_Email1, PIC_Phone1, PIC_NAME2, PIC_Email2
                , PIC_Phone2, ownerShip, PaymentInstrucation, Notification_Email, BizCorporation, Invoice_Status, Direct_Invoice_Upload, PaymentHistory, PaymentPriority, Payment_Interval, Payment_Terms_Days, Payment_Terms, TaxRate, EmailReason, BankReason, Action_On_Data_Form, CR_Status, Created_By);
        }
        public static int Supplier_ChangeRequest_Data_Insert(int? CRID, string Supp_ID, DataTable dtCR, string CR_Status, string Action_On_Data_Form, int? Created_By)
        {

            return DAL_ASL_Supplier.Supplier_ChangeRequest_Data_Insert(CRID, Supp_ID, dtCR, CR_Status, Action_On_Data_Form, Created_By);
        }
        public static int Supplier_Update_Unverify(int? ID, string Supplier_Code, string Action_On_Data_Form, string Status, int? Created_By)
        {

            return DAL_ASL_Supplier.Supplier_Update_Unverify(ID, Supplier_Code, Action_On_Data_Form, Status, Created_By);
        }
        public static int Supplier_Insert_Rework(int? ID, string Supplier_Code, string Action_On_Data_Form,  int? Created_By)
        {

            return DAL_ASL_Supplier.Supplier_Insert_Rework(ID, Supplier_Code, Action_On_Data_Form, Created_By);
        }

        public static int Supplier_ChangeRequest_Status_Update(int? CRID, string Supplier_Code, DataTable dtCR, string CR_Status, string Action_On_Data_Form, int? Created_By)
        {

            return DAL_ASL_Supplier.Supplier_ChangeRequest_Status_Update(CRID, Supplier_Code, dtCR, CR_Status, Action_On_Data_Form, Created_By);
        }
        public static int Supplier_Data_Send_For_Verification(int? Supp_ID, int? Supp_Data_ID, int? Proposed_By)
        {
            return DAL_ASL_Supplier.Supplier_Data_Send_For_Verification(Supp_ID, Supp_Data_ID, Proposed_By);
        }


        public static int Supplier_Data_Send_For_Approval(int? Supp_ID, int? Supp_Data_ID, int? Manager_ID)
        {
            return DAL_ASL_Supplier.Supplier_Data_Send_For_Approval(Supp_ID, Supp_Data_ID, Manager_ID);
        }
        //public static int Send_To_Supplier_Rework(int? Supp_ID, int? Supp_Data_ID, int? Manager_ID)
        //{
        //    return DAL_ASL_Supplier.Send_To_Supplier_Rework(Supp_ID, Supp_Data_ID, Manager_ID);
        //}


        public static int Supplier_Data_Approval(int? Supp_ID, int? Supp_Data_ID, int? Supp_Status, int? BOSS_ID)
        {
            return DAL_ASL_Supplier.Supplier_Data_Approval(Supp_ID, Supp_Data_ID, Supp_Status, BOSS_ID);
        }

        public static DataTable Get_Supplier_Eval_History_Search(string searchtext, int? Supp_ID, int? Evaluation_Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount,int? Pageset,int? UserID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Eval_History_Search(searchtext, Supp_ID, Evaluation_Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, Pageset, UserID);
        }
        public static DataTable Get_Supplier_Pending_Evaluation(string searchtext, int? UserID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Pending_Evaluation(searchtext, UserID);
        }
        public static DataTable Get_Supplier_Eval_Search(int? UserID, string searchtext, int? Supp_Port, string Supp_Type, string Eval_Status, DataTable dtType, string CurrStatus, int ChkCredit, string SupplierDesc)
        {
            return DAL_ASL_Supplier.Get_Supplier_Eval_Search(UserID, searchtext, Supp_Port, Supp_Type, Eval_Status, dtType, CurrStatus, ChkCredit, SupplierDesc);
        }
        public static int Supplier_Remarks_Insert(int? RemarksID,string Supp_ID, string AmendType,string GeneralType,string GreenType,string YellowType,string RedType, string Remarks, int? Created_By)
        {

            return DAL_ASL_Supplier.Supplier_Remarks_Insert(RemarksID, Supp_ID, AmendType,GeneralType,GreenType,YellowType,RedType, Remarks, Created_By);
        }
        public static DataTable Get_Supplier_Remarks(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Remarks(Supp_ID);
        }
        public static DataTable Get_Supplier_Remarks_List(int RemarksID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Remarks_List(RemarksID);
        }
        public static int Delete_Supplier_Remarks(int? RemarksID, int? Created_By)
        {
            return DAL_ASL_Supplier.Delete_Supplier_Remarks(RemarksID, Created_By);
        }
        public static DataSet Get_Supplier_Attachment(string Supplier_Code)
        {
            return DAL_ASL_Supplier.Get_Supplier_Attachment(Supplier_Code);
        }
        public static int Delete_Uploaded_Files(int? FileID,int? USERID)
        {
            return DAL_ASL_Supplier.Delete_Uploaded_Files(FileID, USERID);
        }
        public static DataSet Get_Supplier_Upload_Document(string Supplier_Code, string Supp_ID, string InvoiceType, string FileID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Upload_Document(Supplier_Code, Supp_ID, InvoiceType,FileID);
        }
        public static int Supplier_Upload_Document_Insert(int? FileID, string Supplier_Code, string Supply_ID, string File_Name, string File_Path, string File_Extension, string InvioceRef, DateTime? InvoiceDate, decimal? InvoiceAmount, decimal? TaxAmount, string Remarks,DateTime? InvoiceDueDate,string FileStatus,string Action_On_Data_Form,string InvoiceType, int? Created_By, int TYPE)
        {

            return DAL_ASL_Supplier.Supplier_Upload_Document_Insert(FileID,Supplier_Code, Supply_ID, File_Name, File_Path, File_Extension,InvioceRef,InvoiceDate,InvoiceAmount,TaxAmount,Remarks,InvoiceDueDate,FileStatus,Action_On_Data_Form,InvoiceType, Created_By, TYPE);
        }
        public static int Supplier_Attachment_Insert(string Supplier_Code, string FileName, string Filepath,string FileExtension, int? Created_By, string TYPE, string DocType)
        {

            return DAL_ASL_Supplier.Supplier_Attachment_Insert(Supplier_Code, FileName, Filepath,FileExtension, Created_By, TYPE, DocType);
        }

        public static int Supplier_Attachment_Delete(int? File_ID, string Supp_ID, int? Created_By)
        {
            return DAL_ASL_Supplier.Supplier_Attachment_Delete(File_ID,Supp_ID, Created_By);
        }

        public static int Supplier_Extend_Period(int? Supp_ID, int? For_period)
        {
            return DAL_ASL_Supplier.Supplier_Extend_Period(Supp_ID, For_period);
        }

        public static DataSet Get_Supplier_ApproverList(int? UserID, string Supplier_Type, string Approver_Type)
        {
            return DAL_ASL_Supplier.Get_Supplier_ApproverList(UserID, Supplier_Type, Approver_Type);
        }
        public static DataSet Get_Supplier_CR_ApproverList(int? UserID, string Supplier_Type, string Approver_Type,string Group_Name)
        {
            return DAL_ASL_Supplier.Get_Supplier_CR_ApproverList(UserID, Supplier_Type, Approver_Type, Group_Name);
        }
        public static DataSet Get_Supplier_PaymentHistory(string Supp_ID, int Type, int PaymentID, int PaymentYear)
        {
            return DAL_ASL_Supplier.Get_Supplier_PaymentHistory(Supp_ID, Type, PaymentID, PaymentYear);
        }
        public static DataSet Get_Supplier_POInvoiceHistory(string Supp_ID, int? VesselID, string VesselName,int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_ASL_Supplier.Get_Supplier_POInvoiceHistory(Supp_ID, VesselID, VesselName,  pagenumber, pagesize, ref isfetchcount);
        }
        public static DataSet Get_Supplier_POInvoiceWIP(string Supp_ID,int? userID,  int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_ASL_Supplier.Get_Supplier_POInvoiceWIP(Supp_ID, userID, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataSet Get_Supplier_PO_PendingInvoice(string Supp_ID, int? VesselID, string VesselName)
        {
            return DAL_ASL_Supplier.Get_Supplier_PO_PendingInvoice(Supp_ID, VesselID, VesselName);
        }
        public static DataSet ASL_GET_POInvoice_Status(string Supp_ID, int? VesselID, string VesselName)
        {
            return DAL_ASL_Supplier.ASL_GET_POInvoice_Status(Supp_ID, VesselID, VesselName);
        }
        public static DataSet Get_Supplier_PO_OutStanding(string Supp_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_ASL_Supplier.Get_Supplier_PO_OutStanding(Supp_ID, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataSet Get_Supplier_Statistics(string Supp_ID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Statistics(Supp_ID);
        }
        public static DataTable ASL_Get_TransactionLog(string Supplier_Code)
        {
            return DAL_ASL_Supplier.ASL_Get_TransactionLog(Supplier_Code);
        }
        public static int ASL_Send_CR_mail(string Status,int Created_By,string  Supplier_Code, int? CRID)
        {
            return DAL_ASL_Supplier.ASL_Send_CR_mail(Status, Created_By, Supplier_Code, CRID);
        }

        public static DataSet Get_Supplier_Properties(string Supplier_Code,int UserID)
        {
            return DAL_ASL_Supplier.Get_Supplier_Properties(Supplier_Code, UserID);
        }
        public static int ASL_Supplier_Properties_Insert(string Supplier_Code, DataTable dt, int? CreatedBy)
        {
            return DAL_ASL_Supplier.ASL_Supplier_Properties_Insert(Supplier_Code, dt, CreatedBy);
        }
        public static DataSet Get_CR_Supplier_Properties(string Supplier_Code,int? CRID, int UserID)
        {
            return DAL_ASL_Supplier.Get_CR_Supplier_Properties(Supplier_Code,CRID, UserID);
        }
        public static int ASL_CR_Supplier_Properties_Insert(string Supplier_Code, DataTable dt,string Status, int? CreatedBy,int CRID)
        {
            return DAL_ASL_Supplier.ASL_CR_Supplier_Properties_Insert(Supplier_Code, dt, Status, CreatedBy, CRID);
        }
        public static int ASL_CR_Supplier_Child_Insert(int? CRID,string Supplier_Code, string Status,string Action_Data_Form, int? CreatedBy)
        {
            return DAL_ASL_Supplier.ASL_CR_Supplier_Child_Insert(CRID,Supplier_Code,  Status,Action_Data_Form, CreatedBy);
        }
    }

}
