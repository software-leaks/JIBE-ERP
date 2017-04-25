<%@ Page Title="eForm Vessel Assignment" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="eFormVesselAssignment.aspx.cs" Inherits="eForms_eFormVesselAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="4">
                <tr>
                    <td colspan="3" align="center" style="background: #cccccc; font-size: x-large;">
                        e-Form Vessel Assignment
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 20%;">
                        <asp:ListBox ID="lsteFormList" runat="server" Height="600px" Width="100%" AutoPostBack="true"
                            CssClass="listbox" SelectionMode="Single"  
                            DataTextField="Form_Display_Name" DataValueField="ID" OnSelectedIndexChanged="lstUserList_SelectedIndexChanged">
                        </asp:ListBox>
                       
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="20" DataKeyNames="Vessel_ID" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                            CellPadding="4" ForeColor="#333333"
                            GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="VesselName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VesselCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselCode" runat="server" Text='<%#Eval("Vessel_Short_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Access View">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAccessView" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_view")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRec" runat="server" Text="Click on Initialize Access to initialize menu"></asp:Label>
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
                        <div style="border: 1px solid outset; text-align: center;">
                            <asp:Button ID="btnAssigneForm" runat="server" Text="Assign eForm to Vessel" OnClick="btnAssigneForm_Click"/>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
