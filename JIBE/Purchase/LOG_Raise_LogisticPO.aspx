<%@ Page Title="Logistic PO" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LOG_Raise_LogisticPO.aspx.cs" Inherits="Purchase_LOG_Raise_LogisticPO" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="uc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" Namespace="RJS.Web.WebControl"
    TagPrefix="RJS" %>
<asp:Content ID="Contenthead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../CSS/StyleSheetMsg.css" rel="stylesheet" type="text/css" />
   <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script type="text/javascript">

        function CloseDiv() {

            var control = document.getElementById("ctl00_MainContent_divApprove");
            control.style.visibility = "hidden";
        }

        function CloseDiv2() {

            var control = document.getElementById("ctl00_MainContent_divpurcomment");
            control.style.visibility = "hidden";
            document.getElementById("ctl00_MainContent_btnSendOrder").disabled = false;
        }

        function Validation() {

            var DDLPort = document.getElementById("ctl00_MainContent_DDLPort").value;
            var txtdlvins = document.getElementById("ctl00_MainContent_txtdlvins").value;
            var txteta = document.getElementById("ctl00_MainContent_txteta").value;
            var txtetd = document.getElementById("ctl00_MainContent_txtetd").value; ;

            var txtETATime = document.getElementById("ctl00_MainContent_txtETAAPPM").value; ;
            var txtETDTime = document.getElementById("ctl00_MainContent_txtETDAMPM").value; ;


            var dt1 = parseInt(txteta.substring(0, 2), 10);
            var mon1 = parseInt(txteta.substring(3, 5), 10);
            var yr1 = parseInt(txteta.substring(6, 10), 10);

            var dt2 = parseInt(txtetd.substring(0, 2), 10);
            var mon2 = parseInt(txtetd.substring(3, 5), 10);
            var yr2 = parseInt(txtetd.substring(6, 10), 10);


            var ArrivalDt = new Date(yr1, mon1, dt1);
            var DepartureDate = new Date(yr2, mon2, dt2);

            if (txteta != "") {
                if (txtetd == "") {
                    alert("Please select date for vessel ETD.");
                    return false;
                }
            }
            if (txtetd != "") {
                if (txteta == "") {
                    alert("Please select date for vessel ETA.");
                    return false;
                }
            }
            if (txteta != "" && txtetd != "") {
                if (ArrivalDt > DepartureDate) {
                    alert("Vessel ETA can't be before of vessel ETD.");
                    return false;
                }
            }

            if (txtETATime.split(":")[0] > 24 || txtETATime.split(":")[1] > 60 || txtETDTime.split(":")[0] > 24 || txtETDTime.split(":")[1] > 60) {
                alert("Please enter correct time");
                return false;
            }

        }

        function BlockEnterAndAllowtime(varNumericFlag) {
            if (varNumericFlag == 1) {
                if (event.keyCode == 58) {
                    return true;
                }
                else if (event.keyCode < 48 || event.keyCode > 57) {
                    event.keyCode = null; return false;
                }
                else {
                    return true;
                }
            }
            else {
                if (event.keyCode == 13) {
                    event.keyCode = null; return false;
                }
                else
                    return true;
            }
        }

        function GetAgentDetails(selectedItem) {
            Async_Get_SupplierDetails_ByCode(selectedItem.value);


        }
        function response_getSupplierDetails(retval) {
            debugger;
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

                    document.getElementById("ctl00_MainContent_txtagent").value = String(arVal[0].fullname);
                    document.getElementById("ctl00_MainContent_txtagent").value += "\r\n" + String(arVal[0].address);
                    document.getElementById("ctl00_MainContent_txtagent").value += "\r\n" + String(arVal[0].phone);
                    document.getElementById("ctl00_MainContent_txtagent").value += "\r\n" + String(arVal[0].email);
                    document.getElementById("ctl00_MainContent_lblOnerCharts").value = "";
                }
            }
            catch (ex) { alert(ex.message); }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <asp:Label ID="lblheader" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center">
                            <div class="freezing" style="margin-left: 0px; width: 100%;">
                                <asp:Label ID="lblmsgt" ForeColor="Red" runat="server"></asp:Label>
                                <br />
                                <asp:GridView ID="gvLPOList" runat="server" AutoGenerateColumns="false" GridLines="Horizontal"
                                    Width="800px" RowStyle-Wrap="true" DataKeyNames="order_code">
                                    <Columns>
                                        <asp:BoundField HeaderText="Vesse Name" DataField="vessel_name" />
                                        <asp:TemplateField HeaderText="Order Preview">
                                            <ItemTemplate>
                                                <a href="LOG_PO_Preview.aspx?ORDER_CODE=<%#Eval("order_code")%>&LOG_ID=<%# Request.QueryString["LOG_ID"] %>"
                                                    target="_blank">
                                                    <%# Eval("order_code")%>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectLPO" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="0" style="width: 100%; background-color: #f4ffff">
                    <tr align="center">
                        <td>
                            <asp:Button ID="btnSendOrder" runat="server" OnClick="btnSendOrder_Click" Height="30px"
                                Style="font-size: small" Text="Send PO" Width="149px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblError" Width="500px" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
            <div id="dvRaiselPo" style="width: 800px; position: absolute; top: 20%; left: 30%;
                padding: 15px; border: 1px solid black; top: 20%; z-index: 2; color: black;"
                runat="server" class="popup-css">
                <table border="1" cellspacing="0" style="width: 100%; border-collapse: collapse">
                    <tr>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr align="center">
                                    <td style="background-color: #808080; font-size: small; color: #FFFFFF; font-weight: bold;">
                                        PO Details
                                    </td>
                                    <td align="right" style="width: 16px; background-color: #808080;">
                                        <img src="Image/Close.gif" alt="Click to close." width="12px" height="12px" onclick="JavaScript:CloseDiv2();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                  
                    <tr>
                        <td>
                            <table style="width: 507px; height: 155px;" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="2">
                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Vessel ETA:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txteta" runat="server" Style="font-size: small" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtenderETA" TargetControlID="txteta" Format="dd/MM/yyyy"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                        <asp:TextBox ID="txtETAAPPM" Width="50px" Text="00:00" Style="font-size: small" runat="server"
                                            MaxLength="5"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtETAAPPM" Mask="99:99"
                                            AcceptAMPM="false" AcceptNegative="None" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                            MessageValidatorTip="true" runat="server" ErrorTooltipEnabled="true">
                                        </cc1:MaskedEditExtender>
                                        (hh:mm)24 Hours
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Vessel ETD:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtetd" runat="server" Style="font-size: small" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtenderETD" Format="dd/MM/yyyy" TargetControlID="txtetd"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                        <asp:TextBox ID="txtETDAMPM" Width="50px" Style="font-size: small" Text="00:00" runat="server"
                                            MaxLength="5"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtETDAMPM" Mask="99:99"
                                            AcceptAMPM="false" AcceptNegative="None" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                            MessageValidatorTip="true" runat="server" ErrorTooltipEnabled="true">
                                        </cc1:MaskedEditExtender>
                                        (hh:mm)24 Hours
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Delivery Port:
                                    </td>
                                    <td style="color: #FF0000; font-size: small;" align="left">
                                        <asp:DropDownList ID="DDLPort" runat="server" DataTextField="Port_Name" DataValueField="Id"
                                            Style="font-size: 12px" Width="194px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700; text-align: right; width: 120px">
                                        Delivery Instructions:
                                    </td>
                                    <td style="color: #FF0000; font-size: small;" align="left">
                                        <asp:TextBox ID="txtdlvins" runat="server" Height="40px" TextMode="MultiLine" Width="500px"
                                            MaxLength="1000" Style="font-size: small"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Agent Details:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSentFrom" Font-Size="12px" runat="server" Width="220px"
                                            AutoPostBack="false" onChange="GetAgentDetails(this)">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox ID="txtagent" runat="server" Height="80px" Style="font-size: small"
                                            TextMode="MultiLine" Width="500px" MaxLength="1000"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblOnerCharts" ForeColor="Red" Font-Size="10px" Font-Names="verdana"
                                            runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Other Remarks:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtremark" runat="server" Height="80px" TextMode="MultiLine" Width="500px"
                                            MaxLength="1000" Style="font-size: small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtnIncludeAmount" Text="Send PO with value" Checked="true"
                                            ForeColor="Black" GroupName="poamount" runat="server" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rbtnexcludeAmount" Text="Send PO without value" GroupName="poamount"
                                            ForeColor="Black" runat="server" />
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSendPO" runat="server" OnClick="btnSendPO_click" Height="30px"
                                            Style="font-size: small" Text="Save &amp; Continue.." />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btncancelpo" style="font-size: small; height: 30px" runat="server" OnClick="btncancelpo_Click"
                                            Text="Cancel" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2" style="color: #FF0000; font-size: small;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
