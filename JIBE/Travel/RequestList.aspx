<%@ Page Title="Travel Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="RequestList.aspx.cs" Inherits="RequestList" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/uc_Vessel_List.ascx" TagName="uc_Vessel_List" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
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
    <script src="../Scripts/TRV_RequestList.js?v=1" type="text/javascript"></script>
    <style type="text/css">
        #blur-on-updateprogress
        {
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            z-index: 700;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
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
        
        .htmlchkremark-css
        {
            color: Black;
        }
    </style>
    <style>
        /*Textbox Watermark*/
        
        .unwatermarked
        {
            height: 18px;
            width: 148px;
        }
        
        .watermarked
        {
            height: 20px;
            width: 150px;
            padding: 2px 0 0 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: gray;
        }
    </style>
    <script type="text/javascript">

        var lastExecutor_Vsl = null;

        function ASync_ChangeVesselName() {

            if (lastExecutor_Vsl != null)
                lastExecutor_Vsl.abort();
            var Userid = document.getElementById("<%=hdf_UserID.ClientID%>").value;
            var Vessellistid = document.getElementById("<%=DDlVessel_List.ClientID%>");
            var VeselID = Vessellistid.options[Vessellistid.selectedIndex].value;
            VesselName = Vessellistid.options[Vessellistid.selectedIndex].text;
            if (parseInt(VeselID.toString()) > 0) {
                var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'UPD_Request_Vessel', false, { "ReqID": RequestID_vsl, "VesselID": VeselID, "UserID": Userid }, onSuccessASync_ChangeVesselName, OnfailChangeVesselName);
            }
            lastExecutor_Vsl = service.get_executor();

        }

        function OnfailChangeVesselName() {
            alert('Error while changing vessel name !');
            document.getElementById('dvChangeVesselName').style.display = "none";
        }

        function onSuccessASync_ChangeVesselName(retVal) {
            if (retVal == "1") {
                document.getElementById('dvChangeVesselName').style.display = "none";
                document.getElementById(SrcID).innerHTML = VesselName;
            }

        }


        var lo;
        function selMe(src) {
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
        var currentlink = "";
        $(document).ready(function () {

           //bindselectors();
        });

        function bindselectors() {
            $('a[id^="ctl00_MainContent_lnkMenu"]').click(function () {
                currentlink = $(this).prop("id").slice(-1); ;

                //alert(currentlink);
            });
        }
    </script>

    <script>
        function onSuccessASync_Ins_Remark(retVal, eventArgs) {
            document.getElementById("dvInsRemark").style.display = "none";

        }
        function ASync_Ins_RemarkPage(evt, objthis) {
            var RemarkAgentIDs = "";
            var User_ID = document.getElementById("ctl00_MainContent_hdf_UserID").value;
            var Remark = document.getElementById('txtNewRemark').value;
            document.getElementById("dvInsRemark").style.display = "none";
            if (lastExecutor != null)
                lastExecutor.abort();
            var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'asyncAddRemarks', false, { "Request_ID": _Request_ID, "Remark": Remark, "UserID": User_ID, "Agent_ID": _Agent_ID, "RemarkAgentIDs": RemarkAgentIDs }, onSuccessASync_Ins_Remark, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();

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
    <div class="page-title">
      Travel Requests
    
    </div>
 <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
   
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px; min-height: 620px; overflow: auto;">
        <asp:HiddenField ID="hdf_UserID" runat="server" />
        <asp:ScriptManagerProxy ID="smp1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/TravelService.asmx" />
            </Services>
        </asp:ScriptManagerProxy>
        <div id="dvQuoteAgents" class="draggable" style="position: absolute; background-color: #f8f8f8;
            display: none; border: 1px solid gray;">
            <span id="spnQuoteAgents"></span>
            <br />
        </div>
        <div id="dvGetPaxName" class="draggable" style="position: absolute; background-color: #f8f8f8;
            display: none; border: 1px solid gray;">
            <br />
        </div>
        <div id="dvGetRountInfo" class="draggable" style="position: absolute; background-color: #f8f8f8;
            display: none; border: 1px solid gray;">
            <br />
        </div>
        <div id="dvGetVesselPortCall" class="draggable" style="position: absolute; background-color: #f8f8f8;
            display: none; border: 1px solid gray;">
            <br />
        </div>
        <div style="border: 1px solid #efefef;">
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
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 75%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            Fleet
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbFleet" runat="server" AutoPostBack="true" Width="150px"
                                                DataSourceID="objFleet" DataTextField="name" AppendDataBoundItems="true" DataValueField="code"
                                                OnSelectedIndexChanged="cmbFleet_SelectedIndexChanged">
                                                <asp:ListItem Text="-Select All-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Sector
                                        </td>
                                        <td>
                                            From&nbsp;<asp:TextBox ID="txtSectorFrom" runat="server" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="ACtxtFrom1" TargetControlID="txtSectorFrom"
                                                CompletionSetCount="10" MinimumPrefixLength="2" ServiceMethod="GetAirportList"
                                                ContextKey="" CompletionInterval="200" ServicePath="~/TravelService.asmx" runat="server"
                                                CompletionListItemCssClass="autocomplete-item" CompletionListHighlightedItemCssClass="autocomplete-selected-item">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            To&nbsp;<asp:TextBox ID="txtSectorTo" runat="server" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSectorTo"
                                                CompletionSetCount="10" MinimumPrefixLength="2" ServiceMethod="GetAirportList"
                                                ContextKey="" CompletionInterval="200" ServicePath="~/TravelService.asmx" runat="server"
                                                CompletionListItemCssClass="autocomplete-item" CompletionListHighlightedItemCssClass="autocomplete-selected-item">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                        <td align="right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Vessel
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbVessel" runat="server" AutoPostBack="true" Width="150px"
                                                DataSourceID="objVessel" DataTextField="Vessel_Name" DataValueField="Vessel_id"
                                                OnDataBound="cmbVessel_OnDataBound" OnSelectedIndexChanged="cmbVessel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Travel Date
                                        </td>
                                        <td>
                                            From&nbsp;<asp:TextBox ID="txtTrvDateFrom" runat="server" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="TrvDateFrom" runat="server" TargetControlID="txtTrvDateFrom"
                                                Format="dd-MMM-yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>
                                            To&nbsp;<asp:TextBox ID="txtTrvDateTo" runat="server" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="TrvDateTo" runat="server" TargetControlID="txtTrvDateTo"
                                                Format="dd-MMM-yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td align="right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Travel Agent
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSupplier" AppendDataBoundItems="true" Width="150px" AutoPostBack="true"
                                                runat="server" DataSourceID="objSupplier" DataTextField="fullname" DataValueField="id"
                                                OnSelectedIndexChanged="cmbSupplier_SelectedIndexChanged">
                                                <asp:ListItem Text="-Select All-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Search:
                                        </td>
                                        <td colspan="2">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                           <asp:TextBox ID="txtPaxName" CssClass="unwatermarked" runat="server" Width="345px" ></asp:TextBox>
                                           <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender" runat="server" TargetControlID="txtPaxName" WatermarkText="Type Req ID or Pax Name to search" WatermarkCssClass="watermarked"></ajaxToolkit:TextBoxWatermarkExtender>
                                            <asp:Button ID="btnSearch" runat="server" Style="display: none"
                                                onclick="btnSearch_Click" />
                                                </asp:Panel>
                                        </td>
                                        <td align="right">
                                        </td>
                                    </tr>
                                </table>
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
                                                    <li id="li1"><b>
                                                        <asp:LinkButton ID="lnkMenu1" runat="server" OnClick="NavMenu_Click" CommandArgument="NEW">New Requests</asp:LinkButton></b><i>NEW</i></li>
                                                    <li id="li2"><b>
                                                        <asp:LinkButton ID="lnkMenu2" runat="server" OnClick="NavMenu_Click" CommandArgument="RFQ">Quote received / Approval </asp:LinkButton></b><i>RFQ</i></li>
                                                    <li id="li3"><b>
                                                        <asp:LinkButton ID="lnkMenu3" runat="server" OnClick="NavMenu_Click" CommandArgument="APP">Quote Approved</asp:LinkButton></b><i>APP</i></li>
                                                    <li id="li5"><b>
                                                        <asp:LinkButton ID="lnkMenu5" runat="server" OnClick="NavMenu_Click" CommandArgument="TKT">Ticket Issued</asp:LinkButton></b><i>TKT</i></li>
                                                </ul>
                                                <ul class="ob_iLboICBC" style="height: 110px; min-height: 90px; float: left; width: 183px;
                                                    margin-left: 2px; padding-left: 6px;">
                                                    <li id="li9"><b>
                                                        <asp:LinkButton ID="lnkMenu6" runat="server" OnClick="NavMenu_Click" CommandArgument="REF">Refund Pending</asp:LinkButton></b><i>REF</i></li>
                                                    <li id="li7"><b>
                                                        <asp:LinkButton ID="lnkMenu8" runat="server" OnClick="NavMenu_Click" CommandArgument="REC">Refund Closed</asp:LinkButton></b><i>REC</i></li>
                                                    <li id="li4"><b>
                                                        <asp:LinkButton ID="lnkMenu4" runat="server" OnClick="NavMenu_Click" CommandArgument="RCL">Request Closed</asp:LinkButton></b><i>RCL</i></li>
                                                    <li id="li8"><b>
                                                        <asp:LinkButton ID="lnkMenu9" runat="server" OnClick="NavMenu_Click" CommandArgument="CEN">Cancelled</asp:LinkButton></b><i>CEN</i></li>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: left; margin-top: 2px; border: 1px solid #efefef; min-height: 200px;">
            <asp:UpdatePanel ID="updList" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="padding-right: 40px">
                                <asp:CheckBox ID="chkShowAllPendingApproval" ForeColor="Black" runat="server" Checked="true"
                                    Text="Show All" AutoPostBack="true" OnCheckedChanged="chkShowAllPendingApproval_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                    <asp:Repeater ID="rptParent" DataMember="Request" runat="server" OnItemCommand="rptParent_ItemCommand"
                        OnItemDataBound="rptParent_OnItemDataBound">
                        <HeaderTemplate>
                      
                            <table cellpadding="1px" cellspacing="1px" style="width: 100%;" border="0">
                                <tr class="grid-row-header">
                                  
                                    <td style="width: 50px;">
                                        Req ID
                                    </td>
                                    <td style="width: 70px;">
                                        Vessel
                                    </td>
                                    <td style="width: 160px;">
                                        Route
                                    </td>
                                    <td align="center" style="width: 70px;">
                                        Departure Date
                                    </td>
                                    <td style="width: 50px;">
                                        Travel Type
                                    </td>
                                    <td align="center" style="width: 70px;">
                                        Qtn. Due Date
                                    </td>
                                    <td style="width: 30px;">
                                        Pax
                                    </td>
                                    <td style="width: 30px;">
                                        Smn
                                    </td>
                                    <td style="width: 30px;">
                                        RFQ
                                    </td>
                                    <td style="width: 30px;">
                                        QTN
                                    </td>
                                    <asp:PlaceHolder ID="rptPlhrHearderExpDate" runat="server">
                                        <td align="center" style="width: 120px;">
                                            Earliest Expiry Date
                                        </td>
                                    </asp:PlaceHolder>
                                    <td style="width: 60px;">
                                        Reqst By
                                    </td>
                                    <td style="width: 100px;">
                                        Status
                                    </td>
                                    <td style="width: 10px;">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                    <td>
                                        Pending With
                                    </td>
                                    <td style="text-align: center;">
                                        Actions
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="Repeater-RowStyle-css">
                              
                                <td>
                                    <asp:HyperLink ID="hlnkReqID" 
                                        runat="server" Target="_blank" Text='<%#Eval("id")%>' NavigateUrl='<%# "newrequest.aspx?Request_ID="+Eval("id").ToString()%>'></asp:HyperLink>
                                </td>
                                <%--    class="rpt-tdd-text"--%>
                                <td class="PortCall-overout">
                                    <input id="hdfVesselID" type="hidden" value='<%#Eval("vessel")%>' />
                                    <asp:Label ID="lblVesselname" ForeColor="Blue" runat="server" Style="cursor: pointer"
                                        onclick='<%# "ShowdvChangeVessel(this,event,&#39;"+Eval("id").ToString()+"&#39;)"%>'
                                        Text='  <%#Eval("vessel_name")%>'></asp:Label>
                                </td>
                                <td align="left">
                                    <div class="Rout-overout">
                                        <input id="hdfRoutReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <asp:Label ID="lblRountInfo" CssClass="clickable" ForeColor="Blue" runat="server"
                                            Text='<%#Eval("FlightRoute")%>'></asp:Label>
                                    </div>
                                </td>
                                <td align="center" class="rpt-tdd-text">
                                    <asp:Label ID="lblDeptDate" ForeColor="Blue" runat="server" Style="cursor: pointer"
                                        Text='  <%#Eval("departureDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </td>
                                <td class="rpt-tdd-text">
                                    <%#Eval("classOfTravel")%>
                                </td>
                                <td align="center" class="rpt-tdd-text">
                                    <%#Eval("QuoteDueDate", "{0:dd/MM/yyyy}")%>
                                </td>
                                <td>
                                    <div class="PaxCount-overout">
                                        <input id="hdfReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <asp:Label ID="lblPaxCount" CssClass="clickable" ForeColor="Blue" runat="server"
                                            Text='<%#Eval("PaxCount")%>'></asp:Label>
                                    </div>
                                </td>
                                <td style="text-align: center;">
                                    <%#Eval("isSeaman")%>
                                </td>
                                <td>
                                    <div class="Agent-overout">
                                        <input id="dhnReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <input id="hdnQuoted" type="hidden" value='0' />
                                        <asp:Label ID="lblAgents" CssClass="clickable" ForeColor="Blue" runat="server" Text='<%#Eval("QuoteSent")%>'></asp:Label>
                                    </div>
                                </td>
                                <td>
                                    <div class="Agent-overout">
                                        <input id="dhnReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <input id="hdnQuoted" type="hidden" value='1' />
                                        <asp:Label ID="Label1" CssClass="clickable" ForeColor="Blue" runat="server" Text='<%#Eval("QuoteReceived")%>'></asp:Label>
                                    </div>
                                </td>
                                <asp:PlaceHolder ID="rptPlhrExpDate" runat="server">
                                    <td align="center" class="rpt-tdd-text">
                                        <asp:Label ID="lblColorFlag" Visible="false" runat="server" Text='<%#Eval("COLORFLAG")%>'></asp:Label>
                                        <asp:Label ID="lblAttFlag" Visible="false" runat="server" Text='<%#Eval("AttFlag")%>'></asp:Label>
                                        <asp:Label ID="lblRqstID" Visible="false" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                        <asp:Label ID="lblRemark" Visible="false" runat="server" Text='<%#Eval("remarks")%>'></asp:Label>
                                        <div id="divDateColor" runat="server">
                                            <asp:Label ID="lblOptionExpiry" runat="server" Text='<%#Eval("OptionExpiry")%>'></asp:Label>
                                        </div>
                                    </td>
                                </asp:PlaceHolder>
                                <td class="rpt-tdd-text">
                                    <%#Eval("created_by")%>
                                </td>
                                <td class="rpt-tdd-text">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%#Eval("CurrentStatus")%>
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
                                    <asp:ImageButton ID="imgAtt" ImageUrl="~/Travel/images/attach-icon.gif" runat="server"
                                        ToolTip="Add/View Attachment" />
                                    <asp:ImageButton ID="imgAddAtt" ImageUrl="~/Travel/images/AddAttchment.png" runat="server"
                                        ToolTip="Add Attachment" />
                                    <%--<img id="imgAtt" runat="server" src="images/attach-icon.gif" title ="Attach documents"   class="clickable" alt="Attach documents" onclick='openAttachments(<%#DataBinder.Eval(Container.DataItem, "id")%>)' />--%>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Image ID="imgbtnAllRemark" runat="server" ImageUrl="../Images/remark_new.gif"
                                        onclick='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;0&#39;,event,this,&#39;1&#39;);js_HideTooltip();" %>'
                                        onmouseover='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;0&#39;,event,this,&#39;0&#39;);" %>'
                                        ToolTip="Click to add" />
                                    <%--<img src="images/remarks.gif" class="clickable" alt="View/Add Remarks" onclick="openRemarks(<%#DataBinder.Eval(Container.DataItem, "id")%>);" />--%>
                                </td>
                                <td>
                                    <%#Eval("PendingWith")%>
                                </td>
                                <td style="text-align: center; width: 110px">
                                    <table>
                                        <tr>
                                            <td style="width: 30px">
                                                <img id="imgSendRFQ" src="images/send-rfq.png" alt="Send RFQ" class="clickable" onmouseover="javascript:js_ShowToolTip('<br/>Send RFQ',event,this)"
                                                    runat="server" onclick='<%# "SendRFQ("+ DataBinder.Eval(Container.DataItem, "id")+" );return false;"%>'
                                                    height="18" />
                                            </td>
                                            <td style="width: 30px">
                                                <img runat="server" id="imgEvaluation" src="images/evaluation.png" alt="Evaluate Request"
                                                    height="18" style="border: 0px;" onclick='<%# "openEvaluation("+DataBinder.Eval(Container.DataItem, "id") +");"%>'
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Evaluation',event,this)" />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="imgMarkTraveled" ImageUrl="~/Travel/images/markastravels.png"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Mark ticket as used',event,this)"
                                                    runat="server" OnCommand="imgMarkTraveled_click" OnClick='<%# "asyncGet_Marked_IsTravelled("+Eval("id").ToString()+",this);return false;"%>'
                                                    CommandArgument='<%#Eval("id")%>' />
                                            </td>
                                            <td style="width: 30px; display: none">
                                                <asp:UpdatePanel ID="updimgUserPrefc" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgUserPrefc" OnClick="imgUserPrefc_Click" runat="server" ImageUrl="images/UserPreference.png"
                                                            Height="18px" onmouseover="javascript:js_ShowToolTip(&#39;<br/>User Preference&#39;,event,this)"
                                                            AlternateText="Request User Preference" CssClass="clickable" CommandArgument='<%#Eval("id")%>' />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="ImageButton4" runat="server" AlternateText="Send for Approval"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Send for Approval',event,this)"
                                                    Height="18px" BorderWidth="0px" CommandName="RequestApproval" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                    ImageUrl="images/Approval.png" />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:Image ImageUrl="images/ETicket.png" alt="E-Ticket" Style="height: 18px" class="clickable"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Upload Ticket',event,this)" AlternateText="Upload Ticket"
                                                    runat="server" ID="imgUpdTicket" onclick='<%# "UploadeTicket(&#39;"+Eval("id").ToString()+"&#39;)"%>' />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ImageUrl="images/ETicket.png" Style="height: 18px" class="clickable"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Send Ticket',event,this)" CommandArgument='<%#Eval("id")%>'
                                                    AlternateText="Send Ticket" runat="server" ID="imgSendTicket" CommandName="SENDTICKET" />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="imgRefund" runat="server" AlternateText="Refund Request" BorderWidth="0px"
                                                    Height="18px" onmouseover="javascript:js_ShowToolTip('<br/>Refund',event,this)"
                                                    CommandName="refund" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                    ImageUrl="images/refund.png" Visible='<%# objUA.Edit != 0?true:false  %>' />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="lbtnRollBackTr" runat="server" AlternateText="Roll Back" ImageUrl="~/Travel/images/Cancel_New.gif"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Roll back at Quote received ',event,this)"
                                                    ImageAlign="AbsMiddle" OnClientClick='<%#"Showdiv_rollback("+Eval("id")+")"%>'>
                                                </asp:ImageButton>
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Cancel Request"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Delete',event,this)" Height="18px"
                                                    BorderWidth="0px" CommandName="delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                    ImageUrl="~/Images/Close.gif" OnClientClick="return confirm('This will DELETE the travel request. Do you want to proceed?')" />
                                            </td>
                                            <td style="padding-left: 10px">
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_TRV_DTL_Request&#39;,&#39; ID="+Eval("id")+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Repeater-AlternatingRowStyle-css">
                              
                                <td>
                                    <asp:HyperLink ID="hlnkReqID" 
                                        runat="server" Target="_blank" Text='<%#Eval("id")%>' NavigateUrl='<%# "newrequest.aspx?Request_ID="+Eval("id").ToString()%>'></asp:HyperLink>
                                </td>
                                <%--class="rpt-tdd-text"--%>
                                <td class="PortCall-overout">
                                    <input id="hdfVesselID" type="hidden" value='<%#Eval("vessel")%>' />
                                    <asp:Label ID="lblVesselname" ForeColor="Blue" runat="server" Style="cursor: pointer"
                                        onclick='<%# "ShowdvChangeVessel(this,event,&#39;"+Eval("id").ToString()+"&#39;)"%>'
                                        Text='  <%#Eval("vessel_name")%>'></asp:Label>
                                </td>
                                <td align="left">
                                    <div class="Rout-overout">
                                        <input id="hdfRoutReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <asp:Label ID="lblRountInfo" CssClass="clickable" ForeColor="Blue" runat="server"
                                            Text='<%#Eval("FlightRoute")%>'></asp:Label>
                                    </div>
                                </td>
                                <td class="rpt-tdd-text">
                                    <asp:Label ID="lblDeptDate" ForeColor="Blue" runat="server" Style="cursor: pointer"
                                        Text='  <%#Eval("departureDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </td>
                                <td class="rpt-tdd-text">
                                    <%#Eval("classOfTravel")%>
                                </td>
                                <td class="rpt-tdd-text">
                                    <%#Eval("QuoteDueDate", "{0:dd/MM/yyyy}")%>
                                </td>
                                <td>
                                    <div class="PaxCount-overout">
                                        <input id="hdfReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <asp:Label ID="lblPaxCount" CssClass="clickable" ForeColor="Blue" runat="server"
                                            Text='<%#Eval("PaxCount")%>'></asp:Label>
                                    </div>
                                </td>
                                <td style="text-align: center;">
                                    <%#Eval("isSeaman")%>
                                </td>
                                <td>
                                    <div class="Agent-overout">
                                        <input id="dhnReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <input id="hdnQuoted" type="hidden" value='0' />
                                        <asp:Label ID="lblAgents" CssClass="clickable" ForeColor="Blue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "QuoteSent")%>'></asp:Label>
                                    </div>
                                </td>
                                <td>
                                    <div class="Agent-overout">
                                        <input id="dhnReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <input id="hdnQuoted" type="hidden" value='1' />
                                        <asp:Label ID="Label1" CssClass="clickable" ForeColor="Blue" runat="server" Text='<%#Eval("QuoteReceived")%>'></asp:Label>
                                    </div>
                                </td>
                                <asp:PlaceHolder ID="rptPlhrExpDate" runat="server">
                                    <td class="rpt-tdd-text">
                                        <asp:Label ID="lblColorFlag" Visible="false" runat="server" Text='<%#Eval("COLORFLAG")%>'></asp:Label>
                                        <asp:Label ID="lblAttFlag" Visible="false" runat="server" Text='<%#Eval("AttFlag")%>'></asp:Label>
                                        <asp:Label ID="lblRqstID" Visible="false" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                        <asp:Label ID="lblRemark" Visible="false" runat="server" Text='<%#Eval("remarks")%>'></asp:Label>
                                        <div id="divDateColor" runat="server">
                                            <asp:Label ID="lblOptionExpiry" runat="server" Text='<%#Eval("OptionExpiry")%>'></asp:Label>
                                        </div>
                                    </td>
                                </asp:PlaceHolder>
                                <td class="rpt-tdd-text">
                                    <%#Eval("created_by")%>
                                </td>
                                <td class="rpt-tdd-text">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%#Eval("CurrentStatus")%>
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
                                    <asp:ImageButton ID="imgAtt" ImageUrl="~/Travel/images/attach-icon.gif" runat="server"
                                        ToolTip="Add/View Attachment" />
                                    <asp:ImageButton ID="imgAddAtt" ImageUrl="~/Travel/images/AddAttchment.png" runat="server"
                                        ToolTip="Add Attachment" />
                                    <%--   <img id="imgAtt" runat="server" src="images/attach-icon.gif" runat="server" class="clickable" title="Attach documents" alt="Attach documents" onclick='openAttachments(<%#DataBinder.Eval(Container.DataItem, "id")%>)' />--%>
                                    </div>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Image ID="imgbtnAllRemark" runat="server" ImageUrl="../Images/remark_new.gif"
                                        onclick='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;0&#39;,event,this,&#39;1&#39;);js_HideTooltip();" %>'
                                        onmouseover='<%#"ASync_Get_Remark(&#39;"+Eval("id").ToString()+"&#39;,&#39;0&#39;,event,this,&#39;0&#39;);" %>'
                                        ToolTip="Click to add" />
                                </td>
                                <td>
                                    <%#Eval("PendingWith")%>
                                </td>
                                <td style="text-align: center;">
                                    <table>
                                        <tr>
                                            <td style="width: 40px">
                                                <img id="imgSendRFQ" src="images/send-rfq.png" alt="Send RFQ" class="clickable" onmouseover="javascript:js_ShowToolTip('<br/>Send RFQ',event,this)"
                                                    runat="server" onclick='<%# "SendRFQ("+ DataBinder.Eval(Container.DataItem, "id")+" );return false;"%>'
                                                    height="18" />
                                            </td>
                                            <td style="width: 30px">
                                                <img runat="server" id="imgEvaluation" src="images/evaluation.png" alt="Evaluate Request"
                                                    height="18" style="border: 0px;" onclick='<%# "openEvaluation("+DataBinder.Eval(Container.DataItem, "id") +");"%>'
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Evaluation',event,this)" />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="imgMarkTraveled" ImageUrl="~/Travel/images/markastravels.png"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Mark ticket as used',event,this)"
                                                    runat="server" OnCommand="imgMarkTraveled_click" OnClick='<%# "asyncGet_Marked_IsTravelled("+Eval("id").ToString()+",this);return false"%>'
                                                    CommandArgument='<%#Eval("id")%>' />
                                            </td>
                                            <td style="width: 30px; display: none">
                                                <asp:UpdatePanel ID="updimgUserPrefc" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgUserPrefcA" OnClick="imgUserPrefc_Click" runat="server" ImageUrl="images/UserPreference.png"
                                                            Height="18px" onmouseover="javascript:js_ShowToolTip(&#39;<br/>User Preference&#39;,event,this)"
                                                            AlternateText="Request User Preference" CssClass="clickable" CommandArgument='<%#Eval("id")%>' />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="ImageButton4" runat="server" AlternateText="Send for Approval"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Send for Approval',event,this)"
                                                    Height="18px" BorderWidth="0px" CommandName="RequestApproval" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                    ImageUrl="images/Approval.png" />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:Image ImageUrl="images/ETicket.png" alt="E-Ticket" Style="height: 18px" class="clickable"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Upload Ticket',event,this)" AlternateText="Upload Ticket"
                                                    runat="server" ID="imgUpdTicket" onclick='<%# "UploadeTicket(&#39;"+Eval("id").ToString()+"&#39;)"%>' />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ImageUrl="images/ETicket.png" Style="height: 18px" class="clickable"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Send Ticket',event,this)" CommandArgument='<%#Eval("id")%>'
                                                    AlternateText="Send Ticket" runat="server" ID="imgSendTicket" CommandName="SENDTICKET" />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="imgRefund" runat="server" AlternateText="Refund Request" BorderWidth="0px"
                                                    Height="18px" onmouseover="javascript:js_ShowToolTip('<br/>Refund',event,this)"
                                                    CommandName="refund" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                    ImageUrl="images/refund.png" Visible='<%# objUA.Edit != 0?true:false  %>' />
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="lbtnRollBackTr" runat="server" AlternateText="Roll Back" ImageUrl="~/Travel/images/Cancel_New.gif"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Roll back at Quote received ',event,this)"
                                                    ImageAlign="AbsMiddle" OnClientClick='<%#"Showdiv_rollback("+Eval("id")+")"%>'>
                                                </asp:ImageButton>
                                            </td>
                                            <td style="width: 30px">
                                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Cancel Request"
                                                    onmouseover="javascript:js_ShowToolTip('<br/>Delete',event,this)" Height="18px"
                                                    BorderWidth="0px" CommandName="delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                                    ImageUrl="~/Images/Close.gif" OnClientClick="return confirm('This will DELETE the travel request. Do you want to proceed?')" />
                                            </td>
                                            <td style="padding-left: 10px">
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_TRV_DTL_Request&#39;,&#39; ID="+Eval("id")+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                          
                            </table>
                             <div id="NoRecords" align="center" runat="server" visible="false">
                            No records are available.
                          </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div>
                        <uc2:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindRequestList"
                            AlwaysGetRecordsCount="true" />
                    </div>
                    <asp:HiddenField ID="hdf_RequestID" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:ObjectDataSource ID="objRequests" runat="server" SelectMethod="GetAllRequests"
            TypeName="SMS.Business.TRAV.TravelRequest"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objVessel" runat="server" SelectMethod="Get_VesselList"
            TypeName="SMS.Business.Infrastructure.BLL_Infra_VesselLib">
            <SelectParameters>
                <asp:ControlParameter ControlID="cmbFleet" Name="FleetID" Type="Int32" DefaultValue="0"
                    PropertyName="SelectedValue" />
                <asp:Parameter Name="VesselID" Type="Int32" />
                 <asp:SessionParameter SessionField="USERCOMPANYID" Name="VesselManager" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="SearchText" Type="String" />
                <asp:SessionParameter Name="UserCompanyID" SessionField="USERCOMPANYID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objFleet" runat="server" SelectMethod="GetFleetList_DL"
            TypeName="SMS.Data.Infrastructure.DAL_Infra_VesselLib">
            <SelectParameters>
                <asp:SessionParameter Name="UserCompanyID" SessionField="USERCOMPANYID" Type="Int32" />
                <asp:SessionParameter SessionField="USERCOMPANYID" Name="VesselManager" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objSupplier" runat="server" SelectMethod="Get_SupplierList"
            TypeName="SMS.Business.TRAV.BLL_TRV_Supplier">
            <SelectParameters>
                <asp:Parameter Name="Supplier_Search" Type="String" ConvertEmptyStringToNull="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div id="dvPopup_Refund" style="display: none; width: 600px" title="Refund">
        <asp:UpdatePanel ID="updRefund" runat="server">
            <ContentTemplate>
                <div style="height: 170px">
                    <div style="background-color: #C6D9F1; font-size: medium; font-family: Tahoma; color: Black;">
                        <asp:Label ID="lblRequestIDRefund" runat="server"></asp:Label>
                        <hr />
                    </div>
                    <div>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    Remarks : &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRefunRemarks" runat="server" TextMode="MultiLine" Width="500px"
                                        Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #f8f8f8;">
                        <hr />
                        <center>
                            <asp:HiddenField ID="hdnRequestID" runat="server" />
                            <%-- <asp:Button ID="cmdRefund" runat="server" Text="Save" OnClick="cmdRefund_Click" OnClientClick="return confirm('This will put the request for REFUND. Do you want to proceed?')" /> --%>
                            <asp:Button ID="cmdRefund" runat="server" Text="Save" OnClick="cmdRefund_Click" OnClientClick="return OnRefund(id);" />
                            &nbsp;&nbsp;
                            <asp:Button ID="cmdRefundCancel" runat="server" Text="Cancel" OnClick="cmdRefundCancel_Click" />
                        </center>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="LinkUrl" runat="server" />
    </div>
    <div id="dialog" title="Event Details" style="width: 1200px">
        Loading Data ...
    </div>
    <div id="divSendForApproval" title="Send for Approval" style="width: 500px; display: none">
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
                            <asp:ListBox ID="lstUserList" runat="server" DataTextField="UserName" DataValueField="UserID"
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
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Font-Names="Tahoma"
                                Width="98%" Height="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 5px">
                            <asp:HiddenField ID="hdnRequestID_App" runat="server" />
                            <asp:Label ID="lblMessageOnSendApproval" runat="server" Font-Size="12px" ForeColor="Red"
                                Font-Bold="true" BackColor="Yellow"></asp:Label><br />
                            <asp:Button ID="btnSendForApproval" runat="server" Height="30px" Text="Send For Approval"
                                OnClick="btnSendForApproval_Click" OnClientClick=" return CheckForPendingManagerApproval(id)" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvUserList" style="height: 400px; width: 670px; text-align: center; border: 1px solid gray;
        display: none" title="User List">
        <asp:UpdatePanel ID="updUserlist" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="height: 350px; width: 660px; text-align: left; overflow: scroll">
                    <asp:CheckBoxList ID="chklistUser" RepeatColumns="4" Font-Size="11px" Font-Names="verdana"
                        RepeatDirection="Vertical" RepeatLayout="Table" Height="400px" runat="server">
                    </asp:CheckBoxList>
                </div>
                <asp:Button ID="btnSendUserPreference" runat="server" Text="Send For Preference"
                    OnClick="btnSendUserPreference_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopUp" style="display: none; width: 1200px;" title=''>
        <iframe id="iFrmPopup" src="" frameborder="0" style="width: 100%; height: 600px">
        </iframe>
    </div>
    <div style="display: none; background-color: Teal; padding: 5px; border: 2px groove lime;
        position: absolute; z-index: 1000;" id="dvChangeVesselName">
        <table>
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="DDlVessel_List" runat="server" Font-Size="11px" AutoPostBack="false"
                        DataTextField="Vessel_Short_Name" DataValueField="Vessel_ID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" value="Change Vessel Name" onclick="javascript:ASync_ChangeVesselName();return false;" />
                </td>
                <td>
                    <input type="button" value="Cancel" onclick="javascript:document.getElementById('dvChangeVesselName').style.display = 'none';return false;" />
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none; background-color: #F0FFFF; padding: 5px; border: 2px groove lime;
        position: absolute; z-index: 1000;" id="dvChangeDeptDate">
        <table>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtChangeDeptdate" Width="70px" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChangeDeptdate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" value="Change Dept Date" onclick="javascript:ASync_ChangeDeptDate();return false;" />
                </td>
                <td>
                    <input type="button" value="Cancel" onclick="javascript:document.getElementById('dvChangeDeptDate').style.display = 'none';return false;" />
                </td>
            </tr>
        </table>
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
                    <textarea id="txtNewRemark" rows="7" cols="40"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                    <input type="button" id="btnSaveRemark" value="Save" onclick="ASync_Ins_RemarkPage(event,this)" />
                    &nbsp;
                    <input type="button" id="btnCloseRemark" value="Close" onclick="javascript:document.getElementById('dvInsRemark').style.display = 'none';" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="dvReqAgentsRemark" style="width: 396px; position: relative; max-height: 100px;
                        overflow-y: auto; overflow-x: hidden">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="RollbackTR" style="display: none; width: 500px" title="Roll back at Quote received / Approval">
        <table>
            <tr>
                <td colspan="2" style="padding-bottom: 10px">
                    <asp:Label ID="lblReqestIDRollback" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Remark :
                </td>
                <td>
                    <asp:TextBox ID="txtRemarkRollback" runat="server" Height="80px" Width="400px" TextMode="MultiLine">
                    </asp:TextBox><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                        ErrorMessage="Please enter remark" ValidationGroup="vgrollback" ControlToValidate="txtRemarkRollback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnRollbackTR" runat="server" Text="Roll back at Quote received / Approval"
                        Height="30px" OnClientClick="hideModal('RollbackTR')" ValidationGroup="vgrollback"
                        OnClick="btnRollbackTR_Click" />
                    <br />
                    <asp:HiddenField ID="hdf_TRID_Rollback" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
