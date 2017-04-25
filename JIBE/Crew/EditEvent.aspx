<%@ Page Title="Edit Event" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EditEvent.aspx.cs" Inherits="Crew_EditEvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
            
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        #page-content
        {
            font-family: Tahoma;
            font-size: 11px;
            background-color: White;
        }
        #page-content a:link
        {
            color: Blue;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: Blue;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: Black;
            text-decoration: none;
        }
        
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 2px 0px 2px;
        }
        
        .GridCSS
        {
            cursor: pointer;
        }
        .ListBox_CSS
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .tooltip
        {
            display: none; /*background: transparent url(../Images/bg-content-blue.jpg);*/
            font-size: 10px;
            width: 160px;
            padding: 5px;
            color: #000;
            background-color: #F3F781;
            border: 1px solid gray;
        }
        .linkbutton
        {
            font-family: Tahoma;
            cursor: pointer;
            padding: 5px;
            font-size: 12px;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <style type="text/css">
        #dvVesselMovements
        {
            overflow: auto;
            width: 1100px;
            text-align: left;
        }
        #dvVesselName
        {
            font-weight: bold;
        }
        .vessel-movement-table
        {
            text-align: center;
        }
        .vessel-movement-table td
        {
            border: 1px solid gray;
            width: 150px;
            background-color: #fff;
        }
        .vessel-movement-table div
        {
            width: 140px;
        }
        .vessel-movement-table .port-name
        {
            background-color: #336666;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }
        #dvLatestNoonReport
        {
            text-align: left;
            padding: 2px;
        }
        .noon-report-table
        {
            width: 100%;
            color: Black;
        }
    </style>
    <style type="text/css">
        .popup-background
        {
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }
        .popup-content
        {
            position: absolute;
            float: left;
            z-index: 1;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="page-title">
        Event Planner
     </div>
     <div id="page-content" style="min-height: 620px; border: 1px solid gray; z-index: -2;
        padding: 2px;">
        <asp:UpdatePanel ID="UpdatePanel_PortCalls" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlPortCalls" runat="server" Visible="true">
                    <div id="dvVesselMovement">
                        <table style="width: 100%; margin-top: 20px; border: 1px solid #aabbee; background-color: #A9D0F5;">
                            <tr>
                                <td style="text-align: left; color: Black; font-weight: bold; font-size: 12px;">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                Select Port Call
                                            </td>
                                            <td>
                                            </td>
                                            <td style="text-align: right">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; vertical-align: top;">
                                    <div id="dvContainer">
                                        <table style="width: 100%; border: 1px solid gray; margin-top: 5px;">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvPortCalls" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="1px" CellPadding="2"
                                                        AllowPaging="true" PageSize="15" GridLines="Horizontal" DataKeyNames="Port_Call_ID"
                                                        Font-Size="11px" Width="100%" OnSelectedIndexChanged="gvPortCalls_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Vessel">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Port Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPort_Name" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnPortID" runat="server" Value='<%# Eval("Port_ID")%>'></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Arrival">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival"))) %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Departure" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure"))) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Owners Agent" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOwners_Agent" runat="server" Text='<%# Eval("Owners_Agent")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Charterers Agent" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCharterers_Agent" runat="server" Text='<%# Eval("Charterers_Agent")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                                        CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                            CssClass="pager" />
                                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
                                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEditEvent" runat="server" Visible="true">
                    <div style="background-color: transparent;">
                        <div style="width: 100%; margin-top: 20px; border: 1px solid #aabbee; background-color: #F7F8E0;">
                            <asp:HiddenField ID="hdnEditEventID" runat="server" />
                            <table>
                                <tr>
                                    <td style="text-align: left; color: Black; font-weight: bold; font-size: 12px;" colspan="5">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    Event Details
                                                </td>
                                                <td>
                                                </td>
                                                <td style="text-align: right">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Event Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditEventDate" runat="server" Width="100px" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditEventDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="width: 20px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        Port:
                                    </td>
                                    <td style="color: Black; font-weight: bold;">
                                        <asp:Label ID="lblPort" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align: center">
                                        <asp:TextBox ID="txtEventRemark" runat="server" TextMode="MultiLine" Width="400px"
                                            Height="150px" MaxLength="1000"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align: center">
                                        <asp:Button ID="btnSaveEventEdit" runat="server" Text="Save" OnClick="btnSaveEventEdit_Click" ClientIDMode="Static" />
                                        <asp:Button ID="btnCloseEventEdit" runat="server" Text="Close" OnClick="btnCloseEventEdit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var strDateFormat = "<%= DateFormat %>";
            var CurrentDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';
            $("body").on("click", "#btnSaveEventEdit", function () {
                var Msg = "";
                if ($("[id$='txtEditEventDate']").val() == "") {
                    alert("Event Date is mandatory");
                    return false;
                }
                else {
                    if (IsInvalidDate($("#txtEditEventDate").val(), strDateFormat)) {
                        Msg = "Enter valid Event Date" + CurrentDateFormatMessage;
                        alert(Msg);
                        return false;
                    }
                }
                return true;
            });
        });
    </script>
</asp:Content>
