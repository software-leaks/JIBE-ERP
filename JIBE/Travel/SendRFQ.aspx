<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendRFQ.aspx.cs" Inherits="SendRFQ" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send RFQ</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            bindClientEvents();
        });

        $(document).ready(function () {
            bindPaxsName();
        });

        $(document).ready(function () {
            bindRoutInfo();
        });

        function ValidateValues() {

            if (!confirm("About to send new quotation request, sure?"))
                return false;
        }
        function bindClientEvents() {

            $('.Agent-overout').mouseover(function (evt) { var Reqid = $(this).find('#dhnReqID').val(); var Quoted = $(this).find('#hdnQuoted').val(); GetQuoteAgents(Reqid, Quoted); $('#dvQuoteAgents').show(); $('#dvQuoteAgents').css({ 'top': evt.clientY + 10, 'left': evt.clientX + 10 }); }).mouseout(function () { $('#dvQuoteAgents').hide(); });

        }

        function bindPaxsName() {
            $('.PaxCount-overout').mouseover(function (evt) { var Reqid = $(this).find('#hdfReqID').val(); GetPaxsName(Reqid); $('#dvGetPaxName').show(); $('#dvGetPaxName').css({ 'top': evt.clientY + 10, 'left': evt.clientX + 10 }); }).mouseout(function () { $('#dvGetPaxName').hide(); });
        }

        function bindRoutInfo() {
            $('.Rout-overout').mouseover(function (evt) { var Reqid = $(this).find('#hdfRoutReqID').val(); GetRoutInfo(Reqid); $('#dvGetRountInfo').show(); SetPosition_Relative(evt, 'dvGetRountInfo'); }).mouseout(function () { $('#dvGetRountInfo').hide(); });
        }


        function GetQuoteAgents(id, Quoted) {
            TravelService.GetQuoteAgent(id, Quoted, onGetQuoteAgents);
        }
        function onGetQuoteAgents(result) {
            $('#dvQuoteAgents').html(result);
        }


        function GetPaxsName(rqstid) {
            TravelService.GetPaxsName(rqstid, onGetPaxNames);
        }

        function onGetPaxNames(PaxNameresult) {
            var dvGetPaxName = document.getElementById("dvGetPaxName");
            dvGetPaxName.innerHTML = PaxNameresult;
        }



        function GetRoutInfo(rqstid) {
            TravelService.GetRoutInfo(rqstid, onGetRoutInfo);
        }

        function onGetRoutInfo(RoutInforesult) {
            var dvRoutInfo = document.getElementById("dvGetRountInfo");
            dvRoutInfo.innerHTML = RoutInforesult;
        }

    </script>
