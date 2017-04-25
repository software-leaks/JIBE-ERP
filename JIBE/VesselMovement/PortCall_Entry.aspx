<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCall_Entry.aspx.cs" Inherits="VesselMovement_PortCall_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
   
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 98%;
    height: 100%;">
     
    <script language="javascript" type="text/javascript">


        function validation() {

          

            if (document.getElementById("ddlVessel").value == "0") {
                alert("Please Select Vessel");
                document.getElementById("ddlVessel").focus();
                return false;
            }

            if (document.getElementById("DDLPort") != null) {
               
                var textValue = document.getElementById('DDLPort_TextBox').value;
                var hiddenvalue = document.getElementById('DDLPort_HiddenField').value;

                if (document.getElementById("chkNewLocation").checked == 0 && (hiddenvalue == "0" || textValue == "")) {
                    alert("Please Select port.");
                    document.getElementById("DDLPort").focus();
                    return false;
                }
            }
            else if (document.getElementById("txtlocation") != null) {
                if (document.getElementById("chkNewLocation").checked == 1 && document.getElementById("txtlocation").value == "") {
                    alert("Please Enter port.");
                    document.getElementById("txtlocation").focus();
                    return false;
                }
            }

                return true;
            }



    </script>
   <script type="text/javascript">
       function ShowPopup(message) {
           $(function () {
               $("#dialog").html(message);
               $("#dialog").dialog({
                   title: "jQuery Dialog Popup",
                   buttons: {
                       Close: function () {
                           $(this).dialog('close');
                       }
                   },
                   modal: true
               });
           });
       };
</script>
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
        <div id="dialog" style="display: none"></div>
           <div style="border: 1px solid #cccccc" class="page-title">
                Port Call Details
            </div>
            <table border="0" cellpadding="2" cellspacing="2" width="700px">
                <tr id="tr1" runat="server" visible="false" >
                    <td align="right" style="width: 15%">
                    <asp:Label ID="lblOmmitted" Text="Port Call Status :" runat="server"></asp:Label>
                        &nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 35%" colspan="2">
                       <asp:CheckBox ID="chkPortCallStatus" runat="server" Text="Omitted" />
                    </td>
                    
                </tr>
                <tr>
                    <td align="right" style="width: 15%">
                        Vessel &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td align="left" style="width: 35%" colspan="2">
                        <asp:DropDownList ID="ddlVessel" runat="server" Width="120px" CssClass="txtInput">
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td align="right" style="width: 15%">
                        Port &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td align="left"  >
                   <%-- <asp:DropDownList ID="DDLPort"   runat="server" ></asp:DropDownList>--%>
                        <ajaxToolkit:ComboBox ID="DDLPort" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"  runat="server"  ></ajaxToolkit:ComboBox>
                       <asp:TextBox ID = "txtlocation" Visible = "false" runat = "server" Width="120px" 
                            CssClass="txtInput" MaxLength="100"></asp:TextBox>
                        </td>
                    <td align="right" style="width: 15%" >
                        <asp:CheckBox ID="chkNewLocation" runat="server" Text="New Location" AutoPostBack="true" oncheckedchanged="chkNewLocation_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%">
                        Arrival &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        &nbsp;</td>
                    <td align="left" colspan="3" style="width: 40%">
                        <asp:TextBox ID="dtpArrival" runat="server" Width="120px" MaxLength="100" CssClass="txtInput" onkeydown="return false;"></asp:TextBox>
                        <cc1:CalendarExtender TargetControlID="dtpArrival" ID="caltxtArrival" runat="server">
                        </cc1:CalendarExtender>
                        <asp:DropDownList ID="ddlArrHour" runat="server" AutoPostBack="false" Width="50px"
                            CssClass="txtInput">
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
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
                        <asp:DropDownList ID="ddlArrMin" runat="server" AutoPostBack="false" Width="50px"
                            CssClass="txtInput">
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
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
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="54" Text="55"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Berthing &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                       
                    </td>
                    <td align="left" colspan="3" style="width: 40%">
                        <asp:TextBox ID="dtpBerthing" runat="server" Width="120px" MaxLength="100" CssClass="txtInput" onkeydown="return false;"></asp:TextBox>
                        <cc1:CalendarExtender TargetControlID="dtpBerthing" SelectedDate='<%# DateTime.Now %>' ID="caltxtBerthingDate" runat="server">
                        </cc1:CalendarExtender>
                        <asp:DropDownList ID="ddlBerthingHour" runat="server" AutoPostBack="false" Width="50px"
                            CssClass="txtInput">
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
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
                        <asp:DropDownList ID="ddlBerthingMin" runat="server" AutoPostBack="false" Width="50px"
                            CssClass="txtInput">
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
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
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="54" Text="55"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Departure &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        &nbsp;</td>
                    <td align="left" colspan="3" style="width: 40%">
                        <asp:TextBox ID="dtpDeparture" runat="server" Width="120px" MaxLength="100" CssClass="txtInput" onkeydown="return false;"></asp:TextBox>
                        <cc1:CalendarExtender TargetControlID="dtpDeparture" ID="caltxtDepartureDate" runat="server">
                        </cc1:CalendarExtender>
                        <asp:DropDownList ID="ddlDepHr" runat="server" AutoPostBack="false" Width="50px"
                            CssClass="txtInput">
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
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
                        <asp:DropDownList ID="ddlDepmin" runat="server" AutoPostBack="false" Width="50px"
                            CssClass="txtInput">
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
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
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="54" Text="55"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr >
                    <td align="right">
                        War Risk &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkWarRisk" runat="server" Text="" />
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Ship Crane Required &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkShipCrane" runat="server" Text="" />
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                  <tr>
                    <td align="right">
                        Auto Date On &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkAutoDate" Checked="false" AutoPostBack ="true" 
                            runat="server" Text="" oncheckedchanged="chkAutoDate_CheckedChanged" />
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Charterers Agent &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCharterAgent" runat="server" Width="240px" CssClass="txtInput">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Owners Agent &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlOwnersAgent" runat="server" Width="240px" CssClass="txtInput">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%">
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%">
                        Remark &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:TextBox ID="txtPortRemark" runat="server" Width="240px" 
                            Height="30px" MaxLength="200" CssClass="txtInput"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                   
                    </td>
                    <td align="left">
                     <asp:HiddenField ID="hdPortID" runat="server" />
                    <asp:HiddenField ID="dhDepDate" runat="server" />
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
               
                    <td colspan="4" align="left" style="color: #FF0000; font-size: small;">
                        * Mandatory fields &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4" align="center" style="color: #FF0000; font-size: small;">
                        <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                    </td>
                   
                </tr>
            </table>
        </center>
        <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnsave" runat="server" OnClientClick="return validation();"
                            Text="Save" OnClick="btnsave_Click"  />
                        <asp:TextBox ID="txtPortCallID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtVesselCode" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
