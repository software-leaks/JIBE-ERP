<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarBookingEntry.aspx.cs"
    Inherits="QMS_Calendar_CalendarBookingEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script language="javascript" type="text/javascript">

        function ValidationOnSave() {

            //            var BookinigDate = document.getElementById("txtBookingDate").value;
            //            var Timefrom = document.getElementById("ddltimefrom");
            //            var Timeto = document.getElementById("ddltimeto");
            //            var Room = document.getElementById("txtRoom").value;
            //            var Purpose = document.getElementById("txtRemarks").value;

            //            var e = document.getElementById("ddltimefrom");
            //            var strUser = e.options[e.selectedIndex].value; 

            //            var SelTimeFrom= $('[id$=ddltimefrom]').text();

            //            alert(SelTimeFrom);


            //            if ((document.getElementById("ctl00_MainContent_optForUser_0").checked == false) && (document.getElementById("ctl00_MainContent_optForUser_1").checked == false) && (document.getElementById("ctl00_MainContent_optForUser_2").checked == false)) {
            //                alert('Please Select the For user option.');
            //                return false;
            //            }

            //            if (Timefrom == "0") {
            //                alert('Time From is required')
            //                return false;
            //            }

            //            if (Timeto == "0") {
            //                alert('Time To is required.')
            //                return false;
            //            }

            //            if (Timefrom == Timeto) {

            //                alert('Time from and  time To cannot be equal.')
            //                return false;
            //            }

            //            if (Timefrom > Timeto) {

            //                alert('Time from cannot be less then Time to')
            //                return false;
            //            } 

            //            if (Room == "") {
            //                alert('Booking Room is required.')
            //                return false;
            //            }

            //            if (Purpose == "") {
            //                alert('Purpose is required.')
            //                return false;
            //            }


            return true;
        }


    </script>
    <form id="form1" runat="server" defaultbutton="btnSave">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <center>
        <div id="DivRoomBooking" style="position: relative; overflow: hidden; clear: right;font-family:Tahoma;font-size:13px; padding-top:1px;padding-bottom:0px;" >
            <table width="100%" cellpadding="2" cellspacing="1" style="border:1px #cccccc solid;" >
                <tr align="right" style="padding-top:8px">
                    <td style="width:15%">
                        Date :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="3" style="width:60%">
                        <asp:TextBox ID="txtBookingDate" runat="server" EnableViewState="true" Font-Size="11px"
                            ReadOnly="true" Width="150px" CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtBookingDate_CalendarExtender" runat="server"
                            Format="dd-MM-yyyy" TargetControlID="txtBookingDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Time from :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddltimefrom" runat="server" AutoPostBack="false" Width="156px"
                            CssClass="txtInput">
                            <asp:ListItem Selected="True" Value="0" Text="--SELECT--"></asp:ListItem>
                            
                            <asp:ListItem Value="0000" Text="0000"></asp:ListItem>
                            <asp:ListItem Value="0030" Text="0030"></asp:ListItem>
                            <asp:ListItem Value="0100" Text="0100"></asp:ListItem>
                            <asp:ListItem Value="0130" Text="0130"></asp:ListItem>
                            <asp:ListItem Value="0200" Text="0200"></asp:ListItem>
                            <asp:ListItem Value="0230" Text="0230"></asp:ListItem>
                            <asp:ListItem Value="0300" Text="0300"></asp:ListItem>
                            <asp:ListItem Value="0330" Text="0330"></asp:ListItem>
                            <asp:ListItem Value="0400" Text="0400"></asp:ListItem>
                            <asp:ListItem Value="0430" Text="0430"></asp:ListItem>
                            <asp:ListItem Value="0500" Text="0500"></asp:ListItem>
                            <asp:ListItem Value="0530" Text="0530"></asp:ListItem>
                            <asp:ListItem Value="0600" Text="0600"></asp:ListItem>
                            <asp:ListItem Value="0630" Text="0630"></asp:ListItem>
                            <asp:ListItem Value="0700" Text="0700"></asp:ListItem>
                            <asp:ListItem Value="0730" Text="0730"></asp:ListItem>
                            <asp:ListItem Value="0800" Text="0800"></asp:ListItem>
                            <asp:ListItem Value="0830" Text="0830"></asp:ListItem>
                            <asp:ListItem Value="0900" Text="0900"></asp:ListItem>
                            <asp:ListItem Value="0930" Text="0930"></asp:ListItem>
                            <asp:ListItem Value="1000" Text="1000"></asp:ListItem>
                            <asp:ListItem Value="1030" Text="1030"></asp:ListItem>
                            <asp:ListItem Value="1100" Text="1100"></asp:ListItem>
                            <asp:ListItem Value="1130" Text="1130"></asp:ListItem>
                            <asp:ListItem Value="1200" Text="1200"></asp:ListItem>
                            <asp:ListItem Value="1230" Text="1230"></asp:ListItem>
                            <asp:ListItem Value="1300" Text="1300"></asp:ListItem>
                            <asp:ListItem Value="1330" Text="1330"></asp:ListItem>
                            <asp:ListItem Value="1400" Text="1400"></asp:ListItem>
                            <asp:ListItem Value="1430" Text="1430"></asp:ListItem>
                            <asp:ListItem Value="1500" Text="1500"></asp:ListItem>
                            <asp:ListItem Value="1530" Text="1530"></asp:ListItem>
                            <asp:ListItem Value="1600" Text="1600"></asp:ListItem>
                            <asp:ListItem Value="1630" Text="1630"></asp:ListItem>
                            <asp:ListItem Value="1700" Text="1700"></asp:ListItem>
                            <asp:ListItem Value="1730" Text="1730"></asp:ListItem>
                            <asp:ListItem Value="1800" Text="1800"></asp:ListItem>
                            <asp:ListItem Value="1830" Text="1830"></asp:ListItem>
                            <asp:ListItem Value="1900" Text="1900"></asp:ListItem>
                            <asp:ListItem Value="1930" Text="1930"></asp:ListItem>
                            <asp:ListItem Value="2000" Text="2000"></asp:ListItem>
                            <asp:ListItem Value="2030" Text="2030"></asp:ListItem>
                            <asp:ListItem Value="2100" Text="2100"></asp:ListItem>
                            <asp:ListItem Value="2130" Text="2130"></asp:ListItem>
                            <asp:ListItem Value="2200" Text="2200"></asp:ListItem>
                            <asp:ListItem Value="2230" Text="2230"></asp:ListItem>
                            <asp:ListItem Value="2300" Text="2300"></asp:ListItem>
                            <asp:ListItem Value="2330" Text="2330"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Time To :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddltimeto" runat="server" AppendDataBoundItems="true" AutoPostBack="false"
                            Width="156px" CssClass="txtInput">

                            <asp:ListItem Selected="True" Value="0" Text="--SELECT--"></asp:ListItem>                           
                            <asp:ListItem Value="0030" Text="0030"></asp:ListItem>
                            <asp:ListItem Value="0100" Text="0100"></asp:ListItem>
                            <asp:ListItem Value="0130" Text="0130"></asp:ListItem>
                            <asp:ListItem Value="0200" Text="0200"></asp:ListItem>
                            <asp:ListItem Value="0230" Text="0230"></asp:ListItem>
                            <asp:ListItem Value="0300" Text="0300"></asp:ListItem>
                            <asp:ListItem Value="0330" Text="0330"></asp:ListItem>
                            <asp:ListItem Value="0400" Text="0400"></asp:ListItem>
                            <asp:ListItem Value="0430" Text="0430"></asp:ListItem>
                            <asp:ListItem Value="0500" Text="0500"></asp:ListItem>
                            <asp:ListItem Value="0530" Text="0530"></asp:ListItem>
                            <asp:ListItem Value="0600" Text="0600"></asp:ListItem>
                            <asp:ListItem Value="0630" Text="0630"></asp:ListItem>
                            <asp:ListItem Value="0700" Text="0700"></asp:ListItem>
                            <asp:ListItem Value="0730" Text="0730"></asp:ListItem>
                            <asp:ListItem Value="0800" Text="0800"></asp:ListItem>
                            <asp:ListItem Value="0830" Text="0830"></asp:ListItem>
                            <asp:ListItem Value="0900" Text="0900"></asp:ListItem>
                            <asp:ListItem Value="0930" Text="0930"></asp:ListItem>
                            <asp:ListItem Value="1000" Text="1000"></asp:ListItem>
                            <asp:ListItem Value="1030" Text="1030"></asp:ListItem>
                            <asp:ListItem Value="1100" Text="1100"></asp:ListItem>
                            <asp:ListItem Value="1130" Text="1130"></asp:ListItem>
                            <asp:ListItem Value="1200" Text="1200"></asp:ListItem>
                            <asp:ListItem Value="1230" Text="1230"></asp:ListItem>
                            <asp:ListItem Value="1300" Text="1300"></asp:ListItem>
                            <asp:ListItem Value="1330" Text="1330"></asp:ListItem>
                            <asp:ListItem Value="1400" Text="1400"></asp:ListItem>
                            <asp:ListItem Value="1430" Text="1430"></asp:ListItem>
                            <asp:ListItem Value="1500" Text="1500"></asp:ListItem>
                            <asp:ListItem Value="1530" Text="1530"></asp:ListItem>
                            <asp:ListItem Value="1600" Text="1600"></asp:ListItem>
                            <asp:ListItem Value="1630" Text="1630"></asp:ListItem>
                            <asp:ListItem Value="1700" Text="1700"></asp:ListItem>
                            <asp:ListItem Value="1730" Text="1730"></asp:ListItem>
                            <asp:ListItem Value="1800" Text="1800"></asp:ListItem>
                            <asp:ListItem Value="1830" Text="1830"></asp:ListItem>

                            <asp:ListItem Value="1900" Text="1900"></asp:ListItem>
                            <asp:ListItem Value="1930" Text="1930"></asp:ListItem>
                            <asp:ListItem Value="2000" Text="2000"></asp:ListItem>
                            <asp:ListItem Value="2030" Text="2030"></asp:ListItem>
                            <asp:ListItem Value="2100" Text="2100"></asp:ListItem>
                            <asp:ListItem Value="2130" Text="2130"></asp:ListItem>
                            <asp:ListItem Value="2200" Text="2200"></asp:ListItem>
                            <asp:ListItem Value="2230" Text="2230"></asp:ListItem>
                            <asp:ListItem Value="2300" Text="2300"></asp:ListItem>
                            <asp:ListItem Value="2330" Text="2330"></asp:ListItem>
                            <asp:ListItem Value="0000" Text="0000"></asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Room :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtRoom" runat="server" CssClass="txtInput" Width="150px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Purpose :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Verdana" TextMode="MultiLine"
                            Height="60px" Width="394px" CssClass="txtInput" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Attendees :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="2">
                        <asp:ListBox ID="lstAttendees" runat="server" Height="120px" Width="400px" CssClass="txtInput">
                        </asp:ListBox>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnInvite" runat="server" Text="Add Invitees" Width="90px" OnClick="btnInvite_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Booked By : &nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <asp:Label ID="lblBookedby" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        Booked On :
                    </td>
                    <td align="left">
                        <asp:Label ID="lblBookedOn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height:30px">
                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td colspan="4" style="background:#d8d8d8;height:30px;padding-bottom:0px">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="90px" OnClick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" Width="90px" OnClick="btnClose_Click" />&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="90px" OnClick="btnDelete_Click" />&nbsp;
                        <asp:HiddenField ID="hdfRoomid" runat="server" />
                        <asp:HiddenField ID="hdfBookingid" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
 
        <div id="DivUserList" runat="server" style="height: auto; width: 680px; position: absolute;
            left: 1%; top: 11%" title='Room Booking' class="popup-css">
            <table cellpadding="1" cellspacing="1">
                <tr>
                    <td>
                        Select invitees for the meeting
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 400px; width: 670px; text-align: left; border: 1px solid gray;overflow-x: hidden;overflow-y:scroll;">
                            <asp:CheckBoxList ID="chklistInviteUser" RepeatColumns="4" Font-Size="11px" Font-Names="verdana"
                                RepeatDirection="Vertical" RepeatLayout="Table" Height="400px" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSendInvite" Text="OK" Width="100px" OnClick="btnSendInvite_click"
                            runat="server" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" Width="100px" runat="server" OnClick="btnCancel_click" />
                    </td>
                </tr>
            </table>
        </div> 


    </center>
    </form>
</body>
</html>