</head>
<body style="background-color: White; padding: 5px">
    <form id="frmSendRFQ" runat="server">
    <asp:ScriptManager ID="SMRFQ" runat="server" />
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        <div>
            Send RFQ Request ID - &nbsp;<asp:Literal ID="ltRequestID" runat="server" Text=""></asp:Literal>
        </div>
    </div>
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
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <asp:ScriptManagerProxy ID="smp1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/TravelService.asmx" />
            </Services>
        </asp:ScriptManagerProxy>
        <div>
            <asp:UpdatePanel ID="updSendRFQ" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rptParent" DataMember="Request" runat="server" OnItemDataBound="rptParent_OnItemDataBound">
                        <HeaderTemplate>
                            <table cellpadding="1px" cellspacing="1px" style="width: 100%;" border="0">
                                <tr class="grid-row-header">
                                    <td>
                                    </td>
                                    <td style="width: 50px;">
                                        Req ID
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
                                    <td style="width: 30px;">
                                        Pax
                                    </td>
                                    <td style="width: 30px;">
                                        RFQ
                                    </td>
                                    <td style="width: 30px;">
                                        QTN
                                    </td>
                                    <td style="width: 90px;">
                                        Earliest Expiry Date
                                    </td>
                                    <td style="width: 140px;">
                                        Requested By
                                    </td>
                                    <td style="width: 50px;">
                                        Status
                                    </td>
                                    <td style="width: 50px">
                                        Remarks
                                    </td>
                                    <td>
                                        Seaman?
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="grid-row-data">
                                <td>
                                    <img src="images/plus.png" alt="" name="Expand" child='<%#Eval("id")%>' onclick='toggleChild(this, <%#Eval("id")%>)' />
                                </td>
                                <td>
                                    <%#Eval( "id")%>
                                </td>
                                <td>
                                    <%#Eval("vessel_name")%>
                                </td>
                                <td align="left">
                                    <div class="Rout-overout">
                                        <input id="hdfRoutReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <asp:Label ID="lblRountInfo" CssClass="clickable" ForeColor="Blue" runat="server"
                                            Text='<%#Eval("FlightRoute")%>'></asp:Label>
                                    </div>
                                    <%--  <%#Eval("FlightRoute")%>--%>
                                </td>
                                <td>
                                    <%#Eval("departureDate","{0:dd/MM/yyyy}")%>
                                </td>
                                <td>
                                    <%#Eval("classOfTravel")%>
                                </td>
                                <td>
                                    <%#Eval("QuoteDueDate", "{0:dd/MM/yyyy}")%>
                                </td>
                                <td>
                                    <div class="PaxCount-overout">
                                        <input id="hdfReqID" type="hidden" value='<%#Eval("id")%>' />
                                        <asp:Label ID="lblPaxCount" CssClass="clickable" ForeColor="Blue" runat="server"
                                            Text='<%#Eval("PaxCount")%>'></asp:Label>
                                    </div>
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
                                <td>
                                    <%#Eval("OptionExpiry")%>
                                </td>
                                <td>
                                    <%#Eval("created_by")%>
                                </td>
                                <td>
                                    <%#Eval("CurrentStatus")%>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Image ID="imgRemark" runat="server" ImageUrl="images/remarks.gif" />
                                </td>
                                <td style="text-align: center;">
                                    <%#Eval("isSeaman")%>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptChild" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestPax") %>'
                                OnItemCommand="rptChild_ItemCommand">
                                <HeaderTemplate>
                                    <tr class="grid-childrow-header" style="display: none;">
                                        <td style="background-color: white">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <span>Code</span>
                                        </td>
                                        <td>
                                            <span>Sur Name</span>
                                        </td>
                                        <td colspan="3">
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
                                        <td colspan="2">
                                            <span>Place Of Birth</span>
                                        </td>
                                        <td>
                                            Personal?
                                        </td>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="text-align: left; display: none;" class='grid-childrow-data child<%#((System.Data.DataRow)Container.DataItem)["requestid"]%>'>
                                        <td style="background-color: white">
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
                                        <td colspan="3">
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
                                        <td colspan="2">
                                            <%# ((System.Data.DataRow)Container.DataItem)["Staff_Born_Place"]%>
                                        </td>
                                        <td>
                                            <%# ((System.Data.DataRow)Container.DataItem)["isPersonalTicket"]%>
                                        </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CausesValidation="False"
                                                CommandName="removepax" BorderWidth="0px" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem)["id"]%>'
                                                OnClientClick="return confirm('This will DELETE the Pax from travel request. Do you want to proceed?')"
                                                AlternateText="Delete"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <center>
            <br />
            <asp:UpdatePanel ID="updSupplier" runat="server">
                <ContentTemplate>
                    <table width="98%" style="font-size: 11px">
                        <tr>
                            <td style="width: 200px; text-align: right; padding-right: 5px; color: Black; font-weight: bold">
                                Select Quote Due Date :
                            </td>
                            <td colspan="2" style="text-align: left; padding-left: 5px">
                                <asp:TextBox ID="txtQuoteBy" Width="100px" runat="server" BackColor="LightYellow"
                                    Text=""></asp:TextBox>
                                <asp:DropDownList ID="ddlDepHours1" DataSourceID="ObjectDataSourceHour" DataTextField="HrText"
                                    DataValueField="HrValue" runat="server" />
                                <asp:DropDownList ID="ddlDepMins1" DataSourceID="ObjectDataSourceMinute" DataTextField="MnText"
                                    DataValueField="MnValue" runat="server" />
                                <asp:ObjectDataSource ID="ObjectDataSourceHour" TypeName="SMS.Business.TRAV.BLL_TRV_QuoteRequest"
                                    SelectMethod="AddHour" runat="server"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="ObjectDataSourceMinute" TypeName="SMS.Business.TRAV.BLL_TRV_QuoteRequest"
                                    SelectMethod="AddMinute" runat="server"></asp:ObjectDataSource>
                                <ajaxToolkit:CalendarExtender ID="cal" TargetControlID="txtQuoteBy" runat="server"
                                    Format="dd-MMM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtQuoteBy" ValidationGroup="sendrfq"
                                    runat="server" ControlToValidate="txtQuoteBy" Display="None" ErrorMessage="Please select Quote due date !"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtQuoteBy" TargetControlID="RequiredFieldValidatortxtQuoteBy"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px; text-align: right; padding-right: 5px; color: Black; font-weight: bold">
                                Supplier name/Country :
                            </td>
                            <td style="width: 200px; text-align: left; padding-left: 5px">
                                <asp:TextBox ID="txtSupplierSearch" Width="180px" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: left; padding-left: 20px">
                                <asp:Button ID="btnSearchSupplier" runat="server" Text="Search" OnClick="btnSearchSupplier_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GrdSupplier" runat="server" Width="99%" AutoGenerateColumns="false"
                        OnRowDataBound="GrdSupplier_RowDataBound">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "supplier")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "fullname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "email")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkSelect" name="chkSelect" value='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                        style="border: 0px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:Button ID="cdmSend" runat="server" Text="Send Now" Width="100px" Height="35px"
                ValidationGroup="sendrfq" OnClick="sendRFQ" />
        </center>
        <asp:ObjectDataSource ID="objRequest" runat="server" SelectMethod="GetAllRequests"
            TypeName="SMS.Business.TRAV.BLL_TRV_TravelRequest"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
