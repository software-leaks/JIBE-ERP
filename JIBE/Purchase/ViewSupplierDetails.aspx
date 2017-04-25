<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewSupplierDetails.aspx.cs" Inherits="Technical_INV_ViewSupplierDetails"
    Title="View Supplier Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table cellpadding="0" cellspacing="0" style="height: 16px; width: 100%; background-color: #C0C0C0;">
            <tr>
                <td style="background-color: #808080; font-size: small; color: #FFFFFF; width: 100%;">
                    <b>Supplier Details</b>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="900px" style="font-family: Verdana;
            font-size: small;">
            <tr>
                <td align="right" valign="top" style="width: 50%">
                    <asp:Panel ID="pnlSupplierBasicDetails" runat="server" GroupingText="Supplier Basic Details"
                        Style="font-size: small" Width="99%">
                        <table cellpadding="1" cellspacing="1" align="left" width="100%">
                            <tr align="left">
                                <td style="width: 10%; font-size: small; color: #333333;">
                                    <b>Supplier Code: </b>
                                </td>
                                <td style="width: 27%;">
                                    <asp:TextBox ID="txtSupplierCode" runat="server" Width="200px" ReadOnly="True" Font-Size="small"></asp:TextBox>
                                </td>
                                <td style="width: 5%; font-size: small; color: #333333;">
                                    <b>Name: </b>
                                </td>
                                <td style="width: 27%; font-size: small;">
                                    <asp:TextBox ID="txtSupplierName" runat="server" Width="220px" ReadOnly="True" Font-Size="small"></asp:TextBox>
                                </td>
                                <td style="width: 5%; font-size: small; color: #333333;">
                                    <b>Type: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtSupplierType" runat="server" Width="200px" ReadOnly="True" Font-Size="small"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 10%; font-size: small; color: #333333;">
                                    <b>Full Name: </b>
                                </td>
                                <td style="font-size: small; width: 66%;" colspan="3">
                                    <asp:TextBox ID="txtFullName" runat="server" Width="503px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 5%; font-size: small; color: #333333;">
                                    <b>Cotegory: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtSupplierCategory" runat="server" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 10%; font-size: small; color: #333333;">
                                    <b>Company Type: </b>
                                </td>
                                <td style="width: 27%;">
                                    <asp:TextBox ID="txtCompanyType" runat="server" Width="200px" ReadOnly="True" Font-Size="small"></asp:TextBox>
                                </td>
                                <td style="width: 5%; font-size: small; color: #333333;">
                                    <b>Company Status: </b>
                                </td>
                                <td style="width: 27%; font-size: small;">
                                    <asp:TextBox ID="txtCompanyStatus" runat="server" Width="220px" ReadOnly="True" Font-Size="small"></asp:TextBox>
                                </td>
                                <td style="width: 5%; font-size: small; color: #333333;">
                                    <b>Company Currency: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtCompanyCurrency" runat="server" Width="200px" ReadOnly="True"
                                        Font-Size="small"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" style="width: 50%">
                    <asp:Panel ID="pnlAddress" runat="server" GroupingText="Supplier Address" Style="font-size: small"
                        Width="99%">
                        <table cellpadding="1" cellspacing="1" align="left" width="100%">
                            <tr align="left">
                                <td style="width: 7%; font-size: small; color: #333333;">
                                    <b>Address 1: </b>
                                </td>
                                <td style="width: 40%; font-size: small;">
                                    <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" Width="350px" Font-Size="small"
                                        Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 7%; font-size: small; color: #333333;">
                                    <b>Address 2: </b>
                                </td>
                                <td style="width: 40%; font-size: small;">
                                    <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" Width="350px" Font-Size="small"
                                        Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 7%; font-size: small; color: #333333;">
                                    <b>Address 3: </b>
                                </td>
                                <td style="width: 40%; font-size: small;">
                                    <asp:TextBox ID="txtAddress3" runat="server" TextMode="MultiLine" Width="350px" Font-Size="small"
                                        Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 7%; font-size: small; color: #333333;">
                                    <b>Address 4: </b>
                                </td>
                                <td style="width: 40%; font-size: small;">
                                    <asp:TextBox ID="txtAddress4" runat="server" TextMode="MultiLine" Width="350px" Font-Size="small"
                                        Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 7%; font-size: small; color: #333333;">
                                    <b>Address 5: </b>
                                </td>
                                <td style="width: 40%; font-size: small;">
                                    <asp:TextBox ID="txtAddress5" runat="server" TextMode="MultiLine" Width="350px" Font-Size="small"
                                        Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 7%; font-size: small; color: #333333;">
                                </td>
                                <td style="width: 40%; font-size: small;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" style="width: 50%">
                    <asp:Panel ID="pnlContact" runat="server" GroupingText="Supplier Contact Details"
                        Style="font-size: small" Width="99%">
                        <table cellpadding="1" cellspacing="1" align="left" width="100%">
                            <tr align="left">
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>City: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtCity" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Country: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtCountry" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 4%; font-size: small; color: #333333;">
                                    <b>Postal: </b>
                                </td>
                                <td style="width: 30%; font-size: small;">
                                    <asp:TextBox ID="txtPostal" runat="server" Width="250px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Phone: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtPhone" runat="server" Width="200px" ReadOnly="True" TextMode="MultiLine"
                                        Font-Size="small" Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Extension: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtExtension" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 4%; font-size: small; color: #333333;">
                                    <b>Email: </b>
                                </td>
                                <td style="width: 30%; font-size: small;">
                                    <asp:TextBox ID="txtEmail" runat="server" Width="250px" ReadOnly="True" TextMode="MultiLine"
                                        Font-Size="small" Font-Names="Verdana"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Fax: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtFax" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 6%; font-size: small; color: #333333;">
                                </td>
                                <td style="width: 23%; font-size: small;">
                                </td>
                                <td style="width: 4%; font-size: small; color: #333333;">
                                </td>
                                <td style="width: 30%; font-size: small;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" style="width: 50%">
                    <asp:Panel ID="pnlBankDetails" runat="server" GroupingText="Bank Details" Style="font-size: small"
                        Width="99%">
                        <table cellpadding="1" cellspacing="1" align="left" width="100%">
                            <tr align="left">
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Bank Name: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtBankName" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Branck: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtBranch" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 4%; font-size: small; color: #333333;">
                                    <b>Acc. Number: </b>
                                </td>
                                <td style="width: 30%; font-size: small;">
                                    <asp:TextBox ID="txtBankAccNumber" runat="server" Width="250px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Address: </b>
                                </td>
                                <td style="font-size: small;" colspan="5">
                                    <asp:TextBox ID="txtBankAddress" runat="server" Width="476px" ReadOnly="True" TextMode="MultiLine"
                                        Font-Size="small" Font-Names="Verdana"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>City: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtBankCity" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 6%; font-size: small; color: #333333;">
                                    <b>Country: </b>
                                </td>
                                <td style="width: 23%; font-size: small;">
                                    <asp:TextBox ID="txtBankCountry" runat="server" Width="200px" ReadOnly="True" Font-Size="small"
                                        Font-Names="Verdana"></asp:TextBox>
                                </td>
                                <td style="width: 4%; font-size: small; color: #333333;">
                                </td>
                                <td style="width: 30%; font-size: small;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" style="width: 50%">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Intermediary Bank Details" Style="font-size: small"
                        Width="99%">
                        <table cellpadding="1" cellspacing="1" align="left" width="100%">
                            
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
