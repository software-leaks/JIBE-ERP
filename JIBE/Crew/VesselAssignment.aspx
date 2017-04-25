<%@ Page Title="Vessel Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="VesselAssignment.aspx.cs" Inherits="Crew_VesselAssignment" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
<%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(function () {
            // Dialog			
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

            //		        // Dialog Link
            //		        $('#dialog_link').click(function () {
            //		            $('#dialog').dialog('open');
            //		            return false;
            //		        });


            //hover states on the static widgets
            $('#dialog_link, ul#icons li').hover(
					function () { $(this).addClass('ui-state-hover'); },
					function () { $(this).removeClass('ui-state-hover'); }
				);

        });
    </script>
    <style type="text/css">
        #dialog
        {
            font-family: Verdana;
            font-size: 10px;
        }
    </style>
    <script type="text/javascript">
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

        function showNationalityApproval() {
            //$('[id$=txtAppRequest]').val("Request of approval for more than 2 staffs of the same rank/category.");
            showModal("dvNationalityApproval");
        }  
    </script>
    <style type="text/css">
        .bgPink
        {
            background-color: #F5A9A9;
        }
        .bgEventBlue
        {
            background-color: #CED8F6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div id="page-title" class="page-title">
        <table style="width: 100%">
            <tr>
                <td style="width: 33%">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Vessel Assignment"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                    <div style="float: right">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div class="error-message">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="dvMain">
            <table style="width: 100%;">
                <tr>
                    <td width="50%" style="background-color: #aabbee; text-align: center; color: Black;
                        font-weight: bold;">
                        Crew Finishing Contract
                    </td>
                    <td>
                    </td>
                    <td width="50%" style="background-color: #aabbee; text-align: center; color: Black;
                        font-weight: bold;">
                        Un-Assigned Crew
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlFilter_SignOffCrew" runat="server" DefaultButton="btnFindSignOffCrew">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            Fleet
                                                        </td>
                                                        <td>
                                                            Vessel
                                                        </td>
                                                        <td colspan="2">
                                                            Nationality
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFleet" runat="server" Width="115px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlVessel" runat="server" Width="115px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNationality_SOff" runat="server" Width="115px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Est. Sign Off From
                                                        </td>
                                                        <td>
                                                            To
                                                        </td>
                                                        <td>
                                                            Rank
                                                        </td>
                                                        <td>
                                                            Search Text
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDt" runat="server" Width="100px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDt">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDt" runat="server" Width="100px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtToDt">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRank" runat="server" Width="110px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 130px">
                                                            <asp:TextBox ID="txtFreeText" runat="server" Width="80px"></asp:TextBox>
                                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Search.png"
                                                            OnClick="btnFindSignOffCrew_Click" />
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Clear.gif" OnClick="btnClearSearch_Click" />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:Button ID="btnFindSignOffCrew" runat="server" Text="Search"  OnClick="btnFindSignOffCrew_Click"/>
                                                   <br />
                                                <asp:Button ID="btnClearSearch" runat="server" Width="57px" CssClass="btnCSS"  Text="Clear" OnClick="btnClearSearch_Click" />
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="background-color: Yellow;">
                                                <asp:Label ID="lblSEQ" Text="" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 20px;">
                    </td>
                    <td style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlFilter_UACrew" runat="server" DefaultButton="btnFindUnAssignedCrew">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            Manning Office
                                                        </td>
                                                        <td>
                                                            Nationality
                                                        </td>
                                                        <td>
                                                            Rank
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlManningOffice" runat="server" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNationality" runat="server" Width="115px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRank_UA" runat="server" Width="110px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Available From
                                                        </td>
                                                        <td>
                                                            To
                                                        </td>
                                                        <td>
                                                            Search Text
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDt_UA" runat="server" Width="100px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDt_UA">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDt_UA" runat="server" Width="100px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToDt_UA">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFreeText_UA" runat="server" Width="80px">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:RadioButtonList ID="UA_AvailableOptions" runat="server" CellPadding="0" CellSpacing="1"
                                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="UA_AvailableOptions_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Value="1" Text="Available Ashore" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="On-Board"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="All"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            Vessel
                                                            <asp:DropDownList ID="ddlVessel_UA" runat="server" Width="110px" OnSelectedIndexChanged="ddlVessel_UA_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:Button ID="btnFindUnAssignedCrew" runat="server" Text="Search" OnClick="btnFindUnAssignedCrew_Click"
                                                     /><br />
                                                <asp:Button ID="btnClearSearchUA" runat="server" Width="57px" CssClass="btnCSS" Text="Clear" OnClick="btnClearSearchUA_Click"/>
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="background-color: Yellow;">
                                                <asp:Label ID="lblCurrentCrewList" runat="server" Text="Current Crew List"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                        <div class="grid-container">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvSignOffCrew" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                                        AllowPaging="false" GridLines="Horizontal" Width="100%" DataKeyNames="ID,Vessel_ID,Vessel_Short_Name"
                                        EnablePersistedSelection="true" Font-Size="11px" OnRowDataBound="gvSignOffCrew_RowDataBound"
                                        OnSelectedIndexChanged="gvSignOffCrew_SelectedIndexChanged" CssClass="GridCSS"  >                                     
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVessel_CODE" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'
                                                        class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S/Code" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                        <%# Eval("staff_Code")%></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nationality" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("Nationality")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EOC Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSignOffDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Eval. Avg" HeaderStyle-HorizontalAlign="Center">
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblAvg" runat="server" Text='<%# Eval("AllAvg")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                        CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblNoRec" runat="server" Text="No Staff found."></asp:Label>
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                            CssClass="pager" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPager_OffSigners" runat="server" RecordCountCaption="&nbsp;&nbsp;Staffs"
                                        OnBindDataItem="Search_SigningOff" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td style="padding: 2px; vertical-align: middle; text-align: center;">
                    </td>
                    <td style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                        <div class="grid-container">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvUnAssignedCrew" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                                        AllowPaging="false" GridLines="Horizontal" Width="100%" DataKeyNames="ID,Rank_ID"
                                        EnablePersistedSelection="true" AllowSorting="true" Font-Size="11px" OnRowDataBound="gvUnAssignedCrew_RowDataBound"
                                        OnSelectedIndexChanged="gvUnAssignedCrew_SelectedIndexChanged" CssClass="GridCSS" >
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css"/>
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Manning Office">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVessel_CODE" runat="server" Text='<%# Eval("Company_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S/Code" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                        <%# Eval("staff_Code")%></a>
                                                    <%--<asp:Label ID="lblSTAFF_CODE" runat="server" Text='<%# Eval("staff_Code")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnStaff_Rank_UA" runat="server" Value='<%# Eval("Rank_ID")%>' />
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nationality" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("Nationality")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Readiness" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReadyToJoin" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Available_From_Date"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                                        vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eval. Avg" HeaderStyle-HorizontalAlign="Center">
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblAvg" runat="server" Text='<%# Eval("AllAvg")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                        CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgInvalid" runat="server" ImageUrl="~/images/exclamation.png"
                                                        CausesValidation="False" AlternateText="Invalid" Visible="false" Height="16px">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblNoRecUA" runat="server" Text="No Un-Assigned Staff found for the search."></asp:Label>
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                     
                                        <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                            CssClass="pager" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPager_OnSigners" runat="server" RecordCountCaption="&nbsp;&nbsp;Staffs"
                                        OnBindDataItem="Search_UnAssigned" />
                                    <asp:Label ID="lblStaffHistory" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center; margin-top: 5px;">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    Select Joining Rank:
                    <asp:DropDownList ID="ddlJoiningRank" runat="server" DataTextField="Rank_short_Name"
                        DataValueField="ID" Width="110px">
                    </asp:DropDownList>
                    <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <table style="width: 100%; margin-top: 10px; border: 2px solid #aabbee;">
            <tr style="background-color: #aabbee;">
                <td width="50%" style="text-align: center; color: Black; font-weight: bold;">
                    Crew OnBoard, FINISHING CONTRACT
                </td>
                <td width="50%" style="text-align: center; color: Black; font-weight: bold;">
                    Crew Presently Ashore - ASSIGNED
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvCrewChangeEvent" runat="server" AutoGenerateColumns="False" BorderColor="#336666"
                                BorderStyle="Double" BorderWidth="0px" CellPadding="1" Font-Size="11px" GridLines="Horizontal"
                                Width="100%" DataKeyNames="PKID" AllowSorting="True" OnRowDataBound="gvCrewChangeEvent_RowDataBound"
                                OnRowDeleting="gvCrewChangeEvent_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'
                                                class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblStaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>' CssClass="staffInfo"
                                                Target="_blank" Text='<%# Eval("staff_Code")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Blue" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="290px" HeaderStyle-HorizontalAlign="Left">
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
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
                                            <asp:HyperLink ID="lblstaff_Code_ua" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID_UA")%>' CssClass="staffInfo"
                                                Target="_blank" Text='<%# Eval("staff_Code_ua")%>'></asp:HyperLink>
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
                                            <asp:Label ID="lblReadiness" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("available_from_date"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
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
                                <HeaderStyle HorizontalAlign="Left" BackColor="#336666" Font-Bold="True" ForeColor="white" />
                                <RowStyle HorizontalAlign="Left" />
                                <PagerStyle BackColor="#336666" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#275353" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog" title="Event Details">
        Loading Data ...
    </div>
    <div id="dvNationalityApproval" title="Approval Details" style="width:500px;font-size:12px;display: none">
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
                            The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel
                            <br /><br />
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
</asp:Content>
