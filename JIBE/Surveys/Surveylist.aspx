<%@ Page Title="Surveylist" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  ValidateRequest="false" 
    CodeFile="Surveylist.aspx.cs" Inherits="Surveylist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/messages.js" type="text/javascript"></script>
    <link href="../Styles/messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        input
        {
            font-family: Tahoma;
            font-size: 11px;
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
        .Legend
        {
            color: Black;
            font-weight: bold;
            text-align: left;
            border: 1px solid #cccccc;
            padding-bottom: 2px;
        }
        .Overdue-Reminder
        {
            background-color: #F781F3;
            color: Black;
        }
        .Overdue
        {
            background-color: Red;
            color: Yellow;
            border-left:1px solid #fff;
        }
        .Due0-30
        {
            background-color: Orange;
            color: Black;
            border-left:1px solid #fff;
        }
        .Due30-90
        {
            background-color: Yellow;
            color: Black;
            border-left:1px solid #fff;
        }
        .Done30
        {
            background-color: Green;
            color: White;
            border-left:1px solid #fff;
        }
        .NAExpiry
        {
            color: Blue;
            font-weight: bold;
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
        #tblPager .tdpager
        {
            border: 0px;
        }
        #tblPager .Paging-Custom:hover
        {
            background-color: #CECEF6;
        }
    </style>
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
    </style>
    <script type="text/javascript">
        function showDialog(url) {
            window.open(url);
        }
        function showEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'block';
        }
        function hideEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'none';
        }
        $(document).ready(function () {
            $("#ctl00_MainContent_btnSearch").click();

            $("body").on("click", ".ImgAddNew", function () {
                var res;
                var vesselid = $(this).attr("vid");
                var surveyvesselid = $(this).attr("s_v_id");
                var options = {
                    url: 'Surveylist.aspx?Method=CheckPreviousCertificate&Vessel_ID=' + vesselid + '&Surv_Vessel_ID=' + surveyvesselid,
                    dataType: 'html',
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        res = parseInt(response);
                    }
                }
                $.ajax(options);
                if (res == -1) {
                    if (window.confirm('There is an pending inspection for the renewal of certificate. Are you sure you want to proceed?')) {
                        window.open('SurveyDetails.aspx?vid=' + vesselid + '&s_v_id=' + surveyvesselid + '&s_d_id=0', '_blank');
                    }
                }
                else {
                    window.open('SurveyDetails.aspx?vid=' + vesselid + '&s_v_id=' + surveyvesselid + '&s_d_id=0', '_blank');
                }
            });
        });

        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 33%">
                </td>
                <td style="text-align: center">
                    Survey List
                </td>
                <td style="width: 33%; text-align: right">
                    <asp:ImageButton ToolTip="Export To Excel" ID="ImageButton2" src="../Images/xls.jpg" Height="20px" runat="server"
                        AlternateText="Print" OnClick="ImgExportToExcel_Click" />
               </td>
            </tr>
        </table>
    
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="page-content" style="overflow: auto;">
                <asp:UpdatePanel ID="UpdatePanelFilter" runat="server">
                    <ContentTemplate>
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
                        <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnSearch">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                                <tr>
                                    <td valign="top" style="border: 1px solid #aabbdd;">
                                        <table border="0" cellpadding="0" cellspacing="1">
                                            <tr>
                                                <td style="text-align: right;">
                                                    Fleet:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlFleetList" runat="server" Width="135px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlFleetList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    Vessels:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlVessels" runat="server" Width="135px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlVessels_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="2">
                                                    <asp:RadioButtonList ID="rdoVerified" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Not Verified" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Verified" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right;">
                                                    <asp:Button ID="btnShowNASurveyList" runat="server" Text="Show N/A Survey Certificates"
                                                        OnClick="btnShowNASurveyList_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Search:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSeachText" runat="server" Width="140px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="border: 1px solid #aabbdd;">
                                        <table border="0" cellpadding="0" cellspacing="1">
                                            <tr>
                                                <td style="text-align: right;">
                                                    Main Category:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlMainCategory" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    Sub Category:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:ListBox ID="ddlCategory" runat="server" Width="300px" AutoPostBack="True" SelectionMode="Multiple" Rows="7"
                                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    Survey:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlSurvey" runat="server" Width="300px" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                    <td valign="top" style="border: 1px solid #aabbdd;" width="130px">
                                        <asp:Panel ID="pnlIssueDate" runat="server">
                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr style="background-color: #aabbdd">
                                                    <td colspan="2" style="text-align: left;">
                                                        <asp:Label ID="Label2" runat="server" Text="Issue Date" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        From:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtIssueFromDate" CssClass="" runat="server" Width="80px" ></asp:TextBox>
                                                        <cc1:CalendarExtender ID="calIssueFromDate" runat="server" Enabled="True" TargetControlID="txtIssueFromDate"
                                                            Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        To:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtIssueToDate" CssClass="" runat="server" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="calIssueToDate" runat="server" Enabled="True" TargetControlID="txtIssueToDate"
                                                            Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td valign="top" style="border: 1px solid #aabbdd;" width="130px">
                                        <asp:Panel ID="pnlExpIn" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr style="background-color: #aabbdd">
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="Label4" runat="server" Text="Expiring In" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <asp:RadioButtonList ID="rdoExpiringIn" runat="server" CellPadding="0" CellSpacing="0"
                                                            OnSelectedIndexChanged="rdoExpiringIn_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Overdue Jobs" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="30 Days" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="60 Days" Value="60" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="90 Days" Value="90"></asp:ListItem>
                                                            <asp:ListItem Text="Custom Dates" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Show All" Value="-2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td valign="top" style="border: 1px solid #aabbdd;" width="130px">
                                        <div id="dvExpiryDates">
                                            <asp:Panel ID="pnlExpDate" runat="server">
                                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr style="background-color: #aabbdd">
                                                        <td colspan="2" style="text-align: left;">
                                                            <asp:Label ID="Label3" runat="server" Text="Expiry Date" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            From:
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:TextBox ID="txtExpFromDate" CssClass="" runat="server" Width="80px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calExpFromDate" runat="server" Enabled="True" TargetControlID="txtExpFromDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            To:
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:TextBox ID="txtExpToDate" CssClass="" runat="server" Width="80px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calExpToDate" runat="server" Enabled="True" TargetControlID="txtExpToDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td valign="top" style="border: 1px solid #aabbdd; text-align: center;">
                                        <asp:Button ID="btnSearch" Text="Search" ToolTip="Search" runat="server"  Width="70px"
                                            OnClick="btnSearch_Click" /><br />
                                        <asp:Button ID="btnClearSearch" Text="Clear Filter" ToolTip="Clear Filter" runat="server" Width="70px" OnClick="btnClearSearch_Click" />
                                    </td>
                                    <td valign="top" style="border: 1px solid #aabbdd; text-align: center;">
                                        <div style="padding: 2px;">
                                            <div style="padding: 2px;" class="Legend">
                                                Legend:</div>
                                            <div style="padding: 2px;" class="Overdue">
                                                Overdue</div>
                                            <div style="padding: 2px;" class="Due0-30">
                                                Due in next 30 days</div>
                                            <div style="padding: 2px;" class="Due30-90">
                                                Due in 30~90 days</div>
                                            <div style="padding: 2px;" class="Done30">
                                                Done within last 30 days</div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="100%">
                            <div style="width: 100%; overflow: auto; text-align: left;">
                                <div id="grid-container" style="padding: 1px;">
                                    <asp:GridView ID="grdSurveylist" runat="server" BackColor="White" BorderColor="#aabbdd"
                                        Font-Names="Tahoma" Font-Size="12px" AutoGenerateColumns="false" BorderStyle="Solid"
                                        BorderWidth="1px" CellPadding="1" EnableModelValidation="True" AllowSorting="false"
                                        Width="100%" GridLines="None" OnRowDataBound="grdSurveylist_RowDataBound" OnRowCommand="grdSurveylist_RowCommand"
                                        OnSorting="grdSurveylist_Sorting" AllowPaging="false" PageSize="20" 
                                        OnPageIndexChanging="grdSurveylist_PageIndexChanging" CssClass="gridmain-css">
                                          <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                          <RowStyle CssClass="RowStyle-css" />
                                          <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />

                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Vessel" SortExpression="Vessel_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="120px" HeaderText="Main Category" SortExpression="Survey_Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurvey_MainCategory" runat="server" Text='<%#Eval("Survey_MainCategory") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="200px" HeaderText="Sub Category" SortExpression="Survey_Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurvey_Category" runat="server" Text='<%#Eval("Survey_Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey/Certificate Name" SortExpression="Survey_Cert_Name"
                                                ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurvey_Cert_Name" runat="server" Text='<%#Eval("Survey_Cert_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Remarks" SortExpression="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Survey_Cert_remarks") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-Width="150px" HeaderText="Make/Model" SortExpression="EquipmentType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEquipmentType" runat="server" Text='<%#Eval("EquipmentType") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Issue Date" SortExpression="DateOfIssue">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateOfIssue" runat="server"  Text='<%# Eval("DateOfIssue","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfIssue"))) %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Expiry Date" SortExpression="DateOfExpiry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateOfExpiry" runat="server" Text='<%# Eval("DateOfExpiry","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfExpiry"))) %>'> </asp:Label>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Range(Months)" SortExpression="DateOfExpiry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGraceDateRange" runat="server" Text='<%# Eval("GraceRange")  %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Extension Date" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExtensionDate" runat="server" Text='<%# Eval("ExtensionDate","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("ExtensionDate"))) %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Calculated Expiry Date" SortExpression="DateOfExpiry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCalculatedExpiryDate" runat="server"  Text='<%# Eval("CALCULATED_DATEOFEXPIRY","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("CALCULATED_DATEOFEXPIRY"))) %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px"  HeaderText="Reminder" SortExpression="FollowupReminderDt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFollowupReminderDt" runat="server"   Text='<%# Eval("FollowupReminderDt","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("FollowupReminderDt"))) %>'  ></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgAttachment" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attachment" ToolTip="Attachment"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgVerified" runat="server" ImageUrl="~/Images/checked.gif" AlternateText="Verified" ToolTip="Verified" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="60px" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgAddNew" runat="server" ImageUrl="~/Images/addnew.png" AlternateText="Add New" vid='<%#Eval("Vessel_ID") %>' s_v_id='<%#Eval("Surv_Vessel_ID") %>' s_d_id='0'  ClientIDMode="Static" CssClass="ImgAddNew" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <label id="Label1" runat="server">
                                                No survey found !!</label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                        <PagerStyle Font-Size="16px" CssClass="pager" />
                                    </asp:GridView>
                                </div>
                                <div style="margin: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                    background-color: #F6CEE3;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <uc1:ucCustomPager ID="ucCustomPager" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Survey"
                                                    OnBindDataItem="Filter_Grid" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSearch.ClientID%>", function () {
                var Msg = "";
                if ($.trim($("#<%=txtIssueFromDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtIssueFromDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        Msg = "Enter valid Issue From Date<%=UDFLib.DateFormatMessage()%>\n";
                    }
                }
                if ($.trim($("#<%=txtIssueToDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtIssueToDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        Msg += "Enter valid Issue To Date<%=UDFLib.DateFormatMessage()%>\n";
                    }
                }
                if ($.trim($("#<%=txtExpFromDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtExpFromDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        Msg += "Enter valid Expiry From Date<%=UDFLib.DateFormatMessage()%>\n";
                    }
                }
                if ($.trim($("#<%=txtExpToDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtExpToDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        Msg += "Enter valid Expiry To Date<%=UDFLib.DateFormatMessage()%>\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });
        });
    </script>
</asp:Content>
