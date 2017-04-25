<%@ Page Title="Vessel Min CTM" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="VesselMinCTM.aspx.cs" Inherits="PortageBill_VesselMinCTM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
  <div class="page-title">
        Update Vessel Minimum CTM
    </div>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
        

          <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table  cellspacing="2">
                        <tr>
                            <td align="right">
                                Fleet
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td align="right">
                                Vessel Name
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
                    <asp:GridView ID="gvVesselMinCTM" DataKeyNames="VESSEL_ID" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" AllowPaging="True" PageSize="20" Width="100%" ShowFooter="false"
                        EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="None" 
                         ForeColor="#333333" CssClass="GridView">
                        <Columns>
                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Minimum CTM" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnVesselID" runat="server" Value='<%# Eval("Vessel_ID")%>'/>
                                    <asp:TextBox ID="txtMinCTM" runat="server" Text='<%# Eval("Min_CTM","{0:00.00}")%>'/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Last Updated By" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblPortAgent" runat="server" Text='<%# Eval("Updated_By")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:TemplateField>

                            <asp:TemplateField  HeaderText="Last Updated" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastUpdatedBy" runat="server"  Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Date_of_Creation"))) %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px"
                           />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css"  />
                        <RowStyle CssClass="RowStyle-css"  />
                        <EditRowStyle CssClass="RowStyle-css" BackColor="#7C6F57" />
                        <PagerStyle CssClass="PagerStyle-css" BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>
                <div style="text-align:center"><asp:Button ID="btnSave" text="Save" runat="server" OnClick="btnSave_Click" /></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
