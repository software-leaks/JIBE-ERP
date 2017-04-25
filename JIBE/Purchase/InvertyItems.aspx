<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InvertyItems.aspx.cs"
    Inherits="InvertyItems" Title="Create New Requisition" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="headcontent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.js"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <style type="text/css">
        .tbl
        {
            border: 1px solid gray;
            height: 90px;
        }
        
        .view-header
        {
            font-size: 14px;
            font-family: Calibri;
            font-weight: bold;
            text-align: left;
            width: 100%;
            color: Black;
            background-color: #81DAF5;
            border-collapse: collapse;
            padding: 2px 0px 2px 3px;
        }
        
        .tbl-content
        {
            border: 0px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
        }
        
        .tbl-footer
        {
            border-bottom: 1px solid #81DAF5;
            border-left: 1px solid #81DAF5;
            border-right: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
            padding: 2px 2px 2px 2px;
        }
        
        .tbl-footer-td
        {
            width: 100%;
            padding: 2px 2px 2px 2px;
            text-align: left;
            background-color: #81DAF5;
        }
        .tdh
        {
            text-align: right;
            padding: 3px 2px 3px 0px;
        }
        .tdd
        {
            text-align: left;
            padding: 3px 2px 3px 0px;
        }
    </style>
    <script language="javascript" type="text/javascript">
//        function Validate() {
//            debugger;
//            var chk = 0;
//            var ddlPOType = document.getElementById("ctl00_MainContent_ddlPOType").value;

//            var ddlReqsnType = document.getElementById("ctl00_MainContent_ddlReqsnType").value;
//            var ddlFunction = document.getElementById("ctl00_MainContent_ddlFunction").value;
//            var ddlAccountType = document.getElementById("ctl00_MainContent_ddlAccountType").value;
//            var ddlCatalogue = document.getElementById("ctl00_MainContent_ddlCatalogue").value;
//           
//           var ddlVessel = document.getElementById("ctl00_MainContent_ddlVessel").value;



//           var lbl1 = document.getElementById("ctl00_MainContent_lbl1").innerHTML;
//           
//           var Label2 = document.getElementById("ctl00_MainContent_Label2").innerHTML;
//           var Label9 = document.getElementById("ctl00_MainContent_Label9").innerHTML;
//           var Label6 = document.getElementById("ctl00_MainContent_Label6").innerHTML;

//           var Label7 = document.getElementById("ctl00_MainContent_Label7").innerHTML;
//          
//           var Label8 = document.getElementById("ctl00_MainContent_Label8").innerHTML;
//          
//            if (lbl1 == "*" && ddlPOType == "0") { chk++; }
//            if (Label2  == "*" && ddlReqsnType == "0") { chk++; }
//            if (Label9 == "*" && ddlFunction == "0") { chk++; }
//            if (Label6  == "*" && ddlAccountType == "0") { chk++; }
//            if (Label7  == "*" && ddlCatalogue == "0") { chk++; }
//            if (Label8 == "*" && ddlVessel == "0") { chk++; }
//            
//            if (chk  == 0) {
//                document.getElementById("ctl00_MainContent_btnRequisition").style.backgroundColor = '#FFFF00';
//            } else { document.getElementById("ctl00_MainContent_btnRequisition").style.backgroundColor = '#FFF'; }
//        }
//     function CheckValidate(){
//        var chk = 0;
//            var ddlPOType = document.getElementById("ctl00_MainContent_ddlPOType").value;

//            var ddlReqsnType = document.getElementById("ctl00_MainContent_ddlReqsnType").value;
//            var ddlFunction = document.getElementById("ctl00_MainContent_ddlFunction").value;
//            var ddlAccountType = document.getElementById("ctl00_MainContent_ddlAccountType").value;
//            var ddlCatalogue = document.getElementById("ctl00_MainContent_ddlCatalogue").value;
//            
//           var ddlVessel = document.getElementById("ctl00_MainContent_ddlVessel").value;



//           var lbl1 = document.getElementById("ctl00_MainContent_lbl1").innerHTML;
//           
//           var Label2 = document.getElementById("ctl00_MainContent_Label2").innerHTML;
//           var Label9 = document.getElementById("ctl00_MainContent_Label9").innerHTML;
//           var Label6 = document.getElementById("ctl00_MainContent_Label6").innerHTML;

