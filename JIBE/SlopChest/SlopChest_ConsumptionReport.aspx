<%@ Page Title=" SlopChest Consumption Report " Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SlopChest_ConsumptionReport.aspx.cs" Inherits="SlopChest_SlopChest_ConsumptionReport" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<link href="../Styles/SlopChestReport.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
            <div class="page-title">
                SlopChest Consumption Report
            </div>
            <div style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePaneConsumption" runat="server">
                    <ContentTemplate>
                        <div id="dvTableFilter" style="padding-top: 15px; padding-bottom: 5px; height: 70px;
                            width: 100%; margin-left: auto; margin-right: auto; text-align: center;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="center" style="width: 25%">
                                        <div>
                                            <label class="filterlabel">
                                                Date: &nbsp;
                                            </label>
                                            <asp:TextBox ID="txtDate" runat="server" Width="60%"> </asp:TextBox>
                                        </div>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <div>
                                            <label class="filterlabel">
                                                Crew: &nbsp;
                                            </label>
                                            <asp:TextBox ID="txtCrew" runat="server" Width="60%"> </asp:TextBox>
                                        </div>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <div>
                                            <label class="filterlabel">
                                                Item: &nbsp;
                                            </label>
                                            <%--<asp:TextBox ID="txtItem" runat="server"> </asp:TextBox>--%>
                                            <asp:DropDownList ID="ddItem" runat="server" Width="70%" AppendDataBoundItems="true"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" Style="padding-top: 2px" />
                                    </td>
                                    <td align="center" style="width: 5%;">
                                        <%--<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />--%>
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            Text="Clear Filter" Height="23px" />
                                    </td>
                                    <td style="width: 5%; text-align: left;">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                            Format="dd-MM-yyyy" />
                        <%-- CssClass="MyCalendar"--%>
                        <div id="divGrid" style="position: relative; margin-left: auto; margin-right: auto;
                            text-align: center;">
                            <asp:GridView ID="GvSCDisplayConsumption" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CellPadding="3" DataKeyNames="ROWNUM" EmptyDataText="NO RECORDS FOUND!" OnRowDataBound="GvSCDisplayConsumption_RowDataBound"
                                OnSorting="GvSCDisplayConsumption_Sorting" Width="100%">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="HeaderStyle-css">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlopchest_Date" runat="server" Text='<%#Eval("DATE")%>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="70px"
                                            Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crew" HeaderStyle-CssClass="HeaderStyle-css">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%#"~/Crew/CrewDetails.aspx?ID=" + Eval("ID")%>'
                                                Target="_blank" Text='<%# Eval("CrewCode")%>' CssClass="staffInfo"></asp:HyperLink>&nbsp;&nbsp;
                                            <%-- <asp:Label ID="lblSlopchest_Crew_Code" runat="server" Text='<%#Bind("CrewCode")%>'></asp:Label>&nbsp; --%>
                                            <asp:Label ID="lblSlopchest_Crew" runat="server" Text='<%#Eval("CREW")%>'></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblSlopchest_Crew_Rank" runat="server" Text='<%#Bind("CrewRank")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="130px" Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="HeaderStyle-css">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%# Bind("ITEM") %>' Width="110px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="110px" Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="HeaderStyle-css">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PRICE") %>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="70px" Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="HeaderStyle-css">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("QUANTITY") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="center" Width="50px"
                                            Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Final Amount" HeaderStyle-CssClass="HeaderStyle-css">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinalAmount" runat="server" Text='<%# Bind("FINAL_AMOUNT") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="80px" Wrap="true" />
                                        <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindConsumption" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
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
