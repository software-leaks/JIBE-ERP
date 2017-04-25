<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ASL_Change_Request_Index.aspx.cs"
    EnableEventValidation="false" Inherits="ASL_ASL_Change_Request_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change Request</title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div
        {
            margin: 5px;
        }
        
        #tblChangeRequest
        {
            font-family: Arial, Verdana, Sans-Serif;
            font-size: 12px;
        }
        
        #tblChangeRequest td
        {
            text-align: left;
            border: solid 1px #cccccc;
        }
        #tblChangeRequest1
        {
            font-family: Arial, Verdana, Sans-Serif;
            font-size: 12px;
        }
        
        #tblChangeRequest1 td
        {
            text-align: left;
            border: solid 1px #cccccc;
        }
    </style>
    <script type="text/javascript">
        function OpenScreen(ID, Eval_ID) {
            var url = 'ASL_CR_Approver.aspx?Supp_ID=' + ID + '&Eval_ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_CR_Approver', 'Supplier Change Request Approver', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 1.3, null, null, false, false, true, null);
        }
        

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.parent.$("#ASL_CR").css("height", (parseInt($("#pnlChangeRequest").height()) + 500) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnlChangeRequest").height()) + 500) + "px").css("top", "50px");
        });
    </script>
     
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
  <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptM" runat="server">
    </asp:ScriptManager>
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <center>
        <asp:Panel ID="pnlChangeRequest" runat="server" Visible="true">
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
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-title" class="page-title">
                    Change Request List
                </div>
                
                    <table width="100%">
                        <tr id="trSubmitted" runat="server">
                            <td align="right" style="color: #FF0000;">
                                <asp:Button ID="btnGroup" runat="server" Text="ASL Column Group Relationship" OnClientClick='OpenScreen(null,null);return false;' />&nbsp;&nbsp;&nbsp;&nbsp;
                                Supplier Code &nbsp;:&nbsp;<asp:Label ID="lblSupplierCode" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                Submitted By &nbsp;:&nbsp;
                                <asp:Label ID="lblSubmittedBY" runat="server" Text=""></asp:Label>&nbsp;&nbsp; Submitted
                                Date &nbsp;:&nbsp;<asp:Label ID="lblSubmitteddate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <div>
                                        <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                                            <ItemTemplate>
                                                <table border="1" width="100%" cellpadding="5" cellspacing="0">
                                                     <tr style="background-color:#abcdef;" >
                                                        <td align="center" style="width: 18%;">
                                                            <strong>Data Fields</strong>
                                                        </td>
                                                        <td align="center" style="width: 25%;">
                                                            <strong>Current Value</strong>
                                                        </td>
                                                        <td align="center" style="width: 25%;">
                                                            <strong>New Value</strong>
                                                        </td>
                                                        <td align="center" style="width: 25%;">
                                                            <strong>Reason For Change</strong>
                                                        </td>
                                                        <td align="center" style="width: 7%;">
                                                            <strong><asp:Label ID="lblAction" runat="server" Visible="false" Text="Select"></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" colspan="5">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="20%" align="center" style=" font-weight: bold">
                                                                      <asp:Label ID="lblGroupID" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    <asp:Label ID="lblGroup_Name" runat="server" Visible="false" Text='<%# Eval("Group_Name")%>'></asp:Label>
                                                                        <asp:Label ID="lblGroup_Desc" runat="server" Text='<%# Eval("Group_Desc")%>'></asp:Label>
                                                                    </td>
                                                                    <td width="40%" style="color: Black; height: 20px;" align="left">
                                                                        <asp:Label ID="Label3" runat="server" Text="1st Approver"></asp:Label>&nbsp;&nbsp;
                                                                        <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                        <asp:DropDownList ID="ddlApprover" runat="server" CssClass="txtInput" Width="300px">
                                                                        </asp:DropDownList>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlApprover"
                                                                        ErrorMessage="Approver Name is mandatory" InitialValue="0"  Display="Dynamic" ValidationGroup="vgSubmit" >
                                                                    </asp:RequiredFieldValidator>  
                                                                                     
                                                                    </td>
                                                                    <td width="40%" style="color: Black; height: 15px;" align="left">
                                                                        <asp:Label ID="Label4" runat="server" Text="Final Approver"></asp:Label>&nbsp;&nbsp;
                                                                        <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                        <asp:DropDownList ID="ddlFinalApprover" runat="server" CssClass="txtInput" Width="300px">
                                                                        </asp:DropDownList>
                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFinalApprover"
                                                                        ErrorMessage="Final Approver Name is mandatory" InitialValue="0" Display="Dynamic"  ValidationGroup="vgSubmit">
                                                                    </asp:RequiredFieldValidator>  
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       <%-- <td valign="top">--%>
                                                            <asp:Repeater runat="server" ID="rpt2"  OnItemDataBound="rpt2_ItemDataBound">
                                                            <HeaderTemplate>
                                                             
                                                            </HeaderTemplate>
                                                                <ItemTemplate>
                                                                
                                                                   <%-- <asp:Literal ID="litRowStart" runat="server"></asp:Literal>--%>
                                                                        <tr>
                                                                           
                                                                                    <td width="18%" valign="top" style="color: Black;"
                                                                                        align="center">
                                                                                         <asp:Label ID="lblFieldsName" runat="server" Visible="false" Text='<%# Eval("COLUMN_NAME")%>'></asp:Label>
                                                                                        <asp:Label ID="lblFieldDesc" runat="server" Text='<%# Eval("Column_Description")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td width="15%" valign="top" style="color: Black; height: 15px" align="center">
                                                                                        <asp:Label ID="lblCurrentValue" runat="server" Text='<%# Eval("CurrentValue")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td width="25%" valign="top" style="color: Black; height: 15px" align="center">
                                                                                        <asp:Label ID="lblNewValue" runat="server" Text='<%# Eval("New_Value")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td width="25%" valign="top" style="color: Black; height: 15px" align="center">
                                                                                        <asp:Label ID="lblReason_For_Change" runat="server" Text='<%# Eval("Reason_For_Change")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td width="7%" valign="top" style="color: Black; height: 15px" align="center">
                                                                                        <asp:CheckBox ID="chk" runat="server" Visible="false" />
                                                                                          <asp:HiddenField ID="hdnFieldName" runat="server" Value='<%# Eval("COLUMN_NAME")%>'  />
                                                                                    <asp:HiddenField ID="hdnSendforapprove" runat="server" Value='<%# Eval("Send_For_Approve")%>' />
                                                                                     <asp:HiddenField ID="hdnSendFinalApprove" runat="server" Value='<%# Eval("Send_For_FinalApprove")%>' />
                                                                                    </td>
                                                                                   
                                                                                  </tr>
                                                                                    
                                                                               <%-- </tr>
                                                                            </table>--%>
                                                                   <%--     
                                                                    <asp:Literal ID="litRowEnd" runat="server"></asp:Literal>--%>
                                                                </ItemTemplate>
                                                               <FooterTemplate></FooterTemplate>
                                                            </asp:Repeater>
                                                       <%-- </td>--%>
                                                    </tr>
                                                        <br />    <br />
                                                </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             
                                                <%-- Label used for showing Error Message --%>
                                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="NO RECORDS FOUND"
                                                    Visible="false">
                                                </asp:Label>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                   </div>
                            <div>
                                <center>
                                    <table width="100%" cellpadding="2" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: #DDDDDD">
                                                <asp:Button ID="btnRecallDraft" runat="server" Width="100px" Text="Recall Draft"
                                                     OnClick="btnRecallDraft_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnSubmitRequest" runat="server" Width="100px" Text="Submit Request"
                                                    ValidationGroup="vgSubmit" OnClick="btnSubmitRequest_Click"  />&nbsp;&nbsp;
                                                <asp:Button ID="btnRecallRequest" runat="server" Width="100px" Enabled="false" Text="Recall Request"
                                                    OnClick="btnRecallRequest_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnApprove" Visible="false" runat="server" Width="200px" 
                                                    Text="Approve Selected Changes" OnClick="btnApprove_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnRecallApprove" runat="server" Width="150px" Visible="false" Text="Recall Approved Request"
                                                    OnClick="btnRecallRequest_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnFinalApprove" Visible="false" runat="server" Width="200px" 
                                                    Text="Final Approve Selected Changes" OnClick="btnFinalApprove_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnReject" Visible="false" runat="server" Width="200px" Text="Reject Selected Changes"
                                                    OnClick="btnReject_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            <%--    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="vgSubmit" />--%>
                                <asp:HiddenField ID="hdnCRID" runat="server" />
                                 <asp:HiddenField ID="hdnCRStatus" runat="server" />
                                   <asp:HiddenField ID="hdnCRStatus1" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               
                </div>
        </asp:Panel>
    </center>
    </form>
    </td></tr></table>
</body>
</html>
