<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Batch_Payment_Setup_Entry.aspx.cs" Inherits="PO_LOG_Batch_Payment_Setup_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <center>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-title" class="page-title">
                    Batch Payment Setup Entry
                </div>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                 <tr>
                        <td align="right" style="width: 20%">
                            Payment Type :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" colspan="4" style="width: 35%">
                            <asp:DropDownList ID="ddlPaymentType" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                                <asp:ListItem ID="SGD">Create SGD IBG Payment Mode</asp:ListItem>
                                <asp:ListItem ID="USD">Create USD FWB Payment Mode</asp:ListItem>
                                 <asp:ListItem ID="INR">Create INR BNT Payment Mode</asp:ListItem>
                            </asp:DropDownList>
                           
                        </td>
                        <td align="right" style="width: 15%">
                            Supplier Name:
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" colspan="4" style="width: 40%">
                           <asp:DropDownList ID="ddlSupplierName" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ReqPOType" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Supplier Name is mandatory field." ControlToValidate="ddlSupplierName" ValidationGroup="vgSubmit"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            Payment Currency :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" style="width: 35%">
                           <asp:DropDownList ID="ddlPaymentCurrency" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 15%">
                          Bank Name :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" colspan="4" style="width: 40%">
                            <asp:TextBox ID="txtBankName" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBankName" runat="server" Display="None" ErrorMessage="Bank name is mandatory field."
                                ControlToValidate="txtBankName" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                             Country :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 35%">
                           <asp:DropDownList ID="ddlCountry" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 15%">
                           US Ban State:
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 40%">
                        <asp:DropDownList ID="ddlState" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 20%">
                             Swift/BIC Code :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" style="width: 35%">
                            <asp:TextBox ID="txtSwiftCode" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 15%">
                          Destination ABA Number :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" colspan="4" style="width: 40%">
                             <asp:TextBox ID="txtABANumber" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            Bank Code :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 35%">
                          <asp:TextBox ID="txtBankCode" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 15%">
                            Branch Code:
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 40%">
                           <asp:TextBox ID="txtBranchCode" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 20%">
                           Account Number :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" style="width: 35%">
                         <asp:TextBox ID="txtAccountNumber" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 15%">
                           Beneficiary :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" colspan="4" style="width: 40%">
                          <asp:TextBox ID="txtBeneficiary" CssClass="txtInput" MaxLength="200" Width="300px"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            Apy Form Account :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 35%">
                             <asp:DropDownList ID="ddlPaymentAccount" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                            </asp:DropDownList>
                          
                        </td>
                        <td align="right" style="width: 15%">
                              Pay Mode :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 40%">
                         <asp:DropDownList ID="ddlPayMode" CssClass="txtInput" runat="server" AutoPostBack="true" 
                                Width="300px" >
                            </asp:DropDownList>
                          
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 20%">
                          
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 35%">
                          
                        </td>
                        <td align="right" style="width: 15%">
                           
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 40%">
                     
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                            * Mandatory fields
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnDraft" runat="server" Text="Save" ValidationGroup="vgSubmit" Width="150px"
                                    OnClick="btnDraft_Click" />&nbsp;&nbsp;
                                       <asp:Button ID="btLock" runat="server" Text="Lock" 
                                    ValidationGroup="vgSubmit" Width="150px" onclick="btLock_Click"
                                     />&nbsp;&nbsp;
                                       <asp:Button ID="btnUnLock" runat="server" Text="Un Lock" 
                                    ValidationGroup="vgSubmit" Width="150px" onclick="btnUnLock_Click"
                                     />
                                &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtPaymentID" Visible="false" CssClass="txtInput" Width="1px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
                    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="vgSubmit" />
                </div>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
