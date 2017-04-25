<%@ Page Title="Manning Office Fees" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="DisbUpdateFee.aspx.cs"
    Inherits="CrewDisbursement_DisbUpdateFee" %>

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
            font-size: 12px; /*border-left: 2px solid #3B0B0B;
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
            document.getElementById('dvAddNewDisb').title = "Add New Fee"
            showModal("dvAddNewDisb", true, dvAddNewDisb_onClose);
        }

        function dvAddNewDisb_onClose() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="page-title">
                Manning Office Fees
                  <div style="float: right">
                 <asp:ImageButton ID="ImageButton2" src="../Images/Excel-icon.png" Height="20px" ToolTip="Export To Excel" runat="server"
                OnClick="ImgExportToExcel_Click" AlternateText="Print" />
                </div>
      
            </div>
    <%--<div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold;">
        <div style="float: right">
            <asp:ImageButton ID="ImageButton1" src="../Images/Excel-icon.png" Height="20px" ToolTip="Export To Excel" runat="server"
                OnClick="ImgExportToExcel_Click" AlternateText="Print" /></div>
        
        <div>
            Manning Office Fees</div>
    </div>--%>
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 2px;">
        <div id="page-content">
            <div style="border: 1px solid #cccccc; padding: 2px;">
                <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="4" style="width: 100%;">
                            <tr>
                                <td>
                                    Manning Office
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManningOffice" runat="server" Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Rank Category
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRankCategory" runat="server" Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Approval Status
                                </td>
                                <td>
                                </td>
                                <td>
                                    Sign-On Date
                                </td>
                                <td>
                                    Approved Date
                                </td>
                                <td>
                                </td>
                                <td style="text-align: right" rowspan="2">
                                    <asp:Button ID="BtnSearch" runat="server" ToolTip="Search" OnClick="BtnSearch_Click" Text="Search"
                                        Width="80px" Height="40px" CssClass="btnCSS" />
                                </td>
                            </tr>
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
                                    Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="156px">
                                        <asp:ListItem Text="-SELECT ALL-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Current" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Signed-Off" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoApprovedStatus" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="0" CellSpacing="0">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    From:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSignOnFrom" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtSignOnFrom">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovedFrom" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtApprovedFrom">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Search Text
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Fee Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFeeType" runat="server" Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="55px" Height="20px" Font-Size="11px"
                                        BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0" Text="Month"></asp:ListItem>
                                        <asp:ListItem Value="01" Text="Jan"></asp:ListItem>
                                        <asp:ListItem Value="02" Text="Feb"></asp:ListItem>
                                        <asp:ListItem Value="03" Text="Mar"></asp:ListItem>
                                        <asp:ListItem Value="04" Text="Apr"></asp:ListItem>
                                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="06" Text="Jun"></asp:ListItem>
                                        <asp:ListItem Value="07" Text="Jul"></asp:ListItem>
                                        <asp:ListItem Value="08" Text="Aug"></asp:ListItem>
                                        <asp:ListItem Value="09" Text="Sep"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="50px" Height="20px" Font-Size="11px"
                                        BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0" Text="Year"></asp:ListItem>
                                        <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                                        <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                                        <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                        <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                                        <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                        <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                        <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                        <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                        <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                        <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                        <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                        <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    To:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSignOnTo" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSignOnTo">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovedTo" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtApprovedTo">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="BtnClearFilter" runat="server" ToolTip="Clear Filter" OnClick="BtnClearFilter_Click" Text="Clear Filter"
                                        Width="80px" CssClass="btnCSS" />
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
                            AllowSorting="false" AllowPaging="false" AutoGenerateColumns="False" ShowFooter="true"
                            Width="100%" CssClass="grd"  GridLines="None" DataKeyNames="ID"
                            OnRowDataBound="GridView_Crew_RowDataBound" OnRowEditing="GridView_Crew_RowEditing"
                            OnRowUpdating="GridView_Crew_RowUpdating" OnRowCancelingEdit="GridView_Crew_RowCancelEdit"
                            OnRowCommand="GridView_Crew_RowCommand" >
                             <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                             <RowStyle CssClass="RowStyle-css" />
                             <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <EditRowStyle VerticalAlign="Top" BackColor="#efefef" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Manning Office" SortExpression="MANNING_OFFICE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManning" runat="server" Text='<%# Eval("MANNING_OFFICE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="130px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vessel" SortExpression="Vessel_Short_Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S/Code" SortExpression="STAFF_CODE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                        <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Bind("CrewID")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Staff_FullName" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_FullName")%>'></asp:Label>
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
                                        <asp:Label ID="lblContract" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("joining_date"))) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S/On<br>Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSON" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("sign_on_date"))) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S/Off<br>Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSOff" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fee Type" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFeeType" runat="server" Text='<%# Eval("FeeTypeName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Total:
                                    </FooterTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Due_Date"))) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due Amt" HeaderStyle-HorizontalAlign="Center" ControlStyle-BackColor="Yellow">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDueAmt" runat="server" Text='<%# Eval("Due_Amount","{0:0.00}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    <FooterTemplate>
                                        <asp:Label ID="lblDueTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("Approved_YesNo").ToString()=="1"?Eval("Approved_Amount","{0:0.00}"):"NO"%>'
                                            Visible='<%# Convert.ToBoolean(Eval("Approved_YesNo")) %>' BackColor="Yellow"></asp:Label>
                                        <asp:HiddenField ID="hdnApprovedAmt" runat="server" Value='<%# Eval("Approved_Amount")%>' />
                                        <asp:RadioButtonList ID="rdoApproved" runat="server" Text='<%# Bind("Approved_YesNo")%>'
                                            AutoPostBack="false" Visible='<%# !Convert.ToBoolean(Eval("Approved_YesNo")) %>'
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RadioButtonList ID="rdoApproved" runat="server" Text='<%# Bind("Approved_YesNo")%>'
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblApprovedTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Approved">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateApproved" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Approved_Date"))) %>'
                                            Visible='<%# Convert.ToBoolean(Eval("Approved_YesNo")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDateApproved" runat="server" Width="70px" Text='<%#Bind("Approved_Date","{0:dd/MM/yyyy}")%>'></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateApproved"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PO Number">
                                    <ItemTemplate>
                                    
                                    <%# Eval("ORDER_CODE")%>
                                     </ItemTemplate>
                                     <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="200px" Height="20px"
                                            Text='<%# Bind("Remarks")%>' Visible='<%# !Convert.ToBoolean(Eval("Approved_YesNo")) %>'></asp:TextBox>

                                        <asp:Image ID="ImgRemarks" runat="server" ImageUrl="~/images/comment.png" CausesValidation="False"
                                            Height="16px" AlternateText='<%# Eval("Remarks")%>' CssClass="imgRemark" Visible='<%# Convert.ToBoolean(Eval("Approved_YesNo"))  %>'>
                                        </asp:Image>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="200px" Height="20px"
                                            Text='<%# Bind("Remarks")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Approve" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnApprove" runat="server" AlternateText="Add" CausesValidation="False"
                                           CommandName="Update" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/images/accept.png" 
                                           Visible='<%# Eval("ORDER_CODE").ToString() == "" ? true : false %>' Height="18px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnAddNew" ToolTip="Add New Fee" runat="server" AlternateText="Add" CausesValidation="False"
                                            CommandName="AddNew"  CommandArgument='<%# Eval("ID")%>' ImageUrl="~/images/Plus2.png"
                                            Height="18px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="PO" HeaderStyle-Width="30px" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnPO" runat="server"  ImageUrl="~/images/TRVPODetails.gif" Visible='<%# Eval("IsPOGenerated").ToString()=="1" ?true:false %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
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
                                        <uc1:ucCustomPager ID="ucCustomPager_CrewList" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff"
                                            OnBindDataItem="Load_CrewProcessingFee" />
                                    </td>
                                    <td style="font-weight: bold; font-size: 12px;">
                                        Grand Total Approved:&nbsp;&nbsp;<asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="dvAddNewDisb" title="Add New Fee" style="margin-top: 5px; border: 1px solid #cccccc;
                padding: 2px; width: 600px; font-size: 12px; display: none;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="margin: 10px" cellpadding="3">
                            <tr>
                                <td>
                                    Staff
                                </td>
                                <td style="font-weight: bold">
                                    <asp:Label ID="lblRank" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblStaffCode" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblStaffName" runat="server"></asp:Label>
                                    <asp:HiddenField ID="Vessel_ID" runat="server" />
                                    <asp:HiddenField ID="CrewID" runat="server" />
                                    <asp:HiddenField ID="VoyageID" runat="server" />
                                    <asp:HiddenField ID="ManningOfficeID" runat="server" />
                                    <asp:HiddenField ID="DueDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">
                                    Manning Office
                                </td>
                                <td>
                                    <asp:Label ID="lblManningOffice" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fee Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFeeType_AddNew" runat="server" Width="156px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="addnewfee"
                                        runat="server" ControlToValidate="ddlFeeType_AddNew" Display="Dynamic" InitialValue="0"
                                        ErrorMessage="Please select fee type"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Due Date
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth_AddNew" runat="server" Width="55px" Height="20px"
                                        Font-Size="11px" BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0" Text="Month"></asp:ListItem>
                                        <asp:ListItem Value="01" Text="Jan"></asp:ListItem>
                                        <asp:ListItem Value="02" Text="Feb"></asp:ListItem>
                                        <asp:ListItem Value="03" Text="Mar"></asp:ListItem>
                                        <asp:ListItem Value="04" Text="Apr"></asp:ListItem>
                                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="06" Text="Jun"></asp:ListItem>
                                        <asp:ListItem Value="07" Text="Jul"></asp:ListItem>
                                        <asp:ListItem Value="08" Text="Aug"></asp:ListItem>
                                        <asp:ListItem Value="09" Text="Sep"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlYear_AddNew" runat="server" Width="50px" Height="20px" Font-Size="11px"
                                        BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0" Text="Year"></asp:ListItem>
                                        <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                                        <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                                        <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                        <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                                        <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                        <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                        <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                        <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                        <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                        <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                        <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                        <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="addnewfee"
                                        runat="server" ControlToValidate="ddlMonth_AddNew" Display="Dynamic" InitialValue="0"
                                        ErrorMessage="Please select month"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="addnewfee"
                                        runat="server" ControlToValidate="ddlYear_AddNew" Display="Dynamic" InitialValue="0"
                                        ErrorMessage="Please select year"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAmount_AddNew" runat="server" Width="80px"></asp:TextBox>
                                    (Do not include [+/-] sign)
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="addnewfee"
                                        runat="server" ControlToValidate="txtAmount_AddNew" Display="Dynamic" ErrorMessage=" Enter Amount"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    Remarks
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="350px" TextMode="MultiLine" Height="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnSaveNew" runat="server" Text="Save" OnClick="btnSaveNew_Click"
                                        ValidationGroup="addnewfee" />
                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" OnClientClick="hideModal('dvAddNewDisb'); return false; " />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
