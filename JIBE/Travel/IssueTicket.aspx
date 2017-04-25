<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueTicket.aspx.cs" Inherits="IssueTicket"
    EnableEventValidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <title>Issue Ticket</title>
     <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }    
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.draggable').draggable();
        });

        function AttacheTicket(id) {
            $("#dveTicket").show();
            getOBJ("<%=hdStaffID.ClientID%>").value = id;
        }
      
    </script>
</head>
<body style="background-color: White">
    <form id="frmTicket" runat="server">
    <asp:ScriptManager ID="scmgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress" style="z-index: 9999">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div>
            <asp:HiddenField ID="hdStaffID" runat="server" />
            <div>
                <center>
                    <h2>
                        Issue Ticket
                    </h2>
                </center>
            </div>
             <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
            <div id="dveTicket" class="draggable" style="display: none; width: 600px; position: absolute;
                left: 50px; top: 80px; background-color: #f8f8f8;">
            </div>
            <br />
            <div>
                <table cellpadding="2">
                    <tr>
                        <td style="text-align: left; font-size: 12px; color: Black; font-weight: bold; text-decoration: underline;">
                            Approved Option:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="rptParent" DataMember="Quotation" runat="server" OnItemCommand="rptParent_ItemCommand"
                                OnItemDataBound="rptParent_OnItemDataBound">
                                <HeaderTemplate>
                                    <table cellpadding="2px" cellspacing="1px" style="width: 100%;" border="0">
                                        <%--  class="header"--%>
                                        <tr class="grid-row-header" style="color: #FFF; font-weight: bold;">
                                            <td style="width: 80px;">
                                                GDS Locator
                                            </td>
                                            <td style="width: 200px;">
                                                Ticketing Deadline
                                            </td>
                                            <td style="width: 50px">
                                                Hours
                                            </td>
                                            <td style="width: 50px">
                                                Mins
                                            </td>
                                            <td style="width: 80px;">
                                                Fare
                                            </td>
                                            <td style="width: 80px;">
                                                Tax
                                            </td>
                                            <td style="width: 80px;">
                                                Total
                                            </td>
                                            <td style="width: 80px;">
                                                Currency
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="grid-row-data">
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "GDSLocator")%>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "TicketingDeadline")%>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "TimeHours")%>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "TimeMins")%>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "Fare")%>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "Tax")%>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem, "TotalFare")%>
                                        </td>
                                        <td>
                                            <%#Eval("Currency")%>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: 12px; color: Black; font-weight: bold; text-decoration: underline;">
                            Flight Details:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="rptChild" runat="server" OnItemCommand="rptChild_ItemCommand" OnItemDataBound="rptChild_OnItemDataBound">
                                <HeaderTemplate>
                                    <table cellpadding="4px" cellspacing="0px" style="width: 100%; border: 1px solid gray;">
                                        <tr class="grid-row-header" style="color: #FFF; font-weight: bold;">
                                            <td>
                                                Flight
                                            </td>
                                            <td>
                                                From
                                            </td>
                                            <td>
                                                To
                                            </td>
                                            <td>
                                                Departure
                                            </td>
                                            <td>
                                                Hours
                                            </td>
                                            <td>
                                                Mins
                                            </td>
                                            <td>
                                                Arrival
                                            </td>
                                            <td>
                                                Hours
                                            </td>
                                            <td>
                                                Mins
                                            </td>
                                            <td>
                                                Class
                                            </td>
                                            <td>
                                                Status
                                            </td>
                                            <td>
                                                Locator
                                            </td>
                                            <td style="width: 80px;">
                                                Action
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="grid-row-data">
                                        <td>
                                            <%# Eval("flightNo")%>
                                        </td>
                                        <td>
                                            <%# Eval("TravelFrom")%>
                                        </td>
                                        <td>
                                            <%# Eval("TravelTo")%>
                                        </td>
                                        <td>
                                            <%# Eval("DepartureDate")%>
                                        </td>
                                        <td>
                                            <%# Eval("TimeDepHours")%>
                                        </td>
                                        <td>
                                            <%# Eval("TimeDepMins")%>
                                        </td>
                                        <td>
                                            <%# Eval("ArrivalDate")%>
                                        </td>
                                        <td>
                                            <%# Eval("TimeArrHours")%>
                                        </td>
                                        <td>
                                            <%#Eval("TimeArrMins")%>
                                        </td>
                                        <td>
                                            <%# Eval("TravelClass")%>
                                        </td>
                                        <td>
                                            <%# Eval("FlightStatus")%>
                                        </td>
                                        <td>
                                            <%# Eval("AirlineLocator")%>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="14">
                                            <asp:GridView ID="grdPax" DataKeyNames="staffid" runat="server" Caption="" AutoGenerateColumns="False"
                                                DataSourceID="objPax">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name as Per Passport" HeaderStyle-Width="300px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStaffName" runat="server" Text='<%#Eval("Staff_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderTemplate>
                                                            <table cellpadding="2px" cellspacing="0px;" style="border-collapse: collapse">
                                                                <tr>
                                                                    <td style="width: 160px; text-align: center; color: Gray; font-size: 12px; font-weight: bold;
                                                                        font-family: Tahoma;">
                                                                        Ticket No
                                                                    </td>
                                                                    <td style="width: 160px; text-align: center; color: Gray; font-size: 12px; font-weight: bold;
                                                                        font-family: Tahoma;">
                                                                        Upload e-Ticket
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table cellpadding="2px" cellspacing="0px" width="100%">
                                                                <tr class="grid-row-data">
                                                                    <td style="width: 160px;">
                                                                        <asp:TextBox ID="txtETicketNumber" Width="155px" runat="server" Text=""></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:FileUpload ID="flName" Width="350px" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="cmdTicketNo" runat="server" CommandArgument='<%#Eval("staffid").ToString()+","+Eval("id").ToString()%>'
                                                                            Text="Update" OnClick="cmdTicketNo_onClick" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnPasteTicket" runat="server" OnClick="btnPasteTicket_Click" Text="Paste Ticket"
                                                                            CommandArgument='<%#Eval("staffid").ToString()+","+Eval("id").ToString()%>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:UpdatePanel ID="updtcklist" runat="server"   >
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCreateTicket" EventName="Click" />
            </Triggers>
            <ContentTemplate>
              <asp:GridView ID="grdTickets" Width="400px" runat="server" AutoGenerateColumns="False">
                <HeaderStyle HorizontalAlign="Left" CssClass="grid-row-header" />
                <SelectedRowStyle BackColor="LightGray" ForeColor="Blue" />
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-Ticket No.">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblTicketNo" runat="server" Text='<%#Eval("eTicketNumber") %>'
                                NavigateUrl='<%# "~/"+ ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString() + Eval("Attachment_Path").ToString() %>'
                                Target="_blank"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
          
            <asp:ObjectDataSource ID="objPax" runat="server" TypeName="SMS.Business.TRAV.BLL_TRV_TravelRequest"
                SelectMethod="Get_Request_Pax">
                <SelectParameters>
                    <asp:QueryStringParameter Name="RequestID" QueryStringField="requestid" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </center>
    <div id="dvCreateTicket" style="display: none; width: 500px; text-align: center">
       
        <CKEditor:CKEditorControl ID="txtMailBody" runat="server"></CKEditor:CKEditorControl>
        <br />
        <asp:UpdatePanel ID="updticket" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnCreateTicket" OnClientClick="hideModal('dvCreateTicket')" Text="Save Ticket"
                    runat="server" OnClick="btnCreateTicket_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
    </div>
     <asp:HiddenField ID="hdf_TicketNumber" runat="server" />
        <asp:HiddenField ID="hdf_PaxID" runat="server" />
        <asp:HiddenField ID="hdf_Flightid" runat="server" />
    </form>
</body>
</html>
