<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SlopChestSettings.aspx.cs" Inherits="Purchase_SlopChestSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
        <div class="page-title">
            Slop Chest Settings
        </div>
        <table>
            <tr>
                <td colspan="2" style="font-size:14px;;font-weight:bold;">
                Chose settings from below : &nbsp;
                </td>
            </tr>
            <tr>
             <td>
             <asp:RadioButtonList ID="rbtlstSettings" runat="server"></asp:RadioButtonList>
            <%-- <asp:RadioButton ID="rbtAllItems" runat="server" Checked="false" 
                     Text="All available/consumed items in current month." GroupName="SCSettings"  />--%>
                </td>
            </tr>
         <%--   <tr>
             <td>
             <asp:RadioButton ID="rbtRmaining" runat="server" Checked="false" 
                     Text="Only remaining items of that month." GroupName="SCSettings" />
                </td>
            </tr>--%>
             <tr>
                <td colspan="2" align="center" >
              <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
