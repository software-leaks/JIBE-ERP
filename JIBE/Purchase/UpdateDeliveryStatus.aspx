<%@ Page Title="Order Movement Status" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="UpdateDeliveryStatus.aspx.cs" Inherits="UpdateDeliveryStatus" %>

<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/ctlCityList.ascx" TagName="ctlCityList" TagPrefix="ucCity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    
    
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />

  
    <style type="text/css">
        
        .style2
        {
            height: 31px;
        }
        .style3
        {
            background-color: #C0C0C0;
            font-weight: 700;
        }
        .style4
        {
            background-color: #E0E4FE;
        }
        .style5
        {
            background-color: #F1F2FE;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function GetAgentDetails(selectedItem, txtid) {

            document.getElementById('hdftxtid').value = txtid;
            Async_Get_SupplierDetails_ByCode(selectedItem.value);


        }
        function response_getSupplierDetails(retval) {
            var ar, arS;

            if (retval.indexOf('Working') >= 0) { return; }
            try {

                retval = clearXMLTags(retval);

                if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
                    alert(retval);
                    return;
                }
                if (retval.trim().length > 0) {
                    var arVal = eval(retval);

                    var txtid = document.getElementById('hdftxtid').value;
                    document.getElementById("ctl00_MainContent_" + txtid).value = String(arVal[0].fullname);
                    document.getElementById("ctl00_MainContent_" + txtid).value += "\r\n" + String(arVal[0].address);
                    document.getElementById("ctl00_MainContent_" + txtid).value += "\r\n" + String(arVal[0].phone);
                    document.getElementById("ctl00_MainContent_" + txtid).value += "\r\n" + String(arVal[0].email);
                }
            }
            catch (ex) { alert(ex.message); }
        }

        function GetForwarderDetails(selectedItem, txtid) {

            document.getElementById('hdftxtid').value = txtid;
            Async_Get_SupplierDetails_ByCode(selectedItem.value);


        }
       
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;color:Black;height: 100%;">

         <div style="background:#CCCCCC;font-weight:bold;text-align:center;height:18px;" >
            Order Movement Status
        </div>
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="1" width="100%" style="vertical-align: top; border: 1px solid #cccccc">
                    <tr>
                       <td style="font-weight: 700;text-align: center;" class="style4">
                            Select
                        </td>
                        <td style="font-weight: 700;text-align: center;" class="style4">
                            Stage
                        </td>
                        <td style="font-weight: 700;" class="style4">
                            Details
                        </td>
                        <td class="style4">
                        </td>
                        <td style="font-weight: 700;text-align: center; font-weight: 700;" class="style4">
                            Location
                        </td>
                        <td style="font-weight: 700;text-align: center; font-weight: 700;" class="style4">
                            Remarks
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px"    class="style4"> 
                            <asp:CheckBox ID="chkSupplier" runat="server" AutoPostBack="True" OnCheckedChanged="chkSupplier_CheckedChanged" />
                        </td>
                        <td class="style4">
                            <table>
                                <tr>
                                    <td class="style4">
                                        Supplier :
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblsupplier"  runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4" valign="top">
                            <asp:TextBox ID="txtsupplierAddress" TextMode="MultiLine" ReadOnly="true" Height="130px"
                                Width="250px" runat="server" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                        <td class="style4">
                            <table cellpadding="2" cellspacing="2">
                                <tr>
                                    <td style="font-weight: 700">
                                        Order Readiness
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtSupplierReadiness" CssClass="textbox-css" Font-Names="Tahoma"
                                            Font-Size="9.5pt" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calOrderReadines" Format="dd-MM-yyyy" TargetControlID="txtSupplierReadiness"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            <table cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="2" style="font-weight: 700">
                                        Current
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City
                                    </td>
                                    <td>
                                        <ucCity:ctlCityList ID="ddlSupplierCurrentCity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-weight: 700">
                                        Estimate delivery
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupplierEstimateDate" CssClass="textbox-css" runat="server" Font-Names="Tahoma"
                                            Font-Size="9.5pt"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtSupplierEstimateDate"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port
                                    </td>
                                    <td>
                                        <uc2:ctlPortList ID="ddlSupplierDeliveryPort" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="txtsupplierRemarks" TextMode="MultiLine" runat="server" Height="130px"
                                Width="300px" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <asp:CheckBox ID="chkForwarder" runat="server" AutoPostBack="True" OnCheckedChanged="chkForwarder_CheckedChanged" />
                        </td>
                        <td class="style5">
                            <table>
                                <tr>
                                    <td>
                                        Forwarder :
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlforwarder" runat="server" Font-Size="11px" Width="220px"
                                            AutoPostBack="false" onChange="GetForwarderDetails(this,'txtForwarderAddress')">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="txtForwarderAddress" ReadOnly="true" TextMode="MultiLine" Height="130px"
                                Width="250px" runat="server" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                        <td class="style5" valign="top">
                            <table cellpadding="1" cellspacing="1">
                                <tr>
                                    <td style="font-weight: 700">
                                        Delivery Type
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="optForwarderDeliveryType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Full" Selected="True" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Partial" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtForwarderDeliveryRemarks" Height="85px" TextMode="MultiLine"
                                            runat="server" Width="180px" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style5">
                            <table cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="2" style="font-weight: 700">
                                        Current
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City
                                    </td>
                                    <td>
                                        <ucCity:ctlCityList ID="ddlforwarderCurrentCity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-weight: 700">
                                        Estimate delivery
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtForwarderEstimateDate" CssClass="textbox-css" runat="server"
                                            Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtForwarderEstimateDate"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port
                                    </td>
                                    <td>
                                        <uc2:ctlPortList ID="ddlForwarderDeliveryPort" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="txtForwarderRemarks" TextMode="MultiLine" runat="server" Height="130px"
                                Width="300px" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px" class="style4">
                            <asp:CheckBox ID="chkAgent" runat="server" AutoPostBack="true" OnCheckedChanged="chkAgent_CheckedChanged" />
                        </td>
                        <td class="style4">
                            <table>
                                <tr>
                                    <td>
                                        Agent :
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlAgent" Font-Size="11px" runat="server" Width="220px" AutoPostBack="false"
                                            onChange="GetAgentDetails(this,'txtAgentdetails')">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="txtAgentdetails" TextMode="MultiLine" ReadOnly="true" Height="130px"
                                Width="250px" runat="server" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                        <td class="style4" valign="top">
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="font-weight: 700">
                                        Delivery Type
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="optAgentDeliveryType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Full" Selected="True" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Partial" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAgentDeliveryRemarks" Height="80px" Width="180px" TextMode="MultiLine"
                                            runat="server" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            <table cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="2" style="font-weight: 700">
                                        Current
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City
                                    </td>
                                    <td>
                                        <ucCity:ctlCityList ID="ddlAgentCurrentCity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-weight: 700">
                                        Estimate Delivery
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAgentEstimateDate" CssClass="textbox-css" runat="server" Font-Names="Tahoma"
                                            Font-Size="9.5pt"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender5" Format="dd-MM-yyyy" TargetControlID="txtAgentEstimateDate"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port
                                    </td>
                                    <td>
                                        <uc2:ctlPortList ID="ddlAgentPortDeliveryOnbaord" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="txtAgentRemarks" Height="130px" TextMode="MultiLine" runat="server"
                                Width="300px" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px" class="style5">
                            <asp:CheckBox ID="chkDelivered" runat="server" AutoPostBack="True" OnCheckedChanged="chkDelivered_CheckedChanged" />
                        </td>
                        <td class="style5">
                            <table>
                                <tr>
                                    <td style="width: 150px">
                                        Awaiting VSL Confirmation :
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDelivered" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style5">
                            <table cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="2" class="style5">
                                        <strong>Delivery </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeliveryDate" CssClass="textbox-css" runat="server" Font-Names="Tahoma"
                                            Font-Size="9.5pt"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calDeliveryDate" Format="dd-MM-yyyy" TargetControlID="txtDeliveryDate"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port
                                    </td>
                                    <td>
                                        <uc2:ctlPortList ID="ddlDeliveredPort" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="3" class="style5" valign="top">
                            <asp:TextBox ID="txtDeliveredRemarks" Height="60px" TextMode="MultiLine" runat="server"
                                Width="670px" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px" class="style4">
                            <asp:CheckBox ID="chkVesselAcknowledged" runat="server" AutoPostBack="True" OnCheckedChanged="chkVesselAcknowledged_CheckedChanged" />
                        </td>
                        <td class="style4">
                            <table>
                                <tr>
                                    <td>
                                        Vessel Acknowledged :
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Label ID="lblVesselAcknowledge" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            <table cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="2">
                                        <strong>Delivery </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVesselAcknowlegedDate" CssClass="textbox-css" runat="server"
                                            Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calVesselAcknowlegedDate" Format="dd-MM-yyyy" TargetControlID="txtVesselAcknowlegedDate"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port
                                    </td>
                                    <td>
                                        <uc2:ctlPortList ID="ddlVesselAcknowledgedPort" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="3" class="style4" valign="top">
                            <asp:TextBox ID="txtVesselAcknowledgeRemarks" Height="60px" TextMode="MultiLine"
                                runat="server" Width="670px" Font-Names="Tahoma" Font-Size="9.5pt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr align="center">
                        <td style="text-align: center">
                            <asp:Button ID="btnSave" Width="100px" Height="35px" Font-Names="Verdana" Font-Size="13px"
                                runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClose" runat="server" Width="100px" Height="35px" Font-Size="13px"
                                Font-Names="Verdana" Text="Cancel" OnClientClick="javascript:window.close();" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <input id="hdftxtid" type="hidden" />
    </div>
</asp:Content>
