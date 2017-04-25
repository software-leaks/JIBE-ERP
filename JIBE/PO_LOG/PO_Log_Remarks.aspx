<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Remarks.aspx.cs" Inherits="PO_LOG_PO_Log_Remarks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Remarks Entry
            </div>
             <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                                        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 25%">
                        Remarks :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddRemarks" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="Remarks is mandatory field."
                                    ControlToValidate="txtAddRemarks" ValidationGroup="vgSubmitRemarks" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                        <asp:Button ID="btnAddRemarks" runat="server" Text="Save"  ValidationGroup="vgSubmitRemarks"
                            onclick="btnAddRemarks_Click" />
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                        <div style="height: 200px; background-color: White; overflow-y: scroll; max-height: 200px">
                            <asp:GridView ID="gvRemarks" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
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
                                                        <asp:ImageButton ID="lbtnEdit" runat="server" OnCommand="lbtnEdit_Click" CommandArgument='<%#Eval("[ID]")%>'
                                                            ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="lbtnDelete" runat="server" OnCommand="lbtnDelete_Click"
                                                            OnClientClick="return confirm('Are you sure want to delete?')" CommandArgument='<%#Eval("[ID]")%>'
                                                            ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px">
                                                        </asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:uccustompager id="ucCustomPager1" runat="server" onbinddataitem="BindRemarksGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                        * Mandatory fields
                          <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmitRemarks" />
                    </td>
                </tr>
            </table>
       
        <div style="display:none;" >
         <asp:TextBox ID="txtRemarksID" runat="server"  Width="1px"></asp:TextBox>
          <asp:TextBox ID="txtPOCode" runat="server"  Width="1px"></asp:TextBox>
        </div>
        </ContentTemplate></asp:UpdatePanel>
         </div>
    </center>
  
    </form>
</body>
</html>
