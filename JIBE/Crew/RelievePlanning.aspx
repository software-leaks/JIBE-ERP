<%@ Page Title="Crew Planning" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="RelievePlanning.aspx.cs" Inherits="Crew_RelievePlanning" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList" TagPrefix="ucDDL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>

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

             //hover states on the static widgets
            $('#dialog_link, ul#icons li').hover(
					function () { $(this).addClass('ui-state-hover'); },
					function () { $(this).removeClass('ui-state-hover'); }
				);
        });

        function showVesselType() {
            showModal("divVesselType");
        }
        function hideVesselType() {
            hideModal("divVesselType");
        }
    </script>
    <style type="text/css">
        #dialog
        {
            font-family: Verdana;
            font-size: 10px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var strDateFormat = "<%= DateFormat %>";
            var CurrentDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';
            $("body").on("click", "#btnFindSignOffCrew", function () {
                var Msg = "";
                if ($.trim($("#txtFromDt").val()) != "") {
                    if (IsInvalidDate($("#txtFromDt").val(), strDateFormat)) {
                        Msg += "Enter valid From Date"+CurrentDateFormatMessage+"\n";
                    }
                }
                if ($.trim($("#txtToDt").val()) != "") {
                    if (IsInvalidDate($("#txtToDt").val(), strDateFormat)) {
                        Msg += "Enter valid To Date" + CurrentDateFormatMessage + "\n";
                    }
                }
                if (($.trim($("#txtFromDt").val()) != "") && ($.trim($("#txtToDt").val()) != "")) {
                    if (DateAsFormat($.trim($("#txtFromDt").val()), strDateFormat) > DateAsFormat($.trim($("#txtToDt").val()), strDateFormat)) {
                        Msg += " From date cannot be greater than to date\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });

            $("body").on("click", "#btnFindUnAssignedCrew", function () {
                var Msg = "";
                if ($.trim($("#txtFromDt_UA").val()) != "") {
                    if (IsInvalidDate($("#txtFromDt_UA").val(), strDateFormat)) {
                        Msg += "Enter valid From Date" + CurrentDateFormatMessage + "\n";
                    }
                }
                if ($.trim($("#txtToDt_UA").val()) != "") {
                    if (IsInvalidDate($("#txtToDt_UA").val(), strDateFormat)) {
                        Msg += "Enter valid To Date" + CurrentDateFormatMessage + "\n";
                    }
                }
                if (($.trim($("#txtFromDt_UA").val()) != "") && ($.trim($("#txtToDt_UA").val()) != "")) {
                    if (DateAsFormat($.trim($("#txtFromDt_UA").val()), strDateFormat) > DateAsFormat($.trim($("#txtToDt_UA").val()), strDateFormat)) {
                        Msg += " Available from date cannot be greater than to date\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });
        });
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
            showModal("dvNationalityApproval");
        }
        function funVesselTooltip(CID, ob2, ob3) {
            asyncGet_Vessel_Information_ToolTip(CID, ob2, ob3);
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
    <style type="text/css">
        #dialog
        {
            font-family: Verdana;
            font-size: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" class="page-title">
        <table style="width: 100%">
            <tr>
                <td style="width: 34%">
                </td>
                <td style="width: 32%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Crew Planning"></asp:Label>
                </td>
                <td style="width: 32%; text-align: right;">
                    <asp:HyperLink ID="lnkCrewMatrix" runat="server"  Text="Crew Matrix" Target="_blank" NavigateUrl="~/Crew/CrewMatrixNew.aspx"  style="font-family: Tahoma ,Tahoma, Sans-Serif,vrdana; font-size: 12px; "  />
                </td>
                <td style="width: 34%; text-align: right;">
                    <div style="float: right">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="error-message">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvMain" style="font-family: Tahoma; font-size: 12px; width: 100%; height: 100%; ">
        <table style="width: 100%;">
            <tr>
                <td style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlFilter_SignOffCrew" runat="server" DefaultButton="btnFindSignOffCrew">
                                <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        Fleet
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="115px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Vessel
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="115px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Nationality
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNationality_SOff" runat="server" Width="115px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td></td><td></td>
                                                    <td>
                                                     <asp:Button ID="btnFindSignOffCrew"  ClientIDMode="Static"  runat="server" Width="60px" Text="Search"  OnClick="btnFindSignOffCrew_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Est. Sign Off From
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDt" runat="server" Width="100px"   ClientIDMode="Static" ></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDt">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        To
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDt" runat="server" Width="100px"  ClientIDMode="Static" ></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtToDt">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        Rank
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlRank" runat="server" Width="110px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        Search Text
                                                    </td>
                                                    <td style="width: 130px">
                                                        <asp:TextBox ID="txtFreeText" runat="server" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                     <asp:Button ID="btnClearSearch" runat="server" Width="60px" CssClass="btnCSS" Text="Clear"
                                                         OnClick="btnClearSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 70px">
                                           <%-- <asp:Button ID="btnFindSignOffCrew" runat="server" Text="Search" OnClick="btnFindSignOffCrew_Click" />--%>
                                            <br /><br />
                                           <%-- <asp:Button ID="btnClearSearch" runat="server" Width="57px" CssClass="btnCSS" Text="Clear"
                                                OnClick="btnClearSearch_Click" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="background-color: Yellow;" id="tdSEQ" runat="server" visible="false">
                                            <asp:Label ID="lblSEQ" Text="" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="50%" style="background-color: #aabbee;">
                    <asp:Label ID="Label2" Text="Crew OnBoard, FINISHING CONTRACT" Width="600" runat="server"
                        Style="text-align: center; color: Black; font-weight: bold;"></asp:Label>
                    <asp:Label ID="Label3" Text=" Crew Presently Ashore - ASSIGNED" Width="600" runat="server"
                        Style="text-align: center; color: Black; font-weight: bold;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                    <div class="grid-container">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvSignOffCrew" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                                    AllowPaging="false" GridLines="Horizontal" Width="100%" DataKeyNames="ID,Vessel_ID,Vessel_Short_Name,PKID"
                                    Font-Size="11px" OnRowDataBound="gvSignOffCrew_RowDataBound" OnSelectedIndexChanging="gvSignOffCrew_SelectedIndexChanging"
                                    CssClass="GridCSS" OnRowDeleting="gvSignOffCrew_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_CODE" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                <asp:Label ID="lblVesselId" runat="server" Text='<%# Eval("Vessel_ID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblVesselType" runat="server" Text='<%# Eval("Vessel_Type")%>' Visible="false"></asp:Label>
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
                                                <asp:Label ID="lblRankId" runat="server" Text='<%# Eval("RankId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("Staff_Name_SF")%>'></asp:Label>
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
                                                <asp:Label ID="lblSignOffDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>'  ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Event">
                                            <ItemTemplate>
                                                <img id="imgEvent" class="event-link" src='<%#(Eval("Event_Status_OFF").ToString()=="1")?"../Images/E_OFF_OPEN.png":"../Images/E_OFF_CLOSED.png"%>'
                                                     onclick='<%# "showEvent("+Eval("EventID_OFF").ToString()+"," + Eval("CrewID").ToString()+ ")" %>'
                                                     alt="Event" style='<%# Eval("EventID_OFF").ToString()=="0"? "display:none": "display:block" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assign" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAssign" Width="65" Height="22" CommandName="Select" runat="server"
                                                    OnClick="imgbtnAssign_Click" Text='<%# Eval("AssignButtonHeader")%>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manning Office" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany_Name" runat="server" Text='<%# Eval("Company_Name_UA")%>'></asp:Label>
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
                                                <asp:Label ID="lblRank_ua" runat="server" Text='<%# Eval("Rank_UA")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstaff_name_ua" runat="server" Text='<%# Eval("Staff_Name_UA")%>'></asp:Label>
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
                                                <asp:Label ID="lblReadiness" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Available_From_Date_UA"))) %>'  ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Event">
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
                                                    AlternateText="Delete" Visible='<%# Eval("DeleteVisibility").ToString() =="0"?false:true%>'>
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoRec" runat="server" Text="No Staff found."></asp:Label>
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
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
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePnlAssign" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divUnassigned"  title="Un-Assigned Crew" style="font-family: Tahoma; color: black; text-align: center; display: none; width: 900px;">
                <center>
                  <asp:Label ID="lblVesselIdOffsigner" runat="server" Visible="false"></asp:Label>
                    <table cellpadding="2" cellspacing="2">
                      <tr>
                            <td width="50%"  style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlFilter_UACrew" runat="server" DefaultButton="btnFindUnAssignedCrew" >
                                            <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    Manning Office
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlManningOffice" runat="server" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Nationality
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNationality" runat="server" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Rank
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlRank_UA" runat="server" Width="150px" OnSelectedIndexChanged="ddlRank_UA_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td><asp:Button ID="btnFindUnAssignedCrew" runat="server" Text="Search" OnClick="btnFindUnAssignedCrew_Click" ClientIDMode="Static" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Available From
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFromDt_UA" runat="server" Width="145px" ClientIDMode="Static"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFromDt_UA">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    To
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtToDt_UA" runat="server" Width="145px"  ClientIDMode="Static"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtToDt_UA">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    Search Text
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFreeText_UA" runat="server" Width="145px"> </asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                                <td><asp:Button ID="btnClearSearchUA" runat="server" Width="57px" CssClass="btnCSS" Text="Clear" OnClick="btnClearSearchUA_Click" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:RadioButtonList ID="UA_AvailableOptions" runat="server" CellPadding="0" CellSpacing="1"
                                                                        RepeatDirection="Horizontal" 
                                                                        AutoPostBack="true" >
                                                                        <asp:ListItem Value="1" Text="Available Ashore" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="On-Board"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="All"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="Label1" runat="server" Text="Vessel" Width="62px"></asp:Label>
                                                                    <asp:DropDownList ID="ddlVessel_UA" runat="server" Width="150" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td >
                                                                    Vessel Type 
                                                                </td>
                                                                <td align="left">
                                                                    <ucDDL:ucCustomDropDownList ID="ddlVesselType" runat="server" UseInHeader="False" 
                                                                        Height="150" Width="150" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 70px">
                                                     </td>
                                                </tr>
                                                <tr>
                                                    <td  colspan="2" style="padding: 2px; border: 2px solid #aabbee; vertical-align: top;">
                                                        <div class="grid-container" >
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="gvUnAssignedCrew" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                                                                        AllowPaging="false" GridLines="Horizontal" Width="100%" DataKeyNames="ID,Rank_ID" OnPreRender="gvUnAssignedCrew_PreRender"
                                                                        EnablePersistedSelection="true" AllowSorting="true" Font-Size="11px" OnRowDataBound="gvUnAssignedCrew_RowDataBound" OnSorting="GridView1_Sorting"
                                                                        CssClass="GridCSS">
                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Manning Office" >
                                                                            <HeaderTemplate>
                                                                             <asp:LinkButton ID="lblVessel_ODE" runat="server" ForeColor="Black" CommandArgument="Company_Name" 
                                                                              CommandName="Sort">Manning Office <img id="EVAL_LAST_AVG" runat="server" visible="false" style="border:0"/></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                      <asp:Label ID="lblSTAFFID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                                                      <asp:Label ID="lblVessel_CODE" runat="server" Text='<%# Eval("Company_Name")%>'></asp:Label>
                                                                                      
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="100px" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                          
                                                                            
                                                                            <asp:TemplateField HeaderText="S/Code" HeaderStyle-HorizontalAlign="Left">
                                                                             <HeaderTemplate>
                                                                             <asp:LinkButton ID="lblSO" runat="server" ForeColor="Black" CommandArgument="Staff_Code" 
                                                                              CommandName="Sort"> S/O Code<img id="Staff_Code" runat="server" visible="false" style="border:0"/></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                        <%# Eval("staff_Code")%></a>
                                                                                    
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="60px" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                                                             <HeaderTemplate>
                                                                             <asp:LinkButton ID="lblSOR" runat="server" ForeColor="Black" CommandArgument="Rank_ID" 
                                                                              CommandName="Sort"> Rank<img id="Rank_ID" runat="server" visible="false" style="border:0"/></asp:LinkButton>
                                                                            </HeaderTemplate>
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
                                                                             <HeaderTemplate>
                                                                             <asp:LinkButton ID="lblNSO" runat="server" ForeColor="Black" CommandArgument="Nationality" 
                                                                              CommandName="Sort">Nationality<img id="Sstaff_Code" runat="server" visible="false" style="border:0"/></asp:LinkButton>
                                                                            </HeaderTemplate>
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
                                                                             <HeaderTemplate>
                                                                             <asp:LinkButton ID="lblVNSO" runat="server" ForeColor="Black" CommandArgument="Vessel" 
                                                                              CommandName="Sort">Vessel<img id="vessel" runat="server" visible="false" style="border:0"/></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                                                                        vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="60px" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Vessel Types" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVesselType" runat="server" Text='<%# Eval("VesselType")%>'  onmouseover='<%# "funVesselTooltip( "+ Eval("ID") + ",event,this);" %>' ></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="60px" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="right" ShowHeader="False">
                                                                                <ItemTemplate  >
                                                                                       <asp:RadioButton ID="RowSelector" runat="server" AutoPostBack="true" CommandName="Select"
                                                                                            CommandArgument='<%# Eval("Id") %>' OnCheckedChanged="rbtnSelect_CheckedChanged" />
                                                                                        <asp:ImageButton ID="ImgInvalid" runat="server" ImageUrl="~/images/exclamation.png"
                                                                                            CausesValidation="False" AlternateText="Invalid" Visible="false" Height="16px" >
                                                                                        </asp:ImageButton>
                                                                                            </ItemTemplate>
                                                                                <ItemStyle Width="4px" HorizontalAlign="Right" />
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
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="text-align: center;">
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
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="dialog" title="Event Details">
    </div>
     <div ID="divVesselType" clientidmode="Static" runat="server" title="Confirmation Required" style="width: 500px; font-size: 12px; display: none" >
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <img src="../Images/alert.jpg" />
                            <asp:Label ID="lblConfirmationTitle" runat="server" style="font-weight: bold;" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:RadioButtonList ID="rdbVesselTypeAssignmentList" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="1" Selected="True">Assign and add the vessel type to the crew member vessel type list</asp:ListItem>
                                <asp:ListItem Value="0">Assign without adding the vessel type</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Button ID="btnCancel" Text="Cancel"  runat="server" OnClientClick="return hideVesselType();"/>
                            <asp:Button ID="btnAssignVesselType" Text="Assign"  runat="server" OnClick="btnAssignVesselType_Click"/>
                        </td>
                    </tr>                
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
