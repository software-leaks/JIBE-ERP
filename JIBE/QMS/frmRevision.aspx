<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRevision.aspx.cs" Inherits="Web_frmRevision" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QMS Revision Page</title>
        <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      <br />
    <div id="divMessage" align="center">
      <asp:Label ID="dvMessage" runat="server"   ForeColor="Blue" Font-Size="Medium" ></asp:Label>
    </div>
     <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="border: solid 1px gray;">
                    <tr>
                        <td colspan="2" style="margin-left: 40px; background-color: #c2c2cc;" align="right">
                            <asp:Label ID="lblDate" runat="server" Text="Date :"></asp:Label>
                            <asp:TextBox ID="txtModeDate" runat="server" Text="" MaxLength="80" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtModeDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSendToVessel" runat="server" Text="Update" OnClick="btnSendToVessel_Click" />
                            &nbsp;&nbsp;&nbsp
                            <asp:Button ID="btnClose" runat="server" Text="Close" Width="91px" OnClientClick="javascript:window.close();" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <div style="height: 500px; width: 300px; overflow: auto; border: 1px solid #a1a1bc">
                                <asp:TreeView ID="BrowseTreeView" runat="server" ShowCheckBoxes="Leaf" Style="margin-right: 1px"
                                    BorderColor="#ffffff" BorderStyle="Solid" ExpandDepth="1" Font-Names="Arial"
                                    Font-Size="Small" ForeColor="Black" ImageSet="Msdn" NodeIndent="15" OnSelectedNodeChanged="BrowseTreeView_SelectedNodeChanged">
                                    <ParentNodeStyle Font-Bold="False" />
                                    <HoverNodeStyle Font-Underline="True" />
                                    <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px" />
                                </asp:TreeView>
                            </div>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr style="background-color: #c2c2cc;">
                                    <td>
                                        &nbsp;
                                        <asp:Button ID="btnSelectAll" runat="server" Text="Select All" OnClick="btnSelectAll_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="BtnDeselectAll" runat="server" Text="Deselect All" OnClick="BtnDeselectAll_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="height: 500px; overflow: auto;">
                                            <asp:GridView ID="GrdVesselNameList" runat="server" Width="284px" AutoGenerateColumns="False"
                                                AllowSorting="True" CellPadding="4" DataKeyNames="Name" ForeColor="#333333" GridLines="None">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="2%"
                                                        ItemStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%"></HeaderStyle>
                                                        <ItemStyle Width="2%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <%--  <asp:BoundField DataField="Vessel_Code"  HeaderText="VesselCode" />--%>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" HeaderText="Vessel Name" ReadOnly="True" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ShortName" HeaderText="ShortName" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    BackColor="White" ForeColor="#284775" />
                                                <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#F7F6F3"
                                                    ForeColor="#333333" />
                                                <EditRowStyle CssClass="RowStyle-css" BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="PagerStyle-css" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
