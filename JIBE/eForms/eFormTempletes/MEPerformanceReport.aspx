<%@ Page Title="eForms: Main Engine Performance Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MEPerformanceReport.aspx.cs" Inherits="eForms_eFormTempletes_MEPerformanceReport" %>

<%@ Register Src="MEPerformanceReport_TurboCharger.ascx" TagName="MEPerformanceReport_TurboCharger"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .eform-vertical-text
        {
            font: bold 14px verdana;
            font-weight: normal;
            writing-mode: tb-rl;
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="MAIN ENGINE PERFORMANCE REPORT"></asp:Label>
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
                            <td style="text-align: right; width: 100px">
                                Year:
                            </td>
                            <td style="text-align: left; width: 100px; border: 1px solid black;" class="eform-field-data">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Year_Name")%>'></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; border: 1px solid black; margin-top: 2px;" border="1">
                        <tr>
                            <td style="vertical-align: top">
                                <table style="width: 100%;" border="1">
                                    <tr>
                                        <td>
                                            Engine Make & Type
                                        </td>
                                        <td colspan="4">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("ENGINE_MAKE_AND_TYPE")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MCR Rating
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("MCR_RATING")%>'></asp:Label>
                                        </td>
                                        <td>
                                            HP/ kW at
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("HP_KW_AT")%>'></asp:Label>
                                        </td>
                                        <td>
                                            rpm
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Voyage from
                                        </td>
                                        <td class="eform-field-data" colspan="3">
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("VOYAGE_FROM")%>'></asp:Label>
                                        </td>
                                        <td>
                                            rpm
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            to
                                        </td>
                                        <td class="eform-field-data" colspan="3">
                                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("VOYAGE_TO")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Deadweight
                                        </td>
                                        <td class="eform-field-data" colspan="2">
                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("DEADWEIGHT")%>'></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            mT
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Displacement
                                        </td>
                                        <td class="eform-field-data" colspan="2">
                                            <asp:Label ID="Label10" runat="server" Text='<%#Eval("DISPLACEMENT")%>'></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            mT
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Draft&nbsp;&nbsp;&nbsp;&nbsp;Fwd
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label11" runat="server" Text='<%#Eval("DRAFT_FWD")%>'></asp:Label>
                                        </td>
                                        <td>
                                            m&nbsp;&nbsp; Aft
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label12" runat="server" Text='<%#Eval("DRAFT_AFT")%>'></asp:Label>
                                        </td>
                                        <td>
                                            m
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Barometer
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("BAROMETER")%>'></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            mb Atm. Temp.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Wind Force
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label14" runat="server" Text='<%#Eval("WIND_FORCE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            Rel. Direction
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label15" runat="server" Text='<%#Eval("WIND_FORCE_DIRECTION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            deg.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sea State
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("SEA_STATE")%>'></asp:Label>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Swell (m)
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("SWELL")%>'></asp:Label>
                                        </td>
                                        <td>
                                            Rel. Direction
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label18" runat="server" Text='<%#Eval("SWELL_DIRECTION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            deg.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Current
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label19" runat="server" Text='<%#Eval("CURRENT_KNOTS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            knots
                                        </td>
                                        <td class="eform-field-data" colspan="2">
                                            <asp:Label ID="Label20" runat="server" Text='<%#Eval("CURRENT_FAVOURABLE")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table style="width: 100%;" border="1">
                                    <tr>
                                        <td>
                                            Power Developed
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label21" runat="server" Text='<%#Eval("POWER_DEVELOPED")%>'></asp:Label>
                                        </td>
                                        <td>
                                            HP/ kW
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Shaft Generator
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("SHAFT_GENERATOR")%>'></asp:Label>
                                        </td>
                                        <td>
                                            HP/ kW
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Shaft Power
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("SHAFT_POWER")%>'></asp:Label>
                                        </td>
                                        <td>
                                            HP/ kW
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Prop. Pitchsetting
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label24" runat="server" Text='<%#Eval("PROP_PITCHSETTING")%>'></asp:Label>
                                        </td>
                                        <td>
                                            deg.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            RPM
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label25" runat="server" Text='<%#Eval("RPM")%>'></asp:Label>
                                        </td>
                                        <td class="eform-field-data">
                                            Slip %:
                                            <asp:Label ID="Label26" runat="server" Text='<%#Eval("SLIP_P")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Vessel's Speed
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label27" runat="server" Text='<%#Eval("VESSEL_SPEED")%>'></asp:Label>
                                        </td>
                                        <td>
                                            knots
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Load Indicator
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label28" runat="server" Text='<%#Eval("LOAD_INDICATOR")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Speed Setting Air
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label29" runat="server" Text='<%#Eval("SPEED_SETTING_AIR")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Governor Index
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label30" runat="server" Text='<%#Eval("GOVERNOR_INDEX")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VIT Setting
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label31" runat="server" Text='<%#Eval("VIT_SETTING")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Indicator Spring
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label32" runat="server" Text='<%#Eval("INDICATOR_SPRING")%>'></asp:Label>
                                        </td>
                                        <td>
                                            mm or kg/cm2
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cylinder Constant
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label33" runat="server" Text='<%#Eval("CYLINDER_CONSTANT")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table style="width: 100%;" border="1">
                                    <tr>
                                        <td>
                                            Fuel in Use
                                        </td>
                                        <td class="eform-field-data" colspan="2">
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("FUEL_IN_USE")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fuel Spec. Gravity
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label35" runat="server" Text='<%#Eval("FUEL_SPEC_GRAVITY")%>'></asp:Label>
                                        </td>
                                        <td class="eform-field-data">
                                            at
                                            <asp:Label ID="Label36" runat="server" Text='<%#Eval("FUEL_SPEC_GRAVITY_C")%>'></asp:Label>
                                            C
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            F.O. day tank
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label37" runat="server" Text='<%#Eval("POWER_DEVELOPED")%>'></asp:Label>
                                        </td>
                                        <td>
                                            C
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            F.O. Temp at injection
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label38" runat="server" Text='<%#Eval("FO_TEMP_AT_INJECTION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            C
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            F.O. Viscosity at injection
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label39" runat="server" Text='<%#Eval("FO_VISCOSITY_AT_INJECTION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            cSt
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sulphur %
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label40" runat="server" Text='<%#Eval("SULPHUR_P")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            FQS Setting
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label41" runat="server" Text='<%#Eval("FQS_SETTING")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            F.O. Cons.
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label42" runat="server" Text='<%#Eval("FO_CONS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            mT / 24 hr
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            F.O. Specific Cons.
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label43" runat="server" Text='<%#Eval("FO_SPECIFIC_CONS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            gms/BHP/hr
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cyl. Oil Grade, TBN
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label44" runat="server" Text='<%#Eval("CYL_OIL_GRADE")%>'></asp:Label>
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label45" runat="server" Text='<%#Eval("CYL_OIL_TBN")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cyl. Oil Cons.
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label46" runat="server" Text='<%#Eval("CYL_OIL_CONS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            ltr / 24 hr
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cyl. Oil Specific Cons.
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label47" runat="server" Text='<%#Eval("CYL_OIL_SPECIFIC_CONS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            gms/BHP/hr
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Syst. Oil Cons.
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label48" runat="server" Text='<%#Eval("SYST_OIL_CONS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            ltr / 24 hr
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <asp:GridView ID="GridView_CYL" DataKeyNames="CYL_NO" runat="server" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                GridLines="Both" OnRowCreated="GridView_CYL_RowCreated" OnRowDataBound="GridView_CYL_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Cyl. no.">
                        <ItemTemplate>
                            <%# Eval("CYL_NO")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            Avg
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Press. max.">
                        <ItemTemplate>
                            <%# Eval("PRESS_MAX")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_PRESS_MAX" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Press. comp.">
                        <ItemTemplate>
                            <%# Eval("PRESS_COMP")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_PRESS_COMP" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Press(max-comp)">
                        <ItemTemplate>
                            <%# Eval("PRESS_MAX_COMP")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_PRESS_MAX_COMP" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MIP">
                        <ItemTemplate>
                            <%# Eval("MIP")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_MIP" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Index">
                        <ItemTemplate>
                            <%# Eval("FUEL_PUMP_INDEX")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_FUEL_PUMP_INDEX" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V.I.T">
                        <ItemTemplate>
                            <%# Eval("FUEL_PUMP_VIT")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_FUEL_PUMP_VIT" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exh.Temp.">
                        <ItemTemplate>
                            <%# Eval("EXH_TEMP")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_EXH_TEMP" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.H.P.">
                        <ItemTemplate>
                            <%# Eval("IHP")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_IHP" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max. Cyl. Wear at Last Overhaul">
                        <ItemTemplate>
                            <%# Eval("MAX_CYL_WEAR_AT_LASTOVERHAUL")%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAVG_MAX_CYL_WEAR_AT_LASTOVERHAUL" runat="server"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="New Liner fitted">
                        <ItemTemplate>
                            <%# Eval("RH_NEW_LINER_FITTED")%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit  Overhaul">
                        <ItemTemplate>
                            <%# Eval("RH_UNIT_OVERHAUL")%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exh. V/v overhaul">
                        <ItemTemplate>
                            <%# Eval("RH_EXH_VLV_OVERHAUL")%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Injector renewed">
                        <ItemTemplate>
                            <%# Eval("RH_INJECTOR_RENEWED")%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
            </asp:GridView>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr style="text-align: center; background-color: #cccccc; height: 22px; font-weight: bold;
                    font-size: 14px;">
                    <td style="width: 200px">
                        M.E. Turbochargers
                    </td>
                    <td style="width: 150px">
                        1
                    </td>
                    <td style="width: 150px">
                        2
                    </td>
                    <td style="width: 150px">
                        3
                    </td>
                    <td style="width: 150px">
                        4
                    </td>
                    <td>
                        Miscellaneous Data
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <div style="vertical-align: top; border: 1px solid black; margin-top: 1px;">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="vertical-align: middle; border-right: 1px solid black; border-bottom: 1px solid black;">
                                        <span class="eform-vertical-text">Temp. deg C</span>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    Hrs. since T/C Overhaul
                                                </td>
                                            </tr>
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    Exhaust Before/After Turbine
                                                </td>
                                            </tr>
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    Air Before Filter
                                                </td>
                                            </tr>
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    Air Before / After Cooler
                                                </td>
                                            </tr>
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    C.W. Before / After Cooler
                                                </td>
                                            </tr>
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    C.W. Before / After Blower
                                                </td>
                                            </tr>
                                            <tr style="height: 23px; text-align: left;">
                                                <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                                    Oil : Gas / Air Side
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; border-bottom: 1px solid black; padding-left: 2px;">
                                        Press Drop Across Air Filter
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Press. Drop Across Air Cooler
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Gas inlet to / outlet from turbine
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Turbine R. P. M.
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Hrs. Since LO Pump, Brg Renewal
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Hrs. Rating for P/P Brgs
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Hours Since Cleaning Air Filter
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Hours Since Cleaning Air Cooler
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="border-bottom: 1px solid black; padding-left: 2px;">
                                        Hours Since Turbine wash
                                    </td>
                                </tr>
                                <tr style="height: 22px; text-align: left;">
                                    <td style="padding-left: 2px;">
                                        Hours since Blower wash
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <uc1:MEPerformanceReport_TurboCharger ID="TC1" runat="server" />
                    </td>
                    <td style="vertical-align: top">
                        <uc1:MEPerformanceReport_TurboCharger ID="TC2" runat="server" />
                    </td>
                    <td style="vertical-align: top">
                        <uc1:MEPerformanceReport_TurboCharger ID="TC3" runat="server" />
                    </td>
                    <td style="vertical-align: top">
                        <uc1:MEPerformanceReport_TurboCharger ID="TC4" runat="server" />
                    </td>
                    <td style="vertical-align: top; border: 1px solid black;">
                        <asp:FormView ID="frmMiscData" runat="Server" Width="100%">
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td style="vertical-align: middle; border-right: 1px solid black; border-bottom: 1px solid black;">
                                            <span class="eform-vertical-text">Temp. deg C</span>
                                        </td>
                                        <td>
                                            <table cellpadding="2" cellspacing="0" style="width: 100%; border-collapse: collapse;"
                                                border="1">
                                                <tr style="height: 22px; text-align: left;">
                                                    <td colspan="2">
                                                        Engine Room (T/B Inlet)
                                                    </td>
                                                    <td colspan="2" class="eform-field-data">
                                                        <asp:Label ID="Label48" runat="server" Text='<%#Eval("TEMP_ER_TB_INLET")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td colspan="2">
                                                        Scavenge Air Belt
                                                    </td>
                                                    <td colspan="2" class="eform-field-data">
                                                        <asp:Label ID="Label34" runat="server" Text='<%#Eval("TEMP_SCAVENGE_AIR_BELT")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td colspan="2">
                                                        Sea water
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label49" runat="server" Text='<%#Eval("TEMP_SEA_WATER1")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label50" runat="server" Text='<%#Eval("TEMP_SEA_WATER2")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Jkt Clg IN / OUT(max./min)
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label53" runat="server" Text='<%#Eval("TEMP_JKT_CLG_IN")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label51" runat="server" Text='<%#Eval("TEMP_JKT_CLG_OUT_MAX")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label52" runat="server" Text='<%#Eval("TEMP_JKT_CLG_OUT_MIN")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Pist.Clg IN / OUT(max./min)
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label54" runat="server" Text='<%#Eval("TEMP_PIST_CLG_IN")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label55" runat="server" Text='<%#Eval("TEMP_PIST_CLG_OUT_MAX")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label56" runat="server" Text='<%#Eval("TEMP_PIST_CLG_OUT_MIN")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        L.O. Before/After Coolers
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label58" runat="server" Text='<%#Eval("TEMP_LO_BEFORE_COOLERS")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2" class="eform-field-data">
                                                        <asp:Label ID="Label59" runat="server" Text='<%#Eval("TEMP_LO_AFTER_COOLERS")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td colspan="2">
                                                        Stern tube Bearing
                                                    </td>
                                                    <td colspan="2" class="eform-field-data">
                                                        <asp:Label ID="Label60" runat="server" Text='<%#Eval("TEMP_STERN_TUBE_BEARING")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td colspan="2">
                                                        Thrust bearing
                                                    </td>
                                                    <td colspan="2" class="eform-field-data">
                                                        <asp:Label ID="Label61" runat="server" Text='<%#Eval("TEMP_THRUST_BEARING")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: middle; border-right: 1px solid black; border-bottom: 1px solid black;">
                                            <span class="eform-vertical-text">Press. Kg/cm2</span>
                                        </td>
                                        <td>
                                            <table cellpadding="2" cellspacing="0" style="width: 100%; border-collapse: collapse;"
                                                border="1">
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Scavenge Air Belt
                                                    </td>
                                                    <td  class="eform-field-data">
                                                        <asp:Label ID="Label62" runat="server" Text='<%#Eval("PRES_SCAVENGE_AIR_BELT")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Jacket Cooling
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label63" runat="server" Text='<%#Eval("PRES_JACKET_COOLING")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Piston Cooling
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label64" runat="server" Text='<%#Eval("PRES_PISTON_COOLING")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Lub oil to Main Brgs.
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label65" runat="server" Text='<%#Eval("PRES_LO_TO_MAIN_BRGS")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Fuel oil
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label66" runat="server" Text='<%#Eval("PRES_FO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Sea water
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label67" runat="server" Text='<%#Eval("PRES_SEA_WATER")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: middle; border-right: 1px solid black; border-bottom: 1px solid black;">
                                            <span class="eform-vertical-text">Press. WC</span>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <table cellpadding="2" cellspacing="0" style="width: 100%; border-collapse: collapse;"
                                                border="1">
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Crankcase (Med.Spd. Engine)
                                                    </td>
                                                    <td  class="eform-field-data">
                                                        <asp:Label ID="Label70" runat="server" Text='<%#Eval("PRES_CRANKCASE")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        Exhaust manifold
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label69" runat="server" Text='<%#Eval("PRES_EXHAUST_MANIFOLD")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 22px; text-align: left;">
                                                    <td>
                                                        across EGE
                                                    </td>
                                                    <td  class="eform-field-data">
                                                        <asp:Label ID="Label68" runat="server" Text='<%#Eval("PRES_ACROSS_EGE")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
