<%@ Page Title="Interview Sheet" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Interview.aspx.cs" Inherits="Crew_Interview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        #page-content
        {
            font-size: 12px;
            color: Black;
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
        .dataTable
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
            color: Teal;
        }
        .dataTable td
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
        }
        .dataTable .data
        {
            vertical-align: top;
            background-color: #ffffff;
            color: Black;
        }
        .inline-edit
        {
            font-size: 10px;
            text-decoration: none;
            font-weight: normal;
            color: Blue;
        }
        .up
        {
            background-image: url(../Images/up.png);
            background-repeat: no-repeat;
        }
        .down
        {
            background-image: url(../Images/down.png);
            background-repeat: no-repeat;
        }
        .class-doc-list
        {
            font-size: 11px;
        }
        .class-doc-edit
        {
            font-size: 11px;
        }
        .control-edit
        {
            font-size: 12px;
            padding: 0px;
            width: 150px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            padding: 2px;
        }
        .error-message span
        {
            background-color: Yellow;
            text-align: center;
        }
        .watermarked
        {
            color: #cccccc;
        }
        .green
        {
            background-image: url(../Images/green-dot.png);
        }
        .red
        {
            background-image: url(../Images/red-dot.png);
        }
        .gray
        {
            background-image: url(../Images/gray-dot.png);
        }
        .textRemark
        {
            border: 1px solid gray;
        }
        .highlight
        {
            background-color: #58FAAC;
        }
    </style>
    <script type="text/javascript">
        function setDotColor(color_) {

            document.getElementById("tdDot").style.color = "white";
            if (color_ == "red")
                document.getElementById("tdDot").style.backgroundImage = "url(../Images/red-dot.png)";
            else if (color_ == "green")
                document.getElementById("tdDot").style.backgroundImage = "url(../Images/green-dot.png)";
            else {
                document.getElementById("tdDot").style.backgroundImage = "url(../Images/gray-dot.png)";
                document.getElementById("tdDot").style.color = "blue";
            }


        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="page-title">
                <asp:Label ID="lblPageTitle" runat="server" Text="Interview Sheet"></asp:Label>
            </div>
            <div class="error-message">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
            <div id="page-content" style="border: 1px solid gray; z-index: -2; overflow: auto;
                text-align: center;">
                <asp:HiddenField ID="hdnInterviewID" runat="server" Value="0" />
                <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
                <asp:HiddenField ID="hdnUserType" runat="server" Value="" />
                <asp:Panel ID="pnlInterviewPlanning" runat="server">
                    <div style="text-align: center">
                        <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 60%; text-align: left;">
                            <legend>Interview Planning:</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        Crew Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanCrewName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interview Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interview Time
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanH" runat="server" Width="50px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12" Text="12"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        </asp:DropDownList>
                                        H &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlPlanM" runat="server" Width="50px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                        </asp:DropDownList>
                                        M
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rank
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanRank" runat="server" DataTextField="Rank_Short_Name"
                                            DataValueField="id" Width="154px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interviewer
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanInterviewer" runat="server" DataSourceID="ObjectDataSource_UserList"
                                            DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlPlanInterviewer_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="ObjectDataSource_UserList" runat="server" SelectMethod="Get_UserList"
                                            TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Position
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanInterviewerPosition" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="btnSavePlanning" runat="server" Text="Save" OnClick="btnSavePlanning_Click" />
                                        <asp:Button ID="btnCancelPlanning" runat="server" Text="Cancel" OnClick="btnCancelPlanning_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEdit_InterviewResult" runat="server">
                    <center>
                        <div style="text-align: left; width: 80%;">
                            <table cellspacing="0" cellpadding="4" rules="rows" border="3" style="background-color: White;
                                border-color: #336666; border-width: 3px; border-style: Double; width: 100%;
                                border-collapse: collapse; margin: 5px;">
                                <tr style="color: White; background-color: #336666; font-weight: bold;">
                                    <td style="text-align: center; height: 30px; font-weight: bold; font-size: 14px;">
                                        FOR OFFICE USE ONLY (MANNING AGENT / HEAD OFFICE)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    Interview Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInterviewDate" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtInterviewDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    Interviewer's Name
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUserList" runat="server" DataSourceID="ObjectDataSource_UserList"
                                                        DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <div style="border: 1px solid gray; margin-bottom: 5px; padding: 2px; text-align: center;"
                                                        class="highlight">
                                                        Mandatory Questions are highlighted.</div>
                                                </td>
                                                <%--<td>
                                                    <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    Name of person Interviewed
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPersonInterviewed" runat="server" Width="200px"></asp:TextBox>
                                                    <asp:HyperLink ID="lnkOpenProfile" Target="_blank" runat="server">Open Profile</asp:HyperLink>
                                                </td>
                                                <td rowspan="3" style="text-align: right; vertical-align: top;">
                                                    <table style="border: 1px solid gray; width: 100%; background-color: #66DDFF;" cellpadding="3">
                                                        <tr>
                                                            <td style="text-align: left; color: #888;">
                                                                Planned Interviewer:
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPlannedInterviewer" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left; color: #888;">
                                                                Planned Date:
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPlannedDate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left; color: #888;">
                                                                Time Zone:
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPlannedTimeZone" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left; color: #888;">
                                                                Planned By:
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPlannedBy" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right;" colspan="2">
                                                                <asp:HyperLink ID="lnkEditSchedule" Target="_blank" runat="server" CssClass="linkImageBtn">Re-Schedule Interview</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    For Rank
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlRank" runat="server" DataTextField="Rank_Short_Name" DataValueField="id"
                                                        Width="204px" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Staff Code
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStaffCode" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td style="width: 30px">
                                                    1
                                                </td>
                                                <td>
                                                    English Language Ability
                                                </td>
                                                <td style="text-align: center; background-color: #58FAAC; border: 1px solid gray;">
                                                    Read<br />
                                                    <asp:RadioButtonList ID="rdoEnglishRead" runat="server" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td style="text-align: center; background-color: #58FAAC; border: 1px solid gray;">
                                                    Write<br />
                                                    <asp:RadioButtonList ID="rdoEnglishWrite" runat="server" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td style="text-align: center; background-color: #58FAAC; border: 1px solid gray;"
                                                    colspan="2">
                                                    Communication<br />
                                                    <asp:RadioButtonList ID="rdoEnglishComm" runat="server" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td style="text-align: center;">
                                                    Remarks
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    2
                                                </td>
                                                <td>
                                                    General Impression
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo2" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark2" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    3
                                                </td>
                                                <td>
                                                    Computer Knowledge (Officers)
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo3" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark3" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    4
                                                </td>
                                                <td>
                                                    Safety / Emergency Procedures
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo4" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark4" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    5
                                                </td>
                                                <td>
                                                    I S M Awareness (Officers)
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo5" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark5" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    6
                                                </td>
                                                <td>
                                                    Watch Keeping Procedures
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo6" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark6" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    7
                                                </td>
                                                <td>
                                                    Maintenance
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo7" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark7" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    8
                                                </td>
                                                <td>
                                                    Collision Regulations (Deck Officer)
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo8" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark8" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    9
                                                </td>
                                                <td>
                                                    Stability (Deck Officer)
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo9" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark9" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    10
                                                </td>
                                                <td>
                                                    Automation (Engineers & E/E)
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo10" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark10" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    11
                                                </td>
                                                <td>
                                                    Machinery Knowledge (Engg.)
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo11" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark11" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    12
                                                </td>
                                                <td>
                                                    Reefer Knowledge / Experience
                                                </td>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rdo12" runat="server" RepeatDirection="Horizontal" CellSpacing="4"
                                                        OnSelectedIndexChanged="CalculateMarks" AutoPostBack="true">
                                                        <asp:ListItem Text="V.Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Average" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Below Avg" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark12" runat="server" MaxLength="50" Width="350px" CssClass="textRemark"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; background-color: #F5F6CE; border-collapse: collapse;
                                            border-color: #efefef" border="1">
                                            <tr>
                                                <td colspan="2" style="color: Black; background-color: #cfcfff; vertical-align: top;">
                                                    Recommendations:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: Black; width: 300px;">
                                                    Recommendations for:
                                                </td>
                                                <td style="color: Black; text-align: left;">
                                                    <table style="width: 100%" border="1">
                                                        <tr>
                                                            <td>
                                                                Your Recommendations
                                                            <td style="font-size: 14px;">
                                                                <asp:RadioButtonList ID="rdoSelected" runat="server" RepeatDirection="Horizontal"
                                                                    CellPadding="10" CellSpacing="5">
                                                                    <asp:ListItem Value="1"><font color="Green"><b>Approved</b></font></asp:ListItem>
                                                                    <asp:ListItem Value="0"><font color="Red"><b>Rejected</b></font></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="width: 40%; text-align: right; color: #555">
                                                                Marks out of 5 (higher the better):
                                                            </td>
                                                            <td>
                                                                <div id="tdDot" class="gray" style="width: 30px; height: 30px; text-align: center;
                                                                    background-repeat: no-repeat; font-weight: bold; color: Blue; padding-top: 10px;">
                                                                    <asp:Label ID="lblTotalMarks" runat="server">0.00</asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div style="height: 100px; width: 160px; overflow: auto; background-color: White;
                                                                    border: 1px solid gray;">
                                                                    <asp:CheckBoxList ID="lstVessels" runat="server" DataTextField="vessel_name" DataValueField="vessel_id"
                                                                        CellPadding="0" CellSpacing="0" SelectionMode="Multiple" Font-Names="Tahoma"
                                                                        Font-Size="11px">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="height: 100px; width: 160px; overflow: auto; border: 1px solid gray;">
                                                                    <asp:CheckBoxList ID="chkTradingArea" runat="server" RepeatDirection="Vertical">
                                                                        <asp:ListItem Text="US Trades" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Europe Trades" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Intra-Asia" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Piracy" Value="4"></asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: left; vertical-align: top;">
                                                    <asp:TextBox ID="txtResultText" runat="server" TextMode="MultiLine" Width="600px"
                                                        Height="100px"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtResultText"
                                                        WatermarkText="Write your comment here !!" WatermarkCssClass="watermarked" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Button ID="btnSaveInterviewResult" Text=" Save " runat="server" OnClick="btnSaveInterviewResult_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text=" Close " OnClientClick="window.close()" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </center>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 600px; height: 400px; z-index: 1; color: black"
        title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
