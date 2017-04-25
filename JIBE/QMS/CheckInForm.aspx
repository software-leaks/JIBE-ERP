<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckInForm.aspx.cs" Inherits="CheckInForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check In Form</title>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/Main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function SaveConfirmation() {
            var resp = window.confirm('File is already exists.\n do you want create another version?');
            if (resp == true) {

                document.getElementById('hdnVesionChecked').value = resp;
                window.form1.submit();
            }
        }

    </script>
    <style type="text/css">
        body
        {
            font-family:Tahoma;
            font-size:11px;
        }
        .fileBrowser
        {
            width: 380px;
        }
        .fileBrowser input
        {
            width: 300px;
        }
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
    <br />
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
      <div style="background-color: Yellow; font-size: 12px; color: Red; text-align: center;">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <br />
    <asp:Panel ID="pnlForFileUpload" runat="server">
        <table cellpadding="3" cellspacing="3" style="border-color: Blue; border-width: 1px;"
            align="center" border="1">
            <tr>
                <td align="center" valign="top">
                    <asp:Label ID="lblFormTitle" runat="server" Text="CHECK IN" Font-Bold="True" ForeColor="#003366"
                        ToolTip="this is a check in form"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblFileName" runat="server" Text="Document Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileName" runat="server" ReadOnly="true" Width="380px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDocName" runat="server" Text="Target Folder Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFolderName" ReadOnly="true" runat="server" Width="377px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:FileUpload ID="CheckInfileUp" runat="server" CssClass="fileBrowser" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="374px" Height="77px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Check In" OnClick="btnSave_Click" />
                                <asp:HiddenField ID="hdnVesionChecked" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
