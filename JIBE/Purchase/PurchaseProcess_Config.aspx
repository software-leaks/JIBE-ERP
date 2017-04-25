<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PurchaseProcess_Config.aspx.cs"
    EnableEventValidation="false" Inherits="Purchase_PurchaseProcess_Config" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        #Text1
        {
            width: 75px;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                <b>Purchase Process Configuration </b>
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                        <table width="100%" cellpadding="4" cellspacing="4">
                            <tr align="left">
                                <td align="right" style="width: 20%">
                                    PO Type:&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchPOType" runat="server" Width="100%"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearchPOType"
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
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Department"  ImageUrl="~/Images/Add-icon.png" CommandArgument="Add"
                                        Text="Add Department" OnClick="imgclick" />

                                </td>
                                <td style="width: 5%">
                                    <asp:ImageButton ID="ImgExpExcel"  runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                            </tr>
                        </table>
                        </div>
                         
                        <div>

                            <asp:GridView  ID="rgdConfig" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                 Width="100%" GridLines="Both" AllowSorting="false"
                                CellPadding="1" CellSpacing="0" 
                                PageSize="20">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="PO Type" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            PO Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VARIABLE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Auto_Owner_Selection" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate >
                                           <%-- <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" ForeColor="Black">Auto_Owner_Selection&nbsp;</asp:LinkButton>--%>
                                           Auto Owner Selection
                                        </HeaderTemplate>
                                        <ItemTemplate >
                                           <%-- <asp:LinkButton ID="lblName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AutoOwnerSelection") %>'
                                                Style="color: Black"></asp:LinkButton>--%>
                                                  <asp:Label ID="lblName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AutoOwnerSelection") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                           <%-- <asp:LinkButton ID="lblCodeHeader" runat="server" CommandName="Sort" CommandArgument="Code"
                                                ForeColor="Black">Copy_To_Vessel&nbsp;</asp:LinkButton>--%>
                                            Copy To Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CopyToVessel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Auto_PO_Closing" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                           <%-- <asp:LinkButton ID="lblForm_TypeHeader" runat="server" CommandName="Sort" CommandArgument="Form_Type"
                                                ForeColor="Black">Auto_PO_Closing&nbsp;</asp:LinkButton>--%>
                                                Auto PO Closing
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblForm_Type" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AutoPOClosing") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgUpdate" CommandArgument='<%#Eval("Id")%>' runat="server"
                                                            Text="Update" ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" OnClick="updateClick"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" CommandArgument='<%#Eval("Id")%>' runat="server" OnClick="deleteClick"
                                                            Text="Delete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                            ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" 
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPOGrid" />
                            <%--<asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />--%>
                        </div>
                        <br />
                       
                        <%--<asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />--%>
                        
                        
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
