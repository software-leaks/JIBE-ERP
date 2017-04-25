<%@ Page Title="Purple Finder Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PurpleReport.aspx.cs" Inherits="Operations_PurpleReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function OpenCrewList(vcode) {
            alert(vcode);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text=" This is PurpleFinder Report"></asp:Label>
    </div>
    <div id="page-content" style="height: 640px; color: #333333; border: 1px solid gray;
        overflow: auto">
        <center>
            <div id="dvPurpleReport">
                <style type="text/css">
                    table tr
                    {
                        padding: 0px 0px 0px 0px;
                        white-space: normal;
                        line-height: normal;
                        letter-spacing: normal;
                    }
                    #pageTitle
                    {
                        background-color: gray;
                        color: White;
                        font-size: 12px;
                        text-align: center;
                        padding: 2px;
                        font-weight: bold;
                    }
                    .leafTR
                    {
                        border-bottom-style: solid;
                        border-bottom-color: White;
                        border-bottom-width: 1px;
                    }
                    .leafTD-header
                    {
                        width: 120px;
                        height: 20px;
                        padding: 0px 0px 0px 0px;
                        text-align: left;
                    }
                    .leafTD-data
                    {
                        width: 140px;
                        height: 20px;
                        padding: 0px 0px 0px 0px;
                        background-color: #cce499;
                        text-align: left;
                    }
                    .leafTD-data-left
                    {
                        width: 140px;
                        height: 20px;
                        padding: 0px 0px 0px 2px;
                        background-color: #cce499;
                        text-align: center;
                    }
                    .leafTD-header-midsec
                    {
                        width: 170px;
                        height: 20px;
                        padding: 0px 0px 0px 0px;
                        text-align: left;
                    }
                    .leafTD-data-midsec
                    {
                        width: 115px;
                        height: 20px;
                        padding: 0px 0px 0px 0px;
                        background-color: #cce499;
                        text-align: right;
                    }
                    .leafTD-data-consmp
                    {
                        width: 120px;
                        height: 20px;
                        padding: 0px 0px 0px 0px;
                        background-color: #cce499;
                        text-align: right;
                        border-right: solid 1px white;
                        white-space: normal;
                        line-height: normal;
                        letter-spacing: normal;
                    }
                </style>
                <asp:FormView ID="fvPurpleReport" BorderStyle="Solid" BorderWidth="1px" runat="server">
                    <ItemTemplate>
                       
                        <table>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                    Date:
                                </td>
                                <td class='leafTD-data-left'>
                                    <%#Eval("TelegramDate")%>
                                </td>
                                <td style="width: 20px; height: 20px">
                                </td>
                            </tr>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                    Type:
                                </td>
                                <td class='leafTD-data-left'>
                                    <%#Eval("Telegram_Type")%>
                                </td>
                                <td style="width: 20px; height: 20px">
                                </td>
                            </tr>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                    Speed:
                                </td>
                                <td class='leafTD-data-left'>
                                    <%#Eval("Current_Speed")%>
                                </td>
                                <td style="width: 20px; height: 20px">
                                </td>
                            </tr>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                    Heading:
                                </td>
                                <td class='leafTD-data-left'>
                                    <%#Eval("Vessel_Course")%>
                                </td>
                                <td style="width: 20px; height: 20px">
                                </td>
                            </tr>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                </td>
                                <td class='leafTD-data'>
                                    <table cellspacing="0">
                                        <tr>
                                            <td style="width: 35px; text-align: center">
                                                Deg
                                            </td>
                                            <td style="width: 35px; text-align: center">
                                                Min
                                            </td>
                                            <td style="width: 35px; text-align: center">
                                                Sec
                                            </td>
                                            <td style="width: 10px; text-align: center">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                    Latitude:
                                </td>
                                <td class='leafTD-data-left'>
                                    <table cellspacing="0">
                                        <tr>
                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                <%#Eval("ltdeg")%>
                                            </td>
                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                <%#Eval("ltmint")%>
                                            </td>
                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                <%#Eval("ltsec")%>
                                            </td>
                                            <td style="width: 10px; text-align: center">
                                                <%#Eval("ltns") %>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px; height: 20px">
                                </td>
                            </tr>
                            <tr class='leafTR'>
                                <td class='leafTD-header'>
                                    Longitude:
                                </td>
                                <td class='leafTD-data-left'>
                                    <table cellspacing="0">
                                        <tr>
                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                <%#Eval("lgdeg")%>
                                            </td>
                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                <%#Eval("lgmint")%>
                                            </td>
                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                <%#Eval("lgsec")%>
                                            </td>
                                            <td style="width: 10px; text-align: center">
                                                <%#Eval("lgew") %>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px; height: 20px">
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
            </div>
        </center>
    </div>
</asp:Content>
