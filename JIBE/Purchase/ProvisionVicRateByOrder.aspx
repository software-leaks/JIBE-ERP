<%@ Page Title="Victualing Rate By Order" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProvisionVicRateByOrder.aspx.cs" Inherits="Purchase_ProvisionVicRateByOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
            <div style="font-size: 24px; background-color: #5588BB; width: 800px; color: White;
                text-align: center;">
                <b>Victualing Rate by Order </b>
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                          <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="1" cellspacing="0">
                                <tr align="left">
                                    <td width="20%" align="right" valign="top">
                                        Fleet :
                                    </td>
                                    <td width="25%" valign="top" align="left">
                                        <uc1:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                            Height="150" Width="160" />
                                    </td>

                                    <td align="right" style="width: 25%">
                                        Order Code :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" Height="23" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                 
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%" align="right" valign="top">
                                        Vessel :
                                    </td>
                                    <td width="25%" valign="top" align="left">
                                        <uc1:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                            Height="200" Width="160" />
                                    </td>
                                     <td colspan="5"></td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x: hidden; overflow-y: none; height: 600px; width: 800px;">
                            <asp:GridView ID="rgdLocation" runat="server"  ShowHeaderWhenEmpty="true" 
                                EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                 Width="100%" GridLines="None" 
                                 CellPadding="4" onrowdatabound="rgdLocation_RowDataBound" 
                                onrowediting="rgdLocation_RowEditing" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <PagerStyle CssClass="PagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselID" runat="server" Style="display:none" ClientIDMode="Static" Text='<%# DataBinder.Eval(Container,"DataItem.VESSEL_CODE") %>'></asp:Label>
                                            <asp:Label ID="lblVesselName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                           Requisition Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <%# DataBinder.Eval(Container, "DataItem.Requisition_Code")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                          Order Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                          <%# DataBinder.Eval(Container, "DataItem.Order_Code")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                          Victualing Rate
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <%# DataBinder.Eval(Container, "DataItem.Order_Victuling_rate")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                          Delivery Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <%# DataBinder.Eval(Container, "DataItem.Delivery_Date")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    <HeaderTemplate>
                                        View Provision Details
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:LinkButton ID="lnkProvi" runat="server" ClientIDMode="Static" Text="View Provision Details" style="cursor:hand"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css" />                                    
                                    </asp:TemplateField>
                                  
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindVictuallingRate" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
                        </div>
                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="updProvision" ClientIDMode="Static" runat="server"><ContentTemplate>
        <div id="divProvision" title="<%=Title%>" style="font-family: Tahoma; color: black; display: none;width:750px">
        <center>
        <asp:GridView ID="gvVictuallingRate" runat="server" AutoGenerateColumns="false" Width="500px" ShowHeaderWhenEmpty="true"
                EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CssClass="gridmain-css"
                        BackColor="#D8D8D8" CellPadding="3" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="LAST_DRY_DELIVERY_DATE" HeaderText="Last Supply Date"  DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="CREDIT_DEBIT_DRY_DAYS" HeaderText="Credit/debit Days" />
                            <asp:BoundField DataField="CREDIT_DEBIT_DRY_AMOUNT" HeaderText="Credit/debit Amount" />
                            <asp:BoundField DataField="ORDER_DRY_AMOUNT" HeaderText="Current Amount" />
                            <asp:BoundField DataField="LAST_FRESH_DELIVERY_DATE" HeaderText="Last Supply Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CREDIT_DEBIT_FRESH_DAYS" HeaderText="Credit/debit Days" />
                            <asp:BoundField DataField="CREDIT_DEBIT_FRESH_AMOUNT" HeaderText="Credit/debit Amount" />
                            <asp:BoundField DataField="ORDER_FRESH_AMOUNT" HeaderText="Current Amount" />
                            <asp:BoundField DataField="CREW_COUNT" HeaderText="Crew OB" />
                            <asp:BoundField DataField="ALLOWANCE_STOCK_AMOUNT" HeaderText="5days stock" />
                            <asp:BoundField DataField="EXTRA_MEAL_AMOUNT" HeaderText="Extra Meal Amount" />
                            <asp:BoundField DataField="ORDER_VICTULING_RATE" HeaderText="Victualing Rate" ItemStyle-Font-Bold="true"  ItemStyle-ForeColor="Blue" />
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                    </asp:GridView>
        </center>
        </div>
        </ContentTemplate></asp:UpdatePanel>
    </center>
</asp:Content>

