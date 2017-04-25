<%@ Page Title="License Key" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ShipLicenseKeyGeneration.aspx.cs" Inherits="ShipLicenseKeyGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 80%;
            height: 100%;">
           <div class="page-title">
                           License Key Generation
                        </div>
            <div style="height: 650px; width: 100%; color: Black;">
                <asp:UpdatePanel ID="updLicenseKey" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 5%;">
                                        Vessel&nbsp;:&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="DDLVessel" Width="100%" runat="server"/>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        Status&nbsp;:&nbsp;
                                    </td>
                                    <td style="width: 20%">
                                    <div border:1px solid #cccccc;">
                                        <asp:RadioButtonList ID="optStatus" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Value="0" Text="Not Sent&nbsp;" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Sent"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </div>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Search Text&nbsp;:&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%"></td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center; width: 100%">
                            <div style="text-align: right; width: 100%">
                            <asp:Button ID="btnGenerateKey" Text="Generate License key and send to vessel" Height="30px" runat="server" Width="250px"
                                    onclick="btnGenerateKey_Click" />

                            </div>
                            <asp:GridView ID="gvLicenseKey" runat="server" CellPadding="1" OnRowDataBound="gvLicenseKey_RowDataBound"
                                EmptyDataText="NO RECORDS FOUND!" AutoGenerateColumns="False" Width="100%" GridLines="Both">
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                               <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblID" Visible="false" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%#Eval("VESSEL_ID")%>'></asp:Label>
                                            <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PC-Name">
                                        <HeaderTemplate>
                                            PC Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPC_NAME" runat="server" Text='<%# Bind("PC_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IP ADDRESS">
                                        <HeaderTemplate>
                                            IP ADDRESS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIP_ADDRESS" runat="server" Text='<%# Bind("IP_ADDRESS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Autorized Key">
                                        <HeaderTemplate>
                                            Autorized Key
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAutorizedKey" runat="server" Text='<%# Bind("AUTHORIZED_KEY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Product Key">
                                        <HeaderTemplate>
                                            Product Key
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductKey" runat="server" Text='<%# Bind("LICENSE_KEY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Send Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkStatus" Checked='<%#Convert.ToString(Eval("Status"))=="1"?true : false %>' Enabled='<%#Convert.ToString(Eval("Status"))=="0"?true : false %>'   runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindLicenseKey" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
