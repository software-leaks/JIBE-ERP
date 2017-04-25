<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AdvQuery.aspx.cs"
    Inherits="Web_AdvQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DOCUMENT MANAGEMENT SYSTEM</title>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
    </style>
</head>
<script type="text/javascript" language="javascript">

    function msieversion() {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE");
        if (msie > 0)
            return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
        else
            return 0;
    }
</script>
<script type="text/javascript" language="javascript">
    function testEnterKey() {
        if (event.keyCode == 13) {
            var enterValue = document.getElementById('btnExecute');

            //event.enterValue = true;

        }
    }
</script>
<script type="text/javascript" language="javascript">

    function NAF(szUrl, szQuery) {
        var sName = "idxdoc" + new Date().getMilliseconds().toString();
        window.open(szUrl.replace("#", "%23"), sName, "");
    }

</script>
<body style="margin: 0">
    <form id="QForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divMessage" align="center">
                <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
            </div>
            <div style="overflow: hidden; background-color: #cfcfff; margin-bottom: 1px; border: 1px solid outset;"
                id="Div2">
                <table width="100%" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td>
                            Folder Name:
                        </td>
                        <td style="width: 120px;">
                            <asp:DropDownList ID="FolderName" runat="server" Height="21px" Style="margin-left: 0px"
                                AutoPostBack="true" Width="200px" OnSelectedIndexChanged="FolderName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Where File Size is:
                        </td>
                        <td>
                            <asp:DropDownList ID="FSRest" runat="server" Width="84px">
                                <asp:ListItem Value="&lt;" Selected="True">Less Than</asp:ListItem>
                                <asp:ListItem Value="&gt;">Greater Than</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="FSRestVal" runat="server">
                                <asp:ListItem Value="any" Selected="True">Any size</asp:ListItem>
                                <asp:ListItem Value="100">100 bytes</asp:ListItem>
                                <asp:ListItem Value="1024">1K byte</asp:ListItem>
                                <asp:ListItem Value="10240">10K bytes</asp:ListItem>
                                <asp:ListItem Value="102400">100K bytes</asp:ListItem>
                                <asp:ListItem Value="1048576">1M byte</asp:ListItem>
                                <asp:ListItem Value="10485760">10M bytes</asp:ListItem>
                                <asp:ListItem Value="104857600">100M bytes</asp:ListItem>
                                <asp:ListItem Value="other">Other</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="FSRestOther" runat="server" Text="" Width="50"></asp:TextBox>
                            bytes
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SubFolder Name:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubFolderName" runat="server" Height="21px" Style="margin-left: 0px"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Document Title:
                        </td>
                        <td>
                            <asp:TextBox ID="DocAuthorRestriction" runat="server" Text="" MaxLength="100" Width="241px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Text to Search:
                        </td>
                        <td>
                            <asp:TextBox ID="SearchString" runat="server" Width="228px"></asp:TextBox>
                        </td>
                        <td>
                            File Name:
                        </td>
                        <td>
                            <asp:TextBox ID="FileNameRestriction" runat="server" Text="" MaxLength="100" Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Modified Between:
                        </td>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 40%;">
                                        From Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="FMModDate" runat="server" Text="" MaxLength="40" Width="70px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="FMModDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        To Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ToModeDate" runat="server" Text="" MaxLength="40" Width="70px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="ToModeDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" valign="top">
                            <asp:Button ID="btnExecute" runat="server" Text="Retrieve" OnClick="btnRetrieve_Click" />
                            <asp:Button ID="Button1" runat="server" Text="Clear" OnClick="btnClear_Click1" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: center;">
                <table width="99%" cellpadding="2" cellspacing="0" style="text-align: left;">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="lblDisplay" runat="server" ForeColor="Blue">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="SearchString"
                                    ErrorMessage="Please write proper words to search" ValidationExpression='^([a-zA-z0-9-\s]{2,30})$'
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </asp:Label>
                            <asp:GridView ID="gvSearchResult" runat="server" BackColor="White" BorderColor="White"
                                AutoGenerateColumns="False" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3"
                                CellSpacing="1" GridLines="None" OnRowDataBound="gvSearchResult_RowDataBound"
                                DataKeyNames="Filename" AllowSorting="True" PageSize="18" OnPageIndexChanging="gvSearchResult_PageIndexChanging"
                                Width="100%" OnSorting="gvSearchResult_Sorting">
                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        SortExpression="Filename">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Filename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size(KB)" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        SortExpression="File Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("File Size") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        SortExpression="write">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrite" runat="server" Text='<%# Eval("Read Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version" />
                                    <asp:TemplateField HeaderText="View" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Height="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpenForView" ImageUrl="images/ie.gif" Height="15px"
                                                Width="15px" CommandName='<%# Eval("FilePath") %>' OnCommand="ShowFile" ImageAlign="Middle"
                                                ToolTip="view in Browser" />
                                            <%-- <asp:Label ID="lblpostdt" runat="server" Text='<%# Eval("write")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpostdtNotVissible" runat="server" Text='<%# Eval("write")%>'></asp:Label>
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
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
