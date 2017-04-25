<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChildPopUp.aspx.cs" Inherits="ChildPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Child ASPX page As Popup/Child Page :: </title>
   <%-- 
    <link href="Styles/SMSxFenstorPopup.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/SMSxFenstor.js" type="text/javascript"></script>
    <script src="Scripts/SMSxFenstorPopup.js" type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <center>

    <div>
    
    
    </div>


     <div style="position: relative; overflow: hidden; clear:right;">  

        <div style="font-size: 14px; color: white; background-color: #669999;">
            Child Page - Pop up Page.
        </div>
        <table>

            <tr align="right">
                <td>
                    Name :&nbsp;&nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                  
                </td>
            </tr>
            <tr>
                <td align="right">
                    Email :&nbsp;&nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" 
                        onclick="btnRefresh_Click" />
                </td>
                <td>
                    <asp:Button ID="btnRefreshClose" runat="server" Text="Refresh & Close" />
                </td>
            </tr>
        </table>

     </div> 

     <div  class="tooltip-content" style="font-family:Tahoma;font-size:11px;">
        <div style="float: right; background-color: Yellow;">
            
            <span id="lblCardStatus"></span>
        </div>

        <table>
            <tr>
                <td style="font-weight: bold">
                    Last Vessel:
                                  
                    <span id="lblLastVessel">SANY</span>&nbsp; &nbsp;
                    Signed ON:
                    <span id="lblLastSignedOn">26/01/2011</span>&nbsp; &nbsp;
                    Signed OFF:
                    <span id="lblLastSignedOff">09/05/2011</span>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td style="font-weight: bold">
                    Last 2 remarks:
                </td>
            </tr>
            <tr>
                <td>
                   
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td style="font-weight: bold">
                    Complaints escalated by the staff:
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        
                                <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                    cellspacing="0">
                            
                                </table>
                            
                    </div>
                </td>
            </tr>
        </table>
    </div>

    </center>
    </form>
</body>
</html>

    </form>
</body>
</html>
