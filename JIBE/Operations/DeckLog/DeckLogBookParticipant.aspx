<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeckLogBookParticipant.aspx.cs"
    Inherits="DeckLogBookParticipant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Deck_Engine_LogBook.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">



    </script>
    <style type="text/css">
        .CellClass1
        {
            background-color: #FA5858;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .CellClassChangeFlage1
        {
            background-color: Yellow;
            color: Black;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .CellClassChangeFlage0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        
        .FieldDottedLine
        {
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            border-bottom: 1px dashed;
        }
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 0px solid #FFB733;
        }
        
        .CreateHtmlTableFromDataTable-PageHeader
        {
            background-color: #F6B680;
        }
        
        .CreateHtmlTableFromDataTable-DataHedaer
        {
            background-color: #CCCCCC;
            border: 1px solid gray;
            text-align: center;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 1px solid gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid #cccccc; padding: 1px; font-family: Tahoma; font-size: 12px;
        width: 100%;">
        <table cellspacing="0" cellpadding="0" rules="all" width="100%">
            <tr>
                <td class="HeaderCellColor" align="center" colspan="5">
                    <div style="background-color: #5588BB; color: #FFFFCC; text-align: center; height: 20px;">
                        <b>INCIDENT PARTICIPANT'S LIST</b>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="CellClass0" style="height: 8px;">
                </td>
            </tr>
            <tr>
                <td align="right" class="CellClass0">
                    Incident Date :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:Label ID="lblIncidentDate" Width="215" CssClass="txtReadOnly" runat="server"></asp:Label>
                </td>
                <td align="right" class="CellClass0">
                    Incident Type :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:Label ID="lblIncidentType" Width="215" CssClass="txtReadOnly" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" class="CellClass0">
                    Details :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:TextBox ID="txtIncidentDetails" runat="server" TextMode="MultiLine" CssClass="txtReadOnly"
                        ReadOnly="true" Height="60" Width="214"></asp:TextBox>
                </td>
                <td align="right" class="CellClass0">
                    Action taken :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:TextBox ID="txtActionTaken" runat="server" TextMode="MultiLine" ReadOnly="true"
                        CssClass="txtReadOnly" Height="60" Width="210"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="CellClass0" style="height: 12px;">
                </td>
            </tr>
            <tr>
                <td class="CellClass0" colspan="4">
                    <div style="overflow-x: hidden; overflow-y: scroll; height: 200px">
                        <asp:Repeater ID="rpIncidentParticipant" runat="server">
                            <HeaderTemplate>
                                <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid #cccccc"
                                    width="100%">
                                    <tr>
                                        <td align="center" class="HeaderCellColor">
                                            Staff Code
                                        </td>
                                        <td align="center" class="HeaderCellColor">
                                            Staff Name
                                        </td>
                                        <td align="center" class="HeaderCellColor">
                                            Participant Type
                                        </td>
                                        <td align="center" class="HeaderCellColor">
                                            Action Recommended
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center">
                                    <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                        <%# DataBinder.Eval(Container.DataItem, "Staff_Code")%>
                                    </td>
                                    <td align="left" style="height: 19px; width: 35%;" class="CellClass0">
                                        <%# DataBinder.Eval(Container.DataItem, "Staff_Name")%>
                                    </td>
                                    <td align="left" style="height: 19px; width: 25%;" class="CellClass0">
                                        <%# DataBinder.Eval(Container.DataItem, "PARTICIPANTS_TYPE")%>
                                    </td>
                                    <td align="left" style="height: 19px; width: 25%;" class="CellClass0">
                                        <asp:Label ID="lblActionRecommended" Text='<%#Convert.ToString(Eval("ACTION_RECOMMENDED")).Length>10?Convert.ToString(Eval("ACTION_RECOMMENDED")).Substring(0,10)+"..." : Convert.ToString(Eval("ACTION_RECOMMENDED")) %>'
                                            onmousemove='<%# "js_ShowToolTip(&#39;"+ Convert.ToString(Eval("ACTION_RECOMMENDED")) +"&#39;,event,this)" %>'
                                            runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #efefcf; border: 1px solid Gray;" align="center">
                                    <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                        <%# DataBinder.Eval(Container.DataItem, "Staff_Code")%>
                                    </td>
                                    <td align="left" style="height: 19px; width: 35%;" class="CellClass0">
                                        <%# DataBinder.Eval(Container.DataItem, "Staff_Name")%>
                                    </td>
                                    <td align="left" style="height: 19px; width: 25%;" class="CellClass0">
                                        <%# DataBinder.Eval(Container.DataItem, "PARTICIPANTS_TYPE")%>
                                    </td>
                                    <td align="left" style="height: 19px; width: 25%;" class="CellClass0">
                                        <asp:Label ID="lblActionRecommended" Text='<%#Convert.ToString(Eval("ACTION_RECOMMENDED")).Length>10?Convert.ToString(Eval("ACTION_RECOMMENDED")).Substring(0,10)+"..." : Convert.ToString(Eval("ACTION_RECOMMENDED")) %>'
                                            onmousemove='<%# "js_ShowToolTip(&#39;"+ Convert.ToString(Eval("ACTION_RECOMMENDED")) +"&#39;,event,this)" %>'
                                            runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
