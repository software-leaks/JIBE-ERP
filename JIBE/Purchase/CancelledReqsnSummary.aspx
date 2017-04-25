<%@ Page Title="Cancelled Requisition Summary" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CancelledReqsnSummary.aspx.cs" Inherits="Purchase_CancelledReqsnSummary" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="HeadCont" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        function DisplayTypeLog() {
            document.getElementById("dvreqsType").style.display = "block";
            return false;
        }

        function HideTypeLog() {
            document.getElementById("dvreqsType").style.display = "none";
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdatePanel ID="updReqsn" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" style="height: 16px; width: 100%; background-color: #C0C0C0;">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF; width: 50%;
                            text-align: right">
                            <b>Cancelled Requisition Details</b>
                        </td>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF; width: 15%;
                            text-align: right">
                            <b>Department :</b>
                        </td>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF; width: 40%;
                            text-align: left;">
                            <b>
                                <asp:Label ID="lblDepartment" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td>
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" valign="middle">
                       
                           
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" valign="top" style="width: 50%">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlVesselDetails" runat="server" GroupingText="Vessel Details" Style="font-size: small;"
                                            Width="99%">
                                            <table cellpadding="1" cellspacing="1" align="left" width="100%" style="height: 110px">
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Vessel Name: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblVessel" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Vessel Type: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblVesselType" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 25%; font-size: small; color: #333333;">
                                                        <b>Vessel Ex-Name(s): </b>
                                                    </td>
                                                    <td style="font-size: small;" colspan="3">
                                                        <asp:Label ID="lblVesselExName1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Vessel Hull No.: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblVesselHullNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>IMO No. :</b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblIMOno" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Built Yard: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblVesselYard" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Built Year: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblVesselDelvDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="Panel1" runat="server" GroupingText="Requisition Details" Style="font-size: small"
                                            Width="99%">
                                            <table cellpadding="1" cellspacing="1" align="left" width="100%" style="height: 100px">
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Req. Number: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblReqNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small;">
                                                        <b>Date: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblToDate" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small;">
                                                        <b style="color: #333333">Total Item: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblTotalItem" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333; font-weight: bold">
                                                        Reason for Req :
                                                    </td>
                                                    <td style="font-size: small;" colspan="3">
                                                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="30px" ReadOnly="true"
                                                            Width="310px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 20%; font-size: small; color: #333333; font-weight: bold">
                                                        Reqsn Type:
                                                    </td>
                                                    <td style="font-size: small;" colspan="2">
                                                        <asp:DropDownList ID="ddlReqsnType" Enabled="false" DataSourceID="objsrcReqsnType" DataTextField="Description"
                                                            DataValueField="code" runat="server" Width="200px">
                                                        </asp:DropDownList>
                                                        <asp:ObjectDataSource ID="objsrcReqsnType" SelectMethod="Get_ReqsnType" TypeName="ClsBLLTechnical.TechnicalBAL"
                                                            runat="server"></asp:ObjectDataSource>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnViewlogType" runat="server" OnClientClick="return DisplayTypeLog()"
                                                            Text="View Change Log" Width="130px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 20%; font-size: small; color: #333333; font-weight: bold">
                                                       
                                                    </td>
                                                    <td style="font-size: small;" colspan="2">
                                                      
                                                    </td>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="top" style="width: 50%">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="Panel2" runat="server" GroupingText="Maker Details" Style="font-size: small"
                                            Width="99%">
                                            <table cellpadding="1" cellspacing="1" align="left" width="100%" style="height: 110px">
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Maker Name: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMakerName" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Maker Contact: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMakerContact" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b style="color: #333333">Phone: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMakerPh" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b style="color: #333333">Extension: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMakerExtension" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small;">
                                                        <b style="color: #333333">Email: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMakerEmail" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>City: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMakerCity" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333; font-family: Tahoma">
                                                        <b>Address: </b>
                                                    </td>
                                                    <td style="font-size: small; width: 30%" colspan="3">
                                                        <%--<asp:Label ID="lblMarkerAdd" runat="server" Style="font-weight: 700"></asp:Label>--%>
                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="30px" ReadOnly="true"
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlMachineDetails" runat="server" GroupingText="Machinery Details"
                                            Style="font-size: small" Width="99%">
                                            <table cellpadding="1" cellspacing="1" align="left" width="100%" style="height: 100px">
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>M/c Sr. No.: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblMachinesrno" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Catalogue: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblCatalog" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Particulars: </b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblParticulars" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; font-size: small; color: #333333;">
                                                        <b>Model:</b>
                                                    </td>
                                                    <td style="width: 30%; font-size: small;">
                                                        <asp:Label ID="lblModel" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left">
                            <asp:Panel ID="Panel4" runat="server" GroupingText="Attached Files" Style="font-size: small"
                                Width="100%">
                                <asp:Repeater ID="rpAttachment" runat="server" OnItemDataBound="rpAttachment_ItemDataBound">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <%# Eval("SlNo") %>.
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="lnkAtt" runat="server" NavigateUrl='<%# Eval("File_Path")%>'> <%# Eval("File_Name")%>  </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left">
                            <asp:Panel ID="Panel5" runat="server" GroupingText="Crew List" Style="font-size: small">
                                <asp:GridView ID="gvCrewList" Width="100%" EmptyDataText="No record found." AutoGenerateColumns="true"
                                    runat="server">
                                    <HeaderStyle ForeColor="White" BackColor="#5D7B9D" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Panel ID="Panel3" runat="server" GroupingText="Items in Requisition" Style="font-size: small">
                                <%--     <div style="height: 200px; width: 880px; left: 0px; top: 0px; overflow: scroll">--%>
                                <telerik:RadGrid ID="rgdItems" runat="server" AutoGenerateColumns="False" HeaderStyle-Height="30px"
                                    GridLines="None" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
                                    Skin="Office2007" Style="margin-bottom: 0px" Width="100%" OnItemDataBound="rgdItems_ItemDataBound">
                                    <MasterTableView Width="100%">
                                        <RowIndicatorColumn Visible="False" ItemStyle-BackColor="AliceBlue">
                                            <HeaderStyle Width="0%" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                            <HeaderStyle Width="0%" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ItemID" HeaderText="ItemID" UniqueName="ItemID"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SrNo" HeaderText="Sr.No." UniqueName="ITEM_SERIAL_NO">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Drawing_Number" HeaderText="Drawing No" UniqueName="Drawing_Number"
                                                Visible="true">
                                                <ItemStyle Width="5%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Part_Number" HeaderText="Part No" UniqueName="Part_Number"
                                                Visible="true">
                                                <ItemStyle Width="5%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="ItemDesc" HeaderText="Description" UniqueName="ItemDesc"
                                                Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%#Eval("ItemDesc")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ItemUnit" HeaderText="Unit" UniqueName="ItemUnit">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ROB_QTY" HeaderText="ROB" UniqueName="ROB_QTY"
                                                Visible="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReqestedQty" HeaderText="Requested Qty" UniqueName="ReqestedQty"
                                                Visible="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ItemComments" HeaderText="Comments" UniqueName="ItemComments"
                                                Visible="true">
                                                <ItemStyle Width="15%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="ItemDetails" Display="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshortDesc" Text='<%#Eval("ItemDesc")%>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblLongDesc" Text='<%#Eval("Long_Description")%>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblComments" Text='<%#Eval("ItemComments")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None" />
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                                <%-- </div>--%>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvreqsType" style="display: none; height: 200px; width: 500px; overflow: auto;
            position: fixed; left: 15%; top: 35%; text-align: right; vertical-align: top;"
            class="popup-css">
            <div style="float: left; width: 90%; text-align: center">
                Reqsn Type Log
            </div>
            <div style="float: right; width: 10%">
                <img alt="close" onclick="return HideTypeLog()" src="../Images/Close.gif" height="15px"
                    width="15px" /></div>
            <asp:GridView ID="gvReqsnTypeLog" AutoGenerateColumns="true" Width="498px" EmptyDataText="No record found !"
                runat="server">
                <HeaderStyle CssClass="HeaderStyle-css" />
                <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" />
            </asp:GridView>
        </div>
    </center>
</asp:Content>


