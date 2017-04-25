<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Rights.aspx.cs" Inherits="Infobase_User_Rights" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Template</title>
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
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidateUser() {
            if (document.getElementById("chkAll").checked == false && document.getElementById("lstUserList").value == "") {
                alert("Please select user!");
                return false;

            }
            else
                return true;
        }
    
    </script>
   
</head>
<body>

    <form id="form1" runat="server">

    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td  align="center" colspan="4">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltHeader" runat="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td width="20%" align="right" style="font-weight:bold">
                    Department :
                </td>
                <td width="20%" align="left">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="156px" 
                        CssClass="control-edit required"  AutoPostBack="true" 
                        onselectedindexchanged="ddlDepartment_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
                <td width="20%" align="right" style="font-weight:bold">
                 User Access
                </td>
                 <td width="40%" align="left">
                <asp:RadioButtonList ID="rdlistAccess" RepeatDirection="Horizontal" runat="server">
                <asp:ListItem Text="Owners" Value="Owner" ></asp:ListItem>
                <asp:ListItem Text="Department Users" Value="Department" Selected="True"></asp:ListItem>
                <asp:ListItem Text="All Users" Value="Company"></asp:ListItem>
                </asp:RadioButtonList>
                </td>
                </tr>
                <tr>
                 <td colspan="4">
                 <asp:Panel ID="pnlUsers" runat="server" >
                 <asp:UpdatePanel ID="updUser" runat="server">
                 <ContentTemplate>
                 <table width="100%" style="ho">
                 
                 <tr>
                 <td width="10%">&nbsp;</td>
                 <td width="25%" style="font-weight:bold"> <asp:Label ID="lblUserList" runat="server" Text="User list :"></asp:Label> 
                 <asp:CheckBox ID="chkAll" runat="server" Text="Select All" />
                 </td>
                 <td width="10%">&nbsp;</td>
                  <td  width="25%" style="font-weight:bold"> Selected User : </td>
                   <td width="10%">&nbsp;</td>
                 </tr>
                        <tr>
                         <td width="10%">&nbsp;</td>
                            <td style="vertical-align: top; border: 1px solid #cccccc; width:20%;" align="right" >
                            <asp:Label ID ="lblNoUser" runat="server" Text="No user to select." Visible="false" ></asp:Label>
                             <asp:ListBox ID="lstUserList" runat="server"  width="100%" Height="600px"
                                                SelectionMode="Multiple"></asp:ListBox>
 
                            </td>
                            <td style="vertical-align: middle; border: 1px solid #cccccc; width:10%;font-weight:bold" align="center">
                                <asp:Button ID="btnSelectUser" runat="server" 
                                    Text="&gt;&gt;" Width="50px" ToolTip="Add User" OnClientClick="return ValidateUser();"
                                    onclick="btnSelectUser_Click" />

                            </td>
                            <td style="vertical-align: top; border: 1px solid #cccccc; width:35%;" align="right" valign="top">

                           <asp:GridView ID="gvSelectedUser" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="20" DataKeyNames="User_Id" Width="100%" OnPageIndexChanging="gvSelectedUser_PageIndexChanging"
                            CellPadding="4" ForeColor="#333333"
                            GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="User Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("User_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle Width="30%" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Owner Access">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkOwner" runat="server" Checked='<%# Eval("IsOwner") %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                      <ItemStyle Width="30%" HorizontalAlign="Center"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False" 
                                                 CommandArgument='<%#Eval("User_Right_ID")%>'  OnCommand="ibtnDeleteUser_Click" 
                                                 ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                    </ItemTemplate>

                                      <ItemStyle Width="10%" HorizontalAlign="Center"/>
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
                                <%--<asp:Button ID="btnInitializeMenu" runat="server" OnClick="btnInitializeMenu_Click"
                                    Text="Initialize menu" />--%>
                            </EmptyDataTemplate>
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
         

                            </td>
                             <td width="10%">&nbsp;</td>
                        </tr>
                 </table>
                 
                 </ContentTemplate>
                 <Triggers>
                    <asp:PostBackTrigger ControlID="ddlDepartment" />
                  </Triggers>
                 </asp:UpdatePanel>
                 </asp:Panel>
                 </td>
                </tr>
                <tr>
                    <td align="left" colspan="4" >
                    <div style=" background-color: White; overflow:auto">
    
                        <asp:ValidationSummary ID="vdTemplate" ValidationGroup="ValidateUserAccess" DisplayMode="List" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
                        </div>
                    </td>
                </tr>
                <tr>
                <td align="center" colspan="4">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
                </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>