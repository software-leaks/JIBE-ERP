<%@ Page Title="Travel Module - Invoice" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Travel_Invoice" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.draggable').draggable();
        });

        function openAttachments(ReqID) {
            var url = 'Attachment.aspx?atttype=INVOICE&requestid=' + ReqID;
            OpenPopupWindow('InvoiceWID', 'Attach Invoice', url, 'popup', 550, 900, null, null, true, false, true, Attachment_Closed);
        }
        function Attachment_Closed() {
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        <div>
            <asp:Label ID="lblPageTitle" runat="server" Text="Invoices"></asp:Label></div>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px; min-height: 620px; overflow: auto;">
        <asp:UpdatePanel ID="udpInvoice" runat="server">
            <ContentTemplate>
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
                            Trave Date
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="cmdAllInvoice" Width="120px" BackColor="LightSkyBlue" runat="server"
                                Text="All Invoice" OnClick="cmdAllInvoices_Click" />
                        </td>
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
                            From
                        </td>
                        <td>
                            <asp:TextBox ID="txtTrvDateFrom" runat="server" Width="80px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="TrvDateFrom" runat="server" TargetControlID="txtTrvDateFrom"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>
                            <asp:Button ID="cmdPending" Width="120px" BackColor="White" runat="server" Text="Pending Invoice"
                                OnClick="cmdPending_Click" />
                        </td>
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
                            To
                        </td>
                        <td>
                            <asp:TextBox ID="txtTrvDateTo" runat="server" Width="80px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="TrvDateTo" runat="server" TargetControlID="txtTrvDateTo"
                                Format="dd-MMM-yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>
                            <asp:Button ID="cmdClosed" Width="120px" BackColor="White" runat="server" Text="Paid Invoice"
                                OnClick="cmdClosed_Click" />
                        </td>
                    </tr>
                </table>
                <div id="dvInvoice">
                    <asp:GridView ID="grdInvoice" runat="server" CssClass="" Width="100%" AutoGenerateColumns="false"
                        OnRowCommand="grd_Invoice_RowCommand" OnRowDataBound="grd_Invoice_RowDataBound">
                        <HeaderStyle HorizontalAlign="Left" CssClass="grid-row-header" />
                        <RowStyle CssClass="grid-row-data" BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Req. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnRequestID" runat="server" Value='<%#Eval("id")%>' />
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel">
                                <ItemTemplate>
                                    <asp:Label ID="lblVName" runat="server" Text='<%#Eval("vessel_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Route">
                                <ItemTemplate>
                                    <asp:Label ID="lblRoute" runat="server" Text='<%#Eval("flightroute")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Travel Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeptDate" runat="server" Text='<%#Eval("departuredate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. Of Pax">
                                <ItemTemplate>
                                    <asp:Label ID="lblPax" runat="server" Text='<%#Eval("paxcount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approval Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDOA" runat="server" Text='<%#Eval("date_of_approval")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Event Closed?">
                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("Total_Amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("currentstatus")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvNo" runat="server" Text='<%#Eval("invoice_number")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvAmt" runat="server" Text='<%#Eval("invoice_amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid">
                                <ItemTemplate>                                    
                                    <asp:ImageButton ID="imgPaid" runat="server" CommandArgument='<%#Eval("id") %>' CommandName="PAID"
                                        Height="24px" ImageUrl="~/Travel/images/dollar.png" AlternateText="Mark as Paid" />
                                    <%--<img src="images/edit.gif" class="clickable" alt="" onclick='openAttachments(<%#Eval("id") %>)' />--%>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="text-align: right">
                    <asp:ImageButton ID="imgUploadInvoice" OnClick="imgUploadInvoice_Click" runat="server"
                        ImageUrl="Images/UploadInvoice.png" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvUploadInvoice" style="display: none; width: 600px;" title="Upload Invoice">
            <table cellpadding="4px" cellspacing="0px" style="border: 1px solid gray; background-color: #ffffff;
                width: 100%">
                <caption style="background-color: Gray; color: White;">
                    <b>Upload Invoice</b>
                </caption>
                <tr>
                    <td style="text-align: left;">
                        Invoice No.
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvNo" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Amount
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvAmount" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Currency
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbCurrency" Width="155px" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Due Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvDueDate" runat="server" Width="150px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CEDueDate" runat="server" TargetControlID="txtInvDueDate"
                            Format="dd-MMM-yyyy">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left;">
                        Remarks
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtInvoiceRemarks" runat="server" TextMode="MultiLine" Width="460px"
                            Height="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:FileUpload ID="flName" Width="380px" runat="server" />
                        <asp:Button ID="cmdUpload" runat="server" Text="Upload" OnClick="cmdUpload_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:ObjectDataSource ID="objVessel" runat="server" SelectMethod="Get_VesselList"
        TypeName="SMS.Business.Infrastructure.BLL_Infra_VesselLib">
        <SelectParameters>
            <asp:ControlParameter ControlID="cmbFleet" Name="FleetID" Type="Int32" DefaultValue="0"
                PropertyName="SelectedValue" />
            <asp:Parameter Name="VesselID" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="VesselManager" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="SearchText" Type="String" />
            <asp:Parameter DefaultValue="0" Name="UserCompanyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="objFleet" runat="server" SelectMethod="GetFleetList_DL"
        TypeName="SMS.Data.Infrastructure.DAL_Infra_VesselLib">
        <SelectParameters>
           <asp:SessionParameter DefaultValue="1" Name="CompanyID" SessionField="UserCompanyID"  Type="Int32" />
            <asp:Parameter DefaultValue="89" Name="VesselManager" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="objSupplier" runat="server" SelectMethod="Get_SupplierList"
        TypeName="SMS.Business.TRAV.BLL_TRV_Supplier">
        <SelectParameters>
            <asp:Parameter Name="Supplier_Search" Type="String" ConvertEmptyStringToNull="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
