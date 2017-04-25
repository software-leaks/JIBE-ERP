<%@ Page Title="Event Planner" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="EventPlanner.aspx.cs" Inherits="Crew_EventPlanner" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <style type="text/css">
        .ListBox_CSS
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        
        .linkbutton
        {
            font-family: Tahoma;
            cursor: pointer;
            padding: 5px;
            font-size: 12px;
        }
        .bgEventBlue
        {
            background-color: #CED8F6;
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
        
        #tblPager .Paging-Custom
        {
            text-align: center;
            margin: 1px;
            padding: 1px 3px 1px 3px;
            text-decoration: none;
            font-size: 11px;
            position: relative;
            font-family: Verdana;
        }
        
        .SEQ_Red
        {
            color: Red;
            font-weight: bold;
        }
        .SEQ_Green
        {
            color: Green;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        #dvVesselMovements
        {
            overflow: auto;
            width: 1100px;
            text-align: left;
        }
        #dvVesselName
        {
            font-weight: bold;
        }
        .vessel-movement-table
        {
            text-align: center;
        }
        .vessel-movement-table td
        {
            border: 1px solid gray;
            width: 150px;
            background-color: #fff;
        }
        .vessel-movement-table div
        {
            width: 140px;
        }
        .vessel-movement-table .port-name
        {
            background-color: #336666;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }
        #dvLatestNoonReport
        {
            text-align: left;
            padding: 2px;
        }
        .noon-report-table
        {
            width: 100%;
            color: Black;
        }
    </style>
    <style type="text/css">
        .popup-background
        {
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }
        .popup-content
        {
            position: absolute;
            float: left;
            z-index: 1;
            padding: 10px;
        }
        #dialog
        {
            font-family: Verdana;
            font-size: 10px;
        }
        .DisableRow
        {
            background-color: Yellow;
        }
    </style>
  
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_ProgressBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server" DisplayAfter="2">
                <ProgressTemplate>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:HiddenField ID="hdnPortCallID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="page-title" class="page-title">
        <table style="width: 100%">
            <tr>
                <td style="width: 33%">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Event Planner"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                    <div style="float: right">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvpage-content" class="page-content-main">
        <asp:UpdatePanel ID="UpdatePanelAssignments" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlAssignments" runat="server">
                    <table style="color: Black; font-weight: bold; padding: 5px;">
                        <tr>
                            <td>
                                Fleet
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="padding-left: 20px">
                                Vessel
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnReload" runat="server" Text="Refresh" OnClick="btnReload_Click" />
                            </td>
                            <td>
                                <asp:Label ID="lblSEQ" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; border: 1px solid gray;">
                        <tr>
                            <td width="50%" style="text-align: left; color: Black; font-weight: bold; font-size: 12px;">
                                Crew OnBoard, FINISHING CONTRACT
                            </td>
                            <td width="50%" style="text-align: left; color: Black; font-weight: bold; font-size: 12px;">
                                Crew Presently Ashore - ASSIGNED
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="grid-container">
                                <asp:GridView ID="gvCrewChangeEvent" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                    Font-Size="11px" GridLines="Horizontal" Width="100%" DataKeyNames="PKID" AllowSorting="True"
                                    OnRowDataBound="gvCrewChangeEvent_RowDataBound" OnRowDeleting="gvCrewChangeEvent_RowDeleting"
                                    CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'
                                                    class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblStaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                                    CssClass="staffInfo" Target="_blank" Text='<%# Eval("staff_Code")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Blue" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstaff_name" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nationality" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNationalityOFF" runat="server" Text='<%# Eval("Nationality_SOFF")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EOC Date" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSignOffDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect_OFF" runat="server"></asp:CheckBox>
                                                <img id="imgEvent" class="event-link" src='<%#(Eval("Event_Status_OFF").ToString()=="1")?"../Images/E_OFF_OPEN.png":"../Images/E_OFF_CLOSED.png"%>'
                                                    onclick='<%# "showEvent("+Eval("EventID_OFF").ToString()+"," + Eval("CrewID").ToString()+ ")" %>'
                                                    alt="Event" style='<%# Eval("EventID_OFF").ToString()=="0"? "display:none": "display:block" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <img src="../Images/transp.gif" alt="" style="cursor: pointer" width="10px" />
                                            </ItemTemplate>
                                            <ItemStyle BackColor="white" Width="1px" />
                                            <HeaderStyle BackColor="White" Width="1px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manning Office" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany_Name" runat="server" Text='<%# Eval("Company_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblstaff_Code_ua" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID_UA")%>'
                                                    CssClass="staffInfo" Target="_blank" Text='<%# Eval("staff_Code_ua")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle ForeColor="Blue" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank_ua" runat="server" Text='<%# Eval("Rank_ua")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstaff_name_ua" runat="server" Text='<%# Eval("staff_name_ua")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nationality" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNationalityUA" runat="server" Text='<%# Eval("Nationality_UA")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Readiness" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Convert.ToString(Eval("available_from_date")) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("available_from_date")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnPKID" runat="server" Value='<%# Eval("PKID")%>' />
                                                <asp:HiddenField ID="hdnCrewID_ON" runat="server" Value='<%# Eval("CrewID_UA")%>' />
                                                <asp:HiddenField ID="hdnCrewID_OFF" runat="server" Value='<%# Eval("CrewID")%>' />
                                                <asp:CheckBox ID="chkSelect_ON" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_ON_CheckedChanged">
                                                </asp:CheckBox>
                                                <img id="imgEvent" class="event-link" src='<%#(Eval("Event_Status_ON").ToString()=="1")?"../Images/E_ON_OPEN.png":"../Images/E_ON_CLOSED.png"%>'
                                                    onclick='<%# "showEvent("+Eval("EventID_ON").ToString()+"," + Eval("CrewID_UA").ToString()+ ")" %>'
                                                    alt="Event" style='<%# Eval("EventID_ON").ToString()=="0"? "display:none": "display:block" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CausesValidation="False"
                                                    CommandName="Delete" OnClientClick="return confirm('This will DELETE the vessel assignment record. Do you want to proceed?')"
                                                    AlternateText="Delete"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <PagerStyle BackColor="#336666" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelAdditionalCrew" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlAdditionalCrew" runat="server" Visible="false">
                    <div id="dvAdditionalCrew" style="background-color: #BDBDBD; border: 1px solid #585858;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%; vertical-align: top">
                                    <table style="border: 1px solid #aabbee; background-color: White; width: 100%;">
                                        <tr style="background-color: #aabbee;">
                                            <td style="text-align: center; color: Black; font-weight: bold; font-size: 13px;">
                                                Additional Off-Signers
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; background-color: #fff; vertical-align: top;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            Search:
                                                            <asp:TextBox ID="txtSearchCrew_OffSigners" runat="server" AutoPostBack="true" OnTextChanged="txtSearchCrew_OffSigners_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <div class="grid-container">
                                                                <asp:GridView ID="gvCrewList_OffSigner" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="2" AllowPaging="false" PageSize="15" GridLines="Horizontal" Width="100%"
                                                                    DataKeyNames="ID" AllowSorting="true" Font-Size="11px" CssClass="gridmain-css">
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                                                                                <asp:HiddenField ID="hdnStaffCode" runat="server" Value='<%# Eval("STAFF_CODE")%>' />
                                                                                <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                    <%# Eval("STAFF_CODE")%></a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnRankID" runat="server" Value='<%# Eval("RANK_ID")%>' />
                                                                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSTAFFNAME" runat="server" Text='<%# Eval("staff_fullname")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EOC" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <%# Convert.ToString(Eval("COC")) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("COC")))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="False">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoRec" runat="server" Text="No crew found for the search"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                    <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                                        CssClass="pager" />
                                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
                                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                                                </asp:GridView>
                                                            </div>
                                                            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                                                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                                                background-color: #F6CEE3;">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <uc1:ucCustomPager ID="ucCustomPager_OffSigners" runat="server" RecordCountCaption="&nbsp;&nbsp;Staffs"
                                                                                OnBindDataItem="Load_CrewList_OffSigners" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top">
                                    <table style="border: 1px solid #CECEF6; background-color: White; width: 100%;">
                                        <tr style="background-color: #aabbee;">
                                            <td style="text-align: center; color: Black; font-weight: bold; font-size: 13px;">
                                                Additional On-Signers
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; background-color: #fff; vertical-align: top;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Search:<asp:TextBox ID="txtSearchCrew_OnSigners" runat="server" AutoPostBack="true"
                                                                            Width="150px" OnTextChanged="txtSearchCrew_OnSigners_TextChanged"></asp:TextBox>
                                                                        OnBd Vessel:<asp:DropDownList ID="ddlVesselName" runat="server" DataTextField="VESSEL_SHORT_NAME"
                                                                            DataValueField="Vessel_ID" Width="110px" OnSelectedIndexChanged="ddlVesselName_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        Vessel Type:
                                                                    </td>
                                                                    <td>
                                                                        <ucDDL:ucCustomDropDownList ID="ddlVesselType" runat="server" UseInHeader="False"  OnApplySearch="ddlVesselType_OK"
                                                                            Height="150" Width="140" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <div class="grid-container">
                                                                <asp:GridView ID="gvCrewList_OnSigner" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="2" AllowPaging="false" PageSize="15" GridLines="Horizontal" Width="100%"
                                                                    DataKeyNames="ID" AllowSorting="false" Font-Size="11px" CssClass="gridmain-css"
                                                                    OnRowDataBound="gvCrewList_OnSigner_RowDataBound">
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("vessel_short_name")%>' class='vesselinfo'
                                                                                    vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="60px" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                                                                                <asp:HiddenField ID="hdnStaffCode" runat="server" Value='<%# Eval("STAFF_CODE")%>' />
                                                                                <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                    <%# Eval("STAFF_CODE")%></a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnRankID" runat="server" Value='<%# Eval("RANK_ID")%>' />
                                                                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSTAFFNAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Vessel Types" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVesselType" runat="server" Text='<%# Eval("VesselType")%>' onmouseover='<%# "funVesselTooltip( "+ Eval("ID") + ",event,this);" %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="60px" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EOC" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <%# Convert.ToString(Eval("COC")) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("COC")))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Readiness" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <%#  Convert.ToString(Eval("Available_From_Date")) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Available_From_Date")))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="False">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                                                                <asp:ImageButton ID="ImgInvalid" runat="server" ImageUrl="~/images/exclamation.png"
                                                                                    CausesValidation="False" AlternateText="Invalid" Visible="false" Height="16px">
                                                                                </asp:ImageButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="lblNoRec" runat="server" Text="No crew found for the search"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                                    <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                                        CssClass="pager" />
                                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
                                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                                                </asp:GridView>
                                                            </div>
                                                            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                                                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                                                background-color: #F6CEE3;">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <uc1:ucCustomPager ID="ucCustomPager_OnSigners" runat="server" RecordCountCaption="&nbsp;&nbsp;Staffs"
                                                                                OnBindDataItem="Load_CrewList_OnSigners" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnOK" Text="OK" runat="server" OnClick="btnOK_Click" />
                                    <asp:Button ID="btnCancel" Text="Close" runat="server" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvViewSelectedCrew"  title="Selected Staffs" style="width: 1150px;">
            <asp:UpdatePanel ID="UpdatePanel_AdditionalSelected" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlViewSelectedCrew" title="Selected Staffs" runat="server" Visible="false">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div style="padding: 5px;min-height:250px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr class="gradiant-css-orange">
                                    <th style="text-align: left; padding: 2px; background-color: White; border: 1px solid gray;"
                                        class="gradiant-css-orange">
                                        Additional Off-Signers
                                    </th>
                                    <th>
                                        &nbsp;&nbsp;
                                    </th>
                                    <th style="text-align: left; padding: 2px; background-color: White; border: 1px solid gray;"
                                        class="gradiant-css-orange">
                                        Additional On-Signers
                                    </th>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; background-color: White; border: 1px solid gray;"
                                        class="grid-container">
                                        <asp:GridView ID="gvSelectedOffSigners" runat="server" AutoGenerateColumns="False"
                                            CellPadding="2" AllowPaging="false" PageSize="15" GridLines="Horizontal" Width="100%"
                                            OnRowDataBound="gvSelectedOffSigners_RowDataBound" DataKeyNames="ID" AllowSorting="true"
                                            Font-Size="11px" CssClass="gridmain-css">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                                                        <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                            <%# Eval("STAFF_CODE")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTAFFNAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Signing-Off Voyage" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlSOffVoyages" runat="server" DataTextField="voyage_name"
                                                            DataValueField="ID" AppendDataBoundItems="true" Width="160px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblNoRec" runat="server" Text="No staff selected"></asp:Label>
                                            </EmptyDataTemplate>
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                CssClass="pager" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#487575" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#275353" />
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="vertical-align: top; background-color: White; border: 1px solid gray;"
                                        class="grid-container">
                                        <asp:GridView ID="gvSelectedONSigners" runat="server" AutoGenerateColumns="False"
                                            CellPadding="2" AllowPaging="false" PageSize="15" GridLines="Horizontal" Width="100%"
                                            DataKeyNames="ID" AllowSorting="true" Font-Size="11px" CssClass="gridmain-css"
                                            OnRowDataBound="gvSelectedONSigners_RowDataBound">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                                                        <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                            <%# Eval("STAFF_CODE")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTAFFNAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Joining Rank" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlRank" runat="server" Text='<%# Eval("Rank_ID") %>' DataSourceID="objDS2"
                                                            DataTextField="Rank_Short_Name" DataValueField="ID" AppendDataBoundItems="true"
                                                            Width="100px" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:ObjectDataSource ID="objDS2" runat="server" TypeName="SMS.Business.Crew.BLL_Crew_Admin"
                                                            SelectMethod="Get_RankList"></asp:ObjectDataSource>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank Scale" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlRankScale" runat="server" Width="100px">
                                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Joining Date" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtJoinDate" runat="server" Width="80px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="caltxtJoinDate" runat="server" TargetControlID="txtJoinDate"
                                                            Format='<%# Convert.ToString(Session["User_DateFormat"]) %>'>
                                                        </ajaxToolkit:CalendarExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EOC Date" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCOCDate" runat="server" Width="80px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="caltxtCOCDate" runat="server" TargetControlID="txtCOCDate"
                                                            Format='<%# Convert.ToString(Session["User_DateFormat"]) %>'>
                                                        </ajaxToolkit:CalendarExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Joining Voyage" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlVoyages" runat="server" DataTextField="voyage_name" DataValueField="ID"
                                                            AppendDataBoundItems="true" Width="160px">
                                                            <asp:ListItem Value="0" Text="-Create New Voyage-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSendForApproval" runat="server" Text="Send for Approval"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblNoRec" runat="server" Text="No staff selected"></asp:Label>
                                            </EmptyDataTemplate>
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                CssClass="pager" />
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#487575" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#275353" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Button ID="btnSaveAdditional" runat="server" Text="Save" OnClick="btnSaveAdditional_Click" />
                                        <asp:Button ID="btnCancelAdditional" runat="server" Text="Cancel" OnClick="btnCancelAdditional_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel_PortCalls" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlPortCalls" runat="server" Visible="false">
                    <div id="dvVesselMovement">
                        <table style="width: 100%; margin-top: 20px; border: 1px solid #aabbee; background-color: #A9D0F5;">
                            <tr>
                                <td style="text-align: left; color: Black; font-weight: bold; font-size: 12px;">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                Select Port Call
                                            </td>
                                            <td>
                                            </td>
                                            <td style="text-align: right">
                                                <img src="../Images/up.png" onclick="toggleUpDown()" alt="" height="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; vertical-align: top;">
                                    <div id="dvContainer" style="background-color: White;">
                                        <table style="width: 100%; border: 1px solid gray; margin-top: 5px;">
                                            <tr>
                                                <td class="grid-container">
                                                    <asp:GridView ID="gvPortCalls" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        CellPadding="2" CssClass="gridmain-css" AllowPaging="true" PageSize="15" GridLines="Horizontal"
                                                        DataKeyNames="Port_Call_ID" Font-Size="11px" Width="100%" OnPageIndexChanging="gvPortCalls_PageIndexChanging">
                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                        <RowStyle CssClass="RowStyle-css" />
                                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Vessel">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'
                                                                        class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Port Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPort_Name" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnPortID" runat="server" Value='<%# Eval("Port_ID")%>'></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Arrival">
                                                                <ItemTemplate>
                                                                    <%#  Convert.ToString(Eval("Arrival")) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Departure" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <%#  Convert.ToString(Eval("Departure")) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Owners Agent" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOwners_Agent" runat="server" Text='<%# Eval("Owners_Agent")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Charterers Agent" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCharterers_Agent" runat="server" Text='<%# Eval("Charterers_Agent")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                                        CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                            CssClass="pager" />
                                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
                                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelEvents" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdnEventID" runat="server" />
                <asp:HiddenField ID="hdnEventType" runat="server" />
                <asp:Panel ID="pnlCrewChangeEvent" runat="server" Visible="false">
                    <table style="width: 100%; margin-top: 20px; border: 1px solid gray; font-size: 12px;">
                        <tr>
                            <td style="text-align: left; color: Black; font-weight: bold; font-size: 12px; height: 18px;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            Crew Change Event -
                                            <asp:Label ID="lblEventVessel" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <%--<asp:ImageButton runat="server" ID="ImgReloadEvent" ImageUrl="~/Images/reload.png" OnClick="ImgReloadEvent_Click" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; background-color: #dfdfdf; vertical-align: top; color: Black;
                                padding: 10px;">
                                <table border="0">
                                    <tr>
                                        <td style="padding-left: 60px">
                                            <%--Additional Personnel for the Event:--%>
                                        </td>
                                        <td>
                                            <%--<asp:Button ID="btnSelectAdditional" runat="server" Text="Select ..." OnClick="btnSelectAdditional_Click" />--%>
                                        </td>
                                        <td style="padding-left: 60px">
                                            Event Date:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEventDate" runat="server" Width="100px" ClientIDMode="Static"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtEventDate"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td style="padding-left: 20px">
                                        </td>
                                        <td>
                                            <asp:ObjectDataSource ID="objDSEventPort" runat="server" TypeName="SMS.Business.Infrastructure.BLL_Infra_Port"
                                                SelectMethod="Get_PortList_Mini"></asp:ObjectDataSource>
                                        </td>
                                        <td style="padding-left: 60px">
                                            <asp:Button ID="btnCreateEvent" runat="server" Text="Create Event" OnClick="btnCreateEvent_Click"
                                                ClientIDMode="Static" />
                                        </td>
                                        <td style="padding-left: 20px">
                                            <asp:Button ID="btnCancelEvent" runat="server" Text=" Cancel " OnClick="btnCancelEvent_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="1" cellpadding="1" style="border: 1px solid #610B5E; width: 100%;">
                                    <tr>
                                        <th colspan="7" style="background-color: #336666; color: White">
                                            Off-Signers
                                        </th>
                                        <th>
                                        </th>
                                        <th colspan="7" style="background-color: #336666; color: White">
                                            On-Signers
                                        </th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rpt1" OnItemCommand="rpt1_ItemCommand" OnItemDataBound="rpt1_ItemDataBound">
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: Black;">
                                                <td colspan="3" style="font-weight: bold;">
                                                    Port:
                                                    <asp:Label Text='<%#Eval("Port_Name")%>' ID="lblPortName" runat="server" /> 
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date:
                                                    <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Event_Date")))%>
                                                </td>
                                                <td colspan="7">
                                                    <table>
                                                        <tr>
                                                            <td style="vertical-align: top">
                                                                Projected ONBD Count =
                                                            </td>
                                                            <td style="vertical-align: top" class='<%# Eval("SEQ_Class")%>'>
                                                                <%# Eval("ONBD_Count")%>
                                                            </td>
                                                            <td style="vertical-align: top">
                                                                , SEQ =
                                                            </td>
                                                            <td style='font-weight: bold; vertical-align: top'>
                                                                <%# Eval("SEQ")%>
                                                            </td>
                                                            <td style="vertical-align: top">
                                                                <%# Eval("SEQ_Remarks")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# "&nbsp;&nbsp;Created By: " + Eval("CreatedBy")%>'></asp:Label>
                                                </td>
                                                <td colspan="3" style="text-align: right">
                                                    <asp:ImageButton ID="ImgFlightRequest" runat="server" ImageUrl="~/Travel/images/NewFlightRequest.png"
                                                        Visible='<%#objUA.Add == 0?false:true%>' CommandName="NewFlightRequest" CommandArgument='<%#  Eval("PKID").ToString()+"&Event_Date="+Eval("Event_Date", "{0:dd/MM/yyyy}").ToString()+"&Port_Name="+Eval("Port_Name").ToString()%>' />
                                                </td>
                                            </tr>
                                            <tr style="background-color: #E0E0F8; color: Black; border-bottom: 1px solid gray;">
                                                <td colspan="7" style="text-align: right">
                                                    <asp:LinkButton ID="btnNotifyMO" runat="server" Text="Notify M/O" CommandName="NotifyManningAgent"
                                                        Visible='<%#objUA.Add == 0?false:true%>' CssClass="linkbutton btnNotifyMO" CommandArgument='<%# Eval("PKID")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnNotifyMaster" runat="server" Text="Notify Master" CommandName="NotifyMaster"
                                                        Visible='<%#objUA.Add == 0?false:true%>' CssClass="linkbutton btnNotifyMaster" CommandArgument='<%#  Eval("PKID")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnNotifyFinalCrewChange" runat="server" Text="Final Crew Change"
                                                        Visible='<%#objUA.Add == 0?false:true%>' CommandName="FinalPlan" CssClass="linkbutton btnNotifyFinalCrewChange"
                                                        CommandArgument='<%# Eval("PKID")%>'>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="background-color: White">
                                                </td>
                                                <td colspan="7" style="text-align: right">
                                                    <asp:Image ID="imgEventRemark" runat="server" ImageUrl="~/Images/comment.png" Height="16px"
                                                        CssClass="remarks" />
                                                    <asp:LinkButton ID="btnAddAssignmentToEvent" runat="server" Text="[+] Add Selected Staff(s)"
                                                        Visible='<%#objUA.Add == 0?false:true%>' CssClass="linkbutton" CommandName="AddAssignmentToEvent"
                                                        CommandArgument='<%#  Eval("PKID")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnAddCrewToEvent" runat="server" Text="Add ADDITIONAL Staff(s)"
                                                        Visible='<%#objUA.Add == 0?false:true%>' CssClass="linkbutton" CommandName="AddCrewToEvent"
                                                        CommandArgument='<%#  Eval("PKID") + "," + Eval("Vessel_type")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditEvent" runat="server" Text="Edit Event" CommandName="EditEvent"
                                                        Visible='<%#objUA.Edit == 0?false:true%>' CssClass="linkbutton" CommandArgument='<%#  Eval("PKID")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnCloseEvent" runat="server" Text="Close" CommandName="CloseEvent"
                                                        Visible='<%#objUA.Approve == 0?false:true%>' CssClass="linkbutton" OnClientClick="return confirm('Are you sure, you want to close this event?')"
                                                        CommandArgument='<%#  Eval("PKID")%>'>
                                                    </asp:LinkButton>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CausesValidation="False"
                                                        Visible='<%#objUA.Delete == 0?false:true%>' ToolTip="Delete" CommandName="DeleteEvent"
                                                        CommandArgument='<%#  Eval("PKID")%>' OnClientClick="return confirm('This will DELETE the event and REMOVE all the members from event. Do you want to proceed?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #A9F5D0; color: Black; text-align: left;">
                                                <th style="width: 60px; text-align: left;">
                                                    S/Code
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                    Rank
                                                </th>
                                                <th style="width: 250px; text-align: left;">
                                                    Staff Name
                                                </th>
                                                <th style="width: 80px; text-align: left;">
                                                    EOC Date
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                    Nationality
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                </th>
                                                <th style="width: 20px">
                                                </th>
                                                <th style="background-color: white;">
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                    S/Code
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                    Rank
                                                </th>
                                                <th style="text-align: left;">
                                                    Staff Name
                                                </th>
                                                <th style="width: 80px; text-align: left;">
                                                    Readiness
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                    Nationality
                                                </th>
                                                <th style="width: 60px; text-align: left;">
                                                </th>
                                                <th style="width: 20px">
                                                </th>
                                            </tr>
                                            <asp:Repeater runat="server" ID="rpt2" OnItemCommand="rpt2_ItemCommand" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("EventMembers") %>'
                                                OnItemDataBound="rpt2_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr style="background-color: #E0F8F7; color: Black; border-bottom: 1px solid gray;">
                                                        <td>
                                                            <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["CrewID_Off"] %>'
                                                                target="_blank">
                                                                <%# ((System.Data.DataRow) Container.DataItem)["Staff_Code_Off"] %></a>
                                                        </td>
                                                        <td>
                                                            <%# ((System.Data.DataRow)Container.DataItem)["Rank_Off"]%>
                                                        </td>
                                                        <td class='<%#  (((System.Data.DataRow) Container.DataItem)["IOFF"]).ToString()== "0"?"DisableRow":"TEST" %>'>
                                                            <asp:Label ID="lblStaff_OFF" runat="server" Text='<%# ((System.Data.DataRow) Container.DataItem)["Staff_Name_Off"] %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%#  Convert.ToString(((System.Data.DataRow)Container.DataItem)["Est_Sing_Off_Date"]) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Est_Sing_Off_Date"]))%>
                                                        </td>
                                                        <td>
                                                            <%# ((System.Data.DataRow)Container.DataItem)["Nationality_OFF"]%>
                                                        </td>
                                                        <td>
                                                            <%# ((System.Data.DataRow)Container.DataItem)["AdditionalOFF"]%>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgBtnRemove_Off" runat="server" ImageUrl="~/Images/Clear.gif"
                                                                Visible="false" CommandName="RemoveCrewFromEvent" CommandArgument='<%# (((System.Data.DataRow) Container.DataItem)["PKID"]).ToString()+","+(((System.Data.DataRow) Container.DataItem)["CrewID_OFF"]).ToString() %>' />
                                                        </td>
                                                        <td style="background-color: white;">
                                                        </td>
                                                        <td>
                                                            <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["CrewID_ON"] %>'
                                                                target="_blank">
                                                                <%# ((System.Data.DataRow) Container.DataItem)["Staff_Code_ON"] %></a>
                                                        </td>
                                                        <td>
                                                            <%# ((System.Data.DataRow)Container.DataItem)["Rank_On"]%>
                                                        </td>
                                                        <td class='<%#  (((System.Data.DataRow) Container.DataItem)["ION"]).ToString()== "0"?"DisableRow":"TEST" %>'>
                                                            <asp:Label ID="lblStaff_ON" runat="server" Text='<%# ((System.Data.DataRow) Container.DataItem)["Staff_Name_ON"] %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%#  Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date"]) == "" ? "" : UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date"]))%>
                                                        </td>
                                                        <td>
                                                            <%# ((System.Data.DataRow)Container.DataItem)["Nationality_ON"]%>
                                                        </td>
                                                        <td>
                                                            <%# ((System.Data.DataRow)Container.DataItem)["AdditionalOn"]%>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgBtnRemove_On" runat="server" ImageUrl="~/Images/Clear.gif"
                                                                Visible="false" CommandName="RemoveCrewFromEvent" CommandArgument='<%# (((System.Data.DataRow) Container.DataItem)["PKID"]).ToString()+","+(((System.Data.DataRow) Container.DataItem)["CrewID_ON"]).ToString() %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="13">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dialog" title="Event Details">
        Loading Data ...
    </div>
    <asp:UpdatePanel ID="UpdatePanel_EditEvent" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="position: absolute; float: left; top: 30%; left: 30%;">
                <asp:Panel ID="pnlEditEvent" runat="server" Visible="false">
                    <div style="background-color: transparent; height: 300px; width: 450px;">
                        <div class="popup-content">
                            <asp:HiddenField ID="hdnEditEventID" runat="server" />
                            <table>
                                <tr>
                                    <td colspan="5" style="text-align: center; color: Black; font-weight: bold;">
                                        Edit Event
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Event Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditEventDate" runat="server" Width="100px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditEventDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="width: 20px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        Port:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEditEventPort" runat="server" DataSourceID="objDSEventPort"
                                            ForeColor="Black" Font-Size="12px" DataTextField="Port_Name" DataValueField="Port_ID"
                                            AppendDataBoundItems="true" Enabled="false">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align: center">
                                        <asp:TextBox ID="txtEventRemark" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="200px" MaxLength="1000"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="text-align: center">
                                        <asp:Button ID="btnSaveEventEdit" runat="server" Text="Save" OnClick="btnSaveEventEdit_Click" />
                                        <asp:Button ID="btnCloseEventEdit" runat="server" Text="Close" OnClick="btnCloseEventEdit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <img class="popup-background" src="../images/popupbg8.png" alt="">
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvNationalityApproval" title="Approval Details" style="width: 500px; font-size: 12px;
        display: none">
        <asp:UpdatePanel ID="UpdatePanel_NationalityApproval" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField ID="hdnAppVesselID" runat="server" />
                <asp:HiddenField ID="hdnAppCrewID" runat="server" />
                <asp:HiddenField ID="hdnAppJoiningRankID" runat="server" />
                <asp:HiddenField ID="hdnAppCurrentRankID" runat="server" />
                <asp:HiddenField ID="hdnAppEventID" runat="server" />
                <asp:HiddenField ID="hdnAppSOffCrewID" runat="server" />
                <table style="width: 100%;" cellpadding="5">
                    <tr>
                        <td colspan="2">
                            The ON-SIGNER can not join this vessel as there are already two or more staffs of
                            the same nationality has been joined the vessel
                            <br />
                            <br />
                            Please take approval if you still want him to join the vessel
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Vessel
                        </td>
                        <td>
                            <asp:Label ID="lblAppVessel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Joining Rank
                        </td>
                        <td>
                            <asp:Label ID="lblAppRank" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Request Details
                        </td>
                        <td>
                            <asp:TextBox ID="txtAppRequest" runat="server" TextMode="MultiLine" Width="200" Height="80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="btnNationalityApproval" Text="Send for Approval" runat="server" OnClick="btnNationalityApproval_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnVesselTypeCrew" runat="server" Value="" />
    <asp:HiddenField ID="hdnVesselTypeAssignedCrew" runat="server" Value="" />
    <div id="divVesselType" clientidmode="Static" runat="server" title="Confirmation Required"
        style="width: 500px; font-size: 12px; display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <img src="../Images/alert.jpg" />
                            <asp:Label ID="lblConfirmationTitle"   runat="server" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rdbVesselTypeAssignmentList" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="1" Selected="True">Assign and add the vessel type to the crew member vessel type list</asp:ListItem>
                                <asp:ListItem Value="0">Assign without adding the vessel type</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:HiddenField ID="hdnSelectedCrewId" Value="" runat="server" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Button ID="Button1" Text="Cancel" runat="server" OnClientClick="hideVesselType();return false;" />
                            <asp:Button ID="btnAssignVesselTypeDisplay" OnClientClick="VesselTypeCrew(); return false;" Text="Assign"
                                runat="server" />
                            <asp:Button ID="btnAssignVesselType" Style="display: none;" Text="Assign" runat="server"
                                OnClick="btnAssignVesselType_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
       <script type="text/javascript">
           $(document).ready(function () {
               $("body").on("click", "#<%=btnAssignVesselType.ClientID %>", function () {
                   VesselTypeCrew();
               });

               $("body").on("click", "#closePopupbutton", function () {
                   hideVesselType();
               });
           });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var strDateFormat = "<%= DateFormat %>";
            var CurrentDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';
            //            $('.window').draggable();
            $(".remarks").tooltip();

            $("body").on("click", "#btnCreateEvent", function () {
                var Msg = "";
                if ($.trim($("#txtEventDate").val()) == "") {
                    Msg += "Enter Event Date\n";
                }
                else {
                    if (IsInvalidDate($("#txtEventDate").val(), strDateFormat)) {
                        Msg += "Enter Valid Event Date" + CurrentDateFormatMessage;
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });

            ///Check for port on click of Notify MO and Notify Master
            $("body").on("click", ".btnNotifyMO", function () {
                var ID = this.id.replace("ctl00_MainContent_rpt1_", "").replace("_btnNotifyMO", "");
                if (CheckPortForEvent(ID)) {
                    alert("Select port for this event to Notify M/O");
                    return false;
                }
            });

            $("body").on("click", ".btnNotifyMaster", function () {
                var ID = this.id.replace("ctl00_MainContent_rpt1_", "").replace("_btnNotifyMaster", "");
                if (CheckPortForEvent(ID)) {
                    alert("Select port for this event to Notify Master");
                    return false;
                }
            });


            $("body").on("click", ".btnNotifyFinalCrewChange", function () {
                var ID = this.id.replace("ctl00_MainContent_rpt1_", "").replace("_btnNotifyFinalCrewChange", "");
                if (CheckPortForEvent(ID)) {
                    alert("Select port for this event to Final Crew Change");
                    return false;
                }
            });

            function CheckPortForEvent(ID) {
                var ReturnVal = false;
                if ($.trim($("#ctl00_MainContent_rpt1_" + ID + "_lblPortName").text()) == "")
                    ReturnVal = true;
                return ReturnVal;
            }

            $('#dialog').dialog({
                autoOpen: false,
                modal: true,
                width: 800,
                buttons: {
                    "Close": function () {
                        $(this).dialog("close");
                    },
                    "Reload": function () {
                        var url = "ViewEvent.aspx?id=" + $('#dialog').attr('alt') + "&rnd=" + Math.random();

                        $("#dialog").dialog({ title: 'Loading Data ...' });

                        $.get(url, function (data) {
                            $('#dialog').html(data);
                            $("#dialog").dialog({ title: 'Event Details' });
                        });
                    }
                }
            });
        });
        $(document).load(function () {
            $('.window').draggable();
            $(".remarks").tooltip();
        });
        function ShowCrewDetails(ID) {
            window.open("CrewDetails.aspx?ID=" + ID);
        }
        function showEvent(ID, CrewID) {
            var url = "ViewEvent.aspx?id=" + ID + "&CrewID=" + CrewID + "&rnd=" + Math.random();

            //-- show dialog --
            $('#dialog').dialog('open');

            //-- load event data --
            $.get(url, function (data) {
                $('#dialog').html(data);
            });

            //-- remember event id --
            $('#dialog').attr('alt', ID + "&CrewID=" + CrewID);
        }
        ////---------------------------------
        function Async_getVesselMovements(vessel_id, vessel_code) {
            var url = "../webservice.asmx/getVesselMovements";
            var params = 'vessel_id=' + vessel_id;
            document.getElementById('dvVesselMovements').innerHTML = '';
            obj = new AsyncResponse(url, params, response_getVesselMovements);
            obj.getResponse();
            document.getElementById('dvVesselName').innerHTML = vessel_code;
        }

        function response_getVesselMovements(retval) {
            if (retval.indexOf('Working') >= 0) { return; }
            try {
                retval = clearXMLTags(retval);
                if (retval.indexOf('ERROR:', 0) >= 0) {
                    alert(retval);
                    return;
                }

                if (retval.trim().length > 0) {
                    var arVal = eval(retval);

                    var t = document.createElement('table');
                    var tb = document.createElement('tbody');
                    t.className = "vessel-movement-table";

                    var tr = document.createElement('tr');

                    for (var i = 0; i < arVal.length; i++) {

                        var oRowItem = arVal[i];

                        var Port_Call_ID = oRowItem.Port_Call_ID;
                        var Created_Date = oRowItem.Created_Date;
                        var Vessel_Code = oRowItem.Vessel_Code;
                        var Port_Name = oRowItem.Port_Name;
                        var Arrival = oRowItem.Arrival;
                        var Berthing = oRowItem.Berthing;
                        var Departure = oRowItem.Departure;
                        var Supplier_Name = oRowItem.Supplier_Name;

                        var td = document.createElement('td');

                        var dv1 = document.createElement('div');
                        dv1.className = 'port-name';
                        dv1.innerHTML = Port_Name;
                        dv1.id = Port_Call_ID;
                        td.appendChild(dv1);

                        var dv2 = document.createElement('div');
                        dv2.innerHTML = Arrival;
                        td.appendChild(dv2);

                        if (Berthing == "")
                            Berthing = "-";
                        var dv3 = document.createElement('div');
                        dv3.innerHTML = Berthing;
                        td.appendChild(dv3);

                        var dv4 = document.createElement('div');
                        dv4.innerHTML = Departure;
                        td.appendChild(dv4);

                        var dv5 = document.createElement('div');

                        dv5.innerHTML = (Supplier_Name == '') ? '--' : Supplier_Name;
                        //dv5.style.backgroundColor = 'Gray';
                        //dv5.style.color = 'White';
                        td.appendChild(dv5);

                        tr.appendChild(td);
                    }
                    tb.appendChild(tr);
                    t.appendChild(tb);

                    var dvRes = document.getElementById('dvVesselMovements');
                    dvRes.innerHTML = "";
                    dvRes.appendChild(t);

                    BindEvents();
                }
            }
            catch (ex) { alert(ex.message); }
        }

        var oSelectedDiv;
        function BindEvents() {
            $('.port-name').bind('click', function () {
                if (oSelectedDiv) {
                    oSelectedDiv.style.fontWeight = 'normal';
                    oSelectedDiv.nextSibling.style.backgroundColor = "White";
                }
                this.style.fontWeight = 'bold';
                oSelectedDiv = this;
                this.nextSibling.style.backgroundColor = "Yellow";

                //var objPortList = $('[id$=ddlEventPort]');
                var objPortList = document.getElementById('ctl00_MainContent_ddlEventPort');
                if (objPortList) {
                    objPortList.selectedText = 'TOKYO';
                }
            });
        }

        ////---------------------------------
        function Async_getLatestNoonReport(vessel_code) {
            var url = "../webservice.asmx/getLatestNoonReport";
            var params = 'vessel_code=' + vessel_code;
            //alert(params);
            obj = new AsyncResponse(url, params, response_getLatestNoonReport);
            obj.getResponse();
            document.getElementById('dvVesselName').innerHTML = vessel_code;
        }
        function response_getLatestNoonReport(retval) {

            if (retval.indexOf('Working') >= 0) { return; }
            try {
                retval = clearXMLTags(retval);
                if (retval.indexOf('ERROR:', 0) >= 0) {
                    alert(retval);
                    return;
                }
                //alert(retval);

                if (retval.trim().length > 0) {
                    var arVal = eval(retval);

                    var t = document.createElement('table');
                    var tb = document.createElement('tbody');
                    t.className = "noon-report-table";

                    var tr = document.createElement('tr');

                    for (var i = 0; i < arVal.length; i++) {

                        var oRowItem = arVal[i];

                        var Telegram_ID = oRowItem.Telegram_ID;
                        var Telegram_Date = oRowItem.Telegram_Date;
                        var Port_Name = oRowItem.Port_Name;
                        var NextPort = oRowItem.NextPort;
                        var Vessel_Short_Name = oRowItem.Vessel_Short_Name;
                        var location = oRowItem.location;
                        var ETA_Next_Port = oRowItem.ETA_Next_Port;

                        var td1 = document.createElement('td');
                        td1.innerHTML = "Latest NOON REPORT from " + Vessel_Short_Name + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;On " + Telegram_Date + ",&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Next Port: " + NextPort + ",&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ETA: " + ETA_Next_Port; ;

                        tr.appendChild(td1);
                    }
                    tb.appendChild(tr);
                    t.appendChild(tb);

                    var dvRes = document.getElementById('dvLatestNoonReport');
                    dvRes.innerHTML = "";
                    dvRes.appendChild(t);
                    BindEvents();
                }
            }
            catch (ex) { alert(ex.message); }
        }

        function toggleUpDown() {
            var img = window.event.srcElement;
            if (img.src.indexOf('up.png') >= 0) {
                img.src = '../Images/down.png';
                $('#dvContainer').hide();
            }
            else {
                img.src = '../Images/up.png';
                $('#dvContainer').show();
            }
        }

        function selectCheckBox(chkid) {
            var chk = document.getElementById(chkid);
            if (chk) {
                chk.checked = true;
                alert("ON-Signer and OFF-Signer both selected for the event.");
            }
        }

        function showNationalityApproval(Vessel_ID, EventID, CrewID, CurrentRank_ID, JoiningRank_ID, V, R) {

            $('[id$=lblAppVessel]').html(V);
            $('[id$=lblAppRank]').html(R);

            $('[id$=hdnAppVesselID]').val(Vessel_ID);
            $('[id$=hdnAppEventID]').val(EventID);
            $('[id$=hdnAppCrewID]').val(CrewID);
            $('[id$=hdnAppCurrentRankID]').val(CurrentRank_ID);
            $('[id$=hdnAppJoiningRankID]').val(JoiningRank_ID);

            showModal("dvNationalityApproval");
        }

        var _vesseltype = "";
        var _ConfText = "$$Ranks$$ does not have the required vessel type assignment.Choose if you want to add $$VesselType$$ to his vessel type list,or to assign him one time only";

        ///Show Vessel type assignment popup for multiple crew
        function showVesselType(vesseltype, CrewIDs) {
            if ($.trim(vesseltype) != "") {
                if (CrewIDs != "") {
                    $("#<%=hdnVesselTypeCrew.ClientID %>").val(CrewIDs);
                }

                var CrewIds = $("#<%=hdnVesselTypeCrew.ClientID %>").val();

                if (CrewIds.split("|").length == 1) {
                    $("#<%=btnAssignVesselTypeDisplay.ClientID %>").hide();
                    $("#<%=btnAssignVesselType.ClientID %>").show();
                }

                _vesseltype = vesseltype;

                var CrewId = CrewIds.split("|")[0].split(":")[0];  ////Get CrewId 
                var CrewName = CrewIds.split("|")[0].split(":")[1]; ////Get CrewName  

                ///Reset vessel type assignment list to default
                $("#<%=rdbVesselTypeAssignmentList.ClientID %> input[type='radio']").prop("checked", false);
                $("#<%=rdbVesselTypeAssignmentList.ClientID %> input[type='radio'][value='1']").prop("checked", true);

                $("#<%=hdnSelectedCrewId.ClientID %>").val(CrewId);
                $("#<%= lblConfirmationTitle.ClientID %>").text(_ConfText.replace("$$Ranks$$", CrewName).replace("$$VesselType$$", vesseltype));
                showModal("divVesselType");
            }
        }

        ///remove the assigned crew from hidden field which are not selected to assign vessel type
        function VesselTypeCrew() {
            
            var SelectedCrewId = $("#<%=hdnSelectedCrewId.ClientID %>").val();
            var Selectedvalue = $("#<%=rdbVesselTypeAssignmentList.ClientID %> input[type='radio']:checked").val();
            
            var hdnVesselTypeAssignedCrew = $("#<%= hdnVesselTypeAssignedCrew.ClientID %>").val();
            hdnVesselTypeAssignedCrew += SelectedCrewId + ":" + Selectedvalue + "|";
            $("#<%= hdnVesselTypeAssignedCrew.ClientID %>").val(hdnVesselTypeAssignedCrew);

            var AllCrewID = $("#<%=hdnVesselTypeCrew.ClientID %>").val();
            var NewCrewID = "";
            for (var i = 0; i < AllCrewID.split("|").length; i++) {
                var CrewId = AllCrewID.split("|")[i].split(":")[0];  ////Get CrewId 
                var CrewName = AllCrewID.split("|")[i].split(":")[1]; ////Get CrewName  

                if (CrewId != SelectedCrewId) {
                    if (i == AllCrewID.split("|").length - 1)
                        NewCrewID += CrewId + ":" + CrewName;
                    else
                        NewCrewID += CrewId + ":" + CrewName + "|";
                }
            }

            if (NewCrewID != "") {
                $("#<%=hdnVesselTypeCrew.ClientID %>").val('');
                $("#<%=hdnVesselTypeCrew.ClientID %>").val(NewCrewID);
                showVesselType(_vesseltype, '');
            }
            else {
                hideModal("divVesselType");
            }
            return false;
        }


        ///Onlick of cancel and close button
        function hideVesselType() {
            
            var SelectedCrewId = $("#<%=hdnSelectedCrewId.ClientID %>").val();

            var AllCrewID = $("#<%=hdnVesselTypeCrew.ClientID %>").val();
            var NewCrewID = "";
            for (var j = 0; j < AllCrewID.split("|").length; j++) {
                var CrewId = AllCrewID.split("|")[j].split(":")[0];  ////Get CrewId 
                var CrewName = AllCrewID.split("|")[j].split(":")[1]; ////Get CrewName  

                if (CrewId != SelectedCrewId) {
                    if (j == AllCrewID.split("|").length - 1)
                        NewCrewID += CrewId + ":" + CrewName;
                    else
                        NewCrewID += CrewId + ":" + CrewName + "|";
                }
            }

            ///reassign the new crew list
            if (NewCrewID != "") {
                $("#<%=hdnVesselTypeCrew.ClientID %>").val('');
                $("#<%=hdnVesselTypeCrew.ClientID %>").val(NewCrewID);
                showVesselType(_vesseltype, '');
            }
            else {
                hideModal("divVesselType");
                $("#<%=btnAssignVesselType.ClientID %>").click();
            }
        }
         
        function funVesselTooltip(CID, ob2, ob3) {
            asyncGet_Vessel_Information_ToolTip(CID, ob2, ob3);
        }
    </script>
</asp:Content>
