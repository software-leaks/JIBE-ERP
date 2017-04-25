<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ASL_Data_Index.aspx.cs"
    Inherits="ASL_Data_Index" Title="ASL" EnableEventValidation="false" %>

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
      <script src="../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script language="javascript" type="text/javascript">

        function OpenCRScreen(ID, Eval_ID, Supplier_Type) {
            var url = 'ASL_Change_Request_Index.aspx?Supplier_code=' + ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_CR', 'Change Request', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 1.3, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenEvaluationScreen(ID, Job_ID, Supplier_Type) {
            var url = 'ASL_Evalution.aspx?Supp_ID=' + ID + '&Evaluation_ID=' + Job_ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 1.3, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }

        function OpenASLScreen(Supplier_Code) {
            var url = "../ASL/ASL_General_Data.aspx?Supp_ID=" + Supplier_Code;
            OpenPopupWindowBtnID('Manage_Contacts', 'Supplier Manage Contacts', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 1.3, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
           // OpenPopupWindowBtnID('Manage_Contacts', 'Supplier Manage Contacts', url, 'popup', 850, 1250, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
            //window.open(url, "_blank");
        }


    </script>
    <script language="javascript" type="text/javascript">
        function OpenRegisteredData(ID, Supplier_Type) {
            var Eval_ID = null;
            var url = 'ASL_Data_Entry.aspx?Supp_ID=' + ID + '&ID=' + Eval_ID + '&Supplier_Type=' + Supplier_Type;
            //OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', 850, 1250, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
            window.open(url, "_blank");
        }
        function OpenScreenEvalaution(ID,Supplier_Type) {
            var Eval_ID = null;
            var url = 'ASL_Evalution.aspx?Supp_ID=' + ID + '&Evaluation_ID=' + Eval_ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', document.body.offsetHeight/2, document.body.offsetWidth/1.2, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
//            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', 850, 1300, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreenEHistory(ID,  Supplier_Type) {
            var Eval_ID = null;
            var url = 'ASL_Evalution_History.aspx?Supp_ID=' + ID + '&Evaluation_ID=' + Eval_ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation History', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
           // OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', 850, 1400, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');

        }
        function OpenScreenDocument(ID, Supplier_Type) {

            var url = 'ASL_Supplier_Document.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Document', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Document', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenScreenRemarks(ID, Supplier_Type) {

            var url = 'ASL_Supplier_Remarks.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Remarks', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Remarks', url, 'popup', 850, 1200, null, null, false, false, true, null);
        }
        function OpenScreenSimilerName(ID, Supplier_Type) {


            var url = 'ASL_Supplier_SimilerName.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Similar Names', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
            //OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Similer Name', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenScreenEmail(ID, Supplier_Type) {

            var Eval_ID = null;
            var url = 'ASL_Email_Template.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Email Template', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
            //OpenPopupWindowBtnID('ASL_Evalution', 'Email Template', url, 'popup', 750, 1080, null, null, false, false, true, null);
        }
//        function OpenScreenRequest(ID, Supplier_Type, StatusType) {
//            var Eval_ID = null;
//            if (StatusType == "") {
//                var url = 'ASL_CR1.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
//            }
//            else if (StatusType != "")
//             {
//                 var url = 'ASL_Change_Request_Index.aspx?Supplier_code=' + ID + '&Supplier_Type=' + Supplier_Type;
//            }
//            //OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', 850, 1250, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
//            window.open(url, "_blank");
//        }
        function OpenScreenRequest(ID, Supplier_Type, StatusType) {

            var Eval_ID = null;
            if (StatusType == "") {
                var url = 'ASL_CR.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
            }
            else if (StatusType != "")
             {
                 var url = 'ASL_Change_Request_Index.aspx?Supplier_code=' + ID + '&Supplier_Type=' + Supplier_Type;
            }
             OpenPopupWindowBtnID('ASL_CR', 'Change Request', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 1.3, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
          //  OpenPopupWindowBtnID('ASL_Evalution', 'Change Request', url, 'popup', 850, 1180, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreenCHistory(ID, Supplier_Type) {


            var url = 'ASL_Change_Request_History.aspx?Supp_ID=' + ID + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Change Request History', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_Evalution', 'Change Request History', url, 'popup', 700, 1400, null, null, false, false, true, null);
        }
        function OpenScreenPayment(ID, Register_Name, Supplier_Type) {


            var url = 'ASL_Payment_History.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Payment History', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_Evalution', 'Payment History', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreenPOInvoice(ID, Register_Name, Supplier_Type) {


            var url = 'ASL_PO_Invoice.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'PO & Invoice', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
          //  OpenPopupWindowBtnID('ASL_Evalution', 'PO & Invoice', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreenInvoiceWIP(ID, Register_Name, Supplier_Type) {


            var url = 'ASL_Invoice_WIP.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Invoice WIP', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
          //  OpenPopupWindowBtnID('ASL_Evalution', 'Invoice WIP', url, 'popup', 750, 1220, null, null, false, false, true, null);
        }
        function OpenScreen10(ID, Register_Name, Supplier_Type) {


            var url = 'ASL_InvoiceStatus.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_InvoiceStatus', 'Invoice Status', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreenOutStanding(ID, Register_Name, Supplier_Type) {


            var url = 'ASL_OutStanding.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_OutStanding', 'Supplier OutStandings', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_OutStanding', 'Supplier OutStandings', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreenStatistics(ID, Register_Name, Supplier_Type) {


            var url = 'ASL_Supplier_Statistics.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_Supplier_Statistics', 'Supplier Statistics', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_Supplier_Statistics', 'Supplier Statistics', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenTransactionLog(ID, Register_Name, Supplier_Type) {

            var url = 'ASL_AuditTrail.aspx?Supp_ID=' + ID + '&Supplier_Name=' + Register_Name + '&Supplier_Type=' + Supplier_Type;
            OpenPopupWindowBtnID('ASL_AuditTrail', 'Supplier Audit Trail', url, 'popup', document.body.offsetHeight / 2, document.body.offsetWidth / 1.2, null, null, false, false, true, null);
           // OpenPopupWindowBtnID('ASL_AuditTrail', 'Supplier Audit Trail', url, 'popup', 700, 1250, null, null, false, false, true, null);
        }


        function myFunction() {

            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var w = (size.width);
            var y = (size.height);

           


        }

    </script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
        td.blog_content div
        {
            width: 100%;
            text-align: left;
            padding: 2px;
        }
        
        .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
        .divclass
        {
            width: 100%;
            background-color: #BDBDBD;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
            color:White;
        }
        .divclass1
        {
            background-color: #fff;
            border: 0.5px solid #496077;
        }
        .checkbxinput {
          color: White;
          background-color: green;
          width: 50px;
            height: 20px;
          }
    </style>
<%-- <script type="text/javascript">
     $(function () {
         $("[id*=ctl00_MainContent_gvSupplier] td").bind("click", function () {
             var row = $(this).parent();
             $("[id*=ctl00_MainContent_gvSupplier] tr").each(function () {
                 if ($(this)[0] != row[0]) {
                     $("td", this).removeClass("selected_row");
                 }
             });
             $("td", row).each(function () {
                 if (!$(this).hasClass("selected_row")) {
                     $(this).addClass("selected_row");
                 } else {
                     $(this).removeClass("selected_row");
                 }
             });
         });
     });
</script>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" defaultbutton="btnGet"
    runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <progresstemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                ASL
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <contenttemplate>
                        <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right">
                                        Type :
                                    </td>
                                    <td  align="left"  style=" width:40%;" >
                                   
                                        <asp:CheckBoxList ID="chkType" RepeatDirection="Horizontal" runat="server">
                                        </asp:CheckBoxList>
                                       
                                    </td>
                                     <td align="right" style="width:10%;">
                                        Current Status :
                                    </td>
                                    <td  align="left" style="margin-top:5px; width:25%;" >
                                    <table><tr>
                                    <td align="left" style="background:green; color:White;" ><asp:CheckBox ID="chkApproved" runat="server"  Text="Approved"  /></td>
                                    <td align="left" style="background:White;   color:Black;">  <asp:CheckBox ID="chkCond" runat="server" Text="Conditional" /></td>
                                    <td align="left" style="background:red;   color:White;">   <asp:CheckBox ID="chkBlack" runat="server" Text="Black List"  /></td>
                                    <td align="left" style="background:White;   color:Black;">   <asp:CheckBox ID="chkUnreg" runat="server" Text="Unregistered"  /></td>
                                    <td align="left" style="background:Yellow;  color:Black;">  <asp:CheckBox ID="chkexp" runat="server" Text="Expired" /></td></tr></table>
                                        
                                      
                                     
                                     
                                      
                                    </td>
                                     <td align="left" style="margin-top:5px; width:5%;">
                                       <asp:ImageButton ID="btnGet" runat="server" OnClick="btnGet_Click" ToolTip="Search"  
                                            ImageUrl="~/Images/SearchButton.png" /> &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" rowspan="4" style="margin-top:5px; width:20%;">
                                    <div class="divclass">
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr><td>
                                      <asp:Image ID="Image17" runat="server"  ImageUrl="~/Images/Green.png" />
                                        &nbsp;
                                            <asp:Label ID="Label4" runat="server" Text="Approved"></asp:Label>
                                        
                                    </td></tr><tr><td>
                                         <asp:Image ID="Image18" runat="server"  ImageUrl="~/Images/Red.png" />&nbsp;&nbsp;
                                      <asp:Label ID="Label3" runat="server" Text="Black List"  ></asp:Label> </td></tr><tr><td>
                                        <asp:Image ID="Image19" runat="server"  ImageUrl="~/Images/White.png" />&nbsp;&nbsp;
                                      <asp:Label ID="Label2" runat="server" Text="Conditional/Unregistered"  ></asp:Label> </td></tr><tr><td>
                                       <asp:Image ID="Image20" runat="server"  ImageUrl="~/Images/Yellow.png" />&nbsp;&nbsp;
                                      <asp:Label ID="Label5" runat="server" Text="Expired"  ></asp:Label> </td></tr>
                                      </table>
                                      </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" align="right">
                                      Supply Port :  
                                    </td>
                                    <td style="width: 40%" align="left">
                                      <asp:DropDownList ID="ddlSupplyPort" runat="server" CssClass="txtInput" Width="200px">
                                        </asp:DropDownList>    
                                    </td>
                                    <td style="width: 10%" align="right">
                                        Evaluation Status :
                                    </td>
                                    <td style="width: 25%" align="left">
                                        <asp:DropDownList ID="ddlEvaluationStatus" runat="server" CssClass="txtInput" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="margin-top:5px; width:5%;">
                                         <asp:ImageButton ID="ImageRefresh" runat="server"  Text="Search" ToolTip="Refresh"   
                                            ImageUrl="~/Images/Refresh-icon.png" OnClick="ImageRefresh_Click" />
                                    </td>
                                <%--    <td align="left" style="margin-top:5px; width:20%;">
                                    
                                    </td>--%>
                                   
                                </tr>
                                   <tr>
                                    <td align="right">
                                        Supplier Description :
                                    </td>
                                    <td align="left" style="width: 40%">
                                      <asp:TextBox ID="txtSupplierDesc" runat="server" Width="400px" MaxLength="250" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSupplierDesc"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right">
                                        Supplier Scope :
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:DropDownList ID="ddlSupplyType" runat="server" CssClass="txtInput" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    
                                    <td align="left" style="margin-top:5px; width:5%;">
                                       <asp:ImageButton ID="btnNewSupplier0" runat="server" 
                                            OnClientClick="OpenASLScreen(00000);return false;" ToolTip="Add New Supplier"  
                                            Text="Create New Supplier" ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                   <%-- <td align="left" style="margin-top:5px; width:20%;">
                                     
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td align="right">
                                      Name/Code : </td>
                                    <td align="left"  style="width: 40%">
                                         <asp:TextBox ID="txtfilter" runat="server" Width="400px" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />  </td>
                                     <td align="right">
                                     
                                       Credit Balance :</td>
                                    <td align="left"  style="width: 25%">
                                       <asp:CheckBox ID="chkCredit" runat="server" /></td>
                                    <td align="left" style="margin-top:5px; width:5%;">
                                       <asp:ImageButton ID="ImageSimilar" runat="server" 
                                                    OnClientClick='OpenScreenSimilerName(null,null);return false;' Text="Search" ToolTip="Similar Names"  
                                            ImageUrl="~/Images/equal.png" />
                                    </td>
                                  <%--  <td align="left" style="margin-top:5px; width:20%;">
                                    
                                    </td>--%>
                                 
                                </tr>
                              
                            </table>
                        </div>
                        <div id="divEvaluation" runat="server" visible="false" style="height: 180px; overflow-y: scroll;
                            max-height: 180px">
                            <table>
                                <tr>
                                    <td align="left" style="width: 8%; color: red;">
                                       <asp:Label ID="lblEvaluation" runat="server" Text="ASL Evaluation Pending List"></asp:Label>  
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvEvaluation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="0" CellSpacing="0" Width="100%" GridLines="both"
                                CssClass="gridmain-css" AllowSorting="true" 
                                EnableSortingAndPagingCallbacks="True"  AllowPaging="true" PageSize="5" 
                                onpageindexchanging="gvEvaluation_PageIndexChanging"
                                onrowdatabound="gvEvaluation_RowDataBound" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="lblPStatus">
                                        <HeaderTemplate>
                                            Pending Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:HyperLink ID="lblPStatus" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Eval_Status") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                            <%--<asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("Eval_Status")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="SCode">
                                        <HeaderTemplate>
                                            Scode
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:HyperLink ID="lblScode" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("ID") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                            <%--<asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("Eval_Status")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="3%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                            Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblPType" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Supplier_Type") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                          <%--  <asp:Label ID="lblPType" runat="server" Text='<%#Eval("Supplier_Type")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="25px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Propose Type">
                                        <HeaderTemplate>
                                            Supplier Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblSName" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Supplier_Name") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                               
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="16%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Current ASL Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblCStatus" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("ASL_Status") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                            <%--<asp:Label ID="lblCStatus" runat="server" Text='<%#Eval("ASL_Status")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Propose Type">
                                        <HeaderTemplate>
                                            Proposed By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblPBY" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Proposed_By") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           <%-- <asp:Label ID="lblPBY" runat="server" Text='<%#Eval("Proposed_By")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Propose Type">
                                        <HeaderTemplate>
                                           For 1st Approval 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblApprovedby" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("ForApproval") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           <%-- <asp:Label ID="lblApprovedby" runat="server" Text='<%#Eval("ForApproval")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Propose Type">
                                        <HeaderTemplate>
                                           For Final Approval
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblFinalApprovedBy" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("ForFinalApproval") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                            <%--<asp:Label ID="lblFinalApprovedBy" runat="server" Text='<%#Eval("ForFinalApproval")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Info Status">
                                        <HeaderTemplate>
                                            Proposed Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblProStatus" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Propose_Status") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           <%-- <asp:Label ID="lblProStatus" runat="server" Text='<%#Eval("Propose_Status")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved Type">
                                        <HeaderTemplate>
                                           1st Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblApby" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Approved_By") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                          <%--  <asp:Label ID="lblApby" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved Type">
                                        <HeaderTemplate>
                                           Final Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblFinalpby" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("FinalApproved_By") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                            <%--<asp:Label ID="lblFinalpby" runat="server" Text='<%#Eval("FinalApproved_By")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Urgent">
                                        <HeaderTemplate>
                                            Urgent
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblUrgent" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Urgent_Flag") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           <%-- <asp:Label ID="lblUrgent" runat="server" Text='<%#Eval("Urgent_Flag")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="3%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Urgent">
                                        <HeaderTemplate>
                                            Action by
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblAction_by" runat="server" Font-Underline="false" Width="100%" ForeColor="Red"  Text='<%#Eval("Action_by") %>'
                                           onclick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           <%-- <asp:Label ID="lblAction_by" runat="server" ForeColor="Red" Text='<%#Eval("Action_by")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update" OnClientClick='<%#"OpenEvaluationScreen((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("[Evaluation_ID]") + "&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>'
                                                            CommandArgument='<%#Eval("[ID]") + "," + Eval("[Evaluation_ID]") %>' CommandName="Select"
                                                            ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png" Height="10px" Width="15px">
                                                        </asp:ImageButton>
                                                  
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="3%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>
                         <div id="divCR" runat="server" style="height: 5px; max-height: 5px">
                        </div>
                        <div id="divChnageRequest" runat="server" visible="false" style="height: 180px; overflow-y: scroll;
                            max-height: 180px">
                            <table>
                                <tr>
                                    <td align="left" style="width: 8%; color: Blue;">
                                     <asp:Label ID="lblChangeRequest" runat="server" Text="Data Change Request pending for following supplier"></asp:Label>    
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvChangeRequest" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="Supplier_Code" CellPadding="0" CellSpacing="0"
                                Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" 
                                EnableSortingAndPagingCallbacks="True" AllowPaging="true" PageSize="5" 
                                onpageindexchanging="gvChangeRequest_PageIndexChanging"
                                onrowdatabound="gvChangeRequest_RowDataBound" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Supplier Code">
                                        <HeaderTemplate>
                                            Supplier Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblSCode" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Supplier_Code") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                          
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                           Supplier Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblRegisterName" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Supplier_Type") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                          
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <HeaderTemplate>
                                            Supplier Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblSName" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Supplier_Name") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                      
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="16%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Changes">
                                        <HeaderTemplate>
                                            Changes
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblChange" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("ChangeCount") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="2%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Group Name">
                                        <HeaderTemplate>
                                            Group Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblGroup_Name" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Group_Name") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Submitted By">
                                        <HeaderTemplate>
                                            Submitted By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblVerified_By" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Verified_By") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                          
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver Name">
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblSend_For_Approve" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Send_For_Approve") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Final Approver Name">
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblSend_For_Final_Approve" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Send_For_Final_Approve") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved By">
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblApproved_By" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Approved_By") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                          
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Urgent">
                                        <HeaderTemplate>
                                            Action by
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblAction_by" runat="server" Font-Underline="false" Width="100%" ForeColor="Red"  Text='<%#Eval("Action_by") %>'
                                           onclick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>' >HyperLink</asp:HyperLink>
                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                        <asp:ImageButton ID="ImageButton1" runat="server" Text="Update" OnClientClick='<%#"OpenCRScreen((&#39;"+ Eval("Supplier_Code") +"&#39;),(&#39;" + Eval("[Group_Name]") +"&#39;),(&#39;"+ Eval("[Supplier_Type]") + "&#39;));return false;"%>'
                                                            CommandArgument='<%#Eval("[Supplier_Code]")%>' CommandName="Select" ForeColor="Black"
                                                            ToolTip="View" ImageUrl="~/Images/asl_view.png" Height="10px" Width="15px"></asp:ImageButton>
                                                   
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="2%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager2" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                        </div>
                      
                            <div style="height: 15px; max-height: 15px">
                           </div>
                           
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <asp:GridView ID="gvSupplier" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="gvSupplier_RowDataBound" DataKeyNames="Supplier_Code,Propose_Type,Register_Name,PassString,Status" 
                                CellPadding="0" CellSpacing="0"
                                Width="100%" GridLines="both" OnSorting="gvSupplier_Sorting" CssClass="gridmain-css"
                                AllowSorting="true" onrowcreated="gvSupplier_RowCreated" 
                                onrowcommand="gvSupplier_RowCommand"  >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                         <asp:LinkButton ID="lblProposeType" runat="server" CommandName="Sort" CommandArgument="Propose_Type" >Type</asp:LinkButton> 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                      
                                        <asp:LinkButton ID="lblSCode" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Propose_Type") %>'
                                          CommandName="VIEW"   ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SCode">
                                        <HeaderTemplate>
                                          <asp:LinkButton ID="lblSupplier_Code" runat="server" CommandName="Sort" CommandArgument="Supplier_Code" >SCode</asp:LinkButton>   
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                          <asp:LinkButton ID="Label1" runat="server" CommandName="View"  Font-Underline="false" CommandArgument="Supplier_Code"  ForeColor="Black"  Text='<%#Eval("Supplier_Code") %>'    >SCode</asp:LinkButton>  
                                       
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Propose Type">
                                        <HeaderTemplate>
                                         <asp:LinkButton ID="lblRegister_Name" runat="server" CommandName="Sort" CommandArgument="Register_Name" >Company Registered Name </asp:LinkButton>    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:LinkButton ID="HyperLink1" runat="server" CommandName="View"  Font-Underline="false" Width="100%" ForeColor="Black" Text='<%#Eval("Register_Name") %>'
                                            ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="18%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                        <asp:LinkButton ID="lblAmt" runat="server" CommandName="Sort" CommandArgument="Amt" >Out.USD Amt</asp:LinkButton>     
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lblASL_Amt" CommandName="View"  runat="server" Font-Underline="false" Width="100%" ForeColor="Black" Text='<%#Eval("Amt") %>'
                                           ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                           <asp:LinkButton ID="lblSupp_Status" runat="server" CommandName="Sort" CommandArgument="Supp_Status" >Current Status</asp:LinkButton>      
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lblASL_Status"  CommandName="View" runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Supp_Status") %>'>
                                           </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status valid till">
                                        <HeaderTemplate>
                                          <asp:LinkButton ID="lblExpire_On" runat="server" CommandName="Sort" CommandArgument="Expire_On" >Valid Upto</asp:LinkButton>        
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lblStatusValid" CommandName="View"  runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Expire_On","{0:dd-MMM-yyyy}") %>'
                                            ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Propose Type">
                                        <HeaderTemplate>
                                           <asp:LinkButton ID="lblProposed_Status" runat="server" CommandName="Sort" CommandArgument="Proposed_Status" > Proposed Status</asp:LinkButton>    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lblProposeStatus" CommandName="View"  runat="server" Font-Underline="false" Width="100%" ForeColor="Black"  Text='<%#Eval("Proposed_Status") %>'
                                            ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Info Status">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblAction_On_Data_Form" runat="server" CommandName="Sort" CommandArgument="Action_On_Data_Form" >Info Status</asp:LinkButton>     
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lblSupplierAction" CommandName="View"  runat="server" Font-Underline="false" Width="100%" ForeColor="Black" Text='<%#Eval("Action_On_Data_Form") %>'
                                           ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="10px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                        <asp:ImageButton ID="ImgView" runat="server" CommandName="ImgView" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Edit/View" ImageUrl="~/Images/edit.gif"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                             REGD Data
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                        <asp:ImageButton ID="ImgREGD" runat="server" CommandName="ImgREGD" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Registered Data" ImageUrl="~/Images/data.png"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            New
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                        <asp:ImageButton ID="ImgEvaluateN" runat="server" CommandName="ImgEvaluateN" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>'  
                                                            ForeColor="Black" ToolTip="New Evaluation" ImageUrl="~/Images/Evaluation.png"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            History
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                           
                                                        <asp:ImageButton ID="ImgEvaluateL" runat="server" CommandName="ImgEvaluateL" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>'  
                                                            ForeColor="Black" ToolTip="Evaluation History" ImageUrl="~/Images/Evaluation History2.png"></asp:ImageButton>
                                               
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Docs
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                        <asp:ImageButton ID="ImgSupplierD" runat="server" CommandName="ImgSupplierD" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>'  
                                                            ForeColor="Black" ToolTip="Supplier Documents" ImageUrl="~/Images/Document2.png"></asp:ImageButton>
                                                   
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         
                                                        <asp:ImageButton ID="ImgSupplierR" runat="server" CommandName="ImgSupplierR" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Supplier Remarks" ImageUrl="~/Images/remarks.jpg"></asp:ImageButton>
                                              
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Email
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                        <asp:ImageButton ID="ImgEmail" runat="server" CommandName="ImgEmail" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Email Template" ImageUrl="~/Images/EMail.png"></asp:ImageButton>
                                                   
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                 
                                      <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Request
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                                        <asp:ImageButton ID="ImgChangeR" runat="server" CommandName="ImgChangeR" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[Status]") %>'   
                                                            ForeColor="Black" ToolTip="Change Request" ImageUrl="~/Images/change.png"></asp:ImageButton>
                                                    
                                              
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            History
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                          
                                                     
                                                        <asp:ImageButton ID="ImgChangeH" runat="server" CommandName="ImgChangeH" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Change Request History" ImageUrl="~/Images/change history.jpg"></asp:ImageButton>
                                                   
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Invoice
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                                        <asp:ImageButton ID="ImgInvoice" runat="server" CommandName="ImgInvoice" Width="15px" Height="12px" Text="Update"   CommandArgument='<%#Eval("[PassString]")%>' 
                                                            ForeColor="Black" ToolTip="Invoice Status" ImageUrl="~/Images/invoice-icon.png"></asp:ImageButton>
                                                           
                                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Payment
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                                      
                                               <asp:ImageButton ID="ImgPayment" runat="server" Width="15px" CommandName="ImgPayment" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Payment History" ImageUrl="~/Images/payment history.jpg"></asp:ImageButton>             
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            PO/Inv
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                                    <asp:ImageButton ID="ImgInvoicePO" runat="server" CommandName="ImgInvoicePO" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>'  
                                                            ForeColor="Black" ToolTip="PO & Invoice" ImageUrl="~/Images/po.png"></asp:ImageButton>   
                                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            WIP
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                                  <asp:ImageButton ID="ImgInvoiceWIP" runat="server" CommandName="ImgInvoiceWIP" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Invoice WIP" ImageUrl="~/Images/invoice wip.png"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Out.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                                <asp:ImageButton ID="ImgOutstandings" runat="server" CommandName="ImgOutstandings" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Outstanding" ImageUrl="~/Images/outstandings.jpg"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Stats
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgStatistics" runat="server" CommandName="ImgStatistics" Width="15px" Height="12px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Statistics" ImageUrl="~/Images/statistics.jpg"></asp:ImageButton> 
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            TX.Log
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                          
                                                     <asp:ImageButton ID="ImgTransaction" runat="server" CommandName="ImgTransaction" Width="15px" Height="15px" Text="Update" CommandArgument='<%#Eval("[Supplier_Code]")%>' 
                                                            ForeColor="Black" ToolTip="Transaction Log" ImageUrl="~/Images/txhistory.png"></asp:ImageButton>
                                                           
                                                  
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
