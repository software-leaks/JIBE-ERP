<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AEPerfReport_Engine.ascx.cs"
    Inherits="eForms_eFormTempletes_AEPerfReport_Engine" %>
<asp:FormView ID="frmEngine" runat="server">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table border="0">
                        <tr>
                            <td colspan="2" style="border-bottom:1px solid black;text-align: center; font-weight: bold">
                                Engine No.
                                <asp:Label ID="lblEngineNo" runat="server" Text='<%#Eval("Engine_No")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;height:20px;">
                                Date Measured:
                            </td>
                            <td style="border:1px solid black;width: 150px;" class="eform-field-data">
                                <asp:Label ID="lblDateMeasured" runat="server" Text='<%#Eval("Date_Measured","{0:dd/MM/yyyy}")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px; height:20px;">
                                Load %
                            </td>
                            <td style="border:1px solid black;width: 150px" class="eform-field-data">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("LOAD_P")%>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="min-height: 200; vertical-align: top">
                    <asp:GridView ID="GridView_Units" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="2" AllowPaging="false"
                        Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="Both">
                        <Columns>                            
                            <asp:TemplateField HeaderText="P-Max">
                                <ItemTemplate>
                                    <%# Eval("P_MAX")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fuel Rack">
                                <ItemTemplate>
                                    <%# Eval("FUEL_RACK")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exh. Temp">
                                <ItemTemplate>
                                    <%# Eval("EXH_TEMP")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jkt. Temp">
                                <ItemTemplate>
                                    <%# Eval("JKT_TEMP")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle Height="22" CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border-collapse:collapse">
                        <tr>
                            <td class="eform-field-data" colspan="4" >
                                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%#Eval("TCHARGER_RPM")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="2" style="width:50%;">
                                &nbsp;<asp:Label ID="Label3" runat="server" Text='<%#Eval("SCAV_PRESS")%>'></asp:Label>
                            </td>
                            <td class="eform-field-data" colspan="2" >
                                &nbsp;<asp:Label ID="Label4" runat="server" Text='<%#Eval("SCAV_TEMP")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="4">
                                &nbsp;<asp:Label ID="Label5" runat="server" Text='<%#Eval("AIRCOOLER_PRESSDROP")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="4">
                                &nbsp;<asp:Label ID="Label6" runat="server" Text='<%#Eval("LO_PRESS")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="2">
                                &nbsp;<asp:Label ID="Label7" runat="server" Text='<%#Eval("LO_TEMP_IN")%>'></asp:Label>
                            </td>
                            <td class="eform-field-data" colspan="2">
                                &nbsp;<asp:Label ID="Label8" runat="server" Text='<%#Eval("LO_TEMP_OUT")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="4">
                                &nbsp;<asp:Label ID="Label9" runat="server" Text='<%#Eval("GOVERNOR_LOAD_INDEX")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data"  style="width:25%;">
                                &nbsp;<asp:Label ID="Label12" runat="server" Text='<%#Eval("ELECTRICA_LLOAD_KW")%>'></asp:Label>
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="Label10" runat="server" Text="kW"></asp:Label>
                            </td>
                            <td class="eform-field-data" style="width:25%">
                                &nbsp;<asp:Label ID="Label13" runat="server" Text='<%#Eval("ELECTRICA_LLOAD_AMPS")%>'></asp:Label>
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="Label11" runat="server" Text="Amps"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="2" style="width:50%;">
                                &nbsp;<asp:Label ID="Label14" runat="server" Text='<%#Eval("INJECTORS_RENEWAL")%>'></asp:Label>
                            </td>
                            <td class="eform-field-data" colspan="2">
                                &nbsp;<asp:Label ID="Label15" runat="server" Text="hrs."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="2" style="width:50%;">
                                &nbsp;<asp:Label ID="Label16" runat="server" Text='<%#Eval("CYL_HEADS_OVERHAUL")%>'></asp:Label>
                            </td>
                            <td class="eform-field-data" colspan="2">
                                &nbsp;<asp:Label ID="Label17" runat="server" Text="hrs."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="2" style="width:50%;">
                                &nbsp;<asp:Label ID="Label18" runat="server" Text='<%#Eval("COMP_DECARBONISATION")%>'></asp:Label>
                            </td>
                            <td class="eform-field-data" colspan="2">
                                <asp:Label ID="Label19" runat="server" Text="hrs."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-field-data" colspan="4" style="min-height: 50px;vertical-align:top;">
                                &nbsp;<asp:Label ID="Label20" runat="server" Text='<%#Eval("REMARKS")%>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
