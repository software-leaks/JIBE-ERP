<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ASL_Supplier_CreditNote_Upload.aspx.cs"
    Inherits="ASL_ASL_Supplier_CreditNote_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function Validation() {

            if (document.getElementById("ctl00_MainContent_txtAirPortName").value == "") {
                alert("Please enter airport name.");
                document.getElementById("ctl00_MainContent_txtAirPortName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtElevation").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtElevation").value)) {
                    alert("Elevation allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtElevation").focus();
                    return false;
                }
            }

            return true;
        }

        function OpenScreen1(ID, Job_ID) {

            var url = 'ASL_Evalution_History.aspx?Supp_ID=' + ID;
            OpenPopupWindowBtnID('ASL_Evalution_History', 'Evalution History', url, 'popup', 300, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenScreen(ID, Job_ID) {

            var url = 'ASL_Evalution.aspx?ID=' + ID + '&Supp_ID=' + Job_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evalution', url, 'popup', 800, 1030, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <center>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="dialog" style="display: none">
                </div>
                <div id="page-title" class="page-title">
                    Supplier Upload Document
                </div>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td align="left" colspan="4" style="width: 15%">
                            <b>Purchase Order Details</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                                <table id="Divstep1" visible="false" runat="server" width="100%">
                                    <tr>
                                        <td align="left" colspan="4" style="width: 10%">
                                            <strong>Step 1 : Confirm the Purchase order which the Credit Note is to be matched.</strong><br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            Vessel Name:
                                        </td>
                                        <td align="left" style="width: 40%">
                                            <asp:Label ID="lblVessel" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" style="width: 10%">
                                            Ref:
                                        </td>
                                        <td align="left" style="width: 40%">
                                            <asp:Label ID="lblRef" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            Dated:
                                        </td>
                                        <td align="left" style="width: 40%">
                                            <asp:Label ID="lblDated" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" style="width: 10%">
                                            Amount:
                                        </td>
                                        <td align="left" style="width: 40%">
                                            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        <div style="display:none;" >
                                           <asp:TextBox ID="txtFileID" runat="server"></asp:TextBox> <br /></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 40%">
                                            <asp:Button ID="btnClose" runat="server" Text="Cancel And Close" />
                                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                                <table id="Divstep2" runat="server" visible="false">
                                    <tr>
                                        <td align="left" colspan="4" style="width: 15%">
                                            <strong>Step 2 : Upload scanned Credit Note attachment. Please read following instructions.</strong><br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="color: Red;">
                                            You are allowed to upload only ONE file per Purchase Order. Please combine supporting
                                            documents (including signed delivery receipts) together with your invoice into a
                                            single file. To allow easier processing and viewing, please prepare the file as
                                            PDF Format. You can download a free PDF Writer from this website http://www.dopdf.com/download.php.
                                            <br />
                                            You are to limit your file size to less than 2 MB.
                                            <br />
                                            The file name should contain alphanumeric characters only.<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="width: 70%">
                                            <asp:FileUpload ID="FileUploader" runat="server" Style="width: 50%; height: 18px;
                                                background-color: #F2F2F2; border: 1px solid #cccccc; font-size: 12px; cursor: pointer" />
                                            &nbsp;<asp:Button ID="btnUpload" runat="server" Height="20px" Style="height: 18px;
                                                border: 1px solid #cccccc; font-size: 12px; cursor: pointer" Text="Add Attachment"
                                                Width="100px" OnClick="btnUpload_Click" />
                                            <p style="color: Blue;">
                                                (Maximum file size 2MB, File name should contain only AlphaNumberic Characters.)</p>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                                <table id="DivStep3" runat="server" visible="false">
                                    <tr>
                                        <td align="left" colspan="4" style="width: 15%">
                                            <strong>Step 3 : Update Credit Note details and remarks. </strong>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="color: Red;">
                                            You are required to enter data into all the fields provided, Only remarks is optional.
                                            Once done, click on the [Update Credit Note Data] to save the data.<br />
                                            The [Submit for Processing] button will only be enabled after the required fields
                                            are complete.<br />
                                            Invoice Currency must be the same as Purchase Order Currency. If there are additional
                                            invoices relating to the Purchase Order, please email them to the respective buyers
                                            for processing.<br />
                                            Once you have uploaded the electronic invoice, you are no longer required to send
                                            us the Original Invoice.<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%">
                                            File Name:
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:Label ID="lblFileName" Width="400px" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" width="10%">
                                            Inv Ref:
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:TextBox ID="txtInvioceRef" Width="400px" CssClass="txtInput" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Inv Dated:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtInvoiceDate" Width="400px" CssClass="txtInput" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender TargetControlID="txtInvoiceDate" ID="CalendarExtender1" Format="dd-MM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            Invoice Amount (Plus taxes):
                                        </td>
                                        <td align="left" style="width: 10%">
                                            <asp:TextBox ID="txtInvoiceAmount" Width="400px" CssClass="txtInput" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Tax Amount:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTaxAmount" Width="400px" CssClass="txtInput" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Due Date:
                                        </td>
                                        <td align="left" style="width: 10%">
                                            <asp:TextBox ID="txtDueDate" Width="400px" CssClass="txtInput" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender TargetControlID="txtDueDate" ID="caltxtArrival" Format="dd-MM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Remarks (Optional):
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtRemarks" Width="400px" CssClass="txtInput" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <br />
                                            <br />
                                            <asp:Button ID="btnUploadInvoice" runat="server" Text="Update Credit Note Data" OnClick="btnUploadInvoice_Click" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit for Processing" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete File" OnClick="btnDelete_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                                <table id="DivStep4" runat="server" visible="false">
                                    <tr>
                                        <td colspan="4" align="center" style="color: #FF0000; font-size: small;">
                                            <br />
                                            <br />
                                            Credit Note Ref :
                                            <asp:Label ID="lblInvoiceReferance" runat="server" Text=""></asp:Label>
                                            Dated:
                                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                            of amount
                                            <asp:Label ID="lblInvoiceAmount" runat="server" Text=""></asp:Label>
                                            has been uploaded.<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" >
                                            <asp:Button ID="btnRecallInvoice" runat="server" Enabled="false" Text="Recall Credit Note"
                                                OnClick="btnRecallInvoice_Click" />
                                        </td>
                                        <td align="left" colspan="3" >
                                            <asp:Button ID="btnExit" runat="server" Text="Exit And Close" OnClick="btnExit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="divIframe" runat="server" visible="false" style="border: 1px solid #cccccc; height:500px; font-family: Tahoma;
                    font-size: 12px; width: 100%;">
                    <iframe id="iFrame1" runat="server" width="100%" height="100%"></iframe>
                </div>
            </div>
        </center>
        <div>
            <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
        </div>
    </center>
</asp:Content>
