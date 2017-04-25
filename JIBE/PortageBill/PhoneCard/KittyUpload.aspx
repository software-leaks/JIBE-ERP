<%@ Page Title="Upload Kitty Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="KittyUpload.aspx.cs" Inherits="PortageBill_PhoneCard_KittyUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/uc_SupplierList.ascx"  TagName="uc_SupplierList" TagPrefix="ucSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js?v=2" type="text/javascript"></script>
      <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <style type="text/css">
        .noline
        {
            color: Lime;
            text-decoration: none;
        }
         .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: 0;">
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
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>Upload Card Voucher </b>
                </div>
                <div class="error-message" onclick="javascript:this.style.display='none';">
                  <asp:Label ID="lblMessage" runat="server"></asp:Label>
               </div>
                <div style="height: 850px; color: Black; width: 90%">
                    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                        <ContentTemplate>
                            <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="1" cellspacing="0">
                                    <tr>
                                        <td style="text-align: right;" width="10%">
                                            Vessels:
                                        </td>
                                        <td style="text-align: left;" width="20%">
                                            <asp:DropDownList ID="ddlVessels" runat="server" Width="135px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" width="10%">
                                            Document   :
                                        </td>
                                        <td align="left" width="40%" valign="middle">
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" Height="22px" Style="margin-left: 0px" Font-Names="Tahoma" />
                                           
                                        </td>
                                        <td width="20%" style="text-align: center;">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload"  Height="22px"   Width="80px" Font-Names="Tahoma"  ValidationGroup="Upload"  OnClick="btnUpload_Click">
                                            </asp:Button>
                                            <asp:Button ID="btnSave" Text="Save"  Height="22px"  Width="80px"  OnClick="btnSave_Click"   ValidationGroup="Upload" runat="server" Font-Names="Tahoma"/>
                                        </td>
                                         <td>
                                         <asp:Button ID="btnDownload" runat="server" Text="Download template file"   OnClick="btnDownload_Click"  Font-Names="Tahoma"
                                Height="22px"  Width="140px" /></td>
                                    </tr>
                                    <tr>
                                    <td style="text-align: right;height:28px" >
                                            Supplier:
                                        </td>
                                        <td>
                                            <ucSupplier:uc_SupplierList ID="uc_SupplierListRFQ" Width="150px" Height="22px" runat="server"  />
                                        </td>
                                        <td></td>
                                        <td> <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="FileUpload1" ValidationGroup="Upload"
                                                ErrorMessage="Only xls  are allowed."  ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.XLS)$"> 
                                            </asp:RegularExpressionValidator></td>

                                        <td></td>
                                       <td></td>
                                       
                                    </tr>
                                </table>
                            </div>
                            <div id="divGrid" style="z-index: 0; width: 80%;">
                                <asp:GridView ID="gvUploadKitty" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False"  CellPadding="3"
                                    CellSpacing="0" Width="100%" CssClass="GridView-css" GridLines="None" Font-Size="11px"
                                    AllowSorting="true">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trac Ref Number">
                                            <HeaderTemplate >Card Number </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbtnTracNumber" runat="server" Text='<%#Eval("[CardNumber]")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TC Name">
                                            <HeaderTemplate>Card PIN </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgpscode" runat="server" Text='<%#Eval("[pincode]")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel">
                                            <HeaderTemplate>Units</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("units")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <HeaderTemplate>Title</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Port">
                                            <HeaderTemplate>Sub Title</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPortName" runat="server" Text='<%#Eval("SubTitle")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30"  />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </center>
    </div>
</asp:Content>
