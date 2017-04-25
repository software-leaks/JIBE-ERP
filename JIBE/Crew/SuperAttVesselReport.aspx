<%@ Page Title="Super Attending Vessels" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="SuperAttVesselReport.aspx.cs"
    Inherits="Crew_SuperAttVesselReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            padding: 10px;
            margin-bottom: 20;
            font-family: arial;
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
        .Promoted-Voyage-Row td
        {
            background-color: #F2FFaa;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
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
        
        #grid-container a:link
        {
            color: blue;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #grid-container a:visited
        {
            color: black;
            text-decoration: none;
        }
        #grid-container a:hover
        {
            color: blue;
            text-decoration: none;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../Images/bg.png) left -1672px repeat-x;
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
            padding: 0px 3px 0px 3px;
        }
        .taskpane
        {
            background-image: url(../images/taskpane.png);
            background-repeat: no-repeat;
            background-position: -2px -2px;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 90px;
            width: 180px;
            padding: 15px;
            color: #fff;
        }
        .interview-schedule-table
        {
            padding: 0;
            border-collapse: collapse;
        }
        .interview-schedule-table div
        {
            border: 0px solid gray;
            height: 16px;
            width: 16px;
            margin-top: 2px;
            background: url(../Images/Interview_1.png) no-repeat;
        }
        
        .CrewStatus_Current
        {
            background-color: #aabbdd;
        }
        .CrewStatus_SigningOff
        {
            background-color: #F3F781;
        }
        .CrewStatus_SignedOff
        {
            background-color: #F5A9A9;
        }
        .CrewStatus_Assigned
        {
            background-color: #BBB6FF;
        }
        .CrewStatus_Planned
        {
            background-color: #F781F3;
        }
        .CrewStatus_Pending
        {
            background-color: #81BEF7;
        }
        .CrewStatus_Inactive
        {
            background-color: #848484;
            color: #E6E6E6;
        }
        .CrewStatus_NoVoyage
        {
            background-color: #A9F5D0;
        }
        .CrewStatus_NTBR
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_NTBR_Row
        {
            color: Red;
        }
        .CrewStatus_UNFIT
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_UNFIT_Row
        {
            color: Red;
        }
        .imgCOC
        {
            vertical-align: middle;
        }
        .CrewStatus_Rejected
        {
            background-color: RED;
            color: Yellow;
        }
        input
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        textarea
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 11px;
        }
        .hide
        {
            display: none;
        }
        .show
        {
            display: block;
        }
        #tblPager .tdpager
        {
            border: 0px;
        }
        #tblPager .Paging-Custom:hover
        {
            background-color: #CECEF6;
        }
        .bgYellow
        {
            background-color: Yellow;
            border: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        function openPopup_AddNewDisb() {
            showModal("dvAddNewDisb", true, dvAddNewDisb_onClose);
        }

        function dvAddNewDisb_onClose() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <div class="page-title">
      Superintendents attending vessels
    <div style="float: right">
            <asp:ImageButton ID="ImageButton1" src="../Images/Excel-icon.png" Height="20px" runat="server"
                OnClick="ImgExportToExcel_Click" AlternateText="Print" /></div>

    </div>
 
     <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 2px;">
        <div id="page-content">
            <div style="border: 1px solid #cccccc; padding: 2px;">
                <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="4" style="width: 100%;">
                            <tr>
                                <td>
                                    Fleet
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Sign-On From:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSignOnFrom" runat="server" AutoPostBack="true" OnTextChanged="BtnSearch_Click"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtSignOnFrom">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Search Text
                                </td>
                                <td >
                                    <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" OnTextChanged="BtnSearch_Click"></asp:TextBox>
                                </td>
                                 <td style="text-align: right">
                                    <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" Text="Clear Filter"
                                        Width="80px" CssClass="btnCSS" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" AutoPostBack="true" OnSelectedIndexChanged="BtnSearch_Click">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                   Sign-On To:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSignOnTo" runat="server" AutoPostBack="true" OnTextChanged="BtnSearch_Click"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSignOnTo">
                                    </ajaxToolkit:CalendarExtender>
                                </td>   
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="margin-top: 5px; border: 1px solid #cccccc; padding: 2px;">
                <asp:UpdatePanel ID="UpdatePanel_Crew" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView_Crew" runat="server" PageSize="15" CellPadding="1" EmptyDataText="No record found!"
                            AllowSorting="false" AllowPaging="false" AutoGenerateColumns="False" ShowFooter="false"
                            Width="100%"  ForeColor="#333333" GridLines="None" DataKeyNames="ID"
                            OnRowDataBound="GridView_Crew_RowDataBound">
                            <FooterStyle BackColor="#FFF8C6" ForeColor="#333333" Font-Bold="true" />
                            <EditRowStyle VerticalAlign="Top" BackColor="#efefef" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Vessel" SortExpression="Vessel_Short_Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S/Code" SortExpression="STAFF_CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                        <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Bind("CrewID")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Staff_Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContract" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("joining_date"))) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S/On Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSON" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("sign_on_date"))) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S/Off Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSOff" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days ONBD" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDaysONBD" runat="server" Text='<%# Eval("DaysOnBoard")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                            </Columns>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"/>
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <div style="margin: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                            background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                            background-color: #F6CEE3;">
                            
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <uc1:ucCustomPager ID="ucCustomPager_CrewList" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Records"
                                            OnBindDataItem="Load_Report" />
                                    </td>
                                    <td style="font-weight: bold; font-size: 12px;text-align:right;">
                                        Total Days:&nbsp;&nbsp;<asp:Label ID="lblGrandTotal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>            
        </div>
    </div>
</asp:Content>
