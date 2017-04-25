<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POLOG_Limit_Entry.aspx.cs"
    Inherits="PO_LOG_POLOG_Limit_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Approval Limit Entry
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right">
                                Group&nbsp;:&nbsp;
                            </td>
                            <td>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLGroup" runat="server" Width="300px" AutoPostBack="true" 
                                    CssClass="txtInput" onselectedindexchanged="DDLGroup_SelectedIndexChanged">
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPOType" runat="server" Font-Bold="true" Text=""></asp:Label>
                                 <asp:RequiredFieldValidator ID="ReqCurrency" runat="server" InitialValue="0" Display="None"
                                    ErrorMessage="Group is mandatory field." ControlToValidate="DDLGroup" ValidationGroup="vgSubmit"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Minimum Amount&nbsp;:&nbsp;
                            </td>
                            <td>
                           <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                               <asp:TextBox ID="txtMinApprovalLimit" CssClass="txtInput" Width="300px" runat="server"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegInvoiceValue" runat="server" ErrorMessage="Approval limit is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtMinApprovalLimit"
                                    ForeColor="Red" ValidationExpression="^[0-9.]+$">
                                </asp:RegularExpressionValidator>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  Display="None"
                                    ErrorMessage="Min Approval Limit is mandatory field." ControlToValidate="txtMinApprovalLimit" ValidationGroup="vgSubmit"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                              </td>
                              </tr>
                              <tr>
                              <td align="right">Maximum Amount&nbsp;:&nbsp;</td>
                              <td>
                           <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                              <td align="left">
                                <asp:TextBox ID="txtMaxApprovalLimit" CssClass="txtInput" Width="300px" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Approval limit is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtMaxApprovalLimit"
                                    ForeColor="Red" ValidationExpression="^[0-9.]+$">
                                </asp:RegularExpressionValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="None"
                                    ErrorMessage="Max Approval Limit is mandatory field." ControlToValidate="txtMaxApprovalLimit" ValidationGroup="vgSubmit"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Advance Approver&nbsp;:&nbsp;
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAdvanceApprover" runat="server" Width="300px" CssClass="txtInput">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 15%">
                                Approver List :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left" style="width: 50%">
                                <div style="float: left; text-align: left; width: 500px; height: 300px; overflow-x: hidden;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                     <asp:GridView ID="gvUser" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False" 
                                 DataKeyNames="UserID" Width="100%" CellPadding="4" ForeColor="#333333" CssClass="gridmain-css"
                                GridLines="None" >
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            User Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("User_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            PO Approver
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPO_Approver" runat="server" ToolTip="PO Approver" Checked='<%# Convert.ToBoolean(Eval("PO_Approver")) %>'
                                                ForeColor="white" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Invoice Approver
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice_Approver" runat="server"  ToolTip="Invoice Approver" Checked='<%# Convert.ToBoolean(Eval("Invoice_Approver")) %>'
                                                ForeColor="white" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Final Invoice Approver
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFinalInvoice_Approver" runat="server"  ToolTip="Advance Invoice Approver" Checked='<%# Convert.ToBoolean(Eval("Final_Invoice_Approver")) %>'
                                                ForeColor="white" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                  <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pager" HorizontalAlign="Left" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                                </div>
                            </td>
                           
                        </tr>
                          <tr>
                        <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                            * Mandatory fields
                        </td>
                    </tr>
                    
                    </table>
                    <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnDraft" runat="server" Text="Save" ValidationGroup="vgSubmit" 
                                    Width="150px" onclick="btnDraft_Click"  />
                                &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtLimit" Visible="false" CssClass="txtInput" Width="1px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                
                        <div style=" text-align: center; height: 150px; overflow-x: hidden;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                     <asp:GridView ID="gvLimit" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                     DataKeyNames="Limit_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both"  AllowSorting="true" 
                                         onrowdatabound="gvLimit_RowDataBound">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFF00" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <HeaderTemplate>
                                               Group Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("Group_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Min Approval Limit">
                                            <HeaderTemplate>
                                               Min Approval Amount
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblMin_Amt" runat="server" Text='<%#Eval("Min_Approval_Limit")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Max Approval Amount">
                                             <ItemTemplate>
                                                <asp:Label ID="lblMAX_APPROVAL_LIMIT" runat="server" Text='<%#Eval("Max_Approval_Limit")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance Approver">
                                            <HeaderTemplate>
                                               Advance Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblAdvanceApprover" runat="server" Text='<%#Eval("Advance_Approver")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="btnEdit_Click" CommandArgument='<%#Eval("[Limit_ID]")%>'
                                                                 ForeColor="Black" 
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                  <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="5" OnBindDataItem="BindGroup" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                </div>
                                
                            
                <div>
                    <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
                     <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                </div>
                </contenttemplate>
            </asp:UpdatePanel>
        </div>
    </center>
    </form>
</body>
</html>
