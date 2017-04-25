<%@ Page Title="M/E Monthly Running Hrs Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MERunningHoursMonthlyReport.aspx.cs" Inherits="eForms_eFormTempletes_MERunningHoursMonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr  class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="M/E Monthly Running Hrs Report"></asp:Label>
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
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%; text-align: left; vertical-align: top;">
                                            <table class="eform-report-table" cellpadding="1" border="1">
                                                <tr>
                                                    <td style="width: 150px">
                                                        Vessel Name:
                                                    </td>
                                                    <td style="width: 150px" class="eform-field-data">
                                                        <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        M/E Type:
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ME_Type")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        Report Date:
                                                    </td>
                                                    <td style="text-align: left" class="eform-field-data">
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("ReportDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%; text-align: right; vertical-align: top;">
                                            <table cellpadding="1" border="1">                                                
                                                <tr>                                                    
                                                    <td>
                                                        Total R/H Last Month
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Total_RH_LastMonth")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>                                                    
                                                    <td>
                                                        Running Hrs This Month
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Total_RH_ThisMonth")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>                                                    
                                                    <td>
                                                        Total Running Hrs
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("ME_Total_RH")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-report-sub-header">
                                Running Hours from Last O/H
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_RunningHours" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                                    Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                                    GridLines="Both">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S/N">
                                            <ItemTemplate>
                                                <%# Eval("ID")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CYL.HEAD O/H">
                                            <ItemTemplate>
                                                <%# Eval("CYL_HEAD_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PISTON O/H" >
                                            <ItemTemplate>
                                                <%# Eval("PISTON_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EXH.V/V O/H">
                                            <ItemTemplate>
                                                <%# Eval("EXH_VV_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="F.O V/V O/H">
                                            <ItemTemplate>
                                                <%# Eval("FO_VV_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FUEL PUMP O/H">
                                            <ItemTemplate>
                                                <%# Eval("FUEL_PUMP_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="START V/V O/H">
                                            <ItemTemplate>
                                                <%# Eval("START_VV_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RELIEF V/V O/H">
                                            <ItemTemplate>
                                                <%# Eval("RELIEF_VV_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LINER MAX WEAR AT LAST O/H">
                                            <ItemTemplate>
                                                <%# Eval("LINER_MAX_WEAR_AT_LAST_OH")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="150"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px" ></td>
                        </tr>
                        <tr>
                            <td >
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%; text-align: left; vertical-align: top;">
                                            <table cellpadding="1" border="1">
                                                <tr class="eform-header-row">
                                                    <td style="width: 250px">
                                                        ITEM
                                                    </td>
                                                    <td style="width: 150px">
                                                        GOVERNOR
                                                    </td>
                                                    <td style="width: 150px">
                                                        TCH #1
                                                    </td>
                                                    <td style="width: 150px">
                                                        TCH #2
                                                    </td>
                                                    <td style="width: 150px">
                                                        TCH #3
                                                    </td>
                                                </tr>
                                                <tr class="eform-header-row">
                                                    <td>
                                                        Running Hours for O/H as recommended by Maker
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("RH_From_Last_OH_GOVERNOR")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("RH_From_Last_OH_TCH1")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label16" runat="server" Text='<%#Eval("RH_From_Last_OH_TCH2")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label17" runat="server" Text='<%#Eval("RH_From_Last_OH_TCH3")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left" class="eform-header-row">
                                                        Running Hrs from Last O/H
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("RH_From_Last_OH_GOVERNOR")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label18" runat="server" Text='<%#Eval("RH_From_Last_OH_TCH1")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label19" runat="server" Text='<%#Eval("RH_From_Last_OH_TCH2")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label20" runat="server" Text='<%#Eval("RH_From_Last_OH_TCH2")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%; text-align: right; vertical-align: top;">
                                            <table style="width: 100%;"  cellpadding="1" border="1">                                                
                                                <tr class="eform-header-row">                                                    
                                                    <td style="width:150px">
                                                        ITEM
                                                    </td>
                                                    <td>
                                                        CRANK CASE INSPECTION
                                                    </td>
                                                    <td>
                                                        CRANK SHAFT DEFLECTION
                                                    </td>
                                                    <td>
                                                        LUB OIL SAMPLE LANDED
                                                    </td>
                                                </tr>
                                                <tr>                                                    
                                                    <td  class="eform-header-row">
                                                        Last Done Date
                                                    </td>
                                                    <td class="eform-field-data" style="text-align:center">
                                                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("CRANK_CASE_INSPECTION_LAST_DONE","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data" style="text-align:center">
                                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("CRANK_SHAFT_DEFLECTION_LAST_DONE","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data" style="text-align:center">
                                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("LUBE_OIL_SAMPLE_LANDED_LAST_DONE","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                </tr>
              
                                            
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px" ></td>
                        </tr>
                        <tr>
                            <td style="background-color:#cccccc;">
                                <table style="width: 100%;background-color:#efefef;" >
                                    <tr>
                                        <td style="width: 50%; text-align: left; vertical-align: top;">
                                            <table class="eform-report-table" cellpadding="1" border="1">
                                                <tr class="eform-header-row">
                                                    <td style="width: 250px">
                                                        AIR COOLERS
                                                    </td>
                                                    <td style="width: 150px">
                                                        #1
                                                    </td>
                                                    <td style="width: 150px">
                                                        #2
                                                    </td>
                                                    <td style="width: 150px">
                                                        #3
                                                    </td>
                                                </tr>
                                                <tr class="eform-header-row">
                                                    <td>
                                                        Air Side - Running Hr From Last Cleaning
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("AC1_Air_Side_RH_From_Last_Cleaning")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label12" runat="server" Text='<%#Eval("AC2_Air_Side_RH_From_Last_Cleaning")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="Label13" runat="server" Text='<%#Eval("AC3_Air_Side_RH_From_Last_Cleaning")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left" class="eform-header-row">
                                                        Water Side - Running Hr From Last Cleaning
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label21" runat="server" Text='<%#Eval("AC1_Water_Side_RH_From_Last_Cleaning")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label22" runat="server" Text='<%#Eval("AC2_Water_Side_RH_From_Last_Cleaning")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label23" runat="server" Text='<%#Eval("AC3_Water_Side_RH_From_Last_Cleaning")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left" class="eform-header-row">
                                                        Pressure Drop In mm WC
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label28" runat="server" Text='<%#Eval("AC1_Pressure_Drop_In_mm_WC")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label29" runat="server" Text='<%#Eval("AC2_Pressure_Drop_In_mm_WC")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 150px;text-align:center" class="eform-field-data">
                                                        <asp:Label ID="Label30" runat="server" Text='<%#Eval("AC3_Pressure_Drop_In_mm_WC")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%; text-align: right; vertical-align: top;">
                                            <table style="width: 100%;"  cellpadding="1" border="1">                                                
                                                <tr class="eform-header-row">                                                    
                                                    <td style="width:150px">
                                                        
                                                    </td>
                                                    <td>
                                                        ME T/C WATER WASH  GAS  SIDE
                                                    </td>
                                                    <td>
                                                        ME GOVERNOR LO CHANGE
                                                    </td>
                                                    
                                                </tr>
                                                <tr>                                                    
                                                    <td class="eform-header-row">
                                                        Last Done Date
                                                    </td>
                                                    <td class="eform-field-data" style="text-align:center">
                                                        <asp:Label ID="Label25" runat="server" Text='<%#Eval("ME_TC_WATER_WASH_GAS_SIDE_LAST_DONE","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                    <td class="eform-field-data" style="text-align:center">
                                                        <asp:Label ID="Label26" runat="server" Text='<%#Eval("ME_GOVERNOR_LO_CHANGE_LAST_DONE","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                    
                                                </tr>
              
                                            
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>

