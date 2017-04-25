<%@ Page Title="User Menu Assignment" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="UserMenuAssignTree.aspx.cs" Inherits="Infrastructure_Menu_UserMenuAssignTree" %>

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
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
         </style>
    <style type="text/css">
        .listbox
        {
            border: 0px;
        }
        .SelectedNodeStyle
        {
            background: url(../../Images/bg.png) left -1672px repeat-x;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../../Images/bg.png) left -1672px repeat-x;
            font-size: 14px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
    </style>

    <script type = "text/javascript">
        function CheckAll(oCheckbox) {
            var row = oCheckbox.parentElement.parentElement.parentElement;
            var i = row.rowIndex;
            var GridView1 = document.getElementById("<%=GridView1.ClientID %>");
          if(  GridView1.rows[i].cells[3].getElementsByTagName("input")[0].disabled==false)
              GridView1.rows[i].cells[3].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[4].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[4].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[5].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[5].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[6].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[6].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[7].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[7].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[8].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[8].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[9].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[9].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[10].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[10].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[11].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[11].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[12].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[12].getElementsByTagName("input")[0].checked = oCheckbox.checked;
          if (GridView1.rows[i].cells[13].getElementsByTagName("input")[0].disabled == false)
              GridView1.rows[i].cells[13].getElementsByTagName("input")[0].checked = oCheckbox.checked;
           
        }
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="4">
                <tr>
                    <td>
                        Company Name:
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="lstCompany" runat="server" AutoPostBack="true" Width="205px"
                            DataSourceID="ObjectDataSource_CompanyList" OnDataBound="lstCompany_DataBound"
                            DataTextField="company_name" DataValueField="id" OnSelectedIndexChanged="lstCompany_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource_CompanyList" runat="server" SelectMethod="Get_CompanyListMini"
                            TypeName="SMS.Business.Infrastructure.BLL_Infra_Company">
                            <SelectParameters>
                                <asp:SessionParameter Name="UserCompany" SessionField="UserCompanyID" ConvertEmptyStringToNull="true"
                                    DefaultValue="0" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        Filter User:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchUser" runat="server" Width="200px" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        <div style="border: 1px solid #0489B1; background-color: #A9D0F5; padding: 2px;">
                            <table>
                                <tr>
                                <td>
                    
                                                            <asp:RadioButtonList ID="rdbCopy" runat="server" AutoPostBack="true" 
                                                                onselectedindexchanged="rdbCopy_SelectedIndexChanged" >
                                                            <asp:ListItem Value="1" Selected="True">Copy From User</asp:ListItem>
                                                             <asp:ListItem Value="2">Copy From Role</asp:ListItem>
                                                            </asp:RadioButtonList>
                                </td>
                                    <td>
                                        
                                        <asp:DropDownList ID="ddlCopyFromUser" runat="server" Width="150px" AutoPostBack="false"
                                            DataTextField="USERNAME" DataValueField="USERID">
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td>
                                      
                                        <asp:DropDownList ID="ddlCopyFromRole" Visible="false" runat="server" Width="150px" AutoPostBack="false"
                                            DataTextField="USERNAME" DataValueField="USERID">
                                        </asp:DropDownList>
                                        To User:
                                    </td>
                                      
                                    <td>
                                        <ucDDL:ucCustomDropDownList ID="DDLUser" runat="server" UseInHeader="false" Height="150"
                                            Width="100" Style="float: left;" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAppendMode" runat="server" Width="120px" AutoPostBack="false"
                                            Style="float: left;">
                                            <asp:ListItem Value="0" Text="Remove Existing"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Retain Existing"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlCopyMenu" runat="server" Width="180px" AutoPostBack="false">
                                            <asp:ListItem Value="0" Text="All Menu"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Selected Module/Sub-Module" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnCopy" runat="server" Text="Copy Access" OnClick="btnCopy_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                       
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 15%;">
                        <asp:ListBox ID="lstUserList" runat="server" Height="600px" Width="100%" AutoPostBack="true"
                            CssClass="listbox" SelectionMode="Single" DataSourceID="ObjectDataSource_Users"
                            DataTextField="USERNAME" DataValueField="USERID" OnSelectedIndexChanged="lstUserList_SelectedIndexChanged">
                        </asp:ListBox>
                        <asp:ObjectDataSource ID="ObjectDataSource_Users" runat="server" SelectMethod="Get_UserList"
                            TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lstCompany" DefaultValue="0" Name="CompanyID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="txtSearchUser" DefaultValue="" Name="FilterText"
                                    PropertyName="Text" Type="String" />
                                <asp:SessionParameter Name="UserID" SessionField="USERID" ConvertEmptyStringToNull="true"
                                    DefaultValue="0" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 15%;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                Width="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BorderColor="#cccccc">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                    VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="Menu_Code" Width="100%" 
                            CellPadding="4" ForeColor="#333333"
                            GridLines="None" onrowdatabound="GridView1_RowDataBound" 
                            AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                            PageSize="20">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Menu Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("Menu_Short_Discription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Menu Link">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLink" runat="server" Text='<%#Eval("Menu_Link") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                   Select All
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:CheckBox ID="chkAll"  onclick = "CheckAll(this);" runat="server" ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu_Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Menu_Id" runat="server"  Visible="false" Text='<%#Eval("Menu_Code")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                      
                                <asp:TemplateField HeaderText="Menu" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_Menu" runat="server" Visible="false" Checked='<%# Convert.ToBoolean(Eval("Access_Menu")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_View" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_View")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_Add" runat="server"  Checked='<%# Convert.ToBoolean(Eval("Access_Add")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_Edit" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Edit")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_Delete" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Delete")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_Approve" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Approve")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Admin">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Access_Admin" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Admin")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unverify">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Unverify" runat="server" Checked='<%# Convert.ToBoolean(Eval("Unverify")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Revoke">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Revoke" runat="server" Checked='<%# Convert.ToBoolean(Eval("Revoke")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Urgent">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Urgent" runat="server" Checked='<%# Convert.ToBoolean(Eval("Urgent")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Close">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Close" runat="server" Checked='<%# Convert.ToBoolean(Eval("Close")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unclose">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Unclose" runat="server" Checked='<%# Convert.ToBoolean(Eval("Unclose")) %>'
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRec" runat="server" Text=""></asp:Label>
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
                        <div style="border: 1px solid outset; text-align: right;">
                        
                            <%--<asp:Button ID="btnInitialize" runat="server" Text="Initialize Access" OnClick="btnInitializeMenu_Click" />--%>
                            <asp:Button ID="btnResetMenu" runat="server" Text="Remove Access" 
                                OnClick="btnResetMenu_Click" Visible="False" />
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" 
                                OnClick="btnSave_Click" Visible="False" />


                              
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
