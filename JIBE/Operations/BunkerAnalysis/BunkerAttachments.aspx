<%@ Page Title="Bunker Attachments" Language="C#" AutoEventWireup="true" CodeFile="BunkerAttachments.aspx.cs"
    Inherits="Operations_BunkerAttachments" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Bunker Attachments</title>
     <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }    
    </style>
    <%--<script type="text/javascript" src="../scripts/jquery-1.5.2.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.14.custom.min.js"></script>--%>
</head>
<body>
    <form id="form1" tyle="display: block" runat="server">
     <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    <asp:HiddenField ID="hdnSample_ID" runat="server" />
    <asp:HiddenField ID="hdnTypeID" runat="server" />
    <div>
        <center>
            <div>
                <div style="font-family: Tahoma; font-size: 14px; font-weight: bold; padding: 5px;text-align:left;background-color:#2ECCFA">
                    Attachments</div>
                
                <table style="background-color: white; border: 1px solid #2ECCFA; border-collapse: collapse;
                    width: 100%;" border="1" cellpadding="5">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server">
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="dvAddAttachment" style="display: block;margin-top:15px;">
        <div style="font-family: Tahoma; font-size: 14px; font-weight: bold; padding: 5px;text-align:left;background-color:#F2F2F2">
                    Add Attachments</div>
        <table style="background-color: white; border: 1px solid #F2F2F2; border-collapse: collapse;
            width: 100%;" border="1" cellpadding="2">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlType1" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload_1" runat="server" Width="300px" />
                </td>
                <td>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                     <asp:Label ID="lblmsg1" runat="server"></asp:Label>
                </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlType2" runat="server">
                   </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload_2" runat="server" Width="300px" />
                </td>
                <td>
                  <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblmsg2" runat="server"></asp:Label>
        </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlType3" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload_3" runat="server" Width="300px" />
                </td>
                <td>
                  <div class="error-message" onclick="javascript:this.style.display='none';">
                <asp:Label ID="lblmsg3" runat="server"></asp:Label>
        </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:LinkButton ID="lnkAddAttachment" Text="Upload" runat="server" OnClick="lnkAddAttachment_Click" />
                </td>
            </tr>
        </table>
    </div>
        </center>
    </div>
    
    
    </form>
</body>
</html>
