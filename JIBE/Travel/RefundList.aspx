<%@ Page Title="Refund Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="RefundList.aspx.cs" Inherits="RefundList" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
   
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
   
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.draggable').draggable();
        });

        function ShowEditBox(id) {
            getOBJ("<%=hdRefundID.ClientID%>").value = id;
            showModal('dvRefund');


        }

        function openAttachments(ReqID) {
            var url = 'Attachment.aspx?atttype=DOCUMENT&requestid=' + ReqID;

            OpenPopupWindow('AttachmentWID', 'Attachments', url, 'popup', 550, 900, null, null, true, false, true, Attachment_Closed);

        }
        function Attachment_Closed() {

            return true;
        }

        function openRemarks(ReqID) {
            var url = 'Remarks.aspx?requestid=' + ReqID;

            OpenPopupWindow('RemarksWID', 'Remarks', url, 'popup', 550, 900, null, null, true, false, true, Remarks_Closed);

        }
        function Remarks_Closed() {

            return true;
        }

        function openeTicket(ReqID) {
            var url = 'eTicketList.aspx?requestid=' + ReqID;

            OpenPopupWindow('eTicketWID', 'e-Ticket List', url, 'popup', 400, 600, null, null, true, false, true, eTicket_Closed);
        }
        function eTicket_Closed() {

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

  <div class="page-title">
      Refund Requests
    
    </div>
  <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../Images/loaderbar.gif"alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <asp:HiddenField ID="hdRefundID" runat="server" />
   
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px; min-height: 620px; overflow: auto;">
        <table style="width: 100%;">
            <tr>
                <td>
                    Fleet
                </td>
                <td>
                    <asp:DropDownList ID="cmbFleet" runat="server" AutoPostBack="true" DataSourceID="objFleet"
                        Width="120px" DataTextField="name" AppendDataBoundItems="true" DataValueField="code"
                        OnSelectedIndexChanged="cmbFleet_SelectedIndexChanged">
                        <asp:ListItem Text="-Select All-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Sector
                </td>
                <td>
                    From&nbsp;<asp:TextBox ID="txtSectorFrom" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    To&nbsp;<asp:TextBox ID="txtSectorTo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch"  runat="server" CssClass="btnCSS"
                        Text="Search" OnClick="btnSearch_Click" UseSubmitBehavior="False" />&nbsp;</td>
            </tr>
            <tr>
                <td>
                    Vessel
                </td>
                <td>
                    <asp:DropDownList ID="cmbVessel" runat="server" AutoPostBack="true" DataSourceID="objVessel"
                        Width="120px" DataTextField="Vessel_Name" DataValueField="Vessel_id" OnDataBound="cmbVessel_OnDataBound"
                        OnSelectedIndexChanged="cmbVessel_SelectedIndexChanged">
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
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Travel Agent
                </td>
                <td>
                    <asp:DropDownList ID="cmbSupplier" AppendDataBoundItems="true" AutoPostBack="true"
                        Width="120px" runat="server" DataSourceID="objSupplier" DataTextField="fullname"
                        DataValueField="id" OnSelectedIndexChanged="cmbSupplier_SelectedIndexChanged">
                        <asp:ListItem Text="-Select All-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Name of Person
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtPaxName" runat="server" Width="98%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <asp:UpdatePanel ID="updList" runat="server">
            <ContentTemplate>
                <div id="dvRefund" class="draggable" style="position: absolute; display: none; width: 700px"
                    title=" Update Refund">
                    <table>
                        <tr>
                            <td>
                                No Show Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoShowAmount" onblur="return IsNumeric(id);" runat="server"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                Cancellation Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtCancellationAmount" onblur="return IsNumeric(id);" runat="server"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                Refund Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtRefundAmount" onblur="return IsNumeric(id);" runat="server"
                                    Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Amount Received </b>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtAmountReceived" onblur="return IsNumeric(id);" runat="server"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <h3>
                                    <b>Remarks</b></h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txtRefundRemark" runat="server" TextMode="MultiLine" Width="99%"
                                    Height="30px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <center>
                        <asp:Button ID="cmdUpdteRefund" runat="server" Text="Update Refund" OnClick="cmdUpdteRefund_Click" />
                        &nbsp;
                        <input type="button" id="cmdClose" value="Close" onclick='hideModal("dvRefund")' />
                    </center>
                    <br />
                </div>
                <br />
                <asp:Repeater ID="rptParent" DataMember="Request" runat="server" OnItemCommand="rptParent_ItemCommand"
                    OnItemDataBound="rptParent_OnItemDataBound" >
                    <HeaderTemplate>
                        <table cellpadding="1px" cellspacing="1px" style="width: 100%;" border="0">
                            <tr class="grid-row-header">
                                <td>
                                </td>
                                <td>
                                    Req ID
                                </td>
                                <td style="width: 150px;">
                                    Vessel Name
                                </td>
                                <td style="width: 160px;">
                                    Route
                                </td>
                                <td style="width: 80px;">
                                    Departure Date
                                </td>
                                <td style="width: 50px;">
                                    No Of Pax
                                </td>
                                <td style="width: 50px;">
                                    Approved By
                                </td>
                                <td style="width: 80px">
                                    Approve Date
                                </td>
                                <td style="width: 40px">
                                    Total Amount
                                </td>
                                <td style="width: 140px;">
                                    Requested By
                                </td>
                                <td style="width: 40px">
                                    No Show
                                </td>
                                <td style="width: 40px">
                                    Cancellation
                                </td>
                                <td style="width: 40px">
                                    Refund Amt
                                </td>
                                <td style="width: 40px">
                                    Rec. Amt
                                </td>
                                <td style="width: 80px">
                                    Agent
                                </td>
                                <td style="width: 80px">
                                    PIC
                                </td>
                                <td style="width: 40px;">
                                    Attach
                                </td>
                                <td style="width: 40px">
                                    Remarks
                                </td>
                                <td style="width: 50px;">
                                    E-Ticket
                                </td>
                                <td style="width: 50px;">
                                    Status
                                </td>
                                <td style="text-align: center;">
                                    Actions
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr  class="Repeater-RowStyle-css"">
                            <td>
                                <img src="images/plus.png" alt="" name="Expand" child='<%#Eval("requestid")%>' onclick='toggleChild(this, <%#Eval("requestid")%>)' />
                            </td>
                            <td>
                                <%#Eval("requestid")%>
                            </td>
                            <td>
                                <%#Eval("vessel_name")%>
                            </td>
                            <td>
                                <%#Eval("FlightRoute")%>
                            </td>
                            <td>
                                <%#Eval("departureDate","{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                <%#Eval("PaxCount")%>
                            </td>
                            <td>
                                <%#Eval("request_approved_by")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "request_date_of_approval", "{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                <%#Eval("Total_Amount")%>
                            </td>
                            <td>
                                <%#Eval("created_by")%>
                            </td>
                            <td>
                                <%#Eval("no_show_amount")%>
                            </td>
                            <td>
                                <%#Eval("Cancellation_Amount")%>
                            </td>
                            <td>
                                <%#Eval("Refund_Amount")%>
                            </td>
                            <td>
                                <%#Eval("amount_received")%>
                            </td>
                            <td>
                                <%#Eval("short_name")%>
                            </td>
                            <td>
                                <%#Eval("pic")%>
                            </td>
                            <td>
                                <center>
                                    <img src="images/attach-icon.gif" class="clickable" alt="Attach documents" onclick='openAttachments(<%#DataBinder.Eval(Container.DataItem, "id")%>)' />
                                </center>
                            </td>
                            <td>
                                <center>
                                    <img src="images/remarks.gif" class="clickable" alt="View/Add Remarks" onclick="openRemarks(<%#DataBinder.Eval(Container.DataItem, "id")%>);" />
                                </center>
                            </td>
                            <td>
                                <center>
                                    <img src="images/e-ticket.gif" alt="E-Tickets" class="clickable" onclick="openeTicket(<%#Eval("id") %>)" />
                                </center>
                            </td>
                            <td>
                                Refund Open
                            </td>
                            <td>
                                <center>
                                    <img src="images/Refund.png" alt="Update Info" class="clickable" onclick='ShowEditBox(<%#DataBinder.Eval(Container.DataItem, "id")%>);return false;' />
                                </center>
                            </td>
                        </tr>

                        <asp:Repeater ID="rptChild" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestPax") %>'
                            OnItemCommand="rptChild_ItemCommand">
                            <HeaderTemplate>
                                <tr class="grid-childrow-header" style="display: none; text-align: left;">
                                    <td colspan="2" style="background-color: white">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <span>Code</span>
                                    </td>
                                    <td>
                                        <span>Sur Name</span>
                                    </td>
                                    <td colspan="4">
                                        <span>Given Name</span>
                                    </td>
                                    <td colspan="2">
                                        <span>Rank</span>
                                    </td>
                                    <td colspan="2">
                                        Nationality
                                    </td>
                                    <td colspan="3">
                                        <span>DOB</span>
                                    </td>
                                    <td colspan="3">
                                        <span>Place Of Birth</span>
                                    </td>
                                    <td colspan="3">
                                        Personal?
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="text-align: left; display: none;" class='child<%#((System.Data.DataRow)Container.DataItem)["requestid"]%>'>
                                    <td colspan="2" style="background-color: white">
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
                                    <td colspan="4">
                                        <%# ((System.Data.DataRow)Container.DataItem)["staff_name"]%>
                                    </td>
                                    <td colspan="2">
                                        <%# ((System.Data.DataRow)Container.DataItem)["CurrentRank"]%>
                                    </td>
                                    <td colspan="2">
                                        <%# ((System.Data.DataRow)Container.DataItem)["Staff_Nationality"]%>
                                    </td>
                                    <td colspan="3">
                                        <%# ((System.Data.DataRow)Container.DataItem)["Staff_Birth_Date"]%>
                                    </td>
                                    <td colspan="3">
                                        <%# ((System.Data.DataRow)Container.DataItem)["Staff_Born_Place"]%>
                                    </td>
                                    <td colspan="3">
                                        <%# ((System.Data.DataRow)Container.DataItem)["isPersonalTicket"]%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                       <tr  class=" Repeater-AlternatingRowStyle-css"">
                            <td>
                                <img src="images/plus.png" alt="" name="Expand" child='<%#Eval("requestid")%>' onclick='toggleChild(this, <%#Eval("requestid")%>)' />
                            </td>
                            <td>
                                <%#Eval("requestid")%>
                            </td>
                            <td>
                                <%#Eval("vessel_name")%>
                            </td>
                            <td>
                                <%#Eval("FlightRoute")%>
                            </td>
                            <td>
                                <%#Eval("departureDate","{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                <%#Eval("PaxCount")%>
                            </td>
                            <td>
                                <%#Eval("request_approved_by")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "request_date_of_approval", "{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                <%#Eval("Total_Amount")%>
                            </td>
                            <td>
                                <%#Eval("created_by")%>
                            </td>
                            <td>
                                <%#Eval("no_show_amount")%>
                            </td>
                            <td>
                                <%#Eval("Cancellation_Amount")%>
                            </td>
                            <td>
                                <%#Eval("Refund_Amount")%>
                            </td>
                            <td>
                                <%#Eval("amount_received")%>
                            </td>
                            <td>
                                <%#Eval("short_name")%>
                            </td>
                            <td>
                                <%#Eval("pic")%>
                            </td>
                            <td>
                                <center>
                                    <img src="images/attach-icon.gif" class="clickable" alt="Attach documents" onclick='openAttachments(<%#DataBinder.Eval(Container.DataItem, "id")%>)' />
                                </center>
                            </td>
                            <td>
                                <center>
                                    <img src="images/remarks.gif" class="clickable" alt="View/Add Remarks" onclick="openRemarks(<%#DataBinder.Eval(Container.DataItem, "id")%>);" />
                                </center>
                            </td>
                            <td>
                                <center>
                                    <img src="images/e-ticket.gif" alt="E-Tickets" class="clickable" onclick="openeTicket(<%#Eval("id") %>)" />
                                </center>
                            </td>
                            <td>
                                Refund Open
                            </td>
                            <td>
                                <center>
                                    <img src="images/Refund.png" alt="Update Info" class="clickable" onclick='ShowEditBox(<%#DataBinder.Eval(Container.DataItem, "id")%>);return false;' />
                                </center>
                            </td>
                        </tr>

                        <asp:Repeater ID="rptChild" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestPax") %>'
                            OnItemCommand="rptChild_ItemCommand">
                            <HeaderTemplate>
                                <tr class="grid-childrow-header" style="display: none; text-align: left;">
                                    <td colspan="2" style="background-color: white">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <span>Code</span>
                                    </td>
                                    <td>
                                        <span>Sur Name</span>
                                    </td>
                                    <td colspan="4">
                                        <span>Given Name</span>
                                    </td>
                                    <td colspan="2">
                                        <span>Rank</span>
                                    </td>
                                    <td colspan="2">
                                        Nationality
                                    </td>
                                    <td colspan="3">
                                        <span>DOB</span>
                                    </td>
                                    <td colspan="3">
                                        <span>Place Of Birth</span>
                                    </td>
                                    <td colspan="3">
                                        Personal?
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="text-align: left; display: none;" class='child<%#((System.Data.DataRow)Container.DataItem)["requestid"]%>'>
                                    <td colspan="2" style="background-color: white">
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
                                    <td colspan="4">
                                        <%# ((System.Data.DataRow)Container.DataItem)["staff_name"]%>
                                    </td>
                                    <td colspan="2">
                                        <%# ((System.Data.DataRow)Container.DataItem)["CurrentRank"]%>
                                    </td>
                                    <td colspan="2">
                                        <%# ((System.Data.DataRow)Container.DataItem)["Staff_Nationality"]%>
                                    </td>
                                    <td colspan="3">
                                        <%# ((System.Data.DataRow)Container.DataItem)["Staff_Birth_Date"]%>
                                    </td>
                                    <td colspan="3">
                                        <%# ((System.Data.DataRow)Container.DataItem)["Staff_Born_Place"]%>
                                    </td>
                                    <td colspan="3">
                                        <%# ((System.Data.DataRow)Container.DataItem)["isPersonalTicket"]%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                   
               
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                     <div>
                        <uc2:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindRefundList"
                            AlwaysGetRecordsCount="true" />
                    </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="objRequests" runat="server" SelectMethod="GetRefund_RequestList"
        TypeName="SMS.Business.TRAV.BLL_TRV_Refund"></asp:ObjectDataSource>
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
             <asp:SessionParameter Name="VesselManager" SessionField="USERCOMPANYID" Type="Int32" />
            
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
