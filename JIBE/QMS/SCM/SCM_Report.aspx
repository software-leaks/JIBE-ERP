<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    EnableEventValidation="false" CodeFile="SCM_Report.aspx.cs" Inherits="SCM_Report"
    Title="Safety Committee Report Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
       <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
        <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<style>
    .Page
    {
        height:100%;
    }
</style>
    <center>
        <div style="font-family: Tahoma; font-size: 12px; width: 80%; height: 120%">
          <div class="page-title">
                             Safety Committee Report Index 
                        
            </div>
            <div style="border: 1px solid gray;">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="2" width="100%" style="color: Black;">
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 5%" align="right">
                                    Fleet : &nbsp; &nbsp;
                                </td>
                                <td style="width: 5%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px" Height="20px"
                                        Font-Size="11px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%" align="right">
                                    From : &nbsp;
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:TextBox ID="txtFromDate" runat="server" EnableViewState="true" Font-Size="11px"
                                        Width="120px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="left" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:Button ID="btnRetrieve" runat="server" Font-Size="11px" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Retrieve" Width="80px" />
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:ImageButton ID="btnExport" runat="server" Height="22px" ImageUrl="~/Images/XLS.jpg"
                                        OnClick="btnExport_Click" ToolTip="Export to Excel" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    Vessel : &nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Width="120px" Height="20px" Font-Size="11px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    To : &nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtToDate" runat="server" EnableViewState="true" Font-Size="11px"
                                        Width="120px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnClearFilter" runat="server" Font-Size="11px" Height="20px" OnClick="btnClearFilter_Click"
                                        Text="Clear Filters" Width="80px" />
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="border: 1px solid gray; margin-top: 2px; ;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                      
                            <asp:GridView ID="gvSCMReport" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="gvSCMReport_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                OnSorting="gvSCMReport_Sorting" DataKeyNames="ID" OnRowCreated="gvSCMReport_RowCreated"  
                                SelectedIndex="0">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCMID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                            <asp:LinkButton ID="lbnVesselName" ForeColor="Black" runat="server" CommandArgument='<%#Eval("ID")  + ","+ DataBinder.Eval(Container,"DataItem.MEETING_DATE","{0:dd-MM-yyyy}")+ ","+ Eval("Vessel_ID")%>'
                                                CommandName="" Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Meeting Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblMeetingDateHeader" runat="server" ForeColor="Black" CommandArgument="MEETING_DATE"
                                                CommandName="Sort"> Meeting Date&nbsp;</asp:LinkButton>
                                            <img id="MEETING_DATE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbnMeetingDate" ForeColor="Black" CommandName="Select" runat="server"
                                                Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.MEETING_DATE","{0:dd-MM-yyyy}") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Present Position">
                                        <HeaderTemplate>
                                            Present Position
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPresentPosition" ForeColor="Black" CommandName="Select" runat="server"
                                                Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.PRESENT_POSITION") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="left" Width="450px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verified">
                                        <HeaderTemplate>
                                             Verified
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblIsVerified" ForeColor="Black" CommandName="Select" runat="server"
                                                Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.IsVerified") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindSCMReport" />
                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