//           var Label7 = document.getElementById("ctl00_MainContent_Label7").innerHTML;
//           
//           var Label8 = document.getElementById("ctl00_MainContent_Label8").innerHTML;
//          
//            if (lbl1 == "*" && ddlPOType == "0") { chk++; }
//            if (Label2  == "*" && ddlReqsnType == "0") { chk++; }
//            if (Label9 == "*" && ddlFunction == "0") { chk++; }
//            if (Label6  == "*" && ddlAccountType == "0") { chk++; }
//            if (Label7  == "*" && ddlCatalogue == "0") { chk++; }
//            
//            if (chk  != 0) {alert('Please Select all the Mandatory Fields !!');return false;}
//        }
        function CheckValidation() {

//            //alert('Hi');
//            if (Page_ClientValidate("vgSubmit")) {
//                //alert('Hi');
//                document.getElementById("ctl00_MainContent_btnRequisition").style.backgroundColor = '#FFFF00';
//            }
//            else {
//               //alert('Hello');
//                document.getElementById("ctl00_MainContent_btnRequisition").style.backgroundColor = '#FFF';
//                Page_BlockSubmit = false;

//            }
        }
        //window.onload = Validate;
    </script>
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
       Create New Requisition
    </div>
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
    <asp:UpdatePanel ID="UpdatePane11" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <center>
                <table width="100%" style="color: Black; border-collapse: collapse" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlNewRequisition" runat="server">
                                <div style="border: 1px solid gray;">
                                    <div>
                                        <table class="tbl-content" cellpadding="4" cellspacing="4">
                                            <tr>
                                                <td class="tdh">
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td class="tdd">
                                                </td>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                    User-Vessel Assignement :
                                                </td>
                                                <td style="width: 15px">
                                                    
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    <asp:CheckBox ID="chkAssignement" runat="server" Checked="true" AutoPostBack="true"
                                                        OnCheckedChanged="chkAssignement_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdh">
                                                    PO Type :
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>       
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="ddlPOType" runat="server" Font-Size="12px"  Width="200px"  onchange="CheckValidation()"   OnSelectedIndexChanged="ddlPOType_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="ReqPOType" runat="server" InitialValue="0" Display="None"
                                                        ErrorMessage="PO Type is mandatory field." ControlToValidate="ddlPOType" ValidationGroup="vgSubmit"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                 <td style="text-align: right; width: 170px" class="tdh">
                                                    <asp:Label ID="lblFunction" ForeColor="Black" runat="server" Text="Department/Function :"></asp:Label>
                                                    
                                                </td>
                                                <td style="width: 15px">
                                                    <asp:Label ID="Label9" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                </td>
                                                <td style="text-align: left" class="tdd">

                                                    <asp:DropDownList ID="ddlFunction" runat="server" Width="200px" onchange="CheckValidation()" AutoPostBack="true" OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0"
                                                        Display="None" ErrorMessage="Department/Function is mandatory field." ControlToValidate="ddlFunction"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td class="tdh">
                                                    Reqsn Type :
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="ddlReqsnType" runat="server" Font-Size="12px" AutoPostBack="true" onchange="CheckValidation()" 
                                                        Width="200px" OnSelectedIndexChanged="ddlReqsnType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                                        Display="None" ErrorMessage="Reqsn Type is mandatory field." ControlToValidate="ddlReqsnType"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                 <td class="tdh">
                                                    <asp:Label ID="lblSubCatalogue" ForeColor="Black" runat="server" Text="Catalogue/System :"></asp:Label>
                                                    
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="Label7" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="ddlCatalogue" onchange="CheckValidation()" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="0"
                                                        Display="None" ErrorMessage="Catalogue/System is mandatory field." ControlToValidate="ddlCatalogue"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                            <td class="tdh">
                                                    Fleet :
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="lbl8" runat="server" ForeColor="#FF0000" Text=""></asp:Label> 
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="DDLFleet" Font-Size="12px" onchange="CheckValidation()" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" Width="200px" TabIndex="1">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="tdh">
                                                    Account Type :
                                                </td>
                                                <td style="width: 15px">
                                                    <asp:Label ID="Label6" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="ddlAccountType" runat="server"  onchange="CheckValidation()" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                                        Display="None" ErrorMessage="Account Type is mandatory field." ControlToValidate="ddlAccountType"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            
                                            </tr>
                                            <tr>
                                                
                                                <td  class="tdh">
                                                    Vessel :
                                                </td>
                                                <td style="width: 5px">
                                                   <asp:Label ID="Label8" runat="server" ForeColor="#FF0000" Text="*"></asp:Label> 
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    <asp:DropDownList ID="ddlVessel"  runat="server" onchange="CheckValidation()"
                                                        Width="200px" onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                                        Display="None" ErrorMessage="Vessel is mandatory field." ControlToValidate="ddlVessel"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                   <td style="text-align: right; width: 170px" class="tdh">
                                                    Urgency  :
                                                </td>
                                                <td style="width: 15px">
                                                  <asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text=""></asp:Label>  
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    <asp:DropDownList ID="ddlUrgency" runat="server" Width="200px" onchange="CheckValidation()">
                                                        <%--<asp:ListItem Value="N">Normal</asp:ListItem>
                                                        <asp:ListItem Value="U">Urgent</asp:ListItem>
                                                        <asp:ListItem Value="I">Immediate</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="trItemCategory" runat="server" visible="false">
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                    <asp:Label ID="Label3" ForeColor="Black" runat="server" Text="Item Category :"></asp:Label>&nbsp;
                                                </td>
                                                <td style="width: 5px">
                                                   
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                     <asp:DropDownList ID="ddlItemCategory" runat="server" Width="200px"  
                                                         onselectedindexchanged="ddlItemCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                   
                                                </td>
                                                <td style="width: 5px">
                                                 
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    
                                               
                                                </td>
                                            </tr>
                                            <tr id="trDelivery" runat="server" visible="false">
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                    <asp:Label ID="lblDeliveryDate" ForeColor="Black" runat="server" Text="Delivery Date :"></asp:Label>&nbsp;
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="lblDeliveryAst" ForeColor="#FF0000" runat="server" Text="*"></asp:Label>
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    <asp:TextBox ID="txtDeliveryDate" onchange="return CheckValidation()" runat="server" Width="200px"></asp:TextBox>
                                                    <cc1:CalendarExtender TargetControlID="txtDeliveryDate" ID="caltxtDeliveryDate" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rqDeliveryDate" runat="server"  Display="None" Enabled = "false"
                                                        ErrorMessage="Delivery Date  is mandatory field." ControlToValidate="txtDeliveryDate" ValidationGroup="vgSubmit"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                   
                                                </td>
                                                <td style="width: 5px">
                                                 
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    
                                               
                                                </td>
                                            </tr>
                                            <tr id="trDeliveryPort" runat="server" visible="false">
                                             <td style="text-align: right; width: 170px" class="tdh">
                                                    <asp:Label ID="lblDeliveryPort"  runat="server" Text="Delivery Port :"></asp:Label>&nbsp;
                                                </td>
                                                <td style="width: 5px">
                                                   <asp:Label ID="lblDeliveryPortast" ForeColor="#FF0000" runat="server" Text="*"></asp:Label> 
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                  <asp:DropDownList ID="ddlDeliveryPort" runat="server"  onchange="CheckValidation();" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqDeliveryPort" runat="server" InitialValue="0" Enabled = "false"
                                                        Display="None" ErrorMessage="Delivery Port is mandatory field." ControlToValidate="ddlDeliveryPort"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                   
                                                </td>
                                                <td style="width: 5px">
                                                 
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    
                                               
                                                </td>
                                            </tr>
                                            <tr id="trOwner" runat="server" visible="false">
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                    <asp:Label ID="lblOwnerName" ForeColor="Black" runat="server" Text="Owner Name :"></asp:Label>&nbsp;
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="lblOwnerAst" ForeColor="#FF0000" runat="server" Text="*"></asp:Label>
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    <asp:DropDownList ID="ddlOwnerName" onchange="CheckValidation();" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rqOwnerName" runat="server" InitialValue="0" Display="None" Enabled = "false"
                                                        ErrorMessage="Owner Name  is mandatory field." ControlToValidate="ddlOwnerName" ValidationGroup="vgSubmit"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                    
                                                </td>
                                                <td style="width: 5px">
                                                     
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                   
                                                </td>
                                            </tr>
                                            <tr id="trPortCall" runat="server" visible="false">
                                            <td style="text-align: right; width: 170px" class="tdh">
                                                    <asp:Label ID="lblPortCall" ForeColor="Black" runat="server" Text="Vessel Movement :"></asp:Label>&nbsp;
                                                </td>
                                                <td style="width: 5px">
                                                     <asp:Label ID="lblPortAst"  ForeColor="#FF0000" runat="server" Text="*"></asp:Label>
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    <asp:DropDownList ID="ddlPortCall" onchange="CheckValidation();" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="rqPortCall" runat="server" InitialValue="0" Display="None" Enabled = "false"
                                                        ErrorMessage="Vessel Movement  is mandatory field." ControlToValidate="ddlPortCall" ValidationGroup="vgSubmit"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                   
                                                </td>
                                                <td style="width: 5px">
                                                 
                                                </td>
                                                <td style="text-align: left" class="tdd">
                                                    
                                               
                                                </td>
                                           </tr>
                                            <tr>
                                                <td style="text-align: right; width: 170px" class="tdh">
                                                    Reqn Reason :
                                                </td>
                                                <td style="width: 5px">
                                                    <asp:Label ID="lblReqnReasonAst"  ForeColor="#FF0000" Visible="false" runat="server" Text="*"></asp:Label>
                                                </td>
                                                <td colspan="4" style="text-align: left" class="tdd">
                                                    <asp:TextBox ID="txtReqnReason" runat="server" onchange="CheckValidation();" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rqReqnReason" runat="server"  Display="None" Enabled = "false" 
                                                        ErrorMessage="Reqn Reason  is mandatory field." ControlToValidate="txtReqnReason" ValidationGroup="vgSubmit"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                        <table  class="tbl-content" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnRequisition" OnCommand="btnRequisition_Click" CommandName="1"
                                                        Text="Create New Requisition" ToolTip="New Requisition"   ValidationGroup="vgSubmit"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="vgSubmit" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px;">

                                               <%-- -- OnClientClick="CheckCheckValidation();"--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlViewRequisition" runat="server">
                                <div style="border: 1px solid gray;">
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" align="center">
                                                <b>Your new Requisition matches as existing saved requisition.Select one and click Edit <br />
                                                    or Click New to Create a New Requisition </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="text-align: right; width: 170px" class="tdh">
                                                            Requisition
                                                        </td>
                                                        <td style="width: 15px">
                                                            :
                                                        </td>
                                                        <td class="tdd">
                                                            <asp:DropDownList ID="ddlRequisitionList" Font-Size="12px" runat="server" Width="300px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlRequisitionList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlRequisitionList"
                                                                InitialValue="0" runat="server" ErrorMessage="Please select Requisition" Display="None"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator4"
                                                                runat="server">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                             <asp:Panel ID="pnlMetadata" runat="server" Visible="false">
                                                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; color: Black;
                                                    height: 100%;">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td colspan="3" style="text-align: center;" class="tdh">
                                                                Existing Requistion MetaData
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 170px" class="tdh">
                                                                Creation Date 
                                                            </td>
                                                            <td style="width: 15px">
                                                                :
                                                            </td>
                                                            <td class="tdd">
                                                                <asp:Label ID="lblCreatedDate" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 170px" class="tdh">
                                                                Creation By 
                                                            </td>
                                                            <td style="width: 15px">
                                                                :
                                                            </td>
                                                            <td class="tdd">
                                                                <asp:Label ID="lblCreatedBy" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 170px" class="tdh">
                                                                Number of Item 
                                                            </td>
                                                            <td style="width: 15px">
                                                                :
                                                            </td>
                                                            <td class="tdd">
                                                                <asp:Label ID="lblItem" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 170px" class="tdh">
                                                                Last Saved On 
                                                            </td>
                                                            <td style="width: 15px">
                                                                :
                                                            </td>
                                                            <td class="tdd">
                                                                <asp:Label ID="lblModifiedDate" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnEdit" Text="Edit Requisition" ToolTip="Edit Requisition" runat="server"
                                                    OnClick="btnEdit_Click" />
                                                <asp:Button ID="btnNew" Text="New Requisition" ToolTip="New Requisition" runat="server"
                                                    OnClick="btnNew_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
               </td></tr></table>
              
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

