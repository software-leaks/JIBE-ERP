<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLog.aspx.cs" Title="QMS" Inherits="Web_MainLog" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 
<head id="Head1" runat="server">
    <title>Main Log Page</title>
    <style type="text/css">
        a
        {
            
            font-style: normal;
            font-size: 8pt;
            color: black;
            cursor: pointer;
            text-decoration: none;
        }
        body, td, li
        {
            
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
        <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="FirstPage" runat="server">
    
    
    <%--<a href="" target="_blank" style="display: none">Test</a> <span id="lblLoading0"
        style="position: absolute; top: 0; right: 0; visibility: hidden; color: red;
        font-size: 12px; font-weight: bold;">Loading...</span>
    <table id="tabLogo0" border="0" style="border-width: thin; border-style: none; height: 100px;
        width: 101%;">
        <tr>
            <td nowrap style="vertical-align: bottom; background-color: #FFFFFF;" class="style2">
                &nbsp;<span style="vertical-align: bottom; font-size: medium; font-weight: bold;
                    font-family: Tahoma;">Ship Mgt QMS Logs</span>
            </td>
        </tr>
        <tr>
            <td nowrap style="vertical-align: bottom; background-color: #FFFFFF;" class="style2">--%>
    <table border="0" cellpadding="0" cellspacing="0" style="height: 121px; width: 518px;
        background-color: #DBE3D2; font-size: medium; border: 1px solid black">
        <tr>
            <td align="left" style="border: 0;" class="style4">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 75px; width: 869px;
                    background-color: #8BB1D8; margin-left: 0px;" align="center">
                    <tr>
                        <td align="left" style="border-style: none; border-color: inherit; border-width: 0;"
                            class="style26">
                            &nbsp;From Date:<asp:TextBox ID="ToModDate" runat="server" Text="" MaxLength="80"
                                Width="131px" Style="margin-left: 8px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="ToModDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="border: 0;" class="style27">
                            &nbsp; Manual:&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="Manual1" runat="server"
                                Height="20px" Width="211px" Style="margin-left: 0px" Font-Size="Small"
                                TabIndex="2">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="border: 0;" class="style7" rowspan="2">
                            <asp:Panel ID="PanelGroup" runat="server" Width="146px" Height="60px" Style="margin-top: 0px">
                                <asp:RadioButton ID="RdoViewAll" runat="server" Text="View All" GroupName="GrpRadio"
                                    OnCheckedChanged="RdoViewAll_CheckedChanged" TabIndex="4" />
                                <br>
                                <asp:RadioButton ID="RdoViewRead" runat="server" GroupName="GrpRadio" OnCheckedChanged="RdoViewRead_CheckedChanged"
                                    Text="View Read" TabIndex="5" />
                                <br>
                                <asp:RadioButton ID="RdoViewNotRead" runat="server" GroupName="GrpRadio" OnCheckedChanged="RdoViewNotRead_CheckedChanged"
                                    Text="View Not Read" TabIndex="7" />
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
                            &nbsp To Date:&nbsp &nbsp<asp:TextBox ID="FromModeDate" runat="server" Text="" MaxLength="80"
                                Style="margin-left: 2px" Width="130px" TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="FromModeDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="border: 0;" class="style29">
                            &nbsp; User:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="DDUser" runat="server"
                                Height="20px" Width="209px" Style="margin-left: 3px" TabIndex="3" Font-Size="Small"
                                OnSelectedIndexChanged="DDUser_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-style: none; border-color: inherit; border-width: 0;" align="right"
                            colspan="3">
                            <asp:Button ID="btnExecute" runat="server" Text=" Retrieve   " Height="22px" Width="80px"
                                Font-Bold="True" Font-Italic="False" OnClick="btnExecute_Click" />
                            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" Text="Export To Excel"
                                Height="22px" Font-Bold="True" Width="111px" TabIndex="8" OnClick="btnClear_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
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
                            <asp:GridView ID="GrdQMSlog" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="Black" BorderStyle="Solid" CaptionAlign="Bottom"
                                CellPadding="3" Width="900px" OnPageIndexChanging="GrdQMSlog_PageIndexChanging"
                                Font-Size="10px" PageSize="20" TabIndex="9" ForeColor="Black" 
                                Height="20px" HorizontalAlign="Left"
                                Style="margin-left: 0px" onrowdatabound="GrdQMSlog_RowDataBound">
                                <RowStyle BackColor="White" Font-Bold="False" Font-Names="Arial" Font-Size="6px"
                                    ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="Black"
                                    Wrap="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Parent Manual">
                                        <ItemTemplate>
                                            <asp:Label ID="LOGManuals1" runat="server" Text='<%# Eval("LOGManual1")%>' Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Child Manual">
                                        <ItemTemplate>
                                            <asp:Label ID="LOGManuals2" runat="server" Text='<%# Eval("LOGManual2")%>' Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FileName">
                                        <ItemTemplate>
                                            <asp:Label ID="logfileid" runat="server" Text='<%# Eval("LogFileID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="logdate" runat="server" Text='<%# Eval("LogDate")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Height="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpen" ImageUrl="images/ie.gif" Height="15px"
                                                Width="15px" CommandName='<%# Eval("FilePath") %>' OnCommand="ShowFile" ImageAlign="Middle"
                                                ToolTip="view in Browser" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View in external File" HeaderStyle-ForeColor="White"
                                        HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        HeaderStyle-Height="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpenExt" ImageUrl="images/windowopen.ico"
                                                Height="15px" Width="15px" CommandName='<%# Eval("FilePath") %>' ImageAlign="Middle"
                                                OnCommand="OpenFileExternal" ToolTip="Open in External Application" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#DBE0B1" ForeColor="#4A3C8C" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" 
                                    Font-Size="10px" HorizontalAlign="Left" VerticalAlign="Middle" BorderStyle="Solid" />
                                <HeaderStyle BackColor="#1F3F5F" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                    Height="25px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <AlternatingRowStyle BackColor="#EAEAEA" BorderColor="Black" Font-Names="Arial" Font-Size="XX-Small"
                                    ForeColor="Black" BorderStyle="Double" HorizontalAlign="Left" VerticalAlign="Middle"
                                    Wrap="False" />
                                <EmptyDataRowStyle BackColor="#FFE0C0" BorderColor="Black" BorderStyle="Double" Font-Bold="True"
                                    Font-Italic="True" Font-Overline="False" Font-Size="XX-Small"
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
    <%--</td>
        </tr>
    </table>
    <table>
    </table>
    <div id="Div1" style="overflow: auto;">
    </div>
    <table>
    </table>--%>
    </form>
</body>
</html>
