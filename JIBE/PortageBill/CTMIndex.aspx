<%@ Page Title="CTM Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CTMIndex.aspx.cs" Inherits="PortageBill_CTMIndex" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <style type="text/css">
        .ob_iLboICBC li
        {
            float: left;
            font-size: 11px;
        }
        /* For IE6 */* HTML .ob_iLboICBC li
        {
        }
        * HTML .ob_iLboICBC li b
        {
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
            } catch (ex) {
            }
        }

        var lastExecutorAtt = null;
        var evt;
        function ShowAllAttach(CTM_ID, VesselId, event, objthis) {
            evt = event;
            if (lastExecutorAtt != null)
                lastExecutorAtt.abort();
            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_CTM_Attachments', false, { "CTM_ID": CTM_ID, "Vessel_ID": VesselId }, onSuccess_ShowAllAttach, Onfail);

            lastExecutorAtt = service.get_executor();

        }

        function onSuccess_ShowAllAttach(retval) {
            js_ShowToolTip_Fixed(retval, evt, null, "Attachments");
        }

        $(document).ready(function () {
            $("body").on("click", "#btnSearch", function () {
                var Msg = "";
                if ($.trim($("#ctl00_MainContent_txtFromDate").val()) != "") {
                    if (IsInvalidDate($.trim($("#ctl00_MainContent_txtFromDate").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        Msg += "Select Valid From Date<%=TodayDateFormat %>\n";
                    }
                }
                if ($.trim($("#ctl00_MainContent_txtToDate").val()) != "") {
                    if (IsInvalidDate($.trim($("#ctl00_MainContent_txtToDate").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        Msg += "Select Valid To Date<%=TodayDateFormat %>\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        CTM Requests
    </div>
    <div id="page-content" style="z-index: -2; overflow: auto;">
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
        <div id="dvFilter" style="margin: 2px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" cellspacing="2" border="0">
                        <tr>
                            <td>
                                Fleet
                            </td>
                            <td>
                                <auc:CustomDropDownList ID="ddlFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                    Height="150" Width="160" />
                                <%-- <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>--%>
                            </td>
                            <td>
                                From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFromDate"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>
                                Search Port
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Style="width: 100px" />
                            </td>
                            <td rowspan="2" colspan="3">
                                <asp:HiddenField ID="txtSelMenu" runat="server"></asp:HiddenField>
                                <div class="ob_iLboIC ob_iLboIC_L" style="width: 350px; visibility: visible; background-image: url(../images/navmenubg.png);
                                    background-repeat: x; border: 1px solid #cccccc; border-radius: 5px; padding: 4px">
                                    <div class="ob_iLboICB">
                                        <ul class="ob_iLboICBC" style="float: left; width: 47%; margin-left: 6px; border-right: 1px solid #cccccc;
                                            overflow: hidden">
                                            <li id="li2"><b>
                                                <asp:LinkButton ID="lnkMenuAPPROVAL" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="SENTTOOFFICE">Pending Approval </asp:LinkButton></b></li>
                                            <li id="li8"><b>
                                                <asp:LinkButton ID="lnkMenuAPPROVED" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="APPROVED">Approved </asp:LinkButton></b></li>
                                            <li id="li5"><b>
                                                <asp:LinkButton ID="lnkMenuACKVESSEL" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="ACKVESSEL">Ack by Vessel </asp:LinkButton></b></li>
                                        </ul>
                                        <ul class="ob_iLboICBC" style="float: left; width: 47%; margin-left: 2px; padding-left: 6px;
                                            overflow: hidden">
                                            <li id="li4"><b>
                                                <asp:LinkButton ID="lnkMenuCANCELLED" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="CANCELLED">Cancelled</asp:LinkButton></b></li>
                                            <li id="li3"><b>
                                                <asp:LinkButton ID="lnkMenuALL" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="0">All Status</asp:LinkButton></b></li>
                                        </ul>
                                        <div class="ob_iLboICBR">
                                            <div class="ob_iLboICBRI">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vessel Name
                            </td>
                            <td align="left">
                                <auc:CustomDropDownList ID="ddlVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                    Height="200" Width="160" />
                                <%-- <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>--%>
                            </td>
                            <td>
                                To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtToDate"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" ClientIDMode="Static" runat="server" Text="Search" OnClick="btnSearch_Click" /><asp:Button
                                    ID="btnClearFilter" runat="server" Text="Clear Filter" OnClick="btnClearFilter_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="margin-top: 10px;">
                    <asp:GridView ID="gvCTMRequests" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                        Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="None" OnRowCommand="gvCTMRequests_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S/N" Visible="false">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" AccessibleHeaderText="vesselcol">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CTM Requested On">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblRequestedOn" runat="server" Text='<%# Eval("CTM_DATE")%>'></asp:Label>--%>
                                    <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("CTM_DATE")))%>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblCTMRequestedAmt" runat="server" Text='<%# Eval("CTMRequestedRndOffAmt")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cash On Board">
                                <ItemTemplate>
                                    <asp:Label ID="lblCashOnBoardAmt" runat="server" Text='<%# Eval("CashOnBoardAmt")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested Port">
                                <ItemTemplate>
                                    <asp:Label ID="lblPortAgent" runat="server" Text='<%# Eval("port_name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CTM Due Date">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblCTMDate" runat="server" Text='<%# Eval("CTM_Date")%>'></asp:Label>--%>
                                    <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("CTM_DATE")))%>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField AccessibleHeaderText="ReceivedAmt">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCTMReceivedmtHD" runat="server" Text='Received Amount'></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCTMReceivedmt" runat="server" Text='<%# Eval("ReceivedAmt")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField AccessibleHeaderText="DateReceived">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCTMReceiveDateHD" runat="server" Text='Received On'></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateReceived")))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Approved Amt" DataField="ApprovedAmt" AccessibleHeaderText="ApprovedAmt" />
                            <asp:TemplateField HeaderText="Approved On">
                                <ItemTemplate>
                                    <%# UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("ApprovedOn")))%>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CTM Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblCTMStatus" runat="server" Text='<%# Eval("CTMStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField AccessibleHeaderText="ApproverName">
                                <HeaderTemplate>
                                    Pending with me&nbsp;
                                    <asp:CheckBox ID="chkPendingWith" OnCheckedChanged="chkPendingWith_CheckedChanged"
                                        AutoPostBack="true" Checked='<%#   Convert.ToBoolean(ViewState["PendingWithMe"]) %>'
                                        runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCTMPendingWith" runat="server" Text='<%# Eval("ApproverName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View Details" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:HyperLink ID="btnViewDetails" Style="color: Blue; text-decoration: underline;"
                                        runat="server" Text="View Details" NavigateUrl='<%# "CTMRequest.aspx?ID="+Eval("Id").ToString()+"&Vessel_ID=" + Eval("Vessel_ID").ToString()  %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="border: 0px; width: 50%;">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                    Visible='<%# int.Parse(Eval("DocCount").ToString())  > 1 ?true:false %>' OnClientClick='<%#  "ShowAllAttach("+Eval("Id")+","+Eval("Vessel_ID")+",event,this)" %>' />
                                                <asp:HyperLink ID="hpink" runat="server" NavigateUrl='<%# "~/Uploads/CrewAccount/" + Eval("Doc_Name").ToString()%>'
                                                    ImageUrl="~/Images/attach-icon.png" Visible='<%# Eval("DocCount").ToString()=="1"?true:false %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </td>
                                            <td style="border: 0px; width: 50%;">
                                                <asp:Image ID="imgRecordInfo" Style="cursor: pointer;" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onmouseover='<%# "Get_Record_Information(&#39;ACC_LIB_CTM&#39;,&#39; ID="+Eval("ID").ToString()+" and Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                    </asp:GridView>
                    <auc:CustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="Load_CTM_Requests" />
                    <br />
                    <br />
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
