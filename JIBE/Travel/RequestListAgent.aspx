<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RequestListAgent.aspx.cs"
    Inherits="RequestListAgent" EnableEventValidation="false" Title="Travel Request List" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <style type="text/css">
        .ob_iLboICBC li
        {
            float: left;
            width: 150px;
        }
        /* For IE6 */* HTML .ob_iLboICBC li
        {
            -width: 135px;
        }
        * HTML .ob_iLboICBC li b
        {
            width: 135px;
            overflow: hidden;
        }
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        var lo;

        function selMe(src) {
            try {
                var o;
                var p;
                if (src) {
                    o = document.getElementById(src);
                }
                else {
                    o = window.event.srcElement;
                }
                p = o.parentElement.parentElement;
                p.className = 'ih';
                if (lo) {
                    if (lo.id != p.id) {
                        lo.className = '';
                        lo = p;
                    }
                }
                else {
                    lo = p;
                }
            }
            catch (ex) { }
        }
    </script>
    <script type="text/javascript" language="javascript">

        var vpaxnamehtml = "";


        function bindPaxsName() {
            $('.PaxCount-overout').mouseover(function (evt) { var Reqid = $(this).find('#hdfReqID').val(); GetPaxsName(Reqid); $('#dvGetPaxName').show(); SetPosition_Relative(evt, 'dvGetPaxName'); }).mouseout(function () { $('#dvGetPaxName').hide(); });
        }


        function openAttachments(ReqID, AgentID, SUPPLIER_ID, DocType, ReqSts) {

            var lReqSts = "NOTAPPROVED";
            if (ReqSts != "QUOTE SENT" && ReqSts != "RFQ RECEIVED" && ReqSts != "" && ReqSts != "REWORK RECEIVED") {
                lReqSts = "APPROVED";
            }

            var url = 'Attachment.aspx?atttype=' + DocType + '&requestid=' + ReqID + '&AgentID=' + AgentID + '&REQSTS=' + lReqSts + '&SUPPLIER_ID=' + SUPPLIER_ID;
            document.getElementById("iFrmPopup").src = url;
            showModal('dvPopUp', true, Attachment_Closed);
        }

        function Attachment_Closed() {
            return true;
        }

        function openIssueTicket(ReqID, SUPPLIER_ID) {
            var url = 'IssueTicket.aspx?requestid=' + ReqID + '&SUPPLIER_ID=' + SUPPLIER_ID;

            OpenPopupWindow('IssueTicketID', 'Issue Ticket', url, 'popup', 700, 1200, null, null, true, false, true, IssueTicket_Closed);

        }

        function IssueTicket_Closed() {

            return true;
        }


        function openRemarks(ReqID) {
            var url = 'Remarks.aspx?requestid=' + ReqID;

            document.getElementById("iFrmPopup").src = url;
            showModal('dvPopUp', true, Attachment_Closed);

        }
        function Remarks_Closed() {

            return true;
        }

        var glreqstid, glctlid;

        function GetPaxsName_OnConfirm(rqstid, ctlID) {
            glreqstid = rqstid;
            glctlid = ctlID;

            TravelService.GetPaxsName(rqstid, onGetPaxNames_OnConfirm);
        }


        function onGetPaxNames_OnConfirm(PaxNameresult) {
            //            debugger;
            vpaxnamehtml = PaxNameresult;
            OnTravelledFlagUpdate(glctlid, glreqstid);
            return false;

        }


        function GetPaxsName(rqstid) {

            TravelService.GetPaxsName(rqstid, onGetPaxNames);
        }


        function onGetPaxNames(PaxNameresult) {

            vpaxnamehtml = PaxNameresult

            var dvGetPaxName = document.getElementById("dvGetPaxName");
            dvGetPaxName.innerHTML = PaxNameresult;

        }


        function OnTravelledFlagUpdate(id, rqstid) {


            // GetPaxsName(rqstid);


            $.alerts.okButton = " Yes ";
            $.alerts.cancelButton = " No ";

            var strMsg = "You are selected to mark person - travelled for the following !" + "\n\n"
                                         + vpaxnamehtml + "\n"
                                         + "Once marking, you will not be able to modify same." + "\n"
                                         + "Do you want to continue ?";

            //            var strMsg = "You are selected to mark person - travelled" + "\n\n"
            //                             + "Once marking, you will not be able to modify same." + "\n"
            //                             + "Do you want to continue ?";


            var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

                if (r) {

                    var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                    window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;

                }
                else {
                    return false;
                }
            }

            );

            return false;
        }



    </script>
    <style type="text/css">
        .rpt-tdd-text
        {
            padding-left: 2px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <input type="hidden" id="hdRequestID" name="hdRequestID" />
     <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        <table width="100%">
            <tr>
                <td align="center" style="width: 80%">
                    <div>
                        <asp:Label ID="lblPageTitle" runat="server" Text="Travel Requests"></asp:Label>
                    </div>
                </td>
                <td align="right" style="background-color: Yellow">
                    <a href="Supplier_HelpFile.htm" target="_blank">How to submit your quotation ? </a>
                </td>
            </tr>
        </table>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px; min-height: 620px; overflow: auto;">
        <asp:ScriptManagerProxy ID="smp1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/TravelService.asmx" />
            </Services>
        </asp:ScriptManagerProxy>
        <div id="dvGetPaxName" class="draggable" style="position: absolute; background-color: #f8f8f8;
            display: none; border: 1px solid gray;">
            <br />
        </div>
        <table>
            <tr>
                <td style="width: 80%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        Vessel Manager
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVessel_Manager" runat="server" Width="156px" OnSelectedIndexChanged="ddlVessel_Manager_SelectedIndexChanged"
                                            AutoPostBack="true" >
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Sector
                                    </td>
                                    <td>
                                        From&nbsp;<asp:TextBox ID="txtSectorFrom" runat="server" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="ACEFrom" TargetControlID="txtSectorFrom" CompletionSetCount="10"
                                            MinimumPrefixLength="2" ServiceMethod="GetAirportList" ContextKey="" CompletionInterval="200"
                                            ServicePath="~/TravelService.asmx" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        To&nbsp;<asp:TextBox ID="txtSectorTo" runat="server" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="ACETo" TargetControlID="txtSectorTo" CompletionSetCount="10"
                                            MinimumPrefixLength="2" ServiceMethod="GetAirportList" ContextKey="" CompletionInterval="200"
                                            ServicePath="~/TravelService.asmx" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="cmdAllRequest" Width="120px" BackColor="LightSkyBlue" runat="server"
                                    Text="All Requests" OnClick="cmdAllRequest_Click" />&nbsp;<asp:Button ID="cmdQuoteSent"
                                        Width="120px" BackColor="White" runat="server" Text="Quote Sent" OnClick="cmdQuoteSent_Click" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fleet
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbFleet" runat="server" AutoPostBack="true" DataSourceID="objFleet"
                                            Width="150px" DataTextField="name" AppendDataBoundItems="true" DataValueField="code"
                                            OnSelectedIndexChanged="cmbFleet_SelectedIndexChanged">
                                            <asp:ListItem Text="-Select All-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Trave Date
                                    </td>
                                    <td>
                                        From&nbsp;<asp:TextBox ID="txtTrvDateFrom" runat="server" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="TrvDateFrom" runat="server" TargetControlID="txtTrvDateFrom"
                                            Format="dd-MMM-yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        To&nbsp;<asp:TextBox ID="txtTrvDateTo" runat="server" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="TrvDateTo" runat="server" TargetControlID="txtTrvDateTo"
                                            Format="dd-MMM-yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="cmdPendingQuote" Width="120px" BackColor="White" runat="server" Text="Pending Quote"
                                    OnClick="cmdPendingQuote_Click" />&nbsp;<asp:Button Visible="false" ID="cmdQuoteApproved"
                                        Width="120px" BackColor="White" runat="server" Text="Quote Approved" OnClick="cmdQuoteApproved_Click" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vessel
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbVessel" runat="server" AutoPostBack="true" DataSourceID="objVessel"
                                            Width="150px" DataTextField="Vessel_Name" DataValueField="Vessel_id" OnDataBound="cmbVessel_OnDataBound"
                                            OnSelectedIndexChanged="cmbVessel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Name/Staff Code/Req ID
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPaxName" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td colspan="2" style="text-align: center" rowspan="2" valign="bottom">
                                        <asp:Button ID="btnSearchRequest" runat="server" Height="35px" Width="80px" Text="Search"
                                            OnClick="btnSearchRequest_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Requested By :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRequestedBy" runat="server" DataTextField="UserName" DataValueField="UserID">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Quoted By:
                                    </td>
                                    <td colspan="3" align="left">
                                        <asp:DropDownList ID="ddlQuotedBy" runat="server" DataTextField="QuotedBy" DataValueField="UserID">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="text-align: right">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="ob_iLboIC ob_iLboIC_L" style="width: 392px; height: 110px; visibility: visible;
                                background-image: url(images/navmenubg.png); background-repeat: no-repeat;">
                                <div class="ob_iLboICH">
                                    <div class="ob_iLboICHCL">
                                    </div>
                                    <div class="ob_iLboICHCM">
                                    </div>
                                    <div class="ob_iLboICHCR">
                                    </div>
                                </div>
                                <div class="ob_iLboICB">
                                    <div class="ob_iLboICBL">
                                        <div class="ob_iLboICBLI">
                                        </div>
                                    </div>
                                    <ul class="ob_iLboICBC" style="height: 100px; min-height: 90px; float: left; width: 183px;
                                        margin-left: 6px;">
                                        <li id="li7"><b>
                                            <asp:LinkButton ID="LinkmenuNew" runat="server" OnClick="NavMenu_Click" CommandArgument="NEW">New RFQ</asp:LinkButton></b><i>NEW</i></li>
                                        <li id="li2"><b>
                                            <asp:LinkButton ID="lnkMenu2" runat="server" OnClick="NavMenu_Click" CommandArgument="RFQ">Quote Submitted</asp:LinkButton></b><i>RFQ</i></li>
                                        <li id="li4"><b>
                                            <asp:LinkButton ID="lnkMenu4" runat="server" OnClick="NavMenu_Click" CommandArgument="RCL">Request Declined</asp:LinkButton></b><i>RCL</i></li>
                                        <li id="li3"><b>
                                            <asp:LinkButton ID="lnkMenu3" runat="server" OnClick="NavMenu_Click" CommandArgument="APP">Quote Approved</asp:LinkButton></b><i>APP</i></li>
                                        <li id="li5"><b>
                                            <asp:LinkButton ID="lnkMenu5" runat="server" OnClick="NavMenu_Click" CommandArgument="TKT">Ticket Issued</asp:LinkButton></b><i>TKT</i></li>
                                    </ul>
                                    <ul class="ob_iLboICBC" style="height: 110px; min-height: 90px; float: left; width: 183px;
                                        margin-left: 2px; padding-left: 6px;">
                                        <li id="li10"><b>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="NavMenu_Click" CommandArgument="TRV">Pax Travelled</asp:LinkButton></b><i>TRV</i></li>
                                        <li id="li9"><b>
                                            <asp:LinkButton ID="lnkMenu6" runat="server" OnClick="NavMenu_Click" CommandArgument="REF">Refund Pending</asp:LinkButton></b><i>REF</i></li>
                                        <li id="li1"><b>
                                            <asp:LinkButton ID="lnkMenu8" runat="server" OnClick="NavMenu_Click" CommandArgument="REC">Refund Closed</asp:LinkButton></b><i>REC</i></li>
                                        <li id="li8"><b>
                                            <asp:LinkButton ID="lnkMenu9" runat="server" OnClick="NavMenu_Click" CommandArgument="CEN">Cancelled</asp:LinkButton></b><i>REC</i></li>
                                        <li id="li6"><b>
                                            <asp:LinkButton ID="lnkMenu7" runat="server" OnClick="NavMenu_Click" CommandArgument="ALL">All Requests</asp:LinkButton></b><i>ALL</i></li>
                                    </ul>
                                    <div class="ob_iLboICBR">
                                        <div class="ob_iLboICBRI">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="txtSelMenu" runat="server"></asp:HiddenField>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rptParent" DataMember="Request" runat="server" OnItemDataBound="rptParent_OnItemDataBound">
                    <HeaderTemplate>
                        <table cellpadding="1px" cellspacing="1px" style="width: 100%;" border="0">
                            <tr class="grid-row-header">
                             
                                <td style="width: 50px;">
                                    Req ID
                                </td>
                                <td style="width: 60px">
                                    Company
                                </td>
                                <td style="width: 100px;">
                                    Vessel
                                </td>
                                <td style="width: 160px;">
                                    Route
                                </td>
                                <td style="width: 90px;">
                                    Departure Date
                                </td>
                                <td style="width: 50px;">
                                    Travel Type
                                </td>
                                <td style="width: 90px;">
                                    Quote Due Date
                                </td>
                                <td style="width: 70px;">
                                    No.Of Pax
                                </td>
                                <td style="width: 80px;">
                                    Requested By
                                </td>
                                <td style="width: 120px">
                                    Quoted By
                                </td>
                                <td style="width: 120px;">
                                    Status
                                </td>
                                <td style="width: 25px;">
                                </td>
                                <td style="width: 25px">
                                </td>
                                <td style="width: 50px;">
                                    Seaman?
                                </td>
                                <td style="text-align: center;">
                                    Actions
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="Repeater-RowStyle-css">
                          
                            <td>
                                <%#Eval( "id")%>
                                <asp:Label ID="lblTravelledFlage" runat="server" Visible="false" Text='<%#Eval("IsTraveledFlag") %>'>' ></asp:Label>
                                <asp:Label ID="lblReqstID" runat="server" Visible="false" Text='<%#Eval("id") %>'>' ></asp:Label>
                            </td>
                            <td>
                                <%# Eval("Company_Short_Name")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <asp:Label ID="lblVesselName" Text='<%#Eval("vessel_name")%>' ForeColor="Blue" onmouseover='<%# "js_ShowToolTip(&#39;"+Eval("VesselFlag").ToString()+"&#39;,event,this)" %>'
                                    runat="server"></asp:Label>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("FlightRoute")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("departureDate","{0:dd/MM/yyyy}")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("classOfTravel")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("QuoteBy_date", "{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                <div class="PaxCount-overout">
                                    <input id="hdfReqID" type="hidden" value='<%#Eval("id")%>' />
                                    <asp:Label ID="lblPaxCount" CssClass="clickable" ForeColor="Blue" runat="server"
                                        Text='<%#Eval("PaxCount")%>'></asp:Label>
                                </div>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("created_by")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("QuotedBy")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCurrentStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "currentstatus")%>'></asp:Label>
                                        </td>
                                        <td style="padding-left: 4px">
                                            <asp:Image ID="imgPODetails" runat="server" ImageAlign="AbsBottom" ImageUrl="~/Images/TRVPODetails.GIF"
                                                AlternateText="PO" onclick='<%# "asyncGet_PODetails_ByReqID("+Eval("id").ToString()+",&#39;"+ Convert.ToString(Session["SUPPCODE"])+"&#39;,event,this)" %>'
                                                onmouseover='<%# "asyncGet_PODetails_ByReqID("+Eval("id").ToString()+",&#39;"+ Convert.ToString(Session["SUPPCODE"])+"&#39;,event,this)" %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: center;">
                                <img src="images/attach-icon.gif" class="clickable" alt="Attach documents" onclick='openAttachments(<%#Eval("id")%>,<%#Eval("AgentReqID")%>,<%#Eval("AgentID")%> , "DOCUMENT",<%# "&#39;"+Eval("current_Status").ToString()+"&#39;" %> )' />
                            </td>
                            <td style="text-align: center;">
                                <img src="images/remarks.gif" class="clickable" alt="View/Add Remarks" onclick="openRemarks(<%#Eval("id")%>);" />
                            </td>
                            <td style="text-align: center;">
                                <%#Eval("isSeaman")%>
                            </td>
                            <td align="center">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <img id="imgbtnAllRemark" runat="server" src="../Images/remark_new.gif" onclick='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;"+Eval("AgentID").ToString() +"&#39;,event,this,&#39;1&#39;);js_HideTooltip();" %>'
                                                onmouseover='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;"+Eval("AgentID").ToString() +"&#39;,event,this,&#39;0&#39;);" %>'
                                                title="Click to add" />
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:ImageButton ID="imgMarkTraveled" ImageUrl="~/Travel/images/markastravels.png" Visible ="false"
                                                runat="server" ToolTip="Mark ticket as used" OnCommand="imgMarkTraveled_click" CommandArgument='<%#Eval("AgentReqID")%>'/>
                                        </td>
                                        <td style="border-color: transparent">
                                            <img src="images/flight.png" alt="Add Quotation" style="height: 18px" class="clickable"
                                                onclick='openWindow("SubmitQuote.aspx?", "QuoteRemark", "elastic", 500, 900, "requestid=<%#DataBinder.Eval(Container.DataItem, "id")%>&SUPPLIER_ID=<%#DataBinder.Eval(Container.DataItem, "AgentID")%> " );' />
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:Image ImageUrl="images/UploadInvoice2.png" alt="Upload Invoice" Style="height: 18px"
                                                onclick='<%# "openAttachments("+Eval("id").ToString()+","+Eval("AgentReqID").ToString()+","+Eval("AgentID").ToString()+",&#39;INVOICE&#39;,&#39;&#39;)" %>'
                                                runat="server" ID="imgUpdInv" Visible='<%#( (Eval("current_Status").ToString()=="TRAVELLED" || Eval("current_Status").ToString()=="ISSUED") && Eval("IsTraveledFlag").ToString()=="Y") && objUA.Edit != 0?true:false %>' />
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:Image ImageUrl="images/ETicket.png" alt="E-Ticket" Style="height: 18px" class="clickable"
                                                ToolTip="Upload e-ticket" AlternateText="Upload Ticket" runat="server" ID="imgUpdTicket"
                                                onclick='<%#"openIssueTicket(&#39;"+Eval("id").ToString()+"&#39;,&#39;"+Eval("AgentID").ToString()+"&#39;)"%>' Visible='<%# (Eval("current_Status").ToString()=="APPROVED" || Eval("current_Status").ToString()=="ISSUED") && objUA.Edit != 0?true:false %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                       
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="Repeater-AlternatingRowStyle-css">
                          
                            <td>
                                <%#Eval( "id")%>
                                <asp:Label ID="lblTravelledFlage" runat="server" Visible="false" Text='<%#Eval("IsTraveledFlag") %>'>' ></asp:Label>
                                <asp:Label ID="lblReqstID" runat="server" Visible="false" Text='<%#Eval("id") %>'>' ></asp:Label>
                            </td>
                            <td>
                                <%# Eval("Company_Short_Name")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <asp:Label ID="lblVesselName" Text='<%#Eval("vessel_name")%>' ForeColor="Blue" onmouseover='<%# "js_ShowToolTip(&#39;"+Eval("VesselFlag").ToString()+"&#39;,event,this)" %>'
                                    runat="server"></asp:Label>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("FlightRoute")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("departureDate","{0:dd/MM/yyyy}")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("classOfTravel")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("QuoteBy_date", "{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                <div class="PaxCount-overout">
                                    <input id="hdfReqID" type="hidden" value='<%#Eval("id")%>' />
                                    <asp:Label ID="lblPaxCount" CssClass="clickable" ForeColor="Blue" runat="server"
                                        Text='<%#Eval("PaxCount")%>'></asp:Label>
                                </div>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("created_by")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <%#Eval("QuotedBy")%>
                            </td>
                            <td class="rpt-tdd-text">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCurrentStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "currentstatus")%>'></asp:Label>
                                        </td>
                                        <td style="padding-left: 4px">
                                            <asp:Image ID="imgPODetails" runat="server" ImageAlign="AbsBottom" ImageUrl="~/Images/TRVPODetails.GIF"
                                                AlternateText="PO" onclick='<%# "asyncGet_PODetails_ByReqID("+Eval("id").ToString()+",&#39;"+ Convert.ToString(Session["SUPPCODE"])+"&#39;,event,this)" %>'
                                                onmouseover='<%# "asyncGet_PODetails_ByReqID("+Eval("id").ToString()+",&#39;"+ Convert.ToString(Session["SUPPCODE"])+"&#39;,event,this)" %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: center;">
                                <img src="images/attach-icon.gif" class="clickable" alt="Attach documents" onclick='openAttachments(<%#DataBinder.Eval(Container.DataItem, "id")%>,<%#Eval("AgentReqID")%>,<%#Eval("AgentID")%>,"DOCUMENT", <%# "&#39;"+Eval("current_Status").ToString()+"&#39;" %> )' />
                            </td>
                            <td style="text-align: center;">
                                <img src="images/remarks.gif" class="clickable" alt="View/Add Remarks" onclick="openRemarks(<%#DataBinder.Eval(Container.DataItem, "id")%>);" />
                            </td>
                            <td style="text-align: center;">
                                <%#Eval("isSeaman")%>
                            </td>
                            <td align="center">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <img id="imgbtnAllRemark" runat="server" src="../Images/remark_new.gif" onclick='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;"+Eval("AgentID").ToString() +"&#39;,event,this,&#39;1&#39;);js_HideTooltip();" %>'
                                                onmouseover='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;"+Eval("AgentID").ToString() +"&#39;,event,this,&#39;0&#39;);" %>'
                                                title="Click to add" />
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:ImageButton ID="imgMarkTraveled" ImageUrl="images/markastravels.png" runat="server"
                                                ToolTip="Mark ticket as used" OnCommand="imgMarkTraveled_click" CommandArgument='<%#Eval("AgentReqID")%>'
                                                Visible='<%# (Eval("current_Status").ToString()=="ISSUED" && Eval("IsTraveledFlag").ToString()=="N") && objUA.Edit != 0?true:false %>' />
                                        </td>
                                        <td style="border-color: transparent">
                                            <img src="images/flight.png" alt="Add Quotation" style="height: 18px" class="clickable"
                                                onclick='openWindow("SubmitQuote.aspx?", "QuoteRemark", "elastic", 500, 900, "requestid=<%#DataBinder.Eval(Container.DataItem, "id")%> &SUPPLIER_ID= <%#DataBinder.Eval(Container.DataItem, "AgentID")%> " );'  />
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:Image ImageUrl="images/UploadInvoice2.png" alt="Upload Invoice" Style="height: 18px"
                                                onclick='<%# "openAttachments("+Eval("id").ToString()+","+Eval("AgentReqID").ToString()+","+Eval("AgentID").ToString()+",&#39;INVOICE&#39;,&#39;&#39;)"%>'
                                                runat="server" ID="imgUpdInv" Visible='<%# ( (Eval("current_Status").ToString()=="TRAVELLED" || Eval("current_Status").ToString()=="ISSUED") && Eval("IsTraveledFlag").ToString()=="Y") && objUA.Edit != 0?true:false %>' />
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:Image ImageUrl="images/ETicket.png" alt="E-Ticket" Style="height: 18px" class="clickable"
                                                ToolTip="Upload e-ticket" AlternateText="Upload Ticket" runat="server" ID="imgUpdTicket"
                                                 onclick='<%#"openIssueTicket(&#39;"+Eval("id").ToString()+"&#39;,&#39;"+Eval("AgentID").ToString()+"&#39;)"%>'  Visible='<%#  (Eval("current_Status").ToString()=="APPROVED" || Eval("current_Status").ToString()=="ISSUED")  && objUA.Edit != 0?true:false %>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                      
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div>
                    <uc2:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindTravelAgentRequests"
                        AlwaysGetRecordsCount="true" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopUp" style="display: none; width: 1200px;" title=''>
        <iframe id="iFrmPopup" src="" frameborder="0" style="width: 100%; height: 600px">
        </iframe>
    </div>
    <div id="dvInsRemark" style="width: 400px; display: none; position: absolute; border: 2px solid black;
        background-color: #F0F8FF;">
        <table width="100%">
            <tr>
                <td colspan="2" style="width: 399px; border-bottom: 1px solid gray">
                    <div id="dvShowremark" style="width: 396px; position: relative; max-height: 150px;
                        overflow-y: auto; overflow-x: hidden">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Remark :
                </td>
                <td>
                    <textarea id="txtNewRemark" rows="5" cols="40"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <input type="button" id="btnSaveRemark" value="Save" onclick="ASync_Ins_Remark(event,this)" />
                    <asp:HiddenField ID="hdf_UserID" runat="server" />
                    &nbsp;
                    <input type="button" id="btnCloseRemark" value="Close" onclick="javascript:document.getElementById('dvInsRemark').style.display = 'none';" />
                </td>
            </tr>
        </table>
    </div>
    <asp:ObjectDataSource ID="objVessel" runat="server" SelectMethod="Get_VesselList"
        TypeName="SMS.Business.Infrastructure.BLL_Infra_VesselLib">
        <SelectParameters>
            <asp:ControlParameter ControlID="cmbFleet" Name="FleetID" Type="Int32" DefaultValue="0"
                PropertyName="SelectedValue" />
            <asp:Parameter Name="VesselID" Type="Int32" />
            <asp:ControlParameter ControlID="ddlVessel_Manager" Name="VesselManager" Type="Int32"
                PropertyName="SelectedValue" />
            <asp:Parameter DefaultValue="" Name="SearchText" Type="String" />
            <asp:SessionParameter DefaultValue="0" Name="UserCompanyID" SessionField="UserCompanyID"  Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="objFleet" runat="server" SelectMethod="GetFleetList_DL"
        TypeName="SMS.Data.Infrastructure.DAL_Infra_VesselLib" OnSelecting="objFleet_Selecting">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="UserCompanyID" SessionField="UserCompanyID"  Type="Int32" />
            <asp:ControlParameter ControlID="ddlVessel_Manager" Name="VesselManager" Type="Int32"
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="objSupplier" runat="server" SelectMethod="Get_SupplierList"
        TypeName="SMS.Business.PURC.BLL_PURC_Common">
        <SelectParameters>
            <asp:Parameter DefaultValue="S" Name="Supplier_Category" Type="String" />
            <asp:Parameter Name="Supplier_Search" Type="String" ConvertEmptyStringToNull="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
