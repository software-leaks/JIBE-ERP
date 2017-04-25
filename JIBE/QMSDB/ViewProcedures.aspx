<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewProcedures.aspx.cs" Inherits="ViewProcedures" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/modalpopup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="gradiant-css-orange" style="font-size: 14px; padding: 4px">
        <div style="float: right">
            <asp:Panel ID="pnlCtl" runat="server" Visible="true">
                <asp:Label ID="lblProcedureId" runat="server" Width="20"></asp:Label>
                <asp:Button ID="btnSendTo" runat="server" Text="Send for Appoval" BorderStyle="Solid"
                    OnClick="btnSendTo_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                    Height="24px" BackColor="#81DAF5" />
                <asp:Button ID="btnFinalize" runat="server" Text="Publish Procedure" BorderStyle="Solid"
                    OnClick="btnFinalize_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                 
                    Height="24px" BackColor="#81DAF5" />
            </asp:Panel>
        </div>
        <img src="../images/wizard/database-process-icon.png" style="vertical-align: bottom" height="20px" /> 
        &nbsp;&nbsp; Procedure: &nbsp;&nbsp;<asp:Label ID="lblProcedureName" runat="server"></asp:Label></div>
    <div style="border: 1px solid inset; background-color: #efddef; text-align: left;">
        <asp:Label ID="lblDetails" runat="server"></asp:Label>
        <CKEditor:CKEditorControl ID="txtProcedureDetails" CssClass="cke_show_borders text-editor"
            ReadOnly="true" runat="server" Height="600px" AutoGrowMaxHeight="1500"></CKEditor:CKEditorControl>
    </div>
    <div id="DivSendForApp" title="Approval" style="border: 1px solid #efefef; padding: 10px;
        margin-top: 2px; display: none; width: 400px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel2" runat="server" Visible="true">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblUser" runat="server" Text="Send To" Enabled="true"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="lstUser" runat="server" Width="230px" SelectionMode="Multiple"
                                    DataTextField="UserName" DataValueField="UserID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblWorkStatus" runat="server" Text="For Work"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="230px">
                                    <asp:ListItem Text="Send For Approval" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Re-Work" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Width="240px"
                                    Enabled="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnSendApproval" runat="server" Text="Save" BorderStyle="Solid" OnClick="btnSendForApproval_Click"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
                                <asp:Button ID="btnCancelSend" runat="server" Text="Cancel" BorderStyle="Solid" OnClick="btnCancelApp_Click"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="DivApproval" title="Publis Comments" style="border: 1px solid #efefef; padding: 10px;
        margin-top: 2px; display: none; width: 400px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Visible="true">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="Label3" runat="server" Text="Comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtApprovalComments" runat="server" TextMode="MultiLine" Rows="5"
                                    Width="240px" Enabled="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="BtnSaveApproved" runat="server" Text="Publish" BorderStyle="Solid"
                                    OnClick="BtnSaveApproved_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" BackColor="#81DAF5" />
                                <asp:Button ID="btnCancelPublish" runat="server" Text="Cancel" BorderStyle="Solid"
                                    OnClick="btnCancelPublish_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" BackColor="#81DAF5" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
