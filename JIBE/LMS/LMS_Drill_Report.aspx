<%@ Page Title="Drill Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="LMS_Drill_Report.aspx.cs" Inherits="LMS_LMS_Drill_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
 
    <style type="text/css">
        .AnomalyCell
        {
            background-color: #FA5858;
            color: White;
        }
        .NoAnomaly
        {
            background-color: #31bc1a;
            color: Black;
        }
    </style>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
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
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>Drill Report</b>
                </div>
                <div style="color: Black;">
                    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                        <ContentTemplate>
                            <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="1" cellspacing="0">
                                    <tr>
                                        <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td width="20%" align="right" valign="middle">
                                                                Fleet :
                                                            </td>
                                                            <td width="25%" valign="top" align="left">
                                                                <asp:DropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" AutoPostBack="true"
                                                                     Width="160" />
                                                            </td>
                                                              <td width="20%" align="right" valign="middle">
                                                                Vessel :
                                                            </td>
                                                            <td width="25%" valign="top" align="left">
                                                                <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged" AutoPostBack="true"  
                                                                      Width="160" />
                                                            </td>
                                                         <%--   <td width="20%" align="right" valign="top">
                                                                Date From :
                                                            </td>--%>
                                                 <%--           <td width="25%" valign="top" align="left">
                                                                <asp:TextBox ID="txtFromDate" CssClass="input" runat="server" Width="120px" 
                                                                    ontextchanged="txtFromDate_TextChanged"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </td>--%>
                                                            <td width="10%">
                                                                &nbsp;&nbsp;
                                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel"  
                                                                    Width="16px" ImageUrl="~/Images/Excel-icon.png" 
                                                                    onclick="ImgExpExcel_Click" />
                                                                &nbsp;&nbsp;
                                                                <img src="../Images/Printer.png" style="cursor: hand;" alt="Print" onclick="PrintDiv('divGrid')" />
                                                                &nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                          
                                          
                                                            <td>
                                                              <%--  <asp:Button ID="btnSearch" Text="Search" runat="server" Width="90px" ToolTip="Search"
                                                                    OnClick="btnSearch_Click" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divGrid" >
                                <asp:GridView ID="gvDrill" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="True"
                                    OnRowDataBound="gvERLogIndex_RowDataBound"   CellPadding="3"
                                    GridLines="None" CellSpacing="0" Width="100%"  
                                      Font-Size="12px" CssClass="GridView-css">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" HorizontalAlign="Center" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" HorizontalAlign="Center"/>
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Center"/>
                                      <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
<%--                                    <Columns>
                              
                                        <asp:TemplateField HeaderText="Vessel Name">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbtVesslNameHeader" runat="server">Vessel Name</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="130px"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEngineLogDateHeader" runat="server">Date</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEngineLogDate" runat="server" style="cursor:pointer" Text='<%# DataBinder.Eval(Container,"DataItem.LOG_DATE","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="125px"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                     
                                                                                   
                                         
                                      
                                    </Columns>--%>
                                </asp:GridView>
                            </div>
                            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindIndex" />
                                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                         <Triggers>
            <asp:PostBackTrigger ControlID="ImgExpExcel" />
        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </center>
    </div>
         
</asp:Content>
