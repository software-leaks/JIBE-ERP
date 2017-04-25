<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewMatrix_Assignment.aspx.cs"
    Inherits="Crew_CrewMatrix_Assignment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
     <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: Tahoma,Tahoma,sans-serif,vrdana;
        }
        
         .ajax__calendar_invalid .ajax__calendar_year {color:Red !important;}
         .ajax__calendar_invalid .ajax__calendar_month {color:Red !important;}
         .ajax__calendar_invalid .ajax__calendar_day{color:Red !important;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="grid-container" style="margin-top: 2px">
        <asp:UpdatePanel ID="UpdatePanel_Msg" runat="server">
            <ContentTemplate>
                <div class="error-message">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:GridView ID="gvSignOnCrew" runat="server" AutoGenerateColumns="False" CellPadding="2"
            AllowPaging="false" GridLines="Horizontal" Width="100%" DataKeyNames="ID" AllowSorting="true"
            Font-Size="11px" CssClass="GridView-css" OnRowDataBound="gvSelectedONSigners_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="16%" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                        &nbsp;<a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                            <%# Eval("STAFF_CODE")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank" ItemStyle-Width="16%" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="16%"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;<asp:Label ID="lblSTAFFNAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Joining Rank" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="16%"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;<asp:DropDownList ID="ddlRank" runat="server" DataSourceID="objDS2" DataTextField="Rank_Short_Name"
                            DataValueField="ID" AppendDataBoundItems="true" Width="70px" Enabled="false">
                        </asp:DropDownList>
                        &nbsp;<asp:ObjectDataSource ID="objDS2" runat="server" TypeName="SMS.Business.Crew.BLL_Crew_Admin"
                            SelectMethod="Get_RankList"></asp:ObjectDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Wage Rank Scale" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="16%"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;<asp:DropDownList ID="ddlRankScale" runat="server" Width="93px">
                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Joining Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtJoinDate" runat="server" Width="80px" OnTextChanged="txtJoiningDate_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caltxtJoinDate" StartDate="<%# DateTime.Now %>"
                            runat="server" TargetControlID="txtJoinDate" Format='<%# Convert.ToString(Session["User_DateFormat"]) %>'>
                        </ajaxToolkit:CalendarExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EOC Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCOCDate" runat="server" Width="80px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caltxtCOCDate" runat="server" TargetControlID="txtCOCDate"
                           Format='<%# Convert.ToString(Session["User_DateFormat"]) %>'>
                        </ajaxToolkit:CalendarExtender>
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
            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
        </asp:GridView>
    </div>
    <div id="divEvent" style="margin-top: 2px; vertical-align: bottom; padding: 2px;
        color: Black; text-align: left; font-size: 14px;" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="text-align: center;">
                        Vessel Event:
                        <asp:DropDownList ID="ddlEvents" runat="server" AutoPostBack="false" DataTextField="PORT_NAME"
                            DataValueField="PKID" onchange="ClearEventDate();"  Width="220px" >
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr style="height: 10px; text-align: center;">
                <td>
                    - OR -
                </td>
            </tr>
            <tr>
                <td>
                    <div style="text-align: center;">
                        Create Event:
                        <asp:TextBox ID="txtEventDate" runat="server" Width="220px" onchange="ClearEventDropdown();"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtEventDate">
                        </ajaxToolkit:CalendarExtender>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr id="trAssign1" runat="server">
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btnAssign" Style="width: 70px"  runat="server" Text="Assign" OnClick="btnAssign_Click" />
                    <asp:Button ID="btnCancelAdditional" Style="width: 70px"  runat="server" Text="Cancel" OnClick="btnCancelAdditional_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvNationalityApproval" title="Approval Details" style="width: 500px; font-size: 12px;
        display: none" runat="server" visible="false" >
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
                            <asp:Button ID="Button1" Text="Cancel"  runat="server" OnClientClick="return hideModal('divVesselType');"/>
                            <asp:Button ID="btnAssignVesselType" Text="Assign"  runat="server" OnClick="btnAssignVesselType_Click"/>
                        </td>
                    </tr>                
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script type="text/javascript">

        function showVesselType() {
            showModal("divVesselType");
        }

        function ClearEventDropdown() {
            $("#ddlEvents").val(0);
        }
        function ClearEventDate() {
            $("#txtEventDate").val('');
        }
        function showNationalityApproval() {
            showModal("dvNationalityApproval");
        }


        $(document).ready(function () {
            var strDateFormat = "<%= DateFormat %>";

            function hideVesselType() {
                hideModal("divVesselType");
            }


            $("body").on("click", "#btnAssign", function () {
                var res;
                if ($("#gvSignOnCrew_ctl02_txtJoinDate").val() == "") {
                    $("#gvSignOnCrew_ctl02_txtJoinDate").focus();
                    alert("Enter Joining Date");
                    $("#gvSignOnCrew_ctl02_txtJoinDate").focus();
                    return false;
                }
                else {
                    if (IsInvalidDate($("#gvSignOnCrew_ctl02_txtJoinDate").val(), strDateFormat)){
                        $("#gvSignOnCrew_ctl02_txtJoinDate").focus();
                        alert("Enter valid Joining Date<%= UDFLib.DateFormatMessage() %>");
                        return false;
                    }
                }
                if ($("#gvSignOnCrew_ctl02_txtCOCDate").val() == "") {
                    $("#gvSignOnCrew_ctl02_txtCOCDate").focus();
                    alert("Enter EOC Date");
                    $("#gvSignOnCrew_ctl02_txtCOCDate").focus();
                    return false;
                }
                else {
                    if (IsInvalidDate($("#gvSignOnCrew_ctl02_txtCOCDate").val(), strDateFormat)){
                        $("#gvSignOnCrew_ctl02_txtCOCDate").focus();
                        alert("Enter valid EOC Date<%= UDFLib.DateFormatMessage() %>");
                        return false;
                    }
                }
                if ($("#ddlEvents").val() == "0" && $("#txtEventDate").val() == "") {
                    alert("Please select Vessel Event or Create Event");
                    $("#ddlEvents").focus();
                    return false;
                }
                else {
                    if ($("#txtEventDate").val() != "" && IsInvalidDate($("#txtEventDate").val(), strDateFormat)) {
                        $("#txtEventDate").focus();
                        alert("Enter valid Event Date<%= UDFLib.DateFormatMessage() %>");
                        $("#txtEventDate").focus();
                        return false;
                    }
                }
                var options = {

                    url: 'CrewMatrix_Assignment.aspx?Method=CheckAssign&OnSignnerCrewId=<%=OnSignnerCrewId %>&OffSignnerCrewId=<%=OffSignerCrewId %>&VesselId=<%=VesselId %>',
                    dataType: 'html',
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        res = parseInt(response.split('|')[0]);
                        vesselName = response.split('|')[1];
                    }
                }
                $.ajax(options);
                if (res <= 0) {
                    if (res == -2) {
                        if (window.confirm('The Assignment cannot be done.\nThe ON-signer has an open assignment on the vessel ' + vesselName + '.\nClick OK to assign the crew and remove him from the current assignment.')) {
                            DeleteCurrentAssignment();
                        }
                        else
                            return false;
                    }
                    else if (res == -3) {
                        alert('The ON-signer can not join this vessel as this is his first voyage and the ship does not allow new joiners as ratings.');
                        return false;
                    }
                    else if (res == -5) {
                        alert('This crew member is already onboard on vessel ' + vesselName + ', you can plan a transfer for this crew.');
                        return false;
                    }
                }

            });
            function DeleteCurrentAssignment() {
                var options = {
                    url: 'CrewMatrix_Assignment.aspx?Method=DeleteAssign&OnSignnerCrewId=<%=OnSignnerCrewId %> ',
                    dataType: 'html',
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response == 1) {
                            alert('Assignment Deleted');
                        }
                    }
                }
                $.ajax(options);
            }

        });       

        
    </script>
</body>
</html>
