<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncViewLog.aspx.cs" Inherits="SyncViewLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Sync Log</title>
    <style type="text/css">
        a
        {
            font-family: Tahoma, Arial, Helvetica;
            font-style: normal;
            font-size: 8pt;
            color: black;
            cursor: pointer;
            text-decoration: none;
        }
        body, td, li
        {
            font-family: Tahoma, Arial, Helvetica;
            font-style: normal;
            font-weight: normal;
            font-size: 10pt;
            color: black;
        }
        .style4
        {
            height: 25px;
        }
        .style7
        {
            width: 394px;
        }
        .style26
        {
            width: 420px;
            height: 15px;
        }
        .style27
        {
            width: 610px;
            height: 15px;
        }
        .style28
        {
            width: 420px;
            height: 22px;
        }
        .style29
        {
            width: 610px;
            height: 22px;
        }
    </style>

    <script type="text/javascript">
  function CheckUser() 
    
    {
   alert('Please Select user from the user list')
    
    }
    </script>

</head>
<body>
    <form id="FirstPage" runat="server">
    
    
    <table border="0" cellpadding="0" cellspacing="0" style="height: 121px; width: 518px;
        background-color: #8BB1D8;  font-size: medium; border: 1px solid black">
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td align="left" style="border: 0;" class="style4">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 75px; width: 869px;
                    background-color: #8BB1D8; margin-left: 0px;" align="center">
                    <tr>
                        <td align="left" style="border-style: none; border-color: inherit; border-width: 0;"
                            class="style26">
                            &nbsp;<asp:Label ID="lblVessel" runat="server" Text="Vessel:"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlVesselCode" runat="server" Style="margin-left: 0px" Width="134px"
                                Font-Names="Tahoma" Font-Size="Small" AutoPostBack="True" 
                                onselectedindexchanged="ddlVesselCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border-style: none; border-color: inherit; border-width: 0;"
                            class="style26">
                            &nbsp;From Date:<asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="80"
                                Width="131px" Style="margin-left: 8px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="border: 0;" class="style27">
                            &nbsp;&nbsp; Manual:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlManual1" runat="server"
                                Height="20px" Width="211px" Style="margin-left: 0px" Font-Names="Tahoma" Font-Size="Small"
                                TabIndex="2">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="border: 0;" class="style7" rowspan="2">
                            <asp:Panel ID="PanelGroup" runat="server" Width="146px" Height="60px" Style="margin-top: 0px">
                                <br />
                                <br />
                                <br />
                                <br />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-style: none; border-color: inherit; border-width: 0;" align="left"
                            class="style28">
                            &nbsp To Date:&nbsp &nbsp<asp:TextBox ID="txtToDate" runat="server" Text="" MaxLength="80"
                                Style="margin-left: 2px" Width="130px" TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="border: 0;" class="style29">
                            &nbsp; Crew:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="DDlUser" runat="server"
                                Height="20px" Width="209px" Style="margin-left: 3px" TabIndex="3" Font-Size="Small">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="border-style: none; border-color: inherit; border-width: 0;" align="right">
                            <asp:Button ID="btnExecute" runat="server" Text=" Retrieve   " Height="22px" Width="80px"
                                Font-Bold="True" Font-Italic="False" OnClick="btnExecute_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: White">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDocumentCount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-style: none; border-color: inherit; border-width: 0;" align="right"
                            colspan="3">
                            <asp:GridView ID="GrdSyncQMSlog" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="Black" BorderStyle="Solid" CaptionAlign="Bottom"
                                CellPadding="3" Width="900px" Font-Size="10px" PageSize="18" AllowSorting="True"
                                DataKeyNames="LogFileID" ForeColor="Black" Height="20px" HorizontalAlign="Left"
                                Style="margin-left: 0px" OnSorting="GrdSyncQMSlog_Sorting" OnPageIndexChanging="GrdSyncQMSlog_PageIndexChanging">
                                <RowStyle BackColor="White" Font-Bold="False" Font-Names="Arial" Font-Size="6px"
                                    ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="Black"
                                    Wrap="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Parent Manual" SortExpression="LOGManuals1">
                                        <ItemTemplate>
                                            <asp:Label ID="LOGManuals1" runat="server" Text='<%# Eval("LOGManuals1")%>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Child Manual" SortExpression="LOGManuals2">
                                        <ItemTemplate>
                                            <asp:Label ID="LOGManuals2" runat="server" Text='<%# Eval("LOGManuals2")%>' Width="260px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FileName" SortExpression="LogFileID">
                                        <ItemTemplate>
                                            <asp:Label ID="logfileid" runat="server" Text='<%# Eval("LogFileID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Date" SortExpression="LogDate">
                                        <ItemTemplate>
                                            <asp:Label ID="logdate" runat="server" Text='<%# Eval("LogDateText")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="logfileid" runat="server" Text='<%# Eval("UserName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="logdate" runat="server" Text='<%# Eval("LogDate")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#DBE0B1" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" Font-Names="Tahoma"
                                    Font-Size="10px" HorizontalAlign="Left" VerticalAlign="Middle" BorderStyle="Solid" />
                                <HeaderStyle BackColor="#1F3F5F" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                    Height="25px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <AlternatingRowStyle BackColor="#EAEAEA" BorderColor="Black" Font-Names="Arial" Font-Size="XX-Small"
                                    ForeColor="Black" BorderStyle="Double" HorizontalAlign="Left" VerticalAlign="Middle"
                                    Wrap="False" />
                                <EmptyDataRowStyle BackColor="#FFE0C0" BorderColor="Black" BorderStyle="Double" Font-Bold="True"
                                    Font-Italic="True" Font-Names="Tahoma" Font-Overline="False" Font-Size="XX-Small"
                                    ForeColor="Black" Height="15px" HorizontalAlign="Center" VerticalAlign="Middle"
                                    Width="400px" />
                                <PagerSettings Mode="NumericFirstLast" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
