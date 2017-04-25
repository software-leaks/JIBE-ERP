<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateConsolidatedPO.aspx.cs"
    Inherits="Technical_INV_CreateConsolidatedPO" Title="Consolidate PO" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">

        function Validation() {

            var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var cmdLogisticCompany = document.getElementById("ctl00_MainContent_DDLLogisticCompany").value;
            var cmdPort = document.getElementById("ctl00_MainContent_DDLPort").value;
            var txtDeliveryDate = document.getElementById("ctl00_MainContent_txtDeliveryDate").value;

            if (cmdFleet == "0" || cmdFleet == null) {
                alert("Select fleet and vessel to proceed.");
                return false;
            }

            if (cmdVessels == "--Select--" || cmdVessels == null) {
                alert("Select vessel to proceed.");
                return false;
            }

            if (cmdLogisticCompany == "0" || cmdLogisticCompany == null) {
                alert("Select logistic company.");
                return false;
            }

            if (txtDeliveryDate == "" || txtDeliveryDate == null) {
                alert("Select delivery date.");
                return false;
            }

            if (cmdPort == "0" || cmdPort == null) {
                alert("Select port name.");
                return false;
            }
            return true;

        }


    </script>
    <center>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                    <b>Consolidate PO</b>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 95%;">
            <tr align="left">
                <td style="font-size: small; background-color: #CCCCCC; color: #333333;">
                    <b>Fleet : </b>
                </td>
                <td style="font-size: small; background-color: #CCCCCC;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DDLFleet" runat="server" Width="109px" Font-Size="small"
                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">-Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="font-size: small; background-color: #CCCCCC;">
                </td>
                <td style="font-size: small; background-color: #CCCCCC;">
                </td>
                <td style="font-size: small; background-color: #CCCCCC;">
                </td>
                <td style="font-size: small; background-color: #C0C0C0;">
                </td>
                <td style="font-size: small; background-color: #CCCCCC;">
                </td>
            </tr>
            <tr align="left">
                <td style="width: 149px; font-size: small; background-color: #C0C0C0; color: #333333;">
                    <b>Vessel : </b>
                </td>
                <td style="width: 229px; font-size: small; background-color: #C0C0C0;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DDLVessel" runat="server" Width="109px" Font-Size="small"
                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                              
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 90px; font-size: small; background-color: #C0C0C0; color: #333333;">
                    <b>Delivery Date : </b>
                </td>
                <td style="font-size: small; background-color: #C0C0C0;">
                    <asp:TextBox ID="txtDeliveryDate" runat="server" Style="font-size: small"></asp:TextBox>
                    <RJS:PopCalendar ID="tocal" Control="txtDeliveryDate" runat="server" />
                </td>
                <td style="width: 95px; font-size: small; background-color: #C0C0C0;">
                    &nbsp;
                </td>
                <td style="font-size: small; background-color: #C0C0C0;">
                    &nbsp;
                </td>
            </tr>
            <tr align="left">
                <td style="width: 139px; font-size: small; background-color: #CCCCCC; color: #333333;">
                    <b style="color: #333333">Logistic Company : </b>
                </td>
                <td style="width: 217px; font-size: small; background-color: #CCCCCC;">
                    <asp:DropDownList ID="DDLLogisticCompany" runat="server" AppendDataBoundItems="True"
                        Font-Size="small" Width="194px">
                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 134px; font-size: small; background-color: #CCCCCC;">
                    <b style="color: #333333">Delivery Port: </b>
                </td>
                <td style="font-size: small; background-color: #CCCCCC;">
                    <asp:DropDownList ID="DDLPort" runat="server" AppendDataBoundItems="True" Style="font-size: small"
                        Width="194px">
                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="background-color: #CCCCCC">
                </td>
                <td style="background-color: #CCCCCC">
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" align="center" style="width: 95%; background-color: #999999;">
            <tr>
                <td>
                </td>
                <td style="width: 379px">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rblRaisedPO" runat="server" Width="439px" RepeatDirection="Horizontal"
                                AutoPostBack="True" Font-Size="small" ForeColor="White" Style="font-weight: 700;
                                color: #333333;" OnSelectedIndexChanged="rblRaisedPO_SelectedIndexChanged">
                                <asp:ListItem Text="Raised PO" Value="1" Selected="True"> </asp:ListItem>
                                <asp:ListItem Text="Non-Raised PO" Value="2"> </asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 100%">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="grvPOList" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                            Skin="WebBlue" Width="95%" AutoGenerateColumns="False">
                                            <MasterTableView>
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Font-Size="Smaller" Text='<%# Eval("ID") %>'
                                                                Visible="false" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="REQUISITION_CODE" HeaderText="Req. NO" UniqueName="REQUISITION_CODE">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DOCUMENT_CODE" HeaderText="DOCUMENT_CODE" Visible="False"
                                                        UniqueName="DOCUMENT_CODE">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SUPPLIER" HeaderText="SUPPLIER" Visible="False"
                                                        UniqueName="SUPPLIER">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Document_Date" HeaderText="Req. Date" UniqueName="Document_Date">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="QUOTATION_CODE" HeaderText="Quo. NO" UniqueName="QUOTATION_CODE">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ORDER_CODE" HeaderText="PO NO" UniqueName="ORDER_CODE">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Full_NAME" HeaderText="Supplier Name" ItemStyle-HorizontalAlign="Left" UniqueName="Full_NAME">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Line_Type" HeaderText="Status" UniqueName="Line_Type">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Select For Consolidate" UniqueName="chkForConsolidate">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkForConsolidate" runat="server" Font-Size="Smaller" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None" />
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%; background-color: #CCCCCC;">
                        <tr align="center">
                            <td align="center">
                                <asp:Label ID="lblMessage" runat="server" Text="" Visible="true" Style="color: Red"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSendToSupplier" runat="server" Text="Raise Consolidate PO" Width="149px"
                                            Style="font-size: small" OnClientClick="return Validation();" OnClick="btnSendToSupplier_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjSelectDeliveryPort" runat="server" TypeName="SMS.Business.PURC.BLL_PURC_Purchase"
            SelectMethod="getDeliveryPort"></asp:ObjectDataSource>
    </center>
</asp:Content>
