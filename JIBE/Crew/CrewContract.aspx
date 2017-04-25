<%@ Page Title="Crew Employment Agreement" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewContract.aspx.cs" Inherits="Crew_CrewContract" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintDiv(dvID) {
            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }

        function getBrowserName() {
            var Browser = "";
            if (navigator.appVersion.indexOf("MSIE") != -1) Browser = "MSIE";
            if (navigator.appVersion.indexOf("Chrome") != -1) Browser = "Chrome";
            if (navigator.appVersion.indexOf("Firefox") != -1) Browser = "Firefox";
            return Browser;
        }
        function getOSName() {
            var OSName = "";
            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";
            return OSName;
        }

        function showDialog() {
            document.getElementById('dvAttachment').style.display = "block";
         }
        function closeDialog() {
            document.getElementById('dvAttachment').style.display = "none";
        }
        function ContractDownloaded(msg) {
            return confirm(msg);
        }

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnCurrentDocID" runat="server" />
            <div style="border: 1px solid #B6DAFD; background-color: #E8F3FE; padding: 2px; margin-bottom: 5px;">
                <table style="width: 100%">
                    <tr>
                        <td style="font-weight: bold">
                            <div class="error-message" style="text-align: left">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                              <asp:Button ID="ucUploadCrewContract1" runat ="server"  
                                Text="Upload digitally signed copy" BorderStyle="Solid"   
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" 
                                Width="190px"  OnClientClick="showDialog();return false;" />
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                            <asp:Button ID="btnRollbackDigitalSign" runat="server" Text="Rollback Digital Sign"
                                BorderStyle="Solid" OnClick="btnRollbackDigitalSign_Click" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="160px" />
                            
                            <asp:Button ID="btnRollbackStaffSign" runat="server" Text="Rollback Staff Sign"
                                BorderStyle="Solid" OnClick="btnRollbackStaffSign_Click" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="160px" />

                            <asp:Button ID="btnUnVerify" runat="server" Text="Un-Verify Contract"
                                BorderStyle="Solid" OnClick="btnUnVerify_Click" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="160px" />
                            
                            <asp:Button ID="btnVerify" runat="server" Text="Verify" BorderStyle="Solid" OnClick="btnVerify_Click"
                                OnClientClick="return confirm('Are you sure, you want to verify the document?')"
                                BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5"
                                Width="160px" />

                            <asp:Button ID="btnGenerateContract" runat="server" Text="Re-Generate Contract" BorderStyle="Solid"
                                OnClick="btnGenerateContract_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5" Width="160px" />

                            <asp:Button ID="btnDownload" runat="server" Text="Download PDF" BorderStyle="Solid"
                                OnClick="btnDownload_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5" Width="160px" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="pnlSideLetterExists" runat="server">
                <div style="border: 1px solid #B6DAFD; background-color: #F5F6CE; padding: 2px; margin-bottom: 5px;font-size:12px;color:Black;">
                    <table>
                        <tr>                            
                            <td><asp:Image ID="imgDownloadSideletter" runat="server" ImageUrl="~/Images/sideletter.png" Height="25px" /></td>
                            <td><asp:Label ID="lblSideLetter" runat="server" Text="There is a side letter of US$ 100.00 exists for this contract." Font-Size="14px" ForeColor="Red"></asp:Label></td>
                            <td><asp:LinkButton ID="lnkDownloadSideLetter" runat="server" Text="Download" OnClick="lnkDownloadSideLetter_Click"></asp:LinkButton></td>
                            
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlDigiSignUploader_Mac" runat="server" Visible="false">
        <div style="border: 1px solid #B6DAFD; background-color: #ECF6CE; padding: 2px; margin-bottom: 5px;">
            <table style="width: 100%">
                <tr>
                    <td>
                        <img src="../Images/doc-upload.png" />
                    </td>
                    <td style="font-weight: bold">
                        <div class="error-message" style="text-align: left">
                            <asp:Label ID="Label1" runat="server" Text="Upload digitally signed copy"></asp:Label>
                        </div>
                    </td>
                    <td>
                        <div style="background-color: White; padding: 2px 10px 2px 10px; border: 1px solid gray;">
                            <asp:FileUpload ID="FileUploader1" runat="server" Width="300px" />
                            <asp:Button ID="btnUploadAttachments" runat="server" Text="Upload" OnClick="btnUploadAttachments_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel Width="100%" runat="server">
        <center>
            <div id="dvAttachment" title="Add Attachments" style="display:none; width: 500px; border:1px solid #cccccc;"  >           
                    <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="Label3" runat="server"></asp:Label>
        </div>
                    <table style="width: 100%;">
            
            <tr>
                <td>
                    <b>Attachment :</b>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                        onclick="btnUpload_Click" >
                    </asp:Button>
                        <asp:Button ID="btnClose" runat="server" Text="Close"  OnClientClick="closeDialog();return false;">
                    </asp:Button>
                    
                </td>
            </tr>
            <tr>
            <td>
            <asp:Label id ="lblhdn"  runat="server" Visible = "false"></asp:Label>
            <asp:TextBox ID="txthdn" runat="server" Visible = "false"></asp:TextBox>
            </td>
            </tr>
        </table>          
            </div>
    </center>
    </asp:Panel>





    <div id="dvAgreement" style="border: 1px solid #B6DAFD; background-color: #E8F3FE;
        padding: 2px; font-family: Arial; color: Black;">
        <asp:Panel ID="pnlContract" runat="server">
            <div style="margin: 5px;">
                <table style="width: 100%">
                    <tr>
                        <td style='width: 250px; height: 50px; border: 1px solid #B6DAFD; background-color: white;
                            vertical-align: top;'>
                            <asp:UpdatePanel ID="UpdatePanel_Steps" runat="server">
                                <ContentTemplate>
                                    <asp:FormView ID="frmCrewDetails" runat="server">
                                        <ItemTemplate>
                                            <table cellpadding="3">
                                                <tr>
                                                    <td colspan="3" style="font-weight: bold">
                                                        Staff Details:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        Staff Code:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStaffCode" runat="server" Text='<%#Eval("Staff_Code") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        Staff Name:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStaffName" runat="server" Text='<%#Eval("CrewName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        Rank:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank_Name") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        Vessel:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        Contract Date:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblContractDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Contract_Date"))) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        EOC Date:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEOC" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("COCDate"))) %>' ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        Duration:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMonths" runat="server" Text='<%#Eval("MonthDays") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:FormView>
                                    <div style="margin-top: 20px">
                                        <asp:Repeater runat="server" ID="rptSteps">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="3" style="font-weight: bold">
                                                            Steps Completed:
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="imgStep" runat="server" ImageUrl="~/Images/Ok-icon.png" Height="16px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStep" runat="server" Text='<%#Eval("StepText") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div style="margin-top: 20px">
                                        <asp:Repeater runat="server" ID="rpt1" OnItemCommand="rpt1_ItemCommand">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="3" style="font-weight: bold">
                                                            Document History:
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="color: Black">
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td>
                                                        <img src="../Images/DocTree/pdf.gif" alt='' />
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <asp:LinkButton ID="lnkAgreement" runat="server" Text='<%#Eval("DocName") %>' CommandName="ViewDocument"
                                                            CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table></FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <asp:Panel ID="pnlVerified" runat="server" Visible="false">
                                        <div style="margin-top: 20px">
                                            <table>
                                                <tr>
                                                    <td colspan="3" style="font-weight: bold">
                                                        Verify Status:
                                                    </td>
                                                </tr>
                                                <tr style="color: Black">
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td style="text-align: left">
                                                        Verified By:
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <asp:Label ID="lblVerifiedBy" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="color: Black">
                                                    <td>
                                                    </td>
                                                    <td style="text-align: left">
                                                        Date:
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <asp:Label ID="lblVerifiedDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style='border: 1px solid #B6DAFD; background-color: white; vertical-align: top;'>
                           <asp:UpdatePanel ID="UpdatePanel_Frame" runat="server">
                                <ContentTemplate>
                                    <iframe id="frmContract" src="" runat="server" style="width: 100%; height: 600px;
                                        border: 0;"></iframe>
                                </ContentTemplate>
                            </asp:UpdatePanel>                                  
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlContract_" runat="server" Visible="false">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Repeater runat="server" ID="rpt2" OnItemCreated="rpt2_ItemCreated">
                        <ItemTemplate>
                            <tr style="background-color: #CEF6F5; color: Black">
                                <td>
                                    <%#Eval("EarningDeduction")%>
                                </td>
                                <td>
                                    <%#Eval("Name") %>
                                </td>
                                <td style="text-align: right">
                                    USD
                                </td>
                                <td style="text-align: right">
                                    <%# Eval("Amount", "{0:00.00}")%>
                                </td>
                                <td>
                                    &nbsp;&nbsp;<%#Eval("PayableAt").ToString().Replace("BOC", "One-Time").Replace("MOC", "Per Month").Replace("EOC", "One-Time")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr style="background-color: #CEF6F5; color: Black; font-weight: bold;">
                                <td colspan="2">
                                    Total
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCurrency" runat="server" Text="USD"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;&nbsp;<asp:Label ID="lblPerMonth" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
     <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                    <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                        Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                        MaximumNumberOfFiles="10" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                 <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" />
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
</asp:Content>
