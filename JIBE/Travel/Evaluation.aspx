<%@ Page Title="Quotation Evaluation" Language="C#" MasterPageFile="~/Site.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="Evaluation.aspx.cs"
    Inherits="Evaluation" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/notifier.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function resetQuote(reqid, agentid) {
            try {
                if (!confirm("Are you sure to reset the quote date"))
                    return false;
                else
                    TravelService.ResetToQuote(reqid, agentid, onResetforQuotation);
            } catch (ex) { alert(ex.Message); }
        }

        function onResetforQuotation(result) {
            if (result == true) {
                alert("Quote has been resetted successfully");
                self.close();
            }
            else
                alert("error while resetting quote date");
        }

        function ValidateOnSelectOption(obj, id, amt, IsQuoteExpired, evt) {

            if (IsQuoteExpired == 1) {
                $.notifier.broadcast(
	                {
	                    ttl: 'Alert',
	                    msg: 'Deadline has already passed for this ticket.Please confirm with agents.',
	                    skin: 'rounded,red,absolute,' + evt.pageY + ',' + evt.pageX,
	                    duration: 5000
	                }
                    );
            }
            try {

                getOBJ('<%=hdAgentID.ClientID%>').value = id;
                getOBJ('dvTotalAmount').innerHTML = "Total Amount = " + parseFloat(amt);
                getOBJ('<%=hdf_Totalamount.ClientID%>').value = parseFloat(amt);




            }
            catch (ex) { }
        }


        function ValidatonOnApprove() {
            try {

                var selectedAmount = getOBJ('dvTotalAmount').innerHTML.toString().split('=')[1];
                var cheapestAmount = getOBJ('<%=hdf_Cheapest_Totalamount.ClientID%>').value;

                var totQtnCount = getOBJ('<%=hdf_No_of_Quotation.ClientID%>').value;

                var remarks = document.getElementById('<%=txtApproverRemark.ClientID%>').value;

                if (selectedAmount.trim() == "") {
                    alert("Please select a option and then approve.")
                    return false;
                }

                if (totQtnCount > 1) {


                    if ((parseFloat(selectedAmount).toFixed(2) != parseFloat(cheapestAmount).toFixed(2))) {

                        if (remarks.trim() == "") {
                            alert("Please enter remarks for NOT selecting cheapest flight.")
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (ex) { }
        }





        function openAttachments(ReqID) {
            var url = 'Attachment.aspx?atttype=DOCUMENT&requestid=' + ReqID;
            OpenPopupWindow('A2', 'Attachments', url, 'popup', 550, 900, null, null, true, false, true, Attachment_Closed);
        }
        function Attachment_Closed() {
            return true;
        }

        function ShowApproval() {
            showModal('divTrackApproval');
        }



        function ShowCostDetails(divid, evt) {
            $('.costdetails1').hide();
            document.getElementById(divid).style.display = "block";
            SetPosition_Relative(evt, divid);

        }

    </script>
    <style type="text/css">
        .RowStyle-css td
        {
            border: 1px solid #EAEAEA;
        }
        
        
        .AlternatingRowStyle-css td
        {
            border: 1px solid #EAEAEA;
            background-color: #F7FBFB;
        }
        
        .GridView-css
        {
            border: 1px solid black;
            border-collapse: collapse;
        }
        .tdHeader
        {
            font-weight: bold;
            color: Black;
        }
        .btnhyperlink
        {
            text-decoration: underline;
            color: Blue;
            cursor: pointer;
        }
        .costhd
        {
            padding-right: 10px;
            text-align: right;
            font-size: 11px;
            font-weight: bold;
        }
        .costdt
        {
            padding-left: 10px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdAgentID" runat="server" />
    <asp:ScriptManagerProxy ID="_sm1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/TravelService.asmx" />
        </Services>
    </asp:ScriptManagerProxy>
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        <table width="100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Compare Travel Quotations"></asp:Label>
                    :
                    <asp:Literal ID="ltRequestid" runat="server"></asp:Literal>
                </td>
                <td>
                    <span style="background-color: Yellow; height: 25px; padding: 2px 10px 2px 10px">
                        <asp:Label ID="lblSeamanStatus" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
        </table>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px; min-height: 620px; overflow: auto;
        width: 99%">
        <div>
            <asp:Repeater ID="rptParent" DataMember="Request" runat="server" OnItemDataBound="rptParent_OnItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="1px" cellspacing="0px" width="100%" class="GridView-css" style="border-collapse: separate;
                        border: 1px solid gray">
                        <tr class="HeaderStyle-css" style="font-weight: bold; height: 30px">
                            <td style="width: 30px;">
                                &nbsp;
                            </td>
                            <td style="width: 150px;">
                                Vessel Name
                            </td>
                            <td>
                                Route
                            </td>
                            <td style="width: 80px;">
                                Departure Date
                            </td>
                            <td style="width: 100px;">
                                Travel Type
                            </td>
                            <td style="width: 80px;">
                                Quote Due Date
                            </td>
                            <td style="width: 80px;">
                                No. Of Pax
                            </td>
                            <td style="width: 140px;">
                                Requested By
                            </td>
                            <td style="width: 140px;">
                                Attach
                            </td>
                            <td style="width: 40px;">
                                Seaman?
                            </td>
                            <td align="center">
                                Remarks
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="RowStyle-css">
                        <td>
                            <img src="images/plus.png" alt="" name="Expand" onclick='toggleChild(this, <%#DataBinder.Eval(Container.DataItem, "id")%>)' />
                            &nbsp;<%#DataBinder.Eval(Container.DataItem, "id")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "vessel_name")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "FlightRoute")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "departureDate")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "classOfTravel")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "QuoteDueDate")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "PaxCount")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "created_by")%>
                        </td>
                        <td>
                            <center>
                                <img src="images/attach-icon.gif" class="clickable" alt="Attach documents" onclick='openAttachments(<%#Eval("id")%>)' />
                            </center>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "isSeaman")%>
                        </td>
                        <td align="center">
                            <asp:Image ID="imgRemark" Visible='<%#DataBinder.Eval(Container.DataItem, "remarks").ToString() ==""? false : true %>'
                                runat="server" ImageUrl="images/remarks.gif" />
                        </td>
                    </tr>
                    <tr style="display: none; font-weight: bold; background-color: #C1D5F8">
                        <td style="background-color: white">
                            &nbsp;
                        </td>
                        <td>
                            <span>Code</span>
                        </td>
                        <td>
                            <span>Sur Name</span>
                        </td>
                        <td>
                            <span>Given Name</span>
                        </td>
                        <td>
                            <span>Rank</span>
                        </td>
                        <td>
                            Nationality
                        </td>
                        <td>
                            <span>DOB</span>
                        </td>
                        <td>
                            <span>Place Of Birth</span>
                        </td>
                        <td>
                            Personal?
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <asp:Repeater ID="rptChild" runat="server" OnItemCommand="rptChild_ItemCommand" OnItemDataBound="rptChild_OnItemDataBound"
                        DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestPax") %>'>
                        <ItemTemplate>
                            <tr style="text-align: left;display:none" class='RowStyle-css child<%#((System.Data.DataRow)Container.DataItem)["requestid"]%>'>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <a href='../crew/crewdetails.aspx?id=<%# ((System.Data.DataRow)Container.DataItem)["staffid"]%>'
                                        target="_blank">
                                        <%# ((System.Data.DataRow)Container.DataItem)["staff_code"]%>
                                    </a>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["staff_surname"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["staff_name"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["CurrentRank"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Nationality"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Birth_Date"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Born_Place"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["isPersonalTicket"]%>
                                </td>
                                <td colspan="2">
                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Remove Pax" BorderWidth="0px"
                                        Visible="false" CommandName="REMOVEPAX" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem)["id"]%>'
                                        CssClass="clickable" ImageUrl="~/images/delete.gif" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <br />
        <asp:UpdatePanel ID="updQuotes" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnRefreshpage" runat="server" OnClick="btnRefreshpage_Click" BackColor="Transparent"
                    BorderStyle="None" Font-Bold="true" Font-Size="14px" Font-Underline="true" Text="Quotes :" />
                <div id="dvmaingrid">
                    <asp:Repeater ID="rptQuotes" DataMember="dtQuotes" runat="server" OnItemCommand="rptQuotes_ItemCommand"
                        OnItemDataBound="rptQuotes_OnItemDataBound">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" class="GridView-css">
                                <tr class="HeaderStyle-css" style="height: 35px">
                                    <th style="width: 15%">
                                        Agent / GDS / Deadline
                                    </th>
                                    <th style="width: 50%">
                                        <table width="100%" style="border-collapse: collapse">
                                            <tr>
                                                <td style="width: 20%">
                                                    Airline Locator
                                                </td>
                                                <td style="width: 20%">
                                                    From / Departure Date
                                                </td>
                                                <td style="width: 20%">
                                                    To / Arrival Date
                                                </td>
                                                <td style="width: 16%">
                                                    Flight
                                                </td>
                                                <td style="width: 10%">
                                                    Class
                                                </td>
                                                <td style="width: 10%">
                                                    Status
                                                </td>
                                                <td style="width: 4%">
                                                </td>
                                            </tr>
                                        </table>
                                    </th>
                                    <th style="width: 8%; text-align: center">
                                        Cost<br />
                                    </th>
                                    <th style="width: 15%">
                                        <asp:Button ID="btnReworkToTravelPIC" Text="Rework to Travel PIC" Height="25px" OnClientClick="showModal('dvreworkToTravelPIC')"
                                            BackColor="#ADD8E6" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" Font-Size="10px"
                                            BorderColor="gray" Font-Bold="true" Font-Names="verdana" runat="server" Visible='<%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || objUA.Edit == 0 || IsApproving !=1?false:true %>' />

                                        <asp:Button ID="btnSendforApproval" Text="Send for aproval" Height="25px" OnClick='<%# "asyncGet_Quote_Count_Approval(" + Request.QueryString["requestid"]+ ",&#39;dvSendForApprovalPIC&#39;);return false;" %>'
                                            BackColor="#ADD8E6" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" Font-Size="10px"
                                            BorderColor="gray" Font-Bold="true" Font-Names="verdana" runat="server" Visible='<%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || objUA.Edit == 0 || IsApproving==1?false:true %>' />
                                    </th>
                                    <th style="width: 15%">
                                        <asp:CheckBox ID="chkSEndForApprovalAll" Text="Show All To Approver" onclick='<%#"asyncUPD_Quote_Send_For_Approval(0,this,"+Request.QueryString["requestid"]+","+Session["userid"].ToString()+")" %>'
                                            runat="server" Visible='<%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || objUA.Edit == 0 || IsApproving==1?false:true %>' />
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="datarow" runat="server">
                                <td style="width: 15%; border-left: 1px solid #666666; vertical-align: top">
                                    <table width="100%" style="border-collapse: collapse" cellpadding="5">
                                        <%--  <tr>
                                            <td style="" colspan="2">
                                                <asp:Label ID="lblOption" runat="server" Font-Bold="true" ForeColor="#8A2BE2" Text='<%# "Option: " + Eval("rowid").ToString()  %>'></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="2" style="color: #8A2BE2; font-weight: bold">
                                                <%#DataBinder.Eval(Container.DataItem, "QuotedBy")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdHeader">
                                                GDS Locator
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "GDSLocator")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdHeader">
                                                Deadline
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "TicketingDeadline").ToString()%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%;" valign="top">
                                    <table width="100%" style="border-collapse: collapse" cellpadding="5px">
                                        <asp:Repeater ID="rptFlights" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestQuotes") %>'
                                            OnItemDataBound="rptFlights_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr style="text-align: left;" class='child<%#((System.Data.DataRow)Container.DataItem)["id"]%>'>
                                                    <td style="width: 20%">
                                                        <%# ((System.Data.DataRow)Container.DataItem)["AirlineLocator"]%>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <span style="color: #006161;font-weight:bold;cursor:default" title='<%# "cssbody=[dvbdy1] cssheader=[dvhdr1] header=["+((System.Data.DataRow)Container.DataItem)["FromAirportName"].ToString()+ "]" %>' >
                                                            <%# ((System.Data.DataRow)Container.DataItem)["TravelFrom"]%>
                                                        </span>
                                                        <br />
                                                        
                                                        <%# ((System.Data.DataRow)Container.DataItem)["DepartureDate"]%>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <b style="color: #006161">
                                                            <span style="color: #006161;font-weight:bold;cursor:default" title='<%# "cssbody=[dvbdy1] cssheader=[dvhdr1] header=["+((System.Data.DataRow)Container.DataItem)["ToAirportName"].ToString()+ "]" %>' >
                                                            <%# ((System.Data.DataRow)Container.DataItem)["TravelTo"]%>
                                                            </span>
                                                            
                                                            <br />
                                                        </b>
                                                        <%# ((System.Data.DataRow)Container.DataItem)["ArrivalDate"]%>
                                                    </td>
                                                    <td style="width: 16%">
                                                        <%# ((System.Data.DataRow)Container.DataItem)["FlightName"]%>&nbsp;[<%# ((System.Data.DataRow)Container.DataItem)["FlightNo"]%>]
                                                    </td>
                                                    <td style="width: 10%">
                                                        <%# ((System.Data.DataRow)Container.DataItem)["TravelClass"]%>
                                                    </td>
                                                    <td style="width: 10%">
                                                        <%# ((System.Data.DataRow)Container.DataItem)["FlightStatus"]%>
                                                    </td>
                                                    <td style="width: 4%" align="center">
                                                        <asp:Image ID="imgFlightRemark" runat="server" Visible='<%#((System.Data.DataRow)Container.DataItem)["remarks"].ToString() ==""? false : true %>'
                                                            ImageUrl="images/remarks.gif" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                                <td style="width: 8%; text-align: center; border-collapse: collapse; line-height: 25px;">
                                    <b style="color: Red; font-size: 14px">$ &nbsp;
                                        <asp:Label ID="lblGrandTotal_usd" Text='<%#Eval("USD_Total_Amount")%>' runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <asp:HyperLink ID="viewdetails" runat="server" Text="Cost Details" ForeColor='<%# Eval("IsOtherCharges").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'
                                        NavigateUrl="#" Style="font-size: 11px; font-family: Tahoma;" onmouseout="javascript:$('.costdetails1').hide();"
                                        onmousemove='<%# "ShowCostDetails("+ Eval("rowid").ToString()+",event);" %>'></asp:HyperLink>
                                    <br />
                                    <asp:Button ID="hyplnkAdditionchange" Style="font-size: 11px; font-family: Tahoma"
                                        CssClass="btnhyperlink" BorderStyle="None" BackColor="Transparent" Enabled='<%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || objUA.Edit == 0?false:true %>'
                                        runat="server" Text="Additional" OnClick='<%# "OpenPopupWindowBtnID(&#39;POP__Addtitinal_Details&#39;, &#39;Additional Charges for "+Eval("short_name").ToString() +" for option "+ Eval("rowid").ToString() +"&#39;, &#39;Quotation_Additional_Charge.aspx?QuotationRequest_ID=" + Eval("id").ToString() +"&#39;,&#39;popup&#39;,400,920,null,null,false,false,true,false,&#39;"+ btnRefreshpage.UniqueID+"&#39;);return false;" %>' />
                                    <div id='<%# Eval("rowid").ToString() %>' style="display: none; position: absolute;
                                        width: 230px" class="costdetails1">
                                        <div style="background-color: transparent; height: 10px; width: 30px; text-align: left;
                                            empty-cells: hide; margin: 0px; padding: 0px; background-image: url(../Images/arrowPopup.png);
                                            background-repeat: no-repeat; vertical-align: top">
                                        </div>
                                        <div style="border: 4px solid #FFA500; background-color: #F5F5F5; border-radius: 5px">
                                            <table cellspacing="0" cellpadding="0px" style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: left; font-weight: bold; border-bottom: 2px solid #FFA500;">
                                                        Cost Details
                                                    </td>
                                                    <td align="right" style="border-bottom: 2px solid #FFA500;">
                                                        <img style="height: 14px; width: 14px" alt="close" src="../Images/Close.gif" onclick="javascript:$('.costdetails1').hide();" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Currency
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "currency").ToString()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Fare
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "Fare")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Tax
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "Tax")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Amount / pax
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "TotalAmount").ToString()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Total
                                                    </td>
                                                    <td class="costdt">
                                                        <asp:Label ID="lblGrandTotal" Text="" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Baggage allowances
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "Baggage_Charge").ToString()%>
                                                        KG
                                                        <asp:Label ID="lblUSDRate" Text="" Visible="false" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Date-Change penalty
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "Date_Change_Charge").ToString()%>
                                                        <asp:Label ID="Label1" Text="" Visible="false" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Cancellation Charges
                                                    </td>
                                                    <td class="costdt">
                                                        <%#DataBinder.Eval(Container.DataItem, "Cancellation_Charge").ToString()%>
                                                        <asp:Label ID="Label2" Text="" Visible="false" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="costhd">
                                                        Additional Charges
                                                    </td>
                                                    <td class="costdt">
                                                        USD &nbsp;
                                                        <%#Eval("Add_Charge")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 15%; line-height: 30px; border-collapse: collapse; text-align: center">
                                    <asp:Button ID="imgReset" Text="Rework to Agent" OnClientClick='return confirm("Sure to reset it ?")'
                                        BackColor="#ADD8E6" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" Font-Size="10px"
                                        BorderColor="gray" Font-Bold="true" Font-Names="verdana" runat="server" CommandName="RESETQUOTE"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>' Visible='<%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || objUA.Edit == 0 || IsApproving==1?false:true %>' />
                                    <br />
                                    &nbsp; <a class="clickable" title="cssbody=[dvbdy1] cssheader=[dvhdr1] header=[  <%#DataBinder.Eval(Container.DataItem, "Remarks").ToString()%> ]"
                                        <%#DataBinder.Eval(Container.DataItem, "Remarks").ToString().Trim()=="" ?"style='display:none;'":"style='color: Blue; font-size: 10px; cursor: pointer;text-decoration: underline; font-weight: bold'" %>>
                                        View Remark </a>
                                </td>
                                <td style="width: 15%; line-height: 30px; border-collapse: collapse; border-right: 1px solid #666666">
                                    <input type="radio" id="rdQuote" onclick='ValidateOnSelectOption(this, <%#DataBinder.Eval(Container.DataItem, "AgentID")%>, "<%#Eval("USD_Total_Amount").ToString()%>","<%#Eval("IsQuoteExpired").ToString()%>",event)'
                                        value='<%#DataBinder.Eval(Container.DataItem, "id").ToString()+","+Eval("Supplier").ToString()%>' name="rdQuote" style="border: 0px;"
                                        <%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || UDFLib.ConvertToInteger(Eval("VerifSts")) ==1 ?"checked =checked":"" %> <%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 ?"disabled =disabled":"" %> /><b
                                            style="color: blue">Select to approve </b>
                                    <br />
                                    <asp:CheckBox ID="chkSEndForApproval" Text="Show to Approver" onclick='<%#"asyncUPD_Quote_Send_For_Approval("+DataBinder.Eval(Container.DataItem, "id").ToString()+",this,0,"+Session["userid"].ToString()+")" %>'
                                        Checked='<%# Eval("Sent_For_Approval").ToString()=="1"?true:false %>' runat="server"
                                        Visible='<%#UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || objUA.Edit == 0 || IsApproving==1?false:true %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="border-bottom: 1px solid #666666;">
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div id="dvTotalAmount" style="float: right; padding-right: 30px; font-size: 18px;
            font-weight: bold; color: Red;">
        </div>
        <asp:HiddenField ID="hdf_Totalamount" runat="server" />
        <asp:HiddenField ID="hdf_Cheapest_Totalamount" runat="server" />
        <asp:HiddenField ID="hdf_No_of_Quotation" runat="server" />
        <b style="color: Black">Approver's Remarks</b>
        <asp:TextBox ID="txtApproverRemark" Width="98%" runat="server" Font-Size="12px" Font-Names="Tohama"
            TextMode="MultiLine">
        </asp:TextBox>
        <br />
        <br />
        <center>
            <asp:Button ID="cmdApprove" runat="server" Height="35px" Width="150px" Text="Approve"
                Font-Size="12px" Font-Names="Tahoma" OnClick="cmdApprove_Click" OnClientClick="return ValidatonOnApprove();" />
            <%--  <a href="#" onclick="ShowApproval()">Track Approval</a>--%>
        </center>
        <hr />
        <div id="divTrackApproval" title="Approval History" style="display: block; color: black">
            <b>Approval History :</b><br />
            <br />
            <asp:GridView ID="gvApprovals" runat="server" CellPadding="2" CellSpacing="0" AutoGenerateColumns="true">
                <HeaderStyle CssClass="HeaderStyle-css" Font-Size="10px" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            </asp:GridView>
        </div>
    </div>
    <div id="divSendForApproval" title="Send for Approval" style="width: 500px; display: none">
        <table cellpadding="2" width="500px" cellspacing="0">
            <tr>
                <td colspan="2" style="color: #CC3E3E; font-size: 11px; text-align: left; font-family: Verdana">
                    Cost of tickets selected for approval is beyond your limit.Please forward to one
                    of the following for approval.
                </td>
            </tr>
            <tr>
                <td style="width: 23%; text-align: right; font-size: 11px; font-weight: bold; border-top: 1px solid gray;
                    padding-top: 8px; font-family: Verdana">
                    Select Approver :
                </td>
                <td style="text-align: left; width: 77%; border-top: 1px solid gray; padding-top: 8px">
                    <asp:ListBox ID="lstUserList" runat="server" DataTextField="UserName" DataValueField="UserID"
                        Height="150px" ValidationGroup="apr" Width="99%" Font-Size="12px" Font-Names="verdana">
                    </asp:ListBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="apr"
                        runat="server" ControlToValidate="lstUserList" InitialValue="0" ErrorMessage="Please select approver !"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 23%; text-align: right; font-size: 11px; font-family: Verdana;
                    font-weight: bold">
                    Remark :
                </td>
                <td style="width: 77%">
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="99%" Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; padding-top: 5px">
                    <asp:Button ID="btnSendForApproval" runat="server" Height="30px" Text="Send For Approval"
                        ValidationGroup="apr" OnClick="btnSendForApproval_Click" />
                    <asp:Button ID="btnSendForApprovalCancel" Height="30px" runat="server" Text="Close" />
                </td>
            </tr>
        </table>
    </div>

    <div id="dvreworkToTravelPIC" title="Rework To Travel PIC" style="width: 400px; display: none">
        <table width="100%">
            <tr>
                <td style="font-weight: bold">
                    Remark :
                </td>
                <td>
                    <asp:TextBox ID="txtReworkRemark" runat="server" Height="80px" TextMode="MultiLine"
                        Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnReworkPIC" runat="server" Height="25px" Text="Rework to Travel PIC"
                        OnClick="btnReworkPIC_Click" />
                    &nbsp;
                    <asp:Button ID="btnCloserework" runat="server" Text="Close" Height="25px" OnClientClick="hideModal('dvreworkToTravelPIC');return false;" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvSendForApprovalPIC" title="Send for Approval" style="width: 500px; display: none">
        <asp:UpdatePanel ID="Update_Approval" runat="server">
            <ContentTemplate>
                <table cellpadding="4" width="500px" cellspacing="0">
                    <tr>
                        <td colspan="2" style="width: 100%; text-align: center; font-size: 11px; font-weight: bold;
                            border-top: 1px solid gray; padding-top: 8px; font-family: Tahoma; color: Black">
                            Managerial Approval :
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%; text-align: left; border-top: 1px solid gray;
                            padding-top: 8px">
                            <asp:ListBox ID="lstMngApprList" runat="server" DataTextField="UserName" DataValueField="UserID"
                                Height="150px" ValidationGroup="apr" Width="99%" Font-Size="12px" Font-Names="verdana">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%; text-align: center; font-size: 11px; font-weight: bold;
                            border-top: 1px solid gray; padding-top: 8px; font-family: Tahoma; color: Black">
                            PO Approval :
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%; text-align: left; border-top: 1px solid gray;
                            padding-top: 8px">
                            <asp:ListBox ID="ListBoxPOApprover" runat="server" DataTextField="UserName" DataValueField="UserID"
                                Height="150px" ValidationGroup="apr" Width="99%" Font-Size="12px" Font-Names="verdana">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; font-size: 11px; font-family: Tahoma; color: Black;
                            font-weight: bold">
                            Remark :
                        </td>
                        <td style="width: 85%">
                            <asp:TextBox ID="txtSendForAppRemark" runat="server" TextMode="MultiLine" Font-Names="Tahoma"
                                Width="98%" Height="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 5px">
                            <asp:Label ID="lblMessageOnSendApproval" runat="server" Font-Size="12px" ForeColor="Red"
                                Font-Bold="true" BackColor="Yellow"></asp:Label><br />
                            <asp:Button ID="btnSendForApprovaetByPIC" runat="server" Height="30px" Text="Send For Approval"
                                OnClick="btnSendForApprovalByPIC_Click" OnClientClick=" return CheckForPendingManagerApproval(id)" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
