<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ROBLessThanMin.aspx.cs"
    Inherits="ROBLessThanMin" Title="ROBLessThanMin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <script language="javascript" type="text/javascript">

      

    </script>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 98%; height: 80px;
            height: 100%;">
            <div style="font-size: 24px; background-color: #5588BB; color: White; text-align: center;">
                <b>ROB qty. than Minimum qty. </b>
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 80px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 8%" align="right">
                                        Fleet :
                                    </td>
                                    <td style="width: 3%" align="left">
                                        <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                   
                                  

                                    <td style="width: 8%" align="right">
                                        Dept Type :
                                    </td>
                                    <td style="width: 12%" align="left">
                                        <asp:RadioButtonList ID="optList" runat="server" AutoPostBack="True" ForeColor="Black" Enabled="false"
                                            OnSelectedIndexChanged="optList_SelectedIndexChanged" RepeatDirection="Horizontal"
                                            TabIndex="2">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 6%" align="right">
                                        Department :
                                    </td>
                                    <td style="width: 10%" align="left">
                                        <asp:DropDownList ID="cmbDept" runat="server" AppendDataBoundItems="True" TabIndex="3" OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged" AutoPostBack="true"
                                            Width="160px">
                                            <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 6%" align="right">
                                        Part no / Drawing no / Name:&nbsp;&nbsp;
                                    </td>
                                      <td style="width: 10%" align="left">
                                        <asp:TextBox ID="txtSearchName" runat="server" CssClass="txtInput" Width="300%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" 
                                            TargetControlID="txtSearchName" WatermarkCssClass="watermarked" 
                                            WatermarkText="Type to Search" />
                                    </td>
                                </tr>
                                <tr align="left">

                                 <td  align="right">
                                        Vessel :
                                    </td>
                                    <td style="width: 12%" align="left">
                                        <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Width="120px">
                                        </asp:DropDownList>
                                    </td>

                                   
                                 <td align="right">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>

                                    <td align="right"  >
                                        System :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCatalogue" runat="server" CssClass="txtInput" Width="100%">
                                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x: hidden; overflow-y: none; height: 600px;">
                            <asp:GridView ID="rgdROB" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="rgdROB_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                OnSorting="rgdROB_Sorting" CellPadding="1" CellSpacing="0" OnRowCommand="rgdROB_RowCommand"
                                PageSize="20">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                 <asp:TemplateField HeaderText="Vessel" Visible="true">
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Vessel_Name")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="System" Visible="true">
                                        <HeaderTemplate>
                                            System
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.System_Description") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Number">
                                        <HeaderTemplate>
                                            Part Number
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.Part_Number") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Drawing Number">
                                        <HeaderTemplate>
                                            Drawing Number
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.Drawing_Number") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <HeaderTemplate>
                                            Item Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.Short_Description") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ROB Qty">
                                        <HeaderTemplate>
                                            ROB Qty
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.Inventory_Qty") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Qty">
                                        <HeaderTemplate>
                                            Min Qty
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.Inventory_Min") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdROB" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
