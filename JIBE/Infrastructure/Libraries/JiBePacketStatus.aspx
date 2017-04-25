<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JiBePacketStatus.aspx.cs" Inherits="Infrastructure_Libraries_JiBePacketStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
            height: 100%;">
            <div class="page-title">
                JiBe Packet Status
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px;">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td  align="right">
                                        Vessel :&nbsp;
                                    </td>
                                    <td  align="left">
                                        <asp:DropDownList ID="ddlVessel" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td  align="right">
                                        Status :&nbsp;
                                    </td>
                                    <td  align="left">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Enabled='false' Width="250px">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Missing Packet from Vessel</asp:ListItem>
                                            <asp:ListItem Value="2">Packet Not executed on Vessel</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" >
                                       From Date :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFromDate" CssClass="txtInput" runat="server" Width="150px"></asp:TextBox>
                                        <tlk4:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate">
                                        </tlk4:CalendarExtender>
                                        <asp:CompareValidator ID="dateValidatortxtFromDate" runat="server" Type="Date"
                                            Operator="DataTypeCheck" Display="None" InitialValue="" ControlToValidate="txtFromDate"
                                            ErrorMessage="Please enter a valid date." ValidationGroup="saveFZ">
                                        </asp:CompareValidator>
                                        <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtenderDVtxtFromDate" TargetControlID="dateValidatortxtFromDate"
                                            runat="server">
                                        </tlk4:ValidatorCalloutExtender>
                                    </td>
                                    <td align="right">
                                        To Date :
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtToDate" CssClass="txtInput" runat="server" Width="150px"></asp:TextBox>
                                        <tlk4:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server"
                                            TargetControlID="txtToDate">
                                        </tlk4:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidatorToDate" runat="server" Type="Date" Operator="DataTypeCheck"
                                            Display="None" InitialValue="" ControlToValidate="txtToDate" ErrorMessage="Please enter a valid date."
                                            ValidationGroup="saveFZ">
                                        </asp:CompareValidator>
                                        <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtenderToDate" TargetControlID="CompareValidatorToDate"
                                            runat="server">
                                        </tlk4:ValidatorCalloutExtender>
                                    </td>
                                    <td align="center" >
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search" ValidationGroup="saveFZ"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td >
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvPktStat" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvPktStat_RowDataBound" CellPadding="1" CellSpacing="0" Width="100%"
                                    GridLines="both" OnSorting="gvPktStat_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVesselHeader" runat="server" CommandName="Sort" CommandArgument="Vessel_Name"
                                                    ForeColor="Black">Vessel&nbsp;</asp:LinkButton>
                                                <img id="Vessel" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packet Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblPacketNameHeader" runat="server" CommandName="Sort" CommandArgument="Packet_Name"
                                                    ForeColor="Black">Packet Name&nbsp;</asp:LinkButton>
                                                <img id="PacketName" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacketName" runat="server" Text='<%#Eval("Packet_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSendStatus" runat="server" Text='<%#Eval("Send_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Execute Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExecuteStatus" runat="server" Text='<%# Eval("Execute_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Date">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSendDateHeader" runat="server" CommandName="Sort" CommandArgument="Send_Date"
                                                    ForeColor="Black">Send Date&nbsp;</asp:LinkButton>
                                                <img id="SendDate" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSendDate" runat="server" Text='<%#Eval("Send_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="center" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Execution">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblExecutionDateHeader" runat="server" CommandName="Sort" CommandArgument="Execute_Date"
                                                    ForeColor="Black">Date of Execution&nbsp;</asp:LinkButton>
                                                <img id="ExecuteDate" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblExecuteDate" runat="server" Text='<%# Eval("Execute_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="center" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindJiBePacketGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
