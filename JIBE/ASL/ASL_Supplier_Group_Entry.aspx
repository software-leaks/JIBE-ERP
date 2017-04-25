<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Supplier_Group_Entry.aspx.cs" Inherits="ASL_ASL_Supplier_Group_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Supplier Group</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
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
        .ajax__htmleditor_editor_bottomtoolbar
        {
            display: none;
        }
        .cke_show_borders body
        {
            background: #FFFFCC;
            color: black;
        }
    </style>
     <script language="javascript" type="text/javascript">

         function validation() {
             if (document.getElementById("txtRemarks").value == "") {
                 alert("Remarks is mandatory field.");
                 document.getElementById("txtRemarks").focus();
                 return false;
             }

             return true;
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlRemarks" runat="server" Visible="true">
       <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                <contenttemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="left" colspan="6" style="background: #cccccc; width: 100%">
                        <div id="Div1" class="page-title">
                            Supplier Group Entry
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                     
                        <table>
                            <tr>
                             <td>
                                   <asp:Label ID="Label1" runat="server" Text="Group Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                   <asp:TextBox ID="txtGroup" runat="server" MaxLength="2000" 
                                        Width="400px" Height="20px" ></asp:TextBox>  
                                    <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Group Name is mandatory field."
                                        ControlToValidate="txtGroup" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnRemarks" Text="Add Group" ValidationGroup="vgSubmit"  runat="server"
                                        OnClick="btnRemarks_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnExit" Text="Close" ForeColor="Red" 
                                         runat="server" onclick="btnExit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="vgSubmit" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                  
                </tr>
                <tr>
               
                    <td colspan="6">
                    
                        <div style="height: 500px; background-color: White; overflow-y: scroll; max-height: 500px">
                            <asp:GridView ID="gvGroup" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("GROUP_NAME")%>'></asp:Label>
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
                                                        <asp:ImageButton ID="lbtnEdit" runat="server" CommandArgument='<%#Eval("[ID]")  %>'
                                                            Visible='<%# uaEditFlag %>' ImageUrl="~/images/edit.gif" OnCommand="lbtnEdit_Click"
                                                            Text="Edit"></asp:ImageButton>
                                                    </td>
                                                   
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindRemarksGrid" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                     
                    </td>
                    
                </tr>
            </table>
            </contenttemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
