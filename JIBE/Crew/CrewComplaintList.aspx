<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewComplaintList.aspx.cs"
    Inherits="Crew_CrewComplaintList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewComplaintList">
        <div>
            <table style="width: 100%; border: 1px solid #cccccc; font-family: Tahoma; font-size: 11px;">
                <tr class="gradiant-css-blue">
                    <td style="font-weight: bold; font-size: 12px;">
                        Complaints Escalated to DPA
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <div>
                            <asp:Repeater ID="rptComplaintsToDPA" runat="server">
                                <HeaderTemplate>
                                    <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                        cellspacing="0">
                                        <tr style="background-color: #627AA8; color: Aqua; font-weight: bold; text-align: center;">
                                            <td style="text-align: center; width: 20px;">
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl1" runat="server" Text="Vessel"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="lbl2" runat="server" Text="Escalated On"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lbl3" runat="server" Text="Escalated By"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl4" runat="server" Text="Complaint"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="text-align: center;">
                                        <td>
                                            <img src="../Images/Plus.png" alt="" class="dbx-toggle" onclick="showEscalationLog('<%# Eval("worklist_id")%>','<%# Eval("vessel_id")%>',<%= Request.QueryString["USERID"].ToString() %>)" />
                                        </td>
                                        <td>
                                            <%# Eval("vessel_name")%>
                                        </td>
                                        <td>
                                            <%# Eval("Escalated_On")%>
                                        </td>
                                        <td>
                                            <a href='CrewDetails.aspx?ID=<%# Eval("Escalated_By") %>' target="_blank">
                                                <%# Eval("Escalated_By_Staff_Code")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("Escalated_By_Rank")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("Escalated_by_Name")%>
                                        </td>
                                        <td style="text-align: left; text-decoration: none;">
                                            <a href='../Technical/worklist/ViewJob.aspx?OFFID=<%# Eval("office_id")%>&WLID=<%# Eval("worklist_id")%>&VID=<%# Eval("vessel_id")%>'
                                                target="_blank">
                                                <%# Eval("JOB_DESCRIPTION")%></a>
                                        </td>
                                        <td style="text-align: left; color: Red;">
                                            <%# Eval("status")%>
                                        </td>
                                    </tr>
                                    <tr class='<%# Eval("worklist_id")%>' style="display: none">
                                        <td colspan="8">
                                            <div id='dvLog<%# Eval("worklist_id")%>'>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%; border: 1px solid #cccccc; font-family: Tahoma; font-size: 11px;">
                <tr class="gradiant-css-blue">
                    <td style="font-weight: bold; font-size: 12px;">
                        Pending Crew Complaints
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <div>
                            <asp:Repeater ID="rptAllComplaints" runat="server">
                                <HeaderTemplate>
                                    <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                        cellspacing="0">
                                        <tr style="background-color: #627AA8; color: Aqua; font-weight: bold; text-align: center;">
                                            <td style="text-align: center; width: 20px;">
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl1" runat="server" Text="Vessel"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="lbl2" runat="server" Text="Escalated On"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lbl3" runat="server" Text="Escalated By"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl4" runat="server" Text="Complaint"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="text-align: center;">
                                        <td>
                                            <img src="../Images/Plus.png" alt="" class="dbx-toggle" onclick="showEscalationLog('<%# Eval("worklist_id")%>', '<%# Eval("vessel_id")%>',<%= Request.QueryString["USERID"].ToString() %>)" />
                                        </td>
                                        <td>
                                            <%# Eval("vessel_name")%>
                                        </td>
                                        <td>
                                            <%# Eval("Escalated_On")%>
                                        </td>
                                        <td>
                                            <a href='CrewDetails.aspx?ID=<%# Eval("Escalated_By") %>' target="_blank">
                                                <%# Eval("Escalated_By_Staff_Code")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("Escalated_By_Rank")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("Escalated_by_Name")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <a href='../Technical/worklist/ViewJob.aspx?OFFID=<%# Eval("office_id")%>&WLID=<%# Eval("worklist_id")%>&VID=<%# Eval("vessel_id")%>'>
                                                <%# Eval("JOB_DESCRIPTION")%></a>
                                        </td>
                                        <td style="text-align: left; color: Red;">
                                            <%# Eval("status")%>
                                        </td>
                                    </tr>
                                    <tr class='<%# Eval("worklist_id")%>' style="display: none">
                                        <td colspan="8">
                                            <div id='dvLog<%# Eval("worklist_id")%>'>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
