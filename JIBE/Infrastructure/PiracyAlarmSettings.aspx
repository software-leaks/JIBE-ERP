<%@ Page Language="C#" Title="Piracy Alarm Settings" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="PiracyAlarmSettings.aspx.cs"
    Inherits="DPLMap_PiracyAlarmSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/InfoBox.js" type="text/javascript"></script>
    <style type="text/css">
        .grid-border
        {
            -moz-border-radius: 15px;
            border-radius: 15px;
            background-color: #efefef;
            border: 1px solid #999;
            width: 800px;
            padding: 5px;
            color: #333;
            font-family: Tahoma,Verdana;
            border-bottom: 1px solid #555;
            border-right: 1px solid #555;
        }
        .grid-row
        {
            border-bottom: 1px solid #888;
        }
        .grid-row td
        {
            padding: 4px;
        }
    </style>
    <script type="text/javascript">
        function initStartupScripts() {
            try {
                $('.draggable').draggable();
                $('.InfoBox').Info();
            }
            catch (ex) { }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Piracy Alarm Settings"></asp:Label>
    </div>
    <div class="page-content-main">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="Update1">
        <ContentTemplate>
            <center>
                <div class="grid-border" style="padding-bottom: 20px; margin: 20px; background-image: url(../images/section-header.png);
                    background-repeat: repeat-x;">
                    <asp:GridView ID="GridView_PiracyAlarm" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                        Width="100%" GridLines="None" OnRowCommand="GridView_PiracyAlarm_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("Vessel_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alarm Status" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAlarmStatus" runat="server" ImageUrl='<%#Eval("Alarm_Status").ToString() =="0"? "../images/off.png": "../images/on.png"%>'
                                        Height="20" CommandName="Alarm_Status" CommandArgument='<%# Eval("Vessel_ID").ToString() + "," + Eval("Alarm_Status").ToString() %>'>
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reasons for Status Change" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Font-Size="10">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Log" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgLog" runat="server" ImageUrl="../Images/note.png" Height="18"
                                        CssClass="InfoBox" Visible='<%#Eval("Remarks").ToString() ==""?false:true%>'
                                        vessel_id='<%# Eval("vessel_id") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="grid-row" />
                        <HeaderStyle Height="30" ForeColor="White" Font-Bold="false" />
                    </asp:GridView>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvRemarks" style="display: none">
        <div style="margin: 10px;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnVessel_ID" runat="server" />
                    <table>
                        <tr>
                            <td>
                                New Status
                            </td>
                            <td>
                                <asp:Image ID="imgNewStatus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reasons for Status Change
                            </td>
                            <td>
                                <asp:Label ID="lblNoAccess" runat="server" Text="You don't have sufficient previlege to edit the status."
                                    Visible="false"></asp:Label>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="100" Width="400"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="btnSaveStatus" runat="server" Text="Save" OnClick="btnSaveStatus_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
</asp:Content>
