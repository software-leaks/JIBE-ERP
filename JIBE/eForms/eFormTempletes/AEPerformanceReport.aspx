<%@ Page Title="A/E Performance Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="AEPerformanceReport.aspx.cs" Inherits="eForms_eFormTempletes_AEPerformanceReport" %>

<%@ Register Src="AEPerfReport_Engine.ascx" TagName="AEPerfReport_Engine" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="AUXILIARY  ENGINES  PERFORMANCE  REPORT"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:FormView ID="frmMain" runat="Server" Width="100%">
                <ItemTemplate>
                    <table class="eform-report-table" cellpadding="2" border="0" width="100%">
                        <tr>
                            <td style="width: 50px">
                                M.V
                            </td>
                            <td style="text-align: left; width: 200px; border: 1px solid black;" class="eform-field-data">
                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                            </td>
                            <td style="text-align: right; width: 100px">
                                Month:
                            </td>
                            <td style="text-align: left; width: 100px; border: 1px solid black;" class="eform-field-data">
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Month_Name")%>'></asp:Label>
                            </td>
                            <td style="text-align: right;width: 100px">
                                Year:
                            </td>
                            <td style="text-align: left; width: 100px; border: 1px solid black;" class="eform-field-data">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Year_Name")%>'></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <table border="0" cellpadding="0" cellspacing="2" >
                <tr>
                    <td style="width: 200px;vertical-align:top;">
                        <table cellpadding="0" cellspacing="0" style="width:100%">
                            <tr>
                                <td style="height: 90px; vertical-align:middle;border: 1px solid black;text-align:center; font-weight:bold;">
                                    
                                                Unit Number And<br /> Other Parameters
                                </td>
                            </tr>
                            <tr>
                                <td style="min-height: 200; vertical-align: top">
                                    <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border-collapse:collapse">
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                1
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                2
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                3
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                4
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                5
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                6
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                7
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                8
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                9
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;height:21px;">
                                                10
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top:0px">
                                    <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border-collapse:collapse">
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                T/Charger RPM
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Scav. Press./ Temp.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Air Cooler Press.. drop
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                L.O. Press.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                L.O. Temp. IN | OUT
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Governor Load Index
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Electrical Load
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Injectors Renewal
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Cyl. Heads Overhaul
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;height:22.5px;">
                                                Complete Decarbonisation
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  style="min-height: 60px;vertical-align:middle;">
                                                Remarks
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top;border:1px solid black;">
                        <uc1:AEPerfReport_Engine ID="AEPerfReport_Engine1" runat="server" />
                    </td>
                    <td style="vertical-align: top; border:1px solid black;">
                        <uc1:AEPerfReport_Engine ID="AEPerfReport_Engine2" runat="server" />
                    </td>
                    <td style="vertical-align: top; border:1px solid black;">
                        <uc1:AEPerfReport_Engine ID="AEPerfReport_Engine3" runat="server" />
                    </td>
                    <td style="vertical-align: top; border:1px solid black;">
                        <uc1:AEPerfReport_Engine ID="AEPerfReport_Engine4" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
