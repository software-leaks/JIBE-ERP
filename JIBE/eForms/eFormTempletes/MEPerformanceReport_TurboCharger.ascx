<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MEPerformanceReport_TurboCharger.ascx.cs"
    Inherits="eForms_eFormTempletes_MEPerformanceReport_TurboCharger" %>
<asp:FormView ID="frmTurboCharger" runat="server" Width="100%">
    <ItemTemplate>
        <table border="1" cellpadding="0" cellspacing="0" style="width:100%">
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="lbl1" runat="server" Text='<%#Eval("HRS_SINCE_TC_OVERHAUL")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label1" runat="server" Text='<%#Eval("EXHAUST_BEFORE_TURBINE")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label2" runat="server" Text='<%#Eval("EXHAUST_AFTER_TURBINE")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label3" runat="server" Text='<%#Eval("AIR_BEFORE_FILTER")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label4" runat="server" Text='<%#Eval("AIR_BEFORE_COOLER")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label5" runat="server" Text='<%#Eval("AIR_AFTER_COOLER")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label6" runat="server" Text='<%#Eval("CW_BEFORE_COOLER")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label7" runat="server" Text='<%#Eval("CW_AFTER_COOLER")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label8" runat="server" Text='<%#Eval("CW_BEFORE_BLOWER")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label9" runat="server" Text='<%#Eval("CW_AFTER_BLOWER")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label10" runat="server" Text='<%#Eval("OIL_GAS_SIDE")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label11" runat="server" Text='<%#Eval("OIL_AIR_SIDE")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label12" runat="server" Text='<%#Eval("PRESS_DROP_ACROSS_AIR_FILTER_ST")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label13" runat="server" Text='<%#Eval("PRESS_DROP_ACROSS_AIR_FILTER_END")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label14" runat="server" Text='<%#Eval("PRESS_DROP_ACROSS_AIR_COOLER_ST")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label15" runat="server" Text='<%#Eval("PRESS_DROP_ACROSS_AIR_COOLER_END")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td class="eform-field-data"><asp:Label ID="Label16" runat="server" Text='<%#Eval("GAS_INLET_TO_TURBINE")%>'></asp:Label></td>
                <td class="eform-field-data"><asp:Label ID="Label17" runat="server" Text='<%#Eval("GAS_OUTLET_FROM_TURBINE")%>'></asp:Label></td>
            </tr>


            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label26" runat="server" Text='<%#Eval("TURBINE_RPM")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label18" runat="server" Text='<%#Eval("HRS_SINCE_LO_PUMP_BRG_RENEWAL")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label21" runat="server" Text='<%#Eval("HRS_RATING_FOR_PP_BRGS")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label22" runat="server" Text='<%#Eval("HOURS_SINCE_CLEANING_AIR_FILTER")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label23" runat="server" Text='<%#Eval("HOURS_SINCE_CLEANING_AIR_COOLER")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label24" runat="server" Text='<%#Eval("HOURS_SINCE_TURBINE_WASH")%>'></asp:Label></td>
            </tr>
            <tr style="height:22px">
                <td colspan="2" class="eform-field-data"><asp:Label ID="Label25" runat="server" Text='<%#Eval("HOURS_SINCE_BLOWER_WASH")%>'></asp:Label></td>
            </tr>
            
        </table>
    </ItemTemplate>
</asp:FormView>
