<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallNotification_Entry.aspx.cs" Inherits="VesselMovement_PortCallNotification_Entry" %>

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
     <script src="../Scripts/common_functions.js" type="text/javascript"></script>
   
  
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
    </style>
   
  
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
     
    <script language="javascript" type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


        function validation() {

            debugger;

            var strDateFormat = "<%= DateFormat %>";



            if (document.getElementById("txtName").value == "") {
                  alert("Please Enter Notification Name.");
                document.getElementById("txtName").focus();
                    return false;
                
            }
            if (document.getElementById("txtDescription").value == "") {
                alert("Please Enter Notification Description.");
                document.getElementById("txtDescription").focus();
                return false;

            }
            if (document.getElementById("dtArrivalFrom").value == "") {
                alert("Please Enter Arrival From Date.");
                document.getElementById("dtArrivalFrom").focus();
                return false;
            }
            if (IsInvalidDate($("#dtArrivalFrom").val(), strDateFormat)) {
                alert("Please Enter Valid Arrival From Date.");
                document.getElementById("dtArrivalFrom").focus();
                return false;
            }
            if (document.getElementById("dtArrivalTo").value == "") {
                alert("Please Enter Arrival To Date.");
                document.getElementById("dtArrivalTo").focus();
                return false;
            }
            if (IsInvalidDate($("#dtArrivalTo").val(), strDateFormat)) {
                alert("Please Enter Valid Arrival To Date.");
                document.getElementById("dtArrivalTo").focus();
                return false;
            }

            if (document.getElementById("chkVesselAll").checked == false && document.getElementById("<%= chkVessel.ClientID %>") == null) {
                    alert("Please check atleast one vessel");
                    return false

                }
            if (document.getElementById("chkPortAll").checked == false && document.getElementById("<%= chkPort.ClientID %>")==null) {
                    alert("Please check atleast one port.");
                    return false
                
            }

                if (document.getElementById("chkCountryAll").checked == false && document.getElementById("<%= chkCountry.ClientID %>") == null) {
                    alert("Please check atleast one country.");
                    return false

                }

            if (document.getElementById("chkUserAll").checked == false && document.getElementById("<%= chkUser.ClientID %>") == null) {
                alert("Please check atleast one User.");
               return false
                
            }
            

            return true

        }

        function validateddlPort() {
            if (document.getElementById("ddlPort").value == "0") {
                alert("Please select a port to add.");
                return false;
            }
            return true;
        }
        function validateddlCountry() {
            if (document.getElementById("ddlCountry").value == "0") {
                alert("Please select a Country to add.");
                return false;
            }
            return true;
        }
        function validateddlVessel() {
            if (document.getElementById("ddlVessel").value == "0") {
                alert("Please select a Vessel to add.");
                return false;
            }
            return true;
        }
        function validateddlUser() {
            if (document.getElementById("ddlUser").value == "0") {
                alert("Please select an user to add.");
                return false;
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

    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB; "  >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

        <center>
        <div id="dialog" style="display: none"></div>
           <div style="border: 1px solid #cccccc" class="page-title">
                Port Call Notification Detail
            </div>
            <table border="0" cellpadding="2" cellspacing="2" width="800px">

                <tr>
                    <td align="right" style="width: 39%">
                        Noitification Name &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="100" 
                            CssClass="txtInput"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 39%">
                        Description &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Width="300px" MaxLength="2000" runat="server"></asp:TextBox>
                         </td>
                    <td  style="width: 10%"></td>
                </tr>

                <tr>
                    <td align="right" style="width: 39%">
                        Status &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" CssClass="txtInput">
                        <asp:ListItem Value="2" Text="DRAFT"></asp:ListItem>
                        <asp:ListItem Value="1" Text="ACTIVE"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%">
                    </td>
                </tr>
                <tr>
                   <td align="right">
                        Arrival From &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left"  style="width: 50%">
                        <asp:TextBox ID="dtArrivalFrom" runat="server" Width="100px" MaxLength="100" CssClass="txtInput" onkeydown="return false;"></asp:TextBox>
                        <cc1:CalendarExtender TargetControlID="dtArrivalFrom" ID="cedtArrivalFrom" runat="server">
                        </cc1:CalendarExtender>
                        To:
                      <asp:TextBox ID="dtArrivalTo" runat="server" Width="100px" MaxLength="100" CssClass="txtInput" onkeydown="return false;"></asp:TextBox>
                        <cc1:CalendarExtender TargetControlID="dtArrivalTo" ID="cedtArrivalTo" runat="server">
                        </cc1:CalendarExtender>
                  </td>                                         
                  </tr>
                  <tr>

                    <td style="text-align: Right;">
                      Identify country :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                          <asp:Button ID="btnCountryAdd" runat="server" Text="Add Country" OnClientClick="return validateddlCountry();"
                                        onclick="btnCountryAdd_Click" />
                           <asp:CheckBox ID="chkCountryAll" runat="server" Autopostback="true" Text="Select All" Checked="false" oncheckedchanged="chkCountryAll_CheckedChanged" />
                            <br />
                                             
                                <div id="dvCountry" runat="server" style="float: left; text-align: left; width: 400px; height: 30px;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                    <asp:CheckBoxList ID="chkCountry"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                                   
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td></td>
                  
                  </tr>
                  <tr>

                    <td style="text-align: Right;" >
                      Identify Port :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" class="style1">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList ID="ddlPort" runat="server" Width="200px" CssClass="txtInput">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnPortAdd" runat="server" Text="Add Port" OnClientClick="return validateddlPort();" OnClick="btnPortAdd_Click" />
                                    <asp:CheckBox ID="chkPortAll" runat="server" Autopostback="true" Text="Select All" Checked="false" oncheckedchanged="chkPortAll_CheckedChanged" 
                                        />
                                    <br />
                                    <div id="dvPortList" runat="server"  style="float: left; text-align: left; width: 400px; height: 40px; 
                                        border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                        background-color: #ffffff;">
                                        <asp:CheckBoxList ID="chkPort" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="style1"></td>
                  
                  </tr>
                  <tr>

                    <td style="text-align: Right;">
                      Identify Vessel :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <biv>
                        <asp:DropDownList ID="ddlVessel" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                        <asp:Button ID="btnVesselAdd" runat="server" Text="Add Vessel" OnClientClick="return validateddlVessel();"
                                        onclick="btnVesselAdd_Click" />
                          <asp:CheckBox ID="chkVesselAll" runat="server" Autopostback="true" Text="Select All" Checked="false" oncheckedchanged="chkVesselAll_CheckedChanged"
                                        />
                        <br />
                                             
                                <div id="dvVesselList" runat="server"  style="float: left; text-align: left; width: 400px; height: 40px;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                    <asp:CheckBoxList ID="chkVessel"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                                   
                        </biv>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td></td>
                  
                  </tr>
                   <tr>

                    <td style="text-align: Right;">
                      Identify User :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <biv>
                        <asp:DropDownList ID="ddlUser" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                        <asp:Button ID="btnUserAdd" runat="server" Text="Add User" OnClientClick="return validateddlUser();" onclick="btnUserAdd_Click" />
                         <asp:CheckBox ID="chkUserAll" Autopostback="true" runat="server" Text="Select All" Checked="false" 
                                        oncheckedchanged="chkUserAll_CheckedChanged"></asp:CheckBox>

                        <br />
                                             
                                <div id="dvUserList" runat="server" style="float: left; text-align: left; width: 400px; height: 40px;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                    <asp:CheckBoxList ID="chkUser"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                                   
                        </biv>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td></td>
                  
                  </tr>
                <tr>
                    <td align="right" style="width: 39%">
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                    
                    </td>
                    <td align="left">
                            <asp:HiddenField ID="hdNotificationID" runat="server" />
                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"  Visible="false"></asp:Label>
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
                    <div style="background-color: #d8d8d8; text-align: center">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnsave" runat="server" OnClientClick="return validation();"
                            Text="Save" OnClick="btnsave_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
        </div>
    </div>
        </center>

    </form>
</body>
</html>
