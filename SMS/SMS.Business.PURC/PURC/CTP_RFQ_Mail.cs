using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Data;
using System.Configuration;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;

public class CTP_RFQ_Mail : Page
{
    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    string FileName;

    public CTP_RFQ_Mail()
    {
        
    }

    public CTP_RFQ_Mail(Page currentpg)
    {
        thispage = currentpg;
    }


    Page thispage;
    public void WriteExcell(DataSet ds, string FileName, string strSavePath, string FilePath)
    {

        string path = FilePath + @"CTP_RFQ_FormatFile.xls";


        ExlApp = new Microsoft.Office.Interop.Excel.Application();
     
            ExlWrkBook = ExlApp.Workbooks.Open(path, 0,
                                                      true,
                                                      5,
                                                      "",
                                                      "",
                                                      true,
                                                      Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                      "\t",
                                                      false,
                                                      false,
                                                      0,
                                                      true,
                                                      1,
                                                      0);
            ExlWrkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExlWrkBook.ActiveSheet;


            ExlWrkSheet.Cells[6, 15] = ds.Tables[0].Rows[0]["Quotation_ID"].ToString();
            ExlWrkSheet.Cells[2, 3] = ds.Tables[0].Rows[0]["QTN_Contract_Code"].ToString();

            ExlWrkSheet.Cells[11, 3] = ds.Tables[0].Rows[0]["System_Description"].ToString();

            ExlWrkSheet.Cells[7, 3] = ds.Tables[0].Rows[0]["Full_NAME"].ToString();
            ExlWrkSheet.Cells[5, 13] = DateTime.Now.ToString("yyyy/MM/dd");
            ExlWrkSheet.Cells[10, 3] = ds.Tables[0].Rows[0]["Dept_Name"].ToString();




            int i = 15;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                ExlWrkSheet.Cells[i, 1] = dr["ROWNUM"].ToString();


                ExlWrkSheet.Cells[i, 3] = dr["Part_Number"].ToString();

                ExlWrkSheet.Cells[i, 4] = dr["QTN_Item_ID"].ToString();
                ExlWrkSheet.Cells[i, 5] = dr["Short_Description"].ToString();
                ExlWrkSheet.Cells[i + 1, 5] = dr["Long_Description"].ToString();
                ExlWrkSheet.Cells[i + 2, 5] = dr["Purchaser_Remark"].ToString();
                ExlWrkSheet.Cells[i, 6] = dr["Unit_and_Packings"].ToString();
                ExlWrkSheet.Cells[i, 7] = dr["Unit_and_Packings"].ToString();


                i = i + 3;
            }


