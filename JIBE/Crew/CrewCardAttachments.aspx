<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewCardAttachments.aspx.cs" Inherits="Crew_CrewCardAttachments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater runat="server" ID="rptAttachments">
            <HeaderTemplate>
                <table style='width: 100%;'>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("AttachmentType")%>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <%# "<a href='../Uploads/CrewDocuments/" + Eval("ATTACHMENT_PATH").ToString() + "' target='_blank'>"%>
                        <%# Eval("ATTACHMENT_NAME").ToString() + "</a>" %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
