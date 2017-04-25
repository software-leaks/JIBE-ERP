<%@ Page Language="C#" MasterPageFile="~/Site.master" Title="Handover Reports"  EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="CrewHandOver.aspx.cs" Inherits="CrewHandOver" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="width: 1200px; text-align: left;">
    <div class="page-title">
  
         Handover Reports
    </div>
        <%-- <div style="font-size: 16px; background-color: #5588BB; width: 100%; color: White;
            text-align: center;">
            <b>Crew Handover Index</b>
        </div>--%>
        <div id="dvPageContent" class="page-content-main">
            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                color: Black; text-align: left; background-color: #fff;">
                <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                 <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel" />
                </Triggers>
                    <ContentTemplate>
                        <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                            <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td>
                                        Fleet :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Vessel :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="156px"
                                            AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                         Rank :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRankFilter" runat="server" Width="156px" 
                                            >
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Search (Name/Staff Code) :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" Width="110px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="BtnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search"
                                            Width="80px" Height="25px" CssClass="btnCSS" />
                                    </td>
                                     <td style="text-align: right">
                                        <asp:Button ID="btn_Filter" runat="server"  Text="ClearFilter"
                                            Width="80px" Height="25px" CssClass="btnCSS" onclick="btn_Filter_Click" />
                                    </td>
                                    <td style="text-align: right">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="grid-container" style="margin-top: 2px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvMasterReview" runat="server" AutoGenerateColumns="False" CellPadding="3"
                            CellSpacing="0" Width="100%" EmptyDataText="No Record Found!" CaptionAlign="Bottom"
                            GridLines="None" DataKeyNames="ID" OnRowDataBound="gvMasterReview_RowDataBound"
                            AllowPaging="false" AllowSorting="true" OnSorting="gvMasterReview_Sorting" CssClass="GridView-css">
                             <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Vessel">
                                  <HeaderTemplate>
                                        <asp:LinkButton ID="lbtVesslNameHeader" runat="server" CommandName="Sort" Font-Underline="true" ForeColor="Black" 
                                            CommandArgument="Vessel_Short_Name">Vessel Name</asp:LinkButton>
                                        <img id="Vessel_Short_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                        <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# Eval("Vessel_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CREWID")%>'
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                  <HeaderTemplate>
                                    <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" Font-Underline="true" ForeColor="Black"
                                            CommandArgument="FULL_NAME">Name</asp:LinkButton>
                                        <img id="FULL_NAME" runat="server" visible="false" />
                                        </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CREWID")%>'
                                            Target="_blank" Text='<%# Eval("FULL_NAME")%>' ForeColor="Black"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle  HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                  <HeaderTemplate>
                                    <asp:LinkButton ID="lblRankHeader" runat="server" CommandName="Sort" Font-Underline="true" ForeColor="Black"
                                            CommandArgument="Rank_Short_Name">Rank</asp:LinkButton>
                                        <img id="Rank_Short_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Handover Date" HeaderStyle-HorizontalAlign="Center">
                                 <HeaderTemplate>
                                        <asp:LinkButton ID="lblHandOverHeader" runat="server" CommandName="Sort" Font-Underline="true" ForeColor="Black"
                                              CommandArgument="HANDOVER_DATE">Handover Date</asp:LinkButton>
                                        <img id="HANDOVER_DATE" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSign_On_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("HANDOVER_DATE"))) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        </asp:GridView>
                       <%--  <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindMasterReview" />
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" /> --%>
                        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                            background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                            background-color: #F6CEE3;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 60%">
                                        <uc1:ucCustomPager ID="ucCustomPager" runat="server" PageSize="30" OnBindDataItem="BindMasterReview" />
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td style="width: 210px">
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                    <td style="text-align: right">
                                        <span style="color: Blue;">
                                            <asp:Label ID="lblSEQ" runat="server"></asp:Label></span>&nbsp;&nbsp; &nbsp;
                                        <img id="imgLoading" src="../Images/ajax.gif" alt="" style="display: none;" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