            ExlWrkSheet.get_Range("A" + (ds.Tables[1].Rows.Count * 3 + 15).ToString(), "N1639").Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);

            ExlWrkSheet.get_Range("G9", "G9").NumberFormat = "#0.00";

            ExlWrkSheet.get_Range("M1", "M10").EntireColumn.Hidden = true;

            ExlWrkSheet.Protect("tessmave", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing);


            ExlWrkBook.SaveAs(strSavePath + FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            string destFile = Server.MapPath("../Uploads/Purchase") + "\\" + FileName; ;
            File.Copy(strSavePath + FileName, destFile, true);

       
            ExlWrkBook.Close(null, null, null);
            //ExlApp.Workbooks.Close();
            ExlApp.Quit();
            Marshal.ReleaseComObject(ExlApp);
            Marshal.ReleaseComObject(ExlWrkSheet);
            Marshal.ReleaseComObject(ExlWrkBook);

    


    }

    public void SendMailToSupplier(int Quotation_ID, int RFQType, Page currentpg)
    {
        thispage = currentpg;
        string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();

        DataSet dsSendMailInfo = BLL_PURC_CTP.Get_Ctp_Supplier_Mail(Quotation_ID);

        switch (RFQType)
        {
            // Excel Based RFQ
            case 1:
                string strPath = Server.MapPath(".") + "\\SendRFQ\\";
                string FilePath = Server.MapPath("~") + "\\Purchase\\ExcelFile\\";
                FileName = dsSendMailInfo.Tables[0].Rows[0]["QTN_Contract_Code"].ToString() + "_" + dsSendMailInfo.Tables[0].Rows[0]["First_Name"].ToString().Replace(" ", "_").Replace(".", "_") + DateTime.Now.ToString("yyMMdd") + "_" + dsSendMailInfo.Tables[0].Rows[0]["PORT_NAME"].ToString() + ".xls";
                DataSet dsRFQ = BLL_PURC_CTP.Get_Ctp_RFQ_Items(Quotation_ID);

                WriteExcell(dsRFQ, FileName, strPath, FilePath);

                SendEmailToSupplier(dsSendMailInfo, dsSendMailInfo.Tables[0].Rows[0]["supplier_code"].ToString(), ServerIPAdd, FileName, true, RFQType.ToString(), true);
                break;
            // Web Based RFQ
            case 2:


                SendEmailToSupplier(dsSendMailInfo, dsSendMailInfo.Tables[0].Rows[0]["supplier_code"].ToString(), ServerIPAdd, "", true, RFQType.ToString(), true);
                break;
        }
    }

    public void SendEmailToSupplier(DataSet dsEmailInfo, string strSuppCode, string strServerIPAdd, string Attachment, bool bIsListed, string RFQType, bool IsInsert_Mail)
    {
        try
        {
            DataTable dtSuppDetails = new DataTable();
            string strFormatSubject = "", strFormatBody = "", sToEmailAddress = "";
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

            DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));
            string strEmailAddCc = dtUser.Rows[0]["MailID"].ToString();

            int value = Int32.Parse(RFQType);
            BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
            int MailID = 0;
            String URL = "";
            string UploadFilePath = ConfigurationManager.AppSettings["PURC_UPLOAD_PATH"];
            switch (value)
            {
                case 1:  // Excel Based RFQ

                    FormateEmail(dsEmailInfo, true, strServerIPAdd, out sToEmailAddress, out strFormatSubject, out strFormatBody, bIsListed, Attachment);

                    MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, strFormatBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");

                    //string uploadpath = @"\\server01\uploads\Purchase";
                    string uploadpath = @"uploads\Purchase";

                    BLL_Infra_Common.Insert_EmailAttachedFile(MailID, FileName, uploadpath + @"\" + FileName);


                    URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                    ScriptManager.RegisterStartupScript(thispage, thispage.GetType(), "k" + MailID.ToString(), URL, true);


                    break;

                case 2:  // Web Based RFQ

                    FormateEmail(dsEmailInfo, false, strServerIPAdd, out sToEmailAddress, out strFormatSubject, out strFormatBody, bIsListed, "");

                    MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, strFormatBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");

                    //URL = String.Format("alert('web');");
                    URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                    ScriptManager.RegisterStartupScript(thispage, thispage.GetType(), "k" + MailID.ToString(), URL, true);


                    break;

                case 3 :

                     FormateEmailOnRework(dsEmailInfo, false, strServerIPAdd, out sToEmailAddress, out strFormatSubject, out strFormatBody, bIsListed, "");

                    MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, strFormatBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "READY");
                    URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                    ScriptManager.RegisterStartupScript(thispage, thispage.GetType(), "k" + MailID.ToString(), URL, true);

                    break;
            }
        }
        catch (Exception ex)
        {

        }

    }
    /// <summary>
    /// Add <br> fro break line in Email signature.Mobile No should come in new Line not in same line with address.
    /// Modified by Alok
    /// </summary>
    /// <param name="dsEmailInfo"></param>
    /// <param name="IsExcelBaseRFQ"></param>
    /// <param name="strServerIPAdd"></param>
    /// <param name="sEmailAddress"></param>
    /// <param name="strSubject"></param>```
    /// <param name="strBody"></param>
    /// <param name="bIsListed"></param>
    /// <param name="Attachment"></param>
    protected void FormateEmail(DataSet dsEmailInfo, bool IsExcelBaseRFQ, string strServerIPAdd, out string sEmailAddress, out string strSubject, out string strBody, bool bIsListed, string Attachment)
    {
        BLL_PURC_Purchase objpurch = new BLL_PURC_Purchase();
        string Legalterm = objpurch.Get_LegalTerm(282);
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        StringBuilder sbBody = new StringBuilder();
        sEmailAddress = "";
        strSubject = "";
        strBody = "";
        string webExl = "";
        string strfeature = "";
        string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();

        if (IsExcelBaseRFQ)  //  For Excel Based Quotation
        {

            webExl = " Kindly quote for the attached file : " + Attachment + "<br>";

        }
        else   //For Web Based Quotation.
        {
            
            webExl = @" Kindly quote by clicking on the below link :<br>
                  <a href=" + strServerIPAdd.Trim() + "'>'" + strServerIPAdd.Trim() + @"</a> <br>
                    User Name&nbsp;:  " + dsEmailInfo.Tables[0].Rows[0]["User_name"].ToString() + @"<br> 
                    Password&nbsp;&nbsp; :  " + dsEmailInfo.Tables[0].Rows[0]["Password"].ToString() + @"<br><br>  "
                      ;
            strfeature = @"<span style='color:Red'> “ANNOUNCEMENT OF NEW FEATURE” </span> <br>
                                We have implemented the below new features for vendors easy convenience. <br>
                                Ability to export this RFQ to Excel and send out to your counterparts.<br>
                                Not required to submit RFQ Excel quotation to office via email. Now this excel RFQ quotation can be directly uploaded to our system at your end. <br><br>
                                <hr/>";

        }

        string MailBodySection = @"Dear " + dsEmailInfo.Tables[0].DefaultView[0]["First_Name"].ToString() + @"<br><br>
                               
                                " + webExl + @" <br>
                              
                                Port Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : &nbsp;" + dsEmailInfo.Tables[0].DefaultView[0]["PORT_NAME"].ToString() + @"<br>
                               <br>    
                               
                                THIS IS AN ENQUIRY ONLY. IT IS NOT AN ORDER FOR PURCHASE.<br> <br> 

                                All quotes must be submitted using this format. We will not accept quotes in any other format. <br>
                                Upon receipt of this request  Kindly complete all highlighted cells and return via  this mode of transmission. <br><br>

                                Please review quantity, unit of measure, part number, lead time(in days), delivery date  and mark changes<br> 
                                as required prior sending out the quote. <b>Failure to use this form may result in disqualification.</b><br><br>

                                Special Instruction : If you haven`t received a response from us in 20 working days, Please consider this request to be closed. <br><br>

                                We will require an estimate of total shipment weight if possible, with the quote.  <br>    
                                In the event you are awarded this requisition, Payment will only be made to the name and address listed on the Quotation. <br><br>  
  
                             <b>IMPORTANT: </b><br>
                                Terms Subject to " + Session["Company_Name_GL"].ToString() + @" standard terms and conditions of purchase.<br>
                                *  Please submit your quote urgently by return / within 3 days of receipt of this RFQ. <br><br>
                                
                             <b>Note:</b><br>
                                1.	Insurance will be covered by the vendor’s policy until the goods have been received at the agreed delivery point specified in the order and/or our separate dispatch instructions.<br>
                                2.	No additional goods to this should be supplied without our approval in writing.<br>
                                3.	Please provide appropriate certificate from class maker and/or supplier for the above items.<br>
                                
                               <br><br>          
                                Thank you & best regards,<br>
                                " + Session["USERFULLNAME"].ToString() + @"<br>
                                " + dtUser.Rows[0]["Designation"].ToString() + @"<br>"
                                  + Convert.ToString(Session["Company_Address_GL"]).Replace("\n", "<br>") + @"<br>
                                Tel: " + dtUser.Rows[0]["Mobile_Number"].ToString() + @"<br>
                                Email: " + dtUser.Rows[0]["MailID"].ToString() + @"<br>
                                 
                               <a href='" + ServerIPAdd.Trim() + "'>" + ServerIPAdd.Trim() + @"</a><br><br>"

                                  ;



        strSubject = "RFQ No. " + dsEmailInfo.Tables[0].DefaultView[0]["QTN_Contract_Code"].ToString() + " from " + Session["Company_Name_GL"];


        strBody = MailBodySection;

        //To Get Email Address of the supplier 
        sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();

        if (sEmailAddress == "")
        {
            sEmailAddress = "" + dtUser.Rows[0]["MailID"].ToString() + "";
            strSubject = "Please Add Email Address";
            strBody = "Please Add Email Address of supplier code : " + dsEmailInfo.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString();
        }



    }

    protected void FormateEmailOnRework(DataSet dsEmailInfo, bool IsExcelBaseRFQ, string strServerIPAdd, out string sEmailAddress, out string strSubject, out string strBody, bool bIsListed, string Attachment)
    {
        BLL_PURC_Purchase objpurch = new BLL_PURC_Purchase();
       
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        StringBuilder sbBody = new StringBuilder();
        sEmailAddress = "";
        strSubject = "";
        strBody = "";
        string webExl = "";
        string strfeature;
        string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
            webExl = @" Kindly quote by clicking on the below link :<br>
                  <a href=" + strServerIPAdd.Trim() + "'>'" + strServerIPAdd.Trim() + @"</a> <br>
                    User Name&nbsp;:  " + dsEmailInfo.Tables[0].Rows[0]["User_name"].ToString() + @"<br> 
                    Password&nbsp;&nbsp; :  " + dsEmailInfo.Tables[0].Rows[0]["Password"].ToString() + @"<br><br>  "
                      ;
            strfeature = @"<span style='color:Red'> “ANNOUNCEMENT OF NEW FEATURE” </span> <br>
                                We have implemented the below new features for vendors easy convenience. <br>
                                Ability to export this RFQ to Excel and send out to your counterparts.<br>
                                Not required to submit RFQ Excel quotation to office via email. Now this excel RFQ quotation can be directly uploaded to our system at your end. <br><br>
                                <hr/>";

       
        string MailBodySection = @"Dear " + dsEmailInfo.Tables[0].DefaultView[0]["First_Name"].ToString() + @"<br><br>
                               
                                " + webExl + @" <br>
                              
                                Port Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : &nbsp;" + dsEmailInfo.Tables[0].DefaultView[0]["PORT_NAME"].ToString() + @"<br>
                               <br>    
                               
                                THIS IS AN ENQUIRY ONLY. IT IS NOT AN ORDER FOR PURCHASE.<br> <br> 

                                All quotes must be submitted using this format. We will not accept quotes in any other format. <br>
                                Upon receipt of this request  Kindly complete all highlighted cells and return via  this mode of transmission. <br><br>

                                Please review quantity, unit of measure, part number, lead time(in days), delivery date  and mark changes<br> 
                                as required prior sending out the quote. <b>Failure to use this form may result in disqualification.</b><br><br>

                                Special Instruction : If you haven`t received a response from us in 20 working days, Please consider this request to be closed. <br><br>

                                We will require an estimate of total shipment weight if possible, with the quote.  <br>    
                                In the event you are awarded this requisition, Payment will only be made to the name and address listed on the Quotation. <br><br>  
  
                             <b>IMPORTANT: </b><br>
                                Terms Subject to " + Session["Company_Name_GL"].ToString() + @" standard terms and conditions of purchase.<br>
                                *  Please submit your quote urgently by return / within 3 days of receipt of this RFQ. <br><br>
                                
                             <b>Note:</b><br>
                                1.	Insurance will be covered by the vendor’s policy until the goods have been received at the agreed delivery point specified in the order and/or our separate dispatch instructions.<br>
                                2.	No additional goods to this should be supplied without our approval in writing.<br>
                                3.	Please provide appropriate certificate from class maker and/or supplier for the above items.<br>
                                
                               <br><br>          
                                Thank you & best regards,<br>
                                " + Session["USERFULLNAME"].ToString() + @"<br>
                                " + dtUser.Rows[0]["Designation"].ToString() + @"<br>"
                                  + Convert.ToString(Session["Company_Address_GL"]) +@"
                                Mobile:+65 " + dtUser.Rows[0]["Mobile_Number"].ToString() + @"<br>
                                Email: " + dtUser.Rows[0]["MailID"].ToString() + @"<br>

                               
                                 <a href='" + ServerIPAdd.Trim() + "'>" + ServerIPAdd.Trim() + @"</a><br><br>"

                                  ;



        strSubject = "Rework for RFQ No. " + dsEmailInfo.Tables[0].DefaultView[0]["QTN_Contract_Code"].ToString() + " from " + Session["Company_Name_GL"];


        strBody = MailBodySection;

        //To Get Email Address of the supplier 
        sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();

        if (sEmailAddress == "")
        {
            sEmailAddress = "" + dtUser.Rows[0]["MailID"].ToString() + "";
            strSubject = "Please Add Email Address";
            strBody = "Please Add Email Address of supplier code : " + dsEmailInfo.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString();
        }



    }





}
