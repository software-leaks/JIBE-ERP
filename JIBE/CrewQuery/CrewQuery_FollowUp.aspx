<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewQuery_FollowUp.aspx.cs"
    Inherits="CrewQuery_CrewQuery_FollowUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background: #ffffff;
            font-size: 12px;
            font-family: Tahoma ,Tahoma, Sans-Serif;
            margin: 0px;
            padding: 0px;
            color: #444;
        }
        .dvContainer
        {
            background: rgba(200, 54, 54, 0.5);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvContainer" style="padding: 4px; border: 1px solid #dfdfdf; background-color: #efefef;border-radius:5px;">
        <div class="crewquery-section-header">
            <table style="width: 100%;">
                <tr>
                    <td>
                        Followups
                    </td>
                    <td style="text-align: right; font-weight: normal;">
                        Click to add followup&nbsp;&nbsp;
                        <img src="../Images/arrow_next.gif" style="vertical-align: top" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Repeater ID="rptFollowUps" runat="server">
            <HeaderTemplate>
                <table class="crewquery-table">
                    <tr class="crewquery-table-header">
                        <td style="width: 100px">
                            Date
                        </td>
                        <td style="width: 150px">
                            From
                        </td>
                        <td>
                            Message
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="crewquery-table-row">
                    <td>
                        <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text='<%#Eval("Followup_Text")%>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="crewquery-table-alt-row">
                    <td>
                        <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>' ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text='<%#Eval("Followup_Text")%>'></asp:Label>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
