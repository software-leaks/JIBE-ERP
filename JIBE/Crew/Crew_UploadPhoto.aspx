<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crew_UploadPhoto.aspx.cs" Inherits="Crew_Crew_UploadPhoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <table style="width: 100%;">
            
            <tr>
                <td>
                    Crew Photo
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click">
                    </asp:Button>
                    <input type="button" value="Close" onclick="parent.hideModal('dvPopupFrame');return false;" />
                </td>
            </tr>
            <tr>
            <td>
            <asp:Label id ="lblhdn"  runat="server" Visible = "false"></asp:Label>
            <asp:TextBox ID="txthdn" runat="server" Visible = "false"></asp:TextBox>
            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
